Public Class Form1
    Private AllItem As New List(Of String)

    Private Sub DoUpdate()
        Me.ListView1.Clear()
        Dim prefix As String = Me.TextBox1.Text.ToLower()
        For Each p As String In AllItem
            If p.ToLower().StartsWith(prefix) Then
                Me.ListView1.Items.Add(p)
            End If
        Next
        If Me.ListView1.Items.Count > 0 Then
            Me.ListView1.Items(0).Selected = True
            Me.ListView1.Select()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim require_update = False
        While Console.In.Peek() >= 0
            Dim line1 As String = Console.In.ReadLine()
            Me.AllItem.Add(line1)
            require_update = True
        End While
        If require_update Then
            DoUpdate()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        DoUpdate()
    End Sub

    Private Sub Done()
        For Each p As ListViewItem In ListView1.SelectedItems
            Console.WriteLine(p.Text)
        Next
        Me.Close()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Done()
    End Sub

    Private Sub ListView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView1.KeyPress
        Dim index1 = If(Me.ListView1.SelectedItems.Count > 0, Me.ListView1.SelectedItems(0).Index, -1)
        Select Case e.KeyChar
            Case vbCr, vbLf
                Done()
            Case "n"c, "j"c, Chr(Asc("n"c) And &H1F)
                Me.ListView1.Items((index1 + 1) Mod Me.ListView1.Items.Count).Selected = True
            Case "p"c, "k"c, Chr(Asc("p"c) And &H1F)
                Me.ListView1.Items((index1 + Me.ListView1.Items.Count - 1) Mod Me.ListView1.Items.Count).Selected = True
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Console.InputEncoding = System.Text.Encoding.UTF8
    End Sub
End Class