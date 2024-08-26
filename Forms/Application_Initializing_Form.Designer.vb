<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Application_Initializing_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Application_Initializing_Form))
        Me.Download_PrgrsBr = New System.Windows.Forms.ProgressBar()
        Me.Enter_Password_To_Pass_Lbl = New System.Windows.Forms.Label()
        Me.User_Password_TxtBx = New System.Windows.Forms.TextBox()
        Me.Exit_Btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Download_PrgrsBr
        '
        Me.Download_PrgrsBr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_PrgrsBr.Location = New System.Drawing.Point(36, 91)
        Me.Download_PrgrsBr.Name = "Download_PrgrsBr"
        Me.Download_PrgrsBr.Size = New System.Drawing.Size(186, 18)
        Me.Download_PrgrsBr.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Download_PrgrsBr.TabIndex = 5
        Me.Download_PrgrsBr.Visible = False
        '
        'Enter_Password_To_Pass_Lbl
        '
        Me.Enter_Password_To_Pass_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Enter_Password_To_Pass_Lbl.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Enter_Password_To_Pass_Lbl.Location = New System.Drawing.Point(43, 127)
        Me.Enter_Password_To_Pass_Lbl.Name = "Enter_Password_To_Pass_Lbl"
        Me.Enter_Password_To_Pass_Lbl.Size = New System.Drawing.Size(176, 20)
        Me.Enter_Password_To_Pass_Lbl.TabIndex = 2
        Me.Enter_Password_To_Pass_Lbl.Text = "Enter Password To Pass"
        '
        'User_Password_TxtBx
        '
        Me.User_Password_TxtBx.Location = New System.Drawing.Point(220, 127)
        Me.User_Password_TxtBx.Name = "User_Password_TxtBx"
        Me.User_Password_TxtBx.Size = New System.Drawing.Size(190, 20)
        Me.User_Password_TxtBx.TabIndex = 1
        Me.User_Password_TxtBx.UseSystemPasswordChar = True
        '
        'Exit_Btn
        '
        Me.Exit_Btn.Location = New System.Drawing.Point(335, 155)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.Size = New System.Drawing.Size(75, 23)
        Me.Exit_Btn.TabIndex = 4
        Me.Exit_Btn.Text = "Exit خروج"
        Me.Exit_Btn.UseVisualStyleBackColor = True
        '
        'Application_Initializing_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkGreen
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(461, 123)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.User_Password_TxtBx)
        Me.Controls.Add(Me.Enter_Password_To_Pass_Lbl)
        Me.Controls.Add(Me.Download_PrgrsBr)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Application_Initializing_Form"
        Me.Opacity = 0.95R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Application_Initializing_Form"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.DarkGreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Download_PrgrsBr As ProgressBar
    Friend WithEvents Enter_Password_To_Pass_Lbl As Label
    Friend WithEvents User_Password_TxtBx As TextBox
    Friend WithEvents Exit_Btn As Button
End Class
