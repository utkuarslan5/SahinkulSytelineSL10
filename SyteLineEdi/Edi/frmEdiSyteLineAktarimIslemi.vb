Imports Janus.Windows.GridEX

Public Class frmEdiSyteLineAktarimIslemi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtDate As New DataTable
    Dim dtEdiMst As New DataTable
    Dim dtEdiPrc As New DataTable
    Dim dtTemp As New DataTable
    Dim dtTemp1 As New DataTable
    Dim sSQL As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnEdiMstAktar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdiMstAktar.Click
        Dim sHata As String
        Dim sDepo As String
        Dim sVade As String
        Dim sCarrier As String
        Dim sPlantName As String
        Dim sStatu As String
        Dim sKontratNo As String
        Dim sBzmItnbr As String
        Dim sAmbKod As String
        Dim sCurID As String
        Dim sCustomer As String
        Dim nCustSeq As Integer
        Dim nMpxCum As Double
        Dim dMpxCumTar As Date
        Dim nPrice As Double
        Dim nKMik As Double
        Dim nKumLevel As Integer

        Try

            Durum.Text = "EDI Work File dosyasýndan Master File a Bilgiler Aktarýlýyor...Lütfen Bekleyiniz..."

            StatusStrip1.Refresh()

            sSQL = " DELETE " &
                            " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIWRK" &
                            " WHERE DELDTE=0 And CRDUSR=" & sTirnakEkle(KullaniciAdi))

            db.RunSql(sSQL)

            sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, ("EDIMST" & " WHERE CRDUSR=") + sTirnakEkle(KullaniciAdi))

            db.RunSql(sSQL)

            System.Threading.Thread.Sleep(100)

            sSQL = "Update ediwrk" & _
                    " Set Cusitm= REPLICATE('*',Len(Cusitm) - Len(LTrim(Cusitm))) + LTrim(Cusitm)" & _
                    " Where crdusr=" & sTirnakEkle(KullaniciAdi)

            db.RunSql(sSQL)

            sSQL = " Update EDIWRK" & _
                      " Set CONTNO = ( Select Top 1 KNTRT " & _
                                          " From PLANTPRMJL " & _
                                          " where  SDRODID = SENDER " & _
                                              " and SHIPTO = PLANTID " & _
                                              " and KAPI = GATEID " & _
                                              " and ITEM = CUSITM " & _
                                              " and KNTRT = CONTNO ) " & _
                      " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                          " And CONTNO = " & sTirnakEkle("")

            db.RunSql(sSQL)

            'sSQL = (((" Update " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & " Set CONTNO = ( Select KNTRT " & " From ") + SetTableName(My.Settings.LibNameEDI, "PLANTPRMJL") & " where  SDRODID = SENDER " & " and EDIPLANT = PLANTID " & " and KAPI = GATEID " & " and ITEM = CUSITM) " & " WHERE CRDUSR=") + sTirnakEkle(KullaniciAdi) & " And CONTNO = ") + sTirnakEkle("")

            'db.RunSql(sSQL)

            'sSQL = " Update " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
            '        " Set PlantId = ( Select SHIPTO " & _
            '                                    " From " & SetTableName(My.Settings.LibNameEDI, "KontPlant") & _
            '                                    " where  KNTRT = CONTNO " & " and EDIPLANT = PLANTID " & _
            '                                    " and KAPI = GATEID " & " and ITEM = CUSITM )" & _
            '        " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
            '        " And PLANTID <> ( Select SHIPTO " & _
            '                            " From " & SetTableName(My.Settings.LibNameEDI, "KontPlant") & _
            '                            " where  KNTRT = CONTNO " & " and EDIPLANT = PLANTID " & " and KAPI = GATEID " & " and ITEM = CUSITM)"

            'db.RunSql(sSQL)
            'EdiPlant Alaný için kullanýlýyordu kapatýldý

            System.Threading.Thread.Sleep(100)

            sSQL = " INSERT INTO " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                      " ( SENDER,RECEIVER,MSGNUM,MSGDTM,MSGTX1," & _
                        " MSGTX2,MSGTX3,DUNSID,PLANTID,CUSITM," & _
                        " GATEID,CONTNO,CUMQTY,CUMREF,CUMDTE," & _
                        " DELDTE,RECCNT,DELQTY," & _
                        " MSGSTAT,PUSNO,MISSUER,MATHNDCD," & _
                        " KANBANNO,CRDDTE,CRDTIM,CRDUSR," & _
                        " USERF6,USERF1,L3PREF, USERF4 )" & _
                    " SELECT  SENDER,RECEIVER,MSGNUM,MSGDTM,MSGTX1, " & _
                        " MSGTX2,MSGTX3,DUNSID,PLANTID,CUSITM,GATEID," & _
                        " CONTNO,CUMQTY,CUMREF,CUMDTE,DELDTE,COUNT(DELQTY)," & _
                        " SUM(DELQTY),MSGSTAT,PUSNO,MISSUER,MATHNDCD,KANBANNO," & _
                        Now.Date.ToString("yyyyMMdd") & "," & Now.Date.ToString("HHSS") & "," & _
                        sTirnakEkle(KullaniciAdi) & ",USERF5, USERF1 , L3PREF , DTE_STR  " & _
                        " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                        " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                        " GROUP BY SENDER,RECEIVER,MSGNUM,MSGDTM,MSGTX1," & _
                        " MSGTX2,MSGTX3,DUNSID,PLANTID,CUSITM,GATEID,CONTNO,CUMQTY," & _
                        " CUMREF,CUMDTE,DELDTE,MSGSTAT,PUSNO,MISSUER,MATHNDCD,KANBANNO,USERF5,USERF1,L3PREF,DTE_STR"

            db.RunSql(sSQL)

            sSQL = " SELECT DISTINCT SENDER,MSGNUM,DUNSID,PLANTID,CUSITM,GATEID,CONTNO,CUMQTY " & _
                        " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi)

            dt = db.RunSql(sSQL)

            Bar.Value = 0

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Bar.Maximum = dt.Rows.Count
            End If

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows

                    sBzmItnbr = ""
                    sKontratNo = ""
                    sAmbKod = ""
                    sDepo = My.Settings.EDIDepo
                    sCurID = ""
                    sCarrier = ""
                    sPlantName = ""
                    sCustomer = ""
                    nMpxCum = 0
                    dMpxCumTar = Nothing
                    nKMik = 0

                    If row("CONTNO").ToString = "" Then

                        If (row("SENDER").ToString <> "") AndAlso (row("SENDER").ToString <> "FORD") Then

                            sSQL = " SELECT BZMITM,AMBKOD,KNTRT,CANB,HOUSE,KUMLEVEL, Cust_seq, Vade " & _
                                    " FROM " & SetTableName(My.Settings.LibNameEDI, "PLANTPRMJL") & _
                                    " WHERE  SDRODID=" & sTirnakEkle(row("SENDER").ToString) & _
                                            " AND SHIPTO=" & sTirnakEkle(row("PLANTID").ToString) & _
                                            " AND KAPI=" & sTirnakEkle(row("GATEID").ToString) & _
                                            " AND ITEM=" & sTirnakEkle(row("CUSITM").ToString)

                            dtTemp = db.RunSql(sSQL)

                            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                sCustomer = dtTemp.Rows(0)("CANB").ToString
                                nCustSeq = dtTemp.Rows(0)("Cust_Seq").ToString
                                sBzmItnbr = dtTemp.Rows(0)("BZMITM").ToString
                                sAmbKod = dtTemp.Rows(0)("AMBKOD").ToString
                                sKontratNo = dtTemp.Rows(0)("KNTRT").ToString
                                sDepo = dtTemp.Rows(0)("HOUSE").ToString
                                nKumLevel = IIf(dtTemp.Rows(0)("KUMLEVEL").ToString = "", 0, dtTemp.Rows(0)("KUMLEVEL").ToString)
                                GetRowInfo(sVade, dtTemp, 0, "Vade")
                            End If

                        Else

                            sSQL = " SELECT BZMITM,AMBKOD,KNTRT,CANB,HOUSE,KUMLEVEL, Cust_seq, Vade " & _
                                       "FROM " & SetTableName(My.Settings.LibNameEDI, "PLANTPRMJL") & _
                                       " WHERE SHIPTO=" & sTirnakEkle(row("PLANTID").ToString) & _
                                       " AND KAPI=" & sTirnakEkle(row("GATEID").ToString) & _
                                       " AND ITEM=" & sTirnakEkle(row("CUSITM").ToString)

                            dtTemp = db.RunSql(sSQL)

                            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                sCustomer = dtTemp.Rows(0)("CANB").ToString
                                nCustSeq = dtTemp.Rows(0)("Cust_Seq").ToString
                                sBzmItnbr = dtTemp.Rows(0)("BZMITM").ToString
                                sAmbKod = dtTemp.Rows(0)("AMBKOD").ToString
                                sKontratNo = dtTemp.Rows(0)("KNTRT").ToString
                                sDepo = dtTemp.Rows(0)("HOUSE").ToString
                                nKumLevel = dtTemp.Rows(0)("KUMLEVEL").ToString
                                GetRowInfo(sVade, dtTemp, 0, "Vade")
                            End If

                        End If

                    Else

                        sKontratNo = row("CONTNO").ToString

                        sSQL = " SELECT BZMITM,AMBKOD,CANB,HOUSE,KUMLEVEL, Cust_seq, Vade " & _
                                "FROM " & SetTableName(My.Settings.LibNameEDI, "PLANTPRMJL") & _
                                " WHERE KNTRT=" & sTirnakEkle(sKontratNo) & _
                                " AND SHIPTO=" & sTirnakEkle(row("PLANTID").ToString) & _
                                " AND KAPI=" & sTirnakEkle(row("GATEID").ToString) & _
                                " AND ITEM=" & sTirnakEkle(row("CUSITM").ToString)

                        dtTemp = db.RunSql(sSQL)

                        If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                            GetRowInfo(sBzmItnbr, dtTemp, 0, "BZMITM")
                            GetRowInfo(sAmbKod, dtTemp, 0, "AMBKOD")
                            GetRowInfo(sCustomer, dtTemp, 0, "CANB")
                            GetRowInfo(nCustSeq, dtTemp, 0, "Cust_Seq")
                            GetRowInfo(sDepo, dtTemp, 0, "HOUSE")

                            nKumLevel = IIf(dtTemp.Rows(0)("KUMLEVEL").ToString = "", 0, dtTemp.Rows(0)("KUMLEVEL").ToString)

                            If sDepo = "" Then
                                sDepo = "MAIN"
                            End If

                            GetRowInfo(sVade, dtTemp, 0, "Vade")

                        End If

                    End If
                    ' Mapix Kümülatif Miktar ve Tarihi
                    'nKumLevel 1 ise Kapý ya göre kümül alýnýyor.

                    If nKumLevel = 1 Then

                        sSQL = " SELECT DDARQK,TAR " & _
                                        "FROM " & SetTableName(My.Settings.LibNameEDI, "OFFITEMBL") & _
                                        " WHERE CANBK=" & sTirnakEkle(sCustomer) & _
                                        " AND B9CDK =" & sTirnakEkle(row("PLANTID").ToString) & _
                                        " AND AITXK=" & sTirnakEkle(sBzmItnbr) & _
                                        " And GATE=" & sTirnakEkle(row("GATEID").ToString)

                    Else

                        sSQL = " SELECT DDARQK,TAR " & _
                                    "FROM " & SetTableName(My.Settings.LibNameEDI, "OFFITEMBL") & _
                                    " WHERE CANBK=" & sTirnakEkle(sCustomer) & _
                                    " AND B9CDK =" & sTirnakEkle(row("PLANTID").ToString) & _
                                    " AND AITXK=" & sTirnakEkle(sBzmItnbr)

                    End If

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        nMpxCum = dtTemp.Rows(0)("DDARQK").ToString

                        dMpxCumTar = IIf(dtTemp.Rows(0)("TAR").ToString = "", Nothing, dtTemp.Rows(0)("TAR").ToString)

                    Else

                        nMpxCum = 0

                        dMpxCumTar = Nothing

                    End If

                    'sKapi = sLookup("ATF1CD", SetTableName(My.Settings.LibNameEDI, "MBATREP"), " ATF1CD=" & sTirnakEkle(sBzmItnbr))

                    'If sKapi.TrimEnd() = "" Then
                    '    sStatu = "3"
                    '    sHata = sHata & " Kapý Tanýmsýz"
                    'End If

                    ' ItemBl dan Depo kaydý bulunur.
                    sDepo = sLookup("whse", SetTableName(My.Settings.LibNameEDI, "itemwhse"), (" item=" & sTirnakEkle(sBzmItnbr) & " AND whse=") + sTirnakEkle(sDepo))

                    ' Kutudaki Miktar Bulunur
                    nKMik = nLookup("KMIK", SetTableName(My.Settings.LibNameEDI, "ITMPACK"), (" ITNBR=" & sTirnakEkle(sBzmItnbr) & " AND AMBKOD=") + sTirnakEkle(sAmbKod))

                    sSQL = " SELECT cont_price " & _
                                        " FROM itemcustprice_Fatura2" & _
                                            " WHERE cust_num=" & sTirnakEkle(sCustomer) & _
                                            " AND item = " & sTirnakEkle(sBzmItnbr) & _
                                            " And cust_item = " & sTirnakEkle(row("CUSITM").ToString) & _
                                            " AND effect_date <=Cast('" & Now.Date.ToString("yyyyMMdd") & "' As DateTime)" & _
                                        " ORDER BY effect_date DESC "

                    dtTemp = db.RunSql(sSQL)

                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        nPrice = dtTemp.Rows(0)("cont_price").ToString
                        'sCurID = dtTemp.Rows(0)("CURID").ToString
                    Else
                        nPrice = 0
                    End If

                    sSQL = " SELECT Carrier, Plantname FROM " & SetTableName(My.Settings.LibNameEDI, "Plantprm") & _
                                    " WHERE CANB=" & sTirnakEkle(sCustomer) & _
                                    " AND B9CD=" & sTirnakEkle(row("PLANTID").ToString)

                    dtTemp = db.RunSql(sSQL)

                    If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                        sCarrier = dtTemp.Rows(0)("Carrier").ToString

                        sPlantName = dtTemp.Rows(0)("Plantname").ToString

                    End If

                    sHata = ""

                    If (row("CUMQTY").ToString = 0 AndAlso nMpxCum <> 0) Or (row("CUMQTY").ToString > nMpxCum) Then
                        sStatu = "2"
                    Else
                        sStatu = "0"
                    End If

                    If sStatu = "2" Then
                        sHata = sHata & " Kümülatif Büyük"
                    End If

                    If sBzmItnbr.TrimEnd() = "" Then
                        sStatu = "3"
                        sHata = sHata & " Malzeme Tanýmsýz"
                    End If

                    If sDepo.TrimEnd() = "" Then
                        sStatu = "3"
                        sHata = sHata & " Depo Tanýmsýz"
                    End If

                    If sAmbKod.TrimEnd() = "" Then
                        sStatu = "3"
                        sHata = sHata & " Ambalaj Tanýmsýz"
                    End If

                    If nPrice = 0 Then
                        sStatu = "3"
                        sHata = sHata & " Fiyat Tanýmsýz "
                    End If

                    If sCarrier.TrimEnd() = "" Then
                        sStatu = "3"
                        sHata = sHata & " Nakliyeci Tanýmsýz"
                    End If

                    sHata = Copy(sHata, 0, 150)

                    sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                           " SET MPXITM=" & sTirnakEkle(sBzmItnbr.TrimEnd()) & _
                                " ,MPXCUM=" & Convert.ToString(nMpxCum) & _
                                " ,MPXCMT=" & sTirnakEkle(IIf(dMpxCumTar.ToString("yyyy-MM-dd") = "0001-01-01", "", dMpxCumTar.ToString("yyyy-MM-dd"))) & _
                                " ,MPXCUS=" & sTirnakEkle(sCustomer) & _
                                " ,MPXCUSSEQ = " & nCustSeq & _
                                " ,MPXWHS=" & sTirnakEkle(sDepo.TrimEnd()) & _
                                " ,MPXPRC=" & Convert.ToString(nPrice) & _
                                " ,MPXAMB=" & sTirnakEkle(sAmbKod.TrimEnd()) & _
                                " ,MPXKADT=" & Convert.ToString(nKMik) & _
                                " ,MPXNAKL=" & sTirnakEkle(sCarrier.TrimEnd()) & _
                                " ,MPXPLNM=" & sTirnakEkle(sPlantName.TrimEnd()) & _
                                " ,HATADRM=" & sTirnakEkle(sStatu) & _
                                " ,HATAMSG=" & sTirnakEkle(sHata) & _
                                " ,CONTNO=" & sTirnakEkle(sKontratNo.TrimEnd()) & _
                                " ,KUMLEVEL=" & nKumLevel & _
                                " ,Vade=" & sTirnakEkle(sVade) & _
                            " WHERE SENDER=" & sTirnakEkle(row("SENDER").ToString.TrimEnd()) & _
                            " AND MSGNUM=" & sTirnakEkle(row("MSGNUM").ToString.TrimEnd()) & _
                            " AND DUNSID=" & sTirnakEkle(row("DUNSID").ToString.TrimEnd()) & _
                            " AND PLANTID=" & sTirnakEkle(row("PLANTID").ToString.TrimEnd()) & _
                            " AND GATEID=" & sTirnakEkle(row("GATEID").ToString.TrimEnd()) & _
                            " AND CUSITM=" & sTirnakEkle(row("CUSITM").ToString.TrimEnd()) & _
                            " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                    db.RunSql(sSQL)

                    Bar.Value = Bar.Value + 1

                    Application.DoEvents()

                Next

            End If

            Durum.Text = "..."

            Bar.Value = 0

            MessageBox.Show("Edi Dosyasýndan Veriler Aktarýldý...", "Ekip Mapics", MessageBoxButtons.OK)

            btnListele_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim ds As DataSet

        ds = grdDesAdv.DataSource

        ExceleAktar(ds.Tables(0))
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnKumulatifMiktarGuncelle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sQuery, sB9CDK As String

        Dim nOffTar As Double

        Dim dsOFFITEMBL, dsEDIMST, dsPLANTPRM, dsmatltran As DataSet

        Try

            If MessageBox.Show("...Kümülatif Miktarlar Düzeltilecek? Onaylýyor musunuz?...", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                If nLookup("COUNT(*)", "EDIPRGUSR", " KUMUPD=" & "1") <> 0 Then

                    MessageBox.Show("Kümülatif Miktarlar baþka bir kullanýcý tarafýndan güncelleniyor,iþlem yapamazsýnýz...", "Ekip Mapics", MessageBoxButtons.OK)

                    End

                End If

                sB9CDK = ""

                sQuery = " UPDATE EDIPRGUSR SET KUMUPD=1 WHERE USRKOD='" & KullaniciAdi & "'"

                db.RunSql(sQuery, True)

                sQuery = "SELECT m.trans_num, c.cust_num, c.cust_seq, m.item, m.qty, " & _
                                " m.ref_line_suf, m.ref_num, co.cust_item " & _
                            " FROM matltran m, " & _
                                         " co   c, " & _
                                        " coitem co " & _
                            "where m.ref_num = c.co_num " & _
                                "and   m.ref_num = co.co_num " & _
                                "and   m.ref_line_suf = co.co_line " & _
                                "AND   m.trans_type = 'S' " & _
                                "AND   m.ref_type = 'O' " & _
                                "AND   (m.Uf_Matl_CumFlag <> '1' or m.Uf_Matl_CumFlag is null )" & _
                                "AND   m.qty < 0 "
                '---------------1
                dsmatltran = db.RunSql(sQuery, "matltran")

                If Not dsmatltran Is Nothing Then

                    If dsmatltran.Tables("matltran").Rows.Count > 0 Then

                        For i As Integer = 0 To dsmatltran.Tables("matltran").Rows.Count - 1

                            With dsmatltran.Tables("matltran").Rows(i)

                                sQuery = "UPDATE matltran SET Uf_Matl_CumFlag='1' " & _
                                                " WHERE trans_num =" & .Item("trans_num").ToString

                                db.RunSql(sQuery, True)

                                If nLookup("COUNT(*)", "OFFITEMBL", _
                                  " AENBK='" & My.Settings.Company & "'" & _
                                  " AND CANBK='" & .Item("cust_num").ToString & "'" & _
                                  " AND PlantSequence=" & .Item("cust_seq").ToString & _
                                  " AND AITXK='" & .Item("item").ToString & "'") = 0 Then

                                    sQuery = " SELECT B9CD FROM PLANTPRM " & _
                                                " WHERE CANB = '" & .Item("cust_num").ToString & "'" & _
                                                " AND   PLANTSEQUENCE = " & .Item("cust_seq").ToString

                                    dsPLANTPRM = db.RunSql(sQuery, "PLANTPRM")

                                    If dsPLANTPRM.Tables("PLANTPRM").Rows.Count > 0 Then

                                        sB9CDK = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("B9CD").ToString

                                    End If

                                    sQuery = "INSERT INTO OFFITEMBL " & _
                                             "(AENBK,CANBK,PLANTSEQUENCE,AITXK,DDARQK,USERNAME,TAR,UTIME,B9CDK ) " & _
                                             " VALUES ( " & _
                                                 "'" & My.Settings.Company & "','" & .Item("cust_num").ToString & "'," & _
                                                .Item("cust_seq").ToString & ",'" & .Item("item").ToString & "'," & _
                                                CStr(.Item("qty").ToString * -1) & ",'" & KullaniciAdi & "','" & _
                                                Now.Date.ToString("yyyy-MM-dd") & "','" & Now.ToString("yyyy-MM-dd hh:mm:ss") & "','" & sB9CDK & "') "

                                    db.RunSql(sQuery, True)

                                Else

                                    sQuery = " UPDATE OFFITEMBL SET " & _
                                                " DDARQK=DDARQK + " & CStr(.Item("qty").ToString * -1) & ", " & _
                                                " USERNAME='" & KullaniciAdi & "'," & _
                                                " TAR='" & Now.Date.ToString("yyyy-MM-dd") & "', " & _
                                                " UTIME='" & Now.ToString("yyyy-MM-dd hh:mm:ss") & "'" & _
                                            " WHERE AENBK='" & My.Settings.Company & "'" & _
                                                " AND   CANBK='" & .Item("cust_num").ToString & "'" & _
                                                " AND   PLANTSEQUENCE=" & .Item("cust_seq").ToString & _
                                                " AND   AITXK='" & .Item("item").ToString & "'"

                                    db.RunSql(sQuery, True)

                                End If

                            End With

                        Next i

                    End If

                End If

                '-------------------------1

                '-------------------------2
                sQuery = " SELECT * " & _
                            " FROM EDIMST " & _
                            " WHERE CRDUSR='" & KullaniciAdi & "'"

                dsEDIMST = db.RunSql(sQuery, "EDIMST")

                If Not dsEDIMST Is Nothing Then

                    If dsEDIMST.Tables("EDIMST").Rows.Count > 0 Then

                        For i As Integer = 0 To dsEDIMST.Tables("EDIMST").Rows.Count - 1

                            With dsEDIMST.Tables("EDIMST").Rows(i)
                                '-------------------------3

                                sQuery = " SELECT DDARQK,TAR " & _
                                            " FROM OFFITEMBL " & _
                                            " WHERE AENBK='" & My.Settings.Company & "'" & _
                                                " AND CANBK='" & Trim(.Item("mpxcus").ToString) & "'" & _
                                                " AND B9CDK='" & Trim(.Item("PLANTID").ToString) & "'" & _
                                                " AND AITXK='" & Trim(.Item("MPXITM").ToString) & "'" & _
                                                " AND PlantSequence = '" & Trim(.Item("PlantSeq").ToString) & "'"

                                dsOFFITEMBL = db.RunSql(sQuery, "OFFITEMBL")

                                If dsOFFITEMBL.Tables("OFFITEMBL").Rows.Count > 0 Then

                                    For k As Integer = 0 To dsOFFITEMBL.Tables("OFFITEMBL").Rows.Count - 1

                                        nOffTar = CDate(dsOFFITEMBL.Tables("OFFITEMBL").Rows(k).Item("TAR").ToString).ToString("yyyyMMdd")

                                        If (.Item("MPXCUM").ToString <> dsOFFITEMBL.Tables("OFFITEMBL").Rows(k).Item("DDARQK").ToString) Or _
                                                       (.Item("MPXCMT").ToString <> nOffTar) Then

                                            sQuery = " UPDATE EDIMST SET " & _
                                                        " MPXCUM=" & dsOFFITEMBL.Tables("OFFITEMBL").Rows(k).Item("DDARQK").ToString & _
                                                        " ,MPXCMT='" & CDate(dsOFFITEMBL.Tables("OFFITEMBL").Rows(k).Item("TAR").ToString).ToString("yyyyMMdd") & "'" & _
                                                    " WHERE MPXCUS='" & RTrim(.Item("MPXCUS").ToString) & "'" & _
                                                        " AND PLANTID='" & RTrim(.Item("PLANTID").ToString) & "'" & _
                                                        " AND PLANTSEQ ='" & RTrim(.Item("PLANTSEQ").ToString) & "'" & _
                                                        " AND MPXITM='" & RTrim(.Item("MPXITM").ToString) & "'"

                                            db.RunSql(sQuery, True)

                                        End If

                                    Next k

                                End If

                            End With

                        Next i

                    End If

                End If

                '----------------3
                '----------------2

                'Hata durumu duzeltme KADER 28/04/2005 Persembe ----------------------------
                sQuery = " UPDATE EDIMST " & _
                  " SET HATADRM = '0'" & _
                      " WHERE MPXCUM >= CUMQTY " & _
                            " AND CUMQTY <> 0 " & _
                            " AND HATADRM = '2'"

                db.RunSql(sQuery, True)

                sQuery = " UPDATE EDIMST " & _
                  " SET HATADRM = '2'" & _
                    " WHERE MPXCUM < CUMQTY" & _
                        " AND  HATADRM = '0'"

                db.RunSql(sQuery, True)

                sQuery = " UPDATE EDIPRGUSR  SET KUMUPD='0' WHERE USRKOD='" & KullaniciAdi & "'"

                db.RunSql(sQuery, True)

                MessageBox.Show("Kümülatif Miktarlar Güncellendi...", "Ekip Mapics", MessageBoxButtons.OK)

                btnListele_Click(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnListele_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListele.Click
        Dim sQuery As String

        Dim dsEdiMst As DataSet

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        grdDesAdv.Visible = True

        Try

            sQuery = " Select * " & _
                        " From EdiMst " & _
                        " Where CRDUSR=" & sTirnakEkle(KullaniciAdi)

            dsEdiMst = db.RunSql(sQuery, "EdiMst")

            grdDesAdv.DataSource = dsEdiMst

            grdDesAdv.DataMember = "EdiMst"

            grdDesAdv.Visible = True

            grdDesAdv.Refresh()

            grdDesAdv.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both

            Duzenle(grdDesAdv, False)

            grdDesAdv.CollapseGroups()

            If dsEdiMst.Tables("EdiMst").Rows.Count > 0 Then

                btnNetTarihGuncelle.Enabled = True

            Else

                btnNetTarihGuncelle.Enabled = False

                btnSyteLinDatarlariAktar.Enabled = False

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub btnMesajSil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMesajSil.Click
        frmMesajSil.ShowDialog()

        btnListele_Click(sender, e)
    End Sub

    Private Sub btnSyteLinDatarlariAktar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyteLinDatarlariAktar.Click
        Dim sSQL As String = ""
        Dim sIslDrm As String = ""
        Dim sPUSNO As String = ""
        Dim sMsgGunPUS As String = ""
        Dim sPUS As String = ""
        Dim sL3PREF As String = ""
        Dim sMusteri As String = ""
        Dim sSipKapi As String = ""
        Dim nKayit As Integer
        Dim nPrmSifir As Integer
        Dim nhafta As Integer
        Dim nyil As Integer
        Dim nsip As Integer
        Dim nTopNetQty As Double
        Dim nNetMiktar As Double
        Dim nMaxMsgTar As Double

        prevCustomer = ""
        prevCustSeq = 0
        prevGateId = ""
        prevNetdte = Nothing
        prevPusNo = ""
        prevL3PRef = ""
        prevConum = 0
        prevColine = 0

        Try

            If nLookup("COUNT(*)", _
                   "ActiveBgTasks", _
                   " taskname like 'Mrp%'") <> 0 Then

                MessageBox.Show("Sistemde MRP Çalýþmakta Lütfen Tamamlanmasýný Bekleyiniz!", Application.ProductName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)

                Exit Sub

            End If

            If nLookup("COUNT(*)", _
                       SetTableName(My.Settings.LibNameEDI, "edi_co"), _
                       " cast(cust_num As nvarchar(7)) + cast(cust_seq as nvarchar(4))  IN (SELECT cast(MPXCUS as nvarchar(7)) + cast(MPXCUSSEQ As nvarchar(4)) From " & _
                        SetTableName(My.Settings.LibNameEDI, "edimst") & " Where CRDUSR=" & sTirnakEkle(KullaniciAdi) & ") And posted=0") <> 0 Then

                sMusteri = sLookup(" Distinct cast(cust_num as nvarchar(7))  +" & sTirnakEkle("/") & " + Cast(cust_seq As nvarchar(4))", _
                                   SetTableName(My.Settings.LibNameEDI, "edi_co"), _
                                   " cast(cust_num As nvarchar(7)) + cast(cust_seq as nvarchar(4))  IN (SELECT cast(MPXCUS as nvarchar(7)) + cast(MPXCUSSEQ As nvarchar(4))  FROM " & _
                                    SetTableName(My.Settings.LibNameEDI, "edimst") & " Where CRDUSR=" & sTirnakEkle(KullaniciAdi) & ") And posted=0")

                MessageBox.Show("Syteline Edi Kütüklerde iþlenmemiþ " & sMusteri & " nolu Müþteri/Plant e ait sipariþler var,iþlem yapamazsýnýz...", Application.ProductName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)

                Exit Sub

            End If
            '
            ' sSQL=' SELECT CDAITX ' +
            ' ' FROM ' + SetTableName(dtmEkip.LibNameEDI,'EDIMST')+ ' , ' + SetTableName(dtmEkip.LibNameEDI,'EDISIPCOM'&
            ' ' WHERE   PLANTID= C6B9CD and GATEID <> c6F1CD and '
            ' ' MPXITM = cdaitx and MPXCUS = C6CANB '
            '

            'sSipKapi = sLookup("CDAITX", _
            '        (SetTableName(My.Settings.LibNameEDI, "EDIMST") & " , " & SetTableName(My.Settings.LibNameEDI, "EDISIPCOM")), _
            '        " PLANTID= C6B9CD and GATEID <> c6F1CD and PLANTID Not in ('TK','521','RK') " & _
            '        " And MPXITM = cdaitx and MPXCUS = C6CANB AND CRDUSR=" & sTirnakEkle(KullaniciAdi))

            'If sSipKapi <> "" Then
            '    MessageBox.Show("Sipariþlerde  " & sSipKapi & " nolu malzeme için farklý kapýya sipariþler var,iþlem yapamazsýnýz...", "Ekip Mapics", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
            '    Exit Sub
            'End If

            If MessageBox.Show("Dikkat!..Tüm sipariþler Syteline a iþlenecektir? Onaylýyor musunuz?...", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                Durum.Text = "Syteline Kayýtlarý Ýþleniyor...Lütfen Bekleyiniz..."

                StatusStrip1.Refresh()

                sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(sSQL)

                System.Threading.Thread.Sleep(300)

                sSQL = " SELECT M.SENDER,M.MSGNUM,M.MSGDTM,M.DUNSID," & _
                            " M.PLANTID,M.GATEID,M.CONTNO,M.NETDTE,M.MPXITM,M.MPXCUS,M.MPXCUSSEQ," & _
                            " M.MPXWHS,M.MPXPRC,M.MPXCUR,M.MPXAMB,M.MPXNAKL,M.MPXPLNM," & _
                            " M.MPXKADT,M.MSGSTAT,M.PUSNO, M.L3PREF," & _
                            " P.YUVPRM,SUM(M.NETQTY) AS TOPNETQTY,CUSITM, Vade" & _
                        " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & " M " & _
                        " LEFT OUTER JOIN " & SetTableName(My.Settings.LibNameEDI, "PLANTPRM") & " P " & _
                            " ON M.MPXCUS=P.CANB AND M.PLANTID=P.B9CD " & _
                        " WHERE M.NETDTE<>0 AND M.CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                        " AND (M.HATADRM=" & sTirnakEkle("0")

                If chkMesajDurumu1.Checked Then
                    sSQL = sSQL & " OR M.HATADRM=" & sTirnakEkle("1")
                End If
                If chkMesajDurumu2.Checked Then
                    sSQL = sSQL & " OR M.HATADRM=" & sTirnakEkle("2")
                End If
                sSQL = sSQL & " ) " & " GROUP BY M.SENDER,M.MSGNUM,M.MSGDTM,M.DUNSID,M.PLANTID,M.GATEID,M.CONTNO,M.NETDTE," & _
                            " M.MPXITM,M.MPXCUS,M.MPXCUSSEQ,M.MPXWHS,M.MPXPRC,M.MPXCUR,M.MPXAMB,M.MPXNAKL,M.MPXPLNM,M.MPXKADT,M.MSGSTAT,M.PUSNO,M.L3PREF,P.YUVPRM,CUSITM, Vade"

                dtEdiMst = db.RunSql(sSQL)

                Bar.Value = 0

                If Not dtEdiMst Is Nothing AndAlso dtEdiMst.Rows.Count > 0 Then

                    Bar.Maximum = dtEdiMst.Rows.Count

                End If

                If Not dtEdiMst Is Nothing AndAlso dtEdiMst.Rows.Count > 0 Then

                    For Each rowEdiMst As DataRow In dtEdiMst.Rows
                        ' Ambalaj Yuvarlama Parametresi 1 ise Ambalaj Miktarýna Yuvarlama Yapýlýr.

                        If IIf(rowEdiMst("YUVPRM").ToString = "" Or rowEdiMst("YUVPRM").ToString = 0, 0, 1) = 1 Then

                            If rowEdiMst("MPXKADT").ToString <> 0 Then

                                nTopNetQty = rowEdiMst("TOPNETQTY").ToString / rowEdiMst("MPXKADT").ToString

                                If nTopNetQty - Math.Truncate(nTopNetQty) <> 0.0 Then

                                    nTopNetQty = Math.Truncate(nTopNetQty) + 1

                                Else

                                    nTopNetQty = Math.Truncate(nTopNetQty)
                                End If

                                nTopNetQty = nTopNetQty * rowEdiMst("MPXKADT").ToString
                            Else

                                nTopNetQty = rowEdiMst("TOPNETQTY").ToString

                            End If
                        Else

                            nTopNetQty = rowEdiMst("TOPNETQTY").ToString
                        End If

                        sSQL = " INSERT INTO " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                            " ( SENDER,MSGNUM,MSGDTM,DUNSID,PLANTID,GATEID," & _
                            " CONTNO,NETDTE,MPXITM,MPXCUS,MPXCUSSEQ,MPXWHS,MPXPRC,MPXCUR," & _
                            " MPXAMB,MPXKADT,MPXNAKL,MPXPLNM,NETQTY,TNETQTY,ISLDRM," & _
                            " MSGSTAT,PUSNO,L3PREF,CRDDTE,CRDTIM,CRDUSR,CUSITM, VADE ) " & _
                            " VALUES ( " & _
                            sTirnakEkle(rowEdiMst("SENDER").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MSGNUM").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MSGDTM").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("DUNSID").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("PLANTID").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("GATEID").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("CONTNO").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("NETDTE").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MPXITM").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MPXCUS").ToString) & "," & _
                            rowEdiMst("MPXCUSSEQ").ToString & "," & _
                            sTirnakEkle(rowEdiMst("MPXWHS").ToString) & "," & _
                            Convert.ToString(rowEdiMst("MPXPRC").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MPXCUR").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MPXAMB").ToString) & "," & _
                            Convert.ToString(rowEdiMst("MPXKADT").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MPXNAKL").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("MPXPLNM").ToString) & "," & _
                            Convert.ToString(rowEdiMst("TOPNETQTY").ToString) & "," & _
                            Convert.ToString(nTopNetQty) & "," & _
                            sTirnakEkle("0") & "," & _
                            sTirnakEkle(rowEdiMst("MSGSTAT").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("PUSNO").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("L3PREF").ToString) & ", " & _
                            sTirnakEkle(DateTime.Today.ToString("yyyyMMdd")) & "," & _
                            sTirnakEkle(Now.ToString("HHmmss")) & "," & _
                            sTirnakEkle(KullaniciAdi) & "," & _
                            sTirnakEkle(rowEdiMst("CUSITM").ToString) & "," & _
                            sTirnakEkle(rowEdiMst("Vade").ToString) & " ) "

                        db.RunSql(sSQL)

                        Bar.Value = Bar.Value + 1

                        Application.DoEvents()

                    Next
                End If

                Bar.Value = 0

                'Ediprc Update edilecek Pusno yok ise

                sSQL = " Exec TR_Edi_Pusno_Ekleme @User=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(sSQL)

                'For nsip = 1 To 3

                '    sSQL = " SELECT DISTINCT MPXCUS,PLANTID,GATEID " & _
                '    " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & " S " & _
                '    " LEFT OUTER JOIN " & SetTableName(My.Settings.LibNameEDI, "PLANTPRM") & " P " & _
                '    " ON S.MPXCUS=P.CANB AND S.PLANTID=P.B9CD " & _
                '    " WHERE P.SFRSIP=1 AND S.CRDUSR=" & sTirnakEkle(KullaniciAdi)
                '    If nsip = 1 Then
                '        sSQL = sSQL & " AND S.MSGSTAT=" & sTirnakEkle("H")
                '    ElseIf nsip = 2 Then
                '        sSQL = sSQL & " AND S.MSGSTAT=" & sTirnakEkle("G")
                '    ElseIf nsip = 3 Then
                '        sSQL = sSQL & " AND S.MSGSTAT=" & sTirnakEkle("P")
                '    End If

                '    dtEdiPrc = db.RunSql(sSQL)

                '    For Each rowEdiPrc As DataRow In dtEdiPrc.Rows

                '        sSQL = " SELECT H.C6CVNB,H.C6CANB,H.C6B9CD,H.C6F1CD," & _
                '                     "H.C6A3CD,H.C6CDTX,D.ADAITX,D.ADFCNB,D.ADAQQT,D.ADBJDT " & _
                '                " FROM " & SetTableName(My.Settings.LibNameEDI, "MBC6REP") & " H " & _
                '                " LEFT OUTER JOIN " & SetTableName(My.Settings.LibNameEDI, "MBADREP") & " D " & _
                '                    " ON H.C6CVNB=D.ADCVNB " & _
                '                " WHERE H.C6CANB=" & sTirnakEkle(Convert.ToString(rowEdiPrc("MPXCUS").ToString)) & _
                '                " AND H.C6B9CD=" & sTirnakEkle(rowEdiPrc("PLANTID").ToString) & _
                '                " AND H.C6F1CD=" & sTirnakEkle(rowEdiPrc("GATEID").ToString) & _
                '                " AND (H.CJCBTX like " & sTirnakEkle("FO%") & _
                '                " Or H.CJCBTX like " & sTirnakEkle("EO%") & _
                '                " )"

                '        dtTemp = db.RunSql(sSQL)

                For nsip = 1 To 3
                    sSQL =
                    " SELECT DISTINCT MPXCUS,PLANTID,GATEID, Min(NETDTE) As NETDTE ,  Max(NETDTE) As MaxNETDTE" & _
                    " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & " S " & _
                    " LEFT OUTER JOIN " & SetTableName(My.Settings.LibNameEDI, "PLANTPRM") & " P " & _
                    " ON S.MPXCUS=P.CANB AND S.PLANTID=P.B9CD " & _
                    " WHERE P.SFRSIP=1 AND S.CRDUSR=" & sTirnakEkle(KullaniciAdi)

                    If nsip = 1 Then
                        sSQL = sSQL &
                       " AND S.MSGSTAT=" & sTirnakEkle("H")
                    ElseIf nsip = 2 Then
                        sSQL = sSQL &
                        " AND S.MSGSTAT=" & sTirnakEkle("G")
                    ElseIf nsip = 3 Then
                        sSQL = sSQL &
                        " AND S.MSGSTAT=" & sTirnakEkle("P")

                    End If

                    sSQL = sSQL &
                    " Group By MPXCUS,PLANTID,GATEID"

                    dtEdiPrc = db.RunSql(sSQL)

                    For Each rowEdiPrc As DataRow In dtEdiPrc.Rows

                        sSQL =
                        " SELECT H.C6CVNB,H.C6CANB,H.C6B9CD,H.C6F1CD," & _
                        "H.C6A3CD,H.C6CDTX,D.ADAITX,D.ADFCNB,D.ADAQQT,D.ADBJDT " & _
                        " FROM " & SetTableName(My.Settings.LibNameEDI, "MBC6REP") & " H " & _
                        " LEFT OUTER JOIN " & SetTableName(My.Settings.LibNameEDI, "MBADREP") & " D " & _
                        " ON H.C6CVNB=D.ADCVNB " & _
                        " WHERE H.C6CANB=" & sTirnakEkle(Convert.ToString(rowEdiPrc("MPXCUS").ToString)) & _
                        " AND H.C6B9CD=" & sTirnakEkle(rowEdiPrc("PLANTID").ToString) & _
                        " AND H.C6F1CD= Case When H.C6CANB IN ('  12003','  12020') Then H.C6F1CD Else " & sTirnakEkle(rowEdiPrc("GATEID").ToString) & " End" & _
                        " AND D.ADBJDT>=" & sTirnakEkle(rowEdiPrc("NETDTE").ToString) & _
                        " AND D.ADBJDT<=" & sTirnakEkle(rowEdiPrc("MaxNETDTE").ToString) & _
                        " AND (H.CJCBTX like " & sTirnakEkle("FO%") & _
                        " Or H.CJCBTX like " & sTirnakEkle("EO%") & _
                        " )"

                        dtTemp = db.RunSql(sSQL)

                        For Each rowTemp As DataRow In dtTemp.Rows

                            If nLookup("COUNT(*)", _
                                       SetTableName(My.Settings.LibNameEDI, "EDIPRC"), _
                                       " MPXCUS=" & sTirnakEkle(Convert.ToString(rowTemp("C6CANB").ToString)) & _
                                       " AND PLANTID=" & sTirnakEkle(rowTemp("C6B9CD").ToString) & _
                                       " AND GATEID=" & sTirnakEkle(rowTemp("C6F1CD").ToString) & _
                                       " AND MPXITM=" & sTirnakEkle(rowTemp("ADAITX").ToString) & _
                                       " And CRDUSR=" & sTirnakEkle(KullaniciAdi)) = 0 Then

                                ComSiparisUpdate("D", rowTemp("C6CVNB").ToString, rowTemp("C6B9CD").ToString, rowTemp("C6F1CD").ToString, rowTemp("ADAITX").ToString, rowTemp("C6A3CD").ToString, _
                                 rowTemp("C6CDTX").ToString, "", rowTemp("C6CANB").ToString, 0, rowTemp("ADBJDT").ToString, 0, _
                                 rowTemp("ADFCNB").ToString, "", "", 0, "", "")

                            End If

                        Next

                    Next

                    sSQL = " SELECT DISTINCT MPXCUS,PLANTID,GATEID,MPXITM,PUSNO,L3PREF " & _
                            " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                            " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi)

                    If nsip = 1 Then
                        sSQL = sSQL & " AND MSGSTAT=" & sTirnakEkle("H")
                    ElseIf nsip = 2 Then
                        sSQL = sSQL & " AND MSGSTAT=" & sTirnakEkle("G")
                    ElseIf nsip = 3 Then
                        sSQL = sSQL & " AND MSGSTAT=" & sTirnakEkle("P")
                    End If

                    dtEdiPrc = db.RunSql(sSQL)

                    If Not dtEdiPrc Is Nothing AndAlso dtEdiPrc.Rows.Count > 0 Then

                        If nsip = 1 Then
                            sMsgGunPUS = "H"
                        ElseIf nsip = 2 Then
                            sMsgGunPUS = "G"
                        ElseIf nsip = 3 Then
                            sMsgGunPUS = "P"
                        End If

                        If (sMsgGunPUS = "P") Then
                            nMaxMsgTar = nLookup("MAX(MSGDTM)", SetTableName(My.Settings.LibNameEDI, "EDIPRC"), _
                                                 " MPXCUS=" & sTirnakEkle(dtEdiPrc.Rows(0)("MPXCUS").ToString) & _
                                                 " AND PLANTID=" & sTirnakEkle(dtEdiPrc.Rows(0)("PLANTID").ToString) & _
                                                 " AND MSGSTAT=" & sTirnakEkle("P"))
                        ElseIf (sMsgGunPUS = "G") Then
                            nMaxMsgTar = nLookup("MAX(NETDTE)", SetTableName(My.Settings.LibNameEDI, "EDIMST"), _
                                                 " MPXCUS=" & sTirnakEkle(dtEdiPrc.Rows(0)("MPXCUS").ToString) & _
                                                 " AND PLANTID=" & sTirnakEkle(dtEdiPrc.Rows(0)("PLANTID").ToString) & _
                                                 " AND MSGSTAT=" & sTirnakEkle("G"))
                        End If

                        If (sMsgGunPUS = "P") AndAlso (nMaxMsgTar <> 0) Then

                            nhafta = WeekOfTheYear(DateConvert(Convert.ToString(nMaxMsgTar)))

                            nyil = CInt(Copy(Convert.ToString(nMaxMsgTar), 0, 4))

                            dtHaftaTar.Value = EncodeDateWeek(nyil, nhafta, 6)

                            nMaxMsgTar = dtHaftaTar.Value.ToString("yyyyMMdd")

                        End If

                        For Each rowEdiPrc As DataRow In dtEdiPrc.Rows

                            'sPUSNO = sLookup("PUSNO", SetTableName(My.Settings.LibNameEDI, "EDIPRC"), _
                            '         " MPXCUS=" & sTirnakEkle(rowEdiPrc("MPXCUS").ToString) & _
                            '         " AND PLANTID=" & sTirnakEkle(rowEdiPrc("PLANTID").ToString) & _
                            '         " AND GATEID=" & sTirnakEkle(rowEdiPrc("GATEID").ToString) & _
                            '         " AND MPXITM=" & sTirnakEkle(rowEdiPrc("MPXITM").ToString) & _
                            '         " AND MSGSTAT <>" & sTirnakEkle("H"))

                            'sL3PREF = sLookup("L3PREF", SetTableName(My.Settings.LibNameEDI, "EDIPRC"), _
                            '         " MPXCUS=" & sTirnakEkle(rowEdiPrc("MPXCUS").ToString) & _
                            '         " AND PLANTID=" & sTirnakEkle(rowEdiPrc("PLANTID").ToString) & _
                            '         " AND GATEID=" & sTirnakEkle(rowEdiPrc("GATEID").ToString) & _
                            '         " AND MPXITM=" & sTirnakEkle(rowEdiPrc("MPXITM").ToString) & _
                            '         " AND MSGSTAT <>" & sTirnakEkle("H"))

                            sPUSNO = rowEdiPrc("PUSNO").ToString
                            sL3PREF = rowEdiPrc("L3PREF").ToString

                            sPUS = ""

                            sSQL = ""

                            sSQL = "SELECT H.C6CVNB,H.C6CANB,H.C6B9CD,H.C6F1CD,H.C6A3CD," & _
                                        " H.C6CDTX,D.ADAITX,D.ADFCNB,D.ADAQQT,D.ADBJDT,D.L3PRef , CJCBTX" & _
                                        " FROM " & SetTableName(My.Settings.LibNameEDI, "MBC6REP") & " H " & _
                                        " LEFT OUTER JOIN " & SetTableName(My.Settings.LibNameEDI, "MBADREP") & " D " & _
                                            " ON H.C6CVNB=D.ADCVNB " & _
                                        " WHERE C6CANB=" & sTirnakEkle(rowEdiPrc("MPXCUS").ToString) & _
                                        " AND C6B9CD=" & sTirnakEkle(rowEdiPrc("PLANTID").ToString) & _
                                        " AND C6F1CD=" & sTirnakEkle(rowEdiPrc("GATEID").ToString) & _
                                        " AND ADAITX=" & sTirnakEkle(rowEdiPrc("MPXITM").ToString) & _
                                        " And ADAQQT>0 " & _
                                        " And (L3PRef=" & sTirnakEkle(sL3PREF) & " Or L3PREF='' Or L3PREF is null )" & _
                                        " AND (CJCBTX like 'FO%' Or CJCBTX='' Or CJCBTX is null Or CJCBTX like 'EO%' Or CJCBTX=" & sTirnakEkle(sPUSNO) & ") "

                            If (sMsgGunPUS = "P") OrElse (sMsgGunPUS = "G") Then
                                sSQL = sSQL & " AND ADBJDT<=" & sTirnakEkle(ConvertDate(nMaxMsgTar).ToString("yyyy-MM-dd"))
                            End If

                            dtTemp = db.RunSql(sSQL)

                            Bar.Value = 0

                            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                Bar.Maximum = dtTemp.Rows.Count
                            End If

                            For Each rowTemp As DataRow In dtTemp.Rows

                                Dim SipPusNo, SipL3PRef As String

                                SipPusNo = IIf(rowTemp("CJCBTX").ToString.StartsWith("FO") Or _
                                            rowTemp("CJCBTX").ToString.StartsWith("EO"), _
                                            "", _
                                            rowTemp("CJCBTX").ToString)

                                SipL3PRef = rowTemp("L3PREF").ToString

                                sSQL = " SELECT ISLDRM,TNETQTY,PUSNO,L3PREF  " & _
                                            " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                                            " WHERE MPXCUS=" & sTirnakEkle(rowTemp("C6CANB").ToString) & _
                                            " AND PLANTID=" & sTirnakEkle(rowTemp("C6B9CD").ToString) & _
                                            " AND GATEID=" & sTirnakEkle(rowTemp("C6F1CD").ToString) & _
                                            " AND MPXITM=" & sTirnakEkle(rowTemp("ADAITX").ToString) & _
                                            " AND NETDTE=" & CDate(rowTemp("ADBJDT").ToString).ToString("yyyyMMdd") & _
                                            " AND Pusno=" & sTirnakEkle(sPUSNO) & _
                                            " AND L3PREF=" & sTirnakEkle(sL3PREF) & _
                                            " AND MSGSTAT=" & sTirnakEkle(sMsgGunPUS) & _
                                            " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                                dtTemp1 = db.RunSql(sSQL)

                                nNetMiktar = 0

                                sIslDrm = "0"

                                If Not dtTemp1 Is Nothing AndAlso dtTemp1.Rows.Count > 0 Then

                                    sPUS = dtTemp1.Rows(0)("PUSNO").ToString
                                    'sPUS<> ""
                                    'If sPUS = "" Or Copy(SipPusNo, 1, 2) = "EO" Or Copy(SipPusNo, 1, 2) = "FO" Or SipPusNo = "" Then
                                    If sPUS <> SipPusNo Then

                                        nKayit = 0

                                    Else

                                        nKayit = 1

                                    End If

                                    sIslDrm = dtTemp1.Rows(0)("ISLDRM").ToString

                                    nNetMiktar = dtTemp1.Rows(0)("TNETQTY").ToString

                                    sL3PREF = dtTemp1.Rows(0)("L3PREF").ToString

                                Else

                                    nKayit = 0

                                End If

                                ' COM Kayýtlarýnda olup, gelen sipariþlerde olan kayýtlar için miktar deðiþikliði var ise update iþlemi yapýlýr.

                                If (nKayit <> 0) AndAlso (sIslDrm <> "1") Then
                                    ' Com kayýtlarýna Miktar=nNetMiktar olarak update edilir.

                                    If (sMsgGunPUS = "H") Then

                                        If (rowTemp("ADAQQT").ToString <> nNetMiktar) Then

                                            ComSiparisUpdate("U", rowTemp("C6CVNB").ToString, rowTemp("C6B9CD").ToString, rowTemp("C6F1CD").ToString, rowTemp("ADAITX").ToString, rowTemp("C6A3CD").ToString, _
                                             rowTemp("C6CDTX").ToString, "", rowTemp("C6CANB").ToString, 0, rowTemp("ADBJDT").ToString, nNetMiktar, _
                                             rowTemp("ADFCNB").ToString, "", "", 0, "", "")

                                        End If

                                        sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                                                " SET ISLDRM=" & sTirnakEkle("1") & _
                                                     " WHERE MPXCUS=" & sTirnakEkle(rowTemp("C6CANB").ToString) & _
                                                     " AND PLANTID=" & sTirnakEkle(rowTemp("C6B9CD").ToString) & _
                                                     " AND GATEID=" & sTirnakEkle(rowTemp("C6F1CD").ToString) & _
                                                     " AND MPXITM=" & sTirnakEkle(rowTemp("ADAITX").ToString) & _
                                                     " AND NETDTE=" & CDate(rowTemp("ADBJDT").ToString).ToString("yyyyMMdd") & _
                                                     " AND PUSNO=" & sTirnakEkle("") & _
                                                     " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                                        db.RunSql(sSQL)

                                    ElseIf (sMsgGunPUS = "G") Then

                                        If (rowTemp("ADAQQT").ToString <> nNetMiktar) Then

                                            ComSiparisUpdate("U", rowTemp("C6CVNB").ToString, rowTemp("C6B9CD").ToString, rowTemp("C6F1CD").ToString, rowTemp("ADAITX").ToString, rowTemp("C6A3CD").ToString, _
                                             rowTemp("C6CDTX").ToString, sPUS, rowTemp("C6CANB").ToString, 0, rowTemp("ADBJDT").ToString, nNetMiktar, _
                                             rowTemp("ADFCNB").ToString, sL3PREF, "", 0, "", "")

                                        End If

                                        sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                                             " SET ISLDRM=" & sTirnakEkle("1") & _
                                                  " WHERE MPXCUS=" & sTirnakEkle(rowTemp("C6CANB").ToString) & _
                                                  " AND PLANTID=" & sTirnakEkle(rowTemp("C6B9CD").ToString) & _
                                                  " AND GATEID=" & sTirnakEkle(rowTemp("C6F1CD").ToString) & _
                                                  " AND MPXITM=" & sTirnakEkle(rowTemp("ADAITX").ToString) & _
                                                  " AND NETDTE=" & CDate(rowTemp("ADBJDT").ToString).ToString("yyyyMMdd") & _
                                                  " AND PUSNO=" & sTirnakEkle(sPUS) & _
                                                  " AND L3PREF=" & sTirnakEkle(sL3PREF) & _
                                                  " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                                        db.RunSql(sSQL)

                                    ElseIf (sMsgGunPUS = "P") Then

                                        If (rowTemp("ADAQQT").ToString <> nNetMiktar) Then

                                            ComSiparisUpdate("U", rowTemp("C6CVNB").ToString, rowTemp("C6B9CD").ToString, rowTemp("C6F1CD").ToString, rowTemp("ADAITX").ToString, rowTemp("C6A3CD").ToString, _
                                             rowTemp("C6CDTX").ToString, sPUSNO, rowTemp("C6CANB").ToString, 0, rowTemp("ADBJDT").ToString, nNetMiktar, _
                                             rowTemp("ADFCNB").ToString, sL3PREF, "", 0, "", "")

                                        End If

                                        sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                                             " SET ISLDRM=" & sTirnakEkle("1") & _
                                                  " WHERE MPXCUS=" & sTirnakEkle(rowTemp("C6CANB").ToString) & _
                                                  " AND PLANTID=" & sTirnakEkle(rowTemp("C6B9CD").ToString) & _
                                                  " AND GATEID=" & sTirnakEkle(rowTemp("C6F1CD").ToString) & _
                                                  " AND MPXITM=" & sTirnakEkle(rowTemp("ADAITX").ToString) & _
                                                  " AND NETDTE=" & CDate(rowTemp("ADBJDT").ToString).ToString("yyyyMMdd") & _
                                                  " AND PUSNO=" & sTirnakEkle(sPUSNO) & _
                                                  " AND L3PREF=" & sTirnakEkle(sL3PREF) & _
                                                  " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                                        db.RunSql(sSQL)

                                    End If

                                ElseIf (nKayit <> 0) AndAlso (sIslDrm = "1") Then
                                    ' Com kayýtlarýna Miktar=0 olarak update edilir.

                                    ' COM kayýtlarýnda olup, gelen sipariþlerde olmayan sipariþler için
                                    ComSiparisUpdate("U", rowTemp("C6CVNB").ToString, rowTemp("C6B9CD").ToString, rowTemp("C6F1CD").ToString, rowTemp("ADAITX").ToString, rowTemp("C6A3CD").ToString, _
                                     rowTemp("C6CDTX").ToString, sPUSNO, rowTemp("C6CANB").ToString, 0, rowTemp("ADBJDT").ToString, 0, _
                                     rowTemp("ADFCNB").ToString, sL3PREF, "", 0, "", "")

                                ElseIf nKayit = 0 Then
                                    ' Günlük veya PUS mesajý ise Sýfýrlama parametresi gördermez gibi iþleme koyulur ve gelen mesajda
                                    ' olmayan COM sipariþleri silinir.
                                    If (sMsgGunPUS = "P") OrElse (sMsgGunPUS = "G") Then
                                        nPrmSifir = 0
                                    Else
                                        nPrmSifir = nLookup("SFRPRM", SetTableName(My.Settings.LibNameEDI, "PLANTPRM"), " CANB=" & sTirnakEkle(rowTemp("C6CANB").ToString) & " AND B9CD=" & sTirnakEkle(rowTemp("C6B9CD").ToString))
                                    End If

                                    If (nPrmSifir = 1) OrElse (rowTemp("ADAQQT").ToString = 0) Then
                                        ' Ýþlem yapýlmadan bir sonraki kayda gider.
                                    ElseIf (nPrmSifir = 0) Then
                                        ' Com kayýtlarýna Miktar=0 olarak update edilir.
                                        'Sipariþ kayýtlarý silinir 09/07/2009 Zeki

                                        ComSiparisUpdate("D", rowTemp("C6CVNB").ToString, rowTemp("C6B9CD").ToString, rowTemp("C6F1CD").ToString, _
                                        rowTemp("ADAITX").ToString, rowTemp("C6A3CD").ToString, _
                                        rowTemp("C6CDTX").ToString, sPUSNO, rowTemp("C6CANB").ToString, 0, rowTemp("ADBJDT").ToString, 0, _
                                        rowTemp("ADFCNB").ToString, sL3PREF, "", 0, "", "")

                                    End If

                                End If

                                Bar.Value = Bar.Value + 1

                                Application.DoEvents()

                            Next

                        Next

                    End If

                Next

                ' EDIPRC de Statu=1 olan kayýtlar yeni sipariþlerdi, COM kayýtlarýna insert edilir.

                sSQL = " SELECT * FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & _
                            " WHERE ISLDRM<>" & sTirnakEkle("1") & _
                            " AND CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                            " ORDER BY MPXCUS,MPXCUSSEQ,GATEID,NETDTE,PUSNO,L3PREF, Vade"

                dtEdiPrc = db.RunSql(sSQL)

                Bar.Value = 0

                If Not dtEdiPrc Is Nothing AndAlso dtEdiPrc.Rows.Count > 0 Then

                    Bar.Maximum = dtEdiPrc.Rows.Count

                End If

                For Each rowEdiPrc As DataRow In dtEdiPrc.Rows

                    ComSiparisUpdate("I", "", rowEdiPrc("PLANTID").ToString, rowEdiPrc("GATEID").ToString, rowEdiPrc("MPXITM").ToString, rowEdiPrc("MPXWHS").ToString, _
                    rowEdiPrc("MPXNAKL").ToString, rowEdiPrc("PUSNO").ToString, rowEdiPrc("MPXCUS").ToString, rowEdiPrc("MPXCUSSEQ").ToString, _
                    ConvertDate(rowEdiPrc("NETDTE").ToString), rowEdiPrc("TNETQTY").ToString, _
                    0, rowEdiPrc("L3PREF").ToString, rowEdiPrc("CUSITM").ToString, rowEdiPrc("MPXPRC").ToString, rowEdiPrc("MPXNAKL").ToString, rowEdiPrc("Vade").ToString)

                Next

                Durum.Text = "   "

                Bar.Value = 0

                'sSQL = " INSERT INTO " & SetTableName(My.Settings.LibNameEDI, "EDIBCK") & _
                '    " (SENDER,RECEIVER,MSGNUM,MSGDTM,MSGTX1,MSGTX2,MSGTX3,DUNSID, " & _
                '        " PLANTID,PUSNO,CUSITM,GATEID,CONTNO,CUMQTY,CUMREF,CUMDTE,DELTYP, " & _
                '        " DELQTY,DELDTE,NETQTY,NETDTE,RECCNT,MPXITM,MPXCUM,MPXCMT, " & _
                '        " MPXCUS,MPXWHS,MPXPRC,MPXCUR,MPXAMB,MPXKADT,MPXNAKL,MPXPLNM,  " & _
                '        " HATADRM,MSGSTAT,CRDDTE,CRDTIM,CRDUSR,MISSUER,MATHNDCD,KANBANNO )  " & _
                '         " SELECT  " & " SENDER,RECEIVER,MSGNUM,MSGDTM,MSGTX1,MSGTX2,MSGTX3,DUNSID, " & _
                '            " PLANTID,PUSNO,CUSITM,GATEID,CONTNO,CUMQTY,CUMREF,CUMDTE,DELTYP, " & _
                '            " DELQTY,DELDTE,NETQTY,NETDTE,RECCNT,MPXITM,MPXCUM,MPXCMT, " & _
                '            " MPXCUS,MPXWHS,MPXPRC,MPXCUR,MPXAMB,MPXKADT,MPXNAKL,MPXPLNM, " & _
                '            " HATADRM,MSGSTAT,CRDDTE,CRDTIM,CRDUSR,MISSUER,MATHNDCD,KANBANNO " & _
                '        " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                '        " WHERE NETDTE<>0 AND CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                '        " AND (HATADRM=" & sTirnakEkle("0")

                'If chkMesajDurumu1.Checked Then
                '    sSQL = sSQL & " OR HATADRM=" & sTirnakEkle("1")
                'End If
                'If chkMesajDurumu2.Checked Then
                '    sSQL = sSQL & " OR HATADRM=" & sTirnakEkle("2")
                'End If

                'sSQL = sSQL & " ) "

                'db.RunSql(sSQL)

                ' EDIBCK execute edilmiyordu. Onceden inaktifti, aktiflestirdim. 19/10/2004  KADER

                System.Threading.Thread.Sleep(150)

                sSQL = " SELECT DISTINCT PLANTID,GATEID,MPXITM,MPXCUS,isnull(MATHNDCD,'') As MATHNDCD ,isnull(KANBANNO,'') As KANBANNO, isnull(USERF1,'') As USERF1" & _
                            " FROM " & SetTableName(My.Settings.LibNameEDI, "KONTRTSIP")
                ' + ' WHERE (pusno is null or pusno = '''' or pusno = '' '') ';  L3P için kaldýrýldý. Zeki 02.11.2007

                dtTemp = db.RunSql(sSQL)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowtemp As DataRow In dtTemp.Rows

                        If rowtemp("MPXCUS").ToString = "1010051" Then

                            '" SET " & " USER2 = '" & rowtemp("USERF1").ToString & "' " & _
                            'sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "KONTRTPF") & _
                            '                " SET " & " USER2 = '" & rowtemp("MATHNDCD").ToString & "' " & _
                            '                " WHERE SRKT=" & sTirnakEkle(My.Settings.Company) & _
                            '                " AND CUST=" & sTirnakEkle(Convert.ToString(rowtemp("MPXCUS").ToString)) & " " & _
                            '                " AND SHIPTO=" & sTirnakEkle(rowtemp("PLANTID").ToString) & _
                            '                " AND KAPI=" & sTirnakEkle(rowtemp("GATEID").ToString) & " " & _
                            '                " AND BZMITM=" & sTirnakEkle(rowtemp("MPXITM").ToString)
                            sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "KONTRTPF") & _
                                           " SET " & " USER2 = '" & rowtemp("MATHNDCD").ToString & "' " & _
                                           " WHERE " & _
                                           " CUST=" & sTirnakEkle(Convert.ToString(rowtemp("MPXCUS").ToString)) & " " & _
                                           " AND SHIPTO=" & sTirnakEkle(rowtemp("PLANTID").ToString) & _
                                           " AND KAPI=" & sTirnakEkle(rowtemp("GATEID").ToString) & " " & _
                                           " AND BZMITM=" & sTirnakEkle(rowtemp("MPXITM").ToString)
                            db.RunSql(sSQL)

                        Else

                            Dim sSet As String = ""

                            sSet &= IIf(rowtemp("KANBANNO").ToString = "", "", IIf(sSet.Length > 0, ",", "") & " KANBAN=" & sTirnakEkle(rowtemp("KANBANNO").ToString))
                            sSet &= IIf(rowtemp("MATHNDCD").ToString = "", "", IIf(sSet.Length > 0, ",", "") & " USER2=" & sTirnakEkle(rowtemp("MATHNDCD").ToString))
                            sSet &= IIf(rowtemp("USERF1").ToString = "", "", IIf(sSet.Length > 0, ",", "") & " USER1 =" & sTirnakEkle(rowtemp("USERF1").ToString))

                            If sSet.Length <> 0 Then

                                'sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "KONTRTPF") & _
                                '            " SET " & sSet & _
                                '    " WHERE SRKT=" & sTirnakEkle(My.Settings.Company) & _
                                '    " AND CUST=" & sTirnakEkle(Convert.ToString(rowtemp("MPXCUS").ToString)) & " " & _
                                '    " AND SHIPTO=" & sTirnakEkle(rowtemp("PLANTID").ToString) & _
                                '    " AND KAPI=" & sTirnakEkle(rowtemp("GATEID").ToString) & " " & _
                                '    " AND BZMITM=" & sTirnakEkle(rowtemp("MPXITM").ToString)
                                sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "KONTRTPF") & _
                                          " SET " & sSet & _
                                  " WHERE " & _
                                  " CUST=" & sTirnakEkle(Convert.ToString(rowtemp("MPXCUS").ToString)) & " " & _
                                  " AND SHIPTO=" & sTirnakEkle(rowtemp("PLANTID").ToString) & _
                                  " AND KAPI=" & sTirnakEkle(rowtemp("GATEID").ToString) & " " & _
                                  " AND BZMITM=" & sTirnakEkle(rowtemp("MPXITM").ToString)


                                db.RunSql(sSQL)

                            End If

                        End If

                    Next

                End If

                sSQL = " SELECT DISTINCT M.PLANTID,M.MPXCUS,M.MISSUER " & _
                            " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & " M " & _
                            " JOIN " & SetTableName(My.Settings.LibNameEDI, "PLANTPRM") & " P " & _
                                " ON P.CANB=M.MPXCUS AND P.B9CD=M.PLANTID " & _
                                " AND P.ISSUER<>M.MISSUER " & _
                            " WHERE M.CRDUSR=" & sTirnakEkle(KullaniciAdi)

                dtTemp = db.RunSql(sSQL)

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowtemp As DataRow In dtTemp.Rows

                        sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "PLANTPRM") & _
                                    " SET ISSUER=" & sTirnakEkle(rowtemp("MISSUER").ToString) & _
                                    " WHERE CANB=" & sTirnakEkle(Convert.ToString(rowtemp("MPXCUS").ToString)) & _
                                    " AND B9CD=" & sTirnakEkle(rowtemp("PLANTID").ToString)

                        db.RunSql(sSQL)

                    Next

                End If

                sSQL = " SELECT DISTINCT SENDER,MSGNUM,PLANTID,GATEID,CUSITM,DELDTE " & _
                            "FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                            " WHERE NETDTE<>0 AND CRDUSR=" & sTirnakEkle(KullaniciAdi) & " AND (HATADRM=" & sTirnakEkle("0")

                If chkMesajDurumu1.Checked Then
                    sSQL = sSQL & " OR HATADRM=" & sTirnakEkle("1")
                End If
                If chkMesajDurumu2.Checked Then
                    sSQL = sSQL & " OR HATADRM=" & sTirnakEkle("2")
                End If

                sSQL = sSQL & " ) "

                dtEdiMst = db.RunSql(sSQL)

                If Not dtEdiMst Is Nothing AndAlso dtEdiMst.Rows.Count > 0 Then

                    Bar.Maximum = dtEdiMst.Rows.Count

                End If

                For Each rowEdiMst As DataRow In dtEdiMst.Rows

                    sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                                " WHERE SENDER=" & sTirnakEkle(rowEdiMst("SENDER").ToString) & _
                                " AND MSGNUM=" & sTirnakEkle(rowEdiMst("MSGNUM").ToString) & _
                                " AND PLANTID=" & sTirnakEkle(rowEdiMst("PLANTID").ToString) & _
                                " AND GATEID=" & sTirnakEkle(rowEdiMst("GATEID").ToString) & _
                                " AND CUSITM=" & sTirnakEkle(rowEdiMst("CUSITM").ToString) & _
                                " AND DELDTE=" & Convert.ToString(rowEdiMst("DELDTE").ToString) & _
                                " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                    db.RunSql(sSQL)

                    Bar.Value = Bar.Value + 1

                    Application.DoEvents()

                Next

                Bar.Value = 0

                sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, "EDIPRC") & " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(sSQL)

                sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & " WHERE NETDTE<>0 AND CRDUSR=" & sTirnakEkle(KullaniciAdi) & " AND HATADRM=" & sTirnakEkle("0")

                db.RunSql(sSQL)

                sSQL = " DELETE " & _
                       "FROM  edi_coitem" & _
                       " WHERE posted=1"

                db.RunSql(sSQL)

                sSQL = " DELETE " & _
                            "FROM  edi_co" & _
                            " WHERE posted=1"

                db.RunSql(sSQL)

                If chkMesajDurumu1.Checked Then

                    sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & " WHERE NETDTE<>0 AND CRDUSR=" & sTirnakEkle(KullaniciAdi) & " AND HATADRM=" & sTirnakEkle("1")

                    db.RunSql(sSQL)

                End If

                If chkMesajDurumu2.Checked Then

                    sSQL = " DELETE FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & " WHERE NETDTE<>0 AND CRDUSR=" & sTirnakEkle(KullaniciAdi) & " AND HATADRM=" & sTirnakEkle("2")

                    db.RunSql(sSQL)

                End If
                '        btnPuantajListeClick(Me)
                Beep()
                ' MessageDlg(dtmEkip.GetMessage('InformAktarim','Aktarým Tamamlandý...'),mtInformation,[mbOK],0);
                MessageBox.Show("Aktarým Tamamlandý...")

                Try

                    db.BeginTransaction()

                    sSQL = "delete  " & _
                                " from edi_coitem " & _
                                " where not exists (Select 1 " & _
                                                        " from edi_co " & _
                                                        " where edi_co.co_num = edi_coitem.co_num)"

                    db.RunSql(sSQL)

                    db.CommitTransaction()

                Catch ex As Exception

                    db.RollbackTransaction()

                    Throw ex

                End Try

                btnListele_Click(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btntarih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNetTarihGuncelle.Click
        Dim sSQL As String
        Dim sPlant As String
        Dim sGateId As String
        Dim sItem As String
        Dim sNetTarih As String = ""
        Dim nNetTar As Integer
        Dim nCust As String
        Dim nNetWeek As UShort
        Dim nCalWeek As UShort
        Dim dNetTar As Date = Nothing
        Dim nNetQty As Double
        Dim nfark As Double
        Dim nCalTar As Double
        Dim nCalTar1 As Double

        Try

            If MessageBox.Show("Dikkat!..Net Miktar ve Tarihler Hesaplanacak? Onaylýyor musunuz?...", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                btnSyteLinDatarlariAktar.Enabled = True

                Durum.Text = " Net Tarih ve Miktar Bilgileri Hesaplanýyor...Lütfen Bekleyiniz..."

                StatusStrip1.Refresh()

                Bar.Value = 0

                sSQL = " SELECT * " & _
                            " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                            " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi)

                dt = db.RunSql(sSQL)

                Bar.Value = 0

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Bar.Maximum = dt.Rows.Count
                End If

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each row As DataRow In dt.Rows
                        ' Plant Parametrelerinden ilgili customer ve plant e ait parametreler bulunur.
                        sSQL = ""
                        sSQL = " SELECT TARTIP,TTIME,CALID " & _
                                 " FROM " & SetTableName(My.Settings.LibNameEDI, "PLANTPRM") & _
                                 " WHERE CANB=" & sTirnakEkle(row("MPXCUS").ToString) & _
                                 " AND B9CD=" & sTirnakEkle(row("PLANTID").ToString)

                        dtTemp = db.RunSql(sSQL)

                        If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                            For Each rowTemp As DataRow In dtTemp.Rows

                                If rowTemp("TTIME").ToString = "" Then

                                    MessageBox.Show(row("MPXCUS").ToString & " nolu müþterinin  " & row("PLANTID").ToString & " plant i için yol süresi girilmemiþ.")

                                    Exit Sub

                                End If

                                If rowTemp("TARTIP").ToString = "Y" Then

                                    nNetTar = Convert.ToInt64(row("DELDTE").ToString)

                                    dtNetTar.Value = ConvertDate(nNetTar)

                                    If dtNetTar.Value <= DateTime.Today Then

                                        dtNetTar.Value = DateTime.Today
                                        ' Tarih Tipi Varýþ iseEDI de gelen tarihten Varýþ süresi çýkartýlýr.
                                    End If
                                Else
                                    ' if rowtemp('TARTIP').tostring='V' then
                                    nNetTar = Convert.ToInt64(row("DELDTE").ToString)

                                    dtNetTar.Value = ConvertDate(nNetTar)

                                    dtNetTar.Value = DateAdd(DateInterval.Day, (CInt(rowTemp("TTIME").ToString) * -1), dtNetTar.Value)
                                    ' Bulunan Net Tarih günün tarihinden küçükse Net Tarih günün tarihi olur.

                                    If dtNetTar.Value <= DateTime.Today Then

                                        dtNetTar.Value = DateTime.Today

                                    End If

                                End If

                                ' Paramterelerde ilgili customer, plant in sevkiyat günlerine göre Net Tarih hesaplanýr.
                                If row("MSGSTAT").ToString <> "P" Then

                                    nCalTar = 0

                                    sSQL = " SELECT MIN(CALDT) AS CALTAR " & _
                                                " FROM " & My.Settings.LibNameEDI & ".CALNDR" & _
                                                " WHERE CALN=" & sTirnakEkle(rowTemp("CALID").ToString) & _
                                                " AND CALDT>=" & DateConvertAs400(dtNetTar.Value)

                                    dtDate = db.RunSql(sSQL)

                                    If Not dtDate Is Nothing AndAlso dtDate.Rows.Count > 0 Then
                                        nCalTar = dtDate.Rows(0)("CALTAR").ToString
                                    End If

                                    nCalWeek = 0
                                    nNetWeek = 0

                                    If nCalTar <> 0 Then

                                        dtCalTar.Value = ConvertDateFromDb2(nCalTar)

                                        nCalWeek = WeekOfTheYear(dtCalTar.Value)

                                    End If

                                    nNetWeek = WeekOfTheYear(dtNetTar.Value)

                                    sNetTarih = "0"

                                    nCalTar1 = 0

                                    If nCalWeek = nNetWeek Then

                                        sNetTarih = dtCalTar.Value.ToString("yyyyMMdd")

                                    Else

                                        sSQL = " SELECT MAX(CALDT) AS CALTAR " & _
                                                    " FROM " & My.Settings.LibNameEDI & ".CALNDR" & _
                                                    " WHERE CALN=" & sTirnakEkle(rowTemp("CALID").ToString) & _
                                                    " AND CALDT<=" & DateConvertAs400(dtNetTar.Value) & _
                                                    " AND CALDT>=" & DateConvertAs400(DateTime.Today)

                                        dtDate = db.RunSql(sSQL)

                                        If Not dtDate Is Nothing AndAlso dtDate.Rows.Count > 0 Then

                                            nCalTar1 = dtDate.Rows(0)("CALTAR").ToString

                                        End If

                                    End If

                                    If nCalTar1 = 0 Then

                                        sNetTarih = dtCalTar.Value.ToString("yyyyMMdd")

                                    Else
                                        ' if nCalTar<>0 then
                                        dtCalTar.Value = ConvertDateFromDb2(nCalTar1)

                                        sNetTarih = dtCalTar.Value.ToString("yyyyMMdd")

                                    End If
                                Else

                                    sNetTarih = dtNetTar.Value.ToString("yyyyMMdd")

                                End If

                            Next

                            sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                                        " SET NETDTE=" & sNetTarih & _
                                        " WHERE SENDER=" & sTirnakEkle(row("SENDER").ToString) & _
                                        " AND MSGNUM=" & sTirnakEkle(row("MSGNUM").ToString) & _
                                        " AND PLANTID=" & sTirnakEkle(row("PLANTID").ToString) & _
                                        " AND CUSITM=" & sTirnakEkle(row("CUSITM").ToString) & _
                                        " AND GATEID=" & sTirnakEkle(row("GATEID").ToString) & _
                                        " AND DELDTE=" & Convert.ToString(row("DELDTE").ToString) & _
                                        " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                            db.RunSql(sSQL)

                            If sNetTarih = "0" Then

                                btnSyteLinDatarlariAktar.Enabled = False

                            End If

                        End If

                        Bar.Value = Bar.Value + 1

                        Application.DoEvents()

                    Next

                End If

                Bar.Value = 0

                sSQL = " SELECT SENDER,MSGNUM,PLANTID,MPXCUS," & _
                                " MPXITM,GATEID,DELDTE,NETDTE,MPXCUM," & _
                                " CUMQTY,CUMDTE,DELQTY,PUSNO, L3PREF   " & _
                        " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                        " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                        " And KUMLEVEL=0" & _
                        " ORDER BY MPXCUS,PLANTID,MPXITM,NETDTE,DELDTE "

                dtTemp = db.RunSql(sSQL)

                Bar.Value = 0
                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    Bar.Maximum = dtTemp.Rows.Count
                End If

                nCust = 0
                sPlant = ""
                sItem = ""
                nNetQty = 0
                nfark = 0

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowTemp As DataRow In dtTemp.Rows

                        If (nCust <> rowTemp("MPXCUS").ToString) OrElse _
                            (sPlant <> rowTemp("PLANTID").ToString) OrElse _
                            (sItem <> rowTemp("MPXITM").ToString) Then

                            If rowTemp("CUMDTE").ToString = 0 Then
                                ' KADER 11/03/2005 kaldýrýldý, 15/03/2005 salý gunu tekrar geri kondu.
                                nfark = 0

                            ElseIf CDbl(rowTemp("MPXCUM").ToString) >= CDbl(rowTemp("CUMQTY").ToString) Then

                                nfark = CDbl(rowTemp("MPXCUM").ToString) - CDbl(rowTemp("CUMQTY").ToString)

                            Else

                                nfark = 0

                            End If

                            nCust = rowTemp("MPXCUS").ToString

                            sPlant = rowTemp("PLANTID").ToString

                            sItem = rowTemp("MPXITM").ToString

                        End If

                        If nfark > CDbl(rowTemp("DELQTY").ToString) Then

                            nNetQty = 0

                            nfark = nfark - CDbl(rowTemp("DELQTY").ToString)

                        ElseIf nfark < rowTemp("DELQTY").ToString Then

                            nNetQty = CDbl(rowTemp("DELQTY").ToString) - nfark

                            nfark = 0

                        ElseIf nfark = CDbl(rowTemp("DELQTY").ToString) Then

                            nNetQty = 0

                            nfark = 0

                        End If

                        sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                                " SET NETQTY=" & Convert.ToString(nNetQty) & _
                                " WHERE SENDER=" & sTirnakEkle(rowTemp("SENDER").ToString) & _
                                " AND MSGNUM=" & sTirnakEkle(rowTemp("MSGNUM").ToString) & _
                                " AND PLANTID=" & sTirnakEkle(rowTemp("PLANTID").ToString) & _
                                " AND MPXITM=" & sTirnakEkle(rowTemp("MPXITM").ToString) & _
                                " AND GATEID=" & sTirnakEkle(rowTemp("GATEID").ToString) & _
                                " AND DELDTE=" & Convert.ToString(rowTemp("DELDTE").ToString) & _
                                " AND PUSNO = '" & rowTemp("PUSNO").ToString & "' " & _
                                " AND L3PREF = '" & rowTemp("L3PREF").ToString & "' " & _
                                " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                        db.RunSql(sSQL)

                        Bar.Value = Bar.Value + 1

                        Application.DoEvents()

                    Next

                End If

                'KUMLEVEL 1 olanlar için Kapý bazýnda Net miktarlar hesaplanacak...

                sSQL = " SELECT SENDER,MSGNUM,PLANTID,MPXCUS," & _
                                " MPXITM,GATEID,DELDTE,NETDTE,MPXCUM," & _
                                " CUMQTY,CUMDTE,DELQTY,PUSNO, L3PREF   " & _
                        " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                        " WHERE CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                        " And KUMLEVEL=1" & _
                        " ORDER BY MPXCUS,PLANTID,GATEID,MPXITM,NETDTE,DELDTE "

                dtTemp = db.RunSql(sSQL)

                Bar.Value = 0
                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    Bar.Maximum = dtTemp.Rows.Count
                End If

                nCust = 0
                sPlant = ""
                sItem = ""
                nNetQty = 0
                nfark = 0
                sGateId = ""

                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                    For Each rowTemp As DataRow In dtTemp.Rows

                        If (nCust <> rowTemp("MPXCUS").ToString) OrElse _
                            (sPlant <> rowTemp("PLANTID").ToString) OrElse _
                            (sItem <> rowTemp("MPXITM").ToString) OrElse _
                            (sGateId <> rowTemp("GATEID").ToString) Then

                            If rowTemp("CUMDTE").ToString = 0 Then
                                ' KADER 11/03/2005 kaldýrýldý, 15/03/2005 salý gunu tekrar geri kondu.
                                nfark = 0

                            ElseIf rowTemp("MPXCUM").ToString >= rowTemp("CUMQTY").ToString Then

                                nfark = rowTemp("MPXCUM").ToString - rowTemp("CUMQTY").ToString

                            Else

                                nfark = 0

                            End If

                            nCust = rowTemp("MPXCUS").ToString

                            sPlant = rowTemp("PLANTID").ToString

                            sItem = rowTemp("MPXITM").ToString

                        End If

                        If nfark > rowTemp("DELQTY").ToString Then

                            nNetQty = 0

                            nfark = nfark - rowTemp("DELQTY").ToString

                        ElseIf nfark < rowTemp("DELQTY").ToString Then

                            nNetQty = rowTemp("DELQTY").ToString - nfark

                            nfark = 0

                        ElseIf nfark = rowTemp("DELQTY").ToString Then

                            nNetQty = 0

                            nfark = 0

                        End If

                        sSQL = " UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIMST") & _
                                " SET NETQTY=" & Convert.ToString(nNetQty) & _
                                " WHERE SENDER=" & sTirnakEkle(rowTemp("SENDER").ToString) & _
                                " AND MSGNUM=" & sTirnakEkle(rowTemp("MSGNUM").ToString) & _
                                " AND PLANTID=" & sTirnakEkle(rowTemp("PLANTID").ToString) & _
                                " AND MPXITM=" & sTirnakEkle(rowTemp("MPXITM").ToString) & _
                                " AND GATEID=" & sTirnakEkle(rowTemp("GATEID").ToString) & _
                                " AND DELDTE=" & Convert.ToString(rowTemp("DELDTE").ToString) & _
                                " AND PUSNO = '" & rowTemp("PUSNO").ToString & "' " & _
                                " AND L3PREF = '" & rowTemp("L3PREF").ToString & "' " & _
                                " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                        db.RunSql(sSQL)

                        Bar.Value = Bar.Value + 1

                        Application.DoEvents()

                    Next

                End If

                Durum.Text = "   "

                Bar.Value = 0

                btnSyteLinDatarlariAktar.Enabled = True

                Beep()

                MessageBox.Show("Net Tarih ve Miktarlar Hesaplandý...", Application.ProductName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)

            End If

            btnListele_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub frSeDesAdvAsama2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Nobel_Site_AppDataSet.EDIPRM' table. You can move, or remove it, as needed.

        btnListele_Click(sender, e)
    End Sub

    Private Sub grdDesAdv_GroupsChanged(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.GroupsChangedEventArgs) Handles grdDesAdv.GroupsChanged
        grdDesAdv.CollapseGroups()

        If grdDesAdv.RowCount > 0 Then
            grdDesAdv.Row = 0
        End If
    End Sub

    Private Sub grdDesAdv_GroupsChanging(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.GroupsChangingEventArgs) Handles grdDesAdv.GroupsChanging
        grdDesAdv.CollapseGroups()

        If grdDesAdv.RowCount > 0 Then
            grdDesAdv.Row = 0
        End If
    End Sub

    Private Sub kumul()
    End Sub

    Private Sub SatýrýSilToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SatýrýSilToolStripMenuItem.Click
        Try

            Dim rr As Janus.Windows.GridEX.GridEXRow = grdDesAdv.GetRow

            sSQL = " Delete " & _
                      " From EdiMst" & _
                      " Where Sender= " & sTirnakEkle(rr.Cells("SENDER").Value) & _
                          " And MSGNUM= " & sTirnakEkle(rr.Cells("MSGNUM").Value) & _
                          " And PLANTID= " & sTirnakEkle(rr.Cells("PLANTID").Value) & _
                          " And CUSITM= " & sTirnakEkle(rr.Cells("CUSITM").Value) & _
                          " And GATEID= " & sTirnakEkle(rr.Cells("GATEID").Value) & _
                          " And DELDTE= " & sTirnakEkle(rr.Cells("DELDTE").Value) & _
                          " And PUSNO= " & sTirnakEkle(rr.Cells("PUSNO").Value) & _
                          " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

            db.RunSql(sSQL)

            sSQL = " Delete " & _
                    " From EdiWrk" & _
                    " Where Sender= " & sTirnakEkle(rr.Cells("SENDER").Value) & _
                        " And MSGNUM= " & sTirnakEkle(rr.Cells("MSGNUM").Value) & _
                        " And PLANTID= " & sTirnakEkle(rr.Cells("PLANTID").Value) & _
                        " And CUSITM= " & sTirnakEkle(rr.Cells("CUSITM").Value) & _
                        " And GATEID= " & sTirnakEkle(rr.Cells("GATEID").Value) & _
                        " And DELDTE= " & sTirnakEkle(rr.Cells("DELDTE").Value) & _
                        " And PUSNO= " & sTirnakEkle(rr.Cells("PUSNO").Value) & _
                        " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

            db.RunSql(sSQL)

            btnListele_Click(sender, e)

            'grdDesAdv.AllowDelete = InheritableBoolean.True

            'grdDesAdv.Delete()

            'grdDesAdv.AllowDelete = InheritableBoolean.False

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Ekip Mapics")

        End Try
    End Sub

    #End Region 'Methods

End Class