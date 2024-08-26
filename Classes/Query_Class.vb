
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Query_Class
    Implements IDisposable

    Public Query_CustomSelectWhere As String = String.Empty
    Public Query_CustomParameters As New Dictionary(Of String, String) ' fieldname , value

    Public InsertUpdateColumnsValue As New Dictionary(Of String, Object) ' fieldname , value
    Public OldRecord As Boolean = False
    Public Query_Reader As SqlDataReader
    Public Query_DS As New DataSet
    Public Query_DT As New DataTable

    Private Query_TableName As String = String.Empty
    Private DidQueryFieldValueDoRun As Boolean = False
    Private QueryWhereString As String = String.Empty
    Private QueryWhereStringMSG As String = String.Empty
    Public WhereColumnsValue As New Dictionary(Of String, String)

    Private QCSQLDBConnection As SqlConnection = New SqlConnection(SQLConnStr)
    Public SQLDbCommand As SqlCommand = QCSQLDBConnection.CreateCommand
    Private QueryAdapter As SqlDataAdapter

    Dim QueryTimer = Microsoft.VisualBasic.DateAndTime.Timer
    Dim QueryTimer1 = Microsoft.VisualBasic.DateAndTime.Timer
    Public Sub New(TableName As String,
                 Optional QueryWhere As Dictionary(Of String, String) = Nothing,
                 Optional FillObjField As Dictionary(Of Object, String) = Nothing)
        Try
ReNew:
            If MagNote_Form.Projects_Connection_Strings_CmbBx.SelectedIndex = -1 Then
                Exit Sub
            End If
            If Not OpenDB(MagNote_Form.Projects_Connection_Strings_CmbBx.SelectedItem) Then
                OpenDBStatus = False
                Exit Sub
            End If
            OpenDBStatus = True

            If String.IsNullOrEmpty(TableName) Then Exit Sub
            SqlConnection.ClearAllPools()
            Cursor.Current = Cursors.WaitCursor
            If QCSQLDBConnection.State = ConnectionState.Closed Then
                QCSQLDBConnection.ConnectionString = SQLConnStr
                QCSQLDBConnection.Open()
                If SQLDbCommand.Connection.State = ConnectionState.Closed Then
                    SQLDbCommand.Connection.Open()
                End If
            End If
            SQLDbCommand.CommandTimeout = 0
            Query_TableName = TableName
            If Not CollectColumnsType() Then
                Exit Sub
            End If
            SQLDbCommand.Parameters.Clear()
            CallQuery_Class(QueryWhere, FillObjField)
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ReturnSQLDbCommandParameters())
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Dim NumberOfTrays As Integer
    Dim Change_Msg
    Public Function DoInsertUpdate(Optional ShowASKToUpdateMSG As Boolean = True,
                                                   Optional ShowUpdateSuccessfullyMSG As Boolean = True,
                                                   Optional FinalizeClass As Boolean = True,
                                                   Optional SpecialMessage As String = Nothing,
                                                   Optional SendNotification As Boolean = True,
                                                   Optional DisplayMsgTime As Double = 0) As Boolean
RetryDoInsertUpdate:
        'Cursor.Current = Cursors.WaitCursor
        If Not IsNothing(SpecialMessage) Then
            SpecialMessage = SpecialMessage & vbNewLine
        Else
            SpecialMessage = String.Empty
        End If
        Dim SendMail As Boolean = False
        Dim SendMailTo As String = ""
        Dim ENQC_Msg As String = Nothing
        Try
            If DidQueryFieldValueDoRun = False Then
                QueryFieldValue(False)
            End If
            ' copy parameters before command creation
            Dim OLDSQLDBCommandPatrameters As SqlParameterCollection = SQLDbCommand.Parameters
            Query_OpenConnectionIfClosed()
            'REFILL Parameters after creation
            Change_Msg = QueryWhereString
            For Each SQLDBPara As SqlParameter In OLDSQLDBCommandPatrameters
                If OldRecord Then
                    ' in case of insert  must all parameters cleared as i donot need where parameters
                    SQLDbCommand.Parameters.Add(SQLDBPara.ParameterName, SQLDBPara.SqlDbType).Value = SQLDBPara.Value
                    'SQLDbCommand.Parameters.AddWithValue(SQLDBPara.ParameterName, SQLDBPara.Value)
                End If
                Change_Msg = Replace(LCase(Change_Msg), SQLDBPara.ParameterName.ToString.ToLower, SQLDBPara.Value.ToString)
            Next
            Dim QueryInsertUpdate As String = String.Empty
            'add special fields in all cases
            If OldRecord Then
                If ShowASKToUpdateMSG Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Change_Msg = Replace(Change_Msg, LCase("where"), "")
                        Msg = " سيتم تحديث البيان التالى حيث" & vbNewLine
                    Else
                        Msg = " Update Record "
                    End If
                    If ShowMsg(SpecialMessage & Msg & Change_Msg & " ? ",, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,,,,,, 0) = DialogResult.No Then
                        Return False
                    End If
                End If
                QueryInsertUpdate = QueryUpdate_Builder()
            Else
                QueryInsertUpdate = QueryInsert_Builder()
            End If
            If SQLDbCommand.Connection.ConnectionString.Length = 0 Then
                SQLDbCommand.Connection.ConnectionString = SQLConnStr
                SQLDbCommand.Connection.Open()
            End If
            Using SQLDBConnection
                SQLDbCommand.CommandTimeout = 0
                SQLDbCommand.CommandText = QueryInsertUpdate
                If SQLDbCommand.ExecuteNonQuery() Then
                    If ShowUpdateSuccessfullyMSG Then
                        If MagNote_Form.Language_Btn.Text = "E" Then
                            Change_Msg = Replace(Change_Msg, LCase("where"), "")
                            Msg = " تم تحديث البيان التالى بنجاح" & vbNewLine
                        Else
                            Msg = " Update successfolly "
                        End If
                        ShowMsg(Msg & Change_Msg & vbNewLine & SpecialMessage,, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    End If
                    Return True
                End If
            End Using
            Return False
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ENQC_Msg & ReturnSQLDbCommandParameters(), SendMailTo,,, 30)
        Finally
            NumberOfRetray = 0
            If FinalizeClass Then
                Query_OpenConnectionIfClosed()
                Query_DS.Dispose()
                Query_DT.Dispose()
                Close()
            End If
        End Try
        Return False
    End Function
    Public Function DoDelete(Optional ShowASKToDeleteMSG = True,
                                                   Optional ShowDeleteSuccessfullyMSG = True,
                                                   Optional FinalizeClass = True,
                                                   Optional CheckIfFieldUsed = True,
                                                   Optional SendNotification = True,
                                                   Optional SpecialMessage = Nothing)
        Try
