Public Class frmSevkiyatKalemDetay

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sHata As String
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVazgec.Click
        sSevkiyat.Iptal = True

        Me.Close()
    End Sub

    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        MiktarKontrol()

        If sHata <> "" Then

            MessageBox.Show(sHata)

            'Exit Sub

        End If

        If sSevkiyat.ShipQty = txtShipQty.Text And
            sSevkiyat.BoxQty = txtBoxQty.Text And
            sSevkiyat.PackingCode = cmbPackingCode.Value Then

            sSevkiyat.Iptal = True

        Else

            sSevkiyat.ShipQty = txtShipQty.Text
            sSevkiyat.BoxQty = txtBoxQty.Text
            sSevkiyat.PackingCode = cmbPackingCode.Value

            sSevkiyat.Iptal = False

        End If

        Me.Close()
    End Sub

    Private Sub cmbPackingCode_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPackingCode.ValueChanged
        Dim rowview As DataRowView = cmbPackingCode.SelectedItem

        If Not rowview Is Nothing Then

            txtBoxQty.Text = rowview.Item("KMIK").ToString

        End If
    End Sub

    Private Sub frmIrsaliyeSeri_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            sQuery = " Select AMBKOD, AMBTAN, KMIK" & _
                        " from ITMPACK" &
                        " WHERE ITNBR=" & sTirnakEkle(sSevkiyat.Item)

            dt = db.RunSql(sQuery)

            cmbPackingCode.DataSource = dt

            txtCoNum.Text = sSevkiyat.co_num
            txtCoLine.Text = sSevkiyat.co_line
            txtItem.Text = sSevkiyat.Item
            txtDescription.Text = sSevkiyat.Description
            txtShipQty.Text = sSevkiyat.ShipQty
            txtBoxQty.Text = sSevkiyat.BoxQty
            cmbPackingCode.Text = sSevkiyat.PackingCode
            cmbPackingCode.Value = sSevkiyat.PackingCode

            MiktarKontrol()

            txtShipQty.Focus()

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub MiktarKontrol()
        Dim miktar As Double = -1
        Dim miktarint As Double = 0

        If txtBoxQty.Text = 0 Then
            miktar = 0
        Else
            miktar = Math.Ceiling(txtShipQty.Text / txtBoxQty.Text)
            miktarint = (txtShipQty.Text / txtBoxQty.Text)
        End If

        If miktarint <> CInt(miktarint) Then

            sHata = "Kutu İçi miktar İle Sevk Miktarı Uyumsuz"

            HataGoster.SetError(txtBoxQty, sHata)
            HataGoster.SetError(txtShipQty, sHata)

        Else
            sHata = ""

            HataGoster.SetError(txtBoxQty, sHata)
            HataGoster.SetError(txtShipQty, sHata)

        End If
    End Sub

    Private Sub txtBoxQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBoxQty.LostFocus, txtShipQty.LostFocus
        MiktarKontrol()
    End Sub

    #End Region 'Methods

End Class