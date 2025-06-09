Imports System.Net
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Net.Http.Headers
Public Class GreenApiFileUploader
    Implements IDisposable

    Private instanceId As String
    Private apiToken As String

    Public Sub New(instanceId As String, apiToken As String)
        Me.instanceId = instanceId
        Me.apiToken = apiToken
    End Sub

    Public Function SendWhatsAppMessageFile(toPhone As String, filePath As String, caption As String) As String
        Dim boundary As String = "----GreenApiBoundary" & DateTime.Now.Ticks.ToString("x")
        Dim apiUrl As String = $"https://api.green-api.com/waInstance{instanceId}/sendFileByUpload/{apiToken}"

        ' Read file
        Dim fileBytes As Byte() = File.ReadAllBytes(filePath)
        Dim fileName As String = Path.GetFileName(filePath)

        ' Build multipart/form-data body
        Dim sb As New StringBuilder()

        ' chatId part
        sb.AppendLine($"--{boundary}")
        sb.AppendLine("Content-Disposition: form-data; name=""chatId""")
        sb.AppendLine()
        If Not toPhone.EndsWith("@g.us") Then
            sb.AppendLine($"{toPhone}@c.us")
        End If
        'sb.AppendLine($"{toPhone}@c.us")

        ' caption part
        sb.AppendLine($"--{boundary}")
        sb.AppendLine("Content-Disposition: form-data; name=""caption""")
        sb.AppendLine()
        sb.AppendLine(caption)

        ' file part header
        sb.AppendLine($"--{boundary}")
        sb.AppendLine($"Content-Disposition: form-data; name=""file""; filename=""{fileName}""")
        sb.AppendLine("Content-Type: application/octet-stream")
        sb.AppendLine()
        Dim preFileBytes As Byte() = Encoding.UTF8.GetBytes(sb.ToString())
        Dim postFileBytes As Byte() = Encoding.UTF8.GetBytes(vbCrLf & $"--{boundary}--" & vbCrLf)
        ' Combine all parts
        Dim totalBytes(preFileBytes.Length + fileBytes.Length + postFileBytes.Length - 1) As Byte
        Buffer.BlockCopy(preFileBytes, 0, totalBytes, 0, preFileBytes.Length)
        Buffer.BlockCopy(fileBytes, 0, totalBytes, preFileBytes.Length, fileBytes.Length)
        Buffer.BlockCopy(postFileBytes, 0, totalBytes, preFileBytes.Length + fileBytes.Length, postFileBytes.Length)
        ' Create request
        Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = $"multipart/form-data; boundary={boundary}"
        request.ContentLength = totalBytes.Length
        ' Write to stream
        Using requestStream As Stream = request.GetRequestStream()
            requestStream.Write(totalBytes, 0, totalBytes.Length)
        End Using
        ' Get response
        Try
            Using response As WebResponse = request.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream())
                    Return reader.ReadToEnd()
                End Using
            End Using
        Catch ex As WebException
            Using reader As New StreamReader(ex.Response.GetResponseStream())
                Return "Error: " & reader.ReadToEnd()
            End Using
        End Try
    End Function
    Public Function SendWhatsAppMessage(idInstance As String, apiTokenInstance As String, phoneNumber As String, messageText As String) As Object
        Try
            Dim url As String = $"https://api.green-api.com/waInstance{idInstance}/SendMessage/{apiTokenInstance}"
            ' Message payload
            If Not phoneNumber.EndsWith("@g.us") Then
                phoneNumber &= "@c.us"
            End If
            Dim payload As New With {
            .chatId = phoneNumber,' & "@c.us", ' phone must be in international format without "+"
            .message = messageText
        }
            ' Serialize to JSON
            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)
            Dim data As Byte() = Encoding.UTF8.GetBytes(jsonPayload)
            ' Create request
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.ContentLength = data.Length
            ' Write data to request stream
            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using
            Try
                Using response As WebResponse = request.GetResponse()
                    Using reader As New StreamReader(response.GetResponseStream())
                        Return reader.ReadToEnd()
                    End Using
                End Using
            Catch ex As WebException
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Return "Error: " & reader.ReadToEnd()
                End Using
            End Try
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    ' 

    Public Function GetWhatsAppContacts(idInstance As String, apiToken As String, My_Contacts_List_DGV As DataGridView) As List(Of String)
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Dim url As String = $"https://api.green-api.com/waInstance{idInstance}/getContacts/{apiToken}"
            Dim client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            Dim json As String = client.DownloadString(url)
            Dim contacts = JsonConvert.DeserializeObject(Of List(Of Contact))(json)
            Dim names As New List(Of String)
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, contacts.Count)
            Dim ContactType As String
            Using XMLEditor As New XMLEditor("WhatsAppContants.xml", "WhatsAppContants")
                For Each contact In contacts
                    progress += ProgressToAdd
                    If String.IsNullOrEmpty(contact.name) Then Continue For
                    Previewlbl.Text = "Loading--> " & contact.name & vbNewLine & Math.Floor(progress * 100)
                    Previewlbl.Refresh()
                    Previewlbl.Invalidate()
                    If contact.id.EndsWith("@g.us") Then
                        ContactType = "Group"
                    ElseIf contact.id.EndsWith("@c.us") Then
                        ContactType = "Individual"
                    End If
                    MagNote_Form.My_Contacts_List_DGV.Rows.Add(contact.name, Replace(Replace(contact.id, "@c.us", ""), "@g.us", ""), ContactType)
                    XMLEditor.Add("WhatsAppContactName",
                          New Dictionary(Of String, String) From {
                              {"ContactName", contact.name},
                              {"ContactId", Replace(Replace(contact.id, "@c.us", ""), "@g.us", "")},
                              {"ContactType", ContactType}},
                          New Dictionary(Of String, String) From {
                              {"ContactName", contact.name},
                              {"ContactId", Replace(Replace(contact.id, "@c.us", ""), "@g.us", "")},
                              {"ContactType", ContactType}}, 0)
                Next
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            PreviewPnl.Dispose()
            Previewlbl.Dispose()
        End Try
    End Function
    Dim GroupNumbers As String
    Public Async Function GetGroupMembers(instanceId As String, token As String, groupId As String) As Task
        Dim url As String = $"https://api.green-api.com/waInstance{instanceId}/getGroupData/{groupId}/{token}"
        GroupNumbers = String.Empty
        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync(url)
            If response.IsSuccessStatusCode Then
                Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
                Dim parsed = JObject.Parse(jsonResponse)
                Dim members = parsed("participants")
                For Each member In members
                    GroupNumbers &= member("id").ToString().Replace("@c.us", "") & ","
                Next
                'Return Numbers
            Else
                Console.WriteLine("Error: " & response.StatusCode)
            End If
        End Using
    End Function

    Public Async Function SendMessageToGoup(instanceId As String, apiToken As String, groupId As String, message As String) As Task
        Dim baseUrl As String = $"https://api.green-api.com/waInstance{instanceId}/sendMessage/{apiToken}"

        Dim payload As New With {
        .chatId = groupId,
        .message = message
    }

        Dim json As String = JsonConvert.SerializeObject(payload)
        Using client As New HttpClient()
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync(baseUrl, content)
            Dim result As String = Await response.Content.ReadAsStringAsync()
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Message sent: " & result)
            Else
                MessageBox.Show("Failed: " & result)
            End If


            'Try
            '    response = Await client.PostAsync(
            '$"https://api.green-api.com/waInstance{instanceId}/sendFileByUpload/{apiToken}", Form)
            '    result = Await response.Content.ReadAsStringAsync()
            '    Console.WriteLine(result)
            'Catch ex As WebException
            '    Using reader As New StreamReader(ex.Response.GetResponseStream())
            '        Console.WriteLine(result)
            '    End Using
            'End Try
        End Using
    End Function

    'Public Async Function SendMessageToGoup(instanceId As String, apitoken As String, groupId As String, messageText As String) As Task
    '    Try
    '        Dim url As String = $"https://api.green-api.com/waInstance{instanceId}/sendMessage/{apitoken}"
    '        Dim client As New HttpClient()
    '        'Dim instanceId As String = "1101000000"
    '        'Dim apiToken As String = "your-api-token"
    '        'Dim groupId As String = "1234567890-1612345678@g.us"
    '        Dim jsonBody As String = $"{{""chatId"":""{groupId}"",""message"":""{messageText}""}}"
    '        Dim content As New StringContent(jsonBody, Encoding.UTF8, "application/json")
    '        Dim response As HttpResponseMessage
    '        Dim result
    '        Try
    '            response = Await client.PostAsync(url, content)
    '            result = Await response.Content.ReadAsStringAsync()
    '        Catch ex As WebException
    '            Using reader As New StreamReader(ex.Response.GetResponseStream())
    '                Console.WriteLine(result)
    '            End Using

    '        End Try
    '        Dim responseString As String = Await response.Content.ReadAsStringAsync()
    '        Console.WriteLine(responseString)
    '    Catch ex As Exception

    '    End Try
    'End Function


    'Sub SendMessageToGoup(instanceId As String, apiToken As String, groupId As String, message As String)
    '    Dim baseUrl As String = $"https://api.green-api.com/waInstance{instanceId}/sendMessage/{apiToken}"

    '    Dim payload As New With {
    '    .chatId = groupId,
    '    .message = message
    '}

    '    Dim json As String = JsonConvert.SerializeObject(payload)

    '    Using client As New HttpClient()
    '        Dim content As New StringContent(json, Encoding.UTF8, "application/json")
    '        Dim response As HttpResponseMessage = client.PostAsync(baseUrl, content).Result

    '        Dim result As String = response.Content.ReadAsStringAsync().Result
    '        If response.IsSuccessStatusCode Then
    '            MsgBox("Message sent: " & result)
    '        Else
    '            MsgBox("Failed: " & result)
    '        End If
    '    End Using
    'End Sub

    Dim SendingSendWhatsApMessage As Boolean
    Public Async Function SendWhatsApMessage(instanceId As String, apitoken As String, groupId As String, files As List(Of String), Caption As String) As Task
        Try
            While SendingSendWhatsApMessage
                ApplicationDoEvents(MagNote_Form)
            End While
            SendingSendWhatsApMessage = True
            If Caption.Length = 0 Then
                Caption = "Caption"
            End If
            If files.Count = 0 Then
                Dim baseUrl As String = $"https://api.green-api.com/waInstance{instanceId}/sendMessage/{apitoken}"
                Dim payload As New With {
                            .chatId = groupId,
                            .message = Caption
                        }
                Dim json As String = JsonConvert.SerializeObject(payload)
                Using client As New HttpClient()
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "ربما رقم الموبايل خطا او لم يبدأ بكود البلد او احد لبيانات الرئيسية بها خطأ ما مثال
رقم جرين آبى التعريفي 
الرمز المميز للتوكن"
                    Else
                        Msg = "The Mobile Number May Be Incorrect Or Not Starting With The Country Code
