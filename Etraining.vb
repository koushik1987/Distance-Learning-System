Imports System.Data.OleDb
Public Class Etraining

    Private Sub Etraining_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from training where eno='" & guid & "'", con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            MsgBox(guid)
            If dr.Read() Then
                TextBox8.Text = guid
                TextBox7.Text = dr.GetValue(1)
                TextBox6.Text = dr.GetValue(2)
                TextBox1.Text = dr.GetValue(3)               
            End If
            dr.close()

            Dim cmd2 As New OleDb.OleDbCommand("select * from emp_regn where eno='" & guid & "'", con2)
            Dim dr2 As OleDb.OleDbDataReader = cmd2.ExecuteReader(CommandBehavior.SingleRow)

            If dr2.Read() Then
                TextBox2.Text = dr2.GetValue(1)
                TextBox3.Text = dr2.GetValue(2)
                TextBox4.Text = dr2.GetValue(3)
                TextBox5.Text = dr2.GetValue(4)
            End If

            dr2.Close()

            Dim cmd3 As New OleDb.OleDbCommand("select * from course where ccode=" & TextBox7.Text, con2)
            Dim dr3 As OleDb.OleDbDataReader = cmd3.ExecuteReader(CommandBehavior.SingleRow)

            If dr3.Read() Then
                txtCname.Text = dr3.GetValue(1)
                level.Text = dr3.GetValue(3)
                
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con2.Close()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub
End Class