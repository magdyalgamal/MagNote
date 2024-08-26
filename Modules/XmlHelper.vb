#Region "© Copyright 2005, Rudy.net's Stuff - Rudy Guzman, XmlHelper"
' XmlHelper
' 
' © Copyright 2005, Rudy.net's Stuff - Rudy Guzman
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, 
' are permitted provided that the following conditions are met:
'
'  * Redistributions of source code must retain the above copyright notice, 
'    this list of conditions and the following disclaimer. 
'  * Redistributions in binary form must reproduce the above copyright notice,
'    this list of conditions and the following disclaimer in the documentation
'    and/or other materials provided with the distribution. 
'  * Neither the name of Rudy.net, XmlHelper, nor the names of its contributors 
'    may be used to endorse or promote products derived from this software
'    without specific prior written permission. 
'
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
' ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
' FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
' (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
' LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
' ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
' (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
' EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#End Region


Imports System.Xml
Imports System.Collections.Specialized
Public Class XmlHelper

#Region "XmlDocument Creation"
    Shared Function CreateXmlDocument() As XmlDocument
        Dim doc As New XmlDocument
        Dim decl As XmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", "")
        doc.InsertBefore(decl, doc.DocumentElement)
        Return doc
    End Function

    Shared Function CreateXmlDocument(ByVal rootName As String) As XmlDocument
        Dim doc As New XmlDocument
        Dim decl As XmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", "")
        doc.InsertBefore(decl, doc.DocumentElement)
        Dim newNode As XmlNode = doc.CreateElement(rootName)
        doc.AppendChild(newNode)
        Return doc
    End Function

#End Region

#Region "GetItemValue(node, name, value)"
    'Get string value from a node.
    Shared Function GetItemValue(ByRef node As XmlNode, ByVal itemName As String, ByRef value As String) As Boolean
        Dim success As Boolean = False
        If Not node Is Nothing Then
            If Not node.Item(itemName) Is Nothing Then
                value = node.Item(itemName).InnerText
                success = True
            End If
        End If
        Return success
    End Function

    'Get Int32 value from a node.
    Shared Function GetItemValue(ByRef node As XmlNode, ByVal itemName As String, ByRef value As Int32) As Boolean
        Dim success As Boolean = False
        If Not node Is Nothing Then
            If Not node.Item(itemName) Is Nothing Then
                value = Int32.Parse(node.Item(itemName).InnerText)
                success = True
            End If
        End If
        Return success
    End Function

    'Get UInt32 value from a node.
    Shared Function GetItemValue(ByRef node As XmlNode, ByVal itemName As String, ByRef value As UInt32) As Boolean
        Dim success As Boolean = False
        If Not node Is Nothing Then
            If Not node.Item(itemName) Is Nothing Then
                value = UInt32.Parse(node.Item(itemName).InnerText)
                success = True
            End If
        End If
        Return success
    End Function
#End Region

#Region "SetItemValue(node, name, value)"
    'Set string value in a node
    Shared Function SetItemValue(ByRef node As XmlNode, ByVal itemName As String, ByVal value As String) As Boolean
        Dim success As Boolean = False
        If Not node Is Nothing Then
            If Not node.Item(itemName) Is Nothing Then
                node.Item(itemName).InnerText = value
                success = True
            End If
        End If
        Return success
    End Function

    'Set int32 value in a node
    Shared Function SetItemValue(ByRef node As XmlNode, ByVal itemName As String, ByVal value As Int32) As Boolean
        Dim success As Boolean = False
        If Not node Is Nothing Then
            If Not node.Item(itemName) Is Nothing Then
                node.Item(itemName).InnerText = value.ToString
                success = True
            End If
        End If
        Return success
    End Function

    'Set uint32 value in a node
    Shared Function SetItemValue(ByRef node As XmlNode, ByVal itemName As String, ByVal value As UInt32) As Boolean
        Dim success As Boolean = False
        If Not node Is Nothing Then
            If Not node.Item(itemName) Is Nothing Then
                node.Item(itemName).InnerText = value.ToString
                success = True
            End If
        End If
        Return success
    End Function
#End Region

