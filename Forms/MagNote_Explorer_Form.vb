Imports System.ComponentModel
Imports System.IO

Public Class MagNote_Explorer_Form
    Dim fileExplorer As FileExplorerClss
    Private resizer As FormResizer
    Private Sub MagNote_Explorer_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadForm(Me, Form_ToolTip, Width, Left, Height, Top, 1)
            resizer = New FormResizer(Me)
            Dim ImageList As New ImageList()
            ImageList.Images.Add("folder", My.Resources.open_folder_outline_icon1)  'SystemIcons.Shield.ToBitmap())
            ImageList.Images.Add("file", SystemIcons.Application.ToBitmap())

            'Initialize FileExplorerClss
            fileExplorer = New FileExplorerClss(Mag_Explorer_Directory_TrVw, Mag_Explorer_Directory_Contents_LstVw, ImageList)
            Me.FormBorderStyle = FormBorderStyle.None
            AddHandler Mag_Explorer_Directory_Contents_LstVw.ColumnClick, AddressOf Mag_Explorer_Directory_Contents_LstVw_ColumnClick
        Catch ex As Exception
            MessageBox.Show($"Error loading folders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private lastSortedColumn As Integer = -1
    Private lastSortOrder As SortOrder = SortOrder.None

    Private Sub Mag_Explorer_Directory_Contents_LstVw_ColumnClick(sender As Object, e As ColumnClickEventArgs)
        Try
            ' Toggle sorting order if same column is clicked
            If e.Column = lastSortedColumn Then
                If lastSortOrder = SortOrder.Ascending Then
                    lastSortOrder = SortOrder.Descending
                Else
                    lastSortOrder = SortOrder.Ascending
                End If
            Else
                lastSortOrder = SortOrder.Ascending
            End If
            lastSortedColumn = e.Column
            Mag_Explorer_Directory_Contents_LstVw.ListViewItemSorter = New ListViewItemComparerSort(e.Column, lastSortOrder)
            Mag_Explorer_Directory_Contents_LstVw.Sort()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Go_To_Btn_Click(sender As Object, e As EventArgs) Handles Go_To_Btn.Click
        Try
            If Not Directory.Exists(Current_Path_CmbBx.Text) Then
                Exit Sub
            End If
            Dim FirstRightTxt = Microsoft.VisualBasic.Right(Current_Path_CmbBx.Text, 1)
            If FirstRightTxt = "\" And ActiveControl.Name = sender.name Then
                Current_Path_CmbBx.Text = Microsoft.VisualBasic.Left(Current_Path_CmbBx.Text, Current_Path_CmbBx.Text.Length - 1)
            End If
            fileExplorer.ExpandToMyPath(Current_Path_CmbBx.Text)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MagNote_Explorer_Form_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim foundNode As TreeNode = fileExplorer.FindNodeByText(Mag_Explorer_Directory_TrVw.Nodes, "c:\")
        If foundNode IsNot Nothing Then
            Mag_Explorer_Directory_TrVw.SelectedNode = foundNode
        End If
        Current_Path_CmbBx.Text = MagNoteFolderPath & "\"
        Go_To_Btn_Click(Go_To_Btn, EventArgs.Empty)
        ApplicationDoEvents(Me)
        Current_Path_CmbBx.Text = MagNoteFolderPath & "\"
    End Sub

    Private Sub Current_Path_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Current_Path_CmbBx.SelectedIndexChanged
        If Current_Path_CmbBx.SelectedIndex = -1 Then Exit Sub
        If Microsoft.VisualBasic.Right(Current_Path_CmbBx.Text, 1) <> "\" Then
            Current_Path_CmbBx.Text = Current_Path_CmbBx.Text & "\"
        End If
        If ActiveControl.Name = sender.name Then
            Go_To_Btn_Click(Go_To_Btn, EventArgs.Empty)
        End If
    End Sub

    Private Sub Mag_Explorer_Directory_Contents_LstVw_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Mag_Explorer_Directory_Contents_LstVw.SelectedIndexChanged
        Try
            MagNote_File_Name_Lbl.Text = Nothing
            If Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Count = 0 Then
                Exit Sub
            End If
            Dim selectedItem As ListViewItem = Mag_Explorer_Directory_Contents_LstVw.SelectedItems(0)
            If MagNote_Form.MagNoteFileFormat(selectedItem.Text, 1, 0) Then
                Dim LblText = selectedItem.SubItems(1).ToString
                MagNote_File_Name_Lbl.Text = Replace(LblText.Replace("ListViewSubItem: {", ""), "}", "")
                MagNote_File_Name_Lbl.Left = (MagNote_Header_Pnl.Width - MagNote_File_Name_Lbl.Width) \ 2
                MagNote_File_Name_Lbl.Top = (MagNote_Header_Pnl.Height - MagNote_File_Name_Lbl.Height) \ 2
                MagNote_File_Name_Lbl.Refresh()
                If ActiveControl.Name = sender.name Then Exit Sub
                For Each slctdItm As ListViewItem In Mag_Explorer_Directory_Contents_LstVw.SelectedItems
                    Mag_Explorer_Directory_Contents_LstVw.Items(slctdItm.Index).BackColor = Color.LightBlue
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function FindListViewItemByColumnNameFromLine(lv As ListView, columnName As String, valueToFind As String, startIndex As Integer) As ListViewItem
        Try
            ' Find column index by column name
            Dim columnIndex As Integer = -1
            For i As Integer = 0 To lv.Columns.Count - 1
                If lv.Columns(i).Text.Equals(columnName, StringComparison.OrdinalIgnoreCase) Then
                    columnIndex = i
                    Exit For
                End If
            Next
            If columnIndex = -1 Then
                MessageBox.Show("Column not found: " & columnName)
                Return Nothing
            End If
            ' Loop through items starting from startIndex
            For i As Integer = startIndex To lv.Items.Count - 1
                Dim item As ListViewItem = lv.Items(i)
                If item.SubItems.Count > columnIndex Then
                    If LCase(item.SubItems(columnIndex).Text).Contains(LCase(valueToFind)) Then
                        Return item
                    End If
                End If
            Next
            Return Nothing ' No match found
        Catch ex As Exception
        End Try
    End Function
    Public Sub ListViewClearSelection()
        For Each item As ListViewItem In Mag_Explorer_Directory_Contents_LstVw.Items
            item.BackColor = Mag_Explorer_Directory_Contents_LstVw.BackColor
        Next
        Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Clear()
    End Sub

    Private Sub File_Name_Label_To_Find_Btn_Click(sender As Object, e As EventArgs) Handles File_Name_Label_To_Find_Btn.Click
        Try
            Dim SelectedLineIndex = 0
            If Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Count <> 0 Then
                Dim selectedItem As ListViewItem = Mag_Explorer_Directory_Contents_LstVw.SelectedItems(0)
                SelectedLineIndex = selectedItem.Index + 1 ' Zero-based index
            End If
            Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Clear()
            Dim FindColumn As String = String.Empty
            If File_Label_To_Find_ChkBx.CheckState = CheckState.Unchecked Then
                FindColumn = "Label"
            Else
                FindColumn = "Name"
            End If
            Dim foundItem = FindListViewItemByColumnNameFromLine(Mag_Explorer_Directory_Contents_LstVw, FindColumn, File_Name_Label_To_Find_TxtBx.Text, SelectedLineIndex)
            If foundItem IsNot Nothing Then
                foundItem.Selected = True
                foundItem.EnsureVisible()
            Else
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "الملف غير موجود او أنتهى البحث"
                Else
                    Msg = "Item not found or search ended."
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, 0,,,,,,,, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub File_Name_Label_To_Find_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles File_Name_Label_To_Find_TxtBx.TextChanged
        ListViewClearSelection()
    End Sub

    Private Sub File_Name_Label_To_Find_TxtBx_GotFocus(sender As Object, e As EventArgs) Handles File_Name_Label_To_Find_TxtBx.GotFocus
        File_Name_Label_To_Find_TxtBx.SelectAll()
    End Sub

    Private Sub Find_All_Btn_Click(sender As Object, e As EventArgs) Handles Find_All_Btn.Click
        Try
            Dim SelectedLineIndex = 0
            If Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Count <> 0 Then
                Dim selectedItem As ListViewItem = Mag_Explorer_Directory_Contents_LstVw.SelectedItems(0)
                SelectedLineIndex = selectedItem.Index + 1
            End If
            Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Clear()
            Dim foundItem As Boolean
            Dim FindColumn As Integer = 0
            If File_Label_To_Find_ChkBx.CheckState = CheckState.Unchecked Then
                FindColumn = 1
            Else
                FindColumn = 0
            End If
            For Each File In Mag_Explorer_Directory_Contents_LstVw.Items
                If LCase(File.SubItems(FindColumn).Text).Contains(LCase(File_Name_Label_To_Find_TxtBx.Text)) Then
                    File.Selected = True
                    File.EnsureVisible()
                    File.BackColor = Color.LightBlue
                    foundItem = True
                End If
            Next
            If Not foundItem Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "الملف غير موجود"
                Else
                    Msg = "Item not found."
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, 0,,,,,,,, Me)
            Else
                Mag_Explorer_Directory_Contents_LstVw.SelectedItems.Clear()
                Mag_Explorer_Directory_Contents_LstVw.Refresh()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub File_Label_To_Find_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles File_Label_To_Find_ChkBx.CheckedChanged

    End Sub

    Private Sub File_Label_To_Find_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles File_Label_To_Find_ChkBx.CheckStateChanged
        If File_Label_To_Find_ChkBx.CheckState = CheckState.Unchecked Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                sender.text = "البحث فى العناوين"
                Form_ToolTip.SetToolTip(sender, "البحث يكون فى عامود عناوين الماجنوت فقط")
            Else
                sender.text = "Find In Lables"
                Form_ToolTip.SetToolTip(sender, "Find Will Be In Labels Column Only")
            End If
        Else
            If MagNote_Form.Language_Btn.Text = "E" Then
                sender.text = "البحث فى الأسماء"
                Form_ToolTip.SetToolTip(sender, "البحث يكون فى عامود أسماء الماجنوت فقط")
            Else
                sender.text = "Find In Names"
                Form_ToolTip.SetToolTip(sender, "Find Will Be In Names Column Only")
            End If
        End If
    End Sub
    Dim MouseDown As Boolean
    Private Sub Separator_Pnl_Paint(sender As Object, e As PaintEventArgs) Handles Separator_Pnl.Paint

    End Sub

    Private Sub Separator_Pnl_MouseDown(sender As Object, e As MouseEventArgs) Handles Separator_Pnl.MouseDown
        MouseDown = True
    End Sub

    Private Sub Separator_Pnl_MouseUp(sender As Object, e As MouseEventArgs) Handles Separator_Pnl.MouseUp
        MouseDown = False
    End Sub

    Private Sub Separator_Pnl_MouseMove(sender As Object, e As MouseEventArgs) Handles Separator_Pnl.MouseMove
        If MouseDown Then
            Dim mPos As Point = sender.PointToClient(Control.MousePosition)
            Dim result As Integer = GetHitTest(mPos)
            Mag_Explorer_Directory_Pnl.Width += result
        End If
    End Sub
    Private borderWidth As Integer = 3
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Function GetHitTest(p As Point) As Integer
        Dim w = Me.ClientSize.Width

        ' Check if RightToLeftLayout is enabled
        Dim isRtl As Boolean = Me.RightToLeftLayout

        ' If RTL, flip the X coordinate to match visual layout
        Dim x = If(isRtl, w - p.X, p.X)
        Return x
        If x <= borderWidth Then
            If p.Y <= borderWidth Then Return HTTOPLEFT
            Return HTLEFT
        ElseIf x >= w - borderWidth Then
            If p.Y <= borderWidth Then Return HTTOPRIGHT
            Return HTRIGHT
        End If

        Return -1
    End Function
End Class