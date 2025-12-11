Public Class frmIrsaliyeSeri

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
        sIrsaliyeSeri.Iptal = True

        Me.Close()
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click

        

        sIrsaliyeSeri.IrsaliyeNo = txtIrsaliyeSeri.Text
        sIrsaliyeSeri.TeslimAlan = txtTeslimAlan.Text
        sIrsaliyeSeri.SeferNo = txtSeferNo.Text

        sIrsaliyeSeri.NavlunFaturaNo = txtNavlunFatura.Text

        sIrsaliyeSeri.Plaka = txtPlakaNo.Text
        sIrsaliyeSeri.Transport = cmbTransport.Value
        sIrsaliyeSeri.Carrier = cmbNakliyeci.Value
        sIrsaliyeSeri.Iptal = False

        sIrsaliyeSeri.sNavlunNo = txtNavlunNo.Text
        sIrsaliyeSeri.BeyanNo = txtBeyanNo.Text
        sIrsaliyeSeri.BeyanTarihi = dtmBeyanTarihi.Text

        Me.Close()
    End Sub

    Private Sub frmIrsaliyeSeri_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


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

                If sIrsaliyeSeri.Transport = "" Then

                    GetRowInfo(sIrsaliyeSeri.Transport, dtTemp, 0, "TrnCode")

                End If

            End If

            txtIrsaliyeSeri.Text = sIrsaliyeSeri.IrsaliyeNo

            txtNavlunFatura.Text = sIrsaliyeSeri.NavlunFaturaNo

            txtPlakaNo.Text = sIrsaliyeSeri.Plaka
            txtTeslimAlan.Text = sIrsaliyeSeri.TeslimAlan
            txtSeferNo.Text = sIrsaliyeSeri.SeferNo

            cmbTransport.Text = sIrsaliyeSeri.Transport
            cmbTransport.Value = sIrsaliyeSeri.Transport

            txtNavlunNo.Text = sIrsaliyeSeri.sNavlunNo

            cmbNakliyeci.Text = sIrsaliyeSeri.Carrier
            cmbNakliyeci.Value = sIrsaliyeSeri.Carrier

            txtBeyanNo.Text = sIrsaliyeSeri.BeyanNo
            dtmBeyanTarihi.Text = sIrsaliyeSeri.BeyanTarihi

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    #End Region 'Methods

End Class