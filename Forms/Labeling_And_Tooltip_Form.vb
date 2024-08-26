Imports System.ComponentModel
Imports System.IO
Imports System.Xml
Public Class Labeling_And_Tooltip_Form

    Private Sub Save_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Save_TlStrpBtn.Click
        Try
            Dim Row As DataGridViewRow
            Dim DGV As New DataGridView
            If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
                DGV = Stored_Labeling_And_Tooltip_DGV
            Else
                DGV = Life_Labeling_And_Tooltip_DGV
            End If
            Row = isInDataGridView(Object_Name_TxtBx.Text, "Object_Name", DGV,, 1)
            If IsNothing(Row) Then
                BindingNavigatorAddNewItem.PerformClick()
            End If

            Dim xws As New XmlWriterSettings '
            xws = New XmlWriterSettings()
            xws.Indent = True
            xws.NewLineOnAttributes = True
            Dim Labeling_And_Tooltip_xml_File
            Dim Labeling_And_Tooltip_DGV As New DataGridView
            If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
                Labeling_And_Tooltip_xml_File = MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml"
                Labeling_And_Tooltip_DGV = Stored_Labeling_And_Tooltip_DGV
            Else
                Labeling_And_Tooltip_xml_File = MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml"
                Labeling_And_Tooltip_DGV = Life_Labeling_And_Tooltip_DGV
            End If
            Dim x = 0
            Using xw As XmlWriter = XmlWriter.Create(Labeling_And_Tooltip_xml_File, xws)
                xw.WriteStartDocument()
                xw.WriteStartElement("Life_Labeling_And_Tooltip")
                For Each Row In Labeling_And_Tooltip_DGV.Rows
                    xw.WriteStartElement("Object")
                    xw.WriteAttributeString("Form_Name", Row.Cells("Form_Name").Value)
                    xw.WriteAttributeString("Object_Name", Row.Cells("Object_Name").Value)
                    xw.WriteAttributeString("Local_Language_Label", Row.Cells("Local_Language_Label").Value)
                    xw.WriteAttributeString("Foreign_Language_Label", Row.Cells("Foreign_Language_Label").Value)
                    xw.WriteAttributeString("Local_Language_ToolTip", Row.Cells("Local_Language_ToolTip").Value)
                    xw.WriteAttributeString("Foreign_Language_ToolTip", Row.Cells("Foreign_Language_ToolTip").Value)
                    xw.WriteEndElement()
                Next
                xw.WriteEndElement()
                xw.WriteEndDocument()
                xw.Flush()
                xw.Close()
            End Using

            ShowMsg("File Saved Successfully")
            If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "هل حقا تريد استبدال الملف الحالى بالملف الاصلى المخزن لعناوين وشرح العناصر؟"
                Else
                    Msg = " Do You Realy Want To Replace The Current File By Stored Labeling And Tooltip File?"
                End If
                If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
                If File.Exists(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml") Then
                    File.Copy(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml", MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml", 1)
                End If
                ShowMsg("File Replaced Successfully (Life File By Stored File)")
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
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
    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        Try
            If Object_Name_TxtBx.TextLength = 0 Then Exit Sub
            Dim Row As DataGridViewRow
            Dim DGV As New DataGridView
            If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
                DGV = Stored_Labeling_And_Tooltip_DGV
            Else
                DGV = Life_Labeling_And_Tooltip_DGV
            End If

            Row = isInDataGridView(Object_Name_TxtBx.Text, "Object_Name", DGV,, 1)
            If Not IsNothing(Row) Then
                If Row.Cells("Form_Name").Value = Form_Name_TxtBx.Text Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "هذا العنصر مسجل سابقا على قاعدة البيانات هل تريد استبداله؟"
                    Else
                        Msg = "This Object Already Exist In The DataBase... Do You Wnat To Replace It?"
                    End If
                    If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        DGV.Rows(Row.Index).Cells("Local_Language_Label").Value = Local_Language_Label_TxtBx.Text
                        DGV.Rows(Row.Index).Cells("Foreign_Language_Label").Value = Foreign_Language_Label_TxtBx.Text
                        DGV.Rows(Row.Index).Cells("Local_Language_ToolTip").Value = Local_Language_ToolTip_TxtBx.Text
                        DGV.Rows(Row.Index).Cells("Foreign_Language_ToolTip").Value = Foreign_Language_ToolTip_TxtBx.Text
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Msg = "تم استبدال بيانات العنصر بنجاح"
                        Else
                            Msg = "Object Information Successfully Replaced"
                        End If
                        ShowMsg(Msg)
                    End If
                    Exit Sub
                End If
            End If
            Dim DRrow As DataRow
            If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
                DRrow = Stored_Labeling_And_Tooltip_DGV.DataSource.NewRow
            Else
                DRrow = Life_Labeling_And_Tooltip_DGV.DataSource.NewRow
            End If

            DRrow("Form_Name") = Form_Name_TxtBx.Text
            DRrow("Object_Name") = Object_Name_TxtBx.Text
            DRrow("Local_Language_Label") = Local_Language_Label_TxtBx.Text
            DRrow("Foreign_Language_Label") = Foreign_Language_Label_TxtBx.Text
            DRrow("Local_Language_ToolTip") = Local_Language_ToolTip_TxtBx.Text
            DRrow("Foreign_Language_ToolTip") = Foreign_Language_ToolTip_TxtBx.Text
            ' Add Values to Row here 
            DGV.DataSource.rows.add(DRrow)

            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "تم إضافة بيانات العنصر بنجاح"
            Else
                Msg = "Object Information Successfully Added"
            End If
            ShowMsg(Msg)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem.Click
        Dim DGV As New DataGridView
        If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
            DGV = Stored_Labeling_And_Tooltip_DGV
        Else
            DGV = Life_Labeling_And_Tooltip_DGV
        End If
        For Each row In DGV.SelectedRows
            DGV.Rows.Remove(row)
        Next
    End Sub

    Private Sub Labeling_And_Tooltip_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If MagNote_Form.Language_Btn.Text = "E" Then
                Labeling_Form(Me, "Arabic")
            Else
                Labeling_Form(Me, "English")
            End If
            Available_Forms_CmbBx.ValueMember = "Key"
            Available_Forms_CmbBx.DisplayMember = "Value"
            Form_Objects_CmbBx.ValueMember = "Key"
            Form_Objects_CmbBx.DisplayMember = "Value"
            Available_Forms_CmbBx.Items.Add(MagNote_Form.Name)
            Available_Forms_CmbBx.Items.Add(Me.Name)
            Available_Forms_CmbBx.Items.Add(Update_New_Version_Form.Name)
            Available_Forms_CmbBx.Items.Add("User_Password_Form")
            Available_Forms_CmbBx.Items.Add("Find_Form")

            Preview_Life_Labeling_And_Tooltip_Btn.PerformClick()
            Preview_Stored_Labeling_And_Tooltip_Btn.PerformClick()
            'AddColumsToDGV(Life_Labeling_And_Tooltip_DGV)
            'AddColumsToDGV(Stored_Labeling_And_Tooltip_DGV)
            AddHandler Stored_Labeling_And_Tooltip_DGV.SelectionChanged, AddressOf Life_Labeling_And_Tooltip_DGV_SelectionChanged
            Me.BackColor = MagNote_Form.Note_Back_Color_ClrCmbBx.SelectedItem 'BackColor
            Me.ForeColor = MagNote_Form.Note_Font_Color_ClrCmbBx.SelectedItem ' ForeColor
            Me.Opacity = MagNote_Form.Form_Transparency_TrkBr.Value / 100

        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
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
        If sender.SelectedRows.Count = 0 Then Exit Sub
        Form_Name_TxtBx.Text = sender.CurrentRow.Cells("Form_Name").Value
        Object_Name_TxtBx.Text = sender.CurrentRow.Cells("Object_Name").Value
        Local_Language_Label_TxtBx.Text = sender.CurrentRow.Cells("Local_Language_Label").Value
        Foreign_Language_Label_TxtBx.Text = sender.CurrentRow.Cells("Foreign_Language_Label").Value
        Local_Language_ToolTip_TxtBx.Text = sender.CurrentRow.Cells("Local_Language_ToolTip").Value
        Foreign_Language_ToolTip_TxtBx.Text = sender.CurrentRow.Cells("Foreign_Language_ToolTip").Value
    End Sub

    Private Sub Restore_Stored_Labels_And_ToolTips_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Click
        Try
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "هل حقا تريد استبدال الملف الحالى بالملف الاصلى لعناوين وشرح العناصر؟"
            Else
                Msg = " Do You Realy Want To Replace The Current File By Stored Labeling And Tooltip File?"
            End If
            If ShowMsg(Msg,, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
            If File.Exists(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml") Then
                File.Copy(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml", MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml", 1)
            End If
            ShowMsg("File Replaced Successfully (Life File By Stored File")
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    Private Sub Preview_Life_Labeling_And_Tooltip_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Life_Labeling_And_Tooltip_Btn.Click
        Life_Labeling_And_Tooltip_DGV.DataSource = Nothing
        Dim xmlFile As XmlReader
        Dim ds As DataSet
        If File.Exists(MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml") Then
            ds = New DataSet
            xmlFile = XmlReader.Create(MagNoteFolderPath & "\Life_Labeling_And_Tooltip.xml", New XmlReaderSettings())
            ds.ReadXml(xmlFile)
            Life_Labeling_And_Tooltip_DGV.DataSource = ds.Tables(0)
        End If
    End Sub

    Private Sub Preview_Stored_Labeling_And_Tooltip_Btn_Click(sender As Object, e As EventArgs) Handles Preview_Stored_Labeling_And_Tooltip_Btn.Click
        Stored_Labeling_And_Tooltip_DGV.DataSource = Nothing
        Dim xmlFile As XmlReader
        Dim ds As DataSet
        If File.Exists(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml") Then
            ds = New DataSet
            xmlFile = XmlReader.Create(MagNoteFolderPath & "\Stored_Labeling_And_Tooltip.xml", New XmlReaderSettings())
            ds.ReadXml(xmlFile)
            Stored_Labeling_And_Tooltip_DGV.DataSource = ds.Tables(0)
        End If
    End Sub

    Private Sub Available_Forms_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Available_Forms_CmbBx.SelectedIndexChanged
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
                Form_Objects_CmbBx.Items.Add("FindIn_Lbl")
                Form_Objects_CmbBx.Items.Add("FindText_Lbl")
                Form_Objects_CmbBx.Items.Add("ReplaceBy_Lbl")
                Form_Objects_CmbBx.Items.Add("FindText_CmbBx")
                Form_Objects_CmbBx.Items.Add("ReplaceBy_TxtBx")
                Form_Objects_CmbBx.Items.Add("FindNext_Btn")
                Form_Objects_CmbBx.Items.Add("FindAll_Btn")
                Form_Objects_CmbBx.Items.Add("ReplaceNext_Btn")
                Form_Objects_CmbBx.Items.Add("ReplaceAll_Btn")
                Form_Objects_CmbBx.Items.Add("ClearSearchResult_Btn")
                Form_Objects_CmbBx.Items.Add("Exit_Btn")
        End Select
        Form_Name_TxtBx.Text = Available_Forms_CmbBx.SelectedItem
    End Sub
    Private Function LoadFormObjects(ByVal Form As Form) As Boolean
        Cursor = Cursors.WaitCursor
        Dim ControlsCount() As String
        ReDim ControlsCount(FindControlRecursive(New List(Of Control), Form, New List(Of Type)({GetType(Label), GetType(CheckBox), GetType(TabPage), GetType(Button), GetType(ToolStrip)})).ToList.Count)
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, ControlsCount.Count, Me)
            For Each Cntrl In (FindControlRecursive(New List(Of Control), Form, New List(Of Type)({GetType(Label), GetType(CheckBox), GetType(TabPage), GetType(Button), GetType(ToolStrip)})))
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
        If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
            If Available_Forms_CmbBx.SelectedIndex <> -1 Then
                If Not IsNothing(isInDataGridView(Available_Forms_CmbBx.SelectedItem, "Form_Name", Stored_Labeling_And_Tooltip_DGV, 0, 1)) Then
                    isInDataGridView(Form_Objects_CmbBx.SelectedItem, "Object_Name", Stored_Labeling_And_Tooltip_DGV, 0, 0, 1)
                End If
            End If
        Else
            If Available_Forms_CmbBx.SelectedIndex <> -1 Then
                If Not IsNothing(isInDataGridView(Available_Forms_CmbBx.SelectedItem, "Form_Name", Life_Labeling_And_Tooltip_DGV, 0, 1)) Then
                    isInDataGridView(Form_Objects_CmbBx.SelectedItem, "Object_Name", Life_Labeling_And_Tooltip_DGV, 0, 0, 1)
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
    End Sub

    Private Sub Shortcuts_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Shortcuts_CmbBx.SelectedIndexChanged
        If Shortcuts_CmbBx.SelectedIndex = -1 Then Exit Sub
        Object_Name_TxtBx.Text = Shortcuts_CmbBx.SelectedItem
        Local_Language_Label_TxtBx.Text = Nothing
        Foreign_Language_Label_TxtBx.Text = Nothing
        Local_Language_ToolTip_TxtBx.Text = Nothing
        Foreign_Language_ToolTip_TxtBx.Text = Nothing
        If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
            If Available_Forms_CmbBx.SelectedIndex <> -1 Then
                If Not IsNothing(isInDataGridView(Available_Forms_CmbBx.SelectedItem, "Form_Name", Stored_Labeling_And_Tooltip_DGV, 0, 1)) Then
                    isInDataGridView(Shortcuts_CmbBx.SelectedItem, "Object_Name", Stored_Labeling_And_Tooltip_DGV, 0, 0, 1)
                End If
            End If
        Else
            If Available_Forms_CmbBx.SelectedIndex <> -1 Then
                If Not IsNothing(isInDataGridView(Available_Forms_CmbBx.SelectedItem, "Form_Name", Life_Labeling_And_Tooltip_DGV, 0, 1)) Then
                    isInDataGridView(Shortcuts_CmbBx.SelectedItem, "Object_Name", Life_Labeling_And_Tooltip_DGV, 0, 0, 1)
                End If
            End If
        End If
    End Sub

    Private Sub Reload_Shortcuts_Tooltip_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Reload_Shortcuts_Tooltip_TlStrpBtn.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            MagNote_Form.LoadList(Nothing, Me)
            'Load_Shortcuts_ToolTips(MagNote_Form.Shortcuts_LstVw)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Use_Stored_Data_Tab_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Use_Stored_Data_Tab_ChkBx.CheckedChanged

    End Sub

    Private Sub Use_Stored_Data_Tab_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Use_Stored_Data_Tab_ChkBx.CheckStateChanged
        If Use_Stored_Data_Tab_ChkBx.CheckState = CheckState.Checked Then
            Stored_Labeling_And_Tooltip_Lbl.Enabled = True
            Preview_Stored_Labeling_And_Tooltip_Btn.Enabled = True
            Stored_Labeling_And_Tooltip_DGV.Enabled = True
            Life_Labeling_And_Tooltip_Lbl.Enabled = False
            Preview_Life_Labeling_And_Tooltip_Btn.Enabled = False
            Life_Labeling_And_Tooltip_DGV.Enabled = False
            Preview_Stored_Labeling_And_Tooltip_Btn.PerformClick()
        Else
            Stored_Labeling_And_Tooltip_Lbl.Enabled = False
            Preview_Stored_Labeling_And_Tooltip_Btn.Enabled = False
            Stored_Labeling_And_Tooltip_DGV.Enabled = False
            Life_Labeling_And_Tooltip_Lbl.Enabled = True
            Preview_Life_Labeling_And_Tooltip_Btn.Enabled = True
            Life_Labeling_And_Tooltip_DGV.Enabled = True
            Preview_Life_Labeling_And_Tooltip_Btn.PerformClick()
        End If
    End Sub
End Class