#Region "GetAttributeValue(node, name, value)"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the value from an attribute at the specified node.
    ''' </summary>
    ''' <param name="node">The XmlNode from which this method will get the value of an attribute.</param>
    ''' <param name="attributeName">Name of the attribute that will be read.</param>
    ''' <param name="value">Attribute value read by this method</param>
    ''' <returns>True if node is found and value is retrieved successfully.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function GetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String, ByRef value As String) As Boolean
        Dim success = False
        If Not node Is Nothing Then
            Dim attribute As XmlAttribute = node.Attributes.ItemOf(attributeName)
            If Not attribute Is Nothing Then
                value = attribute.Value
                success = True
            End If
        End If
        Return success
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the value from an attribute at the specified node.
    ''' </summary>
    ''' <param name="node">The XmlNode from which this method will get the value of an attribute.</param>
    ''' <param name="attributeName">Name of the attribute that will be read.</param>
    ''' <param name="value">Attribute value read by this method</param>
    ''' <returns>True if success.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function GetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String, ByRef value As Integer) As Boolean
        Dim success = False
        If Not node Is Nothing Then
            Dim attribute As XmlAttribute = node.Attributes.ItemOf(attributeName)
            If Not attribute Is Nothing Then
                Dim strValue = attribute.Value
                value = Integer.Parse(strValue)
                success = True
            End If
        End If
        Return success
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the value from an attribute at the specified node.
    ''' </summary>
    ''' <param name="node">The XmlNode from which this method will get the value of an attribute.</param>
    ''' <param name="attributeName">Name of the attribute that will be read.</param>
    ''' <param name="value">Attribute value read by this method</param>
    ''' <returns>True if success.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function GetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String, ByRef value As UInt32) As Boolean
        Dim success = False
        If Not node Is Nothing Then
            Dim attribute As XmlAttribute = node.Attributes.ItemOf(attributeName)
            If Not attribute Is Nothing Then
                Dim strValue = attribute.Value
                value = UInt32.Parse(strValue)
                success = True
            End If
        End If
        Return success
    End Function

    Shared Function GetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String) As String
        Dim value As String = Nothing
        If Not node Is Nothing Then
            Dim attribute As XmlAttribute = node.Attributes.ItemOf(attributeName)
            If Not attribute Is Nothing Then
                value = attribute.Value
            End If
        End If
        Return value
    End Function

#End Region

#Region "SetAttributeValue(node, name, value)"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Sets the value of an attribute for a given XmlNode.
    ''' </summary>
    ''' <param name="node">XmlNode whose attribute will be set.</param>
    ''' <param name="attributeName">Name of the attribute to set.</param>
    ''' <param name="value">Value to be set</param>
    ''' <returns>True if success.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function SetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String, ByVal value As String) As Boolean
        Dim success = False
        If Not node Is Nothing Then
            Dim attrNode As XmlNode = node.Attributes.GetNamedItem(attributeName)
            If Not attrNode Is Nothing Then
                attrNode.Value = value
                success = True
            End If
        End If
        Return success
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Sets the value of an attribute for a given XmlNode.
    ''' </summary>
    ''' <param name="node">XmlNode whose attribute will be set.</param>
    ''' <param name="attributeName">Name of the attribute to set.</param>
    ''' <param name="value">Value to be set</param>
    ''' <returns>True if success.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function SetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String, ByVal value As Integer) As Boolean
        Dim success = False
        If Not node Is Nothing Then
            Dim attrNode As XmlNode = node.Attributes.GetNamedItem(attributeName)
            If Not attrNode Is Nothing Then
                attrNode.Value = value.ToString
                success = True
            End If
        End If
        Return success
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Sets the value of an attribute for a given XmlNode.
    ''' </summary>
    ''' <param name="node">XmlNode whose attribute will be set.</param>
    ''' <param name="attributeName">Name of the attribute to set.</param>
    ''' <param name="value">Value to be set</param>
    ''' <returns>True if success.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function SetAttributeValue(ByRef node As XmlNode, ByVal attributeName As String, ByVal value As UInt32) As Boolean
        Dim success = False
        If Not node Is Nothing Then
            Dim attrNode As XmlNode = node.Attributes.GetNamedItem(attributeName)
            If Not attrNode Is Nothing Then
                attrNode.Value = value.ToString
                success = True
            End If
        End If
        Return success
    End Function
#End Region

#Region "More Attribute Methods"
    Shared Function CopyAttribute(ByRef fromNode As XmlNode, ByRef toNode As XmlNode, ByVal attributeName As String) As Boolean
        Dim success As Boolean = False
        Dim doc As XmlDocument = toNode.OwnerDocument
        Dim val As String = ""
        If GetAttributeValue(fromNode, attributeName, val) Then
            If toNode.Attributes(attributeName) Is Nothing Then CreateAttribute(toNode, attributeName, val)
            success = SetAttributeValue(toNode, attributeName, val)
        End If
        Return success
    End Function

    Shared Function CreateAttribute(ByRef node As XmlNode, ByVal attributeName As String, ByVal value As String) As XmlAttribute
        Dim doc As XmlDocument = node.OwnerDocument
        Dim newNode As XmlNode

        Dim attr As XmlAttribute
        'create new attribute
        attr = doc.CreateAttribute(attributeName)
        attr.Value = value
        'link attribute to node
        node.Attributes.SetNamedItem(attr)
        Return attr
    End Function
#End Region

