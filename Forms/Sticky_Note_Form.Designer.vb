
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sticky_Note_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Sticky_Note_Form))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.File_TlStrpMnItm = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CtxMenu_Col_Insert_Columns = New System.Windows.Forms.ToolStripMenuItem()
        Me.Column_CntxtMnStrp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Col_Cut_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Copy_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Paste_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Insert_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Delete_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_TlStrpSprtr_01 = New System.Windows.Forms.ToolStripSeparator()
        Me.Col_Reset_To_Default_Width_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Width_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Hide_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Unhide_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_TlStrpSprtr_02 = New System.Windows.Forms.ToolStripSeparator()
        Me.Col_Filter_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Clear_Filter_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_TlStrpSprtr_03 = New System.Windows.Forms.ToolStripSeparator()
        Me.Col_Group_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_UnGroup_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_UnGroup_All_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_TlStrpSprtr_04 = New System.Windows.Forms.ToolStripSeparator()
        Me.Col_Insert_Page_Break_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Remove_Page_Break_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_TlStrpSprtr_05 = New System.Windows.Forms.ToolStripSeparator()
        Me.Col_Properties_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Col_Format_Cells_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.General_Cell_Format_2_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Numeric_Cell_Format_2_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.DateTime_Cell_Format_2_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Currency_Cell_Format_2_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Text_Cell_Format_2_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_CntxtMnStrp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Cell_Cut_Range_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_Copy_Range_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_Paste_Range_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_TlStrpSprtr_01 = New System.Windows.Forms.ToolStripSeparator()
        Me.Cell_Select_All_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_TlStrpSprtr_02 = New System.Windows.Forms.ToolStripSeparator()
        Me.Cell_Merge_Range_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_UnMerge_Range_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_TlStrpSprtr_03 = New System.Windows.Forms.ToolStripSeparator()
        Me.Cell_Change_Type_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_TlStrpSprtr_04 = New System.Windows.Forms.ToolStripSeparator()
        Me.Cell_Format_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.General_Cell_Format_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Numeric_Cell_Format_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.DateTime_Cell_Format_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Currency_Cell_Format_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Text_Cell_Format_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cell_TlStrpSprtr_102 = New System.Windows.Forms.ToolStripSeparator()
        Me.Close_Grid_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_CntxtMnStrp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Row_Cut_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Copy_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Paste_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_TlStrpSprtr_01 = New System.Windows.Forms.ToolStripSeparator()
        Me.Row_Insert_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Delete_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_TlStrpSprtr_02 = New System.Windows.Forms.ToolStripSeparator()
        Me.Row_Reset_To_Default_Height_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Height_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Hide_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_UnHide_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_TlStrpSprtr_03 = New System.Windows.Forms.ToolStripSeparator()
        Me.Row_Group_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_UnGroup_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_UnGroup_All_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_TlStrpSprtr_04 = New System.Windows.Forms.ToolStripSeparator()
        Me.Row_Insert_Page_Break_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Remove_Page_Break_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_TlStrpSprtr_05 = New System.Windows.Forms.ToolStripSeparator()
        Me.Row_Properties_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Row_Format_Cells_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.General_Cell_Format_1_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Numeric_Cell_Format_1_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.DateTime_Cell_Format_1_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Currency_Cell_Format_1_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Text_Cell_Format_1_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Backup_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Stiky_TlStrp = New System.Windows.Forms.ToolStrip()
        Me.Compress_Me_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.New_Sticky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Open_Sticky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Save_Sticky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Delete_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Print_Sticky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Stiky_tlStrpSprtr_01 = New System.Windows.Forms.ToolStripSeparator()
        Me.Font_Name_Sticky_TlStrpSpltBtn = New System.Windows.Forms.ToolStripSplitButton()
        Me.Select_Font_Sticky_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Text_Color_Sticky_TlStrpSpltBtn = New System.Windows.Forms.ToolStripSplitButton()
        Me.Selected_Text_Color_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Remove_Selected_Text_Color_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Selected_Text_Backcolor_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Bold_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Underline_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.WordWrap_TlStrp = New System.Windows.Forms.ToolStripButton()
        Me.Font_Strikeout_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Stiky_tlStrpSprtr_02 = New System.Windows.Forms.ToolStripSeparator()
        Me.Read_ME_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Stiky_tlStrpSprtr_03 = New System.Windows.Forms.ToolStripSeparator()
        Me.Left_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Center_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Right_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Stiky_tlStrpSprtr_04 = New System.Windows.Forms.ToolStripSeparator()
        Me.Bullets_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Stiky_tlStrpSprtr_05 = New System.Windows.Forms.ToolStripSeparator()
        Me.Find_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Cut_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Copy_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Paste_Stiky_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Stiky_tlStrpSprtr_06 = New System.Windows.Forms.ToolStripSeparator()
        Me.Show_Hide_TlStrpSpltBtn = New System.Windows.Forms.ToolStripSplitButton()
        Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Show_Hide_Sticky_Note_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Show_Hide_Setting_Tab_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Show_Hide_Sticky_Grid_TlStrpMnItm = New System.Windows.Forms.ToolStripMenuItem()
        Me.Form_Size_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Exit_TlStrpBtn = New System.Windows.Forms.ToolStripButton()
        Me.Form_ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.WhatsApp_Btn = New System.Windows.Forms.Button()
        Me.Previous_Btn = New System.Windows.Forms.Button()
        Me.Next_Btn = New System.Windows.Forms.Button()
        Me.Language_Btn = New System.Windows.Forms.Button()
        Me.Add_New_Sticky_Note_Btn = New System.Windows.Forms.Button()
        Me.Reminder_Every_Minutes_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Reminder_Every_Hours_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Reminder_Every_Days_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Minutes_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Hours_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Days_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Save_Sticky_Form_Parameter_Setting_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Pause_Btn = New System.Windows.Forms.Button()
        Me.Start_Btn = New System.Windows.Forms.Button()
        Me.Stop_Btn = New System.Windows.Forms.Button()
        Me.Time_To_Alert_Before_Azan_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Alert_Repeet_Minute_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Alert_Repeet_Hour_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Alert_Repeet_Day_NmrcUpDn = New System.Windows.Forms.NumericUpDown()
        Me.Save_Alert_Btn = New System.Windows.Forms.Button()
        Me.Delete_Alert_Btn = New System.Windows.Forms.Button()
        Me.Pause_Alarm_Timer_Btn = New System.Windows.Forms.Button()
        Me.Hide_Me_PctrBx = New System.Windows.Forms.PictureBox()
        Me.MagNote_PctrBx = New System.Windows.Forms.PictureBox()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.Table_Pnl = New System.Windows.Forms.Panel()
        Me.Exit_Table_Pnl_Btn = New System.Windows.Forms.Button()
        Me.Insert_Btn = New System.Windows.Forms.Button()
        Me.Cell_Height_TxtBx = New System.Windows.Forms.TextBox()
        Me.Cell_Width_TxtBx = New System.Windows.Forms.TextBox()
        Me.Rows_TxtBx = New System.Windows.Forms.TextBox()
        Me.Columns_TxtBx = New System.Windows.Forms.TextBox()
        Me.Table_Grid_Pnl = New System.Windows.Forms.Panel()
        Me.Columns_Count_VScrllBr = New System.Windows.Forms.HScrollBar()
        Me.Table_DGV = New System.Windows.Forms.DataGridView()
        Me.Cell_Width_VScrllBr = New System.Windows.Forms.HScrollBar()
        Me.Rows_Count_VScrllBr = New System.Windows.Forms.VScrollBar()
        Me.Cell_Height_VScrllBr = New System.Windows.Forms.VScrollBar()
        Me.Grid_Sample_Lbl = New System.Windows.Forms.Label()
        Me.Cell_Height_Lbl = New System.Windows.Forms.Label()
        Me.Cell_Width_Lbl = New System.Windows.Forms.Label()
        Me.Rows_Lbl = New System.Windows.Forms.Label()
        Me.Columns_Lbl = New System.Windows.Forms.Label()
        Me.Form_Transparency_TrkBr = New System.Windows.Forms.TrackBar()
        Me.MsgBox_SttsStrp = New System.Windows.Forms.StatusStrip()
        Me.MsgBox_TlStrpSttsLbl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Sticky_Navigater_Pnl = New System.Windows.Forms.Panel()
        Me.Sticky_Note_No_CmbBx = New System.Windows.Forms.ComboBox()
        Me.File_Format_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Sticky_Note_Category_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Sticky_Note_Lbl = New System.Windows.Forms.Label()
        Me.Setting_TbCntrl = New System.Windows.Forms.TabControl()
        Me.Sticky_Notes_TbPg = New System.Windows.Forms.TabPage()
        Me.Available_Sticky_Notes_DGV = New System.Windows.Forms.DataGridView()
        Me.Sticky_Notes_Header_Pnl = New System.Windows.Forms.Panel()
        Me.Available_Sticky_Notes_Lbl = New System.Windows.Forms.Label()
        Me.View_Sticky_Notes_Btn = New System.Windows.Forms.Button()
        Me.Preview_Btn = New System.Windows.Forms.Button()
        Me.Sticky_Parameters_TbPg = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Grid_Panel_Size_Lbl = New System.Windows.Forms.Label()
        Me.Grid_Panel_Size_TxtBx = New System.Windows.Forms.TextBox()
        Me.Sticky_Font_Style_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Font_Size_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Font_Name_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Font_Style_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Sticky_Font_Size_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Sticky_Font_Name_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Sticky_Font_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Sticky_Back_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Sticky_Word_Wrap_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Sticky_Word_Wrap_Lbl = New System.Windows.Forms.Label()
        Me.Pending_Reminder_Alert_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Stop_Reminder_Alert_Lbl = New System.Windows.Forms.Label()
        Me.Next_Reminder_Time_DtTmPkr = New System.Windows.Forms.DateTimePicker()
        Me.Reminder_Every_Lbl = New System.Windows.Forms.Label()
        Me.Next_Reminder_Time_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Have_Reminder_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Sticky_Have_Reminder_Lbl = New System.Windows.Forms.Label()
        Me.Use_Main_Password_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Use_Main_Password_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Applicable_To_Rename_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Sticky_Applicable_To_Rename_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Password_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Password_TxtBx = New System.Windows.Forms.TextBox()
        Me.Secured_Sticky_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Secured_Sticky_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Back_Color_Lbl = New System.Windows.Forms.Label()
        Me.Finished_Sticky_ChkBx = New System.Windows.Forms.CheckBox()
        Me.View_Text_Font_Properties_Btn = New System.Windows.Forms.Button()
        Me.Blocked_Sticky_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Sticky_Font_TxtBx = New System.Windows.Forms.TextBox()
        Me.Blocked_Sticky_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Font_Lbl = New System.Windows.Forms.Label()
        Me.Finished_Sticky_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Font_Color_Lbl = New System.Windows.Forms.Label()
        Me.Form_Parameters_TbPg = New System.Windows.Forms.TabPage()
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl = New System.Windows.Forms.Label()
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl = New System.Windows.Forms.Label()
        Me.Hide_Windows_Desktop_Icons_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Hide_Windows_Desktop_Icons_Lbl = New System.Windows.Forms.Label()
        Me.Keep_Sticky_Opened_After_Delete_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Keep_Sticky_Opened_After_Delete_Lbl = New System.Windows.Forms.Label()
        Me.External_Sticky_Font_Style_Lbl = New System.Windows.Forms.Label()
        Me.External_Sticky_Font_Size_Lbl = New System.Windows.Forms.Label()
        Me.External_Sticky_Font_Name_Lbl = New System.Windows.Forms.Label()
        Me.External_Sticky_Font_Style_CmbBx = New System.Windows.Forms.ComboBox()
        Me.External_Sticky_Font_Size_CmbBx = New System.Windows.Forms.ComboBox()
        Me.External_Sticky_Font_Name_CmbBx = New System.Windows.Forms.ComboBox()
        Me.External_Sticky_Font_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.External_Sticky_Back_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.External_Sticky_Back_Color_Lbl = New System.Windows.Forms.Label()
        Me.View_External_Text_Font_Properties_Btn = New System.Windows.Forms.Button()
        Me.External_Sticky_Font_TxtBx = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.External_Sticky_Font_Color_Lbl = New System.Windows.Forms.Label()
        Me.Application_Starts_Minimized_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Application_Starts_Minimized_Lbl = New System.Windows.Forms.Label()
        Me.Setting_Tab_Control_Size_Lbl = New System.Windows.Forms.Label()
        Me.Setting_Tab_Control_Size_TxtBx = New System.Windows.Forms.TextBox()
        Me.Clear_Previous_Search_Result_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Clear_Previous_Search_Result_Lbl = New System.Windows.Forms.Label()
        Me.Run_Me_As_Administrator_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Run_Me_As_Administrator_Lbl = New System.Windows.Forms.Label()
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Reload_Sticky_Note_After_Change_Category_Lbl = New System.Windows.Forms.Label()
        Me.Cload_Area_Password_Lbl = New System.Windows.Forms.Label()
        Me.Cload_Area_Password_TxtBx = New System.Windows.Forms.TextBox()
        Me.Cload_Area_User_Lbl = New System.Windows.Forms.Label()
        Me.Cload_Area_User_TxtBx = New System.Windows.Forms.TextBox()
        Me.Cload_Area_Path_Lbl = New System.Windows.Forms.Label()
        Me.Cload_Area_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl = New System.Windows.Forms.Label()
        Me.Check_For_New_Version_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Check_For_New_Version_Lbl = New System.Windows.Forms.Label()
        Me.Load_Sticky_Note_At_Startup_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl = New System.Windows.Forms.Label()
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl = New System.Windows.Forms.Label()
        Me.Open_Sticky_In_New_Tab_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Open_Sticky_In_New_Tab_Lbl = New System.Windows.Forms.Label()
        Me.Stickies_Folder_Path_Btn = New System.Windows.Forms.Button()
        Me.Stickies_Folder_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Stickies_Folder_Path_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Form_Color_ClrCmbBx = New ColorsComboBox.ColorsComboBox()
        Me.Stop_Displaying_Controls_ToolTip_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Stop_Displaying_Controls_ToolTip_Lbl = New System.Windows.Forms.Label()
        Me.Me_Always_On_Top_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Me_Always_On_Top_Lbl = New System.Windows.Forms.Label()
        Me.Remember_Opened_External_Files_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Remember_Opened_External_Files_Lbl = New System.Windows.Forms.Label()
        Me.Me_As_Default_Text_File_Editor_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Me_As_Default_Text_File_Editor_Lbl = New System.Windows.Forms.Label()
        Me.Minimize_After_Running_My_Shortcut_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Minimize_After_Running_My_Shortcut_Lbl = New System.Windows.Forms.Label()
        Me.Me_Is_Compressed_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Me_Is_Compressed_Lbl = New System.Windows.Forms.Label()
        Me.Show_Sticky_Tab_Control_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Show_Sticky_Tab_Control_Lbl = New System.Windows.Forms.Label()
        Me.Enable_Maximize_Box_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Enable_Maximize_Box_Lbl = New System.Windows.Forms.Label()
        Me.Show_Form_Border_Style_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Show_Form_Border_Style_Lbl = New System.Windows.Forms.Label()
        Me.Double_Click_To_Run_Shortcut_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Double_Click_To_Run_Shortcut_Lbl = New System.Windows.Forms.Label()
        Me.Warning_Before_Delete_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Warning_Before_Delete_Lbl = New System.Windows.Forms.Label()
        Me.Warning_Before_Save_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Warning_Before_Save_Lbl = New System.Windows.Forms.Label()
        Me.Set_Control_To_Fill_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Set_Control_To_Fill_Lbl = New System.Windows.Forms.Label()
        Me.Main_Password_Lbl = New System.Windows.Forms.Label()
        Me.Main_Password_TxtBx = New System.Windows.Forms.TextBox()
        Me.Complex_Password_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Complex_Password_Lbl = New System.Windows.Forms.Label()
        Me.Enter_Password_To_Pass_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Enter_Password_To_Pass_Lbl = New System.Windows.Forms.Label()
        Me.Reload_Stickies_After_Amendments_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Reload_Stickies_After_Amendments_Lbl = New System.Windows.Forms.Label()
        Me.Backup_Folder_Path_Btn = New System.Windows.Forms.Button()
        Me.Backup_Folder_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Backup_Folder_Path_Lbl = New System.Windows.Forms.Label()
        Me.Next_Backup_Time_TxtBx = New System.Windows.Forms.TextBox()
        Me.Next_Backup_Time_Lbl = New System.Windows.Forms.Label()
        Me.Backup_Every_Lbl = New System.Windows.Forms.Label()
        Me.Periodically_Backup_Stickies_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Periodicaly_Backup_Stickies_Lbl = New System.Windows.Forms.Label()
        Me.Save_Setting_When_Exit_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Save_Setting_When_Exit_Lbl = New System.Windows.Forms.Label()
        Me.Form_Color_Like_Sticky_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Form_Color_Like_Sticky_Color_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Form_Opacity_TxtBx = New System.Windows.Forms.TextBox()
        Me.Sticky_Form_Size_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Form_Opacity_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Form_Size_TxtBx = New System.Windows.Forms.TextBox()
        Me.Sticky_Form_Location_TxtBx = New System.Windows.Forms.TextBox()
        Me.Sticky_Form_Location_Lbl = New System.Windows.Forms.Label()
        Me.Hide_Finished_Sticky_Note_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Run_Me_At_Windows_Startup_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Hide_Finished_Sticky_Note_Lbl = New System.Windows.Forms.Label()
        Me.Run_Me_At_Windows_Startup_Lbl = New System.Windows.Forms.Label()
        Me.Sticky_Form_Color_Lbl = New System.Windows.Forms.Label()
        Me.Shortcuts_TbPg = New System.Windows.Forms.TabPage()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ShortCut_TbCntrl = New System.Windows.Forms.TabControl()
        Me.Prayer_Time_TbPg = New System.Windows.Forms.TabPage()
        Me.Clock2 = New AnalogClock.Clock()
        Me.Time_Left_For_Alert_TxtBx = New System.Windows.Forms.TextBox()
        Me.Select_Alert_File_Path_Btn = New System.Windows.Forms.Button()
        Me.Alert_File_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Upload_Last_Version_Btn = New System.Windows.Forms.Button()
        Me.Alert_File_Path_Lbl = New System.Windows.Forms.Label()
        Me.Time_To_Alert_Before_Azan_Lbl = New System.Windows.Forms.Label()
        Me.Alert_Before_Azan_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Alert_Before_Azan_Lbl = New System.Windows.Forms.Label()
        Me.Azan_Takbeer_Only_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Azan_Takbeer_Only_Lbl = New System.Windows.Forms.Label()
        Me.Azan_Activation_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Azan_Activation_Lbl = New System.Windows.Forms.Label()
        Me.Stop_Fagr_Voice_File_Btn = New System.Windows.Forms.Button()
        Me.Play_Fagr_Voice_File_Btn = New System.Windows.Forms.Button()
        Me.Azan_Spoke_Method_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Azan_Spoke_Method_Lbl = New System.Windows.Forms.Label()
        Me.Voice_Azan_Files_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Fagr_Voice_Files_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Stop_Voice_Azan_File_Btn = New System.Windows.Forms.Button()
        Me.Play_Voice_Azan_File_Btn = New System.Windows.Forms.Button()
        Me.Save_Day_Light_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Save_Day_Light_Lbl = New System.Windows.Forms.Label()
        Me.Left_Time_Lbl = New System.Windows.Forms.Label()
        Me.Date_DtTmPkr = New System.Windows.Forms.DateTimePicker()
        Me.Date_Lbl = New System.Windows.Forms.Label()
        Me.Longitude_TxtBx = New System.Windows.Forms.TextBox()
        Me.Longitude_Lbl = New System.Windows.Forms.Label()
        Me.Latitude_TxtBx = New System.Windows.Forms.TextBox()
        Me.Latitude_Lbl = New System.Windows.Forms.Label()
        Me.City_CmbBx = New System.Windows.Forms.ComboBox()
        Me.City_Lbl = New System.Windows.Forms.Label()
        Me.Country_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Country_Lbl = New System.Windows.Forms.Label()
        Me.Calculation_Methods_CmbBx = New System.Windows.Forms.ComboBox()
        Me.Calculation_Methods_Lbl = New System.Windows.Forms.Label()
        Me.Isha_TxtBx = New System.Windows.Forms.TextBox()
        Me.Maghrib_TxtBx = New System.Windows.Forms.TextBox()
        Me.Asr_TxtBx = New System.Windows.Forms.TextBox()
        Me.Dhuhr_TxtBx = New System.Windows.Forms.TextBox()
        Me.Sunrise_TxtBx = New System.Windows.Forms.TextBox()
        Me.Fajr_TxtBx = New System.Windows.Forms.TextBox()
        Me.Isha_Lbl = New System.Windows.Forms.Label()
        Me.Maghrib_Lbl = New System.Windows.Forms.Label()
        Me.Asr_Lbl = New System.Windows.Forms.Label()
        Me.Dhuhr_Lbl = New System.Windows.Forms.Label()
        Me.Sunrise_Lbl = New System.Windows.Forms.Label()
        Me.Fajr_Lbl = New System.Windows.Forms.Label()
        Me.Alarm_Time_TbPg = New System.Windows.Forms.TabPage()
        Me.Available_Alerts_DGV = New System.Windows.Forms.DataGridView()
        Me.Available_Alarm_Header_Pnl = New System.Windows.Forms.Panel()
        Me.Preview_Available_Alerts_Btn = New System.Windows.Forms.Button()
        Me.Available_Alerts_Lbl = New System.Windows.Forms.Label()
        Me.Alarm_Setting_Pnl = New System.Windows.Forms.Panel()
        Me.Next_Current_Alarm_Time_Lbl = New System.Windows.Forms.Label()
        Me.Alert_Comment_TxtBx = New System.Windows.Forms.TextBox()
        Me.End_Alert_Time_Status_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Alert_Time_Lbl = New System.Windows.Forms.Label()
        Me.Alarm_Repeet_Time_Pnl = New System.Windows.Forms.Panel()
        Me.Alert_Repeet_Time_Lbl = New System.Windows.Forms.Label()
        Me.Stop_Playing_Alert_Btn = New System.Windows.Forms.Button()
        Me.Play_Alert_Btn = New System.Windows.Forms.Button()
        Me.Select_Alert_File_Btn = New System.Windows.Forms.Button()
        Me.Alert_Voice_Path_TxtBx = New System.Windows.Forms.TextBox()
        Me.Alert_Voice_Path_Lbl = New System.Windows.Forms.Label()
        Me.Clock1 = New AnalogClock.Clock()
        Me.Alert_Active_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Alert_Active_Lbl = New System.Windows.Forms.Label()
        Me.Alert_One_Time_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Alert_One_Time_Lbl = New System.Windows.Forms.Label()
        Me.End_Alert_Time_DtTmPikr = New System.Windows.Forms.DateTimePicker()
        Me.End_Alert_Time_Lbl = New System.Windows.Forms.Label()
        Me.Start_Alert_Time_DtTmPikr = New System.Windows.Forms.DateTimePicker()
        Me.Start_Alert_Time_Lbl = New System.Windows.Forms.Label()
        Me.Escalation_To_Author_TbPg = New System.Windows.Forms.TabPage()
        Me.User_Escalation_RchTxtBx = New System.Windows.Forms.RichTextBox()
        Me.Escalation_Pnl = New System.Windows.Forms.Panel()
        Me.Tack_Screen_Snapshot_ChkBx = New System.Windows.Forms.CheckBox()
        Me.Tack_Screen_Snapshot_Lbl = New System.Windows.Forms.Label()
        Me.With_Current_StickyNote_ChkBx = New System.Windows.Forms.CheckBox()
        Me.With_Current_StickyNote_Lbl = New System.Windows.Forms.Label()
        Me.Send_Escalation_To_Author_Btn = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Escalation_Auther_Mail_Lbl = New System.Windows.Forms.Label()
        Me.Escalation_Auther_Mail_TxtBx = New System.Windows.Forms.TextBox()
        Me.Escalation_Label_Lbl = New System.Windows.Forms.Label()
        Me.Escalation_Label_TxtBx = New System.Windows.Forms.TextBox()
        Me.Read_Me_Pnl = New System.Windows.Forms.Panel()
        Me.Windows_Volume_TrckBr = New System.Windows.Forms.TrackBar()
        Me.Arabic_Btn = New System.Windows.Forms.Button()
        Me.English_Btn = New System.Windows.Forms.Button()
        Me.Volume_TrcBr = New System.Windows.Forms.TrackBar()
        Me.Speaking_Rate_TrcBr = New System.Windows.Forms.TrackBar()
        Me.Speaking_Rate_Lbl = New System.Windows.Forms.Label()
        Me.Volume_Lbl = New System.Windows.Forms.Label()
        Me.cmbInstalled = New System.Windows.Forms.ComboBox()
        Me.Installed_Voices_Lbl = New System.Windows.Forms.Label()
        Me.Language_Lbl = New System.Windows.Forms.Label()
        Me.Stickies_Notes_TbCntrl = New System.Windows.Forms.TabControl()
        Me.Spliter_1_Lbl = New System.Windows.Forms.Panel()
        Me.Azan_Tmr = New System.Windows.Forms.Timer(Me.components)
        Me.Alarm_Tmr = New System.Windows.Forms.Timer(Me.components)
        Me.New_Alarm_Btn = New System.Windows.Forms.Button()
        Me.File_TlStrpMnItm.SuspendLayout()
        Me.Column_CntxtMnStrp.SuspendLayout()
        Me.Cell_CntxtMnStrp.SuspendLayout()
        Me.Row_CntxtMnStrp.SuspendLayout()
        Me.Stiky_TlStrp.SuspendLayout()
        CType(Me.Reminder_Every_Minutes_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Reminder_Every_Hours_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Reminder_Every_Days_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Minutes_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Hours_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Days_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Time_To_Alert_Before_Azan_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Alert_Repeet_Minute_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Alert_Repeet_Hour_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Alert_Repeet_Day_NmrcUpDn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Hide_Me_PctrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MagNote_PctrBx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Table_Pnl.SuspendLayout()
        Me.Table_Grid_Pnl.SuspendLayout()
        CType(Me.Table_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Form_Transparency_TrkBr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MsgBox_SttsStrp.SuspendLayout()
        Me.Sticky_Navigater_Pnl.SuspendLayout()
        Me.Setting_TbCntrl.SuspendLayout()
        Me.Sticky_Notes_TbPg.SuspendLayout()
        CType(Me.Available_Sticky_Notes_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Sticky_Notes_Header_Pnl.SuspendLayout()
        Me.Sticky_Parameters_TbPg.SuspendLayout()
        Me.Form_Parameters_TbPg.SuspendLayout()
        Me.Shortcuts_TbPg.SuspendLayout()
        Me.Prayer_Time_TbPg.SuspendLayout()
        Me.Alarm_Time_TbPg.SuspendLayout()
        CType(Me.Available_Alerts_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Available_Alarm_Header_Pnl.SuspendLayout()
        Me.Alarm_Setting_Pnl.SuspendLayout()
        Me.Alarm_Repeet_Time_Pnl.SuspendLayout()
        Me.Escalation_To_Author_TbPg.SuspendLayout()
        Me.Escalation_Pnl.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Read_Me_Pnl.SuspendLayout()
        CType(Me.Windows_Volume_TrckBr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Volume_TrcBr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Speaking_Rate_TrcBr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.Size = New System.Drawing.Size(150, 150)
        '
        'File_TlStrpMnItm
        '
        Me.File_TlStrpMnItm.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.File_TlStrpMnItm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CtxMenu_Col_Insert_Columns})
        Me.File_TlStrpMnItm.Name = "ContextMenuStrip1"
        Me.File_TlStrpMnItm.Size = New System.Drawing.Size(234, 26)
        '
        'CtxMenu_Col_Insert_Columns
        '
        Me.CtxMenu_Col_Insert_Columns.Name = "CtxMenu_Col_Insert_Columns"
        Me.CtxMenu_Col_Insert_Columns.Size = New System.Drawing.Size(233, 22)
        Me.CtxMenu_Col_Insert_Columns.Text = "CtxMenu_Col_Insert_Columns"
        '
        'Column_CntxtMnStrp
        '
        Me.Column_CntxtMnStrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Column_CntxtMnStrp.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column_CntxtMnStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Column_CntxtMnStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Col_Cut_TlStrpMnItm, Me.Col_Copy_TlStrpMnItm, Me.Col_Paste_TlStrpMnItm, Me.Col_Insert_TlStrpMnItm, Me.Col_Delete_TlStrpMnItm, Me.Col_TlStrpSprtr_01, Me.Col_Reset_To_Default_Width_TlStrpMnItm, Me.Col_Width_TlStrpMnItm, Me.Col_Hide_TlStrpMnItm, Me.Col_Unhide_TlStrpMnItm, Me.Col_TlStrpSprtr_02, Me.Col_Filter_TlStrpMnItm, Me.Col_Clear_Filter_TlStrpMnItm, Me.Col_TlStrpSprtr_03, Me.Col_Group_TlStrpMnItm, Me.Col_UnGroup_TlStrpMnItm, Me.Col_UnGroup_All_TlStrpMnItm, Me.Col_TlStrpSprtr_04, Me.Col_Insert_Page_Break_TlStrpMnItm, Me.Col_Remove_Page_Break_TlStrpMnItm, Me.Col_TlStrpSprtr_05, Me.Col_Properties_TlStrpMnItm, Me.Col_Format_Cells_TlStrpMnItm})
        Me.Column_CntxtMnStrp.Name = "Column_CntxtMnStrp"
        Me.Column_CntxtMnStrp.Size = New System.Drawing.Size(191, 502)
        '
        'Col_Cut_TlStrpMnItm
        '
        Me.Col_Cut_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Cut_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Cut_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Cut_TlStrpMnItm.Enabled = False
        Me.Col_Cut_TlStrpMnItm.Image = CType(resources.GetObject("Col_Cut_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Col_Cut_TlStrpMnItm.Name = "Col_Cut_TlStrpMnItm"
        Me.Col_Cut_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Cut_TlStrpMnItm.Text = "Cut"
        '
        'Col_Copy_TlStrpMnItm
        '
        Me.Col_Copy_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Copy_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Copy_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Copy_TlStrpMnItm.Enabled = False
        Me.Col_Copy_TlStrpMnItm.Image = CType(resources.GetObject("Col_Copy_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Col_Copy_TlStrpMnItm.Name = "Col_Copy_TlStrpMnItm"
        Me.Col_Copy_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Copy_TlStrpMnItm.Text = "Copy"
        '
        'Col_Paste_TlStrpMnItm
        '
        Me.Col_Paste_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Paste_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Paste_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Paste_TlStrpMnItm.Enabled = False
        Me.Col_Paste_TlStrpMnItm.Image = CType(resources.GetObject("Col_Paste_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Col_Paste_TlStrpMnItm.Name = "Col_Paste_TlStrpMnItm"
        Me.Col_Paste_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Paste_TlStrpMnItm.Text = "Paste"
        '
        'Col_Insert_TlStrpMnItm
        '
        Me.Col_Insert_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Insert_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Insert_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Insert_TlStrpMnItm.Name = "Col_Insert_TlStrpMnItm"
        Me.Col_Insert_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Insert_TlStrpMnItm.Text = "Insert &Columns"
        '
        'Col_Delete_TlStrpMnItm
        '
        Me.Col_Delete_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Delete_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Delete_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Delete_TlStrpMnItm.Name = "Col_Delete_TlStrpMnItm"
        Me.Col_Delete_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Delete_TlStrpMnItm.Text = "Delete Columns"
        '
        'Col_TlStrpSprtr_01
        '
        Me.Col_TlStrpSprtr_01.Name = "Col_TlStrpSprtr_01"
        Me.Col_TlStrpSprtr_01.Size = New System.Drawing.Size(187, 6)
        Me.Col_TlStrpSprtr_01.Visible = False
        '
        'Col_Reset_To_Default_Width_TlStrpMnItm
        '
        Me.Col_Reset_To_Default_Width_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Reset_To_Default_Width_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Reset_To_Default_Width_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Reset_To_Default_Width_TlStrpMnItm.Name = "Col_Reset_To_Default_Width_TlStrpMnItm"
        Me.Col_Reset_To_Default_Width_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Reset_To_Default_Width_TlStrpMnItm.Text = "Reset to Default Width"
        '
        'Col_Width_TlStrpMnItm
        '
        Me.Col_Width_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Width_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Width_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Width_TlStrpMnItm.Name = "Col_Width_TlStrpMnItm"
        Me.Col_Width_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Width_TlStrpMnItm.Text = "Column &Width..."
        '
        'Col_Hide_TlStrpMnItm
        '
        Me.Col_Hide_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Hide_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Hide_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Hide_TlStrpMnItm.Name = "Col_Hide_TlStrpMnItm"
        Me.Col_Hide_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Hide_TlStrpMnItm.Text = "Hide"
        '
        'Col_Unhide_TlStrpMnItm
        '
        Me.Col_Unhide_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Unhide_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Unhide_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Unhide_TlStrpMnItm.Name = "Col_Unhide_TlStrpMnItm"
        Me.Col_Unhide_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Unhide_TlStrpMnItm.Text = "Unhide"
        '
        'Col_TlStrpSprtr_02
        '
        Me.Col_TlStrpSprtr_02.Name = "Col_TlStrpSprtr_02"
        Me.Col_TlStrpSprtr_02.Size = New System.Drawing.Size(187, 6)
        Me.Col_TlStrpSprtr_02.Visible = False
        '
        'Col_Filter_TlStrpMnItm
        '
        Me.Col_Filter_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Filter_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Filter_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Filter_TlStrpMnItm.Image = CType(resources.GetObject("Col_Filter_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Col_Filter_TlStrpMnItm.Name = "Col_Filter_TlStrpMnItm"
        Me.Col_Filter_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Filter_TlStrpMnItm.Text = "Filter"
        '
        'Col_Clear_Filter_TlStrpMnItm
        '
        Me.Col_Clear_Filter_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Clear_Filter_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Clear_Filter_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Clear_Filter_TlStrpMnItm.Name = "Col_Clear_Filter_TlStrpMnItm"
        Me.Col_Clear_Filter_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Clear_Filter_TlStrpMnItm.Text = "Clear Filter"
        '
        'Col_TlStrpSprtr_03
        '
        Me.Col_TlStrpSprtr_03.Name = "Col_TlStrpSprtr_03"
        Me.Col_TlStrpSprtr_03.Size = New System.Drawing.Size(187, 6)
        Me.Col_TlStrpSprtr_03.Visible = False
        '
        'Col_Group_TlStrpMnItm
        '
        Me.Col_Group_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Group_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Group_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Group_TlStrpMnItm.Name = "Col_Group_TlStrpMnItm"
        Me.Col_Group_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Group_TlStrpMnItm.Text = "Group"
        Me.Col_Group_TlStrpMnItm.Visible = False
        '
        'Col_UnGroup_TlStrpMnItm
        '
        Me.Col_UnGroup_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_UnGroup_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_UnGroup_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_UnGroup_TlStrpMnItm.Name = "Col_UnGroup_TlStrpMnItm"
        Me.Col_UnGroup_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_UnGroup_TlStrpMnItm.Text = "Ungroup"
        Me.Col_UnGroup_TlStrpMnItm.Visible = False
        '
        'Col_UnGroup_All_TlStrpMnItm
        '
        Me.Col_UnGroup_All_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_UnGroup_All_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_UnGroup_All_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_UnGroup_All_TlStrpMnItm.Name = "Col_UnGroup_All_TlStrpMnItm"
        Me.Col_UnGroup_All_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_UnGroup_All_TlStrpMnItm.Text = "Ungroup All"
        Me.Col_UnGroup_All_TlStrpMnItm.Visible = False
        '
        'Col_TlStrpSprtr_04
        '
        Me.Col_TlStrpSprtr_04.Name = "Col_TlStrpSprtr_04"
        Me.Col_TlStrpSprtr_04.Size = New System.Drawing.Size(187, 6)
        Me.Col_TlStrpSprtr_04.Visible = False
        '
        'Col_Insert_Page_Break_TlStrpMnItm
        '
        Me.Col_Insert_Page_Break_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Insert_Page_Break_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Insert_Page_Break_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Insert_Page_Break_TlStrpMnItm.Name = "Col_Insert_Page_Break_TlStrpMnItm"
        Me.Col_Insert_Page_Break_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Insert_Page_Break_TlStrpMnItm.Text = "Insert Page Break"
        Me.Col_Insert_Page_Break_TlStrpMnItm.Visible = False
        '
        'Col_Remove_Page_Break_TlStrpMnItm
        '
        Me.Col_Remove_Page_Break_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Remove_Page_Break_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Remove_Page_Break_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Remove_Page_Break_TlStrpMnItm.Name = "Col_Remove_Page_Break_TlStrpMnItm"
        Me.Col_Remove_Page_Break_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Remove_Page_Break_TlStrpMnItm.Text = "Remove Page Break"
        Me.Col_Remove_Page_Break_TlStrpMnItm.Visible = False
        '
        'Col_TlStrpSprtr_05
        '
        Me.Col_TlStrpSprtr_05.Name = "Col_TlStrpSprtr_05"
        Me.Col_TlStrpSprtr_05.Size = New System.Drawing.Size(187, 6)
        Me.Col_TlStrpSprtr_05.Visible = False
        '
        'Col_Properties_TlStrpMnItm
        '
        Me.Col_Properties_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Properties_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Properties_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Properties_TlStrpMnItm.Name = "Col_Properties_TlStrpMnItm"
        Me.Col_Properties_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Properties_TlStrpMnItm.Text = "Properties..."
        Me.Col_Properties_TlStrpMnItm.Visible = False
        '
        'Col_Format_Cells_TlStrpMnItm
        '
        Me.Col_Format_Cells_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Col_Format_Cells_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Col_Format_Cells_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Col_Format_Cells_TlStrpMnItm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.General_Cell_Format_2_TlStrpMnItm, Me.Numeric_Cell_Format_2_TlStrpMnItm, Me.DateTime_Cell_Format_2_TlStrpMnItm, Me.Currency_Cell_Format_2_TlStrpMnItm, Me.Text_Cell_Format_2_TlStrpMnItm})
        Me.Col_Format_Cells_TlStrpMnItm.Image = CType(resources.GetObject("Col_Format_Cells_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Col_Format_Cells_TlStrpMnItm.Name = "Col_Format_Cells_TlStrpMnItm"
        Me.Col_Format_Cells_TlStrpMnItm.Size = New System.Drawing.Size(190, 26)
        Me.Col_Format_Cells_TlStrpMnItm.Text = "Format Cells..."
        '
        'General_Cell_Format_2_TlStrpMnItm
        '
        Me.General_Cell_Format_2_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("General_Cell_Format_2_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.General_Cell_Format_2_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.General_Cell_Format_2_TlStrpMnItm.Name = "General_Cell_Format_2_TlStrpMnItm"
        Me.General_Cell_Format_2_TlStrpMnItm.Size = New System.Drawing.Size(122, 22)
        Me.General_Cell_Format_2_TlStrpMnItm.Text = "Genral"
        '
        'Numeric_Cell_Format_2_TlStrpMnItm
        '
        Me.Numeric_Cell_Format_2_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Numeric_Cell_Format_2_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Numeric_Cell_Format_2_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Numeric_Cell_Format_2_TlStrpMnItm.Name = "Numeric_Cell_Format_2_TlStrpMnItm"
        Me.Numeric_Cell_Format_2_TlStrpMnItm.Size = New System.Drawing.Size(122, 22)
        Me.Numeric_Cell_Format_2_TlStrpMnItm.Text = "Numeric"
        '
        'DateTime_Cell_Format_2_TlStrpMnItm
        '
        Me.DateTime_Cell_Format_2_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("DateTime_Cell_Format_2_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.DateTime_Cell_Format_2_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DateTime_Cell_Format_2_TlStrpMnItm.Name = "DateTime_Cell_Format_2_TlStrpMnItm"
        Me.DateTime_Cell_Format_2_TlStrpMnItm.Size = New System.Drawing.Size(122, 22)
        Me.DateTime_Cell_Format_2_TlStrpMnItm.Text = "DateTime"
        '
        'Currency_Cell_Format_2_TlStrpMnItm
        '
        Me.Currency_Cell_Format_2_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Currency_Cell_Format_2_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Currency_Cell_Format_2_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Currency_Cell_Format_2_TlStrpMnItm.Name = "Currency_Cell_Format_2_TlStrpMnItm"
        Me.Currency_Cell_Format_2_TlStrpMnItm.Size = New System.Drawing.Size(122, 22)
        Me.Currency_Cell_Format_2_TlStrpMnItm.Text = "Currency"
        '
        'Text_Cell_Format_2_TlStrpMnItm
        '
        Me.Text_Cell_Format_2_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Text_Cell_Format_2_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Text_Cell_Format_2_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Text_Cell_Format_2_TlStrpMnItm.Name = "Text_Cell_Format_2_TlStrpMnItm"
        Me.Text_Cell_Format_2_TlStrpMnItm.Size = New System.Drawing.Size(122, 22)
        Me.Text_Cell_Format_2_TlStrpMnItm.Text = "Text"
        '
        'Cell_CntxtMnStrp
        '
        Me.Cell_CntxtMnStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Cell_CntxtMnStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Cell_Cut_Range_TlStrpMnItm, Me.Cell_Copy_Range_TlStrpMnItm, Me.Cell_Paste_Range_TlStrpMnItm, Me.Cell_TlStrpSprtr_01, Me.Cell_Select_All_TlStrpMnItm, Me.Cell_TlStrpSprtr_02, Me.Cell_Merge_Range_TlStrpMnItm, Me.Cell_UnMerge_Range_TlStrpMnItm, Me.Cell_TlStrpSprtr_03, Me.Cell_Change_Type_TlStrpMnItm, Me.Cell_TlStrpSprtr_04, Me.Cell_Format_TlStrpMnItm, Me.Cell_TlStrpSprtr_102, Me.Close_Grid_TlStrpMnItm})
        Me.Cell_CntxtMnStrp.Name = "Cell_CntxtMnStrp"
        Me.Cell_CntxtMnStrp.Size = New System.Drawing.Size(170, 268)
        '
        'Cell_Cut_Range_TlStrpMnItm
        '
        Me.Cell_Cut_Range_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Cut_Range_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Cut_Range_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Cut_Range_TlStrpMnItm.Image = CType(resources.GetObject("Cell_Cut_Range_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Cell_Cut_Range_TlStrpMnItm.Name = "Cell_Cut_Range_TlStrpMnItm"
        Me.Cell_Cut_Range_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Cut_Range_TlStrpMnItm.Text = "Cut"
        '
        'Cell_Copy_Range_TlStrpMnItm
        '
        Me.Cell_Copy_Range_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Copy_Range_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Copy_Range_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Copy_Range_TlStrpMnItm.Image = CType(resources.GetObject("Cell_Copy_Range_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Cell_Copy_Range_TlStrpMnItm.Name = "Cell_Copy_Range_TlStrpMnItm"
        Me.Cell_Copy_Range_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Copy_Range_TlStrpMnItm.Text = "Copy"
        '
        'Cell_Paste_Range_TlStrpMnItm
        '
        Me.Cell_Paste_Range_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Paste_Range_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Paste_Range_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Paste_Range_TlStrpMnItm.Image = CType(resources.GetObject("Cell_Paste_Range_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Cell_Paste_Range_TlStrpMnItm.Name = "Cell_Paste_Range_TlStrpMnItm"
        Me.Cell_Paste_Range_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Paste_Range_TlStrpMnItm.Text = "Paste"
        '
        'Cell_TlStrpSprtr_01
        '
        Me.Cell_TlStrpSprtr_01.Name = "Cell_TlStrpSprtr_01"
        Me.Cell_TlStrpSprtr_01.Size = New System.Drawing.Size(166, 6)
        '
        'Cell_Select_All_TlStrpMnItm
        '
        Me.Cell_Select_All_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Select_All_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Select_All_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Select_All_TlStrpMnItm.Name = "Cell_Select_All_TlStrpMnItm"
        Me.Cell_Select_All_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Select_All_TlStrpMnItm.Text = "Select All"
        Me.Cell_Select_All_TlStrpMnItm.ToolTipText = "select All"
        '
        'Cell_TlStrpSprtr_02
        '
        Me.Cell_TlStrpSprtr_02.Name = "Cell_TlStrpSprtr_02"
        Me.Cell_TlStrpSprtr_02.Size = New System.Drawing.Size(166, 6)
        Me.Cell_TlStrpSprtr_02.Visible = False
        '
        'Cell_Merge_Range_TlStrpMnItm
        '
        Me.Cell_Merge_Range_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Merge_Range_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Merge_Range_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Merge_Range_TlStrpMnItm.Image = CType(resources.GetObject("Cell_Merge_Range_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Cell_Merge_Range_TlStrpMnItm.Name = "Cell_Merge_Range_TlStrpMnItm"
        Me.Cell_Merge_Range_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Merge_Range_TlStrpMnItm.Text = "Merge"
        '
        'Cell_UnMerge_Range_TlStrpMnItm
        '
        Me.Cell_UnMerge_Range_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_UnMerge_Range_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_UnMerge_Range_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_UnMerge_Range_TlStrpMnItm.Image = CType(resources.GetObject("Cell_UnMerge_Range_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Cell_UnMerge_Range_TlStrpMnItm.Name = "Cell_UnMerge_Range_TlStrpMnItm"
        Me.Cell_UnMerge_Range_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_UnMerge_Range_TlStrpMnItm.Text = "Unmerge"
        '
        'Cell_TlStrpSprtr_03
        '
        Me.Cell_TlStrpSprtr_03.Name = "Cell_TlStrpSprtr_03"
        Me.Cell_TlStrpSprtr_03.Size = New System.Drawing.Size(166, 6)
        Me.Cell_TlStrpSprtr_03.Visible = False
        '
        'Cell_Change_Type_TlStrpMnItm
        '
        Me.Cell_Change_Type_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Change_Type_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Change_Type_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Change_Type_TlStrpMnItm.Name = "Cell_Change_Type_TlStrpMnItm"
        Me.Cell_Change_Type_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Change_Type_TlStrpMnItm.Text = "Change Cell Type"
        Me.Cell_Change_Type_TlStrpMnItm.Visible = False
        '
        'Cell_TlStrpSprtr_04
        '
        Me.Cell_TlStrpSprtr_04.Name = "Cell_TlStrpSprtr_04"
        Me.Cell_TlStrpSprtr_04.Size = New System.Drawing.Size(166, 6)
        Me.Cell_TlStrpSprtr_04.Visible = False
        '
        'Cell_Format_TlStrpMnItm
        '
        Me.Cell_Format_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Cell_Format_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Cell_Format_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cell_Format_TlStrpMnItm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.General_Cell_Format_TlStrpMnItm, Me.Numeric_Cell_Format_TlStrpMnItm, Me.DateTime_Cell_Format_TlStrpMnItm, Me.Currency_Cell_Format_TlStrpMnItm, Me.Text_Cell_Format_TlStrpMnItm})
        Me.Cell_Format_TlStrpMnItm.Image = CType(resources.GetObject("Cell_Format_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Cell_Format_TlStrpMnItm.Name = "Cell_Format_TlStrpMnItm"
        Me.Cell_Format_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Cell_Format_TlStrpMnItm.Text = "&Format Cell..."
        '
        'General_Cell_Format_TlStrpMnItm
        '
        Me.General_Cell_Format_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("General_Cell_Format_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.General_Cell_Format_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.General_Cell_Format_TlStrpMnItm.Name = "General_Cell_Format_TlStrpMnItm"
        Me.General_Cell_Format_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.General_Cell_Format_TlStrpMnItm.Text = "General"
        '
        'Numeric_Cell_Format_TlStrpMnItm
        '
        Me.Numeric_Cell_Format_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Numeric_Cell_Format_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Numeric_Cell_Format_TlStrpMnItm.Name = "Numeric_Cell_Format_TlStrpMnItm"
        Me.Numeric_Cell_Format_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.Numeric_Cell_Format_TlStrpMnItm.Text = "Numeric"
        '
        'DateTime_Cell_Format_TlStrpMnItm
        '
        Me.DateTime_Cell_Format_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("DateTime_Cell_Format_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.DateTime_Cell_Format_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.DateTime_Cell_Format_TlStrpMnItm.Name = "DateTime_Cell_Format_TlStrpMnItm"
        Me.DateTime_Cell_Format_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.DateTime_Cell_Format_TlStrpMnItm.Text = "DateTime"
        '
        'Currency_Cell_Format_TlStrpMnItm
        '
        Me.Currency_Cell_Format_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Currency_Cell_Format_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Currency_Cell_Format_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Currency_Cell_Format_TlStrpMnItm.Name = "Currency_Cell_Format_TlStrpMnItm"
        Me.Currency_Cell_Format_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.Currency_Cell_Format_TlStrpMnItm.Text = "Currency"
        '
        'Text_Cell_Format_TlStrpMnItm
        '
        Me.Text_Cell_Format_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Text_Cell_Format_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Text_Cell_Format_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Text_Cell_Format_TlStrpMnItm.Name = "Text_Cell_Format_TlStrpMnItm"
        Me.Text_Cell_Format_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.Text_Cell_Format_TlStrpMnItm.Text = "Text"
        '
        'Cell_TlStrpSprtr_102
        '
        Me.Cell_TlStrpSprtr_102.Name = "Cell_TlStrpSprtr_102"
        Me.Cell_TlStrpSprtr_102.Size = New System.Drawing.Size(166, 6)
        '
        'Close_Grid_TlStrpMnItm
        '
        Me.Close_Grid_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Close_Grid_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Close_Grid_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Close_Grid_TlStrpMnItm.Image = CType(resources.GetObject("Close_Grid_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Close_Grid_TlStrpMnItm.Name = "Close_Grid_TlStrpMnItm"
        Me.Close_Grid_TlStrpMnItm.Size = New System.Drawing.Size(169, 26)
        Me.Close_Grid_TlStrpMnItm.Text = "Close Grid"
        '
        'Row_CntxtMnStrp
        '
        Me.Row_CntxtMnStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Row_CntxtMnStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Row_Cut_TlStrpMnItm, Me.Row_Copy_TlStrpMnItm, Me.Row_Paste_TlStrpMnItm, Me.Row_TlStrpSprtr_01, Me.Row_Insert_TlStrpMnItm, Me.Row_Delete_TlStrpMnItm, Me.Row_TlStrpSprtr_02, Me.Row_Reset_To_Default_Height_TlStrpMnItm, Me.Row_Height_TlStrpMnItm, Me.Row_Hide_TlStrpMnItm, Me.Row_UnHide_TlStrpMnItm, Me.Row_TlStrpSprtr_03, Me.Row_Group_TlStrpMnItm, Me.Row_UnGroup_TlStrpMnItm, Me.Row_UnGroup_All_TlStrpMnItm, Me.Row_TlStrpSprtr_04, Me.Row_Insert_Page_Break_TlStrpMnItm, Me.Row_Remove_Page_Break_TlStrpMnItm, Me.Row_TlStrpSprtr_05, Me.Row_Properties_TlStrpMnItm, Me.Row_Format_Cells_TlStrpMnItm})
        Me.Row_CntxtMnStrp.Name = "Row_CntxtMnStrp"
        Me.Row_CntxtMnStrp.Size = New System.Drawing.Size(201, 450)
        '
        'Row_Cut_TlStrpMnItm
        '
        Me.Row_Cut_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Cut_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Cut_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Cut_TlStrpMnItm.Enabled = False
        Me.Row_Cut_TlStrpMnItm.Image = CType(resources.GetObject("Row_Cut_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Row_Cut_TlStrpMnItm.Name = "Row_Cut_TlStrpMnItm"
        Me.Row_Cut_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Cut_TlStrpMnItm.Text = "Cut"
        '
        'Row_Copy_TlStrpMnItm
        '
        Me.Row_Copy_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Copy_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Copy_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Copy_TlStrpMnItm.Enabled = False
        Me.Row_Copy_TlStrpMnItm.Image = CType(resources.GetObject("Row_Copy_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Row_Copy_TlStrpMnItm.Name = "Row_Copy_TlStrpMnItm"
        Me.Row_Copy_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Copy_TlStrpMnItm.Text = "Copy"
        '
        'Row_Paste_TlStrpMnItm
        '
        Me.Row_Paste_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Paste_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Paste_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Paste_TlStrpMnItm.Enabled = False
        Me.Row_Paste_TlStrpMnItm.Image = CType(resources.GetObject("Row_Paste_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Row_Paste_TlStrpMnItm.Name = "Row_Paste_TlStrpMnItm"
        Me.Row_Paste_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Paste_TlStrpMnItm.Text = "Paste"
        '
        'Row_TlStrpSprtr_01
        '
        Me.Row_TlStrpSprtr_01.Name = "Row_TlStrpSprtr_01"
        Me.Row_TlStrpSprtr_01.Size = New System.Drawing.Size(197, 6)
        Me.Row_TlStrpSprtr_01.Visible = False
        '
        'Row_Insert_TlStrpMnItm
        '
        Me.Row_Insert_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Insert_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Insert_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Insert_TlStrpMnItm.Name = "Row_Insert_TlStrpMnItm"
        Me.Row_Insert_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Insert_TlStrpMnItm.Text = "Insert &Rows"
        '
        'Row_Delete_TlStrpMnItm
        '
        Me.Row_Delete_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Delete_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Delete_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Delete_TlStrpMnItm.Name = "Row_Delete_TlStrpMnItm"
        Me.Row_Delete_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Delete_TlStrpMnItm.Text = "&Delete Rows"
        '
        'Row_TlStrpSprtr_02
        '
        Me.Row_TlStrpSprtr_02.Name = "Row_TlStrpSprtr_02"
        Me.Row_TlStrpSprtr_02.Size = New System.Drawing.Size(197, 6)
        Me.Row_TlStrpSprtr_02.Visible = False
        '
        'Row_Reset_To_Default_Height_TlStrpMnItm
        '
        Me.Row_Reset_To_Default_Height_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Reset_To_Default_Height_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Reset_To_Default_Height_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Reset_To_Default_Height_TlStrpMnItm.Name = "Row_Reset_To_Default_Height_TlStrpMnItm"
        Me.Row_Reset_To_Default_Height_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Reset_To_Default_Height_TlStrpMnItm.Text = "Reset to Default Height"
        '
        'Row_Height_TlStrpMnItm
        '
        Me.Row_Height_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Height_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Height_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Height_TlStrpMnItm.Name = "Row_Height_TlStrpMnItm"
        Me.Row_Height_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Height_TlStrpMnItm.Text = "Row &Height..."
        '
        'Row_Hide_TlStrpMnItm
        '
        Me.Row_Hide_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Hide_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Hide_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Hide_TlStrpMnItm.Name = "Row_Hide_TlStrpMnItm"
        Me.Row_Hide_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Hide_TlStrpMnItm.Text = "&Hide"
        '
        'Row_UnHide_TlStrpMnItm
        '
        Me.Row_UnHide_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_UnHide_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_UnHide_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_UnHide_TlStrpMnItm.Name = "Row_UnHide_TlStrpMnItm"
        Me.Row_UnHide_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_UnHide_TlStrpMnItm.Text = "&Unhide"
        '
        'Row_TlStrpSprtr_03
        '
        Me.Row_TlStrpSprtr_03.Name = "Row_TlStrpSprtr_03"
        Me.Row_TlStrpSprtr_03.Size = New System.Drawing.Size(197, 6)
        Me.Row_TlStrpSprtr_03.Visible = False
        '
        'Row_Group_TlStrpMnItm
        '
        Me.Row_Group_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Group_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Group_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Group_TlStrpMnItm.Name = "Row_Group_TlStrpMnItm"
        Me.Row_Group_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Group_TlStrpMnItm.Text = "&Group"
        Me.Row_Group_TlStrpMnItm.Visible = False
        '
        'Row_UnGroup_TlStrpMnItm
        '
        Me.Row_UnGroup_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_UnGroup_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_UnGroup_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_UnGroup_TlStrpMnItm.Name = "Row_UnGroup_TlStrpMnItm"
        Me.Row_UnGroup_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_UnGroup_TlStrpMnItm.Text = "Ungroup"
        Me.Row_UnGroup_TlStrpMnItm.Visible = False
        '
        'Row_UnGroup_All_TlStrpMnItm
        '
        Me.Row_UnGroup_All_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_UnGroup_All_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_UnGroup_All_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_UnGroup_All_TlStrpMnItm.Name = "Row_UnGroup_All_TlStrpMnItm"
        Me.Row_UnGroup_All_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_UnGroup_All_TlStrpMnItm.Text = "Ungroup All"
        Me.Row_UnGroup_All_TlStrpMnItm.Visible = False
        '
        'Row_TlStrpSprtr_04
        '
        Me.Row_TlStrpSprtr_04.Name = "Row_TlStrpSprtr_04"
        Me.Row_TlStrpSprtr_04.Size = New System.Drawing.Size(197, 6)
        Me.Row_TlStrpSprtr_04.Visible = False
        '
        'Row_Insert_Page_Break_TlStrpMnItm
        '
        Me.Row_Insert_Page_Break_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Insert_Page_Break_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Insert_Page_Break_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Insert_Page_Break_TlStrpMnItm.Name = "Row_Insert_Page_Break_TlStrpMnItm"
        Me.Row_Insert_Page_Break_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Insert_Page_Break_TlStrpMnItm.Text = "Insert Page Break"
        Me.Row_Insert_Page_Break_TlStrpMnItm.Visible = False
        '
        'Row_Remove_Page_Break_TlStrpMnItm
        '
        Me.Row_Remove_Page_Break_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Remove_Page_Break_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Remove_Page_Break_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Remove_Page_Break_TlStrpMnItm.Name = "Row_Remove_Page_Break_TlStrpMnItm"
        Me.Row_Remove_Page_Break_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Remove_Page_Break_TlStrpMnItm.Text = "Remove Page Break"
        Me.Row_Remove_Page_Break_TlStrpMnItm.Visible = False
        '
        'Row_TlStrpSprtr_05
        '
        Me.Row_TlStrpSprtr_05.Name = "Row_TlStrpSprtr_05"
        Me.Row_TlStrpSprtr_05.Size = New System.Drawing.Size(197, 6)
        Me.Row_TlStrpSprtr_05.Visible = False
        '
        'Row_Properties_TlStrpMnItm
        '
        Me.Row_Properties_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Properties_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Properties_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Properties_TlStrpMnItm.Name = "Row_Properties_TlStrpMnItm"
        Me.Row_Properties_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Properties_TlStrpMnItm.Text = "Properties..."
        Me.Row_Properties_TlStrpMnItm.Visible = False
        '
        'Row_Format_Cells_TlStrpMnItm
        '
        Me.Row_Format_Cells_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Row_Format_Cells_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Row_Format_Cells_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Row_Format_Cells_TlStrpMnItm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.General_Cell_Format_1_TlStrpMnItm, Me.Numeric_Cell_Format_1_TlStrpMnItm, Me.DateTime_Cell_Format_1_TlStrpMnItm, Me.Currency_Cell_Format_1_TlStrpMnItm, Me.Text_Cell_Format_1_TlStrpMnItm})
        Me.Row_Format_Cells_TlStrpMnItm.Image = CType(resources.GetObject("Row_Format_Cells_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Row_Format_Cells_TlStrpMnItm.Name = "Row_Format_Cells_TlStrpMnItm"
        Me.Row_Format_Cells_TlStrpMnItm.Size = New System.Drawing.Size(200, 26)
        Me.Row_Format_Cells_TlStrpMnItm.Text = "Format Cells..."
        '
        'General_Cell_Format_1_TlStrpMnItm
        '
        Me.General_Cell_Format_1_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("General_Cell_Format_1_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.General_Cell_Format_1_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.General_Cell_Format_1_TlStrpMnItm.Name = "General_Cell_Format_1_TlStrpMnItm"
        Me.General_Cell_Format_1_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.General_Cell_Format_1_TlStrpMnItm.Text = "General"
        '
        'Numeric_Cell_Format_1_TlStrpMnItm
        '
        Me.Numeric_Cell_Format_1_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Numeric_Cell_Format_1_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Numeric_Cell_Format_1_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Numeric_Cell_Format_1_TlStrpMnItm.Name = "Numeric_Cell_Format_1_TlStrpMnItm"
        Me.Numeric_Cell_Format_1_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.Numeric_Cell_Format_1_TlStrpMnItm.Text = "Numeric"
        '
        'DateTime_Cell_Format_1_TlStrpMnItm
        '
        Me.DateTime_Cell_Format_1_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("DateTime_Cell_Format_1_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.DateTime_Cell_Format_1_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DateTime_Cell_Format_1_TlStrpMnItm.Name = "DateTime_Cell_Format_1_TlStrpMnItm"
        Me.DateTime_Cell_Format_1_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.DateTime_Cell_Format_1_TlStrpMnItm.Text = "DateTime"
        '
        'Currency_Cell_Format_1_TlStrpMnItm
        '
        Me.Currency_Cell_Format_1_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Currency_Cell_Format_1_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Currency_Cell_Format_1_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Currency_Cell_Format_1_TlStrpMnItm.Name = "Currency_Cell_Format_1_TlStrpMnItm"
        Me.Currency_Cell_Format_1_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.Currency_Cell_Format_1_TlStrpMnItm.Text = "Currency"
        '
        'Text_Cell_Format_1_TlStrpMnItm
        '
        Me.Text_Cell_Format_1_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Text_Cell_Format_1_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Text_Cell_Format_1_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Text_Cell_Format_1_TlStrpMnItm.Name = "Text_Cell_Format_1_TlStrpMnItm"
        Me.Text_Cell_Format_1_TlStrpMnItm.Size = New System.Drawing.Size(124, 22)
        Me.Text_Cell_Format_1_TlStrpMnItm.Text = "Text"
        '
        'Backup_Timer
        '
        '
        'Stiky_TlStrp
        '
        Me.Stiky_TlStrp.AutoSize = False
        Me.Stiky_TlStrp.BackgroundImage = CType(resources.GetObject("Stiky_TlStrp.BackgroundImage"), System.Drawing.Image)
        Me.Stiky_TlStrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Stiky_TlStrp.Dock = System.Windows.Forms.DockStyle.Left
        Me.Stiky_TlStrp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.Stiky_TlStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.Stiky_TlStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Compress_Me_TlStrpBtn, Me.New_Sticky_TlStrpBtn, Me.Open_Sticky_TlStrpBtn, Me.Save_Sticky_TlStrpBtn, Me.Delete_TlStrpBtn, Me.Print_Sticky_TlStrpBtn, Me.Stiky_tlStrpSprtr_01, Me.Font_Name_Sticky_TlStrpSpltBtn, Me.Text_Color_Sticky_TlStrpSpltBtn, Me.Bold_Stiky_TlStrpBtn, Me.Underline_Stiky_TlStrpBtn, Me.WordWrap_TlStrp, Me.Font_Strikeout_TlStrpBtn, Me.Stiky_tlStrpSprtr_02, Me.Read_ME_TlStrpBtn, Me.Stiky_tlStrpSprtr_03, Me.Left_Stiky_TlStrpBtn, Me.Center_Stiky_TlStrpBtn, Me.Right_Stiky_TlStrpBtn, Me.Stiky_tlStrpSprtr_04, Me.Bullets_Stiky_TlStrpBtn, Me.Stiky_tlStrpSprtr_05, Me.Find_TlStrpBtn, Me.Cut_Stiky_TlStrpBtn, Me.Copy_Stiky_TlStrpBtn, Me.Paste_Stiky_TlStrpBtn, Me.Stiky_tlStrpSprtr_06, Me.Show_Hide_TlStrpSpltBtn, Me.Form_Size_TlStrpBtn, Me.Exit_TlStrpBtn})
        Me.Stiky_TlStrp.Location = New System.Drawing.Point(0, 100)
        Me.Stiky_TlStrp.Name = "Stiky_TlStrp"
        Me.Stiky_TlStrp.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.Stiky_TlStrp.Size = New System.Drawing.Size(34, 661)
        Me.Stiky_TlStrp.TabIndex = 1057
        Me.Stiky_TlStrp.Text = "Stiky_TlStrp"
        '
        'Compress_Me_TlStrpBtn
        '
        Me.Compress_Me_TlStrpBtn.BackColor = System.Drawing.Color.Transparent
        Me.Compress_Me_TlStrpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Compress_Me_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Compress_Me_TlStrpBtn.Image = CType(resources.GetObject("Compress_Me_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Compress_Me_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Compress_Me_TlStrpBtn.Name = "Compress_Me_TlStrpBtn"
        Me.Compress_Me_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Compress_Me_TlStrpBtn.Text = "Compress Me"
        '
        'New_Sticky_TlStrpBtn
        '
        Me.New_Sticky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.New_Sticky_TlStrpBtn.Image = CType(resources.GetObject("New_Sticky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.New_Sticky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.New_Sticky_TlStrpBtn.Name = "New_Sticky_TlStrpBtn"
        Me.New_Sticky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.New_Sticky_TlStrpBtn.Text = "&New"
        '
        'Open_Sticky_TlStrpBtn
        '
        Me.Open_Sticky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Open_Sticky_TlStrpBtn.Image = CType(resources.GetObject("Open_Sticky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Open_Sticky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Open_Sticky_TlStrpBtn.Name = "Open_Sticky_TlStrpBtn"
        Me.Open_Sticky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Open_Sticky_TlStrpBtn.Text = "&Open"
        '
        'Save_Sticky_TlStrpBtn
        '
        Me.Save_Sticky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Save_Sticky_TlStrpBtn.Image = CType(resources.GetObject("Save_Sticky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Save_Sticky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Save_Sticky_TlStrpBtn.Name = "Save_Sticky_TlStrpBtn"
        Me.Save_Sticky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Save_Sticky_TlStrpBtn.Text = "&Save"
        '
        'Delete_TlStrpBtn
        '
        Me.Delete_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Delete_TlStrpBtn.Image = CType(resources.GetObject("Delete_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Delete_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Delete_TlStrpBtn.Name = "Delete_TlStrpBtn"
        Me.Delete_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Delete_TlStrpBtn.Text = "Delete/إلغاء"
        '
        'Print_Sticky_TlStrpBtn
        '
        Me.Print_Sticky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Print_Sticky_TlStrpBtn.Image = CType(resources.GetObject("Print_Sticky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Print_Sticky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Print_Sticky_TlStrpBtn.Name = "Print_Sticky_TlStrpBtn"
        Me.Print_Sticky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Print_Sticky_TlStrpBtn.Text = "&Print"
        '
        'Stiky_tlStrpSprtr_01
        '
        Me.Stiky_tlStrpSprtr_01.Name = "Stiky_tlStrpSprtr_01"
        Me.Stiky_tlStrpSprtr_01.Size = New System.Drawing.Size(31, 6)
        '
        'Font_Name_Sticky_TlStrpSpltBtn
        '
        Me.Font_Name_Sticky_TlStrpSpltBtn.AutoSize = False
        Me.Font_Name_Sticky_TlStrpSpltBtn.BackColor = System.Drawing.Color.Transparent
        Me.Font_Name_Sticky_TlStrpSpltBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Font_Name_Sticky_TlStrpSpltBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Font_Name_Sticky_TlStrpSpltBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Select_Font_Sticky_TlStrpMnItm})
        Me.Font_Name_Sticky_TlStrpSpltBtn.Image = CType(resources.GetObject("Font_Name_Sticky_TlStrpSpltBtn.Image"), System.Drawing.Image)
        Me.Font_Name_Sticky_TlStrpSpltBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Font_Name_Sticky_TlStrpSpltBtn.Name = "Font_Name_Sticky_TlStrpSpltBtn"
        Me.Font_Name_Sticky_TlStrpSpltBtn.Size = New System.Drawing.Size(28, 28)
        Me.Font_Name_Sticky_TlStrpSpltBtn.Text = "ToolStripSplitButton2"
        Me.Font_Name_Sticky_TlStrpSpltBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Select_Font_Sticky_TlStrpMnItm
        '
        Me.Select_Font_Sticky_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Select_Font_Sticky_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Select_Font_Sticky_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Select_Font_Sticky_TlStrpMnItm.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Select_Font_Sticky_TlStrpMnItm.ForeColor = System.Drawing.Color.White
        Me.Select_Font_Sticky_TlStrpMnItm.Image = CType(resources.GetObject("Select_Font_Sticky_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Select_Font_Sticky_TlStrpMnItm.Name = "Select_Font_Sticky_TlStrpMnItm"
        Me.Select_Font_Sticky_TlStrpMnItm.Size = New System.Drawing.Size(139, 26)
        Me.Select_Font_Sticky_TlStrpMnItm.Text = "Select Font"
        '
        'Text_Color_Sticky_TlStrpSpltBtn
        '
        Me.Text_Color_Sticky_TlStrpSpltBtn.AutoSize = False
        Me.Text_Color_Sticky_TlStrpSpltBtn.BackColor = System.Drawing.Color.Transparent
        Me.Text_Color_Sticky_TlStrpSpltBtn.BackgroundImage = CType(resources.GetObject("Text_Color_Sticky_TlStrpSpltBtn.BackgroundImage"), System.Drawing.Image)
        Me.Text_Color_Sticky_TlStrpSpltBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Text_Color_Sticky_TlStrpSpltBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Text_Color_Sticky_TlStrpSpltBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Selected_Text_Color_TlStrpMnItm, Me.Remove_Selected_Text_Color_TlStrpMnItm, Me.Selected_Text_Backcolor_TlStrpMnItm, Me.Remove_Selected_Text_Backcolor_TlStrpMnItm})
        Me.Text_Color_Sticky_TlStrpSpltBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text_Color_Sticky_TlStrpSpltBtn.Image = CType(resources.GetObject("Text_Color_Sticky_TlStrpSpltBtn.Image"), System.Drawing.Image)
        Me.Text_Color_Sticky_TlStrpSpltBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Text_Color_Sticky_TlStrpSpltBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.Text_Color_Sticky_TlStrpSpltBtn.Name = "Text_Color_Sticky_TlStrpSpltBtn"
        Me.Text_Color_Sticky_TlStrpSpltBtn.Size = New System.Drawing.Size(28, 20)
        Me.Text_Color_Sticky_TlStrpSpltBtn.Text = "ToolStripSplitButton1"
        Me.Text_Color_Sticky_TlStrpSpltBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Selected_Text_Color_TlStrpMnItm
        '
        Me.Selected_Text_Color_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Selected_Text_Color_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Selected_Text_Color_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Selected_Text_Color_TlStrpMnItm.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Selected_Text_Color_TlStrpMnItm.ForeColor = System.Drawing.Color.White
        Me.Selected_Text_Color_TlStrpMnItm.Image = CType(resources.GetObject("Selected_Text_Color_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Selected_Text_Color_TlStrpMnItm.Name = "Selected_Text_Color_TlStrpMnItm"
        Me.Selected_Text_Color_TlStrpMnItm.Size = New System.Drawing.Size(259, 22)
        Me.Selected_Text_Color_TlStrpMnItm.Text = "Add Selected Text Color"
        '
        'Remove_Selected_Text_Color_TlStrpMnItm
        '
        Me.Remove_Selected_Text_Color_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Remove_Selected_Text_Color_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Remove_Selected_Text_Color_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Remove_Selected_Text_Color_TlStrpMnItm.Image = CType(resources.GetObject("Remove_Selected_Text_Color_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Remove_Selected_Text_Color_TlStrpMnItm.Name = "Remove_Selected_Text_Color_TlStrpMnItm"
        Me.Remove_Selected_Text_Color_TlStrpMnItm.Size = New System.Drawing.Size(259, 22)
        Me.Remove_Selected_Text_Color_TlStrpMnItm.Text = "Remove Selected Text Color"
        '
        'Selected_Text_Backcolor_TlStrpMnItm
        '
        Me.Selected_Text_Backcolor_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Selected_Text_Backcolor_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Selected_Text_Backcolor_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Selected_Text_Backcolor_TlStrpMnItm.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Selected_Text_Backcolor_TlStrpMnItm.ForeColor = System.Drawing.SystemColors.Window
        Me.Selected_Text_Backcolor_TlStrpMnItm.Image = CType(resources.GetObject("Selected_Text_Backcolor_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Selected_Text_Backcolor_TlStrpMnItm.Name = "Selected_Text_Backcolor_TlStrpMnItm"
        Me.Selected_Text_Backcolor_TlStrpMnItm.Size = New System.Drawing.Size(259, 22)
        Me.Selected_Text_Backcolor_TlStrpMnItm.Text = "Add Selected Text Backcolor "
        '
        'Remove_Selected_Text_Backcolor_TlStrpMnItm
        '
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Remove_Selected_Text_Backcolor_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm.Image = CType(resources.GetObject("Remove_Selected_Text_Backcolor_TlStrpMnItm.Image"), System.Drawing.Image)
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm.Name = "Remove_Selected_Text_Backcolor_TlStrpMnItm"
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm.Size = New System.Drawing.Size(259, 22)
        Me.Remove_Selected_Text_Backcolor_TlStrpMnItm.Text = "Remove Selected Text Backcolor "
        '
        'Bold_Stiky_TlStrpBtn
        '
        Me.Bold_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Bold_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Bold_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Bold_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Bold_Stiky_TlStrpBtn.Name = "Bold_Stiky_TlStrpBtn"
        Me.Bold_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Bold_Stiky_TlStrpBtn.Text = "Bold"
        '
        'Underline_Stiky_TlStrpBtn
        '
        Me.Underline_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Underline_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Underline_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Underline_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Underline_Stiky_TlStrpBtn.Name = "Underline_Stiky_TlStrpBtn"
        Me.Underline_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Underline_Stiky_TlStrpBtn.Text = "Underline"
        '
        'WordWrap_TlStrp
        '
        Me.WordWrap_TlStrp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.WordWrap_TlStrp.Image = CType(resources.GetObject("WordWrap_TlStrp.Image"), System.Drawing.Image)
        Me.WordWrap_TlStrp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.WordWrap_TlStrp.Name = "WordWrap_TlStrp"
        Me.WordWrap_TlStrp.Size = New System.Drawing.Size(31, 24)
        Me.WordWrap_TlStrp.Text = "WordWrap"
        Me.WordWrap_TlStrp.ToolTipText = "WordWrap"
        '
        'Font_Strikeout_TlStrpBtn
        '
        Me.Font_Strikeout_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Font_Strikeout_TlStrpBtn.Image = CType(resources.GetObject("Font_Strikeout_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Font_Strikeout_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Font_Strikeout_TlStrpBtn.Name = "Font_Strikeout_TlStrpBtn"
        Me.Font_Strikeout_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Font_Strikeout_TlStrpBtn.Text = "Font Strikeout"
        '
        'Stiky_tlStrpSprtr_02
        '
        Me.Stiky_tlStrpSprtr_02.Name = "Stiky_tlStrpSprtr_02"
        Me.Stiky_tlStrpSprtr_02.Size = New System.Drawing.Size(31, 6)
        '
        'Read_ME_TlStrpBtn
        '
        Me.Read_ME_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Read_ME_TlStrpBtn.Image = CType(resources.GetObject("Read_ME_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Read_ME_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Read_ME_TlStrpBtn.Name = "Read_ME_TlStrpBtn"
        Me.Read_ME_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Read_ME_TlStrpBtn.Text = "Read ME إقرأني"
        '
        'Stiky_tlStrpSprtr_03
        '
        Me.Stiky_tlStrpSprtr_03.Name = "Stiky_tlStrpSprtr_03"
        Me.Stiky_tlStrpSprtr_03.Size = New System.Drawing.Size(31, 6)
        '
        'Left_Stiky_TlStrpBtn
        '
        Me.Left_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Left_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Left_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Left_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Left_Stiky_TlStrpBtn.Name = "Left_Stiky_TlStrpBtn"
        Me.Left_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Left_Stiky_TlStrpBtn.Text = "Left"
        '
        'Center_Stiky_TlStrpBtn
        '
        Me.Center_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Center_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Center_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Center_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Center_Stiky_TlStrpBtn.Name = "Center_Stiky_TlStrpBtn"
        Me.Center_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Center_Stiky_TlStrpBtn.Text = "Center"
        '
        'Right_Stiky_TlStrpBtn
        '
        Me.Right_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Right_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Right_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Right_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Right_Stiky_TlStrpBtn.Name = "Right_Stiky_TlStrpBtn"
        Me.Right_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Right_Stiky_TlStrpBtn.Text = "Right"
        '
        'Stiky_tlStrpSprtr_04
        '
        Me.Stiky_tlStrpSprtr_04.Name = "Stiky_tlStrpSprtr_04"
        Me.Stiky_tlStrpSprtr_04.Size = New System.Drawing.Size(31, 6)
        '
        'Bullets_Stiky_TlStrpBtn
        '
        Me.Bullets_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Bullets_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Bullets_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Bullets_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Bullets_Stiky_TlStrpBtn.Name = "Bullets_Stiky_TlStrpBtn"
        Me.Bullets_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Bullets_Stiky_TlStrpBtn.Text = "Bullets"
        '
        'Stiky_tlStrpSprtr_05
        '
        Me.Stiky_tlStrpSprtr_05.Name = "Stiky_tlStrpSprtr_05"
        Me.Stiky_tlStrpSprtr_05.Size = New System.Drawing.Size(31, 6)
        '
        'Find_TlStrpBtn
        '
        Me.Find_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Find_TlStrpBtn.Image = CType(resources.GetObject("Find_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Find_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Find_TlStrpBtn.Name = "Find_TlStrpBtn"
        Me.Find_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        '
        'Cut_Stiky_TlStrpBtn
        '
        Me.Cut_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Cut_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Cut_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Cut_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Cut_Stiky_TlStrpBtn.Name = "Cut_Stiky_TlStrpBtn"
        Me.Cut_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Cut_Stiky_TlStrpBtn.Text = "C&ut"
        '
        'Copy_Stiky_TlStrpBtn
        '
        Me.Copy_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Copy_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Copy_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Copy_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Copy_Stiky_TlStrpBtn.Name = "Copy_Stiky_TlStrpBtn"
        Me.Copy_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Copy_Stiky_TlStrpBtn.Text = "&Copy"
        '
        'Paste_Stiky_TlStrpBtn
        '
        Me.Paste_Stiky_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Paste_Stiky_TlStrpBtn.Image = CType(resources.GetObject("Paste_Stiky_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Paste_Stiky_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Paste_Stiky_TlStrpBtn.Name = "Paste_Stiky_TlStrpBtn"
        Me.Paste_Stiky_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Paste_Stiky_TlStrpBtn.Text = "&Paste"
        '
        'Stiky_tlStrpSprtr_06
        '
        Me.Stiky_tlStrpSprtr_06.Name = "Stiky_tlStrpSprtr_06"
        Me.Stiky_tlStrpSprtr_06.Size = New System.Drawing.Size(31, 6)
        '
        'Show_Hide_TlStrpSpltBtn
        '
        Me.Show_Hide_TlStrpSpltBtn.AutoSize = False
        Me.Show_Hide_TlStrpSpltBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Show_Hide_TlStrpSpltBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Show_Hide_TlStrpSpltBtn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm, Me.Show_Hide_Sticky_Note_TlStrpMnItm, Me.Show_Hide_Setting_Tab_TlStrpMnItm, Me.Show_Hide_Sticky_Grid_TlStrpMnItm})
        Me.Show_Hide_TlStrpSpltBtn.Image = CType(resources.GetObject("Show_Hide_TlStrpSpltBtn.Image"), System.Drawing.Image)
        Me.Show_Hide_TlStrpSpltBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Show_Hide_TlStrpSpltBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Show_Hide_TlStrpSpltBtn.Name = "Show_Hide_TlStrpSpltBtn"
        Me.Show_Hide_TlStrpSpltBtn.Size = New System.Drawing.Size(27, 20)
        Me.Show_Hide_TlStrpSpltBtn.Text = "Show_Hide_TlStrpSpltBtn"
        Me.Show_Hide_TlStrpSpltBtn.ToolTipText = "Show Or Hide Controls"
        '
        'Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm
        '
        Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm.Name = "Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm"
        Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm.Size = New System.Drawing.Size(268, 22)
        Me.Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm.Text = "Show Hide Stickies Notes TabControl"
        '
        'Show_Hide_Sticky_Note_TlStrpMnItm
        '
        Me.Show_Hide_Sticky_Note_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Show_Hide_Sticky_Note_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Show_Hide_Sticky_Note_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Hide_Sticky_Note_TlStrpMnItm.Name = "Show_Hide_Sticky_Note_TlStrpMnItm"
        Me.Show_Hide_Sticky_Note_TlStrpMnItm.Size = New System.Drawing.Size(268, 22)
        Me.Show_Hide_Sticky_Note_TlStrpMnItm.Text = "Show Hide Sticky Note"
        '
        'Show_Hide_Setting_Tab_TlStrpMnItm
        '
        Me.Show_Hide_Setting_Tab_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Show_Hide_Setting_Tab_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Show_Hide_Setting_Tab_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Hide_Setting_Tab_TlStrpMnItm.Name = "Show_Hide_Setting_Tab_TlStrpMnItm"
        Me.Show_Hide_Setting_Tab_TlStrpMnItm.Size = New System.Drawing.Size(268, 22)
        Me.Show_Hide_Setting_Tab_TlStrpMnItm.Text = "Show Hide Setting Tab"
        '
        'Show_Hide_Sticky_Grid_TlStrpMnItm
        '
        Me.Show_Hide_Sticky_Grid_TlStrpMnItm.BackgroundImage = CType(resources.GetObject("Show_Hide_Sticky_Grid_TlStrpMnItm.BackgroundImage"), System.Drawing.Image)
        Me.Show_Hide_Sticky_Grid_TlStrpMnItm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_Hide_Sticky_Grid_TlStrpMnItm.Name = "Show_Hide_Sticky_Grid_TlStrpMnItm"
        Me.Show_Hide_Sticky_Grid_TlStrpMnItm.Size = New System.Drawing.Size(268, 22)
        Me.Show_Hide_Sticky_Grid_TlStrpMnItm.Text = "Show Hide Sticky Grid"
        '
        'Form_Size_TlStrpBtn
        '
        Me.Form_Size_TlStrpBtn.AutoSize = False
        Me.Form_Size_TlStrpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Form_Size_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Form_Size_TlStrpBtn.Image = CType(resources.GetObject("Form_Size_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Form_Size_TlStrpBtn.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Form_Size_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Form_Size_TlStrpBtn.Name = "Form_Size_TlStrpBtn"
        Me.Form_Size_TlStrpBtn.Size = New System.Drawing.Size(28, 24)
        Me.Form_Size_TlStrpBtn.Text = "TabControl"
        Me.Form_Size_TlStrpBtn.ToolTipText = "Show/Hide TabControl"
        Me.Form_Size_TlStrpBtn.Visible = False
        '
        'Exit_TlStrpBtn
        '
        Me.Exit_TlStrpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Exit_TlStrpBtn.Image = CType(resources.GetObject("Exit_TlStrpBtn.Image"), System.Drawing.Image)
        Me.Exit_TlStrpBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Exit_TlStrpBtn.Name = "Exit_TlStrpBtn"
        Me.Exit_TlStrpBtn.Size = New System.Drawing.Size(31, 24)
        Me.Exit_TlStrpBtn.Text = "Exit"
        '
        'Form_ToolTip
        '
        Me.Form_ToolTip.BackColor = System.Drawing.Color.Yellow
        Me.Form_ToolTip.IsBalloon = True
        '
        'WhatsApp_Btn
        '
        Me.WhatsApp_Btn.BackgroundImage = CType(resources.GetObject("WhatsApp_Btn.BackgroundImage"), System.Drawing.Image)
        Me.WhatsApp_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.WhatsApp_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.WhatsApp_Btn.Dock = System.Windows.Forms.DockStyle.Right
        Me.WhatsApp_Btn.FlatAppearance.BorderSize = 0
        Me.WhatsApp_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.WhatsApp_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.WhatsApp_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.WhatsApp_Btn.Location = New System.Drawing.Point(909, 0)
        Me.WhatsApp_Btn.Name = "WhatsApp_Btn"
        Me.WhatsApp_Btn.Size = New System.Drawing.Size(28, 24)
        Me.WhatsApp_Btn.TabIndex = 1061
        Me.WhatsApp_Btn.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.WhatsApp_Btn, "Send WhatsApp Message أرسل رسالة واتس آب")
        Me.WhatsApp_Btn.UseVisualStyleBackColor = True
        Me.WhatsApp_Btn.Visible = False
        '
        'Previous_Btn
        '
        Me.Previous_Btn.BackgroundImage = CType(resources.GetObject("Previous_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Previous_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Previous_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Previous_Btn.Dock = System.Windows.Forms.DockStyle.Right
        Me.Previous_Btn.FlatAppearance.BorderSize = 0
        Me.Previous_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Previous_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Previous_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Previous_Btn.Location = New System.Drawing.Point(937, 0)
        Me.Previous_Btn.Name = "Previous_Btn"
        Me.Previous_Btn.Size = New System.Drawing.Size(28, 24)
        Me.Previous_Btn.TabIndex = 3
        Me.Previous_Btn.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.Previous_Btn, "Previous Sticky Note")
        Me.Previous_Btn.UseVisualStyleBackColor = True
        '
        'Next_Btn
        '
        Me.Next_Btn.BackgroundImage = CType(resources.GetObject("Next_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Next_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Next_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Next_Btn.Dock = System.Windows.Forms.DockStyle.Right
        Me.Next_Btn.FlatAppearance.BorderSize = 0
        Me.Next_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Next_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Next_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Next_Btn.Location = New System.Drawing.Point(965, 0)
        Me.Next_Btn.Name = "Next_Btn"
        Me.Next_Btn.Size = New System.Drawing.Size(28, 24)
        Me.Next_Btn.TabIndex = 4
        Me.Next_Btn.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.Next_Btn, "Next Sticky Note")
        Me.Next_Btn.UseVisualStyleBackColor = True
        '
        'Language_Btn
        '
        Me.Language_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Language_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Language_Btn.Dock = System.Windows.Forms.DockStyle.Right
        Me.Language_Btn.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Language_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Language_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Language_Btn.Location = New System.Drawing.Point(993, 0)
        Me.Language_Btn.Name = "Language_Btn"
        Me.Language_Btn.Size = New System.Drawing.Size(28, 24)
        Me.Language_Btn.TabIndex = 6
        Me.Language_Btn.TabStop = False
        Me.Language_Btn.Text = "L"
        Me.Form_ToolTip.SetToolTip(Me.Language_Btn, "Next Sticky Note")
        Me.Language_Btn.UseVisualStyleBackColor = True
        '
        'Add_New_Sticky_Note_Btn
        '
        Me.Add_New_Sticky_Note_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_New_Sticky_Note_Btn.BackgroundImage = CType(resources.GetObject("Add_New_Sticky_Note_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Add_New_Sticky_Note_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Add_New_Sticky_Note_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Add_New_Sticky_Note_Btn.FlatAppearance.BorderSize = 0
        Me.Add_New_Sticky_Note_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Add_New_Sticky_Note_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Add_New_Sticky_Note_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Add_New_Sticky_Note_Btn.Location = New System.Drawing.Point(972, -56)
        Me.Add_New_Sticky_Note_Btn.Name = "Add_New_Sticky_Note_Btn"
        Me.Add_New_Sticky_Note_Btn.Size = New System.Drawing.Size(22, 23)
        Me.Add_New_Sticky_Note_Btn.TabIndex = 5
        Me.Add_New_Sticky_Note_Btn.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.Add_New_Sticky_Note_Btn, "Generate New Sticky Note Name")
        Me.Add_New_Sticky_Note_Btn.UseVisualStyleBackColor = True
        '
        'Reminder_Every_Minutes_NmrcUpDn
        '
        Me.Reminder_Every_Minutes_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Reminder_Every_Minutes_NmrcUpDn.Location = New System.Drawing.Point(238, 287)
        Me.Reminder_Every_Minutes_NmrcUpDn.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.Reminder_Every_Minutes_NmrcUpDn.Name = "Reminder_Every_Minutes_NmrcUpDn"
        Me.Reminder_Every_Minutes_NmrcUpDn.Size = New System.Drawing.Size(45, 22)
        Me.Reminder_Every_Minutes_NmrcUpDn.TabIndex = 911
        Me.Form_ToolTip.SetToolTip(Me.Reminder_Every_Minutes_NmrcUpDn, "Minute/دقيقة")
        '
        'Reminder_Every_Hours_NmrcUpDn
        '
        Me.Reminder_Every_Hours_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Reminder_Every_Hours_NmrcUpDn.Location = New System.Drawing.Point(192, 287)
        Me.Reminder_Every_Hours_NmrcUpDn.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.Reminder_Every_Hours_NmrcUpDn.Name = "Reminder_Every_Hours_NmrcUpDn"
        Me.Reminder_Every_Hours_NmrcUpDn.Size = New System.Drawing.Size(45, 22)
        Me.Reminder_Every_Hours_NmrcUpDn.TabIndex = 910
        Me.Form_ToolTip.SetToolTip(Me.Reminder_Every_Hours_NmrcUpDn, "Hour/ساعة")
        '
        'Reminder_Every_Days_NmrcUpDn
        '
        Me.Reminder_Every_Days_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Reminder_Every_Days_NmrcUpDn.Location = New System.Drawing.Point(146, 287)
        Me.Reminder_Every_Days_NmrcUpDn.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.Reminder_Every_Days_NmrcUpDn.Name = "Reminder_Every_Days_NmrcUpDn"
        Me.Reminder_Every_Days_NmrcUpDn.Size = New System.Drawing.Size(45, 22)
        Me.Reminder_Every_Days_NmrcUpDn.TabIndex = 909
        Me.Form_ToolTip.SetToolTip(Me.Reminder_Every_Days_NmrcUpDn, "Day/يوم")
        '
        'Minutes_NmrcUpDn
        '
        Me.Minutes_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Minutes_NmrcUpDn.Location = New System.Drawing.Point(245, 604)
        Me.Minutes_NmrcUpDn.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.Minutes_NmrcUpDn.Name = "Minutes_NmrcUpDn"
        Me.Minutes_NmrcUpDn.Size = New System.Drawing.Size(45, 22)
        Me.Minutes_NmrcUpDn.TabIndex = 903
        Me.Form_ToolTip.SetToolTip(Me.Minutes_NmrcUpDn, "Minute/دقيقة")
        '
        'Hours_NmrcUpDn
        '
        Me.Hours_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Hours_NmrcUpDn.Location = New System.Drawing.Point(198, 604)
        Me.Hours_NmrcUpDn.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.Hours_NmrcUpDn.Name = "Hours_NmrcUpDn"
        Me.Hours_NmrcUpDn.Size = New System.Drawing.Size(45, 22)
        Me.Hours_NmrcUpDn.TabIndex = 902
        Me.Form_ToolTip.SetToolTip(Me.Hours_NmrcUpDn, "Hour/ساعة")
        '
        'Days_NmrcUpDn
        '
        Me.Days_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Days_NmrcUpDn.Location = New System.Drawing.Point(152, 604)
        Me.Days_NmrcUpDn.Maximum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.Days_NmrcUpDn.Name = "Days_NmrcUpDn"
        Me.Days_NmrcUpDn.Size = New System.Drawing.Size(45, 22)
        Me.Days_NmrcUpDn.TabIndex = 901
        Me.Form_ToolTip.SetToolTip(Me.Days_NmrcUpDn, "Day/يوم")
        '
        'Save_Sticky_Form_Parameter_Setting_Btn
        '
        Me.Save_Sticky_Form_Parameter_Setting_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Save_Sticky_Form_Parameter_Setting_Btn.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Save_Sticky_Form_Parameter_Setting_Btn.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Save_Sticky_Form_Parameter_Setting_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Save_Sticky_Form_Parameter_Setting_Btn.Location = New System.Drawing.Point(-1, 695)
        Me.Save_Sticky_Form_Parameter_Setting_Btn.Name = "Save_Sticky_Form_Parameter_Setting_Btn"
        Me.Save_Sticky_Form_Parameter_Setting_Btn.Size = New System.Drawing.Size(317, 30)
        Me.Save_Sticky_Form_Parameter_Setting_Btn.TabIndex = 660
        Me.Save_Sticky_Form_Parameter_Setting_Btn.Text = "Save Sticky Form Parameters Setting"
        Me.Form_ToolTip.SetToolTip(Me.Save_Sticky_Form_Parameter_Setting_Btn, "Save Or Update Your Current Sticky")
        Me.Save_Sticky_Form_Parameter_Setting_Btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Button1.Location = New System.Drawing.Point(105, 118)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(34, 32)
        Me.Button1.TabIndex = 1060
        Me.Form_ToolTip.SetToolTip(Me.Button1, "Press F4 To Click Me")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Pause_Btn
        '
        Me.Pause_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Pause_Btn.BackgroundImage = CType(resources.GetObject("Pause_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Pause_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Pause_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pause_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pause_Btn.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Pause_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Pause_Btn.Location = New System.Drawing.Point(173, 118)
        Me.Pause_Btn.Name = "Pause_Btn"
        Me.Pause_Btn.Size = New System.Drawing.Size(34, 32)
        Me.Pause_Btn.TabIndex = 28
        Me.Form_ToolTip.SetToolTip(Me.Pause_Btn, "Press Shift+F4 To Click Me")
        Me.Pause_Btn.UseVisualStyleBackColor = False
        '
        'Start_Btn
        '
        Me.Start_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Start_Btn.BackgroundImage = CType(resources.GetObject("Start_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Start_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Start_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Start_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Start_Btn.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Start_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Start_Btn.Location = New System.Drawing.Point(209, 118)
        Me.Start_Btn.Name = "Start_Btn"
        Me.Start_Btn.Size = New System.Drawing.Size(34, 32)
        Me.Start_Btn.TabIndex = 26
        Me.Form_ToolTip.SetToolTip(Me.Start_Btn, "Press F4 To Click Me")
        Me.Start_Btn.UseVisualStyleBackColor = False
        '
        'Stop_Btn
        '
        Me.Stop_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Stop_Btn.BackgroundImage = CType(resources.GetObject("Stop_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Stop_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Stop_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Stop_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Stop_Btn.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stop_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stop_Btn.Location = New System.Drawing.Point(140, 118)
        Me.Stop_Btn.Name = "Stop_Btn"
        Me.Stop_Btn.Size = New System.Drawing.Size(34, 32)
        Me.Stop_Btn.TabIndex = 27
        Me.Form_ToolTip.SetToolTip(Me.Stop_Btn, "Press Shift+F4 To Click Me")
        Me.Stop_Btn.UseVisualStyleBackColor = False
        '
        'Time_To_Alert_Before_Azan_NmrcUpDn
        '
        Me.Time_To_Alert_Before_Azan_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Time_To_Alert_Before_Azan_NmrcUpDn.Location = New System.Drawing.Point(790, 146)
        Me.Time_To_Alert_Before_Azan_NmrcUpDn.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.Time_To_Alert_Before_Azan_NmrcUpDn.Name = "Time_To_Alert_Before_Azan_NmrcUpDn"
        Me.Time_To_Alert_Before_Azan_NmrcUpDn.Size = New System.Drawing.Size(45, 23)
        Me.Time_To_Alert_Before_Azan_NmrcUpDn.TabIndex = 1095
        Me.Form_ToolTip.SetToolTip(Me.Time_To_Alert_Before_Azan_NmrcUpDn, "Minute/دقيقة")
        '
        'Alert_Repeet_Minute_NmrcUpDn
        '
        Me.Alert_Repeet_Minute_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Alert_Repeet_Minute_NmrcUpDn.Location = New System.Drawing.Point(87, 31)
        Me.Alert_Repeet_Minute_NmrcUpDn.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.Alert_Repeet_Minute_NmrcUpDn.Name = "Alert_Repeet_Minute_NmrcUpDn"
        Me.Alert_Repeet_Minute_NmrcUpDn.Size = New System.Drawing.Size(42, 22)
        Me.Alert_Repeet_Minute_NmrcUpDn.TabIndex = 1091
        Me.Form_ToolTip.SetToolTip(Me.Alert_Repeet_Minute_NmrcUpDn, "Minute/دقيقة")
        '
        'Alert_Repeet_Hour_NmrcUpDn
        '
        Me.Alert_Repeet_Hour_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Alert_Repeet_Hour_NmrcUpDn.Location = New System.Drawing.Point(44, 31)
        Me.Alert_Repeet_Hour_NmrcUpDn.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.Alert_Repeet_Hour_NmrcUpDn.Name = "Alert_Repeet_Hour_NmrcUpDn"
        Me.Alert_Repeet_Hour_NmrcUpDn.Size = New System.Drawing.Size(42, 22)
        Me.Alert_Repeet_Hour_NmrcUpDn.TabIndex = 1090
        Me.Form_ToolTip.SetToolTip(Me.Alert_Repeet_Hour_NmrcUpDn, "Hour/ساعة")
        '
        'Alert_Repeet_Day_NmrcUpDn
        '
        Me.Alert_Repeet_Day_NmrcUpDn.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Alert_Repeet_Day_NmrcUpDn.Location = New System.Drawing.Point(1, 31)
        Me.Alert_Repeet_Day_NmrcUpDn.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.Alert_Repeet_Day_NmrcUpDn.Name = "Alert_Repeet_Day_NmrcUpDn"
        Me.Alert_Repeet_Day_NmrcUpDn.Size = New System.Drawing.Size(42, 22)
        Me.Alert_Repeet_Day_NmrcUpDn.TabIndex = 1089
        Me.Form_ToolTip.SetToolTip(Me.Alert_Repeet_Day_NmrcUpDn, "Day/يوم")
        '
        'Save_Alert_Btn
        '
        Me.Save_Alert_Btn.BackgroundImage = CType(resources.GetObject("Save_Alert_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Save_Alert_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Save_Alert_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_Alert_Btn.FlatAppearance.BorderSize = 0
        Me.Save_Alert_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_Alert_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Save_Alert_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Save_Alert_Btn.Location = New System.Drawing.Point(4, 162)
        Me.Save_Alert_Btn.Name = "Save_Alert_Btn"
        Me.Save_Alert_Btn.Size = New System.Drawing.Size(28, 28)
        Me.Save_Alert_Btn.TabIndex = 1107
        Me.Form_ToolTip.SetToolTip(Me.Save_Alert_Btn, "Previous Sticky Note")
        Me.Save_Alert_Btn.UseVisualStyleBackColor = True
        '
        'Delete_Alert_Btn
        '
        Me.Delete_Alert_Btn.BackgroundImage = CType(resources.GetObject("Delete_Alert_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Delete_Alert_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Delete_Alert_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_Alert_Btn.FlatAppearance.BorderSize = 0
        Me.Delete_Alert_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_Alert_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Delete_Alert_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Delete_Alert_Btn.Location = New System.Drawing.Point(34, 162)
        Me.Delete_Alert_Btn.Name = "Delete_Alert_Btn"
        Me.Delete_Alert_Btn.Size = New System.Drawing.Size(28, 28)
        Me.Delete_Alert_Btn.TabIndex = 1108
        Me.Form_ToolTip.SetToolTip(Me.Delete_Alert_Btn, "Next Sticky Note")
        Me.Delete_Alert_Btn.UseVisualStyleBackColor = True
        '
        'Pause_Alarm_Timer_Btn
        '
        Me.Pause_Alarm_Timer_Btn.BackgroundImage = CType(resources.GetObject("Pause_Alarm_Timer_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Pause_Alarm_Timer_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Pause_Alarm_Timer_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pause_Alarm_Timer_Btn.FlatAppearance.BorderSize = 0
        Me.Pause_Alarm_Timer_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pause_Alarm_Timer_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Pause_Alarm_Timer_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Pause_Alarm_Timer_Btn.Location = New System.Drawing.Point(5, 133)
        Me.Pause_Alarm_Timer_Btn.Name = "Pause_Alarm_Timer_Btn"
        Me.Pause_Alarm_Timer_Btn.Size = New System.Drawing.Size(28, 28)
        Me.Pause_Alarm_Timer_Btn.TabIndex = 1110
        Me.Pause_Alarm_Timer_Btn.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.Pause_Alarm_Timer_Btn, "Next Sticky Note")
        Me.Pause_Alarm_Timer_Btn.UseVisualStyleBackColor = True
        '
        'Hide_Me_PctrBx
        '
        Me.Hide_Me_PctrBx.BackColor = System.Drawing.Color.DarkMagenta
        Me.Hide_Me_PctrBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Hide_Me_PctrBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hide_Me_PctrBx.Image = CType(resources.GetObject("Hide_Me_PctrBx.Image"), System.Drawing.Image)
        Me.Hide_Me_PctrBx.Location = New System.Drawing.Point(302, 32)
        Me.Hide_Me_PctrBx.Name = "Hide_Me_PctrBx"
        Me.Hide_Me_PctrBx.Size = New System.Drawing.Size(28, 28)
        Me.Hide_Me_PctrBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Hide_Me_PctrBx.TabIndex = 1068
        Me.Hide_Me_PctrBx.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.Hide_Me_PctrBx, "Click to Minimize Me")
        '
        'MagNote_PctrBx
        '
        Me.MagNote_PctrBx.BackColor = System.Drawing.Color.Transparent
        Me.MagNote_PctrBx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.MagNote_PctrBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MagNote_PctrBx.Dock = System.Windows.Forms.DockStyle.Top
        Me.MagNote_PctrBx.Image = CType(resources.GetObject("MagNote_PctrBx.Image"), System.Drawing.Image)
        Me.MagNote_PctrBx.Location = New System.Drawing.Point(0, 0)
        Me.MagNote_PctrBx.Name = "MagNote_PctrBx"
        Me.MagNote_PctrBx.Size = New System.Drawing.Size(1055, 100)
        Me.MagNote_PctrBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.MagNote_PctrBx.TabIndex = 1067
        Me.MagNote_PctrBx.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.MagNote_PctrBx, "Click To Minimize Or Double Click To Maximize Me")
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Table_Pnl
        '
        Me.Table_Pnl.Controls.Add(Me.Exit_Table_Pnl_Btn)
        Me.Table_Pnl.Controls.Add(Me.Insert_Btn)
        Me.Table_Pnl.Controls.Add(Me.Cell_Height_TxtBx)
        Me.Table_Pnl.Controls.Add(Me.Cell_Width_TxtBx)
        Me.Table_Pnl.Controls.Add(Me.Rows_TxtBx)
        Me.Table_Pnl.Controls.Add(Me.Columns_TxtBx)
        Me.Table_Pnl.Controls.Add(Me.Table_Grid_Pnl)
        Me.Table_Pnl.Controls.Add(Me.Cell_Height_Lbl)
        Me.Table_Pnl.Controls.Add(Me.Cell_Width_Lbl)
        Me.Table_Pnl.Controls.Add(Me.Rows_Lbl)
        Me.Table_Pnl.Controls.Add(Me.Columns_Lbl)
        Me.Table_Pnl.Location = New System.Drawing.Point(3375, 200)
        Me.Table_Pnl.Name = "Table_Pnl"
        Me.Table_Pnl.Size = New System.Drawing.Size(489, 262)
        Me.Table_Pnl.TabIndex = 1063
        Me.Table_Pnl.Visible = False
        '
        'Exit_Table_Pnl_Btn
        '
        Me.Exit_Table_Pnl_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Exit_Table_Pnl_Btn.Location = New System.Drawing.Point(57, -1)
        Me.Exit_Table_Pnl_Btn.Name = "Exit_Table_Pnl_Btn"
        Me.Exit_Table_Pnl_Btn.Size = New System.Drawing.Size(60, 49)
        Me.Exit_Table_Pnl_Btn.TabIndex = 1077
        Me.Exit_Table_Pnl_Btn.Text = "Exit"
        Me.Exit_Table_Pnl_Btn.UseVisualStyleBackColor = True
        '
        'Insert_Btn
        '
        Me.Insert_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Insert_Btn.Location = New System.Drawing.Point(-2, -1)
        Me.Insert_Btn.Name = "Insert_Btn"
        Me.Insert_Btn.Size = New System.Drawing.Size(60, 49)
        Me.Insert_Btn.TabIndex = 1062
        Me.Insert_Btn.Text = "Insert"
        Me.Insert_Btn.UseVisualStyleBackColor = True
        '
        'Cell_Height_TxtBx
        '
        Me.Cell_Height_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cell_Height_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Cell_Height_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cell_Height_TxtBx.Location = New System.Drawing.Point(397, 23)
        Me.Cell_Height_TxtBx.Name = "Cell_Height_TxtBx"
        Me.Cell_Height_TxtBx.ReadOnly = True
        Me.Cell_Height_TxtBx.Size = New System.Drawing.Size(92, 20)
        Me.Cell_Height_TxtBx.TabIndex = 1076
        Me.Cell_Height_TxtBx.TabStop = False
        Me.Cell_Height_TxtBx.Text = "20"
        Me.Cell_Height_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Cell_Width_TxtBx
        '
        Me.Cell_Width_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cell_Width_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Cell_Width_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cell_Width_TxtBx.Location = New System.Drawing.Point(304, 23)
        Me.Cell_Width_TxtBx.Name = "Cell_Width_TxtBx"
        Me.Cell_Width_TxtBx.ReadOnly = True
        Me.Cell_Width_TxtBx.Size = New System.Drawing.Size(92, 20)
        Me.Cell_Width_TxtBx.TabIndex = 1075
        Me.Cell_Width_TxtBx.TabStop = False
        Me.Cell_Width_TxtBx.Text = "70"
        Me.Cell_Width_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Rows_TxtBx
        '
        Me.Rows_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Rows_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Rows_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Rows_TxtBx.Location = New System.Drawing.Point(210, 23)
        Me.Rows_TxtBx.Name = "Rows_TxtBx"
        Me.Rows_TxtBx.ReadOnly = True
        Me.Rows_TxtBx.Size = New System.Drawing.Size(92, 20)
        Me.Rows_TxtBx.TabIndex = 1074
        Me.Rows_TxtBx.TabStop = False
        Me.Rows_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Columns_TxtBx
        '
        Me.Columns_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Columns_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Columns_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Columns_TxtBx.Location = New System.Drawing.Point(117, 23)
        Me.Columns_TxtBx.Name = "Columns_TxtBx"
        Me.Columns_TxtBx.ReadOnly = True
        Me.Columns_TxtBx.Size = New System.Drawing.Size(92, 20)
        Me.Columns_TxtBx.TabIndex = 1073
        Me.Columns_TxtBx.TabStop = False
        Me.Columns_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Table_Grid_Pnl
        '
        Me.Table_Grid_Pnl.Controls.Add(Me.Columns_Count_VScrllBr)
        Me.Table_Grid_Pnl.Controls.Add(Me.Table_DGV)
        Me.Table_Grid_Pnl.Controls.Add(Me.Cell_Width_VScrllBr)
        Me.Table_Grid_Pnl.Controls.Add(Me.Rows_Count_VScrllBr)
        Me.Table_Grid_Pnl.Controls.Add(Me.Cell_Height_VScrllBr)
        Me.Table_Grid_Pnl.Controls.Add(Me.Grid_Sample_Lbl)
        Me.Table_Grid_Pnl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Table_Grid_Pnl.Location = New System.Drawing.Point(0, 47)
        Me.Table_Grid_Pnl.Name = "Table_Grid_Pnl"
        Me.Table_Grid_Pnl.Size = New System.Drawing.Size(489, 215)
        Me.Table_Grid_Pnl.TabIndex = 1072
        '
        'Columns_Count_VScrllBr
        '
        Me.Columns_Count_VScrllBr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Columns_Count_VScrllBr.Dock = System.Windows.Forms.DockStyle.Top
        Me.Columns_Count_VScrllBr.Location = New System.Drawing.Point(17, 22)
        Me.Columns_Count_VScrllBr.Minimum = 1
        Me.Columns_Count_VScrllBr.Name = "Columns_Count_VScrllBr"
        Me.Columns_Count_VScrllBr.Size = New System.Drawing.Size(455, 17)
        Me.Columns_Count_VScrllBr.TabIndex = 1075
        Me.Columns_Count_VScrllBr.Value = 2
        '
        'Table_DGV
        '
        Me.Table_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Table_DGV.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Table_DGV.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Table_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.Table_DGV.Location = New System.Drawing.Point(17, 44)
        Me.Table_DGV.Name = "Table_DGV"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Table_DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Table_DGV.RowHeadersWidth = 60
        Me.Table_DGV.Size = New System.Drawing.Size(455, 154)
        Me.Table_DGV.TabIndex = 1072
        '
        'Cell_Width_VScrllBr
        '
        Me.Cell_Width_VScrllBr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cell_Width_VScrllBr.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Cell_Width_VScrllBr.Location = New System.Drawing.Point(17, 198)
        Me.Cell_Width_VScrllBr.Maximum = 1000
        Me.Cell_Width_VScrllBr.Name = "Cell_Width_VScrllBr"
        Me.Cell_Width_VScrllBr.Size = New System.Drawing.Size(455, 17)
        Me.Cell_Width_VScrllBr.TabIndex = 1073
        Me.Cell_Width_VScrllBr.Value = 1
        '
        'Rows_Count_VScrllBr
        '
        Me.Rows_Count_VScrllBr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Rows_Count_VScrllBr.Dock = System.Windows.Forms.DockStyle.Left
        Me.Rows_Count_VScrllBr.Location = New System.Drawing.Point(0, 22)
        Me.Rows_Count_VScrllBr.Minimum = 2
        Me.Rows_Count_VScrllBr.Name = "Rows_Count_VScrllBr"
        Me.Rows_Count_VScrllBr.Size = New System.Drawing.Size(17, 193)
        Me.Rows_Count_VScrllBr.TabIndex = 1076
        Me.Rows_Count_VScrllBr.Value = 2
        '
        'Cell_Height_VScrllBr
        '
        Me.Cell_Height_VScrllBr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cell_Height_VScrllBr.Dock = System.Windows.Forms.DockStyle.Right
        Me.Cell_Height_VScrllBr.Location = New System.Drawing.Point(472, 22)
        Me.Cell_Height_VScrllBr.Name = "Cell_Height_VScrllBr"
        Me.Cell_Height_VScrllBr.Size = New System.Drawing.Size(17, 193)
        Me.Cell_Height_VScrllBr.TabIndex = 1074
        Me.Cell_Height_VScrllBr.Value = 1
        '
        'Grid_Sample_Lbl
        '
        Me.Grid_Sample_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Grid_Sample_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Grid_Sample_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grid_Sample_Lbl.Dock = System.Windows.Forms.DockStyle.Top
        Me.Grid_Sample_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Grid_Sample_Lbl.Name = "Grid_Sample_Lbl"
        Me.Grid_Sample_Lbl.Size = New System.Drawing.Size(489, 22)
        Me.Grid_Sample_Lbl.TabIndex = 1065
        Me.Grid_Sample_Lbl.Text = "Grid Sample"
        Me.Grid_Sample_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Cell_Height_Lbl
        '
        Me.Cell_Height_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Cell_Height_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cell_Height_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cell_Height_Lbl.Location = New System.Drawing.Point(397, 1)
        Me.Cell_Height_Lbl.Name = "Cell_Height_Lbl"
        Me.Cell_Height_Lbl.Size = New System.Drawing.Size(92, 22)
        Me.Cell_Height_Lbl.TabIndex = 1069
        Me.Cell_Height_Lbl.Text = "Cell Height"
        Me.Cell_Height_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Cell_Width_Lbl
        '
        Me.Cell_Width_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Cell_Width_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cell_Width_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cell_Width_Lbl.Location = New System.Drawing.Point(304, 1)
        Me.Cell_Width_Lbl.Name = "Cell_Width_Lbl"
        Me.Cell_Width_Lbl.Size = New System.Drawing.Size(92, 22)
        Me.Cell_Width_Lbl.TabIndex = 1068
        Me.Cell_Width_Lbl.Text = "Cell Width"
        Me.Cell_Width_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Rows_Lbl
        '
        Me.Rows_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Rows_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Rows_Lbl.Location = New System.Drawing.Point(210, 1)
        Me.Rows_Lbl.Name = "Rows_Lbl"
        Me.Rows_Lbl.Size = New System.Drawing.Size(92, 22)
        Me.Rows_Lbl.TabIndex = 1067
        Me.Rows_Lbl.Text = "Rows"
        Me.Rows_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Columns_Lbl
        '
        Me.Columns_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Columns_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Columns_Lbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Columns_Lbl.Location = New System.Drawing.Point(117, 1)
        Me.Columns_Lbl.Name = "Columns_Lbl"
        Me.Columns_Lbl.Size = New System.Drawing.Size(92, 22)
        Me.Columns_Lbl.TabIndex = 1066
        Me.Columns_Lbl.Text = "Columns"
        Me.Columns_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Form_Transparency_TrkBr
        '
        Me.Form_Transparency_TrkBr.AutoSize = False
        Me.Form_Transparency_TrkBr.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Form_Transparency_TrkBr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Form_Transparency_TrkBr.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Form_Transparency_TrkBr.Location = New System.Drawing.Point(34, 435)
        Me.Form_Transparency_TrkBr.Maximum = 95
        Me.Form_Transparency_TrkBr.Minimum = 15
        Me.Form_Transparency_TrkBr.Name = "Form_Transparency_TrkBr"
        Me.Form_Transparency_TrkBr.Size = New System.Drawing.Size(1021, 20)
        Me.Form_Transparency_TrkBr.TabIndex = 0
        Me.Form_Transparency_TrkBr.TickStyle = System.Windows.Forms.TickStyle.None
        Me.Form_Transparency_TrkBr.Value = 95
        '
        'MsgBox_SttsStrp
        '
        Me.MsgBox_SttsStrp.AllowMerge = False
        Me.MsgBox_SttsStrp.AutoSize = False
        Me.MsgBox_SttsStrp.BackColor = System.Drawing.SystemColors.Control
        Me.MsgBox_SttsStrp.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.MsgBox_SttsStrp.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MsgBox_SttsStrp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MsgBox_TlStrpSttsLbl})
        Me.MsgBox_SttsStrp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.MsgBox_SttsStrp.Location = New System.Drawing.Point(34, 455)
        Me.MsgBox_SttsStrp.Name = "MsgBox_SttsStrp"
        Me.MsgBox_SttsStrp.Padding = New System.Windows.Forms.Padding(2, 0, 16, 0)
        Me.MsgBox_SttsStrp.ShowItemToolTips = True
        Me.MsgBox_SttsStrp.Size = New System.Drawing.Size(1021, 73)
        Me.MsgBox_SttsStrp.TabIndex = 2
        '
        'MsgBox_TlStrpSttsLbl
        '
        Me.MsgBox_TlStrpSttsLbl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MsgBox_TlStrpSttsLbl.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust
        Me.MsgBox_TlStrpSttsLbl.ForeColor = System.Drawing.Color.White
        Me.MsgBox_TlStrpSttsLbl.Name = "MsgBox_TlStrpSttsLbl"
        Me.MsgBox_TlStrpSttsLbl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always
        Me.MsgBox_TlStrpSttsLbl.Size = New System.Drawing.Size(0, 0)
        '
        'Sticky_Navigater_Pnl
        '
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Sticky_Note_No_CmbBx)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.File_Format_CmbBx)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Sticky_Note_Category_CmbBx)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.WhatsApp_Btn)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Previous_Btn)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Next_Btn)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Language_Btn)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Sticky_Note_Lbl)
        Me.Sticky_Navigater_Pnl.Controls.Add(Me.Add_New_Sticky_Note_Btn)
        Me.Sticky_Navigater_Pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.Sticky_Navigater_Pnl.Location = New System.Drawing.Point(34, 100)
        Me.Sticky_Navigater_Pnl.Name = "Sticky_Navigater_Pnl"
        Me.Sticky_Navigater_Pnl.Size = New System.Drawing.Size(1021, 24)
        Me.Sticky_Navigater_Pnl.TabIndex = 0
        '
        'Sticky_Note_No_CmbBx
        '
        Me.Sticky_Note_No_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Note_No_CmbBx.DisplayMember = "Value"
        Me.Sticky_Note_No_CmbBx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Sticky_Note_No_CmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Note_No_CmbBx.FormattingEnabled = True
        Me.Sticky_Note_No_CmbBx.Location = New System.Drawing.Point(169, 0)
        Me.Sticky_Note_No_CmbBx.Name = "Sticky_Note_No_CmbBx"
        Me.Sticky_Note_No_CmbBx.Size = New System.Drawing.Size(590, 24)
        Me.Sticky_Note_No_CmbBx.Sorted = True
        Me.Sticky_Note_No_CmbBx.TabIndex = 2
        Me.Sticky_Note_No_CmbBx.ValueMember = "Key"
        '
        'File_Format_CmbBx
        '
        Me.File_Format_CmbBx.BackColor = System.Drawing.SystemColors.Window
        Me.File_Format_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.File_Format_CmbBx.Dock = System.Windows.Forms.DockStyle.Right
        Me.File_Format_CmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.File_Format_CmbBx.FormattingEnabled = True
        Me.File_Format_CmbBx.Location = New System.Drawing.Point(759, 0)
        Me.File_Format_CmbBx.Name = "File_Format_CmbBx"
        Me.File_Format_CmbBx.Size = New System.Drawing.Size(150, 24)
        Me.File_Format_CmbBx.Sorted = True
        Me.File_Format_CmbBx.TabIndex = 1063
        Me.File_Format_CmbBx.TabStop = False
        '
        'Sticky_Note_Category_CmbBx
        '
        Me.Sticky_Note_Category_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Note_Category_CmbBx.Dock = System.Windows.Forms.DockStyle.Left
        Me.Sticky_Note_Category_CmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Note_Category_CmbBx.FormattingEnabled = True
        Me.Sticky_Note_Category_CmbBx.Location = New System.Drawing.Point(0, 0)
        Me.Sticky_Note_Category_CmbBx.Name = "Sticky_Note_Category_CmbBx"
        Me.Sticky_Note_Category_CmbBx.Size = New System.Drawing.Size(169, 24)
        Me.Sticky_Note_Category_CmbBx.Sorted = True
        Me.Sticky_Note_Category_CmbBx.TabIndex = 1062
        Me.Sticky_Note_Category_CmbBx.TabStop = False
        '
        'Sticky_Note_Lbl
        '
        Me.Sticky_Note_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Note_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Note_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Note_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Note_Lbl.Location = New System.Drawing.Point(0, -56)
        Me.Sticky_Note_Lbl.Name = "Sticky_Note_Lbl"
        Me.Sticky_Note_Lbl.Size = New System.Drawing.Size(93, 25)
        Me.Sticky_Note_Lbl.TabIndex = 629
        Me.Sticky_Note_Lbl.Text = "Note No"
        Me.Sticky_Note_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Setting_TbCntrl
        '
        Me.Setting_TbCntrl.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.Setting_TbCntrl.Controls.Add(Me.Sticky_Notes_TbPg)
        Me.Setting_TbCntrl.Controls.Add(Me.Sticky_Parameters_TbPg)
        Me.Setting_TbCntrl.Controls.Add(Me.Form_Parameters_TbPg)
        Me.Setting_TbCntrl.Controls.Add(Me.Shortcuts_TbPg)
        Me.Setting_TbCntrl.Controls.Add(Me.Prayer_Time_TbPg)
        Me.Setting_TbCntrl.Controls.Add(Me.Alarm_Time_TbPg)
        Me.Setting_TbCntrl.Controls.Add(Me.Escalation_To_Author_TbPg)
        Me.Setting_TbCntrl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Setting_TbCntrl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Setting_TbCntrl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.Setting_TbCntrl.Location = New System.Drawing.Point(34, 531)
        Me.Setting_TbCntrl.Name = "Setting_TbCntrl"
        Me.Setting_TbCntrl.SelectedIndex = 0
        Me.Setting_TbCntrl.Size = New System.Drawing.Size(1021, 230)
        Me.Setting_TbCntrl.TabIndex = 1062
        '
        'Sticky_Notes_TbPg
        '
        Me.Sticky_Notes_TbPg.Controls.Add(Me.Available_Sticky_Notes_DGV)
        Me.Sticky_Notes_TbPg.Controls.Add(Me.Sticky_Notes_Header_Pnl)
        Me.Sticky_Notes_TbPg.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Notes_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Sticky_Notes_TbPg.Name = "Sticky_Notes_TbPg"
        Me.Sticky_Notes_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Sticky_Notes_TbPg.TabIndex = 2
        Me.Sticky_Notes_TbPg.Text = "Sticky Notes"
        Me.Sticky_Notes_TbPg.UseVisualStyleBackColor = True
        '
        'Available_Sticky_Notes_DGV
        '
        Me.Available_Sticky_Notes_DGV.AllowUserToAddRows = False
        Me.Available_Sticky_Notes_DGV.AllowUserToDeleteRows = False
        Me.Available_Sticky_Notes_DGV.AllowUserToOrderColumns = True
        Me.Available_Sticky_Notes_DGV.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Available_Sticky_Notes_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Available_Sticky_Notes_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Available_Sticky_Notes_DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Available_Sticky_Notes_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.Available_Sticky_Notes_DGV.EnableHeadersVisualStyles = False
        Me.Available_Sticky_Notes_DGV.Location = New System.Drawing.Point(0, 23)
        Me.Available_Sticky_Notes_DGV.MultiSelect = False
        Me.Available_Sticky_Notes_DGV.Name = "Available_Sticky_Notes_DGV"
        Me.Available_Sticky_Notes_DGV.ReadOnly = True
        Me.Available_Sticky_Notes_DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle3.NullValue = Nothing
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Available_Sticky_Notes_DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.Available_Sticky_Notes_DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.Available_Sticky_Notes_DGV.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.Available_Sticky_Notes_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Available_Sticky_Notes_DGV.Size = New System.Drawing.Size(1013, 175)
        Me.Available_Sticky_Notes_DGV.TabIndex = 0
        Me.Available_Sticky_Notes_DGV.TabStop = False
        '
        'Sticky_Notes_Header_Pnl
        '
        Me.Sticky_Notes_Header_Pnl.Controls.Add(Me.Available_Sticky_Notes_Lbl)
        Me.Sticky_Notes_Header_Pnl.Controls.Add(Me.View_Sticky_Notes_Btn)
        Me.Sticky_Notes_Header_Pnl.Controls.Add(Me.Preview_Btn)
        Me.Sticky_Notes_Header_Pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.Sticky_Notes_Header_Pnl.Location = New System.Drawing.Point(0, 0)
        Me.Sticky_Notes_Header_Pnl.Name = "Sticky_Notes_Header_Pnl"
        Me.Sticky_Notes_Header_Pnl.Size = New System.Drawing.Size(1013, 23)
        Me.Sticky_Notes_Header_Pnl.TabIndex = 645
        '
        'Available_Sticky_Notes_Lbl
        '
        Me.Available_Sticky_Notes_Lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Available_Sticky_Notes_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Available_Sticky_Notes_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Available_Sticky_Notes_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Available_Sticky_Notes_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Available_Sticky_Notes_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Available_Sticky_Notes_Lbl.Name = "Available_Sticky_Notes_Lbl"
        Me.Available_Sticky_Notes_Lbl.Size = New System.Drawing.Size(916, 22)
        Me.Available_Sticky_Notes_Lbl.TabIndex = 31
        Me.Available_Sticky_Notes_Lbl.Text = "Available Notes"
        Me.Available_Sticky_Notes_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'View_Sticky_Notes_Btn
        '
        Me.View_Sticky_Notes_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.View_Sticky_Notes_Btn.BackgroundImage = CType(resources.GetObject("View_Sticky_Notes_Btn.BackgroundImage"), System.Drawing.Image)
        Me.View_Sticky_Notes_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.View_Sticky_Notes_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.View_Sticky_Notes_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.View_Sticky_Notes_Btn.Location = New System.Drawing.Point(916, -1)
        Me.View_Sticky_Notes_Btn.Name = "View_Sticky_Notes_Btn"
        Me.View_Sticky_Notes_Btn.Size = New System.Drawing.Size(25, 24)
        Me.View_Sticky_Notes_Btn.TabIndex = 1
        Me.View_Sticky_Notes_Btn.TabStop = False
        Me.View_Sticky_Notes_Btn.UseVisualStyleBackColor = True
        '
        'Preview_Btn
        '
        Me.Preview_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Preview_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Preview_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Preview_Btn.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Preview_Btn.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Preview_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Preview_Btn.Location = New System.Drawing.Point(940, -1)
        Me.Preview_Btn.Name = "Preview_Btn"
        Me.Preview_Btn.Size = New System.Drawing.Size(73, 24)
        Me.Preview_Btn.TabIndex = 644
        Me.Preview_Btn.Text = "Preview"
        Me.Preview_Btn.UseVisualStyleBackColor = True
        '
        'Sticky_Parameters_TbPg
        '
        Me.Sticky_Parameters_TbPg.AutoScroll = True
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Button2)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Grid_Panel_Size_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Grid_Panel_Size_TxtBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Style_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Size_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Name_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Style_CmbBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Size_CmbBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Name_CmbBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Color_ClrCmbBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Back_Color_ClrCmbBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Word_Wrap_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Word_Wrap_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Pending_Reminder_Alert_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Stop_Reminder_Alert_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Next_Reminder_Time_DtTmPkr)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Reminder_Every_Minutes_NmrcUpDn)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Reminder_Every_Hours_NmrcUpDn)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Reminder_Every_Days_NmrcUpDn)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Reminder_Every_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Next_Reminder_Time_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Have_Reminder_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Have_Reminder_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Use_Main_Password_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Use_Main_Password_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Applicable_To_Rename_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Applicable_To_Rename_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Password_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Password_TxtBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Secured_Sticky_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Secured_Sticky_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Back_Color_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Finished_Sticky_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.View_Text_Font_Properties_Btn)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Blocked_Sticky_ChkBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_TxtBx)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Blocked_Sticky_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Finished_Sticky_Lbl)
        Me.Sticky_Parameters_TbPg.Controls.Add(Me.Sticky_Font_Color_Lbl)
        Me.Sticky_Parameters_TbPg.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Parameters_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Sticky_Parameters_TbPg.Name = "Sticky_Parameters_TbPg"
        Me.Sticky_Parameters_TbPg.Padding = New System.Windows.Forms.Padding(3)
        Me.Sticky_Parameters_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Sticky_Parameters_TbPg.TabIndex = 1
        Me.Sticky_Parameters_TbPg.Text = "Sticky Parameters"
        Me.Sticky_Parameters_TbPg.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(315, 115)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 23)
        Me.Button2.TabIndex = 1073
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Grid_Panel_Size_Lbl
        '
        Me.Grid_Panel_Size_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Grid_Panel_Size_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Grid_Panel_Size_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Grid_Panel_Size_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Grid_Panel_Size_Lbl.Location = New System.Drawing.Point(0, 379)
        Me.Grid_Panel_Size_Lbl.Name = "Grid_Panel_Size_Lbl"
        Me.Grid_Panel_Size_Lbl.Size = New System.Drawing.Size(145, 22)
        Me.Grid_Panel_Size_Lbl.TabIndex = 1069
        Me.Grid_Panel_Size_Lbl.Text = "Grid Panel Size"
        Me.Grid_Panel_Size_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Grid_Panel_Size_TxtBx
        '
        Me.Grid_Panel_Size_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Grid_Panel_Size_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Grid_Panel_Size_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Grid_Panel_Size_TxtBx.Location = New System.Drawing.Point(146, 379)
        Me.Grid_Panel_Size_TxtBx.Multiline = True
        Me.Grid_Panel_Size_TxtBx.Name = "Grid_Panel_Size_TxtBx"
        Me.Grid_Panel_Size_TxtBx.ReadOnly = True
        Me.Grid_Panel_Size_TxtBx.Size = New System.Drawing.Size(169, 22)
        Me.Grid_Panel_Size_TxtBx.TabIndex = 1070
        Me.Grid_Panel_Size_TxtBx.TabStop = False
        '
        'Sticky_Font_Style_Lbl
        '
        Me.Sticky_Font_Style_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Font_Style_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Font_Style_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Font_Style_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Font_Style_Lbl.Location = New System.Drawing.Point(0, 188)
        Me.Sticky_Font_Style_Lbl.Name = "Sticky_Font_Style_Lbl"
        Me.Sticky_Font_Style_Lbl.Size = New System.Drawing.Size(145, 24)
        Me.Sticky_Font_Style_Lbl.TabIndex = 1068
        Me.Sticky_Font_Style_Lbl.Text = "Sticky Font Style"
        Me.Sticky_Font_Style_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Font_Size_Lbl
        '
        Me.Sticky_Font_Size_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Font_Size_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Font_Size_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Font_Size_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Font_Size_Lbl.Location = New System.Drawing.Point(0, 163)
        Me.Sticky_Font_Size_Lbl.Name = "Sticky_Font_Size_Lbl"
        Me.Sticky_Font_Size_Lbl.Size = New System.Drawing.Size(145, 24)
        Me.Sticky_Font_Size_Lbl.TabIndex = 1067
        Me.Sticky_Font_Size_Lbl.Text = "Sticky Font Size"
        Me.Sticky_Font_Size_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Font_Name_Lbl
        '
        Me.Sticky_Font_Name_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Font_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Font_Name_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Font_Name_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Font_Name_Lbl.Location = New System.Drawing.Point(0, 138)
        Me.Sticky_Font_Name_Lbl.Name = "Sticky_Font_Name_Lbl"
        Me.Sticky_Font_Name_Lbl.Size = New System.Drawing.Size(145, 24)
        Me.Sticky_Font_Name_Lbl.TabIndex = 1066
        Me.Sticky_Font_Name_Lbl.Text = "Sticky Font Name"
        Me.Sticky_Font_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Font_Style_CmbBx
        '
        Me.Sticky_Font_Style_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Font_Style_CmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sticky_Font_Style_CmbBx.FormattingEnabled = True
        Me.Sticky_Font_Style_CmbBx.Items.AddRange(New Object() {"Bold", "Bold Italic", "Italic", "Regular", "Strikeout", "Underline"})
        Me.Sticky_Font_Style_CmbBx.Location = New System.Drawing.Point(146, 188)
        Me.Sticky_Font_Style_CmbBx.Name = "Sticky_Font_Style_CmbBx"
        Me.Sticky_Font_Style_CmbBx.Size = New System.Drawing.Size(74, 24)
        Me.Sticky_Font_Style_CmbBx.Sorted = True
        Me.Sticky_Font_Style_CmbBx.TabIndex = 1065
        Me.Sticky_Font_Style_CmbBx.TabStop = False
        Me.Sticky_Font_Style_CmbBx.Text = "Regular"
        '
        'Sticky_Font_Size_CmbBx
        '
        Me.Sticky_Font_Size_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Font_Size_CmbBx.DropDownWidth = 61
        Me.Sticky_Font_Size_CmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Font_Size_CmbBx.FormattingEnabled = True
        Me.Sticky_Font_Size_CmbBx.IntegralHeight = False
        Me.Sticky_Font_Size_CmbBx.Items.AddRange(New Object() {"10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72", "8", "9"})
        Me.Sticky_Font_Size_CmbBx.Location = New System.Drawing.Point(146, 163)
        Me.Sticky_Font_Size_CmbBx.Name = "Sticky_Font_Size_CmbBx"
        Me.Sticky_Font_Size_CmbBx.Size = New System.Drawing.Size(61, 24)
        Me.Sticky_Font_Size_CmbBx.TabIndex = 1064
        Me.Sticky_Font_Size_CmbBx.TabStop = False
        Me.Sticky_Font_Size_CmbBx.Text = "12"
        '
        'Sticky_Font_Name_CmbBx
        '
        Me.Sticky_Font_Name_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sticky_Font_Name_CmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.Sticky_Font_Name_CmbBx.FormattingEnabled = True
        Me.Sticky_Font_Name_CmbBx.Location = New System.Drawing.Point(171, 138)
        Me.Sticky_Font_Name_CmbBx.Name = "Sticky_Font_Name_CmbBx"
        Me.Sticky_Font_Name_CmbBx.Size = New System.Drawing.Size(144, 24)
        Me.Sticky_Font_Name_CmbBx.Sorted = True
        Me.Sticky_Font_Name_CmbBx.TabIndex = 1063
        Me.Sticky_Font_Name_CmbBx.TabStop = False
        Me.Sticky_Font_Name_CmbBx.Text = "Times New Roman"
        '
        'Sticky_Font_Color_ClrCmbBx
        '
        Me.Sticky_Font_Color_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sticky_Font_Color_ClrCmbBx.FormattingEnabled = True
        Me.Sticky_Font_Color_ClrCmbBx.IncludeSystemColors = True
        Me.Sticky_Font_Color_ClrCmbBx.IncludeTransparent = True
        Me.Sticky_Font_Color_ClrCmbBx.Location = New System.Drawing.Point(146, 213)
        Me.Sticky_Font_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Sticky_Font_Color_ClrCmbBx.Name = "Sticky_Font_Color_ClrCmbBx"
        Me.Sticky_Font_Color_ClrCmbBx.Size = New System.Drawing.Size(168, 24)
        Me.Sticky_Font_Color_ClrCmbBx.TabIndex = 917
        '
        'Sticky_Back_Color_ClrCmbBx
        '
        Me.Sticky_Back_Color_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sticky_Back_Color_ClrCmbBx.FormattingEnabled = True
        Me.Sticky_Back_Color_ClrCmbBx.IncludeSystemColors = True
        Me.Sticky_Back_Color_ClrCmbBx.IncludeTransparent = True
        Me.Sticky_Back_Color_ClrCmbBx.Location = New System.Drawing.Point(146, 238)
        Me.Sticky_Back_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Sticky_Back_Color_ClrCmbBx.Name = "Sticky_Back_Color_ClrCmbBx"
        Me.Sticky_Back_Color_ClrCmbBx.Size = New System.Drawing.Size(168, 24)
        Me.Sticky_Back_Color_ClrCmbBx.TabIndex = 918
        '
        'Sticky_Word_Wrap_ChkBx
        '
        Me.Sticky_Word_Wrap_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Word_Wrap_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Sticky_Word_Wrap_ChkBx.Location = New System.Drawing.Point(2, 311)
        Me.Sticky_Word_Wrap_ChkBx.Name = "Sticky_Word_Wrap_ChkBx"
        Me.Sticky_Word_Wrap_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Sticky_Word_Wrap_ChkBx.TabIndex = 916
        Me.Sticky_Word_Wrap_ChkBx.Text = "Sticky Word Wrap"
        Me.Sticky_Word_Wrap_ChkBx.UseVisualStyleBackColor = False
        '
        'Sticky_Word_Wrap_Lbl
        '
        Me.Sticky_Word_Wrap_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Sticky_Word_Wrap_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Word_Wrap_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Word_Wrap_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Word_Wrap_Lbl.Location = New System.Drawing.Point(0, 310)
        Me.Sticky_Word_Wrap_Lbl.Name = "Sticky_Word_Wrap_Lbl"
        Me.Sticky_Word_Wrap_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Sticky_Word_Wrap_Lbl.TabIndex = 915
        Me.Sticky_Word_Wrap_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Pending_Reminder_Alert_ChkBx
        '
        Me.Pending_Reminder_Alert_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Pending_Reminder_Alert_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Pending_Reminder_Alert_ChkBx.Location = New System.Drawing.Point(2, 357)
        Me.Pending_Reminder_Alert_ChkBx.Name = "Pending_Reminder_Alert_ChkBx"
        Me.Pending_Reminder_Alert_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Pending_Reminder_Alert_ChkBx.TabIndex = 914
        Me.Pending_Reminder_Alert_ChkBx.Text = "Pending Reminder Alert"
        Me.Pending_Reminder_Alert_ChkBx.UseVisualStyleBackColor = False
        '
        'Stop_Reminder_Alert_Lbl
        '
        Me.Stop_Reminder_Alert_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Stop_Reminder_Alert_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Stop_Reminder_Alert_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stop_Reminder_Alert_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stop_Reminder_Alert_Lbl.Location = New System.Drawing.Point(0, 356)
        Me.Stop_Reminder_Alert_Lbl.Name = "Stop_Reminder_Alert_Lbl"
        Me.Stop_Reminder_Alert_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Stop_Reminder_Alert_Lbl.TabIndex = 913
        Me.Stop_Reminder_Alert_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Next_Reminder_Time_DtTmPkr
        '
        Me.Next_Reminder_Time_DtTmPkr.CustomFormat = "yyyy-MM-dd HH-mm-ss"
        Me.Next_Reminder_Time_DtTmPkr.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.Next_Reminder_Time_DtTmPkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Next_Reminder_Time_DtTmPkr.Location = New System.Drawing.Point(146, 263)
        Me.Next_Reminder_Time_DtTmPkr.Name = "Next_Reminder_Time_DtTmPkr"
        Me.Next_Reminder_Time_DtTmPkr.Size = New System.Drawing.Size(168, 23)
        Me.Next_Reminder_Time_DtTmPkr.TabIndex = 912
        '
        'Reminder_Every_Lbl
        '
        Me.Reminder_Every_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Reminder_Every_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Reminder_Every_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Reminder_Every_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Reminder_Every_Lbl.Location = New System.Drawing.Point(0, 287)
        Me.Reminder_Every_Lbl.Name = "Reminder_Every_Lbl"
        Me.Reminder_Every_Lbl.Size = New System.Drawing.Size(145, 22)
        Me.Reminder_Every_Lbl.TabIndex = 908
        Me.Reminder_Every_Lbl.Text = "Reminder Every"
        Me.Reminder_Every_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Next_Reminder_Time_Lbl
        '
        Me.Next_Reminder_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Next_Reminder_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Next_Reminder_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Next_Reminder_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Next_Reminder_Time_Lbl.Location = New System.Drawing.Point(0, 263)
        Me.Next_Reminder_Time_Lbl.Name = "Next_Reminder_Time_Lbl"
        Me.Next_Reminder_Time_Lbl.Size = New System.Drawing.Size(145, 23)
        Me.Next_Reminder_Time_Lbl.TabIndex = 906
        Me.Next_Reminder_Time_Lbl.Text = "Next Reminder Time"
        Me.Next_Reminder_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Have_Reminder_ChkBx
        '
        Me.Sticky_Have_Reminder_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Have_Reminder_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Sticky_Have_Reminder_ChkBx.Location = New System.Drawing.Point(2, 334)
        Me.Sticky_Have_Reminder_ChkBx.Name = "Sticky_Have_Reminder_ChkBx"
        Me.Sticky_Have_Reminder_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Sticky_Have_Reminder_ChkBx.TabIndex = 899
        Me.Sticky_Have_Reminder_ChkBx.Text = "Sticky Have Reminder"
        Me.Sticky_Have_Reminder_ChkBx.UseVisualStyleBackColor = False
        '
        'Sticky_Have_Reminder_Lbl
        '
        Me.Sticky_Have_Reminder_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Sticky_Have_Reminder_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Have_Reminder_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Have_Reminder_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Have_Reminder_Lbl.Location = New System.Drawing.Point(0, 333)
        Me.Sticky_Have_Reminder_Lbl.Name = "Sticky_Have_Reminder_Lbl"
        Me.Sticky_Have_Reminder_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Sticky_Have_Reminder_Lbl.TabIndex = 898
        Me.Sticky_Have_Reminder_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Use_Main_Password_ChkBx
        '
        Me.Use_Main_Password_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Use_Main_Password_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Use_Main_Password_ChkBx.Location = New System.Drawing.Point(2, 93)
        Me.Use_Main_Password_ChkBx.Name = "Use_Main_Password_ChkBx"
        Me.Use_Main_Password_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Use_Main_Password_ChkBx.TabIndex = 897
        Me.Use_Main_Password_ChkBx.Text = "Use Main Password"
        Me.Use_Main_Password_ChkBx.UseVisualStyleBackColor = False
        '
        'Use_Main_Password_Lbl
        '
        Me.Use_Main_Password_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Use_Main_Password_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Use_Main_Password_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Use_Main_Password_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Use_Main_Password_Lbl.Location = New System.Drawing.Point(0, 92)
        Me.Use_Main_Password_Lbl.Name = "Use_Main_Password_Lbl"
        Me.Use_Main_Password_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Use_Main_Password_Lbl.TabIndex = 896
        Me.Use_Main_Password_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Applicable_To_Rename_ChkBx
        '
        Me.Sticky_Applicable_To_Rename_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Applicable_To_Rename_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Sticky_Applicable_To_Rename_ChkBx.Location = New System.Drawing.Point(2, 1)
        Me.Sticky_Applicable_To_Rename_ChkBx.Name = "Sticky_Applicable_To_Rename_ChkBx"
        Me.Sticky_Applicable_To_Rename_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Sticky_Applicable_To_Rename_ChkBx.TabIndex = 895
        Me.Sticky_Applicable_To_Rename_ChkBx.Text = "Sticky Applicable To Rename"
        Me.Sticky_Applicable_To_Rename_ChkBx.ThreeState = True
        Me.Sticky_Applicable_To_Rename_ChkBx.UseVisualStyleBackColor = False
        '
        'Sticky_Applicable_To_Rename_Lbl
        '
        Me.Sticky_Applicable_To_Rename_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Sticky_Applicable_To_Rename_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Applicable_To_Rename_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Applicable_To_Rename_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Applicable_To_Rename_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Sticky_Applicable_To_Rename_Lbl.Name = "Sticky_Applicable_To_Rename_Lbl"
        Me.Sticky_Applicable_To_Rename_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Sticky_Applicable_To_Rename_Lbl.TabIndex = 894
        Me.Sticky_Applicable_To_Rename_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Password_Lbl
        '
        Me.Sticky_Password_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Password_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Password_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Password_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Password_Lbl.Location = New System.Drawing.Point(0, 115)
        Me.Sticky_Password_Lbl.Name = "Sticky_Password_Lbl"
        Me.Sticky_Password_Lbl.Size = New System.Drawing.Size(145, 22)
        Me.Sticky_Password_Lbl.TabIndex = 892
        Me.Sticky_Password_Lbl.Text = "Sticky Password"
        Me.Sticky_Password_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Password_TxtBx
        '
        Me.Sticky_Password_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Sticky_Password_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Sticky_Password_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Password_TxtBx.Location = New System.Drawing.Point(146, 115)
        Me.Sticky_Password_TxtBx.Name = "Sticky_Password_TxtBx"
        Me.Sticky_Password_TxtBx.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Sticky_Password_TxtBx.ReadOnly = True
        Me.Sticky_Password_TxtBx.Size = New System.Drawing.Size(169, 22)
        Me.Sticky_Password_TxtBx.TabIndex = 893
        Me.Sticky_Password_TxtBx.TabStop = False
        Me.Sticky_Password_TxtBx.UseSystemPasswordChar = True
        '
        'Secured_Sticky_ChkBx
        '
        Me.Secured_Sticky_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Secured_Sticky_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Secured_Sticky_ChkBx.Location = New System.Drawing.Point(2, 70)
        Me.Secured_Sticky_ChkBx.Name = "Secured_Sticky_ChkBx"
        Me.Secured_Sticky_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Secured_Sticky_ChkBx.TabIndex = 891
        Me.Secured_Sticky_ChkBx.Text = "Secured Sticky"
        Me.Secured_Sticky_ChkBx.UseVisualStyleBackColor = False
        '
        'Secured_Sticky_Lbl
        '
        Me.Secured_Sticky_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Secured_Sticky_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Secured_Sticky_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Secured_Sticky_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Secured_Sticky_Lbl.Location = New System.Drawing.Point(0, 69)
        Me.Secured_Sticky_Lbl.Name = "Secured_Sticky_Lbl"
        Me.Secured_Sticky_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Secured_Sticky_Lbl.TabIndex = 890
        Me.Secured_Sticky_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Back_Color_Lbl
        '
        Me.Sticky_Back_Color_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Back_Color_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Back_Color_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Back_Color_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Back_Color_Lbl.Location = New System.Drawing.Point(0, 238)
        Me.Sticky_Back_Color_Lbl.Name = "Sticky_Back_Color_Lbl"
        Me.Sticky_Back_Color_Lbl.Size = New System.Drawing.Size(145, 24)
        Me.Sticky_Back_Color_Lbl.TabIndex = 889
        Me.Sticky_Back_Color_Lbl.Text = "Sticky Back Color"
        Me.Sticky_Back_Color_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Finished_Sticky_ChkBx
        '
        Me.Finished_Sticky_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Finished_Sticky_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Finished_Sticky_ChkBx.Checked = True
        Me.Finished_Sticky_ChkBx.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.Finished_Sticky_ChkBx.Location = New System.Drawing.Point(2, 47)
        Me.Finished_Sticky_ChkBx.Name = "Finished_Sticky_ChkBx"
        Me.Finished_Sticky_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Finished_Sticky_ChkBx.TabIndex = 650
        Me.Finished_Sticky_ChkBx.Text = "Finished Sticky"
        Me.Finished_Sticky_ChkBx.ThreeState = True
        Me.Finished_Sticky_ChkBx.UseVisualStyleBackColor = False
        '
        'View_Text_Font_Properties_Btn
        '
        Me.View_Text_Font_Properties_Btn.BackgroundImage = CType(resources.GetObject("View_Text_Font_Properties_Btn.BackgroundImage"), System.Drawing.Image)
        Me.View_Text_Font_Properties_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.View_Text_Font_Properties_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.View_Text_Font_Properties_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.View_Text_Font_Properties_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.View_Text_Font_Properties_Btn.Location = New System.Drawing.Point(145, 137)
        Me.View_Text_Font_Properties_Btn.Name = "View_Text_Font_Properties_Btn"
        Me.View_Text_Font_Properties_Btn.Size = New System.Drawing.Size(26, 26)
        Me.View_Text_Font_Properties_Btn.TabIndex = 887
        Me.View_Text_Font_Properties_Btn.TabStop = False
        Me.View_Text_Font_Properties_Btn.UseVisualStyleBackColor = True
        '
        'Blocked_Sticky_ChkBx
        '
        Me.Blocked_Sticky_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Blocked_Sticky_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Blocked_Sticky_ChkBx.Location = New System.Drawing.Point(2, 24)
        Me.Blocked_Sticky_ChkBx.Name = "Blocked_Sticky_ChkBx"
        Me.Blocked_Sticky_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Blocked_Sticky_ChkBx.TabIndex = 648
        Me.Blocked_Sticky_ChkBx.Text = "Blocked Sticky"
        Me.Blocked_Sticky_ChkBx.UseVisualStyleBackColor = False
        '
        'Sticky_Font_TxtBx
        '
        Me.Sticky_Font_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Sticky_Font_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Font_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Font_TxtBx.Location = New System.Drawing.Point(171, 138)
        Me.Sticky_Font_TxtBx.Multiline = True
        Me.Sticky_Font_TxtBx.Name = "Sticky_Font_TxtBx"
        Me.Sticky_Font_TxtBx.ReadOnly = True
        Me.Sticky_Font_TxtBx.Size = New System.Drawing.Size(144, 22)
        Me.Sticky_Font_TxtBx.TabIndex = 885
        Me.Sticky_Font_TxtBx.TabStop = False
        Me.Sticky_Font_TxtBx.Text = "Times New Roman - 12 - Regular"
        '
        'Blocked_Sticky_Lbl
        '
        Me.Blocked_Sticky_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Blocked_Sticky_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Blocked_Sticky_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Blocked_Sticky_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Blocked_Sticky_Lbl.Location = New System.Drawing.Point(0, 23)
        Me.Blocked_Sticky_Lbl.Name = "Blocked_Sticky_Lbl"
        Me.Blocked_Sticky_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Blocked_Sticky_Lbl.TabIndex = 647
        Me.Blocked_Sticky_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Font_Lbl
        '
        Me.Sticky_Font_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Font_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Font_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Font_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Font_Lbl.Location = New System.Drawing.Point(0, 138)
        Me.Sticky_Font_Lbl.Name = "Sticky_Font_Lbl"
        Me.Sticky_Font_Lbl.Size = New System.Drawing.Size(145, 22)
        Me.Sticky_Font_Lbl.TabIndex = 886
        Me.Sticky_Font_Lbl.Text = "Sticky Font"
        Me.Sticky_Font_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Finished_Sticky_Lbl
        '
        Me.Finished_Sticky_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Finished_Sticky_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Finished_Sticky_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Finished_Sticky_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Finished_Sticky_Lbl.Location = New System.Drawing.Point(0, 46)
        Me.Finished_Sticky_Lbl.Name = "Finished_Sticky_Lbl"
        Me.Finished_Sticky_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Finished_Sticky_Lbl.TabIndex = 649
        Me.Finished_Sticky_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Font_Color_Lbl
        '
        Me.Sticky_Font_Color_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Font_Color_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Font_Color_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Font_Color_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Font_Color_Lbl.Location = New System.Drawing.Point(0, 213)
        Me.Sticky_Font_Color_Lbl.Name = "Sticky_Font_Color_Lbl"
        Me.Sticky_Font_Color_Lbl.Size = New System.Drawing.Size(145, 24)
        Me.Sticky_Font_Color_Lbl.TabIndex = 662
        Me.Sticky_Font_Color_Lbl.Text = "Sticky Font Color"
        Me.Sticky_Font_Color_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Parameters_TbPg
        '
        Me.Form_Parameters_TbPg.AutoScroll = True
        Me.Form_Parameters_TbPg.Controls.Add(Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Form_Is_Restricted_By_Screen_Bounds_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Hide_Windows_Desktop_Icons_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Hide_Windows_Desktop_Icons_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Keep_Sticky_Opened_After_Delete_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Keep_Sticky_Opened_After_Delete_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Style_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Size_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Name_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Style_CmbBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Size_CmbBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Name_CmbBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Color_ClrCmbBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Back_Color_ClrCmbBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Back_Color_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.View_External_Text_Font_Properties_Btn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Label5)
        Me.Form_Parameters_TbPg.Controls.Add(Me.External_Sticky_Font_Color_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Application_Starts_Minimized_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Application_Starts_Minimized_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Setting_Tab_Control_Size_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Setting_Tab_Control_Size_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Clear_Previous_Search_Result_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Clear_Previous_Search_Result_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Run_Me_As_Administrator_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Run_Me_As_Administrator_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Reload_Sticky_Note_After_Change_Category_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Reload_Sticky_Note_After_Change_Category_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Cload_Area_Password_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Cload_Area_Password_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Cload_Area_User_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Cload_Area_User_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Cload_Area_Path_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Cload_Area_Path_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Check_For_New_Version_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Check_For_New_Version_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Load_Sticky_Note_At_Startup_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Open_Sticky_In_New_Tab_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Open_Sticky_In_New_Tab_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Stickies_Folder_Path_Btn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Stickies_Folder_Path_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Stickies_Folder_Path_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Color_ClrCmbBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Stop_Displaying_Controls_ToolTip_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Stop_Displaying_Controls_ToolTip_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Me_Always_On_Top_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Me_Always_On_Top_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Remember_Opened_External_Files_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Remember_Opened_External_Files_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Me_As_Default_Text_File_Editor_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Me_As_Default_Text_File_Editor_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Minimize_After_Running_My_Shortcut_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Minimize_After_Running_My_Shortcut_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Me_Is_Compressed_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Me_Is_Compressed_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Show_Sticky_Tab_Control_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Show_Sticky_Tab_Control_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Enable_Maximize_Box_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Enable_Maximize_Box_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Show_Form_Border_Style_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Show_Form_Border_Style_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Double_Click_To_Run_Shortcut_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Double_Click_To_Run_Shortcut_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Warning_Before_Delete_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Warning_Before_Delete_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Warning_Before_Save_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Warning_Before_Save_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Set_Control_To_Fill_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Set_Control_To_Fill_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Main_Password_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Main_Password_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Complex_Password_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Complex_Password_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Enter_Password_To_Pass_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Enter_Password_To_Pass_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Reload_Stickies_After_Amendments_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Reload_Stickies_After_Amendments_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Backup_Folder_Path_Btn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Backup_Folder_Path_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Backup_Folder_Path_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Next_Backup_Time_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Next_Backup_Time_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Minutes_NmrcUpDn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Hours_NmrcUpDn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Days_NmrcUpDn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Backup_Every_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Periodically_Backup_Stickies_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Periodicaly_Backup_Stickies_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Save_Setting_When_Exit_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Save_Setting_When_Exit_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Form_Color_Like_Sticky_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Form_Color_Like_Sticky_Color_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Save_Sticky_Form_Parameter_Setting_Btn)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Opacity_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Size_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Opacity_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Size_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Location_TxtBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Location_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Hide_Finished_Sticky_Note_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Run_Me_At_Windows_Startup_ChkBx)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Hide_Finished_Sticky_Note_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Run_Me_At_Windows_Startup_Lbl)
        Me.Form_Parameters_TbPg.Controls.Add(Me.Sticky_Form_Color_Lbl)
        Me.Form_Parameters_TbPg.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Form_Parameters_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Form_Parameters_TbPg.Name = "Form_Parameters_TbPg"
        Me.Form_Parameters_TbPg.Padding = New System.Windows.Forms.Padding(3)
        Me.Form_Parameters_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Form_Parameters_TbPg.TabIndex = 0
        Me.Form_Parameters_TbPg.Text = "Form Parameters"
        Me.Form_Parameters_TbPg.UseVisualStyleBackColor = True
        '
        'Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx
        '
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.Location = New System.Drawing.Point(318, 254)
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.Name = "Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx"
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.TabIndex = 1089
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.Text = "Ask If Form Is Outside Screen Bounds"
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx.UseVisualStyleBackColor = False
        '
        'Ask_If_Form_Is_Outside_Screen_Bounds_Lbl
        '
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.Location = New System.Drawing.Point(316, 253)
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.Name = "Ask_If_Form_Is_Outside_Screen_Bounds_Lbl"
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.TabIndex = 1088
        Me.Ask_If_Form_Is_Outside_Screen_Bounds_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Is_Restricted_By_Screen_Bounds_ChkBx
        '
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.Location = New System.Drawing.Point(318, 231)
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.Name = "Form_Is_Restricted_By_Screen_Bounds_ChkBx"
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.TabIndex = 1087
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.Text = "Form Is Restricted By Screen Bounds"
        Me.Form_Is_Restricted_By_Screen_Bounds_ChkBx.UseVisualStyleBackColor = False
        '
        'Form_Is_Restricted_By_Screen_Bounds_Lbl
        '
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.Location = New System.Drawing.Point(316, 230)
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.Name = "Form_Is_Restricted_By_Screen_Bounds_Lbl"
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.TabIndex = 1086
        Me.Form_Is_Restricted_By_Screen_Bounds_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Hide_Windows_Desktop_Icons_ChkBx
        '
        Me.Hide_Windows_Desktop_Icons_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Hide_Windows_Desktop_Icons_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Hide_Windows_Desktop_Icons_ChkBx.Location = New System.Drawing.Point(318, 208)
        Me.Hide_Windows_Desktop_Icons_ChkBx.Name = "Hide_Windows_Desktop_Icons_ChkBx"
        Me.Hide_Windows_Desktop_Icons_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Hide_Windows_Desktop_Icons_ChkBx.TabIndex = 1085
        Me.Hide_Windows_Desktop_Icons_ChkBx.Text = "Hide Windows Desktop Icons"
        Me.Hide_Windows_Desktop_Icons_ChkBx.UseVisualStyleBackColor = False
        '
        'Hide_Windows_Desktop_Icons_Lbl
        '
        Me.Hide_Windows_Desktop_Icons_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Hide_Windows_Desktop_Icons_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Hide_Windows_Desktop_Icons_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Hide_Windows_Desktop_Icons_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Hide_Windows_Desktop_Icons_Lbl.Location = New System.Drawing.Point(316, 207)
        Me.Hide_Windows_Desktop_Icons_Lbl.Name = "Hide_Windows_Desktop_Icons_Lbl"
        Me.Hide_Windows_Desktop_Icons_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Hide_Windows_Desktop_Icons_Lbl.TabIndex = 1084
        Me.Hide_Windows_Desktop_Icons_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Keep_Sticky_Opened_After_Delete_ChkBx
        '
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.Location = New System.Drawing.Point(318, 185)
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.Name = "Keep_Sticky_Opened_After_Delete_ChkBx"
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.TabIndex = 1083
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.Text = "Keep Sticky Opened After Delete"
        Me.Keep_Sticky_Opened_After_Delete_ChkBx.UseVisualStyleBackColor = False
        '
        'Keep_Sticky_Opened_After_Delete_Lbl
        '
        Me.Keep_Sticky_Opened_After_Delete_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Keep_Sticky_Opened_After_Delete_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Keep_Sticky_Opened_After_Delete_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Keep_Sticky_Opened_After_Delete_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Keep_Sticky_Opened_After_Delete_Lbl.Location = New System.Drawing.Point(316, 184)
        Me.Keep_Sticky_Opened_After_Delete_Lbl.Name = "Keep_Sticky_Opened_After_Delete_Lbl"
        Me.Keep_Sticky_Opened_After_Delete_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Keep_Sticky_Opened_After_Delete_Lbl.TabIndex = 1082
        Me.Keep_Sticky_Opened_After_Delete_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'External_Sticky_Font_Style_Lbl
        '
        Me.External_Sticky_Font_Style_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.External_Sticky_Font_Style_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.External_Sticky_Font_Style_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.External_Sticky_Font_Style_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.External_Sticky_Font_Style_Lbl.Location = New System.Drawing.Point(316, 437)
        Me.External_Sticky_Font_Style_Lbl.Name = "External_Sticky_Font_Style_Lbl"
        Me.External_Sticky_Font_Style_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.External_Sticky_Font_Style_Lbl.TabIndex = 1081
        Me.External_Sticky_Font_Style_Lbl.Text = "Not Sticky Font Style"
        Me.External_Sticky_Font_Style_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'External_Sticky_Font_Size_Lbl
        '
        Me.External_Sticky_Font_Size_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.External_Sticky_Font_Size_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.External_Sticky_Font_Size_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.External_Sticky_Font_Size_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.External_Sticky_Font_Size_Lbl.Location = New System.Drawing.Point(316, 414)
        Me.External_Sticky_Font_Size_Lbl.Name = "External_Sticky_Font_Size_Lbl"
        Me.External_Sticky_Font_Size_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.External_Sticky_Font_Size_Lbl.TabIndex = 1080
        Me.External_Sticky_Font_Size_Lbl.Text = "Not Sticky Font Size"
        Me.External_Sticky_Font_Size_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'External_Sticky_Font_Name_Lbl
        '
        Me.External_Sticky_Font_Name_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.External_Sticky_Font_Name_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.External_Sticky_Font_Name_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.External_Sticky_Font_Name_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.External_Sticky_Font_Name_Lbl.Location = New System.Drawing.Point(316, 391)
        Me.External_Sticky_Font_Name_Lbl.Name = "External_Sticky_Font_Name_Lbl"
        Me.External_Sticky_Font_Name_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.External_Sticky_Font_Name_Lbl.TabIndex = 1079
        Me.External_Sticky_Font_Name_Lbl.Text = "Not Sticky Font Name"
        Me.External_Sticky_Font_Name_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'External_Sticky_Font_Style_CmbBx
        '
        Me.External_Sticky_Font_Style_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.External_Sticky_Font_Style_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.External_Sticky_Font_Style_CmbBx.FormattingEnabled = True
        Me.External_Sticky_Font_Style_CmbBx.Items.AddRange(New Object() {"Bold", "Bold Italic", "Italic", "Regular", "Strikeout", "Underline"})
        Me.External_Sticky_Font_Style_CmbBx.Location = New System.Drawing.Point(467, 437)
        Me.External_Sticky_Font_Style_CmbBx.Name = "External_Sticky_Font_Style_CmbBx"
        Me.External_Sticky_Font_Style_CmbBx.Size = New System.Drawing.Size(75, 22)
        Me.External_Sticky_Font_Style_CmbBx.Sorted = True
        Me.External_Sticky_Font_Style_CmbBx.TabIndex = 1078
        Me.External_Sticky_Font_Style_CmbBx.TabStop = False
        Me.External_Sticky_Font_Style_CmbBx.Text = "Regular"
        '
        'External_Sticky_Font_Size_CmbBx
        '
        Me.External_Sticky_Font_Size_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.External_Sticky_Font_Size_CmbBx.DropDownWidth = 61
        Me.External_Sticky_Font_Size_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.External_Sticky_Font_Size_CmbBx.FormattingEnabled = True
        Me.External_Sticky_Font_Size_CmbBx.IntegralHeight = False
        Me.External_Sticky_Font_Size_CmbBx.Items.AddRange(New Object() {"10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72", "8", "9"})
        Me.External_Sticky_Font_Size_CmbBx.Location = New System.Drawing.Point(467, 414)
        Me.External_Sticky_Font_Size_CmbBx.Name = "External_Sticky_Font_Size_CmbBx"
        Me.External_Sticky_Font_Size_CmbBx.Size = New System.Drawing.Size(62, 22)
        Me.External_Sticky_Font_Size_CmbBx.TabIndex = 1077
        Me.External_Sticky_Font_Size_CmbBx.TabStop = False
        Me.External_Sticky_Font_Size_CmbBx.Text = "12"
        '
        'External_Sticky_Font_Name_CmbBx
        '
        Me.External_Sticky_Font_Name_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.External_Sticky_Font_Name_CmbBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.External_Sticky_Font_Name_CmbBx.FormattingEnabled = True
        Me.External_Sticky_Font_Name_CmbBx.Location = New System.Drawing.Point(492, 391)
        Me.External_Sticky_Font_Name_CmbBx.Name = "External_Sticky_Font_Name_CmbBx"
        Me.External_Sticky_Font_Name_CmbBx.Size = New System.Drawing.Size(139, 22)
        Me.External_Sticky_Font_Name_CmbBx.Sorted = True
        Me.External_Sticky_Font_Name_CmbBx.TabIndex = 1076
        Me.External_Sticky_Font_Name_CmbBx.TabStop = False
        Me.External_Sticky_Font_Name_CmbBx.Text = "Times New Roman"
        '
        'External_Sticky_Font_Color_ClrCmbBx
        '
        Me.External_Sticky_Font_Color_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.External_Sticky_Font_Color_ClrCmbBx.FormattingEnabled = True
        Me.External_Sticky_Font_Color_ClrCmbBx.IncludeSystemColors = True
        Me.External_Sticky_Font_Color_ClrCmbBx.IncludeTransparent = True
        Me.External_Sticky_Font_Color_ClrCmbBx.Location = New System.Drawing.Point(467, 460)
        Me.External_Sticky_Font_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.External_Sticky_Font_Color_ClrCmbBx.Name = "External_Sticky_Font_Color_ClrCmbBx"
        Me.External_Sticky_Font_Color_ClrCmbBx.Size = New System.Drawing.Size(163, 24)
        Me.External_Sticky_Font_Color_ClrCmbBx.TabIndex = 1074
        '
        'External_Sticky_Back_Color_ClrCmbBx
        '
        Me.External_Sticky_Back_Color_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 10.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.External_Sticky_Back_Color_ClrCmbBx.FormattingEnabled = True
        Me.External_Sticky_Back_Color_ClrCmbBx.IncludeSystemColors = True
        Me.External_Sticky_Back_Color_ClrCmbBx.IncludeTransparent = True
        Me.External_Sticky_Back_Color_ClrCmbBx.Location = New System.Drawing.Point(467, 485)
        Me.External_Sticky_Back_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.External_Sticky_Back_Color_ClrCmbBx.Name = "External_Sticky_Back_Color_ClrCmbBx"
        Me.External_Sticky_Back_Color_ClrCmbBx.Size = New System.Drawing.Size(163, 24)
        Me.External_Sticky_Back_Color_ClrCmbBx.TabIndex = 1075
        '
        'External_Sticky_Back_Color_Lbl
        '
        Me.External_Sticky_Back_Color_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.External_Sticky_Back_Color_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.External_Sticky_Back_Color_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.External_Sticky_Back_Color_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.External_Sticky_Back_Color_Lbl.Location = New System.Drawing.Point(316, 485)
        Me.External_Sticky_Back_Color_Lbl.Name = "External_Sticky_Back_Color_Lbl"
        Me.External_Sticky_Back_Color_Lbl.Size = New System.Drawing.Size(150, 24)
        Me.External_Sticky_Back_Color_Lbl.TabIndex = 1073
        Me.External_Sticky_Back_Color_Lbl.Text = "Not Sticky Back Color"
        Me.External_Sticky_Back_Color_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'View_External_Text_Font_Properties_Btn
        '
        Me.View_External_Text_Font_Properties_Btn.BackgroundImage = CType(resources.GetObject("View_External_Text_Font_Properties_Btn.BackgroundImage"), System.Drawing.Image)
        Me.View_External_Text_Font_Properties_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.View_External_Text_Font_Properties_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.View_External_Text_Font_Properties_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.View_External_Text_Font_Properties_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.View_External_Text_Font_Properties_Btn.Location = New System.Drawing.Point(466, 390)
        Me.View_External_Text_Font_Properties_Btn.Name = "View_External_Text_Font_Properties_Btn"
        Me.View_External_Text_Font_Properties_Btn.Size = New System.Drawing.Size(26, 24)
        Me.View_External_Text_Font_Properties_Btn.TabIndex = 1072
        Me.View_External_Text_Font_Properties_Btn.TabStop = False
        Me.View_External_Text_Font_Properties_Btn.UseVisualStyleBackColor = True
        '
        'External_Sticky_Font_TxtBx
        '
        Me.External_Sticky_Font_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.External_Sticky_Font_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.External_Sticky_Font_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.External_Sticky_Font_TxtBx.Location = New System.Drawing.Point(491, 391)
        Me.External_Sticky_Font_TxtBx.Multiline = True
        Me.External_Sticky_Font_TxtBx.Name = "External_Sticky_Font_TxtBx"
        Me.External_Sticky_Font_TxtBx.ReadOnly = True
        Me.External_Sticky_Font_TxtBx.Size = New System.Drawing.Size(140, 22)
        Me.External_Sticky_Font_TxtBx.TabIndex = 1070
        Me.External_Sticky_Font_TxtBx.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Window
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(316, 391)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(150, 22)
        Me.Label5.TabIndex = 1071
        Me.Label5.Text = "Sticky Font"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'External_Sticky_Font_Color_Lbl
        '
        Me.External_Sticky_Font_Color_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.External_Sticky_Font_Color_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.External_Sticky_Font_Color_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.External_Sticky_Font_Color_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.External_Sticky_Font_Color_Lbl.Location = New System.Drawing.Point(316, 460)
        Me.External_Sticky_Font_Color_Lbl.Name = "External_Sticky_Font_Color_Lbl"
        Me.External_Sticky_Font_Color_Lbl.Size = New System.Drawing.Size(150, 24)
        Me.External_Sticky_Font_Color_Lbl.TabIndex = 1069
        Me.External_Sticky_Font_Color_Lbl.Text = "Not Sticky Font Color"
        Me.External_Sticky_Font_Color_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Application_Starts_Minimized_ChkBx
        '
        Me.Application_Starts_Minimized_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Application_Starts_Minimized_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Application_Starts_Minimized_ChkBx.Location = New System.Drawing.Point(318, 162)
        Me.Application_Starts_Minimized_ChkBx.Name = "Application_Starts_Minimized_ChkBx"
        Me.Application_Starts_Minimized_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Application_Starts_Minimized_ChkBx.TabIndex = 974
        Me.Application_Starts_Minimized_ChkBx.Text = "Application Starts Minimized"
        Me.Application_Starts_Minimized_ChkBx.UseVisualStyleBackColor = False
        '
        'Application_Starts_Minimized_Lbl
        '
        Me.Application_Starts_Minimized_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Application_Starts_Minimized_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Application_Starts_Minimized_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Application_Starts_Minimized_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Application_Starts_Minimized_Lbl.Location = New System.Drawing.Point(316, 161)
        Me.Application_Starts_Minimized_Lbl.Name = "Application_Starts_Minimized_Lbl"
        Me.Application_Starts_Minimized_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Application_Starts_Minimized_Lbl.TabIndex = 973
        Me.Application_Starts_Minimized_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Setting_Tab_Control_Size_Lbl
        '
        Me.Setting_Tab_Control_Size_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Setting_Tab_Control_Size_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Setting_Tab_Control_Size_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Setting_Tab_Control_Size_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Setting_Tab_Control_Size_Lbl.Location = New System.Drawing.Point(316, 368)
        Me.Setting_Tab_Control_Size_Lbl.Name = "Setting_Tab_Control_Size_Lbl"
        Me.Setting_Tab_Control_Size_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Setting_Tab_Control_Size_Lbl.TabIndex = 971
        Me.Setting_Tab_Control_Size_Lbl.Text = "Setting Tab Control Size"
        Me.Setting_Tab_Control_Size_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Setting_Tab_Control_Size_TxtBx
        '
        Me.Setting_Tab_Control_Size_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Setting_Tab_Control_Size_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Setting_Tab_Control_Size_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Setting_Tab_Control_Size_TxtBx.Location = New System.Drawing.Point(467, 368)
        Me.Setting_Tab_Control_Size_TxtBx.Multiline = True
        Me.Setting_Tab_Control_Size_TxtBx.Name = "Setting_Tab_Control_Size_TxtBx"
        Me.Setting_Tab_Control_Size_TxtBx.ReadOnly = True
        Me.Setting_Tab_Control_Size_TxtBx.Size = New System.Drawing.Size(164, 22)
        Me.Setting_Tab_Control_Size_TxtBx.TabIndex = 972
        Me.Setting_Tab_Control_Size_TxtBx.TabStop = False
        '
        'Clear_Previous_Search_Result_ChkBx
        '
        Me.Clear_Previous_Search_Result_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Clear_Previous_Search_Result_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Clear_Previous_Search_Result_ChkBx.Checked = True
        Me.Clear_Previous_Search_Result_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Clear_Previous_Search_Result_ChkBx.Location = New System.Drawing.Point(318, 139)
        Me.Clear_Previous_Search_Result_ChkBx.Name = "Clear_Previous_Search_Result_ChkBx"
        Me.Clear_Previous_Search_Result_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Clear_Previous_Search_Result_ChkBx.TabIndex = 968
        Me.Clear_Previous_Search_Result_ChkBx.Text = "Clear Previous Search Result"
        Me.Clear_Previous_Search_Result_ChkBx.UseVisualStyleBackColor = False
        '
        'Clear_Previous_Search_Result_Lbl
        '
        Me.Clear_Previous_Search_Result_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Clear_Previous_Search_Result_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Clear_Previous_Search_Result_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Clear_Previous_Search_Result_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Clear_Previous_Search_Result_Lbl.Location = New System.Drawing.Point(316, 138)
        Me.Clear_Previous_Search_Result_Lbl.Name = "Clear_Previous_Search_Result_Lbl"
        Me.Clear_Previous_Search_Result_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Clear_Previous_Search_Result_Lbl.TabIndex = 967
        Me.Clear_Previous_Search_Result_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Run_Me_As_Administrator_ChkBx
        '
        Me.Run_Me_As_Administrator_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Run_Me_As_Administrator_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Run_Me_As_Administrator_ChkBx.Enabled = False
        Me.Run_Me_As_Administrator_ChkBx.Location = New System.Drawing.Point(318, 116)
        Me.Run_Me_As_Administrator_ChkBx.Name = "Run_Me_As_Administrator_ChkBx"
        Me.Run_Me_As_Administrator_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Run_Me_As_Administrator_ChkBx.TabIndex = 966
        Me.Run_Me_As_Administrator_ChkBx.Text = "Run Me As Administrator"
        Me.Run_Me_As_Administrator_ChkBx.UseVisualStyleBackColor = False
        '
        'Run_Me_As_Administrator_Lbl
        '
        Me.Run_Me_As_Administrator_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Run_Me_As_Administrator_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Run_Me_As_Administrator_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Run_Me_As_Administrator_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Run_Me_As_Administrator_Lbl.Location = New System.Drawing.Point(316, 115)
        Me.Run_Me_As_Administrator_Lbl.Name = "Run_Me_As_Administrator_Lbl"
        Me.Run_Me_As_Administrator_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Run_Me_As_Administrator_Lbl.TabIndex = 965
        Me.Run_Me_As_Administrator_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Reload_Sticky_Note_After_Change_Category_ChkBx
        '
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.Checked = True
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.Location = New System.Drawing.Point(318, 1)
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.Name = "Reload_Sticky_Note_After_Change_Category_ChkBx"
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.TabIndex = 964
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.Text = "Reload Sticky Note After Change Category"
        Me.Reload_Sticky_Note_After_Change_Category_ChkBx.UseVisualStyleBackColor = False
        '
        'Reload_Sticky_Note_After_Change_Category_Lbl
        '
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.Location = New System.Drawing.Point(316, 0)
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.Name = "Reload_Sticky_Note_After_Change_Category_Lbl"
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.TabIndex = 963
        Me.Reload_Sticky_Note_After_Change_Category_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cload_Area_Password_Lbl
        '
        Me.Cload_Area_Password_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Cload_Area_Password_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cload_Area_Password_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Cload_Area_Password_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cload_Area_Password_Lbl.Location = New System.Drawing.Point(316, 345)
        Me.Cload_Area_Password_Lbl.Name = "Cload_Area_Password_Lbl"
        Me.Cload_Area_Password_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Cload_Area_Password_Lbl.TabIndex = 961
        Me.Cload_Area_Password_Lbl.Text = "Cload Area Password"
        Me.Cload_Area_Password_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cload_Area_Password_TxtBx
        '
        Me.Cload_Area_Password_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Cload_Area_Password_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Cload_Area_Password_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cload_Area_Password_TxtBx.Location = New System.Drawing.Point(467, 345)
        Me.Cload_Area_Password_TxtBx.Multiline = True
        Me.Cload_Area_Password_TxtBx.Name = "Cload_Area_Password_TxtBx"
        Me.Cload_Area_Password_TxtBx.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Cload_Area_Password_TxtBx.Size = New System.Drawing.Size(164, 22)
        Me.Cload_Area_Password_TxtBx.TabIndex = 962
        Me.Cload_Area_Password_TxtBx.UseSystemPasswordChar = True
        '
        'Cload_Area_User_Lbl
        '
        Me.Cload_Area_User_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Cload_Area_User_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cload_Area_User_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Cload_Area_User_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cload_Area_User_Lbl.Location = New System.Drawing.Point(316, 322)
        Me.Cload_Area_User_Lbl.Name = "Cload_Area_User_Lbl"
        Me.Cload_Area_User_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Cload_Area_User_Lbl.TabIndex = 959
        Me.Cload_Area_User_Lbl.Text = "Cload Area User"
        Me.Cload_Area_User_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cload_Area_User_TxtBx
        '
        Me.Cload_Area_User_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Cload_Area_User_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Cload_Area_User_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cload_Area_User_TxtBx.Location = New System.Drawing.Point(467, 322)
        Me.Cload_Area_User_TxtBx.Multiline = True
        Me.Cload_Area_User_TxtBx.Name = "Cload_Area_User_TxtBx"
        Me.Cload_Area_User_TxtBx.Size = New System.Drawing.Size(164, 22)
        Me.Cload_Area_User_TxtBx.TabIndex = 960
        '
        'Cload_Area_Path_Lbl
        '
        Me.Cload_Area_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Cload_Area_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Cload_Area_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Cload_Area_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cload_Area_Path_Lbl.Location = New System.Drawing.Point(316, 299)
        Me.Cload_Area_Path_Lbl.Name = "Cload_Area_Path_Lbl"
        Me.Cload_Area_Path_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Cload_Area_Path_Lbl.TabIndex = 957
        Me.Cload_Area_Path_Lbl.Text = "Cload Area Path"
        Me.Cload_Area_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cload_Area_Path_TxtBx
        '
        Me.Cload_Area_Path_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Cload_Area_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Cload_Area_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cload_Area_Path_TxtBx.Location = New System.Drawing.Point(467, 299)
        Me.Cload_Area_Path_TxtBx.Multiline = True
        Me.Cload_Area_Path_TxtBx.Name = "Cload_Area_Path_TxtBx"
        Me.Cload_Area_Path_TxtBx.Size = New System.Drawing.Size(164, 22)
        Me.Cload_Area_Path_TxtBx.TabIndex = 958
        '
        'Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx
        '
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.Appearance = System.Windows.Forms.Appearance.Button
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.Location = New System.Drawing.Point(317, 277)
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.Name = "Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx"
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.Size = New System.Drawing.Size(313, 20)
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.TabIndex = 956
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.Text = "Save Copy Of Current StickyNote At Cloud Area"
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx.UseVisualStyleBackColor = False
        '
        'Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl
        '
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.Location = New System.Drawing.Point(316, 276)
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.Name = "Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl"
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.TabIndex = 955
        Me.Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Check_For_New_Version_ChkBx
        '
        Me.Check_For_New_Version_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Check_For_New_Version_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Check_For_New_Version_ChkBx.Checked = True
        Me.Check_For_New_Version_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Check_For_New_Version_ChkBx.Location = New System.Drawing.Point(318, 93)
        Me.Check_For_New_Version_ChkBx.Name = "Check_For_New_Version_ChkBx"
        Me.Check_For_New_Version_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Check_For_New_Version_ChkBx.TabIndex = 954
        Me.Check_For_New_Version_ChkBx.Text = "Check For New Version"
        Me.Check_For_New_Version_ChkBx.UseVisualStyleBackColor = False
        '
        'Check_For_New_Version_Lbl
        '
        Me.Check_For_New_Version_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Check_For_New_Version_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Check_For_New_Version_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Check_For_New_Version_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Check_For_New_Version_Lbl.Location = New System.Drawing.Point(316, 92)
        Me.Check_For_New_Version_Lbl.Name = "Check_For_New_Version_Lbl"
        Me.Check_For_New_Version_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Check_For_New_Version_Lbl.TabIndex = 953
        Me.Check_For_New_Version_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Load_Sticky_Note_At_Startup_ChkBx
        '
        Me.Load_Sticky_Note_At_Startup_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Load_Sticky_Note_At_Startup_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Load_Sticky_Note_At_Startup_ChkBx.Checked = True
        Me.Load_Sticky_Note_At_Startup_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Load_Sticky_Note_At_Startup_ChkBx.Location = New System.Drawing.Point(318, 70)
        Me.Load_Sticky_Note_At_Startup_ChkBx.Name = "Load_Sticky_Note_At_Startup_ChkBx"
        Me.Load_Sticky_Note_At_Startup_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Load_Sticky_Note_At_Startup_ChkBx.TabIndex = 952
        Me.Load_Sticky_Note_At_Startup_ChkBx.Text = "Load Sticky Note At Startup"
        Me.Load_Sticky_Note_At_Startup_ChkBx.ThreeState = True
        Me.Load_Sticky_Note_At_Startup_ChkBx.UseVisualStyleBackColor = False
        '
        'Load_Sticky_Note_At_Startup_ChkBx_Lbl
        '
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.Location = New System.Drawing.Point(316, 69)
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.Name = "Load_Sticky_Note_At_Startup_ChkBx_Lbl"
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.TabIndex = 951
        Me.Load_Sticky_Note_At_Startup_ChkBx_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Show_Control_Tab_Pages_In_Multi_Line_ChkBx
        '
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.Checked = True
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.Location = New System.Drawing.Point(318, 47)
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.Name = "Show_Control_Tab_Pages_In_Multi_Line_ChkBx"
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.TabIndex = 950
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.Text = "Show Control Tab Pages In Multi Line"
        Me.Show_Control_Tab_Pages_In_Multi_Line_ChkBx.UseVisualStyleBackColor = False
        '
        'Show_Control_Tab_Pages_In_Multi_Line_Lbl
        '
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.Location = New System.Drawing.Point(316, 46)
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.Name = "Show_Control_Tab_Pages_In_Multi_Line_Lbl"
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.TabIndex = 949
        Me.Show_Control_Tab_Pages_In_Multi_Line_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Open_Sticky_In_New_Tab_ChkBx
        '
        Me.Open_Sticky_In_New_Tab_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Open_Sticky_In_New_Tab_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Open_Sticky_In_New_Tab_ChkBx.Location = New System.Drawing.Point(318, 24)
        Me.Open_Sticky_In_New_Tab_ChkBx.Name = "Open_Sticky_In_New_Tab_ChkBx"
        Me.Open_Sticky_In_New_Tab_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Open_Sticky_In_New_Tab_ChkBx.TabIndex = 948
        Me.Open_Sticky_In_New_Tab_ChkBx.Text = "Open Sticky In New Tab"
        Me.Open_Sticky_In_New_Tab_ChkBx.UseVisualStyleBackColor = False
        '
        'Open_Sticky_In_New_Tab_Lbl
        '
        Me.Open_Sticky_In_New_Tab_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Open_Sticky_In_New_Tab_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Open_Sticky_In_New_Tab_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Open_Sticky_In_New_Tab_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Open_Sticky_In_New_Tab_Lbl.Location = New System.Drawing.Point(316, 23)
        Me.Open_Sticky_In_New_Tab_Lbl.Name = "Open_Sticky_In_New_Tab_Lbl"
        Me.Open_Sticky_In_New_Tab_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Open_Sticky_In_New_Tab_Lbl.TabIndex = 947
        Me.Open_Sticky_In_New_Tab_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Stickies_Folder_Path_Btn
        '
        Me.Stickies_Folder_Path_Btn.BackgroundImage = CType(resources.GetObject("Stickies_Folder_Path_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Stickies_Folder_Path_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Stickies_Folder_Path_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Stickies_Folder_Path_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stickies_Folder_Path_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stickies_Folder_Path_Btn.Location = New System.Drawing.Point(291, 649)
        Me.Stickies_Folder_Path_Btn.Name = "Stickies_Folder_Path_Btn"
        Me.Stickies_Folder_Path_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Stickies_Folder_Path_Btn.TabIndex = 946
        Me.Stickies_Folder_Path_Btn.TabStop = False
        Me.Stickies_Folder_Path_Btn.UseVisualStyleBackColor = True
        '
        'Stickies_Folder_Path_TxtBx
        '
        Me.Stickies_Folder_Path_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Stickies_Folder_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stickies_Folder_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stickies_Folder_Path_TxtBx.Location = New System.Drawing.Point(152, 650)
        Me.Stickies_Folder_Path_TxtBx.Multiline = True
        Me.Stickies_Folder_Path_TxtBx.Name = "Stickies_Folder_Path_TxtBx"
        Me.Stickies_Folder_Path_TxtBx.ReadOnly = True
        Me.Stickies_Folder_Path_TxtBx.Size = New System.Drawing.Size(139, 22)
        Me.Stickies_Folder_Path_TxtBx.TabIndex = 945
        Me.Stickies_Folder_Path_TxtBx.TabStop = False
        '
        'Stickies_Folder_Path_Lbl
        '
        Me.Stickies_Folder_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Stickies_Folder_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Stickies_Folder_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Stickies_Folder_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stickies_Folder_Path_Lbl.Location = New System.Drawing.Point(0, 650)
        Me.Stickies_Folder_Path_Lbl.Name = "Stickies_Folder_Path_Lbl"
        Me.Stickies_Folder_Path_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Stickies_Folder_Path_Lbl.TabIndex = 944
        Me.Stickies_Folder_Path_Lbl.Text = "Stickies Folder Path"
        Me.Stickies_Folder_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Form_Color_ClrCmbBx
        '
        Me.Sticky_Form_Color_ClrCmbBx.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Form_Color_ClrCmbBx.FormattingEnabled = True
        Me.Sticky_Form_Color_ClrCmbBx.IncludeSystemColors = True
        Me.Sticky_Form_Color_ClrCmbBx.IncludeTransparent = True
        Me.Sticky_Form_Color_ClrCmbBx.Location = New System.Drawing.Point(152, 510)
        Me.Sticky_Form_Color_ClrCmbBx.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Sticky_Form_Color_ClrCmbBx.Name = "Sticky_Form_Color_ClrCmbBx"
        Me.Sticky_Form_Color_ClrCmbBx.Size = New System.Drawing.Size(163, 24)
        Me.Sticky_Form_Color_ClrCmbBx.TabIndex = 943
        '
        'Stop_Displaying_Controls_ToolTip_ChkBx
        '
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.Location = New System.Drawing.Point(2, 465)
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.Name = "Stop_Displaying_Controls_ToolTip_ChkBx"
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.TabIndex = 942
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.Text = "Stop Displaying Controls ToolTip"
        Me.Stop_Displaying_Controls_ToolTip_ChkBx.UseVisualStyleBackColor = False
        '
        'Stop_Displaying_Controls_ToolTip_Lbl
        '
        Me.Stop_Displaying_Controls_ToolTip_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Stop_Displaying_Controls_ToolTip_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Stop_Displaying_Controls_ToolTip_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stop_Displaying_Controls_ToolTip_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stop_Displaying_Controls_ToolTip_Lbl.Location = New System.Drawing.Point(0, 464)
        Me.Stop_Displaying_Controls_ToolTip_Lbl.Name = "Stop_Displaying_Controls_ToolTip_Lbl"
        Me.Stop_Displaying_Controls_ToolTip_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Stop_Displaying_Controls_ToolTip_Lbl.TabIndex = 941
        Me.Stop_Displaying_Controls_ToolTip_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Me_Always_On_Top_ChkBx
        '
        Me.Me_Always_On_Top_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Me_Always_On_Top_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Me_Always_On_Top_ChkBx.Location = New System.Drawing.Point(2, 1)
        Me.Me_Always_On_Top_ChkBx.Name = "Me_Always_On_Top_ChkBx"
        Me.Me_Always_On_Top_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Me_Always_On_Top_ChkBx.TabIndex = 940
        Me.Me_Always_On_Top_ChkBx.Text = "Me Always On Top"
        Me.Me_Always_On_Top_ChkBx.UseVisualStyleBackColor = False
        '
        'Me_Always_On_Top_Lbl
        '
        Me.Me_Always_On_Top_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Me_Always_On_Top_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Me_Always_On_Top_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Me_Always_On_Top_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Me_Always_On_Top_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Me_Always_On_Top_Lbl.Name = "Me_Always_On_Top_Lbl"
        Me.Me_Always_On_Top_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Me_Always_On_Top_Lbl.TabIndex = 939
        Me.Me_Always_On_Top_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Remember_Opened_External_Files_ChkBx
        '
        Me.Remember_Opened_External_Files_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Remember_Opened_External_Files_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Remember_Opened_External_Files_ChkBx.Location = New System.Drawing.Point(2, 419)
        Me.Remember_Opened_External_Files_ChkBx.Name = "Remember_Opened_External_Files_ChkBx"
        Me.Remember_Opened_External_Files_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Remember_Opened_External_Files_ChkBx.TabIndex = 938
        Me.Remember_Opened_External_Files_ChkBx.Text = "Remember Opened External Files"
        Me.Remember_Opened_External_Files_ChkBx.UseVisualStyleBackColor = False
        '
        'Remember_Opened_External_Files_Lbl
        '
        Me.Remember_Opened_External_Files_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Remember_Opened_External_Files_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Remember_Opened_External_Files_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Remember_Opened_External_Files_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Remember_Opened_External_Files_Lbl.Location = New System.Drawing.Point(0, 418)
        Me.Remember_Opened_External_Files_Lbl.Name = "Remember_Opened_External_Files_Lbl"
        Me.Remember_Opened_External_Files_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Remember_Opened_External_Files_Lbl.TabIndex = 937
        Me.Remember_Opened_External_Files_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Me_As_Default_Text_File_Editor_ChkBx
        '
        Me.Me_As_Default_Text_File_Editor_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Me_As_Default_Text_File_Editor_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Me_As_Default_Text_File_Editor_ChkBx.Location = New System.Drawing.Point(2, 442)
        Me.Me_As_Default_Text_File_Editor_ChkBx.Name = "Me_As_Default_Text_File_Editor_ChkBx"
        Me.Me_As_Default_Text_File_Editor_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Me_As_Default_Text_File_Editor_ChkBx.TabIndex = 936
        Me.Me_As_Default_Text_File_Editor_ChkBx.Text = "Me As Default Notepad Editor And RTF"
        Me.Me_As_Default_Text_File_Editor_ChkBx.UseVisualStyleBackColor = False
        '
        'Me_As_Default_Text_File_Editor_Lbl
        '
        Me.Me_As_Default_Text_File_Editor_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Me_As_Default_Text_File_Editor_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Me_As_Default_Text_File_Editor_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Me_As_Default_Text_File_Editor_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Me_As_Default_Text_File_Editor_Lbl.Location = New System.Drawing.Point(0, 441)
        Me.Me_As_Default_Text_File_Editor_Lbl.Name = "Me_As_Default_Text_File_Editor_Lbl"
        Me.Me_As_Default_Text_File_Editor_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Me_As_Default_Text_File_Editor_Lbl.TabIndex = 935
        Me.Me_As_Default_Text_File_Editor_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Minimize_After_Running_My_Shortcut_ChkBx
        '
        Me.Minimize_After_Running_My_Shortcut_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Minimize_After_Running_My_Shortcut_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Minimize_After_Running_My_Shortcut_ChkBx.Location = New System.Drawing.Point(2, 394)
        Me.Minimize_After_Running_My_Shortcut_ChkBx.Name = "Minimize_After_Running_My_Shortcut_ChkBx"
        Me.Minimize_After_Running_My_Shortcut_ChkBx.Size = New System.Drawing.Size(308, 22)
        Me.Minimize_After_Running_My_Shortcut_ChkBx.TabIndex = 934
        Me.Minimize_After_Running_My_Shortcut_ChkBx.Text = "Visible After Running My Shortcut"
        Me.Minimize_After_Running_My_Shortcut_ChkBx.ThreeState = True
        Me.Minimize_After_Running_My_Shortcut_ChkBx.UseVisualStyleBackColor = False
        '
        'Minimize_After_Running_My_Shortcut_Lbl
        '
        Me.Minimize_After_Running_My_Shortcut_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Minimize_After_Running_My_Shortcut_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Minimize_After_Running_My_Shortcut_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Minimize_After_Running_My_Shortcut_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Minimize_After_Running_My_Shortcut_Lbl.Location = New System.Drawing.Point(0, 393)
        Me.Minimize_After_Running_My_Shortcut_Lbl.Name = "Minimize_After_Running_My_Shortcut_Lbl"
        Me.Minimize_After_Running_My_Shortcut_Lbl.Size = New System.Drawing.Size(315, 24)
        Me.Minimize_After_Running_My_Shortcut_Lbl.TabIndex = 933
        Me.Minimize_After_Running_My_Shortcut_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Me_Is_Compressed_ChkBx
        '
        Me.Me_Is_Compressed_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Me_Is_Compressed_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Me_Is_Compressed_ChkBx.Checked = True
        Me.Me_Is_Compressed_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Me_Is_Compressed_ChkBx.Location = New System.Drawing.Point(2, 369)
        Me.Me_Is_Compressed_ChkBx.Name = "Me_Is_Compressed_ChkBx"
        Me.Me_Is_Compressed_ChkBx.Size = New System.Drawing.Size(308, 22)
        Me.Me_Is_Compressed_ChkBx.TabIndex = 932
        Me.Me_Is_Compressed_ChkBx.Text = "Me Is Compressed"
        Me.Me_Is_Compressed_ChkBx.UseVisualStyleBackColor = False
        '
        'Me_Is_Compressed_Lbl
        '
        Me.Me_Is_Compressed_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Me_Is_Compressed_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Me_Is_Compressed_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Me_Is_Compressed_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Me_Is_Compressed_Lbl.Location = New System.Drawing.Point(0, 368)
        Me.Me_Is_Compressed_Lbl.Name = "Me_Is_Compressed_Lbl"
        Me.Me_Is_Compressed_Lbl.Size = New System.Drawing.Size(315, 24)
        Me.Me_Is_Compressed_Lbl.TabIndex = 931
        Me.Me_Is_Compressed_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Show_Sticky_Tab_Control_ChkBx
        '
        Me.Show_Sticky_Tab_Control_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Show_Sticky_Tab_Control_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Show_Sticky_Tab_Control_ChkBx.Checked = True
        Me.Show_Sticky_Tab_Control_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Show_Sticky_Tab_Control_ChkBx.Location = New System.Drawing.Point(2, 346)
        Me.Show_Sticky_Tab_Control_ChkBx.Name = "Show_Sticky_Tab_Control_ChkBx"
        Me.Show_Sticky_Tab_Control_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Show_Sticky_Tab_Control_ChkBx.TabIndex = 930
        Me.Show_Sticky_Tab_Control_ChkBx.Text = "Show Sticky Tab Control"
        Me.Show_Sticky_Tab_Control_ChkBx.UseVisualStyleBackColor = False
        '
        'Show_Sticky_Tab_Control_Lbl
        '
        Me.Show_Sticky_Tab_Control_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Show_Sticky_Tab_Control_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Show_Sticky_Tab_Control_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Show_Sticky_Tab_Control_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Show_Sticky_Tab_Control_Lbl.Location = New System.Drawing.Point(0, 345)
        Me.Show_Sticky_Tab_Control_Lbl.Name = "Show_Sticky_Tab_Control_Lbl"
        Me.Show_Sticky_Tab_Control_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Show_Sticky_Tab_Control_Lbl.TabIndex = 929
        Me.Show_Sticky_Tab_Control_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Enable_Maximize_Box_ChkBx
        '
        Me.Enable_Maximize_Box_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Enable_Maximize_Box_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Enable_Maximize_Box_ChkBx.Checked = True
        Me.Enable_Maximize_Box_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Enable_Maximize_Box_ChkBx.Location = New System.Drawing.Point(2, 323)
        Me.Enable_Maximize_Box_ChkBx.Name = "Enable_Maximize_Box_ChkBx"
        Me.Enable_Maximize_Box_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Enable_Maximize_Box_ChkBx.TabIndex = 928
        Me.Enable_Maximize_Box_ChkBx.Text = "Enable Maximize Box"
        Me.Enable_Maximize_Box_ChkBx.UseVisualStyleBackColor = False
        '
        'Enable_Maximize_Box_Lbl
        '
        Me.Enable_Maximize_Box_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Enable_Maximize_Box_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Enable_Maximize_Box_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Enable_Maximize_Box_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Enable_Maximize_Box_Lbl.Location = New System.Drawing.Point(0, 322)
        Me.Enable_Maximize_Box_Lbl.Name = "Enable_Maximize_Box_Lbl"
        Me.Enable_Maximize_Box_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Enable_Maximize_Box_Lbl.TabIndex = 927
        Me.Enable_Maximize_Box_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Show_Form_Border_Style_ChkBx
        '
        Me.Show_Form_Border_Style_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Show_Form_Border_Style_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Show_Form_Border_Style_ChkBx.Checked = True
        Me.Show_Form_Border_Style_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Show_Form_Border_Style_ChkBx.Location = New System.Drawing.Point(2, 300)
        Me.Show_Form_Border_Style_ChkBx.Name = "Show_Form_Border_Style_ChkBx"
        Me.Show_Form_Border_Style_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Show_Form_Border_Style_ChkBx.TabIndex = 926
        Me.Show_Form_Border_Style_ChkBx.Text = "Show Form Border Style"
        Me.Show_Form_Border_Style_ChkBx.UseVisualStyleBackColor = False
        '
        'Show_Form_Border_Style_Lbl
        '
        Me.Show_Form_Border_Style_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Show_Form_Border_Style_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Show_Form_Border_Style_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Show_Form_Border_Style_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Show_Form_Border_Style_Lbl.Location = New System.Drawing.Point(0, 299)
        Me.Show_Form_Border_Style_Lbl.Name = "Show_Form_Border_Style_Lbl"
        Me.Show_Form_Border_Style_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Show_Form_Border_Style_Lbl.TabIndex = 925
        Me.Show_Form_Border_Style_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Double_Click_To_Run_Shortcut_ChkBx
        '
        Me.Double_Click_To_Run_Shortcut_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Double_Click_To_Run_Shortcut_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Double_Click_To_Run_Shortcut_ChkBx.Location = New System.Drawing.Point(2, 277)
        Me.Double_Click_To_Run_Shortcut_ChkBx.Name = "Double_Click_To_Run_Shortcut_ChkBx"
        Me.Double_Click_To_Run_Shortcut_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Double_Click_To_Run_Shortcut_ChkBx.TabIndex = 924
        Me.Double_Click_To_Run_Shortcut_ChkBx.Text = "Double Click To Run Shortcut"
        Me.Double_Click_To_Run_Shortcut_ChkBx.UseVisualStyleBackColor = False
        '
        'Double_Click_To_Run_Shortcut_Lbl
        '
        Me.Double_Click_To_Run_Shortcut_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Double_Click_To_Run_Shortcut_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Double_Click_To_Run_Shortcut_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Double_Click_To_Run_Shortcut_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Double_Click_To_Run_Shortcut_Lbl.Location = New System.Drawing.Point(0, 276)
        Me.Double_Click_To_Run_Shortcut_Lbl.Name = "Double_Click_To_Run_Shortcut_Lbl"
        Me.Double_Click_To_Run_Shortcut_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Double_Click_To_Run_Shortcut_Lbl.TabIndex = 923
        Me.Double_Click_To_Run_Shortcut_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Warning_Before_Delete_ChkBx
        '
        Me.Warning_Before_Delete_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Warning_Before_Delete_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Warning_Before_Delete_ChkBx.Location = New System.Drawing.Point(2, 254)
        Me.Warning_Before_Delete_ChkBx.Name = "Warning_Before_Delete_ChkBx"
        Me.Warning_Before_Delete_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Warning_Before_Delete_ChkBx.TabIndex = 922
        Me.Warning_Before_Delete_ChkBx.Text = "Warning Before Delete"
        Me.Warning_Before_Delete_ChkBx.UseVisualStyleBackColor = False
        '
        'Warning_Before_Delete_Lbl
        '
        Me.Warning_Before_Delete_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Warning_Before_Delete_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Warning_Before_Delete_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Warning_Before_Delete_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Warning_Before_Delete_Lbl.Location = New System.Drawing.Point(0, 253)
        Me.Warning_Before_Delete_Lbl.Name = "Warning_Before_Delete_Lbl"
        Me.Warning_Before_Delete_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Warning_Before_Delete_Lbl.TabIndex = 921
        Me.Warning_Before_Delete_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Warning_Before_Save_ChkBx
        '
        Me.Warning_Before_Save_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Warning_Before_Save_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Warning_Before_Save_ChkBx.Location = New System.Drawing.Point(2, 231)
        Me.Warning_Before_Save_ChkBx.Name = "Warning_Before_Save_ChkBx"
        Me.Warning_Before_Save_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Warning_Before_Save_ChkBx.TabIndex = 920
        Me.Warning_Before_Save_ChkBx.Text = "Warning Before Save"
        Me.Warning_Before_Save_ChkBx.UseVisualStyleBackColor = False
        '
        'Warning_Before_Save_Lbl
        '
        Me.Warning_Before_Save_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Warning_Before_Save_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Warning_Before_Save_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Warning_Before_Save_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Warning_Before_Save_Lbl.Location = New System.Drawing.Point(0, 230)
        Me.Warning_Before_Save_Lbl.Name = "Warning_Before_Save_Lbl"
        Me.Warning_Before_Save_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Warning_Before_Save_Lbl.TabIndex = 919
        Me.Warning_Before_Save_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Set_Control_To_Fill_ChkBx
        '
        Me.Set_Control_To_Fill_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Set_Control_To_Fill_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Set_Control_To_Fill_ChkBx.Checked = True
        Me.Set_Control_To_Fill_ChkBx.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Set_Control_To_Fill_ChkBx.Location = New System.Drawing.Point(2, 208)
        Me.Set_Control_To_Fill_ChkBx.Name = "Set_Control_To_Fill_ChkBx"
        Me.Set_Control_To_Fill_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Set_Control_To_Fill_ChkBx.TabIndex = 918
        Me.Set_Control_To_Fill_ChkBx.Text = "Set Control To Fill"
        Me.Set_Control_To_Fill_ChkBx.UseVisualStyleBackColor = False
        '
        'Set_Control_To_Fill_Lbl
        '
        Me.Set_Control_To_Fill_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Set_Control_To_Fill_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Set_Control_To_Fill_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Set_Control_To_Fill_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Set_Control_To_Fill_Lbl.Location = New System.Drawing.Point(0, 207)
        Me.Set_Control_To_Fill_Lbl.Name = "Set_Control_To_Fill_Lbl"
        Me.Set_Control_To_Fill_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Set_Control_To_Fill_Lbl.TabIndex = 917
        Me.Set_Control_To_Fill_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Main_Password_Lbl
        '
        Me.Main_Password_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Main_Password_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Main_Password_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Main_Password_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Main_Password_Lbl.Location = New System.Drawing.Point(0, 487)
        Me.Main_Password_Lbl.Name = "Main_Password_Lbl"
        Me.Main_Password_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Main_Password_Lbl.TabIndex = 915
        Me.Main_Password_Lbl.Text = "Main Password"
        Me.Main_Password_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Main_Password_TxtBx
        '
        Me.Main_Password_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Main_Password_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Main_Password_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Main_Password_TxtBx.Location = New System.Drawing.Point(152, 487)
        Me.Main_Password_TxtBx.Name = "Main_Password_TxtBx"
        Me.Main_Password_TxtBx.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Main_Password_TxtBx.ReadOnly = True
        Me.Main_Password_TxtBx.Size = New System.Drawing.Size(163, 22)
        Me.Main_Password_TxtBx.TabIndex = 916
        Me.Main_Password_TxtBx.TabStop = False
        Me.Main_Password_TxtBx.UseSystemPasswordChar = True
        '
        'Complex_Password_ChkBx
        '
        Me.Complex_Password_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Complex_Password_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Complex_Password_ChkBx.Location = New System.Drawing.Point(2, 185)
        Me.Complex_Password_ChkBx.Name = "Complex_Password_ChkBx"
        Me.Complex_Password_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Complex_Password_ChkBx.TabIndex = 914
        Me.Complex_Password_ChkBx.Text = "Complex Password"
        Me.Complex_Password_ChkBx.UseVisualStyleBackColor = False
        '
        'Complex_Password_Lbl
        '
        Me.Complex_Password_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Complex_Password_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Complex_Password_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Complex_Password_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Complex_Password_Lbl.Location = New System.Drawing.Point(0, 184)
        Me.Complex_Password_Lbl.Name = "Complex_Password_Lbl"
        Me.Complex_Password_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Complex_Password_Lbl.TabIndex = 913
        Me.Complex_Password_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Enter_Password_To_Pass_ChkBx
        '
        Me.Enter_Password_To_Pass_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Enter_Password_To_Pass_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Enter_Password_To_Pass_ChkBx.Location = New System.Drawing.Point(2, 162)
        Me.Enter_Password_To_Pass_ChkBx.Name = "Enter_Password_To_Pass_ChkBx"
        Me.Enter_Password_To_Pass_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Enter_Password_To_Pass_ChkBx.TabIndex = 912
        Me.Enter_Password_To_Pass_ChkBx.Text = "Enter Password To Pass"
        Me.Enter_Password_To_Pass_ChkBx.UseVisualStyleBackColor = False
        '
        'Enter_Password_To_Pass_Lbl
        '
        Me.Enter_Password_To_Pass_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Enter_Password_To_Pass_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Enter_Password_To_Pass_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Enter_Password_To_Pass_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Enter_Password_To_Pass_Lbl.Location = New System.Drawing.Point(0, 161)
        Me.Enter_Password_To_Pass_Lbl.Name = "Enter_Password_To_Pass_Lbl"
        Me.Enter_Password_To_Pass_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Enter_Password_To_Pass_Lbl.TabIndex = 911
        Me.Enter_Password_To_Pass_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Reload_Stickies_After_Amendments_ChkBx
        '
        Me.Reload_Stickies_After_Amendments_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Reload_Stickies_After_Amendments_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Reload_Stickies_After_Amendments_ChkBx.Location = New System.Drawing.Point(2, 139)
        Me.Reload_Stickies_After_Amendments_ChkBx.Name = "Reload_Stickies_After_Amendments_ChkBx"
        Me.Reload_Stickies_After_Amendments_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Reload_Stickies_After_Amendments_ChkBx.TabIndex = 910
        Me.Reload_Stickies_After_Amendments_ChkBx.Text = "Reload Stickies After Amendments"
        Me.Reload_Stickies_After_Amendments_ChkBx.UseVisualStyleBackColor = False
        '
        'Reload_Stickies_After_Amendments_Lbl
        '
        Me.Reload_Stickies_After_Amendments_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Reload_Stickies_After_Amendments_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Reload_Stickies_After_Amendments_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Reload_Stickies_After_Amendments_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Reload_Stickies_After_Amendments_Lbl.Location = New System.Drawing.Point(0, 138)
        Me.Reload_Stickies_After_Amendments_Lbl.Name = "Reload_Stickies_After_Amendments_Lbl"
        Me.Reload_Stickies_After_Amendments_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Reload_Stickies_After_Amendments_Lbl.TabIndex = 909
        Me.Reload_Stickies_After_Amendments_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Backup_Folder_Path_Btn
        '
        Me.Backup_Folder_Path_Btn.BackgroundImage = CType(resources.GetObject("Backup_Folder_Path_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Backup_Folder_Path_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Backup_Folder_Path_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Backup_Folder_Path_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Backup_Folder_Path_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Backup_Folder_Path_Btn.Location = New System.Drawing.Point(291, 672)
        Me.Backup_Folder_Path_Btn.Name = "Backup_Folder_Path_Btn"
        Me.Backup_Folder_Path_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Backup_Folder_Path_Btn.TabIndex = 908
        Me.Backup_Folder_Path_Btn.TabStop = False
        Me.Backup_Folder_Path_Btn.UseVisualStyleBackColor = True
        '
        'Backup_Folder_Path_TxtBx
        '
        Me.Backup_Folder_Path_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Backup_Folder_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Backup_Folder_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Backup_Folder_Path_TxtBx.Location = New System.Drawing.Point(152, 673)
        Me.Backup_Folder_Path_TxtBx.Multiline = True
        Me.Backup_Folder_Path_TxtBx.Name = "Backup_Folder_Path_TxtBx"
        Me.Backup_Folder_Path_TxtBx.ReadOnly = True
        Me.Backup_Folder_Path_TxtBx.Size = New System.Drawing.Size(139, 22)
        Me.Backup_Folder_Path_TxtBx.TabIndex = 907
        Me.Backup_Folder_Path_TxtBx.TabStop = False
        '
        'Backup_Folder_Path_Lbl
        '
        Me.Backup_Folder_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Backup_Folder_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Backup_Folder_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Backup_Folder_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Backup_Folder_Path_Lbl.Location = New System.Drawing.Point(0, 673)
        Me.Backup_Folder_Path_Lbl.Name = "Backup_Folder_Path_Lbl"
        Me.Backup_Folder_Path_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Backup_Folder_Path_Lbl.TabIndex = 906
        Me.Backup_Folder_Path_Lbl.Text = "Backup Folder Path"
        Me.Backup_Folder_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Next_Backup_Time_TxtBx
        '
        Me.Next_Backup_Time_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Next_Backup_Time_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Next_Backup_Time_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Next_Backup_Time_TxtBx.Location = New System.Drawing.Point(152, 627)
        Me.Next_Backup_Time_TxtBx.Multiline = True
        Me.Next_Backup_Time_TxtBx.Name = "Next_Backup_Time_TxtBx"
        Me.Next_Backup_Time_TxtBx.ReadOnly = True
        Me.Next_Backup_Time_TxtBx.Size = New System.Drawing.Size(163, 22)
        Me.Next_Backup_Time_TxtBx.TabIndex = 905
        Me.Next_Backup_Time_TxtBx.TabStop = False
        '
        'Next_Backup_Time_Lbl
        '
        Me.Next_Backup_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Next_Backup_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Next_Backup_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Next_Backup_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Next_Backup_Time_Lbl.Location = New System.Drawing.Point(0, 627)
        Me.Next_Backup_Time_Lbl.Name = "Next_Backup_Time_Lbl"
        Me.Next_Backup_Time_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Next_Backup_Time_Lbl.TabIndex = 904
        Me.Next_Backup_Time_Lbl.Text = "Next Backup Time"
        Me.Next_Backup_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Backup_Every_Lbl
        '
        Me.Backup_Every_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Backup_Every_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Backup_Every_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Backup_Every_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Backup_Every_Lbl.Location = New System.Drawing.Point(0, 604)
        Me.Backup_Every_Lbl.Name = "Backup_Every_Lbl"
        Me.Backup_Every_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Backup_Every_Lbl.TabIndex = 900
        Me.Backup_Every_Lbl.Text = "Backup Every"
        Me.Backup_Every_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Periodically_Backup_Stickies_ChkBx
        '
        Me.Periodically_Backup_Stickies_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Periodically_Backup_Stickies_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Periodically_Backup_Stickies_ChkBx.Location = New System.Drawing.Point(2, 116)
        Me.Periodically_Backup_Stickies_ChkBx.Name = "Periodically_Backup_Stickies_ChkBx"
        Me.Periodically_Backup_Stickies_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Periodically_Backup_Stickies_ChkBx.TabIndex = 899
        Me.Periodically_Backup_Stickies_ChkBx.Text = "Periodically Backup Stickies"
        Me.Periodically_Backup_Stickies_ChkBx.UseVisualStyleBackColor = False
        '
        'Periodicaly_Backup_Stickies_Lbl
        '
        Me.Periodicaly_Backup_Stickies_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Periodicaly_Backup_Stickies_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Periodicaly_Backup_Stickies_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Periodicaly_Backup_Stickies_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Periodicaly_Backup_Stickies_Lbl.Location = New System.Drawing.Point(0, 115)
        Me.Periodicaly_Backup_Stickies_Lbl.Name = "Periodicaly_Backup_Stickies_Lbl"
        Me.Periodicaly_Backup_Stickies_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Periodicaly_Backup_Stickies_Lbl.TabIndex = 898
        Me.Periodicaly_Backup_Stickies_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Save_Setting_When_Exit_ChkBx
        '
        Me.Save_Setting_When_Exit_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Save_Setting_When_Exit_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_Setting_When_Exit_ChkBx.Location = New System.Drawing.Point(2, 93)
        Me.Save_Setting_When_Exit_ChkBx.Name = "Save_Setting_When_Exit_ChkBx"
        Me.Save_Setting_When_Exit_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Save_Setting_When_Exit_ChkBx.TabIndex = 897
        Me.Save_Setting_When_Exit_ChkBx.Text = "Save Setting When Exit"
        Me.Save_Setting_When_Exit_ChkBx.UseVisualStyleBackColor = False
        '
        'Save_Setting_When_Exit_Lbl
        '
        Me.Save_Setting_When_Exit_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Save_Setting_When_Exit_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Save_Setting_When_Exit_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Save_Setting_When_Exit_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Save_Setting_When_Exit_Lbl.Location = New System.Drawing.Point(0, 92)
        Me.Save_Setting_When_Exit_Lbl.Name = "Save_Setting_When_Exit_Lbl"
        Me.Save_Setting_When_Exit_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Save_Setting_When_Exit_Lbl.TabIndex = 896
        Me.Save_Setting_When_Exit_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form_Color_Like_Sticky_ChkBx
        '
        Me.Form_Color_Like_Sticky_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Form_Color_Like_Sticky_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Form_Color_Like_Sticky_ChkBx.Location = New System.Drawing.Point(2, 70)
        Me.Form_Color_Like_Sticky_ChkBx.Name = "Form_Color_Like_Sticky_ChkBx"
        Me.Form_Color_Like_Sticky_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Form_Color_Like_Sticky_ChkBx.TabIndex = 895
        Me.Form_Color_Like_Sticky_ChkBx.Text = "Form Color Like Sticky"
        Me.Form_Color_Like_Sticky_ChkBx.UseVisualStyleBackColor = False
        '
        'Form_Color_Like_Sticky_Color_Lbl
        '
        Me.Form_Color_Like_Sticky_Color_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Form_Color_Like_Sticky_Color_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Form_Color_Like_Sticky_Color_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Form_Color_Like_Sticky_Color_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Form_Color_Like_Sticky_Color_Lbl.Location = New System.Drawing.Point(0, 69)
        Me.Form_Color_Like_Sticky_Color_Lbl.Name = "Form_Color_Like_Sticky_Color_Lbl"
        Me.Form_Color_Like_Sticky_Color_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Form_Color_Like_Sticky_Color_Lbl.TabIndex = 894
        Me.Form_Color_Like_Sticky_Color_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Form_Opacity_TxtBx
        '
        Me.Sticky_Form_Opacity_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Sticky_Form_Opacity_TxtBx.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Form_Opacity_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Opacity_TxtBx.Location = New System.Drawing.Point(152, 581)
        Me.Sticky_Form_Opacity_TxtBx.Multiline = True
        Me.Sticky_Form_Opacity_TxtBx.Name = "Sticky_Form_Opacity_TxtBx"
        Me.Sticky_Form_Opacity_TxtBx.ReadOnly = True
        Me.Sticky_Form_Opacity_TxtBx.Size = New System.Drawing.Size(163, 22)
        Me.Sticky_Form_Opacity_TxtBx.TabIndex = 893
        Me.Sticky_Form_Opacity_TxtBx.TabStop = False
        '
        'Sticky_Form_Size_Lbl
        '
        Me.Sticky_Form_Size_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Form_Size_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Form_Size_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Form_Size_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Size_Lbl.Location = New System.Drawing.Point(0, 535)
        Me.Sticky_Form_Size_Lbl.Name = "Sticky_Form_Size_Lbl"
        Me.Sticky_Form_Size_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Sticky_Form_Size_Lbl.TabIndex = 888
        Me.Sticky_Form_Size_Lbl.Text = "Sticky Form Size"
        Me.Sticky_Form_Size_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Form_Opacity_Lbl
        '
        Me.Sticky_Form_Opacity_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Form_Opacity_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Form_Opacity_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Form_Opacity_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Opacity_Lbl.Location = New System.Drawing.Point(0, 581)
        Me.Sticky_Form_Opacity_Lbl.Name = "Sticky_Form_Opacity_Lbl"
        Me.Sticky_Form_Opacity_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Sticky_Form_Opacity_Lbl.TabIndex = 892
        Me.Sticky_Form_Opacity_Lbl.Text = "Sticky Form Opacity"
        Me.Sticky_Form_Opacity_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Form_Size_TxtBx
        '
        Me.Sticky_Form_Size_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Sticky_Form_Size_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Form_Size_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Size_TxtBx.Location = New System.Drawing.Point(152, 535)
        Me.Sticky_Form_Size_TxtBx.Multiline = True
        Me.Sticky_Form_Size_TxtBx.Name = "Sticky_Form_Size_TxtBx"
        Me.Sticky_Form_Size_TxtBx.ReadOnly = True
        Me.Sticky_Form_Size_TxtBx.Size = New System.Drawing.Size(163, 22)
        Me.Sticky_Form_Size_TxtBx.TabIndex = 889
        Me.Sticky_Form_Size_TxtBx.TabStop = False
        '
        'Sticky_Form_Location_TxtBx
        '
        Me.Sticky_Form_Location_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Sticky_Form_Location_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sticky_Form_Location_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Location_TxtBx.Location = New System.Drawing.Point(152, 558)
        Me.Sticky_Form_Location_TxtBx.Multiline = True
        Me.Sticky_Form_Location_TxtBx.Name = "Sticky_Form_Location_TxtBx"
        Me.Sticky_Form_Location_TxtBx.ReadOnly = True
        Me.Sticky_Form_Location_TxtBx.Size = New System.Drawing.Size(163, 22)
        Me.Sticky_Form_Location_TxtBx.TabIndex = 891
        Me.Sticky_Form_Location_TxtBx.TabStop = False
        '
        'Sticky_Form_Location_Lbl
        '
        Me.Sticky_Form_Location_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Form_Location_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Form_Location_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Form_Location_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Location_Lbl.Location = New System.Drawing.Point(0, 558)
        Me.Sticky_Form_Location_Lbl.Name = "Sticky_Form_Location_Lbl"
        Me.Sticky_Form_Location_Lbl.Size = New System.Drawing.Size(150, 22)
        Me.Sticky_Form_Location_Lbl.TabIndex = 890
        Me.Sticky_Form_Location_Lbl.Text = "Sticky Form Location"
        Me.Sticky_Form_Location_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Hide_Finished_Sticky_Note_ChkBx
        '
        Me.Hide_Finished_Sticky_Note_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Hide_Finished_Sticky_Note_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Hide_Finished_Sticky_Note_ChkBx.Location = New System.Drawing.Point(2, 24)
        Me.Hide_Finished_Sticky_Note_ChkBx.Name = "Hide_Finished_Sticky_Note_ChkBx"
        Me.Hide_Finished_Sticky_Note_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Hide_Finished_Sticky_Note_ChkBx.TabIndex = 653
        Me.Hide_Finished_Sticky_Note_ChkBx.Text = "Hide Finished Sticky Note"
        Me.Hide_Finished_Sticky_Note_ChkBx.UseVisualStyleBackColor = False
        '
        'Run_Me_At_Windows_Startup_ChkBx
        '
        Me.Run_Me_At_Windows_Startup_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Run_Me_At_Windows_Startup_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Run_Me_At_Windows_Startup_ChkBx.Location = New System.Drawing.Point(2, 47)
        Me.Run_Me_At_Windows_Startup_ChkBx.Name = "Run_Me_At_Windows_Startup_ChkBx"
        Me.Run_Me_At_Windows_Startup_ChkBx.Size = New System.Drawing.Size(308, 20)
        Me.Run_Me_At_Windows_Startup_ChkBx.TabIndex = 657
        Me.Run_Me_At_Windows_Startup_ChkBx.Text = "Run Me At Windows Startup"
        Me.Run_Me_At_Windows_Startup_ChkBx.UseVisualStyleBackColor = False
        '
        'Hide_Finished_Sticky_Note_Lbl
        '
        Me.Hide_Finished_Sticky_Note_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Hide_Finished_Sticky_Note_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Hide_Finished_Sticky_Note_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Hide_Finished_Sticky_Note_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Hide_Finished_Sticky_Note_Lbl.Location = New System.Drawing.Point(0, 23)
        Me.Hide_Finished_Sticky_Note_Lbl.Name = "Hide_Finished_Sticky_Note_Lbl"
        Me.Hide_Finished_Sticky_Note_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Hide_Finished_Sticky_Note_Lbl.TabIndex = 652
        Me.Hide_Finished_Sticky_Note_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Run_Me_At_Windows_Startup_Lbl
        '
        Me.Run_Me_At_Windows_Startup_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Run_Me_At_Windows_Startup_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Run_Me_At_Windows_Startup_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Run_Me_At_Windows_Startup_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Run_Me_At_Windows_Startup_Lbl.Location = New System.Drawing.Point(0, 46)
        Me.Run_Me_At_Windows_Startup_Lbl.Name = "Run_Me_At_Windows_Startup_Lbl"
        Me.Run_Me_At_Windows_Startup_Lbl.Size = New System.Drawing.Size(315, 22)
        Me.Run_Me_At_Windows_Startup_Lbl.TabIndex = 656
        Me.Run_Me_At_Windows_Startup_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sticky_Form_Color_Lbl
        '
        Me.Sticky_Form_Color_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sticky_Form_Color_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sticky_Form_Color_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Sticky_Form_Color_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sticky_Form_Color_Lbl.Location = New System.Drawing.Point(0, 510)
        Me.Sticky_Form_Color_Lbl.Name = "Sticky_Form_Color_Lbl"
        Me.Sticky_Form_Color_Lbl.Size = New System.Drawing.Size(150, 24)
        Me.Sticky_Form_Color_Lbl.TabIndex = 659
        Me.Sticky_Form_Color_Lbl.Text = "Sticky Form Color"
        Me.Sticky_Form_Color_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Shortcuts_TbPg
        '
        Me.Shortcuts_TbPg.Controls.Add(Me.ListView2)
        Me.Shortcuts_TbPg.Controls.Add(Me.ShortCut_TbCntrl)
        Me.Shortcuts_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Shortcuts_TbPg.Name = "Shortcuts_TbPg"
        Me.Shortcuts_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Shortcuts_TbPg.TabIndex = 3
        Me.Shortcuts_TbPg.Text = "Shortcuts"
        Me.Shortcuts_TbPg.UseVisualStyleBackColor = True
        '
        'ListView2
        '
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(1, 30)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(136, 108)
        Me.ListView2.TabIndex = 0
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.Visible = False
        '
        'ShortCut_TbCntrl
        '
        Me.ShortCut_TbCntrl.AllowDrop = True
        Me.ShortCut_TbCntrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ShortCut_TbCntrl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.ShortCut_TbCntrl.Location = New System.Drawing.Point(0, 0)
        Me.ShortCut_TbCntrl.Name = "ShortCut_TbCntrl"
        Me.ShortCut_TbCntrl.SelectedIndex = 0
        Me.ShortCut_TbCntrl.Size = New System.Drawing.Size(1013, 198)
        Me.ShortCut_TbCntrl.TabIndex = 1
        '
        'Prayer_Time_TbPg
        '
        Me.Prayer_Time_TbPg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Prayer_Time_TbPg.Controls.Add(Me.Clock2)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Time_Left_For_Alert_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Select_Alert_File_Path_Btn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Alert_File_Path_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Upload_Last_Version_Btn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Alert_File_Path_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Time_To_Alert_Before_Azan_NmrcUpDn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Time_To_Alert_Before_Azan_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Alert_Before_Azan_ChkBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Alert_Before_Azan_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Azan_Takbeer_Only_ChkBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Azan_Takbeer_Only_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Azan_Activation_ChkBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Azan_Activation_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Stop_Fagr_Voice_File_Btn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Play_Fagr_Voice_File_Btn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Azan_Spoke_Method_ChkBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Azan_Spoke_Method_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Voice_Azan_Files_CmbBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Fagr_Voice_Files_CmbBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Stop_Voice_Azan_File_Btn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Play_Voice_Azan_File_Btn)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Save_Day_Light_ChkBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Save_Day_Light_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Left_Time_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Date_DtTmPkr)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Date_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Longitude_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Longitude_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Latitude_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Latitude_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.City_CmbBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.City_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Country_CmbBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Country_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Calculation_Methods_CmbBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Calculation_Methods_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Isha_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Maghrib_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Asr_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Dhuhr_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Sunrise_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Fajr_TxtBx)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Isha_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Maghrib_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Asr_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Dhuhr_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Sunrise_Lbl)
        Me.Prayer_Time_TbPg.Controls.Add(Me.Fajr_Lbl)
        Me.Prayer_Time_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Prayer_Time_TbPg.Name = "Prayer_Time_TbPg"
        Me.Prayer_Time_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Prayer_Time_TbPg.TabIndex = 4
        Me.Prayer_Time_TbPg.Text = "Prayer Time"
        Me.Prayer_Time_TbPg.UseVisualStyleBackColor = True
        '
        'Clock2
        '
        Me.Clock2.BackColor = System.Drawing.Color.Transparent
        Me.Clock2.BigMarkers = New AnalogClock.Marker() {New AnalogClock.Marker("BigMarker90", 90.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker60", 60.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker30", 30.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker0", 0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker330", 330.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker300", 300.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker270", 270.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker240", 240.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker210", 210.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker180", 180.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker150", 150.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker120", 120.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing)}
        Me.Clock2.CenterPoint.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock2.CenterPoint.RelativeRadius = 0.03!
        Me.Clock2.CenterPoint.Tag = Nothing
        Me.Clock2.ForeColor = System.Drawing.Color.Black
        Me.Clock2.HourHand.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock2.HourHand.RelativeRadius = 0.65!
        Me.Clock2.HourHand.Tag = Nothing
        Me.Clock2.HourHand.Width = 5.0!
        Me.Clock2.Location = New System.Drawing.Point(855, 41)
        Me.Clock2.MinuteHand.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock2.MinuteHand.RelativeRadius = 0.8!
        Me.Clock2.MinuteHand.Tag = Nothing
        Me.Clock2.MinuteHand.Width = 5.0!
        Me.Clock2.Name = "Clock2"
        Me.Clock2.SecondHand.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock2.SecondHand.RelativeRadius = 0.9!
        Me.Clock2.SecondHand.Tag = Nothing
        Me.Clock2.SecondHand.Width = 1.0!
        Me.Clock2.Size = New System.Drawing.Size(136, 137)
        Me.Clock2.SmallMarkers = New AnalogClock.Marker() {New AnalogClock.Marker("SmallMarker90", 90.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker84", 84.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker78", 78.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker72", 72.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker66", 66.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker60", 60.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker54", 54.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker48", 48.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker42", 42.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker36", 36.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker30", 30.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker24", 24.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker18", 18.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker12", 12.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker6", 6.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker0", 0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker354", 354.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker348", 348.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker342", 342.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker336", 336.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker330", 330.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker324", 324.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker318", 318.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker312", 312.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker306", 306.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker300", 300.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker294", 294.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker288", 288.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker282", 282.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker276", 276.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker270", 270.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker264", 264.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker258", 258.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker252", 252.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker246", 246.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker240", 240.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker234", 234.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker228", 228.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker222", 222.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker216", 216.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker210", 210.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker204", 204.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker198", 198.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker192", 192.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker186", 186.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker180", 180.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker174", 174.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker168", 168.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker162", 162.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker156", 156.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker150", 150.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker144", 144.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker138", 138.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker132", 132.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker126", 126.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker120", 120.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker114", 114.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker108", 108.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker102", 102.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker96", 96.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 68.0!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing)}
        Me.Clock2.Symbols = New AnalogClock.Symbol() {New AnalogClock.Symbol("Symbol90", 90.0!, "12", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 0, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol60", 60.0!, "1", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 1, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol30", 30.0!, "2", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 2, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol0", 0!, "3", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 3, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol330", 330.0!, "4", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 4, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol300", 300.0!, "5", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 5, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol270", 270.0!, "6", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 6, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol240", 240.0!, "7", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 7, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol210", 210.0!, "8", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 8, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol180", 180.0!, "9", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 9, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol150", 150.0!, "10", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 10, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol120", 120.0!, "11", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 11, True, True, AnalogClock.SymbolStyle.Numeric, 68.0!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing)}
        Me.Clock2.TabIndex = 1101
        '
        'Time_Left_For_Alert_TxtBx
        '
        Me.Time_Left_For_Alert_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Time_Left_For_Alert_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Time_Left_For_Alert_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Time_Left_For_Alert_TxtBx.Location = New System.Drawing.Point(720, 146)
        Me.Time_Left_For_Alert_TxtBx.Multiline = True
        Me.Time_Left_For_Alert_TxtBx.Name = "Time_Left_For_Alert_TxtBx"
        Me.Time_Left_For_Alert_TxtBx.ReadOnly = True
        Me.Time_Left_For_Alert_TxtBx.Size = New System.Drawing.Size(69, 23)
        Me.Time_Left_For_Alert_TxtBx.TabIndex = 1099
        Me.Time_Left_For_Alert_TxtBx.TabStop = False
        '
        'Select_Alert_File_Path_Btn
        '
        Me.Select_Alert_File_Path_Btn.BackgroundImage = CType(resources.GetObject("Select_Alert_File_Path_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Select_Alert_File_Path_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Select_Alert_File_Path_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Select_Alert_File_Path_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Select_Alert_File_Path_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Select_Alert_File_Path_Btn.Location = New System.Drawing.Point(810, 169)
        Me.Select_Alert_File_Path_Btn.Name = "Select_Alert_File_Path_Btn"
        Me.Select_Alert_File_Path_Btn.Size = New System.Drawing.Size(26, 25)
        Me.Select_Alert_File_Path_Btn.TabIndex = 1098
        Me.Select_Alert_File_Path_Btn.TabStop = False
        Me.Select_Alert_File_Path_Btn.UseVisualStyleBackColor = True
        '
        'Alert_File_Path_TxtBx
        '
        Me.Alert_File_Path_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Alert_File_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_File_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_File_Path_TxtBx.Location = New System.Drawing.Point(602, 170)
        Me.Alert_File_Path_TxtBx.Multiline = True
        Me.Alert_File_Path_TxtBx.Name = "Alert_File_Path_TxtBx"
        Me.Alert_File_Path_TxtBx.ReadOnly = True
        Me.Alert_File_Path_TxtBx.Size = New System.Drawing.Size(208, 23)
        Me.Alert_File_Path_TxtBx.TabIndex = 1097
        Me.Alert_File_Path_TxtBx.TabStop = False
        '
        'Upload_Last_Version_Btn
        '
        Me.Upload_Last_Version_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Upload_Last_Version_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Upload_Last_Version_Btn.Location = New System.Drawing.Point(841, 4)
        Me.Upload_Last_Version_Btn.Name = "Upload_Last_Version_Btn"
        Me.Upload_Last_Version_Btn.Size = New System.Drawing.Size(127, 26)
        Me.Upload_Last_Version_Btn.TabIndex = 1064
        Me.Upload_Last_Version_Btn.Text = "Upload Last Version"
        Me.Upload_Last_Version_Btn.UseVisualStyleBackColor = True
        '
        'Alert_File_Path_Lbl
        '
        Me.Alert_File_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_File_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_File_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_File_Path_Lbl.Location = New System.Drawing.Point(520, 170)
        Me.Alert_File_Path_Lbl.Name = "Alert_File_Path_Lbl"
        Me.Alert_File_Path_Lbl.Size = New System.Drawing.Size(81, 23)
        Me.Alert_File_Path_Lbl.TabIndex = 1096
        Me.Alert_File_Path_Lbl.Text = "مسار ملف التنبيه"
        Me.Alert_File_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Time_To_Alert_Before_Azan_Lbl
        '
        Me.Time_To_Alert_Before_Azan_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Time_To_Alert_Before_Azan_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Time_To_Alert_Before_Azan_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Time_To_Alert_Before_Azan_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Time_To_Alert_Before_Azan_Lbl.Location = New System.Drawing.Point(520, 146)
        Me.Time_To_Alert_Before_Azan_Lbl.Name = "Time_To_Alert_Before_Azan_Lbl"
        Me.Time_To_Alert_Before_Azan_Lbl.Size = New System.Drawing.Size(199, 23)
        Me.Time_To_Alert_Before_Azan_Lbl.TabIndex = 1092
        Me.Time_To_Alert_Before_Azan_Lbl.Text = "وقت التنبية قبل الاذان"
        Me.Time_To_Alert_Before_Azan_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Alert_Before_Azan_ChkBx
        '
        Me.Alert_Before_Azan_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_Before_Azan_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Alert_Before_Azan_ChkBx.Location = New System.Drawing.Point(522, 123)
        Me.Alert_Before_Azan_ChkBx.Name = "Alert_Before_Azan_ChkBx"
        Me.Alert_Before_Azan_ChkBx.Size = New System.Drawing.Size(308, 21)
        Me.Alert_Before_Azan_ChkBx.TabIndex = 1091
        Me.Alert_Before_Azan_ChkBx.Text = "التنبية قبل الاذان"
        Me.Alert_Before_Azan_ChkBx.UseVisualStyleBackColor = False
        '
        'Alert_Before_Azan_Lbl
        '
        Me.Alert_Before_Azan_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Alert_Before_Azan_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_Before_Azan_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Before_Azan_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Before_Azan_Lbl.Location = New System.Drawing.Point(520, 122)
        Me.Alert_Before_Azan_Lbl.Name = "Alert_Before_Azan_Lbl"
        Me.Alert_Before_Azan_Lbl.Size = New System.Drawing.Size(315, 23)
        Me.Alert_Before_Azan_Lbl.TabIndex = 1090
        Me.Alert_Before_Azan_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Azan_Takbeer_Only_ChkBx
        '
        Me.Azan_Takbeer_Only_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Azan_Takbeer_Only_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Azan_Takbeer_Only_ChkBx.Location = New System.Drawing.Point(522, 99)
        Me.Azan_Takbeer_Only_ChkBx.Name = "Azan_Takbeer_Only_ChkBx"
        Me.Azan_Takbeer_Only_ChkBx.Size = New System.Drawing.Size(308, 21)
        Me.Azan_Takbeer_Only_ChkBx.TabIndex = 1089
        Me.Azan_Takbeer_Only_ChkBx.Text = "الاذان تكبير فقط"
        Me.Azan_Takbeer_Only_ChkBx.UseVisualStyleBackColor = False
        '
        'Azan_Takbeer_Only_Lbl
        '
        Me.Azan_Takbeer_Only_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Azan_Takbeer_Only_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Azan_Takbeer_Only_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Azan_Takbeer_Only_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Azan_Takbeer_Only_Lbl.Location = New System.Drawing.Point(520, 98)
        Me.Azan_Takbeer_Only_Lbl.Name = "Azan_Takbeer_Only_Lbl"
        Me.Azan_Takbeer_Only_Lbl.Size = New System.Drawing.Size(315, 23)
        Me.Azan_Takbeer_Only_Lbl.TabIndex = 1088
        Me.Azan_Takbeer_Only_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Azan_Activation_ChkBx
        '
        Me.Azan_Activation_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Azan_Activation_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Azan_Activation_ChkBx.Location = New System.Drawing.Point(522, 75)
        Me.Azan_Activation_ChkBx.Name = "Azan_Activation_ChkBx"
        Me.Azan_Activation_ChkBx.Size = New System.Drawing.Size(308, 21)
        Me.Azan_Activation_ChkBx.TabIndex = 1087
        Me.Azan_Activation_ChkBx.Text = "تفعيل الاذان"
        Me.Azan_Activation_ChkBx.UseVisualStyleBackColor = False
        '
        'Azan_Activation_Lbl
        '
        Me.Azan_Activation_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Azan_Activation_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Azan_Activation_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Azan_Activation_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Azan_Activation_Lbl.Location = New System.Drawing.Point(520, 74)
        Me.Azan_Activation_Lbl.Name = "Azan_Activation_Lbl"
        Me.Azan_Activation_Lbl.Size = New System.Drawing.Size(315, 23)
        Me.Azan_Activation_Lbl.TabIndex = 1086
        Me.Azan_Activation_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Stop_Fagr_Voice_File_Btn
        '
        Me.Stop_Fagr_Voice_File_Btn.BackgroundImage = CType(resources.GetObject("Stop_Fagr_Voice_File_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Stop_Fagr_Voice_File_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Stop_Fagr_Voice_File_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Stop_Fagr_Voice_File_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stop_Fagr_Voice_File_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stop_Fagr_Voice_File_Btn.Location = New System.Drawing.Point(810, 1)
        Me.Stop_Fagr_Voice_File_Btn.Name = "Stop_Fagr_Voice_File_Btn"
        Me.Stop_Fagr_Voice_File_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Stop_Fagr_Voice_File_Btn.TabIndex = 1085
        Me.Stop_Fagr_Voice_File_Btn.TabStop = False
        Me.Stop_Fagr_Voice_File_Btn.UseVisualStyleBackColor = True
        '
        'Play_Fagr_Voice_File_Btn
        '
        Me.Play_Fagr_Voice_File_Btn.BackgroundImage = CType(resources.GetObject("Play_Fagr_Voice_File_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Play_Fagr_Voice_File_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Play_Fagr_Voice_File_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Play_Fagr_Voice_File_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Play_Fagr_Voice_File_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Play_Fagr_Voice_File_Btn.Location = New System.Drawing.Point(786, 1)
        Me.Play_Fagr_Voice_File_Btn.Name = "Play_Fagr_Voice_File_Btn"
        Me.Play_Fagr_Voice_File_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Play_Fagr_Voice_File_Btn.TabIndex = 1084
        Me.Play_Fagr_Voice_File_Btn.TabStop = False
        Me.Play_Fagr_Voice_File_Btn.UseVisualStyleBackColor = True
        '
        'Azan_Spoke_Method_ChkBx
        '
        Me.Azan_Spoke_Method_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Azan_Spoke_Method_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Azan_Spoke_Method_ChkBx.Location = New System.Drawing.Point(522, 51)
        Me.Azan_Spoke_Method_ChkBx.Name = "Azan_Spoke_Method_ChkBx"
        Me.Azan_Spoke_Method_ChkBx.Size = New System.Drawing.Size(308, 21)
        Me.Azan_Spoke_Method_ChkBx.TabIndex = 1083
        Me.Azan_Spoke_Method_ChkBx.Text = "إختيار المؤذن تتابعى"
        Me.Azan_Spoke_Method_ChkBx.ThreeState = True
        Me.Azan_Spoke_Method_ChkBx.UseVisualStyleBackColor = False
        '
        'Azan_Spoke_Method_Lbl
        '
        Me.Azan_Spoke_Method_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Azan_Spoke_Method_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Azan_Spoke_Method_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Azan_Spoke_Method_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Azan_Spoke_Method_Lbl.Location = New System.Drawing.Point(520, 50)
        Me.Azan_Spoke_Method_Lbl.Name = "Azan_Spoke_Method_Lbl"
        Me.Azan_Spoke_Method_Lbl.Size = New System.Drawing.Size(315, 23)
        Me.Azan_Spoke_Method_Lbl.TabIndex = 1082
        Me.Azan_Spoke_Method_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Voice_Azan_Files_CmbBx
        '
        Me.Voice_Azan_Files_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Voice_Azan_Files_CmbBx.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Voice_Azan_Files_CmbBx.FormattingEnabled = True
        Me.Voice_Azan_Files_CmbBx.Location = New System.Drawing.Point(520, 26)
        Me.Voice_Azan_Files_CmbBx.Name = "Voice_Azan_Files_CmbBx"
        Me.Voice_Azan_Files_CmbBx.Size = New System.Drawing.Size(266, 23)
        Me.Voice_Azan_Files_CmbBx.Sorted = True
        Me.Voice_Azan_Files_CmbBx.TabIndex = 1081
        Me.Voice_Azan_Files_CmbBx.TabStop = False
        '
        'Fagr_Voice_Files_CmbBx
        '
        Me.Fagr_Voice_Files_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Fagr_Voice_Files_CmbBx.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Fagr_Voice_Files_CmbBx.FormattingEnabled = True
        Me.Fagr_Voice_Files_CmbBx.Location = New System.Drawing.Point(520, 2)
        Me.Fagr_Voice_Files_CmbBx.Name = "Fagr_Voice_Files_CmbBx"
        Me.Fagr_Voice_Files_CmbBx.Size = New System.Drawing.Size(266, 23)
        Me.Fagr_Voice_Files_CmbBx.Sorted = True
        Me.Fagr_Voice_Files_CmbBx.TabIndex = 1080
        Me.Fagr_Voice_Files_CmbBx.TabStop = False
        '
        'Stop_Voice_Azan_File_Btn
        '
        Me.Stop_Voice_Azan_File_Btn.BackgroundImage = CType(resources.GetObject("Stop_Voice_Azan_File_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Stop_Voice_Azan_File_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Stop_Voice_Azan_File_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Stop_Voice_Azan_File_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stop_Voice_Azan_File_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stop_Voice_Azan_File_Btn.Location = New System.Drawing.Point(810, 25)
        Me.Stop_Voice_Azan_File_Btn.Name = "Stop_Voice_Azan_File_Btn"
        Me.Stop_Voice_Azan_File_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Stop_Voice_Azan_File_Btn.TabIndex = 1079
        Me.Stop_Voice_Azan_File_Btn.TabStop = False
        Me.Stop_Voice_Azan_File_Btn.UseVisualStyleBackColor = True
        '
        'Play_Voice_Azan_File_Btn
        '
        Me.Play_Voice_Azan_File_Btn.BackgroundImage = CType(resources.GetObject("Play_Voice_Azan_File_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Play_Voice_Azan_File_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Play_Voice_Azan_File_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Play_Voice_Azan_File_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Play_Voice_Azan_File_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Play_Voice_Azan_File_Btn.Location = New System.Drawing.Point(786, 25)
        Me.Play_Voice_Azan_File_Btn.Name = "Play_Voice_Azan_File_Btn"
        Me.Play_Voice_Azan_File_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Play_Voice_Azan_File_Btn.TabIndex = 1078
        Me.Play_Voice_Azan_File_Btn.TabStop = False
        Me.Play_Voice_Azan_File_Btn.UseVisualStyleBackColor = True
        '
        'Save_Day_Light_ChkBx
        '
        Me.Save_Day_Light_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Save_Day_Light_ChkBx.Location = New System.Drawing.Point(6, 147)
        Me.Save_Day_Light_ChkBx.Name = "Save_Day_Light_ChkBx"
        Me.Save_Day_Light_ChkBx.Size = New System.Drawing.Size(512, 21)
        Me.Save_Day_Light_ChkBx.TabIndex = 1077
        Me.Save_Day_Light_ChkBx.Text = "Save Day Light"
        Me.Save_Day_Light_ChkBx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Save_Day_Light_ChkBx.UseVisualStyleBackColor = False
        '
        'Save_Day_Light_Lbl
        '
        Me.Save_Day_Light_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Save_Day_Light_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Save_Day_Light_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Save_Day_Light_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Save_Day_Light_Lbl.Location = New System.Drawing.Point(2, 146)
        Me.Save_Day_Light_Lbl.Name = "Save_Day_Light_Lbl"
        Me.Save_Day_Light_Lbl.Size = New System.Drawing.Size(517, 23)
        Me.Save_Day_Light_Lbl.TabIndex = 1076
        Me.Save_Day_Light_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Left_Time_Lbl
        '
        Me.Left_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Left_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Left_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Left_Time_Lbl.Location = New System.Drawing.Point(2, 170)
        Me.Left_Time_Lbl.Name = "Left_Time_Lbl"
        Me.Left_Time_Lbl.Size = New System.Drawing.Size(517, 22)
        Me.Left_Time_Lbl.TabIndex = 1074
        Me.Left_Time_Lbl.Text = "الوقت المتبقي على الآذان التالى"
        Me.Left_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Date_DtTmPkr
        '
        Me.Date_DtTmPkr.CalendarFont = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Date_DtTmPkr.CustomFormat = "yyyy-MM-dd"
        Me.Date_DtTmPkr.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.Date_DtTmPkr.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Date_DtTmPkr.Location = New System.Drawing.Point(78, 2)
        Me.Date_DtTmPkr.Name = "Date_DtTmPkr"
        Me.Date_DtTmPkr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Date_DtTmPkr.RightToLeftLayout = True
        Me.Date_DtTmPkr.Size = New System.Drawing.Size(266, 23)
        Me.Date_DtTmPkr.TabIndex = 1073
        '
        'Date_Lbl
        '
        Me.Date_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Date_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Date_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Date_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Date_Lbl.Location = New System.Drawing.Point(2, 2)
        Me.Date_Lbl.Name = "Date_Lbl"
        Me.Date_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Date_Lbl.TabIndex = 1072
        Me.Date_Lbl.Text = "اليوم"
        Me.Date_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Longitude_TxtBx
        '
        Me.Longitude_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Longitude_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Longitude_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Longitude_TxtBx.Location = New System.Drawing.Point(78, 98)
        Me.Longitude_TxtBx.Multiline = True
        Me.Longitude_TxtBx.Name = "Longitude_TxtBx"
        Me.Longitude_TxtBx.ReadOnly = True
        Me.Longitude_TxtBx.Size = New System.Drawing.Size(266, 23)
        Me.Longitude_TxtBx.TabIndex = 1071
        Me.Longitude_TxtBx.TabStop = False
        '
        'Longitude_Lbl
        '
        Me.Longitude_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Longitude_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Longitude_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Longitude_Lbl.Location = New System.Drawing.Point(2, 98)
        Me.Longitude_Lbl.Name = "Longitude_Lbl"
        Me.Longitude_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Longitude_Lbl.TabIndex = 1070
        Me.Longitude_Lbl.Text = "خط الطول"
        Me.Longitude_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Latitude_TxtBx
        '
        Me.Latitude_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Latitude_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Latitude_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Latitude_TxtBx.Location = New System.Drawing.Point(78, 74)
        Me.Latitude_TxtBx.Multiline = True
        Me.Latitude_TxtBx.Name = "Latitude_TxtBx"
        Me.Latitude_TxtBx.ReadOnly = True
        Me.Latitude_TxtBx.Size = New System.Drawing.Size(266, 23)
        Me.Latitude_TxtBx.TabIndex = 1069
        Me.Latitude_TxtBx.TabStop = False
        '
        'Latitude_Lbl
        '
        Me.Latitude_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Latitude_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Latitude_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Latitude_Lbl.Location = New System.Drawing.Point(2, 74)
        Me.Latitude_Lbl.Name = "Latitude_Lbl"
        Me.Latitude_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Latitude_Lbl.TabIndex = 1068
        Me.Latitude_Lbl.Text = "خط العرض"
        Me.Latitude_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'City_CmbBx
        '
        Me.City_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.City_CmbBx.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.City_CmbBx.FormattingEnabled = True
        Me.City_CmbBx.Items.AddRange(New Object() {"Custom", "Egypt", "ISNA", "Jafari", "Karachi", "Kemenag", "Makkah", "MWL"})
        Me.City_CmbBx.Location = New System.Drawing.Point(78, 50)
        Me.City_CmbBx.Name = "City_CmbBx"
        Me.City_CmbBx.Size = New System.Drawing.Size(266, 23)
        Me.City_CmbBx.Sorted = True
        Me.City_CmbBx.TabIndex = 1067
        Me.City_CmbBx.TabStop = False
        '
        'City_Lbl
        '
        Me.City_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.City_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.City_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.City_Lbl.Location = New System.Drawing.Point(2, 50)
        Me.City_Lbl.Name = "City_Lbl"
        Me.City_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.City_Lbl.TabIndex = 1066
        Me.City_Lbl.Text = "المدينة"
        Me.City_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Country_CmbBx
        '
        Me.Country_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Country_CmbBx.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Country_CmbBx.FormattingEnabled = True
        Me.Country_CmbBx.Items.AddRange(New Object() {"Custom", "Egypt", "ISNA", "Jafari", "Karachi", "Kemenag", "Makkah", "MWL"})
        Me.Country_CmbBx.Location = New System.Drawing.Point(78, 26)
        Me.Country_CmbBx.Name = "Country_CmbBx"
        Me.Country_CmbBx.Size = New System.Drawing.Size(266, 23)
        Me.Country_CmbBx.Sorted = True
        Me.Country_CmbBx.TabIndex = 1065
        Me.Country_CmbBx.TabStop = False
        '
        'Country_Lbl
        '
        Me.Country_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Country_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Country_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Country_Lbl.Location = New System.Drawing.Point(2, 26)
        Me.Country_Lbl.Name = "Country_Lbl"
        Me.Country_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Country_Lbl.TabIndex = 1064
        Me.Country_Lbl.Text = "البلد"
        Me.Country_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Calculation_Methods_CmbBx
        '
        Me.Calculation_Methods_CmbBx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Calculation_Methods_CmbBx.Font = New System.Drawing.Font("Times New Roman", 9.25!)
        Me.Calculation_Methods_CmbBx.FormattingEnabled = True
        Me.Calculation_Methods_CmbBx.Location = New System.Drawing.Point(78, 122)
        Me.Calculation_Methods_CmbBx.Name = "Calculation_Methods_CmbBx"
        Me.Calculation_Methods_CmbBx.Size = New System.Drawing.Size(266, 23)
        Me.Calculation_Methods_CmbBx.Sorted = True
        Me.Calculation_Methods_CmbBx.TabIndex = 1063
        Me.Calculation_Methods_CmbBx.TabStop = False
        '
        'Calculation_Methods_Lbl
        '
        Me.Calculation_Methods_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Calculation_Methods_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Calculation_Methods_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Calculation_Methods_Lbl.Location = New System.Drawing.Point(2, 122)
        Me.Calculation_Methods_Lbl.Name = "Calculation_Methods_Lbl"
        Me.Calculation_Methods_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Calculation_Methods_Lbl.TabIndex = 901
        Me.Calculation_Methods_Lbl.Text = "حساب الوقت"
        Me.Calculation_Methods_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Isha_TxtBx
        '
        Me.Isha_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Isha_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Isha_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Isha_TxtBx.Location = New System.Drawing.Point(345, 122)
        Me.Isha_TxtBx.Multiline = True
        Me.Isha_TxtBx.Name = "Isha_TxtBx"
        Me.Isha_TxtBx.ReadOnly = True
        Me.Isha_TxtBx.Size = New System.Drawing.Size(96, 23)
        Me.Isha_TxtBx.TabIndex = 900
        Me.Isha_TxtBx.TabStop = False
        Me.Isha_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Maghrib_TxtBx
        '
        Me.Maghrib_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Maghrib_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Maghrib_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Maghrib_TxtBx.Location = New System.Drawing.Point(345, 98)
        Me.Maghrib_TxtBx.Multiline = True
        Me.Maghrib_TxtBx.Name = "Maghrib_TxtBx"
        Me.Maghrib_TxtBx.ReadOnly = True
        Me.Maghrib_TxtBx.Size = New System.Drawing.Size(96, 23)
        Me.Maghrib_TxtBx.TabIndex = 898
        Me.Maghrib_TxtBx.TabStop = False
        Me.Maghrib_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Asr_TxtBx
        '
        Me.Asr_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Asr_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Asr_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Asr_TxtBx.Location = New System.Drawing.Point(345, 74)
        Me.Asr_TxtBx.Multiline = True
        Me.Asr_TxtBx.Name = "Asr_TxtBx"
        Me.Asr_TxtBx.ReadOnly = True
        Me.Asr_TxtBx.Size = New System.Drawing.Size(96, 23)
        Me.Asr_TxtBx.TabIndex = 897
        Me.Asr_TxtBx.TabStop = False
        Me.Asr_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Dhuhr_TxtBx
        '
        Me.Dhuhr_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Dhuhr_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Dhuhr_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Dhuhr_TxtBx.Location = New System.Drawing.Point(345, 50)
        Me.Dhuhr_TxtBx.Multiline = True
        Me.Dhuhr_TxtBx.Name = "Dhuhr_TxtBx"
        Me.Dhuhr_TxtBx.ReadOnly = True
        Me.Dhuhr_TxtBx.Size = New System.Drawing.Size(96, 23)
        Me.Dhuhr_TxtBx.TabIndex = 896
        Me.Dhuhr_TxtBx.TabStop = False
        Me.Dhuhr_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Sunrise_TxtBx
        '
        Me.Sunrise_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Sunrise_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sunrise_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Sunrise_TxtBx.Location = New System.Drawing.Point(345, 26)
        Me.Sunrise_TxtBx.Multiline = True
        Me.Sunrise_TxtBx.Name = "Sunrise_TxtBx"
        Me.Sunrise_TxtBx.ReadOnly = True
        Me.Sunrise_TxtBx.Size = New System.Drawing.Size(96, 23)
        Me.Sunrise_TxtBx.TabIndex = 895
        Me.Sunrise_TxtBx.TabStop = False
        Me.Sunrise_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Fajr_TxtBx
        '
        Me.Fajr_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Fajr_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Fajr_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Fajr_TxtBx.Location = New System.Drawing.Point(345, 2)
        Me.Fajr_TxtBx.Multiline = True
        Me.Fajr_TxtBx.Name = "Fajr_TxtBx"
        Me.Fajr_TxtBx.ReadOnly = True
        Me.Fajr_TxtBx.Size = New System.Drawing.Size(96, 23)
        Me.Fajr_TxtBx.TabIndex = 894
        Me.Fajr_TxtBx.TabStop = False
        Me.Fajr_TxtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Isha_Lbl
        '
        Me.Isha_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Isha_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Isha_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Isha_Lbl.Location = New System.Drawing.Point(444, 122)
        Me.Isha_Lbl.Name = "Isha_Lbl"
        Me.Isha_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Isha_Lbl.TabIndex = 6
        Me.Isha_Lbl.Text = "العِشاء"
        Me.Isha_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Maghrib_Lbl
        '
        Me.Maghrib_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Maghrib_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Maghrib_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Maghrib_Lbl.Location = New System.Drawing.Point(444, 98)
        Me.Maghrib_Lbl.Name = "Maghrib_Lbl"
        Me.Maghrib_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Maghrib_Lbl.TabIndex = 4
        Me.Maghrib_Lbl.Text = "المَغرب"
        Me.Maghrib_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Asr_Lbl
        '
        Me.Asr_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Asr_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Asr_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Asr_Lbl.Location = New System.Drawing.Point(444, 74)
        Me.Asr_Lbl.Name = "Asr_Lbl"
        Me.Asr_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Asr_Lbl.TabIndex = 3
        Me.Asr_Lbl.Text = "العَصر"
        Me.Asr_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Dhuhr_Lbl
        '
        Me.Dhuhr_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Dhuhr_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Dhuhr_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Dhuhr_Lbl.Location = New System.Drawing.Point(444, 50)
        Me.Dhuhr_Lbl.Name = "Dhuhr_Lbl"
        Me.Dhuhr_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Dhuhr_Lbl.TabIndex = 2
        Me.Dhuhr_Lbl.Text = "الظُّهْر"
        Me.Dhuhr_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Sunrise_Lbl
        '
        Me.Sunrise_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Sunrise_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Sunrise_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Sunrise_Lbl.Location = New System.Drawing.Point(444, 26)
        Me.Sunrise_Lbl.Name = "Sunrise_Lbl"
        Me.Sunrise_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Sunrise_Lbl.TabIndex = 1
        Me.Sunrise_Lbl.Text = "الشروق"
        Me.Sunrise_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Fajr_Lbl
        '
        Me.Fajr_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Fajr_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Fajr_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Fajr_Lbl.Location = New System.Drawing.Point(444, 2)
        Me.Fajr_Lbl.Name = "Fajr_Lbl"
        Me.Fajr_Lbl.Size = New System.Drawing.Size(75, 23)
        Me.Fajr_Lbl.TabIndex = 0
        Me.Fajr_Lbl.Text = "الفجْر"
        Me.Fajr_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Alarm_Time_TbPg
        '
        Me.Alarm_Time_TbPg.Controls.Add(Me.Available_Alerts_DGV)
        Me.Alarm_Time_TbPg.Controls.Add(Me.Available_Alarm_Header_Pnl)
        Me.Alarm_Time_TbPg.Controls.Add(Me.Alarm_Setting_Pnl)
        Me.Alarm_Time_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Alarm_Time_TbPg.Name = "Alarm_Time_TbPg"
        Me.Alarm_Time_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Alarm_Time_TbPg.TabIndex = 6
        Me.Alarm_Time_TbPg.Text = "Alarm Time"
        Me.Alarm_Time_TbPg.UseVisualStyleBackColor = True
        '
        'Available_Alerts_DGV
        '
        Me.Available_Alerts_DGV.AllowUserToAddRows = False
        Me.Available_Alerts_DGV.AllowUserToDeleteRows = False
        Me.Available_Alerts_DGV.AllowUserToOrderColumns = True
        Me.Available_Alerts_DGV.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Available_Alerts_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.Available_Alerts_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Available_Alerts_DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Available_Alerts_DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.Available_Alerts_DGV.EnableHeadersVisualStyles = False
        Me.Available_Alerts_DGV.Location = New System.Drawing.Point(0, 23)
        Me.Available_Alerts_DGV.MultiSelect = False
        Me.Available_Alerts_DGV.Name = "Available_Alerts_DGV"
        Me.Available_Alerts_DGV.ReadOnly = True
        Me.Available_Alerts_DGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Available_Alerts_DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.Available_Alerts_DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.Available_Alerts_DGV.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Times New Roman", 10.25!)
        Me.Available_Alerts_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Available_Alerts_DGV.Size = New System.Drawing.Size(634, 175)
        Me.Available_Alerts_DGV.TabIndex = 1093
        Me.Available_Alerts_DGV.TabStop = False
        '
        'Available_Alarm_Header_Pnl
        '
        Me.Available_Alarm_Header_Pnl.Controls.Add(Me.Preview_Available_Alerts_Btn)
        Me.Available_Alarm_Header_Pnl.Controls.Add(Me.Available_Alerts_Lbl)
        Me.Available_Alarm_Header_Pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.Available_Alarm_Header_Pnl.Location = New System.Drawing.Point(0, 0)
        Me.Available_Alarm_Header_Pnl.Name = "Available_Alarm_Header_Pnl"
        Me.Available_Alarm_Header_Pnl.Size = New System.Drawing.Size(634, 23)
        Me.Available_Alarm_Header_Pnl.TabIndex = 1092
        '
        'Preview_Available_Alerts_Btn
        '
        Me.Preview_Available_Alerts_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Preview_Available_Alerts_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Preview_Available_Alerts_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Preview_Available_Alerts_Btn.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Preview_Available_Alerts_Btn.Font = New System.Drawing.Font("Times New Roman", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Preview_Available_Alerts_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Preview_Available_Alerts_Btn.Location = New System.Drawing.Point(562, -1)
        Me.Preview_Available_Alerts_Btn.Name = "Preview_Available_Alerts_Btn"
        Me.Preview_Available_Alerts_Btn.Size = New System.Drawing.Size(73, 24)
        Me.Preview_Available_Alerts_Btn.TabIndex = 1091
        Me.Preview_Available_Alerts_Btn.Text = "Preview"
        Me.Preview_Available_Alerts_Btn.UseVisualStyleBackColor = True
        '
        'Available_Alerts_Lbl
        '
        Me.Available_Alerts_Lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Available_Alerts_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Available_Alerts_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Available_Alerts_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Available_Alerts_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Available_Alerts_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Available_Alerts_Lbl.Name = "Available_Alerts_Lbl"
        Me.Available_Alerts_Lbl.Size = New System.Drawing.Size(562, 23)
        Me.Available_Alerts_Lbl.TabIndex = 1089
        Me.Available_Alerts_Lbl.Text = "التنبيهات المتاحة"
        Me.Available_Alerts_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Alarm_Setting_Pnl
        '
        Me.Alarm_Setting_Pnl.Controls.Add(Me.New_Alarm_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Next_Current_Alarm_Time_Lbl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_Comment_TxtBx)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Pause_Alarm_Timer_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.End_Alert_Time_Status_ChkBx)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Save_Alert_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Delete_Alert_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_Time_Lbl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alarm_Repeet_Time_Pnl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Stop_Playing_Alert_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Play_Alert_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Select_Alert_File_Btn)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_Voice_Path_TxtBx)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_Voice_Path_Lbl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Clock1)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_Active_ChkBx)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_Active_Lbl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_One_Time_ChkBx)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Alert_One_Time_Lbl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.End_Alert_Time_DtTmPikr)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.End_Alert_Time_Lbl)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Start_Alert_Time_DtTmPikr)
        Me.Alarm_Setting_Pnl.Controls.Add(Me.Start_Alert_Time_Lbl)
        Me.Alarm_Setting_Pnl.Dock = System.Windows.Forms.DockStyle.Right
        Me.Alarm_Setting_Pnl.Location = New System.Drawing.Point(634, 0)
        Me.Alarm_Setting_Pnl.Name = "Alarm_Setting_Pnl"
        Me.Alarm_Setting_Pnl.Size = New System.Drawing.Size(379, 198)
        Me.Alarm_Setting_Pnl.TabIndex = 1088
        '
        'Next_Current_Alarm_Time_Lbl
        '
        Me.Next_Current_Alarm_Time_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Next_Current_Alarm_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Next_Current_Alarm_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Next_Current_Alarm_Time_Lbl.Location = New System.Drawing.Point(175, 99)
        Me.Next_Current_Alarm_Time_Lbl.Name = "Next_Current_Alarm_Time_Lbl"
        Me.Next_Current_Alarm_Time_Lbl.Size = New System.Drawing.Size(70, 20)
        Me.Next_Current_Alarm_Time_Lbl.TabIndex = 1112
        Me.Next_Current_Alarm_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Alert_Comment_TxtBx
        '
        Me.Alert_Comment_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Alert_Comment_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Comment_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Comment_TxtBx.Location = New System.Drawing.Point(1, 0)
        Me.Alert_Comment_TxtBx.Multiline = True
        Me.Alert_Comment_TxtBx.Name = "Alert_Comment_TxtBx"
        Me.Alert_Comment_TxtBx.Size = New System.Drawing.Size(265, 23)
        Me.Alert_Comment_TxtBx.TabIndex = 1111
        Me.Alert_Comment_TxtBx.TabStop = False
        '
        'End_Alert_Time_Status_ChkBx
        '
        Me.End_Alert_Time_Status_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.End_Alert_Time_Status_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.End_Alert_Time_Status_ChkBx.Location = New System.Drawing.Point(270, 49)
        Me.End_Alert_Time_Status_ChkBx.Name = "End_Alert_Time_Status_ChkBx"
        Me.End_Alert_Time_Status_ChkBx.Size = New System.Drawing.Size(108, 21)
        Me.End_Alert_Time_Status_ChkBx.TabIndex = 1109
        Me.End_Alert_Time_Status_ChkBx.Text = "نهاية وقت التنبية"
        Me.End_Alert_Time_Status_ChkBx.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.End_Alert_Time_Status_ChkBx.UseVisualStyleBackColor = False
        '
        'Alert_Time_Lbl
        '
        Me.Alert_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Time_Lbl.Location = New System.Drawing.Point(267, 0)
        Me.Alert_Time_Lbl.Name = "Alert_Time_Lbl"
        Me.Alert_Time_Lbl.Size = New System.Drawing.Size(112, 23)
        Me.Alert_Time_Lbl.TabIndex = 1105
        Me.Alert_Time_Lbl.Text = "وقت التنبية"
        Me.Alert_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Alarm_Repeet_Time_Pnl
        '
        Me.Alarm_Repeet_Time_Pnl.Controls.Add(Me.Alert_Repeet_Minute_NmrcUpDn)
        Me.Alarm_Repeet_Time_Pnl.Controls.Add(Me.Alert_Repeet_Hour_NmrcUpDn)
        Me.Alarm_Repeet_Time_Pnl.Controls.Add(Me.Alert_Repeet_Day_NmrcUpDn)
        Me.Alarm_Repeet_Time_Pnl.Controls.Add(Me.Alert_Repeet_Time_Lbl)
        Me.Alarm_Repeet_Time_Pnl.Location = New System.Drawing.Point(249, 144)
        Me.Alarm_Repeet_Time_Pnl.Name = "Alarm_Repeet_Time_Pnl"
        Me.Alarm_Repeet_Time_Pnl.Size = New System.Drawing.Size(130, 54)
        Me.Alarm_Repeet_Time_Pnl.TabIndex = 1104
        '
        'Alert_Repeet_Time_Lbl
        '
        Me.Alert_Repeet_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_Repeet_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_Repeet_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Repeet_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Repeet_Time_Lbl.Location = New System.Drawing.Point(0, 0)
        Me.Alert_Repeet_Time_Lbl.Name = "Alert_Repeet_Time_Lbl"
        Me.Alert_Repeet_Time_Lbl.Size = New System.Drawing.Size(130, 54)
        Me.Alert_Repeet_Time_Lbl.TabIndex = 1078
        Me.Alert_Repeet_Time_Lbl.Text = "مدى تكرار التنبيه" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DD          HH          MM"
        Me.Alert_Repeet_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Stop_Playing_Alert_Btn
        '
        Me.Stop_Playing_Alert_Btn.BackgroundImage = CType(resources.GetObject("Stop_Playing_Alert_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Stop_Playing_Alert_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Stop_Playing_Alert_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Stop_Playing_Alert_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Stop_Playing_Alert_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Stop_Playing_Alert_Btn.Location = New System.Drawing.Point(242, 71)
        Me.Stop_Playing_Alert_Btn.Name = "Stop_Playing_Alert_Btn"
        Me.Stop_Playing_Alert_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Stop_Playing_Alert_Btn.TabIndex = 1103
        Me.Stop_Playing_Alert_Btn.TabStop = False
        Me.Stop_Playing_Alert_Btn.UseVisualStyleBackColor = True
        '
        'Play_Alert_Btn
        '
        Me.Play_Alert_Btn.BackgroundImage = CType(resources.GetObject("Play_Alert_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Play_Alert_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Play_Alert_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Play_Alert_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Play_Alert_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Play_Alert_Btn.Location = New System.Drawing.Point(218, 71)
        Me.Play_Alert_Btn.Name = "Play_Alert_Btn"
        Me.Play_Alert_Btn.Size = New System.Drawing.Size(25, 25)
        Me.Play_Alert_Btn.TabIndex = 1102
        Me.Play_Alert_Btn.TabStop = False
        Me.Play_Alert_Btn.UseVisualStyleBackColor = True
        '
        'Select_Alert_File_Btn
        '
        Me.Select_Alert_File_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Select_Alert_File_Btn.BackgroundImage = CType(resources.GetObject("Select_Alert_File_Btn.BackgroundImage"), System.Drawing.Image)
        Me.Select_Alert_File_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Select_Alert_File_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Select_Alert_File_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Select_Alert_File_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Select_Alert_File_Btn.Location = New System.Drawing.Point(0, 71)
        Me.Select_Alert_File_Btn.Name = "Select_Alert_File_Btn"
        Me.Select_Alert_File_Btn.Size = New System.Drawing.Size(26, 25)
        Me.Select_Alert_File_Btn.TabIndex = 1101
        Me.Select_Alert_File_Btn.TabStop = False
        Me.Select_Alert_File_Btn.UseVisualStyleBackColor = True
        '
        'Alert_Voice_Path_TxtBx
        '
        Me.Alert_Voice_Path_TxtBx.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Alert_Voice_Path_TxtBx.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Voice_Path_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Voice_Path_TxtBx.Location = New System.Drawing.Point(26, 72)
        Me.Alert_Voice_Path_TxtBx.Multiline = True
        Me.Alert_Voice_Path_TxtBx.Name = "Alert_Voice_Path_TxtBx"
        Me.Alert_Voice_Path_TxtBx.ReadOnly = True
        Me.Alert_Voice_Path_TxtBx.Size = New System.Drawing.Size(192, 23)
        Me.Alert_Voice_Path_TxtBx.TabIndex = 1100
        Me.Alert_Voice_Path_TxtBx.TabStop = False
        '
        'Alert_Voice_Path_Lbl
        '
        Me.Alert_Voice_Path_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_Voice_Path_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_Voice_Path_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Alert_Voice_Path_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Voice_Path_Lbl.Location = New System.Drawing.Point(267, 72)
        Me.Alert_Voice_Path_Lbl.Name = "Alert_Voice_Path_Lbl"
        Me.Alert_Voice_Path_Lbl.Size = New System.Drawing.Size(112, 23)
        Me.Alert_Voice_Path_Lbl.TabIndex = 1099
        Me.Alert_Voice_Path_Lbl.Text = "مسار ملف التنبيه"
        Me.Alert_Voice_Path_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Clock1
        '
        Me.Clock1.BigMarkers = New AnalogClock.Marker() {New AnalogClock.Marker("BigMarker90", 90.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker60", 60.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker30", 30.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker0", 0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker330", 330.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker300", 300.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker270", 270.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker240", 240.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker210", 210.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker180", 180.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker150", 150.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("BigMarker120", 120.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.06!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing)}
        Me.Clock1.CenterPoint.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock1.CenterPoint.RelativeRadius = 0.03!
        Me.Clock1.CenterPoint.Tag = Nothing
        Me.Clock1.HourHand.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock1.HourHand.RelativeRadius = 0.65!
        Me.Clock1.HourHand.Tag = Nothing
        Me.Clock1.HourHand.Width = 5.0!
        Me.Clock1.Location = New System.Drawing.Point(79, 98)
        Me.Clock1.MinuteHand.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock1.MinuteHand.RelativeRadius = 0.8!
        Me.Clock1.MinuteHand.Tag = Nothing
        Me.Clock1.MinuteHand.Width = 5.0!
        Me.Clock1.Name = "Clock1"
        Me.Clock1.SecondHand.PaintAttributes = New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!)
        Me.Clock1.SecondHand.RelativeRadius = 0.9!
        Me.Clock1.SecondHand.Tag = Nothing
        Me.Clock1.SecondHand.Width = 1.0!
        Me.Clock1.Size = New System.Drawing.Size(104, 97)
        Me.Clock1.SmallMarkers = New AnalogClock.Marker() {New AnalogClock.Marker("SmallMarker90", 90.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker84", 84.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker78", 78.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker72", 72.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker66", 66.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker60", 60.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker54", 54.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker48", 48.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker42", 42.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker36", 36.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker30", 30.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker24", 24.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker18", 18.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker12", 12.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker6", 6.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker0", 0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker354", 354.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker348", 348.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker342", 342.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker336", 336.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker330", 330.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker324", 324.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker318", 318.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker312", 312.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker306", 306.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker300", 300.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker294", 294.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker288", 288.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker282", 282.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker276", 276.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker270", 270.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker264", 264.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker258", 258.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker252", 252.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker246", 246.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker240", 240.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker234", 234.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker228", 228.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker222", 222.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker216", 216.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker210", 210.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker204", 204.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker198", 198.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker192", 192.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker186", 186.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker180", 180.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker174", 174.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker168", 168.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker162", 162.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker156", 156.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker150", 150.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker144", 144.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker138", 138.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker132", 132.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker126", 126.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker120", 120.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker114", 114.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker108", 108.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker102", 102.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing), New AnalogClock.Marker("SmallMarker96", 96.0!, System.Drawing.Color.Black, AnalogClock.MarkerStyle.Regular, True, 48.5!, 1.0!, 0.03!, 1.0!, New AnalogClock.PaintAttributes(AnalogClock.PaintObject.Brush, 1.0!), AnalogClock.SmoothMode.AntiAlias, Nothing)}
        Me.Clock1.Symbols = New AnalogClock.Symbol() {New AnalogClock.Symbol("Symbol90", 90.0!, "12", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 0, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol60", 60.0!, "1", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 1, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol30", 30.0!, "2", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 2, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol0", 0!, "3", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 3, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol330", 330.0!, "4", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 4, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol300", 300.0!, "5", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 5, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol270", 270.0!, "6", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 6, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol240", 240.0!, "7", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 7, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol210", 210.0!, "8", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 8, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol180", 180.0!, "9", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 9, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol150", 150.0!, "10", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 10, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing), New AnalogClock.Symbol("Symbol120", 120.0!, "11", New System.Drawing.Font("Engravers MT", 9.75!, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black, New System.Drawing.Point(1, 1), 11, True, True, AnalogClock.SymbolStyle.Numeric, 48.5!, 0.82!, System.Drawing.Text.TextRenderingHint.SystemDefault, Nothing)}
        Me.Clock1.TabIndex = 1088
        Me.Clock1.UtcOffset = System.TimeSpan.Parse("03:00:00")
        '
        'Alert_Active_ChkBx
        '
        Me.Alert_Active_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_Active_ChkBx.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.Alert_Active_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Active_ChkBx.Location = New System.Drawing.Point(253, 121)
        Me.Alert_Active_ChkBx.Name = "Alert_Active_ChkBx"
        Me.Alert_Active_ChkBx.Size = New System.Drawing.Size(125, 21)
        Me.Alert_Active_ChkBx.TabIndex = 1087
        Me.Alert_Active_ChkBx.Text = "التنبيه فعال"
        Me.Alert_Active_ChkBx.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Alert_Active_ChkBx.UseVisualStyleBackColor = False
        '
        'Alert_Active_Lbl
        '
        Me.Alert_Active_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Alert_Active_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_Active_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_Active_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_Active_Lbl.Location = New System.Drawing.Point(249, 120)
        Me.Alert_Active_Lbl.Name = "Alert_Active_Lbl"
        Me.Alert_Active_Lbl.Size = New System.Drawing.Size(130, 23)
        Me.Alert_Active_Lbl.TabIndex = 1086
        Me.Alert_Active_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Alert_One_Time_ChkBx
        '
        Me.Alert_One_Time_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Alert_One_Time_ChkBx.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_One_Time_ChkBx.Location = New System.Drawing.Point(253, 97)
        Me.Alert_One_Time_ChkBx.Name = "Alert_One_Time_ChkBx"
        Me.Alert_One_Time_ChkBx.Size = New System.Drawing.Size(125, 21)
        Me.Alert_One_Time_ChkBx.TabIndex = 1085
        Me.Alert_One_Time_ChkBx.Text = "التنبيه لمرة واحدة"
        Me.Alert_One_Time_ChkBx.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Alert_One_Time_ChkBx.UseVisualStyleBackColor = False
        '
        'Alert_One_Time_Lbl
        '
        Me.Alert_One_Time_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Alert_One_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Alert_One_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Alert_One_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Alert_One_Time_Lbl.Location = New System.Drawing.Point(249, 96)
        Me.Alert_One_Time_Lbl.Name = "Alert_One_Time_Lbl"
        Me.Alert_One_Time_Lbl.Size = New System.Drawing.Size(130, 23)
        Me.Alert_One_Time_Lbl.TabIndex = 1084
        Me.Alert_One_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'End_Alert_Time_DtTmPikr
        '
        Me.End_Alert_Time_DtTmPikr.CalendarFont = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.End_Alert_Time_DtTmPikr.CustomFormat = "yyyy/MM/dd hh:mm:ss tt MMM ddd"
        Me.End_Alert_Time_DtTmPikr.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.End_Alert_Time_DtTmPikr.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.End_Alert_Time_DtTmPikr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.End_Alert_Time_DtTmPikr.Location = New System.Drawing.Point(1, 48)
        Me.End_Alert_Time_DtTmPikr.Name = "End_Alert_Time_DtTmPikr"
        Me.End_Alert_Time_DtTmPikr.Size = New System.Drawing.Size(265, 23)
        Me.End_Alert_Time_DtTmPikr.TabIndex = 1077
        '
        'End_Alert_Time_Lbl
        '
        Me.End_Alert_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.End_Alert_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.End_Alert_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.End_Alert_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.End_Alert_Time_Lbl.Location = New System.Drawing.Point(267, 48)
        Me.End_Alert_Time_Lbl.Name = "End_Alert_Time_Lbl"
        Me.End_Alert_Time_Lbl.Size = New System.Drawing.Size(112, 23)
        Me.End_Alert_Time_Lbl.TabIndex = 1076
        Me.End_Alert_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Start_Alert_Time_DtTmPikr
        '
        Me.Start_Alert_Time_DtTmPikr.CalendarFont = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Start_Alert_Time_DtTmPikr.CustomFormat = "yyyy/MM/dd hh:mm:ss tt MMM ddd"
        Me.Start_Alert_Time_DtTmPikr.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
        Me.Start_Alert_Time_DtTmPikr.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Start_Alert_Time_DtTmPikr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Start_Alert_Time_DtTmPikr.Location = New System.Drawing.Point(1, 24)
        Me.Start_Alert_Time_DtTmPikr.Name = "Start_Alert_Time_DtTmPikr"
        Me.Start_Alert_Time_DtTmPikr.Size = New System.Drawing.Size(265, 23)
        Me.Start_Alert_Time_DtTmPikr.TabIndex = 1075
        '
        'Start_Alert_Time_Lbl
        '
        Me.Start_Alert_Time_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Start_Alert_Time_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Start_Alert_Time_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Start_Alert_Time_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Start_Alert_Time_Lbl.Location = New System.Drawing.Point(267, 24)
        Me.Start_Alert_Time_Lbl.Name = "Start_Alert_Time_Lbl"
        Me.Start_Alert_Time_Lbl.Size = New System.Drawing.Size(112, 23)
        Me.Start_Alert_Time_Lbl.TabIndex = 1074
        Me.Start_Alert_Time_Lbl.Text = "بداية وقت التنبية"
        Me.Start_Alert_Time_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Escalation_To_Author_TbPg
        '
        Me.Escalation_To_Author_TbPg.Controls.Add(Me.User_Escalation_RchTxtBx)
        Me.Escalation_To_Author_TbPg.Controls.Add(Me.Escalation_Pnl)
        Me.Escalation_To_Author_TbPg.Controls.Add(Me.Panel1)
        Me.Escalation_To_Author_TbPg.Location = New System.Drawing.Point(4, 28)
        Me.Escalation_To_Author_TbPg.Name = "Escalation_To_Author_TbPg"
        Me.Escalation_To_Author_TbPg.Size = New System.Drawing.Size(1013, 198)
        Me.Escalation_To_Author_TbPg.TabIndex = 5
        Me.Escalation_To_Author_TbPg.Text = "Escalation To Author"
        Me.Escalation_To_Author_TbPg.UseVisualStyleBackColor = True
        '
        'User_Escalation_RchTxtBx
        '
        Me.User_Escalation_RchTxtBx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.User_Escalation_RchTxtBx.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.User_Escalation_RchTxtBx.Location = New System.Drawing.Point(0, 28)
        Me.User_Escalation_RchTxtBx.Name = "User_Escalation_RchTxtBx"
        Me.User_Escalation_RchTxtBx.Size = New System.Drawing.Size(1013, 131)
        Me.User_Escalation_RchTxtBx.TabIndex = 0
        Me.User_Escalation_RchTxtBx.Text = ""
        '
        'Escalation_Pnl
        '
        Me.Escalation_Pnl.Controls.Add(Me.Tack_Screen_Snapshot_ChkBx)
        Me.Escalation_Pnl.Controls.Add(Me.Tack_Screen_Snapshot_Lbl)
        Me.Escalation_Pnl.Controls.Add(Me.With_Current_StickyNote_ChkBx)
        Me.Escalation_Pnl.Controls.Add(Me.With_Current_StickyNote_Lbl)
        Me.Escalation_Pnl.Controls.Add(Me.Send_Escalation_To_Author_Btn)
        Me.Escalation_Pnl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Escalation_Pnl.Location = New System.Drawing.Point(0, 159)
        Me.Escalation_Pnl.Name = "Escalation_Pnl"
        Me.Escalation_Pnl.Size = New System.Drawing.Size(1013, 39)
        Me.Escalation_Pnl.TabIndex = 1
        '
        'Tack_Screen_Snapshot_ChkBx
        '
        Me.Tack_Screen_Snapshot_ChkBx.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Tack_Screen_Snapshot_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.Tack_Screen_Snapshot_ChkBx.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Tack_Screen_Snapshot_ChkBx.Location = New System.Drawing.Point(249, 9)
        Me.Tack_Screen_Snapshot_ChkBx.Name = "Tack_Screen_Snapshot_ChkBx"
        Me.Tack_Screen_Snapshot_ChkBx.Size = New System.Drawing.Size(161, 20)
        Me.Tack_Screen_Snapshot_ChkBx.TabIndex = 1069
        Me.Tack_Screen_Snapshot_ChkBx.Text = "Tack Screen Snapshot"
        Me.Tack_Screen_Snapshot_ChkBx.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Tack_Screen_Snapshot_ChkBx.UseVisualStyleBackColor = False
        '
        'Tack_Screen_Snapshot_Lbl
        '
        Me.Tack_Screen_Snapshot_Lbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Tack_Screen_Snapshot_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Tack_Screen_Snapshot_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tack_Screen_Snapshot_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Tack_Screen_Snapshot_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Tack_Screen_Snapshot_Lbl.Location = New System.Drawing.Point(246, 8)
        Me.Tack_Screen_Snapshot_Lbl.Name = "Tack_Screen_Snapshot_Lbl"
        Me.Tack_Screen_Snapshot_Lbl.Size = New System.Drawing.Size(167, 22)
        Me.Tack_Screen_Snapshot_Lbl.TabIndex = 1068
        Me.Tack_Screen_Snapshot_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'With_Current_StickyNote_ChkBx
        '
        Me.With_Current_StickyNote_ChkBx.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.With_Current_StickyNote_ChkBx.BackColor = System.Drawing.SystemColors.Window
        Me.With_Current_StickyNote_ChkBx.Location = New System.Drawing.Point(604, 9)
        Me.With_Current_StickyNote_ChkBx.Name = "With_Current_StickyNote_ChkBx"
        Me.With_Current_StickyNote_ChkBx.Size = New System.Drawing.Size(161, 20)
        Me.With_Current_StickyNote_ChkBx.TabIndex = 1067
        Me.With_Current_StickyNote_ChkBx.Text = "With Current StickyNote"
        Me.With_Current_StickyNote_ChkBx.UseVisualStyleBackColor = False
        '
        'With_Current_StickyNote_Lbl
        '
        Me.With_Current_StickyNote_Lbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.With_Current_StickyNote_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.With_Current_StickyNote_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.With_Current_StickyNote_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.With_Current_StickyNote_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.With_Current_StickyNote_Lbl.Location = New System.Drawing.Point(600, 8)
        Me.With_Current_StickyNote_Lbl.Name = "With_Current_StickyNote_Lbl"
        Me.With_Current_StickyNote_Lbl.Size = New System.Drawing.Size(167, 22)
        Me.With_Current_StickyNote_Lbl.TabIndex = 1066
        Me.With_Current_StickyNote_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Send_Escalation_To_Author_Btn
        '
        Me.Send_Escalation_To_Author_Btn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Send_Escalation_To_Author_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Send_Escalation_To_Author_Btn.Location = New System.Drawing.Point(413, 7)
        Me.Send_Escalation_To_Author_Btn.Name = "Send_Escalation_To_Author_Btn"
        Me.Send_Escalation_To_Author_Btn.Size = New System.Drawing.Size(187, 24)
        Me.Send_Escalation_To_Author_Btn.TabIndex = 1065
        Me.Send_Escalation_To_Author_Btn.Text = "Send Escalation To Author"
        Me.Send_Escalation_To_Author_Btn.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Escalation_Auther_Mail_Lbl)
        Me.Panel1.Controls.Add(Me.Escalation_Auther_Mail_TxtBx)
        Me.Panel1.Controls.Add(Me.Escalation_Label_Lbl)
        Me.Panel1.Controls.Add(Me.Escalation_Label_TxtBx)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1013, 28)
        Me.Panel1.TabIndex = 2
        '
        'Escalation_Auther_Mail_Lbl
        '
        Me.Escalation_Auther_Mail_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Escalation_Auther_Mail_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Escalation_Auther_Mail_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Escalation_Auther_Mail_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Escalation_Auther_Mail_Lbl.Location = New System.Drawing.Point(1, 2)
        Me.Escalation_Auther_Mail_Lbl.Name = "Escalation_Auther_Mail_Lbl"
        Me.Escalation_Auther_Mail_Lbl.Size = New System.Drawing.Size(157, 23)
        Me.Escalation_Auther_Mail_Lbl.TabIndex = 965
        Me.Escalation_Auther_Mail_Lbl.Text = "Escalation Auther Mail"
        Me.Escalation_Auther_Mail_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Escalation_Auther_Mail_TxtBx
        '
        Me.Escalation_Auther_Mail_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Escalation_Auther_Mail_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Escalation_Auther_Mail_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Escalation_Auther_Mail_TxtBx.Location = New System.Drawing.Point(159, 2)
        Me.Escalation_Auther_Mail_TxtBx.Multiline = True
        Me.Escalation_Auther_Mail_TxtBx.Name = "Escalation_Auther_Mail_TxtBx"
        Me.Escalation_Auther_Mail_TxtBx.Size = New System.Drawing.Size(286, 23)
        Me.Escalation_Auther_Mail_TxtBx.TabIndex = 966
        '
        'Escalation_Label_Lbl
        '
        Me.Escalation_Label_Lbl.BackColor = System.Drawing.SystemColors.Window
        Me.Escalation_Label_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Escalation_Label_Lbl.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.Escalation_Label_Lbl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Escalation_Label_Lbl.Location = New System.Drawing.Point(448, 2)
        Me.Escalation_Label_Lbl.Name = "Escalation_Label_Lbl"
        Me.Escalation_Label_Lbl.Size = New System.Drawing.Size(150, 23)
        Me.Escalation_Label_Lbl.TabIndex = 963
        Me.Escalation_Label_Lbl.Text = "Escalation Label"
        Me.Escalation_Label_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Escalation_Label_TxtBx
        '
        Me.Escalation_Label_TxtBx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Escalation_Label_TxtBx.BackColor = System.Drawing.SystemColors.Window
        Me.Escalation_Label_TxtBx.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Escalation_Label_TxtBx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Escalation_Label_TxtBx.Location = New System.Drawing.Point(599, 2)
        Me.Escalation_Label_TxtBx.Multiline = True
        Me.Escalation_Label_TxtBx.Name = "Escalation_Label_TxtBx"
        Me.Escalation_Label_TxtBx.Size = New System.Drawing.Size(411, 23)
        Me.Escalation_Label_TxtBx.TabIndex = 964
        '
        'Read_Me_Pnl
        '
        Me.Read_Me_Pnl.BackColor = System.Drawing.Color.Transparent
        Me.Read_Me_Pnl.BackgroundImage = CType(resources.GetObject("Read_Me_Pnl.BackgroundImage"), System.Drawing.Image)
        Me.Read_Me_Pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Read_Me_Pnl.Controls.Add(Me.Windows_Volume_TrckBr)
        Me.Read_Me_Pnl.Controls.Add(Me.Button1)
        Me.Read_Me_Pnl.Controls.Add(Me.Arabic_Btn)
        Me.Read_Me_Pnl.Controls.Add(Me.English_Btn)
        Me.Read_Me_Pnl.Controls.Add(Me.Volume_TrcBr)
        Me.Read_Me_Pnl.Controls.Add(Me.Speaking_Rate_TrcBr)
        Me.Read_Me_Pnl.Controls.Add(Me.Speaking_Rate_Lbl)
        Me.Read_Me_Pnl.Controls.Add(Me.Volume_Lbl)
        Me.Read_Me_Pnl.Controls.Add(Me.cmbInstalled)
        Me.Read_Me_Pnl.Controls.Add(Me.Installed_Voices_Lbl)
        Me.Read_Me_Pnl.Controls.Add(Me.Pause_Btn)
        Me.Read_Me_Pnl.Controls.Add(Me.Start_Btn)
        Me.Read_Me_Pnl.Controls.Add(Me.Stop_Btn)
        Me.Read_Me_Pnl.Controls.Add(Me.Language_Lbl)
        Me.Read_Me_Pnl.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.Read_Me_Pnl.Location = New System.Drawing.Point(534, 227)
        Me.Read_Me_Pnl.Name = "Read_Me_Pnl"
        Me.Read_Me_Pnl.Size = New System.Drawing.Size(346, 169)
        Me.Read_Me_Pnl.TabIndex = 1063
        Me.Read_Me_Pnl.Visible = False
        '
        'Windows_Volume_TrckBr
        '
        Me.Windows_Volume_TrckBr.AutoSize = False
        Me.Windows_Volume_TrckBr.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.Windows_Volume_TrckBr.LargeChange = 1
        Me.Windows_Volume_TrckBr.Location = New System.Drawing.Point(115, 89)
        Me.Windows_Volume_TrckBr.Maximum = 50
        Me.Windows_Volume_TrckBr.Name = "Windows_Volume_TrckBr"
        Me.Windows_Volume_TrckBr.Size = New System.Drawing.Size(92, 23)
        Me.Windows_Volume_TrckBr.TabIndex = 1061
        Me.Windows_Volume_TrckBr.Value = 25
        '
        'Arabic_Btn
        '
        Me.Arabic_Btn.BackColor = System.Drawing.Color.Transparent
        Me.Arabic_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Arabic_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Arabic_Btn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Arabic_Btn.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Arabic_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Arabic_Btn.Location = New System.Drawing.Point(210, 64)
        Me.Arabic_Btn.Name = "Arabic_Btn"
        Me.Arabic_Btn.Size = New System.Drawing.Size(125, 26)
        Me.Arabic_Btn.TabIndex = 1059
        Me.Arabic_Btn.Text = "تحميل اصوات عربى"
        Me.Arabic_Btn.UseVisualStyleBackColor = False
        '
        'English_Btn
        '
        Me.English_Btn.BackColor = System.Drawing.Color.Transparent
        Me.English_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.English_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.English_Btn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.English_Btn.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.English_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.English_Btn.Location = New System.Drawing.Point(210, 41)
        Me.English_Btn.Name = "English_Btn"
        Me.English_Btn.Size = New System.Drawing.Size(125, 26)
        Me.English_Btn.TabIndex = 1058
        Me.English_Btn.Text = "Load English Voices"
        Me.English_Btn.UseVisualStyleBackColor = False
        '
        'Volume_TrcBr
        '
        Me.Volume_TrcBr.AutoSize = False
        Me.Volume_TrcBr.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.Volume_TrcBr.Location = New System.Drawing.Point(115, 67)
        Me.Volume_TrcBr.Maximum = 100
        Me.Volume_TrcBr.Name = "Volume_TrcBr"
        Me.Volume_TrcBr.Size = New System.Drawing.Size(92, 23)
        Me.Volume_TrcBr.TabIndex = 1056
        Me.Volume_TrcBr.Value = 50
        '
        'Speaking_Rate_TrcBr
        '
        Me.Speaking_Rate_TrcBr.AutoSize = False
        Me.Speaking_Rate_TrcBr.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.Speaking_Rate_TrcBr.Location = New System.Drawing.Point(115, 42)
        Me.Speaking_Rate_TrcBr.Minimum = -10
        Me.Speaking_Rate_TrcBr.Name = "Speaking_Rate_TrcBr"
        Me.Speaking_Rate_TrcBr.Size = New System.Drawing.Size(92, 23)
        Me.Speaking_Rate_TrcBr.TabIndex = 1055
        '
        'Speaking_Rate_Lbl
        '
        Me.Speaking_Rate_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Speaking_Rate_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Speaking_Rate_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Speaking_Rate_Lbl.ForeColor = System.Drawing.Color.White
        Me.Speaking_Rate_Lbl.Location = New System.Drawing.Point(11, 42)
        Me.Speaking_Rate_Lbl.Name = "Speaking_Rate_Lbl"
        Me.Speaking_Rate_Lbl.Size = New System.Drawing.Size(103, 22)
        Me.Speaking_Rate_Lbl.TabIndex = 1054
        Me.Speaking_Rate_Lbl.Text = "Speaking Rate"
        Me.Speaking_Rate_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Volume_Lbl
        '
        Me.Volume_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Volume_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Volume_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Volume_Lbl.ForeColor = System.Drawing.Color.White
        Me.Volume_Lbl.Location = New System.Drawing.Point(11, 67)
        Me.Volume_Lbl.Name = "Volume_Lbl"
        Me.Volume_Lbl.Size = New System.Drawing.Size(103, 46)
        Me.Volume_Lbl.TabIndex = 1053
        Me.Volume_Lbl.Text = "Volume"
        Me.Volume_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbInstalled
        '
        Me.cmbInstalled.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbInstalled.Font = New System.Drawing.Font("Times New Roman", 7.25!)
        Me.cmbInstalled.FormattingEnabled = True
        Me.cmbInstalled.Location = New System.Drawing.Point(115, 18)
        Me.cmbInstalled.Name = "cmbInstalled"
        Me.cmbInstalled.Size = New System.Drawing.Size(198, 20)
        Me.cmbInstalled.TabIndex = 1052
        '
        'Installed_Voices_Lbl
        '
        Me.Installed_Voices_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Installed_Voices_Lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Installed_Voices_Lbl.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Installed_Voices_Lbl.ForeColor = System.Drawing.Color.White
        Me.Installed_Voices_Lbl.Location = New System.Drawing.Point(11, 18)
        Me.Installed_Voices_Lbl.Name = "Installed_Voices_Lbl"
        Me.Installed_Voices_Lbl.Size = New System.Drawing.Size(103, 22)
        Me.Installed_Voices_Lbl.TabIndex = 1051
        Me.Installed_Voices_Lbl.Text = "Installed Voices"
        Me.Installed_Voices_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Language_Lbl
        '
        Me.Language_Lbl.BackColor = System.Drawing.Color.Transparent
        Me.Language_Lbl.Font = New System.Drawing.Font("Tahoma", 18.0!)
        Me.Language_Lbl.ForeColor = System.Drawing.Color.White
        Me.Language_Lbl.Location = New System.Drawing.Point(314, 9)
        Me.Language_Lbl.Name = "Language_Lbl"
        Me.Language_Lbl.Size = New System.Drawing.Size(29, 32)
        Me.Language_Lbl.TabIndex = 1057
        Me.Language_Lbl.Text = "E"
        Me.Language_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Stickies_Notes_TbCntrl
        '
        Me.Stickies_Notes_TbCntrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Stickies_Notes_TbCntrl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.Stickies_Notes_TbCntrl.Location = New System.Drawing.Point(34, 124)
        Me.Stickies_Notes_TbCntrl.Multiline = True
        Me.Stickies_Notes_TbCntrl.Name = "Stickies_Notes_TbCntrl"
        Me.Stickies_Notes_TbCntrl.SelectedIndex = 0
        Me.Stickies_Notes_TbCntrl.Size = New System.Drawing.Size(1021, 407)
        Me.Stickies_Notes_TbCntrl.TabIndex = 1065
        '
        'Spliter_1_Lbl
        '
        Me.Spliter_1_Lbl.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Spliter_1_Lbl.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.Spliter_1_Lbl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Spliter_1_Lbl.Location = New System.Drawing.Point(34, 528)
        Me.Spliter_1_Lbl.Name = "Spliter_1_Lbl"
        Me.Spliter_1_Lbl.Size = New System.Drawing.Size(1021, 3)
        Me.Spliter_1_Lbl.TabIndex = 1066
        '
        'Azan_Tmr
        '
        Me.Azan_Tmr.Interval = 1000
        '
        'Alarm_Tmr
        '
        Me.Alarm_Tmr.Interval = 1000
        '
        'New_Alarm_Btn
        '
        Me.New_Alarm_Btn.BackgroundImage = CType(resources.GetObject("New_Alarm_Btn.BackgroundImage"), System.Drawing.Image)
        Me.New_Alarm_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.New_Alarm_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.New_Alarm_Btn.FlatAppearance.BorderSize = 0
        Me.New_Alarm_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.New_Alarm_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.New_Alarm_Btn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.New_Alarm_Btn.Location = New System.Drawing.Point(5, 104)
        Me.New_Alarm_Btn.Name = "New_Alarm_Btn"
        Me.New_Alarm_Btn.Size = New System.Drawing.Size(28, 28)
        Me.New_Alarm_Btn.TabIndex = 1113
        Me.New_Alarm_Btn.TabStop = False
        Me.Form_ToolTip.SetToolTip(Me.New_Alarm_Btn, "Next Sticky Note")
        Me.New_Alarm_Btn.UseVisualStyleBackColor = True
        '
        'Sticky_Note_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkMagenta
        Me.ClientSize = New System.Drawing.Size(1055, 761)
        Me.Controls.Add(Me.Hide_Me_PctrBx)
        Me.Controls.Add(Me.Form_Transparency_TrkBr)
        Me.Controls.Add(Me.MsgBox_SttsStrp)
        Me.Controls.Add(Me.Spliter_1_Lbl)
        Me.Controls.Add(Me.Table_Pnl)
        Me.Controls.Add(Me.Stickies_Notes_TbCntrl)
        Me.Controls.Add(Me.Read_Me_Pnl)
        Me.Controls.Add(Me.Sticky_Navigater_Pnl)
        Me.Controls.Add(Me.Setting_TbCntrl)
        Me.Controls.Add(Me.Stiky_TlStrp)
        Me.Controls.Add(Me.MagNote_PctrBx)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Times New Roman", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Sticky_Note_Form"
        Me.ShowInTaskbar = False
        Me.Text = "Sticky Note"
        Me.TransparencyKey = System.Drawing.Color.DarkMagenta
        Me.File_TlStrpMnItm.ResumeLayout(False)
        Me.Column_CntxtMnStrp.ResumeLayout(False)
        Me.Cell_CntxtMnStrp.ResumeLayout(False)
        Me.Row_CntxtMnStrp.ResumeLayout(False)
        Me.Stiky_TlStrp.ResumeLayout(False)
        Me.Stiky_TlStrp.PerformLayout()
        CType(Me.Reminder_Every_Minutes_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Reminder_Every_Hours_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Reminder_Every_Days_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Minutes_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Hours_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Days_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Time_To_Alert_Before_Azan_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Alert_Repeet_Minute_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Alert_Repeet_Hour_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Alert_Repeet_Day_NmrcUpDn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Hide_Me_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MagNote_PctrBx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Table_Pnl.ResumeLayout(False)
        Me.Table_Pnl.PerformLayout()
        Me.Table_Grid_Pnl.ResumeLayout(False)
        CType(Me.Table_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Form_Transparency_TrkBr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MsgBox_SttsStrp.ResumeLayout(False)
        Me.MsgBox_SttsStrp.PerformLayout()
        Me.Sticky_Navigater_Pnl.ResumeLayout(False)
        Me.Setting_TbCntrl.ResumeLayout(False)
        Me.Sticky_Notes_TbPg.ResumeLayout(False)
        CType(Me.Available_Sticky_Notes_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Sticky_Notes_Header_Pnl.ResumeLayout(False)
        Me.Sticky_Parameters_TbPg.ResumeLayout(False)
        Me.Sticky_Parameters_TbPg.PerformLayout()
        Me.Form_Parameters_TbPg.ResumeLayout(False)
        Me.Form_Parameters_TbPg.PerformLayout()
        Me.Shortcuts_TbPg.ResumeLayout(False)
        Me.Prayer_Time_TbPg.ResumeLayout(False)
        Me.Prayer_Time_TbPg.PerformLayout()
        Me.Alarm_Time_TbPg.ResumeLayout(False)
        CType(Me.Available_Alerts_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Available_Alarm_Header_Pnl.ResumeLayout(False)
        Me.Alarm_Setting_Pnl.ResumeLayout(False)
        Me.Alarm_Setting_Pnl.PerformLayout()
        Me.Alarm_Repeet_Time_Pnl.ResumeLayout(False)
        Me.Escalation_To_Author_TbPg.ResumeLayout(False)
        Me.Escalation_Pnl.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Read_Me_Pnl.ResumeLayout(False)
        CType(Me.Windows_Volume_TrckBr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Volume_TrcBr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Speaking_Rate_TrcBr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents File_TlStrpMnItm As ContextMenuStrip
    Friend WithEvents CtxMenu_Col_Insert_Columns As ToolStripMenuItem
    Friend WithEvents Column_CntxtMnStrp As ContextMenuStrip
    Friend WithEvents Cell_CntxtMnStrp As ContextMenuStrip
    Friend WithEvents Row_CntxtMnStrp As ContextMenuStrip
    'Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem


    Friend WithEvents Col_Cut_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Copy_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Paste_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Insert_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Delete_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Reset_To_Default_Width_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Width_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Hide_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Unhide_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Filter_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Clear_Filter_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Group_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_UnGroup_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_UnGroup_All_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Insert_Page_Break_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Remove_Page_Break_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Properties_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_Format_Cells_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Col_TlStrpSprtr_01 As ToolStripSeparator
    Friend WithEvents Col_TlStrpSprtr_02 As ToolStripSeparator
    Friend WithEvents Col_TlStrpSprtr_03 As ToolStripSeparator
    Friend WithEvents Col_TlStrpSprtr_04 As ToolStripSeparator
    Friend WithEvents Col_TlStrpSprtr_05 As ToolStripSeparator

    Friend WithEvents Row_Cut_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Copy_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Paste_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Insert_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Delete_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Reset_To_Default_Height_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Height_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Hide_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_UnHide_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Group_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_UnGroup_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_UnGroup_All_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Insert_Page_Break_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Remove_Page_Break_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Properties_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_Format_Cells_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Row_TlStrpSprtr_01 As ToolStripSeparator
    Friend WithEvents Row_TlStrpSprtr_02 As ToolStripSeparator
    Friend WithEvents Row_TlStrpSprtr_03 As ToolStripSeparator
    Friend WithEvents Row_TlStrpSprtr_04 As ToolStripSeparator
    Friend WithEvents Row_TlStrpSprtr_05 As ToolStripSeparator

    Friend WithEvents Cell_Cut_Range_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cell_Copy_Range_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cell_Paste_Range_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cell_Merge_Range_TlStrpMnItm As ToolStripMenuItem

    Friend WithEvents Cell_TlStrpSprtr_102 As ToolStripSeparator
    Friend WithEvents Close_Grid_TlStrpMnItm As ToolStripMenuItem



    Friend WithEvents Cell_UnMerge_Range_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cell_Change_Type_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cell_Format_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cell_TlStrpSprtr_01 As ToolStripSeparator
    Friend WithEvents Cell_TlStrpSprtr_02 As ToolStripSeparator
    Friend WithEvents Cell_TlStrpSprtr_03 As ToolStripSeparator
    Friend WithEvents Cell_TlStrpSprtr_04 As ToolStripSeparator
    Friend WithEvents BottomToolStripPanel As ToolStripPanel
    Friend WithEvents TopToolStripPanel As ToolStripPanel
    Friend WithEvents RightToolStripPanel As ToolStripPanel
    Friend WithEvents LeftToolStripPanel As ToolStripPanel
    Friend WithEvents ContentPanel As ToolStripContentPanel
    Friend WithEvents Cell_Select_All_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Backup_Timer As Timer
    Friend WithEvents Stiky_TlStrp As ToolStrip
    Friend WithEvents Compress_Me_TlStrpBtn As ToolStripButton
    Friend WithEvents New_Sticky_TlStrpBtn As ToolStripButton
    Friend WithEvents Open_Sticky_TlStrpBtn As ToolStripButton
    Friend WithEvents Save_Sticky_TlStrpBtn As ToolStripButton
    Friend WithEvents Delete_TlStrpBtn As ToolStripButton
    Friend WithEvents Print_Sticky_TlStrpBtn As ToolStripButton
    Friend WithEvents Stiky_tlStrpSprtr_01 As ToolStripSeparator
    Friend WithEvents Font_Name_Sticky_TlStrpSpltBtn As ToolStripSplitButton
    Friend WithEvents Select_Font_Sticky_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Text_Color_Sticky_TlStrpSpltBtn As ToolStripSplitButton
    Friend WithEvents Selected_Text_Color_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Selected_Text_Backcolor_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Bold_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Underline_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents WordWrap_TlStrp As ToolStripButton
    Friend WithEvents Font_Strikeout_TlStrpBtn As ToolStripButton
    Friend WithEvents Stiky_tlStrpSprtr_02 As ToolStripSeparator
    Friend WithEvents Read_ME_TlStrpBtn As ToolStripButton
    Friend WithEvents Stiky_tlStrpSprtr_03 As ToolStripSeparator
    Friend WithEvents Left_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Center_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Right_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Stiky_tlStrpSprtr_04 As ToolStripSeparator
    Friend WithEvents Bullets_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Stiky_tlStrpSprtr_05 As ToolStripSeparator
    Friend WithEvents Find_TlStrpBtn As ToolStripButton
    Friend WithEvents Cut_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Copy_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Paste_Stiky_TlStrpBtn As ToolStripButton
    Friend WithEvents Stiky_tlStrpSprtr_06 As ToolStripSeparator
    Friend WithEvents Form_Size_TlStrpBtn As ToolStripButton
    Friend WithEvents Exit_TlStrpBtn As ToolStripButton
    Friend WithEvents Form_ToolTip As ToolTip
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents Table_Pnl As Panel
    Friend WithEvents Exit_Table_Pnl_Btn As Button
    Friend WithEvents Insert_Btn As Button
    Friend WithEvents Cell_Height_TxtBx As TextBox
    Friend WithEvents Cell_Width_TxtBx As TextBox
    Friend WithEvents Rows_TxtBx As TextBox
    Friend WithEvents Columns_TxtBx As TextBox
    Friend WithEvents Table_Grid_Pnl As Panel
    Friend WithEvents Columns_Count_VScrllBr As HScrollBar
    Friend WithEvents Table_DGV As DataGridView
    Friend WithEvents Cell_Width_VScrllBr As HScrollBar
    Friend WithEvents Rows_Count_VScrllBr As VScrollBar
    Friend WithEvents Cell_Height_VScrllBr As VScrollBar
    Friend WithEvents Grid_Sample_Lbl As Label
    Friend WithEvents Cell_Height_Lbl As Label
    Friend WithEvents Cell_Width_Lbl As Label
    Friend WithEvents Rows_Lbl As Label
    Friend WithEvents Columns_Lbl As Label
    Friend WithEvents MsgBox_SttsStrp As StatusStrip
    Friend WithEvents MsgBox_TlStrpSttsLbl As ToolStripStatusLabel
    Friend WithEvents Form_Transparency_TrkBr As TrackBar
    Friend WithEvents Sticky_Navigater_Pnl As Panel
    Friend WithEvents Sticky_Note_Category_CmbBx As ComboBox
    Friend WithEvents WhatsApp_Btn As Button
    Friend WithEvents Previous_Btn As Button
    Friend WithEvents Next_Btn As Button
    Friend WithEvents Language_Btn As Button
    Friend WithEvents Sticky_Note_Lbl As Label
    Friend WithEvents Sticky_Note_No_CmbBx As ComboBox
    Friend WithEvents Add_New_Sticky_Note_Btn As Button
    Friend WithEvents Setting_TbCntrl As TabControl
    Friend WithEvents Sticky_Notes_TbPg As TabPage
    Friend WithEvents Available_Sticky_Notes_DGV As DataGridView
    Friend WithEvents Sticky_Notes_Header_Pnl As Panel
    Friend WithEvents Available_Sticky_Notes_Lbl As Label
    Friend WithEvents View_Sticky_Notes_Btn As Button
    Friend WithEvents Preview_Btn As Button
    Friend WithEvents Sticky_Parameters_TbPg As TabPage
    Friend WithEvents Sticky_Word_Wrap_ChkBx As CheckBox
    Friend WithEvents Sticky_Word_Wrap_Lbl As Label
    Friend WithEvents Pending_Reminder_Alert_ChkBx As CheckBox
    Friend WithEvents Stop_Reminder_Alert_Lbl As Label
    Friend WithEvents Next_Reminder_Time_DtTmPkr As DateTimePicker
    Friend WithEvents Reminder_Every_Minutes_NmrcUpDn As NumericUpDown
    Friend WithEvents Reminder_Every_Hours_NmrcUpDn As NumericUpDown
    Friend WithEvents Reminder_Every_Days_NmrcUpDn As NumericUpDown
    Friend WithEvents Reminder_Every_Lbl As Label
    Friend WithEvents Next_Reminder_Time_Lbl As Label
    Friend WithEvents Sticky_Have_Reminder_ChkBx As CheckBox
    Friend WithEvents Sticky_Have_Reminder_Lbl As Label
    Friend WithEvents Use_Main_Password_ChkBx As CheckBox
    Friend WithEvents Use_Main_Password_Lbl As Label
    Friend WithEvents Sticky_Applicable_To_Rename_ChkBx As CheckBox
    Friend WithEvents Sticky_Applicable_To_Rename_Lbl As Label
    Friend WithEvents Sticky_Password_Lbl As Label
    Friend WithEvents Sticky_Password_TxtBx As TextBox
    Friend WithEvents Secured_Sticky_ChkBx As CheckBox
    Friend WithEvents Secured_Sticky_Lbl As Label
    Friend WithEvents Sticky_Back_Color_Lbl As Label
    Friend WithEvents Finished_Sticky_ChkBx As CheckBox
    Friend WithEvents View_Text_Font_Properties_Btn As Button
    Friend WithEvents Blocked_Sticky_ChkBx As CheckBox
    Friend WithEvents Sticky_Font_TxtBx As TextBox
    Friend WithEvents Blocked_Sticky_Lbl As Label
    Friend WithEvents Sticky_Font_Lbl As Label
    Friend WithEvents Finished_Sticky_Lbl As Label
    Friend WithEvents Sticky_Font_Color_Lbl As Label
    Friend WithEvents Form_Parameters_TbPg As TabPage
    Friend WithEvents Stop_Displaying_Controls_ToolTip_ChkBx As CheckBox
    Friend WithEvents Stop_Displaying_Controls_ToolTip_Lbl As Label
    Friend WithEvents Me_Always_On_Top_ChkBx As CheckBox
    Friend WithEvents Me_Always_On_Top_Lbl As Label
    Friend WithEvents Remember_Opened_External_Files_ChkBx As CheckBox
    Friend WithEvents Remember_Opened_External_Files_Lbl As Label
    Friend WithEvents Me_As_Default_Text_File_Editor_ChkBx As CheckBox
    Friend WithEvents Me_As_Default_Text_File_Editor_Lbl As Label
    Friend WithEvents Minimize_After_Running_My_Shortcut_ChkBx As CheckBox
    Friend WithEvents Minimize_After_Running_My_Shortcut_Lbl As Label
    Friend WithEvents Me_Is_Compressed_ChkBx As CheckBox
    Friend WithEvents Me_Is_Compressed_Lbl As Label
    Friend WithEvents Show_Sticky_Tab_Control_ChkBx As CheckBox
    Friend WithEvents Show_Sticky_Tab_Control_Lbl As Label
    Friend WithEvents Enable_Maximize_Box_ChkBx As CheckBox
    Friend WithEvents Enable_Maximize_Box_Lbl As Label
    Friend WithEvents Show_Form_Border_Style_ChkBx As CheckBox
    Friend WithEvents Show_Form_Border_Style_Lbl As Label
    Friend WithEvents Double_Click_To_Run_Shortcut_ChkBx As CheckBox
    Friend WithEvents Double_Click_To_Run_Shortcut_Lbl As Label
    Friend WithEvents Warning_Before_Delete_ChkBx As CheckBox
    Friend WithEvents Warning_Before_Delete_Lbl As Label
    Friend WithEvents Warning_Before_Save_ChkBx As CheckBox
    Friend WithEvents Warning_Before_Save_Lbl As Label
    Friend WithEvents Set_Control_To_Fill_ChkBx As CheckBox
    Friend WithEvents Set_Control_To_Fill_Lbl As Label
    Friend WithEvents Main_Password_Lbl As Label
    Friend WithEvents Main_Password_TxtBx As TextBox
    Friend WithEvents Complex_Password_ChkBx As CheckBox
    Friend WithEvents Complex_Password_Lbl As Label
    Friend WithEvents Enter_Password_To_Pass_ChkBx As CheckBox
    Friend WithEvents Enter_Password_To_Pass_Lbl As Label
    Friend WithEvents Reload_Stickies_After_Amendments_ChkBx As CheckBox
    Friend WithEvents Reload_Stickies_After_Amendments_Lbl As Label
    Friend WithEvents Backup_Folder_Path_Btn As Button
    Friend WithEvents Backup_Folder_Path_TxtBx As TextBox
    Friend WithEvents Backup_Folder_Path_Lbl As Label
    Friend WithEvents Next_Backup_Time_TxtBx As TextBox
    Friend WithEvents Next_Backup_Time_Lbl As Label
    Friend WithEvents Minutes_NmrcUpDn As NumericUpDown
    Friend WithEvents Hours_NmrcUpDn As NumericUpDown
    Friend WithEvents Days_NmrcUpDn As NumericUpDown
    Friend WithEvents Backup_Every_Lbl As Label
    Friend WithEvents Periodically_Backup_Stickies_ChkBx As CheckBox
    Friend WithEvents Periodicaly_Backup_Stickies_Lbl As Label
    Friend WithEvents Save_Setting_When_Exit_ChkBx As CheckBox
    Friend WithEvents Save_Setting_When_Exit_Lbl As Label
    Friend WithEvents Form_Color_Like_Sticky_ChkBx As CheckBox
    Friend WithEvents Form_Color_Like_Sticky_Color_Lbl As Label
    Friend WithEvents Save_Sticky_Form_Parameter_Setting_Btn As Button
    Friend WithEvents Sticky_Form_Opacity_TxtBx As TextBox
    Friend WithEvents Sticky_Form_Size_Lbl As Label
    Friend WithEvents Sticky_Form_Opacity_Lbl As Label
    Friend WithEvents Sticky_Form_Size_TxtBx As TextBox
    Friend WithEvents Sticky_Form_Location_TxtBx As TextBox
    Friend WithEvents Sticky_Form_Location_Lbl As Label
    Friend WithEvents Hide_Finished_Sticky_Note_ChkBx As CheckBox
    Friend WithEvents Run_Me_At_Windows_Startup_ChkBx As CheckBox
    Friend WithEvents Hide_Finished_Sticky_Note_Lbl As Label
    Friend WithEvents Run_Me_At_Windows_Startup_Lbl As Label
    Friend WithEvents Sticky_Form_Color_Lbl As Label
    Friend WithEvents Shortcuts_TbPg As TabPage
    Friend WithEvents Prayer_Time_TbPg As TabPage
    Friend WithEvents Left_Time_Lbl As Label
    Friend WithEvents Date_DtTmPkr As DateTimePicker
    Friend WithEvents Date_Lbl As Label
    Friend WithEvents Longitude_TxtBx As TextBox
    Friend WithEvents Longitude_Lbl As Label
    Friend WithEvents Latitude_TxtBx As TextBox
    Friend WithEvents Latitude_Lbl As Label
    Friend WithEvents City_CmbBx As ComboBox
    Friend WithEvents City_Lbl As Label
    Friend WithEvents Country_CmbBx As ComboBox
    Friend WithEvents Country_Lbl As Label
    Friend WithEvents Calculation_Methods_CmbBx As ComboBox
    Friend WithEvents Calculation_Methods_Lbl As Label
    Friend WithEvents Isha_TxtBx As TextBox
    Friend WithEvents Maghrib_TxtBx As TextBox
    Friend WithEvents Asr_TxtBx As TextBox
    Friend WithEvents Dhuhr_TxtBx As TextBox
    Friend WithEvents Sunrise_TxtBx As TextBox
    Friend WithEvents Fajr_TxtBx As TextBox
    Friend WithEvents Isha_Lbl As Label
    Friend WithEvents Maghrib_Lbl As Label
    Friend WithEvents Asr_Lbl As Label
    Friend WithEvents Dhuhr_Lbl As Label
    Friend WithEvents Sunrise_Lbl As Label
    Friend WithEvents Fajr_Lbl As Label
    Friend WithEvents Read_Me_Pnl As Panel
    Friend WithEvents Windows_Volume_TrckBr As TrackBar
    Friend WithEvents Button1 As Button
    Friend WithEvents Arabic_Btn As Button
    Friend WithEvents English_Btn As Button
    Friend WithEvents Volume_TrcBr As TrackBar
    Friend WithEvents Speaking_Rate_TrcBr As TrackBar
    Friend WithEvents Speaking_Rate_Lbl As Label
    Friend WithEvents Volume_Lbl As Label
    Friend WithEvents cmbInstalled As ComboBox
    Friend WithEvents Installed_Voices_Lbl As Label
    Friend WithEvents Pause_Btn As Button
    Friend WithEvents Start_Btn As Button
    Friend WithEvents Stop_Btn As Button
    Friend WithEvents Language_Lbl As Label
    Friend WithEvents Sticky_Font_Color_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Sticky_Back_Color_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Sticky_Form_Color_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents Sticky_Font_Size_CmbBx As ComboBox
    Friend WithEvents Sticky_Font_Name_CmbBx As ComboBox
    Friend WithEvents Sticky_Font_Style_CmbBx As ComboBox
    Friend WithEvents Sticky_Font_Style_Lbl As Label
    Friend WithEvents Sticky_Font_Size_Lbl As Label
    Friend WithEvents Sticky_Font_Name_Lbl As Label
    Friend WithEvents Upload_Last_Version_Btn As Button
    Friend WithEvents Stickies_Folder_Path_Btn As Button
    Friend WithEvents Stickies_Folder_Path_TxtBx As TextBox
    Friend WithEvents Stickies_Folder_Path_Lbl As Label
    Friend WithEvents Show_Hide_TlStrpSpltBtn As ToolStripSplitButton
    Friend WithEvents Show_Hide_Sticky_Note_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Show_Hide_Setting_Tab_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Show_Hide_Sticky_Grid_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Numeric_Cell_Format_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents DateTime_Cell_Format_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Currency_Cell_Format_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents General_Cell_Format_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Text_Cell_Format_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents General_Cell_Format_2_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Numeric_Cell_Format_2_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents DateTime_Cell_Format_2_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Currency_Cell_Format_2_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Text_Cell_Format_2_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents General_Cell_Format_1_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Numeric_Cell_Format_1_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents DateTime_Cell_Format_1_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Currency_Cell_Format_1_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Text_Cell_Format_1_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Stickies_Notes_TbCntrl As TabControl
    Friend WithEvents Open_Sticky_In_New_Tab_ChkBx As CheckBox
    Friend WithEvents Open_Sticky_In_New_Tab_Lbl As Label
    Friend WithEvents Spliter_1_Lbl As Panel
    Friend WithEvents Show_Control_Tab_Pages_In_Multi_Line_ChkBx As CheckBox
    Friend WithEvents Show_Control_Tab_Pages_In_Multi_Line_Lbl As Label
    Friend WithEvents Check_For_New_Version_ChkBx As CheckBox
    Friend WithEvents Check_For_New_Version_Lbl As Label
    Friend WithEvents Load_Sticky_Note_At_Startup_ChkBx As CheckBox
    Friend WithEvents Load_Sticky_Note_At_Startup_ChkBx_Lbl As Label
    Friend WithEvents File_Format_CmbBx As ComboBox
    Friend WithEvents Remove_Selected_Text_Color_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Remove_Selected_Text_Backcolor_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Cload_Area_Password_Lbl As Label
    Friend WithEvents Cload_Area_Password_TxtBx As TextBox
    Friend WithEvents Cload_Area_User_Lbl As Label
    Friend WithEvents Cload_Area_User_TxtBx As TextBox
    Friend WithEvents Cload_Area_Path_Lbl As Label
    Friend WithEvents Cload_Area_Path_TxtBx As TextBox
    Friend WithEvents Save_Copy_Of_Current_StickyNote_At_Cloud_Area_ChkBx As CheckBox
    Friend WithEvents Save_Copy_Of_Current_StickyNote_At_Cloud_Area_Lbl As Label
    Friend WithEvents Escalation_To_Author_TbPg As TabPage
    Friend WithEvents User_Escalation_RchTxtBx As RichTextBox
    Friend WithEvents Escalation_Pnl As Panel
    Friend WithEvents Send_Escalation_To_Author_Btn As Button
    Friend WithEvents Tack_Screen_Snapshot_ChkBx As CheckBox
    Friend WithEvents Tack_Screen_Snapshot_Lbl As Label
    Friend WithEvents With_Current_StickyNote_ChkBx As CheckBox
    Friend WithEvents With_Current_StickyNote_Lbl As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Escalation_Label_Lbl As Label
    Friend WithEvents Escalation_Label_TxtBx As TextBox
    Friend WithEvents Escalation_Auther_Mail_Lbl As Label
    Friend WithEvents Escalation_Auther_Mail_TxtBx As TextBox
    Friend WithEvents Reload_Sticky_Note_After_Change_Category_ChkBx As CheckBox
    Friend WithEvents Reload_Sticky_Note_After_Change_Category_Lbl As Label
    Friend WithEvents Run_Me_As_Administrator_ChkBx As CheckBox
    Friend WithEvents Run_Me_As_Administrator_Lbl As Label
    Friend WithEvents Clear_Previous_Search_Result_ChkBx As CheckBox
    Friend WithEvents Clear_Previous_Search_Result_Lbl As Label
    Friend WithEvents Setting_Tab_Control_Size_Lbl As Label
    Friend WithEvents Setting_Tab_Control_Size_TxtBx As TextBox
    Friend WithEvents Grid_Panel_Size_Lbl As Label
    Friend WithEvents Grid_Panel_Size_TxtBx As TextBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ShortCut_TbCntrl As TabControl
    Friend WithEvents Show_Hide_Stickies_Notes_TabControl_TlStrpMnItm As ToolStripMenuItem
    Friend WithEvents Button2 As Button
    Friend WithEvents Save_Day_Light_ChkBx As CheckBox
    Friend WithEvents Save_Day_Light_Lbl As Label
    Friend WithEvents Application_Starts_Minimized_ChkBx As CheckBox
    Friend WithEvents Application_Starts_Minimized_Lbl As Label
    Friend WithEvents Stop_Voice_Azan_File_Btn As Button
    Friend WithEvents Play_Voice_Azan_File_Btn As Button
    Friend WithEvents Fagr_Voice_Files_CmbBx As ComboBox
    Friend WithEvents Voice_Azan_Files_CmbBx As ComboBox
    Friend WithEvents Azan_Spoke_Method_ChkBx As CheckBox
    Friend WithEvents Azan_Spoke_Method_Lbl As Label
    Friend WithEvents Stop_Fagr_Voice_File_Btn As Button
    Friend WithEvents Play_Fagr_Voice_File_Btn As Button
    Friend WithEvents Azan_Activation_ChkBx As CheckBox
    Friend WithEvents Azan_Activation_Lbl As Label
    Friend WithEvents Azan_Tmr As Timer
    Friend WithEvents Azan_Takbeer_Only_ChkBx As CheckBox
    Friend WithEvents Azan_Takbeer_Only_Lbl As Label
    Friend WithEvents Time_To_Alert_Before_Azan_NmrcUpDn As NumericUpDown
    Friend WithEvents Time_To_Alert_Before_Azan_Lbl As Label
    Friend WithEvents Alert_Before_Azan_ChkBx As CheckBox
    Friend WithEvents Alert_Before_Azan_Lbl As Label
    Friend WithEvents Select_Alert_File_Path_Btn As Button
    Friend WithEvents Alert_File_Path_TxtBx As TextBox
    Friend WithEvents Alert_File_Path_Lbl As Label
    Friend WithEvents Time_Left_For_Alert_TxtBx As TextBox
    Friend WithEvents Alarm_Time_TbPg As TabPage
    Friend WithEvents Start_Alert_Time_DtTmPikr As DateTimePicker
    Friend WithEvents Start_Alert_Time_Lbl As Label
    Friend WithEvents Alert_Active_ChkBx As CheckBox
    Friend WithEvents Alert_Active_Lbl As Label
    Friend WithEvents Alert_One_Time_ChkBx As CheckBox
    Friend WithEvents Alert_One_Time_Lbl As Label
    Friend WithEvents Alert_Repeet_Time_Lbl As Label
    Friend WithEvents End_Alert_Time_DtTmPikr As DateTimePicker
    Friend WithEvents End_Alert_Time_Lbl As Label
    Friend WithEvents Preview_Available_Alerts_Btn As Button
    Friend WithEvents Available_Alerts_Lbl As Label
    Friend WithEvents Alarm_Setting_Pnl As Panel
    Friend WithEvents Available_Alerts_DGV As DataGridView
    Friend WithEvents Available_Alarm_Header_Pnl As Panel
    Friend WithEvents Clock1 As AnalogClock.Clock
    Friend WithEvents Clock2 As AnalogClock.Clock
    Friend WithEvents Alert_Repeet_Minute_NmrcUpDn As NumericUpDown
    Friend WithEvents Alert_Repeet_Hour_NmrcUpDn As NumericUpDown
    Friend WithEvents Alert_Repeet_Day_NmrcUpDn As NumericUpDown
    Friend WithEvents Stop_Playing_Alert_Btn As Button
    Friend WithEvents Play_Alert_Btn As Button
    Friend WithEvents Select_Alert_File_Btn As Button
    Friend WithEvents Alert_Voice_Path_TxtBx As TextBox
    Friend WithEvents Alert_Voice_Path_Lbl As Label
    Friend WithEvents Alarm_Repeet_Time_Pnl As Panel
    Friend WithEvents Alert_Time_Lbl As Label
    Friend WithEvents Save_Alert_Btn As Button
    Friend WithEvents Delete_Alert_Btn As Button
    Friend WithEvents Alarm_Tmr As Timer
    Friend WithEvents End_Alert_Time_Status_ChkBx As CheckBox
    Friend WithEvents External_Sticky_Font_Style_Lbl As Label
    Friend WithEvents External_Sticky_Font_Size_Lbl As Label
    Friend WithEvents External_Sticky_Font_Name_Lbl As Label
    Friend WithEvents External_Sticky_Font_Style_CmbBx As ComboBox
    Friend WithEvents External_Sticky_Font_Size_CmbBx As ComboBox
    Friend WithEvents External_Sticky_Font_Name_CmbBx As ComboBox
    Friend WithEvents External_Sticky_Font_Color_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents External_Sticky_Back_Color_ClrCmbBx As ColorsComboBox.ColorsComboBox
    Friend WithEvents External_Sticky_Back_Color_Lbl As Label
    Friend WithEvents View_External_Text_Font_Properties_Btn As Button
    Friend WithEvents External_Sticky_Font_TxtBx As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents External_Sticky_Font_Color_Lbl As Label
    Friend WithEvents Keep_Sticky_Opened_After_Delete_ChkBx As CheckBox
    Friend WithEvents Keep_Sticky_Opened_After_Delete_Lbl As Label
    Friend WithEvents Hide_Windows_Desktop_Icons_ChkBx As CheckBox
    Friend WithEvents Hide_Windows_Desktop_Icons_Lbl As Label
    Friend WithEvents Ask_If_Form_Is_Outside_Screen_Bounds_ChkBx As CheckBox
    Friend WithEvents Ask_If_Form_Is_Outside_Screen_Bounds_Lbl As Label
    Friend WithEvents Form_Is_Restricted_By_Screen_Bounds_ChkBx As CheckBox
    Friend WithEvents Form_Is_Restricted_By_Screen_Bounds_Lbl As Label
    Friend WithEvents MagNote_PctrBx As PictureBox
    Friend WithEvents Pause_Alarm_Timer_Btn As Button
    Friend WithEvents Hide_Me_PctrBx As PictureBox
    Friend WithEvents Alert_Comment_TxtBx As TextBox
    Friend WithEvents Next_Current_Alarm_Time_Lbl As Label
    Friend WithEvents New_Alarm_Btn As Button
End Class
