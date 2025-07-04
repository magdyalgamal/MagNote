Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Xml
Imports unvell.ReoGrid
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Net
Imports System.Configuration
Imports System.Reflection
Imports System.Net.NetworkInformation
Module MainModule
    Public DefaultEncryptionKey As String = "EncryptionKey"
    'Public DefaultEncryptionKeyModeUsed As Boolean
    Public SystemOpenedAbnormal As Boolean
    Public FileOpenedByOpenNoteTlStrpBtn As Boolean
    Public ApplicationStartupPath As String
    Public MyApplicationStartupNextInstance As Boolean
    Public systemShutdown As Boolean = False
    Public IgnoreMagNoteNoCmbBxValidating As Boolean
    Public delimiters As String() = New String() {vbCrLf, vbLf}
    Public cnnStr As New System.Data.OleDb.OleDbConnection
    Public dataadapter As New OleDbDataAdapter("", cnnStr)
    Public connetionString As String
    Public connection As SqlConnection
    Public adapter As SqlDataAdapter
    Public cmdBuilder As SqlCommandBuilder
    Public ds As New DataSet
    Public sql As String
    Public SQLDBConnection As SqlConnection = New SqlConnection(SQLConnStr)
    Public SQLConnStr As String = Nothing 'ConvertODBCToSQLDB()
    Public GetFieldValueDS As New DataSet
    Public MyGetFieldValueProvider As DbProviderFactory
    Public MyGetFieldValueAdapter As DbDataAdapter
    Public MyGetFieldValueCommand As DbCommand
    Public TimeOnly As Boolean
    'Public WithEvents ToolTip1 As New ToolTip
    Public ApplicationRestart As Boolean
    Public SNFF As Boolean
    Public FirstTimeRun As Boolean
    Public UseArgFile As String
    Public ExternalFilePath, ExternalFileName
    Public MagNoteFolderPath As String '
    Public AlreadyAsked As Boolean
    'Public Save_Setting_When_Exit As Boolean
    Public Msg As String
    Public OpenDBStatus As Boolean
    Public PassedMainPasswordToPass As Boolean = False
    Public MagNoteRTF() As RichTextBox
    Public IgnoreErrorMessageForConnection
    Public MessageBoxTimer As New Threading.Thread(AddressOf closeMessageBox)
    Public EncryptionKey As String
    Private Const VK_RETURN As Byte = &HD
    Private Const KEYEVENTF_KEYDOWN As Byte = &H0
    Private Const KEYEVENTF_KEYUP As Byte = &H2
    Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Byte, ByVal dwExtraInfo As Byte)
    Declare Function sndPlaySound Lib "winmm.dll" Alias "sndPlaySoundA" (ByVal lpszName As String, ByVal dwFlags As Long) As Long
    Public Const SND_ASYNC = &H1
    Public RchTxtBxStyle(0) As RchTxtBxStyleDetils
    Structure RchTxtBxStyleDetils
        Public RchTxtBx_Name As String
        Public Blocked_Note As CheckState
        Public Finished_Note As CheckState
        Public Secured_Note As CheckState
        Public Use_Main_Password As CheckState
        Public Note_Password As String
        Public Note_Font As Font
        Public ForeColor As Color
        Public BackColor As Color
        Public AlternatingRowColor As Color
        Public Word_Wrap As CheckState
        Public Note_Have_Reminder As CheckState
        Public Next_Reminder_Time As String
        Public Reminder_Every As String
    End Structure
    'Structure RchTxtBxSelectedTextDetails
    '    Public MagNoteName As String
    '    Public SelectedText As String
    '    Public SelectedTextLength As Integer
    '    Public SelectedTextLine As Integer
    '    Public SelectedTextColumn As Integer
    '    Public SelectionStart As Integer
    'End Structure
    'Public RchTxtBxSelectedText(0) As RchTxtBxSelectedTextDetails
    Public Function AdjustPreview() As Boolean
        Dim CurrentNote = RCSN()
    End Function
    Structure FTP_Login_Data
        Dim FTP_Address As String
        Dim FTP_UserName As String
        Dim FTP_Password As String
        Dim FTP_Use_Default_Credentials As String
    End Structure
    Public FTP_Login(0) As FTP_Login_Data
    Public ClientCredentials(0) As FTP_Login_Data
    Public Function IsInternetAvailable() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "لم تفلح محاولة التحقق من وجود اتصال بالشبكة العنكبوتية الدولية"
            ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "Couldn't Check To Find Internet Connection"
            End If
            ShowMsg(Msg & vbNewLine & ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
        End Try
    End Function
    Public Function FilesBeforClose(Optional ByVal Restore As Boolean = False) As Boolean
        If SystemOpenedAbnormal Then
            Exit Function
        End If
        Dim FilesToCopy As String
        FilesToCopy = "\MagNote_Setting.txt,\MagNotes_Files\AdditionalFilesToUpload.xml,\MagNotes_Files\Alerts.xml,\MagNotes_Files\ExternalOpenFileParameters.xml,\MagNotes_Files\GreenAPI.xml,\MagNotes_Files\Life_Labeling_And_Tooltip.xml,\MagNotes_Files\MagNoteGroupsForWhatsApp.xml,\MagNotes_Files\MagNoteShortcuts.xml,\MagNotes_Files\NewCategories.xml,\MagNotes_Files\WhatsAppContants.xml"
        Dim FileToCopy, FileNameWithoutExtension, FileExtention As String
        If Restore Then
            For Each fil In FilesToCopy.Split(",")
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fil)
                FileExtention = Path.GetExtension(fil)
                FileToCopy = MagNoteFolderPath & "\" & FileNameWithoutExtension & "_Copy" & FileExtention
                If File.Exists(FileToCopy) Then
                    File.Copy(FileToCopy, ApplicationStartupPath & fil, 1)
                End If
            Next
        Else
            For Each fil In FilesToCopy.Split(",")
                FileToCopy = ApplicationStartupPath & fil
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fil)
                FileExtention = Path.GetExtension(fil)
                If File.Exists(FileToCopy) Then
                    File.Copy(FileToCopy, MagNoteFolderPath & "\" & FileNameWithoutExtension & "_Copy" & FileExtention, 1)
                End If
            Next
        End If
    End Function
    Public Sub ApplyCustomMenuColors(MyMenuStrip As Object, Optional ByVal HighlightColor As Color = Nothing)
        Dim MenustripItemHighlightColor
        MenustripItemHighlightColor = HighlightColor
        Dim customColorTable As New CustomColorTable(MenustripItemHighlightColor)
        Dim customRenderer As New ToolStripProfessionalRenderer(customColorTable)
        MyMenuStrip.Renderer = customRenderer
        If MagNote_Form.Language_Btn.Text = "E" Then
            MyMenuStrip.righttoleft = RightToLeft.Yes
        Else
            MyMenuStrip.righttoleft = RightToLeft.No
        End If
    End Sub
    Public Function CreatAppConfig() As Boolean
        Try
            Dim TextToWrite = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "utf-8" & Chr(34) & "?>
<configuration>
		<connectionStrings>
		<add name=" & Chr(34) & "MagNote_Local" & Chr(34) & " connectionString=" & Chr(34) & "
			Data Source=Enter Your SQL Server Name;
			Initial Catalog=Enter Your Database Name;     
			persist security info=False;        
			User Id=Enter Your SQL User Name;     
			Password=Enter Your SQL User Pssword;        
			User Instance=false; 
			Integrated Security=false; 
			Connect Timeout=30;		 
			MultipleActiveResultSets=True;
			Connection Lifetime=0;
			Pooling=True;
			Max Pool Size=10;
			ConnectRetryCount=30;
			ConnectRetryInterval=5;
			Packet Size=32767;
			trusted_connection=no;
			MultipleActiveResultSets=True" & Chr(34) & "
		providerName=" & Chr(34) & "System.Data.SqlClient" & Chr(34) & " />
		<add name=" & Chr(34) & "MagNote_Global" & Chr(34) & " connectionString=" & Chr(34) & "
			Data Source=Enter Your SQL Host Server Name;
			Initial Catalog=Enter Your Database Name At Host;     
			persist security info=False;        
			User Id=Enter Your SQL User Name At Host;     
			Password=Enter Your SQL User Password At Host;        
			User Instance=false; 
			Integrated Security=false; 
			Connect Timeout=30;		 
			MultipleActiveResultSets=True;
			Connection Lifetime=0;
			Pooling=True;
			Max Pool Size=10;
			ConnectRetryCount=30;
			ConnectRetryInterval=5;
			Packet Size=32767;
			trusted_connection=no;
			MultipleActiveResultSets=True" & Chr(34) & "
			providerName=" & Chr(34) & "System.Data.SqlClient" & Chr(34) & " />
	</connectionStrings>
	<startup>
		<supportedRuntime version=" & Chr(34) & "v4.0" & Chr(34) & " sku=" & Chr(34) & ".NETFramework,Version=v4.7.2" & Chr(34) & " />
		<system.windows.forms jitDebugging=" & Chr(34) & "true" & Chr(34) & " />
	</startup>
  <appSettings>
		<add key=" & Chr(34) & "SMTP_Mail_User_Name" & Chr(34) & " value=" & Chr(34) & "EnterSMTPMailUserName" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Mail_User_Password" & Chr(34) & " value=" & Chr(34) & "EnterSMTPMailUserPassword" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Host" & Chr(34) & " value=" & Chr(34) & "EnterSMTPHostServerURL" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Port" & Chr(34) & " value=" & Chr(34) & "587" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_DeliveryMethod" & Chr(34) & " value=" & Chr(34) & "Network" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_EnableSsl" & Chr(34) & " value=" & Chr(34) & "True" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Mail_From" & Chr(34) & " value=" & Chr(34) & "Enter SMTPMailFrom" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Use_Default_Credentials" & Chr(34) & " value=" & Chr(34) & "0" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Use_SmtpServer_Or_SmtpClient" & Chr(34) & " value=" & Chr(34) & "1" & Chr(34) & " />
		<add key=" & Chr(34) & "EncryptionKey" & Chr(34) & " value=" & Chr(34) & "EncryptionKey" & Chr(34) & " />
		<add key=" & Chr(34) & "FTP_Address" & Chr(34) & " value=" & Chr(34) & "EnterFTPAddress" & Chr(34) & " />
		<add key=" & Chr(34) & "FTP_UserName" & Chr(34) & " value=" & Chr(34) & "EnterFTPUserName" & Chr(34) & " />
		<add key=" & Chr(34) & "FTP_Password" & Chr(34) & " value=" & Chr(34) & "EnterFTPPassword" & Chr(34) & " />
  </appSettings>
</configuration>
"
            My.Computer.FileSystem.WriteAllText(ApplicationStartupPath & "\" & Application.ProductName & ".exe.config", TextToWrite, 0, System.Text.Encoding.UTF8)
            MagNote_Form.Protect_Application_Configuration_Setting_Btn_Click(MagNote_Form.Protect_Application_Configuration_Setting_Btn, EventArgs.Empty)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
        End Try
    End Function
    Public Function ChangeControlLanguage(sender As Object, Arabic As Boolean) As Boolean
        If Arabic Then
            sender.RightToLeft = RightToLeft.Yes
            'sender.TextAlign = HorizontalAlignment.Right
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(New Globalization.CultureInfo("ar-EG"))
            ApplicationDoEvents(sender.findform)
        Else
            sender.RightToLeft = RightToLeft.No
            'sender.TextAlign = HorizontalAlignment.Left
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(New Globalization.CultureInfo("en-US"))
            ApplicationDoEvents(sender.findform)
        End If
    End Function
    Public Sub RestartWithElevatedPrivileges(ByVal RunMeAsAdministrator As Boolean)
        If RunMeAsAdministrator Then
            MagNote_Form.Run_Me_As_Administrator_ChkBx.CheckState = CheckState.Checked
            MagNote_Form.Save_Note_Form_Parameter_Setting_Btn_Click(MagNote_Form.Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty, 0)
        End If
        Dim programpath As String = New System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath
        Dim arguments As String() = System.Environment.GetCommandLineArgs().Skip(1).ToArray()
        Dim startinfo As New System.Diagnostics.ProcessStartInfo With {
                .FileName = programpath,
                .UseShellExecute = True,
                .Verb = "runas",
                .Arguments = String.Join(" ", arguments)
            }
        If Debugger.IsAttached Then
            Dim UserApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Environment.CurrentDirectory = UserApplicationDataFolder & "\InfoSysMe\MagNote"
            startinfo.WorkingDirectory = UserApplicationDataFolder & "\InfoSysMe\MagNote"
        End If
        System.Diagnostics.Process.Start(startinfo)
        System.Environment.[Exit](0)
    End Sub
    Public ApplicationDoEventsIsRunning
    Public Function ApplicationDoEvents(ByVal CurrentForm As Form, Optional ByVal IgnoreApplicationDoEvents As Boolean = False) As Boolean
        Try
            If ApplicationDoEventsIsRunning Then
                ProcessWithoutDoEvents()
                Exit Function
            End If
            ApplicationDoEventsIsRunning = True
            Dim CurrenentActiveControl = CurrentForm.ActiveControl
            Application.DoEvents()
            Try
                If Not IgnoreApplicationDoEvents And
                    CurrentForm.ActiveControl.Name <> CurrenentActiveControl.Name Then

                    MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").TextAlign = ContentAlignment.MiddleLeft
                    MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = "Return Activeation To (" & CurrenentActiveControl.Name & ")" & vbNewLine & New StackFrame(1).GetMethod().Name.ToString
                    MagNote_Form.MsgBox_SttsStrp.Refresh()

                    CurrentForm.ActiveControl = CurrenentActiveControl
                End If
            Catch ex As Exception
            End Try

        Finally
            ApplicationDoEventsIsRunning = False
        End Try
    End Function
    Public Async Function ProcessWithoutDoEvents() As Task
        'Await Task.Delay(3000) ' wait 3 seconds
        For i As Integer = 0 To 99
            ' Perform some processing
            'Console.WriteLine("Processing: " & i)
            ' Delay briefly to avoid blocking the UI, instead of DoEvents
            Await Task.Delay(10)
        Next
        'Console.WriteLine("Processing Complete.")

    End Function
    Public WhatsAppCallingDelay
    Public Function WaitAhwile()
        While Microsoft.VisualBasic.DateAndTime.Timer <= WhatsAppCallingDelay
            ApplicationDoEvents(MagNote_Form)
        End While
    End Function
    Public Function EncrypAppConfig() As Boolean
        Try
            If Not File.Exists(ApplicationStartupPath & "\" & Application.ProductName & ".exe.config") Then
                CreatAppConfig()
                Msg = "لم تفلح محاولة العثور على ملف اعدادات النظام..."
                Msg &= vbNewLine & "Couldn't Find Configuration File."

                Msg &= vbNewLine & "تم انشاء اخر جديد يحتاج الى اعداد منك"
                Msg &= vbNewLine & "New File Created And Needs To Update From You"

                Msg &= vbNewLine & "سيتم إعادة تشغيل ماجنوت لتفعيل الاعدادات"
                Msg &= vbNewLine & "MagNote Will Be Restarted To Activate The Settings"

                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification)
                If File.Exists(ApplicationStartupPath & "\" & Application.ProductName & ".exe.config") Then
                    System.Diagnostics.Process.Start(Application.ExecutablePath)
                    ApplicationRestart = True
                    Application.Exit()
                    End
                End If
            Else
                Dim keys As String() = ConfigurationManager.AppSettings.AllKeys
                For Each key As String In keys
                    Select Case key
                        Case "SMTP_Mail_User_Name"
                            MagNote_Form.SMTP_Mail_User_Name_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_Mail_User_Password"
                            MagNote_Form.SMTP_Mail_User_Password_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_Host"
                            MagNote_Form.SMTP_Host_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_Port"
                            MagNote_Form.SMTP_Port_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_DeliveryMethod"
                            MagNote_Form.SMTP_Delivery_Method_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_EnableSsl"
                            MagNote_Form.SMTP_EnableSsl_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_Mail_From"
                            MagNote_Form.SMTP_Mail_From_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_Use_Default_Credentials"
                            MagNote_Form.SMTP_Use_Default_Credentials_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "SMTP_Use_SmtpServer_Or_SmtpClient"
                            MagNote_Form.SMTP_Use_SmtpServer_Or_SmtpClient_TxtBx.Text = ConfigurationManager.AppSettings(key).ToString
                        Case "FTP_Address"
                            FTP_Login(0).FTP_Address = ConfigurationManager.AppSettings(key).ToString
                            MagNote_Form.FTP_Address_TxtBx.Text = FTP_Login(0).FTP_Address
                        Case "FTP_UserName"
                            FTP_Login(0).FTP_UserName = ConfigurationManager.AppSettings(key).ToString
                            MagNote_Form.FTP_User_Name_TxtBx.Text = FTP_Login(0).FTP_UserName
                        Case "FTP_Password"
                            FTP_Login(0).FTP_Password = ConfigurationManager.AppSettings(key).ToString
                            MagNote_Form.FTP_Password_TxtBx.Text = FTP_Login(0).FTP_Password
                        Case "EncryptionKey"
                            EncryptionKey = ConfigurationManager.AppSettings("EncryptionKey")
                            MagNote_Form.Encryption_Key_TxtBx.Text = ConfigurationManager.AppSettings("EncryptionKey")
                        Case "Available_SQL_Conn_Strings"
                            MagNote_Form.Available_SQL_Conn_Strings_CmbBx.Text = ConfigurationManager.AppSettings("Available_SQL_Conn_Strings")
                    End Select
                Next
                MagNote_Form.Available_SQL_Conn_Strings_CmbBx.ValueMember = "Key"
                MagNote_Form.Available_SQL_Conn_Strings_CmbBx.DisplayMember = "Value"

                Dim connectionStrings As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
                For Each connectionString As ConnectionStringSettings In connectionStrings
                    'MagNote_Form.Available_SQL_Conn_Strings_CmbBx.Items.Add(connectionString.Name)
                    MagNote_Form.Available_SQL_Conn_Strings_CmbBx.Items.Add(New KeyValuePair(Of String, String)(connectionString.ConnectionString, connectionString.Name))
                Next
                If MagNote_Form.Available_SQL_Conn_Strings_CmbBx.Items.Count > 0 Then
                    MagNote_Form.Available_SQL_Conn_Strings_CmbBx.Enabled = True
                End If

                If IsNothing(EncryptionKey) Then
                    EncryptionKey = DefaultEncryptionKey '"EncryptionKey"
                End If
            End If
            ' Open the current configuration file
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            ' Get the appSettings section
            Dim section As ConfigurationSection = config.GetSection("appSettings")
            If Not section.SectionInformation.IsProtected Then
                ' Protect the section using the DataProtectionConfigurationProvider
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                section.SectionInformation.ForceSave = True
                ' Save the changes
                config.Save(ConfigurationSaveMode.Modified)
            End If

            ' Get the connectionStrings section
            section = config.GetSection("connectionStrings")
            ' Check if the section is already protected
            If Not section.SectionInformation.IsProtected Then
                ' Protect the section using the DataProtectionConfigurationProvider
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                section.SectionInformation.ForceSave = True
                ' Save the changes
                config.Save(ConfigurationSaveMode.Modified)
            End If
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public OpenedExternalFiles(0) As String
    Public MagNoteOpenedByFileOpenedOutSideMageNote
    Public Function RunAsExternal(Optional ByVal MagNotName As String = Nothing) As Boolean
        Try
            If MagNoteOpenedByFileOpenedOutSideMageNote Then
                Return True
            End If
            'For Each Fil In OpenedExternalFiles
            '    If IsNothing(Fil) Then Continue For
            '    If String.IsNullOrEmpty(MagNotName) And
            '        Not IsNothing(RCSN(0)) Then
            '        MagNotName = RCSN(0).Name
            '    Else
            '        MagNotName = String.Empty
            '    End If
            '    If Fil = Replace(MagNotName, "RchTxtBx", "") Then
            '        Return True
            '    End If
            'Next
        Catch ex As Exception
        End Try
    End Function

    Structure FileOpenedExternalDetails
        Public MagNoteName As String
        Public MagNoteText As String
    End Structure
    Public FileOpenedExternal(0) As FileOpenedExternalDetails
    Public Function RefreshFileOpenedExternal(Optional ByVal IsAmendment As Boolean = False)
        Try
            Dim FileOpenedExternalInx = Array.FindIndex(FileOpenedExternal, Function(f) f.MagNoteName = RCSN(0).Name)
            If FileOpenedExternalInx = -1 Then
                If IsNothing(FileOpenedExternal(0).MagNoteName) Then
                    FileOpenedExternal(0).MagNoteName = RCSN(0).Name
                    FileOpenedExternal(0).MagNoteText = RCSN(0).Text
                Else
                    ReDim Preserve FileOpenedExternal(FileOpenedExternal.Length)
                    FileOpenedExternal(FileOpenedExternal.Length - 1).MagNoteName = RCSN(0).Name
                    FileOpenedExternal(FileOpenedExternal.Length - 1).MagNoteText = RCSN(0).Text
                End If
            ElseIf IsAmendment Then
                If RCSN(0).Text <> FileOpenedExternal(FileOpenedExternalInx).MagNoteText Then
                    Return True
                End If
            Else
                RCSN(0).Focus()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function


    ''' <summary>
    ''' RCSN=ReturnCurrentMagNote
    ''' </summary>
    ''' <returns></returns>
    Public Function RCSN(Optional ByVal ShowErrorMessage As Boolean = True) As RichTextBox
        Try
            If MagNote_Form.MagNotes_Notes_TbCntrl.TabPages.Count = 0 Then
                Exit Function
            ElseIf IsNothing(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab) Then
                Exit Function
            ElseIf MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls.Count = 0 Then
                Exit Function
            End If
            Dim SelectedTabTag = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag
            Dim MagNote = FindControlRecursive(New List(Of Control), MagNote_Form.MagNotes_Notes_TbCntrl, New List(Of Type)({GetType(RichTextBox)}),, (SelectedTabTag & "RchTxtBx")).ToList.Item(0)
            'Dim textSize As Size = TextRenderer.MeasureText(MagNote.Text, MagNote.Font)
            'If textSize.Width > MagNote.ClientSize.Width Then
            '    CType(MagNote, RichTextBox).WordWrap = True
            'End If
            Application.DoEvents()
            Return CType(MagNote, RichTextBox)
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg("Function RCSN()" & vbNewLine & ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    'Public Function RefreshMagNoteBaseSetting()
    '    Dim RchTxtBxStyleInx = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN(0).Name)
    '    If RchTxtBxStyleInx = -1 Then
    '        MagNote_Form.Note_Font_Color_ClrCmbBx.Text = Nothing
    '        MagNote_Form.Note_Back_Color_ClrCmbBx.Text = Nothing
    '        MagNote_Form.Note_Font_Color_ClrCmbBx.Text = MagNote_Form.External_Note_Font_Color_ClrCmbBx.Text
    '        MagNote_Form.Note_Back_Color_ClrCmbBx.Text = MagNote_Form.External_Note_Back_Color_ClrCmbBx.Text 'Color.WhiteSmoke.Name.ToString
    '        MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value = 0
    '        MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value = 0
    '        MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value = 0
    '        MagNote_Form.Next_Reminder_Time_DtTmPkr.Value = Now
    '        MagNote_Form.Note_Word_Wrap_ChkBx.CheckState = CheckState.Checked
    '    Else
    '        If Not IsNothing(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab) Then
    '            MagNote_Form.Next_Reminder_Time_DtTmPkr.Value = RchTxtBxStyle(RchTxtBxStyleInx).Next_Reminder_Time
    '            MagNote_Form.Blocked_Note_ChkBx.CheckState = RchTxtBxStyle(RchTxtBxStyleInx).Blocked_Note
    '            MagNote_Form.Finished_Note_ChkBx.CheckState = RchTxtBxStyle(RchTxtBxStyleInx).Finished_Note
    '            MagNote_Form.Secured_Note_ChkBx.CheckState = RchTxtBxStyle(RchTxtBxStyleInx).Secured_Note
    '            MagNote_Form.Use_Main_Password_ChkBx.CheckState = RchTxtBxStyle(RchTxtBxStyleInx).Use_Main_Password
    '            MagNote_Form.Note_Word_Wrap_ChkBx.CheckState = RchTxtBxStyle(RchTxtBxStyleInx).Word_Wrap
    '            RCSN(0).Font = RchTxtBxStyle(RchTxtBxStyleInx).Note_Font
    '            RCSN(0).ForeColor = RchTxtBxStyle(RchTxtBxStyleInx).ForeColor
    '            RCSN(0).BackColor = RchTxtBxStyle(RchTxtBxStyleInx).BackColor

    '            If MagNote_Form.MagNoteIsOpenedExternal Then
    '                MagNote_Form.External_Note_Font_Color_ClrCmbBx.SelectedItem = RchTxtBxStyle(RchTxtBxStyleInx).Note_Font
    '                MagNote_Form.External_Note_Font_Color_ClrCmbBx.SelectedItem = RchTxtBxStyle(RchTxtBxStyleInx).ForeColor
    '                MagNote_Form.External_Note_Back_Color_ClrCmbBx.SelectedItem = RchTxtBxStyle(RchTxtBxStyleInx).BackColor
    '            Else
    '                MagNote_Form.Note_Font_Name_CmbBx.SelectedItem = RchTxtBxStyle(RchTxtBxStyleInx).Note_Font
    '                MagNote_Form.Note_Back_Color_ClrCmbBx.SelectedItem = RchTxtBxStyle(RchTxtBxStyleInx).ForeColor
    '                MagNote_Form.Note_Font_Color_ClrCmbBx.SelectedItem = RchTxtBxStyle(RchTxtBxStyleInx).BackColor
    '            End If
    '        End If
    '    End If
    'End Function
    Private Function Grid_Pnl_Name() As String
        Dim Grid_Pnl As String = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid_Pnl"
        Return Grid_Pnl
    End Function
    Public Function GridPnl(Optional ByVal ShowErrorMessage As Boolean = True) As Panel
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name)
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function

    Private Function RchTxtBx_Name() As String
        Dim RchTxtBx As String = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Name & "RchTxtBx"
        Return RchTxtBx
    End Function
    Public Function RchTxtBx(Optional ByVal ShowErrorMessage As Boolean = True) As RichTextBox
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(RchTxtBx_Name)
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function

    Public Function Grid(Optional ByVal ShowErrorMessage As Boolean = True) As ReoGridControl
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Cell_Border_Color_ClrCmbBx(Optional ByVal ShowErrorMessage As Boolean = True) As ColorsComboBox.ColorsComboBox
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Cell_Border_Color_ClrCmbBx")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Cell_BackColor_ClrCmbBx(Optional ByVal ShowErrorMessage As Boolean = True) As ColorsComboBox.ColorsComboBox
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Cell_BackColor_ClrCmbBx")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Cell_Text_Color_ClrCmbBx(Optional ByVal ShowErrorMessage As Boolean = True) As ColorsComboBox.ColorsComboBox
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Cell_Text_Color_ClrCmbBx")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Font_TlStrp(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStrip
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function SaveAndBorder_TlStrp(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStrip
        Try
            Return MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("SaveAndBorder_TlStrp")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Color_TlStrpLbl(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripLabel
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Color_TlStrpLbl")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Cell_Border_Color_TlStrpLbl(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripLabel
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("SaveAndBorder_TlStrp"), ToolStrip).Items("Cell_Border_Color_TlStrpLbl")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Cell_BackColor_TlStrpLbl(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripLabel
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Cell_BackColor_TlStrpLbl")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function

    Public Function Font_Size_TlStrpCmbBx(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripComboBox
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Font_Size_TlStrpCmbBx")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Font_TlStrpCmbBx(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripComboBox
        Try
            Dim SlctdTab As TabPage = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab
            If SlctdTab.Controls.Find("Grid_Pnl_Name", 1).Count > 0 Then
                Dim Fnt_TlStrp As ToolStrip = CType(SlctdTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip)
                Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Font_TlStrpCmbBx")
            End If
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Bold_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Bold_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Italic_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Italic_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Strike_Through_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Strike_Through_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Under_Line_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Under_Line_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Align_Left_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Align_Left_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Align_Center_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Align_Center_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Align_Right_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Align_Right_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Distributed_Indent_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Distributed_Indent_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Align_Top_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Align_Top_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Align_Middle_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Align_Middle_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Align_Bottom_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Align_Bottom_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Text_Wrap_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("Font_TlStrp"), ToolStrip).Items("Text_Wrap_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Undo_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("SaveAndBorder_TlStrp"), ToolStrip).Items("Undo_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function Redo_TlStrpBtn(Optional ByVal ShowErrorMessage As Boolean = True) As ToolStripButton
        Try
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(Grid_Pnl_Name).Controls("SaveAndBorder_TlStrp"), ToolStrip).Items("Redo_TlStrpBtn")
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function

    ''' <summary>
    ''' RCSN=ReturnCurrentWebBrowser
    ''' </summary>
    ''' <returns></returns>
    Public Function RCWB(Optional ByVal ShowErrorMessage As Boolean = True) As WebBrowser
        Try
            Dim PathName = Path.GetDirectoryName(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag)
            Dim FileName = Path.GetFileNameWithoutExtension(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag)
            Dim FileExtensionName = Path.GetExtension(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag)
            Return CType(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Controls(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag & "WbBrwsr"), WebBrowser)
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function FindStackTrace(FindmethodName) As Boolean
        Try
            Dim StackNo = 0
            Dim st As New StackTrace()
            For Each sf As StackFrame In st.GetFrames()
                Dim method As MethodBase = sf.GetMethod()
                If method.Name.Contains(FindmethodName) Then
                    Return StackNo
                End If
                StackNo += 1
            Next
            Dim x = 1
        Catch ex As Exception
        End Try
    End Function
    Public ShowMsgDialogResult As DialogResult
    Public Function ShowMsg(ByVal BoxMsg As String,
                            Optional ByVal Title As String = "InfoSysMe (MagNote)",
                            Optional ByVal MBBs As MessageBoxButtons = MessageBoxButtons.OK,
                            Optional ByVal MBI As MessageBoxIcon = MessageBoxIcon.Information,
                            Optional ByVal MBDB As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1,
                            Optional ByVal MOB As MessageBoxOptions = Nothing,
                            Optional ByVal Fls As Boolean = False,
                            Optional ByVal ShowMe As Boolean = True,
                            Optional ByVal TimeToSpendVisible As Integer = 0,
                            Optional ByVal WithSound As Boolean = True,
                            Optional ByVal Withtimer As Boolean = True,
                            Optional ByVal Show_Windows_Notification As Boolean = True,
                            Optional ByVal ForceDisplayWindowsNotification As Boolean = False,
                            Optional ByVal DoMakeTopMost As Boolean = True,
                            Optional ByVal MyForm As Form = Nothing) As DialogResult
        Try
            If MagNote_Form.Language_Btn.Text = "ع" Then
                BoxMsg = CapitalFirstLetter(BoxMsg)
            End If
            If IsNothing(MyForm) Then
                Dim stackFrame As New StackFrame(1)
                Dim method As Reflection.MethodBase = stackFrame.GetMethod()
                If method.DeclaringType IsNot Nothing Then
                    If Microsoft.VisualBasic.Right(method.DeclaringType.Name, 5) = "_Form" Then
                        For Each Frm In Application.OpenForms
                            If Frm.name = method.DeclaringType.Name Then
                                MyForm = Frm
                                Exit For
                            End If
                        Next
                    End If
                End If
                If IsNothing(MyForm) Then
                    MyForm = MagNote_Form
                End If
                Try
                    If Application_Initializing_Form.Visible Then
                        MyForm = Application_Initializing_Form
                    ElseIf CType(Find_Form, Form).Visible Then
                        MyForm = Find_Form
                    End If
                Catch ex As Exception
                End Try
            End If
            ShowMsgDialogResult = DialogResult.None
            If systemShutdown Then
                TimeToSpendVisible = 1
                Show_Windows_Notification = False
                ShowMe = False
                DoMakeTopMost = False
                Select Case MBBs
                    Case MessageBoxButtons.AbortRetryIgnore
                    Case MessageBoxButtons.OKCancel
                    Case MessageBoxButtons.RetryCancel
                    Case MessageBoxButtons.YesNo
                        If MBDB = MessageBoxDefaultButton.Button1 Then
                            Return DialogResult.Yes
                        Else
                            Return DialogResult.No
                        End If
                    Case MessageBoxButtons.YesNoCancel
                End Select
            End If
            If TimeToSpendVisible = 0 Then
                TimeToSpendVisible = 30
            End If
            If Not IsNothing(MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl")) Then
                MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").TextAlign = ContentAlignment.MiddleLeft
                'MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = BoxMsg
                Dim NS = New StackFrame(1).GetMethod().Name.ToString
                UpdateStatus(BoxMsg, "Current Method Name (" & NS & ")")
                MagNote_Form.MsgBox_SttsStrp.Refresh()
            End If
            If WithSound Then PlaySnd("Command.wav")
            If ForceDisplayWindowsNotification Then
                ShowWindowsNotification(BoxMsg, ForceDisplayWindowsNotification)
                If Not ShowMe Then
                    Exit Function
                End If
            ElseIf Not ShowMe And Show_Windows_Notification Then
                Exit Function
            ElseIf Not ShowMe Then
                Exit Function
            End If
            MessageBoxTimer = New Threading.Thread(AddressOf closeMessageBox)
            If Withtimer Then
                MessageBoxTimer.Start(TimeToSpendVisible)
            End If
            MOB = MBOs()
            ShowMsgDialogResult = MessageBox.Show(MyForm, BoxMsg, "Current Method Name (" & New StackFrame(1).GetMethod().Name.ToString & ")", MBBs, MBI, MBDB, MOB)
            Return ShowMsgDialogResult
        Catch ex As Exception
        Finally
            If Not IsNothing(MyForm) Then
                MessageBoxTimer.Abort()
                Try
                    If MyForm.WindowState <> FormWindowState.Minimized Then
                        If DoMakeTopMost Then
                            MyForm.Activate()
                        End If
                    End If
                Catch ex As Exception
                End Try
                If MagNote_Form.Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked Then
                    MagNote_Form.TopMost = False
                End If
                If DoMakeTopMost Then
                    MakeTopMost(ShowMe, MyForm)
                End If
            End If
        End Try
    End Function

    Dim item As New ToolStripMenuItem()
    Private Sub UpdateStatus(newStatus As String, MEthodNAme As String)
        Try
            If String.IsNullOrEmpty(newStatus) Then
                Exit Sub
            End If
            newStatus = MEthodNAme & vbNewLine & newStatus
            newStatus &= vbNewLine & "Message Time: " & Now.ToString
            ' Display it in a ToolStripStatusLabel or main button
            MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = newStatus

            ' Add it to dropdown history
            item = New ToolStripMenuItem(newStatus)
            AddHandler item.Click, Sub() MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = item.Text

            'CType(MagNote_Form.MsgBox_SttsStrp.Items(1), ToolStripDropDownButton).DropDownItems.Insert(0, item)
            If MagNote_Form.MsgBox_SttsStrp.Items.Count > 1 Then
                If MagNote_Form.InvokeRequired Then
                    MagNote_Form.Invoke(Sub()
                                            Dim ddButton = TryCast(MagNote_Form.MsgBox_SttsStrp.Items(1), ToolStripDropDownButton)
                                            If ddButton IsNot Nothing Then
                                                ddButton.DropDownItems.Insert(0, item)
                                            End If
                                        End Sub)
                Else
                    Dim ddButton = TryCast(MagNote_Form.MsgBox_SttsStrp.Items(1), ToolStripDropDownButton)
                    If ddButton IsNot Nothing Then
                        ddButton.DropDownItems.Insert(0, item)
                    End If
                End If
            Else
                ' Optional: handle the case when Items(1) doesn't exist
                'MessageBox.Show("DropDown button not available in MsgBox_SttsStrp.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function IsConnectedToInternet() As Boolean
        Try
            MagNote_Form.Cursor = Cursors.WaitCursor
            Using ping As New Ping()
                Dim reply As PingReply = ping.Send("8.8.8.8", 3000) ' Google's DNS, very reliable
                If reply.Status = IPStatus.Success Then
                    Return True
                Else
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "لا يوجد اتصال مباشر ناجح مع الشبكة العنكبوتية الدولية"
                    Else
                        Msg = "You Don't Have A Direct Succeeded Connection To To Internet"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    Return False
                End If
            End Using
        Catch ex As Exception
            Return False
        Finally
            MagNote_Form.Cursor = Cursors.Default
        End Try
    End Function
    Public Sub Btn_Click(sender As Object, e As EventArgs)
        Try
            sender.findform.activate
        Catch ex As Exception
        End Try
    End Sub

    Public Function PlaySnd(ByVal Sound As String) As Boolean
        Try
            sndPlaySound(Sound, SND_ASYNC)
        Catch ex As Exception
        End Try
    End Function
    Public Function MBOs() As MessageBoxOptions
        Dim MBO As MessageBoxOptions
        If MagNote_Form.Language_Btn.Text = "E" Then
            'MBO = MessageBoxOptions.ServiceNotification Or
            '            MessageBoxOptions.RightAlign Or
            '            MessageBoxOptions.RtlReading
            MBO = MessageBoxOptions.RightAlign
            Return MBO
        Else
            Return Nothing 'MessageBoxOptions.ServiceNotification
        End If
    End Function
    Public Sub closeMessageBox(ByVal delay As Object)
        Threading.Thread.Sleep(CInt(delay) * 1000)
        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0)
        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0)
    End Sub

#Region "Encrypt Decrypt Data"
    Function Encrypt(ByVal plainText As String, ByVal bytKey As Byte(), ByVal bytIV As Byte(), Optional ByVal ShowErrorMessage As Boolean = True) As String
        Try
            Dim cipher As New RijndaelManaged
            Dim encryptor As ICryptoTransform = cipher.CreateEncryptor(bytKey, bytIV)
            Dim data As Byte() = Encoding.Unicode.GetBytes(plainText)
            cipher.Padding = PaddingMode.PKCS7
            Return Convert.ToBase64String(encryptor.TransformFinalBlock(data, 0, data.Length))
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, ShowErrorMessage)
        End Try
    End Function
    Function Decrypt(ByVal encryptedText As String, ByVal key As Byte(), ByVal iv As Byte(), Optional ByVal ShowErrorMessage As Boolean = True) As String
        Try
            Dim cipher As New RijndaelManaged
            Dim decryptor As ICryptoTransform = cipher.CreateDecryptor(key, iv)
            Dim data As Byte() = Convert.FromBase64String(encryptedText)
            cipher.Padding = PaddingMode.PKCS7

            Return Encoding.Unicode.GetString(decryptor.TransformFinalBlock(data, 0, data.Length))
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "My Be The EncryptionKey In Application Configuration File Is Not Correct"
            Else
                Msg = "
ربما مفتاح التشفير المسجل فى ملف تكوين التطبيق غير صحيح"
            End If
            Msg &= vbNewLine & ApplicationStartupPath & "\" & Application.ProductName & ".exe.config"
            ShowMsg(Msg & vbNewLine & vbNewLine & ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, ShowErrorMessage)
        End Try
    End Function
    Public Function Encrypt_Function(Optional ByVal EncryptTextBox As String = Nothing,
                                     Optional ByVal EncryptKey As String = Nothing,
                                     Optional ByVal ShowErrorMessage As Boolean = True,
                                     Optional ByVal IgnoreUseDefaultEncryptionKey As Boolean = False) As String
        Try
            If MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState = CheckState.Checked And
                Not IgnoreUseDefaultEncryptionKey Then
                EncryptKey = "EncryptionKey"
            End If
            Dim bytKey As Byte()
            Dim bytIV As Byte()
            If IsNothing(EncryptKey) Then
                EncryptKey = EncryptionKey
                'EncryptKey = "EncryptionKey"
            End If
            bytKey = CreateKey(EncryptKey)
            'Send the password to the CreateIV function.
            bytIV = CreateIV(EncryptKey)
            Return Encrypt(EncryptTextBox, bytKey, bytIV)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, ShowErrorMessage)
        End Try
    End Function
    Public Function Decrypt_Function(Optional ByVal DecryptTextBox As String = Nothing,
                                     Optional ByVal EncryptKey As String = Nothing,
                                     Optional ByVal ShowErrorMessage As Boolean = True,
                                     Optional ByVal IgnoreUseDefaultEncryptionKey As Boolean = False) As String
        Try
            'Debugger.Launch()
            If MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState = CheckState.Checked And Not IgnoreUseDefaultEncryptionKey Then
                EncryptKey = "EncryptionKey"
            End If
            Dim mthd = New StackFrame(1).GetMethod().Name.ToString
            If New StackFrame(1).GetMethod().Name.ToString = "ReadFile" And
                FileOpenedByOpenNoteTlStrpBtn And
                MagNote_Form.Encryption_Key_Open_File_TxtBx.TextLength > 0 Then
                EncryptKey = MagNote_Form.Encryption_Key_Open_File_TxtBx.Text
            End If
            If String.IsNullOrEmpty(DecryptTextBox) Then
                Return Nothing
            End If
            Dim bytKey As Byte()
            Dim bytIV As Byte()
            If IsNothing(EncryptKey) Then
                EncryptKey = EncryptionKey
            End If
            'Send the password to the CreateKey function.
            bytKey = CreateKey(EncryptKey)
            'Send the password to the CreateIV function.
            bytIV = CreateIV(EncryptKey)
            Dim Password = Decrypt(DecryptTextBox, bytKey, bytIV, ShowErrorMessage)
            Return Password
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, ShowErrorMessage)
            Return Nothing
        End Try
    End Function
    Public Function CreateKey(ByVal strPassword As String) As Byte()
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = strPassword.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte
        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next
        'Declare what hash to use.
        Dim SHA512 As New System.Security.Cryptography.SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
        'Declare bytKey(31).  It will hold 256 bits.
        Dim bytKey(31) As Byte
        'Use For Next to put a specific size (256 bits) of 
        'bytResult into bytKey. The 0 To 31 will put the first 256 bits
        'of 512 bits into bytKey.
        For i As Integer = 0 To 31
            bytKey(i) = bytResult(i)
        Next
        Return bytKey 'Return the key.
    End Function
    Public Function CreateIV(ByVal strPassword As String) As Byte()
        'Convert strPassword to an array and store in chrData.
        Dim chrData() As Char = strPassword.ToCharArray
        'Use intLength to get strPassword size.
        Dim intLength As Integer = chrData.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim bytDataToHash(intLength) As Byte
        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To chrData.GetUpperBound(0)
            bytDataToHash(i) = CByte(Asc(chrData(i)))
        Next
        'Declare what hash to use.
        Dim SHA512 As New System.Security.Cryptography.SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
        'Declare bytIV(15).  It will hold 128 bits.
        Dim bytIV(15) As Byte
        'Use For Next to put a specific size (128 bits) of 
        'bytResult into bytIV. The 0 To 30 for bytKey used the first 256 bits.
        'of the hashed password. The 32 To 47 will put the next 128 bits into bytIV.
        For i As Integer = 32 To 47
            bytIV(i - 32) = bytResult(i)
        Next
        Return bytIV 'return the IV
    End Function
#End Region
    Public IgnoreNoteAmendmented As Boolean
    Public ExitingProgram As Boolean
    Public DefaultRCSN As String
    Public Function NoteAmendmented(Optional ByVal NoteName As String = Nothing, Optional ByVal WindowsShuttingDown As Boolean = False) As DialogResult
        Dim CurrentTpBg
        Try
            'Debugger.Launch()
            Dim ErrorReason As String = String.Empty
            If Not IsNothing(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab) Then
                CurrentTpBg = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab
            End If
            Dim CurentRCSN
            For Each TbPg In MagNote_Form.MagNotes_Notes_TbCntrl.TabPages

                If Not String.IsNullOrEmpty(NoteName) Then
                    If TbPg.name <> NoteName Then
                        Continue For
                    End If
                End If

                'MagNote_Form.ActiveControl = MagNote_Form.MagNotes_Notes_TbCntrl
                'MagNote_Form.ActiveControl.Focus()
                'MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab = TbPg
                If MagNote_Form.MagNote_No_CmbBx.SelectedIndex = -1 Then
                    MagNote_Form.IsInMagNoteCmbBx(TbPg.Name, 1)
                ElseIf DirectCast(MagNote_Form.MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key <> TbPg.name Then
                    MagNote_Form.IsInMagNoteCmbBx(TbPg.Name, 1)
                End If
                If MagNote_Form.MagNote_No_CmbBx.SelectedIndex = -1 Then
                    Continue For
                End If

                CurentRCSN = RCSN(0)
                If IsNothing(CurentRCSN) Then
                    Continue For
                End If
                Dim AryInx = Array.FindIndex(MagNoteRTF, Function(f) f.Name = CurentRCSN.Name)
                If AryInx = -1 Then
                    Continue For
                End If
                Dim rtf1 = CType(MagNoteRTF(AryInx), RichTextBox).Rtf
                Dim rtf2 = CurentRCSN.Rtf
                Dim txt1 = CType(MagNoteRTF(AryInx), RichTextBox).Text
                Dim txt2 = CurentRCSN.Text
                If CurentRCSN.TextLength = 0 And
                    Replace(CurentRCSN.Name, "RchTxtBx", "") = DefaultRCSN Then
                    Continue For
                End If
                If MagNote_Form.MagNoteIsOpenedExternal() And
                    Not MagNote_Form.MagNoteFileFormat(, 1, 0) Then
                    If RefreshFileOpenedExternal(1) Then
                        GoTo ExternalNoteNotSaved
                    Else
                        Continue For
                    End If
                ElseIf Not File.Exists(TbPg.name) And
                           systemShutdown Then
                    GoTo ExternalNoteNotSaved
                ElseIf Not File.Exists(TbPg.name) And New StackFrame(1).GetMethod().Name.ToString = "Exit_Application_Click" Then
                    GoTo NewNoteNotSaved
                End If
                Dim IfIsTrue As Boolean = False
                Dim SelectedIndex = MagNote_Form.MagNote_No_CmbBx.SelectedIndex
                Dim TbCntrlTabPagesCount = MagNote_Form.MagNotes_Notes_TbCntrl.TabPages.Count
                Dim MyCurrentDGVRow As String = String.Empty
                If MagNote_Form.MagNote_No_CmbBx.SelectedIndex <> -1 Then
                    If Not File.Exists(DirectCast(MagNote_Form.MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) And CurentRCSN.TextLength > 0 Then
                        If systemShutdown Then
                            MagNote_Form.Save_Note_TlStrpBtn.PerformClick()
                            Continue For
                        Else
                            GoTo NewNoteNotSaved
                        End If
                    End If
                End If
                If IsNothing(MagNote_Form.CurrentDGVRow) Then
                    MyCurrentDGVRow = Nothing
                End If
                If (IgnoreNoteAmendmented Or
                    IsNothing(CurentRCSN) Or
                    SelectedIndex = -1 Or
                    TbCntrlTabPagesCount = 0 Or
                    IsNothing(MyCurrentDGVRow)) And Not RunAsExternal() Then
                    Continue For
                End If
                Dim RCSN_Text = Replace(Replace(CurentRCSN.Text, vbLf, vbNewLine), vbCrLf, vbNewLine)
                If IsNothing(RCSN_Text) And MagNote_Form.Secured_Note_ChkBx.CheckState = CheckState.Checked Then
                    Continue For
                End If
                If (CurentRCSN.TextLength = 0 Or
                    MagNote_Form.MagNoteIsOpenedExternal) And
                    isInDataGridView(NoteName, "MagNote_Name", MagNote_Form.Available_MagNotes_DGV, 0,,, 1) Then
                    If Not MagNote_Form.MagNoteFileFormat(NoteName, 1, 0) Then
                        If String.IsNullOrEmpty(NoteName) Then
                            Continue For
                        Else
                            Dim SlctdIndx = DirectCast(MagNote_Form.File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                            Dim OldText = My.Computer.FileSystem.ReadAllText(NoteName, System.Text.Encoding.UTF8)
                            Dim ReadedFile As New RichTextBox
                            If SlctdIndx = "rtf" Then
                                ReadedFile.Rtf = OldText
                                OldText = ReadedFile.Text
                            End If
                            If OldText <> RCSN_Text Then
                                ErrorReason &= "OldText <> RCSN_Text"
                                ErrorReason &= vbNewLine & "-------------------------Old_Text-------------------------------"
                                ErrorReason &= vbNewLine & "OldText = " & OldText
                                ErrorReason &= vbNewLine & "-------------------------RCSN_Text-------------------------------"
                                ErrorReason &= vbNewLine & RCSN_Text
                                IfIsTrue = True
                                GoTo ExternalNoteNotSaved
                            Else
                                Continue For
                            End If
                        End If
                    End If
                End If

                Dim SNNCmbBxSelectedIndex = MagNote_Form.MagNote_No_CmbBx.SelectedIndex
                Dim SNNCmbBxText = MagNote_Form.MagNote_No_CmbBx.Text
                Dim SPTxtBxText = MagNote_Form.Note_Password_TxtBx.Text
                Application.DoEvents()
                If IsNothing(MagNote_Form.CurrentDGVRow) Then
                    AryInx = -1
                    GoTo NewNoteNotSaved
                End If
                'don't delete This Paragraph
                '--------------------------------------------------------
                'Dim inx = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN(0).Name)
                'If inx <> -1 Then
                '    If RchTxtBxStyle(inx).Finished_Note <> MagNote_Form.Finished_Note_ChkBx.CheckState Then
                '        ErrorReason &= vbNewLine & "MagNote_Form.Finished_Note_ChkBx.CheckState <> RchTxtBxStyle(inx).Finished_Note"
                '        ErrorReason &= vbNewLine & MagNote_Form.Finished_Note_ChkBx.CheckState
                '        ErrorReason &= vbNewLine & RchTxtBxStyle(inx).Finished_Note
                '    End If
                'End If
                '--------------------------------------------------------
                Dim CrntRow As New DataGridViewRow
                CrntRow = MagNote_Form.CurrentDGVRow
                If CrntRow.Index = -1 Then
                    If Not IsNothing(MagNote_Form.Available_MagNotes_DGV.CurrentRow) Then
                        If MagNote_Form.Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Name").Value = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Name Then
                            MagNote_Form.CurrentDGVRow = MagNote_Form.Available_MagNotes_DGV.CurrentRow
                        End If
                    End If
                End If
                Dim NRTdt1 As DateTime = DateTime.ParseExact(MagNote_Form.CurrentDGVRow.Cells("Next_Reminder_Time").Value, "yyyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)
                Dim NRTToString = MagNote_Form.Next_Reminder_Time_DtTmPkr.Value.ToString
                Dim NRTdt2 As DateTime = DateTime.Parse(NRTToString)
                If MagNote_Form.CurrentDGVRow.Index <> -1 Then
                    If MagNote_Form.Blocked_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Blocked_Note").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Blocked_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Blocked_Note).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Blocked_Note_ChkBx.CheckState
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Blocked_Note").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Finished_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Finished_Note").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Finished_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Finished_Note).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Finished_Note_ChkBx.CheckState
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Finished_Note").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Secured_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Secured_Note").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Secured_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Secured_Note).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Secured_Note_ChkBx.CheckState
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Secured_Note").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Use_Main_Password_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Use_Main_Password").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Use_Main_Password_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Use_Main_Password).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Use_Main_Password_ChkBx.CheckState
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Use_Main_Password").Value
                        IfIsTrue = True
                    ElseIf (MagNote_Form.Note_Password_TxtBx.TextLength > 0 And Encrypt_Function(MagNote_Form.Note_Password_TxtBx.Text) <> MagNote_Form.CurrentDGVRow.Cells("Note_Password").Value) Then
                        ErrorReason &= vbNewLine & "(MagNote_Form.Note_Password_TxtBx.TextLength > 0 And Decrypt_Function(MagNote_Form.Note_Password_TxtBx.Text) <> MagNote_Form.CurrentDGVRow.Cells(Note_Password).Value)"
                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Word_Wrap_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Note_Word_Wrap").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Word_Wrap_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Note_Word_Wrap).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Note_Word_Wrap_ChkBx.CheckState
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Note_Word_Wrap").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Have_Reminder_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Note_Have_Reminder").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Have_Reminder_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Note_Have_Reminder).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Note_Have_Reminder_ChkBx.CheckState
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Note_Have_Reminder").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Font_TxtBx.Text <> MagNote_Form.CurrentDGVRow.Cells("Note_Font").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Font_TxtBx.Text <> MagNote_Form.CurrentDGVRow.Cells(Note_Font).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Note_Font_TxtBx.Text
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Note_Font").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Font_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells("Note_Font_Color").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Font_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells(Note_Font_Color).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Note_Font_Color_ClrCmbBx.Text
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Note_Font_Color").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Back_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells("Note_Back_Color").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Back_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells(Note_Back_Color).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Note_Back_Color_ClrCmbBx.Text
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Note_Back_Color").Value
                        IfIsTrue = True
                    ElseIf NRTdt1 <> NRTdt2 Then
                        Dim DtTmPkr = MagNote_Form.Next_Reminder_Time_DtTmPkr.Text
                        Dim NRT = MagNote_Form.CurrentDGVRow.Cells("Next_Reminder_Time").Value
                        ErrorReason &= vbNewLine & "MagNote_Form.Next_Reminder_Time_DtTmPkr.Text <> MagNote_Form.CurrentDGVRow.Cells(Next_Reminder_Time).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Next_Reminder_Time_DtTmPkr.Value.ToString
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Next_Reminder_Time").Value
                        IfIsTrue = True
                    ElseIf MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value <> MagNote_Form.CurrentDGVRow.Cells("Reminder_Every").Value Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & , & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & , & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value <> MagNote_Form.CurrentDGVRow.Cells(Reminder_Every).Value"
                        ErrorReason &= vbNewLine & MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value
                        ErrorReason &= vbNewLine & MagNote_Form.CurrentDGVRow.Cells("Reminder_Every").Value
                        IfIsTrue = True
                    End If
                Else
                    Exit Function
                End If
