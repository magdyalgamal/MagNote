Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class RefreshWaitAWhileTitles_Class : Implements IDisposable

    Public SFBMF = Microsoft.VisualBasic.DateAndTime.Timer

    Public Function GetFieldValue(Optional ByVal TblNam As String = Nothing,
                                                Optional ByVal SQLWhere As String = "",
                                                Optional ByVal FieldName As String = Nothing,
                                                Optional ByVal NewSelectionForm As Boolean = False,
                                                Optional ByVal ClearDS As Boolean = True,
                                                Optional ByVal SelectTop As Boolean = False,
                                                Optional ByVal OpenConn As Boolean = True,
                                                Optional ByVal Ignore_Send_Mail As Boolean = False,
                                                Optional ByVal SortOrder As String = Nothing,
                                                Optional ByVal FieldsToSelect As String = "*",
                                                Optional ByVal IgnoreWithoutDoublication As Boolean = False,
                                                Optional ByVal ClearAfterLoading As Boolean = False) 'As String
        Try
            Dim GFVTimer = Microsoft.VisualBasic.DateAndTime.Timer + 1
ReRunGetFieldValue:
            If IsNothing(TblNam) Then
                Dim match As Match = Regex.Match(SQLWhere, "From\s+([A-Za-z0-9\.-_]+)\s*", RegexOptions.IgnoreCase)
                If (match.Success) Then
                    TblNam = match.Groups(1).Value
                End If
            End If
            If ClearDS Then 'تفريغ مجموعة جداول البيانات المجهزة من جميع البيانات التى تجتوى عليها 
                GetFieldValueDS.Tables().Clear()
            Else ' تفريغ مجموعة جداول البيانات المجهزة من الجدول المرسل فقط و ابقاء بيانات باقى الجداول كما هى
                If CType(GetFieldValueDS.Tables.Contains(LCase(TblNam)), Boolean) Then
                    GetFieldValueDS.Tables.Remove(LCase(TblNam))
                End If
            End If
            If NewSelectionForm Then
                If SelectTop Then
                    SQLWhere = Replace(LCase(SQLWhere), "select ", "SELECT TOP (1) ")
                End If
                MyGetFieldValueCommand.CommandText = SQLWhere
            Else
                If SelectTop Then
                    If Not TblNam.Contains(".") And Not TblNam.Contains("]") Then
                        MyGetFieldValueCommand.CommandText = "SELECT TOP (1) " & FieldsToSelect & " FROM [" & TblNam & "]" & SQLWhere
                    Else
                        MyGetFieldValueCommand.CommandText = "SELECT TOP (1) " & FieldsToSelect & " FROM " & TblNam & "" & SQLWhere
                    End If
                Else
                    If Not TblNam.Contains(".") And Not TblNam.Contains("]") Then
                        MyGetFieldValueCommand.CommandText = "SELECT " & FieldsToSelect & " FROM [" & TblNam & "]" & SQLWhere
                    Else
                        MyGetFieldValueCommand.CommandText = "SELECT " & FieldsToSelect & " FROM " & TblNam & "" & SQLWhere
                    End If
                End If
                If Not IsNothing(SortOrder) Then MyGetFieldValueCommand.CommandText &= " " & SortOrder
            End If
            'Dim WithoutDoublicationOldValue As Boolean
            'If IgnoreWithoutDoublication Then
            '    WithoutDoublicationOldValue = WithoutDoublication
            '    WithoutDoublication = False
            'End If
            'If WithoutDoublication And Not View_Form.Visible Then
            '    WithoutDoublicationRun(TblNam, MyGetFieldValueCommand.CommandText)
            '    MyGetFieldValueCommand.CommandText = WDR_Quiry
            'End If
            'If IgnoreWithoutDoublication Then
            '    WithoutDoublication = WithoutDoublicationOldValue
            'End If
            If MyGetFieldValueAdapter.Fill(GetFieldValueDS, [TblNam]) Then
                If GetFieldValueDS.Tables([TblNam]).Rows.Count > 0 Then
                    If Not IsNothing(FieldName) Then
                        Dim ReturnValue = GetFieldValueDS.Tables([TblNam]).Rows(0).Item(FieldName).ToString
                        Return GetFieldValueDS.Tables([TblNam]).Rows(0).Item(FieldName).ToString
                    Else
                        Return True
                    End If
                End If
            ElseIf LCase(TblNam) = LCase("INFORMATION_SCHEMA.COLUMNS") Then
                Using myReader As SqlDataReader = MyGetFieldValueCommand.ExecuteReader()
                    If CType(GetFieldValueDS.Tables.Contains(LCase(TblNam)), Boolean) Then
                        GetFieldValueDS.Tables.Remove(LCase(TblNam))
                    End If
                    If myReader.FieldCount > 0 Then
                        Dim CustomQuery_DT = New DataTable(TblNam)
                        CustomQuery_DT.Load(myReader)
                        GetFieldValueDS.Tables.Add(CustomQuery_DT)
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End If
            If Not IsNothing(FieldName) Then
                Return Nothing
            Else
                Return False
            End If
        Catch ex As Exception
            ShowMsg(ex.Message,, MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,,,,,, 0)
        Finally
            MyGetFieldValueCommand.CommandText = Nothing
            If ClearAfterLoading Then
                If CType(GetFieldValueDS.Tables.Contains(TblNam), Boolean) Then
                    GetFieldValueDS.Tables.Remove(TblNam)
                End If
            End If
            'Finalize()
        End Try
    End Function
    'Public Sub RunCommandCom(command As String, arguments As String, permanent As Boolean, Optional ProcessWindowStyleHidden As Boolean = False)
    Public Sub RunCommandCom(ByVal command As String,
                                                     ByVal permanent As Boolean,
                                                     ByVal SourcesFile As String,
                                                     ByVal CompresseFile As String,
                                                     Optional ByVal ProcessWindowStyleHidden As Boolean = False,
                                                     Optional ByVal DeleteSourceFileAfterCompress As Boolean = False)

        Dim CMD_Arguments = "a " & Chr(34) & CompresseFile & Chr(34) & " " & Chr(34) & SourcesFile & Chr(34)
        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        If ProcessWindowStyleHidden Then
            pi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
        End If
        'pi.Arguments = command + " " + If(permanent = True, "/K", "/C") + " " + arguments
        pi.Arguments = If(permanent = True, "/K", "/C") + " " + command + " " + CMD_Arguments
        'pi.Arguments = command + " " + arguments
        pi.FileName = "cmd.exe"
        p.StartInfo = pi
        p.Start()
        WaitAWhile()
        While ProcessRunning("7za")
            Application.DoEvents()
        End While
        If Not File.Exists(CompresseFile) Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "لم يتم أخذ الاحتياطى بشكل طبيعى"
            Else
                Msg = "Backup Not Done With Normal Way"
            End If
            ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        End If
        ProcessRunning("cmd", 1)
        'WaitAwhile
        'Using WAC As New WaitAwhile_Cls : End Using
        If Not IsNothing(CompresseFile) Then
            While FileInUse(CompresseFile, FileAccess.ReadWrite)
                Application.DoEvents()
            End While
        End If

    End Sub
    Public Function FileInUse(sFile As String, ByVal sFileAccess As FileAccess) As Boolean
        Try
            If Not File.Exists(sFile) Then
                Return False
            End If
            Using f As New IO.FileStream(sFile, FileMode.Open, sFileAccess, FileShare.None)
            End Using
        Catch Ex As System.IO.IOException
            Return True
        End Try
        Return False
    End Function
    Public MyWaitTimer As Double
    Public Function WaitAWhile(Optional ByVal WaitTime As Integer = 3,
                               Optional ByVal FromProcces As String = Nothing,
                               Optional ByVal ForceSleep As Boolean = False)
        Try
            MyWaitTimer = Microsoft.VisualBasic.DateAndTime.Timer + WaitTime '(WaitTime * 1000)
            While Microsoft.VisualBasic.DateAndTime.Timer < MyWaitTimer
                If Now.Date <> Today.Date Then
                    Today = Now.Date
                    MyWaitTimer = Microsoft.VisualBasic.DateAndTime.Timer + WaitTime '(WaitTime * 1000)
                End If
                If ForceSleep Then
                    System.Threading.Thread.Sleep(500)
                End If
                Application.DoEvents()
            End While
        Catch ex As Exception
            ShowMsg(ex.Message,, MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,,,,,, 0)
        Finally
        End Try
    End Function


    'Public WDR_Quiry As String = Nothing

    'Private AuthorityDescription As New TextBox

    '    Public Function DBConnectionError(ByVal ex As Exception) As Object
    '        Try
    'RDisplayMsg:
    '            SqlConnection.ClearAllPools()
    '            MyDialogResult = SQLConnectionError(1)
    '            If MyDialogResult <> DialogResult.None Then
    '                If MyDialogResult = DialogResult.Abort Or
    '                    MyDialogResult = DialogResult.Cancel Then
    '                    End
    '                End If
    '                Return MyDialogResult
    '            Else
    '                SQLConnectionNotOpenedIsTrue = False
    '            End If
    '            Return MyDialogResult
    '        Catch ex1 As Exception
    '            If Not IsNothing(ex1.InnerException.Message) Then
    '                MessageBox.Show(ex1.InnerException.Message, "DB Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
    '            Else
    '                MessageBox.Show(ex1.Message, "DB Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)
    '            End If
    '        End Try
    '    End Function
    '    Public Function ExctNnQryCmnd(ByVal MyCommandEx As DbCommand,
    '                                        Optional ByVal ReturnErrorResult As Boolean = False,
    '                                        Optional ByVal DisErrMsg As Boolean = True,
    '                                        Optional ByVal SendMail As Boolean = True,
    '                                        Optional ByVal MsgDelayTime As Integer = 45,
    '                                        Optional ByVal Reader As Boolean = False,
    '                                        Optional ByVal IgnoreExceptionErrorMessage As Boolean = False,
    '                                        Optional ByVal ReturnValue As String = Nothing,
    '                                        Optional ByVal ReturnCancelIfError As Boolean = False) ' As Boolean
    '        Dim dscs = MyCommandEx.Connection.ConnectionString
    '        If Menu_Tree_Description Then
    '            Using ATTMTDC As New AddTextToMenuTreeDescriptionCls
    '                ATTMTDC.AddTextToMenuTreeDescription(GetOrderBy() & " (" & MyCommandEx.CommandText & ")")
    '            End Using
    '        End If
    '        Dim TblNam = Nothing
    '        If LCase(MyCommandEx.CommandText).Contains(LCase("Delete From")) Then
    '            Dim match As Match = Regex.Match(MyCommandEx.CommandText, "Delete From\s+([A-Za-z0-9\.-_]+)\s*", RegexOptions.IgnoreCase)
    '            If (match.Success) Then
    '                TblNam = match.Groups(1).Value
    '                If Not IsNothing(TblNam) Then
    '                    If FieldUsed(TblNam, Nothing,, MyCommandEx.CommandText) Then
    '                        Return False
    '                    End If
    '                End If
    '            End If
    '        End If
    'ReDoExecuteNonQueryCommand:
    '        ErrDescription = Nothing
    '        Dim SendMailTo As String = ""
    '        Dim ENQC_Msg As String = Nothing
    '        Try
    '            'If FaildToOpenConnection() Then Exit Function
    '            If ReturnErrorResult Then
    '                If Not IsNothing(ReturnValue) Then
    '                    Dim match As Match = Regex.Match(MyCommandEx.CommandText, "From\s+([A-Za-z0-9\.-_]+)\s*", RegexOptions.IgnoreCase)
    '                    If (match.Success) Then
    '                        TblNam = match.Groups(1).Value
    '                        Dim MyQuery_Reader As SqlDataReader
    '                        MyQuery_Reader = MyCommandEx.ExecuteReader()
    '                        If MyQuery_Reader.HasRows Then
    '                            Dim Query_DT As New DataTable(TblNam)
    '                            Query_DT.Load(MyQuery_Reader)
    '                            ReturnValue = Query_DT.Rows(0).Item(ReturnValue)
    '                            Return ReturnValue
    '                        End If
    '                    End If
    '                End If
    '                If Reader Then
    '                    If MyCommandEx.ExecuteReader.HasRows Then
    '                        Return True
    '                    Else
    '                        Return False
    '                    End If
    '                Else
    '                    MyCommandEx.ExecuteNonQuery()
    '                End If
    '            Else
    '                If Reader Then
    '                    If MyCommandEx.ExecuteReader.HasRows Then
    '                        Return True
    '                    Else
    '                        Return False
    '                    End If
    '                Else
    '                    If MyCommandEx.ExecuteNonQuery Then
    '                        Return True
    '                    Else ' qery run but not valed
    '                        If Err.Number <> 0 Then
    '                            MsgBox(Err.Description & vbNewLine & "Err.Number = (" & Err.Number & ")")
    '                        End If
    '                        Return False
    '                    End If
    '                End If
    '            End If
    '        Catch ex As Exception
    '            ErrDescription = ex.Message
    '            Dim DBCE = New RefreshWaitAWhileTitles_Class
    '            If ex.Message.Contains("Cannot insert duplicate key") Or
    '                         (ex.Message.Contains("Property cannot be added. Property") And
    '                          ex.Message.Contains("already exists for")) Then
    '                SendMailTo = "Technical_Support_Team"
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "لايمكن اضافة بيان مكرر" & vbNewLine
    '                Else
    '                    ENQC_Msg = "Duplicate Key Impossible" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("Incorrect syntax near ','") Then
    '                SendMailTo = "Technical_Support_Team"
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "ربما لم يتم ادخال احد العناصر المطلوبة" & vbNewLine & "أعد المحاولة" & vbNewLine
    '                Else
    '                    ENQC_Msg = "Maybe One Of The Required Elements Not Entered" & vbNewLine & "Try Again" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("The conversion of a varchar data type to a datetime data type") Then
    '                SendMailTo = "Technical_Support_Team"
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "ربما تكون صيغة التاريخ فى احد العناصر غير صحيحة" & vbNewLine & "أعد المحاولة" & vbNewLine
    '                Else
    '                    ENQC_Msg = "Maybe The Date Format In One Of The Elements is Incorrect" & vbNewLine & "Try Again" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("There is already an object named '") And ex.Message.Contains("' in the database") Then
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "ربما يوجد نفس الجدول مفتوح من قبل" & vbNewLine
    '                Else
    '                    ENQC_Msg = "Maybe There Is The Same Table Already Exist" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("String or binary data would be truncated") Then
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "ربما تكون البيانات التى تحاول ادخالها تحمل اكثر مما يحتمل الحقل المخصص لها" & vbNewLine
    '                Else
    '                    ENQC_Msg = "The Letters You Are Trying To Enter Is To Bigger Than The Maximum Acceptable Letters" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint") Then
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "الالغاء مستحيل ... حيث ان البيان الذى تريد الغائة مرتبط ببيان اخر" & vbNewLine
    '                Else
    '                    ENQC_Msg = "Deletion Is Impossible ... Where The Record You Try To Delete Have A relation With Another Record" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("Cannot insert the value NULL into column") And
    '               ex.Message.Contains("column does not allow nulls") Then
    '                If MagNote_Form.Language_Btn.Text = "E" Then
    '                    ENQC_Msg = "بيانات ناقصة ... من فضلك اكمل البيانات وعاود المحاولة مرة اخرى" & vbNewLine
    '                Else
    '                    ENQC_Msg = "Incomplete Data ... Kindly Complete The Data And Try Again" & vbNewLine
    '                End If
    '            ElseIf ex.Message.Contains("There are fewer columns in the INSERT statement than values specified in the VALUES clause") Then
    '                SendMailTo = "Developers_Team"
    '            ElseIf LCase(ex.Message).Contains(LCase("Lag' is not a recognized built-in function name.")) Then
    '                ENQC_Msg = ""
    '                IgnoreExceptionErrorMessage = True
    '                SendMail = True
    '                MsgDelayTime = 1
    '            ElseIf DBCE.DBConnectionError(ex) <> DialogResult.None Then
    '                Dim DBCEMsg = DBCE.DBConnectionError(ex)
    '                If DBCEMsg = DialogResult.Retry Then
    '                    GoTo ReDoExecuteNonQueryCommand
    '                ElseIf DBCEMsg Then
    '                    SendMailTo = "Network_Team"
    '                    DisplayAIOCriticalError(ex, MSG & vbNewLine & MyCommandEx.CommandText & vbNewLine & dscs, SendMailTo, 0)
    '                    Return False
    '                End If
    '            Else
    '                ENQC_Msg = ""
    '            End If
    '            If Not CType(Val(GetAIODefaultValues("Send Mail By The Error Message In ExecuteNonQueryCommand")), Boolean) Then
    '                SendMail = False
    '            End If
    '            If DisErrMsg Then
    '                If ReturnCancelIfError Then
    '                    DisErr(MsgDelayTime, ENQC_Msg & vbNewLine & "AIO Critical Error" & EnglishDirection & ex.Message & "<br>" & MyCommandEx.CommandText & vbNewLine & dscs & "</div> ", , , , , 1, , , Msg_Form.Cancel_Btn, , , , , , , , ,, SendMail, SendMailTo, IgnoreExceptionErrorMessage)
    '                    Return Msg_Result
    '                Else
    '                    DisErr(MsgDelayTime, ENQC_Msg & vbNewLine & "AIO Critical Error" & EnglishDirection & ex.Message & "<br>" & MyCommandEx.CommandText & vbNewLine & dscs & "</div> ", , , , , , , , , , , , , , , , ,, SendMail, SendMailTo, IgnoreExceptionErrorMessage)
    '                End If
    '            End If
    '            Return False
    '        End Try
    '        Return True
    '    End Function


#Region "IDisposable"
    Private disposedValue As Boolean
    Private components As System.ComponentModel.IContainer
    Public Sub New()
        MyBase.New()
        'This call is the Windows Form Designer necessary.
        InitializeComponent()
        'Add any initialization after the InitializeComponent call ()
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
                ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
                ' TODO: set large fields to null
                disposedValue = True
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Dispose(disposing)
            End If
        End If
    End Sub
    ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    Protected Overrides Sub Finalize()
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=False)
        MyBase.Finalize()
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        'GC.Collect()
        'GC.Collect(2, GCCollectionMode.Optimized)
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
