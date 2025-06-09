Imports System.ComponentModel
Imports System.IO
Imports System.Xml
Public Class Labeling_And_Tooltip_Form

    Private Const WM_SYSCOMMAND As Integer = &H112
    Private Const SC_MAXIMIZE As Integer = &HF030
    Const SC_RESTORE As Integer = &HF120
    Const SC_MINIMIZE As Integer = &HF020


    Private Sub Save_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Save_TlStrpBtn.Click
        Try
            Try
                Dim exists1 As Boolean = Available_Forms_CmbBx.Items.Cast(Of Object)().Any(Function(item) String.Equals(item.ToString(), Form_Name_TxtBx.Text, StringComparison.OrdinalIgnoreCase))
                Dim exists2 As Boolean = Form_Objects_CmbBx.Items.Cast(Of Object)().Any(Function(item) String.Equals(item.ToString(), Object_Name_TxtBx.Text, StringComparison.OrdinalIgnoreCase))
                Dim exists3 As Boolean = Shortcuts_CmbBx.Items.Cast(Of Object)().Any(Function(item) String.Equals(item.ToString(), Object_Name_TxtBx.Text, StringComparison.OrdinalIgnoreCase))
                If Not ((exists1 And exists2) Or
                        (exists1 And exists3)) Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "يجب ادخال بيانات صحيحة اولا قبل عملية التحديث"
                    Else
                        Msg = "Correct Data Must Be Entered First Before The Update Proceed"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                    Exit Sub
                End If

                Using XMLEditor As New XMLEditor("Life_Labeling_And_Tooltip.xml", "Labeling_And_Tooltip_Entries",, Me)
                    XMLEditor.Add(Form_Name_TxtBx.Text,
                              New Dictionary(Of String, String) From {
                                  {"Object_Name", Object_Name_TxtBx.Text}},
                              New Dictionary(Of String, String) From {
                                  {"Object_Name", Object_Name_TxtBx.Text},
                                  {"Local_Language_Label", Local_Language_Label_TxtBx.Text},
                                  {"Foreign_Language_Label", Foreign_Language_Label_TxtBx.Text},
                                  {"Local_Language_ToolTip", Local_Language_ToolTip_TxtBx.Text},
                                  {"Foreign_Language_ToolTip", Foreign_Language_ToolTip_TxtBx.Text}})
                End Using
            Catch ex As Exception
                ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
        End Try
    End Sub
    Private Function AddColumsToDGV(ByVal DGV As DataGridView) As Boolean
        If DGV.Columns.Count = 0 Then
            DGV.Rows.Clear()
            DGV.Columns.Add("Form_Name", "Form Name")
            DGV.Columns.Add("Object_Name", "Object Name")
            DGV.Columns.Add("Local_Language_Label", "Local Language Label")
            DGV.Columns.Add("Foreign_Language_Label", "Foreign Language Label")
            DGV.Columns.Add("Local_Language_ToolTip", "Local Language ToolTip")
            DGV.Columns.Add("Foreign_Language_ToolTip", "Foreign Language ToolTip")
        End If
    End Function
    'Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs)
    '    Try
    '        If Object_Name_TxtBx.TextLength = 0 Then Exit Sub
    '        Dim Row As DataGridViewRow
    '        Dim DGV As New DataGridView
    '        DGV = Life_Labeling_And_Tooltip_DGV
    '        Row = isInDataGridView(Object_Name_TxtBx.Text, "Object_Name", DGV,, 1)
    '        If Not IsNothing(Row) Then
    '            If Row.Cells("Form_Name").Value = Form_Name_TxtBx.Text Then
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    Msg = "هذا العنصر مسجل سابقا على قاعدة البيانات هل تريد استبداله؟"
    '                Else
    '                    Msg = "This Object Already Exist In The DataBase... Do You Wnat To Replace It?"
    '                End If
    '                If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,,,,,,,,,, Me) = DialogResult.Yes Then
    '                    DGV.Rows(Row.Index).Cells("Local_Language_Label").Value = Local_Language_Label_TxtBx.Text
    '                    DGV.Rows(Row.Index).Cells("Foreign_Language_Label").Value = Foreign_Language_Label_TxtBx.Text
    '                    DGV.Rows(Row.Index).Cells("Local_Language_ToolTip").Value = Local_Language_ToolTip_TxtBx.Text
    '                    DGV.Rows(Row.Index).Cells("Foreign_Language_ToolTip").Value = Foreign_Language_ToolTip_TxtBx.Text
    '                    If MagNote_Form.Language_Btn.Text = "E" Then
    '                        Msg = "تم استبدال بيانات العنصر بنجاح"
    '                    Else
    '                        Msg = "Object Information Successfully Replaced"
    '                    End If
    '                    ShowMsg(Msg,,,,,,,,,,,,,, Me)
    '                End If
    '                Exit Sub
    '            End If
    '        End If
    '        Dim DRrow As DataRow
    '        DRrow = Life_Labeling_And_Tooltip_DGV.DataSource.NewRow

    '        DRrow("Form_Name") = Form_Name_TxtBx.Text
    '        DRrow("Object_Name") = Object_Name_TxtBx.Text
    '        DRrow("Local_Language_Label") = Local_Language_Label_TxtBx.Text
    '        DRrow("Foreign_Language_Label") = Foreign_Language_Label_TxtBx.Text
    '        DRrow("Local_Language_ToolTip") = Local_Language_ToolTip_TxtBx.Text
    '        DRrow("Foreign_Language_ToolTip") = Foreign_Language_ToolTip_TxtBx.Text
    '        ' Add Values to Row here 
    '        DGV.DataSource.rows.add(DRrow)
    '        If MagNote_Form.Language_Btn.Text = "E" Then
    '            Msg = "تم إضافة بيانات العنصر بنجاح"
    '        Else
    '            Msg = "Object Information Successfully Added"
    '        End If
    '        ShowMsg(Msg,,,,,,,,,,,,,, Me)
    '    Catch ex As Exception
    '        ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
    '    End Try
    'End Sub

    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem.Click
        Try
            If Not MagNote_Form.IsInMagNoteCmbBx(Form_Name_TxtBx.Text, 0, Available_Forms_CmbBx) Or (Not MagNote_Form.IsInMagNoteCmbBx(Object_Name_TxtBx.Text, 0, Form_Objects_CmbBx) And Not MagNote_Form.IsInMagNoteCmbBx(Object_Name_TxtBx.Text, 0, Shortcuts_CmbBx)) Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "يجب ادخال بيانات صحيحة اولا قبل عملية الالغاء"
                Else
                    Msg = "Correct Data Must Be Entered First Before The Delete Proceed"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            Using XMLEditor1 As New XMLEditor("Life_Labeling_And_Tooltip.xml", "Labeling_And_Tooltip_Entries",, Me)
                XMLEditor1.Delete(Form_Name_TxtBx.Text, New Dictionary(Of String, String) From {{"Object_Name", Object_Name_TxtBx.Text}})
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub
    Private originalFormWidth As Integer
    Private originalLabel1Width As Integer
    Private originalLabel2Width As Integer
    Private originalLabel1Left As Integer
    Private originalLabel2Left As Integer
    Private originalLabel1RightOffset As Integer
    Private originalLabel2RightOffset As Integer

    Private resizer As FormResizer
    Private Sub Labeling_And_Tooltip_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadForm(Me, Form_ToolTip, Width, Left, Height, Top)
            resizer = New FormResizer(Me)

            Available_Forms_CmbBx.ValueMember = "Key"
            Available_Forms_CmbBx.DisplayMember = "Value"
            Form_Objects_CmbBx.ValueMember = "Key"
            Form_Objects_CmbBx.DisplayMember = "Value"
            Available_Forms_CmbBx.Items.Add(MagNote_Form.Name)
            Available_Forms_CmbBx.Items.Add(Me.Name)
            Available_Forms_CmbBx.Items.Add(Update_New_Version_Form.Name)
            Available_Forms_CmbBx.Items.Add("User_Password_Form")
            Available_Forms_CmbBx.Items.Add(Find_Form.Name)

            Preview_Life_Labeling_And_Tooltip_Btn.PerformClick()

            originalFormWidth = Me.Width
            originalLabel1Width = Local_Language_ToolTip_Lbl.Width
            originalLabel2Width = Foreign_Language_ToolTip_Lbl.Width
            originalLabel1Left = Local_Language_ToolTip_Lbl.Left
            originalLabel2Left = Foreign_Language_ToolTip_Lbl.Left

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub
    Private Sub MinimizeFormBtn_Click(sender As Object, e As EventArgs) Handles Minimize_Form_Btn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub MaximizeFormBtn_Click(sender As Object, e As EventArgs) Handles Maximize_Form_Btn.Click
        Try
            If originalFormWidth = 0 Then Exit Sub
            ' Calculate how much wider the form has become
            Dim AdjustValue As Integer
            If Me.WindowState = FormWindowState.Normal Then
                AdjustValue = 0
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                AdjustValue = 12
            End If
            Dim scaleFactor As Double = Me.Width / originalFormWidth
            Foreign_Language_ToolTip_Lbl.Width = CInt(originalLabel1Width * scaleFactor) + AdjustValue
            Foreign_Language_ToolTip_Lbl.Left = CInt(originalLabel1Left * scaleFactor) - AdjustValue
            Local_Language_ToolTip_Lbl.Width = CInt(originalLabel2Width * scaleFactor) + AdjustValue
            Local_Language_ToolTip_Lbl.Left = CInt(originalLabel2Left * scaleFactor)

            Foreign_Language_ToolTip_TxtBx.Width = CInt(originalLabel1Width * scaleFactor) + AdjustValue
            Foreign_Language_ToolTip_TxtBx.Left = CInt(originalLabel1Left * scaleFactor) - AdjustValue
            Local_Language_ToolTip_TxtBx.Width = CInt(originalLabel2Width * scaleFactor) + AdjustValue
            Local_Language_ToolTip_TxtBx.Left = CInt(originalLabel2Left * scaleFactor)
            If MagNote_Form.Language_Btn.Text = "E" Then
                Dim ForeignLanguageToolTipLblLeft = Foreign_Language_ToolTip_Lbl.Left
                Dim LocalLanguageToolTipLblLeft = Local_Language_ToolTip_Lbl.Left
                Dim ForeignLanguageToolTipTxtBxLeft = Foreign_Language_ToolTip_TxtBx.Left
                Dim LocalLanguageToolTipTxtBxLeft = Local_Language_ToolTip_TxtBx.Left
                Foreign_Language_ToolTip_Lbl.Left = LocalLanguageToolTipLblLeft
                Local_Language_ToolTip_Lbl.Left = ForeignLanguageToolTipLblLeft
                Foreign_Language_ToolTip_TxtBx.Left = LocalLanguageToolTipTxtBxLeft
                Local_Language_ToolTip_TxtBx.Left = ForeignLanguageToolTipTxtBxLeft
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ExitFormBtn_Click(sender As Object, e As EventArgs) Handles Exit_Form_Btn.Click
        Me.Close()
    End Sub
    Private Sub Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        MagNote_Form.PaintFindForm(Me)
    End Sub

    Private Sub ToolTip_Draw(sender As Object, e As DrawToolTipEventArgs)
        Dim Objct As Object = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl
        Dim strFormat As New StringFormat
        strFormat.LineAlignment = StringAlignment.Center
        strFormat.Alignment = StringAlignment.Center
        e.Graphics.DrawString("This is when OwnerDrawnBackground of the ToolTip is set to True", Objct.findform.Font, Brushes.Black, Objct.Rectangle, strFormat)
    End Sub

    Private Sub Life_Labeling_And_Tooltip_DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Life_Labeling_And_Tooltip_DGV.CellContentClick

    End Sub

    Private Sub Life_Labeling_And_Tooltip_DGV_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles Life_Labeling_And_Tooltip_DGV.CellValidating
        Select Case Life_Labeling_And_Tooltip_DGV.CurrentCell.ColumnIndex
            Case 1
                Life_Labeling_And_Tooltip_DGV.Rows(Life_Labeling_And_Tooltip_DGV.CurrentCell.RowIndex).Cells("Foreign_Language_Label").Value = Replace(Life_Labeling_And_Tooltip_DGV.CurrentCell.Value, "_", " ")
        End Select
    End Sub

    Private Sub Life_Labeling_And_Tooltip_DGV_SelectionChanged(sender As Object, e As EventArgs) Handles Life_Labeling_And_Tooltip_DGV.SelectionChanged
        If CurrentRowNotEqualRowIndex(sender, 1) Then Exit Sub
        If sender.SelectedRows.Count = 0 Or
            IsNothing(ActiveControl) Then Exit Sub
        Try
            Dim index = dt.Rows.IndexOf(dt.AsEnumerable().
FirstOrDefault(Function(row) row.Field(Of String)("Object_Name") = sender.CurrentRow.Cells("Object_Name").Value AndAlso row.Field(Of String)("Form_Name") = sender.CurrentRow.Cells("Form_Name").Value))
            If index >= 0 Then
                BindingSource.Position = index
            Else
                Form_Name_TxtBx.Text = sender.CurrentRow.Cells("Form_Name").Value.ToString
                If Not String.IsNullOrEmpty(sender.CurrentRow.Cells("Object_Name").value) Then
                    Object_Name_TxtBx.Text = sender.CurrentRow.Cells("Object_Name").Value.ToString
                End If
                Local_Language_Label_TxtBx.Text = sender.CurrentRow.Cells("Local_Language_Label").Value.ToString
                Foreign_Language_Label_TxtBx.Text = sender.CurrentRow.Cells("Foreign_Language_Label").Value.ToString
                Local_Language_ToolTip_TxtBx.Text = sender.CurrentRow.Cells("Local_Language_ToolTip").Value.ToString
                Foreign_Language_ToolTip_TxtBx.Text = sender.CurrentRow.Cells("Foreign_Language_ToolTip").Value.ToString
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Restore_Stored_Labels_And_ToolTips_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Click
        Try
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "هل حقا تريد استبدال الملف الحالى بالملف الاصلى المحفوظ كنسخة احتياطية لعناوين وشرح العناصر؟"
            Else
                Msg = "Do You Really Want To Replace The Current File With The Original File Saved As A Backup Copy Of The Control Labels And Explanations??"
            End If
            If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,,,,,,,,,, Me) = DialogResult.No Then Exit Sub

            My.Computer.FileSystem.DeleteFile(MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)

            If File.Exists(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml") Then
                File.Copy(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml", MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml", 1)
            End If
            ShowMsg("File Replaced Successfully (Life File By Stored File",,,,,,,,,,,,,, Me)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Preview_Life_Labeling_And_Tooltip_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Life_Labeling_And_Tooltip_Btn.Click
        Try

            Life_Labeling_And_Tooltip_DGV.Rows.Clear()
            Using XMLEditor As New XMLEditor("Life_Labeling_And_Tooltip.xml", "Labeling_And_Tooltip_Entries",, Me)
                If IsNothing(XMLEditor.doc) Then Exit Sub
                Dim docRoot = XMLEditor.Element_Exist("Labeling_And_Tooltip_Entries", Nothing,, 1)

                If Life_Labeling_And_Tooltip_DGV.Columns.Count = 0 Then
                    Life_Labeling_And_Tooltip_DGV.Columns.Add("Form_Name", "Form Name")
                    Life_Labeling_And_Tooltip_DGV.Columns.Add("Object_Name", "Object Name")
                    Life_Labeling_And_Tooltip_DGV.Columns.Add("Local_Language_Label", "Local Language Label")
                    Life_Labeling_And_Tooltip_DGV.Columns.Add("Foreign_Language_Label", "Foreign Language Label")
                    Life_Labeling_And_Tooltip_DGV.Columns.Add("Local_Language_ToolTip", "Local Language ToolTip")
                    Life_Labeling_And_Tooltip_DGV.Columns.Add("Foreign_Language_ToolTip", "Foreign Language ToolTip")
                End If
                For Each entry In docRoot
                    Life_Labeling_And_Tooltip_DGV.Rows.Add(
                                                entry.Name,
                                                entry.Element("Object_Name")?.Value,
                                                entry.Element("Local_Language_Label")?.Value,
                                                entry.Element("Foreign_Language_Label")?.Value,
                                                entry.Element("Local_Language_ToolTip")?.Value,
                                                entry.Element("Foreign_Language_ToolTip")?.Value)
                Next
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Available_Forms_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Available_Forms_CmbBx.SelectedIndexChanged
        Form_Objects_CmbBx.SelectedIndex = -1
        Form_Objects_CmbBx.Text = Nothing
        If Available_Forms_CmbBx.SelectedIndex = -1 Then Exit Sub
        Form_Objects_CmbBx.Items.Clear()
        Dim frm As New Form
        Select Case Available_Forms_CmbBx.SelectedItem
            Case MagNote_Form.Name
                LoadFormObjects(MagNote_Form)
            Case Me.Name
                LoadFormObjects(Me)
            Case Update_New_Version_Form.Name
                LoadFormObjects(Update_New_Version_Form)
            Case "User_Password_Form"
                Form_Objects_CmbBx.Items.Add("FindText_Lbl")
                Form_Objects_CmbBx.Items.Add("User_Password_TxtBx")
            Case "Find_Form"
                LoadFormObjects(Find_Form)
        End Select
        Form_Name_TxtBx.Text = Available_Forms_CmbBx.SelectedItem
    End Sub
    Private Function LoadFormObjects(ByVal Form As Form) As Boolean
        Cursor = Cursors.WaitCursor
        Dim ControlsCount() As String
        'Dim CtrlList = FindControlRecursive(New List(Of Control), Form, New List(Of Type)({GetType(Label), GetType(CheckBox), GetType(TabPage), GetType(Button), GetType(ToolStrip), GetType(DataGridView)}))
        Dim CtrlList = FindControlRecursive(New List(Of Control), Form, New List(Of Type))
        ReDim ControlsCount(CtrlList.Count)
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, ControlsCount.Count, Me)
            For Each Cntrl In (CtrlList)
                progress += ProgressToAdd
                Previewlbl.Text = "Loading--> " & Cntrl.Name & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                If Form_Objects_CmbBx.FindStringExact(Cntrl.Name) <> -1 Then
                    Continue For
                End If
                Form_Objects_CmbBx.Items.Add(Cntrl.Name)
                If Cntrl.GetType = GetType(ToolStrip) Then
                    For Each Item In CType(Cntrl, ToolStrip).Items
                        If String.IsNullOrEmpty(Item.Name.ToString) Then
                            Continue For
                        End If
                        If Form_Objects_CmbBx.FindStringExact(Item.Name) <> -1 Then
                            Continue For
                        End If
                        Form_Objects_CmbBx.Items.Add(Item.Name)
                    Next
                End If
            Next
            If Form.Name = MagNote_Form.Name Then
                Shortcuts_CmbBx.Items.Clear()
                For Each cntrl In (FindControlRecursive(New List(Of Control), MagNote_Form.Setting_TbCntrl.TabPages("Shortcuts_TbPg"), New List(Of Type)({GetType(ListView)})))
                    Shortcuts_CmbBx.Items.Add(cntrl.Name)
                    For Each Shortcut In CType(cntrl, ListView).Items
                        isInDataGridView(Shortcut.text, "Object_Name", Life_Labeling_And_Tooltip_DGV, 0, 0, 1)
                        Shortcuts_CmbBx.Items.Add(Shortcut.text)
                    Next

                Next
            End If
        Finally
            Cursor = Cursors.Default
            Previewlbl.Dispose()
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
            Refresh()
        End Try

    End Function

    Private Sub Form_Objects_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Form_Objects_CmbBx.SelectedIndexChanged
        If Form_Objects_CmbBx.SelectedIndex = -1 Then Exit Sub
        Object_Name_TxtBx.Text = Form_Objects_CmbBx.SelectedItem
        Local_Language_Label_TxtBx.Text = Nothing
        Foreign_Language_Label_TxtBx.Text = Nothing
        Local_Language_ToolTip_TxtBx.Text = Nothing
        Foreign_Language_ToolTip_TxtBx.Text = Nothing
        If Available_Forms_CmbBx.SelectedIndex <> -1 Then
            If Not IsNothing(isInDataGridView(Available_Forms_CmbBx.SelectedItem, "Form_Name", Life_Labeling_And_Tooltip_DGV, 0, 1)) Then
                Dim searchString = Available_Forms_CmbBx.SelectedItem & "," & Form_Objects_CmbBx.SelectedItem
                Dim columnToSearch = "Form_Name,Object_Name"
                Dim columns = columnToSearch.Split(","c)
                Dim values = searchString.Split(","c)
                Dim DGVRow = Life_Labeling_And_Tooltip_DGV.Rows.Cast(Of DataGridViewRow)() _
    .FirstOrDefault(Function(r)
                        For i As Integer = 0 To Math.Min(columns.Length, values.Length) - 1
                            Dim colName = columns(i)
                            Dim val = values(i).ToLower()
                            Dim cellVal = r.Cells(colName).Value?.ToString().ToLower()
                            If cellVal <> val Then
                                Return False
                            End If
                        Next
                        Return True
                    End Function)
                If DGVRow IsNot Nothing Then
                    Life_Labeling_And_Tooltip_DGV.Rows(DGVRow.Index).Selected = True
                End If
            End If
        End If
    End Sub

    Private Sub Labeling_And_Tooltip_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub Form_Name_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Form_Name_TxtBx.TextChanged

    End Sub

    Private Sub Form_Name_TxtBx_GotFocus(sender As Object, e As EventArgs) Handles Form_Name_TxtBx.GotFocus, Object_Name_TxtBx.GotFocus, Local_Language_Label_TxtBx.GotFocus, Foreign_Language_Label_TxtBx.GotFocus, Local_Language_ToolTip_TxtBx.GotFocus, Foreign_Language_ToolTip_TxtBx.GotFocus
        Form_Name_TxtBx.SelectAll()
        Select Case sender.name
            Case Form_Name_TxtBx.Name, Object_Name_TxtBx.Name, Foreign_Language_Label_TxtBx.Name, Foreign_Language_ToolTip_TxtBx.Name
                ChangeControlLanguage(sender, 0)
            Case Local_Language_Label_TxtBx.Name, Local_Language_ToolTip_TxtBx.Name
                ChangeControlLanguage(sender, 1)
        End Select
    End Sub

    Private Sub Shortcuts_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Shortcuts_CmbBx.SelectedIndexChanged
        If Shortcuts_CmbBx.SelectedIndex = -1 Then Exit Sub
        Object_Name_TxtBx.Text = Shortcuts_CmbBx.SelectedItem
        Local_Language_Label_TxtBx.Text = Nothing
        Foreign_Language_Label_TxtBx.Text = Nothing
        Local_Language_ToolTip_TxtBx.Text = Nothing
        Foreign_Language_ToolTip_TxtBx.Text = Nothing
        If Available_Forms_CmbBx.SelectedIndex <> -1 Then
            If Not IsNothing(isInDataGridView(Available_Forms_CmbBx.SelectedItem, "Form_Name", Life_Labeling_And_Tooltip_DGV, 0, 1)) Then
                isInDataGridView(Shortcuts_CmbBx.SelectedItem, "Object_Name", Life_Labeling_And_Tooltip_DGV, 0, 0, 1)
            End If
        End If
    End Sub

    Private Sub Reload_Shortcuts_Tooltip_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Reload_Tooltips_TlStrpBtn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            MagNote_Form.LoadList(Nothing, Me)
            Labeling_Form(Me, Nothing, Form_ToolTip)
            Labeling_Form(MagNote_Form, Nothing, Form_ToolTip)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Use_Stored_Data_Tab_ChkBx_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Save_As_Stored_Copy_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Save_As_Stored_Copy_TlStrpBtn.Click
        Try
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "هل حقا تريد استبدال الملف الاصلى المحفوظ كنسخة احتياطية بالملف الحالى لعناوين وشرح العناصر؟"
            Else
                Msg = "Do You Really Want To Replace The Original File Saved As A Backup Copy Of The Control Labels And Explanations With The Current File??"
            End If
            If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,,,,,,,,,, Me) = DialogResult.No Then Exit Sub

            My.Computer.FileSystem.DeleteFile(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)

            If File.Exists(MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml") Then
                File.Copy(MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml", MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml", 1)
            End If
            ShowMsg("File Replaced Successfully (Stored File By Life File",,,,,,,,,,,,,, Me)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub
    Dim dt As New DataTable()

    Private Sub ReLoad_XML_File_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles ReLoad_XML_File_TlStrpBtn.Click
        Try
            Try
                Form_Name_TxtBx.DataBindings.Clear()
                Object_Name_TxtBx.DataBindings.Clear()
                Local_Language_Label_TxtBx.DataBindings.Clear()
                Foreign_Language_Label_TxtBx.DataBindings.Clear()
                Local_Language_ToolTip_TxtBx.DataBindings.Clear()
                Foreign_Language_ToolTip_TxtBx.DataBindings.Clear()
            Catch ex As Exception
            End Try
            Using XMLEditor As New XMLEditor("Life_Labeling_And_Tooltip.xml", "Labeling_And_Tooltip_Entries",, Me)

                ' Create a unified DataTable
                dt = New DataTable()
                dt.Columns.Add("Form_Name")
                dt.Columns.Add("Object_Name")
                dt.Columns.Add("Local_Language_Label")
                dt.Columns.Add("Foreign_Language_Label")
                dt.Columns.Add("Local_Language_ToolTip")
                dt.Columns.Add("Foreign_Language_ToolTip")

                ' Extract both types of forms
                Dim allForms
                If Available_Forms_CmbBx.SelectedIndex <> -1 Then
                    allForms = XMLEditor.doc.Descendants().Where(Function(x) (x.Name.LocalName = "MagNote_Form" OrElse x.Name.LocalName = "Labeling_And_Tooltip_Form") AndAlso
                        x.Name.ToString = Available_Forms_CmbBx.SelectedItem)
                Else
                    allForms = XMLEditor.doc.Descendants().Where(Function(x) x.Name.LocalName = "MagNote_Form" OrElse x.Name.LocalName = "Labeling_And_Tooltip_Form")
                End If
                For Each form In allForms
                    dt.Rows.Add(
                    form.Name,
                    form.Element("Object_Name")?.Value,
                    form.Element("Local_Language_Label")?.Value,
                    form.Element("Foreign_Language_Label")?.Value,
                    form.Element("Local_Language_ToolTip")?.Value,
                    form.Element("Foreign_Language_ToolTip")?.Value
                )
                Next

                ' Bind to BindingSource and Navigator
                BindingSource.DataSource = dt
                BindingNavigator1.BindingSource = BindingSource

                ' Bind fields to textboxes
                Form_Name_TxtBx.DataBindings.Add("Text", BindingSource, "Form_Name")
                Object_Name_TxtBx.DataBindings.Add("Text", BindingSource, "Object_Name")
                Local_Language_Label_TxtBx.DataBindings.Add("Text", BindingSource, "Local_Language_Label")
                Foreign_Language_Label_TxtBx.DataBindings.Add("Text", BindingSource, "Foreign_Language_Label")
                Local_Language_ToolTip_TxtBx.DataBindings.Add("Text", BindingSource, "Local_Language_ToolTip")
                Foreign_Language_ToolTip_TxtBx.DataBindings.Add("Text", BindingSource, "Foreign_Language_ToolTip")
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Object_Name_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Object_Name_TxtBx.TextChanged

    End Sub

    Private Sub Foreign_Language_ToolTip_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Foreign_Language_ToolTip_TxtBx.TextChanged

    End Sub


    Private Sub Foreign_Language_ToolTip_TxtBx_Leave(sender As Object, e As EventArgs) Handles Foreign_Language_ToolTip_TxtBx.Leave
        Try
            'sender.text = CapitalFirstLetter(sender.text)
            'If sender.Text.Length > 0 Then
            '    Dim NewText = String.Empty
            '    For Each Firsteter In sender.Text.split(" ")
            '        If String.IsNullOrEmpty(Firsteter) Then Continue For
            '        NewText &= Char.ToUpper(Firsteter(0)) & Microsoft.VisualBasic.Right(Firsteter, Firsteter.Length - 1) & " "
            '    Next
            '    sender.Text = Microsoft.VisualBasic.Left(NewText, NewText.Length - 1)
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Available_Forms_CmbBx_TextChanged(sender As Object, e As EventArgs) Handles Available_Forms_CmbBx.TextChanged
        Try
            If IsNothing(ActiveControl) Then Exit Sub
            If ActiveControl.Name = sender.name Then
                If Available_Forms_CmbBx.SelectedIndex = -1 Then
                    Form_Objects_CmbBx.SelectedIndex = -1
                    Form_Objects_CmbBx.Text = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Local_Language_ToolTip_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Local_Language_ToolTip_TxtBx.TextChanged

    End Sub

    Private Sub Form_Objects_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles Form_Objects_CmbBx.Validating
    End Sub

    Private Sub Available_Forms_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles Available_Forms_CmbBx.Validating, Form_Objects_CmbBx.Validating, Shortcuts_CmbBx.Validating
        ValidateIfExist(sender, sender.text)
    End Sub
    Private Function ValidateIfExist(sender As Object, Text As Object) As Boolean
        Dim exists1 As Boolean = CType(sender, ComboBox).Items.Cast(Of Object)().Any(Function(item) String.Equals(item.ToString(), Text, StringComparison.OrdinalIgnoreCase))
        If Not exists1 Or
            sender.text.length = 0 Then
            sender.selectedindex = -1
        End If
    End Function

    Private Sub Labeling_And_Tooltip_Form_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    End Sub
End Class