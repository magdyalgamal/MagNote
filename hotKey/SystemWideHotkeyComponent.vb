Imports System.Threading

Public Class SystemWideHotkeyComponent
#Region "Comments"
    '\\ --[SystemWideHotkeyComponent]-----------------------
    '\\ 
    '\\ (c) Merrion Computing Ltd 
    '\\     http://www.merrioncomputing.com
    '\\ ----------------------------------------------------
#End Region
    Inherits System.ComponentModel.Component

#Region "Public events"

    Public Event HotkeyPressed As EventHandler
    Protected Sub OnHotkeyPressed(ByVal e As HotkeyEventArgs)
        RaiseEvent HotkeyPressed(Me, e)
    End Sub

#End Region

#Region "Private member variables"

    Private Modifier As New HotkeyModifierDefinition()
    Private mHotkey As Keys
    Private mhAtom As Int32

    Private Shared mhHotkeyNotification As RegisteredWaitHandle = Nothing
    Private ThreadTimeout As Integer = 50
    Private mwndListener As GlobalHotkeyListener

#End Region

#Region " Component Designer generated code "

    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'Component overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

#Region "Private functions"

    Private Sub StartHotkeyWatch()
        '\\ Only need to register a hotkey in runtime mode...
        If MyBase.DesignMode = False Then
            If Modifier Is Nothing Then
                Modifier = New HotkeyModifierDefinition()
            End If
            '\\ If the atom is already set, free it...
            If mhAtom <> 0 Then
                If GlobalDeleteAtom(mhAtom) > 0 Then
                    Throw New System.ComponentModel.Win32Exception()
                End If
            End If
            If mhAtom = 0 Then
                mhAtom = GetCurrentAtom()
                Dim wh As New ManualResetEvent(False)
                mhHotkeyNotification = ThreadPool.RegisterWaitForSingleObject(wh, New WaitOrTimerCallback(AddressOf HotkeyWaitCallback), wh, -1, True)
                '\\ Start listening for the hotkey
                mwndListener = New GlobalHotkeyListener(mhAtom, Modifier.ModifierFlags, mHotkey, wh)

            End If

        End If
    End Sub

    Private Function GetCurrentAtom() As Int32

        Dim sAtomName As String = "MCL:" & mHotkey.ToString & ":" & Modifier.ToString
        Dim lRet As Int32
        Try
            lRet = GlobalAddAtom(sAtomName)
        Catch e As System.ComponentModel.Win32Exception
            Throw e
        End Try
        Return lRet

    End Function

#End Region

#Region "Public interface"

    Public Property Altkey() As Boolean
        Get
            Return Modifier.AltKey
        End Get
        Set(ByVal Value As Boolean)
            Modifier.AltKey = Value
        End Set
    End Property

    Public Property ControlKey() As Boolean
        Get
            Return Modifier.CtrlKey
        End Get
        Set(ByVal Value As Boolean)
            Modifier.CtrlKey = Value
        End Set
    End Property

    Public Property ShiftKey() As Boolean
        Get
            Return Modifier.ShiftKey
        End Get
        Set(ByVal Value As Boolean)
            Modifier.ShiftKey = Value
        End Set
    End Property

    Public Property WinKey() As Boolean
        Get
            Return Modifier.WinKey
        End Get
        Set(ByVal Value As Boolean)
            Modifier.WinKey = Value
        End Set
    End Property

    Public Property HotKey() As Keys
        Get
            Return mHotkey
        End Get
        Set(ByVal Value As Keys)
            If Value = Keys.F12 Then
                Throw New ArgumentException("The F12 key is reserved for the use of the system debugger")
            ElseIf Value = Keys.Alt Then
                Throw New ArgumentException("The ALT key can only be used as a modifier - set .AltKey=True ")
            ElseIf Value = Keys.Shift Then
                Throw New ArgumentException("The SHIFT key can only be used as a modifier - set .AltKey=True ")
            ElseIf Value = Keys.Control Then
                Throw New ArgumentException("The CONTROL key can only be used as a modifier - set .AltKey=True ")
            Else
                If Value <> mHotkey Then
                    mHotkey = Value
                    Call StartHotkeyWatch()
                End If
            End If
        End Set
    End Property
#End Region

#Region "Asynchronous processing"
    Public Sub HotkeyWaitCallback(
   ByVal state As Object,
   ByVal timedOut As Boolean)

        mhHotkeyNotification = ThreadPool.RegisterWaitForSingleObject(state, New WaitOrTimerCallback(AddressOf HotkeyWaitCallback), state, ThreadTimeout, True)
        CType(state, ManualResetEvent).Reset()

        If Not timedOut Then
            '\\ Raise a hotkeypressed event
            Call OnHotkeyPressed(New HotkeyEventArgs(mHotkey, Modifier))
        End If

    End Sub

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If mhAtom <> 0 Then
            If GlobalDeleteAtom(mhAtom) > 0 Then
                Throw New System.ComponentModel.Win32Exception()
            End If
        End If
    End Sub
End Class
