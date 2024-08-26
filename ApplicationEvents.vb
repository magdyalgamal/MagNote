Imports System.IO

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
                Dim RequiredFolders = "MagNotes_Files,MagNotes_Files\GridFiles,MagNotes_Files\Links,MagNotes_Files\OutsideMagNote,MagNotes_Files\Temp,MagNotes_Files\Shortcuts,MagNotes_Files\Shortcuts\Images"

                For Each Folder In Split(RequiredFolders, ",")
                    Dim FolderPath = Application.Info.DirectoryPath & "\" & Folder
                    If (Not System.IO.Directory.Exists(FolderPath)) Then
                        System.IO.Directory.CreateDirectory(FolderPath)
                    End If
                Next
                FirstTimeRun = True
                'If e.CommandLine.Count > 0 Then
                '    Dim MyArgmnts = e.CommandLine.Item(0).ToString()
                '    If IsNothing(OpenedExternalFiles(0)) Then
                '        OpenedExternalFiles(0) = MyArgmnts
                '    ElseIf Array.Find(OpenedExternalFiles, Function(f) f = MyArgmnts) = -1 Then
                '        ReDim Preserve OpenedExternalFiles(OpenedExternalFiles.Length)
                '        OpenedExternalFiles(OpenedExternalFiles.Length - 1) = MyArgmnts
                '    End If
                'End If
                If e.CommandLine.Count > 0 Then
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
                RunningExternal(sender, Nothing)
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (" & Application.Info.ProductName & ")", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End Try
        End Sub
        Private Sub RunningExternal(ByVal sender As Object,
                                                        ByVal e As Microsoft.VisualBasic.ApplicationServices.
                                                                      StartupNextInstanceEventArgs)
            Try
                Dim MyArgmnts As String = ""
                If IsNothing(e) Then GoTo AlreadyRunning
                If TypeOf Me.MainForm Is MagNote_Form Then
                    If e.CommandLine.Count > 0 Then
                        MyArgmnts = e.CommandLine.Item(0).ToString()
                        If MagNote_Form.MagNotes_Folder_Path_TxtBx.TextLength > 0 And
                            String.IsNullOrEmpty(MyArgmnts) Then
                            MagNoteFolderPath = MagNote_Form.MagNotes_Folder_Path_TxtBx.Text
                        Else
                            MagNoteFolderPath = Application.Info.DirectoryPath
                        End If
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
                        MagNote_Form.Open_Note_In_New_Tab_ChkBx.CheckState = CheckState.Checked
                        ExternalFilePath = Path.GetDirectoryName(UseArgFile)
                        ExternalFileName = Path.GetFileName(UseArgFile)
                        MagNote_Form.ActiveControl = MagNote_Form.MagNote_No_CmbBx

                        DirectCast(MagNote_Form, MagNote_Form).Open_Note_TlStrpBtn_Click(MagNote_Form.Note_TlStrp.Items("OpenToolStripButton"), EventArgs.Empty)
                        MagNote_Form.Open_Note_In_New_Tab_ChkBx.CheckState = OSINTChkBx.CheckState
                        If MagNote_Form.Note_Font_TxtBx.TextLength > 0 Then
                            RCSN.Font = MagNote_Form.GetFontByString(MagNote_Form.Note_Font_TxtBx.Text)
                        End If
                        MakeTopMost(1)
                    End If
                End If
            Catch ex As Exception
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Finally
                UseArgFile = Nothing
                ExternalFilePath = Nothing
                ExternalFileName = Nothing
                FirstTimeRun = False
            End Try
        End Sub
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object,
                                                                      ByVal e As Microsoft.VisualBasic.ApplicationServices.
                                                                      StartupNextInstanceEventArgs) Handles Me.StartupNextInstance

            If e.CommandLine.Count > 0 Then
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
        End Sub
    End Class
End Namespace
