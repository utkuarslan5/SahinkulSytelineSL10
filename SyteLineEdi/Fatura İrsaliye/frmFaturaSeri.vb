Public Class frmFaturaSeri

    #Region "Methods"

    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        If txtFaturaSeriNo.Text = "" Then
          ErrorProvider1.SetError(txtFaturaSeriNo, "Seri No Giriniz...")
          Exit Sub
        Else
          ErrorProvider1.SetError(txtFaturaSeriNo, "")
        End If

        sFaturaSeri.SeriNo = txtFaturaSeriNo.Text
        sFaturaSeri.FaturaNotu = txtFaturaNotu.Text

        sFaturaSeri.Iptal = False

        Me.Close()
    End Sub

    Private Sub btnVazgec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVazgec.Click
        sFaturaSeri.Iptal = True

        Me.Close()
    End Sub

    Private Sub frmFaturaSeri_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFaturaSeriNo.Text = sFaturaSeri.SeriNo
        'txtFaturaNotu.Text = String.Empty
        txtFaturaNotu.Text = sFaturaSeri.FaturaNotu
    End Sub

    #End Region 'Methods

End Class