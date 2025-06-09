Imports System.IO
Imports System.Net
Imports Microsoft.Win32.TaskScheduler

Module CheckForNewVersion
    Dim UpdateCopressData As String = String.Empty
    Dim UpdateSelectFile As String = String.Empty
    Dim UpdateFileName As String = String.Empty
    Dim UpdateFileNameLocal As String = String.Empty
    Dim UpdateFileVersion As String = String.Empty
    Dim UpdateResourcesFileTypeName As String = String.Empty
    Dim UpdateCompany_Name As String = String.Empty
    Dim UpdateByNotification As String = String.Empty
    Dim UpdateUsers() As String
    Dim UpdateUpgradeAnyVersion As String = String.Empty
    Dim UpdateServerOnly As String = String.Empty
    Dim UpdateFileDONE As String = String.Empty
    Dim UpdateCopanyHostFolderName As String = String.Empty
    Dim UpdateUsingHostRootFolder As String = String.Empty
    Dim LocalFileFullPath As String = String.Empty
    Dim UpdateDescription As String = String.Empty
    Dim UpdateUpgradeSelectionsOnly As String = String.Empty
    Dim UpdateMandatoryUpgrade As String = String.Empty
    Dim UpdateImmediatelyUpgrade As String = String.Empty
    Dim UpdateRunAtWindowsStart As String = String.Empty
    Dim UpdateNotAIOFiles As String = String.Empty
    Dim UpdateExecutableFile As String = String.Empty
    Dim UpdateIncludeToCheckVersion As String = String.Empty
    Dim UpdateOperationCompleted As String = String.Empty
    Dim UpdateActive As String = String.Empty
    Dim UpdateLastTimeSeen As String = String.Empty
    Dim client As New WebClient()
    Dim SourceFile As String = String.Empty
    Dim Check_For_New_Version_At_Host_Is_Working As Integer

    ''' <summary>
    ''' Update File DONE 1 = File Downloaded
    ''' Update File DONE 2 = Object Replaced And System Restarted
    ''' Update File DONE 3 = Users Prepared To Update
    ''' Update File DONE 4 = Operation Finished
    ''' </summary>
    ''' <returns></returns>
    Public Function Check_For_New_Version_At_Host() As Boolean
        Try
            'Debugger.Launch()
            Dim UCN As New TextBox
            LocalFileFullPath = ApplicationStartupPath & "\UpdateMagNoteFileInformation.txt"
            SourceFile = "InfoSysMeClients/UpdateMagNoteFileInformation.txt"
            If Not UpDownloadFile(SourceFile, LocalFileFullPath) Then
                Exit Function
            End If
            If File.Exists(LocalFileFullPath) Then
                Dim UpdateFileInformation() = Split(Replace(Replace(My.Computer.FileSystem.ReadAllText(LocalFileFullPath, System.Text.Encoding.UTF8), vbCrLf, ""), vbLf, ""), ",")
                For Each Line In UpdateFileInformation
                    If Left(Line, 2) = "//" Then Continue For 'Ignore This Line
                    For Each item As String In Line.Split(":") '.Last.Split("|")
                        Select Case item
                            Case "Update File Name"
                                UpdateFileName = Split(Line, ":").ToList.Item(1)
                            Case "Update File Version"
                                UpdateFileVersion = Split(Line, ":").ToList.Item(1)
                        End Select
                    Next
                Next
                WriteUpdateLastTimeSeen()
                If CreatSchedualTask() Then
                    ApplicationRestart = True
                    Application.Exit()
                End If
                Exit Function
                If Val(UpdateFileDONE) = 4 Then
                    Exit Function
                ElseIf Val(UpdateFileDONE) = 3 Then 'Update Database Prepared For Users
                    If CType(Val(UpdateByNotification), Boolean) Then
                        MessageBox.Show("Update File DONE = (" & UpdateFileDONE & ") Update Will Be Ended Now Forever Till New Version Appears", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                    End If
                    Dim FileContentsToUpdate = My.Computer.FileSystem.ReadAllText(LocalFileFullPath, System.Text.Encoding.Default)
                    FileContentsToUpdate = Replace(FileContentsToUpdate, "Update File DONE:3", "Update File DONE:4")
                    My.Computer.FileSystem.WriteAllText(LocalFileFullPath, FileContentsToUpdate, 0, System.Text.Encoding.Default)
                    UpDownloadFile(LocalFileFullPath, SourceFile, 1)
                    Exit Function
                ElseIf Val(UpdateFileDONE) = 2 And Val(UpdateServerOnly) = 1 Then
                    Dim FileContentsToUpdate = My.Computer.FileSystem.ReadAllText(LocalFileFullPath, System.Text.Encoding.Default)
                    FileContentsToUpdate = Replace(FileContentsToUpdate, "Update File DONE:2", "Update File DONE:4")
                    My.Computer.FileSystem.WriteAllText(LocalFileFullPath, FileContentsToUpdate, 0, System.Text.Encoding.Default)
                    UpDownloadFile(LocalFileFullPath, SourceFile, 1)
                    Exit Function
                ElseIf Val(UpdateFileDONE) = 1 Then
                    GoTo ReRunAIO
                End If
                If CType(Val(UpdateByNotification), Boolean) Then
                    If MessageBox.Show("There's An Update File Ready To Run. Do You Want To Update Now?", "", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                        Exit Function
                    End If
                End If
                If Not UpDownloadFile(UpdateFileName, ApplicationStartupPath & "\" & UpdateFileNameLocal) Then
                    Exit Function
                End If
                If File.Exists(ApplicationStartupPath & "\" & UpdateFileNameLocal) Then
                    Dim FileContentsToUpdate = My.Computer.FileSystem.ReadAllText(LocalFileFullPath, System.Text.Encoding.UTF8)
                    FileContentsToUpdate = Replace(FileContentsToUpdate, "Update File DONE:0", "Update File DONE:1")
                    My.Computer.FileSystem.WriteAllText(LocalFileFullPath, FileContentsToUpdate, 0, System.Text.Encoding.Default)
                    If Not UpDownloadFile(LocalFileFullPath, SourceFile, 1) Then
                        Exit Function
                    End If
                End If
ReRunAIO:
                If CreatSchedualTask() Then
                    If CType(Val(UpdateByNotification), Boolean) Then
                        If File.Exists(ApplicationStartupPath & "\" & UpdateFileNameLocal) Then
                            MessageBox.Show("Download File (AIO.Exe.Zip) Completed", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                        Else
                            MessageBox.Show("Error While Downloading File (AIO.Exe.Zip)", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                            Exit Function
                        End If
                    End If
                    If File.Exists(ApplicationStartupPath & "\" & UpdateFileNameLocal) Then
                        Dim FileContentsToUpdate = My.Computer.FileSystem.ReadAllText(LocalFileFullPath, System.Text.Encoding.UTF8)
                        FileContentsToUpdate = Replace(FileContentsToUpdate, "Update File DONE:1", "Update File DONE:2")
                        My.Computer.FileSystem.WriteAllText(LocalFileFullPath, FileContentsToUpdate, 0, System.Text.Encoding.Default)
                        If Not UpDownloadFile(LocalFileFullPath, SourceFile, 1) Then
                            Exit Function
                        End If
                    End If
                End If
                'Next
            End If
        Catch ex As Exception
            Return False
        Finally
            client.Dispose()
        End Try
    End Function
    Private Function WriteUpdateLastTimeSeen() As Boolean
        Try
            Dim FileContentsToUpdate = My.Computer.FileSystem.ReadAllText(LocalFileFullPath, System.Text.Encoding.UTF8)
            FileContentsToUpdate = Replace(FileContentsToUpdate, UpdateLastTimeSeen, "Update Last Time Seen:" & Now.ToString)
            My.Computer.FileSystem.WriteAllText(LocalFileFullPath, FileContentsToUpdate, 0, System.Text.Encoding.Default)
            If Not UpDownloadFile(LocalFileFullPath, SourceFile, 1) Then
                If CType(Val(UpdateByNotification), Boolean) Then
                    MessageBox.Show("Couldn't Update Last Time Seen", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
                End If
            End If
        Catch ex As Exception
        Finally
        End Try
    End Function

    Public Function UpDownloadFile(ByVal SourceFile As String, Optional ByVal TargetFile As String = Nothing, Optional ByVal Upload As Boolean = False)
        Try
            Dim Password As String = FTP_Login(0).FTP_Password
            Dim UserName As String = FTP_Login(0).FTP_UserName
            Using client As New WebClient()
                If File.Exists(TargetFile) And Not Upload Then
                    My.Computer.FileSystem.DeleteFile(TargetFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
                client.Credentials = New NetworkCredential(UserName, Password)
                If Upload Then
                    client.UploadFile("ftp://" & FTP_Login(0).FTP_UserName & "@" & FTP_Login(0).FTP_Address & "/YourDomain/" & TargetFile, SourceFile)
                Else
                    client.DownloadFile("ftp://" & FTP_Login(0).FTP_UserName & "@" & FTP_Login(0).FTP_Address & "/YourDomain/" & SourceFile, TargetFile)
                End If
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub RunCommandCom(command As String, arguments As String, permanent As Boolean)
        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = " " + If(permanent = True, "/K", "/C") + " " + command + " " + arguments
        pi.FileName = "cmd.exe"
        p.StartInfo = pi
        Dim unused = p.Start()
    End Sub

    Public Function CloseProcess(ByVal ProcessName)
        Dim clsProcess As New Process   'create new instance of class process
        Dim ProcessCount = 0
        For Each clsProcess In Process.GetProcesses 'list all the processes
            'Debug.Print(clsProcess.ProcessName.ToString)
            If clsProcess.ProcessName = ProcessName Then
                clsProcess.Close()
            End If
        Next
    End Function
    Public Function CreatSchedualTask(Optional ByVal DeleteTaskName As String = Nothing, Optional ByVal RestartApplication As Boolean = False) As Boolean
        Try
            If File.Exists(ApplicationStartupPath & "\MagNote.bat") Then
                My.Computer.FileSystem.DeleteFile(ApplicationStartupPath & "\MagNote.bat", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            End If
            Dim CommandLine = String.Empty
            Dim ReRunMe = String.Empty
            If RestartApplication Then
                CommandLine = ApplicationStartupPath & "\MagNote.exe"
                CommandLine &= "Exit" & vbNewLine
            Else
                Using XMLEditor As New XMLEditor("AdditionalFilesToUpload.xml", "AdditionalFilesToUpload")
                    For Each entry In XMLEditor.doc.Root.Elements()
                        If Convert.ToBoolean(Val(entry.Element("ActiveFile")?.Value.ToString)) And
                            Convert.ToBoolean(Val(entry.Element("Uploaded")?.Value.ToString)) And
                            Convert.ToBoolean(Val(entry.Element("Downloaded")?.Value.ToString)) Then
                            Dim FileExtention = Path.GetExtension(entry.Element("DestinationFilePath")?.Value.ToString)
                            Dim DestinationFilePath = Replace(Replace(entry.Element("DestinationFilePath")?.Value.ToString, "Application.StartupPath", ApplicationStartupPath), FileExtention, "") & "-Copy" & FileExtention
                            CommandLine &= "cd /d " & Chr(34) & "%~dp0" & Chr(34) & vbNewLine
                            CommandLine &= "move /Y " & Chr(34) & DestinationFilePath & Chr(34) & " " & Chr(34) & Replace(DestinationFilePath, "-Copy", "") & Chr(34) & vbNewLine
                        End If
                    Next
                    If CommandLine IsNot Nothing Then
                        CommandLine &= ApplicationStartupPath & "\MagNote.exe" & vbNewLine
                        CommandLine &= "Exit" & vbNewLine
                    Else
                        Exit Function
                    End If
                End Using
            End If
            My.Computer.FileSystem.WriteAllText(ApplicationStartupPath & "\MagNote.bat", CommandLine, 0, System.Text.Encoding.Default)
            If Not File.Exists(ApplicationStartupPath & "\MagNote.vbs") Then
                CommandLine = "Dim WinScriptHost
Set WinScriptHost = CreateObject(" & Chr(34) & "WScript.Shell" & Chr(34) & ")
WinScriptHost.Run " & Chr(34) & ApplicationStartupPath & "\MagNote.bat" & Chr(34) & ", 0
Set WinScriptHost = Nothing"
                My.Computer.FileSystem.WriteAllText(ApplicationStartupPath & "\MagNote.vbs", CommandLine, 0, System.Text.Encoding.Default)
            End If
            Using ts As New TaskService()
                If Not IsNothing(DeleteTaskName) Then Exit Function
                Dim td As TaskDefinition = ts.NewTask
                td.RegistrationInfo.Description = "Run MagNote After Downloading New Update"
                Dim wt As New TimeTrigger
                wt.StartBoundary = Now.AddSeconds(5)
                td.Triggers.Add(wt)
                td.Actions.Add(New ExecAction(ApplicationStartupPath & "\MagNote.vbs"))
                ts.RootFolder.RegisterTaskDefinition("ReRunMagNote", td)
                Return True
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
End Module
