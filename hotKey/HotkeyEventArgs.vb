Public Class HotkeyEventArgs
#Region "Comments"
    '\\ --[HotkeyEventArgs]---------------------------------------------
    '\\ Class wrapper for the event arguments used in the hotkey pressed
    '\\ event of the SystemWideHotkeyComponent
    '\\ (c) Merrion Computing Ltd 
    '\\     http://www.merrioncomputing.com
    '\\ ------------------------------------------------------------------
#End Region
    Inherits EventArgs

#Region "Private member variables"
    Private mKey As Keys
    Private mModifier As HotkeyModifierDefinition
#End Region

#Region "Properties"
    Public ReadOnly Property Key() As Keys
        Get
            Return mKey
        End Get
    End Property

    Public ReadOnly Property Modifier() As HotkeyModifierDefinition
        Get
            Return mModifier
        End Get
    End Property

    Public Sub New(ByVal Key As Keys, ByVal Modifier As HotkeyModifierDefinition)
        mKey = Keys.KeyCode
        mModifier = Modifier
    End Sub

#End Region
End Class
