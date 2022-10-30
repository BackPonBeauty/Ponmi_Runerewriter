Option Strict Off
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization.Json
Imports Newtonsoft.Json
Imports System.Linq
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json.Linq
Imports System.Runtime.InteropServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Security.Principal
Imports System.Windows.Forms
Imports EasyHttp.Http
Imports System.Management
Imports JsonFx.Json
Imports System.Security.Permissions
Imports System.Threading
Imports System.Drawing.Imaging
Imports Microsoft.DirectX.DirectSound

'Partial Public Class RunesReformed
'    Inherits Form
'    Public Shared Property port As String
'    Public Shared Property token As String

'End Class

Public Class Form1
    '<SecurityPermission(SecurityAction.Demand,
    'Flags:=SecurityPermissionFlag.UnmanagedCode)>
    'Protected Overrides Sub WndProc(ByRef m As Message)
    '    Const WM_NCLBUTTONDBLCLK As Integer = &HA3

    '    If m.Msg = WM_NCLBUTTONDBLCLK Then
    '        '非クライアント領域がダブルクリックされた時
    '        m.Result = IntPtr.Zero
    '        Return
    '    End If

    '    MyBase.WndProc(m)
    'End Sub

    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer


    Private aliasName As String = "MediaFile"

    Public sumspe As New DataTable
    Public sumspe_bin As New DataTable
    Public sumspe1 As New DataTable
    Public orespe As New DataTable
    Public oredatabin As New DataTable
    Public country As String
    Public country1 As String
    Public country2 As String
    Public oredata As New DataTable
    Public champdata As New DataTable
    Public runedata As New DataTable
    Public sumspedata As New DataTable
    Public runereformdata As New DataTable
    Public LVdata As New DataTable
    Public hisdata As New DataTable
    Public stdata As New DataTable
    Public pagedata As New DataTable
    Public reg As String = "jp"
    Public api As String = "jp"
    Public vernew As String
    Public verold As String
    Public pri As Integer
    Public sec As Integer
    Public mae As Integer
    Public oldii As Integer
    Public dame1 As Integer
    Public dame2 As Integer
    Public bin2 As Integer
    Public bin3 As Integer
    Public bin4 As Integer
    Public bon As Integer
    Public alp As Integer = -200
    Public dnc As Integer
    Private pc() As System.Windows.Forms.Button
    Private lab() As System.Windows.Forms.Label
    Public runo As Integer
    Public Shared token As String
    Public Shared port As String
    Public Shared http As HttpClient
    Public chroma_old As String = "81000"
    Public ccchamp As Integer = 22
    Public onece As Boolean = False
    Public checkacc As String
    Public checkid As String
    Public checkdn As String
    Public checksl As Integer
    Public aram As String = "aram"
    Public sspfr As Boolean = True
    Public L As Boolean = False

    Public Orepages As String

    Public old_k As Integer = 0
    Public old_d As Integer = 0
    Public chmname As String
    Public nono As Integer = 0
    Dim once As Boolean = False
    ' ウィンドウハンドルを取得
    Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" _
    (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    ' 子ウィンドウハンドルを取得
    Declare Function FindWindowEx Lib "user32.dll" Alias "FindWindowExA" _
    (ByVal hwndParent As Integer, ByVal hwndChildAfter As Integer,
    ByVal lpszClass As String, ByVal lpszWindow As String) As Integer
    '他のアプリにメッセージを送る。返り値 integer
    Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" _
    (ByVal hWnd As Integer, ByVal MSG As Integer,
    ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '他のアプリにメッセージを送る。返り値 Stringbuilder
    Declare Function SendMessageStr Lib "user32.dll" Alias "SendMessageA" _
    (ByVal hWnd As Integer, ByVal MSG As Integer,
    ByVal wParam As Integer, ByVal lParam As StringBuilder) As Integer

    Public Const BM_CLICK = &HF5                                   'マウスクリック コード
    Public Const WM_LBUTTONDOWN As Short = &H201                   'マウスレフトボタンdown
    Public Const WM_LBUTTONUP As Short = &H202                     'マウスレフトボタンup
    Public Const WM_RIGHT As Short = &H39　　　　　　　　　　　　　'右移動(→)キーコード
    Public Const WM_SPACE As Short = &H32　　　　　　　　　　　　　'スペースキーコード

    Public Sub New()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
        http = New HttpClient()
        InitializeComponent()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles Button5.Click
        'Button1をクリックするごとにこのフォームを常に手前または解除します。
        Me.TopMost = Not Me.TopMost
    End Sub

    Private Sub Form1_Show(sender As Object, e As EventArgs) Handles MyBase.Shown
        Application.DoEvents()

        'メッセージボックスを表示する 
        'Dim result As DialogResult = MessageBox.Show("ARAMのデータを読み込みますか？",
        '                                     "質問",
        '                                     MessageBoxButtons.YesNo,
        '                                     MessageBoxIcon.Exclamation,
        '                                     MessageBoxDefaultButton.Button2)

        '何が選択されたか調べる 
        'If result = DialogResult.Yes Then
        Orepages = "orepage.csv"
            aram = "aram"
        'ElseIf result = DialogResult.No Then
        'Orepages = "orepage2.csv"
        '    aram = "5v5"
        'End If

        Dim line As String = ""
        Dim al As New ArrayList
        runo = 5
        pri = 0
        sec = 0
        mae = 0
        oldii = 1000
        dame1 = 1000
        dame2 = 1000
        bin4 = 0
        bin2 = 0
        bin3 = 0
        bon = 0
        sumspe.Columns.Add("id", GetType(String))
        sumspe1.Columns.Add("id", GetType(String))
        sumspe_bin.Columns.Add("id", GetType(String))
        If orespe.Columns.Count = 0 Then
            orespe.Columns.Add("pn", GetType(String))
            orespe.Columns.Add("rp", GetType(String))
            orespe.Columns.Add("r1", GetType(String))
            orespe.Columns.Add("r2", GetType(String))
            orespe.Columns.Add("r3", GetType(String))
            orespe.Columns.Add("r4", GetType(String))
            orespe.Columns.Add("rs", GetType(String))
            orespe.Columns.Add("r5", GetType(String))
            orespe.Columns.Add("r6", GetType(String))
            orespe.Columns.Add("r7", GetType(String))
            orespe.Columns.Add("r8", GetType(String))
            orespe.Columns.Add("r9", GetType(String))
            orespe.Columns.Add("id", GetType(Integer))
            orespe.Columns.Add("s1", GetType(String))
            orespe.Columns.Add("s2", GetType(String))
        End If


        Dim csvDir As String = "." ' "C:\"
        'CSVファイルの名前
        Dim csvFileName As String = "champdata.csv"

        '接続文字列
        Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As New System.Data.OleDb.OleDbConnection(conString)

        Dim commText As String = "SELECT * FROM [" + csvFileName + "]"
        Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)

        da.Fill(champdata)

        Timer1.Enabled = False
        'Exit Sub '*************************************************************************************

        Using sr As StreamReader = New StreamReader(
          "name.cfg", Encoding.GetEncoding("Shift_JIS"))

            line = sr.ReadLine()
            Do Until line Is Nothing
                al.Add(line)
                line = sr.ReadLine()
            Loop

        End Using
        'TextBox2.Text = al.Item(0)
        'TextBox3.Text = al.Item(1)
        'checkid = al.Item(1)
        'TextBox4.Text = al.Item(2)
        country1 = al.Item(3)
        country2 = al.Item(4)
        verold = al.Item(5)
        api = al.Item(2)
        al.Clear()
        DeleteCheck.Checked = True

        GetSummonerId()
        TextBox2.Text = checkdn
        TextBox3.Text = checkid
        TextBox4.Text = checksl
        Begin()

        AddHandler Me.r0.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        AddHandler Me.r1.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        AddHandler Me.r2.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        AddHandler Me.r3.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        AddHandler Me.r4.MouseEnter, AddressOf Me.RuneButtons_MouseHover2

        AddHandler Me.s0.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        AddHandler Me.s2.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        AddHandler Me.s3.MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        s0.Tag = 1
        s2.Tag = 1
        s3.Tag = 1
        r0.Tag = 1
        r1.Tag = 1
        r2.Tag = 1
        r3.Tag = 1
        r4.Tag = 1

        smsp1.SelectedIndex = 2
        smsp2.SelectedIndex = 10
        Leagueconnect()
        Timer1.Enabled = True
        history()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sn As String = TextBox2.Text
        api = TextBox4.Text
        Dim sl As String
        Dim enc As Encoding = Encoding.GetEncoding(932)
        '     Dim s As String = NAT(sn, reg)
        If sn.Length = enc.GetByteCount(sn) Then
            sl = sn.ToLower
        Else
            sl = sn
        End If
        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        Try
            '  uu = "https://" & reg & ".api.pvp.net/api/lol/" & reg & "/v1.4/summoner/by-name/" & sn & "?api_key=" & apis 
            uu = "https://" & reg & "1.api.riotgames.com/lol/summoner/v3/summoners/by-name/" & sn & "?api_key=" & api
            sr = webclient.OpenRead(uu)
            Dim srRead As New System.IO.StreamReader(sr)
            '内容をすべて読み込む
            Dim s As String = srRead.ReadToEnd()
            Do Until srRead.EndOfStream

            Loop
            'ファイルを閉じる
            srRead.Close()

            '結果を返す

            Dim jsonObj As Object = JsonConvert.DeserializeObject(s)

            If jsonObj("id") IsNot Nothing Then
                Dim sa As String = jsonObj("id").ToString
                TextBox3.Text = sa
                TextBox1.AppendText(sl & " : " & sa & vbCrLf)
            End If

            Dim textFile As System.IO.StreamWriter
            textFile = New System.IO.StreamWriter("name.cfg", False, System.Text.Encoding.Default)
            textFile.WriteLine(TextBox2.Text)
            textFile.WriteLine(TextBox3.Text)
            textFile.WriteLine(TextBox4.Text)
            textFile.WriteLine(country1)
            textFile.WriteLine(country2)
            textFile.WriteLine(vernew)
            textFile.Close()
            TextBox1.AppendText("API Key OK" & vbCrLf)
        Catch ex As System.Net.WebException
            TextBox1.AppendText("API Key Error" & vbCrLf)

        End Try
        '    Call champlvl()
    End Sub

    Private Sub Begin()

        champdata.Clear()
        runedata.Clear()
        runereformdata.Clear()

        Application.DoEvents()


        'Dim line As String = ""
        'Dim al As New ArrayList

        'Using sr As StreamReader = New StreamReader(
        '  "ver.cfg", Encoding.GetEncoding("Shift_JIS"))

        '    line = sr.ReadLine()
        '    Do Until line Is Nothing
        '        al.Add(line)
        '        line = sr.ReadLine()
        '    Loop

        'End Using

        ''For i As Integer = 0 To al.Count - 1
        ''    Console.WriteLine(al.Item(i))
        ''Next i

        'Dim l As Integer = al.Item(0).length
        ''     Dim j As Integer = al.Item(0).LastIndexOf("\"c)
        'Dim verold = al.Item(0)
        ''    TextBox1.AppendText("Version = " & verold & vbCrLf)

        'al.Clear()
        vernew = VersionCheck()
        TextBox1.AppendText("Version check " & verold & " : " & vernew & vbCrLf)

        If vernew = "0.0.0" Then
            TextBox1.AppendText("Version Check : Connection Error" & vbCrLf)
            Exit Sub
        End If
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\loading\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\loading\")
        End If
        'System.Threading.Thread.Sleep(5000)

        If verold = vernew Then
            'TextBox1.AppendText("Update Not Found" & vbCrLf & "Loading LocalData" & vbCrLf)
            Call LocalRuneDataLoad(vernew)

            Call LocalSumspeLoad()
        Else
            TextBox1.AppendText("Update Found" & vbCrLf & "Loading OnlineData" & vbCrLf)
            Call OnlineRuneDataLoad(vernew)
            Call LocalSumspeLoad()
            Call OnlineChampDataLoad(vernew)
            Call LocalRuneReformLoad()
            'Call LocalLVload()
            TextBox1.AppendText("Updated Version" & vbCrLf)
        End If
        Dim textFile As System.IO.StreamWriter
        textFile = New System.IO.StreamWriter("name.cfg", False, System.Text.Encoding.Default)
        textFile.WriteLine(TextBox2.Text)
        textFile.WriteLine(TextBox3.Text)
        textFile.WriteLine(TextBox4.Text)
        textFile.WriteLine(country1)
        textFile.WriteLine(country2)

        textFile.WriteLine(vernew)
        textFile.Close()




        Call LocalLVload()

        t0.Text = "1"
        t1.Text = "1"
        t2.Text = "1"
        t3.Text = "1"
        t4.Text = "1"

        y0.Text = "1"

        y2.Text = "1"
        y3.Text = "1"

        mmm1.Text = "0"
        mmm2.Text = "0"
        mmm3.Text = "0"


        Label1.Text = vernew

        Call MyRuneSet()
        Call Tiles()
        'TextBox1.AppendText("Changed" & vbCrLf)
        'DataGridView1.DataSource = champdata
        Form2.Show()
        Form3.Show()
        'Form4.Show()
        'Form5.Show()
    End Sub

    Private Sub Tiles()

        Dim files As String() = System.IO.Directory.GetFiles(".\loading\", "*.jpg", System.IO.SearchOption.AllDirectories)
        'ListBox1に結果を表示する
        CBox.Items.AddRange(files)
    End Sub

    Public Shared Function VersionCheck() As String
        Application.DoEvents()
        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        Try
            uu = "https://ddragon.leagueoflegends.com/api/versions.json"
            sr = webclient.OpenRead(uu)
            Dim srRead As New System.IO.StreamReader(sr)
            '内容をすべて読み込む
            Dim se As String = srRead.ReadToEnd()
            srRead.Close()
            Dim ss As String() = se.Split(","c)
            Dim qq As String = ss(0).Replace("""", " "c)
            qq = qq.Replace("[", " "c)
            qq = qq.TrimStart()
            qq = qq.TrimEnd()
            Dim ver0 As String = qq
            Return ver0

        Catch ex As System.Net.WebException
            Dim ver0 As String = "0.0.0"
            Return ver0
        End Try
    End Function


    Private Sub MyRuneSet()
        Application.DoEvents()
        Dim rc As Integer = runedata.Rows.Count - 1

        Me.pc = New System.Windows.Forms.Button(rc) {}
        Dim xx As Integer = 0
        Dim yy As Integer = 0
        Dim siz As Integer = 45
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\runeimage\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\runeimage\")
        End If
        'TextBox1.AppendText("RuneImage2Loading" & patch & vbCrLf)
        Dim c4 As Integer = 0
        For i As Integer = 0 To rc

            Dim icon As String = runedata.Rows(i).Item("icon")
            Dim name As String = runedata.Rows(i).Item("name")
            Dim id As String = runedata.Rows(i).Item("id")
            Dim csd As String = runedata.Rows(i).Item("csd")
            Dim c1 As String = runedata.Rows(i).Item("c1")
            Dim c2 As String = runedata.Rows(i).Item("c2")
            Dim c3 As String = runedata.Rows(i).Item("c3")
            'System.Threading.Thread.Sleep(1000)
            'TextBox1.AppendText(icon & vbCrLf)

            If c1 = pri Or c2 = 0 Then
                Me.pc(i) = New System.Windows.Forms.Button
                If c2 = 0 Then
                    Me.Controls("r00").Controls.Add(pc(i))
                    c3 = c4
                    c4 = c4 + 1
                Else
                    Me.Controls("r" & c1 & c2).Controls.Add(pc(i))
                End If
                Me.pc(i).Height = siz
                Me.pc(i).Width = siz
                'Me.pc(i).Top = yy
                'Me.pc(i).Left = xx
                Me.pc(i).BackColor = Color.Black
                Me.pc(i).FlatStyle = FlatStyle.Flat
                Me.pc(i).Name = i.ToString
                Me.pc(i).Text = id
                Me.pc(i).Tag = id
                Me.pc(i).Font = New Font("Arial", 1)

                Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
                If System.IO.File.Exists(fileName) Then
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        'Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).Refresh()
                        'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                        ''Me.pc(i).Enabled = False
                    End If


                    img.Dispose()
                    img = Nothing
                    imm = Nothing
                Else
                    Dim wc As New System.Net.WebClient()
                    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/" & icon, "images\" & patch & "\runeimage\" & id & ".png")
                    wc.Dispose()


                    Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(im)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        'Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).Refresh()
                        ''Me.pc(i).Enabled = False
                        'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    End If
                    img.Dispose()
                    img = Nothing

                    im = Nothing
                End If
                'If c1 = "0" Then
                '    xx = 0
                '    yy = yy + 25
                'ElseIf c2 = "1" Then
                '    xx = xx + 50
                'Else
                '    xx = xx + 25
                'End If

                xx = c3 * siz
                Me.pc(i).Top = yy
                Me.pc(i).Left = xx
            End If
            If c1 = "5" Then
                Me.pc(i) = New System.Windows.Forms.Button
                Me.Controls("m" & c2).Controls.Add(pc(i))
                Me.pc(i).Height = siz
                Me.pc(i).Width = siz
                'Me.pc(i).Top = yy
                'Me.pc(i).Left = xx
                Me.pc(i).BackColor = Color.Black
                Me.pc(i).FlatStyle = FlatStyle.Flat
                Me.pc(i).Name = i.ToString
                Me.pc(i).Text = id
                Me.pc(i).Tag = id
                Me.pc(i).Font = New Font("Arial", 1)


                Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
                If System.IO.File.Exists(fileName) Then
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)

                    If c2 <> 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click5
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        'Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).Refresh()
                        'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                        ''Me.pc(i).Enabled = False
                    End If


                    img.Dispose()
                    img = Nothing

                    imm = Nothing
                Else
                    Dim wc As New System.Net.WebClient()
                    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/" & icon, "images\" & patch & "\runeimage\" & id & ".png")
                    wc.Dispose()


                    Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(im)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click5
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        'Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).Refresh()
                        ''Me.pc(i).Enabled = False
                        'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    End If
                    img.Dispose()
                    img = Nothing

                    im = Nothing
                End If

                xx = c3 * siz
                Me.pc(i).Top = yy
                Me.pc(i).Left = xx
            End If
        Next

        'Me.Controls("t0").Text = "8100"
        'Dim immm As String = "images\" & vernew & "\runeimage\" & "8100.png"
        'Dim imgg As Image = Image.FromFile(immm)
        'Me.Controls("r0").BackgroundImage = imgg
        'imgg.Dispose()
        'imgg = Nothing

        'immm = Nothing

        MyRuneSets()

    End Sub

    Private Sub MyRuneSets()
        Dim rc As Integer = runedata.Rows.Count - 1

        Me.pc = New System.Windows.Forms.Button(rc) {}
        Dim xx As Integer = 0
        Dim yy As Integer = 0
        Dim siz As Integer = 45
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\runeimage\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\runeimage\")
        End If
        'TextBox1.AppendText("RuneImage2Loading" & patch & vbCrLf)
        Dim c4 As Integer = 0
        For i As Integer = 0 To rc

            Dim icon As String = runedata.Rows(i).Item("icon")
            Dim name As String = runedata.Rows(i).Item("name")
            Dim id As String = runedata.Rows(i).Item("id")
            Dim csd As String = runedata.Rows(i).Item("csd")
            Dim c1 As String = runedata.Rows(i).Item("c1")
            Dim c2 As String = runedata.Rows(i).Item("c2")
            Dim c3 As String = runedata.Rows(i).Item("c3")

            'System.Threading.Thread.Sleep(1000)
            'TextBox1.AppendText(icon & vbCrLf)
            If c1 = pri Or c2 = 0 Then


                Me.pc(i) = New System.Windows.Forms.Button
                If c2 = 0 Then
                    Me.Controls("s00").Controls.Add(pc(i))
                    'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click3
                    'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    c3 = c4
                    c4 = c4 + 1

                Else
                    Me.Controls("s" & c1 & c2).Controls.Add(pc(i))
                    'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click4
                    'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                End If

                Me.pc(i).Height = siz
                Me.pc(i).Width = siz
                'Me.pc(i).Top = yy
                'Me.pc(i).Left = xx
                Me.pc(i).BackColor = Color.Black
                Me.pc(i).FlatStyle = FlatStyle.Flat
                Me.pc(i).Name = i.ToString
                Me.pc(i).Text = id
                Me.pc(i).Tag = id
                Me.pc(i).Font = New Font("Arial", 1)


                Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
                If System.IO.File.Exists(fileName) Then
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click3
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        'Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).Refresh()
                        ''AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click4
                        ''AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover

                    End If


                    img.Dispose()
                    img = Nothing

                    imm = Nothing
                Else
                    Dim wc As New System.Net.WebClient()
                    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/" & icon, "images\" & patch & "\runeimage\" & id & ".png")
                    wc.Dispose()


                    Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(im)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click3
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        'Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).Refresh()
                        'Me.pc(i).Enabled = False
                        'AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click4
                        'AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover

                    End If
                    img.Dispose()
                    img = Nothing

                    im = Nothing
                End If


                'If c1 = "0" Then
                '    xx = 0
                '    yy = yy + 25
                'ElseIf c2 = "1" Then
                '    xx = xx + 50
                'Else
                '    xx = xx + 25
                'End If

                xx = c3 * siz



                Me.pc(i).Top = yy
                Me.pc(i).Left = xx
            End If

        Next

        'dame1 = 0
        'dame2 = 1
        'SecRuneRefresh(15, 1)
        'Me.Controls("y0").Text = "8300"
        'Dim immm As String = "images\" & vernew & "\runeimage\" & "8300.png"
        'Dim imgg As Image = Image.FromFile(immm)
        'Me.Controls("s0").BackgroundImage = imgg

        'imgg.Dispose()
        'imgg = Nothing

        'immm = Nothing


        'TextBox1.AppendText("RuneImage2Loaded" & vbCrLf)
        DataGridView1.DataSource = runedata
        champbox.SelectedIndex = 0
    End Sub



    Private Sub RuneButtons_Click2(ByVal sender As Object, ByVal e As EventArgs)

        Dim ii As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        Dim c1 As Integer = runedata.Rows(ii).Item("c1")
        Dim c2 As Integer = runedata.Rows(ii).Item("c2")
        Dim c3 As Integer = runedata.Rows(ii).Item("c3")
        Dim idd As Integer = runedata.Rows(ii).Item("id")
        'If dame2 = c1 Then
        '    Exit Sub
        'End If
        dame1 = c1



        If c2 = 0 Then
            r0.BackgroundImage = Nothing
            r1.BackgroundImage = Nothing
            r2.BackgroundImage = Nothing
            r3.BackgroundImage = Nothing
            r4.BackgroundImage = Nothing

            r1.Left = -50
            r2.Left = -50
            r3.Left = -50
            r4.Left = -50

            s0.Left = -50
            s2.Left = -50
            s3.Left = -50


            t0.Text = 0
            t1.Text = 0
            t2.Text = 0
            t3.Text = 0
            t4.Text = 0

            y0.Text = 0
            y2.Text = 0
            y3.Text = 0

            s4.Left = Me.Controls("s00").Left + (c1 * 45)
            s5.Left = Me.Controls("s00").Left
            'r0.BackgroundImage.Dispose()
            Me.Controls("t" & c2).Text = idd
            Dim im As String = "images\" & vernew & "\runeimage\" & idd & ".png"
            Dim img As Image = Image.FromFile(im)
            r0.BackgroundImage = img


            r0.Left = Me.Controls("r00").Left + (c1 * 45)
            'r0.Width = 45
            'r0.Height = 45
            r0.Tag = ii
            'img.Dispose()
            'img = Nothing

            'im = Nothing


            PriRuneRefresh(ii, c1)
        Else
            'Me.Controls("r" & c2).BackgroundImage.Dispose()
            Me.Controls("t" & c2).Text = idd
            Dim im As String = "images\" & vernew & "\runeimage\" & idd & ".png"
            Dim img As Image = Image.FromFile(im)
            Me.Controls("r" & c2).BackgroundImage = img

            Me.Controls("r" & c2).Left = Me.Controls("r0" & c1).Left + (c3 * 45)
            Me.Controls("r" & c2).Tag = ii
            'img.Dispose()
            'img = Nothing

            'im = Nothing

        End If

        'Pagedataload2()

        'TextBox1.AppendText(c1 & vbCrLf & c2 & vbCrLf & c3 & vbCrLf & idd & vbCrLf)
        'Select Case c2

        '    Case 0
        '        runo = 5
        '    Case 1
        '        runo = 6
        '    Case 2
        '        runo = 7
        '    Case 3
        '        runo = 8
        '    Case 4
        '        runo = 9
        'End Select
        '  


        'mae = idd
    End Sub

    Private Sub RuneButtons_Click3(ByVal sender As Object, ByVal e As EventArgs)

        Dim ii As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        Dim c1 As Integer = runedata.Rows(ii).Item("c1")
        Dim c2 As Integer = runedata.Rows(ii).Item("c2")
        Dim c3 As Integer = runedata.Rows(ii).Item("c3")
        Dim idd As Integer = runedata.Rows(ii).Item("id")
        If dame1 = c1 Then
            Exit Sub
        End If
        dame2 = c1
        '

        s0.BackgroundImage = Nothing
        s2.BackgroundImage = Nothing
        s3.BackgroundImage = Nothing

        s2.Left = -50
        s3.Left = -50
        s5.Left = -500

        y0.Text = 0
        y2.Text = 0
        y3.Text = 0

        'Me.Controls("s0").BackgroundImage.Dispose()
        Me.Controls("y0").Text = idd
        Dim imm As String = "images\" & vernew & "\runeimage\" & idd & ".png"
        Dim img As Image = Image.FromFile(imm)
        Me.Controls("s0").BackgroundImage = img

        s0.Left = Me.Controls("s00").Left + (c1 * 45)
        s0.Tag = ii
        SecRuneRefresh(ii, c1)
        'img.Dispose()
        'img = Nothing

        'imm = Nothing


        'TextBox1.AppendText(c1 & vbCrLf & c2 & vbCrLf & c3 & vbCrLf & ii & vbCrLf)

    End Sub

    Private Sub RuneButtons_Click4(ByVal sender As Object, ByVal e As EventArgs)

        Dim ii As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        Dim c1 As Integer = runedata.Rows(ii).Item("c1")
        Dim c2 As Integer = runedata.Rows(ii).Item("c2")
        Dim c3 As Integer = runedata.Rows(ii).Item("c3")
        Dim idd As Integer = runedata.Rows(ii).Item("id")



        If bon = 0 Then
            'Me.Controls("s2").BackgroundImage.Dispose()
            Me.Controls("y2").Text = idd
            Dim im As String = "images\" & vernew & "\runeimage\" & idd & ".png"
            Dim img As Image = Image.FromFile(im)
            Me.Controls("s2").BackgroundImage = img




            Me.Controls("s2").Left = Me.Controls("s0" & c2).Left + (c3 * 45)
            Me.Controls("s2").Top = Me.Controls("s0" & c2).Top
            Me.Controls("s2").Tag = ii
            bon = 1
            bin3 = 2
            bin2 = c2
            'img.Dispose()
            'img = Nothing

            'im = Nothing


        Else
            If bin2 = c2 Then

            Else
                If bin3 = 2 Then
                    bin3 = 3
                Else
                    bin3 = 2
                End If
            End If
            'Me.Controls("s" & bin3).BackgroundImage.Dispose()
            Me.Controls("y" & bin3).Text = idd
            Dim im As String = "images\" & vernew & "\runeimage\" & idd & ".png"
            Dim img As Image = Image.FromFile(im)
            Me.Controls("s" & bin3).BackgroundImage = img


            Me.Controls("s" & bin3).Left = Me.Controls("s0" & c2).Left + (c3 * 45)
            Me.Controls("s" & bin3).Top = Me.Controls("s0" & c2).Top
            Me.Controls("s" & bin3).Tag = ii
            bin2 = c2
            'img.Dispose()
            'img = Nothing

            'im = Nothing
        End If

        'Me.Controls("y" & c2).Text = idd
        'Dim im As String = "images\" & vernew & "\runeimage\" & idd & ".png"
        'Me.Controls("s" & c2).BackgroundImage = Image.FromFile(im)
        'TextBox1.AppendText(c1 & vbCrLf & c2 & vbCrLf & c3 & vbCrLf & ii & vbCrLf)

    End Sub

    Private Sub RuneButtons_Click5(ByVal sender As Object, ByVal e As EventArgs)

        Dim ii As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        Dim c1 As Integer = runedata.Rows(ii).Item("c1")
        Dim c2 As Integer = runedata.Rows(ii).Item("c2")
        Dim c3 As Integer = runedata.Rows(ii).Item("c3")
        Dim idd As Integer = runedata.Rows(ii).Item("id")
        ''If dame1 = c1 Then
        ''    Exit Sub
        ''End If
        ''dame2 = c1
        ''

        ''s0.BackgroundImage = Nothing
        ''s2.BackgroundImage = Nothing
        ''s3.BackgroundImage = Nothing

        ''s2.Left = -50
        ''s3.Left = -50
        ''s5.Left = -500

        ''y0.Text = 0
        ''y2.Text = 0
        ''y3.Text = 0

        ''Me.Controls("s0").BackgroundImage.Dispose()
        ''Me.Controls("y0").Text = idd
        Dim imm As String = "images\" & vernew & "\runeimage\" & idd & ".png"
        Dim img As Image = Image.FromFile(imm)
        Me.Controls("mm" & c2).BackgroundImage = img

        Me.Controls("mm" & c2).Left = Me.Controls("m" & c2).Left + (c3 * 45)
        Me.Controls("mm" & c2).Tag = ii
        Me.Controls("mmm" & c2).Text = idd
        AddHandler Me.Controls("mm" & c2).MouseEnter, AddressOf Me.RuneButtons_MouseHover2
        ''SecRuneRefresh(ii, c1)
        'img.Dispose()
        'img = Nothing

        'imm = Nothing


        TextBox1.AppendText(c1 & c2 & c3 & vbCrLf & idd & vbCrLf)

    End Sub

    Private Sub PriRuneRefresh(ii As Integer, pri As Integer)
        Dim rc As Integer = runedata.Rows.Count - 1

        Me.pc = New System.Windows.Forms.Button(rc) {}
        Dim xx As Integer = 0
        Dim yy As Integer = 0
        Dim siz As Integer = 45
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\runeimage\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\runeimage\")
        End If
        'TextBox1.AppendText("RuneImage2Loading" & patch & vbCrLf)
        Dim c4 As Integer = 0
        For i As Integer = 0 To rc

            Dim icon As String = runedata.Rows(i).Item("icon")
            Dim name As String = runedata.Rows(i).Item("name")
            Dim id As String = runedata.Rows(i).Item("id")
            Dim csd As String = runedata.Rows(i).Item("csd")
            Dim c1 As String = runedata.Rows(i).Item("c1")
            Dim c2 As String = runedata.Rows(i).Item("c2")
            Dim c3 As String = runedata.Rows(i).Item("c3")
            'System.Threading.Thread.Sleep(1000)
            'TextBox1.AppendText(icon & vbCrLf)
            Me.Controls("r0" & c2).Controls.Remove(Me.Controls("r0" & c2).Controls(i.ToString))
            If c1 = pri Or c2 = 0 Then




                Me.pc(i) = New System.Windows.Forms.Button
                If c2 = 0 Then
                    Me.Controls("r00").Controls.Add(pc(i))
                    c3 = c4
                    c4 = c4 + 1
                Else
                    Me.Controls("r0" & c2).Controls.Add(pc(i))
                End If
                Me.pc(i).Height = siz
                Me.pc(i).Width = siz
                'Me.pc(i).Top = yy
                'Me.pc(i).Left = xx
                Me.pc(i).BackColor = Color.Black
                Me.pc(i).FlatStyle = FlatStyle.Flat
                Me.pc(i).Name = i.ToString
                Me.pc(i).Text = id
                Me.pc(i).Tag = id
                Me.pc(i).Font = New Font("Arial", 1)


                Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
                If System.IO.File.Exists(fileName) Then
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)
                    Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                    Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)

                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        Me.pc(i).Refresh()
                        'Me.pc(i).Enabled = False
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    End If


                    img.Dispose()
                    img = Nothing

                    imm = Nothing
                Else
                    Dim wc As New System.Net.WebClient()
                    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/" & icon, "images\" & patch & "\runeimage\" & id & ".png")
                    wc.Dispose()


                    Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(im)
                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)

                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        Me.pc(i).Refresh()
                        'Me.pc(i).Enabled = False
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click2
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    End If
                    img.Dispose()
                    img = Nothing

                    im = Nothing
                End If


                'If c1 = "0" Then
                '    xx = 0
                '    yy = yy + 25
                'ElseIf c2 = "1" Then
                '    xx = xx + 50
                'Else
                '    xx = xx + 25
                'End If

                xx = c3 * siz



                Me.pc(i).Top = yy
                Me.pc(i).Left = xx
            End If

        Next

        'If pri = sec Then
        '    sec = sec + 1
        '    If sec > 4 Then
        '        sec = 0
        '    End If
        '    SecRuneRefresh(ii, sec)
        'End If
    End Sub

    Private Sub SecRuneRefresh(ii As Integer, sec As Integer)
        Dim rc As Integer = runedata.Rows.Count - 1

        Me.pc = New System.Windows.Forms.Button(rc) {}
        Dim xx As Integer = 0
        Dim yy As Integer = 0
        Dim siz As Integer = 45
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\runeimage\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\runeimage\")
        End If
        'TextBox1.AppendText("RuneImage2Loading" & patch & vbCrLf)
        Dim c4 As Integer = 0
        For i As Integer = 0 To rc

            Dim icon As String = runedata.Rows(i).Item("icon")
            Dim name As String = runedata.Rows(i).Item("name")
            Dim id As String = runedata.Rows(i).Item("id")
            Dim csd As String = runedata.Rows(i).Item("csd")
            Dim c1 As String = runedata.Rows(i).Item("c1")
            Dim c2 As String = runedata.Rows(i).Item("c2")
            Dim c3 As String = runedata.Rows(i).Item("c3")
            'System.Threading.Thread.Sleep(1000)
            'TextBox1.AppendText(icon & vbCrLf)
            Me.Controls("s0" & c2).Controls.Remove(Me.Controls("s0" & c2).Controls(i.ToString))
            If c1 = sec Or c2 = 0 Then




                Me.pc(i) = New System.Windows.Forms.Button
                If c2 = 0 Then
                    Me.Controls("s00").Controls.Add(pc(i))
                    c3 = c4
                    c4 = c4 + 1
                Else
                    Me.Controls("s0" & c2).Controls.Add(pc(i))
                End If

                Me.pc(i).Height = siz
                Me.pc(i).Width = siz
                'Me.pc(i).Top = yy
                'Me.pc(i).Left = xx
                Me.pc(i).BackColor = Color.Black
                Me.pc(i).FlatStyle = FlatStyle.Flat
                Me.pc(i).Name = i.ToString
                Me.pc(i).Text = id
                Me.pc(i).Tag = id
                Me.pc(i).Font = New Font("Arial", 1)


                Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
                If System.IO.File.Exists(fileName) Then
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click3
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)

                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        Me.pc(i).Refresh()
                        'Me.pc(i).Enabled = False
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click4
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    End If


                    img.Dispose()
                    img = Nothing

                    imm = Nothing
                Else
                    Dim wc As New System.Net.WebClient()
                    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/" & icon, "images\" & patch & "\runeimage\" & id & ".png")
                    wc.Dispose()


                    Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(im)

                    If c2 = 0 Then
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).Refresh()
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click3
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    Else
                        Me.pc(i).BackgroundImageLayout = ImageLayout.Zoom
                        'Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        'Me.pc(i).BackgroundImage = Create1bppImage(Me.pc(i).BackgroundImage)
                        Me.pc(i).BackgroundImage = Brighten(Me.pc(i).BackgroundImage, alp)
                        Me.pc(i).Refresh()
                        'Me.pc(i).Enabled = False
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click4
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                    End If
                    img.Dispose()
                    img = Nothing

                    im = Nothing
                End If


                'If c1 = "0" Then
                '    xx = 0
                '    yy = yy + 25
                'ElseIf c2 = "1" Then
                '    xx = xx + 50
                'Else
                '    xx = xx + 25
                'End If

                xx = c3 * siz



                Me.pc(i).Top = yy
                Me.pc(i).Left = xx
            End If

        Next

    End Sub


    Private Sub Butef(ii As Integer)
        Me.r00.Controls(ii).Enabled = False
        Me.r00.Controls(ii).Bounds = New Rectangle(Me.r00.Controls(ii).Left + 5, Me.r00.Controls(ii).Top + 5, 20, 20)

    End Sub

    Private Sub Butet(ii As Integer)
        Me.r00.Controls(ii).Enabled = True
        Me.r00.Controls(ii).Bounds = New Rectangle(Me.r00.Controls(ii).Left - 5, Me.r00.Controls(ii).Top - 5, 30, 30)
    End Sub


    Private Sub LocalRuneReformLoad()
        Application.DoEvents()
        If runereformdata.Columns.Count = 0 Then
            runereformdata.Columns.Add("pn", GetType(String))
            runereformdata.Columns.Add("rp", GetType(Integer))
            runereformdata.Columns.Add("r1", GetType(Integer))
            runereformdata.Columns.Add("r2", GetType(Integer))
            runereformdata.Columns.Add("r3", GetType(Integer))
            runereformdata.Columns.Add("r4", GetType(Integer))
            runereformdata.Columns.Add("rs", GetType(Integer))
            runereformdata.Columns.Add("r5", GetType(Integer))
            runereformdata.Columns.Add("r6", GetType(Integer))
            runereformdata.Columns.Add("id", GetType(Integer))
            runereformdata.Columns.Add("s1", GetType(Integer))
            runereformdata.Columns.Add("s2", GetType(Integer))
        End If

        If pagedata.Columns.Count = 0 Then
            pagedata.Columns.Add("pn", GetType(String))
            pagedata.Columns.Add("rp", GetType(Integer))
            pagedata.Columns.Add("r1", GetType(Integer))
            pagedata.Columns.Add("r2", GetType(Integer))
            pagedata.Columns.Add("r3", GetType(Integer))
            pagedata.Columns.Add("r4", GetType(Integer))
            pagedata.Columns.Add("rs", GetType(Integer))
            pagedata.Columns.Add("r5", GetType(Integer))
            pagedata.Columns.Add("r6", GetType(Integer))
            pagedata.Columns.Add("r7", GetType(Integer))
            pagedata.Columns.Add("r8", GetType(Integer))
            pagedata.Columns.Add("r9", GetType(Integer))
            pagedata.Columns.Add("id", GetType(Integer))
            pagedata.Columns.Add("s1", GetType(Integer))
            pagedata.Columns.Add("s2", GetType(Integer))
        End If

        Try
            Dim csvDir As String = "." ' "C:\"
            'CSVファイルの名前
            Dim csvFileName As String = Orepages

            '接続文字列
            Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
            Dim con As New System.Data.OleDb.OleDbConnection(conString)

            Dim commText As String = "SELECT * FROM [" + csvFileName + "]"

            Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)


            da.Fill(runereformdata)
            ' Call RuneImageload(vernew)
            Dim rrd As Integer = runereformdata.Rows.Count - 1
            TextBox1.AppendText("LocalRunePage Loaded" & vbCrLf)
            'DataGridView1.DataSource = runereformdata
            'Pagedataload(0)


        Catch
            TextBox1.AppendText("LocalRunePage Failed" & vbCrLf)

            Dim result As DialogResult = MessageBox.Show("can't open orepage.csv",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            Me.Close()
        End Try

    End Sub

    Private Sub LocalLVload()
        Call cmls()

        Exit Sub



        If LVdata.Columns.Count = 0 Then
            LVdata.Columns.Add("id", GetType(String))
            LVdata.Columns.Add("key", GetType(Integer))
            LVdata.Columns.Add("lvl", GetType(Integer))
        End If

        Try
            Dim csvDir As String = "." ' "C:\"
            'CSVファイルの名前
            Dim csvFileName As String = "lvl.csv"

            '接続文字列
            Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
            Dim con As New System.Data.OleDb.OleDbConnection(conString)

            Dim commText As String = "SELECT * FROM [" + csvFileName + "]"

            Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)


            da.Fill(LVdata)
            ' Call RuneImageload(vernew)
            'Dim rrd As Integer = LVdata.Rows.Count - 1
            TextBox1.AppendText("ChampMasteryLevelLoaded" & vbCrLf)
            'DataGridView1.DataSource = runereformdata
            'Pagedataload(0)

        Catch
            TextBox1.AppendText("CML Failed" & vbCrLf)

            Dim result As DialogResult = MessageBox.Show("can't open CML.csv",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            Me.Close()
        End Try


    End Sub



    Private Sub OnlineRuneReformLoad()

        If runereformdata.Columns.Count = 0 Then
            runereformdata.Columns.Add("pn", GetType(String))
            runereformdata.Columns.Add("rp", GetType(Integer))
            runereformdata.Columns.Add("r1", GetType(Integer))
            runereformdata.Columns.Add("r2", GetType(Integer))
            runereformdata.Columns.Add("r3", GetType(Integer))
            runereformdata.Columns.Add("r4", GetType(Integer))
            runereformdata.Columns.Add("rs", GetType(Integer))
            runereformdata.Columns.Add("r5", GetType(Integer))
            runereformdata.Columns.Add("r6", GetType(Integer))
            runereformdata.Columns.Add("id", GetType(Integer))
        End If

        If pagedata.Columns.Count = 0 Then
            pagedata.Columns.Add("pn", GetType(String))
            pagedata.Columns.Add("rp", GetType(Integer))
            pagedata.Columns.Add("r1", GetType(Integer))
            pagedata.Columns.Add("r2", GetType(Integer))
            pagedata.Columns.Add("r3", GetType(Integer))
            pagedata.Columns.Add("r4", GetType(Integer))
            pagedata.Columns.Add("rs", GetType(Integer))
            pagedata.Columns.Add("r5", GetType(Integer))
            pagedata.Columns.Add("r6", GetType(Integer))
            pagedata.Columns.Add("r7", GetType(Integer))
            pagedata.Columns.Add("r8", GetType(Integer))
            pagedata.Columns.Add("r9", GetType(Integer))


            pagedata.Columns.Add("id", GetType(Integer))
        End If



        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        Try
            uu = "http://runereformedapi.azurewebsites.net/api/runes/Runepages"
            'uu = "runereform.json"
            sr = webclient.OpenRead(uu)
            Dim srRead As New System.IO.StreamReader(sr)
            '内容をすべて読み込む
            Dim se As String = srRead.ReadToEnd()
            srRead.Close()
            Dim jsonObj As Object = JsonConvert.DeserializeObject(se)

            Dim i As Integer = 0
            For Each p In TryCast(jsonObj, Newtonsoft.Json.Linq.JContainer).Select(Function(token) TryCast(token, Newtonsoft.Json.Linq.JProperty))
                Dim pn As String = (jsonObj(i)("_pageName")).ToString
                Dim rp As Integer = (jsonObj(i)("_runeStart")) 'Primary
                Dim r1 As Integer = (jsonObj(i)("_rune1"))
                Dim r2 As Integer = (jsonObj(i)("_rune2"))
                Dim r3 As Integer = (jsonObj(i)("_rune3"))
                Dim r4 As Integer = (jsonObj(i)("_rune4"))
                Dim rs As Integer = (jsonObj(i)("_runeSecondary")) 'Secondry_runeSecondary
                Dim r5 As Integer = (jsonObj(i)("_rune5"))
                Dim r6 As Integer = (jsonObj(i)("_rune6"))
                Dim id As Integer = (jsonObj(i)("_ID"))
                runereformdata.Rows.Add(pn, rp, r1, r2, r3, r4, rs, r5, r6, id)
                i = i + 1
            Next
            ConvertDataTableToCsv(runereformdata, "runereformed.csv", True, False)
            'DataGridView1.DataSource = runereformdata
            'Dim idd As String = (jsonObj).ToString
            TextBox1.AppendText("OnlineRunePage Loaded" & vbCrLf)
        Catch ex As System.Net.WebException
            TextBox1.AppendText("OnlineRuneReformData Load Error" & vbCrLf)
        End Try

    End Sub

    Private Sub LocalSumspeLoad()
        Application.DoEvents()
        If sumspedata.Columns.Count = 0 Then
            sumspedata.Columns.Add("id", GetType(String))
            sumspedata.Columns.Add("name", GetType(String))
            sumspedata.Columns.Add("key", GetType(Integer))
            sumspedata.Columns.Add("image", GetType(String))
        End If

        Try
            Dim csvDir As String = "." ' "C:\"

            Dim csvFileName As String = "sumspedata.csv"


            Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
            Dim con As New System.Data.OleDb.OleDbConnection(conString)

            Dim commText As String = "SELECT * FROM [" + csvFileName + "]"

            Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)


            da.Fill(sumspedata)
            'Call RuneImageload(vernew)

            TextBox1.AppendText("LocalSumspedataLoaded" & vbCrLf)
            'DataGridView1.DataSource = runereformdata
            'Pagedataload(0)
        Catch
            TextBox1.AppendText("LocalSumspedata Failed" & vbCrLf)

            Dim result As DialogResult = MessageBox.Show("can't open orepage.csv",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Me.Close()
        End Try
        Call LocalSumspeLoad1()
    End Sub


    Private Sub LocalSumspeLoad1()

        Dim rrd As Integer = sumspedata.Rows.Count - 1
        For i As Integer = 0 To rrd
            Dim imge As String = sumspedata.Rows(i).Item("image")
            Dim key As String = sumspedata.Rows(i).Item("key")
            Dim fileName As String = "images\" & key & ".jpg"

            If System.IO.File.Exists(fileName) Then
                Dim imm As String = "images\" & key & ".jpg"
                Dim img As Image = Image.FromFile(imm)
                ImageList2.Images.Add(Image.FromFile(imm))
                img.Dispose()
                img = Nothing
                imm = Nothing
            Else
                Dim wc As New System.Net.WebClient()
                Dim ke As String = sumspedata.Rows(i).Item("key")
                wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" & vernew & "/img/spell/" & imge, "images\" & ke & ".jpg")
                wc.Dispose()
                Dim imm As String = "images\" & key & ".jpg"

                Dim img As Image = Image.FromFile(imm)
                ImageList2.Images.Add(Image.FromFile(imm))
                img.Dispose()
                img = Nothing
                imm = Nothing

            End If

            Dim idn As String = sumspedata.Rows(i).Item("name")
            smsp1.Items.AddRange(New Object() {idn})
            smsp2.Items.AddRange(New Object() {idn})
        Next
        smsp1.ImageList = ImageList2
        smsp2.ImageList = ImageList2

    End Sub


    Private Sub OnlineSumspeLoad()
        If sumspedata.Columns.Count = 0 Then
            sumspedata.Columns.Add("id", GetType(String))
            sumspedata.Columns.Add("name", GetType(String))
            sumspedata.Columns.Add("key", GetType(Integer))
            sumspedata.Columns.Add("image", GetType(String))

        End If


        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        Try
            uu = "http://ddragon.leagueoflegends.com/cdn/" & vernew & "/data/" & country1 & "/summoner.json"
            sr = webclient.OpenRead(uu)
            Dim srRead As New System.IO.StreamReader(sr)
            '内容をすべて読み込む
            Dim se As String = srRead.ReadToEnd()
            srRead.Close()
            Dim jsonObj As Object = JsonConvert.DeserializeObject(se)
            '  TextBox1.Text = (jsonObj("data").ToString)
            For Each p In TryCast(jsonObj("data"), Newtonsoft.Json.Linq.JContainer).Select(Function(token) TryCast(token, Newtonsoft.Json.Linq.JProperty))
                Dim id As String = ($"{p.Name}").ToString
                Dim name As String = (jsonObj("data")(id)("name")).ToString
                Dim key As Integer = 0
                'Dim key As Integer = (jsonObj("data")(id)("effectBurn")("key")).ToString

                'Dim title As String = (jsonObj("data")(id)("title")).ToString
                Dim image As String = (jsonObj("data")(id)("image")("full")).ToString

                'uu = "http://ddragon.leagueoflegends.com/cdn/" & vernew & "/data/" & country1 & "/champion/" & id & ".json"
                'sr = webclient.OpenRead(uu)
                'Dim serRead As New System.IO.StreamReader(sr)
                ''内容をすべて読み込む
                'Dim see As String = serRead.ReadToEnd()
                'srRead.Close()
                'Dim jsonObjj As Object = JsonConvert.DeserializeObject(see)
                'Dim q As String = jsonObjj("data")(id)("spells")(0)("image")("full")
                'Dim w As String = jsonObjj("data")(id)("spells")(1)("image")("full")
                'Dim e As String = jsonObjj("data")(id)("spells")(2)("image")("full")
                'Dim r As String = jsonObjj("data")(id)("spells")(3)("image")("full")

                Dim q As String = 0
                Dim w As String = 0
                Dim e As String = 0
                Dim r As String = 0


                sumspedata.Rows.Add(id, name, key, image)

            Next
            ConvertDataTableToCsv(sumspedata, "sumspedata.csv", True, False)
            'DataGridView1.DataSource = sumspedata
            Call RuneImageload(vernew)
            TextBox1.AppendText("Onlinesumspedata Load Success" & vbCrLf)
        Catch ex As System.Net.WebException
            TextBox1.AppendText("Onlinesumspedata Load Error" & vbCrLf)
        End Try
    End Sub

    Private Sub LocalRuneDataLoad(vernew As String)
        'TextBox1.AppendText("LocalRuneData Loading" & vbCrLf)
        Application.DoEvents()
        If runedata.Columns.Count = 0 Then
            runedata.Columns.Add("id", GetType(String))
            runedata.Columns.Add("key", GetType(String))
            runedata.Columns.Add("icon", GetType(String))
            runedata.Columns.Add("name", GetType(String))
            runedata.Columns.Add("csd", GetType(String))
            runedata.Columns.Add("c1", GetType(String))
            runedata.Columns.Add("c2", GetType(String))
            runedata.Columns.Add("c3", GetType(String))
        End If

        Dim csvDir As String = "." ' "C:\"
        'CSVファイルの名前
        Dim csvFileName As String = "runedata.csv"

        '接続文字列
        Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As New System.Data.OleDb.OleDbConnection(conString)

        Dim commText As String = "SELECT * FROM [" + csvFileName + "]"
        Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)

        da.Fill(runedata)
        TextBox1.AppendText("LocalRuneData Loaded" & vbCrLf)
        '  DataGridView1.DataSource = runedata
        Call RuneImageload(vernew)

        Call LocalChampDataLoad(vernew)
        But3.Enabled = True
    End Sub


    Private Sub OnlineRuneDataLoad(vernew As String)

        If runedata.Columns.Count = 0 Then
            runedata.Columns.Add("id", GetType(String))
            runedata.Columns.Add("key", GetType(String))
            runedata.Columns.Add("icon", GetType(String))
            runedata.Columns.Add("name", GetType(String))
            runedata.Columns.Add("csd", GetType(String))
            runedata.Columns.Add("c1", GetType(String))
            runedata.Columns.Add("c2", GetType(String))
            runedata.Columns.Add("c3", GetType(String))
        End If


        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        Try
            uu = "https://ddragon.leagueoflegends.com/cdn/" & vernew & "/data/" & country2 & "/runesReforged.json"
            '' uu = "https://ddragon.leagueoflegends.com/cdn/8.23.1/data/en_US/runesReforged.json
            'uu = "https://127.0.0.1:" & port & "/lol-perks/v1/perks"
            'uu = "rune.json"
            sr = webclient.OpenRead(uu)
            Dim srRead As New System.IO.StreamReader(sr)
            '内容をすべて読み込む
            Dim se As String = srRead.ReadToEnd()
            srRead.Close()
            Dim jsonObj As Object = JsonConvert.DeserializeObject(se)
            'Dim idd As String = (jsonObj).ToString
            'Dim len As Integer = jsonObj.length

            ''   TextBox1.AppendText(idd)
            'TextBox1.AppendText(len & vbCrLf)
            Dim i As Integer = 0

            For Each p In TryCast(jsonObj, Newtonsoft.Json.Linq.JContainer).Select(Function(token) TryCast(token, Newtonsoft.Json.Linq.JProperty))
                '    Dim id As String = ($"{p.Name}").ToString
                Dim id As String = (jsonObj(i)("id")).ToString
                Dim key As String = (jsonObj(i)("key")).ToString
                Dim icon As String = (jsonObj(i)("icon")).ToString
                Dim name As String = (jsonObj(i)("name")).ToString

                runedata.Rows.Add(id, key, icon, name, "0", i, "0", "0")
                ' TextBox1.AppendText("*" & id & " " & key & " " & name & vbCrLf)
                Dim ii As Integer = 0
                For Each pp In jsonObj(i)("slots")

                    Dim iii As Integer = 0
                    Dim l As String = "1"
                    For Each ppp In jsonObj(i)("slots")(ii)("runes")
                        Dim cid As String = jsonObj(i)("slots")(ii)("runes")(iii)("id").ToString
                        Dim ckey As String = jsonObj(i)("slots")(ii)("runes")(iii)("key").ToString
                        Dim cicon As String = jsonObj(i)("slots")(ii)("runes")(iii)("icon").ToString
                        Dim cname As String = jsonObj(i)("slots")(ii)("runes")(iii)("name").ToString
                        Dim csds As String = jsonObj(i)("slots")(ii)("runes")(iii)("shortDesc").ToString
                        Dim csd As String = Regex.Replace(csds, "<.*?>", String.Empty)


                        'TextBox1.AppendText(csd & vbCrLf)
                        runedata.Rows.Add(cid, ckey, cicon, cname, csd, i, ii + 1, iii)
                        l = "2"
                        '    
                        iii = iii + 1
                    Next
                    ii = ii + 1
                Next

                i = i + 1
            Next
            runedata.Rows.Add(5008, "Adaptive", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "アダプティブフォース", "アダプティブフォース+10", 5, 1, 0)
            runedata.Rows.Add(5005, "AttackSpeed", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "攻撃速度", "攻撃速度+9%", 5, 1, 1)
            runedata.Rows.Add(5007, "CDRScaling", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "クールダウン短縮", "クールダウン短縮+1%-10%(レベルに応じて)", 5, 1, 2)
            runedata.Rows.Add(5008, "Adaptive", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "アダプティブフォース", "アダプティブフォース+10", 5, 2, 0)
            runedata.Rows.Add(5002, "Armor", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "物理防御", "物理防御+5", 5, 2, 1)
            runedata.Rows.Add(5003, "MagicRes", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "魔法防御", "魔法防御+5", 5, 2, 2)
            runedata.Rows.Add(5001, "HealthScaling", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "体力", "体力+15-90(レベルに応じて)", 5, 3, 0)
            runedata.Rows.Add(5002, "Armor", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "物理防御", "物理防御+5", 5, 3, 1)
            runedata.Rows.Add(5003, "MagicRes", "perk-images/StatMods/StatModsAdaptiveForceIcon.png", "魔法防御", "魔法防御+5", 5, 3, 2)


            ConvertDataTableToCsv(runedata, "runedata.csv", True, False)
            'DataGridView1.DataSource = runedata
            Call RuneImageload(vernew)
            TextBox1.AppendText("OnlineRuneData Load Success" & vbCrLf)
        Catch ex As System.Net.WebException
            TextBox1.AppendText("OnlineRuneData Load Error" & vbCrLf)
        End Try
    End Sub

    Private Sub RuneImageload(vernew As String)

        Dim rc As Integer = runedata.Rows.Count - 1

        Me.pc = New System.Windows.Forms.Button(rc) {}
        Dim xx As Integer = 0
        Dim yy As Integer = 0
        Dim siz As Integer = 30
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\runeimage\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\runeimage\")
        End If
        'TextBox1.AppendText("RuneImageLoading" & patch & vbCrLf)
        For i As Integer = 0 To rc
            Dim icon As String = runedata.Rows(i).Item("icon")
            Dim name As String = runedata.Rows(i).Item("name")
            Dim id As String = runedata.Rows(i).Item("id")
            Dim key As String = runedata.Rows(i).Item("key")
            Dim csd As String = runedata.Rows(i).Item("csd")
            Dim c1 As String = runedata.Rows(i).Item("c1")
            Dim c2 As String = runedata.Rows(i).Item("c2")
            Dim c3 As String = runedata.Rows(i).Item("c3")
            'System.Threading.Thread.Sleep(1000)
            'TextBox1.AppendText(icon & vbCrLf)
            Me.pc(i) = New System.Windows.Forms.Button
            Me.Controls("perk" & c1).Controls.Add(pc(i))
            Me.pc(i).Height = siz
            Me.pc(i).Width = siz
            'Me.pc(i).Top = yy
            'Me.pc(i).Left = xx
            Me.pc(i).BackColor = Color.Black
            Me.pc(i).FlatStyle = FlatStyle.Flat
            Me.pc(i).Name = i.ToString
            ' Me.pc(i).Name = "Button" & i.ToString

            'Dim fileNames As String = "images\" & patch & "\" & key & "_0.jpg"
            'If System.IO.File.Exists(fileNames) Then

            'Else
            '    Dim wc As New System.Net.WebClient()

            '    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/champion/splash/" & key & "_0.jpg", "images\" & patch & "\" & key & "_0.jpg")
            '    wc.Dispose()
            'End If

            Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
            If System.IO.File.Exists(fileName) Then
                Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                Dim img As Image = Image.FromFile(imm)
                Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click
                AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover
                img.Dispose()
                img = Nothing

                imm = Nothing
            Else
                If id < 5100 Then
                    Try
                        Dim wc As New System.Net.WebClient()
                        wc.DownloadFile("http://www1.interq.or.jp/~masaya/img/" & id & ".png", "images\" & patch & "\runeimage\" & id & ".png")
                        wc.Dispose()


                        Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                        Dim img As Image = Image.FromFile(im)
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover

                        img.Dispose()
                        img = Nothing

                        im = Nothing
                        TextBox1.AppendText(name & " " & vbCrLf)
                        Application.DoEvents()
                    Catch
                        TextBox1.AppendText("LocalRunePage Failed" & vbCrLf)

                        Dim result As DialogResult = MessageBox.Show("can't open orepage.csv",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

                        Me.Close()
                    End Try

                Else

                    Try
                        Dim wc As New System.Net.WebClient()
                        wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/" & icon, "images\" & patch & "\runeimage\" & id & ".png")
                        wc.Dispose()


                        Dim im As String = "images\" & patch & "\runeimage\" & id & ".png"
                        Dim img As Image = Image.FromFile(im)
                        Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                        AddHandler Me.pc(i).Click, AddressOf Me.RuneButtons_Click
                        AddHandler Me.pc(i).MouseEnter, AddressOf Me.RuneButtons_MouseHover

                        img.Dispose()
                        img = Nothing

                        im = Nothing
                        TextBox1.AppendText(name & " " & vbCrLf)
                        Application.DoEvents()
                    Catch
                        TextBox1.AppendText("LocalRunePage Failed" & vbCrLf)

                        Dim result As DialogResult = MessageBox.Show("can't open orepage.csv",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

                        Me.Close()

                    End Try
                End If
            End If


            'If c1 = "0" Then
            '    xx = 0
            '    yy = yy + 25
            'ElseIf c2 = "1" Then
            '    xx = xx + 50
            'Else
            '    xx = xx + 25
            'End If

            '    xx = (c1 * (siz * 5)) + (c3 * siz)
            xx = (c3 * siz)
            yy = (c2 * siz)

            Me.pc(i).Top = yy
            Me.pc(i).Left = xx
        Next

        TextBox1.AppendText("*RuneImageLoaded" & vbCrLf)


    End Sub



    Private Sub RuneButtons_MouseHover(sender As Object, e As EventArgs)

        Dim i As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        'Dim toolTip1 As New System.Windows.Forms.ToolTip With {
        '    .IsBalloon = True,
        '    .InitialDelay = 1000,
        '    .ReshowDelay = 2000
        '}

        Dim csd As String = runedata.Rows(i).Item("csd") 'ツールチップのタイトル
        Dim title As String = runedata.Rows(i).Item("key") & " : " & runedata.Rows(i).Item("name")
        tit.Text = title
        tip.Text = csd
    End Sub
    Private Sub RuneButtons_MouseHover2(sender As Object, e As EventArgs)

        Dim i As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Panel).Tag)

        'Dim toolTip1 As New System.Windows.Forms.ToolTip With {
        '    .IsBalloon = False,
        '    .InitialDelay = 1000,
        '    .ReshowDelay = 2000
        '}

        Dim csd As String = runedata.Rows(i).Item("csd") 'ツールチップのタイトル
        Dim title As String = runedata.Rows(i).Item("key") & " : " & runedata.Rows(i).Item("name")
        tit.Text = title
        tip.Text = csd
    End Sub





    Private Sub ChampButtons_MouseHover(sender As Object, e As EventArgs)

        Dim i As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Text)
        Dim ii As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        'If ii = oldii Then
        '    Exit Sub
        'End If
        Dim ra As Integer = runedata.Rows.Count
        Dim rb As Integer = champdata.Rows.Count
        Dim ke As Integer = 0



        Dim cn As String = ""
        Dim ti As String = ""
        For j As Integer = 0 To rb - 1
            ke = Integer.Parse(champdata.Rows(j).Item("key"))

            If i = ke Then
                cn = champdata.Rows(j).Item("name")
                ti = champdata.Rows(j).Item("title")
                Exit For
            End If
        Next
        'TextBox1.AppendText(ke & ":" & cn & ": " & ii & vbCrLf)
        tit.Text = cn
        tip.Text = ti
    End Sub




    Private Sub LocalChampDataLoad(vernew As String)

        Dim csvDir As String = "." ' "C:\"
        'CSVファイルの名前
        Dim csvFileName As String = "champdata.csv"

        '接続文字列
        Dim conString As String =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" _
            + csvDir + ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As New System.Data.OleDb.OleDbConnection(conString)

        Dim commText As String = "SELECT * FROM [" + csvFileName + "]"
        Dim da As New System.Data.OleDb.OleDbDataAdapter(commText, con)

        da.Fill(champdata)
        Call Imageload(vernew)
        Call LocalRuneReformLoad()
    End Sub

    Private Sub history()
        Application.DoEvents()
        If Hisdata.Columns.Count = 0 Then
            Hisdata.Columns.Add("id", GetType(String))
            hisdata.Columns.Add("win", GetType(String))
        End If
        hisdata.Clear()
        Dim response As HttpResponse = Nothing
        Try
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            '  response = http.[Get]("https://127.0.0.1:" & port & "/lol-match-history/v3/matchlist/account/" & checkacc & "?begIndex=0&endIndex=10")
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-match-history/v1/delta")
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-match-history/v1/products/lol/current-summoner/matches?begIndex=0&endIndex=9")

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
            'TextBox1.AppendText(port & ":" & token & vbCrLf)
            oldid = 0
        Else
            Dim grid = response.DynamicBody
            'Dim jsonObj As Object = JsonConvert.DeserializeObject(grid.ToString)
            Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            'TextBox1.AppendText(jsonObj & vbCrLf)

            Dim id As String = ""
            Dim win As String = ""
            'Dim i As Integer = 9
            Try
                For i As Integer = 0 To 9 'Step -1
                    'TextBox1.AppendText(i & grid(i).name & vbCrLf)

                    id = grid.games.games(i).participants(0).championId
                    win = grid.games.games(i).participants(0).stats.win.ToString
                    'i += 1
                    hisdata.Rows.Add(id, win)
                Next
                'LVdata.Rows.RemoveAt(0)
                'ConvertDataTableToCsv(LVdata, "lvl.csv", True, False)
                'DataGridView1.DataSource = hisdata
                His_Image()
            Catch
                Exit Sub
            End Try

        End If

        ''https://127.0.0.1:52174/lol-match-history/v2/matchlist?begIndex=0&endIndex=10
    End Sub

    Public Sub His_Image()
        Application.DoEvents()
        Dim hisname As String
        Dim patch As String = vernew
        For j As Integer = 0 To hisdata.Rows.Count - 1
            Dim Champid As Integer = hisdata.Rows(j).Item("id")
            Dim rb As Integer = champdata.Rows.Count - 1
            For i As Integer = 0 To rb
                Dim key As Integer = champdata.Rows(i).Item("key")
                If key = Champid Then
                    hisname = champdata.Rows(i).Item("id")
                    Dim immm As String = "images\" & patch & "\champimage\" & hisname & ".png"
                    Dim imgg As Image = Image.FromFile(immm)

                    Me.Controls("his" & j).BackgroundImage = New Bitmap(imgg, 24, 24)
                    Exit For
                End If
            Next
            If hisdata.Rows(j).Item("win") = "True" Then
                Me.Controls("hh" & j).BackColor = Color.Green
            Else
                Me.Controls("hh" & j).BackColor = Color.Red
            End If
        Next


    End Sub

    Private Sub OnlineChampDataLoad(vernew As String)
        Application.DoEvents()
        If champdata.Columns.Count = 0 Then
            champdata.Columns.Add("id", GetType(String))
            champdata.Columns.Add("key", GetType(String))
            champdata.Columns.Add("name", GetType(String))
            champdata.Columns.Add("title", GetType(String))
            champdata.Columns.Add("image", GetType(String))
            champdata.Columns.Add("attack", GetType(String))
            champdata.Columns.Add("defense", GetType(String))
            champdata.Columns.Add("magic", GetType(String))
            champdata.Columns.Add("difficulty", GetType(String))
        End If

        Dim webclient As New System.Net.WebClient()
        Dim sr As System.IO.Stream
        Dim uu As String = ""
        Try
            uu = "http://ddragon.leagueoflegends.com/cdn/" & vernew & "/data/" & country1 & "/champion.json"
            sr = webclient.OpenRead(uu)
            Dim srRead As New System.IO.StreamReader(sr)
            '内容をすべて読み込む
            Dim se As String = srRead.ReadToEnd()
            srRead.Close()
            Dim jsonObj As Object = JsonConvert.DeserializeObject(se)
            '  TextBox1.Text = (jsonObj("data").ToString)
            For Each p In TryCast(jsonObj("data"), Newtonsoft.Json.Linq.JContainer).Select(Function(token) TryCast(token, Newtonsoft.Json.Linq.JProperty))
                Dim id As String = ($"{p.Name}").ToString
                Dim key As String = (jsonObj("data")(id)("key")).ToString
                Dim name As String = (jsonObj("data")(id)("name")).ToString
                Dim title As String = (jsonObj("data")(id)("title")).ToString
                Dim image As String = (jsonObj("data")(id)("image")("full")).ToString
                Dim attack As String = (jsonObj("data")(id)("info")("attack")).ToString
                Dim defense As String = (jsonObj("data")(id)("info")("defense")).ToString
                Dim magic As String = (jsonObj("data")(id)("info")("magic")).ToString
                Dim difficulty As String = (jsonObj("data")(id)("info")("difficulty")).ToString

                'uu = "http://ddragon.leagueoflegends.com/cdn/" & vernew & "/data/" & country1 & "/champion/" & id & ".json"
                'sr = webclient.OpenRead(uu)
                'Dim serRead As New System.IO.StreamReader(sr)
                ''内容をすべて読み込む
                'Dim see As String = serRead.ReadToEnd()
                'srRead.Close()
                'Dim jsonObjj As Object = JsonConvert.DeserializeObject(see)
                'Dim q As String = jsonObjj("data")(id)("spells")(0)("image")("full")
                'Dim w As String = jsonObjj("data")(id)("spells")(1)("image")("full")
                'Dim e As String = jsonObjj("data")(id)("spells")(2)("image")("full")
                'Dim r As String = jsonObjj("data")(id)("spells")(3)("image")("full")

                Dim q As String = 0
                Dim w As String = 0
                Dim e As String = 0
                Dim r As String = 0


                champdata.Rows.Add(id, key, name, title, image, attack, defense, magic, difficulty)

            Next

            ConvertDataTableToCsv(champdata, "champdata.csv", True, False)
            Call Imageload(vernew)
        Catch ex As System.Net.WebException
        End Try


    End Sub

    Private Sub Imageload(vernew As String)

        Dim ra As Integer = runedata.Rows.Count
        Dim rb As Integer = champdata.Rows.Count - 1
        Dim rc As Integer = ra + rb
        Dim siz As Integer = 30

        Me.pc = New System.Windows.Forms.Button(rc) {}
        Me.lab = New System.Windows.Forms.Label(rc) {}
        Dim xx As Integer = 0
        Dim yy As Integer = 0
        Dim patch As String = vernew
        If System.IO.Directory.Exists("images\" & patch & "\champimage\") Then

        Else
            System.IO.Directory.CreateDirectory("images\" & patch & "\champimage\")
        End If

        'If System.IO.Directory.Exists("images\" & patch & "\champspellimage\") Then

        'Else
        '    System.IO.Directory.CreateDirectory("images\" & patch & "\champspellimage\")
        'End If


        For i As Integer = ra To rc
            Dim key As String = champdata.Rows(i - ra).Item("key")
            Dim id As String = champdata.Rows(i - ra).Item("id")
            Dim attack As String = champdata.Rows(i - ra).Item("attack")
            Dim defense As String = champdata.Rows(i - ra).Item("defense")
            Dim magic As String = champdata.Rows(i - ra).Item("magic")
            Dim difficulty As String = champdata.Rows(i - ra).Item("difficulty")
            Dim namae As String = champdata.Rows(i - ra).Item("name")
            champbox.Items.Add(namae)
            champkeybox.Items.Add(key)
            TextBox10.Text = key
            CkBox.Items.Add(key)
            'System.Threading.Thread.Sleep(1000)
            Me.pc(i) = New System.Windows.Forms.Button
            Me.pa2.Controls.Add(pc(i))
            Me.pc(i).Height = siz
            Me.pc(i).Width = siz
            Me.pc(i).Top = yy
            Me.pc(i).Left = xx
            Me.pc(i).BackColor = Color.Black
            Me.pc(i).FlatStyle = FlatStyle.Flat
            'Me.pc(i).ForeColor = Color.Transparent

            Me.pc(i).Font = New Font("Arial", 1)
            Me.pc(i).Text = key
            Me.pc(i).Name = i.ToString()

            'Me.lab(i) = New System.Windows.Forms.Label
            'Me.Panel2.Controls.Add(lab(i))
            'Me.lab(i).AutoSize = False
            'Me.lab(i).Width = siz
            'Me.lab(i).Top = yy + siz
            'Me.lab(i).Left = xx
            'Me.lab(i).BackColor = Color.DarkGray
            'Me.lab(i).FlatStyle = FlatStyle.Flat
            'Me.lab(i).ForeColor = Color.White
            'Me.lab(i).Text = namae
            'Me.lab(i).TextAlign = ContentAlignment.MiddleCenter



            '    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/champion/splash/" & key & "_0.jpg", "images\" & patch & "\" & key & "_0.jpg")


            ''Dim spellqName As String = "images\" & patch & "\champspellimage\" & q
            ''If System.IO.File.Exists(spellqName) Then

            ''Else
            ''    Dim wc As New System.Net.WebClient()
            ''    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" & patch & "/img/spell/" & q, "images\" & patch & "\champspellimage\" & q)
            ''    wc.Dispose()
            ''End If

            ''Dim spellwName As String = "images\" & patch & "\champspellimage\" & w
            ''If System.IO.File.Exists(spellwName) Then

            ''Else
            ''    Dim wc As New System.Net.WebClient()
            ''    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" & patch & "/img/spell/" & w, "images\" & patch & "\champspellimage\" & w)
            ''    wc.Dispose()
            ''End If

            ''Dim spelleName As String = "images\" & patch & "\champspellimage\" & e
            ''If System.IO.File.Exists(spelleName) Then

            ''Else
            ''    Dim wc As New System.Net.WebClient()
            ''    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" & patch & "/img/spell/" & e, "images\" & patch & "\champspellimage\" & e)
            ''    wc.Dispose()
            ''End If

            ''Dim spellrName As String = "images\" & patch & "\champspellimage\" & r
            ''If System.IO.File.Exists(spellrName) Then

            ''Else
            ''    Dim wc As New System.Net.WebClient()
            ''    wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" & patch & "/img/spell/" & r, "images\" & patch & "\champspellimage\" & r)
            ''    wc.Dispose()
            ''End If


            Dim fileName As String = "images\" & patch & "\champimage\" & id & ".png"
            If System.IO.File.Exists(fileName) Then
                Dim imm As String = "images\" & patch & "\champimage\" & id & ".png"
                Dim img As Image = Image.FromFile(imm)
                Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                AddHandler Me.pc(i).Click, AddressOf Me.ChampButtons_Click
                AddHandler Me.pc(i).MouseEnter, AddressOf Me.ChampButtons_MouseHover
                ImageList1.Images.Add(Image.FromFile(imm))

                img.Dispose()
                img = Nothing
                imm = Nothing
            Else
                Dim wc As New System.Net.WebClient()
                wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/" & patch & "/img/champion/" & id & ".png", "images\" & patch & "\champimage\" & id & ".png")
                wc.Dispose()


                Dim imm As String = "images\" & patch & "\champimage\" & id & ".png"
                Dim img As Image = Image.FromFile(imm)
                ImageList1.Images.Add(Image.FromFile(imm))
                Me.pc(i).BackgroundImage = New Bitmap(img, Me.pc(i).Width, Me.pc(i).Height)
                AddHandler Me.pc(i).Click, AddressOf Me.ChampButtons_Click
                AddHandler Me.pc(i).MouseEnter, AddressOf Me.ChampButtons_MouseHover
                img.Dispose()
                img = Nothing
                imm = Nothing
                TextBox1.AppendText(namae & " " & vbCrLf)
                Application.DoEvents()
            End If

            Me.pc(i).Top = yy
            Me.pc(i).Left = xx

            xx = xx + siz
            If xx > 450 Then
                xx = 0
                yy = yy + siz
            End If


            Cbox1.Items.AddRange(New Object() {id})

        Next
        TextBox1.AppendText("*ChampionData Loaded" & vbCrLf)
        Cbox1.ImageList = ImageList1
        'comboimage1.Items.AddRange(New Object() {hidid})

    End Sub

    Private Sub ChampButtons_Click(ByVal sender As Object, ByVal e As EventArgs)



        PageBox.Items.Clear()
        pagedata.Clear()
        Dim cc As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        'Dim cm As Integer

        Dim ra As Integer = runedata.Rows.Count
        'Dim rb As Integer = champdata.Rows.Count - 1
        Dim rc As Integer = cc - ra
        Cbox1.SelectedIndex = rc
        'Dim rrd As Integer = runereformdata.Rows.Count - 1

        'cm = Integer.Parse(champdata.Rows(rc).Item("key"))
        'Dim namae As String = champdata.Rows(rc).Item("name")
        'TextBox1.AppendText(cm & vbCrLf)

        'Dim j As String = 0
        'For i As Integer = 0 To rrd
        '    Dim id As Integer = runereformdata.Rows(i).Item("id")
        '    If id = cm Then
        '        pagedata.Rows.Add(runereformdata.Rows(i).Item("pn"),
        '                          runereformdata.Rows(i).Item("rp"),
        '                          runereformdata.Rows(i).Item("r1"),
        '                          runereformdata.Rows(i).Item("r2"),
        '                          runereformdata.Rows(i).Item("r3"),
        '                          runereformdata.Rows(i).Item("r4"),
        '                          runereformdata.Rows(i).Item("rs"),
        '                          runereformdata.Rows(i).Item("r5"),
        '                          runereformdata.Rows(i).Item("r6"),
        '                          runereformdata.Rows(i).Item("id"))

        '        PageBox.Items.Add(j.ToString & " : " & namae & " " & runereformdata.Rows(i).Item("pn"))
        '        j = j + 1
        '    End If
        'Next
        'If pagedata.Rows.Count > 0 Then
        '    PageBox.SelectedIndex = 0
        '    DataGridView1.DataSource = pagedata
        '    But3.Enabled = True
        'Else
        '    TextBox1.AppendText("Data not found" & vbCrLf)
        'End If
    End Sub
    Private Sub Cbox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbox1.SelectedIndexChanged
        Cbox1_SelectedIndexChanged2()
    End Sub

    Private Sub Cbox1_SelectedIndexChanged2()
        dnc = 1
        champbox.SelectedIndex = Cbox1.SelectedIndex
        CkBox.SelectedIndex = Cbox1.SelectedIndex
        PageBox.Items.Clear()
        pagedata.Clear()
        Dim cc As Integer = Integer.Parse(CkBox.SelectedIndex) 'Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        Dim cm As Integer

        Dim ra As Integer = runedata.Rows.Count
        Dim rb As Integer = champdata.Rows.Count - 1
        Dim rc As Integer = cc ' - ra
        Dim rrd As Integer = runereformdata.Rows.Count - 1

        cm = Integer.Parse(champdata.Rows(rc).Item("key"))
        Dim namae As String = champdata.Rows(rc).Item("name")
        Dim idi As String = champdata.Rows(rc).Item("id")
        'Dim cml As String = LVdata.Rows(rc).Item("lvl")
        'Dim cmp As String = LVdata.Rows(rc).Item("mp")
        Dim patch As String = vernew
        'TextBox1.AppendText(cm & vbCrLf)

        Dim imm As String = "images\" & patch & "\loading\" & idi & "_0.jpg"
        'Dim imm As String = "images\" & patch & "\loading\" & skillno1

        If System.IO.File.Exists(imm) Then
        Else
            TextBox1.AppendText("tiles data not found : " & imm & vbCrLf)
            'imm = "loading\" & champ1 & "_0.jpg"
            Dim wc As New System.Net.WebClient()
            wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/champion/loading/" & idi & "_0.jpg", imm)
            wc.Dispose()
        End If





        Dim img As Image = Image.FromFile(imm)
        pa1.BackgroundImage = New Bitmap(img)

        'Label2.Text = cml
        'Label3.Text = cmp
        Dim j As String = 0
        For i As Integer = 0 To rrd
            Dim id As Integer = runereformdata.Rows(i).Item("id")
            If id = cm Then
                pagedata.Rows.Add(runereformdata.Rows(i).Item("pn"),
                                  runereformdata.Rows(i).Item("rp"),
                                  runereformdata.Rows(i).Item("r1"),
                                  runereformdata.Rows(i).Item("r2"),
                                  runereformdata.Rows(i).Item("r3"),
                                  runereformdata.Rows(i).Item("r4"),
                                  runereformdata.Rows(i).Item("rs"),
                                  runereformdata.Rows(i).Item("r5"),
                                  runereformdata.Rows(i).Item("r6"),
                                  runereformdata.Rows(i).Item("r7"),
                                  runereformdata.Rows(i).Item("r8"),
                                  runereformdata.Rows(i).Item("r9"),
                                  runereformdata.Rows(i).Item("id"),
                                  runereformdata.Rows(i).Item("s1"),
                                  runereformdata.Rows(i).Item("s2"))

                PageBox.Items.Add(j.ToString & " : " & namae & " " & runereformdata.Rows(i).Item("pn"))

                j = j + 1
            End If
        Next
        Label2.Text = "0"
        Label3.Text = "0"
        For k As Integer = 0 To LVdata.Rows.Count - 1
            Dim id As Integer = LVdata.Rows(k).Item("key")
            If id = cm Then


                Dim nn As Integer = LVdata.Rows(k).Item("lvl")
                Dim cl As String = ""
                For ii As Integer = 0 To nn - 1
                    cl = cl & "★"
                Next
                Label2.Text = cl
                Label3.Text = String.Format("{0:#,0} Bytes", LVdata.Rows(k).Item("mp"))

            End If

        Next


        If pagedata.Rows.Count > 0 Then
            PageBox.SelectedIndex = pagedata.Rows.Count - 1
            DataGridView1.DataSource = pagedata
            But3.Enabled = True

        Else
            orespe.Clear()
            'pagedata.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
            'Pagedataload(0)
            TextBox1.AppendText("Data not found" & vbCrLf)
            'orespe.Rows.Add("SampleData", "8400", "8437", "8446", "8429", "8451", "8000", "8014", "9111", "5008", "5008", "5001", cm, "4", "32")
            'ConvertDataTableToCsv(orespe, "orepage.csv", False, True)
            'LocalRuneReformLoad()
            'Pagedataload2()
            'TextBox1.AppendText("ReBuild Rune for " & cm & vbCrLf)
        End If
    End Sub

    Private Sub PageBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PageBox.SelectedIndexChanged
        Dim nn As Integer = PageBox.SelectedIndex
        Pagedataload(nn)
    End Sub

    Private Sub Pagedataload(nn As Integer)
        Dim rc As Integer = runedata.Rows.Count - 1
        Dim patch As String = vernew
        Dim siz As Integer = 30
        If rc > 0 Then
            Dim rp As Integer = pagedata.Rows(nn).Item("rp")
            Dim r1 As Integer = pagedata.Rows(nn).Item("r1")
            Dim r2 As Integer = pagedata.Rows(nn).Item("r2")
            Dim r3 As Integer = pagedata.Rows(nn).Item("r3")
            Dim r4 As Integer = pagedata.Rows(nn).Item("r4")
            Dim rs As Integer = pagedata.Rows(nn).Item("rs")
            Dim r5 As Integer = pagedata.Rows(nn).Item("r5")
            Dim r6 As Integer = pagedata.Rows(nn).Item("r6")
            Dim r7 As Integer = pagedata.Rows(nn).Item("r7")
            Dim r8 As Integer = pagedata.Rows(nn).Item("r8")
            Dim r9 As Integer = pagedata.Rows(nn).Item("r9")
            Dim imm0 As String = "images\" & patch & "\runeimage\" & rp & ".png"
            Dim img0 As Image = Image.FromFile(imm0)
            br0.BackgroundImage = New Bitmap(img0, br0.Width, br0.Height)
            imm0 = "images\" & patch & "\runeimage\" & r1 & ".png"
            img0 = Image.FromFile(imm0)
            br1.BackgroundImage = New Bitmap(img0, br1.Width, br1.Height)
            imm0 = "images\" & patch & "\runeimage\" & r2 & ".png"
            img0 = Image.FromFile(imm0)
            br2.BackgroundImage = New Bitmap(img0, br2.Width, br2.Height)
            imm0 = "images\" & patch & "\runeimage\" & r3 & ".png"
            img0 = Image.FromFile(imm0)
            br3.BackgroundImage = New Bitmap(img0, br3.Width, br3.Height)
            imm0 = "images\" & patch & "\runeimage\" & r4 & ".png"
            img0 = Image.FromFile(imm0)
            br4.BackgroundImage = New Bitmap(img0, br4.Width, br4.Height)
            imm0 = "images\" & patch & "\runeimage\" & rs & ".png"
            img0 = Image.FromFile(imm0)
            br5.BackgroundImage = New Bitmap(img0, br5.Width, br5.Height)
            imm0 = "images\" & patch & "\runeimage\" & r5 & ".png"
            img0 = Image.FromFile(imm0)
            br6.BackgroundImage = New Bitmap(img0, br6.Width, br6.Height)
            imm0 = "images\" & patch & "\runeimage\" & r6 & ".png"
            img0 = Image.FromFile(imm0)
            br7.BackgroundImage = New Bitmap(img0, br7.Width, br7.Height)
            imm0 = "images\" & patch & "\runeimage\" & r7 & ".png"
            img0 = Image.FromFile(imm0)
            br8.BackgroundImage = New Bitmap(img0, br8.Width, br8.Height)
            imm0 = "images\" & patch & "\runeimage\" & r8 & ".png"
            img0 = Image.FromFile(imm0)
            br9.BackgroundImage = New Bitmap(img0, br9.Width, br9.Height)
            imm0 = "images\" & patch & "\runeimage\" & r9 & ".png"
            img0 = Image.FromFile(imm0)
            br10.BackgroundImage = New Bitmap(img0, br10.Width, br10.Height)



            Dim sss1 As Integer
            Dim sss2 As Integer
            If sspfr = False Then
                sss1 = pagedata.Rows(nn).Item("s2")
                sss2 = pagedata.Rows(nn).Item("s1")
            Else
                sss1 = pagedata.Rows(nn).Item("s1")
                sss2 = pagedata.Rows(nn).Item("s2")
            End If
            't0.Text = rp
            't1.Text = r1
            't2.Text = r2
            't3.Text = r3
            't4.Text = r4
            'y0.Text = rs
            'y2.Text = r5
            'y3.Text = r6


            Dim i As Integer = 0
            Dim oldc1 As String = "0"
            Dim pri As Integer = 0
            Dim prima As Integer = 0
            For pi As Integer = 0 To rc
                Dim c1 As String = runedata.Rows(pi).Item("c1")
                If c1 <> oldc1 Then
                    i = 0
                    oldc1 = c1
                    pri = 0
                End If
                Me.Controls("perk" & c1).Controls(i).BackgroundImage = Nothing
                Me.Controls("perk" & c1).Refresh()

                Dim id As String = runedata.Rows(pi).Item("id")



                Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
                If System.IO.File.Exists(fileName) Then
                    If c1 = 5 Then

                    End If
                    If {rp, r1, r2, r3, r4, r5, r6, rs}.Contains(id) Or (c1 = "5" And {0, 1, 2}.Contains(i) And id = r7) Or (c1 = "5" And {3, 4, 5}.Contains(i) And id = r8) Or (c1 = "5" And {6, 7, 8}.Contains(i) And id = r9) Then
                        Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                        Dim img As Image = Image.FromFile(imm)
                        Me.Controls("perk" & c1).Controls(i).BackgroundImageLayout = ImageLayout.Stretch
                        Me.Controls("perk" & c1).Controls(i).BackgroundImage = New Bitmap(img, Me.Controls("perk" & c1).Controls(i).Width, Me.Controls("perk" & c1).Controls(i).Height)
                        Me.Controls("perk" & c1).Controls(i).BackgroundImage = Brighten(Me.Controls("perk" & c1).Controls(i).BackgroundImage, 0)
                        'Me.Controls("perk" & c1).Controls(i).BackgroundImage = Create1bppImage(Me.Controls("perk" & c1).Controls(i).BackgroundImage)

                        'Me.Panel1.Controls(i).Refresh()
                        img.Dispose()
                        img = Nothing
                        imm = Nothing
                        pri = pri + 1
                    Else
                        Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                        Dim img As Image = Image.FromFile(imm)
                        Me.Controls("perk" & c1).Controls(i).BackgroundImageLayout = ImageLayout.Stretch
                        Me.Controls("perk" & c1).Controls(i).BackgroundImage = New Bitmap(img, Me.Controls("perk" & c1).Controls(i).Width, Me.Controls("perk" & c1).Controls(i).Height)

                        Me.Controls("perk" & c1).Controls(i).BackgroundImage = Create1bppImage(Me.Controls("perk" & c1).Controls(i).BackgroundImage)
                        Me.Controls("perk" & c1).Controls(i).BackgroundImage = Brighten(Me.Controls("perk" & c1).Controls(i).BackgroundImage, alp)
                        'Me.Panel1.Controls(i).Refresh()
                        img.Dispose()
                        img = Nothing
                        imm = Nothing
                    End If
                End If
                If pri > 2 Then
                    Me.Controls("perk" & c1).Left = 145
                    If pri > 3 Then
                        Me.Controls("perk" & c1).Left = 15
                    End If
                Else
                    If c1 <> 5 Then
                        Me.Controls("perk" & c1).Left = -500
                    End If
                End If

                i = i + 1
            Next
            Me.perk5.Left = 182
            Dim immm As String = "images\" & sss1 & ".jpg"
            Dim imgg As Image = Image.FromFile(immm)
            ps1.BackgroundImage = New Bitmap(imgg, s1.Width, s1.Height)
            ps1.Tag = sss1
            'imgg.Dispose()
            'imgg = Nothing
            'immm = Nothing

            immm = "images\" & sss2 & ".jpg"
            imgg = Image.FromFile(immm)
            ps2.BackgroundImage = New Bitmap(imgg, s2.Width, s2.Height)
            ps2.Tag = sss2
            ' imgg.Dispose()
            'imgg = Nothing
            'immm = Nothing
            'TextBox1.AppendText("s1" & sss2 & vbCrLf)
            Me.perk5.Left = 275

        End If
    End Sub










    Private Sub Pagedataload2()
        Dim rc As Integer = runedata.Rows.Count - 1
        Dim patch As String = vernew
        Dim siz As Integer = 30

        'Dim rp As Integer = Integer.Parse(t0.Text)
        'Dim r1 As Integer = Integer.Parse(t1.Text)
        'Dim r2 As Integer = Integer.Parse(t2.Text)
        'Dim r3 As Integer = Integer.Parse(t3.Text)
        'Dim r4 As Integer = Integer.Parse(t4.Text)
        'Dim r5 As Integer = Integer.Parse(y2.Text)
        'Dim r6 As Integer = Integer.Parse(y3.Text)
        'Dim rs As Integer = Integer.Parse(y0.Text)

        Dim rp As String = (t0.Text)
        Dim r1 As String = (t1.Text)
        Dim r2 As String = (t2.Text)
        Dim r3 As String = (t3.Text)
        Dim r4 As String = (t4.Text)
        Dim r5 As String = (y2.Text)
        Dim r6 As String = (y3.Text)
        Dim r7 As String = (mmm1.Text)
        Dim r8 As String = (mmm2.Text)
        Dim r9 As String = (mmm3.Text)
        Dim rs As String = (y0.Text)

        Dim i As Integer = 0
        Dim oldc1 As String = "0"
        Dim pri As Integer = 0
        Dim prima As Integer = 0

        For pi As Integer = 0 To rc
            Dim c1 As String = runedata.Rows(pi).Item("c1")
            If c1 <> oldc1 Then
                i = 0
                oldc1 = c1
                pri = 0
            End If
            Me.Controls("perk" & c1).Controls(i).BackgroundImage = Nothing
            Me.Controls("perk" & c1).Refresh()

            Dim id As String = runedata.Rows(pi).Item("id")



            Dim fileName As String = "images\" & patch & "\runeimage\" & id & ".png"
            If System.IO.File.Exists(fileName) Then
                If {rp, r1, r2, r3, r4, r5, r6, rs}.Contains(id) Or (c1 = "5" And {0, 1, 2}.Contains(i) And id = r7) Or (c1 = "5" And {3, 4, 5}.Contains(i) And id = r8) Or (c1 = "5" And {6, 7, 8}.Contains(i) And id = r9) Then
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)
                    Me.Controls("perk" & c1).Controls(i).BackgroundImage = New Bitmap(img, Me.Controls("perk" & c1).Controls(i).Width, Me.Controls("perk" & c1).Controls(i).Height)
                    Me.Controls("perk" & c1).Controls(i).BackgroundImage = Brighten(Me.Controls("perk" & c1).Controls(i).BackgroundImage, 0)
                    'Me.Controls("perk" & c1).Controls(i).BackgroundImage = Create1bppImage(Me.Controls("perk" & c1).Controls(i).BackgroundImage)
                    'Me.Panel1.Controls(i).Refresh()
                    img.Dispose()
                    img = Nothing
                    imm = Nothing
                    pri = pri + 1
                Else
                    Dim imm As String = "images\" & patch & "\runeimage\" & id & ".png"
                    Dim img As Image = Image.FromFile(imm)
                    Me.Controls("perk" & c1).Controls(i).BackgroundImage = New Bitmap(img, Me.Controls("perk" & c1).Controls(i).Width, Me.Controls("perk" & c1).Controls(i).Height)

                    Me.Controls("perk" & c1).Controls(i).BackgroundImage = Create1bppImage(Me.Controls("perk" & c1).Controls(i).BackgroundImage)
                    Me.Controls("perk" & c1).Controls(i).BackgroundImage = Brighten(Me.Controls("perk" & c1).Controls(i).BackgroundImage, alp)
                    'Me.Panel1.Controls(i).Refresh()
                    img.Dispose()
                    img = Nothing
                    imm = Nothing
                End If
            End If
            If pri > 2 Then
                Me.Controls("perk" & c1).Left = 145
                If pri > 3 Then
                    Me.Controls("perk" & c1).Left = 15
                End If
            Else
                If c1 <> 5 Then
                    Me.Controls("perk" & c1).Left = -500
                End If

            End If

            i = i + 1


        Next
        Me.perk5.Left = 275
        DataGridView1.DataSource = runereformdata
    End Sub

    Public Shared Function FindControlByFieldName(
    ByVal frm As Form, ByVal name As String) As Object
        'まずプロパティ名を探し、見つからなければフィールド名を探す
        Dim t As System.Type = frm.GetType()

        Dim pi As System.Reflection.PropertyInfo =
        t.GetProperty(name,
            System.Reflection.BindingFlags.Public Or
            System.Reflection.BindingFlags.NonPublic Or
            System.Reflection.BindingFlags.Instance Or
            System.Reflection.BindingFlags.DeclaredOnly)

        If Not pi Is Nothing Then
            Return pi.GetValue(frm, Nothing)
        End If

        Dim fi As System.Reflection.FieldInfo =
        t.GetField(name,
            System.Reflection.BindingFlags.Public Or
            System.Reflection.BindingFlags.NonPublic Or
            System.Reflection.BindingFlags.Instance Or
            System.Reflection.BindingFlags.DeclaredOnly)

        If fi Is Nothing Then
            Return Nothing
        End If

        Return fi.GetValue(frm)
    End Function




    Private Sub RuneButtons_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim i As Integer = Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        Dim icon As String = runedata.Rows(i).Item("icon")
        Dim name As String = runedata.Rows(i).Item("name")
        Dim key As String = runedata.Rows(i).Item("key")
        Dim id As String = runedata.Rows(i).Item("id")
        Dim csd As String = runedata.Rows(i).Item("csd")
        TextBox1.AppendText(id & name & key & vbCrLf & csd & vbCrLf)

    End Sub

    Public Sub ConvertDataTableToCsv(dt As DataTable, csvPath As String, writeHeader As Boolean, append As Boolean)
        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding =
            System.Text.Encoding.GetEncoding("Shift_JIS")

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(csvPath, append, enc)

        Dim colCount As Integer = dt.Columns.Count
        Dim lastColIndex As Integer = colCount - 1
        Dim i As Integer

        'ヘッダを書き込む
        If writeHeader Then
            For i = 0 To colCount - 1
                'ヘッダの取得
                Dim field As String = dt.Columns(i).Caption
                '"で囲む
                field = EncloseDoubleQuotesIfNeed(field)
                'フィールドを書き込む
                sr.Write(field)
                'カンマを書き込む
                If lastColIndex > i Then
                    sr.Write(","c)
                End If
            Next
            '改行する
            sr.Write(vbCrLf)
        End If

        'レコードを書き込む
        Dim row As DataRow
        For Each row In dt.Rows
            For i = 0 To colCount - 1
                'フィールドの取得
                Dim field As String = row(i).ToString()
                '"で囲む
                field = EncloseDoubleQuotesIfNeed(field)
                'フィールドを書き込む
                sr.Write(field)
                'カンマを書き込む
                If lastColIndex > i Then
                    sr.Write(","c)
                End If
            Next
            '改行する
            sr.Write(vbCrLf)
        Next

        '閉じる
        sr.Close()
    End Sub

    ''' 必要ならば、文字列をダブルクォートで囲む
    Private Function EncloseDoubleQuotesIfNeed(field As String) As String
        If NeedEncloseDoubleQuotes(field) Then
            Return EncloseDoubleQuotes(field)
        End If
        Return field
    End Function


    ''' 文字列をダブルクォートで囲む
    Private Function EncloseDoubleQuotes(field As String) As String
        If field.IndexOf(""""c) > -1 Then
            '"を""とする
            field = field.Replace("""", """""")
        End If
        Return """" & field & """"
    End Function

    ''' 文字列をダブルクォートで囲む必要があるか調べる
    Private Function NeedEncloseDoubleQuotes(field As String) As Boolean
        Return field.IndexOf(""""c) > -1 OrElse
            field.IndexOf(","c) > -1 OrElse
            field.IndexOf(ControlChars.Cr) > -1 OrElse
            field.IndexOf(ControlChars.Lf) > -1 OrElse
            field.StartsWith(" ") OrElse
            field.StartsWith(vbTab) OrElse
            field.EndsWith(" ") OrElse
            field.EndsWith(vbTab)
    End Function


    ''''''keyhooker
    'WithEvents KeyboardHooker1 As New Key
    'Sub KeybordHooker1_KeyDown(sender As Object, e As KeyBoardHookerEventArgs) Handles KeyboardHooker1.KeyDown1
    '    TextBox1.AppendText(CStr(e.vkCode))
    'End Sub

    Public Sub Leagueconnect()
        Dim proce = Process.GetProcessesByName("LeagueClientUx")

        If proce.Length <> 0 Then

            For Each getid In proce

                Using mos As ManagementObjectSearcher = New ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " & getid.Id)

                    For Each mo As ManagementObject In mos.[Get]()

                        If mo("CommandLine") IsNot Nothing Then
                            Dim data As String = (mo("CommandLine").ToString())
                            Dim CommandlineArray As String() = data.Split(""""c)

                            For Each attributes In CommandlineArray
                                'TextBox1.AppendText(attributes & vbCrLf)
                                If attributes.Contains("token") OrElse attributes.Contains("remoting-auth-token") Then
                                    Dim token As String() = attributes.Split("="c)
                                    Form1.token = token(1)
                                End If

                                If attributes.Contains("app-port") OrElse attributes.Contains("app-port") Then
                                    Dim port As String() = attributes.Split("="c)
                                    Form1.port = port(1)
                                End If
                            Next

                            If String.IsNullOrWhiteSpace(Form1.token) OrElse String.IsNullOrWhiteSpace(Form1.port) Then
                                TextBox1.AppendText("League of Legends process is detected but no information can be extracted." & vbCrLf)
                                Me.Close()
                            End If
                            'TextBox1.AppendText("Connect OK" & vbCrLf)
                            'TextBox1.AppendText("token : " & Form1.token & vbCrLf)
                            'TextBox1.AppendText("port : " & Form1.port & vbCrLf)
                            Return
                        End If
                    Next
                End Using
            Next
        End If

        TextBox1.AppendText("Could not find the League of Legends process, is League of Legends running?" & vbCrLf)
        Me.Close()
    End Sub

    Private Sub But3_Click(sender As Object, e As EventArgs) Handles But3.Click
        If pagedata.Rows.Count > 0 Then
            Dim nn As Integer = PageBox.SelectedIndex
            Dim namae As String = Cbox1.SelectedItem & " " & TextBox5.Text
            'Leagueconnect()
            WritePage(nn, namae)
            WriteSpell(nn)
        Else
            TextBox1.AppendText("Error : No Data" & vbCrLf)
        End If

    End Sub


    Public Sub DeletePage()
        If DeleteCheck.Checked Then
            http = New HttpClient()
            Dim response As HttpResponse = Nothing

            Try
                http.Request.Accept = HttpContentTypes.ApplicationJson
                http.Request.ForceBasicAuth = True
                http.Request.SetBasicAuthentication("riot", token)
                response = http.[Get]("https://127.0.0.1:" & port & "/lol-perks/v1/currentpage")
            Catch e As Exception
                TextBox1.AppendText("Error : No Response" & vbCrLf)
                Exit Sub
                'Leagueconnect()
                'http.Request.Accept = HttpContentTypes.ApplicationJson
                'http.Request.ForceBasicAuth = True
                'http.Request.SetBasicAuthentication("riot", token)
                'response = http.[Get]("https://127.0.0.1:" & port & "/lol-perks/v1/currentpage")
            End Try

            Dim currentpage = response.DynamicBody
            Dim deleteid As Integer = currentpage.id

            If deleteid = 54 OrElse deleteid = 53 OrElse deleteid = 52 OrElse deleteid = 51 OrElse deleteid = 50 Then
                TextBox1.AppendText("Cant Delete Pages, Looks like its only Riots default pages left, if you know this is wrong, click the page once so it gets set to current." & vbCrLf)
            Else
                http.Delete("https://127.0.0.1:" & port & "/lol-perks/v1/pages/" & deleteid)
            End If
        End If
    End Sub

    Private Sub WriteSpell(nn As Integer)

        'If PageBox.SelectedItem Is Nothing Then PageBox.SelectedIndex = 0
        'Dim selectedPage As String = PageBox.SelectedItem.ToString()
        'Dim runes As 

        'Try

        Dim Runestart As Integer = pagedata.Rows(nn).Item("rp")

        'Dim name As String = namae
        Dim rune1 As Integer
        Dim rune2 As Integer
        If sspfr = False Then
            rune1 = pagedata.Rows(nn).Item("s2")
            rune2 = pagedata.Rows(nn).Item("s1")
        Else
            rune1 = pagedata.Rows(nn).Item("s1")
            rune2 = pagedata.Rows(nn).Item("s2")
        End If
        ''Dim name As String = namae
        ''Dim Runestart As Integer = Integer.Parse(t0.Text)
        ''Dim rune1 As Integer = Integer.Parse(t1.Text)
        ''Dim rune2 As Integer = Integer.Parse(t2.Text)
        ''Dim rune3 As Integer = Integer.Parse(t3.Text)
        ''Dim rune4 As Integer = Integer.Parse(t4.Text)
        ''Dim rune5 As Integer = Integer.Parse(y2.Text)
        ''Dim rune6 As Integer = Integer.Parse(y3.Text)
        ''Dim secondary As Integer = Integer.Parse(y0.Text)


        '"{""name"":""" &

        'Dim inputLCUx As String = "{""selectedSkinId"":0" & ",""spell1Id"":" & rune1 & ","" & ""spell2Id & "":" & rune2 & ",""wardSkinId"":0" & "}"



        '    Dim Runestart As Integer = runes._runeStart
        '    Dim name As String = runes._pageName
        '    Dim rune1 As Integer = runes._rune1
        '    Dim rune2 As Integer = runes._rune2
        '    Dim rune3 As Integer = runes._rune3
        '    Dim rune4 As Integer = runes._rune4
        '    Dim rune5 As Integer = runes._rune5
        '    Dim rune6 As Integer = runes._rune6
        '    Dim secondary As Integer = runes._runeSecondary
        'Dim inputLCUx As String = "{""selectedSkinId"": 0""",  ""spell1Id"": " & Rune1,  """spell2Id"": "" & Rune2,  ""wardSkinId : 0"" & "}"
        'Dim inputLCUx As String = "{""name"":""" & Name & """,""primaryStyleId"":" & Runestart & ",""selectedPerkIds"": [" & rune1 & "," & rune2 & "," & rune3 & "," & rune4 & "," & rune5 & "," & rune6 & "],""subStyleId"":" & secondary & "}"
        Dim inputLCUx As String = "{""spell1Id"": " & rune1 & ",""spell2Id"":  " & rune2 & "}"
        'Dim inputLCUx As String = "{""selectedSkinId"": 0" & ",""spell1Id"":" & rune1 & ","" & ""spell2Id & "":" & rune2 & ",""wardSkinId"": 0" & "}"
        Dim response As HttpResponse = Nothing

        Try
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.Patch("https://127.0.0.1:" & port & "/lol-champ-select/v1/session/my-selection", inputLCUx, HttpContentTypes.ApplicationJson)
        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response" & vbCrLf)
            Exit Sub
            'Leagueconnect()
            'Dim password As String = token
            'http.Request.Accept = HttpContentTypes.ApplicationJson
            'http.Request.SetBasicAuthentication("riot", password)
            'response = http.Patch("https://127.0.0.1:" & port & "/lol-champ-select/v1/session/my-selection", inputLCUx, HttpContentTypes.ApplicationJson)
        End Try


        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            'Dim [error] = response.StaticBody(Of [Error])()
            'If [error].message.Equals("Max pages reached") Then MessageBox.Show("Max Pages Reached")
        End If

        'TextBox1.AppendText(inputLCUx & vbCrLf)
        'Catch Exception As Exception
        '    MessageBox.Show(Exception.Message)
        'End Try
    End Sub




    Private Sub WritePage(nn As Integer, namae As String)
        DeletePage()
        'If PageBox.SelectedItem Is Nothing Then PageBox.SelectedIndex = 0
        'Dim selectedPage As String = PageBox.SelectedItem.ToString()
        'Dim runes As 

        'Try

        Dim Runestart As Integer = pagedata.Rows(nn).Item("rp")

        Dim name As String = namae
        Dim rune1 As Integer = pagedata.Rows(nn).Item("r1")
        Dim rune2 As Integer = pagedata.Rows(nn).Item("r2")
        Dim rune3 As Integer = pagedata.Rows(nn).Item("r3")
        Dim rune4 As Integer = pagedata.Rows(nn).Item("r4")
        Dim rune5 As Integer = pagedata.Rows(nn).Item("r5")
        Dim rune6 As Integer = pagedata.Rows(nn).Item("r6")
        Dim rune7 As Integer = pagedata.Rows(nn).Item("r7")
        Dim rune8 As Integer = pagedata.Rows(nn).Item("r8")
        Dim rune9 As Integer = pagedata.Rows(nn).Item("r9")
        Dim secondary As Integer = pagedata.Rows(nn).Item("rs")
        ''Dim name As String = namae
        ''Dim Runestart As Integer = Integer.Parse(t0.Text)
        ''Dim rune1 As Integer = Integer.Parse(t1.Text)
        ''Dim rune2 As Integer = Integer.Parse(t2.Text)
        ''Dim rune3 As Integer = Integer.Parse(t3.Text)
        ''Dim rune4 As Integer = Integer.Parse(t4.Text)
        ''Dim rune5 As Integer = Integer.Parse(y2.Text)
        ''Dim rune6 As Integer = Integer.Parse(y3.Text)
        ''Dim secondary As Integer = Integer.Parse(y0.Text)








        '    Dim Runestart As Integer = runes._runeStart
        '    Dim name As String = runes._pageName
        '    Dim rune1 As Integer = runes._rune1
        '    Dim rune2 As Integer = runes._rune2
        '    Dim rune3 As Integer = runes._rune3
        '    Dim rune4 As Integer = runes._rune4
        '    Dim rune5 As Integer = runes._rune5
        '    Dim rune6 As Integer = runes._rune6
        '    Dim secondary As Integer = runes._runeSecondary
        Dim inputLCUx As String = "{""name"":""" & name & """,""primaryStyleId"":" & Runestart & ",""selectedPerkIds"": [" & rune1 & "," & rune2 & "," & rune3 & "," & rune4 & "," & rune5 & "," & rune6 & "," & rune7 & "," & rune8 & "," & rune9 & "],""subStyleId"":" & secondary & "}"
        Dim response As HttpResponse = Nothing

        Try
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.Post("https://127.0.0.1:" & port & "/lol-perks/v1/pages", inputLCUx, HttpContentTypes.ApplicationJson)
        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response" & vbCrLf)
            Exit Sub
            ''Leagueconnect()
            'Dim password As String = token
            'http.Request.Accept = HttpContentTypes.ApplicationJson
            'http.Request.SetBasicAuthentication("riot", password)
            'response = http.Post("https://127.0.0.1:" & port & "/lol-perks/v1/pages", inputLCUx, HttpContentTypes.ApplicationJson)
        End Try


        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            Dim [error] = response.StaticBody(Of [Error])()
            If [error].message.Equals("Max pages reached") Then MessageBox.Show("Max Pages Reached")
        End If


        'Catch Exception As Exception
        '    MessageBox.Show(Exception.Message)
        'End Try
    End Sub

    Public Shared Function Create1bppImage(ByVal img As Bitmap) As Bitmap
        '1bppイメージを作成する
        Dim newImg As New Bitmap(img.Width, img.Height,
                             PixelFormat.Format1bppIndexed)

        'Bitmapをロックする
        Dim bmpDate As BitmapData = newImg.LockBits(
        New Rectangle(0, 0, newImg.Width, newImg.Height),
        ImageLockMode.WriteOnly, newImg.PixelFormat)

        '新しい画像のピクセルデータを作成する
        Dim pixels As Byte() = New Byte(bmpDate.Stride * bmpDate.Height - 1) {}
        For y As Integer = 0 To bmpDate.Height - 1
            For x As Integer = 0 To bmpDate.Width - 1
                '明るさが0.5以上の時は白くする
                If 0.5F <= img.GetPixel(x, y).GetBrightness() Then
                    'ピクセルデータの位置
                    Dim pos As Integer = (x >> 3) + bmpDate.Stride * y
                    '白くする
                    pixels(pos) = pixels(pos) Or CByte(&H80 >> (x And &H7))
                End If
            Next
        Next
        '作成したピクセルデータをコピーする
        Dim ptr As IntPtr = bmpDate.Scan0
        System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length)

        'ロックを解除する
        newImg.UnlockBits(bmpDate)

        Return newImg
    End Function



    Public Class [Error]
        <JsonName("errorCode")>
        Public Property errorCode As String
        <JsonName("httpStatus")>
        Public Property httpStatus As Integer
        <JsonName("message")>
        Public Property message As String
    End Class

    Public Class [Error2]
        <JsonName("errorCode")>
        Public Property errorCode As String
        <JsonName("httpStatus")>
        Public Property httpStatus As Integer
        <JsonName("message")>
        Public Property message As String
    End Class
    '■Brighten
    ''' <summary>画像の明るさを設定する。</summary>
    ''' <param name="Source">対象の画像</param>
    ''' <param name="Alpha">明るさ。-255～の範囲で指定。</param>
    ''' <returns>明るさが設定された画像</returns>
    Private Function Brighten(ByVal Source As Image, ByVal Alpha As Integer) As Bitmap

        '▼引数のチェック
        If IsNothing(Source) Then
            Throw New NullReferenceException("Sourceに値が設定されていません。")
        End If

        If Alpha < -255 OrElse Alpha > 255 Then
            Throw New ArgumentException("Alphaは-255 から255 の範囲で指定してください。")
        End If

        '▼Sourceのイメージをそのまま描画
        Dim g As Graphics
        Dim SourceImage As New Bitmap(Source.Width, Source.Height)

        g = Graphics.FromImage(SourceImage)
        g.DrawImage(Source, New Point(0, 0))

        '▼Sourceのイメージの上に白い(黒い)長方形を重ねる
        Dim MyBrush As SolidBrush

        If Alpha > 0 Then
            '白い長方形を作成
            MyBrush = New SolidBrush(Color.FromArgb(Alpha, 255, 255, 255))
            g.FillRectangle(MyBrush, Source.GetBounds(GraphicsUnit.Pixel))
        Else
            '黒い長方形を作成
            MyBrush = New SolidBrush(Color.FromArgb(-Alpha, 0, 0, 0))
            g.FillRectangle(MyBrush, Source.GetBounds(GraphicsUnit.Pixel))

        End If

        Return SourceImage

    End Function

    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    '    Panel2.Focus()
    'End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim o As Integer = Writecheck()
        If o = 0 Then
            Call Csvw()
        Else
            TextBox1.AppendText("Error :" & o)
        End If

    End Sub

    Function Writecheck()

        Dim e As Integer = 0
        If t0.TextLength < 4 Then
            e = 1
        End If
        If t1.TextLength < 4 Then
            e = 2
        End If
        If t2.TextLength < 4 Then
            e = 3
        End If
        If t3.TextLength < 4 Then
            e = 4
        End If
        If t4.TextLength < 4 Then
            e = 5
        End If
        If y0.TextLength < 4 Then
            e = 6
        End If
        If y2.TextLength < 4 Then
            e = 7
        End If
        If y3.TextLength < 4 Then
            e = 8
        End If
        If TextBox5.TextLength < 1 Then
            e = 9
        End If
        If champkeybox.SelectedItem Is Nothing Then
            e = 10
        End If
        If mmm1.TextLength < 4 Then
            e = 11
        End If
        If mmm2.TextLength < 4 Then
            e = 12
        End If
        If mmm3.TextLength < 4 Then
            e = 13
        End If



        'TextBox1.AppendText("Error :" & e)
        'If e = 0 Then
        '    'Call Csvw()
        '    
        'End If
        Return e

    End Function

    Private Sub Csvw()



        If oredatabin.Columns.Count = 0 Then
            oredatabin.Columns.Add("pn", GetType(String))
            oredatabin.Columns.Add("rp", GetType(Integer))
            oredatabin.Columns.Add("r1", GetType(Integer))
            oredatabin.Columns.Add("r2", GetType(Integer))
            oredatabin.Columns.Add("r3", GetType(Integer))
            oredatabin.Columns.Add("r4", GetType(Integer))
            oredatabin.Columns.Add("rs", GetType(Integer))
            oredatabin.Columns.Add("r5", GetType(Integer))
            oredatabin.Columns.Add("r6", GetType(Integer))
            oredatabin.Columns.Add("r7", GetType(Integer))
            oredatabin.Columns.Add("r8", GetType(Integer))
            oredatabin.Columns.Add("r9", GetType(Integer))



            oredatabin.Columns.Add("id", GetType(Integer))
            oredatabin.Columns.Add("s1", GetType(Integer))
            oredatabin.Columns.Add("s2", GetType(Integer))

        End If


        Dim name As String = TextBox5.Text
        Dim Runestart As Integer = Integer.Parse(t0.Text)
        Dim rune1 As Integer = Integer.Parse(t1.Text)
        Dim rune2 As Integer = Integer.Parse(t2.Text)
        Dim rune3 As Integer = Integer.Parse(t3.Text)
        Dim rune4 As Integer = Integer.Parse(t4.Text)
        Dim rune5 As Integer = Integer.Parse(y2.Text)
        Dim rune6 As Integer = Integer.Parse(y3.Text)
        Dim rune7 As Integer = Integer.Parse(mmm1.Text)
        Dim rune8 As Integer = Integer.Parse(mmm2.Text)
        Dim rune9 As Integer = Integer.Parse(mmm3.Text)



        Dim secondary As Integer = Integer.Parse(y0.Text)
        Dim id As Integer = Integer.Parse(champkeybox.SelectedItem)
        Dim pst1 As Integer = Integer.Parse(sssp1.Text)
        Dim pst2 As Integer = Integer.Parse(sssp2.Text)




        oredatabin.Rows.Add(name, Runestart, rune1, rune2, rune3, rune4, secondary, rune5, rune6, rune7, rune8, rune9, id, pst1, pst2)

        ConvertDataTableToCsv(oredatabin, Orepages, False, True)

        TextBox1.AppendText("PageSaved" & vbCrLf)
        oredatabin.Clear()
        runereformdata.Clear()

        LocalRuneReformLoad()
        Pagedataload2()  'Cbox1.SelectedIndex)
        If Cbox1.SelectedIndex = 0 Then
            Cbox1.SelectedIndex = champbox.SelectedIndex + 1
            Cbox1.SelectedIndex = champbox.SelectedIndex - 1
        Else
            Cbox1.SelectedIndex = champbox.SelectedIndex - 1
            Cbox1.SelectedIndex = champbox.SelectedIndex + 1
        End If

        ' CBox_SelectedIndexChanged2()
        'Dim nn As Integer = PageBox.SelectedIndex
        'Pagedataload(nn)
        'CBox.SelectedIndex = CBox.SelectedIndex - 1
        'CBox.SelectedIndex = CBox.SelectedIndex + 1
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Pagedataload2()
    End Sub

    Private Sub champbox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles champbox.SelectedIndexChanged
        champkeybox.SelectedIndex = champbox.SelectedIndex
        TextBox10.Text = Integer.Parse(champkeybox.SelectedItem)
        Cbox1.SelectedIndex = champbox.SelectedIndex

        r0.BackgroundImage = Nothing
        r1.BackgroundImage = Nothing
        r2.BackgroundImage = Nothing
        r3.BackgroundImage = Nothing
        r4.BackgroundImage = Nothing
        r0.Left = -50
        r1.Left = -50
        r2.Left = -50
        r3.Left = -50
        r4.Left = -50

        s0.Left = -50
        s2.Left = -50
        s3.Left = -50


        t0.Text = 0
        t1.Text = 0
        t2.Text = 0
        t3.Text = 0
        t4.Text = 0

        y0.Text = 0
        y2.Text = 0
        y3.Text = 0

        s4.Left = -500 'Me.Controls("s00").Left + (c1 * 45)
        s5.Left = Me.Controls("r00").Left
        'PageBox.Items.Clear()
        'pagedata.Clear()
        'Dim cc As Integer = champkeybox.SelectedIndex 'Integer.Parse(CType(sender, System.Windows.Forms.Button).Name)
        'Dim cm As Integer

        'Dim ra As Integer = runedata.Rows.Count
        'Dim rb As Integer = champdata.Rows.Count - 1
        'Dim rc As Integer = cc ' - ra
        'Dim rrd As Integer = runereformdata.Rows.Count - 1

        'cm = Integer.Parse(champdata.Rows(rc).Item("key"))
        'Dim namae As String = champdata.Rows(rc).Item("name")
        'TextBox1.AppendText(cm & vbCrLf)

        'Dim j As String = 0
        'For i As Integer = 0 To rrd
        '    Dim id As Integer = runereformdata.Rows(i).Item("id")
        '    If id = cm Then
        '        pagedata.Rows.Add(runereformdata.Rows(i).Item("pn"),
        '                          runereformdata.Rows(i).Item("rp"),
        '                          runereformdata.Rows(i).Item("r1"),
        '                          runereformdata.Rows(i).Item("r2"),
        '                          runereformdata.Rows(i).Item("r3"),
        '                          runereformdata.Rows(i).Item("r4"),
        '                          runereformdata.Rows(i).Item("rs"),
        '                          runereformdata.Rows(i).Item("r5"),
        '                          runereformdata.Rows(i).Item("r6"),
        '                          runereformdata.Rows(i).Item("id"))

        '        PageBox.Items.Add(j.ToString & " : " & namae & " " & runereformdata.Rows(i).Item("pn"))
        '        j = j + 1
        '    End If
        'Next
        'If pagedata.Rows.Count > 0 Then
        '    PageBox.SelectedIndex = 0
        '    DataGridView1.DataSource = pagedata
        '    But3.Enabled = True
        'Else
        '    TextBox1.AppendText("Data not found")
        'End If


    End Sub
    Public dai As Integer = 1
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If dai = 0 Then
            Me.Width = 458
            Me.Height = 310
            dai = 1
            Button4.Text = "<"
        Else
            Me.Width = 1062
            Me.Height = 705
            dai = 0
            Button4.Text = ">"
        End If

    End Sub

    Public oldid As Integer = 0
    Public themestartflag As Boolean = False
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Application.DoEvents()
        Dim response As HttpResponse = Nothing
        Try
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            ''TextBox1.AppendText("Error : No Response" & vbCrLf)
            'Me.close
            ''Exit Sub
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        'Label14.Text = selectf
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            oldid = 0

            'Timer3.Enabled = False
            If themestartflag Then
                themestartflag = False
                Timer7.Enabled = True
                'Timer5.Enabled = False
                Form3.theme_end()
                Form3.theme_start("butchersbridge\renata", 3000000)
            End If
        Else
            If themestartflag = False Then
                themestartflag = True
                Form3.theme_end()
                If Form3.RadioButton1.Checked Then

                    Form3.theme_start("starguardian_jp\championselect", 6000000)
                End If
                If Form3.RadioButton2.Checked Then
                    Form3.theme_start("starguardian_en\championselect", 6000000)
                End If
                If Form3.RadioButton3.Checked Then
                    Form3.theme_start("butchersBridge\butcher's_bridge_1_champion_select", 6000000)
                End If
            End If



            'If response.StatusCode = Nothing Then
            '    TextBox1.AppendText("Error : No Response" & vbCrLf)
            '    Exit Sub
            'Else
            'Dim response2 As HttpResponse = Nothing
            'Try
            '    Dim password As String = token
            '    http.Request.Accept = HttpContentTypes.ApplicationJson
            '    http.Request.SetBasicAuthentication("riot", password)
            '    response2 = http.[Get]("https://127.0.0.1:" & port & "/lol-gameflow/v1/session") ', inputLCUx, HttpContentTypes.ApplicationJson)

            'Catch Exception As Exception

            'End Try
            'If response2.StatusCode <> System.Net.HttpStatusCode.OK Then
            '    Timer3.Enabled = False
            'Else
            'Dim gamemode As String = response2.DynamicBody
            'If onece = False Then
            '    Timer3.Enabled = True
            '    once = True
            'End If


            Skinb4()

            Dim currentChamp As Integer = response.DynamicBody
            If oldid <> currentChamp Then
                Dim Champid As Integer = currentChamp
                If Champid <> 0 Then

                    Dim rb As Integer = champdata.Rows.Count - 1
                    For i As Integer = 0 To rb
                        Dim key As Integer = champdata.Rows(i).Item("key")
                        If key = Champid Then
                            'chmname = champdata.Rows(i).Item("id")
                            Cbox1.SelectedIndex = i
                            'Timer1.Interval = 60000
                            Timer5.Interval = 1000
                            Timer5.Enabled = True
                            TextBox1.AppendText("timer5 S" & vbCrLf)
                            'Timer3.Enabled = True

                            oldid = Champid
                            But3.PerformClick()
                            Exit For
                        End If

                    Next

                End If

            End If

            'End If
        End If



    End Sub

    Public lolt As Integer

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        Application.DoEvents()
        If Process.GetProcessesByName("League of Legends").Length <> 0 Then
            'If L = False Then
            'Timer4.Enabled = True
            'Timer1.Enabled = False
            '    'Timer3.Enabled = False
            'Timer2.Interval = 1000
            '    Timer2.Enabled = True
            '    Form2.onece()
            Form2.Label1.Text = "on"
            'L = True
            'TextBox1.AppendText("Process Active" & L & vbCrLf)
            'End If

            '    Dim sk As Integer = TextBox9.Text
            '    Skin(ccchamp, sk)
            '    onece = 1
            'End If
            stats()
            'mystats()
        Else
            Timer1.Enabled = True
            Form2.Timer1.Enabled = False
            Form2.Label1.Text = "off"
            Timer2.Enabled = False
            onece = False

            'L = False
            'Timer1.Enabled = True
            'TextBox1.AppendText("Process Active" & L & vbCrLf)
            'If L = True Then

            '    Button10.PerformClick()
            '    Form2.Timer1.Enabled = False
            '    Form2.Label1.Text = "off"
            '    TextBox1.AppendText("L once : " & L & vbCrLf)
            '    Timer2.Enabled = False
            '    onece = False
            '    L = False
            'End If
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim cd As Integer = champdata.Rows.Count - 1
        Webreq()
    End Sub


    Private Sub Webreq()


        Dim rc As Integer = champdata.Rows.Count - 1
        Dim ii As String = TextBox7.Text
        Dim iij As Integer = CInt(ii)
        For i As Integer = 0 + iij To rc
            orespe.Clear()
            sumspe.Clear()
            sumspe_bin.Clear()
            sumspe1.Clear()


            Dim na As String = champdata.Rows(i).Item("id")
            Dim key As Integer = champdata.Rows(i).Item("key")

            TextBox1.AppendText(na & vbCrLf)
            ' Dim enc As Encoding = Encoding.UTF8
            'Dim ndb As New NonDispBrowser


            'b.NavigateAndWait("https://www.metasrc.com/aram/na/champion/" & na)
            '  Dim doc As HtmlDocument = ndb.Document
            Dim url As String = "https://www.murderbridge.com/Champion/" & na
            Dim wc As System.Net.WebClient = New System.Net.WebClient
            Dim st As System.IO.Stream = wc.OpenRead(url)
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(st, enc)
            Dim html As String = sr.ReadToEnd
            sr.Close()
            st.Close()
            Dim doc As HtmlAgilityPack.HtmlDocument = New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(html)

            Dim nn As Integer = 0
            For Each item As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div[@class=""runes-column runes-primary""]//img[@class=""rune-image""][@alt]")

                If (Not String.IsNullOrEmpty(item.Attributes("alt").Value)) Then
                    Dim href As String = item.Attributes("alt").Value
                    Dim runes As String = href.Substring(5, 4)

                    TextBox1.AppendText(runes.ToString & vbCrLf)
                    If nn = 0 Then
                        Dim prr As String = runes.Substring(0, 2)
                        If prr = "91" Then
                            prr = "80"
                        End If
                        If prr = "99" Then
                            prr = "81"
                        End If
                        Dim pr As String = prr & "00"
                        sumspe1.Rows.Add(pr)
                    End If

                    sumspe1.Rows.Add(runes)
                End If
                nn += 1
            Next
            nn = 0
            For Each item As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div[@class=""runes-column runes-secondary""]//img[@class=""rune-image""][@alt]")

                If (Not String.IsNullOrEmpty(item.Attributes("alt").Value)) Then
                    Dim href As String = item.Attributes("alt").Value
                    Dim runes As String = href.Substring(5, 4)
                    TextBox1.AppendText(runes.ToString & vbCrLf)
                    If nn = 0 Then
                        Dim prr As String = runes.Substring(0, 2)
                        If prr = "91" Then
                            prr = "80"
                        End If
                        If prr = "99" Then
                            prr = "81"
                        End If
                        Dim pr As String = prr & "00"
                        sumspe1.Rows.Add(pr)
                    End If
                    sumspe1.Rows.Add(runes)
                End If
                nn += 1
            Next
            nn = 0
            For Each item As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div[@class=""stat-shard-left""]//img")
                Dim href As String = item.Attributes("src").Value
                'If (Not String.IsNullOrEmpty(item.Attributes("class").Value)) Then
                '    Dim href As String = item.Attributes("class").Value
                'Dim spli() As String = href.Split("/"c)

                Dim len As Integer = href.Length - 1
                Dim adp As String = ""
                Select Case len
                    Case 945
                        adp = "5008"
                    Case 1065
                        adp = "5005"
                    Case 1125
                        adp = "5007"

                    Case 949
                        adp = "5002"
                    Case 1377
                        adp = "5003"

                    Case 841
                        adp = "5001"
                    Case Else
                        adp = "9999"
                End Select
                TextBox1.AppendText(adp & vbCrLf)
                sumspe1.Rows.Add(adp)
                nn += 1
            Next


            For Each item As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div[@class=""summoners-row""]//div[@class =""image-square""]")
                Dim href As String = item.Attributes("style").Value
                Dim sum As String() = href.Split("/"c)
                Dim summ As String = sum(sum.Count - 1)
                Dim ssum As String = sum(sum.Count - 2)
                Dim sssum As String() = summ.Split(")"c)
                Dim su1 As String = sssum(0)
                If ssum = "spell" Then
                    Dim sums As String
                    Select Case su1
                        Case "SummonerBoost.png"
                            sums = "1"
                        Case "SummonerExhaust.png"
                            sums = "3"
                        Case "SummonerFlash.png"
                            sums = "4"
                        Case "SummonerBacktrack.png"
                            sums = "5"
                        Case "SummonerHaste.png"
                            sums = "6"
                        Case "SummonerHeal.png"
                            sums = "7"
                        Case "SummonerSmite.png"
                            sums = "11"
                        Case "SummonerTeleport.png"
                            sums = "12"
                        Case "SummonerMana.png"
                            sums = "13"
                        Case "SummonerDot.png"
                            sums = "14"
                        Case "SummonerBarrier.png"
                            sums = "21"
                        Case "SummonerSnowball.png"
                            sums = "32"
                        Case Else
                            sums = summ
                    End Select
                    TextBox1.AppendText(sums.ToString & vbCrLf)
                    sumspe_bin.Rows.Add(sums)
                End If
            Next
            Dim spp1 As String = "4"
            Dim spp2 As String
            If sumspe_bin.Rows(0).Item(0) <> "4" Then
                spp2 = sumspe_bin.Rows(0).Item(0)
            Else
                spp2 = sumspe_bin.Rows(1).Item(0)
            End If
            sumspe1.Rows.Add(spp1)
            sumspe1.Rows.Add(spp2)

            orespe.Rows.Add("ARAM", sumspe1.Rows(0).Item("id"), sumspe1.Rows(1).Item("id"), sumspe1.Rows(2).Item("id"), sumspe1.Rows(3).Item("id"), sumspe1.Rows(4).Item("id"), sumspe1.Rows(5).Item("id"), sumspe1.Rows(6).Item("id"), sumspe1.Rows(7).Item("id"), sumspe1.Rows(8).Item("id"), sumspe1.Rows(9).Item("id"), sumspe1.Rows(10).Item("id"), key, sumspe1.Rows(11).Item("id"), sumspe1.Rows(12).Item("id"))
            ConvertDataTableToCsv(orespe, Orepages, False, True)
        Next


        Exit Sub


        '    '' リンク文字列とそのURLの列挙
        '    Dim j As Integer = 0
        '    'Dim k As Integer = 0
        '    'For Each e As HtmlElement In doc.GetElementsByTagName("img")
        '    For Each item As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//img[@class=""lozad""]")

        '        'If Not item Is Nothing And item.HasAttributes Then

        '        Dim href As String = item.Attributes("alt").Value
        '        'Dim href As String = item.Attributes("xlink:href").Value 'e.GetAttribute("src") ' HREF属性の値xlink:href


        '        If (Not String.IsNullOrEmpty(href)) Then
        '            'TextBox1.AppendText(j & href & vbCrLf)
        '            If j = 38 Or j = 40 Then
        '                Dim sums As String
        '                Select Case href
        '                    Case "Cleanse"
        '                        sums = "1"
        '                    Case "Exhaust"
        '                        sums = "3"
        '                    Case "Flash"
        '                        sums = "4"
        '                    Case "Backtrack"
        '                        sums = "5"
        '                    Case "Ghost"
        '                        sums = "6"
        '                    Case "Heal"
        '                        sums = "7"
        '                    Case "Smite"
        '                        sums = "11"
        '                    Case "Teleport"
        '                        sums = "12"
        '                    Case "Clarity"
        '                        sums = "13"
        '                    Case "Ignite"
        '                        sums = "14"
        '                    Case "Barrier"
        '                        sums = "21"
        '                    Case "Mark"
        '                        sums = "32"
        '                    Case Else
        '                        sums = "4"
        '                End Select
        '                sumspe.Rows.Add(sums)
        '            End If
        '            'Dim sum As String() = href.Split("/"c)
        '            'Dim summ As String = sum(sum.Count - 1)
        '            'Dim ssum As String = sum(sum.Count - 2)
        '            'If ssum = "spell" Then
        '            '    Dim sums As String
        '            '    Select Case summ
        '            '        Case "SummonerBoost.png"
        '            '            sums = "1"
        '            '        Case "SummonerExhaust.png"
        '            '            sums = "3"
        '            '        Case "SummonerFlash.png"
        '            '            sums = "4"
        '            '        Case "SummonerBacktrack.png"
        '            '            sums = "5"
        '            '        Case "SummonerHaste.png"
        '            '            sums = "6"
        '            '        Case "SummonerHeal.png"
        '            '            sums = "7"
        '            '        Case "SummonerSmite.png"
        '            '            sums = "11"
        '            '        Case "SummonerTeleport.png"
        '            '            sums = "12"
        '            '        Case "SummonerMana.png"
        '            '            sums = "13"
        '            '        Case "SummonerDot.png"
        '            '            sums = "14"
        '            '        Case "SummonerBarrier.png"
        '            '            sums = "21"
        '            '        Case "SummonerSnowball.png"
        '            '            sums = "32"
        '            '        Case Else
        '            '            sums = summ
        '            '    End Select

        '            '    sumspe.Rows.Add(sums)
        '            '    TextBox1.AppendText("S_Spell: " & sums & vbCrLf)
        '        End If
        '        j += 1
        '        'End If
        '    Next
        '    'Exit Sub
        '    j = 0
        '    For Each item As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//image")

        '        'If Not item Is Nothing And item.HasAttributes Then
        '        '    TextBox1.AppendText("売値: " & item.Attributes("xlink:href").Value & vbCrLf)
        '        'End If
        '        'Dim href As String = item.Attributes("xlink:href").Value 'e.GetAttribute("src") ' HREF属性の値xlink:href


        '        'If (Not String.IsNullOrEmpty(href)) Then
        '        '    Dim sum As String() = href.Split("/"c)
        '        '    Dim summ As String = sum(sum.Count - 1)
        '        '    Dim ssum As String = sum(sum.Count - 2)
        '        '    If ssum = "spell" Then
        '        '        sumspe.Rows.Add(summ)

        '        '        'j = j + 1
        '        '    End If
        '        'End If
        '        'sumspe.Rows.Add("6")
        '        'sumspe.Rows.Add("5")
        '        Dim runes As String = item.Attributes("xlink:href").Value 'e.GetAttribute("xlink:href")
        '        If j > 18 And j < 30 Then
        '            If (Not String.IsNullOrEmpty(runes)) Then
        '                Dim sum As String() = runes.Split("/"c)
        '                Dim summ As String = sum(sum.Count - 1)
        '                Dim sums As String = ""
        '                TextBox1.AppendText(j & summ & vbCrLf)
        '                Select Case summ

        '                    Case "8100.png"
        '                        sums = "8100"
        '                    Case "electrocute.png"
        '                        sums = "8112"
        '                    Case "predator.png"
        '                        sums = "8124"
        '                    Case "darkharvest.png"
        '                        sums = "8128"
        '                    Case "hailofblades.png"
        '                        sums = "9923"
        '                    Case "cheapshot.png"
        '                        sums = "8126"
        '                    Case "greenterror_tasteofblood.png"
        '                        sums = "8139"
        '                    Case "suddenimpact.png"
        '                        sums = "8143"
        '                    Case "zombieward.png"
        '                        sums = "8136"
        '                    Case "ghostporo.png"
        '                        sums = "8120"
        '                    Case "eyeballcollection.png"
        '                        sums = "8138"
        '                    Case "ravenoushunter.png"
        '                        sums = "8135"
        '                    Case "ingenioushunter.png"
        '                        sums = "8134"
        '                    Case "relentlesshunter.png"
        '                        sums = "8105"
        '                    Case "ultimatehunter.png"
        '                        sums = "8106"
        '                    Case "8300.png"
        '                        sums = "8300"
        '                    Case "glacialaugment.png"
        '                        sums = "8351"
        '                    Case "kleptomancy.png"
        '                        sums = "8359"
        '                    Case "unsealedspellbook.png"
        '                        sums = "8360"
        '                    Case "hextechflashtraption.png"
        '                        sums = "8306"
        '                    Case "magicalfootwear.png"
        '                        sums = "8304"
        '                    Case "perfecttiming.png"
        '                        sums = "8313"
        '                    Case "futuresmarket.png"
        '                        sums = "8321"
        '                    Case "miniondematerializer.png"
        '                        sums = "8316"
        '                    Case "biscuitdelivery.png"
        '                        sums = "8345"
        '                    Case "cosmicinsight.png"
        '                        sums = "8347"
        '                    Case "approachvelocity.png"
        '                        sums = "8410"
        '                    Case "timewarptonic.png"
        '                        sums = "8352"
        '                    Case "8000.png"
        '                        sums = "8000"
        '                    Case "presstheattack.png"
        '                        sums = "8005"
        '                    Case "lethaltempotemp.png"
        '                        sums = "8008"
        '                    Case "fleetfootwork.png"
        '                        sums = "8021"
        '                    Case "conqueror.png"
        '                        sums = "8010"
        '                    Case "overheal.png"
        '                        sums = "9101"
        '                    Case "triumph.png"
        '                        sums = "9111"
        '                    Case "presenceofmind.png"
        '                        sums = "8009"
        '                    Case "legendalacrity.png"
        '                        sums = "9104"
        '                    Case "legendtenacity.png"
        '                        sums = "9105"
        '                    Case "legendbloodline.png"
        '                        sums = "9103"
        '                    Case "coupdegrace.png"
        '                        sums = "8014"
        '                    Case "cutdown.png"
        '                        sums = "8017"
        '                    Case "laststand.png"
        '                        sums = "8299"
        '                    Case "8400.png"
        '                        sums = "8400"
        '                    Case "graspoftheundying.png"
        '                        sums = "8437"
        '                    Case "veteranaftershock.png"
        '                        sums = "8439"
        '                    Case "guardian.png"
        '                        sums = "8465"
        '                    Case "demolish.png"
        '                        sums = "8446"
        '                    Case "fontoflife.png"
        '                        sums = "8463"
        '                    Case "mirrorshell.png"
        '                        sums = "8401"
        '                    Case "conditioning.png"
        '                        sums = "8429"
        '                    Case "secondwind.png"
        '                        sums = "8444"
        '                    Case "boneplating.png"
        '                        sums = "8473"
        '                    Case "overgrowth.png"
        '                        sums = "8451"
        '                    Case "revitalize.png"
        '                        sums = "8453"
        '                    Case "unflinching.png"
        '                        sums = "8242"
        '                    Case "8200.png"
        '                        sums = "8200"
        '                    Case "summonaery.png"
        '                        sums = "8214"
        '                    Case "arcanecomet.png"
        '                        sums = "8229"
        '                    Case "phaserush.png"
        '                        sums = "8230"
        '                    Case "pokeshield.png"
        '                        sums = "8224"
        '                    Case "manaflowband.png"
        '                        sums = "8226"
        '                    Case "6361.png"
        '                        sums = "8275"
        '                    Case "transcendence.png"
        '                        sums = "8210"
        '                    Case "celeritytemp.png"
        '                        sums = "8234"
        '                    Case "absolutefocus.png"
        '                        sums = "8233"
        '                    Case "scorch.png"
        '                        sums = "8237"
        '                    Case "waterwalking.png"
        '                        sums = "8232"
        '                    Case "gatheringstorm.png"
        '                        sums = "8236"
        '                    Case "statmodsadaptiveforceicon.png"
        '                        sums = "5008"
        '                    Case "statmodshealthscalingicon.png"
        '                        sums = "5001"
        '                    Case "statmodsarmoricon.png"
        '                        sums = "5002"
        '                    Case "statmodsattackspeedicon.png"
        '                        sums = "5005"
        '                    Case "statmodscdrscalingicon.png"
        '                        sums = "5007"
        '                    Case "statmodsmagicresicon.png"
        '                        sums = "5003"
        '                    Case "firststrike.png"
        '                        sums = "8369"
        '                    Case Else
        '                        sums = summ
        '                End Select




        '                sumspe1.Rows.Add(sums)

        '                'k = k + 1

        '            End If
        '        End If
        '        j += 1

        '    Next
        '    'Exit Sub
        '    TextBox1.AppendText("...done" & vbCrLf)
        '    orespe.Rows.Add("ARAM", sumspe1.Rows(0).Item("id"), sumspe1.Rows(1).Item("id"), sumspe1.Rows(2).Item("id"), sumspe1.Rows(3).Item("id"), sumspe1.Rows(4).Item("id"), sumspe1.Rows(5).Item("id"), sumspe1.Rows(7).Item("id"), sumspe1.Rows(6).Item("id"), sumspe1.Rows(8).Item("id"), sumspe1.Rows(9).Item("id"), sumspe1.Rows(10).Item("id"), key, sumspe.Rows(0).Item("id"), sumspe.Rows(1).Item("id"))
        '    ConvertDataTableToCsv(orespe, Orepages, False, True)
        '    '   ndb.Dispose()

        'Next
        'TextBox1.AppendText("...Web scraping is complete" & vbCrLf)








    End Sub

    Public Sub Webrr()
        Dim url As String = "https://www.metasrc.com/na/aram/champion/lucian"

        'WebRequestの作成
        Dim webreq As System.Net.HttpWebRequest =
        CType(System.Net.WebRequest.Create(url),
        System.Net.HttpWebRequest)

        Dim webres As System.Net.HttpWebResponse = Nothing
        Try
            'サーバーからの応答を受信するためのWebResponseを取得
            webres = CType(webreq.GetResponse(), System.Net.HttpWebResponse)

            '応答したURIを表示する
            Console.WriteLine(webres.ResponseUri)
            '応答ステータスコードを表示する
            Console.WriteLine("{0}:{1}",
                webres.StatusCode, webres.StatusDescription)
        Catch ex As System.Net.WebException
            'HTTPプロトコルエラーかどうか調べる
            If ex.Status = System.Net.WebExceptionStatus.ProtocolError Then
                'HttpWebResponseを取得
                Dim errres As System.Net.HttpWebResponse =
                        CType(ex.Response, System.Net.HttpWebResponse)
                '応答したURIを表示する
                Console.WriteLine(errres.ResponseUri)
                '応答ステータスコードを表示する
                Console.WriteLine("{0}:{1}",
                        errres.StatusCode, errres.StatusDescription)
            Else
                Console.WriteLine(ex.Message)
            End If
        Finally
            '閉じる
            If Not (webres Is Nothing) Then
                webres.Close()
            End If
        End Try
    End Sub


    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub smsp1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles smsp1.SelectedIndexChanged
        Dim ssi1 As Integer = smsp1.SelectedIndex
        Dim sp1 As String = sumspedata.Rows(ssi1).Item("key")
        Dim imm As String = "images\" & sp1 & ".jpg"
        Dim img As Image = Image.FromFile(imm)
        scs1.BackgroundImage = img
        sssp1.Text = sp1

    End Sub

    Private Sub smsp2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles smsp2.SelectedIndexChanged
        Dim ssi1 As Integer = smsp2.SelectedIndex
        Dim sp1 As String = sumspedata.Rows(ssi1).Item("key")
        Dim imm As String = "images\" & sp1 & ".jpg"
        Dim img As Image = Image.FromFile(imm)
        scs2.BackgroundImage = img
        sssp2.Text = sp1

    End Sub
    Public collo As Integer = 0
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If collo = 0 Then
            Me.BackColor = Color.Lime
            For i As Integer = 0 To 5
                Me.Controls("perk" & i).BackColor = Color.Lime
            Next

            collo = 1
        Else
            Me.BackColor = Color.Black
            For i As Integer = 0 To 5
                Me.Controls("perk" & i).BackColor = Color.Black
            Next
            collo = 0
        End If

    End Sub

    'Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
    Private Sub cmls()
        Application.DoEvents()
        If LVdata.Columns.Count = 0 Then
            LVdata.Columns.Add("id", GetType(String))
            LVdata.Columns.Add("key", GetType(Integer))
            LVdata.Columns.Add("lvl", GetType(Integer))
            LVdata.Columns.Add("mp", GetType(Integer))
        End If
        Dim response As HttpResponse = Nothing
        Try
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-collections/v1/inventories/" & checkid & "/champion-mastery")
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
            oldid = 0
        Else
            Dim grid = response.DynamicBody
            '    Dim jsonObj As Object = JsonConvert.DeserializeObject(grid.ToString)

            Dim i As Integer = 0
            For Each items In grid
                'TextBox1.AppendText(i & grid(i).name & vbCrLf)
                Dim name As String = i
                Dim id As String = grid(i).championId
                Dim masterylevel As String = grid(i).championLevel
                Dim masterypoints As String = grid(i).championPoints
                i += 1
                LVdata.Rows.Add(name, id, masterylevel, masterypoints)
            Next
            If LVdata.Rows.Count > 0 Then
                LVdata.Rows.RemoveAt(0)
            End If
            ConvertDataTableToCsv(LVdata, "lvl.csv", True, False)
        End If


    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Dim ch As Integer = Integer.Parse(champkeybox.SelectedItem)
        Dim sk As Integer = TextBox9.Text
        Skin(ch, sk)

        Exit Sub

        Dim response As HttpResponse = Nothing
        Try
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            ''TextBox1.AppendText("Error : No Response" & vbCrLf)
            'Me.close
            ''Exit Sub
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            Dim currentChamp As Integer = Integer.Parse(champkeybox.SelectedItem)
            Skin(currentChamp, 0)
        Else

            Dim currentChamp As Integer = response.DynamicBody
            Skin(currentChamp, 0)
        End If

    End Sub

    Private Sub Skinb4()
        Dim response As HttpResponse = Nothing
        Try
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            ''TextBox1.AppendText("Error : No Response" & vbCrLf)
            'Me.close
            ''Exit Sub
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            Dim currentChamp As Integer = Integer.Parse(champkeybox.SelectedItem)
            'ccchamp = currentChamp
            Skin(currentChamp, 0)
        Else

            Dim currentChamp As Integer = response.DynamicBody
            'ccchamp = currentChamp
            Skin(currentChamp, 0)
        End If

    End Sub



    Private Sub Skin(currentChamp As Integer, k As Integer)
        Dim response As HttpResponse = Nothing

        Dim uu As String = ""
        Try
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/session")

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
            'Dim grid = response.DynamicBody

            'Dim sid As String = ""
            'Dim nn As Integer
            Dim skillno1 As String
            Dim champ1 As String = ""
            'Dim i As Integer = 0
            'For Each items In grid

            '    sid = (grid.myTeam(i).summonerId).ToString
            '    If sid = "20180151" Then
            '        nn = i
            '        Exit For
            '    End If
            '    i += 1
            'Next
            Dim ret0 = k ' (grid.myTeam(nn).selectedSkinId).ToString
            Dim response1 As HttpResponse = Nothing
            Try
                Leagueconnect()
                Dim password As String = token
                http.Request.Accept = HttpContentTypes.ApplicationJson
                http.Request.SetBasicAuthentication("riot", password)
                response1 = http.[Get]("https://127.0.0.1:" & port & "/lol-champions/v1/inventories/" & checkid & "/champions/" & currentChamp & "/skins")

            Catch Exception As Exception
                TextBox1.AppendText("Error : No Response1" & vbCrLf)
                Exit Sub
                'Leagueconnect()
                'Dim password As String = token
                'http.Request.Accept = HttpContentTypes.ApplicationJson
                'http.Request.SetBasicAuthentication("riot", password)
                'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

            End Try
            If response1.StatusCode <> System.Net.HttpStatusCode.OK Then



            Else
                Dim grid1 = response1.DynamicBody
                Dim i As Integer = 0
                Dim nnn As Integer = 99999
                Dim skinn As Integer = 0
                For Each item In grid1
                    If grid1(i).id IsNot Nothing Then
                        If k = (grid1(i).id).ToString Then
                            nnn = i
                            Exit For
                        End If

                    End If
                    i += 1
                Next
                i = 0
                For Each items In grid1
                    If grid1(i).chromas IsNot Nothing Then
                        Dim j As Integer = 0
                        For Each items2 In grid1(i).chromas
                            If grid1(i).chromas(j) IsNot Nothing Then

                                Dim chroma1 As String = (grid1(i).chromas(j).id).ToString

                                If k = chroma1 Then
                                    nnn = i
                                End If

                            End If

                            j += 1


                        Next

                    End If
                    i += 1
                Next
                If nnn = 99999 Then
                    nnn = 0
                End If
                Dim chroma As String = grid1(nnn).id
                Dim l As Integer = chroma.Length - 3
                Dim ret1 As String = chroma.Substring(l)
                Dim ret2 As Integer = Integer.Parse(ret1)
                Dim Champid As Integer = currentChamp
                If Champid <> 0 Then

                    Dim rb As Integer = champdata.Rows.Count - 1
                    For i = 0 To rb
                        Dim key As Integer = champdata.Rows(i).Item("key")
                        If key = Champid Then
                            champ1 = champdata.Rows(i).Item("id")
                            chmname = champ1
                            Exit For
                        End If
                    Next
                    skillno1 = champ1 & "_" & ret2 & ".jpg"
                    Dim patch As String = vernew
                    'If System.IO.Directory.Exists("images\" & patch & "\loading\") Then

                    'Else
                    '    System.IO.Directory.CreateDirectory("images\" & patch & "\loading\")
                    'End If
                    'TextBox1.AppendText(skillno1 & vbCrLf)
                    'If skillno1 = "Seraphine_1.jpg" Then
                    '    skillno1 = "Seraphine_3.jpg"
                    'End If

                    Dim imm As String = "images\" & patch & "\loading\" & skillno1
                        If System.IO.File.Exists(imm) Then
                        Else
                            TextBox1.AppendText("Loading data not found : " & imm & vbCrLf)
                            'imm = "loading\" & champ1 & "_0.jpg"
                            Dim wc As New System.Net.WebClient()
                            wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/champion/loading/" & skillno1, imm)
                            wc.Dispose()
                        End If

                        Dim img As Image = Image.FromFile(imm)
                        pa1.BackgroundImage = New Bitmap(img)

                        'TextBox1.AppendText(k & " : " & ret0 & " : " & chroma & vbCrLf)
                    End If


                End If

        Else

            Dim grid2 = response.DynamicBody

            Dim sid As String = ""
            Dim nn As Integer
            Dim skillno As String
            Dim champ As String = ""
            Dim i As Integer = 0
            For Each items In grid2

                sid = (grid2.myTeam(i).summonerId).ToString
                If sid = checkid Then
                    nn = i
                    Exit For
                End If
                i += 1
            Next
            Dim ret0 = (grid2.myTeam(nn).selectedSkinId).ToString
            Dim response2 As HttpResponse = Nothing
            Try
                Leagueconnect()
                Dim password As String = token
                http.Request.Accept = HttpContentTypes.ApplicationJson
                http.Request.SetBasicAuthentication("riot", password)
                response2 = http.[Get]("https://127.0.0.1:" & port & "/lol-champions/v1/inventories/" & checkid & "/champions/" & currentChamp & "/skins")

            Catch Exception As Exception
                TextBox1.AppendText("Error : No Response2" & vbCrLf)
                Exit Sub
                'Leagueconnect()
                'Dim password As String = token
                'http.Request.Accept = HttpContentTypes.ApplicationJson
                'http.Request.SetBasicAuthentication("riot", password)
                'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

            End Try
            If response2.StatusCode <> System.Net.HttpStatusCode.OK Then



            Else
                Dim grid3 = response2.DynamicBody
                i = 0
                Dim nnn As Integer = 99999
                Dim skinn As Integer = 0
                For Each item In grid3
                    If grid3(i).id IsNot Nothing Then
                        If ret0 = (grid3(i).id).ToString Then
                            nnn = i
                            Exit For
                        End If

                    End If
                    i += 1
                Next
                If nnn = 99999 Then
                    i = 0
                    For Each items In grid3
                        If grid3(i).chromas IsNot Nothing Then
                            Dim j As Integer = 0
                            For Each items2 In grid3(i).chromas
                                If grid3(i).chromas(j) IsNot Nothing Then

                                    Dim chroma1 As String = (grid3(i).chromas(j).id).ToString

                                    If ret0 = chroma1 Then
                                        nnn = i
                                    End If
                                    'TextBox1.AppendText(k & " : " & ret0 & " : " & chroma1 & vbCrLf)
                                End If

                                j += 1


                            Next

                        End If
                        i += 1
                    Next


                End If
                If nnn = 99999 Then
                    nnn = 0
                End If
                Dim chroma As String = grid3(nnn).id
                Dim l As Integer = chroma.Length - 3
                Dim ret1 As String = chroma.Substring(l)
                Dim ret2 As Integer = Integer.Parse(ret1)
                Dim Champid As Integer = currentChamp
                If Champid <> 0 Then

                    Dim rb As Integer = champdata.Rows.Count - 1
                    For i = 0 To rb
                        Dim key As Integer = champdata.Rows(i).Item("key")
                        If key = Champid Then
                            champ = champdata.Rows(i).Item("id")
                            chmname = champ
                            Exit For
                        End If
                    Next
                    TextBox1.AppendText("champ : " & champ & ":" & "_" & ret2 & vbCrLf)
                    If champ = "Seraphine" And (ret2 = 1 Or ret2 = 0) Then
                        ret2 = 3
                    End If
                    skillno = champ & "_" & ret2 & ".jpg"
                    Dim patch As String = vernew
                    'If System.IO.Directory.Exists("images\" & patch & "\loading\") Then

                    'Else
                    '    System.IO.Directory.CreateDirectory("images\" & patch & "\loading\")
                    'End If
                    'TextBox1.AppendText(skillno & vbCrLf)
                    'If skillno = "Seraphine_1.jpg" Then
                    '    skillno = "Seraphine_3.jpg"
                    'End If

                    Dim imm As String = "images\" & patch & "\loading\" & skillno
                    If System.IO.File.Exists(imm) Then
                    Else
                        TextBox1.AppendText("tiles data not found : " & imm & vbCrLf)
                        'imm = "loading\" & champ1 & "_0.jpg"
                        Dim wc As New System.Net.WebClient()
                        wc.DownloadFile("http://ddragon.leagueoflegends.com/cdn/img/champion/loading/" & skillno, imm)
                        wc.Dispose()
                    End If


                    '-----------------------------------
                    'Dim imm As String = "loading\" & skillno
                    'If System.IO.File.Exists(imm) Then
                    'Else
                    '    TextBox1.AppendText("tiles data not found : " & imm & vbCrLf)
                    '    imm = "loading\" & champ & "_0.jpg"
                    'End If
                    '-----------------------------------------
                    Dim img As Image = Image.FromFile(imm)
                    pa1.BackgroundImage = New Bitmap(img)

                    TextBox9.Text = chroma
                    'TextBox1.AppendText(k & " : " & ret0 & " : " & chroma & vbCrLf)
                End If


                'Dim jsonObj As Object = JsonConvert.DeserializeObject(s)

                'If jsonObj("myTeam") IsNot Nothing Then
                '    For i As Integer = 0 To 4

                '            Dim ret0 = jsonObj("myTeam")(nn)("selectedSkinId").ToString
                '    Dim l As Integer = ret0.Length - 3
                '    Dim ret1 As String = ret0.Substring(l)
                '    Dim ret2 As Integer = Integer.Parse(ret1)

                '    Dim Champid As Integer = 39 'currentChamp
                '    If Champid <> 0 Then

                '        Dim rb As Integer = champdata.Rows.Count - 1
                '        For i As Integer = 0 To rb
                '            Dim key As Integer = champdata.Rows(i).Item("key")
                '            If key = Champid Then
                '                champ = champdata.Rows(i).Item("id")
                '                Exit For
                '            End If
                '        Next
                '        skillno = champ & "_" & ret2 & ".jpg"
                '        Dim imm As String = "tiles\" & skillno
                '        Dim img As Image = Image.FromFile(imm)
                '        Panel1.BackgroundImage = New Bitmap(img)
                '        TextBox1.AppendText(imm & vbCrLf)
                '    End If
                'End If
            End If
        End If

    End Sub

    Private Sub GetSummonerId()
        Dim response As HttpResponse = Nothing
        Try
            Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:" & port & "/lol-summoner/v1/current-summoner")

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
            Dim grid = response.DynamicBody
            checkacc = grid.accountId
            checkid = grid.summonerId
            checksl = grid.summonerLevel
            checkdn = grid.displayName
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 458
        Me.Height = 310
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            'RadioButton2.Checked = False
            aram = "aram"
            Orepages = "orepage.csv"
            runereformdata.Clear()
            LocalRuneReformLoad()
        Else
            aram = "5v5"
            Orepages = "orepage2.csv"
            runereformdata.Clear()
            LocalRuneReformLoad()
        End If
    End Sub

    'Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles 
    '    If RadioButton2.Checked = True Then
    '        RadioButton1.Checked = False

    '    Else

    '    End If
    'End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If sspfr = True Then
            sspfr = False
            Button9.Left = 15
        Else
            sspfr = True
            Button9.Left = 2
        End If
        Dim nn As Integer = PageBox.SelectedIndex
        Pagedataload(nn)
    End Sub

    Private Sub CBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBox.SelectedIndexChanged
        Dim dir As String = CBox.SelectedItem
        Dim img As Image = Image.FromFile(dir)
        pa1.BackgroundImage = New Bitmap(img)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Form2.Show()
        Form3.Show()
        'history()
    End Sub



    Private Sub Button11000000_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If Timer2.Enabled = True Then
            'Timer2.Enabled = False
            'Timer2.Interval = 1000
            'Timer1.Enabled = True
            'TextBox1.AppendText("Process Active" & L & vbCrLf)
            'If L = True Then
            '    L = False
            '    Button10.PerformClick()
            '    Form2.Timer1.Enabled = False
            '    Form2.Label1.Text = "off"
            '    Form3.Timer1.Enabled = False
            '    TextBox1.AppendText("L once : " & L & vbCrLf)
            '    '

            '    'once = False
            'End If
            Timer5.Enabled = True
        Else
            Timer2.Enabled = True
            Timer1.Enabled = False
            chmname = champbox.Text
        End If

        'tomtom()


        'Application.DoEvents()
        'If Process.GetProcessesByName("League of Legends").Length > 0 Then
        '    If L = False Then
        '        Timer1.Enabled = False
        '        Timer3.Enabled = False
        '        Timer2.Interval = 1000
        '        Timer2.Enabled = True
        '        Timer4.Enabled = True
        '        L = True
        '    End If

        '    '    Dim sk As Integer = TextBox9.Text
        '    '    Skin(ccchamp, sk)
        '    '    onece = 1
        '    'End If
        '    stats()
        '    mystats()
        'Else
        '    Timer1.Enabled = True
        '    TextBox1.AppendText("Process Active" & L & vbCrLf)
        '    If L = True Then
        '        L = False
        '        Button10.PerformClick()
        '        TextBox1.AppendText("L once : " & L & vbCrLf)
        '        'Timer2.Interval = 10000
        '        Timer2.Enabled = False


        '    End If

        '    'Timer2.Enabled = False
        '    'TextBox1.Text = "Prcsess Running" & vbCrLf
        '    'onece = 0
        'End If
        ''Skinb4()
    End Sub


    Private Sub stats()
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
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/playerlist")
            '  response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/all-grid-champions")
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response 00" & vbCrLf)

            Form2.Timer1.Enabled = False
            Form2.Label1.Text = "off"
            TextBox1.AppendText("L once : " & L & vbCrLf)
            Timer2.Enabled = False

            'For i As Integer = 0 To 9
            '    Me.Controls("sc" & i).Text = ""
            'Next
            'For i As Integer = 0 To 9
            '    Me.Controls("msc" & i).Text = ""
            'Next
            onece = False
            L = False

            Exit Sub
            'Leagueconnect()
            'Dim password As String = token
            'http.Request.Accept = HttpContentTypes.ApplicationJson
            'http.Request.SetBasicAuthentication("riot", password)
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            oldid = 0
        Else
            Dim grid = response.DynamicBody
            Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            Dim kill(9) As Integer
            Dim death(9) As Integer
            Dim ass(9) As Integer
            Dim i As Integer = 0
            'Dim j As Integer = 0
            'Dim k As Integer = 0

            For Each item In grid
                'Application.DoEvents()
                Form2.Pa7.Controls("lvl" & i).Text = grid(i).scores.creepScore
                Form2.Pa7.Controls("sc" & i).Text = grid(i).scores.kills & "/" & grid(i).scores.deaths & "/" & grid(i).scores.assists

                'For Each items In grid(i).items
                '    TextBox1.AppendText(k & ":::" & (grid(i).items(j).slot) & vbCrLf)
                '    If grid(i).items(k).slot <> 6 Then
                '        Me.Pa7.Controls("Panel" & j).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                '    Else
                '        j -= 1
                '    End If

                '    j += 1
                '    k += 1
                'Next
                i += 1
            Next
            'If grid(i).summonerName = "夜来香" Then
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
            Form2.TextBox7.Text = jsonObj
        End If
        ''https://127.0.0.1:52174/lol-match-history/v2/matchlist?begIndex=0&endIndex=10
    End Sub
    Private Sub mystats()
        Application.DoEvents()

        Dim response As HttpResponse = Nothing
        Try
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/activeplayer")


        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response　01" & vbCrLf)

            Form2.Timer1.Enabled = False
            Form2.Label1.Text = "off"
            TextBox1.AppendText("L once : " & L & vbCrLf)
            Timer2.Enabled = False

            'For i As Integer = 0 To 9
            '    Me.Controls("sc" & i).Text = ""
            'Next
            'For i As Integer = 0 To 9
            '    Me.Controls("msc" & i).Text = ""
            'Next
            onece = False
            L = False
            Timer1.Enabled = True
            Exit Sub

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then
            oldid = 0
        Else
            Dim grid = response.DynamicBody

            msc0.Text = ": " & Math.Round(grid.championStats.attackDamage, 0, MidpointRounding.AwayFromZero)
            msc1.Text = ": " & Math.Round(grid.championStats.abilityPower, 0, MidpointRounding.AwayFromZero)
            msc2.Text = ": " & Math.Round(grid.championStats.attackSpeed, 2, MidpointRounding.AwayFromZero)
            msc3.Text = ": " & grid.championStats.attackRange
            msc4.Text = ": " & Math.Round(grid.championStats.armor, 0, MidpointRounding.AwayFromZero)
            msc5.Text = ": " & Math.Round(grid.championStats.magicResist, 0, MidpointRounding.AwayFromZero)
            msc6.Text = ": " & Math.Round(grid.championStats.currentHealth, 0, MidpointRounding.AwayFromZero) & "/" & Math.Round(grid.championStats.maxHealth, 0, MidpointRounding.AwayFromZero) & "/" &
               Math.Round(grid.championStats.healthRegenRate, 1, MidpointRounding.AwayFromZero)
            msc7.Text = ": " & Math.Round(grid.championStats.resourceValue, 0, MidpointRounding.AwayFromZero) & "/" & Math.Round(grid.championStats.resourceMax, 0, MidpointRounding.AwayFromZero) & "/" &
               Math.Round(grid.championStats.resourceRegenRate, 1, MidpointRounding.AwayFromZero)
            msc8.Text = ": " & Math.Round(grid.championStats.moveSpeed, 0, MidpointRounding.AwayFromZero)
            msc9.Text = ": " & Math.Round(grid.currentGold, 0, MidpointRounding.AwayFromZero)
            'msc0.Text = "AR/PenFlat/%              : " & Math.Round(grid.championStats.armor, 1, MidpointRounding.AwayFromZero) & "/" &
            '    grid.championStats.armorPenetrationFlat & "/" & Math.Round(grid.championStats.armorPenetrationPercent, 1, MidpointRounding.AwayFromZero) & "%"
            'msc1.Text = "AD/AP/Range/Speed : " & Math.Round(grid.championStats.attackDamage, 1, MidpointRounding.AwayFromZero) & "/" &
            '    Math.Round(grid.championStats.abilityPower, 1, MidpointRounding.AwayFromZero) & "/" &
            '    grid.championStats.attackRange & "/" & Math.Round(grid.championStats.attackSpeed, 1, MidpointRounding.AwayFromZero)
            'msc2.Text = "Bounus-APen%/MPen% : " & grid.championStats.bonusArmorPenetrationPercent & "%/" & grid.championStats.bonusMagicPenetrationPercent & "%"
            'msc3.Text = "Crit-Chance/Damage : " & grid.championStats.critChance & "/" & grid.championStats.critDamage
            'msc4.Text = "HP/Max/Reg : " & Math.Round(grid.championStats.currentHealth, 1, MidpointRounding.AwayFromZero) & "/" & Math.Round(grid.championStats.maxHealth, 1, MidpointRounding.AwayFromZero) & "/" &
            '    Math.Round(grid.championStats.healthRegenRate, 1, MidpointRounding.AwayFromZero)
            'msc5.Text = "MR/PenFlat/% : " & Math.Round(grid.championStats.magicResist, 1, MidpointRounding.AwayFromZero) & "/" & grid.championStats.magicPenetrationFlat & "/" &
            '    grid.championStats.magicPenetrationPercent & "%"
            'msc6.Text = "Lethality-Physical/Magic:  " & grid.championStats.physicalLethality & "/" & grid.championStats.magicLethality
            'msc7.Text = grid.championStats.resourceType & "/Max/Reg : " & Math.Round(grid.championStats.resourceValue, 1, MidpointRounding.AwayFromZero) & "/" & Math.Round(grid.championStats.resourceMax, 1, MidpointRounding.AwayFromZero) & "/" &
            '    Math.Round(grid.championStats.resourceRegenRate, 1, MidpointRounding.AwayFromZero)
            'msc8.Text = "MS/SpellVamp/Tenacity : " & Math.Round(grid.championStats.moveSpeed, 1, MidpointRounding.AwayFromZero) & "/" & grid.championStats.spellVamp & "/" & grid.championStats.tenacity
            'msc9.Text = "Gold : " & Math.Round(grid.currentGold, 0, MidpointRounding.AwayFromZero)


        End If
        ''https://127.0.0.1:52174/lol-match-history/v2/matchlist?begIndex=0&endIndex=10
    End Sub

    Private Sub Timer10_Tick(sender As Object, e As EventArgs) Handles Timer10.Tick
        Timer10.Enabled = False
        'If Tabb = 96 Then
        Dim cmd As String
        '再生しているWAVEを停止する
        cmd = "stop " + aliasName
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        '  閉じる
        cmd = "close " + aliasName
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        'End If
        'brdy = 1
    End Sub



    Private Sub cham()
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
            Dim password As String = token
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
            oldid = 0
        Else
            Dim grid = response.DynamicBody
            'Dim jsonObj As String = JsonConvert.SerializeObject(grid)

            'TextBox1.Text = jsonObj

            'Exit Sub

            'Dim kill(9) As Integer
            'Dim death(9) As Integer
            'Dim ass(9) As Integer
            Dim i As Integer = 0
            'Dim j As Integer = 0
            'Dim k As Integer = 0

            For Each item In grid
                Application.DoEvents()
                Dim cname As String = grid(i).rawChampionName.ToString
                Dim len As Integer = cname.Length
                Dim last_n As Integer = cname.LastIndexOf("_") + 1
                Dim nn As Integer = len - last_n
                Dim chn As String = cname.Substring(last_n, nn)
                'TextBox1.AppendText(chn & vbCrLf)
                'Me.Controls("ch" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & vernew & "\champimage\" & chn & ".png"), 24, 24)
                If chn = chmname Then
                    nono = i
                End If
                'Dim ic As Integer = grid(i).items.count
                'For Each items In grid(i).items
                '    'TextBox1.AppendText((grid(i).items(j).itemID) & vbCrLf)

                '    Me.Pa7.Controls("Panel" & i & j).BackgroundImage = New Bitmap(Image.FromFile("item\" & grid(i).items(k).itemID & ".png"), 24, 24)
                '    j += 1
                '    k += 1
                'Next
                'For jj As Integer = j To 6
                '    Me.Pa7.Controls("Panel" & i & jj).BackgroundImage = Nothing
                'Next
                'j = 0
                i += 1
            Next

            'If grid(i).summonerName = "夜来香" Then
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
            'i += 1
            '    Next
            'LVdata.Rows.RemoveAt(0)
            'ConvertDataTableToCsv(LVdata, "lvl.csv", True, False)
            'DataGridView1.DataSource = hisdata
            'TextBox11.Text = jsonObj
        End If
        ''https://127.0.0.1:52174/lol-match-history/v2/matchlist?begIndex=0&endIndex=10
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        clea()
    End Sub
    Public Sub clea()
        For i As Integer = 0 To 9
            Me.Controls("sc" & i).Text = "0/0/0"
            Me.Controls("msc" & i).Text = "No Response"
            Me.Controls("lvl" & i).Text = "0"
            Me.Controls("ch" & i).BackgroundImage = New Bitmap(Image.FromFile("images\" & vernew & "\champimage\Teemo.png"), 24, 24)
        Next
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        cham()
        Timer4.Enabled = False
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Event0()
    End Sub

    Private Sub Event0()
        Dim response As HttpResponse = Nothing
        Try
            'Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/eventdata")
            '  response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/all-grid-champions")
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response gamestats" & vbCrLf)
            Exit Sub
            'Leagueconnect()
            'Dim password As String = token
            'http.Request.Accept = HttpContentTypes.ApplicationJson
            'http.Request.SetBasicAuthentication("riot", password)
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then

        Else
            Dim grid = response.DynamicBody
            Dim jsonObj As String = JsonConvert.SerializeObject(grid)
            TextBox11.Text = jsonObj

        End If
    End Sub

    'Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
    '    Dim hwnd(20) As Integer
    '    hwnd(0) = FindWindow("WindowsForms10.Window.8.app.0.141b42a_r7_ad1", " lolj")
    '    hwnd(1) = FindWindowEx(hwnd(0), 0, "WindowsForms10.BUTTON.app.0.141b42a_r7_ad1", "Clear")
    '    'hwnd(2) = FindWindowEx(hwnd(1), 0, "WindowsForms10.BUTTON.app.0.141b42a_r7_ad1", "Clear")
    '    AppActivate("lolj")
    '    'hwnd(2) = FindWindowEx(hwnd(1), 0, "ThunderRT6Frame", "")
    '    'hwnd(3) = FindWindowEx(hwnd(2), 0, "ThunderRT6Frame", "CPU検出時系列ﾃﾞｰﾀ取得")
    '    'hwnd(4) = FindWindowEx(hwnd(3), 0, "ThunderRT6CommandButton", "ﾃﾞｰﾀ要求")
    '    'hwnd(5) = FindWindowEx(hwnd(2), 0, "ThunderRT6Frame", "表示ﾃﾞｰﾀ取得")
    '    'hwnd(6) = FindWindowEx(hwnd(5), 0, "ThunderRT6CommandButton", "ﾃﾞｰﾀ保存")
    '    'hwnd(7) = FindWindowEx(hwnd(5), 0, "ThunderRT6CommandButton", "ﾃﾞｰﾀｸﾘｱ")
    '    SendMessage(hwnd(1), WM_LBUTTONDOWN, 0, 0)
    '    SendMessage(hwnd(1), WM_LBUTTONUP, 0, 0)
    '    Form2.Button1.PerformClick()
    '    Button12.PerformClick()
    '    Timer3.Enabled = False
    '    Form3.RichTextBox1.Text = ""
    'End Sub

    Dim begin_f As Boolean = False
    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        If Process.GetProcessesByName("League of Legends").Length <> 0 Then

            begin_f = True
            Timer2.Enabled = True
            Timer1.Enabled = False
            Timer6.Enabled = True
            check_game()
            TextBox1.AppendText("Game Found" & vbCrLf)

        Else
            'TextBox1.AppendText("Game Not Found : " & rm & vbCrLf)
        End If

    End Sub
    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        If Process.GetProcessesByName("League of Legends").Length <> 0 Then
            If begin_f = True Then

                'Form2.onece()
                'Form2.Timer1.Enabled = True
                'Form2.Label1.Text = "on"

                begin_f = False
                'TextBox1.AppendText("Form3 Start" & vbCrLf)
            End If
        Else
            Timer1.Enabled = True
            Timer2.Enabled = False
            Timer6.Enabled = False
            If Form3.Label1.Text <> "off" Then
                Form3.Label1.Text = "off"
                Form3.yy = 0
                Form3.yy_bin = 0
                TextBox1.AppendText("Game Lost" & vbCrLf)
                Form3.Timer1.Enabled = False
            End If

        End If

    End Sub

    Private Sub check_game()
        Dim response As HttpResponse = Nothing
        Try
            'Leagueconnect()
            Dim password As String = token
            http.Request.Accept = HttpContentTypes.ApplicationJson
            http.Request.SetBasicAuthentication("riot", password)
            response = http.[Get]("https://127.0.0.1:2999/liveclientdata/gamestats")
            '  response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/all-grid-champions")
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        Catch Exception As Exception
            TextBox1.AppendText("Error : No Response gamestats" & vbCrLf)
            Exit Sub
            'Leagueconnect()
            'Dim password As String = token
            'http.Request.Accept = HttpContentTypes.ApplicationJson
            'http.Request.SetBasicAuthentication("riot", password)
            'response = http.[Get]("https://127.0.0.1:" & port & "/lol-champ-select/v1/current-champion") ', inputLCUx, HttpContentTypes.ApplicationJson)

        End Try
        If response.StatusCode <> System.Net.HttpStatusCode.OK Then

        Else
            Dim grid = response.DynamicBody
            msc10.Text = "Timer  : " & Math.Round(grid.gameTime, 0, MidpointRounding.AwayFromZero)
            If grid.gameTime > 0 Then
                'Timer4.Enabled = True
                Timer1.Enabled = False
                Form2.onece()
                Timer2.Enabled = True
                Timer5.Enabled = False
                msc10.Text = Nothing
                Form3.nns = 0
                Form3.RichTextBox1.Clear()
                Form3.nn = 0
                Form3.yy = 0
                Form3.yy_bin = 0
                Form3.Timer1.Enabled = True
                Form3.Label1.Text = "on"
                TextBox1.AppendText("Form3 Start" & vbCrLf)
            End If
        End If

    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Form2.Close()
        Form3.Close()
        Form4.Close()
        Form5.Close()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        'Form2.Show()
        history()
        'Form4.Show()
        'Form5.Show()
    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles Timer7.Tick
        Timer7.Enabled = False
        history()

    End Sub
    'Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
    '    Skinb4()
    'End Sub
End Class

Public Class NonDispBrowser
    Inherits WebBrowser

    Dim done As Boolean

    ' タイムアウト時間（10秒）
    Dim timeout As New TimeSpan(0, 0, 10)

    'Protected Overrides Sub OnDocumentCompleted(
    '            ByVal e As WebBrowserDocumentCompletedEventArgs)
    '    ' ページにフレームが含まれる場合にはフレームごとに
    '    ' このメソッドが実行されるため実際のURLを確認する
    '    If e.Url = Me.Url Then
    '        done = True
    '    End If
    'End Sub

    Protected Overrides Sub OnNewWindow(ByVal e As CancelEventArgs)
        ' ポップアップ・ウィンドウをキャンセル
        e.Cancel = True
    End Sub

    Public Sub New()
        ' スクリプト・エラーを表示しない
        Me.ScriptErrorsSuppressed = True
    End Sub




    Public Function NavigateAndWait(ByVal url As String) As Boolean

        MyBase.Navigate(url) ' ページの移動

        done = False
        Dim start As DateTime = DateTime.Now

        While done = False
            If DateTime.Now - start > timeout Then
                ' タイムアウト

                Return False
            End If
            Application.DoEvents()
        End While

        Return True
    End Function



End Class
'要求するURL