#Region "DataTable manipulation"

    ''' -----------------------------------------------------------------------------
    ''' <summary>s 
    ''' Converts a list of Xml nodes to a DataTable.
    ''' </summary>
    ''' <param name="nodelist">List of Xml nodes</param>
    ''' <returns>DataTable</returns>
    ''' <remarks>
    ''' This method convert
    ''' </remarks>
    ''' -----------------------------------------------------------------------------
    Shared Function GetDataTable(ByRef nodelist As XmlNodeList) As DataTable
        Dim table As DataTable
        Dim node As XmlNode
        If nodelist Is Nothing Then Return Nothing

        'get parameter names
        node = nodelist.Item(0)
        If node Is Nothing Then Return Nothing

        Dim attrCollection As XmlAttributeCollection = node.Attributes
        If attrCollection Is Nothing Then Return Nothing
        If attrCollection.Count = 0 Then Return Nothing

        'create data table
        table = New DataTable
        For Each attr As XmlAttribute In attrCollection
            table.Columns.Add(attr.Name)
        Next

        'add rows
        Dim row As DataRow
        For Each node In nodelist
            row = table.NewRow()
            For Each attr As XmlAttribute In node.Attributes
                row(attr.Name) = attr.Value
            Next
            table.Rows.Add(row)
        Next

        table.AcceptChanges()
        Return table
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a list of Xml nodes to a DataTable and sets one of the columns as a primary key.
    ''' </summary>
    ''' <param name="nodelist"></param>
    ''' <param name="primaryKeyColumn"></param>
    ''' <param name="autoIncrement"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDataTable(ByVal nodelist As XmlNodeList, ByVal primaryKeyColumn As String, ByVal autoIncrement As Boolean) As DataTable
        Dim table As DataTable = Nothing
        Dim node As XmlNode = Nothing
        If nodelist Is Nothing Then
            Return Nothing
        End If
        node = nodelist.Item(0)
        If node Is Nothing Then
            Return Nothing
        End If
        Dim attrCollection As XmlAttributeCollection = node.Attributes
        If attrCollection Is Nothing Then
            Return Nothing
        End If
        If attrCollection.Count = 0 Then
            Return Nothing
        End If
        table = New DataTable
        Dim primaryKeyFieldFound As Boolean = False
        For Each attr As XmlAttribute In attrCollection
            If attr.Name = primaryKeyColumn Then
                primaryKeyFieldFound = True
            End If
            table.Columns.Add(attr.Name)
        Next
        If Not primaryKeyFieldFound Then
            Throw New Exception("Unable to set primary key in datatable because field '" + primaryKeyColumn + "'was not found.")
        End If
        table.PrimaryKey = New DataColumn() {table.Columns(primaryKeyColumn)}
        If autoIncrement Then
            table.Columns(primaryKeyColumn).AutoIncrement = True
            table.Columns(primaryKeyColumn).AutoIncrementStep = 1
        End If
        Dim row As DataRow = Nothing
        For Each n As XmlNode In nodelist
            row = table.NewRow
            For Each a As XmlAttribute In n.Attributes
                row(a.Name) = a.Value
            Next
            table.Rows.Add(row)
        Next
        table.AcceptChanges()
        Return table
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Updates the child nodes of "parentNode" by using the fields from a datatable.
    ''' </summary>
    ''' <param name="parentNode"></param>
    ''' <param name="table"></param>
    ''' <param name="keyField"></param>
    ''' <remarks>
    ''' The child nodes that will be updated must have attribute fields that correspond to
    ''' the DataTable.  The "keyField" will be used to identify the attribute that serves as 
    ''' an identifier of the rows.  The datatable can have less fields than the nodes so
    ''' you have the chance to update smaller subsets.
    ''' Make sure that you did not call "AcceptChanges" before passing the datatable or this
    ''' function will not find any change.
    ''' </remarks>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateChildNodesWithDataTable(ByVal parentNode As XmlNode, ByVal table As DataTable, ByVal keyField As String)
        If parentNode Is Nothing Then
            Throw New ArgumentNullException("Unable to update child nodes because parentNode is null")
        End If
        If parentNode.HasChildNodes Then
            '
            ' Verify that the fields of first child node match the fields in the data table
            ' note that it's ok if the datatable has fewer fields than the nodes.

            Dim firstNode As XmlNode = parentNode.ChildNodes(0)
            Dim missingFields As String = ""
            Dim columnNames As StringCollection = New StringCollection
            For Each col As DataColumn In table.Columns
                If firstNode.Attributes(col.ColumnName) Is Nothing Then
                    If missingFields.Length = 0 Then
                        missingFields = col.ColumnName
                    Else
                        missingFields += ", " + col.ColumnName
                    End If
                Else
                    columnNames.Add(col.ColumnName)
                End If
            Next
            If missingFields.Length > 0 Then
                Throw New Exception("Unable to update nodes with datatable because the nodes are missing the fields: " + missingFields)
            End If
            Dim currTable As DataTable = table.GetChanges(DataRowState.Deleted)
            If Not (currTable Is Nothing) Then
                Dim nodeToDelete As XmlNode
                Trace.WriteLine("Rows Deleted:")
                For Each row As DataRow In table.Rows
                    Dim keyValue As String = row(keyField).ToString
                    nodeToDelete = SelectNode(parentNode, keyField, keyValue)
                    Trace.WriteLine(keyValue)
                    If Not (nodeToDelete Is Nothing) Then
                        parentNode.RemoveChild(nodeToDelete)
                    End If
                Next
            End If
            currTable = table.GetChanges(DataRowState.Modified)
            If Not (currTable Is Nothing) Then
                Dim nodeToUpdate As XmlNode
                Trace.WriteLine("Rows Changed:")
                For Each row As DataRow In currTable.Rows
                    Dim keyValue As String = row(keyField).ToString
                    Trace.WriteLine(keyValue)
                    nodeToUpdate = SelectNode(parentNode, keyField, keyValue)
                    If nodeToUpdate Is Nothing Then
                        Throw New Exception("Unable to update node with '" + keyField + "=" + keyValue + "' because it doesn't exist")
                    End If
                    Dim valueToSet As String
                    For Each colName As String In columnNames
                        If colName = keyField Then
                            ' continue 
                        End If
                        valueToSet = row(colName).ToString
                        SetAttributeValue(nodeToUpdate, colName, valueToSet)
                    Next
                Next
            End If
            currTable = table.GetChanges(DataRowState.Added)
            If Not (currTable Is Nothing) Then
                Dim newNode As XmlNode
                Dim keyValue As String
                Dim doc As XmlDocument = parentNode.OwnerDocument
                Trace.WriteLine("Rows Added:")
                For Each row As DataRow In currTable.Rows
                    keyValue = row(keyField).ToString
                    Trace.WriteLine(keyValue)
                    If SelectNode(parentNode, keyField, keyValue) Is Nothing Then
                        newNode = doc.CreateElement(firstNode.Name)
                        CopyAttributes(row, newNode)
                        parentNode.AppendChild(newNode)
                    Else
                        System.Windows.Forms.MessageBox.Show("Can not add duplicate nodes. Row with '" + keyField + "=" + keyValue + " was not added.", "Error Updating Nodes from Table")
                    End If
                Next
            End If
            table.AcceptChanges()
        End If
    End Sub

    Public Shared Sub UpdateChildNodesWithDataTable(ByVal parentNode As XmlNode, ByVal table As DataTable)
        Dim primaryKeyColumns As DataColumn() = table.PrimaryKey
        If primaryKeyColumns Is Nothing Then
            Throw New Exception("Can not update Child nodes with Table because the table doesn't have a primary key column")
        Else
            UpdateChildNodesWithDataTable(parentNode, table, primaryKeyColumns(0).ColumnName)
        End If
    End Sub

    Public Shared Sub CopyAttributes(ByVal fromRow As DataRow, ByVal toNode As XmlNode)
        For Each col As DataColumn In fromRow.Table.Columns
            CreateAttribute(toNode, col.ColumnName, fromRow(col.ColumnName).ToString)
        Next
    End Sub