ReRunDoDelete:
            If Not IsNothing(SpecialMessage) Then
                SpecialMessage = SpecialMessage & vbNewLine
            Else
                SpecialMessage = String.Empty
            End If
            If DidQueryFieldValueDoRun = False Then
                QueryFieldValue(False)
            End If
            If String.IsNullOrEmpty(QueryWhereString) Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "جملة الاستعلام مفقودة" & vbNewLine & "لا يمكن الغاء البيان"
                Else
                    Msg = " Where Query is missing " & vbNewLine & " can't Delete Record"
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Try
            End If
            ' copy parameters before command creation
            Dim OLDSQLDBCommandPatrameters As SqlParameterCollection = SQLDbCommand.Parameters
            Query_OpenConnectionIfClosed()
            Dim Change_Msg = QueryWhereString
            For Each SQLDBPara As SqlParameter In OLDSQLDBCommandPatrameters
                If OldRecord Then
                    'SQLDbCommand.Parameters.AddWithValue(SQLDBPara.ParameterName, SQLDBPara.Value)
                    SQLDbCommand.Parameters.Add(SQLDBPara.ParameterName, SQLDBPara.SqlDbType).Value = SQLDBPara.Value
                End If
                Change_Msg = Replace(LCase(Change_Msg), SQLDBPara.ParameterName.ToString.ToLower, SQLDBPara.Value.ToString)
            Next
            Dim QueryDelete As String = String.Empty
            If OldRecord Then
                QueryDelete = QueryDelete_Builder()
                Dim match As Match = Regex.Match(QueryDelete, "From\s+([A-Za-z0-9\.-_]+)\s*", RegexOptions.IgnoreCase)
                Dim TblNam
                If (match.Success) Then
                    TblNam = match.Groups(1).Value
                End If
                If ShowASKToDeleteMSG Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = SpecialMessage & "سيتم الغاء البيان التالى" & vbNewLine & Change_Msg & vbNewLine & "من جدول بيانات باسم" & vbNewLine & TblNam
                    Else
                        Msg = SpecialMessage & "Delete Record" & vbNewLine & Change_Msg & vbNewLine & "From Table Name" & vbNewLine & TblNam
                    End If
                    If ShowMsg(Msg & " ? ",, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,,,,,, 0) = DialogResult.No Then
                        Return False
                    End If
                End If
            Else
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "هذا البيان غير موجود" & vbNewLine
                Else
                    Msg = "This Record Not Found" & vbNewLine
                End If
                ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Try
            End If
            If SQLDbCommand.Connection.ConnectionString.Length = 0 Then
                SQLDbCommand.Connection.ConnectionString = SQLConnStr
                SQLDbCommand.Connection.Open()
            End If
            ' FillSQLParameters()
            Using SQLDBConnection
                SQLDbCommand.CommandText = QueryDelete
                If SQLDbCommand.ExecuteNonQuery() Then
                    If ShowDeleteSuccessfullyMSG Then
                        ShowMsg("SuccessMsg",, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    End If
                    Return True
                End If
            End Using
            Return False
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ReturnSQLDbCommandParameters(),,,, 30)
        Finally
            NumberOfRetray = 0
            If FinalizeClass Then
                Query_DS.Dispose()
                Query_DT.Dispose()
                Query_OpenConnectionIfClosed()
                Close()
            End If
        End Try
        Return False
    End Function

    Private Function QueryUpdate_Builder()
        Try
            'Cursor.Current = Cursors.WaitCursor
            If Query_CustomParameters.Keys.Count > 0 Then
                For Each CustomPara In Query_CustomParameters
                    If SQLDbCommand.Parameters.Contains("@" & CustomPara.Key) = False Then
                        QueryAddWhere(CustomPara.Key, CustomPara.Value)
                    End If
                Next
            End If
            Dim Count = 0
            Dim InsertTableName = Query_TableName
            If Not InsertTableName.StartsWith("[") Then
                InsertTableName = "[" & InsertTableName
            End If
            If Not InsertTableName.EndsWith("[") Then
                InsertTableName = InsertTableName & "]"
            End If
            Dim QueryUpdateString As String = " UPDATE " & InsertTableName & " SET  " & vbNewLine
            For Each Field As KeyValuePair(Of String, Object) In InsertUpdateColumnsValue
                Dim ColumnName = Field.Key
                Dim ColumnValue = Field.Value
                Dim NewColumnName = ColumnName
                Dim ColumnCount = 1
                While SQLDbCommand.Parameters.Contains("@" & NewColumnName)
                    NewColumnName = ColumnName & ColumnCount
                    ColumnCount = ColumnCount + 1
                End While
                If Count > 0 Then QueryUpdateString &= ", " & vbNewLine
                If Not IsNothing(ColumnValue) Then
                    If ColumnValue.GetType = GetType(Bitmap) Or ColumnValue.GetType = GetType(Image) Then
                        GoTo ImgeType
                    End If
                End If
                Dim ColumnValueIsString As Boolean = False
                If ColumnValue.GetType = GetType(String) Then
                    ColumnValueIsString = True
                End If
                If IsDBNull(ColumnValue) Then
                    QueryUpdateString &= ColumnName & " =NULL "
                ElseIf ColumnValueIsString Then
                    If String.IsNullOrEmpty(ColumnValue) Then
                        QueryUpdateString &= ColumnName & " =NULL "
                    Else
                        QueryUpdateString &= ColumnName & " =" & ReturnFieldUpdateParameter(ColumnName, ColumnValue, NewColumnName)
                    End If
                Else
