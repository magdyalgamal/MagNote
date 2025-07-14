<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Labeling_And_Tooltip_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            If Me.Visible Then
                MyBase.Dispose(disposing)
            End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Labeling_And_Tooltip_Form))
        Me.Life_Labeling_And_Tooltip_DGV = New System.Windows.Forms.DataGridView()
        Me.Form_Name_Lbl = New System.Windows.Forms.Label()
        Me.Form_Name_TxtBx = New System.Windows.Forms.TextBox()
        Me.Object_Name_TxtBx = New System.Windows.Forms.TextBox()
        Me.Object_Name_Lbl = New System.Windows.Forms.Label()
        Me.Local_Language_Label_TxtBx = New System.Windows.Forms.TextBox()
        Me.Local_Language_Label_Lbl = New System.Windows.Forms.Label()
        Me.Foreign_Language_Label_TxtBx = New System.Windows.Forms.TextBox()
        Me.Foreign_Language_Label_Lbl = New System.Windows.Forms.Label()
        Me.Foreign_Language_ToolTip_TxtBx = New System.Windows.Forms.TextBox()
        Me.Foreign_Language_ToolTip_Lbl = New System.Windows.Forms.Label()
        Me.Local_Language_ToolTip_TxtBx = New System.Windows.Forms.TextBox()
        Me.Local_Language_ToolTip_Lbl = New System.Windows.Forms.Label()
        Me.Life_Labeling_And_Tooltip_Pnl = New System.Windows.Forms.Panel()
        Me.Preview_Life_Labeling_And_Tooltip_Btn = New System.Windows.Forms.Button()
        Me.Life_Labeling_And_Tooltip_Lbl = New System.Windows.Forms.Label()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.ReLoad_XML_File_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Save_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Save_As_Stored_Copy_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Reload_Tooltips_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Form_Objects_Lbl = New System.Windows.Forms.Label()
        Me.Available_Forms_Lbl = New System.Windows.Forms.Label()
        Me.Form_Objects_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Available_Forms_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Shortcuts_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Shortcuts_Lbl = New System.Windows.Forms.Label()
        Me.Form_ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MagNote_Header_Pnl = New System.Windows.Forms.Panel()
        Me.MagNote_File_Name_Lbl = New System.Windows.Forms.Label()
        Me.Form_Controls_Pnl = New System.Windows.Forms.Panel()
        Me.Infosysme_PctrBx = New System.Windows.Forms.PictureBox()
        Me.Maximize_Form_Btn = New System.Windows.Forms.Button()
        Me.Minimize_Form_Btn = New System.Windows.Forms.Button()
        Me.Exit_Form_Btn = New System.Windows.Forms.Button()
        CType(Me.Life_Labeling_And_Tooltip_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Life_Labeling_And_Tooltip_Pnl.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.MagNote_Header_Pnl.SuspendLayout()
        Me.Form_Controls_Pnl.SuspendLayout()
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Life_Labeling_And_Tooltip_DGV
        '
        Me.Life_Labeling_And_Tooltip_DGV.AllowUserToAddRows = False
        Me.Life_Labeling_And_Tooltip_DGV.AllowUserToDeleteRows = False
        Me.Life_Labeling_And_Tooltip_DGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Life_Labeling_And_Tooltip_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Life_Labeling_And_Tooltip_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.Life_Labeling_And_Tooltip_DGV.Location = New System.Drawing.Point(0, 21)
        Me.Life_Labeling_And_Tooltip_DGV.Name = "Life_Labeling_And_Tooltip_DGV"
        Me.Life_Labeling_And_Tooltip_DGV.RowHeadersWidth = 51
        Me.Life_Labeling_And_Tooltip_DGV.Size = New System.Drawing.Size(831, 124)
        Me.Life_Labeling_And_Tooltip_DGV.TabIndex = 0
        '
        'Form_Name_Lbl
        '
        Me.Form_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Form_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Form_Name_Lbl.Location = New System.Drawing.Point(16, 120)
        Me.Form_Name_Lbl.Name = "Form_Name_Lbl"
        Me.Form_Name_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Form_Name_Lbl.TabIndex = 2
        Me.Form_Name_Lbl.Text = "Form Name"
        Me.Form_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Name_TxtBx
        '
        Me.Form_Name_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Form_Name_TxtBx.Location = New System.Drawing.Point(164, 120)
        Me.Form_Name_TxtBx.Multiline = True
        Me.Form_Name_TxtBx.Name = "Form_Name_TxtBx"
        Me.Form_Name_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Form_Name_TxtBx.Size = New System.Drawing.Size(267, 22)
        Me.Form_Name_TxtBx.TabIndex = 3
        '
        'Object_Name_TxtBx
        '
        Me.Object_Name_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Object_Name_TxtBx.Location = New System.Drawing.Point(164, 143)
        Me.Object_Name_TxtBx.Multiline = True
        Me.Object_Name_TxtBx.Name = "Object_Name_TxtBx"
        Me.Object_Name_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Object_Name_TxtBx.Size = New System.Drawing.Size(267, 22)
        Me.Object_Name_TxtBx.TabIndex = 5
        '
        'Object_Name_Lbl
        '
        Me.Object_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Object_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Object_Name_Lbl.Location = New System.Drawing.Point(16, 143)
        Me.Object_Name_Lbl.Name = "Object_Name_Lbl"
        Me.Object_Name_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Object_Name_Lbl.TabIndex = 4
        Me.Object_Name_Lbl.Text = "Object Name"
        Me.Object_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Local_Language_Label_TxtBx
        '
        Me.Local_Language_Label_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Local_Language_Label_TxtBx.Location = New System.Drawing.Point(164, 166)
        Me.Local_Language_Label_TxtBx.Multiline = True
        Me.Local_Language_Label_TxtBx.Name = "Local_Language_Label_TxtBx"
        Me.Local_Language_Label_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Local_Language_Label_TxtBx.Size = New System.Drawing.Size(267, 22)
        Me.Local_Language_Label_TxtBx.TabIndex = 7
        '
        'Local_Language_Label_Lbl
        '
        Me.Local_Language_Label_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Local_Language_Label_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Local_Language_Label_Lbl.Location = New System.Drawing.Point(16, 166)
        Me.Local_Language_Label_Lbl.Name = "Local_Language_Label_Lbl"
        Me.Local_Language_Label_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Local_Language_Label_Lbl.TabIndex = 6
        Me.Local_Language_Label_Lbl.Text = "Local Language Label"
        Me.Local_Language_Label_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Foreign_Language_Label_TxtBx
        '
        Me.Foreign_Language_Label_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Foreign_Language_Label_TxtBx.Location = New System.Drawing.Point(164, 189)
        Me.Foreign_Language_Label_TxtBx.Multiline = True
        Me.Foreign_Language_Label_TxtBx.Name = "Foreign_Language_Label_TxtBx"
        Me.Foreign_Language_Label_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Foreign_Language_Label_TxtBx.Size = New System.Drawing.Size(267, 22)
        Me.Foreign_Language_Label_TxtBx.TabIndex = 9
        '
        'Foreign_Language_Label_Lbl
        '
        Me.Foreign_Language_Label_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Foreign_Language_Label_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Foreign_Language_Label_Lbl.Location = New System.Drawing.Point(16, 189)
        Me.Foreign_Language_Label_Lbl.Name = "Foreign_Language_Label_Lbl"
        Me.Foreign_Language_Label_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Foreign_Language_Label_Lbl.TabIndex = 8
        Me.Foreign_Language_Label_Lbl.Text = "Foreign Language Label"
        Me.Foreign_Language_Label_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Foreign_Language_ToolTip_TxtBx
        '
        Me.Foreign_Language_ToolTip_TxtBx.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Foreign_Language_ToolTip_TxtBx.Location = New System.Drawing.Point(432, 232)
        Me.Foreign_Language_ToolTip_TxtBx.Multiline = True
        Me.Foreign_Language_ToolTip_TxtBx.Name = "Foreign_Language_ToolTip_TxtBx"
        Me.Foreign_Language_ToolTip_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Foreign_Language_ToolTip_TxtBx.Size = New System.Drawing.Size(415, 187)
        Me.Foreign_Language_ToolTip_TxtBx.TabIndex = 13
        '
        'Foreign_Language_ToolTip_Lbl
        '
        Me.Foreign_Language_ToolTip_Lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Foreign_Language_ToolTip_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Foreign_Language_ToolTip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Foreign_Language_ToolTip_Lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(254, Byte))
        Me.Foreign_Language_ToolTip_Lbl.Location = New System.Drawing.Point(432, 212)
        Me.Foreign_Language_ToolTip_Lbl.Name = "Foreign_Language_ToolTip_Lbl"
        Me.Foreign_Language_ToolTip_Lbl.Size = New System.Drawing.Size(415, 20)
        Me.Foreign_Language_ToolTip_Lbl.TabIndex = 12
        Me.Foreign_Language_ToolTip_Lbl.Text = "Foreign Language ToolTip"
        Me.Foreign_Language_ToolTip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Local_Language_ToolTip_TxtBx
        '
        Me.Local_Language_ToolTip_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Local_Language_ToolTip_TxtBx.Location = New System.Drawing.Point(16, 232)
        Me.Local_Language_ToolTip_TxtBx.Multiline = True
        Me.Local_Language_ToolTip_TxtBx.Name = "Local_Language_ToolTip_TxtBx"
        Me.Local_Language_ToolTip_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Local_Language_ToolTip_TxtBx.Size = New System.Drawing.Size(415, 187)
        Me.Local_Language_ToolTip_TxtBx.TabIndex = 11
        '
        'Local_Language_ToolTip_Lbl
        '
        Me.Local_Language_ToolTip_Lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Local_Language_ToolTip_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Local_Language_ToolTip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Local_Language_ToolTip_Lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(254, Byte))
        Me.Local_Language_ToolTip_Lbl.Location = New System.Drawing.Point(16, 212)
        Me.Local_Language_ToolTip_Lbl.Name = "Local_Language_ToolTip_Lbl"
        Me.Local_Language_ToolTip_Lbl.Size = New System.Drawing.Size(415, 20)
        Me.Local_Language_ToolTip_Lbl.TabIndex = 10
        Me.Local_Language_ToolTip_Lbl.Text = "Local Language ToolTip"
        Me.Local_Language_ToolTip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Life_Labeling_And_Tooltip_Pnl
        '
        Me.Life_Labeling_And_Tooltip_Pnl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Life_Labeling_And_Tooltip_Pnl.Controls.Add(Me.Life_Labeling_And_Tooltip_DGV)
        Me.Life_Labeling_And_Tooltip_Pnl.Controls.Add(Me.Preview_Life_Labeling_And_Tooltip_Btn)
        Me.Life_Labeling_And_Tooltip_Pnl.Controls.Add(Me.Life_Labeling_And_Tooltip_Lbl)
        Me.Life_Labeling_And_Tooltip_Pnl.Location = New System.Drawing.Point(16, 420)
        Me.Life_Labeling_And_Tooltip_Pnl.Name = "Life_Labeling_And_Tooltip_Pnl"
        Me.Life_Labeling_And_Tooltip_Pnl.Size = New System.Drawing.Size(831, 145)
        Me.Life_Labeling_And_Tooltip_Pnl.TabIndex = 14
        '
        'Preview_Life_Labeling_And_Tooltip_Btn
        '
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Location = New System.Drawing.Point(757, -1)
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Name = "Preview_Life_Labeling_And_Tooltip_Btn"
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Size = New System.Drawing.Size(75, 22)
        Me.Preview_Life_Labeling_And_Tooltip_Btn.TabIndex = 17
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Text = "Preview"
        Me.Preview_Life_Labeling_And_Tooltip_Btn.UseVisualStyleBackColor = True
        '
        'Life_Labeling_And_Tooltip_Lbl
        '
        Me.Life_Labeling_And_Tooltip_Lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Life_Labeling_And_Tooltip_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Life_Labeling_And_Tooltip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Life_Labeling_And_Tooltip_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Life_Labeling_And_Tooltip_Lbl.Name = "Life_Labeling_And_Tooltip_Lbl"
        Me.Life_Labeling_And_Tooltip_Lbl.Size = New System.Drawing.Size(757, 21)
        Me.Life_Labeling_And_Tooltip_Lbl.TabIndex = 16
        Me.Life_Labeling_And_Tooltip_Lbl.Text = "Life Data"
        Me.Life_Labeling_And_Tooltip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.BackColor = System.Drawing.Color.Transparent
        Me.BindingNavigator1.BindingSource = Me.BindingSource
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.BindingNavigator1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReLoad_XML_File_TlStrpBtn, Me.ToolStripSeparator1, Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.Save_TlStrpBtn, Me.BindingNavigatorDeleteItem, Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn, Me.Save_As_Stored_Copy_TlStrpBtn, Me.Reload_Tooltips_TlStrpBtn})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 79)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(863, 27)
        Me.BindingNavigator1.TabIndex = 15
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 24)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        Me.BindingNavigatorDeleteItem.ToolTipText = "Delete إلغاء"
        '
        'ReLoad_XML_File_TlStrpBtn
        '
        Me.ReLoad_XML_File_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ReLoad_XML_File_TlStrpBtn.Image = CType(resources.GetObject("ReLoad_XML_File_TlStrpBtn.Image"), System.Drawing.Image)
        Me.ReLoad_XML_File_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ReLoad_XML_File_TlStrpBtn.Name = "ReLoad_XML_File_TlStrpBtn"
        Me.ReLoad_XML_File_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.ReLoad_XML_File_TlStrpBtn.Text = "Re Load XML File"
        Me.ReLoad_XML_File_TlStrpBtn.ToolTipText = "Reload XML File إعدة تحميل الملف"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        Me.BindingNavigatorMoveFirstItem.ToolTipText = "Move To First انتقل إلى البداية"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        Me.BindingNavigatorMovePreviousItem.ToolTipText = "Move To Previous انتقل إلى السابق"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 27)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        Me.BindingNavigatorMoveNextItem.ToolTipText = "Move To Next انتقل إلى التالي"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        Me.BindingNavigatorMoveLastItem.ToolTipText = "Move To Last انتقل إلى النهاية"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'Save_TlStrpBtn
        '
        Me.Save_TlStrpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Save_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Save_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.saveHS
        Me.Save_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Save_TlStrpBtn.Name = "Save_TlStrpBtn"
        Me.Save_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.Save_TlStrpBtn.Text = "Save"
        Me.Save_TlStrpBtn.ToolTipText = "Save حفظ"
        '
        'Restore_Stored_Labels_And_ToolTips_TlStrpBtn
        '
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Image = CType(resources.GetObject("Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Name = "Restore_Stored_Labels_And_ToolTips_TlStrpBtn"
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Text = "Restore Stored Labels And ToolTips"
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.ToolTipText = "Restore Stored Labels And ToolTips From Its Copy استعادة العلامات المخزنة والتلمي" &
    "حات من النسخة الاحتياطية"
        '
        'Save_As_Stored_Copy_TlStrpBtn
        '
        Me.Save_As_Stored_Copy_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Save_As_Stored_Copy_TlStrpBtn.Image = CType(resources.GetObject("Save_As_Stored_Copy_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Save_As_Stored_Copy_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Save_As_Stored_Copy_TlStrpBtn.Name = "Save_As_Stored_Copy_TlStrpBtn"
        Me.Save_As_Stored_Copy_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.Save_As_Stored_Copy_TlStrpBtn.Text = "Save As Stored Copy"
        Me.Save_As_Stored_Copy_TlStrpBtn.ToolTipText = "Save As Stored Copy نسخ الملف الاصلى كنسخة احتياطية"
        '
        'Reload_Tooltips_TlStrpBtn
        '
        Me.Reload_Tooltips_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Reload_Tooltips_TlStrpBtn.Image = CType(resources.GetObject("Reload_Tooltips_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Reload_Tooltips_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Reload_Tooltips_TlStrpBtn.Name = "Reload_Tooltips_TlStrpBtn"
        Me.Reload_Tooltips_TlStrpBtn.Size = New System.Drawing.Size(91, 24)
        Me.Reload_Tooltips_TlStrpBtn.Text = "Reload Tooltips"
        Me.Reload_Tooltips_TlStrpBtn.ToolTipText = "Reload Tooltips إعادة تحميل التلميحات"
        '
        'Form_Objects_Lbl
        '
        Me.Form_Objects_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Form_Objects_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Form_Objects_Lbl.Location = New System.Drawing.Point(432, 143)
        Me.Form_Objects_Lbl.Name = "Form_Objects_Lbl"
        Me.Form_Objects_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Form_Objects_Lbl.TabIndex = 19
        Me.Form_Objects_Lbl.Text = "Form Objects"
        Me.Form_Objects_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Available_Forms_Lbl
        '
        Me.Available_Forms_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Available_Forms_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Available_Forms_Lbl.Location = New System.Drawing.Point(432, 120)
        Me.Available_Forms_Lbl.Name = "Available_Forms_Lbl"
        Me.Available_Forms_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Available_Forms_Lbl.TabIndex = 18
        Me.Available_Forms_Lbl.Text = "Available Forms"
        Me.Available_Forms_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Objects_CmbBx
        '
        Me.Form_Objects_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.0!)
        Me.Form_Objects_CmbBx.FormattingEnabled = True
        Me.Form_Objects_CmbBx.Location = New System.Drawing.Point(580, 143)
        Me.Form_Objects_CmbBx.Name = "Form_Objects_CmbBx"
        Me.Form_Objects_CmbBx.Size = New System.Drawing.Size(267, 22)
        Me.Form_Objects_CmbBx.Sorted = True
        Me.Form_Objects_CmbBx.TabIndex = 20
        '
        'Available_Forms_CmbBx
        '
        Me.Available_Forms_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.0!)
        Me.Available_Forms_CmbBx.FormattingEnabled = True
        Me.Available_Forms_CmbBx.Location = New System.Drawing.Point(580, 120)
        Me.Available_Forms_CmbBx.Name = "Available_Forms_CmbBx"
        Me.Available_Forms_CmbBx.Size = New System.Drawing.Size(267, 22)
        Me.Available_Forms_CmbBx.Sorted = True
        Me.Available_Forms_CmbBx.TabIndex = 21
        '
        'Shortcuts_CmbBx
        '
        Me.Shortcuts_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.0!)
        Me.Shortcuts_CmbBx.FormattingEnabled = True
        Me.Shortcuts_CmbBx.Location = New System.Drawing.Point(580, 166)
        Me.Shortcuts_CmbBx.Name = "Shortcuts_CmbBx"
        Me.Shortcuts_CmbBx.Size = New System.Drawing.Size(267, 22)
        Me.Shortcuts_CmbBx.Sorted = True
        Me.Shortcuts_CmbBx.TabIndex = 23
        '
        'Shortcuts_Lbl
        '
        Me.Shortcuts_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Shortcuts_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Shortcuts_Lbl.Location = New System.Drawing.Point(432, 166)
        Me.Shortcuts_Lbl.Name = "Shortcuts_Lbl"
        Me.Shortcuts_Lbl.Size = New System.Drawing.Size(147, 22)
        Me.Shortcuts_Lbl.TabIndex = 22
        Me.Shortcuts_Lbl.Text = "Shortcuts"
        Me.Shortcuts_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_ToolTip
        '
        Me.Form_ToolTip.BackColor = System.Drawing.Color.Yellow
        Me.Form_ToolTip.IsBalloon = True
        Me.Form_ToolTip.ShowAlways = True
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
        Me.MagNote_Header_Pnl.Size = New System.Drawing.Size(863, 79)
        Me.MagNote_Header_Pnl.TabIndex = 1191
        '
        'MagNote_File_Name_Lbl
        '
        Me.MagNote_File_Name_Lbl.AutoSize = True
        Me.MagNote_File_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.MagNote_File_Name_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_File_Name_Lbl.Location = New System.Drawing.Point(463, 32)
        Me.MagNote_File_Name_Lbl.Name = "MagNote_File_Name_Lbl"
        Me.MagNote_File_Name_Lbl.Size = New System.Drawing.Size(0, 19)
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
        Me.Form_Controls_Pnl.Size = New System.Drawing.Size(863, 79)
        Me.Form_Controls_Pnl.TabIndex = 0
        '
        'Infosysme_PctrBx
        '
        Me.Infosysme_PctrBx.BackColor = System.Drawing.Color.Transparent
        Me.Infosysme_PctrBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Infosysme_PctrBx.Image = Global.MagNote.My.Resources.Resources.MagNoteHeaderName_50
        Me.Infosysme_PctrBx.Location = New System.Drawing.Point(1, 0)
        Me.Infosysme_PctrBx.Name = "Infosysme_PctrBx"
        Me.Infosysme_PctrBx.Size = New System.Drawing.Size(366, 75)
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
        Me.Maximize_Form_Btn.Location = New System.Drawing.Point(797, 22)
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
        Me.Minimize_Form_Btn.Location = New System.Drawing.Point(772, 22)
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
        Me.Exit_Form_Btn.Location = New System.Drawing.Point(822, 22)
        Me.Exit_Form_Btn.Name = "Exit_Form_Btn"
        Me.Exit_Form_Btn.Size = New System.Drawing.Size(24, 24)
        Me.Exit_Form_Btn.TabIndex = 1184
        Me.Exit_Form_Btn.UseVisualStyleBackColor = True
        '
        'Labeling_And_Tooltip_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 579)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Controls.Add(Me.MagNote_Header_Pnl)
        Me.Controls.Add(Me.Shortcuts_CmbBx)
        Me.Controls.Add(Me.Shortcuts_Lbl)
        Me.Controls.Add(Me.Available_Forms_CmbBx)
        Me.Controls.Add(Me.Form_Objects_CmbBx)
        Me.Controls.Add(Me.Form_Objects_Lbl)
        Me.Controls.Add(Me.Available_Forms_Lbl)
        Me.Controls.Add(Me.Life_Labeling_And_Tooltip_Pnl)
        Me.Controls.Add(Me.Foreign_Language_ToolTip_TxtBx)
        Me.Controls.Add(Me.Foreign_Language_ToolTip_Lbl)
        Me.Controls.Add(Me.Local_Language_ToolTip_TxtBx)
        Me.Controls.Add(Me.Local_Language_ToolTip_Lbl)
        Me.Controls.Add(Me.Foreign_Language_Label_TxtBx)
        Me.Controls.Add(Me.Foreign_Language_Label_Lbl)
        Me.Controls.Add(Me.Local_Language_Label_TxtBx)
        Me.Controls.Add(Me.Local_Language_Label_Lbl)
        Me.Controls.Add(Me.Object_Name_TxtBx)
        Me.Controls.Add(Me.Object_Name_Lbl)
        Me.Controls.Add(Me.Form_Name_TxtBx)
        Me.Controls.Add(Me.Form_Name_Lbl)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Labeling_And_Tooltip_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Labeling And Tooltip"
        Me.TransparencyKey = System.Drawing.Color.Snow
        CType(Me.Life_Labeling_And_Tooltip_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Life_Labeling_And_Tooltip_Pnl.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.MagNote_Header_Pnl.ResumeLayout(False)
        Me.MagNote_Header_Pnl.PerformLayout()
        Me.Form_Controls_Pnl.ResumeLayout(False)
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Life_Labeling_And_Tooltip_DGV As DataGridView
    Friend WithEvents Form_Name_Lbl As Label
    Friend WithEvents Form_Name_TxtBx As TextBox
    Friend WithEvents Object_Name_TxtBx As TextBox
    Friend WithEvents Object_Name_Lbl As Label
    Friend WithEvents Local_Language_Label_TxtBx As TextBox
    Friend WithEvents Local_Language_Label_Lbl As Label
    Friend WithEvents Foreign_Language_Label_TxtBx As TextBox
    Friend WithEvents Foreign_Language_Label_Lbl As Label
    Friend WithEvents Foreign_Language_ToolTip_TxtBx As TextBox
    Friend WithEvents Foreign_Language_ToolTip_Lbl As Label
    Friend WithEvents Local_Language_ToolTip_TxtBx As TextBox
    Friend WithEvents Local_Language_ToolTip_Lbl As Label
    Friend WithEvents Life_Labeling_And_Tooltip_Pnl As Panel
    Friend WithEvents Life_Labeling_And_Tooltip_Lbl As Label
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents BindingNavigator1 As BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents Save_TlStrpBtn As ToolStripButton
    Friend WithEvents Restore_Stored_Labels_And_ToolTips_TlStrpBtn As ToolStripButton
    Friend WithEvents Preview_Life_Labeling_And_Tooltip_Btn As Button
    Friend WithEvents Form_Objects_Lbl As Label
    Friend WithEvents Available_Forms_Lbl As Label
    Friend WithEvents Form_Objects_CmbBx As ComboBox
    Friend WithEvents Available_Forms_CmbBx As ComboBox
    Friend WithEvents Shortcuts_CmbBx As ComboBox
    Friend WithEvents Shortcuts_Lbl As Label
    Friend WithEvents Reload_Tooltips_TlStrpBtn As ToolStripButton
    Friend WithEvents Save_As_Stored_Copy_TlStrpBtn As ToolStripButton
    Friend WithEvents ReLoad_XML_File_TlStrpBtn As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents MagNote_Header_Pnl As Panel
    Friend WithEvents MagNote_File_Name_Lbl As Label
    Friend WithEvents Form_Controls_Pnl As Panel
    Friend WithEvents Maximize_Form_Btn As Button
    Friend WithEvents Minimize_Form_Btn As Button
    Friend WithEvents Exit_Form_Btn As Button
    Friend WithEvents Infosysme_PctrBx As PictureBox
End Class
