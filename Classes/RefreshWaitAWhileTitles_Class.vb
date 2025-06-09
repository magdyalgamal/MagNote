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
