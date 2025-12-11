Imports System.Collections.ObjectModel
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Xml
Imports CrystalDecisions.Shared
Imports System.Runtime.InteropServices

Module Genel

    #Region "Fields"

    Public dtAyniyat As New DataTable
    Public HataGoster As New ErrorProvider
    Public KullaniciAdi As String

    'Public sKullanici, sDSNNAme, sSifre As Object
    'Public sDBName As String
    Public KullaniciId As String
    Public nAktarilanKayitSayisi, nMiktarSifir As Integer
    Public nGuncellenenKayitSayisi As Integer
    Public prevColine As Integer
    Public prevConum As Double
    Public prevCustomer As String
    Public prevCustSeq As Integer
    Public UretimReturn As Boolean
    Public FindFormUretim As Boolean
    Public prevGateId As String
    Public prevL3PRef As String
    Public prevNetdte As Date
    Public prevPusNo As String
    Public prevVade As String
    Public Pusnolist As New Generic.List(Of String)
    Public sConnStr As String
    Public sFaturaData As New FaturaData
    Public sFaturaSeri As New FaturaSeri
    Public sIrsaliyeSeri As New IrsaliyeSeri
    Public sReportPath As String
    Public VarsayilanAmbar As String

    Dim dtTemp As New DataTable

    #End Region 'Fields

    #Region "Methods"

    '

    Public NotInheritable Class myPrinters
        Private Sub New()
        End Sub
        <DllImport("winspool.drv", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Public Shared Function SetDefaultPrinter(ByVal Name As String) As Boolean
        End Function

    End Class

    Public Sub AppLogger(ByVal sDesc As String, ByVal sFrom As String)
        Dim FileNum As Short

        FileNum = FreeFile()
        FileOpen(FileNum, My.Application.Info.DirectoryPath & "\AppLog.log", OpenMode.Append)
        WriteLine(FileNum, sDesc, sFrom, Now)
        FileClose(FileNum)
    End Sub

    Public Sub AsnXmlInsert(ByVal Urun1 As String, ByVal Miktar1 As String, ByVal PartNoType As String)
    End Sub

    Public Sub AsnXmlUpdate(ByVal Urun1 As String, ByVal Miktar1 As String)
    End Sub

    Public Sub BolgeselAyar()
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("tr-TR")
        'Bolgesel Ayarlar Opsiyonu
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator = "/"

        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy"

        Application.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture
    End Sub

    Public Function CalcTrnNo() As Double
        Dim nTrnId As Double

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        Dim dsEDI_CO, dsCO As DataSet

        Dim sQuery As String

        sQuery = " SELECT max(substring(co_num, 3,8)) as co_max " & _
                        " FROM EDI_CO "

        dsEDI_CO = db.RunSql(sQuery, "EDI_CO")

        If Not dsEDI_CO Is Nothing Then

            If dsEDI_CO.Tables("EDI_CO").Rows.Count > 0 Then

                If (dsEDI_CO.Tables("EDI_CO").Rows(0).Item("co_max").ToString <> "0") And _
                    (dsEDI_CO.Tables("EDI_CO").Rows(0).Item("co_max").ToString <> "") Then

                    nTrnId = dsEDI_CO.Tables("EDI_CO").Rows(0).Item("co_max").ToString

                Else

                    nTrnId = 0

                End If

            End If

        End If

        If nTrnId = 0 Then

            sQuery = "SELECT max(substring(co_num, 3,8)) as co_max " & _
                        " FROM CO " & _
                        " WHERE taken_by like 'EDI%'"

            dsCO = db.RunSql(sQuery, "CO")

            If Not dsCO Is Nothing Then

                If dsCO.Tables("CO").Rows.Count > 0 Then

                    If (dsCO.Tables("CO").Rows(0).Item("co_max").ToString <> "0") And _
                          (dsCO.Tables("CO").Rows(0).Item("co_max").ToString <> "") Then

                        nTrnId = dsCO.Tables("CO").Rows(0).Item("co_max").ToString

                    Else

                        nTrnId = 0

                    End If

                End If

            End If

        End If

        CalcTrnNo = nTrnId + 1
    End Function

    Public Sub COMSiparisExecute(ByVal sTrnTyp As String, ByVal sOrder As String, _
        ByVal nLine As Integer, _
        ByVal sPUS_No As String, ByVal nCustomer As String, _
        ByVal nCustSeq As Integer, _
        ByVal sItem As String, ByVal sPlant As String, _
        ByVal nTrn As Double, ByVal nTarih As Date, _
        ByVal nMiktar As String, ByVal sCustItem As String, _
        ByVal sHouse As String, ByVal sGate As String, _
        ByVal sL3PRef As String, ByVal dPrice As Double, ByVal sNakliyeci As String, _
        ByVal tprevConum As String, ByVal tprevColine As Integer, _
        ByVal tprevVade As String)
        Dim bHata As Boolean

        Dim dt As New DataTable

        Dim sQuery As String

        Dim sTermsCode As String = ""
        Dim sUm As String = ""
        Dim sDescription As String = ""
        Dim sTaxCode As String = ""
        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        Try

            If sTrnTyp = "3" Then

                If nLookup("count(*)", "coitem", "co_num=" & sTirnakEkle(sOrder) & _
                                " And co_line=" & nLine & _
                                " And qty_shipped= 0 And qty_packed=0") > 0 Then
                    Try

                        db.BeginTransaction()

                        sQuery = "UPDATE Coitem " & _
                               " Set qty_ordered_conv=0" & _
                                " ,qty_ordered=0" & _
                                " WHERE co_num = '" & sOrder & "'" & _
                               " And co_line=" & nLine & _
                               " And qty_shipped= 0 And qty_packed=0"

                        db.RunSql(sQuery, True)

                        sQuery = "Delete " & _
                                    "From coitem " & _
                                    " Where co_num=" & sTirnakEkle(sOrder) & _
                                    " And co_line=" & nLine & _
                                    " And qty_shipped= 0 And qty_packed=0"

                        db.RunSql(sQuery)

                        sQuery = "Delete " & _
                                    "From co " & _
                                    " Where co_num=" & sTirnakEkle(sOrder) & _
                                    " And Not Exists( Select 1 From coitem Where coitem.co_num=co.co_num)"

                        db.RunSql(sQuery)

                        db.CommitTransaction()

                    Catch ex As Exception

                        db.RollbackTransaction()

                        GoTo DurumTamam

                    End Try

                    Exit Sub

                End If

                If nLookup("count(*)", "coitem", "co_num=" & sTirnakEkle(sOrder) & _
                              " And co_line=" & nLine & _
                              " And qty_shipped = qty_invoiced") > 0 Then

        DurumTamam:

                    Try

                        db.BeginTransaction()

                        sQuery = "UPDATE COITEM SET " & _
                                  " STAT ='C' " & _
                                  " WHERE co_num = '" & sOrder & "'" & _
                                  " and co_line=" & nLine

                        db.RunSql(sQuery, True)

                        sQuery = "UPDATE CO SET " & _
                                " STAT ='C' " & _
                                " WHERE co_num = '" & sOrder & "'" & _
                                " And Not Exists( Select count(*) From coitem Where coitem.co_num=co.co_num And coitem.stat='O')"

                        db.RunSql(sQuery, True)

                        db.CommitTransaction()

                    Catch ex As Exception

                        db.RollbackTransaction()

                        Throw ex

                    End Try

                Else

                    Try

                        db.BeginTransaction()

                        sQuery = "UPDATE Coitem " & _
                           " Set qty_ordered_conv=qty_shipped" & _
                            " ,qty_ordered=qty_shipped" & _
                            " WHERE co_num = '" & sOrder & "'" & _
                           " And co_line=" & nLine

                        db.RunSql(sQuery, True)

                        sQuery = "UPDATE COITEM SET " & _
                                  " STAT ='F' " & _
                                  " WHERE co_num = '" & sOrder & "'" & _
                                  " and co_line=" & nLine

                        db.RunSql(sQuery, True)

                        sQuery = "UPDATE CO SET " & _
                                " STAT ='F' " & _
                                " WHERE co_num = '" & sOrder & "'" & _
                                " And Not Exists( Select count(*) From coitem Where coitem.co_num=co.co_num And coitem.stat='O')"

                        db.RunSql(sQuery, True)

                        db.CommitTransaction()

                    Catch ex As Exception

                        db.RollbackTransaction()

                        Throw ex

                    End Try

                End If

                If bHata = False Then

                    nGuncellenenKayitSayisi += 1

                End If

            ElseIf sTrnTyp = "2" Then

                Try

                    db.BeginTransaction()

                    sQuery = "UPDATE Coitem " & _
                           " Set qty_ordered_conv=" & nMiktar & "+qty_shipped" & _
                            " ,qty_ordered=" & nMiktar & "+qty_shipped" & _
                            " WHERE co_num = '" & sOrder & "'" & _
                           " And co_line=" & nLine

                    db.RunSql(sQuery, True)

                    db.CommitTransaction()

                Catch ex As Exception

                    db.RollbackTransaction()

                    Throw ex

                End Try

            ElseIf sTrnTyp = "1" Then

                sQuery = "Select Description, U_M " & _
                            " From item " & _
                            " Where item=" & sTirnakEkle(sItem)

                dt = db.RunSql(sQuery)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    sDescription = dt.Rows(0)("Description").ToString

                    sUm = dt.Rows(0)("u_m").ToString

                End If

                'sQuery = "Select terms_code" & _
                '            " from customer" & _
                '            " Where cust_num=" & sTirnakEkle(nCustomer) & _
                '            " and cust_seq=0"

                'dt = db.RunSql(sQuery)

                'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                sTermsCode = tprevVade 'dt.Rows(0)("terms_code").ToString

                'End If

                sQuery = "Select tax_code1" & _
                            " from customer" & _
                            " Where cust_num=" & sTirnakEkle(nCustomer) & _
                            " and cust_seq=" & nCustSeq

                dt = db.RunSql(sQuery)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    sTaxCode = dt.Rows(0)("tax_code1").ToString

                End If

                sTaxCode = IIf(sTaxCode = "", "Null", sTirnakEkle(sTaxCode))

                Try

                    db.BeginTransaction()

                    sQuery = "INSERT INTO EDI_COITEM  ( " & _
                                "recv_date, posted, ext_date, co_num, co_line, co_release, item, qty_ordered, " & _
                                "due_date, reprice, cust_item,  whse, tax_code1," & _
                                "co_cust_num, ref_type, stat, qty_ordered_conv, price, price_conv, u_m, description,cust_po)" & _
                        " VALUES ( " & _
                                "'" & Now.Date.ToString("yyyy-MM-dd") & "', 0, '" & Now.Date.ToString("yyyy-MM-dd") & "'," & _
                                "'EO" & CStr(nTrn).PadLeft(8, "0") & "', " & prevColine & ", 0, '" & RTrim(sItem) & "'," & nMiktar & "," & _
                                "'" & nTarih.ToString("yyyy-MM-dd") & "', 1, '" & RTrim(sCustItem) & "'," & _
                                "'" & sHouse & "','A00','" & nCustomer & "','P','O'," & nMiktar & "," & _
                                dPrice & "," & dPrice & ",'" & sUm & "','" & sDescription & "'," & sTirnakEkle(sL3PRef) & ")"

                    db.RunSql(sQuery, True)

                    If RTrim(sPUS_No) = "" Then

                        sPUS_No = "FO" & nTarih.ToString("yyMMdd") & "/" & sPlant & "/" & sGate

                        If Pusnolist.Contains(sPUS_No) Then

                            Dim i As Integer = 1

        Pusno:
                            sPUS_No = "FO" & nTarih.ToString("yyMMdd") & "/" & sPlant & "/" & i.ToString & "/" & sGate

                            If Pusnolist.Contains(sPUS_No) Then

                                i += 1

                                GoTo Pusno

                            Else

                                Pusnolist.Add(sPUS_No)

                            End If

                        Else

                            Pusnolist.Add(sPUS_No)

                        End If

                        sPUS_No = Copy(sPUS_No, 0, 22)

                    End If

                    If prevConum <> nTrn Then

                        sQuery = " INSERT INTO EDI_CO ( " & _
                                    " trx_code, tp_code, recv_date, posted, ext_date, type, co_num, cust_num, " & _
                                    " cust_seq, contact, cust_po, order_date, stat, whse, ship_code, tax_code1, frt_tax_code1, msc_tax_code1,terms_code ) " & _
                                " VALUES ( " & _
                                    "'PCO','" & nCustomer & "','" & Now.Date.ToString("yyyy-MM-dd") & "', 0, '" & Now.Date.ToString("yyyy-MM-dd") & "'," & _
                                    "'R','EO" & CStr(nTrn).PadLeft(8, "0") & "', '" & nCustomer & "'," & nCustSeq & "," & _
                                    "'" & RTrim(sGate) & "', '" & sPUS_No & "', '" & nTarih.ToString("yyyy-MM-dd") & "','O', '" & sHouse & "', '" & sNakliyeci & "'," & _
                                    "" & sTaxCode & ", " & sTaxCode & ", " & sTaxCode & ",'" & sTermsCode & "'" & _
                                    ")"

                        db.RunSql(sQuery, True)

                        prevConum = nTrn

                    End If

                    db.CommitTransaction()

                Catch ex As Exception

                    db.RollbackTransaction()

                    Throw ex

                End Try

                If bHata = False Then

                    nAktarilanKayitSayisi += 1

                End If

            End If

        Catch ex As Exception

            Throw ex

        End Try
    End Sub

    Public Sub ComSiparisUpdate(ByVal sTip As String, ByVal sOrder As String, ByVal sPlant As String, _
        ByVal sGate As String, ByVal sItem As String, ByVal sHouse As String, _
        ByVal sCarrier As String, ByVal sPUS_No As String, _
        ByVal sCustomer As String, ByVal nCusSeq As Integer, ByVal nTarih As Date, ByVal nMiktar As Double, _
        ByVal nLine As Double, ByVal sL3PREF As String, ByVal sCustItem As String, _
        ByVal dPrice As Double, ByVal sNakliyeci As String, ByVal sVade As String)
        Dim sTrnTyp As String
        Dim nTrn As Double
        Dim nOrderDate As Date
        Dim bKayit As Boolean

        sTrnTyp = ""

        If sTip = "I" Then
            'nOrderDate=nLookUp('MIN(NETDTE)',dtmEkip.LibNameEDI+'.EDIPRC',' MPXCUS='+FloattoStr(nCustomer)+' AND PLANTID='+
            '           Quote(sPlant)+' AND GATEID='+Quote(sGate)+' AND MPXITM='+Quote(sItem))

            nOrderDate = nTarih

            sTrnTyp = "1"

            If nMiktar = 0 Then

                nMiktarSifir += 1

                bKayit = False

            Else

                bKayit = True

            End If

            sOrder = ""

        ElseIf (sTip = "D") Then

            sTrnTyp = "3"

            If RTrim(sLookup("substring(cust_po,1,2)", "co", _
                        " co_num = '" & sOrder & "'")) = "FO" OrElse _
                RTrim(sLookup("substring(cust_po,1,2)", "co", _
                        " co_num = '" & sOrder & "'")) = "EO" OrElse _
                RTrim(sLookup("substring(cust_po,1,2)", "co", _
                        " co_num = '" & sOrder & "'")) = "" Then

                bKayit = True

            Else

                bKayit = False

            End If

        ElseIf (sTip = "U") Then

            sTrnTyp = "2"

            If RTrim(sLookup("substring(cust_po,1,2)", "co", _
                        " co_num = '" & sOrder & "'")) = "FO" OrElse _
                RTrim(sLookup("substring(cust_po,1,2)", "co", _
                        " co_num = '" & sOrder & "'")) = "EO" OrElse _
                RTrim(sLookup("substring(cust_po,1,2)", "co", _
                        " co_num = '" & sOrder & "'")) = "" Then

                bKayit = True

            Else

                bKayit = False

            End If

        End If

        If bKayit Then

            If prevCustomer <> sCustomer OrElse _
                prevCustSeq <> nCusSeq OrElse _
                prevGateId <> sGate OrElse _
                prevNetdte <> nTarih OrElse _
                prevPusNo <> sPUS_No OrElse _
                prevVade <> sVade Then

                nTrn = CalcTrnNo()

                prevCustomer = sCustomer
                prevCustSeq = nCusSeq
                prevGateId = sGate
                prevNetdte = nTarih
                prevPusNo = sPUS_No
                prevVade = sVade
                prevColine = 1

            Else

                nTrn = prevConum
                prevColine = prevColine + 1

            End If

            COMSiparisExecute(sTrnTyp, sOrder, nLine, sPUS_No, sCustomer, nCusSeq, sItem, sPlant, nTrn, nTarih, nMiktar, sCustItem, sHouse, sGate, sL3PREF, dPrice, sNakliyeci, prevConum, prevColine, sVade)

        End If

        'If (sTip = "U") And (bKayit) And (nMiktar <> 0) Then

        '    'nOrderDate=nLookUp('MIN(NETDTE)',SetTableName(dtmEkip.LibNameEDI,'EDIPRC'),' MPXCUS='+FloattoStr(nCustomer)+' AND PLANTID='+
        '    '           Quote(sPlant)+' AND GATEID='+Quote(sGate)+' AND MPXITM='+Quote(sItem))

        '    nOrderDate = nTarih

        '    sTrnTyp = "1"

        '    sOrder = ""

        '    nTrn = CalcTrnNo()

        '    COMSiparisExecute(sTrnTyp, sOrder, nLine, sPUS_No, sCustomer, nCusSeq, sItem, sPlant, nTrn, nTarih, nMiktar, sCustItem, sHouse, sGate, sL3PREF, dPrice, sNakliyeci, prevConum, prevColine)

        'End If
    End Sub

    Public Function ConvertAS400DatetoStr(ByVal sTmp As Date) As String
        ConvertAS400DatetoStr = sTmp.ToString("yyyyMMdd")
    End Function

    Public Function ConvertDate(ByVal sTmp As String) As Date
        Dim sYear, sMonth, sDay As String

        sYear = Mid(sTmp, 1, 4)
        sMonth = Mid(sTmp, 5, 2)
        sDay = Mid(sTmp, 7, 2)

        ConvertDate = CDate(sYear & "-" & sMonth & "-" & sDay)

        Return ConvertDate
    End Function

    Public Function ConvertDateAs400Format(ByVal sTmp As String) As String
        Dim sYear, sMonth, sDay As String
        Dim Tarih As Date
        sYear = Mid(sTmp, 1, 4)
        sMonth = Mid(sTmp, 5, 2)
        sDay = Mid(sTmp, 7, 2)

        Tarih = DateSerial(sYear, sMonth, sDay)

        ConvertDateAs400Format = Tarih.ToString("1yyMMdd")
    End Function

    '
    '1060322 olan tarihi 22.03.2006 ya çevirir
    Public Function ConvertDateFromDb2(ByVal s As String) As Date
        Try
            Dim y As Int32 = ((Integer.Parse(s.Substring(0, 1)) + 1).ToString & "0") & s.Substring(1, 2)
            Dim m As Int32 = s.Substring(3, 2)
            Dim d As Int32 = s.Substring(5, 2)

            Dim dt As New Date(y, m, d)
            Return dt
        Catch ex As Exception
            Return Now
        End Try
    End Function

    Public Function Copy(ByVal Str As String, ByVal Baslangic As Integer, ByVal Bitis As Integer)
        If Bitis < 0 Then

            Copy = ""

        ElseIf Str.Length > Baslangic + Bitis Then

            Copy = Str.Substring(Baslangic, Bitis)

        ElseIf Str.Length > Baslangic Then

            Copy = Str.Substring(Baslangic, Str.Length - Baslangic)

        Else

            Copy = ""

        End If
    End Function

    Public Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Public Function DataTableFilter(ByVal Condition As String, ByVal dt As DataTable) As Boolean
        Dim foundRows() As DataRow = Nothing

        Dim Deger As String

        foundRows = dt.Select(Condition)

        If foundRows.Length > 0 Then

            Deger = foundRows(0).ItemArray(0).ToString

        Else

            Deger = "Yok"

        End If
    End Function

    Public Function DateConvert(ByVal sTmp As String) As Date
        Dim sYear, sMonth, sDay As String

        sYear = Mid(sTmp, 1, 4)
        sMonth = Mid(sTmp, 5, 2)
        sDay = Mid(sTmp, 7, 2)

        DateConvert = DateSerial(sYear, sMonth, sDay)

        Return DateConvert
    End Function

    Public Function DateConvertAs400(ByVal dDate As DateTime) As String
        Dim sDay, sMonth, sYear As String

        sDay = dDate.Day
        sMonth = dDate.Month
        sYear = dDate.Year

        If Len(sDay.ToString) = 1 Then
            sDay = "0" & sDay
        End If

        If Len(sMonth.ToString) = 1 Then
            sMonth = "0" & sMonth
        End If

        DateConvertAs400 = "1" & sYear.Substring(2, 2) & sMonth & sDay

        Return DateConvertAs400
    End Function

    Public Function DateConvertVb(ByVal sTmp As String) As Date
        Dim sYear, sMonth, sDay As String
        If sTmp <> "0" Then
            sYear = Mid(sTmp, 2, 2)
            sMonth = Mid(sTmp, 4, 2)
            sDay = Mid(sTmp, 6, 2)

            DateConvertVb = CDate("20" & sYear & "-" & sMonth & "-" & sDay)

        Else

            DateConvertVb = Nothing

        End If
        Return DateConvertVb
    End Function

    Public Sub Duzenle(ByRef grid As Janus.Windows.GridEX.GridEX, Optional ByVal Collapse As Boolean = True)
        For Each col As Janus.Windows.GridEX.GridEXColumn In grid.RootTable.Columns

            col.AutoSize()

        Next
        If Collapse Then

            grid.CollapseGroups()

        End If
    End Sub

    Public Function EncodeDateWeek(ByVal nYear As Integer, ByVal nWeek As Integer, ByVal nWeekOfDay As Integer) As Date
        Dim sTarih As Date

        sTarih = CDate(DateAdd(DateInterval.Day, ((nWeek - 1) * 7) + nWeekOfDay, CDate(nYear & "-01-01")))

        Return sTarih
    End Function

    '
    Public Sub errLogger(ByVal lNum As Integer, ByVal sDesc As String, ByVal sFrom As String)
        Dim FileNum As Short

        FileNum = FreeFile()
        FileOpen(FileNum, My.Application.Info.DirectoryPath & "\Errors.log", OpenMode.Append)
        WriteLine(FileNum, lNum, sDesc, sFrom, Now)
        FileClose(FileNum)
    End Sub

    Public Sub ExceleAktar(ByVal dt As DataTable)
        Dim objXL As Object
        Dim i As Object
        Dim j As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Dim oldCI As System.Globalization.CultureInfo = _
        System.Threading.Thread.CurrentThread.CurrentCulture

        System.Threading.Thread.CurrentThread.CurrentCulture = _
        New System.Globalization.CultureInfo("en-US")

        'If ListView1.Visible = True Then
        objXL = CreateObject("Excel.Application")

        objXL.Workbooks.Add()

        For i = 0 To dt.Columns.Count - 1

            objXL.Cells(1, i + 1).Value = dt.Columns.Item(i).ToString

        Next i

        For i = 0 To dt.Rows.Count - 1

            For j = 0 To dt.Columns.Count - 1

                If j = 0 Then

                    If IsNumeric(dt.Rows.Item(i)(0).ToString) Then

                        objXL.Cells(i + 2, j + 1).Value = CDbl(dt.Rows.Item(i)(0).ToString)

                    ElseIf IsDate(dt.Rows(i).Item(j).ToString) Then

                        objXL.Cells(i + 2, j + 1).Value = CDate(dt.Rows(i).Item(j).ToString).ToString("dd.MM.yyyy")
                    Else

                        objXL.Cells(i + 2, j + 1).Value = dt.Rows.Item(i)(0).ToString

                    End If

                Else

                    If IsNumeric(dt.Rows.Item(i)(j).ToString) Then

                        objXL.Cells(i + 2, j + 1).Value = CDbl(dt.Rows.Item(i)(j).ToString)

                    ElseIf IsDate(dt.Rows(i).Item(j).ToString) Then

                        objXL.Cells(i + 2, j + 1).Value = CDate(dt.Rows(i).Item(j).ToString).ToString("dd.MM.yyyy")

                    Else

                        objXL.Cells(i + 2, j + 1).Value = dt.Rows.Item(i)(j).ToString

                    End If

                End If

            Next j

        Next i

        objXL.Columns.Autofit()

        Dim sut As Short

        sut = i

        GoTo ExitBlock
        ErrBlock:
        MessageBox.Show(Err.Number & " " & Err.Description)

        ExitBlock:
        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MessageBox.Show("EXCEL'e taþýndý")

        objXL.Visible = True
    End Sub

    Public Function ExtractWord(ByVal i As Integer, ByVal Str As String, ByVal karakter As Char)
        Try
            ExtractWord = IIf(Str.Split(karakter).Length >= i - 1, Str.Split(karakter)(i - 1), "")

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Public Function FindAndReplace(ByVal Str As String, ByVal onceki As String, ByVal sonraki As String)
        FindAndReplace = Str.Replace(onceki, sonraki)
    End Function

    Public Sub GetRowInfo(ByRef oObject As Object, ByVal dtRow As DataTable, ByVal oRow As Integer, ByVal oColumn As Object)
        Try

            If Not dtRow Is Nothing AndAlso dtRow.Rows.Count >= oRow + 1 Then

                oObject = IIf(dtRow.Rows(oRow).Item(oColumn).ToString = "" And (TypeOf (oObject) Is Double Or TypeOf (oObject) Is Integer), 0, dtRow.Rows(oRow).Item(oColumn).ToString)

            Else

                If TypeOf oObject Is Integer Or TypeOf oObject Is Double Then

                    oObject = 0

                ElseIf TypeOf oObject Is String Then

                    oObject = ""

                Else

                    oObject = Nothing

                End If

            End If

        Catch ex As Exception

            Throw ex

        End Try
    End Sub

    Public Sub Inc(ByRef Sayi As Integer)
        Sayi = Sayi + 1
    End Sub

    Public Sub Insert(ByVal Dizi As Collection(Of FieldAndValue), ByVal TabloAdi As String)
        Dim Sorgu As String

        Dim Tablo As New DataTable

        Dim Satir As DataRow

        Dim Baslangic As Boolean = False

        Dim Virgul As String = ","

        Dim Tirnak As String = " "

        Dim db As New Core.Data(My.Settings.ConnectionString)

        Sorgu = " Insert Into " & TabloAdi

        Sorgu = Sorgu & " ("

        Tablo = db.RunSql(" Exec Tr_SqlTableInfo '2005','" & TabloAdi & "'")

        Baslangic = False

        For Each eleman As FieldAndValue In Dizi

            Satir = Tablo.Select("Name=" & "'" & eleman.Name & "'")(0)

            If Satir.Item("IsIdentity").ToString <> "1" Then

                Sorgu = Sorgu & IIf(Baslangic, Virgul, " ") & eleman.Name

                Baslangic = True

            End If

        Next

        Sorgu = Sorgu & " ) Values ( "

        Baslangic = False

        For Each eleman As FieldAndValue In Dizi

            Satir = Tablo.Select("Name=" & "'" & eleman.Name & "'")(0)

            If Satir.Item("IsIdentity").ToString <> "1" Then

                If Satir.Item("DataType").ToString = "datetime" Or _
                 Satir.Item("DataType").ToString = "varchar" Or _
                 Satir.Item("DataType").ToString = "nchar" Or _
                 Satir.Item("DataType").ToString = "char" Or _
                 Satir.Item("DataType").ToString = "ntext" Or _
                 Satir.Item("DataType").ToString = "nvarchar" Then
                    Tirnak = "'"
                Else
                    Tirnak = ""
                End If

                Sorgu = Sorgu & IIf(Baslangic, Virgul, " ") & Tirnak & eleman.Value & Tirnak

                Baslangic = True

            End If

        Next

        Sorgu = Sorgu & ")"

        db.RunSql(Sorgu, True)
    End Sub

    Public Function kontrol(Optional ByVal jControl As Control = Nothing, _
        Optional ByVal Controls As Control = Nothing) As Boolean
        Dim Control
        Dim bHata As Boolean = True
        Dim Name As String
        If jControl Is Nothing Then
            Name = ""
        Else
            Name = jControl.Name
        End If
        For Each Control In Controls.Controls
            If Control.Tag = "1" And IIf(jControl Is Nothing, Control.name, Name) = Control.name Then
                If Control.Text = "" Then
                    HataGoster.SetError(Control, "Lütfen Deðer Giriniz")
                    bHata = False
                Else
                    HataGoster.SetError(Control, "")
                End If

            End If

        Next

        Return bHata
    End Function

    Public Sub MusteriAmbalajBilgileri(ByRef sMAMBKOD As String, _
        ByRef sMPKOD As String, _
        ByRef sMKKOD As String, _
        ByRef sMSPKOD As String, _
        ByRef sMKPKOD As String, _
        ByVal Malzeme As String, _
        ByVal AmbalajKodu As String)
        Dim db As New Core.Data(My.Settings.ConnectionString)
        Dim sSql As String
        Try

            sSql = " SELECT MAMBKOD,MKKOD,MPKOD,MSPKOD,MKPKOD " & _
                    " FROM ITMPACK" & _
                    " WHERE AMBKOD=" & sTirnakEkle(AmbalajKodu) & _
                    " AND ITNBR=" & sTirnakEkle(Malzeme)

            dtTemp = db.RunSql(sSql)

            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                sMAMBKOD = Trim(dtTemp.Rows(0).Item("MAMBKOD").ToString)

                sMPKOD = Trim(dtTemp.Rows(0).Item("MPKOD").ToString)

                sMKKOD = Trim(dtTemp.Rows(0).Item("MKKOD").ToString)

                sMSPKOD = Trim(dtTemp.Rows(0).Item("MSPKOD").ToString)

                sMKPKOD = Trim(dtTemp.Rows(0).Item("MKPKOD").ToString)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub MusteriMalzemeBilgileri(ByRef sMURNKOD As String, _
        ByRef sMUrnTanim As String, _
        ByRef sKontrat As String, _
        ByRef sKanban As String, _
        ByVal sCustomer As String, _
        ByVal sPlant As String, _
        ByVal sGate As String, _
        ByVal sMalzeme As String)
        Dim sSql As String
        Dim db As New Core.Data(My.Settings.ConnectionString)

        Try

            sSql = " SELECT ITEM,KNTRT,REFADI, Kanban " & _
                           " FROM KONTRTPF" & _
                           " WHERE CUST=" & sTirnakEkle(sCustomer) & _
                           " AND SHIPTO=" & sTirnakEkle(sPlant) & _
                           " AND KAPI=" & sTirnakEkle(sGate) & _
                           " AND BZMITM=" & sTirnakEkle(sMalzeme)

            dtTemp = db.RunSql(sSql)

            If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then

                sMURNKOD = RTrim(Replace(dtTemp.Rows(0).Item("ITEM").ToString, "*", " "))

                sKontrat = RTrim(dtTemp.Rows(0).Item("KNTRT").ToString)

                sMUrnTanim = RTrim(dtTemp.Rows(0).Item("REFADI").ToString)

                sKanban = RTrim(dtTemp.Rows(0).Item("KANBAN").ToString)

            End If

        Catch ex As Exception

            Throw ex

        End Try
    End Sub

    Public Function nLookup(ByVal sField As String, ByVal sTable As String, ByVal sWhere As String) As Double
        Dim sQuery As String

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

        Dim ds As DataSet

        sQuery = "SELECT " & sField
        sQuery = sQuery & " FROM " & sTable

        If sWhere <> "" Then

            sQuery = sQuery & " WHERE " & sWhere

        End If

        ds = db.RunSql(sQuery, "Table")

        If Not ds Is Nothing Then

            If ds.Tables("Table").Rows.Count > 0 Then

                nLookup = IIf(ds.Tables("Table").Rows(0).Item(0).ToString = "", 0, ds.Tables("Table").Rows(0).Item(0).ToString)

            End If

        Else

            nLookup = 0

        End If

        Return nLookup
    End Function

    Public Sub RaporCagir(ByVal sRapor As String, _
        Optional ByVal sFormul As String = "", _
        Optional ByVal sBaslik As String = "", _
        Optional ByVal sKagitTipi As String = "", _
        Optional ByVal bShowPrintButton As Boolean = True, _
        Optional ByVal bShowExportButton As Boolean = True, _
        Optional ByVal sTip As String = "", _
        Optional ByVal dtRapor As DataTable = Nothing,
        Optional ByVal Parms As CrystalDecisions.Shared.ParameterFields = Nothing)

        For Each frm As Form In My.Application.OpenForms
            If frm.Name.Equals(frmRapor.Name) Then
                frmRapor.Close()
                frmRapor.Dispose()
            End If
        Next

        frmRapor.sRapor = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "Rapor\" & sRapor

        frmRapor.sBaslik = sBaslik

        frmRapor.sTip = sTip

        frmRapor.sKagitAdi = sKagitTipi

        frmRapor.dtRapor = dtRapor

        frmRapor.Refresh()

        frmRapor.rptViewer.ShowPrintButton = bShowPrintButton

        frmRapor.rptViewer.ShowExportButton = bShowExportButton

        frmRapor.rptViewer.SelectionFormula = sFormul

        frmRapor.rptViewer.ParameterFieldInfo = Parms

        'frmRapor.rptViewer.RefreshReport()

        frmRapor.rptViewer.Refresh()
        '
        frmRapor.Refresh()
        '
        frmRapor.nRefreshTime = 0
        frmRapor.bRefresh = False

        frmRapor.ShowDialog()

        frmRapor.rptViewer.ShowPrintButton = True

        frmRapor.rptViewer.ShowExportButton = True
    End Sub

    Public Sub RaporCagirOzet(ByVal sRapor As String, _
        Optional ByVal sFormul As String = "", _
        Optional ByVal sBaslik As String = "", _
        Optional ByVal sKagitTipi As String = "", _
        Optional ByVal bShowPrintButton As Boolean = True, _
        Optional ByVal bShowExportButton As Boolean = True, _
        Optional ByVal sTip As String = "", _
        Optional ByVal dtRapor As DataTable = Nothing)
        Dim paramFields As New CrystalDecisions.Shared.ParameterFields

        'paramFields = AddParameter("@Isemri", txtRmaNo.Text, paramFields)

        AccessLogOnDatabase("Rapor\" & sRapor, dtRapor)

        frmRaporOzet.sBaslik = sBaslik

        frmRaporOzet.sTip = sTip

        frmRaporOzet.sKagitAdi = sKagitTipi

        frmRaporOzet.CrystalReportViewer1.ShowPrintButton = bShowPrintButton

        frmRaporOzet.CrystalReportViewer1.ShowExportButton = bShowExportButton

        frmRaporOzet.CrystalReportViewer1.SelectionFormula = sFormul

        frmRaporOzet.CrystalReportViewer1.ParameterFieldInfo = paramFields

        If sFormul.Length > 0 Then

            frmRaporOzet.reportDocument1.RecordSelectionFormula = sFormul

        End If

        'Re setting control

        frmRaporOzet.reportDocument1.SummaryInfo.ReportTitle = sBaslik

        'Set the current report object to report.

        If sKagitTipi <> "" Then

            Dim sayfa As System.Drawing.Printing.PaperSize = New System.Drawing.Printing.PaperSize(sKagitTipi, 1, 1)

            Dim pdc As New PrintDocument()

            Dim rawKind As Integer = 0

            For i As Integer = 0 To pdc.PrinterSettings.PaperSizes.Count - 1

                If pdc.PrinterSettings.PaperSizes(i).PaperName = sKagitTipi Then

                    rawKind = CInt(pdc.PrinterSettings.PaperSizes(i).RawKind)

                End If

            Next

            frmRaporOzet.reportDocument1.PrintOptions.PaperSize = DirectCast(rawKind, CrystalDecisions.Shared.PaperSize)

        End If

        frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1

        frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = False

        frmRaporOzet.ShowDialog()
    End Sub

    Public Function ReadConfig(ByVal ConfigName As String) As String
        Try
            Dim ConfigValue1 As String
            Dim XmlReader As New XmlTextReader(AppDomain.CurrentDomain.BaseDirectory.ToString + "Config.xml")
            Dim XmlDocument As New XmlDocument
            XmlDocument.Load(XmlReader)

            ConfigValue1 = XmlDocument.Item("root").Item(ConfigName).InnerText

            Return ConfigValue1

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub RaporCagirOzet2(ByVal sRapor As String, _
        Optional ByVal sFormul As String = "", _
        Optional ByVal sBaslik As String = "", _
        Optional ByVal sKagitTipi As String = "", _
        Optional ByVal bShowPrintButton As Boolean = True, _
        Optional ByVal bShowExportButton As Boolean = True, _
        Optional ByVal sTip As String = "", _
        Optional ByVal dtRapor As DataTable = Nothing
        )
        Dim paramFields As New CrystalDecisions.Shared.ParameterFields

        'paramFields = AddParameter("@Isemri", txtRmaNo.Text, paramFields)

        AccessLogOnDatabase2("Rapor\" & sRapor, dtRapor)

        frmRaporOzet2.sBaslik = sBaslik

        frmRaporOzet2.sTip = sTip

        frmRaporOzet2.sKagitAdi = sKagitTipi

        frmRaporOzet2.CrystalReportViewer2.ShowPrintButton = bShowPrintButton

        frmRaporOzet2.CrystalReportViewer2.ShowExportButton = bShowExportButton

        frmRaporOzet2.CrystalReportViewer2.SelectionFormula = sFormul

        frmRaporOzet2.CrystalReportViewer2.ParameterFieldInfo = paramFields

        If sFormul.Length > 0 Then

            frmRaporOzet2.reportDocument2.RecordSelectionFormula = sFormul

        End If

        'Re setting control

        frmRaporOzet2.reportDocument2.SummaryInfo.ReportTitle = sBaslik

        'Set the current report object to report.

        myPrinters.SetDefaultPrinter(ReadConfig("DefaultPrinterIrsaliye")) 'doPDF v7


        'MessageBox.Show(ReadConfig("DefaultPrinterIrsaliye"))

        Dim db As New Core.Data(My.Settings.ConnectionString.ToString) 'as400 connection

        Dim Str As String
        Dim GetDateStr As String
        Str = " select GETDATE() "
        dtRapor = db.RunSql(Str)
        GetDateStr = CDate(dtRapor.Rows(0)(0)).ToString("yyyy-MM-dd HH:mm:ss.fff")
        Dim PrtDocGeneral As New PrintDocument
        Do
            Str = " select DATEDIFF(ss,'" & GetDateStr & "',GETDATE()) "
            dtRapor = db.RunSql(Str)
            If CInt(dtRapor.Rows(0)(0)) >= 5 Then
                Exit Do
            End If

            Application.DoEvents()
        Loop Until PrtDocGeneral.PrinterSettings.PrinterName = ReadConfig("DefaultPrinterIrsaliye") '"doPDF v7" '.IndexOf("ZEBRA").ToString <> "-1"

        If sKagitTipi <> "" Then

            Dim sayfa As System.Drawing.Printing.PaperSize = New System.Drawing.Printing.PaperSize(sKagitTipi, 1, 1)

            Dim pdc As New PrintDocument()

            Dim rawKind As Integer = 0
            'rawKind = 122
            For i As Integer = 0 To pdc.PrinterSettings.PaperSizes.Count - 1

                If pdc.PrinterSettings.PaperSizes(i).PaperName = sKagitTipi Then

                    rawKind = CInt(pdc.PrinterSettings.PaperSizes(i).RawKind)
                    'MessageBox.Show(rawKind)122
                End If

            Next

            Dim PrtDoc As New PrintDocument
            Dim DefaultPrinter As String = PrtDoc.PrinterSettings.PrinterName

            frmRaporOzet2.reportDocument2.PrintOptions.PrinterName = DefaultPrinter

            frmRaporOzet2.reportDocument2.PrintOptions.PaperSize = DirectCast(rawKind, CrystalDecisions.Shared.PaperSize)
            'frmRaporOzet2.reportDocument2.PrintOptions.CustomPaperSource.RawKind = 122

        End If

        frmRaporOzet2.reportDocument2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
        frmRaporOzet2.CrystalReportViewer2.ReportSource = frmRaporOzet2.reportDocument2


        frmRaporOzet2.CrystalReportViewer2.ShowRefreshButton = False

        frmRaporOzet2.Show() 'ShowDialog()

    End Sub

    Public Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function

    Public Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub

    Public Function SetTableName(ByVal Database As String, ByVal Table As String) As String
        SetTableName = Database & ".dbo." & Table
    End Function

    Public Function sLookup(ByVal sField As String, ByVal sTable As String, ByVal sWhere As String) As String
        Try
            Dim sQuery As String

            Dim db As New Core.Data(My.Settings.ConnectionString.ToString)

            Dim ds As DataSet

            sLookup = ""

            sQuery = "SELECT " & sField

            sQuery = sQuery & " FROM " & sTable

            If sWhere <> "" Then

                sQuery = sQuery & " WHERE " & sWhere

            End If

            ds = db.RunSql(sQuery, "Table")

            If Not ds Is Nothing Then

                If ds.Tables("Table").Rows.Count > 0 Then

                    sLookup = RTrim(ds.Tables("Table").Rows(0).Item(0).ToString)

                End If

            Else

                sLookup = ""

            End If

            Return sLookup

        Catch ex As Exception

            Throw ex

        End Try
    End Function

    Public Function sTarih(ByVal sStr As Object) As String
        sStr = CStr(sStr)

        If sStr = "" Then

            sStr = "0"

            'MessageBox.Show("Hatalý Tarih Ýçeriyor", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            sStr = DateSerial(1990, 1, 1).ToString("yyyy-MM-dd")

        ElseIf IsDate(sStr) Then

            sStr = CDate(sStr).ToString("yyyy-MM-dd")

        Else

            sStr = sStr

        End If

        sStr = "'" & sStr & "'"

        sTarih = sStr

        Return sStr
    End Function

    Public Function sTirnakEkle(ByVal sStr As String) As String
        sStr = RTrim(sStr)

        If sStr Is Nothing Then
            sStr = ""
        End If

        sStr = sStr.Replace("'", " ")

        sStr = sStr.Replace(",", " ")

        sStr = "N'" & sStr & "'"

        sTirnakEkle = sStr

        Return sStr
    End Function

    Public Function StrFill(ByVal Str As String, ByVal Toplam As Integer, ByVal Karakter As Char, ByVal Yon As Char) As String
        StrFill = IIf(Yon = "L", Str.PadLeft(Toplam, Karakter), Str.PadRight(Toplam, Karakter))

        Return StrFill
    End Function

    Public Function TarihCevirSlashli(ByVal sTmp As String) As Date
        Dim sYear, sMonth, sDay As String

        sYear = Mid(sTmp, 7, 4)
        sMonth = Mid(sTmp, 4, 2)
        sDay = Mid(sTmp, 1, 2)

        TarihCevirSlashli = DateSerial(sYear, sMonth, sDay)

        Return TarihCevirSlashli
    End Function

    Public Function TrimSifir(ByVal Str As String) As String
        TrimSifir = Str
    End Function

    Public Function WeekOfTheYear(ByVal dateToEvaluate As DateTime) As Integer
        Return DatePart("ww", dateToEvaluate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
    End Function

    #End Region 'Methods

    #Region "Nested Types"

    Public Structure FaturaData

        #Region "Fields"

        Dim AccountNo As String
        Dim AccountNo2 As String
        Dim BankDetails1 As String
        Dim BankDetails2 As String
        Dim Brut As String
        Dim Detay1 As String
        Dim Detay2 As String
        Dim Iptal As Boolean
        Dim Net As String
        Dim SevkIrsaliyesi As String

        #End Region 'Fields

    End Structure

    Public Structure FaturaSeri

        #Region "Fields"

        Dim FaturaNotu As String
        Dim Iptal As Boolean
        Dim SeriNo As String
        Dim custNum As String
        Dim MusteriFaturaNo As String
        Dim Not1 As String
        Dim Not2 As String
        Dim Not3 As String
        Dim Not4 As String
        Dim Navlun As Decimal
        Dim Sigorta As Decimal
        Dim InvoiceNo As String
        #End Region 'Fields

    End Structure

    Public Structure IrsaliyeSeri

        #Region "Fields"

        Dim Carrier As String
        Dim Iptal As Boolean
        Dim IrsaliyeNo As String
        Dim Musteri As String
        Dim NavlunFaturaNo As String
        Dim Plaka As String
        Dim Plant As String
        Dim SeferNo As String
        Dim SevkNo As Double
        Dim TeslimEden As String
        Dim TeslimAlan As String
        Dim Transport As String
        Dim sNavlunNo As String
        Dim kutuAdedi As Decimal
        Dim paletAdedi As Decimal
        Dim Aciklama As String
        Dim FiiliSevkTarihi As DateTime
        Dim FiiliSevkSaati As String
        Dim Nakliyeci As String
        Dim BeyanNo As String
        Dim BeyanTarihi As DateTime

        #End Region 'Fields

    End Structure

    #End Region 'Nested Types

End Module