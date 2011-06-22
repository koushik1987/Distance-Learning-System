Imports System.Data.OleDb
Public Class Form1

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim uid, pwd As String
        uid = TextBox1.Text
        pwd = TextBox2.Text

        ErrorProvider1.SetError(TextBox1, "")
        ErrorProvider1.SetError(TextBox2, "")
        If uid.Trim() <> "" Then
            If pwd.Trim() <> "" Then
                Dim con As New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=sa.mdb")
                Try
                    con.Open()
                    Dim k As Integer
                    Dim npwd As String = ""
                    If RadioButton1.Checked = True Then
                        For k = 1 To pwd.Length
                            npwd = npwd + Chr(Asc(Mid(pwd, k, 1)) + 128)
                        Next
                    Else
                        npwd = pwd
                    End If
                    Dim cmd As OleDbCommand
                    If RadioButton1.Checked = True Then
                        cmd = New OleDbCommand("select * from users where uid='" & uid & "' and pwd='" & npwd & "'", con)
                    Else
                        cmd = New OleDbCommand("select * from emp_regn where eno='" & uid & "' and pwd='" & npwd & "'", con)
                    End If
                    Dim dr As OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                    If dr.Read() Then
                        guid = uid
                        Me.Hide()
                        If RadioButton1.Checked = True Then

                            Dim f As New Form2()
                            f.ShowDialog()
                        Else
                            Dim f As New empoptions()
                            f.ShowDialog()
                        End If
                    Else
                        MessageBox.Show("Login Denied!")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    con.Close()
                End Try
            Else
                ErrorProvider1.SetError(TextBox2, "Invalid Password! Try Again.")
            End If
        Else
            ErrorProvider1.SetError(TextBox1, "Invalid User-ID! Try Again.")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim uid, pwd As String
        uid = TextBox1.Text
        pwd = TextBox2.Text

        ErrorProvider1.SetError(TextBox1, "")
        ErrorProvider1.SetError(TextBox2, "")
        If uid.Trim() <> "" Then
            If pwd.Trim() <> "" Then
                Dim con As New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=sa.mdb")
                Try
                    con.Open()
                    Dim k As Integer
                    Dim npwd As String = ""
                    If RadioButton1.Checked = True Then
                        For k = 1 To pwd.Length
                            npwd = npwd + Chr(Asc(Mid(pwd, k, 1)) + 128)
                        Next
                    Else
                      
                        npwd = pwd
                    End If

                    Dim cmd As OleDbCommand
                    If RadioButton1.Checked = True Then
                        cmd = New OleDbCommand("select * from users where uid='" & uid & "' and pwd='" & npwd & "'", con)
                    Else
                        cmd = New OleDbCommand("select * from emp_regn where eno='" & uid & "' and pwd='" & npwd & "'", con)
                    End If

                    Dim dr As OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                    If dr.Read() Then
                        guid = uid
                        TextBox2.Text = ""
                        Dim f As New cpwd()
                        f.ShowDialog()
                    Else
                        MessageBox.Show("Login Denied!")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    con.Close()
                End Try
            Else
                ErrorProvider1.SetError(TextBox2, "Invalid Password! Try Again.")
            End If
        Else
            ErrorProvider1.SetError(TextBox1, "Invalid User-ID! Try Again.")
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Hide()

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox1.Text = "Admin"
        Else
            TextBox1.Text = ""

        End If
    End Sub
End Class
