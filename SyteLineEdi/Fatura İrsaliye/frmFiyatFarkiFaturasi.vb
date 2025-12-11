Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmFiyatFarkiFaturasi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim db2 As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

     

            If txtMusteri1.Text = "" Then
                MessageBox.Show("Lütfen Müşteri Seçiniz !")
                Exit Sub
            End If


            'sQuery = "SELECT  a.*, " & _
            ' "  ( a.cont_price - a.eski_fiyat  ) AS fark_fiyati, " & _
            ' "    a.qty_invoiced * ( a.cont_price - a.eski_fiyat ) AS tutar " & _
            ' "  from    ( SELECT inv.CreateDate, inv.inv_line, inv.co_num, " & _
            ' "     inv.co_line, " & _
            ' "     inv.item, " & _
            ' "     itm.DESCRIPTION, " & _
            ' "     itm.u_m, " & _
            ' "     inv.inv_num,  " & _
            ' "     hdr.inv_date, " & _
            ' "     cst.cust_num, " & _
            ' "     cst.NAME, " & _
            ' "     shp.IRSNO  AS irsno, " & _
            ' "     ISNULL(inv.qty_invoiced, 0) qty_invoiced, " & _
            ' "     cst.curr_code, " & _
            ' "     isnull( ( CASE WHEN inv.Uf_Son_Fiyat IS NULL " & _
            ' "            THEN ( CASE WHEN ISNULL(inv.qty_invoiced, 0) = 0 " & _
            ' "                        THEN 0 " & _
            ' "                       ELSE ISNULL(inv.price, 0)  END ) " & _
            ' "            ELSE ISNULL(Uf_Son_Fiyat, 0) " & _
            ' "       END ),0) AS eski_fiyat, "

            'sQuery &= " (SELECT ISNULL(kont.Uf_DovizFiyati * dbo.Tr_KurGetir_Fatura(kont.Uf_DovizCinsi, NULL," &
            '            " hdr.ship_date, CASE WHEN c.lang_code = 'TRK' THEN 0 ELSE 1 END), kont.cont_price)" &
            '            " FROM dbo.Tr_KontratFiyati(hdr.cust_num,inv.item,ic.cust_item, hdr.ship_date) kont)  As cont_price" &
            '            " FROM      dbo.inv_item inv " & _
            '            "    LEFT JOIN dbo.item itm ON inv.item = itm.item " & _
            '            "    LEFT JOIN dbo.inv_hdr hdr ON inv.inv_num = hdr.inv_num " & _
            '            "    LEFT JOIN dbo.customer c ON c.cust_num = hdr.cust_num" &
            '            "    LEFT JOIN dbo.custaddr cst ON hdr.cust_num = cst.cust_num " & _
            '            "                              and hdr.cust_seq = cst.cust_seq " & _
            '            "    LEFT JOIN dbo.itemcust ic ON ic.item = itm.item " &
            '            "                             AND ic.cust_num = cst.cust_num  " &
            '            "    LEFT JOIN dbo.SHPPACK shp ON inv.inv_num   = shp.FATNO  " & _
            '            " ) a " & _
            '            " where 1=1" '& _

            'If chkTarihAraligi.Checked Then

            '    sQuery = sQuery & " and dbo.Dateserial(datepart(Year,a.CreateDate),datepart(Month,a.CreateDate),datepart(Day,a.CreateDate)) >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd")) & _
            '                        " and dbo.Dateserial(datepart(Year,a.CreateDate),datepart(Month,a.CreateDate),datepart(Day,a.CreateDate)) <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy/MM/dd"))

            'Else

            '    sQuery = sQuery & " and dbo.Dateserial(datepart(Year,a.CreateDate),datepart(Month,a.CreateDate),datepart(Day,a.CreateDate)) =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd"))

            'End If

            'If txtMusteri1.Text <> "" Then

            '    sQuery = sQuery & "    and a.cust_num='" & txtMusteri1.Text & "'"

            'End If


            sQuery = "SELECT  a.*, " & _
             "  ( a.cont_price - a.eski_fiyat  ) AS fark_fiyati, " & _
             "    a.qty_invoiced * ( a.cont_price - a.eski_fiyat ) AS tutar " & _
             "  from    ( SELECT inv.CreateDate, inv.inv_line, inv.co_num, " & _
             "     inv.co_line, " & _
             "     inv.item, " & _
             "     itm.DESCRIPTION, " & _
             "     itm.u_m, " & _
             "     inv.inv_num,  " & _
             "     hdr.inv_date, " & _
             "     cst.cust_num, " & _
             "     cst.NAME, " & _
             "     ( select top 1 shp.IRSNO from  dbo.SHPPACK shp where  inv.inv_num   = shp.INVNO )  AS irsno," & _
             "     ISNULL(inv.qty_invoiced, 0) qty_invoiced, " & _
             "     cst.curr_code, " & _
             "     isnull( ( CASE WHEN inv.Uf_Son_Fiyat IS NULL " & _
             "            THEN ( CASE WHEN ISNULL(inv.qty_invoiced, 0) = 0 " & _
             "                        THEN 0 " & _
             "                       ELSE ISNULL(inv.price, 0)  END ) " & _
             "            ELSE ISNULL(Uf_Son_Fiyat, 0) " & _
             "       END ),0) AS eski_fiyat, (SELECT ISNULL(kont.Uf_DovizFiyati * dbo.Tr_KurGetir_Fatura(kont.Uf_DovizCinsi, NULL," &
                        " hdr.ship_date, CASE WHEN c.lang_code = 'TRK' THEN 0 ELSE 1 END), kont.cont_price)" &
                        " FROM dbo.Tr_KontratFiyati(hdr.cust_num,inv.item, (select top 1 cust_item from itemcust ic where ic.item = inv.item  and ic.cust_num= hdr.cust_num  ), hdr.ship_date) kont)  As cont_price " &
                        " FROM      dbo.inv_item inv " & _
                        "    LEFT JOIN dbo.item itm ON inv.item = itm.item " & _
                        "    INNER JOIN dbo.inv_hdr hdr ON inv.inv_num = hdr.inv_num " & _
                        "    LEFT JOIN dbo.customer c ON c.cust_num = hdr.cust_num and c.cust_seq = hdr.cust_seq " &
                        "    LEFT JOIN dbo.custaddr cst ON hdr.cust_num = cst.cust_num and hdr.cust_seq = cst.cust_seq " & _
                        " ) a " & _
                        " where 1=1" '& _

            If chkTarihAraligi.Checked Then

                sQuery = sQuery & " and dbo.Dateserial(datepart(Year,inv_date),datepart(Month,inv_date),datepart(Day,inv_date)) >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd")) & _
                                    " and dbo.Dateserial(datepart(Year,inv_date),datepart(Month,inv_date),datepart(Day,inv_date)) <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy/MM/dd"))

            Else

                sQuery = sQuery & " and dbo.Dateserial(datepart(Year,inv_date),datepart(Month,inv_date),datepart(Day,inv_date)) =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd"))

            End If

            If txtMusteri1.Text <> "" Then

                sQuery = sQuery & "    and a.cust_num='" & txtMusteri1.Text & "'"

            End If



            Application.DoEvents()
            dt = db.RunSql(sQuery)
            Application.DoEvents()

            GridEX1.CheckAllRecords()

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                Duzenle(GridEX1)

                TutarToplamHesapla()
            Else

                GridEX1.DataSource = Nothing

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnYazdır_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdır.Click
        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim row As Janus.Windows.GridEX.GridEXRow

        checkedRows = Me.GridEX1.GetCheckedRows()

        Dim InvNum As String = String.Empty

        Try

            Dim gKonu As String = ""
            Dim gParabirimi As String = ""
            ' Dim gKur As String = ""
            Dim gMiktar As Decimal 'String = ""
            Dim gBirimFiyat As Decimal 'As String = ""
            Dim gTutar As String = ""
            Dim kurSatiri As String
            Dim fNo As String = ""
            Dim mNo As String = ""
            'Dim fAciklama As String = ""
            Dim param, RptParam As New ArrayList
            Dim yazdir As New frmRapor
            Dim kur As String

            Dim hesapno As String

            If checkedRows.Length = 0 Then
                MessageBox.Show("Yazdırılacak Veri Bulunamadı !")
                Exit Sub
            End If

            If checkedRows.Length > 0 Then

                'For Each row In checkedRows
                '  If IIf(row.Cells("tutar").Text = String.Empty, 0, CDec(row.Cells("tutar").Text)) = 0 Then
                '    MessageBox.Show("Lütfen Tutarı Sıfır Olan Satır Seçmeyiniz!")
                '    Exit Sub
                '  End If
                'Next

                Windows.Forms.Cursor.Current = Cursors.WaitCursor

                'Dim InvNum As String = String.Empty
                Dim Infobar As String = String.Empty

                'Dim param As New ArrayList
                Dim paramout As New ArrayList

                param.Clear()
                paramout.Clear()

                param.Add(New SqlClient.SqlParameter("@Custnum", txtMusteri1.Text))
                param.Add(New SqlClient.SqlParameter("@InvDate", Now.Date.ToString("yyyy-MM-dd")))
                param.Add(New SqlClient.SqlParameter("@Type", "I"))
                param.Add(New SqlClient.SqlParameter("@Action", "NextNum"))

                Dim p4 As New SqlClient.SqlParameter("@InvNum", SqlDbType.NVarChar, 12)
                p4.Direction = ParameterDirection.Output
                param.Add(p4)

                Dim p5 As New SqlClient.SqlParameter("@Infobar", SqlDbType.NVarChar, 2800)
                p5.Direction = ParameterDirection.Output
                param.Add(p5)

                db.RunSp("[NextInvNumSp]", param, paramout)

                InvNum = paramout(0).ToString

                Infobar = paramout(1).ToString

                'If IIf(IsDBNull(p5.Value), "", p5.Value) <> "" Then

                '    MessageBox.Show(p5.Value, "Bilgilendirme", MessageBoxButtons.OK)

                'End If

                If InvNum = String.Empty OrElse Infobar <> String.Empty Then
                    MessageBox.Show(Infobar, "Bilgilendirme", MessageBoxButtons.OK)
                    Exit Sub
                End If

                'post kontrolü
                sQuery = "select FaturaNo " &
                        " from Trm_MusteriBag_Fatura_Baslik " &
                        " where posted='1'" &
                            " And FaturaNo='" & InvNum & "'"

                Dim dtpost As DataTable

                dtpost = db.RunSql(sQuery)

                If dtpost.Rows.Count > 0 Then
                    MessageBox.Show("Bu Fatura Numarası Daha Önceden İşlenmiştir.Tekrar Oluşturulamaz !")
                    Exit Sub
                End If

                'Dim fdeger As String = ""
                'sQuery = "select FaturaNo from Trm_MusteriBag_Fatura_Baslik where FaturaNo='" & InvNum & "'"

                'Dim dtfdeger As DataTable

                'dtfdeger = db.RunSql(sQuery)

                'If dtfdeger.Rows.Count > 0 Then
                '    fdeger = dtfdeger.Rows(0)("FaturaNo").ToString
                'End If

                'If fdeger <> String.Empty Then
                '    MessageBox.Show("Bu Fatura No Mevcut !!!.Tekrar Oluşturulamaz !")
                '    Exit Sub
                'End If

                Dim fdeger As Boolean = False
                sQuery = "select posted from Trm_MusteriBag_Fatura_Baslik where FaturaNo='" & InvNum & "'"

                Dim dtfdeger As DataTable

                dtfdeger = db.RunSql(sQuery)

                If dtfdeger.Rows.Count > 0 Then
                    fdeger = dtfdeger.Rows(0)("posted").ToString
                End If

                If fdeger <> False Then
                    MessageBox.Show("Bu Fatura No Mevcut !!!.Tekrar Oluşturulamaz !")
                    Exit Sub
                End If

                'db.BeginTransaction()

                'bu faturaya ait ozet,detay ve açıklama tablolarındaki daha önceki verileri siliniyor...
                sQuery = "delete from Trm_MusteriBag_Fatura_Baslik where FaturaNo='" & InvNum & "'"
                db.RunSql(sQuery, True)

                sQuery = "delete from Trm_MusteriBag_Fatura_Detay where FaturaNo='" & InvNum & "'"
                db.RunSql(sQuery, True)

                sQuery = "delete from Trm_MusteriBag_Fatura_Aciklama where FaturaNo='" & InvNum & "'"
                db.RunSql(sQuery, True)

                Dim kdv, gtop, tutar As Decimal
                kdv = 0
                gtop = 0
                tutar = 0
                Dim kdvtutari As Decimal
                kdvtutari = 0
                Dim kdvkodu As String = ""
                kur = 0

                sQuery = " select curr_code from custaddr " & _
                         " where cust_num='" & txtMusteri1.Text & "'" & _
                         "  and cust_seq=0 "
                Dim dtTmp As New DataTable
                dtTmp = db.RunSql(sQuery)

                If Not IsNothing(dtTmp) AndAlso dtTmp.Rows.Count > 0 Then
                    gParabirimi = dtTmp.Rows(0)("curr_code").ToString
                End If

                sQuery = " select top 1 isnull(sell_rate,0) as sell_rate from currate " & _
                         " where from_curr_code='" & gParabirimi & "'" & _
                         "  and eff_date<='" & Now.Date.ToString("yyyy-MM-dd") & "'" & _
                         " order by eff_date desc "
                'Dim dtTmp As New DataTable
                dtTmp = db.RunSql(sQuery)

                If Not IsNothing(dtTmp) AndAlso dtTmp.Rows.Count > 0 Then
                    kur = CDec(dtTmp.Rows(0)("sell_rate").ToString)
                Else
                    kur = 1
                End If

                'sQuery = " select  from customer " & _
                '         " where cust_num='" & txtMusteri1.Text & "'" & _
                '         "  and cust_seq=0 "
                ''Dim dtTmp As New DataTable
                'dtTmp = db.RunSql(sQuery)

                'If Not IsNothing(dtTmp) AndAlso dtTmp.Rows.Count > 0 Then
                '  kdvdegeri = dtTmp.Rows(0)("").ToString
                'End If

                kdvtutari = CDec(txtKdvTutari.Text)
                gtop = CDec(txtGenelToplam.Text)
                tutar = CDec(txtTutarToplam.Text)
                kdvkodu = "A01"

                ''özete kayıt atılıyor
                sQuery = " insert into Trm_MusteriBag_Fatura_Baslik" &
                         "(MusteriNo,MusteriAdi,ParaBirimi,FaturaNo,Tarih,Tutar,Kdv,GenelToplam,KdvKodu,Posted,Kur) " &
                         " VALUES(" &
                         "'" & txtMusteri1.Text & "','" & txtMusteriAdi1.Text & "'," &
                         "'" & gParabirimi & "','" & InvNum & "'," &
                         "'" & Now.Date.ToString("yyyy-MM-dd") & "'," &
                          tutar & "," & kdvtutari & "," & gtop & ",'" & kdvkodu & "','0'," & kur & ")" ''KdvKodu eklenicek
                ''kdv kodunu almaktaki amaç fatura doğru yazdırıldığında taxcode tablosundan tax_rate alanını almak için
                db.RunSql(sQuery, True)

                sQuery = " Delete From Trm_MusteriBag_Fatura_Detay Where FaturaNo='" & InvNum & "'"
                db.RunSql(sQuery, True)

                For Each row In checkedRows

                    If CDec(row.Cells("tutar").Text) > 0 Then

                        gKonu = row.Cells("Item").Text
                        gParabirimi = row.Cells("curr_code").Text
                        gMiktar = CDec(row.Cells("qty_invoiced").Text) '.Replace(",", "")
                        gBirimFiyat = CDec(row.Cells("fark_fiyati").Text) '.Replace(",", "")
                        gTutar = CDec(row.Cells("tutar").Text) '.Replace(",", "")
                        hesapno = My.Settings.HesapNo 'row.Cells("HesapNo").Text

                        sQuery = " INSERT INTO Trm_MusteriBag_Fatura_Detay (MusteriNo, MusteriAdi, FaturaNo, Konu, " &
                                " Parabirimi, Miktar, BirimFiyat, Tutar,HesapNo)" &
                                " VALUES ('" & row.Cells("cust_num").Text &
                                "','" & row.Cells("name").Text &
                                "','" & InvNum &
                                "','" & gKonu & "', '" & gParabirimi &
                                "'," & gMiktar & "," & gBirimFiyat & "," & gTutar & ",'" & hesapno & "')"
                        db.RunSql(sQuery, True)

                        'sQuery = " INSERT INTO Tr_MusteriBag_Fatura_Detay (MusteriNo, MusteriAdi, FaturaNo, Konu, " & _
                        '          " Parabirimi, Miktar, BirimFiyat, Tutar,HesapNo, Kur)" & _
                        '          " VALUES ('" & row.Cells("cust_num").Text & _
                        '          "','" & row.Cells("name").Text & _
                        '          "','" & InvNum & _
                        '          "','" & gKonu & "', '" & gParabirimi & _
                        '          "'," & gMiktar & "," & gBirimFiyat & "," & gTutar & ",'" & hesapno & "', " & kur & ")"
                        'db.RunSql(sQuery, True)

                        'sQuery = " update inv_item set Uf_Son_Fiyat= " & IIf(row.Cells("cont_price").Text = String.Empty, 0, CDec(row.Cells("cont_price").Text)) & _
                        '         " where inv_num=" & sTirnakEkle(row.Cells("inv_num").Text) & _
                        '         "    and inv_line=" & row.Cells("inv_line").Text & _
                        '         "    and item=" & sTirnakEkle(row.Cells("Item").Text)

                        'db.RunSql(sQuery, True)

                    End If

                Next

                'If frmSaticiBagimsizDetayEkle.txtkur.Value <> 1 Then
                '    kurSatiri = "1 " & frmSaticiBagimsizDetayEkle.txtParaBirimi.Text & "  " & frmSaticiBagimsizDetayEkle.txtkur.Text & " YTL Olarak alınmıştır."
                '    '    araAciklama = "YTL Karşılığı= " + kurTutar.ToString()
                'Else
                '    kurSatiri = ""
                'End If
                kurSatiri = ""

                'sQuery = " update Tr_MusteriBag_Fatura_Baslik " & _
                '            " set Tutar=" & CDec(txtTutarToplam.Text) & _
                '            ",kdv=" & CDec(txtKdvTutari.Text) & _
                '            ",GenelToplam=" & CDec(txtGenelToplam.Text) & _
                '            " where MusteriNo='" & txtMusteri1.Text & _
                '            "' and FaturaNo='" & InvNum & "'"
                'db.RunSql(sQuery, True)

                param.Clear()

                param.Add(New SqlClient.SqlParameter("@InvNum", InvNum.PadLeft(12, " ")))
                param.Add(New SqlClient.SqlParameter("@Translate", 0))

                'Dim db2 As New Core.Data(My.Settings.ConnectionString)
                'db2.BeginTransaction()
                db.RunSp("Tr_Fatura_Rpt", param)

                Dim Aciklama As String = InvNum & " nolu fiyat farkı faturası"

                ''''aciklama tablosunda kayıt varsa update yoksa intesr
                sQuery = " select * from Trm_MusteriBag_Fatura_Aciklama with (nolock) " &
                         " where FaturaNo='" & InvNum & "'"
                Dim dtkontrolaciklama As New DataTable

                dtkontrolaciklama = db.RunSql(sQuery)
                If Not dtkontrolaciklama Is Nothing AndAlso dtkontrolaciklama.Rows.Count > 0 Then
                    sQuery = " update Trm_MusteriBag_Fatura_Aciklama " &
                             " set aciklama='" & Aciklama & "'" &
                             ",kurSatiri='" & kurSatiri & "'" &
                             ",kurOrani='" & kur & "'" &
                             " where FaturaNo='" & InvNum & "'"
                    db.RunSql(sQuery, True)
                Else

                    sQuery = "insert into Trm_MusteriBag_Fatura_Aciklama(FaturaNo,aciklama,kurSatiri,kurOrani)" &
                               " values( '" & InvNum &
                                            "','" & Aciklama &
                                            "','" & kurSatiri &
                                            "','" & kur & "')"
                    '"','" & frmSaticiBagimsizDetayEkle.txtkur.Text & "')"

                    db.RunSql(sQuery, True)

                End If

                'Dim Translate As Integer = 0

                'RptParam.Add(New SqlClient.SqlParameter("@InvNum", fNo))
                'RptParam.Add(New SqlClient.SqlParameter("@Translate", Translate))
                'db.RunSp("Tr_Fatura_Rpt", RptParam)

                'yazdir.ViewReport("TURFatura123.rpt", , "@InvNum='" & fNo & "' &@Translate=0 ")
                'fAciklama = txtAciklama.Text
                'Dim rpt As New AsVol.Print.frmReportViewer("Fatura", "TURFatura3.rpt")

                'rpt.AddParameter("@aciklama", fAciklama)

                'rpt.SetLogonInfo(My.Settings.ConnectionString.Split(";")(0).Split("=")(1), My.Settings.ConnectionString.Split(";")(1).Split("=")(1), My.Settings.ConnectionString.Split(";")(2).Split("=")(1), My.Settings.ConnectionString.Split(";")(3).Split("=")(1))
                ''

                'rpt.ShowReport(True)

                'frmRapor.ViewReport("TURFatura3.rpt", , "aciklama=" & txtAciklama.Text)
                Dim paramFields As New ParameterFields
                paramFields = AddParameter("@aciklama", Aciklama, paramFields)
                paramFields = AddParameter("@kurSatir", kurSatiri, paramFields)

                'kur = frmSaticiBagimsizDetayEkle.txtkur.Text
                kur = 1

                paramFields = AddParameter("@kur", kur, paramFields)

                'ilk önce parametre ismi ni yazıyorsun crystaldekiyle aynı olmalı ve sırasıda aynı olmalı.
                '
                'LogOnDatabase("TURFatura3.rpt")
                '
                'frmRaporOzet.CrystalReportViewer1.ParameterFieldInfo = paramFields
                'frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1 '"E:\MAPICS\Projects\Windows\SLUSOU\Src\SLUSOU_GUI\bin\rptCekmeList.rpt" 'raporun path'tini yazıyorsun
                ''
                'frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = True
                'frmRaporOzet.ShowDialog()
                RaporCagir("TURFatura3.rpt", , , "fatura", False, False, , , paramFields)



                If MessageBox.Show("Fatura Doğru Yazdırıldı mı ? ", "Uyarı", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    'proc çalıştır
                    db.RunSql(" exec TRm_MusteriBagimsizPost '" & InvNum & "','" & IIf(Aciklama = "", InvNum.Trim & " Fatura", Aciklama) & "'")

                    For Each row In checkedRows

                        If CDec(row.Cells("tutar").Text) > 0 Then

                            sQuery = " update inv_item set Uf_Son_Fiyat= " & IIf(row.Cells("cont_price").Text = String.Empty, 0, CDec(row.Cells("cont_price").Text)) & _
                                      " where inv_num=" & sTirnakEkle(row.Cells("inv_num").Text) & _
                                      "    and inv_line=" & row.Cells("inv_line").Text & _
                                      "    and item=" & sTirnakEkle(row.Cells("Item").Text)

                            db.RunSql(sQuery, True)

                        End If

                    Next

                    btnSorgula_Click(sender, e)

                    MessageBox.Show(InvNum & " Numaralı Fatura Muhasebeye İşlendi...")
                    'GridEX1.DataSource = Nothing
                    'Me.Close()

                Else

                    'bu faturaya ait ozet,detay ve açıklama tablolarındaki daha önceki verileri siliniyor...
                    sQuery = "delete from Trm_MusteriBag_Fatura_Baslik where FaturaNo='" & InvNum & "'"
                    db.RunSql(sQuery, True)

                    sQuery = "delete from Trm_MusteriBag_Fatura_Detay where FaturaNo='" & InvNum & "'"
                    db.RunSql(sQuery, True)

                    sQuery = "delete from Trm_MusteriBag_Fatura_Aciklama where FaturaNo='" & InvNum & "'"
                    db.RunSql(sQuery, True)

                End If

                'If db.Transaction Then
                '  db.CommitTransaction()
                'End If

                'If db2.Transaction Then
                '  db2.CommitTransaction()
                'End If

            End If

            'btnSorgula_Click(sender, e)

        Catch ex As Exception
            'If db.Transaction Then
            '  db.RollbackTransaction()
            'End If

            'If db2.Transaction Then
            '  db2.RollbackTransaction()
            'End If

            'bu faturaya ait ozet,detay ve açıklama tablolarındaki daha önceki verileri siliniyor...
            sQuery = "delete from Trm_MusteriBag_Fatura_Baslik where FaturaNo='" & InvNum & "'"
            db.RunSql(sQuery, True)

            sQuery = "delete from Trm_MusteriBag_Fatura_Detay where FaturaNo='" & InvNum & "'"
            db.RunSql(sQuery, True)

            sQuery = "delete from Trm_MusteriBag_Fatura_Aciklama where FaturaNo='" & InvNum & "'"
            db.RunSql(sQuery, True)

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Windows.Forms.Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CellValueChanged
        ', GridEX1.Click, GridEX1.DoubleClick, GridEX1.CurrentCellChanged, GridEX1.LinkClicked

        'GridSec(GridEX1, "inv_num", sender, e)
        Try

            If CDec(CType(sender, Janus.Windows.GridEX.GridEX).GetRow.Cells("tutar").Text) <= 0 Then
                CType(sender, Janus.Windows.GridEX.GridEX).GetRow.IsChecked = False
            End If

            TutarToplamHesapla()

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub KurGetir()
        'Dim ds As DataSet
        'Dim squery As String

        'squery = " Select buy_rate " & _
        '               " from currate " & _
        '            " Where curr_code='" & txtParabirimi.Text & "'" & _
        '            " and  CONVERT(nvarchar(12), eff_date, 105)<='" & dtptarih.Value.ToString("dd-MM-yyyy") & "'"

        'ds = db.RunSql(squery, "currate")

        'If Not ds Is Nothing And ds.Tables("currate").Rows.Count.ToString > 0 Then

        '  kur = ds.Tables("currate").Rows(0)(0).ToString

        'Else

        '  kur = 1

        'End If

        'MsgBox("Sistemde Kur bulunamadı")
    End Sub

    Private Sub TutarToplamHesapla()
        'GridSec(GridEX1, "inv_num", sender, e)

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim row As Janus.Windows.GridEX.GridEXRow

        checkedRows = Me.GridEX1.GetCheckedRows()

        Dim sTutarToplami As Decimal = 0

        Try

            If checkedRows.Length > 0 Then

                For Each row In checkedRows

                    sTutarToplami += IIf(row.Cells("tutar").Text = String.Empty, 0, CDec(row.Cells("tutar").Text))

                Next

            End If

            txtTutarToplam.Text = Format(sTutarToplami, "#0.#0")

            txtKdvTutari.Text = Format(CDec(txtTutarToplam.Text) * 0.2, "#0.#0") '" % 18 KDV"

            txtGenelToplam.Text = Format(CDec(txtTutarToplam.Text) + CDec(txtKdvTutari.Text), "#0.#0")

            'btnSorgula_Click(sender, e)

        Catch ex As Exception

            Throw ex

            'MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müşteri,Name as Tanım " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müşteri", "Tanım", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub btnExceleAktar_Click(sender As Object, e As EventArgs) Handles btnExceleAktar.Click
        Try
            ExceleAktar(GridEX1.DataSource)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region 'Methods

#Region "Other"

    'Private Sub btnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckedAll.Click
    '  For Cnt As Integer = 0 To GridEX1.RowCount - 1
    '    If GridEX1.GetRow(Cnt).RowType = Janus.Windows.GridEX.RowType.Record Then
    '      GridEX1.GetRow(Cnt).IsChecked = True
    '    End If
    '  Next
    'End Sub
    'Private Sub btnUnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnCheckedAll.Click
    '  GridEX1.SelectedItems.Clear()
    'End Sub

#End Region 'Other

    Private Sub btnCheckedAll_Click(sender As System.Object, e As System.EventArgs) Handles btnCheckedAll.Click
        GridEX1.CheckAllRecords()
        TutarToplamHesapla()
    End Sub

    Private Sub btnUnCheckedAll_Click(sender As System.Object, e As System.EventArgs) Handles btnUnCheckedAll.Click
        GridEX1.UnCheckAllRecords()
        TutarToplamHesapla()
    End Sub
End Class