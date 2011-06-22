Public NotInheritable Class SplashScreen1


    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '   If ProgressBar1.Value < ProgressBar1.Maximum Then
    '      ProgressBar1.Value = ProgressBar1.Value + ProgressBar1.Step
    ' Else
    '    Me.Hide()
    '   Timer1.Enabled = False
    'Dim f As New Form1()
    '    f.ShowDialog()
    'End If
    'End Sub

    Private Sub ApplicationTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Hide()
        Timer1.Enabled = False
        Form1.Show()

    End Sub
End Class
