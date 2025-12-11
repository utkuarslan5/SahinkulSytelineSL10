Public Class frmIrsaliyeSeri2
    Dim sQuery As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable

    Private Sub frmIrsaliyeSeri_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Normal Sevkiyat
        'Ekstra Sevkiyat
        'Bedelsiz Sevkiyat
        'Üretim Bakiyesi

        Try
            BolgeselAyar()

            sQuery = " Select delterm, description" & _
                       " from del_term"

            dt = db.RunSql(sQuery)

            cmbTransport.DataSource = dt

            sQuery = " Select ship_code, description" & _
            " from shipcode"

            dt = db.RunSql(sQuery)

            cmbNakliyeci.DataSource = dt

            sQuery = "Select  Carrier, TrnCode" & _
                        " From Plantprm " & _
                        " Where canb=" & sTirnakEkle(sIrsaliyeSeri.Musteri) & _
                        " And b9cd=" & sTirnakEkle(sIrsaliyeSeri.Plant)

            dtTemp = db.RunSql(sQuery)

            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                If sIrsaliyeSeri.Carrier = "" Then

                    GetRowInfo(sIrsaliyeSeri.Carrier, dtTemp, 0, "Carrier")

                End If

            End If



            txtIrsaliyeSeri.Text = sIrsaliyeSeri.IrsaliyeNo
            txtNavlunFatura.Text = sIrsaliyeSeri.NavlunFaturaNo
            txtPlakaNo.Text = sIrsaliyeSeri.Plaka
            txtSeferNo.Text = sIrsaliyeSeri.SeferNo
            cmbTransport.Text = sIrsaliyeSeri.Transport
            cmbTransport.Value = sIrsaliyeSeri.Transport
            'cmbNakliyeSekli.Text = sIrsaliyeSeri.Transport
            'cmbNakliyeSekli.Value = sIrsaliyeSeri.Transport
            cmbNakliyeci.Text = sIrsaliyeSeri.Carrier
            cmbNakliyeci.Value = sIrsaliyeSeri.Carrier
            txtAciklama.Text = sIrsaliyeSeri.Aciklama
            txtKutuAdet.Text = sIrsaliyeSeri.kutuAdedi.ToString
            txtPaletAdet.Text = sIrsaliyeSeri.paletAdedi.ToString
            dtmFiiliSevkSaati.Text = sIrsaliyeSeri.FiiliSevkSaati
            dtmFiiliSevk.Text = sIrsaliyeSeri.FiiliSevkTarihi
            'txtTeslimEden.Text = sIrsaliyeSeri.TeslimEden

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click

        If cmbNakliyeSekli.Text = " " Then
            MessageBox.Show("Nakliye Şeklini Seçiniz.")
            Exit Sub
        End If

        sIrsaliyeSeri.IrsaliyeNo = txtIrsaliyeSeri.Text
        sIrsaliyeSeri.SeferNo = txtSeferNo.Text
        sIrsaliyeSeri.NavlunFaturaNo = txtNavlunFatura.Text
        sIrsaliyeSeri.Plaka = txtPlakaNo.Text
        sIrsaliyeSeri.Transport = cmbTransport.Value
        'sIrsaliyeSeri.Transport = cmbNakliyeSekli.Text
        sIrsaliyeSeri.Carrier = cmbNakliyeci.Value
        sIrsaliyeSeri.TeslimAlan = txtTeslimAlan.Text
        sIrsaliyeSeri.Aciklama = txtAciklama.Text
        sIrsaliyeSeri.Nakliyeci = cmbNakliyeci.Text
        sIrsaliyeSeri.TeslimEden = txtTeslimEden.Text
        sIrsaliyeSeri.kutuAdedi = CDec(txtKutuAdet.Text)
        sIrsaliyeSeri.paletAdedi = CDec(txtPaletAdet.Text)
        sIrsaliyeSeri.FiiliSevkSaati = dtmFiiliSevkSaati.Text
        sIrsaliyeSeri.FiiliSevkTarihi = dtmFiiliSevk.Value
        sIrsaliyeSeri.Iptal = False

        Me.Close()

    End Sub

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click

        sIrsaliyeSeri.Iptal = True

        Me.Close()
    End Sub

End Class