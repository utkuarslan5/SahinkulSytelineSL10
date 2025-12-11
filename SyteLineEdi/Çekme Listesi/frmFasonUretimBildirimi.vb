Imports System.Data.SqlClient


Public Class frmFasonUretimBildirimi

    'Dim db As New Core.DataForDB2(My.Settings.AccessConnection)

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dt, dt2 As New DataTable
    Dim dtOper As New DataTable
    Dim sBeforeOper As String
    Dim sNextOper As String
    Dim dMiktar As Decimal
    Dim sStr, sSQL, sQuery As String
    Dim sWhse As String
    Dim fasonerAdi As String
    Dim irsaliyeNo As String = ""

    Private Sub txtItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyDown

        Dim Malzeme As String = String.Empty

        Dim Isemri As String = String.Empty

        If e.KeyCode = Keys.Enter Then
            txtItem.Text = txtItem.Text.Replace(".", "/")
            txtItem.Text = txtItem.Text.Replace("ç", ".")
            txtItem.Text = txtItem.Text.Replace("Ç", ".")
            txtItem.Text = txtItem.Text.Replace("ö", ",")
            txtItem.Text = txtItem.Text.Replace("Ö", ",")
            txtItem.Text = txtItem.Text.Replace("*", "-")


            Cursor.Current = Cursors.WaitCursor

            Try
                If txtItem.Text.Trim.Length > 0 Then

                    If txtItem.Text.Contains("%") Then

                        Malzeme = txtItem.Text.Split("%")(0)
                        Isemri = txtItem.Text.Split("%")(1)
                        txtItem.Text = Malzeme
                        txtJob.Text = Isemri
                        'txtIrsaliyeNo.Text = Isemri

                        sStr = "select * from job where job='" & Isemri & "'"
                        txtdesc.Text = db.RunSql(sStr).Rows(0)("description")
                        'txtBasvuru.Text = db.RunSql(sStr).Rows(0)("Uf_BAsvuru")

                        sStr = "Exec Tr_Edi_Uretim_Bildirimi_Load @Job=" & sTirnakEkle(Isemri) & ""
                        dtOper = db.RunSql(sStr)

                        cmbOperNo.DataSource = dtOper

                        If dtOper.Rows.Count = 1 Then
                            cmbOperNo.Text = dtOper.Rows(0)("oper_num")
                            Call txtJob_KeyDown(sender, New KeyEventArgs(Keys.Enter))
                            'Call cmbOperNo_ValueChanged(sender, e)
                            'cmbOperNo.ValueMember = dtOper.Rows(0)("oper_num")
                        ElseIf dtOper.Rows.Count > 1 Then

                        End If

                        cmbOperNo.Focus()

                    End If

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub cmbOperNo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOperNo.ValueChanged, cmbOperNo.Leave
        Dim sFilter As String
        Dim foundRows() As DataRow = Nothing

        'txtYer.Text = cmbOperNo.Value

        If dtOper.Rows.Count > 0 Then
            sFilter = "job='" & txtJob.Text & "' And " & " oper_num='" & IIf(cmbOperNo.Text = "", 0, cmbOperNo.Text) & "'"

            foundRows = dtOper.Select(sFilter)


            If foundRows.Length > 0 Then
                sBeforeOper = foundRows(0).ItemArray(6).ToString
                sNextOper = foundRows(0).ItemArray(7).ToString
                dMiktar = foundRows(0).ItemArray(5).ToString
                'txtYer.Text = foundRows(0).ItemArray(8).ToString
            Else
                sBeforeOper = String.Empty
                sNextOper = String.Empty
                dMiktar = 0
                'txtYer.Text = String.Empty
            End If
        Else
            sBeforeOper = String.Empty
            sNextOper = String.Empty
            dMiktar = 0
            '  txtYer.Text = String.Empty
        End If

    End Sub

    Private Sub btnTemizle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemizle.Click

        edtTedarikci.Text = String.Empty
        txtItem.Text = String.Empty
        txtJob.Text = String.Empty
        txtJobItem.Text = String.Empty
        'txtYer.Text = String.Empty
        txtLot.Text = String.Empty
        cmbOperNo.Text = String.Empty
        cmbOperNo.Value = String.Empty
        txtQty.Text = 0
        txtdesc.Text = ""
        cmbOperNo.DataSource = Nothing
        sBeforeOper = String.Empty
        sNextOper = String.Empty
        txtJobItem.Text = ""
        'txtBasvuru.Text = ""
        dMiktar = 0
        'cmbreasoncode.Text = String.Empty
        txtNot.Text = ""
        txtIrsaliye.Text = String.Empty

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            If CInt(txtQty.Text) = 0 Then
                MessageBox.Show("Miktar 0'dan farklı olmalıdır.")
                Exit Sub
            End If

            If txtYer.Text = "GSEVK1" Then
                MessageBox.Show("Giriş yeri GSEVK1 olamaz.")
                Exit Sub
            End If

            'If CDec(txtQty.Text) > CDec(txtjobBakiye.Text) Then
            '    MessageBox.Show("Üretim miktarı bakiyeden büyük olamaz.")
            '    Exit Sub
            'End If

            If txtYer.Text = String.Empty Then Throw New Exception("Yer Tanımsız")
            'If txtLot.Text = String.Empty Then Throw New Exception("Lot Tanımsız")
            If txtItem.Text = String.Empty Then Throw New Exception("Malzeme Tanımsız")
            'If txtQty.Text > dMiktar Then Throw New Exception("Miktar Gerekenden Büyük")
            If txtIrsaliye.Text = String.Empty Then Throw New Exception("İrsaliye No Giriniz")

            irsaliyeNo = ""


            If txtIrsaliye.Text.Length > 12 Then
                irsaliyeNo = txtIrsaliye.Text.Substring(0, 7) & txtIrsaliye.Text.Substring(txtIrsaliye.Text.Length - 5, 5)
            Else
                irsaliyeNo = txtIrsaliye.Text
            End If

            Try
                Dim sorgu As String = "SELECT * " &
                                        " FROM job " &
                                        " WHERE job = " & sTirnakEkle(txtJob.Text) & " AND (qty_complete > qty_released OR stat = 'C')"

                If (db.RunSql(sorgu).Rows.Count > 0) Then
                    MessageBox.Show("İş emri / işlem kapalıdır.", "Uyarı")
                    Exit Sub
                End If

                sorgu = "SELECT * FROM jobroute WHERE complete = 1 AND oper_num=" & cmbOperNo.Text & " and job = " & sTirnakEkle(txtJob.Text)
                If (db.RunSql(sorgu).Rows.Count > 0) Then
                    MessageBox.Show("İş emri / işlem kapalıdır.", "Uyarı")
                    Exit Sub
                End If

                sorgu = "SELECT * FROM item WHERE lot_tracked <> 1 AND item =" & sTirnakEkle(txtItem.Text)
                If (db.RunSql(sorgu).Rows.Count > 0) Then
                    MessageBox.Show("Malzeme lot denetimsizdir.", "Uyarı")
                    Exit Sub
                End If

                Dim tolerans, maxMiktar, toleransMik, siparisMik As Decimal

                sStr = "select buyer from item where item = '" & txtItem.Text & "'"
                dt = db.RunSql(sStr)

                If dt.Rows(0)(0).ToString.Trim = "PMETAL" Or _
                    dt.Rows(0)(0).ToString.Trim = "PMETALK" Or _
                    dt.Rows(0)(0).ToString.Trim = "PFASON11" Or _
                    dt.Rows(0)(0).ToString.Trim = "PFASON2" Or _
                    dt.Rows(0)(0).ToString.Trim = "PFASON3" Or _
                    dt.Rows(0)(0).ToString.Trim = "PFASON4" Or _
                    dt.Rows(0)(0).ToString.Trim = "PFASON5" Or _
                    dt.Rows(0)(0).ToString.Trim = "PFASON21" Then
                    tolerans = 100
                Else
                    tolerans = 5
                End If

                sorgu = "Select (qty_released-qty_complete) as fark, qty_released, qty_complete from job  WHERE job = " & sTirnakEkle(txtJob.Text) & ""
                dt = db.RunSql(sorgu)

                If dt.Rows.Count > 0 Then
                    'maxMiktar = dt.Rows(0)("qty_released") * 1.1

                    siparisMik = dt.Rows(0)("qty_released")
                    maxMiktar = (siparisMik * tolerans / 100) + siparisMik

                    toleransMik = maxMiktar - (dt.Rows(0)("qty_complete"))

                End If

                'If (db.RunSql(sorgu).Rows(0)("fark")) < txtQty.Text Then
                If toleransMik < txtQty.Text Then
                    'If MessageBox.Show(" Girilen Miktar İşemri Miktarından Fazla. Devam Etmek İstiyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    MessageBox.Show("Açık Miktar Yetersiz. İşlem Yapılamıyor (Maksimum Miktar='" & toleransMik & "')")
                    Exit Sub
                    'End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Dim p As New ArrayList

            p.Add(New SqlParameter("@item", txtItem.Text))
            p.Add(New SqlParameter("@loc", txtYer.Text))
            p.Add(New SqlParameter("@lot", IIf(txtLot.Text.Trim = "", irsaliyeNo, txtLot.Text)))
            'p.Add(New SqlParameter("@whse", sWhse))
            ' p.Add(New SqlParameter("@ReturnMessage", txtLot.Text))

            Dim p2 As New SqlClient.SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 4000)

            p2.Direction = ParameterDirection.Output
            p.Add(p2)

            Dim sonuc As Integer = db.RunSp("TRM_Stok_Kontrol_SL9", p, 1, False)

            If Not (IsDBNull(p2.Value) Or p2.Value Is Nothing) AndAlso p2.Value.ToString.Trim <> String.Empty Then
                MessageType = enumMessageType.GirisKontrol
                Throw New Exception(p2.Value.ToString & " !")
                'Return
            End If
            'If (sonuc = -1) Then
            '    MessageBox.Show("Hata oluştu")
            'End If

            'If Not (IsDBNull(p2.Value) Or p2.Value Is Nothing) Then
            '    MessageBox.Show(p2.Value.ToString)
            '    Return
            'End If



            sStr = "Exec Tr_Edi_Uretim_Bildirimi_Load3 @Job='" & txtJob.Text & "'"
            dt = db.RunSql(sStr)

            Dim stokkontrol As Boolean

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each rowDt As DataRow In dt.Rows
                    Try

                        sStr = " Exec TRM_Bilesen_StokSP " &
                      "@job=" & sTirnakEkle(txtJob.Text) & "," &
                      "@Oper_num=" & rowDt.Item("oper_num") & "," &
                      "@qty=" & txtQty.Text

                        db.RunSqlSp(sStr)
                        stokkontrol = False

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        MessageBox.Show("Üretim bildirimi yapılamamıştır")
                        stokkontrol = True

                        Exit Sub
                    End Try

                Next

            End If


            If stokkontrol = False Then
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each rowDt As DataRow In dt.Rows
                        Try

                            sStr = " Exec TR_Bilesen_StokSP2 " &
                          "@job=" & sTirnakEkle(txtJob.Text) & "," &
                          "@Oper_num=" & rowDt.Item("oper_num") & "," &
                          "@qty=" & txtQty.Text

                            db.RunSqlSp(sStr)


                        Catch ex As Exception
                            MessageBox.Show(ex.Message)

                            'MessageBox.Show("Üretim bildirimi yapılamamıştır")
                            'stokkontrol = False
                            ' Exit Sub
                        End Try

                    Next

                End If

            End If
            stokkontrol = True


            btnSend.Enabled = False

            sStr = " Exec Tr_UretimBildirimi_SL9 " &
                        "@Job=" & sTirnakEkle(txtJob.Text) & "," &
                        "@Oper_num=" & cmbOperNo.Text & "," &
                        "@Loc=" & sTirnakEkle(txtYer.Text) & "," &
                        "@Lot=" & sTirnakEkle(IIf(txtLot.Text = "", irsaliyeNo, txtLot.Text)) & "," &
                        "@qty_complete=" & txtQty.Text & "," &
                        "@NextOper=" & sNextOper & "," &
                        "@emp_num = '" & KullaniciAdi & "' , " &
                        "@weight=0," &
                        "@FireNedeni=''," &
                        "@DurusNedeni=''," &
                        "@DurusSuresi=0," &
                        "@Kaynak=''," &
                        "@trans_date=" & sTirnakEkle(dtpHareketTar.Value.ToString("yyyy-MM-dd"))
            '"@ReasonCode='" & cmbreasoncode.Text & "'," &
            '"@BackFlush =0" & "," &
            '"@UF_Makara_Sayisi =0" & "," & _
            '"@trans_date=" & sTirnakEkle(dtpHareketTar.Value.ToString("yyyy-MM-dd"))

            db.RunSql(sStr)

            sStr = "declare @p10 tinyint " & _
            "set @p10=0 " & _
            "declare @p11 nvarchar(2800) " & _
            "set @p11=1 " & _
            "declare @p12 nvarchar(2800) " & _
            "set @p12=NULL " & _
            "declare @p13 nvarchar(2800) " & _
            "set @p13=N'0 receivers created.' " & _
            "declare @p14 nvarchar(2800) " & _
            "set @p14=N' ' " & _
            "exec [dbo].[TR_RSQC_CreateIPReceiverSp] @i_QtyReceived=0,@i_Whse=N'MAIN', " & _
            "@i_Job=N'" & txtJob.Text & "',@i_Suffix=0,@i_OperNum=0,@i_PSNum=NULL, " & _
            "@i_CallingFunction=N'RS_QCCreateIPReceiver',@i_UserCode=N'sa', " & _
            "@firstarticleonly=0,@o_PopUp=@p10 output,@o_PrintTag=@p11 output,@o_RcvrNums=@p12 output,@o_Messages=@p13 output,@Infobar=@p14 output " & _
            "select @p10, @p11, @p12, @p13, @p14 " & _
            "UPDATE RS_QCRCVR SET qc_lot ='" & irsaliyeNo & "',qty_received=" & txtQty.Text & " WHERE rcvr_num =REPLACE(@p12,',','') "

            db.RunSql(sStr)


            btnSend.Enabled = True

            sStr = "Exec Tr_Edi_Uretim_Bildirimi_Load2 @Job='" & txtJob.Text & "', @Opernum='" & cmbOperNo.Text & "'"
            dt = db.RunSql(sStr)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For Each rowDt As DataRow In dt.Rows
                    OperNumKontrol(rowDt.Item("job").ToString, rowDt.Item("oper_num").ToString, rowDt.Item("NextOper").ToString, rowDt.Item("BeforeOper").ToString)
                Next
            End If


            sStr = "UPDATE matltran" &
                        " SET  reason_code='" & cmbreasoncode.Text & "', document_num=" & sTirnakEkle(irsaliyeNo) & _
                    " WHERE trans_type+ ref_type in ('FJ','NJ')" &
                        " AND ref_num=" & sTirnakEkle(txtJob.Text) &
                        " AND document_num IS NULL" &
                        " AND ref_release=" & sTirnakEkle(cmbOperNo.Text) & ""

            db.RunSql(sStr)

            If chketiketyazdir.Checked = True Then
                'sStr = "Select item, description, '" & txtJob.Text & "'  as isemri, getdate() , (SELECT whse FROM job WHERE job= '" & txtJob.Text & "') AS MAIN ,'" & txtQty.Text & "' as miktar from item where item='" & txtItem.Text & "'"
                sStr = " Select item,description,'" & txtQty.Text & "' as qty_on_hand,'" & IIf(txtLot.Text = "", irsaliyeNo, txtLot.Text) & "' as lot,'" & txtYer.Text & "' as Loc  " & _
                    "  ,u_m , getdate() as RecordDate,'" & txtJob.Text & "' as isemri,(SELECT whse FROM job WHERE job= '" & txtJob.Text & "') AS MAIN,'" & irsaliyeNo & "' as Irsaliye  from item where item='" & txtItem.Text & "'"
                dt = db.RunSql(sStr)
                RaporCagir("FJ_Etiket.rpt", "", "", "", True, True, "", dt) '
            End If

            'btnTemizle_Click(Me, e)
            txtItem.Text = String.Empty
            txtJob.Text = String.Empty
            ' txtYer.Text = String.Empty
            txtJobItem.Text = String.Empty
            txtLot.Text = String.Empty
            'txtIrsaliye.Text = String.Empty
            cmbOperNo.Text = String.Empty
            cmbOperNo.Value = String.Empty
            txtQty.Text = 0
            txtdesc.Text = ""
            cmbOperNo.DataSource = Nothing
            sBeforeOper = String.Empty
            sNextOper = String.Empty
            dMiktar = 0
            'txtBasvuru.Text = ""
            txtjobMiktar.Text = ""
            txtjobBakiye.Text = ""
            'cmbreasoncode.Text = String.Empty
            txtNot.Text = ""


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            btnSend.Enabled = True
        End Try

    End Sub

    Private Sub OperNumKontrol(ByVal newjob As String, ByVal newOperNum As String, ByVal NextOperNum As String, ByVal BeforeOperNum As String)

        sNextOper = NextOperNum


        sStr = " Exec Tr_UretimBildirimi " &
                    "@Job='" & newjob & "'," &
                    "@Oper_num=" & newOperNum & "," &
                    "@Loc=" & sTirnakEkle(txtYer.Text) & "," &
                    "@Lot=" & sTirnakEkle(IIf(txtLot.Text = "", irsaliyeNo, txtLot.Text)) & "," &
                    "@qty_complete=" & txtQty.Text & "," &
                    "@NextOper=" & sNextOper & "," &
                    "@ReasonCode='" & cmbreasoncode.Text & "'," &
                    "@BackFlush =1" & "," &
                    "@UF_Makara_Sayisi=0"


        db.RunSql(sStr)
    End Sub


    Private Sub txtJob_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJob.KeyDown
        Dim Malzeme As String = String.Empty

        Dim Isemri As String = String.Empty

        If e.KeyCode = Keys.Enter Then
            txtdesc.Text = txtItem.Text.Replace("'", " ")


            Cursor.Current = Cursors.WaitCursor

            Try
                If txtJob.Text.Trim.Length > 0 Then

                    If Len(txtJob.Text) < 10 Then

                        Dim frtChar As Char = Mid(txtJob.Text, 1, 1)

                        txtJob.Text = Mid(txtJob.Text, 2, Len(txtJob.Text))
                        txtJob.Text = CStr(txtJob.Text).PadLeft(9, "0")

                        txtJob.Text = frtChar & txtJob.Text



                    End If

                    sStr = "SELECT * FROM JOB WHERE JOB='" & txtJob.Text & "'"
                    dt = db.RunSql(sStr)

                    If dt.Rows.Count <> 0 Then
                        txtItem.Text = dt.Rows(0)("item")
                        'txtIrsaliyeNo.Text = txtJob.Text
                        txtdesc.Text = dt.Rows(0)("description")
                        'txtBasvuru.Text = dt.Rows(0)("Uf_Basvuru")
                        txtjobMiktar.Text = dt.Rows(0)("Qty_released")
                        txtjobBakiye.Text = dt.Rows(0)("Qty_released") - dt.Rows(0)("Qty_complete")
                        sWhse = dt.Rows(0)("whse")
                    End If


                    'If txtJob.Text.Contains("%") Then
                    'Malzeme = txtItem.Text.Split("%")(0)
                    Isemri = txtJob.Text
                    'txtItem.Text = Malzeme
                    'txtJob.Text = Isemri
                    'txtLot.Text = Isemri

                    sStr = "Exec Tr_Edi_Uretim_Bildirimi_Load @Job=" & sTirnakEkle(Isemri) & ""
                    dtOper = db.RunSql(sStr)

                    cmbOperNo.DataSource = dtOper

                    If dtOper.Rows.Count = 1 Then

                        cmbOperNo.Text = dtOper.Rows(0)("oper_num")

                        'Call cmbOperNo_ValueChanged(sender, e)
                        'cmbOperNo.ValueMember = dtOper.Rows(0)("oper_num")
                    ElseIf dtOper.Rows.Count > 1 Then


                    End If
                    'cmbOperNo.DataSource = dtOper

                    cmbOperNo.Focus()


                    'End If

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim db As New Core.DataForDB2(My.Settings.AccessConnection)
        Try

            If txtetiketmik.Text = "" Or txtkopyasay.Text = "" Then
                MessageBox.Show("Etiket Miktarı ve Kopya Sayısı Zorunlu Alanlardır.")
                Exit Sub
            End If

            'sStr = "Select item, description, '" & txtJob.Text & "'  as isemri, getdate() , (SELECT whse FROM job WHERE job= '" & txtJob.Text & "') AS MAIN ,'" & txtQty.Text & "' as miktar from item where item='" & txtItem.Text & "'"
            sStr = " Select item,description,'" & txtQty.Text & "' as qty_on_hand,'" & IIf(txtLot.Text = "", irsaliyeNo, txtLot.Text) & "' as lot,'" & txtYer.Text & "' as Loc  " & _
                "  ,u_m , getdate() as RecordDate,'" & txtJob.Text & "' as isemri,(SELECT whse FROM job WHERE job= '" & txtJob.Text & "') AS MAIN,'" & irsaliyeNo & "' as Irsaliye  from item where item='" & txtItem.Text & "'"
            dt = db.RunSql(sStr)
            'RaporCagir("FJ_Etiket.rpt", "", "", "", True, True, "", dt) '


            Dim MALZEME_KODU As String = dt.Rows(0)("item")
            Dim MALZEME_TANIMI As String = dt.Rows(0)("description")
            'Dim LOT_NO As String = txtIrsaliyeNo.Text
            Dim LOT_NO As String = IIf(txtLot.Text = "", irsaliyeNo, txtLot.Text)
            Dim LOC As String = txtYer.Text
            Dim u_m As String = dt.Rows(0)("u_m")
            Dim TARIH As String = Date.Now.ToString("yyyy-MM-dd").ToString
            Dim JOBNO As String = txtJob.Text
            Dim Ambar As String = dt.Rows(0)("MAIN")
            Dim IRSALIYE_NO As String = dt.Rows(0)("Irsaliye")

            Dim MIKTAR As Decimal = 0
            MIKTAR = txtetiketmik.Text

            sStr = "DELETE FROM TRM_LABELDB WHERE SEISLEMNO = '" & JOBNO & "'"
            db.RunSql(sStr)


            For y As Integer = 0 To txtkopyasay.Text - 1
                sStr = " insert into TRM_LABELDB " &
                  " ( " &
                  "  SEISLEMNO, IRSALIYENO, " &
                  "  MALZEMEKODU, MALZEMETANIMI, " &
                  "  MIKTAR, LOT_NO, TARIH , LOC , User1 , u_m, Bilesen1 , hammadde " &
                  " ) " &
                  " values " &
                  " ( " &
                  "  '" & JOBNO & "', '" & txtIrsaliye.Text & "', " &
                  "  '" & MALZEME_KODU & "','" & MALZEME_TANIMI & "', " &
                  "  " & MIKTAR & ", '" & LOT_NO & "', " &
                  "  '" & TARIH & "'," &
                  "  '" & LOC & "'," &
                  "  '" & Ambar & "'," &
                  "  '" & u_m & "'," &
                  "  '" & txtNot.Text & "', '" & fasonerAdi & "'" &
                  " ) "

                db.RunSql(sStr, True)
            Next


            Dim RaporAdi As String


            'sStr = " Select Uf_EtiketTipi" &
            '            " From item" &
            '            " Where item=" & sTirnakEkle(MALZEME_KODU)

            'dt = db.RunSql(sStr)

            'RaporAdi = dt.Rows(0)(0).ToString()
            'GetRowInfo(RaporAdi, dt, 0, 0)

            sStr = " SELECT TRM_LABELDB.* FROM TRM_LABELDB " & _
                   " WHERE TRM_LABELDB.[SEISLEMNO]='" & JOBNO & "'"

            'If RaporAdi = String.Empty Then
            '    RaporAdi = "FJ_Etiket"
            'ElseIf FileExists(System.AppDomain.CurrentDomain.BaseDirectory & RaporAdi & ".rpt").ToString = "-1" Then
            RaporAdi = "FJ_Etiket"
            'End If


            AccessLogOnDatabase(Trim(RaporAdi) & ".rpt", db.RunSql(sStr))

            frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
            frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = False
            frmRaporOzet.ShowDialog()

            sStr = "DELETE FROM TRM_LABELDB WHERE SEISLEMNO='" & JOBNO & "'"
            db.RunSql(sStr, True)

            If Not frmRaporOzet.reportDocument1 Is Nothing Then

                frmRaporOzet.reportDocument1.Close()
                frmRaporOzet.reportDocument1.Dispose()
                GC.Collect()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub txtJobItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJobItem.Click
        Try

            If edtTedarikci.Text = String.Empty Then
                MessageBox.Show("Önce tedarikçiyi seçiniz.")
                Exit Sub
            End If

            Dim ssorgu As String
            Dim u_m As String
            Dim jobNo As String


            FindFormUretim = True

            ssorgu = " SELECT   item as Malzeme, Description as [Tanım],j.job as [İşemri],  " &
                     " cast((isnull(qty_released,0)-isnull(qty_complete,0)) as decimal(10,2)) as [Miktar],left( CAST(end_date AS date),10) as [Tarih],rework " &
                     " From job j left join job_sch js on js.job=j.job " &
                     " left join TR_FASONIE f on f.job=j.job " &
                     " where LEFT(j.job,1) IN ('M','F') and stat='R' " &
                     " and qty_released-qty_complete>0  " &
                     " and f.wc = " & sTirnakEkle(edtTedarikci.Text) &
                     " order by end_date "

            Dim Miktar As String = String.Empty
            Dim rework As String = ""
            Dim talimatDurumu As String = String.Empty
            FindFormCagir(ssorgu, "Malzeme", "Tanım", txtJobItem.Text, txtdesc.Text, txtJob.Text, "İşemri", Miktar, "Miktar", txtenddate.Text, "Tarih", rework, "rework")

            'FindFormCagir(ssorgu, "Malzeme", "Tanım", txtJobItem.Text, txtdesc.Text, txtJob.Text, "İşemri")

            If UretimReturn = True Then
                If MessageBox.Show("Daha Eski Tarihli İşemri Bulunmaktadır.Devam Etmek İstiyormusunuz", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Call btnTemizle_Click(sender, e)
                    FindFormUretim = False
                    Exit Sub
                End If
            End If

            FindFormUretim = False
            txtItem.Text = txtJobItem.Text



            If txtJob.Text <> "" Then

                Call txtJob_KeyDown(sender, New KeyEventArgs(Keys.Enter))

            End If

            txtQty.Focus()
            Exit Sub


            FindFormUretim = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub


    Private Sub frmUretimBildirimi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            BolgeselAyar()
            sQuery = " SELECT  reason_code, description   FROM  reason where reason_class='MFG SCRAP'"
            dt = db.RunSql(sQuery)

            If dt.Rows.Count > 0 Then
                cmbreasoncode.DataSource = dt
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub edtTedarikci_ButtonClick(sender As System.Object, e As System.EventArgs) Handles edtTedarikci.ButtonClick
        Dim ssorgu As String
        ssorgu = " SELECT DISTINCT wc as Fasoner , tanim as [Fasoner Adı] FROM TR_Fasonmlz "

        FindFormCagir(ssorgu, "Fasoner", "Fasoner Adı", edtTedarikci.Text, fasonerAdi)

    End Sub

End Class