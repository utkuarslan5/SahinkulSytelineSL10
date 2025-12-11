Public Class frmCLYenidenBasim

    #Region "Fields"

    Dim db As New Core.Data(My.Settings.ConnectionString)
    Dim dbAccess As New Core.DataForDB2(My.Settings.AccessConnection)
    Dim ds As DataSet
    Dim dt, dtTemp As New DataTable
    Dim sQuery As String

    #End Region 'Fields

    #Region "Methods"

    Private Sub btnCekmeListesiIptal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCekmeListesiIptal.Click
        System.Windows.Forms.Cursor.Current = Cursors.Arrow

        Try

            If MessageBox.Show("Seçili Çekme Listesi Silinecektir Onaylýyormusunuz?!", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then

                Exit Sub

            End If

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            Dim row As Janus.Windows.GridEX.GridEXRow

            For Each row In checkedRows

                If sLookup("Max(DURUM)", "TRCEKLIST", "CEKLIST=" & row.Cells("CEKLIST").Text) <= "2" Then

                    sQuery = "Update MBADREP" & _
                            " Set packed=0" & _
                                " ,qty_packed=0" & _
                                " From TRCEKLIST " & _
                            " Where TRCEKLIST.CEKLIST=" & row.Cells("CEKLIST").Text & _
                            " And TRCEKLIST.ADFCNB=MBADREP.ADFCNB" & _
                            " And TRCEKLIST.ADCVNB=MBADREP.ADCVNB" & _
                            " And TRCEKLIST.ADAITX=MBADREP.ADAITX"

                    db.RunSql(sQuery, True)

                    sQuery = "Delete From TRCEKLIST" & _
                            " Where CEKLIST=" & row.Cells("CEKLIST").Text

                    db.RunSql(sQuery, True)

                    sQuery = "Delete From ETIKETDTY" & _
                            " Where PICKNO=" & row.Cells("CEKLIST").Text

                    db.RunSql(sQuery, True)

                Else

                    sQuery = " Select isnull(Max(SEVKNO),0)" & _
                                " From TRCEKLIST" & _
                                " Where CEKLIST=" & row.Cells("CEKLIST").Text & _
                                " Having isnull(Max(SEVKNO),0) <> 0 "

                    dt = db.RunSql(sQuery)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        MessageBox.Show(row.Cells("CEKLIST").Text & " nolu çekme " & dt.Rows(0).Item(0).ToString & " nolu sevkiyatla iliþkili önce sevkiyatý iptal ediniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Exit Sub

                    ElseIf dt.Rows.Count = 0 Then

                        sQuery = "Delete From TRCEKLIST" & _
                                " Where CEKLIST=" & row.Cells("CEKLIST").Text

                        db.RunSql(sQuery, True)

                        sQuery = "Update MBADREP" & _
                                " Set packed=0" & _
                                    " ,qty_packed=0" & _
                                " Where qty_packed=" & row.Cells("CEKLIST").Text

                        db.RunSql(sQuery, True)

                    End If

                End If

            Next

            ' MessageBox.Show("Çekme Listesi Silme Ýþlemi Tamamlanmýþtýr...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            btnSorgula_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            System.Windows.Forms.Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub btnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckedAll.Click
        GridEX1.CheckAllRecords()
    End Sub

    Private Sub btnSorgula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSorgula.Click
        Dim sDate As String

        sDate = dtmCekmeTarihi.Value.ToString("1yyMMdd")

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If txtMusteri2.Text <> "" AndAlso txtMusteri2.Text < txtMusteri1.Text Then

            MessageBox.Show("Lütfen Ýkinci Müþteri Numarasýný Ýlkinden Büyük Seçiniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Exit Sub

        End If

        Try

            sQuery = "SELECT C6CANB, CUSNM, C6B9CD, C6F1CD,  " & _
                            " C6FNST, ADA3CD , CEKDATE, CRDUSR, CEKLIST" & _
                    " FROM  TRCEKLIST  " & _
                    " WHERE DURUM" & IIf(rdbIlkBasim.Checked, "='0'", _
                                        IIf(rdbTekrarBasim.Checked, " IN ('1','2')", " In ('3','X','4')"))

            If txtMusteri1.Text <> "" Then

                If txtMusteri2.Text = "" Then

                    sQuery = sQuery & "    and c6canb='" & txtMusteri1.Text & "'"

                Else

                    sQuery = sQuery & " and c6canb>='" & txtMusteri1.Text & "'" & _
                                      " and c6canb<='" & txtMusteri2.Text & "'"

                End If

            End If

            If txtKullanici.Text <> "" Then

                sQuery = sQuery & "    and crdusr='" & txtKullanici.Text & "'"

            End If

            If txtAmbar1.Text <> "" Then

                sQuery = sQuery & "    and ada3cd='" & txtAmbar1.Text & "'"

            End If

            If txtCekmeListeNo.Text <> "" Then

                sQuery = sQuery & "    and CEKLIST=" & txtCekmeListeNo.Text

            End If

            If txtTeslimAlan1.Text <> "" Then

                If txtTeslimAlan2.Text = "" Then

                    sQuery = sQuery & "    and C6B9CD='" & txtTeslimAlan1.Text & "'"

                Else

                    sQuery = sQuery & " and C6B9CD>='" & txtTeslimAlan1.Text & "'" & _
                                      " and C6B9CD<='" & txtTeslimAlan1.Text & "'"

                End If

            End If

            If txtKapi1.Text <> "" Then

                If txtKapi2.Text = "" Then

                    sQuery = sQuery & "    and C6f1cd='" & txtKapi1.Text & "'"

                Else

                    sQuery = sQuery & "    and C6f1cd>='" & txtKapi1.Text & "'" & _
                                        "    and C6f1cd<='" & txtKapi1.Text & "'"

                End If

            End If

            If CheckBox1.Checked Then
                sQuery = sQuery & "    and CEKDATE=" & sDate
            End If

            sQuery = sQuery & " group By C6CANB, CUSNM, C6B9CD, C6F1CD, " & _
                                " C6FNST, ADA3CD, CEKDATE, CRDUSR, CEKLIST "

            sQuery = sQuery & " Order By c6canb, C6B9CD, C6f1cd, CEKDATE "

            dt = db.RunSql(sQuery)

            If Not (dt Is Nothing) AndAlso _
                            dt.Rows.Count > 0 Then

                GridEX1.DataSource = dt

                GridEX1.DataMember = "Cekme"

            Else

                'data yoktur

                'MessageBox.Show(" Kayýt Bulunamadý... ", " Ekip Mapics ", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)

                GridEX1.DataSource = Nothing

            End If

            Duzenle(GridEX1)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub btnUnCheckedAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnCheckedAll.Click
        GridEX1.UnCheckAllRecords()
    End Sub

    Private Sub btnYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYazdir.Click
        Try

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'If txtCekmeListeNo.Text <> "" OrElse txtCekmeListeNo.Text <> "0" Then

            '    MessageBox.Show("Lütfen Çekme Listesi No Seçiniz...!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

            '    Exit Sub

            'End If

            sQuery = " Delete From TRCEKLIST"

            dbAccess.RunSql(sQuery, True)

            Dim tmpCeklist As Integer = 0

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            Dim row As Janus.Windows.GridEX.GridEXRow

            For Each row In checkedRows

                If tmpCeklist <> row.Cells("CEKLIST").Text Then

                    tmpCeklist = row.Cells("CEKLIST").Text

                    sQuery = " Select * " & _
                                " From TRCEKLIST"

                    sQuery = sQuery & " Where "

                    sQuery = sQuery & "CEKLIST=" & row.Cells("CEKLIST").Text

                    sQuery = sQuery & "And CRDUSR='" & row.Cells("CRDUSR").Text & "'"

                    ds = db.RunSql(sQuery, "TRCEKLIST")

                    If Not ds Is Nothing Then

                        If ds.Tables("TRCEKLIST").Rows.Count > 0 Then

                            For i As Integer = 0 To ds.Tables("TRCEKLIST").Rows.Count - 1

                                With ds.Tables("TRCEKLIST").Rows(i)

                                    sQuery = " INSERT INTO TRCEKLIST" & _
                                                " (CEKLIST, C6CANB, CUSNM, C6B9CD, C6F1CD, CJCBTX, " & _
                                                " CDAFYV, ADCVNB, ADAITX, ITDSC, ITEM,REFADI, MOHTQ, " & _
                                                " ADDZVA, ADAQQT, ADBJDT, KMIK, C6FNST, ADIIST, DEF1CD , " & _
                                                " ADA3CD ,CEKDATE,ADFCNB,HNDCODE,KANBANNO,AMBKOD, PKSAY , " & _
                                                " KUTUSAYI, PKSIR, PALETSAYI, Ordno, L3PREF) " & _
                                            " VALUES(" & .Item("CEKLIST").ToString & ",'" & .Item("C6CANB").ToString & "'," & _
                                            "'" & .Item("CUSNM").ToString & "','" & .Item("C6B9CD").ToString & "'," & _
                                            "'" & .Item("C6F1CD").ToString & "','" & .Item("CJCBTX").ToString & "'," & _
                                            "'" & .Item("CDAFYV").ToString & "','" & .Item("ADCVNB").ToString & "'," & _
                                            "'" & .Item("ADAITX").ToString & "','" & .Item("ITDSC").ToString & "'," & _
                                            "'" & .Item("ITEM").ToString & "','" & .Item("REFADI").ToString & "'," & _
                                            .Item("MOHTQ").ToString.Replace(",", "") & "," & _
                                            .Item("ADDZVA").ToString.Replace(",", "") & "," & _
                                            .Item("ADAQQT").ToString.Replace(",", "") & "," & _
                                            .Item("ADBJDT").ToString & _
                                            "," & .Item("KMIK").ToString.Replace(",", "") & "," & _
                                            "'" & .Item("C6FNST").ToString & "','" & .Item("ADIIST").ToString & "'," & _
                                            "'" & .Item("DEF1CD").ToString & "','" & .Item("ADA3CD").ToString & "'," & _
                                            "'" & .Item("CEKDATE").ToString & "'," & .Item("ADFCNB").ToString & "," & _
                                            "'" & "" & "'," & _
                                            "'" & .Item("KANBANNO").ToString & "','" & .Item("AMBKOD").ToString & "'," & .Item("PKSAY").ToString & "," & _
                                            .Item("KSAY").ToString & "," & .Item("PKSIR").ToString & "," & .Item("PSAY").ToString & ",'" & _
                                            .Item("JOBNUMBER").ToString & "','" & .Item("L3PREF").ToString & "')"

                                    dbAccess.RunSql(sQuery, True)

                                End With

                            Next i

                            If rdbSevkEdildi.Checked = False Then

                                sQuery = " Update TRCEKLIST" & _
                                         " Set Durum='1'" & _
                                         " Where CEKLIST=" & row.Cells("CEKLIST").Text

                                db.RunSql(sQuery, True)

                            End If

                        End If

                    End If

                End If

            Next

            Dim sSiparisNoRapor As String = ""
            Dim sL3PREF As String

            sQuery = " Select CEKLIST, C6CANB, C6B9CD, ADCVNB, ADFCNB, ADAITX, AMBKOD, ADAQQT, KUTUSAYI, PALETSAYI,ORDNO,L3PREF" & _
                        " From TRCEKLIST"

            dt = dbAccess.RunSql(sQuery)

            If Not dt Is Nothing Then

                If dt.Rows.Count > 0 Then

                    For i As Integer = 0 To dt.Rows.Count - 1

                        With dt.Rows(i)

                            Dim sAmbtan As String = ""
                            Dim sKkod As String = ""
                            Dim sPkod As String = ""
                            Dim sKpkkod As String = ""
                            Dim sSpkod As String = ""
                            Dim sKTnm As String = ""
                            Dim sPTnm As String = ""
                            Dim sKPTnm As String = ""
                            Dim sSPTnm As String = ""
                            Dim nKpmik, nSpmik, nMAgr, _
                                nKAgr, nPAgr, nKPAgr, nSPAgr, _
                                nKHcm, nPHcm, _
                                nTopNetAgr, nTopBrtAgr, nTopHacim As Double

                            Dim nMiktar, nKsay, nPsay As Double

                            Dim nPickNo As Double
                            Dim nSiparisSiraNo As Integer
                            Dim sMusteriNo As String = ""
                            Dim sPlantNo As String = ""
                            Dim sSiparisNo As String = ""
                            Dim sMalzemeNo As String = ""
                            Dim sAmbalajKodu As String = ""
                            Dim sOlcuBirimi As String = ""

                            Dim sPlantName As String = ""
                            Dim sTrnCode As String = ""

                            sPlantNo = .Item("C6B9CD").ToString

                            sMusteriNo = .Item("C6CANB").ToString

                            nPickNo = .Item("CEKLIST").ToString

                            nSiparisSiraNo = .Item("ADFCNB").ToString

                            sSiparisNo = .Item("ADCVNB").ToString

                            sL3PREF = .Item("L3PREF").ToString

                            If Not sSiparisNoRapor.Contains(.Item("ORDNO").ToString) Then

                                sSiparisNoRapor = IIf(sSiparisNoRapor = "", .Item("ORDNO").ToString, sSiparisNoRapor & "," & .Item("ORDNO").ToString)

                            End If

                            sMalzemeNo = .Item("ADAITX").ToString

                            sAmbalajKodu = .Item("AMBKOD").ToString

                            nMiktar = .Item("ADAQQT").ToString

                            nKsay = .Item("KUTUSAYI").ToString

                            nPsay = .Item("PALETSAYI").ToString

                            sQuery = "select AMBTAN, KKOD, PKOD, KPKKOD, SPKOD, KPMIK, SPMIK " & _
                                        " from Itmpack" & _
                                        " Where Itnbr=" & sTirnakEkle(sMalzemeNo) & _
                                        " And Ambkod=" & sTirnakEkle(sAmbalajKodu)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(sAmbtan, dtTemp, 0, "AMBTAN")
                            GetRowInfo(sKkod, dtTemp, 0, "KKOD")
                            GetRowInfo(sPkod, dtTemp, 0, "PKOD")
                            GetRowInfo(sKpkkod, dtTemp, 0, "KPKKOD")
                            GetRowInfo(sSpkod, dtTemp, 0, "SPKOD")
                            GetRowInfo(nKpmik, dtTemp, 0, "KPMIK")
                            GetRowInfo(nSpmik, dtTemp, 0, "SPMIK")

                            sQuery = "select unit_weight, u_m " & _
                                         " from Item" & _
                                         " Where Item=" & sTirnakEkle(sMalzemeNo)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(nMAgr, dtTemp, 0, "unit_weight")

                            GetRowInfo(sOlcuBirimi, dtTemp, 0, "u_m")

                            If sKkod <> "" Then

                                sQuery = "select Description, unit_weight, Uf_BrmHc " & _
                                            " from Item" & _
                                            " Where Item=" & sTirnakEkle(sKkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(sKTnm, dtTemp, 0, "Description")
                                GetRowInfo(nKAgr, dtTemp, 0, "unit_weight")
                                GetRowInfo(nKHcm, dtTemp, 0, "Uf_BrmHc")

                            End If

                            If sPkod <> "" Then

                                sQuery = "select Description, unit_weight, Uf_BrmHc " & _
                                            " from Item" & _
                                            " Where Item=" & sTirnakEkle(sPkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(sPTnm, dtTemp, 0, "Description")
                                GetRowInfo(nPAgr, dtTemp, 0, "unit_weight")
                                GetRowInfo(nPHcm, dtTemp, 0, "Uf_BrmHc")

                            End If

                            If sKpkkod <> "" Then

                                sQuery = "select Description, unit_weight " & _
                                            " from Item" & _
                                            " Where Item=" & sTirnakEkle(sKpkkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(sKPTnm, dtTemp, 0, "Description")
                                GetRowInfo(nKPAgr, dtTemp, 0, "unit_weight")

                            End If

                            If sSpkod <> "" Then

                                sQuery = "select Description, unit_weight " & _
                                            " from Item" & _
                                            " Where Item=" & sTirnakEkle(sSpkod)

                                dtTemp = db.RunSql(sQuery)

                                GetRowInfo(sSPTnm, dtTemp, 0, "Description")
                                GetRowInfo(nSPAgr, dtTemp, 0, "unit_weight")

                            End If

                            sQuery = "select c.Cust_num, c.Cust_Seq, ca.Name, c.Uf_B9CD, c.Uf_TRNCODE" & _
                                        " from customer c " & _
                                        " Left Join custaddr ca  " & _
                                             " On c.Cust_num=ca.Cust_num and c.cust_seq=ca.cust_seq " & _
                                        " Where c.Cust_num=" & sTirnakEkle(sMusteriNo) & _
                                        " And c.Uf_B9CD=" & sTirnakEkle(sPlantNo)

                            dtTemp = db.RunSql(sQuery)

                            GetRowInfo(sPlantName, dtTemp, 0, "Name")
                            GetRowInfo(sTrnCode, dtTemp, 0, "Uf_TRNCODE")

                            nTopNetAgr = nMiktar * nMAgr

                            nTopBrtAgr = nTopNetAgr + _
                                        (nKsay * nKAgr) + _
                                        (nPsay * nPAgr) + _
                                        (nPsay * nKpmik * nKPAgr) + _
                                        (nPsay * nSpmik * nSPAgr)

                            nTopHacim = (nKsay * nKHcm) + (nPsay * nPHcm)
                            nKpmik = nKpmik * nPsay
                            nSpmik = nSpmik * nPsay

                            sQuery = " Update TRCEKLIST" & _
                                        " Set PlantName=" & sTirnakEkle(sPlantName) & _
                                        " , AmbTan=" & sTirnakEkle(sAmbtan) & _
                                        " , TopNetAgr=" & nTopNetAgr & _
                                        " , TopBrtAgr=" & nTopBrtAgr & _
                                        " , TopHacim=" & nTopHacim & _
                                        " , AGOB='KG' " & _
                                        " , HCOB='M3' " & _
                                        " , KKOD=" & sTirnakEkle(sKkod) & _
                                        " , KTNM=" & sTirnakEkle(sKTnm) & _
                                        " , PKOD=" & sTirnakEkle(sPkod) & _
                                        " , PTNM=" & sTirnakEkle(sPTnm) & _
                                        " , KPKKOD=" & sTirnakEkle(sKpkkod) & _
                                        " , KPKTNM=" & sTirnakEkle(sKPTnm) & _
                                        " , SPKOD=" & sTirnakEkle(sSpkod) & _
                                        " , SPTNM=" & sTirnakEkle(sSPTnm) & _
                                        " , KPMIK=" & nKpmik & _
                                        " , SPMIK=" & nSpmik & _
                                        " , TransportCode=" & sTirnakEkle(sTrnCode) & _
                                        " , UNMSR=" & sTirnakEkle(sOlcuBirimi) & _
                                        " , L3PREF=" & sTirnakEkle(sL3PREF) & _
                                    " Where CEKLIST=" & nPickNo & _
                                    " And ADCVNB=" & sTirnakEkle(sSiparisNo) & _
                                    " And ADFCNB=" & nSiparisSiraNo & _
                                    " And ADAITX=" & sTirnakEkle(sMalzemeNo)

                            dbAccess.RunSql(sQuery, True)

                        End With

                    Next i

                End If

            End If

            RaporCagir("RCeki_Liste.rpt")

            If sSiparisNoRapor <> "" Then

                dbAccess.RunSql("Delete From IsmeriYazdir")

                Dim sJob As String = ""

                For i As Integer = 0 To sSiparisNoRapor.Split(",").Length - 1

                    'sQuery = " Insert Into IsmeriYazdir " & _
                    '             " (Isemri)" & _
                    '             " Values (" & _
                    '             sTirnakEkle(sSiparisNoRapor.Split(",")(i).ToString()) & _
                    '             ")"

                    'dbAccess.RunSql(sQuery)
                    If i <> sSiparisNoRapor.Split(",").Length - 1 Then
                        sJob = sJob & IIf(sJob = "", "", ",") & sTirnakEkle(sSiparisNoRapor.Split(",")(i).ToString())
                    Else
                        sJob = sJob & IIf(sJob = "", "", ",") & sTirnakEkle(sSiparisNoRapor.Split(",")(i).ToString())
                    End If

                Next

                sQuery = "  SELECT job.qty_released, job.item As Urun, jobmatl.item As Bilesen, jobmatl.description As BilesenTanim, " & _
                                " jobmatl.qty_issued, job.job, job.qty_scrapped,  " & _
                                " jobmatl.matl_qty, job.type, job.description As UrunTanim, " & _
                                " job.whse, item.drawing_nbr, TRCEKLIST.C6B9CD, " & _
                                " TRCEKLIST.CEKLIST, TRCEKLIST.JOBNUMBER " & _
                            " FROM   job job " & _
                            " INNER JOIN TRCEKLIST TRCEKLIST " & _
                             " On job.job=TRCEKLIST.JOBNUMBER " & _
                            " INNER JOIN jobmatl jobmatl " & _
                             " ON (job.job=jobmatl.job) AND (job.suffix=jobmatl.suffix) " & _
                            " INNER JOIN item item " & _
                                " ON job.item=item.item " & _
                            " wHERE job.type='J'" & _
                            " aND Job.job in (" & sJob & ")"

                dtTemp = db.RunSql(sQuery)

                frmRapor.Parametre.Clear()

                RaporCagir("RCekme_Raporu" & IIf(sInitialCatalog = "SLPBG_App", "_BG", "") & ".rpt", , , , , , , dtTemp)

                'frmRapor.Parametre.Add(New Parametre("Job", sSiparisNoRapor))

                'For i As Integer = 0 To sSiparisNoRapor.Split(",").Length - 1

                '    frmRapor.Parametre.Clear()

                '    frmRapor.sTip = "Sql"

                '    frmRapor.sRapor = AppDomain.CurrentDomain.SetupInformation.ApplicationBase & "RCekme_Raporu.rpt"

                '    frmRapor.Parametre.Add(New Parametre("Job", sSiparisNoRapor.Split(",")(i).ToString))

                '    frmRapor.Refresh()
                '    '
                '    frmRapor.ShowDialog()

                'Next

            End If
            '
            System.Windows.Forms.Cursor.Current = Cursors.Default

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            System.Windows.Forms.Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            dtmCekmeTarihi.Enabled = True
        Else
            dtmCekmeTarihi.Enabled = False
        End If
    End Sub

    Private Sub cmdCekmeListesiIptal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        System.Windows.Forms.Cursor.Current = Cursors.Arrow

        Try

            If MessageBox.Show("Seçili Çekme Listesi Silinecektir Onaylýyormusunuz?!", "Ekip Mapics", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then

                Exit Sub

            End If

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            Dim row As Janus.Windows.GridEX.GridEXRow

            For Each row In checkedRows

                If sLookup("Max(DURUM)", "TRCEKLIST", "CEKLIST=" & row.Cells("CEKLIST").Text) <= "2" Then

                    sQuery = "Delete From TRCEKLIST" & _
                            " Where CEKLIST=" & row.Cells("CEKLIST").Text

                    db.RunSql(sQuery, True)

                    sQuery = "Delete From ETIKETDTY" & _
                            " Where PICKNO=" & row.Cells("CEKLIST").Text

                    db.RunSql(sQuery, True)

                    sQuery = "Update MBADREP" & _
                            " Set packed='0'" & _
                                " ,qty_packed=0" & _
                            " Where qty_packed=" & row.Cells("CEKLIST").Text

                    db.RunSql(sQuery, True)

                Else

                    sQuery = " Select isnull(Max(SEVKNO),0)" & _
                                " From TRCEKLIST" & _
                                " Where CEKLIST=" & row.Cells("CEKLIST").Text

                    dt = db.RunSql(sQuery)

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        MessageBox.Show(row.Cells("CEKLIST").Text & " nolu çekme " & dt.Rows(0).Item(0).ToString & " nolu sevkiyatla iliþkili önce sevkiyatý iptal ediniz!", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Exit Sub

                    ElseIf dt.Rows.Count = 0 Then

                        sQuery = "Delete From TRCEKLIST" & _
                                " Where CEKLIST=" & row.Cells("CEKLIST").Text

                        db.RunSql(sQuery, True)

                        sQuery = "Update MBADREP" & _
                                " Set packed='0'" & _
                                    " ,qty_packed=0" & _
                                " Where qty_packed=" & row.Cells("CEKLIST").Text

                        db.RunSql(sQuery, True)

                    End If

                End If

            Next

            ' MessageBox.Show("Çekme Listesi Silme Ýþlemi Tamamlanmýþtýr...", "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Information)

            btnSorgula_Click(sender, e)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            System.Windows.Forms.Cursor.Current = Cursors.Default

        End Try
    End Sub

    Private Sub cmdYazdir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            sQuery = " Delete From TRCEKLIST"

            dbAccess.RunSql(sQuery, True)

            Dim tmpCeklist As Integer = 0

            Dim checkedRows() As Janus.Windows.GridEX.GridEXRow

            checkedRows = Me.GridEX1.GetCheckedRows()

            Dim row As Janus.Windows.GridEX.GridEXRow

            For Each row In checkedRows

                If tmpCeklist <> row.Cells("CEKLIST").Text Then

                    tmpCeklist = row.Cells("CEKLIST").Text

                    sQuery = " Select * " & _
                                " From TRCEKLIST"

                    sQuery = sQuery & " Where "

                    sQuery = sQuery & "CEKLIST=" & row.Cells("CEKLIST").Text

                    sQuery = sQuery & "And CRDUSR='" & row.Cells("CRDUSR").Text & "'"

                    ds = db.RunSql(sQuery, "TRCEKLIST")

                    If Not ds Is Nothing Then

                        If ds.Tables("TRCEKLIST").Rows.Count > 0 Then

                            For i As Integer = 0 To ds.Tables("TRCEKLIST").Rows.Count - 1

                                With ds.Tables("TRCEKLIST").Rows(i)

                                    sQuery = " INSERT INTO TRCEKLIST" & _
                                                " (CEKLIST, C6CANB, CUSNM, C6B9CD, C6F1CD, CJCBTX, " & _
                                                " CDAFYV, ADCVNB, ADAITX, ITDSC, ITEM,REFADI, MOHTQ, " & _
                                                " ADDZVA, ADAQQT, ADBJDT, KMIK, C6FNST, ADIIST, DEF1CD , " & _
                                                " ADA3CD ,CEKDATE,ADFCNB,HNDCODE,KANBANNO,AMBKOD, PKSAY ) " & _
                                            " VALUES(" & .Item("CEKLIST").ToString & ",'" & .Item("C6CANB").ToString & "'," & _
                                            "'" & .Item("CUSNM").ToString & "','" & .Item("C6B9CD").ToString & "'," & _
                                            "'" & .Item("C6F1CD").ToString & "','" & .Item("CJCBTX").ToString & "'," & _
                                            "'" & .Item("CDAFYV").ToString & "','" & .Item("ADCVNB").ToString & "'," & _
                                            "'" & .Item("ADAITX").ToString & "','" & .Item("ITDSC").ToString & "'," & _
                                            "'" & .Item("ITEM").ToString & "','" & .Item("REFADI").ToString & "'," & _
                                            .Item("MOHTQ").ToString.Replace(",", "") & "," & _
                                            .Item("ADDZVA").ToString.Replace(",", "") & "," & _
                                            .Item("ADAQQT").ToString.Replace(",", "") & "," & _
                                            .Item("ADBJDT").ToString & _
                                            "," & .Item("KMIK").ToString.Replace(",", "") & "," & _
                                            "'" & .Item("C6FNST").ToString & "','" & .Item("ADIIST").ToString & "'," & _
                                            "'" & .Item("DEF1CD").ToString & "','" & .Item("ADA3CD").ToString & "'," & _
                                            "'" & .Item("CEKDATE").ToString & "'," & .Item("ADFCNB").ToString & "," & _
                                            "'" & "" & "'," & _
                                            "'" & .Item("KANBANNO").ToString & "','" & .Item("AMBKOD").ToString & "'," & .Item("PKSAY").ToString & ")"

                                    dbAccess.RunSql(sQuery, True)

                                End With

                            Next i

                            If rdbSevkEdildi.Checked = False Then

                                sQuery = " Update TRCEKLIST" & _
                                         " Set Durum='1'" & _
                                         " Where CEKLIST=" & row.Cells("CEKLIST").Text

                                db.RunSql(sQuery, True)

                            End If

                        End If

                    End If

                End If

            Next

            RaporCagir("rptCEKILISTESI.rpt")

            System.Windows.Forms.Cursor.Current = Cursors.Default

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub frmCLYenidenBasim_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            txtKullanici.Text = KullaniciAdi

            'sQuery = "select distinct CEKLIST " & _
            '            " from  TRCEKLIST"

            'ds = db.RunSql(sQuery, "TRCEKLIST")

            'cmbCekmeListesiNo1.DataSource = ds

            'cmbCekmeListesiNo1.DataMember = "TRCEKLIST"

            'cmbCekmeListesiNo2.DataSource = ds

            'cmbCekmeListesiNo2.DataMember = "TRCEKLIST"

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub txtMusteri1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri1.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Taraf,Name as Tanim " & _
                            " From CustAddr" & _
                            " Where cust_seq=0"

            FindFormCagir(ssorgu, "Taraf", "Tanim", txtMusteri1.Text, txtMusteriAdi1.Text)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    Private Sub txtMusteri2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMusteri2.ButtonClick
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim ssorgu As String

            ssorgu = "SELECT Distinct Cust_Num as Taraf,Name as Tanim " & _
                            " From CustAddr"

            FindFormCagir(ssorgu, "Taraf", "Tanim", txtMusteri2.Text, txtMusteriAdi2.Text)

        Catch ex As Exception

            MessageBox.Show("Ýþlem Gerçekleþtirilemedi" & vbNewLine & "Hata...:" & ex.Message, "Ekip Mapics", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            Me.Cursor = Cursors.Arrow

        End Try
    End Sub

    #End Region 'Methods

End Class