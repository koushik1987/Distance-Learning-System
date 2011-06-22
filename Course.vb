Imports System.Data.oledb
Public Class Course
    Dim cc As Integer = 0

    Private Sub Course_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Max_Ccode()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Select Case OptionCombo.SelectedItem.ToString
                Case "ADD"
                    Button_Save.Text = "SAVE"
                    txtCcode.ReadOnly = False
                    T_Cls()
                    Max_Ccode()

                Case "MODIFY"
                    Button_Save.Text = "UPDATE"
                    txtCcode.ReadOnly = True
                    Try
                        cc = InputBox("Enter Course Code to Modify")
                    Catch ex As InvalidCastException
                        Exit Select
                    End Try

                    Try
                        cmd = New OleDbCommand("select * from course where ccode=" & cc, conn)
                        conn.Open()
                        dr = cmd.ExecuteReader(CommandBehavior.SingleRow)
                        If dr.Read() = True Then
                            txtCcode.Text = "" & dr(0)
                            txtCname.Text = "" & dr(1)
                            txtIyear.Text = "" & dr(2)
                        Else
                            MessageBox.Show("Invalid Course Code", "Course Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        conn.Close()
                    End Try

                Case "DELETE"
                    Button_Save.Text = "DELETE"
                    txtCcode.ReadOnly = True
                    Try
                        cc = InputBox("Enter Course Code to Delete")
                    Catch ex As InvalidCastException
                        Exit Select
                    End Try

                    Try
                        cmd = New OleDbCommand("select * from course where ccode=" & cc, conn)
                        conn.Open()
                        dr = cmd.ExecuteReader(CommandBehavior.SingleRow)
                        If dr.Read() = True Then
                            txtCcode.Text = "" & dr(0)
                            txtCname.Text = "" & dr(1)
                            txtIyear.Text = "" & dr(2)
                        Else
                            MessageBox.Show("Can not delete." & vbCrLf & "Invalid Course Code", "Course Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        conn.Close()
                    End Try

                Case "VIEW"
                    Course_View.ShowDialog()
            End Select
        Catch ee As Exception
            MsgBox("Select an option")
        End Try
    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save.Click
        Select Case OptionCombo.SelectedItem.ToString
            Case "ADD"
                Try
                    cmd = New OleDbCommand("insert into Course values(" & Val(txtCcode.Text) & ",'" & UCase(Trim(txtCname.Text)) & "'," & Val(txtIyear.Text) & "," & ComboBox1.Text & ")", conn)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("New Course is Introduced..", "New Course", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MsgBox("Invalid or Incomplete Data!")
                    Exit Sub
                Finally
                    conn.Close()
                End Try


                Try
                    conn.Open()
                    Dim kk As Integer
                    For kk = 1 To ComboBox1.Text
                        cmd = New OleDbCommand("create table C" & Trim(txtCcode.Text) & "_" & kk & "(qno number,quest char(250),apos number,ans nvarchar(250),prep char(30))", conn)
                        cmd.ExecuteNonQuery()
                    Next
                    MessageBox.Show("Exam Db Prepared")
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    conn.Close()
                End Try

                Max_Ccode()
                txtCname.Text = ""
                txtIyear.Text = ""


            Case "MODIFY"
                Try
                    cmd = New OleDbCommand("update course set cname='" & UCase(Trim(txtCname.Text)) & "',yintro=" & Val(txtIyear.Text) & ",levels=" & ComboBox1.Text & " where ccode=" & Val(txtCcode.Text), conn)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Course Info. Modified Successfully", "Course Modification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MsgBox("Invalid or Incomplete Data!")
                Finally
                    conn.Close()
                End Try
                txtCcode.Text = ""
                txtCname.Text = ""
                txtIyear.Text = ""

            Case "DELETE"
                If MessageBox.Show("Delete?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
                    Try
                        cmd = New OleDbCommand("delete from course where ccode=" & cc, conn)
                        conn.Open()
                        Dim stat As Integer = cmd.ExecuteNonQuery()
                        If stat > 0 Then
                            MessageBox.Show("Course " & txtCname.Text & " Removed from the Syllabus", "Delete Box", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("No Such Course ")
                        End If
                    Catch ex As Exception
                        MsgBox("Invalid or Incomplete Data!")
                    Finally
                        conn.Close()
                    End Try
                    txtCcode.Text = ""
                    txtCname.Text = ""
                    txtIyear.Text = ""
                End If
        End Select
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        Me.Close()
    End Sub

    Private Sub T_Cls()
        Dim ctl As Control
        For Each ctl In Me.Controls
            If TypeOf ctl Is TextBox Then
                ctl.Text = ""
            End If
        Next
    End Sub

    Sub Max_Ccode()
        Try
            cmd = New OleDbCommand("Select max(Ccode) from Course", conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.Read() = True Then
                txtCcode.Text = dr(0) + 1
            End If
        Catch ex As Exception
            txtCcode.Text = 1001
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New Course_View()
        f.ShowDialog()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class