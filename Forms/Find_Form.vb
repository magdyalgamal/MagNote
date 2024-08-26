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

    Private Sub Find_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = MagNote_Form.Note_Back_Color_ClrCmbBx.SelectedItem 'BackColor
        Me.ForeColor = MagNote_Form.Note_Font_Color_ClrCmbBx.SelectedItem ' ForeColor
        Me.Opacity = MagNote_Form.Form_Transparency_TrkBr.Value / 100
        If MagNote_Form.Language_Btn.Text = "ع" Then
            Me.RightToLeftLayout = False
            Me.RightToLeft = RightToLeft.No
        Else
            Me.RightToLeftLayout = True
            Me.RightToLeft = RightToLeft.Yes
        End If
        Labeling_Form(Me, "Arabic")
        Me.Location = New System.Drawing.Point(((Width / 2) + Left) - (Me.Width / 2), ((Height / 2) + Top) - (Me.Height / 2))
        AddHandler_Control_Move(Me)
        If MagNote_Form.StoredFindText_CmbBx.Items.Count > 0 Then
            For Each item In MagNote_Form.StoredFindText_CmbBx.Items
                FindText_CmbBx.Items.Add(item)
            Next
        End If
        ReplaceAll_Btn.Enabled = False
        ReplaceNext_Btn.Enabled = False
        FindIn_CmbBx.ValueMember = "Key"
        FindIn_CmbBx.DisplayMember = "Value"
        If MagNote_Form.Language_Btn.Text = "ع" Then
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Body", "Find In Note Body"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_MagNotes_Bodies", "Find In MagNotes Bodies"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Names", "Find In Note Names"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Labels", "Find In Note Labels"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Shortcuts", "Find In Shortcuts"))
            FindIn_CmbBx.Text = "Find In Note Body"
        Else
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Body", "البحث فى الملاحظة"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_MagNotes_Bodies", "البحث فى محتوى الملاحظات"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Names", "البحث فى أسماء الملاحظات"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Labels", "البحث فى عناوين الملاحظات"))
            FindIn_CmbBx.Items.Add(New KeyValuePair(Of String, String)("Find_In_Note_Shortcuts", "البحث فى الإختصارات"))
            FindIn_CmbBx.Text = "البحث فى الملاحظة"
        End If
    End Sub

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
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            ClearPreviousSearchResult()
            FindInMagNote_TxtBx(FindText_CmbBx.Text, 0)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
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
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
            Return True
        End If
        If FindText_CmbBx.Text.Length = 0 Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "أدخل كلمة البحث أولا"
            Else
                Msg = "Enter The Search Word First"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
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
                'FindArray(FindArray.Length - 1).SelectedText = "SelectionStart = " & xx & " Length = " & TextToFind.Length

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
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            If IsNothing(FindArray) Or
                String.IsNullOrEmpty(FindArray(0).SelectedText) Then
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
                'Dim ST = Split(SelectedText.SelectedText, "=")
                RCSN.SelectionStart = SelectedText.SelectionStart ' Replace(Replace(ST(1), "Length", ""), " ", "")
                RCSN.SelectionLength = SelectedText.SlectionLength ' Replace(ST(2), " ", "")
                RCSN.SelectionBackColor = SelectedText.BackColor '  RCSNSelectionBackColor 'RCSN.BackColor
                RCSN.SelectionColor = SelectedText.ForeColor ' RCSNSelectionColor 'RCSN.ForeColor
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

    Private Sub FindNext_Btn_Click(sender As Object, e As EventArgs) Handles FindNext_Btn.Click
        Try
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
            If DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Labels" Then
                For Each Note In MagNote_Form.Available_MagNotes_DGV.Rows
                    Dim xx As String = LCase(Note.cells("MagNote_Label").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Note.index <= Val(FindNext_Btn.Tag) Then Continue For
                        SerchTextFound = True
                        MagNote_Form.MagNote_No_CmbBx.Focus()
                        MagNote_Form.MagNote_No_CmbBx.Text = Note.cells("MagNote_Label").value
                        FindNext_Btn.Tag = Note.index
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_MagNotes_Bodies" Then
                For Each Note In MagNote_Form.Available_MagNotes_DGV.Rows
                    Dim xx As String = LCase(Note.cells("MagNote").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Note.index <= Val(FindNext_Btn.Tag) Then Continue For
                        MagNote_Form.MagNote_No_CmbBx.Focus()
                        MagNote_Form.MagNote_No_CmbBx.Text = Note.cells("MagNote_Label").value
                        FindNext_Btn.Tag = Note.index
                        SerchTextFound = True
                        Exit Sub
                    End If
                Next
                FindNext_Btn.Tag = Nothing
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Names" Then
                For Each Note In MagNote_Form.Available_MagNotes_DGV.Rows
                    Dim xx As String = LCase(Note.cells("MagNote_Name").value.ToString)
                    If xx.Contains(LCase(FindText_CmbBx.Text)) Then
                        If Not String.IsNullOrEmpty(FindNext_Btn.Tag) And Note.index <= Val(FindNext_Btn.Tag) Then Continue For
                        MagNote_Form.MagNote_No_CmbBx.Focus()
                        MagNote_Form.MagNote_No_CmbBx.Text = Note.cells("MagNote_Label").value
                        FindNext_Btn.Tag = Note.index
                        SerchTextFound = True
                        Exit Sub
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
                                Shortcut.index <= Val(FindNext_Btn.Tag) Then Continue For
                            CurrentShortcutsLstVw.Items(Shortcut.index).selected = True
                            MagNote_Form.ShortCut_TbCntrl.SelectedTab = TbPg
                            FindNext_Btn.Tag = Shortcut.index
                            CurrentShortcutsLstVw.EnsureVisible(Shortcut.index)
                            CurrentShortcutsLstVw.Focus()
                            SerchTextFound = True
                            Exit Sub
                        End If
                    Next
                Next
                FindNext_Btn.Tag = Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            If Not SerchTextFound Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "لا توجد نتيجة لهذا البحث"
                Else
                    Msg = "No Result Fount For This Search"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            ElseIf Val(FindNext_Btn.Tag) = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "نهاية البحث"
                Else
                    Msg = "End Of Search"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        End Try
    End Sub
    Private Sub ReplaceNext_Btn_Click(sender As Object, e As EventArgs) Handles ReplaceNext_Btn.Click
        If FindText_CmbBx.Text = ReplaceBy_TxtBx.Text Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "الكلمة التى تريد استبالها مطابقة لتلك التى تريد استبالها بها"
            Else
                Msg = "The Word You Want To Replace Is Identical To The Word You Want To Replace It With"
            End If
            ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            Exit Sub
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
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
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
                Dim SelectionStart = RCSN.SelectionStart
                Dim SelectionLength = RCSN.SelectionLength
                Dim SelectionRange = RCSN.SelectionStart + RCSN.SelectionLength

                Replace_TxtBx(FindText_CmbBx.Text, RCSN.SelectionStart, 1, SelectionRange)
                AcumulatedSelectionSize = 0
                StoredSelectionStart = 0
            ElseIf DirectCast(FindIn_CmbBx.SelectedItem, KeyValuePair(Of String, String)).Key = "Find_In_Note_Shortcuts" Then
                Exit Sub
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ReplaceAll_Btn_Click(sender As Object, e As EventArgs) Handles ReplaceAll_Btn.Click
        Dim SearchEnd As Integer = 0
        Try
            If FindText_CmbBx.Text = ReplaceBy_TxtBx.Text Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "الكلمة التى تريد استبالها مطابقة لتلك التى تريد استبالها بها"
                Else
                    Msg = "The Word You Want To Replace Is Identical To The Word You Want To Replace It With"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
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
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Exit Sub
            End If
            If TextLengthZero() Then Exit Sub
            ClearPreviousSearchResult()
            Dim SelectionStart = RCSN.SelectionStart '8120
            Dim SelectionLength = RCSN.SelectionLength '229
            SearchEnd = RCSN.SelectionStart + RCSN.SelectionLength
            If RCSN.SelectionLength > 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "سيتم استدال جميع الكلمات المطابقة لكلمة البحث فى الجزء المحدد من الملف الملف... هل انت موافق؟"
                Else
                    Msg = "All Words Compatible With The Search Word Will Be Replaced For The Selected Part Of The File... Are You Agree?"
                End If
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False) = DialogResult.No Then
                    Exit Sub
                End If
                Replace_TxtBx(FindText_CmbBx.Text, RCSN.SelectionStart,, SearchEnd)
            ElseIf RCSN.SelectionLength = 0 Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "سيتم استدال جميع الكلمات المطابقة لكلمة البحث فى كامل الملف... هل انت موافق؟"
                Else
                    Msg = "All Words Compatible With The Search Word Will Be Replaced For The Entire File... Are You Agree?"
                End If
                If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False) Then
                    RCSN.SelectionStart = 0
                    RCSN.SelectAll()
                    Replace_TxtBx(FindText_CmbBx.Text,,, RCSN.TextLength)
                End If
            Else
                Replace_TxtBx(FindText_CmbBx.Text, 0,, SearchEnd)
            End If
            RCSN.DeselectAll()
            Application.DoEvents()
            RCSN.SelectionStart = SelectionStart
            RCSN.SelectionLength = SelectionLength
            AcumulatedSelectionSize = 0
            StoredSelectionStart = 0
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Dim TextToFindExist
    Dim AcumulatedSelectionSize As Integer = 0
    Dim StoredSelectionStart As Integer = 0
    Private Function Replace_TxtBx(ByVal TextToFind As String,
                                   Optional StartPosition As Integer = 0,
                                   Optional FindNext As Boolean = False,
                                   Optional SearchEnd As Integer = 0) As Object
        Dim StoreSelectionLength
        Dim SelectionStart = RCSN.SelectionStart
        Dim RecallMe As Boolean
        Try
            Dim Diff = SearchEnd - StartPosition
            If New StackFrame(1).GetMethod().Name.Contains("Replace_TxtBx") Then
                AcumulatedSelectionSize = Diff
            Else
                StoredSelectionStart = RCSN.SelectionStart
            End If
            StoreSelectionLength = RCSN.SelectionLength
            If RCSN.SelectionLength > 0 Then
                TextToFindExist = RCSN.Find(TextToFind, RCSN.SelectionStart, SearchEnd, CType(0, RichTextBoxFinds))
            Else
                TextToFindExist = RCSN.Find(TextToFind, RCSN.SelectionStart, CType(0, RichTextBoxFinds))
            End If
            If TextToFindExist <> -1 Then
                If RCSN.SelectionStart = 0 Then
                    Dim x = 1
                End If
                FindArray(FindArray.Length - 1).SelectedText = TextToFind
                FindArray(FindArray.Length - 1).BackColor = RCSN.SelectionBackColor
                FindArray(FindArray.Length - 1).ForeColor = RCSN.SelectionColor
                FindArray(FindArray.Length - 1).SelectionStart = RCSN.SelectionStart
                FindArray(FindArray.Length - 1).SlectionLength = TextToFind.Length

                RCSN.SelectedText = Replace(RCSN.SelectedText, RCSN.SelectedText, ReplaceBy_TxtBx.Text)

                RCSN.SelectionStart = TextToFindExist
                RCSN.SelectionLength = ReplaceBy_TxtBx.TextLength
                RCSN.SelectionLength = StoreSelectionLength

                Array.Resize(FindArray, FindArray.Length + 1)
                If TextToFindExist + TextToFind.Length >= RCSN.Text.Length Then
                    If FindNext Then Return 0
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
                        ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                        Exit Function
                    End If
                End If
                RCSN.SelectionStart = RCSN.SelectionStart + TextToFind.Length
                Replace_TxtBx(TextToFind, RCSN.SelectionStart, FindNext, SearchEnd)

            Else
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "تم الانتهاء من عملية استبدال الكلمات"
                Else
                    Msg = "Finished Replacing Words Done"
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Sub ReplaceBy_TxtBx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceBy_TxtBx.TextChanged
        If ReplaceBy_TxtBx.TextLength > 0 Then
            ReplaceAll_Btn.Enabled = True
            ReplaceNext_Btn.Enabled = True
        End If
    End Sub
    Private Sub FindText_CmbBx_LostFocus(sender As Object, e As EventArgs) Handles FindText_CmbBx.LostFocus
        If Not FindText_CmbBx.Items.Contains(FindText_CmbBx.Text) And
            FindText_CmbBx.Text.Length > 0 Then
            FindText_CmbBx.Items.Add(FindText_CmbBx.Text)
        End If
    End Sub
    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        If FindText_CmbBx.Items.Count > 0 Then
            MagNote_Form.StoredFindText_CmbBx.Items.Clear()
            For Each Item In FindText_CmbBx.Items
                MagNote_Form.StoredFindText_CmbBx.Items.Add(Item)
            Next
        End If
        Me.Close()
    End Sub

    Private Sub Clear_Search_Result_Btn_Click(sender As Object, e As EventArgs) Handles Clear_Search_Result_Btn.Click
        ClearSearchResult()
    End Sub

    Private Sub Find_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ClearSearchResult()
    End Sub
End Class