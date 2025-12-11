Public Class frmIhracEvraklari

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

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            sQuery = " Select *" & _
                        " From LiefersheinMst " & _
                        " Where 1=1 "

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & " and MusteriNo='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & " and MusteriNo>='" & txtMusteri1.Text & "'" & _
                                      " and MusteriNo<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtFaturaNo1.Text <> "" Then

                If txtFaturaNo2.Text = "" Then

                    sQuery = sQuery & " and FaturaNo=" & txtFaturaNo1.Text & ""

                Else

                    sQuery = sQuery & " and FaturaNo>=" & txtFaturaNo1.Text & _
                                      " and FaturaNo<=" & txtFaturaNo2.Text

                End If

            End If

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

    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click
        Dim nKapAdedi, nNetAgr, nBrtAgr, nHacim, nKap_En, _
            nKap_Boy, nKap_Yuks, nKutu_En, nKutu_Boy, nKutu_Yuks As Double

        Dim sMItmKod As String = ""
        Dim sMItmDesc As String = ""
        Dim sKontrat As String = ""
        Dim sUserF5 As String = ""
        Dim sRevNo As String = ""
        Dim sDuns As String = ""
        Dim sPlant As String = ""
        Dim sDuns1 As String = ""
        Dim sMAMBKOD As String = ""
        Dim sAmbTanim As String = ""
        Dim sAmbKod As String = ""
        Dim sMKKOD As String = ""
        Dim sMPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sFatTipi As String = ""
        Dim sKutu_Tnm As String = ""
        Dim sPalet_Tnm As String = ""
        Dim sSepr_Tnm As String = ""
        Dim sKapak_Tnm As String = ""
        Dim sPusNo As String = ""
        Dim sAmbar As String = ""
        Dim FaturaNo As Integer

        Dim nRevTar As DateTime

        Dim sSQL As String

        Try

            sSQL = " DELETE FROM ANSLIEFER"
            dbAccess.RunSql(sSQL)

            sSQL = " DELETE FROM ANSNAKLBILD"
            dbAccess.RunSql(sSQL)

            sSQL = " DELETE FROM CEKIGUMR"
            dbAccess.RunSql(sSQL)

            sSQL = " DELETE FROM ANSFATACK"
            dbAccess.RunSql(sSQL)

            sSQL = " DELETE FROM ANSYUKLADR"
            dbAccess.RunSql(sSQL)

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            Dim row As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                For Each row In checkedRows

                    FaturaNo = row.Cells("FaturaNo").Text

                    Exit For

                Next

            End If

            sQuery = " Update shpdty" & _
                        " Set shpdty.price = inv_item.price" & _
                        " from inv_item " & _
                        " Where shpdty.price <> inv_item.price " & _
                            " and shpdty.ordno = inv_item.co_num" & _
                            " and shpdty.seqno = inv_item.co_line" & _
                            " and replicate('0',10 - Len(shpdty.do_num)) + shpdty.do_num = inv_item.do_num " & _
                            " and ltrim(inv_num)=" & sTirnakEkle(FaturaNo)

            db.RunSql(sQuery, True)

            sQuery = "Select * " & _
                        " From LiefersheinPrn" & _
                        " Where FaturaNo=" & FaturaNo

            dt = db.RunSql(sQuery)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each rowDt As DataRow In dt.Rows

                    nKapAdedi = CDbl(rowDt.Item("HKSAY").ToString) + CDbl(rowDt.Item("PSAY").ToString)

                    nNetAgr = rowDt.Item("NETAGR").ToString

                    nBrtAgr = rowDt.Item("BRTAGR").ToString

                    nHacim = rowDt.Item("HACIM").ToString

                    sMItmKod = ""

                    sMItmDesc = ""

                    sKontrat = ""

                    sAmbar = rowDt.Item("Whse").ToString

                    sSQL = " SELECT ITEM,REFADI,AMBKOD,AFKOD,KNTRT " & _
                                " FROM KONTRTPF  " & _
                                " WHERE CUST=" & sTirnakEkle(rowDt.Item("CUST").ToString) & _
                                " AND SHIPTO=" & sTirnakEkle(rowDt.Item("Shipto").ToString) & _
                                " AND KAPI=" & sTirnakEkle(rowDt.Item("KAPI").ToString) & _
                                " AND BZMITM=" & sTirnakEkle(rowDt.Item("ITNBR").ToString)

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        sMItmKod = dtTemp.Rows(0)("ITEM").ToString.Replace("*", " ")

                        sMItmDesc = dtTemp.Rows(0)("REFADI").ToString

                        sKontrat = dtTemp.Rows(0)("KNTRT").ToString

                    End If

                    sUserF5 = sLookup("USERF5", "PLANTPRM", " CANB=" & sTirnakEkle(rowDt.Item("CUST").ToString) & _
                                                            " AND B9CD=" & sTirnakEkle(rowDt.Item("Shipto").ToString))

                    'If sUserF5 = "1" Then

                    sRevNo = sLookup("REVNO", "ITMPACK", " AMBKOD=" & sTirnakEkle(rowDt.Item("AMBKOD").ToString) & _
                                                        " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                    nRevTar = CDate(IIf(sLookup("REVTAR", "ITMPACK", " AMBKOD=" & sTirnakEkle(rowDt.Item("AMBKOD").ToString) & _
                                            " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString)) = "", _
                                            DateSerial(1900, 1, 1), _
                                            sLookup("REVTAR", "ITMPACK", " AMBKOD=" & sTirnakEkle(rowDt.Item("AMBKOD").ToString) & _
                                            " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))))

                    'Else

                    'sRevNo = ""

                    'nRevTar = Now

                    'End If

                    If sAmbar = "BART" Then

                        sDuns = sLookup("DUNSID2", "PLANTPRM", " CANB=" & sTirnakEkle(rowDt.Item("CUST").ToString) & _
                                                                " AND B9CD=" & sTirnakEkle(rowDt.Item("Shipto").ToString))

                    Else

                        sDuns = sLookup("DUNSID", "PLANTPRM", " CANB=" & sTirnakEkle(rowDt.Item("CUST").ToString) & _
                                                                " AND B9CD=" & sTirnakEkle(rowDt.Item("Shipto").ToString))

                    End If

                    sPlant = rowDt.Item("Shipto").ToString

                    'sPlant = sLookup("EDIPLANT", "PLANTPRM", " CANB=" & sTirnakEkle(rowDt.Item("CUST").ToString) & _
                    '                                       " AND B9CD=" & sTirnakEkle(rowDt.Item("Shipto").ToString))

                    sDuns1 = sLookup("USERF2", "PLANTPRM", " CANB=" & sTirnakEkle(rowDt.Item("CUST").ToString) & _
                                                            " AND B9CD=" & sTirnakEkle(rowDt.Item("Shipto").ToString))

                    If sDuns1 <> "" Then

                        sDuns = sDuns + "/" + sDuns1

                    End If

                    sMAMBKOD = ""
                    sMKKOD = ""
                    sMPKOD = ""
                    sMSPKOD = ""
                    sMKPKOD = ""

                    sSQL = " SELECT * " & _
                                " FROM  ITMPCKVW  " & _
                                " WHERE AMBKOD=" & sTirnakEkle(rowDt.Item("AMBKOD").ToString) & _
                                " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString)

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        sAmbKod = dtTemp.Rows(0)("AMBKOD").ToString

                        sAmbTanim = dtTemp.Rows(0)("AMBTAN").ToString

                        sMAMBKOD = dtTemp.Rows(0)("MAMBKOD").ToString

                        sMKKOD = dtTemp.Rows(0)("MKKOD").ToString

                        sMPKOD = dtTemp.Rows(0)("MPKOD").ToString

                        sMSPKOD = dtTemp.Rows(0)("MSPKOD").ToString

                        sMKPKOD = dtTemp.Rows(0)("MKPKOD").ToString

                    End If

                    nKap_En = CInt(rowDt.Item("KAP_EN").ToString * 100)

                    nKap_Boy = CInt(rowDt.Item("KAP_BOY").ToString * 100)

                    nKap_Yuks = CInt(rowDt.Item("KAP_YUK").ToString * 100)

                    sFatTipi = IIf(rowDt.Item("CustCountry").ToString = "Turkey", "Y", "I")

                    nKutu_En = 0

                    nKutu_Boy = 0

                    nKutu_Yuks = 0

                    sSQL = " SELECT BRMUZ,BRMGN,BRMYK " & _
                                " FROM  ITEMDIM  " & _
                                " WHERE ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        nKutu_En = CInt(dtTemp.Rows(0)("BRMGN").ToString * 100)

                        nKutu_Boy = CInt(dtTemp.Rows(0)("BRMUZ").ToString * 100)

                        nKutu_Yuks = CInt(dtTemp.Rows(0)("BRMYK").ToString * 100)

                    End If

                    sKutu_Tnm = sLookup("ITDSC", "ITEMASA", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString))

                    sPalet_Tnm = sLookup("ITDSC", "ITEMASA", " ITNBR=" & sTirnakEkle(rowDt.Item("PKOD").ToString))

                    sSepr_Tnm = sLookup("ITDSC", "ITEMASA", " ITNBR=" & sTirnakEkle(rowDt.Item("SPKOD").ToString))

                    sKapak_Tnm = sLookup("ITDSC", "ITEMASA", " ITNBR=" & sTirnakEkle(rowDt.Item("KPKOD").ToString))

                    sPusNo = rowDt.Item("PUSNO").ToString

                    If Copy(sPusNo, 0, 2) = "FO" Or Copy(sPusNo, 0, 2) = "EO" Then

                        sPusNo = ""

                    End If

                    Dim sAmbarTanimi As String = ""
                    Dim sAmbarAdr1 As String = ""
                    Dim sAmbarAdr2 As String = ""
                    Dim sAmbarZip As String = ""
                    Dim sAmbarCity As String = ""
                    Dim sAmbarCountry As String = ""

                    sSQL = " Select Name, addr##1, addr##2 , zip, city, country" & _
                                " From Whse " & _
                                " Where Whse= " & sTirnakEkle(sAmbar)

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        sAmbarTanimi = dtTemp.Rows(0)("Name").ToString
                        sAmbarAdr1 = dtTemp.Rows(0)("addr##1").ToString
                        sAmbarAdr2 = dtTemp.Rows(0)("addr##2").ToString
                        sAmbarZip = dtTemp.Rows(0)("zip").ToString
                        sAmbarCity = dtTemp.Rows(0)("city").ToString
                        sAmbarCountry = dtTemp.Rows(0)("country").ToString

                    End If

                    If chkLieferschein.Checked Then

                        sSQL = " INSERT INTO ANSLIEFER " & _
                                "(FATTIP,INVNO,INVTAR,DHZ969," & _
                                " COKZNB,SEVKTAR,DHCANB,DHB9CD," & _
                                " DHBYTX,KAPI,DDAITX,MIKTAR," & _
                                " KAPSAYI,KAP_EN,KAP_BOY,KAP_YUK," & _
                                " NETAGR,BRTAGR,HACIM,AGOB,HCOB," & _
                                " STKOB,CUSNM,MURNKOD,MURNTNM,DHBZTX,DHB0TX," & _
                                " DHB1TX,DHZ9JQ,DHZ9JR,DHAAGX,DHB6CD,DHAAGW," & _
                                " FEAAC4,FEAAC5,FEAAC6,FEZ9JV,FEZ9JW,FEAAC7," & _
                                " FEAAC8,FEAADW,SHPNM,SHPADR1,SHPADR2,TRNCODE," & _
                                " TRNDESC,MKKOD,MPKOD,MSPKOD,MKPKOD,MAMBKOD," & _
                                " KSAY,KMIK,PSAY,SPSAY,KPSAY,DUNSNO,KUTU_EN," & _
                                " KUTU_BOY,KUTU_YUK,KUTUTNM,PLT_TNM,SPR_TNM," & _
                                " KPK_TNM,KONTRAT,REVNO, REVTAR,PUSNO  )" & _
                                " VALUES (" & _
                                sTirnakEkle(sFatTipi) & "," & _
                                rowDt.Item("FaturaNo").ToString & "," & _
                                sTarih(rowDt.Item("FATURATAR").ToString) & "," & _
                                rowDt.Item("SHPNO").ToString & "," & _
                                rowDt.Item("PICKNO").ToString & "," & _
                                sTarih(rowDt.Item("SEVKTAR").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Cust").ToString) & "," & _
                                sTirnakEkle(sPlant) & "," & _
                                sTirnakEkle(rowDt.Item("ShipName").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("KAPI").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ITNBR").ToString) & "," & _
                                rowDt.Item("SHIPMIK").ToString & "," & _
                                nKapAdedi & "," & _
                                nKap_En & "," & _
                                nKap_Boy & "," & _
                                nKap_Yuks & "," & _
                                nNetAgr & "," & _
                                nBrtAgr & "," & _
                                nHacim & "," & _
                                sTirnakEkle(rowDt.Item("AGOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("HCOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("STKOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("CustName").ToString) & "," & _
                                sTirnakEkle(sMItmKod) & "," & _
                                sTirnakEkle(sMItmDesc) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##1").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##2").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##3").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##4").ToString) & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle(rowDt.Item("Shipcity").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Shipzip").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Shipcountry").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("CustAddr##1").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("CustAddr##2").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("CustAddr##3").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("CustAddr##4").ToString) & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle(rowDt.Item("Custcity").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Custzip").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Custcountry").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("AmbarAdi").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Addr##1").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Addr##2").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("TRNCODE").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Description").ToString) & "," & _
                                sTirnakEkle(sMKKOD) & "," & _
                                sTirnakEkle(sMPKOD) & "," & _
                                sTirnakEkle(sMSPKOD) & "," & _
                                sTirnakEkle(sMKPKOD) & "," & _
                                sTirnakEkle(sMAMBKOD) & "," & _
                                Convert.ToString(rowDt.Item("KSAY").ToString) & "," & _
                                Convert.ToString(rowDt.Item("KMIK").ToString) & "," & _
                                Convert.ToString(rowDt.Item("PSAY").ToString) & "," & _
                                Convert.ToString(rowDt.Item("SPMIK").ToString) & "," & _
                                Convert.ToString(rowDt.Item("KPMIK").ToString) & "," & _
                                sTirnakEkle(sDuns) & "," & _
                                Convert.ToString(nKutu_En) & "," & _
                                Convert.ToString(nKutu_Boy) & "," & _
                                Convert.ToString(nKutu_Yuks) & "," & _
                                sTirnakEkle(sKutu_Tnm) & "," & _
                                sTirnakEkle(sPalet_Tnm) & "," & _
                                sTirnakEkle(sSepr_Tnm) & "," & _
                                sTirnakEkle(sKapak_Tnm) & "," & _
                                sTirnakEkle(sKontrat) & "," & _
                                sTirnakEkle(sRevNo) & "," & _
                                sTarih(nRevTar) & "," & _
                                sTirnakEkle(sPusNo) & _
                                ")"

                        dbAccess.RunSql(sSQL)

                    End If

                    If chkGumrukCeki.Checked Then

                        Dim sKutuTanimi As String

                        If rowDt.Item("PSAY").ToString > 0 Then

                            sKutuTanimi = sLookup("Description", "Item", "Item=" & sTirnakEkle(rowDt.Item("PKOD").ToString))

                        Else

                            sKutuTanimi = sLookup("Description", "Item", "Item=" & sTirnakEkle(rowDt.Item("KKOD").ToString))

                        End If

                        sSQL = " INSERT INTO CEKIGUMR " & _
                                "(FATTIP,INVNO,INVTAR,DHZ969," & _
                                " SEVKTAR,DHCANB,DHB9CD," & _
                                " DHBYTX,DDAITX,MIKTAR," & _
                                " KAPSAYI,KAP_EN,KAP_BOY,KAP_YUK," & _
                                " NETAGR,BRTAGR,HACIM,AGOB,HCOB," & _
                                " STKOB, CUSNM, AMBKOD, AMBTAN, " & _
                                " SHPNM, MURNKOD, MURNTNM )" & _
                                " VALUES (" & _
                                sTirnakEkle(sFatTipi) & "," & _
                                rowDt.Item("FaturaNo").ToString & "," & _
                                sTarih(rowDt.Item("FATURATAR").ToString) & "," & _
                                rowDt.Item("SHPNO").ToString & "," & _
                                sTarih(rowDt.Item("SEVKTAR").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Cust").ToString) & "," & _
                                sTirnakEkle(sPlant) & "," & _
                                sTirnakEkle(rowDt.Item("ShipName").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ITNBR").ToString) & "," & _
                                rowDt.Item("SHIPMIK").ToString & "," & _
                                nKapAdedi & "," & _
                                nKap_En & "," & _
                                nKap_Boy & "," & _
                                nKap_Yuks & "," & _
                                nNetAgr & "," & _
                                nBrtAgr & "," & _
                                nHacim & "," & _
                                sTirnakEkle(rowDt.Item("AGOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("HCOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("STKOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("CustName").ToString) & "," & _
                                sTirnakEkle(sAmbKod) & "," & _
                                sTirnakEkle(sKutuTanimi) & "," & _
                                sTirnakEkle(sAmbarTanimi) & "," & _
                                sTirnakEkle(sMItmKod) & "," & _
                                sTirnakEkle(sMItmDesc) & _
                                ")"

                        dbAccess.RunSql(sSQL)

                    End If

                    If chkNakliyeBildirimi.Checked Then

                        sSQL = " INSERT INTO ANSNAKLBILD " & _
                                " (FATTIP,INVNO,INVTAR,DHZ969," & _
                                " COKZNB,SEVKTAR,DHCANB,DHB9CD," & _
                                " DHBYTX,KAPI,DDAITX,MIKTAR," & _
                                " KAPSAYI,KAP_EN,KAP_BOY,KAP_YUK," & _
                                " NETAGR,BRTAGR,HACIM,AGOB,HCOB," & _
                                " STKOB,PRICE,TUTAR,MURNKOD,MURNTNM,CUSNM," & _
                                " DHBZTX,DHB0TX,DHB1TX,DHZ9JQ,DHZ9JR,DHAAGX," & _
                                " DHB6CD,DHAAGW,FERIDX,SHPID,SHPNM," & _
                                " NAKL_TNM,NAKL_TO,NAKL_CC,NAKL_FAX," & _
                                " PUSNO,yolsure,PLTMIK,HKMIK," & _
                                " KUTU_EN,KUTU_BOY,KUTU_YUK) " & _
                                " VALUES (" & _
                                sTirnakEkle(sFatTipi) & "," & _
                                rowDt.Item("FaturaNo").ToString & "," & _
                                sTarih(rowDt.Item("FATURATAR").ToString) & "," & _
                                rowDt.Item("SHPNO").ToString & "," & _
                                rowDt.Item("PICKNO").ToString & "," & _
                                sTarih(rowDt.Item("SEVKTAR").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Cust").ToString) & "," & _
                                sTirnakEkle(sPlant) & "," & _
                                sTirnakEkle(rowDt.Item("ShipName").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("KAPI").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ITNBR").ToString) & "," & _
                                rowDt.Item("SHIPMIK").ToString & "," & _
                                nKapAdedi & "," & _
                                nKap_En & "," & _
                                nKap_Boy & "," & _
                                nKap_Yuks & "," & _
                                nNetAgr & "," & _
                                nBrtAgr & "," & _
                                nHacim & "," & _
                                sTirnakEkle(rowDt.Item("AGOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("HCOB").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("STKOB").ToString) & "," & _
                                rowDt.Item("Price").ToString & "," & _
                                rowDt.Item("Tutar").ToString & "," & _
                                sTirnakEkle(sMItmKod) & "," & _
                                sTirnakEkle(sMItmDesc) & "," & _
                                sTirnakEkle(rowDt.Item("CustName").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##1").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##2").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##3").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("ShipAddr##4").ToString) & "," & _
                                sTirnakEkle("") & "," & _
                                sTirnakEkle(rowDt.Item("Shipcity").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Shipzip").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("Shipcountry").ToString) & "," & _
                                sTirnakEkle(rowDt.Item("curr_code").ToString) & "," & _
                                sTirnakEkle(sAmbar) & "," & _
                                sTirnakEkle(sAmbarTanimi) & "," & _
                                sTirnakEkle(rowDt.Item("CarrierName").ToString) & "," & _
                                " ''," & _
                                " ''," & _
                                sTirnakEkle(rowDt.Item("CarrierAddres").ToString) & "," & _
                                sTirnakEkle(sPusNo) & "," & _
                                IIf(rowDt.Item("YolSuresi").ToString = "", 0, rowDt.Item("YolSuresi").ToString) & "," & _
                                rowDt.Item("PSAY").ToString & "," & _
                                rowDt.Item("HKSAY").ToString & "," & _
                                Convert.ToString(nKutu_En) & "," & _
                                Convert.ToString(nKutu_Boy) & "," & _
                                Convert.ToString(nKutu_Yuks) & "" & _
                                ")"

                        dbAccess.RunSql(sSQL)

                        sSQL = " Select 1 " & _
                                    " From ANSYUKLADR" & _
                                    " Where SHPNM=" & sTirnakEkle(sAmbarTanimi)

                        dtTemp = dbAccess.RunSql(sSQL)

                        If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count = 0 Then

                            sSQL = " INSERT INTO ANSYUKLADR " & _
                                        " (SHPID,SHPNM,SHIP1,SHIP2,SHPZP,SCNTR,SCITY) " & _
                                   " Values " & _
                                        "( '" & _
                                        (sAmbar) & "','" & _
                                        (sAmbarTanimi) & "','" & _
                                        (sAmbarAdr1) & "','" & _
                                        (sAmbarAdr2) & "','" & _
                                        (sAmbarZip) & "','" & _
                                        (sAmbarCountry) & "','" & _
                                        (sAmbarCity) & "'" & _
                                        ")"

                            dbAccess.RunSql(sSQL)

                        End If

                    End If

                Next

            End If

            If chkLieferschein.Checked Then

                RaporCagir("RLiefershein.rpt")

            End If

            If chkGumrukCeki.Checked Then

                RaporCagir("RGumruk_Ceki_Liste.rpt")

            End If

            If chkNakliyeBildirimi.Checked Then

                RaporCagir("RNakliyeciBildirimi.rpt")

            End If

            '

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "FaturaNo", sender, e)
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