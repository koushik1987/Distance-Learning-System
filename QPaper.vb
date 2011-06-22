Imports System.Data.oledb
Imports System.io

Public Class QPaper

    Dim cans, sans As Integer
    Dim qno As String

    Dim qnos() As Integer
    Dim jk As Integer
    Dim qn As Integer
    Dim nr, noqs As Integer
    Dim hr, mn, se As Integer
    Dim fg As Integer = 0


    Dim gfg As Integer


    Private Sub QPaper_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dtp1.Text = Date.Today
        

        Button1.Enabled = False
        btnConfirm.Enabled = False
        noqs = 0

        Dim con2 As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=sa.mdb")
        Try
            con2.Open()
            Dim cmd As New OleDb.OleDbCommand("select * from emp_regn where eno='" & guid & "'", con2)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            MsgBox(guid)
            If dr.Read() Then
                TextBox1.Text = guid
                txtName.Text = dr.GetValue(1)
                DEPT_combo.Text = dr.GetValue(2)

            End If
            dr.Close()
            Dim cmd2 As New OleDb.OleDbCommand("select * from training where eno='" & guid & "'", con2)
            Dim dr2 As OleDb.OleDbDataReader = cmd2.ExecuteReader(CommandBehavior.SingleRow)

            If dr2.Read() Then
                Ccode_Combo2.Text = dr2.GetValue(1)
            End If

            dr2.Close()

            Dim cmd3 As New OleDb.OleDbCommand("select * from course where ccode=" & Ccode_Combo2.Text, con2)
            Dim dr3 As OleDb.OleDbDataReader = cmd3.ExecuteReader(CommandBehavior.SingleRow)

            If dr3.Read() Then
                txtCName.Text = dr3.GetValue(1)
                Dim i As Integer
                For i = 1 To dr3.GetValue(3)
                    ComboBox1.Items.Add(i)
                Next
            End If

            dr2.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con2.Close()
        End Try

    End Sub

    Private Sub Ccode_Combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cmd = New oledbCommand("Select cname from course where ccode=" & CInt(CCode_Combo.Text), conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.Read Then
                txtCName.Text = dr(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try


        Try 'Check for Existing of Temp Table
            conn.Open()
            cmd = New oledbcommand("if exists(select Table_Name from Information_Schema.Tables where Table_Name='temp') drop table temp", conn)
            '(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = temp )"
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        Try
            'cmd = New oledbcommand("select * into temp from questans", conn)
            cmd = New oledbcommand("select * into temp from C" & CCode_Combo.Text, conn)
            conn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            'cmd = New oledbcommand("drop table temp ", conn)
            'cmd.ExecuteNonQuery()
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        If RadioButton1.Checked = True Then
            sans = 1
        Else
            If RadioButton2.Checked = True Then
                sans = 2
            Else
                If RadioButton3.Checked = True Then
                    sans = 3
                Else
                    sans = 4
                End If
            End If
        End If
        'Dim conn2 As New oledbclient.oledbconnection("Data Source=administrator;Initial Catalog=oms;User ID=sa;password=cmtes")
        Dim conn2 As New OleDb.OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=sa.mdb")
        Try
            conn2.Open()
            Dim cmd2 As New OleDb.OleDbCommand("insert into tresult values('" & TextBox1.Text & "'," & Ccode_Combo2.Text & ",'" & qno & "'," & sans & "," & cans & ",'" & Dtp1.Text & "')", conn2)
            cmd2.ExecuteNonQuery()
            MessageBox.Show("Answer Recorded!Click Question button for the next question")

            Button1.Enabled = True
            btnConfirm.Enabled = False
        Catch ex As Exception
            MessageBox.Show("Question repeated twice!" & ex.Message)
        Finally
            conn2.Close()
        End Try
    End Sub

    Public Function gen_qno() As Integer
        ' Try
        If conn.State = 0 Then conn.Open()
        cmd = New oledbcommand("select count(qno) from temp", conn)
        dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
        Dim fg As Integer
        fg = 0
        If dr.Read() Then
            nr = dr.GetValue(0)
            Dim r As New Random()
            qn = r.Next(nr)
            gen_qno = qn
        End If
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If noqs < 20 Then
                btnConfirm.Enabled = True
                noqs = noqs + 1
                cmd = New OleDbCommand("select count(qno) from temp", conn)
                conn.Open()
                dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess)

                If dr.Read() Then
                    nr = dr.GetValue(0)
                End If
                'MessageBox.Show("nr=>" & nr)
                Dim t As Integer
                ReDim Preserve qnos(nr)
                For t = 0 To nr
                    qnos(t) = 0
                Next
                Dim r As New Random(1)
                qn = r.Next(0, nr)
                dr.Close()

                Dim cmd2 As New OleDb.OleDbCommand("select * from temp", conn)
                Dim dr2 As OleDbDataReader = cmd2.ExecuteReader(CommandBehavior.SequentialAccess)
                Dim jj, tqn As Integer
                Dim quest, anss As String
                quest = ""
                anss = ""

                For jj = 0 To qn
                    dr2.Read()
                    tqn = dr2.GetValue(0)

                    quest = dr2.GetValue(1)
                    qno = quest
                    cans = dr2.GetValue(2)
                    anss = dr2.GetValue(3)
                Next

                lblQuestion.Text = quest
                Dim arrans() As String

                arrans = Split(anss, "*")
                RadioButton1.Text = arrans(0)
                RadioButton2.Text = arrans(1)
                RadioButton3.Text = arrans(2)
                RadioButton4.Text = arrans(3)
                dr2.Close()

                Dim cmd22 As New OleDbCommand("delete from temp where qno=" & tqn, conn)
                Dim stat As Integer = cmd22.ExecuteNonQuery()
                Button1.Enabled = False
            Else
                Timer1.Enabled = False
                MessageBox.Show("Examination Done!Click to view your result................")
                btnCancel.Enabled = True
                Button2.Enabled = True
                showresult()
            End If
        Catch ex As Exception
            nr = 0
            MsgBox("No questions available in the database" & ex.Message)
            End
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        If ComboBox1.Text <> "" And gfg = 0 Then
            lblEtime.Text = Date.Now.Hour & ":" & Date.Now.Minute
            lblRtime.Text = "00:00:00"
            mn = 3
            se = 60
            Timer1.Enabled = True
            Button1.Enabled = True
            btnStart.Enabled = False
        Else
            MsgBox("Select a level to start the examination!")
            Dim ch
            ch = MsgBox("Want to proceed or return to menu", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm")
            If ch = vbNo Then
                Me.Close()
            End If
        End If
       
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblRtime.Text = mn & ":" & se
        se = se - 1
        If se = 0 Then
            mn = mn - 1
            se = 60
        End If

        If mn = -1 Then
            lblRtime.Text = "00:00:"
            Timer1.Enabled = False
            MessageBox.Show("Your time is up!Click to view your result.............")
            showresult()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
        empoptions.Show()
    End Sub

    Private Sub CCode_Combo_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CCode_Combo.SelectedIndexChanged
        Try
            cmd = New oledbcommand("Select cname from course where ccode=" & CCode_Combo.Text, conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.Read Then
                txtCName.Text = dr(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try


        Try 'Check for Existance of Temp Table
            conn.Open()
            cmd = New oledbcommand("if exists(select Table_Name from Information_Schema.Tables where Table_Name='temp') drop table temp", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        Try
            'cmd = New oledbcommand("select * into temp from questans", conn)
            cmd = New oledbcommand("select * into temp from C" & CCode_Combo.Text, conn)
            conn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub showresult()
        Dim tcans As Integer = 0
        Try
            cmd = New OleDbCommand("Select * from tresult where ccode=" & Ccode_Combo2.Text & " and regno ='" & TextBox1.Text & "' and edate='" & Dtp1.Text & "'", conn)
            conn.Open()
            dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
            While dr.Read()
                If dr.GetValue(3) = dr.GetValue(4) Then tcans = tcans + 1
            End While
            If tcans >= 12 Then
                MsgBox("You have secured " & (tcans / 20) * 100 & "%")
                MsgBox("Successfully Cleared Course " & Ccode_Combo2.Text & " and level " & ComboBox1.Text & " in attempt no " & Semester_Combo.Text)

                Dim cmd3 As New OleDbCommand("insert into result values('" & TextBox1.Text & "'," & Ccode_Combo2.Text & ",'" & ComboBox1.Text & "','" & Dtp1.Text & "','Pass')", conn)
                cmd3.ExecuteReader()
            Else
                Dim cmd3 As New OleDbCommand("insert into result values('" & TextBox1.Text & "'," & Ccode_Combo2.Text & ",'" & ComboBox1.Text & "','" & Dtp1.Text & "','Fail')", conn)
                cmd3.ExecuteReader()
                If Semester_Combo.Text = 3 Then
                    MsgBox("All your attempts have gone in Vain! Contact Co-ordinator.")
                Else
                    MsgBox("You are required to Re-Attempt again!")
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        'Dim f As New Form8()
        'f.TextBox1.Text = TextBox1.Text 'Roll No
        'f.txtCCode.Text = CCode_Combo.Text 'Course Code
        'f.txtClass.Text = txtClass.Text
        'f.ShowDialog()
    End Sub

    Private Sub ComboBox1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.Enter
        
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim fg As Integer
        Dim tc As Integer
        'Dim fg2 As Integer
        Try
            conn.Open()

            Dim cmdt1 As New OleDbCommand("select * from result where eno='" & TextBox1.Text & "' and ccode=" & Ccode_Combo2.Text & " and lvl=" & ComboBox1.Text, conn)
            Dim drt1 As OleDbDataReader = cmdt1.ExecuteReader(CommandBehavior.SequentialAccess)
            If drt1.Read() Then
                MsgBox("This level has been completed already.")
                ComboBox1.Text = ""
                conn.Close()
                gfg = 1
                Exit Sub
            End If
            drt1.Close()


            Dim cmdt As New OleDbCommand("select * from tresult where regno='" & TextBox1.Text & "' and ccode=" & Ccode_Combo2.Text & " and edate='" & Dtp1.Text & "'", conn)
            Dim drt As OleDbDataReader = cmdt.ExecuteReader(CommandBehavior.SequentialAccess)
            If drt.Read() Then
                MsgBox("Exam done for this date.Try again another day")
                ComboBox1.Text = ""
                conn.Close()
                gfg = 1
                Exit Sub
            End If
            drt.Close()

            gfg = 0
            cmd = New OleDbCommand("select count(*) from result where eno='" & TextBox1.Text & "' and ccode=" & Ccode_Combo2.Text & " and lvl=" & ComboBox1.Text & " and status='Pass'", conn)

            Dim cmdck As New OleDbCommand("select * from training where eno='" & TextBox1.Text & "'", conn)
            Dim drck As OleDbDataReader = cmdck.ExecuteReader(CommandBehavior.SingleRow)
            If drck.Read() Then
                If Date.Today > drck.GetValue(3) Then
                    MsgBox("Your training time period exceeds allocated time. Contact co-ordinator")
                    End
                End If
            End If

            Dim dr As OleDbDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read() Then
                Dim nr As Integer
                nr = dr.GetValue(0)
                If (nr = 1) Then
                    MsgBox("Level already Done!")
                    btnStart.Enabled = False
                    fg = 1
                Else

                    'MsgBox("create table temp as select * from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text)
                    Try
                        Dim cmd3 As New OleDbCommand("select * into temp from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text, conn)
                        cmd3.ExecuteNonQuery()
                    Catch ee As Exception
                        Dim cmd33 As New OleDbCommand("drop table temp", conn)
                        cmd33.ExecuteNonQuery()
                        Dim cmd3 As New OleDbCommand("select * into temp from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text, conn)
                        cmd3.ExecuteNonQuery()
                    End Try
                    tc = 1
                    Semester_Combo.Text = nr + 1
                    btnStart.Enabled = True
                End If
            End If
            If fg = 0 Then
                btnStart.Enabled = True
            End If
            dr.Close()
        Catch ee As Exception
            MsgBox(ee.Message)
        End Try

        Try
            Dim cmd2 As New OleDbCommand("select count(*) from result where eno='" & TextBox1.Text & "' and ccode=" & Ccode_Combo2.Text & " and lvl=" & ComboBox1.Text & " and status='Fail'", conn)

            Dim dr2 As OleDbDataReader = cmd2.ExecuteReader(CommandBehavior.SingleRow)
            If dr2.Read() Then
                Dim nr As Integer
                nr = dr2.GetValue(0)
                If nr = 3 Then
                    MsgBox("Sorry! Your specifed attempts exceeded!Contact Co-ordinator")
                    End
                Else
                    'MsgBox("sql=>" & "create table temp as select * from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text)
                    'Dim cmd3 As New OleDbCommand("create table temp as select * from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text, conn)
                    If tc = 0 Then
                        Dim cmd3 As New OleDbCommand("select * into temp from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text, conn)
                        cmd3.ExecuteNonQuery()
                    End If
                    Semester_Combo.Text = nr + 1
                    btnStart.Enabled = True
                    End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try


        'Try
        '    'cmd = New oledbcommand("select * into temp from questans", conn)
        '    Dim cmd3 As New OleDbCommand("create table temp as select * from C" & Ccode_Combo2.Text & "_" & ComboBox1.Text, conn)

        '    cmd3.ExecuteNonQuery()
        'Catch ex As Exception
        '    'cmd = New oledbcommand("drop table temp ", conn)
        '    'cmd.ExecuteNonQuery()
        '    MessageBox.Show(ex.Message)
        'Finally
        '    conn.Close()
        'End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        viresult.Show()
        Button2.Enabled = False
    End Sub
End Class