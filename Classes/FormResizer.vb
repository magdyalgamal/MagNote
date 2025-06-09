' ==== Class: FormResizer.vb ====
Imports System.Runtime.InteropServices

Public Class FormResizer
    Private Const WM_NCHITTEST As Integer = &H84
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17

    Private ReadOnly form As Form
    Private borderWidth As Integer = 7

    Public Sub New(targetForm As Form, Optional resizeMargin As Integer = 7)
        Me.form = targetForm
        Me.borderWidth = resizeMargin
        AddHandler form.MouseDown, AddressOf HandleMouseDown
        AddHandler form.MouseMove, AddressOf HandleFormMouseMove
        AddHandler form.MouseLeave, AddressOf HandleFormMouseLeave
        AddHandler_Control_Move(form, 1)
    End Sub

    Private isOverHitArea As Boolean = False
    Private Sub HandleFormMouseMove(sender As Object, e As MouseEventArgs)
        Dim mPos As Point = sender.PointToClient(Control.MousePosition)
        Dim result As Integer = GetHitTest(mPos)
        If result <> -1 Then
            If Not isOverHitArea Then
                Select Case result
                    Case 10
                        sender.Cursor = Cursors.PanWest
                    Case 11 'left or right
                        sender.Cursor = Cursors.PanEast
                    Case 15
                        sender.Cursor = Cursors.PanSouth
                    Case 16
                        sender.Cursor = Cursors.PanSW
                    Case 17
                        sender.Cursor = Cursors.PanSE
                End Select
                isOverHitArea = True
            End If
        Else
            If isOverHitArea Then
                sender.Cursor = Cursors.Default
                isOverHitArea = False
            End If
        End If
    End Sub
    Private Sub HandleFormMouseLeave(sender As Object, e As EventArgs)
        sender.Cursor = Cursors.Default
        isOverHitArea = False
    End Sub
    Private Sub HandleMouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim mPos As Point = form.PointToClient(Control.MousePosition)
            Dim result As Integer = GetHitTest(mPos)
            If result <> -1 Then
                ReleaseCapture()
                SendMessage(form.Handle, WM_NCHITTEST, IntPtr.Zero, IntPtr.Zero)
                SendMessage(form.Handle, &HA1, CType(result, IntPtr), IntPtr.Zero)
            End If
        End If
    End Sub

    Private Function GetHitTest(p As Point) As Integer
        Dim w = form.ClientSize.Width
        Dim h = form.ClientSize.Height

        ' Check if RightToLeftLayout is enabled
        Dim isRtl As Boolean = form.RightToLeftLayout

        ' If RTL, flip the X coordinate to match visual layout
        Dim x = If(isRtl, w - p.X, p.X)

        If x <= borderWidth Then
            If p.Y <= borderWidth Then Return HTTOPLEFT
            If p.Y >= h - borderWidth Then Return HTBOTTOMLEFT
            Return HTLEFT
        ElseIf x >= w - borderWidth Then
            If p.Y <= borderWidth Then Return HTTOPRIGHT
            If p.Y >= h - borderWidth Then Return HTBOTTOMRIGHT
            Return HTRIGHT
        ElseIf p.Y <= borderWidth Then
            Return HTTOP
        ElseIf p.Y >= h - borderWidth Then
            Return HTBOTTOM
        End If

        Return -1



        'Dim w = form.ClientSize.Width
        'Dim h = form.ClientSize.Height

        'If p.X <= borderWidth Then
        '    If p.Y <= borderWidth Then Return HTTOPLEFT
        '    If p.Y >= h - borderWidth Then Return HTBOTTOMLEFT
        '    Return HTLEFT
        'ElseIf p.X >= w - borderWidth Then
        '    If p.Y <= borderWidth Then Return HTTOPRIGHT
        '    If p.Y >= h - borderWidth Then Return HTBOTTOMRIGHT
        '    Return HTRIGHT
        'ElseIf p.Y <= borderWidth Then
        '    Return HTTOP
        'ElseIf p.Y >= h - borderWidth Then
        '    Return HTBOTTOM
        'End If

        'Return -1
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Boolean
    End Function
End Class