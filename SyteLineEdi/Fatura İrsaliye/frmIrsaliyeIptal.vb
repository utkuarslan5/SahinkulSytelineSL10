Imports System.Threading

Public Class frmIrsaliyeIptal
    Dim sQuery As String
    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)

    Dim dt As New DataTable
    Dim dtTemp As New DataTable
    Dim dtMatltran As New DataTable
    Dim ssorgu As String
    Dim dtIrsaliye As New DataTable

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click

        Try
            If txtSevkNo.Text.Trim = "" Then
                MessageBox.Show("Lütfen sevk numarasını giriniz.")
                Exit Sub
            End If

            Cursor = Cursors.WaitCursor

            sQuery = " Select *" & _
                        " From TrM_IrslyIpt " & _
                        " Where 1=1 "


            sQuery = sQuery & "    and shipment_id=" & txtSevkNo.Text


            sQuery = sQuery & " Order By shipment_id, item "

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                Duzenle(GridEX1)
            Else

                GridEX1.DataSource = Nothing


            End If

        Catch ex As Exception

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MessageBox.Show(ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click

        Dim bEtiketIptal As Boolean
        Dim bCekmeli As Boolean
        Dim bEtiketDurum As Boolean
        Dim nSevkNo As Integer
        Try



            If GridEX1.GetRows.Length = 0 Then
                MessageBox.Show("Lütfen önce kayıtları listeleyiniz.")
                Exit Sub
            End If

            Cursor = Cursors.WaitCursor

            If MessageBox.Show("Sevkiyat iptal edilecektir. Onaylıyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                'Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

                Dim row As Janus.Windows.GridEX.GridEXRow
                Dim Sayac As Integer = 0
                'checkedRows = Me.GridEX1.GetCheckedRows()


                'If checkedRows.Length = 0 Then
                '    MessageBox.Show("Lütfen kayıt seçiniz.")
                '    Cursor = Cursors.Default
                '    Exit Sub
                'End If

                'Else
                nSevkNo = 0

                Dim dt As DataTable
                dt = GridEX1.DataSource

                For Each dr As DataRow In dt.Rows

                    If nSevkNo = 0 Then
                        nSevkNo = CDec(dr.Item("shipment_id"))
                    Else
                        If nSevkNo <> CDec(dr.Item("shipment_id")) Then
                            MessageBox.Show("Farklı sevkiyatlar birleştirilemez.")
                            Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If

                Next

                For Each dr2 As DataRow In dt.Rows

                    If dr2.Item("Uf_FaturaNo") <> "0" Then
                        MessageBox.Show("Faturalanmış sevkler iptal edilemez.")
                        Cursor = Cursors.Default
                        Exit Sub
                        'ElseIf row.Cells("costat").Text <> "O" Then
                        '    MessageBox.Show("Sipariş durumu uygun değil.")
                        '    Cursor = Cursors.Default
                        '    Exit Sub
                        'ElseIf row.Cells("coitemstat").Text <> "O" Then
                        '    MessageBox.Show("Kalem durumu uygun değil.")
                        '    Cursor = Cursors.Default
                        '    Exit Sub
                    ElseIf CDec(dr2.Item("qty")) = 0 Then
                        MessageBox.Show("Sevk hareketi bulunamadı.")
                        Cursor = Cursors.Default
                        Exit Sub
                    End If

                Next

                For Each dr3 As DataRow In dt.Rows

                    sQuery = "SELECT * FROM co_ship_mst " & _
                                " WHERE co_num = " & sTirnakEkle(dr3.Item("co_num")) & _
                                " And co_line = " & dr3.Item("co_line") & _
                                " AND site_ref=" & sTirnakEkle("Default") & _
                                " AND ISNULL(shipment_id,0) = 0"
                    dt = db.RunSql(sQuery)

                    If dt.Rows.Count > 0 Then
                        MessageBox.Show(dr3.Item("co_num") & " nolu siparişin " & CDec(dr3.Item("co_line")) & " nolu kalemine ait " & vbNewLine & _
                                        " sevkiyatın irsaliyesi yazdırılmamış kayıtlar mevcut. " & vbNewLine & _
                                        "Eksik irsaliyeleri yazdırdıktan sonra tekrar deneyiniz.")
                        Exit Sub
                    End If

                Next


                sQuery = "select count(*) from reason_mst where reason_class='CO RETURN' and reason_code='IPT' " & _
                     " AND site_ref=" & sTirnakEkle("Default")
                dt = db.RunSql(sQuery)

                If dt.Rows(0)(0) = 0 Then
                    MessageBox.Show("İptal iade kodunu tanımlayınız.")
                    Exit Sub
                    Cursor = Cursors.Default
                End If

                sQuery = "select count(*) from employee_mst where ltrim(emp_num)='EDI'" & _
                     " AND site_ref=" & sTirnakEkle("Default")

                dt = db.RunSql(sQuery)

                If dt.Rows(0)(0) = 0 Then
                    MessageBox.Show("EDI çalışan kaydını tanımlayınız.")
                    Exit Sub
                    Cursor = Cursors.Default
                End If


                Dim dateSeq As Integer = 0


                sQuery = "SELECT PICKNO FROM SHPPACK WHERE SHPNO = " & nSevkNo
                dtTemp = db.RunSql(sQuery)

                Dim cekmeNo As String

                cekmeNo = dtTemp.Rows(0)(0)


                dt = GridEX1.DataSource


                For Each dr As DataRow In dt.Rows

                    sQuery = "INSERT INTO tmpIrsaliyeIptal  " & _
                      " (DHCVNB, DHZ969, DHCANB, DHIVNB, " & _
                      " DHB9CD, DHF1CD, " & _
                      " DDAITX, DDARQT, DDFCNB, " & _
                      " DHA3CD, co_release, date_seq,MURNKOD,CEKLIST)" & _
                      "Values" & _
                      "(" & sTirnakEkle(dr.Item("co_num")) & _
                      "," & sTirnakEkle(dr.Item("shipment_id")) & _
                      "," & sTirnakEkle(dr.Item("cust_num")) & _
                      "," & sTirnakEkle(CDate(dr.Item("ship_date")).ToString("yyyy-MM-dd")) & _
                      "," & sTirnakEkle(dr.Item("DHB9CD")) & _
                      "," & sTirnakEkle(dr.Item("DHF1CD")) & _
                      "," & sTirnakEkle(dr.Item("item")) & _
                      "," & dr.Item("qty") & _
                      "," & dr.Item("co_line") & _
                      "," & sTirnakEkle(dr.Item("DHA3CD")) & _
                      "," & dr.Item("co_release") & _
                      "," & dr.Item("date_seq") & _
                      "," & sTirnakEkle(dr.Item("MURNKOD")) & _
                      "," & sTirnakEkle(cekmeNo) & _
                      ")"

                    db.RunSql(sQuery)

                Next

                sQuery = "Select DHZ969 AS SEVKNO, CEKLIST  " & _
                           " From tmpIrsaliyeIptal" & _
                           " where DHZ969 = " & nSevkNo & _
                           " Group By DHZ969, CEKLIST"

                dtIrsaliye = db.RunSql(sQuery)

                If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                    For Each rowDt As DataRow In dtIrsaliye.Rows

                        If nLookup("Max(Asndrm)", "SHPPACK", "SHPNO=" & rowDt.Item("SEVKNO").ToString) = 1 Then

                            MessageBox.Show(rowDt.Item("SEVKNO").ToString & " nolu sevkiyatın ASN si gönderilmiş iptal edilemez!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            GoTo SonrakiKayit

                        End If

                        If nLookup("Max(Fatdrm)", "SHPPACK", "SHPNO=" & rowDt.Item("SEVKNO").ToString) = 1 Then

                            MessageBox.Show(rowDt.Item("SEVKNO").ToString & " nolu sevkiyat faturalanmış iptal edilemez!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            GoTo SonrakiKayit

                        End If

                        If rowDt.Item("CEKLIST").ToString <> 0 Then

                            bCekmeli = True

                        Else

                            bCekmeli = False

                        End If

                        If bCekmeli = True Then

                            'Etiket varmı yokmu kontrolu

                            If nLookup("Count(*)", "ETIKETDTY", "PICKNO=" & rowDt.Item("CEKLIST").ToString) > 0 Then

                                bEtiketDurum = True

                                If MessageBox.Show(rowDt.Item("SEVKNO").ToString & " nolu Sevkiyata ilişkin Etiketler iptal edilecek mi?" & vbNewLine & _
                                                    "İptal Durumunda yeniden Etiket basılmalıdır!", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                                    bEtiketIptal = True

                                Else

                                    bEtiketIptal = False

                                End If

                            Else

                                bEtiketDurum = False

                            End If ' Etiketdty de kayıt varmı IF

                        End If ' Cekmeli için IF

                        If bEtiketIptal = True Then

                            'Etiketler iptal ediliyor
                            sQuery = "Delete " & _
                                    " From EtiketDty" & _
                                    " Where PICKNO=" & rowDt.Item("CEKLIST").ToString

                            db.RunSql(sQuery)

                        End If

                        If bCekmeli = True Then

                            'Trceklist Update ediliyor
                            'trCeklist update için alt döngü

                            sQuery = "Update TRCEKLIST" & _
                                        " Set SEVKNO=Null " & _
                                            " ,SEVKMIK=0 " & _
                                            " ,SEVKTAR=Null" & _
                                            " ,IRSNO=Null" & _
                                            " ,DURUM=" & IIf(bEtiketDurum = False Or bEtiketIptal = True, "'1'", "'2'") & _
                                        " Where SEVKNO=" & rowDt.Item("SEVKNO").ToString

                            dt = db.RunSql(sQuery)

                        End If


                        'Shppack Ve Shpdty siliniyor...

                        sQuery = "Delete " & _
                            " From ShpDty" & _
                            " Where SHPNO=" & rowDt.Item("SEVKNO").ToString

                        db.RunSql(sQuery)

                        sQuery = "Delete " & _
                                    " From ShpPack" & _
                                    " Where SHPNO=" & rowDt.Item("SEVKNO").ToString

                        db.RunSql(sQuery)


SonrakiKayit:

                    Next rowDt ' Dongu

                End If '

                'End If 'Iptal Sorgusu

                dt = GridEX1.DataSource

                For Each dr4 As DataRow In dt.Rows

                    sQuery = "SELECT * FROM DCCO_mst " & _
                          " WHERE stat = 'P' " & _
                          " AND site_ref=" & sTirnakEkle("Default")
                    dtTemp = db.RunSql(sQuery)

                    If dtTemp.Rows.Count > 0 Then

                        Thread.Sleep(10000)

                        sQuery = "SELECT * FROM DCCO_mst " & _
                        " WHERE stat = 'P' " & _
                        " AND site_ref=" & sTirnakEkle("Default")
                        dtTemp = db.RunSql(sQuery)

                        If dtTemp.Rows.Count > 0 Then
                            MessageBox.Show("Müşteri sipariş sevkleri hata işleme kayıtları post edilemedi. Hatayı düzeltip tekrar deneyiniz.")
                            Exit Sub
                        End If

                    End If

                    db.BeginTransaction()

                    dt = GridEX1.DataSource

                    sQuery = " Update co_ship_mst " & _
                                    " Set reason_text = 'X', shipment_id = null " & _
                                    " Where shipment_id = " & sTirnakEkle(nSevkNo) & _
                                    " and  co_num  = " & sTirnakEkle(dr4.Item("co_num")) & _
                                    " And co_line = " & dr4.Item("co_line") & _
                                    " and ship_date = " & sTirnakEkle(CDate(dr4.Item("ship_date")).ToString("yyyy-MM-dd HH:mm:ss.fff")) & _
                                    " and date_seq = " & dr4.Item("date_seq") & _
                                    " AND site_ref=" & sTirnakEkle("Default")

                    db.RunSql(sQuery)

                    'sQuery = "Update SHPPACK " & _
                    '    " SET DURUM = 'X' ," & _
                    '    " NOT1 = '" & txtIptalNedeni.Text & "'" & _
                    '    " WHERE SHPNO = '" & nSevkNo & "'"
                    'db.RunSql(sQuery)

                    sQuery = " update co_mst " & _
                          " set stat='O'" & _
                          " Where co_num  = " & sTirnakEkle(dr4.Item("co_num")) & _
                          " And Stat <> 'O'" & _
                          " AND site_ref=" & sTirnakEkle("Default")
                    db.RunSql(sQuery)

                    sQuery = " update coitem_mst " & _
                                " set stat='O'" & _
                                " Where co_num = " & sTirnakEkle(dr4.Item("co_num")) & _
                                " And co_line = " & dr4.Item("co_line") & _
                                " And Stat <> 'O'" & _
                                " AND site_ref=" & sTirnakEkle("Default")
                    db.RunSql(sQuery)

                    sQuery = "Insert Into DCCO_mst" & _
                          " ( site_Ref, termid, trans_date, emp_num,trans_type,co_num,co_line, " & _
                          " item, qty_shipped, qty_returned, whse, loc, lot, stat, override, co_release," & _
                          " reason_code,u_m,rtn_to_stk,shipper_num,NoteExistsFlag,RecordDate,RowPointer," & _
                          " ErrorMessage,CanOverride,CreatedBy,UpdatedBy,CreateDate,remote_site_lot_process,create_loc,InWorkflow" & " )" & _
                          " Values (" & sTirnakEkle("Default") & _
                          ",Null" & "," & _
                          "getdate()" & "," & _
                          "'EDI'" & "," & _
                          "'1'" & "," & _
                          sTirnakEkle(dr4.Item("co_num")) & "," & _
                          dr4.Item("co_line") & "," & _
                          sTirnakEkle(dr4.Item("item")) & "," & _
                          -1 * CDbl(dr4.Item("qty")) & "," & _
                          "0" & "," & _
                          sTirnakEkle(dr4.Item("whse")) & "," & _
                          sTirnakEkle(dr4.Item("loc")) & "," & _
                          sTirnakEkle(dr4.Item("lot")) & "," & _
                          "'P'" & "," & _
                          "1" & "," & _
                          dr4.Item("co_release") & "," & _
                          "'IPT'" & "," & _
                          sTirnakEkle(dr4.Item("u_m")) & "," & _
                          0 & "," & _
                          "Null" & "," & _
                          0 & "," & _
                          sTarih(Date.Now) & "," & _
                          "NEWID()" & "," & _
                          "Null" & "," & _
                          1 & "," & _
                          sTirnakEkle("sa") & "," & _
                          sTirnakEkle("sa") & "," &
                          sTarih(Date.Now) & "," & _
                          "1" & "," & _
                          0 & "," & _
                          0 & ")"

                    db.RunSql(sQuery)


                    If db.Transaction Then
                        db.CommitTransaction()
                    End If


                    '----------------------------------  post işlemi -------------------------------------------------
                    Try

                        ssorgu = "DECLARE     @return_value int, @Infobar InfobarType " & _
                        " EXEC  @return_value = [dbo].[SetSiteSp] @Site = '" & "Default" & "', @Infobar = @Infobar OUTPUT "
                        db.RunSql(ssorgu)

                        ssorgu = " declare @a1 nvarchar(2800)" & _
                                         " set @a1 = NULL" & _
                                         " exec [dbo].[DccoPSp] @FromEdi = 0, @ProcessId = NULL," & _
                                         "@PostDate = '" & Now.Date.ToString("yyyy-MM-dd") & "', @Infobar = @a1 output" & _
                                         " select  @a1"
                        db.RunSql(ssorgu)

                    Catch ex As Exception

                        MessageBox.Show(ex.Message)

                        ssorgu = "DECLARE     @return_value int, @Infobar InfobarType " & _
                        " EXEC  @return_value = [dbo].[SetSiteSp] @Site = '" & "Default" & "', @Infobar = @Infobar OUTPUT "
                        db.RunSql(ssorgu)

                        ssorgu = " declare @a1 nvarchar(2800)" & _
                                           " set @a1 = NULL" & _
                                           " exec [dbo].[DccoPSp] @FromEdi = 0, @ProcessId = NULL," & _
                                           "@PostDate = '" & Now.Date.ToString("yyyy-MM-dd") & "', @Infobar = @a1 output" & _
                                           " select  @a1"

                        db.RunSql(ssorgu)

                    End Try

                Next

                'Offitembl güncelleniyor...

                Dim dtIrsaliye2 As DataTable

                sQuery = "Select DHZ969 AS SEVKNO, CEKLIST, DDAITX  " & _
                         " From tmpIrsaliyeIptal" & _
                         " WHERE DHZ969 = " & txtSevkNo.Text & _
                         " Group By DHZ969, CEKLIST, DDAITX"

                dtIrsaliye = db.RunSql(sQuery)

                If Not dtIrsaliye Is Nothing AndAlso dtIrsaliye.Rows.Count > 0 Then

                    sQuery = "Select DHA3CD, DHCANB, DHB9CD, DHF1CD, DDAITX, Sum(DDARQT) As DDARQT, date_seq" & _
                        " From tmpIrsaliyeIptal" & _
                        " Where DHZ969=" & txtSevkNo.Text & _
                        " Group By DHA3CD, DHCANB, DHB9CD, DHF1CD, DDAITX, date_seq"

                    dtIrsaliye2 = db.RunSql(sQuery)

                    If Not dtIrsaliye2 Is Nothing AndAlso dtIrsaliye2.Rows.Count > 0 Then

                        For i As Integer = 0 To dtIrsaliye2.Rows.Count - 1

                            With dtIrsaliye2.Rows(i)

                                sQuery = " Update OFFITEMBL" & _
                                            " Set DDARQK= DDARQK - " & .Item("DDARQT").ToString & _
                                            " Where CANBK=" & sTirnakEkle(.Item("DHCANB").ToString) & _
                                            " And B9CDK=" & sTirnakEkle(.Item("DHB9CD").ToString) & _
                                            " And GATE IN (" & sTirnakEkle(.Item("DHF1CD").ToString) & ",'')" & _
                                            " And AITXK=" & sTirnakEkle(.Item("DDAITX").ToString)

                                db.RunSql(sQuery)

                            End With

                        Next i

                    End If

                End If

            End If

            Dim MaxTransNum As Integer
            Dim param As New ArrayList
            Dim paramout As New ArrayList
            Dim dtTmp As DataTable

            sQuery = "select * from lot_loc where qty_on_hand >0 and whse = 'GEFC' and lot = '" & txtSevkNo.Text & "'"
            dt = db.RunSql(sQuery)

            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows


                    Dim trans_type As String = 3
                    Dim Gun As String = Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff").ToString
                    '& " 00:00:00:000"

                    sQuery = " select isnull(max(trans_num),0)+1 from dcitem "
                    dtTmp = db.RunSql(sQuery)
                    If Not dtTmp Is Nothing AndAlso dtTmp.Rows.Count > 0 Then
                        MaxTransNum = CInt(dtTmp.Rows(0)(0).ToString)
                    End If

                    param.Clear()
                    param.Add(New SqlClient.SqlParameter("@trans_num", MaxTransNum))
                    param.Add(New SqlClient.SqlParameter("@trans_date", Gun))
                    param.Add(New SqlClient.SqlParameter("@trans_type", 2))
                    param.Add(New SqlClient.SqlParameter("@item", row.Item("item")))
                    param.Add(New SqlClient.SqlParameter("@loc", "GEFCO"))
                    param.Add(New SqlClient.SqlParameter("@lot", txtSevkNo.Text))
                    param.Add(New SqlClient.SqlParameter("@count_qty", row.Item("qty_on_hand")))
                    param.Add(New SqlClient.SqlParameter("@whse", "GEFC"))
                    param.Add(New SqlClient.SqlParameter("@siteref", "Default"))
                    param.Add(New SqlClient.SqlParameter("@emp_num", KullaniciId))
                    param.Add(New SqlClient.SqlParameter("@document_num", txtSevkNo.Text))

                    'Dim p10 As New SqlClient.SqlParameter("@rowsAffected", SqlDbType.Int)
                    'p10.Direction = ParameterDirection.Output
                    'param.Add(p10)

                    Dim p5 As New SqlClient.SqlParameter("@pResult", SqlDbType.NVarChar, 500)
                    p5.Direction = ParameterDirection.Output
                    param.Add(p5)


                    If db.RunSp("[TRM_Dcitem_Insert_Irs_Iptal]", param, 1, False) = True Then


                    Else


                        sQuery = "DELETE FROM dcitem where trans_num = " & MaxTransNum
                        db.RunSql(sQuery)

                    End If

                Next

            End If

           



            btnSorgula_Click(sender, e)

            txtSevkNo.Text = String.Empty

        Catch ex As Exception

            If db.Transaction Then
                db.RollbackTransaction()
            End If

            MessageBox.Show("Islem Gerçeklestirilemedi" & vbNewLine & "    Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MessageBox.Show(ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick, GridEX1.CurrentCellChanged, GridEX1.Click

        GridSec(GridEX1, "shipment_id", sender, e)

    End Sub


    Private Sub frmIrsaliyeIptal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ''TranslateFrm(Me, True)
    End Sub

End Class