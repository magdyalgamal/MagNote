Imports System.Windows.Forms
Imports System.Xml
Imports System.Data
Imports System.IO

Public Class Upload_New_Version_Form

    Inherits System.Windows.Forms.Form

    Private resizer As FormResizer
    Private Sub Upload_New_Version_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadForm(Me, Form_ToolTip, Width, Left, Height, Top)
            resizer = New FormResizer(Me)
            LoadXmlIntoDataGridView(MagNoteFolderPath & "\AdditionalFilesToUpload.xml", Additional_Files_To_Upload_DGV)
            Current_Version_TxtBx.Text = Application.ProductVersion
            Additional_Files_To_Upload_DGV.ClearSelection()
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub
    Public Sub LoadXmlIntoDataGridView(xmlPath As String, dgv As DataGridView)
        Try
            dgv.Columns.Add("FileName", "File Name")
            dgv.Columns.Add("SourceFilePath", "Source File Path")
            dgv.Columns.Add("DestinationFilePath", "Destination File Path")
            dgv.Columns.Add("ActiveFile", "Active File")
            dgv.Columns.Add("Uploaded", "Uploaded")
            dgv.Columns.Add("Downloaded", "Downloaded")
            dgv.Columns.Add("IfExistAskToReplaceElseDelete", "If Exist Ask To Relace Else Delete")
            dgv.Columns.Add("UpdatePathInCategoryToDestinationPath", "Update Path In Category To Destination Path")
            dgv.Columns.Add("Descreption", "Descreption")
            dgv.Columns.Add("CreationDate", "Creation Date")
            dgv.Columns.Add("UpdateFileVersion", "Update File Version")
            Using XMLEditor As New XMLEditor("AdditionalFilesToUpload.xml", "AdditionalFilesToUpload")
                For Each fileNode In XMLEditor.doc.Descendants("AdditionalFilesToUpload").Elements
                    dgv.Rows.Add(
                                                fileNode.Name?.ToString,
                                                fileNode.Elements("SourceFilePath").Value?.ToString,
                                                fileNode.Elements("DestinationFilePath").Value?.ToString,
                                                fileNode.Elements("ActiveFile").Value?.ToString,
                                                fileNode.Elements("Uploaded").Value?.ToString,
                                                fileNode.Elements("Downloaded").Value?.ToString,
                                                fileNode.Elements("IfExistAskToReplaceElseDelete").Value?.ToString,
                                                fileNode.Elements("UpdatePathInCategoryToDestinationPath").Value?.ToString,
                                                fileNode.Elements("Descreption").Value?.ToString,
                                                fileNode.Elements("CreationDate").Value?.ToString,
                                                fileNode.Elements("UpdateFileVersion").Value?.ToString)
                Next
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Update_Btn_Click(sender As Object, e As EventArgs) Handles Update_Btn.Click
        Try
            If File_Name_TxtBx.TextLength = 0 Or
                Source_File_Path_TxtBx.TextLength = 0 Or
                Destination_File_Path_TxtBx.TextLength = 0 Or
                Current_Version_TxtBx.TextLength = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "يجب ادخال بيانات صحيحة اولا قبل عملية التحديث"
                Else
                    Msg = "Correct Data Must Be Entered First Before The Update Proceed"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            If Not MagNote_Form.MagNoteFileFormat(File_Name_TxtBx.Text, 1, 0) And
                Update_PathIn_Category_To_Destination_Path_ChkBx.CheckState = CheckState.Checked Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "غير متاح إضافة ملفات ليست من نوعية ماجنوت الى ملف الفئات"
                Else
                    Msg = "It Is Not Possible To Add Non-MagNote Files To The Categories File."
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            Using XMLEditor As New XMLEditor("AdditionalFilesToUpload.xml", "AdditionalFilesToUpload",, Me)
                XMLEditor.Add(File_Name_TxtBx.Text,
                              New Dictionary(Of String, String) From {
                                  {"SourceFilePath", Source_File_Path_TxtBx.Text}},
                              New Dictionary(Of String, String) From {
                                  {"SourceFilePath", Source_File_Path_TxtBx.Text},
                                  {"DestinationFilePath", Destination_File_Path_TxtBx.Text},
                                  {"ActiveFile", Active_File_ChkBx.CheckState},
                                  {"Uploaded", Uploaded_ChkBx.CheckState},
                                  {"Downloaded", Downloaded_ChkBx.CheckState},
                                  {"IfExistAskToReplaceElseDelete", If_Exist_Ask_To_Replace_Else_Delete_ChkBx.CheckState},
                                  {"UpdatePathInCategoryToDestinationPath", Update_PathIn_Category_To_Destination_Path_ChkBx.CheckState},
                                  {"Descreption", Descreption_TxtBx.Text},
                                  {"CreationDate", Now.ToString},
                                  {"UpdateFileVersion", Current_Version_TxtBx.Text}},,,,, 1)
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub File_Name_Btn_Click(sender As Object, e As EventArgs) Handles Source_File_Path_Btn.Click
        Try
            Dim OpenFileDialog As New OpenFileDialog

            OpenFileDialog.Filter = "All files|*.*"
            OpenFileDialog.Multiselect = False
            OpenFileDialog.FileName = ""
            OpenFileDialog.RestoreDirectory = True
            If OpenFileDialog.ShowDialog <> DialogResult.Cancel Then
                File_Name_TxtBx.Text = Replace(Replace(Replace(Path.GetFileName(OpenFileDialog.FileName), "(", ""), ")", ""), " ", "_")
                Source_File_Path_TxtBx.Text = OpenFileDialog.FileName
                Destination_File_Path_TxtBx.Text = "Application.StartupPath\MagNotes_Files\" & Path.GetFileName(OpenFileDialog.FileName)
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تذكر دائما ابدا ان المسار الافتراضى للملف الهدف هو"
                    Msg &= vbNewLine & "Application.StartupPath\MagNotes_Files\"
                    Msg &= vbNewLine & "خلاف ذلك يجب التعديل يدوى على المسار الافتراضى للملف الهدف"
                Else
                    Msg = "Always remember that the default path to the target file is"
                    Msg &= vbNewLine & "Application.StartupPath\MagNotes_Files\"
                    Msg &= vbNewLine & "Otherwise, you must manually modify the default path to the target file."
                End If
                ShowMsg(Msg & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
        End Try

    End Sub
    Private Sub Delete_Btn_Click(sender As Object, e As EventArgs) Handles Delete_Btn.Click
        Try
            If File_Name_TxtBx.TextLength = 0 Or
                Source_File_Path_TxtBx.TextLength = 0 Or
                Destination_File_Path_TxtBx.TextLength = 0 Or
                Update_File_Version_TxtBx.TextLength = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "يجب ادخال بيانات صحيحة اولا قبل عملية الالغاء"
                Else
                    Msg = "Correct Data Must Be Entered First Before The Delete Proceed"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            Using XMLEditor1 As New XMLEditor("AdditionalFilesToUpload.xml", "AdditionalFilesToUpload",, Me)
                XMLEditor1.Delete(File_Name_TxtBx.Text, New Dictionary(Of String, String) From {{"SourceFilePath", Source_File_Path_TxtBx.Text}})
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub
    Private Sub MinimizeFormBtn_Click(sender As Object, e As EventArgs) Handles Minimize_Form_Btn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        MagNote_Form.PaintFindForm(Me)
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

    Private Sub Additional_Files_To_Upload_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Additional_Files_To_Upload_DGV.CellContentClick

    End Sub

    Private Sub Additional_Files_To_Upload_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Additional_Files_To_Upload_DGV.SelectionChanged
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name <> sender.name Then Exit Sub
        ShowRowFields()
    End Sub

    Private Sub Additional_Files_To_Upload_DGV_Click(sender As Object, e As EventArgs) Handles Additional_Files_To_Upload_DGV.Click
        If IsNothing(ActiveControl) Then Exit Sub
        If ActiveControl.Name <> sender.name Then Exit Sub
        ShowRowFields()
    End Sub
    Private Function ShowRowFields(Optional ByVal Currentrow As DataGridViewRow = Nothing)
        Try
            File_Name_TxtBx.Text = Additional_Files_To_Upload_DGV.CurrentRow.Cells("FileName").Value
            Source_File_Path_TxtBx.Text = Additional_Files_To_Upload_DGV.CurrentRow.Cells("SourceFilePath").Value
            Destination_File_Path_TxtBx.Text = Additional_Files_To_Upload_DGV.CurrentRow.Cells("DestinationFilePath").Value
            Active_File_ChkBx.CheckState = Additional_Files_To_Upload_DGV.CurrentRow.Cells("ActiveFile").Value
            Uploaded_ChkBx.CheckState = Additional_Files_To_Upload_DGV.CurrentRow.Cells("Uploaded").Value
            Downloaded_ChkBx.CheckState = Additional_Files_To_Upload_DGV.CurrentRow.Cells("Downloaded").Value
            If_Exist_Ask_To_Replace_Else_Delete_ChkBx.CheckState = Additional_Files_To_Upload_DGV.CurrentRow.Cells("IfExistAskToReplaceElseDelete").Value
            Update_PathIn_Category_To_Destination_Path_ChkBx.CheckState = Additional_Files_To_Upload_DGV.CurrentRow.Cells("UpdatePathInCategoryToDestinationPath").Value
            Descreption_TxtBx.Text = Additional_Files_To_Upload_DGV.CurrentRow.Cells("Descreption").Value
            Creation_Date_TxtBx.Text = Additional_Files_To_Upload_DGV.CurrentRow.Cells("CreationDate").Value
            Update_File_Version_TxtBx.Text = Additional_Files_To_Upload_DGV.CurrentRow.Cells("UpdateFileVersion").Value
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Function

    Private Sub Preview_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Btn.Click
        Try
            Additional_Files_To_Upload_DGV.Rows.Clear()
            LoadXmlIntoDataGridView(MagNoteFolderPath & "\AdditionalFilesToUpload.xml", Additional_Files_To_Upload_DGV)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Upload_Btn_Click(sender As Object, e As EventArgs) Handles Upload_Btn.Click
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            If Not IsConnectedToInternet() Then
                Exit Sub
            End If
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "جارى الآن رفع الملف واعداد اجراءات التحديث للمستخدمين"
            Else
                Msg = "Currently Uploading File Now And Preparing Settings To Upgrade Users"
            End If
            ShowMsg(Msg,,,,,,, 0,,,,,,, Me)
            If Not MagNote_Form.DecryptFTPCredentials(EncryptionKey) Then
                Exit Sub
            End If
            Dim FileName = String.Empty
            Dim UploadedFiles = String.Empty
            Dim UserName = ClientCredentials(0).FTP_UserName
            Dim Password = ClientCredentials(0).FTP_Password
            Dim Address = ClientCredentials(0).FTP_Address
            If MagNote_Form.UploadFileToFtpUsingWebClient(MagNoteFolderPath & "\AdditionalFilesToUpload.xml", Address & "AdditionalFilesToUpload.xml", UserName, Password) Then
                Using XMLEditor As New XMLEditor("AdditionalFilesToUpload.xml", "AdditionalFilesToUpload")
                    Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, XMLEditor.doc.Root.Elements().Count, Me)
                    For Each entry In XMLEditor.doc.Root.Elements()
                        progress += ProgressToAdd
                        Previewlbl.Text = "Loading--> " & entry.Name.ToString & vbNewLine & Math.Floor(progress * 100)
                        Previewlbl.Refresh()
                        Previewlbl.Invalidate()
                        If Convert.ToBoolean(Val(entry.Element("ActiveFile")?.Value.ToString)) And
                            Not Convert.ToBoolean(Val(entry.Element("Uploaded")?.Value.ToString)) Then
                            FileName = entry.Element("SourceFilePath")?.Value.ToString
                            If MagNote_Form.UploadFileToFtpUsingWebClient(FileName, Address & Path.GetFileName(FileName), UserName, Password) Then
                                Dim entryName = entry.Name.ToString
                                UploadedFiles &= entryName & vbNewLine
                                XMLEditor.Add(Replace(Replace(Replace(Path.GetFileName(FileName), " ", "_"), "(", ""), ")", ""),
                                  New Dictionary(Of String, String) From {
                                      {"SourceFilePath", FileName}},
                                  New Dictionary(Of String, String) From {
                                      {"SourceFilePath", FileName},
                                      {"UpdateFileVersion", Application.ProductVersion},
                                      {"Uploaded", 1},
                                      {"CreationDate", Now}}, 0,,, 1, 1)
                            End If
                        End If
                    Next
                    XMLEditor.doc.Save(XMLEditor.filePath)
                    If Not String.IsNullOrEmpty(UploadedFiles) Then
                        MagNote_Form.UploadFileToFtpUsingWebClient(MagNoteFolderPath & "\AdditionalFilesToUpload.xml", Address & "AdditionalFilesToUpload.xml", UserName, Password)
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Msg = "تم رفع الملف(ات) بنجاح"
                        Else
                            Msg = "File(s) Succefully Uploaded"
                        End If
                        ShowMsg(Msg & vbNewLine & UploadedFiles & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                    ElseIf String.IsNullOrEmpty(UploadedFiles) Then
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Msg = "لا توجد ملفات متاحة للرفع... ربما الملف المخصص لذلك لم يتم تحديثه بعد أو تم إعدادة بطريقة خاطئة"
                        Else
                            Msg = "There are no files available to upload ... Maybe the file allocated to that has not been updated yet or prepared by wrong way"
                        End If
                        ShowMsg(Msg & vbNewLine & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                    End If
                End Using
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Sub
End Class
