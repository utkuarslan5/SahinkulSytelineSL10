Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class frmSatinAlmaIadeFaturasi

    Dim ssorgu As String
    Dim dt As DataTable
    Dim dtVendor As New DataTable
    Dim db As New Core.Data(My.Settings.ConnectionString)
    'Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim ds As New DataSet
    'Dim yazdir As New Print("sql")
    Private sessionId As String
    Public gercekkur As Double
    Dim sMasrafVergiKodu As String
    Dim dVergiDegeri As Decimal
    Dim sHesap As String
    Private dbMain As SaIadeFaturasi
    Dim pcname As String
    Dim dtAdres As DataTable
    Dim paraBirimi As String
    Dim kur As Decimal

#Region " Methods "
    '
    Private Sub ApplyFilter1()

        Try
            '

            If cmdDocNum.Text = "" Then
                Exit Sub
            End If

            If cmbVoucher.Text = "" Then
                Exit Sub
            End If


            Dim db As New SaIadeFaturasi(My.Settings.ConnectionString)
            Dim ds As DataSet
            'txtVendCurrCode
            ds = db.loadRecords( _
                        cmbVendNum.Text, _
                        cmdDocNum.Text, _
                        sessionId, _
                        dtpFaturaTarihi.Value, _
                        pcname, _
                        cmbVoucher.Text)
            'kontrat fiyatı işaretli ise 0 satınalma işaretliyse 0 gönderilir
            'gcb no eklendi max 26 olacak şekilde.
            'gcb önce tur_apinvoice tablosuna insert ediliyor.
            'fiş oluşturua basıldığında tr_ithalat dosyasına insert ediliyor..

            Dim dt As DataTable
            'Not dtDetay Is Nothing AndAlso dtDetay.Rows.Count > 0 
            If Not ds.Tables("TrM_IadeFaturasiYukleSP") Is Nothing AndAlso ds.Tables("TrM_IadeFaturasiYukleSP").Rows.Count > 0 Then
                dt = SelectDistinct(ds.Tables("TrM_IadeFaturasiYukleSP"), "Currency")
                If dt.Rows.Count > 1 Then
                    MessageBox.Show("Farkli Parabirimlerine Ait Sözlesmeler Mevcut Lütfen Kontrol Ediniz", "Hata")
                    Clear()
                    Exit Sub
                End If
            End If


        
            grdMain.DataSource = ds
            grdMain.DataMember = "TrM_IadeFaturasiYukleSP"

            '
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ''
    End Sub
    '
    Private Function preparePrint() As Boolean
        '
        Try
            'Dim db As New SaFatura(ConnectionString)
            '
            For i As Integer = 0 To grdMain.RowCount - 1
                If grdMain.GetRow(i).Cells(0).Value = True Then
                    If Not (dbMain.updateRecords(sessionId, _
                                grdMain.GetRow(i).Cells("transnum").Value, _
                                grdMain.GetRow(i).Cells("Amount").Value)) Then
                        '
                        Throw New Exception("Tutarlar güncellestirilirken hata olustu")
                        '
                    End If
                End If
            Next
            '
            Return True
        Catch ex As Exception

            Return False
        End Try

    End Function
    '
    Private Sub calculate()
        '
        Dim total As Decimal = 0
        Dim taxTotal As Decimal = 0
        '
        For i As Integer = 0 To grdMain.RowCount - 1
            If grdMain.GetRow(i).Cells(0).Value = True Then
                total += Val(grdMain.GetRow(i).Cells("Amount").Value())
                taxTotal += Val(grdMain.GetRow(i).Cells("TaxAmount").Value())

            End If
        Next
        '
        txtStnTutar2.Text = Math.Round(total, 2).ToString("###,###.#0")
        'txtVrgTutar2.Text = Math.Round(taxTotal, 2).ToString("###,###.#0")
        txtVrgTutar2.Text = Math.Round(Math.Round(total, 2) / 100 * dVergiDegeri, 2).ToString("###,###.#0")

    End Sub
    '
    Private Sub Clear()
        '
        dbMain = New SaIadeFaturasi(My.Settings.ConnectionString)
        '
        'Bilgiler ApInvoice tablosundan siliniyor
        If Not dbMain.EmptyApInvoice(sessionId) Then Throw New Exception("Hata Olustu")
        '
        txtStnTutar1.Value = 0
        txtStnTutar2.Value = 0
        txtVrgTutar1.Value = 0
        txtVrgTutar2.Value = 0
        '
        txtFaturaNo.ResetText()
        txtAciklama.ResetText()
        '
        cmdDocNum.Text = ""
        cmdDocNum.DataSource = Nothing
        cmbVoucher.Text = ""
        cmbVoucher.DataSource = Nothing
        grdMain.DataSource = Nothing
        dtpFaturaTarihi.Value = Date.Now
    End Sub

    '
