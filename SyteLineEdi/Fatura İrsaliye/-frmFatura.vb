Public Class frmFatura

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim row As Janus.Windows.GridEX.GridEXRow

        checkedRows = Me.GridEX1.GetCheckedRows()

        Dim sSevkNolar As String = ""
        Dim sSevkNolarTirnakli As String = ""

        Dim sIrsNolar As String = ""
        Dim sFatTipi As String = "Y"
        Dim bGuncelFiyat As Boolean = chkGuncelFiyat.Checked

        Dim dShipDate As Date
        Dim iSevkNo, iPickno, iKsay, iPsay, iHksay, _
            iSpmik, iKpmik, iKAP_EN, iKAP_Boy, iKAP_Yuk As Integer

        Dim nqty_shipped, nNETAGR, nBRTAGR, nhacim, nKAPTUT, nTutar, ncont_price, nKur As Double

        Dim sShipTo As String = ""
        Dim sKAPI As String = ""
        Dim sKNTRT As String = ""
        Dim sKKOD As String = ""
        Dim sPKOD As String = ""
        Dim sSPKOD As String = ""
        Dim sKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sMURNTNM As String = ""
        Dim sMAMBKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMPKOD As String = ""
        Dim sPusno As String = ""
        Dim sAmbkod As String = ""
        Dim sPlaka As String = ""
        Dim sSeferNo As String = ""
        Dim sNavlunNo As String = ""
        Dim sAmbar As String = ""
        Dim sShpno As String = ""
        Dim sMalzeme As String = ""
        Dim sMusteriNo As String = ""
        Dim sKOLIAYN As String = ""
        Dim sPALETAYN As String = ""
        Dim sKAPAKAYN As String = ""
        Dim iVadeFarkli As Integer = 0
        Dim sVadeKosullari As String = ""
        Dim sKdv As String = ""
        Dim iKdvFarkli As Integer = 0
        Try

            If txtFaturaTarihi.Text = "" Then

                MessageBox.Show("Lütfen Fatura Tarihini Seçiniz!")

                Exit Sub

            End If

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                dbAccess.RunSql("Delete From ANSFATURA")
                dbAccess.RunSql("Delete From ANSFATTXT")

                For Each row In checkedRows

                    If Not sKdv.Contains(row.Cells("itemtax_code").Text) Then

                        sKdv = IIf(sKdv = "", "", sKdv & ",") & row.Cells("itemtax_code").Text

                        iKdvFarkli += 1

                    End If

                    If Not sVadeKosullari.Contains(row.Cells("terms_code").Text) Then

                        sVadeKosullari = IIf(sVadeKosullari = "", "", sVadeKosullari & ",") & row.Cells("terms_code").Text

                        iVadeFarkli += 1

                    End If

                    If Not sSevkNolar.Contains(row.Cells("SEVKNO").Text) Then

                        sSevkNolar = IIf(sSevkNolar = "", "", sSevkNolar & ",") & row.Cells("SEVKNO").Text

                    End If

                    If Not sSevkNolarTirnakli.Contains(row.Cells("SEVKNO").Text) Then

                        sSevkNolarTirnakli = IIf(sSevkNolarTirnakli = "", "'", sSevkNolarTirnakli & ",'") & row.Cells("SEVKNO").Text & "'"

                    End If

                    If Not sIrsNolar.Contains(row.Cells("IRSNO").Text) Then

                        sIrsNolar = IIf(sIrsNolar = "", "", sIrsNolar & "/") & row.Cells("IRSNO").Text

                    End If

                Next

            End If

            If iKdvFarkli > 1 Then

                MessageBox.Show("Farklı vergi kodları aynı faturada birleştirilemez.")

                Exit Sub
            End If

            If iVadeFarkli > 1 Then

                MessageBox.Show("Farklı Vade Koşullarına Ait İrsaliyeler Seçili!")

                Exit Sub

            End If

            Dim FaturaNo As Double
            Dim FaturaTutar As Double
            Dim KdvOrani As Double
            Dim BirimFiyat As Double
            Dim sFaturaNo As String

            Dim sParaBirimi As String
            Dim sKurus As String
            FaturaNo = SeriNoAl("FTR")
            sFaturaNo = CStr(FaturaNo).PadLeft(10, "0")

            Dim sDIBNO As String = ""
            Dim sDIBNO2 As String = ""

            sDIBNO = "" 'sLookup("DIBNO", "DIBPF", "")
            sDIBNO2 = "" 'sLookup("DIBNO1", "DIBPF", "")

            sQuery = " Select sevkno, Pickno,  Max(cust_num) As cust_num, Max(name) As Name , Max(country) As country , Max(lang_code) as lang_code," & _
                                " Max(curr_code) As curr_code, item, Max(description) As description, " & _
                                " sum(qty_shipped) As qty_shipped, Max(dtyPrice) As dtyPrice ,  " & _
                                " MAx(itemtax_code) As itemtax_code, MAx(ship_date) As ship_date, " & _
                                " MAx(terms_code) As terms_code, MAx(u_m) As u_m," & _
                                " MAx(shiptoname) As shiptoname, " & _
                                " MAx(Addr##1) As Addr##1," & _
                                " MAx(Addr##2) As Addr##2, MAx(Addr##3) As Addr##3, MAx(Addr##4) As Addr##4," & _
                                " MAx(city) As city, MAx(zip) As zip, MAx(dunsid) As dunsid, MAx(tax_rate) As tax_rate," & _
                                " MAx(tax_reg_num1) As tax_reg_num1,Max(Uf_TaxOffice) As Uf_TaxOffice, MAx(termnm) As termnm, " & _
                                " MAx(AddrShip##1) As AddrShip##1, MAx(AddrShip##2) As AddrShip##2, " & _
                                " MAx(AddrShip##3) As AddrShip##3 , MAx(AddrShip##4) As AddrShip##4 " & _
                            " From FaturaMstPrn1 " & _
                            " Where SEVKNO IN (" & sSevkNolar & ")"

            sQuery = sQuery & " Group By sevkno,  item , Pickno"

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    With dt.Rows(i)

                        sFatTipi = IIf(.Item("lang_code").ToString = "TRK", "Y", "I")

                        nqty_shipped = .Item("qty_shipped").ToString

                        dShipDate = .Item("Ship_Date").ToString

                        GetRowInfo(sShpno, dt, i, "Sevkno")

                        GetRowInfo(sMalzeme, dt, i, "item")

                        GetRowInfo(iPickno, dt, i, "Pickno")

                        sQuery = "Select *" & _
                                    " From Tr_ShppackSum" & _
                                    " Where Shpno=" & sTirnakEkle(sShpno) & _
                                    " And Pickno=" & iPickno & _
                                    " And Itnbr=" & sTirnakEkle(sMalzeme)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(sShipTo, dtTemp, 0, "Shipto")
                        GetRowInfo(sKAPI, dtTemp, 0, "KAPI")
                        GetRowInfo(sMusteriNo, dtTemp, 0, "Cust")
                        GetRowInfo(sPusno, dtTemp, 0, "Pusno")
                        GetRowInfo(sAmbkod, dtTemp, 0, "AMBKOD")
                        GetRowInfo(sKKOD, dtTemp, 0, "KKOD")
                        GetRowInfo(iKsay, dtTemp, 0, "KSAY")
                        GetRowInfo(sPlaka, dtTemp, 0, "Plaka")
                        GetRowInfo(iHksay, dtTemp, 0, "HKSAY")
                        GetRowInfo(sPKOD, dtTemp, 0, "PKOD")
                        GetRowInfo(iPsay, dtTemp, 0, "PSAY")
                        GetRowInfo(nNETAGR, dtTemp, 0, "NETAGR")
                        GetRowInfo(nBRTAGR, dtTemp, 0, "BRTAGR")
                        GetRowInfo(nhacim, dtTemp, 0, "HACIM")
                        GetRowInfo(iKAP_EN, dtTemp, 0, "KAP_EN")
                        GetRowInfo(iKAP_Boy, dtTemp, 0, "KAP_BOY")
                        GetRowInfo(sPKOD, dtTemp, 0, "SPKOD")
                        GetRowInfo(iSpmik, dtTemp, 0, "SPMIK")
                        GetRowInfo(sKPKOD, dtTemp, 0, "KPKOD")
                        GetRowInfo(iKpmik, dtTemp, 0, "KPMIK")
                        GetRowInfo(iKAP_Yuk, dtTemp, 0, "KAP_YUK")
                        GetRowInfo(sSeferNo, dtTemp, 0, "SeferNo")
                        GetRowInfo(sNavlunNo, dtTemp, 0, "NavlunNo")
                        GetRowInfo(sAmbar, dtTemp, 0, "Whse")
                        GetRowInfo(sMURNKOD, dtTemp, 0, "MURNKOD")
                        GetRowInfo(sMURNTNM, dtTemp, 0, "MURNTNM")

                        sQuery = "Select * " & _
                                    " from Kontrtpf " & _
                                        " where  BZMITM =" & sTirnakEkle(sMalzeme) & _
                                        " And CUST =" & sTirnakEkle(sMusteriNo) & _
                                        " And SHIPTO =" & sTirnakEkle(sShipTo) & _
                                        " And KAPI=" & sTirnakEkle(sKAPI)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(sKNTRT, dtTemp, 0, "KNTRT")

                        sQuery = "Select * " & _
                                    " from Itmpack  " & _
                                        " where Itnbr =" & sTirnakEkle(sMalzeme) & _
                                        " And Ambkod =" & sTirnakEkle(sAmbkod)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(sMAMBKOD, dtTemp, 0, "MAMBKOD")

                        GetRowInfo(sMKKOD, dtTemp, 0, "MKKOD")

                        GetRowInfo(sMPKOD, dtTemp, 0, "MPKOD")

                        sQuery = "Select * " & _
                                    " from ITEMDIM  " & _
                                        " where Itnbr =" & sTirnakEkle(sKKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(sKOLIAYN, dtTemp, 0, "AYNTIP")

                        sQuery = "Select * " & _
                                    " from ITEMDIM  " & _
                                        " where Itnbr =" & sTirnakEkle(sPKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(sPALETAYN, dtTemp, 0, "AYNTIP")

                        sQuery = "Select * " & _
                                    " from ITEMDIM  " & _
                                        " where Itnbr =" & sTirnakEkle(sKPKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(sKAPAKAYN, dtTemp, 0, "AYNTIP")

                        sQuery = "Select * " & _
                                    " from Item  " & _
                                        " where Item =" & sTirnakEkle(sKKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(nTutar, dtTemp, 0, "u_ws_price")

                        nKAPTUT = nKAPTUT + (iKsay * nTutar)

                        sQuery = "Select * " & _
                                    " from Item  " & _
                                        " where Item =" & sTirnakEkle(sPKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(nTutar, dtTemp, 0, "u_ws_price")

                        nKAPTUT = nKAPTUT + (iKsay * nTutar)

                        sQuery = "Select * " & _
                                    " from Item  " & _
                                        " where Item =" & sTirnakEkle(sKPKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(nTutar, dtTemp, 0, "u_ws_price")

                        nKAPTUT = nKAPTUT + (iKsay * nTutar)

                        sQuery = "Select * " & _
                                    " from Item  " & _
                                        " where Item =" & sTirnakEkle(sSPKOD)

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(nTutar, dtTemp, 0, "u_ws_price")

                        nKAPTUT = nKAPTUT + (iKsay * nTutar)

                        sQuery = " SELECT isnull(dbo.Tr_KurGetir_Fatura(kont.Uf_DovizCinsi,Null," & sTirnakEkle(dShipDate.ToString("yyyy-MM-dd")) &
                            "," & IIf(sFatTipi = "Y", "0", "1") & " ),1) As Kur," &
                                " isnull(	kont.Uf_DovizFiyati * dbo.Tr_KurGetir_Fatura(kont.Uf_DovizCinsi,Null," &
                                sTirnakEkle(dShipDate.ToString("yyyy-MM-dd")) & "," & IIf(sFatTipi = "Y", "0", "1") & " )," &
                                " kont.cont_price) As cont_price, " &
                                " kont.Uf_DovizFiyati, kont.Uf_DovizCinsi " &
                        " FROM dbo.Tr_KontratFiyati(" & sTirnakEkle(sMusteriNo) & "," &
                                                    sTirnakEkle(sMalzeme) & "," &
                                                    sTirnakEkle(sMURNKOD) & "," &
                                                    sTirnakEkle(dShipDate.ToString("yyyy-MM-dd")) & ") kont"

                        dtTemp = db.RunSql(sQuery)

                        GetRowInfo(ncont_price, dtTemp, 0, "cont_price")

                        GetRowInfo(nKur, dtTemp, 0, "Kur")

                        BirimFiyat = IIf(bGuncelFiyat, ncont_price, .Item("dtyPrice").ToString)

                        nKur = IIf(bGuncelFiyat, nKur, 1)

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
                                    " PAYMENTTERMNM, KUTUFYT, PALETFYT, KAPAKFYT, FaturaNot, DIBNO, DIBNO1, TCODE, TRATE" & _
                                    ")"
                        sQuery = sQuery & " Values ('" & _
                                sFatTipi & "'," & _
                                FaturaNo & "," & _
                                sTirnakEkle(CDate(txtFaturaTarihi.Text).ToString("yyyy-MM-dd")) & "," & _
                                iSevkNo & "," & _
                                iPickno & "," & _
                                sTirnakEkle(dShipDate.ToString("yyyy-MM-dd")) & "," & _
                                sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                sTirnakEkle(sShipTo) & "," & _
                                sTirnakEkle(.Item("shiptoname").ToString) & "," & _
                                sTirnakEkle(.Item("item").ToString) & "," & _
                                sTirnakEkle(sKAPI) & "," & _
                                nqty_shipped & "," & _
                                sTirnakEkle(sKNTRT) & "," & _
                                sTirnakEkle(sKKOD) & "," & _
                                iKsay & "," & _
                                CInt(iPsay) + CInt(iHksay) & "," & _
                                sTirnakEkle(sPKOD) & "," & _
                                iPsay & "," & _
                                sTirnakEkle(sSPKOD) & "," & _
                                iSpmik & "," & _
                                sTirnakEkle(sKPKOD) & "," & _
                                iKpmik & "," & _
                                nNETAGR & "," & _
                                nBRTAGR & "," & _
                                nhacim & "," & _
                                sTirnakEkle("KG") & "," & _
                                sTirnakEkle("M3") & "," & _
                                sTirnakEkle(.Item("U_M").ToString) & "," & _
                                BirimFiyat & "," & _
                                BirimFiyat * nqty_shipped & "," & _
                                sTirnakEkle(IIf(sMURNKOD <> "", sMURNKOD, .Item("item").ToString)) & "," & _
                                sTirnakEkle(IIf(sMURNTNM <> "", sMURNTNM, sLookup("ITDSC", "ITEMASA", "ITNBR=" & sTirnakEkle(.Item("item").ToString)))) & "," & _
                                sTirnakEkle(sMAMBKOD) & "," & _
                                sTirnakEkle(sMKKOD) & "," & _
                                sTirnakEkle(sMPKOD) & "," & _
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
                                sTirnakEkle(nKur) & "," & _
                                sTirnakEkle(sPusno) & "," & _
                                sTirnakEkle(.Item("city").ToString) & "," & _
                                nKAPTUT & "," & _
                                sTirnakEkle("") & "," & _
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
                                sTirnakEkle(sDIBNO) & "," & _
                                sTirnakEkle(sDIBNO2) & "," & _
                                sTirnakEkle(tevkifatCode) & "," & _
                                sTirnakEkle(tevkifatRate) & _
                                 ")"

                        dbAccess.RunSql(sQuery)

                        sParaBirimi = .Item("curr_code").ToString

                        FaturaTutar = FaturaTutar + (BirimFiyat * nqty_shipped)

                        KdvOrani = .Item("tax_rate").ToString

                    End With

                Next i

                sQuery = " SELECT DISTINCT MSGNO,MSGNM FROM FATSTDTXT"

                If sFatTipi = "Y" Then

                    sQuery = sQuery & " WHERE EXPDSG=0"

                ElseIf sFatTipi = "I" Then

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

                If sFatTipi = "I" Then

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

                Dim KdvTutar As Double

                KdvTutar = Math.Round(CDbl(((FaturaTutar * KdvOrani) / 100)), 2)

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

                RaporCagir("SLFatura.rpt", , Yazi, "SLFATURA", False, False, "", dbAccess.RunSql(sQuery))

                'RaporCagir("SLFatura.rpt", , Yazi, "SLFATURA", False, False)

                dbAccess.RunSql("Delete From ANSFATURA")
                dbAccess.RunSql("Delete From ANSFATTXT")

                If MessageBox.Show("Fatura Doğru Yazdırıldı mı?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then

                    btnSorgula_Click(sender, e)

                    Exit Sub

                End If



                '***************Coitem ShpDty Fiyat Güncelleme
                If bGuncelFiyat Then

                    sQuery = "Select co_num, co_line, SevkNo, cont_price, dtyPrice, Item, Kur" & _
                            " From FaturaMst" & _
                            " Where SEVKNO IN (" & sSevkNolar & ")" & _
                            " And cont_price <> dtyPrice"

                    dtTemp = db.RunSql(sQuery)

                    If Not dtTemp Is Nothing AndAlso _
                        dt.Rows.Count > 0 Then

                        For z As Integer = 0 To dtTemp.Rows.Count - 1

                            With dtTemp.Rows(z)

                                sQuery = " Update coitem" & _
                                        " Set Price_conv=" & .Item("cont_price").ToString & _
                                            " , price=" & .Item("cont_price").ToString & _
                                        " Where co_num=" & sTirnakEkle(.Item("co_num").ToString) & _
                                        " And co_line=" & .Item("co_line").ToString & _
                                        " And item=" & sTirnakEkle(.Item("Item").ToString)

                                db.RunSql(sQuery)

                                sQuery = " Update tr_co_ship" & _
                                            " Set Price=" & .Item("cont_price").ToString & _
                                            " Where co_num=" & sTirnakEkle(.Item("co_num").ToString) & _
                                            " And co_line=" & .Item("co_line").ToString & _
                                            " And reason_text=" & sTirnakEkle(.Item("SevkNo").ToString)

                                db.RunSql(sQuery)

                                sQuery = "Update ShpDty" & _
                                            " Set PRICE=" & .Item("cont_price").ToString & _
                                                    " , Kur=" & .Item("kur").ToString & _
                                        " Where SHPNO=" & .Item("SEVKNO").ToString & _
                                            " ANd Itnbr=" & sTirnakEkle(.Item("Item").ToString) & _
                                            " And Ordno=" & sTirnakEkle(.Item("co_num").ToString) & _
                                            " And SEQNO=" & .Item("co_line").ToString

                                db.RunSql(sQuery)

                            End With

                        Next z

                    End If

                End If

                '***************************************

                '***********Shppack Update***************

                sQuery = "Update ShpPack" & _
                            " Set FatNo=" & FaturaNo & _
                            " , FatDrm=1" & _
                            " , FaturaNot=" & sTirnakEkle(sFaturaSeri.FaturaNotu) & _
                        " Where SHPNO In (" & sSevkNolar & ")"

                db.RunSql(sQuery)

                sQuery = "Update ShpDty" & _
                            " Set DO_NUM=" & sTirnakEkle(FaturaNo) & _
                        " Where SHPNO In (" & sSevkNolar & ")"

                db.RunSql(sQuery)

                '***************************************************

                '******************Co_ship Update ********************

                sQuery = "Insert Into co_ship " & _
                            " Select * " & _
                                " From tr_co_ship tr" & _
                                " Where Not Exists ( Select 1 " & _
                                                        " From co_ship c " & _
                                                        " Where c.co_num=tr.co_num " & _
                                                            " And c.co_line=tr.co_line" & _
                                                            " And c.co_release=tr.co_release" & _
                                                            " And c.date_seq=tr.date_seq" & _
                                                            " And c.ship_date=tr.ship_date" & " ) " & _
                                " And reason_text In(" & sSevkNolarTirnakli & ")"

                db.RunSql(sQuery)

                sQuery = " Update co_ship " & _
                            " Set co_ship.Price=tr.Price" & _
                                ", co_ship.qty_shipped=tr.qty_shipped" & _
                            " From tr_co_ship tr" & _
                        " Where co_ship.co_num=tr.co_num " & _
                            " And co_ship.co_line=tr.co_line" & _
                            " And co_ship.co_release=tr.co_release" & _
                            " And co_ship.date_seq=tr.date_seq" & _
                            " And co_ship.ship_date=tr.ship_date" & _
                            " And co_ship.reason_text=tr.reason_text" & _
                            " And co_ship.reason_text In(" & sSevkNolarTirnakli & ")"

                db.RunSql(sQuery)

                sQuery = " Delete " & _
                    " From co_ship " & _
                    " Where Not Exists ( Select 1 " & _
                                            " From tr_co_ship tr " & _
                                            " Where co_ship.co_num=tr.co_num " & _
                                                " And co_ship.co_line=tr.co_line" & _
                                                " And co_ship.co_release=tr.co_release" & _
                                                " And co_ship.date_seq=tr.date_seq" & _
                                                " And co_ship.ship_date=tr.ship_date" & " ) " & _
                    " And reason_text In(" & sSevkNolarTirnakli & ")"

                db.RunSql(sQuery)

                '********************************************************

                '********** Do_line , Do_Hdr, Do_Seq Insert işlemi yapılıyor

                Dim FaturaSiraNo As Integer

                sQuery = "Select cust_num, cust_seq,  co_num, co_line, co_release, qty_shipped,dtyPrice * qty_shipped As Tutar, ship_date, date_seq , Plaka, SevkNo," & _
                            " terms_code, ship_code, ItemTax_Code, item, description, dtyPrice, u_m " & _
                            " From FaturaMst" & _
                            " Where SEVKNO IN (" & sSevkNolar & ")"

                dtTemp = db.RunSql(sQuery)

                If Not dtTemp Is Nothing AndAlso _
                    dt.Rows.Count > 0 Then

                    Try
                        db.BeginTransaction()

                        For y As Integer = 0 To dtTemp.Rows.Count - 1

                            With dtTemp.Rows(y)

                                If y = 0 Then

                                    sQuery = "Insert into Do_Hdr" & _
                                                " (do_num, do_hdr_date, stat, cust_num, cust_seq, veh_num, pickup_date  ) " & _
                                                " Values (" & _
                                                sTirnakEkle(sFaturaNo) & "," & _
                                                "getdate()" & "," & _
                                                sTirnakEkle("A") & "," & _
                                                sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                                .Item("cust_seq").ToString & "," & _
                                                sTirnakEkle(.Item("Plaka").ToString) & "," & _
                                                "getdate()" & ")"

                                    db.RunSql(sQuery)

                                    If db.Result.ReturnValue = False Then

                                        Throw New Exception(db.Result.GetMessages)

                                    End If

                                    FaturaSiraNo = nLookup("Max(con_inv_seq)", "con_inv_hdr", " cust_num=" & sTirnakEkle(.Item("cust_num").ToString)) + 1

                                    sQuery = "Insert into con_inv_hdr" & _
                                            " (cust_num, con_inv_seq, do_invoice, do_num, " & _
                                            " terms_code, ship_code, frt_tax_code1, msc_tax_code1) " & _
                                            " Values (" & _
                                            sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                            FaturaSiraNo & "," & _
                                            sTirnakEkle("S") & "," & _
                                            sTirnakEkle(sFaturaNo) & "," & _
                                            sTirnakEkle(.Item("terms_code").ToString) & "," & _
                                            sTirnakEkle(.Item("ship_code").ToString) & "," & _
                                            sTirnakEkle(.Item("ItemTax_Code").ToString) & "," & _
                                            sTirnakEkle(.Item("ItemTax_Code").ToString) & ")"

                                    db.RunSql(sQuery)

                                    If db.Result.ReturnValue = False Then

                                        Throw New Exception(db.Result.GetMessages)

                                    End If

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
                                                sTirnakEkle(CDate(.Item("ship_date").ToString).ToString("yyyy-MM-dd")) & "," & _
                                                .Item("date_seq").ToString & ")"

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then

                                    Throw New Exception(db.Result.GetMessages)

                                End If

                                sQuery = "Insert into con_inv_item" & _
                                        " (cust_num, con_inv_seq, inv_line, co_num, co_line, " & _
                                        " co_release, qty_to_invoice, ext_price, regen) " & _
                                        " Values (" & _
                                        sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                        FaturaSiraNo & "," & _
                                        y + 1 & "," & _
                                        sTirnakEkle(.Item("co_num").ToString) & "," & _
                                        .Item("co_line").ToString & "," & _
                                        .Item("co_release").ToString & "," & _
                                        .Item("qty_shipped").ToString & "," & _
                                        .Item("Tutar").ToString & ",0)"

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then

                                    Throw New Exception(db.Result.GetMessages)

                                End If

                                sQuery = "Insert into con_inv_line" & _
                                        " (cust_num, con_inv_seq, inv_line, item, description, " & _
                                        " price, u_m, do_num, do_line, do_seq, drop_cust_num," & _
                                        " drop_cust_seq ,qty_to_invoice ,ext_price ) " & _
                                        " Values (" & _
                                        sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                        FaturaSiraNo & "," & _
                                        y + 1 & "," & _
                                        sTirnakEkle(.Item("item").ToString) & "," & _
                                        sTirnakEkle(.Item("description").ToString) & "," & _
                                        .Item("dtyPrice").ToString & "," & _
                                        sTirnakEkle(.Item("u_m").ToString) & "," & _
                                        sTirnakEkle(sFaturaNo) & "," & _
                                        "1" & "," & _
                                        y + 1 & "," & _
                                        sTirnakEkle(.Item("cust_num").ToString) & "," & _
                                        .Item("cust_seq").ToString & "," & _
                                        .Item("qty_shipped").ToString & "," & _
                                        .Item("Tutar").ToString & ")"

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then

                                    Throw New Exception(db.Result.GetMessages)

                                End If

                                sQuery = "Update ShpDty" & _
                                                " Set PRICE=" & .Item("dtyPrice").ToString & _
                                            " Where SHPNO=" & .Item("SEVKNO").ToString & _
                                                " ANd Itnbr=" & sTirnakEkle(.Item("item").ToString) & _
                                                " And Ordno=" & sTirnakEkle(.Item("co_num").ToString) & _
                                                " And SEQNO=" & .Item("co_line").ToString

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then

                                    Throw New Exception(db.Result.GetMessages)

                                End If

                                sQuery = " Update coitem" &
                                            " Set Price_conv=" & .Item("dtyPrice").ToString &
                                                " , price=" & .Item("dtyPrice").ToString &
                                            " Where co_num=" & sTirnakEkle(.Item("co_num").ToString) &
                                            " And co_line=" & .Item("co_line").ToString &
                                            " And item=" & sTirnakEkle(.Item("item").ToString)

                                db.RunSql(sQuery)

                                If db.Result.ReturnValue = False Then
                                    Throw New Exception(db.Result.GetMessages)
                                End If
                            End With

                        Next y

                        sQuery = " Delete " & _
                                    " From tr_co_ship" & _
                                    " Where reason_text In(" & sSevkNolarTirnakli & ")"

                        db.RunSql(sQuery)

                        If db.Result.ReturnValue = False Then

                            Throw New Exception(db.Result.GetMessages)

                        End If

                        db.CommitTransaction()

                    Catch ex As Exception

                        db.RollbackTransaction()

                        MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Try

                End If

                '***********Regen için Update
                sQuery = "Update con_inv_item" & _
                            " Set Regen=0"

                db.RunSql(sQuery, True)

                '*************Post İşlemi

                sQuery = "Exec Tr_FaturaPost_Kons " & _
                                "@Musteri=" & sTirnakEkle(sMusteriNo) & "," &
                                "@DoNum=" & sTirnakEkle(sFaturaNo) & "," &
                                "@FaturaTarihi=" & sTirnakEkle(dtpFaturaTarihi.Value.ToString("yyyy-MM-dd"))

                dt = db.RunSql(sQuery)

                Dim sInvNo As String = ""

                Dim sInvHata As String = ""

                GetRowInfo(sInvNo, dt, 0, "FaturaNo")
                GetRowInfo(sInvHata, dt, 0, "Hata")

                If sInvHata <> "1 Consolidated Invoice Header(s) were processed." Or sInvNo = "0" Then

                    Throw New Exception("Fatura Post İşleminde Hata Oluştu" & vbNewLine & sInvHata)

                Else

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



                End If

                '***********************************

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            dtpFaturaTarihi.Value = Now

            txtFaturaTarihi.Text = ""

            btnSorgula_Click(sender, e)

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

            sQuery = " Select sevkno, max(cust_num) As cust_num, Max(name) As name,  Max(country) As country , " & _
                                " Max(curr_code) curr_code, item, Max(description) As description, " & _
                                " sum(qty_shipped) As qty_shipped, Max(dtyPrice) As dtyPrice , Max(Shipto) As Shipto,   " & _
                                " Max(KAPI) As KAPI, Max(IRSNO) As IRSNO,  Max(SEFERNO) As SEFERNO, Max(NAVLUNNO) As NAVLUNNO," & _
                                " Max(MURNKOD) As MURNKOD, Max(cont_price) As cont_price, " & _
                                " MAx(itemtax_code) As itemtax_code, MAx(ship_date) As ship_date, MAx(Pusno) As Pusno," & _
                                " MAx(terms_code) As terms_code, MAx(u_m) As u_m, Max(Kur) As Kur" & _
                           " From FaturaMst " & _
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

                sQuery = sQuery & " and ship_date >=" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd")) & _
                                    " and ship_date <=" & sTirnakEkle(dtmSevkTarihi2.Value.ToString("yyyy-MM-dd"))

            Else

                sQuery = sQuery & " and ship_date =" & sTirnakEkle(dtmSevkTarihi1.Value.ToString("yyyy-MM-dd"))

            End If

            If txtKullanici.Text <> "" Then

                sQuery = sQuery & "    and Kullanici='" & txtKullanici.Text & "'"

            End If

            sQuery = sQuery & " Group By sevkno, item"

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

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtmSevkTarihi2.Enabled = chkTarihAraligi.Checked
    End Sub

    Private Sub dtpFaturaTarihi_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFaturaTarihi.ValueChanged
        txtFaturaTarihi.Text = CDate(dtpFaturaTarihi.Value).ToString("dd.MM.yyyy")
    End Sub

    Private Sub frmFatura_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtAmbar1.Text = VarsayilanAmbar

        'txtKullanici.Text = KullaniciAdi

        dtpFaturaTarihi.Value = Now

        txtFaturaTarihi.Text = ""
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "SevkNo", sender, e)
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