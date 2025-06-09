Imports System.IO
Imports System.Net

Public Class Update_New_Version_Form
    Dim UpdateFileName As String = String.Empty
    Dim SourceFileName As String = String.Empty
    Dim DestinationFileName As String = String.Empty
    Dim UpdateFileVersion As String = String.Empty
    Dim UpdateDownloadFilePath As String = String.Empty
    Dim DownloadedFiles As String = String.Empty
    Private Function UploadFiles() As Boolean
        Try
            If Not MagNote_Form.UpdateIsAvailable(Me) Then
                Exit Function
            End If
            UpdateFileName = String.Empty
            SourceFileName = String.Empty
            DestinationFileName = String.Empty
            UpdateFileVersion = String.Empty
            UpdateDownloadFilePath = String.Empty
            If Not IsInternetAvailable() Then
                Return False
            End If
            If Not MagNote_Form.DecryptFTPCredentials(Nothing) Then
                Return False
            End If
            Dim Password As String = ClientCredentials(0).FTP_Password
            Dim UserName As String = ClientCredentials(0).FTP_UserName
            Dim USNFI As String = MagNoteFolderPath & "\AdditionalFilesToUpload.xml"
            If File.Exists(USNFI) Then
                SourceFileName = String.Empty
                DestinationFileName = String.Empty
                Dim FileName = String.Empty
                Dim Address = ClientCredentials(0).FTP_Address
                Using XMLEditor As New XMLEditor("AdditionalFilesToUpload.xml", "AdditionalFilesToUpload")
                    'Dim MagNoteExe = String.Empty
                    DownloadedFiles = String.Empty
                    For Each entry In XMLEditor.doc.Root.Elements()
                        If Convert.ToBoolean(Val(entry.Element("ActiveFile")?.Value.ToString)) Then
                            FileName = entry.Element("DestinationFilePath")?.Value.ToString
                            UpdateFileVersion = entry.Element("UpdateFileVersion")?.Value.ToString
                            Dim FileExtension = Path.GetExtension(FileName)
                            FileName = Replace(FileName, FileExtension, "") & "-Copy" & FileExtension
                            If MagNote_Form.DownloadFile(entry.Element("DestinationFilePath")?.Value.ToString, FileName) Then
                                DownloadedFiles &= Path.GetFileName(FileName) & vbNewLine
                                'If entry.Name.ToString = "MagNote.exe" Then
                                '    MagNoteExe = entry.Element("SourceFilePath")?.Value.ToString
                                'End If
                                ApplicationDoEvents(Me)
                                Dim entryName = Replace(Replace(Replace(Path.GetFileName(entry.Element("DestinationFilePath")?.Value.ToString), "(", ""), ")", ""), " ", "_")
                                XMLEditor.Add(entryName,
                                  New Dictionary(Of String, String) From {
                                      {"SourceFilePath", entry.Element("SourceFilePath")?.Value.ToString}},
                                  New Dictionary(Of String, String) From {
                                      {"SourceFilePath", entry.Element("SourceFilePath")?.Value.ToString},
                                      {"Downloaded", 1},
                                      {"CreationDate", Now}}, 0,,, 1, 1)
                                Try
                                    Dim DestinationFilePath = Replace(entry.Element("DestinationFilePath")?.Value.ToString, "Application.StartupPath", ApplicationStartupPath)
                                    If Convert.ToBoolean(Val(entry.Element("IgnoreIfExist")?.Value.ToString)) Then
                                        If File.Exists(DestinationFilePath) Then
                                            Continue For
                                        End If
                                    ElseIf Convert.ToBoolean(Val(entry.Element("IfExistAskToReplace")?.Value.ToString)) Then
                                        If File.Exists(DestinationFilePath) Then
                                            If MagNote_Form.Language_Btn.Text = "E" Then
                                                Msg = "هذا الملف موجود بالفعل ... هل تريد الاستمرار... سيتم استبدال الملف الحالى؟"
                                            ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                                                Msg = "This File Already Exist... Do You Want To Continue... Current File Will Be Replaced?"
                                            End If
                                            If ShowMsg(Msg & vbNewLine & DestinationFilePath & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs,,,,,,,,, Me) = DialogResult.No Then
                                                DestinationFilePath = Replace(DestinationFilePath, Path.GetExtension(DestinationFilePath), "") & "-Copy" & Path.GetExtension(DestinationFilePath)
                                                My.Computer.FileSystem.DeleteFile(DestinationFilePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                                                Continue For
                                            End If
                                        End If
                                    End If
                                    If Convert.ToBoolean(Val(entry.Element("UpdatePathInCategoryToDestinationPath")?.Value.ToString)) Then
                                        MagNote_Form.SaveNoteCategory(Replace(DestinationFilePath, "-Copy", ""),, 0, "ملفات_المساعدة")
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    Next
                    XMLEditor.doc.Save(XMLEditor.filePath)
                    'XMLEditor.Add("MagNote.exe",
                    '              New Dictionary(Of String, String) From {
                    '                  {"SourceFilePath", MagNoteExe}},
                    '              New Dictionary(Of String, String) From {
                    '                  {"SourceFilePath", MagNoteExe},
                    '                  {"Downloaded", 0},
                    '                  {"CreationDate", Now}}, 0,,,, 1)
                    'DownloadedFiles = Replace(DownloadedFiles, "MagNote.exe" & vbNewLine, "")
                    If DownloadedFiles.Length > 0 Then
                        Return True
                    End If
                End Using
            End If
        Catch ex As Exception
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "لم تفلح محاولة التحقق من وجود تحيث للبرنامج"
            ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "Couldn't Check To Find New Version Of The Progarm"
            End If
            ShowMsg(Msg & vbNewLine & ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2, MBOs, 0, 0,,,,,,, Me)
            Return False
        Finally
        End Try
    End Function
    Private Sub Download_Update_Btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Download_Update_Btn.Click
        Try
            If Not UploadFiles() Then
                Exit Sub
            End If
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "تم الانتهاء من تحميل الملفات"
            ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "Download Completed"
            End If
            ShowWindowsNotification(Msg, 1)
            If CreatSchedualTask() Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تم الانتهاء من إعدادا ملفات التحميل للتنفيذ"
                ElseIf MagNote_Form.Language_Btn.Text = "ع" Then
                    Msg = "Preparing Downloaded Files To Be Applied Is Completed"
                End If
                ShowWindowsNotification(Msg, 1)
                ApplicationRestart = True
                Application.Exit()
            End If

        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & "Download Failed", "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            Download_PrgrsBr.Visible = False
            Download_Update_Btn.Enabled = True
        End Try
    End Sub

    Private resizer As FormResizer
    Private Sub Update_New_Version_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadForm(Me, Form_ToolTip, Width, Left, Height, Top)
        resizer = New FormResizer(Me)
    End Sub
    Private Sub Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        MagNote_Form.PaintFindForm(Me)
    End Sub
    Private Sub MinimizeFormBtn_Click(sender As Object, e As EventArgs) Handles Minimize_Form_Btn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub MaximizeFormBtn_Click(sender As Object, e As EventArgs) Handles Maximize_Form_Btn.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Maximize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.upgrade
            Me.WindowState = FormWindowState.Normal
        Else
            Maximize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.DownGrade
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub
    Private Sub ExitFormBtn_Click(sender As Object, e As EventArgs) Handles Exit_Form_Btn.Click
        Me.Close()
    End Sub

    Private Sub Update_Download_File_Path_Btn_Click(sender As Object, e As EventArgs) Handles Update_Download_File_Path_Btn.Click
        Try
            Dim BFP As New FolderBrowserDialog
            If BFP.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Update_Download_File_Path_TxtBx.Text = BFP.SelectedPath
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Current_Version_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Current_Version_TxtBx.TextChanged

    End Sub

    Private Sub Update_Version_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Update_Version_TxtBx.TextChanged

    End Sub

    Private Sub File_Name_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles File_Name_TxtBx.TextChanged

    End Sub
End Class