<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Flowl_Layout_Panel_Form
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

    'Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If (components IsNot Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents wrapContentsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents flowTopDownBtn As System.Windows.Forms.RadioButton
    Friend WithEvents flowBottomUpBtn As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerNonUserCode()> Private Sub InitializeComponent()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.wrapContentsCheckBox = New System.Windows.Forms.CheckBox()
        Me.flowTopDownBtn = New System.Windows.Forms.RadioButton()
        Me.flowBottomUpBtn = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FlowRightLeft_RdioBtn = New System.Windows.Forms.RadioButton()
        Me.FlowLeftRight_RdioBtn = New System.Windows.Forms.RadioButton()
        Me.AutoScroll_CheckBox = New System.Windows.Forms.CheckBox()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(346, 147)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        '
        'wrapContentsCheckBox
        '
        Me.wrapContentsCheckBox.Checked = True
        Me.wrapContentsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.wrapContentsCheckBox.Location = New System.Drawing.Point(121, 36)
        Me.wrapContentsCheckBox.Name = "wrapContentsCheckBox"
        Me.wrapContentsCheckBox.Size = New System.Drawing.Size(104, 24)
        Me.wrapContentsCheckBox.TabIndex = 1
        Me.wrapContentsCheckBox.Text = "Wrap Contents"
        '
        'flowTopDownBtn
        '
        Me.flowTopDownBtn.Location = New System.Drawing.Point(12, 35)
        Me.flowTopDownBtn.Name = "flowTopDownBtn"
        Me.flowTopDownBtn.Size = New System.Drawing.Size(104, 24)
        Me.flowTopDownBtn.TabIndex = 2
        Me.flowTopDownBtn.Text = "Flow TopDown"
        '
        'flowBottomUpBtn
        '
        Me.flowBottomUpBtn.Location = New System.Drawing.Point(12, 64)
        Me.flowBottomUpBtn.Name = "flowBottomUpBtn"
        Me.flowBottomUpBtn.Size = New System.Drawing.Size(104, 24)
        Me.flowBottomUpBtn.TabIndex = 3
        Me.flowBottomUpBtn.Text = "Flow BottomUp"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.AutoScroll_CheckBox)
        Me.Panel1.Controls.Add(Me.FlowRightLeft_RdioBtn)
        Me.Panel1.Controls.Add(Me.FlowLeftRight_RdioBtn)
        Me.Panel1.Controls.Add(Me.wrapContentsCheckBox)
        Me.Panel1.Controls.Add(Me.flowTopDownBtn)
        Me.Panel1.Controls.Add(Me.flowBottomUpBtn)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 147)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(346, 152)
        Me.Panel1.TabIndex = 6
        '
        'FlowRightLeft_RdioBtn
        '
        Me.FlowRightLeft_RdioBtn.Location = New System.Drawing.Point(12, 122)
        Me.FlowRightLeft_RdioBtn.Name = "FlowRightLeft_RdioBtn"
        Me.FlowRightLeft_RdioBtn.Size = New System.Drawing.Size(104, 24)
        Me.FlowRightLeft_RdioBtn.TabIndex = 7
        Me.FlowRightLeft_RdioBtn.Text = "Flow RightLeft"
        '
        'FlowLeftRight_RdioBtn
        '
        Me.FlowLeftRight_RdioBtn.Location = New System.Drawing.Point(12, 93)
        Me.FlowLeftRight_RdioBtn.Name = "FlowLeftRight_RdioBtn"
        Me.FlowLeftRight_RdioBtn.Size = New System.Drawing.Size(104, 24)
        Me.FlowLeftRight_RdioBtn.TabIndex = 6
        Me.FlowLeftRight_RdioBtn.Text = "Flow LeftRight"
        '
        'AutoScroll_CheckBox
        '
        Me.AutoScroll_CheckBox.Location = New System.Drawing.Point(121, 64)
        Me.AutoScroll_CheckBox.Name = "AutoScroll_CheckBox"
        Me.AutoScroll_CheckBox.Size = New System.Drawing.Size(104, 24)
        Me.AutoScroll_CheckBox.TabIndex = 8
        Me.AutoScroll_CheckBox.Text = "Auto Scroll"
        '
        'Flowl_Layout_Panel_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 299)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Flowl_Layout_Panel_Form"
        Me.Text = "Flowl_Layout_Panel_Form"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend Shared ReadOnly Property GetInstance() As Flowl_Layout_Panel_Form
        Get
            If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                SyncLock m_SyncObject
                    If m_DefaultInstance Is Nothing OrElse m_DefaultInstance.IsDisposed() Then
                        m_DefaultInstance = New Flowl_Layout_Panel_Form
                    End If
                End SyncLock
            End If
            Return m_DefaultInstance
        End Get
    End Property

    Private Shared m_DefaultInstance As Flowl_Layout_Panel_Form
    Private Shared m_SyncObject As New Object
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FlowLeftRight_RdioBtn As RadioButton
    Friend WithEvents FlowRightLeft_RdioBtn As RadioButton
    Friend WithEvents AutoScroll_CheckBox As CheckBox
#End Region
End Class
