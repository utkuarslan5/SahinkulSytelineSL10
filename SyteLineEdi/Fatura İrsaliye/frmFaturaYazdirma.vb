Imports CrystalDecisions.Shared

'********************************************
'Date : 11122012
'Name : Rümeyda GÖNÜL
'Form Name : frmFaturaYazdirma
'Main Version : V10.10
'Form Version : V5
'*********************************************


Public Class frmFaturaYazdirma
    Dim sQuery As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)

    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim custNum As String

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick

        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müsteri, Name as Tanim " & _
                            " From CustAddr " & _
                            " Where cust_seq = 0 " & _
                            " ORDER BY 1 "

            FindFormCagir(ssorgu, "Müsteri", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try



    End Sub

    Private Sub txtMusteri2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri2.ButtonClick

        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müsteri" & ",Name as Tanim " & _
                            " From CustAddr " & _
                            " Where cust_seq = 0 " & _
                            " ORDER BY 1 "

            FindFormCagir(ssorgu, "Müsteri", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try



    End Sub

    Private Sub txtAmbar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmbar1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct whse as 'Ambar', name as 'Tanim' " & _
                            " From whse " & _
                            " ORDER BY 1 "

            FindFormCagir(ssorgu, "Ambar", "Tanim", txtAmbar1.Text, "")

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click

        Try
            Cursor = Cursors.WaitCursor

            sQuery = " Select * From  TrM_FaturaPrn_SL10 " & _
                           " Where 1=1 " & _
                                " And ISNULL(Uf_ih_Printed ,0) " & IIf(rdbIlkBasim.Checked, " = 0 ", " = 1 ")

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & "    and cust_num='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & "    and cust_num>='" & txtMusteri1.Text & "'" & _
                                        " and cust_num<='" & txtMusteri2.Text & "'"

                End If


            End If

            If txtAmbar1.Text <> "" Then

                sQuery = sQuery & "    and whse='" & txtAmbar1.Text & "'"

            End If

            If txtTeslimAlan1.Text <> "" Then

                If txtTeslimAlan2.Text = "" Then

                    sQuery = sQuery & "    and cust_seq='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery = sQuery & "    and cust_seq>='" & txtTeslimAlan1.Text & "'" & _
                                        " and cust_seq<='" & txtTeslimAlan2.Text & "'"
                End If

            End If




            If txtKullanici.Text <> "" Then

                sQuery = sQuery & "    and Kullanici like '%" & txtKullanici.Text & "%'"

            End If

            If chkTarihAraligi.Checked Then

                'sQuery = sQuery & " and dbo.Dateserial(datepart(Year,CreateDate),datepart(Month,CreateDate),datepart(Day,CreateDate)) >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd")) & _
                '                    " and dbo.Dateserial(datepart(Year,CreateDate),datepart(Month,CreateDate),datepart(Day,CreateDate)) <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy/MM/dd"))
                'sQuery = sQuery & " and dbo.Dateserial(datepart(Year,inv_date),datepart(Month,inv_date),datepart(Day,inv_date)) >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd")) & _
                '                  " and dbo.Dateserial(datepart(Year,inv_date),datepart(Month,inv_date),datepart(Day,inv_date)) <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy/MM/dd"))
                sQuery = sQuery & " and convert(nvarchar(10),inv_date,126) BETWEEN " & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd")) & " AND " & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy-MM-dd"))

            Else

                'sQuery = sQuery & " and dbo.Dateserial(datepart(Year,CreateDate),datepart(Month,CreateDate),datepart(Day,CreateDate)) =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd"))
                sQuery = sQuery & " and convert(nvarchar(10),inv_date,126) =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd"))
            End If

            'sQuery = sQuery & " Group By inv_num, inv_date, sevkno, item, co_num, co_line"

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                Duzenle(GridEX1)
            Else

                GridEX1.DataSource = Nothing


            End If

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnYazdır_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdır.Click

        Dim checkedRows As Janus.Windows.GridEX.GridEXRow() = Me.GridEX1.GetCheckedRows()

        Dim row As Janus.Windows.GridEX.GridEXRow
        Dim faturaNo As String = ""
        Dim faturaSeriNo As String = ""
        Dim doNum As String = ""
        Dim aciklama As String = ""
        Dim sFatTipi As String = "Y"
        Dim sSevkNolar As String = ""
        Dim sIrsNolar As String = ""
        Dim sIrsNolar2 As String = ""
        'Dim sFatTipi As String = "Y"
        'Dim FaturaTarihi As Date
        Dim FaturaSistemNo As String = ""
        Dim totalInvAmt As Decimal = 0
        Dim paraBirimi As String = ""

        If checkedRows.Length = 0 Then
            MessageBox.Show("Lütfen kayıt seçiniz.")
            Exit Sub
        End If

        Try
            Cursor = Cursors.WaitCursor

            For Each row In checkedRows

                If faturaNo <> "" Then
                    If faturaNo <> row.Cells("inv_num").Text Then
                        MessageBox.Show("Seçilen kayıtlar farklı faturalara ait.")
                        Exit Sub
                    End If
                End If

                faturaNo = row.Cells("inv_num").Text

                faturaSeriNo = row.Cells("Uf_FaturaSeriNo").Text
                aciklama = row.Cells("Uf_ih_Aciklama").Text

                doNum = row.Cells("do_num").Text
                totalInvAmt = row.Cells("TotalInvAmt").Text
                paraBirimi = row.Cells("curr_code").Text
                custNum = row.Cells("cust_num").Text
            Next

            sQuery = "SELECT COUNT(*) FROM TRM_tmpfatura where inv_num = " & sTirnakEkle(faturaNo)
            dt = db.RunSql(sQuery)

            If dt.Rows(0)(0) <> 0 Then
                db.RunSql("DELETE FROM TRM_tmpfatura where inv_num = " & sTirnakEkle(faturaNo))
            End If

            For Each row In checkedRows

                If Not sSevkNolar.Contains(row.Cells("SEVKNO").Text) Then

                    sSevkNolar = IIf(sSevkNolar = "", "", sSevkNolar & ",") & row.Cells("SEVKNO").Text

                End If

                'If Not sIrsNolar.Contains(row.Cells("IRSNO").Text & "-" & TarihCevirSlashli(row.Cells("ship_date").Value)) Then

                '    sIrsNolar = IIf(sIrsNolar = "", "", sIrsNolar & ",") & row.Cells("IRSNO").Text & "-" & TarihCevirSlashli(row.Cells("ship_date").Value)

                'End If
                If Not sIrsNolar.Contains(row.Cells("IRSNO").Text) Then
                    sIrsNolar = IIf(sIrsNolar = "", "", sIrsNolar & "/") & row.Cells("IRSNO").Text
                End If

            Next

            Dim FaturaTutar As Double = 0

            sFaturaSeri.SeriNo = faturaSeriNo
            sFaturaSeri.FaturaNotu = aciklama

            frmFaturaSeri.ShowDialog()

            If sFaturaSeri.Iptal = True Then

                btnSorgula_Click(sender, e)

                Exit Sub

            End If

            Dim Yazi As String

            'Dim KdvTutar As Double
            Dim kurus As String = ""


            If paraBirimi = "YTL" Or paraBirimi = "TL" Or paraBirimi = "TRY" Then

                paraBirimi = "TL"
                kurus = "KRS"

            ElseIf paraBirimi = "EU" OrElse paraBirimi = "EUR" Then

                paraBirimi = "EUR"
                kurus = "CENT"

            ElseIf paraBirimi = "USD" OrElse paraBirimi = "US" Then

                paraBirimi = "USD"
                kurus = "CENT"

            End If

            Yazi = SayiYazOndalikli(totalInvAmt, paraBirimi, kurus)


            For Each row In checkedRows

                sFatTipi = sTirnakEkle(IIf(row.Cells("Country").ToString = "Turkey", "Y", "I"))
                'faturaNo = "F00000000012"
                sQuery = "INSERT INTO TRM_tmpfatura VALUES(" & sFatTipi & ",'" & faturaNo & "'," & row.Cells("inv_seq").Text & ",'" & row.Cells("do_num").Text & "','" & CDate(row.Cells("inv_date").Text).ToString("yyyy-MM-dd") & "', '" & _
                 row.Cells("SEvkNo").Text & "','" & CDate(row.Cells("ship_date").Text).ToString("yyyy-MM-dd") & "','" & row.Cells("IrsNo").Text & "','" & row.Cells("Cust_num").Text & "'," & _
                 row.Cells("cust_seq").Text & ",'" & row.Cells("name").Text & "','" & row.Cells("Country").Text & "','" & row.Cells("Addr##1").Text & "','" & row.Cells("Addr##2").Text & "'," & _
                 "'" & row.Cells("Addr##3").Text & "','" & row.Cells("Addr##4").Text & "','" & row.Cells("Zip").Text & "','" & row.Cells("City").Text & "','" & row.Cells("terms_code").Text & "'," & _
                 "'" & row.Cells("TermsDescription").Text & "','" & row.Cells("tax_reg_num1").Text & "','" & row.Cells("tax_reg_num2").Text & "',NULL," & CDec(row.Cells("totalInvAmt").Text) & "," & _
                 CDec(row.Cells("totalLineAmt").Text) & "," & CDec(row.Cells("totalTaxAmt").Text) & "," & row.Cells("inv_line").Text & ",'" & row.Cells("Co_num").Text & "'," & row.Cells("Co_line").Text & ",'" & row.Cells("Item").Text & "'," & _
                 "'" & row.Cells("cust_item").Text & "','" & row.Cells("Description").Text & "'," & CDec(row.Cells("qty_shipped").Text) & ",'" & row.Cells("U_M").Text & "'," & CDec(row.Cells("DtyPrice").Text) & _
                 ",'" & row.Cells("curr_code").Text & "'," & CDec(row.Cells("Uf_dovizfiyati").Text) & ",'" & row.Cells("Uf_dovizcinsi").Text & "','" & row.Cells("Itemtax_code").Text & "'," & _
                CDec(row.Cells("tax_rate").Text) & "," & CDec(row.Cells("LineAmt").Text) & ",'" & sFaturaSeri.FaturaNotu & "','" & Yazi & "','" & sSevkNolar & "','" & sIrsNolar & "','" & sFaturaSeri.SeriNo & "'," & _
                CDec(0) & "," & CDec(0) & "," & CDec(row.Cells("totalLineAmt").Text) + CDec(0) & "," & CDec(row.Cells("tax_rate2").Text) & "," & CDec(row.Cells("TotalTaxAmt2").Text) & "," & sTirnakEkle(sIrsNolar) & ")"

                db.RunSql(sQuery)
            Next

            'frmRapor.Parametre.Clear()

            Dim paramFields As New ParameterFields
            paramFields.Clear()
            paramFields = AddParameter("@DoNum", doNum, paramFields)

            'LogOnDatabase("Rapor\SLFatura.rpt")


            'frmRaporOzet.CrystalReportViewer1.ParameterFieldInfo = paramFields
            'frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            'frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = True
            'frmRaporOzet.CrystalReportViewer1.ShowPrintButton = True
            'frmRaporOzet.ShowDialog()

            RaporCagir("SLFatura.rpt", , , "fatura", False, False, , , paramFields)

            sQuery = " Update arinv" &
                     " Set ref =" & sTirnakEkle(sFaturaSeri.SeriNo & " Nolu Fatura") &
                     " ,description=" & sTirnakEkle(sFaturaSeri.SeriNo & " Nolu Fatura") &
                     " Where inv_num in  (Select inv_num " &
                     " From TrM_FaturaPrn_SL10" & _
                     " Where SevkNo IN (" & sSevkNolar & "))"

            db.RunSql(sQuery)

            sQuery = "SELECT country FROM custaddr WHERE cust_num ='" & custNum & "' AND cust_seq=0"
            dt = db.RunSql(sQuery)

            If dt.Rows.Count <> 0 Then

                If dt.Rows(0)("country").ToString.Trim.ToUpper <> "TURKIYE" Then
                    If dt.Rows(0)("country").ToString.Trim <> "Türkiye" Then
                        If dt.Rows(0)("country").ToString.Trim.ToUpper <> "TURKEY" Then
                            Try
                                If custNum.Trim = "12114" Then

                                    sQuery = " Update arinvd" &
                                                    " Set acct ='90099001'" &
                                            " Where inv_num in  (Select inv_num " &
                                            " From TrM_FaturaPrn_SL10" &
                                            " Where SevkNo IN (" & sSevkNolar & "))" &
                                            " and acct like '600%'"

                                    db.RunSql(sQuery)

                                    sQuery = " Update arinvd" &
                                                      " Set acct ='90099001'" &
                                             " Where inv_num in  (Select inv_num " &
                                             " From TrM_FaturaPrn_SL10" &
                                             " Where SevkNo IN (" & sSevkNolar & "))" &
                                             " and acct = '9999'"
                                    db.RunSql(sQuery)

                                Else

                                    sQuery = " Update arinvd" &
                                        " Set acct ='601' + substring(acct,4,len(acct)-3)" &
                                        " Where inv_num in  (Select inv_num " &
                                            " From TrM_FaturaPrn_SL10" &
                                            " Where SevkNo IN (" & sSevkNolar & "))" &
                                            " and acct like '600%'"

                                    db.RunSql(sQuery)

                                End If
                                
                            Catch ex As Exception
                                MessageBox.Show("Hesap Bulunamadığından Muhasebe Kaydı Güncellenememiştir.")
                            End Try
                        End If
                    End If
                End If

            End If

            sQuery = "UPDATE inv_hdr_mst SET Uf_ih_Printed = 1 , Uf_ih_Aciklama = " & sTirnakEkle(sFaturaSeri.FaturaNotu) & ", Uf_FaturaSeriNo = " & sTirnakEkle(sFaturaSeri.SeriNo) & " WHERE inv_num =" & sTirnakEkle(faturaNo)
            db.RunSql(sQuery)

            'Where SHPNO In (" & sSevkNolar & ")"
            sQuery = "Update ShpPack " & _
                        " SET FATURASERINO = " & sTirnakEkle(sFaturaSeri.SeriNo) & _
                        ", FaturaNot=" & sTirnakEkle(sFaturaSeri.FaturaNotu) & _
                    " Where LTRIM(INVNO) = (" & LTrim(faturaNo) & ")"

            db.RunSql(sQuery)

            btnSorgula_Click(sender, e)

        Catch ex As Exception
            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MessageBox.Show(ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub



    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick

        GridSec(GridEX1, "inv_num", sender, e)

    End Sub



    Private Sub frmFaturaYazdirma_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ''TranslateFrm(Me, True)
        dtmSevkTarihi1.Value = Date.Now
        dtmSevkTarihi2.Value = Date.Now
    End Sub

    Private Sub UıButton1_Click(sender As System.Object, e As System.EventArgs) Handles UıButton1.Click
        Try
            ExceleAktar(GridEX1.DataSource)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UıButton2_Click(sender As System.Object, e As System.EventArgs) Handles UıButton2.Click

        Dim checkedRows As Janus.Windows.GridEX.GridEXRow() = Me.GridEX1.GetCheckedRows()
        Dim row As Janus.Windows.GridEX.GridEXRow
        Dim faturaNo As String = ""

        If checkedRows.Length = 0 Then
            MessageBox.Show("Lütfen kayıt seçiniz.")
            Exit Sub
        End If


        Try
            Cursor = Cursors.WaitCursor


            For Each row In checkedRows

                faturaNo = row.Cells("inv_num").Text

                If faturaNo <> row.Cells("inv_num").Text Then
                    MessageBox.Show("Aynı anda sadece tek bir faturayı iptal edebilirsiniz.")
                    Cursor = Cursors.Default
                    Exit Sub
                End If

            Next

            If MessageBox.Show(faturaNo & " nolu faturayı silmek istediğinizden emin misiniz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                sQuery = "select * from arinv where LTRIM(inv_num) = " & sTirnakEkle(faturaNo.Trim)
                dt = db.RunSql(sQuery)

                If dt.Rows.Count = 0 Then

                    MessageBox.Show(faturaNo & " nolu fatura post edilmiş. İptal edilemez.")
                    Cursor = Cursors.Default
                    Exit Sub

                Else

                    sQuery = "exec TR_SKFATSILsp '" & faturaNo.Trim & "'"
                    db.RunSql(sQuery)

                    MessageBox.Show(faturaNo & " nolu fatura iptal edildi.")

                End If
            End If

            btnSorgula_Click(sender, e)

        Catch ex As Exception

            Cursor = Cursors.Default

            MessageBox.Show(ex.Message)

        Finally

            Cursor = Cursors.Default

        End Try

    End Sub
End Class