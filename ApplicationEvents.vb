Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports System.Management
Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Try
                FindApplicationWorkingFolder(Nothing)
                If Debugger.IsAttached Then
                    Dim UserApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    File.Copy(Application.Info.DirectoryPath & "\" & Application.Info.ProductName & ".exe",
                      UserApplicationDataFolder & "\InfoSysMe\MagNote\" & Application.Info.ProductName & ".exe", 1)
                    File.Copy(Application.Info.DirectoryPath & "\" & Application.Info.ProductName & ".exe.config",
                      UserApplicationDataFolder & "\InfoSysMe\MagNote\" & Application.Info.ProductName & ".exe.config", 1)
                End If
                FirstTimeRun = True
                Dim MyArgmnts As String
                If e.CommandLine.Count > 0 Then
                    MyArgmnts = e.CommandLine.Item(0).ToString()
                    If IsNothing(OpenedExternalFiles(0)) Then
                        OpenedExternalFiles(0) = MyArgmnts
                    Else
                        Dim NoteOpenedExist As Boolean
                        For Each OpenedNote In OpenedExternalFiles
                            If OpenedNote <> MyArgmnts Then
                                Continue For
                            Else
                                NoteOpenedExist = True
                                Exit For
                            End If
                        Next
                        If Not NoteOpenedExist Then
                            ReDim Preserve OpenedExternalFiles(OpenedExternalFiles.Length)
                            OpenedExternalFiles(OpenedExternalFiles.Length - 1) = MyArgmnts
                        End If
                    End If
                Else
                    If Debugger.IsAttached Then
                        Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote")
                        MagNoteFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\InfoSysMe\MagNote\MagNotes_Files"
                    Else
                        MagNoteFolderPath = Application.Info.DirectoryPath & "\MagNotes_Files"
                    End If
                    Dim parentDirectory As DirectoryInfo = Directory.GetParent(MagNoteFolderPath)
                    ApplicationStartupPath = parentDirectory.FullName
                    Dim RequiredFolders = ".\MagNotes_Files,.\MagNotes_Files\GridFiles,.\MagNotes_Files\Links,.\MagNotes_Files\OutsideMagNote,.\MagNotes_Files\Temp,.\MagNotes_Files\Temp\Alerts Backup,.\MagNotes_Files\Temp\Shortcuts_links Backup,.\MagNotes_Files\Shortcuts,.\MagNotes_Files\Shortcuts\Images,.\Note_Backup_Folder"
                    For Each Folder In Split(RequiredFolders, ",")
                        Dim FolderPath = Folder
                        If (Not System.IO.Directory.Exists(FolderPath)) Then
                            System.IO.Directory.CreateDirectory(FolderPath)
                        End If
                    Next
                End If
                If String.IsNullOrEmpty(ApplicationStartupPath) Then
                    ApplicationStartupPath = Application.Info.DirectoryPath
                End If
                If String.IsNullOrEmpty(MagNoteFolderPath) Then
                    MagNoteFolderPath = Application.Info.DirectoryPath & "\MagNotes_Files"
                End If
                Dim ee As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs
                RunningExternal(sender, ee)
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (" & Application.Info.ProductName & ")", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End Try
        End Sub
        Private Function FindApplicationWorkingFolder(MyArgmnts As String) As Boolean
            If Not String.IsNullOrEmpty(MyArgmnts) Then
                Exit Function
            End If
            Dim DesktopFP = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            If Not File.Exists(Application.Info.DirectoryPath & "\" & Application.Info.ProductName & ".exe") Or Application.Info.DirectoryPath = DesktopFP Then
FWF:
                ShowMsg("يرجى تحديد مجلد عمل التطبيق أولاً والذي يحتوي على ملف تنفيذ التطبيق (" & Application.Info.ProductName & ".exe)" & vbNewLine & "Kindly Select Firstly Application Working folder Which Have The Application Execute file (" & Application.Info.ProductName.ToString & ".exe)")
                Using folderDialog As New FolderBrowserDialog
                    folderDialog.Description = "تحديد مجلد عمل التطبيق Application Working folder"
                    folderDialog.SelectedPath = Application.Info.DirectoryPath
                    If folderDialog.ShowDialog() = DialogResult.OK Then
                        If Not File.Exists(folderDialog.SelectedPath & "\" & Application.Info.ProductName & ".exe") Then
                            GoTo FWF
                        End If

                        Directory.SetCurrentDirectory(folderDialog.SelectedPath)
                    Else
                        End
                    End If
                End Using
            Else
                Directory.SetCurrentDirectory(Application.Info.DirectoryPath)
            End If
        End Function
        Private Sub RunningExternal(ByVal sender As Object,
                                                        ByVal e As Microsoft.VisualBasic.ApplicationServices.
                                                                      StartupNextInstanceEventArgs)
            Dim MyArgmnts As String = ""
            Try
                If IsNothing(e) Then GoTo AlreadyRunning
                If TypeOf Me.MainForm Is MagNote_Form Then
                    If e.CommandLine.Count > 0 Then
                        MyArgmnts = e.CommandLine.Item(0).ToString()
                    Else
AlreadyRunning:
                        If Environment.GetCommandLineArgs.Count > 0 Then
                            Dim arguments As String() = Environment.GetCommandLineArgs()
                            For Each argmnt In arguments
                                If argmnt = Application.Info.DirectoryPath & "\" & Application.Info.ProductName & ".exe" Then Continue For
                                MyArgmnts &= argmnt & ","
                            Next
                            If Not String.IsNullOrEmpty(MyArgmnts) Then
                                MyArgmnts = Microsoft.VisualBasic.Left(MyArgmnts, MyArgmnts.Length - 1)
                            End If
                        End If
                    End If

                    If Not String.IsNullOrEmpty(MyArgmnts) Then
                        Application.DoEvents()
                        Dim OSINTChkBx As New CheckBox
                        OSINTChkBx.CheckState = MagNote_Form.Open_Note_In_New_Tab_ChkBx.CheckState
                        UseArgFile = MyArgmnts
                        If Not String.IsNullOrEmpty(UseArgFile) And
                            MagNote_Form.MagNotes_Notes_TbCntrl.TabPages.Count = 0 Then
                            MagNoteOpenedByFileOpenedOutSideMageNote = True
                        ElseIf Not String.IsNullOrEmpty(UseArgFile) Then
                            MyApplicationStartupNextInstance = True
                        End If
                        MagNote_Form.Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
                        If OpenWithWhatsApp Then
                            UseArgFile = CleanFileName(UseArgFile, 1)
                        End If
                        ExternalFilePath = Path.GetDirectoryName(UseArgFile)
                        ExternalFileName = Path.GetFileName(UseArgFile)
                        SetMagNoteNoCmbBxFocused()
                        If MagNote_Form.MagNoteFileFormat(UseArgFile, 1) And
                            MagNote_Form.Visible = False Then
                            If ShowMsg("هل تريد فتح هذا الملف بشكل منفصل؟" & vbNewLine & "Do You Want To Open This File Separately?",, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                EncrypAppConfig()
                            Else
                                End
                            End If
                        End If
                        DirectCast(MagNote_Form, MagNote_Form).Open_Note_TlStrpBtn_Click(MagNote_Form.Note_TlStrp.Items("OpenToolStripButton"), EventArgs.Empty, 1)
                        MagNote_Form.Open_Note_In_New_Tab_ChkBx.CheckState = OSINTChkBx.CheckState
                        AddMagNoteRTF(1)
                        MakeTopMost(1)
                    End If
                End If
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Finally
                Application.DoEvents()
                MagNote_Form.MyFormWindowState()
                UseArgFile = Nothing
                ExternalFilePath = Nothing
                ExternalFileName = Nothing
                FirstTimeRun = False
            End Try
        End Sub
        Dim OpenWithWhatsApp As Boolean
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object,
                                                                      ByVal e As Microsoft.VisualBasic.ApplicationServices.
                                                                      StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Try
                OpenWithWhatsApp = False
                If e.CommandLine.Count > 0 Then
                    Dim parentName As String = GetParentProcessName()
                    Dim args As String() = e.CommandLine.ToArray()
                    If args.Length > 0 Then
                        Dim openedFile As String = args(0)
                        If IsFileNameEndingSpecial(openedFile) And
                        parentName = "msvsmon" Then
                            OpenWithWhatsApp = True
                        End If
                    End If
                    Dim MyArgmnts = e.CommandLine.Item(0).ToString()
                    If IsNothing(OpenedExternalFiles(0)) Then
                        OpenedExternalFiles(0) = MyArgmnts
                    Else
                        Dim NoteOpenedExist As Boolean
                        For Each OpenedNote In OpenedExternalFiles
                            If OpenedNote <> MyArgmnts Then
                                Continue For
                            Else
                                NoteOpenedExist = True
                                Exit For
                            End If
                        Next
                        If Not NoteOpenedExist Then
                            ReDim Preserve OpenedExternalFiles(OpenedExternalFiles.Length)
                            OpenedExternalFiles(OpenedExternalFiles.Length - 1) = MyArgmnts
                        End If
                    End If
                End If
                RunningExternal(sender, e)
                Application.DoEvents()
                If RCSN(0) IsNot Nothing Then
                    RCSN(0).Focus()
                End If
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End Try
        End Sub
        Public Function CleanFileName(originalName As String, Optional ByVal WithRename As Boolean = False) As String
            Try
                If Not File.Exists(originalName) Then Exit Function
                ' This regex removes any number inside square brackets, including the brackets themselves
                Dim pattern As String = "\[\d+\]"
                Dim result As String = Regex.Replace(originalName, pattern, "").Trim()
                ' Optionally clean up any extra spaces or dashes
                result = Regex.Replace(result, "\s{2,}", " ").Replace("--", "-").Replace("- .", "-").Trim()
                result = result.Replace("_", " ")
                If WithRename Then
                    If File.Exists(result) Then
                        My.Computer.FileSystem.DeleteFile(result, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    End If
                    File.Move(originalName, result)
                End If
                Return result
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End Try
        End Function
        Public Function IsFileNameEndingSpecial(fileName As String) As Boolean
            If Microsoft.VisualBasic.Left(Path.GetFileName(fileName), 7) = "MagNote" Then
                Dim pattern As String = "\)-\[\d+\]\.txt$"
                Return Regex.IsMatch(fileName, pattern)
            End If
        End Function
        Public Function GetParentProcessName() As String
            Dim myId As Integer = Process.GetCurrentProcess().Id
            Dim query As String = $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {myId}"
            Using searcher As New ManagementObjectSearcher(query)
                For Each obj As ManagementObject In searcher.Get()
                    Dim parentId As Integer = Convert.ToInt32(obj("ParentProcessId"))
                    Dim parent As Process = Process.GetProcessById(parentId)
                    Return parent.ProcessName
                Next
            End Using
            Return String.Empty
        End Function
    End Class
End Namespace
