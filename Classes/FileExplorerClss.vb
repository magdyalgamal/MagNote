Imports System.IO
Imports Shell32

Public Class FileExplorerClss
    Private ReadOnly _treeView As TreeView
    Private ReadOnly _listView As ListView
    Private ReadOnly _imageList As ImageList

    ' Constructor to initialize the FileExplorer
    Public Sub New(treeView As TreeView, listView As ListView, imageList As ImageList)
        Try
            _treeView = treeView
            _listView = listView
            _imageList = imageList

            ' Initialize TreeView and ListView
            InitializeTreeView()
            InitializeListView()

            ' Set up event handlers
            AddHandler _treeView.BeforeExpand, AddressOf TreeView_BeforeExpand
            AddHandler _treeView.AfterSelect, AddressOf TreeView_AfterSelect
            AddHandler _listView.DoubleClick, AddressOf ListView_DoubleClick
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    ' Initialize the TreeView with drive nodes
    Private Sub InitializeTreeView()
        Try
            _treeView.Nodes.Clear()
            _treeView.ImageList = _imageList
            Dim rootNodeText As String
            For Each drive As DriveInfo In DriveInfo.GetDrives()
                rootNodeText = Nothing
                Try
                    Dim volumeLabel As String = If(String.IsNullOrWhiteSpace(drive.VolumeLabel), "Local Disk", drive.VolumeLabel)
                    'If Not String.IsNullOrEmpty(drive.VolumeLabel) Then
                    rootNodeText = $"{drive.Name} ({volumeLabel})"
                    'End If
                Catch ex As Exception
                End Try

                Dim driveNode As New TreeNode(drive.Name) With {
                .Tag = drive.Name,
                .ImageIndex = 0,
                .SelectedImageIndex = 0
            }
                If Not String.IsNullOrEmpty(rootNodeText) Then
                    driveNode.Text = rootNodeText
                End If
                _treeView.Nodes.Add(driveNode)
                LoadDummyNode(driveNode)
            Next
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    ' Initialize the ListView properties
    Private Sub InitializeListView()
        _listView.View = View.Details
        _listView.Columns.Clear()
        _listView.Columns.Add("Name", 250)
        _listView.Columns.Add("Label", 250)
        _listView.Columns.Add("Type", 100)
        _listView.Columns.Add("Size", 100, HorizontalAlignment.Right)
        _listView.Columns.Add("Creation Date", 160)
        _listView.Columns.Add("Date Modifaied", 160)
        _listView.SmallImageList = _imageList
    End Sub

    ' Load dummy nodes for expandable folders
    Private Sub LoadDummyNode(node As TreeNode)
        node.Nodes.Add("Dummy")
    End Sub
    Dim ExpandedFolders As String
    ' Populate TreeView with subfolders
    Private Sub TreeView_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs, Optional Tree_Node As TreeNode = Nothing)
        Dim node As TreeNode = e.Node
        If Not IsNothing(Tree_Node) Then
            node = Tree_Node
        End If
        'node.Nodes.Clear()
        Try
            Dim directories As String()
            Try
                directories = Directory.GetDirectories(node.Tag.ToString())
            Catch ex As Exception
                Exit Sub
            End Try
            For Each dir As String In directories
                If Not IsNothing(FindNodeByText(node.Nodes, dir)) Then
                    Exit Sub
                End If
                Dim dirInfo As New DirectoryInfo(dir)
                ExpandedFolders &= vbNewLine & dirInfo.FullName
                Dim dirNode As New TreeNode(dirInfo.Name) With {
                    .Tag = dirInfo.FullName,
                    .ImageIndex = 0,
                    .SelectedImageIndex = 0
                }
                node.Nodes.Add(dirNode)
                LoadDummyNode(dirNode)
                'ApplicationDoEvents(MagNote_Explorer_Form)
            Next
        Catch ex As Exception
            MessageBox.Show($"Error loading folders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' Populate ListView with files and folders
    Private Sub TreeView_AfterSelect(sender As Object, e As TreeViewEventArgs)
        Try
            If IsNothing(e.Node) Then
                Exit Sub
            ElseIf IsNothing(e.Node.Tag) Then
                Exit Sub
            End If
            If e.Node.Tag?.ToString() = "C:\$Recycle.Bin" Then
                LoadRecycleBinContents()
                Exit Sub
            End If
            Dim arg As New TreeViewCancelEventArgs(e.Node, False, Nothing)
            TreeView_BeforeExpand(sender, arg)
            Dim selectedPath As String = e.Node.Tag.ToString()
            MagNote_Explorer_Form.Current_Path_CmbBx.Text = selectedPath
            AddPath()
            PopulateListView(selectedPath)
        Catch ex As Exception
            MessageBox.Show($"Error loading folders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ChangeNodeColor(sender, e)
        End Try
    End Sub
    Private Function AddPath() As Boolean
        Try
            If MagNote_Explorer_Form.ActiveControl.Name = MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.Name Or
          MagNote_Explorer_Form.ActiveControl.Name = MagNote_Explorer_Form.Mag_Explorer_Directory_Contents_LstVw.Name Then
                For Each Item In MagNote_Explorer_Form.Current_Path_CmbBx.Items
                    If Item = MagNote_Explorer_Form.Current_Path_CmbBx.Text Then
                        Exit Function
                    End If
                Next
                MagNote_Explorer_Form.Current_Path_CmbBx.Items.Add(MagNote_Explorer_Form.Current_Path_CmbBx.Text)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    ' Populate ListView with files and subfolders
    Private Sub PopulateListView(path As String)
        _listView.Items.Clear()

        Try
            ' Add directories
            Dim directories As String()
            Try
                directories = Directory.GetDirectories(path)
            Catch ex As Exception
                Exit Sub
            End Try
            For Each dir As String In directories
                Dim dirInfo As New DirectoryInfo(dir)
                Dim item As New ListViewItem(dirInfo.Name) With {
                    .Tag = dirInfo.FullName,
                    .ImageIndex = 0
                }
                item.SubItems.Add("")
                item.SubItems.Add("Folder")
                item.SubItems.Add("")
                _listView.Items.Add(item)
            Next

            ' Add files
            Dim files As String() = Directory.GetFiles(path)
            Dim FileLabel As String
            For Each file As String In files
                Dim fileInfo As New FileInfo(file)

                If MagNote_Form.MagNoteFileFormat(fileInfo.Name, 1, 0) Then
                    FileLabel = MagNote_Form.ReadFile(file,,,,,,, 1)
                Else
                    FileLabel = Nothing
                End If

                Dim item As New ListViewItem(FileInfo.Name) With {
                    .Tag = FileInfo.FullName,
                    .ImageIndex = 1
                }
                item.SubItems.Add(FileLabel)
                item.SubItems.Add(fileInfo.Extension)
                item.SubItems.Add(fileInfo.Length.ToString("N0"))
                item.SubItems.Add(fileInfo.CreationTime)
                item.SubItems.Add(fileInfo.LastWriteTime)
                _listView.Items.Add(item)
            Next
        Catch ex As Exception
            ShowMsg($"Error loading files: {ex.Message}" & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    ' Handle double-click on ListView to navigate into a folder
    Public Sub ListView_DoubleClick(sender As Object, e As EventArgs, Optional AsSelectedPath As String = Nothing)
        Try
            If Not _treeView.SelectedNode.IsExpanded Then
                _treeView.SelectedNode.Expand()
            End If
            'If node.Parent IsNot Nothing Then

            'End If
            If Not String.IsNullOrEmpty(AsSelectedPath) Then
                GoTo UsingUpLevel
            End If
            If _listView.SelectedItems.Count > 0 Then
                Dim selectedItem As ListViewItem = _listView.SelectedItems(0)
                Dim selectedPath As String = selectedItem.Tag?.ToString()
UsingUpLevel:
                If Not String.IsNullOrEmpty(AsSelectedPath) Then
                    selectedPath = AsSelectedPath
                End If
                ' Check if the selected item is a folder
                If Directory.Exists(selectedPath) Then
                    ' Update the TreeView to reflect the new selection
                    Dim matchingNode = FindTreeNodeByPath(_treeView.Nodes, selectedPath)
                    If matchingNode IsNot Nothing Then
                        _treeView.SelectedNode = matchingNode
                        matchingNode.Expand()
                        PopulateListView(selectedPath)
                        MagNote_Explorer_Form.Current_Path_CmbBx.Text = selectedPath
                        AddPath()
                    Else
                        Dim arg As New TreeViewCancelEventArgs(_treeView.SelectedNode, False, Nothing)
                        TreeView_BeforeExpand(sender, arg)
                    End If
                Else
                    If MagNote_Form.MagNoteFileFormat(selectedPath, 1, 0) Or
                    Path.GetExtension(selectedPath) = ".txt" Then
                        UseArgFile = selectedPath
                        ExternalFilePath = Path.GetDirectoryName(selectedPath)
                        ExternalFileName = Path.GetFileName(selectedPath)
                        DirectCast(MagNote_Form, MagNote_Form).Open_Note_TlStrpBtn_Click(MagNote_Form.Note_TlStrp.Items("OpenToolStripButton"), EventArgs.Empty, 1)
                        AddMagNoteRTF(1)
                    Else
                        System.Diagnostics.Process.Start(selectedPath)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Sub LoadRecycleBinContents()
        Try
            _listView.Items.Clear()

            ' Create a Shell object
            Dim shell As New Shell()
            ' Access the Recycle Bin folder
            Dim recycleBin As Folder = shell.NameSpace(ShellSpecialFolderConstants.ssfBITBUCKET)

            If recycleBin IsNot Nothing Then
                ' Iterate through the items in the Recycle Bin
                For Each item As FolderItem In recycleBin.Items()
                    Dim listItem As New ListViewItem(item.Name)
                    listItem.SubItems.Add(recycleBin.GetDetailsOf(item, 1)) ' Original location
                    listItem.SubItems.Add(recycleBin.GetDetailsOf(item, 2)) ' Date deleted
                    _listView.Items.Add(listItem)
                Next
            Else
                MessageBox.Show("Unable to access the Recycle Bin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Public Shared Function FindNodeByText(nodes As TreeNodeCollection, searchText As String) As TreeNode
        Try
            For Each node As TreeNode In nodes
                If Not IsNothing(node.Tag) Then
                    'If node.Text.Equals("Users") Then
                    '    Dim x = 1
                    'End If
                    If node.Tag.Equals(searchText) Then
                        Return node
                    End If
                End If
                ' Search recursively in child nodes
                Dim foundNode = FindNodeByText(node.Nodes, searchText)
                If foundNode IsNot Nothing Then
                    Return foundNode
                End If
            Next

            Return Nothing
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    ' Find a TreeNode by its path
    Private Function FindTreeNodeByPath(nodes As TreeNodeCollection, path As String) As TreeNode
        Try
            For Each node As TreeNode In nodes
                Debug.Print(node.Tag?.ToString())
                If node.Tag?.ToString() = path Then
                    Return node
                    Exit Function
                End If
                If node.Nodes.Count > 0 Then
                    Dim foundNode = FindTreeNodeByPath(node.Nodes, path)
                    If foundNode IsNot Nothing Then
                        Return foundNode
                        Exit Function
                    End If
                End If
            Next
            Return Nothing
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    Private previousNode As TreeNode = Nothing
    Private Sub ChangeNodeColor(sender As Object, e As TreeViewEventArgs)
        Try
            ' Revert the color of the previous node
            If previousNode IsNot Nothing Then
                'previousNode.NodeFont = _treeView.Font ' Reset to default font
                previousNode.BackColor = MagNote_Explorer_Form.BackColor ' Default background color
                previousNode.ForeColor = MagNote_Explorer_Form.ForeColor ' Default text color
            End If

            ' Change the color of the currently selected node
            Dim currentNode As TreeNode = _treeView.SelectedNode
            If currentNode IsNot Nothing Then
                currentNode.BackColor = Color.DarkGreen
                currentNode.ForeColor = Color.White
                'currentNode.NodeFont = New Font(_treeView.Font, FontStyle.Bold)
            End If

            ' Update the previous node reference
            previousNode = currentNode
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    ' 📂 Expand TreeView to match found file path


    Public Sub ExpandToMyPath(filePath As String)
        Try
            Dim directoryPath As String = Path.GetDirectoryName(filePath)
            Dim parts = directoryPath.Split(Path.DirectorySeparatorChar)
            Dim currentNodes = MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.Nodes
            Dim currentPath As String = ""
            Dim dirInfo As New DirectoryInfo(filePath)
            Dim rootPath As String = dirInfo.Root.FullName

            Dim parentDir As String = Directory.GetParent(filePath).FullName
            If ExpandedFolders.Contains(parentDir) Then
                Dim foundNode As TreeNode = FindNodeByText(MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.Nodes, parentDir)
                If foundNode IsNot Nothing Then
                    MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.SelectedNode = foundNode
                    foundNode.EnsureVisible()  ' Scroll into view
                    foundNode.Expand()         ' Optional: expand if it has children
                    Exit Sub
                End If
            End If
            For Each part In parts
                If String.IsNullOrWhiteSpace(part) Then Continue For
                currentPath = If(currentPath = "", part & Path.DirectorySeparatorChar, Path.Combine(currentPath, part))
                For Each nod In currentNodes
                    ApplicationDoEvents(MagNote_Explorer_Form)
                    If nod.nodes.count > 0 And currentPath <> rootPath Then
                        ExpandToMySubPath(MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.SelectedNode, currentPath)
                        Exit For
                    End If
                    If nod.tag = currentPath Then
                        TreeView_BeforeExpand(Nothing, New TreeViewCancelEventArgs(nod, False, TreeViewAction.Expand))
                        nod.Expand()
                        Exit For
                    End If
                Next
            Next
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    Private Function ExpandToMySubPath(nod, currentpath)
        Try
            For Each nod1 In nod.nodes
                If nod1.tag = currentpath Then
                    TreeView_BeforeExpand(Nothing, New TreeViewCancelEventArgs(nod1, False, TreeViewAction.Expand))
                    nod1.Expand()
                    MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.SelectedNode = Nothing
                    MagNote_Explorer_Form.Mag_Explorer_Directory_TrVw.SelectedNode = nod1
                    ApplicationDoEvents(MagNote_Explorer_Form)
                    Exit For
                End If
            Next
        Catch ex As Exception
            ShowMsg(ex.Message & CurrentMagNote(), "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
    ' 🔎 Search for file recursively
    Private Function FindFile(folder As String, fileName As String) As String
        Try
            For Each file In Directory.GetFiles(folder)
                If Path.GetFileName(file).ToLower().Contains(fileName.ToLower()) Then
                    Return file
                End If
            Next
            For Each Drctry In Directory.GetDirectories(folder)
                Dim result = FindFile(Drctry, fileName)
                If result IsNot Nothing Then Return result
            Next
        Catch
            ' Skip protected folders
        End Try
        Return Nothing
    End Function
End Class
