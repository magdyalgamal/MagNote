Public Class WordAbbreviation_Clss : Implements IDisposable

    Private WordMap As New Dictionary(Of String, String)
    Public AbbreviatedWord, OriginalWord As String

    Public Sub New(ByVal WordToAbbreviate As String,
                                ByVal Abbreviate As Boolean,
                                Optional maxLength As Integer = 6)
        ' Example usage
        If Abbreviate Then
            AbbreviatedWord = AbbreviateWord(WordToAbbreviate, maxLength)
        Else
            OriginalWord = RetrieveOriginalWord(WordToAbbreviate)
        End If
        ' Show abbreviated word
        'MessageBox.Show("Abbreviated: " & abbreviatedWord)
        '' Retrieve and show the original word
        'Dim originalWord As String = RetrieveOriginalWord(abbreviatedWord)
        'MessageBox.Show("Original: " & originalWord)
    End Sub

    Public Function AbbreviateWord(ByVal WordToAbbreviate As String, ByVal maxLength As Integer) As String
        ' If the word is already short enough, return it as is
        If WordToAbbreviate.Length <= maxLength Then
            Return WordToAbbreviate
        End If

        ' Determine how many characters to keep at the start and end
        Dim keepLength As Integer = (maxLength - 3) \ 2 ' \ 2 divides and floors the result

        ' Create the abbreviated word
        Dim abbreviated As String = WordToAbbreviate.Substring(0, keepLength) & "..." & WordToAbbreviate.Substring(WordToAbbreviate.Length - keepLength)

        ' Store the mapping of abbreviated word to original word
        If Not wordMap.ContainsKey(abbreviated) Then
            wordMap.Add(abbreviated, WordToAbbreviate)
        End If

        Return abbreviated
    End Function

    Public Function RetrieveOriginalWord(abbreviatedWord As String) As String
        ' Check if the abbreviated word exists in the dictionary
        If wordMap.ContainsKey(abbreviatedWord) Then
            Return wordMap(abbreviatedWord)
        Else
            Return "Original word not found."
        End If
    End Function

#Region "IDisposable"
    Private disposedValue As Boolean
    Private components As System.ComponentModel.IContainer

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