NewNoteNotSaved:
                If AryInx = -1 Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "هذه الماجنوت (الملف) توجد مشكلة فى التحقق من وجود تعديل تم علها من عدمة ... هل تريد الاستمرار؟"
                    Else
                        Msg = "There Is A Problem With This Note (File) In Verifying Whether It Has Been Modified Or Not, Do You Want  Continue?"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & vbNewLine & ErrorReason & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                    If MyDialogResult = DialogResult.Yes Then
                        Continue For
                    Else
                        Return MyDialogResult
                    End If
                End If
                If IfIsTrue Or
                    (txt1 <> txt2 And
                    Not IsNothing(MagNoteRTF)) Then
ExternalNoteNotSaved:
                    If rtf1 <> rtf2 Then
                        ErrorReason &= vbNewLine & "CType(MagNoteRTF(AryInx), RichTextBox).Rtf <> RCSN.Rtf"
                    End If
                    If txt1 <> txt2 Then
                        ErrorReason &= vbNewLine & "CType(MagNoteRTF(AryInx), RichTextBox).Text <> RCSN.Text"
                    End If
                    If systemShutdown And
                        txt1 <> txt2 Then
                        MagNote_Form.Warning_Before_Save_ChkBx.CheckState = CheckState.Unchecked
                        MagNote_Form.Save_Note_TlStrpBtn.PerformClick()
                    Else
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Msg = "هذه الماجنوت (الملف) تم اجراء تعديل عليها ولم يتم الحفظ بعد ... هل تريد حفظ الماجنوت والاستمرار؟"
                        Else
                            Msg = "This Note (File) Already Amendment, Do You Want To Save The Note And Continue?"
                        End If
                        Dim MyDialogResult = ShowMsg(Msg & vbNewLine & ErrorReason & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        If MyDialogResult = DialogResult.Yes Then
                            MagNote_Form.Save_Note_TlStrpBtn.PerformClick()
                            If Not String.IsNullOrEmpty(NoteName) Then
                                Return DialogResult.Yes
                            Else
                                Continue For
                            End If
                        ElseIf MyDialogResult = DialogResult.No Then
                            If Not String.IsNullOrEmpty(NoteName) Then
                                Return DialogResult.Yes
                            Else
                                Continue For
                            End If
                        ElseIf MyDialogResult = DialogResult.Cancel Then
                            Return DialogResult.Cancel
                        End If
                    End If
                End If
            Next
            IgnoreNoteAmendmented = False
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Dim ActvCntrl = MagNote_Form.ActiveControl
            MagNote_Form.ActiveControl = MagNote_Form.MagNotes_Notes_TbCntrl
            MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab = CurrentTpBg
            MagNote_Form.ActiveControl = ActvCntrl
            ShowMsgDialogResult = DialogResult.None
        End Try
    End Function
    Public Function ReturnRCSNName() As Boolean
        If RCSN(0).Name.Contains("MagNote -(287)-") Then
            Dim x = 1
        End If

    End Function
    Public Function AddMagNoteRTF(Optional ByVal UpdateMagNoteRTF As Boolean = False) As Boolean
        Dim RCSN1 = RCSN()
        MagNote_Form.Cursor = Cursors.WaitCursor
        Try
            If Not IsNothing(MagNoteRTF) Then
                If MagNoteRTF.Count = 1 And
                IsNothing(MagNoteRTF(0)) Then
                    MagNoteRTF = Nothing
                End If
            End If
            MagNote_Form.Cursor = Cursors.WaitCursor
            Dim RCSNName = RCSN1.Name
            If IsNothing(MagNoteRTF) Then
                ReDim MagNoteRTF(0)
                MagNoteRTF(0) = New RichTextBox
                MagNoteRTF(0).Name = RCSN1.Name
                MagNoteRTF(0).WordWrap = RCSN1.WordWrap
                MagNoteRTF(0).ForeColor = Color.FromName(RCSN1.ForeColor.Name)
                MagNoteRTF(0).BackColor = Color.FromName(RCSN1.BackColor.Name)
                MagNoteRTF(0).Rtf = RCSN1.Rtf
                MagNoteRTF(0).Font = RCSN1.Font
                FillRchTxtBxStyle()
            ElseIf Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSNName) = -1 Then
                ReDim Preserve MagNoteRTF(MagNoteRTF.Length)
                MagNoteRTF(MagNoteRTF.Length - 1) = New RichTextBox
                MagNoteRTF(MagNoteRTF.Length - 1).Name = RCSN1.Name
                MagNoteRTF(MagNoteRTF.Length - 1).WordWrap = RCSN1.WordWrap
                MagNoteRTF(MagNoteRTF.Length - 1).ForeColor = Color.FromName(RCSN1.ForeColor.Name)
                MagNoteRTF(MagNoteRTF.Length - 1).BackColor = Color.FromName(RCSN1.BackColor.Name)
                MagNoteRTF(MagNoteRTF.Length - 1).Rtf = RCSN1.Rtf
                MagNoteRTF(MagNoteRTF.Length - 1).Font = RCSN1.Font
                FillRchTxtBxStyle()
            ElseIf Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSNName) <> -1 Then
                Dim MagNoteRTFNo = Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSNName)
                If MagNoteRTFNo <> -1 Then
                    MagNoteRTF(MagNoteRTFNo).Name = RCSN1.Name
                    MagNoteRTF(MagNoteRTFNo).WordWrap = RCSN1.WordWrap
                    MagNoteRTF(MagNoteRTFNo).ForeColor = Color.FromName(RCSN1.ForeColor.Name)
                    MagNoteRTF(MagNoteRTFNo).BackColor = Color.FromName(RCSN1.BackColor.Name)
                    MagNoteRTF(MagNoteRTFNo).Rtf = RCSN1.Rtf
                    MagNoteRTF(MagNoteRTFNo).Font = RCSN1.Font
                    FillRchTxtBxStyle()
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MagNote_Form.Cursor = Cursors.Default
        End Try
    End Function
    Public Function FillRchTxtBxStyle() As Boolean
        Try
            'Debugger.Launch()
            Dim RCSNName = RCSN.Name
            Dim inx = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSNName)
            Dim RchTxtBxStyleNo
            If inx = -1 And
               IsNothing(RchTxtBxStyle(0).RchTxtBx_Name) Then
                RchTxtBxStyleNo = 0
            ElseIf inx = -1 Then
                ReDim Preserve RchTxtBxStyle(RchTxtBxStyle.Length)
                RchTxtBxStyleNo = RchTxtBxStyle.Length - 1
            ElseIf inx <> -1 Then
                RchTxtBxStyleNo = inx
            End If
            If String.IsNullOrEmpty(RchTxtBxStyleNo) Then
                Exit Function
            End If
            RchTxtBxStyle(RchTxtBxStyleNo).RchTxtBx_Name = RCSNName
            If Microsoft.VisualBasic.Right(RCSNName, 8) = "RchTxtBx" Then
                RCSNName = Microsoft.VisualBasic.Left(RCSNName, RCSNName.Length - 8)
            End If
            If MagNote_Form.MagNoteFileFormat(RCSNName, 1, 0) Then
                RchTxtBxStyle(RchTxtBxStyleNo).Reminder_Every = MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value
                RchTxtBxStyle(RchTxtBxStyleNo).Next_Reminder_Time = MagNote_Form.Next_Reminder_Time_DtTmPkr.Value
                RchTxtBxStyle(RchTxtBxStyleNo).Blocked_Note = MagNote_Form.Blocked_Note_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Finished_Note = MagNote_Form.Finished_Note_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Secured_Note = MagNote_Form.Secured_Note_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Font = MagNote_Form.Note_Font_Name_CmbBx.SelectedItem ' stfnt
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = MagNote_Form.Note_Have_Reminder_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = MagNote_Form.Note_Have_Reminder_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Password = MagNote_Form.Note_Password_TxtBx.Text
                RchTxtBxStyle(RchTxtBxStyleNo).Use_Main_Password = MagNote_Form.Use_Main_Password_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Word_Wrap = MagNote_Form.Note_Word_Wrap_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).ForeColor = MagNote_Form.Note_Font_Color_ClrCmbBx.SelectedItem 'Text
                RchTxtBxStyle(RchTxtBxStyleNo).BackColor = MagNote_Form.Note_Back_Color_ClrCmbBx.SelectedItem 'Text
            Else
                RchTxtBxStyle(RchTxtBxStyleNo).Reminder_Every = "0,0,0"
                RchTxtBxStyle(RchTxtBxStyleNo).Next_Reminder_Time = Now
                RchTxtBxStyle(RchTxtBxStyleNo).Blocked_Note = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Finished_Note = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Secured_Note = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Font = MagNote_Form.External_Note_Font_Name_CmbBx.SelectedItem 'stfnt
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Password = Nothing
                RchTxtBxStyle(RchTxtBxStyleNo).Use_Main_Password = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Word_Wrap = CheckState.Checked
                RchTxtBxStyle(RchTxtBxStyleNo).ForeColor = MagNote_Form.External_Note_Font_Color_ClrCmbBx.SelectedItem 'Text
                RchTxtBxStyle(RchTxtBxStyleNo).BackColor = MagNote_Form.External_Note_Back_Color_ClrCmbBx.SelectedItem 'Text
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Function CurrentMagNote() As String
        Try
            If MagNote_Form.MagNotes_Notes_TbCntrl.SelectedIndex = -1 Then Exit Function
            Dim CSI As String = vbNewLine & "----------------------------------------"
            If MagNote_Form.Language_Btn.Text = "E" Then
                CSI &= vbNewLine & "عنوان الماجنوت الحالية (" & MagNote_Form.MagNote_No_CmbBx.Text & ")"
                CSI &= vbNewLine & "إسم ملف الماجنوت الحالية (" & MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Name & ")"
            Else
                CSI &= vbNewLine & "Current Note Label (" & MagNote_Form.MagNote_No_CmbBx.Text & ")"
                CSI &= vbNewLine & "Current Note File Name (" & MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Name & ")"
            End If
            Return CSI
        Catch ex As Exception
        End Try
    End Function

    Public Function ProcessRunning(ByVal strName As String, Optional ByVal ClosePrcss As Boolean = False) As Boolean
        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~'
        '               Check if exe exist in process list                           '
        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~'
        Try
            ProcessRunning = False 'default variable value
            Dim clsProcess As New Process   'create new instance of class process
            For Each clsProcess In Process.GetProcesses 'list all the processes
                If clsProcess.ProcessName = strName Then    'compare the process name with the name we give
                    If ClosePrcss Then
                        clsProcess.Kill()
                    Else
                        Return (True)
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Dim ni As NotifyIcon = New NotifyIcon()
    Public Sub ShowWindowsNotification(ByVal Text As String, Optional ForceDisplayWindowsNotification As Boolean = False)
        If MagNote_Form.Activate_Windows_Notification_Tray_ChkBx.CheckState = CheckState.Unchecked And
            Not ForceDisplayWindowsNotification Then
            Exit Sub
        End If
        ni = New NotifyIcon()
        Try
            ni.BalloonTipTitle = "test"
            ni.Visible = True
            ni.Icon = My.Resources.Guillendesign_Variations_3_Notepad
            ni.ShowBalloonTip(1000, "InfoSysMe_MagNote", Text, ToolTipIcon.Info)
        Finally
            ni.Icon = Nothing
            ni.Dispose()
            Application.DoEvents()
        End Try
    End Sub

