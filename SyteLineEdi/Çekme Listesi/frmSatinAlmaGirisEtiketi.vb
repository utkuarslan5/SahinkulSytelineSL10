Public Class frmSatinAlmaGirisEtiketi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim ds As New DataSet

    'Implements RefreshMalzemeSevkEtme
    Dim dt As DataTable
    Dim Row As Janus.Windows.GridEX.GridEXRow
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Public Function RandomNumber() As String
        Dim a As New Random
        Return a.Next
    End Function

    Public Function SpecialCharDelete(ByVal Word As String) As String
        Try

            Dim WordClear As String = Word

            WordClear = WordClear.Replace("'", "")
            'WordClear = WordClear.Replace("'", "''")
            'WordClear = WordClear.Replace(",", ".")

            Return WordClear

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Private Sub BackColorClear()
        Try

            For Each Row As Janus.Windows.GridEX.GridEXRow In GridEXBarkod.GetRows
                Row.BeginEdit()
                Dim fs As New Janus.Windows.GridEX.GridEXFormatStyle
                fs.BackColor = Color.FromArgb(255, 255, 192)
                'Row.RowStyle = fs
                'e.Row.Cells("Yeni Miktar").FormatStyle.BackColor = Color.Red
                Row.Cells("Etiket_Miktari").FormatStyle = fs
                Row.Cells("Kopya_Sayisi").FormatStyle = fs
                Row.EndEdit()
            Next

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub btnListele_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnListele.Click
        Try
            Listele()

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub btnTemizle_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnTemizle.Click
        Try

            EdtSaticiKodu.Text = String.Empty
            EdtSaticiAdi.Text = String.Empty
            EdtIrsaliyeNo.Text = String.Empty

            GridEXBarkod.DataSource = Nothing
            EdtSaticiKodu.Focus()

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub btnYazdir_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles btnYazdir.Click
        Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection) 'Access connection

        Try

            If GridEXBarkod.GetCheckedRows.Length = 0 Then
                MsgBox("Lütfen listeden seçim yapınız !", MsgBoxStyle.Exclamation, "Hata")
            Else

                BackColorClear()

                For Each Row As Janus.Windows.GridEX.GridEXRow In GridEXBarkod.GetCheckedRows
                    If Row.Cells("Etiket_Miktari").Text = String.Empty OrElse Not IsNumeric(Row.Cells("Etiket_Miktari").Text) OrElse (Row.Cells("Etiket_Miktari").Text <> String.Empty AndAlso CDec(Row.Cells("Etiket_Miktari").Text) <= 0) Then
                        'Row.BeginEdit()
                        'Dim fs As New Janus.Windows.GridEX.GridEXFormatStyle
                        'fs.BackColor = Color.Red
                        ''Row.RowStyle = fs
                        ''e.Row.Cells("Yeni Miktar").FormatStyle.BackColor = Color.Red
                        'Row.Cells("Etiket_Miktari").FormatStyle = fs
                        'Row.EndEdit()
                        MsgBox("Lütfen etiket miktarını giriniz !", MsgBoxStyle.Exclamation, "Hata")
                        Exit Sub
                    End If

                    If Row.Cells("Kopya_Sayisi").Text = String.Empty OrElse Not IsNumeric(Row.Cells("Kopya_Sayisi").Text) OrElse Row.Cells("Kopya_Sayisi").Text.IndexOf(".").ToString <> "-1" OrElse (Row.Cells("Kopya_Sayisi").Text <> String.Empty AndAlso CDec(Row.Cells("Kopya_Sayisi").Text) <= 0) Then
                        'Row.BeginEdit()
                        'Dim fs As New Janus.Windows.GridEX.GridEXFormatStyle
                        'fs.BackColor = Color.Red
                        ''Row.RowStyle = fs
                        ''e.Row.Cells("Yeni Miktar").FormatStyle.BackColor = Color.Red
                        'Row.Cells("Kopya_Sayisi").FormatStyle = fs
                        'Row.EndEdit()
                        MsgBox("Lütfen kopya sayısını giriniz !", MsgBoxStyle.Exclamation, "Hata")
                        Exit Sub
                    End If
                Next

                '  If MsgBox("Yazdırma işlemi yapılsın mı ?", MsgBoxStyle.YesNo, "Onay") = MsgBoxResult.Yes Then

                Cursor = Cursors.WaitCursor

                Dim sStr As String

                Dim SEISLEMNO As String = RandomNumber()
                Dim IRSALIYE_NO As String = String.Empty
                Dim SATICI_KODU As String = String.Empty
                Dim SATICI_ADI As String = String.Empty
                Dim MALZEME_KODU As String = String.Empty
                Dim MALZEME_TANIMI As String = String.Empty
                Dim MIKTAR As Decimal = 0
                Dim LOT_NO As String = String.Empty
                Dim TARIH As String = String.Empty

                db.BeginTransaction()
                dbAccess.BeginTransaction()

                For Each row As Janus.Windows.GridEX.GridEXRow In GridEXBarkod.GetCheckedRows

                    If row.Cells.Row.RowType = Janus.Windows.GridEX.RowType.Record Then

                        IRSALIYE_NO = row.Cells("Irsaliye_No").Text
                        SATICI_KODU = SpecialCharDelete(row.Cells("Satici_Kodu").Text)
                        SATICI_ADI = SpecialCharDelete(row.Cells("Satici_Adi").Text)
                        MALZEME_KODU = SpecialCharDelete(row.Cells("Malzeme_Kodu").Text)
                        MALZEME_TANIMI = SpecialCharDelete(row.Cells("Malzeme_Tanimi").Text)
                        MIKTAR = CDec(row.Cells("Etiket_Miktari").Text)
                        LOT_NO = SpecialCharDelete(row.Cells("Lot_No").Text)
                        TARIH = SpecialCharDelete(row.Cells("Tarih").Text)

                        sStr = " update matltran set Uf_Print='P' " & _
                                " where trans_num in " & _
                                "  ( SELECT m.trans_num  " & _
                                " FROM    dbo.matltran m " & _
                                " LEFT JOIN po p ON m.ref_num = p.po_num " & _
                                " WHERE   m.trans_type = 'R' " & _
                                "    And m.ref_type = 'P' " & _
                                "    And p.vend_num =" & sTirnakEkle(SATICI_KODU) & _
                                "    And m.document_num =" & sTirnakEkle(IRSALIYE_NO) & _
                                "    And m.item=" & sTirnakEkle(MALZEME_KODU) & _
                                "    And m.lot=" & sTirnakEkle(LOT_NO) & _
                                "  ) "
                        db.RunSql(sStr, True)

                        For Cnt As Integer = 1 To CLng(row.Cells("Kopya_Sayisi").Text)

                            sStr = " insert into LABELDB " & _
                                   " ( " & _
                                   "  SEISLEMNO, IRSALIYENO,  " & _
                                   "  SATICIKODU, SATICIADI, " & _
                                   "  MALZEMEKODU, MALZEMETANIMI, " & _
                                   "  MIKTAR, LOT_NO, TARIH " & _
                                   " ) " & _
                                   " values " & _
                                   " ( " & _
                                   "  '" & SEISLEMNO & "', '" & IRSALIYE_NO & "', " & _
                                   "  '" & SATICI_KODU & "', '" & SATICI_ADI & "', " & _
                                   "  '" & MALZEME_KODU & "', '" & MALZEME_TANIMI & "', " & _
                                   "  " & MIKTAR & ", '" & LOT_NO & "', " & _
                                   "  '" & TARIH & "'" & _
                                   " ) "

                            dbAccess.RunSql(sStr, True)

                        Next

                    End If

                Next

                If dbAccess.Transaction Then
                    dbAccess.CommitTransaction()
                End If

                sStr = " SELECT LABELDB.* FROM LABELDB " & _
                       " WHERE LABELDB.[SEISLEMNO]='" & SEISLEMNO & "'"

                AccessLogOnDatabase("Malzeme_Etiketi.rpt", dbAccess.RunSql(sStr))

                frmRaporOzet.CrystalReportViewer1.ReportSource = frmRaporOzet.reportDocument1
                frmRaporOzet.CrystalReportViewer1.ShowRefreshButton = False
                frmRaporOzet.ShowDialog()

                sStr = "DELETE FROM LABELDB WHERE SEISLEMNO='" & SEISLEMNO & "'"

                dbAccess.RunSql(sStr, True)

                If db.Transaction Then
                    db.CommitTransaction()
                End If

                Listele()

                'grdLbl.SelectedItems.Clear()
                'End If

            End If

        Catch ex As Exception
            If dbAccess.Transaction Then
                dbAccess.RollbackTransaction()
            End If
            If db.Transaction Then
                db.RollbackTransaction()
            End If
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")
        Finally
            Cursor = Cursors.Arrow
        End Try

        'grdLbl.SelectedItems.Clear()
    End Sub

    Private Sub DurumLoad()
        Try

            Dim Durum_Kodu() As String = {"A", "P", " "}
            Dim Durum_Adı() As String = {"Tümü", "Yazdırıldı", "Yazdırılmadı"}

            Dim dt As New DataTable

            If dt.Columns.Count = 0 Then
                dt.Columns.Add("Durum Kodu", System.Type.GetType("System.String"))
                dt.Columns.Add("Durum Adı", System.Type.GetType("System.String"))
            End If

            dt.Rows.Clear()

            For Each str As String In Durum_Adı
                Dim Row As DataRow
                Row = dt.NewRow
                Row("Durum Adı") = str
                Row("Durum Kodu") = Durum_Kodu(dt.Rows.Count)
                dt.Rows.InsertAt(Row, dt.Rows.Count)
            Next

            cmbDurum.DataSource = dt
            cmbDurum.Value = Durum_Kodu(0).ToString
            cmbDurum.Text = Durum_Adı(0).ToString
            cmbDurum.DropDownList.Columns(1).Caption = "Durum"

        Catch ex As Exception
            Throw ex
            'MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub EdtIrsaliyeNo_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles EdtIrsaliyeNo.ButtonClick
        Try

            If EdtSaticiAdi.Text = String.Empty Then Throw New Exception("Lütfen ilk önce satıcı kodu seçiniz !")

            sQuery = " SELECT distinct m.document_num AS	  [İrsaliye No], " & _
                     "        m.document_num AS	  [İrsaliye No]" & _
                     " FROM dbo.matltran m " & _
                     " LEFT JOIN po p ON m.ref_num=p.po_num "
            sQuery &= IIf(rbSatinalma.Checked, "", "left join tr_fason f on m.ref_num=f.job ") & " WHERE 1=1 AND "
            sQuery &= IIf(rbSatinalma.Checked, "m.trans_type+m.ref_type = 'RP'", "m.trans_type+m.ref_type = 'FJ'") & _
                IIf(rbSatinalma.Checked, " and p.vend_num=", " and f.wc=")
            sQuery &= IIf(rbSatinalma.Checked, sTirnakEkle(EdtSaticiKodu.Text.PadLeft(7, String.Empty).ToString), sTirnakEkle(EdtSaticiKodu.Text.Trim)) & _
                " GROUP BY " & _
                " m.document_num , p.vend_num ,  " & _
                " m.item, m.lot " & _
                " HAVING(SUM(m.qty) > 0)"

            FindFormCagir(sQuery, "İrsaliye No", "İrsaliye No", EdtIrsaliyeNo.Text, "")

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub EdtSaticiKodu_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles EdtSaticiKodu.ButtonClick
        Try

            sQuery = " SELECT vend_num as [Satıcı Kodu], name as [Satıcı Adı] FROM vendaddr"

            FindFormCagir(sQuery, "Satıcı Kodu", "Satıcı Adı", EdtSaticiKodu.Text, EdtSaticiAdi.Text)

            If EdtSaticiAdi.Text <> String.Empty Then
                EdtIrsaliyeNo.Text = String.Empty
                EdtIrsaliyeNo.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub EdtSaticiKodu_KeyDown(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles EdtSaticiKodu.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then

                EdtSaticiAdi.Text = String.Empty

                'If EdtSaticiAdi.Text = String.Empty Then Throw New Exception("Lütfen ilk önce satıcı kodu seçiniz !")

                sQuery = " SELECT vend_num as [Satıcı Kodu], name as [Satıcı Adı] FROM vendaddr" & _
                         " where vend_num=" & sTirnakEkle(EdtSaticiKodu.Text.PadLeft(7, String.Empty).ToString)

                dt = db.RunSql(sQuery)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    EdtSaticiAdi.Text = dt.Rows(0)("Satıcı Adı").ToString
                    EdtIrsaliyeNo.Text = String.Empty
                    EdtIrsaliyeNo.Focus()
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub EdtSaticiKodu_KeyPress(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles EdtSaticiKodu.KeyPress, EdtIrsaliyeNo.KeyPress
        Try

            Dim CtrlTmp As Control = CType(sender, Janus.Windows.GridEX.EditControls.EditBox)

            Select Case CtrlTmp.Name
                Case EdtSaticiKodu.Name
                    '*****If EdtIsemri.Text = String.Empty Then
                    '*****EdtSaticiAdi.Text = String.Empty
                    '*****EdtIrsaliyeNo.Text = String.Empty
                    'End If

                    'EdtIsMerkezi.Text = String.Empty
                    'EdtIsMerkeziTanim.Text = String.Empty
                    'EdtBilesen.Text = String.Empty
                    'EdtBilesenTanim.Text = String.Empty
                Case EdtIrsaliyeNo.Name
                    '*****If EdtBilesen.Text = String.Empty Then
                    'EdtBilesenTanim.Text = String.Empty
                    'End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub EdtSaticiKodu_TextChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles EdtSaticiKodu.TextChanged
        Try

            If EdtSaticiKodu.Text = String.Empty Then

                EdtSaticiAdi.Text = String.Empty
                EdtIrsaliyeNo.Text = String.Empty

            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub frmSatinAlmaGirisEtiketi_KeyDown(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.KeyCode = Keys.F4 Then
                If Me.ActiveControl Is EdtSaticiKodu Then
                    EdtSaticiKodu_Click(sender, e)
                ElseIf Me.ActiveControl Is EdtIrsaliyeNo Then
                    EdtIrsaliyeNo_Click(sender, e)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub frmSatinAlmaGirisEtiketi_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        dtTarihBaslar.Value = Today
        dtTarihBiter.Value = Today
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("tr-TR")
            'Bolgesel Ayarlar Opsiyonu
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator = ","

            'If dtgrd.Columns.Count = 0 Then
            '    For Each Col As Janus.Windows.GridEX.GridEXColumn In GridEXBarkod.RootTable.Columns
            '        dtgrd.Columns.Add(Col.Key, Col.Type)
            '    Next
            'End If
            'GridEXBarkod.DataSource = dtgrd
            DurumLoad()
            EdtSaticiKodu.Focus()

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")

        End Try
    End Sub

    Private Sub GridEXBarkod_CellEdited(ByVal sender As Object, _
        ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEXBarkod.CellEdited
        Try

            If GridEXBarkod.SelectedItems.Count > 0 Then

                If GridEXBarkod.SelectedItems(0).RowType = Janus.Windows.GridEX.RowType.Record Then

                    GridEXBarkod.SelectedItems(0).GetRow.BeginEdit()

                    Dim row As Janus.Windows.GridEX.GridEXRow = GridEXBarkod.SelectedItems(0).GetRow

                    If e.Column.DataMember = "Etiket_Miktari" Then

                        If (row.Cells("Etiket_Miktari").Text <> String.Empty AndAlso Not IsNumeric(row.Cells("Etiket_Miktari").Text)) OrElse (row.Cells("Etiket_Miktari").Text <> String.Empty AndAlso CDec(row.Cells("Etiket_Miktari").Text) <= 0) Then
                            GridEXBarkod.SelectedItems(0).GetRow.CancelEdit()
                            Exit Sub
                        Else
                            row.Cells("Etiket_Miktari").Text = row.Cells("Etiket_Miktari").Text
                            row.Cells("Etiket_Miktari").Value = row.Cells("Etiket_Miktari").Text
                        End If

                    End If

                    If e.Column.DataMember = "Kopya_Sayisi" Then

                        If (row.Cells("Kopya_Sayisi").Text <> String.Empty AndAlso Not IsNumeric(row.Cells("Kopya_Sayisi").Text)) OrElse row.Cells("Kopya_Sayisi").Text.IndexOf(".").ToString <> "-1" OrElse (row.Cells("Kopya_Sayisi").Text <> String.Empty AndAlso CDec(row.Cells("Kopya_Sayisi").Text) <= 0) Then
                            GridEXBarkod.SelectedItems(0).GetRow.CancelEdit()
                            Exit Sub
                        Else
                            row.Cells("Kopya_Sayisi").Text = row.Cells("Kopya_Sayisi").Text
                            row.Cells("Kopya_Sayisi").Value = row.Cells("Kopya_Sayisi").Text
                        End If

                    End If

                    GridEXBarkod.SelectedItems(0).GetRow.EndEdit()

                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Exclamation, "Hata")
        End Try
    End Sub

    Private Sub Listele()
        'Implements RefreshMalzemeSevkEtme.Listele

        Try

            sQuery = " SELECT m.document_num AS	  [Irsaliye_No], "

            sQuery &= IIf(rbSatinalma.Checked, "p.vend_num AS [Satici_Kodu],", " f.wc AS [Satici_Kodu],") & _
                  "       v.name		  AS      [Satici_Adi], " & _
                  "       m.item          AS      [Malzeme_Kodu], " & _
                  "       i.description   AS      [Malzeme_Tanimi],	" & _
                  "       sum(m.qty)      AS      [Miktar], " & _
                  "       m.lot           AS      [Lot_No], " & _
                  "       max(m.trans_date) AS    [Tarih], " & _
                  "       (case when max(isnull(m.Uf_Print,''))='P' then 'Yazdırıldı' else 'Yazdırılmadı' end) AS [Durum], " & _
                  "       '' as [Etiket_Miktari], " & _
                  "       '' as [Kopya_Sayisi] " & _
                  "    FROM dbo.matltran m " & _
                  "    LEFT JOIN po p ON m.ref_num=p.po_num "

            sQuery &= IIf(rbSatinalma.Checked, "    LEFT JOIN vendaddr v ON p.vend_num=v.vend_num  ", "left join tr_fason f on m.ref_num=f.job    LEFT JOIN vendaddr v ON f.wc=v.vend_num  ") & _
                 "    LEFT JOIN item i ON m.item=i.item " & _
                 "    WHERE  1 = 1 AND "

            sQuery &= IIf(rbSatinalma.Checked, "m.trans_type+m.ref_type = 'RP'", "m.trans_type+m.ref_type = 'FJ'")

            sQuery &= " GROUP BY " & _
                      " m.document_num , "

            sQuery &= IIf(rbSatinalma.Checked, "p.vend_num, ", "f.wc, ") & _
                      " v.name, m.item, i.description, m.lot " & _
                      " HAVING(SUM(m.qty) > 0) "

            If EdtSaticiKodu.Text <> "" Then

                sQuery &= IIf(rbSatinalma.Checked, " And p.vend_num=" & sTirnakEkle(EdtSaticiKodu.Text.PadLeft(7, String.Empty)), "And f.wc=" & sTirnakEkle(EdtSaticiKodu.Text.Trim))

            End If

            If EdtIrsaliyeNo.Text <> "" Then

                sQuery &= " And m.document_num=" & sTirnakEkle(EdtIrsaliyeNo.Text)

            End If

            sQuery &= " And max(dbo.formatdatetime(m.trans_date, 'YYYY-MM-DD'))>=" & sTirnakEkle(dtTarihBaslar.Value.ToString("yyyy-MM-dd"))

            sQuery &= " And max(dbo.formatdatetime(m.trans_date, 'YYYY-MM-DD'))<=" & sTirnakEkle(dtTarihBiter.Value.ToString("yyyy-MM-dd"))

            If cmbDurum.Value.ToString <> "A" Then

                sQuery &= " And max(isnull(m.Uf_Print,''))='" & cmbDurum.Value.ToString & "'"

            End If

            dt = db.RunSql(sQuery)

            For Each Row As DataRow In dt.Rows
                Row("Durum") = IIf(Row("Durum").ToString = "Yazdırıldı" Or Row("Durum").ToString = "Yazdirildi", "Yazdırıldı", "Yazdırılmadı")
            Next

            GridEXBarkod.DataSource = Nothing

            GridEXBarkod.DataSource = dt

        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    #End Region 'Methods

End Class