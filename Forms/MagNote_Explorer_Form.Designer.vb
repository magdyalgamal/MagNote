<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MagNote_Explorer_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MagNote_Explorer_Form))
        Me.Current_Path_Lbl = New System.Windows.Forms.Label()
        Me.Mag_Explorer_Directory_Contents_LstVw = New System.Windows.Forms.ListView()
        Me.Mag_Explorer_Directory_TrVw = New System.Windows.Forms.TreeView()
        Me.Go_To_Btn = New System.Windows.Forms.Button()
        Me.Current_Path_CmbBx = New System.Windows.Forms.ComboBox()
        Me.File_Label_To_Find_Lbl = New System.Windows.Forms.Label()
        Me.File_Name_Label_To_Find_TxtBx = New System.Windows.Forms.TextBox()
        Me.File_Name_Label_To_Find_Btn = New System.Windows.Forms.Button()
        Me.Find_All_Btn = New System.Windows.Forms.Button()
        Me.Form_ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MagNote_Header_Pnl = New System.Windows.Forms.Panel()
        Me.Maximize_Form_Btn = New System.Windows.Forms.Button()
        Me.Minimize_Form_Btn = New System.Windows.Forms.Button()
        Me.Exit_Form_Btn = New System.Windows.Forms.Button()
        Me.Infosysme_PctrBx = New System.Windows.Forms.PictureBox()
        Me.MagNote_File_Name_Lbl = New System.Windows.Forms.Label()
        Me.File_Label_To_Find_ChkBx = New System.Windows.Forms.CheckBox()
        Me.MagNote_Header_Pnl.SuspendLayout()
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Current_Path_Lbl
        '
        Me.Current_Path_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Current_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_Path_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Current_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_Path_Lbl.Location = New System.Drawing.Point(7, 85)
        Me.Current_Path_Lbl.Name = "Current_Path_Lbl"
        Me.Current_Path_Lbl.Size = New System.Drawing.Size(104, 27)
        Me.Current_Path_Lbl.TabIndex = 1180
        Me.Current_Path_Lbl.Text = "Current Path"
        Me.Current_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Mag_Explorer_Directory_Contents_LstVw
        '
        Me.Mag_Explorer_Directory_Contents_LstVw.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Mag_Explorer_Directory_Contents_LstVw.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Mag_Explorer_Directory_Contents_LstVw.FullRowSelect = True
        Me.Mag_Explorer_Directory_Contents_LstVw.HideSelection = False
        Me.Mag_Explorer_Directory_Contents_LstVw.Location = New System.Drawing.Point(328, 141)
        Me.Mag_Explorer_Directory_Contents_LstVw.MultiSelect = False
        Me.Mag_Explorer_Directory_Contents_LstVw.Name = "Mag_Explorer_Directory_Contents_LstVw"
        Me.Mag_Explorer_Directory_Contents_LstVw.Size = New System.Drawing.Size(607, 386)
        Me.Mag_Explorer_Directory_Contents_LstVw.TabIndex = 1179
        Me.Mag_Explorer_Directory_Contents_LstVw.UseCompatibleStateImageBehavior = False
        Me.Mag_Explorer_Directory_Contents_LstVw.View = System.Windows.Forms.View.SmallIcon
        '
        'Mag_Explorer_Directory_TrVw
        '
        Me.Mag_Explorer_Directory_TrVw.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Mag_Explorer_Directory_TrVw.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Mag_Explorer_Directory_TrVw.Location = New System.Drawing.Point(7, 113)
        Me.Mag_Explorer_Directory_TrVw.Name = "Mag_Explorer_Directory_TrVw"
        Me.Mag_Explorer_Directory_TrVw.Size = New System.Drawing.Size(320, 414)
        Me.Mag_Explorer_Directory_TrVw.TabIndex = 1178
        '
        'Go_To_Btn
        '
        Me.Go_To_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Go_To_Btn.BackgroundImage = CType(resources.GetObject("Go_To_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Go_To_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Go_To_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Go_To_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Go_To_Btn.Location = New System.Drawing.Point(907, 85)
        Me.Go_To_Btn.Name = "Go_To_Btn"
        Me.Go_To_Btn.Size = New System.Drawing.Size(28, 27)
        Me.Go_To_Btn.TabIndex = 1183
        Me.Go_To_Btn.UseVisualStyleBackColor = True
        '
        'Current_Path_CmbBx
        '
        Me.Current_Path_CmbBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Current_Path_CmbBx.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Current_Path_CmbBx.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_Path_CmbBx.FormattingEnabled = True
        Me.Current_Path_CmbBx.Location = New System.Drawing.Point(112, 85)
        Me.Current_Path_CmbBx.Name = "Current_Path_CmbBx"
        Me.Current_Path_CmbBx.Size = New System.Drawing.Size(794, 27)
        Me.Current_Path_CmbBx.TabIndex = 1184
        '
        'File_Label_To_Find_Lbl
        '
        Me.File_Label_To_Find_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.File_Label_To_Find_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.File_Label_To_Find_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.File_Label_To_Find_Lbl.Location = New System.Drawing.Point(328, 113)
        Me.File_Label_To_Find_Lbl.Name = "File_Label_To_Find_Lbl"
        Me.File_Label_To_Find_Lbl.Size = New System.Drawing.Size(140, 27)
        Me.File_Label_To_Find_Lbl.TabIndex = 1186
        Me.File_Label_To_Find_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'File_Name_Label_To_Find_TxtBx
        '
        Me.File_Name_Label_To_Find_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.File_Name_Label_To_Find_TxtBx.HideSelection = False
        Me.File_Name_Label_To_Find_TxtBx.Location = New System.Drawing.Point(469, 113)
        Me.File_Name_Label_To_Find_TxtBx.Multiline = True
        Me.File_Name_Label_To_Find_TxtBx.Name = "File_Name_Label_To_Find_TxtBx"
        Me.File_Name_Label_To_Find_TxtBx.Size = New System.Drawing.Size(408, 27)
        Me.File_Name_Label_To_Find_TxtBx.TabIndex = 1187
        '
        'File_Name_Label_To_Find_Btn
        '
        Me.File_Name_Label_To_Find_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.File_Name_Label_To_Find_Btn.BackgroundImage = CType(resources.GetObject("File_Name_Label_To_Find_Btn.BackgroundImage"), System.Drawing.Image)
        Me.File_Name_Label_To_Find_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.File_Name_Label_To_Find_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.File_Name_Label_To_Find_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.File_Name_Label_To_Find_Btn.Location = New System.Drawing.Point(907, 113)
        Me.File_Name_Label_To_Find_Btn.Name = "File_Name_Label_To_Find_Btn"
        Me.File_Name_Label_To_Find_Btn.Size = New System.Drawing.Size(28, 27)
        Me.File_Name_Label_To_Find_Btn.TabIndex = 1188
        Me.File_Name_Label_To_Find_Btn.UseVisualStyleBackColor = True
        '
        'Find_All_Btn
        '
        Me.Find_All_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Find_All_Btn.BackgroundImage = CType(resources.GetObject("Find_All_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Find_All_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Find_All_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Find_All_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Find_All_Btn.Location = New System.Drawing.Point(878, 113)
        Me.Find_All_Btn.Name = "Find_All_Btn"
        Me.Find_All_Btn.Size = New System.Drawing.Size(28, 27)
        Me.Find_All_Btn.TabIndex = 1189
        Me.Find_All_Btn.UseVisualStyleBackColor = True
        '
        'Form_ToolTip
        '
        Me.Form_ToolTip.BackColor = System.Drawing.Color.Yellow
        Me.Form_ToolTip.IsBalloon = True
        '
        'MagNote_Header_Pnl
        '
        Me.MagNote_Header_Pnl.BackColor = System.Drawing.Color.Snow
        Me.MagNote_Header_Pnl.BackgroundImage = Global.MagNote.My.Resources.Resources.MagNoteHeaderName12
        Me.MagNote_Header_Pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MagNote_Header_Pnl.Controls.Add(Me.Maximize_Form_Btn)
        Me.MagNote_Header_Pnl.Controls.Add(Me.Minimize_Form_Btn)
        Me.MagNote_Header_Pnl.Controls.Add(Me.Exit_Form_Btn)
        Me.MagNote_Header_Pnl.Controls.Add(Me.Infosysme_PctrBx)
        Me.MagNote_Header_Pnl.Controls.Add(Me.MagNote_File_Name_Lbl)
        Me.MagNote_Header_Pnl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_Header_Pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.MagNote_Header_Pnl.Location = New System.Drawing.Point(0, 0)
        Me.MagNote_Header_Pnl.Name = "MagNote_Header_Pnl"
        Me.MagNote_Header_Pnl.Size = New System.Drawing.Size(942, 79)
        Me.MagNote_Header_Pnl.TabIndex = 1191
        '
        'Maximize_Form_Btn
        '
        Me.Maximize_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Maximize_Form_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Maximize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.upgrade
        Me.Maximize_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Maximize_Form_Btn.Enabled = False
        Me.Maximize_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Maximize_Form_Btn.Location = New System.Drawing.Point(845, 27)
        Me.Maximize_Form_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Maximize_Form_Btn.Name = "Maximize_Form_Btn"
        Me.Maximize_Form_Btn.Size = New System.Drawing.Size(32, 30)
        Me.Maximize_Form_Btn.TabIndex = 1191
        Me.Maximize_Form_Btn.UseVisualStyleBackColor = False
        '
        'Minimize_Form_Btn
        '
        Me.Minimize_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Minimize_Form_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Minimize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Minimize1
        Me.Minimize_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Minimize_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Minimize_Form_Btn.Location = New System.Drawing.Point(812, 27)
        Me.Minimize_Form_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Minimize_Form_Btn.Name = "Minimize_Form_Btn"
        Me.Minimize_Form_Btn.Size = New System.Drawing.Size(32, 30)
        Me.Minimize_Form_Btn.TabIndex = 1190
        Me.Minimize_Form_Btn.UseVisualStyleBackColor = False
        '
        'Exit_Form_Btn
        '
        Me.Exit_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exit_Form_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Exit_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.PwerOff
        Me.Exit_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Exit_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Exit_Form_Btn.Location = New System.Drawing.Point(878, 27)
        Me.Exit_Form_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Exit_Form_Btn.Name = "Exit_Form_Btn"
        Me.Exit_Form_Btn.Size = New System.Drawing.Size(32, 30)
        Me.Exit_Form_Btn.TabIndex = 1189
        Me.Exit_Form_Btn.UseVisualStyleBackColor = False
        '
        'Infosysme_PctrBx
        '
        Me.Infosysme_PctrBx.BackColor = System.Drawing.Color.Transparent
        Me.Infosysme_PctrBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Infosysme_PctrBx.Image = Global.MagNote.My.Resources.Resources.MagNoteHeaderName17
        Me.Infosysme_PctrBx.Location = New System.Drawing.Point(1, 0)
        Me.Infosysme_PctrBx.Name = "Infosysme_PctrBx"
        Me.Infosysme_PctrBx.Size = New System.Drawing.Size(366, 75)
        Me.Infosysme_PctrBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Infosysme_PctrBx.TabIndex = 1188
        Me.Infosysme_PctrBx.TabStop = False
        '
        'MagNote_File_Name_Lbl
        '
        Me.MagNote_File_Name_Lbl.AutoSize = True
        Me.MagNote_File_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.MagNote_File_Name_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_File_Name_Lbl.Location = New System.Drawing.Point(463, 32)
        Me.MagNote_File_Name_Lbl.Name = "MagNote_File_Name_Lbl"
        Me.MagNote_File_Name_Lbl.Size = New System.Drawing.Size(0, 15)
        Me.MagNote_File_Name_Lbl.TabIndex = 1181
        Me.MagNote_File_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'File_Label_To_Find_ChkBx
        '
        Me.File_Label_To_Find_ChkBx.BackColor = System.Drawing.Color.Transparent
        Me.File_Label_To_Find_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.File_Label_To_Find_ChkBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.File_Label_To_Find_ChkBx.Location = New System.Drawing.Point(329, 115)
        Me.File_Label_To_Find_ChkBx.Name = "File_Label_To_Find_ChkBx"
        Me.File_Label_To_Find_ChkBx.Size = New System.Drawing.Size(134, 23)
        Me.File_Label_To_Find_ChkBx.TabIndex = 1192
        Me.File_Label_To_Find_ChkBx.Text = "Label To Find"
        Me.File_Label_To_Find_ChkBx.UseVisualStyleBackColor = False
        '
        'MagNote_Explorer_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(942, 535)
        Me.Controls.Add(Me.File_Label_To_Find_ChkBx)
        Me.Controls.Add(Me.MagNote_Header_Pnl)
        Me.Controls.Add(Me.Go_To_Btn)
        Me.Controls.Add(Me.Current_Path_Lbl)
        Me.Controls.Add(Me.Current_Path_CmbBx)
        Me.Controls.Add(Me.Find_All_Btn)
        Me.Controls.Add(Me.File_Name_Label_To_Find_Btn)
        Me.Controls.Add(Me.File_Name_Label_To_Find_TxtBx)
        Me.Controls.Add(Me.Mag_Explorer_Directory_Contents_LstVw)
        Me.Controls.Add(Me.Mag_Explorer_Directory_TrVw)
        Me.Controls.Add(Me.File_Label_To_Find_Lbl)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MagNote_Explorer_Form"
        Me.Opacity = 0.95R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MagNote Explorer Form"
        Me.TransparencyKey = System.Drawing.Color.Snow
        Me.MagNote_Header_Pnl.ResumeLayout(False)
        Me.MagNote_Header_Pnl.PerformLayout()
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Current_Path_Lbl As Label
    Friend WithEvents Mag_Explorer_Directory_Contents_LstVw As ListView
    Friend WithEvents Mag_Explorer_Directory_TrVw As TreeView
    Friend WithEvents Go_To_Btn As Button
    Friend WithEvents Current_Path_CmbBx As ComboBox
    Friend WithEvents File_Label_To_Find_Lbl As Label
    Friend WithEvents File_Name_Label_To_Find_TxtBx As TextBox
    Friend WithEvents File_Name_Label_To_Find_Btn As Button
    Friend WithEvents Find_All_Btn As Button
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents MagNote_Header_Pnl As Panel
    Friend WithEvents MagNote_File_Name_Lbl As Label
    Friend WithEvents Infosysme_PctrBx As PictureBox
    Friend WithEvents Maximize_Form_Btn As Button
    Friend WithEvents Minimize_Form_Btn As Button
    Friend WithEvents Exit_Form_Btn As Button
    Friend WithEvents File_Label_To_Find_ChkBx As CheckBox
End Class
