Imports System.Xml.Linq
Imports System.IO

Public Class GreenAPIManager
    Private ReadOnly filePath As String

    Public Sub New(xmlFilePath As String)
        Try
            filePath = MagNoteFolderPath & "\" & xmlFilePath
            If Not IsNothing(xmlFilePath) Then
                CreateXmlIfNotExists()
            Else
                filePath &= "GreenAPI.xml"
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    ' Create base XML if not present
    Private Sub CreateXmlIfNotExists()
        If Not File.Exists(filePath) Then
            Dim doc As New XDocument(New XElement("GreenAPI_Entries"))
            doc.Save(filePath)
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "تم إنشاء الملف بنجاح"
            Else
                Msg = "File Created Successfully"
            End If
            ShowMsg(Msg & vbNewLine & filePath, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End If
    End Sub

    ' Add new entry
    Public Sub Add(Name As String, id As String, token As String, phone As String, xtrnlfl As String, dgapi As CheckState, desc As String)
        Try
            If MagNote_Form.Default_Green_API_ChkBx.CheckState Then
                CleareDefaultGrenAPI()
            End If
            Dim doc As XDocument = XDocument.Load(filePath)

            doc.Root.Add(
            New XElement("GreenAPI",
                New XElement("GreenAPI_Name", Name),
                New XElement("ID_Instance", id),
                New XElement("API_Token_Instance", token),
                New XElement("Phone_Number", phone),
                New XElement("External_File", xtrnlfl),
                New XElement("Default_Green_API", Convert.ToDouble(dgapi)),
                New XElement("Description", desc)
            )
        )
            doc.Save(filePath)
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "تم إنشاء العنصر بنجاح"
            Else
                Msg = "Element Created Successfully"
            End If
            ShowMsg(Msg & vbNewLine & Name & vbNewLine & id, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Function CleareDefaultGrenAPI() As Boolean
        Try
            Dim doc As XDocument = XDocument.Load(filePath)
            For Each Elmnt In doc.Descendants("GreenAPI")
                Dim Default_Green_API = Elmnt.Element("Default_Green_API")
                If Default_Green_API IsNot Nothing Then
                    Default_Green_API.Value = CheckState.Unchecked
                End If
            Next
            doc.Save(filePath)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    ' Update entry by ID_Instance
    Public Sub Update(Name As String, id As String, token As String, phone As String, xtrnlfl As String, dgapi As CheckState, desc As String)
        Try
            If MagNote_Form.Default_Green_API_ChkBx.CheckState Then
                CleareDefaultGrenAPI()
            End If
            Dim doc As XDocument = XDocument.Load(filePath)
            Dim entry = doc.Root.Elements("GreenAPI").
            FirstOrDefault(Function(e) e.Element("ID_Instance")?.Value = id AndAlso e.Element("GreenAPI_Name")?.Value = Name)
            If entry IsNot Nothing Then
                entry.Element("API_Token_Instance").Value = token
                entry.Element("Phone_Number").Value = phone
                entry.Element("External_File").Value = xtrnlfl
                entry.Element("Default_Green_API").Value = Convert.ToDouble(dgapi)
                entry.Element("Description").Value = desc
                doc.Save(filePath)
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تم تحديث العنصر بنجاح"
                Else
                    Msg = "Element Updated Successfully"
                End If
                ShowMsg(Msg & vbNewLine & Name & vbNewLine & id, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    ' Delete entry by ID_Instance
    Public Sub Delete(Name As String, id As String)
        Try
            Dim doc As XDocument = XDocument.Load(filePath)
            Dim entry = doc.Root.Elements("GreenAPI").
            FirstOrDefault(Function(e) e.Element("ID_Instance")?.Value = id AndAlso
                           e.Element("GreenAPI_Name")?.Value = Name)
            If entry IsNot Nothing Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "سيتم إلغاء هذا العنصر... هل انت متأكد؟"
                Else
                    Msg = "Thsi Element Will Be Deleted... Are You Sure?"
                End If
                If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                    Exit Sub
                End If
                entry.Remove()
                doc.Save(filePath)
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تم إلغاء العنصر بنجاح"
                Else
                    Msg = "Element Deleted Successfully"
                End If
                ShowMsg(Msg & vbNewLine & Name & vbNewLine & id, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Else
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "لم يتم العثور على هذا العنصر"
                Else
                    Msg = "This Element Not Found"
                End If
                ShowMsg(Msg & vbNewLine & Name & vbNewLine & id, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Function Element_Exist(Name As String, ID_Instance As String)
        Try
            Dim doc As XDocument = XDocument.Load(filePath)
            Dim entry = doc.Root.Elements("GreenAPI").
            FirstOrDefault(Function(e) e.Element("ID_Instance")?.Value = ID_Instance AndAlso
                           e.Element("GreenAPI_Name")?.Value = Name)
            If entry IsNot Nothing Then
                Return entry
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    ' Get API_Token_Instance by Element
    Public Function GetApiTokenByElement(ElementName As String, Elementvalue As String, ElemenToReturnValue As String, Optional ReturnElement As Boolean = False) As Object
        Try
            Dim doc As XDocument = XDocument.Load(filePath)
            Dim entry = doc.Root.Elements("GreenAPI").
            FirstOrDefault(Function(e) e.Element(ElementName)?.Value = Elementvalue)
            If entry IsNot Nothing Then
                If ReturnElement Then
                    Return entry
                Else
                    Return entry.Element(ElemenToReturnValue)?.Value
                End If
            End If
            Return Nothing ' or String.Empty
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public Sub LoadGreenAPICmbBx()
        Try
            MagNote_Form.Green_API_Name_CmbBx.Items.Clear()
            'Dim doc As XDocument = XDocument.Load(MagNoteFolderPath & "\GreenAPI.xml")
            Dim doc As XDocument = XDocument.Load(filePath)
            For Each entry In doc.Root.Elements("GreenAPI")
                ' You can customize what to display: e.g., phone + name
                Dim GreenAPI_Name = entry.Element("GreenAPI_Name")?.Value
                Dim ID_Instance = entry.Element("ID_Instance")?.Value
                MagNote_Form.Green_API_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(GreenAPI_Name, ID_Instance))
            Next
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
End Class
