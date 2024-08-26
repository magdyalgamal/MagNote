Public Class LanguageEntry
    Implements IDisposable
    Private m_Name As String
    Private m_Language As InputLanguage
    Public Sub New(ByVal Name As String, ByVal Language As InputLanguage)
        Try
            m_Name = Name
            m_Language = Language
        Finally
            Finalize()
        End Try
    End Sub
    Public ReadOnly Property Name() As String
    Get
      Return m_Name
    End Get
  End Property
  Public ReadOnly Property Language() As InputLanguage
    Get
      Return m_Language
    End Get
  End Property
  Public ReadOnly Property CurrentLanguage() As InputLanguage
    Get
      Return InputLanguage.CurrentInputLanguage
    End Get
  End Property


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