ImgeType:
                    QueryUpdateString &= ColumnName & " =" & ReturnFieldUpdateParameter(ColumnName, ColumnValue, NewColumnName)
                End If
                Count = Count + 1
            Next
            QueryUpdateString &= vbNewLine & QueryWhereString
            Return QueryUpdateString
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Function
    Private Function QueryDelete_Builder()
        Try
            'Cursor.Current = Cursors.WaitCursor
            If Query_CustomParameters.Keys.Count > 0 Then
                For Each CustomPara In Query_CustomParameters
                    If SQLDbCommand.Parameters.Contains("@" & CustomPara.Key) = False Then
                        QueryAddWhere(CustomPara.Key, CustomPara.Value)
                    End If
                Next
            End If

            Return " DELETE FROM " & Query_TableName & QueryWhereString
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Function
    Private Function QueryInsert_Builder()
        Try
            'Cursor.Current = Cursors.WaitCursor
            Dim InsertTableName = Query_TableName
            If Not InsertTableName.StartsWith("[") Then
                InsertTableName = "[" & InsertTableName
            End If
            If Not InsertTableName.EndsWith("[") Then
                InsertTableName = InsertTableName & "]"
            End If

            Dim QueryInsertString As String = " Insert INTO " & InsertTableName & " ("
            Dim QueryValuesString As String = " ) Values ( "
            For Each Field As KeyValuePair(Of String, Object) In InsertUpdateColumnsValue
                Dim ColumnName = Field.Key
                Dim ColumnValue = Field.Value
                QueryInsertString &= ColumnName & ", "
                QueryValuesString &= ReturnFieldUpdateParameter(ColumnName, ColumnValue) & ", "
            Next
            QueryInsertString = Mid(QueryInsertString, 1, Len(QueryInsertString) - 2)
            QueryValuesString = Mid(QueryValuesString, 1, Len(QueryValuesString) - 2)
            QueryInsertString &= QueryValuesString
            QueryInsertString &= ") "
            Return QueryInsertString
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Function

    Public Query_ColumnsType As New Dictionary(Of String, String)
    Private Function ReturnFieldUpdateParameter(ColumnName As String, ColumnValue As Object, Optional NewColumnName As String = Nothing)
        Try
            If String.IsNullOrEmpty(NewColumnName) Then
                NewColumnName = ColumnName
            End If
            Dim FieldUpdateParameter As String = String.Empty
            Dim ColumnType As String = Query_ColumnsType(ColumnName.ToLower).ToLower

            If ColumnType = "image" Then GoTo IfImage
            Try
                Dim Original As String
                If Not IsNothing(ColumnValue) Then
                    Original = ColumnValue
                End If
                Dim Ascii As New ASCIIEncoding()
                Dim OriginalBytes() As Byte = Ascii.GetBytes(Original)
                Dim Decoded As String = Ascii.GetString(OriginalBytes)
                If IsNothing(Decoded) Then
                    'SQLDbCommand.Parameters.AddWithValue("@" & NewColumnName, DBNull.Value)
                    SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.VarChar).Value = DBNull.Value
                    Return "@" & NewColumnName
                ElseIf String.IsNullOrEmpty(Decoded) Or Decoded.Equals("NULL") Then
                    SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.VarChar).Value = DBNull.Value
                    'SQLDbCommand.Parameters.AddWithValue("@" & NewColumnName, DBNull.Value)
                    Return "@" & NewColumnName
                End If
            Catch ex As Exception
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.VarChar).Value = DBNull.Value
                'SQLDbCommand.Parameters.AddWithValue("@" & NewColumnName, DBNull.Value)
                Return "@" & NewColumnName
            End Try

