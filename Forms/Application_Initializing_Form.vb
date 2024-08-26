Public Class Application_Initializing_Form
    Private Sub Application_Initializing_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        User_Password_TxtBx.Focus()
        Labeling_Form(sender, "Arabic")
        If MagNote_Form.Language_Btn.Text = "ع" Then
            Enter_Password_To_Pass_Lbl.Text = "Enter Password To Pass"
        Else
            Enter_Password_To_Pass_Lbl.Text = "أدخل كلمة السر للمرور"
        End If
        Download_PrgrsBr.Style = ProgressBarStyle.Marquee
        Download_PrgrsBr.Refresh()
        Download_PrgrsBr.Visible = True
        AddHandler_Control_Move(Me)
        Me.Focus()
    End Sub

    Private Sub User_Password_TxtBx_TextChanged(sender As Object, e As EventArgs) Handles User_Password_TxtBx.TextChanged
        If User_Password_TxtBx.TextLength >= MagNote_Form.Main_Password_TxtBx.TextLength Then
            MagNote_Form.EnteredPassword = User_Password_TxtBx.Text
        End If
    End Sub
    Public Sub Application_Initializing_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        sender.Dispose()
    End Sub

    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        If MagNote_Form.Main_Password_TxtBx.Text <> MagNote_Form.EnteredPassword Or
            String.IsNullOrEmpty(MagNote_Form.EnteredPassword) Then
            If MagNote_Form.Language_Btn.Text = "ع" Then
                Msg = "Wrong Main Password Do You Want To Tay Again?"
            Else
                Msg = "كلمة السر خاطئة... هل تريد إعادة المحاولة"
            End If
            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MBOs, False) = DialogResult.No Then
                End
            End If
        ElseIf MagNote_Form.Main_Password_TxtBx.Text = MagNote_Form.EnteredPassword Then
            PassedMainPasswordToPass = True
            Enabled = True
            User_Password_TxtBx.ReadOnly = True
        End If
    End Sub
End Class