Public Class frmLogin

    #Region "Fields"

    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
        End
    End Sub

    Private Sub LogoPictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        KullaniciAdi = txtUsername.Text

        My.Settings.Password = txtPassword.Text

        Me.Hide()

        mdifrmMenu.ShowDialog()

        Me.Close()
    End Sub

    #End Region 'Methods

End Class