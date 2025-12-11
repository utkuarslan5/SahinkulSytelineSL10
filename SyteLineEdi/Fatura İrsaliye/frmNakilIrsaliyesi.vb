Public Class frmNakilIrsaliyesi

    #Region "Fields"

    Dim dt As New DataTable
    Dim print As New Print("sql")

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEkle.Click
        Dim db As New Core.Data(My.Settings.ConnectionString)

        Dim dsMatltran As DataSet

        grdIrsaliye.DataSource = Nothing

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        '@Vendor VendNumType,
        '@DocNum DocumentNumType

        grdIrsaliye.DataSource = Nothing

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        '@Vendor VendNumType,
        '@DocNum DocumentNumType

        If chkIrsaliye.Checked = True And txtIrsaliyeNo.Text <> "" Then

            Dim p As New ArrayList

            p.Add(New SqlClient.SqlParameter("@Vend_Num", cmbSatici.Text))

            p.Add(New SqlClient.SqlParameter("@DocNum", txtIrsaliyeNo.Text))

            p.Add(New SqlClient.SqlParameter("@Tarih", dtmFaturaTarihi.Value.ToString("yyyy-MM-dd")))

            p.Add(New SqlClient.SqlParameter("@SessionID", System.Environment.MachineName))

            dsMatltran = db.RunSp("Tr_TransferIrsaliyesi", p, "Matltran")

            If Not dsMatltran Is Nothing Then

                If dsMatltran.Tables("Matltran").Rows.Count > 0 Then

                    grdIrsaliye.DataSource = dsMatltran

                    grdIrsaliye.DataMember = "Matltran"

                    grdIrsaliye.AutoSizeColumns()

                End If

            End If

        ElseIf chkIrsaliye.Checked = True And txtIrsaliyeNo.Text = "" Then

            grdIrsaliye.DataSource = Nothing

        Else

            Dim p As New ArrayList

            p.Add(New SqlClient.SqlParameter("@Vend_Num", cmbSatici.Text))

            p.Add(New SqlClient.SqlParameter("@DocNum", ""))

            p.Add(New SqlClient.SqlParameter("@Tarih", dtmFaturaTarihi.Value.ToString("yyyy-MM-dd")))

            p.Add(New SqlClient.SqlParameter("@SessionID", System.Environment.MachineName))

            dsMatltran = db.RunSp("Tr_TransferIrsaliyesi", p, "Matltran")

            If Not dsMatltran Is Nothing Then

                If dsMatltran.Tables("Matltran").Rows.Count > 0 Then

                    grdIrsaliye.DataSource = dsMatltran

                    grdIrsaliye.DataMember = "Matltran"

                    grdIrsaliye.AutoSizeColumns()

                End If

            End If

        End If

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnSecimiKaldir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecimiKaldir.Click
        grdIrsaliye.UnCheckAllRecords()
    End Sub

    Private Sub btnTumunuSec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTumunuSec.Click
        grdIrsaliye.CheckAllRecords()
    End Sub

    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click

        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

        Dim db As New Core.Data(My.Settings.ConnectionString)

        Dim sQuery, sIrsaliyeNo As String

        Dim row As Janus.Windows.GridEX.GridEXRow

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If chkIrsaliye.Checked = True And txtIrsaliyeNo.Text <> "" Then

            grdIrsaliye.CheckAllRecords()

            checkedRows = Me.grdIrsaliye.GetCheckedRows()

            sQuery = " Delete " & _
                        " From TR_Transfer_Irsaliyesi_Tmp " & _
                        " Where SessionID='" & System.Environment.MachineName & "'"

            db.RunSql(sQuery, True)

            For Each row In checkedRows

                sQuery = " INSERT INTO TR_Transfer_Irsaliyesi_Tmp " & _
                            " (Trans_Num,Item,ItemDesc,Lot,CelikNo,Loc,LocDesc,Wc,WcDesc,Vend_Num,VendorDesc,TaxOffice,Tax_Reg_Num,Qty,SessionId) " & _
                            " VALUES(" & row.Cells("HareketNo").Text & _
                            ",'" & row.Cells("Malzeme").Text & _
                            "','" & row.Cells("MalzemeTanimi").Text.Replace("'", "''") & _
                            "','" & row.Cells("PartiNo").Text.Replace("'", "''") & _
                            "','" & row.Cells("CelikNo").Text.Replace("'", "''") & _
                            "','" & row.Cells("Yer").Text & _
                            "','" & row.Cells("YerTanimi").Text.Replace("'", "''") & _
                            "','" & row.Cells("IsMerkezi").Text & _
                            "','" & row.Cells("IsMerkeziTanimi").Text.Replace("'", "''") & _
                            "','" & row.Cells("Satici").Text & _
                            "','" & row.Cells("SaticiTanimi").Text.Replace("'", "''") & _
                            "','" & txtVergiDairesi.Text & _
                            "','" & txtVergiNo.Text & _
                            "'," & row.Cells("Miktar").Text & _
                            ",'" & System.Environment.MachineName & "')"

                db.RunSql(sQuery, True)

                sIrsaliyeNo = row.Cells("IrsaliyeNo").Text

            Next

            If txtIrsaliyeNo.Text <> sIrsaliyeNo Then

                If MessageBox.Show(" Daha önce yazdýrýlmýþ Ýrsaliye üzerinde deðiþiklik yapýlacak onaylýyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then

                    Exit Sub

                End If

            End If

            'Dim p1 As New ArrayList

            'p1.Add(New SqlClient.SqlParameter("@IrsaliyeTarihi", dtmFaturaTarihi.Value))

            'p1.Add(New SqlClient.SqlParameter("@SessionID", System.Environment.MachineName))

            'p1.Add(New SqlClient.SqlParameter("@Aciklama", txtAciklama.text))
            ''
            'db.RunSp("Tr_Transfer_Irsaliyesi_Rpt", p1, "Matltran")

            'frmRapor.rptViewer.ReportSource = print.LogOnDatabase(System.Windows.Forms.Application.StartupPath & "\TR_Transfer_Irsaliyesi.rpt")
            'frmRapor.sRapor = System.Windows.Forms.Application.StartupPath & "\TR_Transfer_Irsaliyesi.rpt"
            'frmRapor.sTip = "Sql"
            'frmRapor.ShowDialog()

            sQuery = " Select * " & _
                        " From Tr_Fason_rpt " & _
                        " Where SessionID='" & System.Environment.MachineName & "'"

            dt = db.RunSql(sQuery)

            RaporCagir("TR_Transfer_Irsaliyesi.rpt", , , "SLIRSALIYE", , , , dt)

            'Dim rpt As New AsVol.Print.frmReportViewer("Ýrsaliye", "\\utility\fsTaskMan\TransferIrsaliyesi\TR_Transfer_Irsaliyesi.rpt")

            ''rpt.AddParameter("@IrsaliyeTarihi", dtmFaturaTarihi.Value)

            ''rpt.AddParameter("@SessionId", System.Environment.MachineName)

            ''Dim rpt As New AsVol.Print.frmReportViewer("irsaliye", "C:\VB_Projects\_Musteri\Nobel\Yevmiye\Yevmiye\TR_YevmiyeNumaralandirmaRaporu.rpt")
            ''
            'rpt.SetLogonInfo(My.Settings.ConnectionString.Split(";")(0).Split("=")(1), My.Settings.ConnectionString.Split(";")(1).Split("=")(1), My.Settings.ConnectionString.Split(";")(2).Split("=")(1), My.Settings.ConnectionString.Split(";")(3).Split("=")(1))
            ''
            ''
            'rpt.ShowReport(True)

            If txtIrsaliyeNo.Text <> sIrsaliyeNo Then

                If MessageBox.Show(" Ýrsaliye Doðru Yazdýrýldý mý?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    sQuery = "Update Matltran " & _
                                "Set Document_num='" & txtIrsaliyeNo.Text & "'" & _
                                 " Where Trans_Num IN (Select Trans_Num " & _
                                                        " From TR_Transfer_Irsaliyesi_Tmp" & _
                                                        " Where SessionId='" & System.Environment.MachineName & "') "

                    db.RunSql(sQuery, True)

                End If

            End If

            '
            Exit Sub

        ElseIf chkIrsaliye.Checked = True And txtIrsaliyeNo.Text = "" Then

            If MessageBox.Show(" Ýrsaliye'yi iptal etmek istediðinizden emin misiniz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                grdIrsaliye.CheckAllRecords()

                checkedRows = Me.grdIrsaliye.GetCheckedRows()

                For Each row In checkedRows

                    sQuery = "Update Matltran " & _
                                "Set Document_num= Null" & _
                                 " Where Trans_Num='" & row.Cells("HareketNo").Text & "'"

                    db.RunSql(sQuery, True)

                Next row

                btnEkle_Click(sender, e)

                System.Windows.Forms.Cursor.Current = Cursors.Default

                Exit Sub

            End If

        Else

            'If chkIrsaliye.Checked = False And txtIrsaliyeNo.Text = "" Then

            '    MessageBox.Show("Lütfen Ýrsaliye Numarasýný Yazýnýz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            '    Exit Sub

            'End If

            checkedRows = Me.grdIrsaliye.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen Bir Malzeme Seçiniz", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Exit Sub

            Else

                sQuery = " Delete " & _
                            " From TR_Transfer_Irsaliyesi_Tmp " & _
                            " Where SessionID='" & System.Environment.MachineName & "'"

                db.RunSql(sQuery, True)

                For Each row In checkedRows

                    sQuery = " INSERT INTO TR_Transfer_Irsaliyesi_Tmp " & _
                                " (Trans_Num,Item,ItemDesc,Lot,CelikNo,Loc,LocDesc,Wc,WcDesc,Vend_Num,VendorDesc,TaxOffice,Tax_Reg_Num,Qty,SessionId) " & _
                                " VALUES(" & row.Cells("HareketNo").Text & _
                                ",'" & row.Cells("Malzeme").Text & _
                                "','" & row.Cells("MalzemeTanimi").Text.Replace("'", "''") & _
                                "','" & row.Cells("PartiNo").Text.Replace("'", "''") & _
                                "','" & row.Cells("CelikNo").Text.Replace("'", "''") & _
                                "','" & row.Cells("Yer").Text & _
                                "','" & row.Cells("YerTanimi").Text.Replace("'", "''") & _
                                "','" & row.Cells("IsMerkezi").Text & _
                                "','" & row.Cells("IsMerkeziTanimi").Text.Replace("'", "''") & _
                                "','" & row.Cells("Satici").Text & _
                                "','" & row.Cells("SaticiTanimi").Text.Replace("'", "''") & _
                                "','" & txtVergiDairesi.Text & _
                                "','" & txtVergiNo.Text & _
                                "'," & row.Cells("Miktar").Text & _
                                ",'" & System.Environment.MachineName & "')"

                    db.RunSql(sQuery, True)

                Next

                'Dim p As New ArrayList

                'p.Add(New SqlClient.SqlParameter("@IrsaliyeTarihi", dtmFaturaTarihi.Value.ToString("yyyy-MM-dd")))

                'p.Add(New SqlClient.SqlParameter("@SessionID", System.Environment.MachineName))

                'p.Add(New SqlClient.SqlParameter("@Aciklama", txtAciklama.Text))

                'db.RunSp("Tr_Transfer_Irsaliyesi_Rpt", p, "Matltran")

                sQuery = " Select * " & _
                            " From Tr_Fason_rpt " & _
                            " Where SessionID='" & System.Environment.MachineName & "'"

                dt = db.RunSql(sQuery)

                'frmRapor.rptViewer.ReportSource = print.LogOnDatabase(System.Windows.Forms.Application.StartupPath & "\TR_Transfer_Irsaliyesi.rpt")

                'frmRapor.ShowDialog()
                RaporCagir("TR_Transfer_Irsaliyesi.rpt", , , "SLIRSALIYE", , , , dt)

                'Dim rpt As New AsVol.Print.frmReportViewer("Ýrsaliye", "\\utility\fsTaskMan\TransferIrsaliyesi\TR_Transfer_Irsaliyesi.rpt")

                ''rpt.AddParameter("@IrsaliyeTarihi", dtmFaturaTarihi.Value)

                ''rpt.AddParameter("@SessionId", System.Environment.MachineName)

                ''Dim rpt As New AsVol.Print.frmReportViewer("irsaliye", "C:\VB_Projects\_Musteri\Nobel\Yevmiye\Yevmiye\TR_YevmiyeNumaralandirmaRaporu.rpt")
                ''
                'rpt.SetLogonInfo(My.Settings.ConnectionString.Split(";")(0).Split("=")(1), My.Settings.ConnectionString.Split(";")(1).Split("=")(1), My.Settings.ConnectionString.Split(";")(2).Split("=")(1), My.Settings.ConnectionString.Split(";")(3).Split("=")(1))
                ''
                ''
                'rpt.ShowReport(True)

                '
                If MessageBox.Show(" Ýrsaliye Doðru Yazdýrýldý mý?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    'sQuery = "Update Matltran " & _
                    '            "Set Document_num='" & txtIrsaliyeNo.Text.Replace("'", "''") & "'" & _
                    '             " Where Trans_Num IN (Select Trans_Num " & _
                    '                                    " From TR_Transfer_Irsaliyesi_Tmp" & _
                    '                                    " Where SessionId='" & System.Environment.MachineName & "') "

                    'db.RunSql(sQuery, True)
                    Dim nSevkNo As Integer
                    Dim shipmentId As Decimal
                    'If txtIrsaliyeNo.Text = "" Then

                    nSevkNo = SeriNoAl("SHP")

                    sQuery = " INSERT INTO [dbo].[shipment]([status],[cust_num],[cust_seq],[whse],[tracking_number]" & _
                             " ,[bol_printed],[proforma_printed],[customs_invoice_printed],[cert_of_origin_printed]" & _
                             " ,[pack_slip_printed],[invoice_printed],[RowPointer],[NoteExistsFlag],[CreatedBy]" & _
                             " ,[UpdatedBy] ,[CreateDate],[RecordDate],[InWorkflow])" & _
                             " VALUES('S'," & sTirnakEkle(cmbSatici.Text) & ",0," & sTirnakEkle("MAIN") & _
                             "," & nSevkNo & ",0,0,0,0,0,0,NEWID(),0,'sa','sa',GETDATE(),GETDATE(),0)"

                    db.RunSql(sQuery)

                    sQuery = "SELECT shipment_id FROM shipment_mst WHERE tracking_number =" & nSevkNo
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then
                        shipmentId = dt.Rows(0)("shipment_id")
                    End If

                    sQuery = "Update Matltran " & _
                         "Set Document_num='" & shipmentId & "'" & _
                          " Where Trans_Num IN (Select Trans_Num " & _
                                                 " From TR_Transfer_Irsaliyesi_Tmp" & _
                                                 " Where SessionId='" & System.Environment.MachineName & "') "

                    db.RunSql(sQuery, True)

                    'Else
                    '    sQuery = "Update Matltran " & _
                    '    "Set Document_num='" & txtIrsaliyeNo.Text.Replace("'", "''") & "'" & _
                    '     " Where Trans_Num IN (Select Trans_Num " & _
                    '                            " From TR_Transfer_Irsaliyesi_Tmp" & _
                    '                            " Where SessionId='" & System.Environment.MachineName & "') "

                    '    db.RunSql(sQuery, True)
                    'End If

                    sQuery = "SELECT t.Item, SUM(Qty) Qty, MAX(i.u_m) u_m FROM TR_Transfer_Irsaliyesi_Tmp t with (nolock) " & _
                        " LEFT JOIN item i with (nolock) On i.item = t.Item WHERE SessionId = '" & System.Environment.MachineName & "'" & _
                        " GROUP BY t.item "
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then

                        For Each rw As DataRow In dt.Rows


                            sQuery = " INSERT INTO SHPPACK_TRF(SHPNO,SEVKTAR,CRDTIM,CUST,SHIPMIK,ITNBR,STKOB) " & _
                          " VALUES(" & shipmentId & "," & sTirnakEkle(CDate(dtmFaturaTarihi.Text).ToString("yyyy-MM-dd")) & "," & Now.ToString("HHmmss") & "," & _
                          sTirnakEkle(cmbSatici.Text) & "," & rw.Item("Qty") & "," & sTirnakEkle(rw.Item("Item")) & "," & sTirnakEkle(rw.Item("u_m")) & ")"
                            db.RunSql(sQuery)

                            'sQuery = "INSERT INTO SHPDTY_TRF(SHPNO,SHIPMIK,ITNBR,STKOB) " & _
                            '" VALUES(" & nSevkNo & "," & rw.Item("Qty") & "," & sTirnakEkle(rw.Item("Item")) & "," & sTirnakEkle(rw.Item("u_m")) & ")"
                            'db.RunSql(sQuery)
                        Next


                    End If

                End If

            End If

        End If

        btnEkle_Click(sender, e)

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmbSatici_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSatici.ButtonClick
        Try
            Dim sQuery As String
            sQuery = " SELECT vend_num as [Satýcý Kodu], name as [Satýcý Adý] FROM vendaddr"

            FindFormCagir(sQuery, "Satýcý Kodu", "Satýcý Adý", cmbSatici.Text, txtSaticiAdi.Text)

            LoadTextBoxes()
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub cmbSatici_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sQuery As String

        Dim db As New Core.Data(My.Settings.ConnectionString)

        Dim dsVendor As DataSet

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        sQuery = " Select vendaddr.Name,vendaddr.addr##1, tax_reg_num1, Uf_TaxOffice " & _
                    " From Vendor Left Outer Join vendaddr " & _
                    " On Vendor.vend_num=Vendaddr.vend_num " & _
                    " Where Vendor.vend_num='" & cmbSatici.Text & "'"

        dsVendor = db.RunSql(sQuery, "Vendor")

        If Not dsVendor Is Nothing Then

            If dsVendor.Tables("Vendor").Rows.Count > 0 Then

                For i As Integer = 0 To dsVendor.Tables("Vendor").Rows.Count - 1

                    With dsVendor.Tables("Vendor").Rows(i)

                        txtSaticiAdi.Text = .Item("Name").ToString

                        txtAdres.Text = .Item("addr##1").ToString

                        txtVergiDairesi.Text = LTrim(.Item("tax_reg_num1").ToString.Split("/")(0))
                        Try
                            txtVergiNo.Text = LTrim(.Item("tax_reg_num1").ToString.Split("/")(1))

                        Catch ex As Exception

                        End Try

                    End With

                Next i

            End If

        End If

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Me.Text & Application.ProductVersion
    End Sub

    Private Sub LoadTextBoxes()
        Dim sQuery As String

        Dim db As New Core.Data(My.Settings.ConnectionString)

        Dim dsVendor As DataSet

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        sQuery = " Select vendaddr.Name,vendaddr.addr##1, tax_reg_num1, Uf_TaxOffice " & _
                    " From Vendor Left Outer Join vendaddr " & _
                    " On Vendor.vend_num=Vendaddr.vend_num " & _
                    " Where Vendor.vend_num='" & cmbSatici.Text & "'"

        dsVendor = db.RunSql(sQuery, "Vendor")

        If Not dsVendor Is Nothing Then

            If dsVendor.Tables("Vendor").Rows.Count > 0 Then

                For i As Integer = 0 To dsVendor.Tables("Vendor").Rows.Count - 1

                    With dsVendor.Tables("Vendor").Rows(i)

                        txtSaticiAdi.Text = .Item("Name").ToString

                        txtAdres.Text = .Item("addr##1").ToString

                        'txtVergiDairesi.Text = LTrim(.Item("tax_reg_num1").ToString.Split("/")(0))
                        'Try
                        '    txtVergiNo.Text = LTrim(.Item("tax_reg_num1").ToString.Split("/")(1))
                        'Catch ex As Exception

                        'End Try

                        txtVergiDairesi.Text = .Item("Uf_TaxOffice").ToString
                        txtVergiNo.Text = .Item("tax_reg_num1").ToString

                    End With

                Next i

            End If

        End If

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    #End Region 'Methods

End Class