Module Module2
    Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" _
(ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    Declare Function FindWindowEx Lib "user32.dll" Alias "FindWindowExA" _
    (ByVal hwndParent As Integer, ByVal hwndChildAfter As Integer,
    ByVal lpszClass As String, ByVal lpszWindow As String) As Integer

    Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" _
    (ByVal hWnd As Integer, ByVal MSG As Integer,
    ByVal wParam As Integer, ByVal lParam As Integer) As Integer
End Module
