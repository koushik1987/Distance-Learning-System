Public Class training

    Private Sub ComboBox1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.Enter
        ComboBox1.Items.Clear()
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select eno from emp_regn", con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)

            While dr.Read()
                ComboBox1.Items.Add(dr.GetValue(0))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from emp_regn where eno='" & ComboBox1.Text & "'", con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)

            If dr.Read() Then
                TextBox2.Text = dr.GetValue(1)
                textBox3.Text = dr.GetValue(2)
                TextBox4.Text = dr.GetValue(3)
                TextBox5.Text = dr.GetValue(4)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub ComboBox2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.Enter
        ComboBox2.Items.Clear()
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select ccode from course", con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)

            While dr.Read()
                ComboBox2.Items.Add(dr.GetValue(0))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from course where ccode=" & ComboBox2.Text, con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)

            If dr.Read() Then
                txtCname.Text = dr.GetValue(1)
                level.Text = dr.GetValue(3)
                TextBox6.Text = Date.Today
                TextBox1.Text = Date.Today.AddDays(CInt(level.Text) * 15)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text <> "" And ComboBox2.Text <> "" Then
            Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
            Try

                Dim cmd As New OleDb.OleDbCommand()
                con2.Open()

                cmd = New OleDb.OleDbCommand("insert into training values('" & ComboBox1.Text & "'," & ComboBox2.Text & ",'" & TextBox6.Text & "','" & TextBox1.Text & "','Training')", con2)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Employee Assigned Training!")

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                con2.Close()
            End Try
        Else
            MsgBox("Select a employee and course.")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub training_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class