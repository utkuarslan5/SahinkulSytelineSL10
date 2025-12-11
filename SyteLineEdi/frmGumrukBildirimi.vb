Public Class frmGumrukBildirimi

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim dt, dtBaslik, dtDetay As New DataTable
    Dim dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnAtamaIptal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAtamaIptal.Click
        Try

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            Dim row As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                For Each row In checkedRows

                    If row.Cells("BeyanNo").Text <> "0" Then

                        sQuery = " Update ShpPack" & _
                            " Set BeyanNo=0" & _
                            " , BeyanTarihi=Null" & _
                            " Where BeyanNo =" & row.Cells("BeyanNo").Text

                        db.RunSql(sQuery)

                    End If

                Next row

            End If

            btnSorgula_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Try

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            sQuery = " Select Distinct *" & _
                        " From BeyannameMst " & _
                        " Where 1=1 "

            If rdbBeyanNoAtanmamis.Checked Then

                sQuery = sQuery & " AND BeyanNo=0"

            ElseIf rdbBeyanNoAtanmis.Checked Then

                sQuery = sQuery & " AND BeyanNo<>0"

            End If

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & " and MusteriNo='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & " and MusteriNo>='" & txtMusteri1.Text & "'" & _
                                      " and MusteriNo<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtFaturaNo1.Text <> "" Then

                If txtFaturaNo2.Text = "" Then

                    sQuery = sQuery & " and FaturaNo=" & txtFaturaNo1.Text & ""

                Else

                    sQuery = sQuery & " and FaturaNo>=" & txtFaturaNo1.Text & _
                                      " and FaturaNo<=" & txtFaturaNo2.Text

                End If

            End If

            If txtBeyanNo1.Text <> "" Then

                If txtBeyanNo2.Text = "" Then

                    sQuery = sQuery & " and BeyanNo=" & txtBeyanNo1.Text & ""

                Else

                    sQuery = sQuery & " and BeyanNo>=" & txtBeyanNo1.Text & _
                                      " and BeyanNo<=" & txtBeyanNo2.Text

                End If

            End If

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

    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click
        Dim sSQL, sTermCode As String
        Dim nTutar, nBirimFyt, nGumrNo, nKapAdedi As Double
        Dim nNetAgr, nBrtAgr, nHacim, nHKSay, nPSay, nKSay As Double

        Dim dBeyanTarihi As Date
        Dim sFaturaNoLar As String = ""
        Dim dGGBTarih As String

        Try

            sSQL = " DELETE FROM ANSGUMBILD"

            dbAccess.RunSql(sSQL)

            sSQL = " DELETE FROM ANSAYNIYAT"

            dbAccess.RunSql(sSQL)

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            Dim row As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            If checkedRows.Length = 0 Then

                MessageBox.Show("Lütfen  Seçim Yapınız!")

                Exit Sub

            Else

                For Each row In checkedRows

                    sQuery = " Select isnull(Max( " & _
                                    " case	When T.Malzeme is not null Then 1 " & _
                                            " When D.Item is not null Then 2 " & _
                                            " Else 0 End ),0) As Durum " & _
                                    " from shppack S " & _
                                    " Left Join Tr_AyniyatLog T " & _
                                    " On T.SevkNo = cast(S.ShpNo  as nvarchar(7)) " & _
                                    " Left Join dcitem D " & _
                                    " On D.Document_num =cast(S.ShpNo  as nvarchar(7)) " & _
                                    " Where S.InvNo=" & sTirnakEkle(row.Cells("FaturaNo").Text)

                    dtTemp = db.RunSql(sQuery)

                    If Not dtTemp Is Nothing AndAlso dt.Rows.Count > 0 Then

                        If dtTemp.Rows(0)("Durum").ToString = 1 Then
                            If MessageBox.Show(row.Cells("FaturaNo").Text & " Nolu Faturanın Hatalı Ayniyat Hareketleri Bulunuyor. Devam Etmek İstiyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                                Exit Sub
                            End If
                        End If

                        If dtTemp.Rows(0)("Durum").ToString = 2 Then
                            If MessageBox.Show(row.Cells("FaturaNo").Text & " Nolu Faturanın Bekleyen Ayniyat Hareketleri Bulunuyor. Devam Etmek İstiyor musunuz?", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                    End If

                    If row.Cells("BeyanNo").Text <> "0" Then

                        nGumrNo = row.Cells("BeyanNo").Text

                        dBeyanTarihi = IIf(row.Cells("BeyanTarihi").Text = "", Nothing, row.Cells("BeyanTarihi").Text)

                        Exit For

                    End If

                    If Not sFaturaNoLar.Contains(row.Cells("FaturaNo").Text) Then

                        sFaturaNoLar = IIf(sFaturaNoLar = "", "", sFaturaNoLar & ",") & row.Cells("FaturaNo").Text

                    End If

                Next

            End If

            If nGumrNo = 0 Then

                nGumrNo = SeriNoAl("Beyan")

                dBeyanTarihi = Now.ToString("yyyy-MM-dd")

                sQuery = " Update ShpPack" & _
                            " Set BeyanNo=" & nGumrNo & _
                            " , BeyanTarihi=" & sTirnakEkle(dBeyanTarihi.ToString("yyyy-MM-dd")) & _
                            " Where InvNo in (" & sFaturaNoLar & ")"

                db.RunSql(sQuery)

            End If

            sQuery = "Select * " & _
                        " From BeyannamePrnBaslik" & _
                        " Where BeyanNo=" & nGumrNo

            dtBaslik = db.RunSql(sQuery)

            If Not dtBaslik Is Nothing AndAlso dtBaslik.Rows.Count > 0 Then

                For Each rowBaslik As DataRow In dtBaslik.Rows

                    sQuery = " Select *" & _
                                " From BeyannamePrnDetay" & _
                                " Where ShpNo=" & rowBaslik.Item("ShpNo").ToString

                    dtDetay = db.RunSql(sQuery)

                    If Not dtDetay Is Nothing AndAlso dtDetay.Rows.Count > 0 Then

                        dt = SelectDistinct(dtDetay, "PICKNO")

                        nPSay = 0
                        nHKSay = 0

                        'For Each rowPickno As DataRow In dt.Rows

                        '    nPSay = nPSay + nLookup("Count(ETKSERINO)", "ETIKETDTY", "PLTETK=1 AND PICKNO=" & rowPickno.Item("PICKNO").ToString)

                        '    nHKSay = nHKSay + nLookup("Count(ETKSERINO)", "ETIKETDTY", "KUTUETK=1 AND PLTNO<1 AND PICKNO=" & rowPickno.Item("PICKNO").ToString)

                        'Next

                        For Each rowDetay As DataRow In dtDetay.Rows

                            nBirimFyt = rowDetay.Item("PRICE").ToString * rowDetay.Item("FYTKTS").ToString

                            nTutar = nBirimFyt * rowDetay.Item("MIKTAR").ToString

                            'If rowDetay.Item("SEQNO").ToString = "1" Then

                            nHKSay = rowDetay.Item("HKSAY").ToString

                            nPSay = rowDetay.Item("PSAY").ToString

                            nKSay = rowDetay.Item("KSAY").ToString

                            nNetAgr = rowDetay.Item("NETAGR").ToString

                            nBrtAgr = rowDetay.Item("BRTAGR").ToString

                            nHacim = rowDetay.Item("HACIM").ToString

                            nKapAdedi = rowDetay.Item("HKSAY").ToString + rowDetay.Item("PSAY").ToString

                            'End If

                            sTermCode = rowDetay.Item("terms_code").ToString

                            sSQL = " INSERT INTO ANSGUMBILD " & _
                                    "(GUMNO,INVNO,INVTAR,DHZ969,SEVKTAR,DHCANB,DHB9CD,DHBYTX," & _
                                    " KSAY,PSAY,HKSAY,NETAGR,BRTAGR,HACIM,AGOB,HCOB,TUTAR," & _
                                    " CUSNM,FERIDX,NAKL_KOD,NAKL_TNM,SHPNM," & _
                                    " TOPMIKTAR,TERMCD) VALUES (" & _
                                    rowDetay.Item("BeyanNo").ToString & "," & _
                                    rowBaslik.Item("InvNo").ToString & "," & _
                                    sTarih(rowBaslik.Item("FATURATAR").ToString) & "," & _
                                    rowBaslik.Item("SHPNO").ToString & "," & _
                                    sTarih(rowDetay.Item("SEVKTAR").ToString) & "," & _
                                    sTirnakEkle(rowDetay.Item("Cust").ToString) & "," & _
                                    sTirnakEkle(rowBaslik.Item("Shipto").ToString) & "," & _
                                    sTirnakEkle(rowBaslik.Item("ShipToName").ToString) & "," & _
                                    nKSay & "," & _
                                    nPSay & "," & _
                                    nHKSay & "," & _
                                    nNetAgr & "," & _
                                    nBrtAgr & "," & _
                                    nHacim & "," & _
                                    sTirnakEkle(rowDetay.Item("AGOB").ToString) & "," & _
                                    sTirnakEkle(rowDetay.Item("HCOB").ToString) & "," & _
                                    nTutar & "," & _
                                    sTirnakEkle(rowDetay.Item("CustName").ToString) & "," & _
                                    sTirnakEkle(rowBaslik.Item("Curr_Code").ToString) & "," & _
                                    sTirnakEkle(rowDetay.Item("Carrier").ToString) & "," & _
                                    sTirnakEkle(rowBaslik.Item("CarrierName").ToString) & "," & _
                                    sTirnakEkle(rowDetay.Item("AmbarAdi").ToString) & "," & _
                                    rowDetay.Item("MIKTAR").ToString & "," & _
                                    sTirnakEkle(sTermCode) & "" & _
                                    ")"

                            dbAccess.RunSql(sSQL)

                            '

                        Next rowDetay

                    End If

                Next rowBaslik

            End If

            sQuery = "select matltran.createdate,matltran.document_num,matltran.lot," & _
                            " matltran.loc,matltran.item,item.description,item.u_m, " & _
                            " (matltran.qty * -1) As qty " & _
                    " from matltran" & _
                    " left join item on  matltran.item=item.item " & _
                    " where 1 = 1 " & _
                        " And trans_type+ref_type='GI' " & _
                        " And document_num in ( select distinct cast(shpno as nvarchar(20))" & _
                                                    " from shppack " & _
                                                    " where beyanno=" & nGumrNo & ")"

            dtBaslik = db.RunSql(sQuery)

            If Not dtBaslik Is Nothing AndAlso dtBaslik.Rows.Count > 0 Then

                For Each rowBaslik As DataRow In dtBaslik.Rows

                    dGGBTarih = sLookup("GGBTAR", "GGBPF", "GGBNO=" & sTirnakEkle(rowBaslik.Item("lot").ToString))

                    'If dGGBTarih = "" Then

                    '    dGGBTarih = Now

                    'ElseIf Not IsDate(dGGBTarih) Then

                    '    dGGBTarih = DateSerial("20" & dGGBTarih.Substring(0, 2), dGGBTarih.Substring(2, 2), dGGBTarih.Substring(4, 2))

                    'End If

                    Dim sLocDesc As String

                    sLocDesc = sLookup("Description", "Location", "Loc=" & sTirnakEkle(rowBaslik.Item("loc").ToString))

                    sSQL = " INSERT INTO ANSAYNIYAT " & _
                            "( GGBNO, GGBTAR, FDATE, REFNO, " & _
                            " LBHNO, LOKASYON, ITNBR, ITDSC, UNMSR, MIKTAR) VALUES (" & _
                            nGumrNo & "," & _
                            sTirnakEkle(dGGBTarih) & "," & _
                            sTarih(rowBaslik.Item("createdate").ToString) & "," & _
                            sTirnakEkle(rowBaslik.Item("document_num").ToString) & "," & _
                            sTirnakEkle(rowBaslik.Item("lot").ToString) & "," & _
                            sTirnakEkle(sLocDesc) & "," & _
                            sTirnakEkle(rowBaslik.Item("item").ToString) & "," & _
                            sTirnakEkle(rowBaslik.Item("description").ToString) & "," & _
                            sTirnakEkle(rowBaslik.Item("u_m").ToString) & "," & _
                            rowBaslik.Item("qty").ToString & _
                            ")"

                    dbAccess.RunSql(sSQL)

                Next rowBaslik

            End If

            sQuery = "Select *" & _
                        " From ANSGUMBILD"

            dtTemp = dbAccess.RunSql(sQuery)

            RaporCagirOzet("RGumruk_Bildirimi.rpt", , , , , , , dtTemp)

            sQuery = "Select *" & _
                        " From ANSAYNIYAT"

            dtTemp = dbAccess.RunSql(sQuery)

            RaporCagirOzet("RAyniyat_Raporu.rpt", , , , , , , dtTemp)

            btnSorgula_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("İşlem Gerçekleştirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged, GridEX1.Click, GridEX1.DoubleClick
        GridSec(GridEX1, "BeyanNo", sender, e)
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