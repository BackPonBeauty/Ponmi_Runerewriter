Option Strict Off

Imports System.Net

Imports Newtonsoft.Json

Imports System.Runtime.InteropServices

Imports EasyHttp.Http


Public Class Form3
    Dim one As Boolean = False
    Public myteam As String = "ORDER"
    'Dim nono As String = ""
    Public nn As Integer = 0
    Public nns As Integer = 0
    Public Shared http As HttpClient
    'Dim blue As String = My.Settings("Blue")
    Public syain As New DataTable
    Public syain2 As New DataTable
    Dim siz As Integer = 24
    Dim sizz As Integer = 16
    Dim blue As Color = Color.Cyan
    Dim red As Color = Color.LightPink
    Public score As Integer = 0
    Public bscore As Integer = 0
    Public rscore As Integer = 0
    Public teamscore As Integer = 0
    Public teston As Boolean = False
    Public yy_bin = 0
    Public yy As Integer = 0
    Public patch As String

    Dim kan As Integer = 30
    Public kai As Integer = 220

    Public inhi As Boolean = False
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer
    Public C As Boolean = False

    Private mysound As String = "mysound"
    Private theme As String = "theme"
    Public Sub New()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        http = New HttpClient()
        InitializeComponent()
    End Sub

    <DllImport("USER32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wp As Integer, ByRef lp As Point) As IntPtr
    End Function

    Private Const EM_GETSCROLLPOS As Integer = &H4DD
    Private Const EM_SETSCROLLPOS As Integer = &H4DE

    Private Sub richTextBox1_VScroll(sender As Object, e As System.EventArgs)
        Dim pt As Point
        SendMessage(RichTextBox1.Handle, EM_GETSCROLLPOS, 0, pt)
    End Sub

    Dim currentSummoner As String = "背中ポン美"

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        syain.Columns.Add("chmname")
        syain.Columns.Add("sumname")
        syain.Columns.Add("team")
        syain.Columns.Add("killstreak")
        syain2.Columns.Add("chmname")
        syain2.Columns.Add("sumname")
        syain2.Columns.Add("team")
        syain2.Columns.Add("killstreak")
        'syain.Rows.Add("Ahri", "TheBaconProphet", "ORDER")
        'syain.Rows.Add("Samira", "夜来香", "ORDER")
        'syain.Rows.Add("Kennen", "Humanoid Typhoon", "ORDER")
        'syain.Rows.Add("Taric", "KIIHR", "ORDER")
        'syain.Rows.Add("Soraka", "masyumaro15", "ORDER")
        'syain.Rows.Add("Ziggs", "くず入れ", "CHOS")
        'syain.Rows.Add("Vayne", "ritsuko1111", "CHOS")
        'syain.Rows.Add("Akali", "menma0220", "CHOS")
        'syain.Rows.Add("Alistar", "mamezaru", "CHOS")
        'syain.Rows.Add("Rakan", "jubia63", "CHOS")
        'syain.Rows.Add("Ahri", "Arinko", "ORDER", 0)
        'syain.Rows.Add("Samira", "背中ポン美", "ORDER", 0)
        'syain.Rows.Add("Kennen", "Humanoid Typhoon", "ORDER", 0)
        'syain.Rows.Add("Taric", "KIIHR", "ORDER", 0)
        'syain.Rows.Add("Soraka", "masyumaro15", "ORDER", 0)
        'syain.Rows.Add("Ziggs", "くず入れ", "CHAOS", 0)
        'syain.Rows.Add("Vayne", "ritsuko1111", "CHAOS", 0)
        'syain.Rows.Add("Akali", "menma0220", "CHAOS", 0)
        'syain.Rows.Add("Alistar", "mamezaru", "CHAOS", 0)
        'syain.Rows.Add("Rakan", "jubia63", "CHAOS", 0)
        DataGridView1.DataSource = syain
        DataGridView1.DefaultCellStyle.BackColor = Color.Black
        DataGridView2.DataSource = syain2
        DataGridView2.DefaultCellStyle.BackColor = Color.Black
        Dim sname5() As String = Form1.TextBox5.Text.Split("#")
        currentSummoner = sname5(0)
        'Console.WriteLine("currentSummoner ::::::::::: " & currentSummoner)
        patch = Form1.Label1.Text
    End Sub

    Private Sub statsi(nnn)

        Application.DoEvents()
        Dim response As HttpResponse = Nothing
        Try
            Dim password As String = Form1.token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/eventdata")
        Catch Exception As Exception
            Form1.TextBox1.AppendText("Error : No Response 04" & vbCrLf)
            Label1.Text = "off"
            Exit Sub
        End Try

        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
        Else
            Dim grid = response.DynamicBody
            Try
                If grid.Events(nnn).EventName IsNot Nothing Then
                    Dim mess As String = ""
                    Dim etime_bin As Double = grid.Events(nnn).EventTime
                    Dim etime As String = ""
                    If etime_bin < 60 Then
                        etime = " 00:" & Math.Round(etime_bin, 0, MidpointRounding.AwayFromZero).ToString("00")
                    Else
                        Dim etime_h_bin As Double = etime_bin / 60
                        Dim etime_h As Double = Math.Floor(etime_h_bin)
                        Dim etime_m As Integer = etime_bin Mod 60
                        etime = etime_h.ToString(" 00") & ":" & etime_m.ToString("00")
                    End If
                    Dim ename As String = grid.Events(nnn).EventName
                    '*** 0
                    If ename = "ChampionKill" Then
                        Dim Assisterse As New List(Of String)
                        Dim assChamp As New List(Of String)
                        Dim aaa As Integer = 0
                        For Each Assi In grid.Events(nnn).Assisters
                            Assisterse.Add(grid.Events(nnn).Assisters(aaa).Replace(Chr(13), "").Replace(Chr(10), ""))
                            Dim d0 As DataRow()
                            d0 = syain2.Select("sumname = '" + Assisterse(aaa).Replace(Chr(13), "").Replace(Chr(10), "") + "'")
                            For Each d As DataRow In d0
                                assChamp.Add(d("chmname").ToString)
                            Next
                            aaa += 1
                        Next

                        Dim killername As String = grid.Events(nnn).KillerName
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim d1 As DataRow()
                        d1 = syain2.Select("sumname = '" + killername + "'")
                        Dim killerChamp As String = "Minion"
                        For Each d As DataRow In d1
                            killerChamp = d("chmname").ToString
                        Next
                        Dim vicname As String = grid.Events(nnn).VictimName
                        vicname = vicname.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim d2 As DataRow()
                        d2 = syain2.Select("sumname = '" + vicname + "'")
                        Dim VicChamp As String = "Minion"
                        For Each d As DataRow In d2
                            VicChamp = d("chmname").ToString
                        Next
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next

                        kill(etime, Team, killername, vicname, killerChamp, VicChamp, Assisterse, assChamp)


                        'If Team = "ORDER" Then
                        '    ''RichTextBox1.Focus()
                        '    RichTextBox1.SelectionColor = Color.White
                        '    RichTextBox1.AppendText(vbCrLf & etime & " : ")
                        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\" &  patch & "\champimage\" & killerChamp & ".png",
                        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                        '        bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                        '        Clipboard.SetDataObject(bmp, True)
                        '    End Using
                        '    Dim iData1 As IDataObject = Clipboard.GetDataObject()
                        '    If iData1.GetDataPresent(DataFormats.Bitmap) Then
                        '        RichTextBox1.Paste()
                        '    End If
                        '    Clipboard.Clear()
                        '    'RichTextBox1.AppendText(
                        '    RichTextBox1.SelectionColor = blue
                        '    RichTextBox1.AppendText(killername)
                        '    RichTextBox1.SelectionColor = Color.White
                        '    RichTextBox1.AppendText(" killed  ")
                        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\" &  patch & "\champimage\" & VicChamp & ".png",
                        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                        '        bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                        '        Clipboard.SetDataObject(bmp, True)
                        '    End Using
                        '    Dim iData2 As IDataObject = Clipboard.GetDataObject()
                        '    If iData2.GetDataPresent(DataFormats.Bitmap) Then
                        '        RichTextBox1.Paste()
                        '    End If
                        '    Clipboard.Clear()
                        '    RichTextBox1.SelectionColor = red
                        '    RichTextBox1.AppendText(vicname)
                        '    If assChamp.Count > 0 Then
                        '        Ass(Team, Assisterse, assChamp)
                        '        'RichTextBox1.SelectionColor = Color.White
                        '        'RichTextBox1.AppendText(" Assister: ")
                        '        'Dim aa As Integer = 0
                        '        'For Each assister In Assisterse
                        '        '    RichTextBox1.SelectionColor = blue
                        '        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\" &  patch & "\champimage\" & assChamp(aa) & ".png",
                        '        '                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        '        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                        '        '        bmp = New Bitmap(bmp, CInt(sizz), CInt(sizz))
                        '        '        Clipboard.SetDataObject(bmp, True)
                        '        '    End Using
                        '        '    Dim iData3 As IDataObject = Clipboard.GetDataObject()
                        '        '    If iData3.GetDataPresent(DataFormats.Bitmap) Then
                        '        '        RichTextBox1.Paste()
                        '        '    End If
                        '        '    Clipboard.Clear()
                        '        '    RichTextBox1.SelectionColor = blue
                        '        '    'RichTextBox1.AppendText(Assisterse(aa))

                        '        '    aa += 1
                        '        'Next
                        '    End If

                        'Else
                        '    ''RichTextBox1.Focus()
                        '    RichTextBox1.SelectionColor = Color.White
                        '    RichTextBox1.AppendText(vbCrLf & etime & " : ")
                        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\" &  patch & "\champimage\" & killerChamp & ".png",
                        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                        '        bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                        '        Clipboard.SetDataObject(bmp, True)
                        '    End Using
                        '    Dim iData3 As IDataObject = Clipboard.GetDataObject()
                        '    If iData3.GetDataPresent(DataFormats.Bitmap) Then
                        '        RichTextBox1.Paste()
                        '    End If
                        '    Clipboard.Clear()
                        '    RichTextBox1.SelectionColor = red
                        '    RichTextBox1.AppendText(killername)
                        '    RichTextBox1.SelectionColor = Color.White
                        '    RichTextBox1.AppendText(" killed ")
                        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\" &  patch & "\champimage\" & VicChamp & ".png",
                        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                        '        bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                        '        Clipboard.SetDataObject(bmp, True)
                        '    End Using
                        '    Dim iData4 As IDataObject = Clipboard.GetDataObject()
                        '    If iData4.GetDataPresent(DataFormats.Bitmap) Then
                        '        RichTextBox1.Paste()
                        '    End If
                        '    Clipboard.Clear()
                        '    RichTextBox1.SelectionColor = blue
                        '    RichTextBox1.AppendText(vicname)
                        '    If assChamp.Count > 0 Then
                        '        Ass(Team, Assisterse, assChamp)
                        '        'RichTextBox1.SelectionColor = Color.White
                        '        ''RichTextBox1.AppendText(vbCrLf   & "          Assister: ")
                        '        'RichTextBox1.AppendText(" Assister: ")
                        '        'Dim aa As Integer = 0
                        '        'For Each assister In Assisterse
                        '        '    RichTextBox1.SelectionColor = red
                        '        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\" &  patch & "\champimage\" & assChamp(aa) & ".png",
                        '        '                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        '        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                        '        '        bmp = New Bitmap(bmp, CInt(sizz), CInt(sizz))
                        '        '        Clipboard.SetDataObject(bmp, True)
                        '        '    End Using
                        '        '    Dim iData1 As IDataObject = Clipboard.GetDataObject()
                        '        '    If iData1.GetDataPresent(DataFormats.Bitmap) Then
                        '        '        RichTextBox1.Paste()
                        '        '    End If
                        '        '    Clipboard.Clear()
                        '        '    RichTextBox1.SelectionColor = red
                        '        '    'RichTextBox1.AppendText(Assisterse(aa))

                        '        '    aa += 1
                        '        'Next

                    ElseIf ename = "Multikill" Then
                        yy -= 1
                        Dim killername As String = grid.Events(nnn).KillerName
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim KillStreak As String = grid.Events(nnn).KillStreak
                        Dim killstreakM As String = ""
                        Dim cor As Color = Color.Blue
                        If KillStreak = 2 Then
                            killstreakM = "Double"
                            cor = Color.Yellow
                        End If
                        If KillStreak = 3 Then
                            killstreakM = "Triple"
                            cor = Color.Yellow
                        End If
                        If KillStreak = 4 Then
                            killstreakM = "Quadra"
                            cor = Color.Yellow
                        End If
                        If KillStreak = 5 Then
                            killstreakM = "Penta"
                            cor = Color.Yellow
                        End If

                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        ks(Team, killstreakM)
                        'If Team = "ORDER" Then
                        '    ''RichTextBox1.Focus()

                        '    RichTextBox1.SelectionColor = cor
                        '    RichTextBox1.AppendText(vbCrLf & "                         ")
                        '    RichTextBox1.AppendText(killstreakM & "Kill ")

                        'Else
                        '    ''RichTextBox1.Focus()

                        '    RichTextBox1.SelectionColor = cor
                        '    RichTextBox1.AppendText(vbCrLf & "                         ")
                        '    RichTextBox1.AppendText(killstreakM & "Kill ")

                        'End If
                    ElseIf ename = "FirstBlood" Then
                        yy -= 1
                        Dim killername As String = grid.Events(nnn).Recipient
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        fb(Team)
                        'If Team = "ORDER" Then
                        '    RichTextBox1.SelectionColor = Color.Yellow
                        '    RichTextBox1.AppendText(vbCrLf & "                     FirstBlood ")
                        'Else
                        '    RichTextBox1.SelectionColor = Color.Yellow
                        '    RichTextBox1.AppendText(vbCrLf & "                     FirstBlood ")
                        'End If
                    ElseIf ename = "Ace" Then
                        yy -= 1
                        score -= 1
                        Dim killername As String = grid.Events(nnn).Acer
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        If Team = myteam Then
                            score += 1
                            RichTextBox1.SelectionColor = blue
                            RichTextBox1.AppendText(" ACE ")
                        Else
                            score -= 1
                            RichTextBox1.SelectionColor = red
                            RichTextBox1.AppendText(" ACE ")
                        End If
                    ElseIf ename = "InhibKilled" Then
                        Dim inhib_bin As String = grid.Events(nnn).InhibKilled
                        'Inhib_TOrder_L1_P1_196971597
                        Dim inhib As String = inhib_bin.Substring(7, 1)
                        Dim Assisterse As New List(Of String)
                        Dim assChamp As New List(Of String)
                        Dim aaa As Integer = 0
                        For Each Assi In grid.Events(nnn).Assisters
                            Assisterse.Add(grid.Events(nnn).Assisters(aaa).Replace(Chr(13), "").Replace(Chr(10), ""))
                            Dim d0 As DataRow()
                            d0 = syain2.Select("sumname = '" + Assisterse(aaa).Replace(Chr(13), "").Replace(Chr(10), "") + "'")
                            For Each d As DataRow In d0
                                assChamp.Add(d("chmname").ToString)
                            Next
                            aaa += 1
                        Next
                        'Select Case inhib_bin
                        '    Case "Barracks_T1_C1"
                        '        inhib = "[Blue Inhib]"
                        '    Case "Barracks_T2_C1"
                        '        inhib = "[Red Inhib]"
                        '    Case Else
                        '        inhib = inhib_bin
                        'End Select
                        Dim killername As String = grid.Events(nnn).KillerName
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim d1 As DataRow()
                        d1 = syain2.Select("sumname = '" + killername + "'")
                        Dim killerChamp As String = "Minion"
                        For Each d As DataRow In d1
                            killerChamp = d("chmname").ToString
                        Next
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        Dim Turret_s As String = "Minion"

                        'If inhib = "C" Then
                        '    Turret_s = "CHAOS"
                        'Else
                        '    Turret_s = "ORDER"
                        'End If

                        If inhib = "C" Then
                            Turret_s = "ORDER"
                        Else
                            Turret_s = "CHAOS"
                        End If

             

                        ik(etime, Team, killername, killerChamp, Turret_s, Assisterse, assChamp)
                        TextBox9.AppendText(etime & "," & inhib & "," & Team & "," & killername & "," & killerChamp & "," & Turret_s)


                    ElseIf ename = "TurretKilled" Then
                        Dim turret_bin As String = grid.Events(nnn).TurretKilled
                        Dim killername As String = grid.Events(nnn).KillerName
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        If turret_bin <> "Obelisk" And killername <> "Obelisk" Then
                            'Turret_TChaos_L1_P3_671196429
                            Dim turret As String = turret_bin.Substring(8, 1)
                            Dim Assisterse As New List(Of String)
                            Dim assChamp As New List(Of String)
                            Dim aaa As Integer = 0
                            For Each Assi In grid.Events(nnn).Assisters
                                Assisterse.Add(grid.Events(nnn).Assisters(aaa).Replace(Chr(13), "").Replace(Chr(10), ""))
                                Dim d0 As DataRow()
                                d0 = syain2.Select("sumname = '" + Assisterse(aaa).Replace(Chr(13), "").Replace(Chr(10), "") + "'")
                                For Each d As DataRow In d0
                                    assChamp.Add(d("chmname").ToString)
                                Next
                                aaa += 1
                            Next
                            'Dim killername As String = grid.Events(nnn).KillerName
                            Dim d1 As DataRow()
                            d1 = syain2.Select("sumname = '" + killername + "'")
                            Dim killerChamp As String = "Minion"
                            For Each d As DataRow In d1
                                killerChamp = d("chmname").ToString
                            Next
                            Dim d3 As DataRow()
                            d3 = syain2.Select("sumname = '" + killername + "'")
                            Dim Team As String = "Minion"
                            For Each d As DataRow In d3
                                Team = d("team").ToString
                            Next
                            Dim Turret_s As String = "Minion"
                            If turret = "C" Then
                                Turret_s = "ORDER"
                            Else
                                Turret_s = "CHAOS"
                            End If
                            'If turret = "O" Then
                            '    If myteam = "CHAOS" Then
                            '        Turret_s = "CHAOS"
                            '    Else
                            '        Turret_s = "ORDER"
                            '    End If
                            'Else
                            '    If myteam = "CHAOS" Then
                            '        Turret_s = "ORDER"
                            '    Else
                            '        Turret_s = "CHAOS"
                            '    End If
                            'End If
                            tk(etime, Team, killername, killerChamp, Turret_s, Assisterse, assChamp)
                            TextBox9.AppendText(etime & "," & turret & "," & Team & "," & killername & "," & killerChamp & "," & Turret_s)
                        Else
                            'TextBox9.AppendText("!!!!!!!!!!!!!!!!break")
                            'nns += 1
                            yy -= 1
                        End If

                    ElseIf ename = "FirstBrick" Then
                        yy -= 1
                        score -= 1
                        Dim killername As String = grid.Events(nnn).KillerName
                        killername = killername.Replace(Chr(13), "").Replace(Chr(10), "")
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        If Team = "ORDER" Then
                            score += 1
                            RichTextBox1.SelectionColor = blue
                            RichTextBox1.AppendText(" FirstBrick ")
                        Else
                            score -= 1
                            RichTextBox1.SelectionColor = red
                            RichTextBox1.AppendText(" FirstBrick ")
                        End If
                        'ElseIf ename = "GameEnd" Then

                    Else

                        ''RichTextBox1.Focus()
                        RichTextBox1.SelectionColor = Color.White
                        RichTextBox1.AppendText(vbCrLf & etime & " : ")
                        RichTextBox1.SelectionColor = Color.LightGreen
                        RichTextBox1.AppendText(ename)
                    End If
                    'If ename = "GameEnd" Then
                    '    '  Timer1.Enabled = False
                    '    'Form2.Timer1.Enabled = False
                    '    'Form6.Timer1.Enabled = False
                    '    'Form2.blue = ""
                    '    'Form2.Label1.Text = "off"
                    '    'Form1.Timer2.Enabled = False
                    '    'Form1.onece = False
                    '    'Form1.L = False
                    '    'Form1.L = False
                    '    'Form1.L = False
                    '    'Form1.Timer1.Enabled = True
                    '    'Form1.Button10.PerformClick()
                    '    'nn = 0
                    '    ''Dim tim As DateTime = DateTime.Now.ToString("yyyyMMddhhmmss")
                    '    ''Form1.ConvertDataTableToCsv(syain, tim & "syain.csv", True, False)
                    '    'Dim jsonObj As String = JsonConvert.SerializeObject(grid)
                    '    'TextBox1.Text = jsonObj
                    '    'Form2.blue = ""
                    '    'Form1.ConvertDataTableToCsv(syain, "syain.csv", True, False)
                    '    ''syain.Rows.Clear()
                    '    RichTextBox1.SelectionColor = Color.White
                    '    RichTextBox1.AppendText(vbCrLf & etime & " : ")
                    '    RichTextBox1.SelectionColor = Color.LightGreen
                    '    RichTextBox1.AppendText(ename)
                    'End If
                End If
                scp.Text = score
                'Form5.Panel2.Width = 100 + score
                'RichTextBox1.Focus()
            Catch ex As Exception
                'RichTextBox1.SelectionColor = Color.White
                'Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_b.png",
                '                                   System.IO.FileMode.Open, System.IO.FileAccess.Read)
                '    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                '    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                '    Clipboard.SetDataObject(bmp, True)
                'End Using
                'Dim iData1 As IDataObject = Clipboard.GetDataObject()
                'If iData1.GetDataPresent(DataFormats.Bitmap) Then
                '    RichTextBox1.Paste()
                'End If
                yy -= 1
                nns -= 1
            End Try
        End If
    End Sub

    Private Sub stats(nnn)
        C = False
        scp.Text = nn
        Application.DoEvents()
        Dim response As HttpResponse = Nothing
        Try
            Dim password As String = Form1.token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/eventdata")
        Catch Exception As Exception
            Form1.TextBox1.AppendText("Error : No Response 04")
            'nn = 1
            If Process.GetProcessesByName("League of Legends").Length <> 0 Then
                Exit Sub
            Else
                s_flag = False
                Label1.Text = "OFF"
                Form2.Timer1.Enabled = False
                Form2.Label1.Text = "OFF"
                Exit Sub
            End If
        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then

        Else
            Dim r As New System.Random()
            Dim grid = response.DynamicBody
            'Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            'TextBox7.Text = jsonObj
            Try
                If grid.Events(nnn).EventName IsNot Nothing Then
                    Dim mess As String = ""
                    Dim etime_bin As Double = grid.Events(nnn).EventTime
                    Dim etime As String = ""
                    If etime_bin < 60 Then
                        etime = "00:" & Math.Round(etime_bin, 0, MidpointRounding.AwayFromZero).ToString("00")
                    Else
                        Dim etime_h_bin As Double = etime_bin / 60
                        Dim etime_h As Double = Math.Floor(etime_h_bin)
                        Dim etime_m As Integer = etime_bin Mod 60
                        etime = etime_h.ToString("00") & ":" & etime_m.ToString("00")
                    End If

                    Dim ename As String = grid.Events(nnn).EventName

                    If ename = "ChampionKill" Then
                        Dim Assisterse As New List(Of String)
                        Dim assChamp As New List(Of String)
                        Dim aaa As Integer = 0
                        For Each Assi In grid.Events(nnn).Assisters
                            Assisterse.Add(grid.Events(nnn).Assisters(aaa).Replace(Chr(13), "").Replace(Chr(10), ""))
                            Dim d0 As DataRow()
                            d0 = syain2.Select("sumname = '" + Assisterse(aaa).Replace(Chr(13), "").Replace(Chr(10), "") + "'")
                            For Each d As DataRow In d0
                                assChamp.Add(d("chmname").ToString)
                            Next
                            aaa += 1
                        Next
                        Dim killername As String = grid.Events(nnn).KillerName
                        Dim d1 As DataRow()
                        d1 = syain2.Select("sumname = '" + killername + "'")
                        Dim killerChamp As String = "Minion"
                        For Each d As DataRow In d1
                            killerChamp = d("chmname").ToString
                        Next
                        Dim vicname As String = grid.Events(nnn).VictimName
                        Dim d2 As DataRow()
                        d2 = syain2.Select("sumname = '" + vicname + "'")
                        Dim VicChamp As String = "Minion"
                        Dim killcount As Integer
                        For Each d As DataRow In d2
                            VicChamp = d("chmname").ToString
                            killcount = d("killstreak")
                            d("killstreak") = 0
                        Next
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        Dim KillStreak As Integer
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                            d("killstreak") = d("killstreak") + 1
                            KillStreak = d("killstreak")
                        Next
                        Dim d4 As DataRow()
                        d4 = syain2.Select("sumname = '" + vicname + "'")
                        Dim vicTeam As String = "Minion"
                        For Each d As DataRow In d4
                            vicTeam = d("team").ToString
                        Next
                        'Console.WriteLine(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & vicTeam & "," & KillStreak)
                        If Team = "Minion" And Assisterse.Count = 0 Then
                            Dim Rnd As Integer = r.Next(1, 4)
                            If vicname = currentSummoner Then
                                TextBox2.AppendText(etime & "," & "Executed" & "," & "0" & "," & vicname & "," & Team & "," & 0 & "," & Rnd)
                                TextsChanged(False)
                                If vicTeam = myteam And Rnd = 2 Then
                                    Rnd = r.Next(1, 4)
                                    TextBox2.AppendText(etime & "," & "ExecutedC" & "," & "0" & "," & vicname & "," & Team & "," & 0 & "," & Rnd)
                                    TextsChanged(True)
                                End If
                            End If

                        Else
                            If vicname = currentSummoner Then
                                Dim Rnd As Integer = r.Next(1, 3)
                                TextBox2.AppendText(etime & "," & "dead" & "," & "0" & "," & vicname & "," & Team & "," & 0 & "," & Rnd)
                                TextsChanged(False)
                                Rnd = r.Next(1, 8)
                                TextBox2.AppendText(etime & "," & "deadC" & "," & "0" & "," & vicname & "," & Team & "," & 0 & "," & Rnd)
                                TextsChanged(True)
                            Else
                                '###################################20221113#############################
                                If TextBox3.Lines.Length < 1 Or KillStreak > 2 Or Team = "teemo" Then
                                    'If KillStreak > 2 Or Team = "teemo" Then
                                    Dim Rnd As Integer = r.Next(1, 4)
                                    If KillStreak < 3 Then
                                        TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & KillStreak & "," & Rnd)
                                        TextsChanged(False)
                                    End If
                                    Select Case KillStreak
                                        Case 0, 1, 2
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner
                                                            Rnd = r.Next(1, 8)
                                                            TextBox2.AppendText(etime & "," & "youhaveslayenemy1C" & "," & killername & vicname & "," & "," & Team & "," & KillStreak & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else
                                                            Rnd = r.Next(1, 5)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy1C" & "," & killername & vicname & "," & "," & Team & "," & KillStreak & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally1C" & "," & killername & "," & vicname & "," & Team & "," & KillStreak & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select

                                        Case 3 'legendaly
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner ' me
                                                            TextBox2.AppendText(etime & "," & "youkillstreak" & "," & killername & vicname & "," & "," & Team & "," & 3 & "," & 1)
                                                            TextsChanged(False)
                                                            Rnd = r.Next(1, 4)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy3C" & "," & killername & vicname & "," & "," & Team & "," & 3 & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else ' ally 
                                                            Rnd = r.Next(1, 3)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemy" & "," & killername & "," & vicname & "," & Team & "," & 3 & "," & Rnd)
                                                            TextsChanged(False)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemyC" & "," & killername & "," & vicname & "," & Team & "," & 3 & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & 3 & "," & Rnd)
                                                    TextsChanged(False)
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally3C" & "," & killername & "," & vicname & "," & Team & "," & 3 & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select

                                        Case 4 'rampage
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner ' me
                                                            TextBox2.AppendText(etime & "," & "youkillstreak" & "," & killername & vicname & "," & "," & Team & "," & 4 & "," & 1)
                                                            TextsChanged(False)
                                                            Rnd = r.Next(1, 4)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy4C" & "," & killername & vicname & "," & "," & Team & "," & 4 & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else ' ally 
                                                            Rnd = r.Next(1, 3)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemy" & "," & killername & "," & vicname & "," & Team & "," & 4 & "," & Rnd)
                                                            TextsChanged(False)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemyC" & "," & killername & "," & vicname & "," & Team & "," & 4 & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & 4 & "," & Rnd)
                                                    TextsChanged(False)
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally4C" & "," & killername & "," & vicname & "," & Team & "," & 4 & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select

                                        Case 5 'unstoppable
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner ' me
                                                            TextBox2.AppendText(etime & "," & "youkillstreak" & "," & killername & vicname & "," & "," & Team & "," & 5 & "," & 1)
                                                            TextsChanged(False)
                                                            Rnd = r.Next(1, 4)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy5C" & "," & killername & vicname & "," & "," & Team & "," & 5 & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else ' ally 
                                                            Rnd = r.Next(1, 3)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemy" & "," & killername & "," & vicname & "," & Team & "," & 5 & "," & Rnd)
                                                            TextsChanged(False)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemyC" & "," & killername & "," & vicname & "," & Team & "," & 5 & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & 5 & "," & Rnd)
                                                    TextsChanged(False)
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally5C" & "," & killername & "," & vicname & "," & Team & "," & 5 & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select

                                        Case 6 ' dominate
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner ' me
                                                            TextBox2.AppendText(etime & "," & "youkillstreak" & "," & killername & vicname & "," & "," & Team & "," & 6 & "," & 1)
                                                            TextsChanged(False)
                                                            Rnd = r.Next(1, 4)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy6C" & "," & killername & vicname & "," & "," & Team & "," & 6 & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else ' ally 
                                                            Rnd = r.Next(1, 3)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemy" & "," & killername & "," & vicname & "," & Team & "," & 6 & "," & Rnd)
                                                            TextsChanged(False)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemyC" & "," & killername & "," & vicname & "," & Team & "," & 6 & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & 6 & "," & Rnd)
                                                    TextsChanged(False)
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally6C" & "," & killername & "," & vicname & "," & Team & "," & 6 & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select

                                        Case 7 ' godlike
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner ' me
                                                            TextBox2.AppendText(etime & "," & "youkillstreak" & "," & killername & vicname & "," & "," & Team & "," & 7 & "," & 1)
                                                            TextsChanged(False)
                                                            Rnd = r.Next(1, 4)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy7C" & "," & killername & vicname & "," & "," & Team & "," & 7 & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else ' ally 
                                                            Rnd = r.Next(1, 3)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemy" & "," & killername & "," & vicname & "," & Team & "," & 7 & "," & Rnd)
                                                            TextsChanged(False)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemyC" & "," & killername & "," & vicname & "," & Team & "," & 7 & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & 7 & "," & Rnd)
                                                    TextsChanged(False)
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally7C" & "," & killername & "," & vicname & "," & Team & "," & 7 & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select

                                        Case Else ' legend
                                            Select Case Team
                                                Case myteam
                                                    Select Case killername
                                                        Case currentSummoner ' me
                                                            TextBox2.AppendText(etime & "," & "youkillstreak" & "," & killername & vicname & "," & "," & Team & "," & 8 & "," & 1)
                                                            TextsChanged(False)
                                                            Rnd = r.Next(1, 9)
                                                            TextBox2.AppendText(etime & "," & "youslayenemy8C" & "," & killername & vicname & "," & "," & Team & "," & 8 & "," & Rnd)
                                                            TextsChanged(True)
                                                        Case Else ' ally 
                                                            Rnd = r.Next(1, 3)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemy" & "," & killername & "," & vicname & "," & Team & "," & 8 & "," & Rnd)
                                                            TextsChanged(False)
                                                            TextBox2.AppendText(etime & "," & "allyslayenemyC" & "," & killername & "," & vicname & "," & Team & "," & 8 & "," & Rnd)
                                                            TextsChanged(True)
                                                    End Select
                                                Case Else
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & ename & "," & killername & "," & vicname & "," & Team & "," & 8 & "," & Rnd)
                                                    TextsChanged(False)
                                                    Rnd = r.Next(1, 4)
                                                    TextBox2.AppendText(etime & "," & "enemyslayally8C" & "," & killername & "," & vicname & "," & Team & "," & 8 & "," & Rnd)
                                                    TextsChanged(True)
                                            End Select
                                    End Select

                                    If killcount > 1 Then
                                        Rnd = r.Next(1, 3)
                                        TextBox2.AppendText(etime & "," & "shutdown" & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & Rnd)
                                        TextsChanged(False)
                                        If Team = myteam Then
                                            Rnd = r.Next(1, 4)
                                            TextBox2.AppendText(etime & "," & "shutdownC" & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & Rnd)
                                            TextsChanged(True)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If ename = "MinionsSpawning" Then
                        Dim Rnd As Integer = r.Next(1, 4)
                        TextBox2.AppendText(etime & "," & ename & "," & "0" & "," & "0" & "," & "0" & "," & 0 & "," & Rnd)
                        TextsChanged(False)
                    End If

                    If ename = "Multikill" Then
                        Dim killername As String = grid.Events(nnn).KillerName
                        Dim KillStreak As String = grid.Events(nnn).KillStreak
                        Dim killstreakM As String = ""

                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        ' 条件分岐1
                        Dim Rnd As Integer
                        Select Case Team
                            Case myteam
                                ' 条件分岐 Ally killstreak
                                Select Case KillStreak
                                    Case 2
                                        Rnd = r.Next(1, 6)
                                    Case 3
                                        Rnd = r.Next(1, 4)
                                    Case 4
                                        Rnd = r.Next(1, 4)
                                    Case 5
                                        Rnd = r.Next(1, 6)
                                End Select
                            Case Else
                                ' 条件分岐 Enemy killstreak
                                Select Case KillStreak
                                    Case 2
                                        Rnd = r.Next(1, 4)
                                    Case 3
                                        Rnd = r.Next(1, 4)
                                    Case 4
                                        Rnd = r.Next(1, 4)
                                    Case 5
                                        Rnd = r.Next(1, 5)
                                End Select
                        End Select
                        TextBox2.AppendText(etime & "," & ename & "," & killername & "," & "0" & "," & Team & "," & KillStreak & "," & Rnd)
                        TextsChanged(False)
                        Select Case Team
                            Case myteam
                                ' 条件分岐 Ally killstreak
                                Select Case KillStreak
                                    Case 2
                                        Rnd = r.Next(1, 4)
                                    Case 3
                                        Rnd = r.Next(1, 3)
                                    Case 4
                                        Rnd = r.Next(1, 4)
                                    Case 5
                                        Rnd = r.Next(1, 5)
                                End Select
                            Case Else
                                ' 条件分岐 Enemy killstreak
                                Select Case KillStreak
                                    Case 2
                                        Rnd = r.Next(1, 3)
                                    Case 3
                                        Rnd = r.Next(1, 3)
                                    Case 4
                                        Rnd = 1
                                    Case 5
                                        Rnd = r.Next(1, 4)
                                End Select
                        End Select
                        TextBox2.AppendText(etime & "," & "MultikillC" & "," & killername & "," & "0" & "," & Team & "," & KillStreak & "," & Rnd)
                        TextsChanged(True)
                    End If


                    If ename = "Ace" Then
                        Dim Team As String = grid.Events(nnn).AcingTeam
                        Dim Rnd As Integer = r.Next(1, 4)
                        TextBox2.AppendText(etime & "," & ename & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & Rnd)
                        TextsChanged(False)
                        If myteam = Team Then
                            Rnd = r.Next(1, 4)
                            TextBox2.AppendText(etime & "," & "Acec" & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & Rnd)
                            TextsChanged(True)
                        End If
                    End If

                    If ename = "InhibKilled" Then
                        Dim killername As String = grid.Events(nnn).KillerName
                        Dim turret_bin As String = grid.Events(nnn).InhibKilled
                        Dim turret As String = turret_bin.Substring(7, 1)
                        Dim team As String = "ORDER"
                        TextBox2.AppendText("### myteam : " & myteam & " / turret : " & turret_bin & ":" & turret & vbCrLf)

                        If turret = "C" Then
                            team = "CHAOS" 'ORDER
                        Else
                            team = "ORDER"
                        End If
                        Dim Rnd As Integer = r.Next(1, 3)
                        If team = myteam Then
                            TextBox2.AppendText(etime & "," & "badnews" & "," & killername & "," & "0" & "," & team & "," & 0 & "," & 1)
                            TextsChanged(False)
                        End If
                        Rnd = r.Next(1, 5)
                        TextBox2.AppendText(etime & "," & ename & "," & killername & "," & "0" & "," & team & "," & 0 & "," & Rnd)
                        TextsChanged(False)
                    End If

                    If ename = "InhibRespawned" Then
                        Dim turret_bin As String = grid.Events(nnn).InhibRespawned
                        Dim turret As String = turret_bin.Substring(7, 1)
                        Dim team As String = "ORDER"

                        If turret = "C" Then
                            team = "CHAOS"
                        Else
                            team = "ORDER"
                        End If
                        Dim Rnd As Integer = r.Next(1, 3)
                        If team <> myteam Then
                            TextBox2.AppendText(etime & "," & "badnews" & "," & "0" & "," & "0" & "," & team & "," & 0 & "," & 1)
                            TextsChanged(False)
                        End If
                        Rnd = r.Next(1, 3)
                        TextBox2.AppendText(etime & "," & ename & "," & "0" & "," & "0" & "," & team & "," & 0 & "," & 1)
                        TextsChanged(False)
                    End If

                    If ename = "InhibRespawningSoon" Then
                        Dim turret_bin As String = grid.Events(nnn).InhibRespawningSoon
                        'Inhib_TOrder_L1_P1_196971597
                        Dim turret As String = turret_bin.Substring(7, 1)
                        Dim team As String = "ORDER"

                        If turret = "C" Then
                            team = "CHAOS"
                        Else
                            team = "ORDER"
                        End If
                        Dim Rnd As Integer = r.Next(1, 3)
                        TextBox2.AppendText(etime & "," & ename & "," & "0" & "," & "0" & "," & team & "," & 0 & "," & Rnd)
                        TextsChanged(False)
                    End If

                    If ename = "TurretKilled" Then

                        Dim turret_bin As String = grid.Events(nnn).TurretKilled
                        Dim killername As String = grid.Events(nnn).KillerName
                        If turret_bin <> "Obelisk" And killername <> "Obelisk" Then
                            'If turret_bin <> "Obelisk" Then
                            'Turret_TChaos_L1_P3_671196429
                            Dim turret As String = turret_bin.Substring(8, 1)
                            TextBox2.AppendText("### myteam : " & myteam & " / turret : " & turret_bin & ":" & turret & vbCrLf)
                            Dim Assisterse As New List(Of String)
                            Dim assChamp As New List(Of String)
                            Dim aaa As Integer = 0
                            For Each Assi In grid.Events(nnn).Assisters
                                Assisterse.Add(grid.Events(nnn).Assisters(aaa))
                                Dim d0 As DataRow()
                                d0 = syain2.Select("sumname = '" + Assisterse(aaa) + "'")
                                For Each d As DataRow In d0
                                    assChamp.Add(d("chmname").ToString)
                                Next
                                aaa += 1
                            Next
                            'Dim killername As String = grid.Events(nnn).KillerName
                            'Dim d1 As DataRow()
                            'd1 = syain.Select("sumname = '" + killername + "'")
                            'Dim killerChamp As String = "Minion"
                            'For Each d As DataRow In d1
                            '    killerChamp = d("chmname").ToString
                            'Next
                            'Dim d3 As DataRow()
                            'd3 = syain.Select("sumname = '" + killername + "'")
                            'Dim Team As String = "Minion"
                            'For Each d As DataRow In d3
                            '    Team = d("team").ToString
                            'Next

                            'If turret = "1" Then
                            '    If myteam = "CHAOS" Then
                            '        Turret_s = "ORDER"
                            '    Else
                            '        Turret_s = "CHAOS"
                            '    End If
                            'Else
                            '    If myteam = "ORDER" Then
                            '        Turret_s = "CHAOS"
                            '    Else
                            '        Turret_s = "ORDER"
                            '    End If
                            'End If

                            Dim team As String = "ORDER"
                            If turret = "C" Then
                                team = "ORDER"
                            Else
                                team = "CHAOS"
                            End If


                            'If turret = "O" Then
                            '    If myteam = "CHAOS" Then
                            '        team = "CHAOS"
                            '    Else
                            '        team = "ORDER"
                            '    End If
                            'Else
                            '    If myteam = "CHAOS" Then
                            '        team = "CHAOS"
                            '    Else
                            '        team = "ORDER"
                            '    End If
                            'End If


                            Dim Rnd As Integer = r.Next(1, 4)
                            TextBox2.AppendText(etime & "," & ename & "," & killername & "," & turret_bin & "," & team & "," & turret & "," & Rnd)
                            TextsChanged(False)
                        Else
                            'TextBox2.AppendText("!!!!!!!!!!!!!!!!break2")
                            'nn += 1
                        End If
                    End If


                    If ename = "FirstBlood" Then
                        Dim killername As String = grid.Events(nnn).Recipient
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        TextBox2.AppendText(etime & "," & ename & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & 1)
                        TextsChanged(False)
                        Dim Rnd As Integer = r.Next(1, 3)
                        If myteam = Team Then
                            TextBox2.AppendText(etime & "," & "FirstBloodC" & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & Rnd)
                            TextsChanged(True)
                        End If
                    End If


                    If ename = "FirstBrick" Then
                        Dim killername As String = grid.Events(nnn).KillerName
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        TextBox2.AppendText(etime & "," & "FirstBrick" & "," & "0" & "," & "0" & "," & Team & "," & 0 & "," & 1)
                        TextsChanged(False)
                    End If

                    If ename = "GameEnd" Then
                        Dim Result As String = grid.Events(nnn).Result
                        Dim Rnd As Integer = r.Next(1, 6)
                        TextBox2.AppendText(etime & "," & ename & "," & Result & "," & "0" & "," & "0" & "," & 0 & "," & Rnd)
                        TextsChanged(False)


                        Dim jsonObj As String = JsonConvert.SerializeObject(grid)
                        TextBox7.Text = jsonObj
                        'TextBox4.AppendText(etime & ":" & ename & vbCrLf)
                        end_timer.Enabled = True
                    End If

                    If ename = "GameStart" Then
                        Timer1.Enabled = False
                        'Form2.onece()
                        'Form2.Timer1.Enabled = True
                        'Form2.Label1.Text = "on"
                        Timer3.Enabled = True
                        minion30.Enabled = True
                        s_flag = True
                        Dim Rnd As Integer = r.Next(1, 7)
                        TextBox2.AppendText(etime & "," & ename & "," & "0" & "," & "0" & "," & "0" & "," & 0 & "," & Rnd)
                        TextsChanged(False)
                        Dim blue = ""
                        Dim response2 As HttpResponse = Nothing
                        Try
                            Dim password As String = Form1.token
                            http.Request.Accept = HttpContentTypes.ApplicationJson
                            http.Request.SetBasicAuthentication("riot", password)
                            response2 = http.[Get]("https://127.0.0.1:2999/liveclientdata/playerlist")
                        Catch Exception As Exception
                            'Console.WriteLine("Error : No Response")
                        End Try
                        If response2.StatusCode <> System.Net.HttpStatusCode.OK Then

                        Else
                            syain.Rows.Clear()
                            syain2.Rows.Clear()
                            currentSummoner = Form1.TextBox5.Text
                            Dim grid2 = response2.DynamicBody
                            Dim teamscore As Integer = 0
                            Dim bscore As Integer = 0
                            Dim rscore As Integer = 0
                            Dim i As Integer = 0
                            For Each item In grid2
                                'Dim input As String = grid2(i).rawChampionName.ToString
                                'Dim parts As String() = input.Split("_"c)
                                'Dim chn As String = parts(parts.Length - 1) ' 最後の要素を取得
                                Dim cname As String = grid2(i).rawChampionName.ToString
                                Dim len As Integer = cname.Length
                                Dim last_n As Integer = cname.LastIndexOf("_") + 1
                                Dim nn0 As Integer = len - last_n
                                Dim chn As String = cname.Substring(last_n, nn0).Trim

                                If chn = "Name" Or chn = "" Then
                                    chn = "Teemo"
                                End If
                                'Console.WriteLine(chn)
                                'Dim chn As String = cname 'cname.Substring(last_n, nn)
                                'Dim sname As String = grid2(i).summonerName.ToString

                                Dim sname As String = grid2(i).riotIdGameName.ToString
                                'Dim snm() As String = sname.Split("#")
                                'Dim sname2 As String = snm(0).Trim
                                Dim team As String = (grid2(i).team.ToString).Trim()

                                'Dim team As String = (grid2(i).Team.ToString).Trim()
                                'syain.Rows.Add(chn, sname, team, 0)
                                'Form6.Panel1.Controls("chm" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & Form1.vernew & "\champimage\" & chn & ".png"), 24, 24)
                                'Form6.Panel1.Controls("Label" & i + 13).Text = sname
                                'Form2.sumload(i, sname)

                                If sname = currentSummoner Then
                                    myteam = team
                                    Button7.Text = myteam
                                    chn = Form1.Label16.Text
                                End If
                                syain.Rows.Add(chn, sname, team, 0)
                                syain2.Rows.Add(chn, sname, team, 0)
                                i += 1
                            Next
                            DataGridView1.DataSource = syain
                            DataGridView2.DataSource = syain2
                        End If
                        Timer1.Enabled = True
                    End If
                    'Dim jsonObj As String = JsonConvert.SerializeObject(grid)
                    'TextBox6.Text = jsonObj
                    'TextBox4.AppendText(etime & ":" & ename & vbCrLf)

                End If
            Catch ex As Exception
                If s_flag = True Then
                    nn -= 1
                End If
            End Try
        End If
    End Sub

    Private Sub KillTime()

    End Sub

    Private Sub stats2(nnn)
        'RichTextBox1.Focus()
        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        'Try
        'uu = "http://runereformedapi.azurewebsites.net/api/runes/Runepages"
        uu = "45.json"
        sr = webclient.OpenRead(uu)
        Dim srRead As New System.IO.StreamReader(sr)
        '内容をすべて読み込む
        Dim se As String = srRead.ReadToEnd()
        srRead.Close()
        Dim jsonObj As Object = JsonConvert.DeserializeObject(se)
        Try
            If jsonObj("Events")(nnn)("EventName") IsNot Nothing Then

                Dim etime_bin As Double = jsonObj("Events")(nnn)("EventTime")
                Dim etime As String = ""
                If etime_bin < 60 Then
                    etime = "00:" & Math.Round(etime_bin, 0, MidpointRounding.AwayFromZero).ToString("00")
                Else
                    Dim etime_h_bin As Double = etime_bin / 60
                    Dim etime_h As Double = Math.Floor(etime_h_bin)
                    Dim etime_m As Integer = etime_bin Mod 60
                    etime = etime_h.ToString("00") & ":" & etime_m.ToString("00")
                End If
                Dim ename As String = jsonObj("Events")(nnn)("EventName")
                Dim mess As String = ename
                If ename = "ChampionKill" Then
                    Dim Assisterse As New List(Of String)
                    Dim assChamp As New List(Of String)
                    Dim aaa As Integer = 0
                    For Each Assi In jsonObj("Events")(nnn)("Assisters")
                        Assisterse.Add(jsonObj("Events")(nnn)("Assisters")(aaa))
                        Dim d0 As DataRow()
                        d0 = syain2.Select("sumname = '" + Assisterse(aaa) + "'")
                        For Each d As DataRow In d0
                            assChamp.Add(d("chmname").ToString)
                        Next
                        aaa += 1
                    Next

                    Dim killername As String = jsonObj("Events")(nnn)("KillerName")
                    Dim d1 As DataRow()
                    d1 = syain2.Select("sumname = '" + killername + "'")
                    Dim killerChamp As String = "Minion"
                    For Each d As DataRow In d1
                        killerChamp = d("chmname").ToString
                    Next
                    Dim vicname As String = jsonObj("Events")(nnn)("VictimName")
                    Dim d2 As DataRow()
                    d2 = syain2.Select("sumname = '" + vicname + "'")
                    Dim VicChamp As String = "Minion"
                    For Each d As DataRow In d2
                        VicChamp = d("chmname").ToString
                    Next
                    Dim d3 As DataRow()
                    d3 = syain2.Select("sumname = '" + killername + "'")
                    Dim Team As String = "Minion"
                    For Each d As DataRow In d3
                        Team = d("team").ToString
                    Next
                    kill(etime, Team, killername, vicname, killerChamp, VicChamp, Assisterse, assChamp)
                ElseIf ename = "Multikill" Then
                    Dim killername As String = jsonObj("Events")(nnn)("KillerName")
                    Dim KillStreak As String = jsonObj("Events")(nnn)("KillStreak")
                    Dim killstreakM As String = ""
                    Dim cor As Color = Color.Blue
                    If KillStreak = 2 Then
                        killstreakM = "Double"
                        cor = Color.Yellow
                    End If
                    If KillStreak = 3 Then
                        killstreakM = "Triple"
                        cor = Color.Yellow
                    End If
                    If KillStreak = 4 Then
                        killstreakM = "Quadra"
                        cor = Color.Yellow
                    End If
                    If KillStreak = 5 Then
                        killstreakM = "Penta"
                        cor = Color.Yellow
                    End If

                    Dim d3 As DataRow()
                    d3 = syain2.Select("sumname = '" + killername + "'")
                    Dim Team As String = "Minion"
                    For Each d As DataRow In d3
                        Team = d("team").ToString
                    Next
                    ks(Team, killstreakM)

                ElseIf ename = "FirstBlood" Then
                    Dim killername As String = jsonObj("Events")(nnn)("Recipient")
                    Dim d3 As DataRow()
                    d3 = syain2.Select("sumname = '" + killername + "'")
                    Dim Team As String = "Minion"
                    For Each d As DataRow In d3
                        Team = d("team").ToString
                    Next
                    fb(Team)

                ElseIf ename = "Ace" Then

                    Dim killername As String = jsonObj("Events")(nnn)("Acer")
                    Dim d3 As DataRow()
                    d3 = syain2.Select("sumname = '" + killername + "'")
                    Dim Team As String = "Minion"
                    For Each d As DataRow In d3
                        Team = d("team").ToString
                    Next
                    If Team = "ORDER" Then
                        score += 1
                        RichTextBox1.SelectionColor = blue
                        RichTextBox1.AppendText(" ACE ")
                    Else
                        score -= 1
                        RichTextBox1.SelectionColor = red
                        RichTextBox1.AppendText(" ACE ")
                    End If
                ElseIf ename = "InhibKilled" Then
                    Dim inhib_bin As String = jsonObj("Events")(nnn)("InhibKilled")
                    Dim inhib As String = "inhibiter"
                    Dim Assisterse As New List(Of String)
                    Dim assChamp As New List(Of String)
                    Dim aaa As Integer = 0
                    For Each Assi In jsonObj("Events")(nnn)("Assisters")
                        Assisterse.Add(jsonObj("Events")(nnn)("Assisters")(aaa))
                        Dim d0 As DataRow()
                        d0 = syain2.Select("sumname = '" + Assisterse(aaa) + "'")
                        For Each d As DataRow In d0
                            assChamp.Add(d("chmname").ToString)
                        Next
                        aaa += 1
                    Next

                    Dim killername As String = jsonObj("Events")(nnn)("KillerName")
                    Dim d1 As DataRow()
                    d1 = syain2.Select("sumname = '" + killername + "'")
                    Dim killerChamp As String = "Minion"
                    For Each d As DataRow In d1
                        killerChamp = d("chmname").ToString
                    Next
                    Dim d3 As DataRow()
                    d3 = syain2.Select("sumname = '" + killername + "'")
                    Dim Team As String = "Minion"
                    For Each d As DataRow In d3
                        Team = d("team").ToString
                    Next
                    ik(etime, Team, killername, killerChamp, inhib, Assisterse, assChamp)

                ElseIf ename = "TurretKilled" Then
                    Dim turret_bin As String = jsonObj("Events")(nnn)("TurretKilled")
                    Dim turret As String = turret_bin
                    Dim killername As String = jsonObj("Events")(nnn)("KillerName")
                    If turret_bin <> "Obelisk" And killername <> "Obelisk" Then
                        Dim Assisterse As New List(Of String)
                        Dim assChamp As New List(Of String)
                        Dim aaa As Integer = 0
                        For Each Assi In jsonObj("Events")(nnn)("Assisters")
                            Assisterse.Add(jsonObj("Events")(nnn)("Assisters")(aaa))
                            Dim d0 As DataRow()
                            d0 = syain2.Select("sumname = '" + Assisterse(aaa) + "'")
                            For Each d As DataRow In d0
                                assChamp.Add(d("chmname").ToString)
                            Next
                            aaa += 1
                        Next
                        'Dim killername As String = jsonObj("Events")(nnn)("KillerName")
                        Dim d1 As DataRow()
                        d1 = syain2.Select("sumname = '" + killername + "'")
                        Dim killerChamp As String = "Minion"
                        For Each d As DataRow In d1
                            killerChamp = d("chmname").ToString
                        Next
                        Dim d3 As DataRow()
                        d3 = syain2.Select("sumname = '" + killername + "'")
                        Dim Team As String = "Minion"
                        For Each d As DataRow In d3
                            Team = d("team").ToString
                        Next
                        tk(etime, Team, killername, killerChamp, turret, Assisterse, assChamp)
                    Else
                        'TextBox2.AppendText("!!!!!!!!!!!!!!!!break2")
                        'nns -= 1
                    End If


                ElseIf ename = "FirstBrick" Then

                    Dim killername As String = jsonObj("Events")(nnn)("KillerName")
                    Dim d3 As DataRow()
                    d3 = syain2.Select("sumname = '" + killername + "'")
                    Dim Team As String = "Minion"
                    For Each d As DataRow In d3
                        Team = d("team").ToString
                    Next
                    If Team = myteam Then
                        score += 1
                        RichTextBox1.SelectionColor = blue
                        RichTextBox1.AppendText(" FirstBrick ")
                    Else
                        score -= 1
                        RichTextBox1.SelectionColor = red
                        RichTextBox1.AppendText(" FirstBrick ")
                    End If
                Else
                    RichTextBox1.SelectionColor = Color.White
                    RichTextBox1.AppendText(vbCrLf & etime & " : " & ename)
                End If
                If ename = "GameEnd" Then
                    score = 0
                End If
            End If
            scp.Text = score
            Form5.Panel2.Width = 100 + score
            RichTextBox1.Focus()
        Catch ex As Exception
            RichTextBox1.SelectionColor = Color.White
            Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_b.png",
                                                   System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                Clipboard.SetDataObject(bmp, True)
            End Using
            Dim iData1 As IDataObject = Clipboard.GetDataObject()
            If iData1.GetDataPresent(DataFormats.Bitmap) Then
                RichTextBox1.Paste()
            End If
            nns -= 1
        End Try
    End Sub

    Private Sub fb(team As String)
        If team = myteam Then
            score += 1
            RichTextBox1.SelectionColor = blue
            RichTextBox1.AppendText(" FirstBlood ")
        Else
            score -= 1
            RichTextBox1.SelectionColor = red
            RichTextBox1.AppendText(" FirstBlood ")
        End If
    End Sub

    Private Sub ks(team As String, killstreakM As String)
        Dim cor As Color = Color.LightPink
        If team = myteam Then

            score += 1
            RichTextBox1.SelectionColor = blue
            RichTextBox1.AppendText(" ")
            RichTextBox1.AppendText(killstreakM & "Kill ")

        Else

            score -= 1
            RichTextBox1.SelectionColor = red
            RichTextBox1.AppendText(" ")
            RichTextBox1.AppendText(killstreakM & "Kill ")

        End If
    End Sub

    Private Sub ik(etime As String, team As String, killername As String, killerChamp As String, inhib As String, assisterse As List(Of String), assChamp As List(Of String))
        RichTextBox1.SelectionColor = Color.White
        RichTextBox1.AppendText(vbCrLf & etime & " : ")
        If killerChamp = "Minion" Then
            If inhib = myteam Then
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_b.png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = blue
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-inhib-r.png",
                                            System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData5 As IDataObject = Clipboard.GetDataObject()
                If iData5.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
            Else
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_r.png",
                                       System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = red
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-inhib-b.png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData6 As IDataObject = Clipboard.GetDataObject()
                If iData6.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            End If
        Else
            If team = myteam Then
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = blue
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-inhib-r.png",
                                            System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData5 As IDataObject = Clipboard.GetDataObject()
                If iData5.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            Else
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = red
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-inhib-b.png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData6 As IDataObject = Clipboard.GetDataObject()
                If iData6.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            End If
        End If
        'If team = myteam Then
        '    score += 1
        '    RichTextBox1.SelectionColor = Color.White
        '    RichTextBox1.AppendText(vbCrLf & etime & " : ")
        '    If team = "Minion" Then
        '        Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_b.png",
        '                                 System.IO.FileMode.Open, System.IO.FileAccess.Read)
        '            Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
        '            bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
        '            Clipboard.SetDataObject(bmp, True)
        '        End Using
        '    Else
        '        Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
        '            Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
        '            bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
        '            Clipboard.SetDataObject(bmp, True)
        '        End Using
        '    End If

        '    Dim iData3 As IDataObject = Clipboard.GetDataObject()
        '    If iData3.GetDataPresent(DataFormats.Bitmap) Then
        '        RichTextBox1.Paste()
        '    End If
        '    Clipboard.Clear()

        '    If assChamp.Count > 0 Then
        '        Ass(team, assisterse, assChamp)
        '    End If
        '    RichTextBox1.SelectionColor = blue
        '    RichTextBox1.AppendText(" destroyed ")
        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-tower-r.png",
        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
        '        bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
        '        Clipboard.SetDataObject(bmp, True)
        '    End Using
        '    Dim iData5 As IDataObject = Clipboard.GetDataObject()
        '    If iData5.GetDataPresent(DataFormats.Bitmap) Then
        '        RichTextBox1.Paste()
        '    End If

        'Else
        '    score -= 1
        '    RichTextBox1.SelectionColor = Color.White
        '    RichTextBox1.AppendText(vbCrLf & etime & " : ")
        '    If team = "Minion" Then
        '        Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_r.png",
        '                                 System.IO.FileMode.Open, System.IO.FileAccess.Read)
        '            Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
        '            bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
        '            Clipboard.SetDataObject(bmp, True)
        '        End Using
        '    Else
        '        Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
        '                                         System.IO.FileMode.Open, System.IO.FileAccess.Read)
        '            Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
        '            bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
        '            Clipboard.SetDataObject(bmp, True)
        '        End Using
        '    End If

        '    Dim iData3 As IDataObject = Clipboard.GetDataObject()
        '    If iData3.GetDataPresent(DataFormats.Bitmap) Then
        '        RichTextBox1.Paste()
        '    End If
        '    Clipboard.Clear()

        '    If assChamp.Count > 0 Then
        '        Ass(team, assisterse, assChamp)
        '    End If
        '    RichTextBox1.SelectionColor = red
        '    RichTextBox1.AppendText(" destroyed ")
        '    Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-tower-b.png",
        '                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
        '        Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
        '        bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
        '        Clipboard.SetDataObject(bmp, True)
        '    End Using
        '    Dim iData5 As IDataObject = Clipboard.GetDataObject()
        '    If iData5.GetDataPresent(DataFormats.Bitmap) Then
        '        RichTextBox1.Paste()
        '    End If
        'End If
    End Sub


    Private Sub tk(etime As String, team As String, killername As String, killerChamp As String, turret As String, assisterse As List(Of String), assChamp As List(Of String))
        RichTextBox1.SelectionColor = Color.White
        RichTextBox1.AppendText(vbCrLf & etime & " : ")
        If killerChamp = "Minion" Then
            If turret = myteam Then
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_b.png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = blue
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-tower-r.png",
                                            System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData5 As IDataObject = Clipboard.GetDataObject()
                If iData5.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            Else
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\minion_r.png",
                                       System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = red
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-tower-b.png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData6 As IDataObject = Clipboard.GetDataObject()
                If iData6.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            End If
        Else
            If team = myteam Then
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = blue
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-tower-r.png",
                                            System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData5 As IDataObject = Clipboard.GetDataObject()
                If iData5.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            Else
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
                If assChamp.Count > 0 Then
                    Ass(team, assisterse, assChamp)
                End If
                RichTextBox1.SelectionColor = red
                RichTextBox1.AppendText(" destroyed ")
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\icon-tower-b.png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData6 As IDataObject = Clipboard.GetDataObject()
                If iData6.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            End If
        End If
    End Sub

    Private Sub kill(etime As String, team As String, killername As String, Vicname As String, killerChamp As String, VicChamp As String, assisterse As List(Of String), assChamp As List(Of String))
        TextBox10.Text = "team : " & team & " myteam : " & myteam
        If team = "Minion" And assisterse.Count = 0 Then
            RichTextBox1.SelectionColor = Color.White
            RichTextBox1.AppendText(vbCrLf & etime & " : ")
            Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & VicChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                Clipboard.SetDataObject(bmp, True)
            End Using
            Dim iData2 As IDataObject = Clipboard.GetDataObject()
            If iData2.GetDataPresent(DataFormats.Bitmap) Then
                RichTextBox1.Paste()
            End If
            Clipboard.Clear()
            RichTextBox1.SelectionColor = Color.LightGreen
            RichTextBox1.AppendText(" Executed  ")
            Exit Sub
        End If
        If team = myteam Then
            If teamscore > 0 Then
                score += 1
            Else
                score += 2
            End If

            RichTextBox1.SelectionColor = Color.White
            RichTextBox1.AppendText(vbCrLf & etime & " : ")
            Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                Clipboard.SetDataObject(bmp, True)
            End Using
            Dim iData1 As IDataObject = Clipboard.GetDataObject()
            If iData1.GetDataPresent(DataFormats.Bitmap) Then
                RichTextBox1.Paste()
            End If
            Clipboard.Clear()

            If assChamp.Count > 0 Then
                Ass(team, assisterse, assChamp)
            End If

            RichTextBox1.SelectionColor = blue
            RichTextBox1.AppendText(" killed  ")
            Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & VicChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                Clipboard.SetDataObject(bmp, True)
            End Using
            Dim iData2 As IDataObject = Clipboard.GetDataObject()
            If iData2.GetDataPresent(DataFormats.Bitmap) Then
                RichTextBox1.Paste()
            End If
            Clipboard.Clear()

        Else
            If teamscore < 0 Then
                score -= 1
            Else
                score -= 2
            End If
            RichTextBox1.SelectionColor = Color.White
            RichTextBox1.AppendText(vbCrLf & etime & " : ")
            Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & killerChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                Clipboard.SetDataObject(bmp, True)
            End Using
            Dim iData3 As IDataObject = Clipboard.GetDataObject()
            If iData3.GetDataPresent(DataFormats.Bitmap) Then
                RichTextBox1.Paste()
            End If
            Clipboard.Clear()

            If assChamp.Count > 0 Then
                Ass(team, assisterse, assChamp)
            End If
            RichTextBox1.SelectionColor = red
            RichTextBox1.AppendText(" killed ")
            Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & VicChamp & ".png",
                                        System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                bmp = New Bitmap(bmp, CInt(siz), CInt(siz))
                Clipboard.SetDataObject(bmp, True)
            End Using
            Dim iData4 As IDataObject = Clipboard.GetDataObject()
            If iData4.GetDataPresent(DataFormats.Bitmap) Then
                RichTextBox1.Paste()
            End If
            Clipboard.Clear()

        End If
        score += teamscore
    End Sub

    Private Sub Ass(team As String, assisterse As List(Of String), assChamp As List(Of String))
        Dim killco As Color
        Dim vicco As Color
        Dim minion As String = ""
        Dim pcount As Integer = assisterse.Count
        If team = myteam Then
            score += pcount
            killco = blue
            vicco = red
            minion = "minion_b"
        Else
            score += pcount * -1
            killco = red
            vicco = blue
            minion = "minion_r"
        End If
        'RichTextBox1.SelectionColor = Color.White
        'RichTextBox1.AppendText(" Assister: ")
        Dim aa As Integer = 0
        For Each assister In assisterse
            RichTextBox1.SelectionColor = killco
            Try
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & patch & "\champimage\" & assChamp(aa) & ".png",
                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(sizz), CInt(sizz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            Catch
                Using fs As System.IO.FileStream = New System.IO.FileStream("images\" & minion & ".png",
                                System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    Dim bmp As Bitmap = New Bitmap(System.Drawing.Image.FromStream(fs))
                    bmp = New Bitmap(bmp, CInt(sizz), CInt(sizz))
                    Clipboard.SetDataObject(bmp, True)
                End Using
                Dim iData3 As IDataObject = Clipboard.GetDataObject()
                If iData3.GetDataPresent(DataFormats.Bitmap) Then
                    RichTextBox1.Paste()
                End If
                Clipboard.Clear()
            End Try

            RichTextBox1.SelectionColor = killco
            'RichTextBox1.AppendText(Assisterse(aa))

            aa += 1
        Next
    End Sub

    Dim s_flag As Boolean = False

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        stats(nn)

        If s_flag Then
            nn += 1
        End If
        yy += 1
        statsi(nns)
        yy_bin = (yy * kan) - kai
        'If yy_bin <= 0 Then
        '    yy_bin = 0
        'End If
        nns += 1
        'Label2.Text = nn & ":" & yy & ":" & yy_bin
        Dim pt As Point
        pt.Y = yy_bin
        SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, pt)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Enabled = False
        'Form2.Timer1.Enabled = False
        'Form2.blue = ""
        'Form2.Label1.Text = "off"
        Form1.Timer2.Enabled = False
        'Form1.onece = False
        'Form1.L = False
        'Form1.Timer1.Enabled = True
        'Form1.Button10.PerformClick()
        nn = 0
        nns = 0
        'Dim jsonObj As String = JsonConvert.SerializeObject(grid)
        'TextBox1.Text = jsonObj
        'Form2.blue = ""
        syain.Rows.Clear()
        syain2.Rows.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Clipboard.SetText(TextBox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim tim As String = DateTime.Now.ToString("yyyyMMddhhmmss")
        Form1.ConvertDataTableToCsv(syain, "syain.csv", True, False)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        syain.Clear()
        Dim csvDir As String = "." ' "C:\"
        'CSVファイルの名前
        Dim csvFileName As String = "syain.csv"

        '接続文字列
        Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As New System.Data.OleDb.OleDbConnection(conString)

        Dim commText As String = "SELECT * FROM [" + csvFileName + "]"
        Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)

        da.Fill(syain)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'RichTextBox1.Text = ""
        stats(nns)
        statsi(nns)
        yy += 1
        'statsi(nns)
        yy_bin = (yy * kan) - kai
        'If yy_bin <= 0 Then
        '    yy_bin = 0
        'End If
        nns += 1
        'Label2.Text = nn & ":" & yy & ":" & yy_bin
        Dim pt As Point
        pt.Y = yy_bin
        SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, pt)

        'stats2(nn)
        's_flag = True
        'If s_flag Then
        '    nn += 1
        'End If
        'yy += 1
        'statsi(nns)
        'yy_bin = (yy * kan) - kai
        ''If yy_bin <= 0 Then
        ''    yy_bin = 0
        ''End If
        'nns += 1
        ''Label2.Text = nn & ":" & yy & ":" & yy_bin
        'Dim pt As Point
        'pt.Y = yy_bin
        'SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, pt)






    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        statsi(nns)
        nns += 1
    End Sub

    'Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    '    If Timer1.Enabled = False Then
    '        Timer1.Enabled = True
    '        Label1.Text = "ON"
    '        theme_end()
    '        theme_start("butchersbridge\renata", 6000000)
    '        'theme_start("sounds\Bilgewater", 6000000)
    '    Else
    '        Timer1.Enabled = False
    '        Label1.Text = "OFF"
    '        theme_end()
    '        nn = 0
    '    End If
    'End Sub


    Private Sub mysound_Tick(sender As Object, e As EventArgs) Handles mysoundtimer.Tick
        mysoundtimer.Enabled = False
        'If Tabb = 96 Then
        Dim cmd As String
        '再生しているWAVEを停止する
        cmd = "stop " + mysound
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        '  閉じる
        cmd = "close " + mysound
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        'End If
        Dim lines As New List(Of String)(TextBox3.Lines)
        lines.RemoveAt(0)
        TextBox3.Text = String.Join(vbCrLf, lines)
    End Sub

    Private Sub Theme_Tick(sender As Object, e As EventArgs) Handles themetimer.Tick
        themetimer.Enabled = False
        'If Tabb = 96 Then
        Dim cmd As String
        '再生しているWAVEを停止する
        cmd = "stop " + theme
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        '  閉じる
        cmd = "close " + theme
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        'Console.WriteLine(chst)
        'If chst = True Then
        '    chst = False
        '    Dim cmdd = "play " + theme
        '    'Dim leng As Integer = 6000000
        '    'Dim Rnd As String = ""
        '    Dim sond As String = "starguardian_jp\ambient.mp3"
        '    If mode = 2 Then
        '        sond = "butchersbridge\butcher's_bridge_early.mp3"
        '    End If
        '    Dim fileName As String = sond
        '    cmdd = "open """ + fileName + """ type mpegvideo alias " + theme
        '    If mciSendString(cmdd, Nothing, 0, IntPtr.Zero) <> 0 Then
        '        Return
        '    End If
        '    cmdd = "play " + theme
        '    mciSendString(cmdd, Nothing, 0, IntPtr.Zero)
        '    'leng = Integer.Parse(TextBox4.Text)
        '    themetimer.Interval = 176000
        '    themetimer.Enabled = True
        '    'Console.WriteLine(chst)
        'End If

    End Sub
    Dim CC As String
    Private Sub TextsChanged(C As Boolean)
        If C = True Then
            CC = TextBox2.Lines.Last()
            TextBox8.Text = CC
            Timer4.Enabled = True
        ElseIf C = False Then
            TextBox3.AppendText(TextBox2.Lines.Last() & vbCrLf)
        End If
        TextBox2.AppendText(vbCrLf)
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        If TextBox3.Lines.Length < 3 Then ' 20221102
            TextBox3.AppendText(CC & vbCrLf)
            TextBox8.Text = ""
            Timer4.Enabled = False
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Lines.Length > 0 Then
            Dim cmd As String
            Dim arr1(7) As String
            Dim events As String
            Dim champ As String
            Dim vicChamp As String
            Dim team As String
            Dim killstreak As String
            Dim Rnum As Integer


            arr1 = TextBox3.Lines.First.Split(",")

            events = arr1(1)
            champ = arr1(2)
            vicChamp = arr1(3)
            team = arr1(4)
            killstreak = arr1(5)
            Rnum = arr1(6)
            Dim Rnd As String = "youhavebeenslayc5"
            Dim leng As Integer = 1800
            If mode = 0 Then
                Select Case events
                    Case "FirstBlood"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Rnd = "firstbloodm1"
                                leng = 1100
                            Case Else
                                Rnd = "firstbloode1"
                                leng = 1200
                        End Select

                    Case "FirstBloodC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "firstbloodmc1"
                                leng = 1700
                            Case 2
                                Rnd = "firstbloodmc2"
                                leng = 1300
                        End Select

                    Case "MinionsSpawning"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "minionspawn1"
                                leng = 2000
                            Case 2
                                Rnd = "minionspawn2"
                                leng = 2000
                            Case 3
                                Rnd = "minionspawn3"
                                leng = 3000
                            Case 4
                                Rnd = "minionspawn4"
                                leng = 3100
                        End Select

                    Case "youslayenemy1C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "enemysc1"
                                leng = 2100
                            Case 2
                                Rnd = "enemysc2"
                                leng = 1500
                            Case 3
                                Rnd = "enemysc3"
                                leng = 1500
                            Case 4
                                Rnd = "enemysc4"
                                leng = 2000
                        End Select

                    Case "enemyslayally1C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "allysc1"
                                leng = 2000
                            Case 2
                                Rnd = "allysc2"
                                leng = 2300
                            Case 3
                                Rnd = "allysc3"
                                leng = 2200
                        End Select


                    Case "youhaveslayenemy1C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhaveslayenemyc1"
                                leng = 1000
                            Case 2
                                Rnd = "youhaveslayenemyc2"
                                leng = 1400
                            Case 3
                                Rnd = "youhaveslayenemyc3"
                                leng = 1300
                            Case 4
                                Rnd = "youhaveslayenemyc4"
                                leng = 1450
                            Case 5
                                Rnd = "youhaveslayenemyc5"
                                leng = 900
                            Case 6
                                Rnd = "youhaveslayenemyc6"
                                leng = 1800
                            Case 7
                                Rnd = "youhaveslayenemyc7"
                                leng = 800
                        End Select

                    Case "youslayenemy3C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally3_C1"
                                leng = 1200
                            Case 2
                                Rnd = "ally3_C2"
                                leng = 1400
                            Case 3
                                Rnd = "ally3_C3"
                                leng = 1650
                        End Select

                    Case "enemyslayally3C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy3_C1"
                        leng = 2000
                        'End Select

                    Case "youslayenemy4C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally4_C1"
                                leng = 2000
                            Case 2
                                Rnd = "ally4_C2"
                                leng = 1900
                            Case 3
                                Rnd = "ally4_C3"
                                leng = 1300
                        End Select

                    Case "enemyslayally4C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy4_C1"
                        leng = 1900
                        'End Select

                    Case "youslayenemy5C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally5_C1"
                                leng = 2000
                            Case 2
                                Rnd = "ally5_C2"
                                leng = 2550
                            Case 3
                                Rnd = "ally5_C3"
                                leng = 2500
                        End Select

                    Case "enemyslayally5C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy5_C1"
                        leng = 2100
                        'End Select

                    Case "youslayenemy6C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally6_C1"
                                leng = 1000
                            Case 2
                                Rnd = "ally6_C2"
                                leng = 3200
                            Case 3
                                Rnd = "ally6_C3"
                                leng = 2000
                        End Select

                    Case "enemyslayally6C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy6_C1"
                        leng = 3200
                        'End Select

                    Case "youslayenemy7C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally7_C1"
                                leng = 2000
                            Case 2
                                Rnd = "ally7_C2"
                                leng = 2450
                            Case 3
                                Rnd = "ally7_C3"
                                leng = 2200
                        End Select

                    Case "enemyslayally7C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy7_C1"
                        leng = 2700
                        'End Select

                    Case "youslayenemy8C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally8_C1"
                                leng = 2550
                            Case 2
                                Rnd = "ally8_C2"
                                leng = 3800
                            Case 3
                                Rnd = "ally8_C3"
                                leng = 3300
                            Case 4
                                Rnd = "ally8_C4"
                                leng = 3400
                            Case 5
                                Rnd = "ally8_C5"
                                leng = 1900
                            Case 6
                                Rnd = "ally8_C6"
                                leng = 3800
                        End Select

                    Case "enemyslayally8C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "enemy8_C1"
                                leng = 2550
                            Case 2
                                Rnd = "enemy8_C2"
                                leng = 3000
                            Case 3
                                Rnd = "enemy8_C3"
                                leng = 1600
                        End Select

                    Case "youkillstreak"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "you3_1"
                                leng = 1800
                            Case 4
                                Rnd = "you4_1"
                                leng = 2000
                            Case 5
                                Rnd = "you5_1"
                                leng = 1700
                            Case 6
                                Rnd = "you6_1"
                                leng = 1800
                            Case 7
                                Rnd = "you7_1"
                                leng = 1800
                            Case Else
                                Rnd = "you8_1"
                                leng = 1800
                        End Select

                    Case "allyslayenemy"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "allys3_1"
                                leng = 1800
                            Case 4
                                Rnd = "allys4_1"
                                leng = 1800
                            Case 5
                                Rnd = "allys5_1"
                                leng = 2000
                            Case 6
                                Rnd = "allys6_1"
                                leng = 1900
                            Case 7
                                Rnd = "allys7_1"
                                leng = 1800
                            Case 8
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allys8_1"
                                        leng = 1900
                                    Case 2
                                        Rnd = "allys8_2"
                                        leng = 3800
                                End Select
                        End Select

                    Case "allyslayenemyC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "allys3_C1"
                                leng = 2550
                            Case 4
                                Rnd = "allys4_C1"
                                leng = 3300
                            Case 5
                                Rnd = "allys5_C1"
                                leng = 2050
                            Case 6
                                Rnd = "allys6_C1"
                                leng = 2050
                            Case 7
                                Rnd = "allys7_C1"
                                leng = 2200
                            Case 8
                                Rnd = "allys8_C1"
                                leng = 2800
                        End Select

                    Case "ChampionKill" ' CampionKill START $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 0, 1, 2
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "youhaveslayenemy1"
                                                        leng = 1400
                                                    Case 2
                                                        Rnd = "youhaveslayenemy2"
                                                        leng = 1400
                                                    Case 3
                                                        Rnd = "youhaveslayenemy1"
                                                        leng = 1400
                                                End Select
                                            Case Else
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "enemys1"
                                                        leng = 1400
                                                    Case 2
                                                        Rnd = "enemys2"
                                                        leng = 1600
                                                    Case 3
                                                        Rnd = "enemys3"
                                                        leng = 1400
                                                End Select
                                        End Select
                                    Case 3
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally3_1"
                                                        leng = 1200
                                                    Case 2
                                                        Rnd = "ally3_2"
                                                        leng = 1300
                                                    Case 3
                                                        Rnd = "ally3_3"
                                                        leng = 1500
                                                End Select
                                            Case Else
                                                Rnd = "allys3_1"
                                                leng = 1800
                                        End Select

                                    Case 4
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally4_1"
                                                        leng = 1100
                                                    Case 2
                                                        Rnd = "ally4_2"
                                                        leng = 1100
                                                    Case 3
                                                        Rnd = "ally4_3"
                                                        leng = 1200
                                                End Select
                                            Case Else
                                                Rnd = "allys4_1"
                                                leng = 1600
                                        End Select

                                    Case 5
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally5_1"
                                                        leng = 1400
                                                    Case 2
                                                        Rnd = "ally5_2"
                                                        leng = 1200
                                                    Case 3
                                                        Rnd = "ally5_3"
                                                        leng = 1400
                                                End Select
                                            Case Else
                                                Rnd = "allys5_1"
                                                leng = 2000
                                        End Select

                                    Case 6
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally6_1"
                                                        leng = 1200
                                                    Case 2
                                                        Rnd = "ally6_2"
                                                        leng = 1200
                                                    Case 3
                                                        Rnd = "ally6_3"
                                                        leng = 1600
                                                End Select
                                            Case Else
                                                Rnd = "allys6_1"
                                                leng = 1900
                                        End Select

                                    Case 7
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally7_1"
                                                        leng = 1200
                                                    Case 2
                                                        Rnd = "ally7_2"
                                                        leng = 1300
                                                    Case 3
                                                        Rnd = "ally7_3"
                                                        leng = 1800
                                                End Select
                                            Case Else
                                                Rnd = "allys7_1"
                                                leng = 1800
                                        End Select

                                    Case Else
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally8_1"
                                                        leng = 1450
                                                    Case 2
                                                        Rnd = "ally8_2"
                                                        leng = 1300
                                                    Case 3
                                                        Rnd = "ally8_3"
                                                        leng = 1600
                                                End Select
                                            Case Else
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "allys8_1"
                                                        leng = 1800
                                                    Case 2
                                                        Rnd = "allys8_2"
                                                        leng = 1800
                                                    Case 3
                                                        Rnd = "allys8_1"
                                                        leng = 1800
                                                End Select
                                        End Select
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 0, 1, 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allys1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "allys2"
                                                leng = 1500
                                            Case 3
                                                Rnd = "allys3"
                                                leng = 1500
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy3_1"
                                                leng = 1750
                                            Case 2
                                                Rnd = "enemy3_2"
                                                leng = 1650
                                            Case 3
                                                Rnd = "enemy3_3"
                                                leng = 1650
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy4_1"
                                                leng = 1400
                                            Case 2
                                                Rnd = "enemy4_2"
                                                leng = 1400
                                            Case 3
                                                Rnd = "enemy4_3"
                                                leng = 1300
                                        End Select
                                    Case 5
                                        Rnd = "enemy5_1"
                                        leng = 1450
                                    Case 6
                                        Rnd = "enemy6_1"
                                        leng = 1450
                                    Case 7
                                        Rnd = "enemy7_1"
                                        leng = 1450
                                    Case Else
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy8_1"
                                                leng = 1450
                                            Case 2
                                                Rnd = "enemy8_2"
                                                leng = 1450
                                        End Select
                                End Select

                        End Select '----ChampionKill End $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                    Case "Multikill"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydoublekill1"
                                                leng = 1100
                                            Case 2
                                                Rnd = "allydoublekill2"
                                                leng = 1100
                                            Case 3
                                                Rnd = "allydoublekill3"
                                                leng = 900
                                            Case 4
                                                Rnd = "allydoublekill4"
                                                leng = 900
                                            Case 5
                                                Rnd = "allydoublekill5"
                                                leng = 2000
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allytriplekill1"
                                                leng = 1000
                                            Case 2
                                                Rnd = "allytriplekill2"
                                                leng = 1100
                                            Case 3
                                                Rnd = "allytriplekill3"
                                                leng = 1100
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allyquadra1"
                                                leng = 1200
                                            Case 2
                                                Rnd = "allyquadra2"
                                                leng = 1200
                                            Case 3
                                                Rnd = "allyquadra3"
                                                leng = 1200
                                        End Select
                                    Case 5
                                        'Select Case Rnum
                                        '    Case 1
                                        '        Rnd = "1"
                                        '        leng = 2000
                                        '    Case 2
                                        '        Rnd = "2"
                                        '        leng = 2000
                                        '    Case 3
                                        '        Rnd = "3"
                                        '        leng = 2000
                                        '    Case 4
                                        '        Rnd = "4"
                                        '        leng = 2000
                                        '    Case 5
                                        '        Rnd = "5"
                                        '        leng = 2000
                                        'End Select
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allypenta1"
                                                leng = 1100
                                            Case 2
                                                Rnd = "allypenta2"
                                                leng = 1600
                                            Case 3
                                                Rnd = "allypenta3"
                                                leng = 1400
                                            Case 4
                                                Rnd = "allypenta4"
                                                leng = 2800
                                            Case 5
                                                Rnd = "allypenta5"
                                                leng = 1600
                                        End Select
                                    Case Else
                                        Rnd = "allypenta1"
                                        leng = 1300
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemydoublekill1"
                                                leng = 1300
                                            Case 2
                                                Rnd = "enemydoublekill2"
                                                leng = 1300
                                            Case 3
                                                Rnd = "enemydoublekill3"
                                                leng = 1300
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemytripplekill1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "enemytripplekill2"
                                                leng = 1600
                                            Case 3
                                                Rnd = "enemytripplekill3"
                                                leng = 1600
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemyquadrakill1"
                                                leng = 1600
                                            Case 2
                                                Rnd = "enemyquadrakill2"
                                                leng = 1600
                                            Case 3
                                                Rnd = "enemyquadrakill3"
                                                leng = 4300
                                        End Select
                                    Case 5
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemypantakill1"
                                                leng = 1700
                                            Case 2
                                                Rnd = "enemypantakill2"
                                                leng = 1450
                                            Case 3
                                                Rnd = "enemypantakill3"
                                                leng = 1750
                                            Case 4
                                                Rnd = "enemypantakill4"
                                                leng = 3500
                                        End Select
                                    Case Else
                                        Rnd = "enemypantakill1"
                                        leng = 1700
                                End Select
                        End Select

                    Case "MultikillC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydoublekillC1"
                                                leng = 1000
                                            Case 2
                                                Rnd = "allydoublekillC2"
                                                leng = 2100
                                            Case 3
                                                Rnd = "allydoublekillC3"
                                                leng = 2800
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allytriplekillC1"
                                                leng = 1300
                                            Case 2
                                                Rnd = "allytriplekillC2"
                                                leng = 1800
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allyquadraC1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "allyquadraC2"
                                                leng = 7000
                                            Case 3
                                                Rnd = "allyquadraC3"
                                                leng = 7000
                                        End Select
                                    Case 5
                                        'Select Case Rnum
                                        '    Case 1
                                        '        Rnd = "1"
                                        '        leng = 2000
                                        '    Case 2
                                        '        Rnd = "2"
                                        '        leng = 2000
                                        '    Case 3
                                        '        Rnd = "3"
                                        '        leng = 2000
                                        '    Case 4
                                        '        Rnd = "4"
                                        '        leng = 2000
                                        '    Case 5
                                        '        Rnd = "5"
                                        '        leng = 2000
                                        'End Select
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allypentaC1"
                                                leng = 2650
                                            Case 2
                                                Rnd = "allypentaC2"
                                                leng = 2650
                                            Case 3
                                                Rnd = "allypentaC3"
                                                leng = 2200
                                            Case 4
                                                Rnd = "allypentaC4"
                                                leng = 4600
                                        End Select
                                    Case Else
                                        Rnd = "allypenta1"
                                        leng = 1300
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemydoublekillC1"
                                                leng = 2500
                                            Case 2
                                                Rnd = "enemydoublekillC2"
                                                leng = 2000
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemytripplekillC1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "enemytripplekillC2"
                                                leng = 3200
                                        End Select
                                    Case 4
                                        Rnd = "enemyquadrakillC1"
                                        leng = 2900
                                    Case 5
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemypantakillC1"
                                                leng = 1950
                                            Case 2
                                                Rnd = "enemypantakillC2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "enemypantakillC3"
                                                leng = 2100
                                        End Select
                                    Case Else
                                        Rnd = "enemypantakillC1"
                                        leng = 1600
                                End Select
                        End Select

                    Case "TurretKilled"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case champ
                                    Case currentSummoner
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydestroytower1"
                                                leng = 1600
                                            Case 2
                                                Rnd = "allydestroytower2"
                                                leng = 2650
                                            Case 3
                                                Rnd = "allydestroytower1"
                                                leng = 6000
                                        End Select
                                    Case Else
                                        Rnd = "allydestroytower3"
                                        leng = 2200

                                End Select

                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemydestroytower1"
                                        leng = 2150
                                    Case 2
                                        Rnd = "enemydestroytower2"
                                        leng = 2100
                                    Case 3
                                        Rnd = "enemydestroytower3"
                                        leng = 2250
                                End Select


                                ' English ##################################
                                'Case 1
                                '    Rnd = "allydestroytower1"
                                '    leng = 1600
                                'Case 2
                                '    Rnd = "allydestroytower2"
                                '    leng = 1600
                                'Case 3

                        End Select

                    Case "InhibKilled"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemydestroyinhibi1"
                                        leng = 2000
                                    Case 2
                                        Rnd = "enemydestroyinhibi2"
                                        leng = 2000
                                    Case 3
                                        Rnd = "enemydestroyinhibi3"
                                        leng = 1900
                                    Case 4
                                        Rnd = "enemydestroyinhibi4"
                                        leng = 1700
                                End Select
                            Case Else
                                Select Case champ
                                    Case currentSummoner
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "youdestroyenemyinhibi1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "youdestroyenemyinhibi2"
                                                leng = 2200
                                            Case 3
                                                Rnd = "youdestroyenemyinhibi1"
                                                leng = 1800
                                            Case 4
                                                Rnd = "youdestroyenemyinhibi2"
                                                leng = 2200
                                        End Select
                                    Case Else
                                        Rnd = "yourteamdestroyenemyinhibi1"
                                        leng = 2500
                                End Select
                        End Select
                        If inhi = False Then
                            inhi = True
                            theme_end()
                            theme_start("starguardian_jp\StarGuardian_late", 6000000)
                        End If
                    Case "InhibRespawningSoon"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allyinhibirespawnsoon1"
                                        leng = 2600
                                    Case 2
                                        Rnd = "allyinhibirespawnsoon2"
                                        leng = 2700
                                End Select
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemyinhibirespawnsoon1"
                                        leng = 2600
                                    Case 2
                                        Rnd = "enemyinhibirespawnsoon2"
                                        leng = 2600
                                End Select
                        End Select

                    Case "badnews"
                        Rnd = "badnews1"
                        leng = 1400

                    Case "InhibRespawned"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allyinhibirespawned1"
                                        leng = 2300
                                    Case 2
                                        Rnd = "allyinhibirespawned2"
                                        leng = 2400
                                End Select
                            Case Else
                                Rnd = "enemyinhibirespawned1"
                                leng = 2200
                        End Select

                    Case "ExecutedC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "excecutedc1"
                                leng = 800
                            Case 2
                                Rnd = "excecutedc2"
                                leng = 1000
                            Case 3
                                Rnd = "excecutedc3"
                                leng = 3000
                        End Select

                    Case "Executed"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "excecuted1"
                                leng = 1200
                            Case 2
                                Rnd = "excecuted2"
                                leng = 1200
                            Case 3
                                Rnd = "excecuted3"
                                leng = 1200
                        End Select

                    Case "Acec"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "acemc1"
                                leng = 1800
                            Case 2
                                Rnd = "acemc2"
                                leng = 2300
                            Case 3
                                Rnd = "acemc3"
                                leng = 2300
                        End Select

                    Case "Ace"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "acem1"
                                        leng = 750
                                    Case 2
                                        Rnd = "acem2"
                                        leng = 1550
                                    Case 3
                                        Rnd = "acem3"
                                        leng = 700
                                End Select
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "acee1"
                                        leng = 700
                                    Case 2
                                        Rnd = "acee2"
                                        leng = 1000
                                    Case 3
                                        Rnd = "acee3"
                                        leng = 950
                                End Select
                        End Select

                    Case "deadC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhavebeenslayc1"
                                leng = 3900
                            Case 2
                                Rnd = "youhavebeenslayc2"
                                leng = 1100
                            Case 3
                                Rnd = "youhavebeenslayc3"
                                leng = 1000
                            Case 4
                                Rnd = "youhavebeenslayc4"
                                leng = 1900
                            Case 5
                                Rnd = "youhavebeenslayc5"
                                leng = 400
                            Case 6
                                Rnd = "youhavebeenslayc6"
                                leng = 1200
                            Case 7
                                Rnd = "youhavebeenslayc7"
                                leng = 2600
                        End Select

                    Case "dead"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhavebeenslay1"
                                leng = 2100
                            Case 2
                                Rnd = "youhavebeenslay2"
                                leng = 2100
                        End Select

                    Case "shutdownC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "shutdownC1"
                                leng = 3300
                            Case 2
                                Rnd = "shutdownC2"
                                leng = 3500
                            Case 3
                                Rnd = "shutdownC3"
                                leng = 1000
                        End Select

                    Case "shutdown"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "shutdown1"
                                leng = 900
                            Case 2
                                Rnd = "shutdown2"
                                leng = 900
                        End Select

                    Case "GameStart"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "welcome1"
                                leng = 5600
                            Case 2
                                Rnd = "welcome2"
                                leng = 2200
                            Case 3
                                Rnd = "welcome3"
                                leng = 2000
                            Case 4
                                Rnd = "welcome4"
                                leng = 2400
                            Case 5
                                Rnd = "welcome5"
                                leng = 3400
                            Case 6
                                Rnd = "welcome6"
                                leng = 1700
                        End Select
                        Timer5.Enabled = False
                        theme_end()
                        Timer5.Enabled = False
                        theme_start("starguardian_jp\StarGuardian", 6000000)

                    Case "minion30"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "minion30_1"
                                leng = 2800
                            Case 2
                                Rnd = "minion30_2"
                                leng = 2800
                            Case 3
                                Rnd = "minion30_3"
                                leng = 2900
                            Case 4
                                Rnd = "minion30_4"
                                leng = 3000
                            Case 5
                                Rnd = "minion30_5"
                                leng = 2700
                            Case 6
                                Rnd = "minion30_6"
                                leng = 3600
                            Case 7
                                Rnd = "minion30_7"
                                leng = 4500
                            Case 8
                                Rnd = "minion30_8"
                                leng = 2200
                            Case 9
                                Rnd = "minion30_9"
                                leng = 2600
                            Case 10
                                Rnd = "minion30_10"
                                leng = 5400
                        End Select

                    Case "welcome"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "howlingabyss1"
                                leng = 2200
                            Case 2
                                Rnd = "howlingabyss2"
                                leng = 5000
                        End Select

                    Case "GameEnd"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case champ
                            Case "Win"
                                Select Case Rnum
                                    Case 1
                                        Rnd = "victory1"
                                        leng = 5000
                                    Case 2
                                        Rnd = "victory2"
                                        leng = 5000
                                    Case 3
                                        Rnd = "victory3"
                                        leng = 6500
                                    Case 4
                                        Rnd = "victory4"
                                        leng = 5000
                                    Case 5
                                        Rnd = "victory5"
                                        leng = 5000
                                End Select
                                theme_end()

                                theme_start("starguardian_jp\victory_theme", 13000)
                                Timer5.Interval = 13000
                                Timer5.Enabled = True
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "defeat1"
                                        leng = 5000
                                    Case 2
                                        Rnd = "defeat2"
                                        leng = 5000
                                    Case 3
                                        Rnd = "defeat3"
                                        leng = 5000
                                    Case 4
                                        Rnd = "defeat4"
                                        leng = 5000
                                    Case 5
                                        Rnd = "defeat5"
                                        leng = 5000
                                End Select
                                theme_end()
                                theme_start("starguardian_jp\defeat_theme", 274000)
                                Timer5.Interval = 274000
                                Timer5.Enabled = True
                        End Select
                        s_flag = False
                        Label1.Text = "OFF"
                        inhi = False
                        Timer1.Enabled = False
                    Case "FirstBrick"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        If team <> myteam Then
                            Rnd = "enemydestroytowerC1"
                            leng = 3100
                        End If
                        theme_end()
                        theme_start("starguardian_jp\StarGuardian_mid", 6000000)
                        'Dim lines As New List(Of String)(TextBox3.Lines)
                        'lines.RemoveAt(0)
                        'TextBox3.Text = String.Join(vbCrLf, lines)
                        'Exit Sub
                    Case Else
                        Rnd = "ah"
                        leng = 900

                End Select
                Dim sond As String = "starguardian_jp\" & Rnd & ".mp3"
                Dim fileName As String = sond
                cmd = "open """ + fileName + """ type mpegvideo alias " + mysound
                If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
                    Return
                End If
                cmd = "play " + mysound
                mciSendString(cmd, Nothing, 0, IntPtr.Zero)
                'Dim Buffer As String = New String(Chr(0), 255)
                'Dim Lengt As Double
                'Call mciSendString("status mysound length", Buffer, Len(Buffer), 0)
                'Lengt = Val(Buffer) - 2000
                If teston Then
                    leng = Integer.Parse(TextBox4.Text)
                    teston = False
                End If
                mysoundtimer.Interval = leng
                mysoundtimer.Enabled = True
            End If
            If mode = 1 Then
                Select Case events
                    Case "FirstBlood" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Rnd = "firstbloodm1"
                                leng = 2000
                            Case Else
                                Rnd = "firstbloode1"
                                leng = 2000
                        End Select

                    Case "FirstBloodC" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "firstbloodmc1"
                                leng = 3000
                            Case 2
                                Rnd = "firstbloodmc2"
                                leng = 1700
                        End Select

                    Case "MinionsSpawning" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "minionspawn1"
                                leng = 2000
                            Case 2
                                Rnd = "minionspawn2"
                                leng = 3000
                            Case 3
                                Rnd = "minionspawn3"
                                leng = 3000
                            Case 4
                                Rnd = "minionspawn4"
                                leng = 3900
                        End Select

                    Case "youslayenemy1C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "enemysc1"
                                leng = 2100
                            Case 2
                                Rnd = "enemysc2"
                                leng = 2700
                            Case 3
                                Rnd = "enemysc3"
                                leng = 2400
                            Case 4
                                Rnd = "enemysc4"
                                leng = 2000
                        End Select

                    Case "enemyslayally1C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "allysc1"
                                leng = 2600
                            Case 2
                                Rnd = "allysc2"
                                leng = 2000
                            Case 3
                                Rnd = "allysc3"
                                leng = 2300
                        End Select


                    Case "youhaveslayenemy1C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhaveslayenemyc1"
                                leng = 2300
                            Case 2
                                Rnd = "youhaveslayenemyc2"
                                leng = 2400
                            Case 3
                                Rnd = "youhaveslayenemyc3"
                                leng = 2300
                            Case 4
                                Rnd = "youhaveslayenemyc4"
                                leng = 1400
                            Case 5
                                Rnd = "youhaveslayenemyc5"
                                leng = 1800
                            Case 6
                                Rnd = "youhaveslayenemyc6"
                                leng = 3300
                            Case 7
                                Rnd = "youhaveslayenemyc7"
                                leng = 1100
                        End Select

                    Case "youslayenemy3C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally3_C1"
                                leng = 1800
                            Case 2
                                Rnd = "ally3_C2"
                                leng = 2300
                            Case 3
                                Rnd = "ally3_C3"
                                leng = 2900
                        End Select

                    Case "enemyslayally3C" ' 
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy3_C1"
                        leng = 2000
                        'End Select

                    Case "youslayenemy4C" ' 
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally4_C1"
                                leng = 2000
                            Case 2
                                Rnd = "ally4_C2"
                                leng = 2000
                            Case 3
                                Rnd = "ally4_C3"
                                leng = 1700
                        End Select

                    Case "enemyslayally4C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy4_C1"
                        leng = 1700
                        'End Select

                    Case "youslayenemy5C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally5_C1"
                                leng = 2300
                            Case 2
                                Rnd = "ally5_C2"
                                leng = 2400
                            Case 3
                                Rnd = "ally5_C3"
                                leng = 2400
                        End Select

                    Case "enemyslayally5C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy5_C1"
                        leng = 1800
                        'End Select

                    Case "youslayenemy6C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally6_C1"
                                leng = 1200
                            Case 2
                                Rnd = "ally6_C2"
                                leng = 3400
                            Case 3
                                Rnd = "ally6_C3"
                                leng = 2100
                        End Select

                    Case "enemyslayally6C" ' 
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy6_C1"
                        leng = 3100
                        'End Select

                    Case "youslayenemy7C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally7_C1"
                                leng = 2500
                            Case 2
                                Rnd = "ally7_C2"
                                leng = 2500
                            Case 3
                                Rnd = "ally7_C3"
                                leng = 2200
                        End Select

                    Case "enemyslayally7C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy7_C1"
                        leng = 2600
                        'End Select

                    Case "youslayenemy8C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally8_C1"
                                leng = 3600
                            Case 2
                                Rnd = "ally8_C2"
                                leng = 3500
                            Case 3
                                Rnd = "ally8_C3"
                                leng = 2400
                            Case 4
                                Rnd = "ally8_C4"
                                leng = 1800
                            Case 5
                                Rnd = "ally8_C5"
                                leng = 1900
                            Case 6
                                Rnd = "ally8_C6"
                                leng = 2900
                        End Select

                    Case "enemyslayally8C" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "enemy8_C1"
                                leng = 2550
                            Case 2
                                Rnd = "enemy8_C2"
                                leng = 3000
                            Case 3
                                Rnd = "enemy8_C3"
                                leng = 1600
                        End Select

                    Case "youkillstreak"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "you3_1"
                                leng = 2300
                            Case 4
                                Rnd = "you4_1"
                                leng = 2300
                            Case 5
                                Rnd = "you5_1"
                                leng = 2300
                            Case 6
                                Rnd = "you6_1"
                                leng = 2300
                            Case 7
                                Rnd = "you7_1"
                                leng = 2100
                            Case Else
                                Rnd = "you8_1"
                                leng = 1800
                        End Select

                    Case "allyslayenemy" '
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "allys3_1"
                                leng = 2550
                            Case 4
                                Rnd = "allys4_1"
                                leng = 2500
                            Case 5
                                Rnd = "allys5_1"
                                leng = 2500
                            Case 6
                                Rnd = "allys6_1"
                                leng = 2400
                            Case 7
                                Rnd = "allys7_1"
                                leng = 2400
                            Case 8
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allys8_1"
                                        leng = 2400
                                    Case 2
                                        Rnd = "allys8_2"
                                        leng = 4000
                                End Select
                        End Select

                    Case "allyslayenemyC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "allys3_C1"
                                leng = 3500
                            Case 4
                                Rnd = "allys4_C1"
                                leng = 2800
                            Case 5
                                Rnd = "allys5_C1"
                                leng = 1050
                            Case 6
                                Rnd = "allys6_C1"
                                leng = 3100
                            Case 7
                                Rnd = "allys7_C1"
                                leng = 2500
                            Case 8
                                Rnd = "allys8_C1"
                                leng = 2000
                        End Select

                    Case "ChampionKill" ' CampionKill START $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 0, 1, 2
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "youhaveslayenemy1"
                                                        leng = 2100
                                                    Case 2
                                                        Rnd = "youhaveslayenemy2"
                                                        leng = 2200
                                                    Case 3
                                                        Rnd = "youhaveslayenemy1"
                                                        leng = 2000
                                                End Select
                                            Case Else
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "enemys1"
                                                        leng = 2200
                                                    Case 2
                                                        Rnd = "enemys2"
                                                        leng = 2400
                                                    Case 3
                                                        Rnd = "enemys3"
                                                        leng = 2000
                                                End Select
                                        End Select
                                    Case 3
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally3_1"
                                                        leng = 1500
                                                    Case 2
                                                        Rnd = "ally3_2"
                                                        leng = 1600
                                                    Case 3
                                                        Rnd = "ally3_1"
                                                        leng = 1500
                                                End Select
                                            Case Else
                                                Rnd = "allys3_1"
                                                leng = 2600
                                        End Select

                                    Case 4
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally4_1"
                                                        leng = 1400
                                                    Case 2
                                                        Rnd = "ally4_2"
                                                        leng = 1500
                                                    Case 3
                                                        Rnd = "ally4_3"
                                                        leng = 1500
                                                End Select
                                            Case Else
                                                Rnd = "allys4_1"
                                                leng = 2600
                                        End Select

                                    Case 5
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally5_1"
                                                        leng = 2000
                                                    Case 2
                                                        Rnd = "ally5_2"
                                                        leng = 1800
                                                    Case 3
                                                        Rnd = "ally5_1"
                                                        leng = 2000
                                                End Select
                                            Case Else
                                                Rnd = "allys5_1"
                                                leng = 2400
                                        End Select

                                    Case 6
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally6_1"
                                                        leng = 1600
                                                    Case 2
                                                        Rnd = "ally6_2"
                                                        leng = 1400
                                                    Case 3
                                                        Rnd = "ally6_1"
                                                        leng = 1600
                                                End Select
                                            Case Else
                                                Rnd = "allys6_1"
                                                leng = 2400
                                        End Select

                                    Case 7
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally7_1"
                                                        leng = 1200
                                                    Case 2
                                                        Rnd = "ally7_2"
                                                        leng = 1300
                                                    Case 3
                                                        Rnd = "ally7_1"
                                                        leng = 1200
                                                End Select
                                            Case Else
                                                Rnd = "allys7_1"
                                                leng = 2100
                                        End Select

                                    Case Else
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally8_1"
                                                        leng = 2300
                                                    Case 2
                                                        Rnd = "ally8_2"
                                                        leng = 1900
                                                    Case 3
                                                        Rnd = "ally8_1"
                                                        leng = 2300
                                                End Select
                                            Case Else
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "allys8_1"
                                                        leng = 2300
                                                    Case 2
                                                        Rnd = "allys8_2"
                                                        leng = 3900
                                                    Case 3
                                                        Rnd = "allys8_1"
                                                        leng = 2300
                                                End Select
                                        End Select
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 0, 1, 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allys1"
                                                leng = 2400
                                            Case 2
                                                Rnd = "allys2"
                                                leng = 2400
                                            Case 3
                                                Rnd = "allys3"
                                                leng = 2400
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy3_1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "enemy3_2"
                                                leng = 2000
                                            Case 3
                                                Rnd = "enemy3_3"
                                                leng = 2400
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy4_1"
                                                leng = 2200
                                            Case 2
                                                Rnd = "enemy4_2"
                                                leng = 2000
                                            Case 3
                                                Rnd = "enemy4_3"
                                                leng = 2100
                                        End Select
                                    Case 5
                                        Rnd = "enemy5_1"
                                        leng = 2200
                                    Case 6
                                        Rnd = "enemy6_1"
                                        leng = 2200
                                    Case 7
                                        Rnd = "enemy7_1"
                                        leng = 2000
                                    Case Else
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy8_1"
                                                leng = 2200
                                            Case 2
                                                Rnd = "enemy8_2"
                                                leng = 2350
                                        End Select
                                End Select

                        End Select '----ChampionKill End $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                    Case "Multikill"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydoublekill1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "allydoublekill2"
                                                leng = 1500
                                            Case 3
                                                Rnd = "allydoublekill3"
                                                leng = 1300
                                            Case 4
                                                Rnd = "allydoublekill4"
                                                leng = 1400
                                            Case 5
                                                Rnd = "allydoublekill5"
                                                leng = 2600
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allytriplekill1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "allytriplekill2"
                                                leng = 1700
                                            Case 3
                                                Rnd = "allytriplekill3"
                                                leng = 1700
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allyquadra1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "allyquadra2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "allyquadra3"
                                                leng = 1800
                                        End Select
                                    Case 5
                                        'Select Case Rnum
                                        '    Case 1
                                        '        Rnd = "1"
                                        '        leng = 2000
                                        '    Case 2
                                        '        Rnd = "2"
                                        '        leng = 2000
                                        '    Case 3
                                        '        Rnd = "3"
                                        '        leng = 2000
                                        '    Case 4
                                        '        Rnd = "4"
                                        '        leng = 2000
                                        '    Case 5
                                        '        Rnd = "5"
                                        '        leng = 2000
                                        'End Select
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allypenta1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "allypenta2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "allypenta3"
                                                leng = 1800
                                            Case 4
                                                Rnd = "allypenta4"
                                                leng = 2800
                                            Case 5
                                                Rnd = "allypenta5"
                                                leng = 3600
                                        End Select
                                    Case Else
                                        Rnd = "allypenta1"
                                        leng = 1800
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemydoublekill1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "enemydoublekill2"
                                                leng = 1600
                                            Case 3
                                                Rnd = "enemydoublekill3"
                                                leng = 2000
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemytripplekill1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "enemytripplekill2"
                                                leng = 2100
                                            Case 3
                                                Rnd = "enemytripplekill3"
                                                leng = 1800
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemyquadrakill1"
                                                leng = 1900
                                            Case 2
                                                Rnd = "enemyquadrakill2"
                                                leng = 1900
                                            Case 3
                                                Rnd = "enemyquadrakill3"
                                                leng = 3600
                                        End Select
                                    Case 5
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemypantakill1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "enemypantakill2"
                                                leng = 2000
                                            Case 3
                                                Rnd = "enemypantakill3"
                                                leng = 2000
                                            Case 4
                                                Rnd = "enemypantakill4"
                                                leng = 2200
                                        End Select
                                    Case Else
                                        Rnd = "enemypantakill1"
                                        leng = 1800
                                End Select
                        End Select

                    Case "MultikillC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydoublekillC1"
                                                leng = 1000
                                            Case 2
                                                Rnd = "allydoublekillC2"
                                                leng = 2100
                                            Case 3
                                                Rnd = "allydoublekillC3"
                                                leng = 2800
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allytriplekillC1"
                                                leng = 1300
                                            Case 2
                                                Rnd = "allytriplekillC2"
                                                leng = 1800
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allyquadraC1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "allyquadraC2"
                                                leng = 7000
                                            Case 3
                                                Rnd = "allyquadraC3"
                                                leng = 7000
                                        End Select
                                    Case 5
                                        'Select Case Rnum
                                        '    Case 1
                                        '        Rnd = "1"
                                        '        leng = 2000
                                        '    Case 2
                                        '        Rnd = "2"
                                        '        leng = 2000
                                        '    Case 3
                                        '        Rnd = "3"
                                        '        leng = 2000
                                        '    Case 4
                                        '        Rnd = "4"
                                        '        leng = 2000
                                        '    Case 5
                                        '        Rnd = "5"
                                        '        leng = 2000
                                        'End Select
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allypentaC1"
                                                leng = 2650
                                            Case 2
                                                Rnd = "allypentaC2"
                                                leng = 2650
                                            Case 3
                                                Rnd = "allypentaC3"
                                                leng = 2200
                                            Case 4
                                                Rnd = "allypentaC4"
                                                leng = 4600
                                        End Select
                                    Case Else
                                        Rnd = "allypenta1"
                                        leng = 1300
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemydoublekillC1"
                                                leng = 2500
                                            Case 2
                                                Rnd = "enemydoublekillC2"
                                                leng = 2000
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemytripplekillC1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "enemytripplekillC2"
                                                leng = 3200
                                        End Select
                                    Case 4
                                        Rnd = "enemyquadrakillC1"
                                        leng = 2900
                                    Case 5
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemypantakillC1"
                                                leng = 1950
                                            Case 2
                                                Rnd = "enemypantakillC2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "enemypantakillC3"
                                                leng = 2100
                                        End Select
                                    Case Else
                                        Rnd = "enemypantakillC1"
                                        leng = 1600
                                End Select
                        End Select

                    Case "TurretKilled"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case champ
                                    Case currentSummoner
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydestroytower1"
                                                leng = 1600
                                            Case 2
                                                Rnd = "allydestroytower2"
                                                leng = 2650
                                            Case 3
                                                Rnd = "allydestroytower1"
                                                leng = 1600
                                        End Select
                                    Case Else
                                        Rnd = "allydestroytower3"
                                        leng = 2200
                                End Select


                            Case Else
                                Select Case Rnum


                                    Case 1
                                        Rnd = "enemydestroytower1"
                                        leng = 2150
                                    Case 2
                                        Rnd = "enemydestroytower2"
                                        leng = 2100
                                    Case 3
                                        Rnd = "enemydestroytower3"
                                        leng = 5000
                                End Select


                        End Select

                    Case "InhibKilled"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemydestroyinhibi1"
                                        leng = 2000
                                    Case 2
                                        Rnd = "enemydestroyinhibi2"
                                        leng = 2000
                                    Case 3
                                        Rnd = "enemydestroyinhibi3"
                                        leng = 1900
                                    Case 4
                                        Rnd = "enemydestroyinhibi4"
                                        leng = 1700
                                End Select
                            Case Else
                                Select Case champ
                                    Case currentSummoner
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "youdestroyenemyinhibi1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "youdestroyenemyinhibi2"
                                                leng = 2200
                                            Case 3
                                                Rnd = "youdestroyenemyinhibi1"
                                                leng = 1800
                                            Case 4
                                                Rnd = "youdestroyenemyinhibi2"
                                                leng = 2200
                                        End Select
                                    Case Else
                                        Rnd = "yourteamdestroyenemyinhibi1"
                                        leng = 2500
                                End Select
                        End Select
                        If inhi = False Then
                            inhi = True
                            theme_end()
                            theme_start("starguardian_en\StarGuardian_late", 6000000)
                        End If
                    Case "InhibRespawningSoon"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allyinhibirespawnsoon1"
                                        leng = 2600
                                    Case 2
                                        Rnd = "allyinhibirespawnsoon2"
                                        leng = 2700
                                End Select
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemyinhibirespawnsoon1"
                                        leng = 2600
                                    Case 2
                                        Rnd = "enemyinhibirespawnsoon2"
                                        leng = 2600
                                End Select
                        End Select

                    Case "badnews"
                        Rnd = "badnews1"
                        leng = 1400

                    Case "InhibRespawned"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allyinhibirespawned1"
                                        leng = 2300
                                    Case 2
                                        Rnd = "allyinhibirespawned2"
                                        leng = 2400
                                End Select
                            Case Else
                                Rnd = "enemyinhibirespawned1"
                                leng = 2200
                        End Select

                    Case "ExecutedC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "excecutedc1"
                                leng = 700
                            Case 2
                                Rnd = "excecutedc2"
                                leng = 1000
                            Case 3
                                Rnd = "excecutedc3"
                                leng = 3000
                        End Select

                    Case "Executed"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "excecuted1"
                                leng = 1200
                            Case 2
                                Rnd = "excecuted2"
                                leng = 1200
                            Case 3
                                Rnd = "excecuted3"
                                leng = 1200
                        End Select

                    Case "Acec"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "acemc1"
                                leng = 1800
                            Case 2
                                Rnd = "acemc2"
                                leng = 2300
                            Case 3
                                Rnd = "acemc3"
                                leng = 3000
                        End Select

                    Case "Ace"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "acem1"
                                        leng = 750
                                    Case 2
                                        Rnd = "acem2"
                                        leng = 1550
                                    Case 3
                                        Rnd = "acem3"
                                        leng = 700
                                End Select
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "acee1"
                                        leng = 700
                                    Case 2
                                        Rnd = "acee2"
                                        leng = 1000
                                    Case 3
                                        Rnd = "acee3"
                                        leng = 950
                                End Select
                        End Select

                    Case "deadC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhavebeenslayc1"
                                leng = 3900
                            Case 2
                                Rnd = "youhavebeenslayc2"
                                leng = 1600
                            Case 3
                                Rnd = "youhavebeenslayc3"
                                leng = 1700
                            Case 4
                                Rnd = "youhavebeenslayc4"
                                leng = 12100
                            Case 5
                                Rnd = "youhavebeenslayc5"
                                leng = 1000
                            Case 6
                                Rnd = "youhavebeenslayc6"
                                leng = 1800
                            Case 7
                                Rnd = "youhavebeenslayc7"
                                leng = 2300
                        End Select

                    Case "dead"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhavebeenslay1"
                                leng = 2000
                            Case 2
                                Rnd = "youhavebeenslay2"
                                leng = 2200
                        End Select

                    Case "shutdownC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "shutdownC1"
                                leng = 3300
                            Case 2
                                Rnd = "shutdownC2"
                                leng = 3500
                            Case 3
                                Rnd = "shutdownC3"
                                leng = 2400
                        End Select

                    Case "shutdown"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "shutdown1"
                                leng = 900
                            Case 2
                                Rnd = "shutdown2"
                                leng = 900
                        End Select

                    Case "GameStart"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "welcome1"
                                leng = 4200
                            Case 2
                                Rnd = "welcome2"
                                leng = 1600
                            Case 3
                                Rnd = "welcome3"
                                leng = 2000
                            Case 4
                                Rnd = "welcome4"
                                leng = 1600
                            Case 5
                                Rnd = "welcome5"
                                leng = 2800
                            Case 6
                                Rnd = "welcome6"
                                leng = 1700
                        End Select
                        theme_end()
                        Timer5.Enabled = False
                        theme_start("starguardian_en\StarGuardian", 6000000)

                    Case "minion30"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "minion30_1"
                                leng = 2500
                            Case 2
                                Rnd = "minion30_2"
                                leng = 2500
                            Case 3
                                Rnd = "minion30_3"
                                leng = 2400
                            Case 4
                                Rnd = "minion30_4"
                                leng = 2500
                            Case 5
                                Rnd = "minion30_5"
                                leng = 2500
                            Case 6
                                Rnd = "minion30_6"
                                leng = 3000
                            Case 7
                                Rnd = "minion30_7"
                                leng = 4500
                            Case 8
                                Rnd = "minion30_8"
                                leng = 2800
                            Case 9
                                Rnd = "minion30_9"
                                leng = 2600
                            Case 10
                                Rnd = "minion30_10"
                                leng = 3400
                        End Select

                    Case "welcome"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "howlingabyss1"
                                leng = 2400
                            Case 2
                                Rnd = "howlingabyss2"
                                leng = 4500
                        End Select

                    Case "GameEnd"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case champ
                            Case "Win"
                                Select Case Rnum
                                    Case 1
                                        Rnd = "victory1"
                                        leng = 5000
                                    Case 2
                                        Rnd = "victory2"
                                        leng = 5000
                                    Case 3
                                        Rnd = "victory3"
                                        leng = 6500
                                    Case 4
                                        Rnd = "victory4"
                                        leng = 5000
                                    Case 5
                                        Rnd = "victory5"
                                        leng = 5000
                                End Select
                                theme_end()
                                theme_start("starguardian_en\victory_theme", 13000)
                                Timer5.Interval = 13000
                                Timer5.Enabled = True
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "defeat1"
                                        leng = 5000
                                    Case 2
                                        Rnd = "defeat2"
                                        leng = 5000
                                    Case 3
                                        Rnd = "defeat3"
                                        leng = 5000
                                    Case 4
                                        Rnd = "defeat4"
                                        leng = 5000
                                    Case 5
                                        Rnd = "defeat5"
                                        leng = 5000
                                End Select
                                theme_end()
                                theme_start("starguardian_en\defeat_theme", 274000)
                                Timer5.Interval = 274000
                                Timer5.Enabled = True
                        End Select
                        s_flag = False
                        Label1.Text = "OFF"
                        inhi = False
                        Timer1.Enabled = False
                    Case "FirstBrick"
                        'If teston Then
                        '    Rnum = Integer.Parse(TextBox6.Text)
                        'End If

                        Rnd = "enemydestroytowerC1"
                        'If team = myteam Then
                        leng = 1
                        'Else
                        '    leng = 10
                        'End If

                        theme_end()
                        theme_start("starguardian_en\StarGuardian_mid", 6000000)
                        'Dim lines As New List(Of String)(TextBox3.Lines)
                        'lines.RemoveAt(0)
                        'TextBox3.Text = String.Join(vbCrLf, lines)
                        'Exit Sub
                    Case Else
                        Rnd = "ah"
                        leng = 900

                End Select
                Dim sond As String = "starguardian_en\" & Rnd & ".mp3"
                Dim fileName As String = sond
                cmd = "open """ + fileName + """ type mpegvideo alias " + mysound
                If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
                    Return
                End If
                cmd = "play " + mysound
                mciSendString(cmd, Nothing, 0, IntPtr.Zero)
                'Dim Buffer As String = New String(Chr(0), 255)
                'Dim Lengt As Double
                'Call mciSendString("status mysound length", Buffer, Len(Buffer), 0)
                'Lengt = Val(Buffer) - 2000
                If teston Then
                    leng = Integer.Parse(TextBox4.Text)
                    teston = False
                End If
                mysoundtimer.Interval = leng
                mysoundtimer.Enabled = True
            End If
            If mode = 2 Then
                Rnd = "ah"
                leng = 900
                Select Case events
                    Case "FirstBlood"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Rnd = "firstbloodm1"
                                leng = 2000
                            Case Else
                                Rnd = "firstbloode1"
                                leng = 2000
                        End Select

                    Case "FirstBloodC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "firstbloodmc1"
                                leng = 2000
                            Case 2
                                Rnd = "firstbloodmc2"
                                leng = 2300
                        End Select

                    Case "MinionsSpawning"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "minionspawn1"
                                leng = 2000
                            Case 2
                                Rnd = "minionspawn2"
                                leng = 2000
                            Case 3
                                Rnd = "minionspawn3"
                                leng = 2000
                            Case 4
                                Rnd = "minionspawn4"
                                leng = 2000
                        End Select

                    Case "youslayenemy1C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "enemysc1"
                                leng = 2200
                            Case 2
                                Rnd = "enemysc2"
                                leng = 1600
                            Case 3
                                Rnd = "enemysc3"
                                leng = 2500
                            Case 4
                                Rnd = "enemysc4"
                                leng = 2500
                        End Select

                    Case "enemyslayally1C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "allysc1"
                                leng = 2600
                            Case 2
                                Rnd = "allysc2"
                                leng = 2300
                            Case 3
                                Rnd = "allysc3"
                                leng = 2400
                        End Select


                    Case "youhaveslayenemy1C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhaveslayenemyc1"
                                leng = 2400
                            Case 2
                                Rnd = "youhaveslayenemyc2"
                                leng = 1900
                            Case 3
                                Rnd = "youhaveslayenemyc3"
                                leng = 2000
                            Case 4
                                Rnd = "youhaveslayenemyc4"
                                leng = 2300
                            Case 5
                                Rnd = "youhaveslayenemyc5"
                                leng = 2800
                            Case 6
                                Rnd = "youhaveslayenemyc6"
                                leng = 2200
                            Case 7
                                Rnd = "youhaveslayenemyc7"
                                leng = 2200
                        End Select

                    Case "youslayenemy3C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally3_C1"
                                leng = 2200
                            Case 2
                                Rnd = "ally3_C2"
                                leng = 1800
                            Case 3
                                Rnd = "ally3_C3"
                                leng = 1800
                        End Select

                    Case "enemyslayally3C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy3_C1"
                        leng = 2200
                        'End Select

                    Case "youslayenemy4C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally4_C1"
                                leng = 1700
                            Case 2
                                Rnd = "ally4_C2"
                                leng = 2200
                            Case 3
                                Rnd = "ally4_C3"
                                leng = 1900
                        End Select

                    Case "enemyslayally4C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy4_C1"
                        leng = 1900
                        'End Select

                    Case "youslayenemy5C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally5_C1"
                                leng = 1700
                            Case 2
                                Rnd = "ally5_C2"
                                leng = 1700
                            Case 3
                                Rnd = "ally5_C3"
                                leng = 1700
                        End Select

                    Case "enemyslayally5C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy5_C1"
                        leng = 2600
                        'End Select

                    Case "youslayenemy6C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally6_C1"
                                leng = 2300
                            Case 2
                                Rnd = "ally6_C2"
                                leng = 2000
                            Case 3
                                Rnd = "ally6_C3"
                                leng = 2300
                        End Select

                    Case "enemyslayally6C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy6_C1"
                        leng = 2300
                        'End Select

                    Case "youslayenemy7C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally7_C1"
                                leng = 2800
                            Case 2
                                Rnd = "ally7_C2"
                                leng = 1600
                            Case 3
                                Rnd = "ally7_C3"
                                leng = 1700
                        End Select

                    Case "enemyslayally7C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        'Select Case Rnum
                        '    Case 1
                        Rnd = "enemy7_C1"
                        leng = 1700
                        'End Select

                    Case "youslayenemy8C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "ally8_C1"
                                leng = 1700
                            Case 2
                                Rnd = "ally8_C2"
                                leng = 2100
                            Case 3
                                Rnd = "ally8_C3"
                                leng = 2000
                            Case 4
                                Rnd = "ally8_C4"
                                leng = 1700
                            Case 5
                                Rnd = "ally8_C5"
                                leng = 2100
                            Case 6
                                Rnd = "ally8_C6"
                                leng = 2000
                        End Select

                    Case "enemyslayally8C"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "enemy8_C1"
                                leng = 2000
                            Case 2
                                Rnd = "enemy8_C2"
                                leng = 2000
                            Case 3
                                Rnd = "enemy8_C3"
                                leng = 2000
                        End Select

                    Case "youkillstreak"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "you3_1"
                                leng = 1800
                            Case 4
                                Rnd = "you4_1"
                                leng = 1400
                            Case 5
                                Rnd = "you5_1"
                                leng = 2000
                            Case 6
                                Rnd = "you6_1"
                                leng = 1400
                            Case 7
                                Rnd = "you7_1"
                                leng = 1400
                            Case Else
                                Rnd = "you8_1"
                                leng = 1800
                        End Select

                    Case "allyslayenemy"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "allys3_1"
                                leng = 1800
                            Case 4
                                Rnd = "allys4_1"
                                leng = 1400
                            Case 5
                                Rnd = "allys5_1"
                                leng = 2000
                            Case 6
                                Rnd = "allys6_1"
                                leng = 1400
                            Case 7
                                Rnd = "allys7_1"
                                leng = 1800
                            Case 8
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allys8_1"
                                        leng = 1800
                                    Case 2
                                        Rnd = "allys8_2"
                                        leng = 1800
                                End Select
                        End Select

                    Case "allyslayenemyC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case killstreak
                            Case 3
                                Rnd = "allys3_C1"
                                leng = 1800
                            Case 4
                                Rnd = "allys4_C1"
                                leng = 1800
                            Case 5
                                Rnd = "allys5_C1"
                                leng = 1800
                            Case 6
                                Rnd = "allys6_C1"
                                leng = 2050
                            Case 7
                                Rnd = "allys7_C1"
                                leng = 3000
                            Case 8
                                Rnd = "allys8_C1"
                                leng = 1800
                        End Select

                    Case "ChampionKill" ' CampionKill START $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 0, 1, 2
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "youhaveslayenemy1"
                                                        leng = 2000
                                                    Case 2
                                                        Rnd = "youhaveslayenemy2"
                                                        leng = 2000
                                                    Case 3
                                                        Rnd = "youhaveslayenemy1"
                                                        leng = 2000
                                                End Select
                                            Case Else
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "enemys1"
                                                        leng = 2200
                                                    Case 2
                                                        Rnd = "enemys2"
                                                        leng = 2200
                                                    Case 3
                                                        Rnd = "enemys3"
                                                        leng = 2200
                                                End Select
                                        End Select
                                    Case 3
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally3_1"
                                                        leng = 1800
                                                    Case 2
                                                        Rnd = "ally3_2"
                                                        leng = 1900
                                                    Case 3
                                                        Rnd = "ally3_1"
                                                        leng = 1800
                                                End Select
                                            Case Else
                                                Rnd = "allys3_1"
                                                leng = 1800
                                        End Select

                                    Case 4
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally4_1"
                                                        leng = 1400
                                                    Case 2
                                                        Rnd = "ally4_2"
                                                        leng = 1600
                                                    Case 3
                                                        Rnd = "ally4_3"
                                                        leng = 1600
                                                End Select
                                            Case Else
                                                Rnd = "allys4_1"
                                                leng = 1400
                                        End Select

                                    Case 5
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally5_1"
                                                        leng = 1800
                                                    Case 2
                                                        Rnd = "ally5_2"
                                                        leng = 1800
                                                    Case 3
                                                        Rnd = "ally5_2"
                                                        leng = 1800
                                                End Select
                                            Case Else
                                                Rnd = "allys5_1"
                                                leng = 1800
                                        End Select

                                    Case 6
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally6_1"
                                                        leng = 1500
                                                    Case 2
                                                        Rnd = "ally6_2"
                                                        leng = 1500
                                                    Case 3
                                                        Rnd = "ally6_2"
                                                        leng = 1500
                                                End Select
                                            Case Else
                                                Rnd = "allys6_1"
                                                leng = 1500
                                        End Select

                                    Case 7
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally7_1"
                                                        leng = 1500
                                                    Case 2
                                                        Rnd = "ally7_2"
                                                        leng = 1500
                                                    Case 3
                                                        Rnd = "ally7_2"
                                                        leng = 1500
                                                End Select
                                            Case Else
                                                Rnd = "allys7_1"
                                                leng = 1500
                                        End Select

                                    Case Else
                                        Select Case champ
                                            Case currentSummoner
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "ally8_1"
                                                        leng = 2000
                                                    Case 2
                                                        Rnd = "ally8_2"
                                                        leng = 2000
                                                    Case 3
                                                        Rnd = "ally8_2"
                                                        leng = 2000
                                                End Select
                                            Case Else
                                                Select Case Rnum
                                                    Case 1
                                                        Rnd = "allys8_1"
                                                        leng = 2000
                                                    Case 2
                                                        Rnd = "allys8_2"
                                                        leng = 2000
                                                    Case 3
                                                        Rnd = "allys8_1"
                                                        leng = 2000
                                                End Select
                                        End Select
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 0, 1, 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allys1"
                                                leng = 2400
                                            Case 2
                                                Rnd = "allys2"
                                                leng = 2400
                                            Case 3
                                                Rnd = "allys3"
                                                leng = 2400
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy3_1"
                                                leng = 1900
                                            Case 2
                                                Rnd = "enemy3_2"
                                                leng = 1900
                                            Case 3
                                                Rnd = "enemy3_3"
                                                leng = 2100
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy4_1"
                                                leng = 1600
                                            Case 2
                                                Rnd = "enemy4_2"
                                                leng = 1600
                                            Case 3
                                                Rnd = "enemy4_3"
                                                leng = 2000
                                        End Select
                                    Case 5
                                        Rnd = "enemy5_1"
                                        leng = 2100
                                    Case 6
                                        Rnd = "enemy6_1"
                                        leng = 2100
                                    Case 7
                                        Rnd = "enemy7_1"
                                        leng = 2200
                                    Case Else
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemy8_1"
                                                leng = 2100
                                            Case 2
                                                Rnd = "enemy8_2"
                                                leng = 2100
                                        End Select
                                End Select

                        End Select '----ChampionKill End $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                    Case "Multikill"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydoublekill1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "allydoublekill2"
                                                leng = 1500
                                            Case 3
                                                Rnd = "allydoublekill3"
                                                leng = 2500
                                            Case 4
                                                Rnd = "allydoublekill4"
                                                leng = 3000
                                            Case 5
                                                Rnd = "allydoublekill5"
                                                leng = 2500
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allytriplekill1"
                                                leng = 1600
                                            Case 2
                                                Rnd = "allytriplekill2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "allytriplekill3"
                                                leng = 2000
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allyquadra1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "allyquadra2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "allyquadra3"
                                                leng = 1800
                                        End Select
                                    Case 5
                                        'Select Case Rnum
                                        '    Case 1
                                        '        Rnd = "1"
                                        '        leng = 2000
                                        '    Case 2
                                        '        Rnd = "2"
                                        '        leng = 2000
                                        '    Case 3
                                        '        Rnd = "3"
                                        '        leng = 2000
                                        '    Case 4
                                        '        Rnd = "4"
                                        '        leng = 2000
                                        '    Case 5
                                        '        Rnd = "5"
                                        '        leng = 2000
                                        'End Select
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allypenta1"
                                                leng = 2800
                                            Case 2
                                                Rnd = "allypenta2"
                                                leng = 4000
                                            Case 3
                                                Rnd = "allypenta3"
                                                leng = 3500
                                            Case 4
                                                Rnd = "allypenta4"
                                                leng = 4000
                                            Case 5
                                                Rnd = "allypenta2"
                                                leng = 4000
                                        End Select
                                    Case Else
                                        Rnd = "allypenta1"
                                        leng = 2800
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemydoublekill1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "enemydoublekill2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "enemydoublekill3"
                                                leng = 1800
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemytripplekill1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "enemytripplekill2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "enemytripplekill3"
                                                leng = 1800
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemyquadrakill1"
                                                leng = 1800
                                            Case 2
                                                Rnd = "enemyquadrakill2"
                                                leng = 1800
                                            Case 3
                                                Rnd = "enemyquadrakill3"
                                                leng = 1800
                                        End Select
                                    Case 5
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemypantakill1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "enemypantakill2"
                                                leng = 2000
                                            Case 3
                                                Rnd = "enemypantakill3"
                                                leng = 2250
                                            Case 4
                                                Rnd = "enemypantakill4"
                                                leng = 2000
                                        End Select
                                    Case Else
                                        Rnd = "enemypantakill1"
                                        leng = 2000
                                End Select
                        End Select

                    Case "MultikillC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allydoublekillC1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "allydoublekillC2"
                                                leng = 2100
                                            Case 3
                                                Rnd = "allydoublekillC3"
                                                leng = 1600
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allytriplekillC1"
                                                leng = 1600
                                            Case 2
                                                Rnd = "allytriplekillC2"
                                                leng = 2000
                                        End Select
                                    Case 4
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allyquadraC1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "allyquadraC2"
                                                leng = 2000
                                            Case 3
                                                Rnd = "allyquadraC3"
                                                leng = 4000
                                        End Select
                                    Case 5
                                        'Select Case Rnum
                                        '    Case 1
                                        '        Rnd = "1"
                                        '        leng = 2000
                                        '    Case 2
                                        '        Rnd = "2"
                                        '        leng = 2000
                                        '    Case 3
                                        '        Rnd = "3"
                                        '        leng = 2000
                                        '    Case 4
                                        '        Rnd = "4"
                                        '        leng = 2000
                                        '    Case 5
                                        '        Rnd = "5"
                                        '        leng = 2000
                                        'End Select
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "allypentaC1"
                                                leng = 3000
                                            Case 2
                                                Rnd = "allypentaC2"
                                                leng = 3000
                                            Case 3
                                                Rnd = "allypentaC3"
                                                leng = 3000
                                            Case 4
                                                Rnd = "allypentaC4"
                                                leng = 3000
                                        End Select
                                    Case Else
                                        Rnd = "allypentaC1"
                                        leng = 3000
                                End Select
                            Case Else
                                Select Case killstreak
                                    Case 2
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemydoublekillC1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "enemydoublekillC2"
                                                leng = 2300
                                        End Select
                                    Case 3
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemytripplekillC1"
                                                leng = 1500
                                            Case 2
                                                Rnd = "enemytripplekillC2"
                                                leng = 2100
                                        End Select
                                    Case 4
                                        Rnd = "enemyquadrakillC1"
                                        leng = 2000
                                    Case 5
                                        Select Case Rnum
                                            Case 1
                                                Rnd = "enemypantakillC1"
                                                leng = 2000
                                            Case 2
                                                Rnd = "enemypantakillC2"
                                                leng = 2300
                                            Case 3
                                                Rnd = "enemypantakillC3"
                                                leng = 2000
                                        End Select
                                    Case Else
                                        Rnd = "enemypantakillC1"
                                        leng = 2000
                                End Select
                        End Select

                    Case "TurretKilled"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allydestroytower1"
                                        leng = 1800
                                    Case 2
                                        Rnd = "allydestroytower2"
                                        leng = 1800
                                    Case 3
                                        Rnd = "allydestroytower3"
                                        leng = 1800
                                End Select
                            Case Else
                                'Select Case champ
                                '    Case currentSummoner
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemydestroytower1"
                                        leng = 2100
                                    Case 2
                                        Rnd = "enemydestroytower2"
                                        leng = 2100
                                    Case 3
                                        Rnd = "enemydestroytower3"
                                        leng = 2100
                                End Select
                                'Case Else
                                '    Rnd = "allydestroytower3"
                                '    leng = 2200
                                'End Select

                                ' English ##################################
                                'Case 1
                                '    Rnd = "allydestroytower1"
                                '    leng = 1600
                                'Case 2
                                '    Rnd = "allydestroytower2"
                                '    leng = 1600
                                'Case 3

                        End Select

                    Case "InhibKilled"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemydestroyinhibi1"
                                        leng = 2500
                                    Case 2
                                        Rnd = "enemydestroyinhibi2"
                                        leng = 2400
                                    Case 3
                                        Rnd = "enemydestroyinhibi3"
                                        leng = 1800
                                    Case 4
                                        Rnd = "enemydestroyinhibi4"
                                        leng = 1800


                                End Select
                            Case Else
                                'Select Case champ
                                '    Case currentSummoner
                                Select Case Rnum
                                    Case 1
                                        Rnd = "youdestroyenemyinhibi1"
                                        leng = 2200
                                    Case 2
                                        Rnd = "youdestroyenemyinhibi2"
                                        leng = 2000
                                    Case 3
                                        Rnd = "youdestroyenemyinhibi1"
                                        leng = 2200
                                    Case 4
                                        Rnd = "youdestroyenemyinhibi2"
                                        leng = 2000
                                End Select
                                'Case Else
                                '    Rnd = "yourteamdestroyenemyinhibi1"
                                '    leng = 2500
                                'End Select
                        End Select
                        If inhi = False Then
                            inhi = True
                            theme_end()
                            theme_start("butchersbridge\butcher's_bridge_late", 6000000)
                        End If
                    Case "InhibRespawningSoon"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allyinhibirespawnsoon1"
                                        leng = 2600
                                    Case 2
                                        Rnd = "allyinhibirespawnsoon2"
                                        leng = 2500
                                End Select
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "enemyinhibirespawnsoon1"
                                        leng = 3000
                                    Case 2
                                        Rnd = "enemyinhibirespawnsoon2"
                                        leng = 2600
                                End Select
                        End Select

                    Case "badnews"
                        Rnd = "badnews1"
                        leng = 1

                    Case "InhibRespawned"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "allyinhibirespawned1"
                                        leng = 2600
                                    Case 2
                                        Rnd = "allyinhibirespawned2"
                                        leng = 2000
                                End Select
                            Case Else
                                Rnd = "enemyinhibirespawned1"
                                leng = 2600
                        End Select

                    Case "ExecutedC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "excecutedc1"
                                leng = 1800
                            Case 2
                                Rnd = "excecutedc2"
                                leng = 2000
                            Case 3
                                Rnd = "excecutedc3"
                                leng = 2100
                        End Select

                    Case "Executed"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "excecuted1"
                                leng = 1400
                            Case 2
                                Rnd = "excecuted2"
                                leng = 1400
                            Case 3
                                Rnd = "excecuted3"
                                leng = 1400
                        End Select

                    Case "Acec"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Rnd = "acem1"
                        leng = 1
                        'Select Case Rnum
                        '    Case 1
                        '        Rnd = "acemc1"
                        '        leng = 1800
                        '    Case 2
                        '        Rnd = "acemc2"
                        '        leng = 2300
                        '    Case 3
                        '        Rnd = "acemc3"
                        '        leng = 2300
                        'End Select

                    Case "Ace"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case team
                            Case myteam
                                Select Case Rnum
                                    Case 1
                                        Rnd = "acem1"
                                        leng = 3200
                                    Case 2
                                        Rnd = "acem2"
                                        leng = 3400
                                    Case 3
                                        Rnd = "acem3"
                                        leng = 3200
                                End Select
                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "acee1"
                                        leng = 3200
                                    Case 2
                                        Rnd = "acee2"
                                        leng = 2800
                                    Case 3
                                        Rnd = "acee3"
                                        leng = 2800
                                End Select
                        End Select

                    Case "deadC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhavebeenslayc1"
                                leng = 2100
                            Case 2
                                Rnd = "youhavebeenslayc2"
                                leng = 1300
                            Case 3
                                Rnd = "youhavebeenslayc3"
                                leng = 1500
                            Case 4
                                Rnd = "youhavebeenslayc4"
                                leng = 2000
                            Case 5
                                Rnd = "youhavebeenslayc5"
                                leng = 2000
                            Case 6
                                Rnd = "youhavebeenslayc6"
                                leng = 1600
                            Case 7
                                Rnd = "youhavebeenslayc7"
                                leng = 2100
                        End Select

                    Case "dead"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "youhavebeenslay1"
                                leng = 2100
                            Case 2
                                Rnd = "youhavebeenslay2"
                                leng = 2100
                        End Select

                    Case "shutdownC"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "shutdownC1"
                                leng = 2100
                            Case 2
                                Rnd = "shutdownC2"
                                leng = 2100
                            Case 3
                                Rnd = "shutdownC3"
                                leng = 2100
                        End Select

                    Case "shutdown"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "shutdown1"
                                leng = 1200
                            Case 2
                                Rnd = "shutdown2"
                                leng = 1500
                        End Select

                    Case "GameStart"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "welcome1"
                                leng = 6000
                            Case 2
                                Rnd = "welcome2"
                                leng = 3000
                            Case 3
                                Rnd = "welcome3"
                                leng = 3700
                            Case 4
                                Rnd = "welcome4"
                                leng = 3000
                            Case 5
                                Rnd = "welcome5"
                                leng = 3000
                            Case 6
                                Rnd = "welcome6"
                                leng = 3000
                        End Select
                        theme_end()
                        Timer5.Enabled = False
                        theme_start("butchersbridge\butcher's_bridge_early", 6000000)

                    Case "minion30"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "minion30_1"
                                leng = 2800
                            Case 2
                                Rnd = "minion30_2"
                                leng = 2800
                            Case 3
                                Rnd = "minion30_3"
                                leng = 1800
                            Case 4
                                Rnd = "minion30_4"
                                leng = 2400
                            Case 5
                                Rnd = "minion30_5"
                                leng = 2500
                            Case 6
                                Rnd = "minion30_6"
                                leng = 2700
                            Case 7
                                Rnd = "minion30_7"
                                leng = 2700
                            Case 8
                                Rnd = "minion30_8"
                                leng = 2200
                            Case 9
                                Rnd = "minion30_9"
                                leng = 2400
                            Case 10
                                Rnd = "minion30_10"
                                leng = 2400
                        End Select

                    Case "welcome"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case Rnum
                            Case 1
                                Rnd = "howlingabyss1"
                                leng = 3000
                            Case 2
                                Rnd = "howlingabyss2"
                                leng = 6000
                        End Select

                    Case "GameEnd"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Select Case champ
                            Case "Win"
                                Select Case Rnum
                                    Case 1
                                        Rnd = "victory1"
                                        leng = 2200
                                    Case 2
                                        Rnd = "victory2"
                                        leng = 2200
                                    Case 3
                                        Rnd = "victory3"
                                        leng = 4000
                                    Case 4
                                        Rnd = "victory4"
                                        leng = 4000
                                    Case 5
                                        Rnd = "victory5"
                                        leng = 4000
                                End Select
                                theme_end()
                                theme_start("butchersbridge\butcher's_bridge_victory", 90000)
                                Timer5.Interval = 90000
                                Timer5.Enabled = True


                            Case Else
                                Select Case Rnum
                                    Case 1
                                        Rnd = "defeat1"
                                        leng = 1200
                                    Case 2
                                        Rnd = "defeat2"
                                        leng = 2200
                                    Case 3
                                        Rnd = "defeat3"
                                        leng = 3000
                                    Case 4
                                        Rnd = "defeat4"
                                        leng = 4000
                                    Case 5
                                        Rnd = "defeat5"
                                        leng = 4000
                                End Select
                                theme_end()
                                theme_start("butchersbridge\butcher's_bridge_defeat", 85000)
                                Timer5.Interval = 85000
                                Timer5.Enabled = True

                        End Select
                        s_flag = False
                        Label1.Text = "OFF"
                        inhi = False
                        Timer1.Enabled = False
                    Case "FirstBrick"
                        If teston Then
                            Rnum = Integer.Parse(TextBox6.Text)
                        End If
                        Rnd = "enemydestroytowerC1"
                        leng = 2000


                        theme_end()
                        theme_start("butchersbridge\butcher's_bridge_mid", 6000000)
                        'Dim lines As New List(Of String)(TextBox3.Lines)
                        'lines.RemoveAt(0)
                        'TextBox3.Text = String.Join(vbCrLf, lines)
                        'Exit Sub
                    Case Else
                        Rnd = "ah"
                        leng = 900

                End Select
                Dim sond As String = "butchersbridge\" & Rnd & ".mp3"
                Dim fileName As String = sond
                cmd = "open """ + fileName + """ type mpegvideo alias " + mysound
                If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
                    Return
                End If
                cmd = "play " + mysound
                mciSendString(cmd, Nothing, 0, IntPtr.Zero)
                'Dim Buffer As String = New String(Chr(0), 255)
                'Dim Lengt As Double
                'Call mciSendString("status mysound length", Buffer, Len(Buffer), 0)
                'Lengt = Val(Buffer) - 2000
                If teston Then
                    leng = Integer.Parse(TextBox4.Text)
                    'teston = False
                End If
                mysoundtimer.Interval = leng
                mysoundtimer.Enabled = True
            End If
        End If

    End Sub

    Dim chst As Boolean = False

    Public Sub theme_start(Rnd As String, leng As Integer)

        'If Rnd = "starguardian_jp\championselect" Or Rnd = "starguardian_en\championselect" Or Rnd = "butchersBridge\butcher's_bridge_1_champion_select" Then
        '    chst = True
        'End If
        'Dim Rnd As String = "sound\StarGuardian"
        'Select Case mode
        '    Case 0
        '        Rnd = "sound\StarGuardian"
        '    Case 1
        '        Rnd = "sounds\Bilgewater"
        '    Case Else
        '        Exit Sub
        'End Select
        Dim cmd = "play " + theme
        'Dim leng As Integer = 6000000
        'Dim Rnd As String = ""
        Dim sond As String = Rnd & ".mp3"
        Dim fileName As String = sond
        cmd = "open """ + fileName + """ type mpegvideo alias " + theme
        If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
            Return
        End If
        cmd = "play " + theme
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        'leng = Integer.Parse(TextBox4.Text)
        themetimer.Interval = leng
        themetimer.Enabled = True
    End Sub

    Public Sub theme_end()
        themetimer.Enabled = False
        'If Tabb = 96 Then
        Dim cmd As String
        '再生しているWAVEを停止する
        cmd = "stop " + theme
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        '  閉じる
        cmd = "close " + theme
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)

    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        teston = True
        TextBox2.AppendText(TextBox5.Text)
        TextsChanged(False)
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Dim r As New System.Random()
        Dim Rnum As Integer = r.Next(1, 3)
        TextBox2.AppendText("00:30" & "," & "welcome" & "," & "0" & "," & "0" & "," & "0" & "," & 0 & "," & Rnum)
        TextsChanged(False)
        Timer3.Enabled = False
    End Sub
    Dim mode As Integer = 0
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged
        Dim pat As String = "sounds\Bilgewater"
        Dim leng As Integer = 6000000
        If RadioButton1.Checked Then
            mode = 0
            pat = "butchersbridge\ambient"
            leng = 6000000
        ElseIf RadioButton2.Checked Then
            mode = 1
            pat = "butchersbridge\ambient"
            leng = 6000000
        ElseIf RadioButton3.Checked Then
            mode = 2
            pat = "butchersbridge\butcher's_bridge_early"
            leng = 6000000
        End If
        theme_end()
        theme_start(pat, leng)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        theme_end()
        chst = False
        Timer5.Enabled = False
        inhi = False
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TextBox7.Text <> "" Then
            'Clipboard.SetText(TextBox7.Text)
        End If
        TextBox2.Clear()
        TextBox7.Clear()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)
        Dim r As New System.Random()
        Dim Rnd As Integer = r.Next(1, 4)
        TextBox2.AppendText(Rnd & vbCrLf)
    End Sub

    Private Sub minion30_Tick(sender As Object, e As EventArgs) Handles minion30.Tick
        Dim r As New System.Random()
        Dim Rnum As Integer = r.Next(1, 10)
        TextBox2.AppendText("00:15" & "," & "minion30" & "," & "0" & "," & "0" & "," & "0" & "," & 0 & "," & Rnum)
        TextsChanged(False)
        minion30.Enabled = False
    End Sub

    Private Sub Form3_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        theme_end()
        Dim cmd As String
        '再生しているWAVEを停止する
        cmd = "stop " + mysound
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click
        If Timer2.Enabled = False Then
            Timer2.Enabled = True
            Button9.Text = "on"
        Else
            Timer2.Enabled = False
            Button9.Text = "off"
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Me.TopMost Then
            Me.TopMost = False
        Else
            Me.TopMost = True
        End If
        Timer1.Start()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        nn += 1
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        nn -= 1
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Timer5.Enabled = False
        theme_end()
        Dim pat As String = "sounds\Bilgewater"
        Dim leng As Integer = 6000000
        If RadioButton1.Checked Then
            mode = 0
            pat = "butchersbridge\ambient"
            leng = 6000000
        ElseIf RadioButton2.Checked Then
            mode = 1
            pat = "butchersbridge\ambient"
            leng = 6000000
        ElseIf RadioButton3.Checked Then
            mode = 2
            pat = "butchersbridge\butcher's_bridge_early"
            leng = 6000000
        End If
        theme_end()
        theme_start(pat, leng)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        RichTextBox1.Text = ""
    End Sub

    Private Sub end_timer_Tick(sender As Object, e As EventArgs) Handles end_timer.Tick
        end_timer.Enabled = False
        Form1.Button14.PerformClick()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        nns = 0
        RichTextBox1.Clear()
        'nnn = 0
        nn = 0
        yy = 0
        yy_bin = 0
        stats(nn)
        Label1.Text = "on"
    End Sub


End Class

