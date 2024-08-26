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

Module MainModule
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
    Public ToolTip1 As New ToolTip
    Public ApplicationRestart As Boolean
    Public SNFF As Boolean
    Public FirstTimeRun As Boolean
    Public UseArgFile As String
    Public ExternalFilePath, ExternalFileName
    Public MagNoteFolderPath As String = Application.StartupPath & "\MagNotes_Files"
    Public AlreadyAsked As Boolean
    Public Save_Setting_When_Exit As Boolean
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
        Public Reminder_Every As String
        Public Next_Reminder_Time As String
        Public Blocked_Note As CheckState
        Public Finished_Note As CheckState
        Public Secured_Note As CheckState
        Public Use_Main_Password As CheckState
        Public Note_Password As String
        Public Note_Have_Reminder As CheckState
        Public Note_Font As String
        Public Word_Wrap As CheckState
        Public ForeColor As String
        Public BackColor As String
    End Structure
    Structure RchTxtBxSelectedTextDetails
        Public MagNoteName As String
        Public SelectedText As String
        Public SelectedTextLength As Integer
        Public SelectedTextLine As Integer
        Public SelectedTextColumn As Integer
        Public SelectionStart As Integer
    End Structure
    Public RchTxtBxSelectedText(0) As RchTxtBxSelectedTextDetails
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
    Public Function IsInternetAvailable() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "لم تفلح محاولة التحقق من وجود تحيث للبرنامج"
            ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "Couldn't Check To Find New Version Of The Progarm"
            End If
            ShowMsg(Msg & vbNewLine & ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
        End Try
    End Function
    Public Function CreatAppConfig() As Boolean
        Dim TextToWrite = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "utf-8" & Chr(34) & "?>
<configuration>
		<connectionStrings>
		<add name=" & Chr(34) & "MagNote_Local" & Chr(34) & " connectionString=" & Chr(34) & "
			Data Source=YOUR SQL SERVER NAME;
			Initial Catalog=YOUR DATABASE NAME;     
			persist security info=False;        
			User Id=YOUR SQL USER NAME;     
			Password=YOUR SQL USER PSSWORD;        
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
			Data Source=YOUR SQL HOST SERVER NAME;
			Initial Catalog=YOUR DATABASE NAME AT HOST;     
			persist security info=False;        
			User Id=YOUR SQL USER Name AT HOST;     
			Password=YOUR SQL USER PSSWORD AT HOST;        
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
		<add key=" & Chr(34) & "SMTP_Mail_User_Name" & Chr(34) & " value=" & Chr(34) & "SMTPMAILUSERNAME" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Mail_User_Password" & Chr(34) & " value=" & Chr(34) & "SMTPMAILUSERPASSWORD" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Host" & Chr(34) & " value=" & Chr(34) & "SMTPHOSTSERVERURL" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Port" & Chr(34) & " value=" & Chr(34) & "587" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_DeliveryMethod" & Chr(34) & " value=" & Chr(34) & "Network" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_EnableSsl" & Chr(34) & " value=" & Chr(34) & "True" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Mail_From" & Chr(34) & " value=" & Chr(34) & "SMTPMAILFROM" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Use_Default_Credentials" & Chr(34) & " value=" & Chr(34) & "0" & Chr(34) & " />
		<add key=" & Chr(34) & "SMTP_Use_SmtpServer_Or_SmtpClient" & Chr(34) & " value=" & Chr(34) & "1" & Chr(34) & " />
		<add key=" & Chr(34) & "EncryptionKey" & Chr(34) & " value=" & Chr(34) & "EncryptionKey" & Chr(34) & " />
  </appSettings>
</configuration>
"
        'Application.StartupPath & "\" & Application.ProductName & ".exe.config"
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\" & Application.ProductName & ".exe.config", TextToWrite, 0, System.Text.Encoding.UTF8)
    End Function
    Public Sub RestartWithElevatedPrivileges(ByVal RunMeAsAdministrator As Boolean)
        If RunMeAsAdministrator Then
            MagNote_Form.Run_Me_As_Administrator_ChkBx.CheckState = CheckState.Checked
            MagNote_Form.Save_Note_Form_Parameter_Setting_Btn_Click(MagNote_Form.Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
        End If
        Dim programpath As String = New System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath
        Dim arguments As String() = System.Environment.GetCommandLineArgs().Skip(1).ToArray()
        Dim startinfo As New System.Diagnostics.ProcessStartInfo With {
                .FileName = programpath,
                .UseShellExecute = True,
                .Verb = "runas",
                .Arguments = String.Join(" ", arguments)
            }
        System.Diagnostics.Process.Start(startinfo)
        System.Environment.[Exit](0)
    End Sub
    Public Function EncrypAppConfig() As Boolean
        Try
            If Not File.Exists(Application.StartupPath & "\" & Application.ProductName & ".exe.config") Then
                CreatAppConfig()
                Msg = "لم تفلح محاولة العثور على ملف اعدادات النظام... تم انشاء اخر جديد يحتاج الى اعداد منك"
                Msg &= vbNewLine & "Couldn't Find Configuration File. New File Created And Needs To Update From You"
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                If File.Exists(Application.StartupPath & "\" & Application.ProductName & ".exe.config") Then
                    'RestartWithElevatedPrivileges(0)
                    ' Start a new instance of the application
                    System.Diagnostics.Process.Start(Application.ExecutablePath)

                    ' Close the current instance of the application
                    ApplicationRestart = True
                    Application.Exit()
                    End
                End If
            End If
            ' Open the current configuration file
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            ' Get the appSettings section
            Dim section As ConfigurationSection = config.GetSection("appSettings")
            If ConfigurationManager.AppSettings("EncryptionKey") = "EncryptionKey" Then
                Msg = "ربما لم يتم اعداد ملف اعدادات النظام بعد... لن يتم تشفير الملف إلا بعد تغيير مفتاح التشفير فى الملف"
                Msg &= vbNewLine & "Maybe Application Configuration File Not Prepared Yet... Encrypting The File Will Not Be Done Until Changing The Password In The File"
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            ' Check if the section is already protected
            If Not section.SectionInformation.IsProtected Then
                ' Protect the section using the DataProtectionConfigurationProvider
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                section.SectionInformation.ForceSave = True
                ' Save the changes
                config.Save(ConfigurationSaveMode.Modified)
            End If

            ' Get the connectionStrings section
            Dim CS As ConnectionStringsSection = config.GetSection("connectionStrings")
            ' Check if the section is already protected
            If Not CS.SectionInformation.IsProtected Then
                ' Protect the section using the DataProtectionConfigurationProvider
                CS.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                CS.SectionInformation.ForceSave = True
                ' Save the changes
                config.Save(ConfigurationSaveMode.Modified)
            End If
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public OpenedExternalFiles(0) As String
    Public Function RunAsExternal(Optional ByVal MagNotName As String = Nothing) As Boolean
        Try
            For Each Fil In OpenedExternalFiles
                If IsNothing(Fil) Then Continue For
                If String.IsNullOrEmpty(MagNotName) And
                    Not IsNothing(RCSN(0)) Then
                    MagNotName = RCSN(0).Name
                Else
                    MagNotName = String.Empty
                End If
                If Fil = Replace(MagNotName, "RchTxtBx", "") Then
                    Return True
                End If
            Next
        Catch ex As Exception
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
            End If
            Dim SelectedTabTag = MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag
            Dim MagNote = FindControlRecursive(New List(Of Control), MagNote_Form.MagNotes_Notes_TbCntrl, New List(Of Type)({GetType(RichTextBox)}),, (SelectedTabTag & "RchTxtBx")).ToList.Item(0)
            Application.DoEvents()
            Return CType(MagNote, RichTextBox)
        Catch ex As Exception
            If ShowErrorMessage Then
                ShowMsg("Function RCSN()" & vbNewLine & ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Public Function RefreshNotExistingFile()
        Dim RCSNName = RCSN.Name
        Dim RchTxtBxStyleInx = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN.Name)
        If RchTxtBxStyleInx = -1 Then
            MagNote_Form.Note_Font_Color_ClrCmbBx.Text = Nothing
            MagNote_Form.Note_Back_Color_ClrCmbBx.Text = Nothing
            MagNote_Form.Note_Font_Color_ClrCmbBx.Text = MagNote_Form.External_Note_Font_Color_ClrCmbBx.Text
            MagNote_Form.Note_Back_Color_ClrCmbBx.Text = MagNote_Form.External_Note_Back_Color_ClrCmbBx.Text 'Color.WhiteSmoke.Name.ToString
            MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value = 0
            MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value = 0
            MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value = 0
            MagNote_Form.Next_Reminder_Time_DtTmPkr.Value = Now
            MagNote_Form.Note_Word_Wrap_ChkBx.CheckState = CheckState.Checked
        Else
            MagNote_Form.Next_Reminder_Time_DtTmPkr.Value = RchTxtBxStyle(RchTxtBxStyleInx).Next_Reminder_Time
            MagNote_Form.Secured_Note_ChkBx.CheckState = RchTxtBxStyle(RchTxtBxStyleInx).Secured_Note
            If RchTxtBxStyle(RchTxtBxStyleInx).Word_Wrap = CheckState.Checked Then
                MagNote_Form.Note_Word_Wrap_ChkBx.CheckState = CheckState.Checked
            Else
                MagNote_Form.Note_Word_Wrap_ChkBx.CheckState = CheckState.Unchecked
            End If
            If Not String.IsNullOrEmpty(RchTxtBxStyle(RchTxtBxStyleInx).ForeColor) Then
                MagNote_Form.Note_Font_Color_ClrCmbBx.Text = Nothing
                MagNote_Form.Note_Font_Color_ClrCmbBx.Text = RchTxtBxStyle(RchTxtBxStyleInx).ForeColor
            End If
            If Not String.IsNullOrEmpty(RchTxtBxStyle(RchTxtBxStyleInx).BackColor) Then
                MagNote_Form.Note_Back_Color_ClrCmbBx.Text = Nothing
                MagNote_Form.Note_Back_Color_ClrCmbBx.Text = RchTxtBxStyle(RchTxtBxStyleInx).BackColor
            End If
        End If
    End Function
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
                            Optional ByVal DoMakeTopMost As Boolean = True) As DialogResult
        Try
            If TimeToSpendVisible = 0 Then
                TimeToSpendVisible = 30
            End If
            If Not IsNothing(MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl")) Then
                MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").TextAlign = ContentAlignment.MiddleLeft
                MagNote_Form.MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = BoxMsg
                MagNote_Form.MsgBox_SttsStrp.Refresh()
            End If
            If WithSound Then PlaySnd("Command.wav")
            If Not ShowMe And Show_Windows_Notification Then
                ShowWindowsNotification(BoxMsg, ForceDisplayWindowsNotification)
                Exit Function
            ElseIf Not ShowMe Then
                Exit Function
            End If
            MessageBoxTimer = New Threading.Thread(AddressOf closeMessageBox)
            If Withtimer Then
                MessageBoxTimer.Start(TimeToSpendVisible)
            End If
            If IsNothing(MOB) Then
                MOB = MBOs()
            End If

            Return MessageBox.Show(BoxMsg, "Current Method Name (" & New StackFrame(1).GetMethod().Name.ToString & ")", MBBs, MBI, MBDB, MOB, False)
        Catch ex As Exception
            MagNote_Form.Focus()
        Finally
            MessageBoxTimer.Abort()
            If DoMakeTopMost Then
                MakeTopMost(1)
            End If
        End Try
    End Function
    Public Function PlaySnd(ByVal Sound As String) As Boolean
        Try
            sndPlaySound(Sound, SND_ASYNC)
        Catch ex As Exception
        End Try
    End Function
    Public Function MBOs() As MessageBoxOptions

        If MagNote_Form.Language_Btn.Text = "E" Then
            Return MessageBoxOptions.ServiceNotification Or
                        MessageBoxOptions.RightAlign Or
                        MessageBoxOptions.RtlReading
        Else
            Return MessageBoxOptions.ServiceNotification
        End If
    End Function
    Public Sub closeMessageBox(ByVal delay As Object)
        Threading.Thread.Sleep(CInt(delay) * 1000)
        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0)
        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0)
    End Sub

#Region "Encrypt Decrypt Data"
    Function Encrypt(ByVal plainText As String, ByVal bytKey As Byte(), ByVal bytIV As Byte()) As String
        Try
            Dim cipher As New RijndaelManaged
            Dim encryptor As ICryptoTransform = cipher.CreateEncryptor(bytKey, bytIV)
            Dim data As Byte() = Encoding.Unicode.GetBytes(plainText)
            Return Convert.ToBase64String(encryptor.TransformFinalBlock(data, 0, data.Length))
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Function Decrypt(ByVal encryptedText As String, ByVal key As Byte(), ByVal iv As Byte()) As String
        Try
            Dim cipher As New RijndaelManaged
            Dim decryptor As ICryptoTransform = cipher.CreateDecryptor(key, iv)
            Dim data As Byte() = Convert.FromBase64String(encryptedText)
            Return Encoding.Unicode.GetString(decryptor.TransformFinalBlock(data, 0, data.Length))
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "My Be The EncryptionKey In Application Configuration File Is Not Correct"
            Else
                Msg = "ربما مفتاح التشفير المسجل فى ملف تكوين التطبيق غير صحيح"
            End If
            Msg &= vbNewLine & Application.StartupPath & "\" & Application.ProductName & ".exe.config"
            ShowMsg(Msg & vbNewLine & vbNewLine & ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public Function Encrypt_Function(Optional ByVal EncryptTextBox As String = Nothing, Optional ByVal EncryptKey As String = Nothing) As String
        Try
            Dim bytKey As Byte()
            Dim bytIV As Byte()
            If IsNothing(EncryptKey) Then
                EncryptKey = EncryptionKey
            End If
            bytKey = CreateKey(EncryptKey)
            'Send the password to the CreateIV function.
            bytIV = CreateIV(EncryptKey)
            Return Encrypt(EncryptTextBox, bytKey, bytIV)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public Function Decrypt_Function(Optional ByVal DecryptTextBox As String = Nothing, Optional ByVal EncryptKey As String = Nothing) As String
        Try
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
            Dim Password =
                Decrypt(DecryptTextBox, bytKey, bytIV)
            Return Password
        Catch ex As Exception
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
    Public Function NoteAmendmented(Optional ByVal NoteName As String = Nothing, Optional ByVal WindowsShuttingDown As Boolean = False) As DialogResult
        Try
            For Each TbPg In MagNote_Form.MagNotes_Notes_TbCntrl.TabPages
                Dim IfIsTrue As Boolean = False
                Dim ErrorReason As String = String.Empty
                If Not String.IsNullOrEmpty(NoteName) Then
                    If TbPg.name <> NoteName Then
                        Continue For
                    End If
                End If
                MagNote_Form.ActiveControl = MagNote_Form.MagNotes_Notes_TbCntrl
                MagNote_Form.ActiveControl.Focus()
                MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab = TbPg
                Dim SelectedIndex = MagNote_Form.MagNote_No_CmbBx.SelectedIndex
                Dim TbCntrlTabPagesCount = MagNote_Form.MagNotes_Notes_TbCntrl.TabPages.Count
                Dim MyCurrentDGVRow As String = String.Empty
                If MagNote_Form.MagNote_No_CmbBx.SelectedIndex <> -1 Then
                    If Not File.Exists(DirectCast(MagNote_Form.MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) And RCSN.TextLength > 0 Then
                        If WindowsShuttingDown Then
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
                    IsNothing(RCSN(0)) Or
                    SelectedIndex = -1 Or
                    TbCntrlTabPagesCount = 0 Or
                    IsNothing(MyCurrentDGVRow)) And Not RunAsExternal() Then
                    Continue For
                End If
                Dim xx = Replace(Replace(RCSN.Text, vbLf, vbNewLine), vbCrLf, vbNewLine)
                If IsNothing(xx) And MagNote_Form.Secured_Note_ChkBx.CheckState = CheckState.Checked Then
                    Continue For
                End If
                If (RCSN.TextLength = 0 Or
                    MagNote_Form.MagNoteIsOpenedExternal) And
                    isInDataGridView(NoteName, "MagNote_Name", MagNote_Form.Available_MagNotes_DGV, 0,,, 1) Then
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
                        If OldText <> xx Then
                            ErrorReason &= "OldText <> xx"
                            IfIsTrue = True
                            GoTo ExternalNoteNotSaved
                        Else
                            Continue For
                        End If
                    End If
                End If

                Dim SNNCmbBxSelectedIndex = MagNote_Form.MagNote_No_CmbBx.SelectedIndex
                Dim SNNCmbBxText = MagNote_Form.MagNote_No_CmbBx.Text
                Dim SPTxtBxText = MagNote_Form.Note_Password_TxtBx.Text
                Application.DoEvents()
                If MagNote_Form.ActiveControl.Name = MagNote_Form.Available_MagNotes_DGV.Name Then
                    If MagNote_Form.CurrentDGVRow.Index <> -1 Then
                        If MagNote_Form.Blocked_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Blocked_Note").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Blocked_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Blocked_Note).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Finished_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Finished_Note").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Finished_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Finished_Note).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Secured_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Secured_Note").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Secured_Note_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Secured_Note).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Use_Main_Password_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Use_Main_Password").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Use_Main_Password_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Use_Main_Password).Value"
                            IfIsTrue = True
                        ElseIf (MagNote_Form.Note_Password_TxtBx.TextLength > 0 And Decrypt_Function(MagNote_Form.Note_Password_TxtBx.Text) <> MagNote_Form.CurrentDGVRow.Cells("Note_Password").Value) Then
                            ErrorReason &= vbNewLine & "(MagNote_Form.Note_Password_TxtBx.TextLength > 0 And Decrypt_Function(MagNote_Form.Note_Password_TxtBx.Text) <> MagNote_Form.CurrentDGVRow.Cells(Note_Password).Value)"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Note_Word_Wrap_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Note_Word_Wrap").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Note_Word_Wrap_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Note_Word_Wrap).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Note_Have_Reminder_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells("Note_Have_Reminder").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Note_Have_Reminder_ChkBx.CheckState <> MagNote_Form.CurrentDGVRow.Cells(Note_Have_Reminder).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Note_Font_TxtBx.Text <> MagNote_Form.CurrentDGVRow.Cells("Note_Font").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Note_Font_TxtBx.Text <> MagNote_Form.CurrentDGVRow.Cells(Note_Font).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Note_Font_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells("Note_Font_Color").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Note_Font_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells(Note_Font_Color).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Note_Back_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells("Note_Back_Color").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Note_Back_Color_ClrCmbBx.Text <> MagNote_Form.CurrentDGVRow.Cells(Note_Back_Color).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Next_Reminder_Time_DtTmPkr.Text <> MagNote_Form.CurrentDGVRow.Cells("Next_Reminder_Time").Value Then
                            Dim DtTmPkr = MagNote_Form.Next_Reminder_Time_DtTmPkr.Text
                            Dim NRT = MagNote_Form.CurrentDGVRow.Cells("Next_Reminder_Time").Value
                            ErrorReason &= vbNewLine & "MagNote_Form.Next_Reminder_Time_DtTmPkr.Text <> MagNote_Form.CurrentDGVRow.Cells(Next_Reminder_Time).Value"
                            IfIsTrue = True
                        ElseIf MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value <> MagNote_Form.CurrentDGVRow.Cells("Reminder_Every").Value Then
                            ErrorReason &= vbNewLine & "MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & , & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & , & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value <> MagNote_Form.CurrentDGVRow.Cells(Reminder_Every).Value"
                            IfIsTrue = True
                        End If
                    End If
                Else
                    Dim fnt = RCSN.Font
                    Dim stfnt = RCSN.Font.Name & " - " & RCSN.Font.Size & " - " & fnt.Style
                    stfnt = MagNote_Form.ReturnFontString(RCSN.Font)
                    Dim RchTxtBxStyleINX = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN.Name)
                    If RchTxtBxStyleINX = -1 Then
                        Continue For
                    End If
                    Dim nm = RCSN.Name
                    Dim nm1 = RchTxtBxStyle(RchTxtBxStyleINX).RchTxtBx_Name
                    If Not RunAsExternal() Then
                        Dim nm2 = MagNote_Form.Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Name").Value
                    End If
                    If MagNote_Form.Blocked_Note_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Blocked_Note Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Blocked_Note_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Blocked_Note"
                        IfIsTrue = True
                    ElseIf MagNote_Form.Finished_Note_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Finished_Note Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Finished_Note_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Finished_Note"
                        IfIsTrue = True
                    ElseIf MagNote_Form.Secured_Note_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Secured_Note Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Secured_Note_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Secured_Note"
                        IfIsTrue = True
                    ElseIf MagNote_Form.Use_Main_Password_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Use_Main_Password Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Use_Main_Password_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Use_Main_Password"
                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Password_TxtBx.TextLength > 0 And Decrypt_Function(MagNote_Form.Note_Password_TxtBx.Text) <> RchTxtBxStyle(RchTxtBxStyleINX).Note_Password Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Password_TxtBx.TextLength > 0 And Decrypt_Function(MagNote_Form.Note_Password_TxtBx.Text) <> RchTxtBxStyle(RchTxtBxStyleINX).Note_Password"
                        IfIsTrue = True
                    ElseIf RCSN.WordWrap <> Convert.ToBoolean(MagNote_Form.Note_Word_Wrap_ChkBx.CheckState) Then
                        ErrorReason &= vbNewLine & "RCSN.WordWrap <> Convert.ToBoolean(MagNote_Form.Note_Word_Wrap_ChkBx.CheckState)"
                        Dim RCSNWordWrap = RCSN.WordWrap
                        Dim RCSNWordWrap1 = Convert.ToBoolean(MagNote_Form.Note_Word_Wrap_ChkBx.CheckState)

                        IfIsTrue = True
                    ElseIf MagNote_Form.Note_Have_Reminder_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Note_Have_Reminder Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Note_Have_Reminder_ChkBx.CheckState <> RchTxtBxStyle(RchTxtBxStyleINX).Note_Have_Reminder"
                        IfIsTrue = True
                    ElseIf stfnt <> RchTxtBxStyle(RchTxtBxStyleINX).Note_Font Then
                        ErrorReason &= vbNewLine & "stfnt <> RchTxtBxStyle(RchTxtBxStyleINX).Note_Font"
                        IfIsTrue = True
                    ElseIf RCSN.ForeColor.Name <> RchTxtBxStyle(RchTxtBxStyleINX).ForeColor Then
                        ErrorReason &= vbNewLine & "RCSN.ForeColor.Name <> RchTxtBxStyle(RchTxtBxStyleINX).ForeColor"
                        Dim RCSNForeColorName = RCSN.ForeColor.Name
                        Dim RCSNForeColorName1 = RchTxtBxStyle(RchTxtBxStyleINX).ForeColor

                        IfIsTrue = True
                    ElseIf RCSN.BackColor.Name <> RchTxtBxStyle(RchTxtBxStyleINX).BackColor Then
                        ErrorReason &= vbNewLine & "RCSN.BackColor.Name <> RchTxtBxStyle(RchTxtBxStyleINX).BackColor"
                        Dim RCSNBackColorName = RCSN.BackColor.Name
                        Dim RCSNBackColorName1 = RchTxtBxStyle(RchTxtBxStyleINX).BackColor

                        IfIsTrue = True
                    ElseIf MagNote_Form.Next_Reminder_Time_DtTmPkr.Text <> RchTxtBxStyle(RchTxtBxStyleINX).Next_Reminder_Time And
                        MagNote_Form.Note_Have_Reminder_ChkBx.checkstate = CheckState.Checked Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Next_Reminder_Time_DtTmPkr.Text <> RchTxtBxStyle(RchTxtBxStyleINX).Next_Reminder_Time"
                        Dim NextReminderTimeDtTmPkr = MagNote_Form.Next_Reminder_Time_DtTmPkr.Text
                        Dim NextReminderTimeDtTmPkr1 As DateTime = RchTxtBxStyle(RchTxtBxStyleINX).Next_Reminder_Time
                        NextReminderTimeDtTmPkr = CType(DateTime.ParseExact(NextReminderTimeDtTmPkr, "yyyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture), DateTime)


                        If CType(NextReminderTimeDtTmPkr, DateTime) <> CType(NextReminderTimeDtTmPkr1, DateTime) Then
                            ErrorReason &= vbNewLine & "CType(NextReminderTimeDtTmPkr, DateTime) <> CType(NextReminderTimeDtTmPkr1, DateTime)"
                            IfIsTrue = True
                        End If
                    ElseIf MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value <> RchTxtBxStyle(RchTxtBxStyleINX).Reminder_Every Then
                        ErrorReason &= vbNewLine & "MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & , & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & , & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value <> RchTxtBxStyle"
                        Dim ReminderEveryDaysNmrcUpDn = MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value
                        Dim ReminderEveryDaysNmrcUpDn1 = RchTxtBxStyle(RchTxtBxStyleINX).Reminder_Every

                        IfIsTrue = True
                    End If
                End If
NewNoteNotSaved:
                Dim AryInx = Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSN.Name)
                If AryInx = -1 Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "هذه الملاحظة (الملف) توجد مشكلة فى التحقق من وجود تعديل تم علها من عدمة ... هل تريد الاستمرار؟"
                    Else
                        Msg = "There Is A Problem With This Note (File) In Verifying Whether It Has Been Modified Or Not, Do You Want  Continue?"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                    If MyDialogResult = DialogResult.Yes Then
                        Continue For
                    Else
                        Exit Function
                    End If
                End If
                Dim rtf1 = CType(MagNoteRTF(AryInx), RichTextBox).Rtf
                Dim rtf2 = RCSN.Rtf
                Dim txt1 = CType(MagNoteRTF(AryInx), RichTextBox).Text
                Dim txt2 = RCSN.Text
                If rtf1 <> rtf2 Then
                    Dim x = 1
                End If
                If IfIsTrue Or
                    (txt1 <> txt2 And
                    Not IsNothing(MagNoteRTF)) Then
ExternalNoteNotSaved:
                    If WindowsShuttingDown Then
                        MagNote_Form.Warning_Before_Save_ChkBx.CheckState = CheckState.Unchecked
                        MagNote_Form.Save_Note_TlStrpBtn.PerformClick()
                    Else
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Msg = "هذه الملاحظة (الملف) تم اجراء تعديل عليها ولم يتم الحفظ بعد ... هل تريد حفظ الملاحظة والاستمرار؟"
                        Else
                            Msg = "This Note (File) Already Amendment, Do You Want To Save The Note And Continue?"
                        End If
                        Dim MyDialogResult = ShowMsg(Msg & vbNewLine & ErrorReason & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                        If MyDialogResult = DialogResult.Yes Then
                            MagNote_Form.Save_Note_TlStrpBtn.PerformClick()
                            If Not String.IsNullOrEmpty(NoteName) Then
                                Return DialogResult.Yes
                            End If
                            Continue For
                        ElseIf MyDialogResult = DialogResult.No Then
                            If Not String.IsNullOrEmpty(NoteName) Then
                                Return DialogResult.No
                            End If
                            Continue For
                        ElseIf MyDialogResult = DialogResult.Cancel Then
                            Return DialogResult.Cancel
                        End If
                    End If
                End If
            Next
            IgnoreNoteAmendmented = False
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public Function AddMagNoteRTF() As Boolean
        Try
            MagNote_Form.Cursor = Cursors.WaitCursor
            Dim RCSNName = RCSN.Name
            If IsNothing(MagNoteRTF) Then
                ReDim MagNoteRTF(0)
                MagNoteRTF(0) = New RichTextBox
                MagNoteRTF(0).Name = RCSN.Name
                MagNoteRTF(0).WordWrap = RCSN.WordWrap
                MagNoteRTF(0).ForeColor = Color.FromName(RCSN.ForeColor.Name)
                MagNoteRTF(0).BackColor = Color.FromName(RCSN.BackColor.Name)
                MagNoteRTF(0).Rtf = RCSN.Rtf
                MagNoteRTF(0).Font = RCSN.Font
                FillRchTxtBxStyle()
            ElseIf Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSNName) = -1 Then
                ReDim Preserve MagNoteRTF(MagNoteRTF.Length)
                MagNoteRTF(MagNoteRTF.Length - 1) = New RichTextBox
                MagNoteRTF(MagNoteRTF.Length - 1).Name = RCSN.Name
                MagNoteRTF(MagNoteRTF.Length - 1).WordWrap = RCSN.WordWrap
                MagNoteRTF(MagNoteRTF.Length - 1).ForeColor = Color.FromName(RCSN.ForeColor.Name)
                MagNoteRTF(MagNoteRTF.Length - 1).BackColor = Color.FromName(RCSN.BackColor.Name)
                MagNoteRTF(MagNoteRTF.Length - 1).Rtf = RCSN.Rtf
                MagNoteRTF(MagNoteRTF.Length - 1).Font = RCSN.Font
                FillRchTxtBxStyle()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MagNote_Form.Cursor = Cursors.Default
            Try
                MagNote_Form.RefreshNoteSetting(MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Tag,,,, 1)
            Catch ex As Exception
            End Try
        End Try
    End Function
    Public Function FillRchTxtBxStyle() As Boolean
        Try
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
            If File.Exists(RCSNName) Then
                RchTxtBxStyle(RchTxtBxStyleNo).Reminder_Every = MagNote_Form.Reminder_Every_Days_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Hours_NmrcUpDn.Value & "," & MagNote_Form.Reminder_Every_Minutes_NmrcUpDn.Value
                RchTxtBxStyle(RchTxtBxStyleNo).Next_Reminder_Time = MagNote_Form.Next_Reminder_Time_DtTmPkr.Value

                Dim stfnt = MagNote_Form.Note_Font_Name_CmbBx.Text & " - " & MagNote_Form.Note_Font_Size_CmbBx.Text & " - " & MagNote_Form.Note_Font_Style_CmbBx.Text
                RchTxtBxStyle(RchTxtBxStyleNo).Blocked_Note = MagNote_Form.Blocked_Note_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Finished_Note = MagNote_Form.Finished_Note_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Secured_Note = MagNote_Form.Secured_Note_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Font = stfnt
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = MagNote_Form.Note_Have_Reminder_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = MagNote_Form.Note_Have_Reminder_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Password = MagNote_Form.Note_Password_TxtBx.Text
                RchTxtBxStyle(RchTxtBxStyleNo).Use_Main_Password = MagNote_Form.Use_Main_Password_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).Word_Wrap = MagNote_Form.Note_Word_Wrap_ChkBx.CheckState
                RchTxtBxStyle(RchTxtBxStyleNo).ForeColor = MagNote_Form.Note_Font_Color_ClrCmbBx.Text
                RchTxtBxStyle(RchTxtBxStyleNo).BackColor = MagNote_Form.Note_Back_Color_ClrCmbBx.Text
            Else
                RchTxtBxStyle(RchTxtBxStyleNo).Reminder_Every = "0,0,0"
                RchTxtBxStyle(RchTxtBxStyleNo).Next_Reminder_Time = Now
                RchTxtBxStyle(RchTxtBxStyleNo).Blocked_Note = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Finished_Note = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Secured_Note = CheckState.Unchecked
                Dim stfnt = MagNote_Form.External_Note_Font_Name_CmbBx.Text & " - " & MagNote_Form.External_Note_Font_Size_CmbBx.Text & " - " & MagNote_Form.External_Note_Font_Style_CmbBx.Text
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Font = stfnt
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Have_Reminder = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Note_Password = Nothing
                RchTxtBxStyle(RchTxtBxStyleNo).Use_Main_Password = CheckState.Unchecked
                RchTxtBxStyle(RchTxtBxStyleNo).Word_Wrap = CheckState.Checked
                RchTxtBxStyle(RchTxtBxStyleNo).ForeColor = MagNote_Form.External_Note_Font_Color_ClrCmbBx.Text
                RchTxtBxStyle(RchTxtBxStyleNo).BackColor = MagNote_Form.External_Note_Back_Color_ClrCmbBx.Text
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Function CurrentMagNote() As String
        Try
            If MagNote_Form.MagNotes_Notes_TbCntrl.SelectedIndex = -1 Then Exit Function
            Dim CSI As String = vbNewLine & "----------------------------------------"
            If MagNote_Form.Language_Btn.Text = "E" Then
                CSI &= vbNewLine & "عنوان الملاحظة الحالية (" & MagNote_Form.MagNote_No_CmbBx.Text & ")"
                CSI &= vbNewLine & "إسم ملف الملاحظة الحالية (" & MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Name & ")"
            Else
                CSI &= vbNewLine & "Current Note Label (" & MagNote_Form.MagNote_No_CmbBx.Text & ")"
                CSI &= vbNewLine & "Current Note File Name (" & MagNote_Form.MagNotes_Notes_TbCntrl.SelectedTab.Name & ")"
            End If
            Return CSI
        Catch ex As Exception
        End Try
    End Function
    Public Function TackBackup() As Boolean
        Try
            MagNote_Form.Backup_Timer.Stop()
            Dim Msg As String
            If MagNote_Form.Backup_Folder_Path_TxtBx.TextLength = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "Backup Folder Not Prepared Yet!!!"
                Else
                    Msg = "المجلد الخاص بأخذ الاحتياطى لم يتم إعداده بعد!!!"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Function
            End If
            If Not MagNote_Form.backgroundWorker.IsBusy Then
                MagNote_Form.backgroundWorker.RunWorkerAsync()
            End If

            'Dim FolderName = MagNote_Form.Backup_Folder_Path_TxtBx.Text
            'FolderName = Replace(FolderName, "\\", "\")
            'Dim FileName = "\" & Replace(Replace(Replace(Replace(MagNote_Form.Next_Backup_Time_TxtBx.Text, ":", "-"), ".", "-"), "/", "-"), " ", "_") '
            'If (Not System.IO.Directory.Exists(FolderName)) Then
            '    System.IO.Directory.CreateDirectory(FolderName)
            'End If
            'If File.Exists(FolderName & FileName) Then
            '    Return True
            'End If
            'Dim CompresseFile = FolderName & FileName & ".7z"
            'Using AC As New RefreshWaitAWhileTitles_Class
            '    AC.RunCommandCom(Application.StartupPath & "\7za.exe", 1, MagNoteFolderPath & "\", CompresseFile, 1)
            'End Using
            'While ProcessRunning("7za")
            '    Application.DoEvents()
            'End While
            'If File.Exists(CompresseFile) Then
            '    ShowMsg("Backup Done Successfully" & vbNewLine & FolderName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            '    Return True
            'End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MagNote_Form.Backup_Timer.Start()
        End Try
    End Function
    Public Function ProcessRunning(ByVal strName As String, Optional ByVal ClosePrcss As Boolean = False) As Boolean
        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~'
        '               Check if exe exist in process list                           '
        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~'
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
    Public Sub AddHandler_Control_Move(ByVal sender As Control)
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
    Public Function UpdateAIOAssemblyVersion()
        Try
            If Debugger.IsAttached() Then
                Msg = "Do You Want To Update Assembly Version?"
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MBOs, False) = DialogResult.No Then
                    Exit Function
                End If
                Dim AssemblyFilePath As String = "AssemblyFilePath\AssemblyInfo.vb"
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
                                    Optional ByVal ReturnTrueOrFalse As Boolean = False)
        Dim CurentForm As Form = dataGridView.FindForm
        Try
            If Not dataGridView.Columns.Contains(columnToSearch) Then
                Throw New ArgumentException("The column named '" & columnToSearch & "' does not exists in the DataGridView.", "columnToSearch")
            End If
            If SelectDGVRow Then
                dataGridView.ClearSelection()
            End If
            If Not IsNothing(dataGridView.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) LCase(r.Cells(columnToSearch).Value.ToString()) = LCase(searchString))) Then
                Dim DGVRow = dataGridView.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(Function(r) LCase(r.Cells(columnToSearch).Value.ToString()) = LCase(searchString))
                If ReturnDGVRow Then
                    Return DGVRow
                ElseIf SelectDGVRow Then
                    dataGridView.Rows(DGVRow.Index).Selected = True
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
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function Load_Shortcuts_ToolTips(ByVal Shortcuts_LstVw As ListView) As Boolean
        Dim LifeLabelingAndTooltip As String = String.Empty
        LifeLabelingAndTooltip = MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml"
        If File.Exists(LifeLabelingAndTooltip) Then
            Dim ds As New DataSet
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(LifeLabelingAndTooltip, New XmlReaderSettings())
            ds.ReadXml(xmlFile)
            ToolTip1.UseAnimation = True
            ToolTip1.IsBalloon = True
            If IsNothing(Shortcuts_LstVw) Then Exit Function
            Shortcuts_LstVw.ShowItemToolTips = True
            ToolTip1.SetToolTip(Shortcuts_LstVw, "Shortcuts List View")
            For Each Shortcut In Shortcuts_LstVw.Items
                Dim CntrlInfo() As System.Data.DataRow = ds.Tables(0).Select("Form_Name = '" & MagNote_Form.Name & "' and Object_Name = '" & Shortcut.text & "'")
                If CntrlInfo.Length > 0 Then
                    Dim LstVw As ListViewItem = Shortcuts_LstVw.Items(Shortcuts_LstVw.Items.IndexOf(Shortcut))
                    If Not IsNothing(LstVw) Then
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            LstVw.Text = CntrlInfo(0).Item("Local_Language_Label")
                            LstVw.ToolTipText = CntrlInfo(0).Item("Local_Language_ToolTip")
                        Else
                            LstVw.Text = CntrlInfo(0).Item("Foreign_Language_Label")
                            LstVw.ToolTipText = CntrlInfo(0).Item("Foreign_Language_ToolTip")
                        End If
                        Application.DoEvents()
                    End If
                End If
            Next
            ToolTip1.Active = True
        End If
    End Function
    Public Function Labeling_Form(ByVal FormName As Form, ByVal Language As String) As Boolean
        Try
            Dim LifeLabelingAndTooltip As String = String.Empty
            If Debugger.IsAttached Then
                LifeLabelingAndTooltip = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\MagNotes_Files\Life_Labeling_And_Tooltip.xml"
            Else
                LifeLabelingAndTooltip = MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml"
            End If

            If File.Exists(LifeLabelingAndTooltip) Then
                Dim ds As New DataSet
                Dim xmlFile As XmlReader
                xmlFile = XmlReader.Create(LifeLabelingAndTooltip, New XmlReaderSettings())
                ds.ReadXml(xmlFile)
                ToolTip1.UseAnimation = True
                ToolTip1.IsBalloon = True
                For Each Cntrl In (FindControlRecursive(New List(Of Control), FormName, New List(Of Type)({GetType(Label), GetType(CheckBox), GetType(TabPage), GetType(Button)})))
                    Dim CntrlInfo() As System.Data.DataRow = ds.Tables(0).Select("Form_Name = '" & FormName.Name & "' and Object_Name = '" & Cntrl.Name & "'")
                    If CntrlInfo.Length > 0 Then
                        If Language = "Arabic" Then
                            Cntrl.Text = CntrlInfo(0).Item("Local_Language_Label")
                            ToolTip1.SetToolTip(Cntrl, CntrlInfo(0).Item("Local_Language_ToolTip"))
                        Else
                            Cntrl.Text = CntrlInfo(0).Item("Foreign_Language_Label")
                            ToolTip1.SetToolTip(Cntrl, CntrlInfo(0).Item("Foreign_Language_ToolTip"))
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public progress As Single = 0F
    Private Sub Control_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        e.Graphics.FillRectangle(Brushes.Green, New Rectangle(0, 0, CInt((sender.Width * progress)), sender.height))
        TextRenderer.DrawText(e.Graphics, sender.text & "%", SystemFonts.DefaultFont, New Rectangle(0, 2, sender.Width, sender.Height), Color.Black, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
    End Sub

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