or There May Be An Error In One Of The Main Data Points. Example:
Green Aby ID Instance
API Token Insance"
                    End If

                    Dim content As New StringContent(json, Encoding.UTF8, "application/json")
                    Dim response As HttpResponseMessage = Await client.PostAsync(baseUrl, content)
                    Dim result As String = Await response.Content.ReadAsStringAsync()
                    If Not response.IsSuccessStatusCode Then
                        MagNote_Form.Phone_Numbers_Failed_Sending_Its_Messages_TxtBx.Text &= result
                    End If
                End Using
            Else
                Dim responseIsSuccessStatusCode As String = String.Empty
                Using client As New HttpClient()
                    Using form As New MultipartFormDataContent()
                        form.Add(New StringContent(groupId), "chatId")
                        'If Not filePath.Contains("infosysme.png") Then
                        form.Add(New StringContent(Caption), "caption")
                        'End If
                        For Each filePath As String In files
                            If filePath IsNot Nothing Then
                                Dim fileContent As New ByteArrayContent(IO.File.ReadAllBytes(filePath))
                                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream")
                                form.Add(fileContent, "file", IO.Path.GetFileName(filePath))
                            End If
                        Next
                        Dim response As HttpResponseMessage
                        Dim result
                        response = Await client.PostAsync(
                        $"https://api.green-api.com/waInstance{instanceId}/sendFileByUpload/{apitoken}", form)

                        result = Await response.Content.ReadAsStringAsync()
                        Console.WriteLine(result)
                        If Not response.IsSuccessStatusCode Then
                            MagNote_Form.Phone_Numbers_Failed_Sending_Its_Messages_TxtBx.Text &= result
                        End If
                    End Using
                End Using
            End If

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            SendingSendWhatsApMessage = False
        End Try
    End Function
    Public Async Function SendFileToGroupAsync(instanceId As String, apitoken As String, groupId As String) As Task
        Dim url As String = $"https://api.green-api.com/waInstance{instanceId}/sendFileByUrl/{apitoken}"

        'Dim instanceId As String = "1101000000"
        'Dim apiToken As String = "your-api-token"
        'Dim groupId As String = "1234567890-1612345678@g.us"
        Dim fileUrl As String = "https://example.com/file.pdf"
        Dim fileName As String = "file.pdf"
        Dim caption As String = "Please check this document."


        Dim payload As String = $"{{""chatId"":""{groupId}"",""urlFile"":""{fileUrl}"",""fileName"":""{fileName}"",""caption"":""{caption}""}}"

        Using client As New HttpClient()
            Dim content As New StringContent(payload, Encoding.UTF8, "application/json")
            Dim response = Await client.PostAsync(url, content)
            Dim result As String = Await response.Content.ReadAsStringAsync()
            Console.WriteLine(result)
        End Using
    End Function

#Region "IDisposable"
    Private disposedValue As Boolean
    Private components As System.ComponentModel.IContainer
    Public Sub New()
        MyBase.New()
        'This call is the Windows Form Designer necessary.
        InitializeComponent()
        'Add any initialization after the InitializeComponent call ()
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
                ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
                ' TODO: set large fields to null
                disposedValue = True
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Dispose(disposing)
            End If
        End If
    End Sub
    ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    Protected Overrides Sub Finalize()
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=False)
        MyBase.Finalize()
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        'GC.Collect()
        'GC.Collect(2, GCCollectionMode.Optimized)
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
