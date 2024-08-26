Imports System.ComponentModel
Imports System.Net.Mail
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.Mail
Imports System.Text
Imports System.Net
Imports System.Configuration

Public Class SentMailInBackground
    Inherits BackgroundWorker
    Implements IDisposable
    'Private Workers As New BackgroundWorker
    Public oMsg 'As New System.Net.Mail.MailMessage()
    Public SMTP As New System.Net.Mail.SmtpClient
    Public SMTP_Mail_User_Name
    Public SMTP_Mail_User_Password
    Public SMTP_Host
    Public SMTP_Port
    Public SMTP_DeliveryMethod
    Public SMTP_EnableSsl As Boolean
    Public SMTP_Mail_From
    Public SMTP_Use_Default_Credentials As Boolean
    Public SMTP_Use_SmtpServer_Or_SmtpClient As Boolean
    Public EnglishDirection As String = "<div style=" & Chr(34) & "direction:ltr; text-align:left;" & Chr(34) & ">"
    Public ArabicDirection As String = "<div style=" & Chr(34) & "direction:rtl; text-align:right;" & Chr(34) & ">"
    Public CenterDirection As String = "<div style=" & Chr(34) & "direction:ltr; text-align:center;" & Chr(34) & ">"
    Public SaveTo As String
    Public Function LoadSMTP() As Boolean
        Try
            If MagNote_Form.SMTP_Mail_User_Name_TxtBx.TextLength = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "ربما لم يتم اعداد ملف اعدادات النظام بعد"
                ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                    Msg = "Maybe Application Configuration File Not Prepared Yet."
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, IgnoreErrorMessageForConnection)
                Exit Function
            End If
            SMTP_Mail_User_Name = MagNote_Form.SMTP_Mail_User_Name_TxtBx.Text
            SMTP_Mail_User_Password = MagNote_Form.SMTP_Mail_User_Password_TxtBx.Text
            SMTP_Host = MagNote_Form.SMTP_Host_TxtBx.Text
            SMTP_Port = MagNote_Form.SMTP_Port_TxtBx.Text
            SMTP_DeliveryMethod = MagNote_Form.SMTP_Delivery_Method_TxtBx.Text
            SMTP_EnableSsl = MagNote_Form.SMTP_EnableSsl_TxtBx.Text
            SMTP_Mail_From = MagNote_Form.SMTP_Mail_From_TxtBx.Text
            SMTP_Use_Default_Credentials = MagNote_Form.SMTP_Use_Default_Credentials_TxtBx.Text
            SMTP_Use_SmtpServer_Or_SmtpClient = MagNote_Form.SMTP_Use_SmtpServer_Or_SmtpClient_TxtBx.Text
            Return True
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "ربما لم يتم اعداد ملف اعدادات النظام بعد"
            ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "Maybe Application Configuration File Not Prepared Yet."
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, IgnoreErrorMessageForConnection)
        End Try
    End Function

    Public Sub New(ByVal sSubject As String,
                                            ByVal RchTxtBx As RichTextBox,
                                            ByVal sTo As String,
                                            Optional ByVal sCC As String = Nothing,
                                            Optional ByVal sFilename As String = Nothing,
                                            Optional ByVal sDisplayname As String = Nothing,
                                            Optional ByVal Mail_From As String = "",
                                            Optional ByVal FileAsAttached As Boolean = False,
                                            Optional ByVal AttatchFiles As String = Nothing)
        MyBase.New()
        If Not LoadSMTP() Then
            GoTo Endsub
        End If
        If String.IsNullOrEmpty(sSubject) And
            IsNothing(RchTxtBx) And
            String.IsNullOrEmpty(sTo) Then
            GoTo Endsub
        End If
        'This call is the Windows Form Designer necessary.
        InitializeComponent()
        Dim sBody = ConvertRtfToHtml(RchTxtBx)
        'Add any initialization after the InitializeComponent call ()
        Me.WorkerReportsProgress = True
        Me.WorkerSupportsCancellation = True
        AddHandler Me.DoWork, AddressOf WorkerDoWork
        AddHandler Me.ProgressChanged, AddressOf WorkerProgressChanged
        AddHandler Me.RunWorkerCompleted, AddressOf WorkerCompleted
        'If FormVisible("Menu_Tree_Form") Then
        sBody = PrepareMessage(sBody)
        'End If
        If SMTP_Use_SmtpServer_Or_SmtpClient Then
            SendMailUsingSMTPClient(sSubject,
                                                            sBody,
                                                            sTo,
                                                            sCC,
                                                            sFilename,
                                                            sDisplayname,
                                                            Mail_From,
                                                            FileAsAttached,
                                                            AttatchFiles)
        Else
            SendMailUsingSMTPServer(sSubject,
                                                            sBody,
                                                            sTo,
                                                            sCC,
                                                            sFilename,
                                                            sDisplayname,
                                                            Mail_From,
                                                            FileAsAttached,
                                                            AttatchFiles)
        End If
