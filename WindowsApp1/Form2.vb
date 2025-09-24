Option Strict Off
Imports System.Text
Imports System.IO
Imports System.Net

Imports Newtonsoft.Json

Imports EasyHttp.Http
Public Class Form2

    Dim one As Boolean = False
    Dim nono As String = ""
    Public blue As String = ""
    'Public syain As New DataTable
    Public Shared http As HttpClient

    Public Shared Function CountChar(ByVal s As String, ByVal c As Char) As Integer
        Return s.Length - s.Replace(c.ToString(), "").Length
    End Function
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'syain.Columns.Add("chmname")
        'syain.Columns.Add("sumname")
        'syain.Columns.Add("team")
        'sumload(0, "背中ポン美")
    End Sub
    Public Sub sumload(SumN As Integer, Sumname As String)
        Dim response As HttpResponse = Nothing
        Try
            'Leagueconnect()
            Dim password As String = Form1.token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & Form1.port & "/lol-summoner/v1/summoners?name=" & Sumname)
            ' " & "20180151")
        Catch Exception As Exception
            Form1.TextBox1.AppendText("Error : No Response 03" & vbCrLf)
            Timer1.Enabled = False
            'Form3.theme_end()
            'Form3.theme_start("butchersbridge\renata", 6000000)
            Me.Label1.Text = "off"
            blue = ""
            one = False
            Exit Sub
        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            '   oldid = 0
            'Me.TextBox10.Text = "xxxxxxxxxxxxxxxxxx"
        Else
            Dim grid = response.DynamicBody
            Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            Dim SumLev As Integer = grid.summonerLevel
            'Me.TextBox10.Text = jsonObj
            Form6.Panel1.Controls("Label" & SumN + 3).Text = SumLev
        End If
    End Sub

    Public Sub New()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        http = New HttpClient()
        InitializeComponent()
    End Sub



    Private Sub stats()
        'nono = Form1.champbox.Text
        nono = Form1.chmname
        Application.DoEvents()
        'If stdata.Columns.Count = 0 Then
        '    stdata.Columns.Add("kill", GetType(String))
        '    stdata.Columns.Add("death", GetType(String))
        '    stdata.Columns.Add("ass", GetType(String))
        'End If
        'stdata.Clear()
        Dim response As HttpResponse = Nothing
        Try
            'Leagueconnect()
            Dim password As String = Form1.token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/playerlist")
        Catch Exception As Exception
            Form1.TextBox1.AppendText("Error : No Response 03" & vbCrLf)
            Timer1.Enabled = False
            'Form3.theme_end()
            'Form3.theme_start("butchersbridge\renata", 6000000)
            Label1.Text = "off"
            blue = ""
            one = False
            Exit Sub
        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            '   oldid = 0
        Else
            Dim grid = response.DynamicBody
            Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            Dim kill(9) As Integer
            Dim death(9) As Integer
            Dim ass(9) As Integer
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0

            For Each item In grid
                Application.DoEvents()
                'Me.Controls("sc" & i).Text = grid(i).scores.kills & "/" & grid(i).scores.deaths & "/" & grid(i).scores.assists
                'Me.Controls("lvl" & i).Text = grid(i).level
                'For jj As Integer = 0 To 6
                '    Me.Pa7.Controls("Panel" & i & jj).BackgroundImage = Nothing
                '    'Me.Controls("TextBox" & jj).Text = Nothing
                'Next
                'grid(i).items.Reverse()
                Dim sok(7) As Boolean
                For ii As Integer = 0 To 7
                    sok(ii) = False
                Next
                Dim cname As String = grid(i).rawChampionName.ToString
                'Dim chn As String = grid(i).ChampionName.ToString
                Dim sumn As String = grid(i).riotIdGameName
                'Dim len As Integer = cname.Length
                Dim count As Integer = CountChar(cname, "_")
                Dim chn As String
                Dim parts As String()
                parts = Split(cname, "_", -1, CompareMethod.Text)
                If count > 2 Then
                    chn = parts(3)
                Else
                    chn = parts(1)
                End If

                'Dim ss As String = grid(i).
                If chn = "Name" Then
                    chn = "Teemo"
                End If
                If chn = "Wizard" Then
                    chn = "Neeko"
                End If
                Try
                    Me.Pa7.Controls("ch" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & Form1.vernew & "\champimage\" & chn & ".png"), 24, 24)
                Catch ex As Exception
                    Me.Pa7.Controls("ch" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & Form1.vernew & "\champimage\Neeko.png"), 24, 24)
                    System.IO.File.Copy("images\" & Form1.vernew & "\champimage\Neeko.png", "images\" & Form1.vernew & "\champimage\" & chn & ".png")
                End Try

                'Me.Controls("sumn" & i + 1).Text = sumn

                For Each items In grid(i).items
                    'TextBox1.AppendText(k & ":::" & (grid(i).items(j).slot) & vbCrLf)
                    Dim slot As Integer = grid(i).items(k).slot
                    sok(slot) = True
                    Dim item_bin As String = "images\" & Form1.vernew & "\item\" & grid(i).items(k).itemID & ".png"
                    Try
                        If System.IO.File.Exists(item_bin) Then

                        Else
                            Dim wc As New System.Net.WebClient()
                            wc.DownloadFile("https://ddragon.leagueoflegends.com/cdn/" & Form1.vernew & "/img/item/" & grid(i).items(k).itemID & ".png", "images\" & Form1.vernew & "\runeimage\" & grid(i).items(k).itemID & ".png")
                            wc.Dispose()
                        End If
                    Catch ex As Exception

                    End Try
                    Dim bmp As Bitmap = Nothing
                    Try
                        Using fs As New System.IO.FileStream(item_bin, IO.FileMode.Open, IO.FileAccess.Read)
                            bmp = New Bitmap(Image.FromStream(fs), 24, 24)
                        End Using
                    Catch ex As Exception
                        ' 読み込み失敗時は代替処理（例: 透明の24x24画像）
                        bmp = New Bitmap(24, 24)
                    End Try
                    Me.Pa7.Controls("Panel" & i & slot).BackgroundImage = bmp
                    'Me.Pa7.Controls("Panel" & i & slot).BackgroundImage = New Bitmap(Image.FromFile(item_bin), 24, 24)
                    If nono = chn Then
                        Me.Controls("TextBox" & slot).Text = grid(i).items(k).itemID

                    End If

                    'TextBox7.AppendText(nono & ":::" & chn & vbCrLf)
                    'If grid(i).items(k).slot = 6 Thenaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    '    Me.Pa7.Controls("Panel" & i & 6).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'Else
                    '    Me.Pa7.Controls("Panel" & i & j).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If
                    'If Form1.nono = i Then
                    '    Me.Controls("TextBox" & k).Text = grid(i).items(k).itemID
                    '    '    If Not (Me.Pa7.Controls("Panel" & i & j).BackgroundImage Is Nothing) Then
                    '    '        'Me.Pa7.Controls("Panel" & i & j).BackgroundImage.Save("K:\lol json20190626\WindowsApplication12\bin\Release\images\" & j + 1 & ".png")
                    '    '        My.Computer.FileSystem.CopyFile("item\" & grid(i).items(k).itemID & ".png", "K:\lol json20190626\WindowsApplication12\bin\Release\images\" & j + 1 & ".png", True)
                    '    '    Else
                    '    TextBox7.AppendText(j & ":::" & grid(i).items(k).itemID & ":" & grid(i).items(k).slot & vbCrLf)
                    '    '    End If
                    '    '    End If

                    'End If
                    'If grid(i).items(k).slot = 5 Then
                    '    Me.Pa7.Controls("Panel" & i & 5).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If
                    'If grid(i).items(k).slot = 4 Then
                    '    Me.Pa7.Controls("Panel" & i & 4).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If
                    'If grid(i).items(k).slot = 3 Then
                    '    Me.Pa7.Controls("Panel" & i & 3).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If
                    'If grid(i).items(k).slot = 2 Then
                    '    Me.Pa7.Controls("Panel" & i & 2).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If
                    'If grid(i).items(k).slot = 1 Then
                    '    Me.Pa7.Controls("Panel" & i & 1).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If
                    'If grid(i).items(k).slot = 0 Then
                    '    Me.Pa7.Controls("Panel" & i & 0).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                    'End If


                    'j += 1
                    k += 1

                Next
                For iii As Integer = 0 To 6
                    If sok(iii) = False Then
                        Me.Pa7.Controls("Panel" & i & iii).BackgroundImage = Nothing
                        If nono = chn Then
                            Me.Controls("TextBox" & iii).Text = Nothing
                        End If

                    End If
                Next

                'j = 0
                k = 0
                i += 1
            Next
            'If grid(i).riotIdGameName = "夜来香" Then
            '    Application.DoEvents()
            '    If old_k <> grid(i).scores.kills Then
            '        Application.DoEvents()
            '        'Killとった
            '        Timer10.Enabled = False
            '        'If Tabb = 96 Then
            '        Dim cmd As String
            '        '再生しているWAVEを停止する
            '        cmd = "stop " + aliasName
            '        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
            '        '  閉じる
            '        cmd = "close " + aliasName
            '        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
            '        'End If


            '        'Dim r As New System.Random()
            '        Dim Rnd As String = "9"
            '        Dim sond As String = "teemo\" & Rnd & ".mp3"
            '        Dim fileName As String = sond
            '        'ファイルを開く
            '        'cmd = "open """ + fileName + """ alias " + aliasName
            '        cmd = "open """ + fileName + """ type mpegvideo alias " + aliasName
            '        If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
            '            Return
            '        End If '再生する
            '        cmd = "play " + aliasName
            '        mciSendString(cmd, Nothing, 0, IntPtr.Zero)

            '        Timer10.Interval = 20000
            '        Timer10.Enabled = True
            '        old_k = grid(i).scores.kills
            '    End If
            '    If old_d <> grid(i).scores.deaths Then
            '        Application.DoEvents()
            '        'death
            '        Timer10.Enabled = False
            '        'If Tabb = 96 Then
            '        Dim cmd As String
            '        '再生しているWAVEを停止する
            '        cmd = "stop " + aliasName
            '        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
            '        '  閉じる
            '        cmd = "close " + aliasName
            '        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
            '        'End If


            '        'Dim r As New System.Random()
            '        Dim Rnd As String = "生きる資格はない"
            '        Dim sond As String = "teemo\" & Rnd & ".mp3"
            '        Dim fileName As String = sond
            '        'ファイルを開く
            '        'cmd = "open """ + fileName + """ alias " + aliasName
            '        cmd = "open """ + fileName + """ type mpegvideo alias " + aliasName
            '        If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
            '            Return
            '        End If '再生する
            '        cmd = "play " + aliasName
            '        mciSendString(cmd, Nothing, 0, IntPtr.Zero)

            '        Timer10.Interval = 20000
            '        Timer10.Enabled = True
            '        old_d = grid(i).scores.deaths
            '    End If
            'End If
            '    i += 1
            'Next
            'LVdata.Rows.RemoveAt(0)
            'ConvertDataTableToCsv(LVdata, "lvl.csv", True, False)
            'DataGridView1.DataSource = hisdata
            'TextBox8.Text = jsonObj

        End If
        ''https://127.0.0.1:52174/lol-match-history/v2/matchlist?begIndex=0&endIndex=10
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        stats()

    End Sub

    Public Sub onece()
        Timer1.Enabled = True
        Exit Sub
        nono = Form1.chmname
        Application.DoEvents()
        'If stdata.Columns.Count = 0 Then
        '    stdata.Columns.Add("kill", GetType(String))
        '    stdata.Columns.Add("death", GetType(String))
        '    stdata.Columns.Add("ass", GetType(String))
        'End If
        'stdata.Clear()
        clea()
        blue = ""
        Dim response As HttpResponse = Nothing
        Try
            'Leagueconnect()
            Dim password As String = Form1.token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/playerlist")
            '  response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/all-grid-champions")
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response" & vbCrLf)
            Exit Sub
            'Leagueconnect()
            'Dim password As String = token
            'http.Request.Accept = HttpContentTypes.ApplicationJson
            'http.Request.SetBasicAuthentication("riot", password)
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then

        Else
            Form3.syain.Rows.Clear()
            Dim grid = response.DynamicBody
            Dim teamscore As Integer = 0
            Dim bscore As Integer = 0
            Dim rscore As Integer = 0
            Dim i As Integer = 0
            Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            'TextBox7.Text = jsonObj
            Console.WriteLine("::::::::::" & jsonObj)
            For Each item In grid
                Dim cname As String = grid(i).rawChampionName.ToString
                'Console.WriteLine(Form1.vernew & cname)
                Dim len As Integer = cname.Length
                Dim last_n As Integer = cname.LastIndexOf("_") + 1
                Dim nn As Integer = len - last_n
                Dim chn As String = cname.Substring(last_n, nn)
                Dim sumn As String = grid(i).riotIdGameName
                Dim snm() As String = sumn.Split("#")
                'Dim sname As String =
                Dim team As String = (grid(i).team.ToString).Trim()
                Form3.syain.Rows.Add(chn, snm(0), team)

                Console.WriteLine("2 " & grid(i).rawChampionName.ToString)
                Console.WriteLine("2 " & grid(i).ChampionName.ToString)
                'sumload(i, sumn)


                If nono = chn Then
                    Dim sc1 As String = grid(i).summonerSpells.summonerSpellOne.rawDisplayName.ToString
                    Dim sns1 As Integer = 0
                    Dim sns2 As Integer = 0
                    Select Case sc1
                        Case "GeneratedTip_SummonerSpell_SummonerBoost_DisplayName"
                            sns1 = 1
                        Case "GeneratedTip_SummonerSpell_SummonerExhaust_DisplayName"
                            sns1 = 3
                        Case "GeneratedTip_SummonerSpell_SummonerFlash_DisplayName"
                            sns1 = 4
                        Case "GeneratedTip_SummonerSpell_SummonerBackTrack_DisplayName"
                            sns1 = 5
                        Case "GeneratedTip_SummonerSpell_SummonerGhost_DisplayName"
                            sns1 = 6
                        Case "GeneratedTip_SummonerSpell_SummonerHeal_DisplayName"
                            sns1 = 7
                        Case "GeneratedTip_SummonerSpell_SummonerSmite_DisplayName"
                            sns1 = 11
                        Case "GeneratedTip_SummonerSpell_SummonerTeleport_DisplayName"
                            sns1 = 12
                        Case "GeneratedTip_SummonerSpell_SummonerClarity_DisplayName"
                            sns1 = 13
                        Case "GeneratedTip_SummonerSpell_SummonerIgnite_DisplayName"
                            sns1 = 14
                        Case "GeneratedTip_SummonerSpell_SummonerBarrier_DisplayName"
                            sns1 = 21
                        Case "GeneratedTip_SummonerSpell_SummonerMark_DisplayName"
                            sns1 = 32
                        Case Else
                            sns1 = 5
                    End Select
                    'TextBox9.Text = sns1

                    Dim sc2 As String = grid(i).summonerSpells.summonerSpellTwo.rawDisplayName.ToString
                    Select Case sc2
                        Case "GeneratedTip_SummonerSpell_SummonerBoost_DisplayName"
                            sns2 = 1
                        Case "GeneratedTip_SummonerSpell_SummonerExhaust_DisplayName"
                            sns2 = 3
                        Case "GeneratedTip_SummonerSpell_SummonerFlash_DisplayName"
                            sns2 = 4
                        Case "GeneratedTip_SummonerSpell_SummonerBackTrack_DisplayName"
                            sns2 = 5
                        Case "GeneratedTip_SummonerSpell_SummonerGhost_DisplayName"
                            sns2 = 6
                        Case "GeneratedTip_SummonerSpell_SummonerHeal_DisplayName"
                            sns2 = 7
                        Case "GeneratedTip_SummonerSpell_SummonerSmite_DisplayName"
                            sns2 = 11
                        Case "GeneratedTip_SummonerSpell_SummonerTeleport_DisplayName"
                            sns2 = 12
                        Case "GeneratedTip_SummonerSpell_SummonerClarity_DisplayName"
                            sns2 = 13
                        Case "GeneratedTip_SummonerSpell_SummonerIgnite_DisplayName"
                            sns2 = 14
                        Case "GeneratedTip_SummonerSpell_SummonerBarrier_DisplayName"
                            sns2 = 21
                        Case "GeneratedTip_SummonerSpell_SummonerMark_DisplayName"
                            sns2 = 32
                        Case Else
                            sns2 = 5
                    End Select
                    'TextBox10.Text = sns2
                End If

                Dim atk As Integer
                Dim def As Integer
                Dim mag As Integer
                Dim dif As Integer

                'chn = "Ezreal"


                Me.Pa7.Controls("ch" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & Form1.vernew & "\champimage\" & chn & ".png"), 24, 24)

                'ch0.BackgroundImage = New Bitmap(Image.FromFile("images\14.8.1\champimage\Ahri.png"), 24, 24)
                'Me.Controls("sumn" & i + 1).Text = sumn
                For ii As Integer = 0 To Form1.champdata.Rows.Count - 1
                    Dim id As String = Form1.champdata.Rows(ii).Item("id")
                    If chn = id Then
                        atk = Form1.champdata.Rows(ii).Item("attack")
                        def = Form1.champdata.Rows(ii).Item("defense")
                        mag = Form1.champdata.Rows(ii).Item("magic")
                        dif = Form1.champdata.Rows(ii).Item("difficulty")
                        Exit For
                    End If
                Next


                If team = "ORDER" Then
                    bscore = bscore + atk + def + mag - dif
                Else
                    rscore = rscore + atk + def + mag - dif
                End If

                i += 1
            Next
            teamscore = bscore - rscore
            Label2.Text = "T" & teamscore & ":B " & bscore & ":R " & rscore
            DataGridView1.DataSource = Form3.syain
            TextBox7.AppendText(blue & vbCrLf)
            Timer1.Enabled = True
            'Form3.Timer1.Enabled = True
            'Form3.nn = 0
            'Form3.nn = 1
            'Form3.nns = 1
            'Form3.yy = 0
            'Form3.yy_bin = 0
            'Form3.bscore = bscore
            'Form3.rscore = rscore
            'Form3.teamscore = teamscore
            'Form3.score = teamscore
        End If

    End Sub


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If Timer1.Enabled = True Then
            Timer1.Enabled = False
            Label1.Text = "off"
        Else
            Timer1.Enabled = True
            Label1.Text = "on"
            'onece()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clea()
    End Sub

    Public Sub clea()
        For i As Integer = 0 To 9
            For jj As Integer = 0 To 6
                Me.Pa7.Controls("Panel" & i & jj).BackgroundImage = Nothing
                Me.Pa7.Controls("ch" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & Form1.vernew & "\champimage\Teemo.png"), 24, 24)
            Next
            Me.Pa7.Controls("lvl" & i).Text = "00"
            Me.Pa7.Controls("sc" & i).Text = "00"
        Next
        For i As Integer = 0 To 6
            Me.Controls("TextBox" & i).Text = "00"

        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        onece()

    End Sub
End Class