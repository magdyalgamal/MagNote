Imports System.Xml.Linq
Imports System.IO
Imports System.Xml

Public Class XMLEditor
    Implements IDisposable
    Public ReadOnly filePath As String
    Public doc As XDocument
    Dim CalledForm As Form
    Public Sub New(xmlFilePath As String, RootElement As String, Optional ByVal ShowSuccessMsg As Boolean = True, Optional Form As Form = Nothing)
        Try
            If Form IsNot Nothing Then
                CalledForm = Form
            End If
            doc = New XDocument(New XElement(RootElement))
            If Not xmlFilePath.Contains(MagNoteFolderPath) Then
                filePath = MagNoteFolderPath & "\" & xmlFilePath
            Else
                filePath = xmlFilePath
            End If
            If Not IsNothing(xmlFilePath) Then
                CreateXmlIfNotExists(RootElement, ShowSuccessMsg)
            Else
                'filePath &= "GreenAPI.xml"
                filePath &= RootElement & ".xml"
            End If
            doc = XDocument.Load(filePath)

        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False,,,,,,,, CalledForm)
        End Try
    End Sub

    ' Create base XML if not present
    Private Sub CreateXmlIfNotExists(RootElement As String, Optional ByVal ShowSuccessMsg As Boolean = True)
        If Not File.Exists(filePath) Then
            'Dim doc As New XDocument(New XElement("GreenAPI_Entries"))
            'Dim doc As New XDocument(New XElement(RootElement))
            'doc.Save(filePath)
            SaveDoc(filePath)
            If Not ShowSuccessMsg Then Exit Sub
            If MagNote_Form.Language_Btn.Text = "E" Then
                Msg = "تم إنشاء الملف بنجاح"
            Else
                Msg = "File Created Successfully"
            End If
            ShowMsg(Msg & vbNewLine & filePath, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, ShowSuccessMsg,,,,,,, CalledForm)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ParentElement"></param>
    ''' <param name="ChildElementKeyFields"></param>
    ''' <param name="ChildElementItems"></param>
    ''' <param name="AskQuestion"></param>
    ''' <param name="SubElmnt"></param>
    ''' <param name="Elmnt"></param>
    ''' <param name="IgnoreSaveDoc"></param>
    ''' <param name="UpdateDoc">if UpdateDoc then dont delete the element and got to update it</param>
    Public Sub Add(ByVal ParentElement As String,
                               ByVal ChildElementKeyFields As Dictionary(Of String, String),
                               ByVal ChildElementItems As Dictionary(Of String, String),
                               Optional ByVal AskQuestion As Boolean = True,
                               Optional ByVal SubElmnt As String = "SubElementName",
                               Optional ByVal Elmnt As String = "ElementName",
                               Optional ByVal IgnoreSaveDoc As Boolean = False,
                               Optional ByVal UpdateDoc As Boolean = False)
        Try
            Dim UpdateElement As Boolean
            Dim ChildElement = doc.Root.Elements(ParentElement).FirstOrDefault(
            Function(e)
                Return ChildElementKeyFields.All(Function(c) e.Element(c.Key)?.Value = c.Value)
            End Function)
            If ChildElement IsNot Nothing Then
                If AskQuestion Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "هذا العنصر موجود بالفعل... هل تريد تحديث بياناته؟"
                    Else
                        Msg = "This Element Already Exist... Do You Want To Update It's Data?"
                    End If
                End If
                UpdateElement = True
                If AskQuestion And
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, AskQuestion,,,,,, AskQuestion, CalledForm) = DialogResult.No Then
                    Exit Sub
                ElseIf UpdateDoc Then
                    Update(ParentElement, ChildElementKeyFields, ChildElementItems, AskQuestion, IgnoreSaveDoc)
                    Exit Sub
                Else
                    ChildElement.Remove()
                End If
            End If
            Dim newElement As New XElement(ParentElement)

            For Each kvp In ChildElementItems
                If kvp.Value.TrimStart().StartsWith("<" & SubElmnt & ">") Then
                    Dim container As New XElement(kvp.Key)
                    Dim tempXml As XElement = XElement.Parse("<" & Elmnt & ">" & kvp.Value & "</" & Elmnt & ">")
                    newElement.Add(tempXml)
                Else
                    newElement.Add(New XElement(kvp.Key, kvp.Value))
                End If
            Next

            doc.Root.Add(newElement)
            If Not IgnoreSaveDoc Then
                If Not SaveDoc(filePath, AskQuestion) Then
                    Exit Sub
                End If
            End If
            If AskQuestion Then
                If MagNote_Form.Language_Btn.Text = "E" Then
                    If UpdateElement Then
                        Msg = "تم تحديث العنصر بنجاح"
                    Else
                        Msg = "تم إنشاء العنصر بنجاح"
                    End If
                Else
                    If UpdateElement Then
                        Msg = "Element Updated Successfully"
                    Else
                        Msg = "Element Created Successfully"
                    End If
                End If
                ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False, AskQuestion,,,,,,, CalledForm)
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
        End Try
    End Sub
    Public Function RenmeElement(ByVal OldValue As String, ByVal NewValue As String, ByVal Condition As Dictionary(Of String, String)) As Boolean
        Try
            ' Loop through all elements under <Category_Entries>
            Dim entries = doc.Root.Elements().ToList()
            Dim ConditionElementName, ConditionElementvalue
            For Each kvp In Condition
                ConditionElementName = kvp.Key
                ConditionElementvalue = kvp.Value
            Next

            For Each entry As XElement In entries
                If entry.Name.LocalName = OldValue Then
                    Dim nameElement As XElement = entry.Element(ConditionElementName.ToString)
                    If nameElement IsNot Nothing AndAlso nameElement.Value = ConditionElementvalue Then
                        Dim newElement As New XElement(NewValue, entry.Elements())
                        entry.ReplaceWith(newElement)
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
        End Try
    End Function
    Public Function CleareDefaultGrenAPI(ByVal ParentElement As String,
                               ByVal ChildElement As Dictionary(Of String, String)) As Boolean
        Try
            'doc = XDocument.Load(filePath)
            For Each Elmnt In doc.Descendants(ParentElement)
                For Each Element In ChildElement
                    Dim Default_Green_API = Elmnt.Element(Element.Key)
                    If Default_Green_API IsNot Nothing Then
                        Default_Green_API.Value = Element.Value
                    End If
                Next
            Next
            'doc.Save(filePath)
            SaveDoc(filePath)
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
        End Try
    End Function
    Public Function Update(ByVal ParentElement As String,
                               ByVal ChildElementKeyFields As Dictionary(Of String, String),
                               ByVal ChildElementItems As Dictionary(Of String, String),
                               Optional ByVal AskQuestion As Boolean = True,
                               Optional ByVal IgnoreSaveDoc As Boolean = False)
        Try
            'doc = XDocument.Load(filePath)
            Dim ChildElement = doc.Root.Elements(ParentElement).FirstOrDefault(
            Function(e)
                Return ChildElementKeyFields.All(Function(c) e.Element(c.Key)?.Value = c.Value)
            End Function)
            If ChildElement IsNot Nothing Then
                ' Add each sub-element dynamically
                For Each CEI In ChildElementItems
                    Dim Elmnt As XElement = ChildElement.Element(CEI.Key)
                    If IsNothing(Elmnt) Then
                        ChildElement.Add(New XElement(CEI.Key, CEI.Value))
                    Else
                        ChildElement.Element(CEI.Key).Value = CEI.Value
                    End If
                Next
                If Not IgnoreSaveDoc Then
                    SaveDoc(filePath)
                End If

                '' XmlWriterSettings to preserve newlines
                'Dim settings As New XmlWriterSettings()
                'settings.Indent = True
                'settings.NewLineHandling = NewLineHandling.Entitize  ' Converts newlines into &#xA; to be visible in raw XML
                '' Write to file
                'Using writer As XmlWriter = XmlWriter.Create(filePath, settings)
                '    doc.Save(writer)
                'End Using


                If AskQuestion Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "تم تحديث العنصر بنجاح"
                    Else
                        Msg = "Element Updated Successfully"
                    End If
                    ShowMsg(Msg & vbNewLine, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
        End Try
    End Function
    Public Function SaveDoc(filePath As String, Optional ByVal ShowSuccessMsg As Boolean = True) As Boolean
        Try
            ' XmlWriterSettings to preserve newlines
            Dim settings As New XmlWriterSettings()
            settings.Indent = True
            settings.NewLineHandling = NewLineHandling.Entitize  ' Converts newlines into &#xA; to be visible in raw XML
            ' Write to file
            Using writer As XmlWriter = XmlWriter.Create(filePath, settings)
                doc.Save(writer)
            End Using
            Return True
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0, ShowSuccessMsg,,,,,,, CalledForm)
        End Try
    End Function
    Public Function AddComment(ByVal ParentElement As String,
                               ByVal ChildElementKeyFields As Dictionary(Of String, String),
                               ByVal Comment As String,
                               Optional FileComment As Boolean = False)
        Dim xmlcomment As New XComment(Comment)
        ' Insert comment before the first <GreenAPI> element
        'doc = XDocument.Load(filePath)
        If FileComment Then
            doc.Nodes().First().AddBeforeSelf(xmlcomment)
            'doc.Save(filePath)
            SaveDoc(filePath)
            Exit Function
        End If

        'Dim firstGreenAPI = doc.Root.Element("GreenAPI")
        If Not IsNothing(ChildElementKeyFields) Then
            Dim ChildElement = doc.Root.Elements(ParentElement).FirstOrDefault(
            Function(e)
                Return ChildElementKeyFields.All(Function(c) e.Element(c.Key)?.Value = c.Value)
            End Function)
            If ChildElement IsNot Nothing Then
                Dim prntElmnt As XElement = ChildElement.Parent
                If prntElmnt IsNot Nothing Then
                    prntElmnt.AddBeforeSelf(xmlcomment)
                End If
            End If
        End If
        If IsNothing(ChildElementKeyFields) Then
            Dim Element As XElement = doc.Descendants(ParentElement).FirstOrDefault()
            If Element IsNot Nothing Then
                Element.AddBeforeSelf(xmlcomment)
            End If
        End If
        ' Save changes
        'doc.Save(filePath)
        SaveDoc(filePath)
    End Function
    ' Delete entry by ID_Instance
    ''' <summary>
    ''' RemoveElementContents means that you want to remove the elemnt from <Elemente>ElementName</Element> from entier file
    ''' </summary>
    ''' <param name="ParentElement"></param>
    ''' <param name="ChildElementKeyFields"></param>
    ''' <param name="RemoveElementContents"></param>
    ''' <param name="AskQuestion"></param>
    ''' <returns></returns>
    Public Function Delete(ByVal ParentElement As String,
                               ByVal ChildElementKeyFields As Dictionary(Of String, String),
                               Optional ByVal RemoveElementContents As Boolean = False,
                               Optional ByVal AskQuestion As Boolean = True)
        Try
            'doc = XDocument.Load(filePath)
            If RemoveElementContents Then
                Dim ElementEntries = doc.Descendants(ParentElement)
                If ElementEntries IsNot Nothing Then
                    If ChildElementKeyFields IsNot Nothing Then
                        For Each Element In ChildElementKeyFields
                            Dim ElementEntriesToRemove = ElementEntries.Elements(Element.Key)
                            If ElementEntriesToRemove IsNot Nothing Then
                                If AskQuestion Then
                                    If MagNote_Form.Language_Btn.Text = "E" Then
                                        Msg = "سيتم إلغاء هذا العنصر... هل انت متأكد؟"
                                    Else
                                        Msg = "Thsi Element Will Be Deleted... Are You Sure?"
                                    End If
                                    If ShowMsg(Msg & vbNewLine & Element.Value, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm) = DialogResult.No Then
                                        Exit Function
                                    End If
                                End If
                                ElementEntriesToRemove.Remove()
                            End If
                        Next
                        'doc.Save(filePath)
                    Else
                        If AskQuestion Then
                            If MagNote_Form.Language_Btn.Text = "E" Then
                                Msg = "سيتم إلغاء هذا المجموعة... هل انت متأكد؟"
                            Else
                                Msg = "Thsi Element Will Be Deleted... Are You Sure?"
                            End If
                            If ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm) = DialogResult.No Then
                                Exit Function
                            End If
                        End If
                        ElementEntries.Remove
                    End If
                    SaveDoc(filePath)
                End If
                Exit Function
            End If
            Dim ChildElement = doc.Root.Elements(ParentElement).FirstOrDefault(
            Function(e)
                Return ChildElementKeyFields.All(Function(c) e.Element(c.Key)?.Value = c.Value)
            End Function)
            If ChildElement IsNot Nothing Then
                If AskQuestion Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "سيتم إلغاء هذا العنصر... هل انت متأكد؟"
                    Else
                        Msg = "Thsi Element Will Be Deleted... Are You Sure?"
                    End If
                    If ShowMsg(Msg & ChildElement.ToString, "InfoSysMe (MagNote)", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm) = DialogResult.No Then
                        Exit Function
                    End If
                End If
                ChildElement.Remove()
                'doc.Save(filePath)
                SaveDoc(filePath)
                If AskQuestion Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "تم إلغاء العنصر بنجاح"
                    Else
                        Msg = "Element Deleted Successfully"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
                End If
            Else
                If AskQuestion Then
                    If MagNote_Form.Language_Btn.Text = "E" Then
                        Msg = "لم يتم العثور على هذا العنصر"
                    Else
                        Msg = "This Element Not Found"
                    End If
                    ShowMsg(Msg, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
                End If
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
        End Try
    End Function
    Public Function Element_Exist(ByVal ParentElement As String,
                                                       ByVal ChildElementKeyFields As Dictionary(Of String, String),
                                                       Optional ByVal ReturnElement As Boolean = False,
                                                       Optional ByVal RootElements As Boolean = False)
        Try
            'doc = XDocument.Load(filePath)
            If RootElements And Not ReturnElement Then
                If Not IsNothing(ChildElementKeyFields) Then
                    For Each Elmnt In doc.Descendants(ParentElement)
                        For Each Element In ChildElementKeyFields
                            Dim xx = doc.Root.Elements(Element.Value)
                            If xx.Elements.Count > 0 Then
                                Return doc.Root.Elements(Element.Value)
                            End If
                        Next
                    Next
                    Return Nothing
                Else
                    Return doc.Root.Elements()
                End If
            End If
            If ReturnElement And RootElements Then
                Dim element
                Dim expectedValue

                If Not IsNothing(ChildElementKeyFields) Then
                    For Each Elmnt In doc.Descendants(ParentElement).Elements
                        Dim SubElement = doc.Root.Elements(Elmnt.Name).FirstOrDefault(
            Function(e)
                Return ChildElementKeyFields.All(Function(c) e.Element(c.Key)?.Value = c.Value)
            End Function)
                        If SubElement IsNot Nothing Then
                            Return Elmnt
                        End If
                    Next
                    Return Nothing
                End If
            End If
            Dim ChildElement = doc.Root.Elements(ParentElement).FirstOrDefault(
            Function(e)
                Return ChildElementKeyFields.All(Function(c) e.Element(c.Key)?.Value = c.Value)
            End Function)
            If ChildElement IsNot Nothing Then
                If ReturnElement Then
                    Return ChildElement
                Else
                    Return True
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (MagNote)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, 0,,,,,,,, CalledForm)
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
