Imports CrystalDecisions.Shared

Public Class frmSatinAlmaIadeFaturasi2

#Region "Fields"

    Public gercekkur As Double

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Private dbMain As SaIadeFaturasi
    Dim ds As New DataSet
    Dim dt As DataTable
    Dim dtVendor As New DataTable
    Dim dVergiDegeri As Decimal
    Dim pcname As String

    'Dim yazdir As New Print("sql")
    Private sessionId As String
    Dim sHesap As String
    Dim sMasrafVergiKodu As String
    Dim ssorgu As String

#End Region 'Fields

#Region "Methods"

    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
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

    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub

    '
    Private Sub ApplyFilter1()
        Try
            '

            If cmdDocNum.Text = "" Then
                Exit Sub
            End If

            If cmbVoucher.Text = "" Then
                Exit Sub
            End If

            Dim db As New SaIadeFaturasi(My.Settings.ConnectionString)
            Dim ds As DataSet
            'txtVendCurrCode
            ds = db.loadRecords( _
                        cmbVendNum.Text, _
                        cmdDocNum.Text, _
                        sessionId, _
                        dtpFaturaTarihi.Value, _
                        pcname, _
                        cmbVoucher.Text)
            'kontrat fiyatı işaretli ise 0 satınalma işaretliyse 0 gönderilir
            'gcb no eklendi max 26 olacak şekilde.
            'gcb önce tur_apinvoice tablosuna insert ediliyor.
            'fiş oluşturua basıldığında tr_ithalat dosyasına insert ediliyor..

            Dim dt As DataTable

            dt = SelectDistinct(ds.Tables("TR_IadeFaturasiYukleSP"), "Currency")

            If dt.Rows.Count > 1 Then
                MessageBox.Show("Farklı Parabirimlerine Ait Sözleşmeler Mevcut Lütfen Kontrol Ediniz", "Hata")
                Clear()
                Exit Sub
            End If
            grdMain.DataSource = ds
            grdMain.DataMember = "TR_IadeFaturasiYukleSP"

            '
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ''
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ''degişiklik...
        If cmdDocNum.Text = "" Then
            MessageBox.Show("Lütfen Döküman Numarası Seçiniz..")
            Exit Sub
        End If

        If cmbVendNum.Text = "" Then
            MessageBox.Show("Lütfen Satıcı Seçiniz..")
            Exit Sub
        End If

        If cmbVoucher.Text = "" Then
            MessageBox.Show("Lütfen Fiş Seçiniz..")
            Exit Sub
        End If

        ApplyFilter1()
    End Sub

    Private Sub btnCreateVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click
        Try
            Dim sPonum As String = ""
            Dim iPoline As Integer
            Dim iPorelease As Integer
            Dim dPoReocrddate As DateTime
            Dim dPoTutar As Decimal

            Dim checkrows() As Janus.Windows.GridEX.GridEXRow

            checkrows = grdMain.GetCheckedRows

            If checkrows.Length = 0 Then Throw New Exception("Lütfen bir kayıt seçiniz...")

            If cmbVendNum.Text.Length = 0 Then Throw New Exception("Satıcı seçiniz")

            If cmbVoucher.Text.Length = 0 Then Throw New Exception("Fiş seçiniz")

            If txtFaturaNo.Text.Trim.Length = 0 Then Throw New Exception("Fatura No giriniz")

            If txtVergiKodu.Text.Trim.Length = 0 Then Throw New Exception("Vergi Kodu Seçiniz")

            If dtpFaturaTarihi.Text.Length = 0 Then Throw New Exception("Fatura Tarihini belirtiniz")

            If txtAciklama.Text.Length = 0 Then Throw New Exception("Açıklama Giriniz")
            '
            If CDec(txtStnTutar1.Text) <> CDec(txtStnTutar2.Text) Then Throw New Exception("Stn Tutar doğru değil")

            If CDec(txtVrgTutar1.Text) <> CDec(txtVrgTutar2.Text) Then Throw New Exception("Vrg Tutar doğru değil")

            If kontrol(, GroupBox1) = False Then
                Exit Sub
            End If

            dt = SelectDistinct(CType(grdMain.DataSource, DataSet).Tables(0), "Transdate")

            For Each row As DataRow In dt.Rows

                If DatePart(DateInterval.Month, CDate(row.Item("Transdate").ToString)) <> DatePart(DateInterval.Month, dtpFaturaTarihi.Value) Then

                    If MessageBox.Show("Fatura Tarihi ile Giriş Tarihi Arasında Uyumsuzluk Mevcut. Devam Etmek İstiyor musunuz?", "Hata", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then

                        Exit Sub

                    Else

                        GoTo Devam

                    End If

                End If

            Next

Devam:

            'frmFaturaSeri.ShowDialog()

            'If sFaturaSeri.Iptal = True Then

            '    btnAdd_Click(sender, e)

            '    Exit Sub

            'End If

            Dim sVergiDairesi, sVergiNo, sTermsCode As String

            ssorgu = " select Uf_TaxOffice, tax_reg_num1, terms_code" & _
                              " from vendor" & _
                              " Where vend_num=" & sTirnakEkle(cmbVendNum.Text)

            dt = db.RunSql(ssorgu)

            GetRowInfo(sVergiDairesi, dt, 0, "Uf_TaxOffice")
            GetRowInfo(sVergiNo, dt, 0, "tax_reg_num1")
            GetRowInfo(sTermsCode, dt, 0, "terms_code")

            dbAccess.RunSql("Delete From ANSIADEFATURA")

            sessionId = System.Guid.NewGuid.ToString
            Dim ParaBirimi As String = String.Empty

            For Each row As Janus.Windows.GridEX.GridEXRow In checkrows

                With row

                    ssorgu = " Insert Into ANSIADEFATURA " & _
                                "(" & _
                                " FaturaNo, FaturaTarihi, SaticiNo, SaticiAdi, " & _
                                " Malzeme, Tanim, OlcuBirimi, Miktar, " & _
                                " BirimFiyat, Tutar, ParaBirimi, Vergi, " & _
                                " VergiDairesi, VergiNo, IrsaliyeNo " & _
                                ")"
                    ssorgu = ssorgu & " Values (" & _
                                sTirnakEkle(txtFaturaNo.Text) & "," & _
                                sTirnakEkle(dtpFaturaTarihi.Value.ToString("yyyy-MM-dd")) & "," & _
                                sTirnakEkle(cmbVendNum.Text) & "," & _
                                sTirnakEkle(txtVendName.Text) & "," & _
                                sTirnakEkle(.Cells("Item").Text) & "," & _
                                sTirnakEkle(.Cells("Description").Text) & "," & _
                                sTirnakEkle(.Cells("UM").Text) & "," & _
                                CDec(.Cells("Qty").Text) & "," & _
                                CDec(.Cells("UnitPrice").Text) & "," & _
                                CDec(.Cells("Amount").Text) & "," & _
                                sTirnakEkle(.Cells("Currency").Text) & "," & _
                                dVergiDegeri & "," & _
                                sTirnakEkle(sVergiDairesi) & "," & _
                                sTirnakEkle(sVergiNo) & "," & _
                                sTirnakEkle(.Cells("DocNum").Text) & _
                             ")"

                    dbAccess.RunSql(ssorgu)

                    ParaBirimi = .Cells("Currency").Text

                End With

            Next
            Dim Yazi As String = String.Empty
            Dim Kurus As String = String.Empty

            If ParaBirimi = "YTL" Or ParaBirimi = "TL" Then

                ParaBirimi = "TL"
                Kurus = "KRS"

            ElseIf ParaBirimi = "EU" OrElse ParaBirimi = "EUR" Then

                ParaBirimi = "EUR"
                Kurus = "CENT"

            ElseIf ParaBirimi = "USD" OrElse ParaBirimi = "US" Then

                ParaBirimi = "USD"
                Kurus = "CENT"

            End If

            Yazi = SayiYazOndalikli(CDec(txtStnTutar1.Text) + CDec(txtVrgTutar1.Text), ParaBirimi, Kurus)

            Dim paramFields As New ParameterFields
            paramFields = AddParameter("@aciklama", txtAciklama.Text, paramFields)

            RaporCagir("TRSaticiIadeFatura.rpt", , Yazi, "fatura", False, False, , , paramFields)

            If MessageBox.Show("Fatura Doğru Yazdırıldı mı?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then

                Exit Sub

            End If

            Dim tmpPoNum As String = ""
            Dim sayac As Integer = 0
            For Each row As Janus.Windows.GridEX.GridEXRow In checkrows

                sayac = sayac + 1

                With row

                    ssorgu = " select ref_num, ref_line_suf, ref_release, po.RecordDate" & _
                                " from matltran" & _
                                " Left Join po" & _
                                " On po.po_num=matltran.ref_num" & _
                            " Where trans_num=" & row.Cells("TransNum").Value

                    dt = db.RunSql(ssorgu)

                    GetRowInfo(sPonum, dt, 0, "ref_num")
                    GetRowInfo(iPoline, dt, 0, "ref_line_suf")
                    GetRowInfo(iPorelease, dt, 0, "ref_release")
                    GetRowInfo(dPoReocrddate, dt, 0, "RecordDate")

                    ssorgu = "Insert Into tt_voucher" & _
                                " ( connection_id, type, po_num, po_line, po_release, item," & _
                                " item_description, terms_code, u_m, qtu_qty_received," & _
                                " qtu_qty_voucher, cpr_item_cost, cpr_item_cost_conv," & _
                                " cpr_plan_cost_conv, PoRecordDate )" & _
                                " Values (" & _
                                sTirnakEkle(sessionId) & "," & vbCrLf & _
                                sTirnakEkle("M") & "," & vbCrLf & _
                                sTirnakEkle(sPonum) & "," & vbCrLf & _
                                iPoline & "," & vbCrLf & _
                                iPorelease & "," & vbCrLf & _
                                sTirnakEkle(.Cells("Item").Text) & "," & vbCrLf & _
                                sTirnakEkle(.Cells("Description").Text) & "," & vbCrLf & _
                                sTirnakEkle(sTermsCode) & "," & vbCrLf & _
                                sTirnakEkle(.Cells("UM").Text) & "," & vbCrLf & _
                                CDec(.Cells("Qty").Text) & "," & vbCrLf & _
                                CDec(.Cells("Qty").Text) & "," & vbCrLf & _
                                CDec(.Cells("UnitPrice").Text) & "," & vbCrLf & _
                                CDec(.Cells("UnitPrice").Text) & "," & vbCrLf & _
                                CDec(.Cells("UnitPrice").Text) & "," & vbCrLf & _
                                sTirnakEkle(dPoReocrddate.ToString("yyyy-MM-dd")) & ")"

                    db.RunSql(ssorgu, True)

                    'dPoTutar = dPoTutar + _
                    '        (CDec(.Cells("Qty").Text) * _
                    '         CDec(.Cells("UnitPrice").Text))

                    dPoTutar = dPoTutar + CDec(.Cells("Amount").Text)

                    dPoTutar = Math.Round(dPoTutar, 2)

                    If sayac = 1 Then
                        tmpPoNum = sPonum
                    End If

                    If tmpPoNum <> sPonum Then

                        ssorgu = "Insert Into tt_voucher" & _
                                  " ( connection_id, type, po_num," & _
                                  " qtu_qty_received," & _
                                  " qtu_qty_voucher, cpr_item_cost, cpr_item_cost_conv," & _
                                  " cpr_plan_cost_conv, tax_system, tax_code, amt_tax_basis, " & _
                                  " amt_tax_amount , acct )" & _
                                  " Values (" & _
                                  sTirnakEkle(sessionId) & "," & vbCrLf & _
                                  sTirnakEkle("T") & "," & vbCrLf & _
                                  sTirnakEkle(tmpPoNum) & "," & vbCrLf & _
                                  "0" & "," & vbCrLf & _
                                  "0" & "," & vbCrLf & _
                                  "0" & "," & vbCrLf & _
                                  "0" & "," & vbCrLf & _
                                  "0" & "," & vbCrLf & _
                                  "1" & "," & vbCrLf & _
                                  sTirnakEkle(txtVergiKodu.Text) & "," & vbCrLf & _
                                  dPoTutar & "," & vbCrLf & _
                                  FormatNumber(dPoTutar / 100 * dVergiDegeri, 2) & "," & vbCrLf & _
                                  sTirnakEkle(sHesap) & _
                                  ")"

                        db.RunSql(ssorgu, True)

                        dPoTutar = 0

                    End If

                End With

                tmpPoNum = sPonum

            Next row

            ssorgu = "Insert Into tt_voucher" & _
                            " ( connection_id, type, po_num," & _
                            " qtu_qty_received," & _
                            " qtu_qty_voucher, cpr_item_cost, cpr_item_cost_conv," & _
                            " cpr_plan_cost_conv, tax_system, tax_code, amt_tax_basis, " & _
                            " amt_tax_amount , acct )" & _
                            " Values (" & _
                            sTirnakEkle(sessionId) & "," & vbCrLf & _
                            sTirnakEkle("T") & "," & vbCrLf & _
                            sTirnakEkle(tmpPoNum) & "," & vbCrLf & _
                            "0" & "," & vbCrLf & _
                            "0" & "," & vbCrLf & _
                            "0" & "," & vbCrLf & _
                            "0" & "," & vbCrLf & _
                            "0" & "," & vbCrLf & _
                            "1" & "," & vbCrLf & _
                            sTirnakEkle(txtVergiKodu.Text) & "," & vbCrLf & _
                            dPoTutar & "," & vbCrLf & _
                            Math.Round(dPoTutar / 100 * dVergiDegeri, 2) & "," & vbCrLf & _
                            sTirnakEkle(sHesap) & _
                            ")"

            db.RunSql(ssorgu, True)

            Dim voucher As Integer
            dbMain = New SaIadeFaturasi(My.Settings.ConnectionString)
            '
            'Begin Trans

            '
            'Prepare Print
            If Not preparePrint() Then Throw New Exception("Hata oluştu!")
            '
            'Fiş oluşturuluyor
            If Not dbMain.FisOlustur( _
                        cmbVendNum.Text, _
                        cmbVoucher.Text, _
                        dtpFaturaTarihi.Value.ToString("yyyy-MM-dd"), _
                        txtStnTutar1.Text, _
                        txtVrgTutar1.Text, _
                        txtFaturaNo.Text, _
                        dtpFaturaTarihi.Value.ToString("yyyy-MM-dd"), _
                        sTermsCode, _
                        KullaniciAdi, _
                        sessionId, _
                        voucher, _
                        "") Then Throw New Exception("Hata Oluştu" & vbNewLine & dbMain.Result(0).ToString)
            '

            Try

                db.BeginTransaction()

                For Each row As Janus.Windows.GridEX.GridEXRow In checkrows

                    ssorgu = " Update Matltran" & _
                            " Set Uf_MatlvoucherNo=" & sTirnakEkle(cmbVoucher.Text) & _
                            " Where Trans_Num=" & row.Cells("transnum").Text

                    db.RunSql(ssorgu)

                Next row

                db.CommitTransaction()

            Catch ex As Exception

                db.RollbackTransaction()

                Throw ex

            End Try

            'Bilgiler ApInvoice tablosundan siliniyor
            If Not dbMain.EmptyApInvoice(sessionId) Then Throw New Exception("Hata Oluştu")
            '
            ''''TR_ithalat_dosya insert

            'Show message
            MsgBox("Fiş oluşturuldu [ " & voucher.ToString & " ] ", MsgBoxStyle.Information, "Bilgi")
            '
            grdMain.DataSource = Nothing
            txtAciklama.Text = ""
            txtStnTutar1.Value = 0
            txtStnTutar2.Value = 0

            txtFaturaNo.Text = ""

            txtVendName.Text = ""
            txtVrgTutar1.Value = 0
            txtVrgTutar2.Value = 0

            'Transaction tamamlanıyor

            '
            'Tekrar filtreleniyor
            ApplyFilter1()
            '
            Clear()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)

        End Try
    End Sub

    Private Sub btnTemizle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemizle.Click
        Clear()
        cmbVendNum.Text = ""
        txtVendName.Text = ""
        dtpFaturaTarihi.Value = Now.Date

        pcname = My.Computer.Name

        ssorgu = " delete " & _
                    " from TR_IadeFaturasi" & _
                    " where PcName='" & pcname & "'"

        db.RunSql(ssorgu)
    End Sub

    '
    Private Sub calculate()
        '
        Dim total As Decimal = 0
        Dim taxTotal As Decimal = 0
        '
        For i As Integer = 0 To grdMain.RowCount - 1
            If grdMain.GetRow(i).Cells(0).Value = True Then
                total += Val(grdMain.GetRow(i).Cells("Amount").Value())
                taxTotal += Val(grdMain.GetRow(i).Cells("TaxAmount").Value())

            End If
        Next
        '
        txtStnTutar2.Text = Math.Round(total, 2).ToString("###,###.#0")
        'txtVrgTutar2.Text = Math.Round(taxTotal, 2).ToString("###,###.#0")
        txtVrgTutar2.Text = Math.Round(total / 100 * dVergiDegeri, 2).ToString("###,###.#0")
    End Sub

    '
    Private Sub Clear()
        '
        dbMain = New SaIadeFaturasi(My.Settings.ConnectionString)
        '
        'Bilgiler ApInvoice tablosundan siliniyor
        If Not dbMain.EmptyApInvoice(sessionId) Then Throw New Exception("Hata Oluştu")
        '
        txtStnTutar1.Value = 0
        txtStnTutar2.Value = 0
        txtVrgTutar1.Value = 0
        txtVrgTutar2.Value = 0
        '
        txtFaturaNo.ResetText()
        txtAciklama.ResetText()
        '
        cmdDocNum.Text = ""
        cmdDocNum.DataSource = Nothing
        grdMain.DataSource = Nothing
        dtpFaturaTarihi.Value = Date.Now
    End Sub

    Private Sub cmbVendNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendNum.ButtonClick
        ssorgu = ""
        ssorgu = "SELECT vend_num As [Satıcı No] , (SELECT [Name] " & _
                                                    " FROM vendAddr " & _
                                                    " WHERE vendAddr.Vend_Num = vendor.vend_Num) AS [Satıcı Adı]" & _
                 " FROM vendor " & _
                 " ORDER BY vend_num"

        FindFormCagir(ssorgu, "Satıcı No", "Satıcı Adı", cmbVendNum.Text, txtVendName.Text)
    End Sub

    Private Sub cmbVendNum_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendNum.TextChanged, cmbVendNum.Leave
        Try

            If cmbVendNum.Text = "" Then
                Exit Sub
            End If

            Clear()

            'btnTemizle_Click(sender, e)

            'txtVendCurrCode
            If CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'").Length = 0 Then
                txtVendName.Text = ""
            Else
                txtVendName.Text = CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'")(0)(1)
            End If

            'txtVendInv.Text = CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Value & "'")(0)("InvVend")
            If CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'").Length = 0 Then
                sMasrafVergiKodu = ""
            Else
                sMasrafVergiKodu = CType(dtVendor, DataTable).Select("vend_Num = '" & cmbVendNum.Text & "'")(0)("Tax_Code1")
            End If
            'txtVrgTutar2
            'GetCurrCode()
            '' irsaliye nolar yükleniyor
            'daDocNum.SelectCommand.Parameters("@VendNum").Value = cmbVendNum.Value
            'DsDocNum1.TUR_ApGetDocNumsSp.Rows.Clear()
            ''
            'daDocNum.Fill(DsDocNum1)
            '
            ssorgu = " exec TR_IadeIrsGetDocNum '" & cmbVendNum.Text & "'"
            dt = db.RunSql(ssorgu)
            Clear()
            cmdDocNum.DataSource = dt

            Dim SQL As String = _
                  "select Distinct voucher" + vbCrLf + _
                        "from po p" + vbCrLf + _
                        "Left Join po_vch v On v.po_num=p.po_num" + vbCrLf + _
                        "Where v.type='V'" + vbCrLf + _
                        " And p.vend_num=" + sTirnakEkle(cmbVendNum.Text)

            dt = db.RunSql(SQL)

            cmbVoucher.DataSource = dt

            ApplyFilter1()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub cmdDocNum_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDocNum.Leave
        Try
            Me.Cursor = Cursors.WaitCursor

            If cmdDocNum.Text.Length <> 0 Then
                '
                ssorgu = " select Distinct po_vch.voucher" & _
                            " from matltran mt" & _
                            " left join po_vch  on  po_vch.po_num = mt.ref_num and po_vch.po_line = mt.ref_line_suf " & _
                              " and po_vch.po_release = mt.ref_release " & _
                            " where mt.document_num='" & cmdDocNum.Text & "'" & _
                                " and mt.uf_matlvoucherno='0'" & _
                                " and isnull(po_vch.voucher,0) <> 0 " & _
                                " and po_vch.type='V'"

                dt = db.RunSql(ssorgu)

                cmbVoucher.DataSource = dt

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub frmSatinAlmaFaturasi_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Clear()
    End Sub

    Private Sub frmSatinAlmaFaturasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            pcname = My.Computer.Name
            ssorgu = " delete from TR_IadeFaturasi" & _
                     " where PcName='" & pcname & "'"
            db.RunSql(ssorgu)

            sessionId = System.Guid.NewGuid.ToString
            Me.Cursor = Cursors.WaitCursor
            ssorgu = ""
            ssorgu = "SELECT vend_num, (SELECT [Name] " & _
                                            " FROM vendAddr " & _
                                            " WHERE vendAddr.Vend_Num = vendor.vend_Num) AS VendName," & _
                        " ISNULL(Uf_InvVendNum, '') AS InvVend, curr_code AS VendCurrCode," & _
                        " ISNULL((SELECT Curr_Code FROM Vendor v WHERE v.Vend_Num = Vendor.Uf_InvVendNum), '') AS InvVendCurrCode ," & _
                        " Tax_Code1 " & _
                     " FROM vendor ORDER BY vend_num"
            dt = Nothing
            dt = db.RunSql(ssorgu)

            dtVendor = dt
            dtpFaturaTarihi.Value = Date.Now

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub grdMain_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMain.CellEdited
        Try
            If e.Column.DataMember = "UnitPrice" Then
                Dim row As Janus.Windows.GridEX.GridEXRow

                row = CType(sender, Janus.Windows.GridEX.GridEX).GetRow

                ssorgu = " Update TR_IadeFaturasi" & _
                            " Set UnitPrice=" & row.Cells("UnitPrice").Text & _
                            " , Amount = Qty * " & row.Cells("UnitPrice").Text & _
                            " , TaxAmount = Qty * " & row.Cells("UnitPrice").Text & " * " & "0." & row.Cells("TaxRate").Text & _
                            " Where Transnum=" & CLng(row.Cells("TransNum").Text)

                db.RunSql(ssorgu, True)

                btnAdd_Click(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show(ex.ToString, "Ekip Mapics", MessageBoxButtons.OK)

        End Try
    End Sub

    Private Sub grdMain_CellValueChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdMain.CellValueChanged, grdMain.ColumnHeaderClick
        Try
            Dim sel As Integer = 0
            For i As Integer = 0 To grdMain.RowCount - 1
                If grdMain.GetRow(i).Cells(0).Value = True Then
                    sel += 1
                End If
            Next
            '
            stBar.Panels(1).Text = sel & " kayıt seçildi "
            '
            'tutarlar hesaplanıyor
            calculate()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub grdMain_RowCountChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMain.RowCountChanged
        stBar.Panels(0).Text = " Listede " & grdMain.RowCount & " kayıt var "
    End Sub

    '
    Private Function preparePrint() As Boolean
        '
        Try
            'Dim db As New SaFatura(ConnectionString)
            '
            For i As Integer = 0 To grdMain.RowCount - 1
                If grdMain.GetRow(i).Cells(0).Value = True Then
                    If Not (dbMain.updateRecords(sessionId, _
                                grdMain.GetRow(i).Cells("transnum").Value, _
                                grdMain.GetRow(i).Cells("Amount").Value)) Then
                        '
                        Throw New Exception("Tutarlar güncelleştirilirken hata oluştu")
                        '
                    End If
                End If
            Next
            '
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Sub txtVergi_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVergiKodu.ButtonClick
        ssorgu = ""
        ssorgu = "SELECT tax_code As [Vergi Kodu] , tax_rate [Vergi Değeri] , Ap_Acct [Hesap] " & _
                 " FROM taxcode " & _
                 " ORDER BY tax_code"

        FindFormCagir(ssorgu, "Vergi Kodu", "Vergi Değeri", txtVergiKodu.Text, dVergiDegeri, sHesap, "Hesap")

        txtVrgTutar2.Text = Math.Round(CDec(txtStnTutar2.Text) / 100 * dVergiDegeri, 2).ToString("###,###.#0")
    End Sub

    Private Function ValidationNumeric(ByVal tip As TextBox) As Boolean
        Try

            If IsNumeric(tip.Text) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ValidationNumericEditbox(ByVal tip As Janus.Windows.GridEX.EditControls.EditBox) As Boolean
        Try

            If IsNumeric(tip.Text) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region 'Methods

#Region "Other"

    '

#End Region 'Other

End Class