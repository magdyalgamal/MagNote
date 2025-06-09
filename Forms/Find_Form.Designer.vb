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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Find_Form))
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
        Me.Find_In_Avilable_MagNotes_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Find_In_Avilable_MagNotes_Lbl = New System.Windows.Forms.Label()
        Me.Find_PrgrssBr = New System.Windows.Forms.ProgressBar()
        Me.Back_Color_To_Find_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Back_Color_To_Replace_By_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Back_Color_To_Replace_By_Lbl = New System.Windows.Forms.Label()
        Me.Back_Color_To_Find_Lbl = New System.Windows.Forms.Label()
        Me.Fore_Color_To_Find_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Fore_Color_To_Find_Lbl = New System.Windows.Forms.Label()
        Me.Fore_Color_To_Replace_By_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Fore_Color_To_Replace_By_Lbl = New System.Windows.Forms.Label()
        Me.Show_Back_Color_To_Find_Btn = New System.Windows.Forms.Button()
        Me.Show_Fore_Color_To_Find_Btn = New System.Windows.Forms.Button()
        Me.Show_Back_Color_To_Replace_By_Btn = New System.Windows.Forms.Button()
        Me.Show_Fore_Color_To_Replace_By_Btn = New System.Windows.Forms.Button()
        Me.Clear_Previously_Searched_Phrase_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Clear_Previously_Searched_Phrase_Lbl = New System.Windows.Forms.Label()
        Me.Only_For_Colors_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Only_For_Colors_Lbl = New System.Windows.Forms.Label()
        Me.Text_Selection_Lbl = New System.Windows.Forms.Label()
        Me.StartLbl = New System.Windows.Forms.Label()
        Me.EndLbl = New System.Windows.Forms.Label()
        Me.LengthLbl = New System.Windows.Forms.Label()
        Me.Selection_Start_TxtBx = New System.Windows.Forms.TextBox()
        Me.Selection_End_TxtBx = New System.Windows.Forms.TextBox()
        Me.Selection_Length_TxtBx = New System.Windows.Forms.TextBox()
        Me.RCSN_Text_Length_TxtBx = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
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
        'FindIn_CmbBx
        '
        Me.FindIn_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FindIn_CmbBx.FormattingEnabled = True
        Me.FindIn_CmbBx.Location = New System.Drawing.Point(171, 133)
        Me.FindIn_CmbBx.Name = "FindIn_CmbBx"
        Me.FindIn_CmbBx.Size = New System.Drawing.Size(196, 22)
        Me.FindIn_CmbBx.TabIndex = 991
        '
        'FindIn_Lbl
        '
        Me.FindIn_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.FindIn_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FindIn_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.FindIn_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FindIn_Lbl.Location = New System.Drawing.Point(8, 133)
        Me.FindIn_Lbl.Name = "FindIn_Lbl"
        Me.FindIn_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.FindIn_Lbl.TabIndex = 990
        Me.FindIn_Lbl.Text = "Find In"
        Me.FindIn_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FindText_CmbBx
        '
        Me.FindText_CmbBx.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.FindText_CmbBx.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.FindText_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FindText_CmbBx.FormattingEnabled = True
        Me.FindText_CmbBx.Location = New System.Drawing.Point(171, 156)
        Me.FindText_CmbBx.Name = "FindText_CmbBx"
        Me.FindText_CmbBx.Size = New System.Drawing.Size(385, 22)
        Me.FindText_CmbBx.Sorted = True
        Me.FindText_CmbBx.TabIndex = 993
        '
        'FindText_Lbl
        '
        Me.FindText_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.FindText_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FindText_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.FindText_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FindText_Lbl.Location = New System.Drawing.Point(8, 156)
        Me.FindText_Lbl.Name = "FindText_Lbl"
        Me.FindText_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.FindText_Lbl.TabIndex = 992
        Me.FindText_Lbl.Text = "Find Text"
        Me.FindText_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ReplaceBy_Lbl
        '
        Me.ReplaceBy_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.ReplaceBy_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ReplaceBy_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.ReplaceBy_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ReplaceBy_Lbl.Location = New System.Drawing.Point(8, 179)
        Me.ReplaceBy_Lbl.Name = "ReplaceBy_Lbl"
        Me.ReplaceBy_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.ReplaceBy_Lbl.TabIndex = 994
        Me.ReplaceBy_Lbl.Text = "Replace By"
        Me.ReplaceBy_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ReplaceBy_TxtBx
        '
        Me.ReplaceBy_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReplaceBy_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.ReplaceBy_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ReplaceBy_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ReplaceBy_TxtBx.Location = New System.Drawing.Point(171, 179)
        Me.ReplaceBy_TxtBx.Multiline = True
        Me.ReplaceBy_TxtBx.Name = "ReplaceBy_TxtBx"
        Me.ReplaceBy_TxtBx.Size = New System.Drawing.Size(385, 22)
        Me.ReplaceBy_TxtBx.TabIndex = 995
        '
        'FindNext_Btn
        '
        Me.FindNext_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FindNext_Btn.Location = New System.Drawing.Point(34, 345)
        Me.FindNext_Btn.Name = "FindNext_Btn"
        Me.FindNext_Btn.Size = New System.Drawing.Size(83, 36)
        Me.FindNext_Btn.TabIndex = 996
        Me.FindNext_Btn.Text = "Find Next"
        Me.FindNext_Btn.UseVisualStyleBackColor = True
        '
        'FindAll_Btn
        '
        Me.FindAll_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.FindAll_Btn.Location = New System.Drawing.Point(117, 345)
        Me.FindAll_Btn.Name = "FindAll_Btn"
        Me.FindAll_Btn.Size = New System.Drawing.Size(83, 36)
        Me.FindAll_Btn.TabIndex = 997
        Me.FindAll_Btn.Text = "Find All"
        Me.FindAll_Btn.UseVisualStyleBackColor = True
        '
        'ReplaceNext_Btn
        '
        Me.ReplaceNext_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ReplaceNext_Btn.Location = New System.Drawing.Point(200, 345)
        Me.ReplaceNext_Btn.Name = "ReplaceNext_Btn"
        Me.ReplaceNext_Btn.Size = New System.Drawing.Size(83, 36)
        Me.ReplaceNext_Btn.TabIndex = 998
        Me.ReplaceNext_Btn.Text = "Replace Next"
        Me.ReplaceNext_Btn.UseVisualStyleBackColor = True
        '
        'ReplaceAll_Btn
        '
        Me.ReplaceAll_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ReplaceAll_Btn.Location = New System.Drawing.Point(283, 345)
        Me.ReplaceAll_Btn.Name = "ReplaceAll_Btn"
        Me.ReplaceAll_Btn.Size = New System.Drawing.Size(83, 36)
        Me.ReplaceAll_Btn.TabIndex = 999
        Me.ReplaceAll_Btn.Text = "Replace All"
        Me.ReplaceAll_Btn.UseVisualStyleBackColor = True
        '
        'Clear_Search_Result_Btn
        '
        Me.Clear_Search_Result_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Clear_Search_Result_Btn.Location = New System.Drawing.Point(366, 345)
        Me.Clear_Search_Result_Btn.Name = "Clear_Search_Result_Btn"
        Me.Clear_Search_Result_Btn.Size = New System.Drawing.Size(83, 36)
        Me.Clear_Search_Result_Btn.TabIndex = 1000
        Me.Clear_Search_Result_Btn.Text = "Clear Search Result"
        Me.Clear_Search_Result_Btn.UseVisualStyleBackColor = True
        '
        'Exit_Btn
        '
        Me.Exit_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Exit_Btn.Location = New System.Drawing.Point(449, 345)
        Me.Exit_Btn.Name = "Exit_Btn"
        Me.Exit_Btn.Size = New System.Drawing.Size(83, 36)
        Me.Exit_Btn.TabIndex = 1001
        Me.Exit_Btn.Text = "Exit"
        Me.Exit_Btn.UseVisualStyleBackColor = True
        '
        'Find_In_Avilable_MagNotes_ChkBx
        '
        Me.Find_In_Avilable_MagNotes_ChkBx.BackColor = System.Drawing.Color.Transparent
        Me.Find_In_Avilable_MagNotes_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Find_In_Avilable_MagNotes_ChkBx.Checked = True
        Me.Find_In_Avilable_MagNotes_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Find_In_Avilable_MagNotes_ChkBx.Location = New System.Drawing.Point(10, 88)
        Me.Find_In_Avilable_MagNotes_ChkBx.Name = "Find_In_Avilable_MagNotes_ChkBx"
        Me.Find_In_Avilable_MagNotes_ChkBx.Size = New System.Drawing.Size(176, 20)
        Me.Find_In_Avilable_MagNotes_ChkBx.TabIndex = 1003
        Me.Find_In_Avilable_MagNotes_ChkBx.Text = "Find In Avilable MagNotes"
        Me.Find_In_Avilable_MagNotes_ChkBx.UseVisualStyleBackColor = False
        '
        'Find_In_Avilable_MagNotes_Lbl
        '
        Me.Find_In_Avilable_MagNotes_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Find_In_Avilable_MagNotes_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Find_In_Avilable_MagNotes_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Find_In_Avilable_MagNotes_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Find_In_Avilable_MagNotes_Lbl.Location = New System.Drawing.Point(8, 87)
        Me.Find_In_Avilable_MagNotes_Lbl.Name = "Find_In_Avilable_MagNotes_Lbl"
        Me.Find_In_Avilable_MagNotes_Lbl.Size = New System.Drawing.Size(183, 22)
        Me.Find_In_Avilable_MagNotes_Lbl.TabIndex = 1002
        Me.Find_In_Avilable_MagNotes_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Find_PrgrssBr
        '
        Me.Find_PrgrssBr.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Find_PrgrssBr.Location = New System.Drawing.Point(35, 325)
        Me.Find_PrgrssBr.Name = "Find_PrgrssBr"
        Me.Find_PrgrssBr.Size = New System.Drawing.Size(496, 14)
        Me.Find_PrgrssBr.TabIndex = 1004
        '
        'Back_Color_To_Find_ClrCmbBx
        '
        Me.Back_Color_To_Find_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Back_Color_To_Find_ClrCmbBx.Enabled = False
        Me.Back_Color_To_Find_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 8.5!)
        Me.Back_Color_To_Find_ClrCmbBx.FormattingEnabled = True
        Me.Back_Color_To_Find_ClrCmbBx.IncludeSystemColors = True
        Me.Back_Color_To_Find_ClrCmbBx.IncludeTransparent = True
        Me.Back_Color_To_Find_ClrCmbBx.Location = New System.Drawing.Point(194, 202)
        Me.Back_Color_To_Find_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Back_Color_To_Find_ClrCmbBx.Name = "Back_Color_To_Find_ClrCmbBx"
        Me.Back_Color_To_Find_ClrCmbBx.Size = New System.Drawing.Size(165, 22)
        Me.Back_Color_To_Find_ClrCmbBx.SortAlphabetically = True
        Me.Back_Color_To_Find_ClrCmbBx.TabIndex = 1078
        '
        'Back_Color_To_Replace_By_ClrCmbBx
        '
        Me.Back_Color_To_Replace_By_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Back_Color_To_Replace_By_ClrCmbBx.Enabled = False
        Me.Back_Color_To_Replace_By_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Color_To_Replace_By_ClrCmbBx.FormattingEnabled = True
        Me.Back_Color_To_Replace_By_ClrCmbBx.IncludeSystemColors = True
        Me.Back_Color_To_Replace_By_ClrCmbBx.IncludeTransparent = True
        Me.Back_Color_To_Replace_By_ClrCmbBx.Location = New System.Drawing.Point(194, 225)
        Me.Back_Color_To_Replace_By_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Back_Color_To_Replace_By_ClrCmbBx.Name = "Back_Color_To_Replace_By_ClrCmbBx"
        Me.Back_Color_To_Replace_By_ClrCmbBx.Size = New System.Drawing.Size(165, 22)
        Me.Back_Color_To_Replace_By_ClrCmbBx.SortAlphabetically = True
        Me.Back_Color_To_Replace_By_ClrCmbBx.TabIndex = 1079
        '
        'Back_Color_To_Replace_By_Lbl
        '
        Me.Back_Color_To_Replace_By_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Back_Color_To_Replace_By_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Back_Color_To_Replace_By_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Back_Color_To_Replace_By_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Back_Color_To_Replace_By_Lbl.Location = New System.Drawing.Point(8, 225)
        Me.Back_Color_To_Replace_By_Lbl.Name = "Back_Color_To_Replace_By_Lbl"
        Me.Back_Color_To_Replace_By_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.Back_Color_To_Replace_By_Lbl.TabIndex = 1077
        Me.Back_Color_To_Replace_By_Lbl.Text = "Back Color To Replace By"
        Me.Back_Color_To_Replace_By_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Back_Color_To_Find_Lbl
        '
        Me.Back_Color_To_Find_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Back_Color_To_Find_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Back_Color_To_Find_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Back_Color_To_Find_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Back_Color_To_Find_Lbl.Location = New System.Drawing.Point(8, 202)
        Me.Back_Color_To_Find_Lbl.Name = "Back_Color_To_Find_Lbl"
        Me.Back_Color_To_Find_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.Back_Color_To_Find_Lbl.TabIndex = 1076
        Me.Back_Color_To_Find_Lbl.Text = "Back Color To Find"
        Me.Back_Color_To_Find_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Fore_Color_To_Find_ClrCmbBx
        '
        Me.Fore_Color_To_Find_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Fore_Color_To_Find_ClrCmbBx.Enabled = False
        Me.Fore_Color_To_Find_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 8.5!)
        Me.Fore_Color_To_Find_ClrCmbBx.FormattingEnabled = True
        Me.Fore_Color_To_Find_ClrCmbBx.IncludeSystemColors = True
        Me.Fore_Color_To_Find_ClrCmbBx.IncludeTransparent = True
        Me.Fore_Color_To_Find_ClrCmbBx.Location = New System.Drawing.Point(194, 248)
        Me.Fore_Color_To_Find_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Fore_Color_To_Find_ClrCmbBx.Name = "Fore_Color_To_Find_ClrCmbBx"
        Me.Fore_Color_To_Find_ClrCmbBx.Size = New System.Drawing.Size(165, 22)
        Me.Fore_Color_To_Find_ClrCmbBx.SortAlphabetically = True
        Me.Fore_Color_To_Find_ClrCmbBx.TabIndex = 1081
        '
        'Fore_Color_To_Find_Lbl
        '
        Me.Fore_Color_To_Find_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Fore_Color_To_Find_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Fore_Color_To_Find_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Fore_Color_To_Find_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Fore_Color_To_Find_Lbl.Location = New System.Drawing.Point(8, 248)
        Me.Fore_Color_To_Find_Lbl.Name = "Fore_Color_To_Find_Lbl"
        Me.Fore_Color_To_Find_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.Fore_Color_To_Find_Lbl.TabIndex = 1080
        Me.Fore_Color_To_Find_Lbl.Text = "Fore Color To Find"
        Me.Fore_Color_To_Find_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Fore_Color_To_Replace_By_ClrCmbBx
        '
        Me.Fore_Color_To_Replace_By_ClrCmbBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Fore_Color_To_Replace_By_ClrCmbBx.Enabled = False
        Me.Fore_Color_To_Replace_By_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fore_Color_To_Replace_By_ClrCmbBx.FormattingEnabled = True
        Me.Fore_Color_To_Replace_By_ClrCmbBx.IncludeSystemColors = True
        Me.Fore_Color_To_Replace_By_ClrCmbBx.IncludeTransparent = True
        Me.Fore_Color_To_Replace_By_ClrCmbBx.Location = New System.Drawing.Point(194, 271)
        Me.Fore_Color_To_Replace_By_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Fore_Color_To_Replace_By_ClrCmbBx.Name = "Fore_Color_To_Replace_By_ClrCmbBx"
        Me.Fore_Color_To_Replace_By_ClrCmbBx.Size = New System.Drawing.Size(165, 22)
        Me.Fore_Color_To_Replace_By_ClrCmbBx.SortAlphabetically = True
        Me.Fore_Color_To_Replace_By_ClrCmbBx.TabIndex = 1083
        '
        'Fore_Color_To_Replace_By_Lbl
        '
        Me.Fore_Color_To_Replace_By_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Fore_Color_To_Replace_By_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Fore_Color_To_Replace_By_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Fore_Color_To_Replace_By_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Fore_Color_To_Replace_By_Lbl.Location = New System.Drawing.Point(8, 271)
        Me.Fore_Color_To_Replace_By_Lbl.Name = "Fore_Color_To_Replace_By_Lbl"
        Me.Fore_Color_To_Replace_By_Lbl.Size = New System.Drawing.Size(162, 22)
        Me.Fore_Color_To_Replace_By_Lbl.TabIndex = 1082
        Me.Fore_Color_To_Replace_By_Lbl.Text = "Fore Color To Replace By"
        Me.Fore_Color_To_Replace_By_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Show_Back_Color_To_Find_Btn
        '
        Me.Show_Back_Color_To_Find_Btn.BackgroundImage = CType(resources.GetObject("Show_Back_Color_To_Find_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Show_Back_Color_To_Find_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Back_Color_To_Find_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Back_Color_To_Find_Btn.Location = New System.Drawing.Point(171, 202)
        Me.Show_Back_Color_To_Find_Btn.Name = "Show_Back_Color_To_Find_Btn"
        Me.Show_Back_Color_To_Find_Btn.Size = New System.Drawing.Size(22, 22)
        Me.Show_Back_Color_To_Find_Btn.TabIndex = 1171
        Me.Show_Back_Color_To_Find_Btn.UseVisualStyleBackColor = True
        '
        'Show_Fore_Color_To_Find_Btn
        '
        Me.Show_Fore_Color_To_Find_Btn.BackgroundImage = CType(resources.GetObject("Show_Fore_Color_To_Find_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Show_Fore_Color_To_Find_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Fore_Color_To_Find_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Fore_Color_To_Find_Btn.Location = New System.Drawing.Point(171, 248)
        Me.Show_Fore_Color_To_Find_Btn.Name = "Show_Fore_Color_To_Find_Btn"
        Me.Show_Fore_Color_To_Find_Btn.Size = New System.Drawing.Size(22, 22)
        Me.Show_Fore_Color_To_Find_Btn.TabIndex = 1172
        Me.Show_Fore_Color_To_Find_Btn.UseVisualStyleBackColor = True
        '
        'Show_Back_Color_To_Replace_By_Btn
        '
        Me.Show_Back_Color_To_Replace_By_Btn.BackgroundImage = CType(resources.GetObject("Show_Back_Color_To_Replace_By_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Show_Back_Color_To_Replace_By_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Back_Color_To_Replace_By_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Back_Color_To_Replace_By_Btn.Location = New System.Drawing.Point(171, 225)
        Me.Show_Back_Color_To_Replace_By_Btn.Name = "Show_Back_Color_To_Replace_By_Btn"
        Me.Show_Back_Color_To_Replace_By_Btn.Size = New System.Drawing.Size(22, 22)
        Me.Show_Back_Color_To_Replace_By_Btn.TabIndex = 1173
        Me.Show_Back_Color_To_Replace_By_Btn.UseVisualStyleBackColor = True
        '
        'Show_Fore_Color_To_Replace_By_Btn
        '
        Me.Show_Fore_Color_To_Replace_By_Btn.BackgroundImage = CType(resources.GetObject("Show_Fore_Color_To_Replace_By_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Show_Fore_Color_To_Replace_By_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Fore_Color_To_Replace_By_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Fore_Color_To_Replace_By_Btn.Location = New System.Drawing.Point(171, 271)
        Me.Show_Fore_Color_To_Replace_By_Btn.Name = "Show_Fore_Color_To_Replace_By_Btn"
        Me.Show_Fore_Color_To_Replace_By_Btn.Size = New System.Drawing.Size(22, 22)
        Me.Show_Fore_Color_To_Replace_By_Btn.TabIndex = 1174
        Me.Show_Fore_Color_To_Replace_By_Btn.UseVisualStyleBackColor = True
        '
        'Clear_Previously_Searched_Phrase_ChkBx
        '
        Me.Clear_Previously_Searched_Phrase_ChkBx.BackColor = System.Drawing.Color.Transparent
        Me.Clear_Previously_Searched_Phrase_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Clear_Previously_Searched_Phrase_ChkBx.Location = New System.Drawing.Point(10, 295)
        Me.Clear_Previously_Searched_Phrase_ChkBx.Name = "Clear_Previously_Searched_Phrase_ChkBx"
        Me.Clear_Previously_Searched_Phrase_ChkBx.Size = New System.Drawing.Size(179, 20)
        Me.Clear_Previously_Searched_Phrase_ChkBx.TabIndex = 1177
        Me.Clear_Previously_Searched_Phrase_ChkBx.Text = "Clear Previously Searched Phrase"
        Me.Clear_Previously_Searched_Phrase_ChkBx.UseVisualStyleBackColor = False
        '
        'Clear_Previously_Searched_Phrase_Lbl
        '
        Me.Clear_Previously_Searched_Phrase_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Clear_Previously_Searched_Phrase_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Clear_Previously_Searched_Phrase_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Clear_Previously_Searched_Phrase_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Clear_Previously_Searched_Phrase_Lbl.Location = New System.Drawing.Point(8, 294)
        Me.Clear_Previously_Searched_Phrase_Lbl.Name = "Clear_Previously_Searched_Phrase_Lbl"
        Me.Clear_Previously_Searched_Phrase_Lbl.Size = New System.Drawing.Size(186, 22)
        Me.Clear_Previously_Searched_Phrase_Lbl.TabIndex = 1176
        Me.Clear_Previously_Searched_Phrase_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Only_For_Colors_ChkBx
        '
        Me.Only_For_Colors_ChkBx.BackColor = System.Drawing.Color.Transparent
        Me.Only_For_Colors_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Only_For_Colors_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Only_For_Colors_ChkBx.Location = New System.Drawing.Point(10, 111)
        Me.Only_For_Colors_ChkBx.Name = "Only_For_Colors_ChkBx"
        Me.Only_For_Colors_ChkBx.Size = New System.Drawing.Size(176, 20)
        Me.Only_For_Colors_ChkBx.TabIndex = 1179
        Me.Only_For_Colors_ChkBx.Text = "Find And Replace Text Only"
        Me.Only_For_Colors_ChkBx.ThreeState = True
        Me.Only_For_Colors_ChkBx.UseVisualStyleBackColor = False
        '
        'Only_For_Colors_Lbl
        '
        Me.Only_For_Colors_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Only_For_Colors_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Only_For_Colors_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Only_For_Colors_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Only_For_Colors_Lbl.Location = New System.Drawing.Point(8, 110)
        Me.Only_For_Colors_Lbl.Name = "Only_For_Colors_Lbl"
        Me.Only_For_Colors_Lbl.Size = New System.Drawing.Size(183, 22)
        Me.Only_For_Colors_Lbl.TabIndex = 1178
        Me.Only_For_Colors_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Text_Selection_Lbl
        '
        Me.Text_Selection_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Text_Selection_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Text_Selection_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Text_Selection_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text_Selection_Lbl.Location = New System.Drawing.Point(360, 202)
        Me.Text_Selection_Lbl.Name = "Text_Selection_Lbl"
        Me.Text_Selection_Lbl.Size = New System.Drawing.Size(196, 22)
        Me.Text_Selection_Lbl.TabIndex = 1180
        Me.Text_Selection_Lbl.Text = "Text Selection"
        Me.Text_Selection_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StartLbl
        '
        Me.StartLbl.BackColor = System.Drawing.Color.Transparent
        Me.StartLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.StartLbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.StartLbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.StartLbl.Location = New System.Drawing.Point(360, 225)
        Me.StartLbl.Name = "StartLbl"
        Me.StartLbl.Size = New System.Drawing.Size(105, 22)
        Me.StartLbl.TabIndex = 1181
        Me.StartLbl.Text = "Start Selction"
        Me.StartLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'EndLbl
        '
        Me.EndLbl.BackColor = System.Drawing.Color.Transparent
        Me.EndLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.EndLbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.EndLbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.EndLbl.Location = New System.Drawing.Point(360, 248)
        Me.EndLbl.Name = "EndLbl"
        Me.EndLbl.Size = New System.Drawing.Size(105, 22)
        Me.EndLbl.TabIndex = 1182
        Me.EndLbl.Text = "End Selection"
        Me.EndLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LengthLbl
        '
        Me.LengthLbl.BackColor = System.Drawing.Color.Transparent
        Me.LengthLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LengthLbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.LengthLbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LengthLbl.Location = New System.Drawing.Point(360, 271)
        Me.LengthLbl.Name = "LengthLbl"
        Me.LengthLbl.Size = New System.Drawing.Size(105, 22)
        Me.LengthLbl.TabIndex = 1183
        Me.LengthLbl.Text = "Selection Length"
        Me.LengthLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Selection_Start_TxtBx
        '
        Me.Selection_Start_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Selection_Start_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Selection_Start_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Selection_Start_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Selection_Start_TxtBx.Location = New System.Drawing.Point(466, 225)
        Me.Selection_Start_TxtBx.Multiline = True
        Me.Selection_Start_TxtBx.Name = "Selection_Start_TxtBx"
        Me.Selection_Start_TxtBx.ReadOnly = True
        Me.Selection_Start_TxtBx.Size = New System.Drawing.Size(90, 22)
        Me.Selection_Start_TxtBx.TabIndex = 1184
        Me.Selection_Start_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Selection_End_TxtBx
        '
        Me.Selection_End_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Selection_End_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Selection_End_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Selection_End_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Selection_End_TxtBx.Location = New System.Drawing.Point(466, 248)
        Me.Selection_End_TxtBx.Multiline = True
        Me.Selection_End_TxtBx.Name = "Selection_End_TxtBx"
        Me.Selection_End_TxtBx.ReadOnly = True
        Me.Selection_End_TxtBx.Size = New System.Drawing.Size(90, 22)
        Me.Selection_End_TxtBx.TabIndex = 1185
        Me.Selection_End_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Selection_Length_TxtBx
        '
        Me.Selection_Length_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Selection_Length_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Selection_Length_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Selection_Length_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Selection_Length_TxtBx.Location = New System.Drawing.Point(466, 271)
        Me.Selection_Length_TxtBx.Multiline = True
        Me.Selection_Length_TxtBx.Name = "Selection_Length_TxtBx"
        Me.Selection_Length_TxtBx.ReadOnly = True
        Me.Selection_Length_TxtBx.Size = New System.Drawing.Size(90, 22)
        Me.Selection_Length_TxtBx.TabIndex = 1186
        Me.Selection_Length_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RCSN_Text_Length_TxtBx
        '
        Me.RCSN_Text_Length_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RCSN_Text_Length_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.RCSN_Text_Length_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RCSN_Text_Length_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.RCSN_Text_Length_TxtBx.Location = New System.Drawing.Point(466, 294)
        Me.RCSN_Text_Length_TxtBx.Multiline = True
        Me.RCSN_Text_Length_TxtBx.Name = "RCSN_Text_Length_TxtBx"
        Me.RCSN_Text_Length_TxtBx.ReadOnly = True
        Me.RCSN_Text_Length_TxtBx.Size = New System.Drawing.Size(90, 22)
        Me.RCSN_Text_Length_TxtBx.TabIndex = 1188
        Me.RCSN_Text_Length_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(360, 294)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 22)
        Me.Label1.TabIndex = 1189
        Me.Label1.Text = "File Length"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.MagNote_Header_Pnl.Size = New System.Drawing.Size(564, 79)
        Me.MagNote_Header_Pnl.TabIndex = 1190
        '
        'MagNote_File_Name_Lbl
        '
        Me.MagNote_File_Name_Lbl.AutoSize = True
        Me.MagNote_File_Name_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.MagNote_File_Name_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_File_Name_Lbl.Location = New System.Drawing.Point(463, 32)
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
        Me.Form_Controls_Pnl.Size = New System.Drawing.Size(564, 79)
        Me.Form_Controls_Pnl.TabIndex = 0
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
        Me.Maximize_Form_Btn.Location = New System.Drawing.Point(497, 23)
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
        Me.Minimize_Form_Btn.Location = New System.Drawing.Point(472, 23)
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
        Me.Exit_Form_Btn.Location = New System.Drawing.Point(521, 23)
        Me.Exit_Form_Btn.Name = "Exit_Form_Btn"
        Me.Exit_Form_Btn.Size = New System.Drawing.Size(24, 24)
        Me.Exit_Form_Btn.TabIndex = 1184
        Me.Exit_Form_Btn.UseVisualStyleBackColor = True
        '
        'Find_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 389)
        Me.Controls.Add(Me.MagNote_Header_Pnl)
        Me.Controls.Add(Me.LengthLbl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RCSN_Text_Length_TxtBx)
        Me.Controls.Add(Me.Selection_Length_TxtBx)
        Me.Controls.Add(Me.Selection_End_TxtBx)
        Me.Controls.Add(Me.Selection_Start_TxtBx)
        Me.Controls.Add(Me.EndLbl)
        Me.Controls.Add(Me.StartLbl)
        Me.Controls.Add(Me.Text_Selection_Lbl)
        Me.Controls.Add(Me.Only_For_Colors_ChkBx)
        Me.Controls.Add(Me.Only_For_Colors_Lbl)
        Me.Controls.Add(Me.Clear_Previously_Searched_Phrase_ChkBx)
        Me.Controls.Add(Me.Clear_Previously_Searched_Phrase_Lbl)
        Me.Controls.Add(Me.Show_Back_Color_To_Replace_By_Btn)
        Me.Controls.Add(Me.Show_Fore_Color_To_Replace_By_Btn)
        Me.Controls.Add(Me.Find_PrgrssBr)
        Me.Controls.Add(Me.Show_Fore_Color_To_Find_Btn)
        Me.Controls.Add(Me.Find_In_Avilable_MagNotes_ChkBx)
        Me.Controls.Add(Me.Fore_Color_To_Replace_By_ClrCmbBx)
        Me.Controls.Add(Me.Find_In_Avilable_MagNotes_Lbl)
        Me.Controls.Add(Me.Show_Back_Color_To_Find_Btn)
        Me.Controls.Add(Me.Exit_Btn)
        Me.Controls.Add(Me.Fore_Color_To_Replace_By_Lbl)
        Me.Controls.Add(Me.Clear_Search_Result_Btn)
        Me.Controls.Add(Me.Fore_Color_To_Find_ClrCmbBx)
        Me.Controls.Add(Me.ReplaceAll_Btn)
        Me.Controls.Add(Me.Fore_Color_To_Find_Lbl)
        Me.Controls.Add(Me.ReplaceNext_Btn)
        Me.Controls.Add(Me.Back_Color_To_Find_ClrCmbBx)
        Me.Controls.Add(Me.FindAll_Btn)
        Me.Controls.Add(Me.Back_Color_To_Replace_By_ClrCmbBx)
        Me.Controls.Add(Me.FindNext_Btn)
        Me.Controls.Add(Me.Back_Color_To_Replace_By_Lbl)
        Me.Controls.Add(Me.ReplaceBy_Lbl)
        Me.Controls.Add(Me.Back_Color_To_Find_Lbl)
        Me.Controls.Add(Me.ReplaceBy_TxtBx)
        Me.Controls.Add(Me.FindText_CmbBx)
        Me.Controls.Add(Me.FindText_Lbl)
        Me.Controls.Add(Me.FindIn_CmbBx)
        Me.Controls.Add(Me.FindIn_Lbl)
        Me.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Find_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find_Form"
        Me.TransparencyKey = System.Drawing.Color.Snow
        Me.MagNote_Header_Pnl.ResumeLayout(False)
        Me.MagNote_Header_Pnl.PerformLayout()
        Me.Form_Controls_Pnl.ResumeLayout(False)
        CType(Me.Infosysme_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Find_In_Avilable_MagNotes_ChkBx As CheckBox
    Friend WithEvents Find_In_Avilable_MagNotes_Lbl As Label
    Friend WithEvents Find_PrgrssBr As ProgressBar
    Friend WithEvents Back_Color_To_Find_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Back_Color_To_Replace_By_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Back_Color_To_Replace_By_Lbl As Label
    Friend WithEvents Back_Color_To_Find_Lbl As Label
    Friend WithEvents Fore_Color_To_Find_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Fore_Color_To_Find_Lbl As Label
    Friend WithEvents Fore_Color_To_Replace_By_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Fore_Color_To_Replace_By_Lbl As Label
    Friend WithEvents Show_Back_Color_To_Find_Btn As Button
    Friend WithEvents Show_Fore_Color_To_Find_Btn As Button
    Friend WithEvents Show_Back_Color_To_Replace_By_Btn As Button
    Friend WithEvents Show_Fore_Color_To_Replace_By_Btn As Button
    Friend WithEvents Clear_Previously_Searched_Phrase_ChkBx As CheckBox
    Friend WithEvents Clear_Previously_Searched_Phrase_Lbl As Label
    Friend WithEvents Only_For_Colors_ChkBx As CheckBox
    Friend WithEvents Only_For_Colors_Lbl As Label
    Friend WithEvents Text_Selection_Lbl As Label
    Friend WithEvents StartLbl As Label
    Friend WithEvents EndLbl As Label
    Friend WithEvents LengthLbl As Label
    Friend WithEvents Selection_Start_TxtBx As TextBox
    Friend WithEvents Selection_End_TxtBx As TextBox
    Friend WithEvents Selection_Length_TxtBx As TextBox
    Friend WithEvents RCSN_Text_Length_TxtBx As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents MagNote_Header_Pnl As Panel
    Friend WithEvents Form_Controls_Pnl As Panel
    Friend WithEvents Maximize_Form_Btn As Button
    Friend WithEvents Minimize_Form_Btn As Button
    Friend WithEvents Exit_Form_Btn As Button
    Friend WithEvents MagNote_File_Name_Lbl As Label
    Friend WithEvents Infosysme_PctrBx As PictureBox
End Class
