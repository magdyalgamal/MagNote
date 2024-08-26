Imports System.Runtime.InteropServices

Module HotkeyApi
#Region "Comments"
    '\\ --[HotkeyApi]---------------------------------------------------------------------------
    '\\ Module for all the hotkey related API calls and constants
    '\\ (c) 2003 Merrion Computing Ltd
    '\\ http://www.merrioncomputing.com
    '\\ ------------------------------------------------------------------------------------------
#End Region
#Region "Public enumerated types"
    Public Enum HotkeyModifierFlags
        MOD_ALT = &H1
        MOD_CONTROL = &H2
        MOD_SHIFT = &H4
        MOD_WIN = &H8
    End Enum

    Public Enum WindowStyleBits
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_BORDER The window has a frame border around it
        WS_BORDER = &H800000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_CAPTION The window has a caption title bar
        WS_CAPTION = &HC00000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_CHILD The window is a child (non top level) window
        WS_CHILD = &H40000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_CLIPCHILDREN The window cannot draw over other windows that are its children
        WS_CLIPCHILDREN = &H2000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_CLIPSIBLINGS This window's child windows cannot draw over each other
        WS_CLIPSIBLINGS = &H4000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_DISABLED The window is disabled and cannot accept imput
        WS_DISABLED = &H8000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_DLGFRAME The window has a double border but no caption
        WS_DLGFRAME = &H400000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_GROUP The window is the first of a group of windows in a dialog box
        WS_GROUP = &H20000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_HSCROLL The window has a horizontal scroll bar
        WS_HSCROLL = &H100000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_MAXIMIZE The window is maximised when first created
        WS_MAXIMIZE = &H1000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_MAXIMIZEBOX The window has a maximise box
        WS_MAXIMIZEBOX = &H10000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_MINIMIZE The window is minimised when first created
        WS_MINIMIZE = &H20000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_MINIMIZEBOX The window has a minimise box
        WS_MINIMIZEBOX = &H20000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_OVERLAPPED The window is a standard top level (application) window
        WS_OVERLAPPED = &H0&
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_POPUP The window is a popup type window
        WS_POPUP = &H80000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_SYSMENU The window has a system menu box
        WS_SYSMENU = &H80000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_TABSTOP The window accepts the focus when tabbed to
        WS_TABSTOP = &H10000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_THICKFRAME The window has a resizeable window style thick border
        WS_THICKFRAME = &H40000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_VISIBLE The window is visible
        WS_VISIBLE = &H10000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_VSCROLL The window has a vertical scroll bar
        WS_VSCROLL = &H200000
    End Enum

    ' ##ENUMERATION_DESCRIPTION These extended style bits are combined to define what style elements the window that is created with them will have.
    Public Enum WindowStyleExtendedBits
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_ACCEPTFILES The window will accept files being dropped on it from Explorer
        WS_EX_ACCEPTFILES = &H10&
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_DLGMODALFRAME The window has a double border and may have a caption
        WS_EX_DLGMODALFRAME = &H1&
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_NOPARENTNOTIFY The window will not send a message to its parent (if any) when it is destroyed
        WS_EX_NOPARENTNOTIFY = &H4&
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_TOPMOST The window will float over non topmost windows
        WS_EX_TOPMOST = &H8&
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_TRANSPARENT The window does not obscure windows behind it
        WS_EX_TRANSPARENT = &H20&
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_TOOLWINDOW The window has a toolbox look (smaller caption bar)
        WS_EX_TOOLWINDOW = &H80&
        '\\ New from 95/NT4 onwards
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_MDICHILD The window is an MDI child style window
        WS_EX_MDICHILD = &H40
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_WINDOWEDGE The window has a beveled outer edge
        WS_EX_WINDOWEDGE = &H100
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_CLIENTEDGE The window has a chiselled inner edge
        WS_EX_CLIENTEDGE = &H200
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_CONTEXTHELP The window supports context sensitive help
        WS_EX_CONTEXTHELP = &H400
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_RIGHT Text in this window is right aligned (if WS_EX_RTLREADING is set)
        WS_EX_RIGHT = &H1000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_LEFT Test in this window is left aligned
        WS_EX_LEFT = &H0
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_RTLREADING Arranges text for right to left reading (for Hebrew or Arabic for example)
        WS_EX_RTLREADING = &H2000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_LTRREADING Arranges text for left to right reading
        WS_EX_LTRREADING = &H0
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_LEFTSCROLLBAR Scrollbar on the left hand side (if WS_EX_RTLREADING is set)
        WS_EX_LEFTSCROLLBAR = &H4000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_RIGHTSCROLLBAR Scrollbar on the right hand side
        WS_EX_RIGHTSCROLLBAR = &H0
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_CONTROLPARENT causes child windows of this window to have a tab order
        WS_EX_CONTROLPARENT = &H10000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_STATICEDGE Makes the window a 3D style frame window
        WS_EX_STATICEDGE = &H20000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_APPWINDOW Minimises to the task bar
        WS_EX_APPWINDOW = &H40000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_OVERLAPPEDWINDOW Has a raised edge around it like an application window
        WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE)
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_PALETTEWINDOW Is a floating toolbar or palette style window
        WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST)
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_LAYERED Creates a layered window
        WS_EX_LAYERED = &H80000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_NOINHERITLAYOUT Does not pass on it's layout to child windows
        WS_EX_NOINHERITLAYOUT = &H100000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_LAYOUTRTL Layout ir right to left reading (where appropriate)
        WS_EX_LAYOUTRTL = &H400000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_COMPOSITED Paints the child windows in bottom to top order
        WS_EX_COMPOSITED = &H2000000
        ' ##ENUMERATION_MEMBER_DESCRIPTION WS_EX_NOACTIVATE Window cannot be activated
        WS_EX_NOACTIVATE = &H8000000
    End Enum
#End Region

#Region "Api Declarations"

    <DllImport("user32", EntryPoint:="RegisterHotKey",
SetLastError:=True,
ExactSpelling:=True,
CallingConvention:=CallingConvention.StdCall)>
    Public Function RegisterHotkey(ByVal hwnd As IntPtr,
                               ByVal Id As Int32,
                               <MarshalAs(UnmanagedType.U4)> ByVal fsModifiers As Int32,
                               <MarshalAs(UnmanagedType.U4)> ByVal vkey As Int32) As Boolean

    End Function

    <DllImport("user32", EntryPoint:="UnregisterHotKey",
SetLastError:=True,
ExactSpelling:=True,
CallingConvention:=CallingConvention.StdCall)>
    Public Function UnregisterHotkey(ByVal hwnd As Int32,
                               ByVal Id As Int32) As Boolean

    End Function

    <DllImport("kernel32", EntryPoint:="GlobalAddAtom",
SetLastError:=True,
ExactSpelling:=False)>
    Public Function GlobalAddAtom(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpString As String) As Int32

    End Function

    <DllImport("kernel32", EntryPoint:="GlobalDeleteAtom",
SetLastError:=True,
ExactSpelling:=True,
CallingConvention:=CallingConvention.StdCall)>
    Public Function GlobalDeleteAtom(ByVal nAtom As Int32) As Int32

    End Function

    Public Const WM_HOTKEY = &H312

#End Region

End Module

