Imports System.Data.oledb
Public Class Form9
    Dim Tname As String = ""
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If questTextBox.Text <> "" And Ccode_Combo.Text <> "" And level_combo.Text <> "" And prepTextBox.Text <> "" And aposComboBox.Text <> "" Then
            If ansListbox.Items.Count = 4 Then
                If RadioButton1.Checked = True Then
                    If Ccode_Combo.Text <> "" And level_combo.Text <> "" Then
                        Max_Qno() 'Procedure call for Auto generation of question numbers
                        Tname = "C" & Ccode_Combo.Text & "_" & level_combo.Text
                    Else
                        MessageBox.Show("Select Course and Level to save into db")
                        Exit Sub
                    End If
                    'ADD
                    'Dim Tname As String = "C" & Ccode_Combo.Text
                    Try
                        Dim v_ans As String = ""
                        For i As Integer = 0 To ansListbox.Items.Count - 1
                            v_ans = v_ans & ansListbox.Items(i).ToString & "*"
                        Next
                        conn.Open()
                        'cmd = New SqlCommand("insert into questans(quest,apos,ans,dept,class,prep) values('" & questTextBox.Text & "'," & Val(aposTextbox.Text) & ",'" & v_ans & "','" & DEPT_combo.Text & "','" & Class_Combo.Text & "','" & prepTextBox.Text & "')", conn)
                        'cmd = New SqlCommand("insert into " & Tname & "(quest,apos,ans,dept,class,prep) values('" & questTextBox.Text & "'," & Val(aposTextbox.Text) & ",'" & v_ans & "','" & DEPT_combo.Text & "','" & Class_Combo.Text & "','" & prepTextBox.Text & "')", conn)
                        cmd = New OleDbCommand("insert into " & Tname & " values(" & Val(txtQno.Text) & ",'" & Trim(questTextBox.Text) & "'," & Val(aposComboBox.Text) & ",'" & v_ans & "','" & Trim(prepTextBox.Text) & "')", conn)
                        cmd.ExecuteNonQuery()
                        MsgBox("Question Recorded!")
                        txtQno.Text = ""
                        questTextBox.Text = ""
                        ansListbox.Items.Clear()
                        ansTextBox.Text = ""
                        prepTextBox.Text = ""
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        conn.Close()
                    End Try

                    questTextBox.Text = ""
                    ansListbox.Items.Clear()



                ElseIf RadioButton2.Checked = True Then
                    'MODIFY

                    Try
                        Dim v_ans As String = ""
                        For i As Integer = 0 To ansListbox.Items.Count - 1
                            v_ans = v_ans & ansListbox.Items(i).ToString & "*"
                        Next
                        cmd = New OleDbCommand("update " & Tname & " set quest='" & Trim(questTextBox.Text) & "',apos=" & aposComboBox.Text & ",ans='" & v_ans & "',prep='" & Trim(prepTextBox.Text) & "' where qno=" & txtQno.Text, conn)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        MsgBox("Updated")
                        txtQno.Text = ""
                        questTextBox.Text = ""
                        ansListbox.Items.Clear()
                        ansTextBox.Text = ""
                        prepTextBox.Text = ""

                    Catch ex As Exception
                        MsgBox("Invalid or no data present")
                    Finally
                        conn.Close()
                    End Try
                ElseIf RadioButton3.Checked = True Then
                    'DELETE
                    If MessageBox.Show("Delete?", "Delete Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Try
                            cmd = New OleDbCommand("delete from " & Tname & " where qno=" & txtQno.Text, conn)
                            conn.Open()
                            cmd.ExecuteNonQuery()
                            MsgBox("Question Deleted")
                            txtQno.Text = ""
                            questTextBox.Text = ""
                            ansListbox.Items.Clear()
                            ansTextBox.Text = ""
                            prepTextBox.Text = ""
                        Catch ex As Exception
                            MsgBox("Invalid or no data present")
                        Finally
                            conn.Close()
                        End Try
                    Else
                        MsgBox("Deletion Cancelled")
                    End If
                End If
            Else
                MsgBox("Require 4 answers to save this question")
            End If
        Else
            MsgBox("All Fields are mandatory!")
        End If
        Refresh_Combo()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ansTextBox.Text.Trim() <> "" Then
            If ansListbox.Items.Count < 4 Then
                ansListbox.Items.Add(UCase(ansTextBox.Text))
                ansTextBox.Clear()
                ansTextBox.Focus()
            Else
                MessageBox.Show("4 answers already accepted")
            End If
        Else
            MessageBox.Show("No answer recorded to add")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Form9_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            cmd = New OleDbCommand("select Ccode from Course", conn)
            conn.Open()
            dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
            Ccode_Combo.Items.Clear()
            While dr.Read()
                Ccode_Combo.Items.Add(dr(0))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub DEPT_combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles level_combo.SelectedIndexChanged
        Tname = "C" & Ccode_Combo.Text & "_" & level_combo.Text
        Refresh_Combo()
        'Try
        '    conn.Open()
        '    cmd = New OleDbCommand("select acronym from dept where deptno=" & level_combo.Text, conn)
        '    dr = cmd.ExecuteReader(CommandBehavior.SingleRow)

        '    Class_Combo.Items.Clear()
        '    If dr.Read Then
        '        Class_Combo.Items.Add("A" + dr(0))
        '        Class_Combo.Items.Add("N" + dr(0))
        '        Class_Combo.Items.Add("D" + dr(0))
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    conn.Close()
        'End Try
    End Sub

    Private Sub ansTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ansTextBox.KeyPress
        'If Asc(e.KeyChar) = 13 Then
        '    If ansListbox.Items.Count <= 4 Then
        '        ansListbox.Items.Add(UCase(ansTextBox.Text))
        '        ansTextBox.Text = ""
        '        ansTextBox.Focus()
        '    Else
        '        MessageBox.Show("4 answers already accepted")
        '    End If
        'End If
    End Sub

    Private Sub Ccode_Combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ccode_Combo.SelectedIndexChanged

        Try
            conn.Open()
            cmd = New OleDbCommand("select cname,levels from course where ccode=" & Ccode_Combo.Text, conn)
            dr = cmd.ExecuteReader()
            level_combo.Items.Clear()
            Dim nr, i As Integer

            If dr.Read() Then
                txtCName.Text = dr.GetValue(0)
                nr = dr.GetValue(1)
                i = 1
                While i <= nr

                    level_combo.Items.Add(i)
                    i = i + 1
                End While
            End If

            
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        'Tname = "C" & Ccode_Combo.Text
        'If RadioButton1.Checked = True Then ' Radiobutton Add
        '    Max_Qno() 'Procedure call for Auto generation of question numbers
        '    Try
        '        cmd = New OleDbCommand("Select cname from course where ccode=" & CInt(Ccode_Combo.Text), conn)
        '        conn.Open()
        '        dr = cmd.ExecuteReader()
        '        If dr.Read Then
        '            txtCName.Text = dr(0)
        '        End If
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    Finally
        '        conn.Close()
        '    End Try
        'ElseIf RadioButton2.Checked = True Then 'Radiobutton Edit
        '    Try
        '        cmd = New OleDbCommand("Select cname from course where ccode=" & CInt(Ccode_Combo.Text), conn)
        '        conn.Open()
        '        dr = cmd.ExecuteReader()
        '        If dr.Read Then
        '            txtCName.Text = dr(0)
        '        End If
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    Finally
        '        conn.Close()
        '    End Try

        '    Try
        '        cmd = New OleDbCommand("Select * from " & Tname, conn)
        '        da = New OleDbDataAdapter(cmd)
        '        ds = New DataSet
        '        da.Fill(ds)
        '        DataGrid1.DataSource = ds
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'ElseIf RadioButton3.Checked = True Then
        '    Try
        '        cmd = New OleDbCommand("Select cname from course where ccode=" & CInt(Ccode_Combo.Text), conn)
        '        conn.Open()
        '        dr = cmd.ExecuteReader()
        '        If dr.Read Then
        '            txtCName.Text = dr(0)
        '        End If
        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    Finally
        '        conn.Close()
        '    End Try

        '    Refresh_Combo()
        'End If
    End Sub

    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        On Error Resume Next
        Dim r As Integer
        r = DataGrid1.CurrentCell.RowNumber
        Dim uans()
        txtQno.Text = "" & DataGrid1.Item(r, 0)
        questTextBox.Text = "" & DataGrid1.Item(r, 1)
        aposComboBox.Text = "" & DataGrid1.Item(r, 2)
        uans = Split(DataGrid1.Item(r, 3), "*")
        ansListbox.Items.Clear()
        prepTextBox.Text = "" & DataGrid1.Item(r, 4)

        For i As Integer = LBound(uans) To UBound(uans) - 1
            ansListbox.Items.Add(uans(i))
        Next
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            level_combo.DropDownStyle = ComboBoxStyle.DropDown
            Class_Combo.DropDownStyle = ComboBoxStyle.DropDown
            Button2.Text = "UPDATE"

            btnDel.Visible = True
        Else
            level_combo.DropDownStyle = ComboBoxStyle.DropDownList
            Class_Combo.DropDownStyle = ComboBoxStyle.DropDownList

            btnDel.Visible = False

        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Button2.Text = "SAVE"
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Button2.Text = "DELETE"
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        ansListbox.Items.Remove(ansListbox.SelectedItem)
    End Sub

    Sub Max_Qno()
        'Autogeneration of the Question Number
        Try
            cmd = New OleDbCommand("select max(qno) from " & Tname, conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                txtQno.Text = dr(0) + 1
            Else
                txtQno.Text = 1
            End If
        Catch ex As Exception
            txtQno.Text = 1
        Finally
            conn.Close()
        End Try
    End Sub

    Sub Refresh_Combo()
        Try
            cmd = New OleDbCommand("Select * from " & Tname, conn)
            da = New OleDbDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds)
            DataGrid1.DataSource = ds
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGrid1_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid1.Navigate

    End Sub
End Class