IfImage:
            If ColumnType = "date" Then
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.Date).Value = ConvertDate(ColumnValue)
            ElseIf ColumnType = "varchar" Then
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.VarChar).Value = ColumnValue
            ElseIf ColumnType = "datetime" Then
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.DateTime).Value = ConvertDateTime(ColumnValue)
            ElseIf ColumnType = "binary" Then
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.Binary).Value = ColumnValue
            ElseIf ColumnType = "int" Then
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.Int).Value = ColumnValue
            ElseIf ColumnType = "tinyint" Then
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.TinyInt).Value = ColumnValue
            ElseIf ColumnType = "image" And
                (ColumnValue.GetType <> GetType(Bitmap) And
                ColumnValue.GetType <> GetType(Image)) Then
                Dim ms As MemoryStream = Nothing
                Using fs As FileStream = New FileStream(ColumnValue, FileMode.Open, FileAccess.Read)
                    Dim bytes(fs.Length) As Byte
                    fs.Read(bytes, 0, fs.Length)
                    ms = New MemoryStream(bytes)
                End Using
                Query_OpenConnectionIfClosed()
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.Image).Value = ms.ToArray
            ElseIf ColumnType = "image" And
                (ColumnValue.GetType = GetType(Bitmap) Or ColumnValue.GetType = GetType(Image)) Then
                Dim mstream As New MemoryStream
                CType(ColumnValue, Image).Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim myBytes(mstream.Length - 1) As Byte
                mstream.Position = 0
                mstream.Read(myBytes, 0, mstream.Length)
                'Return myBytes
                SQLDbCommand.Parameters.Add("@" & NewColumnName, SqlDbType.Image).Value = myBytes
            Else
                SQLDbCommand.Parameters.Add("@" & NewColumnName, ColumnValue.GetType).Value = ColumnValue
                'SQLDbCommand.Parameters.AddWithValue("@" & NewColumnName, ColumnValue)
            End If
            Return "@" & NewColumnName
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ColumnName,, 0,, 30)
        Finally
        End Try
        Return " NULL "
    End Function
    Private Function ReturnSQLDbCommandParameters() As String
        Try
            Dim QueryParameters, ParameterSqlValue, SQLCommandText
            QueryParameters = String.Empty
            ParameterSqlValue = String.Empty
            SQLCommandText = String.Empty
            SQLCommandText = SQLDbCommand.CommandText
            Dim MsgParameters As New Dictionary(Of String, Object) ' fieldname , value
            If InsertUpdateColumnsValue.Count = 0 Then
                For Each Parameter In SQLDbCommand.Parameters
                    If Not IsNothing(Parameter.SqlValue) Then
                        SQLCommandText = Replace(SQLCommandText, Parameter.SqlValue.ToString, "")
                        ParameterSqlValue = Parameter.SqlValue.ToString
                    Else
                        ParameterSqlValue = "Nothing"
                    End If
                    MsgParameters.Add(Replace(Parameter.ParameterName.ToString, "@", ""), ParameterSqlValue)
                Next
                InsertUpdateColumnsValue = MsgParameters
            End If
            For Each Parameter In InsertUpdateColumnsValue
                If Not IsNothing(Parameter.Value) Then
                    ParameterSqlValue = Parameter.Value.ToString
                Else
                    ParameterSqlValue = "Nothing"
                End If
                If Not IsNothing(Parameter.Key) Then
                    QueryParameters &= Replace(Parameter.Key.ToString, "@", "") & " = " & ParameterSqlValue & "<br>"
                End If
            Next
            If Not String.IsNullOrEmpty(SQLCommandText) Then
                SQLCommandText = "SQLCommandText = (" & SQLCommandText & ")"
            End If
            If Not String.IsNullOrEmpty(QueryParameters) Then
                QueryParameters = "QueryParameters = (" & QueryParameters & ")"
            End If
            Return SQLCommandText & "<br>" & QueryParameters & "<br>"
        Catch ex As Exception
        End Try
    End Function

    Dim SendMailNotificationsFormName, SendMailNotificationsObjectName
    Enum ColumnPart
        Day
        Month
        Year
    End Enum
    'Dim AddWhereRunning As Boolean
    ''' <summary>
    ''' <param name="ColumnName">اسم عنصر الجدول من قاعدة البيانات</param>
    ''' <param name="ColumnValue">قيمة المراد اضافتها  لهذا العنصر</param>
    ''' <param name="Op"> [= >] </param>
    ''' <param name="Logic"> and or </param>
    ''' 
    ''' </summary>
    Public Sub QueryAddWhere(ColumnName As String,
                                                ColumnValue As String,
                                                Optional Op As String = " = ",
                                                Optional Logic As String = " And ",
                                                Optional UseOr As Boolean = False,
                                                Optional OrValue As String = Nothing,
                                                Optional ColumnPart As String = Nothing,
                                                Optional ErrorMsg As String = Nothing,
                                                Optional DisplayErrMsg As Boolean = False)
        Try
ReRunAddWhere:
            Dim NewColumnName As String = ColumnName
            Dim Count = 1

            While SQLDbCommand.Parameters.Contains("@" & NewColumnName)
                NewColumnName = ColumnName & Count
                Count = Count + 1
            End While
            WhereColumnsValue.Add(NewColumnName, ColumnValue)
            If Not String.IsNullOrEmpty(QueryWhereString) Then
                QueryWhereString &= Logic
            Else
                QueryWhereString = " Where "
            End If
            If String.IsNullOrEmpty(ColumnPart) Then ColumnPart = ""
            If ColumnValue = "DBNull.Value" Or IsDBNull(ColumnValue) Then
                If Op.Equals(" = ") Or Op.Equals("=") Then
                    'IS NULL
                    QueryWhereString &= " (" & ColumnPart & "(" & ColumnName & ")" & " IS NULL"
                ElseIf Op.Equals(" <> ") Or Op.Equals("<>") Then
                    ' IS NOT NULL
                    QueryWhereString &= " (" & ColumnPart & "(" & ColumnName & ")" & "  IS NOT NULL"
                End If
            Else
                QueryWhereString &= " (" & ColumnPart & "(" & ColumnName & ")" & " " & Op & ColumnPart & "(" & ReturnFieldUpdateParameter(ColumnName, ColumnValue, NewColumnName) & ")"
            End If
            If Not IsNothing(OrValue) Then
                Dim NewQuery = " " & ColumnPart & "(" & ColumnName & ")" & " " & Op & ColumnPart & "(" & ReturnFieldUpdateParameter(ColumnName, OrValue, NewColumnName & "_Or") & ")" & vbNewLine
                QueryWhereString &= " Or " & NewQuery & ")" & vbNewLine
            Else
                QueryWhereString &= ")" & vbNewLine
            End If
            QueryWhereStringMSG = QueryWhereString
            'st = Nothing
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ReturnSQLDbCommandParameters(),,,, 30)
        Finally
            'AddWhereRunning = False
            'Cursor.Current = Cursors.Default
        End Try
    End Sub
    Dim NumberOfRetray As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="FillDS">أحيانا لا احتاج الى ملأ البيانات في dataset
    ''' فقط احتاج لمعرفة اذا كان موجود او لا</param>
    ''' <param name="SelectTop"></param>
    ''' <returns></returns>
    Public Function QueryFieldValue(Optional FillDS As Boolean = True,
                                        Optional SelectTop As Boolean = False,
                                        Optional LazyOrderBYColumn As String = Nothing,
                                        Optional LazyStartFrom As Integer = 1,
                                        Optional LazyCount As Integer = 200,
                                        Optional Accumlate_DT As Boolean = False,
                                        Optional LazySortOrder As String = "ASC",
                                        Optional SaelectFields As String = Nothing,
                                        Optional ErrorMsg As String = Nothing,
                                        Optional NormalOrderBy As String = Nothing,
                                        Optional DisplayErrMsg As Boolean = True
                                        ) As Boolean
        Try
ReRunQueryFieldValue:
            If Not String.IsNullOrEmpty(Query_CustomSelectWhere) Then
                If Query_CustomSelectWhere.Contains("Order By") And
                    Not Query_CustomSelectWhere.Contains(" Lag(") And
                    Not Query_CustomSelectWhere.Contains(" Over (") Then
                    Query_CustomSelectWhere &= " " & LazySortOrder
                End If
            End If
