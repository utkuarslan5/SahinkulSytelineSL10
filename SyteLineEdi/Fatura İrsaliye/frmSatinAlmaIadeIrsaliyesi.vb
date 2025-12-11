Public Class frmSatinAlmaIadeIrsaliyesi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim dtIrsaliye As New DataTable
    Dim dtTemp As New DataTable
    Dim sSorgu As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

            'If txtSatici.Text = "" Then

            '    MessageBox.Show("Lütfen Satıcı  Belirtiniz..")

            '    Exit Sub

            'End If

            If Not kontrol(txtSatici, GroupBox1) Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            ssorgu = ""

            sSorgu = "SELECT trans_num ,vend_num ,name" & _
                          " ,po_num ,ref_line_suf ,ref_release " & _
                          " ,item ,description ,u_m " & _
                          " ,terms_code ,qty ,document_num, trans_date, whse " & _
                        " FROM Tr_Satinalma_Iade_Load " & _
                        " Where vend_num =" & sTirnakEkle(txtSatici.Text)

            If chkYenidenBasim.Checked Then

                sSorgu = sSorgu & " And Uf_MatlVoucherNo is not null"

                sSorgu = sSorgu & " And Document_Num =" & sTirnakEkle(cmdDocNum.Text)

            Else

                sSorgu = sSorgu & " And Uf_MatlVoucherNo is null"

            End If

            dt = Nothing

            dt = db.RunSql(ssorgu)

            grdMain.DataSource = dt

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            Dim row As Janus.Windows.GridEX.GridEXRow

            If Not kontrol(, GroupBox1) Then
                Exit Sub
            End If

            checkedRows = Me.grdMain.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                dbAccess.RunSql("Delete From ANSIRSALIYE")

                dbAccess.RunSql("Delete From tmpIrsaliye")

                For Each row In checkedRows
                    sSorgu = "INSERT INTO tmpIrsaliye  " & _
                   " (DHCVNB, DHZ969, DHCANB, DHIVNB, " & _
                   "  DHCQCD, " & _
                   "  DDAITX, DDALTX, DDARQT, DDFCNB, " & _
                   "  DHA3CD, co_release)" & _
                   "Values" & _
                   "(" & sTirnakEkle(row.Cells("po_num").Text) & _
                   "," & sTirnakEkle(IIf(chkYenidenBasim.Checked, cmdDocNum.Text, txtIrsNum.Text)) & _
                   "," & sTirnakEkle(row.Cells("vend_num").Text) & _
                   "," & sTirnakEkle(CDate(dtIrsDate.Value).ToString("yyyy-MM-dd")) & _
                   "," & sTirnakEkle(row.Cells("u_m").Text) & _
                   "," & sTirnakEkle(row.Cells("item").Text) & _
                   "," & sTirnakEkle(row.Cells("description").Text) & _
                   "," & row.Cells("qty").Text & _
                   "," & row.Cells("ref_line_suf").Text & _
                   "," & sTirnakEkle(row.Cells("whse").Text) & _
                   "," & row.Cells("ref_release").Text & _
                   ")"

                    dbAccess.RunSql(sSorgu)

                Next

            End If

            Dim tmpItnbr, _
                tmpWhse, tmpSevkTar, tmpSiparisNo As String

            Dim tmpShipmik As Double

            Dim sVergiNo, sVergiDairesi As String
            Dim nSevkNo As Integer

            Dim sSaticiAdi, sSaticiAdresi1, sSaticiAdresi2, sSaticiAdresi3, _
            sSaticiAdresi4, sSaticiSehir, sSaticiUlke As String

            '***************** İrsaliye Döngüsü********************
            sSorgu = "Select  DHCANB, DDAITX,DDALTX, DHCVNB ,sum(DDARQT) As SEVKMIK , Max(DHIVNB) As DHIVNB, Max(DHA3CD) As DHA3CD, DHCQCD" & _
                        " From tmpIrsaliye" & _
                        " Group By  DHCANB,DDAITX,DDALTX, DHCVNB, DHCQCD"

            dtIrsaliye = dbAccess.RunSql(sSorgu)

            If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                For Each rowDt As DataRow In dtIrsaliye.Rows

                    tmpShipmik = rowDt.Item("SEVKMIK").ToString

                    tmpItnbr = rowDt.Item("DDAITX").ToString

                    tmpSevkTar = rowDt.Item("DHIVNB").ToString

                    tmpSiparisNo = rowDt.Item("DHCVNB").ToString

                    tmpWhse = rowDt.Item("DHA3CD").ToString

                    sSorgu = "Select * " & _
                                   " From vendaddr" & _
                                   " Where vend_num=" & sTirnakEkle(rowDt.Item("DHCANB").ToString)

                    dtTemp = db.RunSql(sSorgu)

                    GetRowInfo(sSaticiAdi, dtTemp, 0, "name")
                    GetRowInfo(sSaticiAdresi1, dtTemp, 0, "addr##1")
                    GetRowInfo(sSaticiAdresi1, dtTemp, 0, "addr##2")
                    GetRowInfo(sSaticiAdresi1, dtTemp, 0, "addr##3")
                    GetRowInfo(sSaticiAdresi1, dtTemp, 0, "addr##4")
                    GetRowInfo(sSaticiSehir, dtTemp, 0, "city")
                    GetRowInfo(sSaticiUlke, dtTemp, 0, "country")

                    sVergiNo = sLookup("tax_reg_num1", "vendor", " vend_num=" & sTirnakEkle(rowDt.Item("DHCANB").ToString))

                    sVergiDairesi = sLookup("Uf_TaxOffice", "vendor", " vend_num=" & sTirnakEkle(rowDt.Item("DHCANB").ToString))

                    sSorgu = " Insert Into ANSIRSALIYE " & _
                            "(DHZ969, SEVKTAR, DHCANB, " & _
                                "SHIPNM, DDAITX, " & _
                                "SEVKMIK, AGOB, ENGNO, " & _
                                "CUSNM, DHBZTX, DHB0TX, DHB1TX, " & _
                                "DHAAGX, DHB6CD, DHAAGW, TAXNUM, " & _
                                "TAXNAME, MURNKOD, MURNTNM )" & _
                            "Values (" & _
                            nSevkNo & "," & _
                            sTirnakEkle(rowDt.Item("DHIVNB").ToString) & "," & _
                            sTirnakEkle(rowDt.Item("DHCANB").ToString) & "," & _
                            sTirnakEkle(sSaticiAdi) & "," & _
                            sTirnakEkle(tmpItnbr) & "," & _
                            rowDt.Item("SEVKMIK").ToString & "," & _
                            sTirnakEkle(rowDt.Item("DHCQCD").ToString) & "," & _
                            sTirnakEkle(sLookup("ENGNO", "ITEMASA", "ITNBR=" & sTirnakEkle(tmpItnbr))) & "," & _
                            sTirnakEkle(sSaticiAdi) & "," & _
                            sTirnakEkle(sSaticiAdresi1) & "," & _
                            sTirnakEkle(sSaticiAdresi2) & "," & _
                            sTirnakEkle(sSaticiAdresi3) & "," & _
                            sTirnakEkle(sSaticiAdresi4) & "," & _
                            sTirnakEkle(sSaticiSehir) & "," & _
                            sTirnakEkle(sSaticiUlke) & "," & _
                            sTirnakEkle(sVergiNo) & "," & _
                            sTirnakEkle(sVergiDairesi) & "," & _
                            sTirnakEkle(tmpItnbr) & "," & _
                            sTirnakEkle(rowDt.Item("DDALTX").ToString) & _
                            ")"

                    dbAccess.RunSql(sSorgu)

                Next rowDt

                RaporCagir("SLIrsaliye.rpt", , , "SLIRSALIYE")

                Try

                    db.BeginTransaction()

                    For Each row In checkedRows

                        sSorgu = " Update Matltran" & _
                                " Set Document_num=" & sTirnakEkle(txtIrsNum.Text) & "," & _
                                    " Uf_MatlvoucherNo=" & sTirnakEkle("0") & _
                                " Where Trans_Num=" & row.Cells("trans_num").Text

                        db.RunSql(sSorgu)

                    Next row

                    db.CommitTransaction()

                Catch ex As Exception

                    db.RollbackTransaction()

                    Throw ex

                End Try

                btnSorgula_Click(sender, e)

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub chkYenidenBasim_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkYenidenBasim.CheckedChanged
        cmdDocNum.Visible = chkYenidenBasim.Checked

        txtIrsNum.Visible = Not chkYenidenBasim.Checked

        cmdDocNum.Location = New System.Drawing.Point(104, 75)

        grdMain.DataSource = Nothing

        grdMain.Refresh()

        txtSatici_TextChanged(sender, e)
    End Sub

    Private Sub cmbVendNum_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSatici.ButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.Cursor = Cursors.WaitCursor
            ssorgu = ""
            ssorgu = "SELECT Distinct Vend_Num as Satici,Name as Tanim " & _
                            " From VendAddr" & _
                     " order by Vend_Num "

            FindFormCagir(ssorgu, "Satici", "Tanim", txtsatici.Text, txtsaticiTanim.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.CurrentCellChanged, grdMain.Click, grdMain.DoubleClick
        If chkYenidenBasim.Checked Then

            GridSec(grdMain, "document_num", sender, e)

        End If
    End Sub

    Private Sub txtSatici_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSatici.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor

            ssorgu = ""
            ssorgu = "SELECT Distinct document_num " & _
                        " FROM Tr_Satinalma_Iade_Load " & _
                        " Where Uf_MatlvoucherNo Is Not null " & _
                         " And  Vend_Num = " & sTirnakEkle(txtSatici.Text)

            dt = db.RunSql(ssorgu)

            cmdDocNum.DataSource = dt

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    #End Region 'Methods

End Class