Public Class HotkeyModifierDefinition
#Region "Comments"
    '\\ --[HotkeyModifierDefinition]--------------------------------
    '\\ Class wrapper for the modifier keys
    '\\ (c) 2003 Merrion Computing Ltd
    '\\ http://www.merrioncomputing.com
    '\\ -------------------------------------------------------------

#End Region

#Region "Private Members"

    Private mModifier As Int32

#End Region

#Region "Public interface"

    Public Property AltKey() As Boolean
        Get
            Return CType((mModifier And HotkeyModifierFlags.MOD_ALT), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                mModifier = mModifier Or HotkeyModifierFlags.MOD_ALT
            Else
                mModifier = mModifier And (Not HotkeyModifierFlags.MOD_ALT)
            End If
        End Set
    End Property

    Public Property CtrlKey() As Boolean
        Get
            Return CType((mModifier And HotkeyModifierFlags.MOD_CONTROL), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                mModifier = mModifier Or HotkeyModifierFlags.MOD_CONTROL
            Else
                mModifier = mModifier And (Not HotkeyModifierFlags.MOD_CONTROL)
            End If
        End Set
    End Property

    Public Property ShiftKey() As Boolean
        Get
            Return CType((mModifier And HotkeyModifierFlags.MOD_SHIFT), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                mModifier = mModifier Or HotkeyModifierFlags.MOD_SHIFT
            Else
                mModifier = mModifier And (Not HotkeyModifierFlags.MOD_SHIFT)
            End If
        End Set
    End Property

    Public Property WinKey() As Boolean
        Get
            Return CType((mModifier And HotkeyModifierFlags.MOD_WIN), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                mModifier = mModifier Or HotkeyModifierFlags.MOD_WIN
            Else
                mModifier = mModifier And (Not HotkeyModifierFlags.MOD_WIN)
            End If
        End Set
    End Property

    Public ReadOnly Property ModifierFlags() As Int32
        Get
            Return mModifier
        End Get
    End Property


#End Region

#Region "Public constructors"
    Public Sub New()

    End Sub

    Public Sub New(ByVal AltKey As Boolean, ByVal ShiftKey As Boolean, ByVal CtrlKey As Boolean, ByVal WinKey As Boolean)
        Me.New()

    End Sub

    Public Sub New(ByVal Modifier As Int32)
        Me.New()
        mModifier = Modifier
    End Sub
#End Region

#Region "Overloaded interface"
    Public Overloads Function Equals(ByVal Test As HotkeyModifierDefinition) As Boolean

        Return (Test.ModifierFlags = ModifierFlags)

    End Function

    Public Overloads Function ToString() As String
        Return ("AltKey=" & AltKey.ToString & ":CtrlKey=" & CtrlKey.ToString & ":ShiftKey=" & ShiftKey.ToString & ":WinKey=" & WinKey.ToString)
    End Function

#End Region

End Class