ReRunGetFieldValue:
            If IsNothing(SaelectFields) Then SaelectFields = " * "
            'Cursor.Current = Cursors.WaitCursor
            Dim Pre_LazyLoading As String = Nothing
            Dim LazyOrderBy As String = Nothing
            Dim Post_LazyLoading As String = Nothing
            Dim LazyEndTO As Double = LazyStartFrom + LazyCount
            If Not IsNothing(LazyOrderBYColumn) Then
                Pre_LazyLoading = "select * from ( "
                LazyOrderBy = " , ROW_NUMBER() OVER(ORDER BY " & LazyOrderBYColumn & " " & LazySortOrder & ") AS Row# "
                Post_LazyLoading = " )a" ' where a.Row# BETWEEN " & LazyStartFrom & " AND " & LazyEndTO
            End If

            Query_OpenConnectionIfClosed(0)
            If IsNothing(Query_TableName) Then
                Dim match As Match = Regex.Match(QueryWhereString, "From\s+([A-Za-z0-9\.-_]+)\s*", RegexOptions.IgnoreCase)
                If (match.Success) Then
                    Query_TableName = match.Groups(1).Value
                End If
            End If

            If Not String.IsNullOrEmpty(Query_CustomSelectWhere) Then
                If Query_CustomSelectWhere.ToLower.Contains("select") Then
                    If Not IsNothing(LazyOrderBYColumn) Then
                        ShowMsg("Lazy Load is not available in custom selection",, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                        Pre_LazyLoading = Nothing
                        LazyOrderBy = Nothing
                        Post_LazyLoading = Nothing
                    End If
                    SQLDbCommand.CommandText = Query_CustomSelectWhere
                Else
                    SQLDbCommand.CommandText = "SELECT " & IIf(SelectTop, "TOP (1)", "") & SaelectFields & LazyOrderBy & " FROM [" & Query_TableName & "] " & Query_CustomSelectWhere
                End If
                If Query_CustomParameters.Keys.Count > 0 Then
                    For Each CustomPara In Query_CustomParameters
                        If SQLDbCommand.Parameters.Contains("@" & CustomPara.Key) = False Then
                            'SQLDbCommand.Parameters.AddWithValue("@" & CustomPara.Key, CustomPara.Value)
                            SQLDbCommand.Parameters.Add("@" & CustomPara.Key, CustomPara.Value.GetType).Value = CustomPara.Value
                        End If
                    Next
                End If
            Else
                SQLDbCommand.CommandText = "SELECT " & IIf(SelectTop, "TOP (1)", "") & SaelectFields & LazyOrderBy & " FROM [" & Query_TableName & "] " & QueryWhereString
            End If
            SQLDbCommand.CommandText = Pre_LazyLoading & SQLDbCommand.CommandText & Post_LazyLoading
            If LCase(Query_TableName) = LCase("INFORMATION_SCHEMA.TABLES") And String.IsNullOrEmpty(Query_CustomSelectWhere) Then
                SQLDbCommand.CommandText = "SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE  (TABLE_TYPE = 'base table' OR TABLE_TYPE = 'view') AND (TABLE_NAME <> 'dtproperties') ORDER BY TABLE_NAME"
            ElseIf LCase(Query_TableName) = LCase("INFORMATION_SCHEMA.COLUMNS") And String.IsNullOrEmpty(Query_CustomSelectWhere) Then
                SQLDbCommand.CommandText = "Select Column_Name, TABLE_NAME From INFORMATION_SCHEMA.COLUMNS " & QueryWhereString
            End If
            Query_DS = New DataSet(Query_TableName)
            If Not IsNothing(LazyOrderBYColumn) And Query_DT.Rows.Count > 0 Then
            Else
                Query_DT = New DataTable(Query_TableName)
            End If
RetryToGetData:
            Using SQLDBConnection
                For Each Prmtr In SQLDbCommand.Parameters
                    If Prmtr.value.ToString = DBNull.Value.ToString Then
                        SQLDbCommand.Parameters(Prmtr.parametername).Value = System.DBNull.Value
                    End If
                Next
                If SQLDbCommand.Connection.State = ConnectionState.Closed Then
                    SQLDbCommand.Connection.Open()
                End If
                Query_Reader = SQLDbCommand.ExecuteReader()
                If Query_Reader.HasRows Then
                    If FillDS Then
                        If Accumlate_DT Then
                            Dim CustomQuery_DT = New DataTable(Query_TableName)
                            CustomQuery_DT.Load(Query_Reader)
                            Query_DT.Merge(CustomQuery_DT, True)
                        Else
                            Query_DT.Load(Query_Reader)
                        End If

                        If IsNothing(LazyOrderBYColumn) Then
                            Query_DS.Tables.Add(Query_DT)
                        End If
                    End If
                    'Query_Reader.Close()
                    DidQueryFieldValueDoRun = True
                    OldRecord = True
                    Return True
                End If
                'Query_Reader.Close()
                DidQueryFieldValueDoRun = True
                OldRecord = False
                Return False
            End Using
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ReturnSQLDbCommandParameters() & ErrorMsg,, DisplayErrMsg)
            Return False
        Finally
            NumberOfRetray = 0
            If Not IsNothing(Query_Reader) Then
                Query_Reader.Close()
            End If
            Close()
        End Try
    End Function
    ''' <summary>
    ''' <param name="IgnorFromDebugStop"> لا يتم الغاء البيان التالى الى بعد التأكد من ان جميع الاستخدامات فى البرنامج التى تخص بارسال قيمة (NULL)ـ </param>
    ''' </summary>
    Dim IgnorFromDebugStop = "Display_Unauthorized_Message
Authority_Mail_Subject
Send_Unauthorized_Mail
Use_Complex_Password"
    ''' <summary>
    ''' لتحميل البيانات الى العناصر البرمجية
    ''' </summary>
    ''' <param name="Object_Name">العنصر البرمجي
    ''' False = return Value</param>
    ''' <param name="Field_Name">عنصر في قاعدة البيانات</param>
    ''' <param name="defaultValue">القيمة الافتراضية في حالة عدم وجود بيانات في ال  DS</param>
    ''' <param name="DntEmptyOBJ_IFEmptyDS">أبق البيان في العنصر البرمجي اذا كانت ال  
    ''' DS  فارغة</param>
    ''' <param name="RowNumber">رقم السطر</param>
    ''' <returns></returns>
    Public Function FillAddObjField(Object_Name, Optional Field_Name = Nothing,
                                        Optional defaultValue = Nothing,
                                        Optional DntEmptyOBJ_IFEmptyDS = False,
                                        Optional RowNumber = 0,
                                        Optional ReFill = False,
                                        Optional CmbxSV = False,
                                        Optional ImageBG = False)
        Dim ObjectType = Object_Name.GetType().Name.ToString
        If DidQueryFieldValueDoRun = False Then
            QueryFieldValue(True)
        End If
        Try
            'Cursor.Current = Cursors.WaitCursor
            If ObjectType = "DataGridView" Then
                Object_Name.tag = Query_DT.TableName
                Object_Name.ClearSelection()
                If Not System.Windows.Forms.SystemInformation.TerminalServerSession Then
                    Dim dgvType As Type = Object_Name.[GetType]()
                    Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
                    pi.SetValue(Object_Name, True, Nothing)
                End If
                Object_Name.AutoGenerateColumns = True
                Object_Name.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                Object_Name.ColumnHeadersVisible = False
                Object_Name.DataSource = Query_DT
                If ReFill Then
                    Object_Name.DataSource = Nothing
                    Dim DatesColumns As New ArrayList
                    If Object_Name.Columns.Count = 0 Then
                        Dim ColumnInx As Integer = 0
                        For Each dc As DataColumn In Query_DT.Columns
                            If Query_DT.Columns.Item(dc.ColumnName).DataType.Name = "Date" Then
                                DatesColumns.Add(dc.ColumnName)
                            End If
                            Object_Name.Columns.Add(New DataGridViewTextBoxColumn())
                            Object_Name.columns(ColumnInx).Name = dc.ColumnName
                            ColumnInx += 1
                        Next
                    End If
                    For Each row As DataRow In Query_DT.Rows
                        For Each Col In DatesColumns
                            row.Item(Col) = ConvertDate(row.Item(Col)).ToString
                        Next
                        Object_Name.Rows.Add(row.ItemArray)
                    Next
                    Try
                        Object_Name.PerformLayout()
                    Catch ex As Exception
                    End Try
                End If
                Object_Name.ColumnHeadersVisible = True
                Return True
            End If
            If Query_DT.Rows.Count = 0 Then
                GoTo ClearThisField
            End If
            Dim FieldType As String = Nothing
            If ObjectType <> "Boolean" Then
                FieldType = Query_ColumnsType(Field_Name.ToLower()).ToLower()
            End If
            If Not IsDBNull(Query_DT.Rows(RowNumber).Item(Field_Name)) Then
                If FieldType = "datetime" Then
                    Object_Name.text = ConvertDateTime(Query_DT.Rows(RowNumber).Item(Field_Name))
                ElseIf FieldType = "date" Then
                    Object_Name.text = ConvertDate(Query_DT.Rows(RowNumber).Item(Field_Name))
                ElseIf FieldType = "image" Then
                    Dim bytBLOBData() As Byte = Query_DT.Rows(RowNumber).Item(Field_Name)
                    Dim stmBLOBData As New MemoryStream(bytBLOBData)
                    If ImageBG Then
                        Object_Name.Image = Image.FromStream(stmBLOBData)
                    Else
                        Object_Name.BackgroundImage = Image.FromStream(stmBLOBData)
                    End If
                Else
                    If ObjectType = "CheckBox" Then
                        Object_Name.CheckState = Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                    ElseIf ObjectType = "Boolean" Then
                        If IgnorFromDebugStop.ToString.Contains(Field_Name) Then
                            'Return CType(Query_DT.Rows(RowNumber).Item(Field_Name).ToString, Boolean)
                            Return Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                        Else
                            'Return CType(Query_DT.Rows(RowNumber).Item(Field_Name).ToString, Boolean)
                            Return Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                        End If
                    ElseIf ObjectType = "String" Then
                        Object_Name = Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                    ElseIf (ObjectType = "ComboBox" Or ObjectType = "ColorComboBox") And CmbxSV Then
                        Object_Name.SelectedValue = Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                    ElseIf (ObjectType = "ComboBox" Or ObjectType = "ColorComboBox") And Not CmbxSV Then
                        Object_Name.Text = Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                    ElseIf ObjectType = "RadioButton" Then
                        Object_Name.checked = CType(Val(Query_DT.Rows(RowNumber).Item(Field_Name).ToString), Boolean)
                    Else
                        Object_Name.text = Query_DT.Rows(RowNumber).Item(Field_Name).ToString
                    End If
                End If
            Else
