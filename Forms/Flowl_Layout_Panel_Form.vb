Imports System.Windows.Forms
Public Class Flowl_Layout_Panel_Form

    Inherits System.Windows.Forms.Form

    Private Sub wrapContentsCheckBox_CheckedChanged(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) _
        Handles wrapContentsCheckBox.CheckedChanged

            Me.FlowLayoutPanel1.WrapContents = Me.wrapContentsCheckBox.Checked

        End Sub

        Private Sub flowTopDownBtn_CheckedChanged(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) _
        Handles flowTopDownBtn.CheckedChanged

            Me.FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown

        End Sub

        Private Sub flowBottomUpBtn_CheckedChanged(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) _
        Handles flowBottomUpBtn.CheckedChanged

            Me.FlowLayoutPanel1.FlowDirection = FlowDirection.BottomUp

        End Sub

    Private Sub flowLeftToRight_CheckedChanged(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs)


        Me.FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight

    End Sub

    Private Sub flowRightToLeftBtn_CheckedChanged(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs)


        Me.FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft

    End Sub

    Dim count = -1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        count += 1
        Dim Pic As New PictureBox
        Dim Title As New Label
        Title.Name = "Label Name " & count
        Title.Text = "Label Name " & count
        Title.Size = New Point(10, 20)
        Title.Dock = DockStyle.Top
        Title.TextAlign = ContentAlignment.MiddleCenter
        Title.BackColor = Color.Transparent
        Pic.Size = New Point(100, 100)
        Pic.BorderStyle = BorderStyle.FixedSingle
        Pic.BackgroundImage = Image.FromFile("D:\Desktop\My Desktop\MyPSD\Pictures\eye1.png")
        Pic.BackgroundImageLayout = ImageLayout.Stretch
        Dim Btn As New Button
        Btn.Size = New Point(10, 20)
        Btn.Text = "New Button " & count
        Btn.Dock = DockStyle.Bottom
        Pic.Controls.Add(Title)
        Pic.Controls.Add(Btn)
        Btn.BringToFront()
        Title.SendToBack()
        FlowLayoutPanel1.Controls.Add(Pic)
    End Sub

    Private Sub FlowLeftRight_RdioBtn_CheckedChanged(sender As Object, e As EventArgs) Handles FlowLeftRight_RdioBtn.CheckedChanged
        Me.FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft
    End Sub

    Private Sub FlowRightLeft_RdioBtn_CheckedChanged(sender As Object, e As EventArgs) Handles FlowRightLeft_RdioBtn.CheckedChanged
        Me.FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight
    End Sub

    Private Sub AutoScroll_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AutoScroll_CheckBox.CheckedChanged
        If AutoScroll_CheckBox.CheckState = CheckState.Checked Then
            FlowLayoutPanel1.AutoScroll = True
        Else
            FlowLayoutPanel1.AutoScroll = False
        End If
    End Sub
End Class
