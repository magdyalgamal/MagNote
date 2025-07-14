<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Application_Initializing_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Application_Initializing_Form))
        Me.Download_PrgrsBr = New System.Windows.Forms.ProgressBar()
        Me.Enter_Password_To_Pass_Lbl = New System.Windows.Forms.Label()
        Me.User_Password_TxtBx = New System.Windows.Forms.TextBox()
        Me.Exit_Btn = New System.Windows.Forms.Button()
        Me.Form_ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Download_PrgrsBr
        '
        Me.Download_PrgrsBr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_PrgrsBr.Location = New System.Drawing.Point(196, 153)
        Me.Download_PrgrsBr.Margin = New System.Windows.Forms.Padding(4)
        Me.Download_PrgrsBr.Name = "Download_PrgrsBr"
        Me.Download_PrgrsBr.Size = New System.Drawing.Size(195, 14)
        Me.Download_PrgrsBr.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Download_PrgrsBr.TabIndex = 5
        Me.Download_PrgrsBr.Visible = False
        '
        'Enter_Password_To_Pass_Lbl
        '
        Me.Enter_Password_To_Pass_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Enter_Password_To_Pass_Lbl.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Enter_Password_To_Pass_Lbl.ForeColor = System.Drawing.Color.White
        Me.Enter_Password_To_Pass_Lbl.Image = Global.MagNote.My.Resources.Resources.MagNoteHeaderName_Pass
        Me.Enter_Password_To_Pass_Lbl.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Enter_Password_To_Pass_Lbl.Location = New System.Drawing.Point(637, 119)
        Me.Enter_Password_To_Pass_Lbl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Enter_Password_To_Pass_Lbl.Name = "Enter_Password_To_Pass_Lbl"
        Me.Enter_Password_To_Pass_Lbl.Size = New System.Drawing.Size(291, 63)
        Me.Enter_Password_To_Pass_Lbl.TabIndex = 2
        Me.Enter_Password_To_Pass_Lbl.Text = "Enter Password To Pass" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "أدخل كلمة السر للمرور"
        Me.Enter_Password_To_Pass_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Enter_Password_To_Pass_Lbl.UseCompatibleTextRendering = True
        '
        'User_Password_TxtBx
        '
        Me.User_Password_TxtBx.Location = New System.Drawing.Point(401, 175)
        Me.User_Password_TxtBx.Margin = New System.Windows.Forms.Padding(4)
        Me.User_Password_TxtBx.Name = "User_Password_TxtBx"
        Me.User_Password_TxtBx.Size = New System.Drawing.Size(192, 22)
        Me.User_Password_TxtBx.TabIndex = 0
        Me.User_Password_TxtBx.UseSystemPasswordChar = True
        '
        'Exit_Btn
        '
        Me.Exit_Btn.Location = New System.Drawing.Point(300, 174)
        Me.Exit_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.Size = New System.Drawing.Size(100, 26)
        Me.Exit_Btn.TabIndex = 4
        Me.Exit_Btn.Text = "Exit خروج"
        Me.Exit_Btn.UseVisualStyleBackColor = True
        '
        'Form_ToolTip
        '
        Me.Form_ToolTip.BackColor = System.Drawing.Color.Yellow
        Me.Form_ToolTip.IsBalloon = True
        Me.Form_ToolTip.ShowAlways = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Enabled = False
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(615, 175)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Application_Initializing_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkGreen
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(615, 175)
        Me.Controls.Add(Me.Download_PrgrsBr)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.User_Password_TxtBx)
        Me.Controls.Add(Me.Enter_Password_To_Pass_Lbl)
        Me.Controls.Add(Me.PictureBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Application_Initializing_Form"
        Me.Opacity = 0.95R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Application_Initializing_Form"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.DarkGreen
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Download_PrgrsBr As ProgressBar
    Friend WithEvents Enter_Password_To_Pass_Lbl As Label
    Friend WithEvents User_Password_TxtBx As TextBox
    Friend WithEvents Exit_Btn As Button
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents PictureBox1 As PictureBox
End Class
