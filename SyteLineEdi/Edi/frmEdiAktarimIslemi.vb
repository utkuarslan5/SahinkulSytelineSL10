Imports System
Imports System.Collections
Imports System.IO

Imports Microsoft.Office.Interop
Imports Microsoft.VisualBasic

Public Class frmEdiAktarimIslemi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt As New DataTable
    Dim dt1 As New DataTable

    'Keep the application object and the workbook object global, so you can
    'retrieve the data in Button2_Click that was set in Button1_Click.
    Dim objApp As Excel.Application
    Dim objBook As Excel._Workbook
    Dim sbckdir As String
    Dim sDefGateID As String = ""
    Dim sDELTyp As String
    Dim sDELTyp1 As String
    Dim sDtmFrmt As String
    Dim sDTMJFrmt As String
    Dim sDTMJTyp As String
    Dim sDTMJ_End As String
    Dim sDTMJ_Str As String
    Dim sDtmTyp As String
    Dim sFtxTyp As String
    Dim sItnbr As String
    Dim sKanban As String
    Dim sKapi As String
    Dim sKontratNo As String
    Dim sLocATyp As String
    Dim sMHandCode As String
    Dim sMIssuer As String
    Dim sMsgDte As String
    Dim sMsgEndDte As String
    Dim sMsgNam As String
    Dim sMsgNum As String
    Dim sMsgRef As String
    Dim sMsgStrDte As String
    Dim sMsgTxt1 As String
    Dim sMsgTxt2 As String
    Dim sMsgTxt3 As String
    Dim sMsgTyp As String
    Dim Sorgu As String
    Dim sPUSNo As String
    Dim sQTY As String
    Dim sQTYDec As String
    Dim sQtyTyp As String
    Dim sRffCTyp As String
    Dim sRFFDTyp As String
    Dim sRFFD_Ref As String

    #End Region 'Fields

    #Region "Constructors"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    #End Region 'Constructors

    #Region "Methods"

    Public Function ConvertEDIDate(ByVal Frmt As String, ByVal Dte As String, ByVal DteTip As String) As String
        Dim result As String = ""
        Dim sStrDt As String = ""
        Dim sEndDt As String = ""
        Dim sEDIDt As String = ""
        Dim nyil1 As Integer
        Dim nhafta1 As Integer
        Dim nyil As UShort
        Dim nay As UShort
        Dim ngun As UShort
        Dim dtHBas_Tar As Date
        Dim dtHBit_Tar As Date
        Try
            If Dte = "0" Or Dte = "" Then

                sEDIDt = ""

            Else

                If Frmt = "102" Then

                    sEDIDt = Dte

                ElseIf Frmt = "610" Then

                    sEDIDt = Dte & "01"

                ElseIf Frmt = "203" Then

                    sEDIDt = Dte.Substring(1 - 1, 8)

                ElseIf Frmt = "201" Then

                    sEDIDt = "20" & Dte.Substring(1 - 1, 6)

                ElseIf Frmt = "101" Then

                    sEDIDt = "20" & Dte

                ElseIf Frmt = "DEL" Then

                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then
                        sEDIDt = "20" & Dte
                    End If

                ElseIf Frmt = "VDA" Then

                    ' YYMMDD
                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then
                        sEDIDt = "20" & Dte
                    End If

                ElseIf Frmt = "VD1" Then
                    ' YY00WW
                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then

                        nyil1 = CInt("20" & Dte.Substring(1 - 1, 2))

                        nhafta1 = CInt(Dte.Substring(5 - 1, 2))

                        dtHBas_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        dtHBit_Tar = dtHBas_Tar.AddDays(6)

                        sStrDt = dtHBas_Tar.ToString("yyyyMMdd")

                        sEndDt = dtHBit_Tar.ToString("yyyyMMdd")

                        If DteTip = "BA" Then
                            sEDIDt = sStrDt
                        ElseIf DteTip = "BI" Then
                            sEDIDt = sEndDt
                        End If

                    End If

                ElseIf Frmt = "VD2" Then

                    ' YYMM00
                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then
                        sEDIDt = "20" & Dte.Substring(1 - 1, 4) & "01"
                    End If

                ElseIf Frmt = "VD3" Then
                    ' YYWWWW
                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then

                        nyil1 = CInt("20" & Dte.Substring(1 - 1, 2))

                        nhafta1 = CInt(Dte.Substring(3 - 1, 2))

                        dtHBas_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        nhafta1 = CInt(Dte.Substring(5 - 1, 2))

                        dtHBit_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        sStrDt = dtHBas_Tar.ToString("yyyyMMdd")

                        sEndDt = dtHBit_Tar.ToString("yyyyMMdd")

                        If DteTip = "BA" Then
                            sEDIDt = sStrDt
                        ElseIf DteTip = "BI" Then
                            sEDIDt = sEndDt
                        End If

                    End If

                ElseIf Frmt = "716" Then

                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then

                        nyil1 = CInt(Dte.Substring(1 - 1, 4))

                        nhafta1 = CInt(Dte.Substring(5 - 1, 2))

                        dtHBas_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        nyil1 = CInt(Dte.Substring(7 - 1, 4))

                        nhafta1 = CInt(Dte.Substring(11 - 1, 2))

                        dtHBit_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        sStrDt = dtHBas_Tar.ToString("yyyyMMdd")

                        sEndDt = dtHBit_Tar.ToString("yyyyMMdd")

                        If DteTip = "BA" Then
                            sEDIDt = sStrDt
                        ElseIf DteTip = "BI" Then
                            sEDIDt = sEndDt
                        End If

                    End If

                ElseIf Frmt = "616" Then

                    If (Dte <> "") AndAlso (Convert.ToSingle(Dte) <> 0) Then

                        nyil1 = CInt(Dte.Substring(1 - 1, 4))

                        nhafta1 = CInt(Dte.Substring(5 - 1, 2))

                        dtHBas_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        dtHBit_Tar = dtHBas_Tar.AddDays(6)

                        sStrDt = dtHBas_Tar.ToString("yyyyMMdd")

                        sEndDt = dtHBit_Tar.ToString("yyyyMMdd")

                        If DteTip = "BA" Then
                            sEDIDt = sStrDt
                        ElseIf DteTip = "BI" Then
                            sEDIDt = sEndDt
                        End If

                    End If

                ElseIf Frmt = "OTS" Then

                    nyil = CInt(Dte.Substring(6, 4))

                    nay = CInt(Dte.Substring(3, 2))

                    ngun = CInt(Dte.Substring(0, 2))

                    sEDIDt = DateSerial(nyil, nay, ngun).ToString("yyyyMMdd")

                ElseIf Frmt = "TYT" Then
                    ' DD.MM.YYYY
                    nyil = CInt(Dte.Substring(7 - 1, 4))

                    nay = CInt(Dte.Substring(4 - 1, 2))

                    ngun = CInt(Dte.Substring(1 - 1, 2))

                    sEDIDt = DateSerial(nyil, nay, ngun).ToString("yyyyMMdd")

                ElseIf Frmt = "TY1" Then
                    ' YYYYWW
                    If (Dte <> "") Then

                        nyil1 = CInt(Dte.Substring(1 - 1, 4))

                        nhafta1 = CInt(Dte.Substring(6 - 1, 2))

                        dtHBas_Tar = EncodeDateWeek(nyil1, nhafta1, 1)

                        dtHBit_Tar = dtHBas_Tar.AddDays(6)

                        sStrDt = dtHBas_Tar.ToString("yyyyMMdd")

                        sEndDt = dtHBit_Tar.ToString("yyyyMMdd")

                        If DteTip = "BA" Then
                            sEDIDt = sStrDt
                        ElseIf DteTip = "BI" Then
                            sEDIDt = sEndDt
                        End If

                    End If
                ElseIf Frmt = "TY2" Then
                    ' DD.MM.YY

                    nyil = CInt("20" & Dte.Substring(7 - 1, 2))

                    nay = CInt(Dte.Substring(4 - 1, 2))

                    ngun = CInt(Dte.Substring(1 - 1, 2))

                    sEDIDt = DateSerial(nyil, nay, ngun).ToString("yyyyMMdd")

                End If

            End If

            If sEDIDt = "" Then
                result = "0"
            Else
                result = sEDIDt
            End If

            Return result

        Catch ex As Exception

            Throw ex

            Return result

        End Try
    End Function

    Public Function DataAktar(ByVal sFName As String) As Short
        Dim result As Short
        Dim TF_Name As String = ""
        Dim sData As String = ""
        Dim sData1 As String = ""
        Dim sTip As String = ""
        Dim sGH As String = ""
        Dim sHataNedeni As String = ""
        Dim bKayit As Boolean
        Dim bkumupdate As Boolean
        Dim sVersion As String = ""
        Dim sRelease As String = ""
        Dim sSender As String = ""
        Dim sReceiver As String = ""
        Dim sMsgComRef As String = ""
        Dim sRFFAEM As String = ""
        Dim sNADCN As String = ""
        Dim sNADTyp As String = ""
        Dim sDUNSID As String = ""
        Dim sHandlingCode As String = ""
        Dim sNADATyp As String = ""
        Dim sPlantID__1 As String = ""
        Dim sSevkYeri As String = ""
        Dim sL3PREF As String = ""
        Dim sQTYATyp As String = ""
        Dim sDELTyp As String = ""
        Dim sDELTyp1 As String = ""
        Dim sDElFreq As String = ""
        Dim sQTYA As String = ""
        Dim sQTYA_Dec As String = ""
        Dim sQTY1 As String = ""
        Dim sDTMLTyp As String = ""
        Dim sDTML_Str As String = ""
        Dim sDTML_End As String = ""
        Dim sDTML_Frmt As String = ""
        Dim sMsgStatu As String = ""
        Dim sARITyp As String = ""
        Dim nLine As Integer
        Dim i As Integer
        Dim nCount As Integer
        Dim J As Integer

        Dim sGunTar(14) As String

        Dim bRFFAEM As Boolean
        Dim nCumQty As Double
        Dim nDelQty As Double
        Dim nPrevCum As Double
        Dim sPlantid As String = ""
        Dim objStreamReader As StreamReader

        result = 0

        sSender = ""
        sReceiver = ""
        sMsgTyp = ""
        sVersion = ""
        sRelease = ""
        nLine = 1
        bKayit = False
        bkumupdate = False

        objStreamReader = New StreamReader(sFName)

        sData = objStreamReader.ReadLine

        Try

            Do While Not sData Is Nothing

                sTip = Copy(sData, 1 - 1, 4).TrimEnd()

                If sTip = "UNB" Then
                    sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                    sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                End If

                If sTip = "UNH" Then
                    sMsgTyp = Copy(sData, 20 - 1, 6).TrimEnd()
                    sVersion = Copy(sData, 27 - 1, 3).TrimEnd()
                    sRelease = Copy(sData, 31 - 1, 3).TrimEnd()
                    sMsgComRef = Copy(sData, 45 - 1, 35).TrimEnd()
                End If
                If (nLine = 1) AndAlso (Copy(sData, 1 - 1, 10) = "PROGRAM NO") Then
                    If (nLine = 1) AndAlso (Copy(sData, 12 - 1, 12) = "FABRIKA KODU") Then
                        sMsgTyp = "CSV-HAFTALIK-OTS"
                    ElseIf (nLine = 1) AndAlso (Copy(sData, 12 - 1, 22) = "SEVK. CETVELI YAY. TAR") Then
                        sMsgTyp = "CSV-GUNLUK-OTS"
                    End If
                ElseIf (nLine = 1) AndAlso (Copy(sData, 1 - 1, 3) = "511") Then
                    sMsgTyp = "VDA-HFT"
                ElseIf (nLine = 1) AndAlso (Copy(sData, 1 - 1, 3) = "551") Then
                    sMsgTyp = "VDA-GNL"
                ElseIf (nLine = 1) AndAlso (sData = """SupplierCode"",""SupplierName"",""DockCode"",""TransmissionDate"",""PartColourCode"",""PartDesc"",""KanbanNo"",""PartNo"",""OrderLot"",""UsageWeekNo"",""TotalPCS"",""LastManifest"",""LeftToOrder""") Then
                    sMsgTyp = "TOYOTA-HFT"
                ElseIf (nLine = 1) AndAlso (sData = """SupplierCode"",""SupplierName"",""DockCode"",""TransmissionDate"",""PartColourCode"",""PartDesc"",""KanbanNo"",""PartNo"",""OrderLot"",""CollectionDate"",""CollectionTime"",""InvoiceNo"",""ManifestNo"",""TotalPCS""") Then
                    sMsgTyp = "TOYOTA-GNL"
                End If
                ' **************************DELFOR D 97A  (Ford OTOSAN'dan farklý olanlar için) *********************************
                ' ************************* Yani Opel, VW, SAAB... icin*********************
                If (sMsgTyp = "DELFOR") AndAlso (sVersion = "D") AndAlso (sRelease = "97A") AndAlso (sSender <> "9041680202001") Then
                    result = 1
                    If bKayit AndAlso ((sTip = "UNH") OrElse (sTip = "LIN") OrElse (sTip = "QTYA") OrElse (sTip = "SCC") OrElse (sTip = "NADA")) Then
                        If (sDELTyp <> "2") AndAlso (sDELTyp <> "3") Then
                            EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                             sKanban, "", sSevkYeri, "")
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        Else
                            bKayit = False
                        End If
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgTyp = Copy(sData, 20 - 1, 6).TrimEnd()
                        sVersion = Copy(sData, 27 - 1, 3).TrimEnd()
                        sRelease = Copy(sData, 31 - 1, 3).TrimEnd()
                        sMsgComRef = Copy(sData, 45 - 1, 35).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sMsgNam = "241" Then
                            sMsgNum = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "158" Then
                            sMsgStrDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "159" Then
                            sMsgEndDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        ' 102:YYYYMMDD
                        sDtmFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "FTX" Then
                        sFtxTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sFtxTyp = "AAI" Then
                            sMsgTxt1 = Copy(sData, 39 - 1, 70).TrimEnd()
                            sMsgTxt2 = Copy(sData, 110 - 1, 70).TrimEnd()
                            sMsgTxt3 = Copy(sData, 180 - 1, 70).TrimEnd()
                        End If
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If (sNADTyp = "SU") OrElse (sNADTyp = "SE") Then
                            sDUNSID = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        If (sNADTyp = "MI") Then
                            sMIssuer = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "NADA" Then
                        sNADATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sNADATyp = "ST" Then
                            ' GM,SAAB
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sNADATyp = "CN" Then
                            ' VW,SK
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sNADATyp = "DP" Then
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                            ' R.Bosch
                        End If
                    ElseIf sTip = "LIN" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 16 - 1, 35).TrimEnd()
                    ElseIf sTip = "PIA" Then
                        sKanban = Copy(sData, 9 - 1, 15).TrimEnd()
                    ElseIf sTip = "LOCA" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") Then
                            sKapi = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "159") Then
                            sMHandCode = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "18") Then
                            sSevkYeri = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                    ElseIf sTip = "RFFC" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "ON") OrElse (sRffCTyp = "CT") Then
                            sKontratNo = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "RFFE" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "CT") Then
                            sKontratNo = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "QTY" Then
                        sQtyTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sQtyTyp = "3") OrElse (sQtyTyp = "70") Then
                            If ((sSender = "RBEDA0") AndAlso (sQtyTyp = "70")) OrElse ((sSender <> "RBEDA0") AndAlso ((sQtyTyp = "3") OrElse (sQtyTyp = "70"))) Then
                                If ((sSender = "5060") OrElse (sSender = "506")) AndAlso (sQtyTyp = "3") Then
                                Else
                                    sQTY = Copy(sData, 9 - 1, 12).TrimEnd()
                                    sQTYDec = Copy(sData, 21 - 1, 3).TrimEnd()
                                End If
                            End If
                        End If
                    ElseIf sTip = "DTMJ" Then
                        ' Kümülatifin tarihi
                        If ((sSender = "RBEDA0") AndAlso (sQtyTyp = "70")) OrElse ((sSender <> "RBEDA0") AndAlso ((sQtyTyp = "3") OrElse (sQtyTyp = "70"))) Then
                            sDTMJTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                            If sDTMJTyp = "51" Then
                                sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            ElseIf sDTMJTyp = "52" Then
                                sDTMJ_End = Copy(sData, 9 - 1, 35).TrimEnd()
                            ElseIf (sSender = "5160") AndAlso (sDTMJTyp = "50") Then
                                sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            Else
                                sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            End If
                            sDTMJFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                        End If
                        If (sSender = "5060") OrElse (sSender = "506") Then
                            sDTMJTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                            If (sDTMJTyp = "50") Then
                                sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            End If
                            ' 102:VW,GM YYYYMMDD
                            sDTMJFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                        End If
                    ElseIf sTip = "RFFD" Then
                        sRFFDTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sRFFDTyp = "AAU" Then
                            sRFFD_Ref = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "SCC" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDElFreq = Copy(sData, 9 - 1, 3).TrimEnd()
                        If (sDElFreq = "P1") AndAlso (sDELTyp = "9") Then
                            sQTY = "0"
                            sQTYDec = "0"
                            sDTMJ_Str = ""
                            sDTMJ_End = ""
                            sDTMJFrmt = ""
                            sQTYA = "0"
                            sQTYA_Dec = "0"
                            '@ Unsupported function or procedure: 'FormatDateTime'
                            sDTML_Str = Now.ToString("yyyyMMdd")

                            sDTML_End = sDTML_Str
                            sDTML_Frmt = "102"
                            sDELTyp = "2"
                            '@ Unsupported function or procedure: 'FormatDateTime'
                            '@ Unsupported function or procedure: 'FormatDateTime'
                            EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, "0", "0", "", "", "", _
                             sRFFD_Ref, sDELTyp, sDElFreq, "0", "0", Now.ToString("yyyyMMdd"), _
                             Now.ToString("yyyyMMdd"), "102", "", "H", "", "", _
                             "", "", sSevkYeri, "")
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        End If
                    ElseIf sTip = "QTYA" Then
                        sQTYATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sQTYATyp = "83") OrElse (sQTYATyp = "84") OrElse (sQTYATyp = "113") OrElse (sQTYATyp = "1") OrElse (sQTYATyp = "131") Then
                            bKayit = True
                            sQTYA = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYA_Dec = Copy(sData, 21 - 1, 3).TrimEnd()
                            If (sQTYATyp = "83") OrElse (sQTYATyp = "84") Then
                                '@ Unsupported function or procedure: 'FormatDateTime'
                                sDTML_Str = Now.ToString("yyyyMMdd")
                            End If
                            If (sQTYATyp = "83") OrElse (sQTYATyp = "84") Then
                                '@ Unsupported function or procedure: 'FormatDateTime'
                                sDTML_End = Now.ToString("yyyyMMdd")
                            End If
                            If (sQTYATyp = "83") OrElse (sQTYATyp = "84") Then
                                sDTML_Frmt = "102"
                            End If
                        End If
                    ElseIf sTip = "DTML" Then
                        sDTMLTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sDTMLTyp = "51") OrElse (sDTMLTyp = "158") OrElse (sDTMLTyp = "2") Then
                            sDTML_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf (sDTMLTyp = "52") OrElse (sDTMLTyp = "159") Then
                            sDTML_End = Copy(sData, 9 - 1, 35).TrimEnd()
                        Else
                            sDTML_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sDTML_Frmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "UNT" Then
                        If (sDELTyp <> "2") AndAlso (sDELTyp <> "3") Then
                            EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                             sKanban, "", sSevkYeri, "")
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                            bKayit = False
                        Else
                            bKayit = False
                        End If
                    ElseIf sTip = "UNZ" Then
                    End If
                    ' **************************DELFOR D 97A  (Ford OTOSAN Formatý) *********************************
                ElseIf (sMsgTyp = "DELFOR") AndAlso (sVersion = "D") AndAlso (sRelease = "97A") AndAlso (sSender = "9041680202001") Then
                    result = 1
                    If bKayit AndAlso ((sTip = "UNH") OrElse (sTip = "LIN") OrElse (sTip = "QTYA") OrElse (sTip = "SCC") OrElse (sTip = "NADA") OrElse (sTip = "NADB")) Then
                        If sDELTyp = "4" Then
                            EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                             sKanban, "", sSevkYeri, "")
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        Else
                            bKayit = False
                        End If
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgTyp = Copy(sData, 20 - 1, 6).TrimEnd()
                        sVersion = Copy(sData, 27 - 1, 3).TrimEnd()
                        sRelease = Copy(sData, 31 - 1, 3).TrimEnd()
                        sMsgComRef = Copy(sData, 45 - 1, 35).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sMsgNam = "241" Then
                            sMsgNum = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "51" Then
                            sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 9 - 1, 35).TrimEnd()
                            sMsgStrDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "158" Then
                            sMsgStrDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "159" Then
                            sMsgEndDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        ' 101:YYYYMMDD
                        sDtmFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If sNADTyp = "SU" Then
                            sDUNSID = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        If sNADTyp = "MI" Then
                            ' sKapi = trimright(copy(sData,9,35));
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "LIN" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 16 - 1, 35).TrimEnd()
                    ElseIf sTip = "LOCA" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") Then
                            sKapi = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "159") Then
                            sMHandCode = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                    ElseIf sTip = "FTX" Then
                        sFtxTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sFtxTyp = "AAI" Then
                            sMsgTxt1 = Copy(sData, 39 - 1, 70).TrimEnd()
                            sMsgTxt2 = Copy(sData, 110 - 1, 70).TrimEnd()
                            sMsgTxt3 = Copy(sData, 180 - 1, 70).TrimEnd()
                        End If
                    ElseIf sTip = "RFFC" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "ON") OrElse (sRffCTyp = "CT") Then
                            sKontratNo = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "QTY" Then
                        sQtyTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sQtyTyp = "79" Then
                            sQTY = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYDec = Copy(sData, 22 - 1, 3).TrimEnd()
                        End If
                    ElseIf sTip = "SCC" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDElFreq = Copy(sData, 9 - 1, 3).TrimEnd()
                        sGH = Copy(sData, 13 - 1, 1).TrimEnd()
                        If (sDElFreq = "P1") AndAlso (sDELTyp = "9") Then
                            sQTY = "0"
                            sQTYDec = "0"
                            sDTMJ_Str = ""
                            sDTMJ_End = ""
                            sDTMJFrmt = ""
                            sQTYA = "0"
                            sQTYA_Dec = "0"
                            '@ Unsupported function or procedure: 'FormatDateTime'
                            sDTML_Str = Now.ToString("yyyyMMdd")
                            sDTML_End = sDTML_Str
                            sDTML_Frmt = "102"
                            sDELTyp = "2"
                            '@ Unsupported function or procedure: 'FormatDateTime'
                            '@ Unsupported function or procedure: 'FormatDateTime'
                            EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, "0", "0", "", "", "", _
                             sRFFD_Ref, sDELTyp, sDElFreq, "0", "0", Now.ToString("yyyyMMdd"), _
                             Now.ToString("yyyyMMdd"), "102", "", "H", "", "", _
                             "", "", sSevkYeri, "")
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        End If
                    ElseIf sTip = "QTYA" Then
                        sQTYATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sQTYATyp = "3" Then
                            bKayit = True
                            sQTYA = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYA_Dec = Copy(sData, 22 - 1, 3).TrimEnd()
                        End If
                    ElseIf sTip = "DTML" Then
                        sDTMLTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If ((sDTMLTyp = "158") AndAlso (sGH = "W")) OrElse ((sDTMLTyp = "159") AndAlso (sGH = "F")) Then
                            sDTML_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            sDTML_End = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sDTML_Frmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "UNT" Then
                        If sDELTyp = "4" Then
                            EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                             sKanban, "", sSevkYeri, "")
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                            bKayit = False
                        Else
                            bKayit = False
                        End If
                    ElseIf sTip = "UNZ" Then
                    End If
                    ' **************************DELJIT D 98B  (Ford OTOSAN Formatý) *********************************
                ElseIf (sMsgTyp = "DELJIT") AndAlso (sVersion = "D") AndAlso (sRelease = "98B") AndAlso (sSender = "9041680202001") Then
                    result = 1
                    If bKayit AndAlso ((sTip = "UNH") OrElse (sTip = "LIN") OrElse (sTip = "QTY")) Then
                        EDIWRKInsert("DELJIT", "D98B", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, "", "G", sMIssuer, sMHandCode, _
                         sKanban, "", sSevkYeri, "")
                        bKayit = False
                        sDTML_Str = ""
                        sDTML_End = ""
                        sDTML_Frmt = ""
                        sQTYA = ""
                        sQTYA_Dec = ""
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgTyp = Copy(sData, 20 - 1, 6).TrimEnd()
                        sVersion = Copy(sData, 27 - 1, 3).TrimEnd()
                        sRelease = Copy(sData, 31 - 1, 3).TrimEnd()
                        sMsgComRef = Copy(sData, 45 - 1, 35).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sMsgNam = "242" Then
                            sMsgNum = Copy(sData, 49 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "51" Then
                            sDTMJ_Str = Copy(sData, 8 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 8 - 1, 35).TrimEnd()
                            sMsgStrDte = Copy(sData, 8 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "158" Then
                            sMsgStrDte = Copy(sData, 8 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "159" Then
                            sMsgEndDte = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                        ' 101:YYYYMMDD
                        sDtmFrmt = Copy(sData, 43 - 1, 3).TrimEnd()
                    ElseIf sTip = "RFF" Then
                        sRFFDTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sRFFDTyp = "ALM" Then
                            sRFFD_Ref = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If sNADTyp = "SU" Then
                            sDUNSID = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                        If sNADTyp = "ST" Then
                            sPlantID__1 = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "LIN" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 14 - 1, 35).TrimEnd()
                    ElseIf sTip = "RFF1" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "ON") OrElse (sRffCTyp = "CT") Then
                            sKontratNo = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "LOC2" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") Then
                            sKapi = Copy(sData, 8 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "159") Then
                            sMHandCode = Copy(sData, 8 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "54") Then
                            sSevkYeri = Copy(sData, 8 - 1, 25).TrimEnd()
                        End If
                    ElseIf sTip = "QTY" Then
                        sQTYATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sQTYATyp = "3" Then
                            bKayit = True
                            ' sQTYA_Dec=trimright(copy(sData,22,3));
                            sQTYA = Copy(sData, 8 - 1, 15).TrimEnd()
                        ElseIf sQTYATyp = "79" Then
                            sQTY = Copy(sData, 8 - 1, 15).TrimEnd()
                        End If
                    ElseIf sTip = "SCC" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDElFreq = Copy(sData, 8 - 1, 3).TrimEnd()
                        sGH = Copy(sData, 11 - 1, 1).TrimEnd()
                    ElseIf sTip = "DTM5" Then
                        sDTMLTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (((sDTMLTyp = "10") OrElse (sDTMLTyp = "2")) AndAlso (sGH = "D")) Then
                            sDTML_Str = Copy(sData, 8 - 1, 35).TrimEnd()
                            sDTML_End = Copy(sData, 8 - 1, 35).TrimEnd()
                            sDTML_Frmt = Copy(sData, 43 - 1, 3).TrimEnd()
                        ElseIf sDTMLTyp = "51" Then
                            sDTMJ_Str = Copy(sData, 8 - 1, 35).TrimEnd()
                            sDTMJFrmt = Copy(sData, 43 - 1, 3).TrimEnd()
                            EDIWRKUpdateKumul(sMsgNum, sPlantID__1, sItnbr, sKapi, sQTY, sQTYDec, _
                             sDTMLTyp, sDTMJ_Str, sDTMJ_End, sDTMJFrmt)
                        End If
                    ElseIf sTip = "UNT" Then
                        EDIWRKInsert("DELJIT", "D98B", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, "", "G", sMIssuer, sMHandCode, _
                         sKanban, "", sSevkYeri, "")
                        sDTML_Str = ""
                        sDTML_End = ""
                        sDTML_Frmt = ""
                        sQTYA = ""
                        sQTYA_Dec = ""
                        bKayit = False
                    ElseIf sTip = "UNZ" Then
                    End If
                    ' **************************DELJIT D 98B  (RENAULT Formatý)*********************************
                ElseIf (sMsgTyp = "DELJIT") AndAlso (sVersion = "D") AndAlso (sRelease = "98B") AndAlso (sSender = "1780129987") Then
                    result = 1
                    sRFFDTyp = ""
                    If bKayit AndAlso ((sTip = "QTY")) Then
                        EDIWRKInsert("DELJIT", "D98B", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, sRFFAEM, "G", sMIssuer, sMHandCode, _
                         sKanban, "", sSevkYeri, sL3PREF)
                        bKayit = False

                        If sRFFAEM.IndexOf("%") > 0 Then
                            If sRFFAEM.Length > 35 Then
                                sRFFAEM = sRFFAEM.Remove(sRFFAEM.IndexOf("%"), 35)
                            Else
                                sRFFAEM = sRFFAEM.Remove(sRFFAEM.IndexOf("%"), sRFFAEM.Length - sRFFAEM.IndexOf("%"))
                            End If

                        End If

                        sQTYA = ""
                        sQTYA_Dec = ""
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgTyp = Copy(sData, 20 - 1, 6).TrimEnd()
                        sVersion = Copy(sData, 27 - 1, 3).TrimEnd()
                        sRelease = Copy(sData, 31 - 1, 3).TrimEnd()
                        sMsgComRef = Copy(sData, 45 - 1, 35).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sMsgNam = "340" Then
                            sMsgNum = Copy(sData, 49 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "51" Then
                            sDTMJ_Str = Copy(sData, 8 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 8 - 1, 35).TrimEnd()
                            sMsgStrDte = Copy(sData, 8 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "117" Then
                            sDTML_Str = Copy(sData, 8 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "128" Then
                            sDTML_End = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                        sDtmFrmt = Copy(sData, 43 - 1, 3).TrimEnd()
                        ' 203:YYYYMMDDHHMM
                        ' 203:YYYYMMDDHHMM
                        sDTML_Frmt = Copy(sData, 43 - 1, 3).TrimEnd()
                    ElseIf sTip = "RFF" Then
                        sRFFDTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sRFFDTyp = "AEM" Then
                            bRFFAEM = True
                            sRFFAEM = Copy(sData, 8 - 1, 35).TrimEnd()
                        Else
                            bRFFAEM = False
                        End If
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If sNADTyp = "SU" Then
                            sDUNSID = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                        If sNADTyp = "MI" Then
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        If sNADTyp = "CN" Then
                            sNADCN = Copy(sData, 8 - 1, 35).TrimEnd()

                            '
                            ' if sNADCN = '09317801299871892' then sPlantID = 'FL'
                            ' else if sNADCN = '09317801299870101' then sPlantID = 'TU'
                            ' else if sNADCN = '09317801299870102' then sPlantID = 'TK'
                            ' else if sNADCN = '09317801299870104' then sPlantID = 'BU'
                            ' else if sNADCN = '09459813295CI' then sPlantID = 'CI'
                            ' else if sNADCN = '0945160796DK' then sPlantID = 'DK'
                            ' else if sNADCN = '0945160796RK' then sPlantID = 'RK'
                            ' else if sNADCN = '0945160796SR' then sPlantID = 'SR'
                            ' else if sNADCN = '0945160796CA' then sPlantID = 'CA'
                            ' else if sNADCN = '0941A470005180009' then sPlantID = 'VB';
                            '
                            sPlantID__1 = sLookup("DACSHP", SetTableName(My.Settings.LibNameEDI, "DACIAPF"), " DACEDI=" & sTirnakEkle(sNADCN))
                        End If
                    ElseIf sTip = "LIN" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 14 - 1, 35).TrimEnd()
                    ElseIf sTip = "RFF1" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "ON") OrElse (sRffCTyp = "CT") Then
                            sKontratNo = Copy(sData, 8 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "LOC2" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") Then
                            sKapi = Copy(sData, 8 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "11") Then
                            sKanban = Copy(sData, 8 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "159") Then
                            sMHandCode = Copy(sData, 8 - 1, 25).TrimEnd()
                        End If
                    ElseIf sTip = "QTY" Then
                        sQTYATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sQTYATyp = "131" Then
                            sQTYA = Copy(sData, 8 - 1, 15).TrimEnd()
                            ' sQTYA_Dec=trimright(copy(sData,22,3));
                            bKayit = True
                        End If
                    ElseIf sTip = "RFF2" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "CW") Then
                            If bRFFAEM = True Then
                                sRFFAEM = sRFFAEM + "%" + Copy(sData, 8 - 1, 35).TrimEnd()
                            Else
                                If sRFFAEM.Length > 0 Then
                                    sRFFAEM = ("D" + Copy(sRFFAEM, 2 - 1, sRFFAEM.Length - 3) & "05") + "%" + Copy(sData, 8 - 1, 35).TrimEnd()
                                Else
                                    sRFFAEM = ("D" + "" & "05") + "%" + Copy(sData, 8 - 1, 35).TrimEnd()
                                End If
                            End If
                        ElseIf sRffCTyp = "AMU" Then
                            sL3PREF = Copy(sData, 8 - 1, 14).TrimEnd()
                            '
                            ' end else if sTip='SCC' then
                            ' begin
                            ' sDELTyp=trimright(copy(sData,5,3));
                            ' sDElFreq=trimright(copy(sData,9,3));
                            ' sGH = trimright(copy(sData,13,1));
                            '
                            ' if (sDElFreq='P1') and (sDELTyp='9') then
                            ' begin
                            ' sQTY='0'; sQTYDec='0'; sDTMJ_Str=''; sDTMJ_End=''; sDTMJFrmt='';
                            ' sQTYA='0'; sQTYA_Dec='0'; sDTML_Str=FormatDateTime('YYYYMMDD',date);
                            ' sDTML_End=sDTML_Str; sDTML_Frmt='102';sDelTyp='2';
                            ' EDIWRKInsert('DELFOR','D97A',sSender,sReceiver,sMsgRef,sMsgNam,sMsgNum,sMsgDte,
                            ' sMsgStrDte,sMsgEndDte,sDtmFrmt,sMsgTxt1,sMsgTxt2,sMsgTxt3,sDUNSID,sPlantID,sItnbr,
                            ' sKapi,sKontratNo,'0','0','','','',sRFFD_Ref,
                            ' sDELTyp,sDElFreq,'0','0',FormatDateTime('YYYYMMDD',date),FormatDateTime('YYYYMMDD',date),
                            ' '102',sPUSNo,'H','','','','', sSevkYeri);
                            ' bKayit=False;
                            ' sDTML_Str=''; sDTML_End=''; sDTML_Frmt=''; sQTYA=''; sQTYA_Dec='';
                            ' end;
                        End If
                    ElseIf sTip = "UNT" Then
                        '
                        ' EDIWRKInsert('DELFOR','D97A',sSender,sReceiver,sMsgRef, sMsgNam, sMsgNum, sMsgDte,
                        ' sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, sMsgTxt2, sMsgTxt3, sDUNSID,
                        ' sPlantID,sItnbr,sKapi,sKontratNo,
                        ' sQTY,sQTYDec,sDTMJ_Str,sDTMJ_End,sDTMJFrmt,sRFFD_Ref,
                        ' sDELTyp,sDElFreq,sQTYA,sQTYA_Dec,sDTML_Str,sDTML_End, sDTML_Frmt,'','H',
                        ' sMIssuer,sMHandCode,sKanban,'',sSevkYeri);
                        ' sDTML_Str=''; sDTML_End=''; sDTML_Frmt=''; sQTYA=''; sQTYA_Dec='';
                        ' bKayit=False;
                    ElseIf sTip = "UNZ" Then
                    End If
                ElseIf (sMsgTyp = "DELINS") AndAlso (sVersion = "3") Then
                    result = 1
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgRef = Copy(sData, 5 - 1, 14).TrimEnd()
                    ElseIf sTip = "MID" Then
                        sMsgRef = ""
                        sMsgNum = Copy(sData, 5 - 1, 17).TrimEnd()
                        sMsgDte = Copy(sData, 23 - 1, 6).TrimEnd()
                        sMsgNam = ""
                        sMsgTxt1 = ""
                        sMsgTxt2 = ""
                        sMsgTxt3 = ""
                        ' sMsgStatu = "H"
                        sDtmFrmt = "DEL"

                        If Copy(sData, 8 - 1, 1).TrimEnd() = "W" Then
                            sMsgStatu = "H"
                        Else
                            sMsgStatu = "G"
                        End If

                    ElseIf sTip = "SDT" Then
                        ' Seller Details
                        If sSender = "094200005562588912" Then
                            sDUNSID = Copy(sData, 206 - 1, 17).TrimEnd()
                        Else
                            sDUNSID = Copy(sData, 5 - 1, 20).TrimEnd()
                        End If
                    ElseIf sTip = "ARI" Then
                        sARITyp = Copy(sData, 5 - 1, 1).TrimEnd()

                        'If sARITyp = "3" Then
                        '    sMsgStatu = "H"
                        'ElseIf sARITyp = "2" Then
                        '    If sSender = "TOFASTR" Then
                        '        sMsgStatu = "H"
                        '    Else
                        '        sMsgStatu = "G"
                        '    End If
                        'End If

                        sMsgStrDte = Copy(sData, 7 - 1, 6).TrimEnd()
                        sMsgEndDte = Copy(sData, 14 - 1, 6).TrimEnd()
                        sDtmTyp = ""
                        sDtmFrmt = "DEL"
                    ElseIf sTip = "CSG" Then
                        If (sSender = "O0013000018PORSCHE-EDI") OrElse (sSender = "003700000001439912") Then
                            sPlantID__1 = Copy(sData, 5 - 1, 20).TrimEnd()
                            sKapi = Copy(sData, 224 - 1, 17).TrimEnd()
                        ElseIf sSender = "TUR0000" Then
                            sPlantID__1 = Copy(sData, 206 - 1, 17).TrimEnd()
                            sKapi = Copy(sData, 422 - 1, 17).TrimEnd()
                        Else
                            sPlantID__1 = Copy(sData, 206 - 1, 17).TrimEnd()
                            sKapi = Copy(sData, 224 - 1, 17).TrimEnd()
                        End If
                    ElseIf sTip = "ARD" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 5 - 1, 35).TrimEnd()
                        sKontratNo = Copy(sData, 272 - 1, 35).TrimEnd()
                    ElseIf sTip = "PDI" Then
                        ' Previous Delivery Instruction
                    ElseIf sTip = "SAD" Then
                        ' Supplemantary Article Details
                    ElseIf sTip = "DST" Then
                        ' Kümülatif Miktar
                        sQtyTyp = ""
                        sDTMJTyp = ""
                        sQTY = Copy(sData, 28 - 1, 10).TrimEnd()
                        sDTMJ_Str = Copy(sData, 5 - 1, 6).TrimEnd()
                        sDTMJ_End = ""
                        sDTMJFrmt = "DEL"
                        If sSender = "TUR0000" Then
                            EDIWRKUpdateKumul(sMsgNum, sPlantID__1, sItnbr, sKapi, sQTY, sQTYDec, _
                             sDTMJTyp, sDTMJ_Str, sDTMJ_End, sDTMJFrmt)
                        End If
                    ElseIf sTip = "PDN" Then
                        ' Previous Despatch Notes
                        sRFFDTyp = ""
                        sRFFD_Ref = Copy(sData, 5 - 1, 17).TrimEnd()
                    ElseIf sTip = "SID" Then
                        ' Schedule Indicator Details
                        sDELTyp = Copy(sData, 5 - 1, 1).TrimEnd()
                        sDElFreq = ""
                    ElseIf sTip = "DEL" Then
                        sQTYATyp = ""
                        bKayit = True
                        sQTYA = Copy(sData, 42 - 1, 15).TrimEnd()
                        ' Miktar
                        sQTYA_Dec = "0"
                        ' Miktarýn Decimal hanesi 12,3
                        sPUSNo = ""
                        sDTMLTyp = ""
                        sDTML_Str = Copy(sData, 5 - 1, 6).TrimEnd()
                        sDTML_End = Copy(sData, 17 - 1, 6).TrimEnd()
                        sDTML_Frmt = "DEL"
                        If sSender = "TUR0000" Then
                            sDELTyp = Copy(sData, 78 - 1, 1).TrimEnd()
                            If sDELTyp = "1" Then
                                sPUSNo = "KESIN"
                            Else
                                sPUSNo = ""
                            End If
                        End If
                        EDIWRKInsert("DELINS", "3", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, sPUSNo, sMsgStatu, sMIssuer, sMHandCode, _
                         sKanban, "", "", "")
                        bKayit = False
                    ElseIf sTip = "UNT" Then
                        EDIWRKInsert("DELINS", "3", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, sPUSNo, sMsgStatu, sMIssuer, sMHandCode, _
                         sKanban, "", "", "")
                        bKayit = False
                    ElseIf sTip = "UNZ" Then
                        ' **************************DELFOR D 96A FORMATI ***********************
                    End If
                ElseIf (sMsgTyp = "DELFOR") AndAlso (sVersion = "D") AndAlso (sRelease = "96A") Then
                    result = 1
                    sMsgStatu = "H"
                    If bKayit AndAlso (sTip = "QTY") Then
                        If (sQtyTyp = "113") Then
                            EDIWRKInsert("DELFOR", "D96A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, sPUSNo, sMsgStatu, sMIssuer, sMHandCode, _
                             sKanban, sQTY1, "", "")
                            Threading.Thread.Sleep(30)
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        End If
                        sQtyTyp = ""
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgRef = Copy(sData, 5 - 1, 14).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sMsgNam = "241") OrElse (sMsgNam = "34") Then
                            sMsgNum = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        ' else if sDtmTyp='158' then sMsgStrDte=trimright(copy(sData,9,35))
                        ' else if sDtmTyp='159' then sMsgEndDte=trimright(copy(sData,9,35));
                        ' 102:YYYYMMDD
                        sDtmFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "FTX" Then
                        sFtxTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sFtxTyp = "AAI" Then
                            sMsgTxt1 = Copy(sData, 39 - 1, 70).TrimEnd()
                            sMsgTxt2 = Copy(sData, 110 - 1, 70).TrimEnd()
                            sMsgTxt3 = Copy(sData, 180 - 1, 70).TrimEnd()
                        End If
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If (sNADTyp = "SU") OrElse (sNADTyp = "SE") Then
                            sDUNSID = Copy(sData, 8, 35).TrimEnd()
                        End If
                        If (sNADTyp = "MI") Then
                            sMIssuer = Copy(sData, 8, 35).TrimEnd()
                        End If
                        If (sNADTyp = "BY") Then
                            sPlantID__1 = Copy(sData, 8, 35).TrimEnd()
                        End If

                    ElseIf sTip = "NADA" Then
                        If bKayit Then
                            EDIWRKInsert("DELFOR", "D96A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, sPUSNo, sMsgStatu, sMIssuer, sMHandCode, _
                             sKanban, sQTY1, "", "")
                            Threading.Thread.Sleep(30)
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        End If
                        sNADATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sNADATyp = "CN" Then
                            If (sSender = "1780129987") Then
                                sNADCN = Copy(sData, 9 - 1, 35).TrimEnd()

                                '
                                ' if sNADCN = '09317801299871892' then sPlantID = 'FL'
                                ' else if sNADCN = '09317801299870101' then sPlantID = 'TU'
                                ' else if sNADCN = '09317801299870102' then sPlantID = 'TK'
                                ' else if sNADCN = '09317801299870104' then sPlantID = 'BU'
                                ' else if sNADCN = '09459813295CI' then sPlantID = 'CI'
                                ' else if sNADCN = '0945160796DK' then sPlantID = 'DK'
                                ' else if sNADCN = '0945160796RK' then sPlantID = 'RK'
                                ' else if sNADCN = '0945160796SR' then sPlantID = 'SR'
                                ' else if sNADCN = '0945160796CA' then sPlantID = 'CA'
                                ' else if sNADCN = '0941A470005180009' then sPlantID = 'VB';
                                '
                                sPlantID__1 = sLookup("DACSHP", SetTableName(My.Settings.LibNameEDI, "DACIAPF"), " DACEDI=" & sTirnakEkle(sNADCN))
                            Else
                                sPlantID__1 = Copy(sData, 233 - 1, 35).TrimEnd()
                                ' Renault
                            End If
                        End If
                    ElseIf sTip = "LIN" Then
                        If bKayit Then
                            EDIWRKInsert("DELFOR", "D96A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, sPUSNo, sMsgStatu, sMIssuer, sMHandCode, _
                             sKanban, sQTY1, "", "")
                            Threading.Thread.Sleep(30)
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        End If
                        If Copy(sData, 12 - 1, 1).TrimEnd() = "3" Then
                            sMsgStatu = "H"
                        ElseIf Copy(sData, 12 - 1, 1).TrimEnd() = "9" Then
                            sMsgStatu = "G"
                        End If
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 16 - 1, 35).TrimEnd()
                    ElseIf sTip = "LOCB" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") Then
                            sKapi = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                        '@ Undeclared identifier(3): 'sPlantid'
                        If sPlantID__1 = "TK" Then
                            sKapi = sKapi.Substring(1 - 1, 5)
                        End If
                    ElseIf sTip = "RFFA" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "ON") Then
                            sKontratNo = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "SCC" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        ' 1:Kesin,4:Planlý
                        sDELTyp1 = Copy(sData, 13 - 1, 1).TrimEnd()
                        ' 1:Kesin,4:Planlý
                        If ((sMsgStatu = "G") OrElse (sMsgStatu = "P")) AndAlso (sDELTyp = "1") Then
                            sMsgStatu = "G"
                            sPUSNo = "KESIN"
                        ElseIf (sMsgStatu = "G") OrElse (sMsgStatu = "P") Then
                            sMsgStatu = "G"
                            sPUSNo = ""
                        End If
                        '@ Undeclared identifier(3): 'sPlantid'
                        If (sPlantid = "TK") AndAlso (sDELTyp = "1") Then
                            sMsgStatu = "G"
                            sPUSNo = "KESIN"
                            '@ Undeclared identifier(3): 'sPlantid'
                        ElseIf (sPlantid = "TK") AndAlso (sDELTyp <> "1") Then
                            sMsgStatu = "H"
                            sPUSNo = ""
                        End If
                        sDElFreq = Copy(sData, 9 - 1, 3).TrimEnd()
                    ElseIf sTip = "QTY" Then
                        ' Kümülatif Miktar 12,3 GM:3 VW:70
                        sQtyTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sQtyTyp = "70") Then
                            sQTY = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYDec = Copy(sData, 21 - 1, 3).TrimEnd()
                        ElseIf sQtyTyp = "113" Then
                            sQTYATyp = sQtyTyp
                            bKayit = True
                            sQTYA = Copy(sData, 9 - 1, 12).TrimEnd()
                            ' Miktar
                            ' Miktarýn Decimal hanesi 12,3
                            sQTYA_Dec = Copy(sData, 21 - 1, 3).TrimEnd()
                        ElseIf sQtyTyp = "78" Then
                            ' Miktar
                            sQTY1 = Copy(sData, 9 - 1, 12).TrimEnd()
                        End If
                        ' if sMsgStatu='G' then
                        ' begin
                        ' sDTMJ_Str=sMsgDte;
                        ' sDTMJFrmt=sDtmFrmt;
                        ' end;
                        If (sQtyTyp = "70") OrElse (sQtyTyp = "78") Then
                            If bKayit Then
                                EDIWRKInsert("DELFOR", "D96A", sSender, sReceiver, sMsgRef, sMsgNam, _
                                 sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                                 sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                                 sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                                 sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                                 sDTML_End, sDTML_Frmt, sPUSNo, sMsgStatu, sMIssuer, sMHandCode, _
                                 sKanban, sQTY1, "", "")
                                bKayit = False
                            End If
                            Threading.Thread.Sleep(30)
                            If (sQtyTyp = "70") Then
                                EDIWRKUpdateKumul(sMsgNum, sPlantID__1, sItnbr, sKapi, sQTY, sQTYDec, _
                                 sDTMJTyp, sDTMJ_Str, sDTMJ_End, sDTMJFrmt)
                                sDTML_Str = ""
                                sDTML_End = ""
                                sDTML_Frmt = ""
                                sQTYA = ""
                                sQTYA_Dec = ""
                            ElseIf sQtyTyp = "78" Then
                                EDIWRKUpdateKumul1(sMsgNum, sPlantID__1, sItnbr, sKapi, sQTY1)
                                sQTY1 = ""
                            End If
                        End If
                    ElseIf sTip = "RFFD" Then
                        sRFFDTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sRFFDTyp = "AAU" Then
                            sRFFD_Ref = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTMG" Then
                        sDTMLTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sDTMLTyp = "64") Then
                            sDTML_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf (sDTMLTyp = "63") Then
                            sDTML_End = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        ' 102:YYYYMMDD,716:YYYYWW,616:YYYYWW,610:CCYYNN
                        sDTML_Frmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "UNT" Then
                    ElseIf sTip = "UNZ" Then
                        ' **************************DELFOR 1 921 - HAFTALIK *********************************
                    End If
                ElseIf (sMsgTyp = "DELFOR") AndAlso (sVersion = "1") AndAlso (sRelease = "921") Then
                    result = 1
                    If bKayit AndAlso (sTip = "QTY") Then
                        If (sQtyTyp = "21") Then
                            EDIWRKInsert("DELFOR", "1921", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                             sKanban, "", "", "")
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        Else
                            bKayit = False
                        End If
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgTyp = Copy(sData, 20 - 1, 6).TrimEnd()
                        sVersion = Copy(sData, 27 - 1, 3).TrimEnd()
                        sRelease = Copy(sData, 31 - 1, 3).TrimEnd()
                        sMsgComRef = Copy(sData, 45 - 1, 35).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sMsgNam = "241" Then
                            sMsgNum = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                        If sMsgNam = "241" Then
                            sKontratNo = Copy(sData, 53 - 1, 10).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sDtmFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sNADTyp = "SU") OrElse (sNADTyp = "SE") Then
                            sDUNSID = Copy(sData, 9 - 1, 17).TrimEnd()
                        End If
                        If sNADTyp = "DP" Then
                            sKapi = Copy(sData, 9 - 1, 17).TrimEnd()
                        End If
                        If sDUNSID = "08088582" Then
                            sPlantID__1 = "26"
                        End If
                    ElseIf sTip = "LIN" Then
                        ' ClearLIN;
                        sItnbr = Copy(sData, 16 - 1, 35).TrimEnd()
                    ElseIf sTip = "RFFB" Then
                        sRffCTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sRffCTyp = "AAK") Then
                            sRFFD_Ref = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "QTY" Then
                        sQtyTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sQtyTyp = "70") Then
                            sQTY = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYDec = Copy(sData, 21 - 1, 3).TrimEnd()
                        End If
                        If (sQtyTyp = "21") Then
                            bKayit = True
                            sQTYA = Copy(sData, 9 - 1, 12).TrimEnd()
                            ' Miktar
                            ' Miktarýn Decimal hanesi 12,3
                            sQTYA_Dec = Copy(sData, 21 - 1, 3).TrimEnd()
                        End If
                    ElseIf sTip = "DTMG" Then
                        sDTMJTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDTMJTyp = "124" Then
                            sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            sDTMJFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                        End If
                        If sDTMJTyp = "2" Then
                            sDTML_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            sDTML_Frmt = Copy(sData, 45 - 1, 3).TrimEnd()
                            If bKayit Then
                                EDIWRKInsert("DELFOR", "1921", sSender, sReceiver, sMsgRef, sMsgNam, _
                                 sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                                 sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                                 sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                                 sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                                 sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                                 sKanban, "", "", "")
                                bKayit = False
                                sDTML_Str = ""
                                sDTML_End = ""
                                sDTML_Frmt = ""
                                sQTYA = ""
                                sQTYA_Dec = ""
                            End If
                        End If
                    ElseIf sTip = "SCC" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        ' 4:Planlý, 10:Acil, 9:Ýhtiyaç yok (VW), 1:Kesin,4:Planlý (GM)
                        ' F:Flexible interval, W:Weekly
                        sDElFreq = Copy(sData, 9 - 1, 3).TrimEnd()
                    ElseIf sTip = "UNT" Then
                        If (sDELTyp <> "2") AndAlso (sDELTyp <> "3") Then
                            EDIWRKInsert("DELFOR", "1921", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                             sKanban, "", "", "")
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                            bKayit = False
                        Else
                            bKayit = False
                        End If
                    ElseIf sTip = "UNZ" Then
                        ' **************************DELJIT D 97A -GÜNLÜK *********************************
                    End If
                ElseIf (sMsgTyp = "DELJIT") AndAlso (sVersion = "D") AndAlso (sRelease = "97A") AndAlso (sMsgComRef = "SH") Then
                    result = 1
                    If bKayit AndAlso ((sTip = "UNH") OrElse (sTip = "SEQ") OrElse (sTip = "QTY")) Then
                        If sQTYATyp = "1" Then
                            EDIWRKInsert("DELJIT", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, sPUSNo, "G", sMIssuer, sMHandCode, _
                             sKanban, "", "", "")
                            bKayit = False
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                            sQTYA = ""
                            sQTYA_Dec = ""
                        End If
                    End If
                    If bkumupdate AndAlso ((sTip = "UNH") OrElse (sTip = "SEQ") OrElse (sTip = "LIN")) Then
                        EDIWRKUpdateKumul(sMsgNum, sPlantID__1, sItnbr, sKapi, sQTY, sQTYDec, _
                         sDTMJTyp, sDTMJ_Str, sDTMJ_End, sDTMJFrmt)
                        bkumupdate = False
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        DataAktar_ClearMessage()
                        sMsgRef = Copy(sData, 5 - 1, 14).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sMsgNam = "242") OrElse (sMsgNam = "241") Then
                            sMsgNum = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "158" Then
                            sMsgStrDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "159" Then
                            sMsgEndDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sDtmFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                    ElseIf sTip = "FTX" Then
                        sFtxTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sFtxTyp = "AAI" Then
                            sMsgTxt1 = Copy(sData, 39 - 1, 70).TrimEnd()
                            sMsgTxt2 = Copy(sData, 110 - 1, 70).TrimEnd()
                            sMsgTxt3 = Copy(sData, 180 - 1, 70).TrimEnd()
                        End If
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If (sNADTyp = "SU") OrElse (sNADTyp = "SE") Then
                            sDUNSID = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        If (sNADTyp = "MI") Then
                            sMIssuer = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sNADATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sNADATyp = "ST" Then
                            ' GM,SAAB
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sNADATyp = "CN" Then
                            ' VW,SK
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sNADATyp = "DP" Then
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                            ' R.Bosch
                        End If
                    ElseIf sTip = "LIN" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 16 - 1, 35).TrimEnd()
                    ElseIf sTip = "PIA" Then
                        sKanban = Copy(sData, 9 - 1, 15).TrimEnd()
                    ElseIf sTip = "LOCB" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") OrElse (sLocATyp = "18") Then
                            sKapi = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "159") Then
                            sMHandCode = Copy(sData, 9 - 1, 25).TrimEnd()
                            ' end else if sTip='RFFC' then
                            ' begin
                            ' sRffCTyp=trimright(copy(sData,5,3));
                            ' if (sRffCTyp='ON') or (sRffCTyp='CT')  then sKontratNo=trimright(copy(sData,9,35));
                        End If
                    ElseIf sTip = "QTY" Then
                        sQtyTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sQTYATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sQtyTyp = "3") OrElse (sQtyTyp = "70") Then
                            ' Kümülatif Miktar
                            bkumupdate = True
                            sQTY = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYDec = Copy(sData, 21 - 1, 3).TrimEnd()
                        End If
                        If (sQTYATyp = "1") Then
                            bKayit = True
                            sQTYA = Copy(sData, 9 - 1, 12).TrimEnd()
                            ' Miktar
                            sQTYA_Dec = Copy(sData, 21 - 1, 3).TrimEnd()
                            ' Miktarýn Decimal hanesi 12,3
                            ' EDIWRKInsert('DELJIT','D97A',sSender,sReceiver,sMsgRef, sMsgNam, sMsgNum, sMsgDte,
                            ' sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, sMsgTxt2, sMsgTxt3,sDUNSID,
                            ' sPlantID,sItnbr,sKapi,sKontratNo,
                            ' sQTY,sQTYDec,sDTMJ_Str,sDTMJ_End,sDTMJFrmt,sRFFD_Ref,
                            ' sDELTyp,sDElFreq,sQTYA,sQTYA_Dec, sDTML_Str,sDTML_End, sDTML_Frmt,sPUSNO,'G',sMIssuer,sMHandCode,sKanban);
                            ' bKayit=False; sQTYA=''; sQTYA_Dec='';
                            sDTML_Str = ""
                            sDTML_End = ""
                            sDTML_Frmt = ""
                        End If
                    ElseIf sTip = "DTME" Then
                        sDTMLTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sDTMLTyp = "10") Then
                            sDTML_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            sDTML_End = Copy(sData, 9 - 1, 35).TrimEnd()
                            sDTML_Frmt = Copy(sData, 45 - 1, 3).TrimEnd()
                            If (sQTYATyp = "1") AndAlso bKayit Then
                                EDIWRKInsert("DELJIT", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                                 sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                                 sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                                 sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                                 sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                                 sDTML_End, sDTML_Frmt, sPUSNo, "G", sMIssuer, sMHandCode, _
                                 sKanban, "", "", "")
                                bKayit = False
                                sDTML_Str = ""
                                sDTML_End = ""
                                sDTML_Frmt = ""
                                sQTYA = ""
                                sQTYA_Dec = ""
                            End If
                        ElseIf (sDTMLTyp = "51") AndAlso (sQtyTyp = "3") Then
                            sDTMJTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                            If sDTMJTyp = "51" Then
                                sDTMJ_Str = Copy(sData, 9 - 1, 35).TrimEnd()
                            End If
                            sDTMJFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                        ElseIf (sDTMLTyp = "11") AndAlso (sQtyTyp = "3") Then
                            sDTMJTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                            If sDTMJTyp = "11" Then
                                sDTMJ_End = Copy(sData, 9 - 1, 35).TrimEnd()
                            End If
                            sDTMJFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                            If bkumupdate Then
                                EDIWRKUpdateKumul(sMsgNum, sPlantID__1, sItnbr, sKapi, sQTY, sQTYDec, _
                                 sDTMJTyp, sDTMJ_Str, sDTMJ_End, sDTMJFrmt)
                                bkumupdate = False
                            End If
                        End If
                    ElseIf sTip = "RFFD" Then
                        sRFFDTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sRFFDTyp = "AAU" Then
                            sRFFD_Ref = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "SEQ" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDElFreq = Copy(sData, 9 - 1, 3).TrimEnd()
                    ElseIf sTip = "UNT" Then
                    ElseIf sTip = "UNZ" Then
                        ' **************************DELJIT D 97A - PUS*********************************
                    End If
                ElseIf (sMsgTyp = "DELJIT") AndAlso (sVersion = "D") AndAlso (sRelease = "97A") AndAlso (sMsgComRef = "PUS") Then
                    result = 1
                    If bKayit AndAlso ((sTip = "UNH") OrElse (sTip = "SEQ") OrElse (sTip = "QTY")) Then
                        If sQTYATyp = "1" Then
                            EDIWRKInsert("DELJIT", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                             sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                             sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                             sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                             sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                             sDTML_End, sDTML_Frmt, sPUSNo, "P", sMIssuer, sMHandCode, _
                             sKanban, "", "", "")
                            bKayit = False
                        End If
                    End If
                    If sTip = "UNB" Then
                        sSender = Copy(sData, 5 - 1, 35).TrimEnd()
                        sReceiver = Copy(sData, 61 - 1, 35).TrimEnd()
                    ElseIf sTip = "UNH" Then
                        ' ClearMessage;
                        sMsgRef = Copy(sData, 5 - 1, 14).TrimEnd()
                    ElseIf sTip = "BGM" Then
                        sMsgNam = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sMsgNam = "242") OrElse (sMsgNam = "241") Then
                            sMsgNum = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                        If (sMsgComRef = "PUS") AndAlso (sMsgNam = "242") Then
                            sPUSNo = Copy(sData, 53 - 1, 35).TrimEnd()
                        End If
                    ElseIf sTip = "DTM" Then
                        sDtmTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sDtmTyp = "137" Then
                            sMsgDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "234" Then
                            sMsgStrDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sDtmTyp = "235" Then
                            sMsgEndDte = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sDtmFrmt = Copy(sData, 45 - 1, 3).TrimEnd()
                        sDTML_Str = sMsgStrDte
                        sDTML_End = sMsgEndDte
                        sDTMLTyp = sDtmTyp
                        sDTML_Frmt = sDtmFrmt
                    ElseIf sTip = "FTX" Then
                        sFtxTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sFtxTyp = "AAI" Then
                            sMsgTxt1 = Copy(sData, 39 - 1, 70).TrimEnd()
                            sMsgTxt2 = Copy(sData, 110 - 1, 70).TrimEnd()
                            sMsgTxt3 = Copy(sData, 180 - 1, 70).TrimEnd()
                        End If
                    ElseIf sTip = "NAD" Then
                        sNADTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDUNSID = ""
                        If (sNADTyp = "SU") OrElse (sNADTyp = "SE") Then
                            sDUNSID = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        If (sNADTyp = "MI") Then
                            sMIssuer = Copy(sData, 9 - 1, 35).TrimEnd()
                        End If
                        sNADATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If sNADATyp = "ST" Then
                            ' GM,SAAB
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sNADATyp = "CN" Then
                            ' VW,SK
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                        ElseIf sNADATyp = "DP" Then
                            sPlantID__1 = Copy(sData, 9 - 1, 35).TrimEnd()
                            ' R.Bosch
                        End If
                    ElseIf sTip = "LIN" Then
                        DataAktar_ClearLIN()
                        sItnbr = Copy(sData, 16 - 1, 35).TrimEnd()
                    ElseIf sTip = "PIA" Then
                        sKanban = Copy(sData, 9 - 1, 15).TrimEnd()
                    ElseIf sTip = "LOCB" Then
                        sLocATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sLocATyp = "11") OrElse (sLocATyp = "18") Then
                            sKapi = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                        If (sLocATyp = "159") Then
                            sMHandCode = Copy(sData, 9 - 1, 25).TrimEnd()
                        End If
                    ElseIf sTip = "QTY" Then
                        sQtyTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sQTYATyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        If (sQtyTyp = "3") OrElse (sQtyTyp = "70") Then
                            ' Kümülatif Miktar
                            ' bkumupdate=True;
                            sQTY = Copy(sData, 9 - 1, 12).TrimEnd()
                            sQTYDec = Copy(sData, 21 - 1, 3).TrimEnd()
                        End If
                        If (sQTYATyp = "1") Then
                            bKayit = True
                            sQTYA = Copy(sData, 9 - 1, 12).TrimEnd()
                            ' Miktar
                            ' Miktarýn Decimal hanesi 12,3
                            sQTYA_Dec = Copy(sData, 21 - 1, 3).TrimEnd()
                        End If
                    ElseIf sTip = "SEQ" Then
                        sDELTyp = Copy(sData, 5 - 1, 3).TrimEnd()
                        sDElFreq = Copy(sData, 9 - 1, 3).TrimEnd()
                    ElseIf sTip = "UNT" Then
                    ElseIf sTip = "UNZ" Then
                    End If
                ElseIf sMsgTyp = "CSV-HAFTALIK-OTS" Then
                    If nLine > 1 Then
                        result = 1
                        sSender = ""
                        sReceiver = My.Settings.Receiver
                        sMsgNam = "FORD-OTOSAN"

                        sMsgNum = ExtractWord(1, sData, ",")

                        sMsgDte = ExtractWord(11, sData, ",")

                        sMsgStrDte = ExtractWord(11, sData, ",")

                        sMsgTxt1 = Copy(ExtractWord(27, sData, ","), 1, 70)

                        sMsgTxt2 = Copy(ExtractWord(27, sData, ","), 71, 70)

                        sMsgTxt3 = Copy(ExtractWord(27, sData, ","), 141, 70)

                        sDtmFrmt = "OTS"

                        sPlantID__1 = ExtractWord(2, sData, ",")

                        sItnbr = ExtractWord(5, sData, ",")

                        sKapi = ExtractWord(2, sData, ",")

                        sQTY = ExtractWord(22, sData, ",")

                        sQTYDec = "0"

                        sQTYA = ExtractWord(31, sData, ",")
                        sQTYA_Dec = "0"
                        sDELTyp = "4"

                        sDElFreq = ExtractWord(28, sData, ",")
                        ' if ExtractWord(20,sData,[','])='' then

                        sDTMJ_Str = ExtractWord(20, sData, ",")
                        ' else sDTMJ_Str=ExtractWord(20,sData,[',']);
                        sDTMJ_End = ""
                        sDTMJFrmt = "OTS"

                        sDTML_Str = ExtractWord(29, sData, ",")

                        sDTML_End = ExtractWord(30, sData, ",")
                        sDTML_Frmt = "OTS"
                        EDIWRKInsert("CSV-OTOSAN", "HFT", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, "", "H", "", "", _
                         "", "", "", "")
                    End If
                ElseIf sMsgTyp = "CSV-GUNLUK-OTS" Then
                    Try
                        If nLine = 1 Then

                            For i = 1 To 15
                                sGunTar(i - 1) = ExtractWord((18 + i), sData, ",")
                            Next

                        End If
                        If nLine > 1 Then
                            result = 1
                            sSender = ""
                            sReceiver = My.Settings.Receiver
                            sMsgNam = "FORD-OTOSAN"

                            sMsgNum = ExtractWord(1, sData, ",")

                            sMsgDte = ExtractWord(2, sData, ",")

                            sMsgStrDte = ExtractWord(2, sData, ",")
                            sMsgTxt1 = ""
                            sMsgTxt2 = ""
                            sMsgTxt2 = ""
                            sDtmFrmt = "OTS"

                            sPlantID__1 = ExtractWord(3, sData, ",")

                            sDUNSID = ExtractWord(5, sData, ",")

                            sItnbr = ExtractWord(7, sData, ",")

                            sKapi = ExtractWord(3, sData, ",")

                            sQTY = ExtractWord(37, sData, ",")
                            sQTYDec = "0"
                            sDELTyp = "4"

                            sDElFreq = ExtractWord(33, sData, ",")

                            sDTMJ_Str = ExtractWord(35, sData, ",")
                            sDTMJ_End = ""
                            sDTMJFrmt = "OTS"
                            For i = 1 To 15

                                sQTYA = ExtractWord(18 + i, sData, ",")
                                sQTYA_Dec = "0"
                                sDTML_Str = sGunTar(i - 1)
                                sDTML_End = sGunTar(i - 1)
                                sDTML_Frmt = "OTS"
                                EDIWRKInsert("CSV-OTOSAN", "GNL", sSender, sReceiver, sMsgRef, sMsgNam, _
                                 sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                                 sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                                 sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                                 "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                                 sDTML_End, sDTML_Frmt, "", "G", "", "", _
                                 "", "", "", "")
                            Next
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                ElseIf (sMsgTyp = "VDA-HFT") Then
                    result = 1
                    nCount = Convert.ToInt64(sData.Length / 128)
                    For i = 0 To nCount - 1
                        sData1 = Copy(sData, i * 128, 128)
                        If sData1.Substring(0, 3) = "511" Then
                            sSender = sData1.Substring(6 - 1, 9).TrimEnd()
                            sReceiver = ""
                            sDUNSID = sData1.Substring(15 - 1, 9).TrimEnd()
                        ElseIf sData1.Substring(1 - 1, 3) = "512" Then
                            sMsgNum = sData1.Substring(9 - 1, 9).TrimEnd()
                            sMsgDte = sData1.Substring(18 - 1, 6).TrimEnd()
                            sMsgRef = ""
                            sMsgNam = ""
                            sMsgTxt1 = ""
                            sMsgTxt2 = ""
                            sMsgTxt3 = ""
                            sMsgStrDte = sData1.Substring(18 - 1, 6).TrimEnd()
                            sDtmFrmt = "VDA"
                            sPlantID__1 = sData1.Substring(6 - 1, 3).TrimEnd()
                            sKapi = sData1.Substring(95 - 1, 5).TrimEnd()
                            sItnbr = sData1.Substring(39 - 1, 22).TrimEnd()
                            sKontratNo = sData1.Substring(83 - 1, 12).TrimEnd()
                            sQtyTyp = "AC"
                        ElseIf sData1.Substring(1 - 1, 3) = "513" Then
                            sDTMJ_Str = sData1.Substring(6 - 1, 6).TrimEnd()
                            sDTMJ_End = ""
                            sDTMJFrmt = "VDA"
                            sQTY = sData1.Substring(38 - 1, 10).TrimEnd()
                            sQTYDec = "0"
                            For J = 0 To 4
                                If (sData1.Substring(48 + 15 * J - 1, 6) = "333333") Then
                                    sQtyTyp = "BK"
                                ElseIf (sData1.Substring(48 + 15 * J - 1, 6) = "444444") Then
                                    sQtyTyp = "AC"
                                ElseIf (sData1.Substring(48 + 15 * J - 1, 6) = "555555") Then
                                    sQtyTyp = "AY"
                                ElseIf (sData1.Substring(48 + 15 * J - 1, 6) = "222222") Then
                                    sQtyTyp = "IP"
                                ElseIf (sData1.Substring(48 + 15 * J - 1, 6) = "999999") Then
                                    sQtyTyp = "99"
                                ElseIf sData1.Substring(48 + 15 * J - 1, 6).Trim() = "" Then
                                    sQtyTyp = ""
                                End If
                                If (sQtyTyp = "BK") Then
                                    sDTML_Str = sData1.Substring(48 + 15 * J - 1, 6)
                                    If (sDTML_Str = "333333") Then
                                        '@ Unsupported function or procedure: 'FormatDateTime'
                                        sDTML_Str = Now.Date.ToString("yyMMdd")
                                    End If
                                    sDTML_Frmt = "VDA"
                                    sQTYA = sData1.Substring(54 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    bKayit = True
                                    If (sData1.Substring(48 + 15 * J - 1, 6) = "333333") AndAlso (sQTYA = "000000000") Then
                                        bKayit = False
                                    End If
                                    sDELTyp = "1"
                                ElseIf (sQtyTyp = "AC") Then
                                    sDTML_Str = sData1.Substring(48 + 15 * J - 1, 6)
                                    If sDTML_Str = "444444" Then
                                        '@ Unsupported function or procedure: 'FormatDateTime'
                                        sDTML_Str = Now.Date.ToString("yyMMdd")
                                    End If
                                    sDTML_Frmt = "VDA"
                                    sQTYA = sData1.Substring(54 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    bKayit = True
                                    If (sData1.Substring(48 + 15 * J - 1, 6) = "444444") AndAlso (sQTYA = "000000000") Then
                                        bKayit = False
                                    End If
                                    sDELTyp = "10"
                                ElseIf (sQtyTyp = "AY") Then
                                    sDTML_Str = sData1.Substring(48 + 15 * J - 1, 6)
                                    If sData1.Substring(48 + 15 * J - 1, 6) <> "555555" Then
                                        If sDTML_Str.Substring(3 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD1"
                                        ElseIf sDTML_Str.Substring(5 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD2"
                                        Else
                                            sDTML_Frmt = "VD3"
                                        End If
                                        bKayit = True
                                    Else
                                        bKayit = False
                                    End If
                                    sQTYA = sData1.Substring(54 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    sDELTyp = "4"
                                ElseIf (sQtyTyp = "IP") Then
                                    '@ Unsupported function or procedure: 'FormatDateTime'
                                    sDTML_Str = Now.Date.ToString("yyMMdd")
                                    sDTML_Frmt = "VDA"
                                    sQTYA = "0"
                                    sQTYA_Dec = "0"
                                    sDELTyp = "9"
                                    bKayit = True
                                ElseIf (sQtyTyp = "99") Then

                                    bKayit = False

                                Else
                                    sDTML_Str = sData1.Substring(48 + 15 * J - 1, 6)
                                    sDTML_Frmt = "VDA"
                                    If sQtyTyp = "AY" Then
                                        If sDTML_Str.Substring(3 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD1"
                                        ElseIf sDTML_Str.Substring(5 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD2"
                                        Else
                                            sDTML_Frmt = "VD3"
                                        End If
                                    End If
                                    sQTYA = sData1.Substring(54 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    If sDTML_Str <> "000000" Then
                                        bKayit = True
                                    Else
                                        bKayit = False
                                    End If
                                End If
                                If sQTYA.Trim() = "" Then
                                    bKayit = False
                                End If
                                If sDTML_Str.Trim() = "" Then
                                    bKayit = False
                                End If
                                If bKayit Then
                                    EDIWRKInsert("VDA", "4905", sSender, sReceiver, sMsgRef, sMsgNam, _
                                     sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                                     sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                                     sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                                     "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                                     sDTML_End, sDTML_Frmt, "", "H", "", "", _
                                     "", "", "", "")
                                    bKayit = False
                                End If
                            Next
                        ElseIf sData1.Substring(1 - 1, 3) = "514" Then
                            For J = 0 To 7
                                If (sData1.Substring(6 + 15 * J - 1, 6) = "333333") Then
                                    sQtyTyp = "BK"
                                ElseIf (sData1.Substring(6 + 15 * J - 1, 6) = "444444") Then
                                    sQtyTyp = "AC"
                                ElseIf (sData1.Substring(6 + 15 * J - 1, 6) = "555555") Then
                                    sQtyTyp = "AY"
                                ElseIf (sData1.Substring(6 + 15 * J - 1, 6) = "222222") Then
                                    sQtyTyp = "IP"
                                ElseIf (sData1.Substring(6 + 15 * J - 1, 6) = "999999") Then
                                    sQtyTyp = "99"
                                ElseIf sData1.Substring(6 + 15 * J - 1, 6).Trim() = "" Then
                                    sQtyTyp = ""
                                End If
                                If (sQtyTyp = "BK") Then
                                    sDTML_Str = sData1.Substring(6 + 15 * J - 1, 6)
                                    If (sDTML_Str = "333333") Then
                                        '@ Unsupported function or procedure: 'FormatDateTime'
                                        sDTML_Str = Now.Date.ToString("yyMMdd")
                                    End If
                                    sDTML_Frmt = "VDA"
                                    sQTYA = sData1.Substring(12 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    bKayit = True
                                    If (sData1.Substring(6 + 15 * J - 1, 6) = "333333") AndAlso (sQTYA = "000000000") Then
                                        bKayit = False
                                    End If
                                    sDELTyp = "1"
                                ElseIf (sQtyTyp = "AC") Then
                                    sDTML_Str = sData1.Substring(6 + 15 * J - 1, 6)
                                    If sDTML_Str = "444444" Then

                                        sDTML_Str = Now.Date.ToString("yyMMdd")
                                    End If
                                    sDTML_Frmt = "VDA"
                                    sQTYA = sData1.Substring(12 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    bKayit = True
                                    If (sData1.Substring(6 + 15 * J - 1, 6) = "444444") AndAlso (sQTYA = "000000000") Then
                                        bKayit = False
                                    End If
                                    sDELTyp = "10"
                                ElseIf (sQtyTyp = "IP") Then
                                    '@ Unsupported function or procedure: 'FormatDateTime'
                                    sDTML_Str = Now.Date.ToString("yyMMdd")
                                    sDTML_Frmt = "VDA"
                                    sQTYA = "0"
                                    sQTYA_Dec = "0"
                                    sDELTyp = "9"
                                    bKayit = True
                                ElseIf (sQtyTyp = "AY") Then
                                    sDTML_Str = sData1.Substring(6 + 15 * J - 1, 6)
                                    If sData1.Substring(6 + 15 * J - 1, 6) <> "555555" Then
                                        If sDTML_Str.Substring(3 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD1"
                                        ElseIf sDTML_Str.Substring(5 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD2"
                                        Else
                                            sDTML_Frmt = "VD3"
                                        End If
                                        bKayit = True
                                    Else
                                        bKayit = False
                                    End If
                                    sQTYA = sData1.Substring(12 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    sDELTyp = "4"
                                ElseIf (sQtyTyp = "99") Then

                                    bKayit = False

                                Else
                                    sDTML_Str = sData1.Substring(6 + 15 * J - 1, 6)
                                    If sQtyTyp = "AY" Then
                                        If sDTML_Str.Substring(3 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD1"
                                        ElseIf sDTML_Str.Substring(5 - 1, 2) = "00" Then
                                            sDTML_Frmt = "VD2"
                                        Else
                                            sDTML_Frmt = "VD3"
                                        End If
                                    End If
                                    sQTYA = sData1.Substring(12 + 15 * J - 1, 9)
                                    sQTYA_Dec = "0"
                                    If sDTML_Str <> "000000" Then
                                        bKayit = True
                                    Else
                                        bKayit = False
                                    End If
                                End If
                                If sQTYA.Trim() = "" Then
                                    bKayit = False
                                End If
                                If sDTML_Str.Trim() = "" Then
                                    bKayit = False
                                End If
                                If (sQTYA.Trim() <> "") AndAlso (sDTML_Str.Trim() <> "") Then
                                    If (Convert.ToSingle(sQTYA) = 0) AndAlso (Convert.ToSingle(sDTML_Str) = 0) Then
                                        bKayit = False
                                    End If
                                    If (Convert.ToSingle(sDTML_Str) = 0) Then
                                        bKayit = False
                                    End If
                                End If
                                If bKayit Then
                                    EDIWRKInsert("VDA", "4905", sSender, sReceiver, sMsgRef, sMsgNam, _
                                     sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                                     sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                                     sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                                     "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                                     sDTML_End, sDTML_Frmt, "", "H", "", "", _
                                     "", "", "", "")
                                    bKayit = False
                                End If
                            Next
                        End If
                    Next
                ElseIf (sMsgTyp = "VDA-GNL") Then
                    result = 1
                    nCount = Convert.ToInt64(sData.Length / 128)
                    For i = 0 To nCount - 1
                        sData1 = Copy(sData, i * 128, 128)
                        If sData1.Substring(1 - 1, 3) = "551" Then
                            sSender = sData1.Substring(6 - 1, 9).TrimEnd()
                            sReceiver = ""
                            sDUNSID = sData1.Substring(15 - 1, 9).TrimEnd()
                        ElseIf sData1.Substring(1 - 1, 3) = "552" Then
                            sMsgNum = sData1.Substring(9 - 1, 9).TrimEnd()
                            sMsgDte = sData1.Substring(18 - 1, 6).TrimEnd()
                            sMsgRef = ""
                            sMsgNam = ""
                            sMsgTxt1 = ""
                            sMsgTxt2 = ""
                            sMsgTxt3 = ""
                            sMsgStrDte = sData1.Substring(18 - 1, 6).TrimEnd()
                            sDtmFrmt = "VDA"
                            sPlantID__1 = sData1.Substring(6 - 1, 3).TrimEnd()
                            'Dim handlingCode As String = ""
                            sHandlingCode = sData1.Substring(72, 8).TrimEnd()
                            sKapi = sData1.Substring(68 - 1, 5).TrimEnd()
                            ' sKontratNo=trimright(copy(sData1,89,12));
                            sItnbr = sData1.Substring(24 - 1, 22).TrimEnd()
                        ElseIf sData1.Substring(1 - 1, 3) = "553" Then
                            sQTY = sData1.Substring(6 - 1, 10).TrimEnd()
                            sQTYDec = "0"
                            sDTMJ_Str = sData1.Substring(16 - 1, 6).TrimEnd()
                            sDTMJ_End = ""
                            sDTMJFrmt = "VDA"
                        ElseIf sData1.Substring(1 - 1, 3) = "554" Then
                            For J = 0 To 5
                                sDTML_Str = sData1.Substring(5 + (20 * J), 6)
                                sQTYA = sData1.Substring(11 + (20 * J), 13)
                                bKayit = True
                                sDELTyp = "10"
                                sDTML_Frmt = "VDA"
                                If sQTYA.Trim() = "" Then
                                    bKayit = False
                                End If
                                If sDTML_Str.Trim() = "" Then
                                    bKayit = False
                                End If
                                If (sQTYA.Trim() <> "") AndAlso (sDTML_Str.Trim() <> "") Then
                                    If (Convert.ToSingle(sQTYA) = 0) AndAlso (Convert.ToSingle(sDTML_Str) = 0) Then
                                        bKayit = False
                                    End If
                                    If (Convert.ToSingle(sDTML_Str) = 0) Then
                                        bKayit = False
                                    End If
                                End If
                                If bKayit Then
                                    'EDIWRKInsert("VDA", "4913", sSender, sReceiver, sMsgRef, sMsgNam,
                                    ' sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1,
                                    ' sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi,
                                    ' sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt,
                                    ' "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str,
                                    ' sDTML_End, sDTML_Frmt, "", IIf(sSender = "FORDOTOTR", "H", "G"), "", sHandlingCode,
                                    ' "", "", "", "")
                                    EDIWRKInsert("VDA", "4913", sSender, sReceiver, sMsgRef, sMsgNam,
                                     sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1,
                                     sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi,
                                     sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt,
                                     "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str,
                                     sDTML_End, sDTML_Frmt, "", "G", "", sHandlingCode,
                                     "", "", "", "")
                                    bKayit = False
                                End If
                            Next
                        End If
                    Next
                ElseIf (sMsgTyp = "TOYOTA-HFT") Then
                    If nLine > 1 Then
                        result = 1
                        sSender = ""
                        sReceiver = My.Settings.Receiver
                        sMsgNam = "TOYOTA"
                        sMsgNum = "1"

                        sMsgDte = ExtractWord(8, sData, """")

                        sMsgStrDte = ExtractWord(8, sData, """")
                        sMsgTxt1 = ""
                        sMsgTxt2 = ""
                        sMsgTxt2 = ""
                        sDtmFrmt = "TYT"

                        sPlantID__1 = ExtractWord(6, sData, """")

                        sDUNSID = ExtractWord(1, sData, """")

                        sItnbr = ExtractWord(16, sData, """")

                        sKapi = ExtractWord(6, sData, """")
                        sQTY = "0"
                        sQTYDec = "0"
                        sDELTyp = "4"
                        sDElFreq = ""
                        sDTMJ_Str = ""
                        sDTMJ_End = ""
                        sDTMJFrmt = ""

                        sQTYA = ExtractWord(19, sData, """")

                        sQTYA = ExtractWord(2, sQTYA, ",")
                        sQTYA_Dec = "0"

                        sDTML_Str = ExtractWord(18, sData, """")
                        sDTML_End = ""
                        sDTML_Frmt = "TY1"
                        EDIWRKInsert("TOYOTA-HFT", "HFT", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, "", "H", "", "", _
                         "", "", "", "")
                    End If
                ElseIf (sMsgTyp = "TOYOTA-GNL") Then
                    If nLine > 1 Then
                        '@ Undeclared identifier(3): 'FindAndReplace'
                        sData = FindAndReplace(sData, ",,", ","""",")
                        result = 1
                        sSender = ""
                        sReceiver = My.Settings.Receiver
                        sMsgNam = "TOYOTA"
                        sMsgNum = "1"

                        sMsgDte = ExtractWord(8, sData, """")

                        sMsgStrDte = ExtractWord(8, sData, """")
                        sMsgTxt1 = ""
                        sMsgTxt2 = ""
                        sMsgTxt2 = ""
                        sDtmFrmt = "TYT"

                        sPlantID__1 = ExtractWord(6, sData, """")

                        sDUNSID = ExtractWord(2, sData, """")

                        sItnbr = ExtractWord(16, sData, """")

                        sKapi = ExtractWord(6, sData, """")
                        sQTY = "0"
                        sQTYDec = "0"
                        sDELTyp = "4"
                        sDElFreq = ""
                        sDTMJ_Str = ""
                        sDTMJ_End = ""
                        sDTMJFrmt = ""

                        sQTYA = ExtractWord(25, sData, """")

                        sQTYA = ExtractWord(2, sQTYA, ",")
                        sQTYA_Dec = "0"

                        sDTML_Str = ExtractWord(18, sData, """")

                        sPUSNo = ExtractWord(24, sData, """")
                        If sPUSNo <> "" Then
                            sMsgNum = sPUSNo
                        End If

                        sPUSNo = ExtractWord(22, sData, """")
                        ' Invoice No bilgisinin gelmesi istendi. Mehmet Tufekcioglu 04/04/2005 KADER
                        sDTML_End = ""
                        sDTML_Frmt = "TY2"
                        EDIWRKInsert("TOYOTA-GNL", "GNL", sSender, sReceiver, sMsgRef, sMsgNam, _
                         sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                         sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                         sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                         "", sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                         sDTML_End, sDTML_Frmt, sPUSNo, "G", "", "", _
                         "", "", "", "")
                    End If
                End If
                ' BAR.Progress = BAR.Progress +SizeOf(SData);
                nLine = nLine + 1

                sData = objStreamReader.ReadLine

            Loop

            objStreamReader.Close()

            If bKayit Then
                EDIWRKInsert("DELFOR", "D97A", sSender, sReceiver, sMsgRef, sMsgNam, _
                 sMsgNum, sMsgDte, sMsgStrDte, sMsgEndDte, sDtmFrmt, sMsgTxt1, _
                 sMsgTxt2, sMsgTxt3, sDUNSID, sPlantID__1, sItnbr, sKapi, _
                 sKontratNo, sQTY, sQTYDec, sDTMJ_Str, sDTMJ_End, sDTMJFrmt, _
                 sRFFD_Ref, sDELTyp, sDElFreq, sQTYA, sQTYA_Dec, sDTML_Str, _
                 sDTML_End, sDTML_Frmt, "", "H", sMIssuer, sMHandCode, _
                 sKanban, "", "", "")
                bKayit = False
            End If
            ' dtmEkip.dtbEkip.Commit;

            ' ******************(Ford - OTOSAN) mesajý için sipariþ edilen miktarýnýn bulunmasý
            If (sMsgTyp = "DELJIT") AndAlso (sVersion = "D") AndAlso (sRelease = "98B") AndAlso (sSender = "9041680202001") Then

                Sorgu = "SELECT sender,plantid,gateid,cusitm,contno,cumqty,delqty,deldte " & _
                         " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & " " & _
                         " WHERE sender = '9041680202001' " & " AND MSGSTAT = 'G' " & _
                            " And CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                        " ORDER BY sender,cusitm,deldte"

                dt = db.RunSql(Sorgu)

                For Each row As DataRow In dt.Rows

                    nCumQty = row("CUMQTY").ToString

                    nDelQty = row("DELQTY").ToString

                    Sorgu = "SELECT delqty " & _
                            " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                            " WHERE sender = '9041680202001' " & "AND MSGSTAT = 'G' " & _
                                " AND plantid = '" & row("PLANTID").ToString & "' " & _
                                " AND gateid = '" & row("GATEID").ToString & "' " & _
                                " AND cusitm = '" & row("CUSITM").ToString & "' " & _
                                " AND contno = '" & row("CONTNO").ToString & "' " & _
                                " And CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                                " AND deldte = (SELECT MAX(deldte) deldte " & _
                                                    " FROM " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                                                    " WHERE sender = '9041680202001'" & _
                                                    " AND MSGSTAT = 'G'" & _
                                                    " AND plantid = '" & row("PLANTID").ToString & "'" & _
                                                    " AND gateid = '" & row("GATEID").ToString & "'" & _
                                                    " AND cusitm = '" & row("CUSITM").ToString & "'" & _
                                                    " AND contno = '" & row("CONTNO").ToString & "'" & _
                                                    " And CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                                                    " AND deldte < " & row("DELDTE").ToString & ")"

                    If dt1.Rows.Count = 0 Then
                        nPrevCum = 0
                    Else
                        nPrevCum = dt.Rows(0)("DELQTY").ToString
                    End If

                    If nCumQty >= nPrevCum Then
                        nPrevCum = nCumQty
                    End If

                    ' Buyuk olan degeri aliyorum. Ya ulasilan cumulatif miktar yada onceki cumulatif miktar
                    nDelQty = nDelQty - nPrevCum
                    If nDelQty < 0 Then
                        nDelQty = 0
                    End If

                    Sorgu = "UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                            " SET USERF6 = " & Convert.ToString(nDelQty) & _
                            " WHERE sender = '9041680202001' " & _
                            " AND   MSGSTAT = 'G' " & _
                            " AND plantid = '" & row("PLANTID").ToString & "' " & _
                            " AND gateid = '" & row("GATEID").ToString & "' " & _
                            " AND cusitm = '" & row("CUSITM").ToString & "' " & _
                            " AND contno = '" & row("CONTNO").ToString & "' " & _
                            " And CRDUSR=" & sTirnakEkle(KullaniciAdi) & _
                            " AND deldte = " & row("DELDTE").ToString

                    db.RunSql(Sorgu, False)

                Next

                Sorgu = "UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                            " SET delqty = userf6" & _
                            " WHERE sender = '9041680202001'" & _
                            " AND MSGSTAT = 'G'" & _
                            " And CRDUSR=" & sTirnakEkle(KullaniciAdi)

                db.RunSql(Sorgu, False)

            End If

            Cursor.Current = Cursors.WaitCursor

            If sHataNedeni <> "" Then
                result = 0
            End If
            If result = 1 Then
                '@ Unsupported property or method(A): 'Add'
                MessageBox.Show(TF_Name & " Dosyasý Baþarýyla Aktarýldý. ...", "Ekip Mapics", MessageBoxButtons.OK)
            Else
                '@ Unsupported property or method(A): 'Add'
                MessageBox.Show(TF_Name & " Dosyasý Aktarýlamadý. Dosya Yapýsý Düzgün Deðil...", "Ekip Mapics", MessageBoxButtons.OK)
            End If

            Return result

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Public Sub DataAktar_ClearLIN()
        sItnbr = ""
        sLocATyp = ""
        sKontratNo = ""
        sRffCTyp = ""
        If sMsgTyp = "DELFOR" Then
            sKapi = ""
        End If
        sQtyTyp = ""
        sQTY = ""
        sQTYDec = ""
        sDTMJTyp = ""
        sMHandCode = ""
        sKanban = ""
        sDTMJ_Str = ""
        sDTMJ_End = ""
        sDTMJFrmt = ""
        sRFFDTyp = ""
        sRFFD_Ref = ""
    End Sub

    Public Sub DataAktar_ClearMessage()
        sMsgRef = ""
        sMsgNam = ""
        sMsgNum = ""
        sPUSNo = ""
        sDELTyp1 = ""
        sDELTyp = ""
        sDtmTyp = ""
        sMsgDte = ""
        sMsgStrDte = ""
        sMsgEndDte = ""
        sDtmFrmt = ""
        sFtxTyp = ""
        sMsgTxt1 = ""
        sMsgTxt2 = ""
        sMsgTxt3 = ""
        sMIssuer = ""
    End Sub

    Public Function EDIWRKInsert(ByVal MesajTip As String, ByVal MesajVer As String, ByVal Sender As String, ByVal Receiver As String, ByVal MsgRef As String, ByVal MsgNam As String, _
        ByVal MsgNum As String, ByVal MsgDte As String, ByVal MsgStrDte As String, ByVal MsgEndDte As String, ByVal DtmFrmt As String, ByVal MsgTxt1 As String, _
        ByVal MsgTxt2 As String, ByVal MsgTxt3 As String, ByVal DUNSID As String, ByVal PlantID As String, ByVal Itnbr As String, ByVal Kapi As String, _
        ByVal KontratNo As String, ByVal QTY As String, ByVal QTYDec As String, ByVal DTMJ_Str As String, ByVal DTMJ_End As String, ByVal DTMJFrm As String, _
        ByVal RFFD_Ref As String, ByVal DELTyp As String, ByVal DElFreq As String, ByVal QTYA As String, ByVal QTYA_Dec As String, ByVal DTML_Str As String, _
        ByVal DTML_End As String, ByVal DTML_Frmt As String, ByVal PUS_No As String, ByVal sMsgStat As String, ByVal sMatIssuer As String, ByVal sMtHndCd As String, _
        ByVal sKnbnNo As String, ByVal QTY1 As String, ByVal SevkYeri As String, ByVal L3PREF As String) As String
        Dim result As String
        Dim sSQL As String
        Dim sMsgTar As String
        Dim sKumTarih As String
        Dim sStrDate As String
        Dim sEndDate As String
        Dim nKumMiktar As Double
        Dim nMiktar As Double
        Dim nUMiktar As Double
        result = "1"
        sMsgTar = "0"
        sKumTarih = "0"
        sStrDate = "0"
        sEndDate = "0"
        nKumMiktar = 0
        nMiktar = 0
        nUMiktar = 0

        Try

            If QTY <> "" Then
                nKumMiktar = Convert.ToSingle(QTY)
            End If
            If QTYA <> "" Then
                nMiktar = Convert.ToSingle(QTYA)
            End If
            If QTY1 <> "" Then
                nUMiktar = Convert.ToSingle(QTY1)
            End If
            ' if (MsgDte='') and (MsgStrDte<>'') then sMsgTar=MsgStrDte
            ' else sMsgTar =MsgDte;
            If MsgEndDte <> "" Then
                sMsgTar = MsgEndDte
            ElseIf (MsgStrDte <> "") Then
                sMsgTar = MsgStrDte
            Else
                sMsgTar = MsgDte
            End If
            sMsgTar = ConvertEDIDate(DtmFrmt, sMsgTar, "")
            If (DTMJ_End <> "") Then
                sKumTarih = DTMJ_End
            ElseIf (DTMJ_End = "") Then
                sKumTarih = DTMJ_Str
            End If
            If sKumTarih <> "" Then
                sKumTarih = ConvertEDIDate(DTMJFrm, sKumTarih, "")
            Else
                sKumTarih = "0"
            End If
            If DTML_Str <> "" Then
                sStrDate = DTML_Str
            End If
            If DTML_End <> "" Then
                sEndDate = DTML_End
            Else
                sEndDate = DTML_Str
            End If
            If (DTML_End = "") AndAlso (DTML_Frmt = "616") Then
                sEndDate = DTML_Str
            End If
            If Kapi = "" Or Kapi Is Nothing Then
                Kapi = sDefGateID
            End If
            Kapi = Copy(Kapi, 0, 10)
            ' nPos=Pos(' ',Kapi);
            ' if nPos>1 then Kapi=copy(Kapi,1,nPos-1);
            ' 03 Mart 2008 Mehmet beyin hata bildirmesi sebebi ile kaldýrýldý   --Zeki
            If (Sender.Trim() = "506") OrElse (Sender.Trim() = "5060") Then
                sKumTarih = sMsgTar
            End If
            sStrDate = ConvertEDIDate(DTML_Frmt, sStrDate, "BA")
            sEndDate = ConvertEDIDate(DTML_Frmt, sEndDate, "BI")

            If (nLookup("COUNT(*)", _
                        "EDIWRK", _
                        " PLANTID=" & sTirnakEkle(PlantID) & _
                        " AND GATEID=" & sTirnakEkle(Kapi) & _
                        " AND CUSITM=" & sTirnakEkle(Itnbr) & _
                        " AND DELDTE=" & sStrDate & _
                        " AND MSGSTAT=" & sTirnakEkle("H") & _
                        " And CRDUSR=" & sTirnakEkle(KullaniciAdi)) <> 0) AndAlso (sMsgStat = "G") Then

                sSQL = " DELETE FROM " & "EDIWRK" & _
                            " WHERE PLANTID=" & sTirnakEkle(PlantID) & _
                            " AND GATEID=" & sTirnakEkle(Kapi) & _
                            " AND CUSITM=" & sTirnakEkle(Itnbr) & _
                            " AND DELDTE=" & sStrDate & _
                            " And CRDUSR=" + sTirnakEkle(KullaniciAdi)

                db.RunSql(sSQL, True)

            End If

            ' if nLookup('COUNT(*)',SetTableName(dtmEkip.LibNameEDI,'EDIWRK'),' PLANTID='+Quote(PlantID)+
            ' ' AND GATEID='+Quote(Kapi)+' AND CUSITM='+Quote(Itnbr)+' AND DELDTE='+sStrDate)=0 then
            ' begin
            If sMsgTar.Length > 8 Then
                sMsgTar = sMsgTar.Substring(0, 8).ToString
            End If
            sSQL = " INSERT INTO " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & _
                    " ( " & " SENDER,RECEIVER,MSGNUM,MSGDTM,MSGTX1," & _
                    " MSGTX2,MSGTX3,DUNSID,PLANTID,CUSITM, " & _
                    " GATEID,CONTNO,CUMQTY,CUMREF,CUMDTE," & _
                    " DELTYP,DELQTY,DTETYP,DTE_STR,DTE_END," & _
                    " DELDTE,MSGSTAT,PUSNO,MISSUER,MATHNDCD," & _
                    " KANBANNO,USERF5,CRDDTE," & _
                    " CRDTIM,CRDUSR,USERF1,L3PREF) VALUES ( " & _
                    sTirnakEkle(Sender) & "," & sTirnakEkle(Receiver) & "," & sTirnakEkle(MsgNum) & "," & sMsgTar & "," & sTirnakEkle(MsgTxt1) & "," & _
                    sTirnakEkle(MsgTxt2) & "," & sTirnakEkle(MsgTxt3) & "," & sTirnakEkle(DUNSID) & "," & sTirnakEkle(PlantID) & "," & sTirnakEkle(Itnbr) & "," & _
                    sTirnakEkle(Kapi) & "," & sTirnakEkle(KontratNo) & "," & Convert.ToString(nKumMiktar) & "," & sTirnakEkle(RFFD_Ref) & "," & sKumTarih & "," & _
                    sTirnakEkle(DELTyp) & "," & Convert.ToString(nMiktar) & "," & sTirnakEkle(DTML_Frmt) & "," & sStrDate & "," & sEndDate & "," & sStrDate & "," & _
                    sTirnakEkle(sMsgStat) & "," & sTirnakEkle(PUS_No) & "," & sTirnakEkle(sMatIssuer) & "," & sTirnakEkle(sMtHndCd) & "," & sTirnakEkle(sKnbnNo) & "," & _
                    Convert.ToString(nUMiktar) & "," & Now.Date.ToString("yyyyMMdd") & "," & Now.ToString("HHmm") & "," & sTirnakEkle(KullaniciAdi) & ", " & "'" & _
                    SevkYeri & "', '" & L3PREF & "' ) "

            db.RunSql(sSQL, True)

            Return result

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Public Function EDIWRKUpdateKumul(ByVal MsgNum As String, ByVal Plant As String, ByVal Itnbr As String, ByVal Kapi As String, ByVal QTY As String, ByVal QTYDec As String, _
        ByVal DTMKumTyp As String, ByVal DTMKumStr As String, ByVal DTMKumEnd As String, ByVal DTMKumFrm As String) As String
        Dim result As String = [String].Empty
        Dim sSQL As String
        Dim sKumTarih As String = ""
        Dim nKumMiktar As Double

        Try

            If (DTMKumEnd <> "") Then
                sKumTarih = DTMKumEnd
            ElseIf (DTMKumEnd = "") Then
                sKumTarih = DTMKumStr
            End If

            sKumTarih = ConvertEDIDate(DTMKumFrm, sKumTarih, "")

            If QTY <> "" Then
                nKumMiktar = Convert.ToSingle(QTY)
            Else
                nKumMiktar = 0
            End If

            If Kapi = "" Then
                Kapi = sDefGateID
            End If

            sSQL = (((((((" UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & " SET CUMQTY=") + Convert.ToString(nKumMiktar) & " ,CUMDTE=") + sKumTarih & " ,CUMREF=") + sTirnakEkle("") & " WHERE PLANTID=") + sTirnakEkle(Plant) & " AND GATEID=") + sTirnakEkle(Kapi) & " AND CUSITM=") + sTirnakEkle(Itnbr) & " And CRDUSR=") + sTirnakEkle(KullaniciAdi)

            db.RunSql(sSQL, True)

            Return result

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Public Function EDIWRKUpdateKumul1(ByVal MsgNum As String, ByVal Plant As String, ByVal Itnbr As String, ByVal Kapi As String, ByVal QTY1 As String) As String
        Dim result As String = [String].Empty
        Dim sSQL As String
        Dim nUMiktar As Double

        Try

            If QTY1 <> "" Then
                nUMiktar = Convert.ToSingle(QTY1)
            Else
                nUMiktar = 0
            End If
            If Kapi = "" Then
                Kapi = sDefGateID
            End If
            sSQL = (((((" UPDATE " & SetTableName(My.Settings.LibNameEDI, "EDIWRK") & " SET userf5=") + Convert.ToString(nUMiktar) & " WHERE PLANTID=") + sTirnakEkle(Plant) & " AND GATEID=") + sTirnakEkle(Kapi) & " AND CUSITM=") + sTirnakEkle(Itnbr) & " And CRDUSR=") + sTirnakEkle(KullaniciAdi)

            db.RunSql(sSQL, True)
            Return result

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnDizin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDizin.Click
        'Dim openDir As FolderBrowserDialog = New FolderBrowserDialog
        '
        openDir.Description = " Görüntülemek Ýstediðiniz Dizini Seçiniz! "
        '
        'DialogResult = openDir.ShowDialog(Me)
        '
        openDir.SelectedPath = txtPath.Text

        If openDir.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '
            If IO.Directory.Exists(openDir.SelectedPath) = True Then
                '
                txtPath.Text = openDir.SelectedPath.ToString
                '
                DosyaListele(openDir.SelectedPath.ToString)
                '
            Else
                '
                MessageBox.Show("Böyle Bir Dizin Bulunamadý!", " Error", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
                '
            End If
            '
        End If
        '
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnMesajAl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMesajAl.Click
        Dim sSndrOdetteId, sRcvrOdetteId, sMsgNum, _
            sMsgDate, sDunsId, sPlantId, sItemNum, _
            sCumRef, sCumQty, sCumDate, sDelType, sDelQty, sDateType, sDelStrDate, sDelEndDate, sDelDate, _
             sFileName As String

        Dim sMTip, sODID, sPlant, sUserName As String

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        checkedRows = Me.GridEX1.GetCheckedRows()

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        sDelDate = ""
        sDelEndDate = ""
        sDelStrDate = ""
        sSndrOdetteId = ""
        sRcvrOdetteId = ""
        sMsgNum = ""
        sMsgDate = ""
        sDunsId = ""
        sPlantId = ""
        sItemNum = ""
        sCumQty = ""
        sCumRef = ""
        sCumDate = ""
        sDelType = ""
        sDelQty = ""
        sDateType = ""

        If checkedRows.Length = 0 Then

            MessageBox.Show("Lütfen Bir Dosya Seçiniz")

        Else

            Dim row As Janus.Windows.GridEX.GridEXRow

            Try
                sDefGateID = sLookup("DEFGATE", "EDIPRM", " SIRKET='" & My.Settings.Company.ToString & "'")

                'sQuery = " Delete From EdiWrk " & " WHERE  CRDUSR=" & sTirnakEkle(KullaniciAdi)

                'db.RunSql(sQuery, True)

                For Each row In checkedRows

                    sMTip = row.Cells("Msg. Tipi").Text

                    sODID = row.Cells("Odette Id").Text

                    sPlant = row.Cells("Plant Kodu").Text

                    If (sMTip = "DELFOR") Or (sMTip = "DELINS") Or (sMTip = "CSV-HAFTALIK-OTS") Or (sMTip = "VDA-HFT") Or (sMTip = "TOYOTA-HFT") Then

                        If nLookup("COUNT(*)", "EDIWRK", " MSGSTAT<>" & sTirnakEkle("H") & "And Sender=" & sTirnakEkle(sODID) & " And PlantId=" & sTirnakEkle(sPlant)) <> 0 Then

                            sUserName = sLookup("Min(CrdUsr)", "EDIWRK", " MSGSTAT<>" & sTirnakEkle("H") & "And Sender=" & sTirnakEkle(sODID) & " And PlantId=" & sTirnakEkle(sPlant))

                            MessageBox.Show("Mapics e Aktarýlmamýþ sipariþler arasýnda " & sUserName & " kullanýcýsýna ait mesaj tipi uyumsuz kayýtlar var,iþlem yapamazsýnýz...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            Exit Sub

                        End If

                    ElseIf (sMTip = "DELJIT") Or (sMTip = "CSV-GUNLUK-OTS") Or (sMTip = "VDA-GNL") Or (sMTip = "TOYOTA-GNL") Then

                        If nLookup("COUNT(*)", "EDIWRK", " MSGSTAT<>" & sTirnakEkle("G") & "And Sender=" & sTirnakEkle(sODID) & " And PlantId=" & sTirnakEkle(sPlant)) <> 0 Then

                            sUserName = sLookup("Min(CrdUsr)", "EDIWRK", " MSGSTAT<>" & sTirnakEkle("G") & "And Sender=" & sTirnakEkle(sODID) & " And PlantId=" & sTirnakEkle(sPlant))

                            MessageBox.Show("Mapics e Aktarýlmamýþ sipariþler arasýnda " & sUserName & " kullanýcýsýna ait mesaj tipi uyumsuz kayýtlar var,iþlem yapamazsýnýz...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            Exit Sub

                        End If

                    ElseIf sMTip = "DELJIT-PUS" Then

                        If nLookup("COUNT(*)", "EDIWRK", " MSGSTAT<>" & sTirnakEkle("P") & "And Sender=" & sTirnakEkle(sODID) & " And PlantId=" & sTirnakEkle(sPlant)) <> 0 Then

                            sUserName = sLookup("Min(CrdUsr)", "EDIWRK", " MSGSTAT<>" & sTirnakEkle("P") & "And Sender=" & sTirnakEkle(sODID) & " And PlantId=" & sTirnakEkle(sPlant))

                            MessageBox.Show("Mapics e Aktarýlmamýþ sipariþler arasýnda " & sUserName & " kullanýcýsýna ait mesaj tipi uyumsuz kayýtlar var,iþlem yapamazsýnýz...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            Exit Sub

                        End If

                    End If

                    If nLookup("count(*)", "EDIWRK", " PlantId=" & sTirnakEkle(row.Cells("Plant Kodu").Text)) > 0 Then

                        MessageBox.Show("'Syteline a Aktarýlmamýþ sipariþler arasýnda " & row.Cells("Plant Kodu").Text & " Plant ine ait kayýtlar var,iþlem yapamazsýnýz...")

                        Exit Sub

                    End If

                    sFileName = row.Cells(1).Text

                    DataAktar(txtPath.Text & "\" & row.Cells(1).Text)

                    'objStreamReader = New StreamReader(txtPath.Text & "\" & row.Cells(1).Text)

                    'strLine = objStreamReader.ReadLine

                    'Do While Not strLine Is Nothing

                    '    myStrArrTirnak = strLine.ToString.Split("'")

                    '    myStrArr = myStrArrTirnak(0).ToString.Split("+")

                    '    If myStrArr(0).ToString = "UNB" Then

                    '        sSndrOdetteId = myStrArr(2).ToString

                    '        sRcvrOdetteId = myStrArr(3).ToString.Split(":")(0)

                    '        sMsgDate = "20" & myStrArr(4).ToString.Split(":")(0)

                    '    End If

                    '    'BGM+241::10+00275'

                    '    If myStrArr(0).ToString = "BGM" Then

                    '        sMsgNum = myStrArr(2).ToString

                    '    End If

                    '    strLine = objStreamReader.ReadLine

                    'Loop

                    'Select Case sSndrOdetteId

                    '    Case "9390"
                    '        RBoschMesajAlimi(txtPath.Text & "\" & row.Cells(1).Text)

                    '    Case "9550"
                    '        RBoschMesajAlimi(txtPath.Text & "\" & row.Cells(1).Text)

                    '    Case "0150"
                    '        RBoschMesajAlimi(txtPath.Text & "\" & row.Cells(1).Text)

                    '    Case "0601"
                    '        RBoschMesajAlimi(txtPath.Text & "\" & row.Cells(1).Text)

                    '    Case "O0013000579MB020000"
                    '        DCMesajAlimi(txtPath.Text & "\" & row.Cells(1).Text)

                    'End Select

                    'objStreamReader.Close()

                    Dim sBckdir As String

                    sBckdir = sLookup("EDIBCKDIR", "EDIPRM", " SIRKET='" & My.Settings.Company & "'")

                    If sBckdir <> "" Then

                        If Not System.IO.Directory.Exists(sBckdir) Then

                            System.IO.Directory.CreateDirectory(sBckdir)

                        End If
                        Dim sDir As String

                        sDir = txtPath.Text.Split("\")(txtPath.Text.Split("\").Length - 1).ToString

                        sDir = IIf(sDir = "", "\", "\" & sDir & "\")

                        If Not System.IO.Directory.Exists(sBckdir & sDir) Then

                            System.IO.Directory.CreateDirectory(sBckdir & sDir)

                        End If

                        If System.IO.File.Exists(sBckdir & sDir & sMsgNum & "_" & sFileName) Then

                            System.IO.File.Delete(sBckdir & sDir & sMsgNum & "_" & sFileName)

                        End If

                        System.IO.File.Move(txtPath.Text & "\" & sFileName, sBckdir & sDir & sMsgNum & "_" & sFileName)

                    End If

                    'MsgBox(row.Cells(1).Text & " Adlý dosyanýn okuma iþlemi tamamlandý ", MsgBoxStyle.Information, " Ekip Mapics ")

                Next row

            Catch ex As Exception

                MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If

        Windows.Forms.Cursor.Current = Cursors.Arrow

        DosyaListele(txtPath.Text.ToString)
    End Sub

    Private Sub btnMesajGoruntule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()

        frmEdiSyteLineAktarimIslemi.ShowDialog()

        Me.Close()
    End Sub

    Private Sub DCMesajAlimi(ByVal sFileName As String)
        Dim sSndrOdetteId As String = ""
        Dim sRcvrOdetteId As String = ""
        Dim sMsgType As String = ""
        Dim sMsgVersion As String = ""
        Dim sMsgRelease As String = ""
        Dim sMsgNum As String = ""
        Dim sMsgDate As String = ""
        Dim sMsgDateFrmt As String = ""
        Dim sDunsId As String = ""
        Dim sPlantId As String = ""
        Dim sItemNum As String = ""
        Dim sContratNum As String = ""
        Dim sCumRef As String = ""
        Dim sCumQty As String = ""
        Dim sCumDate As String = ""
        Dim sDelType As String = ""
        Dim sDelQty As String = ""
        Dim sDateType As String = ""
        Dim sDelStrDate As String = ""
        Dim sDelEndDate As String = ""
        Dim sDelDate As String = ""
        Dim sMsgStat As String = ""
        Dim strLine As String = ""
        Dim myStrArr() As String
        Dim myStrArrTirnak() As String
        Dim sQuery As String = ""

        Dim sGate As String = ""

        Dim bEkleEdiWrk As Boolean

        Dim objStreamReader As StreamReader

        Dim iSayac, i As Integer

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        checkedRows = Me.GridEX1.GetCheckedRows()

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        bEkleEdiWrk = False

        sDelDate = ""
        sDelEndDate = ""
        sDelStrDate = ""
        sSndrOdetteId = ""
        sRcvrOdetteId = ""
        sMsgNum = ""
        sMsgDate = ""
        sDunsId = ""
        sPlantId = ""
        sItemNum = ""
        sCumQty = ""
        sCumRef = ""
        sCumDate = ""
        sDelType = ""
        sDelQty = ""
        sDateType = ""

        sDefGateID = sLookup("DEFGATE", "EDIPRM", " SIRKET='" & My.Settings.Company.ToString & "'")

        sQuery = " Delete From EdiWrk "

        db.RunSql(sQuery, True)

        Try

            objStreamReader = New StreamReader(sFileName)

            strLine = objStreamReader.ReadLine

            Do While Not strLine Is Nothing

                myStrArrTirnak = strLine.ToString.Split("'")

                For i = 0 To myStrArrTirnak.Length - 1

                    myStrArr = myStrArrTirnak(i).ToString.Split("+")

                    If myStrArr(0).ToString = "UNB" Then

                        iSayac = 0

                        sSndrOdetteId = myStrArr(2).ToString

                        sRcvrOdetteId = myStrArr(3).ToString.Split(":")(0)

                        sMsgDate = "20" & myStrArr(4).ToString.Split(":")(0)

                    End If

                    If myStrArr(0).ToString = "UNH" Then

                        iSayac = 1

                        sMsgType = myStrArr(2).ToString.Split(":")(0)

                        sMsgVersion = myStrArr(2).ToString.Split(":")(1)

                        sMsgRelease = myStrArr(2).ToString.Split(":")(2)

                    End If

                    If myStrArr(0).ToString = "BGM" Then

                        If myStrArr(1).ToString.Split(":")(0) = "241" Then

                            sMsgNum = Microsoft.VisualBasic.Right(Trim(myStrArr(2).ToString), 5)

                        End If

                    End If

                    If myStrArr(0).ToString = "DTM" Then

                        If myStrArr(1).ToString.Split(":")(0) = "137" Then

                            sMsgDate = myStrArr(1).ToString.Split(":")(1)

                            sMsgDateFrmt = myStrArr(1).ToString.Split(":")(2)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "50" Then

                            sCumDate = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "2" Then

                            sDelDate = myStrArr(1).ToString.Split(":")(1)

                            sDelEndDate = myStrArr(1).ToString.Split(":")(1)

                            sDelStrDate = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "64" Then

                            sDelDate = myStrArr(1).ToString.Split(":")(1)

                            sDelStrDate = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "63" Then

                            sDelEndDate = myStrArr(1).ToString.Split(":")(1)

                        End If

                    End If

                    If myStrArr(0).ToString = "RFF" Then

                        If myStrArr(1).ToString.Split(":")(0) = "ALM" Then

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "AAK" Then

                            sCumRef = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "CT" Then

                            sContratNum = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "ON" Then

                            sContratNum = myStrArr(1).ToString.Split(":")(1)

                        End If

                    End If

                    If myStrArr(0).ToString = "NAD" Then

                        If myStrArr(1).ToString = "SE" Then

                            sDunsId = myStrArr(2).ToString.Split(":")(0)

                        End If

                        If myStrArr(1).ToString = "CN" Then

                            sPlantId = myStrArr(2).ToString.Split(":")(0)

                        End If

                    End If

                    If myStrArr(0).ToString = "LOC" Then

                        If myStrArr(1).ToString.Split(":")(0) = "11" Then

                            sGate = myStrArr(2).ToString.Split(":")(3)

                            If sGate = "" Then

                                sGate = sDefGateID

                            End If

                        End If

                    End If

                    If myStrArr(0).ToString = "QTY" Then

                        If myStrArr(1).ToString.Split(":")(0) = "70" Then

                            sCumQty = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "113" Then

                            If bEkleEdiWrk = True Then

                                Try
                                    If sCumQty = "" Then

                                        sCumQty = 0

                                        sCumDate = 0

                                    End If

                                    sMsgStat = "H"

                                    sQuery = " INSERT INTO EDIWRK" & _
                                            " (SENDER, RECEIVER, MSGNUM, MSGDTM, " & _
                                            " DUNSID,  PLANTID, CUSITM, GATEID, CONTNO, PUSNO, " & _
                                            " CUMQTY, CUMREF, CUMDTE, DELTYP, DELQTY, DTETYP, DTE_STR, " & _
                                            " DTE_END, DELDTE, MSGSTAT, CRDDTE, CRDTIM, " & _
                                            " CRDUSR)" & _
                                            " VALUES (" & _
                                            "'" & sSndrOdetteId & "','" & sRcvrOdetteId & "'" & _
                                            ",'" & sMsgNum & "'," & sMsgDate & _
                                            ",'" & sDunsId & "' ,'" & sPlantId & "'" & _
                                            ",'" & sItemNum & "','" & sGate & "','" & sContratNum & "'" & _
                                            ",'" & sContratNum & "'," & sCumQty & ",'" & sCumRef & "'" & _
                                            "," & sCumDate & ",'" & sDelType & "'," & sDelQty & ",'" & sDateType & "'" & _
                                            "," & sDelStrDate & "," & sDelEndDate & "," & sDelDate & ",'" & sMsgStat & "'" & _
                                            "," & Now.Date.ToString("yyyyMMdd") & "," & Now.ToString("hhmss") & _
                                            ",'" & KullaniciAdi & "')"

                                    db.RunSql(sQuery, True)

                                    bEkleEdiWrk = False

                                Catch ex As Exception

                                End Try

                            End If

                            sDelQty = myStrArr(1).ToString.Split(":")(1)

                        End If

                    End If

                    If myStrArr(0).ToString = "LIN" Then

                        sItemNum = myStrArr(3).ToString.Split(":")(0)

                    End If

                    If myStrArr(0).ToString = "PIA" Then

                    End If

                    If myStrArr(0).ToString = "SCC" Then

                        sDelType = myStrArr(1).ToString

                        bEkleEdiWrk = True

                    End If

                    If myStrArr(0).ToString = "UNT" Then

                    End If

                    If myStrArr(0).ToString = "UNZ" Then

                    End If

                Next i

                strLine = objStreamReader.ReadLine

            Loop

            objStreamReader.Close()

        Catch ex As Exception

        End Try

        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub DosyaListele(ByVal sDizin As String)
        Dim file As String
        Dim sODID As String
        Dim sMTyp As String
        Dim sVer As String
        Dim sRel As String
        Dim sMCRef As String
        Dim nLine As Integer
        Dim sDte As String = ""
        Dim sFrmt As String = ""
        Dim nCount As Integer
        Dim strLine1 As String
        Dim sPlnt As String
        Dim sNADATyp As String
        Dim sNADCN As String
        Dim sMRef As String
        If Not Directory.Exists(sDizin) Then

            sDizin = "C:\"

            Exit Sub

        End If

        Dim files() As String = Directory.GetFiles(sDizin)

        Dim ds As New DataSet()

        Dim table As New DataTable("Dosya")

        Dim objStreamReader As StreamReader

        Dim strLine, sSaticiNo, sTarih, sTip, sMesajNo As String

        Dim sMesajTipi, sDosyaTarih, sDosyaSaat As String

        Dim i As Integer

        sMesajNo = ""

        sSaticiNo = ""

        sTarih = ""

        sTip = ""

        table.Columns.Add(New DataColumn("DosyaAdi", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Tarih", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Saat", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Msg. Tarihi", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Msg. Tipi", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Odette Id", Type.GetType("System.String")))

        table.Columns.Add(New DataColumn("Plant Kodu", Type.GetType("System.String")))

        ds.Tables.Add(table)

        For Each file In files

            Try

                Dim s As New IO.FileInfo(file)

                sDosyaTarih = s.CreationTime.ToString("dd/MM/yyyy")

                sDosyaSaat = s.CreationTime.ToString("HH:mm")

                sODID = ""
                sPlnt = ""
                sMTyp = ""
                sVer = ""
                sRel = ""
                sMRef = ""
                sMCRef = ""
                nLine = 1

                objStreamReader = New StreamReader(file)

                strLine = objStreamReader.ReadLine

                Do While Not strLine Is Nothing

                    If strLine = "" Then Exit Do
                    If strLine.Contains("+") Then
                        sTip = strLine.Split("+")(0).ToString
                    Else
                        sTip = Copy(strLine, 1 - 1, 4).TrimEnd()
                    End If
                    If sTip = "UNB" Then
                        ' KADER
                        sODID = Copy(strLine, 5 - 1, 35).TrimEnd()

                        sODID = sODID.TrimStart()

                    End If

                    If sTip = "UNH" Then

                        sMTyp = Copy(strLine, 20 - 1, 6).TrimEnd()

                        sVer = Copy(strLine, 27 - 1, 3).TrimEnd()

                        sRel = Copy(strLine, 31 - 1, 3).TrimEnd()

                        If strLine.Length > 44 Then

                            sMCRef = Copy(strLine, 45 - 1, 35).TrimEnd()

                        End If

                    ElseIf (nLine = 1) AndAlso (Copy(strLine, 1 - 1, 3) = "511") Then

                        sMTyp = "VDA-HFT"

                        sDte = Copy(strLine, 34 - 1, 39)

                        sFrmt = "VDA"

                        nCount = Convert.ToInt64(strLine.Length / 128)

                        For i = 0 To nCount - 1

                            strLine1 = Copy(strLine, i * 128 + 1 - 1, 128)

                            If strLine1.Substring(1 - 1, 3) = "512" Then

                                sPlnt = strLine1.Substring(6 - 1, 3).TrimEnd()

                            End If

                            If sPlnt <> "" Then

                                Exit For

                            End If

                        Next

                    ElseIf (nLine = 1) AndAlso (Copy(strLine, 1 - 1, 3) = "551") Then

                        sMTyp = "VDA-GNL"

                        sDte = Copy(strLine, 34 - 1, 39)

                        sFrmt = "VDA"

                        nCount = Convert.ToInt64(strLine.Length / 128)

                        For i = 0 To nCount - 1

                            strLine1 = Copy(strLine, i * 128 + 1 - 1, 128)

                            If strLine1.Substring(1 - 1, 3) = "552" Then

                                sPlnt = strLine1.Substring(6 - 1, 3).TrimEnd()

                            End If

                            If sPlnt <> "" Then

                                Exit For

                            End If

                        Next

                    ElseIf (nLine = 1) AndAlso (Copy(strLine, 1 - 1, 10) = "PROGRAM NO") Then

                        If (nLine = 1) AndAlso (Copy(strLine, 12 - 1, 12) = "FABRIKA KODU") Then

                            sMTyp = "CSV-HAFTALIK-OTS"

                        ElseIf (nLine = 1) AndAlso (Copy(strLine, 12 - 1, 22) = "SEVK. CETVELI YAY. TAR") Then

                            sMTyp = "CSV-GUNLUK-OTS"

                        End If

                    ElseIf (nLine = 1) AndAlso (strLine = """SupplierCode"",""SupplierName"",""DockCode"",""TransmissionDate"",""PartColourCode"",""PartDesc"",""KanbanNo"",""PartNo"",""OrderLot"",""UsageWeekNo"",""TotalPCS"",""LastManifest"",""LeftToOrder""") Then

                        sMTyp = "TOYOTA-HFT"

                    ElseIf (nLine = 1) AndAlso (strLine = """SupplierCode"",""SupplierName"",""DockCode"",""TransmissionDate"",""PartColourCode"",""PartDesc"",""KanbanNo"",""PartNo"",""OrderLot"",""CollectionDate"",""CollectionTime"",""InvoiceNo"",""ManifestNo"",""TotalPCS""") Then

                        sMTyp = "TOYOTA-GNL"

                    End If

                    If (sMTyp = "DELFOR") AndAlso (sVer = "D") AndAlso (sRel = "97A") Then

                        If sTip = "DTM" Then

                            If Copy(strLine, 5 - 1, 3).TrimEnd() = "137" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf Copy(strLine, 5 - 1, 3).TrimEnd() = "158" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                            End If

                            sFrmt = Copy(strLine, 45 - 1, 3).TrimEnd()

                        End If

                        If sTip = "NADA" Then

                            sNADATyp = Copy(strLine, 5 - 1, 3).TrimEnd()

                            If sNADATyp = "ST" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf sNADATyp = "CN" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf sNADATyp = "DP" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            End If

                        ElseIf (sTip = "NAD") AndAlso (sODID = "9041680202001") Then

                            sNADATyp = Copy(strLine, 5 - 1, 3).TrimEnd()

                            If sNADATyp = "MI" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            End If

                        End If

                    ElseIf (sMTyp = "DELJIT") AndAlso (sVer = "D") AndAlso (sRel = "98B") Then

                        If sTip = "DTM" Then

                            If Copy(strLine, 5 - 1, 3).TrimEnd() = "137" Then

                                sDte = Copy(strLine, 8 - 1, 35).TrimEnd()

                            ElseIf Copy(strLine, 5 - 1, 3).TrimEnd() = "158" Then

                                sDte = Copy(strLine, 8 - 1, 35).TrimEnd()

                            End If

                            sFrmt = Copy(strLine, 43 - 1, 3).TrimEnd()

                        End If

                        If sTip = "NAD" Then

                            sNADATyp = Copy(strLine, 5 - 1, 3).TrimEnd()

                            If sNADATyp = "ST" Then

                                sPlnt = Copy(strLine, 8 - 1, 35).TrimEnd()

                            ElseIf (sNADATyp = "CN") AndAlso (sODID = "1780129987") Then

                                sNADCN = Copy(strLine, 8 - 1, 35).TrimEnd()

                                '
                                ' if sNADCN = '09317801299871892' then sPlnt = 'FL'
                                ' else if sNADCN = '09317801299870101' then sPlnt = 'TU'
                                ' else if sNADCN = '09317801299870102' then sPlnt = 'TK'
                                ' else if sNADCN = '09459813295CI' then sPlnt = 'CI'
                                ' else if sNADCN = '0945160796CA' then sPlnt = 'CA'
                                ' else if sNADCN = '0945160796DK' then sPlnt = 'DK'
                                ' else if sNADCN = '0945160796RK' then sPlnt = 'RK'
                                ' else if sNADCN = '0945160796SR' then sPlnt = 'SR'
                                ' else if sNADCN = '09317801299870104' then sPlnt = 'BU';
                                '
                                sPlnt = sLookup("DACSHP", "DACIAPF", " DACEDI=" & sTirnakEkle(sNADCN))

                            ElseIf sNADATyp = "CN" Then

                                sPlnt = Copy(strLine, 8 - 1, 35).TrimEnd()

                            ElseIf sNADATyp = "DP" Then

                                sPlnt = Copy(strLine, 8 - 1, 35).TrimEnd()

                            End If

                        End If

                    ElseIf (sMTyp = "DELFOR") AndAlso (sVer = "1") AndAlso (sRel = "921") Then

                        If sTip = "DTM" Then

                            If Copy(strLine, 5 - 1, 3).TrimEnd() = "137" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                            End If

                            sFrmt = Copy(strLine, 45 - 1, 3).TrimEnd()

                        End If



                        If sTip = "NAD" Then

                            sNADATyp = Copy(strLine, 5 - 1, 3).TrimEnd()

                            If (sNADATyp = "SE") AndAlso (Copy(strLine, 9 - 1, 17).TrimEnd() = "08088582") Then

                                sPlnt = "26"

                            End If

                        End If

                    ElseIf (sMTyp = "DELINS") AndAlso (sVer = "3") Then

                        If sTip = "MID" Then

                            sDte = Copy(strLine, 23 - 1, 6).TrimEnd()

                            sFrmt = "DEL"



                        End If

                        If sTip = "CSG" Then

                            If (sODID = "O0013000018PORSCHE-EDI") OrElse (sODID = "003700000001439912") Then

                                sPlnt = Copy(strLine, 5 - 1, 20).TrimEnd()

                            Else

                                sPlnt = Copy(strLine, 206 - 1, 17).TrimEnd()

                            End If

                        End If

                    ElseIf (sMTyp = "DELFOR") AndAlso (sVer = "D") AndAlso (sRel = "96A") Then

                        If sTip = "DTM" Then

                            If Copy(strLine, 5 - 1, 3).TrimEnd() = "137" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                                If strLine.Length > 44 Then

                                    sFrmt = Copy(strLine, 45 - 1, 3).TrimEnd()

                                End If

                            End If

                        End If

                        If sTip = "NADA" Then

                            sNADATyp = Copy(strLine, 5 - 1, 3).TrimEnd()

                            sNADCN = Copy(strLine, 9 - 1, 35).TrimEnd()

                            If sNADATyp = "CN" Then

                                If (sODID = "1780129987") Then

                                    '
                                    ' if sNADCN = '09317801299871892' then sPlnt = 'FL'
                                    ' else if sNADCN = '09317801299870101' then sPlnt = 'TU'
                                    ' else if sNADCN = '09317801299870102' then sPlnt = 'TK'
                                    ' else if sNADCN = '09317801299870104' then sPlnt = 'BU'
                                    ' else if sNADCN = '09459813295CI' then sPlnt = 'CI'
                                    ' else if sNADCN = '0945160796DK' then sPlnt = 'DK'
                                    ' else if sNADCN = '0945160796RK' then sPlnt = 'RK'
                                    ' else if sNADCN = '0945160796SR' then sPlnt = 'SR'
                                    ' else if sNADCN = '0945160796CA' then sPlnt = 'CA';
                                    '
                                    sPlnt = sLookup("DACSHP", "DACIAPF", " DACEDI=" & sTirnakEkle(sNADCN))

                                Else

                                    sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()
                                    ' Renault

                                End If

                            End If

                        ElseIf (sTip = "SCC") AndAlso (Copy(strLine, 13 - 1, 3).TrimEnd() = "Y") Then

                            sMTyp = "DELJIT"

                        End If

                    ElseIf (sMTyp = "DELJIT") AndAlso (sVer = "D") AndAlso (sRel = "97A") Then

                        If sTip = "DTM" Then

                            If Copy(strLine, 5 - 1, 3).TrimEnd() = "137" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf Copy(strLine, 5 - 1, 3).TrimEnd() = "158" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf Copy(strLine, 5 - 1, 3).TrimEnd() = "234" Then

                                sDte = Copy(strLine, 9 - 1, 35).TrimEnd()

                            End If

                            sFrmt = Copy(strLine, 45 - 1, 3).TrimEnd()

                        End If

                        If sTip = "NAD" Then

                            sNADATyp = Copy(strLine, 5 - 1, 3).TrimEnd()

                            If sNADATyp = "ST" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf sNADATyp = "CN" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            ElseIf sNADATyp = "DP" Then

                                sPlnt = Copy(strLine, 9 - 1, 35).TrimEnd()

                            End If

                        End If

                    ElseIf (nLine > 1) AndAlso (sMTyp = "CSV-HAFTALIK-OTS") Then

                        sDte = strLine.ToString.Split(",")(10)

                        sFrmt = "OTS"

                        sPlnt = strLine.ToString.Split(",")(1)

                    ElseIf (nLine > 1) AndAlso (sMTyp = "CSV-GUNLUK-OTS") Then

                        sDte = strLine.ToString.Split(",")(1)

                        sFrmt = "OTS"

                        sPlnt = strLine.ToString.Split(",")(2)

                    ElseIf (sMTyp = "VDA-HFT") Then

                        nCount = Convert.ToInt64(strLine.Length / 128)

                        For i = 0 To nCount - 1

                            strLine1 = Copy(strLine, i * 128 + 1 - 1, 128)

                            If strLine1.Substring(1 - 1, 3) = "512" Then

                                sPlnt = strLine1.Substring(6 - 1, 3)

                            End If

                        Next

                    ElseIf (sMTyp = "VDA-GNL") Then

                        nCount = Convert.ToInt64(strLine.Length / 128)

                        For i = 0 To nCount - 1

                            strLine1 = Copy(strLine, i * 128 + 1 - 1, 128)

                            If strLine1.Substring(1 - 1, 3) = "552" Then

                                sPlnt = strLine1.Substring(6 - 1, 3)

                            End If

                        Next

                    ElseIf (nLine > 1) AndAlso (sMTyp = "TOYOTA-HFT") Then

                        sDte = strLine.ToString.Split("""")(7)

                        sFrmt = "TYT"

                        sPlnt = strLine.ToString.Split("""")(5)

                    ElseIf (nLine > 1) AndAlso (sMTyp = "TOYOTA-GNL") Then

                        sDte = strLine.ToString.Split("""")(7)

                        sFrmt = "TYT"

                        sPlnt = strLine.ToString.Split("""")(5)

                    End If

                    If (sMTyp = "DELFOR") AndAlso (sVer = "D") AndAlso (sRel = "96A") Then

                        If (sTip = "SCC") Then

                            Exit Do

                        End If

                    ElseIf sPlnt <> "" Then

                        Exit Do

                    End If

                    nLine = nLine + 1

                    strLine = objStreamReader.ReadLine

                Loop

                objStreamReader.Close()

                If (sMTyp = "DELFOR") AndAlso (sVer = "D") AndAlso (sRel = "97A") Then
                    sMesajTipi = "DELFOR"
                ElseIf (sMTyp = "DELINS") AndAlso (sVer = "3") Then
                    sMesajTipi = "DELINS"
                ElseIf (sMTyp = "DELFOR") AndAlso (sVer = "D") AndAlso (sRel = "96A") Then
                    sMesajTipi = "DELFOR"
                ElseIf (sMTyp = "DELFOR") AndAlso (sVer = "1") AndAlso (sRel = "921") Then
                    sMesajTipi = "DELFOR"
                ElseIf (sMTyp = "DELJIT") AndAlso (sVer = "D") AndAlso (sRel = "98B") Then
                    sMesajTipi = "DELJIT"
                ElseIf (sMTyp = "DELJIT") AndAlso (sVer = "D") AndAlso (sRel = "96A") Then
                    sMesajTipi = "DELJIT"
                ElseIf (sMTyp = "DELJIT") AndAlso (sVer = "D") AndAlso (sRel = "97A") AndAlso (sMCRef = "SH") Then
                    sMesajTipi = "DELJIT"
                ElseIf (sMTyp = "DELJIT") AndAlso (sVer = "D") AndAlso (sRel = "97A") AndAlso (sMCRef = "PUS") Then
                    sMesajTipi = "DELJIT-PUS"
                ElseIf sMTyp = "CSV-HAFTALIK-OTS" Then
                    sMesajTipi = "CSV-HAFTALIK-OTS"
                ElseIf sMTyp = "CSV-GUNLUK-OTS" Then
                    sMesajTipi = "CSV-GUNLUK-OTS"
                ElseIf sMTyp = "VDA-GNL" Then
                    sMesajTipi = "VDA-GNL"
                ElseIf sMTyp = "VDA-HFT" Then
                    sMesajTipi = "VDA-HFT"
                ElseIf sMTyp = "TOYOTA-HFT" Then
                    sMesajTipi = "TOYOTA-HFT"
                ElseIf sMTyp = "TOYOTA-GNL" Then
                    sMesajTipi = "TOYOTA-GNL"
                Else
                    sMesajTipi = ""
                End If

                table.Rows.Add(New Object() {file.Split("\")(file.Split("\").Length - 1) _
                                            , sDosyaTarih, sDosyaSaat, ConvertEDIDate(sFrmt, sDte, "BA"), sMesajTipi, sODID, sPlnt})

            Catch ex As Exception

                MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        Next

        Me.GridEX1.DataSource = ds

        Me.GridEX1.DataMember = "Dosya"

        Me.GridEX1.AutoSizeColumns()
    End Sub

    Private Sub frmDesAdv_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sQuery As String

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        Dim ds As DataSet

        sQuery = " Select EDIDIR From EdiPrm"

        ds = db.RunSql(sQuery, "EdiPrm")

        If Not ds Is Nothing Then

            If ds.Tables("EdiPrm").Rows.Count > 0 Then

                txtPath.Text = Trim(ds.Tables("EdiPrm").Rows(0).Item("EDIDIR").ToString)
                'txtPath.Text = "C:\Edi"

                If Not Directory.Exists(txtPath.Text) Then

                    txtPath.Text = "C:\Edi\test"

                End If

            End If

        Else

            txtPath.Text = "c:\Edi\test"

        End If

        DosyaListele(txtPath.Text.ToString)
    End Sub

    Private Sub RBoschMesajAlimi(ByVal sFileName As String)
        Dim sSndrOdetteId As String = ""
        Dim sRcvrOdetteId As String = ""
        Dim sMsgType As String = ""
        Dim sMsgVersion As String = ""
        Dim sMsgRelease As String = ""
        Dim sMsgNum As String = ""
        Dim sMsgDate As String = ""
        Dim sMsgDateFrmt As String = ""
        Dim sDunsId As String = ""
        Dim sPlantId As String = ""
        Dim sItemNum As String = ""
        Dim sContratNum As String = ""
        Dim sCumRef As String = ""
        Dim sCumQty As String = ""
        Dim sCumDate As String = ""
        Dim sDelType As String = ""
        Dim sDelQty As String = ""
        Dim sDateType As String = ""
        Dim sDelStrDate As String = ""
        Dim sDelEndDate As String = ""
        Dim sDelDate As String = ""
        Dim sMsgStat As String = ""
        Dim strLine As String = ""
        Dim myStrArr() As String
        Dim myStrArrTirnak() As String
        Dim sQuery As String = ""

        Dim bEkleEdiWrk As Boolean

        Dim objStreamReader As StreamReader

        Dim iSayac, i As Integer

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        checkedRows = Me.GridEX1.GetCheckedRows()

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        bEkleEdiWrk = False

        sDelDate = ""
        sDelEndDate = ""
        sDelStrDate = ""
        sSndrOdetteId = ""
        sRcvrOdetteId = ""
        sMsgNum = ""
        sMsgDate = ""
        sDunsId = ""
        sPlantId = ""
        sItemNum = ""
        sCumQty = ""
        sCumRef = ""
        sCumDate = ""
        sDelType = ""
        sDelQty = ""
        sDateType = ""

        sDefGateID = sLookup("DEFGATE", "EDIPRM", " SIRKET='" & My.Settings.Company.ToString & "'")

        sQuery = " Delete From EdiWrk "

        db.RunSql(sQuery, True)

        Try

            objStreamReader = New StreamReader(sFileName)

            strLine = objStreamReader.ReadLine

            Do While Not strLine Is Nothing

                myStrArrTirnak = strLine.ToString.Split("'")

                For i = 0 To myStrArrTirnak.Length - 1

                    myStrArr = myStrArrTirnak(i).ToString.Split("+")

                    If myStrArr(0).ToString = "UNB" Then

                        iSayac = 0

                        sSndrOdetteId = myStrArr(2).ToString

                        sRcvrOdetteId = myStrArr(3).ToString.Split(":")(0)

                        sMsgDate = "20" & myStrArr(4).ToString.Split(":")(0)

                    End If

                    If myStrArr(0).ToString = "UNH" Then

                        iSayac = 1

                        sMsgType = myStrArr(2).ToString.Split(":")(0)

                        sMsgVersion = myStrArr(2).ToString.Split(":")(1)

                        sMsgRelease = myStrArr(2).ToString.Split(":")(2)

                    End If

                    If myStrArr(0).ToString = "BGM" Then

                        If myStrArr(1).ToString = "241" Then

                            sMsgNum = Microsoft.VisualBasic.Right(Trim(myStrArr(2).ToString), 5)

                        End If

                    End If

                    If myStrArr(0).ToString = "DTM" Then

                        If myStrArr(1).ToString.Split(":")(0) = "137" Then

                            sMsgDate = myStrArr(1).ToString.Split(":")(1)

                            sMsgDateFrmt = myStrArr(1).ToString.Split(":")(2)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "50" Then

                            sCumDate = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "2" Then

                            sDelDate = myStrArr(1).ToString.Split(":")(1)

                            sDelEndDate = myStrArr(1).ToString.Split(":")(1)

                            sDelStrDate = myStrArr(1).ToString.Split(":")(1)

                        End If

                    End If

                    If myStrArr(0).ToString = "RFF" Then

                        If myStrArr(1).ToString.Split(":")(0) = "ALM" Then

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "AAK" Then

                            sCumRef = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "CT" Then

                            sContratNum = myStrArr(1).ToString.Split(":")(1)

                            bEkleEdiWrk = True

                        End If

                    End If

                    If myStrArr(0).ToString = "NAD" Then

                        If myStrArr(1).ToString = "SE" Then

                            sDunsId = myStrArr(2).ToString.Split(":")(0)

                        End If

                        If myStrArr(1).ToString = "DP" Then

                            sPlantId = myStrArr(2).ToString.Split(":")(0)

                        End If

                    End If

                    If myStrArr(0).ToString = "QTY" Then

                        If myStrArr(1).ToString.Split(":")(0) = "70" Then

                            sCumQty = myStrArr(1).ToString.Split(":")(1)

                        End If

                        If myStrArr(1).ToString.Split(":")(0) = "131" Then

                            sDelQty = myStrArr(1).ToString.Split(":")(1)

                        End If

                    End If

                    If myStrArr(0).ToString = "LIN" Then

                        sItemNum = myStrArr(3).ToString.Split(":")(0)

                    End If

                    If myStrArr(0).ToString = "PIA" Then

                    End If

                    If myStrArr(0).ToString = "SCC" Then

                        sDelType = myStrArr(1).ToString

                        If bEkleEdiWrk = True Then

                            Try

                                sMsgStat = "H"

                                If sCumDate = "" Then sCumDate = "0"
                                If sCumQty = "" Then sCumQty = "0"
                                If sDelStrDate = "" Then sDelStrDate = "0"
                                If sDelEndDate = "" Then sDelEndDate = "0"

                                sQuery = " INSERT INTO EDIWRK" & _
                                        " (SENDER, RECEIVER, MSGNUM, MSGDTM, " & _
                                        " DUNSID,  PLANTID, CUSITM, GATEID, CONTNO, PUSNO, " & _
                                        " CUMQTY, CUMREF, CUMDTE, DELTYP, DELQTY, DTETYP, DTE_STR, " & _
                                        " DTE_END, DELDTE, MSGSTAT, CRDDTE, CRDTIM, " & _
                                        " CRDUSR)" & _
                                        " VALUES (" & _
                                        "'" & sSndrOdetteId & "','" & sRcvrOdetteId & "'" & _
                                        ",'" & sMsgNum & "'," & sMsgDate & _
                                        ",'" & sDunsId & "' ,'" & sPlantId & "'" & _
                                        ",'" & sItemNum & "','" & sDefGateID & "','" & sContratNum & "'" & _
                                        ",'" & sContratNum & "'," & sCumQty & ",'" & sCumRef & "'" & _
                                        "," & sCumDate & ",'" & sDelType & "'," & sDelQty & ",'" & sDateType & "'" & _
                                        "," & sDelStrDate & "," & sDelEndDate & "," & sDelDate & ",'" & sMsgStat & "'" & _
                                        "," & Now.Date.ToString("yyyyMMdd") & "," & Now.ToString("hhmss") & _
                                        ",'" & KullaniciAdi & "')"

                                db.RunSql(sQuery, True)

                                bEkleEdiWrk = False

                            Catch ex As Exception

                            End Try

                        End If

                    End If

                    If myStrArr(0).ToString = "UNT" Then

                        If bEkleEdiWrk = True Then

                            Try

                                sMsgStat = "H"

                                If sCumDate = "" Then sCumDate = "0"
                                If sCumQty = "" Then sCumQty = "0"
                                If sDelStrDate = "" Then sDelStrDate = "0"
                                If sDelEndDate = "" Then sDelEndDate = "0"

                                sQuery = " INSERT INTO EDIWRK" & _
                                        " (SENDER, RECEIVER, MSGNUM, MSGDTM, " & _
                                        " DUNSID,  PLANTID, CUSITM, GATEID, CONTNO, PUSNO, " & _
                                        " CUMQTY, CUMREF, CUMDTE, DELTYP, DELQTY, DTETYP, DTE_STR, " & _
                                        " DTE_END, DELDTE, MSGSTAT, CRDDTE, CRDTIM, " & _
                                        " CRDUSR)" & _
                                        " VALUES (" & _
                                        "'" & sSndrOdetteId & "','" & sRcvrOdetteId & "'" & _
                                        ",'" & sMsgNum & "'," & sMsgDate & _
                                        ",'" & sDunsId & "' ,'" & sPlantId & "'" & _
                                        ",'" & sItemNum & "','" & sDefGateID & "','" & sContratNum & "'" & _
                                        ",'" & sContratNum & "'," & sCumQty & ",'" & sCumRef & "'" & _
                                        "," & sCumDate & ",'" & sDelType & "'," & sDelQty & ",'" & sDateType & "'" & _
                                        "," & sDelStrDate & "," & sDelEndDate & "," & sDelDate & ",'" & sMsgStat & "'" & _
                                        "," & Now.Date.ToString("yyyyMMdd") & "," & Now.ToString("hhmss") & _
                                        ",'" & KullaniciAdi & "')"

                                db.RunSql(sQuery, True)

                                bEkleEdiWrk = False

                            Catch ex As Exception

                            End Try

                        End If

                    End If

                    If myStrArr(0).ToString = "UNZ" Then

                    End If

                Next i

                strLine = objStreamReader.ReadLine

            Loop

            objStreamReader.Close()

        Catch ex As Exception

        End Try

        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    #End Region 'Methods

End Class