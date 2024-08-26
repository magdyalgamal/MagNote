Imports System.Runtime.InteropServices

Public Class FormBoundsRestricter
    Inherits NativeWindow

    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer

        Public Overrides Function ToString() As String
            Return String.Format("{{Left={0},Top={1},Right={2},Bottom={3}}}",
                                 Me.Left,
                                 Me.Top,
                                 Me.Right,
                                 Me.Bottom)
        End Function
    End Structure

    Private Const WM_MOVING As Integer = &H216
    Private Const [TRUE] As Integer = 1

    Private WithEvents target As Form

    Public Sub New(ByVal target As Form)
        Me.target = target
    End Sub

    Private Sub target_HandleCreated(ByVal sender As Object,
                                     ByVal e As EventArgs) Handles target.HandleCreated
        Me.AssignHandle(Me.target.Handle)
    End Sub

    Private Sub target_HandleDestroyed(ByVal sender As Object,
                                       ByVal e As EventArgs) Handles target.HandleDestroyed
        Me.ReleaseHandle()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_MOVING Then
            If MagNote_Form.Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckState = CheckState.Unchecked Then
                Exit Sub
            End If

            Dim currentFormBounds As Rectangle = Me.target.Bounds
            Dim newFormBounds = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(RECT)), RECT)
            Dim screenBounds As Rectangle = Screen.PrimaryScreen.WorkingArea

            'Don't let the form move beyond the bounds of the screen in a horizontal direction.
            If newFormBounds.Left < screenBounds.Left OrElse
                   newFormBounds.Right > screenBounds.Right Then
                newFormBounds.Left = currentFormBounds.Left
                newFormBounds.Right = currentFormBounds.Right
            End If

            'Don't let the form move beyond the bounds of the screen in a vertical direction.
            If newFormBounds.Top < screenBounds.Top OrElse
                   newFormBounds.Bottom > screenBounds.Bottom Then
                newFormBounds.Top = currentFormBounds.Top
                newFormBounds.Bottom = currentFormBounds.Bottom
            End If

            Marshal.StructureToPtr(newFormBounds,
                                       m.LParam,
                                       False)
            m.Result = New IntPtr([TRUE])
        End If

        MyBase.WndProc(m)
    End Sub

End Class
