Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Speech.Synthesis
Imports System.Threading
Imports Microsoft.Win32.TaskScheduler
Imports System.Runtime.InteropServices
'Imports OpenQA.Selenium
'Imports OpenQA.Selenium.Chrome
Public Class Sticky_Note_Form
    Inherits System.Windows.Forms.Form
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.unnamed
    End Sub
    Dim HideStickyNote As Boolean
    Private printFont As Font
    Private streamToPrint As StreamReader
    Dim myWatch As SystemWideHotkeyComponent
    Dim WACls As WatsAppCls
    Dim KeyDownForFirstTime As Boolean = True
    Dim FileToPrint
    Dim SNN_SelectedItem As String = Nothing
    Dim NewSticky
    Private WithEvents Tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuDisplayForm As ToolStripMenuItem
    Private WithEvents MeAlwaysOnTop As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuSep2 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem
    Private WithEvents LockScreen As ToolStripMenuItem
    Private WithEvents Restart As ToolStripMenuItem
    Private WithEvents ShutdownWindows As ToolStripMenuItem
    Private WithEvents RestartWindows As ToolStripMenuItem
    Private WithEvents LogOffUser As ToolStripMenuItem
    Private Function ExitIfStickyNoteIsRunning() As Boolean
        Dim clsProcess As New Process   'create new instance of class process
        Dim ProcessCount = 0
        For Each clsProcess In Process.GetProcesses 'list all the processes
            If clsProcess.ProcessName = Application.ProductName Then
                ProcessCount += 1
                If ProcessCount > 1 Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "This Application Already Running!!!"
                    Else
                        Msg = "هذا البرنامج شغال بالفعل!!!"
                    End If
                    ShowMsg(Msg, "InfoSysMe (" & Application.ProductName & ")", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, False)
                    End
                End If
            End If
        Next
    End Function
    Private Sub myHotKeyPressed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.WindowState = FormWindowState.Normal
            If Not Me.Visible Then
                Me.ShowDialog()
            End If
            Me.BringToFront()
            If Not Me.TopMost Then
                Me.TopMost = True
            End If
            MakeTopMost(1)
            If Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked Then
                Me.TopMost = False
            End If
            If (Me.WindowState = FormWindowState.Normal And Me.Height <= 39) Or Me.WindowState = FormWindowState.Minimized Then
                'Sticky_Note_Form_SizeChanged(Me, EventArgs.Empty)
                Me.WindowState = FormWindowState.Maximized
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim rc As ResizeableControl_Class
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Sticky_Note_No_CmbBx.ValueMember = "Key"
            Sticky_Note_No_CmbBx.DisplayMember = "Value"

            Sticky_Note_Category_CmbBx.ValueMember = "Key"
            Sticky_Note_Category_CmbBx.DisplayMember = "Value"
            ' Creat new Button to resize the tree
            Control.CheckForIllegalCrossThreadCalls = False
            'CreatButtons(Me, Me, Form_Lbl)
            rc = New ResizeableControl_Class(Sticky_Note_Pnl)
            Selected_Text_Color_TlStrpMnItm.BackColor = Color.Red
            CloseProcess("cmd")
            StickyNoteFolderPath = Application.StartupPath & "\Sticky_Notes_Files"
            CreatListView()
            'Sticky_Note_TxtBx.DetectUrls = True
            ExitIfStickyNoteIsRunning()
            Form_ToolTip.BackColor = Color.Yellow
            MakeTopMost()
            If Available_Sticky_Notes_DGV.Columns.Count > 0 Then Exit Sub
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Note_Name", "Sticky Note Name")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Note_Label", "Sticky Note Label")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Note_Category", "Sticky Note Category")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Note", "Sticky Note")
            Available_Sticky_Notes_DGV.Columns.Add("Blocked_Sticky", "Blocked Sticky")
            Available_Sticky_Notes_DGV.Columns.Add("Finished_Sticky", "Finished Sticky")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Font", "Sticky Font")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Font_Color", "Sticky Font Color")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Back_Color", "Sticky Back Color")
            Available_Sticky_Notes_DGV.Columns.Add("Creation_Date", "Creation Date")
            Available_Sticky_Notes_DGV.Columns.Add("Secured_Sticky", "Secured Sticky")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Password", "Sticky Password")
            Available_Sticky_Notes_DGV.Columns.Add("Use_Main_Password", "Use Main Password")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Note_RTF", "Sticky Note RTF")

            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Word_Wrap", "Sticky Word Wrap")
            Available_Sticky_Notes_DGV.Columns.Add("Sticky_Have_Reminder", "Sticky Have Reminder")
            Available_Sticky_Notes_DGV.Columns.Add("Next_Reminder_Time", "Next Reminder Time")
            Available_Sticky_Notes_DGV.Columns.Add("Reminder_Every", "Reminder Every")


            Available_Sticky_Notes_DGV.Columns("Sticky_Note_RTF").Visible = False
            Sticky_Note_TxtBxAddHandler()
            Control.CheckForIllegalCrossThreadCalls = False
            myWatch = New SystemWideHotkeyComponent
            myWatch.Altkey = True
            myWatch.HotKey = Keys.S
            myWatch.ShiftKey = True
            myWatch.HotKey = Keys.F4
            AddHandler myWatch.HotkeyPressed, AddressOf myHotKeyPressed
            IgnoreStickyAmendmented = True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Application.DoEvents()
            Sticky_Note_TxtBx.Focus()
            Me.ActiveControl = Sticky_Note_TxtBx
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Dim UpdateFileName As String = String.Empty
    Dim UpdateFileVersion As String = String.Empty
    Dim UpdateDownloadFilePath As String = String.Empty
    Dim client As New WebClient()
    Private Function Check_For_New_Version() As Boolean
        Try
            If Language_Btn.Text = "E" Then
                Msg = "جارى التحقق من وجود تحديث للبرنامج"
            ElseIf Language_Btn.Text = "ع" Then
                Msg = "Checking To Find New Version Of The Progarm"
            End If
            ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)

            Dim Password As String = "M@g3lO3.C0m"
            Dim UserName As String = "2728786"
            Dim USNFI As String = Application.StartupPath & "\UpdateStickyNoteFileInformation.txt"
            Try
                Using client As New WebClient()
                    If File.Exists(USNFI) Then
                        File.Delete(USNFI)
                    End If
                    client.Credentials = New NetworkCredential(UserName, Password)
                    client.DownloadFile("ftp://2728786@p25-preview.awardspace.net/infosysme.com/UpdateStickyNoteFileInformation.txt", USNFI)
                End Using
            Catch ex As Exception
                If Language_Btn.Text = "E" Then
                    Msg = "لم تفلح محاولة التحقق من وجود تحيث للبرنامج"
                ElseIf Language_Btn.Text = "ع" Then
                    Msg = "Couldn't Check To Find New Version Of The Progarm"
                End If
                ShowMsg(Msg & vbNewLine & ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)
                Return False
            End Try
            If File.Exists(USNFI) Then
                Dim Sticky_Note() = Split(Replace(Replace(My.Computer.FileSystem.ReadAllText(USNFI), vbCrLf, ""), vbLf, ""), ",")
                For Each Line In Sticky_Note
                    If Microsoft.VisualBasic.Left(Line, 2) = "//" Then Continue For 'Ignore This Line
                    For Each item As String In Line.Split(":") '.Last.Split("|")
                        Select Case item
                            Case "Update File Name"
                                UpdateFileName = Split(Line, ":").ToList.Item(1)
                            Case "Update File Version"
                                UpdateFileVersion = Split(Line, ":").ToList.Item(1)
                            Case "Update Download File Path"
                                UpdateDownloadFilePath = Split(Line, ":").ToList.Item(1)
                        End Select
                    Next
                Next
                Dim Update_Version_TxtBx As New TextBox
                Dim CurrentVersionSplit() = Split(My.Application.Info.Version.ToString, ".")
                Dim UpgradeVersionSplit() = Split(UpdateFileVersion, "_")
                Dim Upgrade As Boolean
                Upgrade = False
                Dim Seq = 0
                For Each Value In CurrentVersionSplit
                    Select Case Seq
                        Case 0, 1, 3
                            If Val(CurrentVersionSplit(Seq)) < Val(UpgradeVersionSplit(Seq)) Then
                                Upgrade = True
                                Exit For
                            Else
                                Upgrade = False
                            End If
                        Case 2
                            If Val(Microsoft.VisualBasic.Left(UpgradeVersionSplit(Seq), 2)) <> Val(Microsoft.VisualBasic.Left(CurrentVersionSplit(Seq), 2)) Then
                                Upgrade = True
                                Exit For
                            Else
                                If Val(CurrentVersionSplit(Seq)) < Val(UpgradeVersionSplit(Seq)) Then
                                    Upgrade = True
                                    Exit For
                                Else
                                    Upgrade = False
                                End If
                            End If
                    End Select
                    Seq += 1
                Next
                If Upgrade Then
                    If Language_Btn.Text = "E" Then
                        Msg = "تمت بنجاح عملية محاولة التحقق من وجود تحيث للبرنامج"
                    ElseIf Language_Btn.Text = "ع" Then
                        Msg = "Successfully Checked To Find New Version Of The Progarm"
                    End If
                    ShowMsg(Msg & vbNewLine, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)
                    Return True
                Else
                    If Language_Btn.Text = "E" Then
                        Msg = "انت تستخدم آخر إصدار متاح للبرنامج"
                    ElseIf Language_Btn.Text = "ع" Then
                        Msg = "You Are Using The Last Available Version Of The Program"
                    End If
                    ShowMsg(Msg & vbNewLine, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)
                End If
            End If
        Catch ex As Exception
            If Language_Btn.Text = "E" Then
                Msg = "لم تفلح محاولة التحقق من وجود تحيث للبرنامج"
            ElseIf Language_Btn.Text = "ع" Then
                Msg = "Couldn't Check To Find New Version Of The Progarm"
            End If
            ShowMsg(Msg & vbNewLine & ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)
            Return False
        Finally
            client.Dispose()
        End Try
    End Function
    Private Sub SetAppParameters()
        Try
