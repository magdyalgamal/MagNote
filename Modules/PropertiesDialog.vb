Imports System.Runtime.InteropServices

Module PropertiesDialog

    Private Structure SHELLEXECUTEINFOW
        Public cbSize As UInteger
        Public fMask As UInteger
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.LPWStr)> Public lpVerb As String
        <MarshalAs(UnmanagedType.LPWStr)> Public lpFile As String
        <MarshalAs(UnmanagedType.LPWStr)> Public lpParameters As String
        <MarshalAs(UnmanagedType.LPWStr)> Public lpDirectory As String
        Public nShow As Integer
        Public hInstApp As IntPtr
        Public lpIDList As IntPtr
        <MarshalAs(UnmanagedType.LPWStr)> Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As UInteger
        Public Union1 As Anonymous_98a511c6_8aeb_4e58_afe9_4f14a656c4cc
        Public hProcess As IntPtr
    End Structure

    <StructLayout(LayoutKind.Explicit)>
    Private Structure Anonymous_98a511c6_8aeb_4e58_afe9_4f14a656c4cc
        <FieldOffsetAttribute(0)>
        Public hIcon As IntPtr
        <FieldOffsetAttribute(0)>
        Public hMonitor As IntPtr
    End Structure

    <DllImport("shell32.dll", EntryPoint:="ShellExecuteExW", CallingConvention:=CallingConvention.StdCall)>
    Private Function ShellExecuteExW(ByRef lpExecInfo As SHELLEXECUTEINFOW) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Private Const SEE_MASK_INVOKEIDLIST As Integer = &HC
    Private Const SW_SHOW As Integer = &H5

    Sub ShowProperties(ByVal path As String)
        Dim sei As New SHELLEXECUTEINFOW
        sei.cbSize = CUInt(Marshal.SizeOf(sei))
        sei.fMask = SEE_MASK_INVOKEIDLIST
        sei.hwnd = IntPtr.Zero
        sei.lpVerb = "properties"
        sei.lpFile = path '"C:\TestFolder\MyFile.txt" 'the directory or file path to show the property window of
        sei.lpParameters = ""
        sei.lpDirectory = Nothing
        sei.nShow = SW_SHOW
        sei.hInstApp = IntPtr.Zero
        ShellExecuteExW(sei)
    End Sub

End Module