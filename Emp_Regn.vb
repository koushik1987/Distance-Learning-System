Public Class Emp_Regn

    Dim ch1 As Integer
    Private Sub Form22_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        showdata()
        generate_no()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox2.Text <> "" And TextBox6.Text <> "" And TextBox4.Text <> "" And TextBox5.Text <> "" And IsDate(TextBox4.Text) Then
            Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
            Dim tsno As String
            Dim tpd, tsname, tqty, trpu As String

            Try
                tsno = TextBox1.Text
                tsname = TextBox2.Text
                tpd = textBox3.Text
                tqty = TextBox4.Text
                trpu = TextBox5.Text
                Dim cmd As New OleDb.OleDbCommand()
                con2.Open()
                If RadioButton1.Checked = True Then
                    cmd = New OleDb.OleDbCommand("insert into emp_regn values('" & tsno & "','" & tsname & "','" & tpd & "','" & tqty & "','" & trpu & "','" & TextBox6.Text & "')", con2)
                Else
                    If RadioButton2.Checked = True Then
                        cmd = New OleDb.OleDbCommand("update emp_regn set eno='" & TextBox1.Text & "',ename='" & tsname & "',dept='" & tpd & "',jdate='" & tqty & "',email='" & trpu & "',pwd='" & TextBox6.Text & "' where eno='" & ComboBox1.Text & "'", con2)
                    Else
                        cmd = New OleDb.OleDbCommand("delete from emp_regn where eno='" & TextBox1.Text & "'", con2)
                    End If
                End If
                cmd.ExecuteNonQuery()
                MessageBox.Show("Success")
                showdata()
                generate_no()
                TextBox6.Text = ""
                TextBox2.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                con2.Close()
            End Try
        Else
            MsgBox("Incomplete Form or Invalid Date!")
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            Button1.Text = "Save"
            ch1 = 0
            'TextBox1.Enabled = True
            generate_no()
            ComboBox1.Enabled = False
            Button2.Enabled = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            ch1 = 1
            Button1.Text = "Update"
            TextBox1.Enabled = False
            ComboBox1.Enabled = True
            Button2.Enabled = True
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            ch1 = 1
            Button1.Text = "Delete"
            TextBox1.Enabled = False
            ComboBox1.Enabled = True
            Button2.Enabled = True
        End If
    End Sub

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
                TextBox1.Text = dr.GetValue(0)
                TextBox2.Text = dr.GetValue(1)
                textBox3.Text = dr.GetValue(2)
                TextBox4.Text = dr.GetValue(3)
                TextBox5.Text = dr.GetValue(4)
                TextBox6.Text = dr.GetValue(5)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Public Sub showdata()
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from emp_regn", con2)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from emp_regn where ename like('" & TextBox2.Text & "%')", con2)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from emp_regn where sname like('" & TextBox2.Text & "%')", con2)
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)

        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellChanged
        'Dim cr As Integer
        'Try
        '    cr = DataGridView1.CurrentRow.Index
        '    TextBox1.Text = DataGridView1.Item(0, cr).Value
        '    TextBox2.Text = DataGridView1.Item(1, cr).Value
        '    textBox3.Text = DataGridView1.Item(2, cr).Value
        '    TextBox4.Text = DataGridView1.Item(3, cr).Value
        '    TextBox5.Text = DataGridView1.Item(4, cr).Value
        'Catch ex As Exception

        'End Try

    End Sub

    Public Sub generate_no()
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Dim nn As Integer
        Try

            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from emp_regn", con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
            Dim lv As String = ""
            Dim slv() As String
            While dr.Read()

                lv = dr.GetValue(0)
                slv = Split(lv, "-")
                nn = slv(1)
            End While
            If nn = 0 Then nn = 1000 Else nn = nn + 1
        Catch ex As Exception
            nn = 1000
        Finally
            con2.Close()
        End Try
        ' MsgBox(nn)
        TextBox1.Text = Mid(textBox3.Text, 1, 1) & "-" & nn
        TextBox4.Text = Date.Today
    End Sub

    Private Sub textBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBox3.SelectedIndexChanged
        If ch1 = 0 Then
            generate_no()
        Else
            TextBox1.Text = Mid(textBox3.Text, 1, 1) & Mid(TextBox1.Text, 2)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class