#End Region

#Region "Misc"
    Public Shared Function SelectNode(ByVal parentNode As XmlNode, ByVal attributeName As String, ByVal attributeValue As String) As XmlNode
        Dim node As XmlNode = Nothing
        If parentNode.HasChildNodes Then
            Dim nodeName As String = parentNode.ChildNodes(0).Name
            Dim path As String = nodeName + "[@" + attributeName + "='" + attributeValue + "']"
            node = parentNode.SelectSingleNode(path)
        End If
        Return node
    End Function
#End Region

#Region "Conversion to Array"
    Shared Function GetAttributeArray(ByRef nodeList As XmlNodeList, ByVal attributeName As String) As String()
        Dim arrayOfValues() As String
        If nodeList.Count > 0 Then
            ReDim arrayOfValues(nodeList.Count - 1)
            Dim index As Integer = 0
            For Each node As XmlNode In nodeList
                arrayOfValues(index) = GetAttributeValue(node, attributeName)
                index += 1
            Next
        End If
        Return arrayOfValues
    End Function

    'Gets only the values of the nodes passed in nodelist
    Shared Function GetArray(ByRef nodeList As XmlNodeList) As String()
        Dim arrayOfValues() As String
        If nodeList.Count > 0 Then
            ReDim arrayOfValues(nodeList.Count - 1)
            Dim index As Integer = 0
            For Each node As XmlNode In nodeList
                arrayOfValues(index) = node.InnerText
                index += 1
            Next
        End If
        Return arrayOfValues
    End Function

    'This method gets the name value pair based on the first two attributes of every node
    Shared Function GetNameValuePair(ByRef nodeList As XmlNodeList) As NameValueCollection
        Dim nameVal As New NameValueCollection
        If nodeList Is Nothing Then Return Nothing

        'get parameter names
        Dim node As XmlNode = nodeList.Item(0)
        If node Is Nothing Then Return Nothing

        Dim attrCollection As XmlAttributeCollection = node.Attributes
        If attrCollection Is Nothing Then Return Nothing
        If attrCollection.Count < 2 Then Return Nothing

        Dim attrName1, attrName2 As String
        'read all nodes in nodelist and extract first two attributes
        For Each node In nodeList
            attrName1 = node.Attributes(0).Value
            attrName2 = node.Attributes(1).Value
            nameVal.Add(attrName1, attrName2)
        Next
        Return nameVal
    End Function