#Region "Read Parameters Setting"
            Dim Me_Always_On_Top_Value, Hide_Finished_Sticky_Note_Value, Run_Me_At_Windows_Startup_Value, Form_Color_Like_Sticky_Value, Save_Setting_When_Exit_Value, Note_Font_Value, Sticky_Form_Color_Value, Current_Language_Value, Sticky_Note_No_Value, Sticky_Form_Size_Value, Sticky_Form_Location_Value, Sticky_Form_Opacity_Value, Periodically_Backup_Stickies_Value, Backup_Time_Value, Next_Backup_Time_Value, Reload_Stickies_After_Amendments_Value, Enter_Password_To_Pass_Value, Complex_Password_Value, Main_Password_Value, Set_Control_To_Fill_Value, Warning_Before_Save_Value, Warning_Before_Delete_Value, Double_Click_To_Run_Shortcut_Value, Show_Form_Border_Style_Value, Enable_Maximize_Box_Value, Show_Sticky_Tab_Control_Value, Minimize_After_Running_My_Shortcut_Value, Me_As_Default_Text_File_Editor_Value, Remember_Opened_External_Files_Value
            Application.DoEvents()
            Dim FileName = StickyNoteFolderPath & "\Sticky_Note_Setting.txt"
            If File.Exists(FileName) Then
                Dim Sticky_Note() = Split(Replace(My.Computer.FileSystem.ReadAllText(FileName), vbCrLf, ""), ":")
                Dim ReadSticky As String = String.Empty
                Dim CaseSticky As String = String.Empty
                For Each Sticky In Sticky_Note
                    If String.IsNullOrEmpty(Sticky) Then Continue For
                    If Microsoft.VisualBasic.Left(Sticky, Len("Hide_Finished_Sticky_Note -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Hide_Finished_Sticky_Note -()-" Then
                        ReadSticky = "Hide_Finished_Sticky_Note"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Me_Always_On_Top -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Me_Always_On_Top -()-" Then
                        ReadSticky = "Me_Always_On_Top"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Run_Me_At_Windows_Startup -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Run_Me_At_Windows_Startup -()-" Then
                        ReadSticky = "Run_Me_At_Windows_Startup"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Form_Color_Like_Sticky -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Form_Color_Like_Sticky -()-" Then
                        ReadSticky = "Form_Color_Like_Sticky"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Save_Setting_When_Exit -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Save_Setting_When_Exit -()-" Then
                        ReadSticky = "Save_Setting_When_Exit"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Color -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Form_Color -()-" Then
                        ReadSticky = "Sticky_Form_Color"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Current_Language -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Current_Language -()-" Then
                        ReadSticky = "Current_Language"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Current_Sticky_Note_Name -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Current_Sticky_Note_Name -()-" Then
                        ReadSticky = "Current_Sticky_Note_Name"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Size -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Form_Size -()-" Then
                        ReadSticky = "Sticky_Form_Size"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Location -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Form_Location -()-" Then
                        ReadSticky = "Sticky_Form_Location"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Opacity -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Form_Opacity -()-" Then
                        ReadSticky = "Sticky_Form_Opacity"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Periodically_Backup_Stickies -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Periodically_Backup_Stickies -()-" Then
                        ReadSticky = "Periodically_Backup_Stickies"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Backup_Time -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Backup_Time -()-" Then
                        ReadSticky = "Backup_Time"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Next_Backup_Time -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Next_Backup_Time -()-" Then
                        ReadSticky = "Next_Backup_Time"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Reload_Stickies_After_Amendments -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Reload_Stickies_After_Amendments -()-" Then
                        ReadSticky = "Reload_Stickies_After_Amendments"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Enter_Password_To_Pass -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Enter_Password_To_Pass -()-" Then
                        ReadSticky = "Enter_Password_To_Pass"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Complex_Password -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Complex_Password -()-" Then
                        ReadSticky = "Complex_Password"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Main_Password -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Main_Password -()-" Then
                        ReadSticky = "Main_Password"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Set_Control_To_Fill -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Set_Control_To_Fill -()-" Then
                        ReadSticky = "Set_Control_To_Fill"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Warning_Before_Save -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Warning_Before_Save -()-" Then
                        ReadSticky = "Warning_Before_Save"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Warning_Before_Delete -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Warning_Before_Delete -()-" Then
                        ReadSticky = "Warning_Before_Delete"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Double_Click_To_Run_Shortcut -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Double_Click_To_Run_Shortcut -()-" Then
                        ReadSticky = "Double_Click_To_Run_Shortcut"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Show_Form_Border_Style -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Show_Form_Border_Style -()-" Then
                        ReadSticky = "Show_Form_Border_Style"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Enable_Maximize_Box -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Enable_Maximize_Box -()-" Then
                        ReadSticky = "Enable_Maximize_Box"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Remember_Opened_External_Files -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Remember_Opened_External_Files -()-" Then
                        ReadSticky = "Remember_Opened_External_Files"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Show_Sticky_Tab_Control -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Show_Sticky_Tab_Control -()-" Then
                        ReadSticky = "Show_Sticky_Tab_Control"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Minimize_After_Running_My_Shortcut -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Minimize_After_Running_My_Shortcut -()-" Then
                        ReadSticky = "Minimize_After_Running_My_Shortcut"
                    ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Me_As_Default_Text_File_Editor -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Me_As_Default_Text_File_Editor -()-" Then
                        ReadSticky = "Me_As_Default_Text_File_Editor"
                    End If
                    Select Case ReadSticky
                        Case "Me_Always_On_Top"
                            Me_Always_On_Top_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Me_Always_On_Top -(")), "")
                            Me_Always_On_Top_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Me_Always_On_Top_Value, Me_Always_On_Top_Value.Length - 2).ToString), CheckState)
                            ReadSticky = String.Empty
                        Case "Hide_Finished_Sticky_Note"
                            Hide_Finished_Sticky_Note_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Hide_Finished_Sticky_Note -(")), "")
                            Hide_Finished_Sticky_Note_Value = Microsoft.VisualBasic.Left(Hide_Finished_Sticky_Note_Value, Hide_Finished_Sticky_Note_Value.Length - 2)
                            Hide_Finished_Sticky_Note_ChkBx.CheckState = CType(Val(Hide_Finished_Sticky_Note_Value).ToString, CheckState)
                            ReadSticky = String.Empty
                        Case "Run_Me_At_Windows_Startup"
                            Run_Me_At_Windows_Startup_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Run_Me_At_Windows_Startup -(")), "")
                            Run_Me_At_Windows_Startup_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Run_Me_At_Windows_Startup_Value, Run_Me_At_Windows_Startup_Value.Length - 2).ToString), CheckState)
                            ReadSticky = String.Empty
                        Case "Form_Color_Like_Sticky"
                            Form_Color_Like_Sticky_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Form_Color_Like_Sticky -(")), "")
                            Form_Color_Like_Sticky_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Form_Color_Like_Sticky_Value, Form_Color_Like_Sticky_Value.Length - 2).ToString), CheckState)
                            ReadSticky = String.Empty
                        Case "Save_Setting_When_Exit"
                            Save_Setting_When_Exit_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Save_Setting_When_Exit -(")), "")
                            Save_Setting_When_Exit_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Save_Setting_When_Exit_Value, Save_Setting_When_Exit_Value.Length - 2).ToString), CheckState)
                            ReadSticky = String.Empty
                        Case "Sticky_Form_Color"
                            Sticky_Form_Color_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Color -(")), "")
                            Sticky_Form_Color_ClrCmbBx.Text = Microsoft.VisualBasic.Left(Sticky_Form_Color_Value, Sticky_Form_Color_Value.Length - 2)
                            ReadSticky = String.Empty
                        Case "Current_Language"
                            Current_Language_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Current_Language -(")), "")
                            Language_Btn.Text = Microsoft.VisualBasic.Left(Current_Language_Value, Current_Language_Value.Length - 2)
                            If Language_Btn.Text = "E" Then
                                Language_Btn.Text = "ع"
                            ElseIf Language_Btn.Text = "ع" Then
                                Language_Btn.Text = "E"
                            End If
                            Language_Btn_Click(Language_Btn, EventArgs.Empty)
                            ReadSticky = String.Empty
                        Case "Current_Sticky_Note_Name"
                            Sticky_Note_No_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Current_Sticky_Note_Name -(")), "")
                            'Sticky_Note_No_CmbBx.Focus()
                            'Me.ActiveControl = Sticky_Note_No_CmbBx
                            Sticky_Note_No_CmbBx.Text = Microsoft.VisualBasic.Left(Sticky_Note_No_Value, Sticky_Note_No_Value.Length - 2)
                            ReadSticky = String.Empty
                        Case "Sticky_Form_Size"
                            Sticky_Form_Size_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Size -(")), "")
                            Sticky_Form_Size_Value = Microsoft.VisualBasic.Left(Sticky_Form_Size_Value, Sticky_Form_Size_Value.Length - 2)
                            Sticky_Form_Size_TxtBx.Text = Sticky_Form_Size_Value
                            Sticky_Form_Size_Value = Replace(Replace(Replace(Sticky_Form_Size_Value, "{Width=", ""), " Height=", ""), "}", "")
                            Me.Size = New Point(Split(Sticky_Form_Size_Value, ",").ToList(0), Split(Sticky_Form_Size_Value, ",").ToList(1))
                            ReadSticky = String.Empty
                        Case "Sticky_Form_Location"
                            Sticky_Form_Location_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Location -(")), "")
                            Sticky_Form_Location_Value = Microsoft.VisualBasic.Left(Sticky_Form_Location_Value, Sticky_Form_Location_Value.Length - 2)
                            Sticky_Form_Location_TxtBx.Text = Sticky_Form_Location_Value
                            Sticky_Form_Location_Value = Replace(Replace(Replace(Sticky_Form_Location_Value, "{X=", ""), "Y=", ""), "}", "")
                            Me.Location = New Point(Split(Sticky_Form_Location_Value, ",").ToList(0), Split(Sticky_Form_Location_Value, ",").ToList(1))
                            ReadSticky = String.Empty
                        Case "Sticky_Form_Opacity"
                            Sticky_Form_Opacity_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Form_Opacity -(")), "")
                            Sticky_Form_Opacity_Value = Microsoft.VisualBasic.Left(Sticky_Form_Opacity_Value, Sticky_Form_Opacity_Value.Length - 2)
                            Sticky_Form_Opacity_TxtBx.Text = Sticky_Form_Opacity_Value
                            Form_Transparency_TrkBr.Value = Sticky_Form_Opacity_Value * 100
                            ReadSticky = String.Empty
                        Case "Periodically_Backup_Stickies"
                            Periodically_Backup_Stickies_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Periodically_Backup_Stickies -(")), "")
                            Periodically_Backup_Stickies_ChkBx.CheckState = Microsoft.VisualBasic.Left(Periodically_Backup_Stickies_Value, Periodically_Backup_Stickies_Value.Length - 2)
                            ReadSticky = String.Empty
                        Case "Backup_Time"
                            Backup_Time_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Backup_Time -(")), "")
                            Backup_Time_Value = Microsoft.VisualBasic.Left(Backup_Time_Value, Backup_Time_Value.Length - 2)
                            Days_NmrcUpDn.Value = Split(Backup_Time_Value, ",").ToList(0)
                            Hours_NmrcUpDn.Value = Split(Backup_Time_Value, ",").ToList(1)
                            Minutes_NmrcUpDn.Value = Split(Backup_Time_Value, ",").ToList(2)
                            ReadSticky = String.Empty
                            Application.DoEvents()
                        Case "Next_Backup_Time"
                            Next_Backup_Time_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Next_Backup_Time -(")), "")
                            Next_Backup_Time_TxtBx.Text = Replace(Microsoft.VisualBasic.Left(Next_Backup_Time_Value, Next_Backup_Time_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Reload_Stickies_After_Amendments"
                            Reload_Stickies_After_Amendments_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Reload_Stickies_After_Amendments -(")), "")
                            Reload_Stickies_After_Amendments_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Reload_Stickies_After_Amendments_Value, Reload_Stickies_After_Amendments_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Enter_Password_To_Pass"
                            Enter_Password_To_Pass_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Enter_Password_To_Pass -(")), "")
                            Enter_Password_To_Pass_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Enter_Password_To_Pass_Value, Enter_Password_To_Pass_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Complex_Password"
                            Complex_Password_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Complex_Password -(")), "")
                            Complex_Password_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Complex_Password_Value, Complex_Password_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Main_Password"
                            Main_Password_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Main_Password -(")), "")
                            Main_Password_TxtBx.Text = Decrypt_Function(Replace(Microsoft.VisualBasic.Left(Main_Password_Value, Main_Password_Value.Length - 2), ",", ":"))
                            ReadSticky = String.Empty
                        Case "Set_Control_To_Fill"
                            Set_Control_To_Fill_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Set_Control_To_Fill -(")), "")
                            Set_Control_To_Fill_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Set_Control_To_Fill_Value, Set_Control_To_Fill_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Warning_Before_Save"
                            Warning_Before_Save_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Warning_Before_Save -(")), "")
                            Warning_Before_Save_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Warning_Before_Save_Value, Warning_Before_Save_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Warning_Before_Delete"
                            Warning_Before_Delete_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Warning_Before_Delete -(")), "")
                            Warning_Before_Delete_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Warning_Before_Delete_Value, Warning_Before_Delete_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Double_Click_To_Run_Shortcut"
                            Double_Click_To_Run_Shortcut_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Double_Click_To_Run_Shortcut -(")), "")
                            Double_Click_To_Run_Shortcut_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Double_Click_To_Run_Shortcut_Value, Double_Click_To_Run_Shortcut_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Show_Form_Border_Style"
                            Show_Form_Border_Style_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Show_Form_Border_Style -(")), "")
                            Show_Form_Border_Style_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Show_Form_Border_Style_Value, Show_Form_Border_Style_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Enable_Maximize_Box"
                            Enable_Maximize_Box_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Enable_Maximize_Box -(")), "")
                            Enable_Maximize_Box_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Enable_Maximize_Box_Value, Enable_Maximize_Box_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Remember_Opened_External_Files"
                            Remember_Opened_External_Files_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Remember_Opened_External_Files -(")), "")
                            Remember_Opened_External_Files_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Remember_Opened_External_Files_Value, Remember_Opened_External_Files_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Show_Sticky_Tab_Control"
                            Show_Sticky_Tab_Control_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Show_Sticky_Tab_Control -(")), "")
                            Show_Sticky_Tab_Control_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Show_Sticky_Tab_Control_Value, Show_Sticky_Tab_Control_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Minimize_After_Running_My_Shortcut"
                            Minimize_After_Running_My_Shortcut_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Minimize_After_Running_My_Shortcut -(")), "")
                            Minimize_After_Running_My_Shortcut_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Minimize_After_Running_My_Shortcut_Value, Minimize_After_Running_My_Shortcut_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                        Case "Me_As_Default_Text_File_Editor"
                            Me_As_Default_Text_File_Editor_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Me_As_Default_Text_File_Editor -(")), "")
                            Me_As_Default_Text_File_Editor_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Me_As_Default_Text_File_Editor_Value, Me_As_Default_Text_File_Editor_Value.Length - 2), ",", ":")
                            ReadSticky = String.Empty
                    End Select
                Next
            End If
            FileName = StickyNoteFolderPath & "\FilesShortcuts.txt"
            LoadList(FileName)
#End Region
            If String.IsNullOrEmpty(Sticky_Note_No_Value) Then
                Exit Sub
            End If
            If Sticky_Note_No_Value.Length > 0 Then
                Sticky_Note_No_CmbBx.Text = Microsoft.VisualBasic.Left(Sticky_Note_No_Value, Sticky_Note_No_Value.Length - 2)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Application.DoEvents()
        End Try
    End Sub
    Private Sub TabControl_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Setting_TbCntrl.DrawItem
        Try
            If Correct_Sticky_Note_TxtBx_Font_Color(1) < 0.4 Then
                e.Graphics.FillRectangle(New SolidBrush(Color.White), e.Bounds)
            Else
                e.Graphics.FillRectangle(New SolidBrush(Sticky_Note_TxtBx.BackColor), e.Bounds)
            End If

            Dim paddedBounds As Rectangle = e.Bounds
            paddedBounds.Inflate(-2, -2)
            e.Graphics.DrawString(Setting_TbCntrl.Text, Me.Font, SystemBrushes.HighlightText, paddedBounds)

            Dim tabContas As TabControl = DirectCast(sender, TabControl)
            Dim sTexto As String = tabContas.TabPages(e.Index).Text
            Dim g As Graphics = e.Graphics
            Dim fonte As Font = tabContas.Font
            Dim format = New System.Drawing.StringFormat
            'CHANGES HERE...
            format.Alignment = StringAlignment.Center
            format.LineAlignment = StringAlignment.Center
            Dim pincel As New SolidBrush(Sticky_Note_TxtBx.ForeColor)
            If Correct_Sticky_Note_TxtBx_Font_Color(1) < 0.4 Then
                pincel = New SolidBrush(Color.Black)
            Else
                pincel = New SolidBrush(Sticky_Note_TxtBx.ForeColor)
            End If

            'RENEMED VARIEBLE HERE...
            '        Dim retangulo As RectangleF = RectangleF.op_Implicit(tabContas.GetTabRect(e.Index))
            Dim retangulo As RectangleF = RectangleF.op_Implicit(tabContas.GetTabRect(e.Index))
            If tabContas.SelectedIndex = e.Index Then
                fonte = New Font(fonte, FontStyle.Bold)
                pincel = New SolidBrush(Color.Green)
                'CHANGED BACKGROUN COLOR HERE...
                g.FillRectangle(Brushes.White, retangulo)
            End If
            g.DrawString(sTexto, fonte, pincel, retangulo, format)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
#Region "Sticky_Note_TxtBxAddHandler"
    Private Function Sticky_Note_TxtBxAddHandler()
        Try
            Sticky_Note_TxtBx.ContextMenuStrip = New ContextMenuStrip()
            Dim SelectAll_TxtBx As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                SelectAll_TxtBx.Text = "Select All"
            Else
                SelectAll_TxtBx.Text = "إختر الكل"
            End If
            SelectAll_TxtBx.Tag = Sticky_Note_TxtBx.Name
            SelectAll_TxtBx.Name = "SelectAll" & Sticky_Note_TxtBx.Name
            SelectAll_TxtBx.BackgroundImage = My.Resources.Background4
            SelectAll_TxtBx.Image = My.Resources.SelectAll
            Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(SelectAll_TxtBx)
            SelectAll_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler SelectAll_TxtBx.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
            '-------------------------------------------
            If Not LCase(Sticky_Note_TxtBx.Name).Contains("password") Then
                Dim Copy_TxtBx As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    Copy_TxtBx.Text = "Copy"
                Else
                    Copy_TxtBx.Text = "نسخ"
                End If
                Copy_TxtBx.Tag = Sticky_Note_TxtBx.Name
                Copy_TxtBx.Name = "Copy" & Sticky_Note_TxtBx.Name
                Copy_TxtBx.BackgroundImage = My.Resources.Background4
                Copy_TxtBx.Image = My.Resources.copy
                Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(Copy_TxtBx)
                Copy_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler Copy_TxtBx.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
            End If
            '-------------------------------------------
            If Not LCase(Sticky_Note_TxtBx.Name).Contains("password") Then
                Dim Cut_TxtBx As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    Cut_TxtBx.Text = "Cut"
                Else
                    Cut_TxtBx.Text = "قص"
                End If
                Cut_TxtBx.Name = "Cut" & Sticky_Note_TxtBx.Name
                Cut_TxtBx.Tag = Sticky_Note_TxtBx.Name
                Cut_TxtBx.Image = My.Resources.cut
                Cut_TxtBx.BackgroundImage = My.Resources.Background4
                Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(Cut_TxtBx)
                Cut_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler Cut_TxtBx.Click, AddressOf Cut_TxtBx_To_Cliboard_Click
            End If
            '-------------------------------------------
            Dim Past_TxtBx As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Past_TxtBx.Text = "Past"
            Else
                Past_TxtBx.Text = "لصق"
            End If
            Past_TxtBx.Name = "Past" & Sticky_Note_TxtBx.Name
            Past_TxtBx.Tag = Sticky_Note_TxtBx.Name
            Past_TxtBx.Image = My.Resources.paste
            Past_TxtBx.BackgroundImage = My.Resources.Background4
            Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(Past_TxtBx)
            Past_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Past_TxtBx.Click, AddressOf Past_TxtBx_To_Cliboard_Click
            '-------------------------------------------
            Dim Create_Link_TxtBx As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Create_Link_TxtBx.Text = "Create Link"
            Else
                Create_Link_TxtBx.Text = "إنشاء الرابط"
            End If
            Create_Link_TxtBx.Name = " Create_Link" & Sticky_Note_TxtBx.Name
            Create_Link_TxtBx.Tag = Sticky_Note_TxtBx.Name
            Create_Link_TxtBx.BackgroundImage = My.Resources.Background4
            Create_Link_TxtBx.Image = My.Resources.link1
            Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(Create_Link_TxtBx)
            Create_Link_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Create_Link_TxtBx.Click, AddressOf Create_Link_TxtBx_Click
            '-------------------------------------------
            Dim SetControlToFill As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                SetControlToFill.Text = "Set Control To Fill"
            Else
                SetControlToFill.Text = "إختيار عنصر التعبئة"
            End If
            SetControlToFill.Name = "SetControlToFill" & Sticky_Note_TxtBx.Name
            SetControlToFill.Tag = Sticky_Note_TxtBx.Name
            SetControlToFill.BackgroundImage = My.Resources.Background4
            SetControlToFill.Image = My.Resources.SizeAll_White
            Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(SetControlToFill)
            SetControlToFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Dim SetFillToSticky As New ToolStripMenuItem()
            If Language_Btn.Text = "ع" Then
                SetFillToSticky.Text = "Set Fill To Sticky"
            Else
                SetFillToSticky.Text = "إختيار التعبئة للاصقة"
            End If
            SetFillToSticky.Name = "SetFillToSticky" & Sticky_Note_TxtBx.Name
            SetFillToSticky.Tag = Sticky_Note_TxtBx.Name
            SetFillToSticky.BackgroundImage = My.Resources.Background4
            SetFillToSticky.Image = My.Resources.SizeAll_White
            Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(SetFillToSticky)
            SetFillToSticky.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler SetFillToSticky.Click, AddressOf SetFillToSticky_Click 'add the click handler sub
            'Create another new ToolStripMenuItem to add to the ViewPaperToolStripMenuItem
            Dim SetFillToControlTabs As New ToolStripMenuItem()
            If Language_Btn.Text = "ع" Then
                SetFillToControlTabs.Text = "Set Fill To Control Tabs"
            Else
                SetFillToControlTabs.Text = "إختيار التعبئة لصفحات التبويب"
            End If
            SetFillToControlTabs.Name = "SetFillToControlTabs" & Sticky_Note_TxtBx.Name
            SetFillToControlTabs.Tag = Sticky_Note_TxtBx.Name
            SetFillToControlTabs.BackgroundImage = My.Resources.Background4
            SetFillToControlTabs.Image = My.Resources.SizeAll_White
            Sticky_Note_TxtBx.ContextMenuStrip.Items.Add(SetFillToControlTabs)
            SetFillToControlTabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler SetFillToControlTabs.Click, AddressOf SetFillToControlTabs_Click 'add the click handler sub
            'add the new ToolStripMenuItems to the ViewPaperToolStripMenuItem`s DropDownItems
            SetControlToFill.DropDownItems.Add(SetFillToSticky)
            SetControlToFill.DropDownItems.Add(SetFillToControlTabs)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub Clipboard_Cut()
        Try
            Me.Cursor = Cursors.WaitCursor
            My.Computer.Clipboard.Clear()
            Clipboard.SetText(Sticky_Note_TxtBx.SelectedText)
            Sticky_Note_TxtBx.SelectedText = ""
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Cut_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                Clipboard_Cut()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Clipboard_Past()
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Sticky_Note_TxtBx) Then Exit Sub
            Dim iData As IDataObject = Clipboard.GetDataObject()
            'Check to see if the data is in a text format
            If iData.GetDataPresent(DataFormats.Text) Then
                'If it's text, then paste it into the textbox
                If Not IsNothing(iData.GetData(DataFormats.Text)) Then
                    Try
                        Sticky_Note_TxtBx.SelectedText = CType(iData.GetData(DataFormats.UnicodeText), String)
                    Catch ex As Exception
                        Sticky_Note_TxtBx.SelectedText = CType(iData.GetData(DataFormats.Text), String)
                    End Try
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Public Sub Clipboard_Copy()
        Try
            Me.Cursor = Cursors.WaitCursor
            Try
                If (Me.Sticky_Note_TxtBx.SelectionType And RichTextBoxSelectionTypes.Object) = RichTextBoxSelectionTypes.Object Then
                    Clipboard.Clear()
                    Sticky_Note_TxtBx.Copy()
                    Dim idata As IDataObject = Clipboard.GetDataObject()
                    If idata.GetDataPresent(DataFormats.Bitmap) Then
                        Dim imgObject As Object = idata.GetData(DataFormats.Bitmap)
                        If imgObject IsNot Nothing Then
                            Dim img As Image = TryCast(imgObject, Image)
                            If img IsNot Nothing Then
                                Dim x = 1
                            End If
                        End If
                    Else
                        My.Computer.Clipboard.SetText(Sticky_Note_TxtBx.SelectedText, TextDataFormat.UnicodeText)
                    End If
                Else
                    My.Computer.Clipboard.SetText(Sticky_Note_TxtBx.SelectedText, TextDataFormat.UnicodeText)
                End If
            Catch ex As Exception
                My.Computer.Clipboard.SetText(Sticky_Note_TxtBx.SelectedText, TextDataFormat.Text)
            End Try
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Copy_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                Dim ColumnsTooTibText = Nothing
                My.Computer.Clipboard.Clear()
                If (sender.text.contains("إختر الكل") Or sender.text.contains("Select All")) And
                    (Objct.GetType = GetType(System.Windows.Forms.TextBox) Or
                    Objct.GetType = GetType(System.Windows.Forms.RichTextBox)) Then
                    Objct.SelectAll()
                Else
                    Clipboard_Copy()
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Dim FileLink = ""

    Private Sub SetFillToControlTabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Set_Control_To_Fill_ChkBx.CheckState = CheckState.Unchecked
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub SetFillToSticky_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Set_Control_To_Fill_ChkBx.CheckState = CheckState.Checked
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Create_Link_TxtBx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim LinkAdded As String = String.Empty
            Sticky_Note_TxtBx.Focus()
            Dim Clipboarddata As IDataObject = Clipboard.GetDataObject
            If Clipboarddata.GetDataPresent(DataFormats.FileDrop) Then
                For Each s As String In Clipboarddata.GetData(DataFormats.FileDrop)
                    FileLink = "file:///" & Replace(s.ToString, " ", "%20")
                    Sticky_Note_TxtBx.AppendText(Path.GetFileName(s) & Convert.ToChar(Keys.Tab) & Convert.ToChar(Keys.Tab) & Convert.ToChar(Keys.Tab) & FileLink & vbNewLine)
                    Sticky_Note_TxtBx.SelectionStart = Sticky_Note_TxtBx.TextLength
                    Application.DoEvents()
                    LinkAdded &= "This Link Appended To The Current Sticky Note" & FileLink & vbNewLine
                Next
            End If
            If Not String.IsNullOrEmpty(LinkAdded) Then
                ShowMsg(LinkAdded, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Past_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Clipboard.GetImage()
            Clipboard.GetText()
            Sticky_Note_TxtBx.Paste()
            Sticky_Note_TxtBx.Select()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region
    Dim ArowsKeyDown As Boolean
    Private Sub Sticky_Note_Form_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Exit_TlStrpBtn.PerformClick()
            ElseIf e.KeyCode = Keys.F3 Then
                If CType(Form_ToolTip, ToolTip).Active = False Then
                    CType(Form_ToolTip, ToolTip).Active = True
                Else
                    CType(Form_ToolTip, ToolTip).Active = False
                End If
            ElseIf e.KeyCode = Keys.Left Or
                e.KeyCode = Keys.Right Or
                e.KeyCode = Keys.Up Or
                e.KeyCode = Keys.Down Or
                e.KeyCode = Keys.PageUp Or
                e.KeyCode = Keys.PageDown Then
                ArowsKeyDown = True
            ElseIf (e.Control = True And e.KeyCode = Keys.F) Then
                Find_TlStrpBtn.PerformClick()
            ElseIf e.KeyCode = Keys.F4 And
            KeyDownForFirstTime Then
                CallByName(Me, "Pause_Btn_Click", CallType.Method, Start_Btn, EventArgs.Empty)
            ElseIf (e.Shift = True And e.KeyCode = Keys.F4) Then
                CallByName(Me, "Stop_Btn_Click", CallType.Method, Stop_Btn, EventArgs.Empty)
            Else
                ArowsKeyDown = False
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Sub View_Btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles View_Sticky_Notes_Btn.Click
        Try
            Select Case sender.name
                Case View_Sticky_Notes_Btn.Name
            End Select
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim Reminders As New List(Of Reminderlist)
    Private Sub Preview_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Btn.Click
        Available_Sticky_Notes_DGV.Rows.Clear()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim SelectedIindex
            If Sticky_Note_Category_CmbBx.SelectedIndex = -1 Then
                Sticky_Note_Category_CmbBx.Items.Clear()
                Dim CNF = Application.StartupPath & "\Sticky_Notes_Files"
                Dim Categoris() As String = System.IO.Directory.GetFiles(CNF)
                For Each fil In Categoris
                    Dim v As Integer = Len("Category -(")
                    If Microsoft.VisualBasic.Left(Path.GetFileName(fil), v) <> "Category -(" Then
                        Continue For
                    End If
                    If UCase(Path.GetExtension(fil)) = UCase(".txt") Then
                        Dim CategoryLabel = Replace(My.Computer.FileSystem.ReadAllText(fil), vbCrLf, "")
                        Sticky_Note_Category_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Path.GetFileName(fil), CategoryLabel))
                    End If
                Next
                SelectedIindex = String.Empty
            Else
                If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
                    SelectedIindex = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                End If
            End If
            Reminders.Clear()
            Reminders = New List(Of Reminderlist)
            Dim SelectedItem = Sticky_Note_No_CmbBx.Text
            Sticky_Note_No_CmbBx.Items.Clear()
            Dim SNF = Application.StartupPath & "\Sticky_Notes_Files"
            Dim Fils() As String = System.IO.Directory.GetFiles(SNF)
            For Each fil In Fils
                Dim v As Integer = Len("Sticky_Note -(")
                If Microsoft.VisualBasic.Left(Path.GetFileName(fil), v) <> "Sticky_Note -(" Then
                    Continue For
                End If
                If UCase(Path.GetExtension(fil)) = UCase(".txt") Then
                    If Not String.IsNullOrEmpty(SelectedIindex) Then
                        If Path.GetFileNameWithoutExtension(fil) = SelectedIindex Then
                            ReadFile(fil,, 1)
                            Continue For
                        End If
                    End If
                    ReadFile(fil)
                End If
            Next
            Available_Sticky_Notes_DGV.Sort(Available_Sticky_Notes_DGV.Columns("Creation_Date"), System.ComponentModel.ListSortDirection.Descending)
            Available_Sticky_Notes_DGV.ClearSelection()
            Sticky_Note_No_CmbBx.Text = Nothing
            Sticky_Note_No_CmbBx.SelectedIndex = -1
            Sticky_Note_No_CmbBx.Text = SelectedItem
            If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    If Sticky.cells("Sticky_Note_Name").value = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key Then
                        Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV(0, Sticky.index)
                        Exit For
                    End If
                Next
            End If
            LoadExternalFiles()
            ShowMsg("Loading Available Sticky Notes Finished", "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LoadExternalFiles()
        Try
            Me.Cursor = Cursors.WaitCursor
            If Remember_Opened_External_Files_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Sub
            End If
            Dim OpenedExternalFiles = Application.StartupPath & "\OpenedExternalFiles.txt"
            If File.Exists(OpenedExternalFiles) Then
                For Each ExternalFiles In Split(My.Computer.FileSystem.ReadAllText(OpenedExternalFiles), vbLf)
                    ExternalFiles = Replace(ExternalFiles, vbCr, "")
                    If File.Exists(ExternalFiles) Then
                        Dim Index = Sticky_Note_No_CmbBx.FindStringExact(Path.GetFileName(ExternalFiles))
                        Application.DoEvents()
                        If Index <> -1 And Not IsNothing(Sticky_Note_No_CmbBx.SelectedItem) Then
                            If DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = Path.GetDirectoryName(ExternalFiles) Then
                                Continue For
                            End If
                        ElseIf Index = -1 Then
                            Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Path.GetDirectoryName(ExternalFiles), Path.GetFileName(ExternalFiles)))
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Structure Reminderlist
        Public StickyName As String
        Public StickyLabel As String
        Public ReminderTime As String
        Public Sub New(Name As String, Time As String, Label As String)
            Me.StickyName = Name
            Me.StickyLabel = Label
            Me.ReminderTime = Time
        End Sub
    End Structure
    Dim IgnoreReadFile As Boolean
    Private Function ReadFile(ByVal fil As String,
                                               Optional ByVal JustRead As Boolean = False,
                                               Optional ByVal AddFile As Boolean = False,
                                               Optional ByVal ReplaceDGVR As DataGridViewRow = Nothing) As Object
        Try
            Me.Cursor = Cursors.WaitCursor
            If IgnoreReadFile Or Not File.Exists(fil) Then Exit Function
            Dim Sticky_Note_Name_Value,
                    Sticky_Note_Label_Value,
                    Sticky_Note_Category_Value,
                    Sticky_Note_Value,
                    Blocked_Sticky_Value,
                    Finished_Sticky_Value,
                    Sticky_Font_Value,
                    Sticky_Font_Color_Value,
                    Sticky_Back_Color_Value,
                    Creation_Date_Value,
                    Secured_Sticky_Value,
                    Sticky_Password_Value,
                    Use_Main_Password_Value,
                    Sticky_Note_RTF_Value,
                    Sticky_Word_Wrap_Value,
                    Sticky_Have_Reminder_Value,
                    Next_Reminder_Time_Value,
                    Reminder_Every_Value
            Dim Sticky_Note() = Split(Replace(My.Computer.FileSystem.ReadAllText(fil), vbCrLf, ""), ":")
            Dim ReadSticky As String = String.Empty
            Dim CaseSticky As String = String.Empty
            For Each Sticky In Sticky_Note
                If String.IsNullOrEmpty(Sticky) Then Continue For
                If Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note_Name -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Note_Name -()-" Then
                    ReadSticky = "Sticky_Note_Name"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note_Label -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Note_Label -()-" Then
                    ReadSticky = "Sticky_Note_Label"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note_Category -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Note_Category -()-" Then
                    ReadSticky = "Sticky_Note_Category"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Note -()-" Then
                    ReadSticky = "Sticky_Note"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Blocked_Sticky -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Blocked_Sticky -()-" Then
                    ReadSticky = "Blocked_Sticky"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Finished_Sticky -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Finished_Sticky -()-" Then
                    ReadSticky = "Finished_Sticky"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Font -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Font -()-" Then
                    ReadSticky = "Sticky_Font"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Font_Color -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Font_Color -()-" Then
                    ReadSticky = "Sticky_Font_Color"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Back_Color -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Back_Color -()-" Then
                    ReadSticky = "Sticky_Back_Color"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Creation_Date -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Creation_Date -()-" Then
                    ReadSticky = "Creation_Date"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Secured_Sticky -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Secured_Sticky -()-" Then
                    ReadSticky = "Secured_Sticky"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Password -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Password -()-" Then
                    ReadSticky = "Sticky_Password"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Use_Main_Password -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Use_Main_Password -()-" Then
                    ReadSticky = "Use_Main_Password"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Word_Wrap -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Word_Wrap -()-" Then
                    ReadSticky = "Sticky_Word_Wrap"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Have_Reminder -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Sticky_Have_Reminder -()-" Then
                    ReadSticky = "Sticky_Have_Reminder"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Next_Reminder_Time -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Next_Reminder_Time -()-" Then
                    ReadSticky = "Next_Reminder_Time"
                ElseIf Microsoft.VisualBasic.Left(Sticky, Len("Reminder_Every -(")) & Microsoft.VisualBasic.Right(Sticky, 2) = "Reminder_Every -()-" Then
                    ReadSticky = "Reminder_Every"
                End If
                Select Case ReadSticky
                    Case "Sticky_Note_Name"
                        Sticky_Note_Name_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note_Name -(")), "")
                        Sticky_Note_Name_Value = Microsoft.VisualBasic.Left(Sticky_Note_Name_Value, Sticky_Note_Name_Value.Length - 2)
                        ReadSticky = String.Empty
                        Dim SNN = Path.GetFileNameWithoutExtension(fil)
                        If SNN <> Sticky_Note_Name_Value Then
                            Sticky_Note_Name_Value = SNN
                        End If
                    Case "Sticky_Note_Label"
                        Sticky_Note_Label_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note_Label -(")), "")
                        Sticky_Note_Label_Value = Microsoft.VisualBasic.Left(Sticky_Note_Label_Value, Sticky_Note_Label_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Note_Category"
                        Sticky_Note_Category_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note_Category -(")), "")
                        Sticky_Note_Category_Value = Microsoft.VisualBasic.Left(Sticky_Note_Category_Value, Sticky_Note_Category_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Note"
                        Sticky_Note_RTF_Value = Sticky
                        Sticky_Note_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note -(")), "")
                        Sticky_Note_Value = Microsoft.VisualBasic.Left(Sticky_Note_Value, Sticky_Note_Value.Length - 2)
                        Sticky_Note_Value = Sticky_Note_Value
                        Sticky_Note_RTF_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Note -(")), "")
                        Sticky_Note_RTF_Value = Microsoft.VisualBasic.Left(Sticky_Note_RTF_Value, Sticky_Note_RTF_Value.Length - 2)
                        Sticky_Note_RTF_Value = Sticky_Note_RTF_Value
                        ReadSticky = String.Empty
                    Case "Blocked_Sticky"
                        Blocked_Sticky_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Blocked_Sticky -(")), "")
                        Blocked_Sticky_Value = Microsoft.VisualBasic.Left(Blocked_Sticky_Value, Blocked_Sticky_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Finished_Sticky"
                        Finished_Sticky_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Finished_Sticky -(")), "")
                        Finished_Sticky_Value = Microsoft.VisualBasic.Left(Finished_Sticky_Value, Finished_Sticky_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Font"
                        Sticky_Font_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Font -(")), "")
                        Sticky_Font_Value = Microsoft.VisualBasic.Left(Sticky_Font_Value, Sticky_Font_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Font_Color"
                        Sticky_Font_Color_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Font_Color -(")), "")
                        Sticky_Font_Color_Value = Microsoft.VisualBasic.Left(Sticky_Font_Color_Value, Sticky_Font_Color_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Back_Color"
                        Sticky_Back_Color_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Back_Color -(")), "")
                        Sticky_Back_Color_Value = Microsoft.VisualBasic.Left(Sticky_Back_Color_Value, Sticky_Back_Color_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Creation_Date"
                        Sticky = Replace(Sticky, ",", ":")
                        Creation_Date_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Creation_Date -(")), "")
                        Creation_Date_Value = Microsoft.VisualBasic.Left(Creation_Date_Value, Creation_Date_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Secured_Sticky"
                        Secured_Sticky_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Secured_Sticky -(")), "")
                        Secured_Sticky_Value = Microsoft.VisualBasic.Left(Secured_Sticky_Value, Secured_Sticky_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Password"
                        Sticky_Password_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Password -(")), "")
                        Sticky_Password_Value = Microsoft.VisualBasic.Left(Sticky_Password_Value, Sticky_Password_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Use_Main_Password"
                        Use_Main_Password_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Use_Main_Password -(")), "")
                        Use_Main_Password_Value = Microsoft.VisualBasic.Left(Use_Main_Password_Value, Use_Main_Password_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Word_Wrap"
                        Sticky_Word_Wrap_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Word_Wrap -(")), "")
                        Sticky_Word_Wrap_Value = Microsoft.VisualBasic.Left(Sticky_Word_Wrap_Value, Sticky_Word_Wrap_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Sticky_Have_Reminder"
                        Sticky_Have_Reminder_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Sticky_Have_Reminder -(")), "")
                        Sticky_Have_Reminder_Value = Microsoft.VisualBasic.Left(Sticky_Have_Reminder_Value, Sticky_Have_Reminder_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Next_Reminder_Time"
                        Next_Reminder_Time_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Next_Reminder_Time -(")), "")
                        Next_Reminder_Time_Value = Microsoft.VisualBasic.Left(Next_Reminder_Time_Value, Next_Reminder_Time_Value.Length - 2)
                        ReadSticky = String.Empty
                    Case "Reminder_Every"
                        Reminder_Every_Value = Replace(Sticky, Microsoft.VisualBasic.Left(Sticky, Len("Reminder_Every -(")), "")
                        Reminder_Every_Value = Microsoft.VisualBasic.Left(Reminder_Every_Value, Reminder_Every_Value.Length - 2)
                        ReadSticky = String.Empty
                End Select
            Next
            If Hide_Finished_Sticky_Note_ChkBx.CheckState = CheckState.Checked And
                        Finished_Sticky_Value = 1 Then
            Else
                If Not CType(Val(Secured_Sticky_Value), Boolean) Then
                    Sticky_Note_Value = ConvertRichTextBox(Decrypt_Function(Sticky_Note_Value))
                    Sticky_Note_RTF_Value = Decrypt_Function(Sticky_Note_RTF_Value)
                End If
                If JustRead Or Not IsNothing(ReplaceDGVR) Then
                    Dim array() As String = {Sticky_Note_Name_Value, Sticky_Note_Label_Value, Sticky_Note_Category_Value, Sticky_Note_Value, Blocked_Sticky_Value, Finished_Sticky_Value, Sticky_Font_Value, Sticky_Font_Color_Value, Sticky_Back_Color_Value, Creation_Date_Value, Secured_Sticky_Value, Sticky_Password_Value, Use_Main_Password_Value, Sticky_Note_RTF_Value, Sticky_Word_Wrap_Value, Sticky_Have_Reminder_Value, Next_Reminder_Time_Value, Reminder_Every_Value}
                    If JustRead Then
                        Return array  ' Sticky_Note_Value
                    Else
                        Available_Sticky_Notes_DGV.ReadOnly = False
                        For Each Cell As DataGridViewCell In ReplaceDGVR.Cells
                            Available_Sticky_Notes_DGV.Rows(ReplaceDGVR.Index).Cells(Cell.ColumnIndex).Value = array(Cell.ColumnIndex)
                        Next
                        Available_Sticky_Notes_DGV.ReadOnly = True
                        Exit Function
                    End If
                End If
                If Not AddFile Then
                    If Sticky_Note_Category_CmbBx.SelectedIndex <> -1 Then
                        If (Sticky_Note_Category_Value <> DirectCast(Sticky_Note_Category_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) Then
                            Exit Function
                        End If
                    End If
                End If
                If String.IsNullOrEmpty(Sticky_Note_Label_Value) Then
                    Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Sticky_Note_Name_Value, Sticky_Note_Name_Value))
                Else
                    Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Sticky_Note_Name_Value, Sticky_Note_Label_Value))
                End If
                Available_Sticky_Notes_DGV.Rows.Add(Sticky_Note_Name_Value, Sticky_Note_Label_Value, Sticky_Note_Category_Value, Sticky_Note_Value, Blocked_Sticky_Value, Finished_Sticky_Value, Sticky_Font_Value, Sticky_Font_Color_Value, Sticky_Back_Color_Value, Creation_Date_Value, Secured_Sticky_Value, Sticky_Password_Value, Use_Main_Password_Value, Sticky_Note_RTF_Value, Sticky_Word_Wrap_Value, Sticky_Have_Reminder_Value, Next_Reminder_Time_Value, Reminder_Every_Value)
            End If
            If CType(Val(Sticky_Have_Reminder_Value), Boolean) Then
                Reminders.Add(New Reminderlist(Sticky_Note_Name_Value, Next_Reminder_Time_Value, Sticky_Note_Label_Value))
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub Available_Sticky_Notes_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_Sticky_Notes_DGV.CellContentClick

    End Sub
    Public Sub Available_Sticky_Notes_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Available_Sticky_Notes_DGV.SelectionChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            SaveDialogResultAnseredIsNo = False
            Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked
            Dim ShowSticky As String = String.Empty
            If IsNothing(ActiveControl) Or
                sender.SelectedRows.Count = 0 Or
                OpenExternalMode Then
                Exit Sub
            End If
            If CurrentRowNotEqualRowIndex(sender) Then Exit Sub

            If ActiveControl.Name = Sticky_Password_TxtBx.Name Then
                ShowSticky = "ShowWithDecrypt"
                GoTo SecuredSticky
            End If
            If CommingFromSaveToolStripButton Then GoTo CFSTSB
            If ActiveControl.Name <> sender.name And
                ActiveControl.Name <> Previous_Btn.Name And
                ActiveControl.Name <> Next_Btn.Name And
                ActiveControl.Name <> Sticky_Note_No_CmbBx.Name Then
                Exit Sub
            End If
            If StickyAmendmented() = DialogResult.Cancel Then Exit Sub
CFSTSB:
            If ActiveControl.Name <> Sticky_Note_No_CmbBx.Name Then
                If IsNothing(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_Label").Value) Then
                    Sticky_Note_No_CmbBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_Name").Value
                Else
                    Sticky_Note_No_CmbBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_Label").Value
                End If
            End If
            Sticky_Note_Category_CmbBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_Category").Value
            Secured_Sticky_ChkBx.CheckState = Available_Sticky_Notes_DGV.CurrentRow.Cells("Secured_Sticky").Value
            Use_Main_Password_ChkBx.CheckState = Available_Sticky_Notes_DGV.CurrentRow.Cells("Use_Main_Password").Value
            Dim UseMainPassword As CheckState
            If String.IsNullOrEmpty(Available_Sticky_Notes_DGV.CurrentRow.Cells("Use_Main_Password").Value) Then
                UseMainPassword = CheckState.Unchecked
            Else
                UseMainPassword = CType(Available_Sticky_Notes_DGV.CurrentRow.Cells("Use_Main_Password").Value, CheckState)
            End If
            If CType(Available_Sticky_Notes_DGV.CurrentRow.Cells("Secured_Sticky").Value, CheckState) = CheckState.Checked And
                    Not PassedMainPasswordToPass Then
NotUseMainPassword:
                Sticky_Note_TxtBx.Text = Nothing
                If Sticky_Password_TxtBx.TextLength = 0 Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "This Sticky Is Secured... Kindly Enter Sticky Password To Read It"
                    Else
                        Msg = "هذه اللاصقة مؤمنة بكلمة سر... من فضلك أدخل كلمة سر اللاصقة لإمكانية قراءتها"
                    End If
                    Sticky_Password_TxtBx.ReadOnly = False
                    'Setting_TbCntrl.SelectTab(Setting_TbCntrl.TabPages("Sticky_Parameters_TbPg"))
                    If Not ArowsKeyDown Then
                        Sticky_Password_TxtBx.Focus()
                        Me.ActiveControl = Sticky_Password_TxtBx
                    End If
                    ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)
                    GoTo SecuredSticky
                ElseIf Decrypt_Function(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Password").Value) <> Sticky_Password_TxtBx.Text Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Wrong Password"
                    Else
                        Msg = "كلمة السر خطأ"
                    End If
                    ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, False)
                    GoTo SecuredSticky
                End If
                ShowSticky = "ShowWithDecrypt"
            ElseIf PassedMainPasswordToPass And
                CType(Available_Sticky_Notes_DGV.CurrentRow.Cells("Secured_Sticky").Value, CheckState) = CheckState.Checked And
                UseMainPassword = CheckState.Checked Then
                ShowSticky = "ShowWithDecrypt"
            ElseIf PassedMainPasswordToPass And
                CType(Available_Sticky_Notes_DGV.CurrentRow.Cells("Secured_Sticky").Value, CheckState) = CheckState.Checked And
                UseMainPassword = CheckState.Unchecked Then
                GoTo NotUseMainPassword
            Else
                ShowSticky = "Show"
            End If
SecuredSticky:
            Blocked_Sticky_ChkBx.CheckState = Available_Sticky_Notes_DGV.CurrentRow.Cells("Blocked_Sticky").Value
            Finished_Sticky_ChkBx.CheckState = Available_Sticky_Notes_DGV.CurrentRow.Cells("Finished_Sticky").Value
            If Not IsNothing(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Font").Value) Then
                Sticky_Font_TxtBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Font").Value
            Else
                Sticky_Font_TxtBx.Text = Sticky_Note_TxtBx.Font.Name & " - " & Sticky_Note_TxtBx.Font.Style & " - " & Sticky_Note_TxtBx.Font.Size
            End If
            If Not IsNothing(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Font_Color").Value) Then
                Sticky_Font_Color_ClrCmbBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Font_Color").Value
            Else
                Sticky_Font_Color_ClrCmbBx.Text = Sticky_Note_TxtBx.ForeColor.Name
            End If
            If Not IsNothing(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Back_Color").Value) Then
                Sticky_Back_Color_ClrCmbBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Back_Color").Value
            Else
                Sticky_Back_Color_ClrCmbBx.Text = Sticky_Note_TxtBx.BackColor.Name
            End If
            If ShowSticky = "ShowWithDecrypt" Then
                Try
                    Sticky_Note_TxtBx.Rtf = Decrypt_Function(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note").Value)
                Catch ex As Exception
                    Sticky_Note_TxtBx.Text = ConvertRichTextBox(Decrypt_Function(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_RTF").Value))
                End Try
                If Not ArowsKeyDown Then Sticky_Note_TxtBx.Focus()
            ElseIf ShowSticky = "Show" Then
                Try
                    Sticky_Note_TxtBx.Rtf = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_RTF").Value
                Catch ex As Exception
                    Sticky_Note_TxtBx.Text = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note").Value
                End Try
                If Not ArowsKeyDown Then Sticky_Note_TxtBx.Focus()
            End If

            Sticky_Word_Wrap_ChkBx.CheckState = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Word_Wrap").Value
            Sticky_Have_Reminder_ChkBx.CheckState = Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Have_Reminder").Value
            If Not String.IsNullOrEmpty(Available_Sticky_Notes_DGV.CurrentRow.Cells("Next_Reminder_Time").Value) Then
                Dim dt As DateTime = DateTime.ParseExact(
    Available_Sticky_Notes_DGV.CurrentRow.Cells("Next_Reminder_Time").Value, "yyyy-MM-dd HH-mm-ss",
    CultureInfo.InvariantCulture)
                Next_Reminder_Time_DtTmPkr.Value = dt
            End If

            If Not String.IsNullOrEmpty(Available_Sticky_Notes_DGV.CurrentRow.Cells("Reminder_Every").Value) Then
                Dim Reminder() = Split(Available_Sticky_Notes_DGV.CurrentRow.Cells("Reminder_Every").Value, ",")
                Reminder_Every_Days_NmrcUpDn.Value = Reminder(0)
                Reminder_Every_Hours_NmrcUpDn.Value = Reminder(1)
                Reminder_Every_Minutes_NmrcUpDn.Value = Reminder(2)
            End If

            Sticky_Note_TxtBx.SelectionStart = 0
            Application.DoEvents()
            StickyNoteRTF = Sticky_Note_TxtBx.Rtf
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Link As New LinkLabel()
    Dim FileCount As String = Nothing
    Public Sub Add_New_Sticky_Note_Click(sender As Object, e As EventArgs) Handles Add_New_Sticky_Note_Btn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Sticky_Note_Category_CmbBx.Text = Nothing Then
                CheckPublicCategory()
            End If
            If StickyAmendmented() = DialogResult.Cancel Then Exit Sub
            If Remember_Opened_External_Files_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Do You Want To Create New Sticky Note... If No... New Document Will Be Created?"
                Else
                    Msg = "هل تريد إنشاء ملاحظة لاصقة جديدة ... إذا لم يكن الأمر كذلك ... فسيتم إنشاء مستند جديد؟"
                End If
                Dim MyDialogResult = ShowMsg(Msg & vbNewLine, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                If MyDialogResult = DialogResult.No Then
                    Dim WorkingFolder = StickyNoteFolderPath & "\OutsideStickyNote"
                    Dim NewTextDocument As String = "New Text Document.txt"
                    If File.Exists(WorkingFolder & "\" & NewTextDocument) Then
                        NewTextDocument = "New Text Document (2).txt"
                        Dim FileNo As Integer = 2
                        While File.Exists(WorkingFolder & "\" & NewTextDocument)
                            NewTextDocument = Replace(NewTextDocument, "(" & FileNo & ")", "(" & FileNo + 1 & ")")
                            FileNo += 1
                        End While
                    End If
                    Sticky_Note_No_CmbBx.SelectedIndex = -1
                    CreateOutsideStickyNoteCategory()
                    CreateOutsideStickyNote()
                    Sticky_Note_No_CmbBx.Text = NewTextDocument
                    Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(StickyNoteFolderPath & "\OutsideStickyNote", Sticky_Note_No_CmbBx.Text))
                    If Sticky_Note_No_CmbBx.SelectedIndex = -1 Then
                        Sticky_Note_No_CmbBx.Text = Nothing
                        Sticky_Note_No_CmbBx.Text = NewTextDocument
                    End If
                    NewSticky = "OutsideStickyNote"
                    OpenExternalMode = True
                    Sticky_Note_TxtBx.Text = Nothing
                    Sticky_Note_TxtBx.Focus()
                    Exit Sub
                Else
                    OpenExternalMode = False
                End If
            End If
            If String.IsNullOrEmpty(FileCount) Then
                FileCount = New_Sticky_Note()
            End If
            Sticky_Note_No_CmbBx.Text = "Sticky_Note -(" & FileCount & ")-"
            NewSticky = "Sticky_Note -(" & FileCount & ")-"
            Sticky_Note_TxtBx.Text = Nothing
            Blocked_Sticky_ChkBx.CheckState = CheckState.Unchecked
            Finished_Sticky_ChkBx.CheckState = CheckState.Unchecked
            If Sticky_Back_Color_ClrCmbBx.SelectedIndex + 1 >= Sticky_Back_Color_ClrCmbBx.Items.Count Then
                Sticky_Back_Color_ClrCmbBx.SelectedIndex = 0
            Else
                Sticky_Back_Color_ClrCmbBx.SelectedIndex = Sticky_Back_Color_ClrCmbBx.SelectedIndex + 1
            End If
            Correct_Sticky_Note_TxtBx_Font_Color()
            Sticky_Note_TxtBx.Focus()
            Msg = "BackColor Name = " & Sticky_Back_Color_ClrCmbBx.Text
            Msg &= vbNewLine & "ForeColor Name = " & Sticky_Font_Color_ClrCmbBx.Text
            ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification,, 0)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            StickyNoteRTF = Sticky_Note_TxtBx.Rtf
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function Correct_Sticky_Note_TxtBx_Font_Color(Optional ByVal ReturnDarkOrLight As Boolean = False)
        Dim brightness As Single = Sticky_Back_Color_ClrCmbBx.BackColor.GetBrightness
        If ReturnDarkOrLight Then
            Return brightness
        End If
        If brightness < 0.4 Then
            Sticky_Font_Color_ClrCmbBx.Text = "Window"
        Else
            Sticky_Font_Color_ClrCmbBx.Text = "WindowText"
        End If
    End Function
    Private Function New_Sticky_Note()
        Try
            Me.Cursor = Cursors.WaitCursor
            SaveDialogResultAnseredIsNo = False
            Available_Sticky_Notes_DGV.ClearSelection()
            Sticky_Note_No_CmbBx.Text = Nothing
            Sticky_Note_No_CmbBx.SelectedIndex = -1
            Dim FileCount = 0
            Dim Files() As String = System.IO.Directory.GetFiles(StickyNoteFolderPath)
            For Each file In Files
                Dim F = Path.GetFileNameWithoutExtension(file)
                Dim filename = Microsoft.VisualBasic.Left(F, Len("Sticky_Note -(")) & Microsoft.VisualBasic.Right(Path.GetFileName(F), 2)
                If Path.GetExtension(file) = ".txt" And
                    filename = "Sticky_Note -()-" Then
                    FileCount += 1
                End If
            Next
            Dim CurrentFile = Application.StartupPath & "\Sticky_Notes_Files\Sticky_Note -(" & FileCount & ")-.txt"
            If File.Exists(CurrentFile) Then
                FileCount = 0
            End If
            While File.Exists(CurrentFile)
                FileCount += 1
                CurrentFile = Application.StartupPath & "\Sticky_Notes_Files\Sticky_Note -(" & FileCount & ")-.txt"
            End While
            Return FileCount
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Function LabelingForm(Optional ByVal Language As String = "Arabic")
        Try
            Me.Cursor = Cursors.WaitCursor
            If Language = "Arabic" Then
                Sticky_Note_Lbl.Text = "ملاحظة لاصقة"
                Blocked_Sticky_ChkBx.Text = "لاصقة محظورة"
                Finished_Sticky_ChkBx.Text = "لاصقة منتهية"
                Hide_Finished_Sticky_Note_ChkBx.Text = "إخفاء اللاصقات المنتهية"
                Me_Always_On_Top_ChkBx.Text = "أنا دائما فى المقدمة"
                Run_Me_At_Windows_Startup_ChkBx.Text = "شغلنى مع بداية تشغيل النوافز"
                Sticky_Form_Color_Lbl.Text = "لون اللاصقة"
                Sticky_Font_Lbl.Text = "خط اللاصقة"
                Sticky_Font_Color_Lbl.Text = "لون الخط"
                Available_Sticky_Notes_Lbl.Text = "اللاصقات المتاحة"
                Preview_Btn.Text = "عرض"
                Save_Sticky_Form_Parameter_Setting_Btn.Text = "حفظ المعلمات"
                'Next_Btn.Text = "ب"
                'Previous_Btn.Text = "ق"
                Setting_TbCntrl.TabPages("Sticky_Notes_TbPg").Text = "اللاصقات المتاحة"
                Setting_TbCntrl.TabPages("Sticky_Parameters_TbPg").Text = "معلمات اللاصقة"
                Setting_TbCntrl.TabPages("Form_Parameters_TbPg").Text = "معلمات الشاشة"
                Setting_TbCntrl.TabPages("Shortcuts_TbPg").Text = "إختصاراتى"
                Sticky_Form_Size_Lbl.Text = "حجم الشاشة"
                Sticky_Form_Location_Lbl.Text = "موقع الشاشة"
                Sticky_Form_Opacity_Lbl.Text = "شفافية الشاشة"
                Sticky_Back_Color_Lbl.Text = "لون خلفية اللاصقة"
                Form_Color_Like_Sticky_ChkBx.Text = "لون خلفية الشاشة مثل اللاصقة"
                Secured_Sticky_ChkBx.Text = "لاصقة مؤمنة بكلمة سر"
                Sticky_Password_Lbl.Text = "كلمة سر اللاصقة"
                Save_Setting_When_Exit_ChkBx.Text = "حفظ المعلمات عند الخروج من البرنامج"
                Periodically_Backup_Stickies_ChkBx.Text = "نسخة احتياطية من اللاصقات بصفة دورية"
                Reload_Stickies_After_Amendments_ChkBx.Text = "إعادة تحميل اللاصقات بعد التحديث"
                Sticky_Word_Wrap_ChkBx.Text = "إلتفاف كلمات اللاصقة"
                Sticky_Have_Reminder_ChkBx.Text = "لها وقت تنبيه"
                Next_Reminder_Time_Lbl.Text = "وقت التنبيه التالى"
                Reminder_Every_Lbl.Text = "فترة تكرار التنبيه"
                Pending_Reminder_Alert_ChkBx.Text = "توقف مؤقت للتنبية الحالى"
                Show_Form_Border_Style_ChkBx.Text = "إظهار نمط حدود الشاشة"
                Enable_Maximize_Box_ChkBx.Text = "أتاحة مربع التكبير للشاشة"
                Show_Sticky_Tab_Control_ChkBx.Text = "إظهار جدول التبويب"
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "مرئى بعد تشغيل إختصاراتى"
                Me_Is_Compressed_ChkBx.Text = "شاشتى مضغوطة"
                Me_As_Default_Text_File_Editor_ChkBx.Text = "انا محرر افتراضى للمفكرة ومستند نص منسق"
                Remember_Opened_External_Files_ChkBx.Text = "تذكر الملفات المفتوحة خارجيا"

                Sticky_Applicable_To_Rename_ChkBx.Text = "جاهزية اللاصقة لتعديل الإسم"
                Use_Main_Password_ChkBx.Text = "إستخدم كلمة السر الرئيسية"
                Enter_Password_To_Pass_ChkBx.Text = "إستخدم كلمة سر للمرور"
                Complex_Password_ChkBx.Text = "كلمة السر مغلظة"
                Main_Password_Lbl.Text = "كلمة السر الرئيسية"
                Next_Backup_Time_Lbl.Text = "وقت نسخ الاحتياطى التالى"
                Backup_Folder_Path_Lbl.Text = "مسار مجلد نسخ الاحتياطى"
                Backup_Every_Lbl.Text = "فترة نسخ الاحتياطة"
                Warning_Before_Save_ChkBx.Text = "التنبية قبل الحفظ"
                Warning_Before_Delete_ChkBx.Text = "التنبية قبل الحذف"
                Double_Click_To_Run_Shortcut_ChkBx.Text = "نقرتان لإستدعاء إختصارى"

                Installed_Voices_Lbl.Text = "الأصوات المتاحة"
                Speaking_Rate_Lbl.Text = "سرعة القراءة"
                Volume_Lbl.Text = "إرتفاع الصوت"

                Available_Sticky_Notes_DGV.Columns("Sticky_Note_Name").HeaderText = "إسم اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note_Label").HeaderText = "عنوان اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note_Category").HeaderText = "فئة اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note").HeaderText = "اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Blocked_Sticky").HeaderText = "لاصقة محظورة"
                Available_Sticky_Notes_DGV.Columns("Finished_Sticky").HeaderText = "لاصقة منتهية"
                Available_Sticky_Notes_DGV.Columns("Sticky_Font").HeaderText = "خط اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Sticky_Font_Color").HeaderText = "لون خط اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Sticky_Back_Color").HeaderText = "لون خلفية اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Creation_Date").HeaderText = "تاريخ الإنشاء"
                Available_Sticky_Notes_DGV.Columns("Secured_Sticky").HeaderText = "لاصقة مؤمنة بكلمة سر"
                Available_Sticky_Notes_DGV.Columns("Sticky_Password").HeaderText = "كلمة سر اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Use_Main_Password").HeaderText = "إستخدم كلمة السر الرئيسية"
                Available_Sticky_Notes_DGV.Columns("Sticky_Word_Wrap").HeaderText = "ألتفاف كلمات اللاصقة"
                Available_Sticky_Notes_DGV.Columns("Sticky_Have_Reminder").HeaderText = "لها وقت تنبيه"
                Available_Sticky_Notes_DGV.Columns("Next_Reminder_Time").HeaderText = "وقت التنبيه التالى"
                Available_Sticky_Notes_DGV.Columns("Reminder_Every").HeaderText = "فترة تكرار التنبيه"

                Form_ToolTip.SetToolTip(Previous_Btn, "إعرض اللاصقة السابقة")
                Form_ToolTip.SetToolTip(Sticky_Note_Lbl, "اسم اللاصفة الحالية")
                Form_ToolTip.SetToolTip(Sticky_Note_No_CmbBx, "إختر اللاصقة التى تريد التعامل معها من هنا")
                Form_ToolTip.SetToolTip(Next_Btn, "إعرض اللاصقة التالية من هنا")
                Form_ToolTip.SetToolTip(Add_New_Sticky_Note_Btn, "إنشئ لاصقة جديدة من هنا")
                Form_ToolTip.SetToolTip(Language_Btn, "هذا المفتاح خاص بتغيير لغة الشاشة وعناوينها")
                Form_ToolTip.SetToolTip(Sticky_Note_TxtBx, "أكتب ملاحظتك هنا مع ملاحضة انه يمكنك تغيير لون وخط كلمة معينة من خلال القائمة العامودية بالجانب الايسر من الشاشة")
                Form_ToolTip.SetToolTip(Form_Transparency_TrkBr, "يمكنك تغيير مستوى الشفافية للشاشة من هذا المفتاح")
                Form_ToolTip.SetToolTip(Setting_TbCntrl.TabPages("Sticky_Notes_TbPg"), "انقر هنا لمعرض جميع اللاصقات مع ملاحظة الاخذ فى الاعتبار ان هناك ضبط خاص بطريقة عرض البيانات فى خصوص اللاصقات المنتهية")
                Form_ToolTip.SetToolTip(Preview_Btn, "انقر هنا لمعرض جميع اللاصقات مع ملاحظة الاخذ فى الاعتبار ان هناك ضبط خاص بطريقة عرض البيانات فى خصوص اللاصقات المنتهية")
                Form_ToolTip.SetToolTip(Available_Sticky_Notes_DGV, "جدول عرض جميع اللاصقات المتاحة والصالحة للعرض")
                Form_ToolTip.SetToolTip(Blocked_Sticky_ChkBx, "ضع علام بالنقر بالفأرة لمحظر التعديل على اللاصقة الحالية وبدون علامة فاللاصقة صالحة للتعديل عليها")
                Form_ToolTip.SetToolTip(Finished_Sticky_ChkBx, "ضع علامة فتصبح اللاصقة الحالية منتهية فيمكن تجنب عرضها اثناء عرض اللاصقات المتاحة")
                Form_ToolTip.SetToolTip(Sticky_Font_TxtBx, "أسم الخط الحالى للاصقة الحالية")
                Form_ToolTip.SetToolTip(Sticky_Font_Lbl, "أسم الخط الحالى للاصقة الحالية")
                Form_ToolTip.SetToolTip(View_Text_Font_Properties_Btn, "إختر من هنا نوع الخط الذى ترغب ان تكون عليه اللاصقة الحالية")
                Form_ToolTip.SetToolTip(Sticky_Font_Color_Lbl, "اللون الحالى لخط اللاصقة الحالية")
                Form_ToolTip.SetToolTip(Sticky_Font_Color_ClrCmbBx, "إختر من هنا اللون الذى ترغب ان يكون عليه خط اللاصقة الحالية")
                Form_ToolTip.SetToolTip(Hide_Finished_Sticky_Note_ChkBx, "ضع علامة لعدم عرض اللاصقات المنتهية وبعدم وجود علامة سيتم عرض جميع اللاصقات")
                Form_ToolTip.SetToolTip(Me_Always_On_Top_ChkBx, "البرنامج دائما أعلى جميع النوافذ")
                Form_ToolTip.SetToolTip(Run_Me_At_Windows_Startup_ChkBx, "ضع علامة ان كنت ترغب فى تشغيل البرنامج دائما تلقائياعند تشغيل النوافذ")
                Form_ToolTip.SetToolTip(Sticky_Form_Color_ClrCmbBx, "إختر من هنا اللون الذى ترغب ان تكون عليه اللاصقة الحالية")
                Form_ToolTip.SetToolTip(Sticky_Form_Color_Lbl, "لون اللاصقة الحالية")
                Form_ToolTip.SetToolTip(Sticky_Form_Size_Lbl, "حجم الحالى لخط الاصقة")
                Form_ToolTip.SetToolTip(Sticky_Form_Size_TxtBx, "حجم الحالى لخط الاصقة")
                Form_ToolTip.SetToolTip(Sticky_Form_Location_Lbl, "الموقع الحالى لللاصقة")
                Form_ToolTip.SetToolTip(Sticky_Form_Location_TxtBx, "الموقع الحالى لللاصقة")
                Form_ToolTip.SetToolTip(Sticky_Form_Opacity_Lbl, "درجة الشفافية الحالية للاصقة")
                Form_ToolTip.SetToolTip(Sticky_Form_Opacity_TxtBx, "درجة الشفافية الحالية للاصقة")
                Form_ToolTip.SetToolTip(Save_Sticky_Form_Parameter_Setting_Btn, "حفظ المعلمات الحالية الخاصة بالبرنامج لامكنية استدعائها مرة اخرى عند إعادة تشغيل البرنامج")
                Form_ToolTip.SetToolTip(Sticky_Back_Color_Lbl, "لون خلفية اللاصقة")
                Form_ToolTip.SetToolTip(Sticky_Back_Color_ClrCmbBx, "إختر لون خلفية اللاصقة")
                Form_ToolTip.SetToolTip(Form_Color_Like_Sticky_ChkBx, "بوضع علامة سيصبح لون خلفية الشاشة دائما مثل اللاصقة")
                Form_ToolTip.SetToolTip(Secured_Sticky_ChkBx, "ضع علامة لتكون هذه الاصقة مؤمنة بكلمة سرلايمكن عرضها الا بعد ادخال كلمة السر الخاصة بها")
                Form_ToolTip.SetToolTip(Sticky_Password_Lbl, "كلمة السر الخاصة بهذه اللاصقة")
                Form_ToolTip.SetToolTip(Sticky_Password_TxtBx, "أدخل كلمة السر الخاصة بهذه اللاصقة")
                Form_ToolTip.SetToolTip(Save_Setting_When_Exit_ChkBx, "ضع علامة ليتم حفظ المعلمات الخاصة بالبرنامج عند الخروج من البرنامج")
                Form_ToolTip.SetToolTip(Periodically_Backup_Stickies_ChkBx, "ضع علامة ليتم نسخ اللاصقات كل فترة زمنية")
                Form_ToolTip.SetToolTip(Reload_Stickies_After_Amendments_ChkBx, "اعادة تحميل اللاصقات بعد كل تعديل")
                Form_ToolTip.SetToolTip(Sticky_Applicable_To_Rename_ChkBx, "ضع علامة حتى يتسنى لك التعديل على اسم اللاصقة")
                Form_ToolTip.SetToolTip(Use_Main_Password_ChkBx, "ضع علامة لاستخدام كلمة السر الرئيسية بدلا من كلمة سر اللاصقة")
                Form_ToolTip.SetToolTip(Enter_Password_To_Pass_ChkBx, "ضع علامة فلايتم الدخول الى البرنامج إلا من خلال وجود كملة سر")
                Form_ToolTip.SetToolTip(Complex_Password_ChkBx, "ضع غلامة فلا يقبل البرنامج كلمة السر الا اذا كان تحتوى على اخرف وارقام واخرف خاصة ولا تقل عن ثمانية احرف")
                Form_ToolTip.SetToolTip(Main_Password_Lbl, "كلمة السر الريسية التى تستخدم للدخول على البرنامج")
                Form_ToolTip.SetToolTip(Next_Backup_Time_Lbl, "وقت اخذ النسخة الاحتياطية للمرة القادمة")
                Form_ToolTip.SetToolTip(Backup_Folder_Path_Lbl, "المسار المخصص لحفظ ملفات النسخ الاحتياطية")
                Form_ToolTip.SetToolTip(Backup_Every_Lbl, "الوقت الذى بعد إنقضائه يتم اخذ النسخة الاحتياطية")
                Form_ToolTip.SetToolTip(Sticky_Note_Category_CmbBx, "إختر الفئة المناسبة للاصقة الحالية")
                Form_ToolTip.SetToolTip(Me_As_Default_Text_File_Editor_ChkBx, "لجعل برنامج الملاحظات اللاصقة هو المحرر الافتراضى لكل من ملف المفكرة وملف النص المنصق ")
                'Form_ToolTip.SetToolTip(Sticky_Note_Lbl, "")

                Form_ToolTip.SetToolTip(Installed_Voices_Lbl, "أصوات القراءة التى يمكن الاختيار منها للقراءة")
                Form_ToolTip.SetToolTip(Speaking_Rate_Lbl, "سرعة القراءة الحالية")
                Form_ToolTip.SetToolTip(Volume_Lbl, "درجة إرتفاع أو إنخفاض الصوت")

            ElseIf Language = "English" Then
                Sticky_Note_Lbl.Text = "Sticky Note"
                Blocked_Sticky_ChkBx.Text = "Blocked Sticky"
                Finished_Sticky_ChkBx.Text = "Finished Sticky"
                Me_Always_On_Top_ChkBx.Text = "Me Always On Top"
                Hide_Finished_Sticky_Note_ChkBx.Text = "Hide Finished Stickies"
                Run_Me_At_Windows_Startup_ChkBx.Text = "Run Me At Windows Startup"
                Sticky_Form_Color_Lbl.Text = "Sticky Color"
                Sticky_Font_Lbl.Text = "Sticky Font"
                Sticky_Font_Color_Lbl.Text = "Font Color"
                Available_Sticky_Notes_Lbl.Text = "Available Stickies"
                Preview_Btn.Text = "Preview"
                Save_Sticky_Form_Parameter_Setting_Btn.Text = "Save Setting"
                'Next_Btn.Text = "N"
                'Previous_Btn.Text = "P"
                Setting_TbCntrl.TabPages("Sticky_Notes_TbPg").Text = "Available Stickies"
                Setting_TbCntrl.TabPages("Sticky_Parameters_TbPg").Text = "Sticky Parameters"
                Setting_TbCntrl.TabPages("Form_Parameters_TbPg").Text = "Form Parameters"
                Setting_TbCntrl.TabPages("Shortcuts_TbPg").Text = "My Shortcuts"
                Sticky_Form_Size_Lbl.Text = "Form Size"
                Sticky_Form_Location_Lbl.Text = "Form Location"
                Sticky_Form_Opacity_Lbl.Text = "Form Opacity"
                Secured_Sticky_ChkBx.Text = "Sticky Secured By Password"
                Sticky_Password_Lbl.Text = "Sticky Password"
                Save_Setting_When_Exit_ChkBx.Text = "Save Setting When Exit"
                Form_Color_Like_Sticky_ChkBx.Text = "Form Color Like Sticky"
                Sticky_Back_Color_Lbl.Text = "Sticky Back Color"
                Form_Color_Like_Sticky_ChkBx.Text = "Form Color Like Sticky"
                Secured_Sticky_ChkBx.Text = "Secured Sticky"
                Sticky_Password_Lbl.Text = "Sticky Password"
                Save_Setting_When_Exit_ChkBx.Text = "Save Setting When Exit"
                Periodically_Backup_Stickies_ChkBx.Text = "Periodically Backup Stickies"
                Reload_Stickies_After_Amendments_ChkBx.Text = "Reload Stickies After Amendments"
                Warning_Before_Save_ChkBx.Text = "Warning Before Save"
                Warning_Before_Delete_ChkBx.Text = "Warning Before Delete"
                Double_Click_To_Run_Shortcut_ChkBx.Text = "Double Click To Run Shortcut"

                Sticky_Applicable_To_Rename_ChkBx.Text = "Sticky Applicable To Rename"
                Use_Main_Password_ChkBx.Text = "Use Main Password"
                Enter_Password_To_Pass_ChkBx.Text = "Enter Password To Pass"
                Complex_Password_ChkBx.Text = "Complex Password"
                Main_Password_Lbl.Text = "Main Password"
                Next_Backup_Time_Lbl.Text = "Next Backup Time"
                Backup_Folder_Path_Lbl.Text = "Backup Folder Path"
                Backup_Every_Lbl.Text = "Backup Every"
                Sticky_Word_Wrap_ChkBx.Text = "Sticky Word Wrap"
                Sticky_Have_Reminder_ChkBx.Text = "Sticky Have Reminder"
                Next_Reminder_Time_Lbl.Text = "Next Reminder Time"
                Reminder_Every_Lbl.Text = "Reminder Every"
                Pending_Reminder_Alert_ChkBx.Text = "Pending Reminder Alert"
                Show_Form_Border_Style_ChkBx.Text = "Show Form Border Style"
                Enable_Maximize_Box_ChkBx.Text = "Enable Maximize Box"
                Show_Sticky_Tab_Control_ChkBx.Text = "Show Sticky Tab Control"
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "Visible After Running My Shortcut"
                Me_Is_Compressed_ChkBx.Text = "Me Is Compressed"
                Me_As_Default_Text_File_Editor_ChkBx.Text = "Me As Default Notepad Editor And RTF"
                Remember_Opened_External_Files_ChkBx.Text = "Remember Opened External Files"

                Installed_Voices_Lbl.Text = "Installed Voices"
                Speaking_Rate_Lbl.Text = "Speaking Rate"
                Volume_Lbl.Text = "Volume"

                Available_Sticky_Notes_DGV.Columns("Sticky_Note_Name").HeaderText = "Sticky Note Name"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note_Label").HeaderText = "Sticky Note Label"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note_Category").HeaderText = "Sticky Note Category"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note").HeaderText = "Sticky Note"
                Available_Sticky_Notes_DGV.Columns("Blocked_Sticky").HeaderText = "Blocked Sticky"
                Available_Sticky_Notes_DGV.Columns("Finished_Sticky").HeaderText = "Finished Sticky"
                Available_Sticky_Notes_DGV.Columns("Sticky_Font").HeaderText = "Sticky Font"
                Available_Sticky_Notes_DGV.Columns("Sticky_Font_Color").HeaderText = "Sticky Font Color"
                Available_Sticky_Notes_DGV.Columns("Sticky_Back_Color").HeaderText = "Sticky Back Color"
                Available_Sticky_Notes_DGV.Columns("Creation_Date").HeaderText = "Creation Date"
                Available_Sticky_Notes_DGV.Columns("Secured_Sticky").HeaderText = "Sticky Secured By Password"
                Available_Sticky_Notes_DGV.Columns("Sticky_Password").HeaderText = "Sticky Password"
                Available_Sticky_Notes_DGV.Columns("Use_Main_Password").HeaderText = "Use Main Password"
                Available_Sticky_Notes_DGV.Columns("Sticky_Note_RTF").HeaderText = "Sticky Note RTF"
                Available_Sticky_Notes_DGV.Columns("Sticky_Word_Wrap").HeaderText = "Sticky Word Wrap"
                Available_Sticky_Notes_DGV.Columns("Sticky_Have_Reminder").HeaderText = "Sticky Have Reminder"
                Available_Sticky_Notes_DGV.Columns("Next_Reminder_Time").HeaderText = "Next Reminder Time"
                Available_Sticky_Notes_DGV.Columns("Reminder_Every").HeaderText = "Reminder Every"
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub Finished_Sticky_Note_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Finished_Sticky_ChkBx.CheckedChanged

    End Sub

    Private Sub Finished_Sticky_Note_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Finished_Sticky_ChkBx.CheckStateChanged
        Try
            Select Case Finished_Sticky_ChkBx.CheckState
                Case CheckState.Unchecked
                    If Language_Btn.Text = "ع" Then
                        Finished_Sticky_ChkBx.Text = "Active Sticky"
                    Else
                        Finished_Sticky_ChkBx.Text = "لاصقة نشطة"
                    End If
                Case CheckState.Checked
                    If Language_Btn.Text = "ع" Then
                        Finished_Sticky_ChkBx.Text = "Fineshed Sticky"
                    Else
                        Finished_Sticky_ChkBx.Text = "لاصقة منتهية"
                    End If
                Case CheckState.Indeterminate
                    If Language_Btn.Text = "ع" Then
                        Finished_Sticky_ChkBx.Text = "Pending Sticky"
                    Else
                        Finished_Sticky_ChkBx.Text = "لاصقة معلقة"
                    End If
            End Select
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Sticky_Note_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sticky_Form_Color_ClrCmbBx.SelectedIndexChanged

    End Sub

    Private Sub Sticky_Note_Color_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sticky_Form_Color_ClrCmbBx.SelectedValueChanged
        Try
            If Sticky_Form_Color_ClrCmbBx.SelectedIndex = -1 Or
                Form_Color_Like_Sticky_ChkBx.CheckState = CheckState.Checked Then Exit Sub
            ChangeFormControlsColors(Sticky_Form_Color_ClrCmbBx.Text)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub ChangeFormControlsColors(ByVal ColorName As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.BackColor = Color.FromName(ColorName)
            Me.ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
            For Each Cntrl As Control In Me.Controls
                If Cntrl.Name = "Read_Me_Pnl" Then Continue For
                'MsgBox_TlStrpSttsLbl
                AddHandler_Control_Move(Cntrl)
                Try 'Btn_Paint
                    If Cntrl.Controls.Count > 0 Then
                        For Each sCntrl As Control In Cntrl.Controls
                            If sCntrl.Parent.Name = "Read_Me_Pnl" Then Continue For
                            If sCntrl.Name = MsgBox_SttsStrp.Name Then
                                MsgBox_SttsStrp.Items(0).ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
                            End If
                            If sCntrl.Name = Form_Transparency_TrkBr.Name Then
                                Continue For
                            End If
                            AddHandler_Control_Move(sCntrl)
                            If sCntrl.Name = Sticky_Note_TxtBx.Name Then
                                Continue For
                            End If
                            sCntrl.BackColor = Color.FromName(ColorName)
                            sCntrl.ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
                            If sCntrl.Controls.Count > 0 Then
                                For Each ssCntrl As Control In sCntrl.Controls
                                    AddHandler_Control_Move(ssCntrl)
                                    ssCntrl.BackColor = Color.FromName(ColorName)
                                    ssCntrl.ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
                                    ssCntrl.Refresh()
                                    If ssCntrl.Controls.Count > 0 Then
                                        For Each sssCntrl As Control In ssCntrl.Controls
                                            AddHandler_Control_Move(sssCntrl)
                                            sssCntrl.BackColor = Color.FromName(ColorName)
                                            sssCntrl.ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                    Cntrl.BackColor = Color.FromName(ColorName)
                    Cntrl.ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
                Catch ex As Exception
                End Try
            Next
            Available_Sticky_Notes_DGV.BackgroundColor = Color.FromName(ColorName)
            Available_Sticky_Notes_DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(ColorName)
            Available_Sticky_Notes_DGV.RowHeadersDefaultCellStyle.BackColor = Color.FromName(ColorName)
            Available_Sticky_Notes_DGV.RowsDefaultCellStyle.BackColor = Color.FromName(ColorName)
            Application.DoEvents()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Run_Me_At_Windows_Startup_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Run_Me_At_Windows_Startup_ChkBx.CheckedChanged

    End Sub

    Private Sub Run_Me_At_Windows_Startup_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Run_Me_At_Windows_Startup_ChkBx.CheckStateChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ProductName = Application.ProductName
            Dim Reg As Microsoft.Win32.RegistryKey
            Dim GetReg As String = Nothing
            If Run_Me_At_Windows_Startup_ChkBx.CheckState = CheckState.Checked Then
                Reg = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
                GetReg = Reg.GetValue(ProductName)
                If Len(GetReg) > 0 Then Reg.DeleteValue(ProductName.ToString, True)
                Reg.SetValue(ProductName, Application.StartupPath & "\" & ProductName)
            ElseIf Run_Me_At_Windows_Startup_ChkBx.CheckState = CheckState.Unchecked Then
                Reg = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
                GetReg = Reg.GetValue(ProductName)
                If Len(GetReg) > 0 Then Reg.DeleteValue(ProductName, True)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Sticky_Note_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If OpenExternalMode And Not NormalSticky Then
                If StickyAmendmented() = DialogResult.Cancel Then GoTo CanceExitExternalFile
                Exit Sub
            End If
            If Not AlreadyAsked And
                Minimize_After_Running_My_Shortcut_ChkBx.CheckState <> CheckState.Indeterminate Then
                AlreadyAsked = True
                If Language_Btn.Text = "ع" Then
                    Msg = "Sticky Note Will Be Always In Windows System Tray And Ready To Call By Clicking (Alt+s) Until You Exit The Sticky From The Windows System Tray!!!"
                Else
                    Msg = "تذكر دائما أن برنامج الملاحظات اللاصقة سيكون مخفيا فى علبة الايقونات المخفية للنوافذ ويمكن استدعائة بالنقر على ـ(Alt+s)ـ... حتى يتم الخروج من البرنامج"
                End If
                ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
            End If
            If Secured_Sticky_ChkBx.CheckState = CheckState.Checked Then
                'Add_New_Sticky_Note_Click(Add_New_Sticky_Note_Btn, EventArgs.Empty)
                Sticky_Note_No_CmbBx.SelectedIndex = -1
                Sticky_Note_TxtBx.Text = String.Empty
            End If
            If ApplicationRestart Then
                Exit Sub
            ElseIf Not OpenExternalMode Or NormalSticky Then
                If Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Indeterminate Then
                    Compress_Me_TlStrpBtn.PerformClick()
                Else
                    Me.WindowState = WindowState.Minimized
                End If
CanceExitExternalFile:
                e.Cancel = True
                Tray.Visible = True
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Form_Transparency_TrkBr_Scroll(sender As Object, e As EventArgs) Handles Form_Transparency_TrkBr.Scroll
    End Sub

    Private Sub Previous_Btn_Click(sender As Object, e As EventArgs) Handles Previous_Btn.Click
        Available_Sticky_Notes_DGV.ClearSelection()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim iRowIndex = Available_Sticky_Notes_DGV.CurrentRow.Index - 1
            If iRowIndex = -1 Then
                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(Available_Sticky_Notes_DGV.Rows.Count - 1).Cells(0)
            Else
                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(iRowIndex).Cells(0)
            End If
            Available_Sticky_Notes_DGV.ClearSelection()
            Available_Sticky_Notes_DGV.Rows(iRowIndex).Selected = True
        Catch ex As Exception
            If Available_Sticky_Notes_DGV.Rows.Count > 0 Then
                Available_Sticky_Notes_DGV.Rows(Available_Sticky_Notes_DGV.Rows.Count - 1).Selected = True
                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(Available_Sticky_Notes_DGV.Rows.Count - 1).Cells(0)
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Next_Btn_Click(sender As Object, e As EventArgs) Handles Next_Btn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim iRowIndex = Available_Sticky_Notes_DGV.CurrentRow.Index + 1
            If iRowIndex = Available_Sticky_Notes_DGV.Rows.Count Then
                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(0).Cells(0)
            Else
                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(iRowIndex).Cells(0)
            End If
            Available_Sticky_Notes_DGV.ClearSelection()
            Available_Sticky_Notes_DGV.Rows(iRowIndex).Selected = True
        Catch ex As Exception
            If Available_Sticky_Notes_DGV.Rows.Count > 0 Then
                Available_Sticky_Notes_DGV.Rows(0).Selected = True
                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(0).Cells(0)
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub View_Text_Font_Properties_Btn_Click_1(sender As Object, e As EventArgs) Handles View_Text_Font_Properties_Btn.Click
        Try
            Dim FontDialog As New FontDialog
            FontDialog.Font = Sticky_Font_TxtBx.Font
            If FontDialog.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Sticky_Font_TxtBx.Text = FontDialog.Font.Name & " - " & FontDialog.Font.Size & " - "
                Select Case FontDialog.Font.Style
                    Case 0
                        Sticky_Font_TxtBx.Text &= "Regular"
                    Case 1
                        Sticky_Font_TxtBx.Text &= "Bold"
                    Case 2
                        Sticky_Font_TxtBx.Text &= "Italic"
                    Case 8
                        Sticky_Font_TxtBx.Text &= "Strikeout"
                    Case 4
                        Sticky_Font_TxtBx.Text &= "Underline"
                End Select
            Else
                Sticky_Font_TxtBx.Text = Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Public Sub Save_Sticky_Form_Parameter_Setting_Btn_Click(sender As Object, e As EventArgs) Handles Save_Sticky_Form_Parameter_Setting_Btn.Click
        If Not Save_Sticky_Form_Parameter_Setting_Btn.Enabled Then Exit Sub
        Dim FileName = StickyNoteFolderPath & "\Sticky_Note_Setting.txt"
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Show_ShowMsg As Boolean = True
            If ActiveControl.Name <> sender.Name And
                ActiveControl.Name <> listView1.Name Then
                Show_ShowMsg = False
            End If
            If File.Exists(FileName) And ActiveControl.Name = sender.name Then
                If Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Sticky Note Form Setting File Will Be Saved... Are You Sure?"
                    Else
                        Msg = "سيتم حفظ معلمات ضبط البرنامج... هل انت ماأكد"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & vbNewLine, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False, Show_ShowMsg)
                    If MyDialogResult = DialogResult.No Then Exit Sub
                End If
            End If
            If Periodically_Backup_Stickies_ChkBx.CheckState = CheckState.Checked And (Days_NmrcUpDn.Value = 0 And Hours_NmrcUpDn.Value = 0 And Minutes_NmrcUpDn.Value = 0) Then
                If Language_Btn.Text = "ع" Then
                    Msg = "You Set Backup Periodically But No Period Seted... Period Will Be One Day.. Are You Agree?"
                Else
                    Msg = "لقد فعلت النسخ الاحتياطى ولم تحدد الفترة... سيتم تحديد الفترة يوم واحد... هل انت موافق؟"
                End If
                Dim MyDialogResult = ShowMsg(Msg & vbNewLine, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                If MyDialogResult = DialogResult.No Then Exit Sub
                Days_NmrcUpDn.Value = 1
            End If
            Dim TextToWrite As String
            TextToWrite = ":Current_Language -(" & Language_Btn.Text & ")-"
            If Sticky_Note_No_CmbBx.SelectedIndex <> 1 And String.IsNullOrEmpty(NewSticky) Then
                TextToWrite &= ":Current_Sticky_Note_Name -(" & Sticky_Note_No_CmbBx.Text & ")-"
            End If
            TextToWrite &= ":Run_Me_At_Windows_Startup -(" & Run_Me_At_Windows_Startup_ChkBx.CheckState & ")-"
            TextToWrite &= ":Me_Always_On_Top -(" & Me_Always_On_Top_ChkBx.CheckState & ")-"
            TextToWrite &= ":Hide_Finished_Sticky_Note -(" & Hide_Finished_Sticky_Note_ChkBx.CheckState & ")-"
            TextToWrite &= ":Form_Color_Like_Sticky -(" & Form_Color_Like_Sticky_ChkBx.CheckState & ")-"
            TextToWrite &= ":Save_Setting_When_Exit -(" & Save_Setting_When_Exit_ChkBx.CheckState & ")-"
            TextToWrite &= ":Sticky_Form_Color -(" & Sticky_Form_Color_ClrCmbBx.Text & ")-"
            'TextToWrite &= ":Sticky_Form_Size -(" & Me.Size.ToString & ")-"
            TextToWrite &= ":Sticky_Form_Size -(" & Sticky_Form_Size_TxtBx.Text & ")-"
            'TextToWrite &= ":Sticky_Form_Location -(" & Me.Location.ToString & ")-"
            TextToWrite &= ":Sticky_Form_Location -(" & Sticky_Form_Location_TxtBx.Text & ")-"
            TextToWrite &= ":Sticky_Form_Opacity -(" & Me.Opacity.ToString & ")-"
            TextToWrite &= ":Periodically_Backup_Stickies -(" & Periodically_Backup_Stickies_ChkBx.CheckState & ")-"
            TextToWrite &= ":Backup_Time -(" & Days_NmrcUpDn.Value & "," & Hours_NmrcUpDn.Value & "," & Minutes_NmrcUpDn.Value & ")-"
            TextToWrite &= ":Next_Backup_Time -(" & Replace(Next_Backup_Time_TxtBx.Text, ":", ",") & ")-"
            TextToWrite &= ":Reload_Stickies_After_Amendments -(" & Reload_Stickies_After_Amendments_ChkBx.CheckState & ")-"
            TextToWrite &= ":Enter_Password_To_Pass -(" & Enter_Password_To_Pass_ChkBx.CheckState & ")-"
            TextToWrite &= ":Complex_Password -(" & Complex_Password_ChkBx.CheckState & ")-"
            TextToWrite &= ":Main_Password -(" & Encrypt_Function(Main_Password_TxtBx.Text) & ")-"

            TextToWrite &= ":Set_Control_To_Fill -(" & Set_Control_To_Fill_ChkBx.CheckState & ")-"
            TextToWrite &= ":Warning_Before_Save -(" & Warning_Before_Save_ChkBx.CheckState & ")-"
            TextToWrite &= ":Warning_Before_Delete -(" & Warning_Before_Delete_ChkBx.CheckState & ")-"
            TextToWrite &= ":Double_Click_To_Run_Shortcut -(" & Double_Click_To_Run_Shortcut_ChkBx.CheckState & ")-"

            TextToWrite &= ":Show_Form_Border_Style -(" & Show_Form_Border_Style_ChkBx.CheckState & ")-"
            TextToWrite &= ":Enable_Maximize_Box -(" & Enable_Maximize_Box_ChkBx.CheckState & ")-"
            TextToWrite &= ":Remember_Opened_External_Files -(" & Remember_Opened_External_Files_ChkBx.CheckState & ")-"
            TextToWrite &= ":Show_Sticky_Tab_Control -(" & Show_Sticky_Tab_Control_ChkBx.CheckState & ")-"
            TextToWrite &= ":Minimize_After_Running_My_Shortcut -(" & Minimize_After_Running_My_Shortcut_ChkBx.CheckState & ")-"
            TextToWrite &= ":Me_As_Default_Text_File_Editor -(" & Me_As_Default_Text_File_Editor_ChkBx.CheckState & ")-"

            My.Computer.FileSystem.WriteAllText(FileName, TextToWrite, 0, System.Text.Encoding.UTF8)
            If Language_Btn.Text = "ع" Then
                Msg = "File Updated Successfully"
            Else
                Msg = "تم حفظ الملف بنجاخ"
            End If
            SaveList()
            If ActiveControl.Name = sender.name Then
                ShowMsg(Msg & vbNewLine & FileName, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False, Show_ShowMsg)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Font_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sticky_Font_Color_ClrCmbBx.SelectedIndexChanged

    End Sub

    Private Sub ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sticky_Font_Color_ClrCmbBx.SelectedValueChanged,
            Sticky_Back_Color_ClrCmbBx.SelectedValueChanged,
            Sticky_Form_Color_ClrCmbBx.SelectedValueChanged
        'sender.ForeColor = sender.SelectedItem
    End Sub

    Private Sub Font_Color_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sticky_Font_Color_ClrCmbBx.SelectedValueChanged
        Try
            If Sticky_Back_Color_ClrCmbBx.SelectedIndex = -1 Then Exit Sub
            Sticky_Note_TxtBx.ForeColor = Color.FromName(Sticky_Font_Color_ClrCmbBx.Text)
            If Form_Color_Like_Sticky_ChkBx.CheckState = CheckState.Checked Then
                ChangeFormControlsColors(Sticky_Back_Color_ClrCmbBx.Text)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Note_Font_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Sticky_Font_TxtBx.TextChanged
        Try
            If Sticky_Font_TxtBx.TextLength = 0 Then Exit Sub
            Sticky_Note_TxtBx.Font = GetFontByString(Sticky_Font_TxtBx.Text)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Function GetFontByString(ByVal sFont As String) As Font
        Dim sElement() As String = Split(sFont, " - ")
        Dim sSingle() As String
        Dim sValue As String
        Dim FontName As String
        Dim FontSize As Single
        Dim FontStyle As FontStyle = Drawing.FontStyle.Regular
        Dim FontUnit As GraphicsUnit = GraphicsUnit.Point
        Dim gdiCharSet As Byte
        Dim gdiVerticalFont As Boolean
        sValue = Trim(sElement(0))
        sSingle = Split(sValue, "=")
        FontName = sSingle(0)
        sValue = Trim(sElement(1))
        sSingle = Split(sValue, "=")
        FontSize = sSingle(0)
        Select Case sElement(2)
            Case "Regular"
                Return New Font(FontName, FontSize, Drawing.FontStyle.Regular, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Bold"
                Return New Font(FontName, FontSize, Drawing.FontStyle.Bold, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Italic"
                Return New Font(FontName, FontSize, Drawing.FontStyle.Italic, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Strikeout"
                Return New Font(FontName, FontSize, Drawing.FontStyle.Strikeout, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Underline"
                Return New Font(FontName, FontSize, Drawing.FontStyle.Underline, FontUnit, gdiCharSet, gdiVerticalFont)
        End Select
    End Function

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        Add_New_Sticky_Note_Btn.PerformClick()
    End Sub
    Private Function TheStickyIsAplicableToSave()
        Try
            Dim FileExist, UseMainPassword As Boolean
            If Sticky_Note_TxtBx.TextLength = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Type Something To Save!!!"
                Else
                    Msg = "من فضلك اكتب شئ ليتم حفظة!!!"
                End If
                ShowMsg(Msg & vbNewLine & Sticky_Note_TxtBx.Text, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MBOs, False)
                Return False
            ElseIf Sticky_Note_No_CmbBx.Text.Length = 0 Then
                If String.IsNullOrEmpty(NewSticky) Then RestoreCheckBoxesValue()
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Generate New Sticky Not First!!!"
                Else
                    Msg = "من فضلك استحدث تسلسل جديد أولا!!!"
                End If
                ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MBOs, False)
                Return False
            ElseIf Sticky_Note_No_CmbBx.SelectedIndex <> -1 And
                Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked Then
                If File.Exists(StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt") Then
                    FileExist = True
                ElseIf File.Exists(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) Then
                    Return True
                ElseIf Not OpenExternalMode Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Not Available To Save The File By This Situation!!!"
                    Else
                        Msg = "غير متاح حفظ الملف بهذا الوضع !!!"
                    End If
                    ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MBOs, False)
                    Return False
                End If
            ElseIf Sticky_Note_No_CmbBx.Text.Length > 0 And
                 String.IsNullOrEmpty(OldStickyName) Then
                If File.Exists(StickyNoteFolderPath & "\" & Sticky_Note_No_CmbBx.Text) Then
                    FileExist = True
                End If
            End If
            If OpenExternalMode Then Return True
            If FileExist Or
                Not String.IsNullOrEmpty(OldStickyName) Then
                If String.IsNullOrEmpty(Available_Sticky_Notes_DGV.CurrentRow.Cells("Use_Main_Password").Value) Then
                    UseMainPassword = False
                Else
                    UseMainPassword = Available_Sticky_Notes_DGV.CurrentRow.Cells("Use_Main_Password").Value
                End If
            End If
            If Not IsNothing(Available_Sticky_Notes_DGV.CurrentRow) Then
                If Secured_Sticky_ChkBx.CheckState = CheckState.Unchecked And
                    Available_Sticky_Notes_DGV.CurrentRow.Cells("Secured_Sticky").Value And
                    Sticky_Password_TxtBx.TextLength = 0 And
                    String.IsNullOrEmpty(NewSticky) Then
                    GoTo EnterPassWord
                End If
            End If
            If (Secured_Sticky_ChkBx.CheckState = CheckState.Checked And
                    Sticky_Password_TxtBx.TextLength = 0) And Not PassedMainPasswordToPass Then
EnterPassWord:
                If String.IsNullOrEmpty(NewSticky) Then
                    RestoreCheckBoxesValue()
                End If
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Enter Sticky Password"
                Else
                    Msg = "من فضلك أدخل كلمة سر اللاصقة"
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, False)
                Me.ActiveControl = Sticky_Password_TxtBx
                Sticky_Password_TxtBx.Focus()
                Sticky_Password_TxtBx.ReadOnly = False
                Return False
StickNoteInsilization:
            ElseIf Sticky_Password_TxtBx.TextLength = 0 And
                Secured_Sticky_ChkBx.CheckState = CheckState.Checked And
                   Not UseMainPassword Then
                GoTo EnterPassWord
            ElseIf Sticky_Note_TxtBx.TextLength = 0 And
                     String.IsNullOrEmpty(OldStickyName) Then
                If Not File.Exists(StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt") Then

                    If String.IsNullOrEmpty(NewSticky) Then RestoreCheckBoxesValue()
                    If Language_Btn.Text = "ع" Then
                        Msg = "Kindly Type Something To Save!!!"
                    Else
                        Msg = "من فضلك اكتب شئ ليتم حفظة!!!"
                    End If
                    ShowMsg(Msg & vbNewLine & Sticky_Note_TxtBx.Text, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MBOs, False)
                    Return False
                ElseIf FileExist And
                    PassedMainPasswordToPass And
                    Not UseMainPassword Then
                    GoTo EnterPassWord
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub RestoreCheckBoxesValue()
        Me.ActiveControl = Sticky_Note_TxtBx
        If Available_Sticky_Notes_DGV.SelectedRows.Count = 0 Or
                Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Note_Name").Value <> DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key Then
        End If
        Dim FileName = StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt"
        Dim array() As String = ReadFile(FileName, 1)
        Blocked_Sticky_ChkBx.CheckState = CType(Val(array(4).ToString), CheckState)
        Finished_Sticky_ChkBx.CheckState = CType(Val(array(5).ToString), CheckState)
        Secured_Sticky_ChkBx.CheckState = CType(Val(array(10).ToString), CheckState)
        If IsNothing(array(12)) Then
            Use_Main_Password_ChkBx.CheckState = CheckState.Unchecked
        Else
            Use_Main_Password_ChkBx.CheckState = CType(Val(array(12).ToString), CheckState)
        End If

    End Sub
    Dim SaveDialogResultAnseredIsNo As Boolean = False
    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim DisplayMsg As Boolean = True
            If Debugger.IsAttached Then
                Me.WindowState = FormWindowState.Minimized
            End If
            If Sticky_Note_Category_CmbBx.SelectedIndex = -1 Then
                CheckPublicCategory()
            End If
            If New StackFrame(1).GetMethod().Name.Contains("Backup_Timer_Tick") Then
                DisplayMsg = False
            End If
            If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
                If File.Exists(StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt") And Available_Sticky_Notes_DGV.SelectedRows.Count = 0 Then
                    For Each Sticky In Available_Sticky_Notes_DGV.Rows
                        If Sticky.cells("Sticky_Note_Name").value = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key Then
                            Available_Sticky_Notes_DGV.Rows(Sticky.index).Selected = True
                            Exit For
                        End If
                    Next
                End If
            End If
            If ActiveControl.Name = ToolStrip1.Name Then
                If Not TheStickyIsAplicableToSave() Then Exit Sub
            End If
            Try
                If OpenExternalMode Then GoTo ExternalFile
                If File.Exists(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) Then
ExternalFile:
                    If Language_Btn.Text = "ع" Then
                        Msg = "This File Is An External File And Not Sticky Note... Do You Really Want To Save It?"
                    Else
                        Msg = "هذا الملف ملف خارجي وليس ملاحظة لاصقة ... هل حقًا تريد حفظه؟"
                    End If
                    If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then Exit Sub
                    If Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Checked Then
                        If Path.GetExtension(Sticky_Note_No_CmbBx.Text) = ".rtf" Then
                            Sticky_Note_TxtBx.SaveFile(Path.GetDirectoryName(OldStickyName) & "\" & Sticky_Note_No_CmbBx.Text, RichTextBoxStreamType.RichText)
                        ElseIf Path.GetExtension(Sticky_Note_No_CmbBx.Text) = ".txt" Then
                            My.Computer.FileSystem.WriteAllText(Path.GetDirectoryName(OldStickyName) & "\" & Sticky_Note_No_CmbBx.Text, Sticky_Note_TxtBx.Text, 0, System.Text.Encoding.UTF8)
                        Else
                            Dim SourceFileName
                            SourceFileName = Path.GetDirectoryName(OldStickyName) & "\" & Sticky_Note_No_CmbBx.Text
                            My.Computer.FileSystem.WriteAllText(SourceFileName, Sticky_Note_TxtBx.Text, 0, System.Text.Encoding.UTF8)
                            If OpenExternalMode Then RememberOpenedExternalFiles()
                            Exit Sub
                        End If
                    Else
                        CreateOutsideStickyNote()
                        If Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".rtf" Then
                            Sticky_Note_TxtBx.SaveFile(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value, RichTextBoxStreamType.RichText)
                        ElseIf Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".txt" Then
                            Dim FileNameToSave = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                            My.Computer.FileSystem.WriteAllText(FileNameToSave, Sticky_Note_TxtBx.Text, 0, System.Text.Encoding.UTF8)
                        Else
                            Dim FolderName = StickyNoteFolderPath & "\Temp"
                                If (Not System.IO.Directory.Exists(FolderName)) Then
                                    System.IO.Directory.CreateDirectory(FolderName)
                                End If
                                Dim TempFileName, SourceFileName
                                SourceFileName = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                                TempFileName = FolderName & "\" & Path.GetFileNameWithoutExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) & "(Temp(0))" & Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value)
                                Dim FileNo As Integer = 0
                                While File.Exists(TempFileName)
                                    TempFileName = Replace(TempFileName, "(Temp(" & FileNo & "))", "(Temp(" & FileNo + 1 & "))")
                                    FileNo += 1
                                End While
                            If Language_Btn.Text = "ع" Then
                                Msg = TempFileName & vbNewLine & "As Temp File Will Be Saved Before Sving The Source File... Do You Want To Save A Copy From The Source File?" & vbNewLine & SourceFileName
                            Else
                                Msg = TempFileName & vbNewLine & "كملف مؤقت سيتم حفظة قبل حفظ الملف المصدر" & vbNewLine & SourceFileName & vbNewLine & "هل حقًا تريد حفظ نسخة إحتياطية من الملف المصدر؟"
                                End If
                                If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
                                    File.Copy(SourceFileName, TempFileName)
                                End If
                            'Sticky_Note_TxtBx.SaveFile(SourceFileName, RichTextBoxStreamType.PlainText)
                            My.Computer.FileSystem.WriteAllText(SourceFileName, Sticky_Note_TxtBx.Text, 0, System.Text.Encoding.UTF8)
                            If Language_Btn.Text = "ع" Then
                                Msg = "File Updated Successfully"
                            Else
                                Msg = "تم حفظ الملف بنجاح"
                                End If
                                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MBOs, False)
                                Exit Sub
                            End If
                        End If

                        If Language_Btn.Text = "ع" Then
                        Msg = "File Updated Successfully"
                    Else
                        Msg = "تم حفظ الملف بنجاح"
                    End If
                    If OpenExternalMode Then
                        If Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Checked Then
                            ExtenalFilePath = Path.GetDirectoryName(OldStickyName)
                            ExternalFileName = Path.GetFileName(Sticky_Note_No_CmbBx.Text)
                        Else
                            ExtenalFilePath = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                            ExternalFileName = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                        End If
                        RememberOpenedExternalFiles()
                    End If
                    ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MBOs, False)
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
            Dim FileExist As Boolean
            Dim FileName
            If Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Checked And
                Not String.IsNullOrEmpty(OldStickyName) Then
                If OpenExternalMode Then
                    FileName = OldStickyName
                Else
                    FileName = StickyNoteFolderPath & "\" & OldStickyName & ".txt"
                End If
            ElseIf String.IsNullOrEmpty(NewSticky) Then
                If IsNothing(Sticky_Note_No_CmbBx.SelectedItem) Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "This Sticky Is Not Found In Stored Files Or Its Label May Contains The Character (:)... Kindly Try Again Or If You Want To Change The Sticky Kinldy Put Mark On (Sticky Applicable To Rename) Box Before Amendment!!!"
                    Else
                        Msg = "هذه اللاصقة غير موجودة فى ملفات اللاصقات المحفوظة أو ربما يحمل العنوان الحرف (:)... يرجى إعادة المحاولة أو إن كنت ترغب فى تغيير عنوان اللاصقة فمن فضلك ضع علامة فى مربع (لاصقة قابلة لتعديل الإسم) قبل التعديل"
                    End If
                    ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False)
                    Exit Sub
                Else
                    FileName = StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt"
                End If
            ElseIf Not String.IsNullOrEmpty(NewSticky) Then
                FileName = StickyNoteFolderPath & "\" & NewSticky & ".txt"
            Else
                If Language_Btn.Text = "E" Then
                    Msg = "غير متاح حفظ الملفات على هذا الوضع الحالى... حاول مرة اخرى"
                Else
                    Msg = "Saving Files With The Setuation Is Not Aplicable... Kindly Try Again"
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                Exit Sub
            End If
            Dim TextToWrite As String = Nothing
            Dim SNN, SNL, FileNameInTheFile
            SNN = Path.GetFileNameWithoutExtension(FileName)
            Dim array() As String = ReadFile(FileName, 1)
            If Not IsNothing(array) Then
                FileNameInTheFile = array(0).ToString
            End If
            If File.Exists(FileName) Then
                FileExist = True
                If Available_Sticky_Notes_DGV.CurrentRow.Cells("Blocked_Sticky").Value = 1 And
                    Blocked_Sticky_ChkBx.CheckState = CheckState.Unchecked Then
                    If Language_Btn.Text = "E" Then
                        Msg = "هذا الملف محظور التعامل سيتم فك الحظر... هل انت متأكد؟"
                    Else
                        Msg = "This File Is Blocked File Will Be Unblocked... Are You Sure?"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                    If MyDialogResult = DialogResult.No Then
                        RestoreCheckBoxesValue()
                        Blocked_Sticky_ChkBx.CheckState = CheckState.Checked
                        Exit Sub
                    End If
                ElseIf Available_Sticky_Notes_DGV.CurrentRow.Cells("Blocked_Sticky").Value = 1 And
                    Blocked_Sticky_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "E" Then
                        Msg = "مرفوض حفظ الملفات محظورة التعامل"
                    Else
                        Msg = "Saving Blocked File IS Refused"
                    End If
                    ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, False)
                    Blocked_Sticky_ChkBx.CheckState = CheckState.Checked
                    RestoreCheckBoxesValue()
                    Exit Sub
                ElseIf ActiveControl.Name = ToolStrip1.Name Then
                    If Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                        If Language_Btn.Text = "E" Then
                            Msg = "سيتم حفظ الملف... هل انت متأكد"
                        Else
                            Msg = "This File Will Be Saved... Are You Sure?"
                        End If
                        Dim MyDialogResult = ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                        If MyDialogResult = DialogResult.No Then
                            RestoreCheckBoxesValue()
                            Exit Sub
                        End If
                    End If
                End If
            ElseIf Not String.IsNullOrEmpty(NewSticky) Then
                If Not SaveDialogResultAnseredIsNo Then
                    Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(NewSticky, Sticky_Note_No_CmbBx.Text))
                    Dim Sticky_Note_Text = Sticky_Note_No_CmbBx.Text
                    Sticky_Note_No_CmbBx.Text = Nothing
                    Sticky_Note_No_CmbBx.Text = Sticky_Note_Text
                    If Sticky_Note_No_CmbBx.SelectedIndex = -1 Then
                        If Language_Btn.Text = "E" Then
                            Msg = "غير متاح حفظ الملفات على هذا الوضع الحالى... حاول مرة اخرى"
                        Else
                            Msg = "Saving Files With The Setuation Is Not Aplicable... Kindly Try Again"
                        End If
                        ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                        Exit Sub
                    End If
                    SaveDialogResultAnseredIsNo = False
                End If
            ElseIf SNN <> FileNameInTheFile Then
                If Language_Btn.Text = "E" Then
                    Msg = "أسم الملف مخالف لذلك المسجل بداخل الملف نفسة... غير متاح حفظ الملفات على هذا الوضع... حاول مرة اخرى"
                Else
                    Msg = "The File Name Does Not Matching To The File Name Written In The File Itself... Saving Files With The Setuation Is Not Aplicable... Kindly Try Again"
                End If
                Msg &= vbNewLine & "File Name = " & SNN
                Msg &= vbNewLine & "File Name In The File = " & FileNameInTheFile
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                Exit Sub
            Else
                If Language_Btn.Text = "E" Then
                    Msg = "غير متاح حفظ الملفات على هذا الوضع الحالى... حاول مرة اخرى"
                Else
                    Msg = "Saving Files With The Setuation Is Not Aplicable... Kindly Try Again"
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                Exit Sub
            End If
            If Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Checked Then
                TextToWrite = ":Sticky_Note_Name -(" & OldStickyName & ")-"
                TextToWrite &= ":Sticky_Note_Label -(" & Sticky_Note_No_CmbBx.Text & ")-"
                SNN = ":Sticky_Note_Name -(" & OldStickyName & ")-"
                SNL = ":Sticky_Note_Label -(" & Sticky_Note_No_CmbBx.Text & ")-"
            Else
                TextToWrite = ":Sticky_Note_Name -(" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ")-"
                TextToWrite &= ":Sticky_Note_Label -(" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value & ")-"
                SNN = ":Sticky_Note_Name -(" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ")-"
                SNL = ":Sticky_Note_Label -(" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value & ")-"
            End If
            If Not FileExist Then
                If Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "This Sticky Will Be Saved... Are You Sure?"
                    Else
                        Msg = "سيتم حفظ هذه اللاصقة... هل انت ماأكد"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & vbNewLine & SNN & vbNewLine & SNL, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                    If MyDialogResult = DialogResult.No Then
                        SaveDialogResultAnseredIsNo = True
                        Exit Sub
                    End If
                End If
            End If

            TextToWrite &= ":Sticky_Note_Category -(" & Sticky_Note_Category_CmbBx.Text & ")-"
            TextToWrite &= ":Sticky_Note -(" & Encrypt_Function(Sticky_Note_TxtBx.Rtf) & ")-"
            TextToWrite &= ":Blocked_Sticky -(" & Blocked_Sticky_ChkBx.CheckState & ")-"
            TextToWrite &= ":Finished_Sticky -(" & Finished_Sticky_ChkBx.CheckState & ")-"
            TextToWrite &= ":Sticky_Font -(" & Sticky_Font_TxtBx.Text & ")-"
            TextToWrite &= ":Sticky_Font_Color -(" & Sticky_Font_Color_ClrCmbBx.Text & ")-"
            TextToWrite &= ":Sticky_Back_Color -(" & Sticky_Back_Color_ClrCmbBx.Text & ")-"
            Dim time As DateTime = DateTime.Now
            Dim CreationDate As String
            If FileExist Then
                CreationDate = Available_Sticky_Notes_DGV.CurrentRow.Cells("Creation_Date").Value
                TextToWrite &= ":Creation_Date -(" & Replace(CreationDate, ":", ",") & ")-"
            Else
                CreationDate = time.ToString("yyyy/MM/dd hh:mm:ss.fff tt MMM ddd")
                TextToWrite &= ":Creation_Date -(" & Replace(CreationDate, ":", ",") & ")-"
            End If
            TextToWrite &= ":Secured_Sticky -(" & Secured_Sticky_ChkBx.CheckState & ")-"
            If Secured_Sticky_ChkBx.CheckState = CheckState.Checked Then
                TextToWrite &= ":Sticky_Password -(" & Encrypt_Function(Sticky_Password_TxtBx.Text) & ")-"
            End If
            TextToWrite &= ":Use_Main_Password -(" & Use_Main_Password_ChkBx.CheckState & ")-"
            If Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Checked And (Reminder_Every_Days_NmrcUpDn.Value > 0 Or Reminder_Every_Hours_NmrcUpDn.Value > 0 Or Reminder_Every_Minutes_NmrcUpDn.Value) Then
                Next_Reminder_Time_DtTmPkr.Value = Next_Reminder_Time_DtTmPkr.Value.AddDays(Reminder_Every_Days_NmrcUpDn.Value)
                Next_Reminder_Time_DtTmPkr.Value = Next_Reminder_Time_DtTmPkr.Value.AddHours(Reminder_Every_Hours_NmrcUpDn.Value)
                Next_Reminder_Time_DtTmPkr.Value = Next_Reminder_Time_DtTmPkr.Value.AddMinutes(Reminder_Every_Minutes_NmrcUpDn.Value)

                If Format(Now, "yyyy-MM-dd HH-mm-ss") >= Format(Next_Reminder_Time_DtTmPkr.Value, "yyyy-MM-dd HH-mm-ss") Then
                    Next_Reminder_Time_DtTmPkr.Value = Now.AddDays(Reminder_Every_Days_NmrcUpDn.Value)
                    Next_Reminder_Time_DtTmPkr.Value = Now.AddHours(Reminder_Every_Hours_NmrcUpDn.Value)
                    Next_Reminder_Time_DtTmPkr.Value = Now.AddMinutes(Reminder_Every_Minutes_NmrcUpDn.Value)
                End If
            ElseIf Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Checked Then
                Sticky_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
            End If
            TextToWrite &= ":Sticky_Word_Wrap -(" & Sticky_Word_Wrap_ChkBx.CheckState & ")-"
            TextToWrite &= ":Sticky_Have_Reminder -(" & Sticky_Have_Reminder_ChkBx.CheckState & ")-"
            TextToWrite &= ":Next_Reminder_Time -(" & Format(Next_Reminder_Time_DtTmPkr.Value, "yyyy-MM-dd HH-mm-ss") & ")-"
            'TextToWrite &= ":Next_Reminder_Time -(" & Replace(Next_Reminder_Time_DtTmPkr.Text, ":", ",") & ")-"
            TextToWrite &= ":Reminder_Every -(" & Reminder_Every_Days_NmrcUpDn.Value & "," & Reminder_Every_Hours_NmrcUpDn.Value & "," & Reminder_Every_Minutes_NmrcUpDn.Value & ")-"
            My.Computer.FileSystem.WriteAllText(FileName, TextToWrite, 0, System.Text.Encoding.UTF8)
            If Language_Btn.Text = "ع" Then
                Msg = "The File And Its Parmeters Updated Successfully"
            Else
                Msg = "تم حفظ الملف ومعلماته بنجاح"
            End If
            NewSticky = Nothing


            If Reload_Stickies_After_Amendments_ChkBx.CheckState = CheckState.Unchecked And
                Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "E" Then
                    Msg &= "... هل هل تريد اعادة تحميل اللاصقات بعد التحديث؟"
                Else
                    Msg = "... Do You Want To Reload The Stickies After Update?"
                End If
                Dim MyDialogResult = ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                If MyDialogResult = DialogResult.Yes Then
                    Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                End If
            Else
                If File.Exists(FileName) And
                    Reload_Stickies_After_Amendments_ChkBx.CheckState = CheckState.Unchecked Then
                    ReadFile(FileName,,, Available_Sticky_Notes_DGV.CurrentRow)
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False, DisplayMsg)
            End If


            CommingFromSaveToolStripButton = True
            FileCount = String.Empty
            If Reload_Stickies_After_Amendments_ChkBx.CheckState = CheckState.Checked Then
                Dim SelectedItem = Sticky_Note_No_CmbBx.Text
                Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                Sticky_Note_No_CmbBx.Text = Nothing
                Sticky_Note_No_CmbBx.SelectedIndex = -1
                Sticky_Note_No_CmbBx.Text = SelectedItem
            End If
            StickyNoteRTF = Sticky_Note_TxtBx.Rtf
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MakeTopMost()
            Me.Focus()
            CommingFromSaveToolStripButton = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function Replace_TxtBx(ByVal TextToFind As String, Optional StartPosition As Integer = 0, Optional FindNext As Boolean = False) As Object
        FindArray.Initialize() ' = FindArray(0)
        Try
            Dim xx = Sticky_Note_TxtBx.Find(TextToFind, StartPosition, CType(0, RichTextBoxFinds))
            If xx <> -1 Then
                'FindArray.Initialize()
                Sticky_Note_TxtBx.SelectedText = Replace(Sticky_Note_TxtBx.SelectedText, Sticky_Note_TxtBx.SelectedText, ReplaceBy_TxtBx.Text)
                FindArray(FindArray.Length - 1) = "SelectionStart = " & xx & " Length = " & TextToFind.Length
                Sticky_Note_TxtBx.Focus()
                Sticky_Note_TxtBx.SelectionStart = xx
                Sticky_Note_TxtBx.SelectionLength = TextToFind.Length
                Sticky_Note_TxtBx.SelectionColor = Color.Red
                Sticky_Note_TxtBx.SelectionBackColor = Color.Black
                Array.Resize(FindArray, FindArray.Length + 1)
                If xx + TextToFind.Length >= Sticky_Note_TxtBx.Text.Length Then
                    If FindNext Then Return 0
                    Exit Function
                End If
                If FindNext Then Return xx + TextToFind.Length
                Replace_TxtBx(TextToFind, xx + TextToFind.Length)
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function FindInSticky_Note_TxtBx(ByVal TextToFind As String, Optional StartPosition As Integer = 0, Optional FindNext As Boolean = False) As Object
        Me.Cursor = Cursors.WaitCursor
        FindArray.Initialize()
        Try
            Dim xx = Sticky_Note_TxtBx.Find(TextToFind, StartPosition, CType(0, RichTextBoxFinds))
            If xx <> -1 Then
                'FindArray.Initialize()
                FindArray(FindArray.Length - 1) = "SelectionStart = " & xx & " Length = " & TextToFind.Length
                Sticky_Note_TxtBx.Focus()
                Sticky_Note_TxtBx.SelectionStart = xx
                Sticky_Note_TxtBx.SelectionLength = TextToFind.Length
                Sticky_Note_TxtBx.SelectionColor = Color.Red
                Sticky_Note_TxtBx.SelectionBackColor = Color.Black
                Array.Resize(FindArray, FindArray.Length + 1)
                If xx + TextToFind.Length >= Sticky_Note_TxtBx.Text.Length Then
                    If FindNext Then Return 0
                    Exit Function
                End If
                If FindNext Then Return xx + TextToFind.Length
                FindInSticky_Note_TxtBx(TextToFind, xx + TextToFind.Length)
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub Delete_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Delete_TlStrpBtn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Sticky_Note_TxtBx.TextLength = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Selecet Sticky Not First To Delete!!!"
                Else
                    Msg = "من فضلك إختر الملاحظة اللاصقة التى ترغب فى إلغائها!!!"
                End If
                ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MBOs, False)
                Exit Sub
            End If
            Dim FileName
            If OpenExternalMode Then
                FileName = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
            Else
                FileName = StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt"
            End If
            If File.Exists(FileName) Then
                If Warning_Before_Delete_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "E" Then
                        Msg = "سيتم إلغاء الملف... هل انت متأكد"
                    Else
                        Msg = "This File Will Be Deleyed... Are You Sure?"
                    End If
                    If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                        Exit Sub
                    End If
                End If
                My.Computer.FileSystem.DeleteFile(FileName, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                If Language_Btn.Text = "ع" Then
                    Msg = "This Sticky Sent To Recycle Ben Successfully... And Not Erased From Sticky Notes Preview Table Yet"
                Else
                    Msg = "تم إرسال هذه اللاصقة إلى سلة المهملات بنجاح... ولم يتم ازالتها من جدول عرض اللاصقات بعد"
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                If Not OpenExternalMode Then
                    Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub printDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Me.Cursor = Cursors.WaitCursor
        Try
            ' The PrintPage event is raised for each page to be printed.
            Dim linesPerPage As Single = 0
            Dim yPos As Single = 0
            Dim count As Integer = 0
            Dim leftMargin As Single = e.MarginBounds.Left
            Dim topMargin As Single = e.MarginBounds.Top
            Dim line As String = Nothing
            If IsNothing(streamToPrint.ReadLine()) Then
                streamToPrint.Close()
                If Not StreamToPrintDone(StickyNoteFolderPath & "\FileToPrint.txt") Then Exit Sub
            End If

            ' Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics)
            ' Print each line of the file.
            While count < linesPerPage
                line = streamToPrint.ReadLine()
                If line Is Nothing Then
                    Exit While
                End If
                yPos = topMargin + count * printFont.GetHeight(e.Graphics)
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
                count += 1
            End While

            ' If more lines exist, print another page.
            If (line IsNot Nothing) Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Me.Cursor = Cursors.WaitCursor
        Dim FileToPrint = StickyNoteFolderPath & "\FileToPrint.txt"
        Try
            If Not StreamToPrintDone(FileToPrint) Then Exit Sub
            Try
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog(Me)
                Me.PrintPreviewDialog1.BringToFront()
                'PrintDocument1.Print()
            Finally
                streamToPrint.Close()
            End Try
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            If File.Exists(FileToPrint) Then
                File.Delete(FileToPrint)
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function StreamToPrintDone(FileToPrint) As Boolean
        Try
            If File.Exists(FileToPrint) Then
                File.Delete(FileToPrint)
            End If
            Dim FileName = StickyNoteFolderPath & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & ".txt"
            'Blocked_Sticky_ChkBx.CheckState = CType(Val(Array(3).ToString), CheckState)
            My.Computer.FileSystem.WriteAllText(FileToPrint, vbNewLine & ReadFile(FileName, True)(2), 0, System.Text.Encoding.UTF8)
            'My.Computer.FileSystem.WriteAllText(FileToPrint, ReadFile(FileName, True), 0, System.Text.Encoding.UTF8)
            streamToPrint = New StreamReader(StickyNoteFolderPath & "\FileToPrint.txt")
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub CutToolStripButton_Click(sender As Object, e As EventArgs) Handles CutToolStripButton.Click
        Clipboard_Cut()
    End Sub
    Private Sub CopyToolStripButton_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton.Click
        Clipboard_Copy()
    End Sub
    Private Sub Exit_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Exit_TlStrpBtn.Click
        Try
            If AlreadyAsked Or
                (OpenExternalMode And Not NormalSticky) Then
                Exit Sub
            End If
            AlreadyAsked = True
            If Language_Btn.Text = "ع" Then
                Msg = "Sticky Note Will Be Always In Windows System Tray And Ready To Call By Clicking (Alt+s) Until You Exit The Sticky From The Windows System Tray!!!"
            Else
                Msg = "تذكر دائما أن برنامج الملاحظات اللاصقة سيكون مخفيا فى علبة الايقونات المخفية للنوافذ ويمكن استدعائة بالنقر على ـ(Alt+s)ـ... حتى يتم الخروج من البرنامج"
            End If
            ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Close()
        End Try
    End Sub

#Region "RchTxtBx"
    Private Function ChechRchTxtBx() As Boolean
        If IsNothing(Sticky_Note_TxtBx) Then
            If Language_Btn.Text = "ع" Then
                Msg = "Select One Of Description Object First To Deal With It!!!"
            Else
                Msg = "إختر واحد من المواضيع أولا للتعامل معه"
            End If
            ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False)
            Return False
        End If
        Return True
    End Function

    Private Sub rtb_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            BoldToolStripButton.Checked = sender.SelectionFont.Bold
            UnderlineToolStripButton.Checked = sender.SelectionFont.Underline
            LeftToolStripButton.Checked = IIf(sender.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left, True, False)
            CenterToolStripButton.Checked = IIf(sender.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center, True, False)
            RightToolStripButton.Checked = IIf(sender.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right, True, False)
            BulletsToolStripButton.Checked = sender.SelectionBullet
            Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Sticky_Note_TxtBx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sticky_Note_TxtBx.LostFocus
        sender.AcceptsTab = False
    End Sub
    Dim SavedSticky As Boolean
    Private Sub Sticky_Note_TxtBx_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sticky_Note_TxtBx.GotFocus
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name <> sender.Name Then Exit Sub
        sender.AcceptsTab = True
    End Sub
    Private Sub FontToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not ChechRchTxtBx() Then Exit Sub
        If IsNothing(Sticky_Note_TxtBx.SelectionFont) Then
            If FontDlg.ShowDialog() <> DialogResult.Cancel Then Exit Sub
        End If
        Sticky_Note_TxtBx.SelectionFont = FontDlg.Font
    End Sub
    Private Sub Text_Color_TlStrpBtn_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton1.ButtonClick
        Sticky_Note_TxtBx.SelectionColor = Selected_Text_Color_TlStrpMnItm.BackColor
    End Sub
    Private Sub Selected_Text_Color_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Selected_Text_Color_TlStrpMnItm.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If ColorDlg.ShowDialog() <> DialogResult.Cancel Then
            Selected_Text_Color_TlStrpMnItm.BackColor = ColorDlg.Color
            'ToolStripSplitButton1.BackColor = ColorDlg.Color
            Sticky_Note_TxtBx.SelectionColor = ColorDlg.Color
        End If
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If FontDlg.ShowDialog() <> DialogResult.Cancel Then
            Sticky_Note_TxtBx.SelectionFont = FontDlg.Font
        End If
    End Sub

    Private Sub BoldToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BoldToolStripButton.Click
        ' Switch Bold
        If Not ChechRchTxtBx() Then Exit Sub
        Try
            Dim currentFont As System.Drawing.Font = Sticky_Note_TxtBx.SelectionFont
            If IsNothing(Sticky_Note_TxtBx.SelectionFont) Then
                currentFont = New Drawing.Font(Font.FontFamily, Font.Size, Drawing.FontStyle.Regular)
                Sticky_Note_TxtBx.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, Drawing.FontStyle.Regular)
            End If
            If Sticky_Note_TxtBx.SelectedText.Length > 0 Then
                If Sticky_Note_TxtBx.SelectionFont.Bold = True Then
                    Sticky_Note_TxtBx.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, Drawing.FontStyle.Regular)
                Else
                    Sticky_Note_TxtBx.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, Drawing.FontStyle.Bold)
                End If
            Else
                If Sticky_Note_TxtBx.SelectionFont.Bold = True Then
                    Sticky_Note_TxtBx.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, Drawing.FontStyle.Regular)
                Else
                    Sticky_Note_TxtBx.SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, Drawing.FontStyle.Bold)
                End If
            End If
        Catch ex As Exception
            If Not ex.Message.Contains("Object reference not set to an instance of an object.") Then
                ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Sub
    Private Sub UnderlineToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnderlineToolStripButton.Click
        ' Switch Underline
        If Not ChechRchTxtBx() Then Exit Sub
        'Dim currentFont As System.Drawing.Font = Sticky_Note_TxtBx.SelectionFont
        'Dim FontUnderline = New Font(currentFont, FontStyle.Underline)
        'Dim FontNotUnderline = New Font(currentFont, FontStyle.Regular)
        Dim FontBold As Boolean
        If Sticky_Note_TxtBx.SelectionFont.Bold = True Then
            FontBold = True
        End If

        If Sticky_Note_TxtBx.SelectionFont.Underline = True Then
            If FontBold Then
                Sticky_Note_TxtBx.SelectionFont = New Font(Sticky_Note_TxtBx.SelectionFont.Name, Sticky_Note_TxtBx.SelectionFont.Size, FontStyle.Bold Or FontStyle.Regular)
            Else
                Sticky_Note_TxtBx.SelectionFont = New Font(Sticky_Note_TxtBx.SelectionFont.Name, Sticky_Note_TxtBx.SelectionFont.Size, FontStyle.Regular)
            End If
        Else
            If FontBold Then
                Sticky_Note_TxtBx.SelectionFont = New Font(Sticky_Note_TxtBx.SelectionFont.Name, Sticky_Note_TxtBx.SelectionFont.Size, FontStyle.Bold Or FontStyle.Underline)
            Else
                Sticky_Note_TxtBx.SelectionFont = New Font(Sticky_Note_TxtBx.SelectionFont.Name, Sticky_Note_TxtBx.SelectionFont.Size, FontStyle.Underline)
            End If
        End If
        'UnderlineToolStripButton.Checked = IIf(Sticky_Note_TxtBx.SelectionFont.Underline, True, False)
    End Sub
    Private Sub LeftToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripButton.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Sticky_Note_TxtBx.SelectionAlignment = HorizontalAlignment.Left
        Sticky_Note_TxtBx.LanguageOption = RightToLeftLayout
        Sticky_Note_TxtBx.RightToLeft = RightToLeft.No
    End Sub
    Private Sub CenterToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CenterToolStripButton.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Sticky_Note_TxtBx.SelectionAlignment = HorizontalAlignment.Center
    End Sub
    Private Sub RightToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightToolStripButton.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Sticky_Note_TxtBx.SelectionAlignment = HorizontalAlignment.Right
        Sticky_Note_TxtBx.LanguageOption = RightToLeftLayout
        Sticky_Note_TxtBx.RightToLeft = RightToLeft.Yes
    End Sub
    Private Sub BulletsToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulletsToolStripButton.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Sticky_Note_TxtBx.SelectionBullet = Not Sticky_Note_TxtBx.SelectionBullet
        BulletsToolStripButton.Checked = Sticky_Note_TxtBx.SelectionBullet
    End Sub
    Private Sub WordWrap_TlStrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordWrap_TlStrp.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If Sticky_Note_TxtBx.WordWrap Then
            Sticky_Word_Wrap_ChkBx.CheckState = CheckState.Unchecked
            Sticky_Note_TxtBx.WordWrap = False
        Else
            Sticky_Word_Wrap_ChkBx.CheckState = CheckState.Checked
            Sticky_Note_TxtBx.WordWrap = True
        End If
        Try
            If File.Exists(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) Then
                SaveToolStripButton.PerformClick()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Sticky_Note_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Sticky_Note_TxtBx.TextChanged
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            If ActiveControl.Name <> sender.name Then Exit Sub
            If Not IsNothing(RTFBackup) Then
                If RTFBackup.text <> Sticky_Note_TxtBx.Text And
                    Sticky_Note_TxtBx.Text.Length > 0 Then
                    RTFBackup.rtf = Sticky_Note_TxtBx.Rtf
                End If
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Private Sub PasteToolStripButton_Click(sender As Object, e As EventArgs) Handles PasteToolStripButton.Click
        Clipboard_Past()
    End Sub
#End Region
    Public Function ConvertRichTextBox(Optional Description As String = Nothing) As String
        Try
            If IsNothing(Description) Then Return Nothing
            Dim rtf As New RichTextBox
            If Description.Contains("\rtf1\") Then
                rtf.Rtf = Replace(Replace(Replace(Description, "]", ""), "[", ""), "''", "'")
            ElseIf Description.Contains("\urtf1\") Then
                rtf.Rtf = Replace(Replace(Replace(Replace(Replace(Description,
                                                             "{\urtf1\fbidis\ansi\ansicpg65001\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset178 Times New Roman;}}", "{\rtf1\fbidis\ansi\deff0{\fonttbl{\f0\fnil\fcharset178 Times New Roman;}}"),
                                                          "]", ""),
                                                          "[", ""),
                                                          "''", "'"),
                                                          "{\urtf1\fbidis\ansi\ansicpg65001\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset178 Times New Roman;}{\f1\fnil\fcharset0 Times New Roman;}}", "{\rtf1\fbidis\ansi\deff0{\fonttbl{\f0\fnil\fcharset178 Times New Roman;}}")
            Else
                rtf.Text = Replace(Replace(Replace(Description, "]", ""), "[", ""), "''", "'")
            End If
            Return rtf.Text
        Catch ex As Exception
            If Not ex.Message.Contains("Object reference not set to an instance of an object.") Then
                ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
            Return Nothing
        End Try

    End Function
    Private Sub Blocked_Sticky_Note_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Blocked_Sticky_ChkBx.CheckedChanged
        If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
            SNN_SelectedItem = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        End If
    End Sub
    Dim CommingFromSaveToolStripButton As Boolean
    Private Sub Sticky_Note_No_TxtBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sticky_Note_No_CmbBx.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked Then
                Blocked_Sticky_ChkBx.CheckState = CheckState.Unchecked
                Finished_Sticky_ChkBx.CheckState = CheckState.Unchecked
                Secured_Sticky_ChkBx.CheckState = CheckState.Unchecked
                Use_Main_Password_ChkBx.CheckState = CheckState.Unchecked
                Sticky_Word_Wrap_ChkBx.CheckState = CheckState.Unchecked
                Sticky_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
                Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Unchecked
                Reminder_Every_Days_NmrcUpDn.Value = 0
                Reminder_Every_Hours_NmrcUpDn.Value = 0
                Reminder_Every_Minutes_NmrcUpDn.Value = 0
                Sticky_Password_TxtBx.Text = Nothing
            End If
            If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
                FileCount = Nothing
                NewSticky = Nothing
                If Not IsNothing(OpenExternalMode) Then
                    OpenExternalMode = False
                End If
            End If

            RTFBackup = Nothing
            If IsNothing(ActiveControl) Then Exit Sub
            If sender.name <> ActiveControl.Name And
                Not CommingFromSaveToolStripButton Then Exit Sub
            If Sticky_Note_No_CmbBx.SelectedIndex = -1 Then
                Sticky_Note_No_CmbBx.Text = Nothing
                Sticky_Note_TxtBx.Text = Nothing
                Exit Sub
            End If
            Dim StickFoundInAvailableStickyNotesDGVRows As Boolean
            For Each File In Available_Sticky_Notes_DGV.Rows
                If File.cells("Sticky_Note_Name").value = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key Then
                    Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV.Rows(File.cells(0).Rowindex).Cells(0)
                    Available_Sticky_Notes_DGV.ClearSelection()
                    Available_Sticky_Notes_DGV.Rows(File.cells(0).Rowindex).Selected = True
                    StickFoundInAvailableStickyNotesDGVRows = True
                    Exit For
                End If
            Next
            If Not (StickFoundInAvailableStickyNotesDGVRows) Then
                If Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".txt" Then
                    Sticky_Note_TxtBx.Text = My.Computer.FileSystem.ReadAllText(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value, System.Text.Encoding.UTF8)

                    If Not IsNothing(OpenExternalMode) Then
                        OpenExternalMode = True
                    End If
                ElseIf Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".rtf" Then
                    Sticky_Note_TxtBx.LoadFile(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value, RichTextBoxStreamType.RichText)
                    If Not IsNothing(OpenExternalMode) Then
                        OpenExternalMode = True
                    End If
                Else
                    Sticky_Note_TxtBx.Text = My.Computer.FileSystem.ReadAllText(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value, System.Text.Encoding.UTF8)
                    If Not IsNothing(OpenExternalMode) Then
                        OpenExternalMode = True
                    End If
                End If
                StickyNoteRTF = Sticky_Note_TxtBx.Rtf
            End If
            If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
                MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = "Current File Location = " & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
            End If
            If OpenExternalMode And
            Not Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".rtf" Then
                Dim brightness = Correct_Sticky_Note_TxtBx_Font_Color(1)
                If brightness < 0.4 Then
                    Sticky_Font_Color_ClrCmbBx.Text = "Window"
                    Sticky_Back_Color_ClrCmbBx.Text = "WindowText"
                Else
                    Sticky_Font_Color_ClrCmbBx.Text = "WindowText"
                    Sticky_Back_Color_ClrCmbBx.Text = "Window"
                End If
            ElseIf Path.GetExtension(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".rtf" Then
                Sticky_Back_Color_ClrCmbBx.Text = "DarkCyan"
            End If
        Catch ex As Exception
            If Not ex.Message.Contains("Object reference Not Set To an instance Of an Object.") Then
                ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Finally
            Sticky_Password_TxtBx.Text = Nothing
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Language_Btn_Click(sender As Object, e As EventArgs) Handles Language_Btn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Sticky_Note_TxtBx.Focus()
            Dim myCurrentLanguage As InputLanguage = InputLanguage.CurrentInputLanguage
            Dim myInputLanguage As InputLanguageCollection = InputLanguage.InstalledInputLanguages
            Select Case Language_Btn.Text
                Case <> "ع"
                    For Each language In myInputLanguage
                        If myCurrentLanguage.LayoutName <> language.LayoutName Then
                            InputLanguage.CurrentInputLanguage = language
                            LabelingForm("English")
                        End If
                    Next
                    Me.RightToLeftLayout = False
                    Me.RightToLeft = RightToLeft.No
                    'End If
                    Language_Btn.Text = "ع"
                Case <> "E"
                    For Each language In myInputLanguage
                        If myCurrentLanguage.LayoutName <> language.LayoutName Then
                            InputLanguage.CurrentInputLanguage = language
                            LabelingForm("Arabic")
                        End If
                    Next
                    Me.RightToLeftLayout = True
                    Me.RightToLeft = RightToLeft.Yes
                    'End If
                    Language_Btn.Text = "E"
            End Select
            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MakeTopMost()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Form_Transparency_TrkBr_ValueChanged(sender As Object, e As EventArgs) Handles Form_Transparency_TrkBr.ValueChanged
        Me.Opacity = Form_Transparency_TrkBr.Value / 100
        Application.DoEvents()
    End Sub

    Private Sub Available_Sticky_Notes_DGV_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles Available_Sticky_Notes_DGV.SortCompare
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ActiveControl = Sticky_Note_TxtBx
            If IsNothing(e.CellValue1) Then Exit Sub
            If String.IsNullOrEmpty(e.CellValue1.ToString) Or
                String.IsNullOrEmpty(e.CellValue2.ToString) Then
                e.SortResult = String.Compare(e.CellValue1.ToString, e.CellValue2.ToString)
                e.Handled = True
            End If
            Available_Sticky_Notes_DGV.ClearSelection()
        Catch ex As Exception
            e.Handled = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Sticky_Back_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sticky_Back_Color_ClrCmbBx.SelectedIndexChanged
    End Sub

    Private Sub Sticky_Note_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Sticky_Note_No_CmbBx.TextChanged
    End Sub

    Private Sub Form_Color_Like_Sticky_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Form_Color_Like_Sticky_ChkBx.CheckedChanged

    End Sub

    Private Sub Form_Color_Like_Sticky_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Form_Color_Like_Sticky_ChkBx.CheckStateChanged
        If Form_Color_Like_Sticky_ChkBx.CheckState = CheckState.Checked Then
            Sticky_Form_Color_ClrCmbBx.Enabled = False
        Else
            Sticky_Form_Color_ClrCmbBx.Enabled = True
        End If
    End Sub

    Private Sub Secured_Sticky_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Secured_Sticky_ChkBx.CheckedChanged

    End Sub

    Private Sub Secured_Sticky_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Secured_Sticky_ChkBx.CheckStateChanged
        If Secured_Sticky_ChkBx.CheckState = CheckState.Checked Then
            Sticky_Password_TxtBx.ReadOnly = False
        ElseIf Secured_Sticky_ChkBx.CheckState = CheckState.Unchecked Then
            Sticky_Password_TxtBx.ReadOnly = True
        End If
    End Sub

    Private Sub Available_Sticky_Notes_DGV_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Available_Sticky_Notes_DGV.ColumnWidthChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Public Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OpenFileDialog As New OpenFileDialog
            If Not String.IsNullOrEmpty(UseArgFile) Then
                OpenFileDialog.FileName = UseArgFile
                UseArgFile = String.Empty
                GoTo EXUseArgFile
            End If
            Add_New_Sticky_Note_Btn.PerformClick()
            OpenFileDialog.Title = "Importing Files"
            If Sticky_Note_No_CmbBx.Text.Length > 0 Then
                OpenFileDialog.Filter = "Currnt File|" & Sticky_Note_No_CmbBx.Text & ".txt" & "|RTF Files|*.rtf|Text Files|*.txt|All files|*.*"
            Else
                OpenFileDialog.Filter = "Text Files|*.txt|RTF Files|*.rtf|All files|*.*"
            End If
            OpenFileDialog.Multiselect = False
            OpenFileDialog.FileName = ""
            OpenFileDialog.RestoreDirectory = True
            If OpenFileDialog.ShowDialog = DialogResult.Cancel Then
                Exit Sub
            End If
EXUseArgFile:
            Sticky_Note_No_CmbBx.Focus()
            Sticky_Note_No_CmbBx.Text = Path.GetFileName(OpenFileDialog.FileName)
            If Not Convert.ToBoolean(ParseCommandLineArgs(OpenFileDialog.FileName, 1)) Then
                Exit Sub
            End If
            If Path.GetExtension(OpenFileDialog.FileName) = ".rtf" Then
                Dim TemRichText As New RichTextBox
                TemRichText.LoadFile(OpenFileDialog.FileName, RichTextBoxStreamType.RichText)
                TemRichText.SelectAll()
                TemRichText.Copy()
                Sticky_Note_TxtBx.SelectionStart = Sticky_Note_TxtBx.Text.Length
                Sticky_Note_TxtBx.Paste()
                Sticky_Note_TxtBx.Focus()
            Else
                Dim Sticky_Note = My.Computer.FileSystem.ReadAllText(OpenFileDialog.FileName, System.Text.Encoding.UTF8)
                Sticky_Note_TxtBx.Text = Sticky_Note
                Sticky_Note_TxtBx.Focus()
            End If
            'ParseCommandLineArgs(OpenFileDialog.FileName, 1)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try

    End Sub

    Private Sub Save_Setting_When_Exit_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Save_Setting_When_Exit_ChkBx.CheckedChanged
        Save_Setting_When_Exit = Save_Setting_When_Exit_ChkBx.CheckState
    End Sub

    Private Sub Form_Size_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Form_Size_TlStrpBtn.Click
        If IsNothing(ActiveControl) Then Exit Sub
        If Setting_TbCntrl.Visible Then
            If Sticky_Note_Pnl.Dock = DockStyle.Fill Then
                Setting_TbCntrl.Visible = False
            Else
                Setting_TbCntrl.Visible = False
                Sticky_Note_Pnl.Dock = DockStyle.Fill
                Sticky_Note_Pnl.SendToBack()
            End If
        Else
            If Sticky_Note_Pnl.Dock = DockStyle.Fill Then
                Setting_TbCntrl.Visible = True
                Setting_TbCntrl.Dock = DockStyle.Bottom
                Setting_TbCntrl.SendToBack()
            Else
                Setting_TbCntrl.Visible = True
                Setting_TbCntrl.Dock = DockStyle.Fill
            End If
        End If
        ToolStrip1.SendToBack()
    End Sub

    Private Sub Sticky_Note_Form_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter,
            Sticky_Note_TxtBx.MouseEnter, ToolStrip1.MouseEnter, Sticky_Notes_Header_Pnl.MouseEnter, Sticky_Note_Pnl.MouseEnter, Setting_TbCntrl.MouseEnter, Sticky_Navigater_Pnl.MouseEnter,
            Form_Transparency_TrkBr.MouseEnter, MsgBox_SttsStrp.MouseEnter
        MakeTopMost()
        'Me.Focus()
        'Me.Activate()
        'Application.DoEvents()
    End Sub
    Private Sub Sticky_Back_Color_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sticky_Back_Color_ClrCmbBx.SelectedValueChanged
        If Sticky_Back_Color_ClrCmbBx.SelectedItem = Me.TransparencyKey Then
            If Language_Btn.Text = "E" Then
                Msg = "This Color Will Make Your Sticky Transparent Background... Do You Want To Continue?"
            Else
                Msg = "هذا اللون سيجعل خلفية اللاصقة شفافة... هل تريد الإستمرار؟"
            End If
            If ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                If Sticky_Back_Color_ClrCmbBx.SelectedIndex + 1 = Sticky_Back_Color_ClrCmbBx.Items.Count Then
                    Sticky_Back_Color_ClrCmbBx.SelectedIndex = 0
                Else
                    Sticky_Back_Color_ClrCmbBx.SelectedIndex = Sticky_Back_Color_ClrCmbBx.SelectedIndex + 1
                End If
            End If
        End If
        Try
            Sticky_Note_TxtBx.BackColor = Color.FromName(Sticky_Back_Color_ClrCmbBx.Text)
            If Form_Color_Like_Sticky_ChkBx.CheckState = CheckState.Checked Then
                ChangeFormControlsColors(Sticky_Back_Color_ClrCmbBx.Text)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Sticky_Note_Form_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.Cursor = Cursors.WaitCursor
            Try
                Directory.GetAccessControl(Application.StartupPath & "\Sticky_Notes_Files")
                If Not System.IO.Directory.Exists(Application.StartupPath & "\Sticky_Notes_Files") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\Sticky_Notes_Files")
                End If
            Catch UAE As UnauthorizedAccessException
                'Return True
                ShowMsg(UAE.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Catch ex As Exception
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Sticky_Notes_Files")
            End Try
            'LoadExternalFiles()
            SetAppParameters()
            If Backup_Folder_Path_TxtBx.TextLength = 0 Then
                Backup_Folder_Path_TxtBx.Text = Application.StartupPath & "\Sticky_Backup_Folder"
            End If
            If Periodically_Backup_Stickies_ChkBx.CheckState = CheckState.Checked Then
                Backup_Timer.Start()
            Else
                Backup_Timer.Stop()
            End If
            If OpenExternalFile() Then
                Preview_Btn.Enabled = False
                Save_Sticky_Form_Parameter_Setting_Btn.Enabled = False
                RememberOpenedExternalFiles()
                GoTo OpenExternalFile
            Else
                NormalSticky = True
            End If
            Dim SNN_CmbBx = Sticky_Note_No_CmbBx.Text
            Sticky_Note_No_CmbBx.Text = Nothing
ReEnterMainPassword:
            If Enter_Password_To_Pass_ChkBx.CheckState = CheckState.Checked Then
                Me.Enabled = False
                PassedMainPasswordToPass = False
                EnterPasswordToPass()
                While Input_Form.Visible
                    Application.DoEvents()
                End While
                If Main_Password_TxtBx.Text <> EnteredPassword Or String.IsNullOrEmpty(EnteredPassword) Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Wrong Main Password Do You Want To Tay Again?"
                    Else
                        Msg = "كلمة السر خاطئة... هل تريد إعادة المحاولة"
                    End If
                    If ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MBOs, False) = DialogResult.No Then
                        End
                    Else
                        GoTo ReEnterMainPassword
                    End If
                ElseIf Main_Password_TxtBx.Text = EnteredPassword Then
                    PassedMainPasswordToPass = True
                    Me.Enabled = True
                End If
            End If

            Me.ActiveControl = Preview_Btn
            Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
            Me.ActiveControl = Sticky_Note_No_CmbBx
            Sticky_Note_No_CmbBx.Text = SNN_CmbBx
OpenExternalFile:
            If Not OpenExternalMode Then
                Available_Sticky_Notes_DGV.Sort(Available_Sticky_Notes_DGV.Columns("Creation_Date"), System.ComponentModel.ListSortDirection.Descending)
            Else
                Sticky_Note_No_CmbBx.Text = ExternalFileName
            End If
            CreatMenu()
            Tray_Click(Tray, Nothing)
            If Check_For_New_Version() Then
                Update_New_Version_Form.Show(Me)
                Update_New_Version_Form.File_Name_TxtBx.Text = UpdateFileName
                Update_New_Version_Form.Current_Version_TxtBx.Text = My.Application.Info.Version.ToString
                Update_New_Version_Form.Update_Version_TxtBx.Text = UpdateFileVersion
                Update_New_Version_Form.Update_Download_File_Path_TxtBx.Text = UpdateDownloadFilePath
            End If
            If Language_Btn.Text = "E" Then
                Me.Text = "مذكرة لاصقة... إصدار (" & My.Application.Info.Version.ToString & ")"
            ElseIf Language_Btn.Text = "ع" Then
                Me.Text = "Sticky Note... Version (" & My.Application.Info.Version.ToString & ")"
            End If
            Application.DoEvents()
            Me_As_Default_Text_File_Editor_ChkBx_CheckStateChanged(Me_As_Default_Text_File_Editor_ChkBx, EventArgs.Empty)
            If OpenExternalMode Then
                LoadExternalFiles()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            listView1.AllowDrop = True
            Sticky_Note_TxtBx.AllowDrop = True
            Me.PrintPreviewDialog1 = New PrintPreviewDialog
            'Set the size, location, and name.
            Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(Me.Width, Me.Height)
            Me.PrintPreviewDialog1.Location = New System.Drawing.Point(Me.Top + 20, Me.Left + 20)
            Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
            ' Set the minimum size the dialog can be resized to.
            Me.PrintPreviewDialog1.MinimumSize = New System.Drawing.Size(375, 250)
            ' Set the UseAntiAlias property to true, which will allow the 
            ' operating system to smooth fonts.
            Me.PrintPreviewDialog1.UseAntiAlias = True
            Me.PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
            printFont = New Font("Times New Roman", 12)
            IgnoreStickyAmendmented = False
            If IsNothing(OpenExternalMode) Then
                OpenExternalMode = False
            End If
            If Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked Then
                Me.TopMost = False
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Dim User_Password_TxtBx As New TextBox
    Private Function EnterPasswordToPass() As Boolean
        Input_Form = New Form
        Dim FindText_Lbl As New Label
        User_Password_TxtBx = New TextBox
        Input_Form.Icon = My.Resources.unnamed
        Input_Form.MaximizeBox = False
        Input_Form.MinimizeBox = False
        Input_Form.FormBorderStyle = FormBorderStyle.FixedSingle
        Input_Form.Size = New Size(300, 70)
        FindText_Lbl.Size = New Size(100, 20)
        FindText_Lbl.Location = New Point(9, 5)
        FindText_Lbl.TextAlign = ContentAlignment.MiddleCenter
        FindText_Lbl.BorderStyle = BorderStyle.FixedSingle
        User_Password_TxtBx.Size = New Size(Input_Form.Width - 133, 20)
        User_Password_TxtBx.Location = New Point(FindText_Lbl.Left + FindText_Lbl.Width + 2, FindText_Lbl.Top)
        User_Password_TxtBx.UseSystemPasswordChar = True
        Input_Form.Controls.Add(FindText_Lbl)
        Input_Form.Controls.Add(User_Password_TxtBx)
        User_Password_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Input_Form.Refresh()
        AddHandler Input_Form.Load, AddressOf Input_Form_Form_Load
        AddHandler Input_Form.FormClosing, AddressOf Input_Form_FormClosing
        AddHandler User_Password_TxtBx.TextChanged, AddressOf User_Password_TxtBx_TextChanged
        If Language_Btn.Text = "ع" Then
            Input_Form.Text = "Password Form"
            FindText_Lbl.Text = "Enter Password"
        Else
            Input_Form.Text = "شاشة كلمة سر المرور"
            FindText_Lbl.Text = "كلمة سر المرور"
        End If
        Input_Form.Show(Me)
        Input_Form.Activate()
        Input_Form.Focus()
        User_Password_TxtBx.Focus()
    End Function
    Dim EnteredPassword As String
    Private Sub User_Password_TxtBx_TextChanged(sender As Object, e As EventArgs)
        If User_Password_TxtBx.TextLength >= Main_Password_TxtBx.TextLength Then
            EnteredPassword = User_Password_TxtBx.Text
            Input_Form.Close()
        End If
    End Sub
    Private Sub Blocked_Sticky_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Blocked_Sticky_ChkBx.CheckStateChanged,
        Finished_Sticky_ChkBx.CheckStateChanged,
        Secured_Sticky_ChkBx.CheckStateChanged,
        Use_Main_Password_ChkBx.CheckStateChanged,
        Sticky_Word_Wrap_ChkBx.CheckStateChanged
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            Select Case sender.name
                Case Blocked_Sticky_ChkBx.Name, Finished_Sticky_ChkBx.Name
                    If Blocked_Sticky_ChkBx.CheckState = CheckState.Unchecked And
                        Finished_Sticky_ChkBx.CheckState = CheckState.Unchecked Then
                        Sticky_Note_TxtBx.ReadOnly = False
                    ElseIf Blocked_Sticky_ChkBx.CheckState = CheckState.Checked Or
                        Finished_Sticky_ChkBx.CheckState = CheckState.Checked Then
                        Sticky_Note_TxtBx.ReadOnly = True
                    End If
            End Select
            If ActiveControl.Name <> sender.name Or
                (Sticky_Note_No_CmbBx.SelectedIndex = -1 And
                Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked) Then
                Exit Sub
            End If
            If Not TheStickyIsAplicableToSave() Then Exit Sub
            ToolStrip1.Focus()
            Me.ActiveControl = ToolStrip1
            SaveToolStripButton_Click(SaveToolStripButton, EventArgs.Empty)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim ReminderTime = Microsoft.VisualBasic.DateAndTime.Timer + 30

    Private Sub Backup_Timer_Tick(sender As Object, e As EventArgs) Handles Backup_Timer.Tick
        Try
            Dim PBC As Boolean = False
            While Microsoft.VisualBasic.DateAndTime.Timer >= ReminderTime
                ReminderTime = Microsoft.VisualBasic.DateAndTime.Timer + 180
                For Each Remind In Reminders
                    Dim NowTime = Format(Now, "yyyy-MM-dd HH-mm-ss")
                    If Not NowTime >= Remind.ReminderTime Then Continue For
                    Dim StrdTime = Remind.ReminderTime
                    If Format(Now, "yyyy-MM-dd HH-mm-ss") >= Format(Next_Reminder_Time_DtTmPkr.Value, "yyyy-MM-dd HH-mm-ss") Then
                        Me.ActiveControl = Available_Sticky_Notes_DGV
                        For Each Sticky In Available_Sticky_Notes_DGV.Rows
                            If Sticky.cells("Sticky_Note_Name").value = Remind.StickyName Then
                                If Language_Btn.Text = "ع" Then
                                    Msg = "The Sticky [" & Remind.StickyLabel & "] Have Reminder Time Now " & vbNewLine & "Reminder Time " & Remind.ReminderTime & vbNewLine & "Now Time... Do You Want To View It Now?" & NowTime
                                Else
                                    Msg = "اللاصقة [" & Remind.StickyLabel & "] لها وقت تنبية الآن " & vbNewLine & "وقت التنبية " & Remind.ReminderTime & vbNewLine & "الوقت الآن... هل تريد عرضها الآن؟" & NowTime
                                End If
                                If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                                    If Me.WindowState = FormWindowState.Minimized Then
                                        Me.WindowState = FormWindowState.Normal
                                    End If
                                Else
                                    Exit Sub
                                End If
                                Available_Sticky_Notes_DGV.CurrentCell = Available_Sticky_Notes_DGV(0, Sticky.index)
                                Available_Sticky_Notes_DGV.ClearSelection()
                                StickyNoteRTF = Nothing
                                Me.ActiveControl = Available_Sticky_Notes_DGV
                                Available_Sticky_Notes_DGV.Rows(Sticky.index).Selected = True
                                Exit For
                            End If
                        Next
                        Application.DoEvents()
                    End If
                Next
            End While
            'If PBC Then
            '    Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
            'End If

            If Periodically_Backup_Stickies_ChkBx.CheckState = CheckState.Unchecked Then
                Backup_Timer.Stop()
                Exit Sub
            End If
            If IsDate(Next_Backup_Time_TxtBx.Text) Then
                If Now >= CType(Next_Backup_Time_TxtBx.Text, DateTime) Then
                    If TackBackup() Then
                        Dim BT As DateTime = Now
                        BT = BT.AddDays(Days_NmrcUpDn.Value)
                        BT = BT.AddHours(Hours_NmrcUpDn.Value)
                        BT = BT.AddMinutes(Minutes_NmrcUpDn.Value)
                        Next_Backup_Time_TxtBx.Text = BT
                        Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
                    Else
                        Backup_Timer.Interval = 150000
                    End If
                End If
            Else
                Next_Backup_Time_TxtBx.Text = Now
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Days_NmrcUpDn_ValueChanged(sender As Object, e As EventArgs) Handles Days_NmrcUpDn.ValueChanged, Hours_NmrcUpDn.ValueChanged, Minutes_NmrcUpDn.ValueChanged
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            If ActiveControl.Name <> sender.name Then Exit Sub
            Dim BT As DateTime = Now
            BT = BT.AddDays(Days_NmrcUpDn.Value)
            BT = BT.AddHours(Hours_NmrcUpDn.Value)
            BT = BT.AddMinutes(Minutes_NmrcUpDn.Value)
            Next_Backup_Time_TxtBx.Text = BT
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Backup_Folder_Path_TxtBx_Click(sender As Object, e As EventArgs) Handles Backup_Folder_Path_Btn.Click
        Try
            Dim BFP As New FolderBrowserDialog
            If BFP.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Backup_Folder_Path_TxtBx.Text = BFP.SelectedPath
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Function CreatMenu()
        Try
            If Language_Btn.Text = "ع" Then
                mnuDisplayForm = New ToolStripMenuItem("Display Form")
            Else
                mnuDisplayForm = New ToolStripMenuItem("إظهار الشاشة")
            End If
            mnuDisplayForm.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                Restart = New ToolStripMenuItem("Restart Program")
            Else
                Restart = New ToolStripMenuItem("إعادة تشغيل البرنامج")
            End If
            Restart.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                MeAlwaysOnTop = New ToolStripMenuItem("Me Always On Top")
            Else
                MeAlwaysOnTop = New ToolStripMenuItem("أنا دائما في المقدمة")
            End If
            MeAlwaysOnTop.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            mnuSep1 = New ToolStripSeparator()
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                LockScreen = New ToolStripMenuItem("Lock Screen")
            Else
                LockScreen = New ToolStripMenuItem("إغلاق الشاشة")
            End If
            LockScreen.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                RestartWindows = New ToolStripMenuItem("Restart Windows")
            Else
                RestartWindows = New ToolStripMenuItem("إعادة تشغيل النوافذ")
            End If
            RestartWindows.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                ShutdownWindows = New ToolStripMenuItem("Shutdown Windows")
            Else
                ShutdownWindows = New ToolStripMenuItem("إغلاق النوافذ")
            End If
            ShutdownWindows.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                LogOffUser = New ToolStripMenuItem("Log Off User")
            Else
                LogOffUser = New ToolStripMenuItem("تسجيل خروج المستخدم")
            End If
            LogOffUser.BackgroundImage = My.Resources.Background4
            '---------------------------------------------------------------------------------
            mnuSep2 = New ToolStripSeparator()
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                mnuExit = New ToolStripMenuItem("Exit")
            Else
                mnuExit = New ToolStripMenuItem("خروج")
            End If
            mnuExit.BackgroundImage = My.Resources.Background4
            MainMenu = New ContextMenuStrip
            MainMenu.Items.AddRange(New ToolStripItem() {
                                    mnuDisplayForm,
                                    MeAlwaysOnTop,
                                    Restart,
                                    mnuSep1,
                                    LockScreen,
                                    LogOffUser,
                                    RestartWindows,
                                    ShutdownWindows,
                                    mnuSep2,
                                    mnuExit})
            Tray = New NotifyIcon
            Tray.Icon = My.Resources.unnamed
            Tray.ContextMenuStrip = MainMenu
            Tray.Text = "Profissional Sticky Note"
            Tray.Visible = True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

#Region "Tray Fuctions"
    Private Sub LockScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockScreen.Click
        LockMe()
    End Sub

    Private Sub ShutdownWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutdownWindows.Click
        Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
        System.Diagnostics.Process.Start("shutdown", "-s -t 00")
        'This will make the computer Shutdown
    End Sub

    Private Sub RestartWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartWindows.Click
        Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
        System.Diagnostics.Process.Start("shutdown", "-r -t 00")
        'This will make the computer Restart
    End Sub
    Private Declare Function ExitWindowsEx Lib "user32.dll" (ByVal uFlags As Long, ByVal dwReserved As Long) As Long
    Private Const EWX_FORCE = 4         'const for a forced logoff
    Private Sub LogOffUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOffUser.Click
        'System.Diagnostics.Process.Start("shutdown", "-l -t 00")
        Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
        ExitWindowsEx(EWX_FORCE, 4)
        'This will make the computer Log Off 
    End Sub

    Dim ApplicationRestart As Boolean
    Private Sub Restart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Restart.Click
        Try
            Dim arguments As String() = Environment.GetCommandLineArgs()
            For Each arg In arguments
                If arg = Application.StartupPath & "\" & Application.ProductName & ".exe" Then Continue For
                CreatSchedualTask(, 1)
                Application.Exit()
                Exit Sub
            Next


            Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
            ApplicationRestart = True
            Me.Close()
            mnuExit_Click(mnuExit, EventArgs.Empty)
            Application.DoEvents()
            Application.Restart()

        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub mnuDisplayForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDisplayForm.Click
        Try
            Me.Location = New Point(0, 0)
            Me.Size = New Size(356, 549)
            Me.WindowState = FormWindowState.Normal
            If Not Me.Visible Then
                Me.ShowDialog()
            End If
            MakeTopMost()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub MeAlwaysOnTop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MeAlwaysOnTop.Click
        Try
            If Me.TopMost = True Then
                Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked
                Me.TopMost = False
            Else
                Me_Always_On_Top_ChkBx.CheckState = CheckState.Checked
                MakeTopMost()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Public Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Try
            AlreadyAsked = True
            If Save_Setting_When_Exit And Not IsNothing(ActiveControl) Then
                Me.WindowState = FormWindowState.Normal
                Me.Save_Sticky_Form_Parameter_Setting_Btn_Click(Me.Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
            End If
            If StickyAmendmented() = DialogResult.Cancel Then Exit Sub
            If Not IsNothing(driver) Then
                driver.Quit()
            End If
            If ApplicationRestart Then Exit Sub
            UpdateAIOAssemblyVersion()

            Me.Dispose()
            End
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Tray_Click(sender As Object, e As EventArgs) Handles Tray.Click
        If Not MouseDounRight Then
            myHotKeyPressed(Tray, EventArgs.Empty)
        End If
    End Sub
    Dim MouseDounRight As Boolean
    Private Sub Tray_MouseDown(sender As Object, e As MouseEventArgs) Handles Tray.MouseDown
        Try
            MouseDounRight = False
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.WindowState = FormWindowState.Normal
                If Not Me.Visible Then
                    Me.ShowDialog()
                End If
            Else
                MouseDounRight = True
            End If
        Catch ex As Exception
        Finally
            MakeTopMost()
        End Try
    End Sub

#End Region

    Private Sub Sticky_Password_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Sticky_Password_TxtBx.TextChanged
        Try
            If Sticky_Password_TxtBx.TextLength = 0 Or
               String.IsNullOrEmpty(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Password").Value) Then Exit Sub
            If Decrypt_Function(Available_Sticky_Notes_DGV.CurrentRow.Cells("Sticky_Password").Value) = Sticky_Password_TxtBx.Text Then
                Dim arg = New DataGridViewRowEventArgs(Available_Sticky_Notes_DGV.CurrentRow)

                Available_Sticky_Notes_DGV_SelectionChanged(Available_Sticky_Notes_DGV, arg)
                StickyNoteRTF = Sticky_Note_TxtBx.Rtf
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub SaveToolStripButton_MouseDown(sender As Object, e As MouseEventArgs) Handles SaveToolStripButton.MouseDown
        Me.ActiveControl = ToolStrip1
    End Sub
    Private Sub Sticky_Applicable_To_Rename_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Sticky_Applicable_To_Rename_ChkBx.CheckedChanged

    End Sub
    Public OldStickyName As String = Nothing

    Private Sub Sticky_Applicable_To_Rename_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Sticky_Applicable_To_Rename_ChkBx.CheckStateChanged
        OldStickyName = Nothing
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            If ActiveControl.Name <> sender.name Then Exit Sub
            If Not String.IsNullOrEmpty(NewSticky) Then
                Me.ActiveControl = Sticky_Note_TxtBx
                Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked
                Exit Sub
            End If
            If Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Sub
            End If
            If OpenExternalMode Then
                OldStickyName = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
            Else
                OldStickyName = DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Sticky_Note_No_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles Sticky_Note_No_CmbBx.Validating
        Try
            If Sticky_Note_No_CmbBx.SelectedIndex <> -1 Then
                If File.Exists(DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key & "\" & DirectCast(Sticky_Note_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) Then
                    Exit Sub
                End If
            End If
            If (Sticky_Note_No_CmbBx.SelectedIndex = -1 And
                Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Unchecked And
                 IsNothing(NewSticky) And
                 Sticky_Note_No_CmbBx.Text.Length > 0) Or
                 Sticky_Note_No_CmbBx.Text.Contains(":") Then
                If Language_Btn.Text = "ع" Then
                    Msg = "This Sticky Is Not Found In Stored Files Or Its Label May Contains The Character (:)... Kindly Try Again Or If You Want To Change The Sticky Kinldy Put Mark On (Sticky Applicable To Rename) Box Before Amendment!!! Or Sticky Notes Not Loaded Yet Or Maybe This File Was Opened From Outside The Sticky Notes Program And There Was No Mark On The Object - (Remember Opened External Files)"
                Else
                    Msg = "هذه اللاصقة غير موجودة فى ملفات اللاصقات المحفوظة أو ربما يحمل العنوان الحرف (:)... يرجى إعادة المحاولة أو إن كنت ترغب فى تغيير عنوان اللاصقة فمن فضلك ضع علامة فى مربع (لاصقة قابلة لتعديل الإسم) قبل التعديل أو لم يتم تحميل اللاصقات بعد أو ربما تم فتح هذا الملف من خارج برنامج الملاحظات الاصقة ولم تكن هناك علام على العنصر ـ(تذكر الملفات المفتوحة خارجيا)ـ"
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False)
                Sticky_Note_No_CmbBx.Text = Nothing
                If Not IsNothing(Available_Sticky_Notes_DGV.CurrentRow) Then
                    Dim arg = New DataGridViewRowEventArgs(Available_Sticky_Notes_DGV.CurrentRow)
                    Me.ActiveControl = Available_Sticky_Notes_DGV
                    Available_Sticky_Notes_DGV_SelectionChanged(Available_Sticky_Notes_DGV, arg)
                End If
                e.Cancel = True
            ElseIf OpenExternalMode And NewSticky = "OutsideStickyNote" And
                    Sticky_Note_No_CmbBx.FindStringExact(Sticky_Note_No_CmbBx.Text) = -1 Then
                Sticky_Applicable_To_Rename_ChkBx.CheckState = CheckState.Checked
                OldStickyName = StickyNoteFolderPath & "\OutsideStickyNote\" & Sticky_Note_No_CmbBx.Text
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub WhatsApp_Btn_Click(sender As Object, e As EventArgs) Handles WhatsApp_Btn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Exit_TlStrpBtn.PerformClick()
            Dim regkey As Microsoft.Win32.RegistryKey
            Dim regpath As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths"
            regkey = My.Computer.Registry.LocalMachine.OpenSubKey(regpath)
            Dim subkeys() As String = regkey.GetSubKeyNames
            For Each subk As String In subkeys
                Select Case LCase(subk)
                    Case LCase("Firefox.exe")
                        Dim WACls As New WatsAppCls("Firefox")
                        WACls.TestMethod1()
                        Exit Sub
                    Case LCase("chrome.exe")
                        Dim WACls As New WatsAppCls("Chrome")
                        If IsNothing(driver) Then Continue For
                        WACls.TestMethod1()
                        Exit Sub
                End Select
            Next
            If Language_Btn.Text = "ع" Then
                Msg = "I Didn't Find Any Compatible Browser With me To Send Message With WhatsApp... Kindly Install One Of The Following Applications!!!"
            Else
                Msg = "لم يتم العثور على أى متصفح متوافق مع ارسال رسائل الواتس من خلالى... ارجو تثبيت احد البرامج التالية على جهازك"
            End If
            ShowMsg(Msg & vbNewLine & "Chrome.exe or Firefox.exe" & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Sticky_Note_TxtBx.LinkClicked
    '    Process.Start("http://www.google.com")
    '    ShowMsg("Press here!")
    'End Sub
    Private Sub Sticky_Note_TxtBx_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles Sticky_Note_TxtBx.LinkClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            If Language_Btn.Text = "ع" Then
                Msg = "Are You Sure Thes Link Will Be Executed?"
            Else
                Msg = "هل انت متأكد... سيتم تنفيذ هذا الارتباط؟"
            End If
            If ShowMsg(Msg & vbNewLine & e.LinkText, "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                Exit Sub
            End If
            If Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                Me.WindowState = FormWindowState.Minimized
            ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Indeterminate Then
                Sticky_Note_TxtBx.Focus()
                Compress_Me_TlStrpBtn.PerformClick()
            End If
            System.Diagnostics.Process.Start(e.LinkText)
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & e.LinkText & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Dim RTFBackup
    Dim FindArray(0) As String
    Dim Input_Form As New Form
    Dim FindText_Lbl As New Label
    Dim ReplaceBy_Lbl As New Label
    Dim FindAll_Btn As New Button
    Dim Find_Text_TxtBx As New ComboBox
    Dim FindText_CmbBx As New ComboBox
    Dim ReplaceBy_TxtBx As New TextBox
    Dim FindIn_CmbBx As New ComboBox
    Dim FindIn_Lbl As New Label
    Dim FindNext_Btn As New Button
    Dim ReplaceNext_Btn As New Button
    Dim ReplaceAll_Btn As New Button
    Dim Exit_Btn As New Button
    Private Sub Find_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Find_TlStrpBtn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Input_Form.Visible Then
                Exit_Btn.PerformClick()
                Exit Sub
            End If
            Dim SelectedText = Sticky_Note_TxtBx.SelectedText
            If IsNothing(RTFBackup) Then
                RTFBackup = New RichTextBox
                RTFBackup.Rtf = Sticky_Note_TxtBx.Rtf
                RTFBackup.SelectionColor = Sticky_Note_TxtBx.ForeColor
                RTFBackup.SelectionBackColor = Sticky_Note_TxtBx.BackColor
                RTFBackup.Refresh()
            End If
            Sticky_Note_TxtBx.Text = Nothing
            Application.DoEvents()
            Sticky_Note_TxtBx.Rtf = RTFBackup.Rtf
            Application.DoEvents()
            Input_Form = New Form
            Input_Form.Icon = Me.Icon
            Input_Form.BackColor = Me.BackColor
            Input_Form.ForeColor = Me.ForeColor
            Input_Form.MaximizeBox = False
            Input_Form.MinimizeBox = False
            Input_Form.FormBorderStyle = FormBorderStyle.FixedSingle
            Input_Form.Cursor = System.Windows.Forms.Cursors.Hand
            If Language_Btn.Text = "ع" Then
                Input_Form.RightToLeftLayout = False
                Input_Form.RightToLeft = RightToLeft.No
            Else
                Input_Form.RightToLeftLayout = True
                Input_Form.RightToLeft = RightToLeft.Yes
            End If

            FindText_Lbl = New Label
            ReplaceBy_Lbl = New Label
            FindText_CmbBx = New ComboBox
            ReplaceBy_TxtBx = New TextBox
            FindNext_Btn = New Button
            FindAll_Btn = New Button
            ReplaceNext_Btn = New Button
            ReplaceAll_Btn = New Button
            Exit_Btn = New Button

            Input_Form.Size = New Size(400, 225)

            FindNext_Btn.Name = "FindNext_Btn"
            FindNext_Btn.Size = New Size(97, 21)
            FindNext_Btn.FlatStyle = FlatStyle.Popup
            FindNext_Btn.Cursor = System.Windows.Forms.Cursors.Hand

            FindAll_Btn.Name = "FindNext_Btn"
            FindAll_Btn.FlatStyle = FlatStyle.Popup
            FindAll_Btn.Size = New Size(97, 21)
            FindAll_Btn.Cursor = System.Windows.Forms.Cursors.Hand

            ReplaceNext_Btn.Name = "ReplaceNext_Btn"
            ReplaceNext_Btn.FlatStyle = FlatStyle.Popup
            ReplaceNext_Btn.Size = New Size(97, 21)
            ReplaceNext_Btn.Cursor = System.Windows.Forms.Cursors.Hand

            ReplaceAll_Btn.Name = "ReplaceAll_Btn"
            ReplaceAll_Btn.FlatStyle = FlatStyle.Popup
            ReplaceAll_Btn.Size = New Size(97, 21)
            ReplaceAll_Btn.Cursor = System.Windows.Forms.Cursors.Hand

            Exit_Btn.Name = "Exit_Btn"
            Exit_Btn.FlatStyle = FlatStyle.Popup
            Exit_Btn.Size = New Size(97, 21)
            Exit_Btn.Cursor = System.Windows.Forms.Cursors.Hand

            FindText_Lbl.Size = New Size(97, 20)
            FindText_Lbl.TextAlign = ContentAlignment.MiddleCenter
            FindText_Lbl.BorderStyle = BorderStyle.FixedSingle

            ReplaceBy_Lbl.Size = New Size(97, 20)
            ReplaceBy_Lbl.TextAlign = ContentAlignment.MiddleCenter
            ReplaceBy_Lbl.BorderStyle = BorderStyle.FixedSingle

            FindText_CmbBx.Size = New Size(275, 20)
            FindText_CmbBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            'FindText_CmbBx.Multiline = True
            'FindText_CmbBx.ScrollBars = ScrollBars.Vertical

            ReplaceBy_TxtBx.Size = New Size(275, 20)
            ReplaceBy_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            ReplaceBy_TxtBx.Multiline = True
            ReplaceBy_TxtBx.ScrollBars = ScrollBars.Vertical

            FindIn_Lbl = New Label
            FindIn_Lbl.Size = New Size(97, 20)
            FindIn_Lbl.TextAlign = ContentAlignment.MiddleCenter
            FindIn_Lbl.BorderStyle = BorderStyle.FixedSingle

            FindIn_CmbBx = New ComboBox
            FindIn_CmbBx.Size = New Size(150, 20)
            FindIn_CmbBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            FindIn_CmbBx.FlatStyle = FlatStyle.Standard


            If Language_Btn.Text = "ع" Then
                Input_Form.Text = "Search Form"
                FindIn_Lbl.Text = "Search Object"
                FindText_Lbl.Text = "Text To Find"
                ReplaceBy_Lbl.Text = "Replace By"
                FindNext_Btn.Text = "Find Next"
                FindAll_Btn.Text = "Find All"
                ReplaceNext_Btn.Text = "Replace Next"
                ReplaceAll_Btn.Text = "Replace All"
                Exit_Btn.Text = "Exit"
            Else
                Input_Form.Text = "شاشة البحث"
                FindIn_Lbl.Text = "عنصر البحث"
                FindText_Lbl.Text = "كلمة البحث"
                ReplaceBy_Lbl.Text = "كلمة الإستبدال"
                FindNext_Btn.Text = "إبحث التالى"
                FindAll_Btn.Text = "إبحث الكل"
                ReplaceNext_Btn.Text = "إستبدل التالى"
                ReplaceAll_Btn.Text = "إستبدل الكل"
                Exit_Btn.Text = "خروج"
            End If

            'Column 1
            FindIn_Lbl.Location = New Point(5, 3)
            FindText_Lbl.Location = New Point(5, FindIn_Lbl.Top + FindText_Lbl.Height + 1)
            ReplaceBy_Lbl.Location = New Point(5, FindText_Lbl.Top + FindNext_Btn.Height + 1)
            FindNext_Btn.Location = New Point(5, ReplaceBy_Lbl.Top + FindNext_Btn.Height + 1)
            FindAll_Btn.Location = New Point(5, FindNext_Btn.Top + FindAll_Btn.Height + 1)
            ReplaceNext_Btn.Location = New Point(5, FindAll_Btn.Top + ReplaceNext_Btn.Height + 1)
            ReplaceAll_Btn.Location = New Point(5, ReplaceNext_Btn.Top + ReplaceAll_Btn.Height + 1)
            Exit_Btn.Location = New Point(5, ReplaceAll_Btn.Top + Exit_Btn.Height + 1)
            'Column 2
            FindIn_CmbBx.Location = New Point(103, 3)
            FindText_CmbBx.Location = New Point(103, FindIn_CmbBx.Top + FindText_CmbBx.Height + 1)
            ReplaceBy_TxtBx.Location = New Point(103, FindText_CmbBx.Top + FindText_CmbBx.Height + 1)

            Input_Form.Controls.Add(FindIn_CmbBx)
            Input_Form.Controls.Add(FindIn_Lbl)
            Input_Form.Controls.Add(FindText_Lbl)
            Input_Form.Controls.Add(FindText_CmbBx)
            Input_Form.Controls.Add(ReplaceBy_Lbl)
            Input_Form.Controls.Add(ReplaceBy_TxtBx)
            Input_Form.Controls.Add(FindNext_Btn)
            Input_Form.Controls.Add(FindAll_Btn)
            Input_Form.Controls.Add(ReplaceNext_Btn)
            Input_Form.Controls.Add(ReplaceAll_Btn)
            Input_Form.Controls.Add(Exit_Btn)
            Input_Form.Refresh()
            ReplaceAll_Btn.Enabled = False
            ReplaceNext_Btn.Enabled = False


            FindIn_CmbBx.ValueMember = "Key"
            FindIn_CmbBx.DisplayMember = "Value"
            If Language_Btn.Text = "ع" Then
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Body", "Find In Sticky Body"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Stickies_Bodies", "Find In Stickies Bodies"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Names", "Find In Sticky Names"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Labels", "Find In Sticky Labels"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Shortcuts", "Find In Shortcuts"))
                FindIn_CmbBx.Text = "Find In Sticky Body"
            Else
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Body", "البحث فى اللاصقة"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Stickies_Bodies", "البحث فى محتوى اللاصقات"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Names", "البحث فى أسماء اللاصقات"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Labels", "البحث فى عناوين اللاصقات"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Sticky_Shortcuts", "البحث فى الإختصارات"))
                FindIn_CmbBx.Text = "البحث فى اللاصقة"
            End If

            AddHandler Input_Form.Load, AddressOf Input_Form_Form_Load
            AddHandler Input_Form.FormClosing, AddressOf Input_Form_FormClosing
            AddHandler FindIn_CmbBx.SelectedIndexChanged, AddressOf FindIn_CmbBx_SelectedIndexChanged
            AddHandler FindAll_Btn.Click, AddressOf FindAll_Btn_Click
            AddHandler FindNext_Btn.Click, AddressOf FindNext_Btn_Click
            AddHandler ReplaceNext_Btn.Click, AddressOf ReplaceNext_Btn_Click
            AddHandler ReplaceAll_Btn.Click, AddressOf ReplaceAll_Btn_Click
            AddHandler Exit_Btn.Click, AddressOf Exit_Btn_Click
            AddHandler ReplaceBy_TxtBx.TextChanged, AddressOf ReplaceBy_TxtBx_TextChanged
            AddHandler FindText_CmbBx.LostFocus, AddressOf FindText_CmbBx_LostFocus
            AddHandler_Control_Move(Input_Form)
            If Find_Text_TxtBx.Items.Count > 0 Then
                For Each item In Find_Text_TxtBx.Items
                    FindText_CmbBx.Items.Add(item)
                Next
            End If
            Input_Form.Show(Me)
            FindText_CmbBx.Text = SelectedText
            FindText_CmbBx.Focus()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FindIn_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs)
        Select Case DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            Case "Find_In_Sticky_Names", "Find_In_Sticky_Labels", "Find_In_Stickies_Bodies"
                Setting_TbCntrl.SelectTab(Setting_TbCntrl.TabPages("Sticky_Notes_TbPg"))
            Case "Find_In_Sticky_Shortcuts"
                Setting_TbCntrl.SelectTab(Setting_TbCntrl.TabPages("Shortcuts_TbPg"))
        End Select
    End Sub
    Private Sub ReplaceBy_TxtBx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ReplaceBy_TxtBx.TextLength > 0 Then
            ReplaceAll_Btn.Enabled = True
            ReplaceNext_Btn.Enabled = True
        Else
            ReplaceAll_Btn.Enabled = False
            ReplaceNext_Btn.Enabled = False
        End If
    End Sub
    Private Sub Input_Form_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Input_Form.Location = New System.Drawing.Point(((Width / 2) + Me.Left) - (Input_Form.Width / 2), ((Height / 2) + Me.Top) - (Input_Form.Height / 2))
    End Sub
    Private Sub Input_Form_FormClosing(sender As Object, e As FormClosingEventArgs)
        Input_Form.Dispose()
    End Sub
    Private Sub ReplaceAll_Btn_Click(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If TextLengthZero() Then Exit Sub
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key <> "Find_In_Sticky_Body" Then
SelectedItemNNull:
                If Language_Btn.Text = "E" Then
                    Msg = "غير متاح مع  " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                Else
                    Msg = "Unevailable With " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                End If
                ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            Replace_TxtBx(FindText_CmbBx.Text, 0)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FindAll_Btn_Click(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If TextLengthZero() Then Exit Sub
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key <> "Find_In_Sticky_Body" Then
                If Language_Btn.Text = "E" Then
                    Msg = "غير متاح مع  " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                Else
                    Msg = "Unevailable With " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                End If
                ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            FindInSticky_Note_TxtBx(FindText_CmbBx.Text, 0)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function TextLengthZero() As Boolean
        If IsNothing(FindIn_CmbBx.SelectedItem) Then
            If Language_Btn.Text = "E" Then
                Msg = "يجب أولا تحديد عنصر البحث... حاول مرة اخرى"
            Else
                Msg = "Select Search Object First... Try Again"
            End If
            ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
            Return True
        End If
        If FindText_CmbBx.Text.Length = 0 Then
            If Language_Btn.Text = "E" Then
                Msg = "أدخل كلمة البحث أولا"
            Else
                Msg = "Enter The Search Word First"
            End If
            ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Return True
        End If
    End Function
    Private Sub FindNext_Btn_Click(sender As Object, e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If TextLengthZero() Then Exit Sub
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Labels" Then
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    Dim xx As String = LCase(Sticky.cells("Sticky_Note_Label").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Sticky.index <= Val(FindNext_Btn.Tag) Then Continue For
                        Sticky_Note_No_CmbBx.Focus()
                        Sticky_Note_No_CmbBx.Text = Sticky.cells("Sticky_Note_Label").value
                        FindNext_Btn.Tag = Sticky.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Stickies_Bodies" Then
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    Dim xx As String = LCase(Sticky.cells("Sticky_Note").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Sticky.index <= Val(FindNext_Btn.Tag) Then Continue For
                        Sticky_Note_No_CmbBx.Focus()
                        Sticky_Note_No_CmbBx.Text = Sticky.cells("Sticky_Note_Label").value
                        FindNext_Btn.Tag = Sticky.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Names" Then
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    Dim xx As String = LCase(Sticky.cells("Sticky_Note_Name").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Sticky.index <= Val(FindNext_Btn.Tag) Then Continue For
                        Sticky_Note_No_CmbBx.Focus()
                        Sticky_Note_No_CmbBx.Text = Sticky.cells("Sticky_Note_Label").value
                        FindNext_Btn.Tag = Sticky.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Body" Then
                FindNext_Btn.Tag = FindInSticky_Note_TxtBx(FindText_CmbBx.Text, Val(FindNext_Btn.Tag), 1)
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Shortcuts" Then
                listView1.SelectedItems.Clear()
                For Each Shortcut In listView1.Items
                    Dim xx As String = LCase(Shortcut.text)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And
                            Shortcut.index <= Val(FindNext_Btn.Tag) Then Continue For
                        listView1.Items(Shortcut.index).selected = True
                        FindNext_Btn.Tag = Shortcut.index
                        listView1.EnsureVisible(Shortcut.index)
                        listView1.Focus()
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ReplaceNext_Btn_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor
        If TextLengthZero() Then Exit Sub
        Try
            If IsNothing(FindIn_CmbBx.SelectedItem) Then
                If Language_Btn.Text = "E" Then
                    Msg = "يجب أولا تحديد عنصر البحث... حاول مرة اخرى"
                Else
                    Msg = "Select Search Object First... Try Again"
                End If
                ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Labels" Then
                Exit Sub
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    Dim xx As String = LCase(Sticky.cells("Sticky_Note_Label").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Sticky.index <= Val(FindNext_Btn.Tag) Then Continue For
                        Sticky_Note_No_CmbBx.Focus()
                        Sticky_Note_No_CmbBx.Text = Sticky.cells("Sticky_Note_Label").value
                        FindNext_Btn.Tag = Sticky.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Stickies_Bodies" Then
                Exit Sub
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    Dim xx As String = LCase(Sticky.cells("Sticky_Note").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Sticky.index <= Val(FindNext_Btn.Tag) Then Continue For
                        Sticky_Note_No_CmbBx.Focus()
                        Sticky_Note_No_CmbBx.Text = Sticky.cells("Sticky_Note_Label").value
                        FindNext_Btn.Tag = Sticky.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Names" Then
                Exit Sub
                For Each Sticky In Available_Sticky_Notes_DGV.Rows
                    Dim xx As String = LCase(Sticky.cells("Sticky_Note_Name").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Sticky.index <= Val(FindNext_Btn.Tag) Then Continue For
                        Sticky_Note_No_CmbBx.Focus()
                        Sticky_Note_No_CmbBx.Text = Sticky.cells("Sticky_Note_Label").value
                        FindNext_Btn.Tag = Sticky.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Body" Then
                FindNext_Btn.Tag = Replace_TxtBx(FindText_CmbBx.Text, Val(FindNext_Btn.Tag), 1)
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Sticky_Shortcuts" Then
                Exit Sub
                listView1.SelectedItems.Clear()
                For Each Shortcut In listView1.Items
                    Dim xx As String = LCase(Shortcut.text)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And
                            Shortcut.index <= Val(FindNext_Btn.Tag) Then Continue For
                        listView1.Items(Shortcut.index).selected = True
                        FindNext_Btn.Tag = Shortcut.index
                        listView1.EnsureVisible(Shortcut.index)
                        listView1.Focus()
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs)
        If FindText_CmbBx.Items.Count > 0 Then
            Find_Text_TxtBx.Items.Clear()
            For Each Item In FindText_CmbBx.Items
                Find_Text_TxtBx.Items.Add(Item)
            Next
        End If
        Input_Form.Close()
    End Sub

    Private Sub Enter_Password_To_Pass_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Enter_Password_To_Pass_ChkBx.CheckedChanged

    End Sub

    Private Sub Enter_Password_To_Pass_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Enter_Password_To_Pass_ChkBx.CheckStateChanged
        If Enter_Password_To_Pass_ChkBx.CheckState = CheckState.Checked Then
            Main_Password_TxtBx.ReadOnly = False
        ElseIf Enter_Password_To_Pass_ChkBx.CheckState = CheckState.Unchecked Then
            Main_Password_TxtBx.ReadOnly = True
            Complex_Password_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub GetCurrentPosition()
        Dim Position As Integer = Me.Sticky_Note_TxtBx.SelectionStart
        Dim Line As Integer = Me.Sticky_Note_TxtBx.GetLineFromCharIndex(Position) + 1
        Dim Col As Integer = Position - Me.Sticky_Note_TxtBx.GetFirstCharIndexOfCurrentLine
        If MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text.Contains("Current Position [") Then
            For Each Lin As String In Split(MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text, vbNewLine)
                If Lin.Contains("Current Position") Then
                    MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = Replace(MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text, Lin, "Current Position [Line = " & Line & " Column = " & Col & "]")
                    Exit For
                End If
            Next
        Else
            MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text & vbNewLine & "Current Position [Line = " & Line & " Column = " & Col & "]"
        End If
    End Sub
    Private Sub Sticky_Note_TxtBx_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Sticky_Note_TxtBx.KeyPress
        GetCurrentPosition()
    End Sub
    Private Sub Sticky_Note_TxtBx_KeyUp(sender As Object, e As KeyEventArgs) Handles Sticky_Note_TxtBx.KeyUp
        GetCurrentPosition()
    End Sub
    Dim SelectionStart
    Private Sub Sticky_Note_TxtBx_MouseDown(sender As Object, e As MouseEventArgs) Handles Sticky_Note_TxtBx.MouseDown
        GetCurrentPosition()
        If e.Button = MouseButtons.Right Then Exit Sub
        SelectionStart = Sticky_Note_TxtBx.SelectionStart
        ClearSelection()
    End Sub
    Private Function ClearSelection()
        Try
            If IsNothing(FindArray) Then Exit Function
            For Each SelectedText In FindArray.ToList
                If IsNothing(SelectedText) Then Continue For
                Dim ST = Split(SelectedText, "=")
                Sticky_Note_TxtBx.SelectionStart = Replace(Replace(ST(1), "Length", ""), " ", "")
                Sticky_Note_TxtBx.SelectionLength = Replace(ST(2), " ", "")
                Sticky_Note_TxtBx.SelectionBackColor = Sticky_Note_TxtBx.BackColor
                Sticky_Note_TxtBx.SelectionColor = Sticky_Note_TxtBx.ForeColor
            Next
            Application.DoEvents()
            Sticky_Note_TxtBx.SelectionStart = Val(SelectionStart)
            Dim valueArray(0) As String
            FindArray = valueArray
        Catch ex As Exception
        End Try
    End Function
    Private Function RefreshFormLocationSieText()
        If Not IsNothing(ActiveControl) Then
            If ActiveControl.Name = ToolStrip1.Name Then
                Exit Function
            End If
        Else
            Exit Function
        End If
        Sticky_Form_Location_TxtBx.Text = Me.Location.ToString
        If Me.WindowState = FormWindowState.Minimized Or
            Me.WindowState = FormWindowState.Maximized Or
            Me.Bounds.Equals(Me.RestoreBounds) Or
            Me.Height <= 39 Or
            CompressMeTlStrpBtnClicked Then Exit Function
        Sticky_Form_Size_TxtBx.Text = Me.Size.ToString
    End Function
    Dim SizeChangedInProgress As Boolean
    Dim PreviousSize As New Size
    Public Sub Sticky_Note_Form_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If SizeChangedInProgress Then Exit Sub
            Dim StoredSize As New Size(Val(Replace(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(0), "{Width=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Height=", "")))

            SizeChangedInProgress = True
            If Me.Bounds.Equals(Me.RestoreBounds) And Not PreviousWindowState.Equals(Nothing) And
                            PreviousWindowState.Equals(FormWindowState.Maximized) Then
                Application.DoEvents()
                If PreviousWindowState = Me.WindowState Then Exit Sub
                Me.Location = New Point(Val(Replace(Split(Sticky_Form_Location_TxtBx.Text, ",").ToList.Item(0), "{X=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Location_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Y=", "")))
                Me.Size = New Size(Val(Replace(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(0), "{Width=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Height=", "")))
                Me.Size = New Size(Val(Replace(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(0), "{Width=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Height=", "")))
            ElseIf (Not Me.Bounds.Equals(Me.RestoreBounds) And Not PreviousWindowState.Equals(Nothing) And
                            Me.WindowState = FormWindowState.Maximized And PreviousSize <> StoredSize) Then
                Me.WindowState = FormWindowState.Normal
                Me.Location = New Point(Val(Replace(Split(Sticky_Form_Location_TxtBx.Text, ",").ToList.Item(0), "{X=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Location_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Y=", "")))
                Me.Size = New Size(Val(Replace(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(0), "{Width=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Height=", "")))
                Me.Size = New Size(Val(Replace(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(0), "{Width=", "")), Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Height=", "")))
                Application.DoEvents()
            End If
            SizeChangedInProgress = False
            PreviousWindowState = Me.WindowState
            PreviousSize = Me.Size
            Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked
            RefreshFormLocationSieText()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Sticky_Note_Form_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        RefreshFormLocationSieText()
    End Sub

    Private listView1 As ListView
    Private imageList1 As ImageList
    Private Sub FileList_DragDrop(sender As Object, e As DragEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim lvi As ListViewItem
                Dim item As ListViewItem
                Dim Extension
                Dim iconForFile As Icon
                Dim IsFolder As Boolean

                Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                For Each File In files
                    IsFolder = (System.IO.File.GetAttributes(File) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                    If IsFolder Then
                        Extension = "Folder"
                    Else
                        Extension = Path.GetExtension(Path.GetFileName(File))
                        Extension = Path.GetFileName(File)
                    End If
                    iconForFile = SystemIcons.WinLogo
                    item = New ListViewItem(Path.GetFileName(File), 1)
                    If listView1.Items.Count > 0 Then
                        If Not IsNothing(listView1.FindItemWithText(item.Text, False, 0, False)) Then Continue For
                    End If
                    If Not (imageList1.Images.ContainsKey(Extension)) Then
                        Using IH As New IconHelper
                            If IsFolder Then
                                imageList1.Images.Add(Extension, IH.GetIconFrom(File, IconSize.Jumbo, 0))
                            Else
                                imageList1.Images.Add(Extension, IH.GetIconFrom(File, IconSize.Jumbo, 1))
                            End If
                        End Using
                    End If
                    item.ImageKey = Extension
                    'If listView1.Items.Contains(item) Then Continue For
                    lvi = listView1.Items.Add(item)
                    lvi.Tag = File
                    'End If
                Next
                Application.DoEvents()
                listView1.Sort()
                SaveList()
                LoadList(StickyNoteFolderPath & "\FilesShortcuts.txt")
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub listView1_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            If Language_Btn.Text = "E" Then
                Msg = "سيتم إلغاء الإختصار(ات)... هل انت متأكد"
            Else
                Msg = "This File(s) Will Be Deleyed... Are You Sure?"
            End If
            If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                Exit Sub
            End If
            For Each i As ListViewItem In listView1.SelectedItems
                listView1.Items.Remove(i)
            Next
            SaveList()
        ElseIf e.KeyCode = Keys.Enter Then
            If Not IsNothing(listView1.SelectedItems(0).Tag) Then
                Process.Start(listView1.SelectedItems(0).Tag)
                Me.WindowState = FormWindowState.Minimized
            End If
        End If
    End Sub

    Private Sub FileList_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Private Sub CreatListView()
        Try
            If IsNothing(listView1) Then
                listView1 = New ListView()
                imageList1 = New ImageList()
                listView1.Location = New Point(37, 12)
                listView1.Size = New Size(161, 242)
                listView1.LabelEdit = True
                listView1.AllowDrop = True
                listView1.Sorting = SortOrder.Ascending
                listView1.AutoArrange = True
                Shortcuts_TbPg.Controls.Add(Me.listView1)
                Me.listView1.Dock = System.Windows.Forms.DockStyle.Fill
                AddHandler listView1.DoubleClick, AddressOf listView1_DoubleClick
                AddHandler listView1.Click, AddressOf listView1_Click
                AddHandler listView1.DragDrop, AddressOf FileList_DragDrop
                AddHandler listView1.DragEnter, AddressOf FileList_DragEnter
                AddHandler listView1.KeyDown, AddressOf listView1_KeyDown
                AddHandler listView1.MouseEnter, AddressOf listView1_MouseEnter
                AddHandler listView1.MouseMove, AddressOf listView1_MouseMove
                AddHandler listView1.MouseDown, AddressOf listView1_MouseDown

                listView1.ContextMenuStrip = New ContextMenuStrip()
                Dim listView1_lstvw As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    listView1_lstvw.Text = "Reload My Shortcuts"
                Else
                    listView1_lstvw.Text = "إعادة تحميل إختصاراتى"
                End If
                listView1_lstvw.Tag = listView1.Name
                listView1_lstvw.BackgroundImage = My.Resources.Background4
                listView1_lstvw.Image = My.Resources.reload
                listView1_lstvw.Name = "ReloadMyShortcuts" & listView1.Name
                listView1.ContextMenuStrip.Items.Add(listView1_lstvw)
                listView1_lstvw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler listView1_lstvw.Click, AddressOf listView1_lstvw_Click
                '-------------------------------------------
                Dim listView1_Save As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    listView1_Save.Text = "Save My Shortcuts"
                Else
                    listView1_Save.Text = "حفظ إختصاراتى"
                End If
                listView1_Save.Tag = listView1.Name
                listView1_Save.BackgroundImage = My.Resources.Background4
                listView1_Save.Image = My.Resources.save
                listView1_Save.Name = "ReloadMyShortcuts" & listView1.Name
                listView1.ContextMenuStrip.Items.Add(listView1_Save)
                listView1_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler listView1_Save.Click, AddressOf listView1_Save_Click
                '-------------------------------------------
                Dim listView1_SelectAll As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    listView1_SelectAll.Text = "Select All"
                Else
                    listView1_SelectAll.Text = "إختار الجميع"
                End If
                listView1_SelectAll.Tag = listView1.Name
                listView1_SelectAll.BackgroundImage = My.Resources.Background4
                listView1_SelectAll.Image = My.Resources.SelectAll
                listView1_SelectAll.Name = "ReloadMyShortcuts" & listView1.Name
                listView1.ContextMenuStrip.Items.Add(listView1_SelectAll)
                listView1_SelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler listView1_SelectAll.Click, AddressOf listView1_SelectAll_Click
                '-------------------------------------------
                Dim listView1_Edit_Label As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    listView1_Edit_Label.Text = "Edit Label"
                Else
                    listView1_Edit_Label.Text = "تعديل عنوان"
                End If
                listView1_Edit_Label.Tag = listView1.Name
                listView1_Edit_Label.BackgroundImage = My.Resources.Background4
                listView1_Edit_Label.Image = My.Resources.SelectAll
                listView1_Edit_Label.Name = "EditLabel" & listView1.Name
                listView1.ContextMenuStrip.Items.Add(listView1_Edit_Label)
                listView1_Edit_Label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler listView1_Edit_Label.Click, AddressOf listView1_Edit_Label_Click
                '-------------------------------------------
                'RunProgramAsAdministrator
                Dim listView1_RunAsAdministrator As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    listView1_RunAsAdministrator.Text = "Run As Administrator"
                Else
                    listView1_RunAsAdministrator.Text = "Run As Administrator"
                End If
                listView1_RunAsAdministrator.Tag = listView1.Name
                listView1_RunAsAdministrator.BackgroundImage = My.Resources.Background4
                listView1_RunAsAdministrator.Image = My.Resources.SelectAll
                listView1_RunAsAdministrator.Name = "listView1_RunAsAdministrator" & listView1.Name
                listView1.ContextMenuStrip.Items.Add(listView1_RunAsAdministrator)
                listView1_RunAsAdministrator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler listView1_RunAsAdministrator.Click, AddressOf listView1_RunAsAdministrator_Click
                '-------------------------------------------
            Else
                listView1.Items.Clear()
                imageList1.Images.Clear()
            End If
            'imageList1.ImageSize = New Size(32, 32)
            listView1.Font = New Font(New FontFamily("Times New Roman"), 12, FontStyle.Regular)

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub listView1_lstvw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                LoadList(StickyNoteFolderPath & "\FilesShortcuts.txt")
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub listView1_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                Me.ActiveControl = Save_Sticky_Form_Parameter_Setting_Btn
                Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub listView1_Edit_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "ع" Then
                    listView1.ContextMenuStrip.Items(3).Text = "Change Label"
                Else
                    listView1.ContextMenuStrip.Items(3).Text = "تغيير عنوان"
                End If
                Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Unchecked
            Else
                If Language_Btn.Text = "ع" Then
                    listView1.ContextMenuStrip.Items(3).Text = "One Click"
                Else
                    listView1.ContextMenuStrip.Items(3).Text = "نقرة واحدة"
                End If
                Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Checked
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub listView1_RunAsAdministrator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim process As System.Diagnostics.Process = Nothing
            Dim processStartInfo As System.Diagnostics.ProcessStartInfo
            processStartInfo = New System.Diagnostics.ProcessStartInfo()
            processStartInfo.FileName = listView1.SelectedItems(0).Tag
            processStartInfo.Verb = "runas"
            processStartInfo.Arguments = ""
            processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
            processStartInfo.UseShellExecute = True
            process = System.Diagnostics.Process.Start(processStartInfo)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub listView1_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                For i = 0 To listView1.Items.Count - 1
                    listView1.Items(i).Selected = True
                Next i
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub listView1_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            MouseDounRight = True
        Else
            MouseDounRight = False
        End If
    End Sub
    Private Sub listView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If MouseDounRight Then Exit Sub
            If Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Checked Then Exit Sub
            If Not IsNothing(listView1.SelectedItems(0).Tag) Then
                Process.Start(listView1.SelectedItems(0).Tag)
                If Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                    Me.WindowState = FormWindowState.Minimized
                ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Indeterminate Then
                    Sticky_Note_TxtBx.Focus()
                    Compress_Me_TlStrpBtn.PerformClick()
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub RunProgramAsAdministrator(Optional ByVal ProgramName As String = Nothing)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim process As System.Diagnostics.Process = Nothing
            Dim processStartInfo As System.Diagnostics.ProcessStartInfo
            processStartInfo = New System.Diagnostics.ProcessStartInfo()
            If String.IsNullOrEmpty(ProgramName) Then
                processStartInfo.FileName = ProgramName
            ElseIf String.IsNullOrEmpty(listView1.SelectedItems(0).Tag) Then
                processStartInfo.FileName = listView1.SelectedItems(0).Tag
            Else Exit Sub
            End If
            processStartInfo.Verb = "runas"
            processStartInfo.Arguments = ""
            processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
            processStartInfo.UseShellExecute = True
            process = System.Diagnostics.Process.Start(processStartInfo)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub listView1_DoubleClick(sender As Object, e As EventArgs)
        Try
            If Not IsNothing(listView1.SelectedItems(0).Tag) Then
                Process.Start(listView1.SelectedItems(0).Tag)
                Me.WindowState = FormWindowState.Minimized
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub


    Private Sub SaveList()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim filePath = StickyNoteFolderPath & "\FilesShortcuts.txt"
            Dim TextToWrite As String
            If File.Exists(filePath) Then
                'File.Delete(filePath)
                My.Computer.FileSystem.DeleteFile(filePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            End If
            For Each file In listView1.Items
                TextToWrite &= file.tag & "$" & file.text & ","
            Next
            If String.IsNullOrEmpty(TextToWrite) Then Exit Sub
            My.Computer.FileSystem.WriteAllText(filePath, Microsoft.VisualBasic.Left(TextToWrite, TextToWrite.Length - 1), 0, System.Text.Encoding.UTF8)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LoadList(ByVal FilePath As String)
        DialogResult = DialogResult.None
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not File.Exists(FilePath) Then Exit Sub
            listView1.LargeImageList = imageList1
            listView1.SmallImageList = imageList1
            listView1.View = View.LargeIcon
            imageList1.ImageSize = New Size(48, 48)
            Dim MyIconSize = IconSize.ExtraLarge

            imageList1.ColorDepth = ColorDepth.Depth32Bit
            Dim lvi As ListViewItem
            Dim item As ListViewItem
            Dim Extension
            'Dim iconForFile As Icon
            Dim IsFolder As Boolean
            imageList1.Images.Clear()
            listView1.Items.Clear()
            Dim FilePaths() = Split(My.Computer.FileSystem.ReadAllText(FilePath), ",")
            For Each File In FilePaths
                Dim Fil() = Split(File, "$")
                If Fil(1) = "Adobe Photoshop CS" Then
                    Dim x = 1
                End If
                Try
                    IsFolder = (System.IO.File.GetAttributes(Fil(0)) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                Catch ex As Exception
                    If DialogResult = DialogResult.Cancel Then Continue For
                    If Language_Btn.Text = "ع" Then
                        Msg = "You Can Reload All The Shortcuts Again By Right Clicking On The List"
                    Else
                        Msg = "يمكنك إعادة تحميل جميع الاختصارات مرة أخرى عن طريق النقر فوق القائمة بزر الماوس الأيمن"
                    End If
                    DialogResult = ShowMsg(ex.Message & Msg & vbNewLine & Fil(0), "InfoSysMe (Stick Note)", MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    Continue For
                End Try
                If IsFolder Then
                    Extension = "Folder"
                Else
                    Extension = Path.GetFileName(Fil(0))
                End If
                'iconForFile = SystemIcons.WinLogo
                item = New ListViewItem(Path.GetFileName(Fil(0)), 1)
                If listView1.Items.Count > 0 Then
                    If Not IsNothing(listView1.FindItemWithText(item.Text, False, 0, False)) Then Continue For
                End If
                If Not (imageList1.Images.ContainsKey(Extension)) Then
                    Dim Img As New PictureBox
                    Using IH As New IconHelper
                        If IsFolder Then
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
                            Img.Image = IH.GetIconFrom(Fil(0), MyIconSize, 0).ToBitmap
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
                            imageList1.Images.Add(Extension, Img.Image)
                        Else
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
                            Img.Image = IH.GetIconFrom(Fil(0), MyIconSize, 1).ToBitmap
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
                            imageList1.Images.Add(Extension, Img.Image)
                        End If
                    End Using
                End If
                item.ImageKey = Extension
                lvi = listView1.Items.Add(item)
                lvi.Text = Fil(1)
                lvi.Tag = Fil(0)
            Next

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub listView1_KeyPressed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub listView1_MouseMove(sender As Object, e As MouseEventArgs)
        Dim currentItem As ListViewItem = listView1.GetItemAt(e.X, e.Y)
        If currentItem IsNot Nothing Then
            currentItem.Focused = True
        End If
    End Sub

    Private Sub listView1_MouseEnter(sender As Object, e As EventArgs)
        imageList1.ColorDepth = ColorDepth.Depth32Bit
        If IsNothing(ActiveControl) Then Exit Sub
        'If Not Me.Focused Then Exit Sub
        If Me.ActiveControl.Name <> sender.name Then
            Me.ActiveControl = sender
            sender.Focus()
        End If
    End Sub

    Private Sub Sticky_Note_TxtBx_MouseMove(sender As Object, e As MouseEventArgs) Handles Sticky_Note_TxtBx.MouseMove
        imageList1.ColorDepth = ColorDepth.Depth32Bit
        If Me.ActiveControl.Name <> sender.name Then
            Me.ActiveControl = sender
            sender.Focus()
        End If
    End Sub

    Private Sub Set_Control_To_Fill_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Set_Control_To_Fill_ChkBx.CheckedChanged

    End Sub

    Private Sub Set_Control_To_Fill_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Set_Control_To_Fill_ChkBx.CheckStateChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Set_Control_To_Fill_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "ع" Then
                    Set_Control_To_Fill_ChkBx.Text = "Set Fill To Sticky"
                Else
                    Set_Control_To_Fill_ChkBx.Text = "إختيار التعبئة للاصقة"
                End If
                Setting_TbCntrl.Dock = DockStyle.Bottom
                Setting_TbCntrl.SendToBack()
                Sticky_Note_Pnl.Dock = DockStyle.Fill
            ElseIf Set_Control_To_Fill_ChkBx.CheckState = CheckState.Unchecked Then
                If Language_Btn.Text = "ع" Then
                    Set_Control_To_Fill_ChkBx.Text = "Set Fill To Control Tabs"
                Else
                    Set_Control_To_Fill_ChkBx.Text = "إختيار التعبئة لصفحات التبويب"
                End If
                If Setting_TbCntrl.Visible Then
                    Sticky_Note_Pnl.Dock = DockStyle.Top
                    Setting_TbCntrl.Dock = DockStyle.Fill
                End If
                Setting_TbCntrl.BringToFront()
            End If
            MsgBox_SttsStrp.SendToBack()
            ToolStrip1.SendToBack()
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Dim sbTaRtf As New System.Text.StringBuilder
        sbTaRtf.Append("{\rtf1")
        For index As Integer = 0 To 5
            sbTaRtf.Append("\trowd")
            sbTaRtf.Append("\cellx1000") 'set that cell width to 1000
            sbTaRtf.Append("\cellx2000")
            sbTaRtf.Append("\cellx3000")
            sbTaRtf.Append("\intbl \cell \row")
        Next
        sbTaRtf.Append("\pard")
        sbTaRtf.Append("}")
        Sticky_Note_TxtBx.Rtf = sbTaRtf.ToString()
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        Try
            Dim wc As New WebClient
            'AddHandler wc.DownloadFileCompleted, AddressOf wc_DownloadCompleted
            'AddHandler wc.DownloadProgressChanged, AddressOf wc_ProgressChanged
            Dim savefile As New SaveFileDialog
            Dim fileurl As String = "https://drive.google.com/file/d/1TaieC8RXPIafkvN5AqM5q5na4o8mSs48/view?usp=sharing"
            Dim filelocation As String = Application.StartupPath & "\Infosys.rar"
            If System.IO.File.Exists(filelocation) Then My.Computer.FileSystem.DeleteFile(filelocation)
            wc.DownloadFileAsync(New Uri(fileurl), filelocation)
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & "Download Failed", "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Sticky_Note_Form_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub ToolStripSplitButton2_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton2.ButtonClick
        If Not ChechRchTxtBx() Then Exit Sub
        Sticky_Note_TxtBx.SelectionFont = FontDlg.Font
    End Sub


    Private Sub Next_Reminder_Time_DtTmPkr_ValueChanged(sender As Object, e As EventArgs) Handles Next_Reminder_Time_DtTmPkr.ValueChanged
        If ActiveControl.Name = sender.name Then
            Sticky_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub
    Private Sub Reminder_Every_Days_NmrcUpDn_ValueChanged(sender As Object, e As EventArgs) Handles Reminder_Every_Days_NmrcUpDn.ValueChanged,
            Reminder_Every_Hours_NmrcUpDn.ValueChanged,
            Reminder_Every_Minutes_NmrcUpDn.ValueChanged
        If ActiveControl.Name = sender.name Then
            Sticky_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub Sticky_Have_Reminder_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Sticky_Have_Reminder_ChkBx.CheckedChanged

    End Sub

    Private Sub Sticky_Have_Reminder_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Sticky_Have_Reminder_ChkBx.CheckStateChanged
        Try
            If ActiveControl.Name = sender.name Then
                Me.ActiveControl = Preview_Btn
                Preview_Btn.Focus()
                SaveToolStripButton_Click(SaveToolStripButton, EventArgs.Empty)
                Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & "Download Failed", "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Stop_Reminder_Alert_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Pending_Reminder_Alert_ChkBx.CheckedChanged

    End Sub

    Private Sub Stop_Reminder_Alert_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Pending_Reminder_Alert_ChkBx.CheckStateChanged
        If Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Checked Then
            SaveToolStripButton_Click(SaveToolStripButton, EventArgs.Empty)
            Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
            Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub Sticky_Note_Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked

    End Sub

    Private Sub Show_Form_Border_Style_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Form_Border_Style_ChkBx.CheckedChanged

    End Sub

    Private Sub Show_Form_Border_Style_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Show_Form_Border_Style_ChkBx.CheckStateChanged
        If Show_Form_Border_Style_ChkBx.CheckState = CheckState.Checked Then
            Me.FormBorderStyle = FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = FormBorderStyle.None
        End If
    End Sub

    Private Sub Enable_Maximize_Box_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Enable_Maximize_Box_ChkBx.CheckedChanged

    End Sub

    Private Sub Enable_Maximize_Box_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Enable_Maximize_Box_ChkBx.CheckStateChanged
        If Enable_Maximize_Box_ChkBx.CheckState = CheckState.Checked Then
            Me.MaximizeBox = True
        Else
            Me.MaximizeBox = True
        End If
    End Sub

    Private Sub Show_Sticky_Tab_Control_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Sticky_Tab_Control_ChkBx.CheckedChanged

    End Sub

    Private Sub Show_Sticky_Tab_Control_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Show_Sticky_Tab_Control_ChkBx.CheckStateChanged
        If Show_Sticky_Tab_Control_ChkBx.CheckState = CheckState.Unchecked Then
            Setting_TbCntrl.Visible = False
            Sticky_Note_Pnl.Dock = DockStyle.Fill
        Else
            Setting_TbCntrl.Visible = True
            Sticky_Note_Pnl.Dock = DockStyle.Top
        End If
    End Sub

    Private Sub Double_Click_To_Run_Shortcut_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Double_Click_To_Run_Shortcut_ChkBx.CheckedChanged

    End Sub

    Private Sub Double_Click_To_Run_Shortcut_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Double_Click_To_Run_Shortcut_ChkBx.CheckStateChanged

    End Sub

    Private Sub Warning_Before_Save_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Warning_Before_Save_ChkBx.CheckedChanged

    End Sub

    Private Sub Warning_Before_Save_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Warning_Before_Save_ChkBx.CheckStateChanged

    End Sub

    Private Sub Warning_Before_Delete_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Warning_Before_Delete_ChkBx.CheckedChanged

    End Sub

    Private Sub Warning_Before_Delete_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Warning_Before_Delete_ChkBx.CheckStateChanged

    End Sub

    Private Sub Sticky_Word_Wrap_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Sticky_Word_Wrap_ChkBx.CheckedChanged

    End Sub

    Private Sub Sticky_Word_Wrap_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Sticky_Word_Wrap_ChkBx.CheckStateChanged
        If Sticky_Word_Wrap_ChkBx.CheckState = CheckState.Checked Then
            Sticky_Note_TxtBx.WordWrap = True
        Else
            Sticky_Note_TxtBx.WordWrap = False
        End If
    End Sub

    Private Sub Font_Strikeout_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Font_Strikeout_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Dim SelectionFont As Font = Sticky_Note_TxtBx.SelectionFont
        Dim gdiCharSet As Byte
        Dim gdiVerticalFont As Boolean
        If Sticky_Note_TxtBx.SelectionFont.Strikeout Then
            Sticky_Note_TxtBx.SelectionFont = New Font(SelectionFont.Name, SelectionFont.Size, Drawing.FontStyle.Regular, SelectionFont.Unit, gdiCharSet, gdiVerticalFont)
        Else
            Sticky_Note_TxtBx.SelectionFont = New Font(SelectionFont.Name, SelectionFont.Size, Drawing.FontStyle.Strikeout, SelectionFont.Unit, gdiCharSet, gdiVerticalFont)
        End If
    End Sub

    Private Sub FindText_CmbBx_LostFocus(sender As Object, e As EventArgs)
        If Not FindText_CmbBx.Items.Contains(FindText_CmbBx.Text) And
            FindText_CmbBx.Text.Length > 0 Then
            FindText_CmbBx.Items.Add(FindText_CmbBx.Text)
        End If
    End Sub
    Dim CompressMeTlStrpBtnClicked As Boolean
    Private Sub Compress_Me_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Compress_Me_TlStrpBtn.Click
        Try
            MakeTopMost()
            CompressMeTlStrpBtnClicked = True
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked
            Else
                Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked
            End If
            Sticky_Note_TxtBx.Focus()
            Me.ActiveControl = Sticky_Note_TxtBx
        Catch ex As Exception
        Finally
            CompressMeTlStrpBtnClicked = False
        End Try
    End Sub

    Private Sub Me_Is_Compressed_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Me_Is_Compressed_ChkBx.CheckedChanged

    End Sub

    Public Sub Me_Is_Compressed_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Me_Is_Compressed_ChkBx.CheckStateChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Sticky_Form_Size_TxtBx.TextLength = 0 Then Exit Sub
            If Me.WindowState = FormWindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Location = New Point(Val(Replace(Split(Sticky_Form_Location_TxtBx.Text, ",").
                                            ToList.Item(0), "{X=", "")), Val(Replace(Microsoft.VisualBasic.Right(
                                            Split(Sticky_Form_Location_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Y=", "")))
            Dim FormWidth = Val(Replace(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(0), "{Width=", ""))
            Dim FormHeight = Val(Replace(Microsoft.VisualBasic.Right(Split(Sticky_Form_Size_TxtBx.Text, ",").ToList.Item(1), Sticky_Form_Size_TxtBx.TextLength - 1), "Height=", ""))
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked Then
                For MyWidth = Me.Width To FormWidth Step 10
                    Me.Width = MyWidth
                    If Me.Height <= FormHeight Then
                        Me.Height += 10
                    End If
                    Application.DoEvents()
                Next
                If Me.Height <= FormHeight Then
                    For MyHeight = Me.Height To FormHeight Step 10
                        Me.Height = MyHeight
                        Application.DoEvents()
                    Next
                    Me.Height = FormHeight
                End If
            Else
                If Me.Height > 39 Then
                    For MyHeight = Me.Height To 39 Step -10
                        Me.Height = MyHeight
                        Application.DoEvents()
                    Next
                    Me.Height = 39
                End If
            End If
        Catch ex As Exception
            'ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.WaitCursor
        End Try
    End Sub

    Private Sub Sticky_Note_Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            MakeTopMost()
            'ShowMsg(MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text & vbNewLine & "Me Activated At " & Now.ToString, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MBOs, False, 0)
        Catch ex As Exception
        End Try
    End Sub
    Dim PreviousWindowState As FormWindowState

    Private Sub Minimize_After_Running_My_Shortcut_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Minimize_After_Running_My_Shortcut_ChkBx.CheckedChanged

    End Sub

    Private Sub Minimize_After_Running_My_Shortcut_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Minimize_After_Running_My_Shortcut_ChkBx.CheckStateChanged
        If Language_Btn.Text = "E" Then
            If Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "تصغير بعد تشغيل إختصاراتى"
            ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Unchecked Then
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "مرئى بعد تشغيل إختصاراتى"
            ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Indeterminate Then
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "مضغوط بعد تشغيل إختصاراتى"
            End If
        Else
            If Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "Minimize After Running My Shortcut"
            ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Unchecked Then
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "Visible After Running My Shortcut"
            ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Indeterminate Then
                Minimize_After_Running_My_Shortcut_ChkBx.Text = "Compresse After Running My Shortcut"
            End If
        End If
    End Sub

    Public Function ParseCommandLineArgs(Optional ByVal NewArg As String = Nothing, Optional ByVal CheckIfAlredyExist As Boolean = False) As String
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.Height <= 39 Then
                Me.WindowState = FormWindowState.Maximized
                'Sticky_Note_Form_SizeChanged(Me, EventArgs.Empty)
            End If
            Dim MyDialogResult As DialogResult
            MyDialogResult = StickyAmendmented()
            If MyDialogResult = DialogResult.Cancel Then Exit Function
            If Not String.IsNullOrEmpty(NewArg) Then
                ExtenalFilePath = Path.GetDirectoryName(NewArg)
                ExternalFileName = Path.GetFileName(NewArg)
                Me.ActiveControl = Sticky_Note_No_CmbBx
                If Not CheckIfAlredyExist Then
                    Sticky_Note_No_CmbBx.Focus()
                    Sticky_Note_No_CmbBx.Text = Nothing
                End If
                Dim Index = Sticky_Note_No_CmbBx.FindStringExact(ExternalFileName)
                Application.DoEvents()
                If Index <> -1 Then
                    If Not CheckIfAlredyExist Then
                        Sticky_Note_No_CmbBx.Text = ExternalFileName
                    End If
                    If DirectCast(Sticky_Note_No_CmbBx.Items(Index), KeyValuePair(Of String, String)).Key = ExtenalFilePath Then
                        Exit Function
                    Else
                        If Language_Btn.Text = "ع" Then
                            Msg = "This File Already Exist In Sticky Note Data But In Different Path... Do You Want To Use It?"
                        Else
                            Msg = "هذا الملف موجود بالفعل في بيانات الملاحظات اللاصقة ولكن على مسار مختلف ... هل تريد استخدامه؟"
                        End If
                        Msg &= vbNewLine & "ExtenalFilePath = (" & DirectCast(Sticky_Note_No_CmbBx.Items(Index), KeyValuePair(Of String, String)).Key & ")"
                        If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
                            Exit Function
                        End If
                    End If
                End If
                Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(ExtenalFilePath, ExternalFileName))
                Application.DoEvents()

                Sticky_Note_No_CmbBx.Text = ExternalFileName
                Correct_Sticky_Note_TxtBx_Font_Color()
                RememberOpenedExternalFiles()
                If Index = -1 And CheckIfAlredyExist Then
                    Return False
                ElseIf Index <> -1 And CheckIfAlredyExist Then
                    Return True
                End If
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub RememberOpenedExternalFiles()
        If Remember_Opened_External_Files_ChkBx.CheckState = CheckState.Checked Then
            Dim OpenedExternalFiles = Application.StartupPath & "\OpenedExternalFiles.txt"
            Dim WriteContents As String = String.Empty
            If File.Exists(OpenedExternalFiles) Then
                WriteContents = My.Computer.FileSystem.ReadAllText(OpenedExternalFiles)
            End If
            If Not WriteContents.Contains(ExtenalFilePath & "\" & ExternalFileName) Then
                WriteContents &= vbNewLine & ExtenalFilePath & "\" & ExternalFileName
            End If
            My.Computer.FileSystem.WriteAllText(OpenedExternalFiles, WriteContents, 0, System.Text.Encoding.UTF8)
        End If
    End Sub
    Dim OpenExternalMode, NormalSticky ' As Boolean
    Private Function OpenExternalFile() As Boolean
        Try
            IgnoreReadFile = True
            Dim arguments As String() = Environment.GetCommandLineArgs()
            For Each arg In arguments
                If arg = Application.StartupPath & "\" & Application.ProductName & ".exe" Then Continue For
                Me.ActiveControl = Sticky_Note_No_CmbBx
                Sticky_Note_No_CmbBx.Focus()
                Sticky_Note_No_CmbBx.Text = Nothing
                ExtenalFilePath = Path.GetDirectoryName(arg)
                ExternalFileName = Path.GetFileName(arg)
                Sticky_Note_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(ExtenalFilePath, ExternalFileName))
                Application.DoEvents()
                OpenExternalMode = True
                Return True
            Next
        Catch ex As Exception
        Finally
            IgnoreReadFile = False
        End Try
    End Function

    Private Sub Me_As_Default_Text_File_Editor_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Me_As_Default_Text_File_Editor_ChkBx.CheckedChanged

    End Sub

    Private Sub Me_As_Default_Text_File_Editor_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Me_As_Default_Text_File_Editor_ChkBx.CheckStateChanged
        Me.Cursor = Cursors.WaitCursor
        Try
            If ActiveControl.Name <> Me_As_Default_Text_File_Editor_ChkBx.Name Then
                If File.Exists(Application.StartupPath & "\SUDO.cmd") Then
                    If Me_As_Default_Text_File_Editor_ChkBx.CheckState = CheckState.Checked Then
                        Try
                            Dim Reg As Microsoft.Win32.RegistryKey
                            Dim GetReg As String = Nothing
                            Reg = My.Computer.Registry.ClassesRoot.OpenSubKey("txtfile\shell\open\command\", True)
                            If IsNothing(Reg) Then
                                Reg = My.Computer.Registry.ClassesRoot.CreateSubKey("txtfile\shell\open\command\")
                                Reg.SetValue("", Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                            Else
                                My.Computer.Registry.ClassesRoot.OpenSubKey("txtfile\shell\open\command\", True).SetValue("", Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                            End If
                            My.Computer.Registry.ClassesRoot.OpenSubKey("txtfile\DefaultIcon\", True).SetValue("", Application.ExecutablePath, Microsoft.Win32.RegistryValueKind.String)


                            Reg = My.Computer.Registry.ClassesRoot.OpenSubKey("rtffile\shell\open\command\", True)
                            If IsNothing(Reg) Then
                                Reg = My.Computer.Registry.ClassesRoot.CreateSubKey("rtffile\shell\open\command\")
                                Reg.SetValue("", Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                            Else
                                My.Computer.Registry.ClassesRoot.OpenSubKey("rtffile\shell\open\command\", True).SetValue("", Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                            End If
                            My.Computer.Registry.ClassesRoot.OpenSubKey("rtffile\DefaultIcon\", True).SetValue("", Application.ExecutablePath, Microsoft.Win32.RegistryValueKind.String)

                        Catch ex As Exception
                            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        End Try
                    Else
                        Try
                            Dim NotepadPath = Environment.SystemDirectory + "\notepad.exe"
                            My.Computer.Registry.ClassesRoot.OpenSubKey("txtfile\shell\open\command\", True).SetValue("", NotepadPath, Microsoft.Win32.RegistryValueKind.String)
                            My.Computer.Registry.ClassesRoot.OpenSubKey("txtfile\DefaultIcon\", True).SetValue("", NotepadPath, Microsoft.Win32.RegistryValueKind.String)

                            NotepadPath = Chr(34) & "%ProgramFiles%\Windows NT\Accessories\WORDPAD.EXE" & Chr(34) & " " & Chr(34) & "%1" & Chr(34)
                            My.Computer.Registry.ClassesRoot.OpenSubKey("rtffile\shell\open\command\", True).SetValue("", NotepadPath, Microsoft.Win32.RegistryValueKind.String)
                            My.Computer.Registry.ClassesRoot.OpenSubKey("rtffile\DefaultIcon\", True).SetValue("", NotepadPath, Microsoft.Win32.RegistryValueKind.String)

                        Catch ex As Exception
                            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        End Try
                    End If
                    My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\SUDO.cmd", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    Exit Sub
                End If
                Exit Sub
            Else
                Try
                    If Not File.Exists(Application.StartupPath & "\SUDO.cmd") Then
                        Dim SUDOCommandLine = "@echo Set objShell = CreateObject(" & Chr(34) & "Shell.Application" & Chr(34) & ") > %temp%\sudo.tmp.vbs
@echo args = Right(" & Chr(34) & "%*" & Chr(34) & ", (Len(" & Chr(34) & "%*" & Chr(34) & ") - Len(" & Chr(34) & "%1" & Chr(34) & "))) >> %temp%\sudo.tmp.vbs
@echo objShell.ShellExecute " & Chr(34) & "%1" & Chr(34) & ", args, " & Chr(34) & "" & Chr(34) & ", " & Chr(34) & "runas" & Chr(34) & ", 0 >> %temp%\sudo.tmp.vbs
@cscript %temp%\sudo.tmp.vbs"
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\SUDO.cmd", SUDOCommandLine, 0, System.Text.Encoding.Default)
                    End If

                    If File.Exists(Application.StartupPath & "\Sticky_Note.bat") Then
                        My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\Sticky_Note.bat", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    End If
                    Dim CommandLine = Application.StartupPath & "\sudo cmd /k " & Application.ExecutablePath '& " JAVA file " & vbNewLine
                    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Sticky_Note.bat", CommandLine, 0, System.Text.Encoding.Default)
                    If Not File.Exists(Application.StartupPath & "\Sticky_Note.vbs") Then
                        CommandLine = "Dim WinScriptHost
Set WinScriptHost = CreateObject(" & Chr(34) & "WScript.Shell" & Chr(34) & ")
WinScriptHost.Run " & Chr(34) & Application.StartupPath & "\Sticky_Note.bat" & Chr(34) & ", 0
Set WinScriptHost = Nothing"
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Sticky_Note.vbs", CommandLine, 0, System.Text.Encoding.Default)
                    End If
                    Using ts As New Microsoft.Win32.TaskScheduler.TaskService()
                        Dim td As TaskDefinition = ts.NewTask
                        td.RegistrationInfo.Description = "Run StickyNote As Administrator"
                        Dim wt As New TimeTrigger
                        wt.StartBoundary = Now.AddSeconds(5)
                        td.Triggers.Add(wt)
                        td.Actions.Add(New ExecAction(Application.StartupPath & "\Sticky_Note.vbs"))
                        ts.RootFolder.RegisterTaskDefinition("RunStickyNoteAsAdministrator", td)
                        wt.Enabled = True
                        Save_Sticky_Form_Parameter_Setting_Btn.PerformClick()
                        Application.DoEvents()
                        Application.Exit()
                    End Using
                Catch ex As Exception
                    ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                End Try
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Sticky_Note_TxtBx_MouseEnter(sender As Object, e As EventArgs) Handles Sticky_Note_TxtBx.MouseEnter
        'Me.Activate()
        'Me.Focus()
    End Sub

    Private Sub Sticky_Note_Category_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sticky_Note_Category_CmbBx.SelectedIndexChanged
        'If ActiveControl.Name = sender.name Then
        '    Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
        'End If
    End Sub
    Private Function GetNewCategoryNo() As Integer
        SaveDialogResultAnseredIsNo = False
        Dim FileCount = 0
        Dim Files() As String = System.IO.Directory.GetFiles(StickyNoteFolderPath)
        For Each file In Files
            Dim F = Path.GetFileNameWithoutExtension(file)
            Dim filename = Microsoft.VisualBasic.Left(F, Len("Category -(")) & Microsoft.VisualBasic.Right(Path.GetFileName(F), 2)
            If Path.GetExtension(file) = ".txt" And
                    filename = "Category -()-" Then
                FileCount += 1
            End If
        Next
        Dim CurrentFile = Application.StartupPath & "\Sticky_Notes_Files\Category -(" & FileCount & ")-.txt"
        If File.Exists(CurrentFile) Then
            FileCount = 0
        End If
        While File.Exists(CurrentFile)
            FileCount += 1
            CurrentFile = Application.StartupPath & "\Sticky_Notes_Files\Category -(" & FileCount & ")-.txt"
        End While
        Return FileCount
    End Function
    Private Function CheckPublicCategory()
        Dim CategoryName
        If Language_Btn.Text = "E" Then
            CategoryName = "فئة عامة"
        Else
            CategoryName = "Public Category"
        End If
        If Sticky_Note_Category_CmbBx.FindStringExact(CategoryName) = -1 Then
            Dim CFN = Application.StartupPath & "\Sticky_Notes_Files\Category -(" & GetNewCategoryNo() & ")-.txt"
            If Not File.Exists(CFN) Then
                My.Computer.FileSystem.WriteAllText(CFN, CategoryName, 0, System.Text.Encoding.UTF8)
                Sticky_Note_Category_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Path.GetFileName(CFN), CategoryName))
                Sticky_Note_Category_CmbBx.Text = CategoryName
            End If
        Else
            Sticky_Note_Category_CmbBx.Text = CategoryName
        End If
    End Function '
    Private Sub Sticky_Note_Category_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles Sticky_Note_Category_CmbBx.Validating
        Try
            Me.Cursor = Cursors.WaitCursor
            If OpenExternalMode Then Exit Sub
            Dim CFN = Application.StartupPath & "\Sticky_Notes_Files\Category -(" & GetNewCategoryNo() & ")-.txt"
            If Sticky_Note_Category_CmbBx.SelectedIndex = -1 And Sticky_Note_Category_CmbBx.Text.Length > 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "This File Category Is Not Exit... Do You Want To Open It?"
                Else
                    Msg = "هذه الفئة غير مسجلة من قبل... هل تريد فتح فئة جديدة بهذا الإسم؟"
                End If
                Msg &= vbNewLine & "(" & Sticky_Note_Category_CmbBx.Text & ")"
                If ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                    CheckPublicCategory()
                    e.Cancel = True
                    Exit Sub
                End If
                If Not File.Exists(CFN) Then
                    My.Computer.FileSystem.WriteAllText(CFN, Sticky_Note_Category_CmbBx.Text, 0, System.Text.Encoding.UTF8)
                    If Language_Btn.Text = "ع" Then
                        Msg = "Category Updated Successfully"
                    Else
                        Msg = "تم حفظ الفئة بنجاح"
                    End If
                    Msg &= vbNewLine & Sticky_Note_Category_CmbBx.Text
                    ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
                    Sticky_Note_Category_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Path.GetFileName(CFN), Sticky_Note_Category_CmbBx.Text))
                    Dim OldValue = Sticky_Note_Category_CmbBx.Text
                    Sticky_Note_Category_CmbBx.Text = Nothing
                    Sticky_Note_Category_CmbBx.Text = OldValue
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Sticky_Note_TxtBx_DragDrop(sender As Object, e As DragEventArgs) Handles Sticky_Note_TxtBx.DragDrop
        Try
            Me.Cursor = Cursors.WaitCursor
            If Sticky_Note_No_CmbBx.Text.Length = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Select Sticky First Or Create New One"
                Else
                    Msg = "من فضلك إختر لاصقة أولا أو إنشئ لاصقة جديدة"
                End If
                ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
                Exit Sub
            End If
            Dim IsFolder As Boolean
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            Dim Img As Image
            Dim ValidImage As Boolean
            For Each File In files
                IsFolder = (System.IO.File.GetAttributes(File) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                If IsFolder Then
                    Dim txtFilesArray As String() = Directory.GetFiles(File, "*.*", SearchOption.AllDirectories)
                    '              For Each foundFile As String In My.Computer.FileSystem.GetFiles(
                    'My.Computer.FileSystem.SpecialDirectories.MyDocuments)
                    For Each foundFile As String In txtFilesArray
                        Try
                            ValidImage = True
                            Img = Image.FromFile(foundFile)
                        Catch ex As Exception
                            'generatedExceptionName As OutOfMemoryException
                            ValidImage = False
                        End Try
                        If Not IsNothing(Img) And ValidImage Then
                            Clipboard.SetImage(Img)
                            Sticky_Note_TxtBx.Paste()
                            SendKeys.Send(Keys.Enter)
                            Continue For
                        End If
                        DirectCast(Me, Sticky_Note_Form).ParseCommandLineArgs(foundFile)
                    Next
                Else
                    Try
                        ValidImage = True
                        Img = Image.FromFile(File)
                    Catch ex As Exception
                        ValidImage = False
                    End Try
                    If Not IsNothing(Img) And ValidImage Then
                        Clipboard.SetImage(Img)
                        Sticky_Note_TxtBx.Paste()
                        SendKeys.Send(Keys.Enter)
                        Continue For
                    End If
                    DirectCast(Me, Sticky_Note_Form).ParseCommandLineArgs(File)
                End If
            Next

            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            e.Effect = Windows.Forms.DragDropEffects.None
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Read_ME_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Read_ME_TlStrpBtn.Click
        If Read_Me_Pnl.Visible Then
            Read_Me_Pnl.Visible = False
        Else
            Dim vol As UInteger = 0
            waveOutGetVolume(IntPtr.Zero, vol)
            Windows_Volume_TrckBr.Value = CInt((vol And &HFFFF) / (UShort.MaxValue / 50))
            Windows_Volume = Windows_Volume_TrckBr.Value
            Read_Me_Pnl.Visible = True
            Read_Me_Pnl.BringToFront()
        End If
        If Language_Btn.Text = "E" Then 'Arabic
            Arabic_Btn.PerformClick()
        Else
            English_Btn.PerformClick()
        End If
        If cmbInstalled.Items.Count > 0 Then
            cmbInstalled.SelectedIndex = 0
            Start_Btn.PerformClick()
        End If
    End Sub
    Private Sub Sticky_Note_TxtBx_DragEnter(sender As Object, e As DragEventArgs) Handles Sticky_Note_TxtBx.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
#Region "Read_ME"
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Private Const WM_APPCOMMAND As Integer = &H319

    <DllImport("user32.dll")>
    Public Shared Function SendMessageW(ByVal hWnd As IntPtr,
               ByVal Msg As Integer, ByVal wParam As IntPtr,
               ByVal lParam As IntPtr) As IntPtr
    End Function

    Private Sub English_Btn_Click(sender As Object, e As EventArgs) Handles English_Btn.Click
        ChangeLanguage(0)
        Language_Lbl.Text = "E"
        LoadSystemVoices()
    End Sub
    Private Sub Arabic_Btn_Click(sender As Object, e As EventArgs) Handles Arabic_Btn.Click
        ChangeLanguage(1)
        Language_Lbl.Text = "ع"
        LoadSystemVoices()
    End Sub
    Private Function LoadSystemVoices()
        cmbInstalled.Items.Clear()
        Dim objvoices As ReadOnlyCollection(Of InstalledVoice) = Speech.GetInstalledVoices(System.Windows.Forms.InputLanguage.CurrentInputLanguage.Culture)
        If objvoices.Count = 0 Then Exit Function
        Dim objvoiceInformation As VoiceInfo = objvoices(0).VoiceInfo
        For Each tmpvoice As InstalledVoice In objvoices
            objvoiceInformation = tmpvoice.VoiceInfo
            cmbInstalled.Items.Add(objvoiceInformation.Name.ToString)
        Next
    End Function
    Public m_arrayLanguages As New ArrayList
    Public Sub ChangeLanguage(ByVal indexLanguage As Integer)
        Try
            LoadLanguages()
            Dim entrySelected As LanguageEntry = CType(m_arrayLanguages(indexLanguage), LanguageEntry)
            InputLanguage.CurrentInputLanguage = entrySelected.Language
            Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub LoadLanguages()
        Try
            Dim langInput As InputLanguage
            Dim entryCombo As LanguageEntry
            For Each langInput In InputLanguage.InstalledInputLanguages
                entryCombo = New LanguageEntry(langInput.Culture.NativeName, langInput)
                m_arrayLanguages.Add(entryCombo)
            Next
        Finally
        End Try
    End Sub
    Public Speech As New SpeechSynthesizer

    Private Sub Volume_TrcBr_Scroll(sender As Object, e As EventArgs) Handles Volume_TrcBr.Scroll
        Speech.Volume = Volume_TrcBr.Value
    End Sub

    Private Sub Speaking_Rate_TrcBr_Scroll(sender As Object, e As EventArgs) Handles Speaking_Rate_TrcBr.Scroll
        Speech.Rate = Speaking_Rate_TrcBr.Value
    End Sub

    Public Sub Stop_Btn_Click(sender As Object, e As EventArgs) Handles Stop_Btn.Click
        If cmbInstalled.SelectedIndex = -1 And
            ActiveControl.Name = sender.name Then
            If Language_Btn.Text = "ع" Then
                Msg = "Kindly Select Suitable Voice To Use It"
            Else
                Msg = "يرجى اختيار الصوت المناسب لاستخدامه"
            End If
            ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
            Exit Sub
        End If
        If Speech.State = SynthesizerState.Ready Then Exit Sub
        While Speech.State <> SynthesizerState.Ready
            Speech.SpeakAsyncCancelAll()
            Application.DoEvents()
        End While
    End Sub
    Dim TextToread As New TextBox

    Public Sub Start_Btn_Click(sender As Object, e As EventArgs) Handles Start_Btn.Click
        If cmbInstalled.SelectedIndex = -1 Then
            If Language_Btn.Text = "ع" Then
                Msg = "Kindly Select Suitable Voice To Use It"
            Else
                Msg = "يرجى اختيار الصوت المناسب لاستخدامه"
            End If
            ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
            Exit Sub
        End If
        If Speech.State <> SynthesizerState.Ready And
            Speech.State <> SynthesizerState.Paused Then Exit Sub
        If Speech.State = SynthesizerState.Paused Then
            Speech.Resume()
            Exit Sub
        End If
        Speech.SetOutputToDefaultAudioDevice()
        If Sticky_Note_TxtBx.SelectionStart <> 0 Then
            TextToread.Text = Mid(Sticky_Note_TxtBx.Text, (Sticky_Note_TxtBx.SelectionStart + 1), Sticky_Note_TxtBx.TextLength - Sticky_Note_TxtBx.SelectionStart)
        Else
            TextToread.Text = Nothing
        End If
        Speech.SelectVoice(cmbInstalled.Text)
        If Not String.IsNullOrEmpty(Sticky_Note_TxtBx.SelectedText) Then
            Speech.SpeakAsync(Sticky_Note_TxtBx.SelectedText)
        ElseIf TextToread.TextLength > 0 Then
            Speech.SpeakAsync(TextToread.Text)
        Else
            Speech.SpeakAsync(Sticky_Note_TxtBx.Text)
        End If
    End Sub

    Private Sub Read_Me_Pnl_Paint(sender As Object, e As PaintEventArgs) Handles Read_Me_Pnl.Paint

    End Sub

    Public Sub Pause_Btn_Click(sender As Object, e As EventArgs) Handles Pause_Btn.Click
        If cmbInstalled.SelectedIndex = -1 Then
            If Language_Btn.Text = "ع" Then
                Msg = "Kindly Select Suitable Voice To Use It"
            Else
                Msg = "يرجى اختيار الصوت المناسب لاستخدامه"
            End If
            ShowMsg(Msg & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
            Exit Sub
        End If
        If Speech.State = SynthesizerState.Paused Then
            Speech.Resume()
            Exit Sub
        ElseIf Speech.State = SynthesizerState.Speaking Then
            Speech.Pause()
            Exit Sub
        End If
        If ActiveControl.Name <> sender.name Then
            Start_Btn.PerformClick()
        End If
    End Sub
    <DllImport("winmm.dll")> Private Shared Function waveOutGetVolume(ByVal hwo As IntPtr, ByRef pdwVolume As UInteger) As UInteger
    End Function
    <DllImport("winmm.dll")> Private Shared Function waveOutSetVolume(ByVal hwo As IntPtr, ByVal dwVolume As UInteger) As UInteger
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Read_Me_Pnl.Visible = False
        Stop_Btn_Click(Stop_Btn, EventArgs.Empty)
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles Windows_Volume_TrckBr.Scroll
        'Dim vol As UInteger = CUInt((UShort.MaxValue / 100) * Windows_Volume_TrckBr.Value)
        'waveOutSetVolume(IntPtr.Zero, CUInt((vol And &HFFFF) Or (vol << 16)))
        'Label1.Text = TrackBar1.Value.ToString & "%"
        If TextBox1.Text = "Up" Then
            SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
        Else
            SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_DOWN))
        End If
        Application.DoEvents()
    End Sub

    Private Sub Read_Me_Pnl_MouseDown(sender As Object, e As MouseEventArgs) Handles Read_Me_Pnl.MouseDown,
            Language_Lbl.MouseDown, Installed_Voices_Lbl.MouseDown, Speaking_Rate_Lbl.MouseDown, Volume_Lbl.MouseDown
        MoveForm(sender)
    End Sub
    Dim Windows_Volume As Integer
    Dim TextBox1 As New TextBox
    Private Sub Windows_Volume_TrckBr_ValueChanged(sender As Object, e As EventArgs) Handles Windows_Volume_TrckBr.ValueChanged
        If Windows_Volume_TrckBr.Value > Windows_Volume Then
            TextBox1.Text = "Up"
        ElseIf Windows_Volume_TrckBr.Value < Windows_Volume Then
            TextBox1.Text = "Down"
        End If
        Windows_Volume = Windows_Volume_TrckBr.Value
    End Sub

    Private Sub Sticky_Note_TxtBx_Validating(sender As Object, e As CancelEventArgs) Handles Sticky_Note_TxtBx.Validating
    End Sub

    Private Sub Remember_Opened_External_Files_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Remember_Opened_External_Files_ChkBx.CheckedChanged

    End Sub

    Private Sub Available_Sticky_Notes_DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_Sticky_Notes_DGV.CellClick
        If OpenExternalMode Then
            OpenExternalMode = False
            Dim arg = New DataGridViewRowEventArgs(Available_Sticky_Notes_DGV.CurrentRow)
            CallByName(Me, "Available_Sticky_Notes_DGV_SelectionChanged", CallType.Method, Available_Sticky_Notes_DGV, arg)
        End If
    End Sub

    Private Sub Remember_Opened_External_Files_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Remember_Opened_External_Files_ChkBx.CheckStateChanged
        If ActiveControl.Name <> sender.name Or Remember_Opened_External_Files_ChkBx.CheckState <> CheckState.Checked Then Exit Sub
        CreateOutsideStickyNoteCategory()
    End Sub
    Private Function CreateOutsideStickyNoteCategory() As Boolean
        Dim ItemName As String
        If Language_Btn.Text = "E" Then 'Arabic
            ItemName = "خارج الملاحظات اللاصقة"
        Else
            ItemName = "Outside Of Sticky Note"
        End If
        Dim Inx = Sticky_Note_Category_CmbBx.FindStringExact(ItemName)
        If Inx = -1 Then
            Sticky_Note_Category_CmbBx.Text = ItemName
            Sticky_Note_Category_CmbBx.Focus()
            Sticky_Note_TxtBx.Focus()
        Else
            Sticky_Note_Category_CmbBx.Text = ItemName
        End If
    End Function

    Private Sub Me_Always_On_Top_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Me_Always_On_Top_ChkBx.CheckedChanged

    End Sub

    Private Function CreateOutsideStickyNote() As Boolean
        Dim FolderName = StickyNoteFolderPath & "\OutsideStickyNote"
        If (Not System.IO.Directory.Exists(FolderName)) Then
            System.IO.Directory.CreateDirectory(FolderName)
        End If
    End Function

    Private Sub Me_Always_On_Top_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Me_Always_On_Top_ChkBx.CheckStateChanged
        Save_Sticky_Form_Parameter_Setting_Btn_Click(Save_Sticky_Form_Parameter_Setting_Btn, EventArgs.Empty)
        MakeTopMost()
    End Sub
#End Region

End Class
