Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Data
Public Class MS_Excel_Form
    Dim xlApp As Excel.Application
    Dim xlWorkbook As Excel.Workbook
    Dim xlWorksheet As Excel.Worksheet
    Dim xlRange As Excel.Range
    Dim filePath As String

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub


    Private Sub Open_Excel_File_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Open_Excel_File_TlStrpBtn.Click
        Dim PreviewPnl As New Panel
        Dim Previewlbl As New Label
        Try
            MS_Excel_DGV.DataSource = Nothing

            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Excel Files|*.xlsx;*.xls"

            If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub
            Cursor = Cursors.WaitCursor

            filePath = ofd.FileName
            xlApp = New Excel.Application()
            xlApp.Visible = False

            xlWorkbook = xlApp.Workbooks.Open(filePath)
            xlWorksheet = CType(xlWorkbook.Sheets(1), Excel.Worksheet)
            xlRange = xlWorksheet.UsedRange

            ' Load to DataGridView
            Dim dt As New DataTable()
            Dim rows = xlRange.Rows.Count
            Dim cols = xlRange.Columns.Count
            Dim ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, cols)

            ' Add columns
            Dim x = 1
            For c = 1 To cols
                Dim colName = xlRange.Cells(1, c).Text
                progress += ProgressToAdd
                Previewlbl.Text = "Adding--> " & colName & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                If String.IsNullOrWhiteSpace(colName) Then
                    colName = "Column" & c
                End If
                Try
                    dt.Columns.Add(colName)
                Catch ex As Exception
                    If ex.Message.Contains("already belongs to this DataTable") Then
                        x += 1
                        dt.Columns.Add(colName & " _" & x & "_")
                    End If
                End Try
            Next

            ' Add rows
            ProgressToAdd = AddCustomProgresBar(PreviewPnl, Previewlbl, rows)
            x = 0
            For r = 2 To rows
                progress += ProgressToAdd
                Previewlbl.Text = "Adding--> " & r & vbNewLine & Math.Floor(progress * 100)
                Previewlbl.Refresh()
                Previewlbl.Invalidate()
                Dim newRow = dt.NewRow()
                For c = 1 To cols
                    Try
                        newRow(c - 1) = xlRange.Cells(r, c).Text
                    Catch ex As Exception
                        newRow(c - 1) = xlRange.Cells(r, c).Text
                    End Try
                Next
                dt.Rows.Add(newRow)
            Next
            MS_Excel_DGV.DataSource = dt
            ' Cleanup Excel (we keep the file path only)
            xlWorkbook.Close(False)
            xlApp.Quit()
            ReleaseObject(xlRange)
            ReleaseObject(xlWorksheet)
            ReleaseObject(xlWorkbook)
            ReleaseObject(xlApp)
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
            PreviewPnl.Dispose()
            Previewlbl.Dispose()
        End Try
    End Sub

    Private Sub Save_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Save_TlStrpBtn.Click
        Try
            If String.IsNullOrEmpty(filePath) Then
                MessageBox.Show("Please load an Excel file first.")
                Exit Sub
            End If
            Cursor = Cursors.WaitCursor

            ' Reopen Excel
            xlApp = New Excel.Application()
            xlApp.Visible = False
            xlWorkbook = xlApp.Workbooks.Open(filePath)
            xlWorksheet = CType(xlWorkbook.Sheets(1), Excel.Worksheet)

            ' Write data back to Excel
            Dim dt As DataTable = CType(MS_Excel_DGV.DataSource, DataTable)

            For r = 0 To dt.Rows.Count - 1
                For c = 0 To dt.Columns.Count - 1
                    ' +2 because Excel rows start at 1 and first row is header
                    xlWorksheet.Cells(r + 2, c + 1).Value = dt.Rows(r)(c).ToString()
                Next
            Next

            ' Save and close
            xlWorkbook.Save()
            xlWorkbook.Close(False)
            xlApp.Quit()

            ReleaseObject(xlWorksheet)
            ReleaseObject(xlWorkbook)
            ReleaseObject(xlApp)

            MessageBox.Show("Excel file updated successfully.")
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Exit_TlStrpBtn_Click(sender As Object, e As EventArgs) Handles Exit_TlStrpBtn.Click
        Me.Close()
    End Sub
End Class