#End Region

#Region "Conversions to string"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns contents of an XmlNode in a string.
    ''' </summary>
    ''' <param name="node">The XmlNode whose contents will be read into a string.</param>
    ''' <returns>Xml formatted string with contents of "node" parameter.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function NodeToString(ByRef node As XmlNode) As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("")
        Dim sw As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim writer As XmlTextWriter = New XmlTextWriter(sw)
        writer.Formatting = Formatting.Indented
        If node Is Nothing Then
            writer.WriteStartElement("Node_Empty")
        Else
            writer.WriteStartElement(node.Name)

            ' Write any attributes 
            Dim a As XmlAttribute
            For Each a In node.Attributes
                writer.WriteAttributeString(a.Name, a.Value)
            Next

            ' Write child nodes
            Dim nodes As XmlNodeList = node.SelectNodes("child::*")
            Dim nav As New NodeNavigator
            If Not nodes Is Nothing Then
                For Each n As XmlNode In nodes
                    nav.LoopThroughChildren(writer, n)
                Next
            End If
        End If

        writer.WriteEndElement()
        writer.Close()

        Return sw.ToString()
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convert a XmlNodeList to string
    ''' </summary>
    ''' <param name="nodeList"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' -----------------------------------------------------------------------------
    Public Shared Function NodeListToString(ByVal nodeList As XmlNodeList) As String
        If Not (nodeList Is Nothing) Then
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
            For Each node As XmlNode In nodeList
                If sb.Length = 0 Then
                    sb.Append(NodeToString(node))
                Else
                    sb.Append(vbCrLf + NodeToString(node))
                End If
            Next
            Return sb.ToString
        End If
        Return "nodeList is null"
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Method to convert a XmlDocument to string.
    ''' </summary>
    ''' <param name="xmlDoc">XmlDocument that will be converted to string.</param>
    ''' <returns>A xml formatted string.</returns>
    ''' -----------------------------------------------------------------------------
    Shared Function DocumentToString(ByRef xmlDoc As XmlDocument) As String
        Dim sb As New System.Text.StringBuilder("")
        Dim sw As New System.IO.StringWriter(sb)
        xmlDoc.Save(sw)
        Return sw.ToString()
    End Function

#End Region

#Region "Creation of Multiple child nodes"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string array to XmlNodes and appends all those nodes to a root node.
    ''' </summary>
    ''' <param name="rootNode"></param>
    ''' <param name="names"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' -----------------------------------------------------------------------------
    Shared Sub CreateChildNodes(ByRef rootNode As XmlNode, ByVal names() As String)
        Dim doc As XmlDocument = rootNode.OwnerDocument
        Dim newNode As XmlNode
        For Each name As String In names
            newNode = doc.CreateElement(name)
            rootNode.AppendChild(newNode)
        Next
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="rootNode"></param>
    ''' <param name="names"></param>
    ''' <param name="attributeName"></param>
    ''' <remarks>
    ''' </remarks>
    ''' -----------------------------------------------------------------------------
    Shared Sub CreateChildNodes(ByRef rootNode As XmlNode, ByVal nodeName As String, _
                                ByVal attributeName As String, ByVal attributeValues() As String)
        Dim doc As XmlDocument = rootNode.OwnerDocument
        Dim newNode As XmlNode
        Dim attr As XmlAttribute
        For Each value As String In attributeValues
            newNode = doc.CreateElement(nodeName)
            CreateAttribute(newNode, attributeName, value)
            rootNode.AppendChild(newNode)
        Next
    End Sub
#End Region

#Region "© Copyright 2005, Marc Clifton, All Rights Reserved - XmlDatase methods"
    '(c) 2005, Marc Clifton
    'All Rights Reserved

    'Redistribution and use in source and binary forms, with or without modification,
    'are permitted provided that the following conditions are met:

    'Redistributions of source code must retain the above copyright notice, this list
    'of conditions and the following disclaimer. 

    'Redistributions in binary form must reproduce the above copyright notice, this
    'list of conditions and the following disclaimer in the documentation and/or other
    'materials provided with the distribution. 

    'Neither the name of the Marc Clifton nor the names of its contributors may be
    'used to endorse or promote products derived from this software without specific
    'prior written permission. 

    'THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
    'ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    'WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
    'IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
    'INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
    'BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
    'DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
    'LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
    'OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
    'ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#Region "Disclosure"
    'With Marc Clifton's permission, the methods in this section were copied and 
    'modified to be used in XmlHelper.  The original source code is located at:
    'http://www.codeproject.com/dotnet/XmlDb.asp
#End Region

#Region "Insert"
    ''' <summary>
    ''' Inserts an empty record at the bottom of the hierarchy, creating the
    ''' tree as required.
    ''' </summary>
    ''' <param name="doc">The XmlDocument to which the node will be inserted.</param>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <returns>The XmlNode inserted into the hierarchy.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to insert an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Function Insert(ByVal doc As XmlDocument, ByVal xpath As String) As XmlNode
        VerifyParameters(doc, xpath)
        Dim path2 As String = xpath.Trim("/")  'get rid of slashes in front and back
        Dim segments As String() = path2.Split("/")
        Dim firstNode As XmlNode = doc.LastChild
        Dim nodeIndex As Integer = 0
        If segments.Length > 1 Then
            '''
            ''' Check if we can access the last node.  This comes in handy in cases when the path
            ''' contains attributes.  For example: "University[@Name='UT']/Student[@Id=12222]/Address"
            ''' In example above we would want to get access to last node directly
            '''
            Dim pos As Integer = path2.LastIndexOf("/")
            Dim path3 As String = path2.Substring(0, pos)
            Dim parentNode As XmlNode = doc.LastChild.SelectSingleNode(path3)
            If Not (parentNode Is Nothing) Then
                firstNode = parentNode
                nodeIndex = segments.Length - 1
            End If
        End If
        Dim lastNode As XmlNode = InsertNode(firstNode, segments, nodeIndex)
        Return lastNode
    End Function

    ''' <summary>
    ''' Inserts an record with a multiple fields at the bottom of the hierarchy.
    ''' </summary>
    ''' <param name="doc">The XmlDocument to which the node will be inserted.</param>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <param name="fields">The attribute names that will be created for the node inserted.</param>
    ''' <param name="values">The corresponding value of each field.</param>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to insert an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Sub Insert(ByVal doc As XmlDocument, ByVal xpath As String, ByVal fields As String(), ByVal values As String())
        VerifyParameters(doc, xpath)
        If fields Is Nothing Then
            Throw (New ArgumentNullException("field cannot be null."))
        End If
        If values Is Nothing Then
            Throw (New ArgumentNullException("val cannot be null."))
        End If
        Dim node As XmlNode = Insert(doc, xpath)
        For i As Integer = 0 To fields.Length - 1
            CreateAttribute(node, fields(i), values(i))
        Next
    End Sub

    ''' <summary>
    ''' Inserts a record with a single field at the bottom of the hierarchy.
    ''' </summary>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <param name="field">The field to add to the record.</param>
    ''' <param name="val">The value assigned to the field.</param>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to insert an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Sub Insert(ByVal doc As XmlDocument, ByVal xpath As String, ByVal field As String, ByVal val As String)
        VerifyParameters(doc, xpath)
        If field Is Nothing Then
            Throw (New ArgumentNullException("field cannot be null."))
        End If
        If val Is Nothing Then
            Throw (New ArgumentNullException("val cannot be null."))
        End If
        Dim node As XmlNode = Insert(doc, xpath)
        CreateAttribute(node, field, val)
    End Sub

    ''' <summary>
    ''' Insert a record with multiple fields at the bottom of the hierarchy.
    ''' </summary>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <param name="fields">The array of fields as field/value pairs.</param>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to insert an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Sub Insert(ByVal doc As XmlDocument, ByVal xpath As String, ByVal nameValuePairs As NameValueCollection)
        VerifyParameters(doc, xpath)
        If nameValuePairs Is Nothing Then
            Throw (New ArgumentNullException("fields cannot be null."))
        End If
        Dim node As XmlNode = Insert(doc, xpath)
        Dim iterator As System.Collections.IEnumerator = nameValuePairs.GetEnumerator
        Dim field As String
        Dim val As String
        While iterator.MoveNext
            field = iterator.Current.ToString
            val = nameValuePairs(field)
            CreateAttribute(node, field, val)
        End While
    End Sub

    ''' <summary>
    ''' Inserts a record with multiple fields at bottom of the hierarchy.
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <param name="rowValues">The DataRow values that will be added as attributes.</param>
    ''' <remarks>
    ''' The columns names of the DataRow will become the attribute names and 
    ''' the row values of the DataRow will be the attribute values.
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to insert an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Sub Insert(ByVal doc As XmlDocument, ByVal xpath As String, ByVal rowValues As DataRow)
        VerifyParameters(doc, xpath)
        If rowValues Is Nothing Then
            Throw (New ArgumentNullException("val cannot be null."))
        End If
        Dim node As XmlNode = Insert(doc, xpath)
        For Each col As DataColumn In rowValues.Table.Columns
            CreateAttribute(node, col.ColumnName, rowValues(col.ColumnName).ToString)
        Next
    End Sub

    ''' <summary>
    ''' Inserts a record with multiple fields from a DataTable at bottom of the hierarchy.
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <param name="rowValues">The DataRow values that will be added as attributes.</param>
    Public Shared Sub Insert(ByVal doc As XmlDocument, ByVal xpath As String, ByVal table As DataTable)
        VerifyParameters(doc, xpath)
        If table Is Nothing Then
            Throw (New ArgumentNullException("table cannot be null."))
        End If

        For Each row As DataRow In table.Rows
            Insert(doc, xpath, row)
        Next
    End Sub

    ''' <summary>
    ''' Inserts a record with multiple values at bottom of hierarchy. This is analogous to inserting 
    ''' a column of data.
    ''' </summary>
    ''' <param name="doc"></param>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <param name="field">Name of the attribute to be created at node inserted.</param>
    ''' <param name="values">Values that will be inserted that correspond to the field name.</param>
    ''' <remarks>
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to insert an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Sub Insert(ByVal doc As XmlDocument, ByVal xpath As String, ByVal field As String, ByVal values As String())
        VerifyParameters(doc, xpath)
        Dim node As XmlNode
        For Each val As String In values
            node = Insert(doc, xpath)
            CreateAttribute(node, field, val)
        Next
    End Sub

#End Region

#Region "Update"
    ''' <summary>
    ''' Update a single field in all records in the specified path.
    ''' </summary>
    ''' <param name="doc">The XmlDocument whose node will be udpated.</param>
    ''' <param name="xpath">The xml path.</param>
    ''' <param name="field">The field name to update.</param>
    ''' <param name="val">The new value.</param>
    ''' <returns>The number of records affected.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>
    ''' The "doc" variable must have a root node.  The path should not contain the root node.
    ''' The path can contain only the node names or it can contain attributes in XPath query form.
    ''' For example to update an "Address" node at the bottom, the following is a valid xpath query
    '''     xpath = "University[@Name='UT']/Student[@Id=12222]/Address"
    ''' </remarks>
    Public Shared Function Update(ByVal doc As XmlDocument, ByVal xpath As String, ByVal field As String, ByVal val As String) As Integer
        VerifyParameters(doc, xpath)
        If field Is Nothing Then
            Throw (New ArgumentNullException("field cannot be null."))
        End If
        If val Is Nothing Then
            Throw (New ArgumentNullException("val cannot be null."))
        End If
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim nodeList As XmlNodeList = doc.LastChild.SelectNodes(xpath)
        For Each node As XmlNode In nodeList
            If Not SetAttributeValue(node, field, val) Then
                sb.Append(field + " is not an attribute of: " + NodeToString(node) + vbCrLf)
            End If
        Next
        If sb.Length > 0 Then
            Throw New Exception("Failed to add nodes because:" + vbCrLf + sb.ToString)
        End If
        Return nodeList.Count
    End Function

