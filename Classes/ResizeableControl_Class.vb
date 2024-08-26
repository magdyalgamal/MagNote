Public Class ResizeableControl_Class
    Implements IDisposable

    Private WithEvents MoveControl As Control
    Private mMouseDown As Boolean = False
    Private mEdge As EdgeEnum = EdgeEnum.None
    Private mWidth As Integer = 4
    Private mOutlineDrawn As Boolean = False

    Private Enum EdgeEnum
        None
        Right
        Left
        Top
        Bottom
        TopLeft
    End Enum

    Public Sub New(ByVal Control As Control)
        MoveControl = Control
    End Sub

    Private Sub MoveControl_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoveControl.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mMouseDown = True
        End If
        If (e.X > MoveControl.Width - 10) And (e.X < MoveControl.Width + 10) Then
            MoveControl.Tag = True
        Else
            MoveControl.Tag = False
        End If
    End Sub

    Private Sub MoveControl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoveControl.MouseUp
        mMouseDown = False
        MoveControl.Tag = False
    End Sub

    Private Sub MoveControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MoveControl.MouseMove
        Try
            If sender.name <> MagNote_Form.Name Then
                Dim x = 1
            End If
            Dim c As Control = CType(sender, Control)
            Dim g As Graphics = c.CreateGraphics
            Select Case mEdge
                Case EdgeEnum.TopLeft
                    g.FillRectangle(Brushes.Fuchsia, 0, 0, mWidth * 4, mWidth * 4)
                    mOutlineDrawn = True
                Case EdgeEnum.Left
                    g.FillRectangle(Brushes.Fuchsia, 0, 0, mWidth, c.Height)
                    mOutlineDrawn = True
                Case EdgeEnum.Right
                    g.FillRectangle(Brushes.Fuchsia, c.Width - mWidth, 0, c.Width, c.Height)
                    mOutlineDrawn = True
                Case EdgeEnum.Top
                    g.FillRectangle(Brushes.Fuchsia, 0, 0, c.Width, mWidth)
                    mOutlineDrawn = True
                Case EdgeEnum.Bottom
                    g.FillRectangle(Brushes.Fuchsia, 0, c.Height - mWidth, c.Width, mWidth)
                    mOutlineDrawn = True
                Case EdgeEnum.None
                    If mOutlineDrawn Then
                        c.Refresh()
                        mOutlineDrawn = False
                    End If
            End Select

            If mMouseDown And mEdge <> EdgeEnum.None Then
                c.SuspendLayout()
                Select Case mEdge
                    Case EdgeEnum.TopLeft
                        c.SetBounds(c.Left + e.X, c.Top + e.Y, c.Width, c.Height)
                    Case EdgeEnum.Left
                        c.SetBounds(c.Left + e.X, c.Top, c.Width - e.X, c.Height)
                    Case EdgeEnum.Right
                        c.SetBounds(c.Left, c.Top, c.Width - (c.Width - e.X), c.Height)
                    Case EdgeEnum.Top
                        c.SetBounds(c.Left, c.Top + e.Y, c.Width, c.Height - e.Y)
                    Case EdgeEnum.Bottom
                        c.SetBounds(c.Left, c.Top, c.Width, c.Height - (c.Height - e.Y))
                End Select
                c.ResumeLayout()
            Else
                Select Case True
                    Case e.X <= (mWidth * 4) And e.Y <= (mWidth * 4) 'top left corner
                        c.Cursor = Cursors.SizeAll
                        mEdge = EdgeEnum.TopLeft
                    Case e.X <= mWidth 'left edge
                        c.Cursor = Cursors.VSplit
                        mEdge = EdgeEnum.Left
                    Case e.X > c.Width - (mWidth + 1) 'right edge
                        c.Cursor = Cursors.VSplit
                        mEdge = EdgeEnum.Right
                    Case e.Y <= mWidth 'top edge
                        c.Cursor = Cursors.HSplit
                        mEdge = EdgeEnum.Top
                    Case e.Y > c.Height - (mWidth + 1) 'bottom edge
                        c.Cursor = Cursors.HSplit
                        mEdge = EdgeEnum.Bottom
                    Case Else 'no edge
                        c.Cursor = Cursors.Default
                        mEdge = EdgeEnum.None
                End Select
            End If
            If mMouseDown Then
                MagNote_Form.Note_Form_Size_TxtBx.Text = c.Size.ToString
                Application.DoEvents()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MoveControl_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MoveControl.MouseLeave
        Dim c As Control = CType(sender, Control)
        mEdge = EdgeEnum.None
        c.Refresh()
    End Sub

#Region "IDisposable"
    Private disposedValue As Boolean
    Private components As System.ComponentModel.IContainer
    Public Sub New()
        MyBase.New()
        'This call is the Windows Form Designer necessary.
        InitializeComponent()
        'Add any initialization after the InitializeComponent call ()
    End Sub
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
