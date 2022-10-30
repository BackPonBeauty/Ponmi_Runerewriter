Imports System.Text
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports System.IO
Imports System.Runtime.Serialization.Json
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Xml
Imports TechLifeForum
Imports System
Imports System.Runtime.CompilerServices
Imports System.Linq
Imports System.Collections.Generic
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports MySql.Data.MySqlClient
Imports NMeCab
Imports Microsoft.DirectX.DirectSound
Imports System.Threading
Public Class Form4
    Dim WithEvents irc As IrcClient

    Private Sub btnConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnConnect.Click
        'irc = New IrcClient("irc.friend-chat.jp", 6664)
        'irc.Nick = "ichimamire2"
        ''    irc.ServerPass = "oauth:dmxjj1gtb13bov20i7fjgsexasbqa2"
        'irc.Connect()

        irc = New IrcClient("irc.twitch.tv", 6667)
        irc.Nick = "botnightlovender"
        irc.ServerPass = "oauth:27jqotfx77iwu5xyi0umags6g6zhwn"
        irc.Connect()

    End Sub


    Private Sub btnSend_Click(sender As System.Object, e As System.EventArgs) Handles btnSend.Click
        Dim time0 As DateTime = DateTime.Now.ToString("hh:MM:ss")
        irc.SendMessage("#nightlovender", txtSend.Text)
        Form3.RichTextBox1.SelectionColor = Color.White
        Form3.RichTextBox1.AppendText(vbCrLf & time0 & " : " & txtSend.Text & vbNewLine)
        Form3.RichTextBox1.SelectionColor = Color.White
        rtbOutput.AppendText(txtSend.Text & vbNewLine) '"You" & vbTab & ":" & 
        rtbOutput.ScrollToCaret()
        txtSend.Clear()
        txtSend.Focus()
    End Sub

    Private Sub txtSend_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSend.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSend.PerformClick()
        End If
    End Sub

    Private Sub irc_ChannelMessage(Channel As String, User As String, Message As String) Handles irc.ChannelMessage
        Dim time0 As DateTime = DateTime.Now.ToString("hh:MM:ss")
        Form3.RichTextBox1.SelectionColor = Color.White
        Form3.RichTextBox1.AppendText(vbCrLf & time0 & " : " & User & ": " & Message & vbNewLine)
        Form3.RichTextBox1.SelectionColor = Color.White
        rtbOutput.AppendText(time0 & " : " & User & ": " & Message & vbNewLine) 'User & vbTab & ":" &
        rtbOutput.ScrollToCaret()
        '   btext = Message
        'Call bouyomi(Message)

        'Call marcov(Message)
        'Dim heard As String = Message

        'Dim len1 As Integer = CountChar(heard, "「")
        'Dim len2 As Integer = CountChar(heard, "」")
        'If len1 + len2 = 4 Then
        '    Dim s1 As Integer = heard.IndexOf("「")
        '    Dim e1 As Integer = heard.IndexOf("」")
        '    Dim s2 As Integer = heard.LastIndexOf("「")
        '    Dim e2 As Integer = heard.LastIndexOf("」")
        '    Dim l1 As Integer = e1 - s1
        '    Dim l2 As Integer = e2 - s2
        '    Dim wa1 As String = heard.Substring(s1 + 1, l1 - 1)
        '    Dim wa2 As String = heard.Substring(s2 + 1, l2 - 1)
        '    Dim sw As String
        '    Dim rowsl As Integer = cdi.Rows.Count
        '    For i As Integer = 0 To rowsl - 1
        '        sw = cdi.Rows(i).Item("w1")
        '        If wa1 = sw Then
        '            cdi.Rows(i)("w2") = wa2
        '            TextBox2.Text = wa1 & "は、" & wa2 & "と覚えなおしました。"
        '            irc.SendMessage("#ichimamire", TextBox2.Text)
        '            Dim dataV As New DataView(cdi)
        '            dataV.Sort = "w1 DESC"
        '            cdi = dataV.ToTable

        '            DataGridView1.DataSource = cdi
        '            ConvertDataTableToCsvm(cdi, "Test1.csv", True)
        '            cdsi.Clear()
        '            Exit Sub
        '        End If

        '    Next
        '    TextBox2.Text = wa1 & "は、" & wa2 & "と覚えました。"
        '    irc.SendMessage("#ichimamire", TextBox2.Text)
        '    Call bouyomi(TextBox2.Text)
        '    cdi.Rows.Add(wa1, wa2)


        '    Dim dataView As New DataView(cdi)
        '    dataView.Sort = "w1 DESC"
        '    cdi = dataView.ToTable
        '    DataGridView1.DataSource = cdi
        '    ConvertDataTableToCsvm(cdi, "Test1.csv", True)
        '    Exit Sub
        'End If


        'Dim cRandom As New System.Random()
        'Dim iResult2 As Integer = cRandom.Next(100)
        'If System.Text.RegularExpressions.Regex.IsMatch(heard, "って？|ってなに？") Or iResult2 > 50 Then
        '    Dim sw As String
        '    Dim ii As Integer
        '    For i As Integer = 0 To rowslen
        '        sw = cdi.Rows(i).Item("w1")
        '        'TextBox2.Text = TextBox2.Text & sw + vbCrLf
        '        If 0 <= heard.IndexOf(sw) Then
        '            '   TextBox2.Text = "指定された文字列が含まれています" & vbCr
        '            ii = i
        '            Dim ward1 As String = cdi.Rows(ii).Item("w1")
        '            Dim ward2 As String = cdi.Rows(ii).Item("w2")
        '            TextBox2.Text = ward1 & "は" & ward2 & "です。"
        '            irc.SendMessage("#ichimamire", TextBox2.Text)
        '            Call bouyomi(TextBox2.Text)
        '            Exit For
        '        Else
        '            '  TextBox2.Text = "指定された文字列が含まれていません" & vbCr
        '        End If
        '    Next
        'End If




    End Sub

    Private Sub irc_ExceptionThrown(ex As System.Exception) Handles irc.ExceptionThrown
        MessageBox.Show(ex.Message)
    End Sub

    Private Sub irc_OnConnect() Handles irc.OnConnect
        rtbOutput.AppendText("Connected!" & vbNewLine)
        Dim cha As String = "#" & chname.Text
        irc.JoinChannel(cha)
        btnSend.Enabled = True
    End Sub

    Private Sub irc_ServerMessage(message As String) Handles irc.ServerMessage
        rtbOutput.AppendText(message & vbNewLine)
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub irc_UpdateUsers(Channel As String, userlist() As String) Handles irc.UpdateUsers
        lstUsers.Items.Clear()
        lstUsers.Items.AddRange(userlist)
    End Sub
End Class