Imports System.IO

Public Class frmAsnGonder

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dtTemp, dt, dtPaletVar, dtPaletYok As New DataTable
    Dim sTumShipNo As ArrayList

    #End Region 'Fields

    #Region "Methods"

    Public Sub UpdateTRM_Shppack(ByVal nASN_No As Integer, _
                           ByVal nKumMiktar As Integer, _
                           ByVal sCustomer As String, _
                           ByVal sPlant As String, _
                           ByVal sGate As String, _
                           ByVal sMalzeme As String, _
                           ByVal nPickList As Integer)

        Dim sSql As String

        Try

            If chkAsnYeni.Checked Then

                sSql = "UPDATE Shppack" &
                        " SET ASNDRM=1 " &
                        " ,ASNNO=" & Convert.ToString(nASN_No) &
                        " ,ASNMIK=" & Convert.ToString(nKumMiktar) &
                        " ,ASNTAR=" & sTirnakEkle(Date.Now.ToString("yyyy-MM-dd HH:mm:ss")) &
                        " WHERE  CUST=" & sTirnakEkle(sCustomer) &
                        " AND SHIPTO=" & sTirnakEkle(sPlant) &
                        " AND KAPI=" & sTirnakEkle(sGate) &
                        " AND ITNBR=" & sTirnakEkle(sMalzeme) &
                        " AND isnull(ASNDRM,0)<>9"

                sSql = sSql & " AND PICKNO=" & nPickList

                db.RunSql(sSql)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub AsnGonderDELPHI(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim jj, nGCount As Integer

        Dim nBrtAgr, nNetAgr, nHacim As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            If sAsnTip = "VDO" Then

                sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            If sAsnTip = "GMUS" Then

                sBGM = ""

            Else

                sBGM = "351"

            End If

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            If sAsnTip <> "VDO" Then

                sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "OPEL-YDK") Then

                sDtm = Now.Date.ToString("yyyyMMdd")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            Else

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sAsnTip <> "VDO" Then

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If sAsnTip = "DELPHI" Then

                sDtm = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' ********** GROSS WEIGHT *********
            If (sAsnTip = "RBOSCH") Then
                sMEA = "WT"
            Else
                sMEA = "AAX"
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sMEAC = "G"
            ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sMEAC = "AAD"
            End If
            If (sAsnTip = "GMUS") Then
                sMEAU = "LBR"
            Else
                sMEAU = "KGM"
            End If

            If sAsnTip = "RENAULT" Then

                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")

            ElseIf sAsnTip = "VDO" Then

                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")

            Else

                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")

            End If

            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "GMUS") Then
                    sMEAC = "N"
                ElseIf (sAsnTip = "RBOSCH") Then
                    sMEAC = "AAF"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAC = "AAC"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAC = "AAL"
                End If
                If (sAsnTip <> "GMUS") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                Else
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            ' ********** KAP ADEDI *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") AndAlso (sAsnTip <> "SAAB-YDK") Then
                ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                If (sAsnTip = "RBOSCH") Then
                    sMEA = "CT"
                Else
                    sMEA = "AAX"
                End If
                If (sAsnTip = "LEAR") Then
                    sMEAC = "ABJ"
                Else
                    sMEAC = "SQ"
                End If
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") Then
                    sMEAU = "C62"
                ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAU = "NMP"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAU = "MTQ"
                End If
                If (sAsnTip <> "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
            End If

            If (sAsnTip <> "LEAR") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sRFF = "SRN"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sRFF = "AAO"
                Else
                    sRFF = "CN"
                End If
                sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "SKODA") Then
                sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "RENAULT" Then
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "VDO" Then
                sNAD = "CZ"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sNAD = "DP"
            ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sNAD = "CN"
            Else
                sNAD = "ST"
            End If
            If sAsnTip <> "RENAULT" Then
                If sAsnTip = "OPEL-YDK" Then
                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                End If
            Else
                If sPlant = "TU" Then
                    sNAD1 = "09317801299870101"
                ElseIf sPlant = "TK" Then
                    sNAD1 = "09317801299870102"
                ElseIf sPlant = "BU" Then
                    sNAD1 = "09317801299870104"
                ElseIf sPlant = "CI" Then
                    sNAD1 = "09459813295CI"
                ElseIf sPlant = "SK" Then
                    sNAD1 = "09317801299870007"
                End If
                ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
            End If
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sNAD = "SE"
                Else
                    sNAD = "SU"
                End If
                If sAsnTip = "OPEL-YDK" Then
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") Then
                sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") OrElse (sAsnTip = "RBOSCH") Then
                If (sAsnTip = "RBOSCH") Then
                    sTOD = "6"
                Else
                    sTOD = ""
                End If
                sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sConv = sPlaka
            Else
                sConv = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT1 = sTermCode
            Else
                sTDT1 = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT2 = "31"
            Else
                sTDT2 = ""
            End If
            If (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                sTDT2 = "9"
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT3 = sCarrier
            Else
                sTDT3 = ""
            End If
            If (sAsnTip <> "RBOSCH") Then
                sTDT4 = sCarrier
            Else
                sTDT4 = ""
            End If
            ' if (sMKod='LR') then sTrnQual:='';
            If sAsnTip = "VDO" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")

            Else

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then
                sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then

                If sPlaka = "" Then
                    sPlaka = "UNKNOWN"
                End If

                sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R")

            ElseIf (sAsnTip = "GMUS") Then

                sLine = "CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("4"c, 3, " ", "R")

            Else

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(nPaletSayisi, 35, " ", "R") & StrFill("SA", 3, " ", "R")

            ElseIf (sAsnTip <> "GMUS") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            End If

            If sAsnTip = "VDO" Then

                sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

            End If

            If (sAsnTip <> "GMUS") Then

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RBOSCH") Then

                sLine = "MEAB" & StrFill("LL", 3, " ", "R") & " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If
            If (sAsnTip = "OPEL-YDK") Then
                sQTYU = "C62"
            ElseIf (sAsnTip = "RBOSCH") Then
                sQTYU = ""
            End If
            If (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "VW2") Then

                sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                sLine = "PCI " & StrFill("17", 3, " ", "R")
                ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            If (sAsnTip = "RENAULT") Then
                sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If Convert.ToInt64(nKutuSayi) > 99 Then
                    For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                        For nGCount = 1 To 99
                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                            If sAsnTip = "DELPHI" Then
                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                            Else
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Next
                        If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then
                            sLine = "PCI " & StrFill("17", 3, " ", "R")
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Else
                            If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then
                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If
                        End If
                    Next
                    For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                Else
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                End If
                nRff += 1
            End If

            If (sAsnTip = "RBOSCH") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            ElseIf (sAsnTip = "OPEL-YDK") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            ElseIf (sAsnTip = "VDO") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")
            Else
                sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip = "VDO") Then

                sMEA = "PD"

                If (sAsnTip = "VDO") Then
                    sMEAC = "AAL"
                End If

                sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sQTYU = "C62"
            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then
                sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "VDO") Then
                sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba
                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then
                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")

                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If

                        objWriter.WriteLine(sLine)

                        nSatir += 1

                    Next

                    nRff += 1

                End If

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı
                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                If (sAsnTip <> "GMUS") Then
                    sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1

                    sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If

                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub AsnGonderGMUS(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim jj, nGCount As Integer

        Dim nBrtAgr, nNetAgr, nHacim As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String = ""

        Dim sSql As String = ""

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            If sAsnTip = "VDO" Then

                sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            If sAsnTip = "GMUS" Then

                sBGM = ""

            Else

                sBGM = "351"

            End If

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            If sAsnTip <> "VDO" Then

                sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "OPEL-YDK") Then

                sDtm = Now.Date.ToString("yyyyMMdd")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            Else

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sAsnTip <> "VDO" Then

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If sAsnTip = "DELPHI" Then

                sDtm = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' ********** GROSS WEIGHT *********
            If (sAsnTip = "RBOSCH") Then
                sMEA = "WT"
            Else
                sMEA = "AAX"
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sMEAC = "G"
            ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sMEAC = "AAD"
            End If
            If (sAsnTip = "GMUS") Then
                sMEAU = "LBR"
            Else
                sMEAU = "KGM"
            End If
            If sAsnTip = "RENAULT" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            ElseIf sAsnTip = "VDO" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")
            ElseIf sAsnTip = "GMUS" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")
            Else
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            End If

            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "GMUS") Then
                    sMEAC = "N"
                ElseIf (sAsnTip = "RBOSCH") Then
                    sMEAC = "AAF"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAC = "AAC"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAC = "AAL"
                End If
                If (sAsnTip <> "GMUS") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                Else
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            ' ********** KAP ADEDI *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") AndAlso (sAsnTip <> "SAAB-YDK") Then
                ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                If (sAsnTip = "RBOSCH") Then
                    sMEA = "CT"
                Else
                    sMEA = "AAX"
                End If
                If (sAsnTip = "LEAR") Then
                    sMEAC = "ABJ"
                Else
                    sMEAC = "SQ"
                End If
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") Then
                    sMEAU = "C62"
                ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAU = "NMP"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAU = "MTQ"
                End If
                If (sAsnTip <> "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
            End If

            If (sAsnTip <> "LEAR") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sRFF = "SRN"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sRFF = "AAO"
                Else
                    sRFF = "CN"
                End If
                sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "SKODA") Then
                sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "RENAULT" Then
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "VDO" Then
                sNAD = "CZ"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sNAD = "DP"
            ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sNAD = "CN"
            Else
                sNAD = "ST"
            End If
            If sAsnTip <> "RENAULT" Then
                If sAsnTip = "OPEL-YDK" Then
                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                End If
            Else
                If sPlant = "TU" Then
                    sNAD1 = "09317801299870101"
                ElseIf sPlant = "TK" Then
                    sNAD1 = "09317801299870102"
                ElseIf sPlant = "BU" Then
                    sNAD1 = "09317801299870104"
                ElseIf sPlant = "CI" Then
                    sNAD1 = "09459813295CI"
                ElseIf sPlant = "SK" Then
                    sNAD1 = "09317801299870007"
                End If
                ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
            End If
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sNAD = "SE"
                Else
                    sNAD = "SU"
                End If
                If sAsnTip = "OPEL-YDK" Then
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") Then
                sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") OrElse (sAsnTip = "RBOSCH") Then
                If (sAsnTip = "RBOSCH") Then
                    sTOD = "6"
                Else
                    sTOD = ""
                End If
                sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sConv = sPlaka
            Else
                sConv = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT1 = sTermCode
            Else
                sTDT1 = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT2 = "31"
            Else
                sTDT2 = ""
            End If
            If (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                sTDT2 = "9"
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT3 = sCarrier
            Else
                sTDT3 = ""
            End If
            If (sAsnTip <> "RBOSCH") Then
                sTDT4 = sCarrier
            Else
                sTDT4 = ""
            End If
            ' if (sMKod='LR') then sTrnQual:='';
            If sAsnTip = "VDO" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")

            Else

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then
                sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then

                If sPlaka = "" Then
                    sPlaka = "UNKNOWN"
                End If

                sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R")

            ElseIf (sAsnTip = "GMUS") Then

                sLine = "CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("4"c, 3, " ", "R")

            Else

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(nPaletSayisi, 35, " ", "R") & StrFill("SA", 3, " ", "R")

            ElseIf (sAsnTip <> "GMUS") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            End If

            If sAsnTip = "VDO" Then

                sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

            End If

            If (sAsnTip <> "GMUS") Then

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RBOSCH") Then

                sLine = "MEAB" & StrFill("LL", 3, " ", "R") & " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If
            If (sAsnTip = "OPEL-YDK") Then
                sQTYU = "C62"
            ElseIf (sAsnTip = "RBOSCH") Then
                sQTYU = ""
            End If
            If (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "VW2") Then

                sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                sLine = "PCI " & StrFill("17", 3, " ", "R")
                ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            If (sAsnTip = "RENAULT") Then
                sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If Convert.ToInt64(nKutuSayi) > 99 Then
                    For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                        For nGCount = 1 To 99
                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                            If sAsnTip = "DELPHI" Then
                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                            Else
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Next
                        If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then
                            sLine = "PCI " & StrFill("17", 3, " ", "R")
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Else
                            If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then
                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If
                        End If
                    Next
                    For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                Else
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                End If
                nRff += 1
            End If

            If (sAsnTip = "RBOSCH") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            ElseIf (sAsnTip = "OPEL-YDK") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            ElseIf (sAsnTip = "VDO") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")
            Else
                sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip = "VDO") Then

                sMEA = "PD"

                If (sAsnTip = "VDO") Then
                    sMEAC = "AAL"
                End If

                sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sQTYU = "C62"
            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then
                sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "VDO") Then
                sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba
                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then
                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")

                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If

                        objWriter.WriteLine(sLine)

                        nSatir += 1

                    Next

                    nRff += 1

                End If

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı
                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                If (sAsnTip <> "GMUS") Then
                    sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1

                    sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If

                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub AsnGonderLEAR(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim jj, nGCount As Integer

        Dim nBrtAgr, nNetAgr, nHacim As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String = ""

        Dim sSql As String = ""

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            If sAsnTip = "VDO" Then

                sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            If sAsnTip = "GMUS" Then

                sBGM = ""

            Else

                sBGM = "351"

            End If

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            If sAsnTip <> "VDO" Then

                sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "OPEL-YDK") Then

                sDtm = Now.Date.ToString("yyyyMMdd")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            Else

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sAsnTip <> "VDO" Then

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If sAsnTip = "DELPHI" Then

                sDtm = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' ********** GROSS WEIGHT *********
            If (sAsnTip = "RBOSCH") Then
                sMEA = "WT"
            Else
                sMEA = "AAX"
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sMEAC = "G"
            ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sMEAC = "AAD"
            End If
            If (sAsnTip = "GMUS") Then
                sMEAU = "LBR"
            Else
                sMEAU = "KGM"
            End If
            If sAsnTip = "RENAULT" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            ElseIf sAsnTip = "VDO" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")
            ElseIf sAsnTip = "GMUS" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")
            Else
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            End If

            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "GMUS") Then
                    sMEAC = "N"
                ElseIf (sAsnTip = "RBOSCH") Then
                    sMEAC = "AAF"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAC = "AAC"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAC = "AAL"
                End If
                If (sAsnTip <> "GMUS") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                Else
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            ' ********** KAP ADEDI *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") AndAlso (sAsnTip <> "SAAB-YDK") Then
                ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                If (sAsnTip = "RBOSCH") Then
                    sMEA = "CT"
                Else
                    sMEA = "AAX"
                End If
                If (sAsnTip = "LEAR") Then
                    sMEAC = "ABJ"
                Else
                    sMEAC = "SQ"
                End If
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") Then
                    sMEAU = "C62"
                ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAU = "NMP"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAU = "MTQ"
                End If
                If (sAsnTip <> "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
            End If

            If (sAsnTip <> "LEAR") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sRFF = "SRN"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sRFF = "AAO"
                Else
                    sRFF = "CN"
                End If
                sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "SKODA") Then
                sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "RENAULT" Then
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "VDO" Then
                sNAD = "CZ"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sNAD = "DP"
            ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sNAD = "CN"
            Else
                sNAD = "ST"
            End If
            If sAsnTip <> "RENAULT" Then
                If sAsnTip = "OPEL-YDK" Then
                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                End If
            Else
                If sPlant = "TU" Then
                    sNAD1 = "09317801299870101"
                ElseIf sPlant = "TK" Then
                    sNAD1 = "09317801299870102"
                ElseIf sPlant = "BU" Then
                    sNAD1 = "09317801299870104"
                ElseIf sPlant = "CI" Then
                    sNAD1 = "09459813295CI"
                ElseIf sPlant = "SK" Then
                    sNAD1 = "09317801299870007"
                End If
                ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
            End If
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sNAD = "SE"
                Else
                    sNAD = "SU"
                End If
                If sAsnTip = "OPEL-YDK" Then
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") Then
                sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") OrElse (sAsnTip = "RBOSCH") Then
                If (sAsnTip = "RBOSCH") Then
                    sTOD = "6"
                Else
                    sTOD = ""
                End If
                sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sConv = sPlaka
            Else
                sConv = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT1 = sTermCode
            Else
                sTDT1 = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT2 = "31"
            Else
                sTDT2 = ""
            End If
            If (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                sTDT2 = "9"
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT3 = sCarrier
            Else
                sTDT3 = ""
            End If
            If (sAsnTip <> "RBOSCH") Then
                sTDT4 = sCarrier
            Else
                sTDT4 = ""
            End If
            ' if (sMKod='LR') then sTrnQual:='';
            If sAsnTip = "VDO" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")

            Else

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then
                sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then

                If sPlaka = "" Then
                    sPlaka = "UNKNOWN"
                End If

                sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R")

            ElseIf (sAsnTip = "GMUS") Then

                sLine = "CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("4"c, 3, " ", "R")

            Else

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(nPaletSayisi, 35, " ", "R") & StrFill("SA", 3, " ", "R")

            ElseIf (sAsnTip <> "GMUS") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            End If

            If sAsnTip = "VDO" Then

                sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

            End If

            If (sAsnTip <> "GMUS") Then

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RBOSCH") Then

                sLine = "MEAB" & StrFill("LL", 3, " ", "R") & " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If
            If (sAsnTip = "OPEL-YDK") Then
                sQTYU = "C62"
            ElseIf (sAsnTip = "RBOSCH") Then
                sQTYU = ""
            End If
            If (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "VW2") Then

                sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                sLine = "PCI " & StrFill("17", 3, " ", "R")
                ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            If (sAsnTip = "RENAULT") Then
                sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If Convert.ToInt64(nKutuSayi) > 99 Then
                    For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                        For nGCount = 1 To 99
                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                            If sAsnTip = "DELPHI" Then
                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                            Else
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Next
                        If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then
                            sLine = "PCI " & StrFill("17", 3, " ", "R")
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Else
                            If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then
                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If
                        End If
                    Next
                    For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                Else
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                End If
                nRff += 1
            End If

            If (sAsnTip = "RBOSCH") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            ElseIf (sAsnTip = "OPEL-YDK") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            ElseIf (sAsnTip = "VDO") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")
            Else
                sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip = "VDO") Then

                sMEA = "PD"

                If (sAsnTip = "VDO") Then
                    sMEAC = "AAL"
                End If

                sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sQTYU = "C62"
            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then
                sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "VDO") Then
                sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba
                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then
                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")

                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If

                        objWriter.WriteLine(sLine)

                        nSatir += 1

                    Next

                    nRff += 1

                End If

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı
                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                If (sAsnTip <> "GMUS") Then
                    sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1

                    sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If

                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub AsnGonderOPEL(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim nBrtAgr, nNetAgr As Double

        Dim sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMURNKOD, _
            sGate, sDtm, sBGM, sMEA, sMEAC, sMEAU, _
            sRFF, sNAD, sKontrat, _
            sConv, sTDT1, sTDT2, sTDT3, sTDT4, sBirim, _
            sQTYU, sShipto, sMalzeme, sAmbalajKodu As String

        sKontrat = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            sBGM = "351"

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

            sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

            sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            ' ********** GROSS WEIGHT *********

            sMEA = "AAX"

            sMEAC = "G"

            sMEAU = "KGM"

            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")

            objWriter.WriteLine(sLine)

            nSatir += 1

            ' ********** NET WEIGHT *********
            sMEAC = "N"

            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")

            objWriter.WriteLine(sLine)

            nSatir += 1

            ' ********** KAP ADEDI *********

            ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı

            sMEA = "AAX"

            sMEAC = "SQ"

            sMEAU = "C62"

            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")

            objWriter.WriteLine(sLine)

            nSatir += 1

            sRFF = "CN"
            sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If sISSUER <> "" Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sNAD = "ST"
            sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            objWriter.WriteLine(sLine)

            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)

            nSatir += 1

            sNAD = "SU"
            sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sConv = ""
            sTDT1 = ""
            sTDT2 = ""
            sTDT3 = ""
            sTDT4 = sCarrier
            ' if (sMKod='LR') then sTrnQual:='';

            sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sPlaka = "" Then
                sPlaka = "UNKNOWN"
            End If

            sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If

            sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********

            sQTYU = "C62"

            sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

                sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

                sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            ' Separator
            If rowDt.Item("SPMIK").ToString <> 0 Then
                sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

                sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        objWriter.Close()
    End Sub

    Public Sub AsnGonderOPELYDK(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim nBrtAgr As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If nLin > 1 Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            sBGM = "351"

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            sDtm = Now.Date.ToString("yyyyMMdd")

            sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

            sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

            objWriter.WriteLine(sLine)

            nSatir += 1

            ' ********** GROSS WEIGHT *********
            sMEA = "AAX"
            sMEAC = "G"
            sMEAU = "KGM"
            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")

            objWriter.WriteLine(sLine)
            nSatir += 1

            sRFF = "CN"
            sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sNAD = "ST"
            sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")

            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sNAD = "SU"
            sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sConv = ""

            sTDT1 = ""
            sTDT2 = ""
            sTDT3 = ""
            sTDT4 = sCarrier
            ' if (sMKod='LR') then sTrnQual:='';

            sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If sPlaka = "" Then
                sPlaka = "UNKNOWN"
            End If

            sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If

            sQTYU = "C62"

            sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            nLin += 1

            sQTYU = "C62"

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = "ALIA" & StrFill("TR", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

                sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

                sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

                sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        objWriter.Close()
    End Sub

    Public Sub AsnGonderSAABYDK(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim jj, nGCount As Integer

        Dim nBrtAgr, nNetAgr, nHacim As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            If sAsnTip = "VDO" Then

                sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            If sAsnTip = "GMUS" Then

                sBGM = ""

            Else

                sBGM = "351"

            End If

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            If sAsnTip <> "VDO" Then

                sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "OPEL-YDK") Then

                sDtm = Now.Date.ToString("yyyyMMdd")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            Else

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sAsnTip <> "VDO" Then

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If sAsnTip = "DELPHI" Then

                sDtm = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' ********** GROSS WEIGHT *********
            If (sAsnTip = "RBOSCH") Then
                sMEA = "WT"
            Else
                sMEA = "AAX"
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sMEAC = "G"
            ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sMEAC = "AAD"
            End If
            If (sAsnTip = "GMUS") Then
                sMEAU = "LBR"
            Else
                sMEAU = "KGM"
            End If
            If sAsnTip = "RENAULT" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            ElseIf sAsnTip = "VDO" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")
            ElseIf sAsnTip = "GMUS" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")
            Else
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            End If

            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "GMUS") Then
                    sMEAC = "N"
                ElseIf (sAsnTip = "RBOSCH") Then
                    sMEAC = "AAF"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAC = "AAC"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAC = "AAL"
                End If
                If (sAsnTip <> "GMUS") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                Else
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            ' ********** KAP ADEDI *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") AndAlso (sAsnTip <> "SAAB-YDK") Then
                ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                If (sAsnTip = "RBOSCH") Then
                    sMEA = "CT"
                Else
                    sMEA = "AAX"
                End If
                If (sAsnTip = "LEAR") Then
                    sMEAC = "ABJ"
                Else
                    sMEAC = "SQ"
                End If
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") Then
                    sMEAU = "C62"
                ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAU = "NMP"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAU = "MTQ"
                End If
                If (sAsnTip <> "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
            End If

            If (sAsnTip <> "LEAR") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sRFF = "SRN"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sRFF = "AAO"
                Else
                    sRFF = "CN"
                End If
                sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "SKODA") Then
                sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "RENAULT" Then
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "VDO" Then
                sNAD = "CZ"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sNAD = "DP"
            ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sNAD = "CN"
            Else
                sNAD = "ST"
            End If
            If sAsnTip <> "RENAULT" Then
                If sAsnTip = "OPEL-YDK" Then
                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                End If
            Else
                If sPlant = "TU" Then
                    sNAD1 = "09317801299870101"
                ElseIf sPlant = "TK" Then
                    sNAD1 = "09317801299870102"
                ElseIf sPlant = "BU" Then
                    sNAD1 = "09317801299870104"
                ElseIf sPlant = "CI" Then
                    sNAD1 = "09459813295CI"
                ElseIf sPlant = "SK" Then
                    sNAD1 = "09317801299870007"
                End If
                ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
            End If
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sNAD = "SE"
                Else
                    sNAD = "SU"
                End If
                If sAsnTip = "OPEL-YDK" Then
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") Then
                sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") OrElse (sAsnTip = "RBOSCH") Then
                If (sAsnTip = "RBOSCH") Then
                    sTOD = "6"
                Else
                    sTOD = ""
                End If
                sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sConv = sPlaka
            Else
                sConv = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT1 = sTermCode
            Else
                sTDT1 = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT2 = "31"
            Else
                sTDT2 = ""
            End If
            If (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                sTDT2 = "9"
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT3 = sCarrier
            Else
                sTDT3 = ""
            End If
            If (sAsnTip <> "RBOSCH") Then
                sTDT4 = sCarrier
            Else
                sTDT4 = ""
            End If
            ' if (sMKod='LR') then sTrnQual:='';
            If sAsnTip = "VDO" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")

            Else

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then
                sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then

                If sPlaka = "" Then
                    sPlaka = "UNKNOWN"
                End If

                sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R")

            ElseIf (sAsnTip = "GMUS") Then

                sLine = "CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("4"c, 3, " ", "R")

            Else

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(nPaletSayisi, 35, " ", "R") & StrFill("SA", 3, " ", "R")

            ElseIf (sAsnTip <> "GMUS") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            End If

            If sAsnTip = "VDO" Then

                sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

            End If

            If (sAsnTip <> "GMUS") Then

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RBOSCH") Then

                sLine = "MEAB" & StrFill("LL", 3, " ", "R") & " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If
            If (sAsnTip = "OPEL-YDK") Then
                sQTYU = "C62"
            ElseIf (sAsnTip = "RBOSCH") Then
                sQTYU = ""
            End If
            If (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "VW2") Then

                sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                sLine = "PCI " & StrFill("17", 3, " ", "R")
                ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            If (sAsnTip = "RENAULT") Then
                sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If Convert.ToInt64(nKutuSayi) > 99 Then
                    For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                        For nGCount = 1 To 99
                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                            If sAsnTip = "DELPHI" Then
                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                            Else
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Next
                        If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then
                            sLine = "PCI " & StrFill("17", 3, " ", "R")
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Else
                            If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then
                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If
                        End If
                    Next
                    For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                Else
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                End If
                nRff += 1
            End If

            If (sAsnTip = "RBOSCH") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            ElseIf (sAsnTip = "OPEL-YDK") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            ElseIf (sAsnTip = "VDO") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")
            Else
                sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip = "VDO") Then

                sMEA = "PD"

                If (sAsnTip = "VDO") Then
                    sMEAC = "AAL"
                End If

                sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sQTYU = "C62"
            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then
                sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "VDO") Then
                sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba
                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then
                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")

                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If

                        objWriter.WriteLine(sLine)

                        nSatir += 1

                    Next

                    nRff += 1

                End If

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı
                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                If (sAsnTip <> "GMUS") Then

                    sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1

                    sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If

                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub AsnGonderSKODA(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim nBrtAgr, nNetAgr As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            nUnh += 1
            sBGM = "351"

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")
            sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")
            sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            ' ********** GROSS WEIGHT *********
            sMEA = "AAX"
            sMEAC = "AAD"
            sMEAU = "KGM"
            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            sMEAC = "AAL"
            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** KAP ADEDI *********
            sMEA = "AAX"
            sMEAC = "SQ"
            sMEAU = "NMP"
            sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sRFF = "AAO"
            sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            sNAD = "CN"
            sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sNAD = "SU"
            sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            sConv = ""
            sTDT1 = ""
            sTDT2 = ""
            sTDT2 = "9"
            sTDT3 = ""
            sTDT4 = sCarrier

            sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If sPlaka = "" Then
                sPlaka = "UNKNOWN"
            End If

            sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If

            sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            nLin += 1

            sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

                sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

                sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then

                sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

                sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1

            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub AsnGonderVDO(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim jj, nGCount As Integer

        Dim nBrtAgr, nNetAgr, nHacim As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            If sAsnTip = "VDO" Then

                sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            If sAsnTip = "GMUS" Then

                sBGM = ""

            Else

                sBGM = "351"

            End If

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            If sAsnTip <> "VDO" Then

                sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "OPEL-YDK") Then

                sDtm = Now.Date.ToString("yyyyMMdd")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            Else

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sAsnTip <> "VDO" Then

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If sAsnTip = "DELPHI" Then

                sDtm = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' ********** GROSS WEIGHT *********
            If (sAsnTip = "RBOSCH") Then
                sMEA = "WT"
            Else
                sMEA = "AAX"
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sMEAC = "G"
            ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sMEAC = "AAD"
            End If
            If (sAsnTip = "GMUS") Then
                sMEAU = "LBR"
            Else
                sMEAU = "KGM"
            End If
            If sAsnTip = "RENAULT" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            ElseIf sAsnTip = "VDO" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")
            ElseIf sAsnTip = "GMUS" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")
            Else
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            End If

            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "GMUS") Then
                    sMEAC = "N"
                ElseIf (sAsnTip = "RBOSCH") Then
                    sMEAC = "AAF"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAC = "AAC"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAC = "AAL"
                End If
                If (sAsnTip <> "GMUS") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                Else
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            ' ********** KAP ADEDI *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") AndAlso (sAsnTip <> "SAAB-YDK") Then
                ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                If (sAsnTip = "RBOSCH") Then
                    sMEA = "CT"
                Else
                    sMEA = "AAX"
                End If
                If (sAsnTip = "LEAR") Then
                    sMEAC = "ABJ"
                Else
                    sMEAC = "SQ"
                End If
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") Then
                    sMEAU = "C62"
                ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAU = "NMP"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAU = "MTQ"
                End If
                If (sAsnTip <> "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
            End If

            If (sAsnTip <> "LEAR") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sRFF = "SRN"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sRFF = "AAO"
                Else
                    sRFF = "CN"
                End If
                sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "SKODA") Then
                sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "RENAULT" Then
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "VDO" Then
                sNAD = "CZ"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sNAD = "DP"
            ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sNAD = "CN"
            Else
                sNAD = "ST"
            End If
            If sAsnTip <> "RENAULT" Then
                If sAsnTip = "OPEL-YDK" Then
                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                End If
            Else
                If sPlant = "TU" Then
                    sNAD1 = "09317801299870101"
                ElseIf sPlant = "TK" Then
                    sNAD1 = "09317801299870102"
                ElseIf sPlant = "BU" Then
                    sNAD1 = "09317801299870104"
                ElseIf sPlant = "CI" Then
                    sNAD1 = "09459813295CI"
                ElseIf sPlant = "SK" Then
                    sNAD1 = "09317801299870007"
                End If
                ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
            End If
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sNAD = "SE"
                Else
                    sNAD = "SU"
                End If
                If sAsnTip = "OPEL-YDK" Then
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") Then
                sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") OrElse (sAsnTip = "RBOSCH") Then
                If (sAsnTip = "RBOSCH") Then
                    sTOD = "6"
                Else
                    sTOD = ""
                End If
                sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sConv = sPlaka
            Else
                sConv = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT1 = sTermCode
            Else
                sTDT1 = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT2 = "31"
            Else
                sTDT2 = ""
            End If
            If (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                sTDT2 = "9"
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT3 = sCarrier
            Else
                sTDT3 = ""
            End If
            If (sAsnTip <> "RBOSCH") Then
                sTDT4 = sCarrier
            Else
                sTDT4 = ""
            End If
            ' if (sMKod='LR') then sTrnQual:='';
            If sAsnTip = "VDO" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")

            Else

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then
                sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then

                If sPlaka = "" Then
                    sPlaka = "UNKNOWN"
                End If

                sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R")

            ElseIf (sAsnTip = "GMUS") Then

                sLine = "CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("4"c, 3, " ", "R")

            Else

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(nPaletSayisi, 35, " ", "R") & StrFill("SA", 3, " ", "R")

            ElseIf (sAsnTip <> "GMUS") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            End If

            If sAsnTip = "VDO" Then

                sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

            End If

            If (sAsnTip <> "GMUS") Then

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RBOSCH") Then

                sLine = "MEAB" & StrFill("LL", 3, " ", "R") & " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If
            If (sAsnTip = "OPEL-YDK") Then
                sQTYU = "C62"
            ElseIf (sAsnTip = "RBOSCH") Then
                sQTYU = ""
            End If
            If (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "VW2") Then

                sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                sLine = "PCI " & StrFill("17", 3, " ", "R")
                ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            If (sAsnTip = "RENAULT") Then
                sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If Convert.ToInt64(nKutuSayi) > 99 Then
                    For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                        For nGCount = 1 To 99
                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                            If sAsnTip = "DELPHI" Then
                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                            Else
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Next
                        If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then
                            sLine = "PCI " & StrFill("17", 3, " ", "R")
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Else
                            If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then
                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If
                        End If
                    Next
                    For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                Else
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                End If
                nRff += 1
            End If

            If (sAsnTip = "RBOSCH") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            ElseIf (sAsnTip = "OPEL-YDK") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            ElseIf (sAsnTip = "VDO") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")
            Else
                sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip = "VDO") Then

                sMEA = "PD"

                If (sAsnTip = "VDO") Then
                    sMEAC = "AAL"
                End If

                sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sQTYU = "C62"
            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then
                sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "VDO") Then
                sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba
                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then
                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")

                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If

                        objWriter.WriteLine(sLine)

                        nSatir += 1

                    Next

                    nRff += 1

                End If

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı
                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                If (sAsnTip <> "GMUS") Then
                    sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1

                    sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If

                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub AsnGonderYdk(ByVal sAsnTip As String, _
        ByVal MesajFile As String, _
        ByVal dtShpPack As DataTable, _
        ByVal sRCVODID As String, _
        ByVal sRCVODCD As String, _
        ByVal sASNODID As String, _
        ByVal sASNODCD As String, _
        ByVal sASN_No As String, _
        ByVal nPickList As Integer, _
        ByVal sFatura As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sISSUER As String, _
        ByVal sLieferDrm As String, _
        ByVal nYolSuresi As Integer, _
        ByVal nKapAdet As Integer, _
        ByVal sCarrier As String, _
        ByVal sDUNS As String, _
        ByVal sTermCode As String, _
        ByVal sPlaka As String, _
        ByVal sTrnQual As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal sDHAABZ As String, _
        ByVal nASN_No As Integer _
        )
        Dim nCps, nMsgNo, nUnh, nLin, nRff, nSatir, nMiktar, _
            nKutuSayi, nKumMiktar, nShpNo, nPickNo, nPaletSayisi _
            As Integer

        Dim jj, nGCount As Integer

        Dim nBrtAgr, nNetAgr, nHacim As Double

        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sGate As String = ""
        Dim sDtm As String = ""
        Dim sBGM As String = ""
        Dim sMEA As String = ""
        Dim sMEAC As String = ""
        Dim sMEAU As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sKontrat As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sBirim As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sShipto As String = ""
        Dim sMalzeme As String = ""
        Dim sAmbalajKodu As String = ""

        Dim sLine As String

        Dim objWriter As System.IO.StreamWriter

        objWriter = New System.IO.StreamWriter(MesajFile)

        nCps = 0

        sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") & " " & StrFill(sRCVODCD, 4, " ", "R") & " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

        objWriter.WriteLine(sLine)

        nMsgNo = 1

        nUnh = 0

        nLin = 1

        For Each rowDt As DataRow In dtShpPack.Rows

            nRff = 1

            sGate = ""
            sGate = rowDt.Item("KAPI").ToString

            sShipto = ""
            sShipto = rowDt.Item("SHIPTO").ToString

            nShpNo = 0
            nShpNo = rowDt.Item("ShpNo").ToString

            sMalzeme = ""
            sMalzeme = rowDt.Item("ITNBR").ToString

            nPickNo = 0
            nPickNo = rowDt.Item("PICKNO").ToString

            sAmbalajKodu = ""
            sAmbalajKodu = rowDt.Item("AMBKOD").ToString

            sMPKOD = ""
            sMKKOD = ""
            sMSPKOD = ""
            sMKPKOD = ""
            sMURNKOD = ""
            ' Müşteri Kutu, Palet, Separator, Kapak Kodları
            MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, sMalzeme, sAmbalajKodu)

            ' Müşteri Ürün Kodu, Adı
            MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, sMalzeme)

            nSatir = 1

            If (nLin > 1) AndAlso (sAsnTip = "GMUS") Then

                GoTo GMUS

            End If

            sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

            If sAsnTip = "VDO" Then

                sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nUnh += 1

            If sAsnTip = "GMUS" Then

                sBGM = ""

            Else

                sBGM = "351"

            End If

            If sLieferDrm = "0" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

            ElseIf sLieferDrm = "1" Then

                sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

            End If

            If sAsnTip <> "VDO" Then

                sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "OPEL-YDK") Then

                sDtm = Now.Date.ToString("yyyyMMdd")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

            Else

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDtm, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If sAsnTip <> "VDO" Then

                sDtm = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If sAsnTip = "DELPHI" Then

                sDtm = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDtm, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If
            ' ********** GROSS WEIGHT *********
            If (sAsnTip = "RBOSCH") Then
                sMEA = "WT"
            Else
                sMEA = "AAX"
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sMEAC = "G"
            ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sMEAC = "AAD"
            End If
            If (sAsnTip = "GMUS") Then
                sMEAU = "LBR"
            Else
                sMEAU = "KGM"
            End If
            If sAsnTip = "RENAULT" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            ElseIf sAsnTip = "VDO" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")
            ElseIf sAsnTip = "GMUS" Then
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")
            Else
                sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
            End If

            objWriter.WriteLine(sLine)
            nSatir += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "GMUS") Then
                    sMEAC = "N"
                ElseIf (sAsnTip = "RBOSCH") Then
                    sMEAC = "AAF"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAC = "AAC"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAC = "AAL"
                End If
                If (sAsnTip <> "GMUS") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                Else
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            ' ********** KAP ADEDI *********
            If (sAsnTip <> "OPEL-YDK") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") AndAlso (sAsnTip <> "SAAB-YDK") Then
                ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                If (sAsnTip = "RBOSCH") Then
                    sMEA = "CT"
                Else
                    sMEA = "AAX"
                End If
                If (sAsnTip = "LEAR") Then
                    sMEAC = "ABJ"
                Else
                    sMEAC = "SQ"
                End If
                If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") Then
                    sMEAU = "C62"
                ElseIf (sAsnTip = "RBOSCH") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sMEAU = "NMP"
                ElseIf (sAsnTip = "LEAR") Then
                    sMEAU = "MTQ"
                End If
                If (sAsnTip <> "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "LEAR") Then
                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
            End If

            If (sAsnTip <> "LEAR") AndAlso (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sRFF = "SRN"
                ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                    sRFF = "AAO"
                Else
                    sRFF = "CN"
                End If
                sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "SKODA") Then
                sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "RENAULT" Then
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If sAsnTip = "VDO" Then
                sNAD = "CZ"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
                sNAD = "SE"
                sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sNAD = "DP"
            ElseIf (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "VW2") Then
                sNAD = "CN"
            Else
                sNAD = "ST"
            End If
            If sAsnTip <> "RENAULT" Then
                If sAsnTip = "OPEL-YDK" Then
                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                End If
            Else
                If sPlant = "TU" Then
                    sNAD1 = "09317801299870101"
                ElseIf sPlant = "TK" Then
                    sNAD1 = "09317801299870102"
                ElseIf sPlant = "BU" Then
                    sNAD1 = "09317801299870104"
                ElseIf sPlant = "CI" Then
                    sNAD1 = "09459813295CI"
                ElseIf sPlant = "SK" Then
                    sNAD1 = "09317801299870007"
                End If
                ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
            End If
            objWriter.WriteLine(sLine)
            nSatir += 1
            sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            If (sAsnTip <> "RENAULT") AndAlso (sAsnTip <> "VDO") Then
                If (sAsnTip = "RBOSCH") Then
                    sNAD = "SE"
                Else
                    sNAD = "SU"
                End If
                If sAsnTip = "OPEL-YDK" Then
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                Else
                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                End If
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") Then
                sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "LEAR") OrElse (sAsnTip = "RBOSCH") Then
                If (sAsnTip = "RBOSCH") Then
                    sTOD = "6"
                Else
                    sTOD = ""
                End If
                sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RBOSCH") Then
                sConv = sPlaka
            Else
                sConv = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT1 = sTermCode
            Else
                sTDT1 = ""
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT2 = "31"
            Else
                sTDT2 = ""
            End If
            If (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "VW2") Then
                sTDT2 = "9"
            End If
            If (sAsnTip = "RBOSCH") Then
                sTDT3 = sCarrier
            Else
                sTDT3 = ""
            End If
            If (sAsnTip <> "RBOSCH") Then
                sTDT4 = sCarrier
            Else
                sTDT4 = ""
            End If
            ' if (sMKod='LR') then sTrnQual:='';
            If sAsnTip = "VDO" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")

            ElseIf sAsnTip = "GMUS" Then

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")

            Else

                sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then
                sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then

                If sPlaka = "" Then
                    sPlaka = "UNKNOWN"
                End If

                sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            nMiktar = rowDt.Item("SHIPMIK").ToString

            nKutuSayi = rowDt.Item("KSAY").ToString

            nPaletSayisi = rowDt.Item("PSAY").ToString

            UpdateKumul(nKumMiktar, sCustomer, sPlant, sMalzeme, sGate, sKumulDuzeyi, nMiktar, nShpNo, sShipto, nPickNo)

        GMUS:

            nCps += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R")

            ElseIf (sAsnTip = "GMUS") Then

                sLine = "CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("4"c, 3, " ", "R")

            Else

                sLine = "CPS " & StrFill("1", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            If (sAsnTip = "RBOSCH") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(nPaletSayisi, 35, " ", "R") & StrFill("SA", 3, " ", "R")

            ElseIf (sAsnTip <> "GMUS") Then

                sLine = "PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

            End If

            If sAsnTip = "VDO" Then

                sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

            End If

            If (sAsnTip <> "GMUS") Then

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RBOSCH") Then

                sLine = "MEAB" & StrFill("LL", 3, " ", "R") & " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
            sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

            If sBirim = "AD" Then
                sQTYU = "PCE"
            ElseIf sBirim = "KG" Then
                sQTYU = "KGM"
            ElseIf sBirim = "MT" Then
                sQTYU = "MTR"
            End If
            If (sAsnTip = "OPEL-YDK") Then
                sQTYU = "C62"
            ElseIf (sAsnTip = "RBOSCH") Then
                sQTYU = ""
            End If
            If (sAsnTip = "RBOSCH") OrElse (sAsnTip = "LEAR") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "VW2") Then

                sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                sLine = "PCI " & StrFill("17", 3, " ", "R")
                ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            If (sAsnTip = "RENAULT") Then
                sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If
            If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") OrElse (sAsnTip = "DELPHI") Then
                ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If Convert.ToInt64(nKutuSayi) > 99 Then
                    For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                        For nGCount = 1 To 99
                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                            If sAsnTip = "DELPHI" Then
                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                            Else
                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Next
                        If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then
                            sLine = "PCI " & StrFill("17", 3, " ", "R")
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                        Else
                            If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then
                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If
                        End If
                    Next
                    For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                Else
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)
                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else
                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                End If
                nRff += 1
            End If

            If (sAsnTip = "RBOSCH") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
            ElseIf (sAsnTip = "OPEL-YDK") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            ElseIf (sAsnTip = "VDO") Then
                sLine = "LIN " & StrFill(CStr(nLin), 6, " ", "R") & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")
            Else
                sLine = "LIN " & StrFill("", 11, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") & " " & StrFill("IN", 3, " ", "R")
            End If

            objWriter.WriteLine(sLine)

            nSatir += 1

            nLin += 1
            ' ********** NET WEIGHT *********
            If (sAsnTip = "VDO") Then

                sMEA = "PD"

                If (sAsnTip = "VDO") Then
                    sMEAC = "AAL"
                End If

                sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                objWriter.WriteLine(sLine)

                nSatir += 1

            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "GMUS") Then
                sQTYU = "C62"
            End If

            If (sAsnTip = "OPEL") OrElse (sAsnTip = "DELPHI") OrElse (sAsnTip = "SAAB-YDK") OrElse (sAsnTip = "SKODA") OrElse (sAsnTip = "VW") OrElse (sAsnTip = "AUDI") OrElse (sAsnTip = "RENAULT") OrElse (sAsnTip = "GMUS") OrElse (sAsnTip = "VW2") Then
                sLine = ("QTYA" & StrFill("3", 3, " ", "R") & " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1

            If (sAsnTip = "OPEL-YDK") OrElse (sAsnTip = "VDO") Then
                sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                objWriter.WriteLine(sLine)
                nSatir += 1
            End If

            sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")
            objWriter.WriteLine(sLine)
            nSatir += 1
            ' Palet

            If rowDt.Item("PSAY").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba
                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then
                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")

                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If

                        objWriter.WriteLine(sLine)

                        nSatir += 1

                    Next

                    nRff += 1

                End If

            End If
            ' Kapak

            If rowDt.Item("KPMIK").ToString <> 0 Then

                If (sAsnTip <> "GMUS") Then

                    sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                    sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then
                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı
                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")

                        Else

                            sLine = "GIR " & StrFill("3", 3, " ", "R") + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")

                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If
            ' Separator

            If rowDt.Item("SPMIK").ToString <> 0 Then
                If (sAsnTip <> "GMUS") Then
                    sLine = "CPS " & StrFill("4", 12, " ", "R") & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1

                    sLine = "PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L") & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                    If sAsnTip = "VDO" Then

                        sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                    End If

                    objWriter.WriteLine(sLine)

                    nSatir += 1

                End If

                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                If (sAsnTip = "RENAULT") Then

                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                    objWriter.WriteLine(sLine)
                    nSatir += 1
                End If
                ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                If (sAsnTip = "RENAULT") OrElse (sAsnTip = "VDO") Then
                    ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                    For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                        sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                        If sAsnTip = "DELPHI" Then

                            ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                        Else

                            sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                        End If
                        objWriter.WriteLine(sLine)
                        nSatir += 1
                    Next
                    nRff += 1
                End If
            End If

            sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
            ' WriteLn(fMessages,sLine); inc(nSatir);
            nMsgNo = nMsgNo + 1
            ' end;

            UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, sMalzeme, nPickList)

        Next rowDt

        sLine = "UNZ " & StrFill(Convert.ToString(nUnh), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
        ' WriteLn(fMessages,sLine); inc(nSatir);
        ' ********************VW AUDI -- DESADV D98 A******************************
        objWriter.Close()
    End Sub

    Public Sub UpdateKumul(ByRef nKumMiktar As Integer, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sMalzeme As String, _
        ByVal sGate As String, _
        ByVal sKumulDuzeyi As String, _
        ByVal nMiktar As Integer, _
        ByVal nShpNo As Integer, _
        ByVal sShipTo As String, _
        ByVal nPickNo As Integer _
        )
        Dim sSql As String

        Try

            If chkAsnYeni.Checked Then

                sSql = " CANBK=" & sTirnakEkle(sCustomer) & _
                        " AND B9CDK=" & sTirnakEkle(sPlant) & _
                        " AND AITXK=" & sTirnakEkle(sMalzeme)

                If sKumulDuzeyi = 1 Then

                    sSql = sSql & " And KAPI=" & sTirnakEkle(sGate)

                End If

                nKumMiktar = nLookup("ASNKUM", "OFFITEMBL", sSql)

                nKumMiktar = nKumMiktar + nMiktar

                sSql = " UPDATE " & "OFFITEMBL" & _
                            " SET ASNKUM=" & Convert.ToString(nKumMiktar) & _
                            " WHERE CANBK=" & sTirnakEkle(sCustomer) & _
                            " AND B9CDK=" & sTirnakEkle(sPlant) & _
                            " AND AITXK=" & sTirnakEkle(sMalzeme)

                If sKumulDuzeyi = 1 Then

                    sSql = sSql & " And Gate=" & sTirnakEkle(sGate)

                End If

                db.RunSql(sSql)

            Else

                sSql = "  SHPNO=" & Convert.ToString(nShpNo) & _
                        " AND SHIPTO=" & sTirnakEkle(sShipTo) & _
                        " AND KAPI=" & sTirnakEkle(sGate) & _
                        " AND ITNBR=" & sTirnakEkle(sMalzeme) & _
                        " AND isnull(ASNDRM,0)<>9"

                sSql = sSql & " AND PICKNO=" & Convert.ToString(nPickNo)

                nKumMiktar = nLookup("ASNMIK", "SHPPACK", sSql)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub UpdateShpPack(ByVal nASN_No As Integer, _
        ByVal nKumMiktar As Integer, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sGate As String, _
        ByVal sMalzeme As String, _
        ByVal nPickList As Integer)
        Dim sSql As String

        Try

            If chkAsnYeni.Checked Then

                sSql = "UPDATE SHPPACK" & _
                        " SET ASNDRM=1 " & _
                        " ,ASNNO=" & Convert.ToString(nASN_No) & _
                        " ,ASNMIK=" & Convert.ToString(nKumMiktar) & _
                        " WHERE  CUST=" & sTirnakEkle(sCustomer) & _
                        " AND SHIPTO=" & sTirnakEkle(sPlant) & _
                        " AND KAPI=" & sTirnakEkle(sGate) & _
                        " AND ITNBR=" & sTirnakEkle(sMalzeme) & _
                        " AND isnull(ASNDRM,0)<>9"

                sSql = sSql & " AND PICKNO=" & Convert.ToString(nPickList)

                db.RunSql(sSql)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAsnGonder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAsnGonder.Click
        Dim sayac713 As Boolean = True
        Dim sSQL As String
        Dim sError As String = ""
        Dim sLine As String = ""
        Dim sASN_No As String = ""
        Dim sASNTip As String = ""
        Dim sUSERF3 As String = ""
        Dim sTarih As String = ""
        Dim sSDRODID As String = ""
        Dim sASNODID As String = ""
        Dim sASNODCD As String = ""
        Dim sRCVODID As String = ""
        Dim sSDRODCD As String = ""
        Dim sRCVODCD As String = ""
        Dim sISSUER As String = ""
        Dim sTmpItnbr As String = ""
        Dim sMPKOD As String = ""
        Dim sMKKOD As String = ""
        Dim sMSPKOD As String = ""
        Dim sMKPKOD As String = ""
        Dim sMURNKOD As String = ""
        Dim sPusNo As String = ""
        Dim sYedekParcaPlanti As String
        Dim sIrsaliyeno As String = ""
        Dim nPickList As Integer
        Dim nShipNo As Integer
        Dim sCarrier As String = ""
        Dim sPlant As String = ""
        Dim sCustomerSeq As Integer
        Dim sCustomer As String = ""
        Dim sGate As String = ""
        Dim sTermCode As String = ""
        Dim sFatura As String = ""
        Dim sLieferDrm As String = ""
        Dim sSevkYeri As String = ""
        Dim sDUNS As String = ""
        Dim sDunsSifir As String = ""
        Dim sDHAABZ As String = ""
        Dim sPlaka As String
        Dim sTrnQual As String = ""
        Dim sKontrat As String = ""
        Dim sUSERF2 As String = ""
        Dim sCarrierID As String = ""
        Dim sKumulDuzeyi As Integer
        Dim sConsignee As String = ""
        Dim sGateTmp As String = ""
        'Dim sAsnKons As String
        Dim j As Integer
        Dim nMsgNo As Integer
        Dim nSatir As Integer
        Dim nUNH As Integer
        Dim nPac As Integer
        Dim nRff As Integer
        Dim nLin As Integer
        Dim nGCount As Integer
        Dim nYolSure As Integer
        Dim nYolSuresi As Integer
        Dim nLine As Integer
        Dim i As Integer
        Dim k As Integer
        Dim sGirNo As Integer
        Dim n713Bosluk As Integer
        Dim nASN_No As Double
        Dim nNetAgr As Double
        Dim nKapAdet As Double
        Dim nHacim As Double
        Dim nMiktar As Double
        Dim nKumMiktar As Double
        Dim bTmpItnbr As Boolean
        Dim bOk As Boolean
        Dim nIlkKutuNo As Int64
        Dim nIlkPaletNo As Int64
        Dim nBrtAgr As Double
        Dim nBrtAgrVda As Integer
        Dim nNetAgrVda As Integer
        Dim nBrtAgrRnd As Integer
        Dim nKutuSayi As Double
        Dim sASNDir As String = ""
        Dim MesajFile As String = ""
        Dim sBirim As String = ""
        Dim sCrdTim As String = ""
        Dim sBGM As String = ""
        Dim sDTM As String = ""
        Dim sTime As String = ""
        Dim sMEA As String = ""
        Dim sMEAU As String = ""
        Dim sMEAC As String = ""
        Dim sRFF As String = ""
        Dim sNAD As String = ""
        Dim sNAD1 As String = ""
        Dim sTOD As String = ""
        Dim sConv As String = ""
        Dim sTDT1 As String = ""
        Dim sTDT2 As String = ""
        Dim sTDT3 As String = ""
        Dim sTDT4 As String = ""
        Dim sQTYU As String = ""
        Dim sGir As String = ""
        Dim sRAdr As String = ""
        Dim nCps As Integer
        Dim jj As Integer
        Dim nRffAat As Integer
        Dim nGir As Integer
        Dim nPaletKutuAdedi As Integer
        Dim sGIRMik As Integer
        Dim sGIRMikKayit As Integer
        Dim nDonguSayisi As Integer
        Dim objWriter As System.IO.StreamWriter
        Dim sAmbar As String = ""
        Dim nNavlunNo As String = ""
        Dim sSeferNo As String = ""

        Try

            If MessageBox.Show("Dikkat!..Seçilen Çeki Listeleri için ASN Dosyası oluşturulacaktır. Onaylıyor musunuz?...", Application.ProductName, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then

                Exit Sub

            End If

            mError.Text = ""

            sASNDir = sLookup("EDIASNDIR", "EDIPRM", "")
            'sASNDir = "c:\EkipEdi"

            sTumShipNo = New ArrayList()

            bOk = True

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            Dim row As Janus.Windows.GridEX.GridEXRow

            checkedRows = grdDesAdv.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else
                Dim picklist As String = String.Empty

                For Each row In checkedRows

                    If Not picklist.Contains(Convert.ToString(row.Cells("PICKNO").Text)) Then

                        picklist = IIf(picklist = "", "", picklist & ",") & row.Cells("PICKNO").Text

                    End If

                Next
                's.USERF4 NAVLUN
                sSQL = "SELECT PICKNO,ITNBR,SHIPMIK,KKOD,cust_box_code as Koli_Tanimi,KSAY,PSAY,KPMIK,PKOD,cust_palette_code as Palet_Tanimi," & _
                         " KPKOD,cust_cover_code as Kapak_Tanimi, s.USERF5 SEFERNO ,NAVLUNNO NAVLUN, PLAKA,MURNKOD  FROM SHPPACK s" & _
                         " LEFT JOIN TR_itempack_edi p on p.item=s.ITNBR and p.pack_type=AMBKOD WHERE PICKNO IN (" & picklist & ") order by 2"
                dt = db.RunSql(sSQL)

                frmDetay.picklist = picklist
                frmDetay.dt = dt
                frmDetay.ShowDialog()

                If frmDetay.iptal Then
                    Exit Sub
                End If


                For Each row In checkedRows

                    sSDRODID = ""
                    sRCVODID = ""
                    sSDRODCD = ""
                    sRCVODCD = ""
                    sASNODID = ""
                    sASNODCD = ""
                    sISSUER = ""
                    sCarrier = ""
                    sDHAABZ = ""
                    sPlaka = ""
                    sDUNS = ""
                    sAmbar = ""

                    nShipNo = Convert.ToString(row.Cells("ShpNo").Text)

                    sTumShipNo.Add(nShipNo)

                    nPickList = Convert.ToString(row.Cells("PIckno").Text)

                    sFatura = Convert.ToString(row.Cells("Invno").Text)

                    'sAsnKons = row.Cells(10).Text
                    sPlant = row.Cells("ShIpto").Text
                    'sEdiPlant = row.Cells(11).Text
                    sAmbar = row.Cells("Whse").Text

                    sCustomer = Convert.ToString(row.Cells("Cust").Text)

                    sCustomerSeq = IIf(row.Cells("Cust_seq").Text = "", 0, row.Cells("Cust_seq").Text)

                    sLieferDrm = "0"
                    ' Sender,Receiver odette ID, Material Issuer

                    sSQL = " SELECT SDRODID,RCVODID,SDRODCD,RCVODCD,ASNODID,ASNODCD," & _
                                " ISSUER,DUNSID,USERF1,USERF2,USERF3,isnull(LIEFERDRM,0) as LIEFERDRM ,TTIME,CONSCODE,DUNSID2 " & _
                                " FROM PLANTPRM" & _
                                " WHERE CANB=" & sTirnakEkle(sCustomer) & _
                                " AND B9CD=" & sTirnakEkle(sPlant) & _
                                " And Cust_seq=" & sCustomerSeq

                    dt = db.RunSql(sSQL)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        GetRowInfo(sSDRODID, dt, 0, "SDRODID")

                        GetRowInfo(sASNODID, dt, 0, "ASNODID")

                        GetRowInfo(sRCVODID, dt, 0, "RCVODID")

                        GetRowInfo(sASNODCD, dt, 0, "ASNODCD")

                        GetRowInfo(sSDRODCD, dt, 0, "SDRODCD")

                        GetRowInfo(sRCVODCD, dt, 0, "RCVODCD")

                        GetRowInfo(sISSUER, dt, 0, "ISSUER")

                        If sAmbar = "BART" Then

                            GetRowInfo(sDUNS, dt, 0, "DUNSID2")

                        Else

                            GetRowInfo(sDUNS, dt, 0, "DUNSID")

                        End If

                        GetRowInfo(sASNTip, dt, 0, "USERF1")

                        GetRowInfo(sUSERF2, dt, 0, "USERF2")

                        GetRowInfo(sLieferDrm, dt, 0, "LIEFERDRM")

                        GetRowInfo(nYolSuresi, dt, 0, "TTIME")

                        GetRowInfo(sUSERF3, dt, 0, "USERF3")

                        GetRowInfo(sConsignee, dt, 0, "CONSCODE")

                    End If

                    sError = ""

                    If (sSDRODID = "") OrElse (sRCVODID = "") OrElse (sASNODID = "") Then
                        sError = " ODETTE ID Tanımlı Değil."
                    End If
                    If sDUNS = "" Then
                        sError = sError & " DUNS No Tanımlı Değil."
                    End If
                    If sASNTip = "" Then
                        sError = sError & " ASN Tipi Tanımlı Değil"
                    End If
                    If Not (Directory.Exists(sASNDir) OrElse Directory.Exists(sASNDir + "\" + sASNODID)) Then
                        sError = sError & " ASN Directory Yok."
                    End If

                    sSQL = " SELECT * " & _
                                    " FROM SHPPACK" & " S " & _
                                    " Inner JOIN KONTRTPF" & " K " & _
                                    " ON  K.CUST=S.CUST AND " & _
                                    " K.SHIPTO=S.SHIPTO AND K.KAPI=S.KAPI AND K.BZMITM=S.ITNBR "

                    sSQL = sSQL & " WHERE S.PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                    dt = db.RunSql(sSQL)

                    If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then

                        sError = sError & " Müşteri Malzeme Kodu Tanımsız"

                    End If

                    sSQL = " SELECT I.MKKOD,I.MPKOD,I.MSPKOD,I.MKPKOD," & _
                                    " S.KKOD,S.PKOD,S.SPKOD,S.KPKOD,S.KSAY," & _
                                    " S.PSAY,S.SPMIK,S.KPMIK " & _
                            " FROM SHPPACK  S " & _
                            " LEFT OUTER JOIN ITMPACK  I " & _
                                " ON  I.AMBKOD=S.AMBKOD AND I.ITNBR=S.ITNBR"

                    sSQL = sSQL & " WHERE S.PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                    dt = db.RunSql(sSQL)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        If (dt.Rows(0).Item("KSAY").ToString <> 0) AndAlso (dt.Rows(0).Item("MKKOD").ToString = "") Then

                            sError = sError & " Müşteri Kutu Kodu Tanımsız"

                        End If

                        If (dt.Rows(0).Item("PSAY").ToString <> 0) AndAlso (dt.Rows(0).Item("MPKOD").ToString = "") Then

                            sError = sError & " Müşteri Palet Kodu Tanımsız"

                        End If

                        If (dt.Rows(0).Item("KPKOD").ToString <> "") Then

                            If (dt.Rows(0).Item("KPMIK").ToString <> 0) AndAlso (dt.Rows(0).Item("MKPKOD").ToString = "") Then

                                sError = sError & " Müşteri Kapak Kodu Tanımsız"

                            End If

                        End If

                        If (dt.Rows(0).Item("SPKOD").ToString <> "") Then

                            If (dt.Rows(0).Item("SPMIK").ToString <> 0) AndAlso (dt.Rows(0).Item("MSPKOD").ToString = "") Then

                                sError = sError & " Müşteri Separatör Kodu Tanımsız"

                            End If

                        End If

                    End If

                    If ((sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "SAAB-YDK")) AndAlso (sISSUER = "") Then

                        sError = sError & " Material Issuer Tanımlı Değil"

                    End If

                    If sError <> "" Then

                        mError.Text = "PickList :" & Convert.ToString(row.Cells("PIckno").Text) & "  ASN Mesajı Oluşturulamadı. " + sError

                        Exit Sub

                    End If

                    If chkAsnYeni.Checked Then
                        ' ASN Numarası

                        nASN_No = SeriNoAl("AsnNo")

                        sASN_No = Convert.ToString(nASN_No).PadLeft(10, "0")

                    Else

                        sSQL = " PICKNO=" & Convert.ToString(row.Cells("PIckno").Text) & " AND isnull(ASNDRM,0)<>9"

                        nASN_No = nLookup("ASNNO", "SHPPACK", sSQL)

                        sASN_No = Convert.ToString(nASN_No).PadLeft(10, "0")

                    End If
                    ' Nakliyeci, Plaka Numarası .

                    sSQL = " SELECT Plaka, Carrier, Trncode,USERF4,USERF5,NAVLUNNO,IRSNO " & _
                            " FROM Shppack" & _
                            " WHERE Shpno=" & nShipNo & _
                            " AND Pickno=" & nPickList

                    dt = db.RunSql(sSQL)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        GetRowInfo(sPlaka, dt, 0, "Plaka")

                        GetRowInfo(sDHAABZ, dt, 0, "Trncode")

                        GetRowInfo(sCarrier, dt, 0, "Carrier")

                        GetRowInfo(nNavlunNo, dt, 0, "NAVLUNNO")

                        'If nNavlunNo = "" Then
                        '    MessageBox.Show("Navlun no boş, lütfen kontrol ediniz.", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    Exit Sub
                        'End If


                        GetRowInfo(sSeferNo, dt, 0, ("USERF5"))
                        'If sSeferNo = "" Then
                        '    MessageBox.Show("Sefer no boş, lütfen kontrol ediniz.", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    Exit Sub
                        'End If

                        GetRowInfo(sIrsaliyeno, dt, 0, "IRSNO")

                        'GetRowInfo(nNavlunNo, dt, 0, "USERF4")


                    End If

                    ' FOB,CIF,EXW Bilgisi

                    sTermCode = sLookup("terms_code", "customer", " cust_num=" & sTirnakEkle(sCustomer) & " AND cust_seq=0")

                    If (sTermCode = "FOB") OrElse (sTermCode = "EXW") Then
                        sTrnQual = "12"
                    ElseIf sTermCode = "CIF" Then
                        sTrnQual = "25"
                    Else
                        sTrnQual = "12"
                    End If

                    ' Brüt Ağırlık, Net Ağırlık, Kap Adedi Hesaplama
                    nBrtAgr = 0
                    nNetAgr = 0
                    nKapAdet = 0
                    nHacim = 0

                    sSQL = " SELECT SUM(BRTAGR) AS BAGR ,SUM(NETAGR) AS NAGR,SUM(HKSAY) AS HKUTU," & _
                                    "SUM(PSAY) AS PSAYI, SUM(HACIM) AS HCM " & _
                                "FROM " & "SHPPACK" & _
                                " WHERE  CUST=" & sTirnakEkle(sCustomer)

                    sSQL = sSQL & " AND PICKNO=" & nPickList & " AND isnull(ASNDRM,0)<>9"

                    dt = db.RunSql(sSQL)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        GetRowInfo(nBrtAgr, dt, 0, "BAGR")

                        GetRowInfo(nNetAgr, dt, 0, "NAGR")

                        Dim nHKUTU As Integer
                        Dim nPSAYI As Integer

                        GetRowInfo(nHKUTU, dt, 0, "HKUTU")
                        GetRowInfo(nPSAYI, dt, 0, "PSAYI")

                        nKapAdet = nHKUTU + nPSAYI

                        GetRowInfo(nHacim, dt, 0, "HCM")

                    End If

                    sKumulDuzeyi = CInt(sLookup("isnull(Uf_KumulDuzeyi,0)", "Customer", " Cust_num=" & sTirnakEkle(sCustomer) & " AND Cust_Seq=" & sCustomerSeq))

                    ' *********************************** DESADV D97 A  ****************************
                    ' Opel, Opel Yedek Parça, Saab Yedek Parça ,Robert Bosch,Lear, Delphi
                    ' AsnTipkontrol

                    Select Case sASNTip

                        Case "OPEL", "SAAB-YDK", _
                                "OPEL-YDK", "SKODA", _
                                "LEAR", "RBOSCH", _
                                "VDO", "DELPHI", "GMUS"

                            MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant

                            If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then
                                Directory.CreateDirectory(sASNDir + "\" + sSDRODID)
                            End If

                            If (File.Exists(MesajFile)) Then
                                File.Delete(MesajFile)
                            End If

                            objWriter = New System.IO.StreamWriter(MesajFile)

                            nCps = 0

                            ' FormatDateTime('yyMMdd',date)+' '+FormatDateTime('HHmm',time)+' '+

                            sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") + " " & StrFill(sRCVODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")

                            objWriter.WriteLine(sLine)

                            sSQL = " SELECT * " & _
                                        " FROM SHPPACK" & _
                                        " WHERE SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & _
                                        " AND isnull(ASNDRM,0)<>9"

                            sSQL = sSQL & " AND PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                            dt = db.RunSql(sSQL)

                            nMsgNo = 1

                            nUNH = 0

                            nLin = 1

                            nPickList = Trim(Convert.ToString(row.Cells("PIckno").Text))

                            sFatura = Trim(Convert.ToString(row.Cells("InvNo").Text))

                            For Each rowDt As DataRow In dt.Rows

                                nRff = 1

                                sGate = Trim(rowDt.Item("KAPI").ToString)

                                sMPKOD = ""

                                sMKKOD = ""
                                sMSPKOD = ""
                                sMKPKOD = ""
                                sMURNKOD = ""
                                ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, rowDt.Item("ITNBR").ToString, rowDt.Item("AMBKOD").ToString)

                                ' Müşteri Ürün Kodu, Adı
                                MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                nSatir = 1

                                If (nLin > 1) AndAlso (sASNTip = "GMUS") Then

                                    GoTo GMUS

                                End If

                                sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("DESADV", 6, " ", "R") + " " & StrFill("D"c, 3, " ", "R") + " " & StrFill("97A", 3, " ", "R") + " " & StrFill("UN", 2, " ", "R")

                                If sASNTip = "VDO" Then

                                    sLine = sLine + " " & StrFill("A01051", 6, " ", "R")

                                End If

                                objWriter.WriteLine(sLine)

                                nSatir += 1

                                nUNH += 1

                                If sASNTip = "GMUS" Then

                                    sBGM = ""

                                Else

                                    sBGM = "351"

                                End If

                                If sLieferDrm = "0" Then

                                    sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(nPickList, 35, " ", "R")

                                ElseIf sLieferDrm = "1" Then

                                    sLine = "BGM " & StrFill(sBGM, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 35, " ", "R") + " " & StrFill(sFatura, 35, " ", "R")

                                End If

                                If sASNTip <> "VDO" Then

                                    sLine = sLine & StrFill("", 18, " ", "R") & StrFill("9"c, 3, " ", "R")

                                End If

                                objWriter.WriteLine(sLine)

                                nSatir += 1

                                If (sASNTip = "OPEL-YDK") Then

                                    sDTM = Now.Date.ToString("yyyyMMdd")

                                    sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDTM, 35, " ", "R") & " " & StrFill("102", 3, " ", "R")

                                Else

                                    sDTM = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                                    sLine = "DTM " & StrFill("137", 3, " ", "R") & " " & StrFill(sDTM, 35, " ", "R") & " " & StrFill("203", 3, " ", "R")

                                End If

                                objWriter.WriteLine(sLine)

                                nSatir += 1

                                If sASNTip <> "VDO" Then

                                    sDTM = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                                    sLine = "DTM " & StrFill("11", 3, " ", "R") + " " & StrFill(sDTM, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                End If

                                If sASNTip = "DELPHI" Then

                                    sDTM = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                                    sLine = "DTM " & StrFill("132", 3, " ", "R") + " " & StrFill(sDTM, 35, " ", "R") + " " & StrFill("203", 3, " ", "R")

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                End If
                                ' ********** GROSS WEIGHT *********
                                If (sASNTip = "RBOSCH") Then
                                    sMEA = "WT"
                                Else
                                    sMEA = "AAX"
                                End If
                                If (sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "SAAB-YDK") OrElse (sASNTip = "OPEL-YDK") OrElse (sASNTip = "GMUS") Then
                                    sMEAC = "G"
                                ElseIf (sASNTip = "RBOSCH") OrElse (sASNTip = "LEAR") OrElse (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") OrElse (sASNTip = "VW2") Then
                                    sMEAC = "AAD"
                                End If
                                If (sASNTip = "GMUS") Then
                                    sMEAU = "LBR"
                                Else
                                    sMEAU = "KGM"
                                End If
                                If sASNTip = "RENAULT" Then
                                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 80, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
                                ElseIf sASNTip = "VDO" Then
                                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, " ", "R")
                                ElseIf sASNTip = "GMUS" Then
                                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nBrtAgr * 2.2046)), 18, "0", "L")
                                Else
                                    sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nBrtAgr), 18, "0", "L")
                                End If

                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' ********** NET WEIGHT *********
                                If (sASNTip <> "OPEL-YDK") AndAlso (sASNTip <> "RENAULT") AndAlso (sASNTip <> "VDO") Then
                                    If (sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "OPEL-YDK") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "SAAB-YDK") OrElse (sASNTip = "GMUS") Then
                                        sMEAC = "N"
                                    ElseIf (sASNTip = "RBOSCH") Then
                                        sMEAC = "AAF"
                                    ElseIf (sASNTip = "LEAR") Then
                                        sMEAC = "AAC"
                                    ElseIf (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "VW2") Then
                                        sMEAC = "AAL"
                                    End If
                                    If (sASNTip <> "GMUS") Then
                                        sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nNetAgr), 18, "0", "L")
                                    Else
                                        sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(Math.Round(nNetAgr * 2.2046)), 18, "0", "L")
                                    End If
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                ' ********** KAP ADEDI *********
                                If (sASNTip <> "OPEL-YDK") AndAlso (sASNTip <> "RENAULT") AndAlso (sASNTip <> "VDO") AndAlso (sASNTip <> "SAAB-YDK") Then
                                    ' 'SAAB-YDK' da MEA SQ segmentinin kaldırılması istendi. KADER 15/03/2005 Salı
                                    If (sASNTip = "RBOSCH") Then
                                        sMEA = "CT"
                                    Else
                                        sMEA = "AAX"
                                    End If
                                    If (sASNTip = "LEAR") Then
                                        sMEAC = "ABJ"
                                    Else
                                        sMEAC = "SQ"
                                    End If
                                    If (sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "OPEL-YDK") OrElse (sASNTip = "SAAB-YDK") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "GMUS") Then
                                        sMEAU = "C62"
                                    ElseIf (sASNTip = "RBOSCH") OrElse (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "VW2") Then
                                        sMEAU = "NMP"
                                    ElseIf (sASNTip = "LEAR") Then
                                        sMEAU = "MTQ"
                                    End If
                                    If (sASNTip <> "LEAR") Then
                                        sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nKapAdet), 18, "0", "L")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    If (sASNTip = "LEAR") Then
                                        sLine = "MEA " & StrFill(sMEA, 3, " ", "R") & " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(nHacim), 18, "0", "L")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                End If
                                If (sASNTip <> "LEAR") AndAlso (sASNTip <> "RENAULT") AndAlso (sASNTip <> "VDO") Then
                                    If (sASNTip = "RBOSCH") Then
                                        sRFF = "SRN"
                                    ElseIf (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "VW2") Then
                                        sRFF = "AAO"
                                    Else
                                        sRFF = "CN"
                                    End If
                                    sLine = "RFF " & StrFill(sRFF, 3, " ", "R") & " " & StrFill(sCarrier, 35, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "SKODA") Then
                                    sLine = "NAD " & StrFill("BY", 3, " ", "R") & " " & StrFill("SKODA", 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip <> "OPEL-YDK") AndAlso (sISSUER <> "") Then
                                    sLine = "NAD " & StrFill("MI", 3, " ", "R") & " " & StrFill(sISSUER, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If sASNTip = "RENAULT" Then

                                    sNAD = "SE"
                                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    sLine = "RFFA" & StrFill("ADE", 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If sASNTip = "VDO" Then
                                    sNAD = "CZ"
                                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sNAD = "SE"
                                    sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "RBOSCH") Then
                                    sNAD = "DP"
                                ElseIf (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") OrElse (sASNTip = "VW2") Then
                                    sNAD = "CN"
                                Else
                                    sNAD = "ST"
                                End If
                                If sASNTip <> "RENAULT" Then
                                    If sASNTip = "OPEL-YDK" Then
                                        ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sPlant,35,' ','R')+StrFill('',5,' ','R')+StrFill('92',3,' ','R')
                                        sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                                    Else
                                        sLine = "NAD " & StrFill(sNAD, 3, " ", "R") & " " & StrFill(sPlant, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                                    End If
                                Else
                                    If sPlant = "TU" Then
                                        sNAD1 = "09317801299870101"
                                    ElseIf sPlant = "TK" Then
                                        sNAD1 = "09317801299870102"
                                    ElseIf sPlant = "BU" Then
                                        sNAD1 = "09317801299870104"
                                    ElseIf sPlant = "CI" Then
                                        sNAD1 = "09459813295CI"
                                    ElseIf sPlant = "SK" Then
                                        sNAD1 = "09317801299870007"
                                    End If
                                    ' sLine:='NAD '+StrFill(sNAD,3,' ','R')+' '+StrFill(sNAD1,35,' ','R')+StrFill('',189,' ','R')+StrFill(sPlant,35,' ','R')
                                    sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sNAD1, 35, " ", "R") & StrFill("", 189, " ", "R") & StrFill(sPlant, 35, " ", "R")
                                End If
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = ("LOC " & StrFill("11", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                If (sASNTip <> "RENAULT") AndAlso (sASNTip <> "VDO") Then
                                    If (sASNTip = "RBOSCH") Then
                                        sNAD = "SE"
                                    Else
                                        sNAD = "SU"
                                    End If
                                    If sASNTip = "OPEL-YDK" Then
                                        sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")
                                    Else
                                        sLine = ("NAD " & StrFill(sNAD, 3, " ", "R")) + " " & StrFill(sDUNS, 35, " ", "R") & StrFill("", 5, " ", "R") & StrFill("16", 3, " ", "R")
                                    End If
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "LEAR") Then
                                    sLine = ("CTA " & StrFill("SU", 3, " ", "R")) + " " & StrFill("", 19, " ", "R") & StrFill(sISSUER, 35, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "LEAR") OrElse (sASNTip = "RBOSCH") Then
                                    If (sASNTip = "RBOSCH") Then
                                        sTOD = "6"
                                    Else
                                        sTOD = ""
                                    End If
                                    sLine = ("TOD " & StrFill(sTOD, 3, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sTermCode, 3, " ", "R") & StrFill("", 9, " ", "R") & StrFill("", 70, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "RBOSCH") Then
                                    sConv = sPlaka
                                Else
                                    sConv = ""
                                End If
                                If (sASNTip = "RBOSCH") Then
                                    sTDT1 = sTermCode
                                Else
                                    sTDT1 = ""
                                End If
                                If (sASNTip = "RBOSCH") Then
                                    sTDT2 = "31"
                                Else
                                    sTDT2 = ""
                                End If
                                If (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "VW2") Then
                                    sTDT2 = "9"c
                                End If
                                If (sASNTip = "RBOSCH") Then
                                    sTDT3 = sCarrier
                                Else
                                    sTDT3 = ""
                                End If
                                If (sASNTip <> "RBOSCH") Then
                                    sTDT4 = sCarrier
                                Else
                                    sTDT4 = ""
                                End If
                                ' if (sMKod='LR') then sTrnQual:='';
                                If sASNTip = "VDO" Then

                                    sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R")
                                ElseIf sASNTip = "GMUS" Then

                                    sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("182", 3, " ", "R")
                                Else

                                    sLine = "TDT " & StrFill(sTrnQual, 3, " ", "R") + " " & StrFill(sConv, 17, " ", "R") + " " & StrFill(sDHAABZ, 3, " ", "R") + " " & StrFill(sTDT1, 17, " ", "R") + " " & StrFill(sTDT2, 8, " ", "R") + " " & StrFill(sTDT3, 17, " ", "R") + " " & StrFill(sTDT4, 17, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("92", 3, " ", "R")
                                End If
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                If (sASNTip = "RBOSCH") Then
                                    sLine = ("LOCB" & StrFill("5", 3, " ", "R")) + " " & StrFill(sGate, 25, " ", "R") & StrFill("", 5, " ", "R") & StrFill("91", 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "SAAB-YDK") OrElse (sASNTip = "OPEL-YDK") OrElse (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "GMUS") OrElse (sASNTip = "VW2") Then
                                    If sPlaka = "" Then
                                        sPlaka = "UNKNOWN"
                                    End If

                                    sLine = ("EQD " & StrFill("TE", 3, " ", "R")) + " " & StrFill(sPlaka, 17, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If

                                nMiktar = rowDt.Item("SHIPMIK").ToString

                                nKutuSayi = rowDt.Item("KSAY").ToString

                                UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                            rowDt.Item("ITNBR").ToString, _
                                            sGate, sKumulDuzeyi, nMiktar, _
                                            rowDt.Item("SHPNO").ToString, _
                                            rowDt.Item("SHIPTO").ToString, _
                                            rowDt.Item("PICKNO").ToString)

GMUS:
                                nCps += 1

                                If (sASNTip = "RBOSCH") Then

                                    sLine = ("CPS " & StrFill("1", 12, " ", "R")) & StrFill("", 14, " ", "R")

                                ElseIf (sASNTip = "GMUS") Then

                                    sLine = ("CPS " & StrFill(Convert.ToString(nCps), 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("4", 3, " ", "R")

                                Else

                                    sLine = ("CPS " & StrFill("1", 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("1", 3, " ", "R")

                                End If

                                objWriter.WriteLine(sLine)

                                nSatir += 1

                                If (sASNTip = "RBOSCH") Then

                                    sLine = ("PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L")) & StrFill("3"c, 13, " ", "R") & StrFill("PA", 17, " ", "R") & StrFill("", 45, " ", "R") & StrFill("X"c, 3, " ", "R") + " " & StrFill(sMKKOD, 36, " ", "R") & StrFill("BP", 3, " ", "R") & StrFill(rowDt.Item("PSAY").ToString, 35, " ", "R") & StrFill("SA", 3, " ", "R")

                                ElseIf (sASNTip <> "GMUS") Then

                                    sLine = ("PAC " & StrFill(Convert.ToString(nKutuSayi), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKKOD, 17, " ", "R")

                                End If

                                If sASNTip = "VDO" Then

                                    sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                                End If

                                If (sASNTip <> "GMUS") Then

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                End If

                                If (sASNTip = "RBOSCH") Then

                                    sLine = ("MEAB" & StrFill("LL", 3, " ", "R")) + " " & StrFill("HM", 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill("NMP", 3, " ", "R") + " " & StrFill(Convert.ToString(0), 18, "0", "L")

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                End If

                                sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                If sBirim = "AD" Then
                                    sQTYU = "PCE"
                                ElseIf sBirim = "KG" Then
                                    sQTYU = "KGM"
                                ElseIf sBirim = "MT" Then
                                    sQTYU = "MTR"
                                End If
                                If (sASNTip = "OPEL-YDK") Then
                                    sQTYU = "C62"
                                ElseIf (sASNTip = "RBOSCH") Then
                                    sQTYU = ""
                                End If
                                If (sASNTip = "RBOSCH") OrElse (sASNTip = "LEAR") OrElse (sASNTip = "OPEL-YDK") OrElse (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "VW2") Then

                                    sLine = "QTY " & StrFill("52", 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 12, "0", "L") & ".000" + " " & StrFill(sQTYU, 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") OrElse (sASNTip = "DELPHI") Then
                                    ' KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma

                                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                                    ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "RENAULT") Then

                                    sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") OrElse (sASNTip = "DELPHI") Then
                                    ' KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                                    If Convert.ToInt64(nKutuSayi) > 99 Then
                                        For jj = 1 To (Convert.ToInt64(nKutuSayi) / 99)
                                            For nGCount = 1 To 99

                                                sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                                                If sASNTip = "DELPHI" Then

                                                    ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                                                    sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                                                Else

                                                    sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                                                End If
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            Next
                                            If jj <> (Convert.ToInt64(nKutuSayi) / 99) Then

                                                sLine = "PCI " & StrFill("17", 3, " ", "R")
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            Else
                                                If (Convert.ToInt64(nKutuSayi) Mod 99) <> 0 Then

                                                    sLine = "PCI " & StrFill("17", 3, " ", "R")
                                                    objWriter.WriteLine(sLine)
                                                    nSatir += 1
                                                End If
                                            End If
                                        Next
                                        For nGCount = 1 To (Convert.ToInt64(nKutuSayi) Mod 99)

                                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                                            If sASNTip = "DELPHI" Then

                                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                                            Else

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        Next
                                    Else
                                        For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                                            If sASNTip = "DELPHI" Then

                                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                                            Else

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        Next
                                    End If
                                    nRff += 1
                                End If
                                If (sASNTip = "RBOSCH") Then

                                    sLine = ("LIN " & StrFill(CStr(nLin), 6, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") + " " & StrFill("IN", 3, " ", "R") & StrFill("", 5, " ", "R") & StrFill("92", 3, " ", "R")

                                ElseIf (sASNTip = "OPEL-YDK") Then

                                    sLine = ("LIN " & StrFill(CStr(nLin), 6, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") + " " & StrFill("IN", 3, " ", "R")

                                ElseIf (sASNTip = "VDO") Then

                                    sLine = ("LIN " & StrFill(CStr(nLin), 6, " ", "R")) & StrFill("", 5, " ", "R") & StrFill(sMURNKOD, 35, " ", "R") + " " & StrFill("IN", 3, " ", "R") & StrFill("", 20, " ", "R") & StrFill("0", 2, " ", "R")

                                Else

                                    sLine = ("LIN " & StrFill("", 11, " ", "R")) & StrFill(sMURNKOD, 35, " ", "R") + " " & StrFill("IN", 3, " ", "R")

                                End If

                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                nLin += 1
                                ' ********** NET WEIGHT *********
                                If (sASNTip = "VDO") Then

                                    sMEA = "PD"

                                    If (sASNTip = "VDO") Then

                                        sMEAC = "AAL"

                                    End If

                                    sLine = ("MEAC" & StrFill(sMEA, 3, " ", "R")) + " " & StrFill(sMEAC, 3, " ", "R") & StrFill("", 94, " ", "R") & StrFill(sMEAU, 3, " ", "R") + " " & StrFill(Convert.ToString(rowDt.Item("NETAGR").ToString), 18, " ", "R")

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                End If
                                If (sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "SAAB-YDK") OrElse (sASNTip = "OPEL-YDK") OrElse (sASNTip = "GMUS") Then
                                    sQTYU = "C62"
                                End If
                                If (sASNTip = "OPEL") OrElse (sASNTip = "DELPHI") OrElse (sASNTip = "SAAB-YDK") OrElse (sASNTip = "SKODA") OrElse (sASNTip = "VW") OrElse (sASNTip = "AUDI") OrElse (sASNTip = "RENAULT") OrElse (sASNTip = "GMUS") OrElse (sASNTip = "VW2") Then

                                    sLine = (("QTYA" & StrFill("3"c, 3, " ", "R")) + " " & StrFill(Convert.ToString(nKumMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If

                                sLine = (("QTYA" & StrFill("12", 3, " ", "R")) + " " & StrFill(Convert.ToString(nMiktar), 12, "0", "L") & ".000") + " " & StrFill(sQTYU, 3, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                If (sASNTip = "OPEL-YDK") OrElse (sASNTip = "VDO") Then

                                    sLine = "ALIA" & StrFill("TR", 3, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If

                                sLine = ("RFFC" & StrFill("ON", 3, " ", "R")) + " " & StrFill(sKontrat, 35, " ", "R")

                                objWriter.WriteLine(sLine)

                                nSatir += 1
                                ' Palet
                                If rowDt.Item("PSAY").ToString <> 0 Then

                                    If (sASNTip <> "GMUS") Then

                                        sLine = ("CPS " & StrFill("2"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                        sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMPKOD, 17, " ", "R")

                                        If sASNTip = "VDO" Then

                                            sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                                        End If

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                    End If
                                    ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                                    If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") Then
                                        ' KADER Delphi'de(DELPHI) PCI sadece ambalajlarda kaldırıldı 08/06/2005 Carsamba

                                        sLine = "PCI " & StrFill("17", 3, " ", "R")
                                        ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    If (sASNTip = "RENAULT") Then

                                        sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                                    If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") Then
                                        ' KADER Delphi'de(DELPHI) GIR sadece ambalajlarda kaldırıldı 08/06/2005 Carcamba
                                        For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                                            If sASNTip = "DELPHI" Then

                                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                                            Else

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        Next
                                        nRff += 1
                                    End If
                                End If
                                ' Kapak

                                If rowDt.Item("KPMIK").ToString <> 0 Then
                                    If (sASNTip <> "GMUS") Then

                                        sLine = ("CPS " & StrFill("3"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("3"c, 3, " ", "R")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMKPKOD, 17, " ", "R")
                                        If sASNTip = "VDO" Then

                                            sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                                    If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") Then
                                        ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı  08/06/2005 carsamba

                                        sLine = "PCI " & StrFill("17", 3, " ", "R")
                                        ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    If (sASNTip = "RENAULT") Then

                                        sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Salı
                                    If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") Then
                                        ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                                        For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                                            If sASNTip = "DELPHI" Then

                                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 08/03/2005 Salı

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                                            Else

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        Next
                                        nRff += 1
                                    End If
                                End If
                                ' Separator

                                If rowDt.Item("SPMIK").ToString <> 0 Then
                                    If (sASNTip <> "GMUS") Then

                                        sLine = ("CPS " & StrFill("4"c, 12, " ", "R")) & StrFill("", 14, " ", "R") & StrFill("2"c, 3, " ", "R")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("PAC " & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString), 8, "0", "L")) & StrFill("", 13, " ", "R") & StrFill(sMSPKOD, 17, " ", "R")

                                        If sASNTip = "VDO" Then

                                            sLine = sLine & StrFill("", 5, " ", "R") & StrFill("92", 17, " ", "R")

                                        End If

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                    End If

                                    ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then // KADER Delphi'de(DELPHI) PCI segmenti yokmus.  11/03/2005 Cuma
                                    If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") Then
                                        ' KADER Delphi'de(DELPHI) PCI sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba

                                        sLine = "PCI " & StrFill("17", 3, " ", "R")
                                        ' if sASNTip='VDO' then sLine:=sLine+StrFill('',365,' ','R')+StrFill('1',3,' ','R')+StrFill('',5,' ','R')+StrFill('10',3,' ','R');
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    If (sASNTip = "RENAULT") Then

                                        sLine = ("RFFB" & StrFill("AAT", 3, " ", "R")) + " " & StrFill(CStr(nRff), 1, " ", "R")
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If
                                    ' if (sASNTip='RENAULT') or (sASNTip='VDO') or (sASNTip='DELPHI') then  // KADER Delphi'de(DELPHI) GIR segmenti yokmus.  11/03/2005 Cuma
                                    If (sASNTip = "RENAULT") OrElse (sASNTip = "VDO") Then
                                        ' KADER Delphi'de(DELPHI) GIR sadece ambalajlardan kaldırıldı.  08/06/2005 Carsamba
                                        For nGCount = 1 To Convert.ToInt64(nKutuSayi)

                                            sGir = StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(nGCount), 3, "0", "L")
                                            If sASNTip = "DELPHI" Then

                                                ' KADER Delphi'de bu segmentte ML yerine AW olması istendi 11/03/2005 Cuma

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("AW", 3, " ", "R")
                                            Else

                                                sLine = ("GIR " & StrFill("3"c, 3, " ", "R")) + " " & StrFill(sGir, 35, " ", "R") + " " & StrFill("ML", 3, " ", "R")
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        Next
                                        nRff += 1
                                    End If
                                End If

                                sLine = "UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L") + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
                                ' WriteLn(fMessages,sLine); inc(nSatir);
                                nMsgNo = nMsgNo + 1
                                ' end;

                                If chkAsnYeni.Checked Then

                                    sSQL = "UPDATE SHPPACK" & _
                                            " SET ASNDRM=1 " & _
                                            " ,ASNNO=" & Convert.ToString(nASN_No) & _
                                            " ,ASNMIK=" & Convert.ToString(nKumMiktar) & _
                                            " WHERE  CUST=" & sTirnakEkle(sCustomer) & _
                                            " AND SHIPTO=" & sTirnakEkle(sPlant) & _
                                            " AND KAPI=" & sTirnakEkle(sGate) & _
                                            " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString) & _
                                            " AND isnull(ASNDRM,0)<>9"

                                    sSQL = sSQL & " AND SHPNO=" & sTirnakEkle(rowDt.Item("SHPNO").ToString)

                                    db.RunSql(sSQL)

                                End If

                            Next rowDt

                            sLine = "UNZ " & StrFill(Convert.ToString(nUNH), 6, "0", "L") + " " & StrFill(sASN_No, 14, " ", "R")
                            ' WriteLn(fMessages,sLine); inc(nSatir);
                            ' ********************VW AUDI -- DESADV D98 A******************************
                            objWriter.Close()

                            'AsnTipkontrol
                        Case "PSA"
                            Try

                                'MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant
                                MesajFile = sASNDir + "\" + "A" + sASN_No + "." + sPlant
                                'If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then
                                If Not Directory.Exists(sASNDir) Then
                                    'Directory.CreateDirectory(sASNDir + "\" + sSDRODID)
                                    Directory.CreateDirectory(sASNDir)
                                End If


                                If (File.Exists(MesajFile)) Then
                                    File.Delete(MesajFile)
                                End If
                                objWriter = New System.IO.StreamWriter(MesajFile)
                                ' sLine:='UNB+UNOA:1+'+Trim(sRCVODID)+'+'+Trim(sASNODID)+'+'+FormatDateTime('yyMMdd',date)+':'+FormatDateTime('HHmm',time)+'+'+Trim(sASN_No)+'++++++1''';  Degistirildi 02/03/06

                                'O0013003189TEKLAS71  Now.Date.ToString("yyMMdd") + ":"c + Now.ToString("HHmm")


                                nSatir = 1

                                If sASNTip = "PSA" Then
                                    'sLine = "UNB+UNOA:1+" & sSDRODID & "+" & sASNODID.Trim() + "+" + Now.Date.ToString("yyMMdd") + ":"c + Now.ToString("HHmm") + "+" + sASN_No.Trim() & "++++++0'"
                                    sLine = "UNB+UNOA:1+0798000065SAHIN+" & sASNODID.Trim() + "+" + Now.Date.ToString("yyMMdd") + ":"c + Now.ToString("HHmm") + "+" + sASN_No.Trim() & "++++++0'"
                                    objWriter.Write(sLine)
                                Else
                                    sLine = "UNB+UNOA:1+0733002708PRSS+" & sASNODID.Trim() + "+" + Now.Date.ToString("yyMMdd") + ":"c + Now.ToString("HHmm") + "+" + sASN_No.Trim() & "++++++0'"
                                    objWriter.Write(sLine)
                                End If



                                sSQL = " SELECT * FROM " & "Shppack" & " WHERE SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & " AND isnull(ASNDRM,0)<>9"
                                sSQL = (sSQL & " AND PICKNO=") + Convert.ToString(row.Cells("PIckno").Text)

                                dt = db.RunSql(sSQL)

                                nMsgNo = 1
                                nUNH = 0
                                nRff = 1
                                nSatir += 1
                                nRffAat = 1
                                nGir = 1

                                nPickList = Trim(Convert.ToString(row.Cells("PIckno").Text))

                                sFatura = Trim(Convert.ToString(row.Cells("InvNo").Text))

                                sGate = ""

                                For Each rowDt As DataRow In dt.Rows

                                    If sGate <> dt.Rows(0).Item("KAPI").ToString Then
                                        '@ Unsupported property or method(A): 'cstr'
                                        sLine = "UNH+" & CStr(nMsgNo) & "+DESADV:D:96A:UN:A01052++1'"
                                        objWriter.Write(sLine)

                                        nSatir += 1
                                        nUNH += 1
                                        nMsgNo = nMsgNo + 1

                                        sBGM = "351"
                                        If (sLieferDrm = "0") OrElse (sLieferDrm = "") Then
                                            'sLine = ("BGM+351+" & dt.Rows(0).Item("SHPNO").ToString.Trim()) + "'"
                                            sLine = ("BGM+351+" & IIf(dt.Rows(0).Item("IRSNO").ToString.Trim() = "", nPickList, dt.Rows(0).Item("IRSNO").ToString.Trim())) + "'"
                                        ElseIf sLieferDrm = "1" Then
                                            sLine = ("BGM+351+" & sFatura.Trim()) + "'"
                                        End If
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        sLine = (("DTM+11:" & ConvertAS400DatetoStr(Now.Date.ToString)) + Now.ToString("HHmm") & ":203") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1


                                        sDTM = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")
                                        sLine = ("DTM+132:" & sDTM & ":203") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1


                                        sDTM = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")
                                        sLine = ("DTM+137:" & sDTM & ":203") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        ' ********** GROSS WEIGHT *********
                                        sMEA = "AAX"
                                        sMEAC = "AAD"
                                        sMEAU = "KGM"

                                        nBrtAgrRnd = Math.Round(nBrtAgr)

                                        sLine = ("MEA+AAX+AAD+KGM:" & Convert.ToString(nBrtAgrRnd).Trim()) + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1


                                        sDunsSifir = "00" & sDUNS.Trim() & "00"
                                        'sLine = ("NAD+CZ+" & sDunsSifir & "::92") + "'"
                                        sLine = ("NAD+CZ+" & sDunsSifir & "::5++SAHINKUL") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1


                                        If dt.Rows(0).Item("PUSNO").ToString.Contains("%") = True Then

                                            sPusNo = dt.Rows(0).Item("PUSNO").ToString.Trim().Substring(0, dt.Rows(0).Item("PUSNO").ToString.Trim().IndexOf("%"))

                                        Else
                                            If dt.Rows(0).Item("PUSNO").ToString = "" Then

                                                sPusNo = ""

                                            ElseIf dt.Rows(0).Item("PUSNO").ToString.Substring(0, 2) = "EO" Or
                                                dt.Rows(0).Item("PUSNO").ToString.Substring(0, 2) = "FO" Then

                                                sPusNo = ""
                                            Else
                                                GetRowInfo(sPusNo, dt, 0, "PUSNO")

                                            End If

                                        End If


                                        sLine = ("NAD+SE+" & sDunsSifir & "::5++SAHINKUL") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        'sLine = ("RFF+ADE:" & StrFill(sDUNS.Trim(), 8, "0", "L")) + "'"
                                        sLine = ("RFF+ADE:A00HTY'")
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        sNAD = "CN"
                                        sNAD1 = sLookup("DACEDI", "DACIAPF", " DACSHP=" & sTirnakEkle(sPlant))
                                        '15.10.2021
                                        'sLine = ("NAD+" & sNAD.Trim()) + "+" + sNAD1.Trim() & "::10'"
                                        'sLine = ("NAD+" & sNAD.Trim()) + "+" + sPlant.Trim() & "::5++GEFCO MAROC SA'"
                                        sLine = ("NAD+" & sNAD.Trim()) + "+GEFCO::5" + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1


                                        GetRowInfo(sGate, dt, 0, "KAPI")

                                        sLine = ("LOC+11+" & sGate.Trim()) + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1


                                        sTDT4 = sCarrier
                                        ' 
                                        ' if sPlaka='' then sPlaka:='UNKNOWN';
                                        ' sLine:='EQD+TE+'+Trim(sPlaka)+'''';
                                        ' Write(fMessages,sLine); inc(nSatir);
                                        ' 
                                        If sPlaka = "" Then
                                            sPlaka = "UNKNOWN"
                                        End If
                                        sLine = ("EQD+TE+" & sPlaka.Trim()) + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                    End If
                                    sMPKOD = ""
                                    sMKKOD = ""
                                    sMSPKOD = ""
                                    sMKPKOD = ""
                                    sMURNKOD = ""
                                    ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                    MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, rowDt.Item("ITNBR").ToString, rowDt.Item("AMBKOD").ToString)

                                    ' Müşteri Ürün Kodu, Adı
                                    MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                    nMiktar = rowDt.Item("SHIPMIK").ToString

                                    nKutuSayi = rowDt.Item("KSAY").ToString

                                    UpdateKumul2(nKumMiktar, sCustomer, sPlant, _
                                                rowDt.Item("CUST_SEQ").ToString, _
                                                rowDt.Item("ITNBR").ToString, _
                                                sGate, sKumulDuzeyi, nMiktar, _
                                                rowDt.Item("SHPNO").ToString, _
                                                rowDt.Item("SHIPTO").ToString, _
                                                rowDt.Item("PICKNO").ToString)


                                    sLine = "CPS+" & CStr(nRff) & "++" & "1'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                    nRff += 1

                                    'YENI

                                    Dim ht As New Hashtable
                                    Dim dtTemp1 As New DataTable
                                    Dim dtTemp2 As New DataTable
                                    Dim dtTemp3 As New DataTable


                                    ht.Add("@Shpno", nShipNo)
                                    ht.Add("@Pickno", nPickList)
                                    ht.Add("@Shipto", row.Cells("ShIpto").Text)
                                    ht.Add("@Itnbr", rowDt.Item("ITNBR").ToString)

                                    dtTemp = db.RunSp2("TRM_PaletBilgisi", ht).Tables(0)

                                    nMiktar = 0

                                    nKutuSayi = 0

                                    For Each rowPalet As DataRow In dtTemp.Rows

                                        nKutuSayi = nKutuSayi + rowPalet.Item("KSAY").ToString

                                    Next

                                    sLine = "PAC+" & Convert.ToString(nKutuSayi) & "++" + sMKKOD.Trim() & "::92'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                    sBirim = Convert.ToString(rowDt.Item("STKOB").ToString)

                                    If sBirim = "AD" Then
                                        sQTYU = "PCE"
                                    ElseIf sBirim = "KG" Then
                                        sQTYU = "KGM"
                                    ElseIf sBirim = "MT" Then
                                        sQTYU = "MTR"
                                    End If


                                    sLine = "QTY+52:" & rowDt.Item("KMIK").ToString.Trim() & ":" & sQTYU.Trim() & "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    For Each rowPalet As DataRow In dtTemp.Rows

                                        nMiktar = nMiktar + rowPalet.Item("SEVKMIK").ToString


                                        If rowPalet.Item("SIRA").ToString = 1 Then

                                            sSQL = "SELECT * " &
                                                                " FROM ETIKETDTY" + " " &
                                                                " WHERE  (pltno = '' Or pltno=0 or pltno is null)" &
                                                                " AND KUTUETK=1" &
                                                                " AND itnbr = '" & rowPalet.Item("ITNBR").ToString & "' " &
                                                                " AND pickno = " & rowDt.Item("PICKNO").ToString & " "

                                            dtPaletYok = db.RunSql(sSQL)

                                            For Each rowDtPaletYok As DataRow In dtPaletYok.Rows

                                                If rowDtPaletYok.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                                    sPusNo = Trim(Copy(rowDtPaletYok.Item("PUSNO").ToString, rowDtPaletYok.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd())

                                                Else

                                                    sPusNo = ""

                                                End If

                                                'If sPusNo = "" Then

                                                'sLine = "GIR+3+" & StrFill(rowDtPaletYok.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML'"

                                                sLine = "GIR+3+" & rowDtPaletYok.Item("ETKSERINO").ToString & ":ML'"
                                                'Else

                                                '    'sLine = "GIR+3+" & StrFill(rowDtPaletYok.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML++" + sPusNo & ":BU'"
                                                '    sLine = "GIR+3+" & rowDtPaletYok.Item("ETKSERINO").ToString & ":ML++" + sPusNo & ":BU'"
                                                'End If

                                                objWriter.Write(sLine)
                                                nSatir += 1
                                                nGir += 1

                                            Next rowDtPaletYok

                                        ElseIf rowPalet.Item("SIRA").ToString = 2 Then

                                            sLine = "PCI+17'"
                                            objWriter.Write(sLine)
                                            nSatir += 1

                                            sLine = "RFF+AAT:" & rowPalet.Item("PLTNO").ToString + "'"
                                            objWriter.Write(sLine)
                                            nSatir += 1
                                            nRffAat += 1

                                            sSQL = "SELECT * " &
                                                                " FROM ETIKETDTY" & " " &
                                                                " WHERE  pltno='" & rowPalet.Item("PLTNO").ToString & "'" &
                                                                " And KutuEtk=1" &
                                                                " AND itnbr = '" & rowPalet.Item("ITNBR").ToString & "'" &
                                                                " AND pickno = " & rowPalet.Item("PICKNO").ToString &
                                                            " Order By Etkserino"

                                            dtTemp1 = db.RunSql(sSQL)

                                            For Each rowDtTemp As DataRow In dtTemp1.Rows

                                                If rowDtTemp.Item("PUSNO").ToString.Contains("%") Then

                                                    sPusNo = Trim(Copy(rowDtTemp.Item("PUSNO").ToString, rowDtTemp.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd())

                                                Else

                                                    sPusNo = ""

                                                End If

                                                'If sPusNo = "" Then

                                                'sLine = "GIR+3+" & StrFill(rowDtTemp.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML'"
                                                sLine = "GIR+3+" & rowDtTemp.Item("ETKSERINO").ToString & ":ML'"

                                                'Else

                                                ''sLine = "GIR+3+" & StrFill(rowDtTemp.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML++" + sPusNo & ":BU'"
                                                'sLine = "GIR+3+" & rowDtTemp.Item("ETKSERINO").ToString & ":ML++" + sPusNo & ":BU'"

                                                'End If

                                                objWriter.Write(sLine)
                                                nSatir += 1
                                                nGir += 1

                                            Next rowDtTemp

                                        ElseIf rowPalet.Item("SIRA").ToString = 3 Then

                                            nPaletKutuAdedi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                            If rowDt.Item("PSAY").ToString <> 0 Then

                                                If rowDt.Item("HKSAY").ToString <> 0 Then

                                                    For k = 1 To rowDt.Item("HKSAY").ToString

                                                        sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                                        If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then
                                                            sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()
                                                        Else
                                                            sPusNo = ""
                                                        End If

                                                        'If sPusNo = "" Then
                                                        'sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"
                                                        sLine = "GIR+3+" & sGir & ":ML'"
                                                        'Else
                                                        '    'sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++" & sPusNo & ":BU'"
                                                        '    sLine = "GIR+3+" & sGir & ":ML++" & sPusNo & ":BU'"
                                                        'End If

                                                        objWriter.Write(sLine)
                                                        nSatir += 1
                                                        nGir += 1

                                                    Next

                                                End If


                                                For k = 1 To CInt(Convert.ToString(rowDt.Item("PSAY").ToString))

                                                    sLine = "PCI+17'"
                                                    objWriter.Write(sLine)
                                                    nSatir += 1

                                                    sLine = "RFF+AAT:" & CStr(nRffAat) & "'"
                                                    objWriter.Write(sLine)
                                                    nSatir += 1
                                                    nRffAat += 1

                                                    For nGCount = 1 To rowDt.Item("PKMIK").ToString

                                                        If nPaletKutuAdedi <> 0 Then

                                                            nPaletKutuAdedi = nPaletKutuAdedi - 1

                                                            sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                                            If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then
                                                                sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()
                                                            Else
                                                                sPusNo = ""
                                                            End If

                                                            'If sPusNo = "" Then
                                                            'sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"
                                                            sLine = "GIR+3+" & sGir & ":ML'"
                                                            'Else
                                                            '    'sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"
                                                            '    sLine = ("GIR+3+" & sGir & ":ML++") + sPusNo & ":BU'"
                                                            'End If

                                                            objWriter.Write(sLine)
                                                            nSatir += 1
                                                            nGir += 1

                                                        End If

                                                    Next

                                                Next

                                            Else

                                                For nGCount = 1 To rowDt.Item("KSAY").ToString

                                                    sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                                    If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then
                                                        sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()
                                                    Else
                                                        sPusNo = ""
                                                    End If

                                                    'If sPusNo = "" Then
                                                    'sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"
                                                    sLine = "GIR+3+" & sGir & ":ML'"
                                                    'Else
                                                    ''sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"
                                                    'sLine = ("GIR+3+" & sGir & ":ML++") + sPusNo & ":BU'"
                                                    'End If

                                                    objWriter.Write(sLine)
                                                    nSatir += 1
                                                    nGir += 1

                                                Next

                                            End If

                                        End If

                                    Next


                                    sLine = "LIN+++" & RTrim(sMURNKOD) & ":IN++0'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sLine = ("QTY+12:" & Convert.ToString(nMiktar)) + ":" + sQTYU + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sLine = "ALI+TR'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sLine = ("RFF+ON:" & sKontrat.Trim()) + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    UpdateTRM_Shppack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString, nPickList)



                                Next rowDt


                                sLine = ("UNT+" & Convert.ToString(nSatir - 1)) + "+" + Convert.ToString(nMsgNo - 1) + "'"
                                objWriter.Write(sLine)
                                nSatir += 1

                                sLine = ("UNZ+" & Convert.ToString(nUNH)) + "+" + sASN_No + "'"
                                objWriter.Write(sLine)
                                nSatir += 1

                                objWriter.Close()

                            Catch ex As Exception
                                MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            Finally

                            End Try
                        Case "VW", "AUDI", "VW2"
                            Try

                                MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant

                                If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then
                                    Directory.CreateDirectory(sASNDir + "\" + sSDRODID)
                                End If

                                If (File.Exists(MesajFile)) Then
                                    File.Delete(MesajFile)
                                End If

                                objWriter = New System.IO.StreamWriter(MesajFile)

                                sLine = "UNB+UNOA:2+" & sRCVODID & "+" & sASNODID & "+" & Now.Date.ToString("yyMMdd") & ":" & Now.ToString("HHmm") & "+" & sASN_No & "'"
                                objWriter.WriteLine(sLine)
                                nMsgNo = 1
                                nLine = 0
                                nRff = 1

                                sSQL = " SELECT * FROM " & "SHPPACK" & _
                                        " WHERE  SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & _
                                        " AND isnull(ASNDRM,0)<>9"

                                sSQL = sSQL & " AND PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                                nMsgNo = 1
                                nUNH = 0
                                nSatir = 1

                                nPickList = Convert.ToString(row.Cells("PIckno").Text)

                                sFatura = Convert.ToString(row.Cells("InvNo").Text)

                                dt = db.RunSql(sSQL)

                                GetRowInfo(sGate, dt, 0, "KAPI")

                                nBrtAgrVda = Math.Round(nBrtAgr)

                                nNetAgrVda = Math.Round(nNetAgr)

                                If nMsgNo = 1 Then
                                    '@ Unsupported property or method(A): 'cstr'
                                    sLine = "UNH+" & CStr(nMsgNo) & "+DESADV:D:98A:UN" + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    nUNH += 1
                                    nMsgNo += 1
                                    If sLieferDrm = "0" Then
                                        sLine = "BGM+351+" & nPickList & "::+9" + "'"
                                    ElseIf sLieferDrm = "1" Then
                                        sLine = "BGM+351+" & sFatura & "::+9" + "'"
                                    End If

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                    sDTM = Now.Date.ToString("yyyyMMdd") + Now.ToString("HHmm")

                                    sLine = "DTM+137:" & sDTM & ":203" + "'"

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                    sLine = "DTM+11:" & sDTM & ":203" + "'"

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                    nYolSure = nLookup("TTIME", "PLANTPRM", " CANB=" & sTirnakEkle(Convert.ToString(dt.Rows(0).Item("CUST").ToString)) & _
                                                                            " AND B9CD=" & sTirnakEkle(dt.Rows(0).Item("SHIPTO").ToString))

                                    sDTM = DateAdd(DateInterval.Day, nYolSure, Now.Date).ToString("yyyyMMdd") + Now.ToString("HHmm")

                                    sLine = "DTM+191:" & sDTM & ":203" + "'"

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1
                                    ' ********** GROSS WEIGHT *********
                                    sMEA = "AAX"
                                    sMEAC = "AAD"
                                    sMEAU = "KGM"
                                    sLine = ("MEA+AAX+AAD+KGM:" & Convert.ToString(nBrtAgrVda)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = ("MEA+AAX+AAL+KGM:" & Convert.ToString(nNetAgrVda)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = ("MEA+AAX+SQ+NMP:" & Convert.ToString(nKapAdet)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    ' sLine:='RFF+AAO:'+FloattoStr(rowDt.Item('SHPNO').ToString)+'''';
                                    ' WriteLn(fMessages,sLine); inc(nSatir);
                                    sLine = "NAD+FW+" & sCarrier & "::92" & "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    If (sASNTip = "VW") OrElse (sASNTip = "VW2") Then
                                        sLine = "NAD+BY+VW::92" & "'"
                                    ElseIf (sASNTip = "AUDI") Then
                                        sLine = "NAD+BY+AUDI::92" & "'"
                                    End If
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = ("NAD+CN+" & sPlant & "::92") + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    If (sASNTip = "VW") OrElse (sASNTip = "VW2") Then

                                        sSQL = " SELECT USER2 " & _
                                                " FROM " & "KONTRTPF" & _
                                                " WHERE  CUST=" & sTirnakEkle(sCustomer) & " " & _
                                                " AND SHIPTO=" & sTirnakEkle(sPlant) & _
                                                " AND KAPI=" & sTirnakEkle(sGate) & _
                                                " AND BZMITM=" & sTirnakEkle(dt.Rows(0).Item("ITNBR").ToString)

                                        dtTemp = db.RunSql(sSQL)

                                        If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                                            sSevkYeri = Trim(dtTemp.Rows(0).Item("USER2").ToString)

                                        End If

                                        sLine = "LOC+7+" & sSevkYeri & "::92+X" & "'"

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                    End If
                                    If (sASNTip = "VW2") Then

                                        sLine = "NAD+CZ+" & sDUNS.Substring(0, sDUNS.Length - 1) & "::92" + "'"

                                    Else

                                        sLine = "NAD+CZ+" & sDUNS & "::92" + "'"

                                    End If

                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    If (sASNTip = "VW2") Then

                                        sLine = "LOC+9+" & sDUNS & "::92" & "'"

                                    Else

                                        sLine = "LOC+9+" & sDUNS & sUSERF2 & "::92" & "'"

                                    End If

                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = "TDT+" & sTrnQual & "++" & sDHAABZ & "+9+" & sCarrier & "::92+++UNKNOWN:146:5+1" & "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    If sPlaka = "" Then
                                        sPlaka = "UNKNOWN"
                                    End If
                                    sLine = ("EQD+TE+" & sPlaka & ":146") + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If

                                For Each rowDt As DataRow In dt.Rows

                                    sMPKOD = ""
                                    sMKKOD = ""
                                    sMSPKOD = ""
                                    sMKPKOD = ""
                                    sMURNKOD = ""
                                    ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                    MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, rowDt.Item("ITNBR").ToString, rowDt.Item("AMBKOD").ToString)

                                    ' Müşteri Ürün Kodu, Adı
                                    MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                    nMiktar = rowDt.Item("SHIPMIK").ToString

                                    'sBirim = sLookup("DDDHCD", "MBDDREP", "  DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                    sBirim = sLookup("STKOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                    If sBirim = "AD" Then
                                        sQTYU = "PCE"
                                    ElseIf sBirim = "KG" Then
                                        sQTYU = "KGM"
                                    ElseIf sBirim = "MT" Then
                                        sQTYU = "MTR"
                                    End If

                                    UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                                rowDt.Item("ITNBR").ToString, _
                                                sGate, sKumulDuzeyi, nMiktar, _
                                                rowDt.Item("SHPNO").ToString, _
                                                rowDt.Item("SHIPTO").ToString, _
                                                rowDt.Item("PICKNO").ToString)

                                    If (rowDt.Item("HKSAY").ToString <> 0) OrElse (rowDt.Item("PSAY").ToString = 0) Then

                                        sLine = ("CPS+" & StrFill(CStr(nRff), 6, "0", "L") & "++4") + "'"

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                        If (rowDt.Item("HKSAY").ToString <> 0) Then

                                            If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)) = "1" Then

                                                sLine = (("PAC+" & Convert.ToString(rowDt.Item("HKSAY").ToString) & "+::11+") + sMKKOD & "::92") + "'"
                                            Else

                                                sLine = (("PAC+" & Convert.ToString(rowDt.Item("HKSAY").ToString) & "+::2+") + sMKKOD & "::92") + "'"
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1

                                            sLine = ("QTY+52:" & Convert.ToString(rowDt.Item("KMIK").ToString)) + ":" + sQTYU + "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1

                                            sLine = "PCI+17+++S::10" & "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1

                                            If rowDt.Item("HKSAY").ToString = 1 Then

                                                sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + "'"

                                            Else

                                                sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + ":"c & StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(rowDt.Item("HKSAY").ToString), 3, "0", "L") + "'"

                                            End If

                                            objWriter.WriteLine(sLine)

                                            nSatir += 1

                                        ElseIf (rowDt.Item("PSAY").ToString = 0) Then

                                            If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)) = "1" Then

                                                sLine = (("PAC+" & Convert.ToString(rowDt.Item("KSAY").ToString) & "+::11+") + sMKKOD & "::92") + "'"

                                            Else

                                                sLine = (("PAC+" & Convert.ToString(rowDt.Item("KSAY").ToString) & "+::2+") + sMKKOD & "::92") + "'"

                                            End If

                                            sLine = ("QTY+52:" & Convert.ToString(rowDt.Item("KMIK").ToString)) + ":"c + sQTYU + "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1

                                            sLine = "PCI+17+++S::10" & "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1

                                            If rowDt.Item("KSAY").ToString = 1 Then

                                                sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + "'"

                                            Else

                                                sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + ":"c & StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(rowDt.Item("KSAY").ToString), 3, "0", "L") + "'"

                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        End If

                                        sLine = "LIN+++" & sMURNKOD & ":" & "IN" & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nLine += 1

                                        sLine = ("PIA+1+" & sKontrat & ":ON") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        ' Zeki 27.06.2007 'Mehmet Bey in isteği üzerine değişiklik yapıldı
                                        If sGate.Substring(1 - 1, 3) = "KOD" Then
                                            sLine = "IMD+C+66+::5" & "'"
                                        Else
                                            sLine = "IMD+C+63+::5" & "'"
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        ' 63

                                        nKutuSayi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                        If (rowDt.Item("HKSAY").ToString <> 0) Then

                                            sLine = ("QTY+1:" & Convert.ToString(nMiktar - (nKutuSayi * rowDt.Item("KMIK").ToString))) + ":"c + sQTYU + "'"

                                        ElseIf (rowDt.Item("PSAY").ToString = 0) Then

                                            sLine = ("QTY+1:" & Convert.ToString(nMiktar)) + ":"c + sQTYU + "'"

                                        End If

                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = "ALI+TR+1+7" & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("RFF+AAU:" & Convert.ToString(rowDt.Item("PICKNO").ToString)) + ":"c + Convert.ToString(nLine) + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = "DTM+171:" & ConvertAS400DatetoStr(rowDt.Item("SEVKTAR").ToString) & ":102" + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        sLine = ("LOC+11+" & sGate & "::92") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        sLine = ("LOC+19+" & sPlant & "::92") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nRff += 1
                                    End If
                                    ' Palet

                                    If rowDt.Item("PSAY").ToString <> 0 Then

                                        sLine = ("CPS+" & StrFill(CStr(nRff), 6, "0", "L") & "++3") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        If (rowDt.Item("KPMIK").ToString <> 0) AndAlso (rowDt.Item("KPKOD").ToString <> "") Then

                                            If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KPKOD").ToString)) = "1" Then

                                                ' Kapak ve seperatorlerde sadece 37 bilgisi olmalıymis. KADER 30/05/2005 Pazartesi
                                                sLine = (("PAC+" & Convert.ToString(rowDt.Item("KPMIK").ToString) & "+:37:11+") + sMKPKOD & "::92") + "'"
                                            Else

                                                sLine = (("PAC+" & Convert.ToString(rowDt.Item("KPMIK").ToString) & "+:37:2+") + sMKPKOD & "::92") + "'"
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        End If

                                        If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("PKOD").ToString)) = "1" Then

                                            sLine = (("PAC+" & Convert.ToString(rowDt.Item("PSAY").ToString) & "+::11+") + sMPKOD & "::92") + "'"
                                        Else

                                            sLine = (("PAC+" & Convert.ToString(rowDt.Item("PSAY").ToString) & "+::2+") + sMPKOD & "::92") + "'"

                                        End If

                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = "PCI+17+++M::10" & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        If rowDt.Item("PSAY").ToString = 1 Then

                                            sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + "'"

                                        Else

                                            sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + ":"c & StrFill(CStr(nRff), 1, " ", "R") & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString), 3, "0", "L") + "'"

                                        End If

                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nRff += 1

                                        sLine = (("CPS+" & StrFill(CStr(nRff), 6, "0", "L")) + "+" & StrFill(CStr(nRff - 1), 6, "0", "L") & "+1") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        nKutuSayi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString
                                        If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)) = "1" Then
                                            sLine = (("PAC+" & Convert.ToString(nKutuSayi) & "+::11+") + sMKKOD & "::92") + "'"
                                        Else
                                            sLine = (("PAC+" & Convert.ToString(nKutuSayi) & "+::2+") + sMKKOD & "::92") + "'"
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("QTY+52:" & Convert.ToString(rowDt.Item("KMIK").ToString)) + ":"c + sQTYU + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = "PCI+17+++S::10" & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        If nKutuSayi = 1 Then

                                            sLine = ("GIN+ML+" & StrFill(CStr(nRff), 1, " ", "R")) & StrFill("1", 3, "0", "L") + "'"

                                        Else

                                            sLine = ("GIN+ML+" & CStr(nRff)) & StrFill("1", 3, "0", "L") + ":"c + CStr(nRff) & StrFill(Convert.ToString(nKutuSayi), 3, "0", "L") + "'"

                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nRff += 1

                                        sLine = (("LIN+++" & sMURNKOD) + ":"c & "IN") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nLine += 1

                                        sLine = ("PIA+1+" & sKontrat & ":ON") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        ' Zeki 27.06.2007 'Mehmet Bey in isteği üzerine değişiklik yapıldı
                                        sYedekParcaPlanti = rowDt.Item("SHIPTO").ToString

                                        ' Zeki 27.06.2007 'Mehmet Bey in isteği üzerine değişiklik yapıldı
                                        If sYedekParcaPlanti = "14" Then
                                            sLine = "IMD+C+66+::5" & "'"
                                        Else
                                            sLine = "IMD+C+63+::5" & "'"
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("QTY+1:" & Convert.ToString(nKutuSayi * rowDt.Item("KMIK").ToString)) + ":"c + sQTYU + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = "ALI+TR+1+7" & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("RFF+AAU:" & Convert.ToString(rowDt.Item("PICKNO").ToString)) & ":" + Convert.ToString(nLine) & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("DTM+171:" & ConvertAS400DatetoStr(rowDt.Item("SEVKTAR").ToString) & ":102") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        sLine = ("LOC+11+" & sGate & "::92") & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                        ' sLine:='LOC+19+'+sPlant+'::92'+'''';
                                        sLine = ("LOC+19+" & sPlant & "::92") & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1

                                    End If

                                    If chkAsnYeni.Checked Then

                                        sSQL = " UPDATE " & "SHPPACK" & _
                                                " SET ASNDRM=1" & _
                                                    " ,ASNNO=" & Convert.ToString(nASN_No) & _
                                                    " ,ASNMIK=" & Convert.ToString(nKumMiktar) & _
                                                " WHERE CUST=" & sTirnakEkle(sCustomer) & _
                                                " AND SHIPTO=" & sTirnakEkle(sPlant) & _
                                                " AND KAPI=" & sTirnakEkle(sGate) & _
                                                " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString) & _
                                                " AND isnull(ASNDRM,0)<>9"

                                        sSQL = sSQL & " AND SHPNO=" & sTirnakEkle(rowDt.Item("SHPNO").ToString)

                                        db.RunSql(sSQL)

                                    End If

                                Next rowDt
                                sLine = ("UNT+" & Convert.ToString(nSatir)) + "+" + Convert.ToString(nMsgNo - 1) + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = ("UNZ+" & Convert.ToString(nUNH)) + "+" + sASN_No + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' ******************** DESADV D98 B - FORD Otosan ******************************

                            Catch ex As Exception

                                MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            Finally

                                objWriter.Close()

                            End Try
                            'AsnTipkontrol
                        Case "FORDD98B"

                            MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant

                            If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then
                                Directory.CreateDirectory(sASNDir + "\" + sSDRODID)
                            End If

                            If (File.Exists(MesajFile)) Then
                                File.Delete(MesajFile)
                            End If

                            objWriter = New System.IO.StreamWriter(MesajFile)

                            sLine = "UNA:+.? '"

                            objWriter.WriteLine(sLine)

                            sLine = "UNB+UNOC:3+" & sRCVODID + "+" + sASNODID + "+" + Now.Date.ToString("DDMMYY") + ":" + Now.ToString("HHmm") + "+" + sASN_No + "'"

                            objWriter.WriteLine(sLine)

                            nMsgNo = 1
                            nLine = 0
                            nRff = 1
                            ' Nakliyeci, Plaka Numarası .

                            sSQL = " SELECT DHHWTX FROM " & "MBDHREP" & " WHERE DHCANB=" & sCustomer & " AND DHB9CD=" & sTirnakEkle(sPlant) & " AND DHZ969=" & Convert.ToString(row.Cells(6).Text)

                            dtTemp = db.RunSql(sSQL)

                            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                                sPlaka = Trim(dtTemp.Rows(0).Item("DHHWTX").ToString)

                            End If

                            sSQL = " SELECT * FROM " & "SHPPACK" & " WHERE  SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & " AND isnull(ASNDRM,0)<>9"
                            sSQL = sSQL & " AND PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                            dt = db.RunSql(sSQL)

                            nMsgNo = 1
                            nUNH = 0
                            nSatir = 1
                            sGirNo = 1

                            nPickList = Trim(Convert.ToString(row.Cells("PIckno").Text))

                            sFatura = Trim(Convert.ToString(row.Cells("InvNo").Text))


                            GetRowInfo(sIrsaliyeno, dt, 0, "IRSNO")

                            GetRowInfo(sGate, dt, 0, "KAPI")

                            nBrtAgrVda = Math.Round(nBrtAgr)
                            nNetAgrVda = Math.Round(nNetAgr)
                            If nMsgNo = 1 Then
                                '@ Unsupported property or method(A): 'cstr'
                                sLine = "UNH+" & CStr(nMsgNo) & "+DESADV:D:98B:UN" + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                nUNH += 1
                                nMsgNo += 1
                                If (sLieferDrm = "0") OrElse (sLieferDrm = "") OrElse (sLieferDrm = " ") Then
                                    sLine = ("BGM+351+" & sIrsaliyeno & "+9") + "'"
                                ElseIf sLieferDrm = "1" Then
                                    sLine = ("BGM+351+" & sFatura & "+9") + "'"
                                End If
                                objWriter.WriteLine(sLine)
                                nSatir += 1

                                sDTM = Now.Date.ToString("yyyyMMdd") + Now.Date.ToString("HHmmSS")
                                sLine = ("DTM+11:" & sDTM & ":204") + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = ("DTM+137:" & sDTM & ":204") + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1

                                nYolSure = nLookup("TTIME", "PLANTPRM", " CANB=" & Convert.ToString(dt.Rows(0).Item("CUST").ToString) & " AND B9CD=" & sTirnakEkle(dt.Rows(0).Item("SHIPTO").ToString))

                                sDTM = DateAdd(DateInterval.Day, nYolSure, Now.Date).ToString("yyyyMMdd") + Now.Date.ToString("HHmmSS")
                                sLine = ("DTM+17:" & sDTM & ":204") + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' ********** GROSS WEIGHT *********
                                sLine = ("MEA+WT+G+KG:" & Convert.ToString(nBrtAgrVda)) + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = ("MEA+WT+N+KG:" & Convert.ToString(nNetAgrVda)) + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' Irsaliye ekranındaki

                                sLine = ("RFF+FN:" & dt.Rows(0).Item("USERF4").ToString) + "'"
                                ' Navlun Fatura No
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' sLine:='NAD+ST+'+sPlant+'::92'+'''';
                                sLine = ("NAD+ST+" & sPlant & "::92") + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = ("NAD+SF+" & sDUNS & "::92") + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = ("NAD+SU+" & sDUNS & "::92") + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = "TOD++PP+::116:FOB" & "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                sLine = "LOC+5'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                '
                                ' if (sASNTip='VW') then begin
                                ' sSQL:=' SELECT USER2 FROM '+SetTableName(dtmEkip.LibNameMapMod,'KONTRTPF')+
                                ' ' WHERE SRKT='+dtmEkip.Company+' AND CUST='+sCustomer+' '+
                                ' ' AND SHIPTO='+Quote(sPlant)+
                                ' ' AND KAPI='+Quote(sGate)+' AND BZMITM='+Quote(rowDt.Item('ITNBR').ToString);
                                ' with qryTemp do
                                ' begin
                                ' Close; SQL.Clear; SQL.Add(sSQL); Open; First;
                                ' if not eof then
                                ' begin
                                ' sSevkYeri :=rowDt.Item('USER2').ToString;
                                ' end;
                                ' end;
                                ' sLine:='LOC+7+'+sSevkYeri+'::92+X'+'''';
                                ' WriteLn(fMessages,sLine); inc(nSatir);
                                ' end;
                                ' Conveyence No

                                sLine = (("TDT+12+" & dt.Rows(0).Item("USERF3").ToString) + "+" + dt.Rows(0).Item("TRNCODE").ToString & "++") + sCarrier & "+++'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                '
                                ' sLine:='TDT+25+'+rowDt.Item('USERF3').ToString+'+'+rowDt.Item('TRNCODE').ToString+'++'+sCarrier+'+++'+'''';
                                ' WriteLn(fMessages,sLine); inc(nSatir);
                                '
                                ' Conveyence No
                                '
                                ' sLine:='EQD+CN+'+rowDt.Item('USERF3').ToString+'''';
                                ' WriteLn(fMessages,sLine); inc(nSatir);
                                '
                                sLine = ("EQD+TE+" & sPlaka.Substring(1 - 1, 7)) + "'"
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                            End If

                            For Each rowDt As DataRow In dt.Rows

                                sMPKOD = ""
                                sMKKOD = ""
                                sMSPKOD = ""
                                sMKPKOD = ""
                                sMURNKOD = ""
                                ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, dt.Rows(0).Item("ITNBR").ToString, dt.Rows(0).Item("AMBKOD").ToString)

                                ' Müşteri Ürün Kodu, Adı
                                MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                nMiktar = rowDt.Item("SHIPMIK").ToString

                                'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                If sBirim = "AD" Then
                                    sQTYU = "PCE"
                                ElseIf sBirim = "KG" Then
                                    sQTYU = "KGM"
                                ElseIf sBirim = "MT" Then
                                    sQTYU = "MTR"
                                End If
                                UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                            rowDt.Item("ITNBR").ToString, _
                                            sGate, sKumulDuzeyi, nMiktar, _
                                            rowDt.Item("SHPNO").ToString, _
                                            rowDt.Item("SHIPTO").ToString, _
                                            rowDt.Item("PICKNO").ToString)

                                ' Paletsizler
                                If (rowDt.Item("HKSAY").ToString <> 0) OrElse (rowDt.Item("PSAY").ToString = 0) Then

                                    sLine = ("CPS+" & CStr(nRff) & "++4") + "'"

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                    If (rowDt.Item("HKSAY").ToString <> 0) Then

                                        If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)) = "1" Then

                                            sLine = "PAC+" & Convert.ToString(rowDt.Item("HKSAY").ToString) & "++" + sMKKOD & "::116" + "'"

                                            objWriter.WriteLine(sLine)

                                            nSatir += 1

                                            sLine = "QTY+117:" & Convert.ToString(rowDt.Item("HKSAY").ToString) + "'"

                                            objWriter.WriteLine(sLine)

                                            nSatir += 1

                                        End If

                                        'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                        sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                        If sBirim = "AD" Then
                                            sQTYU = "PCE"
                                        ElseIf sBirim = "KG" Then
                                            sQTYU = "KGM"
                                        ElseIf sBirim = "MT" Then
                                            sQTYU = "MTR"
                                        End If

                                        sLine = ("QTY+52:" & rowDt.Item("KMIK").ToString.Trim()) + ":" + sQTYU.Trim() + "'"

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                        sLine = "PCI+15" & "'"

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1
                                        ' ***************************************
                                        '
                                        ' sLine := 'GIR+3';
                                        ' for k := 1 to rowDt.Item('HKSAY').ToString do begin
                                        ' sLine := sLine + '+S'+cstr(sGirNo)+':ML';
                                        ' sGirNo := sGirNo + 1;
                                        ' end;
                                        ' sLine := sLine + '''';
                                        ' WriteLn(fMessages,sLine); inc(nSatir);
                                        ' ***************************************

                                        sGIRMik = rowDt.Item("HKSAY").ToString

                                        sGIRMikKayit = rowDt.Item("HKSAY").ToString

                                        nDonguSayisi = 495

                                        For j = 1 To (sGIRMikKayit / 495)
                                            While nDonguSayisi <> 0
                                                sLine = "GIR+3"
                                                For k = 1 To 5
                                                    If sGIRMik <> 0 Then

                                                        sLine = (sLine & "+S") + CStr(sGirNo) & ":ML"
                                                        sGirNo = sGirNo + 1
                                                        sGIRMik = sGIRMik - 1
                                                        nDonguSayisi = nDonguSayisi - 1
                                                    End If
                                                Next
                                                sLine = sLine + "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            End While
                                            ' while do
                                            If j <> (Convert.ToInt64(sGIRMikKayit) / 495) Then
                                                sLine = "PCI+17" & "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            Else
                                                If (Convert.ToInt64(sGIRMikKayit) Mod 495) <> 0 Then
                                                    sLine = "PCI+17" & "'"
                                                    objWriter.WriteLine(sLine)
                                                    nSatir += 1
                                                End If
                                            End If
                                            nDonguSayisi = 495
                                        Next
                                        For jj = 1 To (Convert.ToInt64(sGIRMikKayit) Mod 495)
                                            If sGIRMik <> 0 Then
                                                sLine = "GIR+3"
                                                For k = 1 To 5
                                                    If sGIRMik <> 0 Then

                                                        sLine = (sLine & "+S") + CStr(sGirNo) & ":ML"
                                                        sGirNo = sGirNo + 1
                                                        sGIRMik = sGIRMik - 1
                                                    End If
                                                Next
                                                sLine = sLine + "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            End If
                                            ' for
                                        Next

                                    ElseIf (rowDt.Item("PSAY").ToString = 0) Then

                                        If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)) = "1" Then

                                            sLine = (("PAC+" & Convert.ToString(rowDt.Item("KSAY").ToString) & "++") + sMKKOD & "::116") + "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1

                                            sLine = ("QTY+117:" & Convert.ToString(rowDt.Item("KSAY").ToString)) + "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        End If

                                        'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                        sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                        If sBirim = "AD" Then
                                            sQTYU = "PCE"
                                        ElseIf sBirim = "KG" Then
                                            sQTYU = "KGM"
                                        ElseIf sBirim = "MT" Then
                                            sQTYU = "MTR"
                                        End If

                                        sLine = ("QTY+52:" & rowDt.Item("KMIK").ToString.Trim()) + ":"c + sQTYU.Trim() + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        sLine = "PCI+15" & "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        '
                                        ' sLine := 'GIR+3';
                                        ' for k := 1 to rowDt.Item('KSAY').ToString do begin
                                        ' sLine := sLine + '+S'+cstr(sGirNo)+':ML';
                                        ' sGirNo := sGirNo + 1;
                                        ' end;
                                        ' sLine := sLine + '''';
                                        ' WriteLn(fMessages,sLine); inc(nSatir);

                                        sGIRMik = rowDt.Item("KSAY").ToString

                                        sGIRMikKayit = rowDt.Item("KSAY").ToString

                                        nDonguSayisi = 495
                                        For j = 1 To (sGIRMikKayit / 495)
                                            While nDonguSayisi <> 0
                                                sLine = "GIR+3"
                                                For k = 1 To 5
                                                    If sGIRMik <> 0 Then

                                                        sLine = (sLine & "+S") + CStr(sGirNo) & ":ML"
                                                        sGirNo = sGirNo + 1
                                                        sGIRMik = sGIRMik - 1
                                                        nDonguSayisi = nDonguSayisi - 1
                                                    End If
                                                Next
                                                sLine = sLine + "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            End While
                                            ' while do
                                            If j <> (Convert.ToInt64(sGIRMikKayit) / 495) Then
                                                sLine = "PCI+17" & "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            Else
                                                If (Convert.ToInt64(sGIRMikKayit) Mod 495) <> 0 Then
                                                    sLine = "PCI+17" & "'"
                                                    objWriter.WriteLine(sLine)
                                                    nSatir += 1
                                                End If
                                            End If
                                            nDonguSayisi = 495
                                        Next
                                        For jj = 1 To (Convert.ToInt64(sGIRMikKayit) Mod 495)
                                            If sGIRMik <> 0 Then
                                                sLine = "GIR+3"
                                                For k = 1 To 5
                                                    If sGIRMik <> 0 Then

                                                        sLine = (sLine & "+S") + CStr(sGirNo) & ":ML"
                                                        sGirNo = sGirNo + 1
                                                        sGIRMik = sGIRMik - 1
                                                    End If
                                                Next
                                                sLine = sLine + "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            End If
                                            ' for
                                        Next
                                    End If

                                    sLine = (("LIN+" & CStr(nRff) & "++") + sMURNKOD + ":"c & "IN") + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    nLine += 1

                                    nBrtAgr = rowDt.Item("NETAGR").ToString
                                    nBrtAgrRnd = Math.Round(nBrtAgr)
                                    sLine = ("MEA+PD+N+KG:" & Convert.ToString(nBrtAgrRnd)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    nKutuSayi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                    If (rowDt.Item("HKSAY").ToString <> 0) Then

                                        sLine = ("QTY+12:" & Convert.ToString(nMiktar - (nKutuSayi * rowDt.Item("KMIK").ToString))) + ":"c + sQTYU + "'"

                                    ElseIf (rowDt.Item("PSAY").ToString = 0) Then
                                        sLine = ("QTY+12:" & Convert.ToString(nMiktar)) + ":"c + sQTYU + "'"
                                    End If
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = ("QTY+3:" & Convert.ToString(nKumMiktar)) + ":"c + sQTYU + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    sLine = ("RFF+PK:" & Convert.ToString(rowDt.Item("PICKNO").ToString)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    nRff += 1
                                End If
                                ' Paletliler

                                If rowDt.Item("PSAY").ToString <> 0 Then

                                    sLine = ("CPS+" & CStr(nRff) & "++4") + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    nKutuSayi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                    If sLookup("AYNTIP", "ITEMDIM", " ITNBR=" & sTirnakEkle(rowDt.Item("KKOD").ToString)) = "1" Then
                                        sLine = (("PAC+" & Convert.ToString(nKutuSayi) & "++") + sMKKOD & "::116") + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        sLine = ("QTY+117:" & Convert.ToString(nKutuSayi)) + "'"
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                    End If

                                    'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                    sBirim = sLookup("AGOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                    If sBirim = "AD" Then
                                        sQTYU = "PCE"
                                    ElseIf sBirim = "KG" Then
                                        sQTYU = "KGM"
                                    ElseIf sBirim = "MT" Then
                                        sQTYU = "MTR"
                                    End If

                                    sLine = ("QTY+52:" & rowDt.Item("KMIK").ToString.Trim()) + ":" + sQTYU.Trim() + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = "PCI+15" & "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    '
                                    ' sLine := 'GIR+3';
                                    ' for k := 1 to Cint(FloatToStr(nKutuSayi)) do begin
                                    ' sLine := sLine + '+S'+cstr(sGirNo)+':ML';
                                    ' sGirNo := sGirNo + 1;
                                    ' end;
                                    ' sLine := sLine + '''';
                                    ' WriteLn(fMessages,sLine); inc(nSatir);
                                    '

                                    sGIRMik = CInt(Convert.ToString(nKutuSayi))

                                    sGIRMikKayit = CInt(Convert.ToString(nKutuSayi))
                                    nDonguSayisi = 495
                                    For j = 1 To (sGIRMikKayit / 495)
                                        While nDonguSayisi <> 0
                                            sLine = "GIR+3"
                                            For k = 1 To 5
                                                If sGIRMik <> 0 Then

                                                    sLine = (sLine & "+S") + CStr(sGirNo) & ":ML"
                                                    sGirNo = sGirNo + 1
                                                    sGIRMik = sGIRMik - 1
                                                    nDonguSayisi = nDonguSayisi - 1
                                                End If
                                            Next
                                            sLine = sLine + "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        End While
                                        ' while do
                                        If j <> (Convert.ToInt64(sGIRMikKayit) / 495) Then
                                            sLine = "PCI+17" & "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        Else
                                            If (Convert.ToInt64(sGIRMikKayit) Mod 495) <> 0 Then
                                                sLine = "PCI+17" & "'"
                                                objWriter.WriteLine(sLine)
                                                nSatir += 1
                                            End If
                                        End If
                                        nDonguSayisi = 495
                                    Next
                                    For jj = 1 To (Convert.ToInt64(sGIRMikKayit) Mod 495)
                                        If sGIRMik <> 0 Then
                                            sLine = "GIR+3"
                                            For k = 1 To 5
                                                If sGIRMik <> 0 Then
                                                    sLine = (sLine & "+S") + CStr(sGirNo) & ":ML"
                                                    sGirNo = sGirNo + 1
                                                    sGIRMik = sGIRMik - 1
                                                End If
                                            Next
                                            sLine = sLine + "'"
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                        End If
                                    Next
                                    ' for

                                    sLine = (("LIN+" & CStr(nRff) & "++") + sMURNKOD + ":"c & "IN") + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    nLine += 1

                                    nBrtAgr = rowDt.Item("NETAGR").ToString
                                    nBrtAgrRnd = Math.Round(nBrtAgr)
                                    sLine = ("MEA+PD+N+KG:" & Convert.ToString(nBrtAgrRnd)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    nKutuSayi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                    sLine = ("QTY+12:" & Convert.ToString(nKutuSayi * rowDt.Item("KMIK").ToString)) + ":"c + sQTYU + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    sLine = ("QTY+3:" & Convert.ToString(nKumMiktar)) + ":"c + sQTYU + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    sLine = ("RFF+PK:" & Convert.ToString(rowDt.Item("PICKNO").ToString)) + "'"
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                    nRff += 1
                                End If

                                UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString, nPickList)

                            Next rowDt
                            sLine = ("CNT+1:" & Convert.ToString(nSatir + 3)) + "'"
                            objWriter.WriteLine(sLine)
                            '@ Undeclared identifier(3): 'Inc'
                            Inc(nSatir)
                            '@ Undeclared identifier(3): 'cstr'
                            sLine = "CNT+2:" & CStr(nLine) + "'"
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                            sLine = ("UNT+" & Convert.ToString(nSatir)) + "+" + Convert.ToString(nMsgNo - 1) + "'"
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                            sLine = ("UNZ+" & Convert.ToString(nUNH)) + "+" + sASN_No + "'"
                            objWriter.WriteLine(sLine)
                            nSatir += 1
                            ' ***************************** RENAULT DESADV D96 A *****************************
                            objWriter.Close()
                            'AsnTipkontrol

                        Case "RENAULT", "CONTI"

                            nSatir = 0

                            nRffAat = 1

                            nGir = 1

                            MesajFile = sASNDir + "\" + sSDRODID.Replace(":", "") + "\" + "A" + sASN_No + "." + sPlant

                            If Not Directory.Exists(sASNDir + "\" + sSDRODID.Replace(":", "")) Then

                                Directory.CreateDirectory(sASNDir & "\" & sSDRODID.Replace(":", ""))

                            End If

                            If (File.Exists(MesajFile)) Then

                                File.Delete(MesajFile)

                            End If

                            objWriter = New System.IO.StreamWriter(MesajFile)
                            ' sLine:='UNB+UNOA:1+'+Trim(sRCVODID)+'+'+Trim(sASNODID)+'+'+FormatDateTime('yyMMdd',date)+':'+FormatDateTime('HHmm',time)+'+'+Trim(sASN_No)+'++++++1''';  Degistirildi 02/03/06

                            sLine = "UNB+UNOA:1+0798000065SAHIN+" & sASNODID.Trim() + "+" + Now.Date.ToString("yyMMdd") + ":"c + Now.ToString("HHmm") + "+" + sASN_No.Trim() & "++++++0'"
                            objWriter.Write(sLine)
                            nSatir += 1
                            nMsgNo = 1


                            sSQL = "UPDATE SHPPACK SET ASNDRM = 1 WHERE SHPNO = '" & Convert.ToString(row.Cells("SHPNO").Text) & "'"
                            db.RunSql(sSQL)

                            sSQL = " SELECT * FROM SHPPACK" &
                                        " WHERE SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) &
                                        " AND isnull(ASNDRM,0)<>9"
                            sSQL = (sSQL & " AND SHPNO=") + Convert.ToString(row.Cells("SHPNO").Text)

                            dt = db.RunSql(sSQL)




                            nMsgNo = 1
                            nUNH = 0
                            nRff = 1

                            nPickList = Trim(Convert.ToString(row.Cells("PIckno").Text))

                            sFatura = Trim(Convert.ToString(row.Cells("InvNo").Text))

                            sGate = Nothing

                            For Each rowDt As DataRow In dt.Rows


                                If sGate <> rowDt.Item("KAPI").ToString Or sGate Is Nothing Then

                                    '@ Unsupported property or method(A): 'cstr'
                                    sLine = "UNH+" & CStr(nMsgNo) & "+DESADV:D:96A:UN:A01052'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                    nUNH += 1
                                    nMsgNo = nMsgNo + 1
                                    sBGM = "351"

                                    If (sLieferDrm = "0") OrElse (sLieferDrm = "") Then
                                        sLine = ("BGM+351+" & dt.Rows(0).Item("SHPNO").ToString.Trim()) + IIf(sASNTip = "CONTI", "+9", "") + "'"
                                    ElseIf sLieferDrm = "1" Then
                                        sLine = ("BGM+351+" & sFatura.Trim()) + IIf(sASNTip = "CONTI", "+9", "") + "'"
                                    End If
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sLine = "DTM+11:" & ConvertAS400DatetoStr(dt.Rows(0).Item("SEVKTAR").ToString) & ":102" + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sDTM = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyyyMMdd")
                                    sLine = "DTM+132:" & sDTM & ":102" + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sDTM = Now.Date.ToString("yyyyMMdd")
                                    sLine = "DTM+137:" & sDTM & ":102" + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                    ' ********** GROSS WEIGHT *********
                                    sMEA = "AAX"
                                    sMEAC = "AAD"
                                    sMEAU = "KGM"
                                    nBrtAgrRnd = Math.Round(nBrtAgr)
                                    sLine = ("MEA+AAX+AAD+KGM:" & Convert.ToString(nBrtAgrRnd).Trim()) + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    If dt.Rows(0).Item("PUSNO").ToString.Contains("%") = True Then

                                        sPusNo = dt.Rows(0).Item("PUSNO").ToString.Trim().Substring(0, dt.Rows(0).Item("PUSNO").ToString.Trim().IndexOf("%"))

                                    Else

                                        If dt.Rows(0).Item("PUSNO").ToString.Length > 2 Then
                                            If dt.Rows(0).Item("PUSNO").ToString.Substring(0, 2) = "EO" OrElse
                                                dt.Rows(0).Item("PUSNO").ToString.Substring(0, 2) = "FO" Then

                                                sPusNo = ""

                                            Else

                                                GetRowInfo(sPusNo, dt, 0, "PUSNO")

                                            End If

                                        Else

                                            GetRowInfo(sPusNo, dt, 0, "PUSNO")

                                        End If

                                    End If

                                    If sASNTip = "RENAULT" Then
                                        If sPusNo = "" Then
                                            sLine = "RFF+CRN'"
                                        Else
                                            sLine = ("RFF+CRN:" & sPusNo) + "'"
                                        End If
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                    End If
                                   
                                    sLine = "RFF+AAS:" & rowDt.Item("IRSNO").ToString + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sNAD = "ST"
                                    sLine = ("NAD+" & sNAD.Trim()) + "+" + sPlant.Trim() & "::92'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sNAD = "SE"
                                    sLine = ("NAD+" & sNAD.Trim()) + "+" + sDUNS.Trim() & "::92'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sNAD = "BY"
                                    sLine = ("NAD+" & sNAD.Trim()) + "+" + sPlant.Trim() & "::92'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    sNAD = "IV"
                                    sLine = ("NAD+" & sNAD.Trim()) + "+" + sPlant.Trim() & "::92'"
                                    objWriter.Write(sLine)
                                    nSatir += 1

                                    If sASNTip = "RENAULT" Then
                                        sLine = ("RFF+ADE:" & StrFill(sDUNS.Trim(), 8, "0", "L")) + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                    End If
                                 
                                    'sNAD = "CN"

                                    'sNAD1 = sLookup("DACEDI", "DACIAPF", " DACSHP=" & sTirnakEkle(sPlant))
                                    ''
                                    '' if sPlant='TU' then sNAD1:='09317801299870101'
                                    '' else if sPlant='TK' then  sNAD1:='09317801299870102'
                                    '' else if sPlant='FL' then  sNAD1:='09317801299871892'
                                    '' else if sPlant='BU' then  sNAD1:='09317801299870104'
                                    '' else if sPlant='CI' then  sNAD1:='09459813295CI'
                                    '' else if sPlant='SK' then  sNAD1:='09317801299870007';
                                    ''
                                    'sLine = ("NAD+" & sNAD.Trim()) + "+" + sNAD1.Trim() & "::10'"
                                    'objWriter.Write(sLine)
                                    'nSatir += 1

                                    GetRowInfo(sGate, dt, 0, "KAPI")

                                    'sLine = ("LOC+11+" & sGate.Trim()) + "'"
                                    'objWriter.Write(sLine)
                                    'nSatir += 1
                                    sDunsSifir = "00" & sDUNS.Trim() & "00"
                                    'sLine = ("NAD+CZ+" & sDunsSifir & "::92") + "'"
                                    'objWriter.Write(sLine)
                                    'nSatir += 1
                                    sTDT4 = sCarrier
                                    '
                                    ' if sPlaka='' then sPlaka:='UNKNOWN';
                                    ' sLine:='EQD+TE+'+Trim(sPlaka)+'''';
                                    ' Write(fMessages,sLine); inc(nSatir);
                                    '
                                    If sPlaka = "" Then
                                        sPlaka = "UNKNOWN"
                                    End If
                                    'sLine = ("EQD+TE+" & sPlaka.Trim()) + "'"
                                    'objWriter.Write(sLine)
                                    'nSatir += 1
                                End If
                                sMPKOD = ""
                                sMKKOD = ""
                                sMSPKOD = ""
                                sMKPKOD = ""
                                sMURNKOD = ""
                                ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, rowDt.Item("ITNBR").ToString, rowDt.Item("AMBKOD").ToString)

                                ' Müşteri Ürün Kodu, Adı
                                MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                nMiktar = rowDt.Item("SHIPMIK").ToString

                                nKutuSayi = rowDt.Item("KSAY").ToString

                                UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                            rowDt.Item("ITNBR").ToString, _
                                            sGate, sKumulDuzeyi, nMiktar, _
                                            rowDt.Item("SHPNO").ToString, _
                                            rowDt.Item("SHIPTO").ToString, _
                                            rowDt.Item("PICKNO").ToString)

                                If sASNTip = "RENAULT" Then

                                    sLine = "CPS+" & CStr(nRff) & "++" & "1'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                    nRff += 1

                                ElseIf sASNTip = "CONTI" Then

                                    sLine = "CPS+" & CStr(nRff) & "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                    nRff += 1

                                End If
                               

                                sLine = "PAC+" & Convert.ToString(nKutuSayi) & "++" + sMKKOD.Trim() & "::92'"
                                objWriter.Write(sLine)
                                nSatir += 1

                                'sBirim = sLookup("DDDHCD", "MBDDREP", "  DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))
                                sBirim = sLookup("STKOB", "SHPPACK", " SHPNO=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                If sBirim = "AD" Then
                                    sQTYU = "PCE"
                                ElseIf sBirim = "KG" Then
                                    sQTYU = "KGM"
                                ElseIf sBirim = "MT" Then
                                    sQTYU = "MTR"
                                End If

                                'sLine = "QTY+52:" & rowDt.Item("KMIK").ToString.Trim() & ":" & sQTYU.Trim() & "'"
                                'objWriter.Write(sLine)
                                'nSatir += 1

                                nPaletKutuAdedi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                'If rowDt.Item("PSAY").ToString <> 0 Then

                                '    If rowDt.Item("HKSAY").ToString <> 0 Then

                                '        For k = 1 To rowDt.Item("HKSAY").ToString

                                '            sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                '            If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                '                sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()
                                '            Else
                                '                sPusNo = ""
                                '            End If
                                '            If sPusNo = "" Then

                                '                sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"
                                '            Else

                                '                sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"
                                '            End If
                                '            objWriter.Write(sLine)
                                '            nSatir += 1
                                '            nGir += 1
                                '        Next
                                '    End If

                                'For k = 1 To CInt(Convert.ToString(rowDt.Item("PSAY").ToString))
                                '    sLine = "PCI+17'"
                                '    objWriter.Write(sLine)
                                '    nSatir += 1

                                '    sLine = ("RFF+AAT:" & StrFill(CStr(nRffAat), 9, "0", "L")) + "'"
                                '    objWriter.Write(sLine)
                                '    nSatir += 1
                                '    nRffAat += 1

                                '    For nGCount = 1 To rowDt.Item("PKMIK").ToString
                                '        If nPaletKutuAdedi <> 0 Then
                                '            nPaletKutuAdedi = nPaletKutuAdedi - 1

                                '            sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                '            If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                '                sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()
                                '            Else
                                '                sPusNo = ""
                                '            End If
                                '            If sPusNo = "" Then

                                '                sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"
                                '            Else

                                '                sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"
                                '            End If
                                '            objWriter.Write(sLine)
                                '            nSatir += 1
                                '            nGir += 1
                                '        End If
                                '    Next
                                'Next
                                'Else

                                'For nGCount = 1 To rowDt.Item("KSAY").ToString

                                '    sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                '    If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                '        sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()
                                '    Else
                                '        sPusNo = ""
                                '    End If
                                '    If sPusNo = "" Then

                                '        sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"
                                '    Else

                                '        sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"
                                '    End If
                                '    objWriter.Write(sLine)
                                '    nSatir += 1
                                '    nGir += 1
                                'Next
                                'End If

                                sLine = "LIN+++" & RTrim(sMURNKOD) & ":IN++0'"
                                objWriter.Write(sLine)
                                nSatir += 1

                                sLine = "QTY+12:" & Convert.ToString(nMiktar) + ":" + sQTYU + "'"
                                objWriter.Write(sLine)
                                nSatir += 1

                                If sASNTip = "RENAULT" Then
                                    sLine = "ALI+TR'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                End If

                                If sASNTip = "CONTI" Then
                                    sLine = "RFF+ON:" & sKontrat.Trim() + ":0010'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                Else
                                    sLine = "RFF+ON:" & sKontrat.Trim() + "'"
                                    objWriter.Write(sLine)
                                    nSatir += 1
                                End If
                             
                              


                                If sASNTip = "RENAULT" Then

                                    sSQL = "select Top 1 co.order_date" &
                                                " from shpdty" &
                                                " Left Join co" &
                                                     " On ORDNO=co_num" &
                                                " where shpno=" & Convert.ToString(rowDt.Item("SHPNO").ToString) &
                                                    " and Itnbr=" & sTirnakEkle(Convert.ToString(rowDt.Item("Itnbr").ToString))

                                    dt = db.RunSql(sSQL)

                                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                                        sDTM = CDate(dt.Rows(0).Item(0).ToString).ToString("yyyyMMdd")
                                        sLine = "DTM+4:" & sDTM & ":102" + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                    End If

                                End If

                                UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString, nPickList)

                            Next rowDt

                            sLine = ("UNT+" & Convert.ToString(nSatir)) + "+" + Convert.ToString(nMsgNo - 1) + "'"
                            objWriter.Write(sLine)
                            nSatir += 1

                            sLine = ("UNZ+" & Convert.ToString(nUNH)) + "+" + sASN_No + "'"
                            objWriter.Write(sLine)
                            nSatir += 1

                            objWriter.Close()

                            'AsnTipkontrol
                            ' ***************************** RENAULT L3P *****************************
                        Case "L3P"
                            If sTumShipNo.Count > 1 Then
                                For i = 0 To sTumShipNo.Count - 1
                                    If nShipNo = sTumShipNo(i) Then
                                        bOk = False
                                    End If
                                Next
                            ElseIf sTumShipNo.Count = 1 Then
                                bOk = True
                            End If
                            bTmpItnbr = True
                            If bOk = True Then
                                ' Brüt Ağırlık, Net Ağırlık, Kap Adedi Hesaplama
                                nBrtAgr = 0
                                nNetAgr = 0
                                nKapAdet = 0
                                nHacim = 0

                                sSQL = " SELECT SUM(BRTAGR) AS BAGR ,SUM(NETAGR) AS NAGR,SUM(HKSAY) AS HKUTU,SUM(PSAY) AS PSAYI, SUM(HACIM) AS HCM " & _
                                        " FROM " & "SHPPACK" & _
                                        " WHERE CUST=" & sTirnakEkle(row.Cells("Cust").Text) & _
                                        " AND   SHPNO=" & nShipNo & " AND isnull(ASNDRM,0)<>9" & _
                                        " And PICKNO=" & nPickList

                                dtTemp = db.RunSql(sSQL)

                                GetRowInfo(nBrtAgr, dtTemp, 0, "BAGR")

                                GetRowInfo(nNetAgr, dtTemp, 0, "NAGR")

                                Dim nHKUTU, nPSAYI As Integer

                                GetRowInfo(nHKUTU, dtTemp, 0, "HKUTU")

                                GetRowInfo(nPSAYI, dtTemp, 0, "PSAYI")

                                nKapAdet = nHKUTU + nPSAYI

                                GetRowInfo(nHacim, dtTemp, 0, "HCM")

                                nSatir = 1
                                nRffAat = 1
                                nGir = 1
                                MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant

                                If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then
                                    Directory.CreateDirectory(sASNDir + "\" + sSDRODID)
                                End If

                                If (File.Exists(MesajFile)) Then
                                    File.Delete(MesajFile)
                                End If
                                objWriter = New System.IO.StreamWriter(MesajFile)

                                sLine = "UNB+UNOA:1+" & "O0013003189TEKLAS71" & "+" & sASNODID.Trim() & "+" & Now.Date.ToString("yyMMdd") & ":" & Now.ToString("HHmm") & "+" & sASN_No.Trim() & "++++++0'"
                                objWriter.Write(sLine)
                                nSatir += 1
                                nMsgNo = 1

                                ' ' AND PICKNO='+FloattoStr(tvPick.DataController.Values[nRowP,3])+
                                sSQL = " SELECT * " & _
                                        " FROM " & "SHPPACK" & " " & _
                                        " WHERE SHPNO =" & nShipNo & _
                                        " AND SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & _
                                        " AND PICKNO=" & nPickList & _
                                        " AND isnull(ASNDRM,0)<>9" & _
                                        " Order By Itnbr,PUSNO"

                                dt = db.RunSql(sSQL)

                                nMsgNo = 1
                                nUNH = 0
                                nRff = 1

                                nPickList = Trim(Convert.ToString(row.Cells("PIckno").Text))

                                sFatura = Trim(Convert.ToString(row.Cells("InvNo").Text))
                                sTmpItnbr = ""
                                sGate = ""

                                For Each rowDt As DataRow In dt.Rows

                                    If sGate.Trim() <> rowDt.Item("KAPI").ToString Then
                                        '@ Unsupported property or method(A): 'cstr'
                                        sLine = "UNH+" & CStr(nMsgNo) & "+DESADV:D:96A:UN:A01052'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        nUNH += 1
                                        nMsgNo = nMsgNo + 1
                                        sBGM = "351"
                                        If (sLieferDrm = "0") OrElse (sLieferDrm = "") Then

                                            sLine = ("BGM+351+" & dt.Rows(0).Item("SHPNO").ToString.Trim()) + "'"
                                        ElseIf sLieferDrm = "1" Then
                                            sLine = ("BGM+351+" & sFatura.Trim()) + "'"
                                        End If
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sSQL = "  SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & " AND isnull(ASNDRM,0)<>9"

                                        sSQL = (sSQL & " AND PICKNO=") + Convert.ToString(row.Cells("PIckno").Text)

                                        sCrdTim = sLookup("MAX(CRDTIM)", "SHPPACK", sSQL)
                                        If sCrdTim.Length = 5 Then
                                            sCrdTim = "0" + sCrdTim
                                        End If

                                        GetRowInfo(sDTM, dt, 0, "SEVKTAR")

                                        sDTM = CDate(sDTM).ToString("yyyyMMdd")

                                        sLine = "DTM+11:" & sDTM & Copy(sCrdTim, 0, 4) & ":203" + "'"

                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        ' sDTM:=FormatDateTime('yyyyMMdd',(date+nYolSuresi))+copy(sCrdTim, 1, 4);

                                        sDTM = DateAdd(DateInterval.Day, nYolSuresi, CDate(dt.Rows(0).Item("SEVKTAR").ToString)).ToString("yyyyMMdd") & Copy(sCrdTim, 0, 4)
                                        sLine = ("DTM+132:" & sDTM & ":203") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        ' sDTM:=FormatDateTime('yyyyMMdd',date)+FormatDateTime('HHmm',time);

                                        GetRowInfo(sCrdTim, dt, 0, "CRDTIM")

                                        If sCrdTim.Length = 5 Then
                                            sCrdTim = "0" & sCrdTim
                                        End If

                                        GetRowInfo(sDTM, dt, 0, "CRDDTE")

                                        sDTM = CDate(sDTM).ToString("yyyyMMdd") & Copy(sCrdTim, 0, 4)

                                        sLine = ("DTM+137:" & sDTM & ":203") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        ' ********** GROSS WEIGHT *********
                                        sMEA = "AAX"
                                        sMEAC = "AAD"
                                        sMEAU = "KGM"
                                        nBrtAgrRnd = Math.Round(nBrtAgr)
                                        sLine = "MEA+AAX+AAD+KGM:" & Convert.ToString(nBrtAgrRnd).Trim() + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        If dt.Rows(0).Item("PUSNO").ToString.Trim().Contains("%") Then

                                            sPusNo = dt.Rows(0).Item("PUSNO").ToString.Split("%")(0).ToString

                                        Else

                                            sPusNo = ""

                                        End If
                                        ' Zeki  19.09.2006
                                        If (sASNTip = "L3P") AndAlso (sPusNo = "") Then
                                            sLine = ("RFF+CRN" & sPusNo) & "'"
                                        Else
                                            sLine = ("RFF+CRN:" & sPusNo) & "'"
                                        End If

                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sNAD = "SE"
                                        sLine = ("NAD+" & sNAD.Trim()) + "+" + sRCVODID.Trim() & "::10'"
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        If (dt.Rows(0).Item("Cust").ToString = "3203203") Then
                                            sLine = ("RFF+ADE:" & sDUNS.Trim()) & "'"
                                        Else
                                            sLine = ("RFF+ADE:" & StrFill(sDUNS.Trim(), 8, "0", "L")) + "'"
                                        End If

                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sNAD = "CN"

                                        sNAD1 = sLookup("DACEDI", "DACIAPF", " DACSHP=" & sTirnakEkle(sPlant))
                                        '
                                        ' if sPlant='TU' then sNAD1:='09317801299870101'
                                        ' else if sPlant='TK' then sNAD1:='09317801299870102'
                                        ' else if sPlant='FL' then sNAD1:='09317801299871892'
                                        ' else if sPlant='VB' then sNAD1:='0941A470005180009'
                                        ' else if sPlant='BU' then  sNAD1:='09317801299870104'
                                        ' else if sPlant='CI' then  sNAD1:='09459813295CI'
                                        ' else if sPlant='SK' then  sNAD1:='09317801299870007';
                                        '
                                        sLine = ("NAD+" & sNAD.Trim()) + "+" + sNAD1.Trim() & "::10'"
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        sGateTmp = sLookup("KANBAN", "KONTRTPF", " CUST=" & sTirnakEkle(dt.Rows(0).Item("CUST").ToString.Trim()) & " AND BZMITM=" & sTirnakEkle(dt.Rows(0).Item("ITNBR").ToString.Trim()) & " AND SHIPTO=" & sTirnakEkle(dt.Rows(0).Item("SHIPTO").ToString.Trim()) & " AND KAPI=" & sTirnakEkle(rowDt.Item("KAPI").ToString.Trim()))

                                        GetRowInfo(sGate, dt, 0, "KAPI")

                                        sLine = ("LOC+11+" & sGateTmp.Trim()) + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sDunsSifir = sDUNS.Trim() & "00"
                                        sLine = ("NAD+CZ+" & sDunsSifir & "::92") + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sTDT4 = sCarrier
                                        If sPlaka = "" Then
                                            sPlaka = "UNKNOWN"
                                        End If
                                        sLine = "EQD+TE+" & sPlaka.Trim() + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                    End If
                                    sMPKOD = ""
                                    sMKKOD = ""
                                    sMSPKOD = ""
                                    sMKPKOD = ""
                                    sMURNKOD = ""

                                    ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                    MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, dt.Rows(0).Item("ITNBR").ToString, dt.Rows(0).Item("AMBKOD").ToString)

                                    ' Müşteri Ürün Kodu, Adı
                                    MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                    UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                            rowDt.Item("ITNBR").ToString, _
                                            sGate, sKumulDuzeyi, nMiktar, _
                                            rowDt.Item("SHPNO").ToString, _
                                            rowDt.Item("SHIPTO").ToString, _
                                            rowDt.Item("PICKNO").ToString)

                                    If sTmpItnbr <> sMURNKOD Then

                                        sSQL = " SELECT Sum(ShipMik) As ShipMik , Sum(KSAY) As KSAY " & _
                                                " From SHPPACK" + " " & _
                                                " WHERE SHPNO =" & nShipNo & _
                                                " AND SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & _
                                                " AND PICKNO=" & nPickList & _
                                                " AND isnull(ASNDRM,0)<>9" & _
                                                " AND ITNBR =" & sTirnakEkle(rowDt.Item("ITNBR").ToString)

                                        dtTemp = db.RunSql(sSQL)

                                        GetRowInfo(nMiktar, dtTemp, 0, "ShipMik")

                                        GetRowInfo(nKutuSayi, dtTemp, 0, "KSAY")

                                        sLine = "CPS+" & CStr(nRff) & "++" & "1'"

                                        objWriter.Write(sLine)

                                        nSatir += 1

                                        Inc(nRff)

                                        sLine = "PAC+" & Convert.ToString(nKutuSayi) & "++" + sMKKOD.Trim() & "::92'"

                                        objWriter.Write(sLine)

                                        nSatir += 1

                                        'sBirim = sLookup("DDDHCD", "MBDDREP", " DDZ969=" & Convert.ToString(rowDt.Item("SHPNO").ToString) & " AND DDAITX=" & sTirnakEkle(rowDt.Item("ITNBR").ToString))

                                        sBirim = Convert.ToString(rowDt.Item("STKOB").ToString)

                                        If sBirim = "AD" Then
                                            sQTYU = "PCE"
                                        ElseIf sBirim = "KG" Then
                                            sQTYU = "KGM"
                                        ElseIf sBirim = "MT" Then
                                            sQTYU = "MTR"
                                        End If

                                        sLine = "QTY+52:" & rowDt.Item("KMIK").ToString.Trim() & ":" & sQTYU.Trim() & "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1

                                        nIlkKutuNo = rowDt.Item("ILKKUTUNO").ToString

                                        nIlkPaletNo = rowDt.Item("ILKPALETNO").ToString

                                        sSQL = " (pltno <> '' Or pltno is not null Or pltno=0) " & " AND    itnbr = '" & rowDt.Item("ITNBR").ToString & "' " + " "

                                        sSQL = sSQL & "And pickno = " & rowDt.Item("PICKNO").ToString & " "

                                        If nLookup("COUNT(*)", "ETIKETDTY", sSQL) <> 0 Then

                                            sSQL = "SELECT * " & _
                                                        " FROM ETIKETDTY" + " " & _
                                                        " WHERE  (pltno = '' Or pltno=0)" & _
                                                        " AND KUTUETK=1" & _
                                                        " AND    itnbr = '" & rowDt.Item("ITNBR").ToString & "' " + " "

                                            sSQL = sSQL & " And pickno = " & rowDt.Item("PICKNO").ToString & " "

                                            dtPaletYok = db.RunSql(sSQL)

                                            For Each rowDtPaletYok As DataRow In dtPaletYok.Rows

                                                If rowDtPaletYok.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                                    sPusNo = Trim(Copy(rowDtPaletYok.Item("PUSNO").ToString, rowDtPaletYok.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd())

                                                Else
                                                    sPusNo = ""
                                                End If
                                                If sPusNo = "" Then
                                                    sLine = "GIR+3+" & StrFill(rowDtPaletYok.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML'"
                                                Else
                                                    sLine = "GIR+3+" & StrFill(rowDtPaletYok.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML++" + sPusNo & ":BU'"
                                                End If
                                                objWriter.Write(sLine)
                                                nSatir += 1
                                                nGir += 1

                                            Next rowDtPaletYok

                                            sSQL = "SELECT DISTINCT pltno " & _
                                                        " FROM ETIKETDTY" + " " & _
                                                        " WHERE (pltno<> '' Or pltno<>0) " & _
                                                        " AND KUTUETK=1" & _
                                                        " And Pusno=" & sTirnakEkle(rowDt.Item("PUSNO").ToString) & _
                                                        " And itnbr = '" + rowDt.Item("ITNBR").ToString & "' " & " "

                                            sSQL = sSQL & " And pickno = " + rowDt.Item("PICKNO").ToString + " "
                                            sSQL = sSQL & " Order By Pltno"

                                            dtPaletVar = db.RunSql(sSQL)

                                            For Each rowDtPaletVar As DataRow In dtPaletVar.Rows

                                                sLine = "PCI+17'"

                                                objWriter.Write(sLine)

                                                nSatir += 1

                                                sLine = "RFF+AAT:" & StrFill(rowDtPaletVar.Item("PLTNO").ToString, 9, "0", "L") + "'"
                                                objWriter.Write(sLine)
                                                nSatir += 1
                                                nRffAat += 1

                                                sSQL = "SELECT * " & _
                                                            " FROM ETIKETDTY" & " " & _
                                                            " WHERE  pltno='" & rowDtPaletVar.Item("PLTNO").ToString & "' " & _
                                                            " And KutuEtk=1" & _
                                                            " AND    itnbr = '" & rowDt.Item("ITNBR").ToString & "' " + " "

                                                sSQL = sSQL & " And pickno = " + rowDt.Item("PICKNO").ToString + " "
                                                sSQL = sSQL & " Order By Etkserino"

                                                dtTemp = db.RunSql(sSQL)

                                                For Each rowDtTemp As DataRow In dtTemp.Rows

                                                    If rowDtTemp.Item("PUSNO").ToString.Contains("%") Then

                                                        sPusNo = Trim(Copy(rowDtTemp.Item("PUSNO").ToString, rowDtTemp.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd())

                                                    Else

                                                        sPusNo = ""

                                                    End If

                                                    If sPusNo = "" Then

                                                        sLine = "GIR+3+" & StrFill(rowDtTemp.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML'"

                                                    Else

                                                        sLine = "GIR+3+" & StrFill(rowDtTemp.Item("ETKSERINO").ToString, 9, "0", "L") & ":ML++" + sPusNo & ":BU'"

                                                    End If

                                                    objWriter.Write(sLine)
                                                    nSatir += 1
                                                    nGir += 1

                                                Next rowDtTemp

                                            Next rowDtPaletVar

                                        Else

                                            nPaletKutuAdedi = rowDt.Item("KSAY").ToString - rowDt.Item("HKSAY").ToString

                                            If rowDt.Item("PSAY").ToString <> 0 Then

                                                If rowDt.Item("HKSAY").ToString <> 0 Then

                                                    For k = 1 To rowDt.Item("HKSAY").ToString

                                                        sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                                        If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                                            sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()

                                                        Else

                                                            sPusNo = ""

                                                        End If

                                                        If sPusNo = "" Then

                                                            sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"

                                                        Else

                                                            sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++" & sPusNo & ":BU'"

                                                        End If

                                                        objWriter.Write(sLine)
                                                        nSatir += 1
                                                        nGir += 1
                                                    Next
                                                End If

                                                For k = 1 To CInt(Convert.ToString(rowDt.Item("PSAY").ToString))
                                                    sLine = "PCI+17'"
                                                    objWriter.Write(sLine)
                                                    nSatir += 1

                                                    sLine = "RFF+AAT:" & StrFill(CStr(nRffAat), 9, "0", "L") & "'"
                                                    objWriter.Write(sLine)
                                                    nSatir += 1
                                                    nRffAat += 1

                                                    For nGCount = 1 To rowDt.Item("PKMIK").ToString
                                                        If nPaletKutuAdedi <> 0 Then
                                                            nPaletKutuAdedi = nPaletKutuAdedi - 1

                                                            sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                                            If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                                                sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()

                                                            Else

                                                                sPusNo = ""

                                                            End If

                                                            If sPusNo = "" Then

                                                                sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"

                                                            Else

                                                                sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"

                                                            End If

                                                            objWriter.Write(sLine)

                                                            nSatir += 1

                                                            nGir += 1

                                                        End If

                                                    Next

                                                Next

                                            Else

                                                For nGCount = 1 To rowDt.Item("KSAY").ToString

                                                    sGir = StrFill("1", 1, " ", "R") & StrFill(Convert.ToString(nGir), 3, "0", "L")

                                                    If rowDt.Item("PUSNO").ToString.IndexOf("%") <> 0 Then

                                                        sPusNo = Copy(rowDt.Item("PUSNO").ToString, rowDt.Item("PUSNO").ToString.IndexOf("%") + 1, 35).TrimEnd()

                                                    Else

                                                        sPusNo = ""

                                                    End If

                                                    If sPusNo = "" Then

                                                        sLine = "GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML'"

                                                    Else

                                                        sLine = ("GIR+3+" & StrFill(sGir, 9, "0", "L") & ":ML++") + sPusNo & ":BU'"

                                                    End If
                                                    objWriter.Write(sLine)
                                                    nSatir += 1
                                                    nGir += 1
                                                Next
                                            End If
                                        End If
                                        sLine = "LIN+++" & RTrim(sMURNKOD) & ":IN++0'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sLine = "QTY+12:" & Convert.ToString(nMiktar) & ":" & sQTYU + "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sLine = "ALI+TR'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sLine = "RFF+ON:" & sKontrat.Trim() & "'"
                                        objWriter.Write(sLine)
                                        nSatir += 1
                                        sTmpItnbr = sMURNKOD
                                        nMiktar = 0
                                        nKutuSayi = 0

                                        UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString, nPickList)

                                    End If

                                Next rowDt

                                sLine = "UNT+" & Convert.ToString(nSatir - 1) + "+" + Convert.ToString(nMsgNo - 1) + "'"
                                objWriter.Write(sLine)
                                nSatir += 1
                                sLine = "UNZ+" & Convert.ToString(nUNH) + "+" + sASN_No + "'"
                                objWriter.Write(sLine)
                                nSatir += 1
                                objWriter.Close()

                                sSQL = " UPDATE Shppack" & " " & _
                                        " SET Asnno=" & Convert.ToString(nASN_No) & _
                                            " WHERE  Shpno=" & nShipNo & _
                                            " And PICKNO=" & nPickList

                                db.RunSql(sSQL)
                                ' *********************************** ODETTE  AVIEXP ****************************
                            End If
                            'AsnTipkontrol
                        Case "PORSCHE", "SAAB", "TOFAS"

                            MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant

                            If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then
                                Directory.CreateDirectory(sASNDir + "\" + sSDRODID)
                            End If

                            If (File.Exists(MesajFile)) Then
                                File.Delete(MesajFile)
                            End If

                            objWriter = New System.IO.StreamWriter(MesajFile)
                            If sPlant = "THN" Then
                                sRAdr = "APM021"
                            ElseIf sPlant = "SDT" Then
                                sRAdr = "APM041"
                            ElseIf sPlant = "GBG" Then
                                sRAdr = "APM071"
                            End If
                            If (sASNTip = "SAAB") Then
                                sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") + " " & StrFill(sRCVODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill(sRAdr, 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R") & StrFill("", 19, " ", "R") & "AVIEXP-SAAB"
                            Else
                                sLine = "UNB " & StrFill(sRCVODID, 35, " ", "R") + " " & StrFill(sRCVODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASNODID, 35, " ", "R") + " " & StrFill(sASNODCD, 4, " ", "R") + " " & StrFill("", 14, " ", "R") + " " & StrFill(sASN_No, 14, " ", "R")
                            End If
                            objWriter.WriteLine(sLine)

                            sSQL = " SELECT * " & _
                                        " FROM SHPPACK" & _
                                        " WHERE  SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & _
                                        " AND isnull(ASNDRM,0)<>9"

                            sSQL = sSQL & " AND PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                            dt = db.RunSql(sSQL)

                            nMsgNo = 1

                            nUNH = 0

                            nPickList = Trim(Convert.ToString(row.Cells("PIckno").Text))

                            sFatura = Trim(Convert.ToString(row.Cells("InvNo").Text))

                            For Each rowDt As DataRow In dt.Rows

                                GetRowInfo(sGate, dt, 0, "KAPI")
                                sMPKOD = ""
                                sMKKOD = ""
                                sMSPKOD = ""
                                sMKPKOD = ""
                                sMURNKOD = ""
                                ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, dt.Rows(0).Item("ITNBR").ToString, dt.Rows(0).Item("AMBKOD").ToString)

                                ' Müşteri Ürün Kodu, Adı
                                MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                nSatir = 1

                                sLine = "UNH " & StrFill(CStr(nMsgNo), 14, " ", "R") + " " & StrFill("AVIEXP", 6, " ", "R") + " " & StrFill("3"c, 3, " ", "R") + " " & StrFill("", 3, " ", "R") + " " & StrFill("OD", 2, " ", "R")

                                objWriter.WriteLine(sLine)

                                nSatir += 1

                                nUNH += 1

                                sDTM = Now.Date.ToString("yyMMdd")

                                sTime = Now.ToString("HHmm")

                                If sLieferDrm = "0" Then

                                    sLine = "MID " & StrFill(nPickList, 17, " ", "R") + " " & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R")

                                ElseIf sLieferDrm = "1" Then

                                    sLine = "MID " & StrFill(sFatura, 17, " ", "R") + " " & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R")
                                End If
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                If sASNTip = "SAAB" Then

                                    sLine = ("CDT " & StrFill("", 165, " ", "R")) & StrFill(sDUNS, 35, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                ElseIf (sASNTip = "TOFAS") OrElse (sASNTip = "PORSCHE") Then

                                    sLine = "CDT " & StrFill(sDUNS, 20, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If
                                ' sLine:='CSG '+StrFill('',201,' ','R')+StrFill(sPlant,17,' ','R')+' '+StrFill(sGate,17,' ','R');

                                sLine = "CSG " & StrFill("", 201, " ", "R") & StrFill(sPlant, 17, " ", "R") + " " & StrFill(sGate, 17, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                If sASNTip = "PORSCHE" Then

                                    sLine = ("DTR " & StrFill(sDHAABZ, 17, " ", "R")) & StrFill("", 37, " ", "R") & StrFill(sCarrier, 20, " ", "R") & StrFill("", 199, " ", "R") & StrFill(sASN_No, 17, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If

                                nMiktar = rowDt.Item("SHIPMIK").ToString

                                UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                            rowDt.Item("ITNBR").ToString, _
                                            sGate, sKumulDuzeyi, nMiktar, _
                                            rowDt.Item("SHPNO").ToString, _
                                            rowDt.Item("SHIPTO").ToString, _
                                            rowDt.Item("PICKNO").ToString)

                                sQTYU = "PCE"

                                sLine = ("ARD " & StrFill(sMURNKOD, 35, " ", "R")) & StrFill("", 217, " ", "R") & StrFill(Convert.ToString(nMiktar), 10, "0", "L") + " " & StrFill(sQTYU, 3, " ", "R") + " " & StrFill(sKontrat, 17, " ", "R")
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' Kutu

                                If rowDt.Item("KSAY").ToString <> 0 Then

                                    sLine = ("ADP " & StrFill("", 115, " ", "R")) & StrFill("01", 9, " ", "R")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1

                                    sLine = ("TCO " & StrFill(sMKKOD, 35, " ", "R")) & StrFill("", 40, " ", "R") & StrFill(Convert.ToString(rowDt.Item("KSAY").ToString), 6, "0", "L") & StrFill("", 1, " ", "R") & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 10, "0", "L")
                                    objWriter.WriteLine(sLine)
                                    nSatir += 1
                                End If

                                sLine = ("UNT " & StrFill(Convert.ToString(nSatir), 6, "0", "L")) + " " & StrFill(Convert.ToString(nMsgNo), 14, " ", "R")
                                ' WriteLn(fMessages,sLine); inc(nSatir);
                                nMsgNo = nMsgNo + 1

                                If chkAsnYeni.Checked Then

                                    sSQL = " UPDATE SHPPACK" & _
                                                " SET ASNDRM=1" & _
                                                    " ,ASNNO=" & Convert.ToString(nASN_No) & _
                                                    " ,ASNMIK=" & Convert.ToString(nKumMiktar) & _
                                                " WHERE CUST=" & sTirnakEkle(sCustomer) & _
                                                    " AND SHIPTO=" & sTirnakEkle(sPlant) & _
                                                    " AND KAPI=" & sTirnakEkle(sGate) & _
                                                    " AND ITNBR=" & sTirnakEkle(rowDt.Item("ITNBR").ToString) & _
                                                    " AND isnull(ASNDRM,0)<>9"

                                    sSQL = sSQL & " AND SHPNO=" & sTirnakEkle(rowDt.Item("SHPNO").ToString)

                                    db.RunSql(sSQL)

                                End If

                            Next rowDt

                            sLine = "PVT " & StrFill(Convert.ToString(nBrtAgr), 12, "0", "L")
                            objWriter.WriteLine(sLine)
                            nSatir += 1

                            sLine = ("UNZ " & StrFill(Convert.ToString(nUNH), 6, "0", "L")) + " " & StrFill(sASN_No, 14, " ", "R")
                            ' WriteLn(fMessages,sLine); inc(nSatir);
                            ' ************************************** VDA-4913 *****************************
                            objWriter.Close()
                            'AsnTipkontrol

                        Case "FORD-VDA", "SEAT-VDA"

                            'MesajFile = sASNDir + "\" + sSDRODID + "\" + "A" + sASN_No + "." + sPlant
                            MesajFile = sASNDir + "\" + sSDRODID + "\" + "FORD.SEMH1AAS" & "." & Date.Now.ToString("ddMMyyyy") & "." & _
                               Date.Now.ToString("hhmmss") & ".EDI"

                            If Not Directory.Exists(sASNDir + "\" + sSDRODID) Then

                                Directory.CreateDirectory(sASNDir + "\" + sSDRODID)

                            End If

                            If (File.Exists(MesajFile)) Then
                                File.Delete(MesajFile)
                            End If

                            objWriter = New System.IO.StreamWriter(MesajFile)
                            ' if sASNTip='FORD-VDA' then sRCVODID:='FORD';
                            ' if sASNTip='FORD-VDA' then sRCVODID:=sUSERF3;
                            sRCVODID = sUSERF3

                            sLine = "71102" & StrFill(sRCVODID, 9, " ", "R") & StrFill(sDUNS & sUSERF2, 9, " ", "R") & StrFill(Convert.ToString(nASN_No), 5, "0", "L") & StrFill(Convert.ToString(nASN_No), 5, "0", "L") & Now.Date.ToString("yyMMdd") & StrFill(sConsignee, 9, " ", "R")

                            If sASNTip = "FORD-VDA" Then
                                sLine = StrFill(sLine, 128, " ", "R")
                            Else
                                sLine = StrFill(sLine, 126, " ", "R")
                            End If

                            objWriter.WriteLine(sLine)

                            sDTM = Now.Date.ToString("yyMMdd")

                            sTime = Now.ToString("HHmm")

                            nPickList = Convert.ToString(row.Cells("PIckno").Text)

                            sFatura = Convert.ToString(row.Cells("Invno").Text)

                            'sCarrierID = sLookup("Uf_Carrier", "Customer", " Cust_num=" & sTirnakEkle(sCustomer) & " AND Cust_Seq=" & sCustomerSeq)
                            sCarrierID = sLookup("Carrier", "SHPPACK", " pickno=" & sTirnakEkle(nPickList) & " ")

                            sTarih = DateAdd(DateInterval.Day, nYolSuresi, Now.Date).ToString("yyMMdd")
                            sTime = Now.ToString("HHmm")

                            nBrtAgrVda = Math.Round(nBrtAgr)
                            nNetAgrVda = Math.Round(nNetAgr)
                            If sASNTip = "FORD-VDA" Then


                                'Burası 1
                                ' sLine = "71202" & StrFill(nNavlunNo, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill(sCarrier, 14, " ", "R") & "01" & StrFill(nNavlunNo, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R")

                                'sLine = "71202" & StrFill(sIrsaliyeno, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill(sCarrier, 14, " ", "R") & "01" & StrFill(sSeferNo, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R") & StrFill(sTime, 4, " ", "R")
                                sLine = "71202" & StrFill(nShipNo, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill(sCarrier, 14, " ", "R") & "01" & StrFill(sSeferNo, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R") & StrFill(sTime, 4, " ", "R")
                                If sASNTip = "FORD-VDA" Then
                                    sLine = StrFill(sLine, 128, " ", "R")
                                Else
                                    sLine = StrFill(sLine, 126, " ", "R")
                                End If

                                objWriter.WriteLine(sLine)

                                If sayac713 = True Then


                                    'If sLieferDrm = "0" Then
                                    'sLine = "71302" & StrFill(sIrsaliyeno, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(row.Cells("KAPI").Text, 5, " ", "R") & "01" & StrFill("", 4, " ", "R") & StrFill("", 18, " ", "R") & StrFill(sPlant, 3, " ", "R") & StrFill(sPlaka, 8, " ", "R") & StrFill("", 17, " ", "R") & StrFill(nNavlunNo, 9, " ", "R")
                                    sLine = "71302" & StrFill(nShipNo, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(row.Cells("KAPI").Text, 5, " ", "R") & "01" & StrFill("", 4, " ", "R") & StrFill("", 18, " ", "R") & StrFill(sPlant, 3, " ", "R") & StrFill(sPlaka, 8, " ", "R") & StrFill("", 17, " ", "R") & StrFill(nNavlunNo, 9, " ", "R")
                                    'sLine = "71302" & StrFill(sIrsaliyeno, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "01" & StrFill("", 4, " ", "R") & StrFill(sKontrat, 8, " ", "R") & StrFill("", n713Bosluk, " ", "R") & StrFill(sPlant, 3, " ", "R") & StrFill(sPlaka, 8, " ", "R") & StrFill("", 17, " ", "R") & StrFill(nNavlunNo, 9, " ", "R")

                                    'ElseIf sLieferDrm = "1" Then
                                    '    sLine = "71302" & StrFill(sFatura, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "03" & StrFill("", 4, " ", "R") & StrFill(sKontrat, 8, " ", "R") & StrFill("", n713Bosluk, " ", "R") & StrFill(sPlant, 17, " ", "R")
                                    'End If

                                    sayac713 = False

                                    If sASNTip = "FORD-VDA" Then
                                        sLine = StrFill(sLine, 128, " ", "R")
                                    Else
                                        sLine = StrFill(sLine, 126, " ", "R")
                                    End If

                                    objWriter.WriteLine(sLine)
                                End If

                            Else

                                If sLieferDrm = "0" Then

                                    sLine = "71202" & StrFill(nPickList, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("01", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill("DK", 14, " ", "R") & StrFill(sDHAABZ, 2, " ", "R") & StrFill(sPlaka, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R")
                                    'sLine = "71202" & StrFill(nPickList, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("01", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill("DK", 14, " ", "R") & StrFill(sDHAABZ, 2, " ", "R") & StrFill(sSeferNo, 25, " ", "R") & StrFill(sPlaka, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R")
                                    sLine = StrFill(sLine, 126, " ", "R")

                                    objWriter.WriteLine(sLine)

                                    If sayac713 = True Then


                                        'If sLieferDrm = "0" Then

                                        sLine = "71302" & StrFill(nPickList, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "01" & StrFill("", 4, " ", "R") & StrFill("", 18, " ", "R") & StrFill(sPlant, 3, " ", "R") & StrFill(sPlaka, 8, " ", "R") & StrFill("", 17, " ", "R") & StrFill(nNavlunNo, 9, " ", "R")

                                        'ElseIf sLieferDrm = "1" Then
                                        '    sLine = "71302" & StrFill(sFatura, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "03" & StrFill("", 4, " ", "R") & StrFill(sKontrat, 8, " ", "R") & StrFill("", n713Bosluk, " ", "R") & StrFill(sPlant, 17, " ", "R")
                                        'End If

                                        sayac713 = False

                                        sLine = StrFill(sLine, 126, " ", "R")

                                        objWriter.WriteLine(sLine)
                                    End If

                                ElseIf sLieferDrm = "1" Then

                                    sLine = "71202" & StrFill(sFatura, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("01", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill("DK", 14, " ", "R") & StrFill(sDHAABZ, 2, " ", "R") & StrFill(sPlaka, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R")
                                    'sLine = "71202" & StrFill(sFatura, 8, "0", "L") & StrFill("", 3, " ", "R") & StrFill(sCarrierID, 14, " ", "R") & StrFill(sDTM, 6, " ", "R") & StrFill(sTime, 4, " ", "R") & StrFill(Convert.ToString(nBrtAgrVda), 7, "0", "L") & StrFill(Convert.ToString(nNetAgrVda), 7, "0", "L") & StrFill("01", 3, " ", "R") & StrFill(Convert.ToString(nKapAdet), 4, "0", "L") & StrFill("DK", 14, " ", "R") & StrFill(sDHAABZ, 2, " ", "R") & StrFill(sSeferNo, 25, " ", "R") & StrFill(sPlaka, 25, " ", "R") & StrFill("", 9, " ", "R") & StrFill(sTarih, 6, " ", "R")
                                    sLine = StrFill(sLine, 126, " ", "R")

                                    objWriter.WriteLine(sLine)

                                    If sayac713 = True Then


                                        'If sLieferDrm = "0" Then

                                        sLine = "71302" & StrFill(sFatura, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "01" & StrFill("", 4, " ", "R") & StrFill("", 18, " ", "R") & StrFill(sPlant, 3, " ", "R") & StrFill(sPlaka, 8, " ", "R") & StrFill("", 17, " ", "R") & StrFill(nNavlunNo, 9, " ", "R")

                                        'ElseIf sLieferDrm = "1" Then
                                        '    sLine = "71302" & StrFill(sFatura, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "03" & StrFill("", 4, " ", "R") & StrFill(sKontrat, 8, " ", "R") & StrFill("", n713Bosluk, " ", "R") & StrFill(sPlant, 17, " ", "R")
                                        'End If

                                        sayac713 = False

                                        sLine = StrFill(sLine, 126, " ", "R")

                                        objWriter.WriteLine(sLine)
                                    End If

                                End If

                            End If

                            'sLine = StrFill(sLine, 126, " ", "R")

                            'objWriter.WriteLine(sLine)

                            sSQL = " SELECT * " & _
                                        " FROM SHPPACK" & _
                                        " WHERE SHIPTO=" & sTirnakEkle(row.Cells("ShIpto").Text) & _
                                        " AND isnull(ASNDRM,0)<>9"

                            sSQL = sSQL & " AND PICKNO=" & Convert.ToString(row.Cells("PIckno").Text)

                            dt = db.RunSql(sSQL)

                            nMsgNo = 1

                            nUNH = 0

                            nPac = 0

                            Dim nDetaySatir As String

                            For Each rowDt As DataRow In dt.Rows

                                GetRowInfo(sGate, dt, 0, "KAPI")
                                sMPKOD = ""
                                sMKKOD = ""
                                sMSPKOD = ""
                                sMKPKOD = ""
                                sMURNKOD = ""
                                ' Müşteri Kutu, Palet, Separator, Kapak Kodları
                                MusteriAmbalajBilgileri("", sMPKOD, sMKKOD, sMSPKOD, sMKPKOD, rowDt.Item("ITNBR").ToString, rowDt.Item("AMBKOD").ToString)

                                ' Müşteri Ürün Kodu, Adı
                                MusteriMalzemeBilgileri(sMURNKOD, "", sKontrat, "", sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString)

                                nSatir = 1

                                n713Bosluk = 10

                                If RTrim(sKontrat).Length - 8 > 0 Then

                                    n713Bosluk = 10 - (RTrim(sKontrat).Length - 8)

                                End If


                                'If sayac713 = True Then


                                '    'If sLieferDrm = "0" Then

                                '    sLine = "71302" & StrFill(nPickList, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "01" & StrFill("", 4, " ", "R") & StrFill(sKontrat, 8, " ", "R") & StrFill("", n713Bosluk, " ", "R") & StrFill(sPlant, 3, " ", "R") & StrFill(sPlaka, 8, " ", "R") & StrFill("", 17, " ", "R") & StrFill(nNavlunNo, 9, " ", "R")

                                '    'ElseIf sLieferDrm = "1" Then
                                '    '    sLine = "71302" & StrFill(sFatura, 8, "0", "L") & StrFill(sDTM, 6, " ", "R") & StrFill(sGate, 5, " ", "R") & "03" & StrFill("", 4, " ", "R") & StrFill(sKontrat, 8, " ", "R") & StrFill("", n713Bosluk, " ", "R") & StrFill(sPlant, 17, " ", "R")
                                '    'End If

                                '    sayac713 = False

                                '    sLine = StrFill(sLine, 126, " ", "R")

                                '    objWriter.WriteLine(sLine)
                                'End If
                                nSatir += 1

                                nUNH += 1

                                nMiktar = rowDt.Item("SHIPMIK").ToString

                                UpdateKumul(nKumMiktar, sCustomer, sPlant, _
                                            rowDt.Item("ITNBR").ToString, _
                                            sGate, sKumulDuzeyi, nMiktar, _
                                            rowDt.Item("SHPNO").ToString, _
                                            rowDt.Item("SHIPTO").ToString, _
                                            rowDt.Item("PICKNO").ToString)

                                'sQTYU = "ST"

                                sQTYU = rowDt.Item("STKOB").ToString

                                If sQTYU = "AD" Then
                                    'sQTYU = "EA"  '13.06.14 
                                    sQTYU = "ST"
                                End If

                                nDetaySatir += 1
                                nDetaySatir = CStr(nDetaySatir).PadLeft(3, "0")

                                sLine = "71403" & StrFill(sMURNKOD, 22, " ", "R") & StrFill(rowDt.Item("ITNBR").ToString, 22, " ", "R") & "999" & StrFill(Convert.ToString(nMiktar) & "000", 13, "0", "L") & StrFill(sQTYU, 2, " ", "R") & StrFill(Convert.ToString(nKumMiktar) & "000", 13, "0", "L") & StrFill(sQTYU, 2, " ", "R") & StrFill("", 4, " ", "R") & nDetaySatir

                                If Copy(sGate, 0, 3) = "KOD" Then

                                    sLine = StrFill(sLine, 105, " ", "R")
                                    sLine = sLine & "E"

                                    If sASNTip = "FORD-VDA" Then
                                        sLine = StrFill(sLine, 128, " ", "R")
                                    Else
                                        sLine = StrFill(sLine, 126, " ", "R")
                                    End If
                                Else

                                    If sASNTip = "FORD-VDA" Then
                                        sLine = StrFill(sLine, 128, " ", "R")
                                    Else
                                        sLine = StrFill(sLine, 126, " ", "R")
                                    End If
                                End If
                                objWriter.WriteLine(sLine)
                                nSatir += 1
                                ' Kutu

                                UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString, nPickList)

                                If rowDt.Item("KSAY").ToString <> 0 Then
                                    If Math.Floor(rowDt.Item("KSAY").ToString / 99) > 0 Then
                                        For i = 1 To Math.Floor(rowDt.Item("KSAY").ToString / 99)

                                            sLine = "71503" & StrFill(sMKKOD, 22, " ", "R") & StrFill(rowDt.Item("KKOD").ToString, 22, " ", "R") & StrFill("99", 13, "0", "L") & StrFill("001", 3, " ", "R")

                                            If (sSDRODID = "O0013006576LOREL") OrElse (sSDRODID = "O0013000057AUDI-INGR21") OrElse (sSDRODID = "O0013000001VW      R41") OrElse (sSDRODID = "O0013001198SCHENKER    NT") Then

                                                sLine = sLine & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 10, "0", "L") & "000"

                                            Else

                                                sLine = sLine & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 13, "0", "L")

                                            End If

                                            sLine = sLine & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R") + "S"c

                                            If sASNTip = "FORD-VDA" Then
                                                sLine = StrFill(sLine, 128, " ", "R")
                                            Else
                                                sLine = StrFill(sLine, 126, " ", "R")
                                            End If
                                            objWriter.WriteLine(sLine)
                                            nSatir += 1
                                            nPac += 1
                                        Next

                                    End If

                                    If rowDt.Item("KSAY").ToString Mod 99 > 0 Then

                                        sLine = ("71503" & StrFill(sMKKOD, 22, " ", "R")) & StrFill(rowDt.Item("KKOD").ToString, 22, " ", "R") & StrFill(CStr(rowDt.Item("KSAY").ToString Mod 99), 13, "0", "L") & StrFill("001", 3, " ", "R")

                                        If (sSDRODID = "O0013006576LOREL") OrElse (sSDRODID = "O0013000057AUDI-INGR21") OrElse (sSDRODID = "O0013000001VW      R41") OrElse (sSDRODID = "O0013001198SCHENKER    NT") Then

                                            sLine = sLine & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 10, "0", "L") & "000"

                                        Else

                                            sLine = sLine & StrFill(Convert.ToString(rowDt.Item("KMIK").ToString), 13, "0", "L")

                                        End If

                                        sLine = sLine & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R") + "S"c

                                        If sASNTip = "FORD-VDA" Then
                                            sLine = StrFill(sLine, 128, " ", "R")
                                        Else
                                            sLine = StrFill(sLine, 126, " ", "R")
                                        End If

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                        nPac += 1

                                    End If

                                End If
                                ' Palet

                                If rowDt.Item("PSAY").ToString <> 0 Then

                                    For i = 1 To (rowDt.Item("PSAY").ToString / 99)

                                        sLine = "71503" & StrFill(sMPKOD, 22, " ", "R") & StrFill(rowDt.Item("PKOD").ToString, 22, " ", "R") & StrFill("99", 13, "0", "L") & StrFill("001", 3, " ", "R") & StrFill("0", 13, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R") + "M"c

                                        If sASNTip = "FORD-VDA" Then
                                            sLine = StrFill(sLine, 128, " ", "R")
                                        Else
                                            sLine = StrFill(sLine, 126, " ", "R")
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nPac += 1
                                    Next

                                    If rowDt.Item("PSAY").ToString <> 99 Then

                                        sLine = "71503" & StrFill(sMPKOD, 22, " ", "R") & StrFill(rowDt.Item("PKOD").ToString, 22, " ", "R") & StrFill(Convert.ToString(rowDt.Item("PSAY").ToString Mod 99), 13, "0", "L") & StrFill("001", 3, " ", "R") & StrFill("0", 13, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R") + "M"

                                        If sASNTip = "FORD-VDA" Then
                                            sLine = StrFill(sLine, 128, " ", "R")
                                        Else
                                            sLine = StrFill(sLine, 126, " ", "R")
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nPac += 1
                                    End If
                                End If
                                ' Kapak

                                If rowDt.Item("KPMIK").ToString <> 0 Then

                                    For i = 1 To (rowDt.Item("KPMIK").ToString / 99)

                                        sLine = ("71503" & StrFill(sMKPKOD, 22, " ", "R")) & StrFill(rowDt.Item("KPKOD").ToString, 22, " ", "R") & StrFill("99", 13, "0", "L") & StrFill("001", 3, " ", "R") & StrFill("0", 13, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R")

                                        If sASNTip = "FORD-VDA" Then
                                            sLine = StrFill(sLine, 128, " ", "R")
                                        Else
                                            sLine = StrFill(sLine, 126, " ", "R")
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nPac += 1
                                    Next

                                    If rowDt.Item("KPMIK").ToString <> 99 Then

                                        sLine = "71503" & StrFill(sMKPKOD, 22, " ", "R") & StrFill(rowDt.Item("KPKOD").ToString, 22, " ", "R") & StrFill(Convert.ToString(rowDt.Item("KPMIK").ToString Mod 99), 13, "0", "L") & StrFill("001", 3, " ", "R") & StrFill("0", 13, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R")

                                        If sASNTip = "FORD-VDA" Then
                                            sLine = StrFill(sLine, 128, " ", "R")
                                        Else
                                            sLine = StrFill(sLine, 126, " ", "R")
                                        End If
                                        objWriter.WriteLine(sLine)
                                        nSatir += 1
                                        nPac += 1
                                    End If
                                End If
                                ' Separator

                                If rowDt.Item("SPMIK").ToString <> 0 Then

                                    For i = 1 To (rowDt.Item("SPMIK").ToString / 99)

                                        sLine = "71503" & StrFill(sMSPKOD, 22, " ", "R") & StrFill(rowDt.Item("SPKOD").ToString, 22, " ", "R") & StrFill("99", 13, "0", "L") & StrFill("001", 3, " ", "R") & StrFill("0", 13, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R")

                                        If sASNTip = "FORD-VDA" Then
                                            sLine = StrFill(sLine, 128, " ", "R")
                                        Else
                                            sLine = StrFill(sLine, 126, " ", "R")
                                        End If

                                        objWriter.WriteLine(sLine)

                                        nSatir += 1

                                        nPac += 1

                                    Next

                                    sLine = "71503" & StrFill(sMSPKOD, 22, " ", "R") & StrFill(rowDt.Item("SPKOD").ToString, 22, " ", "R") & StrFill(Convert.ToString(rowDt.Item("SPMIK").ToString Mod 99), 13, "0", "L") & StrFill("001", 3, " ", "R") & StrFill("0", 13, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 9, "0", "L") & StrFill("0", 12, "0", "L") & StrFill("1", 1, "0", "L") & StrFill("", 15, " ", "R")

                                    If sASNTip = "FORD-VDA" Then
                                        sLine = StrFill(sLine, 128, " ", "R")
                                    Else
                                        sLine = StrFill(sLine, 126, " ", "R")
                                    End If

                                    objWriter.WriteLine(sLine)

                                    nSatir += 1

                                    nPac += 1

                                End If

                                nMsgNo = nMsgNo + 1

                                'UpdateShpPack(nASN_No, nKumMiktar, sCustomer, sPlant, sGate, rowDt.Item("ITNBR").ToString, nPickList)

                            Next rowDt

                            sLine = "71902" & StrFill("1", 7, "0", "L") & StrFill("1", 7, "0", "L") & StrFill(Convert.ToString(nUNH), 7, "0", "L") & _
                            StrFill(Convert.ToString(nUNH), 7, "0", "L") & StrFill(Convert.ToString(nPac), 7, "0", "L") & StrFill("0", 7, "0", "L") & _
                            StrFill("0", 7, "0", "L") & StrFill("1", 7, "0", "L") & StrFill("0", 7, "0", "L")
                            ' 54. karakter 1 yerine 0 olarak degistirildi. KADER 04/11/2004
                            If sASNTip = "FORD-VDA" Then
                                sLine = StrFill(sLine, 128, " ", "R")
                            Else
                                sLine = StrFill(sLine, 126, " ", "R")
                            End If

                            objWriter.WriteLine(sLine)

                            nSatir += 1

                            objWriter.Close()

                    End Select
                    'If chkAsnYeni.Checked = True Then
                    '    sSQL = " UPDATE ShpPack" & _
                    '                              " SET  ASNDRM = 1 ,ASNMIK='" & nKumMiktar & "' ,Asnno=" & Convert.ToString(nASN_No) & _
                    '                              " WHERE Pickno=" & nPickList

                    '    db.RunSql(sSQL)
                    'Else
                    '    sSQL = " UPDATE ShpPack" & _
                    '                          " SET  ASNDRM = 1 ,Asnno=" & Convert.ToString(nASN_No) & _
                    '                          " WHERE Pickno=" & nPickList

                    '    db.RunSql(sSQL)

                    'End If

                    ' if- CheckBox True
                Next

            End If

            


            ' For
            btnListele_Click(sender, e)
            ' if message

        Catch ex As Exception

            mError.Text = ex.ToString

        End Try
    End Sub

    Private Sub btnCikis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCikis.Click
        Me.Close()
    End Sub

    Private Sub btnListele_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListele.Click
        Dim sSQL As String

        Try

            If (chkAsnTekrar.Checked) AndAlso (txtCekmeListeNo1.Text = "") Then

                MessageBox.Show(" Çeki Liste Numarasını Girmek Zorundasınız!...", "Ekip Mapics", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)

                Exit Sub

            End If

            'StatusBar.Panels(0).Text = "ASN Bilgileri Hazırlanıyor.Lütfen Bekleyiniz..."
            'StatusBar.Refresh()

            'BAR.Progress = 0
            '

            sSQL = " Select *" & _
                        " From AsnGonderim"
            sSQL = sSQL & " WHERE  1=1"

            If txtFaturaNo1.Text <> "" Then

                sSQL = sSQL & " AND Invno=" & Convert.ToString(txtFaturaNo1.Text)

            End If

            If chkAsnYeni.Checked Then

                sSQL = sSQL & " AND isnull(ASNDRM,0)=0 "

            Else

                sSQL = sSQL & " AND isnull(ASNDRM,0)=1 "

            End If

            If (txtTeslimAlan1.Text <> "") Then

                sSQL = sSQL & " AND SHIPTO=" & sTirnakEkle(txtTeslimAlan1.Text)

            End If

            If (txtMusteri1.Text <> "") AndAlso (txtMusteri2.Text <> "") Then

                sSQL = sSQL & " AND CUST<=" & sTirnakEkle(txtMusteri2.Text) & " AND CUST>=" & sTirnakEkle(txtMusteri1.Text)

            ElseIf (txtMusteri1.Text <> "") Then

                sSQL = sSQL & " AND CUST=" & sTirnakEkle(txtMusteri1.Text)

            End If

            If (txtCekmeListeNo1.Text <> "") AndAlso (txtCekmeListeNo2.Text <> "") Then

                sSQL = sSQL & " And pickno >=  " & Convert.ToString(txtCekmeListeNo1.Text) & " And pickno   <= " + Convert.ToString(txtCekmeListeNo2.Text)

            ElseIf (txtCekmeListeNo1.Text <> "") Then

                sSQL = sSQL & " And pickno   =  " & Convert.ToString(txtCekmeListeNo1.Text)

            End If

            If (txtFaturaNo1.Text <> "") AndAlso (txtFaturaNo2.Text <> "") Then

                sSQL = sSQL & " AND Invno>=" & Convert.ToString(txtFaturaNo1.Text) & " AND Invno<=" & Convert.ToString(txtFaturaNo2.Text)

            ElseIf (txtFaturaNo1.Text <> "") Then

                sSQL = sSQL & " AND Invno=" & Convert.ToString(txtFaturaNo1.Text)

            End If

            If (dtTarih1.Text <> "") AndAlso (dtTarih2.Text <> "" AndAlso chkTarihAraligi.Checked) Then

                sSQL = sSQL & " AND Sevktar>=" & sTarih(dtTarih1.Value) & _
                            " AND Sevktar<=" & sTarih(dtTarih2.Value)

            ElseIf (dtTarih1.Text <> "") Then

                sSQL = sSQL & " AND Sevktar=" & sTarih(dtTarih1.Value)

            End If

            dt = db.RunSql(sSQL)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                grdDesAdv.DataSource = dt

                Duzenle(grdDesAdv)

            Else

                grdDesAdv.DataSource = Nothing

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub chkTarihAraligi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTarihAraligi.CheckedChanged
        dtTarih2.Enabled = chkTarihAraligi.Checked
    End Sub

    Private Sub frmAsnGonder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        chkAsnYeni.Checked = True

        dtTarih1.Value = Now

        dtTarih2.Value = Now
    End Sub

    Private Sub txtMusteri1_ButtonClick(sender As System.Object, e As System.EventArgs) Handles txtMusteri1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Taraf,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Taraf", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri2_ButtonClick(sender As System.Object, e As System.EventArgs) Handles txtMusteri2.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Taraf,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Taraf", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Public Sub UpdateKumul2(ByRef nKumMiktar As Integer, _
                         ByVal sCustomer As String, _
                         ByVal sPlant As String, _
                         ByVal sCustSeq As Integer, _
                         ByVal sMalzeme As String, _
                         ByVal sGate As String, _
                         ByVal sKumulDuzeyi As String, _
                         ByVal nMiktar As Integer, _
                         ByVal nShpNo As Integer, _
                         ByVal sShipTo As String, _
                         ByVal nPickNo As Integer _
                         )

        Dim sSql As String

        Try

            If chkAsnYeni.Checked Then

                sSql = " CANBK=" & sTirnakEkle(sCustomer) & _
                        " AND B9CDK=" & sTirnakEkle(sPlant) & _
                        " AND AENBK = " & sTirnakEkle(sCustSeq) & _
                        " AND AITXK=" & sTirnakEkle(sMalzeme)

                If sKumulDuzeyi = 1 Then

                    sSql = sSql & " And GATE=" & sTirnakEkle(sGate)

                End If

                nKumMiktar = nLookup("ASNKUM", "OFFITEMBL", sSql)


                nKumMiktar = nKumMiktar + nMiktar

                sSql = " UPDATE " & "OFFITEMBL" & _
                            " SET ASNKUM=" & Convert.ToString(nKumMiktar) & _
                            " WHERE CANBK=" & sTirnakEkle(sCustomer) & _
                            " AND B9CDK=" & sTirnakEkle(sPlant) & _
                            " AND AENBK=" & sTirnakEkle(sCustSeq) & _
                            " AND AITXK=" & sTirnakEkle(sMalzeme)

                If sKumulDuzeyi = 1 Then

                    sSql = sSql & " And Gate=" & sTirnakEkle(sGate)

                End If

                db.RunSql(sSql)

            Else

                sSql = "  SHPNO=" & Convert.ToString(nShpNo) & _
                        " AND SHIPTO=" & sTirnakEkle(sShipTo) & _
                        " AND KAPI=" & sTirnakEkle(sGate) & _
                        " AND ITNBR=" & sTirnakEkle(sMalzeme) & _
                        " AND isnull(ASNDRM,0)<>9"


                sSql = sSql & " AND PICKNO=" & Convert.ToString(nPickNo)


                nKumMiktar = nLookup("ASNMIK", "Shppack", sSql)

            End If


        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    #End Region 'Methods

End Class