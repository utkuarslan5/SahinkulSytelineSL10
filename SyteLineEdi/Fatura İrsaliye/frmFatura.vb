Imports CrystalDecisions.Shared

'********************************************
'Date : 11122012
'Name : Rümeyda GÖNÜL
'Form Name : frmFatura
'Main Version : V10.8
'Form Version : V3
'*********************************************

Public Class frmFatura
    Dim sQuery As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)

    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim ds As DataSet

    Dim listFatura As New List(Of structFatura)
    Dim listMalzeme As New List(Of structMalzeme)
    Dim paraBirimi As String = ""
    Dim nkur As Double
    Public kur As Decimal

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick

        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Musteri, Name as Tanim " & _
                            " From CustAddr " & _
                            " Where cust_seq = 0 " & _
                            " ORDER BY 1 "

            FindFormCagir(ssorgu, "Musteri", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

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

            ssorgu = "SELECT Distinct Cust_Num as Musteri, Name as Tanim " & _
                            " From CustAddr " & _
                            " Where cust_seq = 0 " & _
                            " ORDER BY 1 "

            FindFormCagir(ssorgu, "Musteri", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

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
                            " ORDER BY 1"

            FindFormCagir(ssorgu, "Ambar", "Tanım", txtAmbar1.Text, "")

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click

        Try

            Cursor = Cursors.WaitCursor

            sQuery = " Select convert(nvarchar,ship_date,103) ship_date2 , *, 0 InvPrice, 0 Kur From TrM_FaturaMst_SL9 " &
                           " Where 1=1 "

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

                    sQuery = sQuery & "    and shipto='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery = sQuery & "    and shipto>='" & txtTeslimAlan1.Text & "'" & _
                                        " and shipto<='" & txtTeslimAlan2.Text & "'"
                End If

            End If


            If txtKapi1.Text <> "" Then

                If txtKapi2.Text = "" Then

                    sQuery = sQuery & "    and KAPI='" & txtKapi1.Text & "'"

                Else

                    sQuery = sQuery & "    and KAPI>='" & txtKapi1.Text & "'" & _
                                        "    and KAPI<='" & txtKapi2.Text & "'"

                End If

            End If


            If txtSevkNo1.Text <> "" Then

                If txtSevkNo2.Text = "" Then

                    sQuery = sQuery & "    and sevkno=" & txtSevkNo1.Text

                Else

                    sQuery = sQuery & "    and sevkno>=" & txtSevkNo1.Text & "" & _
                                        "    and sevkno<=" & txtSevkNo2.Text & ""

                End If

            End If


            If chkTarihAraligi.Checked Then

                sQuery = sQuery & " and convert(nvarchar(10),ship_date,126) >= " & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd")) & _
                                    " and convert(nvarchar(10),ship_date,126) <= " & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy-MM-dd"))

            Else

                sQuery = sQuery & " and convert(nvarchar(10),ship_date,126) =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd"))

            End If

            If txtKullanici.Text <> "" Then

                sQuery = sQuery & " and Kullanici='" & txtKullanici.Text & "'"

            End If

            'sQuery = sQuery & " Group By sevkno, item,co_num,co_line"

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                Duzenle(GridEX1)
            Else

                GridEX1.DataSource = Nothing


            End If

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default

        End Try

    End Sub

    Private Sub frmFatura_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''TranslateFrm(Me, True)

        txtAmbar1.Text = VarsayilanAmbar
        txtKullanici.Text = String.Empty
        dtpFaturaTarihi.Value = Now
        dtmSevkTarihi1.Value = Now
        dtmSevkTarihi2.Value = Now
        txtFaturaTarihi.Text = ""
        chkGuncelFiyat.Checked = True
        CreateTable()

    End Sub

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim row As Janus.Windows.GridEX.GridEXRow

        checkedRows = Me.GridEX1.GetCheckedRows()

        Dim sSevkNolar As String = ""
        Dim sSevkNolarTirnakli As String = ""

        Dim sIrsNolar As String = ""
        Dim sIrsNolar2 As String = ""
        Dim sFatTipi As String = "Y"
        Dim bGuncelFiyat As Boolean = chkGuncelFiyat.Checked


      
        If checkedRows.Length = 0 Then
            MessageBox.Show("Lütfen kayıt seçiniz.")
            Exit Sub
        End If

        If txtFaturaTarihi.Text = "" Then
            MessageBox.Show("Lütfen Fatura Tarihini Seçiniz!")
            Exit Sub
        End If

        Dim ksKontrol As Integer = 0
        Dim siparisNo As String = ""

        'If txtAmbar1.Text = "GEFC" Then

        '    For Each row In checkedRows

        '        If ksKontrol = 0 Then
        '            siparisNo = row.Cells("co_num").Text
        '        Else
        '            If siparisNo <> row.Cells("co_num").Text Then
        '                MessageBox.Show("Farklı sipariş numaraları birleştirilemez.")
        '                Exit Sub
        '            End If
        '        End If

        '        ksKontrol = ksKontrol + 1

        '    Next

        'End If

        Dim custNum As String = ""
        Dim kdv As String = ""

        For Each row In checkedRows

            If custNum <> "" Then
                If custNum <> row.Cells("cust_num").Text Then
                    MessageBox.Show("Seçilen kayıtlar farklı müşterilere ait.")
                    Exit Sub
                End If
            End If

            If kdv <> "" Then
                If kdv <> row.Cells("ItemTax_Code").Text Then
                    MessageBox.Show("Seçilen kayıtlar farklı kdv tipine ait.")
                    Exit Sub
                End If
            End If

            custNum = row.Cells("cust_num").Text
            kdv = row.Cells("ItemTax_Code").Text

            Dim dtKismi As DataTable
            sQuery = "select * from co_ship_mst where reason_text ='" & row.Cells("SEVKNO").Text & "' and Uf_posted=1"
            dtKismi = db.RunSql(sQuery)

            If dtKismi.Rows.Count > 0 Then
                MessageBox.Show(row.Cells("SEVKNO").Text & " nolu sevk kısmi faturalanmıştır. Lütfen Kısmı Fatura Oluşturma ekranına gidiniz.")
                Exit Sub
            End If
        Next

        Try
            For Each row In checkedRows

                If row.Cells("terms_Code").Text = "" Then
                    MessageBox.Show("Seçmiş olduğunuz kayıtlarda ödeme şekli tanımsız müşteri mevcut.", "Ekip Mapics")
                    Exit Sub
                End If
                If row.Cells("ItemTax_Code").Text = "" Then
                    MessageBox.Show("Seçmiş olduğunuz kayıtlarda vergi kodu tanımsız malzeme mevcut.", "Ekip Mapics")
                    Exit Sub
                End If
                If row.Cells("end_user_type").Text = "" Then
                    MessageBox.Show("Seçmiş olduğunuz kayıtlarda müşteri kategorisi tanımsız müşteri mevcut.", "Ekip Mapics")
                    Exit Sub
                End If

            Next


            Cursor = Cursors.WaitCursor

            For Each row In checkedRows

                If Not sSevkNolar.Contains(row.Cells("SEVKNO").Text) Then

                    sSevkNolar = IIf(sSevkNolar = "", "", sSevkNolar & ",") & row.Cells("SEVKNO").Text

                End If

                If Not sSevkNolarTirnakli.Contains(row.Cells("SEVKNO").Text) Then

                    sSevkNolarTirnakli = IIf(sSevkNolarTirnakli = "", "'", sSevkNolarTirnakli & ",'") & row.Cells("SEVKNO").Text & "'"

                End If

                'If Not sIrsNolar.Contains(row.Cells("IRSNO").Text & "-" & row.Cells("ship_date").Value) Then

                'sIrsNolar = IIf(sIrsNolar = "", "", sIrsNolar & ",") & row.Cells("IRSNO").Text & "-" & row.Cells("ship_date").Value
                If Not sIrsNolar.Contains(row.Cells("IRSNO").Text) Then
                    sIrsNolar = IIf(sIrsNolar = "", "", sIrsNolar & "/") & row.Cells("IRSNO").Text
                End If

            Next


            Dim FaturaNo As Double
            Dim sFaturaNo As String

            FaturaNo = SeriNoAl("FTR")
            sFaturaNo = CStr(FaturaNo).PadLeft(30, "0")
            'sFaturaNo = CStr(FaturaNo).PadLeft(30, " ")

            Dim sLineAmt As Decimal = 0


            For Each row In checkedRows

                If Not chkGuncelFiyat.Checked Then
                    row.Cells("InvPrice").Text = row.Cells("DtyPrice").Text
                Else
                    If row.Cells("Uf_dovizfiyati").Text > 0 Then

                        paraBirimi = row.Cells("Uf_dovizcinsi").Text
                        'KurGetir()

                        sQuery = "select ISNULL(dbo.Tr_KurGetir_Fatura('" & paraBirimi & "',null,'" & CDate(row.Cells("ship_date").Text).ToString("yyyy-MM-dd") & "'," & IIf(row.Cells("lang_code").Text = "TRK", 0, 1) & ") ,1) As Kur"
                        dtTemp = db.RunSql(sQuery)
                        GetRowInfo(nkur, dtTemp, 0, "Kur")

                        row.Cells("Kur").Text = nkur
                        row.Cells("InvPrice").Text = row.Cells("Uf_dovizfiyati").Text * nkur

                    Else
                        row.Cells("InvPrice").Text = row.Cells("cont_price").Text
                    End If
                End If

                'If row.Cells("Uf_dovizfiyati").Text > 0 Then

                '    paraBirimi = row.Cells("Uf_dovizcinsi").Text
                '    KurGetir()
                '    row.Cells("Kur").Text = kur
                '    row.Cells("InvPrice").Text = row.Cells("Uf_dovizfiyati").Text * kur

                'ElseIf row.Cells("Uf_dovizfiyati").Text = 0 And chkGuncelFiyat.Checked Then
                '    row.Cells("InvPrice").Text = row.Cells("cont_price").Text
                'Else
                '    row.Cells("InvPrice").Text = row.Cells("DtyPrice").Text
                'End If

            Next

            For Each row In checkedRows
                If row.Cells("InvPrice").Text = 0 Then
                    If MessageBox.Show("Seçmiş olduğunuz kayıtlarda satış fiyatı sıfır (0) olan ürün mevcut. Devam etmek istiyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            Next

            Dim totalTaxAmt2 As Decimal = 0
            Dim totalTaxAmt As Decimal = 0
            Dim totalDiscTaxAmt As Decimal = 0
            Dim totalLineAmt As Decimal = 0
            Dim totalLineAmtOrj As Decimal = 0
            Dim totalDiscLineAmt As Decimal = 0
            Dim totalInvAmt As Decimal = 0
            Dim discAmount As Decimal = 0
            Dim disc As Decimal = 0
            Dim discOran As Decimal = 0

            disc = 0

            'If txtIskonto.Text <> "" OrElse txtIskonto.Text <> "0" Then
            '    discOran = 1 - (CDec(txtIskonto.Text) / 100)
            'Else
            discOran = 1
            'End If

        
           
            For Each row In checkedRows
                '
                disc = row.Cells("disc").Text

                totalLineAmt = totalLineAmt + Math.Round(CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text) * (CDec(disc) / 100), 2)
                'totalTaxAmt = totalTaxAmt + (Math.Round(CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text), 2) * (CDec(disc) / 100)) * row.Cells("tax_rate").Text
                totalTaxAmt = totalTaxAmt + Math.Round(((CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text)) - (CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text) * (CDec(disc) / 100))), 2) * row.Cells("tax_rate").Text
                totalTaxAmt2 = totalTaxAmt2 + Math.Round(((CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text)) - (CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text) * (CDec(disc) / 100))), 2) * row.Cells("tax_rate2").Text
                'totalLineAmtOrj = totalLineAmtOrj + Math.Round(CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text), 2)
                totalLineAmtOrj = totalLineAmtOrj + Math.Round(((CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text)) - (CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text) * (CDec(disc) / 100))), 2)
                totalDiscLineAmt = totalDiscLineAmt + Math.Round(CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text) * (CDec(disc) / 100), 2)
                totalDiscTaxAmt = totalDiscTaxAmt + (Math.Round(CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text), 2) * (CDec(disc) / 100)) * row.Cells("tax_rate").Text

            Next

            totalLineAmt = Math.Round(totalLineAmt, 2)

            totalTaxAmt2 = Math.Round(totalTaxAmt2 / 100, 2) * -1
            totalTaxAmt = Math.Round(totalTaxAmt / 100, 2)
            totalLineAmtOrj = Math.Round(totalLineAmtOrj, 2)
            'totalLineAmt = Math.Round(totalTaxAmt + totalLineAmtOrj, 2)
            totalDiscLineAmt = Math.Round(totalDiscLineAmt, 2)
            totalDiscTaxAmt = Math.Round(totalDiscTaxAmt / 100, 2)

            totalInvAmt = totalLineAmtOrj + totalTaxAmt - totalTaxAmt2

            For Each row In checkedRows

                sFatTipi = sTirnakEkle(IIf(row.Cells("Country").ToString = "Turkey", "Y", "I"))

                sLineAmt = Math.Round(CDec(row.Cells("qty_shipped").Text * row.Cells("Invprice").Text), 2)

                'kur alma kısmını biz ekledik TR_FaturaKur tablosu için

                'Dim langcode As String
                'sQuery = "select lang_code from customer where cust_num = " & sTirnakEkle(row.Cells("Cust_num").Text) & " and cust_seq"
                'dtTemp = db.RunSql(sQuery)

                'If dtTemp.Rows.Count > 0 Then
                '    langcode = dtTemp.Rows(0)(0)
                'End If



                'sQuery = "select ISNULL(dbo.Tr_KurGetir_Fatura('" & row.Cells("curr_code").Text & "','TL','" & CDate(txtFaturaTarihi.Text).ToString("yyyy-MM-dd") & "'," & IIf(row.Cells("lang_code").Text = "TRK", 0, 1) & "),1) As Kur"
                'dtTemp = db.RunSql(sQuery)
                'GetRowInfo(nkur, dtTemp, 0, "Kur")

                'paraBirimi = row.Cells("Uf_dovizcinsi").Text
                'sQuery = "select ISNULL(dbo.Tr_KurGetir_Fatura('" & paraBirimi & "',null,'" & CDate(row.Cells("ship_date").Text).ToString("yyyy-MM-dd") & "'," & IIf(row.Cells("lang_code").Text = "TRK", 0, 1) & ") ,1) As Kur"
                'dtTemp = db.RunSql(sQuery)
                'GetRowInfo(nkur, dtTemp, 0, "Kur")


                'kur alma kısmını biz ekledik

                sQuery = "INSERT INTO TRM_tmpfatura VALUES(" & sFatTipi & ",0,0,'" & sFaturaNo & "','" & CDate(txtFaturaTarihi.Text).ToString("yyyy-MM-dd") & " " & Now.ToString("HH:mm:ss") & "', '" & _
                    row.Cells("SEvkNo").Text & "','" & CDate(row.Cells("ship_date").Text).ToString("yyyy-MM-dd") & "','" & row.Cells("IrsNo").Text & "','" & row.Cells("Cust_num").Text & "'," & row.Cells("cust_seq").Text & "," & _
                    "'" & row.Cells("name").Text & "','" & row.Cells("Country").Text & "','" & row.Cells("Addr##1").Text & "','" & row.Cells("Addr##2").Text & "','" & row.Cells("Addr##3").Text & "'," & _
                    "'" & row.Cells("Addr##4").Text & "','" & row.Cells("Zip").Text & "','" & row.Cells("City").Text & "','" & row.Cells("terms_code").Text & "','" & row.Cells("TermsDescription").Text & "'," & _
                    "'" & row.Cells("tax_reg_num1").Text & "','" & row.Cells("tax_reg_num2").Text & "',NULL," & totalInvAmt & "," & totalLineAmt & "," & totalTaxAmt & ",0,'" & row.Cells("Co_num").Text & "'," & row.Cells("Co_line").Text & "," & _
                    "'" & row.Cells("Item").Text & "','" & row.Cells("cust_item").Text & "','" & row.Cells("Description").Text & "'," & row.Cells("qty_shipped").Text & ",'" & row.Cells("U_M").Text & "'," & _
                    row.Cells("InvPrice").Text & ",'" & row.Cells("curr_code").Text & "'," & row.Cells("Uf_dovizfiyati").Text & ",'" & row.Cells("Uf_dovizcinsi").Text & "','" & row.Cells("Itemtax_code").Text & "'," & row.Cells("tax_rate").Text & "," & sLineAmt & _
                    ",NULL,NULL,NULL,NULL,NULL"

                If disc <> 0 Then
                    sQuery &= "," & CDec(disc) & "," & CDec(totalDiscLineAmt) & "," & CDec(totalLineAmtOrj) & "," & row.Cells("tax_rate2").Text & "," & totalTaxAmt2 & ")"
                Else
                    sQuery &= ",0,0," & CDec(totalLineAmtOrj) & "," & row.Cells("tax_rate2").Text & "," & totalTaxAmt2 & "," & sTirnakEkle(sIrsNolar) & ")"
                End If


                'sQuery = "INSERT INTO TRM_tmpfatura VALUES(" & sFatTipi & ",0,0,'" & sFaturaNo & "','" & CDate(txtFaturaTarihi.Text).ToString("yyyy-MM-dd") & " " & Now.ToString("HH:mm:ss") & "', '" & _
                '  row.Cells("SEvkNo").Text & "','" & CDate(row.Cells("ship_date").Text).ToString("yyyy-MM-dd") & "','" & row.Cells("IrsNo").Text & "','" & row.Cells("Cust_num").Text & "'," & row.Cells("cust_seq").Text & "," & _
                '  "'" & row.Cells("name").Text & "','" & row.Cells("Country").Text & "','" & row.Cells("Addr##1").Text & "','" & row.Cells("Addr##2").Text & "','" & row.Cells("Addr##3").Text & "'," & _
                '  "'" & row.Cells("Addr##4").Text & "','" & row.Cells("Zip").Text & "','" & row.Cells("City").Text & "','" & row.Cells("terms_code").Text & "','" & row.Cells("TermsDescription").Text & "'," & _
                '  "'" & row.Cells("tax_reg_num1").Text & "','" & row.Cells("tax_reg_num2").Text & "',NULL," & totalInvAmt & "," & totalLineAmt & "," & totalTaxAmt & ",0,'" & row.Cells("Co_num").Text & "'," & row.Cells("Co_line").Text & "," & _
                '  "'" & row.Cells("Item").Text & "','" & row.Cells("cust_item").Text & "','" & row.Cells("Description").Text & "'," & row.Cells("qty_shipped").Text & ",'" & row.Cells("U_M").Text & "'," & _
                '  row.Cells("InvPrice").Text & ",'" & row.Cells("curr_code").Text & "'," & row.Cells("Uf_dovizfiyati").Text & ",'" & row.Cells("Uf_dovizcinsi").Text & "','" & row.Cells("Itemtax_code").Text & "'," & row.Cells("tax_rate").Text & "," & sLineAmt & _
                '  ",NULL,NULL,NULL,NULL,NULL"

                'If disc <> 0 Then
                '    sQuery &= "," & CDec(disc) & "," & CDec(totalDiscLineAmt) & "," & CDec(totalLineAmtOrj) & "," & row.Cells("tax_rate2").Text & "," & totalTaxAmt2 & ")"
                'Else
                '    sQuery &= ",0,0," & CDec(totalLineAmtOrj) & "," & row.Cells("tax_rate2").Text & "," & totalTaxAmt2 & ")"
                'End If

                db.RunSql(sQuery)
            Next


            'Dim Yazi As String

            'Dim KdvTutar As Double

            'KdvTutar = Math.Round(CDbl(((FaturaTutar * KdvOrani) / 100)), 2)

            'FaturaTutar = Math.Round(FaturaTutar, 2) + KdvTutar

            'If sParaBirimi = "YTL" Or sParaBirimi = "TL" Or sParaBirimi = "TRY" Then

            '    sParaBirimi = "TL"
            '    sKurus = "KRS"

            'ElseIf sParaBirimi = "EU" OrElse sParaBirimi = "EUR" Then

            '    sParaBirimi = "EUR"
            '    sKurus = "CENT"

            'ElseIf sParaBirimi = "USD" OrElse sParaBirimi = "US" Then

            '    sParaBirimi = "USD"
            '    sKurus = "CENT"

            'End If

            'Yazi = SayiYazOndalikli(FaturaTutar, sParaBirimi, sKurus)

            'RaporCagir("SLFatura.rpt", , , "SLFATURA", True, True)

            'YAZDIRMA KISMI ----------------------------------------------------------------------
            Dim paramFields As New ParameterFields
            paramFields.Clear()
            paramFields = AddParameter("@DoNum", sFaturaNo, paramFields)

            'If DefaultSite = "faztrade" Then
            '    LogOnDatabase("Rapor\SLFatura_trade.rpt")
            'Else
            'LogOnDatabase("Rapor\SLFatura.rpt")
            'End If

            'frmRaporOzet.CrystalReportViewer1.ParameterFieldInfo = paramFields
            'frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            'frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = False ' True
            'frmRaporOzet.CrystalReportViewer1.ShowPrintButton = False

            'frmRaporOzet.ShowDialog()

            RaporCagir("SLFatura.rpt", , , "fatura", False, False, , , paramFields)

            If MessageBox.Show("Faturayı onaylıyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then

                sQuery = "DELETE FROM TRM_tmpfatura WHERE DoNum = '" & sFaturaNo & "'"
                db.RunSql(sQuery)

                btnSorgula_Click(sender, e)

                Exit Sub

            End If

            '-------------------------------------------------------------------------------------



            '*************** Coitem co_ship Fiyat Güncelleme

            For Each row In checkedRows

                sQuery = " Update coitem" & _
                                   " Set Price_conv=" & row.Cells("InvPrice").Text & _
                                   " , price=" & row.Cells("InvPrice").Text & _
                          " Where co_num=" & sTirnakEkle(row.Cells("Co_num").Text) & _
                          " And co_line=" & row.Cells("Co_line").Text & _
                          " And item=" & sTirnakEkle(row.Cells("Item").Text)

                db.RunSql(sQuery)

                '" , Uf_dovizcinsi = '" & row.Cells("Uf_dovizcinsi").Text & "'" & _
                '       " , Uf_dovizfiyati = " & row.Cells("Uf_dovizfiyati").Text & _
                '       " , Uf_Kur = " & row.Cells("Kur").Text & _
                '       " , Uf_DoNum = '" & sFaturaNo & "'" & _

                paraBirimi = row.Cells("Uf_dovizcinsi").Text
                sQuery = "select ISNULL(dbo.Tr_KurGetir_Fatura('" & paraBirimi & "',null,'" & CDate(row.Cells("ship_date").Text).ToString("yyyy-MM-dd") & "'," & IIf(row.Cells("lang_code").Text = "TRK", 0, 1) & ") ,1) As Kur"
                dtTemp = db.RunSql(sQuery)
                GetRowInfo(nkur, dtTemp, 0, "Kur")

                sQuery = " Update co_ship" & _
                                   " Set Price=" & row.Cells("InvPrice").Text & _
                                   " , Uf_Kur = " & nkur & _
                                   " , Uf_dovizcinsi = " & sTirnakEkle(paraBirimi) & _
                            " Where co_num=" & sTirnakEkle(row.Cells("Co_num").Text) & _
                            " And co_line=" & row.Cells("Co_line").Text & _
                            " And reason_text=" & sTirnakEkle(row.Cells("SEvkNo").Text) & _
                            " And shipment_id=" & sTirnakEkle(row.Cells("SEvkNo").Text)

                db.RunSql(sQuery)

            Next

            ''********** Do_line , Do_Hdr, Do_Seq Insert işlemi yapılıyor



            sQuery = "Update ShpPack" & _
                        " Set FatNo=" & FaturaNo & _
                        " , FatDrm=1" & _
                        " , FATURASERINO = " & sTirnakEkle(sFaturaSeri.SeriNo) & _
                        " , FaturaNot=" & sTirnakEkle(sFaturaSeri.FaturaNotu) & _
                    " Where SHPNO In (" & sSevkNolar & ")"

            db.RunSql(sQuery)

            sQuery = "Update ShpDty" & _
                        " Set DO_NUM=" & sTirnakEkle(sFaturaNo) & _
                    " Where SHPNO In (" & sSevkNolar & ")"

            db.RunSql(sQuery)


            Dim FaturaSiraNo As Integer
            Dim ToplamIndirimTutari As Decimal = 0
            'listFatura.Clear()

            sQuery = "Select cust_num, cust_seq,  co_num, co_line, co_release, qty_shipped,ROUND(dtyPrice * qty_shipped,2) As Tutar, ship_date, date_seq , SevkNo," & _
                        " terms_code, ship_code, ItemTax_Code, item, dtyPrice, description, u_m,tax_rate ,curr_code" & _
                        " From TrM_FaturaMst_SL9" & _
                        " Where SEVKNO IN (" & sSevkNolar & ")"

            dtTemp = db.RunSql(sQuery)

            If Not dtTemp Is Nothing AndAlso _
                dt.Rows.Count > 0 Then

                Try
                    db.BeginTransaction()

                    For y As Integer = 0 To dtTemp.Rows.Count - 1

                        sQuery = "Delete" & _
                                  " from con_inv_item" & _
                                  " Where not exists  (Select 1" & _
                                                     " From con_inv_hdr h" & _
                                                      " where h.cust_num = con_inv_item.cust_num " & _
                                                     " and h.con_inv_seq = con_inv_item.con_inv_seq)"

                        db.RunSql(sQuery)

                        sQuery = "Delete" & _
                                  " from con_inv_line" & _
                                  " Where not exists  (Select 1" & _
                                                     " From con_inv_hdr h" & _
                                                      " where h.cust_num = con_inv_line.cust_num " & _
                                                     " and h.con_inv_seq = con_inv_line.con_inv_seq)"

                        db.RunSql(sQuery)


                        With dtTemp.Rows(y)

                            If y = 0 Then
                                'Dim plaka As String
                                'If .Item("Plaka").ToString = "" Then
                                '    plaka = ""
                                'ElseIf .Item("Plaka").ToString.Length > 15 Then
                                '    plaka = .Item("Plaka").ToString.Substring(0, 15)
                                'Else
                                '    plaka = .Item("Plaka").ToString
                                'End If

                                sQuery = "Insert into Do_Hdr" & _
                                            " (do_num, do_hdr_date, stat, cust_num, cust_seq, pickup_date, curr_code  ) " & _
                                            " Values (" & _
                                            sTirnakEkle(sFaturaNo) & "," & _
                                            "getdate()" & "," & _
                                            sTirnakEkle("A") & "," & _
                                            sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                            .Item("cust_seq").ToString & "," & _
                                            "getdate()" & "," & _
                                            sTirnakEkle(.Item("curr_code").ToString) & ")"

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then
                                    Throw New Exception(db.Result.GetMessages)
                                End If

                                FaturaSiraNo = nLookup("Max(con_inv_seq)", "con_inv_hdr", " cust_num=" & sTirnakEkle(.Item("cust_num").ToString)) + 1
                                'XXX
                                'sTirnakEkle(.Item("ship_code").ToString) & "," & _
                                'sQuery = "Insert into con_inv_hdr" & _
                                '        " (cust_num, con_inv_seq, do_invoice, do_num ," & _
                                '        " terms_code, frt_tax_code1, msc_tax_code1,shipment_id,curr_code) " & _
                                '        " Values (" & _
                                '        sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                '        FaturaSiraNo & "," & _
                                '        sTirnakEkle("C") & "," & _
                                '        sTirnakEkle(sFaturaNo) & "," & _
                                '        sTirnakEkle(.Item("terms_code").ToString) & "," & _
                                '        sTirnakEkle(.Item("ItemTax_Code").ToString) & "," & _
                                '        sTirnakEkle(.Item("ItemTax_Code").ToString) & "," &
                                '        .Item("SevkNo").ToString & "," & _
                                '        sTirnakEkle(.Item("curr_code").ToString) & ")"
                                sQuery = "Insert into con_inv_hdr" & _
                               " (cust_num, con_inv_seq, do_invoice, do_num ," & _
                               " terms_code, frt_tax_code1, msc_tax_code1,curr_code) " & _
                               " Values (" & _
                               sTirnakEkle(.Item("cust_num").ToString) & "," & _
                               FaturaSiraNo & "," & _
                               sTirnakEkle("C") & "," & _
                               sTirnakEkle(sFaturaNo) & "," & _
                               sTirnakEkle(.Item("terms_code").ToString) & "," & _
                               sTirnakEkle(.Item("ItemTax_Code").ToString) & "," & _
                               sTirnakEkle(.Item("ItemTax_Code").ToString) & "," &
                               sTirnakEkle(.Item("curr_code").ToString) & ")"

                                db.RunSql(sQuery)


                                If db.Result.ReturnValue = False Then
                                    Throw New Exception(db.Result.GetMessages)
                                End If


                                'XXX
                                sQuery = "Insert into Do_Line" & _
                                            " (do_num, do_line  ) " & _
                                            " Values (" & _
                                            sTirnakEkle(sFaturaNo) & "," & _
                                            "1" & ")"

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then
                                    Throw New Exception(db.Result.GetMessages)
                                End If

                            End If
                            'XXX
                            sQuery = "Insert into Do_Seq" & _
                                            " (do_num, do_line, do_seq, ref_type, ref_num, " & _
                                            " ref_line , ref_release, ship_date, date_seq) " & _
                                            " Values (" & _
                                            sTirnakEkle(sFaturaNo) & "," & _
                                            "1" & "," & _
                                            y + 1 & "," & _
                                            sTirnakEkle("O") & "," & _
                                            sTirnakEkle(.Item("co_num").ToString) & "," & _
                                            .Item("co_line").ToString & "," & _
                                            .Item("co_release").ToString & "," & _
                                            sTirnakEkle(CDate(.Item("ship_date").ToString).ToString("yyyy-MM-dd HH:mm:ss")) & "," & _
                                            .Item("date_seq").ToString & ")"

                            db.RunSql(sQuery)


                            If db.Result.ReturnValue = False Then
                                Throw New Exception(db.Result.GetMessages)
                            End If
                            'XXX

                            sQuery = "Insert into con_inv_item" & _
                                    " (cust_num, con_inv_seq, inv_line, co_num, co_line, " & _
                                    " co_release, qty_to_invoice, ext_price) " & _
                                    " Values (" & _
                                    sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                    FaturaSiraNo & "," & _
                                    y + 1 & "," & _
                                    sTirnakEkle(.Item("co_num").ToString) & "," & _
                                    .Item("co_line").ToString & "," & _
                                    .Item("co_release").ToString & "," & _
                                    .Item("qty_shipped").ToString & "," & _
                                    .Item("Tutar").ToString & ")"

                            db.RunSql(sQuery)


                            If db.Result.ReturnValue = False Then
                                Throw New Exception(db.Result.GetMessages)
                            End If
                            'XXX
                            sQuery = "Insert into con_inv_line" & _
                                    " (do_num, do_line, do_seq, cust_num, con_inv_seq, inv_line, item, description, " & _
                                    " price, u_m, drop_cust_num," & _
                                    " drop_cust_seq ,qty_to_invoice ,ext_price ) " & _
                                    " Values (" & _
                                    sTirnakEkle(sFaturaNo) & "," & _
                                     "1" & "," & _
                                     y + 1 & "," & _
                                    sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                    FaturaSiraNo & "," & _
                                    y + 1 & "," & _
                                    sTirnakEkle(.Item("item").ToString) & "," & _
                                    sTirnakEkle(.Item("description").ToString) & "," & _
                                    .Item("dtyPrice").ToString & "," & _
                                    sTirnakEkle(.Item("u_m").ToString) & "," & _
                                    sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                    .Item("cust_seq").ToString & "," & _
                                    .Item("qty_shipped").ToString & "," & _
                                    .Item("Tutar").ToString & ")"

                            db.RunSql(sQuery)

                            If db.Result.ReturnValue = False Then
                                Throw New Exception(db.Result.GetMessages)
                            End If


                        End With

                    Next y
                    'XXX

                    If db.Result.ReturnValue = False Then
                        Throw New Exception(db.Result.GetMessages)
                    End If

                    If db.Transaction Then
                        db.CommitTransaction()
                    End If


                Catch ex As Exception

                    If db.Transaction Then
                        db.RollbackTransaction()
                    End If

                    'MessageBox.Show(ex.Message)
                    MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally

                    Cursor = Cursors.Default
                    CreateTable()
                End Try

            End If

            '***********************************Post İşlemi

            sQuery = "update co_ship_mst set shipment_id = null where do_num = " & sTirnakEkle(sFaturaNo)
            db.RunSql(sQuery)

            sQuery = "UPDATE con_inv_item SET regen = 0 WHERE cust_num = " & sTirnakEkle(custNum) & ""
            db.RunSql(sQuery)

            sFaturaNo = CStr(FaturaNo).PadLeft(30, "0")

            Try
                sQuery = " exec setsitesp 'Default',0 " & vbNewLine & _
                 " Exec FaturaPost_Kons_SL10 " & _
                           "@Musteri=" & sTirnakEkle(custNum) & "," &
                           "@DoNum=" & sTirnakEkle(sFaturaNo) & "," &
                           "@FaturaTarihi=" & sTirnakEkle(dtpFaturaTarihi.Value.ToString("yyyy-MM-dd"))

                dt = db.RunSql(sQuery)

                'sFaturaNo = CStr(FaturaNo).PadLeft(10, " ")
                sQuery = "DELETE FROM TRM_tmpfatura WHERE DoNum = '" & sFaturaNo & "'"
                db.RunSql(sQuery)

                sQuery = "update co_ship_mst set shipment_id = reason_text, uf_donum = do_num where do_num = " & sTirnakEkle(sFaturaNo)
                db.RunSql(sQuery)

                sQuery = "update inv_hdr set Uf_ih_Aciklama = " & sTirnakEkle(sFaturaSeri.FaturaNotu) & ",  Uf_FaturaSeriNo = " & sTirnakEkle(sFaturaSeri.SeriNo) & " where do_num = " & sTirnakEkle(sFaturaNo)
                db.RunSql(sQuery)

            Catch ex As Exception
                FaturaIptal(sSevkNolar, sSevkNolarTirnakli, custNum)
                Throw New Exception("Fatura Post İşleminde Hata Oluştu" & vbNewLine & ex.Message)
                btnSorgula_Click(sender, e)
                Exit Sub
            End Try


            Dim sInvNo As String = ""

            Dim sInvHata As String = ""

            GetRowInfo(sInvNo, dt, 0, "FaturaNo")
            GetRowInfo(sInvHata, dt, 0, "Hata")


            If sInvNo = "0" Then

                sQuery = "update co_ship_mst set shipment_id = null where do_num = " & sTirnakEkle(sFaturaNo)
                db.RunSql(sQuery)

                sQuery = "UPDATE con_inv_item SET regen = 0 WHERE cust_num = " & sTirnakEkle(custNum) & ""
                db.RunSql(sQuery)

                sFaturaNo = CStr(FaturaNo).PadLeft(30, " ")

                Try
                    sQuery = " exec setsitesp 'Default',0 " & vbNewLine & _
                       " Exec FaturaPost_Kons_SL10 " & _
                          "@Musteri=" & sTirnakEkle(custNum) & "," &
                          "@DoNum=" & sTirnakEkle(sFaturaNo) & "," &
                          "@FaturaTarihi=" & sTirnakEkle(dtpFaturaTarihi.Value.ToString("yyyy-MM-dd"))

                    dt = db.RunSql(sQuery)

                    sQuery = "DELETE FROM TRM_tmpfatura WHERE DoNum = '" & sFaturaNo & "'"
                    db.RunSql(sQuery)

                    sQuery = "update co_ship_mst set shipment_id = reason_text where do_num = " & sTirnakEkle(sFaturaNo)
                    db.RunSql(sQuery)

                    sQuery = "update inv_hdr set Uf_ih_Aciklama = " & sTirnakEkle(sFaturaSeri.FaturaNotu) & " where do_num = " & sTirnakEkle(sFaturaNo)
                    db.RunSql(sQuery)

                Catch ex As Exception
                    FaturaIptal(sSevkNolar, sSevkNolarTirnakli, custNum)
                    Throw New Exception("Fatura Post İşleminde Hata Oluştu" & vbNewLine & ex.Message)
                    btnSorgula_Click(sender, e)
                    Exit Sub
                End Try

                GetRowInfo(sInvNo, dt, 0, "FaturaNo")
                GetRowInfo(sInvHata, dt, 0, "Hata")
            End If

            If sInvNo = "0" Then
                Cursor = Cursors.Default

                Try
                    FaturaIptal(sSevkNolar, sSevkNolarTirnakli, custNum)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                Throw New Exception("Fatura Post İşleminde Hata Oluştu" & vbNewLine & sInvHata)
                btnSorgula_Click(sender, e)
                Exit Sub
            End If

            'sQuery = "select * from inv_hdr where inv_num = " & sTirnakEkle(sInvNo)
            'dt = db.RunSql(sQuery)

            'If dt.Rows.Count = 0 Then

            '    Try
            '        FaturaIptal(sSevkNolar, sSevkNolarTirnakli, custNum)
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try

            '    Throw New Exception("Fatura Post İşleminde Hata Oluştu" & vbNewLine & sInvHata)
            '    btnSorgula_Click(sender, e)
            '    Exit Sub
            'End If

            sQuery = "Update ShpPack" & _
                        " Set Invno=" & sInvNo & _
                        " , InvDrm=1" & _
                    " Where SHPNO In (" & sSevkNolar & ")"

            db.RunSql(sQuery)

            sQuery = "Insert Into TR_FaturaKur" & _
                                   " (FaturaNo, Kur)" & _
                                   " Values ('" & _
                                    sInvNo & "'," & _
                                   nKur & ")"

            db.RunSql(sQuery)



            MessageBox.Show(sInvNo & " Nolu Fatura Post Edildi...")


            sQuery = "Update co_ship_mst set Uf_posted = 1 , Uf_FaturaNo = " & sTirnakEkle(sInvNo) & " WHERE do_num = " & sTirnakEkle(sFaturaNo)
            db.RunSql(sQuery)



        Catch ex As Exception

            'FaturaIptal(sSevkNolar, sSevkNolarTirnakli, custNum)
            MessageBox.Show("Fatura Oluşturulurken Hata Oluştu" & vbNewLine & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Cursor = Cursors.Default

            dtpFaturaTarihi.Value = Now
            txtFaturaTarihi.Text = ""
            btnSorgula_Click(sender, e)
        End Try

    End Sub

    Private Sub FaturaIptal(ByVal sevkNolar As String, ByVal sevkNolarTirnakli As String, ByVal musteri As String)

        Dim do_num As String

        sQuery = "SELECT Uf_Donum FROM co_ship_mst WHERE shipment_id IN (" & sevkNolar & ")"
        do_num = db.RunSql(sQuery).Rows(0)(0)

        If do_num = "" Then
            Exit Sub
        End If

        sQuery = "UPDATE co_ship_mst SET Uf_FaturaNo = 0, Uf_posted = 0 , Uf_DoNum = Null WHERE shipment_id IN (" & sevkNolar & ")"
        db.RunSql(sQuery)

        sQuery = "DELETE FROM con_inv_line WHERE do_num LIKE '%" & do_num & "'"
        db.RunSql(sQuery)

        sQuery = "DELETE FROM con_inv_item WHERE cust_num = " & sTirnakEkle(musteri) & _
        " AND  con_inv_seq = (SELECT con_inv_seq FROM con_inv_hdr WHERE do_num LIKE '%" & do_num & "' AND cust_num = " & sTirnakEkle(musteri) & ")"
        db.RunSql(sQuery)

        sQuery = "DELETE FROM con_inv_hdr WHERE do_num LIKE '%" & do_num & "'"
        db.RunSql(sQuery)

        sQuery = "DELETE FROM do_line WHERE  do_num LIKE '%" & do_num & "'"
        db.RunSql(sQuery)

        sQuery = "DELETE FROM do_seq WHERE  do_num LIKE '%" & do_num & "'"
        db.RunSql(sQuery)

        sQuery = "DELETE FROM do_hdr WHERE do_num LIKE '%" & do_num & "'"
        db.RunSql(sQuery)

        'sQuery = "Select cust_num,  co_num, co_line, co_release " & _
        '                   " From TrM_FaturaMst" & _
        '                   " Where SEVKNO IN (" & sevkNolar & ")"

        'dtTemp = db.RunSql(sQuery)

        'If Not dtTemp Is Nothing Then

        '    For y As Integer = 0 To dtTemp.Rows.Count - 1
        '        With dtTemp.Rows(y)

        '            sQuery = "DELETE FROM con_inv_item WHERE cust_num = " & sTirnakEkle(.Item("cust_num").ToString) & _
        '                " AND  co_num = " & sTirnakEkle(.Item("co_num").ToString) & _
        '                " AND co_line = " & .Item("co_line").ToString & " AND co_release = " & .Item("co_release").ToString
        '            db.RunSql(sQuery)

        '        End With
        '    Next

        'End If

        'sQuery = "UPDATE TRM_SHPDTY SET do_num = NULL WHERE SHPNO IN (" & sevkNolar & ")"
        'db.RunSql(sQuery)

    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick

        If txtAmbar1.Text = "GEFC" Then
            GridSec(GridEX1, "co_num", sender, e)
        Else
            GridSec(GridEX1, "SevkNo", sender, e)
        End If

    End Sub

    Private Sub dtpFaturaTarihi_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFaturaTarihi.ValueChanged
        txtFaturaTarihi.Text = CDate(dtpFaturaTarihi.Value).ToString("dd.MM.yyyy")
    End Sub

    Private Sub CreateTable()

        ds = Nothing
        Dim dtFatura As New DataTable("Fatura")

        dtFatura.Columns.Add("Sevk No")
        dtFatura.Columns.Add("Müşteri")
        dtFatura.Columns.Add("Müşteri Adı")
        'dtFatura.Columns.Add("Plant")
        'dtFatura.Columns.Add("Kapı")
        dtFatura.Columns.Add("İrsaliye No")
        dtFatura.Columns.Add("Sevk Tarihi")
        dtFatura.Columns.Add("Malzeme")
        dtFatura.Columns.Add("Müşteri Malzeme Kodu")
        dtFatura.Columns.Add("Tanım")
        dtFatura.Columns.Add("Sevk Miktarı")
        dtFatura.Columns.Add("Ölçü Birimi")
        dtFatura.Columns.Add("Sipariş Fiyatı")
        dtFatura.Columns.Add("Güncel Fiyat")
        dtFatura.Columns.Add("Para Birimi")
        dtFatura.Columns.Add("Koşul")
        dtFatura.Columns.Add("Kdv")
        dtFatura.Columns.Add("Ülke")
        'dtFatura.Columns.Add("Pus No")
        'dtFatura.Columns.Add("Sefer No")
        'dtFatura.Columns.Add("Navlun No")

        ds = New DataSet
        ds.Tables.Add(dtFatura)

    End Sub

    Private Sub btnExceleAktar_Click(sender As System.Object, e As System.EventArgs) Handles btnExceleAktar.Click

        Try


            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            ' Dim row As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()


            If checkedRows.Length = 0 Then
                MessageBox.Show("Seçim yapmadan fatura bilgilerini excele aktaramazsınız.")
                Exit Sub
            End If

            CreateTable()
            ds.Tables("Fatura").Rows.Clear()

            For Each row As Janus.Windows.GridEX.GridEXRow In checkedRows

                Dim dr As DataRow
                dr = ds.Tables("Fatura").NewRow

                dr("Sevk No") = row.Cells("SevkNo").Value
                dr("Müşteri") = row.Cells("Cust_num").Value
                dr("Müşteri Adı") = row.Cells("name").Value
                'dr("Plant") = row.Cells("Shipto").Value
                'dr("Kapı") = row.Cells("KAPI").Value
                dr("İrsaliye No") = row.Cells("IRSNO").Value
                dr("Sevk Tarihi") = row.Cells("ship_date").Value
                dr("Malzeme") = row.Cells("Item").Value
                dr("Müşteri Malzeme Kodu") = row.Cells("cust_item").Value
                dr("Tanım") = row.Cells("Description").Value
                dr("Sevk Miktarı") = row.Cells("qty_shipped").Value
                dr("Ölçü Birimi") = row.Cells("u_m").Value
                dr("Sipariş Fiyatı") = row.Cells("DtyPrice").Value
                dr("Güncel Fiyat") = row.Cells("cont_price").Value
                dr("Para Birimi") = row.Cells("curr_code").Value
                dr("Koşul") = row.Cells("terms_code").Value
                dr("Kdv") = row.Cells("Itemtax_code").Value
                dr("Ülke") = row.Cells("Country").Value
                'dr("Pus No") = row.Cells("Pusno").Value
                ' dr("Sefer No") = row.Cells("SeferNo").Value
                ' dr("Navlun No") = row.Cells("NavlunNo").Value

                ds.Tables("Fatura").Rows.Add(dr)

            Next

            ExceleAktar(ds.Tables("Fatura"))

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Dim dt2 As New DataTable
        'For Each row In checkedRows
        '    dt2.Rows.Add(row)
        'Next
        'ExceleAktar(dt2)
        'ExceleAktar(GridEX1.DataSource)
    End Sub

    Private Sub KurGetir()

        Dim ds As DataSet
        Dim squery As String

        squery = " Select buy_rate " & _
                       " from currate " & _
                    " Where from_curr_code='" & paraBirimi & "'" & _
                    " and  CONVERT(nvarchar(10), eff_date, 126)<='" & dtpFaturaTarihi.Value.ToString("yyyy-MM-dd") & "'" &
                    " Order By eff_date DESC"

        ds = db.RunSql(squery, "currate")

        If Not ds Is Nothing And ds.Tables("currate").Rows.Count.ToString > 0 Then
            kur = ds.Tables("currate").Rows(0)(0).ToString

        Else
            kur = 1
        End If

    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class