Endsub:
    End Sub
    'Public Function PrepareMessage(ByVal Bdy As String) As String
    '    If IsNothing(Bdy) Then
    '        Dim x = 1
    '    End If
    '    Dim Direction As String = String.Empty
    '    'Return Bdy
    '    Dim CharacterPosition = -1
    '    Dim PreparedBdy As String = String.Empty
    '    Using LDPBC As New Loadind_Data_PrgrsBr_Cls(Bdy.Count)
    '        For Each Character As String In Bdy
    '            If Debugger.IsAttached Then
    '                PreparedBdy = Bdy
    '                Exit For
    '            End If
    '            LDPBC.RevalueLoadindDataPrgrsBr()
    '            CharacterPosition += 1
    '            Dim matchA As Match = Regex.Match(Character, "\p{IsArabic}", RegexOptions.IgnoreCase)
    '            Dim match1 As Match = Regex.Match(Character, "[a-zA-Z]", RegexOptions.IgnoreCase)
    '            If (matchA.Success) Then
    '                If Direction <> "{يتمإضافةإتجاهعربىهنا}" Then
    '                    Direction = "{يتمإضافةإتجاهعربىهنا}"
    '                    If Microsoft.VisualBasic.Right(PreparedBdy, 1) = "<" Then
    '                        PreparedBdy = Microsoft.VisualBasic.Left(PreparedBdy, PreparedBdy.Length - 1)
    '                        PreparedBdy &= "{يتمإضافةإتجاهعربىهنا}" & "<"
    '                    ElseIf Microsoft.VisualBasic.Right(PreparedBdy, 2) <> "</" Then
    '                        PreparedBdy &= "{يتمإضافةإتجاهعربىهنا}"
    '                    End If
    '                End If
    '            ElseIf (match1.Success) Then
    '                If Direction <> "{AddEnglishDirctionHere}" Then
    '                    Direction = "{AddEnglishDirctionHere}"
    '                    If Microsoft.VisualBasic.Right(PreparedBdy, 1) = "<" Then
    '                        PreparedBdy = Microsoft.VisualBasic.Left(PreparedBdy, PreparedBdy.Length - 1)
    '                        PreparedBdy &= "{AddEnglishDirctionHere}" & "<"
    '                    ElseIf Microsoft.VisualBasic.Right(PreparedBdy, 2) <> "</" Then
    '                        PreparedBdy &= "{AddEnglishDirctionHere}"
    '                    End If
    '                End If
    '            End If
    '            PreparedBdy &= Character
    '        Next
    '    End Using


    '    Bdy = PreparedBdy
    '    If IsNothing(Bdy) Then
    '        Dim x = 1
    '    End If
    '    Return Replace(Replace(Bdy, "{AddEnglishDirctionHere}", EnglishDirection), "{يتمإضافةإتجاهعربىهنا}", ArabicDirection)
    '    Bdy &= EnglishDirection
    'End Function
    Private Sub SendMailUsingSMTPClient(ByVal sSubject As String,
                                            ByVal sBody As String,
                                            ByVal sTo As String,
                                            Optional ByVal sCC As String = Nothing,
                                            Optional ByVal sFilename As String = Nothing,
                                            Optional ByVal sDisplayname As String = Nothing,
                                            Optional ByVal Mail_From As String = "",
                                            Optional ByVal FileAsAttached As Boolean = False,
                                            Optional ByVal AttatchFiles As String = Nothing)
        Try
            If sTo = "ReceiveMagNoteErrorMsg; " Or
                String.IsNullOrEmpty(sTo) Then
                sTo = "MagNoteErrorMsg@gmail.com;"
            End If
            Dim FromName As String = SMTP_Mail_From
            Dim ToAccounts As String() = Nothing
            sTo = Replace(sTo, ",", ";")
            If sTo.Length > 0 Then
                ToAccounts = sTo.Split(";")
            Else
                ShowMsg("To Mail Is Empty" & vbNewLine & sBody)
                Exit Sub
            End If
            oMsg = New System.Net.Mail.MailMessage(FromName, ToAccounts(0))
            Dim Accounts As Integer = 0
            For Each Acount In ToAccounts
                Try
                    If Acount.ToString.Length > 0 And
                        Not CType(oMsg, System.Net.Mail.MailMessage).To.Contains(New MailAddress(Acount)) Then
                        CType(oMsg, System.Net.Mail.MailMessage).To.Add(New MailAddress(Acount))
                        Accounts += 1
                    End If
                Catch ex As Exception
                End Try
            Next
            If CType(oMsg, System.Net.Mail.MailMessage).To.Count = 0 Then
                CType(oMsg, System.Net.Mail.MailMessage).To.Add(New MailAddress("AIOErrorMsg@gmail.com"))
            End If
            If Not IsNothing(sCC) And sCC <> "; " Then
                Dim ToScc As String() = Nothing
                ToScc = sCC.Split(";")
                For Each CC In ToScc
                    Try
                        If CC.ToString.Length > 0 Then
                            CType(oMsg, System.Net.Mail.MailMessage).CC.Add(CC)
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If
            If IsNothing(Mail_From) Then Mail_From = ""
            CType(oMsg, System.Net.Mail.MailMessage).Subject = Replace(sSubject, vbCrLf, Chr(34) & " - " & Chr(34)) & " - [ Current User (" & Mail_From & ")]"
            If sBody.Contains("<body") Then
                CType(oMsg, System.Net.Mail.MailMessage).Body = sBody
                If FileAsAttached Then
                    CType(oMsg, System.Net.Mail.MailMessage).Body &= "<BODY>"
                Else
                    CType(oMsg, System.Net.Mail.MailMessage).Body &= "<FONT face=Arial color=#000080 size=2></FONT>" & "<IMG alt='' hspace=0 src='" & Path.GetFileName(SaveTo) & "' align=baseline border=0>&nbsp;" & "<BODY>"
                End If
            Else
                If IsNothing(sBody) Then
                    CType(oMsg, System.Net.Mail.MailMessage).Body &= "<br><span style='font-family:'Calibri','sans-serif''>" & Err.Description & "</span></br>"
                Else
                    sBody = Replace(sBody, vbNewLine, "<br>")
                    CType(oMsg, System.Net.Mail.MailMessage).Body &= "<br><span style='font-family:'Calibri','sans-serif''>" & sBody & "</span></br>"
                End If
            End If
            CType(oMsg, System.Net.Mail.MailMessage).Body &= EnglishDirection
            CType(oMsg, System.Net.Mail.MailMessage).Body &= "<br><span style='font-family:'Calibri','sans-serif''> " &
                                  "<br>" &
                                 "[Message Time (" & Now & ")] <br>" &
                                 "[MagNote Version (" & My.Application.Info.Version.ToString & ")] <br>" &
                                 "[MagNote Owner User No. (" & Mail_From & ")] <br>" &
                                 "[MagNote Path (" & Application.StartupPath & ")]  <br>" &
                                 "[User Mail Address (" & MagNote_Form.Escalation_Auther_Mail_TxtBx.Text & ")]  <br>" &
                                 "[Network IP Address (" & IPAddress().ToString & ")]  <br>" &
                                 "[Public IP Address (" & PublicIPAddress() & ")]  <br>" &
                                 "[Windows Domain Name (" & Environment.UserDomainName & ")]  <br>" &
                                 "[Windows User Name (" & Environment.UserName & ")]  <br>" &
                                 "[Windows Machine Name (" & Environment.MachineName & ")] <br>" &
                                 " </span></br><p><p>"
            CType(oMsg, System.Net.Mail.MailMessage).Body &= "<FONT face=Arial color=#000080 size=2></FONT>" & "<IMG alt='' hspace=0 src='" & Path.GetFileName(SaveTo) & "' align=baseline border=0 />&nbsp;"
            CType(oMsg, System.Net.Mail.MailMessage).IsBodyHtml = True
            CType(oMsg, System.Net.Mail.MailMessage).Priority = System.Net.Mail.MailPriority.High
            If Not IsNothing(sFilename) Or Not IsNothing(AttatchFiles) Then
                If Not IsNothing(sFilename) Then
                    Dim Attachment = New System.Net.Mail.Attachment(sFilename)
                    CType(oMsg, System.Net.Mail.MailMessage).Attachments.Add(Attachment)
                End If
                If Not IsNothing(AttatchFiles) Then
                    Dim AttatchAllFiles() = Split(AttatchFiles, ",")
                    For Each AttatchFile In AttatchAllFiles
                        Dim Attachment = New System.Net.Mail.Attachment(AttatchFile)
                        CType(oMsg, System.Net.Mail.MailMessage).Attachments.Add(Attachment)
                    Next
                End If
            End If
            Try
                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(SMTP_Mail_User_Name.ToString, SMTP_Mail_User_Password.ToString)
                SMTP.Host = SMTP_Host
                SMTP.Port = SMTP_Port
                SMTP.EnableSsl = True 'SMTP_EnableSsl
                SMTP.UseDefaultCredentials = False
                Select Case SMTP_DeliveryMethod
                    Case "Network"
                        SMTP.DeliveryMethod = SmtpDeliveryMethod.Network
                    Case "PickupDirectoryFromIis"
                        SMTP.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
                    Case "SpecifiedPickupDirectory"
                        SMTP.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory
                End Select
                SMTP.Credentials = basicAuthenticationInfo
                RunWorkerAsync()
            Catch ex As Exception
                If ex.Message.Contains("Failure sending mail.") Then
                    ShowMsg("Failure sending mail.")
                Else
                    ShowMsg(sBody & vbNewLine & ex.Message)
                End If
            End Try
        Catch ex As Exception
            ShowMsg(ex.Message, sBody & vbNewLine & sFilename)
        Finally
        End Try
    End Sub
    Public Function ConvertRtfToHtml(ByVal RTF As RichTextBox) As String
        Dim strHTML As String
        Dim strColour As String
        Dim blnBold As Boolean
        Dim blnItalic As Boolean
        Dim strFont As String
        Dim shtSize As Short
        Dim lngOriginalStart As Long
        Dim lngOriginalLength As Long
        Dim intCount As Integer
        If RTF.Text.Length = 0 Then
            Exit Function
        End If
        ' Store original selections, then select first character
        lngOriginalStart = 0
        lngOriginalLength = RTF.TextLength
        RTF.Select(0, 1)
        ' Add HTML header
        strHTML = "<html>"
        ' Set up initial parameters
        strColour = RTF.SelectionColor.ToKnownColor.ToString
        blnBold = RTF.SelectionFont.Bold
        blnItalic = RTF.SelectionFont.Italic
        strFont = RTF.SelectionFont.FontFamily.Name
        shtSize = RTF.SelectionFont.Size
        ' Include first 'style' parameters in the HTML
        strHTML += "<span style=""font-family " & strFont &
          "; font-size " & shtSize & "pt; color: " _
                          & strColour & """>"
        ' Include bold tag, if required
        If blnBold = True Then
            strHTML += "<b>"
        End If
        ' Include italic tag, if required
        If blnItalic = True Then
            strHTML += "<i>"
        End If
        ' Finally, add our first character
        strHTML += RTF.Text.Substring(0, 1)
        ' Loop around all remaining characters
        For intCount = 2 To RTF.Text.Length
            ' Select current character
            RTF.Select(intCount - 1, 1)
            ' If this is a line break, add HTML tag
            If RTF.Text.Substring(intCount - 1, 1) =
                   Convert.ToChar(10) Then
                strHTML += "<br>"
            End If
            ' Check/implement any changes in style
            If RTF.SelectionColor.ToKnownColor.ToString <>
               strColour Or RTF.SelectionFont.FontFamily.Name _
               <> strFont Or RTF.SelectionFont.Size <> shtSize _
               Then
                strHTML += "</span><span style=""font-family " _
                  & RTF.SelectionFont.FontFamily.Name &
                  "; font-size " & RTF.SelectionFont.Size &
                  "pt; color " &
                  RTF.SelectionColor.ToKnownColor.ToString & """>"
            End If
            ' Check for bold changes
            If RTF.SelectionFont.Bold <> blnBold Then
                If RTF.SelectionFont.Bold = False Then
                    strHTML += "</b>"
                Else
                    strHTML += "<b>"
                End If
            End If
            ' Check for italic changes
            If RTF.SelectionFont.Italic <> blnItalic Then
                If RTF.SelectionFont.Italic = False Then
                    strHTML += "</i>"
                Else
                    strHTML += "<i>"
                End If
            End If
            ' Add the actual 
            strHTML += Mid(RTF.Text, intCount, 1)
            ' Update variables with current style
            strColour = RTF.SelectionColor.ToKnownColor.ToString
            blnBold = RTF.SelectionFont.Bold
            blnItalic = RTF.SelectionFont.Italic
            strFont = RTF.SelectionFont.FontFamily.Name
            shtSize = RTF.SelectionFont.Size
        Next
        ' Close off any open bold/italic tags
        If blnBold = True Then strHTML += ""
        If blnItalic = True Then strHTML += ""
        ' Terminate outstanding HTML tags
        strHTML += "</span></html>"
        ' Restore original RichTextRTF selection
        RTF.Select(lngOriginalStart, lngOriginalLength)
        ' Return HTML
        Return strHTML
    End Function
    Public Function PublicIPAddress() As String
        Try
            Dim ExternalIP As String
            ExternalIP = (New WebClient()).DownloadString("http://checkip.dyndns.org/")
            ExternalIP = (New Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(ExternalIP)(0).ToString()
            Return ExternalIP
        Catch
            Return Nothing
        End Try
    End Function
    Public Function IPAddress() As System.Net.IPAddress

        Try
            With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
                Try
                    Return New System.Net.IPAddress(.AddressList(1).Address)
                Catch ex As Exception
                    Return New System.Net.IPAddress(.AddressList(0).Address)
                End Try
            End With
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub SendMailUsingSMTPServer(ByVal sSubject As String,
                                            ByVal sBody As String,
                                            ByVal sTo As String,
                                            Optional ByVal sCC As String = Nothing,
                                            Optional ByVal sFilename As String = Nothing,
                                            Optional ByVal sDisplayname As String = Nothing,
                                            Optional ByVal Mail_From As String = "",
                                            Optional ByVal FileAsAttached As Boolean = False,
                                            Optional ByVal AttatchFiles As String = Nothing)
        Try
            oMsg = New Web.Mail.MailMessage
            If Not IsNothing(sFilename) Or Not IsNothing(AttatchFiles) Then
                If Not IsNothing(sFilename) Then
                    For Each file In Split(sFilename, ",")
                        If String.IsNullOrEmpty(file) Then Continue For
                        Dim oAttachment As New MailAttachment(file)
                        CType(oMsg, System.Web.Mail.MailMessage).Attachments.Add(oAttachment)
                    Next
                End If
                If Not IsNothing(AttatchFiles) Then
                    Dim AttatchAllFiles() = Split(AttatchFiles, ",")
                    For Each AttatchFile In AttatchAllFiles
                        Dim Attachment As New MailAttachment(AttatchFile)
                        CType(oMsg, System.Web.Mail.MailMessage).Attachments.Add(Attachment)
                    Next
                End If
            End If
            If Not String.IsNullOrEmpty(sTo) Then
                If sTo.Contains("ReceiveMagNoteErrorMsg;") Then
                    sTo = "AIOErrorMsg@gmail.com;"
                End If
            Else
                sTo = "AIOErrorMsg@gmail.com;"
            End If
            Dim FromName As String = Nothing
            If Not IsNothing(Mail_From) Then
                FromName = Mail_From
            Else
                FromName = SMTP_Mail_From
            End If
            If String.IsNullOrEmpty(FromName) Then
                FromName = SMTP_Mail_From
            End If
            sTo = Replace(sTo, ",", ";")
RefilloMsg:
            CType(oMsg, System.Web.Mail.MailMessage).To = sTo
            CType(oMsg, System.Web.Mail.MailMessage).Cc = sCC
            CType(oMsg, System.Web.Mail.MailMessage).Subject = Replace(sSubject, vbCrLf, Chr(34) & " - " & Chr(34)) & " - [ Current User (tickyNote)]"
            If IsNothing(sBody) Then
                Exit Sub
            End If
            If sBody.Contains("<body") Then
                CType(oMsg, System.Web.Mail.MailMessage).Body = sBody
                If FileAsAttached Then
                    CType(oMsg, System.Web.Mail.MailMessage).Body &= "<BODY>"
                Else
                    CType(oMsg, System.Web.Mail.MailMessage).Body &= "<FONT face=Arial color=#000080 size=2></FONT>" & "<IMG alt='' hspace=0 src='" & Path.GetFileName(SaveTo) & "' align=baseline border=0>&nbsp;" & "<BODY>"
                End If
            Else
                If IsNothing(sBody) Then
                    CType(oMsg, System.Web.Mail.MailMessage).Body &= "<br><span style='font-family:'Calibri','sans-serif''>" & Err.Description & "</span></br>"
                Else
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        sBody = "" & "<br>" & sBody & "<br>" & ""
                    End If
                    sBody = Replace(sBody, vbNewLine, "<br>")
                    CType(oMsg, System.Web.Mail.MailMessage).Body &= "<br><span style='font-family:'Calibri','sans-serif''>" & sBody & "</span></br>"
                End If
            End If
            CType(oMsg, System.Web.Mail.MailMessage).Body &= EnglishDirection
            CType(oMsg, System.Web.Mail.MailMessage).Body &= "<br><span style='font-family:'Calibri','sans-serif''> " &
                                  "<br>" &
                                 "[Message Time (" & Now & ")] <br>" &
                                 "[MagNote Version (" & My.Application.Info.Version.ToString & ")] <br>" &
                                 "[MagNote Owner User No. (" & Mail_From & ")] <br>" &
                                 "[MagNote Path (" & Application.StartupPath & ")]  <br>" &
                                 "[User Mai Address (" & MagNote_Form.Escalation_Auther_Mail_TxtBx.Text & ")]  <br>" &
                                 "[Network IP Address (" & IPAddress().ToString & ")]  <br>" &
                                 "[Public IP Address (" & PublicIPAddress() & ")]  <br>" &
                                 "[Windows Domain Name (" & Environment.UserDomainName & ")]  <br>" &
                                 "[Windows User Name (" & Environment.UserName & ")]  <br>" &
                                 "[Windows Machine Name (" & Environment.MachineName & ")] <br>" &
                                 " </span></br><p><p>"
            CType(oMsg, System.Web.Mail.MailMessage).Body &= "<FONT face=Arial color=#000080 size=2></FONT>" & "<IMG alt='' hspace=0 src='" & Path.GetFileName(SaveTo) & "' align=baseline border=0 />&nbsp;"
            CType(oMsg, System.Web.Mail.MailMessage).BodyFormat = MailFormat.Html
            CType(oMsg, System.Web.Mail.MailMessage).Priority = System.Net.Mail.MailPriority.High
            CType(oMsg, System.Web.Mail.MailMessage).BodyEncoding = System.Text.Encoding.UTF8
            Try
                'If IsNothing(SMTP_Mail_User_Name) Or IsNothing(SMTP_Mail_User_Password) Then
                '    SendMailByDefaultSetting(Nothing)
                'End If
                CType(oMsg, System.Web.Mail.MailMessage).From = FromName
                Dim oSendUsernameKey As String = "http://schemas.microsoft.com/cdo/configuration/sendusername"
                CType(oMsg, System.Web.Mail.MailMessage).Fields.Add(oSendUsernameKey, SMTP_Mail_User_Name.ToString)    'Sends the username with the msg
                Dim oSendPasswordKey As String = "http://schemas.microsoft.com/cdo/configuration/sendpassword"
                CType(oMsg, System.Web.Mail.MailMessage).Fields.Add(oSendPasswordKey, SMTP_Mail_User_Password.ToString) 'Sends the password with the msg
                Dim oSendPortKey As String = "http://schemas.microsoft.com/cdo/configuration/smtpserverport"
                CType(oMsg, System.Web.Mail.MailMessage).Fields.Add(oSendPortKey, SMTP_Port) 'Sends the Out Port with the msg
                Dim oSendEnableSslKey As String = "http://schemas.microsoft.com/cdo/configuration/smtpusessl"
                CType(oMsg, System.Web.Mail.MailMessage).Fields.Add(oSendEnableSslKey, Convert.ToBoolean(SMTP_EnableSsl)) 'Sends the sendusing with the msg
                Dim oAuthenticationKey As String = "http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"
                CType(oMsg, System.Web.Mail.MailMessage).Fields.Add(oAuthenticationKey, Convert.ToBoolean(SMTP_Use_Default_Credentials))    'Enables basic authentication
                SmtpMail.SmtpServer = SMTP_Host 'Set the mail server
                RunWorkerAsync()
            Catch ex As Exception
                If ex.Message.Contains("Failure sending mail.") Then
                    ShowMsg("Failure sending mail.")
                Else
                    ShowMsg(sBody & vbNewLine & ex.Message)
                End If
            End Try
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & sBody & vbNewLine & sFilename,,, 1)
        Finally
        End Try
    End Sub
    Dim NumberOfTry = 0
    Dim RunSQLQueriesToRun As Boolean

    Private Sub WorkerDoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        Try
