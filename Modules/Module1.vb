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
    Public Function MakeTopMost(Optional ByVal BringMeToFront As Boolean = False, Optional ByVal MyForm As Form = Nothing, Optional IgnoreCheckState As Boolean = False)
        Try
            'If MagNote_Form.Me_Always_On_Top_ChkBx.CheckState = CheckState.Unchecked And
            '    Not BringMeToFront Then
            '    Exit Function
            'End If
            If IsNothing(MyForm) Then
                MyForm = MagNote_Form
            End If
            If MyForm.Name = MagNote_Form.Name Then
                If MyForm.WindowState <> FormWindowState.Minimized Then
                    MyForm.BringToFront()
                    MyForm.Focus()
                    RCSN(0).Focus()
                    MyForm.Activate()
                End If
            Else
                MyForm.BringToFront()
                MyForm.Focus()
                'RCSN(0).Focus()
                MyForm.Activate()
            End If
            'MagNote_Form.BringMeToFront()
            If MagNote_Form.Me_Always_On_Top_ChkBx.CheckState = CheckState.Checked Or IgnoreCheckState Then
                SetWindowPos(MagNote_Form.Handle(), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
                MagNote_Form.TopMost = True
            End If
            Application.DoEvents()
        Catch ex As Exception
        Finally
            'MagNote_Form.Activate()
        End Try
    End Function

    Public Function MakeNormal()
        SetWindowPos(MagNote_Form.Handle(), HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    End Function
End Module


