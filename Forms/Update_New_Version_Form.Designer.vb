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
        Me.SuspendLayout()
        '
        'Download_PrgrsBr
        '
        Me.Download_PrgrsBr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_PrgrsBr.Location = New System.Drawing.Point(35, 161)
        Me.Download_PrgrsBr.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Download_PrgrsBr.Name = "Download_PrgrsBr"
        Me.Download_PrgrsBr.Size = New System.Drawing.Size(456, 22)
        Me.Download_PrgrsBr.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.Download_PrgrsBr.TabIndex = 0
        Me.Download_PrgrsBr.Visible = False
        '
        'Download_Progress_Percentage_Lbl
        '
        Me.Download_Progress_Percentage_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Download_Progress_Percentage_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Progress_Percentage_Lbl.Location = New System.Drawing.Point(35, 134)
        Me.Download_Progress_Percentage_Lbl.Name = "Download_Progress_Percentage_Lbl"
        Me.Download_Progress_Percentage_Lbl.Size = New System.Drawing.Size(129, 25)
        Me.Download_Progress_Percentage_Lbl.TabIndex = 1
        Me.Download_Progress_Percentage_Lbl.Text = "Percentage"
        Me.Download_Progress_Percentage_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Download_Progress_Percentage_TxtBx
        '
        Me.Download_Progress_Percentage_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download_Progress_Percentage_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Progress_Percentage_TxtBx.Location = New System.Drawing.Point(166, 134)
        Me.Download_Progress_Percentage_TxtBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Download_Progress_Percentage_TxtBx.Multiline = True
        Me.Download_Progress_Percentage_TxtBx.Name = "Download_Progress_Percentage_TxtBx"
        Me.Download_Progress_Percentage_TxtBx.ReadOnly = True
        Me.Download_Progress_Percentage_TxtBx.Size = New System.Drawing.Size(325, 25)
        Me.Download_Progress_Percentage_TxtBx.TabIndex = 2
        Me.Download_Progress_Percentage_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Download_Update_Btn
        '
        Me.Download_Update_Btn.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Download_Update_Btn.Location = New System.Drawing.Point(152, 197)
        Me.Download_Update_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Download_Update_Btn.Name = "Download_Update_Btn"
        Me.Download_Update_Btn.Size = New System.Drawing.Size(222, 33)
        Me.Download_Update_Btn.TabIndex = 3
        Me.Download_Update_Btn.Text = "Download Update"
        Me.Download_Update_Btn.UseVisualStyleBackColor = True
        '
        'File_Name_TxtBx
        '
        Me.File_Name_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.File_Name_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.File_Name_TxtBx.Location = New System.Drawing.Point(166, 26)
        Me.File_Name_TxtBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.File_Name_TxtBx.Multiline = True
        Me.File_Name_TxtBx.Name = "File_Name_TxtBx"
        Me.File_Name_TxtBx.ReadOnly = True
        Me.File_Name_TxtBx.Size = New System.Drawing.Size(325, 25)
        Me.File_Name_TxtBx.TabIndex = 4
        Me.File_Name_TxtBx.Text = "MagNote.rar"
        Me.File_Name_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'File_Name_Lbl
        '
        Me.File_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.File_Name_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.File_Name_Lbl.Location = New System.Drawing.Point(35, 26)
        Me.File_Name_Lbl.Name = "File_Name_Lbl"
        Me.File_Name_Lbl.Size = New System.Drawing.Size(129, 25)
        Me.File_Name_Lbl.TabIndex = 5
        Me.File_Name_Lbl.Text = "File Name"
        Me.File_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Current_Version_Lbl
        '
        Me.Current_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_Version_Lbl.Location = New System.Drawing.Point(35, 53)
        Me.Current_Version_Lbl.Name = "Current_Version_Lbl"
        Me.Current_Version_Lbl.Size = New System.Drawing.Size(129, 25)
        Me.Current_Version_Lbl.TabIndex = 7
        Me.Current_Version_Lbl.Text = "Current Version"
        Me.Current_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Current_Version_TxtBx
        '
        Me.Current_Version_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Current_Version_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Current_Version_TxtBx.Location = New System.Drawing.Point(166, 53)
        Me.Current_Version_TxtBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Current_Version_TxtBx.Multiline = True
        Me.Current_Version_TxtBx.Name = "Current_Version_TxtBx"
        Me.Current_Version_TxtBx.ReadOnly = True
        Me.Current_Version_TxtBx.Size = New System.Drawing.Size(325, 25)
        Me.Current_Version_TxtBx.TabIndex = 6
        Me.Current_Version_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_Version_Lbl
        '
        Me.Update_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Update_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Version_Lbl.Location = New System.Drawing.Point(35, 80)
        Me.Update_Version_Lbl.Name = "Update_Version_Lbl"
        Me.Update_Version_Lbl.Size = New System.Drawing.Size(129, 25)
        Me.Update_Version_Lbl.TabIndex = 9
        Me.Update_Version_Lbl.Text = "Update Version"
        Me.Update_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Update_Version_TxtBx
        '
        Me.Update_Version_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update_Version_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Version_TxtBx.Location = New System.Drawing.Point(166, 80)
        Me.Update_Version_TxtBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Update_Version_TxtBx.Multiline = True
        Me.Update_Version_TxtBx.Name = "Update_Version_TxtBx"
        Me.Update_Version_TxtBx.ReadOnly = True
        Me.Update_Version_TxtBx.Size = New System.Drawing.Size(325, 25)
        Me.Update_Version_TxtBx.TabIndex = 8
        Me.Update_Version_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_Download_File_Path_Lbl
        '
        Me.Update_Download_File_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Update_Download_File_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Download_File_Path_Lbl.Location = New System.Drawing.Point(35, 107)
        Me.Update_Download_File_Path_Lbl.Name = "Update_Download_File_Path_Lbl"
        Me.Update_Download_File_Path_Lbl.Size = New System.Drawing.Size(129, 25)
        Me.Update_Download_File_Path_Lbl.TabIndex = 11
        Me.Update_Download_File_Path_Lbl.Text = "File Path"
        Me.Update_Download_File_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Update_Download_File_Path_TxtBx
        '
        Me.Update_Download_File_Path_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update_Download_File_Path_TxtBx.Enabled = False
        Me.Update_Download_File_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Download_File_Path_TxtBx.Location = New System.Drawing.Point(191, 107)
        Me.Update_Download_File_Path_TxtBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Update_Download_File_Path_TxtBx.Multiline = True
        Me.Update_Download_File_Path_TxtBx.Name = "Update_Download_File_Path_TxtBx"
        Me.Update_Download_File_Path_TxtBx.ReadOnly = True
        Me.Update_Download_File_Path_TxtBx.Size = New System.Drawing.Size(299, 25)
        Me.Update_Download_File_Path_TxtBx.TabIndex = 10
        Me.Update_Download_File_Path_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Update_Download_File_Path_Btn
        '
        Me.Update_Download_File_Path_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.folder
        Me.Update_Download_File_Path_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Update_Download_File_Path_Btn.Location = New System.Drawing.Point(164, 106)
        Me.Update_Download_File_Path_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Update_Download_File_Path_Btn.Name = "Update_Download_File_Path_Btn"
        Me.Update_Download_File_Path_Btn.Size = New System.Drawing.Size(27, 28)
        Me.Update_Download_File_Path_Btn.TabIndex = 12
        Me.Update_Download_File_Path_Btn.UseVisualStyleBackColor = True
        '
        'Update_New_Version_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 256)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Update_New_Version_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Update New Version"
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
End Class
