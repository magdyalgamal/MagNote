<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Upload_New_Version_Form
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
    '<System.Diagnostics.DebuggerStepThrough()>
    'Private Sub InitializeComponent()

    'End Sub
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerNonUserCode()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Upload_New_Version_Form))
        Me.MagNote_Header_Pnl = New System.Windows.Forms.Panel()
        Me.MagNote_File_Name_Lbl = New System.Windows.Forms.Label()
        Me.Form_Controls_Pnl = New System.Windows.Forms.Panel()
        Me.Infosysme_PctrBx = New System.Windows.Forms.PictureBox()
        Me.Maximize_Form_Btn = New System.Windows.Forms.Button()
        Me.Minimize_Form_Btn = New System.Windows.Forms.Button()
        Me.Exit_Form_Btn = New System.Windows.Forms.Button()
        Me.Form_ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.File_Name_Lbl = New System.Windows.Forms.Label()
        Me.File_Name_TxtBx = New System.Windows.Forms.TextBox()
        Me.Additional_Files_To_Upload_DGV = New System.Windows.Forms.DataGridView()
        Me.Additional_Files_To_Upload_Lbl = New System.Windows.Forms.Label()
        Me.Source_File_Path_Lbl = New System.Windows.Forms.Label()
        Me.Source_File_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Destination_File_Path_Lbl = New System.Windows.Forms.Label()
        Me.Destination_File_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Active_File_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Active_File_Lbl = New System.Windows.Forms.Label()
        Me.Uploaded_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Uploaded_Lbl = New System.Windows.Forms.Label()
        Me.Downloaded_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Downloaded_Lbl = New System.Windows.Forms.Label()
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx = New System.Windows.Forms.CheckBox()
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl = New System.Windows.Forms.Label()
        Me.Descreption_Lbl = New System.Windows.Forms.Label()
        Me.Descreption_TxtBx = New System.Windows.Forms.TextBox()
        Me.Creation_Date_Lbl = New System.Windows.Forms.Label()
        Me.Creation_Date_TxtBx = New System.Windows.Forms.TextBox()
        Me.Update_File_Version_Lbl = New System.Windows.Forms.Label()
        Me.Update_File_Version_TxtBx = New System.Windows.Forms.TextBox()
        Me.Current_Version_Lbl = New System.Windows.Forms.Label()
        Me.Current_Version_TxtBx = New System.Windows.Forms.TextBox()
        Me.Update_Btn = New System.Windows.Forms.Button()
        Me.Delete_Btn = New System.Windows.Forms.Button()
        Me.Preview_Btn = New System.Windows.Forms.Button()
        Me.Upload_Btn = New System.Windows.Forms.Button()
        Me.Source_File_Path_Btn = New System.Windows.Forms.Button()
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Update_PathIn_Category_To_Destination_Path_Lbl = New System.Windows.Forms.Label()
        Me.MagNote_Header_Pnl.SuspendLayout()
        Me.Form_Controls_Pnl.SuspendLayout()
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Additional_Files_To_Upload_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.MagNote_Header_Pnl.Size = New System.Drawing.Size(913, 85)
        Me.MagNote_Header_Pnl.TabIndex = 1191
        '
        'MagNote_File_Name_Lbl
        '
        Me.MagNote_File_Name_Lbl.AutoSize = True
        Me.MagNote_File_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.MagNote_File_Name_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_File_Name_Lbl.Location = New System.Drawing.Point(463, 34)
        Me.MagNote_File_Name_Lbl.Name = "MagNote_File_Name_Lbl"
        Me.MagNote_File_Name_Lbl.Size = New System.Drawing.Size(0, 14)
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
        Me.Form_Controls_Pnl.Size = New System.Drawing.Size(913, 85)
        Me.Form_Controls_Pnl.TabIndex = 0
        '
        'Infosysme_PctrBx
        '
        Me.Infosysme_PctrBx.BackColor = System.Drawing.Color.Transparent
        Me.Infosysme_PctrBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Infosysme_PctrBx.Image = Global.MagNote.My.Resources.Resources.MagNoteHeaderName17
        Me.Infosysme_PctrBx.Location = New System.Drawing.Point(1, 0)
        Me.Infosysme_PctrBx.Name = "Infosysme_PctrBx"
        Me.Infosysme_PctrBx.Size = New System.Drawing.Size(366, 81)
        Me.Infosysme_PctrBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Infosysme_PctrBx.TabIndex = 1187
        Me.Infosysme_PctrBx.TabStop = False
        '
        'Maximize_Form_Btn
        '
        Me.Maximize_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Maximize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.upgrade
        Me.Maximize_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Maximize_Form_Btn.Enabled = False
        Me.Maximize_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Maximize_Form_Btn.Location = New System.Drawing.Point(846, 25)
        Me.Maximize_Form_Btn.Name = "Maximize_Form_Btn"
        Me.Maximize_Form_Btn.Size = New System.Drawing.Size(24, 26)
        Me.Maximize_Form_Btn.TabIndex = 1186
        Me.Maximize_Form_Btn.UseVisualStyleBackColor = True
        '
        'Minimize_Form_Btn
        '
        Me.Minimize_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Minimize_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.Minimize1
        Me.Minimize_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Minimize_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Minimize_Form_Btn.Location = New System.Drawing.Point(821, 25)
        Me.Minimize_Form_Btn.Name = "Minimize_Form_Btn"
        Me.Minimize_Form_Btn.Size = New System.Drawing.Size(24, 26)
        Me.Minimize_Form_Btn.TabIndex = 1185
        Me.Minimize_Form_Btn.UseVisualStyleBackColor = True
        '
        'Exit_Form_Btn
        '
        Me.Exit_Form_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exit_Form_Btn.BackgroundImage = Global.MagNote.My.Resources.Resources.PwerOff
        Me.Exit_Form_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Exit_Form_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Exit_Form_Btn.Location = New System.Drawing.Point(870, 25)
        Me.Exit_Form_Btn.Name = "Exit_Form_Btn"
        Me.Exit_Form_Btn.Size = New System.Drawing.Size(24, 26)
        Me.Exit_Form_Btn.TabIndex = 1184
        Me.Exit_Form_Btn.UseVisualStyleBackColor = True
        '
        'Form_ToolTip
        '
        Me.Form_ToolTip.BackColor = System.Drawing.Color.Yellow
        Me.Form_ToolTip.IsBalloon = True
        '
        'File_Name_Lbl
        '
        Me.File_Name_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.File_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.File_Name_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.File_Name_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.File_Name_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.File_Name_Lbl.Location = New System.Drawing.Point(12, 98)
        Me.File_Name_Lbl.Name = "File_Name_Lbl"
        Me.File_Name_Lbl.Size = New System.Drawing.Size(265, 23)
        Me.File_Name_Lbl.TabIndex = 1192
        Me.File_Name_Lbl.Text = "File Name"
        Me.File_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'File_Name_TxtBx
        '
        Me.File_Name_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.File_Name_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.File_Name_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.File_Name_TxtBx.Location = New System.Drawing.Point(278, 98)
        Me.File_Name_TxtBx.Multiline = True
        Me.File_Name_TxtBx.Name = "File_Name_TxtBx"
        Me.File_Name_TxtBx.Size = New System.Drawing.Size(255, 23)
        Me.File_Name_TxtBx.TabIndex = 1193
        '
        'Additional_Files_To_Upload_DGV
        '
        Me.Additional_Files_To_Upload_DGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Additional_Files_To_Upload_DGV.BackgroundColor = System.Drawing.SystemColors.Window
        Me.Additional_Files_To_Upload_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Additional_Files_To_Upload_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.Additional_Files_To_Upload_DGV.Location = New System.Drawing.Point(12, 342)
        Me.Additional_Files_To_Upload_DGV.MultiSelect = False
        Me.Additional_Files_To_Upload_DGV.Name = "Additional_Files_To_Upload_DGV"
        Me.Additional_Files_To_Upload_DGV.ReadOnly = True
        Me.Additional_Files_To_Upload_DGV.RowHeadersWidth = 51
        Me.Additional_Files_To_Upload_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Additional_Files_To_Upload_DGV.Size = New System.Drawing.Size(889, 132)
        Me.Additional_Files_To_Upload_DGV.TabIndex = 1194
        '
        'Additional_Files_To_Upload_Lbl
        '
        Me.Additional_Files_To_Upload_Lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Additional_Files_To_Upload_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Additional_Files_To_Upload_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Additional_Files_To_Upload_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Additional_Files_To_Upload_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Additional_Files_To_Upload_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Additional_Files_To_Upload_Lbl.Location = New System.Drawing.Point(12, 319)
        Me.Additional_Files_To_Upload_Lbl.Name = "Additional_Files_To_Upload_Lbl"
        Me.Additional_Files_To_Upload_Lbl.Size = New System.Drawing.Size(797, 23)
        Me.Additional_Files_To_Upload_Lbl.TabIndex = 1195
        Me.Additional_Files_To_Upload_Lbl.Text = "Additional Files To Upload"
        Me.Additional_Files_To_Upload_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Source_File_Path_Lbl
        '
        Me.Source_File_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Source_File_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Source_File_Path_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Source_File_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Source_File_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Source_File_Path_Lbl.Location = New System.Drawing.Point(12, 122)
        Me.Source_File_Path_Lbl.Name = "Source_File_Path_Lbl"
        Me.Source_File_Path_Lbl.Size = New System.Drawing.Size(265, 23)
        Me.Source_File_Path_Lbl.TabIndex = 1196
        Me.Source_File_Path_Lbl.Text = "Source File Path"
        Me.Source_File_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Source_File_Path_TxtBx
        '
        Me.Source_File_Path_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Source_File_Path_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Source_File_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Source_File_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Source_File_Path_TxtBx.Location = New System.Drawing.Point(278, 122)
        Me.Source_File_Path_TxtBx.Multiline = True
        Me.Source_File_Path_TxtBx.Name = "Source_File_Path_TxtBx"
        Me.Source_File_Path_TxtBx.Size = New System.Drawing.Size(600, 23)
        Me.Source_File_Path_TxtBx.TabIndex = 1197
        '
        'Destination_File_Path_Lbl
        '
        Me.Destination_File_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Destination_File_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Destination_File_Path_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Destination_File_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Destination_File_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Destination_File_Path_Lbl.Location = New System.Drawing.Point(12, 146)
        Me.Destination_File_Path_Lbl.Name = "Destination_File_Path_Lbl"
        Me.Destination_File_Path_Lbl.Size = New System.Drawing.Size(265, 23)
        Me.Destination_File_Path_Lbl.TabIndex = 1198
        Me.Destination_File_Path_Lbl.Text = "Destination File Path"
        Me.Destination_File_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Destination_File_Path_TxtBx
        '
        Me.Destination_File_Path_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Destination_File_Path_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Destination_File_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Destination_File_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Destination_File_Path_TxtBx.Location = New System.Drawing.Point(278, 146)
        Me.Destination_File_Path_TxtBx.Multiline = True
        Me.Destination_File_Path_TxtBx.Name = "Destination_File_Path_TxtBx"
        Me.Destination_File_Path_TxtBx.Size = New System.Drawing.Size(623, 23)
        Me.Destination_File_Path_TxtBx.TabIndex = 1199
        '
        'Active_File_ChkBx
        '
        Me.Active_File_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Active_File_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Active_File_ChkBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Active_File_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Active_File_ChkBx.Location = New System.Drawing.Point(14, 171)
        Me.Active_File_ChkBx.Name = "Active_File_ChkBx"
        Me.Active_File_ChkBx.Size = New System.Drawing.Size(283, 22)
        Me.Active_File_ChkBx.TabIndex = 1201
        Me.Active_File_ChkBx.Text = "Active File"
        Me.Active_File_ChkBx.UseVisualStyleBackColor = False
        '
        'Active_File_Lbl
        '
        Me.Active_File_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Active_File_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Active_File_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Active_File_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Active_File_Lbl.Location = New System.Drawing.Point(12, 170)
        Me.Active_File_Lbl.Name = "Active_File_Lbl"
        Me.Active_File_Lbl.Size = New System.Drawing.Size(290, 24)
        Me.Active_File_Lbl.TabIndex = 1200
        Me.Active_File_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Uploaded_ChkBx
        '
        Me.Uploaded_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Uploaded_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Uploaded_ChkBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Uploaded_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Uploaded_ChkBx.Location = New System.Drawing.Point(14, 196)
        Me.Uploaded_ChkBx.Name = "Uploaded_ChkBx"
        Me.Uploaded_ChkBx.Size = New System.Drawing.Size(283, 22)
        Me.Uploaded_ChkBx.TabIndex = 1203
        Me.Uploaded_ChkBx.Text = "Uploaded"
        Me.Uploaded_ChkBx.UseVisualStyleBackColor = False
        '
        'Uploaded_Lbl
        '
        Me.Uploaded_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Uploaded_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Uploaded_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Uploaded_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Uploaded_Lbl.Location = New System.Drawing.Point(12, 195)
        Me.Uploaded_Lbl.Name = "Uploaded_Lbl"
        Me.Uploaded_Lbl.Size = New System.Drawing.Size(290, 24)
        Me.Uploaded_Lbl.TabIndex = 1202
        Me.Uploaded_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Downloaded_ChkBx
        '
        Me.Downloaded_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Downloaded_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Downloaded_ChkBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Downloaded_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Downloaded_ChkBx.Location = New System.Drawing.Point(14, 221)
        Me.Downloaded_ChkBx.Name = "Downloaded_ChkBx"
        Me.Downloaded_ChkBx.Size = New System.Drawing.Size(283, 22)
        Me.Downloaded_ChkBx.TabIndex = 1205
        Me.Downloaded_ChkBx.Text = "Downloaded"
        Me.Downloaded_ChkBx.UseVisualStyleBackColor = False
        '
        'Downloaded_Lbl
        '
        Me.Downloaded_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Downloaded_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Downloaded_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Downloaded_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Downloaded_Lbl.Location = New System.Drawing.Point(12, 220)
        Me.Downloaded_Lbl.Name = "Downloaded_Lbl"
        Me.Downloaded_Lbl.Size = New System.Drawing.Size(290, 24)
        Me.Downloaded_Lbl.TabIndex = 1204
        Me.Downloaded_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'If_Exist_Ask_To_Replace_Else_Delete_ChkBx
        '
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.Location = New System.Drawing.Point(14, 246)
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.Name = "If_Exist_Ask_To_Replace_Else_Delete_ChkBx"
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.Size = New System.Drawing.Size(283, 22)
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.TabIndex = 1207
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.Text = "If Exist Ask To Relace Else Delete"
        Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx.UseVisualStyleBackColor = False
        '
        'If_Exist_Ask_To_Replace_Else_Delete_Lbl
        '
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.Location = New System.Drawing.Point(12, 245)
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.Name = "If_Exist_Ask_To_Replace_Else_Delete_Lbl"
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.Size = New System.Drawing.Size(290, 24)
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.TabIndex = 1206
        Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Descreption_Lbl
        '
        Me.Descreption_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Descreption_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Descreption_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Descreption_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Descreption_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Descreption_Lbl.Location = New System.Drawing.Point(12, 295)
        Me.Descreption_Lbl.Name = "Descreption_Lbl"
        Me.Descreption_Lbl.Size = New System.Drawing.Size(265, 23)
        Me.Descreption_Lbl.TabIndex = 1208
        Me.Descreption_Lbl.Text = "Descreption"
        Me.Descreption_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Descreption_TxtBx
        '
        Me.Descreption_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Descreption_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Descreption_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Descreption_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Descreption_TxtBx.Location = New System.Drawing.Point(278, 295)
        Me.Descreption_TxtBx.Multiline = True
        Me.Descreption_TxtBx.Name = "Descreption_TxtBx"
        Me.Descreption_TxtBx.Size = New System.Drawing.Size(623, 23)
        Me.Descreption_TxtBx.TabIndex = 1209
        '
        'Creation_Date_Lbl
        '
        Me.Creation_Date_Lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Creation_Date_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Creation_Date_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Creation_Date_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Creation_Date_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Creation_Date_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Creation_Date_Lbl.Location = New System.Drawing.Point(574, 231)
        Me.Creation_Date_Lbl.Name = "Creation_Date_Lbl"
        Me.Creation_Date_Lbl.Size = New System.Drawing.Size(131, 23)
        Me.Creation_Date_Lbl.TabIndex = 1210
        Me.Creation_Date_Lbl.Text = "Creation Date"
        Me.Creation_Date_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Creation_Date_TxtBx
        '
        Me.Creation_Date_TxtBx.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Creation_Date_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Creation_Date_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Creation_Date_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Creation_Date_TxtBx.Location = New System.Drawing.Point(706, 231)
        Me.Creation_Date_TxtBx.Multiline = True
        Me.Creation_Date_TxtBx.Name = "Creation_Date_TxtBx"
        Me.Creation_Date_TxtBx.Size = New System.Drawing.Size(195, 23)
        Me.Creation_Date_TxtBx.TabIndex = 1211
        '
        'Update_File_Version_Lbl
        '
        Me.Update_File_Version_Lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update_File_Version_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Update_File_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Update_File_Version_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Update_File_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Update_File_Version_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Update_File_Version_Lbl.Location = New System.Drawing.Point(574, 183)
        Me.Update_File_Version_Lbl.Name = "Update_File_Version_Lbl"
        Me.Update_File_Version_Lbl.Size = New System.Drawing.Size(131, 23)
        Me.Update_File_Version_Lbl.TabIndex = 1212
        Me.Update_File_Version_Lbl.Text = "Update File Version"
        Me.Update_File_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Update_File_Version_TxtBx
        '
        Me.Update_File_Version_TxtBx.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update_File_Version_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Update_File_Version_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Update_File_Version_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Update_File_Version_TxtBx.Location = New System.Drawing.Point(706, 183)
        Me.Update_File_Version_TxtBx.Multiline = True
        Me.Update_File_Version_TxtBx.Name = "Update_File_Version_TxtBx"
        Me.Update_File_Version_TxtBx.Size = New System.Drawing.Size(195, 23)
        Me.Update_File_Version_TxtBx.TabIndex = 1213
        '
        'Current_Version_Lbl
        '
        Me.Current_Version_Lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Current_Version_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Current_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Current_Version_Lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.Current_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Current_Version_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Current_Version_Lbl.Location = New System.Drawing.Point(574, 207)
        Me.Current_Version_Lbl.Name = "Current_Version_Lbl"
        Me.Current_Version_Lbl.Size = New System.Drawing.Size(131, 23)
        Me.Current_Version_Lbl.TabIndex = 1214
        Me.Current_Version_Lbl.Text = "Current Version"
        Me.Current_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Current_Version_TxtBx
        '
        Me.Current_Version_TxtBx.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Current_Version_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Current_Version_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Current_Version_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Current_Version_TxtBx.Location = New System.Drawing.Point(706, 207)
        Me.Current_Version_TxtBx.Multiline = True
        Me.Current_Version_TxtBx.Name = "Current_Version_TxtBx"
        Me.Current_Version_TxtBx.Size = New System.Drawing.Size(195, 23)
        Me.Current_Version_TxtBx.TabIndex = 1215
        '
        'Update_Btn
        '
        Me.Update_Btn.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_Btn.Location = New System.Drawing.Point(339, 484)
        Me.Update_Btn.Name = "Update_Btn"
        Me.Update_Btn.Size = New System.Drawing.Size(78, 36)
        Me.Update_Btn.TabIndex = 1216
        Me.Update_Btn.Text = "Update"
        Me.Update_Btn.UseVisualStyleBackColor = True
        '
        'Delete_Btn
        '
        Me.Delete_Btn.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delete_Btn.Location = New System.Drawing.Point(417, 484)
        Me.Delete_Btn.Name = "Delete_Btn"
        Me.Delete_Btn.Size = New System.Drawing.Size(78, 36)
        Me.Delete_Btn.TabIndex = 1217
        Me.Delete_Btn.Text = "Delete"
        Me.Delete_Btn.UseVisualStyleBackColor = True
        '
        'Preview_Btn
        '
        Me.Preview_Btn.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Preview_Btn.Location = New System.Drawing.Point(809, 318)
        Me.Preview_Btn.Name = "Preview_Btn"
        Me.Preview_Btn.Size = New System.Drawing.Size(93, 24)
        Me.Preview_Btn.TabIndex = 1218
        Me.Preview_Btn.Text = "Preview"
        Me.Preview_Btn.UseVisualStyleBackColor = True
        '
        'Upload_Btn
        '
        Me.Upload_Btn.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Upload_Btn.Location = New System.Drawing.Point(495, 484)
        Me.Upload_Btn.Name = "Upload_Btn"
        Me.Upload_Btn.Size = New System.Drawing.Size(78, 36)
        Me.Upload_Btn.TabIndex = 1219
        Me.Upload_Btn.Text = "Upload"
        Me.Upload_Btn.UseVisualStyleBackColor = True
        '
        'Source_File_Path_Btn
        '
        Me.Source_File_Path_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Source_File_Path_Btn.BackgroundImage = CType(resources.GetObject("Source_File_Path_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Source_File_Path_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Source_File_Path_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Source_File_Path_Btn.Enabled = False
        Me.Source_File_Path_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Source_File_Path_Btn.Location = New System.Drawing.Point(879, 123)
        Me.Source_File_Path_Btn.Name = "Source_File_Path_Btn"
        Me.Source_File_Path_Btn.Size = New System.Drawing.Size(22, 22)
        Me.Source_File_Path_Btn.TabIndex = 1220
        Me.Source_File_Path_Btn.UseVisualStyleBackColor = True
        '
        'Update_PathIn_Category_To_Destination_Path_ChkBx
        '
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.Location = New System.Drawing.Point(14, 271)
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.Name = "Update_PathIn_Category_To_Destination_Path_ChkBx"
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.Size = New System.Drawing.Size(283, 22)
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.TabIndex = 1222
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.Text = "Update Path In Category To Destination Path"
        Me.Update_PathIn_Category_To_Destination_Path_ChkBx.UseVisualStyleBackColor = False
        '
        'Update_PathIn_Category_To_Destination_Path_Lbl
        '
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.Location = New System.Drawing.Point(12, 270)
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.Name = "Update_PathIn_Category_To_Destination_Path_Lbl"
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.Size = New System.Drawing.Size(290, 24)
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.TabIndex = 1221
        Me.Update_PathIn_Category_To_Destination_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Upload_New_Version_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 529)
        Me.Controls.Add(Me.Update_PathIn_Category_To_Destination_Path_ChkBx)
        Me.Controls.Add(Me.Update_PathIn_Category_To_Destination_Path_Lbl)
        Me.Controls.Add(Me.Source_File_Path_Btn)
        Me.Controls.Add(Me.Upload_Btn)
        Me.Controls.Add(Me.Preview_Btn)
        Me.Controls.Add(Me.Delete_Btn)
        Me.Controls.Add(Me.Update_Btn)
        Me.Controls.Add(Me.Current_Version_Lbl)
        Me.Controls.Add(Me.Current_Version_TxtBx)
        Me.Controls.Add(Me.Update_File_Version_Lbl)
        Me.Controls.Add(Me.Update_File_Version_TxtBx)
        Me.Controls.Add(Me.Creation_Date_Lbl)
        Me.Controls.Add(Me.Creation_Date_TxtBx)
        Me.Controls.Add(Me.Descreption_Lbl)
        Me.Controls.Add(Me.Descreption_TxtBx)
        Me.Controls.Add(Me.If_Exist_Ask_To_Replace_Else_Delete_ChkBx)
        Me.Controls.Add(Me.If_Exist_Ask_To_Replace_Else_Delete_Lbl)
        Me.Controls.Add(Me.Downloaded_ChkBx)
        Me.Controls.Add(Me.Downloaded_Lbl)
        Me.Controls.Add(Me.Uploaded_ChkBx)
        Me.Controls.Add(Me.Uploaded_Lbl)
        Me.Controls.Add(Me.Active_File_ChkBx)
        Me.Controls.Add(Me.Active_File_Lbl)
        Me.Controls.Add(Me.Destination_File_Path_Lbl)
        Me.Controls.Add(Me.Destination_File_Path_TxtBx)
        Me.Controls.Add(Me.Source_File_Path_Lbl)
        Me.Controls.Add(Me.Source_File_Path_TxtBx)
        Me.Controls.Add(Me.Additional_Files_To_Upload_Lbl)
        Me.Controls.Add(Me.Additional_Files_To_Upload_DGV)
        Me.Controls.Add(Me.File_Name_Lbl)
        Me.Controls.Add(Me.File_Name_TxtBx)
        Me.Controls.Add(Me.MagNote_Header_Pnl)
        Me.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Upload_New_Version_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upload New Version"
        Me.TransparencyKey = System.Drawing.Color.Snow
        Me.MagNote_Header_Pnl.ResumeLayout(False)
        Me.MagNote_Header_Pnl.PerformLayout()
        Me.Form_Controls_Pnl.ResumeLayout(False)
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Additional_Files_To_Upload_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Private Shared m_SyncObject As New Object
    Friend WithEvents MagNote_Header_Pnl As Panel
    Friend WithEvents MagNote_File_Name_Lbl As Label
    Friend WithEvents Form_Controls_Pnl As Panel
    Friend WithEvents Infosysme_PctrBx As PictureBox
    Friend WithEvents Maximize_Form_Btn As Button
    Friend WithEvents Minimize_Form_Btn As Button
    Friend WithEvents Exit_Form_Btn As Button
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents File_Name_Lbl As Label
    Friend WithEvents File_Name_TxtBx As TextBox
    Friend WithEvents Additional_Files_To_Upload_DGV As DataGridView
    Friend WithEvents Additional_Files_To_Upload_Lbl As Label
    Friend WithEvents Source_File_Path_Lbl As Label
    Friend WithEvents Source_File_Path_TxtBx As TextBox
    Friend WithEvents Destination_File_Path_Lbl As Label
    Friend WithEvents Destination_File_Path_TxtBx As TextBox
    Friend WithEvents Active_File_ChkBx As CheckBox
    Friend WithEvents Active_File_Lbl As Label
    Friend WithEvents Uploaded_ChkBx As CheckBox
    Friend WithEvents Uploaded_Lbl As Label
    Friend WithEvents Downloaded_ChkBx As CheckBox
    Friend WithEvents Downloaded_Lbl As Label
    Friend WithEvents If_Exist_Ask_To_Replace_Else_Delete_ChkBx As CheckBox
    Friend WithEvents If_Exist_Ask_To_Replace_Else_Delete_Lbl As Label
    Friend WithEvents Descreption_Lbl As Label
    Friend WithEvents Descreption_TxtBx As TextBox
    Friend WithEvents Creation_Date_Lbl As Label
    Friend WithEvents Creation_Date_TxtBx As TextBox
    Friend WithEvents Update_File_Version_Lbl As Label
    Friend WithEvents Update_File_Version_TxtBx As TextBox
    Friend WithEvents Current_Version_Lbl As Label
    Friend WithEvents Current_Version_TxtBx As TextBox
    Friend WithEvents Update_Btn As Button
    Friend WithEvents Delete_Btn As Button
    Friend WithEvents Preview_Btn As Button
    Friend WithEvents Upload_Btn As Button
    Friend WithEvents Source_File_Path_Btn As Button
    Friend WithEvents Update_PathIn_Category_To_Destination_Path_ChkBx As CheckBox
    Friend WithEvents Update_PathIn_Category_To_Destination_Path_Lbl As Label
#End Region
End Class
