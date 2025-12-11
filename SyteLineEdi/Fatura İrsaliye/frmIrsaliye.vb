'********************************************
'Date : 04122012
'Name : Rümeyda GÖNÜL
'Form Name : frmIrsaliye
'Main Version : V10.4
'Form Version : V2
'*********************************************
Imports System.Drawing.Printing
Public Class frmIrsaliye

    Dim sQuery As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)

    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim dtIrsaliye As New DataTable
    Dim bCekmeli As Boolean = True
    Dim bEtiketDurum As Boolean = False
    Dim AYNTIP As String
    Dim ayniyat As Boolean = False

    Private Sub frmIrsaliye_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TranslateFrm(Me, True)
        GridEX1.GroupByBoxInfoText = "Gruplamak istediğiniz alanı buraya sürükleyip bırakın."
        txtAmbar1.Text = VarsayilanAmbar

        txtKullanici.Text = ""
        dtmSevkTarihi1.Value = DateTime.Now
        dtmSevkTarihi2.Value = DateTime.Now

    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick

        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as 'Müsteri',Name as 'Tanim'" & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

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

            ssorgu = "SELECT Distinct Cust_Num as 'Müsteri',Name as 'Tanim'" & _
                            " From CustAddr" & _
                            " Where cust_seq=0"


            FindFormCagir(ssorgu, "Müsteri", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try

    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
       
        Try

            Cursor = Cursors.WaitCursor

            sQuery = " Select PICKNO AS CekmeNumarasi, DHCVNB AS SiparisNo, DDFCNB AS SiparisKalemi, DHZ969 AS SevkNo, DHCANB AS Musteri," &
                        " DHIVNB AS SevkTarihi, DHB9CD AS Plant,Gefco_Document, DHBYTX AS PlantAdi, DHAFVN AS Kullanici, DHCQCD AS OlcuBirimi, DHF1CD AS Kapi," &
                        " DHCDTX AS Nakliyeci, DDAITX AS Malzeme, DDALTX AS MalzemeTanimi, DDARQT AS SevkMiktari, DDAAGP AS BirimFiyat, " &
                        " DDAAGK AS BirimAgirlik, DDDHCD AS StokOlcuBirimi, CUSNM AS MusteriAdi, DHA3CD AS Ambar, co_release, date_seq, Ambkod, MURNKOD,terms_code,ONAY " &
                        " From Irslymst " &
                        " Where 1=1 "

            If rdbIlkBasim.Checked Then

                sQuery = sQuery & " And DHZ969 = '0' "

            Else

                sQuery = sQuery & " And DHZ969 <> '0' And DHZ969 <> '' And DHZ969 <> '0'"

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

                sQuery = sQuery & " and cast ( DHIVNB as date) >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd")) & _
                                    " and cast ( DHIVNB as date ) <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy-MM-dd"))

            Else

                sQuery = sQuery & " and DHIVNB >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd"))

            End If

            sQuery = sQuery & " Order By DHCANB, DHB9CD, DHIVNB, DDAITX "
            'sQuery = sQuery & " Order By DHIVNB DESC"
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
            Cursor = Cursors.Default
            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick

        If txtAmbar1.Text = "GEFC" Then
            GridSec(GridEX1, "DHCVNB", sender, e)
        Else
            GridSec(GridEX1, "PICKNO", sender, e)
        End If

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
        Dim sAmbalaj As String = ""

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

        If checkedRows.Length = 0 Then
            MessageBox.Show("Lütfen  Seçim Yapiniz!")
            Exit Sub
        End If

        Try
            Cursor = Cursors.WaitCursor

            dbAccess.RunSql("Delete From tmpIrsaliye")

            sIrsaliyeSeri.IrsaliyeNo = ""
            sIrsaliyeSeri.Musteri = ""
            sIrsaliyeSeri.SeferNo = ""
            sIrsaliyeSeri.NavlunFaturaNo = ""
            sIrsaliyeSeri.Plaka = ""
            sIrsaliyeSeri.Transport = ""
            sIrsaliyeSeri.Plant = ""
            sIrsaliyeSeri.sNavlunNo = ""
            sIrsaliyeSeri.BeyanNo = ""

            Dim ksKontrol As Integer = 0
            Dim siparisNo As String = ""

            If txtAmbar1.Text = "GEFC" Then

                For Each row In checkedRows

                    If ksKontrol = 0 Then
                        siparisNo = row.Cells("DHCVNB").Text
                    Else
                        If siparisNo <> row.Cells("DHCVNB").Text Then
                            MessageBox.Show("Farklı sipariş numaraları birleştirilemez.")
                            Exit Sub
                        End If
                    End If

                    ksKontrol = ksKontrol + 1

                Next

            End If
           

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

                If Not sKontrolKapi.Contains(row.Cells("DHB9CD").Text) Then

                    sKontrolKapi = IIf(sKontrolKapi = "", row.Cells("DHB9CD").Text, sKontrolKapi & "," & row.Cells("DHB9CD").Text)

                End If

                'If rdbYenidenBasim.Checked Then

                '    sQuery = "SELECT ksay FROM TR_IRSPACK WHERE SHPNO=" & row.Cells("DHZ969").Text
                '    dt = db.RunSql(sQuery)

                '    If sAmbalaj = "" Then
                '        If dt.Rows.Count > 0 Then
                '            For Each row2 As DataRow In dt.Rows
                '                If Not sAmbalaj.Contains(row2.Item("ksay").ToString) Then
                '                    sAmbalaj = sAmbalaj & row2.Item("ksay").ToString & " " & "/" & " "
                '                End If
                '            Next
                '            sAmbalaj = sAmbalaj.Substring(0, sAmbalaj.Length - 3)
                '        End If
                '    End If

                'End If
                sIrsaliyeSeri.IrsaliyeNo = row.Cells("Gefco_Document").Text
            Next

            For Each row In checkedRows

                If row.Cells("PICKNO").Text <> "" And row.Cells("PICKNO").Text <> 0 And rdbIlkBasim.Checked Then


                    sQuery = "SELECT ETKSERINO FROM ETIKETDTY WHERE PICKNO = '" & row.Cells("PICKNO").Text & "'"
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count = 0 Then
                        MessageBox.Show(row.Cells("PICKNO").Text & " nolu çekmenin etiketlerini oluşturunuz.")
                        Exit Sub
                    End If

                End If

                If row.Cells("ONAY").Text = "HATA" Then
                    MessageBox.Show("Çekme listesine ait okutulmamış etiket mevcut.")
                    Exit Sub
                End If

                'If rdbIlkBasim.Checked Then
                '    sQuery = "SELECT qty_ordered - qty_shipped FROM coitem WHERE co_num = " & sTirnakEkle(row.Cells("DHCVNB").Text) & " AND co_line=" & row.Cells("DDFCNB").Text
                '    dt = db.RunSql(sQuery)

                '    If dt.Rows.Count <> 0 Then
                '        If dt.Rows(0)(0) < 0 Then
                '            MessageBox.Show("Sipariş Miktarından Fazla Sevkiyat Yapılamaz.")
                '            Exit Sub
                '        End If
                '    End If
                'End If

            Next

            If sKontrolMusteri.Split(",").Length > 1 OrElse _
            sKontrolPlant.Split(",").Length > 1 OrElse _
            sKontrolNakliyeci.Split(",").Length > 1 OrElse _
            sKontrolKapi.Split(",").Length > 1 Then

                Cursor = Cursors.Default
                MessageBox.Show("Müsteri Plant Kapi ve Nakliyeci Bilesimi Hatali…", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub

            End If


            If iVadeFarkli > 1 Then

                MessageBox.Show("Farklı Vade Koşullarına Ait Çekme Listeleri Seçili!")

                Exit Sub

            End If

            Dim maxSevkTarihi As DateTime = Nothing

            For Each row In checkedRows
                '"yyyy-MM-dd HH:mm:ss.fff"
                If maxSevkTarihi = Nothing Then
                    maxSevkTarihi = CDate(row.Cells("DHIVNB").Text).ToString("yyyy-MM-dd")
                ElseIf maxSevkTarihi < CDate(row.Cells("DHIVNB").Text).ToString("yyyy-MM-dd") Then
                    maxSevkTarihi = CDate(row.Cells("DHIVNB").Text).ToString("yyyy-MM-dd")
                End If
            Next

            For Each row In checkedRows

                If row.Cells("DDARQT").Text > 0 Then

                    sQuery = "INSERT INTO tmpIrsaliye  " &
               " (DHCVNB, DHZ969, DHCANB, DHIVNB, " &
               " DHB9CD, DHBYTX, DHAFVN, DHCQCD, DHF1CD, " &
               " DHCDTX, DDAITX, DDALTX, DDARQT, DDFCNB, " &
               " DDAAGP, DDAAGK, DDDHCD, DHA3CD, co_release, date_seq,PICKNO,AMBKOD)" &
               "Values" &
               "(" & sTirnakEkle(row.Cells("DHCVNB").Text) &
               "," & sTirnakEkle(row.Cells("DHZ969").Text) &
               "," & sTirnakEkle(row.Cells("DHCANB").Text) &
               "," & sTirnakEkle(CDate(row.Cells("DHIVNB").Text).ToString("yyyy-MM-dd HH:mm:ss")) &
               "," & sTirnakEkle(row.Cells("DHB9CD").Text) &
               "," & sTirnakEkle(row.Cells("DHBYTX").Text) &
               "," & sTirnakEkle(row.Cells("DHAFVN").Text) &
               "," & sTirnakEkle(row.Cells("DHCQCD").Text) &
               "," & sTirnakEkle(row.Cells("DHF1CD").Text) &
               "," & sTirnakEkle(row.Cells("DHCDTX").Text) &
               "," & sTirnakEkle(row.Cells("DDAITX").Text) &
               "," & sTirnakEkle(row.Cells("DDALTX").Text) &
               "," & row.Cells("DDARQT").Text &
               "," & row.Cells("DDFCNB").Text &
               "," & row.Cells("DDAAGP").Text &
               "," & row.Cells("DDAAGK").Text &
               "," & sTirnakEkle(row.Cells("DDDHCD").Text) &
               "," & sTirnakEkle(row.Cells("DHA3CD").Text) &
               "," & row.Cells("co_release").Text &
               "," & row.Cells("date_seq").Text &
               "," & row.Cells("PICKNO").Text &
               "," & sTirnakEkle(row.Cells("AMBKOD").Text) &
               ")"

                    dbAccess.RunSql(sQuery)

                End If


                sQuery = " Select 1" & _
                            " From KONTRTPF" & _
                            " Where CUST=" & sTirnakEkle(row.Cells("DHCANB").Text) & _
                            " And BZMITM=" & sTirnakEkle(row.Cells("DDAITX").Text) & _
                            " And isnull(SHIPTO,'')=" & sTirnakEkle(row.Cells("DHB9CD").Text) & _
                            " And KAPI=" & sTirnakEkle(row.Cells("DHF1CD").Text)


                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count = 0 AndAlso row.Cells("PICKNO").Text <> 0 Then
                    Cursor = Cursors.Default
                    MessageBox.Show(row.Cells("DDAITX").Text & " Malzemenin Kontrat Kaydi Bulunamadi…", "Ekip Mapics")
                    Exit Sub
                End If


                If Not sKontrolMalzeme.Contains(row.Cells("DDAITX").Text) Then

                    sKontrolMalzeme = IIf(sKontrolMalzeme = "", sTirnakEkle(row.Cells("DDAITX").Text), sKontrolMalzeme & "," & sTirnakEkle(row.Cells("DDAITX").Text))

                End If

                If rdbYenidenBasim.Checked Then

                    nSevkNo = row.Cells("DHZ969").Text

                End If

                nMaxCekmeNo = IIf(row.Cells("PICKNO").Text > nMaxCekmeNo, row.Cells("PICKNO").Text, nMaxCekmeNo)

                If row.Cells("PICKNO").Text <> 0 Then

                    If sLookup("1", "ETIKETDTY", " Durum<>'X' and KUTUETK=1 AND PLTNO=0 And Pickno=" & row.Cells("PICKNO").Text) = "1" Then
                        Cursor = Cursors.Default
                        MessageBox.Show(row.Cells("PICKNO").Text & " Nolu Çekme Paletlenmemis…", "Ekip Mapics", MessageBoxButtons.OK)
                        Exit Sub
                    End If

                End If

                If row.Cells("PICKNO").Text <> 0 Then

                    Dim sKontrolSevkNo As String

                    sKontrolSevkNo = sLookup("ShpNo", "Shppack", " Pickno=" & row.Cells("PICKNO").Text)

                    If sKontrolSevkNo <> "" AndAlso rdbIlkBasim.Checked Then
                        Cursor = Cursors.Default
                        MessageBox.Show(row.Cells("PICKNO").Text & " Nolu Çekme Listesi " & sKontrolSevkNo & " Nolu Irsaliye ile Iliskili", "Ekip Mapics", MessageBoxButtons.OK)
                        Exit Sub
                    End If

                End If

                nCekmeNo = IIf(nCekmeNo <> 0, 1, 0) * row.Cells("PICKNO").Text

                If Not sCekmeNolar.Contains(row.Cells("PICKNO").Text) Then

                    'If row.Cells("PICKNO").Text <> 0 Then

                    '    sQuery = "Select 1" & _MS00427
                    '                " From TrM_EtiketDty" & _
                    '                " Where Pickno = " & row.Cells("PICKNO").Text

                    '    dtTemp = db.RunSql(sQuery)

                    '    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count = 0 Then
                    '        Cursor = Cursors.Default
                    '        MessageBox.Show(row.Cells("PICKNO").Text & GetMessage("Msg69"), "Ekip Mapics", MessageBoxButtons.OK)
                    '        Exit Sub
                    '    End If

                    'End If

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
                                " From tmpIrsaliye " & _
                                " Where PICKNO= " & rowdt.Item("PICKNO") & _
                                    " And DHCVNB= '" & rowdt.Item("DHCVNB").ToString & "'" & _
                                    " And DDFCNB=" & rowdt.Item("DDFCNB")

                    If CType(dbAccess.RunSql(sQuery), DataTable).Rows.Count = 0 Then
                        Cursor = Cursors.Default
                        MessageBox.Show("Lütfen Picklist in Tüm Kalemlerini Seçiniz…", "Ekip Mapics", MessageBoxButtons.OK)
                        Exit Sub
                    End If

                Next

            End If

            If rdbYenidenBasim.Checked Then

                GoTo Seri

            End If

            If nCekmeNo <> 0 And nMaxCekmeNo <> 0 Then

                bCekmeli = True

            ElseIf nCekmeNo = 0 And nMaxCekmeNo = 0 Then

                If MessageBox.Show("Çekme No Bos!, EDI verileri olusmayacak!, Çekme No belirtmeden Devam edecek misiniz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                bCekmeli = False

            ElseIf nCekmeNo = 0 And nMaxCekmeNo <> 0 Then
                Cursor = Cursors.Default
                MessageBox.Show("Çekmeli Ve Çekmesiz Siparisler Birlikte Sevk edilemez!", "Ekip Mapics", MessageBoxButtons.OK)
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


                            If CDbl(dt.Rows(0).Item("CEKMEMIK").ToString) > CDbl(dt.Rows(0).Item("SEVKMIK").ToString) Then

                                If Math.Truncate(CDbl(dt.Rows(0).Item("SEVKMIK").ToString) / CDbl(dt.Rows(0).Item("KMIK").ToString)) <> _
                                   CDbl(dt.Rows(0).Item("SEVKMIK").ToString) / CDbl(dt.Rows(0).Item("KMIK").ToString) Then

                                    MessageBox.Show("Sevk Miktari Kutu Miktarina Uymuyor!, Sevki düzeltin!" & vbNewLine & _
                                                "Siparis No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                                "Siparis Sirasi...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                                "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                                "Sevk Miktari.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                                "Çekme Miktari....:" & dt.Rows(0).Item("CEKMEMIK").ToString & vbNewLine & _
                                                "Kutu Miktari.....:" & dt.Rows(0).Item("KMIK").ToString, _
                                                "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    btnSorgula_Click(sender, e)
                                    Cursor = Cursors.Default
                                    Exit Sub

                                End If

                                If MessageBox.Show("Eksik Sevkiyat Devam Etmek Istiyor musunuz?" & vbNewLine & _
                                                "Siparis No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                                "Siparis Sirasi...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                                "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                                "Sevk Miktari.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                                "Çekme Miktari....:" & dt.Rows(0).Item("CEKMEMIK").ToString, _
                                                "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then

                                    btnSorgula_Click(sender, e)
                                    Cursor = Cursors.Default
                                    Exit Sub

                                Else

                                    bEksikSevkiyat = True


                                End If



                            ElseIf CDbl(dt.Rows(0).Item("CEKMEMIK").ToString) = CDbl(dt.Rows(0).Item("SEVKMIK").ToString) Then

                                bEksikSevkiyat = IIf(bEksikSevkiyat = True, True, False)

                                'ElseIf CDbl(dt.Rows(0).Item("CEKMEMIK").ToString) <> CDbl(dt.Rows(0).Item("KMIK").ToString) * nKsay Then

                                '    MessageBox.Show("Çekme Miktari ile Etiket Sayisi Uyumsuz!, Yeni Çekme Alin!" & vbNewLine & _
                                '                    "Siparis No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                '                    "Siparis Sirasi...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                '                    "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                '                    "Sevk Miktari.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                '                    "Çekme No.........:" & rowDt.Item("PICKNO").ToString & vbNewLine & _
                                '                    "Çekme Miktari....:" & dt.Rows(0).Item("CEKMEMIK").ToString, _
                                '                    "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                '    Cursor = Cursors.Default
                                '    Exit Sub

                            Else

                                MessageBox.Show("Sevk Miktari Çekme Miktarindan Büyük Olamaz!, Sevki düzeltin!" & vbNewLine & _
                                                "Siparis No.......:" & dt.Rows(0).Item("co_num").ToString & vbNewLine & _
                                                "Siparis Sirasi...:" & dt.Rows(0).Item("co_line").ToString & vbNewLine & _
                                                "Malzeme No.......:" & dt.Rows(0).Item("ADAITX").ToString & vbNewLine & _
                                                "Sevk Miktari.....:" & dt.Rows(0).Item("SEVKMIK").ToString & vbNewLine & _
                                                "Çekme Miktari....:" & dt.Rows(0).Item("CEKMEMIK").ToString, _
                                                "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                btnSorgula_Click(sender, e)
                                Cursor = Cursors.Default
                                Exit Sub

                            End If

                        Else

                            MessageBox.Show("Çekme Listesinde Olmayan Siparis/Kalem Sevk Edildi!, Sevki düzeltin!" & vbNewLine & _
                                                "Siparis No.......:" & rowDt.Item("DHCVNB").ToString & vbNewLine & _
                                                "Siparis Sirasi...:" & rowDt.Item("DDFCNB").ToString & vbNewLine & _
                                                "Sevk Miktari.....:" & rowDt.Item("SEVKMIK").ToString & vbNewLine, _
                                                "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            btnSorgula_Click(sender, e)
                            Cursor = Cursors.Default
                            Exit Sub

                        End If

                    Next

                End If

            End If

Seri:


            sIrsaliyeSeri.IrsaliyeNo = ""
            sIrsaliyeSeri.Musteri = ""
            sIrsaliyeSeri.SeferNo = ""
            sIrsaliyeSeri.TeslimAlan = ""
            sIrsaliyeSeri.NavlunFaturaNo = ""
            sIrsaliyeSeri.Plaka = ""
            sIrsaliyeSeri.Carrier = ""
            sIrsaliyeSeri.Transport = ""
            sIrsaliyeSeri.Plant = ""
            sIrsaliyeSeri.sNavlunNo = ""
            sIrsaliyeSeri.BeyanNo = ""

            sQuery = " Select IRSNO, TRNCODE, ISNULL(NakliyeSekli,' ') NakliyeSekli, USERF3, USERF4, USERF5, PLAKA, CARRIER, Aciklama, KutuAdedi, PaletAdedi, SEVKTAR, NAVLUNNO, BeyanNo, BeyanTarihi  " & _
                        " From Shppack" & _
                        " Where SHPNO=" & nSevkNo


            dtTemp = db.RunSql(sQuery)

            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                For Each rowDt As DataRow In dtTemp.Rows

                    'sIrsaliyeSeri.Transport = rowDt.Item("TRNCODE").ToString
                    sIrsaliyeSeri.Transport = rowDt.Item("NakliyeSekli").ToString

                    sIrsaliyeSeri.SeferNo = rowDt.Item("USERF5").ToString

                    sIrsaliyeSeri.TeslimAlan = rowDt.Item("USERF3").ToString

                    sIrsaliyeSeri.NavlunFaturaNo = rowDt.Item("USERF4").ToString

                    sIrsaliyeSeri.Plaka = rowDt.Item("PLAKA").ToString

                    sIrsaliyeSeri.Carrier = rowDt.Item("CARRIER").ToString

                    'Gefco_Document alanı irsaliye seri ekranına gelsin istendiği için kaldırıldı.
                    'sIrsaliyeSeri.IrsaliyeNo = rowDt.Item("IRSNO").ToString

                    sIrsaliyeSeri.sNavlunNo = rowDt.Item("NAVLUNNO").ToString

                    sIrsaliyeSeri.BeyanNo = rowDt.Item("BeyanNo").ToString

                    If rowDt.Item("BeyanTarihi").ToString <> "" Then
                        sIrsaliyeSeri.BeyanTarihi = rowDt.Item("BeyanTarihi").ToString
                    End If

                Next

            Else

                sIrsaliyeSeri.Carrier = sKontrolNakliyeci

            End If


            frmIrsaliyeSeri.ShowDialog()

            Dim aaa As String = sIrsaliyeSeri.sNavlunNo


            'If sIrsaliyeSeri.sNavlunNo = "" OrElse sIrsaliyeSeri.SeferNo = "" Then
            '    MessageBox.Show("Navlun No ve Sefer No alanları boş geçilemez.", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End If

            If sIrsaliyeSeri.Iptal = True Then

                btnSorgula_Click(sender, e)

                Exit Sub

            End If
            'sTirnakEkle(CDate(sIrsaliyeSeri.FiiliSevkTarihi.ToString("yyyy-MM-dd") & " " & sIrsaliyeSeri.FiiliSevkSaati & ".000"))
            Dim shipmentId As Decimal

            If rdbYenidenBasim.Checked Then
                '", TRNCODE = " & sTirnakEkle(sIrsaliyeSeri.Transport) & _


                sQuery = "SELECT shipment_id FROM shipment with (nolock) WHERE tracking_number = " & nSevkNo
                dt = db.RunSql(sQuery)

                If dt.Rows.Count > 0 Then
                    shipmentId = dt.Rows(0)("shipment_id")
                End If

                sQuery = "SELECT TOP 1 IRSNO FROM Shppack Where SHPNO=" & nSevkNo
                dt = db.RunSql(sQuery)

                Dim IrsNo As String

                If dt.Rows.Count > 0 Then
                    IrsNo = dt.Rows(0)(0)
                End If



                sQuery = "Select DHCVNB, DDFCNB, co_release, date_seq, DHIVNB  " & _
                           " From tmpIrsaliye"

                dtIrsaliye = dbAccess.RunSql(sQuery)

                If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                    For Each rowDt As DataRow In dtIrsaliye.Rows

                        sQuery = " Update co_ship" &
                                        " Set reason_text=" & nSevkNo &
                                        " Where co_num=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) &
                                            " and co_line=" & rowDt.Item("DDFCNB").ToString &
                                            " and co_release=" & rowDt.Item("co_release").ToString &
                                            " and date_seq=" & rowDt.Item("date_seq").ToString &
                            " and cast(ship_date as date) = " & sTirnakEkle(CDate(rowDt.Item("DHIVNB").ToString).ToString("yyyy-MM-dd"))

                        db.RunSql(sQuery)


                        sQuery = " Update matltran" &
                                  " Set document_num=" & sTirnakEkle(nSevkNo) &
                                  " Where trans_type + ref_type ='SO' AND ref_num =" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) &
                                  " and ref_line_suf=" & rowDt.Item("DDFCNB").ToString &
                                  " and document_num = " & sTirnakEkle(IrsNo)

                        db.RunSql(sQuery)

                    Next

                End If

                '", Aciklama = " & sTirnakEkle(sIrsaliyeSeri.Aciklama) & _
                '            ", KutuAdedi = " & sIrsaliyeSeri.kutuAdedi & _
                '            ", PaletAdedi = " & sIrsaliyeSeri.paletAdedi & _
                '            ", SEVKTAR = " & sTirnakEkle(sIrsaliyeSeri.FiiliSevkTarihi.ToString("yyyy-MM-dd") & " " & sIrsaliyeSeri.FiiliSevkSaati & ".000") & _

                sQuery = " Update Shppack " &
                                " Set IRSNO = " & sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo) &
                                ", NakliyeSekli = " & sTirnakEkle(sIrsaliyeSeri.Transport) &
                                ", USERF3 = " & sTirnakEkle(sIrsaliyeSeri.TeslimAlan) &
                                ", USERF4 = " & sTirnakEkle(sIrsaliyeSeri.NavlunFaturaNo) &
                                ", USERF5 = " & sTirnakEkle(sIrsaliyeSeri.SeferNo) &
                                ", PLAKA = " & sTirnakEkle(sIrsaliyeSeri.Plaka) &
                                ", NAVLUNNO=" & sTirnakEkle(sIrsaliyeSeri.sNavlunNo) &
                                ", CARRIER = " & sTirnakEkle(sIrsaliyeSeri.Carrier) &
                                ", BeyanNo = " & sTirnakEkle(sIrsaliyeSeri.BeyanNo) &
                                ", BeyanTarihi = " & sTirnakEkle(sIrsaliyeSeri.BeyanTarihi.ToString("yyyy-MM-dd"))

                If rdbIlkBasim.Checked Then
                    sQuery &= ", SEVKTAR = " & sTirnakEkle(maxSevkTarihi.ToString("yyyy-MM-dd"))
                End If

                sQuery &= " Where SHPNO=" & nSevkNo

                db.RunSql(sQuery, True)


                GoTo Yazdir


            End If

            'nSevkNo = SeriNoAl("SHIP")

            Try

                db.BeginTransaction()


                If rdbIlkBasim.Checked Then

                    nSevkNo = SeriNoAl("SHP")

                    'nSevkNo = db.RunSql("select ISNULL(max(shipment_id),0) + 1 FROM shipment with (nolock) ").Rows(0)(0)

                    For Each row In checkedRows

                        sQuery = " INSERT INTO [dbo].[shipment]([status],[cust_num],[cust_seq],[whse],[tracking_number]" & _
                                 " ,[bol_printed],[proforma_printed],[customs_invoice_printed],[cert_of_origin_printed]" & _
                                 " ,[pack_slip_printed],[invoice_printed],[RowPointer],[NoteExistsFlag],[CreatedBy]" & _
                                 " ,[UpdatedBy] ,[CreateDate],[RecordDate],[InWorkflow])" & _
                                 " VALUES('S'," & sTirnakEkle(row.Cells("DHCANB").Text) & ",0," & sTirnakEkle(row.Cells("DHA3CD").Text) & _
                                 "," & nSevkNo & ",0,0,0,0,0,0,NEWID(),0,'sa','sa',GETDATE(),GETDATE(),0)"

                        db.RunSql(sQuery)

                        Exit For

                    Next

                    sQuery = "SELECT shipment_id FROM shipment_mst WHERE tracking_number =" & nSevkNo
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then
                        shipmentId = dt.Rows(0)("shipment_id")
                    End If


                End If


                If bCekmeli Then


                    bEtiketDurum = True


                    sQuery = "Select PICKNO, DHCVNB, DDFCNB, MAX(DHIVNB) As SEVKTAR, sum(DDARQT) As SEVKMIK  " & _
                                 " From tmpIrsaliye" & _
                                 " Group By PICKNO, DHCVNB, DDFCNB"

                    dtIrsaliye = dbAccess.RunSql(sQuery)

                    If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                        For Each rowDt As DataRow In dtIrsaliye.Rows

                            sQuery = " Update TRCEKLIST" &
                                        " Set SEVKNO=" & shipmentId &
                                            " ,SEVKMIK=" & rowDt.Item("SEVKMIK").ToString &
                                            " ,SEVKTAR=" & sTirnakEkle(maxSevkTarihi.ToString("yyyy-MM-dd")) &
                                            " ,IRSNO=" & sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo) &
                                            " ,DURUM='3'" &
                                        " Where CEKLIST =" & rowDt.Item("PICKNO").ToString &
                                        " And ADCVNB=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) &
                                        " And ADFCNB=" & rowDt.Item("DDFCNB").ToString

                            db.RunSql(sQuery)

                        Next

                    End If

                    If bEksikSevkiyat Then

                        sQuery = "Update TRCEKLIST " & _
                                    " Set Durum='X' " & _
                                " Where CEKLIST in (" & sCekmeNolar & ")" & _
                                " And DURUM<>'3'"

                        db.RunSql(sQuery)

                        '  sQuery = "Update TRM_ETIKETDTY " & _
                        '    " Set Durum='X' " & _
                        '" Where PICKNO in (" & sCekmeNolar & ")" & _
                        '" And DURUM<>'3' AND KUTUETK=1"
                        '  db.RunSql(sQuery)

                        sQuery = "update coitem " & _
                                    " Set packed = 0 " & _
                                    " ,qty_packed = 0 " & _
                                " From trceklist " & _
                                " Where Ceklist in (" & sCekmeNolar & ")" & _
                                    " And Durum='X' " & _
                                    " And trceklist.ADCVNB=coitem.co_num " & _
                                    " And trceklist.ADFCNB=coitem.co_line "

                        db.RunSql(sQuery)


                        If sLookup("Count(*)", "TRCEKLIST", "CEKLIST in (" & sCekmeNolar & ")" & _
                                                            " And DURUM='X'") > 0 Then

                            sQuery = "update etiketdty" & _
                                        " Set etiketdty.durum='X' " & _
                                        " From trceklist " & _
                                        " Where Ordno = ADCVNB " & _
                                        " And Ordseq=ADFCNB" & _
                                        " And CEKLIST in (" & sCekmeNolar & ")" & _
                                        " And trceklist.DURUM='X' "

                            db.RunSql(sQuery)

                        End If

                        sQuery = "select CEKLIST, ADCVNB, ADFCNB, ADAITX,ADAQQT," & _
                                      " KMIK, PKSAY, PKSIR, isnull(SEVKMIK,0) As SEVKMIK" & _
                                  " from trceklist" & _
                                  " Where CEKLIST in (" & sCekmeNolar & ")" & _
                                  " And ADAQQT<>isnull(SEVKMIK,0)" & _
                                  " And DURUM='3'"

                        dt = db.RunSql(sQuery)

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each rowDt As DataRow In dt.Rows

                                sQuery = "SELECT palette_qty,palette_seq_box_qty FROM TR_itempack_edi WHERE item = " & sTirnakEkle(rowDt.Item("ADAITX"))
                                dtTemp = db.RunSql(sQuery)

                                If dtTemp.Rows(0)(0) <> 0 And dtTemp.Rows(0)(1) = 0 Then
                                    MessageBox.Show(rowDt.Item("ADAITX") & " malzemesinin palet tanımında hata var. Düzelttikten sonra tekrar deneyiniz.")
                                    Exit Sub
                                End If


                            Next
                        End If

                        sQuery = "select CEKLIST, ADCVNB, ADFCNB, ADAITX,ADAQQT," & _
                                        " KMIK, PKSAY, PKSIR, isnull(SEVKMIK,0) As SEVKMIK" & _
                                    " from trceklist" & _
                                    " Where CEKLIST in (" & sCekmeNolar & ")" & _
                                    " And ADAQQT<>isnull(SEVKMIK,0)" & _
                                    " And DURUM='3'"

                        dt = db.RunSql(sQuery)

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                            For Each rowDt As DataRow In dt.Rows


                                sQuery = " Exec EksikCekme " & rowDt.Item("CEKLIST").ToString()

                                db.RunSql(sQuery)

                            Next

                        End If



                        sQuery = "Update ETIKETDTY " & _
                                    " Set Durum='3' " & _
                                " Where PICKNO in (" & sCekmeNolar & ")" & _
                                " And isnull(DURUM,'')<>'X'"

                        db.RunSql(sQuery)

                    Else ' Eksik Sevkiyat Yoksa

                        If bEksikSevkiyat = False Then

                            sQuery = "Update TRCEKLIST " & _
                                        " Set Durum='3' " & _
                                    " Where CEKLIST in (" & sCekmeNolar & ")"

                            db.RunSql(sQuery)

                            If bEtiketDurum Then

                                sQuery = "Update ETIKETDTY " & _
                                            " Set Durum='3' " & _
                                        " Where PICKNO in (" & sCekmeNolar & ")"

                                db.RunSql(sQuery)

                            End If


                        End If

                    End If

                End If


                'CO_SHIP Update ediliyor....
                'Dim maxDate As String = ""

                'sQuery = " Select DHCVNB, DDFCNB , max(DHIVNB) DHIVNB " &
                '         " From tmpIrsaliye" &
                '         " Group By DHCVNB, DDFCNB"

                'dtTemp = db.RunSql(sQuery)

                'If dtTemp.Rows.Count > 0 Then
                '    maxDate = dtTemp.Rows(0)("DHIVNB")
                'End If


                sQuery = "Select DHCVNB, DDFCNB, co_release, date_seq, DHIVNB  " &
                            " From tmpIrsaliye"

                dtIrsaliye = dbAccess.RunSql(sQuery)

                If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                    For Each rowDt As DataRow In dtIrsaliye.Rows
                        '"yyyy-MM-dd HH:mm:ss.fff"
                        sQuery = " Update co_ship" & _
                                    " Set reason_text=" & sTirnakEkle(shipmentId) & _
                                    ", shipment_id = " & shipmentId & _
                                    " Where co_num=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                        " and co_line=" & rowDt.Item("DDFCNB").ToString & _
                                        " and co_release=" & rowDt.Item("co_release").ToString & _
                                        " and date_seq=" & rowDt.Item("date_seq").ToString & _
                                        " and  cast(ship_date as date) = " & sTirnakEkle(CDate(rowDt.Item("DHIVNB").ToString).ToString("yyyy-MM-dd")) & _
                                        " and isnull(reason_text ,'') = ''"

                        db.RunSql(sQuery)


                        sQuery = " Update matltran" & _
                                  " Set document_num=" & sTirnakEkle(shipmentId) & _
                                  " Where trans_type + ref_type ='SO' AND ref_num =" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                  " and ref_line_suf=" & rowDt.Item("DDFCNB").ToString & _
                                  " and document_num IS NULL "

                        db.RunSql(sQuery)
                    Next

                    ''************co_ship e insert ******************
                    'sQuery = " insert into trm_co_ship " & _
                    '            " Select * " & _
                    '            " from co_ship_mst " & _
                    '            " Where reason_text=" & sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo)

                    'db.RunSql(sQuery)

                End If

                'TRM_Shppack  Insert

                Dim tmpShipto, tmpCust, tmpShipnm, tmpKapi, tmpItnbr, _
                    tmpAmbkod, tmpKkod, tmpPkod, tmpSpkod, tmpKpkkod, _
                    tmpAGOB, tmpHCOB, tmpSTKOB, tmpPusno, tmpYukladr, _
                    tmpWhse, tmpSevkTar, tmpKumLevel, _
                    tmpMusteriUrunKodu, tmpMusteriUrunTanimi As String

                Dim tmpCustSeq As Integer

                Dim tmpPickdt, tmpKsay, tmpShipmik, tmpKmik, tmpHksay, tmpPsay, _
                    tmpPkmik, tmpPksir, tmpSpmik, tmpKpmik, tmpNetagr, tmpBrtagr, _
                    tmpHacim, tmpKapen, tmpKapboy, tmpKapyuk, tmpBrmAgr, tmpKpbrmik, _
                    tmpSpbrmik, tmpPltAgr, tmpKutuAgr, tmpKpkAgr, tmpSprAgr, _
                    tmpKutuHacmi, tmpPaletHacmi, tmpPrice, tmpSprHacmi, tmpKpkHacmi As Double
                '***************** TRM_Shppack Insert Döngüsü********************

                tmpAmbkod = ""

                sQuery = "Select PICKNO, DDAITX ,sum(DDARQT) As SEVKMIK , Max(DHIVNB) As DHIVNB, Max(DHA3CD) As DHA3CD ,  Max(DHCVNB) As DHCVNB " &
                            " From tmpIrsaliye" &
                            " Group By PICKNO, DDAITX"

                dtIrsaliye = dbAccess.RunSql(sQuery)

                If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                    For Each rowDt As DataRow In dtIrsaliye.Rows

                        tmpShipmik = rowDt.Item("SEVKMIK").ToString

                        tmpItnbr = rowDt.Item("DDAITX").ToString

                        tmpSevkTar = rowDt.Item("DHIVNB")

                        tmpPickNo = rowDt.Item("PICKNO").ToString

                        tmpWhse = rowDt.Item("DHA3CD").ToString

                        sQuery = "Select co.Cust_num, co.cust_seq, custaddr.name , co.cust_po, co.contact, co.whse, plantprm.b9cd , isnull(kumlevel,'1') As kumlevel" & _
                                    " From co" & _
                                    " Left Join Plantprm On co.cust_seq=Plantprm.cust_seq And co.cust_num=Plantprm.canb" & _
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


                        sQuery = "Select  c.u_m, c.unit_weight, c.price" & _
                                " From coitem c" & _
                                " Where c.co_num=" & sTirnakEkle(rowDt.Item("DHCVNB").ToString) & _
                                " And c.item=" & sTirnakEkle(tmpItnbr)

                        dtTemp = db.RunSql(sQuery)


                        GetRowInfo(tmpSTKOB, dtTemp, 0, "u_m")

                        GetRowInfo(tmpBrmAgr, dtTemp, 0, "unit_weight")

                        GetRowInfo(tmpPrice, dtTemp, 0, "price")

                        sQuery = " Select ITEM, REFADI" & _
                                    " From KONTRTPF" & _
                                    " Where CUST=" & sTirnakEkle(tmpCust) & _
                                    " And BZMITM=" & sTirnakEkle(tmpItnbr) & _
                                    " And SHIPTO=" & sTirnakEkle(tmpShipto) & _
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

                            sQuery = "select MAx(KKOD) KKOD , Max(KMIK) KMIK," & _
                                                " Max(PKOD) PKOD, Max(PKMIK) PKMIK, " & _
                                                " Max(PKSIR) PKSIR " & _
                                            " from ETIKETDTY " & _
                                            " Where pickno =" & tmpPickNo & _
                                                " and Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                                " and kutuetk=1 " & _
                                            " Group By Pickno,Itnbr"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpKkod, dtTemp, 0, "KKOD")
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

                            sQuery = "Select Count(ETKSERINO) As KSAY " & _
                                        " From ETIKETDTY" & _
                                        " Where PICKNO =" & tmpPickNo & _
                                        " And Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                        " And Durum='3' And KUTUETK='1'"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpKsay, dtTemp, 0, "KSAY")

                            '*****
                            sQuery = "Select Count(A.Pltno) As PSAY From " & _
                                    "(Select Distinct pltno " & _
                                        " From ETIKETDTY" & _
                                        " Where PICKNO=" & tmpPickNo & _
                                        " And Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                        " And Durum='3' And KUTUETK='1'" & _
                                        " And Pltno>0 " & _
                                        " ) A"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpPsay, dtTemp, 0, "PSAY")

                            sQuery = "Select Count(ETKSERINO) As HKSAY " & _
                                        " From ETIKETDTY" & _
                                        " Where PICKNO=" & tmpPickNo & _
                                        " And Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                        " And Durum='3' And PLTNO='-1' And KUTUETK=1"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpHksay, dtTemp, 0, "HKSAY")

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

                        If bCekmeli Then

                            sQuery = "select MAx(IBRMAGR) IBRMAGR " & _
                                      " from ETIKETDTY " & _
                                      " Where pickno =" & tmpPickNo & _
                                          " and Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                          " and kutuetk=1 " & _
                                      " Group By Pickno,Itnbr"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpBrmAgr, dtTemp, 0, "IBRMAGR")

                            tmpNetagr = tmpShipmik * tmpBrmAgr

                        Else

                            tmpNetagr = tmpShipmik * tmpBrmAgr

                        End If


                        If bCekmeli Then

                            sQuery = " Select BRMHC " & _
                              " From ITEMDIM" & _
                              " Where ITNBR=" & sTirnakEkle(tmpKkod)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpKutuHacmi, dtTemp, 0, "BRMHC")

                            sQuery = "select MAx(KBRMAGR) KBRMAGR " & _
                                           " from ETIKETDTY " & _
                                           " Where pickno =" & tmpPickNo & _
                                               " and Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                               " and kutuetk=1 " & _
                                           " Group By Pickno,Itnbr"

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpKutuAgr, dtTemp, 0, "KBRMAGR")


                        Else
                            sQuery = " Select BRMAG, BRMHC" & _
                              " From ITEMDIM" & _
                              " Where ITNBR=" & sTirnakEkle(tmpKkod)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(tmpKutuAgr, dtTemp, 0, "BRMAG")

                            GetRowInfo(tmpKutuHacmi, dtTemp, 0, "BRMHC")
                        End If
                      

                        If tmpPsay > 0 Then

                            If bCekmeli Then

                                sQuery = " Select BRMHC " & _
                                  " From ITEMDIM" & _
                                  " Where ITNBR=" & sTirnakEkle(tmpPkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpPaletHacmi, dtTemp, 0, "BRMHC")

                                sQuery = "select MAx(PBRMAGR) PBRMAGR " & _
                                               " from ETIKETDTY " & _
                                               " Where pickno =" & tmpPickNo & _
                                                   " and Itnbr=" & sTirnakEkle(tmpItnbr) & _
                                                   " and pltetk=1 " & _
                                               " Group By Pickno,Itnbr"

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpPltAgr, dtTemp, 0, "PBRMAGR")

                            Else
                                sQuery = " Select BRMAG, BRMHC" & _
                                       " From ITEMDIM" & _
                                       " Where ITNBR=" & sTirnakEkle(tmpPkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(tmpPltAgr, dtTemp, 0, "BRMAG")

                                GetRowInfo(tmpPaletHacmi, dtTemp, 0, "BRMHC")

                            End If
                           

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
                        '" YUKLADR,KAP_EN,KAP_BOY,KAP_YUK,TRNCODE, " & _
                        'sTarih(CDate(tmpSevkTar).ToString("yyyy-MM-dd")) & "," & _


                        sQuery = "SELECT * FROM PLANTPRM " & _
                            " WHERE ISNULL(ASNPRM,'0') <> 1 AND CANB = '" & tmpCust & "'  AND cust_seq = " & tmpCustSeq

                        dtTemp = db.RunSql(sQuery)

                        If dtTemp.Rows.Count > 0 Then
                            ayniyat = True
                        Else
                            ayniyat = False
                        End If



                        If tmpKapi.Length > 12 Then
                            tmpKapi = tmpKapi.Substring(0, 12)
                        End If

                        '15.02.24 CEMAL GULÇAY TALEBİ
                        'sTirnakEkle(Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")) & "," &
                        sQuery = " INSERT INTO Shppack" &
                            " (SHPNO,CUST,PICKNO,PICKDT,SHIPTO, Cust_Seq, SHIPNM," &
                            " KAPI,ITNBR,SEVKTAR,IRSNO,AMBKOD,SHIPMIK,KKOD,KSAY," &
                            " KMIK,HKSAY,PKOD,PSAY,PKMIK,PKSIR,SPKOD,SPMIK,KPKOD," &
                            " KPMIK,NETAGR,BRTAGR,HACIM,AGOB,HCOB,STKOB,PUSNO," &
                            " YUKLADR,KAP_EN,KAP_BOY,KAP_YUK, " &
                            " CRDDTE,CRDTIM,CRDUSR, USERF3, USERF4, USERF5, whse, " &
                            " MURNKOD, MURNTNM, Plaka, Carrier, NAVLUNNO, BeyanNo, BeyanTarihi) " &
                        " VALUES (" &
                        shipmentId & "," &
                        sTirnakEkle(tmpCust) & "," &
                        tmpPickNo & "," &
                        IIf(tmpPickdt <> 0, sTarih(DateConvertVb(tmpPickdt).ToString("yyyy-MM-dd")), "Null") & "," &
                        sTirnakEkle(tmpShipto) & "," &
                        tmpCustSeq & "," &
                        sTirnakEkle(tmpShipnm) & "," &
                        sTirnakEkle(tmpKapi) & "," &
                        sTirnakEkle(tmpItnbr) & "," &
                        sTirnakEkle(CDate(maxSevkTarihi).ToString("yyyy-MM-dd")) & "," &
                        sTirnakEkle(sIrsaliyeSeri.IrsaliyeNo) & "," &
                        sTirnakEkle(tmpAmbkod) & "," &
                        tmpShipmik & "," &
                        sTirnakEkle(tmpKkod) & "," &
                        tmpKsay & "," &
                        tmpKmik & "," &
                        tmpHksay & "," &
                        sTirnakEkle(tmpPkod) & "," &
                        tmpPsay & "," &
                        tmpPkmik & "," &
                        tmpPksir & "," &
                        sTirnakEkle(tmpSpkod) & "," &
                        tmpSpmik & "," &
                        sTirnakEkle(tmpKpkkod) & "," &
                        tmpKpmik & "," &
                        tmpNetagr & "," &
                        tmpBrtagr & "," &
                        tmpHacim & "," &
                        sTirnakEkle(tmpAGOB) & "," &
                        sTirnakEkle(tmpHCOB) & "," &
                        sTirnakEkle(tmpSTKOB) & "," &
                        sTirnakEkle(tmpPusno) & "," &
                        tmpYukladr & "," &
                        tmpKapen & "," &
                        tmpKapboy & "," &
                        tmpKapyuk & "," &
                        sTirnakEkle(Now.ToString("yyyy-MM-dd")) & "," &
                        Now.ToString("HHmmss") & "," &
                        sTirnakEkle(KullaniciAdi) & "," &
                        sTirnakEkle(sIrsaliyeSeri.TeslimAlan) & "," &
                        sTirnakEkle(sIrsaliyeSeri.NavlunFaturaNo) & "," &
                        sTirnakEkle(sIrsaliyeSeri.SeferNo) & "," &
                        sTirnakEkle(tmpWhse) & "," &
                        sTirnakEkle(tmpMusteriUrunKodu) & "," &
                        sTirnakEkle(tmpMusteriUrunTanimi) & "," &
                        sTirnakEkle(sIrsaliyeSeri.Plaka) & "," &
                        sTirnakEkle(sIrsaliyeSeri.Carrier) & "," &
                        sTirnakEkle(sIrsaliyeSeri.sNavlunNo) & "," &
                        sTirnakEkle(sIrsaliyeSeri.BeyanNo) & "," &
                        sTirnakEkle(sIrsaliyeSeri.BeyanTarihi.ToString("yyyy-MM-dd")) &
                        ")"



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
                            '" AND AENBK=" & sTirnakEkle(sCustSeq) & _
                            sQuery = " insert into OFFITEMBL " & _
                                        " (CANBK, B9CDK, GATE, AITXK, DDARQK, ASNKUM, [USER], TAR, UTIME,AENBK)" & _
                                        " Values (" & _
                                        sTirnakEkle(tmpCust) & "," & _
                                        sTirnakEkle(tmpShipto) & "," & _
                                        sTirnakEkle(IIf(tmpKumLevel = "2", tmpKapi, "")) & "," & _
                                        sTirnakEkle(tmpItnbr) & "," & _
                                        tmpShipmik & "," & _
                                        "0" & "," & _
                                        sTirnakEkle(KullaniciAdi) & "," & _
                                        sTirnakEkle(Now.ToString("yyyy-MM-dd")) & "," & _
                                        Now.ToString("HHmmss") & "," & _
                                        tmpCustSeq & ")"


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

                                tmpPusno = sLookup("cust_po", "co", "co_num=" & sTirnakEkle(rowDty.Item("DHCVNB").ToString))

                                sQuery = " INSERT INTO Shpdty" & _
                                        " ( SHPNO,PICKNO,SHIPTO," & _
                                        " KAPI,ITNBR, SEQNO, MIKTAR, " & _
                                        " STKOB,PRICE,FYTKTS,ORDNO, RANNO, Ambkod ) " & _
                                    " VALUES (" & _
                                    shipmentId & "," & _
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
                                    sTirnakEkle(rowDty.Item("Ambkod").ToString) & _
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

                If db.Transaction Then
                    db.CommitTransaction()
                End If


            Catch ex As Exception

                If db.Transaction Then
                    db.RollbackTransaction()
                End If

                Cursor = Cursors.Default
                Throw ex

            Finally
                Cursor = Cursors.Default
            End Try



            If ayniyat Then

                '********************Ayniyat Bilgileri 
Yazdir:
                Dim Malzeme, Tanim, Ambar, Loc, Whse, OlcuBirimi As String
                Dim Miktar, EldekiMiktar As Double

                dbAccess.RunSql("Delete From Ayniyat")

                sQuery = " Select Whse, STKOB As U_M , KKOD, SUM(KSAY-HKSAY) As KSAY" & _
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

                                sQuery = "SELECT description,ISNULL(Uf_AYNTIP,'') AYNTIP FROM item WHERE item='" & Malzeme & "'"
                                dt = db.RunSql(sQuery)

                                If dt.Rows.Count > 0 Then
                                    Tanim = dt.Rows(0)("description").ToString
                                    AYNTIP = dt.Rows(0)("AYNTIP").ToString
                                End If

                                Miktar = .Item("KSAY").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                If AYNTIP = "1" Then
                                    AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "K")
                                End If


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

                                sQuery = "SELECT description,ISNULL(Uf_AYNTIP,'') AYNTIP  FROM item WHERE item='" & Malzeme & "'"
                                dt = db.RunSql(sQuery)

                                If dt.Rows.Count > 0 Then
                                    Tanim = dt.Rows(0)("description").ToString
                                    AYNTIP = dt.Rows(0)("AYNTIP").ToString
                                End If

                                Miktar = .Item("KSAY").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                If AYNTIP = "1" Then

                                    AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "H")

                                End If
                            End If

                        End With

                    Next

                End If




                sQuery = " select  HOUSE As Whse,'AD' as U_M , PKOD  , count(etkserino) As PSAY" & _
                        " from etiketdty " & _
                        " where pickno in ( " & sCekmeNolar & ")" & _
                        " and pltetk=1" & _
                        " and Durum<>'X'" & _
                        " group by HOUSE, Pkod"
                '    ' " And Itnbr in (" & sKontrolMalzeme & ")" & _

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowDty As DataRow In dtTemp.Rows

                        With rowDty

                            Ambar = .Item("WHSE").ToString

                            OlcuBirimi = .Item("U_M").ToString

                            If .Item("PKOD").ToString <> "" Then

                                Malzeme = .Item("PKOD").ToString

                                sQuery = "SELECT description,ISNULL(Uf_AYNTIP,'') AYNTIP FROM item WHERE item='" & Malzeme & "'"
                                dt = db.RunSql(sQuery)

                                If dt.Rows.Count > 0 Then
                                    Tanim = dt.Rows(0)("description").ToString
                                    AYNTIP = dt.Rows(0)("AYNTIP").ToString
                                End If

                                Miktar = .Item("PSAY").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                If AYNTIP = "1" Then

                                    AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "P")

                                End If
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
                            " left join Shppack s" & _
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

                                sQuery = "SELECT description,ISNULL(Uf_AYNTIP,'') AYNTIP FROM item WHERE item='" & Malzeme & "'"
                                dt = db.RunSql(sQuery)

                                If dt.Rows.Count > 0 Then
                                    Tanim = dt.Rows(0)("description").ToString
                                    AYNTIP = dt.Rows(0)("AYNTIP").ToString
                                End If

                                Miktar = .Item("KPMIK").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                If AYNTIP = "1" Then

                                    AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "T")

                                End If

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
                                           " left join Shppack s" & _
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

                                sQuery = "SELECT description,ISNULL(Uf_AYNTIP,'') AYNTIP FROM item WHERE item='" & Malzeme & "'"
                                dt = db.RunSql(sQuery)

                                If dt.Rows.Count > 0 Then
                                    Tanim = dt.Rows(0)("description").ToString
                                    AYNTIP = dt.Rows(0)("AYNTIP").ToString
                                End If

                                Miktar = .Item("SPMIK").ToString

                                MiktarAl(Ambar, Malzeme, Tanim, EldekiMiktar)

                                If AYNTIP = "1" Then

                                    AyniyatInsert(nSevkNo, Ambar, Malzeme, Tanim, OlcuBirimi, Miktar, EldekiMiktar, "S")

                                End If
                            End If

                        End With

                    Next

                End If

                ''  GoTo AYNİYAT

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

            End If


            '************************************ AYNIYAT ***************************************************
