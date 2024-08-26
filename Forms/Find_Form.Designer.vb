<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Find_Form
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
        Me.FindIn_CmbBx = New System.Windows.Forms.ComboBox()
        Me.FindIn_Lbl = New System.Windows.Forms.Label()
        Me.FindText_CmbBx = New System.Windows.Forms.ComboBox()
        Me.FindText_Lbl = New System.Windows.Forms.Label()
        Me.ReplaceBy_Lbl = New System.Windows.Forms.Label()
        Me.ReplaceBy_TxtBx = New System.Windows.Forms.TextBox()
        Me.FindNext_Btn = New System.Windows.Forms.Button()
        Me.FindAll_Btn = New System.Windows.Forms.Button()
        Me.ReplaceNext_Btn = New System.Windows.Forms.Button()
        Me.ReplaceAll_Btn = New System.Windows.Forms.Button()
        Me.Clear_Search_Result_Btn = New System.Windows.Forms.Button()
        Me.Exit_Btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'FindIn_CmbBx
        '
        Me.FindIn_CmbBx.Font = New System.Drawing.Font("Times New Roman", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FindIn_CmbBx.FormattingEnabled = True
        Me.FindIn_CmbBx.Location = New System.Drawing.Point(142, 9)
        Me.FindIn_CmbBx.Name = "FindIn_CmbBx"
        Me.FindIn_CmbBx.Size = New System.Drawing.Size(196, 20)
        Me.FindIn_CmbBx.TabIndex = 991
        '
        'FindIn_Lbl
        '
        Me.FindIn_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.FindIn_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FindIn_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.FindIn_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FindIn_Lbl.Location = New System.Drawing.Point(7, 9)
        Me.FindIn_Lbl.Name = "FindIn_Lbl"
        Me.FindIn_Lbl.Size = New System.Drawing.Size(134, 20)
        Me.FindIn_Lbl.TabIndex = 990
        Me.FindIn_Lbl.Text = "Find In"
        Me.FindIn_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FindText_CmbBx
        '
        Me.FindText_CmbBx.Font = New System.Drawing.Font("Times New Roman", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FindText_CmbBx.FormattingEnabled = True
        Me.FindText_CmbBx.Location = New System.Drawing.Point(142, 30)
        Me.FindText_CmbBx.Name = "FindText_CmbBx"
        Me.FindText_CmbBx.Size = New System.Drawing.Size(361, 20)
        Me.FindText_CmbBx.TabIndex = 993
        '
        'FindText_Lbl
        '
        Me.FindText_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.FindText_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FindText_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.FindText_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FindText_Lbl.Location = New System.Drawing.Point(7, 30)
        Me.FindText_Lbl.Name = "FindText_Lbl"
        Me.FindText_Lbl.Size = New System.Drawing.Size(134, 20)
        Me.FindText_Lbl.TabIndex = 992
        Me.FindText_Lbl.Text = "Find Text"
        Me.FindText_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ReplaceBy_Lbl
        '
        Me.ReplaceBy_Lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ReplaceBy_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.ReplaceBy_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ReplaceBy_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ReplaceBy_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ReplaceBy_Lbl.Location = New System.Drawing.Point(7, 51)
        Me.ReplaceBy_Lbl.Name = "ReplaceBy_Lbl"
        Me.ReplaceBy_Lbl.Size = New System.Drawing.Size(134, 20)
        Me.ReplaceBy_Lbl.TabIndex = 994
        Me.ReplaceBy_Lbl.Text = "Replace By"
        Me.ReplaceBy_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ReplaceBy_TxtBx
        '
        Me.ReplaceBy_TxtBx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReplaceBy_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.ReplaceBy_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ReplaceBy_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ReplaceBy_TxtBx.Location = New System.Drawing.Point(142, 51)
        Me.ReplaceBy_TxtBx.Multiline = True
        Me.ReplaceBy_TxtBx.Name = "ReplaceBy_TxtBx"
        Me.ReplaceBy_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ReplaceBy_TxtBx.Size = New System.Drawing.Size(361, 20)
        Me.ReplaceBy_TxtBx.TabIndex = 995
        '
        'FindNext_Btn
        '
        Me.FindNext_Btn.Location = New System.Drawing.Point(6, 71)
        Me.FindNext_Btn.Name = "FindNext_Btn"
        Me.FindNext_Btn.Size = New System.Drawing.Size(83, 36)
        Me.FindNext_Btn.TabIndex = 996
        Me.FindNext_Btn.Text = "Find Next"
        Me.FindNext_Btn.UseVisualStyleBackColor = True
        '
        'FindAll_Btn
        '
        Me.FindAll_Btn.Location = New System.Drawing.Point(89, 71)
        Me.FindAll_Btn.Name = "FindAll_Btn"
        Me.FindAll_Btn.Size = New System.Drawing.Size(83, 36)
        Me.FindAll_Btn.TabIndex = 997
        Me.FindAll_Btn.Text = "Find All"
        Me.FindAll_Btn.UseVisualStyleBackColor = True
        '
        'ReplaceNext_Btn
        '
        Me.ReplaceNext_Btn.Location = New System.Drawing.Point(172, 71)
        Me.ReplaceNext_Btn.Name = "ReplaceNext_Btn"
        Me.ReplaceNext_Btn.Size = New System.Drawing.Size(83, 36)
        Me.ReplaceNext_Btn.TabIndex = 998
        Me.ReplaceNext_Btn.Text = "Replace Next"
        Me.ReplaceNext_Btn.UseVisualStyleBackColor = True
        '
        'ReplaceAll_Btn
        '
        Me.ReplaceAll_Btn.Location = New System.Drawing.Point(255, 71)
        Me.ReplaceAll_Btn.Name = "ReplaceAll_Btn"
        Me.ReplaceAll_Btn.Size = New System.Drawing.Size(83, 36)
        Me.ReplaceAll_Btn.TabIndex = 999
        Me.ReplaceAll_Btn.Text = "Replace All"
        Me.ReplaceAll_Btn.UseVisualStyleBackColor = True
        '
        'Clear_Search_Result_Btn
        '
        Me.Clear_Search_Result_Btn.Location = New System.Drawing.Point(338, 71)
        Me.Clear_Search_Result_Btn.Name = "Clear_Search_Result_Btn"
        Me.Clear_Search_Result_Btn.Size = New System.Drawing.Size(83, 36)
        Me.Clear_Search_Result_Btn.TabIndex = 1000
        Me.Clear_Search_Result_Btn.Text = "Clear Search Result"
        Me.Clear_Search_Result_Btn.UseVisualStyleBackColor = True
        '
        'Exit_Btn
        '
        Me.Exit_Btn.Location = New System.Drawing.Point(421, 71)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.Size = New System.Drawing.Size(83, 36)
        Me.Exit_Btn.TabIndex = 1001
        Me.Exit_Btn.Text = "Exit"
        Me.Exit_Btn.UseVisualStyleBackColor = True
        '
        'Find_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 116)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.Clear_Search_Result_Btn)
        Me.Controls.Add(Me.ReplaceAll_Btn)
        Me.Controls.Add(Me.ReplaceNext_Btn)
        Me.Controls.Add(Me.FindAll_Btn)
        Me.Controls.Add(Me.FindNext_Btn)
        Me.Controls.Add(Me.ReplaceBy_Lbl)
        Me.Controls.Add(Me.ReplaceBy_TxtBx)
        Me.Controls.Add(Me.FindText_CmbBx)
        Me.Controls.Add(Me.FindText_Lbl)
        Me.Controls.Add(Me.FindIn_CmbBx)
        Me.Controls.Add(Me.FindIn_Lbl)
        Me.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Find_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find_Form"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FindIn_CmbBx As ComboBox
    Friend WithEvents FindIn_Lbl As Label
    Friend WithEvents FindText_CmbBx As ComboBox
    Friend WithEvents FindText_Lbl As Label
    Friend WithEvents ReplaceBy_Lbl As Label
    Friend WithEvents ReplaceBy_TxtBx As TextBox
    Friend WithEvents FindNext_Btn As Button
    Friend WithEvents FindAll_Btn As Button
    Friend WithEvents ReplaceNext_Btn As Button
    Friend WithEvents ReplaceAll_Btn As Button
    Friend WithEvents Clear_Search_Result_Btn As Button
    Friend WithEvents Exit_Btn As Button
End Class
