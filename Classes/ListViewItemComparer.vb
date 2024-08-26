Public Class ListViewItemComparer : Implements IDisposable

    Implements IComparer, IComparer(Of ListViewItem)

    Private columnIndex As Integer

    Public Sub New(columnIndex As Integer)
        Me.columnIndex = columnIndex
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Return CompareCore(DirectCast(x, ListViewItem), DirectCast(y, ListViewItem))
    End Function

    Private Function CompareCore(x As ListViewItem, y As ListViewItem) As Integer Implements IComparer(Of ListViewItem).Compare
        Dim result As Integer

        Select Case Me.columnIndex
            Case 0
                result = x.Text.CompareTo(y.Text)
            Case 1
                result = CInt(x.SubItems(1).Text).CompareTo(CInt(y.SubItems(1).Text))
            Case 2
                result = CDate(x.SubItems(2).Text).CompareTo(CDate(y.SubItems(2).Text))
        End Select

        Return result
    End Function
#Region "IDisposable"
    Private disposedValue As Boolean
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
                ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
                ' TODO: set large fields to null
                disposedValue = True
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Dispose(disposing)
            End If
        End If
    End Sub
    ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    Protected Overrides Sub Finalize()
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=False)
        MyBase.Finalize()
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        'GC.Collect()
        'GC.Collect(2, GCCollectionMode.Optimized)
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
