Public Class CustomColorTable
    Inherits ProfessionalColorTable
    Dim NewHighlightColor As Color
    Public Sub New(HighlightColor As Color)
        MyBase.New()
        NewHighlightColor = HighlightColor
    End Sub

    Public Overrides ReadOnly Property MenuItemSelected As Color
        Get
            Return NewHighlightColor ' Change this to your desired highlight color
        End Get
    End Property

    Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
        Get
            Return NewHighlightColor ' Change this to your desired gradient start color
        End Get
    End Property

    Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
        Get
            Return NewHighlightColor ' Change this to your desired gradient end color
        End Get
    End Property
End Class