<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MS_Excel_Form
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MS_Excel_Form))
        Me.MS_Excel_DGV = New System.Windows.Forms.DataGridView()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.Open_Excel_File_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Save_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Exit_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        CType(Me.MS_Excel_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MS_Excel_DGV
        '
        Me.MS_Excel_DGV.AllowUserToAddRows = False
        Me.MS_Excel_DGV.AllowUserToDeleteRows = False
        Me.MS_Excel_DGV.AllowUserToOrderColumns = True
        Me.MS_Excel_DGV.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MS_Excel_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.MS_Excel_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MS_Excel_DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MS_Excel_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.MS_Excel_DGV.EnableHeadersVisualStyles = False
        Me.MS_Excel_DGV.Location = New System.Drawing.Point(0, 27)
        Me.MS_Excel_DGV.MultiSelect = False
        Me.MS_Excel_DGV.Name = "MS_Excel_DGV"
        Me.MS_Excel_DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle2.NullValue = Nothing
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MS_Excel_DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MS_Excel_DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.MS_Excel_DGV.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.MS_Excel_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MS_Excel_DGV.Size = New System.Drawing.Size(800, 423)
        Me.MS_Excel_DGV.TabIndex = 1
        Me.MS_Excel_DGV.TabStop = False
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.BackColor = System.Drawing.Color.Transparent
        Me.BindingNavigator1.CountItem = Nothing
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.BindingNavigator1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Open_Excel_File_TlStrpBtn, Me.Save_TlStrpBtn, Me.BindingNavigatorDeleteItem, Me.ToolStripSeparator1, Me.Exit_TlStrpBtn})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Nothing
        Me.BindingNavigator1.MoveLastItem = Nothing
        Me.BindingNavigator1.MoveNextItem = Nothing
        Me.BindingNavigator1.MovePreviousItem = Nothing
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Nothing
        Me.BindingNavigator1.Size = New System.Drawing.Size(800, 27)
        Me.BindingNavigator1.TabIndex = 16
        Me.BindingNavigator1.Text = "BindingNavigator1"
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
        'Open_Excel_File_TlStrpBtn
        '
        Me.Open_Excel_File_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Open_Excel_File_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources.open_folder_outline_icon
        Me.Open_Excel_File_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Open_Excel_File_TlStrpBtn.Name = "Open_Excel_File_TlStrpBtn"
        Me.Open_Excel_File_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.Open_Excel_File_TlStrpBtn.Text = "Open Excel File"
        Me.Open_Excel_File_TlStrpBtn.ToolTipText = "Open Excel File فتح ملف اكسل"
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'Exit_TlStrpBtn
        '
        Me.Exit_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Exit_TlStrpBtn.Image = Global.MagNote.My.Resources.Resources._exit
        Me.Exit_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Exit_TlStrpBtn.Name = "Exit_TlStrpBtn"
        Me.Exit_TlStrpBtn.Size = New System.Drawing.Size(24, 24)
        Me.Exit_TlStrpBtn.Text = "Exit"
        Me.Exit_TlStrpBtn.ToolTipText = "Exit Excel"
        '
        'MS_Excel_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.MS_Excel_DGV)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Name = "MS_Excel_Form"
        Me.Text = "MS_Excel_Form"
        CType(Me.MS_Excel_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MS_Excel_DGV As DataGridView
    Friend WithEvents BindingNavigator1 As BindingNavigator
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents Open_Excel_File_TlStrpBtn As ToolStripButton
    Friend WithEvents Save_TlStrpBtn As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Exit_TlStrpBtn As ToolStripButton
End Class
