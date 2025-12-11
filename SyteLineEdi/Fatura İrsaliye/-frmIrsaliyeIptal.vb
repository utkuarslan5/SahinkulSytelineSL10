Public Class frmIrsaliyeIptal

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt As New DataTable
    Dim dtIrsaliye As New DataTable
    Dim dtMatltran As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnOlustur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOlustur.Click
        Dim bEtiketIptal As Boolean
        Dim bCekmeli As Boolean
        Dim bEtiketDurum As Boolean

        Try

            If MessageBox.Show("İrsaliye İptal Edilecek Onaylıyormusunuz?", "Ekip Mapics", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

                Dim row As Janus.Windows.GridEX.GridEXRow
                Dim Sayac As Integer = 0
                checkedRows = Me.GridEX1.GetCheckedRows()

                If checkedRows.Length = 0 Then

                    MessageBox.Show("Lütfen  Seçim Yapınız!")

                    Exit Sub

                Else

                    dbAccess.RunSql("Delete From tmpIrsaliyeIptal")

                    For Each row In checkedRows

                        sQuery = "INSERT INTO tmpIrsaliyeIptal  " & _
                        " (DHCVNB, DHZ969, DHCANB, DHIVNB, " & _
                        " DHB9CD, DHBYTX, DHAFVN, DHCQCD, DHF1CD, " & _
                        " DHCDTX, DDAITX, DDALTX, DDARQT, DDFCNB, " & _
                        " DDAAGP, DDAAGK, DDDHCD, DHA3CD, co_release, date_seq,CEKLIST)" & _
                        "Values" & _
                        "(" & sTirnakEkle(row.Cells("DHCVNB").Text) & _
                        "," & sTirnakEkle(row.Cells("DHZ969").Text) & _
                        "," & sTirnakEkle(row.Cells("DHCANB").Text) & _
                        "," & sTirnakEkle(row.Cells("DHIVNB").Text) & _
                        "," & sTirnakEkle(row.Cells("DHB9CD").Text) & _
                        "," & sTirnakEkle(row.Cells("DHBYTX").Text) & _
                        "," & sTirnakEkle(row.Cells("DHAFVN").Text) & _
                        "," & sTirnakEkle(row.Cells("DHCQCD").Text) & _
                        "," & sTirnakEkle(row.Cells("DHF1CD").Text) & _
                        "," & sTirnakEkle(row.Cells("DHCDTX").Text) & _
                        "," & sTirnakEkle(row.Cells("DDAITX").Text) & _
                        "," & sTirnakEkle(row.Cells("DDALTX").Text) & _
                        "," & row.Cells("DDARQT").Text & _
                        "," & row.Cells("DDFCNB").Text & _
                        "," & row.Cells("DDAAGP").Text & _
                        "," & row.Cells("DDAAGK").Text & _
                        "," & sTirnakEkle(row.Cells("DDDHCD").Text) & _
                        "," & sTirnakEkle(row.Cells("DHA3CD").Text) & _
                        "," & row.Cells("co_release").Text & _
                        "," & row.Cells("date_seq").Text & _
                        "," & row.Cells("CEKLIST").Text & _
                        ")"

                        dbAccess.RunSql(sQuery)

                    Next 'Grid Döngüsü

                End If 'Grid

                'Access den CEKLIST ve SEVKNO bazında gruplu okuma işlemi
                'Ana döngü başlangıcı

                sQuery = "Select DHZ969 AS SEVKNO, CEKLIST  " & _
                            " From tmpIrsaliyeIptal" & _
                            " Group By DHZ969, CEKLIST"

                dtIrsaliye = dbAccess.RunSql(sQuery)

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

                        'Offitembl güncelleniyor...

                        sQuery = "Select DHA3CD, DHCANB, DHB9CD, DHF1CD, DDAITX, Sum(DDARQT) As DDARQT " & _
                                    " From tmpIrsaliyeIptal" & _
                                    " Where DHZ969=" & rowDt.Item("SEVKNO").ToString & _
                                    " Group By DHA3CD, DHCANB, DHB9CD, DHF1CD, DDAITX "

                        dt = dbAccess.RunSql(sQuery)

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                            For i As Integer = 0 To dt.Rows.Count - 1

                                With dt.Rows(i)

                                    sQuery = " Update OFFITEMBL" & _
                                                " Set DDARQK=DDARQK - " & .Item("DDARQT").ToString & _
                                                " Where CANBK=" & sTirnakEkle(.Item("DHCANB").ToString) & _
                                                " And B9CDK=" & sTirnakEkle(.Item("DHB9CD").ToString) & _
                                                " And GATE IN (" & sTirnakEkle(.Item("DHF1CD").ToString) & ",'')" & _
                                                " And AITXK=" & sTirnakEkle(.Item("DDAITX").ToString)

                                    db.RunSql(sQuery)

                                End With

                            Next i

                        End If

                        'Ayniyatlar Geri Koyuluyor...

                        sQuery = "Select document_num, m.item As MALZEME, description As TANIM, loc, lot,  whse As AMBAR, u_m As OLCUBIRIMI, sum(qty)  As MIKTAR " & _
                                    " From matltran m" & _
                                    " Left Join item i On m.item=i.item" & _
                                    " Where document_num=" & sTirnakEkle(rowDt.Item("SEVKNO").ToString) & _
                                    " And trans_type+ref_type='GI'" & _
                                    " Group By document_num, whse, m.item, description, loc, lot, u_m"

                        dt = db.RunSql(sQuery)

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                            dtAyniyat = dt

                            frmAyniyatIade.ShowDialog()

                            If frmAyniyatIade.bAyniyatIade Then

                                sQuery = "Delete " & _
                                        " From Dcitem" & _
                                        " Where document_num=" & sTirnakEkle(rowDt.Item("SEVKNO").ToString)

                                db.RunSql(sQuery)

                                For i As Integer = 0 To dt.Rows.Count - 1

                                    With dt.Rows(i)

                                        sQuery = " INSERT INTO dcitem " & _
                                            " (trans_num ,stat ,trans_date ,emp_num" & _
                                            " ,trans_type ,item ,whse ,loc ,lot" & _
                                            " ,override ,count_qty ,reason_code" & _
                                            " ,u_m ,document_num)" & _
                                    " VALUES " & _
                                           "( " & SeriNoAl("AYN") & _
                                           " ,'P'" & _
                                           " ,getdate()" & _
                                           " ,'EDI'" & _
                                           " ,'3'" & _
                                           " , " & sTirnakEkle(.Item("MALZEME").ToString) & _
                                           " , " & sTirnakEkle(.Item("AMBAR").ToString) & _
                                           " , " & sTirnakEkle(.Item("loc").ToString) & _
                                           " , " & sTirnakEkle(.Item("lot").ToString) & _
                                           " ,1" & _
                                           " ," & -1 * CDbl(.Item("MIKTAR").ToString) & _
                                           " ,'AYR' " & _
                                           " ," & sTirnakEkle(.Item("OLCUBIRIMI").ToString) & _
                                           " ," & sTirnakEkle("X" & rowDt.Item("SEVKNO").ToString) & ")"

                                        db.RunSql(sQuery)

                                    End With

                                Next i

                            End If

                        End If ' Ayniyat IF

                        'co_ship reason alanı null yapıldı
                        sQuery = " Update co_ship" & _
                                    " Set reason_text='X'" & _
                                    " Where reason_text=" & sTirnakEkle(rowDt.Item("SEVKNO").ToString)

                        db.RunSql(sQuery)

                        'Sevkiyattaki ürünler için iade kayıtları (DCCO)
                        sQuery = "Select DHA3CD, DHCVNB, DDFCNB, DDAITX, DHIVNB, DDARQT,co_release, DHCQCD " & _
                                    " From tmpIrsaliyeIptal" & _
                                    " Where DHZ969=" & rowDt.Item("SEVKNO").ToString & _
                                    " And CEKLIST=" & rowDt.Item("CEKLIST").ToString

                        dt = dbAccess.RunSql(sQuery)

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                            For k As Integer = 0 To dt.Rows.Count - 1

                                With dt.Rows(k)

                                    Dim mtLot As String
                                    Dim mtLoc As String
                                    Dim mtTransnum As Double
                                    sQuery = " Select Top 1 trans_num, loc, lot" & _
                                                " From Matltran" & _
                                                " Where trans_type+ref_type='SO'" & _
                                                " And ref_num=" & sTirnakEkle(.Item("DHCVNB").ToString) & _
                                                " And ref_line_suf=" & .Item("DDFCNB").ToString & _
                                                " And trans_date=" & sTirnakEkle(CDate(.Item("DHIVNB").ToString).ToString("yyyy-MM-dd")) & _
                                                " And qty=(-1 * " & .Item("DDARQT").ToString & ")" & _
                                                " And whse=" & sTirnakEkle(.Item("DHA3CD").ToString) & _
                                                " And document_num is null"

                                    dtMatltran = db.RunSql(sQuery)

                                    If Not dtMatltran Is Nothing AndAlso dtMatltran.Rows.Count > 0 Then

                                        mtLoc = dtMatltran.Rows(0).Item("loc").ToString

                                        mtLot = dtMatltran.Rows(0).Item("lot").ToString

                                        mtTransnum = dtMatltran.Rows(0).Item("trans_num").ToString

                                        sQuery = "Update Matltran" & _
                                                " Set document_num=" & sTirnakEkle("X" & rowDt.Item("SEVKNO").ToString) & _
                                                " Where trans_num=" & mtTransnum

                                        db.RunSql(sQuery)

                                    Else

                                        mtLoc = ""

                                        mtLot = ""

                                    End If

                                    sQuery = " update co " & _
                                                " set stat='O'" & _
                                                " Where co_num=" & sTirnakEkle(.Item("DHCVNB").ToString) & _
                                                " And Stat='C'"

                                    db.RunSql(sQuery)

                                    sQuery = " update coitem " & _
                                                " set stat='O'" & _
                                                " Where co_num=" & sTirnakEkle(.Item("DHCVNB").ToString) & _
                                                " And co_line=" & .Item("DDFCNB").ToString & _
                                                " And Stat='C'"

                                    db.RunSql(sQuery)

                                    sQuery = "Insert Into DCCO" & _
                                                " (  termid, trans_date, emp_num,trans_type,co_num,co_line, " & _
                                                " item, qty_shipped, qty_returned, whse, loc, lot, stat, override, co_release," & _
                                                " reason_code,u_m" & " )" & _
                                                " Values (" & _
                                                "Null" & "," & _
                                                "getdate()" & "," & _
                                                "'EDI'" & "," & _
                                                "'1'" & "," & _
                                                sTirnakEkle(.Item("DHCVNB").ToString) & "," & _
                                                .Item("DDFCNB").ToString & "," & _
                                                sTirnakEkle(.Item("DDAITX").ToString) & "," & _
                                                -1 * CDbl(.Item("DDARQT").ToString) & "," & _
                                                "0" & "," & _
                                                sTirnakEkle(.Item("DHA3CD").ToString) & "," & _
                                                sTirnakEkle(mtLoc) & "," & _
                                                sTirnakEkle(mtLot) & "," & _
                                                "'P'" & "," & _
                                                "1" & "," & _
                                                .Item("co_release").ToString & "," & _
                                                "'IPT'" & "," & _
                                                sTirnakEkle(.Item("DHCQCD").ToString) & ")"

                                    db.RunSql(sQuery)

                                End With

                            Next k

                        End If

                        sQuery = "Insert Into tr_co_ship_arc" & _
                                    " Select *" & _
                                        " FRom tr_co_ship" & _
                                        " Where reason_text=" & sTirnakEkle(rowDt.Item("SEVKNO").ToString)

                        db.RunSql(sQuery)

                        sQuery = "Delete " & _
                                " From tr_co_ship" & _
                                " Where reason_text=" & sTirnakEkle(rowDt.Item("SEVKNO").ToString)

                        db.RunSql(sQuery)

        SonrakiKayit:

                    Next rowDt ' Dongu

                End If '

            End If 'Iptal Sorgusu

            btnSorgula_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            sQuery = " Select *" & _
                        " From IrslymstIpt " & _
                        " Where 1=1 "

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & "    and DHCANB='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & "    and DHCANB>='" & txtMusteri1.Text & "'" & _
                                        " and DHCANB<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtAmbar1.Text <> "" Then

                sQuery = sQuery & "    and DHA3CD='" & txtAmbar1.Text & "'"

            End If

            If txtTeslimAlan1.Text <> "" Then

                If txtTeslimAlan2.Text = "" Then

                    sQuery = sQuery & "    and DHB9CD='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery = sQuery & "    and DHB9CD>='" & txtTeslimAlan1.Text & "'" & _
                                        " and DHB9CD<='" & txtTeslimAlan2.Text & "'"
                End If

            End If

            If txtKapi1.Text <> "" Then

                If txtKapi2.Text = "" Then

                    sQuery = sQuery & "    and DHF1CD='" & txtKapi1.Text & "'"

                Else

                    sQuery = sQuery & "    and DHF1CD>='" & txtKapi1.Text & "'" & _
                                        "    and DHF1CD<='" & txtKapi2.Text & "'"

                End If

            End If

            If txtKullanici.Text <> "" Then

                sQuery = sQuery & "    and DHAFVN='" & txtKullanici.Text & "'"

            End If

            If txtSevkNo.Text <> "" Then

                sQuery = sQuery & "    and DHZ969=" & txtSevkNo.Text

            End If

            sQuery = sQuery & " Order By DHCANB, DHB9CD, DHIVNB,DDAITX "

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                             dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                Duzenle(GridEX1)
            Else

                GridEX1.DataSource = Nothing

            End If

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "DHZ969", sender, e)
    End Sub

    Private Sub txtAmbar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmbar1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct whse as Ambar,name as Tanim " & _
                            " From whse" & _
                            " Order By 1"

            FindFormCagir(ssorgu, "Ambar", "Tanim", txtAmbar1.Text, "")

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müşteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müşteri", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri2.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Müşteri,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Müşteri", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    #End Region 'Methods

End Class