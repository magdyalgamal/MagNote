Imports System.ComponentModel
Imports System.IO

Public Class Find_Form
    Structure FindArrayFields
        ''' <summary>
        ''' BackColor As Color
        ''' ForeColor As Color
        ''' SelectionStart As Integer
        ''' SlectionLength As Integer
        ''' SelectedText As String
        ''' </summary>
        Dim BackColor As Color
        Dim ForeColor As Color
        Dim SelectionStart As Integer
        Dim SlectionLength As Integer
        Dim SelectedText As String
    End Structure
    Private resizer As FormResizer
    Private Sub Find_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadForm(Me, Form_ToolTip, Width, Left, Height, Top)

            resizer = New FormResizer(Me)
            FindText_CmbBx.Items.Clear()
            If File.Exists(SearchWordsRepository) Then
                For Each Word In My.Computer.FileSystem.ReadAllText(SearchWordsRepository, System.Text.Encoding.UTF8).Split(delimiters, StringSplitOptions.None)
                    If String.IsNullOrEmpty(Word) Then Continue For
                    FindText_CmbBx.Items.Add(Replace(Replace(Word, vbCr, ""), vbCrLf, ""))
                Next
            End If
            Selection_Start_TxtBx.Text = RCSN(0).SelectionStart
            Selection_End_TxtBx.Text = RCSN(0).SelectionStart + RCSN(0).SelectionLength
            Selection_Length_TxtBx.Text = RCSN(0).SelectionLength
            RCSN_Text_Length_TxtBx.Text = RCSN(0).Text.Length

            ReplaceAll_Btn.Enabled = False
            ReplaceNext_Btn.Enabled = False
            FindIn_CmbBx.ValueMember = "Key"
            FindIn_CmbBx.DisplayMember = "Value"
            FillFindMagNoteItems()
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
    Private Sub Find_Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'MagNote_Form.PaintFindForm(Me)
    End Sub

    Private Function FillFindMagNoteItems() As Boolean
        If New StackFrame(1).GetMethod().Name.Contains("ReplaceBy_TxtBx_Validating") And
            Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
            If ReplaceBy_TxtBx.TextLength > 0 Then
                If MagNote_Form.Language_Btn.Text = "ع" Then
                    If FindIn_CmbBx.Text <> "Replace In Note Body" Then
                        FindIn_CmbBx.Items.Clear()
                        FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Body", "Replace In Note Body"))
                        FindIn_CmbBx.SelectedIndex = 0
                        Return True
                    End If
                Else
                    If FindIn_CmbBx.Text <> "إستبدال فى الماجنوت" Then
                        FindIn_CmbBx.Items.Clear()
                        FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Body", "إستبدال فى الماجنوت"))
                        FindIn_CmbBx.SelectedIndex = 0
                        Return True
                    End If
                End If
            End If
        Else
            FindIn_CmbBx.Items.Clear()
            If MagNote_Form.Language_Btn.Text = "ع" Then
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Body", "Find In Note Body"))
                If Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
                    FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_MagNotes_Bodies", "Find In MagNotes Bodies"))
                End If
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Names", "Find In Note Names"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Labels", "Find In Note Labels"))
                If Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
                    FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Shortcuts", "Find In Shortcuts"))
                End If
                FindIn_CmbBx.Text = "Find In Note Body"
            Else
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Body", "البحث فى الماجنوت"))
                If Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
                    FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_MagNotes_Bodies", "البحث فى محتوى الماجنوتات"))
                End If
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Names", "البحث فى أسماء الماجنوتات"))
                FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Labels", "البحث فى عناوين الماجنوتات"))
                If Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
                    FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Shortcuts", "البحث فى الإختصارات"))
                End If
                FindIn_CmbBx.Text = "البحث فى الماجنوت"
            End If
        End If
        ChangeFind_InAvilableMagNotesChkBxStts()
    End Function

    Private Sub FindIn_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FindIn_CmbBx.SelectedIndexChanged

    End Sub

    Private Sub FindIn_CmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles FindIn_CmbBx.SelectedValueChanged
        If FindIn_CmbBx.SelectedIndex = -1 Then Exit Sub
        Select Case DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key
            Case "Find_In_Note_Names", "Find_In_Note_Labels", "Find_In_MagNotes_Bodies"
                MagNote_Form.Setting_TbCntrl.SelectTab(MagNote_Form.Setting_TbCntrl.TabPages("MagNotes_TbPg"))
            Case "Find_In_Note_Shortcuts"
                MagNote_Form.Setting_TbCntrl.SelectTab(MagNote_Form.Setting_TbCntrl.TabPages("Shortcuts_TbPg"))
        End Select
    End Sub

    Private Sub FindAll_Btn_Click(sender As Object, e As EventArgs) Handles FindAll_Btn.Click
        Try
            Cursor = Cursors.WaitCursor
            If TextLengthZero() Then Exit Sub
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key <> "Find_In_Note_Body" Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "غير متاح مع  " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                Else
                    Msg = "Unevailable With " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            ClearPreviousSearchResult()
            FindInMagNote_TxtBx(FindText_CmbBx.Text, 0)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function TextLengthZero() As Boolean
        If IsNothing(FindIn_CmbBx.SelectedItem) Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "يجب أولا تحديد عنصر البحث... حاول مرة اخرى"
            Else
                Msg = "Select Search Object First... Try Again"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            Return True
        End If
        If FindText_CmbBx.Text.Length = 0 And
            Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex = -1 And
            Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex = -1 Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "أدخل كلمة البحث أولا"
            Else
                Msg = "Enter The Search Word First"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            Return True
        End If
    End Function
    Public Function ClearPreviousSearchResult() As Boolean
        If MagNote_Form.Clear_Previous_Search_Result_ChkBx.CheckState = CheckState.Checked Then
            ClearSearchResult()
        End If
    End Function
    Dim SearchParamiters As String
    Dim SerchTextFound As Boolean
    Dim FindArray(0) As FindArrayFields
    Private Function FindInMagNote_TxtBx(ByVal TextToFind As String, Optional StartPosition As Integer = 0, Optional FindNext As Boolean = False) As Object
        Cursor = Cursors.WaitCursor
        Try
            Dim xx = RCSN.Find(TextToFind, StartPosition, CType(0, RichTextBoxFinds))
            If xx <> -1 Then
                SerchTextFound = True
                FindArray(FindArray.Length - 1).SelectedText = TextToFind
                FindArray(FindArray.Length - 1).BackColor = RCSN.SelectionBackColor
                FindArray(FindArray.Length - 1).ForeColor = RCSN.SelectionColor
                FindArray(FindArray.Length - 1).SelectionStart = xx
                FindArray(FindArray.Length - 1).SlectionLength = TextToFind.Length

                RCSN.SelectionStart = xx
                RCSN.SelectionLength = TextToFind.Length
                RCSN.SelectionColor = Color.Red
                RCSN.SelectionBackColor = Color.Black
                Array.Resize(FindArray, FindArray.Length + 1)
                If xx + TextToFind.Length >= RCSN.Text.Length Then
                    If FindNext Then Return 0
                    Exit Function
                End If
                If FindNext Then Return xx + TextToFind.Length
                FindInMagNote_TxtBx(TextToFind, xx + TextToFind.Length)
                RCSN.Focus()
            Else
                Return 0
            End If
        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try
    End Function
    Private Function ClearSearchResult()
        If Clear_Previously_Searched_Phrase_ChkBx.CheckState = CheckState.Checked Then
            FindText_CmbBx.Items.Clear()
            If File.Exists(SearchWordsRepository) Then
                My.Computer.FileSystem.DeleteFile(SearchWordsRepository, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            End If
        End If
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            If IsNothing(FindArray) Or
                (String.IsNullOrEmpty(FindArray(0).SelectedText) And Not FindArray.Length > 1) Then
                Exit Function
            End If
            Dim SelectionStart = RCSN.SelectionStart
            Dim SelectionLength = RCSN.SelectionLength
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, FindArray.Count)
            For Each SelectedText In FindArray.ToList
                progress += ProgressToAdd
                Previewlbl.Text = "}Clearing--> " & SelectedText.SelectedText & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                If IsNothing(SelectedText) Then Continue For
                RCSN.SelectionStart = SelectedText.SelectionStart
                RCSN.SelectionLength = SelectedText.SlectionLength
                RCSN.SelectionBackColor = SelectedText.BackColor
                RCSN.SelectionColor = SelectedText.ForeColor
            Next
            Application.DoEvents()
            RCSN.SelectionStart = Val(SelectionStart)
            RCSN.SelectionLength = Val(SelectionLength)
            Dim valueArray(0) As FindArrayFields
            FindArray = valueArray
            FindArray.Initialize()
        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Function
    Dim RCSNSelectionBackColor, RCSNSelectionColor As Color
    Private Function FindNextBackForeColor() As Boolean
        Dim ReplaceNextBtnClicked As Boolean
        If New StackFrame(1).GetMethod().Name.Contains("ReplaceNext_Btn_Click") Or
            New StackFrame(1).GetMethod().Name.Contains("FindNext_Btn_Click") Then
            ReplaceNextBtnClicked = True
        ElseIf New StackFrame(1).GetMethod().Name.Contains("ReplaceAll_Btn_Tick") Then
        End If


        Dim rtchtxtbx = RCSN(0)
        Dim FindAt = rtchtxtbx.Text.Length
        If IsNothing(rtchtxtbx) Then Exit Function
        If rtchtxtbx.SelectionLength > 0 Then
            FindAt = rtchtxtbx.SelectionLength
        End If
        rtchtxtbx.SelectionLength = 1
        For I = rtchtxtbx.SelectionStart + 1 To FindAt
            rtchtxtbx.SelectionStart = I
            If Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                If rtchtxtbx.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem And
                   rtchtxtbx.SelectionBackColor = Back_Color_To_Find_ClrCmbBx.SelectedItem And
                   ReplaceNextBtnClicked Then
                    Return True
                End If
            ElseIf Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                If rtchtxtbx.SelectionBackColor.ToArgb = Back_Color_To_Find_ClrCmbBx.SelectedItem.ToArgb And
                   ReplaceNextBtnClicked Then
                    'RCSN(0).SelectionStart = rtchtxtbx.SelectionStart + 1
                    Return True
                End If
            ElseIf Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                If rtchtxtbx.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem And
                   ReplaceNextBtnClicked Then
                    Return True
                End If
            End If
        Next
    End Function
    Private Sub FindNext_Btn_Click(sender As Object, e As EventArgs) Handles FindNext_Btn.Click
        Try
            If Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Or
                Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                RCSN.SelectionLength = 0
                If FindNextBackForeColor() Then
                    RCSN.Focus()
                    SerchTextFound = True
                End If
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            If SearchParamiters <> FindIn_CmbBx.SelectedItem.ToString & FindText_CmbBx.Text Then
                SerchTextFound = False
                SearchParamiters = FindIn_CmbBx.SelectedItem.ToString & FindText_CmbBx.Text
            End If
            Cursor = Cursors.WaitCursor
            ClearPreviousSearchResult()
            Application.DoEvents()
            RCSNSelectionBackColor = RCSN.SelectionBackColor
            RCSNSelectionColor = RCSN.SelectionColor
            Select Case Find_In_Avilable_MagNotes_ChkBx.CheckState
                Case CheckState.Checked
                    FindInAvilableMagNotes()
                Case CheckState.Unchecked
                    FindInSavedMagNotes()
            End Select

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
            If Not SerchTextFound Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "لا توجد نتيجة لهذا البحث"
                Else
                    Msg = "No Result Fount For This Search"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            ElseIf Val(FindNext_Btn.Tag) = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "نهاية البحث"
                Else
                    Msg = "End Of Search"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            End If
        End Try
    End Sub

    Private Function FindInAvilableMagNotes() As Boolean
        If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Labels" Then
            For Each Note In MagNote_Form.Available_MagNotes_DGV.Rows
                Dim xx As String = LCase(Note.cells("MagNote_Label").value.ToString)
                If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                    If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Note.index <= Val(FindNext_Btn.Tag) Then
                        Continue For
                    End If
                    SerchTextFound = True
                    SetMagNoteNoCmbBxFocused()
                    MagNote_Form.MagNote_No_CmbBx.Text = Note.cells("MagNote_Label").value
                    FindNext_Btn.Tag = Note.index
                    Exit Function
                End If
            Next
            FindNext_Btn.Tag = Nothing
        ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_MagNotes_Bodies" Then
            For Each Note In MagNote_Form.Available_MagNotes_DGV.Rows
                Dim xx As String = LCase(Note.cells("MagNote").value.ToString)
                If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                    If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Note.index <= Val(FindNext_Btn.Tag) Then
                        Continue For
                    End If
                    SetMagNoteNoCmbBxFocused()
                    MagNote_Form.MagNote_No_CmbBx.Text = Note.cells("MagNote_Label").value
                    FindNext_Btn.Tag = Note.index
                    SerchTextFound = True
                    Exit Function
                End If
            Next
            FindNext_Btn.Tag = Nothing
        ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Names" Then
            For Each Note In MagNote_Form.Available_MagNotes_DGV.Rows
                Dim xx As String = LCase(Note.cells("MagNote_Name").value.ToString)
                If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                    If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Note.index <= Val(FindNext_Btn.Tag) Then
                        Continue For
                    End If
                    SetMagNoteNoCmbBxFocused()
                    MagNote_Form.MagNote_No_CmbBx.Text = Note.cells("MagNote_Label").value
                    FindNext_Btn.Tag = Note.index
                    SerchTextFound = True
                    Exit Function
                End If
            Next
            FindNext_Btn.Tag = Nothing
        ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Body" Then
            FindNext_Btn.Tag = FindInMagNote_TxtBx(FindText_CmbBx.Text, Val(FindNext_Btn.Tag), 1)
            If Not IsNothing(FindNext_Btn.Tag) And Val(FindNext_Btn.Tag.ToString) <> 0 Then
                RCSN.SelectionStart = FindNext_Btn.Tag - FindText_CmbBx.Text.Length
                MagNote_Form.ActiveControl = RCSN()
                RCSN.Focus()
            End If
        ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Shortcuts" Then
            For Each TbPg In MagNote_Form.ShortCut_TbCntrl.TabPages
                Dim CurrentShortcutsLstVw = CType(MagNote_Form.ShortCut_TbCntrl.Controls(TbPg.text).Controls(TbPg.text), ListView)
                CurrentShortcutsLstVw.SelectedItems.Clear()
                For Each Shortcut In CurrentShortcutsLstVw.Items
                    Dim xx As String = LCase(Shortcut.text)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And
                            Shortcut.index <= Val(FindNext_Btn.Tag) Then
                            Continue For
                        End If
                        CurrentShortcutsLstVw.Items(Shortcut.index).selected = True
                        MagNote_Form.ShortCut_TbCntrl.SelectedTab = TbPg
                        FindNext_Btn.Tag = Shortcut.index
                        CurrentShortcutsLstVw.EnsureVisible(Shortcut.index)
                        CurrentShortcutsLstVw.Focus()
                        SerchTextFound = True
                        Exit Function
                    End If
                Next
            Next
            FindNext_Btn.Tag = Nothing
        End If
    End Function
    Private Function FindInSavedMagNotes() As Boolean

        Dim MagNote_FormUse_Default_Encryption_Key_ChkBxCheckState As CheckState
        Try
            MagNote_FormUse_Default_Encryption_Key_ChkBxCheckState = MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState
            Dim MagNotes() As String = Directory.GetFiles(MagNoteFolderPath, "MagNote -(*.txt", SearchOption.TopDirectoryOnly)
            Dim MagoteDGVRow As New DataGridViewRow
            Dim F, FileName, HideFinishedMagNoteChkBx
            Find_PrgrssBr.Maximum = MagNotes.Count
            Find_PrgrssBr.Value = 0
            For Each MagNoteFile In MagNotes
                MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState = CheckState.Unchecked
                Find_PrgrssBr.Value += 1
                Find_PrgrssBr.Refresh()
                MagoteDGVRow = New DataGridViewRow
                F = Path.GetFileNameWithoutExtension(MagNoteFile)
                FileName = Microsoft.VisualBasic.Left(F, Len("MagNote -(")) & Microsoft.VisualBasic.Right(Path.GetFileName(F), 2)
                If Not Path.GetExtension(MagNoteFile) = ".txt" Or
                            FileName <> "MagNote -()-" Then
                    Continue For
                Else
                    Dim array() As String = MagNote_Form.ReadFile(MagNoteFile, 1)
                    Dim CellNo = 0
                    Dim Column As New DataGridViewTextBoxCell()
                    For Each Cell In array
                        If String.IsNullOrEmpty(Cell) Then
                            Cell = String.Empty
                        End If
                        Column = New DataGridViewTextBoxCell()
                        Column.Value = Cell
                        MagoteDGVRow.Cells.Add(Column)
                        CellNo += 1
                    Next
                End If
                If String.IsNullOrEmpty(MagoteDGVRow.Cells(24).Value) Then
                    MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState = CheckState.Unchecked
                Else
                    MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState = MagoteDGVRow.Cells(24).Value
                End If
                If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Labels" Then
                    Dim xx As String = LCase(MagoteDGVRow.Cells(1).Value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        MagNote_Form.ReadFile(MagoteDGVRow.Cells(0).Value,, 1,,, 1)
                        SerchTextFound = True
                    End If
                ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Names" Then
                    If LCase(MagoteDGVRow.Cells(0).Value.ToString).Contains(LCase(FindText_CmbBx.Text)) Then
                        MagNote_Form.ReadFile(MagoteDGVRow.Cells(0).Value,, 1,,, 1)
                        SerchTextFound = True
                    End If
                ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Body" Then

                    MagoteDGVRow.Cells(3).Value = MagNote_Form.ConvertRichTextBox(Decrypt_Function(MagoteDGVRow.Cells(3).Value,, 0))
                    If Not IsNothing(MagoteDGVRow.Cells(3).Value) Then
                        If MagoteDGVRow.Cells(3).Value.ToString.Contains(FindText_CmbBx.Text) Or
                        MagoteDGVRow.Cells(2).Value = FindText_CmbBx.Text Then 'MagNote

                            HideFinishedMagNoteChkBx = MagNote_Form.Hide_Finished_MagNote_ChkBx.CheckState
                            MagNote_Form.Hide_Finished_MagNote_ChkBx.CheckState = CheckState.Unchecked

                            MagNote_Form.ReadFile(MagoteDGVRow.Cells(0).Value,, 1,,, 1)

                            MagNote_Form.Hide_Finished_MagNote_ChkBx.CheckState = HideFinishedMagNoteChkBx

                            SerchTextFound = True
                        End If
                    Else
                        ShowMsg("Couldent Decrypt This MagNote (" & MagNoteFile & ")", "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                    End If
                End If
            Next
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            MagNote_Form.Use_Default_Encryption_Key_ChkBx.CheckState = MagNote_FormUse_Default_Encryption_Key_ChkBxCheckState
        End Try
    End Function
    Private Function ReplaceOnlyBackForeColor() As Boolean
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Dim CurrentSelectionStart = RCSN(0).SelectionStart
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ToTxtLngth = RCSN.TextLength
            Dim FromTxtLngth = 0
            If RCSN(0).SelectionLength <> 0 Then
                ToTxtLngth = RCSN(0).SelectionLength
            End If
            If New StackFrame(1).GetMethod().Name = "ReplaceNext_Btn_Click" Then
                FromTxtLngth = RCSN(0).SelectionStart
            ElseIf New StackFrame(1).GetMethod().Name = "ReplaceAll_Btn_Tick" Then
            End If
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, ToTxtLngth - 1)
            Dim Input = RCSN(0).Text
            Dim positions As New List(Of Integer)()
            Dim index As Integer = Input.IndexOf(FindText_CmbBx.Text, StringComparison.OrdinalIgnoreCase)
            While index <> -1
                positions.Add(index)
                index = Input.IndexOf(FindText_CmbBx.Text, index + FindText_CmbBx.Text.Length, StringComparison.OrdinalIgnoreCase)
            End While
            For Wrd = 0 To positions.Count - 1
                Me.Cursor = Cursors.WaitCursor
                progress += ProgressToAdd
                Previewlbl.Text = "Changing Color Properties--> " & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()

                RCSN.SelectionStart = positions(Wrd)
                RCSN.SelectionLength = FindText_CmbBx.Text.Length
                If Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                    RCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                End If
                If Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                    RCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                End If
            Next
            If positions.Count > 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تم الانتهاء من استبدال الالوان؟"
                Else
                    Msg = "Replacing Color Finished"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
            RCSN(0).SelectionStart = CurrentSelectionStart
        End Try
    End Function

    Private Function ReplaceBackForeColor() As Boolean
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ToTxtLngth = RCSN.TextLength
            Dim FromTxtLngth = 0
            If RCSN(0).SelectionLength <> 0 Then
                ToTxtLngth = RCSN(0).SelectionLength
            End If
            If New StackFrame(1).GetMethod().Name = "ReplaceNext_Btn_Click" Then
                FromTxtLngth = RCSN(0).SelectionStart
            ElseIf New StackFrame(1).GetMethod().Name = "ReplaceAll_Btn_Tick" Then
            End If
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, ToTxtLngth - 1)
            For i As Integer = FromTxtLngth To ToTxtLngth - 1
                Me.Cursor = Cursors.WaitCursor
                progress += ProgressToAdd
                Previewlbl.Text = "Changing Color Properties--> " & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()

                RCSN.Select(i, 1)
                RCSN.Select(i, 1)

                If Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                    Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                    If RCSN.SelectionBackColor = Back_Color_To_Find_ClrCmbBx.SelectedItem And
                        RCSN.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem Then
                        If Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                            RCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                        If Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                            RCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                    End If
                ElseIf Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                           Fore_Color_To_Find_ClrCmbBx.SelectedIndex = -1 Then
                    If RCSN.SelectionBackColor = Back_Color_To_Find_ClrCmbBx.SelectedItem Then
                        If Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                            RCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                        If Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                            RCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                    End If
                ElseIf Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                           Back_Color_To_Find_ClrCmbBx.SelectedIndex = -1 Then
                    If RCSN.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem Then
                        If Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                            RCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                        If Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                            RCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                    End If
                End If
            Next
            Dim x = 1
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Visible = False
            PreviewPnl.Dispose()
        End Try
    End Function
    Private Sub ReplaceNext_Btn_Click(sender As Object, e As EventArgs) Handles ReplaceNext_Btn.Click
        If Only_For_Colors_ChkBx.CheckState = CheckState.Checked Then
            If Back_Color_To_Find_ClrCmbBx.Enabled Or
            Fore_Color_To_Find_ClrCmbBx.Enabled Then
                ReplaceBackForeColor()
                Exit Sub
            End If
            If FindIn_CmbBx.Text.Length > 0 And
                    ReplaceBy_TxtBx.TextLength = 0 And
                    (Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Or
                    Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1) Then
                ReplaceOnlyBackForeColor()
                Exit Sub
            End If
        End If

        If FindText_CmbBx.Text = ReplaceBy_TxtBx.Text Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "الكلمة التى تريد استبدالها مطابقة لتلك التى تريد استبدالها بها... هل تريد الإستمرار؟"
            Else
                Msg = "The Word You Want To Replace Is Identical To The Word You Want To Replace It With... Do You Want To Continue?"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me) = DialogResult.No Then
                Exit Sub
            End If
        End If
        Cursor = Cursors.WaitCursor
        If TextLengthZero() Then Exit Sub
        Try
            If IsNothing(FindIn_CmbBx.SelectedItem) Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "يجب أولا تحديد عنصر البحث... حاول مرة اخرى"
                Else
                    Msg = "Select Search Object First... Try Again"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            ClearPreviousSearchResult()
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Labels" Then
                Exit Sub
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_MagNotes_Bodies" Then
                Exit Sub
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Names" Then
                Exit Sub
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Body" Then
                'Dim SelectionStart = RCSN.SelectionStart
                'Dim SelectionLength = RCSN.Text.Length
                Dim SelectionEnd = RCSN.Text.Length
                If RCSN.SelectionLength > 0 Then
                    'SelectionLength = RCSN.SelectionLength
                    SelectionEnd = RCSN.SelectionStart + RCSN.SelectionLength
                End If
                CurrentRCSN = RCSN()
                Replace_TxtBx(FindText_CmbBx.Text, RCSN.SelectionStart, 1, SelectionEnd)
                AcumulatedSelectionSize = 0
                StoredSelectionStart = 0
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Shortcuts" Then
                Exit Sub
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ReplaceAll_Btn_Click(sender As Object, e As EventArgs) Handles ReplaceAll_Btn.Click
        Dim SearchEnd As Integer = 0
        Try
            If FindText_CmbBx.Text = ReplaceBy_TxtBx.Text And
                ReplaceBy_TxtBx.TextLength > 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "الكلمة التى تريد استبدالها مطابقة لتلك التى تريد استبدالها بها... هل تريد الاستمرار؟"
                Else
                    Msg = "The Word You Want To Replace Is Identical To The Word You Want To Replace It With... Do You Want To Continie?"
                End If
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            Cursor = Cursors.WaitCursor
            If TextLengthZero() Then Exit Sub
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key <> "Find_In_Note_Body" Then
SelectedItemNNull:
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "غير متاح مع  " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                Else
                    Msg = "Unevailable With " & DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Value
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            ClearPreviousSearchResult()
            Dim SelectionStart = RCSN.SelectionStart '8120
            Dim SelectionLength = RCSN.SelectionLength '229
            SearchEnd = RCSN.SelectionStart + RCSN.SelectionLength
            If RCSN.SelectionLength > 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "سيتم استدال جميع الكلمات المطابقة لكلمة البحث فى الجزء المحدد من الماجنوت... هل انت موافق؟"
                Else
                    Msg = "All Words Compatible With The Search Word Will Be Replaced For The Selected Part Of The MagNote... Are You Agree?"
                End If
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me) = DialogResult.No Then
                    Exit Sub
                End If
                CurrentRCSN = RCSN()
                Replace_TxtBx(FindText_CmbBx.Text, RCSN.SelectionStart,, SearchEnd)
            ElseIf RCSN.SelectionLength = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "سيتم استدال جميع الكلمات المطابقة لكلمة البحث فى كامل الماجنوت... هل انت موافق؟"
                Else
                    Msg = "All Words Compatible With The Search Word Will Be Replaced For The Entire MagNote... Are You Agree?"
                End If
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me) Then
                    RCSN.SelectionStart = 0
                    RCSN.SelectAll()
                    CurrentRCSN = RCSN()
                    Replace_TxtBx(FindText_CmbBx.Text,,, RCSN.TextLength)
                End If
            Else
                CurrentRCSN = RCSN()
                Replace_TxtBx(FindText_CmbBx.Text, 0,, SearchEnd)
            End If
            RCSN.DeselectAll()
            ApplicationDoEvents(Me)
            RCSN.SelectionStart = SelectionStart
            RCSN.SelectionLength = SelectionLength
            AcumulatedSelectionSize = 0
            StoredSelectionStart = 0
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Dim TextToFindExist
    Dim AcumulatedSelectionSize As Integer = 0
    Dim StoredSelectionStart As Integer = 0
    Dim CurrentRCSN
    Private Function Replace_TxtBx(ByVal TextToFind As String,
                                   Optional StartPosition As Integer = 0,
                                   Optional FindNext As Boolean = False,
                                   Optional SearchEnd As Integer = 0) As Object
        Dim ColorToFindExist As Boolean
        Try
            Dim Diff = SearchEnd - StartPosition
            If New StackFrame(1).GetMethod().Name.Contains("Replace_TxtBx") Then
                AcumulatedSelectionSize = Diff
            Else
                StoredSelectionStart = CurrentRCSN.SelectionStart
            End If
ReRun:
            If SearchEnd > CurrentRCSN.Text.Length Then
                SearchEnd = CurrentRCSN.Text.Length ' - TextToFind.Length
            End If
            If SearchEnd > 0 Then
                TextToFindExist = CurrentRCSN.Find(TextToFind, StartPosition, SearchEnd, CType(0, RichTextBoxFinds))
            Else
                TextToFindExist = CurrentRCSN.Find(TextToFind, StartPosition, CType(0, RichTextBoxFinds))
            End If
            Refresh()
            If TextToFindExist <> -1 Then
                If Only_For_Colors_ChkBx.CheckState <> CheckState.Unchecked Then
                    If Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                        Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 And
                        Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                        Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                        If CurrentRCSN.SelectionBackColor = Back_Color_To_Find_ClrCmbBx.SelectedItem And
                            CurrentRCSN.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem Then
                            ColorToFindExist = True
                            CurrentRCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                            CurrentRCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                    ElseIf Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                            Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                        If CurrentRCSN.SelectionBackColor = Back_Color_To_Find_ClrCmbBx.SelectedItem Then
                            ColorToFindExist = True
                            CurrentRCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                    ElseIf Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 And
                               Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                        If CurrentRCSN.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem Then
                            ColorToFindExist = True
                            CurrentRCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                        End If
                    ElseIf Back_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                        ColorToFindExist = True
                        CurrentRCSN.SelectionBackColor = Back_Color_To_Replace_By_ClrCmbBx.SelectedItem
                    ElseIf Fore_Color_To_Replace_By_ClrCmbBx.SelectedIndex <> -1 Then
                        ColorToFindExist = True
                        CurrentRCSN.SelectionColor = Fore_Color_To_Replace_By_ClrCmbBx.SelectedItem
                    ElseIf Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                        If CurrentRCSN.SelectionColor = Fore_Color_To_Find_ClrCmbBx.SelectedItem Then
                            ColorToFindExist = True
                        End If
                    ElseIf Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Then
                        If CurrentRCSN.SelectionBackColor = Back_Color_To_Find_ClrCmbBx.SelectedItem Then
                            ColorToFindExist = True
                        End If
                    End If



                    If Only_For_Colors_ChkBx.CheckState = CheckState.Checked And
                    ColorToFindExist Then
                        'CurrentRCSN.SelectedText = Replace(CurrentRCSN.SelectedText, CurrentRCSN.SelectedText, ReplaceBy_TxtBx.Text)
                        DeleteReplaceText()
                    ElseIf ColorToFindExist And (
                               Fore_Color_To_Find_ClrCmbBx.SelectedIndex <> -1 Or
                               Back_Color_To_Find_ClrCmbBx.SelectedIndex <> -1) Then
                        'CurrentRCSN.SelectedText = Replace(CurrentRCSN.SelectedText, CurrentRCSN.SelectedText, ReplaceBy_TxtBx.Text)
                        DeleteReplaceText()
                    ElseIf Fore_Color_To_Find_ClrCmbBx.SelectedIndex = -1 And
                               Back_Color_To_Find_ClrCmbBx.SelectedIndex = -1 Then
                        'CurrentRCSN.SelectedText = Replace(CurrentRCSN.SelectedText, CurrentRCSN.SelectedText, ReplaceBy_TxtBx.Text)
                        DeleteReplaceText()
                    End If
                Else
                    'CurrentRCSN.SelectedText = Replace(CurrentRCSN.SelectedText, CurrentRCSN.SelectedText, ReplaceBy_TxtBx.Text)
                    DeleteReplaceText()
                End If
                If TextToFindExist + TextToFind.Length >= CurrentRCSN.Text.Length Then
                    If FindNext Then
                        Return 0
                    End If
                    Exit Function
                End If
                If FindNext Then
                    Return TextToFindExist + TextToFind.Length
                End If
                If SearchEnd > 0 Then
                    If StoredSelectionStart + AcumulatedSelectionSize >= SearchEnd Then
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Msg = "تم الانتهاء من عملية استبدال الكلمات"
                        Else
                            Msg = "Finished Replacing Words Done"
                        End If
                        ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
                        Exit Function
                    End If
                End If
                If CurrentRCSN.SelectionStart + TextToFind.Length <= SearchEnd Then
                    CurrentRCSN.SelectionLength = 0
                    CurrentRCSN.SelectionStart = CurrentRCSN.SelectionStart + TextToFind.Length
                    StartPosition = CurrentRCSN.SelectionStart
                    ColorToFindExist = False
                    GoTo ReRun

                    'Replace_TxtBx(TextToFind, CurrentRCSN.SelectionStart, FindNext, SearchEnd)
                    'Exit Function
                End If
            Else
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تم الانتهاء من عملية استبدال الكلمات"
                Else
                    Msg = "Finished Replacing Words Done"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
            End If
        Catch ex As Exception
        Finally
        End Try
    End Function
    Private Function DeleteReplaceText() As Boolean
        RCSN.SelectionLength = 0
        For x = 1 To FindText_CmbBx.Text.Length
            RCSN.Focus()
            SendKeys.SendWait("{DELETE}")
        Next
        RCSN.SelectedText = ReplaceBy_TxtBx.Text
    End Function
    Private Sub ReplaceBy_TxtBx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceBy_TxtBx.TextChanged,
        FindText_CmbBx.TextChanged
        FindNext_Btn.Tag = Nothing
        SerchTextFound = False
        ClearSearchResult()
        If ReplaceBy_TxtBx.TextLength > 0 And
            FindText_CmbBx.Text.Length > 0 And
            Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
            ReplaceAll_Btn.Enabled = True
            ReplaceNext_Btn.Enabled = True
        ElseIf FindText_CmbBx.Text.Length = 0 Then
            ReplaceAll_Btn.Enabled = False
            ReplaceNext_Btn.Enabled = False
        End If
    End Sub
    Private Sub FindText_CmbBx_LostFocus(sender As Object, e As EventArgs) Handles FindText_CmbBx.LostFocus
    End Sub
    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        'If FindText_CmbBx.Items.Count > 0 Then
        '    MagNote_Form.StoredFindText_CmbBx.Items.Clear()
        '    For Each Item In FindText_CmbBx.Items
        '        MagNote_Form.StoredFindText_CmbBx.Items.Add(Item)
        '    Next
        'End If
        Me.Close()
    End Sub

    Private Sub Clear_Search_Result_Btn_Click(sender As Object, e As EventArgs) Handles Clear_Search_Result_Btn.Click
        ClearSearchResult()
    End Sub

    Private Sub Find_In_Avilable_MagNotes_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Find_In_Avilable_MagNotes_ChkBx.CheckedChanged
        'Find_Color_Pnl.Visible = True
    End Sub

    Dim SearchWordsRepository As String = MagNoteFolderPath & "\SearchWordsRepository.fil"
    Private Sub Find_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If RunAsExternal() Then Exit Sub
            Dim TextToWrite As String
            For Each Word In FindText_CmbBx.Items
                TextToWrite &= Word & vbNewLine
            Next
            If Not String.IsNullOrEmpty(TextToWrite) Then
                TextToWrite = Microsoft.VisualBasic.Left(TextToWrite, TextToWrite.Length - 1)
            End If
            My.Computer.FileSystem.WriteAllText(SearchWordsRepository, TextToWrite, 0, System.Text.Encoding.UTF8)
            ClearSearchResult()
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub
    Private Sub ChangeFind_InAvilableMagNotesChkBxStts()
        If Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Find_In_Avilable_MagNotes_ChkBx.Text = "بحث فى الماجنوتات المتاحة"
            Else
                Find_In_Avilable_MagNotes_ChkBx.Text = "Find In Avilable MagNotes"
            End If
        Else
            If MagNote_Form.Language_Btn.Text = "E" Then
                Find_In_Avilable_MagNotes_ChkBx.Text = "بحث فى الماجنوتات المحفوظة"
            Else
                Find_In_Avilable_MagNotes_ChkBx.Text = "Find In Saved MagNotes"
            End If
        End If
    End Sub
    Private Sub Find_In_Avilable_MagNotes_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Find_In_Avilable_MagNotes_ChkBx.CheckStateChanged
        If Find_In_Avilable_MagNotes_ChkBx.CheckState = CheckState.Checked Then
            FindAll_Btn.Enabled = True
            ReplaceBy_TxtBx.Enabled = True
            ReplaceBy_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Else
            FindAll_Btn.Enabled = False
            ReplaceBy_TxtBx.Enabled = False
            ReplaceBy_TxtBx.BackColor = Color.WhiteSmoke
        End If
        FillFindMagNoteItems()
    End Sub

    Private Sub FindText_CmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FindText_CmbBx.SelectedIndexChanged

    End Sub

    Private Sub ReplaceBy_TxtBx_Validating(sender As Object, e As CancelEventArgs) Handles ReplaceBy_TxtBx.Validating
        If ReplaceBy_TxtBx.TextLength = 0 Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "هل تريد الاستبدال بكلمة فارغة؟"
            Else
                Msg = "Do Wnt To Replace By Blank Text"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me) = DialogResult.No Then
                e.Cancel = True
            End If
        End If

        If FillFindMagNoteItems() Then
            e.Cancel = True
        End If

    End Sub

    Private Sub FindText_CmbBx_Validating(sender As Object, e As CancelEventArgs) Handles FindText_CmbBx.Validating
        If FindText_CmbBx.Text.Length = 0 Then Exit Sub
        If FindText_CmbBx.SelectedIndex = -1 Then
            FindText_CmbBx.Items.Add(FindText_CmbBx.Text)
        End If
    End Sub

    Private Sub Show_Back_Color_Btn_Click(sender As Object, e As EventArgs) Handles Show_Back_Color_To_Find_Btn.Click, Show_Fore_Color_To_Find_Btn.Click, Show_Back_Color_To_Replace_By_Btn.Click, Show_Fore_Color_To_Replace_By_Btn.Click
        Try
            If Only_For_Colors_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Sub
            End If
            Dim SelectionColor, SelectionBackColor As Color
            SelectionColor = RCSN(0).SelectionColor
            If Not SelectionColor.IsKnownColor Then
                Fore_Color_To_Find_ClrCmbBx.Items.Add(SelectionColor)
            End If
            SelectionBackColor = RCSN(0).SelectionBackColor
            If Not SelectionBackColor.IsKnownColor Then
                Back_Color_To_Find_ClrCmbBx.Items.Add(SelectionBackColor)
            End If
            Dim Cntrl = CType(FindControlRecursive(New List(Of Control), Me, New List(Of Type),, Replace(Replace(sender.name, "Show_", ""), "Btn", "ClrCmbBx")).ToList.Item(0), ColorsComboBox.ColorsComboBox)
            ReplaceNext_Btn.Enabled = False
            ReplaceAll_Btn.Enabled = False
            If Cntrl.Enabled Then
                Cntrl.Enabled = False
                Cntrl.SelectedIndex = -1
            Else
                Cntrl.Enabled = True
            End If

            Select Case sender.name
                Case Show_Back_Color_To_Find_Btn.Name
                    If Back_Color_To_Find_ClrCmbBx.Enabled Then
                        Back_Color_To_Find_ClrCmbBx.SelectedItem = SelectionBackColor
                        Back_Color_To_Find_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
                        Back_Color_To_Replace_By_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
                    Else
                        Back_Color_To_Find_ClrCmbBx.BackColor = System.Drawing.SystemColors.Window
                        Back_Color_To_Replace_By_ClrCmbBx.BackColor = System.Drawing.SystemColors.Window
                    End If
                Case Show_Fore_Color_To_Find_Btn.Name
                    If Fore_Color_To_Find_ClrCmbBx.Enabled Then
                        Fore_Color_To_Find_ClrCmbBx.SelectedItem = SelectionColor
                        Fore_Color_To_Find_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
                        Fore_Color_To_Replace_By_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
                    Else
                        Fore_Color_To_Find_ClrCmbBx.BackColor = System.Drawing.SystemColors.Window
                        Fore_Color_To_Replace_By_ClrCmbBx.BackColor = System.Drawing.SystemColors.Window
                    End If
                Case Show_Back_Color_To_Replace_By_Btn.Name
                    If Back_Color_To_Replace_By_ClrCmbBx.Enabled = True Then
                        ReplaceNext_Btn.Enabled = True
                        ReplaceAll_Btn.Enabled = True
                    Else
                        ReplaceNext_Btn.Enabled = False
                        ReplaceAll_Btn.Enabled = False
                    End If
                Case Show_Fore_Color_To_Replace_By_Btn.Name
                    If Fore_Color_To_Replace_By_ClrCmbBx.Enabled = True Then
                        ReplaceNext_Btn.Enabled = True
                    Else
                        ReplaceNext_Btn.Enabled = False
                    End If
            End Select
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, Me)
        End Try
    End Sub

    Private Sub Back_Color_To_Find_ClrCmbBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Back_Color_To_Find_ClrCmbBx.SelectedIndexChanged

    End Sub

    Private Sub Find_Color_Pnl_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Back_Color_To_Find_ClrCmbBx_SelectedValueChanged(sender As Object, e As EventArgs) Handles Back_Color_To_Find_ClrCmbBx.SelectedValueChanged, Back_Color_To_Replace_By_ClrCmbBx.SelectedValueChanged, Fore_Color_To_Find_ClrCmbBx.SelectedValueChanged, Fore_Color_To_Replace_By_ClrCmbBx.SelectedValueChanged

        Dim Cntrl = CType(FindControlRecursive(New List(Of Control), Me, New List(Of Type),, Replace(sender.name, "ClrCmbBx", "Lbl")).ToList.Item(0), Label)

        Cntrl.BackColor = sender.selecteditem
        Dim Backbrightness As Single = Cntrl.BackColor.GetBrightness
        Dim Forebrightness As Single = Cntrl.BackColor.GetBrightness
        If Backbrightness > 0.4 And
            Forebrightness > 0.4 Then
            Cntrl.ForeColor = System.Drawing.SystemColors.WindowText
        ElseIf Backbrightness < 0.4 And
            Forebrightness < 0.4 Then
            Cntrl.ForeColor = System.Drawing.SystemColors.Window
        End If

    End Sub

    Private Sub Only_For_Colors_ChkBx_CheckedChanged(sender As Object, e As EventArgs) Handles Only_For_Colors_ChkBx.CheckedChanged

    End Sub

    Private Sub Find_Color_Pnl_EnabledChanged(sender As Object, e As EventArgs)
        Back_Color_To_Find_Lbl.Enabled = sender.Enabled
        Show_Back_Color_To_Find_Btn.Enabled = sender.Enabled
        Back_Color_To_Find_ClrCmbBx.Enabled = sender.Enabled
        Back_Color_To_Replace_By_Lbl.Enabled = sender.Enabled
        Show_Back_Color_To_Replace_By_Btn.Enabled = sender.Enabled
        Back_Color_To_Replace_By_ClrCmbBx.Enabled = sender.Enabled
        Fore_Color_To_Find_Lbl.Enabled = sender.Enabled
        Show_Fore_Color_To_Find_Btn.Enabled = sender.Enabled
        Fore_Color_To_Find_ClrCmbBx.Enabled = sender.Enabled
        Fore_Color_To_Replace_By_Lbl.Enabled = sender.Enabled
        Show_Fore_Color_To_Replace_By_Btn.Enabled = sender.Enabled
        Fore_Color_To_Replace_By_ClrCmbBx.Enabled = sender.Enabled
    End Sub

    Private Sub Selection_Length_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles Selection_Length_TxtBx.TextChanged
        If Val(Selection_Length_TxtBx.Text) > 0 Then
            Selection_Length_TxtBx.BackColor = Color.Red
        Else
            Selection_Length_TxtBx.BackColor = Me.BackColor
        End If

    End Sub

    Private Sub Only_For_Colors_ChkBx_CheckStateChanged(sender As Object, e As EventArgs) Handles Only_For_Colors_ChkBx.CheckStateChanged
        If Only_For_Colors_ChkBx.CheckState = CheckState.Unchecked Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Only_For_Colors_ChkBx.Text = "بحث وتعديل نص فقط"
            Else
                Only_For_Colors_ChkBx.Text = "Find And Replace Text Only"
            End If
        ElseIf Only_For_Colors_ChkBx.CheckState = CheckState.Checked Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Only_For_Colors_ChkBx.Text = "بحث وتعديل لون فقط"
            Else
                Only_For_Colors_ChkBx.Text = "Find And Replace Color Only"
            End If
        ElseIf Only_For_Colors_ChkBx.CheckState = CheckState.Indeterminate Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Only_For_Colors_ChkBx.Text = "بحث وتعديل نص ولون"
            Else
                Only_For_Colors_ChkBx.Text = "Find And Replace Text And Color"
            End If
        End If
    End Sub


End Class