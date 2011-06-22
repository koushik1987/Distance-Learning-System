Imports System.Data.OleDb

Public Class cpwd

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=sa.mdb")
        Dim p1, p2 As String
        p1 = TextBox1.Text.Trim()
        p2 = TextBox2.Text.Trim()
        Try
            If p1 = p2 Then
                con.Open()
                Dim k As Integer
                Dim npwd As String = ""

                Dim cmd As OleDbCommand
                If guid = "Admin" Then
                    For k = 1 To p1.Length
                        npwd = npwd + Chr(Asc(Mid(p1, k, 1)) + 128)
                    Next
                    cmd = New OleDbCommand("update users set pwd='" + npwd + "' where uid='" & guid & "'", con)
                Else
                    cmd = New OleDbCommand("update emp_regn set pwd='" + p2 + "' where eno='" & guid & "'", con)
                End If

                Dim stat As Integer
                stat = cmd.ExecuteNonQuery()
                If stat > 0 Then
                    MessageBox.Show("Password Successfully Updated!")
                    Me.Close()
                Else
                    MessageBox.Show("Password Updation Failed!")
                End If
            Else
                MessageBox.Show("Password Mismatch!")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class