#End Region

    Private Sub frmSatinAlmaFaturasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try


            pcname = My.Computer.Name
            ssorgu = " delete from TrM_IadeFaturasi" & _
                     " where PcName='" & pcname & "'"
            db.RunSql(ssorgu)


            sessionId = System.Guid.NewGuid.ToString

            Me.Cursor = Cursors.WaitCursor

            ssorgu = ""
            ssorgu = "SELECT vend_num, (SELECT [Name] " & _
                                            " FROM vendAddr " & _
                                            " WHERE vendAddr.Vend_Num = vendor.vend_Num) AS VendName," & _
                        " ISNULL(Uf_InvVendNum, '') AS InvVend, curr_code AS VendCurrCode," & _
                        " ISNULL((SELECT Curr_Code FROM Vendor v WHERE v.Vend_Num = Vendor.Uf_InvVendNum), '') AS InvVendCurrCode ," & _
                        " Tax_Code1 " & _
                     " FROM vendor ORDER BY vend_num"
            dt = Nothing
            dt = db.RunSql(ssorgu)

            dtVendor = dt
            dtpFaturaTarihi.Value = Date.Now

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub


    Private Sub cmbVendNum_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendNum.TextChanged, cmbVendNum.Leave

        Try

            If cmbVendNum.Text = "" Then
                Exit Sub
            End If

            Clear()

            'btnTemizle_Click(sender, e)


            'txtVendCurrCode
            If CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'").Length = 0 Then
                txtVendName.Text = ""
            Else
                txtVendName.Text = CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'")(0)(1)
            End If


            'txtVendInv.Text = CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Value & "'")(0)("InvVend")
            If CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'").Length = 0 Then
                sMasrafVergiKodu = ""
            Else
                sMasrafVergiKodu = CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'")(0)("Tax_Code1")
            End If
            'txtVrgTutar2
            'GetCurrCode()
            '' irsaliye nolar yükleniyor
            'daDocNum.SelectCommand.Parameters("@VendNum").Value = cmbVendNum.Value
            'DsDocNum1.TUR_ApGetDocNumsSp.Rows.Clear()
            ''
            'daDocNum.Fill(DsDocNum1)
            '
            If rdbIlkBasım.Checked Then
                ssorgu = " exec TrM_IadeIrsGetDocNum '" & cmbVendNum.Text & "'"
                dt = db.RunSql(ssorgu)
                Clear()
                cmdDocNum.DataSource = dt

                Dim SQL As String = _
                  "select Distinct voucher" + vbCrLf + _
                        "from po p" + vbCrLf + _
                        "Left Join po_vch v On v.po_num=p.po_num" + vbCrLf + _
                        "Where v.type='V'" + vbCrLf + _
                        " And p.vend_num=" + sTirnakEkle(cmbVendNum.Text)


                dt = db.RunSql(SQL)

                cmbVoucher.DataSource = dt

                ApplyFilter1()

            ElseIf rdbYenidenBasım.Checked Then

                ssorgu = "SELECT distinct DocNum as document_num FROM TRM_IadeFaturasiYB WHERE vend_num = " & sTirnakEkle(cmbVendNum.Text)
                dt = db.RunSql(ssorgu)
                Clear()
                cmdDocNum.DataSource = dt

            End If

            KurGetir()
            txtKur.Text = kur

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ''degişiklik...
        If cmdDocNum.Text = "" Then
            MessageBox.Show("Lütfen Döküman Numarasi Seçiniz..")
            Exit Sub
        End If

        If cmbVendNum.Text = "" Then
            MessageBox.Show("Lütfen Satici Seçiniz..")
            Exit Sub
        End If

        If cmbVoucher.Text = "" Then
            MessageBox.Show("Lütfen Fis Seçiniz..")
            Exit Sub
        End If

        If rdbIlkBasım.Checked Then

            ApplyFilter1()

        ElseIf rdbYenidenBasım.Checked Then

            ssorgu = "SELECT DocNum, Item, Description,UM,Qty,UnitPrice,Currency,Amount," & _
                " TaxAmount,Terms_Code,TransNum,Selected,Kur,TransDate,TaxRate " & _
                " FROM TRM_IadeFaturasiYB WHERE vend_num = " & _
                sTirnakEkle(cmbVendNum.Text) & " AND DocNum = " & sTirnakEkle(cmdDocNum.Text) & " AND VoucherNo = " & sTirnakEkle(cmbVoucher.Text)
            ds = db.RunSql(ssorgu, "TRM_IadeFaturasiYukleSP")
            grdMain.DataSource = ds
            grdMain.DataMember = "TRM_IadeFaturasiYukleSP"

        End If



    End Sub


    Private Sub btnCreateVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click

        Try

            Dim sPonum As String = ""
            Dim iPoline As Integer
            Dim iPorelease As Integer
            Dim dPoReocrddate As DateTime
            Dim dPoTutar As Decimal

            Dim checkrows() As Janus.Windows.GridEX.GridEXRow

            checkrows = grdMain.GetCheckedRows

            If checkrows.Length = 0 Then Throw New Exception("Lütfen bir kayit seçiniz...")
            If cmbVendNum.Text.Length = 0 Then Throw New Exception("Satici seçiniz")
            If cmbVoucher.Text.Length = 0 Then Throw New Exception("Fis seçiniz")
            If txtFaturaNo.Text.Trim.Length = 0 Then Throw New Exception("Fatura No giriniz")
            If txtVergiKodu.Text.Trim.Length = 0 Then Throw New Exception("Vergi Kodu Seçiniz")
            If dtpFaturaTarihi.Text.Length = 0 Then Throw New Exception("Fatura Tarihini belirtiniz")
            If txtAciklama.Text.Length = 0 Then Throw New Exception("Açiklama Giriniz")
            If CDec(txtStnTutar1.Text) <> CDec(txtStnTutar2.Text) Then Throw New Exception("Stn Tutar dogru degil")
            If CDec(txtVrgTutar1.Text) <> CDec(txtVrgTutar2.Text) Then Throw New Exception("Vrg Tutar dogru degil")
            'Exit Sub
            If kontrol(, GroupBox1) = False Then
                Exit Sub
            End If


            dt = SelectDistinct(CType(grdMain.DataSource, DataSet).Tables(0), "Transdate")

            For Each row As DataRow In dt.Rows

                If DatePart(DateInterval.Month, CDate(row.Item("Transdate").ToString)) <> DatePart(DateInterval.Month, dtpFaturaTarihi.Value) Then

                    If MessageBox.Show("Fatura Tarihi ile Giris Tarihi Arasinda Uyumsuzluk Mevcut. Devam Etmek Istiyor musunuz?", "Hata", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then

                        Exit Sub

                    Else

                        GoTo Devam

                    End If

                End If

            Next

Devam:

            Dim sVergiDairesi, sVergiNo, sTermsCode As String

            ssorgu = " select Uf_TaxOffice, tax_reg_num1, terms_code" & _
                              " from vendor" & _
                              " Where vend_num=" & sTirnakEkle(cmbVendNum.Text)

            dt = db.RunSql(ssorgu)

            GetRowInfo(sVergiDairesi, dt, 0, "Uf_TaxOffice")
            GetRowInfo(sVergiNo, dt, 0, "tax_reg_num1")
            GetRowInfo(sTermsCode, dt, 0, "terms_code")

            db.RunSql("Delete From ANSIADEFATURA")

            sessionId = System.Guid.NewGuid.ToString
            Dim ParaBirimi As String = String.Empty
            Dim Yazi As String = String.Empty
            Dim Kurus As String = String.Empty


            For Each row As Janus.Windows.GridEX.GridEXRow In checkrows

                With row

                    ParaBirimi = .Cells("Currency").Text

                    If ParaBirimi = "YTL" Or ParaBirimi = "TL" Or ParaBirimi = "TRY" Then

                        ParaBirimi = "TL"

                        Kurus = "KRS"

                    ElseIf ParaBirimi = "EU" OrElse ParaBirimi = "EUR" Then

                        ParaBirimi = "EUR"

                        Kurus = "CENT"

                    ElseIf ParaBirimi = "USD" OrElse ParaBirimi = "US" Then

                        ParaBirimi = "USD"

                        Kurus = "CENT"

                    End If

                    Dim KdvTutar, FaturaTutar As Double

                    KdvTutar = Math.Round(CDbl(txtVrgTutar1.Text), 2)

                    FaturaTutar = Math.Round(CDbl(txtStnTutar1.Text), 2)

                    Yazi = SayiYazOndalikli(KdvTutar + FaturaTutar, ParaBirimi, Kurus)

                    Yazi = SayiYazOndalikli(CDec(txtStnTutar1.Text) + CDec(txtVrgTutar1.Text), ParaBirimi, Kurus)

                    ssorgu = "SELECT addr##1,addr##2,addr##3,addr##4,city,county FROM vendaddr WHERE vend_num=" & sTirnakEkle(cmbVendNum.Text)
                    dtAdres = db.RunSql(ssorgu)

                    ssorgu = " Insert Into ANSIADEFATURA " & _
                                "(" & _
                                " FaturaNo, FaturaTarihi, SaticiNo, SaticiAdi, " & _
                                " Malzeme, Tanim, OlcuBirimi, Miktar, " & _
                                " BirimFiyat, Tutar, ParaBirimi, Vergi, " & _
                                " VergiDairesi, VergiNo, IrsaliyeNo, Yaziile, " & _
                                " Address1, Address2, Address3, Address4 ,Aciklama, City, County" & _
                                ")"
                    ssorgu = ssorgu & " Values (" & _
                                sTirnakEkle(txtFaturaNo.Text) & "," & _
                                sTirnakEkle(dtpFaturaTarihi.Value.ToString("yyyy-MM-dd")) & "," & _
                                sTirnakEkle(cmbVendNum.Text) & "," & _
                                sTirnakEkle(txtVendName.Text) & "," & _
                                sTirnakEkle(.Cells("Item").Text) & "," & _
                                sTirnakEkle(.Cells("Description").Text) & "," & _
                                sTirnakEkle(.Cells("UM").Text) & "," & _
                                CDec(.Cells("Qty").Text) & "," & _
                                CDec(.Cells("UnitPrice").Text) & "," & _
                                CDec(.Cells("Amount").Text) & "," & _
                                sTirnakEkle(.Cells("Currency").Text) & "," & _
                                dVergiDegeri & "," & _
                                sTirnakEkle(sVergiDairesi) & "," & _
                                sTirnakEkle(sVergiNo) & "," & _
                                sTirnakEkle(.Cells("DocNum").Text & " - " & .Cells("TransDate").Text) & "," & _
                                sTirnakEkle(Yazi) & "," & _
                                sTirnakEkle(IIf(dtAdres.Rows.Count <> 0, dtAdres.Rows(0)("addr##1"), "")) & "," & _
                                sTirnakEkle(IIf(dtAdres.Rows.Count <> 0, dtAdres.Rows(0)("addr##2"), "")) & "," & _
                                sTirnakEkle(IIf(dtAdres.Rows.Count <> 0, dtAdres.Rows(0)("addr##3"), "")) & "," & _
                                sTirnakEkle(IIf(dtAdres.Rows.Count <> 0, dtAdres.Rows(0)("addr##4"), "")) & "," & _
                                sTirnakEkle(txtAciklama.Text) & "," & _
                                sTirnakEkle(dtAdres.Rows(0)("city")) & "," & _
                                sTirnakEkle(dtAdres.Rows(0)("county")) & _
                             ")"

                    db.RunSql(ssorgu)

                    ParaBirimi = .Cells("Currency").Text

                End With

            Next

            'RaporCagir("TRSaticiIadeFatura.rpt", , Yazi, "satinalmaiadefaturasi", True, True)

            'RaporCagirOzet3("TRSaticiIadeFatura.rpt", , Yazi, "SLFATURA", True, True)
            'myPrinters.SetDefaultPrinter(ReadConfig("DefaultPrinterMuhasebe"))
            Dim toplamTutar As Decimal
            toplamTutar = CDec(txtVrgTutar1.Text) + CDec(txtStnTutar1.Text)

            Dim paramFields As New ParameterFields
            paramFields.Clear()
            paramFields = AddParameter("@kdv", txtVrgTutar1.Text, paramFields)
            paramFields = AddParameter("@aratoplam", txtStnTutar1.Text, paramFields)
            paramFields = AddParameter("@toplam", toplamTutar, paramFields)
            paramFields = AddParameter("@yaziiletutar", Yazi, paramFields)
            
            'LogOnDatabase("Rapor\TRSaticiIadeFatura.rpt")
         
            'frmRaporOzet.CrystalReportViewer1.ParameterFieldInfo = paramFields
            'frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            'frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = True
            'frmRaporOzet.CrystalReportViewer1.ShowPrintButton = True
            'frmRaporOzet.ShowDialog()

            'RaporCagirOzet("TRSaticiIadeFatura.rpt", , Yazi, "SLFATURA", True, True)

            RaporCagir("TRSaticiIadeFatura.rpt", , , "fatura", False, False, , , paramFields)

            If rdbYenidenBasım.Checked Then
                Exit Sub
            End If

            If MessageBox.Show("Fatura Doğru Yazdırıldı mı?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then

                Exit Sub

            End If


            '------------------------------------------------------------------------------------------
            KurGetir()
            Dim dtTemp As DataTable

            ssorgu = "SELECT * FROM TRM_IadeFaturasi WHERE DocNum = '" & cmdDocNum.Text & "'"
            dt = db.RunSql(ssorgu)

            ssorgu = "SELECT * FROM TrM_SaticiBag_Fatura_Baslik WHERE SaticiNo = " & sTirnakEkle(cmbVendNum.Text) & " AND FaturaNo = " & sTirnakEkle(txtFaturaNo.Text)
            dtTemp = db.RunSql(ssorgu)

            If dtTemp.Rows.Count > 0 Then
                ssorgu = "DELETE FROM TrM_SaticiBag_Fatura_Baslik WHERE SaticiNo = " & sTirnakEkle(cmbVendNum.Text) & " AND FaturaNo = " & sTirnakEkle(txtFaturaNo.Text)
                db.RunSql(ssorgu)
            End If

            ssorgu = "INSERT INTO TrM_SaticiBag_Fatura_Baslik VALUES('" & cmbVendNum.Text & "','" & txtVendName.Text & "','" & ParaBirimi & "','" & txtFaturaNo.Text & "','" &
                dtpFaturaTarihi.Value.ToString("yyyy-MM-dd") & "'," & CDec(txtStnTutar1.Text) & "," & txtKur.Text & "," & CDec(txtVrgTutar1.Text) & "," & CDec(txtStnTutar1.Text) + CDec(txtVrgTutar1.Text) &
                ",'" & txtVergiKodu.Text & "',0,NULL)"
            db.RunSql(ssorgu)

            ssorgu = "SELECT * FROM TrM_SaticiBag_Fatura_Detay WHERE SaticiNo = " & sTirnakEkle(cmbVendNum.Text) & " AND FaturaNo = " & sTirnakEkle(txtFaturaNo.Text)
            dtTemp = db.RunSql(ssorgu)

            If dtTemp.Rows.Count > 0 Then
                ssorgu = "DELETE FROM TrM_SaticiBag_Fatura_Detay WHERE SaticiNo = " & sTirnakEkle(cmbVendNum.Text) & " AND FaturaNo = " & sTirnakEkle(txtFaturaNo.Text)
                db.RunSql(ssorgu)
            End If

            Dim sayac As Integer = 0

            For Each row As DataRow In dt.Rows

                sayac = sayac + 1

                ssorgu = "INSERT INTO TrM_SaticiBag_Fatura_Detay VALUES('" & cmbVendNum.Text & "','" & txtVendName.Text & "','" & txtFaturaNo.Text & "','" & txtAciklama.Text & "','" &
                    ParaBirimi & "'," & CDec(row.Item("Qty").ToString) & "," & CDec(row.Item("UnitPrice").ToString) & "," & Math.Round(CDec(row.Item("Amount")), 2) & ",'" & row.Item("inv_acct").ToString & "','" & row.Item("UM").ToString & "'," & sayac & ")"
                db.RunSql(ssorgu)

            Next

            db.RunSql(" exec TrM_SaticiBagimsizPost '" & txtFaturaNo.Text & "','" & txtAciklama.Text & "','" & "" & "'")

            Dim voucher As Integer
            'Dim dtTemp As DataTable

            ssorgu = "SELECT voucher FROM TrM_SaticiBag_Fatura_Baslik WHERE FaturaNo = '" & txtFaturaNo.Text & "'"
            dtTemp = db.RunSql(ssorgu)
            voucher = dtTemp.Rows(0)(0)

            Dim date_seq As Integer

            For Each row As DataRow In dt.Rows

                ssorgu = "SELECT ISNULL(MAX(date_seq),0) date_seq FROM po_vch WHERE po_num='" & row.Item("po_num").ToString & "' AND po_line='" & row.Item("po_line").ToString & "' AND rcvd_date ='" & dtpFaturaTarihi.Value.ToString("yyyy-MM-dd") & "'"
                dt = db.RunSql(ssorgu)

                If dt.Rows.Count = 0 Then
                    date_seq = 1
                ElseIf dt.Rows.Count > 0 Then
                    date_seq = dt.Rows(0)("date_seq") + 1
                End If

                ssorgu = "INSERT INTO po_vch ( po_num, po_line, po_release, rcvd_date, date_seq, exch_rate, item_cost, qty_vouchered,type, voucher, uf_invNumAll ) " & _
                    " VALUES('" & row.Item("po_num").ToString & "','" & row.Item("po_line").ToString & "',0,'" & dtpFaturaTarihi.Value.ToString("yyyy-MM-dd") & "'," & date_seq & _
                    "," & txtKur.Text & "," & row.Item("UnitPrice").ToString & "," & row.Item("Qty") * -1 & ",'V'," & voucher & ",'IADE')"
                db.RunSql(ssorgu)

            Next

            For Each row As Janus.Windows.GridEX.GridEXRow In checkrows
                '" , Uf_MatlTran_Inv_Num = " & sTirnakEkle(txtFaturaNo.Text) & _
                ssorgu = " Update Matltran" & _
                        " Set Uf_MatlvoucherNo=" & sTirnakEkle(voucher) & _
                        " Where Trans_Num=" & CInt(row.Cells("transnum").Text)

                db.RunSql(ssorgu)

            Next row

            '------------------------------------------------------------------------------------------
            'Dim voucher As String = ""

            ssorgu = "SELECT * FROM TrM_SaticiBag_Fatura_Baslik " & _
                " WHERE SaticiNo = '" & cmbVendNum.Text & "' AND FaturaNo = '" & txtFaturaNo.Text & "'"
            dt = db.RunSql(ssorgu)

            If dt.Rows.Count > 0 Then
                voucher = dt.Rows(0)("Voucher").ToString
            End If

            If Not dbMain.EmptyApInvoice(sessionId) Then Throw New Exception("Hata Olustu")
            MsgBox("Fis olusturuldu [ " & voucher & " ] ", MsgBoxStyle.Information, "Bilgi")
            'Tekrar filtreleniyor
            dbMain.CommitTransaction()

            ApplyFilter1()

            grdMain.DataSource = Nothing
            txtAciklama.Text = ""
            txtStnTutar1.Value = 0
            txtStnTutar2.Value = 0

            txtFaturaNo.Text = ""

            txtVendName.Text = ""
            txtVrgTutar1.Value = 0
            txtVrgTutar2.Value = 0
            '
            Clear()
          

            If Not dbMain.EmptyApInvoice(sessionId) Then Throw New Exception("Hata Olustu")
            MsgBox("Fis olusturuldu [ " & cmbVoucher.Text & " ] ", MsgBoxStyle.Information, "Bilgi")
            'Tekrar filtreleniyor
            dbMain.CommitTransaction()

            ApplyFilter1()

            grdMain.DataSource = Nothing
            txtAciklama.Text = ""
            txtStnTutar1.Value = 0
            txtStnTutar2.Value = 0

            txtFaturaNo.Text = ""

            txtVendName.Text = ""
            txtVrgTutar1.Value = 0
            txtVrgTutar2.Value = 0
            '
            Clear()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)

        End Try

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

    Private Sub grdMain_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMain.CellEdited
        Try
            If e.Column.DataMember = "UnitPrice" Then
                Dim row As Janus.Windows.GridEX.GridEXRow

                row = CType(sender, Janus.Windows.GridEX.GridEX).GetRow

                ssorgu = " Update TrM_IadeFaturasi" & _
                            " Set UnitPrice=" & CDec(row.Cells("UnitPrice").Text) & _
                            " , Amount = Qty * " & CDec(row.Cells("UnitPrice").Text) & _
                            " , TaxAmount = Qty * " & CDec(row.Cells("UnitPrice").Text) & " * " & "0." & row.Cells("TaxRate").Text & _
                            " Where Transnum=" & CLng(row.Cells("TransNum").Text)

                db.RunSql(ssorgu, True)

                btnAdd_Click(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show(ex.ToString, "Ekip Mapics", MessageBoxButtons.OK)

        End Try
    End Sub


    Private Sub grdMain_RowCountChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMain.RowCountChanged
        stBar.Panels(0).Text = " Listede " & grdMain.RowCount & " kayıt var "
    End Sub

    Private Sub btnTemizle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemizle.Click
        Clear()

        cmbVendNum.Text = ""
        txtVendName.Text = ""
        dtpFaturaTarihi.Value = Now.Date

        pcname = My.Computer.Name

        ssorgu = " delete " & _
                    " from TrM_IadeFaturasi" & _
                    " where PcName='" & pcname & "'"

        db.RunSql(ssorgu)

    End Sub

    Private Sub grdMain_CellValueChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMain.CellValueChanged, grdMain.ColumnHeaderClick

        Try
            Dim sel As Integer = 0
            For i As Integer = 0 To grdMain.RowCount - 1
                If grdMain.GetRow(i).Cells(0).Value = True Then
                    sel += 1
                End If
            Next
            '
            stBar.Panels(1).Text = sel & " kayıt seçildi "
            '
            'tutarlar hesaplanıyor
            calculate()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub frmSatinAlmaFaturasi_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Clear()
    End Sub

    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function

    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub

    Private Function ValidationNumeric(ByVal tip As TextBox) As Boolean

        Try

            If IsNumeric(tip.Text) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function ValidationNumericEditbox(ByVal tip As Janus.Windows.GridEX.EditControls.EditBox) As Boolean

        Try

            If IsNumeric(tip.Text) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub cmbVendNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendNum.ButtonClick
        'ssorgu = ""

        ssorgu = " select a.vend_num as [Satıcı] , a.name as [Satıcı Adı] ,v.curr_code as  [Para Birimi] " & _
             " from vendaddr a " & _
             " left join vendor v " & _
             " on a.vend_num=v.vend_num " & _
             " ORDER BY 1 "


        FindFormCagir(ssorgu, "Satıcı", "Satıcı Adı", cmbVendNum.Text, txtVendName.Text, paraBirimi, "Para Birimi")
        
    KurGetir()
        txtKur.Text = kur
    End Sub

    Private Sub cmdDocNum_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDocNum.Leave
        Try
            Me.Cursor = Cursors.WaitCursor

            If cmdDocNum.Text = String.Empty Then
                Exit Sub
            End If
          
            If rdbIlkBasım.Checked Then

                ssorgu = " select Distinct po_vch.voucher" & _
                       " from matltran mt " & _
                       " left join po_vch on po_vch.po_num = mt.ref_num and po_vch.po_line = mt.ref_line_suf " & _
                         " and po_vch.po_release = mt.ref_release " & _
                       " where mt.document_num='" & cmdDocNum.Text & "'" & _
                           " and mt.uf_matlvoucherno='0'" & _
                           " and isnull(po_vch.voucher,0) <> 0 and po_vch.type = 'V'"

                dt = db.RunSql(ssorgu)

                cmbVoucher.DataSource = dt

            ElseIf rdbYenidenBasım.Checked Then

                ssorgu = "SELECT VoucherNo as Voucher FROM TRM_IadeFaturasiYB WHERE vend_num = " & sTirnakEkle(cmbVendNum.Text) & " AND DocNum = " & sTirnakEkle(cmdDocNum.Text)
                dt = db.RunSql(ssorgu)
                cmbVoucher.DataSource = dt

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    Private Sub txtVergi_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVergiKodu.ButtonClick

        ssorgu = ""
        ssorgu = " SELECT tax_code As [Vergi Kodu], tax_rate As [Vergi Degeri], Ap_Acct [Hesap] " & _
                 " FROM taxcode " & _
                 " ORDER BY 1 "

        FindFormCagir(ssorgu, "Vergi Kodu", "Vergi Degeri", txtVergiKodu.Text, dVergiDegeri, sHesap, "Hesap")

        txtVrgTutar2.Text = Math.Round(CDec(txtStnTutar2.Text) / 100 * dVergiDegeri, 2).ToString("###,###.#0")

    End Sub

    Private Sub rdbYenidenBasım_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbYenidenBasım.CheckedChanged

        If cmbVendNum.Text = String.Empty Then
            Exit Sub
        End If

        If rdbIlkBasım.Checked Then

            ssorgu = " exec TrM_IadeIrsGetDocNum '" & cmbVendNum.Text & "'"
            dt = db.RunSql(ssorgu)
            Clear()
            cmdDocNum.DataSource = dt

        ElseIf rdbYenidenBasım.Checked Then

            ssorgu = "SELECT distinct DocNum as document_num FROM TRM_IadeFaturasiYB WHERE vend_num = " & sTirnakEkle(cmbVendNum.Text)
            dt = db.RunSql(ssorgu)
            cmdDocNum.DataSource = dt

        End If

    End Sub

    Private Sub dtpFaturaTarihi_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFaturaTarihi.ValueChanged
        KurGetir()
        txtKur.Text = kur
    End Sub

End Class
