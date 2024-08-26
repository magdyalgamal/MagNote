Imports System.IO
Imports System.Text
Imports unvell.ReoGrid.Actions
Imports unvell.ReoGrid
Imports unvell.ReoGrid.Data
Imports unvell.ReoGrid.IO
Imports PrayTimes
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Speech.Synthesis
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Security.Cryptography
Imports unvell.ReoGrid.DataFormat
Imports unvell.ReoGrid.Events
Imports Shell32
Imports System.Text.RegularExpressions
Imports System.Xml
Imports WMPLib
Imports System.Drawing.Printing
Imports System.Configuration
Imports System
Imports System.Drawing
Imports System.Diagnostics
Public Class MagNote_Form
    Inherits System.Windows.Forms.Form
    Private Shared WM_QUERYENDSESSION As Integer = &H11
    Private Shared WM_ENDSESSION As Integer = &H16
    Private Shared systemShutdown As Boolean = False
    Private Restricter As New FormBoundsRestricter(Me)
    Dim RC As ResizeableControl_Class
    Dim CurrentDayOfYear = Today.DayOfYear
    Private Function CreateShortCut(ByVal TargetName As String, ByVal ShortCutPath As String, ByVal ShortCutName As String) As Boolean
        Dim oShell As Object
        Dim oLink As Object
        'you don’t need to import anything in the project reference to create the Shell Object

        Try
            Dim ImagePath = ShortCutPath & "Iamge.ico"
            Try
                Dim iconURL = TargetName & "/favicon.ico"
                Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(iconURL)
                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Dim stream As System.IO.Stream = response.GetResponseStream()
                Dim favicon = Image.FromStream(stream)
                Dim PctrBx As New PictureBox
                PctrBx.Image = favicon
                If File.Exists(ImagePath) Then
                    File.Delete(ImagePath)
                End If
                PctrBx.Image.Save(ImagePath, Imaging.ImageFormat.Icon)
            Catch ex As Exception
                ImagePath = Nothing
            End Try
            oShell = CreateObject("WScript.Shell")
            oLink = oShell.CreateShortcut(ShortCutPath & ShortCutName & ".lnk")
            oLink.TargetPath = TargetName
            oLink.WindowStyle = 1
            oLink.WorkingDirectory = ShortCutPath '"c:\windows"
            If Not IsNothing(ImagePath) Then
                oLink.IconLocation = oShell.ExpandEnvironmentStrings(ImagePath) 'Windows Update Icon
            End If
            oLink.Save()
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try

    End Function


    Private Function RegisterMeWithDesktopRightClickMenu() As Boolean
        Try
            Dim Reg As Microsoft.Win32.RegistryKey
            Reg = My.Computer.Registry.ClassesRoot.OpenSubKey("Directory\Background\shell\MagNote")
            If IsNothing(Reg) Then
                Dim ProductName
                If Debugger.IsAttached Then
                    ProductName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\" & Application.ProductName & ".exe"
                Else
                    ProductName = Application.StartupPath & "\" & Application.ProductName & ".exe"
                End If
                Reg = My.Computer.Registry.ClassesRoot.CreateSubKey("Directory\Background\shell\MagNote")
                Reg.SetValue("Icon", ProductName)
                Reg = My.Computer.Registry.ClassesRoot.CreateSubKey("Directory\Background\shell\MagNote\Command")
                Reg.SetValue("", ProductName)
            End If
        Catch ex As Exception
            If ex.Message.Contains("Access to the registry key") And ex.Message.Contains(" is denied.") Or
                ex.Message.Contains("Requested registry access is not allowed") Then
                RestartWithElevatedPrivileges(1)
            Else
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Function
    Private Sub MagNote_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If EncrypAppConfig() Then
                EncryptionKey = ConfigurationManager.AppSettings("EncryptionKey")
                FTP_Login(0).FTP_Address = ConfigurationManager.AppSettings("FTP_Address")
                FTP_Login(0).FTP_UserName = ConfigurationManager.AppSettings("FTP_UserName")
                FTP_Login(0).FTP_Password = ConfigurationManager.AppSettings("FTP_Password")
                FTP_Address_TxtBx.Text = ConfigurationManager.AppSettings("FTP_Address")
                FTP_User_Name_TxtBx.Text = ConfigurationManager.AppSettings("FTP_UserName")
                FTP_Password_TxtBx.Text = ConfigurationManager.AppSettings("FTP_Password")
                Encryption_Key_TxtBx.Text = ConfigurationManager.AppSettings("EncryptionKey")
                Dim FileName = Application.StartupPath & "\MailCredentials.inf"
                If File.Exists(FileName) Then
                    Dim TextToRead() = Split(Decrypt_Function(My.Computer.FileSystem.ReadAllText(FileName, System.Text.Encoding.UTF8)), vbCrLf)
                    For Each Line In TextToRead
                        Select Case True
                            Case Line.Contains("SMTP_Mail_User_Name")
                                SMTP_Mail_User_Name_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_Mail_User_Password")
                                SMTP_Mail_User_Password_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_Host")
                                SMTP_Host_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_Port")
                                SMTP_Port_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_DeliveryMethod")
                                SMTP_Delivery_Method_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_EnableSsl")
                                SMTP_EnableSsl_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_Mail_From")
                                SMTP_Mail_From_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_Use_Default_Credentials")
                                SMTP_Use_Default_Credentials_TxtBx.Text = Split(Line, ",").ToList(1)
                            Case Line.Contains("SMTP_Use_SmtpServer_Or_SmtpClient")
                                SMTP_Use_SmtpServer_Or_SmtpClient_TxtBx.Text = Split(Line, ",").ToList(1)
                        End Select
                    Next
                End If
                If IsNothing(EncryptionKey) Then
                    EncryptionKey = String.Empty
                End If
            End If
            Me.Opacity = 0
            Application_Initializing_Form.TopMost = True
            If Not RunAsExternal(UseArgFile) Then
                If Debugger.IsAttached Then
                    If ShowMsg("Do You Want To Show Application Initializing Form?",, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,,,, 3) = DialogResult.Yes Then
                        Application_Initializing_Form.Show()
                    End If
                Else
                    Application_Initializing_Form.Show()
                End If
                For Each tabPage As TabPage In Setting_TbCntrl.TabPages
                    If tabPage.Name <> "Prayer_Time_TbPg" And
                            tabPage.Name <> "Form_Parameters_TbPg" Then Continue For
                    For Each ctrl In tabPage.Controls
                        If ctrl.GetType() = GetType(CheckBox) Then
                            AddHandler CType(ctrl, CheckBox).CheckStateChanged, AddressOf ChkBx_CheckStateChanged
                        End If
                    Next
                Next
            End If
            If Debugger.IsAttached Then
                Show_Btn.Visible = True
            Else
                Show_Btn.Visible = False
            End If
            Note_Font_Name_CmbBx.ValueMember = "Key"
            Note_Font_Name_CmbBx.DisplayMember = "Value"
            External_Note_Font_Name_CmbBx.ValueMember = "Key"
            External_Note_Font_Name_CmbBx.DisplayMember = "Value"
            User_Name_CmbBx.ValueMember = "Key"
            User_Name_CmbBx.DisplayMember = "Value"
            Authority_User_CmbBx.ValueMember = "Key"
            Authority_User_CmbBx.DisplayMember = "Value"
            Customer_Name_CmbBx.ValueMember = "Key"
            Customer_Name_CmbBx.DisplayMember = "Value"

            Project_Name_CmbBx.ValueMember = "Key"
            Project_Name_CmbBx.DisplayMember = "Value"
            Project_Customer_Name_CmbBx.ValueMember = "Key"
            Project_Customer_Name_CmbBx.DisplayMember = "Value"
            Employee_Name_CmbBx.ValueMember = "Key"
            Employee_Name_CmbBx.DisplayMember = "Value"
            Project_Detail_Project_Name_CmbBx.ValueMember = "Key"
            Project_Detail_Project_Name_CmbBx.DisplayMember = "Value"
            Event_Owner_CmbBx.ValueMember = "Key"
            Event_Owner_CmbBx.DisplayMember = "Value"
            Approval_From_Name_CmbBx.ValueMember = "Key"
            Approval_From_Name_CmbBx.DisplayMember = "Value"
            User_Authority_Name_CmbBx.ValueMember = "Key"
            User_Authority_Name_CmbBx.DisplayMember = "Value"

            If Note_Font_Name_CmbBx.Items.Count = 0 Then
                For Each oFont As FontFamily In FontFamily.Families
                    Note_Font_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(oFont.Name, oFont.Name))
                    External_Note_Font_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(oFont.Name, oFont.Name))
                Next
            End If

            If Not RunAsExternal(UseArgFile) Then
                Fagr_Voice_Files_CmbBx.ValueMember = "Key"
                Fagr_Voice_Files_CmbBx.DisplayMember = "Value"
                For Each VoiceFile In Directory.GetFiles(Application.StartupPath & "\Azan Voices\Fagr Voice")
                    If Path.GetExtension(VoiceFile) = ".jpg" Then Continue For
                    Fagr_Voice_Files_CmbBx.Items.Add(New KeyValuePair(Of String, String)(VoiceFile, Path.GetFileNameWithoutExtension(VoiceFile)))
                Next
                Voice_Azan_Files_CmbBx.ValueMember = "Key"
                Voice_Azan_Files_CmbBx.DisplayMember = "Value"
                For Each VoiceFile In Directory.GetFiles(Application.StartupPath & "\Azan Voices")
                    If Path.GetExtension(VoiceFile) = ".jpg" Then Continue For
                    Voice_Azan_Files_CmbBx.Items.Add(New KeyValuePair(Of String, String)(VoiceFile, Path.GetFileNameWithoutExtension(VoiceFile)))
                Next
            Else
                Setting_TbCntrl.Visible = False
            End If
            RegisterMeWithDesktopRightClickMenu()
            RC = New ResizeableControl_Class(Me)

            AddHandler Setting_TbCntrl.DrawItem, AddressOf TbCntrl_DrawItem
            AddHandler ShortCut_TbCntrl.DrawItem, AddressOf TbCntrl_DrawItem
            AddHandler MagNotes_Notes_TbCntrl.DrawItem, AddressOf TbCntrl_DrawItem
            AddHandler Projects_TbCntrl.DrawItem, AddressOf TbCntrl_DrawItem

#Region "MagNote"
            Setting_TbCntrl.SelectTab(Setting_TbCntrl.TabPages("Note_Parameters_TbPg"))
            Setting_TbCntrl.SelectTab(Setting_TbCntrl.TabPages("MagNotes_TbPg"))

            Cursor = Cursors.WaitCursor
            MagNote_No_CmbBx.ValueMember = "Key"
            MagNote_No_CmbBx.DisplayMember = "Value"

            File_Format_CmbBx.ValueMember = "Key"
            File_Format_CmbBx.DisplayMember = "Value"

            MagNote_Category_CmbBx.ValueMember = "Key"
            MagNote_Category_CmbBx.DisplayMember = "Value"
            Control.CheckForIllegalCrossThreadCalls = False
            Selected_Text_Color_TlStrpMnItm.BackColor = Color.Red
            CloseProcess("cmd")
            ExitIfMagNoteIsRunning()
            Form_ToolTip.BackColor = Color.Yellow
            Form_ToolTip.IsBalloon = True
            MakeTopMost()
            If Available_MagNotes_DGV.Columns.Count > 0 Then Exit Sub
            AddAvailable_MagNotes_DGVColumns()
            MagNote_TxtBxAddHandler()
            Control.CheckForIllegalCrossThreadCalls = False
            myWatch = New SystemWideHotkeyComponent
            myWatch.Altkey = True
            myWatch.HotKey = Keys.S
            myWatch.ShiftKey = True
            myWatch.HotKey = Keys.F4
            AddHandler myWatch.HotkeyPressed, AddressOf myHotKeyPressed

            myWatch1 = New SystemWideHotkeyComponent
            myWatch1.Altkey = True
            myWatch1.HotKey = Keys.D
            myWatch1.ShiftKey = True
            myWatch1.HotKey = Keys.F4
            AddHandler myWatch1.HotkeyPressed, AddressOf HideDesktopIcons

            myWatch1 = New SystemWideHotkeyComponent
            myWatch1.Altkey = True
            myWatch1.HotKey = Keys.M
            myWatch1.ShiftKey = True
            myWatch1.HotKey = Keys.F4
            AddHandler myWatch1.HotkeyPressed, AddressOf MuteAlerts

            IgnoreNoteAmendmented = True
            MagNotes_Notes_TabControl_Add_MenuStrip()
            Available_MagNotes_DGV_Add_MenuStrip()
#End Region

            Available_MagNotes_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            Available_Alerts_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            Availabel_Users_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            Availabel_Customers_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            Available_Projects_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            Project_Events_Details_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            User_Authorities_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
            Available_Authorities_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText

            Alert_Sound_Volume_TrkBr.Value = GetVolume()

        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Declare Function ShutdownBlockReasonCreate Lib "user32.dll" (ByVal hWnd As IntPtr, <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal reason As String) As Boolean
    Declare Function ShutdownBlockReasonDestroy Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_QUERYENDSESSION OrElse m.Msg = WM_ENDSESSION Then
            If systemShutdown Then Exit Sub
            systemShutdown = True
            If Not RunAsExternal() Then
                FinalizeShutDown()
                Exit Sub
            End If
        End If
        Const WM_SYSCOMMAND As Integer = &H112
        Const SC_MAXIMIZE As Integer = &HF030
        Const SC_RESTORE As Integer = &HF120
        Const SC_MINIMIZE As Integer = &HF020
        Const SC_CLOSE As Integer = &HF060
        If m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32() = SC_MAXIMIZE Then
            IgnoreWinSize = True
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                MeIsComlressed(1)
                Return
            End If
        ElseIf m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32() = SC_RESTORE Then
            ShowMsg("Restore Down button clicked" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                Me.Activate()
                MeIsComlressed()
                Return
            End If
        ElseIf m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32() = SC_MINIMIZE Then
            IgnoreWinSize = True
            ShowMsg("Minimize button clicked" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                MeIsComlressed()
                Return
            End If
        ElseIf m.Msg = WM_SYSCOMMAND AndAlso m.WParam.ToInt32() = SC_CLOSE Then
            ShowMsg("Close button clicked" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
        End If
        If CurrentDayOfYear <> Today.DayOfYear Then
            CurrentDayOfYear = Today.DayOfYear
            Alert_Tmr.Start()
        End If
        MyBase.WndProc(m)
    End Sub
    Private Function StartStopwatch() As Boolean
        Dim sw As New Stopwatch
        ' Pretend work for 3 minutes
        sw.Start()
        Do While sw.ElapsedMilliseconds < 180000
            Application.DoEvents()
        Loop
        sw.Stop()
        systemShutdown = False

        Dim p As New ProcessStartInfo("shutdown", "/s /t 0")
        p.CreateNoWindow = True
        p.UseShellExecute = False
        Process.Start(p)
        Application.Exit()
    End Function
    Private Function FinalizeShutDown()
        SaveFormParameters(Nothing)
        NoteAmendmented(, 1)
        If IfOpenedTabPagesChanged() Then
            SaveOpenedTabPages()
        End If
        RememberOpenedExternalFiles()
    End Function
#Region "Reo_Grid"
#Region "Alignment"
    Private Sub Text_Align_Center_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.HorizontalAlign,
            .HAlign = ReoGridHorAlign.Center
        }))
    End Sub
    Private Sub Text_Align_Bottom_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Bottom
            }))
    End Sub

    Private Sub Text_Align_Middle_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Middle
            }))
    End Sub

    Private Sub Text_Align_Top_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Top
            }))
    End Sub

    Private Sub Distributed_Indent_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.DistributedIndent
            }))
    End Sub

    Private Sub Text_Align_Right_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.Right
            }))
    End Sub

    Private Sub Text_Align_Left_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(Grid.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.HorizontalAlign,
            .HAlign = ReoGridHorAlign.Left
        }))
    End Sub
    Private Sub Text_Wrap_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(CurrentSelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.TextWrap,
            .TextWrapMode = If(sender.Checked, TextWrapMode.WordBreak, TextWrapMode.NoWrap)
        }))
    End Sub
    Public ReadOnly Property GridControl As ReoGridControl
        Get
            Return Grid()
        End Get
    End Property
    Public ReadOnly Property CurrentWorksheet As Worksheet
        Get
            Return Grid.CurrentWorksheet
        End Get
    End Property
    Public Property CurrentSelectionRange As RangePosition
        Get
            Return Grid.CurrentWorksheet.SelectionRange
        End Get
        Set(ByVal value As RangePosition)
            Grid.CurrentWorksheet.SelectionRange = value
        End Set
    End Property
#End Region

#Region "Cell & Range"
    Private Sub Cell_Merge_Range_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_Merge_Range_TlStrpMnItm.Click
        Try
            Grid.DoAction(New MergeRangeAction(CurrentWorksheet.SelectionRange))
        Catch __unusedRangeTooSmallException1__ As RangeTooSmallException
        Catch __unusedRangeIntersectionException2__ As RangeIntersectionException
            ShowMsg(__unusedRangeIntersectionException2__.Message, "ReoGrid Editor")
        End Try
    End Sub

    Private Sub Cell_UnMerge_Range_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_UnMerge_Range_TlStrpMnItm.Click
        Grid.DoAction(New UnmergeRangeAction(CurrentWorksheet.SelectionRange))
    End Sub

    Private Sub ApplyFunctionToSelectedRange(ByVal funName As String)
        Dim sheet = CurrentWorksheet
        Dim range = CurrentSelectionRange
        If range.Rows > 1 Then
            For c As Integer = range.Col To range.EndCol
                Dim cell = sheet.Cells(range.EndRow, c)
                If String.IsNullOrEmpty(cell.DisplayText) Then
                    cell.Formula = String.Format("{0}({1})", funName, RangePosition.FromCellPosition(range.Row, range.Col, range.EndRow - 1, c).ToAddress())
                    Exit For
                End If
            Next
        End If
        If range.Cols > 1 Then
            For r As Integer = range.Row To range.EndRow
                Dim cell = sheet.Cells(r, range.EndCol)
                If String.IsNullOrEmpty(cell.DisplayText) Then
                    cell.Formula = String.Format("{0}({1})", funName, RangePosition.FromCellPosition(range.Row, range.Col, r, range.EndCol - 1).ToAddress())
                    Exit For
                End If
            Next
        End If
    End Sub
#End Region

#Region "Style"

    Private Sub Bold_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.FontStyleBold,
            .Bold = sender.Checked
        }))
    End Sub

    Private Sub Italic_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.FontStyleItalic,
            .Italic = sender.Checked
        }))
    End Sub

    Private Sub Under_Line_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.FontStyleUnderline,
            .Underline = sender.Checked
        }))
    End Sub

    Private Sub Strike_Through_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.FontStyleStrikethrough,
            .Strikethrough = sender.Checked
        }))
    End Sub

    Private Sub SetGridFontSize()
        If isUIUpdating Then Return
        Dim FontSize As Single = 9
        Single.TryParse(Font_Size_TlStrpCmbBx.Text, FontSize)
        If FontSize <= 0 Then FontSize = 1.0F
        Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.FontSize,
            .FontSize = FontSize
        }))
    End Sub

#End Region

#Region "Update Menus & Toolbars"
    Private isUIUpdating As Boolean = False
    Private Sub UpdateMenuAndToolStrips()
        If isUIUpdating Then Return
        isUIUpdating = True
        Dim worksheet = CurrentWorksheet
        Dim style As WorksheetRangeStyle = worksheet.GetCellStyles(worksheet.SelectionRange.StartPos)

        If style IsNot Nothing Then
            Font_TlStrpCmbBx.Text = style.FontName
            Font_Size_TlStrpCmbBx.Text = style.FontSize.ToString()
            Bold_TlStrpBtn.Checked = style.Bold
            Italic_TlStrpBtn.Checked = style.Italic
            Strike_Through_TlStrpBtn.Checked = style.Strikethrough
            Under_Line_TlStrpBtn.Checked = style.Underline
            Text_Align_Left_TlStrpBtn.Checked = style.HAlign = ReoGridHorAlign.Left
            Text_Align_Center_TlStrpBtn.Checked = style.HAlign = ReoGridHorAlign.Center
            Text_Align_Right_TlStrpBtn.Checked = style.HAlign = ReoGridHorAlign.Right
            Distributed_Indent_TlStrpBtn.Checked = style.HAlign = ReoGridHorAlign.DistributedIndent
            Text_Align_Top_TlStrpBtn.Checked = style.VAlign = ReoGridVerAlign.Top
            Text_Align_Middle_TlStrpBtn.Checked = style.VAlign = ReoGridVerAlign.Middle
            Text_Align_Bottom_TlStrpBtn.Checked = style.VAlign = ReoGridVerAlign.Bottom
            Text_Wrap_TlStrpBtn.Checked = style.TextWrapMode <> TextWrapMode.NoWrap
            Dim borderInfo As RangeBorderInfoSet = worksheet.GetRangeBorders(worksheet.SelectionRange)

            Undo_TlStrpBtn.Enabled = CSharpImpl.Assigning(Undo_TlStrpBtn.Enabled, Grid.CanUndo())
            Redo_TlStrpBtn.Enabled = CSharpImpl.Assigning(Redo_TlStrpBtn.Enabled, Grid.CanRedo())
            isUIUpdating = False
        End If
    End Sub
    Private settingSelectionMode As Boolean = False
    Private Class CSharpImpl
        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        Shared Function Assigning(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
    Private Sub fontSizeToolStripComboBox_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Font_Size_TlStrpCmbBx_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        SetGridFontSize()
    End Sub
    Private Sub CtxMenu_Col_Insert_Columns_Click(sender As Object, e As EventArgs) Handles CtxMenu_Col_Insert_Columns.Click
    End Sub
    Private Sub Font_TlStrpCmbBx_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Font_TlStrpCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontName,
                .FontName = DirectCast(sender.SelectedItem, KeyValuePair(Of String, String)).Value
            }))
            Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
            .Flag = PlainStyleFlag.FontSize,
            .FontSize = Font_Size_TlStrpCmbBx.Text
        }))
        Catch ex As Exception
        End Try
    End Sub
#End Region
    Private Sub Zoom_TlStrpCmbBx_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        If isUIUpdating Then Return
        Dim ZTSDDB_Value As String = sender.Text
        If ZTSDDB_Value.Length > 0 Then
            If Not ZTSDDB_Value.Contains("%") Then
                ZTSDDB_Value = sender.Text & "%"
            Else
                ZTSDDB_Value = sender.Text
            End If
            Dim value As Integer = 100
            If Integer.TryParse(ZTSDDB_Value.Substring(0, ZTSDDB_Value.Length - 1), value) Then
                Dim scale As Single = CSng(value) / 100.0F
                scale = CSng(Math.Round(scale, 1))
                CurrentWorksheet.SetScale(scale)
            End If
        End If
    End Sub
    Private Sub Font_Smaller_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New StepRangeFontSizeAction(Grid.CurrentWorksheet.SelectionRange, False))
    End Sub
    Private Sub Enlarge_Font_TlStrpBtn_Click(sender As Object, e As EventArgs)
        Grid.DoAction(New StepRangeFontSizeAction(Grid.CurrentWorksheet.SelectionRange, True))
    End Sub
#Region "Border Settings"
    Private Sub SetSelectionBorder(ByVal borderPos As BorderPositions, ByVal style As BorderLineStyle)
        Grid.DoAction(New SetRangeBorderAction(CurrentWorksheet.SelectionRange, borderPos, New RangeBorderStyle With {
            .Color = Color.FromArgb(255, Cell_Border_Color_ClrCmbBx.SelectedItem),
            .Style = style
        }))
    End Sub
    Private Sub Bold_Outside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Dotted_Outside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Dotted)
    End Sub
    Private Sub Solid_Outside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Solid)
    End Sub
    Private Sub Dashed_Outside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Dashed)
    End Sub
    Private Sub Inside_Borders_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Inside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Inside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Dotted_Inside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Inside_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Dashed)
    End Sub
    Private Sub Left_Right_Borders_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Left_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Left_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Left_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Left_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Dashed)
    End Sub
    Private Sub Top_Bottom_Borders_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Top_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Top_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Top_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Top_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Dashed)
    End Sub
    Private Sub All_Borders_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.All, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_All_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.All, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_All_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.All, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Dotted_All_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.All, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_All_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.All, BorderLineStyle.Dashed)
    End Sub
    Private Sub Solid_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Solid)
    End Sub
    Private Sub Left_Border_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Left, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Dashed)
    End Sub
    Private Sub Top_Border_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Top_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Solid)
    End Sub
    Private Sub Blod_Top_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Top, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Top_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Top_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Dashed)
    End Sub
    Private Sub Bottom_Border_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Bottom_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Dashed)
    End Sub
    Private Sub Right_Boder_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Right, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Right_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Dashed)
    End Sub
    Private Sub Right_Slash_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Right_Slash_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Right_Slash_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Right_Slash_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Right_Slash_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Dashed)
    End Sub
    Private Sub Slash_Left_TlStrpBtn_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Solid)
    End Sub
    Private Sub Solid_Slash_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Solid)
    End Sub
    Private Sub Bold_Slash_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.BoldSolid)
    End Sub
    Private Sub Doted_Slash_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Dotted)
    End Sub
    Private Sub Dashed_Slash_Left_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Dashed)
    End Sub
    Private Sub ColorComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Select Case sender.name
                Case "Cell_Border_Color_ClrCmbBx"
'after selection the colore you have to select boreder style
                Case "Cell_BackColor_ClrCmbBx"
                    Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle() With {
                .Flag = PlainStyleFlag.BackColor,
                .BackColor = Color.FromArgb(255, Cell_BackColor_ClrCmbBx.SelectedItem)
            }))

                Case "Cell_Text_Color_ClrCmbBx"
                    Dim textColor = Color.FromArgb(255, Cell_Text_Color_ClrCmbBx.SelectedItem)
                    If textColor.IsEmpty Then
                        Grid.DoAction(New RemoveRangeStyleAction(CurrentWorksheet.SelectionRange, PlainStyleFlag.TextColor))
                    Else
                        Grid.DoAction(New SetRangeStyleAction(CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                    .Flag = PlainStyleFlag.TextColor,
                    .TextColor = textColor
                }))
                    End If
            End Select
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Clear_Borders_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Grid.DoAction(New SetRangeBorderAction(CurrentWorksheet.SelectionRange, BorderPositions.All, New RangeBorderStyle With {
            .Color = Color.Empty,
            .Style = BorderLineStyle.None
        }))
    End Sub
    Private Sub Outside_Borders_TlStrpBtn_ButtonClick(sender As Object, e As EventArgs)
        SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Solid)
    End Sub
    Private Sub Zoom_TlStrpCmbBx_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Text_Color_TlStrpLbl_Click(sender As Object, e As EventArgs)
    End Sub
    Dim rect As Rectangle
    Dim pnt As Point
    Private Sub TlStrpLbl_LocationChanged(sender As Object, e As EventArgs)
        Try
            If WindowState = FormWindowState.Minimized Or
                WindowState = FormWindowState.Maximized Then
                Exit Sub
            End If
            If Not Visible Then
                Exit Sub
            End If
            Select Case sender.name
                Case Text_Color_TlStrpLbl.Name
                    rect = Text_Color_TlStrpLbl.Bounds
                    pnt = New Point(rect.Left, rect.Bottom)
                    Cell_Text_Color_ClrCmbBx.Location = pnt
                Case Cell_Border_Color_TlStrpLbl.Name
                    rect = Cell_Border_Color_TlStrpLbl.Bounds
                    pnt = New Point(rect.Left, rect.Bottom)
                    Cell_Border_Color_ClrCmbBx.Location = pnt
                Case Cell_BackColor_TlStrpLbl.Name
                    rect = Cell_BackColor_TlStrpLbl.Bounds
                    pnt = New Point(rect.Left, rect.Bottom)
                    Cell_BackColor_ClrCmbBx.Location = pnt
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Undo(ByVal sender As Object, ByVal e As EventArgs)
        Grid.Undo()
    End Sub
    Private Sub Redo(ByVal sender As Object, ByVal e As EventArgs)
        Grid.Redo()
    End Sub
    Private Sub Style_Brush_TlStrpBtn_Click_1(sender As Object, e As EventArgs)
        CurrentWorksheet.StartPickRangeAndCopyStyle()
    End Sub
#End Region
#Region "Editing"
    Private Sub Cut_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Cut()
    End Sub
    Private Sub Copy_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Copy()
    End Sub
    Private Sub Paste_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Paste()
    End Sub

    Private Sub Cell_Cut_Range_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_Cut_Range_TlStrpMnItm.Click, Col_Cut_TlStrpMnItm.Click, Row_Cut_TlStrpMnItm.Click
        Cut_TlStrpBtn_Click(sender, EventArgs.Empty)
    End Sub
    Private Sub Cell_Copy_Range_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_Copy_Range_TlStrpMnItm.Click, Col_Copy_TlStrpMnItm.Click, Row_Copy_TlStrpMnItm.Click
        Copy_TlStrpBtn_Click(sender, EventArgs.Empty)
    End Sub
    Private Sub Cell_Paste_Range_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_Paste_Range_TlStrpMnItm.Click, Col_Paste_TlStrpMnItm.Click, Row_Paste_TlStrpMnItm.Click
        Paste_TlStrpBtn_Click(sender, EventArgs.Empty)
    End Sub
    Private Sub repeatLastActionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Grid.RepeatLastAction(CurrentWorksheet.SelectionRange)
    End Sub
    Private Sub Cell_Select_All_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_Select_All_TlStrpMnItm.Click
        CurrentWorksheet.SelectAll()
    End Sub
    Private Sub Cut()
        Try
            CurrentWorksheet.Cut()
        Catch ex As Exception
            ShowMsg(ex.Message,,, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Copy()
        Try
            CurrentWorksheet.Copy()
        Catch ex As Exception
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Paste()
        Try
            CurrentWorksheet.Paste()
        Catch ex As Exception
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
#Region "#View & Print"

    Private Sub Close_Grid_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Close_Grid_TlStrpMnItm.Click
        If Language_Btn.Text = "E" Then
            Msg = "هل انت متأكد... غلق الجدول معناه تجاهل حفظ الجدول مع حفظ الملف... هل انت متأكد ؟"
        Else
            Msg = "Are You Sure... Closing The Grid Means Ignor Saving The Grid With Saving The File... Are You Sure?"
        End If
        If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
        RCSN.Dock = System.Windows.Forms.DockStyle.Fill
        GridPnl.Visible = False
    End Sub

    Private Sub Cell_Format_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cell_Format_TlStrpMnItm.Click
    End Sub
    Private Sub Row_Remove_Page_Break_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Row_Remove_Page_Break_TlStrpMnItm.Click
        Grid.CurrentWorksheet.RemoveRowPageBreak(Grid.CurrentWorksheet.FocusPos.Row)
    End Sub

    Private Sub Col_Remove_Page_Break_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Remove_Page_Break_TlStrpMnItm.Click
        Try
            Grid.CurrentWorksheet.RemoveColumnPageBreak(Grid.CurrentWorksheet.FocusPos.Col)
        Catch ex As Exception
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Row_Insert_Page_Break_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Row_Insert_Page_Break_TlStrpMnItm.Click
        Try
            CurrentWorksheet.RowPageBreaks.Add(CurrentWorksheet.FocusPos.Row)
        Catch ex As Exception
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Col_Insert_Page_Break_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Insert_Page_Break_TlStrpMnItm.Click
        CurrentWorksheet.ColumnPageBreaks.Add(CurrentWorksheet.FocusPos.Col)
    End Sub
    Private Sub Print_Preview_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Grid.CurrentWorksheet.ResetAllPageBreaks()
            Grid.CurrentWorksheet.AutoSplitPage()
            Grid.CurrentWorksheet.EnableSettings(WorksheetSettings.View_ShowPageBreaks)
        Catch ex As Exception
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
            Return
        End Try
        Using session = Grid.CurrentWorksheet.CreatePrintSession()
            Using ppd As PrintPreviewDialog = New PrintPreviewDialog()
                ppd.Document = session.PrintDocument
                ppd.SetBounds(200, 200, Width, Height)
                ppd.PrintPreviewControl.Zoom = 1.0R
                ppd.ShowDialog(Me)
            End Using
        End Using
    End Sub
#End Region
#Region "Save File"
    Public Property CurrentFilePath As String
    Private currentTempFilePath As String
    Public Sub LoadFile(ByVal path As String)
        Try
            Dim Encoding As Encoding = Encoding.[Default]
            CurrentFilePath = Nothing
            Initiate_ReoGride()
            SetGridSize()
            Grid.Reset()
            Grid.Load(path, IO.FileFormat._Auto, Encoding)
            Msg = System.IO.Path.GetFileName(path) & " - (By ReoGrid Editor)" '& ProductVersion
            Text = GetMyInfo() & " / " & Msg
            CurrentFilePath = path
            currentTempFilePath = Nothing
            If Grid.Visible And (Blocked_Note_ChkBx.CheckState = CheckState.Checked Or Finished_Note_ChkBx.CheckState = CheckState.Checked) Then
                GridReadonly(True)
            End If
            Grid.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, True)
            For Each Sheet In Grid.Worksheets
                AddHandler Sheet.AfterCellEdit, AddressOf Worksheet_AfterCellEdit
                Sheet.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, True)
                Sheet.SetSettings(WorksheetSettings.Edit_AutoExpandColumnWidth, True)
                Sheet.SetSettings(WorksheetSettings.Edit_AutoExpandRowHeight, True)
                Sheet.SetSettings(WorksheetSettings.Edit_AutoFormatCell, True)
                Sheet.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, True)
                Sheet.SetSettings(WorksheetSettings.View_ShowRulers, True)
                Sheet.SetSettings(WorksheetSettings.View_ShowRulers, True)
                Sheet.Recalculate()
                Application.DoEvents()
            Next
            Grid.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, True)
            Grid.CurrentWorksheet.SelectionForwardDirection = SelectionForwardDirection.Down
        Catch ex As FileNotFoundException
            ShowMsg(ex.Message & vbNewLine & "Load File Failed " + ex.FileName, "ReoGrid Editor",, MessageBoxIcon.Error)
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & "Load File Failed ", "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub NewFile()
        If Not CloseDocument() Then
            Return
        End If

        Grid.Reset()
        Grid.Text = GetMyInfo() & " / " & "Untitled " & " - ReoGrid Editor " + ProductVersion
        CurrentFilePath = Nothing
        currentTempFilePath = Nothing
        AddHandler Grid.CurrentWorksheet.SelectionRangeChanged, AddressOf grid_SelectionRangeChanged
    End Sub
    Private Function SaveFile(ByVal path As String) As Boolean
        Dim fm As FileFormat = FileFormat._Auto

        If LCase(path).EndsWith(LCase(".xlsx"), StringComparison.CurrentCultureIgnoreCase) Then
            fm = FileFormat.Excel2007
        ElseIf LCase(path).EndsWith(LCase(".rgf"), StringComparison.CurrentCultureIgnoreCase) Then
            fm = FileFormat.ReoGridFormat
        ElseIf LCase(path).EndsWith(LCase(".csv"), StringComparison.CurrentCultureIgnoreCase) Then
            fm = FileFormat.CSV
        End If
        Try
            Grid.Save(path, fm)
            SetCurrentDocumentFile(path)
            Return True
        Catch ex As Exception
            If File.Exists(path) Then
                File.Delete(CurrentFilePath)
            End If
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Function
    Private Function GetMyInfo() As String
        Try
            Dim MeText As String
            If Language_Btn.Text = "E" Then
                MeText = "مذكرة... إصدار (" & My.Application.Info.Version.ToString & ")"
            ElseIf Language_Btn.Text = "ع" Then
                MeText = "MagNote... Version (" & My.Application.Info.Version.ToString & ")"
            End If
            Return MeText
        Catch ex As Exception
            ShowMsg(ex.Message, "ReoGrid Editor",, MessageBoxIcon.Error)
        End Try
    End Function
    Private Sub SetCurrentDocumentFile(ByVal filepath As String)
        Text = GetMyInfo() & " / " & System.IO.Path.GetFileName(filepath) & " - ReoGrid Editor " & ProductVersion
        CurrentFilePath = filepath
        currentTempFilePath = Nothing
    End Sub
    Private Sub new_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        NewFile()
    End Sub
    Private Sub load_TlStrpBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Using ofd As OpenFileDialog = New OpenFileDialog()
            ofd.Filter = "Looks up a localized string similar to Excel 2007(*.xlsx)|*.xlsx|ReoGrid XML Format(*.rgf)|*.rgf|CSV File(*.csv)|*.csv|All Files(*.*)|*.*."

            If ofd.ShowDialog(Me) = DialogResult.OK Then
                LoadFile(ofd.FileName)
                SetCurrentDocumentFile(ofd.FileName)
            End If
        End Using
    End Sub
    Public Function SaveDocument() As Boolean
        If String.IsNullOrEmpty(CurrentFilePath) Then
            Return SaveAsDocument()
        Else
            Return SaveFile(CurrentFilePath)
        End If
    End Function
    Public Function SaveAsDocument() As Boolean
        Using sfd As SaveFileDialog = New SaveFileDialog()
            sfd.Filter = "Looks up a localized string similar to Excel 2007(*.xlsx)|*.xlsx|ReoGrid XML Format(*.rgf)|*.rgf|CSV File(*.csv)|*.csv|All Files(*.*)|*.*."
            If Not String.IsNullOrEmpty(CurrentFilePath) Then
                sfd.FileName = Path.GetFileNameWithoutExtension(CurrentFilePath)
                Dim format = GetFormatByExtension(CurrentFilePath)
                Select Case format
                    Case FileFormat.Excel2007
                        sfd.FilterIndex = 1
                    Case FileFormat.ReoGridFormat
                        sfd.FilterIndex = 2
                    Case FileFormat.CSV
                        sfd.FilterIndex = 3
                End Select
            End If
            If sfd.ShowDialog(Me) = DialogResult.OK Then
                Return (SaveFile(sfd.FileName))
            End If
        End Using
    End Function
    Public Property NewDocumentOnLoad As Boolean
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        Try
            MyBase.OnLoad(e)
            If Not String.IsNullOrEmpty(CurrentFilePath) Then
                LoadFile(CurrentFilePath)
            ElseIf NewDocumentOnLoad Then
                NewFile()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        load_TlStrpBtn_Click(sender, EventArgs.Empty)
    End Sub
    Public Function CloseDocument(Optional ByVal DisMsg As Boolean = True) As Boolean
        If Grid.IsWorkbookEmpty Then
            Return True
        End If
        Dim dr As DialogResult
        If DisMsg Then
            dr = ShowMsg("Msg_Save_Changes", "ReoGrid Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        Else
            dr = System.Windows.Forms.DialogResult.No
        End If

        If dr = System.Windows.Forms.DialogResult.No Then
            Return True
        ElseIf dr = System.Windows.Forms.DialogResult.Cancel Then
            Return False
        End If
        Dim format As FileFormat = FileFormat._Auto
        If Not String.IsNullOrEmpty(CurrentFilePath) Then
            format = GetFormatByExtension(CurrentFilePath)
        End If
        If format = FileFormat._Auto OrElse String.IsNullOrEmpty(CurrentFilePath) Then
            Return SaveAsDocument()
        Else
            Return (SaveDocument())
        End If
    End Function
    Private Function GetFormatByExtension(ByVal FilePath As String) As FileFormat
        If String.IsNullOrEmpty(FilePath) Then
            Return FileFormat._Auto
        End If
        Dim ext As String = Path.GetExtension(CurrentFilePath)
        If ext.Equals(".rgf", StringComparison.CurrentCultureIgnoreCase) OrElse ext.Equals(".xml", StringComparison.CurrentCultureIgnoreCase) Then
            Return FileFormat.ReoGridFormat
        ElseIf ext.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase) Then
            Return FileFormat.Excel2007
        ElseIf ext.Equals(".csv", StringComparison.CurrentCultureIgnoreCase) Then
            Return FileFormat.CSV
        Else
            Return FileFormat._Auto
        End If
    End Function
    Friend Sub ShowStatus(ByVal msg As String)
        ShowStatus(msg, False)
    End Sub
    Friend Sub ShowStatus(ByVal msg As String, ByVal [error] As Boolean)
        MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = msg
        MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").ForeColor = If([error], Color.Red, SystemColors.WindowText)
    End Sub
    Private Sub Save_TlStrpBtn_Click(sender As Object, e As EventArgs)
        If sender.Checked Then
            If Language_Btn.Text = "E" Then
                Msg = "هل تريد حفظ الجدول خارج الملاحظة الملاحظة؟"
            Else
                Msg = "Save File Outside Of MagNote?"
            End If
            Dim dr = ShowMsg(Msg, "ReoGrid Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If dr = System.Windows.Forms.DialogResult.Yes Then
                Using ofd As SaveFileDialog = New SaveFileDialog()
                    Dim Extension = DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                    ofd.FileName = MagNote_No_CmbBx.Text & ".xlsx"
                    ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    ofd.Filter = "Currnt File|" & ofd.FileName & "|Text Files|*.txt|All files|*.*"
                    If ofd.ShowDialog(Me) <> DialogResult.Cancel Then
                        CurrentFilePath = ofd.FileName
                    End If
                End Using
                SaveDocument()
            ElseIf dr = System.Windows.Forms.DialogResult.No Then
                Save_Note_TlStrpBtn_Click(sender, EventArgs.Empty)
            End If
            sender.Checked = False
        End If
    End Sub
#End Region
#Region "Context Menu"
    Private Sub Col_Insert_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Insert_TlStrpMnItm.Click
        If CurrentSelectionRange.Cols >= 1 Then
            Grid.DoAction(New InsertColumnsAction(CurrentSelectionRange.Col, CurrentSelectionRange.Cols))
        End If
    End Sub
    Private Sub Row_Insert_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Row_Insert_TlStrpMnItm.Click
        If CurrentSelectionRange.Rows >= 1 Then
            Grid.DoAction(New InsertRowsAction(CurrentSelectionRange.Row, CurrentSelectionRange.Rows))
        End If
    End Sub
    Private Sub Col_Delete_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Delete_TlStrpMnItm.Click
        If CurrentSelectionRange.Cols >= 1 Then
            Grid.DoAction(New RemoveColumnsAction(CurrentSelectionRange.Col, CurrentSelectionRange.Cols))
        End If

    End Sub
    Private Sub Row_Delete_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Row_Delete_TlStrpMnItm.Click
        If CurrentSelectionRange.Rows >= 1 Then
            Grid.DoAction(New RemoveRowsAction(CurrentSelectionRange.Row, CurrentSelectionRange.Rows))
        End If
    End Sub
    Private Sub Col_Reset_To_Default_Width_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Reset_To_Default_Width_TlStrpMnItm.Click
        Grid.DoAction(New SetColumnsWidthAction(CurrentSelectionRange.Col, CurrentSelectionRange.Cols, Worksheet.InitDefaultColumnWidth))
    End Sub
    Private Sub Row_Reset_To_Default_Height_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Row_Reset_To_Default_Height_TlStrpMnItm.Click
        Grid.DoAction(New SetRowsHeightAction(CurrentSelectionRange.Row, CurrentSelectionRange.Rows, Worksheet.InitDefaultRowHeight))
    End Sub
#End Region
#Region "Filter"
    Private columnFilter As AutoColumnFilter
    Private Sub Col_Filter_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Filter_TlStrpMnItm.Click
        If columnFilter IsNot Nothing Then
            columnFilter.Detach()
        End If

        Dim action As CreateAutoFilterAction = New CreateAutoFilterAction(CurrentWorksheet.SelectionRange)
        Grid.DoAction(action)
        columnFilter = action.AutoColumnFilter
    End Sub
    Private Sub Col_Clear_Filter_TlStrpMnItm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Col_Clear_Filter_TlStrpMnItm.Click
        If columnFilter IsNot Nothing Then
            columnFilter.Detach()
        End If
    End Sub
    Private Sub Col_Width_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Col_Width_TlStrpMnItm.Click
    End Sub
    Private Sub Col_Hide_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Col_Hide_TlStrpMnItm.Click
        Grid.DoAction(New HideColumnsAction(
                CurrentWorksheet.SelectionRange.Col, CurrentWorksheet.SelectionRange.Cols))
    End Sub
    Private Sub Col_Unhide_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Col_Unhide_TlStrpMnItm.Click
        Grid.DoAction(New UnhideColumnsAction(
                CurrentWorksheet.SelectionRange.Col, CurrentWorksheet.SelectionRange.Cols))
    End Sub
    Private Sub Row_Hide_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Row_Hide_TlStrpMnItm.Click
        Grid.DoAction(New HideRowsAction(
                CurrentWorksheet.SelectionRange.Row, CurrentWorksheet.SelectionRange.Rows))
    End Sub
    Private Sub Row_Unhide_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Row_UnHide_TlStrpMnItm.Click
        Grid.DoAction(New UnhideRowsAction(
                CurrentWorksheet.SelectionRange.Row, CurrentWorksheet.SelectionRange.Rows))
    End Sub

    Private Sub Col_Format_Cells_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Col_Format_Cells_TlStrpMnItm.Click
    End Sub
    Private Sub grid_SelectionRangeChanged(ByVal sender As Object, ByVal e As RangeEventArgs)
        Dim worksheet = TryCast(sender, Worksheet)

        If worksheet.Name = Grid.CurrentWorksheet.Name Then

            If worksheet.SelectionRange = RangePosition.Empty Then
                'rangeInfoToolStripStatusLabel.Text = "Selection None"
            Else
                'rangeInfoToolStripStatusLabel.Text = String.Format("{0} {1} x {2}", worksheet.SelectionRange.ToString(), worksheet.SelectionRange.Rows, worksheet.SelectionRange.Cols)
            End If

            'UpdateMenuAndToolStrips()
        End If
    End Sub
    Dim Spliter_2_Lbl As New Panel
    Private Function AddSpliter(ByVal sender As Object) As Boolean
        Dim SenderName = sender.name & "_Spliter"
        For Each cntrl In (FindControlRecursive(New List(Of Control), sender, New List(Of Type)({GetType(Panel)})))
            If cntrl.Name.Contains("_Spliter") Then Exit Function
        Next
        Dim sender_Spliter As New Panel
        sender_Spliter.BackColor = System.Drawing.Color.DarkSlateGray
        sender_Spliter.Cursor = System.Windows.Forms.Cursors.HSplit
        sender_Spliter.Dock = System.Windows.Forms.DockStyle.Top
        sender_Spliter.Name = SenderName
        sender_Spliter.Height = 3
        sender_Spliter.TabIndex = 1066
        sender.controls.Add(sender_Spliter)
        Form_ToolTip.SetToolTip(sender_Spliter, sender_Spliter.Name)
        sender_Spliter.SendToBack()
        AddHandler CType(sender_Spliter, Control).MouseMove, AddressOf SpliterMouseMove
        AddHandler CType(sender_Spliter, Control).MouseUp, AddressOf SpliterMouseUp
        AddHandler CType(sender_Spliter, Control).MouseDown, AddressOf SpliterMouseDown
        SpltrMsPnt(SpltrMsPnt.Length - 1).RchTxtBx_Name = SenderName
        ReDim Preserve SpltrMsPnt(SpltrMsPnt.Length)
    End Function
    Dim SpltrMsdn As Boolean
    Dim SpltrMsPnt(0) As Spliter
    Structure Spliter
        Public RchTxtBx_Name As String
        Public Spliter_Position As Point
    End Structure
    Dim NewHeight As Integer
    Private Sub SpliterMouseMove(sender As Object, e As MouseEventArgs) Handles Spliter_1_Lbl.MouseMove
        Dim SpliterName = sender.name
        Dim SpltrName As Spliter
        Try
            If Not SpltrMsdn Then Exit Sub
            If SpliterName = Spliter_1_Lbl.Name Then
                GoTo SpliterMouseMove
            End If
            Dim GridPnlName = GridPnl.Name & "_Spliter"
            For Each Spltr In SpltrMsPnt
                If IsNothing(GridPnl) Then Continue For
                If Spltr.RchTxtBx_Name = GridPnlName Then
                    SpltrName = Spltr
                    Exit For
                End If
            Next
SpliterMouseMove:
            If e.Y <> mouseYStart Then
                If e.Y > mouseYStart Then
                    Dim dif = e.Y - mouseYStart
                    If dif = 1 Then Exit Sub
                    If sender.name = Spliter_1_Lbl.Name Then
                        NewHeight = Setting_TbCntrl.Height - dif
                        Setting_TbCntrl.Height = NewHeight
                    ElseIf sender.name <> Spliter_1_Lbl.Name Then
                        NewHeight = RchTxtBx.Height
                        NewHeight += dif
                        RchTxtBx.Height = NewHeight
                    End If
                ElseIf e.Y < mouseYStart Then
                    Dim dif = mouseYStart - e.Y
                    If dif = 1 Then Exit Sub
                    If sender.name = Spliter_1_Lbl.Name Then
                        NewHeight = Setting_TbCntrl.Height + dif
                        Setting_TbCntrl.Height = NewHeight
                    ElseIf sender.name <> Spliter_1_Lbl.Name Then
                        NewHeight = RchTxtBx.Height
                        NewHeight -= dif
                        RchTxtBx.Height = NewHeight
                    End If
                End If
                mouseYStart = e.Y
                Application.DoEvents()
                Me.Invalidate()
            End If
            Debug.Print("NewHeight=" & NewHeight & vbNewLine & "e.Y=" & e.Y & vbNewLine & "mouseYStart=" & mouseYStart)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
        End Try
    End Sub
    Dim mouseYStart As Integer = 0
    Private Sub SpliterMouseDown(sender As Object, e As MouseEventArgs) Handles Spliter_1_Lbl.MouseDown
        Dim SpliterName = sender.name
        mouseYStart = e.Y
        If SpliterName = Spliter_1_Lbl.Name Then
            NewHeight = Setting_TbCntrl.Height
        Else
            NewHeight = RchTxtBx.Height
        End If
        SpltrMsdn = True
        Application.DoEvents()
    End Sub
    Private Sub SpliterMouseUp(sender As Object, e As MouseEventArgs) Handles Spliter_1_Lbl.MouseUp
        SpltrMsdn = False
        Application.DoEvents()
    End Sub

    Public Function AddAvailable_MagNotes_DGVColumns() As Boolean
        Try
            If Available_MagNotes_DGV.Columns.Count > 0 Then Exit Function
            Available_MagNotes_DGV.Columns.Add("MagNote_Name", "MagNote Name")
            Available_MagNotes_DGV.Columns.Add("MagNote_Label", "MagNote Label")
            Available_MagNotes_DGV.Columns.Add("MagNote_Category", "MagNote Category")
            Available_MagNotes_DGV.Columns.Add("MagNote", "MagNote")
            Available_MagNotes_DGV.Columns.Add("Blocked_Note", "Blocked Note")
            Available_MagNotes_DGV.Columns.Add("Finished_Note", "Finished Note")
            Available_MagNotes_DGV.Columns.Add("Note_Font", "Note Font")
            Available_MagNotes_DGV.Columns.Add("Note_Font_Color", "Note Font Color")
            Available_MagNotes_DGV.Columns.Add("Note_Back_Color", "Note Back Color")
            Available_MagNotes_DGV.Columns.Add("Alternating_Row_Color", "Alternating Row Color")
            Available_MagNotes_DGV.Columns.Add("Creation_Date", "Creation Date")
            Available_MagNotes_DGV.Columns.Add("Secured_Note", "Secured Note")
            Available_MagNotes_DGV.Columns.Add("Note_Password", "Note Password")
            Available_MagNotes_DGV.Columns.Add("Use_Main_Password", "Use Main Password")
            Available_MagNotes_DGV.Columns.Add("MagNote_RTF", "MagNote RTF")
            Available_MagNotes_DGV.Columns.Add("Note_Word_Wrap", "Note Word Wrap")
            Available_MagNotes_DGV.Columns.Add("Note_Have_Reminder", "Note Have Reminder")
            Available_MagNotes_DGV.Columns.Add("Next_Reminder_Time", "Next Reminder Time")
            Available_MagNotes_DGV.Columns.Add("Reminder_Every", "Reminder Every")
            Available_MagNotes_DGV.Columns.Add("MagNote_Grid", "MagNote Grid")
            Available_MagNotes_DGV.Columns("MagNote_RTF").Visible = False
            Available_MagNotes_DGV.Columns.Add("Grid_Panel_Size", "Grid Panel Size")
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

#End Region

#End Region

#Region "MagNote"
    Public Sub New()
        InitializeComponent()
        Icon = My.Resources.MagNote
        backgroundWorker.WorkerReportsProgress = True
        backgroundWorker.WorkerSupportsCancellation = True
    End Sub
    Dim HideMagNote As Boolean
    Private streamToPrint As StreamReader
    Dim myWatch As SystemWideHotkeyComponent
    Dim myWatch1 As SystemWideHotkeyComponent
    Dim KeyDownForFirstTime As Boolean = True
    Dim FileToPrint
    Dim SNN_SelectedItem As String = Nothing
    Private WithEvents Tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuDisplayForm As ToolStripMenuItem
    Private WithEvents MeAlwaysOnTop As ToolStripMenuItem
    Private WithEvents CheckForUpdates As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuSep2 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem
    Private WithEvents LockScreen As ToolStripMenuItem
    Private WithEvents Restart As ToolStripMenuItem
    Private WithEvents ShutdownWindows As ToolStripMenuItem
    Private WithEvents RestartWindows As ToolStripMenuItem
    Private WithEvents LogOffUser As ToolStripMenuItem
    Private Function ExitIfMagNoteIsRunning() As Boolean
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
                End If
            End If
        Next
    End Function
    ''' <summary>
    ''' Pressing Alt+s To Show Or Hide Me
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub myHotKeyPressed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            MyFormWindowState(1,,,, 1)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
    Private Declare Function GetWindow Lib "user32" (ByVal hwnd As Long, ByVal wCmd As Long) As Long
    Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long
    Private Declare Function EnableWindow Lib "user32" (ByVal hwnd As Long, ByVal fEnable As Long) As Long

    Private Const GW_CHILD = 5
    Private Const SW_HIDE = 0
    Private Const SW_SHOW = 5
    Sub ShowDesktopIcons(ByVal bVisible As Boolean)
        Dim hWnd_DesktopIcons As Long
        hWnd_DesktopIcons = GetWindow(FindWindow("Progman", "Program Manager"), GW_CHILD)
        If bVisible Then
            ShowWindow(hWnd_DesktopIcons, SW_SHOW)
        Else
            ShowWindow(hWnd_DesktopIcons, SW_HIDE)
        End If
        Application.DoEvents()
    End Sub
    Dim DesctopIconsIsVisible As Boolean = True
    Private Sub HideDesktopIcons(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If DesctopIconsIsVisible Then
                DesctopIconsIsVisible = False
            Else
                DesctopIconsIsVisible = True
            End If
            ShowDesktopIcons(DesctopIconsIsVisible)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub MuteAlerts(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Mute_ChkBx.CheckState = CheckState.Checked Then
                Mute_ChkBx.CheckState = CheckState.Unchecked
            Else
                Mute_ChkBx.CheckState = CheckState.Checked
            End If
            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim UpdateFileName As String = String.Empty
    Dim SourceFileName As String = String.Empty
    Dim DestinationFileName As String = String.Empty
    Dim UpdateFileVersion As String = String.Empty
    Dim UpdateDownloadFilePath As String = String.Empty
    Dim client As New WebClient()
    Private Function Check_For_New_Version(Optional ForceDisplayWindowsNotification As Boolean = False) As Boolean
        Try
            If Debugger.IsAttached Then Exit Function
            If Language_Btn.Text = "E" Then
                Msg = "جارى التحقق من وجود تحديث للبرنامج"
            ElseIf Language_Btn.Text = "ع" Then
                Msg = "Checking To Find New Version Of The Progarm"
            End If
            ShowWindowsNotification(Msg, ForceDisplayWindowsNotification)
            If Not IsInternetAvailable() Then
                Exit Function
            End If

            Dim Password As String = FTP_Login(0).FTP_Password
            Dim UserName As String = FTP_Login(0).FTP_UserName
            Dim USNFI As String = Application.StartupPath & "\UpdateMagNoteFileInformation.txt"
            Try
                Using client As New WebClient()
                    If File.Exists(USNFI) Then
                        File.Delete(USNFI)
                    End If
                    client.Credentials = New NetworkCredential(UserName, Password)
                    client.DownloadFile("ftp://" & FTP_Login(0).FTP_UserName & "@" & FTP_Login(0).FTP_Address & "/UpdateMagNoteFileInformation.txt", USNFI)
                End Using
            Catch ex As Exception
                If Language_Btn.Text = "E" Then
                    Msg = "لم تفلح محاولة التحقق من وجود تحيث للبرنامج"
                ElseIf Language_Btn.Text = "ع" Then
                    Msg = "Couldn't Check To Find New Version Of The Progarm"
                End If
                ShowWindowsNotification(Msg, ForceDisplayWindowsNotification)
                Return False
            End Try
            If File.Exists(USNFI) Then
                Dim MagNote() = Split(Replace(Replace(My.Computer.FileSystem.ReadAllText(USNFI, System.Text.Encoding.UTF8), vbCrLf, ""), vbLf, ""), ",")
                Dim NoteRecordsFinished As Boolean
                For Each Line In MagNote
                    If Microsoft.VisualBasic.Left(Line, 2) = "//" Then Continue For 'Ignore This Line
                    If Line = "DownloadFiles" Then
                        NoteRecordsFinished = True
                        Continue For
                    End If
                    If Not NoteRecordsFinished Then
                        For Each item As String In Line.Split(":")
                            Select Case item
                                Case "Update File Name"
                                    UpdateFileName = Split(Line, ":").ToList.Item(1)
                                Case "Update File Version"
                                    UpdateFileVersion = Split(Line, ":").ToList.Item(1)
                                Case "Update Download File Path"
                                    UpdateDownloadFilePath = Split(Line, ":").ToList.Item(1)
                            End Select
                        Next
                    Else
                        For Each item As String In Line.Split(":")
                            Select Case item
                                Case "Source File Name"
                                    SourceFileName = Split(Line, ":").ToList.Item(1)
                                Case "Destination File Name"
                                    DestinationFileName = Split(Line, ":").ToList.Item(1)
                            End Select
                        Next
                        If Not String.IsNullOrEmpty(SourceFileName) And
                            Not String.IsNullOrEmpty(DestinationFileName) And
                            UpdateIsAvailable() Then
                            DownloadFile(SourceFileName, DestinationFileName)
                            SourceFileName = String.Empty
                            DestinationFileName = String.Empty
                        End If
                    End If
                Next
                If UpdateIsAvailable() Then
                    If Language_Btn.Text = "E" Then
                        Msg = "تمت بنجاح عملية محاولة التحقق من وجود تحيث للبرنامج"
                    ElseIf Language_Btn.Text = "ع" Then
                        Msg = "Successfully Checked To Find New Version Of The Progarm"
                    End If
                    ShowWindowsNotification(Msg, ForceDisplayWindowsNotification)
                    Return True
                Else
                    If Language_Btn.Text = "E" Then
                        Msg = "انت تستخدم آخر إصدار متاح للبرنامج"
                    ElseIf Language_Btn.Text = "ع" Then
                        Msg = "You Are Using The Last Available Version Of The Program"
                    End If
                    ShowMsg(Msg & vbNewLine, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0,,,,, ForceDisplayWindowsNotification)
                End If
            End If
        Catch ex As Exception
            If Language_Btn.Text = "E" Then
                Msg = "لم تفلح محاولة التحقق من وجود تحيث للبرنامج"
            ElseIf Language_Btn.Text = "ع" Then
                Msg = "Couldn't Check To Find New Version Of The Progarm"
            End If
            ShowMsg(Msg & vbNewLine & ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0)
            Return False
        Finally
            client.Dispose()
        End Try
    End Function
    Private Function UpdateIsAvailable() As Boolean
        Dim Update_Version_TxtBx As New TextBox
        Dim CurrentVersionSplit() = Split(My.Application.Info.Version.ToString, ".")
        Dim UpgradeVersionSplit() = Split(UpdateFileVersion, ".")
        Dim Seq = 0
        For Each Value In CurrentVersionSplit
            If Val(CurrentVersionSplit(Seq)) < Val(UpgradeVersionSplit(Seq)) Then
                Return True
            End If
            Seq += 1
        Next
    End Function
    Private Function DownloadFile(ByVal SourcesFile, ByVal DestinationFile) As Boolean
        Try
            If DestinationFile.ToString.Contains("Application.StartupPath") Then
                DestinationFile = Replace(DestinationFile, "Application.StartupPath", Application.StartupPath)
            End If
            If File.Exists(DestinationFile) Then
                My.Computer.FileSystem.DeleteFile(DestinationFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            End If

            Dim Password As String = FTP_Login(0).FTP_Password
            Dim UserName As String = FTP_Login(0).FTP_UserName
            Using WClient As New WebClient()
                WClient.Credentials = New NetworkCredential(UserName, Password)
                WClient.DownloadFileAsync(New Uri("ftp://" & FTP_Login(0).FTP_UserName & "@" & FTP_Login(0).FTP_Address & "/" & SourcesFile), DestinationFile)
            End Using
        Catch ex As Exception
        End Try
    End Function
    Private Function CreateMagNoteSettingFile(ByVal FileName As String) As Boolean
        Try
            Dim ApplicationFolder As String = Replace(Application.StartupPath, ":", "(Instead Of Colon)")
            Dim TextToWrite As String = ":Current_Language -(E)-:Current_MagNote_Name -(MagNote -(0)-," & ApplicationFolder & "\MagNotes_Files\MagNote -(0)-.txt,Visual Studio Code Category)-:Run_Me_At_Windows_Startup -(1)-:Me_Always_On_Top -(0)-:Hide_Finished_MagNote -(1)-:Save_Setting_When_Exit -(1)-:Note_Form_Opacity -(0.79)-:Periodically_Backup_MagNotes -(1)-:Backup_Time -(1,0,0)-:Next_Backup_Time -(02/03/2024 8,51,07 PM)-:Reload_MagNotes_After_Amendments -(0)-:Enter_Password_To_Pass -(0)-:Complex_Password -(0)-:Main_Password -()-:Set_Control_To_Fill -(1)-:Warning_Before_Save -(0)-:Warning_Before_Delete -(0)-:Double_Click_To_Run_Shortcut -(0)-:Keep_Note_Opened_After_Delete -(0)-:Hide_Windows_Desktop_Icons -(0)-:Form_Is_Restricted_By_Screen_Bounds -(0)-:Ask_If_Form_Is_Outside_Screen_Bounds -(1)-:Enable_Maximize_Box -(0)-:Remember_Opened_External_Files -(1)-:Show_Note_Tab_Control -(1)-:Me_Is_Compressed -(2)-:Minimize_After_Running_My_Shortcut -(0)-:Me_As_Default_Text_File_Editor -(0)-:Run_Me_As_Administrator -(0)-:Application_Starts_Minimized -(2)-:Save_Day_Light -(0)-:Country -(مصر)-:City -(القاهرة)-:Calculation_Methods -(مصر)-:Fagr_Voice_Files -(الفجر 1)-:Voice_Azan_Files -(محمد على البنا)-:Alert_File_Path -(" & ApplicationFolder & "\Azan Voices\Alerts\Allah Song Mp3 - Subhanallah - Ayisha Abdul Basith ! Islamic Arabic.mp3)-:Stop_Displaying_Controls_ToolTip -(1)-:Activate_Windows_Notification_Tray -(1)-:MagNotes_Folder_Path -(" & ApplicationFolder & "\MagNotes_Files)-:Backup_Folder_Path -(" & ApplicationFolder & "\Note_Backup_Folder)-:Open_Note_In_New_Tab -(1)-:Show_Control_Tab_Pages_In_Multi_Line -(1)-:Load_MagNote_At_Startup -(0)-:Azan_Spoke_Method -(0)-:Azan_Activation -(0)-:Alert_Before_Azan -(1)-:Time_To_Alert_Before_Azan -(20)-:Azan_Takbeer_Only -(0)-:Reload_MagNote_After_Change_Category -(0)-:Show_Form_Border_Style -(1)-:Form_Color_Like_Note -(1)-:Note_Form_Color -(RoyalBlue)-:Note_Form_Size -({Width=1049, Height=789})-:Setting_TabContrl_Size -({Width=0, Height=230})-:Note_Form_Location -({X=407,Y=0})-:Check_For_New_Version -(0)-:Remember_Opened_Notes_When_Close -(1)-:Apply_Multiple_New_Files -(1)-:Activate_Projects_TabPage -(0)-:Activate_MagNotes_TabPage -(1)-:Activate_Note_Parameters_TabPage -(1)-:Activate_ShortCut_TabPage -(1)-:Activate_Prayer_Time_TabPage -(1)-:Activate_Alert_Time_TabPage -(1)-:Ignore_Error_Message_For_Connection -(1)-:Force_Stop_Playing_Current_Sound_File -(0)-:Force_Activate_TabPage_When_Alert_Is_Active -(0)-:Activate_Alert_Function -(0)-:Immediately_Update_Form_Parameters -(0)"
            My.Computer.FileSystem.WriteAllText(FileName, TextToWrite, 0, System.Text.Encoding.UTF8)

            Dim FileToEdit As String = Application.StartupPath & "\MagNotes_Files\MagNote -(0)-.txt"
            If File.Exists(FileToEdit) Then
                TextToWrite = My.Computer.FileSystem.ReadAllText(FileToEdit, System.Text.Encoding.UTF8)
                TextToWrite = Replace(TextToWrite, "MagNote_InstallationFolders", ApplicationFolder)
                My.Computer.FileSystem.WriteAllText(FileToEdit, TextToWrite, 0, System.Text.Encoding.UTF8)
            End If
            FileToEdit = Application.StartupPath & "\MagNotes_Files\OpenedTabPages.txt"
            If File.Exists(FileToEdit) Then
                TextToWrite = My.Computer.FileSystem.ReadAllText(FileToEdit, System.Text.Encoding.UTF8)
                TextToWrite = Replace(TextToWrite, "MagNote_InstallationFolders", Application.StartupPath)
                My.Computer.FileSystem.WriteAllText(FileToEdit, TextToWrite, 0, System.Text.Encoding.UTF8)
            End If

            FileToEdit = Application.StartupPath & "\MagNotes_Files\Categories.xml"
            If File.Exists(FileToEdit) Then
                TextToWrite = My.Computer.FileSystem.ReadAllText(FileToEdit, System.Text.Encoding.UTF8)
                TextToWrite = Replace(TextToWrite, "MagNote_InstallationFolders", Application.StartupPath)
                My.Computer.FileSystem.WriteAllText(FileToEdit, TextToWrite, 0, System.Text.Encoding.UTF8)
            End If

            ShowMsg("Setting And Help Files Are Created Correctly", "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)

            Return True
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Dim CurrentMagNoteName As String
    Private Sub SetAppParameters()

        Try
#Region "Read Parameters Setting"
            Dim FileName
            If Debugger.IsAttached Then
                FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\MagNote_Setting.txt"
            Else
                FileName = Application.StartupPath & "\MagNote_Setting.txt"
            End If

            If Not File.Exists(FileName) Then
                If Not CreateMagNoteSettingFile(FileName) Then
                    Exit Sub
                End If
            End If

            Dim Me_Always_On_Top_Value, Hide_Finished_MagNote_Value, Run_Me_At_Windows_Startup_Value, Form_Color_Like_Note_Value, Save_Setting_When_Exit_Value, Note_Font_Value, Note_Form_Color_Value, Current_Language_Value, MagNote_No_Value, Note_Form_Size_Value, Setting_Tab_Control_Size_Value, Grid_Panel_Size_Value, Note_Form_Location_Value, Note_Form_Opacity_Value, Periodically_Backup_MagNotes_Value, Backup_Time_Value, Next_Backup_Time_Value, Reload_MagNotes_After_Amendments_Value, Enter_Password_To_Pass_Value, Complex_Password_Value, Main_Password_Value, Set_Control_To_Fill_Value, Warning_Before_Save_Value, Warning_Before_Delete_Value, Double_Click_To_Run_Shortcut_Value, Show_Form_Border_Style_Value, Enable_Maximize_Box_Value, Show_Note_Tab_Control_Value, Me_Is_Compressed_Value, Minimize_After_Running_My_Shortcut_Value, Me_As_Default_Text_File_Editor_Value, Run_Me_As_Administrator_Value, Remember_Opened_External_Files_Value, Country_Value, City_Value, Calculation_Methods_Value, Save_Day_Light_Value, Stop_Displaying_Controls_ToolTip_Value, MagNotes_Folder_Path_Value, Backup_Folder_Path_Value, Open_Note_In_New_Tab_Value, Show_Control_Tab_Pages_In_Multi_Line_Value, Load_MagNote_At_Startup_Value, Check_For_New_Version_Value, Reload_MagNote_After_Change_Category_Value, Application_Starts_Minimized_Value, Fagr_Voice_Files_Value, Voice_Azan_Files_Value, Azan_Spoke_Method_Value, Azan_Activation_Value, Azan_Takbeer_Only_Value, Alert_Before_Azan_Value, Time_To_Alert_Before_Azan_Value, Alert_File_Path_Value, Keep_Note_Opened_After_Delete_Value, Hide_Windows_Desktop_Icons_Value, Form_Is_Restricted_By_Screen_Bounds_Value, Ask_If_Form_Is_Outside_Screen_Bounds_Value, Remember_Opened_Notes_When_Close_Value, Apply_Multiple_New_Files_Value, Activate_Projects_TabPage_Value, Activate_MagNotes_TabPage_Value, Activate_Note_Parameters_TabPage_Value, Activate_ShortCut_TabPage_Value, Activate_Prayer_Time_TabPage_Value, Activate_Alert_Time_TabPage_Value, Ignore_Error_Message_For_Connection_Value, Force_Stop_Playing_Current_Sound_File_Value, Force_Activate_TabPage_When_Alert_Is_Active_Value, Activate_Windows_Notification_Tray_Value, Activate_Alert_Function_Value, Immediately_Update_Form_Parameters_Value

            Application.DoEvents()
            If File.Exists(FileName) Then
                Dim MagNote() = Split(Replace(My.Computer.FileSystem.ReadAllText(FileName, System.Text.Encoding.UTF8), vbCrLf, ""), ":")
                Dim ReadNote As String = String.Empty
                Dim CaseNote As String = String.Empty
                For Each Note In MagNote
                    If String.IsNullOrEmpty(Note) Then Continue For
                    If Microsoft.VisualBasic.Left(Note, Len("Current_MagNote_Name -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Current_MagNote_Name -()-" Then
                        ReadNote = "Current_MagNote_Name"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Hide_Finished_MagNote -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Hide_Finished_MagNote -()-" Then
                        ReadNote = "Hide_Finished_MagNote"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Me_Always_On_Top -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Me_Always_On_Top -()-" Then
                        ReadNote = "Me_Always_On_Top"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Run_Me_At_Windows_Startup -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Run_Me_At_Windows_Startup -()-" Then
                        ReadNote = "Run_Me_At_Windows_Startup"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Form_Color_Like_Note -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Form_Color_Like_Note -()-" Then
                        ReadNote = "Form_Color_Like_Note"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Save_Setting_When_Exit -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Save_Setting_When_Exit -()-" Then
                        ReadNote = "Save_Setting_When_Exit"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Form_Color -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Form_Color -()-" Then
                        ReadNote = "Note_Form_Color"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Current_Language -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Current_Language -()-" Then
                        ReadNote = "Current_Language"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Current_MagNote_Name -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Current_MagNote_Name -()-" Then
                        ReadNote = "Current_MagNote_Name"

                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Setting_Tab_Control_Size -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Setting_Tab_Control_Size -()-" Then
                        ReadNote = "Setting_Tab_Control_Size"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Grid_Panel_Size -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Grid_Panel_Size -()-" Then
                        ReadNote = "Grid_Panel_Size"

                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Form_Location -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Form_Location -()-" Then
                        ReadNote = "Note_Form_Location"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Form_Opacity -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Form_Opacity -()-" Then
                        ReadNote = "Note_Form_Opacity"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Periodically_Backup_MagNotes -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Periodically_Backup_MagNotes -()-" Then
                        ReadNote = "Periodically_Backup_MagNotes"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Backup_Time -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Backup_Time -()-" Then
                        ReadNote = "Backup_Time"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Next_Backup_Time -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Next_Backup_Time -()-" Then
                        ReadNote = "Next_Backup_Time"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Reload_MagNotes_After_Amendments -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Reload_MagNotes_After_Amendments -()-" Then
                        ReadNote = "Reload_MagNotes_After_Amendments"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Enter_Password_To_Pass -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Enter_Password_To_Pass -()-" Then
                        ReadNote = "Enter_Password_To_Pass"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Complex_Password -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Complex_Password -()-" Then
                        ReadNote = "Complex_Password"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Main_Password -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Main_Password -()-" Then
                        ReadNote = "Main_Password"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Set_Control_To_Fill -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Set_Control_To_Fill -()-" Then
                        ReadNote = "Set_Control_To_Fill"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Warning_Before_Save -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Warning_Before_Save -()-" Then
                        ReadNote = "Warning_Before_Save"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Warning_Before_Delete -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Warning_Before_Delete -()-" Then
                        ReadNote = "Warning_Before_Delete"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Double_Click_To_Run_Shortcut -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Double_Click_To_Run_Shortcut -()-" Then
                        ReadNote = "Double_Click_To_Run_Shortcut"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Show_Form_Border_Style -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Show_Form_Border_Style -()-" Then
                        ReadNote = "Show_Form_Border_Style"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Keep_Note_Opened_After_Delete -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Keep_Note_Opened_After_Delete -()-" Then
                        ReadNote = "Keep_Note_Opened_After_Delete"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Hide_Windows_Desktop_Icons -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Hide_Windows_Desktop_Icons -()-" Then
                        ReadNote = "Hide_Windows_Desktop_Icons"

                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Form_Is_Restricted_By_Screen_Bounds -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Form_Is_Restricted_By_Screen_Bounds -()-" Then
                        ReadNote = "Form_Is_Restricted_By_Screen_Bounds"

                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Ask_If_Form_Is_Outside_Screen_Bounds -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Ask_If_Form_Is_Outside_Screen_Bounds -()-" Then
                        ReadNote = "Ask_If_Form_Is_Outside_Screen_Bounds"

                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Enable_Maximize_Box -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Enable_Maximize_Box -()-" Then
                        ReadNote = "Enable_Maximize_Box"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Remember_Opened_External_Files -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Remember_Opened_External_Files -()-" Then
                        ReadNote = "Remember_Opened_External_Files"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Show_Note_Tab_Control -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Show_Note_Tab_Control -()-" Then
                        ReadNote = "Show_Note_Tab_Control"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Me_Is_Compressed -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Me_Is_Compressed -()-" Then
                        ReadNote = "Me_Is_Compressed"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Minimize_After_Running_My_Shortcut -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Minimize_After_Running_My_Shortcut -()-" Then
                        ReadNote = "Minimize_After_Running_My_Shortcut"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Me_As_Default_Text_File_Editor -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Me_As_Default_Text_File_Editor -()-" Then
                        ReadNote = "Me_As_Default_Text_File_Editor"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Run_Me_As_Administrator -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Run_Me_As_Administrator -()-" Then
                        ReadNote = "Run_Me_As_Administrator"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Application_Starts_Minimized -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Application_Starts_Minimized -()-" Then
                        ReadNote = "Application_Starts_Minimized"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Save_Day_Light -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Save_Day_Light -()-" Then
                        ReadNote = "Save_Day_Light"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Country -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Country -()-" Then
                        ReadNote = "Country"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("City -(")) & Microsoft.VisualBasic.Right(Note, 2) = "City -()-" Then
                        ReadNote = "City"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Calculation_Methods -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Calculation_Methods -()-" Then
                        ReadNote = "Calculation_Methods"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Fagr_Voice_Files -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Fagr_Voice_Files -()-" Then
                        ReadNote = "Fagr_Voice_Files"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Voice_Azan_Files -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Voice_Azan_Files -()-" Then
                        ReadNote = "Voice_Azan_Files"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Alert_File_Path -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Alert_File_Path -()-" Then
                        ReadNote = "Alert_File_Path"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Stop_Displaying_Controls_ToolTip -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Stop_Displaying_Controls_ToolTip -()-" Then
                        ReadNote = "Stop_Displaying_Controls_ToolTip"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_Windows_Notification_Tray -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_Windows_Notification_Tray -()-" Then
                        ReadNote = "Activate_Windows_Notification_Tray"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("MagNotes_Folder_Path -(")) & Microsoft.VisualBasic.Right(Note, 2) = "MagNotes_Folder_Path -()-" Then
                        ReadNote = "MagNotes_Folder_Path"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Backup_Folder_Path -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Backup_Folder_Path -()-" Then
                        ReadNote = "Backup_Folder_Path"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Open_Note_In_New_Tab -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Open_Note_In_New_Tab -()-" Then
                        ReadNote = "Open_Note_In_New_Tab"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Show_Control_Tab_Pages_In_Multi_Line -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Show_Control_Tab_Pages_In_Multi_Line -()-" Then
                        ReadNote = "Show_Control_Tab_Pages_In_Multi_Line"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Load_MagNote_At_Startup -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Load_MagNote_At_Startup -()-" Then
                        ReadNote = "Load_MagNote_At_Startup"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Check_For_New_Version -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Check_For_New_Version -()-" Then
                        ReadNote = "Check_For_New_Version"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Remember_Opened_Notes_When_Close -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Remember_Opened_Notes_When_Close -()-" Then
                        ReadNote = "Remember_Opened_Notes_When_Close"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Apply_Multiple_New_Files -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Apply_Multiple_New_Files -()-" Then
                        ReadNote = "Apply_Multiple_New_Files"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_Projects_TabPage -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_Projects_TabPage -()-" Then
                        ReadNote = "Activate_Projects_TabPage"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_MagNotes_TabPage -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_MagNotes_TabPage -()-" Then
                        ReadNote = "Activate_MagNotes_TabPage"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_Note_Parameters_TabPage -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_Note_Parameters_TabPage -()-" Then
                        ReadNote = "Activate_Note_Parameters_TabPage"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_ShortCut_TabPage -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_ShortCut_TabPage -()-" Then
                        ReadNote = "Activate_ShortCut_TabPage"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_Prayer_Time_TabPage -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_Prayer_Time_TabPage -()-" Then
                        ReadNote = "Activate_Prayer_Time_TabPage"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_Alert_Time_TabPage -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_Alert_Time_TabPage -()-" Then
                        ReadNote = "Activate_Alert_Time_TabPage"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Ignore_Error_Message_For_Connection -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Ignore_Error_Message_For_Connection -()-" Then
                        ReadNote = "Ignore_Error_Message_For_Connection"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Force_Stop_Playing_Current_Sound_File -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Force_Stop_Playing_Current_Sound_File -()-" Then
                        ReadNote = "Force_Stop_Playing_Current_Sound_File"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Force_Activate_TabPage_When_Alert_Is_Active -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Force_Activate_TabPage_When_Alert_Is_Active -()-" Then
                        ReadNote = "Force_Activate_TabPage_When_Alert_Is_Active"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Immediately_Update_Form_Parameters -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Immediately_Update_Form_Parameters -()-" Then
                        ReadNote = "Immediately_Update_Form_Parameters"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Activate_Alert_Function -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Activate_Alert_Function -()-" Then
                        ReadNote = "Activate_Alert_Function"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Azan_Spoke_Method -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Azan_Spoke_Method -()-" Then
                        ReadNote = "Azan_Spoke_Method"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Azan_Activation -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Azan_Activation -()-" Then
                        ReadNote = "Azan_Activation"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Alert_Before_Azan -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Alert_Before_Azan -()-" Then
                        ReadNote = "Alert_Before_Azan"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Time_To_Alert_Before_Azan -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Time_To_Alert_Before_Azan -()-" Then
                        ReadNote = "Time_To_Alert_Before_Azan"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Azan_Takbeer_Only -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Azan_Takbeer_Only -()-" Then
                        ReadNote = "Azan_Takbeer_Only"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Reload_MagNote_After_Change_Category -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Reload_MagNote_After_Change_Category -()-" Then
                        ReadNote = "Reload_MagNote_After_Change_Category"
                    ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Form_Size -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Form_Size -()-" Then
                        ReadNote = "Note_Form_Size"
                    End If
                    Try
                        Note = Replace(Note, "(Instead Of Colon)", ":")
                        Select Case ReadNote
                            Case "Current_MagNote_Name"
                                CurrentMagNoteName = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Current_MagNote_Name -(")), "")
                                CurrentMagNoteName = Microsoft.VisualBasic.Left(CurrentMagNoteName, CurrentMagNoteName.Length - 2)
                                ReadNote = String.Empty
                            Case "Me_Always_On_Top"
                                Me_Always_On_Top_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Me_Always_On_Top -(")), "")
                                Me_Always_On_Top_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Me_Always_On_Top_Value, Me_Always_On_Top_Value.Length - 2).ToString), CheckState)
                                ReadNote = String.Empty
                            Case "Hide_Finished_MagNote"
                                Hide_Finished_MagNote_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Hide_Finished_MagNote -(")), "")
                                Hide_Finished_MagNote_Value = Microsoft.VisualBasic.Left(Hide_Finished_MagNote_Value, Hide_Finished_MagNote_Value.Length - 2)
                                Hide_Finished_MagNote_ChkBx.CheckState = CType(Val(Hide_Finished_MagNote_Value).ToString, CheckState)
                                ReadNote = String.Empty
                            Case "Run_Me_At_Windows_Startup"
                                Run_Me_At_Windows_Startup_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Run_Me_At_Windows_Startup -(")), "")
                                Run_Me_At_Windows_Startup_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Run_Me_At_Windows_Startup_Value, Run_Me_At_Windows_Startup_Value.Length - 2).ToString), CheckState)
                                ReadNote = String.Empty
                            Case "Form_Color_Like_Note"
                                Form_Color_Like_Note_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Form_Color_Like_Note -(")), "")
                                Form_Color_Like_Note_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Form_Color_Like_Note_Value, Form_Color_Like_Note_Value.Length - 2).ToString), CheckState)
                                ReadNote = String.Empty
                            Case "Save_Setting_When_Exit"
                                Save_Setting_When_Exit_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Save_Setting_When_Exit -(")), "")
                                Save_Setting_When_Exit_ChkBx.CheckState = CType(Val(Microsoft.VisualBasic.Left(Save_Setting_When_Exit_Value, Save_Setting_When_Exit_Value.Length - 2).ToString), CheckState)
                                ReadNote = String.Empty
                            Case "Note_Form_Color"
                                Note_Form_Color_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Form_Color -(")), "")
                                Note_Form_Color_ClrCmbBx.Text = Microsoft.VisualBasic.Left(Note_Form_Color_Value, Note_Form_Color_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Current_Language"
                                Current_Language_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Current_Language -(")), "")
                                Language_Btn.Text = Microsoft.VisualBasic.Left(Current_Language_Value, Current_Language_Value.Length - 2)
                                If Language_Btn.Text = "E" Then
                                    Language_Btn.Text = "ع"
                                ElseIf Language_Btn.Text = "ع" Then
                                    Language_Btn.Text = "E"
                                End If
                                Language_Btn_Click(Language_Btn, EventArgs.Empty)
                                ReadNote = String.Empty
                            Case "Current_MagNote_Name"
                                MagNote_No_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Current_MagNote_Name -(")), "")
                                If Not RunAsExternal() Then
                                    MagNote_No_CmbBx.Text = Split(Microsoft.VisualBasic.Left(MagNote_No_Value, MagNote_No_Value.Length - 2), ",").ToList.Item(0)
                                    Try
                                        OpenCertainFile = Split(Microsoft.VisualBasic.Left(MagNote_No_Value, MagNote_No_Value.Length - 2), ",").ToList.Item(1)
                                        MagNote_Category_CmbBx.Text = Split(Microsoft.VisualBasic.Left(MagNote_No_Value, MagNote_No_Value.Length - 2), ",").ToList.Item(2)
                                    Catch ex As Exception
                                    End Try
                                End If
                                ReadNote = String.Empty
                            Case "Note_Form_Size"
                                Note_Form_Size_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Form_Size -(")), "")
                                Note_Form_Size_Value = Microsoft.VisualBasic.Left(Note_Form_Size_Value, Note_Form_Size_Value.Length - 2)
                                Note_Form_Size_TxtBx.Text = Note_Form_Size_Value
                                Me.Size = ReturnSize(Note_Form_Size_Value)
                                ReadNote = String.Empty
                            Case "Setting_Tab_Control_Size"
                                Setting_Tab_Control_Size_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Setting_Tab_Control_Size -(")), "")
                                Setting_Tab_Control_Size_Value = Microsoft.VisualBasic.Left(Setting_Tab_Control_Size_Value, Setting_Tab_Control_Size_Value.Length - 2)
                                Setting_Tab_Control_Size_TxtBx.Text = Setting_Tab_Control_Size_Value
                                Setting_Tab_Control_Size_Value = ReturnSize(Setting_Tab_Control_Size_Value)
                                Dim TCSize = ReturnSize(Setting_Tab_Control_Size_TxtBx.Text)
                                Setting_TbCntrl.Size = TCSize
                                ReadNote = String.Empty
                            Case "Note_Form_Location"
                                Note_Form_Location_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Form_Location -(")), "")
                                Note_Form_Location_Value = Microsoft.VisualBasic.Left(Note_Form_Location_Value, Note_Form_Location_Value.Length - 2)
                                Note_Form_Location_TxtBx.Text = Note_Form_Location_Value
                                Note_Form_Location_Value = Replace(Replace(Replace(Note_Form_Location_Value, "{X=", ""), "Y=", ""), "}", "")
                                If Not IsNothing(Note_Form_Location_Value) Then
                                    If Not CheckFormLocation(Note_Form_Location_Value) Then
                                        Location = New Point(Split(Note_Form_Location_Value, ",").ToList(0), Split(Note_Form_Location_Value, ",").ToList(1))
                                    End If
                                Else
                                    Note_Form_Location_TxtBx.Text = "{X=0,Y=0}"
                                End If
                                ReadNote = String.Empty
                            Case "Note_Form_Opacity"
                                Note_Form_Opacity_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Form_Opacity -(")), "")
                                Note_Form_Opacity_Value = Microsoft.VisualBasic.Left(Note_Form_Opacity_Value, Note_Form_Opacity_Value.Length - 2)
                                Note_Form_Opacity_TxtBx.Text = Note_Form_Opacity_Value
                                If (Note_Form_Opacity_Value >= Form_Transparency_TrkBr.Minimum) And
                                    (Note_Form_Opacity_Value <= Form_Transparency_TrkBr.Maximum) Then
                                    Form_Transparency_TrkBr.Value = Note_Form_Opacity_Value * 100
                                Else
                                    Form_Transparency_TrkBr.Value = Form_Transparency_TrkBr.Maximum
                                End If
                                ReadNote = String.Empty
                            Case "Periodically_Backup_MagNotes"
                                Periodically_Backup_MagNotes_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Periodically_Backup_MagNotes -(")), "")
                                Periodically_Backup_MagNotes_ChkBx.CheckState = Microsoft.VisualBasic.Left(Periodically_Backup_MagNotes_Value, Periodically_Backup_MagNotes_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Backup_Time"
                                Backup_Time_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Backup_Time -(")), "")
                                Backup_Time_Value = Microsoft.VisualBasic.Left(Backup_Time_Value, Backup_Time_Value.Length - 2)
                                Days_NmrcUpDn.Value = Split(Backup_Time_Value, ",").ToList(0)
                                Hours_NmrcUpDn.Value = Split(Backup_Time_Value, ",").ToList(1)
                                Minutes_NmrcUpDn.Value = Split(Backup_Time_Value, ",").ToList(2)
                                ReadNote = String.Empty
                                Application.DoEvents()
                            Case "Next_Backup_Time"
                                Next_Backup_Time_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Next_Backup_Time -(")), "")
                                Next_Backup_Time_TxtBx.Text = Replace(Microsoft.VisualBasic.Left(Next_Backup_Time_Value, Next_Backup_Time_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Reload_MagNotes_After_Amendments"
                                Reload_MagNotes_After_Amendments_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Reload_MagNotes_After_Amendments -(")), "")
                                Reload_MagNotes_After_Amendments_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Reload_MagNotes_After_Amendments_Value, Reload_MagNotes_After_Amendments_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Enter_Password_To_Pass"
                                Enter_Password_To_Pass_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Enter_Password_To_Pass -(")), "")
                                Enter_Password_To_Pass_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Enter_Password_To_Pass_Value, Enter_Password_To_Pass_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Complex_Password"
                                Complex_Password_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Complex_Password -(")), "")
                                Complex_Password_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Complex_Password_Value, Complex_Password_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Main_Password"
                                Main_Password_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Main_Password -(")), "")
                                Main_Password_Value = Replace(Microsoft.VisualBasic.Left(Main_Password_Value, Main_Password_Value.Length - 2), ",", ":")
                                Main_Password_TxtBx.Text = Main_Password_Value
                                If Main_Password_TxtBx.TextLength > 0 Then
                                    Main_Password_TxtBx.Text = Decrypt_Function(Main_Password_Value)
                                End If
                                ReadNote = String.Empty
                            Case "Set_Control_To_Fill"
                                Set_Control_To_Fill_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Set_Control_To_Fill -(")), "")
                                ReadNote = String.Empty
                            Case "Warning_Before_Save"
                                Warning_Before_Save_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Warning_Before_Save -(")), "")
                                Warning_Before_Save_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Warning_Before_Save_Value, Warning_Before_Save_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Warning_Before_Delete"
                                Warning_Before_Delete_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Warning_Before_Delete -(")), "")
                                Warning_Before_Delete_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Warning_Before_Delete_Value, Warning_Before_Delete_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Double_Click_To_Run_Shortcut"
                                Double_Click_To_Run_Shortcut_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Double_Click_To_Run_Shortcut -(")), "")
                                Double_Click_To_Run_Shortcut_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Double_Click_To_Run_Shortcut_Value, Double_Click_To_Run_Shortcut_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Show_Form_Border_Style"
                                Show_Form_Border_Style_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Show_Form_Border_Style -(")), "")
                                Show_Form_Border_Style_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Show_Form_Border_Style_Value, Show_Form_Border_Style_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Keep_Note_Opened_After_Delete"
                                Keep_Note_Opened_After_Delete_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Keep_Note_Opened_After_Delete -(")), "")
                                Keep_Note_Opened_After_Delete_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Keep_Note_Opened_After_Delete_Value, Keep_Note_Opened_After_Delete_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Hide_Windows_Desktop_Icons"
                                Hide_Windows_Desktop_Icons_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Hide_Windows_Desktop_Icons -(")), "")
                                Hide_Windows_Desktop_Icons_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Hide_Windows_Desktop_Icons_Value, Hide_Windows_Desktop_Icons_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Form_Is_Restricted_By_Screen_Bounds"
                                Form_Is_Restricted_By_Screen_Bounds_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Form_Is_Restricted_By_Screen_Bounds -(")), "")
                                Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Form_Is_Restricted_By_Screen_Bounds_Value, Form_Is_Restricted_By_Screen_Bounds_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Ask_If_Form_Is_Outside_Screen_Bounds"
                                Ask_If_Form_Is_Outside_Screen_Bounds_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Ask_If_Form_Is_Outside_Screen_Bounds -(")), "")
                                Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Ask_If_Form_Is_Outside_Screen_Bounds_Value, Ask_If_Form_Is_Outside_Screen_Bounds_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Enable_Maximize_Box"
                                Enable_Maximize_Box_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Enable_Maximize_Box -(")), "")
                                Enable_Maximize_Box_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Enable_Maximize_Box_Value, Enable_Maximize_Box_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Remember_Opened_External_Files"
                                Remember_Opened_External_Files_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Remember_Opened_External_Files -(")), "")
                                Remember_Opened_External_Files_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Remember_Opened_External_Files_Value, Remember_Opened_External_Files_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Show_Note_Tab_Control"
                                Show_Note_Tab_Control_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Show_Note_Tab_Control -(")), "")
                                Show_Note_Tab_Control_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Show_Note_Tab_Control_Value, Show_Note_Tab_Control_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Me_Is_Compressed"
                                Me_Is_Compressed_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Me_Is_Compressed -(")), "")
                                Me_Is_Compressed_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Me_Is_Compressed_Value, Me_Is_Compressed_Value.Length - 2), ",", ":")
                                Me_Is_Compressed_ChkBx_CheckStateChanged(Me_Is_Compressed_ChkBx, EventArgs.Empty)
                                ReadNote = String.Empty
                            Case "Minimize_After_Running_My_Shortcut"
                                Minimize_After_Running_My_Shortcut_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Minimize_After_Running_My_Shortcut -(")), "")
                                Minimize_After_Running_My_Shortcut_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Minimize_After_Running_My_Shortcut_Value, Minimize_After_Running_My_Shortcut_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Me_As_Default_Text_File_Editor"
                                Me_As_Default_Text_File_Editor_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Me_As_Default_Text_File_Editor -(")), "")
                                Me_As_Default_Text_File_Editor_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Me_As_Default_Text_File_Editor_Value, Me_As_Default_Text_File_Editor_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Run_Me_As_Administrator"
                                Run_Me_As_Administrator_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Run_Me_As_Administrator -(")), "")
                                Run_Me_As_Administrator_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Run_Me_As_Administrator_Value, Run_Me_As_Administrator_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Application_Starts_Minimized"
                                Application_Starts_Minimized_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Application_Starts_Minimized -(")), "")
                                Application_Starts_Minimized_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Application_Starts_Minimized_Value, Application_Starts_Minimized_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Save_Day_Light"
                                Save_Day_Light_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Save_Day_Light -(")), "")
                                Save_Day_Light_ChkBx.CheckState = Microsoft.VisualBasic.Left(Save_Day_Light_Value, Save_Day_Light_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Country"
                                Country_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Country -(")), "")
                                Country_Value = Replace(Country_Value, Microsoft.VisualBasic.Right(Country_Value, Len(")-")), "")
                                Country_CmbBx.Text = Country_Value
                                Dim CurrentActiveContrl = ActiveControl
                                Country_CmbBx.Focus()
                                Me.ActiveControl = Country_CmbBx
                                Country_CmbBx_SelectedIndexChanged(Country_CmbBx, EventArgs.Empty)
                                Me.ActiveControl = CurrentActiveContrl
                                ReadNote = String.Empty
                                Application.DoEvents()
                            Case "City"
                                City_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("City -(")), "")
                                City_Value = Replace(City_Value, Microsoft.VisualBasic.Right(City_Value, Len(")-")), "")
                                City_CmbBx.Text = City_Value
                                City_CmbBx_SelectedIndexChanged(City_CmbBx, EventArgs.Empty)
                                ReadNote = String.Empty
                            Case "Calculation_Methods"
                                Calculation_Methods_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Calculation_Methods -(")), "")
                                Calculation_Methods_CmbBx.Text = Microsoft.VisualBasic.Left(Calculation_Methods_Value, Calculation_Methods_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Fagr_Voice_Files"
                                Fagr_Voice_Files_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Fagr_Voice_Files -(")), "")
                                Fagr_Voice_Files_CmbBx.Text = Microsoft.VisualBasic.Left(Fagr_Voice_Files_Value, Fagr_Voice_Files_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Voice_Azan_Files"
                                Voice_Azan_Files_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Voice_Azan_Files -(")), "")
                                Voice_Azan_Files_CmbBx.Text = Microsoft.VisualBasic.Left(Voice_Azan_Files_Value, Voice_Azan_Files_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Alert_File_Path"
                                Alert_File_Path_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Alert_File_Path -(")), "")
                                Alert_File_Path_TxtBx.Text = Microsoft.VisualBasic.Left(Alert_File_Path_Value, Alert_File_Path_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Stop_Displaying_Controls_ToolTip"
                                Stop_Displaying_Controls_ToolTip_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Stop_Displaying_Controls_ToolTip -(")), "")
                                Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = Microsoft.VisualBasic.Left(Stop_Displaying_Controls_ToolTip_Value, Stop_Displaying_Controls_ToolTip_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Activate_Windows_Notification_Tray"
                                Activate_Windows_Notification_Tray_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_Windows_Notification_Tray -(")), "")
                                Activate_Windows_Notification_Tray_ChkBx.CheckState = Microsoft.VisualBasic.Left(Activate_Windows_Notification_Tray_Value, Activate_Windows_Notification_Tray_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "MagNotes_Folder_Path"
                                MagNotes_Folder_Path_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("MagNotes_Folder_Path -(")), "")
                                MagNotes_Folder_Path_TxtBx.Text = Microsoft.VisualBasic.Left(MagNotes_Folder_Path_Value, MagNotes_Folder_Path_Value.Length - 2)
                                If Not System.IO.Directory.Exists(MagNotes_Folder_Path_TxtBx.Text) Then
                                    If Language_Btn.Text = "E" Then
                                        Msg = "لم يتم العثور على المجلد المخصص لحفظ ملفات النظام... سيتم انشاء اخر جديد... هل تريد الاستمرار؟"
                                    Else
                                        Msg = "Couldn't Find working folder For The Application... Will Create Another One... Do You Want To Continue?"
                                    End If
                                    If ShowMsg(Msg & vbNewLine & vbNewLine & MagNotes_Folder_Path_TxtBx.Text & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
                                        If Debugger.IsAttached Then
                                            MagNotes_Folder_Path_TxtBx.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\" & "\MagNotes_Files"
                                        Else
                                            MagNotes_Folder_Path_TxtBx.Text = Application.StartupPath & "\MagNotes_Files"
                                        End If
                                        MagNoteFolderPath = MagNotes_Folder_Path_TxtBx.Text
                                        Try
                                            If Not System.IO.Directory.Exists(MagNoteFolderPath) Then
                                                System.IO.Directory.CreateDirectory(MagNoteFolderPath)
                                            End If
                                            If Not System.IO.Directory.Exists(MagNoteFolderPath & "\Shortcuts") Then
                                                System.IO.Directory.CreateDirectory(MagNoteFolderPath & "\Shortcuts")
                                            End If
                                            If Not System.IO.Directory.Exists(MagNoteFolderPath & "\Shortcuts\Images") Then
                                                System.IO.Directory.CreateDirectory(MagNoteFolderPath & "\Shortcuts\Images")
                                            End If
                                        Catch UAE As UnauthorizedAccessException
                                            ShowMsg(UAE.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                                        Catch ex As Exception
                                            System.IO.Directory.CreateDirectory(Application.StartupPath & "\MagNotes_Files")
                                        End Try
                                    Else
                                        End
                                    End If
                                End If
                                ReadNote = String.Empty
                            Case "Backup_Folder_Path"
                                Backup_Folder_Path_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Backup_Folder_Path -(")), "")
                                Backup_Folder_Path_TxtBx.Text = Microsoft.VisualBasic.Left(Backup_Folder_Path_Value, Backup_Folder_Path_Value.Length - 2)
                                ReadNote = String.Empty
                            Case "Open_Note_In_New_Tab"
                                Open_Note_In_New_Tab_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Open_Note_In_New_Tab -(")), "")
                                Open_Note_In_New_Tab_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Open_Note_In_New_Tab_Value, Open_Note_In_New_Tab_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Show_Control_Tab_Pages_In_Multi_Line"
                                Show_Control_Tab_Pages_In_Multi_Line_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Show_Control_Tab_Pages_In_Multi_Line -(")), "")
                                Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Show_Control_Tab_Pages_In_Multi_Line_Value, Show_Control_Tab_Pages_In_Multi_Line_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Load_MagNote_At_Startup"
                                Load_MagNote_At_Startup_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Load_MagNote_At_Startup -(")), "")
                                Load_MagNote_At_Startup_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Load_MagNote_At_Startup_Value, Load_MagNote_At_Startup_Value.Length - 2), ",", ":")
                                Load_MagNote_At_Startup_ChkBx_CheckStateChanged(Load_MagNote_At_Startup_ChkBx, EventArgs.Empty)
                                ReadNote = String.Empty
                            Case "Check_For_New_Version"
                                Check_For_New_Version_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Check_For_New_Version -(")), "")
                                Check_For_New_Version_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Check_For_New_Version_Value, Check_For_New_Version_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Remember_Opened_Notes_When_Close"
                                Remember_Opened_Notes_When_Close_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Remember_Opened_Notes_When_Close -(")), "")
                                Remember_Opened_Notes_When_Close_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Remember_Opened_Notes_When_Close_Value, Remember_Opened_Notes_When_Close_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Apply_Multiple_New_Files"
                                Apply_Multiple_New_Files_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Apply_Multiple_New_Files -(")), "")
                                Apply_Multiple_New_Files_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Apply_Multiple_New_Files_Value, Apply_Multiple_New_Files_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Activate_Projects_TabPage"
                                Activate_Projects_TabPage_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_Projects_TabPage -(")), "")
                                Activate_Projects_TabPage_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_Projects_TabPage_Value, Activate_Projects_TabPage_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Projects_TabPage_ChkBx_CheckStateChanged(Activate_Projects_TabPage_ChkBx, EventArgs.Empty)
                            Case "Activate_Note_Parameters_TabPage"
                                Activate_Note_Parameters_TabPage_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_Note_Parameters_TabPage -(")), "")
                                Activate_Note_Parameters_TabPage_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_Note_Parameters_TabPage_Value, Activate_Note_Parameters_TabPage_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Projects_TabPage_ChkBx_CheckStateChanged(Activate_Note_Parameters_TabPage_ChkBx, EventArgs.Empty)
                            Case "Activate_MagNotes_TabPage"
                                Activate_MagNotes_TabPage_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_MagNotes_TabPage -(")), "")
                                Activate_MagNotes_TabPage_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_MagNotes_TabPage_Value, Activate_MagNotes_TabPage_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Projects_TabPage_ChkBx_CheckStateChanged(Activate_MagNotes_TabPage_ChkBx, EventArgs.Empty)
                            Case "Activate_ShortCut_TabPage"
                                Activate_ShortCut_TabPage_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_ShortCut_TabPage -(")), "")
                                Activate_ShortCuts_TabPage_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_ShortCut_TabPage_Value, Activate_ShortCut_TabPage_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Projects_TabPage_ChkBx_CheckStateChanged(Activate_ShortCuts_TabPage_ChkBx, EventArgs.Empty)
                            Case "Activate_Prayer_Time_TabPage"
                                Activate_Prayer_Time_TabPage_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_Prayer_Time_TabPage -(")), "")
                                Activate_Prayer_Time_TabPage_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_Prayer_Time_TabPage_Value, Activate_Prayer_Time_TabPage_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Projects_TabPage_ChkBx_CheckStateChanged(Activate_Prayer_Time_TabPage_ChkBx, EventArgs.Empty)
                            Case "Activate_Alert_Time_TabPage"
                                Activate_Alert_Time_TabPage_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_Alert_Time_TabPage -(")), "")
                                Activate_Alert_Time_TabPage_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_Alert_Time_TabPage_Value, Activate_Alert_Time_TabPage_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Projects_TabPage_ChkBx_CheckStateChanged(Activate_Alert_Time_TabPage_ChkBx, EventArgs.Empty)
                            Case "Ignore_Error_Message_For_Connection"
                                Ignore_Error_Message_For_Connection_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Ignore_Error_Message_For_Connection -(")), "")
                                Ignore_Error_Message_For_Connection_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Ignore_Error_Message_For_Connection_Value, Ignore_Error_Message_For_Connection_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Force_Stop_Playing_Current_Sound_File"
                                Force_Stop_Playing_Current_Sound_File_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Force_Stop_Playing_Current_Sound_File -(")), "")
                                Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Force_Stop_Playing_Current_Sound_File_Value, Force_Stop_Playing_Current_Sound_File_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Force_Activate_TabPage_When_Alert_Is_Active"
                                Force_Activate_TabPage_When_Alert_Is_Active_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Force_Activate_TabPage_When_Alert_Is_Active -(")), "")
                                Force_Activate_TabPage_When_Alert_Is_Active_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Force_Activate_TabPage_When_Alert_Is_Active_Value, Force_Activate_TabPage_When_Alert_Is_Active_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Immediately_Update_Form_Parameters"
                                Immediately_Update_Form_Parameters_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Immediately_Update_Form_Parameters -(")), "")
                                Immediately_Update_Form_Parameters_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Immediately_Update_Form_Parameters_Value, Immediately_Update_Form_Parameters_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Activate_Alert_Function"
                                Activate_Alert_Function_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Activate_Alert_Function -(")), "")
                                Activate_Alert_Function_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Activate_Alert_Function_Value, Activate_Alert_Function_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                                Activate_Alert_Function_ChkBx_CheckStateChanged(Activate_Alert_Function_ChkBx, EventArgs.Empty)
                            Case "Azan_Spoke_Method"
                                Azan_Spoke_Method_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Azan_Spoke_Method -(")), "")
                                Azan_Spoke_Method_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Azan_Spoke_Method_Value, Azan_Spoke_Method_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Azan_Activation"
                                Azan_Activation_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Azan_Activation -(")), "")
                                Azan_Activation_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Azan_Activation_Value, Azan_Activation_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Alert_Before_Azan"
                                Alert_Before_Azan_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Alert_Before_Azan -(")), "")
                                Alert_Before_Azan_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Alert_Before_Azan_Value, Alert_Before_Azan_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Time_To_Alert_Before_Azan"
                                Time_To_Alert_Before_Azan_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Time_To_Alert_Before_Azan -(")), "")
                                Time_To_Alert_Before_Azan_NmrcUpDn.Value = Replace(Microsoft.VisualBasic.Left(Time_To_Alert_Before_Azan_Value, Time_To_Alert_Before_Azan_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Azan_Takbeer_Only"
                                Azan_Takbeer_Only_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Azan_Takbeer_Only -(")), "")
                                Azan_Takbeer_Only_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Azan_Takbeer_Only_Value, Azan_Takbeer_Only_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                            Case "Reload_MagNote_After_Change_Category"
                                Reload_MagNote_After_Change_Category_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Reload_MagNote_After_Change_Category -(")), "")
                                Reload_MagNote_After_Change_Category_ChkBx.CheckState = Replace(Microsoft.VisualBasic.Left(Reload_MagNote_After_Change_Category_Value, Reload_MagNote_After_Change_Category_Value.Length - 2), ",", ":")
                                ReadNote = String.Empty
                        End Select
                    Catch ex As Exception
                        ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    End Try
                Next
            End If
            CreatListView()
            LoadList(Nothing)
#End Region
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Application.DoEvents()
        End Try
    End Sub
#Region "MagNote_TxtBxAddHandler"
    Private Function Available_MagNotes_DGV_Add_MenuStrip() As Boolean
        Available_MagNotes_DGV.ContextMenuStrip = New ContextMenuStrip()
        Dim Apply_Select_Multiple_Rows As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Apply_Select_Multiple_Rows.Text = "Apply Select Multiple Rows"
        Else
            Apply_Select_Multiple_Rows.Text = "السماح بإختيار أصفف متعددة"
        End If
        Apply_Select_Multiple_Rows.Name = "ApplySelectMultipleRows"
        Apply_Select_Multiple_Rows.Tag = MagNotes_Notes_TbCntrl.Name
        Apply_Select_Multiple_Rows.Image = My.Resources.PwerOff
        Apply_Select_Multiple_Rows.BackgroundImage = My.Resources.Background4
        Available_MagNotes_DGV.ContextMenuStrip.Items.Add(Apply_Select_Multiple_Rows)
        Apply_Select_Multiple_Rows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Apply_Select_Multiple_Rows.Click, AddressOf Apply_Select_Multiple_Rows_Click
        '-------------------------------------------------------------
        Dim RemoveCurrentRow As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            RemoveCurrentRow.Text = "Remove Selected Row(s)"
        Else
            RemoveCurrentRow.Text = "حزف السطر(الاسطر) الختارة"
        End If
        RemoveCurrentRow.Tag = MagNotes_Notes_TbCntrl.Name
        RemoveCurrentRow.Name = "RemoveSelectedRow"
        RemoveCurrentRow.BackgroundImage = My.Resources.Background4
        RemoveCurrentRow.Image = My.Resources.PwerOff
        Available_MagNotes_DGV.ContextMenuStrip.Items.Add(RemoveCurrentRow)
        RemoveCurrentRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler RemoveCurrentRow.Click, AddressOf Remove_Selected_Row_Click
        '-------------------------------------------------------------
    End Function
    Private Function MagNotes_Notes_TabControl_Add_MenuStrip() As Boolean
        MagNotes_Notes_TbCntrl.ContextMenuStrip = New ContextMenuStrip()
        Dim Close_Current_TabPage As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Close_Current_TabPage.Text = "Close TabPage"
        Else
            Close_Current_TabPage.Text = "إقفال صفحة التبويب"
        End If
        Close_Current_TabPage.Tag = MagNotes_Notes_TbCntrl.Name
        Close_Current_TabPage.Name = "CloseCurrentTabPage"
        Close_Current_TabPage.BackgroundImage = My.Resources.Background4
        Close_Current_TabPage.Image = My.Resources.PwerOff
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Close_Current_TabPage)
        Close_Current_TabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Close_Current_TabPage.Click, AddressOf Close_Current_TabPage_Click
        '-------------------------------------------------------------
        Dim Remove_Current_TabPage As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Remove_Current_TabPage.Text = "Remove TabPages"
        Else
            Remove_Current_TabPage.Text = "حزف صفحة التبويب"
        End If
        Remove_Current_TabPage.Name = "RemoveCurrentTabPage"
        Remove_Current_TabPage.Tag = MagNotes_Notes_TbCntrl.Name
        Remove_Current_TabPage.Image = My.Resources.PwerOff
        Remove_Current_TabPage.BackgroundImage = My.Resources.Background4
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Remove_Current_TabPage)
        Remove_Current_TabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Remove_Current_TabPage.Click, AddressOf Remove_Current_TabPage_Click
        '-------------------------------------------------------------
        Dim Close_All_TabPages As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Close_All_TabPages.Text = "Close All TabPages"
        Else
            Close_All_TabPages.Text = "إقفال جميع صفحات التبويب"
        End If
        Close_All_TabPages.Name = "CloseAllTabPages"
        Close_All_TabPages.Tag = MagNotes_Notes_TbCntrl.Name
        Close_All_TabPages.Image = My.Resources.PwerOff
        Close_All_TabPages.BackgroundImage = My.Resources.Background4
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Close_All_TabPages)
        Close_All_TabPages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Close_All_TabPages.Click, AddressOf Close_All_TabPages_Click
        '-------------------------------------------------------------
        Dim Close_All_TabPages_Except_Current As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Close_All_TabPages_Except_Current.Text = "Close All TabPages Excep Current"
        Else
            Close_All_TabPages_Except_Current.Text = "إقفال جميع صفحات التبويب ماعدا الحالية"
        End If
        Close_All_TabPages_Except_Current.Name = "CloseAllTabPagesExceptCurrent"
        Close_All_TabPages_Except_Current.Tag = MagNotes_Notes_TbCntrl.Name
        Close_All_TabPages_Except_Current.Image = My.Resources.PwerOff
        Close_All_TabPages_Except_Current.BackgroundImage = My.Resources.Background4
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Close_All_TabPages_Except_Current)
        Close_All_TabPages_Except_Current.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Close_All_TabPages_Except_Current.Click, AddressOf Close_All_TabPages_Except_Current_Click

        '-------------------------------------------------------------
        Dim Close_All_TabPages_Except_Basic As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Close_All_TabPages_Except_Basic.Text = "Close All TabPages Excep Basic"
        Else
            Close_All_TabPages_Except_Basic.Text = "إقفال جميع صفحات التبويب ماعدا الأساسية"
        End If
        Close_All_TabPages_Except_Basic.Name = "CloseAllTabPagesExceptCurrent"
        Close_All_TabPages_Except_Basic.Tag = MagNotes_Notes_TbCntrl.Name
        Close_All_TabPages_Except_Basic.Image = My.Resources.PwerOff
        Close_All_TabPages_Except_Basic.BackgroundImage = My.Resources.Background4
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Close_All_TabPages_Except_Basic)
        Close_All_TabPages_Except_Basic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Close_All_TabPages_Except_Basic.Click, AddressOf Close_All_TabPages_Except_Basic_Click
        '-------------------------------------------------------------
        Dim Add_New_MagNote As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Add_New_MagNote.Text = "Add New MagNote"
        Else
            Add_New_MagNote.Text = "إنشاء ملاحظة جديدة"
        End If
        Add_New_MagNote.Name = "Add_New_MagNote"
        Add_New_MagNote.Tag = MagNotes_Notes_TbCntrl.Name
        Add_New_MagNote.Image = My.Resources.NewDocumentHS
        Add_New_MagNote.BackgroundImage = My.Resources.Background4
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Add_New_MagNote)
        Add_New_MagNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Add_New_MagNote.Click, AddressOf New_Note_TlStrpBtn_Click
        '-------------------------------------------------------------
        Dim Show_Labeling_And_Tooltip_Form As New ToolStripMenuItem
        If Language_Btn.Text = "ع" Then
            Show_Labeling_And_Tooltip_Form.Text = "Show Labeling And Tooltip Form"
        Else
            Show_Labeling_And_Tooltip_Form.Text = "إظهار شاشة إعداد عناوين وشرح العناصر"
        End If
        Show_Labeling_And_Tooltip_Form.Name = "Show_Labeling_And_Tooltip_Form"
        Show_Labeling_And_Tooltip_Form.Tag = MagNotes_Notes_TbCntrl.Name
        Show_Labeling_And_Tooltip_Form.Image = My.Resources.NewDocumentHS
        Show_Labeling_And_Tooltip_Form.BackgroundImage = My.Resources.Background4
        MagNotes_Notes_TbCntrl.ContextMenuStrip.Items.Add(Show_Labeling_And_Tooltip_Form)
        Show_Labeling_And_Tooltip_Form.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AddHandler Show_Labeling_And_Tooltip_Form.Click, AddressOf Show_Labeling_And_Tooltip_Form_Click

    End Function
    Private Sub Remove_Current_TabPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IsNothing(TabPageToClose) Then
                Exit Sub
            ElseIf NoteAmendmented(TabPageToClose.Name) = DialogResult.Cancel Then
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor
            Dim AryInx = RCSN.Name
            Dim TbPgIndex = MagNotes_Notes_TbCntrl.TabPages.IndexOf(TabPageToClose)
            For Each cntrl In MagNotes_Notes_TbCntrl.TabPages(TbPgIndex).Controls
                cntrl.dispose
            Next
            MagNote_No_CmbBx.Items.Remove(IsInMagNoteCmbBx(TabPageToClose.Tag,,,, 1))
            Try
                Available_MagNotes_DGV.Rows.RemoveAt(CType(isInDataGridView(TabPageToClose.Tag, "MagNote_Name", Available_MagNotes_DGV, 0, 1), DataGridViewRow).Index)
            Catch ex As Exception
            End Try
            MagNotes_Notes_TbCntrl.TabPages.Remove(MagNotes_Notes_TbCntrl.TabPages(TabPageToClose.Tag))
            FileCount = Nothing
            RemoveMagNoteRTF(AryInx)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            TabPageToClose = Nothing
        End Try
    End Sub
    Private Sub Close_Current_TabPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IsNothing(TabPageToClose) Or
                NoteAmendmented(TabPageToClose.Name) = DialogResult.Cancel Then
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor
            Dim AryInx = RCSN.Name
            Dim TbPgIndex = MagNotes_Notes_TbCntrl.TabPages.IndexOf(TabPageToClose)
            For Each cntrl In MagNotes_Notes_TbCntrl.TabPages(TbPgIndex).Controls
                cntrl.dispose
            Next
            If Not File.Exists(TabPageToClose.Tag) Then
                MagNote_No_CmbBx.Items.Remove(IsInMagNoteCmbBx(TabPageToClose.Tag,,,, 1))
                Try
                    Available_MagNotes_DGV.Rows.RemoveAt(CType(isInDataGridView(TabPageToClose.Tag, "MagNote_Name", Available_MagNotes_DGV, 0, 1), DataGridViewRow).Index)
                Catch ex As Exception
                End Try
            End If
            MagNotes_Notes_TbCntrl.TabPages.Remove(MagNotes_Notes_TbCntrl.TabPages(TabPageToClose.Tag))
            FileCount = Nothing
            RemoveMagNoteRTF(AryInx)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            TabPageToClose = Nothing
        End Try
    End Sub


    Private Sub Apply_Select_Multiple_Rows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Available_MagNotes_DGV.MultiSelect = True Then
                Available_MagNotes_DGV.MultiSelect = False
            Else
                Available_MagNotes_DGV.MultiSelect = True
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Remove_Selected_Row_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            If Available_MagNotes_DGV.SelectedRows.Count = 0 Then
                Exit Sub
            End If
            TabPageToClose = New TabPage
            Dim SelectedRows(0) As DataGridViewRow
            For Each Row In Available_MagNotes_DGV.SelectedRows
                SelectedRows(SelectedRows.Count - 1) = Row
                If Available_MagNotes_DGV.SelectedRows.Count > SelectedRows.Length Then
                    ReDim Preserve SelectedRows(SelectedRows.Length)
                End If
            Next

            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, SelectedRows.Count)
            For Each Note In SelectedRows
                Dim AryInx = Note.Cells("MagNote_Name").Value
                progress += ProgressToAdd
                Previewlbl.Text = "Loading--> " & Note.Cells("MagNote_Name").Value & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                Try
                    MagNotes_Notes_TbCntrl.TabPages.Remove(MagNotes_Notes_TbCntrl.TabPages(Note.Cells("MagNote_Name").Value))
                Catch ex As Exception
                End Try
                MagNote_No_CmbBx.Items.Remove(IsInMagNoteCmbBx(Note.Cells("MagNote_Name").Value,,, , 1))
                Available_MagNotes_DGV.Rows.RemoveAt(CType(isInDataGridView(Note.Cells("MagNote_Name").Value, "MagNote_Name", Available_MagNotes_DGV, 0, 1), DataGridViewRow).Index)
                FileCount = Nothing
                RemoveMagNoteRTF(AryInx)
            Next
            If Language_Btn.Text = "E" Then
                Msg = "تم الانتهاء من حزف الملاحظات المختارة بنجاح وعددها"
            Else
                Msg = "The Selected Notes Removed Successfully And Its Count Is "
            End If
            ShowMsg(Msg & vbNewLine & SelectedRows.Count & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Sub

    Private Function RemoveMagNoteRTF(ByVal AryInx) As Boolean
        Dim TmpMagNoteRTF(0) As RichTextBox
        '
        For Each Item In MagNoteRTF
            If Item.Name = AryInx Or
                 Item.Name = Replace(AryInx, ")-.txt", ")-.txtRchTxtBx") Then
                Continue For
            End If
            TmpMagNoteRTF(TmpMagNoteRTF.Length - 1) = Item
            If TmpMagNoteRTF.Length < MagNoteRTF.Length - 1 Then
                ReDim Preserve TmpMagNoteRTF(TmpMagNoteRTF.Length)
            End If
        Next
        Erase MagNoteRTF
        MagNoteRTF = TmpMagNoteRTF

        Dim TmpRchTxtBxStyle(0) As RchTxtBxStyleDetils
        For Each Item In RchTxtBxStyle
            If Item.RchTxtBx_Name = AryInx Or
                Item.RchTxtBx_Name = Replace(AryInx, ")-.txt", ")-.txtRchTxtBx") Then
                Continue For
            End If
            TmpRchTxtBxStyle(TmpRchTxtBxStyle.Length - 1) = Item
            If TmpRchTxtBxStyle.Length < RchTxtBxStyle.Length - 1 Then
                ReDim Preserve TmpRchTxtBxStyle(TmpRchTxtBxStyle.Length)
            End If
        Next
        Erase RchTxtBxStyle
        RchTxtBxStyle = TmpRchTxtBxStyle
    End Function
    Private Sub Close_All_TabPages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim TbPgTag = MagNotes_Notes_TbCntrl.SelectedTab.Tag
            For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                If NoteAmendmented(TbPg.Name) <> DialogResult.Cancel Then
                    MagNotes_Notes_TbCntrl.TabPages.Remove(TbPg)
                    If Not File.Exists(TbPg.tag) Then
                        MagNote_No_CmbBx.Items.Remove(IsInMagNoteCmbBx(TbPgTag,,, , 1))
                    End If
                End If
            Next
            MagNotes_Notes_TbCntrl.Refresh()
            FileCount = Nothing
            New_Note_TlStrpBtn_Click(New_Note_TlStrpBtn, EventArgs.Empty)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Close_All_TabPages_Except_Current_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim SlctdTbPgNm = MagNotes_Notes_TbCntrl.SelectedTab.Name
            For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                If NoteAmendmented(TbPg.Name) <> DialogResult.Cancel Then
                    If TbPg.name = SlctdTbPgNm Then
                        Continue For
                    End If
                    If Not File.Exists(TbPg.tag) Then
                        MagNote_No_CmbBx.Items.Remove(IsInMagNoteCmbBx(MagNotes_Notes_TbCntrl.SelectedTab.Tag,,, , 1))
                    End If
                    MagNotes_Notes_TbCntrl.TabPages.Remove(TbPg)
                End If
            Next
            MagNotes_Notes_TbCntrl.Refresh()
            FileCount = Nothing
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Close_All_TabPages_Except_Basic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Cursor = Cursors.WaitCursor
            Dim OpenedTbPgsFiles = MagNotes_Folder_Path_TxtBx.Text & "\OpenedTabPages.txt"
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, MagNotes_Notes_TbCntrl.TabPages.Count)
            If File.Exists(OpenedTbPgsFiles) Then
                Dim OpenedTabPages = My.Computer.FileSystem.ReadAllText(OpenedTbPgsFiles, System.Text.Encoding.UTF8)
                For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                    progress += ProgressToAdd
                    Previewlbl.Text = "Loading--> " & TbPg.name & vbNewLine & Math.Floor(progress * 100)
                    Previewlbl.Refresh()
                    Previewlbl.Invalidate()
                    If OpenedTabPages.Contains(TbPg.name) Then
                        Continue For
                    End If
                    If NoteAmendmented(TbPg.Name) <> DialogResult.Cancel Then
                        If Not File.Exists(TbPg.tag) Then
                            MagNote_No_CmbBx.Items.Remove(IsInMagNoteCmbBx(MagNotes_Notes_TbCntrl.SelectedTab.Tag,,, , 1))
                        End If
                        MagNotes_Notes_TbCntrl.TabPages.Remove(TbPg)
                    End If
                Next
            End If
            MagNotes_Notes_TbCntrl.Refresh()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Sub
    Public Function MagNote_TxtBxAddHandler(Optional ByVal RchTxtBx As RichTextBox = Nothing)
        Try
            If IsNothing(RCSN(0)) Then
                Exit Function
            End If
            RCSN.ContextMenuStrip = New ContextMenuStrip()
            Dim SelectAll_TxtBx As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                SelectAll_TxtBx.Text = "Select All"
            Else
                SelectAll_TxtBx.Text = "إختر الكل"
            End If
            SelectAll_TxtBx.Tag = RCSN.Name
            SelectAll_TxtBx.Name = "SelectAll" & RCSN.Name
            SelectAll_TxtBx.BackgroundImage = My.Resources.Background4
            SelectAll_TxtBx.Image = My.Resources.SelectAll
            RCSN.ContextMenuStrip.Items.Add(SelectAll_TxtBx)
            SelectAll_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler SelectAll_TxtBx.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
            '-------------------------------------------
            If Not LCase(RCSN.Name).Contains("password") Then
                Dim Copy_TxtBx As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    Copy_TxtBx.Text = "Copy"
                Else
                    Copy_TxtBx.Text = "نسخ"
                End If
                Copy_TxtBx.Tag = RCSN.Name
                Copy_TxtBx.Name = "Copy" & RCSN.Name
                Copy_TxtBx.BackgroundImage = My.Resources.Background4
                Copy_TxtBx.Image = My.Resources.copy
                RCSN.ContextMenuStrip.Items.Add(Copy_TxtBx)
                Copy_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                AddHandler Copy_TxtBx.Click, AddressOf Copy_TxtBx_To_Cliboard_Click
            End If
            '-------------------------------------------
            If Not LCase(RCSN.Name).Contains("password") Then
                Dim Cut_TxtBx As New ToolStripMenuItem
                If Language_Btn.Text = "ع" Then
                    Cut_TxtBx.Text = "Cut"
                Else
                    Cut_TxtBx.Text = "قص"
                End If
                Cut_TxtBx.Name = "Cut" & RCSN.Name
                Cut_TxtBx.Tag = RCSN.Name
                Cut_TxtBx.Image = My.Resources.cut
                Cut_TxtBx.BackgroundImage = My.Resources.Background4
                RCSN.ContextMenuStrip.Items.Add(Cut_TxtBx)
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
            Past_TxtBx.Name = "Past" & RCSN.Name
            Past_TxtBx.Tag = RCSN.Name
            Past_TxtBx.Image = My.Resources.paste
            Past_TxtBx.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Past_TxtBx)
            Past_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Past_TxtBx.Click, AddressOf Past_TxtBx_To_Cliboard_Click
            '-------------------------------------------
            Dim Insert_Special_Character As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Insert_Special_Character.Text = "Insert Special Character"
            Else
                Insert_Special_Character.Text = "إدراج حرف خاص"
            End If
            Insert_Special_Character.Name = "INS" & RCSN.Name
            Insert_Special_Character.Tag = RCSN.Name
            Insert_Special_Character.Image = My.Resources.SpecialCharacter
            Insert_Special_Character.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Insert_Special_Character)
            Insert_Special_Character.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Insert_Special_Character.Click, AddressOf Insert_Special_Character_To_Cliboard_Click
            '-------------------------------------------
            Dim Encrypt_Selected_Text As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Encrypt_Selected_Text.Text = "Encrypt Selected Text"
            Else
                Encrypt_Selected_Text.Text = "تشفير الكلمات المختارة"
            End If
            Encrypt_Selected_Text.Name = "Encrypt_Selected_Text" & RCSN.Name
            Encrypt_Selected_Text.Tag = RCSN.Name
            Encrypt_Selected_Text.Image = My.Resources.encrypt
            Encrypt_Selected_Text.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Encrypt_Selected_Text)
            Encrypt_Selected_Text.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Encrypt_Selected_Text.Click, AddressOf Encrypt_Selected_Text_Click
            '-------------------------------------------
            Dim Decrypt_Selected_Text As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Decrypt_Selected_Text.Text = "Decrypt Selected Text"
            Else
                Decrypt_Selected_Text.Text = "فك تشفير الكلمات المختارة"
            End If
            Decrypt_Selected_Text.Name = "Decrypt_Selected_Text" & RCSN.Name
            Decrypt_Selected_Text.Tag = RCSN.Name
            Decrypt_Selected_Text.Image = My.Resources.decrypt
            Decrypt_Selected_Text.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Decrypt_Selected_Text)
            Decrypt_Selected_Text.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Decrypt_Selected_Text.Click, AddressOf Decrypt_Selected_Text_Click
            '-------------------------------------------
            Dim Copy_Encrypted_Word As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Copy_Encrypted_Word.Text = "Copy Encrypted Word"
            Else
                Copy_Encrypted_Word.Text = "نسخ الكلمة المشفرة"
            End If
            Copy_Encrypted_Word.Name = "Copy_Encrypted_Word" & RCSN.Name
            Copy_Encrypted_Word.Tag = RCSN.Name
            Copy_Encrypted_Word.Image = My.Resources.NewDocumentHS
            Copy_Encrypted_Word.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Copy_Encrypted_Word)
            Copy_Encrypted_Word.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Copy_Encrypted_Word.Click, AddressOf Copy_Encrypted_Word_Click
            '-------------------------------------------
            Dim Insert_Table_Btn As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Insert_Table_Btn.Text = "Insert Table"
            Else
                Insert_Table_Btn.Text = "إنشاء جدول"
            End If
            Insert_Table_Btn.Name = "Insert_Table" & RCSN.Name
            Insert_Table_Btn.Tag = RCSN.Name
            Insert_Table_Btn.Image = My.Resources.NewDocumentHS
            Insert_Table_Btn.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Insert_Table_Btn)
            Insert_Table_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Insert_Table_Btn.Click, AddressOf Initiate_ReoGride
            '-------------------------------------------
            Dim Create_Link_TxtBx As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Create_Link_TxtBx.Text = "Create Link"
            Else
                Create_Link_TxtBx.Text = "إنشاء الرابط"
            End If
            Create_Link_TxtBx.Name = " Create_Link" & RCSN.Name
            Create_Link_TxtBx.Tag = RCSN.Name
            Create_Link_TxtBx.BackgroundImage = My.Resources.Background4
            Create_Link_TxtBx.Image = My.Resources.link1
            RCSN.ContextMenuStrip.Items.Add(Create_Link_TxtBx)
            Create_Link_TxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Create_Link_TxtBx.Click, AddressOf InsertLink 'Create_Link_TxtBx_Click 
            '-------------------------------------------
            Dim SetControlToFill As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                SetControlToFill.Text = "Set Control To Fill"
            Else
                SetControlToFill.Text = "إختيار عنصر التعبئة"
            End If
            SetControlToFill.Name = "SetControlToFill" & RCSN.Name
            SetControlToFill.Tag = RCSN.Name
            SetControlToFill.BackgroundImage = My.Resources.Background4
            SetControlToFill.Image = My.Resources.SizeAll_White
            RCSN.ContextMenuStrip.Items.Add(SetControlToFill)
            SetControlToFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '-------------------------------------------
            Dim SetFillToNote As New ToolStripMenuItem()
            If Language_Btn.Text = "ع" Then
                SetFillToNote.Text = "Set Fill To Note"
            Else
                SetFillToNote.Text = "إختيار التعبئة لملاحظة"
            End If
            SetFillToNote.Name = "SetFillToNote" & RCSN.Name
            SetFillToNote.Tag = RCSN.Name
            SetFillToNote.BackgroundImage = My.Resources.Background4
            SetFillToNote.Image = My.Resources.SizeAll_White
            RCSN.ContextMenuStrip.Items.Add(SetFillToNote)
            SetFillToNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler SetFillToNote.Click, AddressOf SetFillToNote_Click 'add the click handler sub
            '-------------------------------------------
            Dim SetFillToControlTabs As New ToolStripMenuItem()
            If Language_Btn.Text = "ع" Then
                SetFillToControlTabs.Text = "Set Fill To Control Tabs"
            Else
                SetFillToControlTabs.Text = "إختيار التعبئة لصفحات التبويب"
            End If
            SetFillToControlTabs.Name = "SetFillToControlTabs" & RCSN.Name
            SetFillToControlTabs.Tag = RCSN.Name
            SetFillToControlTabs.BackgroundImage = My.Resources.Background4
            SetFillToControlTabs.Image = My.Resources.SizeAll_White
            RCSN.ContextMenuStrip.Items.Add(SetFillToControlTabs)
            SetFillToControlTabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler SetFillToControlTabs.Click, AddressOf SetFillToControlTabs_Click
            '-------------------------------------------------------------
            Dim Show_Labeling_And_Tooltip_Form As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Show_Labeling_And_Tooltip_Form.Text = "Show Labeling And Tooltip Form"
            Else
                Show_Labeling_And_Tooltip_Form.Text = "إظهار شاشة إعداد عناوين وشرح العناصر"
            End If
            Show_Labeling_And_Tooltip_Form.Name = "Show_Labeling_And_Tooltip_Form"
            Show_Labeling_And_Tooltip_Form.Tag = MagNotes_Notes_TbCntrl.Name
            Show_Labeling_And_Tooltip_Form.Image = My.Resources.NewDocumentHS
            Show_Labeling_And_Tooltip_Form.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Show_Labeling_And_Tooltip_Form)
            Show_Labeling_And_Tooltip_Form.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Show_Labeling_And_Tooltip_Form.Click, AddressOf Show_Labeling_And_Tooltip_Form_Click
            '-------------------------------------------------------------
            Dim ShowHide_RchTxtBx As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                ShowHide_RchTxtBx.Text = "Show / Hide"
            Else
                ShowHide_RchTxtBx.Text = "إخفاء / إظهار"
            End If
            ShowHide_RchTxtBx.Name = "Close_This_Window"
            ShowHide_RchTxtBx.Tag = MagNotes_Notes_TbCntrl.Name
            ShowHide_RchTxtBx.Image = My.Resources.NewDocumentHS
            ShowHide_RchTxtBx.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(ShowHide_RchTxtBx)
            ShowHide_RchTxtBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler ShowHide_RchTxtBx.Click, AddressOf ShowHide_RchTxtBx_Click
            '-------------------------------------------------------------
            Dim Find As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Find.Text = "Find"
            Else
                Find.Text = "بحث"
            End If
            Find.Name = "Find"
            Find.Tag = MagNotes_Notes_TbCntrl.Name
            Find.Image = My.Resources.NewDocumentHS
            Find.BackgroundImage = My.Resources.Background4
            RCSN.ContextMenuStrip.Items.Add(Find)
            Find.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Find.Click, AddressOf Find_Click

            SetControlToFill.DropDownItems.Add(SetFillToNote)
            SetControlToFill.DropDownItems.Add(SetFillToControlTabs)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub Encrypt_Selected_Text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                If RCSN.SelectedText.Length > 0 Then
                    RCSN.SelectedText = "Encrypted Word [" & Encrypt_Function(RCSN.SelectedText) & "]"
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Decrypt_Selected_Text_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                If RCSN.SelectedText.Length > 0 Then
                    If RCSN.SelectedText.Contains("Encrypted Word [") And
                        Microsoft.VisualBasic.Right(RCSN.SelectedText, 1) = "]" Then
                        Dim TextToDecrypt As String = Replace(RCSN.SelectedText, "Encrypted Word [", "")
                        TextToDecrypt = Microsoft.VisualBasic.Left(TextToDecrypt, TextToDecrypt.Length - 1)
                        RCSN.SelectedText = Decrypt_Function(TextToDecrypt)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function CheckEnterPasswordToPass() As Boolean
        If Main_Password_TxtBx.TextLength = 0 Or
                    Enter_Password_To_Pass_ChkBx.CheckState = CheckState.Unchecked Then
            If Language_Btn.Text = "E" Then
                Msg = "تسخ الكلمات المشفرة تستخدم فقط مع خاصية ادخال كلمة السر للمرور... من فضلك ضع علامة على العنصر ـ(إستخدم كلمة سر للمرور)ـ وادخل كلمة سر صحيحة للمرو"
            Else
                Msg = "Copy Encrypted Word Is Used only With The Enter Password To Pass Feature... Kindly Check The Object (Enter Password To Pass) And Enter Correct Password To Pass"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Return True
        End If
    End Function
    Private Sub Copy_Encrypted_Word_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If CheckEnterPasswordToPass() Then Exit Sub
            Clipboard.Clear()
            Cursor = Cursors.WaitCursor
            If RCSN.SelectedText.Length > 0 Then
                If RCSN.SelectedText.Contains("Encrypted Word [") And
                        Microsoft.VisualBasic.Right(RCSN.SelectedText, 1) = "]" Then
                    Dim TextToDecrypt As String = Replace(RCSN.SelectedText, "Encrypted Word [", "")
                    TextToDecrypt = Microsoft.VisualBasic.Left(TextToDecrypt, TextToDecrypt.Length - 1)
                    TextToDecrypt = Decrypt_Function(TextToDecrypt)
                    Clipboard.SetText(TextToDecrypt)
                Else
                    If Language_Btn.Text = "E" Then
                        Msg = "ربما لم يتم اختيار جملة مناسبة لفك التشفير... حيث صيغة الجملة يجب ان تبدأ بـ" & vbNewLine & "Encrypted Word [" & vbNewLine & "ويجب ان ينتهى بـ" & vbNewLine & "]"
                    Else
                        Msg = "Maybe The Selected Sentence Was Not Chosen For Decryption... The Sentence Formate Must Start With (Encrypted Word [) And Must End With (])"
                    End If
                    ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)

                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Clipboard_Cut()
        Try
            Cursor = Cursors.WaitCursor
            My.Computer.Clipboard.Clear()
            Clipboard.SetText(RCSN.SelectedText)
            RCSN.SelectedText = ""
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Cut_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                Clipboard_Cut()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Clipboard_Past()
        Try
            Cursor = Cursors.WaitCursor
            If IsNothing(RCSN(0)) Then Exit Sub
            Dim iData As IDataObject = Clipboard.GetDataObject()
            'Check to see if the data is in a text format
            If iData.GetDataPresent(DataFormats.Text) Then
                'If it's text, then paste it into the textbox
                If Not IsNothing(iData.GetData(DataFormats.Text)) Then
                    Try
                        RCSN.SelectedText = CType(iData.GetData(DataFormats.UnicodeText), String)
                    Catch ex As Exception
                        RCSN.SelectedText = CType(iData.GetData(DataFormats.Text), String)
                    End Try
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub
    Public Sub Clipboard_Copy()
        Try
            Cursor = Cursors.WaitCursor
            Try
                If (RCSN.SelectionType And RichTextBoxSelectionTypes.Object) = RichTextBoxSelectionTypes.Object Then
                    Clipboard.Clear()
                    RCSN.Copy()
                    Dim idata As IDataObject = Clipboard.GetDataObject()
                    If idata.GetDataPresent("System.Drawing.Bitmap") Then
                        Dim imgObject As Object = idata.GetData("System.Drawing.Bitmap")
                        If imgObject IsNot Nothing Then
                            Dim img As Image = TryCast(imgObject, Image)
                            If img IsNot Nothing Then
                                Dim x = 1
                            End If
                        End If
                    Else
                        My.Computer.Clipboard.SetText(RCSN.SelectedText, TextDataFormat.UnicodeText)
                    End If
                Else
                    My.Computer.Clipboard.SetText(RCSN.SelectedText, TextDataFormat.UnicodeText)
                End If
            Catch ex As Exception
                My.Computer.Clipboard.SetText(RCSN.SelectedText, TextDataFormat.Text)
            End Try
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Copy_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                Dim ColumnsTooTibText = Nothing
                If (sender.text.contains("إختر الكل") Or sender.text.contains("Select All")) And
                    (Objct.GetType = GetType(System.Windows.Forms.TextBox) Or
                    Objct.GetType = GetType(RichTextBox)) Then
                    Objct.SelectAll()
                Else
                    If RCSN.SelectedText.Contains("Encrypted Word [") Then
                        If Language_Btn.Text = "E" Then
                            Msg = "النص التى اخترته مشفر... هل تريد نسخ الكلمة المشفرة؟"
                        Else
                            Msg = "The Text You Selected Is Encrypted... Do You Want To Copy The Encrypted Word?"
                        End If
                        If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs) = DialogResult.Yes Then
                            Copy_Encrypted_Word_Click(Nothing, EventArgs.Empty)
                            Exit Sub
                        End If
                    End If
                    My.Computer.Clipboard.Clear()
                    Clipboard_Copy()
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub linkLabel1_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        ' Specify that the link was visited.
        sender.LinkVisited = True
        ' Navigate to a URL.
        System.Diagnostics.Process.Start(sender.tag)
    End Sub

    Dim FileLink = ""

    Private Sub SetFillToControlTabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Set_Control_To_Fill_ChkBx.CheckState = CheckState.Unchecked
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub SetFillToNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Set_Control_To_Fill_ChkBx.CheckState = CheckState.Checked
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Sub InsertLink(ByVal text As String, ByVal position As Integer)
        RCSN.SelectionStart = position
        RCSN.SelectedText = text
        RCSN.[Select](position, text.Length)
        RCSN.[Select](position + text.Length, 0)
    End Sub

    Private Sub Insert_Special_Character_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim charMapPath As String = "C:\Windows\System32\charmap.exe"
            ' Check if the Character Map utility exists
            If File.Exists(charMapPath) Then
                ' Open the Character Map utility
                Dim fontName = "Wingdings"
                Process.Start(charMapPath, $"/F ""{fontName}""")
            Else
                Console.WriteLine("Character Map utility not found.")
                If Language_Btn.Text = "E" Then
                    Msg = "لم يتم العثور على البرنامج الخاص بإدراج حرف خاص"
                Else
                    Msg = "The Program For Inserting A Special Character Is Not Found"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MBOs, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Past_TxtBx_To_Cliboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RCSN.DetectUrls = True
            Cursor = Cursors.WaitCursor
            Dim data As IDataObject = Clipboard.GetDataObject()
            Clipboard.GetDataObject()
            If My.Computer.Clipboard.ContainsData(DataFormats.Html) Then
                If Language_Btn.Text = "E" Then
                    Msg = "هل تريد لصق مكونات الذاكرة كنص مقروء ام على هيئة لغة ترميز النصوص التشعبية (html)"
                Else
                    Msg = "Do You Want To Past The Clepboard Contents as text readable or HTML format?"
                End If
                If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                    RCSN.SelectedText = Clipboard.GetDataObject.GetData(System.Windows.Forms.DataFormats.Html, True)
                ElseIf DialogResult = DialogResult.No Then
                    RCSN.SelectedText = Clipboard.GetDataObject.GetData(System.Windows.Forms.DataFormats.StringFormat, True)
                Else
                    RCSN.Paste()
                End If

            ElseIf data.GetDataPresent(DataFormats.Rtf, False) Then
                ' if available, paste into the RTF selection
                RCSN.SelectedRtf = data.GetData(DataFormats.Rtf).ToString()
            ElseIf (Clipboard.ContainsText(TextDataFormat.Html)) Then
                Clipboard.GetText(TextDataFormat.Html)
            ElseIf My.Computer.Clipboard.ContainsImage() Then
                Clipboard.GetImage()
            Else
                RCSN.Paste()
            End If
            RCSN.Select()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#End Region
    Private Sub MagNote_Form_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Exit_TlStrpBtn_Click(Exit_TlStrpBtn, EventArgs.Empty)
            ElseIf e.KeyCode = Keys.F3 Then
                If CType(Form_ToolTip, ToolTip).Active = False Then
                    Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = CheckState.Unchecked
                Else
                    Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = CheckState.Checked
                End If
            ElseIf (e.Control = True And e.KeyCode = Keys.F) Then
                Find_TlStrpBtn_Click(Find_TlStrpBtn, EventArgs.Empty)
            ElseIf e.KeyCode = Keys.F4 And
            KeyDownForFirstTime Then
                CallByName(Me, "Pause_Btn_Click", CallType.Method, Start_Btn, EventArgs.Empty)
            ElseIf (e.Shift = True And e.KeyCode = Keys.F4) Then
                CallByName(Me, "Stop_Btn_Click", CallType.Method, Stop_Btn, EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim Reminders As New List(Of Reminderlist)
    Dim OpenCertainFile As String = Nothing
    Dim doc As New XmlDocument

    Private Sub Preview_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Btn.Click
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Available_MagNotes_DGV.Rows.Clear()
            MagNote_No_CmbBx.Items.Clear()
            If Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Unchecked Then 'عرض الملفات المفتوحة سابقا مع الفئة الحالة
                Dim MC_CmbBx As String = MagNote_Category_CmbBx.SelectedIndex
                LoadOpenedTabPages()
                MagNote_Category_CmbBx.SelectedIndex = MC_CmbBx
            ElseIf Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Indeterminate Then 'عرض الملاحظات المفتوحة سابقا فقط
                LoadOpenedTabPages()
                Exit Sub
            End If
            ' عرض الفئة الحالي فقط
            Dim CategoryiesFileName = MagNotes_Folder_Path_TxtBx.Text & "\Categories.xml"
            Dim fils(0) As String
            If File.Exists(CategoryiesFileName) And
                MagNote_Category_CmbBx.SelectedIndex <> -1 Then
                doc.Load(CategoryiesFileName)
                Dim NodeToSelect = "/Categories/" & Replace(MagNote_Category_CmbBx.SelectedItem, Space(1), "_") & "/MagNote"
                Dim categoryNodes As XmlNodeList = doc.SelectNodes(NodeToSelect)
                Dim FilesToLoad As String
                For Each categoryNode As XmlNode In categoryNodes
                    FilesToLoad = categoryNode.Attributes("Path").Value
                    Array.Resize(fils, fils.Length + 1)
                    fils(fils.Length - 1) = FilesToLoad
                Next
            ElseIf MagNote_Category_CmbBx.SelectedIndex = -1 And
                MagNote_Category_CmbBx.Text = "" Then
                doc.Load(CategoryiesFileName)
                Dim NodeToSelect = "/Categories/" & Replace(MagNote_Category_CmbBx.SelectedItem, Space(1), "_") & "/MagNote"
                Dim categoryNodes As XmlNodeList = doc.SelectNodes(NodeToSelect)
                Dim FilesToLoad As String
                For Each categoryNode As XmlNode In categoryNodes
                    Try
                        FilesToLoad = categoryNode.Attributes("Path").Value
                        Array.Resize(fils, fils.Length + 1)
                        ' Add the new item to the end of the array
                        fils(fils.Length - 1) = FilesToLoad
                    Catch ex As Exception
                    End Try
                Next
            End If

            Dim OutSideOrNew As String
            Dim CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.Count) As DataGridViewRow
            For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg)) = isInDataGridView(TbPg.Tag, "MagNote_Name", Available_MagNotes_DGV, 0, 1)
                If IsNothing(CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg))) Then
                    OutSideOrNew &= TbPg.tag & "," & TbPg.text & vbNewLine
                End If
            Next
            Cursor = Cursors.WaitCursor
            Available_MagNotes_DGV.Rows.Clear()
            Dim SelectedIindex As String
            If MagNote_Category_CmbBx.SelectedIndex = -1 Then
                LoadCategories()
                SelectedIindex = String.Empty
            Else
                SelectedIindex = MagNote_Category_CmbBx.SelectedItem
            End If
            Reminders.Clear()
            Reminders = New List(Of Reminderlist)
            Dim SelectedItem = MagNote_No_CmbBx.Text
            MagNote_No_CmbBx.Items.Clear()
            If IsNothing(fils) Then
                If MagNote_Category_CmbBx.SelectedIndex <> -1 Then
                    Dim CartgoryFiles As String = MagNotes_Folder_Path_TxtBx.Text & "\Category(" & MagNote_Category_CmbBx.SelectedItem & ")Files.txt"
                    If File.Exists(CartgoryFiles) Then
                        fils = Split(Replace(Replace(My.Computer.FileSystem.ReadAllText(CartgoryFiles, System.Text.Encoding.UTF8), vbCrLf, ""), vbLf, ""), ",")
                    Else
                        GoTo OldRead
                    End If
                Else
OldRead:
                    If Not String.IsNullOrEmpty(OpenCertainFile) Then
                        fils = System.IO.Directory.GetFiles(MagNotes_Folder_Path_TxtBx.Text, Path.GetFileName(OpenCertainFile))
                    Else
                        fils = System.IO.Directory.GetFiles(MagNotes_Folder_Path_TxtBx.Text, "MagNote -(*")
                    End If
                End If
            End If

            If Not IsNothing(fils) Then
                Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, fils.Count)
                For Each fil In fils
                    If IsNothing(fil) Then Continue For
                    progress += ProgressToAdd
                    Previewlbl.Text = "Loading--> " & fil & vbNewLine & Math.Floor(progress * 100)
                    Previewlbl.Refresh()
                    Previewlbl.Invalidate()
                    If UCase(Path.GetExtension(fil)) = UCase(".txt") Then
                        If Not String.IsNullOrEmpty(SelectedIindex) Then
                            ReadFile(fil,, 1,,, 1, SelectedIindex)
                            Continue For
                        End If
                        ReadFile(fil,, 1,,, 1, "AllCategories")
                    End If
                Next
            End If
            If Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Checked Then
                GoTo WithoutOpenedNotesAtStartup
            End If
            For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                If Not IsNothing(CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg))) Then
                    If IsNothing(isInDataGridView(TbPg.Tag, "MagNote_Name", Available_MagNotes_DGV, 0, 1)) Then
                        Available_MagNotes_DGV.Rows.Add(CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg)))
                        MagNote_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg)).Cells("MagNote_Name").Value, CurrentOpenedNote(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg)).Cells("MagNote_Label").Value))
                    End If
                End If
            Next
            If Not String.IsNullOrEmpty(OutSideOrNew) Then
                For Each Note In Split(OutSideOrNew, vbNewLine)
                    If Note.Length = 0 Then Continue For
                    MagNote_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Split(Note, ",").ToList.Item(0), Split(Note, ",").ToList.Item(1)))
                Next
            End If
            LoadExternalFiles()
WithoutOpenedNotesAtStartup:
            Available_MagNotes_DGV.Sort(Available_MagNotes_DGV.Columns("Creation_Date"), System.ComponentModel.ListSortDirection.Descending)
            Available_MagNotes_DGV.Sort(Available_MagNotes_DGV.Columns("MagNote_Category"), System.ComponentModel.ListSortDirection.Ascending)
            Available_MagNotes_DGV.ClearSelection()
            If Not IsNothing(MagNotes_Notes_TbCntrl.SelectedTab) Then
                IsInMagNoteCmbBx(MagNotes_Notes_TbCntrl.SelectedTab.Tag, 1, MagNote_No_CmbBx)
            End If
            If Language_Btn.Text = "E" Then
                Msg = "تم الانتهاء من تحميل الملاحظات"
            Else
                Msg = "Loading MagNotes Finished"
            End If

            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            OpenCertainFile = Nothing
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
            Dim AMNF_ChkStt As CheckState
            AMNF_ChkStt = Apply_Multiple_New_Files_ChkBx.CheckState
            Apply_Multiple_New_Files_ChkBx.CheckState = CheckState.Unchecked
            New_Note_TlStrpBtn_Click(New_Note_TlStrpBtn, EventArgs.Empty)
            Apply_Multiple_New_Files_ChkBx.CheckState = AMNF_ChkStt
        End Try
    End Sub

    Private Sub Available_MagNotes_DGV_RowPostPaint(sender As Object,
    e As DataGridViewRowPostPaintEventArgs) Handles Available_MagNotes_DGV.RowPostPaint

        Dim grid As DataGridView = CType(sender, DataGridView)
        Dim rowIdx As String = (e.RowIndex + 1).ToString()
        Dim rowFont As New System.Drawing.Font("Tahoma", 8.0!,
        System.Drawing.FontStyle.Bold,
        System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Dim centerFormat = New StringFormat()
        centerFormat.Alignment = StringAlignment.Far
        centerFormat.LineAlignment = StringAlignment.Near

        Dim headerBounds As Rectangle = New Rectangle(e.RowBounds.Left, e.RowBounds.Top,
        grid.RowHeadersWidth, e.RowBounds.Height)
        e.Graphics.DrawString(rowIdx, rowFont, SystemBrushes.ControlText,
        headerBounds, centerFormat)
    End Sub
    Private Sub LoadCategories()
        MagNote_Category_CmbBx.Items.Clear()
        Dim CategoriesFilePath = MagNotes_Folder_Path_TxtBx.Text & "\Categories.txt"
        If File.Exists(CategoriesFilePath) Then
            Dim CategoryExist As Boolean
            For Each Category In Split(My.Computer.FileSystem.ReadAllText(CategoriesFilePath, System.Text.Encoding.UTF8), vbCrLf)
                If IsNothing(Category) Then Continue For
                If Category.Length > 0 Then
                    MagNote_Category_CmbBx.Items.Add(Category)
                End If
            Next
        End If
    End Sub
    Dim CurrentProcess As String = String.Empty
    Private Sub LoadOpenedTabPages()
        If Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Checked Then
            Exit Sub
        End If
        CurrentProcess = "LoadOpenedTabPages"
        Cursor = Cursors.WaitCursor
        Dim OSINT_ChkBx As CheckState = Open_Note_In_New_Tab_ChkBx.CheckState
        Dim Actvcntrl = ActiveControl
        Try
            Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
            Dim OpenedTbPgsFiles = MagNotes_Folder_Path_TxtBx.Text & "\OpenedTabPages.txt"
            If File.Exists(OpenedTbPgsFiles) Then
                For Each TbPgFiles In Split(My.Computer.FileSystem.ReadAllText(OpenedTbPgsFiles, System.Text.Encoding.UTF8), vbLf)
                    ActiveControl = MagNote_No_CmbBx
                    TbPgFiles = Replace(TbPgFiles, vbCr, "")
                    If Not AddLoadOpenedTabPages(TbPgFiles) Then
                        Continue For
                    End If
                Next
                If Language_Btn.Text = "E" Then
                    Msg = "تم الانتهاء من تحميل الملفات المفتوحة سابقا"
                Else
                    Msg = "Loading Opened Previous Files Finished"
                End If
                ShowWindowsNotification(Msg)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Open_Note_In_New_Tab_ChkBx.CheckState = OSINT_ChkBx
            Cursor = Cursors.Default
            ActiveControl = Actvcntrl
            CurrentProcess = String.Empty
        End Try
    End Sub
    Private Function AddLoadOpenedTabPages(TbPgFiles) As Boolean
        TbPgFiles = Replace(TbPgFiles, vbCr, "")
        If File.Exists(TbPgFiles) Then
            Application.DoEvents()
            If IsInMagNoteCmbBx(TbPgFiles) Then
                Return False
            Else
                If MagNoteFileFormat(TbPgFiles, 1) Then
                    MagNoteFileFormat(TbPgFiles)
                Else
                    SelectFileFormat(TbPgFiles)
                    AddToMagNoteNoCmbBx(TbPgFiles, 1)
                End If
            End If
        End If
    End Function
    Private Function IfOpenedTabPagesChanged() As Boolean
        Cursor = Cursors.WaitCursor
        Try
            Dim OpenedTbPgsFiles = MagNotes_Folder_Path_TxtBx.Text & "\OpenedTabPages.txt"
            Dim CurrentTbPgs, OpenedTbPgs
            If File.Exists(OpenedTbPgsFiles) Then
                For Each TbPgFiles In Split(My.Computer.FileSystem.ReadAllText(OpenedTbPgsFiles, System.Text.Encoding.UTF8), vbLf)
                    TbPgFiles = Replace(TbPgFiles, vbCr, "")
                    If File.Exists(TbPgFiles) Then
                        OpenedTbPgs &= TbPgFiles
                    End If
                Next
            Else
                SaveOpenedTabPages()
                Return False
            End If
            For Each OpenedTbPg In MagNotes_Notes_TbCntrl.TabPages
                If File.Exists(OpenedTbPg.tag) Then
                    CurrentTbPgs &= OpenedTbPg.tag
                End If
            Next
            If OpenedTbPgs <> CurrentTbPgs Then
                Return True
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub LoadExternalFiles()
        Try
            Cursor = Cursors.WaitCursor
            Dim OpenedExternalFiles = MagNotes_Folder_Path_TxtBx.Text & "\OpenedExternalFiles.txt"
            If File.Exists(OpenedExternalFiles) Then
                For Each ExternalFiles In Split(My.Computer.FileSystem.ReadAllText(OpenedExternalFiles, System.Text.Encoding.UTF8), vbLf)
                    ExternalFiles = Replace(ExternalFiles, vbCr, "")
                    If File.Exists(ExternalFiles) Then
                        Application.DoEvents()
                        If IsInMagNoteCmbBx(ExternalFiles, 0) Then
                            Continue For
                        Else
                            AddToMagNoteNoCmbBx(ExternalFiles, 0)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Structure Reminderlist
        Public NoteName As String
        Public NoteLabel As String
        Public ReminderTime As String
        Public Sub New(Name As String, Time As String, Label As String)
            NoteName = Name
            NoteLabel = Label
            ReminderTime = Time
        End Sub
    End Structure
    Dim IgnoreReadFile As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fil"> The File Name to Read with Full Path</param>
    ''' <param name="ReturnMagNoteAray">Return MagNote Aray Only</param>
    ''' <param name="AddFile">Add The File To DGV Only</param>
    ''' <param name="ReplaceDGVR">Replace The This Row With The Row Was Read  Only</param>
    ''' <param name="ReturnMagNoteValue">Return The MagNote Text Contents Only</param>
    ''' <param name="AddToMagNoteCmbBx">Add The File To MagNoteCmbBx Only</param>
    ''' <returns></returns>
    Private Function ReadFile(ByVal fil As String,
                                               Optional ByVal ReturnMagNoteAray As Boolean = False,
                                               Optional ByVal AddFile As Boolean = False,
                                               Optional ByVal UpdateDGVRow As DataGridViewRow = Nothing,
                                                Optional ByVal ReturnMagNoteValue As Boolean = False,
                                                Optional ByVal AddToMagNoteCmbBx As Boolean = False,
                                                Optional ByVal AddByCategory As String = Nothing) As Object
        Try
            Cursor = Cursors.WaitCursor
            If IgnoreReadFile Or Not File.Exists(fil) Then Exit Function
            Dim MagNote_Name_Value,
                    MagNote_Label_Value,
                    MagNote_Category_Value,
                    MagNote_Value,
                    Blocked_Note_Value,
                    Finished_Note_Value,
                    Note_Font_Value,
                    Note_Font_Color_Value,
                    Note_Back_Color_Value,
                    Alternating_Row_Color_Value,
                    Creation_Date_Value,
                    Secured_Note_Value,
                    Note_Password_Value,
                    Use_Main_Password_Value,
                    MagNote_RTF_Value,
                    Note_Word_Wrap_Value,
                    Note_Have_Reminder_Value,
                    Next_Reminder_Time_Value,
                    Reminder_Every_Value,
                    MagNote_Grid_Value,
                    Grid_Panel_Size_Value
            Dim MagNote() = Split(Replace(My.Computer.FileSystem.ReadAllText(fil, System.Text.Encoding.UTF8), vbCrLf, ""), ":")
            Dim ReadNote As String = String.Empty
            Dim CaseNote As String = String.Empty
            For Each Note In MagNote
                If String.IsNullOrEmpty(Note) Then Continue For
                If Microsoft.VisualBasic.Left(Note, Len("MagNote_Name -(")) & Microsoft.VisualBasic.Right(Note, 2) = "MagNote_Name -()-" Then
                    ReadNote = "MagNote_Name"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("MagNote_Label -(")) & Microsoft.VisualBasic.Right(Note, 2) = "MagNote_Label -()-" Then
                    ReadNote = "MagNote_Label"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("MagNote_Category -(")) & Microsoft.VisualBasic.Right(Note, 2) = "MagNote_Category -()-" Then
                    ReadNote = "MagNote_Category"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("MagNote -(")) & Microsoft.VisualBasic.Right(Note, 2) = "MagNote -()-" Then
                    ReadNote = "MagNote"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Blocked_Note -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Blocked_Note -()-" Then
                    ReadNote = "Blocked_Note"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Finished_Note -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Finished_Note -()-" Then
                    ReadNote = "Finished_Note"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Font -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Font -()-" Then
                    ReadNote = "Note_Font"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Font_Color -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Font_Color -()-" Then
                    ReadNote = "Note_Font_Color"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Back_Color -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Back_Color -()-" Then
                    ReadNote = "Note_Back_Color"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Alternating_Row_Color -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Alternating_Row_Color -()-" Then
                    ReadNote = "Alternating_Row_Color"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Creation_Date -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Creation_Date -()-" Then
                    ReadNote = "Creation_Date"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Secured_Note -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Secured_Note -()-" Then
                    ReadNote = "Secured_Note"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Password -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Password -()-" Then
                    ReadNote = "Note_Password"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Use_Main_Password -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Use_Main_Password -()-" Then
                    ReadNote = "Use_Main_Password"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Word_Wrap -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Word_Wrap -()-" Then
                    ReadNote = "Note_Word_Wrap"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Note_Have_Reminder -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Note_Have_Reminder -()-" Then
                    ReadNote = "Note_Have_Reminder"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Next_Reminder_Time -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Next_Reminder_Time -()-" Then
                    ReadNote = "Next_Reminder_Time"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Reminder_Every -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Reminder_Every -()-" Then
                    ReadNote = "Reminder_Every"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("MagNote_Grid -(")) & Microsoft.VisualBasic.Right(Note, 2) = "MagNote_Grid -()-" Then
                    ReadNote = "MagNote_Grid"
                ElseIf Microsoft.VisualBasic.Left(Note, Len("Grid_Panel_Size -(")) & Microsoft.VisualBasic.Right(Note, 2) = "Grid_Panel_Size -()-" Then
                    ReadNote = "Grid_Panel_Size"
                End If
                Select Case ReadNote
                    Case "MagNote_Name"
                        MagNote_Name_Value = fil
                        ReadNote = String.Empty
                    Case "MagNote_Label"
                        MagNote_Label_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("MagNote_Label -(")), "")
                        MagNote_Label_Value = Microsoft.VisualBasic.Left(MagNote_Label_Value, MagNote_Label_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "MagNote_Category"
                        MagNote_Category_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("MagNote_Category -(")), "")
                        MagNote_Category_Value = Microsoft.VisualBasic.Left(MagNote_Category_Value, MagNote_Category_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "MagNote"
                        MagNote_RTF_Value = Note
                        MagNote_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("MagNote -(")), "")
                        MagNote_Value = Microsoft.VisualBasic.Left(MagNote_Value, MagNote_Value.Length - 2)
                        MagNote_Value = MagNote_Value
                        MagNote_RTF_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("MagNote -(")), "")
                        MagNote_RTF_Value = Microsoft.VisualBasic.Left(MagNote_RTF_Value, MagNote_RTF_Value.Length - 2)
                        MagNote_RTF_Value = MagNote_RTF_Value
                        ReadNote = String.Empty
                    Case "Blocked_Note"
                        Blocked_Note_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Blocked_Note -(")), "")
                        Blocked_Note_Value = Microsoft.VisualBasic.Left(Blocked_Note_Value, Blocked_Note_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Finished_Note"
                        Finished_Note_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Finished_Note -(")), "")
                        Finished_Note_Value = Microsoft.VisualBasic.Left(Finished_Note_Value, Finished_Note_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Note_Font"
                        Note_Font_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Font -(")), "")
                        Note_Font_Value = Microsoft.VisualBasic.Left(Note_Font_Value, Note_Font_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Note_Font_Color"
                        Note_Font_Color_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Font_Color -(")), "")
                        Note_Font_Color_Value = Microsoft.VisualBasic.Left(Note_Font_Color_Value, Note_Font_Color_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Note_Back_Color"
                        Note_Back_Color_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Back_Color -(")), "")
                        Note_Back_Color_Value = Microsoft.VisualBasic.Left(Note_Back_Color_Value, Note_Back_Color_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Alternating_Row_Color"
                        Alternating_Row_Color_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Alternating_Row_Color -(")), "")
                        Alternating_Row_Color_Value = Microsoft.VisualBasic.Left(Alternating_Row_Color_Value, Alternating_Row_Color_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Creation_Date"
                        Note = Replace(Note, ",", ":")
                        Creation_Date_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Creation_Date -(")), "")
                        Creation_Date_Value = Microsoft.VisualBasic.Left(Creation_Date_Value, Creation_Date_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Secured_Note"
                        Secured_Note_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Secured_Note -(")), "")
                        Secured_Note_Value = Microsoft.VisualBasic.Left(Secured_Note_Value, Secured_Note_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Note_Password"
                        Note_Password_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Password -(")), "")
                        Note_Password_Value = Microsoft.VisualBasic.Left(Note_Password_Value, Note_Password_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Use_Main_Password"
                        Use_Main_Password_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Use_Main_Password -(")), "")
                        Use_Main_Password_Value = Microsoft.VisualBasic.Left(Use_Main_Password_Value, Use_Main_Password_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Note_Word_Wrap"
                        Note_Word_Wrap_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Word_Wrap -(")), "")
                        Note_Word_Wrap_Value = Microsoft.VisualBasic.Left(Note_Word_Wrap_Value, Note_Word_Wrap_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Note_Have_Reminder"
                        Note_Have_Reminder_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Note_Have_Reminder -(")), "")
                        Note_Have_Reminder_Value = Microsoft.VisualBasic.Left(Note_Have_Reminder_Value, Note_Have_Reminder_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Next_Reminder_Time"
                        Next_Reminder_Time_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Next_Reminder_Time -(")), "")
                        Next_Reminder_Time_Value = Microsoft.VisualBasic.Left(Next_Reminder_Time_Value, Next_Reminder_Time_Value.Length - 2)
                        ReadNote = String.Empty
                    Case "Reminder_Every"
                        Reminder_Every_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Reminder_Every -(")), "")
                        Reminder_Every_Value = Microsoft.VisualBasic.Left(Reminder_Every_Value, Reminder_Every_Value.Length - 2)
                        Reminder_Every = Reminder_Every_Value
                        ReadNote = String.Empty
                    Case "MagNote_Grid"
                        MagNote_Grid_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("MagNote_Grid -(")), "")
                        MagNote_Grid_Value = Microsoft.VisualBasic.Left(MagNote_Grid_Value, MagNote_Grid_Value.Length - 2)
                        MagNote_Grid_Value = Replace(MagNote_Grid_Value, "(Instead Of Colon)", ":")
                        ReadNote = String.Empty
                    Case "Grid_Panel_Size"
                        Grid_Panel_Size_Value = Replace(Note, Microsoft.VisualBasic.Left(Note, Len("Grid_Panel_Size -(")), "")
                        Grid_Panel_Size_Value = Microsoft.VisualBasic.Left(Grid_Panel_Size_Value, Grid_Panel_Size_Value.Length - 2)
                        If Not ReturnMagNoteAray Then
                            Grid_Panel_Size_TxtBx.Text = Grid_Panel_Size_Value
                        End If
                        ReadNote = String.Empty
                End Select
            Next



            If ReturnMagNoteAray Then
                GoTo ReturnMagNoteAraySub
            End If
            If IsNothing(MagNote_Grid_Value) Then
                Grid_Panel_Size_TxtBx.Text = Nothing
            End If
            If Not CType(Val(Secured_Note_Value), Boolean) Then
                MagNote_Value = ConvertRichTextBox(Decrypt_Function(MagNote_Value))
                MagNote_RTF_Value = Decrypt_Function(MagNote_RTF_Value)
            End If

            If Hide_Finished_MagNote_ChkBx.CheckState = CheckState.Checked And
            Finished_Note_Value = 1 And String.IsNullOrEmpty(ExternalFileName) Then
                GoTo isInDataGridViewTrue
            End If
            Dim DoAddFile As Boolean = True
            If AddFile Then
                If Not String.IsNullOrEmpty(AddByCategory) Then
                    If MagNote_Category_Value <> AddByCategory And
                    Not AddByCategory.Equals("AllCategories") Then
                        DoAddFile = False
                    End If
                End If
                If DoAddFile Then
                    If IsNothing(isInDataGridView(MagNote_Name_Value, "MagNote_Name", Available_MagNotes_DGV)) Then
                        If String.IsNullOrEmpty(MagNote_Name_Value) Then
                            Exit Function
                        End If
                        Available_MagNotes_DGV.Rows.Add(MagNote_Name_Value, MagNote_Label_Value, MagNote_Category_Value, MagNote_Value, Blocked_Note_Value, Finished_Note_Value, Note_Font_Value, Note_Font_Color_Value, Note_Back_Color_Value, Alternating_Row_Color_Value, Creation_Date_Value, Secured_Note_Value, Note_Password_Value, Use_Main_Password_Value, MagNote_RTF_Value, Note_Word_Wrap_Value, Note_Have_Reminder_Value, Next_Reminder_Time_Value, Reminder_Every_Value, MagNote_Grid_Value, Grid_Panel_Size_Value)
                    End If
                End If
            End If
ReturnMagNoteAraySub:
            If ReturnMagNoteAray Or Not IsNothing(UpdateDGVRow) Then
                Dim array() As String = {MagNote_Name_Value, MagNote_Label_Value, MagNote_Category_Value, MagNote_Value, Blocked_Note_Value, Finished_Note_Value, Note_Font_Value, Note_Font_Color_Value, Note_Back_Color_Value, Alternating_Row_Color_Value, Creation_Date_Value, Secured_Note_Value, Note_Password_Value, Use_Main_Password_Value, MagNote_RTF_Value, Note_Word_Wrap_Value, Note_Have_Reminder_Value, Next_Reminder_Time_Value, Reminder_Every_Value, MagNote_Grid_Value, Grid_Panel_Size_Value}
                If Not IsNothing(UpdateDGVRow) Then
                    Available_MagNotes_DGV.ReadOnly = False
                    For Each Cell As DataGridViewCell In UpdateDGVRow.Cells
                        Available_MagNotes_DGV.Rows(UpdateDGVRow.Index).Cells(Cell.ColumnIndex).Value = array(Cell.ColumnIndex)
                    Next
                    Available_MagNotes_DGV.ReadOnly = True
                Else
                    Return array
                End If
            End If
            If ReturnMagNoteValue Then
                Return MagNote_Category_Value
            End If

            If AddToMagNoteCmbBx Then
                ComingFromReadFile = True
                If String.IsNullOrEmpty(AddByCategory) Then
                    AddToMagNoteNoCmbBx(fil)
                ElseIf DoAddFile Then
                    AddToMagNoteNoCmbBx(fil, 0)
                End If
            End If
isInDataGridViewTrue:
            If CType(Val(Note_Have_Reminder_Value), Boolean) Then
                Dim RmindrIsExist As Boolean
                For Each Remind In Reminders
                    If Remind.NoteName = MagNote_Name_Value Then
                        RmindrIsExist = True
                        Exit For
                    End If
                Next
                If Not RmindrIsExist Then
                    Reminders.Add(New Reminderlist(MagNote_Name_Value, Next_Reminder_Time_Value, MagNote_Label_Value))
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            ComingFromReadFile = False
            Cursor = Cursors.Default
        End Try
    End Function
    Dim Reminder_Every

    Private Sub Available_MagNotes_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_MagNotes_DGV.CellContentClick
    End Sub
    Public CurrentDGVRow As DataGridViewRow
    Public Sub Available_MagNotes_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Available_MagNotes_DGV.SelectionChanged
        Try
            If Available_MagNotes_DGV.MultiSelect = True Then Exit Sub
            Cursor = Cursors.WaitCursor
            SaveDialogResultAnseredIsNo = False
            Dim ShowNote As String = String.Empty
            If IsNothing(ActiveControl) Or
                sender.SelectedRows.Count = 0 Or
                IsNothing(MagNotes_Notes_TbCntrl.SelectedTab) Then
                Exit Sub
            End If
            If CurrentRowNotEqualRowIndex(sender, 1) Then
                Exit Sub
            End If
            If ActiveControl.Name = Note_Password_TxtBx.Name Then
                ShowNote = "ShowWithDecrypt"
            End If
            If CommingFromSaveToolStripButton Then GoTo CFSTSB
            If Not IsNothing(Available_MagNotes_DGV.CurrentRow) Then
                If Available_MagNotes_DGV.CurrentRow.Selected Then
                    CurrentDGVRow = Available_MagNotes_DGV.CurrentRow
                End If
            End If
            If (ActiveControl.Name <> sender.name And
                ActiveControl.Name <> Previous_Btn.Name And
                ActiveControl.Name <> Next_Btn.Name And
                ActiveControl.Name <> MagNote_No_CmbBx.Name) And
                Not ExitingProgram Then
                Exit Sub
            End If
            If IsNothing(CurrentDGVRow) Or
                 ExitingProgram Then
                Exit Sub
            End If
CFSTSB:
            If MagNotes_Notes_TbCntrl.SelectedTab.Tag <> Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Name").Value And ActiveControl.Name <> sender.Name Then
                Exit Sub
            ElseIf ActiveControl.Name = sender.Name Then
                If Not IsInMagNoteCmbBx(Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Name").Value, 1) Then
                    ReadFile(Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Name").Value,,,,, 1)
                    Exit Sub
                End If
            End If
            If ActiveControl.Name = sender.name Then
                RefreshNoteSetting(, Available_MagNotes_DGV.CurrentRow, 1, ShowNote)
            Else
                RefreshNoteSetting(, Available_MagNotes_DGV.CurrentRow, 0, ShowNote)
            End If
            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            If Available_MagNotes_DGV.SelectedRows.Count = 0 Then
                CurrentDGVRow = Nothing
            End If
        End Try
    End Sub

    Public Function RefreshNoteSetting(
                                        Optional ByVal MagNoteName As String = Nothing,
                                        Optional ByVal DGVRow As DataGridViewRow = Nothing,
                                        Optional ByVal RefreshTbxBx As Boolean = False,
                                        Optional ByVal ShowNote As String = Nothing,
                                        Optional ByVal AsNoteAmendmented As Boolean = False) As Boolean
        Try
            If IsNothing(DGVRow) Then
                DGVRow = isInDataGridView(MagNoteName, "MagNote_Name", Available_MagNotes_DGV, 0, 1)
            End If
            If IsNothing(DGVRow) Then
                If AsNoteAmendmented Then
                    RefreshNotExistingFile()
                End If
                Return False
            End If
            If ActiveControl.Name = Note_Password_TxtBx.Name Then
                ShowNote = "ShowWithDecrypt"
                GoTo SecuredNote
            End If

            If ActiveControl.Name <> MagNote_No_CmbBx.Name And
                    ActiveControl.Name <> Previous_Btn.Name And
                    ActiveControl.Name <> Next_Btn.Name Then
                IsInMagNoteCmbBx(DGVRow.Cells("MagNote_Name").Value, 1)
            End If

            MagNote_Category_CmbBx.Text = DGVRow.Cells("MagNote_Category").Value
            Secured_Note_ChkBx.CheckState = DGVRow.Cells("Secured_Note").Value
            Use_Main_Password_ChkBx.CheckState = DGVRow.Cells("Use_Main_Password").Value
            Dim UseMainPassword As CheckState
            If String.IsNullOrEmpty(DGVRow.Cells("Use_Main_Password").Value) Then
                UseMainPassword = CheckState.Unchecked
            Else
                UseMainPassword = CType(DGVRow.Cells("Use_Main_Password").Value, CheckState)
            End If
            If CType(DGVRow.Cells("Secured_Note").Value, CheckState) = CheckState.Checked And
                        Not PassedMainPasswordToPass Then
NotUseMainPassword:
                If RefreshTbxBx Then RCSN.Text = Nothing
                If Note_Password_TxtBx.TextLength = 0 Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "This Note Is Secured... Kindly Enter Note Password To Read It"
                    Else
                        Msg = "هذه الملاحظة مؤمنة بكلمة سر... من فضلك أدخل كلمة سر الملاحظة لإمكانية قراءتها"
                    End If
                    Note_Password_TxtBx.ReadOnly = False
                    ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0,,,, 0)
                    GoTo SecuredNote
                ElseIf Decrypt_Function(DGVRow.Cells("Note_Password").Value) <> Note_Password_TxtBx.Text Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Wrong Password"
                    Else
                        Msg = "كلمة السر خطأ"
                    End If
                    ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, False)
                    GoTo SecuredNote
                End If
                ShowNote = "ShowWithDecrypt"
            ElseIf PassedMainPasswordToPass And
                    CType(DGVRow.Cells("Secured_Note").Value, CheckState) = CheckState.Checked And
                    UseMainPassword = CheckState.Checked Then
                ShowNote = "ShowWithDecrypt"
            ElseIf PassedMainPasswordToPass And
                    CType(DGVRow.Cells("Secured_Note").Value, CheckState) = CheckState.Checked And
                    UseMainPassword = CheckState.Unchecked Then
                GoTo NotUseMainPassword
            Else
                ShowNote = "Show"
            End If
SecuredNote:
            Note_Font_TxtBx.Text = Nothing
            Note_Font_Color_ClrCmbBx.Text = Nothing
            Note_Back_Color_ClrCmbBx.Text = Nothing
            If Not IsNothing(DGVRow.Cells("Note_Font").Value) Then
                Note_Font_TxtBx.Text = DGVRow.Cells("Note_Font").Value
            Else
                Note_Font_TxtBx.Text = RCSN.Font.Name & " - " & RCSN.Font.Style & " - " & RCSN.Font.Size
            End If
            If Not IsNothing(DGVRow.Cells("Note_Font_Color").Value) Then
                Note_Font_Color_ClrCmbBx.Text = DGVRow.Cells("Note_Font_Color").Value
            Else
                Note_Font_Color_ClrCmbBx.Text = RCSN.ForeColor.Name
            End If
            If Not String.IsNullOrEmpty(DGVRow.Cells("Note_Back_Color").Value) Then
                Note_Back_Color_ClrCmbBx.Text = DGVRow.Cells("Note_Back_Color").Value
            Else
                Note_Back_Color_ClrCmbBx.Text = RCSN.BackColor.Name
            End If

            If Not IsNothing(DGVRow.Cells("Alternating_Row_Color").Value) Then
                Alternating_Row_Color_ClrCmbBx.Text = DGVRow.Cells("Alternating_Row_Color").Value
            Else
                Alternating_Row_Color_ClrCmbBx.Text = External_Note_Alternating_Row_Color_ClrCmbBx.Text
            End If
            Application.DoEvents()
            If RefreshTbxBx Then
                If Not IsNothing(DGVRow.Cells("MagNote_Grid").Value) Then
                    Dim SourceFile = DGVRow.Cells("MagNote_Grid").Value
                    Dim DestintionFile = Replace(SourceFile, ".txt", ".xlsx")
                    If File.Exists(SourceFile) Then
                        If Decrypt(SourceFile, DestintionFile) Then
                            If File.Exists(DestintionFile) Then
                                LoadFile(DestintionFile)
                                File.Delete(DestintionFile)
                            End If
                        End If
                    Else
                        If Language_Btn.Text = "E" Then
                            Msg = "ملف الجداول غير موجود على هذا المجلد (" & SourceFile & ")"
                        Else
                            Msg = "The ReoGrid File (" & SourceFile & ") Not Found In This Path..."
                        End If
                        ShowMsg(Msg,,, MessageBoxIcon.Exclamation)
                    End If
                End If
                If ShowNote = "ShowWithDecrypt" Then
                    Try
                        RCSN.Rtf = Decrypt_Function(DGVRow.Cells("MagNote").Value)
                    Catch ex As Exception
                        RCSN.Text = ConvertRichTextBox(Decrypt_Function(DGVRow.Cells("MagNote_RTF").Value))
                    End Try
                ElseIf ShowNote = "Show" Then
                    Try
                        RCSN.Rtf = DGVRow.Cells("MagNote_RTF").Value
                        Application.DoEvents()
                    Catch ex As Exception
                        RCSN.Text = DGVRow.Cells("MagNote").Value
                    End Try
                End If
            End If
            Note_Have_Reminder_ChkBx.CheckState = DGVRow.Cells("Note_Have_Reminder").Value
            If Not String.IsNullOrEmpty(DGVRow.Cells("Next_Reminder_Time").Value) Then
                Dim dt As DateTime = DateTime.ParseExact(DGVRow.Cells("Next_Reminder_Time").Value, "yyyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)
                Next_Reminder_Time_DtTmPkr.Value = dt
            End If

            If Not String.IsNullOrEmpty(DGVRow.Cells("Reminder_Every").Value) Then
                Dim Reminder() = Split(DGVRow.Cells("Reminder_Every").Value, ",")
                Reminder_Every_Days_NmrcUpDn.Value = Reminder(0)
                Reminder_Every_Hours_NmrcUpDn.Value = Reminder(1)
                Reminder_Every_Minutes_NmrcUpDn.Value = Reminder(2)
            End If
            Blocked_Note_ChkBx.CheckState = DGVRow.Cells("Blocked_Note").Value
            Finished_Note_ChkBx.CheckState = DGVRow.Cells("Finished_Note").Value
            Note_Word_Wrap_ChkBx.CheckState = DGVRow.Cells("Note_Word_Wrap").Value

            CheckStateChanged(Note_Word_Wrap_ChkBx, EventArgs.Empty)
            CheckStateChanged(Use_Main_Password_ChkBx, EventArgs.Empty)
            CheckStateChanged(Note_Have_Reminder_ChkBx, EventArgs.Empty)
            CheckStateChanged(Blocked_Note_ChkBx, EventArgs.Empty)
            CheckStateChanged(Finished_Note_ChkBx, EventArgs.Empty)
            CheckStateChanged(Secured_Note_ChkBx, EventArgs.Empty)
        Finally
            If RCSN.SelectedText.Length = 0 Then
                RCSN.SelectionStart = 0
            End If
            Application.DoEvents()
        End Try
    End Function
    Private Function IsInMagNoteCmbBx(ByVal SearchString As String,
                                         Optional ByVal SelectItem As Boolean = False,
                                         Optional CmbBx As ComboBox = Nothing,
                                         Optional ByVal RetuenKey As Boolean = False,
                                         Optional ByVal RetuenSelectedItem As Boolean = False,
                                         Optional ByVal FindInValue As Boolean = False,
                                         Optional ByVal ActivateCmbBx As Boolean = False,
                                         Optional ByVal ClearSelection As Boolean = False) As Object
        Try
            If SelectItem Or RetuenSelectedItem Then
                If Not IsNothing(CmbBx) And ClearSelection Then
                    CmbBx.SelectedIndex = -1
                End If
            End If
            Dim SelectedItem
            If IsNothing(CmbBx) Then
                Dim CurrentActiveControl
                If ActivateCmbBx Then
                    CurrentActiveControl = Me.ActiveControl
                    Me.ActiveControl = MagNote_No_CmbBx
                    MagNote_No_CmbBx.Focus()
                End If
                SelectedItem = MagNote_No_CmbBx.Items.Cast(Of KeyValuePair(Of String, String))().FirstOrDefault(Function(r) r.Key.Equals(SearchString))
                If Not IsNothing(SelectedItem.Key) And SelectItem Then
                    MagNote_No_CmbBx.SelectedItem = SelectedItem
                ElseIf Not IsNothing(SelectedItem.Key) And Not SelectItem And Not RetuenKey And Not RetuenSelectedItem Then
                    Return True
                ElseIf Not IsNothing(SelectedItem.Key) And RetuenKey Then
                    Return SelectedItem.Key
                ElseIf RetuenSelectedItem Then
                    Return SelectedItem
                End If
                If ActivateCmbBx Then
                    Me.ActiveControl = CurrentActiveControl
                    CurrentActiveControl.Focus()
                End If
            Else
                If FindInValue Then
                    SelectedItem = CmbBx.Items.Cast(Of KeyValuePair(Of String, String))().FirstOrDefault(Function(r) r.Value.Equals(SearchString))
                Else
                    SelectedItem = CmbBx.Items.Cast(Of KeyValuePair(Of String, String))().FirstOrDefault(Function(r) r.Key.Equals(SearchString))
                End If
                If Not IsNothing(SelectedItem.Key) And SelectItem Then
                    Select Case SelectedItem.Key
                        Case "rtf"
                            If Language_Btn.Text = "E" Then
                                CmbBx.Text = "مربع نص منسق (rtf)"
                            Else
                                CmbBx.Text = "Rich Text Box (rtf)"
                            End If
                        Case "txt"
                            If Not MagNoteFileFormat(, 1) Then
                                If Language_Btn.Text = "E" Then
                                    CmbBx.Text = "ملاحظة (txt)"
                                Else
                                    CmbBx.Text = "Text Document (txt)"
                                End If
                            Else
                                CmbBx.SelectedItem = SelectedItem
                            End If
                        Case Else
                            CmbBx.SelectedItem = SelectedItem
                    End Select
                ElseIf Not IsNothing(SelectedItem.Key) And Not SelectItem And Not RetuenKey Then
                    Return True
                ElseIf Not IsNothing(SelectedItem.Key) And RetuenKey Then
                    Return SelectedItem.Key
                End If
            End If
            If RetuenKey Then
                Return Nothing
            Else
                If IsNothing(SelectedItem.key) Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            If RetuenKey Then
                Return Nothing
            Else
                Return False
            End If
        End Try
    End Function

    Public Function WriteExcelFile(ByVal SNG As Byte()) As Boolean
        Dim FileName = Application.StartupPath & "\MagNotes_Files\MagNote_Grid.xlsx"
        If File.Exists(FileName) Then
            File.Delete(FileName)
        End If
        My.Computer.FileSystem.WriteAllBytes(FileName, SNG, 0)
    End Function
    Private Function UnicodeStringToBytes(ByVal str As String) As Byte()
        Return System.Text.Encoding.Unicode.GetBytes(str)
    End Function

    Private Link As New LinkLabel()
    Dim FileCount As String = Nothing
    Dim OpenFileDialog1 As SaveFileDialog
    Dim CreatProjectEvent As Boolean
    Public Sub Add_New_MagNote_Click(sender As Object, e As EventArgs) Handles Add_New_MagNote_Btn.Click
        Dim OSINT_ChkBx As CheckState = Open_Note_In_New_Tab_ChkBx.CheckState
        Try
            MagNote_No_CmbBx.SelectedIndex = -1
            Cursor = Cursors.WaitCursor
            If MagNote_Category_CmbBx.Text = Nothing Then
                CheckPublicCategory()
            End If
            If DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value <> "MagNote (txt)" And
                DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value <> "ملاحظة (txt)" Then
                Dim MyDialogResult As DialogResult
                OpenFileDialog1 = New SaveFileDialog
                Dim Extension = DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                OpenFileDialog1.FileName = "New " & Extension & " Document." & Extension
                OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                OpenFileDialog1.Filter = "Currnt File|" & OpenFileDialog1.FileName & "|RTF Files|*.rtf|Text Files|*.txt|All files|*.*"
                If OpenFileDialog1.ShowDialog(Me) = DialogResult.Cancel Then
                    Exit Sub
                End If
                MagNote_No_CmbBx.SelectedIndex = -1
                CreateOutsideMagNoteCategory()
                CreateOutsideMagNote() 'Open the folder if not exist
                MagNote_No_CmbBx.Text = OpenFileDialog1.FileName
                If MagNote_No_CmbBx.SelectedIndex <> -1 Then
                    If DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = OpenFileDialog1.FileName Then
                        If Language_Btn.Text = "E" Then
                            Msg = "هذا الملف مسجل ضمن بيانات البرنامج سابقا"
                        Else
                            Msg = "This File Already Recorded In Program Information Previously"
                        End If
                        ShowMsg(Msg,,, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
                AddToMagNoteNoCmbBx(OpenFileDialog1.FileName)
                If MagNote_No_CmbBx.SelectedIndex = -1 Then
                    MagNote_No_CmbBx.Text = Nothing
                    MagNote_No_CmbBx.Text = Path.GetFileName(OpenFileDialog1.FileName)
                End If
                Exit Sub
            End If
            Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
            FileCount = Generate_New_MagNote_Name()
            If Apply_Multiple_New_Files_ChkBx.CheckState = CheckState.Unchecked Then
                If IsInMagNoteCmbBx(MagNoteFolderPath & "\MagNote -(" & FileCount & ")-." & DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key, 0) Then
                    GoTo NotApplyMultipleNewFiles
                End If
            End If
            While IsInMagNoteCmbBx(MagNoteFolderPath & "\MagNote -(" & FileCount & ")-." & DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key, 0)
                FileCount += 1
            End While
            Dim NewFileName = MagNoteFolderPath & "\MagNote -(" & FileCount & ")-." & DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            While File.Exists(NewFileName)
                FileCount += 1
                NewFileName = MagNoteFolderPath & "\MagNote -(" & FileCount & ")-." & DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            End While
NotApplyMultipleNewFiles:
            MagNote_No_CmbBx.SelectedIndex = -1
            If CreatProjectEvent Then
                MagNote_No_CmbBx.Text = "Project Event"
            Else
                MagNote_No_CmbBx.Text = "MagNote -(" & FileCount & ")-"
            End If
            If MagNote_No_CmbBx.SelectedIndex <> -1 Then
                FileNameEqualToFileAlreadyExistIn_MagNote_No_CmbBx = "MagNote -(" & FileCount & ")-"
                MagNote_No_CmbBx.SelectedIndex = -1
            End If
            ActiveControl = MagNote_No_CmbBx
            If CreatProjectEvent Then
                AddToMagNoteNoCmbBx(MagNoteFolderPath & "\Project Event.txt", 1)
            Else
                AddToMagNoteNoCmbBx(MagNoteFolderPath & "\" & "MagNote -(" & FileCount & ")-." & IsInMagNoteCmbBx(DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key,, File_Format_CmbBx, 1), 1)
            End If
            RCSN.Text = Nothing
            Blocked_Note_ChkBx.CheckState = CheckState.Unchecked
            Finished_Note_ChkBx.CheckState = CheckState.Unchecked
            Note_Word_Wrap_ChkBx.CheckState = CheckState.Checked
            If External_Note_Back_Color_ClrCmbBx.SelectedIndex <> -1 Then
                Note_Back_Color_ClrCmbBx.Text = External_Note_Back_Color_ClrCmbBx.Text
            End If
            If External_Note_Font_Color_ClrCmbBx.SelectedIndex <> -1 Then
                Note_Font_Color_ClrCmbBx.Text = External_Note_Font_Color_ClrCmbBx.Text
            End If
            If External_Note_Font_TxtBx.TextLength <> 0 Then
                RCSN.Font = GetFontByString(External_Note_Font_TxtBx.Text)
            End If
            If External_Note_Alternating_Row_Color_ClrCmbBx.SelectedIndex <> -1 Then
                Alternating_Row_Color_ClrCmbBx.Text = External_Note_Alternating_Row_Color_ClrCmbBx.Text
            End If

            Correct_MagNote_TxtBx_Font_Color()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            AddMagNoteRTF()
            Application.DoEvents()
            Open_Note_In_New_Tab_ChkBx.CheckState = OSINT_ChkBx
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function Correct_MagNote_TxtBx_Font_Color(Optional ByVal ReturnDarkOrLight As Boolean = False)
        Dim brightness As Single = Note_Back_Color_ClrCmbBx.BackColor.GetBrightness
        If ReturnDarkOrLight Then
            Return brightness
        End If
        If brightness < 0.4 Then
            Note_Font_Color_ClrCmbBx.Text = "Window"
        Else
            Note_Font_Color_ClrCmbBx.Text = "WindowText"
        End If
    End Function

    ''' <summary>
    ''' NSNFC = New_MagNote_File_Count
    ''' </summary>
    ''' <returns></returns>
    Private Function Generate_New_MagNote_Name(Optional ByVal GetSerial As Boolean = False)
        Try
            Cursor = Cursors.WaitCursor
            SaveDialogResultAnseredIsNo = False
            Available_MagNotes_DGV.ClearSelection()
            If Not GetSerial Then
                MagNote_No_CmbBx.Text = Nothing
                MagNote_No_CmbBx.SelectedIndex = -1
            End If
            Dim NSNFC = 0
            Dim Files() As String = System.IO.Directory.GetFiles(MagNoteFolderPath)
            For Each file In Files
                Dim F = Path.GetFileNameWithoutExtension(file)
                Dim filename = Microsoft.VisualBasic.Left(F, Len("MagNote -(")) & Microsoft.VisualBasic.Right(Path.GetFileName(F), 2)
                If Path.GetExtension(file) = ".txt" And
                    filename = "MagNote -()-" Then
                    NSNFC += 1
                End If
            Next
            Dim CurrentFile = MagNoteFolderPath & "\MagNote -(" & NSNFC & ")-.txt"
            If File.Exists(CurrentFile) Then
                NSNFC = 0
            End If
            While File.Exists(CurrentFile)
                NSNFC += 1
                CurrentFile = MagNoteFolderPath & "\MagNote -(" & NSNFC & ")-.txt"
            End While
            Return NSNFC
        Finally
            Cursor = Cursors.Default
        End Try
    End Function
    Private Function Generate_New_Shortcut_Name()
        Try
            Dim ShortcutsFolderPath = MagNoteFolderPath & "\Shortcuts"
            Cursor = Cursors.WaitCursor '
            Dim NSNFC = 0
            Dim Files() As String = System.IO.Directory.GetFiles(ShortcutsFolderPath)
            For Each file In Files
                Dim F = Path.GetFileNameWithoutExtension(file)
                Dim filename = Microsoft.VisualBasic.Left(F, Len("Shortcut -(")) & Microsoft.VisualBasic.Right(Path.GetFileName(F), 2)
                If Path.GetExtension(file) = ".lnk" And
                    filename = "Shortcut -()-" Then
                    NSNFC += 1
                End If
            Next
            Dim CurrentFile = ShortcutsFolderPath & "\Shortcut -(" & NSNFC & ")-"
            If File.Exists(CurrentFile) Then
                NSNFC = 0
            End If
            While File.Exists(CurrentFile)
                NSNFC += 1
                CurrentFile = ShortcutsFolderPath & "\Shortcut -(" & NSNFC & ")-"
            End While
            Return "Shortcut -(" & NSNFC & ")-"
        Finally
            Cursor = Cursors.Default
        End Try
    End Function
    Private Function LabelingForm(Optional ByVal Language As String = "Arabic")
        Try
            Labeling_Form(Me, Language)
            Cursor = Cursors.WaitCursor
            If Language = "Arabic" Then
                Available_MagNotes_DGV.Columns("MagNote_Name").HeaderText = "إسم الملاحظة"
                Available_MagNotes_DGV.Columns("MagNote_Label").HeaderText = "عنوان الملاحظة"
                Available_MagNotes_DGV.Columns("MagNote_Category").HeaderText = "فئة الملاحظة"
                Available_MagNotes_DGV.Columns("MagNote").HeaderText = "الملاحظة"
                Available_MagNotes_DGV.Columns("Blocked_Note").HeaderText = "ملاحظة محظورة"
                Available_MagNotes_DGV.Columns("Finished_Note").HeaderText = "ملاحظة منتهية"
                Available_MagNotes_DGV.Columns("Note_Font").HeaderText = "خط الملاحظة"
                Available_MagNotes_DGV.Columns("Note_Font_Color").HeaderText = "لون خط الملاحظة"
                Available_MagNotes_DGV.Columns("Note_Back_Color").HeaderText = "لون خلفية الملاحظة"
                Available_MagNotes_DGV.Columns("Alternating_Row_Color").HeaderText = "لون الخط البديل فى الجداول"
                Available_MagNotes_DGV.Columns("Creation_Date").HeaderText = "تاريخ الإنشاء"
                Available_MagNotes_DGV.Columns("Secured_Note").HeaderText = "ملاحظة مؤمنة بكلمة سر"
                Available_MagNotes_DGV.Columns("Note_Password").HeaderText = "كلمة سر الملاحظة"
                Available_MagNotes_DGV.Columns("Use_Main_Password").HeaderText = "إستخدم كلمة السر الرئيسية"
                Available_MagNotes_DGV.Columns("Note_Word_Wrap").HeaderText = "ألتفاف كلمات الملاحظة"
                Available_MagNotes_DGV.Columns("Note_Have_Reminder").HeaderText = "لها وقت تنبيه"
                Available_MagNotes_DGV.Columns("Next_Reminder_Time").HeaderText = "وقت التنبيه التالى"
                Available_MagNotes_DGV.Columns("Reminder_Every").HeaderText = "فترة تكرار التنبيه"
            ElseIf Language = "English" Then
                Available_MagNotes_DGV.Columns("MagNote_Name").HeaderText = "MagNote Name"
                Available_MagNotes_DGV.Columns("MagNote_Label").HeaderText = "MagNote Label"
                Available_MagNotes_DGV.Columns("MagNote_Category").HeaderText = "MagNote Category"
                Available_MagNotes_DGV.Columns("MagNote").HeaderText = "MagNote"
                Available_MagNotes_DGV.Columns("Blocked_Note").HeaderText = "Blocked Note"
                Available_MagNotes_DGV.Columns("Finished_Note").HeaderText = "Finished Note"
                Available_MagNotes_DGV.Columns("Note_Font").HeaderText = "Note Font"
                Available_MagNotes_DGV.Columns("Note_Font_Color").HeaderText = "Note Font Color"
                Available_MagNotes_DGV.Columns("Note_Back_Color").HeaderText = "Note Back Color"
                Available_MagNotes_DGV.Columns("Alternating_Row_Color").HeaderText = "Alternating Row Color"
                Available_MagNotes_DGV.Columns("Creation_Date").HeaderText = "Creation Date"
                Available_MagNotes_DGV.Columns("Secured_Note").HeaderText = "Note Secured By Password"
                Available_MagNotes_DGV.Columns("Note_Password").HeaderText = "Note Password"
                Available_MagNotes_DGV.Columns("Use_Main_Password").HeaderText = "Use Main Password"
                Available_MagNotes_DGV.Columns("MagNote_RTF").HeaderText = "MagNote RTF"
                Available_MagNotes_DGV.Columns("Note_Word_Wrap").HeaderText = "Note Word Wrap"
                Available_MagNotes_DGV.Columns("Note_Have_Reminder").HeaderText = "Note Have Reminder"
                Available_MagNotes_DGV.Columns("Next_Reminder_Time").HeaderText = "Next Reminder Time"
                Available_MagNotes_DGV.Columns("Reminder_Every").HeaderText = "Reminder Every"
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Function
    Private Sub Finished_MagNote_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Finished_Note_ChkBx.CheckedChanged

    End Sub

    Private Sub Finished_MagNote_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Finished_Note_ChkBx.CheckStateChanged
        Try
            Select Case Finished_Note_ChkBx.CheckState
                Case CheckState.Unchecked
                    If Language_Btn.Text = "ع" Then
                        Finished_Note_ChkBx.Text = "Active Note"
                    Else
                        Finished_Note_ChkBx.Text = "ملاحظة نشطة"
                    End If
                Case CheckState.Checked
                    If Language_Btn.Text = "ع" Then
                        Finished_Note_ChkBx.Text = "Fineshed Note"
                    Else
                        Finished_Note_ChkBx.Text = "ملاحظة منتهية"
                    End If
                Case CheckState.Indeterminate
                    If Language_Btn.Text = "ع" Then
                        Finished_Note_ChkBx.Text = "Pending Note"
                    Else
                        Finished_Note_ChkBx.Text = "ملاحظة معلقة"
                    End If
            End Select
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub MagNote_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Note_Form_Color_ClrCmbBx.SelectedIndexChanged

    End Sub

    Private Sub MagNote_Color_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Note_Form_Color_ClrCmbBx.SelectedValueChanged
        Try
            If ActiveControl.Name <> sender.name And
                Form_Color_Like_Note_ChkBx.CheckState = CheckState.Checked Then
                Exit Sub
            End If
            If Note_Form_Color_ClrCmbBx.SelectedIndex = -1 Or
                Form_Color_Like_Note_ChkBx.CheckState = CheckState.Checked Then
                Exit Sub
            End If
            ChangeFormControlsColors(Note_Form_Color_ClrCmbBx.Text, 0)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub ChangeFormControlsColors(ByVal ColorName As String, ByVal FrClr As Boolean)
        Try
            If Form_Color_Like_Note_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Sub
            End If
            If String.IsNullOrEmpty(ColorName) Then
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor
            If Not String.IsNullOrEmpty(ColorName) And Not FrClr Then
            Else
                If Note_Back_Color_ClrCmbBx.SelectedIndex <> -1 And Not FrClr Then
                    ColorName = BackColor.Name
                End If
            End If
            For Each Cntrl As Control In FindControlRecursive(New List(Of Control), Me, New List(Of Type))
                If Cntrl.Name = MagNote_PctrBx.Name Or
                    Cntrl.Name = Hide_Me_PctrBx.Name Then
                    Continue For
                End If
                If Cntrl.GetType = GetType(RichTextBox) Or
                   Cntrl.GetType = GetType(ReoGridControl) Then
                    If Cntrl.Name <> User_Escalation_RchTxtBx.Name Then
                        If Cntrl.Name <> MagNotes_Notes_TbCntrl.SelectedTab.Name & "RchTxtBx" And
                            Cntrl.Name <> MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid" Then
                            Continue For
                        End If
                    End If
                End If
                If Cntrl.Name.Contains("Spliter") Or
                    Cntrl.Name = Form_Transparency_TrkBr.Name Or
                    Cntrl.Parent.Name = "Read_Me_Pnl" Or
                    Cntrl.Name = "Read_Me_Pnl" Or
                    Cntrl.Name = "Insert_Table_Btn" Then
                    Continue For
                End If
                If Cntrl.Name = MsgBox_SttsStrp.Name Then
                    If Not IsNothing(CType(Cntrl, StatusStrip).Items("MsgBox_TlStrpSttsLbl")) Then
                        If FrClr Then
                            MsgBox_SttsStrp.Items(0).ForeColor = Color.FromName(Note_Font_Color_ClrCmbBx.Text)
                        End If
                    End If
                End If
                If FrClr Then
                    Cntrl.ForeColor = Color.FromName(Note_Font_Color_ClrCmbBx.Text)
                Else
                    Cntrl.BackColor = Color.FromName(ColorName)
                End If
                If Cntrl.GetType = GetType(DataGridView) And Not FrClr Then
                    CType(Cntrl, DataGridView).BackgroundColor = Color.FromName(ColorName)
                    CType(Cntrl, DataGridView).ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(ColorName)
                    CType(Cntrl, DataGridView).RowHeadersDefaultCellStyle.BackColor = Color.FromName(ColorName)
                    CType(Cntrl, DataGridView).RowsDefaultCellStyle.BackColor = Color.FromName(ColorName)
                End If
            Next
            Application.DoEvents()
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Run_Me_At_Windows_Startup_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Run_Me_At_Windows_Startup_ChkBx.CheckedChanged

    End Sub

    Private Sub Run_Me_At_Windows_Startup_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Run_Me_At_Windows_Startup_ChkBx.CheckStateChanged
        Try
            Cursor = Cursors.WaitCursor
            Dim ProductName = Application.ProductName
            Dim Reg As Microsoft.Win32.RegistryKey
            Dim GetReg As String = Nothing
            If Run_Me_At_Windows_Startup_ChkBx.CheckState = CheckState.Checked Then
                Reg = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
                GetReg = Reg.GetValue(ProductName)
                If Len(GetReg) > 0 Then
                    Reg.DeleteValue(ProductName.ToString, True)
                End If
                Reg.SetValue(ProductName, Application.StartupPath & "\" & ProductName)
            ElseIf Run_Me_At_Windows_Startup_ChkBx.CheckState = CheckState.Unchecked Then
                Reg = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
                GetReg = Reg.GetValue(ProductName)
                If Len(GetReg) > 0 Then Reg.DeleteValue(ProductName, True)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function SaveOpenedTabPages() As Boolean
        Dim WordToSave As String
        If MagNotes_Notes_TbCntrl.TabPages.Count = 0 Then Exit Function
        If Load_MagNote_At_Startup_ChkBx.CheckState = CheckState.Indeterminate Then
            For Each Note In Available_MagNotes_DGV.Rows
                WordToSave &= Note.cells("MagNote_Name").value & vbNewLine
            Next
        Else
            For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                WordToSave &= TbPg.tag & vbNewLine
            Next
        End If
        If MagNotes_Folder_Path_TxtBx.TextLength > 0 Then
            My.Computer.FileSystem.WriteAllText(MagNotes_Folder_Path_TxtBx.Text & "\OpenedTabPages.txt", Microsoft.VisualBasic.Left(WordToSave, WordToSave.Length - 1), 0, System.Text.Encoding.UTF8)
        ElseIf MagNoteFolderPath.Length > 0 Then
            My.Computer.FileSystem.WriteAllText(MagNoteFolderPath & "\OpenedTabPages.txt", Microsoft.VisualBasic.Left(WordToSave, WordToSave.Length - 1), 0, System.Text.Encoding.UTF8)
        End If
    End Function
    Private Sub MagNote_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If systemShutdown Then
                e.Cancel = True
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor
            If RunAsExternal() Then
                If NoteAmendmented() = DialogResult.Cancel Then
                    GoTo CanceExitExternalFile
                End If
                Exit Sub
            End If
            If Not AlreadyAsked And
                Minimize_After_Running_My_Shortcut_ChkBx.CheckState <> CheckState.Indeterminate And
                Not ApplicationRestart Then
                AlreadyAsked = True
                If Language_Btn.Text = "ع" Then
                    Msg = "MagNote Will Be Always In Windows System Tray And Ready To Call By Clicking (Alt+s) Until You Exit The Note From The Windows System Tray!!!"
                Else
                    Msg = "تذكر دائما أن برنامج الملاحظات الملاحظة سيكون مخفيا فى علبة الايقونات المخفية للنوافذ ويمكن استدعائة بالنقر على" & vbNewLine & " ـ(Alt+s)ـ " & vbNewLine & "... حتى يتم الخروج من البرنامج"
                End If
                ShowWindowsNotification(Msg)
            End If
            If Secured_Note_ChkBx.CheckState = CheckState.Checked Then
                MagNote_No_CmbBx.SelectedIndex = -1
                RCSN.Text = String.Empty
            End If
            If ApplicationRestart Then
                If Not Tray Is Nothing Then
                    Tray.Visible = False
                    Tray.Dispose()
                    Tray = Nothing
                End If
                Exit Sub
            ElseIf Not RunAsExternal() Then
                If Path.GetDirectoryName(RCSN.Name) <> MagNoteFolderPath Then
                    Close_Current_TabPage_Click(Me, Nothing)
                End If
                Me.WindowState = FormWindowState.Minimized
CanceExitExternalFile:
                e.Cancel = True
                Tray.Visible = True
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Form_Transparency_TrkBr_Scroll(sender As Object, e As EventArgs) Handles Form_Transparency_TrkBr.Scroll
    End Sub

    Private Sub Previous_Btn_Click(sender As Object, e As EventArgs) Handles Previous_Btn.Click
        Try
            NextNote.Maximum = MagNote_No_CmbBx.Items.Count - 1
            If NextNote.Value = 0 Then
                NextNote.Value = NextNote.Maximum
            Else
                NextNote.Value -= 1
            End If
            MagNote_No_CmbBx.SelectedIndex = NextNote.Value
        Catch ex As Exception
            If Available_MagNotes_DGV.Rows.Count > 0 Then
                Available_MagNotes_DGV.Rows(Available_MagNotes_DGV.Rows.Count - 1).Selected = True
                Available_MagNotes_DGV.CurrentCell = Available_MagNotes_DGV.Rows(Available_MagNotes_DGV.Rows.Count - 1).Cells(0)
            End If
        Finally
            Cursor = Cursors.Default
            IgnoreNoteAmendmented = False
        End Try
    End Sub
    Dim NextNote As New VScrollBar
    Private Sub Next_Btn_Click(sender As Object, e As EventArgs) Handles Next_Btn.Click
        Try
            NextNote.Maximum = MagNote_No_CmbBx.Items.Count - 1
            If NextNote.Value = NextNote.Maximum Then
                NextNote.Value = 0
            Else
                NextNote.Value += 1
            End If
            MagNote_No_CmbBx.SelectedIndex = NextNote.Value
        Catch ex As Exception
            If Available_MagNotes_DGV.Rows.Count > 0 Then
                Available_MagNotes_DGV.Rows(0).Selected = True
                Available_MagNotes_DGV.CurrentCell = Available_MagNotes_DGV.Rows(0).Cells(0)
            End If
        Finally
            Cursor = Cursors.Default
            IgnoreNoteAmendmented = False
        End Try
    End Sub
    Private Sub View_Text_Font_Properties_Btn_Click_1(sender As Object, e As EventArgs) Handles View_Text_Font_Properties_Btn.Click
        Try
            Dim FontDialog As New FontDialog
            FontDialog.Font = Note_Font_TxtBx.Font
            If FontDialog.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Note_Font_TxtBx.Text = ReturnFontString(FontDialog.Font)
            Else
                Note_Font_TxtBx.Text = Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Function ReturnFontString(Fnt As Font) As String
        Try
            Dim NoteFontTxtBx = Fnt.Name & " - " & Fnt.Size & " - "
            Select Case Fnt.Style
                Case 0
                    NoteFontTxtBx &= "Regular"
                Case 1
                    NoteFontTxtBx &= "Bold"
                Case 2
                    NoteFontTxtBx &= "Italic"
                Case 8
                    NoteFontTxtBx &= "Strikeout"
                Case 4
                    NoteFontTxtBx &= "Underline"
            End Select
            Return NoteFontTxtBx
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Public Sub Save_Note_Form_Parameter_Setting_Btn_Click(sender As Object, e As EventArgs) Handles Save_Note_Form_Parameter_Setting_Btn.Click
        If Not Save_Note_Form_Parameter_Setting_Btn.Enabled Then Exit Sub
        If MagNoteFolderPath <> MagNotes_Folder_Path_TxtBx.Text And MagNotes_Folder_Path_TxtBx.TextLength > 0 Then
            MagNoteFolderPath = MagNotes_Folder_Path_TxtBx.Text
        End If
        Dim FileName = Application.StartupPath & "\MagNote_Setting.txt"
        Try
            Cursor = Cursors.WaitCursor
            Dim Show_ShowMsg As Boolean = True
            If ActiveControl.Name <> sender.Name Then
                If Not IsNothing(Shortcuts_LstVw) Then
                    If ActiveControl.Name <> Shortcuts_LstVw.Name Then
                        Show_ShowMsg = False
                    End If
                End If
            End If
            If File.Exists(FileName) And ActiveControl.Name = sender.name Then
                If Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "MagNote Form Setting File Will Be Saved... Are You Sure?"
                    Else
                        Msg = "سيتم حفظ معلمات ضبط البرنامج... هل انت ماأكد"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & vbNewLine, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False, Show_ShowMsg)
                    If MyDialogResult = DialogResult.No Then Exit Sub
                End If
            End If
            If Periodically_Backup_MagNotes_ChkBx.CheckState = CheckState.Checked And (Days_NmrcUpDn.Value = 0 And Hours_NmrcUpDn.Value = 0 And Minutes_NmrcUpDn.Value = 0) Then
                If Language_Btn.Text = "ع" Then
                    Msg = "You Set Backup Periodically But No Period Seted... Period Will Be One Day.. Are You Agree?"
                Else
                    Msg = "لقد فعلت النسخ الاحتياطى ولم تحدد الفترة... سيتم تحديد الفترة يوم واحد... هل انت موافق؟"
                End If
                Dim MyDialogResult = ShowMsg(Msg & vbNewLine, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                If MyDialogResult = DialogResult.No Then Exit Sub
                Days_NmrcUpDn.Value = 1
            End If
            If Not SaveFormParameters(FileName) Then Exit Sub
            If Language_Btn.Text = "ع" Then
                Msg = "File Updated Successfully"
            Else
                Msg = "تم حفظ الملف بنجاخ"
            End If
            SaveOpenedTabPages()
            SaveList(, 0)
            If ActiveControl.Name = sender.name Then
                ShowMsg(Msg & vbNewLine & FileName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False, Show_ShowMsg)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function SaveFormParameters(ByVal FileName As String) As Boolean
        Try
            If IsNothing(FileName) Then
                FileName = Application.StartupPath & "\MagNote_Setting.txt"
            End If
            If Debugger.IsAttached Then
                FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\" & "\MagNote_Setting.txt"
            End If
            Dim TextToWrite As String
            TextToWrite = ":Current_Language -(" & Language_Btn.Text & ")-"
            If Not IsNothing(MagNotes_Notes_TbCntrl.SelectedTab) Then
                Dim FN = Replace(MagNotes_Notes_TbCntrl.SelectedTab.Name, ":", "(Instead Of Colon)")
                Dim FL = Path.GetFileName(MagNotes_Notes_TbCntrl.SelectedTab.Name)
                TextToWrite &= ":Current_MagNote_Name -(" & FL & "," & FN & "," & MagNote_Category_CmbBx.Text & ")-"
            End If
            TextToWrite &= ":Run_Me_At_Windows_Startup -(" & Run_Me_At_Windows_Startup_ChkBx.CheckState & ")-"
            TextToWrite &= ":Me_Always_On_Top -(" & Me_Always_On_Top_ChkBx.CheckState & ")-"
            TextToWrite &= ":Hide_Finished_MagNote -(" & Hide_Finished_MagNote_ChkBx.CheckState & ")-"
            TextToWrite &= ":Save_Setting_When_Exit -(" & Save_Setting_When_Exit_ChkBx.CheckState & ")-"
            TextToWrite &= ":Note_Form_Opacity -(" & Opacity.ToString & ")-"
            TextToWrite &= ":Periodically_Backup_MagNotes -(" & Periodically_Backup_MagNotes_ChkBx.CheckState & ")-"
            TextToWrite &= ":Backup_Time -(" & Days_NmrcUpDn.Value & "," & Hours_NmrcUpDn.Value & "," & Minutes_NmrcUpDn.Value & ")-"
            TextToWrite &= ":Next_Backup_Time -(" & Replace(Next_Backup_Time_TxtBx.Text, ":", ",") & ")-"
            TextToWrite &= ":Reload_MagNotes_After_Amendments -(" & Reload_MagNotes_After_Amendments_ChkBx.CheckState & ")-"
            TextToWrite &= ":Enter_Password_To_Pass -(" & Enter_Password_To_Pass_ChkBx.CheckState & ")-"
            TextToWrite &= ":Complex_Password -(" & Complex_Password_ChkBx.CheckState & ")-"
            TextToWrite &= ":Main_Password -(" & Encrypt_Function(Main_Password_TxtBx.Text) & ")-"
            TextToWrite &= ":Set_Control_To_Fill -(" & Set_Control_To_Fill_ChkBx.CheckState & ")-"
            TextToWrite &= ":Warning_Before_Save -(" & Warning_Before_Save_ChkBx.CheckState & ")-"
            TextToWrite &= ":Warning_Before_Delete -(" & Warning_Before_Delete_ChkBx.CheckState & ")-"
            TextToWrite &= ":Double_Click_To_Run_Shortcut -(" & Double_Click_To_Run_Shortcut_ChkBx.CheckState & ")-"
            TextToWrite &= ":Keep_Note_Opened_After_Delete -(" & Keep_Note_Opened_After_Delete_ChkBx.CheckState & ")-"
            TextToWrite &= ":Hide_Windows_Desktop_Icons -(" & Hide_Windows_Desktop_Icons_ChkBx.CheckState & ")-"
            TextToWrite &= ":Form_Is_Restricted_By_Screen_Bounds -(" & Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckState & ")-"
            TextToWrite &= ":Ask_If_Form_Is_Outside_Screen_Bounds -(" & Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.CheckState & ")-"
            TextToWrite &= ":Enable_Maximize_Box -(" & Enable_Maximize_Box_ChkBx.CheckState & ")-"
            TextToWrite &= ":Remember_Opened_External_Files -(" & Remember_Opened_External_Files_ChkBx.CheckState & ")-"
            TextToWrite &= ":Show_Note_Tab_Control -(" & Show_Note_Tab_Control_ChkBx.CheckState & ")-"
            TextToWrite &= ":Note_Form_Size -(" & Note_Form_Size_TxtBx.Text & ")-"
            TextToWrite &= ":Me_Is_Compressed -(" & Me_Is_Compressed_ChkBx.CheckState & ")-"
            TextToWrite &= ":Minimize_After_Running_My_Shortcut -(" & Minimize_After_Running_My_Shortcut_ChkBx.CheckState & ")-"
            TextToWrite &= ":Me_As_Default_Text_File_Editor -(" & Me_As_Default_Text_File_Editor_ChkBx.CheckState & ")-"
            TextToWrite &= ":Run_Me_As_Administrator -(" & Run_Me_As_Administrator_ChkBx.CheckState & ")-"
            TextToWrite &= ":Application_Starts_Minimized -(" & Application_Starts_Minimized_ChkBx.CheckState & ")-"
            TextToWrite &= ":Save_Day_Light -(" & Save_Day_Light_ChkBx.CheckState & ")-"
            TextToWrite &= ":Country -(" & Country_CmbBx.Text & ")-"
            TextToWrite &= ":City -(" & City_CmbBx.Text & ")-"
            TextToWrite &= ":Calculation_Methods -(" & Calculation_Methods_CmbBx.Text & ")-"
            TextToWrite &= ":Fagr_Voice_Files -(" & Fagr_Voice_Files_CmbBx.Text & ")-"
            TextToWrite &= ":Voice_Azan_Files -(" & Voice_Azan_Files_CmbBx.Text & ")-"
            TextToWrite &= ":Alert_File_Path -(" & Replace(Alert_File_Path_TxtBx.Text, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Stop_Displaying_Controls_ToolTip -(" & Stop_Displaying_Controls_ToolTip_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_Windows_Notification_Tray -(" & Activate_Windows_Notification_Tray_ChkBx.CheckState & ")-"
            TextToWrite &= ":MagNotes_Folder_Path -(" & Replace(MagNotes_Folder_Path_TxtBx.Text, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Backup_Folder_Path -(" & Replace(Backup_Folder_Path_TxtBx.Text, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Open_Note_In_New_Tab -(" & Open_Note_In_New_Tab_ChkBx.CheckState & ")-"
            TextToWrite &= ":Show_Control_Tab_Pages_In_Multi_Line -(" & Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckState & ")-"
            TextToWrite &= ":Load_MagNote_At_Startup -(" & Load_MagNote_At_Startup_ChkBx.CheckState & ")-"
            TextToWrite &= ":Azan_Spoke_Method -(" & Azan_Spoke_Method_ChkBx.CheckState & ")-"
            TextToWrite &= ":Azan_Activation -(" & Azan_Activation_ChkBx.CheckState & ")-"
            TextToWrite &= ":Alert_Before_Azan -(" & Alert_Before_Azan_ChkBx.CheckState & ")-"
            TextToWrite &= ":Time_To_Alert_Before_Azan -(" & Time_To_Alert_Before_Azan_NmrcUpDn.Value & ")-"
            TextToWrite &= ":Azan_Takbeer_Only -(" & Azan_Takbeer_Only_ChkBx.CheckState & ")-"
            TextToWrite &= ":Reload_MagNote_After_Change_Category -(" & Reload_MagNote_After_Change_Category_ChkBx.CheckState & ")-"
            TextToWrite &= ":Show_Form_Border_Style -(" & Show_Form_Border_Style_ChkBx.CheckState & ")-"
            TextToWrite &= ":Form_Color_Like_Note -(" & Form_Color_Like_Note_ChkBx.CheckState & ")-"
            TextToWrite &= ":Note_Form_Color -(" & Note_Form_Color_ClrCmbBx.Text & ")-"
            TextToWrite &= ":Setting_TabContrl_Size -(" & Setting_Tab_Control_Size_TxtBx.Text & ")-"
            Dim MyLocation As String = Replace(Replace(Replace(Replace(Note_Form_Location_TxtBx.Text, "X=", ""), "Y=", ""), "{", ""), "}", "")
            If Not CheckFormLocation(MyLocation) Then
                TextToWrite &= ":Note_Form_Location -(" & Note_Form_Location_TxtBx.Text & ")-"
            Else
                TextToWrite &= ":Note_Form_Location -({X=407,Y=0})-"
            End If
            TextToWrite &= ":Check_For_New_Version -(" & Check_For_New_Version_ChkBx.CheckState & ")-"
            TextToWrite &= ":Remember_Opened_Notes_When_Close -(" & Remember_Opened_Notes_When_Close_ChkBx.CheckState & ")-"
            TextToWrite &= ":Apply_Multiple_New_Files -(" & Apply_Multiple_New_Files_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_Projects_TabPage -(" & Activate_Projects_TabPage_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_MagNotes_TabPage -(" & Activate_MagNotes_TabPage_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_Note_Parameters_TabPage -(" & Activate_Note_Parameters_TabPage_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_ShortCut_TabPage -(" & Activate_ShortCuts_TabPage_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_Prayer_Time_TabPage -(" & Activate_Prayer_Time_TabPage_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_Alert_Time_TabPage -(" & Activate_Alert_Time_TabPage_ChkBx.CheckState & ")-"
            TextToWrite &= ":Ignore_Error_Message_For_Connection -(" & Ignore_Error_Message_For_Connection_ChkBx.CheckState & ")-"
            TextToWrite &= ":Ignore_Error_Message_For_Connection -(" & Ignore_Error_Message_For_Connection_ChkBx.CheckState & ")-"
            TextToWrite &= ":Force_Stop_Playing_Current_Sound_File -(" & Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState & ")-"
            TextToWrite &= ":Force_Activate_TabPage_When_Alert_Is_Active -(" & Force_Activate_TabPage_When_Alert_Is_Active_ChkBx.CheckState & ")-"
            TextToWrite &= ":Activate_Alert_Function -(" & Activate_Alert_Function_ChkBx.CheckState & ")-"
            TextToWrite &= ":Immediately_Update_Form_Parameters -(" & Immediately_Update_Form_Parameters_ChkBx.CheckState & ")-"
            My.Computer.FileSystem.WriteAllText(FileName, TextToWrite, 0, System.Text.Encoding.UTF8)
            If MagNotes_Folder_Path_TxtBx.TextLength > 0 Then
                File.Copy(FileName, MagNotes_Folder_Path_TxtBx.Text & "\MagNote_Setting_Copy.txt", 1)
            End If
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try

    End Function
    Private Sub Font_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Note_Font_Color_ClrCmbBx.SelectedIndexChanged

    End Sub

    Private Sub ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Note_Font_Color_ClrCmbBx.SelectedValueChanged, Note_Back_Color_ClrCmbBx.SelectedValueChanged,
        Note_Back_Color_ClrCmbBx.SelectedValueChanged,
            Note_Form_Color_ClrCmbBx.SelectedValueChanged
    End Sub

    Private Sub Font_Color_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Note_Font_Color_ClrCmbBx.SelectedValueChanged
        Try
            If sender.SelectedIndex = -1 Then Exit Sub
            ChangeFormControlsColors(Note_Font_Color_ClrCmbBx.Text, 1)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Note_Font_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Note_Font_TxtBx.TextChanged
        Try
            If Note_Font_TxtBx.TextLength = 0 Then Exit Sub
            If Not IsNothing(RCSN(0)) Then
                RCSN(0).Font = GetFontByString(Note_Font_TxtBx.Text)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Function GetFontByString(ByVal sFont As String) As Font
        Dim sElement() As String = Split(sFont, " - ")
        Dim sSingle() As String
        Dim sValue As String
        Dim FontName As String
        Dim FontSize As Single
        Dim FontStyle As FontStyle = System.Drawing.FontStyle.Regular
        Dim FontUnit As GraphicsUnit = GraphicsUnit.Point
        Dim gdiCharSet As Byte
        Dim gdiVerticalFont As Boolean
        sValue = Trim(sElement(0))
        sSingle = Split(sValue, "=")
        FontName = sSingle(0)
        sValue = Trim(sElement(1))
        sSingle = Split(sValue, "=")
        FontSize = sSingle(0)
        Note_Font_Name_CmbBx.Text = FontName
        Note_Font_Size_CmbBx.Text = FontSize
        Note_Font_Style_CmbBx.Text = sElement(2)
        If Not IsNothing(Font_TlStrpCmbBx(0)) Then
            Font_TlStrpCmbBx.Text = FontName
            Font_Size_TlStrpCmbBx.Text = FontSize
        End If
        Select Case sElement(2)
            Case "Regular"
                Return New Font(FontName, FontSize, System.Drawing.FontStyle.Regular, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Bold"
                If Not IsNothing(Bold_TlStrpBtn(0)) Then
                    Bold_TlStrpBtn_Click(Bold_TlStrpBtn, EventArgs.Empty)
                End If
                Return New Font(FontName, FontSize, System.Drawing.FontStyle.Bold, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Italic"
                If Not IsNothing(Italic_TlStrpBtn(0)) Then
                    Italic_TlStrpBtn_Click(Italic_TlStrpBtn, EventArgs.Empty)
                End If
                Return New Font(FontName, FontSize, System.Drawing.FontStyle.Italic, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Strikeout"
                If Not IsNothing(Strike_Through_TlStrpBtn(0)) Then
                    Strike_Through_TlStrpBtn_Click(Strike_Through_TlStrpBtn, EventArgs.Empty)
                End If
                Return New Font(FontName, FontSize, System.Drawing.FontStyle.Strikeout, FontUnit, gdiCharSet, gdiVerticalFont)
            Case "Underline"
                If Not IsNothing(Under_Line_TlStrpBtn(0)) Then
                    Under_Line_TlStrpBtn_Click(Under_Line_TlStrpBtn, EventArgs.Empty)
                End If
                Return New Font(FontName, FontSize, System.Drawing.FontStyle.Underline, FontUnit, gdiCharSet, gdiVerticalFont)
        End Select
    End Function
    Private Sub ShowHide_RchTxtBx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If RCSN.Visible = False Then
                RCSN.Visible = True
            Else
                RCSN.Visible = False
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Show_Labeling_And_Tooltip_Form_Click(sender As Object, e As EventArgs)
        If Labeling_And_Tooltip_Form.Visible Then
            Labeling_And_Tooltip_Form.Close()
        End If
        Labeling_And_Tooltip_Form.Show(Me)
        Application.DoEvents()
        Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
        If Objct.GetType = GetType(ListView) Then
            Labeling_And_Tooltip_Form.Available_Forms_CmbBx.Text = Me.Name
            Application.DoEvents()
            Labeling_And_Tooltip_Form.Shortcuts_CmbBx.Text = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).FocusedItem.Text
        End If
    End Sub
    Private Sub Find_Click(sender As Object, e As EventArgs)
        Find_TlStrpBtn_Click(Find_TlStrpBtn, EventArgs.Empty)
    End Sub

    Private Sub New_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles New_Note_TlStrpBtn.Click
        Add_New_MagNote_Btn.PerformClick()
    End Sub
    Private Function TheNoteIsAplicableToSave()
        Try
            Dim FileExist, UseMainPassword As Boolean
            If RCSN.TextLength = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Type Something To Save!!!"
                Else
                    Msg = "من فضلك اكتب شئ ليتم حفظة!!!"
                End If
                ShowMsg(Msg & vbNewLine & RCSN.Text, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MBOs, False)
                Return False
            ElseIf MagNote_No_CmbBx.Text.Length = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Generate New Note Not First!!!"
                Else
                    Msg = "من فضلك استحدث تسلسل جديد أولا!!!"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MBOs, False)
                Return False
            ElseIf MagNote_No_CmbBx.SelectedIndex <> -1 Then
                If File.Exists(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) Then
                    FileExist = True
                ElseIf Not File.Exists(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) Then
                    Return True
                ElseIf Not MagNoteIsOpenedExternal() Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Not Available To Save The File By This Situation!!!"
                    Else
                        Msg = "غير متاح حفظ الملف بهذا الوضع !!!"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MBOs, False)
                    Return False
                End If
            ElseIf MagNote_No_CmbBx.Text.Length > 0 Then
                If File.Exists(MagNoteFolderPath & "\" & MagNote_No_CmbBx.Text) Then
                    FileExist = True
                End If
            End If
            If MagNoteIsOpenedExternal() Then Return True
            If MagNote_Category_CmbBx.SelectedIndex = -1 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Select MagNote Category To Save!!!"
                Else
                    Msg = "إختار فئة ماجنوت للحفظ !!!"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MBOs, False)
                Return False
            End If

            Dim NoteRow = isInDataGridView(MagNotes_Notes_TbCntrl.SelectedTab.Name, "MagNote_Name", Available_MagNotes_DGV, 0, 1)
            If FileExist And
                Not IsNothing(NoteRow) Then
                If String.IsNullOrEmpty(NoteRow.Cells("Use_Main_Password").Value) Then
                    UseMainPassword = False
                Else
                    UseMainPassword = NoteRow.Cells("Use_Main_Password").Value
                End If
            End If
            If Not IsNothing(NoteRow) Then
                If Secured_Note_ChkBx.CheckState = CheckState.Unchecked And
                    NoteRow.Cells("Secured_Note").Value And
                    Note_Password_TxtBx.TextLength = 0 Then
                    GoTo EnterPassWord
                End If
            End If
            If (Secured_Note_ChkBx.CheckState = CheckState.Checked And
                    Note_Password_TxtBx.TextLength = 0) And Not PassedMainPasswordToPass Then
EnterPassWord:
                RestoreCheckBoxesValue()
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Enter Note Password"
                Else
                    Msg = "من فضلك أدخل كلمة سر الملاحظة"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, False)
                ActiveControl = Note_Password_TxtBx
                Note_Password_TxtBx.Focus()
                Note_Password_TxtBx.ReadOnly = False
                Return False
MagNoteInsilization:
            ElseIf Note_Password_TxtBx.TextLength = 0 And
                Secured_Note_ChkBx.CheckState = CheckState.Checked And
                   Not UseMainPassword Then
                GoTo EnterPassWord
            ElseIf RCSN.TextLength = 0 Then
                If Not File.Exists(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) Then
                    RestoreCheckBoxesValue()
                    If Language_Btn.Text = "ع" Then
                        Msg = "Kindly Type Something To Save!!!"
                    Else
                        Msg = "من فضلك اكتب شئ ليتم حفظة!!!"
                    End If
                    ShowMsg(Msg & vbNewLine & RCSN.Text, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MBOs, False)
                    Return False
                ElseIf FileExist And
                    PassedMainPasswordToPass And
                    Not UseMainPassword Then
                    GoTo EnterPassWord
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub RestoreCheckBoxesValue()
        ActiveControl = RCSN()
        Dim FileName = MagNotes_Notes_TbCntrl.SelectedTab.Tag
        Dim array() As String = ReadFile(FileName, 1)
        Blocked_Note_ChkBx.CheckState = CType(Val(array(4).ToString), CheckState)
        Finished_Note_ChkBx.CheckState = CType(Val(array(5).ToString), CheckState)
        Secured_Note_ChkBx.CheckState = CType(Val(array(10).ToString), CheckState)
        If IsNothing(array(12)) Then
            Use_Main_Password_ChkBx.CheckState = CheckState.Unchecked
        Else
            Use_Main_Password_ChkBx.CheckState = CType(Val(array(12).ToString), CheckState)
        End If

    End Sub
    Private Function SaveNoteCategory(ByVal NoteName)
        Try
            Dim CategoryiesFileName = MagNotes_Folder_Path_TxtBx.Text & "\Categories.xml"
            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(CategoryiesFileName)
            For Each chld In xmlDoc
                For Each cat In chld
                    For Each ct In cat
                        Dim Nm = ct.Attributes("Name").Value
                        Dim Nm1 = Path.GetFileName(NoteName)
                        If Nm = Nm1 Then
                            CType(cat, XmlNode).RemoveChild(ct)
                            xmlDoc.Save(CategoryiesFileName)
                            SaveNoteCategory(NoteName)
                            Exit Function
                        End If
                    Next
                Next
            Next
ExitFore:
            Dim myNode As System.Xml.XmlNode
            Dim NewRoot As XmlNode
            Dim CategoryExist As Boolean
            For Each chld In xmlDoc
                For Each cat In chld
                    If cat.Name = Replace(MagNote_Category_CmbBx.Text, Space(1), "_") Then
                        If Not IsNothing(NewRoot) Then
                            myNode = NewRoot.Clone()
                            myNode.Attributes("Name").Value = Path.GetFileName(NoteName)
                            myNode.Attributes("Path").Value = NoteName
                            myNode.Attributes("Creation_Date").Value = Now
                            cat.AppendChild(myNode)
                            CategoryExist = True
                            GoTo ExitFore1
                        End If
                    Else
                        If Not IsNothing(cat.FirstChild) And
                            IsNothing(NewRoot) Then
                            If Not IsNothing(cat.attributes) Then
                                NewRoot = cat.FirstChild
                            End If
                        End If
                    End If
                Next
            Next
ExitFore1:
            If Not CategoryExist Then 'caegory not found
                Dim root As XmlNode = xmlDoc.DocumentElement
                Dim elem As XmlElement = xmlDoc.CreateElement(Replace(MagNote_Category_CmbBx.Text, Space(1), "_"))
                elem.InnerText = ""
                root.AppendChild(elem)
                xmlDoc.Save(CategoryiesFileName)
                CategoryExist = True
                GoTo ExitFore
            End If
            xmlDoc.Save(CategoryiesFileName)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Dim SaveDialogResultAnseredIsNo As Boolean = False
    Private Sub Save_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Save_Note_TlStrpBtn.Click
        Dim FileNameToSave
        Try
            If MagNote_No_CmbBx.Text = "Project Event" Then
                Update_Btn_Click(Update_Btn, EventArgs.Empty)
                Exit Sub
            End If
            If File_Format_CmbBx.SelectedIndex = -1 Then
                If Language_Btn.Text = "E" Then
                    Msg = "من فضلك أختار نوع الملف الذى تريد حفظة أولا"
                Else
                    Msg = "Kindly Select The File Kind You Want To Save First"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If

            If Secured_Note_ChkBx.CheckState = CheckState.Checked And
                Note_Password_TxtBx.TextLength = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "من فضلك ادخل أولا كلمة السر الخاصة بالتعامل مع هذا الملف"
                Else
                    Msg = "Kindly Enter The Password To Deal With This File"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            Dim FileExtension = DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
            Cursor = Cursors.WaitCursor
            Dim DisplayMsg As Boolean = True
            MyFormWindowState(0, 1, FormWindowState.Minimized)
            If MagNote_Category_CmbBx.SelectedIndex = -1 Then
                CheckPublicCategory()
            End If
            If New StackFrame(1).GetMethod().Name.Contains("Backup_Timer_Tick") Then
                DisplayMsg = False
            End If
            If MagNotes_Notes_TbCntrl.SelectedIndex <> -1 And
                (FileExtension = "MagNote (txt)" Or FileExtension = "ملاحظة (txt)") Then
                FileNameToSave = MagNotes_Notes_TbCntrl.SelectedTab.Tag
            ElseIf MagNotes_Notes_TbCntrl.SelectedIndex <> -1 And
                (FileExtension <> "MagNote (txt)" And FileExtension <> "ملاحظة (txt)") Then
                Dim OpenFileDialog As New SaveFileDialog
                OpenFileDialog.Title = "Saving Files"
                If MagNote_No_CmbBx.Text.Length > 0 Then
                    OpenFileDialog.Filter = "Current File|" & MagNote_No_CmbBx.Text & "|RTF Files|*.rtf|Text Files|*.txt|All files|*.*"
                Else
                    OpenFileDialog.Filter = "Text Files|*.txt|RTF Files|*.rtf|All files|*.*"
                End If
                If String.IsNullOrEmpty(Path.GetExtension(MagNote_No_CmbBx.Text)) Then
                    OpenFileDialog.FileName = MagNote_No_CmbBx.Text & "." & DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                Else
                    OpenFileDialog.FileName = MagNote_No_CmbBx.Text
                End If
                If MagNote_No_CmbBx.SelectedIndex <> -1 Then
                    OpenFileDialog.InitialDirectory = Path.GetDirectoryName(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key)
                End If
                If OpenFileDialog.ShowDialog = DialogResult.Cancel Then
                    Exit Sub
                Else
                    FileNameToSave = OpenFileDialog.FileName
                End If
            Else
                If Language_Btn.Text = "E" Then
                    Msg = "توجد مشكلة فى مسار حفظ الملف"
                Else
                    Msg = "There Is A Problem With The File Path To Save"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If String.IsNullOrEmpty(FileNameToSave) Then
                Exit Sub
            End If
            If ActiveControl.Name = Note_TlStrp.Name And File.Exists(FileNameToSave) Then
                If Not TheNoteIsAplicableToSave() Then Exit Sub
            End If
            If FileExtension <> "MagNote (txt)" And FileExtension <> "ملاحظة (txt)" Then
                CreateOutsideMagNote()
SaveAsRTF:
                If File.Exists(FileNameToSave) Then
                    If Not TheNoteIsAplicableToSave() Then Exit Sub
                End If
                If FileExtension = "Rich Text Box (rtf)" Or FileExtension = "مربع نص منسق (rtf)" Then
                    RCSN.SaveFile(FileNameToSave, RichTextBoxStreamType.RichText)
                Else
                    My.Computer.FileSystem.WriteAllText(FileNameToSave, RCSN.Text, 0, System.Text.Encoding.UTF8)
                End If
                If Language_Btn.Text = "ع" Then
                    Msg = "File Updated Successfully"
                Else
                    Msg = "تم حفظ الملف بنجاح"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MBOs, False)
                Exit Sub
            Else
                Dim FileExist As Boolean
                Dim TextToWrite As String = Nothing
                Dim SNN, SNL, FileNameInTheFile
                Dim OldCatgory As String = String.Empty
                If File.Exists(FileNameToSave) Then
                    SNN = Path.GetFileNameWithoutExtension(FileNameToSave)
                    Dim array() As String = ReadFile(FileNameToSave, 1) 'just return the row
                    If Not IsNothing(array) Then
                        OldCatgory = array(2)
                        If IsNothing(array(0)) Then
                            If Language_Btn.Text = "E" Then
                                Msg = "هذا الملف ليس ملاحظة... هل تريد حفظه على هيئة ملاحظة؟"
                            Else
                                Msg = "This File Is Not A MagNote Do You Want To Save It As SticyNote Type?"
                            End If
                            Dim MyDialogResult = ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                            If MyDialogResult = DialogResult.No Then
                                GoTo SaveAsRTF
                                Exit Sub
                            Else
                                FileNameToSave = Replace(FileNameToSave, Path.GetFileName(FileNameToSave), "MagNote -(" & Generate_New_MagNote_Name(1) & ")-.txt")
                                FileNameToSave = Replace(FileNameToSave, Path.GetDirectoryName(FileNameToSave), MagNoteFolderPath)
                                GoTo SaveNotNoteNotAsMagNote
                            End If
                        Else
                            FileNameInTheFile = Path.GetFileNameWithoutExtension(array(0).ToString)
                        End If
                    End If
                    FileExist = True
                    If Available_MagNotes_DGV.CurrentRow.Cells("Blocked_Note").Value = 1 And
                    Blocked_Note_ChkBx.CheckState = CheckState.Unchecked Then
                        If Language_Btn.Text = "E" Then
                            Msg = "هذا الملف محظور التعامل سيتم فك الحظر... هل انت متأكد؟"
                        Else
                            Msg = "This File Is Blocked File Will Be Unblocked... Are You Sure?"
                        End If
                        Dim MyDialogResult = ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                        If MyDialogResult = DialogResult.No Then
                            RestoreCheckBoxesValue()
                            Blocked_Note_ChkBx.CheckState = CheckState.Checked
                            Exit Sub
                        End If
                    ElseIf Available_MagNotes_DGV.CurrentRow.Cells("Blocked_Note").Value = 1 And
                    Blocked_Note_ChkBx.CheckState = CheckState.Checked Then
                        If Language_Btn.Text = "E" Then
                            Msg = "مرفوض حفظ الملفات محظورة التعامل"
                        Else
                            Msg = "Saving Blocked File IS Refused"
                        End If
                        If Blocked_Note_ChkBx.CheckState = CheckState.Checked Then
                            Msg &= vbNewLine & Blocked_Note_ChkBx.Text & " (" & Blocked_Note_ChkBx.Checked & ")"
                        End If
                        If Finished_Note_ChkBx.CheckState = CheckState.Checked Then
                            Msg &= vbNewLine & Finished_Note_ChkBx.Text & " (" & Finished_Note_ChkBx.Checked & ")"
                        End If
                        If Language_Btn.Text = "ع" Then
                            Msg &= vbNewLine & "Do You Want To Change It's Status To Be Editable?"
                        Else
                            Msg &= vbNewLine & "هل تريد تغيير حالتها لتكون قابلة للتعديل؟"
                        End If
                        If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs) = DialogResult.Yes Then
                            If Blocked_Note_ChkBx.CheckState = CheckState.Checked Then
                                Blocked_Note_ChkBx.CheckState = CheckState.Unchecked
                                If Blocked_Note_ChkBx.CheckState = CheckState.Checked Then Exit Sub
                            End If
                            If Finished_Note_ChkBx.CheckState = CheckState.Checked Then
                                Finished_Note_ChkBx.CheckState = CheckState.Unchecked
                                If Finished_Note_ChkBx.CheckState = CheckState.Checked Then Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    ElseIf ActiveControl.Name = Note_TlStrp.Name Then
                        If Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                            If Language_Btn.Text = "E" Then
                                Msg = "سيتم حفظ الملف... هل انت متأكد"
                            Else
                                Msg = "This File Will Be Saved... Are You Sure?"
                            End If
                            Dim MyDialogResult = ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                            If MyDialogResult = DialogResult.No Then
                                RestoreCheckBoxesValue()
                                Exit Sub
                            End If
                        End If
                    End If
                    If SNN <> Path.GetFileNameWithoutExtension(FileNameInTheFile) Then
                        If Language_Btn.Text = "E" Then
                            Msg = "أسم الملف مخالف لذلك المسجل بداخل الملف نفسة... غير متاح حفظ الملفات على هذا الوضع... حاول مرة اخرى"
                        Else
                            Msg = "The File Name Does Not Matching To The File Name Written In The File Itself... Saving Files With The Setuation Is Not Aplicable... Kindly Try Again"
                        End If
                        Msg &= vbNewLine & "File Name = " & SNN
                        Msg &= vbNewLine & "File Name In The File = " & FileNameInTheFile
                        ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                        Exit Sub
                    End If
                End If
SaveNotNoteNotAsMagNote:
                Dim NoteName
                NoteName = "MagNote_Name -(" & Replace(FileNameToSave, ":", "(Instead Of Colon)") & ")-"
                TextToWrite = ":MagNote_Name -(" & Replace(FileNameToSave, ":", "(Instead Of Colon)") & ")-"
                If MagNote_No_CmbBx.SelectedIndex <> -1 Then
                    TextToWrite &= ":MagNote_Label -(" & DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value & ")-"
                    SNL = ":MagNote_Label -(" & DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value & ")-"
                Else
                    TextToWrite &= ":MagNote_Label -(" & MagNote_No_CmbBx.Text & ")-"
                    SNL = ":MagNote_Label -(" & MagNote_No_CmbBx.Text & ")-"
                End If
                SNN = ":MagNote_Name -(" & Replace(FileNameToSave, ":", "(Instead Of Colon)") & ")-"
                If Not FileExist Then
                    If Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                        If Language_Btn.Text = "ع" Then
                            Msg = "This Note Will Be Saved... Are You Sure?"
                        Else
                            Msg = "سيتم حفظ هذه الملاحظة... هل انت ماأكد"
                        End If
                        Dim MyDialogResult = ShowMsg(Msg & vbNewLine & SNN & vbNewLine & SNL, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                        If MyDialogResult = DialogResult.No Then
                            SaveDialogResultAnseredIsNo = True
                            Exit Sub
                        End If
                    End If
                End If
                If File.Exists(FileNameToSave) Then
                    If Not TheNoteIsAplicableToSave() Then Exit Sub
                End If
                TextToWrite &= ":MagNote_Category -(" & MagNote_Category_CmbBx.Text & ")-"
                If File.Exists(Application.StartupPath & "\Temp.File") Then
                    File.Delete(Application.StartupPath & "\Temp.File")
                End If
                RCSN.SaveFile(Application.StartupPath & "\Temp.File", RichTextBoxStreamType.RichText)
                TextToWrite &= ":MagNote -(" & Encrypt_Function(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\Temp.File", System.Text.Encoding.UTF8)) & ")-"
                File.Delete(Application.StartupPath & "\Temp.File")
                TextToWrite &= ":Blocked_Note -(" & Blocked_Note_ChkBx.CheckState & ")-"
                TextToWrite &= ":Finished_Note -(" & Finished_Note_ChkBx.CheckState & ")-"
                TextToWrite &= ":Note_Font -(" & Note_Font_TxtBx.Text & ")-"
                TextToWrite &= ":Note_Font_Color -(" & Note_Font_Color_ClrCmbBx.Text & ")-"
                TextToWrite &= ":Note_Back_Color -(" & Note_Back_Color_ClrCmbBx.Text & ")-"
                TextToWrite &= ":Alternating_Row_Color -(" & Alternating_Row_Color_ClrCmbBx.Text & ")-"
                If Grid_Panel_Size_TxtBx.TextLength > 0 Then
                    TextToWrite &= ":Grid_Panel_Size -(" & Grid_Panel_Size_TxtBx.Text & ")-"
                End If
                Dim time As DateTime = DateTime.Now
                Dim CreationDate As String
                If FileExist Then
                    CreationDate = Available_MagNotes_DGV.CurrentRow.Cells("Creation_Date").Value
                    TextToWrite &= ":Creation_Date -(" & Replace(CreationDate, ":", ",") & ")-"
                Else
                    CreationDate = time.ToString("yyyy/MM/dd hh:mm:ss.fff tt MMM ddd")
                    TextToWrite &= ":Creation_Date -(" & Replace(CreationDate, ":", ",") & ")-"
                End If
                TextToWrite &= ":Secured_Note -(" & Secured_Note_ChkBx.CheckState & ")-"
                If Secured_Note_ChkBx.CheckState = CheckState.Checked Then
                    TextToWrite &= ":Note_Password -(" & Encrypt_Function(Note_Password_TxtBx.Text) & ")-"
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
                    Note_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
                End If
                TextToWrite &= ":Note_Word_Wrap -(" & Note_Word_Wrap_ChkBx.CheckState & ")-"
                TextToWrite &= ":Note_Have_Reminder -(" & Note_Have_Reminder_ChkBx.CheckState & ")-"
                TextToWrite &= ":Next_Reminder_Time -(" & Format(Next_Reminder_Time_DtTmPkr.Value, "yyyy-MM-dd HH-mm-ss") & ")-"
                TextToWrite &= ":Reminder_Every -(" & Reminder_Every_Days_NmrcUpDn.Value & "," & Reminder_Every_Hours_NmrcUpDn.Value & "," & Reminder_Every_Minutes_NmrcUpDn.Value & ")-"
                If Not IsNothing(GridPnl(0)) Then
                    If GridPnl.Visible Then
                        CurrentFilePath = Nothing
                        If Not Save_Note_Grid() Then
                            If Language_Btn.Text = "E" Then
                                Msg = "لم يتم حفظ الجدول لوجود مشكلة... هل تريد الاستمرار في حفظ بيانات الملاحظة؟"
                            Else
                                Msg = "Grid Did'nt Saved Cause Saving Have Error... Do You Want To Continue Saving The Note Data?"
                            End If
                            Dim MyDialogResult = ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                            If MyDialogResult = DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                        If Not String.IsNullOrEmpty(CurrentFilePath) Then
                            If GridPnl.Visible And File.Exists(CurrentFilePath) Then
                                Dim GridFile = Path.GetDirectoryName(FileNameToSave) & "\GridFiles\" & Path.GetFileNameWithoutExtension(FileNameToSave) & " Grid.txt"
                                If File.Exists(GridFile) Then
                                    File.Delete(GridFile)
                                End If
                                If Encrypt(CurrentFilePath, GridFile) Then
                                    TextToWrite &= ":MagNote_Grid -(" & Replace(GridFile, ":", "(Instead Of Colon)") & ")-"
                                    File.Delete(CurrentFilePath)
                                End If
                            End If
                        End If
                    Else
                        Dim MagNoteRow = isInDataGridView(FileNameToSave, "MagNote_Name", Available_MagNotes_DGV, 0, 1)
                        If Not IsNothing(MagNoteRow) Then
                            If Not IsNothing(MagNoteRow.cells("MagNote_Grid").value) Then
                                TextToWrite &= ":MagNote_Grid -(" & Replace(MagNoteRow.cells("MagNote_Grid").value, ":", "(Instead Of Colon)") & ")-"
                            End If
                        End If
                    End If
                End If
                My.Computer.FileSystem.WriteAllText(FileNameToSave, TextToWrite, 0, System.Text.Encoding.UTF8)
                SaveMagNoteLinks()
                SaveNoteCategory(FileNameToSave)
                If systemShutdown Then
                    Exit Sub
                End If
                If Language_Btn.Text = "ع" Then
                    Msg = "The File And Its Parmeters Updated Successfully"
                Else
                    Msg = "تم حفظ الملف ومعلماته بنجاح"
                End If
                If Reload_MagNotes_After_Amendments_ChkBx.CheckState = CheckState.Unchecked And
                    Warning_Before_Save_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "E" Then
                        Msg &= "... هل هل تريد اعادة تحميل الملاحظات بعد التحديث؟"
                    Else
                        Msg = "... Do You Want To Reload The MagNotes After Update?"
                    End If
                    Dim MyDialogResult = ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False)
                    If MyDialogResult = DialogResult.Yes Then
                        Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                    End If
                Else
                    If File.Exists(FileNameToSave) And
                        Reload_MagNotes_After_Amendments_ChkBx.CheckState = CheckState.Unchecked Then
                        ReadFile(FileNameToSave,,, Available_MagNotes_DGV.CurrentRow)
                    End If
                    ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False, DisplayMsg)
                End If
                CommingFromSaveToolStripButton = True
                FileCount = String.Empty
                If Reload_MagNotes_After_Amendments_ChkBx.CheckState = CheckState.Checked Then
                    Dim SelectedItem = MagNote_No_CmbBx.Text
                    Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                    MagNote_No_CmbBx.Text = Nothing
                    MagNote_No_CmbBx.SelectedIndex = -1
                    MagNote_No_CmbBx.Text = SelectedItem
                End If
                CType(MagNoteRTF(Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSN.Name)), RichTextBox).Rtf = RCSN.Rtf
            End If
            RememberOpenedExternalFiles()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MakeTopMost()
            Focus()
            CommingFromSaveToolStripButton = False
            Cursor = Cursors.Default
            If Not String.IsNullOrEmpty(FileNameToSave) Then
                If Not IsNothing(OpenFileDialog1) Then
                    OpenFileDialog1.FileName = Nothing
                End If
            End If
            MyFormWindowState(0, 1, FormWindowState.Normal)
        End Try
    End Sub
    Private Function Save_Note_Grid() As Boolean
        Try
            CurrentFilePath = MagNoteFolderPath & "\TempFile.xlsx"
            If File.Exists(CurrentFilePath) Then
                File.Delete(CurrentFilePath)
            End If
            If SaveDocument() Then Return True
        Catch ex As Exception
            File.Delete(CurrentFilePath)
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Function UnicodeBytesToString(ByVal bytes() As Byte) As String
        Return System.Text.Encoding.Unicode.GetString(bytes)
    End Function
    Dim TextToFindExist
    Dim AcumulatedSelectionSize As Integer = 0
    Dim StoredSelectionStart As Integer = 0
    Private Function Replace_TxtBx(ByVal TextToFind As String,
                                   Optional StartPosition As Integer = 0,
                                   Optional FindNext As Boolean = False,
                                   Optional SearchEnd As Integer = 0) As Object
        Dim StoreSelectionLength
        Dim SelectionStart = RCSN.SelectionStart
        Dim RecallMe As Boolean
        Try
            Dim Diff = SearchEnd - StartPosition
            If New StackFrame(1).GetMethod().Name.Contains("Replace_TxtBx") Then
                AcumulatedSelectionSize = Diff
            Else
                StoredSelectionStart = RCSN.SelectionStart
            End If

            StoreSelectionLength = RCSN.SelectionLength

            If RCSN.SelectionLength > 0 Then
                TextToFindExist = RCSN.Find(TextToFind, RCSN.SelectionStart, SearchEnd, CType(0, RichTextBoxFinds))
            Else
                TextToFindExist = RCSN.Find(TextToFind, RCSN.SelectionStart, CType(0, RichTextBoxFinds))
            End If
            If TextToFindExist <> -1 Then
                If RCSN.SelectionStart = 0 Then
                    Dim x = 1
                End If
                RCSN.SelectedText = Replace(RCSN.SelectedText, RCSN.SelectedText, ReplaceBy_TxtBx.Text)
                FindArray(FindArray.Length - 1) = "SelectionStart = " & TextToFindExist & " Length = " & ReplaceBy_TxtBx.TextLength
                RCSN.SelectionStart = TextToFindExist
                RCSN.SelectionLength = ReplaceBy_TxtBx.TextLength
                RCSN.SelectionLength = StoreSelectionLength

                Array.Resize(FindArray, FindArray.Length + 1)
                If TextToFindExist + TextToFind.Length >= RCSN.Text.Length Then
                    If FindNext Then Return 0
                    Exit Function
                End If
                If FindNext Then
                    Return TextToFindExist + TextToFind.Length
                End If
                If SearchEnd > 0 Then
                    If StoredSelectionStart + AcumulatedSelectionSize >= SearchEnd Then
                        Exit Function
                    End If
                End If
                Replace_TxtBx(TextToFind, RCSN.SelectionStart, FindNext, SearchEnd)
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function ClearPreviousSearchResult() As Boolean
        If Clear_Previous_Search_Result_ChkBx.CheckState = CheckState.Checked Then
            ClearSearchResult()
        End If
    End Function

    Private Sub Delete_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Delete_TlStrpBtn.Click
        Try
            Dim MagNoteGridExist As Boolean = False
            If Not IsNothing(Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Grid").Value) Then
                If File.Exists(Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Grid").Value) Then
                    MagNoteGridExist = True
                End If
            End If
            Cursor = Cursors.WaitCursor
            Dim FileName
            FileName = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            If File.Exists(FileName) Then
                If Warning_Before_Delete_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "E" Then
                        Msg = "سيتم إلغاء الملف... هل انت متأكد"
                    Else
                        Msg = "This File Will Be Deleyed... Are You Sure?"
                    End If
                    If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                        Exit Sub
                    End If
                End If

                My.Computer.FileSystem.DeleteFile(FileName, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                If MagNoteGridExist Then
                    If FileName = Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Name").Value Then
                        My.Computer.FileSystem.DeleteFile(Available_MagNotes_DGV.CurrentRow.Cells("MagNote_Grid").Value, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    End If
                End If
                Dim LinksFileToDelete = Replace(RCSN.Name, ")-.txtRchTxtBx", ")-.lnks")
                If File.Exists(LinksFileToDelete) Then
                    My.Computer.FileSystem.DeleteFile(LinksFileToDelete, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If

                Msg = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value & vbNewLine
                If Language_Btn.Text = "ع" Then
                    Msg &= "This Note Sent To Recycle Ben Successfully..."
                Else
                    Msg &= "تم إرسال هذه الملاحظة إلى سلة المهملات بنجاح..."
                End If
                If Keep_Note_Opened_After_Delete_ChkBx.CheckState = CheckState.Checked Then
                    If Language_Btn.Text = "ع" Then
                        Msg &= " And Not Removed From MagNotes Preview Pane Yet"
                    Else
                        Msg &= " ولم يتم ازالتها من مربع عرض الملاحظات بعد"
                    End If
                Else
                    TabPageToClose = MagNotes_Notes_TbCntrl.SelectedTab
                    Close_Current_TabPage_Click(Me, Nothing)
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
                If Not MagNoteIsOpenedExternal() And Reload_MagNotes_After_Amendments_ChkBx.CheckState = CheckState.Checked Then
                    Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                End If
            Else
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Selecet MagNote First To Delete!!!"
                Else
                    Msg = "من فضلك إختر الملاحظة الملاحظة التى ترغب فى إلغائها!!!"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MBOs, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Dim PageNumber = 0
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Application.DoEvents()
            Dim charactersOnPage As Integer = 0
            Dim linesPerPage As Integer = 0
            ' Sets the value of charactersOnPage to the number of characters 
            ' of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(StringToPrint, Me.Font, e.MarginBounds.Size, StringFormat.GenericTypographic, charactersOnPage, linesPerPage)

            ' Draws the string within the bounds of the page.
            e.Graphics.DrawString(StringToPrint, Me.Font, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic)
            If charactersOnPage = 0 Then
                Exit Sub
            End If
            ' Remove the portion of the string that has been printed.
            StringToPrint = StringToPrint.Substring(charactersOnPage)

            ' Check to see if more pages are to be printed.
            If StringToPrint.Length > 0 Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If

            ' If there are no more pages, reset the string to be printed.
            If Not e.HasMorePages Then
                StringToPrint = DocumentContents
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
        Finally
            PageNumber += 1
        End Try
    End Sub
    Private DocumentContents As String
    Private StringToPrint As String
    Private Sub Print_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Print_Note_TlStrpBtn.Click
        PageNumber = 0
        FileToPrint = MagNoteFolderPath & "\FileToPrint.txt"
        My.Computer.FileSystem.WriteAllText(FileToPrint, RCSN.Text, 0, System.Text.Encoding.UTF8)

        Dim docName As String = "FileToPrint.txt"
        Dim docPath As String = MagNoteFolderPath
        Dim fullPath As String = System.IO.Path.Combine(docPath, docName)

        PrintDocument1.DocumentName = docName
        StringToPrint = System.IO.File.ReadAllText(fullPath, System.Text.Encoding.UTF8)
        If Language_Btn.Text = "E" Then
            Msg = "هل تريد عرض التقرير قبل ارسالها الى الطابعة؟"
        Else
            Msg = "Do You Want To Preview The Report Before Sending To The Printer?"
        End If
        If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
            PrintDocument1.Print()
            Exit Sub
        End If

        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog(Me)
    End Sub

    Private Sub Cut_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Cut_Note_TlStrpBtn.Click
        Clipboard_Cut()
    End Sub
    Private Sub Copy_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Copy_Note_TlStrpBtn.Click
        Clipboard_Copy()
    End Sub
    Private Sub Exit_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Exit_TlStrpBtn.Click
        Try
            If AlreadyAsked Or
                RunAsExternal() Then
                Exit Sub
            End If
            AlreadyAsked = True
            If Language_Btn.Text = "ع" Then
                Msg = "MagNote Will Be Always In Windows System Tray And Ready To Call By Clicking (Alt+s) Until You Exit The Note From The Windows System Tray!!!"
            Else
                Msg = "تذكر دائما أن برنامج الملاحظات الملاحظة سيكون مخفيا فى علبة الايقونات المخفية للنوافذ ويمكن استدعائة بالنقر على ـ(Alt+s)ـ... حتى يتم الخروج من البرنامج"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBOs, False)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Close()
        End Try
    End Sub

#Region "RchTxtBx"
    Private Function ChechRchTxtBx() As Boolean
        If IsNothing(RCSN(0)) Then
            If Language_Btn.Text = "ع" Then
                Msg = "Select One Of Description Object First To Deal With It!!!"
            Else
                Msg = "إختر واحد من المواضيع أولا للتعامل معه"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False)
            Return False
        End If
        Return True
    End Function

    Private Sub rtb_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Bold_Note_TlStrpBtn.Checked = sender.SelectionFont.Bold
            Underline_Note_TlStrpBtn.Checked = sender.SelectionFont.Underline
            Left_Note_TlStrpBtn.Checked = IIf(sender.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left, True, False)
            Center_Note_TlStrpBtn.Checked = IIf(sender.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center, True, False)
            Right_Note_TlStrpBtn.Checked = IIf(sender.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right, True, False)
            Bullets_Note_TlStrpBtn.Checked = sender.SelectionBullet
            Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RchTxtBx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        sender.AcceptsTab = False
    End Sub
    Dim SavedNote As Boolean
    Dim CurrentRchTxtBx As New RichTextBox
    Private Sub RchTxtBx_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name <> sender.Name Then Exit Sub
        sender.AcceptsTab = True
    End Sub
    Private Sub Text_Color_Note_TlStrpSpltBtn_ButtonClick(sender As Object, e As EventArgs) Handles Text_Color_Note_TlStrpSpltBtn.ButtonClick
        If BackIsSelected Then
            RCSN.SelectionBackColor = ColorDlg.Color
        Else
            RCSN.SelectionColor = ColorDlg.Color
        End If
    End Sub
    Dim FontDlg As New FontDialog
    Dim ColorDlg As New ColorDialog
    Dim BackIsSelected As Boolean
    Private Sub Selected_Text_Color_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Selected_Text_Color_TlStrpMnItm.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If ColorDlg.ShowDialog() <> DialogResult.Cancel Then
            Selected_Text_Color_TlStrpMnItm.BackColor = ColorDlg.Color
            RCSN.SelectionColor = ColorDlg.Color
            BackIsSelected = False
            Application.DoEvents()
        End If
    End Sub
    Private Sub Remove_Selected_Text_Color_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Remove_Selected_Text_Color_TlStrpMnItm.Click
        If Not ChechRchTxtBx() Then Exit Sub
        RCSN.SelectionColor = RCSN.ForeColor
    End Sub
    Private Sub Remove_Selected_Text_Backcolor_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Remove_Selected_Text_Backcolor_TlStrpMnItm.Click
        If Not ChechRchTxtBx() Then Exit Sub
        RCSN.SelectionBackColor = RCSN.BackColor
    End Sub

    Private Sub Select_Font_Note_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Select_Font_Note_TlStrpMnItm.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If FontDlg.ShowDialog() <> DialogResult.Cancel Then
            RCSN.SelectionFont = FontDlg.Font
            If RCSN.SelectionLength > 0 Then
                RCSN.SelectionFont = FontDlg.Font
            Else
                Note_Font_TxtBx.Text = ReturnFontString(FontDlg.Font)
            End If
        End If
    End Sub

    Private Sub Bold_Note_TlStrpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bold_Note_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Try
            Dim currentFont As System.Drawing.Font = RCSN.SelectionFont
            If IsNothing(RCSN.SelectionFont) Then
                currentFont = New System.Drawing.Font(Font.FontFamily, Font.Size, System.Drawing.FontStyle.Regular)
                RCSN.SelectionFont = New System.Drawing.Font(currentFont.FontFamily, currentFont.Size, System.Drawing.FontStyle.Regular)
            End If
            If RCSN.SelectedText.Length > 0 Then
                If RCSN.SelectionFont.Bold = True Then
                    RCSN.SelectionFont = New System.Drawing.Font(currentFont.FontFamily, currentFont.Size, System.Drawing.FontStyle.Regular)
                Else
                    RCSN.SelectionFont = New System.Drawing.Font(currentFont.FontFamily, currentFont.Size, System.Drawing.FontStyle.Bold)
                End If
            Else
                If RCSN.SelectionFont.Bold = True Then
                    RCSN.SelectionFont = New System.Drawing.Font(currentFont.FontFamily, currentFont.Size, System.Drawing.FontStyle.Regular)
                Else
                    RCSN.SelectionFont = New System.Drawing.Font(currentFont.FontFamily, currentFont.Size, System.Drawing.FontStyle.Bold)
                End If
            End If
        Catch ex As Exception
            If Not ex.Message.Contains("Object reference not set to an instance of an object.") Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Sub
    Private Sub Underline_Note_TlStrpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Underline_Note_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Dim FontBold As Boolean
        If RCSN.SelectionFont.Bold = True Then
            FontBold = True
        End If
        If RCSN.SelectionFont.Underline = True Then
            If FontBold Then
                RCSN.SelectionFont = New Font(RCSN.SelectionFont.Name, RCSN.SelectionFont.Size, FontStyle.Bold Or FontStyle.Regular)
            Else
                RCSN.SelectionFont = New Font(RCSN.SelectionFont.Name, RCSN.SelectionFont.Size, FontStyle.Regular)
            End If
        Else
            If FontBold Then
                RCSN.SelectionFont = New Font(RCSN.SelectionFont.Name, RCSN.SelectionFont.Size, FontStyle.Bold Or FontStyle.Underline)
            Else
                RCSN.SelectionFont = New Font(RCSN.SelectionFont.Name, RCSN.SelectionFont.Size, FontStyle.Underline)
            End If
        End If
    End Sub
    Private Sub Left_Note_TlStrpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Left_Note_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        RCSN.SelectionAlignment = HorizontalAlignment.Left
        RCSN.LanguageOption = RightToLeftLayout
        RCSN.RightToLeft = RightToLeft.No
    End Sub
    Private Sub Center_Note_TlStrpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Center_Note_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        RCSN.SelectionAlignment = HorizontalAlignment.Center
    End Sub
    Private Sub Right_Note_TlStrpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Right_Note_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Dim lang = InputLanguage.InstalledInputLanguages.Cast(Of InputLanguage).FirstOrDefault(Function(x) x.Culture.TwoLetterISOLanguageName = "ar")
        If lang IsNot Nothing Then InputLanguage.CurrentInputLanguage = lang
        RCSN.SelectionAlignment = HorizontalAlignment.Right
    End Sub
    Private Sub Bullets_Note_TlStrpBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bullets_Note_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        RCSN.SelectionBullet = Not RCSN.SelectionBullet
        Bullets_Note_TlStrpBtn.Checked = RCSN.SelectionBullet
    End Sub
    Private Sub WordWrap_TlStrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordWrap_TlStrp.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If RCSN.WordWrap Then
            Note_Word_Wrap_ChkBx.CheckState = CheckState.Unchecked
            RCSN.WordWrap = False
        Else
            Note_Word_Wrap_ChkBx.CheckState = CheckState.Checked
            RCSN.WordWrap = True
        End If
        Try
            If File.Exists(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) Then
                Save_Note_TlStrpBtn_Click(Save_Note_TlStrpBtn, EventArgs.Empty)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RchTxtBx_TextChanged(sender As Object, e As EventArgs)
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            If ActiveControl.Name <> sender.name Then Exit Sub
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub RchTxtBx_SelectionChanged(sender As Object, e As EventArgs)
        Try
            If RCSN(0).SelectionLength = 0 Then
                For Each Note In RchTxtBxSelectedText
                    If Note.MagNoteName = Path.GetFileName(RCSN(0).Name) Then
                        Note.MagNoteName = Nothing
                        Note.SelectedText = Nothing
                        Note.SelectedTextColumn = Nothing
                        Note.SelectedTextLength = Nothing
                        Note.SelectedTextLine = Nothing
                        Note.SelectionStart = Nothing
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub Paste_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Paste_Note_TlStrpBtn.Click
        Past_TxtBx_To_Cliboard_Click(sender, EventArgs.Empty)
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
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
            Return Nothing
        End Try

    End Function
    Dim FileNameEqualToFileAlreadyExistIn_MagNote_No_CmbBx As String
    Public Function GetTabPagByName(Optional ByVal AddTbPg As Boolean = False) As TabPage
        Try
            Dim ActvCntrl = ActiveControl
            Dim TbPgName, TbPgText, TbPgTag As String
            Dim TbPg As TabPage
            If MagNote_No_CmbBx.SelectedIndex = -1 Then
                Dim OpenFileDialog1 As New SaveFileDialog
                If String.IsNullOrEmpty(FileNameEqualToFileAlreadyExistIn_MagNote_No_CmbBx) Then
                    OpenFileDialog1.FileName = MagNote_No_CmbBx.Text
                Else
                    OpenFileDialog1.FileName = FileNameEqualToFileAlreadyExistIn_MagNote_No_CmbBx
                End If
                OpenFileDialog1.InitialDirectory = MagNoteFolderPath
                OpenFileDialog1.Filter = "Currnt File|" & OpenFileDialog1.FileName & ".txt" & "|RTF Files|*.rtf|Text Files|*.txt|All files|*.*"
                If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                    TbPgName = OpenFileDialog1.FileName
                    TbPgText = OpenFileDialog1.FileName
                    TbPgTag = OpenFileDialog1.FileName
                    AddToMagNoteNoCmbBx(OpenFileDialog1.FileName)
                    Exit Function
                    GoTo SelectedItemIsNothing
                Else
                    MagNote_No_CmbBx.SelectedIndex = -1
                    MagNote_No_CmbBx.Text = Nothing
                    Exit Function
                End If
            End If
            TbPgName = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            TbPgText = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
            TbPgTag = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
SelectedItemIsNothing:
            For Each tb In MagNotes_Notes_TbCntrl.TabPages
                If tb.name = TbPgName And
                    tb.tag = TbPgTag Then
                    If ActiveControl.Name <> Preview_Btn.Name Then
                        MagNotes_Notes_TbCntrl.SelectedTab = tb
                    End If
                    GoTo SelectTab
                End If
            Next
            If IsNothing(TbPg) And Not AddTbPg Then
                Return Nothing
            ElseIf AddTbPg Then
                If Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Unchecked Then
                    If MagNotes_Notes_TbCntrl.SelectedIndex <> -1 Then
                        MagNotes_Notes_TbCntrl.SelectedTab.Name = TbPgName
                        MagNotes_Notes_TbCntrl.SelectedTab.Text = TbPgText
                        TbPg = MagNotes_Notes_TbCntrl.SelectedTab
                        GoTo SelectTab
                    End If
                End If
AddNewTab:
                If IsNothing(TbPg) And Not DontAddTapBages Then
                    TbPg = New TabPage
                    TbPg.Name = TbPgName
                    TbPg.Text = TbPgText
                    TbPg.Tag = TbPgTag
                    MagNotes_Notes_TbCntrl.Controls.Add(TbPg)
                    MagNotes_Notes_TbCntrl.SelectedTab = TbPg
                    InitiateRchTxtBx(TbPg.Name)
                    MagNote_TxtBxAddHandler()
                    LoadMagNoteLinks()
                End If
            End If
SelectTab:
            ActiveControl = ActvCntrl
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Dim CommingFromSaveToolStripButton, ComingFromReadFile As Boolean
    Private Sub MagNote_No_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MagNote_No_CmbBx.SelectedIndexChanged
        Dim FileName
        Dim OSINT_ChStt As CheckState = Open_Note_In_New_Tab_ChkBx.CheckState
        Try
            FileName = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            If IsNothing(ActiveControl) And Not RunAsExternal(FileName) Then
                Exit Sub
            End If
            If MagNote_No_CmbBx.SelectedIndex = -1 Or
                    ActiveControl.Name = MagNotes_Notes_TbCntrl.Name Then
                Exit Sub
            End If
            NextNote.Maximum = MagNote_No_CmbBx.Items.Count - 1
            NextNote.Value = MagNote_No_CmbBx.SelectedIndex
            Cursor = Cursors.WaitCursor
            Blocked_Note_ChkBx.CheckState = CheckState.Unchecked
            Finished_Note_ChkBx.CheckState = CheckState.Unchecked
            Secured_Note_ChkBx.CheckState = CheckState.Unchecked
            Use_Main_Password_ChkBx.CheckState = CheckState.Unchecked
            Note_Word_Wrap_ChkBx.CheckState = CheckState.Unchecked
            Note_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
            Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Unchecked
            Reminder_Every_Days_NmrcUpDn.Value = 0
            Reminder_Every_Hours_NmrcUpDn.Value = 0
            Reminder_Every_Minutes_NmrcUpDn.Value = 0
            Note_Password_TxtBx.Text = Nothing
            If Not IsNothing(GetTabPagByName(1)) Then
                For Each Sheet In Grid.Worksheets
                    AddHandler Sheet.AfterCellEdit, AddressOf Worksheet_AfterCellEdit
                    Sheet.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, True)
                    Sheet.Recalculate()
                Next
            End If
            If IsNothing(ActiveControl) Or
                Not File.Exists(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) Then
                RefreshNoteSetting(MagNotes_Notes_TbCntrl.SelectedTab.Tag)
                Exit Sub
            End If
            If sender.name <> ActiveControl.Name And
                Not CommingFromSaveToolStripButton And
                ActiveControl.Name <> Next_Btn.Name And
                ActiveControl.Name <> Previous_Btn.Name And
                ActiveControl.Name <> Available_MagNotes_DGV.Name Then
                RefreshNoteSetting(MagNotes_Notes_TbCntrl.SelectedTab.Tag)
                Exit Sub
            End If
            If MagNote_No_CmbBx.SelectedIndex = -1 Then
                MagNote_No_CmbBx.Text = Nothing
                RCSN.Text = Nothing
                Exit Sub
            End If
            Dim RowIndex = isInDataGridView(FileName, "MagNote_Name", Available_MagNotes_DGV, 0)
            If Not IsNothing(RowIndex) Then
                Available_MagNotes_DGV.ClearSelection()
                Available_MagNotes_DGV.CurrentCell = Available_MagNotes_DGV.Item(0, RowIndex)
                Available_MagNotes_DGV.Rows(RowIndex).Selected = True
            Else
                If ComingFromReadFile Then
                    Exit Sub
                End If
                If MagNoteFileFormat(,, 0) Then
                ElseIf Path.GetExtension(FileName) = ".txt" Then
                    RCSN.Text = My.Computer.FileSystem.ReadAllText(FileName, System.Text.Encoding.UTF8)
                ElseIf Path.GetExtension(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) = ".rtf" Then
                    Dim RchTxtBxStyleInx = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN.Name)
                    RCSN.LoadFile(FileName, RichTextBoxStreamType.RichText)
                    If RchTxtBxStyleInx = -1 Then
                        RchTxtBxStyleInx = Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN.Name)
                        '--------------------------
                        If RchTxtBxStyleInx <> -1 Then
                            If RchTxtBxStyle(RchTxtBxStyleInx).Word_Wrap <> RCSN.WordWrap Then
                                If RCSN.WordWrap Then
                                    RchTxtBxStyle(RchTxtBxStyleInx).Word_Wrap = CheckState.Checked
                                    Note_Word_Wrap_ChkBx.CheckState = CheckState.Checked
                                Else
                                    RchTxtBxStyle(RchTxtBxStyleInx).Word_Wrap = CheckState.Unchecked
                                    Note_Word_Wrap_ChkBx.CheckState = CheckState.Unchecked
                                End If
                            End If

                            If RchTxtBxStyle(RchTxtBxStyleInx).Note_Font <> ReturnFontString(RCSN.Font) Then
                                RchTxtBxStyle(RchTxtBxStyleInx).Note_Font = ReturnFontString(RCSN.Font)
                                Note_Font_TxtBx.Text = ReturnFontString(RCSN.Font)
                            End If
                        End If
                    End If
                    RefreshNoteSetting(MagNotes_Notes_TbCntrl.SelectedTab.Tag,,,, 1)
                Else
                    If Path.GetExtension(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value) <> ".xlsx" Then
                        Cursor = Cursors.WaitCursor
                        RCSN.Text = My.Computer.FileSystem.ReadAllText(FileName, System.Text.Encoding.UTF8)
                    End If
                    Cursor = Cursors.Default
                End If
            End If
            If MagNoteIsOpenedExternal() And
                Not Path.GetExtension(FileName) = ".rtf" Then
                Dim brightness = Correct_MagNote_TxtBx_Font_Color(1)
                If brightness < 0.4 Then
                    Note_Font_Color_ClrCmbBx.Text = "Window"
                    Note_Back_Color_ClrCmbBx.Text = "WindowText"
                Else
                    Note_Font_Color_ClrCmbBx.Text = "WindowText"
                    Note_Back_Color_ClrCmbBx.Text = "Window"
                End If
            End If
            If MagNoteIsOpenedExternal() Then
                Available_MagNotes_DGV.ClearSelection()
            End If
        Catch ex As Exception
            If Not ex.Message.Contains("Object reference Not Set To an instance Of an Object.") Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0, 0)
            End If
        Finally
            Try
                Setting_TbCntrl.Invalidate()
                MagNotes_Notes_TbCntrl.Invalidate()
                ShortCut_TbCntrl.Invalidate()
                If MagNote_No_CmbBx.SelectedIndex <> -1 Then
                    '--------------------------
                    If Not DontAddTapBages Then
                        If Array.FindIndex(RchTxtBxStyle, Function(f) f.RchTxtBx_Name = RCSN.Name) = -1 Then
                            Application.DoEvents()
                            AddMagNoteRTF()
                        End If
                    End If
                    ShowMsg(Alert_Comment_TxtBx.Text, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False, 0,, 0,, 0)
                    If MagNotes_Notes_TbCntrl.SelectedIndex <> -1 Then
                        If Not String.IsNullOrEmpty(FileName) Then
                            GetFileType()
                        End If
                    End If
                End If
                Open_Note_In_New_Tab_ChkBx.CheckState = OSINT_ChStt
                Note_Password_TxtBx.Text = Nothing
                Cursor = Cursors.Default
                Spliter_1_Lbl.BringToFront()
                ComingFromReadFile = False
                RCSN(0).Focus()
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0, 0)
            End Try
        End Try
    End Sub
    Public Function MagNoteFileFormat(Optional ByVal FileName As String = Nothing,
                                         Optional ByVal ReturnTrueOrFalse As Boolean = False,
                                         Optional ByVal AddFileToMagNoteComboBox As Boolean = True) As Boolean
        Try
            AddAvailable_MagNotes_DGVColumns()
            Dim NoteFileNameFormat = "MagNote -()-"
            Dim FileToRead
            If Not String.IsNullOrEmpty(FileName) Then
                FileToRead = FileName
            Else
                FileToRead = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            End If
            Dim FileToReadFormate As String
            FileToReadFormate = Microsoft.VisualBasic.Left(Path.GetFileNameWithoutExtension(FileToRead), ("MagNote -(").Length) & Microsoft.VisualBasic.Right(Path.GetFileNameWithoutExtension(FileToRead), (")-").Length)

            If FileToRead.Length <= NoteFileNameFormat.Length Then
            ElseIf FileToReadFormate = NoteFileNameFormat Then
                If ReturnTrueOrFalse Then
                    Return True
                Else
                    If AddFileToMagNoteComboBox Then
                        ReadFile(FileToRead,, 1,,, 1)
                    Else
                        ReadFile(FileToRead,, 1)
                    End If
                    Return True
                End If
            End If
        Catch ex As Exception
        Finally
        End Try
    End Function

    Public Function MagNoteIsOpenedExternal() As Boolean
        If MagNoteFolderPath <> MagNotes_Folder_Path_TxtBx.Text And
            Not MagNoteFileFormat(, 1, 0) Then
            Return True
        End If
    End Function
    Private Sub Language_Btn_Click(sender As Object, e As EventArgs) Handles Language_Btn.Click
        Try
            Cursor = Cursors.WaitCursor
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
                    RightToLeftLayout = False
                    RightToLeft = RightToLeft.No
                    Language_Btn.Text = "ع"
                Case <> "E"
                    For Each language In myInputLanguage
                        If myCurrentLanguage.LayoutName <> language.LayoutName Then
                            InputLanguage.CurrentInputLanguage = language
                            LabelingForm("Arabic")
                        End If
                    Next
                    Language_Btn.Text = "E"
            End Select
            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            MakeTopMost()
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Form_Transparency_TrkBr_ValueChanged(sender As Object, e As EventArgs) Handles Form_Transparency_TrkBr.ValueChanged
        If Form_Transparency_TrkBr.Enabled Then
            Me.Opacity = Form_Transparency_TrkBr.Value / 100
            Application.DoEvents()
        End If
    End Sub

    Private Sub Available_MagNotes_DGV_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles Available_MagNotes_DGV.SortCompare
        Try
            Cursor = Cursors.WaitCursor
            If IsNothing(e.CellValue1) Then Exit Sub
            If String.IsNullOrEmpty(e.CellValue1.ToString) Or
                String.IsNullOrEmpty(e.CellValue2.ToString) Then
                e.SortResult = String.Compare(e.CellValue1.ToString, e.CellValue2.ToString)
                e.Handled = True
            End If
            Available_MagNotes_DGV.ClearSelection()
        Catch ex As Exception
            e.Handled = False
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Note_Back_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Note_Back_Color_ClrCmbBx.SelectedIndexChanged

    End Sub

    Private Sub MagNote_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles MagNote_No_CmbBx.TextChanged
    End Sub

    Private Sub Form_Color_Like_Note_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Form_Color_Like_Note_ChkBx.CheckedChanged

    End Sub

    Private Sub Form_Color_Like_Note_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Form_Color_Like_Note_ChkBx.CheckStateChanged
        If Form_Color_Like_Note_ChkBx.CheckState = CheckState.Checked Then
            Note_Form_Color_ClrCmbBx.Enabled = False
        Else
            Note_Form_Color_ClrCmbBx.Enabled = True
        End If
    End Sub

    Private Sub Secured_Note_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Secured_Note_ChkBx.CheckedChanged

    End Sub

    Private Sub Secured_Note_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Secured_Note_ChkBx.CheckStateChanged
        If Secured_Note_ChkBx.CheckState = CheckState.Checked Then
            Note_Password_TxtBx.ReadOnly = False
        ElseIf Secured_Note_ChkBx.CheckState = CheckState.Unchecked Then
            Note_Password_TxtBx.ReadOnly = True
        End If
    End Sub

    Public Sub Open_Note_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Open_Note_TlStrpBtn.Click
        Try
            Dim OpenFileDialog As New OpenFileDialog
            If RunAsExternal(UseArgFile) Or
                Not String.IsNullOrEmpty(UseArgFile) Then
                OpenFileDialog.FileName = ExternalFilePath & "\" & ExternalFileName
                If Not OpenFileDialog.FileName = "\" Then
                    GoTo EXUseArgFile
                End If
            End If
            OpenFileDialog.Title = "Importing Files"
            OpenFileDialog.Filter = "Text Files|*.txt|RTF Files|*.rtf|All files|*.*"
            OpenFileDialog.Multiselect = False
            OpenFileDialog.FileName = ""
            OpenFileDialog.RestoreDirectory = True
            If OpenFileDialog.ShowDialog = DialogResult.Cancel Then
                Exit Sub
            Else
                Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
            End If
EXUseArgFile:
            MagNote_No_CmbBx.SelectedIndex = -1
            MagNote_No_CmbBx.Focus()
            Dim OpenFileDialogFileName = Path.GetFileName(OpenFileDialog.FileName)
            If Note_Back_Color_ClrCmbBx.SelectedIndex = -1 Then
                Note_Font_Name_CmbBx.Text = "Times New Roman"
                Note_Font_Color_ClrCmbBx.Text = "WindowText"
                Note_Back_Color_ClrCmbBx.Text = "Snow"
            End If
            If Not Convert.ToBoolean(ParseCommandLineArgs(OpenFileDialog.FileName)) Then
                Exit Sub
            ElseIf MagNoteFileFormat(OpenFileDialog.FileName, 1, 0) Then
                MagNote_No_CmbBx.SelectedIndex = -1
                Available_MagNotes_DGV.Focus()
                Me.ActiveControl = Available_MagNotes_DGV
                If isInDataGridView(OpenFileDialog.FileName, "MagNote_Name", Available_MagNotes_DGV,,, 1, 1) Then
                    Exit Sub
                End If
            End If
            If Path.GetExtension(OpenFileDialog.FileName) = ".rtf" Then
            ElseIf Path.GetExtension(OpenFileDialog.FileName) = ".xlsx" Or
                Path.GetExtension(OpenFileDialog.FileName) = ".xls" Then
                LoadFile(OpenFileDialog.FileName)
                SetCurrentDocumentFile(OpenFileDialog.FileName)
            Else
                'Dim MagNote = My.Computer.FileSystem.ReadAllText(OpenFileDialog.FileName, System.Text.Encoding.UTF8)
                'RCSN.Text = MagNote
                'RCSN.BringToFront()
                'If RunAsExternal() Then
                '    RefreshNotExistingFile()
                'End If
            End If
            If IsArabicWord(Microsoft.VisualBasic.Left(RCSN().Text, 1)) Then
                Right_Note_TlStrpBtn_Click(Right_Note_TlStrpBtn, EventArgs.Empty)
            ElseIf IsEnglishWord(Microsoft.VisualBasic.Left(RCSN().Text, 1)) Then
                Left_Note_TlStrpBtn_Click(Left_Note_TlStrpBtn, EventArgs.Empty)
            Else
                Left_Note_TlStrpBtn_Click(Left_Note_TlStrpBtn, EventArgs.Empty)
            End If
            Try
                If ActiveControl.Name <> sender.name Then
                    Dim x = 1
                End If
            Catch ex As Exception
                MyFormWindowState()
            End Try
            SelectFileFormat(OpenFileDialog.FileName)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    ''' <summary>
    ''' FWS = 
    ''' MARMS = 
    ''' </summary>
    ''' <param name="MeIsActiveForm"></param>
    ''' <param name="DebuggerIsAttached"></param>
    ''' <param name="FWS"> = FormWindowState </param>
    ''' <param name="MARMS"> = Minimize_After_Running_My_Shortcut </param>
    ''' <returns></returns>
    Private Function MyFormWindowState(Optional ByVal MeIsActiveForm As Boolean = False,
                                       Optional ByVal DebuggerIsAttached As Boolean = False,
                                       Optional FWS As FormWindowState = FormWindowState.Normal,
                                       Optional ByVal MARMS As Boolean = False,
                                       Optional ByVal TrayClick As Boolean = False,
                                       Optional ByVal WidowsStrtup As Boolean = False)
        Try
            If DebuggerIsAttached And Debugger.IsAttached Then
                Me.WindowState = FWS
                Exit Function
            End If
            Dim IAIF = IsApplicationInForeground()
            If Not IAIF And Not WidowsStrtup Then
                If Me.WindowState = FormWindowState.Minimized Then
                    If Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked Then
                        Me.WindowState = FormWindowState.Normal
                    ElseIf Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                        Me.WindowState = FormWindowState.Normal
                    ElseIf Me_Is_Compressed_ChkBx.CheckState = CheckState.Indeterminate Then
                        Me.WindowState = FormWindowState.Maximized
                    End If
                End If
                Me.BringToFront()
                Me.Activate()
                Application.DoEvents()
                Exit Function
            End If
            If MARMS Then '
                If Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                    WindowState = FormWindowState.Minimized
                ElseIf Minimize_After_Running_My_Shortcut_ChkBx.CheckState = CheckState.Indeterminate Then
                    If Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                        MeIsComlressed()
                    End If
                End If
                Exit Function
            End If
            If WidowsStrtup Then
                If Application_Starts_Minimized_ChkBx.CheckState = CheckState.Unchecked Then
                    Me.WindowState = FormWindowState.Minimized
                ElseIf Application_Starts_Minimized_ChkBx.CheckState = CheckState.Checked Then
                    Me.WindowState = FormWindowState.Normal
                ElseIf Application_Starts_Minimized_ChkBx.CheckState = CheckState.Indeterminate Then
                    Me.WindowState = FormWindowState.Maximized
                End If
                Exit Function
            End If
            If Not MeIsActiveForm Then
                If Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked Then
                    Me.WindowState = FormWindowState.Normal
                ElseIf Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                    MeIsComlressed()
                ElseIf Me_Is_Compressed_ChkBx.CheckState = CheckState.Indeterminate Then
                    Me.WindowState = FormWindowState.Maximized
                End If
                MakeTopMost(1)
                Me.BringToFront()
            Else
                If Me.ActiveForm IsNot Nothing AndAlso
                Me.ActiveForm Is Me Then
                    Me.WindowState = FormWindowState.Minimized
                ElseIf Me.WindowState = FormWindowState.Minimized Then
                    MyFormWindowState(,, Nothing,, 1)
                ElseIf Me.WindowState <> FormWindowState.Minimized Then
                    MakeTopMost(1)
                    Me.Activate()
                    Me.BringToFront()
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    ' Import the GetForegroundWindow function from user32.dll
    <DllImport("user32.dll")>
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    ' Import the GetActiveWindow function from user32.dll
    <DllImport("user32.dll")>
    Private Shared Function GetActiveWindow() As IntPtr
    End Function

    ' Method to check if the application is in the foreground
    Private Function IsApplicationInForeground() As Boolean
        ' Get the handle of the currently foreground window
        Dim foregroundWindow As IntPtr = GetForegroundWindow()
        ' Get the handle of the current form
        Dim currentWindow As IntPtr = Me.Handle
        ShowMsg(foregroundWindow.ToString & "    " & currentWindow.ToString, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)

        ' Compare the handles
        Return foregroundWindow = currentWindow
    End Function


    Public Function SelectFileFormat(ByVal FileName As String)
        If Not DirectCast(Me, MagNote_Form).MagNoteFileFormat(FileName) Then
            Dim FileExtention = Replace(Path.GetExtension(FileName), ".", "")
            Dim SelectedItem = File_Format_CmbBx.Items.Cast(Of KeyValuePair(Of String, String))().FirstOrDefault(Function(r) r.Key.Equals(FileExtention))
            If IsNothing(SelectedItem.Value) Then
                If Language_Btn.Text = "E" Then
                    File_Format_CmbBx.Text = "مفكرة النوافذ (txt)"
                Else
                    File_Format_CmbBx.Text = "Text Document (txt)"
                End If
            Else
                File_Format_CmbBx.Text = SelectedItem.Value
            End If
        End If
    End Function
    Private Function ReadRTFFile() As Boolean
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.Multiselect = False
        OpenFileDialog.FileName = ""
        OpenFileDialog.RestoreDirectory = True
        If OpenFileDialog.ShowDialog = DialogResult.Cancel Then Exit Function
        Dim rtftext As String = String.Empty
        For Each Line In My.Computer.FileSystem.ReadAllText(OpenFileDialog.FileName, System.Text.Encoding.UTF8)
            rtftext &= Line
        Next
        RCSN.Text = rtftext
    End Function
    Private Sub Save_Setting_When_Exit_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Save_Setting_When_Exit_ChkBx.CheckedChanged
    End Sub

    Private Sub Form_Size_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Form_Size_TlStrpBtn.Click
        If IsNothing(ActiveControl) Then Exit Sub
        If Setting_TbCntrl.Visible Then
            Setting_TbCntrl.Visible = False
        Else
            Setting_TbCntrl.Visible = True
        End If
    End Sub

    Private Sub Note_Back_Color_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Note_Back_Color_ClrCmbBx.SelectedValueChanged
        If sender.SelectedIndex = -1 Then Exit Sub
        If Note_Back_Color_ClrCmbBx.SelectedItem = TransparencyKey Then
            If Language_Btn.Text = "E" Then
                Msg = "This Color Will Make Your Note Transparent Background... Do You Want To Continue?"
            Else
                Msg = "هذا اللون سيجعل خلفية الملاحظة شفافة... هل تريد الإستمرار؟"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                If Note_Back_Color_ClrCmbBx.SelectedIndex + 1 = Note_Back_Color_ClrCmbBx.Items.Count Then
                    Note_Back_Color_ClrCmbBx.SelectedIndex = 0
                Else
                    Note_Back_Color_ClrCmbBx.SelectedIndex = Note_Back_Color_ClrCmbBx.SelectedIndex + 1
                End If
            End If
        End If
        Try
            ChngClr()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Function ChngClr()
        If Not Note_Back_Color_ClrCmbBx.Text.Equals("Transparent") Then
            If Form_Color_Like_Note_ChkBx.CheckState = CheckState.Checked Then
                If CurrentProcess <> "LoadOpenedTabPages" Then
                    ChangeFormControlsColors(Note_Back_Color_ClrCmbBx.Text, 0)
                End If
            Else
                If Not IsNothing(RCSN(0)) Then
                    RCSN.BackColor = Color.FromName(Note_Back_Color_ClrCmbBx.Text)
                End If
            End If
        End If
    End Function
    Private Sub MagNote_Form_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Form_Transparency_TrkBr.Enabled = False
        Try
            If Not RunAsExternal(UseArgFile) Then
                If Language_Btn.Text = "ع" Then
                    ShowWindowsNotification("InfoSysMe MagNote Is Running Here..." & vbNewLine & "Press Alt+s To Show Me →")
                Else
                    ShowWindowsNotification("→" & "إن شاء الله ستجدنى هنا..." & vbNewLine & "فقط إضغط مفتاحى  (Alt+s) لإظهارى")
                End If
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                Latitude_TxtBx.Text = 30.0444
                Longitude_TxtBx.Text = 31.2358
                LoadCalculationMethods()
                PrayerTime()
                SetAppParameters()
                Without_Opened_Notes_At_Startup_ChkBx_CheckStateChanged(Without_Opened_Notes_At_Startup_ChkBx, EventArgs.Empty)
                CheckPublicCategory()
            Else
                Me.Opacity = 0.95
            End If

            External_Note_Font_Name_CmbBx.Text = "Times New Roman"
            External_Note_Font_Color_ClrCmbBx.Text = "Black"
            External_Note_Back_Color_ClrCmbBx.Text = "Snow"
            External_Note_Alternating_Row_Color_ClrCmbBx.Text = "Silver"
            If Debugger.IsAttached Then
                Dim UserApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                File.Copy(Application.StartupPath & "\" & Application.ProductName & ".exe",
                      UserApplicationDataFolder & "\InfoSysMe\MagNote\" & Application.ProductName & ".exe", 1)
            End If

            Dim SNN_CmbBx = MagNote_No_CmbBx.Text
            Dim SNNC_CmbBx = MagNote_Category_CmbBx.Text
            If Language_Btn.Text = "E" Then
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("txt", "ملاحظة (txt)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("txt", "مفكرة النوافذ (txt)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("rtf", "مربع نص منسق (rtf)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("html", "لغة ترميز النصوص التشعبية (html)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("htm", "لغة ترميز النصوص التشعبية (htm)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("xml", "لغة التوصيف الموسعة (xml)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("vbs", "لغة النص البصرى الأساسى (vbs)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("ini", "ملف التهيئة / التكوين (ini)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("config", "ملف التهيئة (config)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("bat", "ملف الحزمة (bat)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("cmd", "ملف الأمر (cmd)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("sql", "لغة الاستعلام الهيكلية (sql)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("xlsx", "مايكروسوفت اكسل (xlsx)"))
                File_Format_CmbBx.Text = "ملاحظة (txt)"
                File_Format_CmbBx.RightToLeft = RightToLeft.Yes
            Else
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("txt", "MagNote (txt)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("txt", "Text Document (txt)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("rtf", "Rich Text Box (rtf)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("html", "Hypertext Markup Language (html)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("htm", "Hypertext Markup Language (htm)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("xml", "eXtensible Markup Language (xml)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("vbs", "Virtual Basic Script (vbs)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("ini", "Initialization/Configuration File (ini)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("config", "Configuration File (config)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("bat", "Batch File (bat)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("cmd", "Command (cmd)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("sql", "Structured Query Language (sql)"))
                File_Format_CmbBx.Items.Add(New KeyValuePair(Of String, String)("xlsx", "Microsoft Excel (xlsx)"))
                File_Format_CmbBx.RightToLeft = RightToLeft.No
                File_Format_CmbBx.Text = "MagNote (txt)"
            End If
            If Not IsNothing(RCSN(0)) Then
                If RCSN.Text.Length > 0 Then
                    If MagNotes_Notes_TbCntrl.TabPages.Count > 0 Then
                        If MagNotes_Notes_TbCntrl.SelectedIndex <> -1 Then
                            If IsNothing(MagNotes_Notes_TbCntrl.SelectedTab.Controls(MagNotes_Notes_TbCntrl.SelectedTab.Name & "RchTxtBx")) Then
                                MagNotes_Notes_TbCntrl.SelectedTab.Controls.Add(RchTxtBx)
                            End If
                        End If
                    End If
                End If
            End If
            Table_Pnl.Location = New Point(3, 28)
            Table_Pnl.Size = New Size(104, 21)
            If Backup_Folder_Path_TxtBx.TextLength = 0 Then
                Backup_Folder_Path_TxtBx.Text = Application.StartupPath & "\Note_Backup_Folder"
            End If
            If MagNotes_Folder_Path_TxtBx.TextLength = 0 Then
                MagNotes_Folder_Path_TxtBx.Text = Application.StartupPath & "\MagNotes_Files"
            End If
            MagNoteFolderPath = MagNotes_Folder_Path_TxtBx.Text
            Try
                If Not System.IO.Directory.Exists(MagNoteFolderPath) Then
                    System.IO.Directory.CreateDirectory(MagNoteFolderPath)
                End If
                If Not System.IO.Directory.Exists(MagNoteFolderPath & "\Shortcuts") Then
                    System.IO.Directory.CreateDirectory(MagNoteFolderPath & "\Shortcuts")
                End If
                If Not System.IO.Directory.Exists(MagNoteFolderPath & "\Shortcuts\Images") Then
                    System.IO.Directory.CreateDirectory(MagNoteFolderPath & "\Shortcuts\Images")
                End If
            Catch UAE As UnauthorizedAccessException
                ShowMsg(UAE.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Catch ex As Exception
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\MagNotes_Files")
            End Try

            If Periodically_Backup_MagNotes_ChkBx.CheckState = CheckState.Checked And
            Not RunAsExternal(UseArgFile) Then
                Backup_Timer.Start()
            Else
                Backup_Timer.Stop()
                Alert_Tmr.Stop()
                Azan_Tmr.Stop()
            End If
            If RunAsExternal(UseArgFile) Then
                Preview_Btn.Enabled = False
                Save_Note_Form_Parameter_Setting_Btn.Enabled = False
                RememberOpenedExternalFiles()
                GoTo OpenExternalFile
            End If
            MagNote_No_CmbBx.Text = Nothing
ReEnterMainPassword:
            If Enter_Password_To_Pass_ChkBx.CheckState = CheckState.Checked Then
                If Debugger.IsAttached Then GoTo PassOk
                Enabled = False
                PassedMainPasswordToPass = False
                Application_Initializing_Form.Size = New Point(461, 190)
                Application_Initializing_Form.Focus()
                Application_Initializing_Form.BringToFront()
                While Application_Initializing_Form.Visible
                    Application.DoEvents()
                    If Main_Password_TxtBx.Text = EnteredPassword And
                    Not String.IsNullOrEmpty(EnteredPassword) Then
                        Application_Initializing_Form.Size = New Point(461, 123)
                        Exit While
                    End If
                End While
                If Main_Password_TxtBx.Text <> EnteredPassword Or
                String.IsNullOrEmpty(EnteredPassword) Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Wrong Main Password Do You Want To Tay Again?"
                    Else
                        Msg = "كلمة السر خاطئة... هل تريد إعادة المحاولة"
                    End If
                    If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MBOs, False) = DialogResult.No Then
                        End
                    Else
                        GoTo ReEnterMainPassword
                    End If
                ElseIf Main_Password_TxtBx.Text = EnteredPassword Then
PassOk:
                    PassedMainPasswordToPass = True
                    Enabled = True
                End If
            End If
            MyFormWindowState(,,,,, 1)
            If IsNothing(Setting_TbCntrl.TabPages("MagNotes_TbPg")) Then
                GoTo MagNotesTbPgNotVizible
            End If
            LoadMagNoteAtStartup(SNNC_CmbBx, SNN_CmbBx)
MagNotesTbPgNotVizible:
            Dim TbPg = MagNotes_Notes_TbCntrl.TabPages.Cast(Of TabPage)().FirstOrDefault(Function(r) r.Text.Equals(SNN_CmbBx))
            If Not IsNothing(TbPg) Then
                Application.DoEvents()
                ActiveControl = MagNote_No_CmbBx
                IsInMagNoteCmbBx(TbPg.Tag, 1)
            End If

OpenExternalFile:
            If Not MagNoteIsOpenedExternal() Then
                Available_MagNotes_DGV.Sort(Available_MagNotes_DGV.Columns("Creation_Date"), System.ComponentModel.ListSortDirection.Descending)
            End If
            CreatMenu()
            If Check_For_New_Version_ChkBx.CheckState = CheckState.Checked And
            Not RunAsExternal() Then
                If Check_For_New_Version() Then
                    Update_New_Version_Form.Show(Me)
                    Update_New_Version_Form.File_Name_TxtBx.Text = UpdateFileName
                    Update_New_Version_Form.Current_Version_TxtBx.Text = My.Application.Info.Version.ToString
                    Update_New_Version_Form.Update_Version_TxtBx.Text = UpdateFileVersion
                    Update_New_Version_Form.Update_Download_File_Path_TxtBx.Text = UpdateDownloadFilePath
                End If
            End If
            Text = GetMyInfo()
            Application.DoEvents()
            Me_As_Default_Text_File_Editor_ChkBx_CheckStateChanged(Me_As_Default_Text_File_Editor_ChkBx, EventArgs.Empty)
            If Not RunAsExternal() Then
                File_Format_CmbBx.Text = "ملاحظة (txt)"
                New_Note_TlStrpBtn.PerformClick()
            Else
                SelectFileFormat(RCSN.Name)
            End If
            If Ignore_Error_Message_For_Connection_ChkBx.CheckState = CheckState.Checked Then
                IgnoreErrorMessageForConnection = False
            Else
                IgnoreErrorMessageForConnection = True
            End If

        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Try
                If Not RunAsExternal(UseArgFile) Then
                    For Each TbPg In ShortCut_TbCntrl.TabPages
                        CType(ShortCut_TbCntrl.Controls(TbPg.Text).Controls(TbPg.Text), ListView).AllowDrop = True
                    Next
                End If
                RCSN.AllowDrop = True
                PrintPreviewDialog1 = New PrintPreviewDialog
                'Set the size, location, and na
                PrintPreviewDialog1.ClientSize = New System.Drawing.Size(Width, Height)
                PrintPreviewDialog1.Location = New System.Drawing.Point(Top + 20, Left + 20)
                PrintPreviewDialog1.Name = "PrintPreviewDialog1"
                ' Set the minimum size the dialog can be resized to.
                PrintPreviewDialog1.MinimumSize = New System.Drawing.Size(375, 250)
                ' Set the UseAntiAlias property to true, which will allow the 
                ' operating system to smooth fonts.
                PrintPreviewDialog1.UseAntiAlias = True
                PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
                IgnoreNoteAmendmented = False
                If Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked Then
                    TopMost = False
                End If
                Cursor = Cursors.Default
                ActiveControl = MagNote_No_CmbBx
                MagNotes_Notes_TbCntrl.BringToFront()
                If Not Debugger.IsAttached Then
                    Upload_Last_Version_Btn.Visible = False
                    Upload_Last_Version_Btn.BringToFront()
                    AddHandler_Control_Move(Upload_Last_Version_Btn)
                End If
                If Not RunAsExternal(UseArgFile) Then
                    AddHandler_Control_Move(MagNote_PctrBx)
                    AddHandler_Control_Move(Hide_Me_PctrBx)
                    Preview_Available_Alerts_Btn_Click(Preview_Available_Alerts_Btn, EventArgs.Empty)
                    FTP_User_Name_TxtBx.Text = FTP_Login(0).FTP_UserName
                    FTP_Address_TxtBx.Text = "ftp://" & FTP_Login(0).FTP_UserName & "@" & FTP_Login(0).FTP_Address & "/infosysme.com/MagNoteFiles/"
                End If
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End Try
        End Try
        If Not RunAsExternal(UseArgFile) Then
            Try
                Available_Alerts_DGV.ClearSelection()
                Application.DoEvents()
                Form_Transparency_TrkBr.Enabled = True
                Application_Initializing_Form.Close()
                Me.Opacity = Form_Transparency_TrkBr.Value / 100
                If Load_MagNote_At_Startup_ChkBx.CheckState = CheckState.Indeterminate Then
                    Me.ActiveControl = Available_MagNotes_DGV
                    isInDataGridView(Split(CurrentMagNoteName, ",").ToList.Item(1), "MagNote_Name", Available_MagNotes_DGV,,, 1)
                End If
                RCSN(0).Focus()
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End Try
        End If
        Application.DoEvents()
        For Each TpBg In MagNotes_Notes_TbCntrl.TabPages
            Dim RchTxtBxName = TpBg.Name & "RchTxtBx"
            Try
                Dim Txt1 = CType(MagNoteRTF(Array.FindIndex(MagNoteRTF, Function(f) f.Name = RchTxtBxName)), RichTextBox).Text
                Dim Txt2 = CType(MagNotes_Notes_TbCntrl.TabPages(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TpBg)).Controls(RchTxtBxName), RichTextBox).Text
                CType(MagNoteRTF(Array.FindIndex(MagNoteRTF, Function(f) f.Name = RchTxtBxName)), RichTextBox).Rtf = CType(MagNotes_Notes_TbCntrl.TabPages(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TpBg)).Controls(RchTxtBxName), RichTextBox).Rtf
            Catch ex As Exception
            End Try
            Dim x = 1
        Next

    End Sub
    Dim DontAddTapBages As Boolean
    Private Function LoadMagNoteAtStartup(ByVal SNNC_CmbBx, ByVal SNN_CmbBx) As Boolean
        Try

            ActiveControl = Preview_Btn
            LoadCategories()
            'CheckState.Checked"تحميل جميع الملفات فى بداية البرنامج"
            'CheckState.Unchecked "تحميل الملفات المفتوحة فقط فى بداية البرنامج"
            'CheckState.Indeterminate "تحميل الملف الحالي فقط فى بداية البرنامج"
            If Load_MagNote_At_Startup_ChkBx.CheckState = CheckState.Checked Then
                MagNote_Category_CmbBx.SelectedIndex = -1
                MagNote_Category_CmbBx.Text = Nothing
                Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
                LoadOpenedTabPages()
            ElseIf Load_MagNote_At_Startup_ChkBx.CheckState = CheckState.Unchecked Then
                LoadOpenedTabPages()
            ElseIf Load_MagNote_At_Startup_ChkBx.CheckState = CheckState.Indeterminate Then
                AddLoadOpenedTabPages(Split(CurrentMagNoteName, ",").ToList.Item(1))
                DontAddTapBages = True
                LoadOpenedTabPages()
                Application.DoEvents()
                DontAddTapBages = False
            End If
            MagNote_Category_CmbBx.Text = SNNC_CmbBx
            MagNote_No_CmbBx.Text = SNN_CmbBx
        Finally
            DontAddTapBages = False
        End Try
    End Function

    Dim User_Password_TxtBx As New TextBox
    Private Function EnterPasswordToPass() As Boolean
        Input_Form = New Form
        Input_Form.Name = "User_Password_Form"
        Dim FindText_Lbl As New Label
        FindText_Lbl.Name = "FindText_Lbl"
        User_Password_TxtBx = New TextBox
        User_Password_TxtBx.Name = "User_Password_TxtBx"
        Input_Form.Icon = My.Resources.MagNote
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
        Else
            Input_Form.Text = "شاشة كلمة سر المرور"
        End If
        Input_Form.StartPosition = FormStartPosition.CenterScreen
        Input_Form.Show(Application_Initializing_Form)
        Input_Form.Activate()
        Input_Form.Focus()
        User_Password_TxtBx.Focus()
    End Function
    Public EnteredPassword As String = String.Empty
    Public Sub User_Password_TxtBx_TextChanged(sender As Object, e As EventArgs)
        If User_Password_TxtBx.TextLength >= Main_Password_TxtBx.TextLength Then
            EnteredPassword = User_Password_TxtBx.Text
            Input_Form.Close()
        End If
    End Sub
    Private Sub CheckStateChanged(sender As Object, e As EventArgs) Handles Use_Main_Password_ChkBx.CheckStateChanged,
        Note_Word_Wrap_ChkBx.CheckStateChanged,
        Note_Have_Reminder_ChkBx.CheckStateChanged,
        Blocked_Note_ChkBx.CheckStateChanged,
        Finished_Note_ChkBx.CheckStateChanged,
        Secured_Note_ChkBx.CheckStateChanged
        Try
            If IsNothing(RCSN(0)) Then
                Exit Sub
            End If
            Select Case sender.name
                Case Finished_Note_ChkBx.Name
                    RCSN.ReadOnly = Convert.ToBoolean(Finished_Note_ChkBx.CheckState)
                    GridReadonly(Convert.ToBoolean(Finished_Note_ChkBx.CheckState))
                Case Blocked_Note_ChkBx.Name
                    RCSN.ReadOnly = Convert.ToBoolean(Blocked_Note_ChkBx.CheckState)
                    GridReadonly(Convert.ToBoolean(Blocked_Note_ChkBx.CheckState))
                Case Note_Word_Wrap_ChkBx.Name
                    RCSN.WordWrap = Convert.ToBoolean(Note_Word_Wrap_ChkBx.CheckState)
            End Select
            If IsNothing(ActiveControl) Then
                Exit Sub
            End If
            If ActiveControl.Name <> sender.name Or
                MagNote_No_CmbBx.SelectedIndex = -1 Then
                Exit Sub
            End If
            If ActiveControl.Name <> sender.name Then Exit Sub
            Note_TlStrp.Focus()
            ActiveControl = Note_TlStrp
            Dim FileExtension = DirectCast(File_Format_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
            If (FileExtension = "MagNote (txt)" Or FileExtension = "ملاحظة (txt)") Then
                Save_Note_TlStrpBtn_Click(Save_Note_TlStrpBtn, EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub GridReadonly(ByVal RdOnly As Boolean)
        If IsNothing(Font_TlStrp(0)) Then
            Exit Sub
        End If
        If RdOnly Then
            Font_TlStrp.Enabled = False
            SaveAndBorder_TlStrp.Enabled = False
            Grid.Readonly = True
        Else
            Font_TlStrp.Enabled = True
            SaveAndBorder_TlStrp.Enabled = True
            Grid.Readonly = False
        End If
    End Sub



    Dim ReminderTime = Microsoft.VisualBasic.DateAndTime.Timer + 30

    Private Sub Backup_Timer_Tick(sender As Object, e As EventArgs) Handles Backup_Timer.Tick
        Try
            Backup_Timer.Stop()
            Dim PBC As Boolean = False
            While Microsoft.VisualBasic.DateAndTime.Timer >= ReminderTime
                ReminderTime = Microsoft.VisualBasic.DateAndTime.Timer + 180
                For Each Remind In Reminders
                    Dim NowTime = Format(Now, "yyyy-MM-dd HH-mm-ss")
                    If Not NowTime >= Remind.ReminderTime Then Continue For
                    Dim StrdTime = Remind.ReminderTime
                    If Format(Now, "yyyy-MM-dd HH-mm-ss") >= Format(Next_Reminder_Time_DtTmPkr.Value, "yyyy-MM-dd HH-mm-ss") Then
                        ActiveControl = Available_MagNotes_DGV
                        For Each Note In Available_MagNotes_DGV.Rows
                            If Note.cells("MagNote_Name").value = Remind.NoteName Then
                                If Language_Btn.Text = "ع" Then
                                    Msg = "The Note [" & Remind.NoteLabel & "] Have Reminder Time Now " & vbNewLine & "Reminder Time " & Remind.ReminderTime & vbNewLine & "Now Ti.. Do You Want To View It Now?" & NowTime
                                Else
                                    Msg = "الملاحظة [" & Remind.NoteLabel & "] لها وقت تنبية الآن " & vbNewLine & "وقت التنبية " & Remind.ReminderTime & vbNewLine & "الوقت الآن... هل تريد عرضها الآن؟" & NowTime
                                End If
                                If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                                    MyFormWindowState()
                                Else
                                    Exit Sub
                                End If
                                Available_MagNotes_DGV.CurrentCell = Available_MagNotes_DGV(0, Note.index)
                                Available_MagNotes_DGV.ClearSelection()
                                CType(MagNoteRTF(Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSN.Name)), RichTextBox).Rtf = Nothing
                                ActiveControl = Available_MagNotes_DGV
                                Available_MagNotes_DGV.Rows(Note.index).Selected = True
                                Exit For
                            End If
                        Next
                        Application.DoEvents()
                    End If
                Next
                Next_Reminder_Time_DtTmPkr.Value = Now.AddMinutes(3)
            End While

            If Periodically_Backup_MagNotes_ChkBx.CheckState = CheckState.Unchecked Then
                Backup_Timer.Stop()
                Exit Sub
            End If
            If IsDate(Next_Backup_Time_TxtBx.Text) Then
                Dim storeddatetime As DateTime = CType(Next_Backup_Time_TxtBx.Text, DateTime)
                Dim nowdatetime As DateTime = CType(Now, DateTime)
                If nowdatetime >= storeddatetime Then
                    TackBackup()
                End If
            Else
                Next_Backup_Time_TxtBx.Text = Now
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Backup_Timer.Start()
        End Try
    End Sub

    Private Sub Days_NmrcUpDn_ValueChanged(sender As Object, e As EventArgs) Handles Minutes_NmrcUpDn.ValueChanged, Hours_NmrcUpDn.ValueChanged, Days_NmrcUpDn.ValueChanged
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            If ActiveControl.Name <> sender.name Then Exit Sub
            Dim BT As DateTime = Now
            BT = BT.AddDays(Days_NmrcUpDn.Value)
            BT = BT.AddHours(Hours_NmrcUpDn.Value)
            BT = BT.AddMinutes(Minutes_NmrcUpDn.Value)
            Next_Backup_Time_TxtBx.Text = BT
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Backup_Folder_Path_TxtBx_Click(sender As Object, e As EventArgs) Handles Backup_Folder_Path_Btn.Click
        Try
            Dim BFP As New FolderBrowserDialog
            If BFP.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Backup_Folder_Path_TxtBx.Text = BFP.SelectedPath
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub MagNotes_Folder_Path_TxtBx_Click(sender As Object, e As EventArgs) Handles MagNotes_Folder_Path_Btn.Click
        Try
            Dim BFP As New FolderBrowserDialog
            If BFP.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                MagNotes_Folder_Path_TxtBx.Text = BFP.SelectedPath
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
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
            mnuDisplayForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                Restart = New ToolStripMenuItem("Restart Program")
            Else
                Restart = New ToolStripMenuItem("إعادة تشغيل البرنامج")
            End If
            Restart.BackgroundImage = My.Resources.Background4
            Restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                MeAlwaysOnTop = New ToolStripMenuItem("Me Always On Top")
            Else
                MeAlwaysOnTop = New ToolStripMenuItem("أنا دائما في المقدمة")
            End If
            MeAlwaysOnTop.BackgroundImage = My.Resources.Background4
            MeAlwaysOnTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                CheckForUpdates = New ToolStripMenuItem("Check For Updates")
            Else
                CheckForUpdates = New ToolStripMenuItem("إختبر وجود تحديث للبرنامج")
            End If
            CheckForUpdates.BackgroundImage = My.Resources.Background4
            CheckForUpdates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            mnuSep1 = New ToolStripSeparator()
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                LockScreen = New ToolStripMenuItem("Lock Screen")
            Else
                LockScreen = New ToolStripMenuItem("إغلاق الشاشة")
            End If
            LockScreen.BackgroundImage = My.Resources.Background4
            LockScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                RestartWindows = New ToolStripMenuItem("Restart Windows")
            Else
                RestartWindows = New ToolStripMenuItem("إعادة تشغيل النوافذ")
            End If
            RestartWindows.BackgroundImage = My.Resources.Background4
            RestartWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                ShutdownWindows = New ToolStripMenuItem("Shutdown Windows")
            Else
                ShutdownWindows = New ToolStripMenuItem("إغلاق النوافذ")
            End If
            ShutdownWindows.BackgroundImage = My.Resources.Background4
            ShutdownWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                LogOffUser = New ToolStripMenuItem("Log Off User")
            Else
                LogOffUser = New ToolStripMenuItem("تسجيل خروج المستخدم")
            End If
            LogOffUser.BackgroundImage = My.Resources.Background4
            LogOffUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            '---------------------------------------------------------------------------------
            mnuSep2 = New ToolStripSeparator()
            '---------------------------------------------------------------------------------
            If Language_Btn.Text = "ع" Then
                mnuExit = New ToolStripMenuItem("Exit")
            Else
                mnuExit = New ToolStripMenuItem("خروج")
            End If
            mnuExit.BackgroundImage = My.Resources.Background4
            mnuExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            MainMenu = New ContextMenuStrip
            MainMenu.Items.AddRange(New ToolStripItem() {
                                    mnuDisplayForm,
                                    MeAlwaysOnTop,
                                    CheckForUpdates,
                                    Restart,
                                    mnuSep1,
                                    LockScreen,
                                    LogOffUser,
                                    RestartWindows,
                                    ShutdownWindows,
                                    mnuSep2,
                                    mnuExit})
            Tray = New NotifyIcon
            Tray.Icon = My.Resources.MagNote
            Tray.ContextMenuStrip = MainMenu
            Tray.Text = "Profissional MagNote"
            Tray.Visible = True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

#Region "Tray Fuctions"
    Private Sub LockScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockScreen.Click
        LockMe()
    End Sub

    Private Sub ShutdownWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutdownWindows.Click
        Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
        System.Diagnostics.Process.Start("shutdown", "-s -t 00")
        'This will make the computer Shutdown
    End Sub

    Public Sub RestartWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartWindows.Click
        If Save_Setting_When_Exit_ChkBx.CheckState = CheckState.Checked Then
            Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
        End If
        System.Diagnostics.Process.Start("shutdown", "-r -t 00")
        'This will make the computer Restart
    End Sub
    Private Declare Function ExitWindowsEx Lib "user32.dll" (ByVal uFlags As Long, ByVal dwReserved As Long) As Long
    Private Const EWX_FORCE = 4         'const for a forced logoff
    Private Sub LogOffUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOffUser.Click
        'System.Diagnostics.Process.Start("shutdown", "-l -t 00")
        Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
        ExitWindowsEx(EWX_FORCE, 4)
        'This will make the computer Log Off 
    End Sub

    Public Sub Restart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Restart.Click
        Try
            Dim arguments As String() = Environment.GetCommandLineArgs()
            For Each arg In arguments
                If arg = Application.StartupPath & "\" & Application.ProductName & ".exe" Then Continue For
                CreatSchedualTask(, 1)
                ApplicationRestart = True
                Application.Exit()
                Exit Sub
            Next


            Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
            ApplicationRestart = True
            Close()
            Application.DoEvents()
            Application.Restart()

        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub mnuDisplayForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDisplayForm.Click
        Try
            If ShowMsg("Do You Want To Locate The Screen To The Default Location... Current Location Is (" & Me.Location.ToString & ") And Stored Location Is (" & Note_Form_Location_TxtBx.Text & ")", "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                Location = New Point(0, 0)
            End If
            Me.Size = ReturnSize(Note_Form_Size_TxtBx.Text)
            MyFormWindowState()
            If Not Visible Then
                ShowDialog()
            End If
            MakeTopMost()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Location = New Point(0, 0)
            MyFormWindowState()
            Me.Size = New Size(356, 549)
        End Try
    End Sub
    Private Sub MeAlwaysOnTop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MeAlwaysOnTop.Click
        Try
            If TopMost = True Then
                Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked
                TopMost = False
            Else
                Me_Always_On_Top_ChkBx.CheckState = CheckState.Checked
                MakeTopMost()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub CheckForUpdates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckForUpdates.Click
        Try
            If Check_For_New_Version(1) Then
                Update_New_Version_Form.Show(Me)
                Update_New_Version_Form.File_Name_TxtBx.Text = UpdateFileName
                Update_New_Version_Form.Current_Version_TxtBx.Text = My.Application.Info.Version.ToString
                Update_New_Version_Form.Update_Version_TxtBx.Text = UpdateFileVersion
                Update_New_Version_Form.Update_Download_File_Path_TxtBx.Text = UpdateDownloadFilePath
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub OpenNotificationSettings()
        Try
            ' Open the Windows 10 notifications & actions settings page
            Process.Start("ms-settings:notifications")
        Catch ex As Exception
            MessageBox.Show("Unable to open the notification settings. Please open them manually.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Try
            AlreadyAsked = True
            If Save_Setting_When_Exit And Not IsNothing(ActiveControl) Then
                If Not Debugger.IsAttached Then
                    If IfOpenedTabPagesChanged() And Load_MagNote_At_Startup_ChkBx.CheckState <> CheckState.Indeterminate Then
                        MyFormWindowState()
                        If Language_Btn.Text = "E" Then
                            Msg = "الصفحات المفتوحة حاليا مخالفة لتلك التى تم البدء بها هل تريد تحديث ملف الصفحات المفتوحة سابقا؟"
                        Else
                            Msg = "The Currently Opened Tab Pages Are Different Than The Started With It. Do You Want To Update The Previously Opened Tab Pages File?"
                        End If
                        If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            SaveOpenedTabPages()
                        End If
                    End If
                End If
                Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
            End If
            ExitingProgram = True
            If NoteAmendmented() = DialogResult.Cancel Then
                ExitingProgram = False
                Exit Sub
            End If
            If ApplicationRestart Then Exit Sub
            UpdateAIOAssemblyVersion()
            If Not Tray Is Nothing Then
                Tray.Visible = False
                Tray.Dispose()
                Tray = Nothing
            End If
            Dispose()
            End
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Tray_Click(sender As Object, e As EventArgs) Handles Tray.Click
        If Not MouseDounRight Then
            MyFormWindowState(,,,, 1)
        End If
    End Sub
    Dim MouseDounRight As Boolean
    Private Sub Tray_MouseDown(sender As Object, e As MouseEventArgs) Handles Tray.MouseDown
        Try
            MouseDounRight = False
            If e.Button = Windows.Forms.MouseButtons.Left Then
                If Not Visible Then
                    ShowDialog()
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

    Private Sub Note_Password_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Note_Password_TxtBx.TextChanged
        Try
            If Note_Password_TxtBx.TextLength = 0 Or
               String.IsNullOrEmpty(Available_MagNotes_DGV.CurrentRow.Cells("Note_Password").Value) Then Exit Sub
            Dim x = Decrypt_Function(Available_MagNotes_DGV.CurrentRow.Cells("Note_Password").Value)
            If Decrypt_Function(Available_MagNotes_DGV.CurrentRow.Cells("Note_Password").Value) = Note_Password_TxtBx.Text Then
                Dim arg = New DataGridViewRowEventArgs(Available_MagNotes_DGV.CurrentRow)
                ActiveControl = Available_MagNotes_DGV
                Available_MagNotes_DGV_SelectionChanged(Available_MagNotes_DGV, arg)
                CType(MagNoteRTF(Array.FindIndex(MagNoteRTF, Function(f) f.Name = RCSN.Name)), RichTextBox).Rtf = Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub SaveToolStripButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Save_Note_TlStrpBtn.MouseDown
        ActiveControl = Note_TlStrp
    End Sub
    Private Sub MagNote_No_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles MagNote_No_CmbBx.Validating
        Try
            Exit Sub
            If MagNote_No_CmbBx.SelectedIndex <> -1 Then
                If File.Exists(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key) Then
                    Exit Sub
                End If
            End If
            If (MagNote_No_CmbBx.SelectedIndex = -1 And
                 MagNote_No_CmbBx.Text.Length > 0) Or
                 MagNote_No_CmbBx.Text.Contains(":") Then
                If Language_Btn.Text = "ع" Then
                    Msg = "This Note Is Not Found In Stored Files Or Its Label May Contains The Character (:)... Kindly Try Again Or If You Want To Change The Note Kinldy Put Mark On (Note Applicable To Rename) Box Before Amendment!!! Or MagNotes Not Loaded Yet Or Maybe This File Was Opened From Outside The MagNotes Program And There Was No Mark On The Object - (Remember Opened External Files)... " & vbNewLine & "Do You Want To Correct To The Optimal Situation To Change The Name Of The Note Or To Save New MagNote And Try Again?"
                Else
                    Msg = "لم يتم العثور على هذه الملاحظة في الملفات المخزنة أو قد يحتوي عنوانها على الحرف (:)" &
                        vbNewLine & "يرجى المحاولة مرة أخرى" &
                        vbNewLine & "أو إذا كنت تريد تغيير إسم الملاحظة فيرجى وضع علامة على المربع (ملاحظة قابلة لإعادة التسمية) قبل التعديل!!!" &
                        vbNewLine & " أو لم يتم تحميل MagNotes بعد" &
                        vbNewLine & "أو ربما تم فتح هذا الملف من خارج
                        برنامج MagNotes ولم تكن هناك علامة على الكائن - (تذكر الملفات الخارجية المفتوحة)ـ" &
                        vbNewLine & "هل تريد التصحيح الى الوضع الامثل لتغيير الاسم أو لحفظ ملاحظة جديدة واعادة المحاولة؟"
                End If
                If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
                    Exit Sub
                End If
                If Not IsNothing(Available_MagNotes_DGV.CurrentRow) Then
                    Dim arg = New DataGridViewRowEventArgs(Available_MagNotes_DGV.CurrentRow)
                    ActiveControl = Available_MagNotes_DGV
                    Available_MagNotes_DGV_SelectionChanged(Available_MagNotes_DGV, arg)
                End If
                e.Cancel = True
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub RchTxtBx_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs)
        Try
            Cursor = Cursors.WaitCursor
            If Language_Btn.Text = "ع" Then
                Msg = "Are You Sure Thes Link Will Be Executed?"
            Else
                Msg = "هل انت متأكد... سيتم تنفيذ هذا الارتباط؟"
            End If
            If ShowMsg(Msg & vbNewLine & e.LinkText, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False,, 3) = DialogResult.No Then
                Exit Sub
            End If
            MyFormWindowState(,,, 1)
            Dim link = RCSN.SelectedRtf
            If e.LinkText.ToLower.StartsWith("file://") Then
                System.Diagnostics.Process.Start(LCase(e.LinkText).Replace(LCase("[[InsteadOfSpace]]"), " "))
            Else
                System.Diagnostics.Process.Start(e.LinkText)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & e.LinkText & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Public StoredFindText_CmbBx As New ComboBox
    Dim FindArray(0) As String
    Dim Input_Form As New Form
    Dim FindText_Lbl As New Label
    Dim ReplaceBy_Lbl As New Label
    Dim FindText_CmbBx As New ComboBox
    Dim ReplaceBy_TxtBx As New TextBox
    Dim FindIn_CmbBx As New ComboBox
    Dim FindIn_Lbl As New Label
    Dim FindNext_Btn As New Button
    Dim FindAll_Btn As New Button
    Dim ReplaceNext_Btn As New Button
    Dim ReplaceAll_Btn As New Button
    Dim ClearSearchResult_Btn As New Button
    Dim Exit_Btn As New Button
    Private Sub Find_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Find_TlStrpBtn.Click
        Try
            Dim SelectedText = RCSN.SelectedText
            If Not Find_Form.Visible Then
                Find_Form.Show(Me)
            End If
            Find_Form.FindText_CmbBx.Text = SelectedText
            Find_Form.FindText_CmbBx.Focus()
        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    '#Region "Search Form"
    Public Sub Input_Form_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Labeling_Form(sender, "Arabic")
    End Sub
    Public Sub Input_Form_FormClosing(sender As Object, e As FormClosingEventArgs)
        Input_Form.Dispose()
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

    Private Function GetCurrentPosition(Optional ByVal ReturnPosotopn As Boolean = False) As Point
        Try
            Dim Position As Integer = RCSN.SelectionStart
            Dim Line As Integer = RCSN.GetLineFromCharIndex(Position) + 1
            Dim Col As Integer = Position - RCSN.GetFirstCharIndexOfCurrentLine
            Dim SelectionLength = RCSN.SelectionLength
            If MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text.Contains("Current Position [") Then
                For Each Lin As String In Split(MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text, vbNewLine)
                    If Lin.Contains("Current Position") Then
                        MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = Replace(MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text, Lin, "Current Position [Line = " & Line & " Column = " & Col & "] Cursor Position [" & Position & "] Selection Text Length [" & SelectionLength & "]")
                        Exit For
                    End If
                Next
            Else
                MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text = MsgBox_SttsStrp.Items("MsgBox_TlStrpSttsLbl").Text & vbNewLine & "Current Position [Line = " & Line & " Column = " & Col & "] Cursor Position [" & Position & "] Selection Text Length [" & SelectionLength & "]"
            End If
            Return New Point(Line, Col)
        Catch ex As Exception
        End Try
    End Function
    Private Sub RchTxtBx_KeyPress(sender As Object, e As KeyPressEventArgs)
        GetCurrentPosition()
    End Sub
    Private Sub RchTxtBx_KeyUp(sender As Object, e As KeyEventArgs)
        GetCurrentPosition()
    End Sub
    Private Sub RchTxtBx_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Try
            If e.Control AndAlso e.Shift AndAlso (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down) Then
                If RCSN(0).SelectionLength = 0 Then
                    Exit Sub
                End If
                For Each Note In RchTxtBxSelectedText
                    If Note.MagNoteName = Path.GetFileName(RCSN(0).Name) Then
                        RCSN(0).DeselectAll()
                        Exit For
                    End If
                Next
                Dim x = 1
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim MouseButtonsRight As Boolean
    Private Sub RchTxtBx_MouseDown(sender As Object, e As MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then Exit Sub
            ' Get the character index at the mouse click position
            Dim charIndex As Integer = RCSN.GetCharIndexFromPosition(e.Location)
            ' Loop through the dictionary to find which link was clicked
            For Each kvp In LinkMap
                If IsNothing(kvp) Then Continue For
                Dim CurrentPosition = GetCurrentPosition()
                Dim LinkLine = kvp.SelectionLine
                Dim RCSName = kvp.MagNoteName
                Dim SelectionStart = kvp.SelectionStart
                Dim SelectionLength = kvp.SelectionLength
                Dim LinkToRun = kvp.URL
                Dim RCSNSelectionStart = RCSN.SelectionStart
                Dim MagNoteName = Path.GetFileName(RCSN().Name)
                If Val(LinkLine) = CurrentPosition.X And
                    Val(RCSNSelectionStart) >= Val(SelectionStart) And
                    Val(RCSNSelectionStart) <= Val(SelectionStart) + Val(SelectionLength) And
                    MagNoteName = RCSName Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Are You Sure Thes Link Will Be Executed?"
                    Else
                        Msg = "هل انت متأكد... سيتم تنفيذ هذا الارتباط؟"
                    End If
                    If ShowMsg(Msg & vbNewLine & LinkToRun, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False,, 3) = DialogResult.No Then
                        Exit Sub
                    End If
                    System.Diagnostics.Process.Start(kvp.URL)
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Function LoadMagNoteLinks() As Boolean
        Dim FileToLoad = Replace(RCSN.Name, ")-.txtRchTxtBx", ")-.lnks")
        If Not File.Exists(FileToLoad) Then Exit Function
        Dim LineText() As String
        For Each Lnk In Split(My.Computer.FileSystem.ReadAllText(FileToLoad, System.Text.Encoding.UTF8), vbCrLf)
            If String.IsNullOrEmpty(Lnk) Then Continue For
            LineText = Split(Lnk, ",")
            Dim foundPerson As LinkMapDetils = Array.Find(LinkMap, Function(p) p.SelectionStart = LineText(2))
            If Not IsNothing(foundPerson.MagNoteName) Then Continue For
            LinkMap(LinkMap.Length - 1).MagNoteName = LineText(0)
            LinkMap(LinkMap.Length - 1).SelectionLine = LineText(1)
            LinkMap(LinkMap.Length - 1).SelectionStart = LineText(2)
            LinkMap(LinkMap.Length - 1).SelectionLength = LineText(3)
            LinkMap(LinkMap.Length - 1).URL = LineText(4)
            ReDim Preserve LinkMap(LinkMap.Length)
            RCSN.SelectionStart = LineText(2)
            RCSN.SelectionLength = LineText(3)
            RCSN.SelectionFont = New Font(RCSN.Font, FontStyle.Underline)
            RCSN.SelectionColor = Color.Blue
            RCSN.DeselectAll()
        Next
        If Language_Btn.Text = "E" Then
            Msg = "تم بحمد الله إنشاء الرابط بنجاح"
        Else
            Msg = "Thank God, The Link Added Successfully"
        End If
        ShowMsg(Msg & vbNewLine & RCSN.Name, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MBOs, False, 0)
    End Function
    Private Function SaveMagNoteLinks() As Boolean
        Dim LineToSave As String = String.Empty
        For Each Lnk In LinkMap
            If Path.GetFileName(Lnk.MagNoteName) = Path.GetFileName(RCSN.Name) Then
                LineToSave &= Lnk.MagNoteName & "," & Lnk.SelectionLine & "," & Lnk.SelectionStart & "," & Lnk.SelectionLength & "," & Lnk.URL & vbNewLine
            End If
        Next
        If LineToSave.Length > 0 Then
            Dim FilePath = Replace(RCSN.Name, ")-.txtRchTxtBx", ")-.lnks")
            My.Computer.FileSystem.WriteAllText(FilePath, LineToSave, 0, System.Text.Encoding.UTF8)
        End If
    End Function
    Structure LinkMapDetils
        Dim MagNoteName As String
        Dim SelectionLine As Integer
        Dim SelectionStart As Integer
        Dim SelectionLength As Integer
        Dim URL As String
    End Structure

    Private LinkMap(0) As LinkMapDetils
    Public Sub InsertLink(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If RCSN.SelectionLength = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "إختر المساحة المناسبة لك لاستخدامها فى عملية الربط"
                Else
                    Msg = "Select Suitable Area To Be Linked"
                End If
                Msg &= vbNewLine & "URL Like http(s)://infosysme.com/" & vbNewLine
                Msg &= "URL Like mailto://someone@mailhost.com/" & vbNewLine
                Msg &= "URL Like file://D:\Desktop\download.jpg"
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MBOs, False)
                Exit Sub
            End If
            Dim Position = GetCurrentPosition()
            Dim iData As IDataObject = Clipboard.GetDataObject()
            'Check to see if the data is in a text format
            If iData.GetDataPresent(DataFormats.Text) Then
                If Not IsNothing(iData.GetData(DataFormats.Text)) Then
                    Dim URl = CType(iData.GetData(DataFormats.UnicodeText), String)
                    Dim HttpPattern As String = "http(s)?://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?"
                    If Regex.IsMatch(URl, HttpPattern) Then
                        GoTo CreateTheLink
                    ElseIf File.Exists(Replace(URl, "file://", "")) Then
                        GoTo CreateTheLink
                    Else
                        Try
                            Dim a As New System.Net.Mail.MailAddress(Replace(URl, "mailto:", ""))
                            GoTo CreateTheLink
                        Catch ex As Exception
                        End Try
                    End If

NotCorrectURL:
                    If Language_Btn.Text = "E" Then
                        Msg = "لا تحتوى ذاكرة الجهاز على رابط صحيح للربط"
                    Else
                        Msg = "The Machine Memory Has No Correct URL To Be Linked"
                    End If
                    Msg &= "URL Like http(s)://infosysme.com/" & vbNewLine
                    Msg &= "URL Like mailto://someone@mailhost.com/" & vbNewLine
                    Msg &= "URL Like file://D:\Desktop\download.jpg"
                    ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MBOs, False)
                    Exit Sub
CreateTheLink:
                    RCSN.SelectionFont = New Font(RCSN.Font, FontStyle.Underline)
                    RCSN.SelectionColor = Color.Blue
                    Dim RCSName = Path.GetFileName(RCSN.Name)
                    Dim Inx = 0
                    For Each lnk In LinkMap
                        If RCSName = lnk.MagNoteName And
                                Position.X = lnk.SelectionLine And
                                RCSN.SelectionStart = lnk.SelectionStart And
                                RCSN.SelectionLength = lnk.SelectionLength And
                                URl = lnk.URL Then
                            Dim gBookList As List(Of LinkMapDetils) = LinkMap.ToList
                            gBookList.RemoveAt(Inx)
                            LinkMap = gBookList.ToArray
                            Exit For
                        End If
                        Inx += 1
                    Next
                    LinkMap(LinkMap.Length - 1).MagNoteName = RCSName
                    LinkMap(LinkMap.Length - 1).SelectionLine = Position.X
                    LinkMap(LinkMap.Length - 1).SelectionStart = RCSN.SelectionStart
                    LinkMap(LinkMap.Length - 1).SelectionLength = RCSN.SelectionLength
                    LinkMap(LinkMap.Length - 1).URL = URl
                    ReDim Preserve LinkMap(LinkMap.Length)
                    If Language_Btn.Text = "E" Then
                        Msg = "تم بحمد الله إنشاء الرابط بنجاح"
                    Else
                        Msg = "Thank God, The Link Added Successfully"
                    End If
                    ShowMsg(Msg & vbNewLine & URl, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MBOs, False,, 3)
                End If
            Else
                GoTo NotCorrectURL
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub RchTxtBx_MouseUp(sender As Object, e As MouseEventArgs)
        Try
            If e.Button <> MouseButtons.Right Then
                GetCurrentPosition()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function ClearSearchResult()
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            If IsNothing(FindArray) Then Exit Function
            Dim SelectionStart = RCSN.SelectionStart
            Dim SelectionLength = RCSN.SelectionLength
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, FindArray.Count)
            For Each SelectedText In FindArray.ToList
                progress += ProgressToAdd
                Previewlbl.Text = "}Clearing--> " & SelectedText & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                If IsNothing(SelectedText) Then Continue For
                Dim ST = Split(SelectedText, "=")
                RCSN.SelectionStart = Replace(Replace(ST(1), "Length", ""), " ", "")
                RCSN.SelectionLength = Replace(ST(2), " ", "")
                RCSN.SelectionBackColor = RCSN.BackColor
                RCSN.SelectionColor = RCSN.ForeColor
            Next
            Application.DoEvents()
            RCSN.SelectionStart = Val(SelectionStart)
            RCSN.SelectionLength = Val(SelectionLength)
            Dim valueArray(0) As String
            FindArray = valueArray
            FindArray.Initialize()

        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Function
    Dim SizeChangedInProgress As Boolean
    Dim PreviousSize As New Size
    Public Shortcuts_LstVw As ListView
    Public imageList1 As ImageList
    Private Sub FileList_DragDrop(sender As Object, e As DragEventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim lvi As ListViewItem
            Dim CurrentShortcutsLstVw = CType(ShortCut_TbCntrl.Controls(sender.name).Controls(sender.name), ListView)
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
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
                    If CurrentShortcutsLstVw.Items.Count > 0 Then
                        If Not IsNothing(CurrentShortcutsLstVw.FindItemWithText(item.Text, False, 0, False)) Then Continue For
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
                    lvi = CurrentShortcutsLstVw.Items.Add(item)
                    lvi.Tag = File
                Next
                Application.DoEvents()
                CurrentShortcutsLstVw.Sorting = SortOrder.Ascending
                SaveList(CurrentShortcutsLstVw.Name)
                LoadList(CurrentShortcutsLstVw.Name)
            ElseIf e.Data.GetDataPresent(GetType(System.Windows.Forms.ListViewItem)) Then
                Dim breakfast As ListView.SelectedListViewItemCollection = DragFromListview.SelectedItems
                For Each item In breakfast
                    DragFromListview.Items.Remove(item)
                    CType(sender, ListView).Items.Add(item)
                Next
                SaveList(CurrentShortcutsLstVw.Name, 0)
                LoadList(CurrentShortcutsLstVw.Name)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub FileList_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        ElseIf e.Data.GetDataPresent(GetType(Object())) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListViewItem") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub
    Private Function CreatCotrolPanelTabPage() As Boolean
        Try
            Dim shell As New Shell32.Shell()
            Dim controlPanelNamespace As Shell32.Folder = shell.NameSpace(Shell32.ShellSpecialFolderConstants.ssfCONTROLS)
            Dim tabPage As New TabPage With {
            .Text = "CotrolPanel",
            .Name = "CotrolPanel"
        }
            CreatNewListView(tabPage)
            ShortCut_TbCntrl.TabPages.Add(tabPage)
            Dim DeleteShortCut As Boolean
            DialogResult = DialogResult.None
            Cursor = Cursors.WaitCursor
            CType(tabPage.Controls(tabPage.Text), ListView).LargeImageList = imageList1
            CType(tabPage.Controls(tabPage.Text), ListView).SmallImageList = imageList1
            CType(tabPage.Controls(tabPage.Text), ListView).View = View.LargeIcon
            imageList1.ImageSize = New Size(48, 48)
            Dim MyIconSize = IconSize.ExtraLarge
            CType(tabPage.Controls(tabPage.Text), ListView).AllowColumnReorder = True
            CType(tabPage.Controls(tabPage.Text), ListView).Sorting = SortOrder.Ascending
            CType(tabPage.Controls(tabPage.Text), ListView).ShowItemToolTips = True
            imageList1.ColorDepth = ColorDepth.Depth32Bit
            Dim item As ListViewItem
            Dim Extension
            Dim IsFolder As Boolean
            CType(tabPage.Controls(tabPage.Text), ListView).Items.Clear()
            Dim icon As Icon
            Dim ConrolPanelFilePath As String '= "Control Panel\All Control Panel Items\"
            LoadControlPanelItems(CType(tabPage.Controls(tabPage.Text), ListView), imageList1)
            CType(tabPage.Controls(tabPage.Text), ListView).ListViewItemSorter = New ListViewItemComparer(0)
            CType(tabPage.Controls(tabPage.Text), ListView).Sort()
            Load_Shortcuts_ToolTips(CType(tabPage.Controls(tabPage.Text), ListView))
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Private Sub LoadControlPanelItems(listView As ListView, imageList As ImageList)
        ' Initialize Shell32.Shell
        Dim shell As New Shell32.Shell()
        Dim controlPanelNamespace As Shell32.Folder = shell.NameSpace(Shell32.ShellSpecialFolderConstants.ssfCONTROLS)
        Dim Icon As Icon
        ' Add each item in the Control Panel to the ListView
        For Each item As Shell32.FolderItem In controlPanelNamespace.Items()
            ' Retrieve the actual path for the item
            Dim itemPath As String = item.Path
            'Using IFFI As New IconFromFolderItem_Clss
            '    ' Retrieve the icon for the item
            '    icon = IFFI.GetIconFromFolderItem(item)
            'End Using
            Try

                Using IH As New IconHelper
                    Icon = IH.GetIconFrom(itemPath, IconSize.ExtraLarge, 0)
                End Using
            Catch ex As Exception

            End Try
            ' Add the icon to the ImageList
            If Icon IsNot Nothing Then
                imageList.Images.Add(Icon)
            End If

            ' Create a ListViewItem
            Dim listItem As New ListViewItem(item.Name) With {
                .Tag = itemPath,
                .ImageIndex = imageList.Images.Count - 1
            }
            listView.Items.Add(listItem)
        Next
    End Sub


    Private Function GetActualPath(item As Shell32.FolderItem) As String
        Try
            ' If the item has a path, return it
            If Not String.IsNullOrEmpty(item.Path) Then
                Return item.Path
            End If
            ' If the item does not have a path, use the parsing name
            Dim controlPanelNamespace As String = "::{26EE0668-A00A-44D7-9371-BEB064C98683}\0\"
            Dim parsingName As String = item.Path
            If parsingName.StartsWith(controlPanelNamespace) Then
                parsingName = parsingName.Replace(controlPanelNamespace, "Control Panel\\")
            End If
            Return parsingName
        Catch ex As Exception
            ' Handle any errors
            Return String.Empty
        End Try
    End Function
    Private Function CreatMyDesktopTabPage() As Boolean
        Try
            Dim tabPage As New TabPage With {
                .Text = "My Desktop",
                .Name = "My Desktop"
            }
            CreatNewListView(tabPage)
            ShortCut_TbCntrl.TabPages.Add(tabPage)
            Dim DeleteShortCut As Boolean
            DialogResult = DialogResult.None
            Cursor = Cursors.WaitCursor
            CType(tabPage.Controls(tabPage.Text), ListView).LargeImageList = imageList1
            CType(tabPage.Controls(tabPage.Text), ListView).SmallImageList = imageList1
            CType(tabPage.Controls(tabPage.Text), ListView).View = View.LargeIcon
            imageList1.ImageSize = New Size(48, 48)
            Dim MyIconSize = IconSize.ExtraLarge
            CType(tabPage.Controls(tabPage.Text), ListView).AllowColumnReorder = True
            CType(tabPage.Controls(tabPage.Text), ListView).Sorting = SortOrder.Ascending
            CType(tabPage.Controls(tabPage.Text), ListView).ShowItemToolTips = True
            imageList1.ColorDepth = ColorDepth.Depth32Bit
            Dim lvi As ListViewItem
            Dim item As ListViewItem
            Dim Extension
            Dim IsFolder As Boolean
            CType(tabPage.Controls(tabPage.Text), ListView).Items.Clear()
            Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Dim shortcutFiles As String() = Directory.GetFiles(desktopPath)
            Dim shortcutDirectories As String() = Directory.GetDirectories(desktopPath, "*.*")
            For Each Drctry In shortcutDirectories
                ReDim Preserve shortcutFiles(shortcutFiles.Length)
                shortcutFiles(shortcutFiles.Length - 1) = Drctry
            Next
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)
            shortcutDirectories = Directory.GetFiles(desktopPath)
            For Each Drctry In shortcutDirectories
                ReDim Preserve shortcutFiles(shortcutFiles.Length)
                shortcutFiles(shortcutFiles.Length - 1) = Drctry
            Next

            For Each File In shortcutFiles
                Dim Fil() = Split(File, "$")
                Try
                    IsFolder = (System.IO.File.GetAttributes(Fil(0)) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                Catch ex As Exception
                    If DialogResult = DialogResult.Cancel Then Continue For
                    If Language_Btn.Text = "ع" Then
                        Msg = "You Can Reload All The Shortcuts Again By Right Clicking On The List"
                    Else
                        Msg = "يمكنك إعادة تحميل جميع الاختصارات مرة أخرى عن طريق النقر فوق القائمة بزر الماوس الأيمن... هل تريد إلغاء الاختصار؟"
                    End If
                    DialogResult = ShowMsg(ex.Message & Msg & vbNewLine & Fil(0), "InfoSysMe (MagNote)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    If DialogResult = DialogResult.Cancel Then
                        Continue For
                    ElseIf DialogResult = DialogResult.Yes Then
                        DeleteShortCut = True
                        Continue For
                    End If
                End Try
                If IsFolder Then
                    Extension = "Folder"
                Else
                    Extension = Path.GetFileName(Fil(0))
                End If
                item = New ListViewItem(Path.GetFileName(Fil(0)), 1)
                If CType(tabPage.Controls(tabPage.Text), ListView).Items.Count > 0 Then
                    If Not IsNothing(CType(tabPage.Controls(tabPage.Text), ListView).FindItemWithText(item.Text, False, 0, False)) Then Continue For
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
                item.Name = Path.GetFileName(Fil(0))
                item.Text = Path.GetFileName(Fil(0))
                Try
                    lvi = CType(tabPage.Controls(tabPage.Text), ListView).Items.Add(item)
                    If Fil.Count > 1 Then
                        lvi.Text = Fil(1)
                    Else
                        lvi.Text = Path.GetFileName(Fil(0)) 'Fil(0)
                    End If
                    lvi.Tag = Fil(0)
                    lvi.ToolTipText = Path.GetFileName(Fil(0)) 'Extension
                Catch ex As Exception
                End Try
            Next
            CType(tabPage.Controls(tabPage.Text), ListView).ListViewItemSorter = New ListViewItemComparer(0)
            CType(tabPage.Controls(tabPage.Text), ListView).Sort()
            Load_Shortcuts_ToolTips(CType(tabPage.Controls(tabPage.Text), ListView))
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try

    End Function
    Private Sub CreatListView(Optional ByVal TbPg As TabPage = Nothing)
        Dim Files() = Directory.GetFiles(MagNoteFolderPath, "*_(Shortcuts_links).txt")
        If Not IsNothing(TbPg) Then
            CreatNewListView(TbPg)
        ElseIf Files.Count = 0 Then
            Me.ShortCut_TbCntrl.TabPages.Add("General", "General")
            CreatNewListView(Me.ShortCut_TbCntrl.TabPages("General"))
        Else
            For Each File In Files
                Dim TbPgName = Replace(Path.GetFileName(File), "_(Shortcuts_links).txt", "")
                Me.ShortCut_TbCntrl.TabPages.Add(TbPgName, TbPgName)
                CType(ShortCut_TbCntrl, TabControl).TabPages(TbPgName).AllowDrop = True
                CreatNewListView(Me.ShortCut_TbCntrl.TabPages(TbPgName))
            Next
            CreatMyDesktopTabPage()
        End If
    End Sub
    Private Sub CreatNewListView(Optional TbPg As TabPage = Nothing)
        Try
            Dim TmpShortcutsLstVw = New ListView()
            imageList1 = New ImageList()
            TmpShortcutsLstVw.Location = New Point(37, 12)
            TmpShortcutsLstVw.Size = New Size(161, 242)
            TmpShortcutsLstVw.LabelEdit = True
            TmpShortcutsLstVw.Sorting = SortOrder.Ascending
            TmpShortcutsLstVw.AutoArrange = True
            TmpShortcutsLstVw.Name = TbPg.Text
            CType(TbPg, TabPage).Controls.Add(TmpShortcutsLstVw)

            TmpShortcutsLstVw.AllowDrop = True
            TmpShortcutsLstVw.Dock = System.Windows.Forms.DockStyle.Fill
            AddHandler TmpShortcutsLstVw.DoubleClick, AddressOf Shortcuts_LstVw_DoubleClick
            AddHandler TmpShortcutsLstVw.Click, AddressOf Shortcuts_LstVw_Click
            AddHandler TmpShortcutsLstVw.DragDrop, AddressOf FileList_DragDrop
            AddHandler TmpShortcutsLstVw.DragEnter, AddressOf FileList_DragEnter
            AddHandler TmpShortcutsLstVw.ItemDrag, AddressOf Shortcuts_LstVw_ItemDrag
            AddHandler TmpShortcutsLstVw.DragOver, AddressOf Shortcuts_LstVw_DragOver
            AddHandler TmpShortcutsLstVw.KeyDown, AddressOf Shortcuts_LstVw_KeyDown
            AddHandler TmpShortcutsLstVw.MouseEnter, AddressOf Shortcuts_LstVw_MouseEnter
            AddHandler TmpShortcutsLstVw.MouseMove, AddressOf Shortcuts_LstVw_MouseMove
            AddHandler TmpShortcutsLstVw.MouseDown, AddressOf Shortcuts_LstVw_MouseDown
            AddHandler TmpShortcutsLstVw.AfterLabelEdit, AddressOf Shortcuts_LstVw_AfterLabelEdit


            TmpShortcutsLstVw.ContextMenuStrip = New ContextMenuStrip()
            Dim Shortcuts_LstVw_lstvw As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Shortcuts_LstVw_lstvw.Text = "Reload My Shortcuts"
            Else
                Shortcuts_LstVw_lstvw.Text = "إعادة تحميل إختصاراتى"
            End If
            Shortcuts_LstVw_lstvw.Text &= " (" & TbPg.Text & ")"
            Shortcuts_LstVw_lstvw.Tag = TmpShortcutsLstVw.Name
            Shortcuts_LstVw_lstvw.BackgroundImage = My.Resources.Background4
            Shortcuts_LstVw_lstvw.Image = My.Resources.DownloadsSZ
            Shortcuts_LstVw_lstvw.Name = "ReloadMyShortcuts" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Shortcuts_LstVw_lstvw)
            Shortcuts_LstVw_lstvw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Shortcuts_LstVw_lstvw.Click, AddressOf Shortcuts_LstVw_lstvw_Click

            '-------------------------------------------
            Dim Create_New_Shortcuts_TabPage As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Create_New_Shortcuts_TabPage.Text = "Create New Shortcut TabPage"
            Else
                Create_New_Shortcuts_TabPage.Text = "إنشاء صفحة تبويب اختصارات جديدة"
            End If
            Create_New_Shortcuts_TabPage.Tag = TmpShortcutsLstVw.Name
            Create_New_Shortcuts_TabPage.BackgroundImage = My.Resources.Background4
            Create_New_Shortcuts_TabPage.Image = My.Resources.paste
            Create_New_Shortcuts_TabPage.Name = "CreateNewShortcutTabPage" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Create_New_Shortcuts_TabPage)
            Create_New_Shortcuts_TabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Create_New_Shortcuts_TabPage.Click, AddressOf Create_New_Shortcuts_TabPage_Click
            '-------------------------------------------
            Dim Delete_Current_Shortcuts_TabPage As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Delete_Current_Shortcuts_TabPage.Text = "Create Current Shortcut TabPage"
            Else
                Delete_Current_Shortcuts_TabPage.Text = "إلغاء صفحة تبويب الاختصارات الحالية"
            End If
            Delete_Current_Shortcuts_TabPage.Text &= " (" & TbPg.Text & ")"
            Delete_Current_Shortcuts_TabPage.Tag = TmpShortcutsLstVw.Name
            Delete_Current_Shortcuts_TabPage.BackgroundImage = My.Resources.Background4
            Delete_Current_Shortcuts_TabPage.Image = My.Resources.paste
            Delete_Current_Shortcuts_TabPage.Name = "CreateCurrentShortcutTabPage" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Delete_Current_Shortcuts_TabPage)
            Delete_Current_Shortcuts_TabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Delete_Current_Shortcuts_TabPage.Click, AddressOf Delete_Current_Shortcuts_TabPage_Click
            '-------------------------------------------
            Dim Create_New_Shortcut As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Create_New_Shortcut.Text = "Create New Shortcut"
            Else
                Create_New_Shortcut.Text = "إنشاء إختصار"
            End If
            Create_New_Shortcut.Tag = TmpShortcutsLstVw.Name
            Create_New_Shortcut.BackgroundImage = My.Resources.Background4
            Create_New_Shortcut.Image = My.Resources.paste
            Create_New_Shortcut.Name = "CreateNewShortcut" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Create_New_Shortcut)
            Create_New_Shortcut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Create_New_Shortcut.Click, AddressOf Create_New_Shortcut_Click

            '-------------------------------------------
            Dim Create_Shortcut_From_Clipboard As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Create_Shortcut_From_Clipboard.Text = "Create New Shortcut From Clipboard"
            Else
                Create_Shortcut_From_Clipboard.Text = "إنشاء إختصار من الحافظة"
            End If
            Create_Shortcut_From_Clipboard.Tag = TmpShortcutsLstVw.Name
            Create_Shortcut_From_Clipboard.BackgroundImage = My.Resources.Background4
            Create_Shortcut_From_Clipboard.Image = My.Resources.paste
            Create_Shortcut_From_Clipboard.Name = "CreateNewShortcutFromClipboard" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Create_Shortcut_From_Clipboard)
            Create_Shortcut_From_Clipboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Create_Shortcut_From_Clipboard.Click, AddressOf Create_Shortcut_From_Clipboard_Click

            '-------------------------------------------
            Dim Change_Item_Picture As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Change_Item_Picture.Text = "Change Item Picture"
            Else
                Change_Item_Picture.Text = "تغيير صورة العنصر"
            End If
            Change_Item_Picture.Tag = TmpShortcutsLstVw.Name
            Change_Item_Picture.BackgroundImage = My.Resources.Background4
            Change_Item_Picture.Image = My.Resources.Edit
            Change_Item_Picture.Name = "ChangeItemPicture" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Change_Item_Picture)
            Change_Item_Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Change_Item_Picture.Click, AddressOf Change_Item_Picture_Click
            '-------------------------------------------
            Dim Shortcut_Properties As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Shortcut_Properties.Text = "Shortcut Properties"
            Else
                Shortcut_Properties.Text = "خصائص الإختصار"
            End If
            Shortcut_Properties.Tag = TmpShortcutsLstVw.Name
            Shortcut_Properties.BackgroundImage = My.Resources.Background4
            Shortcut_Properties.Image = My.Resources.PropertiesHS
            Shortcut_Properties.Name = "ShortcutProperties" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Shortcut_Properties)
            Shortcut_Properties.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Shortcut_Properties.Click, AddressOf Shortcut_Properties_Click
            '-------------------------------------------
            Dim Shortcuts_LstVw_Save As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Shortcuts_LstVw_Save.Text = "Save My Shortcuts"
            Else
                Shortcuts_LstVw_Save.Text = "حفظ إختصاراتى"
            End If
            Shortcuts_LstVw_Save.Text &= " (" & TbPg.Text & ")"
            Shortcuts_LstVw_Save.Tag = TmpShortcutsLstVw.Name
            Shortcuts_LstVw_Save.BackgroundImage = My.Resources.Background4
            Shortcuts_LstVw_Save.Image = My.Resources.save
            Shortcuts_LstVw_Save.Name = "SaveMyShortcuts" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Shortcuts_LstVw_Save)
            Shortcuts_LstVw_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Shortcuts_LstVw_Save.Click, AddressOf Shortcuts_LstVw_Save_Click
            '-------------------------------------------
            Dim Shortcuts_LstVw_SelectAll As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Shortcuts_LstVw_SelectAll.Text = "Select All"
            Else
                Shortcuts_LstVw_SelectAll.Text = "إختار الجميع"
            End If
            Shortcuts_LstVw_SelectAll.Tag = TmpShortcutsLstVw.Name
            Shortcuts_LstVw_SelectAll.BackgroundImage = My.Resources.Background4
            Shortcuts_LstVw_SelectAll.Image = My.Resources.SelectAll
            Shortcuts_LstVw_SelectAll.Name = "SelectAll" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Shortcuts_LstVw_SelectAll)
            Shortcuts_LstVw_SelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Shortcuts_LstVw_SelectAll.Click, AddressOf Shortcuts_LstVw_SelectAll_Click
            '-------------------------------------------
            Dim Shortcuts_LstVw_Edit_Label As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Shortcuts_LstVw_Edit_Label.Text = "Edit Label"
            Else
                Shortcuts_LstVw_Edit_Label.Text = "تعديل عنوان"
            End If
            Shortcuts_LstVw_Edit_Label.Tag = TmpShortcutsLstVw.Name
            Shortcuts_LstVw_Edit_Label.BackgroundImage = My.Resources.Background4
            Shortcuts_LstVw_Edit_Label.Image = My.Resources.Edit
            Shortcuts_LstVw_Edit_Label.Name = "EditLabel" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Shortcuts_LstVw_Edit_Label)
            Shortcuts_LstVw_Edit_Label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Shortcuts_LstVw_Edit_Label.Click, AddressOf Shortcuts_LstVw_Edit_Label_Click
            '-------------------------------------------
            'RunProgramAsAdministrator
            Dim Shortcuts_LstVw_RunAsAdministrator As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Shortcuts_LstVw_RunAsAdministrator.Text = "Run As Administrator"
            Else
                Shortcuts_LstVw_RunAsAdministrator.Text = "Run As Administrator"
            End If
            Shortcuts_LstVw_RunAsAdministrator.Tag = TmpShortcutsLstVw.Name
            Shortcuts_LstVw_RunAsAdministrator.BackgroundImage = My.Resources.Background4
            Shortcuts_LstVw_RunAsAdministrator.Image = My.Resources.MagdyFetrah1
            Shortcuts_LstVw_RunAsAdministrator.Name = "Shortcuts_LstVw_RunAsAdministrator" & TmpShortcutsLstVw.Name
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Shortcuts_LstVw_RunAsAdministrator)
            Shortcuts_LstVw_RunAsAdministrator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Shortcuts_LstVw_RunAsAdministrator.Click, AddressOf Shortcuts_LstVw_RunAsAdministrator_Click
            '-------------------------------------------
            Dim Delete_Shortcut As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Delete_Shortcut.Text = "Delete Shortcut(s)"
            Else
                Delete_Shortcut.Text = "إلغاء الإختصار(ات)"
            End If
            Delete_Shortcut.Name = "Delete_Shortcut(s)"
            Delete_Shortcut.Tag = MagNotes_Notes_TbCntrl.Name
            Delete_Shortcut.Image = My.Resources.Delete
            Delete_Shortcut.BackgroundImage = My.Resources.Background4
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Delete_Shortcut)
            Delete_Shortcut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Delete_Shortcut.Click, AddressOf Delete_Shortcut_Click
            '-------------------------------------------------------------
            Dim Show_Labeling_And_Tooltip_Form As New ToolStripMenuItem
            If Language_Btn.Text = "ع" Then
                Show_Labeling_And_Tooltip_Form.Text = "Show Labeling And Tooltip Form"
            Else
                Show_Labeling_And_Tooltip_Form.Text = "إظهار شاشة إعداد عناوين وشرح العناصر"
            End If
            Show_Labeling_And_Tooltip_Form.Name = "Show_Labeling_And_Tooltip_Form"
            Show_Labeling_And_Tooltip_Form.Tag = MagNotes_Notes_TbCntrl.Name
            Show_Labeling_And_Tooltip_Form.Image = My.Resources.TabControl7
            Show_Labeling_And_Tooltip_Form.BackgroundImage = My.Resources.Background4
            TmpShortcutsLstVw.ContextMenuStrip.Items.Add(Show_Labeling_And_Tooltip_Form)
            Show_Labeling_And_Tooltip_Form.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            AddHandler Show_Labeling_And_Tooltip_Form.Click, AddressOf Show_Labeling_And_Tooltip_Form_Click
            TmpShortcutsLstVw.Font = New Font(New FontFamily("Times New Roman"), 12, FontStyle.Regular)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_lstvw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            Dim x = Objct.text
            If Not IsNothing(Objct) Then
                If Objct.name = "My Desktop" Then
                    ShortCut_TbCntrl.TabPages.Remove(ShortCut_TbCntrl.TabPages(Objct.name))
                    CreatMyDesktopTabPage()
                Else
                    LoadList(Objct.name)
                End If
                ShortCut_TbCntrl.SelectedTab = ShortCut_TbCntrl.TabPages(Objct.name)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Public Sub Create_New_Shortcut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.Title = "Select File Shortcut"
        OpenFileDialog.Filter = "All files|*.*"
        OpenFileDialog.Multiselect = True
        OpenFileDialog.FileName = ""
        OpenFileDialog.RestoreDirectory = True
        If OpenFileDialog.ShowDialog <> DialogResult.Cancel Then
            Dim CurrentShortcutsLstVw
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            CurrentShortcutsLstVw = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView)

            For Each File In OpenFileDialog.FileNames
                Dim f() As String = {File}
                Dim d As New DataObject(DataFormats.FileDrop, f)
                Clipboard.SetDataObject(d, True)
                Create_Shortcut_From_Clipboard_Click(sender, e)
            Next
            CurrentShortcutsLstVw.Sort()
            SaveList(CurrentShortcutsLstVw.Name)
            LoadList(CurrentShortcutsLstVw.Name)
        End If

    End Sub
    Public Sub Create_Shortcut_From_Clipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim CurrentShortcutsLstVw
            If sender.name = Shortcut_Propertis_Form.Name Then
                CurrentShortcutsLstVw = Shortcuts_LstVw
                GoTo CommingFromShortcut_Propertis_Form
            End If
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            CurrentShortcutsLstVw = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView)
            Dim lvi As ListViewItem
            Dim item As ListViewItem
            Dim Extension
            Dim iconForFile As Icon
            Dim IsFolder As Boolean
            Dim files() As String = Clipboard.GetData(DataFormats.FileDrop)
            If Not IsNothing(files) Then
                Dim uriResult1 As Uri
                Dim result1 As Boolean = Uri.TryCreate(files(0), UriKind.Absolute, uriResult1) AndAlso (uriResult1.Scheme = Uri.UriSchemeFile OrElse uriResult1.Scheme = Uri.UriSchemeFile)
                Dim ShortCutName1 = Path.GetFileNameWithoutExtension(files(0))
                If result1 Then
                    If Not CreateShortCut(files(0), MagNoteFolderPath & "\Shortcuts\", ShortCutName1) Then
                        Exit Sub
                    End If
                    files(0) = MagNoteFolderPath & "\Shortcuts\" & ShortCutName1 & ".lnk"
                End If
            End If
CommingFromShortcut_Propertis_Form:
            If IsNothing(files) Then
                Dim uriName = Clipboard.GetDataObject.GetData(System.Windows.Forms.DataFormats.StringFormat, True)
                Dim uriResult As Uri
                Dim result As Boolean = Uri.TryCreate(uriName, UriKind.Absolute, uriResult) AndAlso (uriResult.Scheme = Uri.UriSchemeHttp OrElse uriResult.Scheme = Uri.UriSchemeHttps)
                Dim ShortCutName = uriResult.Authority ' Generate_New_Shortcut_Name()
                If result Then
                    If Not CreateShortCut(uriName, MagNoteFolderPath & "\Shortcuts\", ShortCutName) Then
                        Exit Sub
                    End If
                    ReDim files(1)
                    files(0) = MagNoteFolderPath & "\Shortcuts\" & ShortCutName & ".lnk"
                ElseIf Not String.IsNullOrEmpty(uriName) Then
                    If File.Exists(uriName) Or
                        Directory.Exists(uriName) Then
                        If Not CreateShortCut(uriName, MagNoteFolderPath & "\Shortcuts\", Path.GetFileName(uriName)) Then
                            Exit Sub
                        End If
                        ReDim files(1)
                        files(0) = MagNoteFolderPath & "\Shortcuts\" & Path.GetFileName(uriName) & ".lnk"
                    End If
                End If
                If IsNothing(files) Then Exit Sub

            End If
            For Each File In files
                If IsNothing(File) Then Continue For
                IsFolder = (System.IO.File.GetAttributes(File) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                If IsFolder Then
                    Extension = "Folder"
                Else
                    Extension = Path.GetExtension(Path.GetFileName(File))
                    Extension = Path.GetFileName(File)
                End If
                iconForFile = SystemIcons.WinLogo
                item = New ListViewItem(Path.GetFileName(File), 1)
                If CurrentShortcutsLstVw.Items.Count > 0 Then
                    If Not IsNothing(CurrentShortcutsLstVw.FindItemWithText(item.Text, False, 0, False)) Then Continue For
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
                lvi = CurrentShortcutsLstVw.Items.Add(item)
                lvi.Tag = File
            Next

            Application.DoEvents()
            If Not (sender.name.contains("CreateNewShortcut")) Then
                CurrentShortcutsLstVw.Sort()
                SaveList(CurrentShortcutsLstVw.Name)
                LoadList(CurrentShortcutsLstVw.Name)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub Create_New_Shortcuts_TabPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            Dim CurrentShortcutsLstVw = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView)
            If Language_Btn.Text = "E" Then
                Msg = "أدخل اسم علامة تبويب الاختصار الجديدة"
            Else
                Msg = "Enter New Shortcut TabPage Name"
            End If
            Dim TbPgName As String = InputBox(Msg, Application.ProductName)
            If TbPgName.Length > 0 Then
                Me.ShortCut_TbCntrl.TabPages.Add(TbPgName, TbPgName)
                Me.ShortCut_TbCntrl.SelectedTab = Me.ShortCut_TbCntrl.TabPages(TbPgName)
                CreatListView(Me.ShortCut_TbCntrl.SelectedTab)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub Delete_Current_Shortcuts_TabPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            Dim CurrentShortcutsLstVw = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView)
            If Language_Btn.Text = "E" Then
                Msg = "سيتم إلغاء علامة تبويب الاختصار الحالية... هل انت موافق؟"
            Else
                Msg = "Current Shortcut TabPage Will Be Deleted... Are You Agree?"
            End If
            Dim FilePath = MagNoteFolderPath & "\" & Objct.name & "_(Shortcuts_links).txt"
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                Me.ShortCut_TbCntrl.TabPages.Remove(Me.ShortCut_TbCntrl.TabPages(Objct.name))
                If File.Exists(FilePath) Then
                    My.Computer.FileSystem.DeleteFile(FilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                SaveList(Objct.name)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Change_Item_Picture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            Dim CurrentShortcutsLstVw = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView)
            If Not IsNothing(Objct) Then

                Dim OpenFileDialog As New OpenFileDialog
                OpenFileDialog.Title = "Select Suitable Image/Icon"
                OpenFileDialog.Filter = "Portable Network Graphics|*.png|JPEG Joint Photographic Expert Group image|*.jpg;*.jpeg;*.jfif;*.pjpeg;*.pjp|All files|*.*"
                OpenFileDialog.Multiselect = False
                OpenFileDialog.FileName = ""
                OpenFileDialog.RestoreDirectory = True
                If OpenFileDialog.ShowDialog <> DialogResult.Cancel Then
                    Dim ItemName = CurrentShortcutsLstVw.Items(CurrentShortcutsLstVw.Items.IndexOf(CurrentShortcutsLstVw.FocusedItem)).ImageKey
                    Dim PictureFullPath = MagNoteFolderPath & "\Shortcuts\Images\" & ItemName & Path.GetExtension(OpenFileDialog.FileName)
                    File.Copy(OpenFileDialog.FileName, PictureFullPath, 1)
                    imageList1.Images.Add(ItemName, Image.FromFile(PictureFullPath))
                    imageList1.Images(imageList1.Images.Count - 1).Tag = OpenFileDialog.FileName
                    LoadList(Objct.name)
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Shortcut_Properties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            Dim CurrentLstvw = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView)
            Shortcut_Propertis_Form.Shortcut_Image_Key_TxtBx.Text = CurrentLstvw.Items(CurrentLstvw.Items.IndexOf(CurrentLstvw.FocusedItem)).ImageKey
            Shortcut_Propertis_Form.Shortcut_Text_TxtBx.Text = CurrentLstvw.Items(CurrentLstvw.Items.IndexOf(CurrentLstvw.FocusedItem)).Text
            Shortcut_Propertis_Form.Available_Shortcuts_CmbBx.Text = CurrentLstvw.Items(CurrentLstvw.Items.IndexOf(CurrentLstvw.FocusedItem)).Name.ToString
            Shortcut_Propertis_Form.Shortcut_Path_TxtBx.Text = CurrentLstvw.Items(CurrentLstvw.Items.IndexOf(CurrentLstvw.FocusedItem)).Tag
            Shortcut_Propertis_Form.Shortcut_Tool_Tip_TxtBx.Text = CurrentLstvw.Items(CurrentLstvw.Items.IndexOf(CurrentLstvw.FocusedItem)).ToolTipText
            Shortcut_Propertis_Form.Show()
            Dim PicturePath = MagNoteFolderPath & "\Shortcuts\Images\" & CurrentLstvw.FocusedItem.ImageKey & ".png"
            If File.Exists(PicturePath) Then
                Shortcut_Propertis_Form.Shortcut_Picture_Path_TxtBx.Text = PicturePath
            Else
                Shortcut_Propertis_Form.Shortcut_Picture_Path_TxtBx.Text = Shortcut_Propertis_Form.Shortcut_Path_TxtBx.Text
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_Edit_Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "ع" Then
                    sender.Text = "Change Label"
                Else
                    sender.Text = "تعديل عنوان"
                End If
                Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Unchecked
            Else
                If Language_Btn.Text = "ع" Then
                    sender.Text = "One Click"
                Else
                    sender.Text = "نقرة واحدة"
                End If
                Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Checked
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Delete_Shortcut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).SelectedItems.Count = 0 Then Exit Sub
            If Language_Btn.Text = "E" Then
                Msg = "سيتم إلغاء الإختصار(ات)... هل انت متأكد"
                Msg &= vbNewLine & "عدد الاختصارات التى سيتم إلغائها (" & Objct.SelectedItems.Count & ")"
            Else
                Msg = "This File(s) Will Be Deleyed... Are You Sure?"
                Msg = vbNewLine & "Number Of Shorcuts Will Be Deleted Are (" & Objct.SelectedItems.Count & ")"
            End If
            If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                Exit Sub
            End If
            For Each Shortcut As ListViewItem In CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).SelectedItems
                CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).Items.Remove(Shortcut)
                If Not IsNothing(IsLink(Shortcut.Tag)) Then
                    If File.Exists(Shortcut.Tag) Then
                        File.Delete(Shortcut.Tag)
                    End If
                End If
            Next
            SaveList(ShortCut_TbCntrl.Controls(Objct.name).name)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            If Language_Btn.Text = "E" Then
                Msg = "سيتم إلغاء الإختصار(ات)... هل انت متأكد"
            Else
                Msg = "This File(s) Will Be Deleyed... Are You Sure?"
            End If
            If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                Exit Sub
            End If
            For Each i As ListViewItem In sender.SelectedItems
                sender.Items.Remove(i)
            Next
            SaveList(sender.parent.name)
        ElseIf e.KeyCode = Keys.Enter Then
            If Not IsNothing(sender.SelectedItems(0).Tag) Then
                Process.Start(sender.SelectedItems(0).Tag)
                Me.WindowState = FormWindowState.Minimized
            End If
        End If
    End Sub

    Private Sub Shortcuts_LstVw_RunAsAdministrator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl

            Cursor = Cursors.WaitCursor
            Dim process As System.Diagnostics.Process = Nothing
            Dim processStartInfo As System.Diagnostics.ProcessStartInfo
            processStartInfo = New System.Diagnostics.ProcessStartInfo()
            processStartInfo.FileName = CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).SelectedItems(0).Tag
            processStartInfo.Verb = "runas"
            processStartInfo.Arguments = ""
            processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
            processStartInfo.UseShellExecute = True
            process = System.Diagnostics.Process.Start(processStartInfo)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
            If Not IsNothing(Objct) Then
                For i = 0 To CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).Items.Count - 1
                    CType(ShortCut_TbCntrl.Controls(Objct.name).Controls(Objct.name), ListView).Items(i).Selected = True
                Next i
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            MouseDounRight = True
        Else
            MouseDounRight = False
        End If
    End Sub
    Private Sub Shortcuts_LstVw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim LinkName = sender.SelectedItems(0).Tag
            Cursor = Cursors.WaitCursor
            If MouseDounRight Then Exit Sub
            If Double_Click_To_Run_Shortcut_ChkBx.CheckState = CheckState.Checked Then Exit Sub
            If Not IsNothing(sender.SelectedItems(0).Tag) Then
                Process.Start(sender.SelectedItems(0).Tag)
                MyFormWindowState(0, 0, Nothing, 1)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Shortcuts_LstVw_DoubleClick(sender As Object, e As EventArgs)
        Try
            If Not IsNothing(sender.SelectedItems(0).Tag) Then
                Process.Start(sender.SelectedItems(0).Tag)
                MyFormWindowState(0, 0, Nothing, 1)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Shl As Shell
    Private Const ssfBITBUCKET As Long = 10
    Private Const recycleNAME As Integer = 0
    Private Const recyclePATH As Integer = 1
    Private Function Restore(ByVal Item As String) As Boolean
        Shl = New Shell()
        Dim Recycler As Folder = Shl.[NameSpace](10)
        For i As Integer = 0 To Recycler.Items().Count - 1
            Dim FI As FolderItem = Recycler.Items().Item(i)
            Dim FileName As String = Recycler.GetDetailsOf(FI, 0)
            If Path.GetExtension(FileName) = "" Then FileName += Path.GetExtension(FI.Path)
            Dim FilePath As String = Recycler.GetDetailsOf(FI, 1)
            If Item = Path.Combine(FilePath, FileName) Then
                DoVerb(FI, "ESTORE")
                Return True
            End If
        Next
        Return False
    End Function

    Private Function DoVerb(ByVal Item As FolderItem, ByVal Verb As String) As Boolean
        For Each FIVerb As FolderItemVerb In Item.Verbs()
            If FIVerb.Name.ToUpper().Contains(Verb.ToUpper()) Then
                FIVerb.DoIt()
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub SaveList(Optional ByVal TbPgName As String = Nothing, Optional ByVal DisplayMsg As Boolean = True)
        Dim FilePath
        For Each TbPg In ShortCut_TbCntrl.TabPages
            If Not String.IsNullOrEmpty(TbPgName) Then
                If TbPg.Text <> TbPgName Then
                    Continue For
                End If
            End If
            If TbPg.name = "My Desktop" Then
                Continue For
            End If
            FilePath = MagNoteFolderPath & "\" & TbPg.Text & "_(Shortcuts_links).txt"
            Dim tx() As Control = TbPg.Controls.Find(TbPg.text, 1)
            If tx.Length = 0 Then Continue For
            Try
                Cursor = Cursors.WaitCursor
                Dim TextToWrite As String = Nothing
                If File.Exists(FilePath) Then
                    My.Computer.FileSystem.DeleteFile(FilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
                TbPg.controls(TbPg.text).Refresh()
                For Each file In TbPg.controls(TbPg.text).Items
                    TextToWrite &= file.tag & "$" & file.text & ","
                Next
                If String.IsNullOrEmpty(TextToWrite) Then Exit Sub
                My.Computer.FileSystem.WriteAllText(FilePath, Microsoft.VisualBasic.Left(TextToWrite, TextToWrite.Length - 1), 0, System.Text.Encoding.UTF8)
                If DisplayMsg Then
                    If Language_Btn.Text = "E" Then
                        Msg = "تم حفظ علام(ات/ة) تبويب الاختصار بنجاح"
                    Else
                        Msg = "Shortcut TabPage(s) Have Been Saved Successfully"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                End If
            Catch ex As Exception
                ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                If Not Restore(FilePath) Then
                    If Language_Btn.Text = "E" Then
                        Msg = "لم نتمكن من استرجاع ملف الاختصارات من سلة المحذوفات"
                    Else
                        Msg = "Couldn't Restore Shortcuts File From Recycle Bin"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                End If
            Finally
                Cursor = Cursors.Default
            End Try
        Next
    End Sub
    Public Sub LoadList(ByVal FilePath As String, Optional AnotherForm As Form = Nothing)
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Dim CntrlName = FilePath
            If String.IsNullOrEmpty(FilePath) Then
                Dim Files() = Directory.GetFiles(MagNoteFolderPath, "*_(Shortcuts_links).txt")
                If Files.Length > 0 Then
                    Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, ShortCut_TbCntrl.TabPages.Count, AnotherForm)
                    PreviewPnl.BringToFront()
                    For Each TbPg In ShortCut_TbCntrl.TabPages
                        progress += ProgressToAdd
                        Previewlbl.Text = "Loading--> " & TbPg.name & vbNewLine & Math.Floor(progress * 100)
                        Previewlbl.Refresh()
                        Previewlbl.Invalidate()
                        TbPg.controls(TbPg.text).Refresh()
                        FilePath = MagNoteFolderPath & "\" & TbPg.text & "_(Shortcuts_links).txt"
                        LoadShortcutListFiles(CType(TbPg.controls(TbPg.text), ListView), FilePath, AnotherForm)
                    Next
                Else
                    FilePath = MagNoteFolderPath & "\General_(Shortcuts_links).txt"
                    LoadShortcutListFiles(CType(ShortCut_TbCntrl.Controls("General").Controls("General"), ListView), FilePath, AnotherForm)
                End If
            Else
                FilePath = MagNoteFolderPath & "\" & CntrlName & "_(Shortcuts_links).txt"
                LoadShortcutListFiles(CType(ShortCut_TbCntrl.Controls(CntrlName).Controls(CntrlName), ListView), FilePath, AnotherForm)
            End If
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Sub
    Public Function LoadShortcutListFiles(ByVal ShortcutsLstVw As ListView, FilePath As String, Optional AnotherForm As Form = Nothing) As Boolean
        Dim DeleteShortCut As Boolean
        DialogResult = DialogResult.None
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Cursor = Cursors.WaitCursor
            If Not File.Exists(FilePath) Then Exit Function
            ShortcutsLstVw.LargeImageList = imageList1
            ShortcutsLstVw.SmallImageList = imageList1
            ShortcutsLstVw.View = View.LargeIcon
            imageList1.ImageSize = New Size(48, 48)
            Dim MyIconSize = IconSize.ExtraLarge
            ShortcutsLstVw.AllowColumnReorder = True
            ShortcutsLstVw.Sorting = SortOrder.Ascending
            ShortcutsLstVw.ShowItemToolTips = True
            imageList1.ColorDepth = ColorDepth.Depth32Bit
            Dim lvi As ListViewItem
            Dim item As ListViewItem
            Dim Extension
            Dim IsFolder As Boolean
            ShortcutsLstVw.Items.Clear()
            Dim FilesCount = Directory.GetFiles(MagNoteFolderPath & "\Shortcuts\Images").Length
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, FilesCount, AnotherForm)
            For Each File In Directory.GetFiles(MagNoteFolderPath & "\Shortcuts\Images")
                progress += ProgressToAdd
                Previewlbl.Text = "Loading--> " & File & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                Dim LinkName = Path.GetFileNameWithoutExtension(File)
                If imageList1.Images.IndexOfKey(LinkName) = -1 Then
                    Try
                        imageList1.Images.Add(LinkName, Image.FromFile(File))
                    Catch ex As Exception
                        If ex.Message <> "Out of memory." Then
                            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        End If
                    End Try
                End If
            Next
            Dim FilePaths() = Split(My.Computer.FileSystem.ReadAllText(FilePath, System.Text.Encoding.UTF8), ",")
            ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, FilePaths.Length, AnotherForm)
            For Each File In FilePaths
                progress += ProgressToAdd
                Previewlbl.Text = "Loading--> " & File & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                Dim Fil() = Split(File, "$")
                Try
                    IsFolder = (System.IO.File.GetAttributes(Fil(0)) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                Catch ex As Exception
                    IsFolder = False
                    If DialogResult = DialogResult.Cancel Then Continue For
                    If Language_Btn.Text = "ع" Then
                        Msg = "You Can Reload All The Shortcuts Again By Right Clicking On The List"
                    Else
                        Msg = "يمكنك إعادة تحميل جميع الاختصارات مرة أخرى عن طريق النقر فوق القائمة بزر الماوس الأيمن... هل تريد إلغاء الاختصار؟"
                    End If
                    DialogResult = ShowMsg(Fil(1) & vbNewLine & vbNewLine & ex.Message & Msg & vbNewLine & Fil(0), "InfoSysMe (MagNote)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    If DialogResult = DialogResult.Cancel Then
                        Continue For
                    ElseIf DialogResult = DialogResult.Yes Then
                        DeleteShortCut = True
                        Continue For
                    End If
                End Try
                If IsFolder Then
                    Extension = "Folder"
                Else
                    Extension = Path.GetFileName(Fil(0))
                End If
                item = New ListViewItem(Path.GetFileName(Fil(0)), 1)
                If ShortcutsLstVw.Items.Count > 0 Then
                    If Not IsNothing(ShortcutsLstVw.FindItemWithText(item.Text, False, 0, False)) Then Continue For
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
                item.Name = Path.GetFileName(Fil(0))

                lvi = ShortcutsLstVw.Items.Add(item)
                lvi.Text = Fil(1)
                lvi.Tag = Fil(0)
                lvi.ToolTipText = Extension
            Next
            ShortcutsLstVw.ListViewItemSorter = New ListViewItemComparer(0)
            ShortcutsLstVw.Sort()
            Load_Shortcuts_ToolTips(ShortcutsLstVw)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            If DeleteShortCut Then
                SaveList(Replace(Path.GetFileName(FilePath), "_(Shortcuts_links).txt", ""))
            End If
            Cursor = Cursors.Default
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Function
    Private Sub Shortcuts_LstVw_KeyPressed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Shortcuts_LstVw_MouseMove(sender As Object, e As MouseEventArgs)
        Dim currentItem As ListViewItem = sender.GetItemAt(e.X, e.Y)
        If currentItem IsNot Nothing Then
            currentItem.Focused = True
        End If
    End Sub

    Private Sub Shortcuts_LstVw_MouseEnter(sender As Object, e As EventArgs)
        imageList1.ColorDepth = ColorDepth.Depth32Bit
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name <> sender.name Then
            ActiveControl = sender
            sender.Focus()
        End If
    End Sub
    Dim MouseCharIndex As Integer
    Dim MagNoteName As String
    Private Sub RchTxtBx_MouseMove(sender As Object, e As MouseEventArgs)
        MouseCharIndex = RCSN.GetCharIndexFromPosition(e.Location)
        MagNoteName = Path.GetFileName(RCSN.Name)
        For Each Lnk In LinkMap
            If MagNoteName = Lnk.MagNoteName Then
                If MouseCharIndex >= Lnk.SelectionStart And
                    MouseCharIndex <= (Lnk.SelectionStart + Lnk.SelectionLength) Then
                    RCSN.Cursor = Cursors.Hand
                    Exit For
                Else
                    RCSN.Cursor = Cursors.IBeam
                End If
            End If
        Next
    End Sub

    Private Sub Set_Control_To_Fill_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Set_Control_To_Fill_ChkBx.CheckedChanged

    End Sub

    Private Sub Set_Control_To_Fill_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Set_Control_To_Fill_ChkBx.CheckStateChanged
        Try
            Exit Sub
            Cursor = Cursors.WaitCursor
            If Set_Control_To_Fill_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "ع" Then
                    Set_Control_To_Fill_ChkBx.Text = "Set Fill To Note"
                Else
                    Set_Control_To_Fill_ChkBx.Text = "إختيار التعبئة لملاحظة"
                End If
                Setting_TbCntrl.Dock = DockStyle.Bottom
                Setting_TbCntrl.SendToBack()
                Spliter_1_Lbl.SendToBack()
            ElseIf Set_Control_To_Fill_ChkBx.CheckState = CheckState.Unchecked Then
                If Language_Btn.Text = "ع" Then
                    Set_Control_To_Fill_ChkBx.Text = "Set Fill To Control Tabs"
                Else
                    Set_Control_To_Fill_ChkBx.Text = "إختيار التعبئة لصفحات التبويب"
                End If
                If Setting_TbCntrl.Visible Then
                    Setting_TbCntrl.Dock = DockStyle.Fill
                End If
                Setting_TbCntrl.BringToFront()
            End If
            MsgBox_SttsStrp.SendToBack()
            Note_TlStrp.SendToBack()
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
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
        RCSN.Rtf = sbTaRtf.ToString()
    End Sub

    Private Sub MagNote_Form_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
    End Sub

    Private Sub Font_Name_Note_TlStrpSpltBtn_ButtonClick(sender As Object, e As EventArgs) Handles Font_Name_Note_TlStrpSpltBtn.ButtonClick
        If Not ChechRchTxtBx() Then Exit Sub
        If RCSN.SelectionLength > 0 Then
            RCSN.SelectionFont = FontDlg.Font
        Else
            RCSN.Font = GetFontByString(Note_Font_TxtBx.Text)
        End If
    End Sub


    Private Sub Next_Reminder_Time_DtTmPkr_ValueChanged(sender As Object, e As EventArgs) Handles Next_Reminder_Time_DtTmPkr.ValueChanged
        If ActiveControl.Name = sender.name Then
            Note_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub
    Private Sub Reminder_Every_Days_NmrcUpDn_ValueChanged(sender As Object, e As EventArgs) Handles Reminder_Every_Minutes_NmrcUpDn.ValueChanged, Reminder_Every_Hours_NmrcUpDn.ValueChanged, Reminder_Every_Days_NmrcUpDn.ValueChanged
        If ActiveControl.Name = sender.name Then
            Note_Have_Reminder_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub Note_Have_Reminder_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Note_Have_Reminder_ChkBx.CheckedChanged

    End Sub
    Private Sub Stop_Reminder_Alert_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Pending_Reminder_Alert_ChkBx.CheckedChanged

    End Sub

    Private Sub Stop_Reminder_Alert_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Pending_Reminder_Alert_ChkBx.CheckStateChanged
        If Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Checked Then
            Save_Note_TlStrpBtn_Click(Save_Note_TlStrpBtn, EventArgs.Empty)
            Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
            Pending_Reminder_Alert_ChkBx.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub MagNote_Form_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles MyBase.HelpButtonClicked

    End Sub

    Private Sub Show_Form_Border_Style_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Form_Border_Style_ChkBx.CheckedChanged

    End Sub

    Private Sub Show_Form_Border_Style_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Show_Form_Border_Style_ChkBx.CheckStateChanged
        If Show_Form_Border_Style_ChkBx.CheckState = CheckState.Checked Then
            FormBorderStyle = FormBorderStyle.Sizable
            MagNote_PctrBx.Visible = False
            Hide_Me_PctrBx.Visible = False
        Else
            FormBorderStyle = FormBorderStyle.None
            MagNote_PctrBx.Visible = True
            Hide_Me_PctrBx.Visible = True
        End If
    End Sub

    Private Sub Enable_Maximize_Box_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Enable_Maximize_Box_ChkBx.CheckedChanged

    End Sub

    Private Sub Enable_Maximize_Box_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Enable_Maximize_Box_ChkBx.CheckStateChanged
        If Enable_Maximize_Box_ChkBx.CheckState = CheckState.Checked Then
            MaximizeBox = True
        Else
            MaximizeBox = True
        End If
    End Sub

    Private Sub Show_Note_Tab_Control_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Note_Tab_Control_ChkBx.CheckedChanged

    End Sub

    Private Sub Show_Note_Tab_Control_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Show_Note_Tab_Control_ChkBx.CheckStateChanged
        Exit Sub
        If Show_Note_Tab_Control_ChkBx.CheckState = CheckState.Unchecked Then
            Setting_TbCntrl.Visible = False
        Else
            Setting_TbCntrl.Visible = True
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

    Private Sub Note_Word_Wrap_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Note_Word_Wrap_ChkBx.CheckedChanged

    End Sub

    Private Sub Font_Strikeout_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Font_Strikeout_TlStrpBtn.Click
        If Not ChechRchTxtBx() Then Exit Sub
        Dim SelectionFont As Font = RCSN.SelectionFont
        Dim gdiCharSet As Byte
        Dim gdiVerticalFont As Boolean
        If RCSN.SelectionFont.Strikeout Then
            RCSN.SelectionFont = New Font(SelectionFont.Name, SelectionFont.Size, System.Drawing.FontStyle.Regular, SelectionFont.Unit, gdiCharSet, gdiVerticalFont)
        Else
            RCSN.SelectionFont = New Font(SelectionFont.Name, SelectionFont.Size, System.Drawing.FontStyle.Strikeout, SelectionFont.Unit, gdiCharSet, gdiVerticalFont)
        End If
    End Sub

    Dim CompressMeTlStrpBtnClicked As Boolean
    Private Sub Compress_Me_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Compress_Me_TlStrpBtn.Click
        Try
            CompressMeTlStrpBtnClicked = True
            Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked
            Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked
            ActiveControl = RCSN()
        Catch ex As Exception
        Finally
            CompressMeTlStrpBtnClicked = False
        End Try
    End Sub

    Private Sub Me_Is_Compressed_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Me_Is_Compressed_ChkBx.CheckedChanged

    End Sub
    Public Function FormOnScreen(ByVal frm As Form) As Boolean
        Try
            If Not frm.IsHandleCreated Then
                Throw New InvalidOperationException()
            End If
            If Not frm.Visible OrElse frm.WindowState = FormWindowState.Minimized Then Return False
            Return PointVisible(New Point(frm.Left, frm.Top)) AndAlso PointVisible(New Point(frm.Right, frm.Top)) AndAlso PointVisible(New Point(frm.Right, frm.Bottom)) AndAlso PointVisible(New Point(frm.Left, frm.Bottom))
        Catch ex As Exception

        End Try
    End Function

    Private Shared Function PointVisible(ByVal p As Point) As Boolean
        Dim scr = Screen.FromPoint(p)
        Return scr.Bounds.Contains(p)
    End Function
    Private Function CheckFormLocation(ByVal MyLocation) As Boolean
        If Me.WindowState = WindowState.Minimized Or
            Application_Starts_Minimized_ChkBx.CheckState <> CheckState.Checked Then Return False

        Dim NotInLocation = DialogResult.No
        Dim MeIsInsideOfSreen As Boolean
        MeIsInsideOfSreen = FormOnScreen(Me)
        If Not MeIsInsideOfSreen Then
            NotInLocation = DialogResult.Yes
            If Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.CheckState = CheckState.Checked Then
                DialogResult = ShowMsg("Location = " & Me.Location.ToString & vbNewLine & "سيتم تغيير موضع عرض الشاشة حيث ان البيانات المتاحة ستظهر الشاشة خارج نطاق النوافذ... هل انت موافق على ذلك" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Else
                DialogResult = DialogResult.No
            End If
        End If
        If NotInLocation Then
            If DialogResult = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Sub Me_Is_Compressed_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Me_Is_Compressed_ChkBx.CheckStateChanged
        Try
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Unchecked Then
                If Language_Btn.Text = "E" Then
                    Me_Is_Compressed_ChkBx.Text = "شاشتى طبيعية"
                Else
                    Me_Is_Compressed_ChkBx.Text = "Normal Window"
                End If
            ElseIf Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "E" Then
                    Me_Is_Compressed_ChkBx.Text = "شاشتى مضغوطة"
                Else
                    Me_Is_Compressed_ChkBx.Text = "Compressed Window"
                End If
                If ActiveControl.Name = sender.name Then
                    MeIsComlressed()
                End If
            ElseIf Me_Is_Compressed_ChkBx.CheckState = CheckState.Indeterminate Then
                If Language_Btn.Text = "E" Then
                    Me_Is_Compressed_ChkBx.Text = "شاشتى كبيرة"
                Else
                    Me_Is_Compressed_ChkBx.Text = "Maximized Window"
                End If
            End If
        Catch ex As Exception
        Finally
            Cursor = Cursors.WaitCursor
        End Try
    End Sub
    Private Function MeIsComlressed(Optional ByVal MeIsNormal As Boolean = False)
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
                Exit Function
            End If
            If Show_Form_Border_Style_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Function
            End If
            Cursor = Cursors.WaitCursor
            If Note_Form_Size_TxtBx.TextLength = 0 Then
                Exit Function
            End If
            Dim FormHeight = Val(Replace(Microsoft.VisualBasic.Right(Split(Note_Form_Size_TxtBx.Text, ",").ToList.Item(1), Note_Form_Size_TxtBx.TextLength - 1), "Height=", ""))
            Dim borderSize = Me.Height - Me.ClientSize.Height
            If Me_Is_Compressed_ChkBx.CheckState = CheckState.Checked Then
                If Me.Height = borderSize Then
                    If Not MeIsNormal Then
                        Me.WindowState = FormWindowState.Minimized
                    Else
                        If Me.Height <= FormHeight Then
                            For MyHeight = Me.Height To FormHeight Step 10
                                Me.Height = MyHeight
                                Application.DoEvents()
                            Next
                            Me.Height = FormHeight
                        End If
                    End If
                ElseIf Me.Height > borderSize And
                Not MeIsNormal Then
                    If Me.WindowState = FormWindowState.Maximized Then
                        Me.WindowState = FormWindowState.Normal
                    Else
                        For MyHeight = Me.Height To borderSize Step -10
                            Me.Height = MyHeight
                            If MyHeight <= borderSize Then
                                Exit For
                            End If
                            Application.DoEvents()
                        Next
                        Me.Height = borderSize
                    End If
                ElseIf MeIsNormal Then
                    Me.WindowState = FormWindowState.Maximized
                End If
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Sub MagNote_Form_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Try
            RCSN(0).Focus()
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

    Public Function ParseCommandLineArgs(Optional ByVal NewArg As String = Nothing) As Boolean
        Try
            Cursor = Cursors.WaitCursor
            If Not String.IsNullOrEmpty(NewArg) Then
                ExternalFilePath = Path.GetDirectoryName(NewArg)
                ExternalFileName = Path.GetFileName(NewArg)
                ActiveControl = MagNote_No_CmbBx
                Dim Index = IsInMagNoteCmbBx(NewArg, 0)
                If Index Then GoTo UseCurrentFile
                If AddToMagNoteNoCmbBx(NewArg) Then
                    Return True
                End If
                AddToMagNoteNoCmbBx(NewArg)
UseCurrentFile:
                Application.DoEvents()
                MagNote_No_CmbBx.SelectedIndex = -1
                MagNote_No_CmbBx.Text = ExternalFileName
                Correct_MagNote_TxtBx_Font_Color()
                If RunAsExternal(UseArgFile) Then
                    RememberOpenedExternalFiles()
                End If
                Return Index
            End If
        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try
    End Function
    Private Function OpenStoredNoteInstead(ByVal Index As Integer, Optional ByVal ExternalFilePath As String = Nothing) As Boolean
        If Not RunAsExternal() Then Return True
        If String.IsNullOrEmpty(ExternalFilePath) Then
            ExternalFilePath = DirectCast(MagNote_No_CmbBx.Items(Index), KeyValuePair(Of String, String)).Key
        End If
        If Language_Btn.Text = "ع" Then
            Msg = "This File Already Exist In MagNote Data But In Different Path... Do You Want To Use It?"
        Else
            Msg = "هذا الملف موجود بالفعل في بيانات الملاحظات الملاحظة ولكن على مسار مختلف ... هل تريد استخدامه؟"
        End If
        Msg &= vbNewLine & "ExternalFilePath = (" & ExternalFilePath & ")"
        If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
            Return True
        Else
            Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
        End If
    End Function
    Private Sub RememberOpenedExternalFiles()
        If Remember_Opened_External_Files_ChkBx.CheckState = CheckState.Checked And
            MagNotes_Folder_Path_TxtBx.Text <> MagNoteFolderPath Then
            Dim FileNameToSave = DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            Dim OpenedExternalFiles = MagNotes_Folder_Path_TxtBx.Text & "\OpenedExternalFiles.txt"
            Dim WriteContents As String = String.Empty
            If File.Exists(OpenedExternalFiles) Then
                WriteContents = My.Computer.FileSystem.ReadAllText(OpenedExternalFiles, System.Text.Encoding.UTF8)
            End If
            If Not WriteContents.Contains(FileNameToSave) Then
                WriteContents &= vbNewLine & FileNameToSave
                My.Computer.FileSystem.WriteAllText(OpenedExternalFiles, WriteContents, 0, System.Text.Encoding.UTF8)
            End If
        End If
    End Sub
    Private Function AddToMagNoteNoCmbBx(ByVal ExtenalPath As String, Optional ByVal SelectNote As Boolean = True) As Boolean
        Dim OSINT_ChkBx As CheckState = Open_Note_In_New_Tab_ChkBx.CheckState
        Try
            Dim FileLable As String = GetFileLabel(ExtenalPath)
            Dim FileExistInCmbBx = IsInMagNoteCmbBx(ExtenalPath,,, 1)
            Dim FileInTeSamePath As Boolean = False
            If Path.GetDirectoryName(ExtenalPath) = Path.GetDirectoryName(FileExistInCmbBx) Then
                FileInTeSamePath = True
            End If
            If Not FileInTeSamePath Then
                If Not IsNothing(FileExistInCmbBx) Then
                    If OpenStoredNoteInstead(Nothing, FileExistInCmbBx) Then
                        ActiveControl = MagNote_No_CmbBx
                        IsInMagNoteCmbBx(ExtenalPath, SelectNote, MagNote_No_CmbBx)
                        Return True
                    Else
                        MagNote_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(ExtenalPath, FileLable))
                        SelectNote = True
                    End If
                Else
                    MagNote_No_CmbBx.Items.Add(New KeyValuePair(Of String, String)(ExtenalPath, FileLable))
                End If
            End If
            Application.DoEvents()
            IsInMagNoteCmbBx(ExtenalPath, SelectNote)
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Open_Note_In_New_Tab_ChkBx.CheckState = OSINT_ChkBx
        End Try
    End Function
    Private Function GetFileLabel(ByVal ExtenalPath) As String
        If File.Exists(ExtenalPath) Then
            Dim FilesContents() = Split(My.Computer.FileSystem.ReadAllText(ExtenalPath, System.Text.Encoding.UTF8), ":")
            For Each line In FilesContents
                If Microsoft.VisualBasic.Left(line, Len("MagNote_Label -(")) & Microsoft.VisualBasic.Right(line, 2) = "MagNote_Label -()-" Then
                    Dim MagNote_Label = Replace(line, Microsoft.VisualBasic.Left(line, Len("MagNote_Label -(")), "")
                    MagNote_Label = Microsoft.VisualBasic.Left(MagNote_Label, MagNote_Label.Length - 2)
                    Return MagNote_Label
                End If
            Next
            Return Path.GetFileName(ExtenalPath)
        Else
            Return (Path.GetFileNameWithoutExtension(ExtenalPath))
        End If
    End Function
    Private Sub Me_As_Default_Text_File_Editor_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Me_As_Default_Text_File_Editor_ChkBx.CheckedChanged

    End Sub

    Private Sub Me_As_Default_Text_File_Editor_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Me_As_Default_Text_File_Editor_ChkBx.CheckStateChanged
        Cursor = Cursors.WaitCursor
        Dim IgnoreChangeMeAsAdministrator As Boolean
        Try
            If Run_Me_As_Administrator_ChkBx.CheckState = CheckState.Checked Then GoTo MeAsAdministrator
            If ActiveControl.Name = Me_As_Default_Text_File_Editor_ChkBx.Name Then
                If Language_Btn.Text = "E" Then
                    Msg = "لتطيق هذا الإختيار سيتم اعادة تشغيل البرنامج... هل تريد تنفيذ التطبيق الآن؟"
                Else
                    Msg = "To Run This Setting The Program Must Be Restart... Do You Want To Run This Setting Now?"
                End If
                If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                    Run_Me_As_Administrator_ChkBx.CheckState = CheckState.Checked
                    Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
                    IgnoreChangeMeAsAdministrator = True
                    Exit Sub
                End If
MeAsAdministrator:
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
                        If ex.Message.Contains("Access to the registry key") And ex.Message.Contains(" is denied.") Or
                            ex.Message.Contains("Requested registry access is not allowed") Then
                            RestartWithElevatedPrivileges(1)
                        Else
                            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        End If
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
                        If ex.Message.Contains("Access to the registry key") And ex.Message.Contains(" is denied.") Or
                            ex.Message.Contains("Requested registry access is not allowed") Then
                            RestartWithElevatedPrivileges(1)
                        Else
                            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        End If
                    End Try
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Access to the registry key") And ex.Message.Contains(" is denied.") Or
                ex.Message.Contains("Requested registry access is not allowed") Then
                RestartWithElevatedPrivileges(1)
            Else
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Finally
            If Not IgnoreChangeMeAsAdministrator Then
                Run_Me_As_Administrator_ChkBx.CheckState = CheckState.Unchecked
            End If
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub MagNote_Category_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MagNote_Category_CmbBx.SelectedIndexChanged
        If ActiveControl.Name = sender.name And
           Reload_MagNote_After_Change_Category_ChkBx.CheckState = CheckState.Checked Then
            If Language_Btn.Text = "ع" Then
                Msg = "Do You Want To Load All Notes Regarding To This Category Only?"
            Else
                Msg = "هل تريد تحميل الملاحظات المرتبطة بهذه الفئة فقط؟"
            End If
            Msg &= vbNewLine & "(" & MagNote_Category_CmbBx.Text & ")"
            If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.No Then
                Exit Sub
            End If
            ActiveControl = Preview_Btn
            Preview_Btn.Focus()
            Preview_Btn_Click(Preview_Btn, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' GNCNFC = GetNewCategoryNoFileCount
    ''' </summary>
    ''' <returns></returns>
    Private Function GetNewCategoryNo() As Integer
        SaveDialogResultAnseredIsNo = False
        Dim GNCNFC = 0
        Dim Files() As String = System.IO.Directory.GetFiles(MagNoteFolderPath)
        For Each file In Files
            Dim F = Path.GetFileNameWithoutExtension(file)
            Dim filename = Microsoft.VisualBasic.Left(F, Len("Category -(")) & Microsoft.VisualBasic.Right(Path.GetFileName(F), 2)
            If Path.GetExtension(file) = ".txt" And
                    filename = "Category -()-" Then
                GNCNFC += 1
            End If
        Next
        Dim CurrentFile = MagNoteFolderPath & "\Category -(" & GNCNFC & ")-.txt"
        If File.Exists(CurrentFile) Then
            GNCNFC = 0
        End If
        While File.Exists(CurrentFile)
            GNCNFC += 1
            CurrentFile = MagNoteFolderPath & "\Category -(" & GNCNFC & ")-.txt"
        End While
        Return GNCNFC
    End Function
    Private Function CheckPublicCategory()
        Try
            Dim CategoryName
            If Language_Btn.Text = "E" Then
                CategoryName = "فئة عامة"
            Else
                CategoryName = "Public Category"
            End If
            Dim CategoriesFilePath = MagNotes_Folder_Path_TxtBx.Text & "\Categories.txt"
            If File.Exists(CategoriesFilePath) Then
                Dim CategoryExist As Boolean
                For Each Category In Split(My.Computer.FileSystem.ReadAllText(CategoriesFilePath, System.Text.Encoding.UTF8), vbCrLf)
                    If CategoryName = Category Then
                        CategoryExist = True
                        Exit Function
                    End If
                Next
                If Not CategoryExist Then
                    My.Computer.FileSystem.WriteAllText(CategoriesFilePath, CategoryName & vbCrLf, 1, System.Text.Encoding.UTF8)
                    LoadCategories()
                End If
            Else
                My.Computer.FileSystem.WriteAllText(CategoriesFilePath, CategoryName & vbCrLf, 0, System.Text.Encoding.UTF8)
                LoadCategories()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function '
    Private Sub MagNote_Category_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles MagNote_Category_CmbBx.Validating
        Try
            Cursor = Cursors.WaitCursor
            If RunAsExternal() Then Exit Sub
            Dim CategoriesFilePath = MagNotes_Folder_Path_TxtBx.Text & "\Categories.txt"
            If File.Exists(CategoriesFilePath) Then
                Dim CategoryExist As Boolean
                For Each Category In Split(My.Computer.FileSystem.ReadAllText(CategoriesFilePath, System.Text.Encoding.UTF8), vbCrLf)
                    If MagNote_Category_CmbBx.Text = Category Then
                        CategoryExist = True
                        Exit For
                    End If
                Next
                If Not CategoryExist Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "This File Category Is Not Exit... Do You Want To Open It?"
                    Else
                        Msg = "هذه الفئة غير مسجلة من قبل... هل تريد فتح فئة جديدة بهذا الإسم؟"
                    End If
                    Msg &= vbNewLine & "(" & MagNote_Category_CmbBx.Text & ")"
                    If ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
                        My.Computer.FileSystem.WriteAllText(CategoriesFilePath, MagNote_Category_CmbBx.Text & vbCrLf, 1, System.Text.Encoding.UTF8)
                        LoadCategories()
                    End If
                End If
            Else
                My.Computer.FileSystem.WriteAllText(CategoriesFilePath, MagNote_Category_CmbBx.Text & vbCrLf, 0, System.Text.Encoding.UTF8)
                LoadCategories()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RchTxtBx_DragDrop(sender As Object, e As DragEventArgs)
        Try
            Cursor = Cursors.WaitCursor
            If MagNote_No_CmbBx.Text.Length = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Kindly Select Note First Or Create New One"
                Else
                    Msg = "من فضلك إختر ملاحظة أولا أو إنشئ ملاحظة جديدة"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
                Exit Sub
            End If
            Dim IsFolder As Boolean
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            Dim Img As Image
            Dim ValidImage As Boolean
            For Each File In files
                Select Case Path.GetExtension(File)
                    Case ".xlsx", ".xls"
                        LoadFile(File)
                        SetCurrentDocumentFile(File)
                End Select
                IsFolder = (System.IO.File.GetAttributes(File) And System.IO.FileAttributes.Directory) = FileAttributes.Directory
                If IsFolder Then
                    Dim txtFilesArray As String() = Directory.GetFiles(File, "*.*", SearchOption.AllDirectories)
                    For Each foundFile As String In txtFilesArray
                        Try
                            ValidImage = True
                            Img = Image.FromFile(foundFile)
                        Catch ex As Exception
                            ValidImage = False
                        End Try
                        If Not IsNothing(Img) And ValidImage Then
                            Clipboard.SetImage(Img)
                            RCSN.Paste()
                            SendKeys.Send(Keys.Enter)
                            Continue For
                        End If
                        DirectCast(Me, MagNote_Form).ParseCommandLineArgs(foundFile)
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
                        RCSN.Paste()
                        SendKeys.Send(Keys.Enter)
                        Continue For
                    End If
                    DirectCast(Me, MagNote_Form).ParseCommandLineArgs(File)
                End If
            Next

            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            e.Effect = Windows.Forms.DragDropEffects.None
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub English_Btn_Click(sender As Object, e As EventArgs) Handles English_Btn.Click
        LoadSystemVoices(0)
    End Sub
    Private Sub Arabic_Btn_Click(sender As Object, e As EventArgs) Handles Arabic_Btn.Click
        LoadSystemVoices(1)
    End Sub

    Private Sub Read_ME_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Read_ME_TlStrpBtn.Click
        Try
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
            If RCSN().SelectedText.Length > 0 Then
                If IsArabicWord(RCSN().SelectedText) Then
                    LoadSystemVoices(1)
                ElseIf IsEnglishWord(RCSN().SelectedText) Then
                    LoadSystemVoices(0)
                End If
            ElseIf RCSN().SelectionStart > 0 Then
                Try
                    Dim TextLaang = Mid(RCSN().Text, RCSN().SelectionStart, 1)
                    If IsArabicWord(TextLaang) Then
                        LoadSystemVoices(1)
                    ElseIf IsEnglishWord(TextLaang) Then
                        LoadSystemVoices(0)
                    End If
                Catch ex As Exception
                    GoTo NoTextLaang
                End Try
            Else
NoTextLaang:
                If Language_Btn.Text = "E" Then 'Arabic
                    LoadSystemVoices(1)
                Else
                    LoadSystemVoices(0)
                End If
            End If
            If cmbInstalled.Items.Count > 0 Then
                cmbInstalled.SelectedIndex = 0
                Start_Btn_Click(Start_Btn, EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Function IsArabicWord(ByVal word As String) As Boolean
        For Each c As Char In word
            If Char.GetUnicodeCategory(c) = Globalization.UnicodeCategory.OtherLetter Then
                Dim unicodeValue As Integer = AscW(c)
                ' Check if the character is within the Arabic Unicode block
                If unicodeValue >= &H600 AndAlso unicodeValue <= &H6FF Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Public Function IsEnglishWord(ByVal word As String) As Boolean
        For Each c As Char In word
            ' Check if the character is a Latin letter (A-Z, a-z)
            If (c >= "A"c AndAlso c <= "Z"c) OrElse (c >= "a"c AndAlso c <= "z"c) Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub RchTxtBx_DragEnter(sender As Object, e As DragEventArgs)
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

    Private Function LoadSystemVoices(ByVal ArabicLanguageType As Boolean)
        If ArabicLanguageType Then
            ChangeLanguage(1)
            Language_Lbl.Text = "ع"
        Else
            ChangeLanguage(0)
            Language_Lbl.Text = "E"
        End If
        cmbInstalled.Items.Clear()
        Dim objvoices As ReadOnlyCollection(Of InstalledVoice) = Speech.GetInstalledVoices(System.Windows.Forms.InputLanguage.CurrentInputLanguage.Culture)
        If objvoices.Count = 0 Then
            If Language_Btn.Text = "ع" Then
                Msg = "The audio files for this language may not be loaded... You can add the audio files through the Windows Language Setting program."
            Else
                Msg = "ربما لم يتم تحميل الملفات الصوتية الخاصة بهذه اللغة... يمكنك اضافة الملفات الصوتية من خلال برنامج أعداد اللغة فى النوافذ"
            End If
            ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
            Exit Function
        End If
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
            ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
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
            ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
            Exit Sub
        End If
        If Speech.State <> SynthesizerState.Ready And
            Speech.State <> SynthesizerState.Paused Then Exit Sub
        If Speech.State = SynthesizerState.Paused Then
            Speech.Resume()
            Exit Sub
        End If
        Speech.SetOutputToDefaultAudioDevice()
        If RCSN.SelectionStart <> 0 Then
            TextToread.Text = Mid(RCSN.Text, (RCSN.SelectionStart + 1), RCSN.TextLength - RCSN.SelectionStart)
        Else
            TextToread.Text = Nothing
        End If
        Speech.SelectVoice(cmbInstalled.Text)
        If Not String.IsNullOrEmpty(RCSN.SelectedText) Then
            Speech.SpeakAsync(RCSN.SelectedText)
        ElseIf TextToread.TextLength > 0 Then
            Speech.SpeakAsync(TextToread.Text)
        Else
            Speech.SpeakAsync(RCSN.Text)
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
            ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs)
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
    Private Sub Close_Btn_Click(sender As Object, e As EventArgs) Handles Close_Btn.Click
        Read_Me_Pnl.Visible = False
        Stop_Btn_Click(Stop_Btn, EventArgs.Empty)
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles Windows_Volume_TrckBr.Scroll
        If TextBox1.Text = "Up" Then
            SendMessageW(Handle, WM_APPCOMMAND, Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
        Else
            SendMessageW(Handle, WM_APPCOMMAND, Handle, New IntPtr(APPCOMMAND_VOLUME_DOWN))
        End If
        Application.DoEvents()
    End Sub

    Private Sub Read_Me_Pnl_MouseDown(sender As Object, e As MouseEventArgs) Handles Volume_Lbl.MouseDown, Speaking_Rate_Lbl.MouseDown, Read_Me_Pnl.MouseDown, Language_Lbl.MouseDown, Installed_Voices_Lbl.MouseDown,
        Language_Lbl.MouseDown, Installed_Voices_Lbl.MouseDown
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

    Private Sub RchTxtBx_Validating(sender As Object, e As CancelEventArgs)
    End Sub

    Private Sub Remember_Opened_External_Files_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Remember_Opened_External_Files_ChkBx.CheckedChanged

    End Sub

    Private Sub Available_MagNotes_DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_MagNotes_DGV.CellClick
        If MagNoteIsOpenedExternal() Then
            Dim arg = New DataGridViewRowEventArgs(Available_MagNotes_DGV.CurrentRow)
            CallByName(Me, "Available_MagNotes_DGV_SelectionChanged", CallType.Method, Available_MagNotes_DGV, arg)
        End If
    End Sub

    Private Sub Remember_Opened_External_Files_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Remember_Opened_External_Files_ChkBx.CheckStateChanged
        If ActiveControl.Name <> sender.name Or Remember_Opened_External_Files_ChkBx.CheckState <> CheckState.Checked Then Exit Sub
        CreateOutsideMagNoteCategory()
    End Sub
    Private Function CreateOutsideMagNoteCategory() As Boolean
        Dim ItemName As String
        If Language_Btn.Text = "E" Then 'Arabic
            ItemName = "خارج الملاحظات الملاحظة"
        Else
            ItemName = "Outside Of MagNote"
        End If
        If MagNote_Category_CmbBx.FindStringExact(ItemName) = -1 Then
            MagNote_Category_CmbBx.Text = ItemName
            MagNote_Category_CmbBx.Focus()
        Else
            MagNote_Category_CmbBx.Text = ItemName
        End If
    End Function

    Private Sub Me_Always_On_Top_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Me_Always_On_Top_ChkBx.CheckedChanged

    End Sub

    Private Function CreateOutsideMagNote() As Boolean
        Dim FolderName = MagNoteFolderPath & "\OutsideMagNote"
        If (Not System.IO.Directory.Exists(FolderName)) Then
            System.IO.Directory.CreateDirectory(FolderName)
        End If
    End Function

    Private Sub Blocked_Note_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Blocked_Note_ChkBx.CheckedChanged

    End Sub

    Private Sub Use_Main_Password_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Use_Main_Password_ChkBx.CheckedChanged

    End Sub

    Private Sub Me_Always_On_Top_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Me_Always_On_Top_ChkBx.CheckStateChanged
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name <> sender.name Then Exit Sub
        MakeTopMost()
    End Sub
#End Region
    Private Function LoadCalculationMethods()
        Try
            Calculation_Methods_CmbBx.ValueMember = "Key"
            Calculation_Methods_CmbBx.DisplayMember = "Value"
            Calculation_Methods_CmbBx.Items.Clear()
            If Language_Btn.Text = "E" Then
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(0, "جعفرى"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(1, "كراتشى"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(2, "الصومال"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(3, "رابطة العالم الإسلامي"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(4, "مكه"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(5, "مصر"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(6, "أندونيسيا"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(7, "مخصص"))
                Calculation_Methods_CmbBx.Text = "مصر"
            Else
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(0, "Jafari"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(1, "Karachi"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(2, "ISNA"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(3, "MWL"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(4, "Makkah"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(5, "Egypt"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(6, "Kemenag"))
                Calculation_Methods_CmbBx.Items.Add(New KeyValuePair(Of String, String)(7, "Custom"))
                Calculation_Methods_CmbBx.Text = "Egypt"
            End If
            Country_CmbBx.ValueMember = "Key"
            Country_CmbBx.DisplayMember = "Value"
            Country_CmbBx.Items.Clear()
            Dim CountryToSelect As String
            Dim CountriesCSV = Application.StartupPath & "\Countries.csv"
            If File.Exists(CountriesCSV) Then
                Dim Countries() = Split(My.Computer.FileSystem.ReadAllText(CountriesCSV, System.Text.Encoding.Default), vbCrLf)
                For Each Country In Countries
                    If String.IsNullOrEmpty(Country) Then Continue For
                    Country_CmbBx.Items.Add(Country)
                Next
            ElseIf File.Exists(Application.StartupPath & "\WorldCountriesCitiesLatitudeLongitude.csv") Then
                Dim Countries() = Split(My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\WorldCountriesCitiesLatitudeLongitude.csv", System.Text.Encoding.Default), vbCrLf)
                CountryToSelect = String.Empty
                Dim FullCuntries As String = String.Empty
                For Each Country In Countries
                    If String.IsNullOrEmpty(Country) Then Continue For
                    If Language_Btn.Text = "E" Then
                        CountryToSelect = (Split(Country, ",").ToList.Item(5))
                    Else
                        CountryToSelect = (Split(Country, ",").ToList.Item(4))
                    End If
                    If Not FullCuntries.Contains(CountryToSelect) Then
                        FullCuntries &= CountryToSelect & vbNewLine
                    End If
                Next
                If Not IsNothing(FullCuntries) Then
                    For Each Country In Split(FullCuntries, vbCrLf)
                        Country_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Country, Country))
                    Next
                    My.Computer.FileSystem.WriteAllText(CountriesCSV, FullCuntries, 1, System.Text.Encoding.UTF8)
                End If
            End If
            City_CmbBx.ValueMember = "Key"
            City_CmbBx.DisplayMember = "Value"
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Private Sub Country_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Country_CmbBx.SelectedIndexChanged
        Try
            If ActiveControl.Name <> sender.name Then Exit Sub
            Dim CountryClmn
            Dim TownClmn
            Dim Twn As String
            If Language_Btn.Text = "E" Then
                CountryClmn = 5
                TownClmn = 0
            Else
                CountryClmn = 4
                TownClmn = 1
            End If
            City_CmbBx.Items.Clear()
            Dim CountriesCSV
            If Debugger.IsAttached Then
                CountriesCSV = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\" & Country_CmbBx.Text & ".csv"
            Else
                CountriesCSV = Application.StartupPath & "\" & Country_CmbBx.Text & ".csv"
            End If
            If File.Exists(CountriesCSV) Then
                Dim Countries() = Split(My.Computer.FileSystem.ReadAllText(CountriesCSV, System.Text.Encoding.Default), vbCrLf)
                For Each Country In Countries
                    If String.IsNullOrEmpty(Country) Then Continue For
                    City_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Split(Country, ",").ToList.Item(2) & "," & Split(Country, ",").ToList.Item(3), Split(Country, ",").ToList.Item(TownClmn)))
                Next
            Else
                Dim TextToWrite
                Dim WCsCsLL
                If Debugger.IsAttached Then
                    WCsCsLL = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\WorldCountriesCitiesLatitudeLongitude.csv"
                Else
                    WCsCsLL = Application.StartupPath & "\WorldCountriesCitiesLatitudeLongitude.csv"
                End If
                If File.Exists(WCsCsLL) Then
                    Dim Countries() = Split(My.Computer.FileSystem.ReadAllText(WCsCsLL, System.Text.Encoding.Default), vbCrLf)
                    For Each Country In Countries
                        If String.IsNullOrEmpty(Country) Then Continue For

                        If Split(Country, ",").ToList.Item(CountryClmn) <> Country_CmbBx.Text Then Continue For

                        City_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Split(Country, ",").ToList.Item(2) & "," & Split(Country, ",").ToList.Item(3), Split(Country, ",").ToList.Item(TownClmn)))
                        For ColumnNo = 0 To 11
                            If ColumnNo = 2 Then
                                TextToWrite &= Split(Country, ",").ToList.Item(TownClmn) & ","
                                TextToWrite &= Split(Country, ",").ToList.Item(ColumnNo) & ","
                            ElseIf ColumnNo = 5 Then
                                Continue For
                            Else
                                TextToWrite &= Split(Country, ",").ToList.Item(ColumnNo) & ","
                            End If
                        Next
                        TextToWrite = Microsoft.VisualBasic.Left(TextToWrite, TextToWrite.ToString.Length - 1) & vbCrLf
                        If TextToWrite.ToString.Length > 0 Then
                            My.Computer.FileSystem.WriteAllText(CountriesCSV, TextToWrite, 1, System.Text.Encoding.Default)
                        End If
                        TextToWrite = String.Empty
                    Next
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub City_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles City_CmbBx.SelectedIndexChanged
        Try
            If City_CmbBx.SelectedIndex = -1 Then Exit Sub
            Latitude_TxtBx.Text = Split(DirectCast(City_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key, ",").ToList(0)
            Longitude_TxtBx.Text = Split(DirectCast(City_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key, ",").ToList(1)
            PrayerTime()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Function PrayerTime()
        Try
            Dim PT As New PrayTimesCalculator(CType(Val(Latitude_TxtBx.Text), Double), CType(Val(Longitude_TxtBx.Text), Double))
            PT.CalculationMethod = DirectCast(Calculation_Methods_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            Dim tim = PT.GetPrayerTimes(Now)
            Dim time1 As TimeSpan = TimeSpan.FromHours(-1)
            If Save_Day_Light_ChkBx.CheckState = CheckState.Checked Then
                Dim ts As TimeSpan = tim.Fajr
                Fajr_TxtBx.Text = ts.Add(time1).ToString
                ts = tim.Sunrise
                Sunrise_TxtBx.Text = ts.Add(time1).ToString
                ts = tim.Dhuhr
                Dhuhr_TxtBx.Text = ts.Add(time1).ToString
                ts = tim.Asr
                Asr_TxtBx.Text = ts.Add(time1).ToString
                ts = tim.Maghrib
                Maghrib_TxtBx.Text = ts.Add(time1).ToString
                ts = tim.Isha
                Isha_TxtBx.Text = ts.Add(time1).ToString
            Else
                Fajr_TxtBx.Text = tim.Fajr.ToString
                Sunrise_TxtBx.Text = tim.Sunrise.ToString
                Dhuhr_TxtBx.Text = tim.Dhuhr.ToString
                Asr_TxtBx.Text = tim.Asr.ToString
                Maghrib_TxtBx.Text = tim.Maghrib.ToString
                Isha_TxtBx.Text = tim.Isha.ToString
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try


        'Dim Fjr = tim.Fajr
        'Dim Snrs = tim.Sunrise
        'Dim Dhr = tim.Dhuhr
        'Dim Asr = tim.Asr
        'Dim Mgrb = tim.Maghrib
        'Dim Ish = tim.Isha
        'Dim Snst = tim.Sunset
        'Dim x = "4:18 AM الفجْر
        '                5:45 AM الشروق
        '                11:47 AM    الظُّهْر
        '                3:13 PM العَصر
        '                5:48 PM المَغرب
        '                7:05 PM العِشاء"
    End Function
    Public Function CalcTimeLeft(Optional ByVal ReturnPrayName As Boolean = False) As String
        Try
            If Asr_TxtBx.ReadOnly = False Then
                Exit Function
            End If

            Application.DoEvents()
            Dim PrayTime As String = String.Empty
            Dim StartTime, EndTime As Date
            StartTime = CType(Date.Today & " " & Isha_TxtBx.Text, DateTime)
            If Date.Now.Hour < 12 Then
                EndTime = CType(Date.Today & " " & Fajr_TxtBx.Text, DateTime)
            Else
                EndTime = CType(Date.Today.AddDays(1) & " " & Fajr_TxtBx.Text, DateTime)
            End If

            If TimeIsWithinRange(StartTime, EndTime) Then
                PrayTime = "Fajr"
            Else
                StartTime = CType(Date.Today & " " & Fajr_TxtBx.Text, DateTime)
                EndTime = CType(Date.Today & " " & Sunrise_TxtBx.Text, DateTime)
                If TimeIsWithinRange(StartTime, EndTime) Then
                    PrayTime = "Sunrise"
                Else
                    StartTime = CType(Date.Today & " " & Sunrise_TxtBx.Text, DateTime)
                    EndTime = CType(Date.Today & " " & Dhuhr_TxtBx.Text, DateTime)
                    If TimeIsWithinRange(StartTime, EndTime) Then
                        PrayTime = "Dhuhr"
                    Else
                        StartTime = CType(Date.Today & " " & Dhuhr_TxtBx.Text, DateTime)
                        EndTime = CType(Date.Today & " " & Asr_TxtBx.Text, DateTime)
                        If TimeIsWithinRange(StartTime, EndTime) Then
                            PrayTime = "Asr"
                        Else
                            StartTime = CType(Date.Today & " " & Asr_TxtBx.Text, DateTime)
                            EndTime = CType(Date.Today & " " & Maghrib_TxtBx.Text, DateTime)
                            If TimeIsWithinRange(StartTime, EndTime) Then
                                PrayTime = "Maghrib"
                            Else
                                StartTime = CType(Date.Today & " " & Maghrib_TxtBx.Text, DateTime)
                                EndTime = CType(Date.Today & " " & Isha_TxtBx.Text, DateTime)
                                If TimeIsWithinRange(StartTime, EndTime) Then
                                    PrayTime = "Isha"
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            If StartTime > EndTime Then
                PrayTime = "Fajr"
            End If
            Dim TimeDif
            Select Case PrayTime
                Case "Fajr"
                    If Date.Now.Hour < 12 Then
                        TimeDif = calculateDiffDates(Now, CType(Now.Date & " " & Fajr_TxtBx.Text, DateTime)).ToString
                    Else
                        TimeDif = calculateDiffDates(Now, CType(Now.Date.AddDays(1) & " " & Fajr_TxtBx.Text, DateTime)).ToString
                    End If
                    Left_Time_Lbl.Text = "الوقت المتبقى على صلاة الفجر"
                Case "Sunrise"
                    TimeDif = calculateDiffDates(Now, CType(Now.Date & " " & Sunrise_TxtBx.Text, DateTime)).ToString
                    Left_Time_Lbl.Text = "الوقت المتبقى على الشروق"
                Case "Dhuhr"
                    TimeDif = calculateDiffDates(Now, CType(Now.Date & " " & Dhuhr_TxtBx.Text, DateTime)).ToString
                    Left_Time_Lbl.Text = "الوقت المتبقى على صلاة الظهر"
                Case "Asr"
                    TimeDif = calculateDiffDates(Now, CType(Now.Date & " " & Asr_TxtBx.Text, DateTime)).ToString
                    Left_Time_Lbl.Text = "الوقت المتبقى على صلاة العصر"
                Case "Maghrib"
                    TimeDif = calculateDiffDates(Now, CType(Now.Date & " " & Maghrib_TxtBx.Text, DateTime)).ToString
                    Left_Time_Lbl.Text = "الوقت المتبقى على صلاة المغرب"
                Case "Isha"
                    TimeDif = calculateDiffDates(Now, CType(Now.Date & " " & Isha_TxtBx.Text, DateTime)).ToString
                    Left_Time_Lbl.Text = "الوقت المتبقى على صلاة العشاء"
            End Select
            Left_Time_Lbl.RightToLeft = RightToLeft.Yes
            Left_Time_Lbl.Text &= TimeDif
            Left_Time_Lbl.Text &= " < -- الوقت الحالى (" & Format(Now, "hh:mm:ss tt").ToString & ")"
            If ReturnPrayName Then
                Return PrayTime & "," & TimeDif
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Function TimeIsWithinRange(ByVal StartTime As Date, ByVal EndTime As Date) As Boolean
        Dim CurrentTime As Date = Now
        If EndTime.Ticks < StartTime.Ticks Then
            'EndTime is time for next day  
            If (CurrentTime.Ticks >= StartTime.Ticks And CurrentTime.Ticks >= EndTime.Ticks) Or
                (CurrentTime.Ticks <= StartTime.Ticks And CurrentTime.Ticks <= EndTime.Ticks) Then
                Return True
            Else
                Return False
            End If
        Else
            If CurrentTime.Ticks >= StartTime.Ticks And CurrentTime.Ticks <= EndTime.Ticks Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Private Sub Adjust_Showing_Form()
        RCSN.SendToBack()
        If Not IsNothing(GridPnl) Then
            GridPnl.SendToBack()
        End If
        MsgBox_SttsStrp.SendToBack()
        Form_Transparency_TrkBr.SendToBack()

        Setting_TbCntrl.SendToBack()
        Spliter_1_Lbl.SendToBack()
        Note_Navigater_Pnl.SendToBack()
        Note_TlStrp.SendToBack()
    End Sub
    Public Sub Insert_Btn_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Insert_Btn.MouseMove
        If Insert_Table_BtnMouseDown And
            (Insert_Table_Btn_Y <> e.Y Or
             Insert_Table_Btn_X <> e.X) Then
            If CType(sender, Control).Cursor = Cursors.SizeWE Or
                CType(sender, Control).Cursor = Cursors.IBeam Then
                Exit Sub
            End If
            Application.DoEvents()
            MoveForm(Table_Pnl)
            Insert_Table_BtnMouseDown = False
            Insert_Table_Btn_Y = e.Y
            Insert_Table_Btn_X = e.X
        End If
    End Sub
    Dim Insert_Table_Btn_Y, Insert_Table_Btn_X, Insert_Table_BtnMouseDown

    Private Sub ScrllBr_Scroll(sender As Object, e As ScrollEventArgs) Handles Rows_Count_VScrllBr.Scroll, Columns_Count_VScrllBr.Scroll, Cell_Width_VScrllBr.Scroll, Cell_Height_VScrllBr.Scroll
        Select Case sender.name
            Case Cell_Height_VScrllBr.Name
                Cell_Height_TxtBx.Text = Cell_Height_VScrllBr.Value
            Case Cell_Width_VScrllBr.Name
                Cell_Width_TxtBx.Text = Cell_Width_VScrllBr.Value
            Case Columns_Count_VScrllBr.Name
                Columns_TxtBx.Text = Columns_Count_VScrllBr.Value
            Case Rows_Count_VScrllBr.Name
                Rows_TxtBx.Text = Rows_Count_VScrllBr.Value
        End Select

    End Sub

    Private Sub Columns_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Columns_TxtBx.TextChanged
        If Table_DGV.Columns.Count > Val(Columns_TxtBx.Text) And Columns_TxtBx.TextLength > 0 Then
            For x = Table_DGV.Columns.Count To Val(Columns_TxtBx.Text) + 1 Step -1
                Table_DGV.Columns.RemoveAt(x - 1)
            Next
        ElseIf Table_DGV.Columns.Count < Val(Columns_TxtBx.Text) And Columns_TxtBx.TextLength > 0 Then
            For x = Table_DGV.Columns.Count To Val(Columns_TxtBx.Text) - 1
                Table_DGV.Columns.Add(x, "(" & x + 1 & ")")
            Next
        End If
    End Sub

    Private Sub Rows_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Rows_TxtBx.TextChanged
        If Table_DGV.Columns.Count = 0 Then Exit Sub
        Try
            If Table_DGV.Rows.Count > Val(Rows_TxtBx.Text) And Rows_TxtBx.TextLength > 0 Then
                For x = Table_DGV.Rows.Count - 1 To Val(Rows_TxtBx.Text) Step -1
                    Try
                        Table_DGV.Rows.RemoveAt(x - 1)
                    Catch ex As Exception
                    End Try
                Next
            ElseIf Table_DGV.Rows.Count < Val(Rows_TxtBx.Text) And Rows_TxtBx.TextLength > 0 Then
                For x = Table_DGV.Rows.Count To Val(Rows_TxtBx.Text) - 1
                    Table_DGV.Rows.Add()
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Cell_Width_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Cell_Width_TxtBx.TextChanged
        If Val(Cell_Width_TxtBx.Text) > 0 Then
            Table_DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            For Each Clmn In Table_DGV.Columns
                Dim column As DataGridViewColumn = Table_DGV.Columns(Clmn.index)
                column.Width = Val(Cell_Width_TxtBx.Text)
            Next
        End If
    End Sub

    Private Sub Cell_Height_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Cell_Height_TxtBx.TextChanged
        If Val(Cell_Height_TxtBx.Text) > 0 Then
            For Each Rw In Table_DGV.Rows
                Table_DGV.Rows(Rw.index).Height = Val(Cell_Height_TxtBx.Text)
            Next
        End If
    End Sub

    Private Sub Exit_Table_Pnl_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Table_Pnl_Btn.Click
        Table_Pnl.Visible = False
    End Sub

    Private Sub Selected_Text_Backcolor_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Selected_Text_Backcolor_TlStrpMnItm.Click
        If Not ChechRchTxtBx() Then Exit Sub
        If ColorDlg.ShowDialog() <> DialogResult.Cancel Then
            Selected_Text_Color_TlStrpMnItm.BackColor = ColorDlg.Color
            RCSN.SelectionBackColor = ColorDlg.Color
            Application.DoEvents()
            BackIsSelected = True
        End If
    End Sub


    Private Sub Stop_Displaying_Controls_ToolTip_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Stop_Displaying_Controls_ToolTip_ChkBx.CheckedChanged
    End Sub

    Private Sub Table_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Table_DGV.CellContentClick

    End Sub

    Public Sub Insert_Table_Btn_MouseDown(ByVal sender As Object, e As MouseEventArgs) Handles Insert_Btn.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Insert_Table_BtnMouseDown = True
            Insert_Table_Btn_Y = e.Y
            Insert_Table_Btn_X = e.X
        End If
    End Sub
    Public Sub Note_Font_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Note_Font_Name_CmbBx.SelectedIndexChanged,
        Note_Font_Size_CmbBx.SelectedIndexChanged,
        Note_Font_Style_CmbBx.SelectedIndexChanged
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name = sender.name Then
            Note_Font_TxtBx.Text = Note_Font_Name_CmbBx.Text & " - " & Note_Font_Size_CmbBx.Text & " - " & Note_Font_Style_CmbBx.Text
        End If
    End Sub
    Public Sub External_Note_Font_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles External_Note_Font_Name_CmbBx.SelectedIndexChanged,
        External_Note_Font_Size_CmbBx.SelectedIndexChanged,
        External_Note_Font_Style_CmbBx.SelectedIndexChanged
        External_Note_Font_TxtBx.Text = External_Note_Font_Name_CmbBx.Text & " - " & External_Note_Font_Size_CmbBx.Text & " - " & External_Note_Font_Style_CmbBx.Text
    End Sub

    Private Function Change_RchTxtBxFont() As Boolean

    End Function

    Public Sub Insert_Table_Btn_MouseUp(ByVal sender As Object, e As MouseEventArgs) Handles Insert_Btn.MouseUp
        Insert_Table_BtnMouseDown = False
        Insert_Table_Btn_Y = e.Y
        Insert_Table_Btn_X = e.X
    End Sub

    Private Sub Table_DGV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Table_DGV.RowsAdded
        Table_DGV.Rows(e.RowIndex).HeaderCell.Value = "(" & Table_DGV.Rows.Count & ")"
    End Sub

#End Region


    Private Shared Function Assign(Of T)(ByRef source As T, ByVal value As T) As T
        source = value
        Return value
    End Function
    Private Function Encrypt(inputFilePath As String, outputfilePath As String) As Boolean
        Try
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using fs As New FileStream(outputfilePath, FileMode.Create)
                    Using cs As New CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        Using fsInput As New FileStream(inputFilePath, FileMode.Open)
                            Dim data As Integer
                            While (Assign(data, fsInput.ReadByte())) <> -1
                                cs.WriteByte(CByte(data))
                            End While
                        End Using
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Function Decrypt(inputFilePath As String, outputfilePath As String) As Boolean
        Try
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using fs As New FileStream(inputFilePath, FileMode.Open)
                    Using cs As New CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read)
                        Using fsOutput As New FileStream(outputfilePath, FileMode.Create)
                            Dim data As Integer
                            While (Assign(data, cs.ReadByte())) <> -1
                                fsOutput.WriteByte(CByte(data))
                            End While
                        End Using
                    End Using
                End Using
            End Using
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Private Sub Numeric_Cell_Format_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Numeric_Cell_Format_TlStrpMnItm.Click, Numeric_Cell_Format_1_TlStrpMnItm.Click, Numeric_Cell_Format_1_TlStrpMnItm.Click
        Dim sheet = Grid.CurrentWorksheet
        sheet.SelectionStyle = WorksheetSelectionStyle.FocusRect
        sheet.SetRangeDataFormat(sheet.SelectionRange.ToAddress, CellDataFormatFlag.Number, New NumberDataFormatter.NumberFormatArgs() With {
    .DecimalPlaces = 2,
    .NegativeStyle = NumberDataFormatter.NumberNegativeStyle.RedBrackets,
    .UseSeparator = True
})

    End Sub
    Private Sub DateTime_Cell_Format_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles DateTime_Cell_Format_TlStrpMnItm.Click, DateTime_Cell_Format_1_TlStrpMnItm.Click, DateTime_Cell_Format_2_TlStrpMnItm.Click
        Dim sheet = Grid.CurrentWorksheet
        sheet.SelectionStyle = WorksheetSelectionStyle.FocusRect
        sheet.SetRangeDataFormat(sheet.SelectionRange.ToAddress, CellDataFormatFlag.DateTime, New NumberDataFormatter.NumberFormatArgs() With {
    .DecimalPlaces = 2,
    .NegativeStyle = NumberDataFormatter.NumberNegativeStyle.RedBrackets,
    .UseSeparator = True
})

    End Sub
    Private Sub Currency_Cell_Format_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Currency_Cell_Format_TlStrpMnItm.Click, Currency_Cell_Format_1_TlStrpMnItm.Click, Currency_Cell_Format_2_TlStrpMnItm.Click
        Dim sheet = Grid.CurrentWorksheet
        sheet.SelectionStyle = WorksheetSelectionStyle.FocusRect
        sheet.SetRangeDataFormat(sheet.SelectionRange.ToAddress, CellDataFormatFlag.Currency, New NumberDataFormatter.NumberFormatArgs() With {
    .DecimalPlaces = 2,
    .NegativeStyle = NumberDataFormatter.NumberNegativeStyle.RedBrackets,
    .UseSeparator = True
})

    End Sub
    Private Sub General_Cell_Format_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles General_Cell_Format_TlStrpMnItm.Click, General_Cell_Format_1_TlStrpMnItm.Click, General_Cell_Format_2_TlStrpMnItm.Click
        Dim sheet = Grid.CurrentWorksheet
        sheet.SelectionStyle = WorksheetSelectionStyle.FocusRect
        sheet.SetRangeDataFormat(sheet.SelectionRange.ToAddress, CellDataFormatFlag.General, New NumberDataFormatter.NumberFormatArgs() With {
    .DecimalPlaces = 2,
    .NegativeStyle = NumberDataFormatter.NumberNegativeStyle.RedBrackets,
    .UseSeparator = True
})

    End Sub
    Private Sub Text_Cell_Format_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Text_Cell_Format_TlStrpMnItm.Click, Text_Cell_Format_1_TlStrpMnItm.Click, Text_Cell_Format_2_TlStrpMnItm.Click
        Dim sheet = Grid.CurrentWorksheet
        sheet.SelectionStyle = WorksheetSelectionStyle.FocusRect
        sheet.SetRangeDataFormat(sheet.SelectionRange.ToAddress, CellDataFormatFlag.Text, New NumberDataFormatter.NumberFormatArgs() With {
    .DecimalPlaces = 2,
    .NegativeStyle = NumberDataFormatter.NumberNegativeStyle.RedBrackets,
    .UseSeparator = True
})

    End Sub


    Private Sub MagNotes_Folder_Path_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles MagNotes_Folder_Path_TxtBx.TextChanged
        MagNoteFolderPath = MagNotes_Folder_Path_TxtBx.Text
    End Sub
    Private Function Copy_Available_MagNotes_DGV_To_Clipboard()
        If Available_MagNotes_DGV.Rows.Count = 0 Then Exit Function
        Available_MagNotes_DGV.MultiSelect = True
        Available_MagNotes_DGV.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Available_MagNotes_DGV.SuspendLayout()
        If Available_MagNotes_DGV.SelectedRows.Count = 0 Then
            Available_MagNotes_DGV.SelectAll()
        End If
        Application.DoEvents()
        Clipboard.SetDataObject(Available_MagNotes_DGV.GetClipboardContent())
        Available_MagNotes_DGV.ClearSelection()
        Available_MagNotes_DGV.MultiSelect = False
    End Function
    Private Function UpdateMagNoteInternalName() As Boolean
        Dim fils()
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.Filter = "All files|*.*"
        OpenFileDialog.Multiselect = True
        OpenFileDialog.FileName = ""
        OpenFileDialog.RestoreDirectory = True
        If OpenFileDialog.ShowDialog = DialogResult.Cancel Then
            Exit Function
        Else
            fils = OpenFileDialog.FileNames
        End If
        Dim WordToSave As String
        For Each Fil In fils
            Dim FileToPast = Replace(Fil, Path.GetFileNameWithoutExtension(Fil), Path.GetFileNameWithoutExtension(Fil) & "_Copy")
            File.Copy(Fil, FileToPast, 1)
            WordToSave = My.Computer.FileSystem.ReadAllText(Fil, System.Text.Encoding.UTF8)
            WordToSave = Replace(WordToSave, "OldFlder", "NewFolder")
            WordToSave = Replace(WordToSave, "(F(Instead Of Colon)", "(C(Instead Of Colon)")
            WordToSave = Replace(WordToSave, "Sticky_Note", "MagNote")
            WordToSave = Replace(WordToSave, "Sticky", "Note")
            Application.DoEvents()
            My.Computer.FileSystem.WriteAllText(Fil, WordToSave, 0, System.Text.Encoding.UTF8)
            WordToSave = String.Empty
        Next
    End Function

    Private Sub Upload_Last_Version_Btn_Click(sender As Object, e As EventArgs) Handles Upload_Last_Version_Btn.Click
        Try
            If ShowMsg("Do You Want To Run Test Routine",, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                LoadCalculationMethods()
                Exit Sub
            End If
            If Language_Btn.Text = "E" Then
                Msg = "جارى الآن رفع الملف واعداد اجراءات التحديث للمستخدمين"
            Else
                Msg = "Currently Uploading File Now And Preparing Settings To Upgrade Users"
            End If
            ShowMsg(Msg,,,,,,, 0)
            Cursor = Cursors.WaitCursor
            Dim FileName = "UpdateMagNoteFileInformation.txt"
            Dim UserName = FTP_Login(0).FTP_UserName
            Dim Password = FTP_Login(0).FTP_Password
            Dim Address = "ftp://" & FTP_Login(0).FTP_UserName & "@" & FTP_Login(0).FTP_Address
            Dim WordToSave = "//Update MagNote Program,
Update File Name:" & Application.ProductName & "-Copy.exe,
Update File Version:" & Application.ProductVersion & ",
Update Download File Path:,"
            Dim AFTU = MagNoteFolderPath & "\AdditionalFilesToUpload.txt"
            If File.Exists(AFTU) Then
                WordToSave &= vbNewLine & My.Computer.FileSystem.ReadAllText(AFTU, System.Text.Encoding.UTF8)
            End If
            My.Computer.FileSystem.WriteAllText(MagNoteFolderPath & "\" & FileName, WordToSave, 0, System.Text.Encoding.UTF8)
            My.Computer.Network.UploadFile(MagNoteFolderPath & "\" & FileName, Address & FileName, UserName, Password)
            FileName = Application.StartupPath & "\" & Application.ProductName & "-Copy.exe"
            File.Copy(Application.StartupPath & "\" & Application.ProductName & ".exe", FileName, 1)
            My.Computer.Network.UploadFile(FileName, Address & "/" & Application.ProductName & "-Copy.exe", UserName, Password)
            If Language_Btn.Text = "E" Then
                Msg = "تم رفع الملف بنجاح"
            Else
                Msg = "File Succefully Uploaded"
            End If
            ShowMsg(Msg & vbNewLine & FileName & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Show_Hide_MagNote_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Show_Hide_MagNote_TlStrpMnItm.Click
        If RCSN.Visible Then
            RCSN.Visible = False
        Else
            RCSN.Visible = True
        End If
        Adjust_Showing_Form()
    End Sub

    Private Sub Show_Hide_Setting_Tab_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Show_Hide_Setting_Tab_TlStrpMnItm.Click
        If Setting_TbCntrl.Visible Then
            Setting_TbCntrl.Visible = False
        Else
            Setting_TbCntrl.Visible = True
        End If
        Adjust_Showing_Form()
    End Sub

    Private Sub Show_Hide_Note_Grid_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Show_Hide_Note_Grid_TlStrpMnItm.Click
        If IsNothing(GridPnl) Then Exit Sub
        If GridPnl.Visible Then
            GridPnl.Visible = False
        Else
            Grid.Parent = GridPnl()
            Grid.Dock = DockStyle.Fill
            GridPnl.Visible = True
            GridPnl.Parent = MagNotes_Notes_TbCntrl.SelectedTab
            GridPnl.Dock = DockStyle.Bottom
            GridPnl.SendToBack()
        End If
        Adjust_Showing_Form()
    End Sub

    Private Sub MagNotes_Notes_TbCntrl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MagNotes_Notes_TbCntrl.SelectedIndexChanged
        Dim RchTxtBxName As String
        Dim MeAsActivecontrol As Boolean
        Try
            If ActiveControl.Name = sender.name Then
                MeAsActivecontrol = True
            End If
            Application.DoEvents()
            If MagNotes_Notes_TbCntrl.SelectedIndex = -1 Then
                Exit Sub
            End If
            IsInMagNoteCmbBx(MagNotes_Notes_TbCntrl.SelectedTab.Tag, 1,,,,, 1)
            If MagNotes_Notes_TbCntrl.SelectedIndex = -1 Then
                Exit Sub
            End If
            If DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value = MagNotes_Notes_TbCntrl.SelectedTab.Text And
              DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = MagNotes_Notes_TbCntrl.SelectedTab.Tag And
              MagNotes_Notes_TbCntrl.SelectedTab.Controls.Count <> 0 Then
                RchTxtBxName = MagNotes_Notes_TbCntrl.SelectedTab.Name & "RchTxtBx"
                CType(MagNotes_Notes_TbCntrl.SelectedTab.Controls(RchTxtBxName), RichTextBox).BringToFront()
                If isInDataGridView(MagNotes_Notes_TbCntrl.SelectedTab.Name, "MagNote_Name", Available_MagNotes_DGV, 0,,, 1) Then
                    Available_MagNotes_DGV.Rows(isInDataGridView(DirectCast(MagNote_No_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key, "MagNote_Name", Available_MagNotes_DGV, 0)).Selected = True
                Else
                    Available_MagNotes_DGV.ClearSelection()
                End If
            End If
            If MagNotes_Notes_TbCntrl.SelectedIndex <> -1 Then
                GetFileType()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Try
                'SelectFileFormat(MagNotes_Notes_TbCntrl.SelectedTab.Tag)
                Cursor = Cursors.Default
                If Not IsNothing(GridPnl(0)) Then
                    GridPnl.BringToFront()
                End If
                If MagNotes_Notes_TbCntrl.SelectedIndex <> -1 And
                    Not String.IsNullOrEmpty(RchTxtBxName) Then
                    RefreshNoteSetting(MagNotes_Notes_TbCntrl.SelectedTab.Tag,, 1,, MeAsActivecontrol)
                End If
                If Not String.IsNullOrEmpty(RchTxtBxName) Then
                    RCSN(0).Focus()
                End If
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Private Function GetFileType() As Boolean
        Try
            Dim Extension = Path.GetExtension(MagNotes_Notes_TbCntrl.SelectedTab.Tag)
            If Not MagNoteFileFormat(MagNotes_Notes_TbCntrl.SelectedTab.Tag, 1) Then
                IsInMagNoteCmbBx(Microsoft.VisualBasic.Right(Extension, Extension.Length - 1), 1, File_Format_CmbBx)
            Else
                If Language_Btn.Text = "E" Then
                    IsInMagNoteCmbBx("ملاحظة (txt)", 1, File_Format_CmbBx,,, 1)
                Else
                    IsInMagNoteCmbBx("MagNote (txt)", 1, File_Format_CmbBx,,, 1)
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Public Function InitiateRchTxtBx(Optional ByVal RchTxtBxName As String = Nothing) As Boolean
        If Not String.IsNullOrEmpty(RchTxtBxName) Then
            If Not IsNothing(MagNotes_Notes_TbCntrl.SelectedTab.Controls(RchTxtBxName & "RchTxtBx")) Then
                Exit Function
            End If
        End If
        Dim NewRchTxtBx As RichTextBox = New RichTextBox
        With NewRchTxtBx
            .Name = MagNotes_Notes_TbCntrl.SelectedTab.Name & "RchTxtBx"
            .Dock = DockStyle.Fill
            .Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
            .DetectUrls = True
            If Language_Btn.Text = "E" Then
                .RightToLeft = RightToLeft.Yes
                .SelectionAlignment = HorizontalAlignment.Right
            End If
        End With
        MagNotes_Notes_TbCntrl.SelectedTab.Controls.Add(NewRchTxtBx)
        NewRchTxtBx.Visible = True
        NewRchTxtBx.EnableAutoDragDrop = True
        AddHandler NewRchTxtBx.TextChanged, AddressOf RchTxtBx_TextChanged
        AddHandler NewRchTxtBx.DragDrop, AddressOf RchTxtBx_DragDrop
        AddHandler NewRchTxtBx.DragEnter, AddressOf RchTxtBx_DragEnter
        AddHandler NewRchTxtBx.GotFocus, AddressOf RchTxtBx_GotFocus
        AddHandler NewRchTxtBx.LinkClicked, AddressOf RchTxtBx_LinkClicked
        AddHandler NewRchTxtBx.LostFocus, AddressOf RchTxtBx_LostFocus
        AddHandler NewRchTxtBx.MouseDown, AddressOf RchTxtBx_MouseDown
        AddHandler NewRchTxtBx.MouseMove, AddressOf RchTxtBx_MouseMove
        AddHandler NewRchTxtBx.MouseUp, AddressOf RchTxtBx_MouseUp
        AddHandler NewRchTxtBx.Validating, AddressOf RchTxtBx_Validating
        AddHandler NewRchTxtBx.Click, AddressOf RchTxtBx_Click
        AddHandler NewRchTxtBx.KeyUp, AddressOf RchTxtBx_KeyUp
        AddHandler NewRchTxtBx.KeyDown, AddressOf RchTxtBx_KeyDown
        AddHandler NewRchTxtBx.SelectionChanged, AddressOf RchTxtBx_SelectionChanged

        If IsNothing(RchTxtBxSelectedText(0).MagNoteName) Then
            RchTxtBxSelectedText(0).MagNoteName = Path.GetFileName(NewRchTxtBx.Name)
        Else
            ReDim Preserve RchTxtBxSelectedText(RchTxtBxSelectedText.Length)
            RchTxtBxSelectedText(RchTxtBxSelectedText.Length - 1).MagNoteName = Path.GetFileName(NewRchTxtBx.Name)
        End If
    End Function


    Private Sub Grid_GotFocus(sender As Object, e As EventArgs)
    End Sub

    Private Sub Spliter_1_Lbl_Click(sender As Object, e As EventArgs) Handles Spliter_1_Lbl.Click

    End Sub

    Private Sub Worksheet_AfterCellEdit(ByVal sender As Object, ByVal e As CellAfterEditEventArgs)
        For Each Sheet In Grid.Worksheets
            If Sheet.Name <> sender.name Then
                Continue For
            End If
            Sheet.Recalculate()
            Sheet.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, True)
        Next
    End Sub
    Private Sub Grid_Click(sender As Object, e As EventArgs)
        Dim b = Grid.CurrentWorksheet.FocusPos
        Debug.Print(b.ToString)
        If Grid.CurrentWorksheet.Cells(b).HasFormula Then
            Dim x = Grid.CurrentWorksheet.Cells(b).Formula
            Form_ToolTip.SetToolTip(Grid, Grid.CurrentWorksheet.Cells(b).Formula)
        Else
            Form_ToolTip.SetToolTip(Grid, b.ToString)
        End If
    End Sub
    Private Sub Spliter_1_Lbl_Paint(sender As Object, e As PaintEventArgs) Handles Spliter_1_Lbl.Paint
    End Sub
    Private Sub Show_Control_Tab_Pages_In_Multi_Line_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckedChanged
    End Sub
    Private Sub Load_MagNote_At_Startup_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Load_MagNote_At_Startup_ChkBx.CheckedChanged
    End Sub
    Private Sub Save_By_MS_Excel_TlStrpBtn_Click(sender As Object, e As EventArgs)
        If Save_Note_Grid() Then
            Process.Start("EXCEL.EXE", MagNoteFolderPath & "\TempFile.xlsx")
        End If
    End Sub

    Private Sub Show_Control_Tab_Pages_In_Multi_Line_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckStateChanged
        Select Case Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckState
            Case CheckState.Checked
                MagNotes_Notes_TbCntrl.Multiline = True
            Case CheckState.Unchecked
                MagNotes_Notes_TbCntrl.Multiline = False
        End Select
    End Sub

    Private Sub Load_MagNote_At_Startup_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Load_MagNote_At_Startup_ChkBx.CheckStateChanged
        Select Case Load_MagNote_At_Startup_ChkBx.CheckState
            Case CheckState.Checked
                If Language_Btn.Text = "E" Then
                    Load_MagNote_At_Startup_ChkBx.Text = "تحميل جميع الملفات فى بداية البرنامج"
                Else
                    Load_MagNote_At_Startup_ChkBx.Text = "Load Files At Progarm Startup"
                End If
            Case CheckState.Unchecked
                If Language_Btn.Text = "E" Then
                    Load_MagNote_At_Startup_ChkBx.Text = "تحميل الملفات المفتوحة فقط فى بداية البرنامج"
                Else
                    Load_MagNote_At_Startup_ChkBx.Text = "Load Opened Files Only At Progarm Startup"
                End If
            Case CheckState.Indeterminate
                If Language_Btn.Text = "E" Then
                    Load_MagNote_At_Startup_ChkBx.Text = "تحميل الملف الحالي فقط فى بداية البرنامج"
                Else
                    Load_MagNote_At_Startup_ChkBx.Text = "Load Current File Only At Progarm Startup"
                End If
        End Select
    End Sub
    Private Sub Send_Escalation_To_Author_Btn_Click(sender As Object, e As EventArgs) Handles Send_Escalation_To_Author_Btn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Escalation_Auther_Mail_TxtBx.TextLength = 0 Or
                User_Escalation_RchTxtBx.TextLength = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "عفوا... يجب ادخال البريد الالكترونى الخاص بالراسل وشرح الاشكالية المطلوب ارسالها"
                Else
                    Msg = "Pardon... Must Enter The Sender Email Address And The Escalation Description You Want To Send"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                Exit Sub
            End If
            If IsValidEmailFormat(Escalation_Auther_Mail_TxtBx) Then
                Dim AttatchFile As String = Nothing
                If With_Current_MagNote_ChkBx.CheckState = CheckState.Checked Then
                    AttatchFile = MagNotes_Notes_TbCntrl.SelectedTab.Tag
                End If
                Using sen As New SentMailInBackground(Escalation_Label_TxtBx.Text, User_Escalation_RchTxtBx, "MagNote_Escalation@infosysme.com", Escalation_Auther_Mail_TxtBx.Text, Nothing, "User Name (" & Escalation_Auther_Mail_TxtBx.Text & ")", Escalation_Auther_Mail_TxtBx.Text, , AttatchFile)
                End Using
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Function IsValidEmailFormat(ByVal EmailAddress As Object) As Boolean
        Try
            If EmailAddress.Textlength = 0 Then
                Return True
            End If
            Dim a As New System.Net.Mail.MailAddress(EmailAddress.Text)
            Return True
        Catch
            If Language_Btn.Text = "E" Then
                Msg = "صيغة هذا البريد الالكترونى غير سليمة ... هل تريد الاستمرار؟"
            Else
                Msg = "This Email Format Is Not Correct... Do You Want To Continue?"
            End If
            If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.No Then
                CType(EmailAddress, TextBox).Focus()
                Return False
            Else
                EmailAddress.Text = Nothing
            End If
        End Try
    End Function
    Private Sub Save_Copy_Of_Current_MagNote_At_Cloud_Area_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Save_Copy_Of_Current_MagNote_At_Cloud_Area_ChkBx.CheckedChanged
    End Sub


    Private Sub User_Escalation_RchTxtBx_TextChanged(sender As Object, e As EventArgs) Handles User_Escalation_RchTxtBx.TextChanged

    End Sub

    Private Sub RchTxtBx_Click(sender As Object, e As EventArgs)
    End Sub
    Public Function FtpFileExists(ByVal fileUri As String) As Boolean
        Dim request As FtpWebRequest = WebRequest.Create(fileUri)
        request.Credentials = New NetworkCredential(FTP_User_Name_TxtBx.Text, FTP_Password_TxtBx.Text)
        request.Method = WebRequestMethods.Ftp.GetFileSize
        Try
            Dim response As FtpWebResponse = request.GetResponse()
            ' THE FILE EXISTS
        Catch ex As WebException
            Dim response As FtpWebResponse = ex.Response
            If FtpStatusCode.ActionNotTakenFileUnavailable = response.StatusCode Then
                ' THE FILE DOES NOT EXIST
                Return False
            End If
        End Try
        Return True
    End Function
    Private Sub Save_Copy_Of_Current_MagNote_At_Cloud_Area_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Save_Copy_Of_Current_MagNote_At_Cloud_Area_ChkBx.CheckStateChanged
        Try
            If Save_Copy_Of_Current_MagNote_At_Cloud_Area_ChkBx.CheckState = CheckState.Checked Then
                If FTP_Address_TxtBx.TextLength = 0 Or
                    FTP_User_Name_TxtBx.TextLength = 0 Or
                    FTP_Password_TxtBx.TextLength = 0 Then
                    If Language_Btn.Text = "E" Then
                        Msg = "من فضلك ادخل بيانات الاتصال بالموقع السحابى اولا"
                    Else
                        Msg = "Please Enter The Cloud Site Contact Information First"
                    End If
                    ShowMsg(Msg)
                    Exit Sub
                End If
                Dim FileName = MagNotes_Notes_TbCntrl.SelectedTab.Tag
                Dim Address = FTP_Address_TxtBx.Text
                Dim UserName = FTP_User_Name_TxtBx.Text
                Dim Password = FTP_Password_TxtBx.Text
                If FtpFileExists(Address & Path.GetFileName(FileName)) Then
                    If Language_Btn.Text = "E" Then
                        Msg = "توجد نسخة من هذا الملف على الموقع السحابى... هل تريد استبدالها بالملف الحالى؟"
                    Else
                        Msg = "There IS A Copy Of This File On The Cloud Site... Do You Want To Replace It With The Current File?"
                    End If
                    If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
                End If
                If Language_Btn.Text = "E" Then
                    Msg = "جارى الآن رفع الملف"
                Else
                    Msg = "Currently Uploading File Now"
                End If
                ShowMsg(Msg,,,,,,, 0)
                Cursor = Cursors.WaitCursor
                My.Computer.Network.UploadFile(FileName, Address & Path.GetFileName(FileName), UserName, Password)
                If Language_Btn.Text = "E" Then
                    Msg = "تم رفع الملف بنجاح"
                Else
                    Msg = "File Succefully Uploaded"
                End If
                ShowMsg(Msg & vbNewLine & FileName & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            Save_Copy_Of_Current_MagNote_At_Cloud_Area_ChkBx.CheckState = CheckState.Unchecked
        End Try

    End Sub
    Dim CursorPoint As Point
    Dim TabPageToClose As TabPage
    Private Sub MagNotes_Notes_TbCntrl_MouseDown(sender As Object, e As MouseEventArgs) Handles MagNotes_Notes_TbCntrl.MouseDown
        If e.Button = MouseButtons.Right Then
            TabPageToClose = New TabPage
            For Each TbPg In MagNotes_Notes_TbCntrl.TabPages
                If MagNotes_Notes_TbCntrl.GetTabRect(MagNotes_Notes_TbCntrl.TabPages.IndexOf(TbPg)).Contains(e.X, e.Y) Then
                    TabPageToClose = TbPg
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub MagNotes_Notes_TbCntrl_MouseUp(sender As Object, e As MouseEventArgs) Handles MagNotes_Notes_TbCntrl.MouseUp
    End Sub

    Dim cntrl As New Object
    Private Function Initiate_ReoGride() As unvell.ReoGrid.ReoGridControl
        Try
            If MagNotes_Notes_TbCntrl.SelectedTab.Controls.Find(MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid_Pnl", 1).Length <> 0 Then
                MagNotes_Notes_TbCntrl.SelectedTab.Controls(MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid_Pnl").Visible = True
                MagNotes_Notes_TbCntrl.SelectedTab.Controls(MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid_Pnl").Dock = DockStyle.Fill
                RchTxtBx.Dock = DockStyle.Top
                RchTxtBx.SendToBack()
                MagNotes_Notes_TbCntrl.SelectedTab.Controls(MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid_Pnl").BringToFront()
                Exit Function
            End If
            Dim GridPnl = New System.Windows.Forms.Panel()
            GridPnl.Location = New System.Drawing.Point(30, 80)
            GridPnl.Name = MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid_Pnl"
            GridPnl.Size = New System.Drawing.Size(908, 250)
            MagNotes_Notes_TbCntrl.SelectedTab.Controls.Add(GridPnl)
            GridPnl.Dock = System.Windows.Forms.DockStyle.Fill
            RchTxtBx.Dock = System.Windows.Forms.DockStyle.Top
            GridPnl.Visible = True
            Dim Grid = New unvell.ReoGrid.ReoGridControl()
            Grid.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Grid.ColumnHeaderContextMenuStrip = Column_CntxtMnStrp
            Grid.ContextMenuStrip = Cell_CntxtMnStrp
            Grid.Dock = System.Windows.Forms.DockStyle.Fill
            Grid.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
            Grid.LeadHeaderContextMenuStrip = Nothing
            Grid.Location = New System.Drawing.Point(0, 54)
            Grid.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Grid.Name = MagNotes_Notes_TbCntrl.SelectedTab.Text & "Grid"
            Grid.RowHeaderContextMenuStrip = Row_CntxtMnStrp
            Grid.Script = Nothing
            Grid.SheetTabContextMenuStrip = Nothing
            Grid.SheetTabNewButtonVisible = True
            Grid.SheetTabVisible = True
            Grid.SheetTabWidth = 298
            Grid.ShowScrollEndSpacing = True
            Grid.Size = New System.Drawing.Size(908, 246)
            Grid.TabIndex = 34
            Grid.Text = "ReoGridControl1"

            Dim Save_TlStrpSprtr_09 = New System.Windows.Forms.ToolStripSeparator()
            Dim Cell_Back_Color_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Text_Color_TlStrpLbl = New System.Windows.Forms.ToolStripLabel()
            Dim Cell_Text_Color_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Cell_BackColor_TlStrpLbl = New System.Windows.Forms.ToolStripLabel()
            Dim Font_TlStrpSprtr_01 = New System.Windows.Forms.ToolStripSeparator()
            Dim Font_TlStrpCmbBx = New System.Windows.Forms.ToolStripComboBox()
            Dim Font_Size_TlStrpCmbBx = New System.Windows.Forms.ToolStripComboBox()
            Dim Enlarge_Font_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Font_Smaller_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Bold_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Italic_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Under_Line_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Strike_Through_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Font_TlStrpSprtr_02 = New System.Windows.Forms.ToolStripSeparator()
            Dim Text_Wrap_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Font_TlStrpSprtr_03 = New System.Windows.Forms.ToolStripSeparator()
            Dim Text_Align_Left_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Text_Align_Center_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Text_Align_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Distributed_Indent_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Font_TlStrpSprtr_04 = New System.Windows.Forms.ToolStripSeparator()
            Dim Text_Align_Top_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Text_Align_Middle_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Text_Align_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Font_TlStrpSprtr_05 = New System.Windows.Forms.ToolStripSeparator()
            Dim Zoom_TlStrpCmbBx = New System.Windows.Forms.ToolStripComboBox()
            Dim new_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim load_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Save_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Save_By_MS_Excel_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Print_Preview_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Save_TlStrpSprtr_01 = New System.Windows.Forms.ToolStripSeparator()
            Dim Copy_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Cut_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Paste_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Style_Brush_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Save_TlStrpSprtr_02 = New System.Windows.Forms.ToolStripSeparator()
            Dim Undo_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Redo_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Save_TlStrpSprtr_03 = New System.Windows.Forms.ToolStripSeparator()
            Dim Cell_Border_Color_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Cell_Border_Color_TlStrpLbl = New System.Windows.Forms.ToolStripLabel()
            Dim Save_TlStrpSprtr_04 = New System.Windows.Forms.ToolStripSeparator()
            Dim Outside_Borders_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Save_TlStrpSprtr_08 = New System.Windows.Forms.ToolStripSeparator()
            Dim Cell_Merge_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Unmerge_Range_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            '-------------------------------------------
            Dim Solid_Outside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Outside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dotted_Outside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Outside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Inside_Borders_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Inside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Inside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dotted_Inside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Inside_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim All_Borders_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_All_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_All_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dotted_All_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_All_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Clear_Borders_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
            Dim Save_TlStrpSprtr_05 = New System.Windows.Forms.ToolStripSeparator()
            Dim Top_Bottom_Borders_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Top_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Top_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Top_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Top_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Left_Right_Borders_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Left_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Left_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Left_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Left_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Save_TlStrpSprtr_06 = New System.Windows.Forms.ToolStripSeparator()
            Dim Top_Border_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Top_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Blod_Top_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Top_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Top_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bottom_Border_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Bottom_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Left_Border_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Right_Boder_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Right_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Save_TlStrpSprtr_07 = New System.Windows.Forms.ToolStripSeparator()
            Dim Right_Slash_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Right_Slash_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Right_Slash_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Right_Slash_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Right_Slash_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Slash_Left_TlStrpBtn = New System.Windows.Forms.ToolStripSplitButton()
            Dim Solid_Slash_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Bold_Slash_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Doted_Slash_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            Dim Dashed_Slash_Left_TlStrpBtn = New System.Windows.Forms.ToolStripMenuItem()
            '-------------------------------
            Dim Cell_Border_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
            Dim Cell_BackColor_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
            Dim Cell_Text_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()

            Save_TlStrpSprtr_09.Name = "Save_TlStrpSprtr_09"
            Save_TlStrpSprtr_09.Size = New System.Drawing.Size(6, 27)
            Cell_Back_Color_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Cell_Back_Color_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.ColorHS
            Cell_Back_Color_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Cell_Back_Color_TlStrpBtn.Name = "Cell_Back_Color_TlStrpBtn"
            Cell_Back_Color_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Cell_Back_Color_TlStrpBtn.Text = "Cell BackColor"
            Text_Color_TlStrpLbl.AutoSize = False
            Text_Color_TlStrpLbl.Name = "Text_Color_TlStrpLbl"
            Text_Color_TlStrpLbl.Size = New System.Drawing.Size(87, 22)
            Text_Color_TlStrpLbl.ToolTipText = "Text Color"
            Cell_Text_Color_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Cell_Text_Color_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.ColorHS
            Cell_Text_Color_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Cell_Text_Color_TlStrpBtn.Name = "Cell_Text_Color_TlStrpBtn"
            Cell_Text_Color_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Cell_Text_Color_TlStrpBtn.Text = "Text Color"
            Cell_Text_Color_TlStrpBtn.ToolTipText = "Cell Text Color"
            Cell_BackColor_TlStrpLbl.AutoSize = False
            Cell_BackColor_TlStrpLbl.Name = "Cell_BackColor_TlStrpLbl"
            Cell_BackColor_TlStrpLbl.Size = New System.Drawing.Size(87, 22)
            Cell_BackColor_TlStrpLbl.ToolTipText = "Cell BackColor"
            Font_TlStrpSprtr_01.Name = "Font_TlStrpSprtr_01"
            Font_TlStrpSprtr_01.Size = New System.Drawing.Size(6, 23)
            Font_TlStrpCmbBx.DropDownHeight = 400
            Font_TlStrpCmbBx.DropDownWidth = 200
            Font_TlStrpCmbBx.IntegralHeight = False
            Font_TlStrpCmbBx.Name = "Font_TlStrpCmbBx"
            Font_TlStrpCmbBx.Size = New System.Drawing.Size(150, 23)
            Font_TlStrpCmbBx.Text = "Times New Roman"
            Font_TlStrpCmbBx.ToolTipText = "Change font"
            Font_Size_TlStrpCmbBx.AutoSize = False
            Font_Size_TlStrpCmbBx.Name = "Font_Size_TlStrpCmbBx"
            Font_Size_TlStrpCmbBx.Size = New System.Drawing.Size(50, 23)
            Font_Size_TlStrpCmbBx.Text = "8"
            Font_Size_TlStrpCmbBx.ToolTipText = "Change font size"
            Font_Size_TlStrpCmbBx.Items.AddRange(New Object() {"8", "9", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
            Enlarge_Font_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Enlarge_Font_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.font_larger
            Enlarge_Font_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Enlarge_Font_TlStrpBtn.Name = "Enlarge_Font_TlStrpBtn"
            Enlarge_Font_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Enlarge_Font_TlStrpBtn.Text = "Make text larger"
            Font_Smaller_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Font_Smaller_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.font_smaller
            Font_Smaller_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Font_Smaller_TlStrpBtn.Name = "Font_Smaller_TlStrpBtn"
            Font_Smaller_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Font_Smaller_TlStrpBtn.Text = "Make text smaller"
            Bold_TlStrpBtn.CheckOnClick = True
            Bold_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Bold_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.bold
            Bold_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Bold_TlStrpBtn.Margin = New System.Windows.Forms.Padding(2, 1, 0, 2)
            Bold_TlStrpBtn.Name = "Bold_TlStrpBtn"
            Bold_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Bold_TlStrpBtn.Text = "Bold"
            Italic_TlStrpBtn.CheckOnClick = True
            Italic_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Italic_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.italic
            Italic_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Italic_TlStrpBtn.Name = "Italic_TlStrpBtn"
            Italic_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Italic_TlStrpBtn.Text = "Italic"
            Under_Line_TlStrpBtn.CheckOnClick = True
            Under_Line_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Under_Line_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.underlinee
            Under_Line_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Under_Line_TlStrpBtn.Name = "Under_Line_TlStrpBtn"
            Under_Line_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Under_Line_TlStrpBtn.Text = "Underline"
            Strike_Through_TlStrpBtn.CheckOnClick = True
            Strike_Through_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Strike_Through_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.strikethrough
            Strike_Through_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Strike_Through_TlStrpBtn.Name = "Strike_Through_TlStrpBtn"
            Strike_Through_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Strike_Through_TlStrpBtn.Text = "Strikethrough"
            Font_TlStrpSprtr_02.Name = "Font_TlStrpSprtr_02"
            Font_TlStrpSprtr_02.Size = New System.Drawing.Size(6, 23)
            Text_Wrap_TlStrpBtn.CheckOnClick = True
            Text_Wrap_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Wrap_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.textwrap
            Text_Wrap_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Wrap_TlStrpBtn.Name = "Text_Wrap_TlStrpBtn"
            Text_Wrap_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Text_Wrap_TlStrpBtn.Text = "Text Wrap"
            Font_TlStrpSprtr_03.Name = "Font_TlStrpSprtr_03"
            Font_TlStrpSprtr_03.Size = New System.Drawing.Size(6, 23)
            Text_Align_Left_TlStrpBtn.CheckOnClick = True
            Text_Align_Left_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Align_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.AlignTableCellMiddleLeftJustHS
            Text_Align_Left_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Align_Left_TlStrpBtn.Name = "Text_Align_Left_TlStrpBtn"
            Text_Align_Left_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Text_Align_Left_TlStrpBtn.Text = "Text Left Align"
            Text_Align_Center_TlStrpBtn.CheckOnClick = True
            Text_Align_Center_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Align_Center_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.AlignTableCellMiddleCenterHS
            Text_Align_Center_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Align_Center_TlStrpBtn.Name = "Text_Align_Center_TlStrpBtn"
            Text_Align_Center_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Text_Align_Center_TlStrpBtn.Text = "Text Center Align"
            Text_Align_Right_TlStrpBtn.CheckOnClick = True
            Text_Align_Right_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Align_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.AlignTableCellMiddleRightHS
            Text_Align_Right_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Align_Right_TlStrpBtn.Name = "Text_Align_Right_TlStrpBtn"
            Text_Align_Right_TlStrpBtn.Size = New System.Drawing.Size(32, 24)
            Text_Align_Right_TlStrpBtn.Text = "Text Right Align"
            Distributed_Indent_TlStrpBtn.CheckOnClick = True
            Distributed_Indent_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Distributed_Indent_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.PageWidthHS
            Distributed_Indent_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Distributed_Indent_TlStrpBtn.Name = "Distributed_Indent_TlStrpBtn"
            Distributed_Indent_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Distributed_Indent_TlStrpBtn.Text = "Text Distributed Indent"
            Font_TlStrpSprtr_04.Name = "Font_TlStrpSprtr_04"
            Font_TlStrpSprtr_04.Size = New System.Drawing.Size(6, 23)
            Text_Align_Top_TlStrpBtn.CheckOnClick = True
            Text_Align_Top_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Align_Top_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.AlignLayoutTop
            Text_Align_Top_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Align_Top_TlStrpBtn.Name = "Text_Align_Top_TlStrpBtn"
            Text_Align_Top_TlStrpBtn.Size = New System.Drawing.Size(32, 24)
            Text_Align_Top_TlStrpBtn.Text = "Text Top Align"
            Text_Align_Middle_TlStrpBtn.CheckOnClick = True
            Text_Align_Middle_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Align_Middle_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.AlignLayoutMiddle
            Text_Align_Middle_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Align_Middle_TlStrpBtn.Name = "Text_Align_Middle_TlStrpBtn"
            Text_Align_Middle_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Text_Align_Middle_TlStrpBtn.Text = "Text Middle Align"
            Text_Align_Bottom_TlStrpBtn.CheckOnClick = True
            Text_Align_Bottom_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Text_Align_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.AlignLayoutBottom
            Text_Align_Bottom_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Text_Align_Bottom_TlStrpBtn.Name = "Text_Align_Bottom_TlStrpBtn"
            Text_Align_Bottom_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Text_Align_Bottom_TlStrpBtn.Text = "Text Bottom Align"
            Font_TlStrpSprtr_05.Name = "Font_TlStrpSprtr_05"
            Font_TlStrpSprtr_05.Size = New System.Drawing.Size(6, 23)
            Zoom_TlStrpCmbBx.AutoSize = False
            Zoom_TlStrpCmbBx.Name = "Zoom_TlStrpCmbBx"
            Zoom_TlStrpCmbBx.Size = New System.Drawing.Size(50, 23)
            Zoom_TlStrpCmbBx.Text = "100%"
            Zoom_TlStrpCmbBx.ToolTipText = "Grid Zoom"
            Zoom_TlStrpCmbBx.Items.AddRange(New Object() {"10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%", "110%", "120%", "130%", "140%", "150%", "160%", "170%", "180%", "190%", "200%", "250%", "400%"})
            new_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            new_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.NewDocumentHS
            new_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            new_TlStrpBtn.Name = "new_TlStrpBtn"
            new_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            new_TlStrpBtn.Text = "New"
            load_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            load_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.openHS
            load_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            load_TlStrpBtn.Name = "load_TlStrpBtn"
            load_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            load_TlStrpBtn.Text = "Open"
            Save_TlStrpBtn.CheckOnClick = True
            Save_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Save_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.saveHS
            Save_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Save_TlStrpBtn.Name = "Save_TlStrpBtn"
            Save_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Save_TlStrpBtn.Text = "Save"
            Save_By_MS_Excel_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Save_By_MS_Excel_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.SaveByExcel
            Save_By_MS_Excel_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Save_By_MS_Excel_TlStrpBtn.Name = "Save_By_MS_Excel_TlStrpBtn"
            Save_By_MS_Excel_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Save_By_MS_Excel_TlStrpBtn.Text = "Save By MS Excel"
            Print_Preview_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Print_Preview_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.PrintPreviewHS
            Print_Preview_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Print_Preview_TlStrpBtn.Name = "Print_Preview_TlStrpBtn"
            Print_Preview_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Print_Preview_TlStrpBtn.Text = "Print Preview"
            Print_Preview_TlStrpBtn.ToolTipText = "Print Preview"
            Save_TlStrpSprtr_01.Name = "Save_TlStrpSprtr_01"
            Save_TlStrpSprtr_01.Size = New System.Drawing.Size(6, 27)
            Copy_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Copy_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.CopyHS
            Copy_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Copy_TlStrpBtn.Name = "Copy_TlStrpBtn"
            Copy_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Copy_TlStrpBtn.Text = "Copy"
            Cut_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Cut_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.CutHS
            Cut_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Cut_TlStrpBtn.Name = "Cut_TlStrpBtn"
            Cut_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Cut_TlStrpBtn.Text = "Cut"
            Paste_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Paste_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.PasteHS
            Paste_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Paste_TlStrpBtn.Name = "Paste_TlStrpBtn"
            Paste_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Paste_TlStrpBtn.Text = "Paste"
            Style_Brush_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Style_Brush_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.style_brush
            Style_Brush_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Style_Brush_TlStrpBtn.Name = "Style_Brush_TlStrpBtn"
            Style_Brush_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Style_Brush_TlStrpBtn.Text = "Style Brush"
            Save_TlStrpSprtr_02.Name = "Save_TlStrpSprtr_02"
            Save_TlStrpSprtr_02.Size = New System.Drawing.Size(6, 27)
            Undo_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Undo_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.Edit_UndoHS
            Undo_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Undo_TlStrpBtn.Name = "Undo_TlStrpBtn"
            Undo_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Undo_TlStrpBtn.Text = "Undo"
            Undo_TlStrpBtn.ToolTipText = "Undo"
            Redo_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Redo_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.Edit_RedoHS
            Redo_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Redo_TlStrpBtn.Name = "Redo_TlStrpBtn"
            Redo_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Redo_TlStrpBtn.Text = "Redo"
            Save_TlStrpSprtr_03.Name = "Save_TlStrpSprtr_03"
            Save_TlStrpSprtr_03.Size = New System.Drawing.Size(6, 27)
            Cell_Border_Color_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Cell_Border_Color_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.ColorHS
            Cell_Border_Color_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Cell_Border_Color_TlStrpBtn.Name = "Cell_Border_Color_TlStrpBtn"
            Cell_Border_Color_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Cell_Border_Color_TlStrpBtn.Text = "ToolStripButton1"
            Cell_Border_Color_TlStrpBtn.ToolTipText = "Cell Borders Color"
            Cell_Border_Color_TlStrpLbl.AutoSize = False
            Cell_Border_Color_TlStrpLbl.Name = "Cell_Border_Color_TlStrpLbl"
            Cell_Border_Color_TlStrpLbl.Size = New System.Drawing.Size(87, 22)
            Cell_Border_Color_TlStrpLbl.ToolTipText = "Cell Border Color"
            Save_TlStrpSprtr_04.Name = "Save_TlStrpSprtr_04"
            Save_TlStrpSprtr_04.Size = New System.Drawing.Size(6, 27)

            '
            'Outside_Borders_TlStrpBtn
            '
            Outside_Borders_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Outside_Borders_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Outside_TlStrpBtn, Bold_Outside_TlStrpBtn, Dotted_Outside_TlStrpBtn, Dashed_Outside_TlStrpBtn})
            Outside_Borders_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.outline_solid
            Outside_Borders_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Outside_Borders_TlStrpBtn.Name = "Outside_Borders_TlStrpBtn"
            Outside_Borders_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Outside_Borders_TlStrpBtn.Text = "Outside Borders"
            '
            'Solid_Outside_TlStrpBtn
            '
            Solid_Outside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.outline_solid
            Solid_Outside_TlStrpBtn.Name = "Solid_Outside_TlStrpBtn"
            Solid_Outside_TlStrpBtn.Size = New System.Drawing.Size(161, 26)
            Solid_Outside_TlStrpBtn.Text = "Outside Solid"
            '
            'Bold_Outside_TlStrpBtn
            '
            Bold_Outside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.outline_bold
            Bold_Outside_TlStrpBtn.Name = "Bold_Outside_TlStrpBtn"
            Bold_Outside_TlStrpBtn.Size = New System.Drawing.Size(161, 26)
            Bold_Outside_TlStrpBtn.Text = "Outside Bold"
            '
            'Dotted_Outside_TlStrpBtn
            '
            Dotted_Outside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.outline_dot
            Dotted_Outside_TlStrpBtn.Name = "Dotted_Outside_TlStrpBtn"
            Dotted_Outside_TlStrpBtn.Size = New System.Drawing.Size(161, 26)
            Dotted_Outside_TlStrpBtn.Text = "Outside Dotted"
            '
            'Dashed_Outside_TlStrpBtn
            '
            Dashed_Outside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.outline_dash
            Dashed_Outside_TlStrpBtn.Name = "Dashed_Outside_TlStrpBtn"
            Dashed_Outside_TlStrpBtn.Size = New System.Drawing.Size(161, 26)
            Dashed_Outside_TlStrpBtn.Text = "Outside Dashed"
            '
            'Inside_Borders_TlStrpBtn
            '
            Inside_Borders_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Inside_Borders_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Inside_TlStrpBtn, Bold_Inside_TlStrpBtn, Dotted_Inside_TlStrpBtn, Dashed_Inside_TlStrpBtn})
            Inside_Borders_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.inside_solid
            Inside_Borders_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Inside_Borders_TlStrpBtn.Name = "Inside_Borders_TlStrpBtn"
            Inside_Borders_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Inside_Borders_TlStrpBtn.Text = "Inside Borders"
            '
            'Solid_Inside_TlStrpBtn
            '
            Solid_Inside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.inside_solid
            Solid_Inside_TlStrpBtn.Name = "Solid_Inside_TlStrpBtn"
            Solid_Inside_TlStrpBtn.Size = New System.Drawing.Size(151, 26)
            Solid_Inside_TlStrpBtn.Text = "Inside Solid"
            '
            'Bold_Inside_TlStrpBtn
            '
            Bold_Inside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.inside_bold
            Bold_Inside_TlStrpBtn.Name = "Bold_Inside_TlStrpBtn"
            Bold_Inside_TlStrpBtn.Size = New System.Drawing.Size(151, 26)
            Bold_Inside_TlStrpBtn.Text = "Inside Bold"
            '
            'Dotted_Inside_TlStrpBtn
            '
            Dotted_Inside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.inside_dot
            Dotted_Inside_TlStrpBtn.Name = "Dotted_Inside_TlStrpBtn"
            Dotted_Inside_TlStrpBtn.Size = New System.Drawing.Size(151, 26)
            Dotted_Inside_TlStrpBtn.Text = "Inside Dotted"
            '
            'Dashed_Inside_TlStrpBtn
            '
            Dashed_Inside_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.inside_dash
            Dashed_Inside_TlStrpBtn.Name = "Dashed_Inside_TlStrpBtn"
            Dashed_Inside_TlStrpBtn.Size = New System.Drawing.Size(151, 26)
            Dashed_Inside_TlStrpBtn.Text = "Inside Dashed"
            '
            'All_Borders_TlStrpBtn
            '
            All_Borders_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            All_Borders_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_All_TlStrpBtn, Bold_All_TlStrpBtn, Dotted_All_TlStrpBtn, Dashed_All_TlStrpBtn})
            All_Borders_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.all_solid
            All_Borders_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            All_Borders_TlStrpBtn.Name = "All_Borders_TlStrpBtn"
            All_Borders_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            All_Borders_TlStrpBtn.Text = "All Borders"
            '
            'Solid_All_TlStrpBtn
            '
            Solid_All_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.all_solid
            Solid_All_TlStrpBtn.Name = "Solid_All_TlStrpBtn"
            Solid_All_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Solid_All_TlStrpBtn.Text = "All Solid"
            '
            'Bold_All_TlStrpBtn
            '
            Bold_All_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.all_bold
            Bold_All_TlStrpBtn.Name = "Bold_All_TlStrpBtn"
            Bold_All_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Bold_All_TlStrpBtn.Text = "All Bold"
            '
            'Dotted_All_TlStrpBtn
            '
            Dotted_All_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.all_dot
            Dotted_All_TlStrpBtn.Name = "Dotted_All_TlStrpBtn"
            Dotted_All_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Dotted_All_TlStrpBtn.Text = "All Dotted"
            '
            'Dashed_All_TlStrpBtn
            '
            Dashed_All_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.all_dash
            Dashed_All_TlStrpBtn.Name = "Dashed_All_TlStrpBtn"
            Dashed_All_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Dashed_All_TlStrpBtn.Text = "All Dashed"
            '
            'Clear_Borders_TlStrpBtn
            '
            Clear_Borders_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Clear_Borders_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.none_border
            Clear_Borders_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Clear_Borders_TlStrpBtn.Name = "Clear_Borders_TlStrpBtn"
            Clear_Borders_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Clear_Borders_TlStrpBtn.Text = "Clear Borders"
            '
            'Save_TlStrpSprtr_05
            '
            Save_TlStrpSprtr_05.Name = "Save_TlStrpSprtr_05"
            Save_TlStrpSprtr_05.Size = New System.Drawing.Size(6, 27)
            '
            'Top_Bottom_Borders_TlStrpBtn
            '
            Top_Bottom_Borders_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Top_Bottom_Borders_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Top_Bottom_TlStrpBtn, Bold_Top_Bottom_TlStrpBtn, Doted_Top_Bottom_TlStrpBtn, Dashed_Top_Bottom_TlStrpBtn})
            Top_Bottom_Borders_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_bottom_solid
            Top_Bottom_Borders_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Top_Bottom_Borders_TlStrpBtn.Name = "Top_Bottom_Borders_TlStrpBtn"
            Top_Bottom_Borders_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Top_Bottom_Borders_TlStrpBtn.Text = "Top & Bottom Borders"
            '
            'Solid_Top_Bottom_TlStrpBtn
            '
            Solid_Top_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_bottom_solid
            Solid_Top_Bottom_TlStrpBtn.Name = "Solid_Top_Bottom_TlStrpBtn"
            Solid_Top_Bottom_TlStrpBtn.Size = New System.Drawing.Size(169, 26)
            Solid_Top_Bottom_TlStrpBtn.Text = "Top Bottom Solid"
            '
            'Bold_Top_Bottom_TlStrpBtn
            '
            Bold_Top_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_bottom_bold
            Bold_Top_Bottom_TlStrpBtn.Name = "Bold_Top_Bottom_TlStrpBtn"
            Bold_Top_Bottom_TlStrpBtn.Size = New System.Drawing.Size(169, 26)
            Bold_Top_Bottom_TlStrpBtn.Text = "Top Bottom Bold"
            '
            'Doted_Top_Bottom_TlStrpBtn
            '
            Doted_Top_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_bottom_dot
            Doted_Top_Bottom_TlStrpBtn.Name = "Doted_Top_Bottom_TlStrpBtn"
            Doted_Top_Bottom_TlStrpBtn.Size = New System.Drawing.Size(169, 26)
            Doted_Top_Bottom_TlStrpBtn.Text = "Top Bottom Dot"
            '
            'Dashed_Top_Bottom_TlStrpBtn
            '
            Dashed_Top_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_bottom_dash
            Dashed_Top_Bottom_TlStrpBtn.Name = "Dashed_Top_Bottom_TlStrpBtn"
            Dashed_Top_Bottom_TlStrpBtn.Size = New System.Drawing.Size(169, 26)
            Dashed_Top_Bottom_TlStrpBtn.Text = "Top Bottom Dash"
            '
            'Left_Right_Borders_TlStrpBtn
            '
            Left_Right_Borders_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Left_Right_Borders_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Left_Right_TlStrpBtn, Bold_Left_Right_TlStrpBtn, Doted_Left_Right_TlStrpBtn, Dashed_Left_Right_TlStrpBtn})
            Left_Right_Borders_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_right_solid
            Left_Right_Borders_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Left_Right_Borders_TlStrpBtn.Name = "Left_Right_Borders_TlStrpBtn"
            Left_Right_Borders_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Left_Right_Borders_TlStrpBtn.Text = "Left & Right Borders"
            '
            'Solid_Left_Right_TlStrpBtn
            '
            Solid_Left_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_right_solid
            Solid_Left_Right_TlStrpBtn.Name = "Solid_Left_Right_TlStrpBtn"
            Solid_Left_Right_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Solid_Left_Right_TlStrpBtn.Text = "Left Right Solid"
            '
            'Bold_Left_Right_TlStrpBtn
            '
            Bold_Left_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_right_blod
            Bold_Left_Right_TlStrpBtn.Name = "Bold_Left_Right_TlStrpBtn"
            Bold_Left_Right_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Bold_Left_Right_TlStrpBtn.Text = "Left Right Bold"
            '
            'Doted_Left_Right_TlStrpBtn
            '
            Doted_Left_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_right_dot
            Doted_Left_Right_TlStrpBtn.Name = "Doted_Left_Right_TlStrpBtn"
            Doted_Left_Right_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Doted_Left_Right_TlStrpBtn.Text = "Left Right Dot"
            '
            'Dashed_Left_Right_TlStrpBtn
            '
            Dashed_Left_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_right_dash
            Dashed_Left_Right_TlStrpBtn.Name = "Dashed_Left_Right_TlStrpBtn"
            Dashed_Left_Right_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Dashed_Left_Right_TlStrpBtn.Text = "Left Right Dash"
            '
            'Save_TlStrpSprtr_06
            '
            Save_TlStrpSprtr_06.Name = "Save_TlStrpSprtr_06"
            Save_TlStrpSprtr_06.Size = New System.Drawing.Size(6, 27)
            '
            'Top_Border_TlStrpBtn
            '
            Top_Border_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Top_Border_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Top_TlStrpBtn, Blod_Top_TlStrpBtn, Doted_Top_TlStrpBtn, Dashed_Top_TlStrpBtn})
            Top_Border_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_line_solid
            Top_Border_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Top_Border_TlStrpBtn.Name = "Top_Border_TlStrpBtn"
            Top_Border_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Top_Border_TlStrpBtn.Text = "Top Borders"
            '
            'Solid_Top_TlStrpBtn
            '
            Solid_Top_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_line_solid
            Solid_Top_TlStrpBtn.Name = "Solid_Top_TlStrpBtn"
            Solid_Top_TlStrpBtn.Size = New System.Drawing.Size(126, 26)
            Solid_Top_TlStrpBtn.Text = "Top Solid"
            '
            'Blod_Top_TlStrpBtn
            '
            Blod_Top_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_line_bold
            Blod_Top_TlStrpBtn.Name = "Blod_Top_TlStrpBtn"
            Blod_Top_TlStrpBtn.Size = New System.Drawing.Size(126, 26)
            Blod_Top_TlStrpBtn.Text = "Top Bold"
            '
            'Doted_Top_TlStrpBtn
            '
            Doted_Top_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_line_dot
            Doted_Top_TlStrpBtn.Name = "Doted_Top_TlStrpBtn"
            Doted_Top_TlStrpBtn.Size = New System.Drawing.Size(126, 26)
            Doted_Top_TlStrpBtn.Text = "Top Dot"
            '
            'Dashed_Top_TlStrpBtn
            '
            Dashed_Top_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.top_line_dash
            Dashed_Top_TlStrpBtn.Name = "Dashed_Top_TlStrpBtn"
            Dashed_Top_TlStrpBtn.Size = New System.Drawing.Size(126, 26)
            Dashed_Top_TlStrpBtn.Text = "Top Dash"
            '
            'Bottom_Border_TlStrpBtn
            '
            Bottom_Border_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Bottom_Border_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Bottom_TlStrpBtn, Bold_Bottom_TlStrpBtn, Doted_Bottom_TlStrpBtn, Dashed_Bottom_TlStrpBtn})
            Bottom_Border_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.bottom_line_solid
            Bottom_Border_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Bottom_Border_TlStrpBtn.Name = "Bottom_Border_TlStrpBtn"
            Bottom_Border_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Bottom_Border_TlStrpBtn.Text = "Bottom Borders"
            '
            'Solid_Bottom_TlStrpBtn
            '
            Solid_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.bottom_line_solid
            Solid_Bottom_TlStrpBtn.Name = "Solid_Bottom_TlStrpBtn"
            Solid_Bottom_TlStrpBtn.Size = New System.Drawing.Size(147, 26)
            Solid_Bottom_TlStrpBtn.Text = "Bottom Solid"
            '
            'Bold_Bottom_TlStrpBtn
            '
            Bold_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.bottom_line_bold
            Bold_Bottom_TlStrpBtn.Name = "Bold_Bottom_TlStrpBtn"
            Bold_Bottom_TlStrpBtn.Size = New System.Drawing.Size(147, 26)
            Bold_Bottom_TlStrpBtn.Text = "Bottom Bold"
            '
            'Doted_Bottom_TlStrpBtn
            '
            Doted_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.bottom_line_dot
            Doted_Bottom_TlStrpBtn.Name = "Doted_Bottom_TlStrpBtn"
            Doted_Bottom_TlStrpBtn.Size = New System.Drawing.Size(147, 26)
            Doted_Bottom_TlStrpBtn.Text = "Bottom Dot"
            '
            'Dashed_Bottom_TlStrpBtn
            '
            Dashed_Bottom_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.bottom_line_dash
            Dashed_Bottom_TlStrpBtn.Name = "Dashed_Bottom_TlStrpBtn"
            Dashed_Bottom_TlStrpBtn.Size = New System.Drawing.Size(147, 26)
            Dashed_Bottom_TlStrpBtn.Text = "Bottom Dash"
            '
            'Left_Border_TlStrpBtn
            '
            Left_Border_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Left_Border_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Left_TlStrpBtn, Bold_Left_TlStrpBtn, Doted_Left_TlStrpBtn, Dashed_Left_TlStrpBtn})
            Left_Border_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_line_solid
            Left_Border_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Left_Border_TlStrpBtn.Name = "Left_Border_TlStrpBtn"
            Left_Border_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Left_Border_TlStrpBtn.Text = "Left Borders"
            '
            'Solid_Left_TlStrpBtn
            '
            Solid_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_line_solid
            Solid_Left_TlStrpBtn.Name = "Solid_Left_TlStrpBtn"
            Solid_Left_TlStrpBtn.Size = New System.Drawing.Size(127, 26)
            Solid_Left_TlStrpBtn.Text = "Left Solid"
            '
            'Bold_Left_TlStrpBtn
            '
            Bold_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_line_bold
            Bold_Left_TlStrpBtn.Name = "Bold_Left_TlStrpBtn"
            Bold_Left_TlStrpBtn.Size = New System.Drawing.Size(127, 26)
            Bold_Left_TlStrpBtn.Text = "Left Bold"
            '
            'Doted_Left_TlStrpBtn
            '
            Doted_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_line_dot
            Doted_Left_TlStrpBtn.Name = "Doted_Left_TlStrpBtn"
            Doted_Left_TlStrpBtn.Size = New System.Drawing.Size(127, 26)
            Doted_Left_TlStrpBtn.Text = "Left Dot"
            '
            'Dashed_Left_TlStrpBtn
            '
            Dashed_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.left_line_dash
            Dashed_Left_TlStrpBtn.Name = "Dashed_Left_TlStrpBtn"
            Dashed_Left_TlStrpBtn.Size = New System.Drawing.Size(127, 26)
            Dashed_Left_TlStrpBtn.Text = "Left Dash"
            '
            'Right_Boder_TlStrpBtn
            '
            Right_Boder_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Right_Boder_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Right_TlStrpBtn, Bold_Right_TlStrpBtn, Doted_Right_TlStrpBtn, Dashed_Right_TlStrpBtn})
            Right_Boder_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.right_line_solid
            Right_Boder_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Right_Boder_TlStrpBtn.Name = "Right_Boder_TlStrpBtn"
            Right_Boder_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Right_Boder_TlStrpBtn.Text = "Right Borders"
            '
            'Solid_Right_TlStrpBtn
            '
            Solid_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.right_line_solid
            Solid_Right_TlStrpBtn.Name = "Solid_Right_TlStrpBtn"
            Solid_Right_TlStrpBtn.Size = New System.Drawing.Size(135, 26)
            Solid_Right_TlStrpBtn.Text = "Right Solid"
            '
            'Bold_Right_TlStrpBtn
            '
            Bold_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.right_line_bold
            Bold_Right_TlStrpBtn.Name = "Bold_Right_TlStrpBtn"
            Bold_Right_TlStrpBtn.Size = New System.Drawing.Size(135, 26)
            Bold_Right_TlStrpBtn.Text = "Right Bold"
            '
            'Doted_Right_TlStrpBtn
            '
            Doted_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.right_line_dot
            Doted_Right_TlStrpBtn.Name = "Doted_Right_TlStrpBtn"
            Doted_Right_TlStrpBtn.Size = New System.Drawing.Size(135, 26)
            Doted_Right_TlStrpBtn.Text = "Right Dot"
            '
            'Dashed_Right_TlStrpBtn
            '
            Dashed_Right_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.right_line_dash
            Dashed_Right_TlStrpBtn.Name = "Dashed_Right_TlStrpBtn"
            Dashed_Right_TlStrpBtn.Size = New System.Drawing.Size(135, 26)
            Dashed_Right_TlStrpBtn.Text = "Right Dash"



            Save_TlStrpSprtr_08.Name = "Save_TlStrpSprtr_08"
            Save_TlStrpSprtr_08.Size = New System.Drawing.Size(6, 27)
            Cell_Merge_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Cell_Merge_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.cell_merge
            Cell_Merge_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Cell_Merge_TlStrpBtn.Name = "Cell_Merge_TlStrpBtn"
            Cell_Merge_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Cell_Merge_TlStrpBtn.Text = "Merge"
            Unmerge_Range_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Unmerge_Range_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.cell_unmerge
            Unmerge_Range_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Unmerge_Range_TlStrpBtn.Name = "Unmerge_Range_TlStrpBtn"
            Unmerge_Range_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
            Unmerge_Range_TlStrpBtn.Text = "Unmerge"

            Dim Font_TlStrp = New System.Windows.Forms.ToolStrip()
            Font_TlStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
            Font_TlStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Cell_Back_Color_TlStrpBtn, Text_Color_TlStrpLbl, Cell_Text_Color_TlStrpBtn, Cell_BackColor_TlStrpLbl, Font_TlStrpSprtr_01, Font_TlStrpCmbBx, Font_Size_TlStrpCmbBx, Enlarge_Font_TlStrpBtn, Font_Smaller_TlStrpBtn, Bold_TlStrpBtn, Italic_TlStrpBtn, Under_Line_TlStrpBtn, Strike_Through_TlStrpBtn, Font_TlStrpSprtr_02, Text_Wrap_TlStrpBtn, Font_TlStrpSprtr_03, Text_Align_Left_TlStrpBtn, Text_Align_Center_TlStrpBtn, Text_Align_Right_TlStrpBtn, Distributed_Indent_TlStrpBtn, Font_TlStrpSprtr_04, Text_Align_Top_TlStrpBtn, Text_Align_Middle_TlStrpBtn, Text_Align_Bottom_TlStrpBtn, Font_TlStrpSprtr_05, Zoom_TlStrpCmbBx})
            Font_TlStrp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
            Font_TlStrp.Location = New System.Drawing.Point(0, 27)
            Font_TlStrp.Name = "Font_TlStrp"
            Font_TlStrp.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
            Font_TlStrp.Size = New System.Drawing.Size(908, 27)
            Font_TlStrp.TabIndex = 3
            Font_TlStrp.Text = "toolStrip2"

            Dim SaveAndBorder_TlStrp = New System.Windows.Forms.ToolStrip()
            SaveAndBorder_TlStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
            SaveAndBorder_TlStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {new_TlStrpBtn, load_TlStrpBtn, Save_TlStrpBtn, Save_By_MS_Excel_TlStrpBtn, Print_Preview_TlStrpBtn, Save_TlStrpSprtr_01, Copy_TlStrpBtn, Cut_TlStrpBtn, Paste_TlStrpBtn, Style_Brush_TlStrpBtn, Save_TlStrpSprtr_02, Undo_TlStrpBtn, Redo_TlStrpBtn, Save_TlStrpSprtr_03, Cell_Border_Color_TlStrpBtn, Cell_Border_Color_TlStrpLbl, Save_TlStrpSprtr_04, Outside_Borders_TlStrpBtn, Inside_Borders_TlStrpBtn, All_Borders_TlStrpBtn, Clear_Borders_TlStrpBtn, Save_TlStrpSprtr_05, Top_Bottom_Borders_TlStrpBtn, Left_Right_Borders_TlStrpBtn, Save_TlStrpSprtr_06, Top_Border_TlStrpBtn, Bottom_Border_TlStrpBtn, Left_Border_TlStrpBtn, Right_Boder_TlStrpBtn, Save_TlStrpSprtr_07, Right_Slash_TlStrpBtn, Slash_Left_TlStrpBtn, Save_TlStrpSprtr_08, Cell_Merge_TlStrpBtn, Unmerge_Range_TlStrpBtn, Save_TlStrpSprtr_09})
            SaveAndBorder_TlStrp.Location = New System.Drawing.Point(0, 0)
            SaveAndBorder_TlStrp.Name = "SaveAndBorder_TlStrp"
            SaveAndBorder_TlStrp.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
            SaveAndBorder_TlStrp.Size = New System.Drawing.Size(908, 27)
            SaveAndBorder_TlStrp.TabIndex = 35
            SaveAndBorder_TlStrp.Text = "SaveAndBorder_TlStrp"

            '
            'Cell_Border_Color_ClrCmbBx
            '
            Cell_Border_Color_ClrCmbBx.FormattingEnabled = True
            Cell_Border_Color_ClrCmbBx.IncludeSystemColors = True
            Cell_Border_Color_ClrCmbBx.IncludeTransparent = True
            Cell_Border_Color_ClrCmbBx.Location = New System.Drawing.Point(315, 5)
            Cell_Border_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Cell_Border_Color_ClrCmbBx.Name = "Cell_Border_Color_ClrCmbBx"
            Cell_Border_Color_ClrCmbBx.Size = New System.Drawing.Size(86, 21)
            Cell_Border_Color_ClrCmbBx.TabIndex = 37
            '
            'Cell_BackColor_ClrCmbBx
            '
            Cell_BackColor_ClrCmbBx.FormattingEnabled = True
            Cell_BackColor_ClrCmbBx.IncludeSystemColors = True
            Cell_BackColor_ClrCmbBx.IncludeTransparent = True
            Cell_BackColor_ClrCmbBx.Location = New System.Drawing.Point(21, 32)
            Cell_BackColor_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Cell_BackColor_ClrCmbBx.Name = "Cell_BackColor_ClrCmbBx"
            Cell_BackColor_ClrCmbBx.Size = New System.Drawing.Size(86, 21)
            Cell_BackColor_ClrCmbBx.TabIndex = 36
            '
            'Cell_Text_Color_ClrCmbBx
            '
            Cell_Text_Color_ClrCmbBx.FormattingEnabled = True
            Cell_Text_Color_ClrCmbBx.IncludeSystemColors = True
            Cell_Text_Color_ClrCmbBx.IncludeTransparent = True
            Cell_Text_Color_ClrCmbBx.Location = New System.Drawing.Point(134, 32)
            Cell_Text_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Cell_Text_Color_ClrCmbBx.Name = "Cell_Text_Color_ClrCmbBx"
            Cell_Text_Color_ClrCmbBx.Size = New System.Drawing.Size(86, 21)
            Cell_Text_Color_ClrCmbBx.TabIndex = 38

            '
            'Right_Slash_TlStrpBtn
            '
            Right_Slash_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Right_Slash_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Right_Slash_TlStrpBtn, Bold_Right_Slash_TlStrpBtn, Doted_Right_Slash_TlStrpBtn, Dashed_Right_Slash_TlStrpBtn})
            Right_Slash_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_right_solid
            Right_Slash_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Right_Slash_TlStrpBtn.Name = "Right_Slash_TlStrpBtn"
            Right_Slash_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Right_Slash_TlStrpBtn.Text = "Slash"
            '
            'Solid_Right_Slash_TlStrpBtn
            '
            Solid_Right_Slash_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_right_solid
            Solid_Right_Slash_TlStrpBtn.Name = "Solid_Right_Slash_TlStrpBtn"
            Solid_Right_Slash_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Solid_Right_Slash_TlStrpBtn.Text = "Slash Solid"
            '
            'Bold_Right_Slash_TlStrpBtn
            '
            Bold_Right_Slash_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_right_blod
            Bold_Right_Slash_TlStrpBtn.Name = "Bold_Right_Slash_TlStrpBtn"
            Bold_Right_Slash_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Bold_Right_Slash_TlStrpBtn.Text = "Slash Bold"
            '
            'Doted_Right_Slash_TlStrpBtn
            '
            Doted_Right_Slash_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_right_dot
            Doted_Right_Slash_TlStrpBtn.Name = "Doted_Right_Slash_TlStrpBtn"
            Doted_Right_Slash_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Doted_Right_Slash_TlStrpBtn.Text = "Slash Dot"
            '
            'Dashed_Right_Slash_TlStrpBtn
            '
            Dashed_Right_Slash_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_right_dash
            Dashed_Right_Slash_TlStrpBtn.Name = "Dashed_Right_Slash_TlStrpBtn"
            Dashed_Right_Slash_TlStrpBtn.Size = New System.Drawing.Size(134, 26)
            Dashed_Right_Slash_TlStrpBtn.Text = "Slash Dash"
            '
            'Slash_Left_TlStrpBtn
            '
            Slash_Left_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Slash_Left_TlStrpBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Solid_Slash_Left_TlStrpBtn, Bold_Slash_Left_TlStrpBtn, Doted_Slash_Left_TlStrpBtn, Dashed_Slash_Left_TlStrpBtn})
            Slash_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_left_solid
            Slash_Left_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
            Slash_Left_TlStrpBtn.Name = "Slash_Left_TlStrpBtn"
            Slash_Left_TlStrpBtn.Size = New System.Drawing.Size(36, 24)
            Slash_Left_TlStrpBtn.Text = "Backslash"
            '
            'Solid_Slash_Left_TlStrpBtn
            '
            Solid_Slash_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_left_solid
            Solid_Slash_Left_TlStrpBtn.Name = "Solid_Slash_Left_TlStrpBtn"
            Solid_Slash_Left_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Solid_Slash_Left_TlStrpBtn.Text = "Backslash Solid"
            '
            'Bold_Slash_Left_TlStrpBtn
            '
            Bold_Slash_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_left_blod
            Bold_Slash_Left_TlStrpBtn.Name = "Bold_Slash_Left_TlStrpBtn"
            Bold_Slash_Left_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Bold_Slash_Left_TlStrpBtn.Text = "Backslash Bold"
            '
            'Doted_Slash_Left_TlStrpBtn
            '
            Doted_Slash_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_left_dot
            Doted_Slash_Left_TlStrpBtn.Name = "Doted_Slash_Left_TlStrpBtn"
            Doted_Slash_Left_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Doted_Slash_Left_TlStrpBtn.Text = "Backslash Dot"
            '
            'Dashed_Slash_Left_TlStrpBtn
            '
            Dashed_Slash_Left_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.slash_left_solid
            Dashed_Slash_Left_TlStrpBtn.Name = "Dashed_Slash_Left_TlStrpBtn"
            Dashed_Slash_Left_TlStrpBtn.Size = New System.Drawing.Size(158, 26)
            Dashed_Slash_Left_TlStrpBtn.Text = "Backslash Dash"

            GridPnl.Controls.Add(Cell_Border_Color_ClrCmbBx)
            GridPnl.Controls.Add(Cell_BackColor_ClrCmbBx)
            GridPnl.Controls.Add(Cell_Text_Color_ClrCmbBx)
            GridPnl.Controls.Add(Grid)
            GridPnl.Controls.Add(Font_TlStrp)
            GridPnl.Controls.Add(SaveAndBorder_TlStrp)

            AddHandler Grid.CurrentWorksheet.SelectionRangeChanged, AddressOf grid_SelectionRangeChanged
            AddHandler User_Escalation_RchTxtBx.GotFocus, AddressOf RchTxtBx_GotFocus
            AddHandler Cell_Border_Color_ClrCmbBx.SelectedIndexChanged, AddressOf ColorComboBox_SelectedIndexChanged
            AddHandler Cell_BackColor_ClrCmbBx.SelectedIndexChanged, AddressOf ColorComboBox_SelectedIndexChanged
            AddHandler Cell_Text_Color_ClrCmbBx.SelectedIndexChanged, AddressOf ColorComboBox_SelectedIndexChanged
            AddHandler Font_Size_TlStrpCmbBx.SelectedIndexChanged, AddressOf Font_TlStrpCmbBx_SelectedIndexChanged
            AddHandler Font_Size_TlStrpCmbBx.TextChanged, AddressOf Font_Size_TlStrpCmbBx_TextChanged
            AddHandler Zoom_TlStrpCmbBx.TextChanged, AddressOf Zoom_TlStrpCmbBx_TextChanged
            AddHandler Font_Smaller_TlStrpBtn.Click, AddressOf Font_Smaller_TlStrpBtn_Click
            AddHandler Enlarge_Font_TlStrpBtn.Click, AddressOf Enlarge_Font_TlStrpBtn_Click

            AddHandler Outside_Borders_TlStrpBtn.ButtonClick, AddressOf Outside_Borders_TlStrpBtn_ButtonClick

            If Note_Font_Name_CmbBx.Items.Count = 0 Then
                Font_TlStrpCmbBx.ComboBox.ValueMember = "Key"
                Font_TlStrpCmbBx.ComboBox.DisplayMember = "Value"
                For Each oFont As FontFamily In FontFamily.Families
                    Font_TlStrpCmbBx.Items.Add(New KeyValuePair(Of String, String)(oFont.Name, oFont.Name))
                Next
                Font_TlStrpCmbBx.Text = "Times New Roman"
            End If
            AddHandler Cell_Merge_TlStrpBtn.Click, AddressOf Cell_Merge_Range_TlStrpMnItm_Click
            AddHandler Unmerge_Range_TlStrpBtn.Click, AddressOf Cell_UnMerge_Range_TlStrpMnItm_Click
            AddHandler Solid_Outside_TlStrpBtn.Click, AddressOf Solid_Outside_TlStrpBtn_Click
            AddHandler Bold_Outside_TlStrpBtn.Click, AddressOf Bold_Outside_TlStrpBtn_Click
            AddHandler Dotted_Outside_TlStrpBtn.Click, AddressOf Dotted_Outside_TlStrpBtn_Click
            AddHandler Dashed_Outside_TlStrpBtn.Click, AddressOf Dashed_Outside_TlStrpBtn_Click
            AddHandler Solid_Inside_TlStrpBtn.Click, AddressOf Solid_Inside_TlStrpBtn_Click
            AddHandler Bold_Inside_TlStrpBtn.Click, AddressOf Bold_Inside_TlStrpBtn_Click
            AddHandler Dotted_Inside_TlStrpBtn.Click, AddressOf Dotted_Inside_TlStrpBtn_Click
            AddHandler Dashed_Inside_TlStrpBtn.Click, AddressOf Dashed_Inside_TlStrpBtn_Click
            AddHandler Solid_All_TlStrpBtn.Click, AddressOf Solid_All_TlStrpBtn_Click
            AddHandler Bold_All_TlStrpBtn.Click, AddressOf Bold_All_TlStrpBtn_Click
            AddHandler Dotted_All_TlStrpBtn.Click, AddressOf Dotted_All_TlStrpBtn_Click
            AddHandler Dashed_All_TlStrpBtn.Click, AddressOf Dashed_All_TlStrpBtn_Click
            AddHandler Clear_Borders_TlStrpBtn.Click, AddressOf Clear_Borders_TlStrpBtn_Click
            AddHandler Solid_Top_Bottom_TlStrpBtn.Click, AddressOf Solid_Top_Bottom_TlStrpBtn_Click
            AddHandler Bold_Top_Bottom_TlStrpBtn.Click, AddressOf Bold_Top_Bottom_TlStrpBtn_Click
            AddHandler Doted_Top_Bottom_TlStrpBtn.Click, AddressOf Doted_Top_Bottom_TlStrpBtn_Click
            AddHandler Dashed_Top_Bottom_TlStrpBtn.Click, AddressOf Dashed_Top_Bottom_TlStrpBtn_Click
            AddHandler Solid_Left_Right_TlStrpBtn.Click, AddressOf Solid_Left_Right_TlStrpBtn_Click
            AddHandler Bold_Left_Right_TlStrpBtn.Click, AddressOf Bold_Left_Right_TlStrpBtn_Click
            AddHandler Doted_Left_Right_TlStrpBtn.Click, AddressOf Doted_Left_Right_TlStrpBtn_Click
            AddHandler Dashed_Left_Right_TlStrpBtn.Click, AddressOf Dashed_Left_Right_TlStrpBtn_Click
            AddHandler Solid_Top_TlStrpBtn.Click, AddressOf Solid_Top_TlStrpBtn_Click
            AddHandler Blod_Top_TlStrpBtn.Click, AddressOf Blod_Top_TlStrpBtn_Click
            AddHandler Doted_Top_TlStrpBtn.Click, AddressOf Doted_Top_TlStrpBtn_Click
            AddHandler Dashed_Top_TlStrpBtn.Click, AddressOf Dashed_Top_TlStrpBtn_Click
            AddHandler Solid_Bottom_TlStrpBtn.Click, AddressOf Solid_Bottom_TlStrpBtn_Click
            AddHandler Bold_Bottom_TlStrpBtn.Click, AddressOf Bold_Bottom_TlStrpBtn_Click
            AddHandler Doted_Bottom_TlStrpBtn.Click, AddressOf Doted_Bottom_TlStrpBtn_Click
            AddHandler Dashed_Bottom_TlStrpBtn.Click, AddressOf Dashed_Bottom_TlStrpBtn_Click
            AddHandler Solid_Left_TlStrpBtn.Click, AddressOf Solid_Left_TlStrpBtn_Click
            AddHandler Bold_Left_TlStrpBtn.Click, AddressOf Bold_Left_TlStrpBtn_Click
            AddHandler Doted_Left_TlStrpBtn.Click, AddressOf Doted_Left_TlStrpBtn_Click
            AddHandler Dashed_Left_TlStrpBtn.Click, AddressOf Dashed_Left_TlStrpBtn_Click
            AddHandler Solid_Right_TlStrpBtn.Click, AddressOf Solid_Right_TlStrpBtn_Click
            AddHandler Bold_Right_TlStrpBtn.Click, AddressOf Bold_Right_TlStrpBtn_Click
            AddHandler Doted_Right_TlStrpBtn.Click, AddressOf Doted_Right_TlStrpBtn_Click
            AddHandler Dashed_Right_TlStrpBtn.Click, AddressOf Dashed_Right_TlStrpBtn_Click
            AddHandler Undo_TlStrpBtn.Click, AddressOf Undo
            AddHandler Redo_TlStrpBtn.Click, AddressOf Redo
            AddHandler Style_Brush_TlStrpBtn.Click, AddressOf Style_Brush_TlStrpBtn_Click_1
            AddHandler Paste_TlStrpBtn.Click, AddressOf Paste_TlStrpBtn_Click
            AddHandler Cut_TlStrpBtn.Click, AddressOf Cut_TlStrpBtn_Click
            AddHandler Copy_TlStrpBtn.Click, AddressOf Copy_TlStrpBtn_Click
            AddHandler Print_Preview_TlStrpBtn.Click, AddressOf Print_Preview_TlStrpBtn_Click
            AddHandler Save_By_MS_Excel_TlStrpBtn.Click, AddressOf Save_By_MS_Excel_TlStrpBtn_Click
            AddHandler Save_TlStrpBtn.Click, AddressOf Save_TlStrpBtn_Click
            AddHandler load_TlStrpBtn.Click, AddressOf load_TlStrpBtn_Click
            AddHandler new_TlStrpBtn.Click, AddressOf new_TlStrpBtn_Click
            AddHandler Text_Align_Bottom_TlStrpBtn.Click, AddressOf Text_Align_Bottom_TlStrpBtn_Click
            AddHandler Text_Align_Middle_TlStrpBtn.Click, AddressOf Text_Align_Middle_TlStrpBtn_Click
            AddHandler Text_Align_Top_TlStrpBtn.Click, AddressOf Text_Align_Top_TlStrpBtn_Click
            AddHandler Distributed_Indent_TlStrpBtn.Click, AddressOf Distributed_Indent_TlStrpBtn_Click
            AddHandler Text_Align_Right_TlStrpBtn.Click, AddressOf Text_Align_Right_TlStrpBtn_Click
            AddHandler Text_Align_Center_TlStrpBtn.Click, AddressOf Text_Align_Center_TlStrpBtn_Click
            AddHandler Text_Align_Left_TlStrpBtn.Click, AddressOf Text_Align_Left_TlStrpBtn_Click
            AddHandler Text_Wrap_TlStrpBtn.Click, AddressOf Text_Wrap_TlStrpBtn_Click
            AddHandler Strike_Through_TlStrpBtn.Click, AddressOf Strike_Through_TlStrpBtn_Click
            AddHandler Italic_TlStrpBtn.Click, AddressOf Italic_TlStrpBtn_Click
            AddHandler Bold_TlStrpBtn.Click, AddressOf Bold_TlStrpBtn_Click
            AddHandler Font_Smaller_TlStrpBtn.Click, AddressOf Font_Smaller_TlStrpBtn_Click
            AddHandler Enlarge_Font_TlStrpBtn.Click, AddressOf Enlarge_Font_TlStrpBtn_Click
            AddHandler Style_Brush_TlStrpBtn.Click, AddressOf Style_Brush_TlStrpBtn_Click_1
            AddHandler Right_Slash_TlStrpBtn.Click, AddressOf Right_Slash_TlStrpBtn_ButtonClick
            AddHandler Solid_Right_Slash_TlStrpBtn.Click, AddressOf Solid_Right_Slash_TlStrpBtn_Click
            AddHandler Bold_Right_Slash_TlStrpBtn.Click, AddressOf Bold_Right_Slash_TlStrpBtn_Click
            AddHandler Doted_Right_Slash_TlStrpBtn.Click, AddressOf Doted_Right_Slash_TlStrpBtn_Click
            AddHandler Dashed_Right_Slash_TlStrpBtn.Click, AddressOf Dashed_Right_Slash_TlStrpBtn_Click
            AddHandler Slash_Left_TlStrpBtn.Click, AddressOf Slash_Left_TlStrpBtn_ButtonClick
            AddHandler Solid_Slash_Left_TlStrpBtn.Click, AddressOf Solid_Slash_Left_TlStrpBtn_Click
            AddHandler Bold_Slash_Left_TlStrpBtn.Click, AddressOf Bold_Slash_Left_TlStrpBtn_Click
            AddHandler Doted_Slash_Left_TlStrpBtn.Click, AddressOf Doted_Slash_Left_TlStrpBtn_Click
            AddHandler Dashed_Slash_Left_TlStrpBtn.Click, AddressOf Dashed_Slash_Left_TlStrpBtn_Click
            AddHandler GridPnl.SizeChanged, AddressOf Grid_Panel_SizeChanged
            Cell_Border_Color_ClrCmbBx.Visible = True
            Cell_BackColor_ClrCmbBx.Visible = True
            Cell_Text_Color_ClrCmbBx.Visible = True

            If Grid_Panel_Size_TxtBx.TextLength > 0 Then
                Dim GPSize = ReturnSize(Grid_Panel_Size_TxtBx.Text)
                GridPnl.Size = GPSize
            End If
            Grid.Visible = True
            Font_TlStrp.Visible = True
            SaveAndBorder_TlStrp.Visible = True
            AddSpliter(GridPnl)
            Return Grid
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            RchTxtBx.SendToBack()
        End Try
    End Function
    Private Function SetGridSize() As Boolean
        If Not IsNothing(GridPnl(0)) And Grid_Panel_Size_TxtBx.TextLength > 0 Then
            GridPnl(0).Size = ReturnSize(Grid_Panel_Size_TxtBx.Text)
        ElseIf Not IsNothing(GridPnl(0)) And Grid_Panel_Size_TxtBx.TextLength = 0 Then
            Grid_Panel_Size_TxtBx.Text = GridPnl(0).Size.ToString
        End If
    End Function

    Private Function ReturnSize(ByVal Size_TxtBx As String) As Size
        If String.IsNullOrEmpty(Size_TxtBx) Then Return Nothing
        Return New Point(Split(Replace(Replace(Replace(Size_TxtBx, "{Width=", ""), " Height=", ""), "}", ""), ",").ToList(0), Split(Replace(Replace(Replace(Size_TxtBx, "{Width=", ""), " Height=", ""), "}", ""), ",").ToList(1))
    End Function
    Private Sub Setting_TbCntrl_Size_Lbl_Click(sender As Object, e As EventArgs) Handles Setting_Tab_Control_Size_Lbl.Click

    End Sub


    Private Sub MagNote_Form_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Not Tray Is Nothing Then
            Tray.Visible = False
            Tray.Dispose()
            Tray = Nothing
        End If
    End Sub

    Private Sub Show_Hide_MagNotes_Notes_TabControl_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Show_Hide_MagNotes_Notes_TabControl_TlStrpMnItm.Click
        If MagNotes_Notes_TbCntrl.Visible Then
            MagNotes_Notes_TbCntrl.Visible = False
        Else
            MagNotes_Notes_TbCntrl.Visible = True
        End If
        Adjust_Showing_Form()
        Spliter_1_Lbl.BringToFront()
    End Sub


    Private Sub Setting_TbCntrl_SizeChanged(sender As Object, e As EventArgs) Handles Setting_TbCntrl.SizeChanged
        Setting_Tab_Control_Size_TxtBx.Text = Setting_TbCntrl.Size.ToString
    End Sub
    Private Sub Grid_Panel_SizeChanged(sender As Object, e As EventArgs)
        Grid_Panel_Size_TxtBx.Text = sender.Size.ToString
        Application.DoEvents()
    End Sub
    Private Sub Shortcuts_LstVw_AfterLabelEdit(sender As Object, e As LabelEditEventArgs)
        If ActiveControl.Name = sender.name Then
            RCSN.Focus()
            Application.DoEvents()
            SaveList(sender.parent.name)
        End If
    End Sub
    Dim DragFromListview As ListView
    Private Sub Shortcuts_LstVw_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs)
        Try
            DragFromListview = sender
            ' Create a DataObject that holds the ListViewItem.
            sender.DoDragDrop(New DataObject("System.Windows.Forms.ListViewItem", sender.SelectedItems), DragDropEffects.Move)
        Finally
        End Try
    End Sub

    Private Sub Show_Btn_Click(sender As Object, e As EventArgs) Handles Show_Btn.Click
        If Debugger.IsAttached Then
            Note_Password_TxtBx.UseSystemPasswordChar = False
            Note_Password_TxtBx.PasswordChar = Nothing
        End If
    End Sub

    Private Sub Shortcuts_LstVw_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        ' Check that a ListViewItem is being passed.
        Dim xx = e.Data 'System.Windows.Forms.DataObject
        If e.Data.GetDataPresent(GetType(System.Windows.Forms.ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        ElseIf e.Data.GetDataPresent(GetType(Object())) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub Shortcuts_LstVw_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        Try
            If e.Data.GetDataPresent(GetType(System.Windows.Forms.ListViewItem)) Then
                Dim breakfast As ListView.SelectedListViewItemCollection = DragFromListview.SelectedItems
                For Each item In breakfast
                    DragFromListview.Items.Remove(item)
                    CType(sender, ListView).Items.Add(item)
                Next
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Sub Shortcuts_LstVw_DragOver(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(GetType(System.Windows.Forms.ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub
    Dim wmp As New WindowsMediaPlayer
    Dim PlayAlert As Boolean
    Private Sub Play_Voice_Azan_File_Btn_Click(sender As Object, e As EventArgs) Handles Play_Voice_Azan_File_Btn.Click
        Try
            If Force_Activate_TabPage_When_Alert_Is_Active_ChkBx.CheckState = CheckState.Checked Then
                MyFormWindowState()
                Me.BringToFront()
            End If
            Select Case wmp.playState
                Case WMPPlayState.wmppsUndefined'0
                Case WMPPlayState.wmppsStopped'1
                Case WMPPlayState.wmppsPaused'2
                Case WMPPlayState.wmppsPlaying'3
                Case WMPPlayState.wmppsScanForward'4
                Case WMPPlayState.wmppsScanReverse'5
                Case WMPPlayState.wmppsBuffering'6
                Case WMPPlayState.wmppsWaiting'7
                Case WMPPlayState.wmppsMediaEnded'8
                Case WMPPlayState.wmppsTransitioning'9
                Case WMPPlayState.wmppsReady'10
                Case WMPPlayState.wmppsReconnecting'11
                Case WMPPlayState.wmppsLast '12
            End Select
            Try
                If Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState = CheckState.Checked Then
                    If Force_Stop_Playing_Current_File_ChkBx.CheckState = CheckState.Unchecked Then
                        wmp.close()
                    End If
                End If
            Catch ex As Exception
            End Try
            If Not String.IsNullOrEmpty(wmp.playState) Then
                If wmp.playState <> WMPPlayState.wmppsPlaying Then
                    wmp.close()
                Else
                    If Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState = CheckState.Checked Then
                        If Force_Stop_Playing_Current_File_ChkBx.CheckState = CheckState.Unchecked Then
                            wmp.close()
                        End If
                    Else
                        While wmp.playState = WMPPlayState.wmppsPlaying
                            Application.DoEvents()
                        End While
                    End If
                End If
            End If
            Dim SelectedFileToPlay
            If PlayAlert Then
                SelectedFileToPlay = Alert_File_Path_TxtBx.Text
            ElseIf Azan_Takbeer_Only_ChkBx.CheckState = CheckState.Checked Then
                SelectedFileToPlay = Application.StartupPath & "\Azan Voices\Takbeer Only\azan-takber.mp3"
            Else
                SelectedFileToPlay = DirectCast(Voice_Azan_Files_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            End If
            wmp.URL() = SelectedFileToPlay
            wmp.controls.play()
            Do While wmp.playState = WMPLib.WMPPlayState.wmppsPlaying Or
                 wmp.playState = WMPLib.WMPPlayState.wmppsTransitioning
                Application.DoEvents()
                System.Threading.Thread.Sleep(100)
            Loop
            Using RWAWT As New RefreshWaitAWhileTitles_Class
                RWAWT.WaitAWhile()
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            PlayAlert = False
        End Try
    End Sub

    Private Sub Stop_Voice_Azan_File_Btn_Click(sender As Object, e As EventArgs) Handles Stop_Voice_Azan_File_Btn.Click, Stop_Fagr_Voice_File_Btn.Click, Stop_Playing_Alert_Btn.Click
        Try
            wmp.controls.stop()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Azan_Spoke_Method_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Azan_Spoke_Method_ChkBx.CheckedChanged

    End Sub

    Private Sub ShortCut_TbCntrl_DragOver(sender As Object, e As DragEventArgs) Handles ShortCut_TbCntrl.DragOver
        Dim pos As Point = ShortCut_TbCntrl.PointToClient(Control.MousePosition)
        For ix As Integer = 0 To ShortCut_TbCntrl.TabCount - 1
            If ShortCut_TbCntrl.GetTabRect(ix).Contains(pos) Then
                ShortCut_TbCntrl.SelectedIndex = ix
                Exit For
            End If
        Next
    End Sub

    Private Sub Play_Fagr_Voice_File_Btn_Click(sender As Object, e As EventArgs) Handles Play_Fagr_Voice_File_Btn.Click
        Try
            If Force_Activate_TabPage_When_Alert_Is_Active_ChkBx.CheckState = CheckState.Checked Then
                MyFormWindowState()
                Me.BringToFront()
            End If
            If Not String.IsNullOrEmpty(wmp.playState) Then
                If wmp.playState <> WMPPlayState.wmppsPlaying Then
                    wmp.close()
                Else
                    Exit Sub
                End If
            End If
            Dim SelectedFileToPlay
            If PlayAlert Then
                SelectedFileToPlay = Fagr_Voice_Files_CmbBx.Text
            ElseIf Azan_Takbeer_Only_ChkBx.CheckState = CheckState.Checked Then
                SelectedFileToPlay = Application.StartupPath & "\Azan Voices\Takbeer Only\azan-takber.mp3"
            Else
                SelectedFileToPlay = DirectCast(Fagr_Voice_Files_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            End If
            wmp.URL() = SelectedFileToPlay
            wmp.controls.play()
            Do While wmp.playState = WMPLib.WMPPlayState.wmppsPlaying Or
                 wmp.playState = WMPLib.WMPPlayState.wmppsTransitioning
                Application.DoEvents()
                System.Threading.Thread.Sleep(100)
            Loop
            Using RWAWT As New RefreshWaitAWhileTitles_Class
                RWAWT.WaitAWhile()
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            PlayAlert = False
        End Try
    End Sub
    Dim PlayingAzan As Boolean
    Private Sub Azan_Tmr_Tick(sender As Object, e As EventArgs) Handles Azan_Tmr.Tick
        Try
            If Azan_Activation_ChkBx.CheckState = CheckState.Unchecked Then
                Azan_Tmr.Stop()
            End If
            Dim PrayName = CalcTimeLeft(1)
            If Split(PrayName, ",").ToList(0) = "Sunrise" Then
                Exit Sub
            End If
            If IsNothing(PrayName) Or PrayName = "," Then Exit Sub
            If Alert_Before_Azan_ChkBx.CheckState = CheckState.Checked And
                Time_To_Alert_Before_Azan_NmrcUpDn.Value > 0 Then
                If ValedAlertBeforeAzan(PrayName) Then Exit Sub
            End If
            If Convert.ToDateTime(Fajr_TxtBx.Text).TimeOfDay >= Now.TimeOfDay Or
                Convert.ToDateTime(Dhuhr_TxtBx.Text).TimeOfDay >= Now.TimeOfDay Or
                Convert.ToDateTime(Asr_TxtBx.Text).TimeOfDay >= Now.TimeOfDay Or
                Convert.ToDateTime(Maghrib_TxtBx.Text).TimeOfDay >= Now.TimeOfDay Or
                Convert.ToDateTime(Isha_TxtBx.Text).TimeOfDay >= Now.TimeOfDay Then
                If Split(PrayName, ",").ToList(1) >= "00:00:00" And
                         Split(PrayName, ",").ToList(1) <= "00:00:03" Then
                Else
                    PlayingAzan = False
                    Exit Sub
                End If
                If PlayingAzan Then Exit Sub
                wmp.close()
                PlayingAzan = True
                For Each TbPg In Setting_TbCntrl.TabPages
                    If TbPg.name = "Prayer_Time_TbPg" Then
                        Setting_TbCntrl.SelectedTab = TbPg
                        Exit For
                    End If
                Next
                If Split(PrayName, ",").ToList(0) = "Fajr" Then
                    If Azan_Spoke_Method_ChkBx.CheckState = CheckState.Unchecked Then
                        If Fagr_Voice_Files_CmbBx.SelectedIndex = -1 Then
                            Fagr_Voice_Files_CmbBx.SelectedIndex = 0
                        ElseIf Fagr_Voice_Files_CmbBx.SelectedIndex = (Fagr_Voice_Files_CmbBx.Items.Count - 1) Then
                            Fagr_Voice_Files_CmbBx.SelectedIndex = 0
                        Else
                            Fagr_Voice_Files_CmbBx.SelectedIndex += 1
                        End If
                        Play_Fagr_Voice_File_Btn.PerformClick()
                    ElseIf Azan_Spoke_Method_ChkBx.CheckState = CheckState.Checked Then
                        Play_Fagr_Voice_File_Btn.PerformClick()
                    ElseIf Azan_Spoke_Method_ChkBx.CheckState = CheckState.Indeterminate Then
                        SelectRandomlyAzan(Fagr_Voice_Files_CmbBx)
                        Play_Fagr_Voice_File_Btn.PerformClick()
                    End If
                Else
                    If Azan_Spoke_Method_ChkBx.CheckState = CheckState.Unchecked Then
                        If Voice_Azan_Files_CmbBx.SelectedIndex = -1 Then
                            Voice_Azan_Files_CmbBx.SelectedIndex = 0
                        ElseIf Voice_Azan_Files_CmbBx.SelectedIndex = (Voice_Azan_Files_CmbBx.Items.Count - 1) Then
                            Voice_Azan_Files_CmbBx.SelectedIndex = 0
                        Else
                            Voice_Azan_Files_CmbBx.SelectedIndex += 1
                        End If
                        Play_Voice_Azan_File_Btn_Click(Play_Voice_Azan_File_Btn, EventArgs.Empty)
                    ElseIf Azan_Spoke_Method_ChkBx.CheckState = CheckState.Checked Then
                        Play_Voice_Azan_File_Btn_Click(Play_Voice_Azan_File_Btn, EventArgs.Empty)
                    ElseIf Azan_Spoke_Method_ChkBx.CheckState = CheckState.Indeterminate Then
                        SelectRandomlyAzan(Voice_Azan_Files_CmbBx)
                        Play_Voice_Azan_File_Btn_Click(Play_Voice_Azan_File_Btn, EventArgs.Empty)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Dim PlayingAlert As Boolean
    Private Function ValedAlertBeforeAzan(ByVal PrayName) As Boolean
        Try
            Dim CDDs = calculateDiffDates(Convert.ToDateTime(Split(PrayName, ",").ToList(1)), CType(Now.Date & " 00:" & Time_To_Alert_Before_Azan_NmrcUpDn.Value & ":00", DateTime)).ToString
            Time_Left_For_Alert_TxtBx.Text = CDDs
            If PlayAlert Then Exit Function
            If CDDs >= "00:00:00" And CDDs <= "00:00:03" Then
                PlayingAlert = True
                PlayAlert = True
                wmp.close()
                Play_Voice_Azan_File_Btn_Click(Play_Voice_Azan_File_Btn, EventArgs.Empty)
            Else
                PlayingAlert = False
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub Note_Parameters_TbPg_Click(sender As Object, e As EventArgs) Handles Note_Parameters_TbPg.Click

    End Sub

    Private Sub Azan_Activation_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Azan_Activation_ChkBx.CheckedChanged
    End Sub

    Private Sub Asr_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Asr_TxtBx.TextChanged

    End Sub

    Private Sub ShortCut_TbCntrl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShortCut_TbCntrl.SelectedIndexChanged
        Try
            Shortcuts_LstVw = CType(ShortCut_TbCntrl.Controls(ShortCut_TbCntrl.SelectedTab.Name).Controls(ShortCut_TbCntrl.SelectedTab.Name), ListView)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Fagr_Voice_Files_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Fagr_Voice_Files_CmbBx.SelectedIndexChanged

    End Sub
    Private Sub Preview_Available_Alerts_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Available_Alerts_Btn.Click
        Try
            Dim AlertFileName = MagNotes_Folder_Path_TxtBx.Text & "\Alerts.txt"
            If Available_Alerts_DGV.Columns.Count = 0 Then
                If Language_Btn.Text = "E" Then
                    Available_Alerts_DGV.Columns.Add("Alert_Comment", "عنوان التنبيه")
                    Available_Alerts_DGV.Columns.Add("Alert_Active", "التنبيه فعال")
                    Available_Alerts_DGV.Columns.Add("Start_Alert_Time", "بداية وقت التنبيه")
                    Available_Alerts_DGV.Columns.Add("End_Alert_Time", "نهاية وقت التنبيه")
                    Available_Alerts_DGV.Columns.Add("Next_Alert_Time", "الوقت التالى للتنبيه")
                    Available_Alerts_DGV.Columns.Add("Related_To_MagNote", "التنبية مرتبط بملاحظة")
                    Available_Alerts_DGV.Columns.Add("MagNote_Name", "إسم الملاحظة")
                    Available_Alerts_DGV.Columns.Add("Alert_Repeet_Day", "مدى التنبية ـ(يوم)ـ")
                    Available_Alerts_DGV.Columns.Add("Alert_Repeet_Hour", "مدى التنبية ـ(ساعة)ـ")
                    Available_Alerts_DGV.Columns.Add("Alert_Repeet_Minute", "مدى التنبية ـ(دقيقة)ـ")
                    Available_Alerts_DGV.Columns.Add("Infinity_Alert", "لانهائى التنبية")
                    Available_Alerts_DGV.Columns.Add("Sound_Files_To_Play", "ملفات صوتية للتشغيل")
                    Available_Alerts_DGV.Columns.Add("Last_Played_File", "آخر ملف صوتى تم تشغيلة")
                    Available_Alerts_DGV.Columns.Add("Force_Stop_Playing_Current_File", "حتمية ايقاف التنبيه الحالى")
                    Available_Alerts_DGV.Columns.Add("Minutes_Between_Repetitions", "المدى بين التكرارات ـ(دقيقة)ـ")
                    Available_Alerts_DGV.Columns.Add("Hours_Between_Repetitions", "المدى بين التكرارات ـ(ساعة)ـ")
                    Available_Alerts_DGV.Columns.Add("Days_Between_Repetitions", "المدى بين التكرارات ـ(يوم)ـ")
                    Available_Alerts_DGV.Columns.Add("Alert_Sound_Volume", "مستوى صوت التنبيه")
                Else
                    Available_Alerts_DGV.Columns.Add("Alert_Comment", "Alert Comment")
                    Available_Alerts_DGV.Columns.Add("Alert_Active", "Alert Active")
                    Available_Alerts_DGV.Columns.Add("Start_Alert_Time", "Start Alert Time")
                    Available_Alerts_DGV.Columns.Add("End_Alert_Time", "End Alert Time")
                    Available_Alerts_DGV.Columns.Add("Next_Alert_Time", "Next_Alert_Time")
                    Available_Alerts_DGV.Columns.Add("Related_To_MagNote", "Related To MagNote")
                    Available_Alerts_DGV.Columns.Add("MagNote_Name", "MagNote Name")
                    Available_Alerts_DGV.Columns.Add("Alert_Repeet_Day", "Alert Duration (Days)")
                    Available_Alerts_DGV.Columns.Add("Alert_Repeet_Hour", "Alert Duration (Hours)")
                    Available_Alerts_DGV.Columns.Add("Alert_Repeet_Minute", "Alert Duration (Minutes)")
                    Available_Alerts_DGV.Columns.Add("Infinity_Alert", "Infinity Alert")
                    Available_Alerts_DGV.Columns.Add("Sound_Files_To_Play", "Sound Files To Play")
                    Available_Alerts_DGV.Columns.Add("Last_Played_File", "Last Played File")
                    Available_Alerts_DGV.Columns.Add("Force_Stop_Playing_Current_File", "Force Stop Playing Current File")
                    Available_Alerts_DGV.Columns.Add("Minutes_Between_Repetitions", "Minutes Beteen Repetitions")
                    Available_Alerts_DGV.Columns.Add("Hours_Between_Repetitions", "Hours Between Repetitions")
                    Available_Alerts_DGV.Columns.Add("Days_Between_Repetitions", "Days Between Repetitions")
                    Available_Alerts_DGV.Columns.Add("Alert_Sound_Volume", "Alert Sound Volume")
                End If
            End If
            If Not File.Exists(AlertFileName) Then Exit Sub
            Dim FilePaths() = Split(My.Computer.FileSystem.ReadAllText(AlertFileName, System.Text.Encoding.UTF8), ":EndTheRecord")
            Available_Alerts_DGV.Rows.Clear()
            For Each Alert In FilePaths
                If Alert.Length = 0 Then Continue For
                Dim Alert_Comment, Start_Alert_Time, End_Alert_Time, Next_Alert_Time, Related_To_MagNote, MagNote_Name, Alert_Active, Alert_Repeet_Day, Alert_Repeet_Hour, Alert_Repeet_Minute, Infinity_Alert, Sound_Files_To_Play, Last_Played_File, Force_Stop_Playing_Current_File, Minutes_Between_Repetitions, Hours_Between_Repetitions, Days_Between_Repetitions, Alert_Sound_Volume

                Dim TimeToAdd() = Split(Alert, ":")
                Dim FieldNumber = 0
                For Each Line In TimeToAdd
                    If Not String.IsNullOrEmpty(Line) Then
                        FieldNumber += 1
                    End If
                    Select Case True
                        Case Line.Contains("Alert_Comment")
                            Alert_Comment = Replace(Replace(Replace(TimeToAdd(FieldNumber), "(Instead Of Colon)", ":"), "Alert_Comment -(", ""), ")-", "")
                        Case Line.Contains("Start_Alert_Time")
                            Start_Alert_Time = Microsoft.VisualBasic.Left(TimeToAdd(FieldNumber), TimeToAdd(FieldNumber).Length)
                            Start_Alert_Time = Replace(Replace(Replace(Start_Alert_Time, "(Instead Of Colon)", ":"), "Start_Alert_Time -(", ""), ")-", "")
                        Case Line.Contains("End_Alert_Time")
                            End_Alert_Time = Microsoft.VisualBasic.Left(TimeToAdd(FieldNumber), TimeToAdd(FieldNumber).Length)
                            End_Alert_Time = Replace(Replace(Replace(End_Alert_Time, "(Instead Of Colon)", ":"), "End_Alert_Time -(", ""), ")-", "")
                        Case Line.Contains("Next_Alert_Time")
                            Next_Alert_Time = Microsoft.VisualBasic.Left(TimeToAdd(FieldNumber), TimeToAdd(FieldNumber).Length)
                            Next_Alert_Time = Replace(Replace(Replace(Next_Alert_Time, "(Instead Of Colon)", ":"), "Next_Alert_Time -(", ""), ")-", "")
                        Case Line.Contains("Related_To_MagNote")
                            Related_To_MagNote = Replace(Replace(TimeToAdd(FieldNumber), "Related_To_MagNote -(", ""), ")-", "")
                        Case Line.Contains("MagNote_Name")
                            MagNote_Name = Replace(Replace(TimeToAdd(FieldNumber), "(Instead Of Colon)", ":"), "MagNote_Name -(", "")
                            MagNote_Name = Microsoft.VisualBasic.Left(MagNote_Name, MagNote_Name.Length - 2)
                        Case Line.Contains("Alert_Active")
                            Alert_Active = Replace(Replace(TimeToAdd(FieldNumber), "Alert_Active -(", ""), ")-", "")
                        Case Line.Contains("Alert_Repeet_Day")
                            Alert_Repeet_Day = Replace(Replace(TimeToAdd(FieldNumber), "Alert_Repeet_Day -(", ""), ")-", "")
                        Case Line.Contains("Alert_Repeet_Hour")
                            Alert_Repeet_Hour = Replace(Replace(TimeToAdd(FieldNumber), "Alert_Repeet_Hour -(", ""), ")-", "")
                        Case Line.Contains("Alert_Repeet_Minute")
                            Alert_Repeet_Minute = Replace(Replace(TimeToAdd(FieldNumber), "Alert_Repeet_Minute -(", ""), ")-", "")
                        Case Line.Contains("Infinity_Alert")
                            Infinity_Alert = Replace(Replace(TimeToAdd(FieldNumber), "Infinity_Alert -(", ""), ")-", "")
                        Case Line.Contains("Sound_Files_To_Play")
                            Sound_Files_To_Play = Replace(Replace(Replace(TimeToAdd(FieldNumber), "(Instead Of Colon)", ":"), "Sound_Files_To_Play -(", ""), ")-", "")
                            If Microsoft.VisualBasic.Right(Sound_Files_To_Play, vbNewLine.Length) = vbNewLine Then
                                Sound_Files_To_Play = Microsoft.VisualBasic.Left(Sound_Files_To_Play, Sound_Files_To_Play.ToString.Length - vbNewLine.Length)
                            End If
                        Case Line.Contains("Last_Played_File")
                            Last_Played_File = Replace(Replace(Replace(TimeToAdd(FieldNumber), "(Instead Of Colon)", ":"), "Last_Played_File -(", ""), ")-", "")
                        Case Line.Contains("Force_Stop_Playing_Current_File")
                            Force_Stop_Playing_Current_File = Replace(Replace(TimeToAdd(FieldNumber), "Force_Stop_Playing_Current_File -(", ""), ")-", "")
                        Case Line.Contains("Minutes_Between_Repetitions")
                            Minutes_Between_Repetitions = Replace(Replace(TimeToAdd(FieldNumber), "Minutes_Between_Repetitions -(", ""), ")-", "")
                        Case Line.Contains("Hours_Between_Repetitions")
                            Hours_Between_Repetitions = Replace(Replace(TimeToAdd(FieldNumber), "Hours_Between_Repetitions -(", ""), ")-", "")
                        Case Line.Contains("Days_Between_Repetitions")
                            Days_Between_Repetitions = Replace(Replace(TimeToAdd(FieldNumber), "Days_Between_Repetitions -(", ""), ")-", "")
                        Case Line.Contains("Alert_Sound_Volume")
                            Alert_Sound_Volume = Replace(Replace(TimeToAdd(FieldNumber), "Alert_Sound_Volume -(", ""), ")-", "")
                    End Select
                Next
                If IsNothing(Next_Alert_Time) Then
                    Next_Alert_Time = Start_Alert_Time
                End If
                Available_Alerts_DGV.Rows.Add(
                Alert_Comment,
                Alert_Active,
                Start_Alert_Time,
                End_Alert_Time,
                Next_Alert_Time,
                Related_To_MagNote,
                MagNote_Name,
                Alert_Repeet_Day,
                Alert_Repeet_Hour,
                Alert_Repeet_Minute,
                Infinity_Alert,
                Sound_Files_To_Play,
                Last_Played_File,
                Force_Stop_Playing_Current_File,
                Minutes_Between_Repetitions,
                Hours_Between_Repetitions,
                Days_Between_Repetitions,
                Alert_Sound_Volume)
            Next
            Application.DoEvents()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Available_Alerts_DGV.ClearSelection()
        End Try
    End Sub
    Dim ForceStopPlayingCurrentPlayedFile As Boolean
    Dim CurentAlertSoundVolume = GetVolume()
    Private Sub Play_Alert_Btn_Click(sender As Object, e As EventArgs, Optional ByVal Alert As DataGridViewRow = Nothing) Handles Play_Alert_Btn.Click
        Try
            If ForceStopPlayingCurrentPlayedFile Then
                If Not String.IsNullOrEmpty(wmp.playState) Then
                    While wmp.playState = WMPPlayState.wmppsPlaying
                        Application.DoEvents()
                    End While
                End If
            End If
            If Not IsNothing(Alert) Then
                ForceStopPlayingCurrentPlayedFile = Alert.Cells("Force_Stop_Playing_Current_File").Value
                If Alert.Cells("Alert_Sound_Volume").Value <> CurentAlertSoundVolume Then
                    SetVolume(Alert.Cells("Alert_Sound_Volume").Value)
                End If
            End If
            If Mute_ChkBx.CheckState = CheckState.Checked Then
                Exit Sub
            End If
            Try
                If Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState = CheckState.Checked Then
                    If Force_Stop_Playing_Current_File_ChkBx.CheckState = CheckState.Unchecked Then
                        wmp.close()
                    End If
                End If
            Catch ex As Exception
            End Try

            If Not String.IsNullOrEmpty(wmp.playState) Then
                If wmp.playState <> WMPPlayState.wmppsPlaying Then
                    wmp.close()
                Else
                    If Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState = CheckState.Checked Then
                        If Force_Stop_Playing_Current_File_ChkBx.CheckState = CheckState.Unchecked Then
                            wmp.close()
                        End If
                    Else
                        While wmp.playState = WMPPlayState.wmppsPlaying
                            Application.DoEvents()
                        End While
                    End If
                End If
            End If
            wmp.URL() = Alert_Voice_Path_TxtBx.Text
            wmp.controls.play()
            If wmp.playState = WMPLib.WMPPlayState.wmppsTransitioning Then
                System.Threading.Thread.Sleep(100)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & "Voice File=(" & Alert_Voice_Path_TxtBx.Text & ")" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            PlayAlert = False
            ForceStopPlayingCurrentPlayedFile = False
        End Try
    End Sub

    Private Sub Add_Alert_Flder_Files_Btn_Click(sender As Object, e As EventArgs) Handles Add_Alert_Flder_Files_Btn.Click
        Using folderDialog As New FolderBrowserDialog
            AddAlertFilesDGVColumn()
            folderDialog.Description = "Select Alert Voice Folder Path"
            If File.Exists(Alert_Voice_Path_TxtBx.Text) Then
                folderDialog.SelectedPath = Path.GetDirectoryName(Alert_Voice_Path_TxtBx.Text) 'Environment.SpecialFolder.MyComputer
            Else
                folderDialog.SelectedPath = Application.StartupPath
            End If
            If folderDialog.ShowDialog() = DialogResult.OK Then
                If Alert_Files_DGV.Rows.Count > 0 And
                    Alert_Files_DGV.Rows.Count > 0 Then
                    If Language_Btn.Text = "E" Then
                        Msg = "هل تريد اضافة الملفات المختارة الى الملفات المضافة سابقا؟"
                    Else
                        Msg = "Do You Want To Add Selected Files To Previously Added Files?"
                    End If
                    If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                        Alert_Files_DGV.Rows.Clear()
                    End If
                End If
                Alert_Voice_Path_TxtBx.Text = folderDialog.SelectedPath
                Alert_Voice_Files_CmbBx.Items.Clear()
                DoRecursiveSearch(Alert_Voice_Path_TxtBx.Text,,, 1)
                For Each File In Alert_Voice_Files_CmbBx.Items
                    Alert_Files_DGV.Rows.Add(DirectCast(File, KeyValuePair(Of String, String)).Key, Path.GetFileName(DirectCast(File, KeyValuePair(Of String, String)).Key))
                Next
            End If
        End Using
    End Sub

    Private Sub Start_Alert_Time_DtTmPikr_ValueChanged(sender As Object, e As EventArgs) Handles Start_Alert_Time_DtTmPikr.ValueChanged
        End_Alert_Time_DtTmPikr.Value = Start_Alert_Time_DtTmPikr.Value.AddMinutes(1)
    End Sub

    Private Sub Alert_One_Time_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Related_To_MagNote_ChkBx.CheckedChanged

    End Sub

    Private Sub Azan_Spoke_Method_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Azan_Spoke_Method_ChkBx.CheckStateChanged
        Try
            Select Case Azan_Spoke_Method_ChkBx.CheckState
                Case CheckState.Checked
                    If Language_Btn.Text = "E" Then
                        sender.text = "أختيار المؤذن ثابت"
                    Else
                        sender.text = "Fixed Muezzin Selection"
                    End If
                Case CheckState.Unchecked
                    If Language_Btn.Text = "E" Then
                        sender.text = "إختيار المؤذن تتابعى"
                    Else
                        sender.text = "Sequentially Muezzin Selection"
                    End If
                Case CheckState.Indeterminate
                    If Language_Btn.Text = "E" Then
                        sender.text = "إختيار المؤذن عشوائى"
                    Else
                        sender.text = "Randome Muezzin Selection"
                    End If
            End Select
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Function FindCurrentAlertTime(Optional ByVal CheckAlertName As Boolean = False) As Boolean
        Try
            Dim StartAlertTimeToSerch, EndAlertTimeToSerch
            EndAlertTimeToSerch = Format(End_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt")
            StartAlertTimeToSerch = Format(Start_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt")
            Dim DGVRow
            For Each row In Available_Alerts_DGV.Rows
                If row.cells("Start_Alert_Time").value = StartAlertTimeToSerch And
                    row.cells("End_Alert_Time").value = EndAlertTimeToSerch Then
                    If CheckAlertName Then
                        If row.cells("Alert_Comment").value = Alert_Comment_TxtBx.Text Then
                            DGVRow = row
                            Exit For
                        End If
                    Else
                        DGVRow = row
                        Exit For
                    End If
                End If
            Next
            If Not IsNothing(DGVRow) Then
                Available_Alerts_DGV.ClearSelection()
                Available_Alerts_DGV.CurrentCell() = Available_Alerts_DGV(0, CType(DGVRow, DataGridViewRow).Index)
                Available_Alerts_DGV.Rows(CType(DGVRow, DataGridViewRow).Index).Selected = True
                Return True
            Else
                Dim x = 1
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    Private Function TheCurrentNoteIsNotEqualToTheRelatedOne(AlertName) As Boolean
        If Language_Btn.Text = "ع" Then
            Msg = "The Current Note Is Not Equal To The Related One... Do You Want To Use The Already Related One?"
        Else
            Msg = "الملاحظة الحالية غير مطابقة للملاحظة المرتبطة التنبيه... هل تريد استخدام الملاحظة المرتبطة بالفعل؟"
        End If
        If ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
            Return False
        End If
        Return True
    End Function

    Private Function AlertReadyToUpdate(AlertName) As Boolean
        Try
            Dim AlertExist As Boolean
            Dim MagNoteName = Nothing
            If FindCurrentAlertTime() Then
                AlertExist = True
            End If
            Dim TotalPeriodTime As TimeSpan
            TotalPeriodTime = End_Alert_Time_DtTmPikr.Value - Start_Alert_Time_DtTmPikr.Value
            'Dim durationEndTme As DateTime
            'durationEndTme = Start_Alert_Time_DtTmPikr.Value.AddMinutes(Repetition_Times_NmrcUpDn.Value * Minutes_Between_Repetitions_NmrcUpDn.Value)
            'Dim remainingTime As TimeSpan = durationEndTme - Start_Alert_Time_DtTmPikr.Value
            'If remainingTime.TotalSeconds > TotalPeriodTime.TotalSeconds Then
            '    If Language_Btn.Text = "E" Then
            '        Msg = "لا يمكن ان تكون فترة تكرار التنبيه اكبر من فترة التنبيه نفسه"
            '    Else
            '        Msg = "The Duration Of The Repetition Period Of The Alert Cannot Be Greater Than The Alert Duration Itself."
            '    End If
            '    Msg &= vbNewLine & remainingTime.TotalSeconds & " > " & TotalPeriodTime.TotalSeconds
            '    ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            '    Return False
            'End If
            If MagNotes_Folder_Path_TxtBx.TextLength = 0 Or
                Alert_Files_DGV.Rows.Count = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "من فضلك  إختار ملف(ات) التنبيه الصوتى"
                Else
                    Msg = "Kindly Select The Alerts Voice Files"
                End If
                ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            If CType(Start_Alert_Time_DtTmPikr.Value, DateTime) > CType(End_Alert_Time_DtTmPikr.Value, DateTime) Then
                If Language_Btn.Text = "E" Then
                    Msg = "هذا التنبية غير منطقى... حيث بداية التنبية اكبر من او تساوى نهايته"
                Else
                    Msg = "This Alert Is Not Logically... Where The Alert Start Is Greater Than Or Equal To The It's End"
                End If
                ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            If Alert_Comment_TxtBx.TextLength = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "من فضلك ادخل إسم تعريفى للتنبية"
                Else
                    Msg = "Kindly Enter A Definition Name For The Alert"
                End If
                ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            If Alert_Active_ChkBx.CheckState = CheckState.Unchecked Then
                If Language_Btn.Text = "E" Then
                    Msg = "هذا التنبيه غير فعال... هل تريد تفعيلة؟"
                Else
                    Msg = "This Alert Is Not Active... Do You Want To Activate It?"
                End If
                If ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                    Alert_Active_ChkBx.CheckState = CheckState.Checked
                End If
            End If

            If Alert_Repeet_Minute_NmrcUpDn.Value +
                Alert_Repeet_Hour_NmrcUpDn.Value +
                Alert_Repeet_Day_NmrcUpDn.Value = 0 Then
                If Language_Btn.Text = "ع" Then
                    Msg = "You Did Not Add Any Alert Repitition Time For This Alert Yet... Kindly Prepare Alert Repetition Time"
                Else
                    Msg = "لم تقم بإعداد وقت إعادة التنبية بعد... برجاء تحتد وقت اادة التنبيه"
                End If
                ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            Dim RepeetTime As DateTime
            RepeetTime = Start_Alert_Time_DtTmPikr.Value
            RepeetTime.AddMinutes(Minutes_Between_Repetitions_NmrcUpDn.Value)
            RepeetTime.AddMinutes(Hours_Between_Repetitions_NmrcUpDn.Value)
            RepeetTime.AddMinutes(Days_Between_Repetitions_NmrcUpDn.Value)
            If RepeetTime > End_Alert_Time_DtTmPikr.Value Then
                If Language_Btn.Text = "ع" Then
                    Msg = "The Time Between Repetitions Is Exceeded to End Alert Time... Kindly Add Correct Time Between Repetitions"
                Else
                    Msg = "لقد تجاوز المدى بين التكرارات الوقت المحدد لإنهاء وقت التنبيه... يرجى إضافة الوقت الصحيح بين التكرارات"
                End If
                ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            If AlertExist Then
                If Related_To_MagNote_ChkBx.CheckState = CheckState.Checked And
                    MagNotes_Notes_TbCntrl.SelectedTab.Name <> Available_Alerts_DGV.CurrentRow.Cells("MagNote_Name").Value Then
                    If Not (TheCurrentNoteIsNotEqualToTheRelatedOne(AlertName)) Then
                        Exit Function
                    End If
                End If
            ElseIf Related_To_MagNote_ChkBx.CheckState = CheckState.Checked Then
                If Available_Alerts_DGV.SelectedRows.Count = 1 And
                    Not IsNothing(Available_Alerts_DGV.CurrentRow) Then
                    If MagNotes_Notes_TbCntrl.SelectedTab.Name <> Available_Alerts_DGV.CurrentRow.Cells("MagNote_Name").Value Then
                        If Not (TheCurrentNoteIsNotEqualToTheRelatedOne(AlertName)) Then
                            Exit Function
                        End If
                    End If
                ElseIf Not File.Exists(MagNotes_Notes_TbCntrl.SelectedTab.Name) Then
                    If Language_Btn.Text = "ع" Then
                        Msg = "Kindly Select An Existing Note To Relate It To This Alert"
                    Else
                        Msg = "يرجى تحديد ملاحظة موجودة لربطها بهذا التنبيه"
                    End If
                    ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    Return False
                End If
            End If
            If FindCurrentAlertTime() Then
                If Language_Btn.Text = "E" Then
                    Msg = "وقت هذا التنبيه مسجل من قبل... هل تريد التعديل عليه؟"
                Else
                    Msg = "This Alert Time Already Recorded Before... Do You Want To Update It?"
                End If
                Msg &= vbNewLine & Available_Alerts_DGV.CurrentRow.Cells("Alert_Comment").Value
                If ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                    Return False
                End If
                UpdateAvailableAlertsDGVCurrentRows()
            ElseIf Available_Alerts_DGV.SelectedRows.Count = 1 And
                    Not IsNothing(Available_Alerts_DGV.CurrentRow) Then
                If Language_Btn.Text = "E" Then
                    Msg = "هل تريد التعديل على هذا التنبيه؟"
                Else
                    Msg = "Do You Want To Update This Alert?"
                End If
                Msg &= vbNewLine & Available_Alerts_DGV.CurrentRow.Cells("Alert_Comment").Value
                Msg &= vbNewLine & Available_Alerts_DGV.CurrentRow.Cells("Start_Alert_Time").Value
                Msg &= vbNewLine & Available_Alerts_DGV.CurrentRow.Cells("End_Alert_Time").Value
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                    Return False
                End If
                UpdateAvailableAlertsDGVCurrentRows()
            Else
                If Related_To_MagNote_ChkBx.CheckState = CheckState.Checked Then
                    MagNoteName = MagNotes_Notes_TbCntrl.SelectedTab.Name
                End If
                Dim FilesToAdd
                For Each File In Alert_Files_DGV.Rows
                    If IsNothing(File.cells("File_Path").value) Then Continue For
                    FilesToAdd &= File.cells("File_Path").value & vbNewLine
                Next
                FilesToAdd = Microsoft.VisualBasic.Left(FilesToAdd, FilesToAdd.ToString.Length - vbNewLine.Length)
                Available_Alerts_DGV.Rows.Add(
                            Alert_Comment_TxtBx.Text,
                            Convert.ToInt32(Alert_Active_ChkBx.CheckState),
                            Format(Start_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt"),
                            Format(End_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt"),
                            Format(Start_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt"),
                            Convert.ToInt32(Related_To_MagNote_ChkBx.CheckState),
                            MagNoteName,
                            Alert_Repeet_Day_NmrcUpDn.Value,
                            Alert_Repeet_Hour_NmrcUpDn.Value,
                            Alert_Repeet_Minute_NmrcUpDn.Value,
                            FilesToAdd,
                            Nothing,
                            Convert.ToInt32(Force_Stop_Playing_Current_File_ChkBx.CheckState),
                            Minutes_Between_Repetitions_NmrcUpDn.Value,
                            Hours_Between_Repetitions_NmrcUpDn.Value,
                            Days_Between_Repetitions_NmrcUpDn.Value,
                            Alert_Sound_Volume_TrkBr.Value)
            End If
            If Alert_Files_DGV.Rows.Count > 0 And
                Infinity_Alert_ChkBx.CheckState = CheckState.Checked Then
                If Language_Btn.Text = "E" Then
                    Msg = "هل تريد حفظ ملفات التنبية الصوتية المختارة مع التنبيه الحالى؟"
                Else
                    Msg = "Do You Want To Save Selected Alerts Sound Files With Current Alert?"
                End If
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                    Return True
                End If
                Dim FilesToAdd As String = String.Empty
                For Each File In Alert_Files_DGV.Rows
                    FilesToAdd &= File.cells("File_Path").value & vbNewLine
                Next
                Available_Alerts_DGV.CurrentRow.Cells("Sound_Files_To_Play").Value = FilesToAdd
            ElseIf Alert_Files_DGV.Rows.Count = 1 Then
                Available_Alerts_DGV.CurrentRow.Cells("Sound_Files_To_Play").Value = Alert_Files_DGV.Rows(0).cells("File_Path").value
            End If
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Dim DGVRow
    Private Function AlertRelatedToMagNote(ByVal Alert As DataGridViewRow) As Boolean
        Dim MagNoteName = Alert.Cells("MagNote_Name").Value
        If Force_Stop_Playing_Current_File_ChkBx.CheckState = CheckState.Unchecked Then
            If Language_Btn.Text = "ع" Then
                Msg = "The Alert [" & Alert.Cells("Alert_Comment").Value & "] Have Reminder Time Now " & vbNewLine & "Reminder Time " & Start_Alert_Time_DtTmPikr.Value & vbNewLine & "Now Ti.. Do You Want To View It Now? "
            Else
                Msg = "الملاحظة [" & Alert.Cells("Alert_Comment").Value & "] لها وقت تنبية الآن " & vbNewLine & "وقت التنبية " & Start_Alert_Time_DtTmPikr.Value & vbNewLine & "الوقت الآن... هل تريد عرضها الآن؟"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                MakeTopMost(1)
            Else
                Exit Function
            End If
        End If
        If Not isInDataGridView(MagNoteName, "MagNote_Name", Available_MagNotes_DGV,,, 1, 1) Then
            Dim OSINTChkBx As New CheckBox
            OSINTChkBx.CheckState = Open_Note_In_New_Tab_ChkBx.CheckState
            UseArgFile = MagNoteName
            Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
            ExternalFilePath = Path.GetDirectoryName(MagNoteName)
            ExternalFileName = Path.GetFileName(MagNoteName)
            Me.ActiveControl = MagNote_No_CmbBx
            If Not DirectCast(Me, MagNote_Form).MagNoteFileFormat(MagNoteName) Then
                DirectCast(Me, MagNote_Form).Open_Note_TlStrpBtn_Click(Note_TlStrp.Items("OpenToolStripButton"), EventArgs.Empty)
            End If
            Open_Note_In_New_Tab_ChkBx.CheckState = OSINTChkBx.CheckState
        End If

    End Function
    Private Sub Save_Alert_Btn_Click(sender As Object, e As EventArgs) Handles Save_Alert_Btn.Click
        Try
            Dim AlertName As String = String.Empty
            If Not IsNothing(Available_Alerts_DGV.CurrentRow) Then
                AlertName = vbNewLine & Alert_Comment_TxtBx.Text
            End If
            Dim FileNameToSave = MagNotes_Folder_Path_TxtBx.Text & "\Alerts.txt"
            Cursor = Cursors.WaitCursor
            If Not AlertReadyToUpdate(AlertName) Then
                Exit Sub
            End If
            If File.Exists(FileNameToSave) Then
                My.Computer.FileSystem.DeleteFile(FileNameToSave, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            End If
            RefreshAlertFile()
            If Language_Btn.Text = "ع" Then
                Msg = "The File Successfully"
            Else
                Msg = "تم حفظ الملف بنجاح"
            End If
            ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Alert_Voice_Files_CmbBx.Items.Clear()
            Alert_Files_DGV.Rows.Clear()
            Preview_Available_Alerts_Btn_Click(Preview_Available_Alerts_Btn, EventArgs.Empty)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            If Language_Btn.Text = "ع" Then
                Msg = "Do You Want To Start Alert Again?"
            Else
                Msg = "هل تريد اعادة تشغيل التنبيه مرة اخرى؟"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                Me.ActiveControl = Enable_Update_Btn
                Enable_Update_Btn_Click(Enable_Update_Btn, EventArgs.Empty)
            End If
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function UpdateAvailableAlertsDGVCurrentRows(Optional ByVal CurrentDGVRow As DataGridViewRow = Nothing) As Boolean
        Available_Alerts_DGV.CurrentRow.Cells("Alert_Comment").Value = Alert_Comment_TxtBx.Text
        Available_Alerts_DGV.CurrentRow.Cells("Start_Alert_Time").Value = Format(Start_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt")
        Available_Alerts_DGV.CurrentRow.Cells("End_Alert_Time").Value = Format(End_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt")
        IgnoreSortAvailableAlertsDGV = True
        Available_Alerts_DGV.CurrentRow.Cells("Next_Alert_Time").Value = Format(Start_Alert_Time_DtTmPikr.Value, "yyyy/MM/dd hh:mm:ss tt")
        Available_Alerts_DGV.CurrentRow.Cells("Related_To_MagNote").Value = Convert.ToInt32(Related_To_MagNote_ChkBx.CheckState)
        Dim MagNoteName = Nothing
        If Related_To_MagNote_ChkBx.CheckState = CheckState.Checked And
                Not File.Exists(MagNotes_Notes_TbCntrl.SelectedTab.Name) Then
            If Available_Alerts_DGV.SelectedRows.Count = 1 Then
                If Not IsNothing(Available_Alerts_DGV.CurrentRow.Cells("MagNote_Name").Value) Then
                    MagNoteName = Available_Alerts_DGV.CurrentRow.Cells("MagNote_Name").Value
                End If
            End If
        ElseIf Related_To_MagNote_ChkBx.CheckState = CheckState.Checked Then
            MagNoteName = MagNotes_Notes_TbCntrl.SelectedTab.Name
        End If
        Available_Alerts_DGV.CurrentRow.Cells("MagNote_Name").Value = MagNoteName
        Available_Alerts_DGV.CurrentRow.Cells("Alert_Active").Value = Convert.ToInt32(Alert_Active_ChkBx.CheckState)
        Available_Alerts_DGV.CurrentRow.Cells("Alert_Repeet_Day").Value = Alert_Repeet_Day_NmrcUpDn.Value
        Available_Alerts_DGV.CurrentRow.Cells("Alert_Repeet_Hour").Value = Alert_Repeet_Hour_NmrcUpDn.Value
        Available_Alerts_DGV.CurrentRow.Cells("Alert_Repeet_Minute").Value = Alert_Repeet_Minute_NmrcUpDn.Value
        Available_Alerts_DGV.CurrentRow.Cells("Infinity_Alert").Value = Convert.ToInt32(Infinity_Alert_ChkBx.CheckState)
        Available_Alerts_DGV.CurrentRow.Cells("Force_Stop_Playing_Current_File").Value = Convert.ToInt32(Force_Stop_Playing_Current_File_ChkBx.CheckState)
        Available_Alerts_DGV.CurrentRow.Cells("Minutes_Between_Repetitions").Value = Minutes_Between_Repetitions_NmrcUpDn.Value
        Available_Alerts_DGV.CurrentRow.Cells("Hours_Between_Repetitions").Value = Hours_Between_Repetitions_NmrcUpDn.Value
        Available_Alerts_DGV.CurrentRow.Cells("Days_Between_Repetitions").Value = Days_Between_Repetitions_NmrcUpDn.Value
        Available_Alerts_DGV.CurrentRow.Cells("Alert_Sound_Volume").Value = Alert_Sound_Volume_TrkBr.Value
    End Function
    Private Sub Delete_Alert_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Alert_Btn.Click
        Try
            If Not IsNothing(Available_Alerts_DGV.CurrentRow) And Available_Alerts_DGV.SelectedRows.Count <> 0 Then
                Dim AlertName = vbNewLine & Available_Alerts_DGV.CurrentRow.Cells("Alert_Comment").Value
                If Language_Btn.Text = "E" Then
                    Msg = "هل حقا تريد إلغاء التنبي(ه/ات) المختار(ة)؟"
                Else
                    Msg = "Do You Really Want To Delete Selected Alert(s)?"
                End If
                If ShowMsg(Msg & AlertName, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                    Exit Sub
                End If
RearangeIndex:
                For i As Integer = Available_Alerts_DGV.SelectedRows.Count - 1 To 0 Step -1
                    Dim rowIndex As Integer = Available_Alerts_DGV.SelectedRows(i).Index
                    Available_Alerts_DGV.Rows.RemoveAt(rowIndex)
                Next
                RefreshAlertFile()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            If Language_Btn.Text = "ع" Then
                Msg = "Do You Want To Start Alert Again?"
            Else
                Msg = "هل تريد اعادة تشغيل التنبيه مرة اخرى؟"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.Yes Then
                Me.ActiveControl = Enable_Update_Btn
                Enable_Update_Btn_Click(Enable_Update_Btn, EventArgs.Empty)
            End If
        End Try
    End Sub
    Private Function SelectRandomlyAzan(ByVal CmbBx As ComboBox) As Boolean
        Try
            Dim random As Random = New Random()
            Dim newSelectedIndex As Integer = CmbBx.SelectedIndex
            While newSelectedIndex = CmbBx.SelectedIndex
                newSelectedIndex = random.[Next](0, CmbBx.Items.Count)
            End While
            CmbBx.SelectedIndex = newSelectedIndex
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub Asr_TxtBx_DoubleClick(sender As Object, e As EventArgs) Handles Fajr_TxtBx.DoubleClick, Dhuhr_TxtBx.DoubleClick, Asr_TxtBx.DoubleClick, Maghrib_TxtBx.DoubleClick, Isha_TxtBx.DoubleClick
        If sender.ReadOnly Then
            sender.ReadOnly = False
        Else
            sender.ReadOnly = True
        End If
    End Sub

    Private Sub Related_To_MagNote_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Related_To_MagNote_ChkBx.CheckStateChanged
    End Sub
    Private Sub Available_Alerts_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Available_Alerts_DGV.SelectionChanged
        Try
            If sender.SelectedRows.Count = 0 Then Exit Sub
            If ActiveControl.Name <> sender.name Then
                Exit Sub
            End If
            AddAlertFilesDGVColumn()
            If Not IsNothing(sender.CurrentRow) And
                Alert_Setting_Pnl.Enabled = True Then
                RefreshAlert(sender.CurrentRow)
                Alert_Files_DGV.Rows.Clear()
                If Not IsNothing(Available_Alerts_DGV.CurrentRow) Then
                    If Not String.IsNullOrEmpty(Available_Alerts_DGV.CurrentRow.Cells("Sound_Files_To_Play").Value) Then
                        Dim FileName
                        For Each file In Split(Available_Alerts_DGV.CurrentRow.Cells("Sound_Files_To_Play").Value, vbCrLf)
                            If Not isInDataGridView(file, "File_Path", Alert_Files_DGV) Then
                                FileName = Path.GetFileName(file)
                                Alert_Files_DGV.Rows.Add(file, FileName)
                            End If
                        Next
                        Alert_Files_DGV.ClearSelection()
                    End If
                End If
            ElseIf Not IsNothing(sender.CurrentRow) And
                Available_Alerts_DGV.SelectedRows.count > 0 And
                ActiveControl.Name = sender.name Then
                Available_Alerts_DGV.ClearSelection()
                If Language_Btn.Text = "E" Then
                    Msg = "لعرض بيانات التنبيه يجب اولا ايقاف تشغيل الميقاتى"
                Else
                    Msg = "To Preview Alert Data Kindly Stop The Timer First"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            Else
                ShowMsg("IsNothing(sender.CurrentRow)", "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Available_Alerts_DGV.Refresh()
        End Try
    End Sub
    Dim AlertExist, ValidRepetition As Boolean
    Dim ForceStopPlayingCurrentFile As Boolean
    Dim NextAlertTime, NowAlertTime, EndAlertTime, InfinityAlert, RepeetValue, CurrentTime
    Dim MinutesBetweenRepetitions As Integer

    Private Sub Alert_Tmr_Tick(sender As Object, e As EventArgs) Handles Alert_Tmr.Tick
        Try
            Application.DoEvents()
            If wmp.playState <> WMPPlayState.wmppsPlaying Then
                SetVolume(CurentAlertSoundVolume)
            End If
            Alert_Tmr.Stop()
            If Activate_Alert_Function_ChkBx.CheckState = CheckState.Unchecked Then
                Alert_Tmr.Stop()
                If Alert_Setting_Pnl.Enabled Then
                    Me.ActiveControl = Enable_Update_Btn
                    Enable_Update_Btn_Click(Enable_Update_Btn, EventArgs.Empty)
                End If
                Exit Sub
            End If
            For Each Alert In Available_Alerts_DGV.Rows
                If Not Convert.ToBoolean(Val(Alert.Cells("Alert_Active").Value)) Then
                    Continue For
                End If
                NextAlertTime = Convert.ToDateTime(Alert.Cells("Next_Alert_Time").Value)
                NowAlertTime = Convert.ToDateTime(Now)
                EndAlertTime = Convert.ToDateTime(Alert.Cells("End_Alert_Time").Value)
                InfinityAlert = Convert.ToBoolean(Val(Alert.Cells("Infinity_Alert").Value))
                If Alert.Cells("Alert_Comment").Value = "test" Then
                    Dim x = 1
                End If
                If NextAlertTime > EndAlertTime And
                     Not InfinityAlert Then
                    Available_Alerts_DGV.Rows(Alert.index).Cells("Alert_Active").Value = 0
                    RefreshAlertFile()
                    Continue For
                End If
                If NowAlertTime >= NextAlertTime Then
                    Alert_Voice_Files_CmbBx.Items.Clear()
                    For Each file In Split(Alert.Cells("Sound_Files_To_Play").Value, vbCrLf)
                        Alert_Voice_Files_CmbBx.Items.Add(New KeyValuePair(Of String, String)(file, Path.GetFileNameWithoutExtension(file)))
                    Next
                    '----------------
                    Dim TimeToUpdate As DateTime
                    AlertExist = True
                    Dim CheckTime As DateTime = Now
                    If InfinityAlert Then
                        CheckTime = CheckTime.AddMinutes(-Alert.Cells("Alert_Repeet_Minute").Value)
                        CheckTime = CheckTime.AddHours(-Alert.Cells("Alert_Repeet_Hour").Value)
                        CheckTime = CheckTime.AddDays(-Alert.Cells("Alert_Repeet_Day").Value)
                        TimeToUpdate = NextAlertTime
                    Else
                        CheckTime = CheckTime.AddMinutes(-Alert.Cells("Minutes_Between_Repetitions").Value)
                        CheckTime = CheckTime.AddHours(-Alert.Cells("Hours_Between_Repetitions").Value)
                        CheckTime = CheckTime.AddDays(-Alert.Cells("Days_Between_Repetitions").Value)
                        TimeToUpdate = NextAlertTime
                    End If
                    CurrentTime = TimeToUpdate
                    If InfinityAlert Then
                        RepeetValue = Val(Alert.Cells("Alert_Repeet_Minute").Value.ToString) +
                                        Val(Alert.Cells("Alert_Repeet_Hour").Value.ToString) +
                                        Val(Alert.Cells("Alert_Repeet_Day").Value.ToString)
                    Else
                        RepeetValue = Val(Alert.Cells("Minutes_Between_Repetitions").Value.ToString) +
                                        Val(Alert.Cells("Hours_Between_Repetitions").Value.ToString) +
                                        Val(Alert.Cells("Days_Between_Repetitions").Value.ToString)
                    End If
                    If RepeetValue <> 0 Then
                        While TimeToUpdate < CheckTime
                            If InfinityAlert Then
                                TimeToUpdate = TimeToUpdate.AddMinutes(Alert.Cells("Alert_Repeet_Minute").Value)
                                TimeToUpdate = TimeToUpdate.AddHours(Alert.Cells("Alert_Repeet_Hour").Value)
                                TimeToUpdate = TimeToUpdate.AddDays(Alert.Cells("Alert_Repeet_Day").Value)
                            Else
                                TimeToUpdate = TimeToUpdate.AddMinutes(Alert.Cells("Minutes_Between_Repetitions").Value)
                                TimeToUpdate = TimeToUpdate.AddHours(Alert.Cells("Hours_Between_Repetitions").Value)
                                TimeToUpdate = TimeToUpdate.AddDays(Alert.Cells("Days_Between_Repetitions").Value)
                            End If
                        End While
                    End If
                    If InfinityAlert Then
                        TimeToUpdate = TimeToUpdate.AddMinutes(Alert.Cells("Alert_Repeet_Minute").Value)
                        TimeToUpdate = TimeToUpdate.AddHours(Alert.Cells("Alert_Repeet_Hour").Value)
                        TimeToUpdate = TimeToUpdate.AddDays(Alert.Cells("Alert_Repeet_Day").Value)
                    Else
                        TimeToUpdate = TimeToUpdate.AddMinutes(Alert.Cells("Minutes_Between_Repetitions").Value)
                        TimeToUpdate = TimeToUpdate.AddHours(Alert.Cells("Hours_Between_Repetitions").Value)
                        TimeToUpdate = TimeToUpdate.AddDays(Alert.Cells("Days_Between_Repetitions").Value)
                    End If
                    Available_Alerts_DGV.Rows(Alert.index).Cells("Next_Alert_Time").Value = TimeToUpdate
                    If Convert.ToBoolean(Val(Alert.Cells("Related_To_MagNote").Value.ToString)) Then
                        AlertRelatedToMagNote(Alert)
                    End If
                    While wmp.playState = WMPPlayState.wmppsPlaying
                        ShowMsg("Now Plying " & Alert.Cells("Alert_Comment").Value, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
                        If Alert_Setting_Pnl.Enabled = False Then
                            wmp.controls.stop()
                            wmp.close()
                            Exit While
                        End If
                        If Force_Stop_Playing_Current_Sound_File_ChkBx.CheckState = CheckState.Checked Then
                            If Not ForceStopPlayingCurrentFile Then
                                wmp.controls.stop()
                                wmp.close()
                                Exit While
                            End If
                        End If
                        Application.DoEvents()
                    End While
                    PlayAlertFile(Alert, CurrentTime)
                End If
            Next
            If AlertExist Then
                RefreshAlertFile()
            End If
            AlertExist = False
            For Each Alert In Available_Alerts_DGV.Rows
                If Not Convert.ToBoolean(Val(Alert.Cells("Alert_Active").Value)) Then
                    Continue For
                End If
                AlertExist = True
                Exit For
            Next
            If Not AlertExist And Available_Alerts_DGV.Rows.Count > 0 Then
                Enable_Update_Btn_Click(Enable_Update_Btn, EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & Alert_Comment_TxtBx.Text, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            NextAlertTime = Nothing
            NowAlertTime = Nothing
            EndAlertTime = Nothing
            InfinityAlert = Nothing
            AlertExist = False
            ValidRepetition = False
            RepeetValue = Nothing
            CurrentTime = Nothing
            If Alert_Setting_Pnl.Enabled = False Then
                Alert_Tmr.Start()
            End If
            Application.DoEvents()
        End Try
    End Sub
    Private Function RefreshAlertFile() As Boolean
        Dim FileNameToSave = MagNotes_Folder_Path_TxtBx.Text & "\Alerts.txt"
        Dim TextToWrite As String = Nothing
        If File.Exists(FileNameToSave) Then
            My.Computer.FileSystem.DeleteFile(FileNameToSave, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
        End If
        For Each Row In Available_Alerts_DGV.Rows
            TextToWrite &= ":Alert_Comment -(" & Replace(Row.cells("Alert_Comment").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Start_Alert_Time -(" & Replace(Row.cells("Start_Alert_Time").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":End_Alert_Time -(" & Replace(Row.cells("End_Alert_Time").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Next_Alert_Time -(" & Replace(Row.cells("Next_Alert_Time").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Related_To_MagNote -(" & Row.cells("Related_To_MagNote").value & ")-"
            TextToWrite &= ":MagNote_Name -(" & Replace(Row.cells("MagNote_Name").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Alert_Active -(" & Row.cells("Alert_Active").value & ")-"
            TextToWrite &= ":Alert_Repeet_Day -(" & Row.cells("Alert_Repeet_Day").value & ")-"
            TextToWrite &= ":Alert_Repeet_Hour -(" & Row.cells("Alert_Repeet_Hour").value & ")-"
            TextToWrite &= ":Alert_Repeet_Minute -(" & Row.cells("Alert_Repeet_Minute").value & ")-"
            TextToWrite &= ":Infinity_Alert -(" & Row.cells("Infinity_Alert").value & ")-"
            TextToWrite &= ":Sound_Files_To_Play -(" & Replace(Row.cells("Sound_Files_To_Play").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Last_Played_File -(" & Replace(Row.cells("Last_Played_File").value, ":", "(Instead Of Colon)") & ")-"
            TextToWrite &= ":Force_Stop_Playing_Current_File -(" & Row.cells("Force_Stop_Playing_Current_File").value & ")-"
            TextToWrite &= ":Minutes_Between_Repetitions -(" & Row.cells("Minutes_Between_Repetitions").value & ")-"
            TextToWrite &= ":Hours_Between_Repetitions -(" & Row.cells("Hours_Between_Repetitions").value & ")-"
            TextToWrite &= ":Days_Between_Repetitions -(" & Row.cells("Days_Between_Repetitions").value & ")-"
            If IsNothing(Row.cells("Alert_Sound_Volume").value) Then
                TextToWrite &= ":Alert_Sound_Volume -(" & GetVolume() & ")-"
            ElseIf val(Row.cells("Alert_Sound_Volume").value.ToString) = 0 Then
                TextToWrite &= ":Alert_Sound_Volume -(" & GetVolume() & ")-"
            Else
                TextToWrite &= ":Alert_Sound_Volume -(" & Row.cells("Alert_Sound_Volume").value & ")-"
            End If

            TextToWrite &= ":EndTheRecord"
            My.Computer.FileSystem.WriteAllText(FileNameToSave, TextToWrite, 1, System.Text.Encoding.UTF8)
            TextToWrite = String.Empty
        Next
    End Function
    Private Function PlayAlertFile(ByVal CurrentAlert As DataGridViewRow, CurrentTime As DateTime)
        Try
            If Alert_Setting_Pnl.Enabled = True Then
                ShowMsg("Alert_Setting_Pnl.Enabled = True", "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0)
                Exit Function
            End If
            ForceStopPlayingCurrentFile = CurrentAlert.Cells("Force_Stop_Playing_Current_File").Value
            Alert_Voice_Path_TxtBx.Text = Nothing
            If Convert.ToInt32(CurrentAlert.Cells("Infinity_Alert").Value) = 1 And
                Alert_Voice_Files_CmbBx.Items.Count > 0 Then 'تتابعى
                IsInMagNoteCmbBx(CurrentAlert.Cells("Last_Played_File").Value, 1, Alert_Voice_Files_CmbBx)
                If Alert_Voice_Files_CmbBx.SelectedIndex = -1 Then
                    Alert_Voice_Files_CmbBx.SelectedIndex = 0
                ElseIf Alert_Voice_Files_CmbBx.SelectedIndex = (Alert_Voice_Files_CmbBx.Items.Count - 1) Then
                    Alert_Voice_Files_CmbBx.SelectedIndex = 0
                Else
                    If Alert_Voice_Files_CmbBx.SelectedIndex + 1 = Alert_Voice_Files_CmbBx.Items.Count Then
                        Alert_Voice_Files_CmbBx.SelectedIndex = 0
                    Else
                        Alert_Voice_Files_CmbBx.SelectedIndex += 1
                    End If
                End If
            ElseIf Convert.ToInt32(CurrentAlert.Cells("Infinity_Alert").Value) = 0 Then 'ملف معين
                IsInMagNoteCmbBx(CurrentAlert.Cells("Sound_Files_To_Play").Value, 1, Alert_Voice_Files_CmbBx,,,,, 1)
            End If
            If Alert_Voice_Files_CmbBx.SelectedIndex <> -1 Then
                Alert_Voice_Path_TxtBx.Text = DirectCast(Alert_Voice_Files_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
                If Language_Btn.Text = "E" Then
                    Msg = "يتم تشغل هذا التنبية الان "
                Else
                    Msg = "Playing This Alert Now "
                End If
                ShowMsg(Msg &
                        CurrentAlert.Cells("Alert_Comment").Value & vbNewLine &
                        CurrentAlert.Cells("Last_Played_File").Value & vbNewLine &
                        CurrentTime, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, 0,,,,,, Convert.ToBoolean(Force_Activate_TabPage_When_Alert_Is_Active_ChkBx.CheckState))
                Play_Alert_Btn_Click(Play_Alert_Btn, EventArgs.Empty, CurrentAlert)
                Available_Alerts_DGV.Rows(CurrentAlert.Index).Cells("Last_Played_File").Value = Alert_Voice_Path_TxtBx.Text
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & Alert_Voice_Path_TxtBx.Text, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Function RefreshAlert(ByVal CurrentAlert As DataGridViewRow)
        Try
            Alert_Comment_TxtBx.Text = CurrentAlert.Cells("Alert_Comment").Value
            Start_Alert_Time_DtTmPikr.Value = CurrentAlert.Cells("Start_Alert_Time").Value
            End_Alert_Time_DtTmPikr.Value = CurrentAlert.Cells("End_Alert_Time").Value
            Related_To_MagNote_ChkBx.CheckState = CurrentAlert.Cells("Related_To_MagNote").Value
            Alert_Active_ChkBx.CheckState = CurrentAlert.Cells("Alert_Active").Value
            Alert_Repeet_Minute_NmrcUpDn.Value = CurrentAlert.Cells("Alert_Repeet_Minute").Value
            Alert_Repeet_Hour_NmrcUpDn.Value = CurrentAlert.Cells("Alert_Repeet_Hour").Value
            Alert_Repeet_Day_NmrcUpDn.Value = CurrentAlert.Cells("Alert_Repeet_Day").Value
            Infinity_Alert_ChkBx.CheckState = CurrentAlert.Cells("Infinity_Alert").Value
            Force_Stop_Playing_Current_File_ChkBx.CheckState = CurrentAlert.Cells("Force_Stop_Playing_Current_File").Value
            Minutes_Between_Repetitions_NmrcUpDn.Value = Val(CurrentAlert.Cells("Minutes_Between_Repetitions").Value.ToString)
            Hours_Between_Repetitions_NmrcUpDn.Value = Val(CurrentAlert.Cells("Hours_Between_Repetitions").Value.ToString)
            Days_Between_Repetitions_NmrcUpDn.Value = Val(CurrentAlert.Cells("Days_Between_Repetitions").Value.ToString)
            If IsNothing(CurrentAlert.Cells("Alert_Sound_Volume").Value) Then
                Alert_Sound_Volume_TrkBr.Value = GetVolume()
            ElseIf val(CurrentAlert.Cells("Alert_Sound_Volume").Value.ToString) = 0 Then
                Alert_Sound_Volume_TrkBr.Value = GetVolume()
            Else
                Alert_Sound_Volume_TrkBr.Value = CurrentAlert.Cells("Alert_Sound_Volume").Value
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
        End Try
    End Function
    Private Sub ChangeAlertTmrStatus(ByVal StartAlert As Boolean)
        Try
            If Activate_Alert_Function_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Sub
            End If
            If StartAlert Then
                Alert_Tmr.Start()
                Pause_Alert_Timer_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Puse
                Available_Alerts_DGV.MultiSelect = False
                Alert_Setting_Pnl.Enabled = False
            Else
                Alert_Tmr.Stop()
                Pause_Alert_Timer_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Play
                Available_Alerts_DGV.MultiSelect = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Hide_Windows_Desktop_Icons_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Hide_Windows_Desktop_Icons_ChkBx.CheckedChanged

    End Sub

    Private Sub Available_Alerts_DGV_KeyDown(sender As Object, e As KeyEventArgs) Handles Available_Alerts_DGV.KeyDown
        If e.KeyCode = Keys.Delete Then
            Delete_Alert_Btn.PerformClick()
        End If
    End Sub

    Private Sub Form_Is_Restricted_By_Screen_Bounds_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckedChanged

    End Sub

    Private Sub MagNote_PctrBx_MouseClick(sender As Object, e As MouseEventArgs) Handles MagNote_PctrBx.MouseClick
        If e.Button = MouseButtons.Right Then
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                Me.WindowState = FormWindowState.Normal
            End If
        Else
            Me.WindowState = FormWindowState.Minimized
        End If
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles Hide_Me_PctrBx.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Available_Alerts_DGV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Available_Alerts_DGV.RowsAdded
        Available_Alerts_DGV.ClearSelection()
    End Sub
    Private Sub Alert_Active_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Alert_Active_ChkBx.CheckedChanged

    End Sub

    Private Sub Hide_Windows_Desktop_Icons_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Hide_Windows_Desktop_Icons_ChkBx.CheckStateChanged
        If Hide_Windows_Desktop_Icons_ChkBx.CheckState = CheckState.Checked Then
            ShowDesktopIcons(False)
        Else
            ShowDesktopIcons(True)
        End If
    End Sub
    Private Sub Form_Is_Restricted_By_Screen_Bounds_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckStateChanged
        If Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckState = CheckState.Checked Then
            Restricter = New FormBoundsRestricter(Me)
        End If
    End Sub
    Private Sub New_Alert_Btn_Click(sender As Object, e As EventArgs) Handles New_Alert_Btn.Click
        If Language_Btn.Text = "E" Then
            Msg = "هل تريد انشاء تنبية جديد?"
        Else
            Msg = "D You Want To Creat New Alert?"
        End If
        If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
            Exit Sub
        End If
        If Not Alert_Setting_Pnl.Enabled Then
            Me.ActiveControl = Enable_Update_Btn
            Enable_Update_Btn_Click(Enable_Update_Btn, EventArgs.Empty)
        End If
        ClearAlertInfo()
    End Sub
    Private Function ClearAlertInfo() As Boolean
        Alert_Voice_Path_TxtBx.Text = Nothing
        Available_Alerts_DGV.ClearSelection()
        Alert_Comment_TxtBx.Text = Nothing
        Start_Alert_Time_DtTmPikr.Value = Now
        End_Alert_Time_DtTmPikr.Value = Start_Alert_Time_DtTmPikr.Value.AddMinutes(1)
        Related_To_MagNote_ChkBx.CheckState = CheckState.Unchecked
        Alert_Active_ChkBx.CheckState = CheckState.Unchecked
        Alert_Repeet_Day_NmrcUpDn.Value = 0
        Alert_Repeet_Minute_NmrcUpDn.Value = 0
        Alert_Repeet_Hour_NmrcUpDn.Value = 0
        Infinity_Alert_ChkBx.CheckState = CheckState.Unchecked
        Add_Alert_Flder_Files_Btn.Enabled = False
        Start_Alert_Time_DtTmPikr.Value = Now
        Force_Stop_Playing_Current_File_ChkBx.CheckState = CheckState.Unchecked
        Minutes_Between_Repetitions_NmrcUpDn.Value = 0
        Hours_Between_Repetitions_NmrcUpDn.Value = 0
        Days_Between_Repetitions_NmrcUpDn.Value = 0
        Alert_Sound_Volume_TrkBr.Value = GetVolume()
        Alert_Files_DGV.Rows.Clear()
        ClearCurrentRow(Available_Alerts_DGV)
        ClearCurrentRow(Alert_Files_DGV)
    End Function
    Private Sub ClearCurrentRow(ByVal DGV As DataGridView)
        ' Set the CurrentCell to Nothing to effectively remove focus from any row
        DGV.CurrentCell = Nothing
        ' Optionally, you can clear the selection to ensure no rows are selected
        DGV.ClearSelection()
        DGV.Refresh()
    End Sub
    Private Sub TbCntrl_DrawItem(sender As Object, e As DrawItemEventArgs)
        Dim Actvcntrl = Me.ActiveControl

        Try
            Select Case sender.name
                Case Setting_TbCntrl.Name
                    TabHeader(Setting_TbCntrl, e, Setting_TbCntrl)
                Case ShortCut_TbCntrl.Name
                    TabHeader(ShortCut_TbCntrl, e, ShortCut_TbCntrl)
                Case MagNotes_Notes_TbCntrl.Name
                    TabHeader(MagNotes_Notes_TbCntrl, e, MagNotes_Notes_TbCntrl)
                Case Projects_TbCntrl.Name
                    TabHeader(Projects_TbCntrl, e, Projects_TbCntrl)
            End Select
        Catch ex As Exception
        Finally
            Try
                Actvcntrl.Focus()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Sub TabHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs, ByVal TabControl1 As Windows.Forms.TabControl)
        If e.Index = 0 And
                TabControl1.SelectedIndex = 0 Then
            Dim rec As Rectangle = sender.ClientRectangle
            Dim backColor As SolidBrush = New SolidBrush(Note_Back_Color_ClrCmbBx.SelectedItem)
            e.Graphics.FillRectangle(backColor, rec)
        End If
        Dim tp As TabPage = TabControl1.TabPages(e.Index)
        Dim br As Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height - 1)
        Dim r1 As Rectangle = e.Bounds
        sf.Alignment = StringAlignment.Center
        r1.Location = New Point(r1.X + 0, r1.Y)
        r1.Size = New Size(r1.Width - 0, r1.Height)
        Dim strTitle As String = tp.Text
        If TabControl1.SelectedIndex = e.Index Then
            br = New SolidBrush(Note_Font_Color_ClrCmbBx.SelectedItem)
            e.Graphics.FillRectangle(br, r1)
            br = New SolidBrush(Note_Back_Color_ClrCmbBx.SelectedItem)
            Dim fonte As Font = TabControl1.Font
            fonte = New Font(fonte, FontStyle.Bold)
            e.Graphics.DrawString(strTitle, fonte, br, r, sf)
        Else
            If e.Index = 0 And
                TabControl1.SelectedIndex <> 0 Then
                Dim rec As Rectangle = sender.ClientRectangle
                Dim backColor As SolidBrush = New SolidBrush(Note_Back_Color_ClrCmbBx.SelectedItem)
                e.Graphics.FillRectangle(backColor, rec)
            End If
            br = New SolidBrush(Note_Back_Color_ClrCmbBx.SelectedItem)
            e.Graphics.FillRectangle(br, r1)
            br = New SolidBrush(Note_Font_Color_ClrCmbBx.SelectedItem)
            e.Graphics.DrawString(strTitle, TabControl1.Font, br, r, sf)
        End If
    End Sub

    Private Sub Country_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles _
                        Country_CmbBx.Validating,
                        City_CmbBx.Validating,
                        Calculation_Methods_CmbBx.Validating,
                        Fagr_Voice_Files_CmbBx.Validating,
                        Voice_Azan_Files_CmbBx.Validating
        If ActiveControl.Name <> sender.name Then
            Exit Sub
        End If
        If sender.SelectedIndex = -1 And sender.text.length > 0 Then
            If Language_Btn.Text = "ع" Then
                Msg = "Kindly Select Correct Item " & sender.name
            Else
                Msg = "من فضلك اختار البيان الصحيح " & sender.name
            End If
            ShowMsg(Msg & vbNewLine & sender.name, "InfoSysMe (" & Application.ProductName & ")", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MBOs, False, 0)
            sender.focus
            e.Cancel = True
        End If
    End Sub

    Private Sub Encrypt_Selected_Cell_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Encrypt_Selected_Cell_TlStrpMnItm.Click
        Try
            If Not Grid.CurrentWorksheet.SelectionRange.IsSingleCell Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Encrypt Or Decrypt Is Valid Only For One Cell..."
                Else
                    Msg = "تشفير او فك التشفير فعال فقط مع خلية واحدة فقط"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            Copy()
            Dim CopedCell = Clipboard.GetText
            CopedCell = "Encrypted Word [" & Encrypt_Function(CopedCell) & "]"
            My.Computer.Clipboard.Clear()
            My.Computer.Clipboard.SetText(CopedCell, TextDataFormat.Text)
            Paste()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Decrypt_Selected_Cell_TlStrpMnItm_Click(sender As Object, e As EventArgs) Handles Decrypt_Selected_Cell_TlStrpMnItm.Click, Copy_Encrypted_Cell_TlStrpMnItm.Click
        Try
            If Not Grid.CurrentWorksheet.SelectionRange.IsSingleCell Then
                If Language_Btn.Text = "ع" Then
                    Msg = "Encrypt Or Decrypt Is Valid Only For One Cell..."
                Else
                    Msg = "تشفير او فك التشفير فعال فقط مع خلية واحدة فقط"
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If sender.name = Copy_Encrypted_Cell_TlStrpMnItm.Name Then
                If CheckEnterPasswordToPass() Then Exit Sub
            End If

            Copy()
            Dim CopedCell = Clipboard.GetText
            CopedCell = Decrypt_Function(Replace(Replace(CopedCell, "Encrypted Word [", ""), "]", ""))
            My.Computer.Clipboard.Clear()
            My.Computer.Clipboard.SetText(CopedCell, TextDataFormat.Text)
            If sender.name = Decrypt_Selected_Cell_TlStrpMnItm.Name Then
                Paste()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub


    Private Sub Infinity_Alert_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Infinity_Alert_ChkBx.CheckedChanged

    End Sub

    Private Sub MagNotes_Notes_TbCntrl_MouseClick(sender As Object, e As MouseEventArgs) Handles MagNotes_Notes_TbCntrl.MouseClick
    End Sub

    Private Sub ShortCut_TbCntrl_Click(sender As Object, e As EventArgs) Handles ShortCut_TbCntrl.Click
    End Sub

    Private Sub Setting_TbCntrl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Setting_TbCntrl.SelectedIndexChanged, ShortCut_TbCntrl.SelectedIndexChanged, MagNotes_Notes_TbCntrl.SelectedIndexChanged
        MagNotes_Notes_TbCntrl.Invalidate()
        Setting_TbCntrl.Invalidate()
        ShortCut_TbCntrl.Invalidate()
        If Setting_TbCntrl.SelectedTab.Name = "Project_Situation_TbPg" Then
            MagNote_No_CmbBx.Enabled = False
        Else
            MagNote_No_CmbBx.Enabled = True
        End If
    End Sub

    Private Sub Infinity_Alert_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Infinity_Alert_ChkBx.CheckStateChanged
        If ActiveControl.Name = sender.name And
                Infinity_Alert_ChkBx.CheckState = CheckState.Checked Then
            If Language_Btn.Text = "E" Then
                Msg = "هل تريد جعل التنبية لانهائى?"
            Else
                Msg = "D You Want Make Alert Infinity?"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                Infinity_Alert_ChkBx.CheckState = CheckState.Unchecked
                Exit Sub
            End If
        End If
        If Infinity_Alert_ChkBx.CheckState = CheckState.Checked Then
            Infinity_Alert_ChkBx.BackgroundImage = Global.MagNote.My.Resources.Resources.InfinityGreen
        Else
            Infinity_Alert_ChkBx.BackgroundImage = Global.MagNote.My.Resources.Resources.Infinity
        End If
    End Sub
    Dim Alert_Voice_Files_CmbBx As New ComboBox

    Private Sub Mute_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Mute_ChkBx.CheckedChanged

    End Sub
    Private Sub Project_Situation_TbPg_Click(sender As Object, e As EventArgs) Handles Project_Situation_TbPg.Click

    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles Project_Duration_Month_NmrcUpDn.ValueChanged

    End Sub

    Private Sub Activate_Projects_TabPage_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Activate_Projects_TabPage_ChkBx.CheckedChanged

    End Sub
    Public Function DoRecursiveSearch(ByVal DirPath As String,
                                                              Optional ByVal FileSearchPattern As String = "*.*",
                                                              Optional ByVal FolderSearchPattern As String = "*",
                                                              Optional ByVal Recursive As Boolean = True) As ArrayList
        ' Specify the directories you want to manipulate.
        Dim di As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(DirPath)
        Dim ar As New ArrayList
        Try
            ' Get only subdirectories
            Dim dirs As System.IO.FileSystemInfo() = di.GetDirectories(FolderSearchPattern)
            Dim files As System.IO.FileSystemInfo()
            Dim diNext As System.IO.DirectoryInfo
            Dim fiNext As System.IO.FileInfo

            files = di.GetFiles(FileSearchPattern)
            For Each fiNext In files
                ar.Add(fiNext)
                Alert_Voice_Files_CmbBx.Items.Add(New KeyValuePair(Of String, String)(fiNext.FullName, Path.GetFileNameWithoutExtension(fiNext.FullName)))
            Next

            If Recursive Then
                For Each diNext In dirs
                    ar.AddRange(DoRecursiveSearch(diNext.FullName, FileSearchPattern, FolderSearchPattern, Recursive))
                Next
            End If
            Return ar
        Catch e As Exception
            Console.WriteLine("The process failed: {0}", e.ToString())
        End Try

    End Function

    Private Sub Need_Approval_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Need_Approval_ChkBx.CheckedChanged

    End Sub

    Private Sub Approved_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Approved_ChkBx.CheckedChanged

    End Sub

    Private Sub Mute_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Mute_ChkBx.CheckStateChanged
        If Mute_ChkBx.CheckState = CheckState.Checked Then
            Mute_ChkBx.BackgroundImage = Global.MagNote.My.Resources.Resources.MuteGreen
            wmp.controls.stop()
        Else
            Mute_ChkBx.BackgroundImage = Global.MagNote.My.Resources.Resources.MuteRed
        End If
    End Sub

    Private Sub Activate_Projects_TabPage_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Activate_Projects_TabPage_ChkBx.CheckStateChanged,
        Activate_MagNotes_TabPage_ChkBx.CheckStateChanged,
        Activate_Note_Parameters_TabPage_ChkBx.CheckStateChanged,
        Activate_Prayer_Time_TabPage_ChkBx.CheckStateChanged,
        Activate_ShortCuts_TabPage_ChkBx.CheckStateChanged,
        Activate_Alert_Time_TabPage_ChkBx.CheckStateChanged
        Try
            Dim TbPgNm = Replace(Replace(sender.name, "_TabPage_ChkBx", "_TbPg"), "Activate_", "")
            Select Case sender.name
                Case Activate_MagNotes_TabPage_ChkBx.Name
                    TbPgNm = "MagNotes_TbPg"
                Case Activate_Note_Parameters_TabPage_ChkBx.Name
                    TbPgNm = "Note_Parameters_TbPg"
                Case Activate_Prayer_Time_TabPage_ChkBx.Name
                    TbPgNm = "Prayer_Time_TbPg"
                Case Activate_ShortCuts_TabPage_ChkBx.Name
                    TbPgNm = "Shortcuts_TbPg"
                Case Activate_Alert_Time_TabPage_ChkBx.Name
                    TbPgNm = "Alert_Time_TbPg"
                Case Activate_Projects_TabPage_ChkBx.Name
                    TbPgNm = "Project_Situation_TbPg"
                    If Activate_Projects_TabPage_ChkBx.CheckState = CheckState.Checked Then
                        AddTabBack(TbPgNm)
                        Setting_TbCntrl.TabPages(TbPgNm).Enabled = True
                        Projects_TbCntrl.Enabled = True
                        Projects_TbCntrl.TabPages("Customers_TbPg").Enabled = False
                        Projects_TbCntrl.TabPages("Available_Projects_TbPg").Enabled = False
                        Projects_TbCntrl.TabPages("Project_Events_Details_TbPg").Enabled = False
                        Projects_TbCntrl.TabPages("User_Authorities_TbPg").Enabled = False
                        Projects_TbCntrl.TabPages("Available_Authorities_TbPg").Enabled = False
                        Dim CurrentMagNoteSelectedDataBase = Projects_Connection_Strings_CmbBx.Text
                        If Not AddAvailableDatabaseConnectinString() Then GoTo ExitSelecCase
                        If Projects_Connection_Strings_CmbBx.Items.Count > 0 Then
                            Projects_Connection_Strings_CmbBx.Enabled = True
                        End If
                        If Projects_Connection_Strings_CmbBx.Text.Length = 0 Then GoTo ExitSelecCase
                        If Projects_Connection_Strings_CmbBx.FindStringExact(Projects_Connection_Strings_CmbBx.Text) Then
                            Projects_Connection_Strings_CmbBx.SelectedItem = Projects_Connection_Strings_CmbBx.Text
                        End If
                    Else
                        RemoveCurrentTab(Setting_TbCntrl.TabPages(TbPgNm))
                        Projects_Connection_Strings_CmbBx.SelectedIndex = -1
                        Projects_Connection_Strings_CmbBx.Enabled = False
                        Projects_TbCntrl.TabPages("Users_TbPg").Enabled = False
                    End If
                    GoTo ExitSelecCase
            End Select
            If sender.CheckState = CheckState.Checked Then
                AddTabBack(TbPgNm)
                Setting_TbCntrl.TabPages(TbPgNm).Enabled = True
            Else
                Setting_TbCntrl.TabPages(TbPgNm).Enabled = False
                RemoveCurrentTab(Setting_TbCntrl.TabPages(TbPgNm))
            End If

ExitSelecCase:
            Setting_TbCntrl.Invalidate()
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Function AddAvailableDatabaseConnectinString() As Boolean
        Projects_Connection_Strings_CmbBx.Items.Clear()
        For Each database In ConfigurationManager.ConnectionStrings
            Projects_Connection_Strings_CmbBx.Items.Add(database.name)
        Next
        If Projects_Connection_Strings_CmbBx.Items.Count > 0 Then
            Return True
        End If
    End Function
    Private Sub Application_Starts_Minimized_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Application_Starts_Minimized_ChkBx.CheckedChanged

    End Sub

    Private Sub Preview_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Opened_Project_Details_ChkBx.CheckStateChanged,
            Opened_Project_ChkBx.CheckStateChanged
        Select Case Opened_Project_Details_ChkBx.CheckState
            Case CheckState.Checked
                If Language_Btn.Text = "E" Then
                    Opened_Project_Details_ChkBx.Text = "مغلق"
                Else
                    Opened_Project_Details_ChkBx.Text = "Closed"
                End If
            Case CheckState.Unchecked
                If Language_Btn.Text = "E" Then
                    Opened_Project_Details_ChkBx.Text = "مفتوح"
                Else
                    Opened_Project_Details_ChkBx.Text = "Opened"
                End If
            Case CheckState.Indeterminate
                If Language_Btn.Text = "E" Then
                    Opened_Project_Details_ChkBx.Text = "الكل"
                Else
                    Opened_Project_Details_ChkBx.Text = "ALL"
                End If
        End Select
    End Sub

    Private Sub Update_User_Btn_Click(sender As Object, e As EventArgs) Handles Update_User_Btn.Click
        If Projects_Connection_Strings_CmbBx.SelectedIndex = -1 Then
            Exit Sub
        End If
        If Projects_TbCntrl.TabPages("Customers_TbPg").Enabled = False Then
            If Language_Btn.Text = "E" Then
                Msg = "ربما لم يتم اختيار المستخدم بعد للعمل عليه، أو لم يتم إنشاء تسجيل دخول إلى النظام"
            Else
                Msg = "Maybe The User Has Not Yet Been Selected To Work On It, Or A Login Has Not Been Created To Enter The System"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If Logged_In_User_No_TxtBx.Text <> User_No_TxtBx.Text Then
            If Not UserAuthorized("Update Another Users") Then Exit Sub
        End If
        If Not UserAuthorized("Update Users") Then Exit Sub
        Using QS As New Query_Class("Users")
            QS.QueryAddWhere("User_No", User_No_TxtBx.Text)
            If Not QS.QueryFieldValue(False) Then
                QS.InsertUpdateColumnsValue.Add("User_No", User_No_TxtBx.Text)
            End If
            QS.InsertUpdateColumnsValue.Add("User_Name", User_Name_CmbBx.Text)
            QS.InsertUpdateColumnsValue.Add("Password", Encrypt_Function(Password_TxtBx.Text, User_Name_CmbBx.Text))
            QS.InsertUpdateColumnsValue.Add("E_Mail", E_Mail_TxtBx.Text)
            QS.InsertUpdateColumnsValue.Add("Cellular_No", Cellular_No_TxtBx.Text)
            QS.DoInsertUpdate()
        End Using
    End Sub

    Public DoDataChangesFields(0) As DoDataChangesDefine
    Public DoDataChangesKeys(0) As DoDataChangesDefine
    Structure DoDataChangesDefine
        Public Table_Fiedl_Name As String
        Public Object_Value As String
    End Structure
    ''' <summary>
    ''' ToDo True = Update
    ''' ToDo False = Delete
    ''' </summary>
    ''' <param name="TableName"></param>
    ''' <param name="Keys"></param>
    ''' <param name="Fields"></param>
    ''' <param name="ToDo"></param>
    ''' <returns></returns>
    Public Function DoDataChanges(ByVal TableName As String,
                                                             ByVal Keys() As DoDataChangesDefine,
                                                             ByVal Fields() As DoDataChangesDefine,
                                                             ByVal ToDo As Boolean) As Boolean
        Try
            If Logged_In_User_Name_TxtBx.TextLength = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "ربما لم يتم اختيار المستخدم بعد للعمل عليه، أو لم يتم إنشاء تسجيل دخول إلى النظام"
                Else
                    Msg = "Maybe The User Has Not Yet Been Selected To Work On It, Or A Login Has Not Been Created To Enter The System"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                Return False
            End If
            Using QS As New Query_Class(TableName)
                For Each Key In Keys
                    QS.QueryAddWhere(Key.Table_Fiedl_Name, Key.Object_Value)
                Next
                If Not ToDo Then
                    If QS.QueryFieldValue() Then 'Update Or Delete
                        QS.DoDelete()
                    End If
                Else
                    If QS.QueryFieldValue() Then 'Update Or Delete
                        For Each Field In Fields
                            QS.InsertUpdateColumnsValue.Add(Field.Table_Fiedl_Name, Field.Object_Value)
                        Next
                        QS.InsertUpdateColumnsValue.Add("Last_Update_User", Logged_In_User_No_TxtBx)
                        QS.InsertUpdateColumnsValue.Add("Last_Update_Time", ConvertDateTime(Now))

                        QS.DoInsertUpdate()
                    Else 'new
                        For Each Key In Keys
                            QS.InsertUpdateColumnsValue.Add(Key.Table_Fiedl_Name, Key.Object_Value)
                        Next
                        For Each Field In Fields
                            QS.InsertUpdateColumnsValue.Add(Field.Table_Fiedl_Name, Field.Object_Value)
                        Next
                        QS.InsertUpdateColumnsValue.Add("Creation_User", Logged_In_User_No_TxtBx)
                        QS.InsertUpdateColumnsValue.Add("Creation_Time", ConvertDateTime(Now))
                        QS.InsertUpdateColumnsValue.Add("Last_Update_User", Logged_In_User_No_TxtBx)
                        QS.InsertUpdateColumnsValue.Add("Last_Update_Time", ConvertDateTime(Now))
                        QS.DoInsertUpdate()
                    End If
                End If
            End Using
        Catch ex As Exception
            Using QS As New Query_Class(Nothing)
                QS.DisplayAIOCriticalError(ex)
            End Using
        End Try
    End Function
    Private Sub Update_Authorities_Btn_Click(sender As Object, e As EventArgs) Handles Update_User_Authorities_Btn.Click
        If Authority_User_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر المستخدم الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The User You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If User_Authority_Name_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر الصلاحية الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Authority You Want To Use First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If Not UserAuthorized("Update User Authorities") Then
            Exit Sub
        End If
        ReDim DoDataChangesKeys(0)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = DirectCast(Authority_User_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "User_No"
        ReDim Preserve DoDataChangesKeys(DoDataChangesKeys.Length)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = User_Authority_Name_CmbBx.Text
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Authority_Name"
        ReDim DoDataChangesFields(0)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = User_Authority_Description_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Description"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Active_ChkBx.CheckState
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Active"

        DoDataChanges("User_Authorities", DoDataChangesKeys, DoDataChangesFields, 1)
    End Sub
    Private Function UserAuthorized(ByVal AuthorityName) As Boolean
        Using QS As New Query_Class("User_Authorities")
            QS.QueryAddWhere("User_No", Logged_In_User_No_TxtBx.Text)
            QS.QueryAddWhere("Authority_Name", AuthorityName)
            QS.QueryAddWhere("Active", 1)
            If QS.QueryFieldValue() Then
                Return True
            Else
                If Language_Btn.Text = "E" Then
                    Msg = "غير مرخص لك التعامل مع هذة الصلاحية"
                Else
                    Msg = "You Are Not Allowed To Use This Authority"
                End If
                ShowMsg(Msg & vbNewLine & "(" & AuthorityName & ")",, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            End If
        End Using
    End Function

    Private Sub Preview_Availabel_Users_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Availabel_Users_Btn.Click
        Try
            Using QS As New Query_Class("Users")
                QS.QueryFieldValue()
                Availabel_Users_DGV.DataSource = QS.Query_DS.Tables("Users")
                User_Name_CmbBx.Items.Clear()
                Authority_User_CmbBx.Items.Clear()
                User_Name_CmbBx.Text = Nothing
                Authority_User_CmbBx.Text = Nothing
                For Each User In QS.Query_DS.Tables("Users").Rows
                    User_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("User_No"), User.item("User_Name")))
                    Authority_User_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("User_No"), User.item("User_Name")))
                Next
                Availabel_Users_DGV.ClearSelection()
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Preview_Available_Projects_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Available_Projects_Btn.Click
        Using QS As New Query_Class("Projects")
            If Opened_Project_ChkBx.CheckState = CheckState.Unchecked Then
                QS.QueryAddWhere("Project_Opened", 0)
            ElseIf Opened_Project_ChkBx.CheckState = CheckState.Checked Then
                QS.QueryAddWhere("Project_Opened", 1)
            End If
            QS.QueryFieldValue()
            Available_Projects_DGV.DataSource = QS.Query_DS.Tables("Projects")

            Project_Name_CmbBx.Items.Clear()
            Project_Detail_Project_Name_CmbBx.Items.Clear()
            If Not IsNothing(QS.Query_DS.Tables("Projects")) Then
                For Each Project In QS.Query_DS.Tables("Projects").Rows
                    Project_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Project.item("Project_No"), Project.item("Project_Name").ToString))
                    Project_Detail_Project_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Project.item("Project_No"), Project.item("Project_Name").ToString))
                Next
            End If
        End Using
        Using QS As New Query_Class("Users")
            QS.QueryFieldValue()
            Employee_Name_CmbBx.Items.Clear()
            If Not IsNothing(QS.Query_DS.Tables("Users")) Then
                For Each User In QS.Query_DS.Tables("Users").Rows
                    Employee_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("User_No"), User.item("User_Name")))
                    Event_Owner_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("User_No"), User.item("User_Name")))
                    Approval_From_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("User_No"), User.item("User_Name")))
                Next
            End If
        End Using
        Using QS As New Query_Class("Customers")
            QS.QueryFieldValue()
            Project_Customer_Name_CmbBx.Items.Clear()
            If Not IsNothing(QS.Query_DS.Tables("Customers")) Then
                For Each User In QS.Query_DS.Tables("Customers").Rows
                    Project_Customer_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("Customer_No"), User.item("Customer_Name")))
                Next
            End If
        End Using
        Available_Projects_DGV.ClearSelection()

    End Sub

    Private Sub Preview_Project_Details_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Project_Details_Btn.Click
        Using QS As New Query_Class("Project_Events_View")
            If Project_Detail_Project_Name_CmbBx.SelectedIndex <> -1 Then
                QS.QueryAddWhere("Project_No", DirectCast(Project_Detail_Project_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key)
            End If
            If Opened_Project_Details_ChkBx.CheckState = CheckState.Unchecked Then
                QS.QueryAddWhere("Event_Status", 0)
            ElseIf Opened_Project_Details_ChkBx.CheckState = CheckState.Checked Then
                QS.QueryAddWhere("Event_Status", 1)
            End If

            QS.QueryFieldValue()
            Project_Events_Details_DGV.DataSource = QS.Query_DS.Tables("Project_Events_View")


        End Using
        Project_Events_Details_DGV.ClearSelection()
    End Sub

    Private Sub Preview_Availabel_Customers_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Availabel_Customers_Btn.Click
        Using QS As New Query_Class("Customers")
            QS.QueryFieldValue()
            Availabel_Customers_DGV.DataSource = QS.Query_DS.Tables("Customers")

            Customer_Name_CmbBx.Items.Clear()
            If Not IsNothing(QS.Query_DS.Tables("Customers")) Then
                For Each User In QS.Query_DS.Tables("Customers").Rows
                    Customer_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("Customer_No"), User.item("Customer_Name")))
                Next
            End If
        End Using
        Availabel_Customers_DGV.ClearSelection()

    End Sub

    Private Sub Delete_User_Btn_Click(sender As Object, e As EventArgs) Handles Delete_User_Btn.Click
        If Projects_Connection_Strings_CmbBx.SelectedIndex = -1 Then
            Exit Sub
        End If
        Using QS As New Query_Class("Users")
            QS.QueryAddWhere("User_No", User_No_TxtBx.Text)
            If QS.QueryFieldValue(False) Then
                QS.DoDelete()
            End If
        End Using
    End Sub

    Private Sub User_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles User_No_TxtBx.TextChanged
        Dim Users As New Query_Class("Users",
                New Dictionary(Of String, String) From {
                                {"User_No", User_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {User_Name_CmbBx, "User_Name"},
                                {Cellular_No_TxtBx, "Cellular_No"},
                                {E_Mail_TxtBx, "E_Mail"}})
        Password_TxtBx.Text = Nothing
        If Users.OldRecord Then
            E_Mail_TxtBx.ReadOnly = True
        End If
    End Sub

    Private Sub Availabel_Users_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Availabel_Users_DGV.CellContentClick

    End Sub

    Private Sub Approved_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Approved_ChkBx.CheckStateChanged
        Select Case Approved_ChkBx.CheckState
            Case CheckState.Checked
                If Language_Btn.Text = "E" Then
                    Approved_ChkBx.Text = "موافقة"
                Else
                    Approved_ChkBx.Text = "Approved"
                End If
            Case CheckState.Unchecked
                If Language_Btn.Text = "E" Then
                    Approved_ChkBx.Text = "ليس بعد"
                Else
                    Approved_ChkBx.Text = "Not Yet"
                End If
            Case CheckState.Indeterminate
                If Language_Btn.Text = "E" Then
                    Approved_ChkBx.Text = "مرفوض"
                Else
                    Approved_ChkBx.Text = "Refused"
                End If
        End Select
        If ActiveControl.Name = sender.name Then
            Approved_Time_TxtBx.Text = Now
            Approved_User_No_TxtBx.Text = User_No_TxtBx.Text
        End If
    End Sub

    Private Sub User_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles User_Name_CmbBx.SelectedIndexChanged
        If User_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        User_No_TxtBx.Text = DirectCast(User_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
    End Sub

    Private Sub Password_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Password_TxtBx.TextChanged
        Password_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Password_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        If Password_TxtBx.TextLength = 0 Then Exit Sub
        Using QS As New Query_Class("Users")
            QS.QueryAddWhere("User_No", User_No_TxtBx.Text)
            If QS.QueryFieldValue() Then
                Dim StoredPassword = Decrypt_Function(QS.FillAddObjField(False, "Password"), User_Name_CmbBx.Text)
                If StoredPassword = Password_TxtBx.Text Then
                    Password_TxtBx.BackColor = Color.Green
                    Reset_Password_Btn.Enabled = False
                    Show_Password_Btn.Enabled = False
                    E_Mail_TxtBx.ReadOnly = False
                Else
                    Password_TxtBx.BackColor = Color.Red
                    Reset_Password_Btn.Enabled = True
                    Show_Password_Btn.Enabled = True
                    E_Mail_TxtBx.ReadOnly = True
                End If
            End If
        End Using
    End Sub

    Private Sub LogIn_Btn_Click(sender As Object, e As EventArgs) Handles LogIn_Btn.Click
        If User_Name_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر المستخدم الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The User You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        Using QS As New Query_Class("Users")
            QS.QueryAddWhere("User_No", User_No_TxtBx.Text)
            If QS.QueryFieldValue() Then
                Dim StoredPassword = Decrypt_Function(QS.FillAddObjField(False, "Password"), User_Name_CmbBx.Text)
                If StoredPassword = Password_TxtBx.Text Then
                    Logged_In_User_No_TxtBx.Text = User_No_TxtBx.Text
                    Logged_In_User_Name_TxtBx.Text = User_Name_CmbBx.Text
                    Projects_TbCntrl.TabPages("Customers_TbPg").AutoScroll = True
                    Projects_TbCntrl.TabPages("Customers_TbPg").Enabled = True
                    Projects_TbCntrl.TabPages("Available_Projects_TbPg").AutoScroll = True
                    Projects_TbCntrl.TabPages("Available_Projects_TbPg").Enabled = True
                    Projects_TbCntrl.TabPages("Project_Events_Details_TbPg").AutoScroll = True
                    Projects_TbCntrl.TabPages("Project_Events_Details_TbPg").Enabled = True
                    Projects_TbCntrl.TabPages("User_Authorities_TbPg").AutoScroll = True
                    Projects_TbCntrl.TabPages("User_Authorities_TbPg").Enabled = True
                    Projects_TbCntrl.TabPages("Available_Authorities_TbPg").AutoScroll = True
                    Projects_TbCntrl.TabPages("Available_Authorities_TbPg").Enabled = True
                    Preview_Availabel_Customers_Btn_Click(Preview_Availabel_Customers_Btn, EventArgs.Empty)
                    Preview_Available_Projects_Btn_Click(Preview_Available_Projects_Btn, EventArgs.Empty)
                    Preview_Project_Details_Btn_Click(Preview_Project_Details_Btn, EventArgs.Empty)
                    Preview_Available_Authorities_Btn_Click(Preview_Available_Authorities_Btn, EventArgs.Empty)
                Else
                    Logged_In_User_No_TxtBx.Text = Nothing
                    Logged_In_User_Name_TxtBx.Text = Nothing
                    If Language_Btn.Text = "E" Then
                        Msg = "كلمة السر غير صحيحة... حاول مرة اخرى"
                    Else
                        Msg = "Not Correct Password... Try Again"
                    End If
                    ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                End If
            Else
                If Language_Btn.Text = "E" Then
                    Msg = "لا يوجد مستخدم بهذا الرقم"
                Else
                    Msg = "No User By Thes Number"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            End If
        End Using
    End Sub

    Private Sub Preview_User_Authorities_Btn_Click(sender As Object, e As EventArgs) Handles Preview_User_Authorities_Btn.Click
        User_Authorities_DGV.DataSource = Nothing
        If Authority_User_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "لا يوجد مستخدم بهذا الرقم"
            Else
                Msg = "No User By Thes Number"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        Using QS As New Query_Class("User_Authorities")
            QS.QueryAddWhere("User_No", DirectCast(Authority_User_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key)
            QS.QueryFieldValue()
            User_Authorities_DGV.DataSource = QS.Query_DS.Tables("User_Authorities")
        End Using
        User_Authorities_DGV.ClearSelection()
    End Sub

    Private Sub Authority_Description_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles User_Authority_Description_TxtBx.TextChanged

    End Sub

    Private Sub Preview_Available_Authorities_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Available_Authorities_Btn.Click
        Available_Authorities_DGV.DataSource = Nothing
        Authority_Name_CmbBx.Items.Clear()
        Using QS As New Query_Class("Authorities")
            QS.QueryFieldValue()
            Available_Authorities_DGV.DataSource = QS.Query_DS.Tables("Authorities")
            For Each Authority In QS.Query_DS.Tables("Authorities").Rows
                Authority_Name_CmbBx.Items.Add(Authority.item("Authority_Name"))
                User_Authority_Name_CmbBx.Items.Add(Authority.item("Authority_Name"))
            Next
        End Using
        Available_Authorities_DGV.ClearSelection()
    End Sub

    Private Sub Update_Authority_Btn_Click(sender As Object, e As EventArgs) Handles Update_Authority_Btn.Click
        ReDim DoDataChangesKeys(0)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = Authority_Name_CmbBx.Text
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Authority_Name"
        ReDim DoDataChangesFields(0)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Authority_Description_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Description"
        DoDataChanges("Authorities", DoDataChangesKeys, DoDataChangesFields, 1)
    End Sub

    Private Sub Authority_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Authority_Name_CmbBx.SelectedIndexChanged
        If Authority_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        Dim Stores As New Query_Class("Authorities",
                New Dictionary(Of String, String) From {
                                {"Authority_Name", Authority_Name_CmbBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Authority_Description_TxtBx, "Description"}})
    End Sub

    Private Sub Available_Authorities_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_Authorities_DGV.CellContentClick

    End Sub

    Private Sub Need_Approval_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Need_Approval_ChkBx.CheckStateChanged
        Select Case Need_Approval_ChkBx.CheckState
            Case CheckState.Checked
                Approval_Pnl.Enabled = True
            Case CheckState.Unchecked
                Approval_Pnl.Enabled = False
        End Select
    End Sub

    Private Sub Availabel_Customers_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Availabel_Customers_DGV.CellContentClick

    End Sub

    Private Sub Update_Customer_Btn_Click(sender As Object, e As EventArgs) Handles Update_Customer_Btn.Click
        If Customer_Name_CmbBx.Text.Length = 0 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر العميل الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Cutomer You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If

        ReDim DoDataChangesKeys(0)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = Customer_No_TxtBx.Text
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Customer_No"
        ReDim DoDataChangesFields(0)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Customer_Name_CmbBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Customer_Name"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Customer_Cellulars_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Customer_Cellulars"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Customer_Contacts_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Customer_Contacts"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Customer_Email_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Customer_Email"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Customer_Address_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Customer_Address"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Customer_Description_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Description"

        DoDataChanges("Customers", DoDataChangesKeys, DoDataChangesFields, 1)
    End Sub

    Private Sub Delete_Customer_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Customer_Btn.Click
        If Customer_Name_CmbBx.Text.Length = 0 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر العميل الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Cutomer You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If

        ReDim DoDataChangesKeys(0)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = Customer_No_TxtBx.Text
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Customer_No"
        DoDataChanges("Customers", DoDataChangesKeys, DoDataChangesFields, 0)
        Preview_Availabel_Customers_Btn_Click(Preview_Availabel_Customers_Btn, EventArgs.Empty)
    End Sub

    Private Sub Application_Starts_Minimized_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Application_Starts_Minimized_ChkBx.CheckStateChanged
        Select Case Application_Starts_Minimized_ChkBx.CheckState
            Case CheckState.Unchecked
                If Language_Btn.Text = "E" Then
                    Application_Starts_Minimized_ChkBx.Text = "البرنامج يبدا طبيعى"
                Else
                    Application_Starts_Minimized_ChkBx.Text = "Program Starts Normal"
                End If
            Case CheckState.Checked
                If Language_Btn.Text = "E" Then
                    Application_Starts_Minimized_ChkBx.Text = "البرنامج يبدا مصغر"
                Else
                    Application_Starts_Minimized_ChkBx.Text = "Program Starts Minimized"
                End If
            Case CheckState.Indeterminate
                If Language_Btn.Text = "E" Then
                    Application_Starts_Minimized_ChkBx.Text = "البرنامج يبدا مكبر"
                Else
                    Application_Starts_Minimized_ChkBx.Text = "Program Starts Maximized"
                End If
        End Select
    End Sub

    Private Sub Customer_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Customer_No_TxtBx.TextChanged
        Dim Customers As New Query_Class("Customers",
                New Dictionary(Of String, String) From {
                                {"Customer_No", Customer_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Customer_Name_CmbBx, "Customer_Name"},
                                {Customer_Cellulars_TxtBx, "Customer_Cellulars"},
                                {Customer_Contacts_TxtBx, "Customer_Contacts"},
                                {Customer_Email_TxtBx, "Customer_Email"},
                                {Customer_Address_TxtBx, "Customer_Address"},
                                {Customer_Description_TxtBx, "Description"}})
    End Sub

    Private Sub Customer_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Customer_Name_CmbBx.SelectedIndexChanged
        If Customer_Name_CmbBx.SelectedIndex <> -1 Then
            Customer_No_TxtBx.Text = DirectCast(Customer_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            Project_Customer_No_TxtBx.Text = DirectCast(Customer_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        End If
    End Sub

    Private Sub Project_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Project_No_TxtBx.TextChanged
        Dim Project_Duration As String = String.Empty
        Dim Projects As New Query_Class("Projects",
                New Dictionary(Of String, String) From {
                                {"Project_No", Project_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Project_Name_CmbBx, "Project_Name"},
                                {Project_Description_TxtBx, "Description"},
                                {Project_Customer_No_TxtBx, "Customer_No"},
                                {Employee_No_TxtBx, "Employee_No"},
                                {Suggested_End_Date_DtTmPkr, "Suggested_End_Date"},
                                {Project_Opened_ChkBx, "Project_Opened"},
                                {Project_Starts_At_DtTmPkr, "Project_Starts_At"},
                                {Project_Duration, "Project_Duration"},
                                {Project_Ended_At_DtTmPkr, "Project_Ended_At"}})
        If Projects.OldRecord Then
            Projects.FillAddObjField(Project_Detail_Project_Name_CmbBx, "Project_Name")
            If Not String.IsNullOrEmpty(Project_Duration) Then
                Project_Duration_Day_NmrcUpDn.Value = Val(Split(Project_Duration, ":").ToList.Item(0))
                Project_Duration_Month_NmrcUpDn.Value = Val(Split(Project_Duration, ":").ToList.Item(1))
                Project_Duration_Year_NmrcUpDn.Value = Val(Split(Project_Duration, ":").ToList.Item(2))
            End If
        End If
    End Sub

    Private Sub Update_Project_Information_Btn_Click(sender As Object, e As EventArgs) Handles Update_Project_Information_Btn.Click
        If Employee_Name_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر الموظف الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Emplyee You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If

        If Project_Customer_Name_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر العميل الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Customer You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If Project_Name_CmbBx.Text.Length = 0 Or
                Project_No_TxtBx.TextLength = 0 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر المشروع الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Prjoect You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If

        ReDim DoDataChangesKeys(0)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = Project_No_TxtBx.Text
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Project_No"
        ReDim DoDataChangesFields(0)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Description_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Description"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Name_CmbBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Project_Name"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Customer_No_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Customer_No"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Employee_No_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Employee_No"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Suggested_End_Date_DtTmPkr.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Suggested_End_Date"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Opened_ChkBx.CheckState
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Project_Opened"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Starts_At_DtTmPkr.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Project_Starts_At"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Ended_At_DtTmPkr.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Project_Ended_At"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Duration_Day_NmrcUpDn.Value & ":" & Project_Duration_Month_NmrcUpDn.Value & ":" & Project_Duration_Year_NmrcUpDn.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Project_Duration"

        DoDataChanges("Projects", DoDataChangesKeys, DoDataChangesFields, 1)

    End Sub

    Private Sub Delete_Project_Information_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Project_Information_Btn.Click

    End Sub

    Private Sub Project_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Project_Name_CmbBx.SelectedIndexChanged
        If Project_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        Project_No_TxtBx.Text = DirectCast(Project_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        Project_Detail_Project_No_TxtBx.Text = DirectCast(Project_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
    End Sub

    Private Sub Project_Customer_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Project_Customer_Name_CmbBx.SelectedIndexChanged
        If Project_Customer_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        Project_Customer_No_TxtBx.Text = DirectCast(Project_Customer_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
    End Sub

    Private Sub Employee_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Employee_Name_CmbBx.SelectedIndexChanged
        If Employee_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        Employee_No_TxtBx.Text = DirectCast(Employee_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key

    End Sub

    Private Sub Project_Customer_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Project_Customer_No_TxtBx.TextChanged
        Dim Customers As New Query_Class("Customers",
                New Dictionary(Of String, String) From {
                                {"Customer_No", Project_Customer_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Project_Customer_Name_CmbBx, "Customer_Name"}})
    End Sub

    Private Sub Employee_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Employee_No_TxtBx.TextChanged
        Dim Stores As New Query_Class("Users",
                New Dictionary(Of String, String) From {
                                {"User_No", Employee_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Employee_Name_CmbBx, "User_Name"}})
    End Sub

    Private Sub Available_Projects_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_Projects_DGV.CellContentClick

    End Sub

    Private Sub Availabel_Users_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Availabel_Users_DGV.SelectionChanged
        If ActiveControl.Name <> sender.name Or
         Availabel_Users_DGV.SelectedRows.Count = 0 Then Exit Sub
        User_No_TxtBx.Text = Availabel_Users_DGV.CurrentRow.Cells("User_No").Value
    End Sub

    Private Sub Prject_Ended_At_DtTmPkr_ValueChanged(sender As Object, e As EventArgs) Handles Project_Ended_At_DtTmPkr.ValueChanged

    End Sub

    Private Sub Project_Opened_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Project_Opened_ChkBx.CheckedChanged
    End Sub


    Private Sub Available_Authorities_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Available_Authorities_DGV.SelectionChanged
        If Available_Authorities_DGV.SelectedRows.Count = 0 Or
            ActiveControl.Name <> sender.name Then
            Exit Sub
        End If
        Authority_Name_CmbBx.Text = Available_Authorities_DGV.CurrentRow.Cells("Authority_Name").Value
    End Sub

    Private Sub Opened_Project_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Opened_Project_ChkBx.CheckedChanged

    End Sub

    Private Sub Availabel_Customers_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Availabel_Customers_DGV.SelectionChanged
        If Availabel_Customers_DGV.SelectedRows.Count = 0 Then Exit Sub
        Customer_No_TxtBx.Text = Availabel_Customers_DGV.CurrentRow.Cells("Customer_No").Value
    End Sub

    Private Sub Opened_Project_Details_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Opened_Project_Details_ChkBx.CheckedChanged

    End Sub

    Private Sub Project_Detail_Project_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Project_Detail_Project_No_TxtBx.TextChanged
        Dim Project_Duration As String = String.Empty
        Dim Projects As New Query_Class("Projects",
                New Dictionary(Of String, String) From {
                                {"Project_No", Project_Detail_Project_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Project_Detail_Project_Name_CmbBx, "Project_Name"},
                                {Project_Description_TxtBx, "Description"},
                                {Project_Customer_No_TxtBx, "Customer_No"},
                                {Employee_No_TxtBx, "Employee_No"},
                                {Suggested_End_Date_DtTmPkr, "Suggested_End_Date"},
                                {Project_Opened_ChkBx, "Project_Opened"},
                                {Project_Starts_At_DtTmPkr, "Project_Starts_At"},
                                {Project_Duration, "Project_Duration"},
                                {Project_Ended_At_DtTmPkr, "Project_Ended_At"}})
        If Projects.OldRecord Then
            Projects.FillAddObjField(Project_Name_CmbBx, "Project_Name")
            If Not String.IsNullOrEmpty(Project_Duration) Then
                Project_Duration_Day_NmrcUpDn.Value = Val(Split(Project_Duration, ":").ToList.Item(0))
                Project_Duration_Month_NmrcUpDn.Value = Val(Split(Project_Duration, ":").ToList.Item(1))
                Project_Duration_Year_NmrcUpDn.Value = Val(Split(Project_Duration, ":").ToList.Item(2))
            End If
        End If

    End Sub

    Private Sub Project_Detail_Project_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Project_Detail_Project_Name_CmbBx.SelectedIndexChanged
        If Project_Detail_Project_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        Project_No_TxtBx.Text = DirectCast(Project_Detail_Project_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        Project_Detail_Project_No_TxtBx.Text = DirectCast(Project_Detail_Project_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key

    End Sub

    Private Sub Event_Owner_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Event_Owner_CmbBx.SelectedIndexChanged

    End Sub

    Private Sub Available_Projects_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Available_Projects_DGV.SelectionChanged
        If ActiveControl.Name <> sender.name Or
                Available_Projects_DGV.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        Project_No_TxtBx.Text = Available_Projects_DGV.CurrentRow.Cells("Project_No").Value
    End Sub

    Private Sub Event_Owner_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Event_Owner_TxtBx.TextChanged
        Dim Users As New Query_Class("Users",
                New Dictionary(Of String, String) From {
                                {"User_No", Event_Owner_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Event_Owner_CmbBx, "User_Name"}})
    End Sub

    Private Sub Project_Event_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Project_Event_No_TxtBx.TextChanged
        If Project_Detail_Project_Name_CmbBx.SelectedIndex = -1 And
            Project_Event_No_TxtBx.TextLength > 0 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر المشروع الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Prject You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        Dim Project_Events As New Query_Class("Project_Events",
                New Dictionary(Of String, String) From {
                                {"Project_No", Project_No_TxtBx.Text},
                                {"Project_Event_No", Project_Event_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Event_Owner_TxtBx, "Event_Owner"},
                                {Event_Label_TxtBx, "Event_Label"},
                                {Event_Starts_At_DtTmPkr, "Event_Starts_At"},
                                {Event_Status_ChkBx, "Event_Status"},
                                {Suggested_Ended_Date_DtTmPkr, "Suggested_End_Date"},
                                {Event_Ended_At_DtTmPkr, "Event_Ended_At"},
                                {Need_Approval_ChkBx, "Need_Approval"},
                                {Approval_From_No_TxtBx, "Approval_From"},
                                {Approved_ChkBx, "Approved"},
                                {Approved_User_No_TxtBx, "Approved_User"},
                                {Approved_Time_TxtBx, "Approved_Time"},
                                {Project_Event_Description_TxtBx, "Description"}})
        If Project_Events.OldRecord Then
            If Not String.IsNullOrEmpty(Project_Events.FillAddObjField(False, "Event_Grid")) Then
                Dim WorkingExcelFile = Application.StartupPath & "\WorkingExcelFile.xlsx"
                Dim myByteArray() As Byte = CType(Project_Events.Query_DT.Rows(0).Item("Event_Grid"), Byte())
                If myByteArray.Length > 0 Then
                    Dim myStream As New MemoryStream(myByteArray, True)
                    myStream.Write(myByteArray, 0, myByteArray.Length)
                    Dim Writer As System.IO.StreamWriter
                    Writer = New System.IO.StreamWriter(WorkingExcelFile)
                    myStream.WriteTo(Writer.BaseStream)
                    myStream.Close()
                    Writer.Close()
                    myStream.Dispose()
                    If File.Exists(WorkingExcelFile) Then
                        LoadFile(WorkingExcelFile)
                    End If
                End If
            Else
                If Not IsNothing(GridPnl) Then
                    GridPnl.Visible = False
                End If
            End If
            If Not IsInMagNoteCmbBx(MagNoteFolderPath & "\Project Event.txt", 1, MagNote_No_CmbBx) Then
                New_Detail_Btn_Click(New_Detail_Btn, EventArgs.Empty)
            End If
            RCSN.Rtf = Replace(Replace(Replace(Project_Events.FillAddObjField(False, "The_Event"), "]", ""), "[", ""), "''", "'")
        Else
            If IsInMagNoteCmbBx(MagNoteFolderPath & "\Project Event.txt", 1, MagNote_No_CmbBx) Then
                RCSN.Text = Nothing
            Else
                New_Detail_Btn_Click(New_Detail_Btn, EventArgs.Empty)
            End If
        End If
    End Sub
    Private Function NewSerial(ByVal TableName, ByVal FieldName, ByVal ControlName, ByVal WhereQuere) As Integer
        Using QS As New Query_Class(TableName)
            Dim sql As String = "SELECT " & FieldName & " FROM " & TableName & WhereQuere & " ORDER BY " & FieldName & " DESC;"
            QS.MyCommand.CommandText = sql
            Using reader = QS.MyCommand.ExecuteReader()
                If reader.Read() Then
                    Return reader(Split(FieldName, ",").ToList.Item(Split(FieldName, ",").ToList.Count - 1)) + 1
                Else
                    Return 1
                End If
            End Using
        End Using
    End Function
    Private Sub Update_Btn_Click(sender As Object, e As EventArgs) Handles Update_Btn.Click
        If Project_Detail_Project_Name_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر المشروع الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Prject You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If Event_Owner_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر محرر الحدث الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Event Owner You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If Project_Event_No_TxtBx.TextLength = 0 Then
            If Language_Btn.Text = "E" Then
                Msg = "إختر  الحدث الذى تريد العمل عليه اولا"
            Else
                Msg = "Select The Event  You Want To Work By First"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        If Need_Approval_ChkBx.CheckState = CheckState.Checked Then
            If Approval_From_Name_CmbBx.SelectedIndex = -1 Then
                If Language_Btn.Text = "E" Then
                    Msg = "إختر مستخدم الموافقة الذى تريد العمل عليه اولا"
                Else
                    Msg = "Select The User Approval You Want To Work By First"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                Exit Sub
            End If
        End If
        If Not IsInMagNoteCmbBx(MagNoteFolderPath & "\Project Event.txt", 1, MagNote_No_CmbBx) Then
            If Language_Btn.Text = "E" Then
                Msg = "لم تقم بتحديث الحدث بعد... من فضلك ادخل الحدث الذى ترغب فى حفظة ثم عاود المحاولة"
            Else
                Msg = "You Didn't Added The Event Yet... Kindly Add The Avent You Want To Save And Tray Again"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        Else
            If RCSN.Text.Length = 0 Then
                If Language_Btn.Text = "E" Then
                    Msg = "لم تقم بكتابة الحدث بعد... من فضلك إكتب الحدث الذى ترغب فى حفظة ثم عاود المحاولة"
                Else
                    Msg = "You Didn't Wrote  The Event Yet... Kindly Wrote The Event You Want To Save And Tray Again"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                Exit Sub
            End If
        End If

        Using QS As New Query_Class("Project_Events")
            QS.QueryAddWhere("Project_No", Project_No_TxtBx.Text)
            QS.QueryAddWhere("Project_Event_No", Project_Event_No_TxtBx.Text)
            If QS.QueryFieldValue() Then
                If QS.FillAddObjField(False, "Creation_User") <> Logged_In_User_No_TxtBx.Text Then
                    If Not UserAuthorized("Update Project Details For Another User") Then
                        Exit Sub
                    End If
                End If
            End If
        End Using

        ReDim DoDataChangesKeys(0)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = DirectCast(Project_Detail_Project_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Project_No"
        ReDim Preserve DoDataChangesKeys(DoDataChangesKeys.Length)
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Object_Value = Project_Event_No_TxtBx.Text
        DoDataChangesKeys(DoDataChangesKeys.Length - 1).Table_Fiedl_Name = "Project_Event_No"

        '
        ReDim DoDataChangesFields(0)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = DirectCast(Event_Owner_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Event_Owner"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Event_Label_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Event_Label"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Event_Starts_At_DtTmPkr.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Event_Starts_At"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Event_Status_ChkBx.CheckState
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Event_Status"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Suggested_Ended_Date_DtTmPkr.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Suggested_End_Date"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Event_Ended_At_DtTmPkr.Value
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Event_Ended_At"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Need_Approval_ChkBx.CheckState
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Need_Approval"
        If Approval_From_Name_CmbBx.SelectedIndex <> -1 Then
            ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
            DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = DirectCast(Approval_From_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Approval_From"
        End If
        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Approved_ChkBx.CheckState
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Approved"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Approved_User_No_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Approved_User"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Approved_Time_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Approved_Time"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = "[" & Replace(RCSN.Rtf, "'", "''") & "]"
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "The_Event"

        ReDim Preserve DoDataChangesFields(DoDataChangesFields.Length)
        DoDataChangesFields(DoDataChangesFields.Length - 1).Object_Value = Project_Event_Description_TxtBx.Text
        DoDataChangesFields(DoDataChangesFields.Length - 1).Table_Fiedl_Name = "Description"

        DoDataChanges("Project_Events", DoDataChangesKeys, DoDataChangesFields, 1)

        If Not IsNothing(GridPnl(0)) Then
            If GridPnl.Visible Then
                CurrentFilePath = Nothing
                If Not Save_Note_Grid() Then
                    If Language_Btn.Text = "E" Then
                        Msg = "لم يتم حفظ الجدول لوجود مشكلة"
                    Else
                        Msg = "Grid Did'nt Saved Cause Saving Have Error"
                    End If
                    ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MBOs, False)
                    Exit Sub
                End If
                If Not String.IsNullOrEmpty(CurrentFilePath) Then
                    If GridPnl.Visible And File.Exists(CurrentFilePath) Then
                        Using QS As New Query_Class("Project_Events")
                            QS.QueryAddWhere("Project_No", Project_No_TxtBx.Text)
                            QS.QueryAddWhere("Project_Event_No", Project_Event_No_TxtBx.Text)
                            If QS.QueryFieldValue() Then
                                QS.InsertUpdateColumnsValue.Add("Event_Grid", CurrentFilePath)
                                QS.DoInsertUpdate(0, 0)
                            End If
                        End Using
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Approved_User_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Approved_User_No_TxtBx.TextChanged
        Dim Stores As New Query_Class("Users",
                New Dictionary(Of String, String) From {
                                {"User_No", Approved_User_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Approved_User_Name_TxtBx, "User_Name"}})
    End Sub

    Private Sub Project_Events_Details_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Project_Events_Details_DGV.CellContentClick

    End Sub

    Private Sub Project_Opened_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Project_Opened_ChkBx.CheckStateChanged, Event_Status_ChkBx.CheckStateChanged
        Select Case sender.CheckState
            Case CheckState.Unchecked
                If Language_Btn.Text = "E" Then
                    sender.Text = "مفتوح"
                Else
                    sender.Text = "Opened"
                End If
            Case CheckState.Checked
                If Language_Btn.Text = "E" Then
                    sender.Text = "مغلق"
                Else
                    sender.Text = "Closed"
                End If
            Case CheckState.Indeterminate
                If Language_Btn.Text = "E" Then
                    sender.Text = "معلق"
                Else
                    sender.Text = "Pending"
                End If
        End Select
    End Sub

    Private Sub New_Detail_Btn_Click(sender As Object, e As EventArgs) Handles New_Detail_Btn.Click
        Try
            If Not IsInMagNoteCmbBx("Project_Event", 1, MagNote_No_CmbBx) Then
                CreatProjectEvent = True
                Add_New_MagNote_Btn.PerformClick()
            End If
            If Project_Detail_Project_Name_CmbBx.SelectedIndex = -1 Then
                If Language_Btn.Text = "E" Then
                    Msg = "إختر المشروع الذى تريد العمل عليه اولا"
                Else
                    Msg = "Select The Prject You Want To Work By First"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                Exit Sub
            End If

            Project_Event_No_TxtBx.Text = NewSerial("Project_Events", "Project_No,Project_Event_No", Project_Event_No_TxtBx, " Where Project_No = " & Project_Detail_Project_No_TxtBx.Text)
        Finally
            CreatProjectEvent = False
        End Try
    End Sub

    Private Sub Opened_Project_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Opened_Project_ChkBx.CheckStateChanged

    End Sub

    Private Sub Event_Owner_CmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Event_Owner_CmbBx.SelectedValueChanged
        If Event_Owner_CmbBx.SelectedIndex = -1 Then Exit Sub
        Event_Owner_TxtBx.Text = DirectCast(Event_Owner_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
    End Sub

    Private Sub Reset_Password_Btn_Click(sender As Object, e As EventArgs) Handles Reset_Password_Btn.Click
        If Language_Btn.Text = "E" Then
            Msg = "سيتم الغاء كلمة السر القديمة وانشاء اخرى بديله وارسالها بالبريد... هل انت موافق؟"
        Else
            Msg = "Old Password Will Be Deleted And Another One Will Be Created Instead... Are You Agree"
        End If
        If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
        Dim passwordLength As Integer = 12
        Dim password As String = GenerateRandomPassword(passwordLength)
        If password.Length > 0 Then
            Using QS As New Query_Class("Users")
                QS.QueryAddWhere("User_No", User_No_TxtBx.Text)
                If QS.QueryFieldValue(False) Then
                    QS.InsertUpdateColumnsValue.Add("Password", Encrypt_Function(password))
                    If QS.DoInsertUpdate(0, 0) Then
                        Dim NewRchTxtBx As New RichTextBox
                        NewRchTxtBx.Text = "New Password = [" & password & "]"
                        Using sen As New SentMailInBackground("Pssword Reset", NewRchTxtBx, "MagNote_Escalation@infosysme.com;" & E_Mail_TxtBx.Text, E_Mail_TxtBx.Text, Nothing, "User Name (" & User_Name_CmbBx.Text & ")", E_Mail_TxtBx.Text)
                        End Using
                    End If
                End If
            End Using
        End If
    End Sub
    Function GenerateRandomPassword(length As Integer) As String
        ' Define character sets for generating the password
        Dim uppercaseLetters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim lowercaseLetters As String = "abcdefghijklmnopqrstuvwxyz"
        Dim numbers As String = "0123456789"
        Dim specialSymbols As String = "!@#$%^&*()-_=+[{]}\|;:'"",<.>/?"
        ' Combine all character sets
        Dim allCharacters As String = uppercaseLetters & lowercaseLetters & numbers & specialSymbols
        ' Create a StringBuilder to store the generated password
        Dim passwordBuilder As New StringBuilder()
        ' Create a random number generator
        Dim random As New Random()
        ' Generate random characters for the password
        For i As Integer = 1 To length
            ' Select a random character from the combined character set
            Dim randomIndex As Integer = random.Next(0, allCharacters.Length)
            Dim randomCharacter As Char = allCharacters(randomIndex)
            ' Append the random character to the password
            passwordBuilder.Append(randomCharacter)
        Next
        ' Convert the StringBuilder to a string and return the generated password
        Return passwordBuilder.ToString()
    End Function

    Private Sub User_Authorities_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles User_Authorities_DGV.CellContentClick

    End Sub

    Private Sub Project_Events_Details_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Project_Events_Details_DGV.SelectionChanged
        '
        If ActiveControl.Name <> sender.name Or
                Project_Events_Details_DGV.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        Project_Detail_Project_No_TxtBx.Text = Project_Events_Details_DGV.CurrentRow.Cells("Project_No").Value
        Project_Event_No_TxtBx.Text = Project_Events_Details_DGV.CurrentRow.Cells("Project_Event_No").Value
    End Sub

    Private Sub User_Authority_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles User_Authority_Name_CmbBx.SelectedIndexChanged

    End Sub

    Private Sub Approval_From_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Approval_From_No_TxtBx.TextChanged
        Dim Users As New Query_Class("Users",
                New Dictionary(Of String, String) From {
                                {"User_No", Approval_From_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Approval_From_Name_CmbBx, "User_Name"}})
    End Sub

    Private Sub Authority_User_No_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Authority_User_No_TxtBx.TextChanged
        Dim Stores As New Query_Class("Users",
                New Dictionary(Of String, String) From {
                                {"User_No", Authority_User_No_TxtBx.Text}},
                New Dictionary(Of Object, String) From {
                                {Authority_User_CmbBx, "User_Name"}})
    End Sub

    Private Sub Authority_User_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Authority_User_CmbBx.SelectedIndexChanged
        If Authority_User_CmbBx.SelectedIndex <> -1 Then
            Authority_User_No_TxtBx.Text = DirectCast(Authority_User_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
        End If
    End Sub

    Private Sub Send_E_Mail_Btn_Click(sender As Object, e As EventArgs) Handles Send_E_Mail_Btn.Click
        Dim NewRchTxtBx As New RichTextBox
        If Authority_User_CmbBx.SelectedIndex = -1 Or
            User_Authority_Name_CmbBx.SelectedIndex = -1 Then
            If Language_Btn.Text = "E" Then
                Msg = "ربما لم يتم اختيار المستخدم او الصلاحية التى تريد العمل بهما"
            Else
                Msg = "The User Or Authority You Want To Work With May Not Have Been Selected"
            End If
            ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
            Exit Sub
        End If
        NewRchTxtBx.Text = DirectCast(Authority_User_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value & " Asked To Assign Or Activate This Authority To Him = [" & User_Authority_Name_CmbBx.SelectedItem & "]"
        Using sen As New SentMailInBackground("Assign Authority", NewRchTxtBx, "MagNote_Escalation@infosysme.com;" & E_Mail_TxtBx.Text, E_Mail_TxtBx.Text, Nothing, "User Name (" & User_Name_CmbBx.Text & ")", E_Mail_TxtBx.Text)
        End Using
    End Sub

    Private Sub Ask_To_Assign_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Ask_To_Assign_ChkBx.CheckedChanged

    End Sub

    Private Sub Approval_From_Name_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Approval_From_Name_CmbBx.SelectedIndexChanged
        If Approval_From_Name_CmbBx.SelectedIndex = -1 Then Exit Sub
        Approval_From_No_TxtBx.Text = DirectCast(Approval_From_Name_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
    End Sub

    Private Sub Show_Password_Btn_Click(sender As Object, e As EventArgs) Handles Show_Password_Btn.Click

    End Sub

    Private Sub User_Authorities_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles User_Authorities_DGV.SelectionChanged
        If ActiveControl.Name <> sender.name Or
            User_Authorities_DGV.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        Authority_User_CmbBx.Text = User_Authorities_DGV.CurrentRow.Cells("User_No").Value
        User_Authority_Name_CmbBx.Text = User_Authorities_DGV.CurrentRow.Cells("Authority_Name").Value
    End Sub

    Private Sub User_Authority_Name_CmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles User_Authority_Name_CmbBx.SelectedValueChanged
        If Authority_User_CmbBx.SelectedIndex = -1 Or
            User_Authority_Name_CmbBx.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim User_Authorities As New Query_Class("User_Authorities",
                New Dictionary(Of String, String) From {
                                {"User_No", DirectCast(Authority_User_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key},
                                {"Authority_Name", User_Authority_Name_CmbBx.SelectedItem}},
                New Dictionary(Of Object, String) From {
                                {Active_ChkBx, "Active"},
                                {User_Authority_Description_TxtBx, "Description"}})
        Ask_To_Assign_ChkBx.CheckState = CheckState.Unchecked
        If Not User_Authorities.OldRecord Then
            Ask_To_Assign_ChkBx.Enabled = True
        ElseIf Active_ChkBx.CheckState = CheckState.Unchecked Then
            Ask_To_Assign_ChkBx.Enabled = True
        Else
            Ask_To_Assign_ChkBx.Enabled = False
        End If
    End Sub

    Private Sub E_Mail_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles E_Mail_TxtBx.TextChanged

    End Sub

    Private Sub Ask_To_Assign_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Ask_To_Assign_ChkBx.CheckStateChanged
        If Ask_To_Assign_ChkBx.CheckState = CheckState.Checked Then
            Send_E_Mail_Btn.Enabled = True
        Else
            Send_E_Mail_Btn.Enabled = False
        End If
    End Sub

    Private Sub New_User_Btn_Click(sender As Object, e As EventArgs) Handles New_User_Btn.Click
        User_Name_CmbBx.SelectedIndex = -1
        Password_TxtBx.Text = Nothing
        Cellular_No_TxtBx.Text = Nothing
        User_No_TxtBx.Text = Nothing
        E_Mail_TxtBx.Text = Nothing
        E_Mail_TxtBx.ReadOnly = False
        If Not UserAuthorized("Create New User") Then Exit Sub
        User_No_TxtBx.Text = NewSerial("Users", "User_No", User_No_TxtBx, Nothing)
    End Sub

    Private Sub New_Customer_Btn_Click(sender As Object, e As EventArgs) Handles New_Customer_Btn.Click
        Customer_No_TxtBx.Text = NewSerial("Customers", "Customer_No", Customer_No_TxtBx, Nothing)
    End Sub

    Private Sub New_Prject_Btn_Click(sender As Object, e As EventArgs) Handles New_Prject_Btn.Click
        Project_No_TxtBx.Text = NewSerial("Projects", "Project_No", Project_No_TxtBx, Nothing)
    End Sub

    Private Sub Alternating_Row_Color_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Alternating_Row_Color_ClrCmbBx.SelectedIndexChanged
        If Alternating_Row_Color_ClrCmbBx.SelectedIndex = -1 Then Exit Sub
        For Each Cntrl As Control In FindControlRecursive(New List(Of Control), Me, New List(Of Type)({GetType(DataGridView)}))
            CType(Cntrl, DataGridView).AlternatingRowsDefaultCellStyle.BackColor = Alternating_Row_Color_ClrCmbBx.SelectedItem
        Next
        Application.DoEvents()
    End Sub

    Private Sub Without_Opened_Notes_At_Startup_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Without_Opened_Notes_At_Startup_ChkBx.CheckedChanged

    End Sub

    Private Sub Show_Password_Btn_MouseDown(sender As Object, e As MouseEventArgs) Handles Show_Password_Btn.MouseDown
        Password_TxtBx.UseSystemPasswordChar = False
        Password_TxtBx.PasswordChar = Nothing
    End Sub

    Private Sub Enable_Update_Btn_Click(sender As Object, e As EventArgs) Handles Enable_Update_Btn.Click
        If Alert_Setting_Pnl.Enabled Then
            Alert_Setting_Pnl.Enabled = False
        Else
            Alert_Setting_Pnl.Enabled = True
        End If
        ClearAlertInfo()
    End Sub

    Private Sub Projects_Connection_Strings_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Projects_Connection_Strings_CmbBx.SelectedIndexChanged
        If Projects_Connection_Strings_CmbBx.SelectedIndex = -1 Then Exit Sub
        User_Name_CmbBx.Items.Clear()
        Try
            Using QS As New Query_Class("Users")
                If Not OpenDBStatus Then
                    Exit Sub
                End If
                If QS.QueryFieldValue() Then
                    For Each User In QS.Query_DT.Rows
                        User_Name_CmbBx.Items.Add(New KeyValuePair(Of String, String)(User.item("User_No"), User.item("User_Name")))
                    Next
                End If
                If QS.OldRecord Then
                    Projects_TbCntrl.TabPages("Users_TbPg").Enabled = True
                Else
                    Projects_TbCntrl.TabPages("Users_TbPg").Enabled = False
                End If
            End Using
            If Language_Btn.Text = "E" Then
                Msg = "تم الاتصال بقاعدة البيانات بنجاح"
            Else
                Msg = "Connecting To The Database Done Successfully"
            End If
            ShowMsg(Msg & vbNewLine & Projects_Connection_Strings_CmbBx.SelectedItem, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub



    Private Sub Show_Password_Btn_MouseUp(sender As Object, e As MouseEventArgs) Handles Show_Password_Btn.MouseUp
        Password_TxtBx.UseSystemPasswordChar = True
        Password_TxtBx.PasswordChar = "*"
    End Sub

    Private Sub E_Mail_TxtBx_Validating(sender As Object, e As CancelEventArgs) Handles E_Mail_TxtBx.Validating
        If Not IsValidEmailFormat(E_Mail_TxtBx) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Without_Opened_Notes_At_Startup_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Without_Opened_Notes_At_Startup_ChkBx.CheckStateChanged
        If Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Unchecked Then
            If Language_Btn.Text = "E" Then
                Without_Opened_Notes_At_Startup_ChkBx.Text = "عرض الملاحظات المفتوحة سابقاً مع الفئة الحالية"
            Else
                Without_Opened_Notes_At_Startup_ChkBx.Text = "Preview Previously Opened Note And Current Category"
            End If
        ElseIf Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Checked Then
            If Language_Btn.Text = "E" Then
                Without_Opened_Notes_At_Startup_ChkBx.Text = "عرض الفئة الحالية فقط"
            Else
                Without_Opened_Notes_At_Startup_ChkBx.Text = "Preview Current Category Only"
            End If
        ElseIf Without_Opened_Notes_At_Startup_ChkBx.CheckState = CheckState.Indeterminate Then
            If Language_Btn.Text = "E" Then
                Without_Opened_Notes_At_Startup_ChkBx.Text = "عرض الملاحظات المفتوحة سابقاً فقط"
            Else
                Without_Opened_Notes_At_Startup_ChkBx.Text = "Preview Previously Opened Note Only"
            End If
        End If
    End Sub

    Private Sub Alert_Files_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Alert_Files_DGV.CellContentClick

    End Sub

    Dim RemoveTabs As New List(Of TabPage)
    Private Sub Delete_Selected_Files_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Selected_Alert_Files_Btn.Click

        For i As Integer = Alert_Files_DGV.SelectedRows.Count - 1 To 0 Step -1
            Dim row As DataGridViewRow = Alert_Files_DGV.SelectedRows(i)
            Alert_Files_DGV.Rows.Remove(row)
        Next
    End Sub

    Private Sub Add_Alert_Files_Click(sender As Object, e As EventArgs) Handles Add_Alert_Files_Btn.Click
        Try
            AddAlertFilesDGVColumn()
            Dim OpenFileDialog As New OpenFileDialog
            OpenFileDialog.Title = "Audio Files"
            OpenFileDialog.Filter = "MP3 Audio|*.MP3|WAVE Files|*.WAVE|All files|*.*"
            If Infinity_Alert_ChkBx.CheckState = CheckState.Checked Then
                OpenFileDialog.Multiselect = True
            End If
            OpenFileDialog.FileName = ""
            OpenFileDialog.RestoreDirectory = True
            If File.Exists(Alert_Voice_Path_TxtBx.Text) Then
                OpenFileDialog.InitialDirectory = Path.GetDirectoryName(Alert_Voice_Path_TxtBx.Text)
                OpenFileDialog.FileName = Path.GetFileName(Alert_Voice_Path_TxtBx.Text)
            End If

            If OpenFileDialog.ShowDialog = DialogResult.Cancel Then
                Exit Sub
            Else
                If Infinity_Alert_ChkBx.CheckState = CheckState.Checked And
                    Alert_Files_DGV.Rows.Count > 0 Then
                    If Language_Btn.Text = "E" Then
                        Msg = "هل تريد اضافة الملفات المختارة الى الملفات المضافة سابقا؟"
                    Else
                        Msg = "Do You Want To Add Selected Files To Previously Added Files?"
                    End If
                    If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                        Alert_Files_DGV.Rows.Clear()
                    End If
                Else
                    Alert_Files_DGV.Rows.Clear()
                End If
                For Each File In OpenFileDialog.FileNames
                    If Not isInDataGridView(File, "File_Path", Alert_Files_DGV) Then
                        Alert_Files_DGV.Rows.Add(File, Path.GetFileName(File))
                    End If
                Next
            End If

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Public Function RemoveCurrentTab(ByVal tp As TabPage) As Boolean
        Dim result As Boolean
        Try
            RemoveTabs.Add(tp)
            Setting_TbCntrl.TabPages.Remove(tp)
            result = True
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
        Return result
    End Function

    Private Sub Main_Password_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Main_Password_TxtBx.TextChanged

    End Sub

    Public Function AddTabBack(ByVal title As String) As Boolean
        Dim result As Boolean
        Try
            If Not String.IsNullOrEmpty(title) Then
                For Each tp As TabPage In RemoveTabs
                    If String.Compare(tp.Name, title) = 0 Then
                        RemoveTabs.Remove(tp)
                        Setting_TbCntrl.TabPages.Add(tp)
                        result = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
        Return result
    End Function
    Private Sub AddAlertFilesDGVColumn()
        If Alert_Files_DGV.Columns.Count = 0 Then
            Alert_Files_DGV.Columns.Add("File_Path", "File Path")
            Alert_Files_DGV.Columns.Add("File_Name", "File Name")
        End If
    End Sub
    Private Sub Alert_Files_DGV_KeyDown(sender As Object, e As KeyEventArgs) Handles Alert_Files_DGV.KeyDown
        If e.KeyCode = Keys.Delete Then
            Delete_Selected_Files_Btn_Click(Delete_Selected_Alert_Files_Btn, EventArgs.Empty)
        End If
    End Sub

    Private Sub Main_Password_TxtBx_Validating(sender As Object, e As CancelEventArgs) Handles Main_Password_TxtBx.Validating
        If Complex_Password_ChkBx.CheckState = CheckState.Checked And
            Main_Password_TxtBx.TextLength > 0 Then
            If Not IsPasswordComplex(Main_Password_TxtBx.Text) Then
                If Language_Btn.Text = "E" Then
                    Msg =
"يجب أن تفي كلمة المرور الخاصة بك بالشروط التالية:
1- الحد الأدنى للطول (على سبيل المثال، 8 أحرف على الأقل).
2- تحتوي على أحرف كبيرة وصغيرة
3- تحتوي على أرقام.
4- تحتوي على أحرف خاصة."
                Else
                    Msg =
 "Your Password Must Meet These Conditions:-
    1- Minimum length (e.g., at least 8 characters).
    2- Contains both uppercase and lowercase letters.
    3- Contains digits.
    4- Contains special characters."
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                e.Cancel = True
            End If
        End If
    End Sub
    Public Function IsPasswordComplex(password As String) As Boolean
        ' Define the minimum length for a complex password
        Dim minLength As Integer = 8

        ' Define the regex patterns for different character sets
        Dim hasUpperCase As String = "[A-Z]"
        Dim hasLowerCase As String = "[a-z]"
        Dim hasDigit As String = "\d"
        Dim hasSpecialChar As String = "[^a-zA-Z\d]"

        ' Check if the password meets all the complexity requirements
        If password.Length >= minLength AndAlso
           Regex.IsMatch(password, hasUpperCase) AndAlso
           Regex.IsMatch(password, hasLowerCase) AndAlso
           Regex.IsMatch(password, hasDigit) AndAlso
           Regex.IsMatch(password, hasSpecialChar) Then
            Return True
        End If

        ' If any of the requirements are not met, return False
        Return False
    End Function

#Region "WorkInBackground"
    Public WithEvents backgroundWorker As New System.ComponentModel.BackgroundWorker
    Public Sub backgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles backgroundWorker.DoWork
        ' Perform background operation here
        Dim FolderName = Backup_Folder_Path_TxtBx.Text
        FolderName = Replace(FolderName, "\\", "\")
        Dim FileName = "\" & Replace(Replace(Replace(Replace(Next_Backup_Time_TxtBx.Text, ":", "-"), ".", "-"), "/", "-"), " ", "_") '
        If (Not System.IO.Directory.Exists(FolderName)) Then
            System.IO.Directory.CreateDirectory(FolderName)
        End If
        If File.Exists(FolderName & FileName) Then
            'Return True
            Dim BT As DateTime = Now
            BT = BT.AddDays(Days_NmrcUpDn.Value)
            BT = BT.AddHours(Hours_NmrcUpDn.Value)
            BT = BT.AddMinutes(Minutes_NmrcUpDn.Value)
            Next_Backup_Time_TxtBx.Text = BT
            Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
            Exit Sub
        End If
        Dim CompresseFile = FolderName & FileName & ".7z"
        Using AC As New RefreshWaitAWhileTitles_Class
            AC.RunCommandCom(Application.StartupPath & "\7za.exe", 1, MagNoteFolderPath & "\", CompresseFile, 1)
        End Using
        While ProcessRunning("7za")
            Application.DoEvents()
        End While
        If File.Exists(CompresseFile) Then
            Dim BT As DateTime = Now
            BT = BT.AddDays(Days_NmrcUpDn.Value)
            BT = BT.AddHours(Hours_NmrcUpDn.Value)
            BT = BT.AddMinutes(Minutes_NmrcUpDn.Value)
            Next_Backup_Time_TxtBx.Text = BT
        Else
            Backup_Timer.Interval = 150000
        End If
    End Sub

    Private Sub Alert_Setting_Pnl_Paint(sender As Object, e As PaintEventArgs) Handles Alert_Setting_Pnl.Paint

    End Sub

    Public Sub backgroundWorker_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles backgroundWorker.ProgressChanged
    End Sub

    Private Sub Activate_Alert_Function_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Activate_Alert_Function_ChkBx.CheckedChanged

    End Sub

    Public Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
        If e.Cancelled Then
            MessageBox.Show("Operation was cancelled.")
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show("An error occurred: " & e.Error.Message)
        Else
            Dim FolderName = Backup_Folder_Path_TxtBx.Text
            FolderName = Replace(FolderName, "\\", "\")
            ShowMsg("Backup Done Successfully" & vbNewLine & FolderName, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            If Immediately_Update_Form_Parameters_ChkBx.CheckState = CheckState.Checked Then
                Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
            End If
        End If
    End Sub

    Private Sub Immediately_Update_Form_Parameters_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Immediately_Update_Form_Parameters_ChkBx.CheckedChanged

    End Sub

    Private Sub Note_Password_TxtBx_Validating(sender As Object, e As CancelEventArgs) Handles Note_Password_TxtBx.Validating
        If Note_Password_TxtBx.TextLength = 0 Then Exit Sub
        Dim x = Decrypt_Function(Available_MagNotes_DGV.CurrentRow.Cells("Note_Password").Value)
        If Decrypt_Function(Available_MagNotes_DGV.CurrentRow.Cells("Note_Password").Value) <> Note_Password_TxtBx.Text Then
            If Language_Btn.Text = "E" Then
                Msg = "كلمة السر خطأ"
            Else
                Msg = "Wrong Password"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Note_Password_TxtBx.Focus()
            e.Cancel = True
        End If
    End Sub

    Private Sub Alert_Setting_Pnl_EnabledChanged(sender As Object, e As EventArgs) Handles Alert_Setting_Pnl.EnabledChanged
        Add_Alert_Files_Btn.Enabled = sender.Enabled
        Delete_Selected_Alert_Files_Btn.Enabled = sender.Enabled
        If Alert_Setting_Pnl.Enabled Then
            Alert_Tmr.Stop()
            Pause_Alert_Timer_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Play
        Else
            Alert_Tmr.Start()
            Pause_Alert_Timer_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Puse
        End If
    End Sub

    Private Sub Activate_Alert_Function_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Activate_Alert_Function_ChkBx.CheckStateChanged
        If Activate_Alert_Function_ChkBx.CheckState = CheckState.Checked Then
            Alert_Tmr.Start()
        ElseIf Activate_Alert_Function_ChkBx.CheckState = CheckState.unChecked Then
            Alert_Tmr.Stop()
        End If
    End Sub

    Private Sub ChkBx_CheckStateChanged(sender As Object, e As EventArgs)
        If Immediately_Update_Form_Parameters_ChkBx.CheckState = CheckState.Checked And
            ActiveControl.Name = sender.name Then
            Save_Note_Form_Parameter_Setting_Btn_Click(Save_Note_Form_Parameter_Setting_Btn, EventArgs.Empty)
        End If
    End Sub

    Private Sub Available_Alerts_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Available_Alerts_DGV.CellContentClick

    End Sub

    Private Sub Stop_Displaying_Controls_ToolTip_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Stop_Displaying_Controls_ToolTip_ChkBx.CheckStateChanged
        If Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = CheckState.Unchecked Then
            CType(Form_ToolTip, ToolTip).Active = True
            If Not IsNothing(Shortcuts_LstVw) Then
                Shortcuts_LstVw.ShowItemToolTips = True
            End If
        ElseIf Stop_Displaying_Controls_ToolTip_ChkBx.CheckState = CheckState.Checked Then
            CType(Form_ToolTip, ToolTip).Active = False
            If Not IsNothing(Shortcuts_LstVw) Then
                Shortcuts_LstVw.ShowItemToolTips = False
            End If
        End If
    End Sub

    Private Sub Note_Form_Location_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Note_Form_Location_TxtBx.TextChanged

    End Sub

    Private Sub Azan_Activation_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Azan_Activation_ChkBx.CheckStateChanged
        If Azan_Activation_ChkBx.CheckState = CheckState.Checked Then
            Azan_Tmr.Start()
        Else
            Azan_Tmr.Stop()
        End If
    End Sub

    Dim DateTimeToSet As DateTime
    Private Sub Alert_Repeet_Minute_NmrcUpDn_ValueChanged(sender As Object, e As EventArgs) Handles Alert_Repeet_Minute_NmrcUpDn.ValueChanged, Alert_Repeet_Hour_NmrcUpDn.ValueChanged, Alert_Repeet_Day_NmrcUpDn.ValueChanged
        If ActiveControl.Name = sender.name Then
            DateTimeToSet = Start_Alert_Time_DtTmPikr.Value
            DateTimeToSet = DateTimeToSet.AddMinutes(Alert_Repeet_Minute_NmrcUpDn.Value)
            DateTimeToSet = DateTimeToSet.AddHours(Alert_Repeet_Hour_NmrcUpDn.Value)
            DateTimeToSet = DateTimeToSet.AddDays(Alert_Repeet_Day_NmrcUpDn.Value)
            End_Alert_Time_DtTmPikr.Value = DateTimeToSet
        End If
    End Sub

    Private Sub End_Alert_Time_DtTmPikr_ValueChanged(sender As Object, e As EventArgs) Handles End_Alert_Time_DtTmPikr.ValueChanged, Start_Alert_Time_DtTmPikr.ValueChanged
        Try
            If ActiveControl.Name = sender.name Or
                ActiveControl.Name = Start_Alert_Time_DtTmPikr.Name Then
                Dim Tim As TimeSpan
                Tim = End_Alert_Time_DtTmPikr.Value - Start_Alert_Time_DtTmPikr.Value
                Alert_Repeet_Minute_NmrcUpDn.Value = Tim.Minutes
                Alert_Repeet_Hour_NmrcUpDn.Value = Tim.Hours
                Alert_Repeet_Day_NmrcUpDn.Value = Tim.Days
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Save_Setting_When_Exit_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Save_Setting_When_Exit_ChkBx.CheckStateChanged
        Save_Setting_When_Exit = Save_Setting_When_Exit_ChkBx.CheckState
    End Sub
    Dim IgnoreSortAvailableAlertsDGV As Boolean
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Encrypt_Application_Configuration_Setting_Btn.Click
        Try
            If File.Exists(Application.StartupPath & "\" & Application.ProductName & ".exe.config") Then
                ' Open the current configuration file
                Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                ' Get the appSettings section
                Dim section As ConfigurationSection = config.GetSection("appSettings")

                If ConfigurationManager.AppSettings("EncryptionKey") = "EncryptionKey" Then
                    Msg = "ربما لم يتم اعداد ملف اعدادات النظام بعد... لن يتم تشفير الملف إلا بعد تغيير مفتاح التشفير على الاقل فى الملف"
                    Msg &= vbNewLine & "Maybe Application Configuration File Not Prepared Yet... Encrypting The File Will Not Be Done Until Changing The Password In The File At least"
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                    Exit Sub
                End If
                If Not section.SectionInformation.IsProtected Then
                    ' Protect the section using the DataProtectionConfigurationProvider
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
                    section.SectionInformation.ForceSave = True
                    ' Save the changes
                    config.Save(ConfigurationSaveMode.Modified)
                End If

            Else 'Create Another one by Default Values
                Msg = "لم تفلح محاولة العثور على ملف اعدادات النظام... تم انشاء اخر جديد يحتاج الى اعداد منك"
                Msg &= vbNewLine & "Couldn't Find Configuration File. New File Created And Needs To Update From You"
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                CreatAppConfig()
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Available_Alerts_DGV_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Available_Alerts_DGV.CellValueChanged
        Dim NextAlert As New DataGridViewColumn
        NextAlert = Available_Alerts_DGV.Columns("Next_Alert_Time")
        If e.ColumnIndex = NextAlert.Index And Not IgnoreSortAvailableAlertsDGV Then
            Try
                If Available_Alerts_DGV.Rows.Count > 0 Then
                    Available_Alerts_DGV.Sort(Available_Alerts_DGV.Columns("Next_Alert_Time"), System.ComponentModel.ListSortDirection.Ascending)
                    Available_Alerts_DGV.Refresh()
                End If
            Catch ex As Exception
            Finally
                IgnoreSortAvailableAlertsDGV = False
            End Try
        End If
    End Sub

    Private Sub Alert_Files_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Alert_Files_DGV.SelectionChanged
        If ActiveControl.Name <> sender.name Or
            IsNothing(Alert_Files_DGV.CurrentRow) Then Exit Sub
        Alert_Voice_Path_TxtBx.Text = Alert_Files_DGV.CurrentRow.Cells("File_Path").Value
    End Sub

    Private Sub Alert_Repeet_Minute_NmrcUpDn_Validating(sender As Object, e As CancelEventArgs) Handles Alert_Repeet_Minute_NmrcUpDn.Validating, Alert_Repeet_Hour_NmrcUpDn.Validating, Alert_Repeet_Day_NmrcUpDn.Validating, Minutes_Between_Repetitions_NmrcUpDn.Validating
        If String.IsNullOrEmpty(sender.text) Then
            If Language_Btn.Text = "E" Then
                Msg = "من فضلك ادخل قيمة صحيحة"
            Else
                Msg = "Kindly Enter Valid Value"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            e.Cancel = True
        End If
    End Sub

    Private Sub Alert_Sound_Volume_TrkBr_Scroll(sender As Object, e As EventArgs) Handles Alert_Sound_Volume_TrkBr.Scroll
        ToolTip1.SetToolTip(sender, Alert_Sound_Volume_TrkBr.Value)
    End Sub

    Private Sub Delete_Authority_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Authority_Btn.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LinkMap = New LinkMapDetils(0) {}
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    End Sub

    Private Sub MagNote_PctrBx_Click(sender As Object, e As EventArgs) Handles MagNote_PctrBx.Click

    End Sub

    Dim IgnoreWinSize As Boolean = False
    Private Sub MagNote_Form_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Not IgnoreWinSize Then
            Note_Form_Size_TxtBx.Text = Me.Size.ToString
        End If
        IgnoreWinSize = False
    End Sub

    Private Sub MagNote_Form_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Not IgnoreWinSize And
            Me.WindowState = FormWindowState.Normal Then
            Note_Form_Location_TxtBx.Text = Me.Location.ToString
        End If
    End Sub

    Private Sub Infinity_Alert_ChkBx_EnabledChanged(sender As Object, e As EventArgs) Handles Infinity_Alert_ChkBx.EnabledChanged
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name = sender.name Then
            Add_Alert_Flder_Files_Btn.Enabled = sender.Enabled
        End If
    End Sub

#End Region

End Class
'----------------------------------------------------New Class--------------------------
Public Class myReverserClass
    Implements IComparer

    ' Calls CaseInsensitiveComparer.Compare with the parameters reversed.
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
         Implements IComparer.Compare
        Return New CaseInsensitiveComparer().Compare(y.name, x.name)
    End Function 'IComparer.Compare

End Class