AYNİYAT:
            dbAccess.RunSql("Delete From ANSIRSALIYE")

            sQuery = " Select * " & _
                    " From Shppack s " & _
                    " Left join custaddr c On s.cust=c.cust_num and c.cust_seq=s.cust_seq " & _
                    " Left join customer c1 On s.cust=c1.cust_num and c1.cust_seq=s.cust_seq" & _
                    " Left Join ITMPACK i On s.Itnbr=i.Itnbr And s.Ambkod=i.Ambkod " & _
                    " Where SHPNO=" & shipmentId

            dt = db.RunSql(sQuery)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each rowdt As DataRow In dt.Rows

                    sVergiNo = sLookup("tax_reg_num1", "customer", " cust_num=" & sTirnakEkle(rowdt.Item("cust_num").ToString) & _
                                                                                                " And cust_seq=0")

                    sVergiDairesi = sLookup("Uf_TaxOffice", "customer", " cust_num=" & sTirnakEkle(rowdt.Item("cust_num").ToString) & _
                                                                                                " And cust_seq=0")

                    'If rdbYenidenBasim.Checked Then

                    sQuery = "SELECT ksay FROM TR_IRSPACK WHERE SHPNO=" & nSevkNo
                    dt = db.RunSql(sQuery)

                    If sAmbalaj = "" Then
                        If dt.Rows.Count > 0 Then
                            For Each row2 As DataRow In dt.Rows
                                If Not sAmbalaj.Contains(row2.Item("ksay").ToString) Then
                                    sAmbalaj = sAmbalaj & row2.Item("ksay").ToString & " " & "/" & " "
                                End If
                            Next
                            sAmbalaj = sAmbalaj.Substring(0, sAmbalaj.Length - 3)
                        End If
                    End If

                    'End If

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
                                "PLAKANO,SEFERNO,NAVLUNNO,TESLIMEDEN, " & _
                                "TESLIMALAN,ACIKLAMA,NAKLIYECI, KutuAdedi, " & _
                                "PaletAdedi,Country,City,County,AMBALAJ)" & _
                            "Values (" & _
                            shipmentId & "," & _
                            sTirnakEkle(rowdt.Item("SEVKTAR")) & "," & _
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
                            sTirnakEkle(sLookup("ENGNO", "ITEMASA", "ITNBR=" & sTirnakEkle(rowdt.Item("ITNBR").ToString))) & "," & _
                            sTirnakEkle(sLookup("REVNO", "ITMPACK", "ITNBR=" & sTirnakEkle(rowdt.Item("ITNBR").ToString) & " And AMBKOD=" & sTirnakEkle(rowdt.Item("AMBKOD").ToString))) & "," & _
                            sTarih(sLookup("REVTAR", "ITMPACK", "ITNBR=" & sTirnakEkle(rowdt.Item("ITNBR").ToString) & " And AMBKOD=" & sTirnakEkle(rowdt.Item("AMBKOD").ToString))) & "," & _
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
                            sTirnakEkle(sIrsaliyeSeri.SeferNo) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.NavlunFaturaNo) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.TeslimEden) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.TeslimAlan) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.Aciklama) & "," & _
                            sTirnakEkle(sIrsaliyeSeri.Nakliyeci) & "," & _
                            sIrsaliyeSeri.kutuAdedi & "," & _
                            sIrsaliyeSeri.paletAdedi & "," & _
                            sTirnakEkle(rowdt.Item("Country").ToString) & "," & _
                            sTirnakEkle(rowdt.Item("City").ToString) & "," & _
                            sTirnakEkle(rowdt.Item("County").ToString) & "," & _
                            sTirnakEkle(sAmbalaj) & _
                            ")"

                    dbAccess.RunSql(sQuery)

                Next

                If sIrsaliyeSeri.Carrier = "GEFC" Then

                    sQuery = "SELECT SHPNO,ITNBR,SHIPMIK,case when (select count(*) from itemwhse where itemwhse.item = SHPPACK.ITNBR and itemwhse.whse = 'GEFC') = 0 then 0 " & _
                        "  else ISNULL(KonsinyeDurum ,1) end KonsinyeDurum, " & _
                        " case when KonsinyeDurum = 2 THEN 'İşlem Tamamlandı' " & _
                        " when  (select count(*) from itemwhse where itemwhse.item = SHPPACK.ITNBR and itemwhse.whse = 'GEFC') = 0 then ' GEFC Ambarı İçin Malzeme Ambar Kaydı Yok' " & _
                        " ELSE '' END AS Sonuc FROM SHPPACK WHERE SHPNO = " & nSevkNo
                    dtTemp = db.RunSql(sQuery)
                    'frmKonsinyeBilgileri.dtKonsinye.Clear()
                    frmKonsinyeBilgileri.dtKonsinye = dtTemp
                    frmKonsinyeBilgileri.ShowDialog()

                End If

                sQuery = " SELECT * FROM ANSIRSALIYE "
                dt = dbAccess.RunSql(sQuery)

                If sInitialCatalog = "SLELB_App" Then
                    RaporCagirOzet("SLIrsaliye_SLELB.rpt", , , "SLIRSALIYE", True, , , dt)
                Else
                    RaporCagirOzet("SLIrsaliye.rpt", , , "SLIRSALIYE", True, , , dt)
                End If


                'myPrinters.SetDefaultPrinter(ReadConfig("DefaultPrinterSevkiyat"))

                'Dim Str As String
                'Dim GetDateStr As String
                'Str = " select GETDATE() "
                'dt = db.RunSql(Str)
                'GetDateStr = CDate(dt.Rows(0)(0)).ToString("yyyy-MM-dd HH:mm:ss.fff")
                'Dim PrtDocGeneral As New PrintDocument
                'Do

                '    myPrinters.SetDefaultPrinter(ReadConfig("DefaultPrinterSevkiyat"))

                '    Str = " select DATEDIFF(ss,'" & GetDateStr & "',GETDATE()) "
                '    dt = db.RunSql(Str)
                '    If CInt(dt.Rows(0)(0)) >= 5 Then
                '        Exit Do
                '    End If

                '    Application.DoEvents()
                'Loop Until PrtDocGeneral.PrinterSettings.PrinterName = ReadConfig("DefaultPrinterSevkiyat") '"doPDF v7" '.IndexOf("ZEBRA").ToString <> "-1"

                btnSorgula_Click(sender, e)

            End If

            ayniyat = False

        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

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

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Public Sub MiktarAl(ByVal Ambar As String, ByVal Malzeme As String, ByRef Tanim As String, ByRef Miktar As Double)

        Try

            sQuery = "Select i.item , i.description, Sum(isnull(mohtq,0)) - Sum(isnull(count_qty,0)) As Miktar " & _
                                            "From item i " & _
                                            " Left Join dcitemsum d on d.item=i.item  And d.whse=" & sTirnakEkle(Ambar) & _
                                            " Left Join Itembl b On i.item=b.Itnbr And b.House=" & sTirnakEkle(Ambar) & _
                                            " Left Join ITEMDIM m on m.itnbr=i.item " & _
                                            "Where i.item=" & sTirnakEkle(Malzeme) & _
                                                " And m.ayntip=1 " & _
                                            "Group by  i.item , i.description"

            dt = db.RunSql(sQuery)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                'Tanim = dt.Rows(0).Item("description").ToString

                Miktar = dt.Rows(0).Item("Miktar").ToString

            Else

                Tanim = ""

                Miktar = 0

            End If

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub txtAmbar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmbar1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct whse as 'Malzeme',name as 'Tanim'" & _
                            " From whse" & _
                            " Order By 1"

            FindFormCagir(ssorgu, "Malzeme", "Tanim", txtAmbar1.Text, "")

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub

    Private Sub btnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckedAll.Click
        GridEX1.CheckAllRecords()
    End Sub

    Private Sub btnUnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnCheckedAll.Click
        GridEX1.UnCheckAllRecords()
    End Sub

    Private Sub AddConditionalFormatting()
        'Imports Janus.Windows.GridEX is used in this module

        'Adding a condition using Discontinued field

        Dim fc As Janus.Windows.GridEX.GridEXFormatCondition

        fc = New Janus.Windows.GridEX.GridEXFormatCondition(GridEX1.RootTable.Columns("DDAAGP"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)

        fc.FormatStyle.FontBold = Janus.Windows.GridEX.TriState.True

        fc.FormatStyle.ForeColor = Color.Red

        GridEX1.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub btnExceleAktar_Click(sender As System.Object, e As System.EventArgs) Handles btnExceleAktar.Click
        If dt.Rows.Count = 0 Then
            MessageBox.Show("Aktarılacak veri bulunamadı.")
            Exit Sub
        End If
        ExceleAktar(dt)
    End Sub

End Class