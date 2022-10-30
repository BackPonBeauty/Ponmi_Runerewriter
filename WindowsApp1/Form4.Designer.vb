<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chname = New System.Windows.Forms.TextBox()
        Me.txtSend = New System.Windows.Forms.TextBox()
        Me.rtbOutput = New System.Windows.Forms.RichTextBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.madd = New System.Windows.Forms.Button()
        Me.lstUsers = New System.Windows.Forms.ListBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.Labelsan = New System.Windows.Forms.Label()
        Me.Labelni = New System.Windows.Forms.Label()
        Me.Labelichi = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'chname
        '
        Me.chname.ForeColor = System.Drawing.Color.Black
        Me.chname.Location = New System.Drawing.Point(93, 9)
        Me.chname.Name = "chname"
        Me.chname.Size = New System.Drawing.Size(108, 24)
        Me.chname.TabIndex = 89
        Me.chname.Text = "nightlovender"
        '
        'txtSend
        '
        Me.txtSend.ForeColor = System.Drawing.Color.Black
        Me.txtSend.Location = New System.Drawing.Point(12, 204)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(598, 24)
        Me.txtSend.TabIndex = 85
        '
        'rtbOutput
        '
        Me.rtbOutput.BackColor = System.Drawing.SystemColors.InfoText
        Me.rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbOutput.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rtbOutput.ForeColor = System.Drawing.Color.White
        Me.rtbOutput.Location = New System.Drawing.Point(13, 39)
        Me.rtbOutput.Name = "rtbOutput"
        Me.rtbOutput.Size = New System.Drawing.Size(597, 159)
        Me.rtbOutput.TabIndex = 84
        Me.rtbOutput.Text = "チャットスペース　ABCDEF"
        '
        'btnConnect
        '
        Me.btnConnect.ForeColor = System.Drawing.Color.Black
        Me.btnConnect.Location = New System.Drawing.Point(12, 8)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 25)
        Me.btnConnect.TabIndex = 83
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'madd
        '
        Me.madd.ForeColor = System.Drawing.Color.Black
        Me.madd.Location = New System.Drawing.Point(521, 236)
        Me.madd.Name = "madd"
        Me.madd.Size = New System.Drawing.Size(215, 26)
        Me.madd.TabIndex = 92
        Me.madd.Text = "reset"
        Me.madd.UseVisualStyleBackColor = True
        '
        'lstUsers
        '
        Me.lstUsers.ForeColor = System.Drawing.Color.Black
        Me.lstUsers.FormattingEnabled = True
        Me.lstUsers.ItemHeight = 17
        Me.lstUsers.Location = New System.Drawing.Point(616, 39)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.Size = New System.Drawing.Size(120, 157)
        Me.lstUsers.TabIndex = 87
        '
        'btnSend
        '
        Me.btnSend.Enabled = False
        Me.btnSend.ForeColor = System.Drawing.Color.Black
        Me.btnSend.Location = New System.Drawing.Point(616, 204)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(120, 26)
        Me.btnSend.TabIndex = 86
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'Labelsan
        '
        Me.Labelsan.AutoSize = True
        Me.Labelsan.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Labelsan.ForeColor = System.Drawing.Color.Black
        Me.Labelsan.Location = New System.Drawing.Point(306, 108)
        Me.Labelsan.Name = "Labelsan"
        Me.Labelsan.Size = New System.Drawing.Size(48, 17)
        Me.Labelsan.TabIndex = 82
        Me.Labelsan.Text = "aaaaa"
        '
        'Labelni
        '
        Me.Labelni.AutoSize = True
        Me.Labelni.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Labelni.ForeColor = System.Drawing.Color.Black
        Me.Labelni.Location = New System.Drawing.Point(306, 90)
        Me.Labelni.Name = "Labelni"
        Me.Labelni.Size = New System.Drawing.Size(48, 17)
        Me.Labelni.TabIndex = 81
        Me.Labelni.Text = "aaaaa"
        '
        'Labelichi
        '
        Me.Labelichi.AutoSize = True
        Me.Labelichi.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Labelichi.ForeColor = System.Drawing.Color.Black
        Me.Labelichi.Location = New System.Drawing.Point(306, 69)
        Me.Labelichi.Name = "Labelichi"
        Me.Labelichi.Size = New System.Drawing.Size(48, 17)
        Me.Labelichi.TabIndex = 80
        Me.Labelichi.Text = "aaaaa"
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(841, 420)
        Me.Controls.Add(Me.chname)
        Me.Controls.Add(Me.txtSend)
        Me.Controls.Add(Me.rtbOutput)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.madd)
        Me.Controls.Add(Me.lstUsers)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.Labelsan)
        Me.Controls.Add(Me.Labelni)
        Me.Controls.Add(Me.Labelichi)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.WindowsApp1.My.MySettings.Default, "mirc", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.Location = Global.WindowsApp1.My.MySettings.Default.mirc
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form4"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Form4"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chname As TextBox
    Private WithEvents txtSend As TextBox
    Private WithEvents rtbOutput As RichTextBox
    Private WithEvents btnConnect As Button
    Friend WithEvents madd As Button
    Private WithEvents lstUsers As ListBox
    Private WithEvents btnSend As Button
    Friend WithEvents Labelsan As Label
    Friend WithEvents Labelni As Label
    Friend WithEvents Labelichi As Label
End Class
