Imports System
Imports System.IO
Imports System.Text
Imports System.Xml

Public Class frmSevkiyatOnay

    #Region "Fields"

    ''Dim dbAs400 As New Core.DataForDB2(My.Settings.cnn)
    Dim dbCore As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dtSevkiyat As New DataTable
    Dim listSilinenler As New List(Of Sevkiyat)
    Dim sSorgu As String = String.Empty
    Dim nKutuMiktari As Double
    Dim dtPalet As New DataTable

    Dim musteri As String = ""

#End Region 'Fields

#Region "Methods"

    Private Sub btnAktar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAktar.Click
        Dim dsYeni, dsTekrar, dsPLANTPRM, dsITMPACK, dsITEMASA, dsMBC6REP As DataSet

        Dim dsEtiketNo As New DataSet

        Dim iKutuSayisi, iEtiketNo, iPaletSayisi, iPalettekiSiraSayisi, _
            iPalettekiKutuMiktari, iMod, iKutuMiktari As Integer

        Dim dMalzemeBirimAgirligi, dKutuBirimAgirligi, dPaletBirimAgirligi, _
            dSeparatorBirimAgirligi, dKapakBirimAgirligi, dNetAgirlik, dBrutAgirilik, _
            KPMik, SPMik As Decimal

        Dim sEtkAdres1, sEtkAdres2, sEtkTip, sKutuKodu, sMusteriKutuKodu, _
            sPaletKodu, sMusteriPaletKodu, sRevizyonNo, sRevizyonTarihi, _
            sDunsNo, sUserF2, sIrsaliyeNo, _
            sHandlingKodu, sSupplierAdres, sKanbanNo, sEtiketNo, sCDAFYV, sCekDate, sC6F1CD, sREFADI, sDEF1CD As String

        Dim PSay As Double = 0

        Dim HSay As Double = 0

        Dim SSay As Double = 0

        Dim KSay As Double = 0

        Dim PKmik As Double = 0

        Dim PaletKSay As Double = 0

        Try

            Dim iHata As Integer = 0

            For Each row2 As DataRow In dtSevkiyat.Rows

                musteri = row2.Item("C6CANB").ToString.Trim

                Exit For
            Next

            If musteri.Trim = "12115" Then
                'MessageBox.Show("PSA müþterisi için manuel düzenleme yapýlamaz")
                btnSevkiyat.Enabled = True
                Exit Sub

            End If

            dbCore.BeginTransaction()

            For Each row2 As DataRow In dtSevkiyat.Rows

                Dim miktar As Double = -1

                If row2.Item("Box_Qty") = 0 Then
                    miktar = 0
                Else
                    miktar = (row2.Item("Ship_Qty") / row2.Item("Box_Qty"))
                End If

                If miktar <> CInt(miktar) Then

                    MessageBox.Show("Kutu Ýçi miktar Ýle Sevk Miktarý Uyumsuz")

                    'dbCore.RollbackTransaction()

                    'Degistir(row)

                    'Exit Sub

                End If

                sSorgu = " Update TRCEKLIST" &
                            " Set ADAQQT = " & row2.Item("Ship_Qty") &
                                " , KMIK=" & row2.Item("Box_Qty") &
                                " , Ambkod=" & sTirnakEkle(row2.Item("Packing_Code")) &
                                " , Ksay=" & row2.Item("Box_Count") &
                            " Where ADCVNB=" & sTirnakEkle(row2.Item("co_num")) &
                            " AND ADFCNB=" & row2.Item("co_line") & _
                            " AND CEKLIST=" & txtPickNo.Text

                dbCore.RunSql(sSorgu)

                iHata += 1

            Next

            For Each stsilinenler As Sevkiyat In listSilinenler

                sSorgu = " Delete From TRCEKLIST" &
                            " Where ADCVNB=" & sTirnakEkle(stsilinenler.co_num) &
                            " AND ADFCNB=" & stsilinenler.co_line

                dbCore.RunSql(sSorgu)

            Next


            sSorgu = " Delete From ETIKETDTY" &
                            " Where PICKNO=" & txtPickNo.Text

            dbCore.RunSql(sSorgu)

            'sSorgu = " Exec TR_Sevkiyat_Etiketi " & txtPickNo.Text

            'dbCore.RunSql(sSorgu)

            Dim row As Janus.Windows.GridEX.GridEXRow

            For i As Integer = 0 To grd.RowCount - 1

                row = grd.GetRow(i)

                Dim Miktar As Double

                'If rdbCekmeListesi.Checked Then

                '    Miktar = row.Cells("ADAQQT").Text

                'ElseIf rdbSevkiyat.Checked Then

                Miktar = row.Cells("Ship_Qty").Text

                'End If

                If row.Cells("Box_Qty").Text = "" Then row.Cells("Box_Qty").Text = 0

                If CInt(row.Cells("Box_Qty").Text) <> 0 Then

                    iKutuSayisi = CInt(Miktar) / CInt(row.Cells("Box_Qty").Text)

                    If CInt(Miktar) - (iKutuSayisi * CInt(row.Cells("Box_Qty").Text)) > 0 Then

                        iKutuSayisi = iKutuSayisi + 1

                    End If

                End If

                'sSorgu = " Select I.ETKTIP, I.KKOD, I.MKKOD, I.PKOD, I.MPKOD, I.PKSIR, " & _
                '        " I.PKMIK, I.REVNO, I.REVTAR , isnull(ITEMDIM_K.BRMAG,0) AS KutuA, " & _
                '        " isnull(ITEMDIM_P.BRMAG,0) AS PaletA, isnull(ITEMDIM_KP.BRMAG,0) AS KapakA, " & _
                '        " isnull(ITEMDIM_SP.BRMAG,0) AS SeperatorA, k.USER1 As HNDCODE, k.KANBAN, k.USER2 " & _
                sSorgu = " Select C6CANB,C6B9CD,CJCBTX,CDAFYV,DEF1CD,T.ITEM,T.REFADI,T.CEKDATE AS CEKDATE,T.C6F1CD," & _
                        " i.ETKTIP, i.KKOD, i.MKKOD, i.PKOD, i.MPKOD, i.PKSIR,ADA3CD,ADCVNB,ADFCNB,CDAFYV, " & _
                        " I.PKMIK, I.REVNO, I.REVTAR , isnull(ITEMDIM_K.BRMAG,0) AS KutuA, " & _
                        " isnull(ITEMDIM_P.BRMAG,0) AS PaletA, isnull(ITEMDIM_KP.BRMAG,0) AS KapakA, " & _
                        " isnull(ITEMDIM_SP.BRMAG,0) AS SeperatorA, k.USER1 As HNDCODE, k.KANBAN, k.USER2 " & _
                " FROM	TrCeklist T    " & _
                 " Left OUTER JOIN ITMPACK I " & _
                    "	On I.Itnbr=T.ADAITX And I.Ambkod=T.Ambkod " & _
                 " Left OUTER JOIN ITEMDIM ITEMDIM_SP " & _
                    "	ON ITEMDIM_SP.ITNBR = I.SPKOD " & _
                 " LEFT OUTER JOIN ITEMDIM ITEMDIM_KP " & _
                    "	ON I.KPKKOD = ITEMDIM_KP.ITNBR " & _
                 " LEFT OUTER JOIN ITEMDIM ITEMDIM_P " & _
                    "	ON I.PKOD = ITEMDIM_P.ITNBR " & _
                 " LEFT OUTER JOIN ITEMDIM ITEMDIM_K " & _
                    "	ON I.KKOD = ITEMDIM_K.ITNBR " & _
                 " Left Outer Join Kontrtpf k " & _
                    "	On k.Bzmitm=I.Itnbr " & _
                         "	And k.Cust=T.C6CANB " & _
                         "	And k.ShipTo=T.C6B9CD " & _
                         "	And k.Kapi=T.C6F1CD " & _
                    " where T.ceklist =" & row.Cells("Pick_No").Text & _
                    " And T.ADAITX=" & sTirnakEkle(row.Cells("Item").Text)

                dsITMPACK = dbCore.RunSql(sSorgu, "ITMPACK")

                If Not dsITMPACK Is Nothing Then

                    If dsITMPACK.Tables("ITMPACK").Rows.Count > 0 Then

                        sDEF1CD = dsITMPACK.Tables("ITMPACK").Rows(0).Item("DEF1CD").ToString

                        sREFADI = dsITMPACK.Tables("ITMPACK").Rows(0).Item("REFADI").ToString

                        sC6F1CD = dsITMPACK.Tables("ITMPACK").Rows(0).Item("C6F1CD").ToString

                        sCekDate = dsITMPACK.Tables("ITMPACK").Rows(0).Item("CEKDATE").ToString

                        sCDAFYV = dsITMPACK.Tables("ITMPACK").Rows(0).Item("CDAFYV").ToString

                        sEtkTip = dsITMPACK.Tables("ITMPACK").Rows(0).Item("ETKTIP").ToString

                        sKutuKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KKOD").ToString

                        sMusteriKutuKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("MKKOD").ToString

                        sPaletKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PKOD").ToString

                        sMusteriPaletKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("MPKOD").ToString

                        iPalettekiSiraSayisi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PKSIR").ToString

                        iPalettekiKutuMiktari = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PKMIK").ToString

                        sRevizyonNo = dsITMPACK.Tables("ITMPACK").Rows(0).Item("REVNO").ToString

                        sRevizyonTarihi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("REVTAR").ToString

                        dKutuBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KutuA").ToString

                        dPaletBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PaletA").ToString

                        dKapakBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KapakA").ToString

                        dSeparatorBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("SeperatorA").ToString

                        sHandlingKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("HNDCODE").ToString

                        sKanbanNo = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KANBAN").ToString

                        sUserF2 = dsITMPACK.Tables("ITMPACK").Rows(0).Item("USER2").ToString

                    Else

                        sEtkTip = ""

                        sKutuKodu = ""

                        sMusteriKutuKodu = ""

                        sPaletKodu = ""

                        sMusteriPaletKodu = ""

                        sRevizyonNo = ""

                        sRevizyonTarihi = ""

                        sHandlingKodu = ""

                        sKanbanNo = ""

                        sUserF2 = ""

                    End If

                End If

                If sEtkTip = "" Then

                    MessageBox.Show(row.Cells("Item").Text & _
                                    " Nolu Malzemenin Etiket Tipi Tanýmsýz" & _
                                    " Etiket Tipini Tanýmlayýp Tekrar Deneyiniz.", "Ekip Mapics")

                    sSorgu = " Delete " & _
                            " From Etiketdty" & _
                            " Where Pickno=" & txtPickNo.Text

                    dbCore.RunSql(sSorgu)

                    Exit Sub

                End If

                If sRevizyonTarihi = "" Or sRevizyonTarihi = "0" Then
                    sRevizyonTarihi = "0"
                Else
                    sRevizyonTarihi = DateConvertAs400(sRevizyonTarihi)
                End If

                sSorgu = " Select WEGHT " & _
                                " From ITEMASA " & _
                                " Where Itnbr ='" & row.Cells("Item").Text & "'"

                dsITEMASA = dbCore.RunSql(sSorgu, "ITEMASA")

                If Not dsITEMASA Is Nothing Then

                    If dsITEMASA.Tables("ITEMASA").Rows.Count > 0 Then

                        dMalzemeBirimAgirligi = dsITEMASA.Tables("ITEMASA").Rows(0).Item("WEGHT").ToString

                    End If

                End If

                sSorgu = "Select   (select ETKADR " & _
                                        " From  EDIPRM  ) As Adres " & _
                            " From MBC6REP " & _
                            " Where C6CVNB ='" & row.Cells("co_num").Text & "'"

                dsMBC6REP = dbCore.RunSql(sSorgu, "MBC6REP")

                If Not dsMBC6REP Is Nothing Then

                    If dsMBC6REP.Tables("MBC6REP").Rows.Count > 0 Then

                        sSupplierAdres = dsMBC6REP.Tables("MBC6REP").Rows(0).Item("Adres").ToString

                    End If

                End If

                sSorgu = " SELECT ETKADR1,ETKADR2, DUNSID,  DUNSID2 " & _
                                    " FROM  PLANTPRM " & _
                                    " Where CANB=" & sTirnakEkle(row.Cells("C6CANB").Text) & _
                                    " And B9CD=" & sTirnakEkle(row.Cells("C6B9CD").Text)

                dsPLANTPRM = dbCore.RunSql(sSorgu, "PLANTPRM")

                If Not dsPLANTPRM Is Nothing Then

                    If dsPLANTPRM.Tables("PLANTPRM").Rows.Count > 0 Then

                        sEtkAdres1 = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("ETKADR1").ToString

                        sEtkAdres2 = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("ETKADR2").ToString

                        If row.Cells("ADA3CD").Text = "BART" Then

                            sDunsNo = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("DUNSID2").ToString

                        Else

                            sDunsNo = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("DUNSID").ToString

                        End If


                    Else

                        sEtkAdres1 = ""

                        sEtkAdres2 = ""

                        'sUserF2 = ""

                        sDunsNo = ""

                    End If

                End If

                'If rdbSevkiyat.Checked Then

                sIrsaliyeNo = row.Cells("IRSNO").Text

                'Else

                '    sIrsaliyeNo = ""

                'End If

                iEtiketNo = SeriNoAl("ETK", iKutuSayisi)

                Dim toplamKutuSayisi As Integer = iKutuSayisi

                For k As Integer = 1 To iKutuSayisi

                    sEtiketNo = iEtiketNo

                    If k = iKutuSayisi Then

                        iMod = CInt(Miktar) Mod CInt(row.Cells("Box_Qty").Text)

                        iKutuMiktari = CInt(Miktar) - (iMod * CInt(row.Cells("Box_Qty").Text))

                        If iMod <> 0 Then

                            row.Cells("Box_Qty").Text = iMod

                        Else

                            row.Cells("Box_Qty").Text = row.Cells("Box_Qty").Text

                        End If

                    End If

                    dNetAgirlik = row.Cells("Box_Qty").Text * dMalzemeBirimAgirligi

                    dBrutAgirilik = dNetAgirlik + dKutuBirimAgirligi

                    nKutuMiktari = row.Cells("Box_Qty").Text

                    Dim sPusNo As String

                    sPusNo = IIf(row.Cells("CJCBTX").Text = "", row.Cells("Pick_No").Text, row.Cells("CJCBTX").Text)

                    'Kutu Etiketlerini insert ediyoruz
                    'Sql
                    sSorgu = "INSERT INTO  ETIKETDTY" & _
                                " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                                " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                                " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                                " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                                " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                                " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                                " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                                " DNGRCODE, IRSNO, DOCK, HNDCODE, SPPLRADR, NETWEIGHT, " & _
                                " BRTWEIGHT, BOXES, QTYPPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE,KANBANNO, Durum ) " & _
                            " VALUES(" & row.Cells("Pick_No").Text & "," & sCekDate & _
                                     ", " & sTirnakEkle(row.Cells("C6CANB").Text) & " , '" & sEtkAdres1 & "' , '" & sEtkAdres2 & "' , '" & sEtkTip & "' , 1 , 0 , '' , '',  '" & row.Cells("C6B9CD").Text & "','" & sC6F1CD & "'" & _
                                     ",'" & row.Cells("ITEM").Text & "','" & sREFADI & "','" & row.Cells("Item").Text & "'," & CInt(row.Cells("Box_Qty").Text.Replace(",", ".")) * toplamKutuSayisi & "," & "'" & sKutuKodu & "'" & ",'" & sMusteriKutuKodu & "'" & _
                                     ", 1," & nKutuMiktari & "," & "'" & sPaletKodu & "'" & "," & "'" & sMusteriPaletKodu & "'" & ", " & iPaletSayisi & " , " & iPalettekiSiraSayisi & " " & _
                                     ", " & iPalettekiKutuMiktari & " , 0 ," & "'" & sRevizyonNo & "'," & sRevizyonTarihi & "," & "'" & sDunsNo & "'," & dMalzemeBirimAgirligi & _
                                     ", " & dKutuBirimAgirligi & " , " & dPaletBirimAgirligi & " , " & " 'KG','KG','KG','KG' " & _
                                     ", 'KG' , " & dSeparatorBirimAgirligi & " , " & dKapakBirimAgirligi & " , '" & sPusNo & "'," & sCekDate & _
                                     ", '" & sUserF2 & "' ,'','" & sIrsaliyeNo & "', '" & sDEF1CD & "' , " & " '" & sHandlingKodu & "', '" & sSupplierAdres & "' " & _
                                     ", " & dNetAgirlik & " , " & dBrutAgirilik & " , 1 ," & nKutuMiktari & ", '" & sEtiketNo & "' , '0' , '', '" & _
                                     sCDAFYV & "','" & row.Cells("co_num").Text & "'," & row.Cells("co_line").Text & ",'" & row.Cells("ADA3CD").Text & "','" & sKanbanNo & "',1" & _
                                     ") "

                    dbCore.RunSql(sSorgu, True)
                    'Kutu Etiketlerini insert ediyoruz

                    iEtiketNo = iEtiketNo - 1

                Next k

            Next i

            sSorgu = "Select Max(C6CANB) As C6CANB, Max(C6B9CD) As C6B9CD " & _
                        " From TRCEKLIST" & _
                        " Where CEKLIST=" & txtPickNo.Text

            dt = dbCore.RunSql(sSorgu)


            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then


                sSorgu = "SELECT	CEKLIST ,C6CANB ,max(CUSNM) As CUSNM  ,C6B9CD," & _
                                " C6F1CD,max(CJCBTX) As CJCBTX ,max(CDAFYV) As CDAFYV, " & _
                                " ADAITX,max(ITDSC) As ITDSC,max(Replace(ITEM,'*',' ')) As ITEM, " & _
                                " max(REFADI) As REFADI,max(MOHTQ) As MOHTQ,sum(ADDZVA) As ADDZVA," & _
                                " sum(ADAQQT) As ADAQQT,max(ADBJDT) As ADBJDT,max(KMIK) As KMIK," & _
                                " max(C6FNST) As C6FNST,max(ADIIST) As ADIIST,max(DEF1CD) As DEF1CD," & _
                                " max(ADA3CD) As ADA3CD,max(CEKDATE) As CEKDATE, max(DURUM) As DURUM," & _
                                " max(KANBANNO) As KANBANNO,max(Ambkod) As Ambkod, Sum(KSAY) As KUTUADET," & _
                                " Max(PKSAY) As PKMIK, MAX(PKSIR) As PKSIR, SUM(SEVKMIK) As SEVKMIK, MAX(SEVKNO) As SEVKNO" & _
                            " FROM TRCEKLIST " & _
                            " Where CEKLIST=" & txtPickNo.Text & _
                            " Group by CEKLIST ,C6CANB ,CUSNM ,C6B9CD,C6F1CD,ADAITX"

                dtPalet = dbCore.RunSql(sSorgu)

                If Not dtPalet Is Nothing AndAlso dtPalet.Rows.Count > 0 Then

                    For i As Integer = 0 To dtPalet.Rows.Count - 1

                        With dtPalet.Rows(i)
                            '************* Palet Sayýsý Hesaplama ********************

                            Dim Miktar As Double

                            Dim KutuAdet As Double


                            Miktar = .Item("ADAQQT").ToString


                            KutuAdet = Math.Ceiling(Miktar / .Item("KMIK").ToString)

                            If (.Item("PKMIK").ToString * .Item("PKSIR").ToString) <> "0" Then

                                PKmik = .Item("PKMIK").ToString

                                If CDec(KutuAdet / .Item("PKSIR").ToString) = Math.Truncate(KutuAdet / .Item("PKSIR").ToString) Then

                                    HSay = 0

                                    KSay = KutuAdet

                                    If Math.Truncate(KSay / .Item("PKMIK").ToString) = CDec(KSay / .Item("PKMIK").ToString) Then

                                        PSay = Math.Truncate(KSay / .Item("PKMIK").ToString)

                                    Else

                                        PSay = Math.Truncate(KSay / .Item("PKMIK").ToString)

                                        PSay = PSay + 1

                                    End If

                                Else

                                    HSay = KutuAdet - ((Math.Truncate(KutuAdet / .Item("PKSIR").ToString) * .Item("PKSIR").ToString))

                                    KSay = KutuAdet - HSay

                                    If Math.Truncate(KSay / .Item("PKMIK").ToString) = CDec(KSay / .Item("PKMIK").ToString) Then

                                        PSay = Math.Truncate(KSay / .Item("PKMIK").ToString)

                                    Else

                                        PSay = Math.Truncate(KSay / .Item("PKMIK").ToString)

                                        PSay = PSay + 1

                                    End If

                                End If

                            End If



                            sSorgu = " Select I.ETKTIP, I.KKOD, I.MKKOD, I.PKOD, I.MPKOD, I.PKSIR, " & _
                                                " I.PKMIK, I.REVNO, I.REVTAR , isnull(ITEMDIM_K.BRMAG,0) AS KutuA, " & _
                                                " isnull(ITEMDIM_P.BRMAG,0) AS PaletA, isnull(ITEMDIM_KP.BRMAG,0) AS KapakA, " & _
                                                " isnull(ITEMDIM_SP.BRMAG,0) AS SeperatorA, k.USER1 As HNDCODE, k.KANBAN , I.KPMIK, I.SPMIK" & _
                                        " FROM	TrCeklist T    " & _
                                         " Left OUTER JOIN ITMPACK I " & _
                                            "	On I.Itnbr=T.ADAITX And I.Ambkod=T.Ambkod " & _
                                         " Left OUTER JOIN ITEMDIM ITEMDIM_SP " & _
                                            "	ON ITEMDIM_SP.ITNBR = I.SPKOD " & _
                                         " LEFT OUTER JOIN ITEMDIM ITEMDIM_KP " & _
                                            "	ON I.KPKKOD = ITEMDIM_KP.ITNBR " & _
                                         " LEFT OUTER JOIN ITEMDIM ITEMDIM_P " & _
                                            "	ON I.PKOD = ITEMDIM_P.ITNBR " & _
                                         " LEFT OUTER JOIN ITEMDIM ITEMDIM_K " & _
                                            "	ON I.KKOD = ITEMDIM_K.ITNBR " & _
                                         " Left Outer Join Kontrtpf k " & _
                                            "	On k.Bzmitm=I.Itnbr " & _
                                                 "	And k.Cust=T.C6CANB " & _
                                                 "	And k.ShipTo=T.C6B9CD " & _
                                                 "	And k.Kapi=T.C6F1CD " & _
                                            " where T.ceklist =" & .Item("CEKLIST").ToString & _
                                            " And T.ADAITX=" & sTirnakEkle(.Item("ADAITX").ToString)

                            dsITMPACK = dbCore.RunSql(sSorgu, "ITMPACK")

                            If Not dsITMPACK Is Nothing Then

                                If dsITMPACK.Tables("ITMPACK").Rows.Count > 0 Then

                                    sEtkTip = dsITMPACK.Tables("ITMPACK").Rows(0).Item("ETKTIP").ToString

                                    sKutuKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KKOD").ToString

                                    sMusteriKutuKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("MKKOD").ToString

                                    sPaletKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PKOD").ToString

                                    sMusteriPaletKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("MPKOD").ToString

                                    iPalettekiSiraSayisi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PKSIR").ToString

                                    iPalettekiKutuMiktari = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PKMIK").ToString

                                    sRevizyonNo = dsITMPACK.Tables("ITMPACK").Rows(0).Item("REVNO").ToString

                                    sRevizyonTarihi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("REVTAR").ToString

                                    dKutuBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KutuA").ToString

                                    dPaletBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("PaletA").ToString

                                    dKapakBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KapakA").ToString

                                    dSeparatorBirimAgirligi = dsITMPACK.Tables("ITMPACK").Rows(0).Item("SeperatorA").ToString

                                    sHandlingKodu = dsITMPACK.Tables("ITMPACK").Rows(0).Item("HNDCODE").ToString

                                    sKanbanNo = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KANBAN").ToString

                                    KPMik = dsITMPACK.Tables("ITMPACK").Rows(0).Item("KPMIK").ToString

                                    SPMik = dsITMPACK.Tables("ITMPACK").Rows(0).Item("SPMIK").ToString

                                Else

                                    sEtkTip = ""

                                    sKutuKodu = ""

                                    sMusteriKutuKodu = ""

                                    sPaletKodu = ""

                                    sMusteriPaletKodu = ""

                                    sRevizyonNo = ""

                                    sRevizyonTarihi = ""

                                    sHandlingKodu = ""

                                    sKanbanNo = ""

                                End If

                            End If

                            If sRevizyonTarihi = "" Or sRevizyonTarihi = "0" Then
                                sRevizyonTarihi = "0"
                            Else
                                sRevizyonTarihi = DateConvertAs400(sRevizyonTarihi)
                            End If

                            sSorgu = " Select WEGHT " & _
                                            " From ITEMASA " & _
                                            " Where Itnbr ='" & .Item("ADAITX").ToString & "'"

                            dsITEMASA = dbCore.RunSql(sSorgu, "ITEMASA")

                            If Not dsITEMASA Is Nothing Then

                                If dsITEMASA.Tables("ITEMASA").Rows.Count > 0 Then

                                    dMalzemeBirimAgirligi = dsITEMASA.Tables("ITEMASA").Rows(0).Item("WEGHT").ToString

                                End If

                            End If

                            sSorgu = "select ETKADR  As Adres " & _
                                                    " From  EDIPRM  "

                            dsMBC6REP = dbCore.RunSql(sSorgu, "MBC6REP")

                            If Not dsMBC6REP Is Nothing Then

                                If dsMBC6REP.Tables("MBC6REP").Rows.Count > 0 Then

                                    sSupplierAdres = dsMBC6REP.Tables("MBC6REP").Rows(0).Item("Adres").ToString

                                End If

                            End If

                            sSorgu = " SELECT ETKADR1,ETKADR2, DUNSID, USERF2,DUNSID2 " & _
                                                " FROM  PLANTPRM " & _
                                                " Where CANB=" & sTirnakEkle(.Item("C6CANB").ToString) & _
                                                " And B9CD=" & sTirnakEkle(.Item("C6B9CD").ToString)

                            dsPLANTPRM = dbCore.RunSql(sSorgu, "PLANTPRM")

                            If Not dsPLANTPRM Is Nothing Then

                                If dsPLANTPRM.Tables("PLANTPRM").Rows.Count > 0 Then

                                    sEtkAdres1 = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("ETKADR1").ToString

                                    sEtkAdres2 = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("ETKADR2").ToString

                                    If .Item("ADA3CD").ToString = "BART" Then

                                        sDunsNo = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("DUNSID2").ToString

                                    Else

                                        sDunsNo = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("DUNSID").ToString

                                    End If

                                    sUserF2 = dsPLANTPRM.Tables("PLANTPRM").Rows(0).Item("USERF2").ToString

                                Else

                                    sEtkAdres1 = ""

                                    sEtkAdres2 = ""

                                    sUserF2 = ""

                                    sDunsNo = ""

                                End If

                            End If



                            'If rdbSevkiyat.Checked Then

                            '    sIrsaliyeNo = .Item("SEVKNO").ToString

                            'Else

                            sIrsaliyeNo = ""

                            'End If



                            sSorgu = " DECLARE @Value AS  NUMERIC (10, 0)  " & _
                                    " EXEC Tr_GetSeqNo 'ETK'," & PSay & ", @Value output " & _
                                    " SELECT @Value"
                            'iEtiketNo = SeriNoAl("ETK", PSay)
                            dt = dbCore.RunSql(sSorgu)

                            If dt.Rows.Count > 0 Then
                                iEtiketNo = dt.Rows(0)(0)
                            End If

                            PaletKSay = HSay

                            For k As Integer = 1 To PSay

                                sEtiketNo = iEtiketNo

                                If KSay >= PKmik Then

                                    PaletKSay = PKmik

                                    KSay = KSay - PKmik

                                Else

                                    PaletKSay = KSay

                                    KSay = 0

                                End If

                                If k = PSay Then

                                    iMod = CInt(Miktar) Mod CInt(.Item("KMIK").ToString)

                                    iKutuMiktari = CInt(Miktar) - (iMod * CInt(.Item("KMIK").ToString))

                                    If iMod <> 0 Then

                                        '.Item("KMIK").ToString = iMod

                                    Else

                                        '.Item("KMIK").ToString = .Item("KMIK").ToString

                                    End If

                                End If

                                dNetAgirlik = .Item("KMIK").ToString * dMalzemeBirimAgirligi * PaletKSay

                                dBrutAgirilik = dNetAgirlik + (dKutuBirimAgirligi * PaletKSay) + dPaletBirimAgirligi + _
                                                (dKapakBirimAgirligi * KPMik) + (dSeparatorBirimAgirligi * SPMik)

                                Dim sPusNo As String

                                sPusNo = IIf(.Item("CJCBTX").ToString = "", .Item("CEKLIST").ToString, .Item("CJCBTX").ToString)

                                'Palet Etiketlerini insert ediyoruz
                                'Sql
                                sSorgu = "INSERT INTO  ETIKETDTY" & _
                                            " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                                            " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                                            " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                                            " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                                            " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                                            " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                                            " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                                            " DNGRCODE, IRSNO, DOCK, HNDCODE, SPPLRADR, NETWEIGHT, " & _
                                            " BRTWEIGHT, BOXES, QTYPPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE,KANBANNO,Durum ) " & _
                                        " VALUES(" & .Item("CEKLIST").ToString & "," & .Item("CEKDATE").ToString & _
                                                 ", " & sTirnakEkle(.Item("C6CANB").ToString) & " , '" & sEtkAdres1 & "' , '" & sEtkAdres2 & "' , '" & sEtkTip & "' , 0 , 1 , '' , '',  '" & .Item("C6B9CD").ToString & "','" & .Item("C6F1CD").ToString & "'" & _
                                                 ",'" & .Item("ITEM").ToString & "','" & .Item("REFADI").ToString & "','" & .Item("ADAITX").ToString & "'," & (PaletKSay * .Item("KMIK").ToString.Replace(",", ".")) & "," & "'" & sKutuKodu & "'" & ",'" & sMusteriKutuKodu & "'" & _
                                                 "," & PaletKSay & "," & .Item("KMIK").ToString & "," & "'" & sPaletKodu & "'" & "," & "'" & sMusteriPaletKodu & "'" & ", " & iPaletSayisi & " , " & iPalettekiSiraSayisi & " " & _
                                                 ", " & iPalettekiKutuMiktari & " , 0 ," & "'" & sRevizyonNo & "'," & sRevizyonTarihi & "," & "'" & sDunsNo & "'," & dMalzemeBirimAgirligi & _
                                                 ", " & dKutuBirimAgirligi & " , " & dPaletBirimAgirligi & " , " & " 'KG','KG','KG','KG' " & _
                                                 ", 'KG' , " & dSeparatorBirimAgirligi & " , " & dKapakBirimAgirligi & " , '" & sPusNo & "'," & .Item("CEKDATE").ToString & _
                                                 ", '" & sUserF2 & "' ,'','" & sIrsaliyeNo & "', '" & .Item("DEF1CD").ToString & "' , " & " '" & sHandlingKodu & "', '" & sSupplierAdres & "' " & _
                                                 ", " & dNetAgirlik & " , " & dBrutAgirilik & " , 1 ," & .Item("KMIK").ToString & ", '" & sEtiketNo & "' , '0' , 'MONO', '" & _
                                                 .Item("CDAFYV").ToString & "','" & "" & "'," & "0" & ",'" & .Item("ADA3CD").ToString & "','" & sKanbanNo & "',1" & _
                                                 ") "

                                dbCore.RunSql(sSorgu, True)
                                'Palet Etiketlerini insert ediyoruz

                                'Palet numaralarýný update edeceðiz
                                sSorgu = "Update EtiketDty" & _
                                            " Set PltNo = " & sEtiketNo & _
                                        " Where EtkSeriNo in (select Top " & PaletKSay & " Etkserino " & _
                                        " from ETiketdty" & _
                                        " where Pltno = 0" & _
                                            " And pickno=" & .Item("CEKLIST").ToString & _
                                            " And Itnbr='" & .Item("ADAITX").ToString & "' " & _
                                            " And KutuETK=1 " & _
                                            " And (EtkSeriNo<>0 Or EtkSeriNo<>'') " & _
                                            " Order By EtkSerino) "

                                dbCore.RunSql(sSorgu, True)

                                iEtiketNo = iEtiketNo - 1

                            Next k

                            sSorgu = "Update EtiketDty" & _
                                        " Set PltNo = -1" & _
                                    " Where EtkSeriNo in (select Top " & HSay & " Etkserino " & _
                                    " from ETiketdty" & _
                                    " where Pltno = 0" & _
                                        " And pickno=" & .Item("CEKLIST").ToString & _
                                        " And Itnbr='" & .Item("ADAITX").ToString & "' " & _
                                        " And KutuETK=1 " & _
                                        " And (EtkSeriNo<>0 Or EtkSeriNo<>'') " & _
                                        " Order By EtkSerino) "

                            dbCore.RunSql(sSorgu, True)

                            sSorgu = "Update EtiketDty" & _
                                       " Set PltNo = -1" & _
                                   " where Pltno = 0" & _
                                       " And pickno=" & .Item("CEKLIST").ToString & _
                                       " And Itnbr='" & .Item("ADAITX").ToString & "' " & _
                                       " And KutuETK=1 " & _
                                       " And (EtkSeriNo<>0 Or EtkSeriNo<>'')"

                            dbCore.RunSql(sSorgu, True)

                        End With

                    Next i

                End If

            End If

            sSorgu = "Update EtiketDty" & _
                        " Set PltNo = -1" & _
                    " where Pltno = 0" & _
                        " And pickno=" & txtPickNo.Text

            dbCore.RunSql(sSorgu, True)




            For Each row2 As DataRow In dtSevkiyat.Rows

                sSorgu = " Update ETIKETDTY" &
                            " Set DURUM='4', MKKOD=" & sTirnakEkle(row2.Item("Box_Grup")) &
                            " Where Pickno=" & txtPickNo.Text &
                            " And Ordno=" & sTirnakEkle(row2.Item("co_num")) &
                            " And OrdSeq=" & row2.Item("co_line")

                dbCore.RunSql(sSorgu)

            Next

            MessageBox.Show("Veriler Kaydedildi")

            btnSevkiyat.Enabled = True

            If dbCore.Transaction = True Then
                dbCore.CommitTransaction()
            End If

        Catch ex As Exception

            If dbCore.Transaction = True Then
                dbCore.RollbackTransaction()
            End If

            'hataya düþerse txt dosyayý oluþturursa silicek.
            'My.Computer.FileSystem.DeleteFile("c:\YPSTXT\deneme.txt")
            MessageBox.Show(ex.Message)

        Finally

        End Try
    End Sub

    Private Sub btnGoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoster.Click
        Try

            If txtPickNo.Text.Length = 0 Then

                MessageBox.Show("Lütfen Çekme Listesi Giriniz...")

                HataGoster.SetError(txtPickNo, "Lütfen Çekme Listesi Giriniz...")

                Exit Sub

            End If

            'sSorgu = " SELECT k.USER1, MAX(ADAITX) AS Item,  MAX(ksay) As Ksay" &
            '                " FROM    TRCEKLIST t" &
            '                " LEFT JOIN KONTRTPF k" &
            '                        " ON k.CUST = t.C6CANB" &
            '                            " AND k.SHIPTO = t.C6B9CD" &
            '                            " AND k.KAPI = t.C6F1CD" &
            '                            " AND k.BZMITM = t.ADAITX" &
            '                " WHERE CEKLIST = " & txtPickNo.Text &
            '                    " AND k.USER1 IS NOT NULL" &
            '                " GROUP BY k.USER1 "

            'Dim sMalzeme As String = ""
            'Dim nKsay As Integer = 0
            'Dim sGrup As String = ""

            'dt = dbCore.RunSql(sSorgu)

            'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each row As DataRow In dt.Rows

            '        sSorgu = " Update TRCEKLIST" &
            '                    " SET     TRCEKLIST.KSAY = CASE WHEN TRCEKLIST.ADAITX=" & sTirnakEkle(row.Item("Item").ToString) &
            '                                " THEN " & row.Item("Ksay").ToString & " ELSE 0 END " &
            '                    " FROM    ITMPACK I" &
            '                    " WHERE k.CUST = TRCEKLIST.C6CANB " &
            '                        " AND k.SHIPTO = TRCEKLIST.C6B9CD" &
            '                        " AND k.KAPI = TRCEKLIST.C6F1CD" &
            '                        " AND k.BZMITM = TRCEKLIST.ADAITX" &
            '                        " AND CEKLIST = " & txtPickNo.Text &
            '                        " AND k.USER1 =" & sTirnakEkle(row.Item("USER1").ToString) &
            '                        " AND k.USER1 IS NOT NULL"

            '        dbCore.RunSql(sSorgu)

            '    Next

            'End If

            sSorgu = " SELECT  Pick_No, co_num, co_line, Item, Description," &
                                " Cust_Item, Cust_Item_Desc, Ship_Qty, Box_Qty," &
                                " Packing_Code, Box_Count, " &
                                " Box_Grup, Cust_Box_Grup, " &
                                " Box_Grup_Orj,C6CANB,C6B9CD,ADA3CD,CJCBTX,SEVKMIK,PKMIK,PKSIR,SEVKMIK,Replace(ITEM,'*',' ') As MURNKOD " &
                        " FROM TR_View_Sevkiyat_Onay" &
                        " where  Pick_No =" & txtPickNo.Text

            dtSevkiyat = dbCore.RunSql(sSorgu)

            KitDuzenle(IIf(chkKit.Checked, 1, 0))

            grd.DataSource = dtSevkiyat

            Duzenle(grd)

            listSilinenler.Clear()

            If Not dtSevkiyat Is Nothing AndAlso dtSevkiyat.Rows.Count > 0 Then

                btnAktar.Enabled = True

            Else

                btnAktar.Enabled = False

            End If

            'For Each row As DataRow In dtSevkiyat.Rows

            '    If row.Item("Box_Qty") <> 0 Then
            '        row.Item("Box_Count") = Math.Ceiling(row.Item("Ship_Qty") / row.Item("Box_Qty"))
            '    Else
            '        row.Item("Box_Count") = 0
            '    End If

            '    row.AcceptChanges()

            'Next

            btnSevkiyat.Enabled = False

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub btnSevkiyat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSevkiyat.Click
        Try

            If txtSevkTarihi.Text = "" Then

                MessageBox.Show("Lütfen Sevk Tarihini Seçiniz!")

                Exit Sub

            End If

            sSorgu = "select  cust_num from customer_mst where LTRIM(cust_num)='" & musteri & "' and stat='D'"
            dt = dbCore.RunSql(sSorgu)

            If dt.Rows.Count > 0 Then
                MessageBox.Show("Müþterinin sevkiyatlarý durdurulmuþtur.Ýþlem yapýlamaz.")
                Exit Sub
            End If

            sSorgu = " SELECT  ADAITX as Item, SUM(ADAQQT) As ShipQty, ( SELECT   qty_on_hand" &
                                                        " FROM TR_Ambar_Malzeme_Stok" &
                                                        " WHERE item = ADAITX" &
                                                            " AND whse = ADA3CD ) As Qty_On_Hand" &
                    " FROM TRCEKLIST" &
                    " WHERE CEKLIST = " & txtPickNo.Text &
                    " GROUP BY ADA3CD, ADAITX" &
                    " HAVING  SUM(ADAQQT) > ( SELECT  qty_on_hand" &
                                                " FROM TR_Ambar_Malzeme_Stok" &
                                                " WHERE item = ADAITX " &
                                                    " AND whse = ADA3CD )"

            dt = dbCore.RunSql(sSorgu)

            Dim sHata As String = "Malzeme" & vbTab & vbTab & "Sevkiyat Miktarý" & vbTab & vbTab & "Eldeki Miktar" & vbNewLine

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows

                    sHata &= row.Item("Item").ToString & vbTab & vbTab & row.Item("ShipQty").ToString & vbTab & vbTab & row.Item("Qty_On_Hand").ToString & vbNewLine

                Next

                MessageBox.Show("Eldeki Miktar Sevkiyat Miktarýndan Az" & vbNewLine & sHata)

                Exit Sub

            End If

            sSorgu = "Exec TR_Sevkiyat_Onay @CekmeNo = " & txtPickNo.Text & " , @Tarih=" & sTirnakEkle(dtpSevkTarihi.Value.ToString("yyyy-MM-dd"))

            dbCore.RunSql(sSorgu)

            MessageBox.Show("Sevkiyat Oluþturuldu...")

            grd.DataSource = Nothing

            grd.Refresh()

            dtpSevkTarihi.Value = Now

            txtSevkTarihi.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString)

        End Try
    End Sub

    Private Sub chkKit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkKit.CheckedChanged
        KitDuzenle(IIf(chkKit.Checked, 1, 0))
    End Sub

    Private Sub Degistir(Optional ByRef rowDt As DataRow = Nothing)
        Dim row As Janus.Windows.GridEX.GridEXRow

        If Not rowDt Is Nothing Then

            For Each rowTmp As Janus.Windows.GridEX.GridEXRow In grd.GetRows

                If rowTmp.Cells("co_num").Text = rowDt.Item("co_num") And
                    rowTmp.Cells("co_line").Text = rowDt.Item("co_line") Then

                    row = rowTmp

                    rowDt = dtSevkiyat.Rows(rowTmp.Position)

                End If
            Next

        Else

            row = grd.SelectedItems(0).GetRow

            rowDt = dtSevkiyat.Select("co_num='" & row.Cells("co_num").Text & "' and co_line=" & row.Cells("co_line").Text)(0)

        End If

        sSevkiyat.co_num = row.Cells("co_num").Text
        sSevkiyat.co_line = row.Cells("co_line").Text
        sSevkiyat.Item = row.Cells("Item").Text
        sSevkiyat.Description = row.Cells("Description").Text
        sSevkiyat.ShipQty = row.Cells("Ship_Qty").Text
        sSevkiyat.BoxQty = row.Cells("Box_Qty").Text
        sSevkiyat.PackingCode = row.Cells("Packing_Code").Text
        sSevkiyat.Iptal = False

        frmSevkiyatKalemDetay.ShowDialog()

        If sSevkiyat.Iptal = False Then

            rowDt.Item("Ship_Qty") = sSevkiyat.ShipQty
            rowDt.Item("Box_Qty") = sSevkiyat.BoxQty
            rowDt.Item("Packing_Code") = sSevkiyat.PackingCode

            rowDt.AcceptChanges()

            dtSevkiyat.AcceptChanges()

            VeriYenile()

        End If
    End Sub

    Private Sub DeðiþtirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Degistir()
    End Sub

    Private Sub dtpSevkTarihi_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpSevkTarihi.ValueChanged
        txtSevkTarihi.Text = CDate(dtpSevkTarihi.Value).ToString("dd.MM.yyyy")
    End Sub

    Private Sub frmSevkiyatOnay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpSevkTarihi.Value = Now

        txtSevkTarihi.Text = ""
    End Sub

    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Degistir()
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        Degistir()
    End Sub

    Private Sub KitDuzenle(Optional ByVal KitDurumu As Integer = 1)
        Try

            For Each row As DataRow In dtSevkiyat.Rows

                If KitDurumu = 1 Then
                    row.Item("Box_Grup") = row.Item("Cust_Box_Grup")
                Else
                    row.Item("Box_Grup") = row.Item("Box_Grup_Orj")
                End If

                If row.Item("Box_Grup").ToString.StartsWith("KIT") Then

                    row.Item("Box_Count") = 0

                Else
                    If row.Item("Box_Qty") = 0 Then
                        row.Item("Box_Count") = 0
                    Else
                        row.Item("Box_Count") = Math.Ceiling(row.Item("Ship_Qty") / row.Item("Box_Qty"))
                    End If
                End If

                row.AcceptChanges()

            Next

            grd.DataSource = Nothing
            grd.Refresh()
            grd.DataSource = dtSevkiyat

        Catch ex As Exception

            MessageBox.Show(ex.ToString)

        End Try
    End Sub

    Private Sub SatýrSilToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SatýrSilToolStripMenuItem.Click
        Sil()
    End Sub

    Private Sub Sil()
        Try

            Dim rowGrd As Janus.Windows.GridEX.GridEXRow

            rowGrd = grd.SelectedItems(0).GetRow

            sSevkiyat.co_num = (rowGrd.Cells("co_num").Text)

            sSevkiyat.co_line = (rowGrd.Cells("co_line").Text)

            listSilinenler.Add(sSevkiyat)

            dtSevkiyat.Rows(grd.SelectedItems(0).Position).Delete()

            dtSevkiyat.AcceptChanges()

            MessageBox.Show("Silme Ýþlemi Baþarý Ýle Gerçekleþti", "Ekip Mapics", MessageBoxButtons.OK)

            VeriYenile()

        Catch ex As Exception

            MessageBox.Show(ex.ToString, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub VeriYenile()
        grd.DataSource = Nothing
        grd.Refresh()

        For Each row As DataRow In dtSevkiyat.Rows

            If row.Item("Box_Qty") <> 0 Then
                row.Item("Box_Count") = Math.Ceiling(row.Item("Ship_Qty") / row.Item("Box_Qty"))
            Else
                row.Item("Box_Count") = 0
            End If

            row.AcceptChanges()

        Next

        dtSevkiyat.AcceptChanges()

        grd.DataSource = dtSevkiyat

        btnSevkiyat.Enabled = False
    End Sub

#End Region 'Methods

End Class