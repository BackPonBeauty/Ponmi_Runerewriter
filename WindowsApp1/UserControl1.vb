Public Class UserControl1
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        DrawMode = DrawMode.OwnerDrawFixed
    End Sub
    Private ListaImg1 As New ImageList

    Public Property ImageList() As ImageList
        Get
            Return ListaImg1
        End Get
        Set(ByVal ListaImagem As ImageList)
            ListaImg1 = ListaImagem
        End Set
    End Property

    Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
        e.DrawBackground()
        e.DrawFocusRectangle()
        Dim bounds As New Rectangle
        bounds = e.Bounds
        Dim s As String = Me.Items(e.Index)
        Try
            If (ListaImg1.Images.Count <> 0) Then
                ListaImg1.Draw(e.Graphics, bounds.Left, bounds.Top, e.Index)
                e.Graphics.DrawString(s, e.Font, New SolidBrush(e.ForeColor), bounds.Left + ListaImg1.Images(e.Index).Width, bounds.Top)
                'ListaImg1.Draw(e.Graphics, bounds.Left, bounds.Top, e.Index)
                'e.Graphics.DrawString(s, e.Font, New SolidBrush(e.ForeColor), bounds.Left + ListaImg1.Images(e.Index).Width, bounds.Top)
            Else
                e.Graphics.DrawString(s, e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
            End If
        Catch ex As Exception
            e.Graphics.DrawString(s, e.Font, New SolidBrush(e.ForeColor), bounds.Left, bounds.Top)
        End Try
        MyBase.OnDrawItem(e)
    End Sub
End Class