ClearThisField:
                If DntEmptyOBJ_IFEmptyDS = False Then
                    If ObjectType = "CheckBox" Then
                        Object_Name.CheckState = CheckState.Unchecked
                    ElseIf ObjectType = "Boolean" Then
                        If IgnorFromDebugStop.ToString.Contains(Field_Name) Then
                            If IsNothing(defaultValue) Then
                                Return False
                            Else
                                Return defaultValue
                            End If
                        Else
                            If IsNothing(defaultValue) Then
                                Return String.Empty
                            Else
                                Return defaultValue
                            End If
                        End If

                    ElseIf ObjectType = "String" Then
                        Object_Name = String.Empty
                    ElseIf ObjectType = "ComboBox" Or ObjectType = "ColorComboBox" Then
                        Object_Name.text = Nothing
                    ElseIf ObjectType = "RadioButton" Then
                        Object_Name.checked = False
                    Else
                        Object_Name.text = defaultValue
                    End If
                End If
            End If
        Catch ex As Exception
            Dim myMethod As MethodInfo = Object_Name.GetType.GetMethod("Name")
            Dim Obj_Name = Nothing
            Dim ObjectName = Nothing
            If Not IsNothing(myMethod) Then
                Obj_Name = Object_Name.Name.ToString
            End If
            If Object_Name.GetType = GetType(Boolean) Then
                If Object_Name = False Then
                    ObjectName = Nothing
                End If
            Else
                ObjectName = Object_Name.name
            End If
            DisplayAIOCriticalError(ex, ReturnSQLDbCommandParameters() & " Error While Filling (" & ObjectName & " By  " & Field_Name & ") With Type " & ObjectType,,,, 10)
            'Return Nothing
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Function
    Public Sub CallQuery_Class(ByVal QueryWhere As Dictionary(Of String, String),
                                                        ByVal FillObjField As Dictionary(Of Object, String))
        Try
            'Cursor.Current = Cursors.WaitCursor
            If Not IsNothing(QueryWhere) Then
                For Each Obj In QueryWhere
                    Dim Field_Name As String = Obj.Key
                    Dim Object_Name As Object = Obj.Value
                    Dim ObjectType = Object_Name.GetType().Name.ToString
                    QueryAddWhere(Field_Name, Object_Name.ToString)
                Next
            End If
            If Not IsNothing(FillObjField) Then
                For Each Obj In FillObjField
                    Dim Object_Name As Object = Obj.Key
                    Dim Field_Name As String = Obj.Value
                    FillAddObjField(Object_Name, Field_Name)
                Next
            End If
        Catch ex As Exception
            DisplayAIOCriticalError(ex, ReturnSQLDbCommandParameters(),,,, 15)
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Sub

    Dim TryCount = 0
    Private Function CollectColumnsType() As Boolean
        Try
            Application.DoEvents()
            Using GFV As New RefreshWaitAWhileTitles_Class
                Dim SafeTableNameRules As New System.Text.RegularExpressions.Regex("[^-0-9a-zA-Z_.-]")
                Query_TableName = SafeTableNameRules.Replace(Query_TableName, String.Empty)
                If LCase(Query_TableName) = LCase("INFORMATION_SCHEMA.TABLES") Then
                    If GFV.GetFieldValue(Query_TableName, "Select table_name from Information_Schema.Tables where (table_type = 'base table' or table_type = 'view') and table_name <> 'dtproperties' Order By table_name",, 1, 0,,, 1,,, 1) Then
                        For Each SCHEMACOLUMN In GetFieldValueDS.Tables("INFORMATION_SCHEMA.TABLES").Rows
                            If (Query_ColumnsType.ContainsKey(SCHEMACOLUMN.item("table_name")) = False) Then
                                Query_ColumnsType.Add(SCHEMACOLUMN.item("table_name").tolower, "varchar")
                            End If
                        Next
                        Return True
                    End If
                ElseIf LCase(Query_TableName) = LCase("INFORMATION_SCHEMA.COLUMNS") Then
                    If GFV.GetFieldValue(, "Select COLUMN_NAME, TABLE_NAME, DATA_TYPE From INFORMATION_SCHEMA.COLUMNS AS SCHEMACOLUMNS Where Table_Name = '" & Query_TableName & "'",, 1, 0,,, 1) Then
                        For Each SCHEMACOLUMN As DataColumn In GetFieldValueDS.Tables("INFORMATION_SCHEMA.COLUMNS").Columns
                            Query_ColumnsType.Add(SCHEMACOLUMN.ColumnName.ToLower, SCHEMACOLUMN.DataType.Name.ToLower)
                        Next
                        Return True
                    End If
                ElseIf GFV.GetFieldValue(, "Select COLUMN_NAME ,  DATA_TYPE From INFORMATION_SCHEMA.COLUMNS AS SCHEMACOLUMNS Where Table_Name = '" & Query_TableName & "'",, 1, 0,,, 1,,, 1) Then
                    For Each SCHEMACOLUMN In GetFieldValueDS.Tables("INFORMATION_SCHEMA.COLUMNS").Rows
                        If (Query_ColumnsType.ContainsKey(SCHEMACOLUMN.item("COLUMN_NAME")) = False) Then
                            Query_ColumnsType.Add(SCHEMACOLUMN.item("COLUMN_NAME").tolower, SCHEMACOLUMN.item("DATA_TYPE").tolower)
                        End If
                    Next
                    Return True
                End If
            End Using
        Finally
        End Try
    End Function

    Public Sub Query_OpenConnectionIfClosed(Optional ByVal CreatSQLDbCommand As Boolean = True)
        Try
            'Cursor.Current = Cursors.WaitCursor
            Dim Today As Date
            If QCSQLDBConnection.State <> ConnectionState.Open Then
                If QCSQLDBConnection.State <> ConnectionState.Closed Then
                    QCSQLDBConnection.Close()
                End If
                If QCSQLDBConnection.State = ConnectionState.Open Then
                    QCSQLDBConnection.Close()
                End If
                While Not Query_OpenConnection(, CreatSQLDbCommand)
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "لم تفلح محاولة الاتصال بقاعدة البيانات... راجع بيانات الاتصال تم عاود الاتصا مرة اخرى"
                    Else
                        Msg = "An attempt to connect to the database was unsuccessful... Check the connection information. and retry to connect again"
                        End If
                        ShowMsg(Msg,, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                        Exit Sub
                    Application.DoEvents()
                End While
            End If
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Sub
    Public MyTimer As Double
    Public Function WaitThreeSeconds()
        Try
            'Cursor.Current = Cursors.WaitCursor
            Dim Today As Date ' = Today.Date
            MyTimer = Microsoft.VisualBasic.DateAndTime.Timer + (3 * 1000)
            While Microsoft.VisualBasic.DateAndTime.Timer >= MyTimer
                If Now.Date <> Today.Date Then
                    Today = Now.Date
                    MyTimer = Microsoft.VisualBasic.DateAndTime.Timer + (3 * 1000)
                End If
            End While
            Return True
        Finally
            'Cursor.Current = Cursors.Default
        End Try
    End Function

    Public Function Query_OpenConnection(Optional ByVal DisMsg As Boolean = False, Optional ByVal CreatSQLDbCommand As Boolean = True) As Boolean
        Try
            'Cursor.Current = Cursors.WaitCursor
ReOpenConnection:
            If Not QCSQLDBConnection.State = ConnectionState.Closed And
                Not QCSQLDBConnection.State = ConnectionState.Open Then
                QCSQLDBConnection.Close()
                WaitThreeSeconds()
                SQLDbCommand = QCSQLDBConnection.CreateCommand
                SQLDbCommand.CommandTimeout = 30
                QCSQLDBConnection.ConnectionString = SQLConnStr
                QCSQLDBConnection.Open()
            ElseIf Not QCSQLDBConnection.State = ConnectionState.Open Then
                If CreatSQLDbCommand Then
                    SQLDbCommand = QCSQLDBConnection.CreateCommand
                Else
                    Dim x = 1
                End If
                'SQLDbCommand.Connection.ConnectionString = SQLConnStr
                SQLDbCommand.CommandTimeout = 30
                QCSQLDBConnection.ConnectionString = SQLConnStr
                QCSQLDBConnection.Open()
            End If
        Catch ex As Exception
            DisplayAIOCriticalError(ex,,,,, 30)
            Return False
        Finally
        End Try
        Return True
    End Function
    Public Sub Close()
        If QCSQLDBConnection.State = ConnectionState.Open Then
            QCSQLDBConnection.Close()
        End If
        Me.Finalize()
        Me.Dispose()
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        GC.SuppressFinalize(Me)
    End Sub
    Public Function DisplayAIOCriticalError(ByVal ex As Exception,
                                                 Optional AdditionalMessage As String = Nothing,
                                                 Optional ByVal SendMailTo As String = Nothing,
                                                 Optional ByVal ShowForm As Boolean = True,
                                                 Optional ByVal AsMessageBox As Boolean = False,
                                                 Optional ByVal Dly As Integer = 0,
                                                 Optional ByVal SendMail As Boolean = False) As Boolean
        Dim Language
        If MagNote_Form.Language_Btn.Text = "E" Then
            Language = 0
        Else
            Language = 1
        End If
        If Not IsNothing(ex.InnerException) Then
            AdditionalMessage &= vbNewLine & "Ex InnerException Is [[[(" & ex.InnerException.Message & ")]]]"
        End If
        If ex.Message.Contains("A device attached to the system is not functioning") Then
            ShowForm = False
        End If
        If ex.Message.Contains("The process cannot access the file") And
            ex.Message.Contains("because it is being used by another process.") Then
            If MagNote_Form.Language_Btn.Text = "E" Then
                AdditionalMessage = "ربما يكون الملف الذى تحاول استخدامه قيد الاستخدام فى عملية أخرى "
            End If
        End If
        Dim DAIOCE = "AIO Critical Error"
        If Not String.IsNullOrEmpty(AdditionalMessage) Then
            DAIOCE &= vbNewLine & AdditionalMessage
        End If
        DAIOCE &= ex.Message & vbNewLine
        If Not IsNothing(ex.InnerException) Then
            DAIOCE &= ex.InnerException.Message.ToString & vbNewLine
        End If
        MessageBox.Show(DAIOCE & vbNewLine & ex.StackTrace, "", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, False)

    End Function
    Public MyProvider As DbProviderFactory
    Public MyAdapter As DbDataAdapter
    Public MyCommand As DbCommand
    Public MyConnectionSettings As ConnectionStringSettings
    Public MyConnection As DbConnection
    Public Function OpenDB(ByVal KindOfDB As String) As Boolean
        Try
            'Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(Application.StartupPath & "\" & Application.ProductName & ".exe")
            'Dim section As ConnectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)
            'If Not section.SectionInformation.IsProtected Then
            '    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
            'End If
            'config.Save()
            MyConnectionSettings = ConfigurationManager.ConnectionStrings(KindOfDB)
            If IsNothing(MyConnectionSettings) Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    Msg = "لم يتم العثور على المعلومات الكافية لفتح الاتصال بقاعدة البيانات ... ربما هذا الاتصال غير مسجل بملف إعدادات الإتصالات"
                Else
                    Msg = "Not Enough Information To Open The Connection To The Database Was Found ... Maybe This Connection Is Not Registered With The Conniction Settings File"
                End If
                MessageBox.Show(Msg, "InfoSysMe (AIO)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
                Return False
            End If
            MyProvider = DbProviderFactories.GetFactory(MyConnectionSettings.ProviderName)
            MyConnection = MyProvider.CreateConnection()
            MyConnection.ConnectionString = MyConnectionSettings.ConnectionString
            MyAdapter = MyProvider.CreateDataAdapter()
            MyCommand = MyProvider.CreateCommand()
            MyCommand.CommandTimeout = 0
            MyAdapter.SelectCommand = MyCommand
            MyAdapter.UpdateCommand = MyCommand
            MyAdapter.InsertCommand = MyCommand
            MyAdapter.SelectCommand.Connection = MyConnection

            MyGetFieldValueProvider = DbProviderFactories.GetFactory(MyConnectionSettings.ProviderName)
            MyGetFieldValueAdapter = MyGetFieldValueProvider.CreateDataAdapter()
            MyGetFieldValueCommand = MyGetFieldValueProvider.CreateCommand()
            MyGetFieldValueCommand.CommandTimeout = 0
            MyGetFieldValueAdapter.SelectCommand = MyGetFieldValueCommand
            MyGetFieldValueAdapter.UpdateCommand = MyGetFieldValueCommand
            MyGetFieldValueAdapter.InsertCommand = MyGetFieldValueCommand
            MyGetFieldValueAdapter.SelectCommand.Connection = MyConnection

            MyConnection.Open()
            If MyConnection.State <> ConnectionState.Open Then
                Return False
            End If
            SQLConnStr = MyConnectionSettings.ConnectionString

        Catch SQLExp As SqlException
            DisplayAIOCriticalError(SQLExp)
            Return False
        Catch IOExp As IOException
            DisplayAIOCriticalError(IOExp)
            Return False
        Catch ex As Exception
            DisplayAIOCriticalError(ex)
            Return False
        Finally
        End Try
        Return True
    End Function
    Public Function ConvertODBCToSQLDB()
        Dim Provider = DbProviderFactories.GetFactory(MyConnectionSettings.ProviderName)
        If Provider.GetType.Name.ToString = "SqlClientFactory" Then
            Return MyConnectionSettings.ConnectionString
        End If
        Dim Server = MyConnection.DataSource
        Dim Database = MyConnection.Database
        Dim ThisConnectionSettings = MyConnectionSettings.ConnectionString
        Dim SQLDBConnectionString = "Database=" & Database & ";"
        Dim m As Match = Regex.Match(ThisConnectionSettings, "uid=(.*?)(?:;)", RegexOptions.IgnoreCase)
        SQLDBConnectionString &= IIf(m.Success, "Uid=" & m.Groups(1).Value & ";", Nothing)
        m = Regex.Match(ThisConnectionSettings, "pwd=(.*?)(?:;)", RegexOptions.IgnoreCase)
        SQLDBConnectionString &= IIf(m.Success, "Pwd=" & m.Groups(1).Value & ";", Nothing)
        m = Regex.Match(ThisConnectionSettings, "ServerName=(.*?)(?:;)", RegexOptions.IgnoreCase)
        Server = IIf(m.Success, m.Groups(1).Value, MyConnection.DataSource)
        SQLDBConnectionString &= "Server=" & Server & ";"
        Return SQLDBConnectionString
    End Function

End Class


