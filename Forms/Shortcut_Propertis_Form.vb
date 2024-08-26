Imports System.IO
Imports Shell32
Public Class Shortcut_Propertis_Form



    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        Me.Close()
    End Sub

    Private Sub Shortcut_Path_Btn_Click(sender As Object, e As EventArgs) Handles Shortcut_Path_Btn.Click

        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.Title = "Shortcut Path"
        OpenFileDialog.Filter = "Current File|" & Path.GetFileName(Shortcut_Path_TxtBx.Text) & "|All files|*.*"
        OpenFileDialog.Multiselect = False
        'OpenFileDialog.FileName = Path.GetFileName(Shortcut_Path_TxtBx.Text)
        OpenFileDialog.RestoreDirectory = True
        OpenFileDialog.FileName = Nothing
        OpenFileDialog.InitialDirectory = Path.GetDirectoryName(Shortcut_Path_TxtBx.Text)
        If OpenFileDialog.ShowDialog <> DialogResult.Cancel Then
            Shortcut_Path_TxtBx.Text = OpenFileDialog.FileName
        End If


    End Sub

    Private Sub Picture_Parh_Btn_Click(sender As Object, e As EventArgs) Handles Picture_Path_Btn.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.Title = "Picture Path"
        OpenFileDialog.Filter = "Current File|" & Path.GetFileName(Shortcut_Picture_Path_TxtBx.Text) & "|All files|*.*"
        OpenFileDialog.Multiselect = False
        OpenFileDialog.FileName = Path.GetFileName(Shortcut_Picture_Path_TxtBx.Text)
        OpenFileDialog.RestoreDirectory = True
        OpenFileDialog.InitialDirectory = Path.GetDirectoryName(Shortcut_Picture_Path_TxtBx.Text)
        If OpenFileDialog.ShowDialog <> DialogResult.Cancel Then
            Shortcut_Picture_Path_TxtBx.Text = OpenFileDialog.FileName
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ShowProperties(Shortcut_Path_TxtBx.Text)
    End Sub

    Private Sub Find_Shortcut_Link_Btn_Click(sender As Object, e As EventArgs) Handles Find_Shortcut_Link_Btn.Click
        Dim target As String = Shortcut_Path_TxtBx.Text
        Shortcut_Link_TxtBx.Text = GetShortcutTargetFile(target)
        Exit Sub
    End Sub


    Private Sub Update_Shortcut_Link_Btn_Click(sender As Object, e As EventArgs) Handles Update_Shortcut_Link_Btn.Click
        Try
            Dim GSTF = GetShortcutTargetFile(Shortcut_Path_TxtBx.Text, 1)
            Select Case GSTF.GetType()
                Case GetType(Boolean)
                Case GetType(String)
                    GSTF = 0
            End Select
            If GSTF Then
                'MagNote_Form.LoadList(MagNoteFolderPath & "\FilesShortcuts.txt")
                'Dim ItemName
                'For Each itm In MagNote_Form.Shortcuts_LstVw.Items
                '    If itm.name = Name_TxtBx.Text Then
                '        ItemName = itm.ImageKey
                '        Exit For
                '    End If
                'Next
                If File.Exists(Shortcut_Picture_Path_TxtBx.Text) Then
                    If (MagNote_Form.imageList1.Images.ContainsKey(Available_Shortcuts_CmbBx.Text)) Then
                        Dim PictureFullPath = MagNoteFolderPath & "\Shortcuts\Images\" & Available_Shortcuts_CmbBx.Text & Path.GetExtension(Shortcut_Picture_Path_TxtBx.Text)
                        If Shortcut_Picture_Path_TxtBx.Text <> PictureFullPath Then
                            File.Copy(Shortcut_Picture_Path_TxtBx.Text, PictureFullPath, 1)
                        End If
                        MagNote_Form.imageList1.Images.Add(Available_Shortcuts_CmbBx.Text, Image.FromFile(PictureFullPath))
                    End If
                End If
                MagNote_Form.SaveList(MagNote_Form.Shortcuts_LstVw.Name, 0)
                MagNote_Form.LoadList(MagNote_Form.Shortcuts_LstVw.Name)
                ShowMsg("Update Successfully Done" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
            If ex.Message <> "Out of memory." Then
                ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'add the proper link here
        Try
            Available_Shortcuts_CmbBx.ValueMember = "Key"
            Available_Shortcuts_CmbBx.DisplayMember = "Value"
            Dim Files() = Directory.GetFiles(MagNoteFolderPath, "*_(Shortcuts_links).txt")
            For Each Shortcuts_links_File In Files
                Dim FilePaths() = Split(My.Computer.FileSystem.ReadAllText(Shortcuts_links_File, System.Text.Encoding.UTF8), ",")
                For Each File In FilePaths
                    Dim Fil() = Split(File, "$")
                    Available_Shortcuts_CmbBx.Items.Add(New KeyValuePair(Of String, String)(Fil(0), Path.GetFileName(Fil(0))))
                Next
            Next
            Dim target As String = Shortcut_Path_TxtBx.Text '"C:\Users\your user name\Desktop\DirectX.lnk"
            If IsNothing(IsLink(target)) Then Exit Sub
            Shortcut_Link_TxtBx.Text = GetShortcutTargetFile(target).ToString
            Dim icon = System.Drawing.Icon.ExtractAssociatedIcon(Shortcut_Path_TxtBx.Text)
            Shortcut_Icon_PctrBx.Image = icon.ToBitmap
            Me.BackColor = MagNote_Form.Note_Back_Color_ClrCmbBx.SelectedItem 'BackColor
            Me.ForeColor = MagNote_Form.Note_Font_Color_ClrCmbBx.SelectedItem ' ForeColor
            Me.Opacity = MagNote_Form.Form_Transparency_TrkBr.Value / 100
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Function GetLnkTarget(lnkPath As String) As String
        'Dim shl = New Shell32.Shell()
        '' Move this to class scope
        'lnkPath = System.IO.Path.GetFullPath(lnkPath)
        'Dim dir = shl.[NameSpace](System.IO.Path.GetDirectoryName(lnkPath))
        'Dim itm = dir.Items().Item(System.IO.Path.GetFileName(lnkPath))
        Dim itm = IsLink(lnkPath)
        If Not IsNothing(itm) Then
            Dim lnk = DirectCast(itm.GetLink, Shell32.ShellLinkObject)
            Try
                Dim arguments = lnk.Arguments
                Dim location = lnk.WorkingDirectory
                Dim lnkPath1 = lnk.Path
            Catch ex As Exception
            End Try
            Return lnk.Target.Path
        End If
    End Function
    Public Function GetShortcutTargetFile(ByVal shortcutFilename As String, Optional ByVal Update As Boolean = False) As Object
        Try
            Dim pathOnly As String = Path.GetDirectoryName(shortcutFilename)
            Dim filenameOnly As String = Path.GetFileName(shortcutFilename)
            Dim shell As Shell = New Shell()
            Dim folder As Folder = shell.[NameSpace](pathOnly)
            Dim folderItem As FolderItem = folder.ParseName(filenameOnly)
            If folderItem IsNot Nothing Then
                If folderItem.IsLink Then
                    Dim oShell As Object = CreateObject("WScript.Shell")
                    Dim oLink As Object = oShell.CreateShortcut(shortcutFilename)
                    If Update Then
                        oLink.TargetPath = Shortcut_Link_TxtBx.Text
                        Try
                            oLink.WorkingDirectory = pathOnly
                            'oLink.IconLocation = shell.ExpandEnvironmentStrings(Picture_Path_TxtBx.Text)
                            oLink.IconLocation = Shortcut_Picture_Path_TxtBx.Text & ", 0"
                        Catch ex As Exception
                        End Try
                        oLink.WindowStyle = 1
                        oLink.Description = Shortcut_Description_TxtBx.Text
                        oLink.Save()
                        Return True
                    Else
                        Return GetLnkTarget(shortcutFilename)
                    End If
                ElseIf File.Exists(Shortcut_Path_TxtBx.Text) Or
                    Directory.Exists(Shortcut_Path_TxtBx.Text) Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "الاختصار غير قابل للتعديل ... هل تريد إنشاء آخر قابل للتعديل؟"
                    Else
                        Msg = "Shortcut Is Not Editable... Do You Want To Create Another One?"
                    End If

                    If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBOs, False) = DialogResult.Yes Then
                        Clipboard.SetText(Shortcut_Path_TxtBx.Text)
                        MagNote_Form.Create_Shortcut_From_Clipboard_Click(Me, EventArgs.Empty)
                        If File.Exists(Shortcut_Path_TxtBx.Text) Or
                            Directory.Exists(Shortcut_Path_TxtBx.Text) Then
                            'Dim Shortcut = New ListViewItem(Path.GetFileName(Shortcut_Path_TxtBx.Text), 1)
                            Dim Shortcut = MagNote_Form.Shortcuts_LstVw.Items.Find(Shortcut_Text_TxtBx.Text, 1)
                            If Shortcut.Length > 0 Then
                                MagNote_Form.Shortcuts_LstVw.Items.Remove(Shortcut(0))
                                If Not IsNothing(IsLink(Shortcut(0).Tag)) Then
                                    If File.Exists(Shortcut(0).Tag) Or
                                        Directory.Exists(Shortcut(0).Tag) Then
                                        File.Delete(Shortcut(0).Tag)
                                    End If
                                End If
                            End If
                            MagNote_Form.SaveList(MagNote_Form.Shortcuts_LstVw.Name, 0)
                            MagNote_Form.LoadList(MagNote_Form.Shortcuts_LstVw.Name)
                        End If
                        Return True
                        Else
                        Return GetLnkTarget(shortcutFilename)
                    End If
                End If
            End If
            Return String.Empty
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private Sub Picture_Path_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Shortcut_Picture_Path_TxtBx.TextChanged
        Try
            If File.Exists(Shortcut_Picture_Path_TxtBx.Text) Then
                If Not IsNothing(IsLink(Shortcut_Path_TxtBx.Text)) Then
                    If Not IsNothing(IsLink(Shortcut_Picture_Path_TxtBx.Text)) Then
                        Dim icon = System.Drawing.Icon.ExtractAssociatedIcon(Shortcut_Path_TxtBx.Text)
                        Shortcut_Icon_PctrBx.Image = icon.ToBitmap
                    Else
                        Shortcut_Icon_PctrBx.Image = Image.FromFile(Shortcut_Picture_Path_TxtBx.Text)
                    End If
                Else
                    Shortcut_Icon_PctrBx.Image = Nothing
                    Dim FileNumber = -1
                    Dim TempIconPath = MagNoteFolderPath & "\temp\myfilename" & FileNumber & ".ico"
                    While File.Exists(TempIconPath)
                        FileNumber += 1
                        TempIconPath = MagNoteFolderPath & "\temp\myfilename" & FileNumber & ".ico"
                        Try
                            File.Delete(TempIconPath)
                        Catch ex As Exception
                        End Try
                    End While
                    Dim Img As Bitmap
                    Try
                        Img = ReturnIcon(Shortcut_Path_TxtBx.Text, 0).ToBitmap
                    Catch ex As Exception
                    End Try
                    If Not IsNothing(Img) Then
                        Img.Save(TempIconPath, System.Drawing.Imaging.ImageFormat.Icon)
                        Shortcut_Icon_PctrBx.Image = Image.FromFile(TempIconPath) ' ReturnIcon(Shortcut_Path_TxtBx.Text, 0)
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Equals("Out of memory.") Then Exit Sub
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Declare Function ExtractIcon Lib "shell32.dll" Alias "ExtractIconExA" (ByVal lpszFile As String, ByVal nIconIndex As Integer, ByRef phiconLarge As Integer, ByRef phiconSmall As Integer, ByVal nIcons As Integer) As Integer
    Private Function ReturnIcon(ByVal Path As String, ByVal Index As Integer, Optional ByVal small As Boolean = False) As Icon
        Dim bigIcon As Integer
        Dim smallIcon As Integer

        ExtractIcon(Path, Index, bigIcon, smallIcon, 1)

        If bigIcon = 0 Then
            ExtractIcon(Path, 0, bigIcon, smallIcon, 1)
        End If

        If bigIcon <> 0 Then
            If small = False Then
                Return Icon.FromHandle(bigIcon)
            Else
                Return Icon.FromHandle(smallIcon)
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Sub Available_Shortcuts_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Available_Shortcuts_CmbBx.SelectedIndexChanged

    End Sub

    Private Sub Shortcut_Propertis_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Shortcut_Icon_PctrBx.Image = Nothing
        Me.Dispose()
    End Sub
End Class