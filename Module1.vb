Module Module1
    Public conn As New OleDb.OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=sa.mdb")
    Public cmd As OleDb.OleDbCommand
    Public dr As OleDb.OleDbDataReader
    Public da As OleDb.OleDbDataAdapter
    Public ds As DataSet
    Public guid As String
End Module
