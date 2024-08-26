Imports System.IO
Imports System.Net

Public Class Update_New_Version_Form
    Private WithEvents WClient As New WebClient
    Private DlStage As Integer
    Private FileName As String = "Current Filename"
    Private LastCount As Integer = 0

    Private Sub Download_Update_Btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Download_Update_Btn.Click
        Try
            Download_Update_Btn.Enabled = False
            Download_PrgrsBr.Visible = True
            Download_PrgrsBr.Style = ProgressBarStyle.Marquee
            Download_PrgrsBr.Refresh()
            Download_Progress_Percentage_TxtBx.Text = Nothing
            Dim Password As String = "EnterYourAccountPassword"
            Dim UserName As String = "EnterYouHostAccount"
            If Update_Download_File_Path_TxtBx.TextLength = 0 Then
                Update_Download_File_Path_TxtBx.Text = Application.StartupPath
            End If
            If File.Exists(Update_Download_File_Path_TxtBx.Text & "\" & File_Name_TxtBx.Text) Then
                My.Computer.FileSystem.DeleteFile(Update_Download_File_Path_TxtBx.Text & "\" & File_Name_TxtBx.Text, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            End If
            Using WClient As New WebClient()
                AddHandler WClient.DownloadProgressChanged, AddressOf wClient_DownloadProgressChanged
                AddHandler WClient.DownloadFileCompleted, AddressOf wClient_DownloadComplete
                WClient.Credentials = New NetworkCredential(UserName, Password)
                WClient.DownloadFileAsync(New Uri("ftp://EnterYourHostccount/" & File_Name_TxtBx.Text), Update_Download_File_Path_TxtBx.Text & "\" & File_Name_TxtBx.Text)
            End Using

        Catch ex As Exception
            ShowMsg(ex.Message & vbNewLine & "Download Failed", "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Download_PrgrsBr.Visible = False
            Download_Update_Btn.Enabled = True
            WClient.Dispose()
        End Try
    End Sub

    Private Sub wClient_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WClient.DownloadProgressChanged
        Download_PrgrsBr.Value = e.ProgressPercentage
        If e.ProgressPercentage > LastCount Then
            Download_PrgrsBr.Style = ProgressBarStyle.Blocks
            Download_PrgrsBr.Refresh()
            Download_Progress_Percentage_TxtBx.Text = File_Name_TxtBx.Text & "... " & LastCount.ToString & " % Completed"
            LastCount += 5
        End If
    End Sub

    Private Sub wClient_DownloadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles WClient.DownloadFileCompleted
        Download_Progress_Percentage_TxtBx.Text = File_Name_TxtBx.Text & "... " & "100 % Complete"
        LastCount = 0
        If File.Exists(Update_Download_File_Path_TxtBx.Text & "\" & File_Name_TxtBx.Text) Then
            ShowMsg("Download Completed", "InfoSysMe (StickNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            If CreatSchedualTask() Then
                Application.Exit()
            End If
        End If
        Download_PrgrsBr.Visible = False
        WClient.Dispose()
        Download_Update_Btn.Enabled = True
    End Sub

    Private Sub Update_New_Version_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
    End Sub

    Private Sub Update_Download_File_Path_Btn_Click(sender As Object, e As EventArgs) Handles Update_Download_File_Path_Btn.Click
        Try
            Dim BFP As New FolderBrowserDialog
            If BFP.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                Update_Download_File_Path_TxtBx.Text = BFP.SelectedPath
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentStickInfo(), "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
End Class