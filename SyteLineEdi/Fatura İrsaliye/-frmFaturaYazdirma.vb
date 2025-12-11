Imports System.IO
Imports System.Text

Public Class frmFaturaYazdirma

#Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

#End Region 'Fields

#Region "Methods"

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click

        Try

            sQuery = " Select inv_num, inv_date, sevkno, MAX(cust_num) As cust_num, MAX(name) As name,  MAX(country) As country , " & _
                            " MAX(curr_code) curr_code, item, MAX(description) As description, " & _
                            " sum(qty_shipped) As qty_shipped, MAX(dtyPrice) As dtyPrice , MAX(Shipto) As Shipto,   " & _
                            " MAX(KAPI) As KAPI, MAX(IRSNO) As IRSNO,  MAX(SEFERNO) As SEFERNO, MAX(NAVLUNNO) As NAVLUNNO," & _
                            " MAX(MURNKOD) As MURNKOD, MAX(dtyPrice) As cont_price, " & _
                            " MAX(itemtax_code) As itemtax_code, MAX(ship_date) As ship_date, MAX(Pusno) As Pusno," & _
                            " MAX(terms_code) As terms_code, MAX(u_m) As u_m" & _
                       " From FaturaPrn " & _
                       " Where 1=1 " & _
                            " And PRNSTAT " & IIf(rdbIlkBasim.Checked, " Not like '*", "like '*") & "%'"

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery &= "  and cust_num='" & txtMusteri1.Text & "'"

                Else

                    sQuery &= "  and cust_num>='" & txtMusteri1.Text & "'" & _
                                        " and cust_num<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtAmbar1.Text <> "" Then

                sQuery &= " and whse='" & txtAmbar1.Text & "'"

            End If

            If txtTeslimAlan1.Text <> "" Then

                If txtTeslimAlan2.Text = "" Then

                    sQuery &= " and shipto='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery &= "  and shipto>='" & txtTeslimAlan1.Text & "'" & _
                                        " and shipto<='" & txtTeslimAlan2.Text & "'"
                End If

            End If

            If txtKapi1.Text <> "" Then

                If txtKapi2.Text = "" Then

                    sQuery &= " and KAPI='" & txtKapi1.Text & "'"

                Else

                    sQuery &= " and KAPI>='" & txtKapi1.Text & "'" & _
                                        "    and KAPI<='" & txtKapi2.Text & "'"

                End If

            End If

            If txtKullanici.Text <> "" Then

                sQuery &= "    and Kullanici like '%" & txtKullanici.Text & "%'"

            End If

            If chkTarihAraligi.Checked Then

                sQuery &= " and dbo.Dateserial(datepart(Year,CreateDate),datepart(Month,CreateDate),datepart(Day,CreateDate)) >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd")) & _
                                    " and dbo.Dateserial(datepart(Year,CreateDate),datepart(Month,CreateDate),datepart(Day,CreateDate)) <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy/MM/dd"))

            Else

                sQuery &= " and dbo.Dateserial(datepart(Year,CreateDate),datepart(Month,CreateDate),datepart(Day,CreateDate)) =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy/MM/dd"))

            End If

            sQuery &= " Group By inv_num, inv_date, sevkno, item"

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                         dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                Duzenle(GridEX1)
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

        Dim sSevkNolar As String = ""
        Dim sIrsNolar As String = ""
        Dim sFatTipi As String = "Y"
        Dim FaturaTarihi As Date
        Dim FaturaSistemNo As String = ""

        Try

            dbAccess.RunSql("Delete From ANSFATURA")
            dbAccess.RunSql("Delete From ANSFATTXT")

            If checkedRows.Length = 0 Then
                MessageBox.Show("Lütfen  Seçim Yapınız!")
                Exit Sub
            Else
                'dbAccess.RunSql("Delete From ANSFATURA")
                'dbAccess.RunSql("Delete From ANSFATTXT")

                For Each row In checkedRows
                    If Not sSevkNolar.Contains(row.Cells("SEVKNO").Text) Then
                        sSevkNolar = IIf(sSevkNolar = "", "", sSevkNolar & ",") & row.Cells("SEVKNO").Text
                    End If

                    If Not sIrsNolar.Contains(row.Cells("IRSNO").Text & "-" & CDate(row.Cells("ship_date").Text).ToString("dd-MM-yyyy")) Then
                        sIrsNolar = IIf(sIrsNolar = "", "", sIrsNolar & "/") & row.Cells("IRSNO").Text & "-" & CDate(row.Cells("ship_date").Text).ToString("dd-MM-yyyy")
                    End If

                Next

            End If

            'Dim FaturaNo As Double
            Dim FaturaTutar As Double = 0
            Dim KdvOrani As Integer
            Dim BirimFiyat As Double
            Dim KdvTutar As Double
            Dim sParaBirimi As String
            Dim sKurus As String
            'Dim sFaturaNo As String
            'FaturaNo = SeriNoAl("FTR")
            'sFaturaNo = CStr(FaturaNo).PadLeft(10, "0")

            Dim sDIBNO As String = ""
            Dim sDIBNO2 As String = ""

            sDIBNO = sLookup("DIBNO", "DIBPF", "")
            sDIBNO2 = sLookup("DIBNO1", "DIBPF", "")

            sQuery = " Select inv_num, inv_date , sevkno, Max(cust_num) As cust_num, Max(name) As Name , Max(country) As country , " & _
                            " Max(curr_code) curr_code, item, Max(description) As description, " & _
                            " sum(qty_shipped) As qty_shipped, Max(dtyPrice) As dtyPrice , Max(Shipto) As Shipto,   " & _
                            " Max(KAPI) As KAPI, Max(IRSNO) As IRSNO,  Max(SEFERNO) As SEFERNO, Max(NAVLUNNO) As NAVLUNNO," & _
                            " Max(MURNKOD) As MURNKOD, dtyPrice As cont_price, " & _
                            " MAx(itemtax_code) As itemtax_code, MAx(ship_date) As ship_date, MAx(Pusno) As Pusno," & _
                            " MAx(terms_code) As terms_code, MAx(u_m) As u_m," & _
                            " pickno, MAx(shiptoname) As shiptoname, MAx(kntrt) As kntrt," & _
                            " MAx(kkod) As kkod, Sum(psay) As psay, Sum(ksay) As ksay, " & _
                            " MAx(pkod) As pkod, Sum(hksay) As hksay," & _
                            " MAx(spkod) As spkod, Sum(spmik) As spmik, Max(kpkod) As kpkod, " & _
                            " Sum(kpmik) As kpmik, Sum(netagr) As netagr, Sum(brtagr) As brtagr," & _
                            " Sum(hacim) As hacim, MAx(murntnm) As murntnm," & _
                            " MAx(mambkod) As mambkod, MAx(mkkod) As mkkod, MAx(mpkod) As mpkod, MAx(Addr##1) As Addr##1," & _
                            " MAx(Addr##2) As Addr##2, MAx(Addr##3) As Addr##3, MAx(Addr##4) As Addr##4," & _
                            " MAx(city) As city, MAx(zip) As zip, MAx(dunsid) As dunsid, MAx(tax_rate) As tax_rate, Max(sales_tax) As sales_tax," & _
                            " MAx(tax_reg_num1) As tax_reg_num1, Max(Uf_TaxOffice) As Uf_TaxOffice, MAx(termnm) As termnm, Min(FaturaSeriNo) As FaturaSeriNo, " & _
                            " MAx(AddrShip##1) As AddrShip##1, MAx(AddrShip##2) As AddrShip##2, MAx(AddrShip##3) As AddrShip##3 , MAx(AddrShip##4) As AddrShip##4 , MAX(lang_code) AS lang_code," & _
                            " Sum(KAPTUT) As KAPTUT, Max(Kur) As Kur,MAX(FATURANOT) AS FATURANOT " & _
                        " From FaturaPrnSum " & _
                        " Where SEVKNO IN (" & sSevkNolar & ")"

            sQuery = sQuery & " Group By  inv_num, inv_date , sevkno, item, pickno,dtyPrice"

            dt = db.RunSql(sQuery)
            sFaturaData.Brut = 0
            sFaturaData.Net = 0

            If Not (dt Is Nothing) AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    With dt.Rows(i)

                        sFatTipi = sTirnakEkle(IIf(.Item("lang_code").ToString = "TRK", "Y", "I"))

                        sFaturaData.Brut += .Item("brtagr")

                        sFaturaData.Net += .Item("netagr")

                        If i = dt.Rows.Count - 1 Then

                            sFaturaSeri.SeriNo = .Item("FaturaSeriNo").ToString
                            sFaturaSeri.FaturaNotu = .Item("FATURANOT").ToString
                            frmFaturaSeri.ShowDialog()

                            If sFaturaSeri.Iptal = True Then

                                dbAccess.RunSql("Delete From ANSFATURA")
                                dbAccess.RunSql("Delete From ANSFATTXT")

                                btnSorgula_Click(sender, e)

                                Exit Sub

                            End If

                            If (sFatTipi = "N'I'") Then

                                Dim frm As New frmFaturaYazdirmaData

                                frm.ShowDialog()

                                If sFaturaData.Iptal = True Then

                                    dbAccess.RunSql("Delete From ANSFATURA")
                                    dbAccess.RunSql("Delete From ANSFATTXT")

                                    btnSorgula_Click(sender, e)

                                    Exit Sub

                                End If

                            End If

                        End If

                        BirimFiyat = .Item("dtyPrice").ToString


                        sQuery = "select NXT_LVL_CODE, TAX_RATETevkifat FROM TR_TevkifatTipOran WHERE tax_code = " & sTirnakEkle(.Item("itemtax_code").ToString)
                        dtTemp = db.RunSql(sQuery)

                        Dim tevkifatCode As String = ""
                        Dim tevkifatRate As String = ""

                        If dtTemp.Rows.Count > 0 Then
                            tevkifatCode = dtTemp.Rows(0)("NXT_LVL_CODE")
                            tevkifatRate = dtTemp.Rows(0)("TAX_RATETevkifat")
                        End If


                        sQuery = " Insert Into ANSFATURA " & _
                                    "(" & _
                                    " FATTIP, INVNO, INVTAR, DHZ969,  " & _
                                    " COKZNB, SEVKTAR, DHCANB, DHB9CD, " & _
                                    " DHBYTX, DDAITX, KAPI, MIKTAR, " & _
                                    " KONTNO, KKOD, KSAY, KAPSAYI, PKOD, " & _
                                    " PSAY, SPKOD, SPMIK, KPKOD, " & _
                                    " KPMIK, NETAGR, BRTAGR, HACIM, " & _
                                    " AGOB, HCOB, STKOB, PRICE, TUTAR, " & _
                                    " MURNKOD, MURNTNM, MAMBKOD, MKKOD, " & _
                                    " MPKOD, CUSNM, FEAAC4, FEAAC5, " & _
                                    " FEAAC6, FEZ9JV, FEZ9JW, FEAAC7, " & _
                                    " FEAAC8, FEAADW, DUNSNO, FERIDX, " & _
                                    " GFZ9VC, C9CEZ8, C9NRHV, TERMCD, " & _
                                    " TERMNM, NAVTUT, SIGTUT, IRSALIYE1, " & _
                                    " IRSALIYE2, PUSNO, DHZ9JR, KAPTUT, " & _
                                    " FATURANO, MUSFATKODU, KOLIAYN, PALETAYN," & _
                                    " DHBZTX, DHB0TX, DHB1TX, ALZ9HH ,PAYMENTTERMCD, " & _
                                    " PAYMENTTERMNM, KUTUFYT, PALETFYT, KAPAKFYT ,FaturaNot, DIBNO, DIBNO1,  " & _
                                    " FDetay1, FDetay2, FBankDetay1, FBankDetay2, FAccountNo1, FAccountNo2 ,FBrut, FNet, FSevkIrsaliyesi, TCODE, TRATE  " & _
                                    ")"
                        sQuery = sQuery & " Values (" & _
                                sFatTipi & "," & _
                                .Item("inv_Num").ToString & "," & _
                                sTirnakEkle(CDate(.Item("inv_date").ToString).ToString("yyyy-MM-dd")) & "," & _
                                .Item("SEVKNO").ToString & "," & _
                                .Item("pickno").ToString & "," & _
                                sTirnakEkle(CDate(.Item("ship_date").ToString).ToString("yyyy-MM-dd")) & "," & _
                                sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                sTirnakEkle(.Item("shipto").ToString) & "," & _
                                sTirnakEkle(.Item("shiptoname").ToString) & "," & _
                                sTirnakEkle(.Item("item").ToString) & "," & _
                                sTirnakEkle(.Item("KAPI").ToString) & "," & _
                                .Item("qty_shipped").ToString & "," & _
                                sTirnakEkle(.Item("KNTRT").ToString) & "," & _
                                sTirnakEkle(.Item("KKOD").ToString) & "," & _
                                .Item("KSAY").ToString & "," & _
                                CInt(.Item("PSAY").ToString) + CInt(.Item("HKSAY").ToString) & "," & _
                                sTirnakEkle(.Item("PKOD").ToString) & "," & _
                                .Item("PSAY").ToString & "," & _
                                sTirnakEkle(.Item("SPKOD").ToString) & "," & _
                                .Item("spmik").ToString & "," & _
                                sTirnakEkle(.Item("KPKOD").ToString) & "," & _
                                .Item("kpmik").ToString & "," & _
                                .Item("NETAGR").ToString & "," & _
                                .Item("BRTAGR").ToString & "," & _
                                .Item("hacim").ToString & "," & _
                                sTirnakEkle("KG") & "," & _
                                sTirnakEkle("M3") & "," & _
                                sTirnakEkle(.Item("U_M").ToString) & "," & _
                                BirimFiyat & "," & _
                                Math.Round(BirimFiyat * .Item("qty_shipped").ToString, 2) & "," & _
                                sTirnakEkle(.Item("MURNKOD").ToString.Replace("*", " ")) & "," & _
                                sTirnakEkle(IIf(.Item("MURNTNM").ToString <> "", .Item("MURNTNM").ToString, sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(.Item("item").ToString)))) & "," & _
                                sTirnakEkle(.Item("MAMBKOD").ToString) & "," & _
                                sTirnakEkle(.Item("MKKOD").ToString) & "," & _
                                sTirnakEkle(.Item("MPKOD").ToString) & "," & _
                                sTirnakEkle(.Item("NAME").ToString) & "," & _
                                sTirnakEkle(.Item("Addr##1").ToString) & "," & _
                                sTirnakEkle(.Item("Addr##2").ToString) & "," & _
                                sTirnakEkle(.Item("Addr##3").ToString) & "," & _
                                sTirnakEkle(.Item("Addr##4").ToString) & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle(.Item("city").ToString) & "," & _
                                sTirnakEkle(.Item("zip").ToString) & "," & _
                                sTirnakEkle(.Item("country").ToString) & "," & _
                                sTirnakEkle(.Item("dunsid").ToString) & "," & _
                                sTirnakEkle(.Item("curr_code").ToString) & "," & _
                                .Item("tax_rate").ToString & "," & _
                                sTirnakEkle(.Item("tax_reg_num1").ToString) & "," & _
                                sTirnakEkle(.Item("Uf_Taxoffice").ToString) & "," & _
                                sTirnakEkle(.Item("terms_code").ToString) & "," & _
                                sTirnakEkle(.Item("TERMNM").ToString) & "," & _
                                "0" & "," & _
                                "0" & "," & _
                                sTirnakEkle(sIrsNolar) & "," & _
                                sTirnakEkle(.Item("Kur").ToString) & "," & _
                                sTirnakEkle(.Item("Pusno").ToString) & "," & _
                                sTirnakEkle(.Item("city").ToString) & "," & _
                                .Item("KAPTUT").ToString & "," & _
                                sTirnakEkle(.Item("FaturaSeriNo").ToString) & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle(.Item("AddrShip##1").ToString) & "," & _
                                sTirnakEkle(.Item("AddrShip##2").ToString) & "," & _
                                sTirnakEkle(.Item("AddrShip##3").ToString) & "," & _
                                sTirnakEkle(.Item("AddrShip##4").ToString) & "," & _
                                sTirnakEkle(.Item("terms_code").ToString) & "," & _
                                sTirnakEkle(.Item("TERMNM").ToString) & "," & _
                                "0" & "," & _
                                "0" & "," & _
                                "0" & "," & _
                                sTirnakEkle(sFaturaSeri.FaturaNotu) & "," & _
                                sTirnakEkle(sDIBNO) & "," & sTirnakEkle(sDIBNO2) & "," &
                                sTirnakEkle(sFaturaData.Detay1) & "," & sTirnakEkle(sFaturaData.Detay2) & "," & sTirnakEkle(sFaturaData.BankDetails1) & "," &
                                sTirnakEkle(sFaturaData.BankDetails2) & "," & sTirnakEkle(sFaturaData.AccountNo) & "," & sTirnakEkle(sFaturaData.AccountNo2) & "," &
                                sTirnakEkle(sFaturaData.Brut) & "," & sTirnakEkle(sFaturaData.Net) & "," & sTirnakEkle(sFaturaData.SevkIrsaliyesi) & "," & _
                                sTirnakEkle(tevkifatCode) & "," & _
                                sTirnakEkle(tevkifatRate) & ")"

                        ' " FDetay1, FDetay2, FBankDetay1, FBankDetay2, FAccountNo1, FAccountNo2 ,FBrut, FNet, FSevkIrsaliyesi  " & _

                        dbAccess.RunSql(sQuery)

                        sQuery = " update ANSFATURA set FaturaNot=" & sTirnakEkle(sFaturaSeri.FaturaNotu)

                        dbAccess.RunSql(sQuery)

                        FaturaTutar = FaturaTutar + (BirimFiyat * .Item("qty_shipped").ToString)

                        sParaBirimi = .Item("curr_code").ToString

                        KdvOrani = .Item("tax_rate").ToString

                        KdvTutar = .Item("sales_tax").ToString

                        FaturaTarihi = .Item("inv_date").ToString

                        FaturaSistemNo = .Item("inv_num").ToString

                    End With

                Next i

                sQuery = " SELECT DISTINCT MSGNO,MSGNM FROM FATSTDTXT"

                If sFatTipi = "N'Y'" Then

                    sQuery = sQuery & " WHERE EXPDSG=0"

                ElseIf sFatTipi = "N'I'" Then

                    sQuery = sQuery & " WHERE EXPDSG=1"

                End If

                dtTemp = db.RunSql(sQuery)

                If Not (dtTemp Is Nothing) AndAlso _
                                 dtTemp.Rows.Count > 0 Then

                    For k As Integer = 0 To dtTemp.Rows.Count - 1

                        With dtTemp.Rows(k)

                            sQuery = "Insert Into ANSFATTXT" & _
                                    " (MSGNO, MSGNM)" & _
                                    " Values (" & _
                                    .Item("MSGNO").ToString & "," & _
                                    sTirnakEkle(.Item("MSGNM").ToString) & ")"

                            dbAccess.RunSql(sQuery)

                        End With

                    Next k

                End If

                If sFatTipi = "N'I'" Then

                    Dim nKapFiyat As Double = 0

                    Dim nKapTutar As Double = 0

                    sQuery = "SELECT KKOD,SUM(KSAY) AS AMBSAY FROM ANSFATURA GROUP BY KKOD"

                    dtAyniyat = dbAccess.RunSql(sQuery)

                    If Not dtAyniyat Is Nothing AndAlso dtAyniyat.Rows.Count > 0 Then

                        For i As Integer = 0 To dtAyniyat.Rows.Count - 1

                            nKapFiyat = nLookup("u_ws_price", "item", " item=" & sTirnakEkle(dtAyniyat.Rows(i).Item("KKOD").ToString))

                            nKapTutar = nKapTutar + (nKapFiyat * dtAyniyat.Rows(i).Item("AMBSAY").ToString)

                        Next

                    End If

                    sQuery = "SELECT PKOD,SUM(PSAY) AS AMBSAY FROM ANSFATURA GROUP BY PKOD"

                    dtAyniyat = dbAccess.RunSql(sQuery)

                    If Not dtAyniyat Is Nothing AndAlso dtAyniyat.Rows.Count > 0 Then

                        For i As Integer = 0 To dtAyniyat.Rows.Count - 1

                            nKapFiyat = nLookup("u_ws_price", "item", " item=" & sTirnakEkle(dtAyniyat.Rows(i).Item("PKOD").ToString))

                            nKapTutar = nKapTutar + (nKapFiyat * dtAyniyat.Rows(i).Item("AMBSAY").ToString)

                        Next

                    End If

                    sQuery = " UPDATE ANSFATURA SET KAPTUT=" & nKapTutar

                    dbAccess.RunSql(sQuery)

                End If

                Dim Yazi As String

                'KdvTutar = Math.Round(CDbl(((FaturaTutar * KdvOrani) / 100)), 2)

                FaturaTutar = Math.Round(FaturaTutar, 2) + KdvTutar

                If sParaBirimi = "YTL" Or sParaBirimi = "TL" Then

                    sParaBirimi = "TL"
                    sKurus = "KRS"

                ElseIf sParaBirimi = "EU" OrElse sParaBirimi = "EUR" Then

                    sParaBirimi = "EUR"
                    sKurus = "CENT"

                ElseIf sParaBirimi = "USD" OrElse sParaBirimi = "US" Then

                    sParaBirimi = "USD"
                    sKurus = "CENT"

                End If

                Yazi = SayiYazOndalikli(FaturaTutar, sParaBirimi, sKurus)

                sQuery = " SELECT  CUSNM, MIKTAR, MURNKOD, MURNTNM, DDAITX, KAPSAYI, " & _
                         " KAPI, KONTNO, COKZNB, FEAAC4, FEAAC5, INVTAR, FERIDX, TUTAR, " & _
                         " NETAGR, GFZ9VC, C9CEZ8, C9NRHV, DHZ9JR, FATTIP, PRICE, DHCANB, " & _
                         " STKOB, FDetay1, FDetay2, FBankDetay1, FBankDetay2, " & _
                         " FAccountNo1, FAccountNo2, FBrut, FNet, DHB1TX, IRSALIYE2," & _
                         " FaturaNot, IRSALIYE1,Pusno,TCODE,TRATE " & _
                         " FROM ANSFATURA " & _
                         " WHERE MIKTAR <> 0 " & _
                         " ORDER BY MURNKOD, KAPI, KONTNO, DDAITX "

                RaporCagir("SLFatura.rpt", , Yazi, "SLFATURA", True, True, "", dbAccess.RunSql(sQuery))

                dbAccess.RunSql("Delete From ANSFATURA")
                dbAccess.RunSql("Delete From ANSFATTXT")

                'RaporCagir("SLFatura.rpt", , Yazi, "SLFATURA", True, True)

                sQuery = " Update ShpPack" & _
                            " Set FaturaSeriNo=" & sTirnakEkle(sFaturaSeri.SeriNo) & _
                            " , FaturaTar=" & sTirnakEkle(FaturaTarihi.ToString("yyyy-MM-dd")) & _
                            " , INVNO=" & sTirnakEkle(FaturaSistemNo) & _
                            " , FaturaNot=" & sTirnakEkle(sFaturaSeri.FaturaNotu) & _
                            " Where ShpNo IN (" & sSevkNolar & ")"

                db.RunSql(sQuery)

                If rdbIlkBasim.Checked Then

                    sQuery = " Update FaturaPrn" & _
                                " Set Kullanici='*' + Kullanici" & _
                                " Where SevkNo IN (" & sSevkNolar & ")"

                    db.RunSql(sQuery)

                End If

                sQuery = " Update arinv" &
                   " Set ref =" & sTirnakEkle(sFaturaSeri.SeriNo & " Nolu Fatura") &
                   " ,description=" & sTirnakEkle(sFaturaSeri.SeriNo & " Nolu Fatura") &
                   " Where inv_num in  (Select inv_num " &
                   " From FaturaPrn" & _
                   " Where SevkNo IN (" & sSevkNolar & "))"

                db.RunSql(sQuery)

                btnSorgula_Click(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub

    Function dosyaoku() As String
        Dim Fs As FileStream
        Dim Okuma As StreamReader

        Dim dosyano As String

        Try

            Fs = New FileStream(Application.StartupPath & "\Sayac.txt", FileMode.Open)
            Okuma = New StreamReader(Fs, Encoding.GetEncoding("windows-1254"))

            While Okuma.Peek <> -1
                dosyano = Okuma.ReadLine ' bu satir ilk değer başlıklar
                Windows.Forms.Cursor.Current = Cursors.WaitCursor
            End While
            Okuma.Close()
            Fs.Close()

            dosyaoku = dosyano

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Okuma.Close()
            Fs.Close()
        End Try
    End Function

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "inv_num", sender, e)
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