#End Region

#Region "Delete"
    ''' <summary>
    ''' Deletes all records of the specified path.
    ''' </summary>
    ''' <param name="xpath">The xml XPath query to get to the bottom node.</param>
    ''' <returns>The number of records deleted.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>Additional exceptions may be thrown by the XmlDocument class.</remarks>
    Public Shared Function Delete(ByVal doc As XmlDocument, ByVal xpath As String) As Integer
        VerifyParameters(doc, xpath)
        Dim nodeList As XmlNodeList = doc.LastChild.SelectNodes(xpath)
        For Each node As XmlNode In nodeList
            node.ParentNode.RemoveChild(node)
        Next
        Return nodeList.Count
    End Function

    ''' <summary>
    ''' Deletes a field from all records on the specified path.
    ''' </summary>
    ''' <param name="path">The xml path.</param>
    ''' <param name="field">The field to delete.</param>
    ''' <returns>The number of records affected.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>Additional exceptions may be thrown by the XmlDocument class.</remarks>
    Public Shared Function Delete(ByVal doc As XmlDocument, ByVal xpath As String, ByVal field As String) As Integer
        VerifyParameters(doc, xpath)
        If field Is Nothing Then
            Throw (New ArgumentNullException("field cannot be null."))
        End If
        Dim nodeList As XmlNodeList = doc.SelectNodes(xpath)
        For Each node As XmlNode In nodeList
            Dim attrib As XmlAttribute = node.Attributes(field)
            node.Attributes.Remove(attrib)
        Next
        Return nodeList.Count
    End Function
#End Region

#Region "Query"
    ''' <summary>
    ''' Return a single string representing the value of the specified field
    ''' for the first record encountered.
    ''' </summary>
    ''' <param name="xpath">The xml path.</param>
    ''' <param name="field">The desired field.</param>
    ''' <returns>A string with the field's value or null.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>Additional exceptions may be thrown by the XmlDocument class.</remarks>
    Public Shared Function QueryScalar(ByVal doc As XmlDocument, ByVal xpath As String, ByVal field As String) As String
        VerifyParameters(doc, xpath)
        If field Is Nothing Then
            Throw (New ArgumentNullException("field cannot be null."))
        End If
        Dim ret As String = Nothing
        Dim node As XmlNode = doc.LastChild.SelectSingleNode(xpath)
        If Not (node Is Nothing) Then
            ret = GetAttributeValue(node, field)
        End If
        Return ret
    End Function

    ''' <summary>
    ''' Returns a DataTable for all rows on the path.
    ''' </summary>
    ''' <param name="xpath">The xml path.</param>
    ''' <returns>The DataTable with the returned rows.
    ''' The row count will be 0 if no rows returned.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>Additional exceptions may be thrown by the XmlDocument class.</remarks>
    Public Shared Function Query(ByVal doc As XmlDocument, ByVal xpath As String) As DataTable
        VerifyParameters(doc, xpath)
        Try
            Dim dt As DataTable = New DataTable
            Dim nodeList As XmlNodeList = doc.LastChild.SelectNodes(xpath)
            If Not (nodeList.Count = 0) Then
                CreateColumns(dt, nodeList(0))
            End If
            For Each node As XmlNode In nodeList
                Dim dr As DataRow = dt.NewRow
                For Each attr As XmlAttribute In node.Attributes
                    dr(attr.Name) = attr.Value
                Next
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function

    ''' <summary>
    ''' Returns an array of values for the specified field for all rows on
    ''' the path.
    ''' </summary>
    ''' <param name="xpath">The xml path.</param>
    ''' <param name="field">The desired field.</param>
    ''' <returns>The array of string values for each row qualified by the path.
    ''' A null is returned if the query results in 0 rows.</returns>
    ''' <exception cref="System.ArgumentNullException">Thrown when an argument is null.</exception>
    ''' <remarks>Additional exceptions may be thrown by the XmlDocument class.</remarks>
    Public Shared Function QueryField(ByVal doc As XmlDocument, ByVal xpath As String, ByVal field As String) As String()
        Try
            VerifyParameters(doc, xpath)
            If field Is Nothing Then
                Throw (New ArgumentNullException("field cannot be null."))
            End If
            Dim nodeList As XmlNodeList = doc.LastChild.SelectNodes(xpath)
            Dim s As String() = Nothing
            If Not (nodeList.Count = 0) Then
                s = New String(nodeList.Count - 1) {}
                Dim i As Integer = 0
                For Each node As XmlNode In nodeList
                    s(i) = node.Attributes(field).Value
                    i += 1
                Next
            End If
            Return s
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Function
#End Region

