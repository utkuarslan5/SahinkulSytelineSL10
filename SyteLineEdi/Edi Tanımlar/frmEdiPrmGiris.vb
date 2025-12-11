Public Class frmEdiPrmGiris

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim sSql As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnCikis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCikis.Click
        Me.Close()
    End Sub

    Private Sub btnKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKaydet.Click
        Try
            sSql = "Update EdiPrm " & _
                    "Set " & _
                    "SIRKET=" & sTirnakEkle(txtSirket.Text) & "," & _
                    "EDIDIR=" & sTirnakEkle(txtEdiDir.Text.Trim) & "," & _
                    "EDIBCKDIR=" & sTirnakEkle(txtEdiBckDir.Text.Trim) & "," & _
                    "EDIASNDIR=" & sTirnakEkle(txtEdiAsnDir.Text.Trim) & "," & _
                    "ETKSUPNM=" & sTirnakEkle(txtEtkSupNm.Text) & "," & _
                    "MAXVOL=" & txtMaxVol.Text & "," & _
                    "IRSSTR=" & sTirnakEkle(txtIrsStr.Text) & "," & _
                    "FATSTR=" & sTirnakEkle(txtFatStr.Text) & "," & _
                    "AGOB=" & sTirnakEkle(txtAgob.Text) & "," & _
                    "BYOB=" & sTirnakEkle(txtByob.Text) & "," & _
                    "HCOB=" & sTirnakEkle(txtHcob.Text) & "," & _
                    "DEFGATE=" & sTirnakEkle(txtDefGate.Text) & "," & _
                    "PRMLUD=" & sTirnakEkle(txtPrmLud.Text) & "," & _
                    "PRMLUU=" & sTirnakEkle(txtPrmLuu.Text) & "," & _
                    "USERF1=" & txtUserf1.Text & "," & _
                    "USERF2=" & txtUserf2.Text & "," & _
                    "USERF3=" & sTirnakEkle(txtUserf3.Text) & "," & _
                    "USERF4=" & sTirnakEkle(txtUserf4.Text) & "," & _
                    "USERF5=" & sTirnakEkle(txtUserf5.Text) & "," & _
                    "USERF6=" & sTirnakEkle(txtUserf6.Text) & "," & _
                    "USERF7=" & txtUserf7.Text & "," & _
                    "USERF8=" & txtUserf8.Text & "," & _
                    "ETKADR1=" & sTirnakEkle(txtEtkAdr1.Text) & "," & _
                    "ETKADR2=" & sTirnakEkle(txtEtkAdr2.Text) & "," & _
                    "ETKADR3=" & sTirnakEkle(txtEtkAdr3.Text) & "," & _
                    "ETKADR4=" & sTirnakEkle(txtEtkAdr4.Text) & "," & _
                    "Contact=" & sTirnakEkle(txtContact.Text) & "," & _
                    "Telephone=" & sTirnakEkle(txtTelephone.Text)

            db.RunSql(sSql, True)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub frmEdiPrmGiris_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            sSql = "Select * " & _
                    " From EdiPrm"

            dt = db.RunSql(sSql)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                With dt.Rows(0)

                    txtSirket.Text = .Item("SIRKET").ToString
                    txtEdiDir.Text = .Item("EDIDIR").ToString
                    txtEdiBckDir.Text = .Item("EDIBCKDIR").ToString
                    txtEdiAsnDir.Text = .Item("EDIASNDIR").ToString
                    txtEtkSupNm.Text = .Item("EtkSupNm").ToString
                    txtMaxVol.Text = .Item("MaxVol").ToString
                    txtIrsStr.Text = .Item("IrsStr").ToString
                    txtFatStr.Text = .Item("FatStr").ToString
                    txtAgob.Text = .Item("Agob").ToString
                    txtByob.Text = .Item("Byob").ToString
                    txtHcob.Text = .Item("Hcob").ToString
                    txtDefGate.Text = .Item("DefGate").ToString
                    txtPrmLud.Text = .Item("PrmLud").ToString
                    txtPrmLuu.Text = .Item("PrmLuu").ToString
                    txtUserf1.Text = .Item("Userf1").ToString
                    txtUserf2.Text = .Item("Userf2").ToString
                    txtUserf3.Text = .Item("Userf3").ToString
                    txtUserf4.Text = .Item("Userf4").ToString
                    txtUserf5.Text = .Item("Userf5").ToString
                    txtUserf6.Text = .Item("Userf6").ToString
                    txtUserf7.Text = .Item("Userf7").ToString
                    txtUserf8.Text = .Item("Userf8").ToString
                    txtEtkAdr1.Text = .Item("EtkAdr1").ToString
                    txtEtkAdr2.Text = .Item("EtkAdr2").ToString
                    txtEtkAdr3.Text = .Item("EtkAdr3").ToString
                    txtEtkAdr4.Text = .Item("EtkAdr4").ToString
                    txtContact.Text = .Item("Contact").ToString
                    txtTelephone.Text = .Item("Telephone").ToString

                End With

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    #End Region 'Methods

End Class