#Region "Move Form"
    Public CntrlMouseDown As Boolean
    Public Declare Function ReleaseCapture Lib "user32" () As Integer
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Integer) As Integer
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HTCAPTION As Short = 2
    Public BackupTime As DateTime = Now
    Public Function MoveForm(ByVal Frm As Object) As Boolean
        ReleaseCapture()
        SendMessage(Frm.Handle.ToInt32, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Function

    Public Sub Control_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If sender.Name.Contains("_Spliter") Then
            Exit Sub
        End If
        If CntrlMouseDown And
            (Control_Y <> e.Y Or
            Control_X <> e.X) Then
            If CType(sender, Control).Cursor = Cursors.SizeWE Or
                CType(sender, Control).Cursor = Cursors.IBeam Then
                Exit Sub
            End If
            Application.DoEvents()
            Dim CellBackColorClrCmbBxName As Boolean
            If Not IsNothing(Cell_BackColor_ClrCmbBx(0)) Then
                If sender.name = Cell_BackColor_ClrCmbBx.Name Then
                    CellBackColorClrCmbBxName = True
                End If
            End If
            If sender.Name = MagNote_Form.Upload_Last_Version_Btn.Name Or
                 CellBackColorClrCmbBxName Then
                MoveForm(sender)
            Else
                MoveForm(sender.findform)
            End If
            CntrlMouseDown = False
            Control_Y = e.Y
            Control_X = e.X
        End If
        Dim Lctin = sender.location
    End Sub
    Dim Control_Y, Control_X
    Public Sub AddHandler_Control_Move(ByVal sender As Control, Optional AllCtrl As Boolean = False)
        Try
            If sender.Parent.Name = "Read_Me_Pnl" Or
                sender.Name = "Read_Me_Pnl" Or
                sender.Name = "Insert_Table_Btn" Then
                Exit Sub
            End If
        Catch ex As Exception
        End Try
        AddHandler sender.MouseDown, AddressOf Control_MouseDown
        AddHandler sender.MouseMove, AddressOf Control_MouseMove
        AddHandler sender.MouseUp, AddressOf Control_MouseUp
        If AllCtrl Then
            For Each Ctrl In FindControlRecursive(New List(Of Control), sender, New List(Of Type)({GetType(Label), GetType(Panel), GetType(PictureBox), GetType(TabControl), GetType(TabPage), GetType(StatusStrip)}))
                If Ctrl.Name.Contains("Spliter") Or
                       New List(Of Type)({GetType(TextBox), GetType(DataGridView), GetType(RichTextBox), GetType(ComboBox)}).Contains(Ctrl.GetType) Then
                    Continue For
                End If
                AddHandler Ctrl.MouseDown, AddressOf Control_MouseDown
                AddHandler Ctrl.MouseMove, AddressOf Control_MouseMove
                AddHandler Ctrl.MouseUp, AddressOf Control_MouseUp
            Next
        End If
    End Sub

    Public Sub Control_MouseDown(ByVal sender As Object, e As Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            CntrlMouseDown = True
            Control_Y = e.Y
            Control_X = e.X
        End If
    End Sub
    Public Sub Control_MouseUp(ByVal sender As Object, e As Windows.Forms.MouseEventArgs)
        'If e.Button = Windows.Forms.MouseButtons.Left Then
        CntrlMouseDown = False
        Control_Y = e.Y
        Control_X = e.X
        'End If
    End Sub
#End Region
    Public Function CurrentRowNotEqualRowIndex(ByVal DGV As DataGridView, Optional ByVal SetCurrentRow As Boolean = False) As Boolean
        If IsNothing(DGV.CurrentRow) Then
            If SetCurrentRow And DGV.SelectedRows.Count > 0 Then
                DGV.CurrentCell = DGV.Rows(DGV.SelectedRows(0).Index).Cells(0)
                CurrentRowNotEqualRowIndex(DGV, SetCurrentRow)
            End If
            Return False
        End If
        For Each Row In DGV.SelectedRows
            If DGV.CurrentRow.Index <> Row.index Then
                If SetCurrentRow Then
                    Try
                        DGV.CurrentCell = DGV.Rows(Row.index).Cells(0)
                        Return False
                    Catch ex As Exception
                        Return True
                    End Try
                End If
                Return True
            End If
        Next
    End Function
    Public Function CapitalFirstLetter(ByVal TextToChange As String) As Boolean
        Try
            If Not String.IsNullOrEmpty(TextToChange) Then
                Dim NewText = String.Empty
                For Each Firsteter In TextToChange.Split(" ")
                    If String.IsNullOrEmpty(Firsteter) Then Continue For
                    NewText &= Char.ToUpper(Firsteter(0)) & Microsoft.VisualBasic.Right(Firsteter, Firsteter.Length - 1) & " "
                Next
                TextToChange = Microsoft.VisualBasic.Left(NewText, NewText.Length - 1)
                Return TextToChange
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function UpdateAIOAssemblyVersion()
        Try
            If Debugger.IsAttached() Then
                Msg = "Do You Want To Update Assembly Version?"
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MBOs, False) = DialogResult.No Then
                    Exit Function
                End If
                Dim AssemblyFilePath As String = "I:\My Project\AssemblyInfo.vb"
                Dim TextToReplaceBy As String = Chr(34) & "2022.1802." & Now.Today.ToString("yy") & Now.Today.DayOfYear & "."
                Dim AssemblyVersion As String = "<Assembly: AssemblyVersion(" & TextToReplaceBy
                Dim AssemblyFileVersion As String = "<Assembly: AssemblyFileVersion(" & TextToReplaceBy
                If System.IO.File.Exists(AssemblyFilePath) Then
                    Dim lines() As String = System.IO.File.ReadAllLines(AssemblyFilePath)
                    For i As Integer = 0 To lines.Length - 1
                        If Microsoft.VisualBasic.Left(lines(i), 1) = "'" Then Continue For
                        Dim SearchWithinThis As String = lines(i)
                        Dim SearchForThis As String = "<Assembly: AssemblyVersion("
                        Dim FirstCharacter As Integer = SearchWithinThis.IndexOf(SearchForThis)
                        If FirstCharacter <> -1 Then
                            Dim Line() = Split(lines(i), ".")
                            lines(i) = AssemblyVersion & (Val(Line(3)) + 1) & Chr(34) & ")>"
                        End If
                        SearchForThis = "<Assembly: AssemblyFileVersion("
                        FirstCharacter = SearchWithinThis.IndexOf(SearchForThis)
                        If FirstCharacter <> -1 Then
                            Dim Line() = Split(lines(i), ".")
                            lines(i) = AssemblyFileVersion & (Val(Line(3)) + 1) & Chr(34) & ")>"
                        End If
                    Next
                    System.IO.File.WriteAllLines(AssemblyFilePath, lines) 'assuming you want to write the file
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Public Function FindControlRecursive(ByVal list As List(Of Control),
                                     ByVal parent As Control,
                                     Optional ByVal ctrlType As List(Of System.Type) = Nothing,
                                     Optional ChngCntrlLayout As Boolean = False,
                                     Optional Objects As String = Nothing,
                                     Optional IncludeForms As Boolean = False) As List(Of Control)
        Try
            If parent Is Nothing Then
                Return list
            End If
            Dim ParentTyp As Type = parent.GetType()
            If Not IsNothing(ctrlType) Then
                If ctrlType.Count = 0 Then GoTo ctrlType0
                For Each typ In ctrlType
                    If parent.GetType Is typ Then
                        If parent.Name.ToString.Length > 0 Then
                            If Not IsNothing(Objects) Then
                                If Objects = parent.Name Then
                                    list.Add(parent)
                                    Return list
                                End If
                            ElseIf typ.BaseType.FullName = "System.Windows.Forms.Form" And IncludeForms Then
                                list.Add(parent)
                            ElseIf typ.BaseType.FullName <> "System.Windows.Forms.Form" Then
                                list.Add(parent)
                            End If
                        End If
                    End If
                Next
            Else
ctrlType0:
                If parent.Name.ToString.Length > 0 Then
                    If Not IsNothing(Objects) Then
                        If Objects = parent.Name Then
                            If ParentTyp = GetType(System.Windows.Forms.Form) And Not IncludeForms Then
                                Dim x = 1
                            Else
                                list.Add(parent)
                                Return list
                            End If
                        End If
                    ElseIf ParentTyp.BaseType.FullName = "System.Windows.Forms.Form" And IncludeForms Then
                        list.Add(parent)
                    ElseIf ParentTyp.BaseType.FullName <> "System.Windows.Forms.Form" And
                        ParentTyp.FullName <> "System.Windows.Forms.Form" Then
                        list.Add(parent)
                    End If
                End If
            End If
            For Each child As Control In parent.Controls
                If Not IsNothing(Objects) And list.Count > 0 Then
                    Exit For
                End If
                FindControlRecursive(list, child, ctrlType, ChngCntrlLayout, Objects, IncludeForms)
            Next
            Return list
        Catch ex As Exception
        Finally
        End Try
    End Function
    Public Function isInDataGridView(
                                    ByVal searchString As String,
                                    ByVal columnToSearch As String,
                                    ByVal dataGridView As DataGridView,
                                    Optional ByVal SelectCmbBxItem As Boolean = True,
                                    Optional ByVal ReturnDGVRow As Boolean = False,
                                    Optional ByVal SelectDGVRow As Boolean = False,
                                    Optional ByVal ReturnTrueOrFalse As Boolean = False,
                                    Optional ByVal MultiSelection As Boolean = False,
                                    Optional ByVal SetCell0 As Boolean = 1)
        Dim CurentForm As Form = dataGridView.FindForm
        Try
            If Not dataGridView.Columns.Contains(columnToSearch) Then
                Throw New ArgumentException("The column named '" & columnToSearch & "' does not exists in the DataGridView.", "columnToSearch")
            End If
            If SelectDGVRow And Not MultiSelection Then
                dataGridView.ClearSelection()
            End If
            'Dim match = dataGridView.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) r.Cells("Name").Value.ToString() = "Alice")
            Dim DGVRow As New DataGridViewRow
            DGVRow = dataGridView.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) LCase(r.Cells(columnToSearch).Value.ToString()) = LCase(searchString))

            If DGVRow IsNot Nothing Then
                'Dim DGVRow = dataGridView.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) LCase(r.Cells(columnToSearch).Value.ToString()) = LCase(searchString))
                If ReturnDGVRow Then
                    Return DGVRow
                ElseIf SelectDGVRow Then
                    'dataGridView.Rows(DGVRow.Index).Selected = True
                    MagNote_Form.SelectAvailableMagNotesDGV(DGVRow.Index, dataGridView, SetCell0)
                    If ReturnTrueOrFalse Then
                        Return True
                    End If
                ElseIf SelectCmbBxItem And Not IsNothing(CurentForm.Controls("MagNote_No_CmbBx")) Then
                    Dim SelectedItem = CType(CurentForm.Controls("MagNote_No_CmbBx"), ComboBox).Items.Cast(Of KeyValuePair(Of String, String))().FirstOrDefault(Function(r) r.Key.Equals(searchString)) '.Value.ToString
                    If Not IsNothing(SelectedItem) Then
                        CType(CurentForm.Controls("MagNote_No_CmbBx"), ComboBox).SelectedItem = SelectedItem
                    End If
                ElseIf ReturnTrueOrFalse Then
                    Return True
                End If
                Return DGVRow.Index
            ElseIf ReturnTrueOrFalse Then
                Return False
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function SetMagNoteNoCmbBxFocused() As Boolean
        MagNote_Form.MagNote_No_CmbBx.Focus()
        MagNote_Form.ActiveControl = MagNote_Form.MagNote_No_CmbBx
        IgnoreMagNoteNoCmbBxValidating = True
    End Function

    Public Function Load_Shortcuts_ToolTips(ByVal Shortcuts_LstVw As ListView) As Boolean
        Try
            Using XMLe As New XMLEditor("Life_Labeling_And_Tooltip.xml", "Labeling_And_Tooltip_Entries")
                Dim Elmnt
                Dim LstVw As ListViewItem
                For Each Shortcut In Shortcuts_LstVw.Items
                    Elmnt = XMLe.Element_Exist(MagNote_Form.Name, New Dictionary(Of String, String) From {
                                      {"Object_Name", Shortcut.text}}, 1)
                    If Not IsNothing(Elmnt) Then
                        LstVw = Shortcuts_LstVw.Items(Shortcuts_LstVw.Items.IndexOf(Shortcut))
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            LstVw.Text = Elmnt.Element("Local_Language_Label")?.Value
                            LstVw.ToolTipText = Elmnt.Element("Local_Language_ToolTip")?.Value
                        Else
                            LstVw.Text = Elmnt.Element("Foreign_Language_Label")?.Value
                            LstVw.ToolTipText = Elmnt.Element("Foreign_Language_ToolTip")?.Value
                        End If
                    End If
                Next
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub AddContextMenuStripEdit(ChildControl)
        ChildControl.ContextMenuStrip = New ContextMenuStrip()
        Dim Copy_Control_Name As New ToolStripMenuItem()
        If MagNote_Form.Language_Btn.Text = "E" Then
            Copy_Control_Name.Text = "نسخ إسم العنصر"
        Else
            Copy_Control_Name.Text = "Copy Control Name"
        End If
        Copy_Control_Name.Name = "Copy_Control_Name"
        ChildControl.ContextMenuStrip.Items.Add(Copy_Control_Name)
        Copy_Control_Name.BackgroundImage = My.Resources.Background4
        Copy_Control_Name.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Copy_Control_Name.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
        '-------------------------------------------
        Dim CopyToolTip_TxtBx As New ToolStripMenuItem
        If MagNote_Form.Language_Btn.Text = "E" Then
            CopyToolTip_TxtBx.Text = "نسخ شرح العنصر"
        Else
            CopyToolTip_TxtBx.Text = "Copy Control Description"
        End If
        CopyToolTip_TxtBx.Name = "CopyToolTip" & ChildControl.Name
        'CopyToolTip_TxtBx.Tag = sender.Name
        ChildControl.ContextMenuStrip.Items.Add(CopyToolTip_TxtBx)
        CopyToolTip_TxtBx.BackgroundImage = My.Resources.Background4
        CopyToolTip_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler CopyToolTip_TxtBx.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
        '-------------------------------------------
        Dim CopyControlContents_TxtBx As New ToolStripMenuItem
        If MagNote_Form.Language_Btn.Text = "E" Then
            CopyControlContents_TxtBx.Text = "نسخ محتوى العنصر"
        Else
            CopyControlContents_TxtBx.Text = "Copy Control Contents"
        End If
        CopyControlContents_TxtBx.Name = "CopyControlContents" & ChildControl.Name
        'CopyControlContents_TxtBx.Tag = sender.Name
        ChildControl.ContextMenuStrip.Items.Add(CopyControlContents_TxtBx)
        CopyControlContents_TxtBx.BackgroundImage = My.Resources.Background4
        CopyControlContents_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler CopyControlContents_TxtBx.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
        '-------------------------------------------------------------
        Dim Show_Labeling_And_Tooltip_Form As New ToolStripMenuItem
        If MagNote_Form.Language_Btn.Text = "ع" Then
            Show_Labeling_And_Tooltip_Form.Text = "Show Labeling And Tooltip Form"
        Else
            Show_Labeling_And_Tooltip_Form.Text = "إظهار شاشة إعداد عناوين وشرح العناصر"
        End If
        Show_Labeling_And_Tooltip_Form.Name = "Show_Labeling_And_Tooltip_Form"
        Show_Labeling_And_Tooltip_Form.Tag = ChildControl.Name
        Show_Labeling_And_Tooltip_Form.Image = My.Resources.ShowForm
        Show_Labeling_And_Tooltip_Form.BackgroundImage = My.Resources.Background4
        ChildControl.ContextMenuStrip.Items.Add(Show_Labeling_And_Tooltip_Form)
        Show_Labeling_And_Tooltip_Form.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Show_Labeling_And_Tooltip_Form.Click, AddressOf MagNote_Form.Show_Labeling_And_Tooltip_Form_Click
        If ChildControl.GetType = GetType(TextBox) Then
            Dim PastCliboardToTextBoxContents As New ToolStripMenuItem
            If MagNote_Form.Language_Btn.Text = "E" Then
                PastCliboardToTextBoxContents.Text = "لصق محتوى العنصر"
            Else
                PastCliboardToTextBoxContents.Text = "Past Control Contents"
            End If
            PastCliboardToTextBoxContents.Name = "PastControlContents" & ChildControl.Name
            ChildControl.ContextMenuStrip.Items.Add(PastCliboardToTextBoxContents)
            PastCliboardToTextBoxContents.BackgroundImage = My.Resources.Background4
            PastCliboardToTextBoxContents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler PastCliboardToTextBoxContents.Click, AddressOf MagNote_Form.PastCliboardToTextBoxContents_Click
        End If
        ChildControl.ContextMenuStrip.ForeColor = Color.LightGreen
        ApplyCustomMenuColors(ChildControl.ContextMenuStrip, Color.Black)
    End Sub

    Public Function LoadForm(frm As Form, Form_ToolTip As ToolTip, width As Integer, left As Integer, Height As Integer, Top As Integer, Optional ByVal IgnoreRTL As Boolean = False) As Boolean
        If MagNote_Form.Language_Btn.Text = "ع" Then
            frm.RightToLeftLayout = False
            frm.RightToLeft = RightToLeft.No
            Labeling_Form(frm, "English", Form_ToolTip)
        Else
            Labeling_Form(frm, "Arabic", Form_ToolTip)
            If Not IgnoreRTL Then
                frm.RightToLeftLayout = True
                frm.RightToLeft = RightToLeft.Yes
            End If
        End If
        frm.Location = New System.Drawing.Point(((width / 2) + left) - (frm.Width / 2), ((Height / 2) + Top) - (frm.Height / 2))
        frm.BackColor = MagNote_Form.Note_Back_Color_ClrCmbBx.SelectedItem
        frm.ForeColor = MagNote_Form.Note_Font_Color_ClrCmbBx.SelectedItem
        frm.Opacity = MagNote_Form.Form_Transparency_TrkBr.Value / 100
        If MagNote_Form.Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = CheckState.Unchecked Then
            CType(Form_ToolTip, ToolTip).Active = True
        ElseIf MagNote_Form.Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = CheckState.Checked Then
            CType(Form_ToolTip, ToolTip).Active = False
        End If
    End Function
    Private Sub Copy_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
        Try
            Objct.findform.Cursor = Cursors.WaitCursor
            Objct.Cursor = Cursors.WaitCursor

            'If Objct.parent.name = MagNote_Form.Setting_TbCntrl.Name Then
            '    ShowMsg("Not Available Till This Moment" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            '    Exit Sub
            'End If

            If Not IsNothing(Objct) Then
                Dim ColumnsTooTibText = Nothing
                My.Computer.Clipboard.Clear()
                If sender.text.contains("نسخ شرح العنصر") Or
                    sender.text.contains("Copy Control Description") Then
                    If Objct.GetType = GetType(TabControl) Then
                        Dim FormControl = FindControlRecursive(New List(Of Control), MagNote_Form, New List(Of Type),, MagNote_Form.CurrentToolTipTbPgName).ToList.Item(0)
                        Msg = MagNote_Form.Form_ToolTip.GetToolTip(FormControl)
                    Else
                        Msg = MagNote_Form.Form_ToolTip.GetToolTip(Objct)
                    End If

                    '& vbNewLine & ColumnsTooTibText
                    'Try
                    '    If MagNote_Form.Language_Btn.Text = "E" Then
                    '        If Objct.GetType = GetType(TabControl) Then
                    '            Msg &= "إسم العنصر: " & MagNote_Form.CurrentToolTipTbPgName & vbNewLine
                    '        Else
                    '            Msg &= "إسم العنصر: " & Objct.name.ToString & vbNewLine
                    '        End If
                    '        Msg &= "شاشة العنصر: " & Objct.findform.name.ToString & vbNewLine
                    '        Msg &= "Form AutoSclae Mode: " & CType(Objct.findform, Form).AutoScaleMode.ToString & vbNewLine
                    '        If Not LCase(Objct.name).contains("password") Then
                    '            Msg &= "محتوى العنصر: " & Objct.Text
                    '        End If
                    '        Msg &= vbNewLine & "نوع العنصر: " & Objct.GetType.ToString
                    '    Else
                    '        If Objct.GetType = GetType(TabControl) Then
                    '            Msg &= "Object Name: " & MagNote_Form.CurrentToolTipTbPgName & vbNewLine
                    '        Else
                    '            My.Computer.Clipboard.SetText(Objct.name.ToString)
                    '            Msg &= "Object Name: " & Objct.name.ToString & vbNewLine
                    '        End If
                    '        Msg &= "Form Name: " & Objct.findform.name.ToString & vbNewLine
                    '        If Not LCase(Objct.name).contains("password") Then
                    '            Msg &= "Object Contints: " & Objct.Text
                    '        End If
                    '        Msg &= vbNewLine & "Object Type: " & Objct.GetType.ToString
                    '    End If
                    'Catch ex As Exception
                    'End Try

                    If Not String.IsNullOrEmpty(Msg) Then
                        My.Computer.Clipboard.SetText(Msg)
                    End If
                ElseIf sender.text.contains("نسخ محتوى العنصر") Or
                    sender.text.contains("Copy Control Contents") Then
                    Select Case Objct.GetType
                        Case GetType(StatusStrip)
                            'MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text
                            My.Computer.Clipboard.SetText(CType(Objct, StatusStrip).Items("MsgBox_TlStrpSttsLbl").Text.ToString)
                        Case GetType(DataGridView)
                            Dim MultiSelect = CType(Objct, DataGridView).MultiSelect
                            CType(Objct, DataGridView).ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
                            If CType(Objct, DataGridView).SelectedRows.Count = 0 Then
                                CType(Objct, DataGridView).MultiSelect = True
                                CType(Objct, DataGridView).SelectAll()
                            End If
                            ' Select mode should be FullRowSelect for consistency
                            CType(Objct, DataGridView).SelectionMode = DataGridViewSelectionMode.FullRowSelect

                            Dim data As DataObject = CType(Objct, DataGridView).GetClipboardContent()
                            If data IsNot Nothing Then
                                Clipboard.SetDataObject(data)
                            End If
                            CType(Objct, DataGridView).MultiSelect = MultiSelect
                            ShowMsg("Copied To Clipboard")
                        Case Else
                            If Not LCase(Objct.name).contains("password") Then
                                My.Computer.Clipboard.SetText(Objct.Text.ToString)
                            End If
                    End Select
                Else
                    If Objct.GetType = GetType(TabControl) Then
                        My.Computer.Clipboard.SetText(MagNote_Form.CurrentToolTipTbPgName)
                    Else
                        My.Computer.Clipboard.SetText(Objct.name.ToString)
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            Objct.findform.Cursor = Cursors.Default
            Objct.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub FormToolTip_Draw(sender As Object, e As DrawToolTipEventArgs)
        ' Custom background
        e.Graphics.FillRectangle(New SolidBrush(Color.Yellow), e.Bounds)
        e.Graphics.DrawRectangle(Pens.DarkBlue, New Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1))
        e.Graphics.DrawString(e.ToolTipText, New Font("Times New Roman", 9.5, FontStyle.Regular),
                              Brushes.DarkBlue, e.Bounds)
    End Sub
    Public Function Labeling_Form(ByVal FormName As Form, ByVal Language As String, ByVal MyFormToolTip As ToolTip) As Boolean
        Try
            Using XMLe As New XMLEditor("Life_Labeling_And_Tooltip.xml", "Labeling_And_Tooltip_Entries")
                Dim Elmnt
                Dim LstVw As ListViewItem

                MyFormToolTip.OwnerDraw = False
                MyFormToolTip.UseAnimation = True
                MyFormToolTip.IsBalloon = True

                MyFormToolTip.AutoPopDelay = 8000
                MyFormToolTip.InitialDelay = 500
                MyFormToolTip.ReshowDelay = 200
                MyFormToolTip.ShowAlways = True

                ApplicationDoEvents(FormName)
                Dim ToolTipHeader As New TextBox
                Dim CntrlTipTxt As String
                Dim FormControl = FindControlRecursive(New List(Of Control), FormName, New List(Of Type))
                For Each Cntrl In FormControl
                    CntrlTipTxt = String.Empty
                    Elmnt = XMLe.Element_Exist(FormName.Name, New Dictionary(Of String, String) From {
                                          {"Object_Name", Cntrl.Name}}, 1)
                    If Not IsNothing(Elmnt) Then
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Cntrl.Text = Elmnt.Element("Local_Language_Label")?.Value
                            ToolTipHeader.Text = "إسم العنصر: " & Cntrl.Name & vbNewLine
                            If Not LCase(Cntrl.Name).Contains("password") And (Cntrl.GetType = GetType(Label) Or Cntrl.GetType = GetType(CheckBox)) Then
                                ToolTipHeader.Text &= "عنوان العنصر: " & Cntrl.Text & vbNewLine
                            End If
                            ToolTipHeader.Text &= "شرح العنصر: " & Elmnt.Element("Local_Language_ToolTip")?.Value.ToString & vbNewLine
                            ToolTipHeader.Text &= "--------------------------------------" & vbNewLine
                            ToolTipHeader.Text &= "خط العنصر: " & CType(Cntrl.Font, Font).Name & " المقاس " & CType(Cntrl.Font, Font).Size & vbNewLine
                            ToolTipHeader.Text &= "حجم العنصر: (العرض=" & Cntrl.Width & ") (الإرتفاع=" & Cntrl.Height & ")" & vbNewLine
                        Else
                            Cntrl.Text = Elmnt.Element("Foreign_Language_Label")?.Value
                            ToolTipHeader.Text = "Object Name: " & Cntrl.Name & vbNewLine
                            If Not LCase(Cntrl.Name).Contains("password") And (Cntrl.GetType = GetType(Label) Or Cntrl.GetType = GetType(CheckBox)) Then
                                ToolTipHeader.Text &= "Object Label: " & Cntrl.Text & vbNewLine
                            End If
                            ToolTipHeader.Text &= "Object Description: " & Elmnt.Element("Foreign_Language_ToolTip")?.Value.ToString & vbNewLine
                            ToolTipHeader.Text &= "--------------------------------------" & vbNewLine
                            ToolTipHeader.Text &= "Object Font: " & Cntrl.Font.ToString & vbNewLine
                            ToolTipHeader.Text &= "Object Font: " & CType(Cntrl.Font, Font).Name & " Size " & CType(Cntrl.Font, Font).Size & vbNewLine
                            ToolTipHeader.Text &= "Object Size: (W=" & Cntrl.Width & ") (H=" & Cntrl.Height & ")" & vbNewLine
                        End If
                    Else
                        ToolTipHeader.Text = "Object Name: " & Cntrl.Name & vbNewLine
                        If Not LCase(Cntrl.Name).Contains("password") And (Cntrl.GetType = GetType(Label) Or Cntrl.GetType = GetType(CheckBox)) Then
                            ToolTipHeader.Text &= "Object Label: " & Cntrl.Text & vbNewLine
                        End If
                        If Not IsNothing(MyFormToolTip) Then
                            ToolTipHeader.Text &= "Object Description: " & MyFormToolTip.GetToolTip(Cntrl) & vbNewLine
                        Else
                            ToolTipHeader.Text &= "Object Description: " & "" & vbNewLine
                        End If
                        ToolTipHeader.Text &= "--------------------------------------" & vbNewLine
                        ToolTipHeader.Text &= "Object Font: " & Cntrl.Font.ToString & vbNewLine
                        ToolTipHeader.Text &= "Object Font: " & CType(Cntrl.Font, Font).Name & " Size " & CType(Cntrl.Font, Font).Size & vbNewLine
                        ToolTipHeader.Text &= "Object Size: (W=" & Cntrl.Width & ") (H=" & Cntrl.Height & ")" & vbNewLine
                    End If
                    MyFormToolTip.SetToolTip(Cntrl, ToolTipHeader.Text)

                    If Not New List(Of Type)({GetType(PictureBox), GetType(ListView), GetType(TreeView), GetType(Panel), GetType(TabControl), GetType(TabPage), GetType(RichTextBox), GetType(DataGridView)}).Contains(Cntrl.GetType) Then
                        AddContextMenuStripEdit(Cntrl)
                    End If
                Next
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public progress As Single = 0F
    Private Sub Control_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        Try
            e.Graphics.FillRectangle(Brushes.Green, New Rectangle(0, 0, CInt((sender.Width * progress)), sender.height))
            TextRenderer.DrawText(e.Graphics, sender.text & "%", SystemFonts.DefaultFont, New Rectangle(0, 2, sender.Width, sender.Height), Color.Black, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetBrightness(color As Color) As Single
        Return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255
    End Function

    ' Adjust ForeColor based on BackColor brightness
    Public Sub AdjustForeColor(control As Object, Optional ByVal AskToAdjust As Boolean = False)
        Dim BackBrightness As Single = GetBrightness(control.BackColor)
        Dim ForeBrightness As Single = GetBrightness(control.ForeColor)
        Dim Dif
        If ForeBrightness > BackBrightness Then
            Dif = ForeBrightness - BackBrightness
        Else
            Dif = BackBrightness - ForeBrightness
        End If
        If Dif > 0.3 Then
            Exit Sub
        End If
        If BackBrightness > 0.5 And ForeBrightness > 0.5 Then
            If AskToAdjust Then
                If Not AdjustColor() Then
                    Exit Sub
                End If
            End If
            control.ForeColor = Color.Black ' Dark ForeColor for bright background
        ElseIf BackBrightness < 0.5 And ForeBrightness < 0.5 Then
            If AskToAdjust Then
                If Not AdjustColor() Then
                    Exit Sub
                End If
            End If
            control.ForeColor = Color.White ' Light ForeColor for dark background
        End If
    End Sub
    Private Function AdjustColor() As Boolean
        If MagNote_Form.Language_Btn.Text = "E" Then
            Msg = "خلفية اللون متقارب جدا مع لون الخط بحيث تصبح الرؤية سيئة... هل تريدالاستمرار؟"
        Else
            Msg = "The Back Color Is Too Close To The Font Color So That Visibility Is Poor... Do You Want To Continue?"
        End If
        If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MBOs, False) = DialogResult.Yes Then
            Return True
        End If
    End Function

    Public Function AddCustomProgresBar(PreviewPnl As Panel,
                                                                     Previewlbl As Label,
                                                                     ItemsCount As Integer,
                                                                     Optional FormName As Form = Nothing)
        Try

            If IsNothing(FormName) Then
                FormName = MagNote_Form
            End If
            FormName.Controls.Add(PreviewPnl)
            PreviewPnl.Size = New Size(450, 40)
            PreviewPnl.Controls.Add(Previewlbl)
            Previewlbl.Dock = DockStyle.Fill
            Previewlbl.TextAlign = ContentAlignment.MiddleCenter
            Dim y = (PreviewPnl.Parent.Height \ 2) - (PreviewPnl.Height \ 2)
            Dim x = (PreviewPnl.Parent.Width \ 2) - (PreviewPnl.Width \ 2)
            PreviewPnl.Location = New Point(x, y)
            Previewlbl.Visible = True
            If Not IsNothing(RCSN(0)) Then
                Previewlbl.ForeColor = RCSN(0).BackColor
                Previewlbl.BackColor = RCSN(0).BackColor
            Else
                Previewlbl.ForeColor = Color.Black
                Previewlbl.BackColor = System.Drawing.SystemColors.Window
            End If
            PreviewPnl.BackColor = Color.Transparent
            Previewlbl.BorderStyle = BorderStyle.FixedSingle
            PreviewPnl.Parent = FormName
            PreviewPnl.Visible = True
            PreviewPnl.BringToFront()
            AddHandler Previewlbl.Paint, AddressOf Control_Paint
            progress = 0F

            Return (100 / ItemsCount) / 100
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function ConvertDate(Optional ByVal TextDate As String = Nothing, Optional ByVal DMY As Boolean = False) As String
        Dim DateText As Date
        Try
            If String.IsNullOrEmpty(TextDate) Then
                Return Nothing
            End If
            DateText = TextDate
            If IsDate(DateText) Then
                If DMY Then
                    ConvertDate = Format(DateText, "dd-MM-yyyy")
                Else
                    ConvertDate = Format(DateText, "yyyy-MM-dd")
                End If
                Return ConvertDate
            End If
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "صيغة التاريخ صيغة لا يمكن التعامل معها او خاطئة"
            Else
                Msg = "Date Format Is Not Combatiple Or Wrong Format"
            End If
            Msg &= vbNewLine & DateText
            ShowMsg(Msg & vbNewLine & ex.Message,, MessageBoxButtons.OK, MessageBoxIcon.Information = MessageBoxIcon.Asterisk)
            Return Nothing
        End Try
    End Function
    Public Function ConvertDateTime(Optional ByVal TextDate As String = Nothing,
                                                      Optional ByVal ShowFraction As Boolean = True,
                                                      Optional ByVal DMY As Boolean = False,
                                                      Optional ByVal ConvertAmPm As Boolean = False) As String
        Dim DateText As DateTime
        Try
            If String.IsNullOrEmpty(TextDate) Then
                Return Nothing
            End If
            DateText = TextDate
            If IsDate(DateText) Then
                If (DateText.ToString.Contains("AM") Or DateText.ToString.Contains("PM")) And ConvertAmPm Then
                    If TimeOnly Then
                        If ShowFraction Then
                            ConvertDateTime = Format(DateText, "HH:mm:ss tt")
                        Else
                            ConvertDateTime = Format(DateText, "HH:mm:ss")
                        End If
                    Else
                        If ShowFraction Then
                            If DMY Then
                                ConvertDateTime = Format(DateText.Date, "dd-MM-yyyy") & " " & Format(DateText, "HH:mm:ss tt")
                            Else
                                ConvertDateTime = Format(DateText.Date, "yyyy-MM-dd") & " " & Format(DateText, "HH:mm:ss tt")
                            End If
                        Else
                            If DMY Then
                                ConvertDateTime = Format(DateText.Date, "dd-MM-yyyy") & " " & Format(DateText, "HH:mm:ss")
                            Else
                                ConvertDateTime = Format(DateText.Date, "yyyy-MM-dd") & " " & Format(DateText, "HH:mm:ss")
                            End If
                        End If
                    End If
                Else
                    If TimeOnly Then
                        If ShowFraction Then
                            ConvertDateTime = Format(DateText, "HH:mm:ss.fff")
                        Else
                            ConvertDateTime = Format(DateText, "HH:mm:ss")
                        End If
                    Else
                        If ShowFraction Then
                            If DMY Then
                                ConvertDateTime = Format(DateText, "dd-MM-yyyy HH:mm:ss.fff")
                            Else
                                ConvertDateTime = Format(DateText, "yyyy-MM-dd HH:mm:ss.fff")
                            End If
                        Else
                            If DMY Then
                                ConvertDateTime = Format(DateText.Date, "dd-MM-yyyy") & " " & Format(DateText, "HH:mm:ss")
                            Else
                                ConvertDateTime = Format(DateText.Date, "yyyy-MM-dd") & " " & Format(DateText, "HH:mm:ss")
                            End If
                        End If
                    End If
                End If
                Return ConvertDateTime
            End If
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "صيغة التاريخ صيغة لا يمكن التعامل معها او خاطئة"
            Else
                Msg = "Date Format Is Not Combatiple Or Wrong Format"
            End If
            Msg &= vbNewLine & DateText
            ShowMsg(Msg & vbNewLine & ex.Message,, MessageBoxButtons.OK, MessageBoxIcon.Information = MessageBoxIcon.Asterisk)
            Return Nothing
        End Try
    End Function
    Public Function IsLink(lnkPath As String) As Object
        Try
            Dim shl = New Shell32.Shell()
            lnkPath = System.IO.Path.GetFullPath(lnkPath)
            Dim dir = shl.[NameSpace](System.IO.Path.GetDirectoryName(lnkPath))
            Dim itm = dir.Items().Item(System.IO.Path.GetFileName(lnkPath))
            If itm.IsLink Then
                Return itm
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Function calculateDiffDates(ByVal StartDate As DateTime, ByVal EndDate As DateTime) 'As Integer
        'Dim diff As Integer
        'diff = (EndDate - StartDate).TotalDays
        'Return diff

        Dim startTime As New DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartDate.Hour, StartDate.Minute, StartDate.Second)     ' 10:30 AM today
        Dim endTime As New DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndDate.Hour, EndDate.Minute, EndDate.Second)     ' 2:00 AM tomorrow

        Dim duration As TimeSpan = endTime - startTime        'Subtract start time from end time
        Return duration
    End Function

End Module
