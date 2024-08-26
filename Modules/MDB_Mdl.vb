Imports System.Data.OleDb
Imports System.IO

Module MDB_Mdl
    Public Function DoOpenConnection()
        OpenConnection(Application.StartupPath & "\MagNotes.mdb")
    End Function
    Public Function OpenConnection(Optional ByVal DB_FILENAME As String = Nothing) As Boolean
        cnnStr.Close()

        If cnnStr.State <> ConnectionState.Open Then
            Dim dbpath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
            dbpath = New Uri(dbpath).LocalPath
            If IsNothing(DB_FILENAME) Then
                DB_FILENAME = dbpath & "\Sucker_POS.mdb"
            End If
            cnnStr.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" & DB_FILENAME
            Try
                cnnStr.Open()
            Catch ex As Exception
                If Not File.Exists(DB_FILENAME) Then '"Provider=Microsoft.ACE.OLEDB.16.0;"
                    CreateAccessDatabase(DB_FILENAME)
                End If
            End Try
        End If
    End Function
    Public Function CreateAccessDatabase(ByVal DatabaseFullPath As String) As Boolean
        Dim bAns As Boolean
        Dim cat As New ADOX.Catalog()
        Try
            Dim sCreateString As String
            sCreateString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" & DatabaseFullPath
            cat.Create(sCreateString)
            bAns = True
        Catch Excep As System.Runtime.InteropServices.COMException
            bAns = False
        Finally
            cat = Nothing
        End Try
        Return bAns
    End Function
    Private Sub Add_Field_Btn_Click(sender As Object, e As EventArgs)
        Try
            OpenConnection()

            Using cnnStr ' = New System.Data.OleDb.OleDbConnection(cnnStr.ConnectionString)
                Using cmd = cnnStr.CreateCommand()
                    cmd.CommandText = "INSERT INTO Item_Card([Item_No],[Item_Name],[Division_No],[Group_No],[Category_No],[Item_Price],[Unit_Of_Measure],[Description]) VALUES(@Item_No,@Item_Name,@Division_No,@Group_No,@Category_No,@Item_Price,@Unit_Of_Measure,@Description)"
                    Dim dbpath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
                    dbpath = New Uri(dbpath).LocalPath
                    Dim FILENAME = dbpath & "\PriceList.txt"
                    Dim ItemNumber = 500
                    If File.Exists(FILENAME) Then
                        For Each Item In Split(My.Computer.FileSystem.ReadAllText(FILENAME, System.Text.Encoding.UTF8), vbLf)
                            If IsNothing(Item) Then Continue For
                            If Item.Length = 0 Then Continue For
                            cmd.Parameters.AddWithValue("@ItemNo", ItemNumber)
                            cmd.Parameters.AddWithValue("@Item_Name", Split(Item, ",").ToList.Item(0))

                            cmd.Parameters.AddWithValue("@Division_No", 1)
                            cmd.Parameters.AddWithValue("@Group_No", 11)
                            cmd.Parameters.AddWithValue("@Category_No", 0)

                            cmd.Parameters.AddWithValue("@Item_Price", Replace(Split(Item, ",").ToList.Item(1), vbCr, ""))

                            cmd.Parameters.AddWithValue("@Unit_Of_Measure", 5)
                            cmd.Parameters.AddWithValue("@Description", "Item Added By Magdy AlGamal")
                            cmd.ExecuteNonQuery()
                            cmd.Parameters.Clear()
                            ItemNumber += 1
                        Next
                    End If
                End Using
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message)
        End Try
    End Sub

    Private Sub Read_Table_Btn_Click(sender As Object, e As EventArgs)
        Try
            If Not IsNothing(ds.Tables("Table_Name")) Then
                ds.Tables("Table_Name").Clear()
            End If
            OpenConnection()
            Using cnnStr
                Using cmd = cnnStr.CreateCommand()
                    Dim SQLQuery As String = "Select * from " & "Table_Name"
                    Dim dataadapter As New OleDbDataAdapter(SQLQuery, cnnStr)
                    cmd.CommandText = SQLQuery
                    dataadapter.Fill(ds, "Table_Name")
                End Using
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message)
        End Try
    End Sub

    Private Sub Add_Primary_Key_Btn_Click(sender As Object, e As EventArgs)
        Try
            'Dim Sql As String = "SELECT FloatTable.DateAndTime, FloatTable.Millitm, FloatTable.TagIndex, FloatTable.Val, StringTable.TagIndex"
            'Sql += " FROM FloatTable INNER JOIN StringTable ON FloatTable.ID = StringTable.ID"

            'OpenConnection()

            'Using cnnStr
            '    Using cmd = cnnStr.CreateCommand()
            '        Dim IndexFields As String
            '        For Each Field In Table_Schema_DGV.SelectedRows
            '            IndexFields &= "[" & Field.cells("Column_Name").value & "],"
            '        Next
            '        IndexFields = Microsoft.VisualBasic.Left(IndexFields, IndexFields.Length - 1)
            '        cmd.CommandText = "create Index [PrimaryKey] On [" & Table_Name_TxtBx.Text & "] (" & IndexFields & ") WITH PRIMARY"
            '        cmd.ExecuteNonQuery()
            '        ShowMsg("PrimaryKey Created Successfully")
            '    End Using
            'End Using
        Catch ex As Exception
            ShowMsg(ex.Message)
        End Try
    End Sub
    Private Sub InsertNewRow()
        Try
            OpenConnection()
            Using cnnStr
                Using cmd = cnnStr.CreateCommand()
                    cmd.CommandText = "INSERT INTO Item_Card([Item_No],[Item_Name],[Division_No],[Group_No],[Category_No],[Item_Price],[Unit_Of_Measure],[Description]) VALUES(@Item_No,@Item_Name,@Division_No,@Group_No,@Category_No,@Item_Price,@Unit_Of_Measure,@Description)"
                    cmd.Parameters.AddWithValue("@Item_No", "Item_No")
                    cmd.Parameters.AddWithValue("@Item_Name", "Item_Name")

                    cmd.Parameters.AddWithValue("@Division_No", "Division_No")
                    cmd.Parameters.AddWithValue("@Group_No", "Group_No")
                    cmd.Parameters.AddWithValue("@Category_No", "Category_No")

                    cmd.Parameters.AddWithValue("@Item_Price", "Item_Price")

                    cmd.Parameters.AddWithValue("@Unit_Of_Measure", "Unit_Of_Measure")
                    cmd.Parameters.AddWithValue("@Description", "Description")
                    cmd.ExecuteNonQuery()
                    ShowMsg("Record Added Successfully")
                    cmd.Parameters.Clear()
                End Using
            End Using
        Catch ex As Exception
            ShowMsg(ex.Message)
        End Try
    End Sub
End Module
