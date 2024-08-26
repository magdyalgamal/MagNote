Imports System.Runtime.InteropServices
Module Module1
    <DllImport("user32.dll")>
    Private Sub LockWorkStation()
    End Sub
    ''' <summary>
    ''' Call LockWorkStation here or perhaps
    ''' in a button click event
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LockMe()
        LockWorkStation()
    End Sub
    Private ReadOnly HWND_TOPMOST As New IntPtr(-1)
    Private ReadOnly HWND_NOTOPMOST As New IntPtr(-2)
    Private Const SWP_NOSIZE As Integer = &H1
    Private Const SWP_NOMOVE As Integer = &H2
    <DllImport("user32.dll", SetLastError:=True)>
    Private Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean
    End Function
    Public Function MakeTopMost(Optional ByVal BringMeToFront As Boolean = False)
        Try
            If MagNote_Form.Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked And
                Not BringMeToFront Then
                Exit Function
            End If
            SetWindowPos(MagNote_Form.Handle(), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
            MagNote_Form.BringToFront()
            If MagNote_Form.Me_Always_On_Top_ChkBx.CheckState = CheckState.Checked Then
                MagNote_Form.TopMost = True
            End If
            MagNote_Form.Activate()
            'Application.DoEvents()
            If MagNote_Form.CanFocus Then
                MagNote_Form.Focus()
            End If
        Catch ex As Exception
        Finally
            If MagNote_Form.Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked Then
                MagNote_Form.TopMost = False
            End If
            MagNote_Form.Focus()
            Try
            Catch ex As Exception
            End Try
        End Try
    End Function

    Public Function MakeNormal()
        SetWindowPos(MagNote_Form.Handle(), HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    End Function
End Module