ReTryToSend:
            If SMTP_Use_SmtpServer_Or_SmtpClient Then
                If IsNothing(oMsg.Sender) And Not IsNothing(oMsg.From) Then
                    oMsg.Sender = oMsg.From
                Else
                    Exit Sub
                End If
                SMTP.Send(oMsg)
            Else
                SmtpMail.Send(oMsg) 'Send the message
                End If
            Application.DoEvents()
        Catch ex As Exception
            Application.DoEvents()
            If ex.Message.Contains("The SMTP host was not specified") Then
                NumberOfTry += 1
                If NumberOfTry <> 4 Then
                    GoTo ReTryToSend
                Else
                    ShowMsg(ex.Message)
                End If
            Else
                ShowMsg(ex.Message)
            End If
        Finally
            oMsg = Nothing
        End Try
    End Sub
    Private Sub WorkerProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs)
        ' I did something!
    End Sub
    Dim Stopwatch As Stopwatch = Stopwatch.StartNew()
    Private Sub WorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Try
            ShowMsg(" Sending Mail Completed And Its Elapset Time Was " & Stopwatch.Elapsed.ToString)
        Catch ex As Exception
        End Try
    End Sub
    Public Function PrepareMessage(ByVal Bdy As String) As String
        If IsNothing(Bdy) Then
            Dim x = 1
        End If
        Dim Direction As String = String.Empty
        'Return Bdy
        Dim CharacterPosition = -1
        Dim PreparedBdy As String = String.Empty
        If Debugger.IsAttached Then
            GoTo DebuggerIsAttached
        End If
        For Each Character As String In Bdy
            CharacterPosition += 1
            Dim matchA As Match = Regex.Match(Character, "\p{IsArabic}", RegexOptions.IgnoreCase)
            Dim match1 As Match = Regex.Match(Character, "[a-zA-Z]", RegexOptions.IgnoreCase)
            If (matchA.Success) Then
                If Direction <> "{يتمإضافةإتجاهعربىهنا}" Then
                    Direction = "{يتمإضافةإتجاهعربىهنا}"
                    If Microsoft.VisualBasic.Right(PreparedBdy, 1) = "<" Then
                        PreparedBdy = Microsoft.VisualBasic.Left(PreparedBdy, PreparedBdy.Length - 1)
                        PreparedBdy &= "{يتمإضافةإتجاهعربىهنا}" & "<"
                    ElseIf Microsoft.VisualBasic.Right(PreparedBdy, 2) <> "</" Then
                        PreparedBdy &= "{يتمإضافةإتجاهعربىهنا}"
                    End If
                End If
            ElseIf (match1.Success) Then
                If Direction <> "{AddEnglishDirctionHere}" Then
                    Direction = "{AddEnglishDirctionHere}"
                    If Microsoft.VisualBasic.Right(PreparedBdy, 1) = "<" Then
                        PreparedBdy = Microsoft.VisualBasic.Left(PreparedBdy, PreparedBdy.Length - 1)
                        PreparedBdy &= "{AddEnglishDirctionHere}" & "<"
                    ElseIf Microsoft.VisualBasic.Right(PreparedBdy, 2) <> "</" Then
                        PreparedBdy &= "{AddEnglishDirctionHere}"
                    End If
                End If
            End If
            PreparedBdy &= Character
        Next

        Bdy = PreparedBdy
DebuggerIsAttached:
        If IsNothing(Bdy) Then
            Dim x = 1
        End If
        Bdy &= EnglishDirection
        Return Replace(Replace(Bdy, "{AddEnglishDirctionHere}", EnglishDirection), "{يتمإضافةإتجاهعربىهنا}", ArabicDirection)
    End Function

#Region "IDisposable"
    Private disposedValue As Boolean
    Private components As System.ComponentModel.IContainer

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
