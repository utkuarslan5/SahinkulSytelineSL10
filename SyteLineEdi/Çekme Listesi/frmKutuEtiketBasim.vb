Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Public Class frmKutuEtiketBasim

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString.ToString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim ds As DataSet
    Dim dt As New DataTable
    Dim dtBaslik As New DataTable
    Dim dtPalet As New DataTable
    Dim dtTemp As New DataTable
    Dim nEtkMax As Double
    Dim nKutuMiktari As Double
    Dim Ps As New PrintDialog
    Dim sEtkMax As String
    Dim sLine As String
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Public Sub yazdirmaislemi(ByVal sEtkTip As String)
        Dim sPapTip As String = ""
        Dim sPapName As String = ""
        Dim nTopEtiket As Integer = 0
        Dim nMaxSayac As Integer = 0
        Dim nEtkSayac As Integer = 0
        Dim sKanban As String = ""
        Dim sKontrat As String = ""
        Dim sKisaPlant As String = ""
        Dim sMatHand As String = ""
        Dim nKutuAdet As Double = 0
        Dim nNetWeight As Double = 0
        Dim nBrtWeight As Double = 0
        Dim nQuantityPerPack As Double = 0
        Dim sAGOB As String = ""
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim k As Integer = 0
        Dim sSupplierNo As String = ""
        Dim sPurchaseOrder As String = ""
        Dim dSevkDate As DateTime
        Dim dChangeDate As DateTime
        Dim sSupplier As String = ""
        Dim sSupplierAdres As String = ""
        Dim sLotNo As String = ""
        Dim sText As String = ""
        Try

            If rdbLaser.Checked Then

                If chkKutuEtiketi.Checked Then

                    frmRapor.Parametre.Clear()

                    RaporCagir(sEtkTip & "Box.rpt")

                End If

                If chkPaletEtiketi.Checked Then

                    frmRapor.Parametre.Clear()

                    RaporCagir(sEtkTip & "Pallet.rpt")

                End If

            Else

                If MessageBox.Show("Etiketler Basýlacak. Onaylýyor musunuz?...", "Ekip Mapics", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                    Ps.PrinterSettings = New PrinterSettings()

                    sQuery = " SELECT * " & _
                                    " FROM ANSETIKETDTY " & _
                                    " WHERE (PLTETK=1 OR KUTUETK=1) " & _
                                    " ORDER BY ITNBR,KUTUETK Desc,ETKSERINO"

                    sPapTip = "999999"

                    dtBaslik = dbAccess.RunSql(sQuery)

                    If Not dtBaslik Is Nothing AndAlso dtBaslik.Rows.Count > 0 Then

                        If chkKutuEtiketi.Checked Or chkPaletEtiketi.Checked Then

                            If (Ps.ShowDialog() = DialogResult.Cancel) Then

                                Exit Sub

                            End If

                        End If

                        For i As Integer = 0 To dtBaslik.Rows.Count - 1

                            With dtBaslik.Rows(i)
                                ' ***********************Kutu Etiketi Basýlacak********************
                                If chkKutuEtiketi.Checked AndAlso .Item("KUTUETK").ToString = "1" Then

                                    If sPapTip <> .Item("KPAPTIP").ToString Then

                                        sPapTip = .Item("KPAPTIP").ToString

                                        'sPapName = sLookup("PAPTAN", "ETKPAP", " ETKPAP=" & sTirnakEkle(sPapTip))

                                        MessageBox.Show((sPapName & " - ") + "Etiket Kaðýdýna Basýlacak.Kontrol Edin...", "Ekip Mapics", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)

                                    End If

                                    Dim sEtiketNo As String = ""

                                    sEtkTip = .Item("ETKTIP").ToString

                                    sKanban = ""

                                    sMatHand = ""

                                    sQuery = " SELECT KANBAN,USER1,KNTRT, KPlant " & _
                                                                  " FROM KONTRTPF" & _
                                                                  " WHERE CUST=" & sTirnakEkle(.Item("CUST").ToString) & _
                                                                  " AND SHIPTO=" & sTirnakEkle(.Item("PLANTID").ToString) & _
                                                                  " AND KAPI=" & sTirnakEkle(.Item("GATEID").ToString) & _
                                                                  " AND BZMITM=" & sTirnakEkle(.Item("ITNBR").ToString)

                                    dtTemp = db.RunSql(sQuery)

                                    GetRowInfo(sKanban, dtTemp, 0, "KANBAN")

                                    GetRowInfo(sMatHand, dtTemp, 0, "USER1")

                                    GetRowInfo(sKontrat, dtTemp, 0, "KNTRT")

                                    GetRowInfo(sKisaPlant, dtTemp, 0, "KPlant")

                                    nQuantityPerPack = .Item("KMIK").ToString

                                    nNetWeight = nQuantityPerPack * .Item("IBRMAGR").ToString

                                    nBrtWeight = nNetWeight + .Item("KBRMAGR").ToString

                                    nKutuAdet = 1

                                    'If .Item("KAGOB").ToString <> sAGOB Then

                                    '    nNetWeight = 0 'BirimDonustur(sAGOB, .Item("KAGOB").ToString, nNetWeight)

                                    '    nBrtWeight = 0 'BirimDonustur(sAGOB, .Item("KAGOB").ToString, nBrtWeight)

                                    'End If

                                    Dim sPath As String

                                    sPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & _
                                            "Etiket\EDIEtiket_" & sEtkTip & ".ini"

                                    Dim fs As FileStream = New FileStream(sPath, FileMode.Open, FileAccess.Read)

                                    Dim Okuma As StreamReader = New StreamReader(fs)

                                    'Dosya sonu olup olmadýðý kontrolünü Peek() methodunu çaðýrarak yaptýk. Peek metodu sonraki karakterin ASCII kodunu dönderir. Eðer hiç karakter yoksa -1 deðerini dönderir.

                                    sSupplierNo = "PEXE0231"

                                    sPurchaseOrder = ""

                                    While Okuma.Peek <> -1

                                        sLine = Okuma.ReadLine()

                                        If sLine.Contains(")KutuLine(") And Not sLine.Contains("Count") Then

                                            If .Item("REVTAR").ToString <> "0" AndAlso .Item("REVTAR").ToString <> "" Then

                                                dChangeDate = DateConvertVb(Convert.ToString(.Item("REVTAR").ToString))

                                            End If

                                            dSevkDate = DateConvertVb(Convert.ToString(.Item("SEVKTAR").ToString))

                                            sLine = Replace(sLine, "[RECEIVER1]", Trim(.Item("ETKADR1").ToString))

                                            sLine = Replace(sLine, "[RECEIVER2]", Trim(.Item("ETKADR2").ToString))

                                            If (.Item("PUSNO").ToString <> "") AndAlso (.Item("PUSNO").ToString <> "KESIN") Then

                                                sLine = Replace(sLine, "[LIEFERSCHEIN]", Trim(.Item("PUSNO").ToString))

                                            Else

                                                sLine = Replace(sLine, "[LIEFERSCHEIN]", Trim(Convert.ToString(.Item("PICKNO").ToString)))

                                            End If

                                            sLine = Replace(sLine, "[CUSTITMKOD]", Trim(.Item("MURNKOD").ToString))

                                            If (.Item("PUSNO").ToString <> "") AndAlso (.Item("PUSNO").ToString <> "KESIN") Then

                                                sLine = Replace(sLine, "[CUSTITMDSC]", Mid((Trim(.Item("MURNTNM").ToString) + " " + .Item("ITNBR").ToString + " " + Convert.ToString(.Item("PICKNO").ToString)), 1, 24))

                                            Else

                                                sLine = Replace(sLine, "[CUSTITMDSC]", Mid((.Item("MURNTNM").ToString + " " + .Item("ITNBR").ToString), 1, 24))

                                            End If

                                            sLine = Replace(sLine, "[PICKNO]", Trim(.Item("PICKNO").ToString))

                                            sLine = Replace(sLine, "[CUSTITMDSC1]", Mid(Trim(.Item("MURNTNM").ToString), 1, 24))

                                            sLine = Replace(sLine, "[ITEMKOD]", Trim(.Item("ITNBR").ToString))

                                            sLine = Replace(sLine, "[MUSTKUTUKODU]", Trim(.Item("MKKOD").ToString))

                                            sLine = Replace(sLine, "[MUSTPALETKODU]", Trim(.Item("MPKOD").ToString))

                                            If sEtkTip = "OPEL2" Then

                                                If sKisaPlant <> "" Then

                                                    sLine = Replace(sLine, "[SHIPTO]", Trim(sKisaPlant))

                                                Else

                                                    sLine = Replace(sLine, "[SHIPTO]", Trim(.Item("PLANTID").ToString))

                                                End If

                                                sLine = Replace(sLine, "[KNTRT]", Trim(sKontrat))

                                            Else

                                                sLine = Replace(sLine, "[SHIPTO]", Trim(.Item("PLANTID").ToString))

                                            End If

                                            sLine = Replace(sLine, "[QUANTITYPERPACK]", Convert.ToString(nQuantityPerPack))

                                            sLine = Replace(sLine, "[BOXES]", Convert.ToString(nKutuAdet))

                                            sLine = Replace(sLine, "[UNITMEASURE]", "PCS")

                                            If sEtkTip = "OPEL2" Then

                                                sLine = Replace(sLine, "[NETWEIGHT]", Convert.ToString(CInt(nNetWeight)))

                                                sLine = Replace(sLine, "[BRTWEIGHT]", Convert.ToString(CInt(nBrtWeight)))

                                            Else

                                                sLine = Replace(sLine, "[NETWEIGHT]", Convert.ToString(nNetWeight))

                                                sLine = Replace(sLine, "[BRTWEIGHT]", Convert.ToString(nBrtWeight))

                                            End If

                                            sLine = Replace(sLine, "[SUPPLIERCODE]", Trim(.Item("DUNSNO").ToString))

                                            sLine = Replace(sLine, "[PURCHASEORDER]", sPurchaseOrder)

                                            sEtiketNo = IIf(sEtkTip = "OPEL2", .Item("ETKSERINO").ToString.Substring(1, .Item("ETKSERINO").ToString.Length - 1), .Item("ETKSERINO").ToString)

                                            sLine = Replace(sLine, "[ETIKETSERINO]", Convert.ToString(Trim(sEtiketNo)))

                                            sLine = Replace(sLine, "[GATE]", Trim(.Item("GATEID").ToString))

                                            sLine = Replace(sLine, "[LOTNO]", sLotNo)

                                            If sEtkTip = "RenoK" Then

                                                '.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US"))

                                                sLine = Replace(sLine, "[DATE]", DateTime.Today.ToString("yyMMdd"))

                                                sLine = Replace(sLine, "[DATEFRM1]", DateTime.Today.ToString("yyMMdd"))

                                                sLine = Replace(sLine, "[ENGCHANGEDATE]", dChangeDate.ToString("yyMMdd"))

                                                sLine = Replace(sLine, "[SEVKDATE]", dSevkDate.ToString("yyMMdd"))

                                            Else

                                                If sEtkTip = "OPEL2" Then

                                                    sLine = Replace(sLine, "[DATE]", DateTime.Today.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                    sLine = Replace(sLine, "[DATEFRM1]", DateTime.Today.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                    sLine = Replace(sLine, "[ENGCHANGEDATE]", dChangeDate.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                    sLine = Replace(sLine, "[SEVKDATE]", dSevkDate.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                Else

                                                    sLine = Replace(sLine, "[DATE]", DateTime.Today.ToString("dd.MM.yy"))

                                                    sLine = Replace(sLine, "[DATEFRM1]", DateTime.Today.ToString("dd.MM.yy"))

                                                    sLine = Replace(sLine, "[ENGCHANGEDATE]", dChangeDate.ToString("dd.MM.yy"))

                                                    sLine = Replace(sLine, "[SEVKDATE]", dSevkDate.ToString("dd.MM.yy"))

                                                End If

                                            End If

                                            sLine = Replace(sLine, "[ENGCHANGE]", Trim(.Item("REVNO").ToString))

                                            sLine = Replace(sLine, "[SUPPLIER]", sSupplier)

                                            sLine = Replace(sLine, "[SUPPLIERNO]", sSupplierNo)

                                            sLine = Replace(sLine, "[SUPPLIERADR]", sSupplierAdres)

                                            sLine = Replace(sLine, "[KANBANNO]", sKanban)

                                            sLine = Replace(sLine, "[BATCHNO]", "")

                                            sLine = Replace(sLine, "[HANDLINGCODE]", sMatHand.Trim)

                                            sLine = Replace(sLine, "Ð", "G")

                                            sLine = Replace(sLine, "Ü", "U")

                                            sLine = Replace(sLine, "Þ", "S")

                                            sLine = Replace(sLine, "Ý", "I")

                                            sLine = Replace(sLine, "Ö", "O")

                                            sLine = Replace(sLine, "Ç", "C")

                                            sLine = Replace(sLine, "ð", "g")

                                            sLine = Replace(sLine, "ü", "u")

                                            sLine = Replace(sLine, "þ", "s")

                                            sLine = Replace(sLine, "ý", "i")

                                            sLine = Replace(sLine, "ö", "o")

                                            sLine = Replace(sLine, "ç", "c")

                                            sText &= sLine

                                        End If

                                    End While

                                    Okuma.Close()

                                    fs.Close()

                                    sText = Replace(sText, "Ð", "G")

                                    sText = Replace(sText, "Ü", "UE")

                                    sText = Replace(sText, "Þ", "S")

                                    sText = Replace(sText, "Ý", "I")

                                    sText = Replace(sText, "Ö", "OE")

                                    sText = Replace(sText, "Ç", "C")

                                    sText = Replace(sText, "ð", "g")

                                    sText = Replace(sText, "ü", "ue")

                                    sText = Replace(sText, "þ", "s")

                                    sText = Replace(sText, "ý", "i")

                                    sText = Replace(sText, "ö", "oe")

                                    sText = Replace(sText, "ç", "c")

                                    'Dim objWriter As System.IO.StreamWriter

                                    'objWriter = New System.IO.StreamWriter(txtCeklistNo.Text & Convert.ToString(.Item("ETKSERINO").ToString) & ".txt")

                                    'objWriter.WriteLine(sText)

                                    'objWriter.Close()

                                    RawPrinterHelper.SendStringToPrinter(Ps.PrinterSettings.PrinterName, sText)

                                    sText = ""

                                End If
                                '' ***********************Palet Etiketi Basýlacak********************
                                If chkPaletEtiketi.Checked AndAlso .Item("PLTETK").ToString = "1" Then

                                    If sPapTip <> .Item("PPAPTIP").ToString Then

                                        sPapTip = .Item("PPAPTIP").ToString

                                        'sPapName = sLookup("PAPTAN", "ETKPAP", " ETKPAP=" & sTirnakEkle(sPapTip))

                                        MessageBox.Show((sPapName & " - ") + "Etiket Kaðýdýna Basýlacak.Kontrol Edin...", "Ekip Mapics", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)

                                    End If

                                    Dim sEtiketNo As String = ""

                                    sEtkTip = .Item("ETKTIP").ToString

                                    sQuery = " SELECT KANBAN, USER1, KNTRT, KPlant " & _
                                                    " FROM KONTRTPF" & _
                                                    " WHERE CUST=" & sTirnakEkle(.Item("CUST").ToString) & _
                                                        " AND SHIPTO=" & sTirnakEkle(.Item("PLANTID").ToString) & _
                                                        " AND KAPI=" & sTirnakEkle(.Item("GATEID").ToString) & _
                                                        " AND BZMITM=" & sTirnakEkle(.Item("ITNBR").ToString)

                                    dtTemp = db.RunSql(sQuery)

                                    GetRowInfo(sKanban, dtTemp, 0, "KANBAN")

                                    GetRowInfo(sMatHand, dtTemp, 0, "USER1")

                                    GetRowInfo(sKontrat, dtTemp, 0, "KNTRT")

                                    GetRowInfo(sKisaPlant, dtTemp, 0, "KPlant")

                                    nKutuAdet = .Item("KSAY").ToString

                                    nQuantityPerPack = nKutuAdet * .Item("KMIK").ToString

                                    nNetWeight = nQuantityPerPack * .Item("IBRMAGR").ToString

                                    nBrtWeight = nNetWeight + .Item("PBRMAGR").ToString + (.Item("KBRMAGR").ToString * nKutuAdet) + .Item("SPBRMAGR").ToString + .Item("KPBRMAGR").ToString

                                    Dim sPath As String

                                    sPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & _
                                            "Etiket\EDIEtiket_" & sEtkTip & ".ini"

                                    Dim fs As FileStream = New FileStream(sPath, FileMode.Open, FileAccess.Read)

                                    Dim Okuma As StreamReader = New StreamReader(fs)

                                    'Dosya sonu olup olmadýðý kontrolünü Peek() methodunu çaðýrarak yaptýk. Peek metodu sonraki karakterin ASCII kodunu dönderir. Eðer hiç karakter yoksa -1 deðerini dönderir.

                                    While Okuma.Peek <> -1

                                        sLine = Okuma.ReadLine()

                                        sSupplierNo = "PEXE0231"

                                        sPurchaseOrder = ""

                                        If .Item("REVTAR").ToString <> "0" AndAlso .Item("REVTAR").ToString <> "" Then

                                            dChangeDate = DateConvertVb(Convert.ToString(.Item("REVTAR").ToString))

                                        End If

                                        dSevkDate = DateConvertVb(Convert.ToString(.Item("SEVKTAR").ToString))

                                        If sLine.Contains(")PaletLine(") And Not sLine.Contains("Count") Then

                                            sLine = Replace(sLine, "[RECEIVER1]", Trim(.Item("ETKADR1").ToString))

                                            sLine = Replace(sLine, "[RECEIVER2]", Trim(.Item("ETKADR2").ToString))

                                            If (.Item("PUSNO").ToString <> "") AndAlso (.Item("PUSNO").ToString <> "KESIN") Then

                                                sLine = Replace(sLine, "[LIEFERSCHEIN]", Trim(.Item("PUSNO").ToString))

                                            Else

                                                sLine = Replace(sLine, "[LIEFERSCHEIN]", Convert.ToString(Trim(.Item("PICKNO").ToString)))

                                            End If

                                            sLine = Replace(sLine, "[CUSTITMKOD]", Trim(.Item("MURNKOD").ToString))

                                            If (.Item("PUSNO").ToString <> "") AndAlso (.Item("PUSNO").ToString <> "KESIN") Then

                                                sLine = Replace(sLine, "[CUSTITMDSC]", Trim(.Item("MURNTNM").ToString) & " " & Trim(.Item("ITNBR").ToString) & " " & Convert.ToString(Trim(.Item("PICKNO").ToString)))

                                            Else

                                                sLine = Replace(sLine, "[CUSTITMDSC]", Trim(.Item("MURNTNM").ToString) & " " & Trim(.Item("ITNBR").ToString))

                                            End If

                                            sLine = Replace(sLine, "[PICKNO]", Trim(.Item("PICKNO").ToString))

                                            sLine = Replace(sLine, "[CUSTITMDSC1]", Trim(.Item("MURNTNM").ToString))

                                            sLine = Replace(sLine, "[ITEMKOD]", Trim(.Item("ITNBR").ToString))

                                            sLine = Replace(sLine, "[MUSTKUTUKODU]", Trim(.Item("MKKOD").ToString))

                                            sLine = Replace(sLine, "[MUSTPALETKODU]", Trim(.Item("MPKOD").ToString))

                                            'sLine = Replace(sLine, "[SHIPTO]", Trim(.Item("PLANTID").ToString))

                                            If sEtkTip = "OPEL2" Then

                                                If sKisaPlant <> "" Then

                                                    sLine = Replace(sLine, "[SHIPTO]", Trim(sKisaPlant))

                                                Else

                                                    sLine = Replace(sLine, "[SHIPTO]", Trim(.Item("PLANTID").ToString))

                                                End If

                                                sLine = Replace(sLine, "[KNTRT]", Trim(sKontrat))

                                            Else

                                                sLine = Replace(sLine, "[SHIPTO]", Trim(.Item("PLANTID").ToString))

                                            End If

                                            sLine = Replace(sLine, "[QUANTITYPERPACK]", Convert.ToString(nQuantityPerPack))

                                            sLine = Replace(sLine, "[BOXES]", Convert.ToString(nKutuAdet))

                                            sLine = Replace(sLine, "[UNITMEASURE]", "PCS")

                                            If sEtkTip = "OPEL2" Then

                                                sLine = Replace(sLine, "[NETWEIGHT]", Convert.ToString(CInt(nNetWeight)))

                                                sLine = Replace(sLine, "[BRTWEIGHT]", Convert.ToString(CInt(nBrtWeight)))

                                            Else

                                                sLine = Replace(sLine, "[NETWEIGHT]", Convert.ToString(nNetWeight))

                                                sLine = Replace(sLine, "[BRTWEIGHT]", Convert.ToString(nBrtWeight))

                                            End If

                                            sLine = Replace(sLine, "[SUPPLIERCODE]", Trim(.Item("DUNSNO").ToString))

                                            sLine = Replace(sLine, "[PURCHASEORDER]", sPurchaseOrder)

                                            sEtiketNo = IIf(sEtkTip = "OPEL2", .Item("ETKSERINO").ToString.Substring(1, .Item("ETKSERINO").ToString.Length - 1), .Item("ETKSERINO").ToString)

                                            sLine = Replace(sLine, "[ETIKETSERINO]", Convert.ToString(Trim(sEtiketNo)))

                                            sLine = Replace(sLine, "[GATE]", Trim(.Item("GATEID").ToString))

                                            sLine = Replace(sLine, "[LOTNO]", sLotNo)

                                            If sEtkTip = "RenoP" Then

                                                sLine = Replace(sLine, "[DATE]", DateTime.Today.ToString("yyMMdd"))

                                                sLine = Replace(sLine, "[DATEFRM1]", DateTime.Today.ToString("yyMMdd"))

                                                sLine = Replace(sLine, "[ENGCHANGEDATE]", dChangeDate.ToString("yyMMdd"))

                                                sLine = Replace(sLine, "[SEVKDATE]", FormatDateTime("yyMMdd", dSevkDate.ToString("yyMMdd")))

                                            Else

                                                If sEtkTip = "OPEL2" Then

                                                    sLine = Replace(sLine, "[SEVKDATE]", dSevkDate.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                    sLine = Replace(sLine, "[DATE]", DateTime.Today.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                    sLine = Replace(sLine, "[DATEFRM1]", DateTime.Today.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                    sLine = Replace(sLine, "[ENGCHANGEDATE]", dChangeDate.ToString("ddMMMyyyy", CultureInfo.CreateSpecificCulture("en-US")))

                                                Else

                                                    sLine = Replace(sLine, "[SEVKDATE]", dSevkDate.ToString("dd.MM.yy"))

                                                    sLine = Replace(sLine, "[DATE]", DateTime.Today.ToString("dd.MM.yy"))

                                                    sLine = Replace(sLine, "[DATEFRM1]", DateTime.Today.ToString("dd.MM.yy"))

                                                    sLine = Replace(sLine, "[ENGCHANGEDATE]", dChangeDate.ToString("dd.MM.yy"))

                                                End If

                                                'sLine = Replace(sLine, "[SEVKDATE]", dSevkDate.ToString("dd.MM.yy"))

                                            End If

                                            sLine = Replace(sLine, "[ENGCHANGE]", Trim(.Item("REVNO").ToString))

                                            sLine = Replace(sLine, "[SUPPLIER]", sSupplier)

                                            sLine = Replace(sLine, "[SUPPLIERNO]", sSupplierNo)

                                            sLine = Replace(sLine, "[SUPPLIERADR]", sSupplierAdres)

                                            sLine = Replace(sLine, "[KANBANNO]", sKanban)

                                            sLine = Replace(sLine, "[BATCHNO]", "")

                                            sLine = Replace(sLine, "[HANDLINGCODE]", sMatHand)

                                            sLine = Replace(sLine, "Ð", "G")

                                            sLine = Replace(sLine, "Ü", "U")

                                            sLine = Replace(sLine, "Þ", "S")

                                            sLine = Replace(sLine, "Ý", "I")

                                            sLine = Replace(sLine, "Ö", "O")

                                            sLine = Replace(sLine, "Ç", "C")

                                            sLine = Replace(sLine, "ð", "g")

                                            sLine = Replace(sLine, "ü", "u")

                                            sLine = Replace(sLine, "þ", "s")

                                            sLine = Replace(sLine, "ý", "i")

                                            sLine = Replace(sLine, "ö", "o")

                                            sLine = Replace(sLine, "ç", "c")

                                            sText &= sLine

                                        End If

                                    End While

                                    Okuma.Close()

                                    fs.Close()

                                    sText = Replace(sText, "Ð", "G")

                                    sText = Replace(sText, "Ü", "UE")

                                    sText = Replace(sText, "Þ", "S")

                                    sText = Replace(sText, "Ý", "I")

                                    sText = Replace(sText, "Ö", "OE")

                                    sText = Replace(sText, "Ç", "C")

                                    sText = Replace(sText, "ð", "g")

                                    sText = Replace(sText, "ü", "ue")

                                    sText = Replace(sText, "þ", "s")

                                    sText = Replace(sText, "ý", "i")

                                    sText = Replace(sText, "ö", "oe")

                                    sText = Replace(sText, "ç", "c")

                                    'Dim objWriter As System.IO.StreamWriter

                                    'objWriter = New System.IO.StreamWriter(txtCeklistNo.Text & Convert.ToString(.Item("ETKSERINO").ToString) & ".txt")

                                    'objWriter.WriteLine(sText)

                                    'objWriter.Close()

                                    If Not sText Is Nothing Then

                                        ' Dosyayý yazýcýya yazdýr.
                                        RawPrinterHelper.SendStringToPrinter(Ps.PrinterSettings.PrinterName, sText)

                                    End If

                                    sText = ""

                                End If

                            End With

                        Next i

                    End If

                End If

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
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
            sHandlingKodu, sSupplierAdres, sKanbanNo, sEtiketNo As String

        Dim PSay As Double = 0

        Dim HSay As Double = 0

        Dim SSay As Double = 0

        Dim KSay As Double = 0

        Dim PKmik As Double = 0

        Dim PaletKSay As Double = 0

        Try

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            sEtkAdres1 = ""
            sEtkAdres2 = ""
            sEtkTip = ""
            sKutuKodu = ""
            sMusteriKutuKodu = ""
            sPaletKodu = ""
            sMusteriPaletKodu = ""
            sRevizyonNo = ""
            sRevizyonTarihi = ""
            sDunsNo = ""
            sUserF2 = ""
            sHandlingKodu = ""
            sSupplierAdres = ""
            sKanbanNo = ""
            sEtiketNo = ""

            dbAccess.RunSql("Delete From ANSETIKETDTY", True)

            If rdbSevkiyat.Checked Then

                If MessageBox.Show("Oluþturulan Etiketler Silinecek Onaylýyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                    sQuery = " Delete" & _
                                " FROM  ETIKETDTY " & _
                                " Where PICKNO=" & txtCeklistNo.Text

                    db.RunSql(sQuery)

                Else

                    Exit Sub

                End If

            End If

            sQuery = " SELECT ETKSERINO " & _
                    " FROM  ETIKETDTY " & _
                    " Where PICKNO=" & txtCeklistNo.Text

            dsYeni = db.RunSql(sQuery, "ETIKETDTY")

            If Not dsYeni Is Nothing Then

                If dsYeni.Tables("ETIKETDTY").Rows.Count > 0 Then

                    If MessageBox.Show(txtCeklistNo.Text & " Nolu Picklist in Etiketleri Mevcut Yeniden Yazdýrýlsýn mý?", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then

                        Exit Sub

                    End If

                    sQuery = " SELECT * " & _
                            " FROM  ETIKETDTY " & _
                            " Where PICKNO=" & txtCeklistNo.Text

                    dsTekrar = db.RunSql(sQuery, "ETIKETDTY")

                    If Not dsTekrar Is Nothing Then

                        If dsTekrar.Tables("ETIKETDTY").Rows.Count > 0 Then

                            For i As Integer = 0 To dsTekrar.Tables("ETIKETDTY").Rows.Count - 1

                                With dsTekrar.Tables("ETIKETDTY").Rows(i)

                                    sQuery = "exec TR_ETKTARIHsp " & .Item("ETKSERINO").ToString
                                    dt = db.RunSql(sQuery)

                                    sQuery = "INSERT INTO ANSETIKETDTY" & _
                                              " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                                              " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                                              " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                                              " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                                              " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                                              " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                                              " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                                              " DANGERCODE, IRSNO, DOCK, HANDLINGCODE, SUPPLIERADR, NETWEIGHT, " & _
                                              " BRTWEIGHT, BOXES, QUANTITYPERPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE,KANBANNO ,TAR1,TAR2,TAR3) " & _
                                          " VALUES(" & .Item("PICKNO").ToString & "," & .Item("PICKDT").ToString & ",'" & (.Item("CUST").ToString) & "'," & _
                                               "'" & .Item("ETKADR1").ToString & "','" & .Item("ETKADR2").ToString & "','" & .Item("ETKTIP").ToString & "'," & _
                                               .Item("KUTUETK").ToString & "  , " & .Item("PLTETK").ToString & " , '" & .Item("KPAPTIP").ToString & "'," & _
                                               "'" & .Item("PPAPTIP").ToString & "' , '" & .Item("PLANTID").ToString & "' , '" & .Item("GATEID").ToString & "' , " & _
                                               "'" & .Item("MURNKOD").ToString & "' , '" & .Item("MURNTNM").ToString & "' , '" & .Item("ITNBR").ToString & "' , " & _
                                               .Item("MIKTAR").ToString & ", '" & .Item("KKOD").ToString & "' , '" & .Item("MKKOD").ToString & "' , " & _
                                               .Item("KSAY").ToString & "," & .Item("KMIK").ToString & " , ' " & .Item("PKOD").ToString & "' , " & _
                                               "'" & .Item("MPKOD").ToString & "' , " & .Item("PSAY").ToString & " , " & .Item("PKSIR").ToString & " , " & _
                                               .Item("PKMIK").ToString & "," & .Item("HKSAY").ToString & " , '" & .Item("REVNO").ToString & "' , " & _
                                               "'" & .Item("REVTAR").ToString & "' , '" & .Item("DUNSNO").ToString & "' , " & .Item("IBRMAGR").ToString & " , " & _
                                               .Item("KBRMAGR").ToString & " , " & .Item("PBRMAGR").ToString & " , '" & .Item("KAGOB").ToString & "' , " & _
                                               "'" & .Item("PAGOB").ToString & "' , '" & .Item("IAGOB").ToString & "' , '" & .Item("SPAGOB").ToString & "' , " & _
                                               "'" & .Item("KPAGOB").ToString & "' , " & .Item("SPBRMAGR").ToString & " , " & .Item("KPBRMAGR").ToString & " , " & _
                                               "'" & .Item("PUSNO").ToString & "' , " & .Item("SEVKTAR").ToString & " , '" & .Item("USERF2").ToString & "' , " & _
                                               "'" & .Item("DNGRCODE").ToString & "' , '" & .Item("IRSNO").ToString & "' , '" & .Item("DOCK").ToString & "' , " & _
                                               "'" & .Item("HNDCODE").ToString & "' , '" & .Item("SPPLRADR").ToString & "' , " & .Item("NETWEIGHT").ToString & " , " & _
                                               .Item("BRTWEIGHT").ToString & " , " & .Item("BOXES").ToString & " , " & .Item("KMIK").ToString & " , " & _
                                               "'" & .Item("ETKSERINO").ToString & "' , '" & .Item("PLTNO").ToString & "' , '" & .Item("PLTTYPE").ToString & "' , " & _
                                               "'" & .Item("L3P").ToString & "' , '" & .Item("ORDNO").ToString & "' , " & .Item("ORDSEQ").ToString & " , '" & .Item("HOUSE").ToString & "'," & _
                                               "'" & .Item("KANBANNO").ToString & "' , '" & dt.Rows(0)("create_date") & "' , '" & _
                                               dt.Rows(0)("exp_date") & "' , '" & dt.Rows(0)("SEVKTAR") & "')"


                                    sEtkTip = .Item("ETKTIP").ToString

                                    dbAccess.RunSql(sQuery, True)
                                End With

                            Next

                            yazdirmaislemi(sEtkTip)

                            Exit Sub

                        End If

                    End If

                Else

                    Dim row As Janus.Windows.GridEX.GridEXRow

                    For i As Integer = 0 To GridEX1.RowCount - 1

                        row = GridEX1.GetRow(i)

                        Dim Miktar As Double

                        If rdbCekmeListesi.Checked Then

                            Miktar = row.Cells("ADAQQT").Text

                        ElseIf rdbSevkiyat.Checked Then

                            Miktar = row.Cells("SEVKMIK").Text

                        End If

                        If row.Cells("KMIK").Text = "" Then row.Cells("KMIK").Text = 0

                        If CInt(row.Cells("KMIK").Text) <> 0 Then

                            iKutuSayisi = CInt(Miktar) / CInt(row.Cells("KMIK").Text)

                            If CInt(Miktar) - (iKutuSayisi * CInt(row.Cells("KMIK").Text)) > 0 Then

                                iKutuSayisi = iKutuSayisi + 1

                            End If

                        End If

                        sQuery = " Select I.ETKTIP, I.KKOD, I.MKKOD, I.PKOD, I.MPKOD, I.PKSIR, " & _
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
                            " where T.ceklist =" & row.Cells("CEKLIST").Text & _
                            " And T.ADAITX=" & sTirnakEkle(row.Cells("ADAITX").Text)

                        dsITMPACK = db.RunSql(sQuery, "ITMPACK")

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

                            MessageBox.Show(row.Cells("ADAITX").Text & _
                                            " Nolu Malzemenin Etiket Tipi Tanýmsýz" & _
                                            " Etiket Tipini Tanýmlayýp Tekrar Deneyiniz.", "Ekip Mapics")

                            sQuery = " Delete " & _
                                    " From Etiketdty" & _
                                    " Where Pickno=" & txtCeklistNo.Text

                            db.RunSql(sQuery)

                            Exit Sub

                        End If

                        If sRevizyonTarihi = "" Or sRevizyonTarihi = "0" Then
                            sRevizyonTarihi = "0"
                        Else
                            sRevizyonTarihi = DateConvertAs400(sRevizyonTarihi)
                        End If

                        sQuery = " Select WEGHT " & _
                                        " From ITEMASA " & _
                                        " Where Itnbr ='" & row.Cells("ADAITX").Text & "'"

                        dsITEMASA = db.RunSql(sQuery, "ITEMASA")

                        If Not dsITEMASA Is Nothing Then

                            If dsITEMASA.Tables("ITEMASA").Rows.Count > 0 Then

                                dMalzemeBirimAgirligi = dsITEMASA.Tables("ITEMASA").Rows(0).Item("WEGHT").ToString

                            End If

                        End If

                        sQuery = "Select   (select ETKADR " & _
                                                " From  EDIPRM  ) As Adres " & _
                                    " From MBC6REP " & _
                                    " Where C6CVNB ='" & row.Cells("ADCVNB").Text & "'"

                        dsMBC6REP = db.RunSql(sQuery, "MBC6REP")

                        If Not dsMBC6REP Is Nothing Then

                            If dsMBC6REP.Tables("MBC6REP").Rows.Count > 0 Then

                                sSupplierAdres = dsMBC6REP.Tables("MBC6REP").Rows(0).Item("Adres").ToString

                            End If

                        End If

                        sQuery = " SELECT ETKADR1,ETKADR2, DUNSID,  DUNSID2 " & _
                                            " FROM  PLANTPRM " & _
                                            " Where CANB=" & sTirnakEkle(row.Cells("C6CANB").Text) & _
                                            " And B9CD=" & sTirnakEkle(row.Cells("C6B9CD").Text)

                        dsPLANTPRM = db.RunSql(sQuery, "PLANTPRM")

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

                        If rdbSevkiyat.Checked Then

                            sIrsaliyeNo = row.Cells("IRSNO").Text

                        Else

                            sIrsaliyeNo = ""

                        End If

                        iEtiketNo = SeriNoAl("ETK", iKutuSayisi)

                        Dim toplamKutuSayisi As Integer = iKutuSayisi

                        For k As Integer = 1 To iKutuSayisi

                            sEtiketNo = iEtiketNo

                            If k = iKutuSayisi Then

                                iMod = CInt(Miktar) Mod CInt(row.Cells("KMIK").Text)

                                iKutuMiktari = CInt(Miktar) - (iMod * CInt(row.Cells("KMIK").Text))

                                If iMod <> 0 Then

                                    row.Cells("KMIK").Text = iMod

                                Else

                                    row.Cells("KMIK").Text = row.Cells("KMIK").Text

                                End If

                            End If

                            dNetAgirlik = row.Cells("KMIK").Text * dMalzemeBirimAgirligi

                            dBrutAgirilik = dNetAgirlik + dKutuBirimAgirligi

                            nKutuMiktari = row.Cells("KMIK").Text

                            Dim sPusNo As String

                            sPusNo = IIf(row.Cells("CJCBTX").Text = "", row.Cells("CEKLIST").Text, row.Cells("CJCBTX").Text)

                            'Kutu Etiketlerini insert ediyoruz
                            'Sql
                            sQuery = "INSERT INTO  ETIKETDTY" & _
                                        " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                                        " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                                        " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                                        " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                                        " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                                        " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                                        " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                                        " DNGRCODE, IRSNO, DOCK, HNDCODE, SPPLRADR, NETWEIGHT, " & _
                                        " BRTWEIGHT, BOXES, QTYPPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE,KANBANNO, Durum ) " & _
                                    " VALUES(" & row.Cells("CEKLIST").Text & "," & row.Cells("CEKDATE").Text & _
                                             ", " & sTirnakEkle(row.Cells("C6CANB").Text) & " , '" & sEtkAdres1 & "' , '" & sEtkAdres2 & "' , '" & sEtkTip & "' , 1 , 0 , '' , '',  '" & row.Cells("C6B9CD").Text & "','" & row.Cells("C6F1CD").Text & "'" & _
                                             ",'" & row.Cells("ITEM").Text & "','" & row.Cells("REFADI").Text & "','" & row.Cells("ADAITX").Text & "'," & CInt(row.Cells("KMIK").Text.Replace(",", ".")) * toplamKutuSayisi & "," & "'" & sKutuKodu & "'" & ",'" & sMusteriKutuKodu & "'" & _
                                             ", 1," & nKutuMiktari & "," & "'" & sPaletKodu & "'" & "," & "'" & sMusteriPaletKodu & "'" & ", " & iPaletSayisi & " , " & iPalettekiSiraSayisi & " " & _
                                             ", " & iPalettekiKutuMiktari & " , 0 ," & "'" & sRevizyonNo & "'," & sRevizyonTarihi & "," & "'" & sDunsNo & "'," & dMalzemeBirimAgirligi & _
                                             ", " & dKutuBirimAgirligi & " , " & dPaletBirimAgirligi & " , " & " 'KG','KG','KG','KG' " & _
                                             ", 'KG' , " & dSeparatorBirimAgirligi & " , " & dKapakBirimAgirligi & " , '" & sPusNo & "'," & row.Cells("CEKDATE").Text & _
                                             ", '" & sUserF2 & "' ,'','" & sIrsaliyeNo & "', '" & row.Cells("DEF1CD").Text & "' , " & " '" & sHandlingKodu & "', '" & sSupplierAdres & "' " & _
                                             ", " & dNetAgirlik & " , " & dBrutAgirilik & " , 1 ," & nKutuMiktari & ", '" & sEtiketNo & "' , '0' , '', '" & _
                                             row.Cells("CDAFYV").Text & "','" & row.Cells("ADCVNB").Text & "'," & row.Cells("ADFCNB").Text & ",'" & row.Cells("ADA3CD").Text & "','" & sKanbanNo & "',1" & _
                                             ") "

                            db.RunSql(sQuery, True)
                            'Kutu Etiketlerini insert ediyoruz
                            sQuery = "exec TR_ETKTARIHsp " & sEtiketNo
                            dt = db.RunSql(sQuery)

                            'Access
                            sQuery = "INSERT INTO ANSETIKETDTY" & _
                                        " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                                        " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                                        " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                                        " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                                        " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                                        " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                                        " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                                        " DANGERCODE, IRSNO, DOCK, HANDLINGCODE, SUPPLIERADR, NETWEIGHT, " & _
                                        " BRTWEIGHT, BOXES, QUANTITYPERPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE, KANBANNO,TAR1,TAR2,TAR3 ) " & _
                                    " VALUES(" & row.Cells("CEKLIST").Text & "," & row.Cells("CEKDATE").Text & _
                                             ", '" & row.Cells("C6CANB").Text & "' , '" & sEtkAdres1 & "' , '" & sEtkAdres2 & "' , '" & sEtkTip & "' , 1 , 0 , '' , '',  '" & row.Cells("C6B9CD").Text & "','" & row.Cells("C6F1CD").Text & "'" & _
                                             ",'" & row.Cells("ITEM").Text & "','" & row.Cells("REFADI").Text & "','" & row.Cells("ADAITX").Text & "'," & CInt(row.Cells("KMIK").Text.Replace(",", ".")) * toplamKutuSayisi & "," & "'" & sKutuKodu & "'" & ",'" & sMusteriKutuKodu & "'" & _
                                             ", 1 ," & nKutuMiktari & "," & "'" & sPaletKodu & "'" & "," & "'" & sMusteriPaletKodu & "'" & ", " & iPaletSayisi & " , " & iPalettekiSiraSayisi & " " & _
                                             ", " & iPalettekiKutuMiktari & " , 0 ," & "'" & sRevizyonNo & "'," & sRevizyonTarihi & "," & "'" & sDunsNo & "'," & dMalzemeBirimAgirligi & _
                                             ", " & dKutuBirimAgirligi & " , " & dPaletBirimAgirligi & " , " & " 'KG','KG','KG','KG' " & _
                                             ", 'KG' , " & dSeparatorBirimAgirligi & " , " & dKapakBirimAgirligi & " , '" & sPusNo & "'," & row.Cells("CEKDATE").Text & _
                                             ", '" & sUserF2 & "' ,'','" & sIrsaliyeNo & "', '" & row.Cells("DEF1CD").Text & "' , " & " '" & sHandlingKodu & "', '" & sSupplierAdres & "' " & _
                                             ", " & dNetAgirlik & " , " & dBrutAgirilik & " , 1 ," & nKutuMiktari & ", '" & sEtiketNo & "' , '0' , '', '" & _
                                             row.Cells("CDAFYV").Text & "','" & row.Cells("ADCVNB").Text & "'," & row.Cells("ADFCNB").Text & ",'" & row.Cells("ADA3CD").Text & "','" & sKanbanNo & "','" & dt.Rows(0)("create_date") & "' , '" & _
                                               dt.Rows(0)("exp_date") & "' , '" & dt.Rows(0)("SEVKTAR") & "')"


                            dbAccess.RunSql(sQuery, True)


                            iEtiketNo = iEtiketNo - 1

                        Next k

                    Next i

                    sQuery = "Select Max(C6CANB) As C6CANB, Max(C6B9CD) As C6B9CD " & _
                                " From TRCEKLIST" & _
                                " Where CEKLIST=" & txtCeklistNo.Text

                    dt = db.RunSql(sQuery)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        sQuery = "SELECT	CEKLIST ,C6CANB ,max(CUSNM) As CUSNM  ,C6B9CD," & _
                                        " C6F1CD,max(CJCBTX) As CJCBTX ,max(CDAFYV) As CDAFYV, " & _
                                        " ADAITX,max(ITDSC) As ITDSC,max(Replace(ITEM,'*',' ')) As ITEM, " & _
                                        " max(REFADI) As REFADI,max(MOHTQ) As MOHTQ,sum(ADDZVA) As ADDZVA," & _
                                        " sum(ADAQQT) As ADAQQT,max(ADBJDT) As ADBJDT,max(KMIK) As KMIK," & _
                                        " max(C6FNST) As C6FNST,max(ADIIST) As ADIIST,max(DEF1CD) As DEF1CD," & _
                                        " max(ADA3CD) As ADA3CD,max(CEKDATE) As CEKDATE, max(DURUM) As DURUM," & _
                                        " max(KANBANNO) As KANBANNO,max(Ambkod) As Ambkod, Sum(KSAY) As KUTUADET," & _
                                        " Max(PKSAY) As PKMIK, MAX(PKSIR) As PKSIR, SUM(SEVKMIK) As SEVKMIK, MAX(SEVKNO) As SEVKNO" & _
                                    " FROM TRCEKLIST " & _
                                    " Where CEKLIST=" & txtCeklistNo.Text & _
                                    " Group by CEKLIST ,C6CANB ,CUSNM ,C6B9CD,C6F1CD,ADAITX"

                        dtPalet = db.RunSql(sQuery)

                        If Not dtPalet Is Nothing AndAlso dtPalet.Rows.Count > 0 Then

                            For i As Integer = 0 To dtPalet.Rows.Count - 1

                                With dtPalet.Rows(i)
                                    '************* Palet Sayýsý Hesaplama ********************

                                    Dim Miktar As Double

                                    Dim KutuAdet As Double

                                    If rdbCekmeListesi.Checked Then

                                        Miktar = .Item("ADAQQT").ToString

                                    ElseIf rdbSevkiyat.Checked Then

                                        Miktar = .Item("SEVKMIK").ToString

                                    End If

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

                                    sQuery = " Select I.ETKTIP, I.KKOD, I.MKKOD, I.PKOD, I.MPKOD, I.PKSIR, " & _
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

                                    dsITMPACK = db.RunSql(sQuery, "ITMPACK")

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

                                    sQuery = " Select WEGHT " & _
                                                    " From ITEMASA " & _
                                                    " Where Itnbr ='" & .Item("ADAITX").ToString & "'"

                                    dsITEMASA = db.RunSql(sQuery, "ITEMASA")

                                    If Not dsITEMASA Is Nothing Then

                                        If dsITEMASA.Tables("ITEMASA").Rows.Count > 0 Then

                                            dMalzemeBirimAgirligi = dsITEMASA.Tables("ITEMASA").Rows(0).Item("WEGHT").ToString

                                        End If

                                    End If

                                    sQuery = "select ETKADR  As Adres " & _
                                                            " From  EDIPRM  "

                                    dsMBC6REP = db.RunSql(sQuery, "MBC6REP")

                                    If Not dsMBC6REP Is Nothing Then

                                        If dsMBC6REP.Tables("MBC6REP").Rows.Count > 0 Then

                                            sSupplierAdres = dsMBC6REP.Tables("MBC6REP").Rows(0).Item("Adres").ToString

                                        End If

                                    End If

                                    sQuery = " SELECT ETKADR1,ETKADR2, DUNSID, USERF2,DUNSID2 " & _
                                                        " FROM  PLANTPRM " & _
                                                        " Where CANB=" & sTirnakEkle(.Item("C6CANB").ToString) & _
                                                        " And B9CD=" & sTirnakEkle(.Item("C6B9CD").ToString)

                                    dsPLANTPRM = db.RunSql(sQuery, "PLANTPRM")

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

                                    If rdbSevkiyat.Checked Then

                                        sIrsaliyeNo = .Item("SEVKNO").ToString

                                    Else

                                        sIrsaliyeNo = ""

                                    End If

                                    iEtiketNo = SeriNoAl("ETK", PSay)

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
                                        sQuery = "INSERT INTO  ETIKETDTY" & _
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

                                        db.RunSql(sQuery, True)
                                        'Palet Etiketlerini insert ediyoruz
                                        'Access
                                        sQuery = "INSERT INTO ANSETIKETDTY" & _
                                                    " (PICKNO, PICKDT, CUST, ETKADR1, ETKADR2, ETKTIP, " & _
                                                    " KUTUETK, PLTETK, KPAPTIP, PPAPTIP, PLANTID, GATEID, " & _
                                                    " MURNKOD, MURNTNM, ITNBR, MIKTAR, KKOD, MKKOD, " & _
                                                    " KSAY, KMIK, PKOD, MPKOD, PSAY, PKSIR, " & _
                                                    " PKMIK, HKSAY, REVNO, REVTAR, DUNSNO, IBRMAGR, " & _
                                                    " KBRMAGR, PBRMAGR, KAGOB, PAGOB, IAGOB, SPAGOB, " & _
                                                    " KPAGOB, SPBRMAGR, KPBRMAGR, PUSNO, SEVKTAR, USERF2, " & _
                                                    " DANGERCODE, IRSNO, DOCK, HANDLINGCODE, SUPPLIERADR, NETWEIGHT, " & _
                                                    " BRTWEIGHT, BOXES, QUANTITYPERPACK, ETKSERINO, PLTNO, PLTTYPE, L3P , ORDNO , ORDSEQ , HOUSE, KANBANNO ) " & _
                                                " VALUES(" & .Item("CEKLIST").ToString & "," & .Item("CEKDATE").ToString & _
                                                         ", '" & .Item("C6CANB").ToString & "' , '" & sEtkAdres1 & "' , '" & sEtkAdres2 & "' , '" & sEtkTip & "' , 0 , 1 , '' , '',  '" & .Item("C6B9CD").ToString & "','" & .Item("C6F1CD").ToString & "'" & _
                                                         ",'" & .Item("ITEM").ToString & "','" & .Item("REFADI").ToString & "','" & .Item("ADAITX").ToString & "'," & (PaletKSay * .Item("KMIK").ToString.Replace(",", ".")) & "," & "'" & sKutuKodu & "'" & ",'" & sMusteriKutuKodu & "'" & _
                                                         "," & PaletKSay & "," & .Item("KMIK").ToString & "," & "'" & sPaletKodu & "'" & "," & "'" & sMusteriPaletKodu & "'" & ", " & iPaletSayisi & " , " & iPalettekiSiraSayisi & " " & _
                                                         ", " & iPalettekiKutuMiktari & " , 0 ," & "'" & sRevizyonNo & "'," & sRevizyonTarihi & "," & "'" & sDunsNo & "'," & dMalzemeBirimAgirligi & _
                                                         ", " & dKutuBirimAgirligi & " , " & dPaletBirimAgirligi & " , " & " 'KG','KG','KG','KG' " & _
                                                         ", 'KG' , " & dSeparatorBirimAgirligi & " , " & dKapakBirimAgirligi & " , '" & sPusNo & "'," & .Item("CEKDATE").ToString & _
                                                         ", '" & sUserF2 & "' ,'','" & sIrsaliyeNo & "', '" & .Item("DEF1CD").ToString & "' , " & " '" & sHandlingKodu & "', '" & sSupplierAdres & "' " & _
                                                         ", " & dNetAgirlik & " , " & dBrutAgirilik & " , 1 ," & (PaletKSay * .Item("KMIK").ToString.Replace(",", ".")) & ", '" & sEtiketNo & "' , '0' , '', '" & _
                                                         .Item("CDAFYV").ToString & "','" & "" & "'," & "0" & ",'" & .Item("ADA3CD").ToString & "','" & sKanbanNo & "'" & _
                                                         ") "

                                        dbAccess.RunSql(sQuery, True)


                                        'Palet numaralarýný update edeceðiz
                                        sQuery = "Update EtiketDty" & _
                                                    " Set PltNo = " & sEtiketNo & _
                                                " Where EtkSeriNo in (select Top " & PaletKSay & " Etkserino " & _
                                                " from ETiketdty" & _
                                                " where Pltno = 0" & _
                                                    " And pickno=" & .Item("CEKLIST").ToString & _
                                                    " And Itnbr='" & .Item("ADAITX").ToString & "' " & _
                                                    " And KutuETK=1 " & _
                                                    " And (EtkSeriNo<>0 Or EtkSeriNo<>'') " & _
                                                    " Order By EtkSerino) "

                                        db.RunSql(sQuery, True)

                                        iEtiketNo = iEtiketNo - 1

                                    Next k

                                    sQuery = "Update EtiketDty" & _
                                                " Set PltNo = -1" & _
                                            " Where EtkSeriNo in (select Top " & HSay & " Etkserino " & _
                                            " from ETiketdty" & _
                                            " where Pltno = 0" & _
                                                " And pickno=" & .Item("CEKLIST").ToString & _
                                                " And Itnbr='" & .Item("ADAITX").ToString & "' " & _
                                                " And KutuETK=1 " & _
                                                " And (EtkSeriNo<>0 Or EtkSeriNo<>'') " & _
                                                " Order By EtkSerino) "

                                    db.RunSql(sQuery, True)

                                    sQuery = "Update EtiketDty" & _
                                               " Set PltNo = -1" & _
                                           " where Pltno = 0" & _
                                               " And pickno=" & .Item("CEKLIST").ToString & _
                                               " And Itnbr='" & .Item("ADAITX").ToString & "' " & _
                                               " And KutuETK=1 " & _
                                               " And (EtkSeriNo<>0 Or EtkSeriNo<>'')"

                                    db.RunSql(sQuery, True)

                                End With

                            Next i

                        End If

                    End If

                    'End If

                End If

                sQuery = "Update EtiketDty" & _
                            " Set PltNo = -1" & _
                        " where Pltno = 0" & _
                            " And pickno=" & txtCeklistNo.Text

                db.RunSql(sQuery, True)

                If rdbSevkiyat.Checked = False Then

                    sQuery = "Update TRCEKLIST" & _
                                " Set DURUM='2' " & _
                                " Where CEKLIST=" & txtCeklistNo.Text

                    db.RunSql(sQuery)

                End If

                If chkYazdirma.Checked Then

                    yazdirmaislemi(sEtkTip)

                End If

            End If

            GridEX1.DataSource = Nothing

            GridEX1.Refresh()

            btnOlustur.Enabled = False

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        '
        Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try

            sQuery = "SELECT CEKLIST, C6CANB, CUSNM, C6B9CD, C6F1CD, CJCBTX, CDAFYV,     " & _
                        " ADCVNB, ADAITX, ITDSC, Replace(ITEM,'*',' ') As ITEM, REFADI, MOHTQ, ADDZVA, ADAQQT, ADBJDT, " & _
                        " KMIK, C6FNST, ADIIST, DEF1CD, ADA3CD, CEKDATE, ADFCNB, DURUM, " & _
                        " case KMIK when 0 then 0 else ADAQQT  / KMIK end as KUTUADET, KANBANNO ," & _
                        " SEVKMIK, SEVKNO, IRSNO " & _
                        " From TRCEKLIST " & _
                        " Where CEKLIST=" & txtCeklistNo.Text

            'If rdbCekmeListesi.Checked Then

            '    If rdbYazdirilmadi.Checked Then

            '        sQuery = sQuery & " And DURUM in ('0','1')"

            '    ElseIf rdbYazdirildi.Checked Then

            '        sQuery = sQuery & " And DURUM in ('2')"

            '    End If

            'ElseIf rdbSevkiyat.Checked Then

            '    sQuery = sQuery & " And DURUM in ('3','X')"

            'End If

            ds = db.RunSql(sQuery, "Cekme")

            If Not (ds Is Nothing) AndAlso _
                            ds.Tables.Count > 0 AndAlso _
                            ds.Tables("Cekme").Rows.Count > 0 Then

                GridEX1.DataSource = ds

                GridEX1.DataMember = "Cekme"

                btnOlustur.Enabled = True

            Else

                GridEX1.DataSource = Nothing

                btnOlustur.Enabled = False

                MessageBox.Show(" Kayýt Bulunamadý! ", "Ekip Mapics", MessageBoxButtons.OK)

            End If

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    #End Region 'Methods

End Class