Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Friend Class FAna
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Public bOk As Boolean
    Public sFatTarihi, sSirketPCFormat, sFormat, sFormatBrmFiy As Object
    Public sFormatDefterPC As String
    Public sFPC, sTPC As Object
    Public sHoldingNo As Object
    Public sMusAdi As String
    Public sMusNo, sMusNoTum As Object
    Public sSirketNo As String
    Public sSirketPC As String
    Public sTxtDosYol As String
    Public sUserName As String

    Dim bSirketPBBoleni As Byte
    Dim bYuvBasSay As Byte
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dFatKDVTutar As Double
    Dim dFatTutar As Object
    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim sFirmaKodu As String
    Dim sItemDesc As String
    Dim sItemNo As Object

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnOlustur.Click
        Dim sTEXT As String
        Dim i As Object
        Dim lKytNo As Integer
        Dim sFatNo As String = ""
        Dim sFatNoTemp As String = ""
        Dim sFatTar As String = ""
        Dim bChkVar As Boolean
        Dim sMiktar, sFatKDVTut, sFatTut, sBrmFyt, sTutar As Object
        Dim sKDVTutar As String

        bChkVar = False

        For i = 0 To ListView1.Items.Count - 1
            If ListView1.Items.Item(i).Checked = True Then
                bChkVar = True
                Exit For
            End If
        Next i

        If bChkVar = False Then
            MsgBox("Kayýt seçiniz")
            GoTo ExitBlock
        End If

        On Error GoTo ErrBlock
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        sFirmaKodu = "0457"

        sTEXT = "1TH" & sFirmaKodu & Today.ToString("yyyyMMdd") & KarakterEkle("", 165, " ") & vbCrLf

        sFatNoTemp = ""
        lKytNo = 0
        For i = 0 To ListView1.Items.Count - 1

            sFatTut = String.Empty
            sFatKDVTut = String.Empty
            sBrmFyt = String.Empty
            sMiktar = String.Empty
            sTutar = String.Empty
            sKDVTutar = String.Empty

            If ListView1.Items.Item(i).Checked = True And sFatNoTemp <> ListView1.Items.Item(i).SubItems(5).Text Then

                If sFatNoTemp <> "" Then

                    sTEXT = sTEXT & "4FF" & sFirmaKodu & sFatTar & sFatNo & KarakterEkle(CStr(lKytNo), 3, "0", False) & KarakterEkle("", 156, " ") & vbCrLf

                    sFatNoTemp = String.Empty

                    sFatTar = String.Empty

                    sFatNo = String.Empty

                End If

                sFatTar = CDate(ListView1.Items.Item(i).Text).ToString("yyyyMMdd")

                sFatNo = KarakterEkle(ListView1.Items.Item(i).SubItems(1).Text, 6, " ")

                sFatTut = KarakterEkle(VB.Format(ListView1.Items.Item(i).SubItems(2).Text, "0.00"), 15, "0", False)

                sFatTut = Replace(sFatTut, ",", ".")

                sFatKDVTut = KarakterEkle(VB.Format(ListView1.Items.Item(i).SubItems(3).Text, "0.00"), 13, "0", False)

                sFatKDVTut = Replace(sFatKDVTut, ",", ".")

                sTEXT = sTEXT & "2FH" & sFirmaKodu & sFatTar & sFatNo & sFatTut & sFatKDVTut & KarakterEkle(ListView1.Items.Item(i).SubItems(4).Text.ToString.Trim, 120) & KarakterEkle("", 11, " ") & vbCrLf

                sFatNoTemp = ListView1.Items.Item(i).SubItems(5).Text

                lKytNo = 0

            End If

            If ListView1.Items.Item(i).Text = String.Empty And
                ListView1.Items.Item(i).SubItems(1).Text = String.Empty And
                sFatNoTemp.Trim = ListView1.Items.Item(i).SubItems(5).Text.ToString.Trim Then

                sBrmFyt = KarakterEkle(CStr(VB.Format(ListView1.Items.Item(i).SubItems(8).Text, "0.00")), 13, "0", False)

                sMiktar = KarakterEkle(CStr(VB.Format(ListView1.Items.Item(i).SubItems(9).Text, "0.00")), 11, "0", False)

                sTutar = KarakterEkle(CStr(VB.Format(ListView1.Items.Item(i).SubItems(10).Text, "0.00")), 15, "0", False)

                sKDVTutar = KarakterEkle(CStr(VB.Format(ListView1.Items.Item(i).SubItems(11).Text, "0.00")), 13, "0", False)

                sFatNoTemp = Replace(sFatNoTemp, ",", ".")

                sBrmFyt = Replace(sBrmFyt, ",", ".")

                sMiktar = Replace(sMiktar, ",", ".")

                sTutar = Replace(sTutar, ",", ".")

                sKDVTutar = Replace(sKDVTutar, ",", ".")

                lKytNo = lKytNo + 1

                sTEXT = sTEXT & "3DE" & sFirmaKodu & sFatTar & sFatNo & KarakterEkle(ListView1.Items.Item(i).SubItems(6).Text, 11) & sBrmFyt & sMiktar & sTutar & sKDVTutar & KarakterEkle(CStr(lKytNo), 3, "0", False) & KarakterEkle("", 93, " ") & vbCrLf

            End If

        Next i

        sTEXT = sTEXT & "4FF" & sFirmaKodu & sFatTar & sFatNo & KarakterEkle(CStr(lKytNo), 3, "0", False) & KarakterEkle("", 156, " ") & vbCrLf

        sTEXT = sTEXT & "5TF" & sFirmaKodu & Today.ToString("yyyyMMdd") & KarakterEkle("", 165, " ")

        RichTextBox1.Text = sTEXT

        Dim DosyaAdi As String

        DosyaAdi = Txt_DosyaYol.Text & "\" & sFirmaKodu & VB.Format(Today, "mm") & VB.Format(Today, "dd") & ".txt"

        RichTextBox1.SaveFile(DosyaAdi, Windows.Forms.RichTextBoxStreamType.PlainText)

        MsgBox("Dosya Oluþturuldu")

        GoTo ExitBlock

        ErrBlock:

        MsgBox(Err.Number & " " & Err.Description)

        ExitBlock:

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnSorgula_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnSorgula.Click
        Dim sQuery As String

        Dim LstItm As System.Windows.Forms.ListViewItem

        Dim dKDVOran As Double

        Dim dTutar As Double

        Dim sIrsNo As String

        Dim dKurDegeri As Double

        If Txt_Taraf.Text = String.Empty Then
            MsgBox("Taraf seçiniz")
            cmdTanim.Focus()
            GoTo ExitBlock
        End If

        ListView1.Items.Clear()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'FEGHNB  inv_date
        'FEGGNB  inv_num
        'FEKNNB  cust_num
        'FATSERINO INVNO
        'FERIDX curr_code

        sQuery = "select faturatar, INVNO, CUST, FATURASERINO, curr_code "
        sQuery = sQuery & " FROM    shppack s" &
                        " LEFT JOIN dbo.custaddr c  ON s.CUST=c.cust_num" &
                                                        " AND c.cust_seq=0"

        sQuery = sQuery & " where CUST=" & sMusNoTum

        sQuery = sQuery & " and faturatar Between " & sTirnakEkle(dtmBaslangic.Value.ToString("yyyy-MM-dd")) &
                            " and " & sTirnakEkle(dtmBitis.Value.ToString("yyyy-MM-dd"))

        sQuery = sQuery & " group by faturatar, INVNO, CUST, FATURASERINO, curr_code"

        sQuery = sQuery & " order by 1, 4"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        dt = db.RunSql(sQuery)

        If Not dt Is Nothing Then

            If dt.Rows.Count = 0 Then
                btnOlustur.Enabled = False
                MsgBox("Kayýt bulunamadý")
                GoTo ExitBlock
            Else
                btnOlustur.Enabled = True
            End If
        Else
            MsgBox("Kayýt bulunamadý")
            GoTo ExitBlock
        End If

        dFatTutar = 0
        dFatKDVTutar = 0

        sItemNo = String.Empty
        sItemDesc = String.Empty

        For Each row As DataRow In dt.Rows

            sFPC = row.Item("curr_code").ToString

            If sFPC <> sTPC Then
                dKurDegeri = KurGetir(db, sFPC, sTPC, row.Item("faturatar").ToString)
            Else
                dKurDegeri = 1
            End If

            dtTemp = db.RunSql("select distinct IRSNO from SHPPACK where INVNO=" & sTirnakEkle(row.Item("INVNO").ToString.PadLeft(12, " ")))

            sIrsNo = String.Empty

            If Not dtTemp Is Nothing Then

                For Each rowTemp As DataRow In dtTemp.Rows

                    sIrsNo = sIrsNo & KarakterEkle(rowTemp.Item("IRSNO").ToString, 6)

                Next

            End If

            Dim str(12) As String

            Dim itm As ListViewItem

            str(0) = CStr(DateFormatToDate(row.Item("faturatar").ToString))

            str(1) = row.Item("FATURASERINO").ToString

            FaturaTutari(row.Item("INVNO").ToString, dKurDegeri)

            str(2) = FormatNumber(dFatTutar, 2, TriState.True)

            str(3) = FormatNumber(dFatKDVTutar, 2, TriState.True)

            str(4) = sIrsNo

            str(5) = row.Item("INVNO").ToString

            itm = New ListViewItem(str)

            ListView1.Items.Add(itm)

            Array.Clear(str, 0, 11)

            sQuery = "SELECT  ii.item, i.description, ic.cust_item, ic.end_user, ii.qty_invoiced, ii.price," &
                                    " tc.tax_rate" &
                        " FROM    dbo.inv_item ii " &
                        " LEFT JOIN inv_hdr ih " &
                                " ON ih.inv_num = ii.inv_num " &
                        " LEFT JOIN item i " &
                                " ON i.item = ii.item " &
                        " LEFT JOIN itemcust ic " &
                                " ON ic.item = ii.item AND " &
                                    " ic.cust_num = ih.cust_num " &
                        " LEFT JOIN taxcode tc " &
                                " ON tc.tax_code = ii.tax_code1 " &
                        " WHERE   ii.inv_num ='" & row.Item("INVNO").ToString.PadLeft(12, " ") & "'"

            dtTemp = db.RunSql(sQuery)

            If Not dtTemp Is Nothing Then

                For Each rowtemp As DataRow In dtTemp.Rows

                    sItemNo = Trim(rowtemp.Item("cust_item").ToString)

                    sItemDesc = Trim(rowtemp.Item("end_user").ToString)

                    If (sItemNo = String.Empty) And (sItemDesc = String.Empty) Then

                        sItemNo = Trim(rowtemp.Item("item").ToString)

                        sItemDesc = Trim(rowtemp.Item("description").ToString)

                    End If

                    str(5) = row.Item("INVNO").ToString

                    str(6) = sItemNo

                    str(7) = sItemDesc

                    str(8) = FormatNumber((rowtemp.Item("price").ToString * rowtemp.Item("qty_invoiced").ToString * dKurDegeri) / rowtemp.Item("qty_invoiced").ToString, 2, TriState.True)

                    str(9) = FormatNumber(rowtemp.Item("qty_invoiced").ToString, 2, TriState.True)

                    dKDVOran = 0

                    If IsDBNull(rowtemp.Item("tax_rate").ToString) = False Then dKDVOran = rowtemp.Item("tax_rate").ToString

                    dTutar = rowtemp.Item("price").ToString * rowtemp.Item("qty_invoiced").ToString * dKurDegeri

                    str(10) = FormatNumber(dTutar, 2, TriState.True)

                    str(11) = FormatNumber(System.Math.Round(dTutar * dKDVOran / 100, 2), 2, TriState.True)

                    str(12) = row.Item("INVNO").ToString

                    itm = New ListViewItem(str)

                    ListView1.Items.Add(itm)

                    Array.Clear(str, 0, 11)

                Next rowtemp

            End If

        Next row

        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        ListView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
        ListView1.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.ColumnContent)
        ListView1.AutoResizeColumn(6, ColumnHeaderAutoResizeStyle.ColumnContent)
        ListView1.AutoResizeColumn(10, ColumnHeaderAutoResizeStyle.HeaderSize)
        ListView1.AutoResizeColumn(11, ColumnHeaderAutoResizeStyle.ColumnContent)

        GoTo ExitBlock

        ErrBlock:
        MsgBox(Err.Number & " " & Err.Description)

        ExitBlock:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub chkAralik_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAralik.CheckedChanged
        dtmBitis.Enabled = chkAralik.Checked
    End Sub

    Private Sub Chk_Hepsi_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Chk_Hepsi.CheckStateChanged
        Dim i As Integer
        For i = 1 To ListView1.Items.Count
            If ListView1.Items.Item(i).Text <> String.Empty Then ListView1.Items.Item(i).Checked = Chk_Hepsi.CheckState
        Next i
    End Sub

    Private Sub Cmd_Tanim_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTanim.Click
        Dim ResSQLTemp As New ADODB.Recordset

        FTanimlar.ShowDialog()

        If sMusNoTum = String.Empty Then GoTo ExitBlock

        sMusNo = Mid(sMusNoTum, 1, Len(sMusNoTum) - 2)

        If sTPC = "TL" Then

            sFormat = "##,##0"

            sFormatBrmFiy = "##,##0"
            sFormatDefterPC = "##,##0"
            bYuvBasSay = 0
        Else

            sFormat = "##,##0.00"
            bYuvBasSay = 2

            sFormatBrmFiy = "##,##0.00"
            sFormatDefterPC = "##,##0.00"
        End If

        ExitBlock:
    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        On Error Resume Next
        FDizinBul.Dir1.Path = Txt_DosyaYol.Text
        FDizinBul.ShowDialog()
    End Sub

    Private Sub FAna_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim lpReturnString, lpKeyName, lpAppName, lpDefault, lpFileName As String

        Dim Size_Renamed As Integer
        Dim Path As String
        Dim Succ As Short
        Dim Res As Integer

        On Error GoTo ErrBlock

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        lpAppName = "PROGRAM"
        lpKeyName = "TXTDOSYOL"
        lpDefault = ""
        lpReturnString = Space(128)
        Size_Renamed = Len(lpReturnString)
        lpFileName = My.Application.Info.DirectoryPath & "\Dsn.ini"
        Res = GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, lpReturnString, Size_Renamed, lpFileName)
        lpReturnString = Trim(lpReturnString)
        If lpReturnString <> Chr(0) Then
            lpReturnString = Mid(lpReturnString, 1, InStr(lpReturnString, Chr(0)) - 1)
        End If
        sTxtDosYol = lpReturnString
        Txt_DosyaYol.Text = sTxtDosYol

        lpKeyName = "FIRMAKODU"
        lpDefault = ""
        lpReturnString = Space(128)
        Size_Renamed = Len(lpReturnString)
        lpFileName = My.Application.Info.DirectoryPath & "\Dsn.ini"
        Res = GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, lpReturnString, Size_Renamed, lpFileName)
        lpReturnString = Trim(lpReturnString)

        If lpReturnString <> Chr(0) Then
            lpReturnString = Mid(lpReturnString, 1, InStr(lpReturnString, Chr(0)) - 1)
        End If

        sFirmaKodu = lpReturnString

        dtmBaslangic.Value = Today
        dtmBitis.Value = dtmBaslangic.Value

        GoTo ExitBlock
        ErrBlock:
        If Err.Number <> 0 Then
            MsgBox(Err.Number & " " & Err.Description)
        End If
        Me.Close()
        ExitBlock:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FaturaTutari(ByRef lFatNo As String, ByRef dKur As Double)
        Dim dKDVOran As Double
        Dim sQuery As String
        On Error GoTo ErrBlock

        sQuery = "select (ii.price*ii.qty_invoiced" & "*" & dKur & ") TUTAR, tc.tax_rate" &
                    " from inv_item ii " &
                    " LEFT JOIN taxcode tc" &
                        " ON tc.tax_code = ii.tax_code1" &
                        " where  ii.inv_num=" & lFatNo.PadLeft(12, " ") &
                    " group by ii.item, ii.price , ii.qty_invoiced, tc.tax_rate"

        dtTemp = db.RunSql(sQuery)

        dFatTutar = 0
        dFatKDVTutar = 0
        If Not dtTemp Is Nothing Then

            For Each rowTemp As DataRow In dtTemp.Rows

                dFatTutar = dFatTutar + rowTemp.Item("TUTAR").ToString
                dKDVOran = 0

                If IsDBNull(rowTemp.Item("tax_rate").ToString) = False Then dKDVOran = rowTemp.Item("tax_rate").ToString

                dFatKDVTutar = dFatKDVTutar + (rowTemp.Item("TUTAR").ToString * dKDVOran / 100)

            Next rowTemp
        End If

        dFatKDVTutar = System.Math.Round(dFatKDVTutar, 2)

        GoTo ExitBlock
        ErrBlock:
        MsgBox(Err.Number & " " & Err.Description)
        ExitBlock:
    End Sub

    Private Sub ListView1_ItemCheck(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ItemCheckEventArgs) Handles ListView1.ItemCheck
        Dim Item As System.Windows.Forms.ListViewItem = ListView1.Items(eventArgs.Index)

        If Item.Text = String.Empty Then

            'Item.Checked = False
            eventArgs.NewValue = False

        End If
    End Sub

    Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        If e.Item.Text = String.Empty Then

            e.Item.Checked = False

        End If
    End Sub

    Private Sub ResimNoAdi(ByRef sDDAITX As String)
        dtTemp = db.RunSql("select BIHJTX, BIHLTX from MBBIREP" & " where BICANB=" & sMusNo & " and  BIAITX='" & sDDAITX & "'")

        If Not dtTemp Is Nothing Then

            For Each rowTemp As DataRow In dtTemp.Rows

                sItemNo = Trim(rowTemp.Item("BIHJTX").ToString)

                sItemDesc = Trim(rowTemp.Item("BIHLTX").ToString)

            Next rowTemp

        End If

        GoTo ExitBlock
        ErrBlock:
        MsgBox(Err.Number & " " & Err.Description)
        ExitBlock:
    End Sub

    #End Region 'Methods

End Class