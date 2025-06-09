Imports System.Runtime.InteropServices

Public Class BringToFront_Clss : Implements IDisposable
    ' Import the required Windows API functions
    'Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    'End Function
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long

    <DllImport("user32.dll")>
    Private Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    End Function

    ' Constants for keyboard input
    Private Const WM_KEYDOWN As UInteger = &H100
    Private Const WM_KEYUP As UInteger = &H101

    Private Const WM_LBUTTONDOWN As UInteger = &H201
    Private Const WM_LBUTTONUP As UInteger = &H202

    Public Sub MoveMouseActivity(sender As Object, e As EventArgs, ProgramToActivate As String)
        ' Find the window handle of Notepad (change title accordingly)
        'Dim hWnd As IntPtr = FindWindow(Nothing, "Untitled - Notepad")
        Dim hWnd As IntPtr = FindWindow(ProgramToActivate, Nothing)

        If hWnd <> IntPtr.Zero Then
            ' Send the letter "A" (ASCII 65)
            PostMessage(hWnd, WM_KEYDOWN, CType(Keys.A, IntPtr), IntPtr.Zero)
            PostMessage(hWnd, WM_KEYUP, CType(Keys.A, IntPtr), IntPtr.Zero)


            Dim x As Integer = 100
            Dim y As Integer = 50
            Dim lParam As IntPtr = New IntPtr((y << 16) Or (x And &HFFFF))

            ' Send mouse down and up messages
            PostMessage(hWnd, WM_LBUTTONDOWN, IntPtr.Zero, lParam)
            PostMessage(hWnd, WM_LBUTTONUP, IntPtr.Zero, lParam)


            'SetForegroundWindow(hWnd)
            'AppActivate("notepad")
        Else
            MessageBox.Show("Target window not found!")
        End If
    End Sub
    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
    End Function

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

End Class
