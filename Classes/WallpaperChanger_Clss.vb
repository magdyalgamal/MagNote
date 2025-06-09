Imports System.IO
Imports Microsoft.Win32
Public Class WallpaperChanger_Clss
    Implements IDisposable
    Public Sub New(Optional ByVal UploadWallpaper As Boolean = False, Optional ByVal UseFolderContentsChkBx As CheckState = CheckState.Unchecked)
        MyBase.New()
        InitializeComponent()
    End Sub
    Public Sub SetWallpaperFit(imagePath As String, fitStyle As String)
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", True)
        If IsNothing(imagePath) Then
            imagePath = GetCurrentWallpaper()
        End If
        If key IsNot Nothing Then
            Select Case fitStyle.ToLower()
                Case "tile"
                    key.SetValue("WallpaperStyle", "0")
                    key.SetValue("TileWallpaper", "1")
                Case "center"
                    key.SetValue("WallpaperStyle", "0")
                    key.SetValue("TileWallpaper", "0")
                Case "stretch"
                    key.SetValue("WallpaperStyle", "2")
                    key.SetValue("TileWallpaper", "0")
                Case "fit"
                    key.SetValue("WallpaperStyle", "6")
                    key.SetValue("TileWallpaper", "0")
                Case "fill"
                    key.SetValue("WallpaperStyle", "10")
                    key.SetValue("TileWallpaper", "0")
                Case "span"
                    key.SetValue("WallpaperStyle", "22")
                    key.SetValue("TileWallpaper", "0")
                Case Else
                    Throw New ArgumentException("Invalid fit style specified.")
            End Select

            ' Set the wallpaper image path
            key.SetValue("Wallpaper", imagePath)

            ' Apply changes
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINIFILE Or SPIF_SENDCHANGE)
        End If
    End Sub

    ' Constants for SystemParametersInfo function
    Private Const SPI_SETDESKWALLPAPER As Integer = 20
    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDCHANGE As Integer = &H2

    ' PInvoke SystemParametersInfo function
    <Runtime.InteropServices.DllImport("user32.dll", CharSet:=Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function SystemParametersInfo(uiAction As Integer, uiParam As Integer, pvParam As String, fWinIni As Integer) As Boolean
    End Function

    Public Function GetCurrentWallpaper() As String
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", False)
        If key IsNot Nothing Then
            Dim wallpaperPath As String = key.GetValue("Wallpaper").ToString()
            key.Close()
            Return wallpaperPath
        Else
            Return String.Empty
        End If
    End Function

    Public Function GetWallpaperStyle() As String
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
        If key IsNot Nothing Then
            Dim style As String = key.GetValue("WallpaperStyle").ToString()
            Select Case style
                Case "0"
                    Return "Tile"
                Case "1"
                    Return "Center"
                Case "2"
                    Return "Stretch"
                Case "6"
                    Return "Fit"
                Case "10"
                    Return "Fill"
                Case "22"
                    Return "Span"
                Case Else
                    Return "Unknown"
            End Select
        Else
            Return "Registry key not found."
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
