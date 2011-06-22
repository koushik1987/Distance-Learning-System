Imports System.Data.OleDb
Public Class Course_View

    Private Sub Ok_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ok_Button.Click
        Me.Close()
    End Sub

    Private Sub Course_View_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmd = New OleDb.OleDbCommand("Select* from course", conn)
        da = New OleDb.OleDbDataAdapter(cmd)
        ds = New DataSet
        da.Fill(ds)
        DataGrid1.DataSource = ds
    End Sub
End Class