Imports System.Data.oledb
Public Class vresults

    Private Sub vresults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'SADataSet.result' table. You can move, or remove it, as needed.
        Try
            conn.Open()
            cmd = New OleDbCommand("select * from result ", conn)
            Dim da As New OleDbDataAdapter(cmd)
            Dim ds As New DataSet

            da.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class