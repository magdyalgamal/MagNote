<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Labeling_And_Tooltip_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Labeling_And_Tooltip_Form))
        Me.Life_Labeling_And_Tooltip_DGV = New System.Windows.Forms.DataGridView()
        Me.Stored_Labeling_And_Tooltip_DGV = New System.Windows.Forms.DataGridView()
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Stored_Labeling_And_Tooltip_Lbl = New System.Windows.Forms.Label()
        Me.Preview_Stored_Labeling_And_Tooltip_Btn = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Preview_Life_Labeling_And_Tooltip_Btn = New System.Windows.Forms.Button()
        Me.Life_Labeling_And_Tooltip_Lbl = New System.Windows.Forms.Label()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
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
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Form_Objects_Lbl = New System.Windows.Forms.Label()
        Me.Available_Forms_Lbl = New System.Windows.Forms.Label()
        Me.Form_Objects_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Available_Forms_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Shortcuts_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Shortcuts_Lbl = New System.Windows.Forms.Label()
        Me.Use_Stored_Data_Tab_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Open_Note_In_New_Tab_Lbl = New System.Windows.Forms.Label()
        CType(Me.Life_Labeling_And_Tooltip_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Stored_Labeling_And_Tooltip_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
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
        Me.Life_Labeling_And_Tooltip_DGV.Size = New System.Drawing.Size(378, 247)
        Me.Life_Labeling_And_Tooltip_DGV.TabIndex = 0
        '
        'Stored_Labeling_And_Tooltip_DGV
        '
        Me.Stored_Labeling_And_Tooltip_DGV.AllowUserToAddRows = False
        Me.Stored_Labeling_And_Tooltip_DGV.AllowUserToDeleteRows = False
        Me.Stored_Labeling_And_Tooltip_DGV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Stored_Labeling_And_Tooltip_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Stored_Labeling_And_Tooltip_DGV.Enabled = False
        Me.Stored_Labeling_And_Tooltip_DGV.Location = New System.Drawing.Point(0, 21)
        Me.Stored_Labeling_And_Tooltip_DGV.Name = "Stored_Labeling_And_Tooltip_DGV"
        Me.Stored_Labeling_And_Tooltip_DGV.ReadOnly = True
        Me.Stored_Labeling_And_Tooltip_DGV.RowHeadersWidth = 51
        Me.Stored_Labeling_And_Tooltip_DGV.Size = New System.Drawing.Size(450, 247)
        Me.Stored_Labeling_And_Tooltip_DGV.TabIndex = 1
        '
        'Form_Name_Lbl
        '
        Me.Form_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Form_Name_Lbl.Location = New System.Drawing.Point(17, 38)
        Me.Form_Name_Lbl.Name = "Form_Name_Lbl"
        Me.Form_Name_Lbl.Size = New System.Drawing.Size(147, 20)
        Me.Form_Name_Lbl.TabIndex = 2
        Me.Form_Name_Lbl.Text = "Form Name"
        Me.Form_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Name_TxtBx
        '
        Me.Form_Name_TxtBx.Location = New System.Drawing.Point(165, 38)
        Me.Form_Name_TxtBx.Name = "Form_Name_TxtBx"
        Me.Form_Name_TxtBx.Size = New System.Drawing.Size(331, 20)
        Me.Form_Name_TxtBx.TabIndex = 3
        '
        'Object_Name_TxtBx
        '
        Me.Object_Name_TxtBx.Location = New System.Drawing.Point(165, 59)
        Me.Object_Name_TxtBx.Name = "Object_Name_TxtBx"
        Me.Object_Name_TxtBx.Size = New System.Drawing.Size(331, 20)
        Me.Object_Name_TxtBx.TabIndex = 5
        '
        'Object_Name_Lbl
        '
        Me.Object_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Object_Name_Lbl.Location = New System.Drawing.Point(17, 59)
        Me.Object_Name_Lbl.Name = "Object_Name_Lbl"
        Me.Object_Name_Lbl.Size = New System.Drawing.Size(147, 20)
        Me.Object_Name_Lbl.TabIndex = 4
        Me.Object_Name_Lbl.Text = "Object Name"
        Me.Object_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Local_Language_Label_TxtBx
        '
        Me.Local_Language_Label_TxtBx.Location = New System.Drawing.Point(165, 80)
        Me.Local_Language_Label_TxtBx.Name = "Local_Language_Label_TxtBx"
        Me.Local_Language_Label_TxtBx.Size = New System.Drawing.Size(331, 20)
        Me.Local_Language_Label_TxtBx.TabIndex = 7
        '
        'Local_Language_Label_Lbl
        '
        Me.Local_Language_Label_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Local_Language_Label_Lbl.Location = New System.Drawing.Point(17, 80)
        Me.Local_Language_Label_Lbl.Name = "Local_Language_Label_Lbl"
        Me.Local_Language_Label_Lbl.Size = New System.Drawing.Size(147, 20)
        Me.Local_Language_Label_Lbl.TabIndex = 6
        Me.Local_Language_Label_Lbl.Text = "Local Language Label"
        Me.Local_Language_Label_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Foreign_Language_Label_TxtBx
        '
        Me.Foreign_Language_Label_TxtBx.Location = New System.Drawing.Point(165, 101)
        Me.Foreign_Language_Label_TxtBx.Name = "Foreign_Language_Label_TxtBx"
        Me.Foreign_Language_Label_TxtBx.Size = New System.Drawing.Size(331, 20)
        Me.Foreign_Language_Label_TxtBx.TabIndex = 9
        '
        'Foreign_Language_Label_Lbl
        '
        Me.Foreign_Language_Label_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Foreign_Language_Label_Lbl.Location = New System.Drawing.Point(17, 101)
        Me.Foreign_Language_Label_Lbl.Name = "Foreign_Language_Label_Lbl"
        Me.Foreign_Language_Label_Lbl.Size = New System.Drawing.Size(147, 20)
        Me.Foreign_Language_Label_Lbl.TabIndex = 8
        Me.Foreign_Language_Label_Lbl.Text = "Foreign Language Label"
        Me.Foreign_Language_Label_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Foreign_Language_ToolTip_TxtBx
        '
        Me.Foreign_Language_ToolTip_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Foreign_Language_ToolTip_TxtBx.Location = New System.Drawing.Point(165, 143)
        Me.Foreign_Language_ToolTip_TxtBx.Multiline = True
        Me.Foreign_Language_ToolTip_TxtBx.Name = "Foreign_Language_ToolTip_TxtBx"
        Me.Foreign_Language_ToolTip_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Foreign_Language_ToolTip_TxtBx.Size = New System.Drawing.Size(681, 20)
        Me.Foreign_Language_ToolTip_TxtBx.TabIndex = 13
        '
        'Foreign_Language_ToolTip_Lbl
        '
        Me.Foreign_Language_ToolTip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Foreign_Language_ToolTip_Lbl.Location = New System.Drawing.Point(17, 143)
        Me.Foreign_Language_ToolTip_Lbl.Name = "Foreign_Language_ToolTip_Lbl"
        Me.Foreign_Language_ToolTip_Lbl.Size = New System.Drawing.Size(147, 20)
        Me.Foreign_Language_ToolTip_Lbl.TabIndex = 12
        Me.Foreign_Language_ToolTip_Lbl.Text = "Foreign Language ToolTip"
        Me.Foreign_Language_ToolTip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Local_Language_ToolTip_TxtBx
        '
        Me.Local_Language_ToolTip_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Local_Language_ToolTip_TxtBx.Location = New System.Drawing.Point(165, 122)
        Me.Local_Language_ToolTip_TxtBx.Multiline = True
        Me.Local_Language_ToolTip_TxtBx.Name = "Local_Language_ToolTip_TxtBx"
        Me.Local_Language_ToolTip_TxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Local_Language_ToolTip_TxtBx.Size = New System.Drawing.Size(681, 20)
        Me.Local_Language_ToolTip_TxtBx.TabIndex = 11
        '
        'Local_Language_ToolTip_Lbl
        '
        Me.Local_Language_ToolTip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Local_Language_ToolTip_Lbl.Location = New System.Drawing.Point(17, 122)
        Me.Local_Language_ToolTip_Lbl.Name = "Local_Language_ToolTip_Lbl"
        Me.Local_Language_ToolTip_Lbl.Size = New System.Drawing.Size(147, 20)
        Me.Local_Language_ToolTip_Lbl.TabIndex = 10
        Me.Local_Language_ToolTip_Lbl.Text = "Local Language ToolTip"
        Me.Local_Language_ToolTip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(17, 164)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 268)
        Me.Panel1.TabIndex = 14
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Stored_Labeling_And_Tooltip_Lbl)
        Me.Panel3.Controls.Add(Me.Preview_Stored_Labeling_And_Tooltip_Btn)
        Me.Panel3.Controls.Add(Me.Stored_Labeling_And_Tooltip_DGV)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(379, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(450, 268)
        Me.Panel3.TabIndex = 21
        '
        'Stored_Labeling_And_Tooltip_Lbl
        '
        Me.Stored_Labeling_And_Tooltip_Lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Stored_Labeling_And_Tooltip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Stored_Labeling_And_Tooltip_Lbl.Enabled = False
        Me.Stored_Labeling_And_Tooltip_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Stored_Labeling_And_Tooltip_Lbl.Name = "Stored_Labeling_And_Tooltip_Lbl"
        Me.Stored_Labeling_And_Tooltip_Lbl.Size = New System.Drawing.Size(376, 21)
        Me.Stored_Labeling_And_Tooltip_Lbl.TabIndex = 19
        Me.Stored_Labeling_And_Tooltip_Lbl.Text = "Stored Data"
        Me.Stored_Labeling_And_Tooltip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Preview_Stored_Labeling_And_Tooltip_Btn
        '
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.Enabled = False
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.Location = New System.Drawing.Point(376, -1)
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.Name = "Preview_Stored_Labeling_And_Tooltip_Btn"
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.Size = New System.Drawing.Size(75, 22)
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.TabIndex = 18
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.Text = "Preview"
        Me.Preview_Stored_Labeling_And_Tooltip_Btn.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Preview_Life_Labeling_And_Tooltip_Btn)
        Me.Panel2.Controls.Add(Me.Life_Labeling_And_Tooltip_DGV)
        Me.Panel2.Controls.Add(Me.Life_Labeling_And_Tooltip_Lbl)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(378, 269)
        Me.Panel2.TabIndex = 20
        '
        'Preview_Life_Labeling_And_Tooltip_Btn
        '
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Preview_Life_Labeling_And_Tooltip_Btn.Location = New System.Drawing.Point(304, -1)
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
        Me.Life_Labeling_And_Tooltip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Life_Labeling_And_Tooltip_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Life_Labeling_And_Tooltip_Lbl.Name = "Life_Labeling_And_Tooltip_Lbl"
        Me.Life_Labeling_And_Tooltip_Lbl.Size = New System.Drawing.Size(304, 21)
        Me.Life_Labeling_And_Tooltip_Lbl.TabIndex = 16
        Me.Life_Labeling_And_Tooltip_Lbl.Text = "Life Data"
        Me.Life_Labeling_And_Tooltip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1.BindingSource = Me.BindingSource
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.BindingNavigator1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.Save_TlStrpBtn, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn, Me.Reload_Shortcuts_Tooltip_TlStrpBtn})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
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
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
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
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
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
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
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
        '
        'Restore_Stored_Labels_And_ToolTips_TlStrpBtn
        '
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.RepeatHS
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Name = "Restore_Stored_Labels_And_ToolTips_TlStrpBtn"
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.Restore_Stored_Labels_And_ToolTips_TlStrpBtn.Text = "Restore Stored Labels And ToolTips"
        '
        'Reload_Shortcuts_Tooltip_TlStrpBtn
        '
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn.Image = CType(resources.GetObject("Reload_Shortcuts_Tooltip_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn.Name = "Reload_Shortcuts_Tooltip_TlStrpBtn"
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn.Size = New System.Drawing.Size(144, 24)
        Me.Reload_Shortcuts_Tooltip_TlStrpBtn.Text = "Reload Shortcuts Tooltips"
        '
        'Form_Objects_Lbl
        '
        Me.Form_Objects_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Form_Objects_Lbl.Location = New System.Drawing.Point(497, 60)
        Me.Form_Objects_Lbl.Name = "Form_Objects_Lbl"
        Me.Form_Objects_Lbl.Size = New System.Drawing.Size(147, 21)
        Me.Form_Objects_Lbl.TabIndex = 19
        Me.Form_Objects_Lbl.Text = "Form Objects"
        Me.Form_Objects_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Available_Forms_Lbl
        '
        Me.Available_Forms_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Available_Forms_Lbl.Location = New System.Drawing.Point(497, 38)
        Me.Available_Forms_Lbl.Name = "Available_Forms_Lbl"
        Me.Available_Forms_Lbl.Size = New System.Drawing.Size(147, 21)
        Me.Available_Forms_Lbl.TabIndex = 18
        Me.Available_Forms_Lbl.Text = "Available Forms"
        Me.Available_Forms_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Objects_CmbBx
        '
        Me.Form_Objects_CmbBx.FormattingEnabled = True
        Me.Form_Objects_CmbBx.Location = New System.Drawing.Point(645, 60)
        Me.Form_Objects_CmbBx.Name = "Form_Objects_CmbBx"
        Me.Form_Objects_CmbBx.Size = New System.Drawing.Size(201, 21)
        Me.Form_Objects_CmbBx.Sorted = True
        Me.Form_Objects_CmbBx.TabIndex = 20
        '
        'Available_Forms_CmbBx
        '
        Me.Available_Forms_CmbBx.FormattingEnabled = True
        Me.Available_Forms_CmbBx.Location = New System.Drawing.Point(645, 38)
        Me.Available_Forms_CmbBx.Name = "Available_Forms_CmbBx"
        Me.Available_Forms_CmbBx.Size = New System.Drawing.Size(201, 21)
        Me.Available_Forms_CmbBx.Sorted = True
        Me.Available_Forms_CmbBx.TabIndex = 21
        '
        'Shortcuts_CmbBx
        '
        Me.Shortcuts_CmbBx.FormattingEnabled = True
        Me.Shortcuts_CmbBx.Location = New System.Drawing.Point(645, 82)
        Me.Shortcuts_CmbBx.Name = "Shortcuts_CmbBx"
        Me.Shortcuts_CmbBx.Size = New System.Drawing.Size(201, 21)
        Me.Shortcuts_CmbBx.Sorted = True
        Me.Shortcuts_CmbBx.TabIndex = 23
        '
        'Shortcuts_Lbl
        '
        Me.Shortcuts_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Shortcuts_Lbl.Location = New System.Drawing.Point(497, 82)
        Me.Shortcuts_Lbl.Name = "Shortcuts_Lbl"
        Me.Shortcuts_Lbl.Size = New System.Drawing.Size(147, 21)
        Me.Shortcuts_Lbl.TabIndex = 22
        Me.Shortcuts_Lbl.Text = "Shortcuts"
        Me.Shortcuts_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Use_Stored_Data_Tab_ChkBx
        '
        Me.Use_Stored_Data_Tab_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Use_Stored_Data_Tab_ChkBx.CheckAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Use_Stored_Data_Tab_ChkBx.Location = New System.Drawing.Point(499, 105)
        Me.Use_Stored_Data_Tab_ChkBx.Name = "Use_Stored_Data_Tab_ChkBx"
        Me.Use_Stored_Data_Tab_ChkBx.Size = New System.Drawing.Size(160, 15)
        Me.Use_Stored_Data_Tab_ChkBx.TabIndex = 950
        Me.Use_Stored_Data_Tab_ChkBx.Text = "Use Stored Data"
        Me.Use_Stored_Data_Tab_ChkBx.UseVisualStyleBackColor = False
        '
        'Open_Note_In_New_Tab_Lbl
        '
        Me.Open_Note_In_New_Tab_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Open_Note_In_New_Tab_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Open_Note_In_New_Tab_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Open_Note_In_New_Tab_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Open_Note_In_New_Tab_Lbl.Location = New System.Drawing.Point(497, 104)
        Me.Open_Note_In_New_Tab_Lbl.Name = "Open_Note_In_New_Tab_Lbl"
        Me.Open_Note_In_New_Tab_Lbl.Size = New System.Drawing.Size(164, 17)
        Me.Open_Note_In_New_Tab_Lbl.TabIndex = 949
        Me.Open_Note_In_New_Tab_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Labeling_And_Tooltip_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 447)
        Me.Controls.Add(Me.Use_Stored_Data_Tab_ChkBx)
        Me.Controls.Add(Me.Open_Note_In_New_Tab_Lbl)
        Me.Controls.Add(Me.Shortcuts_CmbBx)
        Me.Controls.Add(Me.Shortcuts_Lbl)
        Me.Controls.Add(Me.Available_Forms_CmbBx)
        Me.Controls.Add(Me.Form_Objects_CmbBx)
        Me.Controls.Add(Me.Form_Objects_Lbl)
        Me.Controls.Add(Me.Available_Forms_Lbl)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Controls.Add(Me.Panel1)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Labeling_And_Tooltip_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Labeling And Tooltip"
        Me.TopMost = True
        CType(Me.Life_Labeling_And_Tooltip_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Stored_Labeling_And_Tooltip_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Life_Labeling_And_Tooltip_DGV As DataGridView
    Friend WithEvents Stored_Labeling_And_Tooltip_DGV As DataGridView
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Life_Labeling_And_Tooltip_Lbl As Label
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents BindingNavigator1 As BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
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
    Friend WithEvents Preview_Stored_Labeling_And_Tooltip_Btn As Button
    Friend WithEvents Preview_Life_Labeling_And_Tooltip_Btn As Button
    Friend WithEvents Stored_Labeling_And_Tooltip_Lbl As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Form_Objects_Lbl As Label
    Friend WithEvents Available_Forms_Lbl As Label
    Friend WithEvents Form_Objects_CmbBx As ComboBox
    Friend WithEvents Available_Forms_CmbBx As ComboBox
    Friend WithEvents Shortcuts_CmbBx As ComboBox
    Friend WithEvents Shortcuts_Lbl As Label
    Friend WithEvents Reload_Shortcuts_Tooltip_TlStrpBtn As ToolStripButton
    Friend WithEvents Use_Stored_Data_Tab_ChkBx As CheckBox
    Friend WithEvents Open_Note_In_New_Tab_Lbl As Label
End Class
