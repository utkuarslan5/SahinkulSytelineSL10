Public Class frmAyniyatIade

    Public bAyniyatIade As Boolean = False

    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click

        bAyniyatIade = True

        Me.Close()
    
    End Sub

    Private Sub btnCikis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCikis.Click

        bAyniyatIade = False

        Me.Close()

    End Sub

    Private Sub frmAyniyatIade_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GridEX1.DataSource = dtAyniyat

    End Sub
End Class