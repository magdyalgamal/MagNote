<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Update_New_Version_Form
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Update_New_Version_Form))
        Me.Download_PrgrsBr = New System.Windows.Forms.ProgressBar()
        Me.Download_Progress_Percentage_Lbl = New System.Windows.Forms.Label()
        Me.Download_Progress_Percentage_TxtBx = New System.Windows.Forms.TextBox()
        Me.Download_Update_Btn = New System.Windows.Forms.Button()
        Me.File_Name_TxtBx = New System.Windows.Forms.TextBox()
        Me.File_Name_Lbl = New System.Windows.Forms.Label()
        Me.Current_Version_Lbl = New System.Windows.Forms.Label()
        Me.Current_Version_TxtBx = New System.Windows.Forms.TextBox()
        Me.Update_Version_Lbl = New System.Windows.Forms.Label()
        Me.Update_Version_TxtBx = New System.Windows.Forms.TextBox()
        Me.Update_Download_File_Path_Lbl = New System.Windows.Forms.Label()
        Me.Update_Download_File_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Update_Download_File_Path_Btn = New System.Windows.Forms.Button()
        Me.Form_ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MagNote_Header_Pnl = New System.Windows.Forms.Panel()
        Me.MagNote_File_Name_Lbl = New System.Windows.Forms.Label()
        Me.Form_Controls_Pnl = New System.Windows.Forms.Panel()
        Me.Infosysme_PctrBx = New System.Windows.Forms.PictureBox()
        Me.Maximize_Form_Btn = New System.Windows.Forms.Button()
        Me.Minimize_Form_Btn = New System.Windows.Forms.Button()
        Me.Exit_Form_Btn = New System.Windows.Forms.Button()
        Me.MagNote_Header_Pnl.SuspendLayout()
        Me.Form_Controls_Pnl.SuspendLayout()
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Download_PrgrsBr
        '
        Me.Download_PrgrsBr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_PrgrsBr.Location = New System.Drawing.Point(18, 266)
        Me.Download_PrgrsBr.Name = "Download_PrgrsBr"
        Me.Download_PrgrsBr.Size = New System.Drawing.Size(549, 18)
        Me.Download_PrgrsBr.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Download_PrgrsBr.TabIndex = 0
        Me.Download_PrgrsBr.Visible = False
        '
        'Download_Progress_Percentage_Lbl
        '
        Me.Download_Progress_Percentage_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Download_Progress_Percentage_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Progress_Percentage_Lbl.Location = New System.Drawing.Point(18, 244)
        Me.Download_Progress_Percentage_Lbl.Name = "Download_Progress_Percentage_Lbl"
        Me.Download_Progress_Percentage_Lbl.Size = New System.Drawing.Size(136, 21)
        Me.Download_Progress_Percentage_Lbl.TabIndex = 1
        Me.Download_Progress_Percentage_Lbl.Text = "Percentage"
        Me.Download_Progress_Percentage_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Download_Progress_Percentage_TxtBx
        '
        Me.Download_Progress_Percentage_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_Progress_Percentage_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Progress_Percentage_TxtBx.Location = New System.Drawing.Point(155, 244)
        Me.Download_Progress_Percentage_TxtBx.Multiline = True
        Me.Download_Progress_Percentage_TxtBx.Name = "Download_Progress_Percentage_TxtBx"
        Me.Download_Progress_Percentage_TxtBx.ReadOnly = True
        Me.Download_Progress_Percentage_TxtBx.Size = New System.Drawing.Size(412, 21)
        Me.Download_Progress_Percentage_TxtBx.TabIndex = 2
        Me.Download_Progress_Percentage_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Download_Update_Btn
        '
        Me.Download_Update_Btn.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Update_Btn.Location = New System.Drawing.Point(197, 295)
        Me.Download_Update_Btn.Name = "Download_Update_Btn"
        Me.Download_Update_Btn.Size = New System.Drawing.Size(190, 27)
        Me.Download_Update_Btn.TabIndex = 3
        Me.Download_Update_Btn.Text = "Download Update"
        Me.Download_Update_Btn.UseVisualStyleBackColor = True
        '
        'File_Name_TxtBx
        '
        Me.File_Name_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.File_Name_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.File_Name_TxtBx.Location = New System.Drawing.Point(155, 95)
        Me.File_Name_TxtBx.Multiline = True
        Me.File_Name_TxtBx.Name = "File_Name_TxtBx"
        Me.File_Name_TxtBx.ReadOnly = True
        Me.File_Name_TxtBx.Size = New System.Drawing.Size(412, 82)
        Me.File_Name_TxtBx.TabIndex = 4
        '
        'File_Name_Lbl
        '
        Me.File_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.File_Name_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.File_Name_Lbl.Location = New System.Drawing.Point(18, 95)
        Me.File_Name_Lbl.Name = "File_Name_Lbl"
        Me.File_Name_Lbl.Size = New System.Drawing.Size(136, 82)
        Me.File_Name_Lbl.TabIndex = 5
        Me.File_Name_Lbl.Text = "File Name"
        Me.File_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Current_Version_Lbl
        '
        Me.Current_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_Version_Lbl.Location = New System.Drawing.Point(18, 178)
        Me.Current_Version_Lbl.Name = "Current_Version_Lbl"
        Me.Current_Version_Lbl.Size = New System.Drawing.Size(136, 21)
        Me.Current_Version_Lbl.TabIndex = 7
        Me.Current_Version_Lbl.Text = "Current Version"
        Me.Current_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Current_Version_TxtBx
        '
        Me.Current_Version_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Current_Version_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_Version_TxtBx.Location = New System.Drawing.Point(155, 178)
        Me.Current_Version_TxtBx.Multiline = True
        Me.Current_Version_TxtBx.Name = "Current_Version_TxtBx"
        Me.Current_Version_TxtBx.ReadOnly = True
        Me.Current_Version_TxtBx.Size = New System.Drawing.Size(412, 21)
        Me.Current_Version_TxtBx.TabIndex = 6
        Me.Current_Version_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_Version_Lbl
        '
        Me.Update_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Update_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Version_Lbl.Location = New System.Drawing.Point(18, 200)
        Me.Update_Version_Lbl.Name = "Update_Version_Lbl"
        Me.Update_Version_Lbl.Size = New System.Drawing.Size(136, 21)
        Me.Update_Version_Lbl.TabIndex = 9
        Me.Update_Version_Lbl.Text = "Update Version"
        Me.Update_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Update_Version_TxtBx
        '
        Me.Update_Version_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update_Version_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Version_TxtBx.Location = New System.Drawing.Point(155, 200)
        Me.Update_Version_TxtBx.Multiline = True
        Me.Update_Version_TxtBx.Name = "Update_Version_TxtBx"
        Me.Update_Version_TxtBx.ReadOnly = True
        Me.Update_Version_TxtBx.Size = New System.Drawing.Size(412, 21)
        Me.Update_Version_TxtBx.TabIndex = 8
        Me.Update_Version_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_Download_File_Path_Lbl
        '
        Me.Update_Download_File_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Update_Download_File_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Download_File_Path_Lbl.Location = New System.Drawing.Point(18, 222)
        Me.Update_Download_File_Path_Lbl.Name = "Update_Download_File_Path_Lbl"
        Me.Update_Download_File_Path_Lbl.Size = New System.Drawing.Size(136, 21)
        Me.Update_Download_File_Path_Lbl.TabIndex = 11
        Me.Update_Download_File_Path_Lbl.Text = "Download Files Path"
        Me.Update_Download_File_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Update_Download_File_Path_TxtBx
        '
        Me.Update_Download_File_Path_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update_Download_File_Path_TxtBx.Enabled = False
        Me.Update_Download_File_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Download_File_Path_TxtBx.Location = New System.Drawing.Point(177, 222)
        Me.Update_Download_File_Path_TxtBx.Multiline = True
        Me.Update_Download_File_Path_TxtBx.Name = "Update_Download_File_Path_TxtBx"
        Me.Update_Download_File_Path_TxtBx.ReadOnly = True
        Me.Update_Download_File_Path_TxtBx.Size = New System.Drawing.Size(390, 21)
        Me.Update_Download_File_Path_TxtBx.TabIndex = 10
        Me.Update_Download_File_Path_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_Download_File_Path_Btn
        '
        Me.Update_Download_File_Path_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.folder
        Me.Update_Download_File_Path_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Update_Download_File_Path_Btn.Location = New System.Drawing.Point(154, 221)
        Me.Update_Download_File_Path_Btn.Name = "Update_Download_File_Path_Btn"
        Me.Update_Download_File_Path_Btn.Size = New System.Drawing.Size(23, 23)
        Me.Update_Download_File_Path_Btn.TabIndex = 12
        Me.Update_Download_File_Path_Btn.UseVisualStyleBackColor = True
        '
        'Form_ToolTip
        '
        Me.Form_ToolTip.BackColor = System.Drawing.Color.Yellow
        Me.Form_ToolTip.IsBalloon = True
        '
        'MagNote_Header_Pnl
        '
        Me.MagNote_Header_Pnl.BackColor = System.Drawing.Color.Snow
        Me.MagNote_Header_Pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.MagNote_Header_Pnl.Controls.Add(Me.MagNote_File_Name_Lbl)
        Me.MagNote_Header_Pnl.Controls.Add(Me.Form_Controls_Pnl)
        Me.MagNote_Header_Pnl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_Header_Pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.MagNote_Header_Pnl.Location = New System.Drawing.Point(0, 0)
        Me.MagNote_Header_Pnl.Name = "MagNote_Header_Pnl"
        Me.MagNote_Header_Pnl.Size = New System.Drawing.Size(584, 79)
        Me.MagNote_Header_Pnl.TabIndex = 1191
        '
        'MagNote_File_Name_Lbl
        '
        Me.MagNote_File_Name_Lbl.AutoSize = True
        Me.MagNote_File_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.MagNote_File_Name_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_File_Name_Lbl.Location = New System.Drawing.Point(463, 32)
        Me.MagNote_File_Name_Lbl.Name = "MagNote_File_Name_Lbl"
        Me.MagNote_File_Name_Lbl.Size = New System.Drawing.Size(0, 13)
        Me.MagNote_File_Name_Lbl.TabIndex = 1181
        Me.MagNote_File_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form_Controls_Pnl
        '
        Me.Form_Controls_Pnl.BackColor = System.Drawing.Color.Transparent
        Me.Form_Controls_Pnl.BackgroundImage = Global.MagNote.My.Resources.Resources.MagNoteHeaderName12
        Me.Form_Controls_Pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Form_Controls_Pnl.Controls.Add(Me.Infosysme_PctrBx)
        Me.Form_Controls_Pnl.Controls.Add(Me.Maximize_Form_Btn)
        Me.Form_Controls_Pnl.Controls.Add(Me.Minimize_Form_Btn)
        Me.Form_Controls_Pnl.Controls.Add(Me.Exit_Form_Btn)
        Me.Form_Controls_Pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Form_Controls_Pnl.Location = New System.Drawing.Point(0, 0)
        Me.Form_Controls_Pnl.Name = "Form_Controls_Pnl"
        Me.Form_Controls_Pnl.Size = New System.Drawing.Size(584, 79)
        Me.Form_Controls_Pnl.TabIndex = 0
        '
        'Infosysme_PctrBx
        '
        Me.Infosysme_PctrBx.BackColor = System.Drawing.Color.Transparent
        Me.Infosysme_PctrBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Infosysme_PctrBx.Image = Global.MagNote.My.Resources.Resources.MagNoteHeaderName17
        Me.Infosysme_PctrBx.Location = New System.Drawing.Point(1, 0)
        Me.Infosysme_PctrBx.Name = "Infosysme_PctrBx"
        Me.Infosysme_PctrBx.Size = New System.Drawing.Size(366, 79)
        Me.Infosysme_PctrBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Infosysme_PctrBx.TabIndex = 1188
        Me.Infosysme_PctrBx.TabStop = False
        '
        'Maximize_Form_Btn
        '
        Me.Maximize_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Maximize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.upgrade
        Me.Maximize_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Maximize_Form_Btn.Enabled = False
        Me.Maximize_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Maximize_Form_Btn.Location = New System.Drawing.Point(526, 22)
        Me.Maximize_Form_Btn.Name = "Maximize_Form_Btn"
        Me.Maximize_Form_Btn.Size = New System.Drawing.Size(24, 24)
        Me.Maximize_Form_Btn.TabIndex = 1186
        Me.Maximize_Form_Btn.UseVisualStyleBackColor = True
        '
        'Minimize_Form_Btn
        '
        Me.Minimize_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Minimize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Minimize1
        Me.Minimize_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Minimize_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Minimize_Form_Btn.Location = New System.Drawing.Point(502, 22)
        Me.Minimize_Form_Btn.Name = "Minimize_Form_Btn"
        Me.Minimize_Form_Btn.Size = New System.Drawing.Size(24, 24)
        Me.Minimize_Form_Btn.TabIndex = 1185
        Me.Minimize_Form_Btn.UseVisualStyleBackColor = True
        '
        'Exit_Form_Btn
        '
        Me.Exit_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exit_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.PwerOff
        Me.Exit_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Exit_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Exit_Form_Btn.Location = New System.Drawing.Point(551, 22)
        Me.Exit_Form_Btn.Name = "Exit_Form_Btn"
        Me.Exit_Form_Btn.Size = New System.Drawing.Size(24, 24)
        Me.Exit_Form_Btn.TabIndex = 1184
        Me.Exit_Form_Btn.UseVisualStyleBackColor = True
        '
        'Update_New_Version_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 339)
        Me.Controls.Add(Me.MagNote_Header_Pnl)
        Me.Controls.Add(Me.Update_Download_File_Path_Btn)
        Me.Controls.Add(Me.Update_Download_File_Path_Lbl)
        Me.Controls.Add(Me.Update_Download_File_Path_TxtBx)
        Me.Controls.Add(Me.Update_Version_Lbl)
        Me.Controls.Add(Me.Update_Version_TxtBx)
        Me.Controls.Add(Me.Current_Version_Lbl)
        Me.Controls.Add(Me.Current_Version_TxtBx)
        Me.Controls.Add(Me.File_Name_Lbl)
        Me.Controls.Add(Me.File_Name_TxtBx)
        Me.Controls.Add(Me.Download_Update_Btn)
        Me.Controls.Add(Me.Download_Progress_Percentage_TxtBx)
        Me.Controls.Add(Me.Download_Progress_Percentage_Lbl)
        Me.Controls.Add(Me.Download_PrgrsBr)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Update_New_Version_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update New Version"
        Me.TransparencyKey = System.Drawing.Color.Snow
        Me.MagNote_Header_Pnl.ResumeLayout(False)
        Me.MagNote_Header_Pnl.PerformLayout()
        Me.Form_Controls_Pnl.ResumeLayout(False)
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Download_PrgrsBr As ProgressBar
    Friend WithEvents Download_Progress_Percentage_Lbl As Label
    Friend WithEvents Download_Progress_Percentage_TxtBx As TextBox
    Friend WithEvents Download_Update_Btn As Button
    Friend WithEvents File_Name_TxtBx As TextBox
    Friend WithEvents File_Name_Lbl As Label
    Friend WithEvents Current_Version_Lbl As Label
    Friend WithEvents Current_Version_TxtBx As TextBox
    Friend WithEvents Update_Version_Lbl As Label
    Friend WithEvents Update_Version_TxtBx As TextBox
    Friend WithEvents Update_Download_File_Path_Lbl As Label
    Friend WithEvents Update_Download_File_Path_TxtBx As TextBox
    Friend WithEvents Update_Download_File_Path_Btn As Button
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents MagNote_Header_Pnl As Panel
    Friend WithEvents MagNote_File_Name_Lbl As Label
    Friend WithEvents Form_Controls_Pnl As Panel
    Friend WithEvents Maximize_Form_Btn As Button
    Friend WithEvents Minimize_Form_Btn As Button
    Friend WithEvents Exit_Form_Btn As Button
    Friend WithEvents Infosysme_PctrBx As PictureBox
End Class