#End Region

#Region "NodeNavigator Class"
    Private Class NodeNavigator
        ' Recursively loop over a node subtree
        Friend Sub LoopThroughChildren(ByVal writer As XmlTextWriter, ByVal rootNode As XmlNode)
            ' Write the start tag
            If rootNode.NodeType = XmlNodeType.Element Then
                writer.WriteStartElement(rootNode.Name)

                ' Write any attributes 
                Dim a As XmlAttribute
                For Each a In rootNode.Attributes
                    writer.WriteAttributeString(a.Name, a.Value)
                Next

                ' Write any child nodes
                Dim n As XmlNode
                For Each n In rootNode.ChildNodes
                    LoopThroughChildren(writer, n)
                Next

                ' Write the end tag
                writer.WriteEndElement()
            Else
                ' Write any text
                If rootNode.NodeType = XmlNodeType.Text Then
                    writer.WriteString(rootNode.Value)
                End If
            End If
        End Sub
    End Class
#End Region

#Region "Helpers"
    ''' <summary>
    ''' Inserts a node at the specified segment if it doesn't exist, otherwise
    ''' traverses the node.
    ''' </summary>
    ''' <param name="node">The current node.</param>
    ''' <param name="segments">The path segment list.</param>
    ''' <param name="idx">The current segment.</param>
    ''' <returns></returns>
    Protected Shared Function InsertNode(ByVal node As XmlNode, ByVal segments As String(), ByVal idx As Integer) As XmlNode
        Dim newNode As XmlNode = Nothing
        If idx = segments.Length Then
            Return node
        End If
        If idx + 1 < segments.Length Then
            For Each child As XmlNode In node.ChildNodes
                If child.Name = segments(idx) Then
                    newNode = InsertNode(child, segments, idx + 1)
                    Return newNode
                End If
            Next
        End If
        Dim doc As XmlDocument = node.OwnerDocument
        newNode = doc.CreateElement(segments(idx))
        node.AppendChild(newNode)
        Dim nextNode As XmlNode = InsertNode(newNode, segments, idx + 1)
        Return nextNode
    End Function

    ''' <summary>
    ''' Creates columns given an XmlNode.
    ''' </summary>
    ''' <param name="dt">The target DataTable.</param>
    ''' <param name="node">The source XmlNode.</param>
    Protected Shared Sub CreateColumns(ByVal dt As DataTable, ByVal node As XmlNode)
        For Each attr As XmlAttribute In node.Attributes
            dt.Columns.Add(New DataColumn(attr.Name))
        Next
    End Sub

    Protected Shared Sub VerifyParameters(ByVal doc As XmlDocument, ByVal xpath As String)
        Try
            If doc Is Nothing Then
                Throw New Exception("doc cannot be null.")
            End If
            If doc.LastChild.GetType Is GetType(System.Xml.XmlDeclaration) Then
                Throw New Exception("XmlDocument requires at least the a root node")
            End If
            If xpath Is Nothing Then
                Throw (New ArgumentNullException("path cannot be null."))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
#End Region
End Class
