Imports System.Runtime.InteropServices
Public Class ClearMemory
    Implements IDisposable
    <DllImport("KERNEL32.DLL", EntryPoint:="SetProcessWorkingSetSize", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Friend Shared Function SetProcessWorkingSetSize32Bit(ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Integer, ByVal dwMaximumWorkingSetSize As Integer) As Boolean
    End Function

    Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
    <DllImport("KERNEL64.DLL", EntryPoint:="SetProcessWorkingSetSize", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Friend Shared Function SetProcessWorkingSetSize64Bit(ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Long, ByVal dwMaximumWorkingSetSize As Long) As Boolean
    End Function
    Public Sub FlushMem()
        Try
            System.GC.Collect()
            System.GC.WaitForPendingFinalizers()
            System.GC.Collect()
            If Environment.OSVersion.Platform = PlatformID.Win32NT Then
                SetProcessWorkingSetSize32Bit(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1)
                Dim BitProcess
                If Environment.Is64BitProcess Then '
                    BitProcess = "64-bit process"
                Else '
                    BitProcess = "32-bit process"
                End If '
                SetProcessWorkingSetSize32Bit(Process.GetCurrentProcess().Handle, -1, -1)
                Dim PN = Application.ProductName
                Dim myProcesses As Process() = Process.GetProcessesByName(PN)
                Dim myProcess As Process
                For Each myProcess In myProcesses
                    SetProcessWorkingSetSize32Bit(myProcess.Handle, -1, -1)
                Next myProcess
            End If
        Catch ex As Exception
        End Try
    End Sub

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
