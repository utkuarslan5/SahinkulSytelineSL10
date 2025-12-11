Public Class frmIrsaliye

    #Region "Fields"

    Dim bCekmeli As Boolean = True
    Dim bEtiketDurum As Boolean = False
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtIrsaliye As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Public Sub AyniyatInsert(ByVal ShpNo As Integer, ByVal Ambar As String, ByVal Malzeme As String, _
        ByVal Tanim As String, ByVal OlcuBirimi As String, _
        ByVal Miktar As Double, ByVal EldekiMiktar As Double, _
        ByVal Tip As String)
        Try

            sQuery = " Insert Into Ayniyat" & _
                    " (ShpNo, Ambar, Malzeme, TANIM, OLCUBIRIMI , MIKTAR,ELDEKIMIKTAR,TIP)" & _
                      " Values (" & _
                        ShpNo & "," & _
                        sTirnakEkle(Ambar) & "," & _
                        sTirnakEkle(Malzeme) & "," & _
                        sTirnakEkle(Tanim) & "," & _
                        sTirnakEkle(OlcuBirimi) & "," & _
                        Miktar & "," & _
                        EldekiMiktar & "," & _
                        sTirnakEkle(Tip) & ")"

            dbAccess.RunSql(sQuery)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Public Sub MiktarAl(ByVal Ambar As String, ByVal Malzeme As String, ByRef Tanim As String, ByRef Miktar As Double)
        Try

            sQuery = "Select i.item , i.description, Sum(isnull(mohtq,0)) - Sum(isnull(count_qty,0)) As Miktar " & _
                                            "From item i " & _
                                            " Left Join dcitemsum d on d.item=i.item  And d.whse=" & sTirnakEkle(Ambar) & _
                                            " Left Join Itembl b On i.item=b.Itnbr And b.House=" & sTirnakEkle(Ambar) & _
                                            " Left Join Itemdim m on m.itnbr=i.item " & _
                                            "Where i.item=" & sTirnakEkle(Malzeme) & _
                                                " And m.ayntip=1 " & _
                                            "Group by  i.item , i.description"

            dt = db.RunSql(sQuery)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                Tanim = dt.Rows(0).Item("description").ToString

                Miktar = dt.Rows(0).Item("Miktar").ToString

            Else

                Tanim = ""

                Miktar = 0

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub AddConditionalFormatting()
        'Imports Janus.Windows.GridEX is used in this module

        'Adding a condition using Discontinued field

        Dim fc As Janus.Windows.GridEX.GridEXFormatCondition

        ''adding a format condition to use font bold when OnSale field is true
        'fc = New GridEXFormatCondition(GridEX1.RootTable.Columns("ADIIST"), ConditionOperator.GreaterThan, "50")

        'fc.FormatStyle.FontBold = TriState.True

        'fc.FormatStyle.ForeColor = Color.Red

        'GridEX1.RootTable.FormatConditions.Add(fc)

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(GridEX1.RootTable.Columns("DDAAGP"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)

        fc.FormatStyle.FontBold = Janus.Windows.GridEX.TriState.True

        fc.FormatStyle.ForeColor = Color.Red

        GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub btnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckedAll.Click
        GridEX1.CheckAllRecords()
    End Sub

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
        Dim bEksikSevkiyat As Boolean = False

        Dim bKontrol As Boolean = False

        Dim nSevkNo, tmpPickNo As Integer

        Dim sCekmeNolar As String = ""
        Dim sKontrolMusteri As String = ""
        Dim sKontrolNakliyeci As String = ""
        Dim sKontrolPlant As String = ""
        Dim sKontrolKapi As String = ""
        Dim sKontrolMalzeme As String = ""

        Dim nKutuFark As Double

        Dim nPalettekiKutuSayisi As Double = 0

        Dim nIlkPalettekiKutuSayisi As Double = 0

        Dim nYeniNetAgr As Double = 0

        Dim nYeniBrtAgr As Double = 0

        Dim nYenimiktar As Double = 0

        Dim nHariciKutuSayisi As Double = 0

        Dim nDeltaKutu As Double = 0

        Dim nPaletSiradakiKutuSayisi As Double = 0

        Dim nKutudakiMiktar As Double = 0

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim row As Janus.Windows.GridEX.GridEXRow

        Dim nCekmeNo As Integer = 1

        Dim nMaxCekmeNo As Integer = 0

        Dim sVergiNo, sVergiDairesi As String

        Dim sMusteri, sPlant As String

        Dim sMusteriSiraNo As Integer

        Dim iVadeFarkli As Integer = 0

        Dim sVadeKosullari As String = ""

        checkedRows = Me.GridEX1.GetCheckedRows()

        Try

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                dbAccess.RunSql("Delete From tmpIrsaliye")

                sIrsaliyeSeri.IrsaliyeNo = ""
                sIrsaliyeSeri.Musteri = ""
                sIrsaliyeSeri.SeferNo = ""
                sIrsaliyeSeri.TeslimAlan = ""
                sIrsaliyeSeri.NavlunFaturaNo = ""
                sIrsaliyeSeri.Plaka = ""
                sIrsaliyeSeri.Transport = ""
                sIrsaliyeSeri.Plant = ""
                sIrsaliyeSeri.sNavlunNo = ""

                For Each row In checkedRows

                    If Not sVadeKosullari.Contains(row.Cells("terms_code").Text) Then

                        sVadeKosullari = IIf(sVadeKosullari = "", "", sVadeKosullari & ",") & row.Cells("terms_code").Text

                        iVadeFarkli += 1

                    End If

                    If Not sKontrolMusteri.Contains(row.Cells("DHCANB").Text) Then

                        sKontrolMusteri = IIf(sKontrolMusteri = "", row.Cells("DHCANB").Text, sKontrolMusteri & "," & row.Cells("DHCANB").Text)

                    End If

                    If Not sKontrolNakliyeci.Contains(row.Cells("DHCDTX").Text) Then

                        sKontrolNakliyeci = IIf(sKontrolNakliyeci = "", row.Cells("DHCDTX").Text, sKontrolNakliyeci & "," & row.Cells("DHCDTX").Text)

                    End If

                    If Not sKontrolPlant.Contains(row.Cells("DHB9CD").Text) Then

                        sKontrolPlant = IIf(sKontrolPlant = "", row.Cells("DHB9CD").Text, sKontrolPlant & "," & row.Cells("DHB9CD").Text)

                    End If

                    If Not sKontrolKapi.Contains(row.Cells("DHF1CD").Text) Then

                        sKontrolKapi = IIf(sKontrolKapi = "", row.Cells("DHF1CD").Text, sKontrolKapi & "," & row.Cells("DHF1CD").Text)

                    End If

                Next

                If sKontrolMusteri.Split(",").Length > 1 OrElse _
                sKontrolPlant.Split(",").Length > 1 OrElse _
                sKontrolNakliyeci.Split(",").Length > 1 OrElse _
                sKontrolKapi.Split(",").Length > 1 Then

                    MessageBox.Show("Müşteri Plant Kapı ve Nakliyeci Bileşimi Hatalı...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Exit Sub

                End If

                If iVadeFarkli > 1 Then

                    MessageBox.Show("Farklı Vade Koşullarına Ait Çekme Listeleri Seçili!")

                    Exit Sub

                End If

                For Each row In checkedRows

                    sQuery = "INSERT INTO tmpIrsaliye  " & _
                    " (DHCVNB, DHZ969, DHCANB, DHIVNB, " & _
                    " DHB9CD, DHBYTX, DHAFVN, DHCQCD, DHF1CD, " & _
                    " DHCDTX, DDAITX, DDALTX, DDARQT, DDFCNB, " & _
                    " DDAAGP, DDAAGK, DDDHCD, DHA3CD, co_release, date_seq,PICKNO,AMBKOD)" & _
                    "Values" & _
                    "(" & sTirnakEkle(row.Cells("DHCVNB").Text) & _
                    "," & sTirnakEkle(row.Cells("DHZ969").Text) & _
                    "," & sTirnakEkle(row.Cells("DHCANB").Text) & _
                    "," & sTirnakEkle(CDate(row.Cells("DHIVNB").Text).ToString("yyyy-MM-dd")) & _
                    "," & sTirnakEkle(row.Cells("DHB9CD").Text) & _
                    "," & sTirnakEkle(row.Cells("DHBYTX").Text) & _
                    "," & sTirnakEkle(row.Cells("DHAFVN").Text) & _
                    "," & sTirnakEkle(row.Cells("DHCQCD").Text) & _
                    "," & sTirnakEkle(row.Cells("DHF1CD").Text) & _
                    "," & sTirnakEkle(row.Cells("DHCDTX").Text) & _
                    "," & sTirnakEkle(row.Cells("DDAITX").Text) & _
                    "," & sTirnakEkle(row.Cells("DDALTX").Text) & _
                    "," & row.Cells("DDARQT").Text & _
                    "," & row.Cells("DDFCNB").Text & _
                    "," & row.Cells("DDAAGP").Text & _
                    "," & row.Cells("DDAAGK").Text & _
                    "," & sTirnakEkle(row.Cells("DDDHCD").Text) & _
                    "," & sTirnakEkle(row.Cells("DHA3CD").Text) & _
                    "," & row.Cells("co_release").Text & _
                    "," & row.Cells("date_seq").Text & _
                    "," & row.Cells("PICKNO").Text & _
                    "," & sTirnakEkle(row.Cells("AMBKOD").Text) & _
                    ")"

                    dbAccess.RunSql(sQuery)

                    sQuery = " Select 1" & _
                                " From KONTRTPF" & _
                                " Where CUST=" & sTirnakEkle(row.Cells("DHCANB").Text) & _
                                " And BZMITM=" & sTirnakEkle(row.Cells("DDAITX").Text) & _
                                " And isnull(SHIPTO,'')=" & sTirnakEkle(row.Cells("DHB9CD").Text) & _
                                " And KAPI=" & sTirnakEkle(row.Cells("DHF1CD").Text)

                    dtTemp = db.RunSql(sQuery)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count = 0 AndAlso row.Cells("PICKNO").Text <> 0 Then

                        MessageBox.Show(row.Cells("DDAITX").Text & " Malzemenin Kontrat Kaydı Bulunamadı...", "Ekip Mapics")

                        Exit Sub

                    End If

                    If Not sKontrolMalzeme.Contains(row.Cells("DDAITX").Text) Then

                        sKontrolMalzeme = IIf(sKontrolMalzeme = "", sTirnakEkle(row.Cells("DDAITX").Text), sKontrolMalzeme & "," & sTirnakEkle(row.Cells("DDAITX").Text))

                    End If

                    If rdbTumu.Checked Then

                        nSevkNo = row.Cells("DHZ969").Text

                    End If

                    nMaxCekmeNo = IIf(row.Cells("PICKNO").Text > nMaxCekmeNo, row.Cells("PICKNO").Text, nMaxCekmeNo)

                    If row.Cells("PICKNO").Text <> 0 Then

                        If sLookup("1", "ETIKETDTY", " Durum<>'X' and KUTUETK=1 AND PLTNO=0 And Pickno=" & row.Cells("PICKNO").Text) = "1" Then

                            MessageBox.Show(row.Cells("PICKNO").Text & " Nolu Çekme Paletlenmemiş...", "Ekip Mapics", MessageBoxButtons.OK)

                            Exit Sub

                        End If

                    End If

                    If row.Cells("PICKNO").Text <> 0 Then

                        Dim sKontrolSevkNo As String

                        sKontrolSevkNo = sLookup("ShpNo", "Shppack", " Pickno=" & row.Cells("PICKNO").Text)

                        If sKontrolSevkNo <> "" AndAlso rdbIlkBasim.Checked Then

                            MessageBox.Show(row.Cells("PICKNO").Text & " Nolu Çekme Listesi " & sKontrolSevkNo & " Nolu İrsaliye ile İlişkili", "Ekip Mapics", MessageBoxButtons.OK)

                            Exit Sub

                        End If

                    End If

                    nCekmeNo = IIf(nCekmeNo <> 0, 1, 0) * row.Cells("PICKNO").Text

                    If Not sCekmeNolar.Contains(row.Cells("PICKNO").Text) Then

                        If row.Cells("PICKNO").Text <> 0 Then

                            sQuery = "Select 1" & _
                                        " From EtiketDty" & _
                                        " Where Pickno = " & row.Cells("PICKNO").Text

                            dtTemp = db.RunSql(sQuery)

                            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count = 0 Then

                                If MessageBox.Show(row.Cells("PICKNO").Text & " Nolu Picklist in Etiketlerini oluşturunuz...", "Ekip Mapics", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then

                                    Exit Sub

                                End If

                            End If

                        End If

                        sCekmeNolar = IIf(sCekmeNolar = "", "", sCekmeNolar & ",") & row.Cells("PICKNO").Text

                    End If

                    sIrsaliyeSeri.Musteri = row.Cells("DHCANB").Text

                    sIrsaliyeSeri.Plant = row.Cells("DHB9CD").Text

                Next

                sQuery = "Select PICKNO, DHCVNB, DDFCNB" & _
                             " From IRSLYMST" & _
                             " Where PICKNO in (" & sCekmeNolar & ")"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowdt As DataRow In dtTemp.Rows

                        sQuery = " Select * " & _
                                    " From tmpIrsaliye" & _
                                    " Where PICKNO =" & rowdt.Item("PICKNO").ToString & _
                                        " And DHCVNB=" & sTirnakEkle(rowdt.Item("DHCVNB").ToString) & _
                                        " And DDFCNB=" & rowdt.Item("DDFCNB").ToString

                        If CType(dbAccess.RunSql(sQuery), DataTable).Rows.Count = 0 Then

                            MessageBox.Show("Lütfen Picklist in Tüm Kalemlerini Seçiniz...", "Ekip Mapics", MessageBoxButtons.OK)

                            Exit Sub

                        End If

                    Next

                End If

                If rdbTumu.Checked Then

                    GoTo Seri

                End If

                If nCekmeNo <> 0 And nMaxCekmeNo <> 0 Then

                    bCekmeli = True

                ElseIf nCekmeNo = 0 And nMaxCekmeNo = 0 Then

                    If MessageBox.Show("Çekme No Boş!, EDI verileri oluşmayacak!, Çekme No belirtmeden Devam edecek misiniz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then

                        Exit Sub

                    End If

                    bCekmeli = False

                ElseIf nCekmeNo = 0 And nMaxCekmeNo <> 0 Then

                    MessageBox.Show("Çekmeli Ve Çekmesiz Siparişler Birlikte Sevk edilemez!", "Ekip Mapics", MessageBoxButtons.OK)

                    Exit Sub

                End If

                bEksikSevkiyat = False

                If bCekmeli Then

                    sQuery = "Select PICKNO, DHCVNB, DDFCNB, sum(DDARQT) As SEVKMIK  " & _
                            " From tmpIrsaliye" & _
                            " Group By PICKNO, DHCVNB, DDFCNB"

                    dtIrsaliye = dbAccess.RunSql(sQuery)

                    If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                        For Each rowDt As DataRow In dtIrsaliye.Rows

                            tmpPickNo = rowDt.Item("PICKNO").ToString

                            sQuery = " Select ADCVNB as co_num, ADFCNB as co_line, ADAITX, " & _
                                                " Sum(ADAQQT) As CEKMEMIK ," & rowDt.Item("SEVKMIK").ToString & " As SEVKMIK, MAX(KMIK) As KMIK" & _
                                                    " From Trceklist " & _
                                                    " Where CEKLIST =" & rowDt.Item("PICKNO").ToString & _
                                                    " And ADCVNB= " & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                                    " And ADFCNB=" & rowDt.Item("DDFCNB").ToString & _
                                                    " Group By ADCVNB, ADFCNB, ADAITX "

                            dt = db.RunSql(sQuery)

                            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                                Dim nKsay As Integer

                                sQuery = "select Sum(Ksay) As Ksay" & _
                                                " from TRCEKLIST " & _
                                                " Where CEKLIST =" & tmpPickNo & _
                                                    " and ADCVNB=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                                    " and ADFCNB= " & rowDt.Item("DDFCNB").ToString & _
                                                " Group By CEKLIST, ADAITX"

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(nKsay, dtTemp, 0, "Ksay")

                                If CInt(dt.Rows(0).Item("CEKMEMIK").ToString) > CInt(dt.Rows(0).Item("SEVKMIK").ToString) Then

                                    If Math.Truncate(CDbl(dt.Rows(0).Item("SEVKMIK").ToString) / CDbl(dt.Rows(0).Item("KMIK").ToString)) <> _
                                       CDbl(dt.Rows(0).Item("SEVKMIK").ToString) / CDbl(dt.Rows(0).Item("KMIK").ToString) Then

                                        MessageBox.Show("Sevk Miktarı Kutu Miktarına Uymuyor!, Sevki düzeltin!" & vbNewLine & _
                                                    "Sipariş No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                                    "Sipariş Sırası...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                                    "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                                    "Sevk Miktarı.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                                    "Çekme Miktarı....:" & dt.Rows(0).Item("CEKMEMIK").ToString & vbNewLine & _
                                                    "Kutu Miktarı.....:" & dt.Rows(0).Item("KMIK").ToString, _
                                                    "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                        btnSorgula_Click(sender, e)

                                        Exit Sub

                                    End If

                                    If MessageBox.Show("Eksik Sevkiyat Devam Etmek İstiyor musunuz?" & vbNewLine & _
                                                    "Sipariş No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                                    "Sipariş Sırası...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                                    "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                                    "Sevk Miktarı.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                                    "Çekme Miktarı....:" & dt.Rows(0).Item("CEKMEMIK").ToString, _
                                                    "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then

                                        btnSorgula_Click(sender, e)

                                        Exit Sub

                                    Else

                                        bEksikSevkiyat = True

                                    End If

                                ElseIf CInt(dt.Rows(0).Item("CEKMEMIK").ToString) = CInt(dt.Rows(0).Item("SEVKMIK").ToString) Then

                                    bEksikSevkiyat = IIf(bEksikSevkiyat = True, True, False)

                                    'ElseIf CDbl(dt.Rows(0).Item("CEKMEMIK").ToString) <> CDbl(dt.Rows(0).Item("KMIK").ToString) * nKsay Then

                                    '    MessageBox.Show("Çekme Miktarı ile Etiket Sayısı Uyumsuz!, Yeni Çekme Alın!" & vbNewLine & _
                                    '                    "Sipariş No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                    '                    "Sipariş Sırası...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                    '                    "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                    '                    "Sevk Miktarı.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                    '                    "Çekme No.........:" & rowDt.Item("PICKNO").ToString & vbNewLine & _
                                    '                    "Çekme Miktarı....:" & dt.Rows(0).Item("CEKMEMIK").ToString, _
                                    '                    "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    '    Exit Sub

                                Else

                                    MessageBox.Show("Sevk Miktarı Çekme Miktarından Büyük Olamaz!, Sevki düzeltin!" & vbNewLine & _
                                                    "Sipariş No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                                    "Sipariş Sırası...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                                    "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                                    "Sevk Miktarı.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                                    "Çekme Miktarı....:" & dt.Rows(0).Item("CEKMEMIK").ToString, _
                                                    "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    btnSorgula_Click(sender, e)

                                    Exit Sub

                                End If

                            Else

                                MessageBox.Show("Çekme Listesinde Olmayan Sipariş/Kalem Sevk Edildi!, Sevki düzeltin!" & vbNewLine & _
                                                    "Sipariş No.......:" & rowDt.Item("DHCVNB").ToString & vbNewLine & _
                                                    "Sipariş Sırası...:" & rowDt.Item("DDFCNB").ToString & vbNewLine & _
                                                    "Sevk Miktarı.....:" & rowDt.Item("SEVKMIK").ToString & vbNewLine, _
                                                    "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                btnSorgula_Click(sender, e)

                                Exit Sub

                            End If

                        Next

                    End If

                End If

        Seri:

                sQuery = " Select IRSNO, TRNCODE, USERF3, USERF5, USERF4,NAVLUNNO, PLAKA, CARRIER" & _
                            " From SHPPACK" & _
                            " Where SHPNO=" & nSevkNo

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDt As DataRow In dtTemp.Rows

                        sIrsaliyeSeri.Transport = rowDt.Item("TRNCODE").ToString

                        sIrsaliyeSeri.SeferNo = rowDt.Item("USERF5").ToString

                        sIrsaliyeSeri.TeslimAlan = rowDt.Item("USERF3").ToString

                        sIrsaliyeSeri.NavlunFaturaNo = rowDt.Item("USERF4").ToString

                        sIrsaliyeSeri.Plaka = rowDt.Item("PLAKA").ToString

                        sIrsaliyeSeri.Carrier = rowDt.Item("CARRIER").ToString

                        sIrsaliyeSeri.IrsaliyeNo = rowDt.Item("IRSNO").ToString

                        sIrsaliyeSeri.sNavlunNo = rowDt.Item("NAVLUNNO")

                    Next

                Else

                    sIrsaliyeSeri.Carrier = sKontrolNakliyeci

                End If

                frmIrsaliyeSeri.ShowDialog()
                Dim aaa As String = sIrsaliyeSeri.sNavlunNo


                If sIrsaliyeSeri.sNavlunNo = "" OrElse sIrsaliyeSeri.SeferNo = "" Then
                    MessageBox.Show("Navlun No ve Sefer No alanları boş geçilemez.", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If


                If sIrsaliyeSeri.Iptal = True Then

                    btnSorgula_Click(sender, e)

                    Exit Sub

                End If

                If rdbTumu.Checked Then

                    sQuery = " Update SHPPACK " & _
                                    " Set IRSNO=" & sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo) & _
                                    ", TRNCODE=" & sTirnakEkle(sIrsaliyeSeri.Transport) & _
                                    ", USERF3=" & sTirnakEkle(sIrsaliyeSeri.TeslimAlan) & _
                                    ", USERF5=" & sTirnakEkle(sIrsaliyeSeri.SeferNo) & _
                                    ", USERF4=" & sTirnakEkle(sIrsaliyeSeri.NavlunFaturaNo) & _
                                    ", PLAKA=" & sTirnakEkle(sIrsaliyeSeri.Plaka) & _
                                    ", NAVLUNNO=" & sTirnakEkle(sIrsaliyeSeri.sNavlunNo) & _
                                    ", CARRIER=" & sTirnakEkle(sIrsaliyeSeri.Carrier) & _
                                " Where SHPNO=" & nSevkNo

                    db.RunSql(sQuery, True)

                    GoTo Yazdir

                End If

                nSevkNo = SeriNoAl("SHIP")

                Try

                    db.BeginTransaction()

                    If bCekmeli Then

                        bEtiketDurum = True

                        sQuery = "Select PICKNO, DHCVNB, DDFCNB, MAX(DHIVNB) As SEVKTAR, sum(DDARQT) As SEVKMIK  " & _
                                     " From tmpIrsaliye" & _
                                     " Group By PICKNO, DHCVNB, DDFCNB"

                        dtIrsaliye = dbAccess.RunSql(sQuery)

                        If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                            For Each rowDt As DataRow In dtIrsaliye.Rows

                                sQuery = " Update TRCEKLIST" & _
                                            " Set SEVKNO=" & nSevkNo & _
                                                " ,SEVKMIK=" & rowDt.Item("SEVKMIK").ToString & _
                                                " ,SEVKTAR=" & sTirnakEkle(CDate(rowDt.Item("SEVKTAR").ToString).ToString("yyyy-MM-dd")) & _
                                                " ,IRSNO=" & sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo) & _
                                                " ,DURUM='4'" & _
                                            " Where CEKLIST =" & rowDt.Item("PICKNO").ToString & _
                                            " And ADCVNB=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                            " And ADFCNB=" & rowDt.Item("DDFCNB").ToString

                                db.RunSql(sQuery)

                            Next

                        End If

                    End If

                    sQuery = "Update TRCEKLIST " & _
                                    " Set Durum='4' " & _
                                " Where CEKLIST in (" & sCekmeNolar & ")"

                    db.RunSql(sQuery)

                    'If bEtiketDurum Then

                    sQuery = "Update ETIKETDTY " & _
                                " Set Durum='4' " & _
                            " Where PICKNO in (" & sCekmeNolar & ")"

                    db.RunSql(sQuery)

                    'End If

                    'CO_SHIP Update ediliyor....

                    sQuery = "Select DHCVNB, DDFCNB, co_release, date_seq, DHIVNB  " & _
                                " From tmpIrsaliye"

                    dtIrsaliye = dbAccess.RunSql(sQuery)

                    If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                        For Each rowDt As DataRow In dtIrsaliye.Rows

                            sQuery = " Update co_ship" & _
                                        " Set reason_text=" & nSevkNo & _
                                        " Where co_num=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                            " and co_line=" & rowDt.Item("DDFCNB").ToString & _
                                            " and co_release=" & rowDt.Item("co_release").ToString & _
                                            " and date_seq=" & rowDt.Item("date_seq").ToString & _
                                            " and ship_date=" & sTirnakEkle(CDate(rowDt.Item("DHIVNB").ToString).ToString("yyyy-MM-dd"))

                            db.RunSql(sQuery)

                        Next

                        '************TR_CO_SHIP e insert ******************
                        sQuery = " insert into tr_co_ship " & _
                                    " Select * " & _
                                    " from co_ship" & _
                                    " Where reason_text=" & sTirnakEkle(nSevkNo)

                        db.RunSql(sQuery)

                    End If

                    'SHPPACK  Insert

                    Dim tmpShipto As String = String.Empty
                    Dim tmpCust As String = String.Empty
                    Dim tmpShipnm As String = String.Empty
                    Dim tmpKapi As String = String.Empty
                    Dim tmpItnbr As String = String.Empty
                    Dim tmpAmbkod As String = String.Empty
                    Dim tmpKkod As String = String.Empty
                    Dim tmpMKkod As String = String.Empty
                    Dim tmpPkod As String = String.Empty
                    Dim tmpSpkod As String = String.Empty
                    Dim tmpKpkkod As String = String.Empty
                    Dim tmpAGOB As String = String.Empty
                    Dim tmpHCOB As String = String.Empty
                    Dim tmpSTKOB As String = String.Empty
                    Dim tmpPusno As String = String.Empty
                    Dim tmpYukladr As String = String.Empty
                    Dim tmpWhse As String = String.Empty
                    Dim tmpSevkTar As String = String.Empty
                    Dim tmpKumLevel As String = String.Empty
                    Dim tmpMusteriUrunKodu As String = String.Empty
                    Dim tmpMusteriUrunTanimi As String = String.Empty
                    Dim sParaBirimi As String = String.Empty

                    Dim tmpCustSeq As Integer

                    Dim tmpPickdt, tmpKsay, tmpShipmik, tmpKmik, tmpHksay, tmpPsay, _
                        tmpPkmik, tmpPksir, tmpSpmik, tmpKpmik, tmpNetagr, tmpBrtagr, _
                        tmpHacim, tmpKapen, tmpKapboy, tmpKapyuk, tmpBrmAgr, tmpKpbrmik, _
                        tmpSpbrmik, tmpPltAgr, tmpKutuAgr, tmpKpkAgr, tmpSprAgr, _
                        tmpKutuHacmi, tmpPaletHacmi, tmpPrice, tmpSprHacmi, tmpKpkHacmi, dBirimFiyat, dKur As Double
                    '***************** ShpPack Insert Döngüsü********************

                    tmpAmbkod = ""

                    sQuery = "Select PICKNO, DDAITX ,sum(DDARQT) As SEVKMIK , Max(DHIVNB) As DHIVNB, Max(DHA3CD) As DHA3CD ,  Max(DHCVNB) As DHCVNB" & _
                                " From tmpIrsaliye" & _
                                " Group By PICKNO, DDAITX"

                    dtIrsaliye = dbAccess.RunSql(sQuery)

                    If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                        For Each rowDt As DataRow In dtIrsaliye.Rows

                            tmpShipmik = rowDt.Item("SEVKMIK").ToString

                            tmpItnbr = rowDt.Item("DDAITX").ToString

                            tmpSevkTar = rowDt.Item("DHIVNB").ToString

                            tmpPickNo = rowDt.Item("PICKNO").ToString

                            tmpWhse = rowDt.Item("DHA3CD").ToString

                            sQuery = "Select co.Cust_num, co.cust_seq, custaddr.name , co.cust_po, " &
                                        " co.contact, co.whse, plantprm.b9cd , isnull(kumlevel,'1') As kumlevel" & _
                                        " From co" & _
                                        " Left Join Plantprm On co.cust_seq=plantprm.cust_seq And co.cust_num=plantprm.canb" & _
                                        " Left Join custaddr On co.cust_seq=custaddr.cust_seq And co.cust_num=custaddr.cust_num" & _
                                        " Where co.co_num=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpCust, dtTemp, 0, "cust_num")
                            GetRowInfo(tmpCustSeq, dtTemp, 0, "cust_seq")
                            GetRowInfo(tmpPusno, dtTemp, 0, "cust_po")
                            GetRowInfo(tmpKapi, dtTemp, 0, "contact")
                            GetRowInfo(tmpShipto, dtTemp, 0, "b9cd")
                            GetRowInfo(tmpShipnm, dtTemp, 0, "name")
                            GetRowInfo(tmpKumLevel, dtTemp, 0, "kumlevel")

                            sQuery = "Select  c.u_m, c.unit_weight, c.price, c.Uf_DovizCinsi, c.Uf_DovizFiyati " & _
                                    " From coitem c" & _
                                    " Where c.co_num=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                    " And c.item=" & sTirnakEkle(tmpItnbr)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpSTKOB, dtTemp, 0, "u_m")

                            GetRowInfo(tmpBrmAgr, dtTemp, 0, "unit_weight")

                            GetRowInfo(tmpPrice, dtTemp, 0, "price")

                            GetRowInfo(dBirimFiyat, dtTemp, 0, "Uf_DovizFiyati")

                            GetRowInfo(sParaBirimi, dtTemp, 0, "Uf_DovizCinsi")

                            sQuery = " select Top 1 sell_rate" & _
                                            " from currate" & _
                                            " Where curr_code =" & sTirnakEkle(sParaBirimi) & _
                                                " And eff_date <= " & sTirnakEkle(CDate(rowDt.Item("DHIVNB").ToString).ToString("yyyy-MM-dd")) & _
                                            " Order By eff_date Desc"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(dKur, dtTemp, 0, "sell_rate")

                            If dBirimFiyat <> 0 Then

                                tmpPrice = dKur * dBirimFiyat

                            End If

                            sQuery = " Select ITEM, REFADI" & _
                                        " From KONTRTPF" & _
                                        " Where CUST=" & sTirnakEkle(tmpCust) & _
                                        " And BZMITM=" & sTirnakEkle(tmpItnbr) & _
                                        " And isnull(SHIPTO,'')=" & sTirnakEkle(tmpShipto) & _
                                        " And KAPI=" & sTirnakEkle(tmpKapi)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpMusteriUrunKodu, dtTemp, 0, "ITEM")

                            GetRowInfo(tmpMusteriUrunTanimi, dtTemp, 0, "REFADI")

                            tmpYukladr = sLookup("isnull(Zip,0)", "whse", "whse=" & sTirnakEkle(tmpWhse))

                            If bCekmeli Then

                                sQuery = "Select Max(Ambkod) As Ambkod , MAx(CEKDATE) As  CEKDATE " & _
                                        " From TRCEKLIST" & _
                                        " Where CEKLIST = " & tmpPickNo & _
                                        " And ADCVNB=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                        " And ADAITX=" & sTirnakEkle(tmpItnbr)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpAmbkod, dtTemp, 0, "Ambkod")

                                GetRowInfo(tmpPickdt, dtTemp, 0, "CEKDATE")

                                sQuery = "select Max(MKKOD) MKKOD ,Max(KKOD) KKOD , Max(KMIK) KMIK," & _
                                                    " Max(PKOD) PKOD, Max(PKMIK) PKMIK, " & _
                                                    " Max(PKSIR) PKSIR " & _
                                                " from ETIKETDTY " & _
                                                " Where pickno =" & tmpPickNo & _
                                                    " and Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                                    " and kutuetk=1 " & _
                                                " Group By Pickno,Itnbr"

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKkod, dtTemp, 0, "KKOD")
                                GetRowInfo(tmpMKkod, dtTemp, 0, "MKKOD")
                                GetRowInfo(tmpKmik, dtTemp, 0, "KMIK")
                                GetRowInfo(tmpPkod, dtTemp, 0, "PKOD")
                                GetRowInfo(tmpPkmik, dtTemp, 0, "PKMIK")
                                GetRowInfo(tmpPksir, dtTemp, 0, "PKSIR")

                                sQuery = "Select KKOD,KMIK,PKOD, PKMIK, PKSIR, " & _
                                                " KPKKOD, KPMIK, SPKOD, SPMIK  " & _
                                        " From ITMPACK" & _
                                        " Where AMBKOD=" & sTirnakEkle(tmpAmbkod) & _
                                            " And Itnbr=" & sTirnakEkle(tmpItnbr)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKpkkod, dtTemp, 0, "KPKKOD")
                                GetRowInfo(tmpKpbrmik, dtTemp, 0, "KPMIK")
                                GetRowInfo(tmpSpkod, dtTemp, 0, "SPKOD")
                                GetRowInfo(tmpSpbrmik, dtTemp, 0, "SPMIK")

                            Else

                                sQuery = "Select KKOD,KMIK,PKOD, PKMIK, PKSIR, " & _
                                                " KPKKOD, KPMIK, SPKOD, SPMIK  " & _
                                        " From ITMPACK" & _
                                        " Where AMBKOD=" & sTirnakEkle(tmpAmbkod) & _
                                            " And Itnbr=" & sTirnakEkle(tmpItnbr)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKkod, dtTemp, 0, "KKOD")
                                GetRowInfo(tmpKmik, dtTemp, 0, "KMIK")
                                GetRowInfo(tmpPkod, dtTemp, 0, "PKOD")
                                GetRowInfo(tmpPkmik, dtTemp, 0, "PKMIK")
                                GetRowInfo(tmpPksir, dtTemp, 0, "PKSIR")
                                GetRowInfo(tmpKpkkod, dtTemp, 0, "KPKKOD")
                                GetRowInfo(tmpKpbrmik, dtTemp, 0, "KPMIK")
                                GetRowInfo(tmpSpkod, dtTemp, 0, "SPKOD")
                                GetRowInfo(tmpSpbrmik, dtTemp, 0, "SPMIK")

                            End If

                            If bEtiketDurum Then

                                sQuery = "Select Sum(KSay) As KSAY " & _
                                            " From TRCEKLIST" & _
                                            " Where CEKLIST =" & tmpPickNo & _
                                            " And ADAITX=" & sTirnakEkle(tmpItnbr) & _
                                            " And Durum='4'"

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKsay, dtTemp, 0, "KSAY")

                                'tmpKsay = IIf(tmpMKkod.StartsWith("KIT"), 0, tmpKsay)

                                '*****
                                sQuery = "Select Count(A.Pltno) As PSAY From " & _
                                        "(Select Distinct pltno " & _
                                            " From ETIKETDTY" & _
                                            " Where PICKNO=" & tmpPickNo & _
                                            " And Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                            " And Durum='4' And KUTUETK='1'" & _
                                            " And Pltno>0 " & _
                                            " ) A"

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpPsay, dtTemp, 0, "PSAY")

                                'sQuery = "Select Count(ETKSERINO) As HKSAY " & _
                                '            " From ETIKETDTY" & _
                                '            " Where PICKNO=" & tmpPickNo & _
                                '            " And Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                '            " And Durum='4' And PLTNO='-1' And KUTUETK=1"

                                'dtTemp = db.RunSql(sQuery)

                                'GetRowInfo(tmpHksay, dtTemp, 0, "HKSAY")

                            ElseIf bEtiketDurum = False Then

                                If tmpKmik <> 0 Then

                                    tmpKsay = Math.Truncate(tmpShipmik / tmpKmik)

                                Else

                                    tmpKsay = 0

                                End If

                                Dim tmpSirasay, tmpPaletKutuSay As Double

                                If tmpPksir <> 0 AndAlso tmpPkmik <> 0 Then

                                    tmpSirasay = Math.Truncate(tmpKsay / tmpPksir)

                                    tmpPaletKutuSay = tmpSirasay * tmpPksir

                                    tmpPsay = Math.Ceiling(tmpPaletKutuSay / tmpPkmik)

                                Else

                                    tmpPsay = 0

                                End If

                                tmpHksay = tmpKsay - tmpPaletKutuSay

                            End If

                            If tmpKpkkod <> "" Then

                                tmpKpmik = tmpPsay * tmpKpbrmik

                            Else

                                tmpKpmik = 0

                            End If

                            If tmpSpkod <> "" Then

                                tmpSpmik = tmpPsay * tmpSpbrmik

                            Else

                                tmpSpmik = 0

                            End If

                            tmpNetagr = tmpShipmik * tmpBrmAgr

                            sQuery = " Select BRMAG, BRMHC" & _
                                    " From ITEMDIM" & _
                                    " Where ITNBR=" & sTirnakEkle(tmpKkod)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpKutuAgr, dtTemp, 0, "BRMAG")

                            GetRowInfo(tmpKutuHacmi, dtTemp, 0, "BRMHC")

                            If tmpPsay > 0 Then

                                sQuery = " Select BRMAG, BRMHC" & _
                                            " From ITEMDIM" & _
                                            " Where ITNBR=" & sTirnakEkle(tmpPkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpPltAgr, dtTemp, 0, "BRMAG")

                                GetRowInfo(tmpPaletHacmi, dtTemp, 0, "BRMHC")

                            End If

                            If tmpKpmik > 0 Then

                                sQuery = " Select BRMAG, BRMHC " & _
                                            " From ITEMDIM" & _
                                            " Where ITNBR=" & sTirnakEkle(tmpKpkkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKpkAgr, dtTemp, 0, "BRMAG")
                                GetRowInfo(tmpKpkHacmi, dtTemp, 0, "BRMHC")

                            End If

                            If tmpSpmik > 0 Then

                                sQuery = " Select BRMAG, BRMHC " & _
                                            " From ITEMDIM" & _
                                            " Where ITNBR=" & sTirnakEkle(tmpSpkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpSprAgr, dtTemp, 0, "BRMAG")
                                GetRowInfo(tmpSprHacmi, dtTemp, 0, "BRMHC")

                            End If

                            tmpBrtagr = tmpNetagr + _
                                        (tmpKutuAgr * tmpKsay) + _
                                        (tmpPsay * tmpPltAgr) + _
                                        (tmpKpmik * tmpKpkAgr) + _
                                        (tmpSpmik * tmpSprAgr)

                            tmpHacim = (tmpKsay * tmpKutuHacmi) + _
                                        (tmpPsay * tmpPaletHacmi) + _
                                        (tmpKpmik * tmpKpkHacmi) + _
                                        (tmpSpmik * tmpSprHacmi)

                            tmpAGOB = "KG"

                            tmpHCOB = "M3"

                            If tmpPsay = 0 Then

                                sQuery = " Select BRMGN, BRMUZ, BRMYK  " & _
                                            " From ITEMDIM" & _
                                            " Where ITNBR=" & sTirnakEkle(tmpKkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKapen, dtTemp, 0, "BRMGN")

                                GetRowInfo(tmpKapboy, dtTemp, 0, "BRMUZ")

                                GetRowInfo(tmpKapyuk, dtTemp, 0, "BRMYK")

                            Else

                                sQuery = " Select BRMGN, BRMUZ , BRMYK " & _
                                            " From ITEMDIM" & _
                                            " Where ITNBR=" & sTirnakEkle(tmpPkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpKapen, dtTemp, 0, "BRMGN")

                                GetRowInfo(tmpKapboy, dtTemp, 0, "BRMUZ")

                                GetRowInfo(tmpKapyuk, dtTemp, 0, "BRMYK")

                                sQuery = " Select BRMYK  " & _
                                            " From ITEMDIM" & _
                                            " Where ITNBR=" & sTirnakEkle(tmpKkod)

                                dtTemp = db.RunSql(sQuery)

                                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                                    If tmpPksir = 0 Then

                                        tmpKapyuk = tmpKapyuk

                                    Else

                                        tmpKapyuk = tmpKapyuk + (dtTemp.Rows(0).Item("BRMYK").ToString * (tmpPkmik / tmpPksir))

                                    End If

                                End If

                            End If

                            sQuery = " INSERT INTO SHPPACK" & _
                                " (SHPNO,CUST,PICKNO,PICKDT,SHIPTO, Cust_Seq, SHIPNM," & _
                                " KAPI,ITNBR,SEVKTAR,IRSNO,AMBKOD,SHIPMIK,KKOD,KSAY," & _
                                " KMIK,HKSAY,PKOD,PSAY,PKMIK,PKSIR,SPKOD,SPMIK,KPKOD," & _
                                " KPMIK,NETAGR,BRTAGR,HACIM,AGOB,HCOB,STKOB,PUSNO," & _
                                " YUKLADR,KAP_EN,KAP_BOY,KAP_YUK,TRNCODE, " & _
                                " CRDDTE,CRDTIM,CRDUSR, USERF3, USERF5, USERF4, whse, " & _
                                " NAVLUNNO, MURNKOD , MURNTNM,Plaka, Carrier ) " & _
                            " VALUES (" & _
                            nSevkNo & "," & _
                            sTirnakEkle(tmpCust) & "," & _
                            tmpPickNo & "," & _
                            IIf(tmpPickdt <> 0, sTarih(DateConvertVb(tmpPickdt).ToString("yyyy-MM-dd")), "Null") & "," & _
                            sTirnakEkle(tmpShipto) & "," & _
                            tmpCustSeq & "," & _
                            sTirnakEkle(tmpShipnm) & "," & _
                            sTirnakEkle(tmpKapi) & "," & _
                            sTirnakEkle(tmpItnbr) & "," & _
                            sTarih(CDate(tmpSevkTar).ToString("yyyy-MM-dd")) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo) & "," & _
                            sTirnakEkle(tmpAmbkod) & "," & _
                            tmpShipmik & "," & _
                            sTirnakEkle(tmpKkod) & "," & _
                            tmpKsay & "," & _
                            tmpKmik & "," & _
                            tmpHksay & "," & _
                            sTirnakEkle(tmpPkod) & "," & _
                            tmpPsay & "," & _
                            tmpPkmik & "," & _
                            tmpPksir & "," & _
                            sTirnakEkle(tmpSpkod) & "," & _
                            tmpSpmik & "," & _
                            sTirnakEkle(tmpKpkkod) & "," & _
                            tmpKpmik & "," & _
                            tmpNetagr & "," & _
                            tmpBrtagr & "," & _
                            tmpHacim & "," & _
                            sTirnakEkle(tmpAGOB) & "," & _
                            sTirnakEkle(tmpHCOB) & "," & _
                            sTirnakEkle(tmpSTKOB) & "," & _
                            sTirnakEkle(tmpPusno) & "," & _
                            tmpYukladr & "," & _
                            tmpKapen & "," & _
                            tmpKapboy & "," & _
                            tmpKapyuk & "," & _
                            sTirnakEkle(sIrsaliyeSeri.Transport) & "," & _
                            sTirnakEkle(Now.ToString("yyyy-MM-dd")) & "," & _
                            Now.ToString("HHmmss") & "," & _
                            sTirnakEkle(KullaniciAdi) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.TeslimAlan) & "," & _
                            IIf(sIrsaliyeSeri.SeferNo = "", "NULL", sTirnakEkle(sIrsaliyeSeri.SeferNo)) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.NavlunFaturaNo) & "," & _
                            sTirnakEkle(tmpWhse) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.sNavlunNo) & "," & _
                            sTirnakEkle(tmpMusteriUrunKodu) & "," & _
                            sTirnakEkle(tmpMusteriUrunTanimi) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.Plaka) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.Carrier) & _
                            ")"

                            'IIf(sIrsaliyeSeri.SeferNo = "", "NULL", sIrsaliyeSeri.SeferNo)

                            'sTirnakEkle(sIrsaliyeSeri.SeferNo) & "," & _
                            db.RunSql(sQuery)

                            If sLookup("1", "OFFITEMBL", "CANBK=" & sTirnakEkle(tmpCust) & _
                                            " And B9CDK=" & sTirnakEkle(tmpShipto) & _
                                            IIf(tmpKumLevel = 2, " And GATE=" & sTirnakEkle(tmpKapi), "") & _
                                            " And AITXK=" & sTirnakEkle(tmpItnbr)) = "1" Then

                                sQuery = " Update OFFITEMBL " & _
                                            " Set DDARQK=DDARQK + " & tmpShipmik & _
                                                " ,[USER] =" & sTirnakEkle(KullaniciAdi) & _
                                                " ,TAR =" & sTirnakEkle(Now.ToString("yyyy-MM-dd")) & _
                                                " ,UTIME= " & Now.ToString("HHmmss") & _
                                            " Where CANBK=" & sTirnakEkle(tmpCust) & _
                                            " And B9CDK=" & sTirnakEkle(tmpShipto) & _
                                            IIf(tmpKumLevel = 2, " And GATE=" & sTirnakEkle(tmpKapi), "") & _
                                            " And AITXK=" & sTirnakEkle(tmpItnbr)

                                db.RunSql(sQuery)

                            Else

                                sQuery = " insert into OFFITEMBL " & _
                                            " (CANBK, B9CDK, GATE, AITXK, DDARQK, ASNKUM, [USER], TAR, UTIME)" & _
                                            " Values (" & _
                                            sTirnakEkle(tmpCust) & "," & _
                                            sTirnakEkle(tmpShipto) & "," & _
                                            sTirnakEkle(IIf(tmpKumLevel = "2", tmpKapi, "")) & "," & _
                                            sTirnakEkle(tmpItnbr) & "," & _
                                            tmpShipmik & "," & _
                                            "0" & "," & _
                                            sTirnakEkle(KullaniciAdi) & "," & _
                                            sTirnakEkle(Now.ToString("yyyy-MM-dd")) & "," & _
                                            Now.ToString("HHmmss") & ")"

                                db.RunSql(sQuery)

                            End If

                            sQuery = "Select PICKNO , DDAITX, DHCVNB, DDFCNB , Ambkod , sum(DDARQT) As SEVKMIK  " & _
                                        " From tmpIrsaliye" & _
                                        " Where DDAITX=" & sTirnakEkle(tmpItnbr) & _
                                        " And PICKNO=" & tmpPickNo & _
                                        " Group By PICKNO, DDAITX, DHCVNB, DDFCNB, Ambkod"

                            dtTemp = dbAccess.RunSql(sQuery)

                            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                                For Each rowDty As DataRow In dtTemp.Rows

                                    sQuery = " INSERT INTO SHPDTY" & _
                                            " ( SHPNO,PICKNO,SHIPTO," & _
                                            " KAPI,ITNBR, SEQNO, MIKTAR, " & _
                                            " STKOB,PRICE,FYTKTS,ORDNO, " & _
                                            " RANNO, Ambkod, ParaBirimi, BirimFiyat, Kur  ) " & _
                                        " VALUES (" & _
                                        nSevkNo & "," & _
                                        tmpPickNo & "," & _
                                        sTirnakEkle(tmpShipto) & "," & _
                                        sTirnakEkle(tmpKapi) & "," & _
                                        sTirnakEkle(tmpItnbr) & "," & _
                                        rowDty.Item("DDFCNB").ToString & "," & _
                                        rowDty.Item("SEVKMIK").ToString & "," & _
                                        sTirnakEkle(tmpSTKOB) & "," & _
                                        tmpPrice & "," & _
                                        "1" & "," & _
                                        sTirnakEkle(rowDty.Item("DHCVNB").ToString) & "," & _
                                        sTirnakEkle(tmpPusno) & "," & _
                                        sTirnakEkle(rowDty.Item("Ambkod").ToString) & "," & _
                                        sTirnakEkle(sParaBirimi) & "," & _
                                        dBirimFiyat & "," & _
                                        dKur & _
                                        ")"

                                    db.RunSql(sQuery)

                                    sQuery = " Update Coitem" & _
                                                " Set packed=0, " & _
                                                    " qty_packed=0" & _
                                                " Where co_num=" & sTirnakEkle(rowDty.Item("DHCVNB").ToString) & _
                                                " and co_line=" & rowDty.Item("DDFCNB").ToString

                                    db.RunSql(sQuery)

                                Next

                            End If

                        Next

                    End If

                    db.CommitTransaction()

                Catch ex As Exception

                    db.RollbackTransaction()

                    Throw ex

                End Try

                '********************Ayniyat Bilgileri
        Yazdir:
                Dim Malzeme, Tanim, Ambar, OlcuBirimi As String
                Dim Miktar, EldekiMiktar As Double

                dbAccess.RunSql("Delete From Ayniyat")

                sQuery = " Select Whse, STKOB As U_M , KKOD, SUM(KSAY-HKSAY) As KSAY" & _
                            " From Shppack" & _
                            " Where SHPNO=" & nSevkNo & _
                            " Group By Whse, STKOB , KKOD" &
                            " Having SUM(KSAY-HKSAY) >0"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDty As DataRow In dtTemp.Rows

                        With rowDty

                            Ambar = .Item("WHSE").ToString

                            OlcuBirimi = .Item("U_M").ToString

                            If .Item("KKOD").ToString <> "" Then

                                Malzeme = .Item("KKOD").ToString

                                Miktar = .Item("KSAY").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "K")

                            End If

                        End With

                    Next

                End If

                sQuery = " Select Whse, STKOB As U_M , KKOD, SUM(HKSAY) As KSAY" & _
                            " From Shppack" & _
                            " Where SHPNO=" & nSevkNo & _
                            " Group By Whse, STKOB , KKOD"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDty As DataRow In dtTemp.Rows

                        With rowDty

                            Ambar = .Item("WHSE").ToString

                            OlcuBirimi = .Item("U_M").ToString

                            If .Item("KKOD").ToString <> "" Then

                                Malzeme = .Item("KKOD").ToString

                                Miktar = .Item("KSAY").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "H")

                            End If

                        End With

                    Next

                End If

                sQuery = " select  HOUSE As Whse,'AD' as U_M , PKOD  , count(etkserino) As PSAY" & _
                        " from etiketdty " & _
                        " where pickno in ( " & sCekmeNolar & ")" & _
                        " And Itnbr in (" & sKontrolMalzeme & ")" & _
                        " and pltetk=1" & _
                        " and Durum<>'X'" & _
                        " group by HOUSE, Pkod"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDty As DataRow In dtTemp.Rows

                        With rowDty

                            Ambar = .Item("WHSE").ToString

                            OlcuBirimi = .Item("U_M").ToString

                            If .Item("PKOD").ToString <> "" Then

                                Malzeme = .Item("PKOD").ToString

                                Miktar = .Item("PSAY").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "P")

                            End If

                        End With

                    Next

                End If

                sQuery = "Select s.Whse, s.STKOB As U_M , KPKOD, Sum(KPMIK) As KPMIK" & _
                            " From (select Distinct  pickno,min(itnbr) As itnbr" & _
                                        " from etiketdty" & _
                                        " where pickno in ( " & sCekmeNolar & ")" & _
                                            " And Itnbr in (" & sKontrolMalzeme & ")" & _
                                            " and kutuetk=1 " & _
                                            " and pltno<>-1 " & _
                                            " and Durum<>'X'" & _
                                        " group by pickno,pltno) e" & _
                            " left join shppack s" & _
                                 " on e.pickno=s.pickno and s.itnbr=e.itnbr" & _
                            " Group By s.Whse, s.STKOB,KPKOD"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDty As DataRow In dtTemp.Rows

                        With rowDty

                            Ambar = .Item("WHSE").ToString

                            OlcuBirimi = .Item("U_M").ToString

                            If .Item("KPKOD").ToString <> "" Then

                                Malzeme = .Item("KPKOD").ToString

                                Miktar = .Item("KPMIK").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "T")

                            End If

                        End With

                    Next

                End If

                sQuery = "Select s.Whse, s.STKOB As U_M , SPKOD, Sum(SPMIK) As SPMIK" & _
                                           " From (select Distinct pickno,min(itnbr) As itnbr" & _
                                                       " from etiketdty" & _
                                                       " where pickno in ( " & sCekmeNolar & ")" & _
                                                           " And Itnbr in (" & sKontrolMalzeme & ")" & _
                                                           " and kutuetk=1 " & _
                                                           " and pltno<>-1 " & _
                                                           " and Durum<>'X'" & _
                                                       " group by pickno,pltno) e" & _
                                           " left join shppack s" & _
                                                " on e.pickno=s.pickno and s.itnbr=e.itnbr" & _
                                           " Group By s.Whse, s.STKOB,SPKOD"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDty As DataRow In dtTemp.Rows

                        With rowDty

                            Ambar = .Item("WHSE").ToString

                            OlcuBirimi = .Item("U_M").ToString

                            If .Item("SPKOD").ToString <> "" Then

                                Malzeme = .Item("SPKOD").ToString

                                Miktar = .Item("SPMIK").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "S")

                            End If

                        End With

                    Next

                End If

                If rdbIlkBasim.Checked Then

                    sQuery = " Select Ambar, Malzeme , TANIM,  OLCUBIRIMI , Sum(MIKTAR) As MIKTAR, Min(ELDEKIMIKTAR) As ELDEKIMIKTAR  " & _
                                    " From Ayniyat " & _
                                    " Group By Ambar, Malzeme , TANIM,  OLCUBIRIMI"

                    dt = dbAccess.RunSql(sQuery)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        dtAyniyat = dt

                        sIrsaliyeSeri.SevkNo = nSevkNo

                        frmAyniyatGirisi.ShowDialog()

                    End If

                End If

                dbAccess.RunSql("Delete From ANSIRSALIYE")

                sQuery = " Select * " & _
                        " From Shppack s " & _
                        " Left join custaddr c On s.cust=c.cust_num and c.cust_seq=s.cust_seq " & _
                        " Left join customer c1 On s.cust=c1.cust_num and c1.cust_seq=s.cust_seq" & _
                        " Left Join itmpack i On s.Itnbr=i.Itnbr And s.Ambkod=i.Ambkod " & _
                        " left join kontrtpf k on s.cust = k.cust AND s.Cust_Seq = k.cust_seq AND s.SHIPTO = k.SHIPTO AND s.KAPI = k.KAPI AND s.ITNBR = k.BZMITM" & _
                        " Where SHPNO=" & nSevkNo

                dt = db.RunSql(sQuery)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each rowdt As DataRow In dt.Rows

                        sVergiNo = sLookup("tax_reg_num1", "customer", " cust_num=" & sTirnakEkle(rowdt.Item("cust_num").ToString) & _
                                                                                                    " And cust_seq=0")

                        sVergiDairesi = sLookup("Uf_TaxOffice", "customer", " cust_num=" & sTirnakEkle(rowdt.Item("cust_num").ToString) & _
                                                                                                    " And cust_seq=0")

                        sQuery = " Insert Into ANSIRSALIYE " & _
                                "(DHZ969, SEVKTAR, DHCANB, PICKNO," & _
                                    "SHIPTO, KAPI, SHIPNM, DDAITX, " & _
                                    "SEVKMIK, AMBKOD, KKOD, KSAY, " & _
                                    "KMIK, PKOD, PSAY, SPKOD, " & _
                                    "SPMIK, KPKOD, KPMIK, HKSAY, " & _
                                    "NETAGR, BRTAGR, HACIM, AGOB, " & _
                                    "HCOB, STKOB, ENGNO, REVNO, " & _
                                    "REVTAR, MAMBKOD, MKKOD, MPKOD, " & _
                                    "CUSNM, DHBZTX, DHB0TX, DHB1TX, " & _
                                    "DHAAGX, DHB6CD, DHAAGW, TAXNUM, " & _
                                    "TAXNAME, KTTTNM, PLTTNM, SPTNM, " & _
                                    "KPTNM, MURNKOD, MURNTNM, PUSNO, " & _
                                    "PLAKANO, SOFOR, DEPO )" & _
                                "Values (" & _
                                nSevkNo & "," & _
                                sTarih(rowdt.Item("SEVKTAR").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("CUST").ToString) & "," & _
                                rowdt.Item("PICKNO").ToString & "," & _
                                sTirnakEkle(rowdt.Item("SHIPTO").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("KAPI").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("SHIPNM").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("ITNBR").ToString) & "," & _
                                rowdt.Item("SHIPMIK").ToString & "," & _
                                sTirnakEkle(rowdt.Item("AMBKOD").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("KKOD").ToString) & "," & _
                                rowdt.Item("KSAY").ToString & "," & _
                                rowdt.Item("KMIK").ToString & "," & _
                                sTirnakEkle(rowdt.Item("PKOD").ToString) & "," & _
                                rowdt.Item("PSAY").ToString & "," & _
                                sTirnakEkle(rowdt.Item("SPKOD").ToString) & "," & _
                                rowdt.Item("SPMIK").ToString & "," & _
                                sTirnakEkle(rowdt.Item("KPKOD").ToString) & "," & _
                                rowdt.Item("KPMIK").ToString & "," & _
                                rowdt.Item("HKSAY").ToString & "," & _
                                rowdt.Item("NETAGR").ToString & "," & _
                                rowdt.Item("BRTAGR").ToString & "," & _
                                rowdt.Item("HACIM").ToString & "," & _
                                sTirnakEkle(rowdt.Item("AGOB").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("HCOB").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("STKOB").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("USERF4").ToString) & "," & _
                                sTirnakEkle(sLookup("REVNO", "ITMPACK", "ITNBR=" & sTirnakEkle(rowdt.Item("ITNBR").ToString) & " And AMBKOD=" & sTirnakEkle(rowdt.Item("AMBKOD").ToString))) & "," & _
                                sTarih(sLookup("revision", "item", "item=" & sTirnakEkle(rowdt.Item("ITNBR").ToString))) & "," & _
                                sTirnakEkle(rowdt.Item("MAMBKOD").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("MKKOD").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("MPKOD").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("name").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("addr##1").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("addr##2").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("addr##3").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("addr##4").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("zip").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("country").ToString) & "," & _
                                sTirnakEkle(sVergiNo) & "," & _
                                sTirnakEkle(sVergiDairesi) & "," & _
                                sTirnakEkle(sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(rowdt.Item("KKOD").ToString))) & "," & _
                                sTirnakEkle(sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(rowdt.Item("PKOD").ToString))) & "," & _
                                sTirnakEkle(sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(rowdt.Item("KPKOD").ToString))) & "," & _
                                sTirnakEkle(sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(rowdt.Item("SPKOD").ToString))) & "," & _
                                sTirnakEkle(rowdt.Item("MURNKOD").ToString.Replace("*", " ")) & "," & _
                                sTirnakEkle(IIf(rowdt.Item("MURNTNM").ToString <> "", rowdt.Item("MURNTNM").ToString, sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(rowdt.Item("ITNBR").ToString)))) & "," & _
                                sTirnakEkle(rowdt.Item("PUSNO").ToString) & "," & _
                                sTirnakEkle(sIrsaliyeSeri.Plaka) & "," & _
                                sTirnakEkle(rowdt.Item("USERF3").ToString) & "," & _
                                sTirnakEkle(rowdt.Item("USER2").ToString) & _
                                ")"

                        dbAccess.RunSql(sQuery)

                    Next

                    RaporCagir("SLIrsaliye.rpt", , , "SLIRSALIYE")

                    btnSorgula_Click(sender, e)

                End If

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            sQuery = " Select *" & _
                        " From Irslymst " & _
                        " Where 1=1 "

            If rdbIlkBasim.Checked Then

                sQuery = sQuery & " And DHZ969 is null "

            Else

                sQuery = sQuery & " And DHZ969 is not null And DHZ969 <> '' And DHZ969 <> '0'"

            End If

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & "    and DHCANB='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & "    and DHCANB>='" & txtMusteri1.Text & "'" & _
                                        " and DHCANB<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtAmbar1.Text <> "" Then

                sQuery = sQuery & "    and DHA3CD='" & txtAmbar1.Text & "'"

            End If

            If txtTeslimAlan1.Text <> "" Then

                If txtTeslimAlan2.Text = "" Then

                    sQuery = sQuery & "    and DHB9CD='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery = sQuery & "    and DHB9CD>='" & txtTeslimAlan1.Text & "'" & _
                                        " and DHB9CD<='" & txtTeslimAlan2.Text & "'"
                End If

            End If

            If txtKapi1.Text <> "" Then

                If txtKapi2.Text = "" Then

                    sQuery = sQuery & "    and DHF1CD='" & txtKapi1.Text & "'"

                Else

                    sQuery = sQuery & "    and DHF1CD>='" & txtKapi1.Text & "'" & _
                                        "    and DHF1CD<='" & txtKapi2.Text & "'"

                End If

            End If

            If txtKullanici.Text <> "" Then

                sQuery = sQuery & "    and DHAFVN='" & txtKullanici.Text & "'"

            End If

            If chkTarihAraligi.Checked Then

                sQuery = sQuery & " and DHIVNB >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd")) & _
                                    " and DHIVNB <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy-MM-dd"))

            Else

                sQuery = sQuery & " and DHIVNB =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd"))

            End If

            sQuery = sQuery & " Order By DHCANB, DHB9CD, DHIVNB,DDAITX "

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                AddConditionalFormatting()

                Duzenle(GridEX1)

            Else

                GridEX1.DataSource = Nothing

            End If
        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnUnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnCheckedAll.Click
        GridEX1.UnCheckAllRecords()
    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub

    Private Sub frmIrsaliye_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtAmbar1.Text = VarsayilanAmbar

        'txtKullanici.Text = KullaniciAdi
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "PICKNO", sender, e)
    End Sub

    Private Sub txtAmbar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmbar1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct whse as Ambar,name as Tanim " & _
                            " From whse" & _
                            " Order By 1"

            FindFormCagir(ssorgu, "Ambar", "Tanim", txtAmbar1.Text, "")

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müşteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müşteri", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri2.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müşteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müşteri", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    #End Region 'Methods

End Class