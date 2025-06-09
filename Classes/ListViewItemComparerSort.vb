Public Class ListViewItemComparerSort
    Implements IComparer

    Private col As Integer
    Private order As SortOrder

    Public Sub New(column As Integer, sortOrder As SortOrder)
        col = column
        order = sortOrder
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Try
            Dim itemX As ListViewItem = CType(x, ListViewItem)
            Dim itemY As ListViewItem = CType(y, ListViewItem)
            Dim result As Integer = String.Compare(itemX.SubItems(col).Text, itemY.SubItems(col).Text)
            If order = SortOrder.Descending Then
                result = -result
            End If
            Return result
        Catch ex As Exception
        End Try
    End Function
End Class
