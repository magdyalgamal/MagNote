Imports System.Threading

Public Class GlobalHotkeyListener
#Region "Comments"
    '\\ --[HotkeyModifierDefinition]--------------------------------
    '\\ Class that creates a native window to watch for the WM_HOTKEY
    '\\ window message.
    '\\ (c) 2003 Merrion Computing Ltd
    '\\ http://www.merrioncomputing.com
    '\\ -------------------------------------------------------------

#End Region
    Inherits NativeWindow

#Region "Private member variables"
    Private windowHandle As Integer
    Private mwh As ManualResetEvent
#End Region

    Public Sub New(ByVal Id As Int32,
                   ByVal fsModifiers As Int32,
                   ByVal vkey As Int32,
                    ByRef wh As ManualResetEvent)

        '\\ Get a local copy of the wait handle

        mwh = wh
        Dim cp As CreateParams = New CreateParams()

        ' Fill in the CreateParams details.
        cp.Caption = ""
        cp.ClassName = "STATIC"

        ' Set the position on the form
        cp.X = 0
        cp.Y = 0
        cp.Height = 0
        cp.Width = 0

        '\\ Set the style and extended style flags
        cp.Style = WindowStyleBits.WS_MINIMIZE
        cp.ExStyle = WindowStyleExtendedBits.WS_EX_NOACTIVATE

        ' Create the actual window
        Me.CreateHandle(cp)

        Try
            If Not RegisterHotkey(MyBase.Handle, Id, fsModifiers, vkey) Then
                'Throw New Win32Exception(Marshal.GetLastWin32Error(), "HotKey failed")
                Throw New System.ComponentModel.Win32Exception()
            End If
        Catch e As Exception
            System.Diagnostics.Debug.WriteLine(e.ToString)
        End Try
    End Sub

    ' Listen to when the handle changes to keep the variable in sync

    <System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")>
    Protected Overrides Sub OnHandleChange()
        windowHandle = Me.Handle.ToInt32()
    End Sub

    <System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")>
    Protected Overrides Sub WndProc(ByRef m As Message)
        ' Listen for messages that are sent to the button window. Some messages are sent
        ' to the parent window instead of the button's window.

        Select Case (m.Msg)
            Case WM_HOTKEY
                ' Respond to the hotkey message (asynchronously??)
                If Not mwh Is Nothing Then
                    mwh.Set()
                End If
        End Select

        MyBase.WndProc(m)
    End Sub

End Class
