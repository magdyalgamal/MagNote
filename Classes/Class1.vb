Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics
Imports unvell.Common
Imports unvell.ReoGrid.Editor.Properties
Imports unvell.ReoGrid.PropertyPages
Imports unvell.ReoGrid.Actions
Imports unvell.ReoGrid.CellTypes
Imports unvell.ReoGrid.Events
Imports unvell.ReoGrid.Data
Imports unvell.ReoGrid.WinForm
Imports unvell.ReoGrid.IO
Imports unvell.ReoGrid.DataFormat
Imports unvell.ReoGrid.Graphics
Imports unvell.ReoGrid.Rendering
Imports unvell.ReoGrid.Editor.LangRes
Imports unvell.ReoGrid.Print
Imports unvell.ReoGrid.Drawing
Imports unvell.ReoGrid.Drawing.Text
Imports Point = System.Drawing.Point

Namespace unvell.ReoGrid.Editor
    Partial Public Class ReoGridEditor
        Inherits Form

        Private nameManagerForm As NamedRangeManageForm = Nothing

        Public Sub New()
            InitializeComponent()
            NewDocumentOnLoad = True
            SuspendLayout()
            isUIUpdating = True
            SetupUILanguage()
            fontToolStripComboBox.Text = Worksheet.DefaultStyle.FontName
            fontSizeToolStripComboBox.Text = Worksheet.DefaultStyle.FontSize.ToString()
            fontSizeToolStripComboBox.Items.AddRange(FontUIToolkit.FontSizeList.[Select](Function(f) CObj(f)).ToArray())
            backColorPickerToolStripButton.CloseOnClick = True
            borderColorPickToolStripItem.CloseOnClick = True
            textColorPickToolStripItem.CloseOnClick = True
            Sticky_Note_Form.undoToolStripButton.Enabled = CSharpImpl.__Assign(Sticky_Note_Form.undoToolStripMenuItem.Enabled, CSharpImpl.__Assign(Sticky_Note_Form.redoToolStripButton.Enabled, CSharpImpl.__Assign(Sticky_Note_Form.redoToolStripMenuItem.Enabled, CSharpImpl.__Assign(Sticky_Note_Form.repeatLastActionToolStripMenuItem.Enabled, False))))
            zoomToolStripDropDownButton.Text = "100%"
            isUIUpdating = False
            toolbarToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(fontToolStrip.Visible, CSharpImpl.__Assign(toolStrip1.Visible, toolbarToolStripMenuItem.Checked))
            formulaBarToolStripMenuItem.CheckedChanged += Function(s, e) CSharpImpl.__Assign(formulaBar.Visible, formulaBarToolStripMenuItem.Checked)
            statusBarToolStripMenuItem.CheckedChanged += Function(s, e) CSharpImpl.__Assign(statusStrip1.Visible, statusBarToolStripMenuItem.Checked)
            sheetSwitcherToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.Grid.SetSettings(WorkbookSettings.View_ShowSheetTabControl, sheetSwitcherToolStripMenuItem.Checked)
            showHorizontaScrolllToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.Grid.SetSettings(WorkbookSettings.View_ShowHorScroll, showHorizontaScrolllToolStripMenuItem.Checked)
            showVerticalScrollbarToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.Grid.SetSettings(WorkbookSettings.View_ShowVerScroll, showVerticalScrollbarToolStripMenuItem.Checked)
            showGridLinesToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowGridLine, showGridLinesToolStripMenuItem.Checked)
            showPageBreakToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowPageBreaks, showPageBreakToolStripMenuItem.Checked)
            showFrozenLineToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowFrozenLine, showFrozenLineToolStripMenuItem.Checked)
            showRowHeaderToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, showRowHeaderToolStripMenuItem.Checked)
            showColumnHeaderToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowColumnHeader, showColumnHeaderToolStripMenuItem.Checked)
            showRowOutlineToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_AllowShowRowOutlines, showRowOutlineToolStripMenuItem.Checked)
            showColumnOutlineToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.View_AllowShowColumnOutlines, showColumnOutlineToolStripMenuItem.Checked)
            sheetReadonlyToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.Edit_Readonly, sheetReadonlyToolStripMenuItem.Checked)
            resetAllPageBreaksToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ResetAllPageBreaks()
            resetAllPageBreaksToolStripMenuItem1.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ResetAllPageBreaks()
            Sticky_Note_Form.Grid.WorksheetInserted += Function(ss, ee)
                                                           Dim worksheet = ee.Worksheet
                                                           worksheet.SelectionRangeChanged += AddressOf grid_SelectionRangeChanged
                                                           worksheet.SelectionModeChanged += AddressOf worksheet_SelectionModeChanged
                                                           worksheet.SelectionStyleChanged += AddressOf worksheet_SelectionModeChanged
                                                           worksheet.SelectionForwardDirectionChanged += AddressOf worksheet_SelectionForwardDirectionChanged
                                                           worksheet.FocusPosStyleChanged += AddressOf worksheet_SelectionModeChanged
                                                           worksheet.CellsFrozen += AddressOf UpdateMenuAndToolStripsWhenAction
                                                           worksheet.Resetted += AddressOf worksheet_Resetted
                                                           worksheet.SettingsChanged += AddressOf worksheet_SettingsChanged
                                                           worksheet.Scaled += AddressOf worksheet_GridScaled
                                                       End Function

            Sticky_Note_Form.Grid.WorksheetRemoved += Function(ss, ee)
                                                          Dim worksheet = ee.Worksheet
                                                          worksheet.SelectionRangeChanged -= AddressOf grid_SelectionRangeChanged
                                                          worksheet.SelectionModeChanged -= AddressOf worksheet_SelectionModeChanged
                                                          worksheet.SelectionStyleChanged -= AddressOf worksheet_SelectionModeChanged
                                                          worksheet.SelectionForwardDirectionChanged -= AddressOf worksheet_SelectionForwardDirectionChanged
                                                          worksheet.FocusPosStyleChanged -= AddressOf worksheet_SelectionModeChanged
                                                          worksheet.CellsFrozen -= AddressOf UpdateMenuAndToolStripsWhenAction
                                                          worksheet.Resetted -= AddressOf worksheet_Resetted
                                                          worksheet.SettingsChanged -= AddressOf worksheet_SettingsChanged
                                                          worksheet.Scaled -= AddressOf worksheet_GridScaled
                                                      End Function

            selModeNoneToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionMode, WorksheetSelectionMode.None)
            selModeCellToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionMode, WorksheetSelectionMode.Cell)
            selModeRangeToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionMode, WorksheetSelectionMode.Range)
            selModeRowToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionMode, WorksheetSelectionMode.Row)
            selModeColumnToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionMode, WorksheetSelectionMode.Column)
            selStyleNoneToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionStyle, WorksheetSelectionStyle.None)
            selStyleDefaultToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionStyle, WorksheetSelectionStyle.[Default])
            selStyleFocusRectToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionStyle, WorksheetSelectionStyle.FocusRect)
            selDirRightToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionForwardDirection, SelectionForwardDirection.Right)
            selDirDownToolStripMenuItem.Click += Function(s, e) CSharpImpl.__Assign(Sticky_Note_Form.Grid.CurrentWorksheet.SelectionForwardDirection, SelectionForwardDirection.Down)
            zoomToolStripDropDownButton.TextChanged += AddressOf zoomToolStripDropDownButton_TextChanged
            undoToolStripButton.Click += AddressOf Undo
            redoToolStripButton.Click += AddressOf Redo
            undoToolStripMenuItem.Click += AddressOf Undo
            redoToolStripMenuItem.Click += AddressOf Redo
            mergeRangeToolStripMenuItem.Click += AddressOf MergeSelectionRange
            cellMergeToolStripButton.Click += AddressOf MergeSelectionRange
            unmergeRangeToolStripMenuItem.Click += AddressOf UnmergeSelectionRange
            unmergeRangeToolStripButton.Click += AddressOf UnmergeSelectionRange
            mergeCellsToolStripMenuItem.Click += AddressOf MergeSelectionRange
            unmergeCellsToolStripMenuItem.Click += AddressOf UnmergeSelectionRange
            formatCellsToolStripMenuItem.Click += AddressOf formatCellToolStripMenuItem_Click
            resizeToolStripMenuItem.Click += AddressOf resizeToolStripMenuItem_Click
            textWrapToolStripButton.Click += AddressOf textWrapToolStripButton_Click
            Sticky_Note_Form.Grid.ActionPerformed += Function(s, e) UpdateMenuAndToolStripsWhenAction(s, e)
            Sticky_Note_Form.Grid.Undid += Function(s, e) UpdateMenuAndToolStripsWhenAction(s, e)
            Sticky_Note_Form.Grid.Redid += Function(s, e) UpdateMenuAndToolStripsWhenAction(s, e)
            rowHeightToolStripMenuItem.Click += Function(s, e)
                                                    Dim worksheet = Sticky_Note_Form.CurrentWorksheet

                                                    Using rowHeightForm As SetWidthOrHeightDialog = New SetWidthOrHeightDialog(RowOrColumn.Row)
                                                        rowHeightForm.Value = worksheet.GetRowHeight(worksheet.SelectionRange.Row)

                                                        If rowHeightForm.ShowDialog() = DialogResult.OK Then
                                                            Sticky_Note_Form.Grid.DoAction(New SetRowsHeightAction(worksheet.SelectionRange.Row, worksheet.SelectionRange.Rows, CUShort(rowHeightForm.Value)))
                                                        End If
                                                    End Using
                                                End Function

            columnWidthToolStripMenuItem.Click += Function(s, e)
                                                      Dim worksheet = Sticky_Note_Form.CurrentWorksheet

                                                      Using colWidthForm As SetWidthOrHeightDialog = New SetWidthOrHeightDialog(RowOrColumn.Column)
                                                          colWidthForm.Value = worksheet.GetColumnWidth(worksheet.SelectionRange.Col)

                                                          If colWidthForm.ShowDialog() = DialogResult.OK Then
                                                              Sticky_Note_Form.Grid.DoAction(New SetColumnsWidthAction(worksheet.SelectionRange.Col, worksheet.SelectionRange.Cols, CUShort(colWidthForm.Value)))
                                                          End If
                                                      End Using
                                                  End Function

            exportAsHtmlToolStripMenuItem.Click += Function(s, e)

                                                       Using sfd As SaveFileDialog = New SaveFileDialog()
                                                           sfd.Filter = "HTML File(*.html;*.htm)|*.html;*.htm"
                                                           sfd.FileName = "Exported ReoGrid Worksheet"

                                                           If sfd.ShowDialog() = DialogResult.OK Then

                                                               Using fs As FileStream = New FileStream(sfd.FileName, FileMode.Create)
                                                                   Sticky_Note_Form.CurrentWorksheet.ExportAsHTML(fs)
                                                               End Using

                                                               RGUtility.OpenFileOrLink(sfd.FileName)
                                                           End If
                                                       End Using
                                                   End Function

            editXMLToolStripMenuItem.Click += Function(s, e)
                                                  Dim filepath As String = Nothing

                                                  If String.IsNullOrEmpty(Sticky_Note_Form.CurrentFilePath) Then

                                                      If String.IsNullOrEmpty(currentTempFilePath) Then
                                                          currentTempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()) & ".txt")
                                                      End If

                                                      filepath = currentTempFilePath
                                                  ElseIf Not Sticky_Note_Form.CurrentFilePath.EndsWith(".rgf") AndAlso Not Sticky_Note_Form.CurrentFilePath.EndsWith(".xml") Then
                                                      MessageBox.Show(LangResource.Msg_Only_RGF_Edit_XML)
                                                      Return
                                                  Else

                                                      If MessageBox.Show(LangResource.Msg_Save_File_Immediately, "Edit XML", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Cancel Then
                                                          Return
                                                      End If

                                                      filepath = Sticky_Note_Form.CurrentFilePath
                                                  End If

                                                  Using fs = New FileStream(filepath, FileMode.Create, FileAccess.Write)
                                                      Sticky_Note_Form.CurrentWorksheet.Save(fs)
                                                  End Using

                                                  Dim p As Process = RGUtility.OpenFileOrLink("notepad.exe", filepath)
                                                  p.WaitForExit()

                                                  If p.ExitCode = 0 Then
                                                      Sticky_Note_Form.CurrentWorksheet.Load(filepath)
                                                  End If
                                              End Function

            saveToolStripButton.Click += Function(s, e) SaveDocument()
            saveToolStripMenuItem.Click += Function(s, e) SaveDocument()
            saveAsToolStripMenuItem.Click += Function(s, e) SaveAsDocument()
            groupRowsToolStripMenuItem.Click += AddressOf groupRowsToolStripMenuItem_Click
            groupRowsToolStripMenuItem1.Click += AddressOf groupRowsToolStripMenuItem_Click
            ungroupRowsToolStripMenuItem.Click += AddressOf ungroupRowsToolStripMenuItem_Click
            ungroupRowsToolStripMenuItem1.Click += AddressOf ungroupRowsToolStripMenuItem_Click
            ungroupAllRowsToolStripMenuItem.Click += AddressOf ungroupAllRowsToolStripMenuItem_Click
            ungroupAllRowsToolStripMenuItem1.Click += AddressOf ungroupAllRowsToolStripMenuItem_Click
            groupColumnsToolStripMenuItem.Click += AddressOf groupColumnsToolStripMenuItem_Click
            groupColumnsToolStripMenuItem1.Click += AddressOf groupColumnsToolStripMenuItem_Click
            ungroupColumnsToolStripMenuItem.Click += AddressOf ungroupColumnsToolStripMenuItem_Click
            ungroupColumnsToolStripMenuItem1.Click += AddressOf ungroupColumnsToolStripMenuItem_Click
            ungroupAllColumnsToolStripMenuItem.Click += AddressOf ungroupAllColumnsToolStripMenuItem_Click
            ungroupAllColumnsToolStripMenuItem1.Click += AddressOf ungroupAllColumnsToolStripMenuItem_Click
            hideRowsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.Grid.DoAction(New HideRowsAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange.Row, Sticky_Note_Form.CurrentWorksheet.SelectionRange.Rows))
            unhideRowsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.Grid.DoAction(New UnhideRowsAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange.Row, Sticky_Note_Form.CurrentWorksheet.SelectionRange.Rows))
            hideColumnsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.Grid.DoAction(New HideColumnsAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange.Col, Sticky_Note_Form.CurrentWorksheet.SelectionRange.Cols))
            unhideColumnsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.Grid.DoAction(New UnhideColumnsAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange.Col, Sticky_Note_Form.CurrentWorksheet.SelectionRange.Cols))
            freezeToCellToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.LeftTop)
            freezeToLeftToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.Left)
            freezeToTopToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.Top)
            freezeToRightToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.Right)
            freezeToBottomToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.Bottom)
            freezeToLeftTopToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.LeftTop)
            freezeToLeftBottomToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.LeftBottom)
            freezeToRightTopToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.RightTop)
            freezeToRightBottomToolStripMenuItem.Click += Function(s, e) FreezeToEdge(FreezeArea.RightBottom)
            grid.GotFocus += Function(s, e)
                                 cutToolStripButton.Enabled = CSharpImpl.__Assign(cutToolStripMenuItem.Enabled, CSharpImpl.__Assign(pasteToolStripButton.Enabled, CSharpImpl.__Assign(pasteToolStripMenuItem.Enabled, CSharpImpl.__Assign(copyToolStripButton.Enabled, CSharpImpl.__Assign(copyToolStripMenuItem.Enabled, CSharpImpl.__Assign(undoToolStripButton.Enabled, CSharpImpl.__Assign(undoToolStripMenuItem.Enabled, CSharpImpl.__Assign(redoToolStripButton.Enabled, CSharpImpl.__Assign(redoToolStripMenuItem.Enabled, CSharpImpl.__Assign(repeatLastActionToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowCutToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowCopyToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowPasteToolStripMenuItem.Enabled, CSharpImpl.__Assign(colCutToolStripMenuItem.Enabled, CSharpImpl.__Assign(colCopyToolStripMenuItem.Enabled, CSharpImpl.__Assign(colPasteToolStripMenuItem.Enabled, True))))))))))))))))
                             End Function

            grid.LostFocus += Function(s, e)
                                  cutToolStripButton.Enabled = CSharpImpl.__Assign(cutToolStripMenuItem.Enabled, CSharpImpl.__Assign(pasteToolStripButton.Enabled, CSharpImpl.__Assign(pasteToolStripMenuItem.Enabled, CSharpImpl.__Assign(copyToolStripButton.Enabled, CSharpImpl.__Assign(copyToolStripMenuItem.Enabled, CSharpImpl.__Assign(undoToolStripButton.Enabled, CSharpImpl.__Assign(undoToolStripMenuItem.Enabled, CSharpImpl.__Assign(redoToolStripButton.Enabled, CSharpImpl.__Assign(redoToolStripMenuItem.Enabled, CSharpImpl.__Assign(repeatLastActionToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowCutToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowCopyToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowPasteToolStripMenuItem.Enabled, CSharpImpl.__Assign(colCutToolStripMenuItem.Enabled, CSharpImpl.__Assign(colCopyToolStripMenuItem.Enabled, CSharpImpl.__Assign(colPasteToolStripMenuItem.Enabled, False))))))))))))))))
                              End Function

            defineNamedRangeToolStripMenuItem.Click += Function(s, e)
                                                           Dim sheet = Sticky_Note_Form.CurrentWorksheet
                                                           Dim name = sheet.GetNameByRange(sheet.SelectionRange)
                                                           Dim namedRange As NamedRange = Nothing

                                                           If Not String.IsNullOrEmpty(name) Then
                                                               namedRange = sheet.GetNamedRange(name)
                                                           End If

                                                           Using dnrf As DefineNamedRangeDialog = New DefineNamedRangeDialog()
                                                               dnrf.Range = sheet.SelectionRange

                                                               If namedRange IsNot Nothing Then
                                                                   dnrf.RangeName = name
                                                                   dnrf.Comment = namedRange.Comment
                                                               End If

                                                               If dnrf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                                                   Dim newName = dnrf.RangeName
                                                                   Dim existedRange = sheet.GetNamedRange(newName)

                                                                   If existedRange IsNot Nothing Then

                                                                       If MessageBox.Show(Me, LangRes.LangResource.Msg_Named_Range_Overwrite, Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Cancel Then
                                                                           Return
                                                                       End If

                                                                       sheet.UndefineNamedRange(newName)
                                                                   End If

                                                                   Dim range = NamedRangeManageForm.DefineNamedRange(Me, sheet, newName, dnrf.Comment, dnrf.Range)

                                                                   If Sticky_Note_Form.formulaBar IsNot Nothing AndAlso Sticky_Note_Form.formulaBar.Visible Then
                                                                       Sticky_Note_Form.formulaBar.RefreshCurrentAddress()
                                                                   End If
                                                               End If
                                                           End Using
                                                       End Function

            Sticky_Note_Form.nameManagerToolStripMenuItem.Click += Function(s, e)

                                                                       If Sticky_Note_Form.nameManagerForm Is Nothing OrElse Sticky_Note_Form.nameManagerForm.IsDisposed Then
                                                                           Sticky_Note_Form.nameManagerForm = New NamedRangeManageForm(Sticky_Note_Form.Grid)
                                                                       End If

                                                                       Sticky_Note_Form.nameManagerForm.Show(Me)
                                                                   End Function

            tracePrecedentsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.TraceCellPrecedents(Sticky_Note_Form.CurrentWorksheet.FocusPos)
            traceDependentsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.TraceCellDependents(Sticky_Note_Form.CurrentWorksheet.FocusPos)
            removeAllArrowsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.RemoveRangeAllTraceArrows(Sticky_Note_Form.CurrentWorksheet.SelectionRange)
            removePrecedentArrowsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.IterateCells(Sticky_Note_Form.CurrentWorksheet.SelectionRange, Function(r, c, cell) Sticky_Note_Form.CurrentWorksheet.RemoveCellTracePrecedents(cell))
            removeDependentArrowsToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.IterateCells(Sticky_Note_Form.CurrentWorksheet.SelectionRange, Function(r, c, cell) Sticky_Note_Form.CurrentWorksheet.RemoveCellTraceDependents(cell))
            columnPropertiesToolStripMenuItem.Click += Function(s, e)
                                                           Dim worksheet = Sticky_Note_Form.CurrentWorksheet
                                                           Dim index As Integer = worksheet.SelectionRange.Col
                                                           Dim count As Integer = worksheet.SelectionRange.Cols

                                                           Using hf = New HeaderPropertyDialog(RowOrColumn.Column)
                                                               Dim sampleHeader = worksheet.ColumnHeaders(index)
                                                               hf.HeaderText = sampleHeader.Text
                                                               hf.HeaderTextColor = If(sampleHeader.TextColor, Color.Empty)
                                                               hf.DefaultCellBody = sampleHeader.DefaultCellBody
                                                               hf.AutoFitToCell = sampleHeader.IsAutoWidth

                                                               If hf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                                                   Dim newText = If(String.IsNullOrEmpty(hf.HeaderText), Nothing, hf.HeaderText)

                                                                   For i As Integer = index To index + count - 1
                                                                       Dim header = worksheet.ColumnHeaders(i)

                                                                       If String.IsNullOrEmpty(header.Text) OrElse newText Is Nothing Then
                                                                           header.Text = newText
                                                                       End If

                                                                       header.TextColor = hf.HeaderTextColor
                                                                       header.DefaultCellBody = hf.DefaultCellBody
                                                                       header.IsAutoWidth = hf.AutoFitToCell
                                                                   Next
                                                               End If
                                                           End Using
                                                       End Function

            rowPropertiesToolStripMenuItem.Click += Function(s, e)
                                                        Dim sheet = Sticky_Note_Form.Grid.CurrentWorksheet
                                                        Dim index As Integer = sheet.SelectionRange.Row
                                                        Dim count As Integer = sheet.SelectionRange.Rows

                                                        Using hpf = New HeaderPropertyDialog(RowOrColumn.Row)
                                                            Dim sampleHeader = sheet.RowHeaders(index)
                                                            hpf.HeaderText = sampleHeader.Text
                                                            hpf.HeaderTextColor = If(sampleHeader.TextColor, Color.Empty)
                                                            hpf.DefaultCellBody = sampleHeader.DefaultCellBody
                                                            hpf.RowHeaderWidth = sheet.RowHeaderWidth
                                                            hpf.AutoFitToCell = sampleHeader.IsAutoHeight

                                                            If hpf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                                                Dim newText = If(String.IsNullOrEmpty(hpf.HeaderText), Nothing, hpf.HeaderText)

                                                                For i As Integer = index To index + count - 1
                                                                    Dim header = sheet.RowHeaders(i)

                                                                    If String.IsNullOrEmpty(header.Text) OrElse newText Is Nothing Then
                                                                        header.Text = newText
                                                                    End If

                                                                    header.TextColor = hpf.HeaderTextColor
                                                                    header.DefaultCellBody = hpf.DefaultCellBody
                                                                    header.IsAutoHeight = hpf.AutoFitToCell
                                                                Next

                                                                If hpf.RowHeaderWidth <> sheet.RowHeaderWidth Then
                                                                    sheet.RowHeaderWidth = hpf.RowHeaderWidth
                                                                End If
                                                            End If
                                                        End Using
                                                    End Function

            rowCutToolStripMenuItem.Click += AddressOf Sticky_Note_Form.cutRangeToolStripMenuItem_Click
            rowCopyToolStripMenuItem.Click += AddressOf Sticky_Note_Form.copyRangeToolStripMenuItem_Click
            rowPasteToolStripMenuItem.Click += AddressOf Sticky_Note_Form.pasteRangeToolStripMenuItem_Click
            colCutToolStripMenuItem.Click += AddressOf Sticky_Note_Form.cutRangeToolStripMenuItem_Click
            colCopyToolStripMenuItem.Click += AddressOf Sticky_Note_Form.copyRangeToolStripMenuItem_Click
            colPasteToolStripMenuItem.Click += AddressOf Sticky_Note_Form.pasteRangeToolStripMenuItem_Click
            rowFormatCellsToolStripMenuItem.Click += AddressOf Sticky_Note_Form.formatCellToolStripMenuItem_Click
            colFormatCellsToolStripMenuItem.Click += AddressOf Sticky_Note_Form.formatCellToolStripMenuItem_Click
            printSettingsToolStripMenuItem.Click += AddressOf Sticky_Note_Form.printSettingsToolStripMenuItem_Click
            printToolStripMenuItem.Click += AddressOf PrintToolStripMenuItem_Click
            Dim noneTypeMenuItem = New ToolStripMenuItem(LangResource.None)
            noneTypeMenuItem.Click += AddressOf cellTypeNoneMenuItem_Click
            changeCellsTypeToolStripMenuItem.DropDownItems.Add(noneTypeMenuItem)
            changeCellsTypeToolStripMenuItem.DropDownItems.Add(New ToolStripSeparator())
            Dim noneTypeMenuItem2 = New ToolStripMenuItem(LangResource.None)
            noneTypeMenuItem2.Click += AddressOf cellTypeNoneMenuItem_Click
            changeCellsTypeToolStripMenuItem2.DropDownItems.Add(noneTypeMenuItem2)
            changeCellsTypeToolStripMenuItem2.DropDownItems.Add(New ToolStripSeparator())

            For Each cellType In CellTypesManager.CellTypes
                Dim name = cellType.Key
                If nasticky_Note_Form.EndsWith("Cell") Then name = nasticky_Note_Form.Substring(0, nasticky_Note_Form.Length - 4)
                Dim menuItem = New ToolStripMenuItem(name) With {
                    .Tag = cellType.Value
                }
                menuItem.Click += AddressOf cellTypeMenuItem_Click
                changeCellsTypeToolStripMenuItem.DropDownItems.Add(menuItem)
                Dim menuItem2 = New ToolStripMenuItem(name) With {
                    .Tag = cellType.Value
                }
                menuItem2.Click += AddressOf cellTypeMenuItem_Click
                changeCellsTypeToolStripMenuItem2.DropDownItems.Add(menuItem2)
            Next

            rowContextMenuStrip.Opening += Function(s, e)
                                               insertRowPageBreakToolStripMenuItem.Enabled = Not Sticky_Note_Form.Grid.CurrentWorksheet.PrintableRange.IsEmpty
                                               removeRowPageBreakToolStripMenuItem.Enabled = Sticky_Note_Form.Grid.CurrentWorksheet.RowPageBreaks.Contains(Sticky_Note_Form.Grid.CurrentWorksheet.FocusPos.Row)
                                           End Function

            columnContextMenuStrip.Opening += Function(s, e)
                                                  insertColPageBreakToolStripMenuItem.Enabled = Not Sticky_Note_Form.Grid.CurrentWorksheet.PrintableRange.IsEmpty
                                                  removeColPageBreakToolStripMenuItem.Enabled = Sticky_Note_Form.Grid.CurrentWorksheet.ColumnPageBreaks.Contains(Sticky_Note_Form.Grid.CurrentWorksheet.FocusPos.Col)
                                              End Function

            Sticky_Note_Form.AutoFunctionSumToolStripMenuItem.Click += Function(s, e) ApplyFunctionToSelectedRange("SUM")
            Sticky_Note_Form.AutoFunctionAverageToolStripMenuItem.Click += Function(s, e) ApplyFunctionToSelectedRange("AVERAGE")
            Sticky_Note_Form.AutoFunctionCountToolStripMenuItem.Click += Function(s, e) ApplyFunctionToSelectedRange("COUNT")
            Sticky_Note_Form.AutoFunctionMaxToolStripMenuItem.Click += Function(s, e) ApplyFunctionToSelectedRange("MAX")
            Sticky_Note_Form.AutoFunctionMinToolStripMenuItem.Click += Function(s, e) ApplyFunctionToSelectedRange("MIN")
            Sticky_Note_Form.focusStyleDefaultToolStripMenuItem.CheckedChanged += Function(s, e)
                                                                                      If Sticky_Note_Form.focusStyleDefaultToolStripMenuItem.Checked Then Sticky_Note_Form.CurrentWorksheet.FocusPosStyle = FocusPosStyle.[Default]
                                                                                  End Function

            Sticky_Note_Form.focusStyleNoneToolStripMenuItem.CheckedChanged += Function(s, e)
                                                                                   If focusStyleNoneToolStripMenuItem.Checked Then Sticky_Note_Form.CurrentWorksheet.FocusPosStyle = FocusPosStyle.None
                                                                               End Function

            scriptEditorToolStripMenuItem.Click += Function(s, e)
                                                       MessageBox.Show("Script execution is not supported by this edition.", Application.ProductName)
                                                   End Function

            homepageToolStripMenuItem.Click += Function(s, e)

                                                   Try
                                                       RGUtility.OpenFileOrLink(LangResource.HP_Homepage)
                                                   Catch
                                                   End Try
                                               End Function

            documentationToolStripMenuItem.Click += Function(s, e)

                                                        Try
                                                            RGUtility.OpenFileOrLink(LangResource.HP_Homepage_Document)
                                                        Catch
                                                        End Try
                                                    End Function

            insertColPageBreakToolStripMenuItem.Click += AddressOf insertColPageBreakToolStripMenuItem_Click
            insertRowPageBreakToolStripMenuItem.Click += AddressOf insertRowPageBreakToolStripMenuItem_Click
            removeColPageBreakToolStripMenuItem.Click += AddressOf removeColPageBreakToolStripMenuItem_Click
            removeRowPageBreakToolStripMenuItem.Click += AddressOf removeRowPageBreakToolStripMenuItem_Click
            filterToolStripMenuItem.Click += AddressOf filterToolStripMenuItem_Click
            clearFilterToolStripMenuItem.Click += AddressOf clearFilterToolStripMenuItem_Click
            columnFilterToolStripMenuItem.Click += AddressOf filterToolStripMenuItem_Click
            clearColumnFilterToolStripMenuItem.Click += AddressOf clearFilterToolStripMenuItem_Click
            Sticky_Note_Form.Grid.ExceptionHappened += Function(s, e)

                                                           If TypeOf e.Exception Is RangeIntersectionException Then
                                                               MessageBox.Show(Me, LangResource.Msg_Range_Intersection_Exception, "ReoGrid Editor", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
                                                           ElseIf TypeOf e.Exception Is OperationOnReadonlyCellException Then
                                                               MessageBox.Show(Me, LangResource.Msg_Operation_Aborted, "ReoGrid Editor", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
                                                           End If
                                                       End Function

            Sticky_Note_Form.Grid.CurrentWorksheetChanged += Function(s, e)
                                                                 UpdateMenuAndToolStrips()
                                                                 worksheet_GridScaled(Sticky_Note_Form.CurrentWorksheet, e)
                                                                 UpdateWorksheetSettings(Sticky_Note_Form.Grid.CurrentWorksheet)
                                                                 UpdateSelectionModeAndStyle()
                                                                 UpdateSelectionForwardDirection()
                                                             End Function

            Sticky_Note_Form.Grid.SettingsChanged += Function(s, e)
                                                         sheetSwitcherToolStripMenuItem.Checked = Sticky_Note_Form.Grid.HasSettings(WorkbookSettings.View_ShowSheetTabControl)
                                                         showHorizontaScrolllToolStripMenuItem.Checked = Sticky_Note_Form.Grid.HasSettings(WorkbookSettings.View_ShowHorScroll)
                                                         showVerticalScrollbarToolStripMenuItem.Checked = Sticky_Note_Form.Grid.HasSettings(WorkbookSettings.View_ShowVerScroll)
                                                     End Function

            Sticky_Note_Form.clearAllToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.All)
            Sticky_Note_Form.clearDataToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.Data)
            Sticky_Note_Form.clearDataFormatToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.DataFormat)
            Sticky_Note_Form.clearFormulaToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.Formula)
            Sticky_Note_Form.clearCellBodyToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.Body)
            Sticky_Note_Form.clearStylesToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.Style)
            Sticky_Note_Form.clearBordersToolStripButton.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.ClearRangeContent(Sticky_Note_Form.CurrentSelectionRange, CellElementFlag.Border)
            Sticky_Note_Form.exportCurrentWorksheetToolStripMenuItem.Click += Function(s, e) ExportAsCsv(RangePosition.EntireRange)
            Sticky_Note_Form.exportSelectedRangeToolStripMenuItem.Click += Function(s, e) ExportAsCsv(CurrentSelectionRange)
            Sticky_Note_Form.dragToMoveRangeToolStripMenuItem.CheckedChanged += Function(s, e) CurrentWorksheet.SetSettings(WorksheetSettings.Edit_DragSelectionToMoveCells, Sticky_Note_Form.dragToMoveRangeToolStripMenuItem.Checked)
            Sticky_Note_Form.dragToFillSerialToolStripMenuItem.CheckedChanged += Function(s, e) CurrentWorksheet.SetSettings(WorksheetSettings.Edit_DragSelectionToFillSerial, Sticky_Note_Form.dragToFillSerialToolStripMenuItem.Checked)
            Sticky_Note_Form.suspendReferenceUpdatingToolStripMenuItem.CheckedChanged += Function(s, e) Sticky_Note_Form.CurrentWorksheet.SetSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell, Not Sticky_Note_Form.suspendReferenceUpdatingToolStripMenuItem.Checked)
            Sticky_Note_Form.recalculateWorksheetToolStripMenuItem.Click += Function(s, e) Sticky_Note_Form.CurrentWorksheet.Recalculate()
            ResumeLayout()
        End Sub

        Private Sub ExportAsCsv(ByVal range As RangePosition)
            Using dlg As SaveFileDialog = New SaveFileDialog()
                dlg.Filter = LangResource.Filter_Export_As_CSV
                dlg.FileName = Path.GetFileNameWithoutExtension(Sticky_Note_Form.CurrentFilePath)

                If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                    Using fs As FileStream = New FileStream(dlg.FileName, FileMode.Create, FileAccess.Write, FileShare.Read)
                        CurrentWorksheet.ExportAsCSV(fs, range)
                    End Using
                End If
            End Using
        End Sub

        Private Sub worksheet_SelectionModeChanged(ByVal sender As Object, ByVal e As EventArgs)
            UpdateSelectionModeAndStyle()
        End Sub

        Private Sub worksheet_SelectionForwardDirectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            UpdateSelectionForwardDirection()
        End Sub

        Private Sub worksheet_Resetted(ByVal sender As Object, ByVal e As EventArgs)
            statusToolStripStatusLabel.Text = String.Empty
        End Sub

        Private Sub worksheet_SettingsChanged(ByVal sender As Object, ByVal e As SettingsChangedEventArgs)
            Dim worksheet = TryCast(sender, Worksheet)
            If worksheet IsNot Nothing Then UpdateWorksheetSettings(worksheet)
        End Sub

        Private Sub UpdateWorksheetSettings(ByVal sheet As Worksheet)
            Dim visible As Boolean = False
            visible = sheet.HasSettings(WorksheetSettings.View_ShowGridLine)
            If showGridLinesToolStripMenuItem.Checked <> visible Then showGridLinesToolStripMenuItem.Checked = visible
            visible = sheet.HasSettings(WorksheetSettings.View_ShowPageBreaks)
            If showPageBreakToolStripMenuItem.Checked <> visible Then showPageBreakToolStripMenuItem.Checked = visible
            visible = sheet.HasSettings(WorksheetSettings.View_ShowFrozenLine)
            If showFrozenLineToolStripMenuItem.Checked <> visible Then showFrozenLineToolStripMenuItem.Checked = visible
            visible = sheet.HasSettings(WorksheetSettings.View_ShowRowHeader)
            If showRowHeaderToolStripMenuItem.Checked <> visible Then showRowHeaderToolStripMenuItem.Checked = visible
            visible = sheet.HasSettings(WorksheetSettings.View_ShowColumnHeader)
            If showColumnHeaderToolStripMenuItem.Checked <> visible Then showColumnHeaderToolStripMenuItem.Checked = visible
            visible = sheet.HasSettings(WorksheetSettings.View_AllowShowRowOutlines)
            If showRowOutlineToolStripMenuItem.Checked <> visible Then showRowOutlineToolStripMenuItem.Checked = visible
            visible = sheet.HasSettings(WorksheetSettings.View_AllowShowColumnOutlines)
            If showColumnOutlineToolStripMenuItem.Checked <> visible Then showColumnOutlineToolStripMenuItem.Checked = visible
            Dim check = sheet.HasSettings(WorksheetSettings.Edit_DragSelectionToMoveCells)
            If Sticky_Note_Form.dragToMoveRangeToolStripMenuItem.Checked <> check Then Sticky_Note_Form.dragToMoveRangeToolStripMenuItem.Checked = check
            check = sheet.HasSettings(WorksheetSettings.Edit_DragSelectionToFillSerial)
            If Sticky_Note_Form.dragToFillSerialToolStripMenuItem.Checked <> check Then Sticky_Note_Form.dragToFillSerialToolStripMenuItem.Checked = check
            check = Not sheet.HasSettings(WorksheetSettings.Formula_AutoUpdateReferenceCell)
            If Sticky_Note_Form.suspendReferenceUpdatingToolStripMenuItem.Checked <> check Then Sticky_Note_Form.suspendReferenceUpdatingToolStripMenuItem.Checked = check
            sheetReadonlyToolStripMenuItem.Checked = sheet.HasSettings(WorksheetSettings.Edit_Readonly)
        End Sub

        Private Sub cellTypeNoneMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet

            If worksheet IsNot Nothing Then
                worksheet.IterateCells(worksheet.SelectionRange, False, Function(r, c, cell)
                                                                            cell.Body = Nothing
                                                                            Return True
                                                                        End Function)
            End If
        End Sub

        Private Sub cellTypeMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet
            Dim menuItem = TryCast(sender, ToolStripMenuItem)

            If menuItem IsNot Nothing AndAlso TypeOf menuItem.Tag Is Type AndAlso worksheet IsNot Nothing AndAlso Not worksheet.SelectionRange.IsEmpty Then

                For Each cell In worksheet.Ranges(worksheet.SelectionRange).Cells
                    cell.Body = TryCast(System.Activator.CreateInstance(CType(menuItem.Tag, Type)), ICellBody)
                Next
            End If
        End Sub

        Private Sub textWrapToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentSelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.TextWrap,
                .TextWrapMode = If(textWrapToolStripButton.Checked, TextWrapMode.WordBreak, TextWrapMode.NoWrap)
            }))
        End Sub

        Private Sub worksheet_GridScaled(ByVal sender As Object, ByVal e As EventArgs)
            Dim worksheet = CType(sender, Worksheet)
            zoomToolStripDropDownButton.Text = worksheet.ScaleFactor * 100 & "%"
        End Sub

        Public ReadOnly Property GridControl As ReoGridControl
            Get
                Return Sticky_Note_Form.Grid
            End Get
        End Property

        Public ReadOnly Property CurrentWorksheet As Worksheet
            Get
                Return Sticky_Note_Form.Grid.CurrentWorksheet
            End Get
        End Property

        Public Property CurrentSelectionRange As RangePosition
            Get
                Return Sticky_Note_Form.Grid.CurrentWorksheet.SelectionRange
            End Get
            Set(ByVal value As RangePosition)
                Sticky_Note_Form.Grid.CurrentWorksheet.SelectionRange = value
            End Set
        End Property

        Friend Sub ShowStatus(ByVal msg As String)
            ShowStatus(msg, False)
        End Sub

        Friend Sub ShowStatus(ByVal msg As String, ByVal [error] As Boolean)
            statusToolStripStatusLabel.Text = msg
            statusToolStripStatusLabel.ForeColor = If([error], Color.Red, SystemColors.WindowText)
        End Sub

        Public Sub ShowError(ByVal msg As String)
            ShowStatus(msg, True)
        End Sub

        Private Sub UpdateMenuAndToolStripsWhenAction(ByVal sender As Object, ByVal e As EventArgs)
            UpdateMenuAndToolStrips()
        End Sub

        Private Sub Undo(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.Undo()
        End Sub

        Private Sub Redo(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.Redo()
        End Sub

        Private Sub zoomToolStripDropDownButton_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If isUIUpdating Then Return

            If zoomToolStripDropDownButton.Text.Length > 0 Then
                Dim value As Integer = 100

                If Integer.TryParse(zoomToolStripDropDownButton.Text.Substring(0, zoomToolStripDropDownButton.Text.Length - 1), value) Then
                    Dim scale As Single = CSng(value) / 100.0F
                    scale = CSng(Math.Round(scale, 1))
                    Sticky_Note_Form.CurrentWorksheet.SetScale(scale)
                End If
            End If
        End Sub

        Private Sub grid_SelectionRangeChanged(ByVal sender As Object, ByVal e As RangeEventArgs)
            Dim worksheet = TryCast(sender, Worksheet)

            If worksheet = Sticky_Note_Form.CurrentWorksheet Then

                If worksheet.SelectionRange = RangePosition.Empty Then
                    rangeInfoToolStripStatusLabel.Text = "Selection None"
                Else
                    rangeInfoToolStripStatusLabel.Text = String.Format("{0} {1} x {2}", worksheet.SelectionRange.ToString(), worksheet.SelectionRange.Rows, worksheet.SelectionRange.Cols)
                End If

                UpdateMenuAndToolStrips()
            End If
        End Sub

        Private Sub SetupUILanguage()
            Sticky_Note_Form.fileToolStripMenuItem.Text = LangResource.Menu_File
            Sticky_Note_Form.newToolStripMenuItem.Text = LangResource.Menu_File_New
            Sticky_Note_Form.newWindowToolStripMenuItem.Text = LangResource.Menu_File_New_Window
            Sticky_Note_Form.openToolStripMenuItem.Text = LangResource.Menu_File_Open
            Sticky_Note_Form.saveToolStripMenuItem.Text = LangResource.Menu_File_Save
            Sticky_Note_Form.saveAsToolStripMenuItem.Text = LangResource.Menu_File_Save_As
            Sticky_Note_Form.editXMLToolStripMenuItem.Text = LangResource.Menu_File_Edit_RGF_XML
            Sticky_Note_Form.exportAsHtmlToolStripMenuItem.Text = LangResource.Menu_File_Export_As_HTML
            Sticky_Note_Form.exportAsCSVToolStripMenuItem.Text = LangResource.Menu_File_Export_As_CSV
            Sticky_Note_Form.exportSelectedRangeToolStripMenuItem.Text = LangResource.Menu_File_Export_As_CSV_Selected_Range
            Sticky_Note_Form.exportCurrentWorksheetToolStripMenuItem.Text = LangResource.Menu_File_Export_As_CSV_Current_Worksheet
            Sticky_Note_Form.printPreviewToolStripMenuItem.Text = LangResource.Menu_File_Print_Preview
            Sticky_Note_Form.printSettingsToolStripMenuItem.Text = LangResource.Menu_File_Print_Settings
            Sticky_Note_Form.printToolStripMenuItem.Text = LangResource.Menu_File_Print
            Sticky_Note_Form.exitToolStripMenuItem.Text = LangResource.Menu_File_Exit
            Sticky_Note_Form.editToolStripMenuItem.Text = LangResource.Menu_Edit
            Sticky_Note_Form.undoToolStripMenuItem.Text = LangResource.Menu_Undo
            Sticky_Note_Form.redoToolStripMenuItem.Text = LangResource.Menu_Redo
            Sticky_Note_Form.repeatLastActionToolStripMenuItem.Text = LangResource.Menu_Edit_Repeat_Last_Action
            Sticky_Note_Form.cutToolStripMenuItem.Text = LangResource.Menu_Cut
            Sticky_Note_Form.copyToolStripMenuItem.Text = LangResource.Menu_Copy
            Sticky_Note_Form.pasteToolStripMenuItem.Text = LangResource.Menu_Paste
            Sticky_Note_Form.clearToolStripMenuItem.Text = LangResource.Menu_Edit_Clear
            Sticky_Note_Form.clearAllToolStripMenuItem.Text = LangResource.All
            Sticky_Note_Form.clearDataToolStripMenuItem.Text = LangResource.Data
            Sticky_Note_Form.clearDataFormatToolStripMenuItem.Text = LangResource.Data_Format
            Sticky_Note_Form.clearFormulaToolStripMenuItem.Text = LangResource.Formula
            Sticky_Note_Form.clearCellBodyToolStripMenuItem.Text = LangResource.CellBody
            Sticky_Note_Form.clearStylesToolStripMenuItem.Text = LangResource.Style
            Sticky_Note_Form.clearBordersToolStripMenuItem.Text = LangResource.Border
            Sticky_Note_Form.focusCellStyleToolStripMenuItem.Text = LangResource.Menu_Edit_Focus_Cell_Style
            Sticky_Note_Form.focusStyleDefaultToolStripMenuItem.Text = LangResource.[Default]
            Sticky_Note_Form.focusStyleNoneToolStripMenuItem.Text = LangResource.None
            Sticky_Note_Form.selectionToolStripMenuItem.Text = LangResource.Menu_Edit_Selection
            Sticky_Note_Form.dragToMoveRangeToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Drag_To_Move_Content
            Sticky_Note_Form.dragToFillSerialToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Drag_To_Fill_Serial
            Sticky_Note_Form.selectionStyleToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Style
            Sticky_Note_Form.selStyleDefaultToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Style_Default
            Sticky_Note_Form.selStyleFocusRectToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Style_Focus_Rect
            Sticky_Note_Form.selStyleNoneToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Style_None
            Sticky_Note_Form.selectionModeToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Mode
            Sticky_Note_Form.selModeNoneToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Mode_None
            Sticky_Note_Form.selModeCellToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Mode_Cell
            Sticky_Note_Form.selModeRangeToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Mode_Range
            Sticky_Note_Form.selModeRowToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Mode_Row
            Sticky_Note_Form.selModeColumnToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Mode_Column
            Sticky_Note_Form.selectionMoveDirectionToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Move_Direction
            Sticky_Note_Form.selDirRightToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Move_Direction_Right
            Sticky_Note_Form.selDirDownToolStripMenuItem.Text = LangResource.Menu_Edit_Selection_Move_Direction_Down
            Sticky_Note_Form.selectAllToolStripMenuItem.Text = LangResource.Menu_Edit_Select_All
            Sticky_Note_Form.viewToolStripMenuItem.Text = LangResource.Menu_View
            Sticky_Note_Form.componentsToolStripMenuItem.Text = LangResource.Menu_View_Components
            Sticky_Note_Form.toolbarToolStripMenuItem.Text = LangResource.Menu_View_Components_Toolbar
            Sticky_Note_Form.formulaBarToolStripMenuItem.Text = LangResource.Menu_View_Components_FormulaBar
            Sticky_Note_Form.statusBarToolStripMenuItem.Text = LangResource.Menu_View_Components_StatusBar
            Sticky_Note_Form.visibleToolStripMenuItem.Text = LangResource.Menu_View_Visible
            Sticky_Note_Form.showGridLinesToolStripMenuItem.Text = LangResource.Menu_View_Visible_Grid_Lines
            Sticky_Note_Form.showPageBreakToolStripMenuItem.Text = LangResource.Menu_View_Visible_Page_Breaks
            Sticky_Note_Form.showFrozenLineToolStripMenuItem.Text = LangResource.Menu_View_Visible_Forzen_Line
            Sticky_Note_Form.sheetSwitcherToolStripMenuItem.Text = LangResource.Menu_View_Visible_Sheet_Tab
            Sticky_Note_Form.showHorizontaScrolllToolStripMenuItem.Text = LangResource.Menu_View_Visible_Horizontal_ScrollBar
            Sticky_Note_Form.showVerticalScrollbarToolStripMenuItem.Text = LangResource.Menu_View_Visible_Vertical_ScrollBar
            Sticky_Note_Form.showRowHeaderToolStripMenuItem.Text = LangResource.Menu_View_Visible_Row_Header
            Sticky_Note_Form.showColumnHeaderToolStripMenuItem.Text = LangResource.Menu_View_Visible_Column_Header
            Sticky_Note_Form.showRowOutlineToolStripMenuItem.Text = LangResource.Menu_View_Visible_Row_Outline_Panel
            Sticky_Note_Form.showColumnOutlineToolStripMenuItem.Text = LangResource.Menu_View_Visible_Column_Outline_Panel
            Sticky_Note_Form.resetAllPageBreaksToolStripMenuItem.Text = LangResource.Menu_Reset_All_Page_Breaks
            Sticky_Note_Form.freezeToCellToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Cell
            Sticky_Note_Form.freezeToSpecifiedEdgeToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges
            Sticky_Note_Form.freezeToLeftToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Left
            Sticky_Note_Form.freezeToRightToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Right
            Sticky_Note_Form.freezeToTopToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Top
            Sticky_Note_Form.freezeToBottomToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Bottom
            Sticky_Note_Form.freezeToLeftTopToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Top_Left
            Sticky_Note_Form.freezeToLeftBottomToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Bottom_Left
            Sticky_Note_Form.freezeToRightTopToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Top_Right
            Sticky_Note_Form.freezeToRightBottomToolStripMenuItem.Text = LangResource.Menu_View_Freeze_To_Edges_Bottom_Right
            Sticky_Note_Form.unfreezeToolStripMenuItem.Text = LangResource.Menu_View_Unfreeze
            Sticky_Note_Form.cellsToolStripMenuItem.Text = LangResource.Menu_Cells
            Sticky_Note_Form.mergeCellsToolStripMenuItem.Text = LangResource.Menu_Cells_Merge_Cells
            Sticky_Note_Form.unmergeCellsToolStripMenuItem.Text = LangResource.Menu_Cells_Unmerge_Cells
            Sticky_Note_Form.changeCellsTypeToolStripMenuItem.Text = LangResource.Menu_Change_Cells_Type
            Sticky_Note_Form.formatCellsToolStripMenuItem.Text = LangResource.Menu_Format_Cells
            Sticky_Note_Form.sheetToolStripMenuItem.Text = LangResource.Menu_Sheet
            Sticky_Note_Form.filterToolStripMenuItem.Text = LangResource.Menu_Sheet_Filter
            Sticky_Note_Form.clearFilterToolStripMenuItem.Text = LangResource.Menu_Sheet_Clear_Filter
            Sticky_Note_Form.groupToolStripMenuItem.Text = LangResource.Menu_Sheet_Group
            Sticky_Note_Form.groupRowsToolStripMenuItem.Text = LangResource.Menu_Sheet_Group_Rows
            Sticky_Note_Form.groupColumnsToolStripMenuItem.Text = LangResource.Menu_Sheet_Group_Columns
            Sticky_Note_Form.ungroupToolStripMenuItem.Text = LangResource.Menu_Sheet_Ungroup
            Sticky_Note_Form.ungroupRowsToolStripMenuItem.Text = LangResource.Menu_Sheet_Ungroup_Selection_Rows
            Sticky_Note_Form.ungroupAllRowsToolStripMenuItem.Text = LangResource.Menu_Sheet_Ungroup_All_Rows
            Sticky_Note_Form.ungroupColumnsToolStripMenuItem.Text = LangResource.Menu_Sheet_Ungroup_Selection_Columns
            Sticky_Note_Form.ungroupAllColumnsToolStripMenuItem.Text = LangResource.Menu_Sheet_Ungroup_All_Columns
            Sticky_Note_Form.insertToolStripMenuItem.Text = LangResource.Menu_Sheet_Insert
            Sticky_Note_Form.resizeToolStripMenuItem.Text = LangResource.Menu_Sheet_Resize
            Sticky_Note_Form.sheetReadonlyToolStripMenuItem.Text = LangResource.Menu_Edit_Readonly
            Sticky_Note_Form.formulaToolStripMenuItem.Text = LangResource.Menu_Formula
            Sticky_Note_Form.autoFunctionToolStripMenuItem.Text = LangResource.Menu_Formula_Auto_Function
            Sticky_Note_Form.defineNamedRangeToolStripMenuItem.Text = LangResource.Menu_Formula_Define_Name
            Sticky_Note_Form.nameManagerToolStripMenuItem.Text = LangResource.Menu_Formula_Name_Manager
            Sticky_Note_Form.tracePrecedentsToolStripMenuItem.Text = LangResource.Menu_Formula_Trace_Precedents
            Sticky_Note_Form.traceDependentsToolStripMenuItem.Text = LangResource.Menu_Formula_Trace_Dependents
            Sticky_Note_Form.removeArrowsToolStripMenuItem.Text = LangResource.Menu_Formula_Remove_Trace_Arrows
            Sticky_Note_Form.removeAllArrowsToolStripMenuItem.Text = LangResource.Menu_Formula_Remove_Trace_Arrows_Remove_All_Arrows
            Sticky_Note_Form.removePrecedentArrowsToolStripMenuItem.Text = LangResource.Menu_Formula_Remove_Trace_Arrows_Remove_Precedent_Arrows
            Sticky_Note_Form.removeDependentArrowsToolStripMenuItem.Text = LangResource.Menu_Formula_Remove_Trace_Arrows_Remove_Dependent_Arrows
            Sticky_Note_Form.suspendReferenceUpdatingToolStripMenuItem.Text = LangResource.Menu_Formula_Suspend_Reference_Updates
            Sticky_Note_Form.recalculateWorksheetToolStripMenuItem.Text = LangResource.Menu_Formula_Recalculate_Worksheet
            Sticky_Note_Form.scriptToolStripMenuItem.Text = LangResource.Menu_Script
            Sticky_Note_Form.scriptEditorToolStripMenuItem.Text = LangResource.Menu_Script_Script_Editor
            Sticky_Note_Form.runFunctionToolStripMenuItem.Text = LangResource.Menu_Script_Run_Function
            Sticky_Note_Form.toolsToolStripMenuItem.Text = LangResource.Menu_Tools
            Sticky_Note_Form.controlStyleToolStripMenuItem.Text = LangResource.Menu_Tools_Control_Appearance
            Sticky_Note_Form.helpToolStripMenuItem.Text = LangResource.Menu_Help
            Sticky_Note_Form.homepageToolStripMenuItem.Text = LangResource.Menu_Help_Homepage
            Sticky_Note_Form.documentationToolStripMenuItem.Text = LangResource.Menu_Help_Documents
            Sticky_Note_Form.aboutToolStripMenuItem.Text = LangResource.Menu_Help_About
            Sticky_Note_Form.colCutToolStripMenuItem.Text = LangResource.Menu_Cut
            Sticky_Note_Form.colCopyToolStripMenuItem.Text = LangResource.Menu_Copy
            Sticky_Note_Form.colPasteToolStripMenuItem.Text = LangResource.Menu_Paste
            Sticky_Note_Form.insertColToolStripMenuItem.Text = LangResource.CtxMenu_Col_Insert_Columns
            Sticky_Note_Form.deleteColumnToolStripMenuItem.Text = LangResource.CtxMenu_Col_Delete_Columns
            Sticky_Note_Form.resetToDefaultWidthToolStripMenuItem.Text = LangResource.CtxMenu_Col_Reset_To_Default_Width
            Sticky_Note_Form.columnWidthToolStripMenuItem.Text = LangResource.CtxMenu_Col_Column_Width
            Sticky_Note_Form.hideColumnsToolStripMenuItem.Text = LangResource.Menu_Hide
            Sticky_Note_Form.unhideColumnsToolStripMenuItem.Text = LangResource.Menu_Unhide
            Sticky_Note_Form.columnFilterToolStripMenuItem.Text = LangResource.CtxMenu_Col_Filter
            Sticky_Note_Form.clearColumnFilterToolStripMenuItem.Text = LangResource.CtxMenu_Col_Clear_Filter
            Sticky_Note_Form.groupColumnsToolStripMenuItem1.Text = LangResource.Menu_Group
            Sticky_Note_Form.ungroupColumnsToolStripMenuItem1.Text = LangResource.Menu_Ungroup
            Sticky_Note_Form.ungroupAllColumnsToolStripMenuItem1.Text = LangResource.Menu_Ungroup_All
            Sticky_Note_Form.insertColPageBreakToolStripMenuItem.Text = LangResource.Menu_Insert_Page_Break
            Sticky_Note_Form.removeColPageBreakToolStripMenuItem.Text = LangResource.Menu_Remove_Page_Break
            Sticky_Note_Form.columnPropertiesToolStripMenuItem.Text = LangResource.Menu_Property
            Sticky_Note_Form.colFormatCellsToolStripMenuItem.Text = LangResource.Menu_Format_Cells
            Sticky_Note_Form.rowCutToolStripMenuItem.Text = LangResource.Menu_Cut
            Sticky_Note_Form.rowCopyToolStripMenuItem.Text = LangResource.Menu_Copy
            Sticky_Note_Form.rowPasteToolStripMenuItem.Text = LangResource.Menu_Paste
            Sticky_Note_Form.insertRowToolStripMenuItem.Text = LangResource.CtxMenu_Row_Insert_Rows
            Sticky_Note_Form.deleteRowsToolStripMenuItem.Text = LangResource.CtxMenu_Row_Delete_Rows
            Sticky_Note_Form.resetToDefaultHeightToolStripMenuItem.Text = LangResource.CtxMenu_Row_Reset_to_Default_Height
            Sticky_Note_Form.rowHeightToolStripMenuItem.Text = LangResource.CtxMenu_Row_Row_Height
            Sticky_Note_Form.hideRowsToolStripMenuItem.Text = LangResource.Menu_Hide
            Sticky_Note_Form.unhideRowsToolStripMenuItem.Text = LangResource.Menu_Unhide
            Sticky_Note_Form.groupRowsToolStripMenuItem1.Text = LangResource.Menu_Group
            Sticky_Note_Form.ungroupRowsToolStripMenuItem1.Text = LangResource.Menu_Ungroup
            Sticky_Note_Form.ungroupAllRowsToolStripMenuItem1.Text = LangResource.Menu_Ungroup_All
            Sticky_Note_Form.insertRowPageBreakToolStripMenuItem.Text = LangResource.Menu_Insert_Page_Break
            Sticky_Note_Form.removeRowPageBreakToolStripMenuItem.Text = LangResource.Menu_Remove_Page_Break
            Sticky_Note_Form.rowPropertiesToolStripMenuItem.Text = LangResource.Menu_Property
            Sticky_Note_Form.rowFormatCellsToolStripMenuItem.Text = LangResource.Menu_Format_Cells
            Sticky_Note_Form.cutRangeToolStripMenuItem.Text = LangResource.Menu_Cut
            Sticky_Note_Form.copyRangeToolStripMenuItem.Text = LangResource.Menu_Copy
            Sticky_Note_Form.pasteRangeToolStripMenuItem.Text = LangResource.Menu_Paste
            Sticky_Note_Form.mergeRangeToolStripMenuItem.Text = LangResource.CtxMenu_Cell_Merge
            Sticky_Note_Form.unmergeRangeToolStripMenuItem.Text = LangResource.CtxMenu_Cell_Unmerge
            Sticky_Note_Form.changeCellsTypeToolStripMenuItem2.Text = LangResource.Menu_Change_Cells_Type
            Sticky_Note_Form.formatCellToolStripMenuItem.Text = LangResource.Menu_Format_Cells
            Sticky_Note_Form.resetAllPageBreaksToolStripMenuItem1.Text = LangResource.Menu_Reset_All_Page_Breaks
        End Sub

        Private cultureEN_US As System.Globalization.CultureInfo
        Private cultureJP_JP As System.Globalization.CultureInfo
        Private cultureZH_CN As System.Globalization.CultureInfo

        Public Sub ChangeLanguageToEnglish()
            If cultureEN_US Is Nothing Then cultureEN_US = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureEN_US
            SetupUILanguage()
        End Sub

        Public Sub ChangeLanguageToJapanese()
            If cultureJP_JP Is Nothing Then cultureJP_JP = New System.Globalization.CultureInfo("ja-JP")
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureJP_JP
            SetupUILanguage()
        End Sub

        Public Sub ChangeLanguageToChinese()
            If cultureZH_CN Is Nothing Then cultureZH_CN = New System.Globalization.CultureInfo("zh-CN")
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureZH_CN
            SetupUILanguage()
        End Sub

        Private Sub englishenUSToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            ChangeLanguageToEnglish()
        End Sub

        Private Sub japanesejpJPToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            ChangeLanguageToJapanese()
        End Sub

        Private Sub simplifiedChinesezhCNToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            ChangeLanguageToChinese()
        End Sub

        Private isUIUpdating As Boolean = False

        Private Sub UpdateMenuAndToolStrips()
            If isUIUpdating Then Return
            isUIUpdating = True
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet
            Dim style As WorksheetRangeStyle = worksheet.GetCellStyles(worksheet.SelectionRange.StartPos)

            If style IsNot Nothing Then
                Dim [set] As Action = Sub()
                                          fontToolStripComboBox.Text = style.FontName
                                          fontSizeToolStripComboBox.Text = style.FontSize.ToString()
                                          boldToolStripButton.Checked = style.Bold
                                          italicToolStripButton.Checked = style.Italic
                                          strikethroughToolStripButton.Checked = style.Strikethrough
                                          underlineToolStripButton.Checked = style.Underline
                                          textColorPickToolStripItem.SolidColor = style.TextColor
                                          backColorPickerToolStripButton.SolidColor = style.BackColor
                                          textAlignLeftToolStripButton.Checked = style.HAlign = ReoGridHorAlign.Left
                                          textAlignCenterToolStripButton.Checked = style.HAlign = ReoGridHorAlign.Center
                                          textAlignRightToolStripButton.Checked = style.HAlign = ReoGridHorAlign.Right
                                          distributedIndentToolStripButton.Checked = style.HAlign = ReoGridHorAlign.DistributedIndent
                                          textAlignTopToolStripButton.Checked = style.VAlign = ReoGridVerAlign.Top
                                          textAlignMiddleToolStripButton.Checked = style.VAlign = ReoGridVerAlign.Middle
                                          textAlignBottomToolStripButton.Checked = style.VAlign = ReoGridVerAlign.Bottom
                                          textWrapToolStripButton.Checked = style.TextWrapMode <> TextWrapMode.NoWrap
                                          Dim borderInfo As RangeBorderInfoSet = worksheet.GetRangeBorders(worksheet.SelectionRange)

                                          If borderInfo.Left IsNot Nothing Then
                                              borderColorPickToolStripItem.SolidColor = borderInfo.Left.Color
                                          ElseIf borderInfo.Right IsNot Nothing Then
                                              borderColorPickToolStripItem.SolidColor = borderInfo.Right.Color
                                          ElseIf borderInfo.Top IsNot Nothing Then
                                              borderColorPickToolStripItem.SolidColor = borderInfo.Top.Color
                                          ElseIf borderInfo.Bottom IsNot Nothing Then
                                              borderColorPickToolStripItem.SolidColor = borderInfo.Bottom.Color
                                          ElseIf borderInfo.InsideHorizontal IsNot Nothing Then
                                              borderColorPickToolStripItem.SolidColor = borderInfo.InsideHorizontal.Color
                                          ElseIf borderInfo.InsideVertical IsNot Nothing Then
                                              borderColorPickToolStripItem.SolidColor = borderInfo.InsideVertical.Color
                                          Else
                                              borderColorPickToolStripItem.SolidColor = Color.Black
                                          End If

                                          undoToolStripButton.Enabled = CSharpImpl.__Assign(undoToolStripMenuItem.Enabled, Sticky_Note_Form.Grid.CanUndo())
                                          redoToolStripButton.Enabled = CSharpImpl.__Assign(redoToolStripMenuItem.Enabled, Sticky_Note_Form.Grid.CanRedo())
                                          repeatLastActionToolStripMenuItem.Enabled = Sticky_Note_Form.Grid.CanUndo() OrElse Sticky_Note_Form.Grid.CanRedo()
                                          cutToolStripButton.Enabled = CSharpImpl.__Assign(cutToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowCutToolStripMenuItem.Enabled, CSharpImpl.__Assign(colCutToolStripMenuItem.Enabled, worksheet.CanCut())))
                                          copyToolStripButton.Enabled = CSharpImpl.__Assign(copyToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowCopyToolStripMenuItem.Enabled, CSharpImpl.__Assign(colCopyToolStripMenuItem.Enabled, worksheet.CanCopy())))
                                          pasteToolStripButton.Enabled = CSharpImpl.__Assign(pasteToolStripMenuItem.Enabled, CSharpImpl.__Assign(rowPasteToolStripMenuItem.Enabled, CSharpImpl.__Assign(colPasteToolStripMenuItem.Enabled, worksheet.CanPaste())))
                                          unfreezeToolStripMenuItem.Enabled = worksheet.IsFrozen
                                          isUIUpdating = False
                                      End Sub

                If Sticky_Note_Form.InvokeRequired Then
                    Sticky_Note_Form.Invoke([set])
                Else
                    [set]()
                End If
            End If

            debugToolStripMenuItem.Enabled = False
        End Sub

        Private settingSelectionMode As Boolean = False

        Private Sub UpdateSelectionModeAndStyle()
            If settingSelectionMode Then Return
            settingSelectionMode = True
            selModeNoneToolStripMenuItem.Checked = False
            selModeCellToolStripMenuItem.Checked = False
            selModeRangeToolStripMenuItem.Checked = False
            selModeRowToolStripMenuItem.Checked = False
            selModeColumnToolStripMenuItem.Checked = False

            Select Case Sticky_Note_Form.CurrentWorksheet.SelectionMode
                Case WorksheetSelectionMode.None
                    selModeNoneToolStripMenuItem.Checked = True
                Case WorksheetSelectionMode.Cell
                    selModeCellToolStripMenuItem.Checked = True
                Case WorksheetSelectionMode.Row
                    selModeRowToolStripMenuItem.Checked = True
                Case WorksheetSelectionMode.Column
                    selModeColumnToolStripMenuItem.Checked = True
                Case Else
                    selModeRangeToolStripMenuItem.Checked = True
            End Select

            selStyleNoneToolStripMenuItem.Checked = False
            selStyleDefaultToolStripMenuItem.Checked = False
            selStyleFocusRectToolStripMenuItem.Checked = False

            Select Case Sticky_Note_Form.CurrentWorksheet.SelectionStyle
                Case WorksheetSelectionStyle.None
                    selStyleNoneToolStripMenuItem.Checked = True
                Case WorksheetSelectionStyle.FocusRect
                    selStyleFocusRectToolStripMenuItem.Checked = True
                Case Else
                    selStyleDefaultToolStripMenuItem.Checked = True
            End Select

            focusStyleDefaultToolStripMenuItem.Checked = False
            focusStyleNoneToolStripMenuItem.Checked = False

            Select Case Sticky_Note_Form.CurrentWorksheet.FocusPosStyle
                Case FocusPosStyle.None
                    focusStyleNoneToolStripMenuItem.Checked = True
                Case Else
                    focusStyleDefaultToolStripMenuItem.Checked = True
            End Select

            settingSelectionMode = False
        End Sub

        Private Sub UpdateSelectionForwardDirection()
            Select Case Sticky_Note_Form.CurrentWorksheet.SelectionForwardDirection
                Case SelectionForwardDirection.Down
                    selDirRightToolStripMenuItem.Checked = False
                    selDirDownToolStripMenuItem.Checked = True
                Case Else
                    selDirRightToolStripMenuItem.Checked = True
                    selDirDownToolStripMenuItem.Checked = False
            End Select
        End Sub

        Public Property CurrentFilePath As String
        Private currentTempFilePath As String

        Public Sub LoadFile(ByVal path As String)
            LoadFile(path, Encoding.[Default])
        End Sub

        Public Sub LoadFile(ByVal path As String, ByVal encoding As Encoding)
            Sticky_Note_Form.CurrentFilePath = Nothing
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet
            Dim success As Boolean = False
            grid.CurrentWorksheet.Reset()

            Try
                grid.Load(path, IO.FileFormat._Auto, encoding)
                success = True
            Catch ex As FileNotFoundException
                success = False
                MessageBox.Show(LangResource.Msg_File_Not_Found & ex.FileName, "ReoGrid Editor", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
            Catch ex As Exception
                success = False
                MessageBox.Show(LangResource.Msg_Load_File_Failed & ex.Message, "ReoGrid Editor", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
            End Try

            If success Then
                Sticky_Note_Form.Text = System.IO.Path.GetFileName(path) & " - ReoGrid Editor " & Sticky_Note_Form.ProductVersion
                ShowStatus(String.Empty)
                Sticky_Note_Form.CurrentFilePath = path
                Sticky_Note_Form.currentTempFilePath = Nothing
            End If
        End Sub

        Private Sub NewFile()
            If Not CloseDocument() Then
                Return
            End If

            Sticky_Note_Form.Grid.Reset()
            Sticky_Note_Form.Text = LangResource.Untitled & " - ReoGrid Editor " + Sticky_Note_Form.ProductVersion
            Sticky_Note_Form.CurrentFilePath = Nothing
            Sticky_Note_Form.currentTempFilePath = Nothing
        End Sub

        Private Sub SaveFile(ByVal path As String)
            Dim fm As FileFormat = FileFormat._Auto

            If path.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase) Then
                fm = FileFormat.Excel2007
            ElseIf path.EndsWith(".rgf", StringComparison.CurrentCultureIgnoreCase) Then
                fm = FileFormat.ReoGridFormat
            ElseIf path.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase) Then
                fm = FileFormat.CSV
            End If

            Try
                Sticky_Note_Form.Grid.Save(path, fm)
                Sticky_Note_Form.SetCurrentDocumentFile(path)
            Catch ex As Exception
                MessageBox.Show(Me, "Save error: " & ex.Message, "Save Workbook", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub SetCurrentDocumentFile(ByVal filepath As String)
            Sticky_Note_Form.Text = System.IO.Path.GetFileName(filepath) & " - ReoGrid Editor " & Sticky_Note_Form.ProductVersion
            Sticky_Note_Form.CurrentFilePath = filepath
            Sticky_Note_Form.currentTempFilePath = Nothing
        End Sub

        Private Sub newToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            NewFile()
        End Sub

        Private Sub loadToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Using ofd As OpenFileDialog = New OpenFileDialog()
                ofd.Filter = LangResource.Filter_Load_File

                If ofd.ShowDialog(Me) = DialogResult.OK Then
                    LoadFile(ofd.FileName)
                    Sticky_Note_Form.SetCurrentDocumentFile(ofd.FileName)
                End If
            End Using
        End Sub

        Public Sub SaveDocument()
            If String.IsNullOrEmpty(CurrentFilePath) Then
                SaveAsDocument()
            Else
                SaveFile(Sticky_Note_Form.CurrentFilePath)
            End If
        End Sub

        Public Function SaveAsDocument() As Boolean
            Using sfd As SaveFileDialog = New SaveFileDialog()
                sfd.Filter = LangResource.Filter_Save_File

                If Not String.IsNullOrEmpty(Sticky_Note_Form.CurrentFilePath) Then
                    sfd.FileName = Path.GetFileNameWithoutExtension(Sticky_Note_Form.CurrentFilePath)
                    Dim format = GetFormatByExtension(Sticky_Note_Form.CurrentFilePath)

                    Select Case format
                        Case FileFormat.Excel2007
                            sfd.FilterIndex = 1
                        Case FileFormat.ReoGridFormat
                            sfd.FilterIndex = 2
                        Case FileFormat.CSV
                            sfd.FilterIndex = 3
                    End Select
                End If

                If sfd.ShowDialog(Me) = DialogResult.OK Then
                    SaveFile(sfd.FileName)
                    Return True
                End If
            End Using

            Return False
        End Function

        Public Property NewDocumentOnLoad As Boolean

        Protected Overrides Sub OnLoad(ByVal e As EventArgs)
            MyBase.OnLoad(e)

            If Not String.IsNullOrEmpty(CurrentFilePath) Then
                Sticky_Note_Form.Grid.Reset()
                LoadFile(CurrentFilePath)
            ElseIf NewDocumentOnLoad Then
                NewFile()
            End If

            UpdateSelectionModeAndStyle()
            UpdateSelectionForwardDirection()
            grid.Focus()
        End Sub

        Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
            MyBase.OnClosing(e)
        End Sub

        Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            newToolStripButton.PerformClick()
        End Sub

        Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            loadToolStripButton.PerformClick()
        End Sub

        Public Function CloseDocument() As Boolean
            If Sticky_Note_Form.Grid.IsWorkbookEmpty Then
                Return True
            End If

            Dim dr = MessageBox.Show(LangResource.Msg_Save_Changes, "ReoGrid Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If dr = System.Windows.Forms.DialogResult.No Then
                Return True
            ElseIf dr = System.Windows.Forms.DialogResult.Cancel Then
                Return False
            End If

            Dim format As FileFormat = FileFormat._Auto

            If Not String.IsNullOrEmpty(Sticky_Note_Form.CurrentFilePath) Then
                format = GetFormatByExtension(Sticky_Note_Form.CurrentFilePath)
            End If

            If format = FileFormat._Auto OrElse String.IsNullOrEmpty(Sticky_Note_Form.CurrentFilePath) Then
                Return SaveAsDocument()
            Else
                SaveDocument()
            End If

            Return True
        End Function

        Private Function GetFormatByExtension(ByVal path As String) As FileFormat
            If String.IsNullOrEmpty(path) Then
                Return FileFormat._Auto
            End If

            Dim ext As String = path.GetExtension(Sticky_Note_Form.CurrentFilePath)

            If ext.Equals(".rgf", StringComparison.CurrentCultureIgnoreCase) OrElse ext.Equals(".xml", StringComparison.CurrentCultureIgnoreCase) Then
                Return FileFormat.ReoGridFormat
            ElseIf ext.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase) Then
                Return FileFormat.Excel2007
            ElseIf ext.Equals(".csv", StringComparison.CurrentCultureIgnoreCase) Then
                Return FileFormat.CSV
            Else
                Return FileFormat._Auto
            End If
        End Function

        Private Sub textLeftToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.Left
            }))
        End Sub

        Private Sub textCenterToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.Center
            }))
        End Sub

        Private Sub textRightToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.Right
            }))
        End Sub

        Private Sub distributedIndentToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.DistributedIndent
            }))
        End Sub

        Private Sub textAlignTopToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Top
            }))
        End Sub

        Private Sub textAlignMiddleToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Middle
            }))
        End Sub

        Private Sub textAlignBottomToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Bottom
            }))
        End Sub

        Private Sub SetSelectionBorder(ByVal borderPos As BorderPositions, ByVal style As BorderLineStyle)
            Sticky_Note_Form.Grid.DoAction(New SetRangeBorderAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, borderPos, New RangeBorderStyle With {
                .Color = borderColorPickToolStripItem.SolidColor,
                .style = style
            }))
        End Sub

        Private Sub boldOutlineToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub dottedOutlineToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Dotted)
        End Sub

        Private Sub boundsSolidToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Solid)
        End Sub

        Private Sub solidOutlineToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Solid)
        End Sub

        Private Sub dashedOutlineToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Outside, BorderLineStyle.Dashed)
        End Sub

        Private Sub insideSolidToolStripSplitButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Solid)
        End Sub

        Private Sub insideSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Solid)
        End Sub

        Private Sub insideBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub insideDottedToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Dotted)
        End Sub

        Private Sub insideDashedToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.InsideAll, BorderLineStyle.Dashed)
        End Sub

        Private Sub leftRightSolidToolStripSplitButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Solid)
        End Sub

        Private Sub leftRightSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Solid)
        End Sub

        Private Sub leftRightBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub leftRightDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Dotted)
        End Sub

        Private Sub leftRightDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.LeftRight, BorderLineStyle.Dashed)
        End Sub

        Private Sub topBottomSolidToolStripSplitButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Solid)
        End Sub

        Private Sub topBottomSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Solid)
        End Sub

        Private Sub topBottomBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub topBottomDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Dotted)
        End Sub

        Private Sub topBottomDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.TopBottom, BorderLineStyle.Dashed)
        End Sub

        Private Sub allSolidToolStripSplitButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.All, BorderLineStyle.Solid)
        End Sub

        Private Sub allSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.All, BorderLineStyle.Solid)
        End Sub

        Private Sub allBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.All, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub allDottedToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.All, BorderLineStyle.Dotted)
        End Sub

        Private Sub allDashedToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.All, BorderLineStyle.Dashed)
        End Sub

        Private Sub leftSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Solid)
        End Sub

        Private Sub leftSolidToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Solid)
        End Sub

        Private Sub leftBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Left, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub leftDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Dotted)
        End Sub

        Private Sub leftDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Left, BorderLineStyle.Dashed)
        End Sub

        Private Sub topSolidToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Solid)
        End Sub

        Private Sub topSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Solid)
        End Sub

        Private Sub topBlodToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Top, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub topDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Dotted)
        End Sub

        Private Sub topDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Top, BorderLineStyle.Dashed)
        End Sub

        Private Sub bottomToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Solid)
        End Sub

        Private Sub bottomSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Solid)
        End Sub

        Private Sub bottomBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub bottomDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Dotted)
        End Sub

        Private Sub bottomDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Bottom, BorderLineStyle.Dashed)
        End Sub

        Private Sub rightSolidToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Solid)
        End Sub

        Private Sub rightSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Solid)
        End Sub

        Private Sub rightBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Right, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub rightDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Dotted)
        End Sub

        Private Sub rightDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Right, BorderLineStyle.Dashed)
        End Sub

        Private Sub slashRightSolidToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Solid)
        End Sub

        Private Sub slashRightSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Solid)
        End Sub

        Private Sub slashRightBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub slashRightDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Dotted)
        End Sub

        Private Sub slashRightDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Slash, BorderLineStyle.Dashed)
        End Sub

        Private Sub slashLeftSolidToolStripButton_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Solid)
        End Sub

        Private Sub slashLeftSolidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Solid)
        End Sub

        Private Sub slashLeftBoldToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.BoldSolid)
        End Sub

        Private Sub slashLeftDotToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Dotted)
        End Sub

        Private Sub slashLeftDashToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SetSelectionBorder(BorderPositions.Backslash, BorderLineStyle.Dashed)
        End Sub

        Private Sub clearBordersToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeBorderAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, BorderPositions.All, New RangeBorderStyle With {
                .Color = Color.Empty,
                .Style = BorderLineStyle.None
            }))
        End Sub

        Private Sub backColorPickerToolStripButton_ColorPicked(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle() With {
                .Flag = PlainStyleFlag.BackColor,
                .BackColor = backColorPickerToolStripButton.SolidColor
            }))
        End Sub

        Private Sub textColorPickToolStripItem_ColorPicked(ByVal sender As Object, ByVal e As EventArgs)
            Dim color = textColorPickToolStripItem.SolidColor

            If color.IsEmpty Then
                Sticky_Note_Form.Grid.DoAction(New RemoveRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, PlainStyleFlag.TextColor))
            Else
                Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                    .Flag = PlainStyleFlag.TextColor,
                    .TextColor = color
                }))
            End If
        End Sub

        Private Sub boldToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontStyleBold,
                .Bold = boldToolStripButton.Checked
            }))
        End Sub

        Private Sub italicToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontStyleItalic,
                .Italic = italicToolStripButton.Checked
            }))
        End Sub

        Private Sub underlineToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontStyleUnderline,
                .Underline = underlineToolStripButton.Checked
            }))
        End Sub

        Private Sub strikethroughToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontStyleStrikethrough,
                .Strikethrough = strikethroughToolStripButton.Checked
            }))
        End Sub

        Private Sub styleBrushToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.CurrentWorksheet.StartPickRangeAndCopyStyle()
        End Sub

        Private Sub enlargeFontToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New StepRangeFontSizeAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, True))
            UpdateMenuAndToolStrips()
        End Sub

        Private Sub fontSmallerToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New StepRangeFontSizeAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, False))
            UpdateMenuAndToolStrips()
        End Sub

        Private Sub fontToolStripComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If isUIUpdating Then Return
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontName,
                .FontName = fontToolStripComboBox.Text
            }))
        End Sub

        Private Sub fontSizeToolStripComboBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            SetGridFontSize()
        End Sub

        Private Sub SetGridFontSize()
            If isUIUpdating Then Return
            Dim size As Single = 9
            Single.TryParse(fontSizeToolStripComboBox.Text, size)
            If size <= 0 Then size = 1.0F
            Sticky_Note_Form.Grid.DoAction(New SetRangeStyleAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontSize,
                .FontSize = size
            }))
        End Sub

        Private Sub MergeSelectionRange(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Sticky_Note_Form.Grid.DoAction(New MergeRangeAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange))
            Catch __unusedRangeTooSmallException1__ As RangeTooSmallException
            Catch __unusedRangeIntersectionException2__ As RangeIntersectionException
                MessageBox.Show(LangResource.Msg_Range_Intersection_Exception, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End Sub

        Private Sub UnmergeSelectionRange(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New UnmergeRangeAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange))
        End Sub

        Private Sub resizeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet

            Using rgf = New ResizeGridDialog()
                rgf.Rows = worksheet.RowCount
                rgf.Cols = worksheet.ColumnCount

                If rgf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    Dim ag As WorksheetActionGroup = New WorksheetActionGroup()

                    If rgf.Rows < worksheet.RowCount Then
                        ag.Actions.Add(New RemoveRowsAction(rgf.Rows, worksheet.RowCount - rgf.Rows))
                    ElseIf rgf.Rows > worksheet.RowCount Then
                        ag.Actions.Add(New InsertRowsAction(worksheet.RowCount, rgf.Rows - worksheet.RowCount))
                    End If

                    If rgf.Cols < worksheet.ColumnCount Then
                        ag.Actions.Add(New RemoveColumnsAction(rgf.Cols, worksheet.ColumnCount - rgf.Cols))
                    ElseIf rgf.Cols > worksheet.ColumnCount Then
                        ag.Actions.Add(New InsertColumnsAction(worksheet.ColumnCount, rgf.Cols - worksheet.ColumnCount))
                    End If

                    If ag.Actions.Count > 0 Then
                        Cursor = Cursors.WaitCursor

                        Try
                            Sticky_Note_Form.Grid.DoAction(ag)
                        Finally
                            Cursor = Cursors.[Default]
                        End Try
                    End If
                End If
            End Using
        End Sub

        Private Sub ApplyFunctionToSelectedRange(ByVal funName As String)
            Dim sheet = Sticky_Note_Form.CurrentWorksheet
            Dim range = Sticky_Note_Form.CurrentSelectionRange

            If range.Rows > 1 Then

                For c As Integer = range.Col To range.EndCol
                    Dim cell = sheet.Cells(range.EndRow, c)

                    If String.IsNullOrEmpty(cell.DisplayText) Then
                        cell.Formula = String.Format("{0}({1})", funName, RangePosition.FromCellPosition(range.Row, range.Col, range.EndRow - 1, c).ToAddress())
                        Exit For
                    End If
                Next
            End If

            If range.Cols > 1 Then

                For r As Integer = range.Row To range.EndRow
                    Dim cell = sheet.Cells(r, range.EndCol)

                    If String.IsNullOrEmpty(cell.DisplayText) Then
                        cell.Formula = String.Format("{0}({1})", funName, RangePosition.FromCellPosition(range.Row, range.Col, r, range.EndCol - 1).ToAddress())
                        Exit For
                    End If
                Next
            End If
        End Sub

        Private Sub insertColToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Sticky_Note_Form.CurrentSelectionRange.Cols >= 1 Then
                Sticky_Note_Form.Grid.DoAction(New InsertColumnsAction(CurrentSelectionRange.Col, CurrentSelectionRange.Cols))
            End If
        End Sub

        Private Sub insertRowToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Sticky_Note_Form.CurrentSelectionRange.Rows >= 1 Then
                Sticky_Note_Form.Grid.DoAction(New InsertRowsAction(CurrentSelectionRange.Row, CurrentSelectionRange.Rows))
            End If
        End Sub

        Private Sub deleteColumnToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Sticky_Note_Form.CurrentSelectionRange.Cols >= 1 Then
                Sticky_Note_Form.Grid.DoAction(New RemoveColumnsAction(CurrentSelectionRange.Col, CurrentSelectionRange.Cols))
            End If
        End Sub

        Private Sub deleteRowsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Sticky_Note_Form.CurrentSelectionRange.Rows >= 1 Then
                Sticky_Note_Form.Grid.DoAction(New RemoveRowsAction(CurrentSelectionRange.Row, CurrentSelectionRange.Rows))
            End If
        End Sub

        Private Sub resetToDefaultWidthToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetColumnsWidthAction(CurrentSelectionRange.Col, CurrentSelectionRange.Cols, Worksheet.InitDefaultColumnWidth))
        End Sub

        Private Sub resetToDefaultHeightToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New SetRowsHeightAction(CurrentSelectionRange.Row, CurrentSelectionRange.Rows, Worksheet.InitDefaultRowHeight))
        End Sub

        Protected Overrides Sub OnShown(ByVal e As EventArgs)
            MyBase.OnShown(e)
        End Sub

        Protected Overrides Sub OnMove(ByVal e As EventArgs)
            MyBase.OnMove(e)
        End Sub

        Private Sub cutRangeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Cut()
        End Sub

        Private Sub copyRangeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Copy()
        End Sub

        Private Sub pasteRangeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Paste()
        End Sub

        Private Sub Cut()
            Try
                Sticky_Note_Form.CurrentWorksheet.Cut()
            Catch __unusedRangeIntersectionException1__ As RangeIntersectionException
                MessageBox.Show(LangResource.Msg_Range_Intersection_Exception)
            Catch
                MessageBox.Show(LangResource.Msg_Operation_Aborted)
            End Try
        End Sub

        Private Sub Copy()
            Try
                Sticky_Note_Form.CurrentWorksheet.Copy()
            Catch __unusedRangeIntersectionException1__ As RangeIntersectionException
                MessageBox.Show(LangResource.Msg_Range_Intersection_Exception)
            Catch
                MessageBox.Show(LangResource.Msg_Operation_Aborted)
            End Try
        End Sub

        Private Sub Paste()
            Try
                Sticky_Note_Form.CurrentWorksheet.Paste()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub cutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            cutToolStripButton.PerformClick()
        End Sub

        Private Sub copyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            copyToolStripButton.PerformClick()
        End Sub

        Private Sub pasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            pasteToolStripButton.PerformClick()
        End Sub

        Private Sub repeatLastActionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.RepeatLastAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange)
        End Sub

        Private Sub selectAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.CurrentWorksheet.SelectAll()
        End Sub

        Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Close()
        End Sub

        Private Sub newWindowToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            New ReoGridEditor().Show()
        End Sub

        Private Sub styleEditorToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim styleEditor As ControlAppearanceEditorForm = New ControlAppearanceEditorForm()
            styleEditor.Grid = Sticky_Note_Form.Grid
            styleEditor.Show(Me)
        End Sub

        Private Sub formatCellToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Using form As PropertyForm = New PropertyForm(Sticky_Note_Form.Grid)
                form.ShowDialog(Me)
            End Using
        End Sub

        Private Sub printPreviewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            printPreviewToolStripButton.PerformClick()
        End Sub

        Private Sub printPreviewToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Sticky_Note_Form.Grid.CurrentWorksheet.AutoSplitPage()
                Sticky_Note_Form.Grid.CurrentWorksheet.EnableSettings(WorksheetSettings.View_ShowPageBreaks)
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, Application.ProductName & " " + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End Try

            Using session = Sticky_Note_Form.Grid.CurrentWorksheet.CreatePrintSession()

                Using ppd As PrintPreviewDialog = New PrintPreviewDialog()
                    ppd.Document = session.PrintDocument
                    ppd.SetBounds(200, 200, 1024, 768)
                    ppd.PrintPreviewControl.Zoom = 1.0R
                    ppd.ShowDialog(Me)
                End Using
            End Using
        End Sub

        Private Sub PrintToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim session As PrintSession = Nothing

            Try
                session = grid.CurrentWorksheet.CreatePrintSession()
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, Sticky_Note_Form.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End Try

            Using pd = New System.Windows.Forms.PrintDialog()
                pd.Document = session.PrintDocument
                pd.UseEXDialog = True

                If pd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    session.Print()
                End If
            End Using

            If session IsNot Nothing Then session.Dispose()
        End Sub

        Private Sub removeRowPageBreakToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.CurrentWorksheet.RemoveRowPageBreak(Sticky_Note_Form.Grid.CurrentWorksheet.FocusPos.Row)
        End Sub

        Private Sub printSettingsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Using psf As PrintSettingsDialog = New PrintSettingsDialog()
                Dim sheet = Sticky_Note_Form.Grid.CurrentWorksheet

                If sheet.PrintSettings Is Nothing Then
                    sheet.PrintSettings = New PrintSettings()
                End If

                psf.PrintSettings = CType(sheet.PrintSettings.Clone(), PrintSettings)

                If psf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    sheet.PrintSettings = psf.PrintSettings
                    sheet.AutoSplitPage()
                    sheet.EnableSettings(WorksheetSettings.View_ShowPageBreaks)
                End If
            End Using
        End Sub

        Private Sub removeColPageBreakToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Sticky_Note_Form.Grid.CurrentWorksheet.RemoveColumnPageBreak(Sticky_Note_Form.Grid.CurrentWorksheet.FocusPos.Col)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub insertRowPageBreakToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Sticky_Note_Form.CurrentWorksheet.RowPageBreaks.Add(Sticky_Note_Form.CurrentWorksheet.FocusPos.Row)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub insertColPageBreakToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.CurrentWorksheet.ColumnPageBreaks.Add(Sticky_Note_Form.CurrentWorksheet.FocusPos.Col)
        End Sub

        Private Sub FreezeToEdge(ByVal freezePos As FreezeArea)
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet

            If Not worksheet.SelectionRange.IsEmpty Then
                worksheet.FreezeToCell(worksheet.FocusPos, freezePos)
                UpdateMenuAndToolStrips()
            End If
        End Sub

        Private Sub unfreezeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.CurrentWorksheet.Unfreeze()
            UpdateMenuAndToolStrips()
        End Sub

        Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            New AboutForm().ShowDialog(Me)
        End Sub

        Private Sub groupRowsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Sticky_Note_Form.Grid.DoAction(New AddOutlineAction(RowOrColumn.Row, Sticky_Note_Form.CurrentSelectionRange.Row, Sticky_Note_Form.CurrentSelectionRange.Rows))
            Catch __unusedOutlineOutOfRangeException1__ As OutlineOutOfRangeException
                MessageBox.Show(LangResource.Msg_Outline_Out_Of_Range)
            Catch __unusedOutlineAlreadyDefinedException2__ As OutlineAlreadyDefinedException
                MessageBox.Show(LangResource.Msg_Outline_Already_Exist)
            Catch __unusedOutlineIntersectedException3__ As OutlineIntersectedException
                MessageBox.Show(LangResource.Msg_Outline_Intersected)
            Catch __unusedOutlineTooMuchException4__ As OutlineTooMuchException
                MessageBox.Show(LangResource.Msg_Outline_Too_Much)
            End Try
        End Sub

        Private Sub ungroupRowsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim removeOutlineAction = New RemoveOutlineAction(RowOrColumn.Row, Sticky_Note_Form.CurrentSelectionRange.Row, Sticky_Note_Form.CurrentSelectionRange.Rows)

            Try
                Sticky_Note_Form.Grid.DoAction(removeOutlineAction)
            Catch
            End Try

            If removeOutlineAction.RemovedOutline Is Nothing Then
                MessageBox.Show(LangResource.Msg_Outline_Not_Found)
            End If
        End Sub

        Private Sub groupColumnsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim worksheet = Sticky_Note_Form.CurrentWorksheet

            Try
                Sticky_Note_Form.Grid.DoAction(New AddOutlineAction(RowOrColumn.Column, Sticky_Note_Form.CurrentSelectionRange.Col, Sticky_Note_Form.CurrentSelectionRange.Cols))
            Catch __unusedOutlineOutOfRangeException1__ As OutlineOutOfRangeException
                MessageBox.Show(LangResource.Msg_Outline_Out_Of_Range)
            Catch __unusedOutlineAlreadyDefinedException2__ As OutlineAlreadyDefinedException
                MessageBox.Show(LangResource.Msg_Outline_Already_Exist)
            Catch __unusedOutlineIntersectedException3__ As OutlineIntersectedException
                MessageBox.Show(LangResource.Msg_Outline_Intersected)
            Catch __unusedOutlineTooMuchException4__ As OutlineTooMuchException
                MessageBox.Show(LangResource.Msg_Outline_Too_Much)
            End Try
        End Sub

        Private Sub ungroupColumnsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim removeOutlineAction = New RemoveOutlineAction(RowOrColumn.Column, Sticky_Note_Form.CurrentSelectionRange.Col, Sticky_Note_Form.CurrentSelectionRange.Cols)

            Try
                Sticky_Note_Form.Grid.DoAction(removeOutlineAction)
            Catch
            End Try

            If removeOutlineAction.RemovedOutline Is Nothing Then
                MessageBox.Show(LangResource.Msg_Outline_Not_Found)
            End If
        End Sub

        Private Sub ungroupAllRowsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New ClearOutlineAction(RowOrColumn.Row))
        End Sub

        Private Sub ungroupAllColumnsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Sticky_Note_Form.Grid.DoAction(New ClearOutlineAction(RowOrColumn.Column))
        End Sub

        Private columnFilter As AutoColumnFilter

        Private Sub filterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Sticky_Note_Form.columnFilter IsNot Nothing Then
                Sticky_Note_Form.columnFilter.Detach()
            End If

            Dim action As CreateAutoFilterAction = New CreateAutoFilterAction(Sticky_Note_Form.CurrentWorksheet.SelectionRange)
            Sticky_Note_Form.Grid.DoAction(action)
            Sticky_Note_Form.columnFilter = action.AutoColumnFilter
        End Sub

        Private Sub clearFilterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Sticky_Note_Form.columnFilter IsNot Nothing Then
                columnFilter.Detach()
            End If
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
